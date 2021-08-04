using System;
using System.Collections.Generic;
using OpenMetaverse;
using OpenMetaverse.StructuredData;
using OpenSim.Framework;

namespace eZombies {

  internal class ezZombier : IDisposable {

    private class cNpc {      
      private string[] mNames;      
      public string[] Names { get { return mNames; } }
      public AvatarAppearance Appearance { get; set; }

      public cNpc( string nc, AvatarAppearance ava ) {                
        Appearance = ava;
        mNames = new string[] {
          nc.StartsWith( "npc." ) ? nc.Substring( 4 ) : nc,
          "eZ"
        };
      }
    }

    private class cZombie {
      private UUID mId;             // Zombie UID once rezzed
      private bool mIsAlive;        // true=alive false=died            
      private DateTime mDieTime;    // when started die animation
      private DateTime mSndTime;    // Last Roar from this zombie

      public UUID Id { get { return mId; } }      
      public bool IsAlive { get { return mIsAlive; } }
      public double DieTime { get { return (DateTime.Now - mDieTime).TotalSeconds; } }
      public double SndTime { get { return (DateTime.Now - mSndTime).TotalSeconds; } }

      public cZombie( UUID zid ) {
        mId = zid;        
        mIsAlive = true;
        mDieTime = mSndTime = DateTime.Now;        
      }

      public void Kill() {
        if (mIsAlive) {
          mIsAlive = false;
          mDieTime = DateTime.Now;
        }
      }    
      
      public void Roar() {
        mSndTime = DateTime.Now;
      }  
    }
    
    private List<cNpc> mNpcs = null;             // List of Npc usable for Zombie
    private List<Vector3> mRezPos = null;        // List of positions where rez zombies        
    private Dictionary<string,cZombie> mRezzed = null;   // List of Zombie active in game
    private List<string> mDieAnim = null;
    private List<UUID> mSnds = null;

    private double mSniffDist = 90;              // Max distance a zombie can "smell" a player
    private double mScratchDist = 1.5;           // Scratch distance (usually under 1.2 mt)
    private double mBiteDist = 0.8;              // Bite distance (usually under 0.8 mt)
    private int mScratchLife = 10;               // Scratch life removed
    private int mBiteLife = 25;                  // Bite life removed
    private double mZombieSpeedTM = 180;         // after this time new Zombie start to run faster (usually 180 secs)
    private double mZombieEmitTM = 6;            // after this time new Zombie will be emitted (usually 6 secs)
    private double mZombieDieTM = 4;             // after this time died Zombie desappear (usually 4 secs)
    private int mMaxZombies = 50;                // Max number of Zombies on game (usually 50)
    private double mZombieRoarTM = 10;            // after this time a zombie roar (emit sound)
    private double mZombieRoarDist = 30;          // Distance to roar

    private bool mEnabled = false;
    private double mInterval = 6;    
    private eZombie mGame;

    private double mSpeed = 1;

    public int Count { get { return mRezzed.Count; } }

    public ezZombier( eZombie game ) {
      mGame = game;

      mNpcs = new List<cNpc>();
      mRezzed = new Dictionary<string, cZombie>();
      mRezPos = new List<Vector3>();
      mDieAnim = new List<string>();
      mSnds = new List<UUID>();

      mEnabled = false;      
    }

    public void Enabled( bool sts ) {
      if (sts == mEnabled) return;

      mEnabled = sts;
      if (mEnabled) {
        mSpeed = 1;
        mInterval = mZombieEmitTM;
                
        mGame.Tmr.TCreate( "ZombieRez", mInterval, true, RezCycle );        
        mGame.Tmr.TCreate( "ZombieMove", .5, true, MoveCycle );
        if (mZombieSpeedTM > 0) mGame.Tmr.TCreate( "ZombieSpeed", mZombieSpeedTM, true, SpeedCycle );
      }
      else {
        mGame.Tmr.TRemove( "ZombieRez" );        
        mGame.Tmr.TRemove( "ZombieSpeed" );
        mGame.Tmr.TRemove( "ZombieMove" );

        mGame.NpcRemoveAll();
        mRezzed.Clear();
      }
    }

    public bool DecodeParam( string cmd, string val ) {
      bool ok = true;

      int itmp;
      double dtmp;
      Vector3 vtmp;

      if (cmd == "zombie_pos" && Vector3.TryParse( val, out vtmp )) mRezPos.Add( vtmp );
      else if (cmd == "sniff_distance" && double.TryParse( val, out dtmp )) mSniffDist = dtmp;
      else if (cmd == "scratch_distance" && double.TryParse( val, out dtmp )) mScratchDist = dtmp;
      else if (cmd == "bite_distance" && double.TryParse( val, out dtmp )) mBiteDist = dtmp;
      else if (cmd == "scratch_life" && int.TryParse( val, out itmp )) mScratchLife = itmp;
      else if (cmd == "bite_life" && int.TryParse( val, out itmp )) mBiteLife = itmp;
      else if (cmd == "zombie_speed_tm" && double.TryParse( val, out dtmp )) mZombieSpeedTM = dtmp;
      else if (cmd == "zombie_rez_tm" && double.TryParse( val, out dtmp )) mZombieEmitTM = dtmp;
      else if (cmd == "zombie_die_tm" && double.TryParse( val, out dtmp )) mZombieDieTM = dtmp;
      else if (cmd == "max_zombies" && int.TryParse( val, out itmp )) mMaxZombies = itmp;
      else if (cmd == "zombie_roar_tm" && double.TryParse( val, out dtmp )) mZombieRoarTM = dtmp;
      else if (cmd == "zombie_roar_dist" && double.TryParse( val, out dtmp )) mZombieRoarDist = dtmp;
      else ok = false;

      return ok;
    }

    public string NpcAdd( string name, string data ) {
      string errd = "";

      AvatarAppearance app = null;
      try {
        OSDMap appearanceOsd = (OSDMap)OSDParser.DeserializeLLSDXml( data );
        app = new AvatarAppearance();
        app.Unpack( appearanceOsd );
      }
      catch (Exception e) {
        errd = e.Message;
      }
      if (!string.IsNullOrEmpty( errd )) return errd;

      if (app != null) mNpcs.Add( new cNpc( name, app ) );
      else errd = "Error decoding Npc";

      return errd;
    }

    public void NpcSndRoarAdd( UUID sid ) {
      mSnds.Add( sid );      
    }

    public void NpcAnimDieAdd( string name ) {
      mDieAnim.Add( name );
    }

    public void Kill( UUID zid ) {
      if (mRezzed.ContainsKey( zid.ToString() )) {
        mGame.NpcStop( zid );
        string ani = GetDieAnim();
        if (ani != null) mGame.AvPlayAnim( zid, ani );
        mRezzed[zid.ToString()].Kill();
      }
    }

    public int Bomb( Vector3 pos, double radius ) {
      int killed = 0;
      foreach (cZombie z in mRezzed.Values) {
        if (z.IsAlive && mGame.GetAgentDistFrom( z.Id, pos ) <= radius) {
          killed++;
          Kill( z.Id );          
        }
      }
      return killed;
    }

    public string[] ValidateParam() {
      List<string> ret = new List<string>();

      if (mRezPos.Count < 1) ret.Add( "No Zombie_Pos specified" );
      if (mSniffDist < 20 || mSniffDist > 200) ret.Add( "Invalid Sniff_Distance" );
      if (mScratchDist < 0.5 || mScratchDist > 1.5) ret.Add( "Invalid Scratch_distance" );
      if (mBiteDist < 0.2 || mBiteDist > 1) ret.Add( "Invalid Bite_distance" );
      if (mScratchLife < 1 || mScratchLife > 100) ret.Add( "Invalid Scratch_life" );
      if (mBiteLife < 1 || mBiteLife > 100) ret.Add( "Invalid Bite_life" );
      if (mZombieSpeedTM != 0 && (mZombieSpeedTM < 60 || mZombieSpeedTM > 600)) ret.Add( "Invalid Zombie_Speed_TM" );
      if (mZombieEmitTM < 2 || mZombieEmitTM > 30) ret.Add( "Invalid Zombie_Rez_TM" );
      if (mZombieDieTM < 0 || mZombieDieTM > 10) ret.Add( "Invalid Zombie_Die_TM" );
      if (mMaxZombies < 1 || mMaxZombies > 100) ret.Add( "Invalid Max_Zombies" );
      if (mNpcs.Count < 1) ret.Add( "Missing Npc cards" );
      if (mZombieRoarTM < 0 || mZombieRoarTM > 60) ret.Add( "Invalid Zombie_Roar_TM" );
      if (mZombieRoarDist < 0 || mZombieRoarDist > 128) ret.Add( "Invalid Zombie_Roar_Dist" );

      return ret.ToArray();
    }

    public string[] ListParam() {
      List<string> ret = new List<string>();

      ret.Add( string.Format( "zombie_pos: Nr. {0}", mRezPos.Count ) );
      ret.Add( string.Format( "sniff_distance: {0}", mSniffDist ) );
      ret.Add( string.Format( "scratch_distance: {0}", mScratchDist ) );
      ret.Add( string.Format( "bite_distance: {0}", mBiteDist ) );
      ret.Add( string.Format( "scratch_life: {0}", mScratchLife ) );
      ret.Add( string.Format( "bite_life: {0}", mBiteLife ) );
      ret.Add( string.Format( "zombie_speed_tm: {0}", mZombieSpeedTM ) );
      ret.Add( string.Format( "zombie_rez_tm: {0}", mZombieEmitTM ) );
      ret.Add( string.Format( "zombie_die_tm: {0}", mZombieDieTM ) );
      ret.Add( string.Format( "max_zombies: {0}", mMaxZombies ) );
      ret.Add( string.Format( "zombie_roar_tm: {0}", mZombieRoarTM ) );
      ret.Add( string.Format( "zombie_roar_dist: {0}", mZombieRoarDist ) );
      ret.Add( string.Format( "Zombie Roar Sounds loaded Nr.: {0}", mSnds.Count ) );
      foreach ( string s in mDieAnim ) ret.Add( string.Format( "zombies_anim: {0}", s ) );
      
      return ret.ToArray();
    }

    public bool IsZombie( UUID id ) {
      return mRezzed.ContainsKey( id.ToString() );
    }

    public bool IsZombie( string id ) {
      return mRezzed.ContainsKey( id );
    }

    public bool IsZombieAlive( string id ) {
      return mRezzed.ContainsKey( id ) && mRezzed[id].IsAlive;
    }
    
    private void RezCycle( string param ) {
      if (mRezzed.Count > mMaxZombies) return;

      double tInt = mZombieEmitTM - mGame.Players.Count + 1;
      if (tInt < 2) tInt = 2;
      if (mInterval != tInt) {
        mInterval = tInt;
        mGame.Tmr.TInterval( "ZombieRez", mInterval );
      }

      // select the kind of zombie to be rezzed
      int nzk = mNpcs.Count;
      cNpc npc = nzk < 2 ? mNpcs[0] : mNpcs[mGame.Rnd.Next( nzk )];

      // select position where emit zomnbie
      Vector3 pos = mRezPos[mGame.Rnd.Next( mRezPos.Count )];
        
      // create zombie       
      UUID id = mGame.NpcRez( npc.Names[0], npc.Names[1], pos, npc.Appearance );
      if (id == null && id == UUID.Zero) return;

      mGame.SetSpeed( id, (float)mSpeed );
      mRezzed.Add( id.ToString(), new cZombie( id ) );      
    }
    
    private void SpeedCycle( string param ) {
      mSpeed += 0.2;
      if (mSpeed > 3.2) {
        mSpeed = 3.2;
        mGame.Tmr.TRemove( "ZombieSpeed" );
      }
    }

    private void MoveCycle( string param ) {
      DateTime atm = DateTime.Now;
      List<string> flush = new List<string>();

      ezAgPos[] avs = mGame.Players.GetPlayersPos();

      foreach (cZombie z in mRezzed.Values) {
        if (!z.IsAlive) { 
          if (z.DieTime >= mZombieDieTM) {
            mGame.NpcRemove( z.Id );
            flush.Add( z.Id.ToString() );
          }
          continue;
        }        

        Vector3 zpos = mGame.GetAgentPos( z.Id );
        double dist = 9999;
        Vector3 pos = Vector3.Zero;
        foreach(ezAgPos p in avs) {
          double d = mGame.VecDist( p.Pos, zpos );
          if (d < dist) { pos = p.Pos; dist = d; }
          if (d < mBiteDist) mGame.Players.SetLife( p.Id, -mBiteLife );
          else if (d < mScratchDist) mGame.Players.SetLife( p.Id, -mScratchLife );

          if (mSnds.Count > 0 && d < mZombieRoarDist && z.SndTime > mZombieRoarTM) {            
            z.Roar();
            mGame.PlaySound( mSnds[mGame.Rnd.Next( mSnds.Count )], zpos, (float)mZombieRoarDist );
          }
        }
        if (dist > mSniffDist)
          mGame.NpcStop( z.Id );
        else
          mGame.NpcWalkTo( z.Id, pos );
      }

      foreach (string id in flush) mRezzed.Remove( id );
    }

    public void Dispose() {
      Enabled( false );
      mNpcs.Clear();      
      mRezPos.Clear();
      mDieAnim.Clear();
      mSnds.Clear();
    }

    private string GetDieAnim() {
      string ret = null;
      if (mDieAnim.Count == 1) ret = mDieAnim[0];
      if (mDieAnim.Count > 1) ret = mDieAnim[mGame.Rnd.Next(mDieAnim.Count)];
      return ret;
    }

  }
}
