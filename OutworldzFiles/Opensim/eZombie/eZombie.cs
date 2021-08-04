using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using Mono.Addins;
using log4net;
using Nini.Config;
using OpenMetaverse;
using OpenMetaverse.Rendering;
using OpenSim.Framework;
using OpenSim.Region.Framework.Interfaces;
using OpenSim.Region.Framework.Scenes;


[assembly: Addin( "eZombie", OpenSim.VersionInfo.VersionNumber )]
[assembly: AddinDependency( "OpenSim.Region.Framework", OpenSim.VersionInfo.VersionNumber )]

namespace eZombies {

  [Extension( Path = "/OpenSim/RegionModules", NodeName = "RegionModule", Id = "eZombieMod" )]
  public partial class eZombie : INonSharedRegionModule {

    private static readonly ILog mLog = LogManager.GetLogger( MethodBase.GetCurrentMethod().DeclaringType );

    #region private variables
    private IConfig mModConfig = null;
    private bool mModEnabled = false;
    private Scene mScene = null;
    private IScriptModuleComms mComms;

    // from opensim.ini [eZombie] section
    private string mModPath = "eZombie/";       // Path where store data on server
    
    //private ezGame mGame = null;        
    private bool mRunning = false;
    private bool mDebug = false;
    private int mHudAttachPoint = -1;

    private IWorldComm mWcomms = null;
    private INPCModule mNpcMod = null;
    private IRendering mMesherMod = null;

    private ezTimer mTmr = null;
    private Random mRnd = null;
    SceneObjectPart mHost = null;

    private ezRezzer mRezzer = null;
    private ezZombier mZombier = null;
    private ezPlayers mPlayers = null;
    private ezRecords mRecord = null;

    //private UUID mParcelID;                         // Parcel ID      
    private Vector3[] mSafeArea = new Vector3[2];     // Safe Area corners
    private Vector3 mPosHall;                         // Hall region coord (where Avatar goes when killed)
    private Vector3 mPosArena;                        // Arena region coord (where Avatar goes when login in game)        
    #endregion private variables

    #region Properties    
    internal Vector3[] SafeArea { get { return mSafeArea; } }
    internal ezTimer Tmr { get { return mTmr; } }
    internal Random Rnd { get { return mRnd; } }
    internal bool IsActive { get { return mModEnabled && mRunning; } }
    internal string ModPath { get { return mModPath; } }

    internal ezRezzer Rezzer { get { return mRezzer; } }
    internal ezZombier Zombier { get { return mZombier; } }
    internal ezPlayers Players { get { return mPlayers; } }
    internal ezRecords Records { get { return mRecord; } }
    internal Vector3 PosHall { get { return mPosHall; } }
    internal Vector3 PosArena { get { return mPosArena; } }
    internal int HudAttachPoint { get { return mHudAttachPoint; } }
    #endregion Properties

    // ----------------------------------------------------
    //  MODULE
    // ----------------------------------------------------
    #region IRegionModule Members
    public string Name {
      get { return GetType().Name; }
    }

    public void Initialise( IConfigSource config ) {

      try {
        if ((mModConfig = config.Configs["eZombie"]) != null) {
          mModEnabled = mModConfig.GetBoolean( "Enabled", mModEnabled );
          if (mModEnabled) {
            mModPath = mModConfig.GetString( "Path", mModPath ).Replace( '\\', '/' );
            if (!mModPath.EndsWith( "/" )) mModPath += "/";            
          }
          Directory.CreateDirectory( mModPath );
        }
      }
      catch (Exception e) {
        mLog.ErrorFormat( "[eZombie] initialization error: {0}", e.Message );
        return;
      }

      mLog.WarnFormat( "[eZombie] module {0} enabled", (mModEnabled ? "is" : "is not") );
    }

    public void PostInitialise() {
      //if (mModEnabled) { mLog.Debug( "[eZombie] PostInitialise" ); }
    }

    public void Close() { }
    public void AddRegion( Scene scene ) { }

    public void RemoveRegion( Scene scene ) {
      mLog.Debug( "[eZombie] region remove and cleanup" );
      if (mRunning)
        SrvStop();
      else {
        // if server was not running then do not run next region startup
        File.Delete( mModPath + GetRegionName( true ) + ".sts" );
      }
      mRecord.Dispose();
      clearVars();
    }

    public void RegionLoaded( Scene scene ) {
      if (!mModEnabled) return;

      mScene = scene;

      mModEnabled = false;
      mComms = mScene.RequestModuleInterface<IScriptModuleComms>();
      if (mComms == null) { mLog.Error( "[eZombie] ScriptModuleComms interface not defined" ); return; }

      mWcomms = mScene.RequestModuleInterface<IWorldComm>();
      if (mWcomms == null) { mLog.Error( "[eZombie] WorldComm interface not defined" ); return; }

      mNpcMod = scene.RequestModuleInterface<INPCModule>();
      if (mNpcMod == null) { mLog.Error( "[eZombie] NPC module not loaded" ); return; }

      List<string> renderers = RenderingLoader.ListRenderers( Util.ExecutingDirectory() );
      if (renderers.Count > 0) mMesherMod = RenderingLoader.LoadRenderer( renderers[0] );
      if (mMesherMod == null) { mLog.Error( "[eZombie] Mesher not loaded" ); return; }

      mModEnabled = true;
      mScene.StoreExtraSetting( "auto_grant_attach_perms", "true" );

      mRnd = Util.RandomClass;
      mTmr = new ezTimer( 200, false );
      mRecord = new ezRecords( this, mModPath );
      //-----------------------

      mComms.RegisterScriptInvocation( this, "ezON" );
      mComms.RegisterScriptInvocation( this, "ezOFF" );
      mComms.RegisterScriptInvocation( this, "ezDebugON" );
      mComms.RegisterScriptInvocation( this, "ezDebugOFF" );
      mComms.RegisterScriptInvocation( this, "ezStatus" );
      mComms.RegisterScriptInvocation( this, "ezInfo" );
      mComms.RegisterScriptInvocation( this, "ezParams" );
      mComms.RegisterScriptInvocation( this, "ezIncomingAv" );
      mComms.RegisterScriptInvocation( this, "ezGunShot" );
      mComms.RegisterScriptInvocation( this, "ezOnGame" );
      mComms.RegisterScriptInvocation( this, "ezTopList" );
      
      // check if exists file status and in case boot the server
      string stsPath = mModPath + GetRegionName( true ) + ".sts";
      if (File.Exists( stsPath )) {
        UUID hostID;
        if (UUID.TryParse( File.ReadAllText( stsPath, System.Text.Encoding.UTF8 ), out hostID ))
          SrvStart( hostID );
      }
    }

    public Type ReplaceableInterface {
      get { return null; }
    }
    #endregion

    #region modInvoike functions
    public object[] ezON( UUID hostID, UUID scriptID ) {
      if (!IsObjectAllowed( hostID )) return new object[] { "Permission danied" };
      return SrvStart( hostID );
    }

    public string ezOFF( UUID hostID, UUID scriptID ) {
      if (!IsObjectAllowed( hostID )) return "Permission danied";
      if (IsActive) SrvStop();
      return "OK";
    }

    public void ezDebugON( UUID hostID, UUID scriptID ) {
      if (IsObjectAllowed( hostID )) mDebug = true;
    }

    public void ezDebugOFF( UUID hostID, UUID scriptID ) {
      if (IsObjectAllowed( hostID )) mDebug = false;
    }

    public int ezStatus( UUID hostID, UUID scriptID ) {
      return IsActive ? 1 : 0;
    }

    public object[] ezInfo( UUID hostID, UUID scriptID ) {
      return SrvGetInfo();
    }

    public object[] ezParams( UUID hostID, UUID scriptID ) {
      if (!IsActive) return new object[] { "Server: OFF" };
      if (SrvPrimID() != hostID) return new object[] { "Permission DANIED!" };

      return SrvGetParams();
    }

    public void ezIncomingAv( UUID hostID, UUID scriptID, string pid, string gname ) {
      UUID id;
      if (IsObjectAllowed( hostID ) && IsActive && UUID.TryParse( pid, out id ))
        mPlayers.IncomingAvi( id, gname );
    }

    public void ezGunShot( UUID hostID, UUID scriptID ) {
      if (IsActive) mPlayers.Gunshot( hostID.ToString() );
    }

    public object[] ezOnGame( UUID hostID, UUID scriptID ) {
      return IsActive ? mPlayers.GetPlayers() : new object[] { };
    }

    public object[] ezTopList( UUID hostID, UUID scriptID ) {
      return Records.GetTopPlayers();
    }    
    #endregion modInvoike functions

    #region Start/Stop server
    public string[] SrvStart( UUID hostID ) {
      List<string> ret = new List<string>();

      if (!mModEnabled) ret.Add( "Server not enabled" );
      if (mRunning) ret.Add( "Server already active" );
      if (ret.Count != 0) return ret.ToArray();

      // get References to Server prim data
      mHost = new SceneObjectPart();
      if (!mScene.TryGetSceneObjectPart( hostID, out mHost )) {
        ret.Add( "Can't get server object" );
        return ret.ToArray();
      }

      // Init vars with defaults
      NpcRemoveAll();
      clearVars();
      mPlayers = new ezPlayers( this );
      mZombier = new ezZombier( this );
      mRezzer = new ezRezzer( this );
      //mParcelID = UUID.Zero;
      mPosHall = Vector3.Zero;
      mPosArena = Vector3.Zero;

      // Load notecards      
      string errd = null;

      mHost.TaskInventory.LockItemsForRead( true );
      foreach (TaskInventoryItem item in mHost.TaskInventory.Values) {
        //mLog.WarnFormat( ">>> {0} == {1}", item.Type, item.Name );
        string name = item.Name.ToLower();
        if (item.Type == 7) {
          if (name == "server.cfg")
            errd = decodeConfig( LoadNotecard( item.AssetID ) );
          else if (name.StartsWith( "npc." ))
            errd = mZombier.NpcAdd( item.Name, LoadNotecard( item.AssetID ) );

          if (!string.IsNullOrEmpty( errd )) { ret.Add( "[" + item.Name + "] " + errd ); break; }
        }
        else if (item.Type == 20) {
          if (name.StartsWith( "npc.die." ))
            mZombier.NpcAnimDieAdd( item.Name );
        }
        else if (item.Type == 1) {
          if (name.StartsWith( "npc.roar." ))
            mZombier.NpcSndRoarAdd( item.AssetID );
        }
        else if (item.Type == 6) {
          if (name.StartsWith( "rez.healbox." )) mRezzer.SetRezObj( ezRezzer.eType.HealBox, item.Name );
          else if (name.StartsWith( "rez.potion." )) mRezzer.SetRezObj( ezRezzer.eType.Potion, item.Name );
          else if (name.StartsWith( "rez.bomb." )) mRezzer.SetRezObj( ezRezzer.eType.Bomb, item.Name );
          else if (name.StartsWith( "rez.prize1." )) mRezzer.SetRezObj( ezRezzer.eType.Prize1, item.Name );
          else if (name.StartsWith( "rez.prize2." )) mRezzer.SetRezObj( ezRezzer.eType.Prize2, item.Name );
          else if (name.StartsWith( "rez.prize3." )) mRezzer.SetRezObj( ezRezzer.eType.Prize3, item.Name );
        }
      }
      mHost.TaskInventory.LockItemsForRead( false );
      if (ret.Count != 0) return ret.ToArray();

      #region Check Loaded params
      // --- check Local params
      //if (mParcelID == UUID.Zero) ret.Add( "Invalid or missing Parcel_ID" );
      if (mSafeArea[0] == mSafeArea[1]) ret.Add( "Invalid or missing Safe_Area_Corner" );
      if (mPosHall == Vector3.Zero) ret.Add( "Invalid or missing Pos_Hall" );
      if (mPosArena == Vector3.Zero) ret.Add( "Invalid or missing Pos_Arena" );
      if (mHudAttachPoint == -1) ret.Add( "Invalid or missing Hud_Attach_To" );
      // --- check Module params
      ret.AddRange( mPlayers.ValidateParam() );
      ret.AddRange( mRezzer.ValidateParam() );
      ret.AddRange( mZombier.ValidateParam() );
      #endregion Check params

      mRecord.StoreToWeb();

      mRunning = true;
      mTmr.Enabled = true;
      mTmr.TCreate( "Main", 1, true, MainCycle );

      string stsPath = mModPath + GetRegionName( true ) + ".sts";
      File.WriteAllText( stsPath, hostID.ToString(), System.Text.Encoding.UTF8 );

      return ret.ToArray();
    }

    public void SrvStop() {
      if (IsActive) {
        mTmr.TRemove( "Main" );
        mTmr.Enabled = false;

        mRezzer.Enabled( false );
        mZombier.Enabled( false );
        mRunning = false;
      }
      clearVars();
    }

    public UUID SrvPrimID() {
      return IsActive ? mHost.UUID : UUID.Zero;
    }

    public string[] SrvGetInfo() {
      List<string> ret = new List<string>() { "Status: " + (IsActive ? "ON" : "OFF") };
      if (IsActive) {
        ret.Add( string.Format( "Players: {0}", mPlayers.Count ) );
        ret.Add( string.Format( "Zombies: {0}", mZombier.Count ) );
        ret.Add( string.Format( "Rezzed: {0}", mRezzer.Count ) );
      }
      return ret.ToArray();
    }

    public string[] SrvGetParams() {
      List<string> ret = new List<string>();
      if (IsActive) {
        //ret.Add( string.Format( "parcel_id: {0}", mParcelID.ToString() ) );
        ret.Add( string.Format( "safe_area_corner: {0}, {1}", mSafeArea[0].ToString(), mSafeArea[0].ToString() ) );
        ret.Add( string.Format( "pos_hall: {0}", mPosHall.ToString() ) );
        ret.Add( string.Format( "pos_arena: {0}", mPosArena.ToString() ) );
        ret.AddRange( mPlayers.ListParam() );
        ret.AddRange( mZombier.ListParam() );
        ret.AddRange( mRezzer.ListParam() );
      }
      return ret.ToArray();
    }
    #endregion Start/Stop server

    #region Helpers
    private void clearVars() {
      if (mPlayers != null) { mPlayers.Dispose(); mPlayers = null; }
      if (mRezzer != null) { mRezzer.Dispose(); mRezzer = null; }
      if (mZombier != null) { mZombier.Dispose(); mZombier = null; }
    }

    private string decodeConfig( string ncData ) {
      if (string.IsNullOrEmpty( ncData )) return "Server.cfg seems empty";

      mRecord.ClearParam();

      string[] ncLines = ncData.Split( new char[] { '\n' } );

      for (int count = 0, nl = ncLines.Length; count < nl; count++) {
        string line = ncLines[count]; // line is already trimmed by loadNotecard
        if (string.IsNullOrEmpty( line ) || line.StartsWith( "//" )) continue;

        int pos = line.IndexOf( "=" );
        if (pos < 4) return "Error at notecard line " + (count + 1).ToString();

        string cmd = line.Substring( 0, pos ).Trim().ToLower();
        string val = line.Substring( pos + 1 ).Trim();

        bool ok = false;
        if (!ok) ok = decodeLocalParam( cmd, val );
        if (!ok) ok = mRezzer.DecodeParam( cmd, val );
        if (!ok) ok = mZombier.DecodeParam( cmd, val );
        if (!ok) ok = mPlayers.DecodeParam( cmd, val );
        if (!ok) ok = mRecord.DecodeParam( cmd, val );
        if (!ok) return "Value error at notecard line " + (count + 1).ToString();
      }
      return "";
    }

    private bool decodeLocalParam( string cmd, string val ) {
      bool ok = true;

      Vector3 vtmp;      
      if (cmd == "safe_area_corner1" && Vector3.TryParse( val, out vtmp )) mSafeArea[0] = vtmp;
      else if (cmd == "safe_area_corner2" && Vector3.TryParse( val, out vtmp )) mSafeArea[1] = vtmp;
      else if (cmd == "pos_hall" && Vector3.TryParse( val, out vtmp )) mPosHall = vtmp;
      else if (cmd == "pos_arena" && Vector3.TryParse( val, out vtmp )) mPosArena = vtmp;
      else if (cmd == "debug") mDebug = val.ToLower() == "on";
      else if (cmd == "hud_attach_to") {
        switch (val.ToLower()) {
          case "top-left": mHudAttachPoint = 34; break;
          case "top-center": mHudAttachPoint = 33; break;
          case "top-right": mHudAttachPoint = 32; break;          
          case "center": mHudAttachPoint = 31; break;          
          case "bottom-left": mHudAttachPoint = 36; break;
          case "bottom-center": mHudAttachPoint = 37; break;
          case "bottom-right": mHudAttachPoint = 38; break;
          default: break;
        }
      }
      else ok = false;

      return ok;
    }

    private void MainCycle( string param ) {

      // check for intruders
      mScene.ForEachRootScenePresence( delegate ( ScenePresence sp ) {        
        bool isin = IsInArena( sp.AbsolutePosition );

        if (mNpcMod.IsNPC( sp.UUID, mScene )) {
          if (!isin) mZombier.Kill( sp.UUID );
          return;
        }

        bool isplayer = Players.IsPlayer( sp.UUID.ToString() );         

        if (sp.IsInTransit || sp.IsDeleted || sp.IsChildAgent || !isin) {
          if (isplayer) Players.HardRemove( sp.UUID.ToString() );
          return;
        }

        if (!isplayer) {
          RegionTP( sp.UUID, mPosHall );          
          return;
        }

        // verify if it do something against fly
        sp.Flying = false;
      } );

      // maybe we need to verify if a player is still in region to remove
      // the ones who tp away (but before better test if the previus cycle already 
      // cleanup them )
      
      bool enab = mPlayers.Count > 0;
      mPlayers.Enabled( enab );
      mRezzer.Enabled( enab );
      mZombier.Enabled( enab );
    }

    #endregion Helpers
  }
}