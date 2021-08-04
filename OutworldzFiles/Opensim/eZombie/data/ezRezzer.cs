using System;
using System.Collections.Generic;
using OpenMetaverse;

namespace eZombies {

  internal class ezRezzer : IDisposable {
    private static readonly double cBRINGDIST = 2;

    public enum eType {
      HealBox = 0,
      Potion = 1,
      Bomb = 2,
      Prize1 = 3,
      Prize2 = 4,
      Prize3 = 5
    };

    private class cRezDef {
      public eType Tp { get; set; }
      public string Name { get; set; }
      public double Perc { get; set; }

      public cRezDef( ezRezzer.eType tp, string name, double perc ) {
        Tp = tp;
        Name = name;
        Perc = perc;
      }
    }

    private class cRezzed {
      private UUID mId;
      private DateTime mTm;
      private Vector3 mPos;
      private eType mTp;

      public UUID Id { get { return mId; } }
      public DateTime Tm { get { return mTm; } }
      public Vector3 Pos { get { return mPos; } }
      public eType Tp { get { return mTp; } }

      public cRezzed( UUID id, Vector3 pos, eType tp ) {
        mId = id;
        mPos = pos;
        mTp = tp;
        mTm = DateTime.Now;
      }
    }

    private List<Vector3> mRezPos = null;
    private Dictionary<eType, cRezDef> mObjs = null;
    private List<cRezzed> mRezzed = null;
    private bool mEnabled = false;
    private double mInterval;
    private double mLifeTm;    
    private eZombie mGame;
        
    public int Count { get { return mRezzed.Count; } }

    public ezRezzer( eZombie game ) {
      mGame = game;

      mObjs = new Dictionary<eType, cRezDef>() {
        { eType.HealBox, null },
        { eType.Potion, null },
        { eType.Bomb, null },
        { eType.Prize1, null },
        { eType.Prize2, null },
        { eType.Prize3, null }
      };
      mRezzed = new List<cRezzed>();
      mRezPos = new List<Vector3>();

      mEnabled = false;                
      mLifeTm = 60;
    }

    public void Enabled( bool sts ) {
      if (sts == mEnabled) return;

      mEnabled = sts;
      if (mEnabled) {
        mGame.Tmr.TCreate( "ObjRez", mInterval, true, RezCycle );
        mGame.Tmr.TCreate( "ObjKill", 1, true, DieCycle );
      }
      else {
        mGame.Tmr.TRemove( "ObjRez" );
        mGame.Tmr.TRemove( "ObjKill" );

        foreach (cRezzed r in mRezzed) mGame.ObjectRemove( r.Id );
        mRezzed.Clear();
      }
    }

    public bool DecodeParam( string cmd, string val) {
      bool ok = true;
            
      double dtmp;
      Vector3 vtmp;
      
      if (cmd == "objrez_pos" && Vector3.TryParse( val, out vtmp )) mRezPos.Add( vtmp );
      else if (cmd == "objrez_tm" && double.TryParse( val, out dtmp )) mInterval = dtmp;
      else if (cmd == "objrez_life_tm" && double.TryParse( val, out dtmp )) mLifeTm = dtmp;      
      else ok = false;

      return ok;
    }   

    public void SetRezObj( eType tp, string name ) {
      double perc;
      int pos = name.LastIndexOf( "." );

      if (pos >= 0 && double.TryParse( name.Substring( pos+1 ), out perc )) 
        mObjs[tp] = new cRezDef( tp, name, perc );
    }

    public string[] ValidateParam() {
      List<string> ret = new List<string>();

      if( mRezPos.Count < 0 ) ret.Add( "No Objrez_Pos specified" );
      if (mInterval < 2 || mInterval > 30) ret.Add( "Invalid ObjRez_TM" );
      if (mLifeTm < 30 || mLifeTm > 180) ret.Add( "Invalid ObjRez_Life_TM" );
      if (mObjs[eType.HealBox] == null) ret.Add( "Missing rez.healbox.xx" );
      if (mObjs[eType.Potion] == null) ret.Add( "Missing rez.potion.xx" );
      if (mObjs[eType.Bomb] == null) ret.Add( "Missing rez.bomb.xx" );
      if (mObjs[eType.Prize1] == null) ret.Add( "Missing rez.prize1.xx" );
      if (TotalPerc() != 100) ret.Add( string.Format( "Total rez objects percent must be 100, is {0}", TotalPerc() ) );
      
      return ret.ToArray();
    }

    public string[] ListParam() {
      List<string> ret = new List<string>();

      ret.Add( string.Format( "objrez_pos: Nr. {0}", mRezzed.Count ) );
      ret.Add( string.Format( "objrez_tm: {0}", mInterval ) );
      ret.Add( string.Format( "objrez_life_tm: {0}", mLifeTm ) );

      return ret.ToArray();
    }

    private double TotalPerc() {     
      double p = 0;
      foreach (cRezDef def in mObjs.Values) {        
        if (def != null) p += def.Perc;
      }
      return p;
    }
    
    public List<eType>BringObjects( Vector3 avpos ) {
      List<eType> lst = new List<eType>();

      for( int i = mRezzed.Count - 1; i >= 0; i--) {
        cRezzed r = mRezzed[i];
        if (mGame.VecDist( avpos, r.Pos ) < cBRINGDIST) {
          lst.Add( r.Tp );
          mGame.ObjectRemove( r.Id );
          mRezzed.RemoveAt( i );
        }
      }

      return lst;
    }

    private void RezCycle( string param ) {
      if (!mEnabled) return;

      double i = mGame.Rnd.Next( 101 );
      double l = 0;
      cRezDef def = null;
      foreach (KeyValuePair<eType, cRezDef> r in mObjs) {
        if (i >= l && i <= l + r.Value.Perc) { def = r.Value; break; }
        l += r.Value.Perc;
      }
      if (def != null) {
        Vector3 pos = mRezPos[mGame.Rnd.Next( mRezPos.Count )];
        pos.X += (mGame.Rnd.Next( 7 ) - 3) / 2f;
        pos.Y += (mGame.Rnd.Next( 7 ) - 3) / 2f;
        UUID rid = mGame.ObjectRez( def.Name, pos );
        if (rid != UUID.Zero) mRezzed.Add( new cRezzed( rid, pos, def.Tp ) );        
      }
    }

    private void DieCycle( string param ) {
      DateTime atm = DateTime.Now;
      
      for (int k = mRezzed.Count - 1; k >= 0; k--) {
        if ((atm - mRezzed[k].Tm).TotalSeconds > mLifeTm) {
          mGame.ObjectRemove( mRezzed[k].Id );
          mRezzed.RemoveAt( k );
        }
      }
    }

    public void Dispose() {
      Enabled( false );
      mObjs.Clear();     
      mRezPos.Clear();
    }   
  }  
}

