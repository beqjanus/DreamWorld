Imports System.Net.Sockets
Imports System.Text.RegularExpressions
Imports MySql.Data.MySqlClient

Public Class MysqlInterface

    Implements IDisposable

    Private ReadOnly MysqlConn As MySqlConnection
    Private _gConnStr As String = ""

    Public Sub New(connStr As String)
        GConnStr = connStr
        MysqlConn = New MySqlConnection(connStr)
    End Sub

    <CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")>
    Public Function GetAgentList() As Dictionary(Of String, String)

        Dim NewSQLConn As New MySqlConnection(GConnStr)
        Dim stm As String = "SELECT useraccounts.FirstName, useraccounts.LastName, regions.regionName FROM (presence INNER JOIN useraccounts ON presence.UserID = useraccounts.PrincipalID) INNER JOIN regions  ON presence.RegionID = regions.uuid;"
        Dim Dict As New Dictionary(Of String, String)

        Try
            NewSQLConn.Open()
            Dim cmd As MySqlCommand = New MySqlCommand(stm, NewSQLConn)
            Dim reader As MySqlDataReader = cmd.ExecuteReader()

            While reader.Read()
                Debug.Print(reader.GetString(0) & " " & reader.GetString(1) & " in region " & reader.GetString(2))
                Dict.Add(reader.GetString(0) & " " & reader.GetString(1), reader.GetString(2))
            End While
        Catch ex As MySqlException
            Console.WriteLine("Error: " & ex.ToString())
        Finally
            MysqlConn.Close()
        End Try

        Return Dict

    End Function

    Public Function GetHGAgentList() As Dictionary(Of String, String)

        ' griduse table column UserID
        '6f285c43-e656-42d9-b0e9-a78684fee15c;http://www.Outworldz.com:9000/;Ferd Frederix
        Dim Dict As New Dictionary(Of String, String)
        Dim NewSQLConn As New MySqlConnection(GConnStr)
        Dim UserStmt = "SELECT UserID, LastRegionID from GridUser where online = 'true'"
        Dim pattern As String = "(.*?);.*;(.*)$"
        Dim Avatar As String = ""
        Dim UUID As String = ""

        Try
            NewSQLConn.Open()
            Dim cmd As MySqlCommand = New MySqlCommand(UserStmt, NewSQLConn)
            Dim reader As MySqlDataReader = cmd.ExecuteReader()

            While reader.Read()
                Debug.Print(reader.GetString(0))
                Dim LongName = reader.GetString(0)
                UUID = reader.GetString(1)
                For Each m In Regex.Matches(LongName, pattern)
                    Debug.Print("Avatar {0}", m.Groups(2).Value)
                    Debug.Print("Region UUID {0}", m.Groups(1).Value)
                    Avatar = m.Groups(2).Value.ToString
                    Dict.Add(Avatar, GetRegionName(UUID))
                Next

            End While
        Catch ex As MySqlException
            Console.WriteLine("Error: " & ex.ToString())
        Finally
            MysqlConn.Close()
        End Try

        Return Dict
    End Function

    <CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")>
    Private Function GetRegionName(UUID As String) As String
        Dim Val As String = ""
        Try
            Dim MysqlConn = New MySqlConnection(GConnStr)
            MysqlConn.Open()

            Dim stm = "Select RegionName from regions where uuid = '" & UUID & "';"
            Dim cmd As MySqlCommand = New MySqlCommand(stm, MysqlConn)
            Dim reader As MySqlDataReader = cmd.ExecuteReader()

            If reader.Read() Then
                Debug.Print("Region Name = {0}", reader.GetString(0))
                Val = reader.GetString(0)
            End If
        Catch ex As MySqlException
            Console.WriteLine("Error: " & ex.ToString())
        Finally
            MysqlConn.Close()
        End Try

        Return Val

    End Function

    Public Function IsUserPresent(regionUUID As String) As Integer

        Dim UserCount = QueryString("SELECT count(RegionID) from presence where RegionID = '" + regionUUID + "'")
        If UserCount = Nothing Then Return 0
        'Debug.Print("User Count: {0}", UserCount)
        Return Convert.ToInt16(UserCount)

    End Function

    Public Function IsMySqlRunning() As String

        Dim Mysql = CheckPort("127.0.0.1", CType(Form1.MySetting.MySqlPort, Integer))
        If Mysql Then
            Dim version = QueryString("SELECT VERSION()")
            Debug.Print("MySQL version: {0}", version)
            Return version
        End If
        Return Nothing

    End Function

    <CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")>
    Public Function QueryString(SQL As String) As String
        Try
            'Debug.Print("Connecting to MySQL...")
            Dim MysqlConn = New MySqlConnection(GConnStr)
            MysqlConn.Open()

            Dim cmd As MySqlCommand = New MySqlCommand(SQL, MysqlConn)
            Dim v = Convert.ToString(cmd.ExecuteScalar())
            Return v
        Catch ex As Exception
            Debug.Print(ex.Message)
        Finally
            MysqlConn.Close()
        End Try
        Return Nothing

    End Function

    Shared Function CheckPort(ServerAddress As String, Port As Integer) As Boolean

        Dim iPort As Integer = Convert.ToInt16(Port)
        Dim ClientSocket As New TcpClient

        Try
            ClientSocket.Connect(ServerAddress, iPort)
        Catch ex As Exception
            Return False
        End Try

        If ClientSocket.Connected Then
            ClientSocket.Close()
            Return True
        End If
        CheckPort = False

    End Function

#Region "IDisposable Support"

    Private disposedValue As Boolean ' To detect redundant calls

    Public Property GConnStr As String
        Get
            Return _gConnStr
        End Get
        Set(value As String)
            _gConnStr = value
        End Set
    End Property

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                MysqlConn.Close()
                MysqlConn.Dispose()
            End If
        End If
        disposedValue = True

    End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    <CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1816:CallGCSuppressFinalizeCorrectly")>
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
    End Sub

#End Region

End Class