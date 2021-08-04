using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using OpenMetaverse;
using OpenSim.Framework;
using OpenSim.Region.Framework.Interfaces;
using OpenSim.Region.Framework.Scenes;
using OpenSim.Region.ScriptEngine.Shared.Api;
using OpenSim.Region.ScriptEngine.Shared;
using OpenSim.Region.ScriptEngine.Interfaces;

namespace eZombies {

  public partial class eZombie {

    public string GetAvatarName( UUID id ) {
      ScenePresence sp = mScene.GetScenePresence( id );
      if (sp == null) return "User Uknown";
      return sp.Firstname + " " + sp.Lastname;
    }

    public string GetRegionName(bool clean = false) {
      string nm = mScene.RegionInfo.RegionName;
      if (!clean) return nm;

      string mask = "0123456789qwertyuioplkjhgfdsazxcvbnm_-";
      string ret = "";
      for(int i = 0, l = nm.Length; i < l; i++) {
        string c = nm.Substring( i, 1 );
        ret += mask.IndexOf( c.ToLower() ) < 0 ? "_" : c;
      }
      return ret;
    }

    public string GetRegionID() {
      return mScene.RegionInfo.RegionID.ToString();
    }

    public bool IsInSafe( Vector3 pos ) {      
      // There is a cooler math way to evalutate is a post belong to a cube.
      // This way less cool but faster
      float a = Math.Min( mSafeArea[0].Z, mSafeArea[1].Z );
      float b = Math.Max( mSafeArea[0].Z, mSafeArea[1].Z );
      if (pos.Z < a || pos.Z > b) return false;

      a = Math.Min( mSafeArea[0].X, mSafeArea[1].X );
      b = Math.Max( mSafeArea[0].X, mSafeArea[1].X );
      if (pos.X < a || pos.X > b) return false;
            
      a = Math.Min( mSafeArea[0].Y, mSafeArea[1].Y );
      b = Math.Max( mSafeArea[0].Y, mSafeArea[1].Y );
      return pos.Y < a || pos.Y > b ? false : true;      
    }

    public bool IsInArena( Vector3 pos ) {
      return !IsInSafe( pos );
    }
    
    public ScenePresence GetScenePresence( UUID uid ) {
      return mScene.GetScenePresence( uid );
    }

    public Vector3 GetAgentPos( UUID aid ) {
      ScenePresence sp = mScene.GetScenePresence( aid );
      if (sp == null) return Vector3.Zero;
      if (!IsInArena( sp.AbsolutePosition )) return Vector3.Zero;
      return sp.AbsolutePosition;
    }

    public double GetAgentDistFrom( UUID aid, Vector3 pos ) {
      ScenePresence sp = mScene.GetScenePresence( aid );
      return sp == null ? 999999 : VecDist( sp.AbsolutePosition, pos );
    }

    public UUID ObjectRez( string name, Vector3 pos ) {
      UUID sogid = UUID.Zero;

      try {
        TaskInventoryItem item = mHost.Inventory.GetInventoryItem( name );
        if (item != null) sogid = ObjectRez( item, pos );
      }
      catch (Exception e) { WriteLogErr( "ObjectRez: " + name, e.Message ); }      

      return sogid;
    }

    public UUID ObjectRez( TaskInventoryItem item, Vector3 pos, int scriptparam = 0 ) {
      if (null == item) return UUID.Zero;

      List<SceneObjectGroup> objlist;
      List<Vector3> veclist;
      Vector3 bbox;
      float offsetHeight;

      bool success = mHost.Inventory.GetRezReadySceneObjects( item, out objlist, out veclist, out bbox, out offsetHeight );
      if (!success) return UUID.Zero;

      // we rez only single items (multi prims) not grouped prims
      if (objlist.Count != 1) return UUID.Zero;

      SceneObjectGroup sog = objlist[0];
      sog.RezzerID = mHost.UUID;

      if (sog.PrimCount > 1)
        pos -= sog.GetGeometricCenter() * sog.RootPart.GetWorldRotation();

      Vector3 curpos = pos + veclist[0];

      if (sog.IsAttachment == false && sog.RootPart.Shape.State != 0) {
        sog.RootPart.AttachedPos = sog.AbsolutePosition;
        sog.RootPart.Shape.LastAttachPoint = (byte)sog.AttachmentPoint;
      }

      mScene.AddNewSceneObject( sog, true, curpos, null, Vector3.Zero );

      // We can only call this after adding the scene object, since the scene object references the scene
      // to find out if scripts should be activated at all.
      sog.CreateScriptInstances( scriptparam, true, mScene.DefaultScriptEngine, 3 );
      sog.ScheduleGroupForFullUpdate();

      //mLog.DebugFormat( "[eZombie] Rezzing {0} made of {1} parts", item.Name, sog.PrimCount );

      return sog.RootPart.UUID;
    }

    public void ObjectRemove( string key ) {
      UUID oid;
      if (UUID.TryParse( key, out oid )) ObjectRemove( oid );
    }

    public void ObjectRemove( UUID oid ) {
      SceneObjectGroup sog = null;
      try { sog = mScene.GetSceneObjectGroup( oid ); }
      catch (Exception e) { WriteLogErr( "ObjectRemove: " + oid.ToString(), e.Message ); }

      if (sog == null || sog.IsDeleted || sog.IsAttachment) return;
      mScene.DeleteSceneObject( sog, false );
    }

    public UUID NpcRez( string name1, string name2, Vector3 pos, AvatarAppearance ap ) {
      UUID id = mNpcMod.CreateNPC( name1, name2, pos, UUID.Random(), UUID.Zero, string.Empty, UUID.Zero, false, mScene, ap );
      if (id != UUID.Zero) {
        ScenePresence sp;
        if (mScene.TryGetScenePresence( id, out sp )) sp.SendAvatarDataToAllAgents();
      }
      return id;
    }

    public bool IsNpc( UUID nid ) {      
      return mNpcMod.IsNPC( nid, mScene );
    }

    public void NpcRemove( UUID nid ) {
      if (mNpcMod.IsNPC( nid, mScene )) {
        try { mNpcMod.DeleteNPC( nid, mScene ); }
        catch { }
      }
    }

    public void NpcRemoveAll() {
      List<UUID> torem = new List<UUID>();
      mScene.ForEachRootScenePresence( delegate ( ScenePresence sp ) {
        if (sp.IsDeleted || sp.IsChildAgent) return;

        // better we remove all npc not only the one is arena,
        // this way we will kill fugitives too
        //if (!IsInArena( sp.AbsolutePosition )) return;

        if (mNpcMod.IsNPC( sp.UUID, mScene )) torem.Add( sp.UUID );
      } );
      foreach (UUID id in torem) {      
        try { mNpcMod.DeleteNPC( id, mScene ); }
        catch { }
      }
    }

    public void NpcStop( UUID nid ) {      
      mNpcMod.StopMoveToTarget( nid, mScene );      
    }

    public void NpcWalkTo( UUID nid, Vector3 pos ) {
      mNpcMod.StopMoveToTarget( nid, mScene );
      mNpcMod.MoveToTarget( nid, mScene, pos, true, false, false );
    }

    public void SetSpeed( string id, float speed ) {
      SetSpeed( new UUID( id ), speed );
    }

    public void SetSpeed( UUID id, float speed ) {
      ScenePresence av = mScene.GetScenePresence( id );
      if (av != null) av.SpeedModifier = speed;
    }

    public void SetRun( UUID id ) {
      ScenePresence av = mScene.GetScenePresence( id );
      if (av != null) av.SetAlwaysRun = true;
    }

    public void SetWalk( UUID id ) {
      ScenePresence av = mScene.GetScenePresence( id );
      if (av != null) { av.SetAlwaysRun = false; av.SpeedModifier = 1; }
    }

    public void SetRunSpeed( UUID id, float speed ) {
      ScenePresence av = mScene.GetScenePresence( id );
      if (av != null) { av.SetAlwaysRun = true; av.SpeedModifier = speed; }
    }

    private string LoadNotecard( UUID ncId ) {
      if (!NotecardCache.IsCached( ncId )) {
        AssetBase a = mScene.AssetService.Get( ncId.ToString() );
        if (a == null) return null;
        NotecardCache.Cache( ncId, a.Data );
      };

      StringBuilder ncData = new StringBuilder();
      for (int count = 0; count < NotecardCache.GetLines( ncId ); count++)
        ncData.Append( NotecardCache.GetLine( ncId, count ).Trim() + "\n" );

      return ncData.ToString();
    }

    public double VecDist( Vector3 a, Vector3 b ) {
      double dx = a.X - b.X;
      double dy = a.Y - b.Y;
      double dz = a.Z - b.Z;
      return Math.Sqrt( dx * dx + dy * dy + dz * dz );
    }

    public void RegionTP( string aid, Vector3 dest ) {
      UUID id;
      if (UUID.TryParse( aid, out id )) RegionTP( id, dest );
    }

    public void RegionTP( UUID aid, Vector3 dest ) {      
      ScenePresence sp = mScene.GetScenePresence( aid );
      RegionTP( sp, dest );                  
    }

    public void RegionTP( ScenePresence sp, Vector3 dest ) {
      if (sp == null || sp.IsDeleted || sp.IsInTransit) return;
            
      sp.AbsolutePosition = dest;
      sp.CameraPosition = dest;
      sp.FlyDisabled = true;      
      sp.ControllingClient.SendLocalTeleport( dest, dest, (uint)TeleportFlags.ViaLocation );
    }
        
    public UUID ObjectAttachTemp( UUID pid, string itemName, int attachmentPoint ) {
      ScenePresence sp = null;
      SceneObjectPart op = null;

      try { sp = mScene.GetScenePresence( pid ); }
      catch (Exception e) { WriteLogErr( "ObjectAttachTemp: " + itemName, e.Message ); }      
      if (sp == null || sp.IsDeleted || sp.IsInTransit) return UUID.Zero;

      UUID newID = ObjectRez( itemName, sp.AbsolutePosition );
      if (newID == UUID.Zero) return UUID.Zero;

      try { op = mScene.GetSceneObjectPart( newID ); }
      catch (Exception e) { WriteLogErr( "ObjectAttachTemp: " + itemName + " - " + newID.ToString(), e.Message ); }      
      SceneObjectGroup og = op.ParentGroup;
            
      if (sp.UUID != og.OwnerID) {
        try {
          og.SetOwner( sp.UUID, sp.ControllingClient.ActiveGroupId );

          if (mScene.Permissions.PropagatePermissions()) {
            foreach (SceneObjectPart child in og.Parts) {
              child.Inventory.ChangeInventoryOwner( sp.UUID );
              child.TriggerScriptChangedEvent( Changed.OWNER );
              child.ApplyNextOwnerPermissions();
            }
          }

          og.RootPart.ObjectSaleType = 0;
          og.RootPart.SalePrice = 10;

          og.HasGroupChanged = true;
          og.RootPart.SendPropertiesToClient( sp.ControllingClient );
          og.RootPart.ScheduleFullUpdate();
        }
        catch (Exception e) { WriteLogErr( "ObjectAttachTemp: " + itemName, e.Message ); }        
      }

      bool ok = false;
      try {
        ok = mScene.AttachmentsModule.AttachObject( sp, op.ParentGroup, (uint)attachmentPoint, false, false, true );
      }
      catch (Exception e) { WriteLogErr( "ObjectAttachTemp: " + itemName, e.Message ); }

      if (!ok) {
        ObjectRemove( newID );
        return UUID.Zero;
      }

      return newID;
    }

    public void ObjectDettach( UUID pid, UUID oid ) {

      ScenePresence sp = mScene.GetScenePresence( pid );
      if (sp == null || sp.IsDeleted || sp.IsInTransit) return;

      SceneObjectPart op = mScene.GetSceneObjectPart( oid );
      if (op == null) return;

      mScene.AttachmentsModule.DetachSingleAttachmentToInv( sp, op.ParentGroup );
    }
    
    public void ObjectMessage( UUID oid, string message ) {
      SceneObjectPart sop = mScene.GetSceneObjectPart( oid );
      if (sop == null) return;

      object[] resobj = new object[] { new LSL_Types.LSLString( mHost.UUID.ToString() ), new LSL_Types.LSLString( message ) };

      IScriptModule[] engines = mScene.RequestModuleInterfaces<IScriptModule>();
      foreach (IScriptModule engine in engines)
        ((IScriptEngine)engine).PostObjectEvent( sop.LocalId, new EventParams( "dataserver", resobj, new DetectParams[0] ) );
    }

    public void HostMessage( string message ) {
      
      object[] resobj = new object[] { new LSL_Types.LSLString( UUID.Zero.ToString() ), new LSL_Types.LSLString( message ) };

      IScriptModule[] engines = mScene.RequestModuleInterfaces<IScriptModule>();
      foreach (IScriptModule engine in engines)
        ((IScriptEngine)engine).PostObjectEvent( mHost.LocalId, new EventParams( "dataserver", resobj, new DetectParams[0] ) );
    }

    public void AvPlayAnim( UUID pid, string animation ) {

      ScenePresence sp = mScene.GetScenePresence( pid );
      if (sp == null) return;
      
      UUID animID = UUID.Zero;
      mHost.TaskInventory.LockItemsForRead( true );
      foreach (KeyValuePair<UUID, TaskInventoryItem> inv in mHost.TaskInventory) {
        if (inv.Value.Type == (int)AssetType.Animation) {
          if (inv.Value.Name == animation) {
            animID = inv.Value.AssetID;
            break;
          }
        }
      }
      mHost.TaskInventory.LockItemsForRead( false );

      if (animID == UUID.Zero)
        sp.Animator.AddAnimation( animation, mHost.UUID );
      else
        sp.Animator.AddAnimation( animID, mHost.UUID );
    }

    public void PlaySound( UUID sndId, Vector3 position, float radius = 60 ) {      
      SceneObjectPart sp = mScene.GetSceneObjectPart( mHost.UUID );      
      ulong regionHandle = mScene.RegionInfo.RegionHandle;

      if (radius < 1 || radius > 60) radius = 60;

      mScene.ForEachRootScenePresence( delegate ( ScenePresence isp ) {
        if (mNpcMod.IsNPC( isp.UUID, mScene )) return;
        
        if (VecDist( isp.AbsolutePosition, position ) <= radius) 
          isp.ControllingClient.SendTriggeredSound( sndId, sp.OwnerID, sp.UUID, sp.ParentGroup.UUID, regionHandle, position, 1 );
      } );
    }

    internal bool IsObjectAllowed( UUID oid ) {
      SceneObjectPart sop = null;
      try { sop = mScene.GetSceneObjectPart( oid ); }
      catch (Exception e) { WriteLogErr( "IsObjectAllowed: " + oid.ToString(), e.Message ); }
      if (sop == null) return false;

      try {
        // Regionowners may use the function      
        if (mScene.RegionInfo.EstateSettings.EstateOwner == sop.OwnerID) return true;

        // Estate Managers may use the function
        if (mScene.RegionInfo.EstateSettings.IsEstateManagerOrOwner( sop.OwnerID )) return true;

        // Grid gods may use the function
        if (mScene.Permissions.IsGridGod( sop.OwnerID )) return true;

        // Parcel Owner can use this function      
        ILandObject land = mScene.LandChannel.GetLandObject( sop.AbsolutePosition );
        if (land.LandData.OwnerID == sop.OwnerID) return true;
      }
      catch (Exception e) { WriteLogErr( "IsObjectAllowed (2): " + oid.ToString(), e.Message ); }
      return false;
    }

    internal void Whisper( SceneObjectPart sopfrom, int channelID, string text ) {

      if (text.Length > 1023) text = text.Substring( 0, 1023 );

      OSChatMessage args = new OSChatMessage();
      args.Message = Utils.BytesToString( Utils.StringToBytes( text ) ); // ??? i wonder why
      args.Channel = 0;
      args.Type = ChatTypeEnum.Whisper;
      args.Position = sopfrom.AbsolutePosition;
      args.SenderUUID = sopfrom.UUID;
      args.SenderObject = sopfrom;
      args.Scene = mScene;
      //args.Destination = toId;
      args.From = sopfrom.Name;

      mScene.EventManager.TriggerOnChatBroadcast( mScene, args );
    }

    internal void Say( SceneObjectPart sopfrom, int channelID, string text ) {

      if (text.Length > 1023) text = text.Substring( 0, 1023 );

      OSChatMessage args = new OSChatMessage();
      args.Message = Utils.BytesToString( Utils.StringToBytes( text ) ); // ??? i wonder why
      args.Channel = 0;
      args.Type = ChatTypeEnum.Say;
      args.Position = sopfrom.AbsolutePosition;
      args.SenderUUID = sopfrom.UUID;
      args.SenderObject = sopfrom;
      args.Scene = mScene;
      //args.Destination = toId;
      args.From = sopfrom.Name;

      mScene.EventManager.TriggerOnChatBroadcast( mScene, args );
    }

    internal void WriteLogErr( string fnc, string txt ) {
      string msg = ">>> "+ fnc + Environment.NewLine + txt;
      File.AppendAllText( mModPath + "errlog.txt", msg );
    }
  }
}
