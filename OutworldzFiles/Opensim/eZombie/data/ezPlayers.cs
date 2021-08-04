using System;
using System.Collections.Generic;
using OpenMetaverse;
using OpenSim.Region.Framework.Scenes;

namespace eZombies {

  internal class ezAgPos {
    public string Id = null;
    public Vector3 Pos = Vector3.Zero;    

    public ezAgPos() {}
    public ezAgPos( string id, Vector3 pos ) { Id=id; Pos = pos; }    
  }
  
  internal class ezPlayers : IDisposable {

    private class cPlayer {

      private string mAviID;
      private string mGunID;
      private string mHudID;
      private string mName;
      private DateTime mStartTime;
      private int mLife;
      private int mPotion;
      private DateTime mEnergyTime;
      private int mKills;
      private int mScore;
      private int mTopScore;
      private bool mMsgSent;

      public string AviID { get { return mAviID; } set { mAviID = value; } }
      public string GunID { get { return mGunID; } set { mGunID = value; } }
      public string HudID { get { return mHudID; } set { mHudID = value; } }
      public string Name { get { return mName; } /*set { mName = value; }*/ }
      public TimeSpan LifeTime { get { return (DateTime.Now - mStartTime); } }
      public int Life { get { return mLife; } set { mLife = value < 0 ? 0 : value > 100 ? 100 : value; } }
      public int Potion { get { return mPotion; } set { mPotion = value < 0 ? 0 : value > 4 ? 4 : value; mEnergyTime = DateTime.Now; } }
      public float Energy { get { return mPotion / 2f; } }
      public double EnergyTime { get { return (DateTime.Now - mEnergyTime).TotalSeconds; } }
      public int Kills { get { return mKills; } set { mKills = value; } }
      public int Score { get { return mScore; } set { mScore = value; } }
      public int TopScore { get { return mTopScore; } set { mTopScore = value; } }
      public bool MsgSent { get { return mMsgSent; } set { mMsgSent = value; } }
      
      public cPlayer( string pid, string name, string gid, string hid ) {
        mAviID = pid;
        mGunID = gid;
        mHudID = hid;
        mName = name;        
        mStartTime = DateTime.Now;
        mLife = 100;
        mPotion = 0;
        mEnergyTime = mStartTime;
        mKills = 0;
        mScore = 0;        
        mMsgSent = false;
        mTopScore = 0;
      }

      public void ResetEnergyTime() {
        mEnergyTime = DateTime.Now;
      }      
    }
    
    private Dictionary<string, cPlayer> mPlayers = null;   // List of active players in game

    // --- Player params
    private int mMaxPlayers = 10;         // Max number of players
    private double mEnergyDecTm = 90;     // Time for decreasing energy
    private int mHealQty = 30;            // Life qty for heal box
    private double mBombDist = 30;        // Bomb killing area
    private double mGunDist = 45;         // Gun shot distance
    // --- Score params
    private int mScoreKill = 30;          // Score for each zombie killed
    private int mScorePotion = 5;         // Score for each potion found
    private int mScoreBomb = 0;           // Score for each bomb found
    private int mScorePrize1 = 75;        // Score for each prize level 1 found
    private int mScorePrize2 = 100;       // Score for each prize level 2 found
    private int mScorePrize3 = 150;       // Score for each prize level 3 found

    private bool mEnabled = false;
    private eZombie mGame;

    public int Count { get { return mPlayers.Count; } }
    public int MaxPlayers { get { return mMaxPlayers; } }

    public ezPlayers( eZombie game ) {
      mGame = game;

      mPlayers = new Dictionary<string, cPlayer>();     
    }

    public void Enabled( bool sts ) {
      if (sts == mEnabled) return;

      mEnabled = sts;
      if (mEnabled) {
        mGame.Tmr.TCreate( "PlEnergy", 1, true, EnergyCycle );
        mGame.Tmr.TCreate( "PlBring", 0.2, true, BringCycle );        
      }
      else {
        mGame.Tmr.TRemove( "PlEnergy" );
        mGame.Tmr.TRemove( "PlBring" );        
      }
    }

    public bool DecodeParam( string cmd, string val ) {
      bool ok = true;

      int itmp;
      double dtmp;
      
      // --- Player params
      if (cmd == "max_players" && int.TryParse( val, out itmp )) mMaxPlayers = itmp;
      else if (cmd == "energy_decay_tm" && double.TryParse( val, out dtmp )) mEnergyDecTm = dtmp;
      else if (cmd == "heal_qty" && int.TryParse( val, out itmp )) mHealQty = itmp;
      else if (cmd == "bomb_radius" && double.TryParse( val, out dtmp )) mBombDist = dtmp;
      // --- Score params
      else if (cmd == "score_kill" && int.TryParse( val, out itmp )) mScoreKill = itmp;
      else if (cmd == "score_potion" && int.TryParse( val, out itmp )) mScorePotion = itmp;
      else if (cmd == "score_bomb" && int.TryParse( val, out itmp )) mScoreBomb = itmp;
      else if (cmd == "score_prize1" && int.TryParse( val, out itmp )) mScorePrize1 = itmp;
      else if (cmd == "score_prize2" && int.TryParse( val, out itmp )) mScorePrize2 = itmp;
      else if (cmd == "score_prize3" && int.TryParse( val, out itmp )) mScorePrize3 = itmp;
      else ok = false;

      return ok;
    }    

    public string[] ValidateParam() {
      List<string> ret = new List<string>();

      if (mMaxPlayers < 1) ret.Add("Invalid Max_Players");
      if (mEnergyDecTm < 10 || mEnergyDecTm > 180) ret.Add("Invalid Energy_Decay_TM");
      if (mHealQty < 1 || mHealQty > 100) ret.Add("Invalid Heal_Qty");
      if (mBombDist < 1 || mBombDist > 255) ret.Add("Invalid Bomb_radius");            
      if (mScoreKill < 0 || mScoreKill > 9999) ret.Add("Invalid Score_Kill");
      if (mScorePotion < 0 || mScorePotion > 9999) ret.Add("Invalid Score_Potion");
      if (mScoreBomb < 0 || mScoreBomb > 999) ret.Add("Invalid Score_Bomb");
      if (mScorePrize1 < 0 || mScorePrize1 > 9999) ret.Add("Invalid Score_Prize1");
      if (mScorePrize2 < 0 || mScorePrize2 > 9999) ret.Add("Invalid Score_Prize2");
      if (mScorePrize3 < 0 || mScorePrize3 > 9999) ret.Add("Invalid Score_Prize3");     

      return ret.ToArray();
    }

    public string[] ListParam() {
      List<string> ret = new List<string>();

      ret.Add( string.Format( "max_players: {0}", mMaxPlayers ) );
      ret.Add( string.Format( "energy_decay_tm: {0}", mEnergyDecTm ) );
      ret.Add( string.Format( "heal_qty: {0}", mHealQty ) );
      ret.Add( string.Format( "bomb_radius: {0}", mBombDist ) );

      ret.Add( string.Format( "score_kill: {0}", mScoreKill ) );
      ret.Add( string.Format( "score_potion: {0}", mScorePotion ) );
      ret.Add( string.Format( "score_bomb: {0}", mScoreBomb ) );
      ret.Add( string.Format( "score_prize: {0}, {1}, {2}", mScorePrize1, mScorePrize2, mScorePrize3 ) );

      return ret.ToArray();
    }

    public bool IsPlayer( UUID id ) {
      return mPlayers.ContainsKey( id.ToString() );
    }

    public bool IsPlayer( string id ) {
      return mPlayers.ContainsKey( id );
    }

    /*
    public void Add( UUID pid, UUID gid, UUID hid ) {
      string spid = pid.ToString();

      if (mPlayers.ContainsKey( spid )) return;
      
      mPlayers.Add( pid.ToString(), new cPlayer( spid, mGame.GetAvatarName(pid), gid.ToString(), hid.ToString() ) );
      mPlayers[spid].TopScore = mGame.Records.GetPlayerRecord( spid );

      mGame.SetRunSpeed( pid, 1 );
      //llForceMouselook

      mGame.Tmr.TCreate( "UpdOnGame", 5, true, UpdateOnGame, true );
    }*/
       
    public void Gunshot( string gid ) {

      string pid = null;
      foreach (cPlayer pl in mPlayers.Values) {
        if (pl.GunID == gid) { pid = pl.AviID; break; }
      }
      if (pid == null) return;

      UUID uid = new UUID( pid );
      ScenePresence sp = mGame.GetScenePresence( uid );
      if (sp == null) return; // maybe it's gone ?? of course not, but better check

      Vector3 cpos = sp.CameraPosition;
      Quaternion crot = sp.CameraRotation;
      Vector3 scorr = new Vector3( 0.5f, 0f, 0f ) * crot;
      Vector3 ecorr = new Vector3( (float)mGunDist, 0f, 0f ) * crot;
      List<UUID> hits = mGame.CastRayV3( cpos + scorr, cpos + ecorr, 2 );
      if (hits.Count < 1) return;
      string target = hits[0].ToString();
      if (target == pid && hits.Count > 1) target = hits[1].ToString();
      if (mGame.Zombier.IsZombieAlive( target )) {        
        mGame.Zombier.Kill( new UUID(target) );        
        mPlayers[pid].Kills++;
        mPlayers[pid].Score += mScoreKill;        
      }      
    }

    public void SetLife( string pid, int qta ) {
      if (!mPlayers.ContainsKey( pid )) return;

      cPlayer pl = mPlayers[pid];
      pl.Life += qta;

      if (pl.Life <= 0) {
        mGame.Records.SetPlayer( pl.AviID, pl.Name, pl.LifeTime, pl.Kills, pl.Score );
        mGame.ObjectDettach( new UUID( pl.AviID ), new UUID( pl.GunID ) );
        mGame.ObjectDettach( new UUID( pl.AviID ), new UUID( pl.HudID ) );
        mGame.SetWalk( new UUID(pl.AviID) );
        mPlayers.Remove( pid );
        mGame.RegionTP( pl.AviID, mGame.PosHall );        

        mGame.Tmr.TCreate( "UpdOnGame", 5, true, UpdateOnGame, true );
      }
    }

    public void HardRemove( string pid ) {
      cPlayer pl;
      if (mPlayers.TryGetValue(pid,out pl)) {
        //mGame.Records.SetPlayer( pl.AviID, pl.Name, pl.LifeTime, pl.Kills, pl.Score );
        mGame.ObjectDettach( new UUID( pl.AviID ), new UUID( pl.GunID ) );
        mGame.ObjectDettach( new UUID( pl.AviID ), new UUID( pl.HudID ) );
        mPlayers.Remove( pid );
      }      
    }

    public ezAgPos[] GetPlayersPos() {
      List<ezAgPos> ret = new List<ezAgPos>();
      foreach (cPlayer pl in mPlayers.Values) {
        Vector3 pos = mGame.GetAgentPos( new UUID( pl.AviID ) );
        if (pos != Vector3.Zero) ret.Add( new ezAgPos( pl.AviID, pos ) );
      }
      return ret.ToArray();
    }

    public object[] GetPlayers() {
      List<object> names = new List<object>();
      foreach (cPlayer pl in mPlayers.Values) names.Add( pl.Name );
      return names.ToArray();
    }

    public void IncomingAvi( UUID pid, string gname ) {
      string spid = pid.ToString();

      if (!mGame.IsActive || mPlayers.ContainsKey( spid )) return;
      
      UUID gid = mGame.ObjectAttachTemp( pid, gname, 6 ); // RHAND
      if (gid == UUID.Zero) return;
      
      UUID hid = mGame.ObjectAttachTemp( pid, "eZ_HUD", mGame.HudAttachPoint );        
      if (hid == UUID.Zero) {
        mGame.ObjectDettach( pid, gid );
        return;
      }
               
      mPlayers.Add( spid, new cPlayer( spid, mGame.GetAvatarName( pid ), gid.ToString(), hid.ToString() ) );      
      mPlayers[spid].TopScore = mGame.Records.GetPlayerRecord( spid );      
      mGame.SetRunSpeed( pid, 1 );
      mGame.Tmr.TCreate( "UpdOnGame", 5, true, UpdateOnGame, true );      
      mGame.RegionTP( pid, mGame.PosArena );

      //ScenePresence sp = mGame.GetScenePresence( pid );
      //llForceMouselook
      //lock tp
    }

    public void Dispose() {
      //Enabled( false );
      mPlayers.Clear();      
    }

    private void EnergyCycle( string param ) {
      if (mPlayers.Count < 1) return;

      DateTime tm = DateTime.Now;
      foreach( cPlayer pl in mPlayers.Values) {
        double et = pl.EnergyTime;

        if (pl.Potion > 0 && et >= mEnergyDecTm) {
          pl.Potion--;          
          mGame.SetRunSpeed( new UUID(pl.AviID), 1 + (pl.Potion / 2) );
        }

        // Life,LifeTime,Potion,Energy,Kills,Score     
        int er = pl.Potion > 0 ? (int)(mEnergyDecTm - et) : 0;
        string sts = string.Format( "{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}",
          pl.Life,
          pl.LifeTime.ToString( @"h\:mm\:ss" ),
          pl.Potion,
          er < 0 ? 0 : er,
          pl.Kills,
          pl.Score,
          pl.Score > pl.TopScore ? 1 : 0,
          pl.Score > mGame.Records.TopScore ? 1 : 0
        );
                
        mGame.ObjectMessage( new UUID( pl.HudID ), "ezUPDATE|" + sts );
      }      
    }

    private void BringCycle( string param ) {
      if (mPlayers.Count < 1) return;

      List<cPlayer> torem = new List<cPlayer>();
            
      foreach (cPlayer pl in mPlayers.Values) {
        Vector3 pos = mGame.GetAgentPos( new UUID( pl.AviID ) );
        // this will cleanup from avatar no more in parcel
        if (pos == Vector3.Zero) { torem.Add( pl ); continue; }

        List<ezRezzer.eType> lst = mGame.Rezzer.BringObjects( pos );
        foreach (ezRezzer.eType tp in lst) {
          if (tp == ezRezzer.eType.HealBox)
            pl.Life += mHealQty;
          else if (tp == ezRezzer.eType.Potion) {
            pl.Potion++;
            pl.Score += mScorePotion;
            mGame.SetRunSpeed( new UUID( pl.AviID ), 1 + (pl.Potion / 2) );
          }
          else if (tp == ezRezzer.eType.Bomb) {
            int kill = mGame.Zombier.Bomb( pos, mBombDist );
            pl.Kills += kill;
            pl.Score += mScoreBomb + mScoreKill * kill;
          }
          else if (tp == ezRezzer.eType.Prize1) pl.Score += mScorePrize1;
          else if (tp == ezRezzer.eType.Prize2) pl.Score += mScorePrize2;
          else if (tp == ezRezzer.eType.Prize3) pl.Score += mScorePrize3;
        }
      }

      foreach (cPlayer pl in torem) {
        UUID uid = new UUID( pl.AviID );
        mGame.Records.SetPlayer( pl.AviID, pl.Name, pl.LifeTime, pl.Kills, pl.Score );
        mGame.ObjectDettach( uid, new UUID( pl.GunID ) );
        mGame.ObjectDettach( uid, new UUID( pl.HudID ) );
        mPlayers.Remove( pl.AviID );
      }
    }
    
    private void UpdateOnGame( string param ) {
      mGame.HostMessage( "ezONGAME|"+ mPlayers.Count.ToString() );
    }
  }
}

