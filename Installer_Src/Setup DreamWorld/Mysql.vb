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

Public Module MysqlInterface

    Sub New()
        'nothing
    End Sub

    Public Function CheckPort(ServerAddress As String, Port As Integer) As Boolean

        Dim iPort As Integer = Convert.ToInt16(Port)
        Using ClientSocket As New TcpClient
            Try
                ClientSocket.Connect(ServerAddress, iPort)
            Catch ex As ArgumentNullException
                Return False
            Catch ex As ArgumentOutOfRangeException
                Return False
            Catch ex As SocketException
                Return False
            Catch ex As ObjectDisposedException
                Return False
            End Try

            If ClientSocket.Connected Then
                Return True
            End If
        End Using

        Return False

    End Function

    Public Sub DeleteRegionlist()

        Using osconnection As MySqlConnection = New MySqlConnection(Form1.Settings.OSSearchConnectionString())
            Try
                osconnection.Open()
                Dim stm As String = "delete from hostsregister"
                Using cmd As MySqlCommand = New MySqlCommand(stm, osconnection)
                    cmd.ExecuteScalar()
                End Using
            Catch ex As InvalidOperationException
                Debug.Print("Failed to Connect to OsSearch")
                Return
            Catch ex As MySqlException
                Debug.Print("Failed to Connect to OsSearch")
                Return
            End Try
        End Using
    End Sub

    Public Sub DeleteSearchDatabase()

        Using osconnection As MySqlConnection = New MySqlConnection(Form1.Settings.OSSearchConnectionString())
            Try
                osconnection.Open()
                Dim stm As String = "DROP DATABASE ossearch;"
                Using cmd As New MySqlCommand(stm, osconnection)
                    cmd.ExecuteScalar()
                End Using
            Catch ex As InvalidOperationException
                Debug.Print("Failed to Connect to OsSearch")
                Return
            Catch ex As MySqlException
                Debug.Print("Failed to Connect to OsSearch")
                Return
            End Try
        End Using

    End Sub

    Public Sub DeregisterRegions()

        If Form1.PropOpensimIsRunning Then
            MsgBox("Opensim is running. Cannot clear the list of registered regions", vbInformation)
            Return
        End If

        Dim Mysql = CheckPort("127.0.0.1", CType(Form1.Settings.MySqlRobustDBPort, Integer))
        If Mysql Then
            QueryString("delete from robust.regions;")
            Form1.Print(My.Resources.Deregister_All)
        End If

    End Sub

    ''' <summary>
    ''' Returns Estate Name give an Estate UUID
    ''' </summary>
    ''' <param name="UUID"></param>
    ''' <returns>Name as string</returns>
    Public Function EstateName(UUID As String) As String

        If Form1.Settings.RegionMySqlConnection.Length = 0 Then Return ""

        'Debug.Print(Form1.Settings.RegionMySqlConnection)
        Dim name As String = ""
        Dim Val As String = ""

        Try
            Using MysqlConn As New MySqlConnection(Form1.Settings.RegionMySqlConnection)
                MysqlConn.Open()
                Dim stm = "Select EstateID from estate_map where regionid = '" & UUID & "';"
#Disable Warning CA2100 ' Review SQL queries for security vulnerabilities
                Using cmd As MySqlCommand = New MySqlCommand(stm, MysqlConn)
#Enable Warning CA2100 ' Review SQL queries for security vulnerabilities
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            'Debug.Print("ID = {0}", reader.GetString(0))
                            Val = reader.GetString(0)
                        End If
                    End Using
                End Using
            End Using
        Catch ex As MySqlException
            Console.WriteLine("Error: " & ex.ToString())
            Return ""
        End Try

        Try
            Dim stm1 = "Select EstateName from estate_settings where EstateID = '" & Val & "';"
            Using MysqlConn As New MySqlConnection(Form1.Settings.RegionMySqlConnection)
                MysqlConn.Open()
#Disable Warning CA2100 ' Review SQL queries for security vulnerabilities
                Using cmd2 As MySqlCommand = New MySqlCommand(stm1, MysqlConn)
#Enable Warning CA2100 ' Review SQL queries for security vulnerabilities
                    Using reader2 As MySqlDataReader = cmd2.ExecuteReader()
                        If reader2.Read() Then
                            'Debug.Print("Name = {0}", reader2.GetString(0))
                            name = reader2.GetString(0)
                        End If
                    End Using
                End Using
            End Using
        Catch ex As MySqlException
            Console.WriteLine("Error: " & ex.ToString())
            Return ""
        Finally

        End Try

        Return name

    End Function

    Public Function GetAgentList() As Dictionary(Of String, String)

        Dim Dict As New Dictionary(Of String, String)
        If Form1.Settings.ServerType <> "Robust" Then Return Dict

        Using NewSQLConn As New MySqlConnection(Form1.Settings.RobustMysqlConnection)
            Dim stm As String = "SELECT useraccounts.FirstName, useraccounts.LastName, regions.regionName FROM (presence INNER JOIN useraccounts ON presence.UserID = useraccounts.PrincipalID) INNER JOIN regions  ON presence.RegionID = regions.uuid;"

            Try
                NewSQLConn.Open()
                Using cmd As New MySqlCommand(stm, NewSQLConn)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            Debug.Print(reader.GetString(0) & " " & reader.GetString(1) & " in region " & reader.GetString(2))
                            Dict.Add(reader.GetString(0) & " " & reader.GetString(1), reader.GetString(2))
                        End While
                    End Using
                End Using
            Catch ex As MySqlException
                Console.WriteLine("Error: " & ex.ToString())
            End Try
        End Using

        ' If Debugger.IsAttached Then
        '    Dict.Add("Test User", "Welcome")
        'End If

        Return Dict

    End Function

    Public Function GetHGAgentList() As Dictionary(Of String, String)

        ' griduse table column UserID
        '6f285c43-e656-42d9-b0e9-a78684fee15c;http://www.Outworldz.com:9000/;Ferd Frederix
        Dim Dict As New Dictionary(Of String, String)

        Dim UserStmt = "SELECT UserID, LastRegionID from GridUser where online = 'true'"
        Dim pattern As String = "(.*?);.*;(.*)$"
        Dim Avatar As String
        Dim UUID As String
        Using NewSQLConn As New MySqlConnection(Form1.Settings.RobustMysqlConnection)
            Try
                NewSQLConn.Open()
                Using cmd As MySqlCommand = New MySqlCommand(UserStmt, NewSQLConn)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()

                        While reader.Read()
                            Debug.Print(reader.GetString(0))
                            Dim LongName = reader.GetString(0)
                            UUID = reader.GetString(1)
                            For Each m In Regex.Matches(LongName, pattern)
                                Debug.Print("Avatar {0}", m.Groups(2).Value)
                                Debug.Print("Region UUID {0}", m.Groups(1).Value)
                                Avatar = m.Groups(2).Value.ToString
                                If UUID <> "00000000-0000-0000-0000-000000000000" Then
                                    Dict.Add(Avatar, GetRegionName(UUID))
                                End If

                            Next
                        End While
                    End Using
                End Using
#Disable Warning CA1031 ' Do not catch general exception types
            Catch ex As Exception
#Enable Warning CA1031 ' Do not catch general exception types
                Console.WriteLine("Error: " & ex.ToString())
            End Try
        End Using

        'If Debugger.IsAttached Then
        'Dict.Add("Test User2", "Welcome")
        'End If

        Return Dict

    End Function

    Public Function IsMySqlRunning() As String

        Dim Mysql = CheckPort("127.0.0.1", Form1.Settings.MySqlRegionDBPort)
        If Mysql Then
            Dim version = QueryString("SELECT VERSION()")
            Debug.Print("MySQL version: {0}", version)
            Return version
        End If
        Return Nothing

    End Function

    Public Function QueryString(SQL As String) As String
        Using MysqlConn = New MySqlConnection(Form1.Settings.RobustMysqlConnection)
            Try
                MysqlConn.Open()
                Dim v As String
#Disable Warning CA2100 ' Review SQL queries for security vulnerabilities
                Using cmd As MySqlCommand = New MySqlCommand(SQL, MysqlConn)
#Enable Warning CA2100 ' Review SQL queries for security vulnerabilities
                    v = Convert.ToString(cmd.ExecuteScalar(), Globalization.CultureInfo.InvariantCulture)
                End Using
                Return v
#Disable Warning CA1031 ' Do not catch general exception types
            Catch ex As Exception
#Enable Warning CA1031 ' Do not catch general exception types
                Debug.Print(ex.Message)
            End Try
        End Using

        Return ""

    End Function

    Private Function GetRegionName(UUID As String) As String
        Dim Val As String = ""
        Dim MysqlConn = New MySqlConnection(Form1.Settings.RobustMysqlConnection)
        Try
            MysqlConn.Open()

            Dim stm = "Select RegionName from regions where uuid = '" & UUID & "';"
#Disable Warning CA2100 ' Review SQL queries for security vulnerabilities
            Using cmd As New MySqlCommand(stm, MysqlConn)
                Using reader As MySqlDataReader = cmd.ExecuteReader()
#Enable Warning CA2100 ' Review SQL queries for security vulnerabilities

                    If reader.Read() Then
                        Debug.Print("Region Name = {0}", reader.GetString(0))
                        Val = reader.GetString(0)
                    End If
                End Using
            End Using
        Catch ex As MySqlException
            Console.WriteLine("Error: " & ex.ToString())
        Finally
            MysqlConn.Close()
        End Try

        Return Val

    End Function

End Module
