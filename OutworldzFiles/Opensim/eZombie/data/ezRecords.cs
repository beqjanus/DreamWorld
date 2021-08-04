using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Net;
using OpenMetaverse;

namespace eZombies {

  internal class ezRecords : IDisposable {

    private class cPlRec {

      private string mId;
      private string mName;      
      private string mPlayTm;
      private int mKills;
      private int mScore;
      
      public string Id { get { return mId; } set { mId = value; } }
      public string Name { get { return mName; } set { mName = value; } }
      public string PlayTm { get { return mPlayTm; } set { mPlayTm = value; } }
      public int Kills { get { return mKills; } set { mKills = value; } }
      public int Score { get { return mScore; } set { mScore = value; } }
  
      public cPlRec() {
        mId = null;
        mPlayTm = null;
        mName = null;
        mKills = 0;
        mScore = 0;        
      }

      public cPlRec( string[] par ) {
        UUID kpar;
        int ipar;

        mId = UUID.TryParse( par[0], out kpar ) ? kpar.ToString() : null;
        mPlayTm = par[1];
        mName = par[2];
        mKills = int.TryParse( par[3], out ipar ) ? ipar : 0;
        mScore = int.TryParse( par[4], out ipar ) ? ipar : 0;
      }

      public cPlRec( string id, string name ) {
        mId = id;
        mName = name;
        mPlayTm = null;
        mName = null;
        mKills = 0;
        mScore = 0;
      }

      public string Encode() {
        return string.Format( "{0}|{1}|{2}|{3}|{4}",  mId, mPlayTm, mName, mKills, mScore );
      }

      public object[] Encode2List() {
        return new object[] { mId, mName, mPlayTm, mKills, mScore };
      }      
    }
        
    private Dictionary<string, cPlRec> mLst = null;
    private string mStorePath = null;             // Path where store data on server 
    private int mTopScore = 0;
    private int mTopKills = 0;

    private List<cPlRec> mTop = null;
    private int mTopMin = 0;
    
    private bool mModified;
    private eZombie mGame;

    private Dictionary<string, string> mScoreUrls = null;   // list of URL where send scores

    public int TopKills { get { return mTopKills; } }
    public int TopScore { get { return mTopScore; } }

    public ezRecords( eZombie game, string path ) {
      mGame = game;
      mLst = new Dictionary<string, cPlRec>();
      mScoreUrls = new Dictionary<string, string>();
      mTop = new List<cPlRec>();
      mTopMin = 0;      

      mStorePath = path + mGame.GetRegionName(true) +".tdb";
      
      LoadRecords();
      RebuildTop();
    }

    private void LoadRecords() {
      if (!File.Exists( mStorePath )) return;

      mLst.Clear();

      IEnumerable<string> lines = File.ReadLines( mStorePath );
      foreach (string line in lines) {
        string[] par = line.Split( new char[] { '|' } );
        if (par.Length == 5) {
          cPlRec rec = new cPlRec( par );
          if (rec.Id != null) {
            mLst.Add( rec.Id, rec );
            if (rec.Kills > mTopKills) mTopKills = rec.Kills;
            if (rec.Score > mTopScore) mTopScore = rec.Score;
          }
        }        
      }
      mModified = false;
    }

    public void ClearParam() {
      if (mScoreUrls != null) mScoreUrls.Clear();
    }

    public bool DecodeParam( string cmd, string val ) {
      bool ok = false;

      if (cmd == "score_url" && !string.IsNullOrEmpty(val)) {
        string[] pars = val.Split( new char[] { '|' } );
        if (pars.Length == 2 && Uri.IsWellFormedUriString( pars[1], UriKind.RelativeOrAbsolute ) && !string.IsNullOrEmpty( pars[0] )) {
          mScoreUrls[pars[1]] = pars[0];
          ok = true;
        }       
      } 

      return ok;
    }

    public int SetPlayer( string id, string name, TimeSpan tm, int kill, int score ) {
      int ret = 0;
      if (kill > mTopKills) { ret |= 1; mTopKills = kill; }
      if (score > mTopScore) { ret |= 2; mTopScore = score; }

      cPlRec pl;
      if (mLst.ContainsKey( id ))
        pl = mLst[id];
      else {
        pl = new cPlRec() { Id = id, Name = name.Replace( '|', '_' ) };
        mLst.Add( pl.Id, pl );
      }

      if (pl.Score <= score) {
        pl.Score = score;
        pl.Kills = kill;
        pl.PlayTm = tm.ToString( @"h\:mm\:ss" );
        mModified = true;
        StoreRecords();
        /// message personal record
      }      

      if (score > mTopMin) {
        RebuildTop();
        mGame.Tmr.TCreate( "Records", 5, true, UpdateTops, true );
      }

      return ret;
    }

    public int GetPlayerRecord( string id ) {
      return mLst.ContainsKey( id ) ? mLst[id].Score : 0;
    }

    public object[] GetTopPlayers() {      
      List<object> ret = new List<object>();
      foreach(cPlRec r in mTop) {        
        ret.Add( r.Name );
        ret.Add( r.Score );
        ret.Add( r.Kills );
        ret.Add( r.PlayTm );        
      }
      return ret.ToArray();
    }
            
    private void StoreRecords() {
      if (mModified) {
        List<string> lines = new List<string>();

        foreach (cPlRec rec in mLst.Values) lines.Add( rec.Encode() );
        File.WriteAllLines( mStorePath, lines, Encoding.UTF8 );
        mModified = false;

        StoreToWeb();
      }
    }

    public void StoreToWeb() {
      if (mScoreUrls.Count < 1) return;

      List<object> scores = new List<object>();
      foreach (cPlRec rec in mLst.Values) scores.Add( rec.Encode2List() );

      // fields sequence "uID,uName,uTime,uKills,uScore"
      foreach (KeyValuePair<string, string> kv in mScoreUrls)
        Send( kv.Key, kv.Value, scores );      
    }
    
    public void Dispose() {
      // not sure this can be done without delay shutdown too long
      //StoreRecords();

      mLst.Clear();
      mScoreUrls.Clear();
    }

    private void UpdateTops( string param ) {
      // can't happen, but better be sure and avoid exceptions
      if (mGame.IsActive) 
        mGame.HostMessage( "ezTOPS|" + mTop.Count.ToString() );              
    }

    public void RebuildTop() {
      List<cPlRec> lst = new List<cPlRec>();
      lst.AddRange( mLst.Values );
      lst.Sort( delegate ( cPlRec a, cPlRec b ) { return b.Score.CompareTo( a.Score ); } );

      mTop.Clear();
      mTopMin = int.MaxValue;
      int i = 0;
      int n = Math.Min( lst.Count, 10 );      
      foreach (cPlRec r in lst) {
        if (++i > n) break;
        mTop.Add( r );
        mTopMin = Math.Min( mTopMin, r.Score );
      }            
    }

    #region SEND REQ to Urls
    private void Send( string url, string guid, List<object>scores ) {      
      if (string.IsNullOrEmpty( url )) return;
            
      string sendJT = jsonEncode.Encode( new Dictionary<string, object> {
        { "gameID", guid },
        { "method", "scoreupdate" },
        { "regionID", mGame.GetRegionID() },
        { "regionName", mGame.GetRegionName() },
        { "scores", scores }
      } );      
      byte[] buffer = Encoding.UTF8.GetBytes( sendJT );
      
      WebRequest wr = WebRequest.Create( url );
      wr.Method = "PUT";
      wr.Timeout = 20 * 1000;
      wr.ContentType = "application/json";      
      wr.ContentLength = buffer.Length;

      try {
        using (Stream rs = wr.GetRequestStream()) {
          rs.Write( buffer, 0, buffer.Length );
          rs.Flush();
          rs.Close();
        }
        wr.BeginGetResponse( AsyncCallback, wr );
      }
      catch (Exception) { }      
    }

    private static void AsyncCallback( IAsyncResult result ) {
      WebRequest request = (WebRequest)result.AsyncState;
      using (WebResponse resp = request.EndGetResponse( result )) {}
    }
    #endregion SEND REQ to Urls
  }
}
