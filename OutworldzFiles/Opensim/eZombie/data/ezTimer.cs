using System;
using System.Timers;
using System.Collections.Generic;


namespace eZombies {

  internal class ezTimer : IDisposable {
    
    private class TIrun {
      public string Key;      
      public Action<string> CbFnc;
      
      public TIrun( string key, Action<string> cbfnc ) {
        Key = key;
        CbFnc = cbfnc;
      }
    }

    private class TInfo {
      public string Key;
      public double Interval;
      public bool Enabled;
      public DateTime Next;
      public Action<string> CbFnc;
      public bool OnlyOnce;

      public TInfo( string key, double secs, bool enabled, Action<string> cbfnc, bool onlyonce = false ) {
        Key = key;
        Interval = secs;
        Enabled = enabled;
        Next = DateTime.Now.AddSeconds( Interval );
        CbFnc = cbfnc;
        OnlyOnce = onlyonce;                
      }

      public void ResetNext( DateTime? tm = null ) {
        Next = (tm == null ? DateTime.Now : (DateTime)tm).AddSeconds( Interval );
      }
    }
    
    private static Timer mMTmr = null;
    private static DateTime mBaseData;
    private Dictionary<string, TInfo> mTmrs = new Dictionary<string, TInfo>();
    private object mTmrLock = new object();
    //private bool mProcessing = false;

    public bool Enabled { get { return mMTmr.Enabled; } set { mMTmr.Enabled = value; } }

    public ezTimer( double interval = 200, bool enab = true ) {
      mBaseData = DateTime.Now;
         
      mMTmr = new Timer( interval );
      mMTmr.Elapsed += OnTimerCycle;
      mMTmr.AutoReset = true;
      mMTmr.Enabled = enab;
    }

    public void Dispose() {
      mMTmr.Stop();
      mMTmr.Dispose();
      lock (mTmrLock) { mTmrs.Clear(); }
    }

    public void TCreate( string key, double sec, bool enab, Action<string> cbfnc, bool onlyonce = false ) {
      lock (mTmrLock) {
        if (!mTmrs.ContainsKey( key ))
          mTmrs.Add( key, new TInfo( key, sec, enab, cbfnc, onlyonce ) );
      }
    }

    public void TRemove( string key ) {
      lock (mTmrLock) {
        if (mTmrs.ContainsKey( key )) mTmrs.Remove( key );
      }
    }

    public void TEnable( string key, bool sts ) {
      lock (mTmrLock) {
        if (mTmrs.ContainsKey( key )) {
          mTmrs[key].Enabled = sts;
          if (sts) mTmrs[key].ResetNext();
        }
      }
    }

    public void TInterval( string key, double interval ) {
      lock (mTmrLock) {
        if (mTmrs.ContainsKey( key )) {
          mTmrs[key].Interval = interval;          
        }
      }
    }

    private void OnTimerCycle( object source, ElapsedEventArgs e ) {
      //if (mProcessing) return;
      //mProcessing = true;

      List<TIrun> elst = new List<TIrun>();      
      DateTime tm = DateTime.Now;

      lock (mTmrLock) {
        List<string> tokill = new List<string>();

        foreach (TInfo ti in mTmrs.Values) {
          if (ti.Enabled && ti.Next <= tm) {
            elst.Add( new TIrun( ti.Key, ti.CbFnc ) );
            if (ti.OnlyOnce)
              tokill.Add( ti.Key );
            else
              ti.ResetNext( tm );
          }
        }
        foreach (string k in tokill) mTmrs.Remove( k );        
      }

      foreach (TIrun tir in elst) tir.CbFnc( tir.Key );       

      //mProcessing = false;
    }    
  }
}
