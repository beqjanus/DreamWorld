#Region "Copyright"

' Copyright 2014 Fred Beckhusen for www.Outworldz.com https://opensource.org/licenses/AGPL

'Permission Is hereby granted, free Of charge, to any person obtaining a copy of this software
' And associated documentation files (the "Software"), to deal in the Software without restriction,
'including without limitation the rights To use, copy, modify, merge, publish, distribute, sublicense,
'And/Or sell copies Of the Software, And To permit persons To whom the Software Is furnished To
'Do so, subject To the following conditions:

'The above copyright notice And this permission notice shall be included In all copies Or '
'substantial portions Of the Software.

'THE SOFTWARE Is PROVIDED "AS IS", WITHOUT WARRANTY Of ANY KIND, EXPRESS Or IMPLIED,
' INCLUDING BUT Not LIMITED To THE WARRANTIES Of MERCHANTABILITY, FITNESS For A PARTICULAR
'PURPOSE And NONINFRINGEMENT.In NO Event SHALL THE AUTHORS Or COPYRIGHT HOLDERS BE LIABLE
'For ANY CLAIM, DAMAGES Or OTHER LIABILITY, WHETHER In AN ACTION Of CONTRACT, TORT Or
'OTHERWISE, ARISING FROM, OUT Of Or In CONNECTION With THE SOFTWARE Or THE USE Or OTHER
'DEALINGS IN THE SOFTWARE.Imports System

#End Region

Imports System.Net.Sockets
Imports System.Text.RegularExpressions
Imports MySql.Data.MySqlClient

Public Class MysqlInterface

    Public Sub New()
        'nothing
    End Sub

    Public Sub DeleteSearchDatabase()

        Dim osconnection As MySqlConnection = New MySqlConnection(Form1.OSSearchConnectionString())
        Try
            osconnection.Open()
        Catch ex As Exception
            Debug.Print("Failed to Connect to OsSearch")
            Return
        End Try
        Try
            Dim stm As String = "DROP DATABASE ossearch;"
            Dim cmd As MySqlCommand = New MySqlCommand(stm, osconnection)
            cmd.ExecuteScalar()
        Catch ex As Exception
        Finally
            osconnection.Close()
        End Try

    End Sub

    Public Shared Sub DeleteRegionlist()

        Dim osconnection As MySqlConnection = New MySqlConnection(Form1.OSSearchConnectionString())
        Try
            osconnection.Open()
        Catch ex As Exception
            Debug.Print("Failed to Connect to OsSearch")
            Return
        End Try
        Dim stm As String = "delete from hostsregister"
        Dim cmd As MySqlCommand = New MySqlCommand(stm, osconnection)
        cmd.ExecuteScalar()
        osconnection.Close()

    End Sub

    <CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")>
    Public Function GetAgentList() As Dictionary(Of String, String)

        Dim Dict As New Dictionary(Of String, String)
        If Form1.PropMySetting.ServerType <> "Robust" Then Return Dict

        Dim NewSQLConn As New MySqlConnection(Form1.RobustMysqlConnection)
        Dim stm As String = "SELECT useraccounts.FirstName, useraccounts.LastName, regions.regionName FROM (presence INNER JOIN useraccounts ON presence.UserID = useraccounts.PrincipalID) INNER JOIN regions  ON presence.RegionID = regions.uuid;"

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
            NewSQLConn.Close()
        End Try

        Return Dict

    End Function

    Public Function GetHGAgentList() As Dictionary(Of String, String)

        ' griduse table column UserID
        '6f285c43-e656-42d9-b0e9-a78684fee15c;http://www.Outworldz.com:9000/;Ferd Frederix
        Dim Dict As New Dictionary(Of String, String)
        Dim NewSQLConn As New MySqlConnection(Form1.RobustMysqlConnection)
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
            NewSQLConn.Close()
        End Try

        Return Dict
    End Function

    <CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")>
    Private Function GetRegionName(UUID As String) As String
        Dim Val As String = ""
        Dim MysqlConn = New MySqlConnection(Form1.RobustMysqlConnection)
        Try

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

        Dim UserCount = QueryString("SELECT count(RegionID) from presence where RegionID = '" + regionUUID + "' And RegionID <> '00000000-0000-0000-0000-000000000000'")
        If UserCount = Nothing Then Return 0
        'Debug.Print("User Count: {0}", UserCount)
        Return Convert.ToInt16(UserCount, Form1.Usa)

    End Function

    Public Function IsMySqlRunning() As String

        Dim Mysql = CheckPort("127.0.0.1", Form1.PropMySetting.MySqlRegionDBPort)
        If Mysql Then
            Dim version = QueryString("SELECT VERSION()")
            Debug.Print("MySQL version: {0}", version)
            Return version
        End If
        Return Nothing

    End Function

    Public Sub DeregisterRegions()

        If Form1.PropOpensimIsRunning Then
            MsgBox("Opensim is running. Cannot clear the list of registered regions", vbInformation)
            Return
        End If

        Dim Mysql = CheckPort("127.0.0.1", CType(Form1.PropMySetting.MySqlRobustDBPort, Integer))
        If Mysql Then
            QueryString("delete from robust.regions;")
            Form1.Print("All Regions are deregistered.")
        End If

    End Sub

    <CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")>
    Public Function QueryString(SQL As String) As String
        Dim MysqlConn = New MySqlConnection(Form1.RobustMysqlConnection)
        Try
            MysqlConn.Open()
            Dim cmd As MySqlCommand = New MySqlCommand(SQL, MysqlConn)
            Dim v = Convert.ToString(cmd.ExecuteScalar(), Form1.Usa)
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

    ''' <summary>
    ''' Returns Estate Name give an Estate UUID
    ''' </summary>
    ''' <param name="UUID"></param>
    ''' <returns>Name as string</returns>
    Public Function EstateName(UUID As String) As String

        If Form1.RegionMySqlConnection.Length = 0 Then Return ""

        Debug.Print(Form1.RegionMySqlConnection)
        Dim name As String = ""
        Dim Val As String = ""
        Dim MysqlConn As MySqlConnection
        Try
            MysqlConn = New MySqlConnection(Form1.RegionMySqlConnection)
            MysqlConn.Open()
            Dim stm = "Select EstateID from opensim.estate_map where regionid = '" & UUID & "';"
            Dim cmd As MySqlCommand = New MySqlCommand(stm, MysqlConn)
            Dim reader As MySqlDataReader = cmd.ExecuteReader()

            If reader.Read() Then
                Debug.Print("ID = {0}", reader.GetString(0))
                Val = reader.GetString(0)
            End If
            reader.Close()
        Catch ex As MySqlException
            Console.WriteLine("Error: " & ex.ToString())
            Return ""
        End Try

        Try

            Dim stm1 = "Select EstateName from opensim.estate_settings where EstateID = '" & Val & "';"
            Dim cmd2 As MySqlCommand = New MySqlCommand(stm1, MysqlConn)
            Dim reader2 As MySqlDataReader = cmd2.ExecuteReader()

            If reader2.Read() Then
                Debug.Print("Name = {0}", reader2.GetString(0))
                name = reader2.GetString(0)
            End If
            reader2.Close()
        Catch ex As MySqlException
            Console.WriteLine("Error: " & ex.ToString())
            Return ""
        Finally
            MysqlConn.Close()
        End Try

        Return name

    End Function

End Class