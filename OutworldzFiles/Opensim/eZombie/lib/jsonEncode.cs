using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Reflection;

namespace eZombies {

  public class jsonEncode {

    public static string Encode( object obj ) {
      jsonEncode je = new jsonEncode( obj );
      return je.json;
    }

    // ----------------------------------------------------------
    // INSTANCE
    // ----------------------------------------------------------
    private StringBuilder _sb;
    private string _str = "";
    private string _spcs = "";
    public string json { get { return _str; } }

    public jsonEncode( object obj ) {
      if ( obj == null ) return;

      _sb = new StringBuilder();

      if ( obj is Array )
        _setArray( obj );

      else if ( obj.GetType().Name == typeof( Dictionary<,> ).Name )
        _setDictionary( obj );

      else if ( obj.GetType().Name == typeof( List<> ).Name )
        _setList( obj );

      else
        _setGenericClass( obj );

      _str = _sb.ToString();
      _sb.Clear();     
    }

    #region encode helpers   
    private void _setGenericClass( object obj ) {
      if ( obj == null ) { _sb.Append( "null" ); return; }

      Type t = obj.GetType();
      PropertyInfo[] props = t.GetProperties();
      if ( props.Length < 1 ) { _sb.Append( "null" ); return; }      
      
      bool second = false;
      _spcs += "  ";
      _sb.Append( "{\n" + _spcs );

      foreach ( PropertyInfo prp in props ) {
        if ( second ) _sb.Append(','); else second = true;
        _sb.Append( "\"" + prp.Name + "\":" );
        _setValue( prp.GetValue( obj, new object[] { } ) );
      }

      if (_spcs.Length > 1) _spcs = _spcs.Substring( 0, _spcs.Length - 2 );
      _sb.Append( '\n' + _spcs + '}' );
    } 
    
    private void _setDictionary( object obj ) {
      IDictionary idict = (IDictionary)obj;

      bool second = false;
      _spcs += "  ";
      _sb.Append( "{\n" );


      foreach (string key in idict.Keys) {
        if ( second ) _sb.Append( ",\n" ); else second = true;
        _sb.Append( _spcs + "\"" + key + "\":" );
        _setValue( idict[key] );
      }

      if (_spcs.Length > 1) _spcs = _spcs.Substring( 0, _spcs.Length - 2 );
      _sb.Append( '\n' + _spcs + '}' );
    }

    private void _setList(  object obj ) {
      IList ilist = (IList)obj;    

      bool second = false;
      _spcs += "  ";
      _sb.Append( "[\n" + _spcs );

      foreach ( object o in ilist ) {
        if ( second ) _sb.Append( ',' ); else second = true;
        _setValue( o );
      }

      if (_spcs.Length > 1) _spcs = _spcs.Substring( 0, _spcs.Length - 2 );
      _sb.Append( '\n' + _spcs + ']' );      
    }

    private void _setArray( object obj ) {
      Array c = (Array)obj;

      bool second = false;
      _spcs += "  ";
      _sb.Append( "[\n" + _spcs );

      foreach ( object o in c ) {
        if ( second ) _sb.Append( ',' ); else second = true;
        _setValue( o );
      }

      if (_spcs.Length > 1) _spcs = _spcs.Substring( 0, _spcs.Length - 2 );
      _sb.Append( '\n' + _spcs + ']' );
    }

    private void _setValue( object obj ) {

      if ( obj == null ) 
        _sb.Append( "null" );
      else if ( obj is bool ) 
        _sb.Append( (bool)obj ? "true" : "false" );
      else if ( obj is string ) 
        _setString( obj );
      else if ( obj is char ) 
        _sb.Append( "\"" + (char)obj + "\"" );
      else if ( (obj is DateTime) || (obj is DateTime?) ) 
        _setDateTime( obj );      
      else if ( obj is int || obj is long || obj is double || obj is decimal || obj is float ||
                obj is byte || obj is short || obj is sbyte || obj is ushort || obj is uint || obj is ulong ) 
        _sb.Append( ((IConvertible)obj).ToString( NumberFormatInfo.InvariantInfo ) );
      else if ( obj is Enum ) 
        _sb.Append( ((int)obj).ToString( NumberFormatInfo.InvariantInfo ) );
      else if ( obj.GetType().Name == typeof( Dictionary<,> ).Name ) 
        _setDictionary( obj );
      else if ( obj.GetType().Name == typeof( List<> ).Name ) 
        _setList( obj );
      else if ( obj is Array) 
        _setArray( obj );
      else
        _setGenericClass( obj );            
    }

    private void _setString( object ostr ) {
      string str = (string)ostr;
      if ( string.IsNullOrEmpty( str ) ) { _sb.Append( "\"\"" ); return; }

      _sb.Append( '\"' );
      int n, l = str.Length;

      for ( n = 0; n < l; n++ ) {
        char c = str[n];
        string add = "";

        switch ( c ) {
          case '"': add += "\\\""; break;
          case '\\': add += "\\\\"; break;
          case '\b': add += "\\b"; break;
          case '\f': add += "\\f"; break;
          case '\t': add += "\\t"; break;
          case '\r': add += "\\r"; break;
          case '\n': add += "\\n"; break;
          default: break;
        }
        if ( add != "" ) { _sb.Append(add); continue; }

        if ( (c >= ' ') && (c < 128) ) { _sb.Append( c ); continue; }

        _sb.Append( "\\u" + ((int)c).ToString( "x4", NumberFormatInfo.InvariantInfo ) );
      }
      _sb.Append( '\"' );
    }

    private void _setDateTime( object odt ) {
      if ( odt == null ) { _sb.Append( "null" ); return; }      
      _sb.Append( '\"' + string.Format( "{0:yyyy-MM-dd HH:mm:ss}", (DateTime)odt ) + '\"' );
    }    
    #endregion
  }
 
}
