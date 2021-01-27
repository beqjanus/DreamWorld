#Region "Copyright"

' Copyright 2014 Fred Beckhusen for outworldz.com https://opensource.org/licenses/AGPL

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

Imports System.IO
Imports System.Net.Sockets
Imports System.Text.RegularExpressions
Imports System.Threading
Imports MySql.Data.MySqlClient
Imports Ionic.Zip

Public Module MysqlInterface
    Private WithEvents ProcessMySql As Process = New Process()
    Private _IsRunning As Boolean
    Private _MysqlCrashCounter As Integer
    Private _MysqlExited As Boolean

    Sub New()
        'nothing
    End Sub

    Public Property MysqlCrashCounter As Integer
        Get
            Return _MysqlCrashCounter
        End Get
        Set(value As Integer)
            _MysqlCrashCounter = value
        End Set
    End Property

    Public Property PropMysqlExited() As Boolean
        Get
            Return _MysqlExited
        End Get
        Set(ByVal Value As Boolean)
            _MysqlExited = Value
        End Set
    End Property

    Public Property IsRunning As Boolean
        Get
            Return _IsRunning
        End Get
        Set(value As Boolean)
            _IsRunning = value
        End Set
    End Property

#Region "Mysql"

    Public Function StartMySQL() As Boolean
        Log("INFO", "Checking Mysql")
        If MysqlInterface.IsMySqlRunning() Then
            MysqlInterface.IsRunning = True
            MySQLIcon(True)
            PropMysqlExited = False
            Log("INFO", "Mysql is running")
            Return True
        End If

        Log("INFO", "Mysql is not running")
        ' Build data folder if it does not exist
        MakeMysql()

        MySQLIcon(False)
        ' Start MySql in background.

        TextPrint(My.Resources.Mysql_Starting)

        ' SAVE INI file
        If Settings.LoadIni(IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\mysql\my.ini"), "#") Then Return True
        Settings.SetIni("mysqld", "basedir", """" & FormSetup.PropCurSlashDir & "/OutworldzFiles/Mysql" & """")
        Settings.SetIni("mysqld", "datadir", """" & FormSetup.PropCurSlashDir & "/OutworldzFiles/Mysql/Data" & """")
        Settings.SetIni("mysqld", "port", CStr(Settings.MySqlRobustDBPort))
        Settings.SetIni("client", "port", CStr(Settings.MySqlRobustDBPort))
        Settings.SaveINI(System.Text.Encoding.ASCII)

        ' create test program slants the other way:
        Dim testProgram As String = IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Mysql\bin\StartManually.bat")
        DeleteFile(testProgram)

        Try
            Using outputFile As New StreamWriter(testProgram, True)
                outputFile.WriteLine("@REM A program to run Mysql manually for troubleshooting." & vbCrLf _
                             & "mysqld.exe --defaults-file=" & """" & FormSetup.PropCurSlashDir & "/OutworldzFiles/mysql/my.ini" & """")
            End Using
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try

        CreateService()
        CreateStopMySql()

        Application.DoEvents()
        ' Mysql was not running, so lets start it up.
        Dim pi As ProcessStartInfo = New ProcessStartInfo With {
            .Arguments = "--defaults-file=" & """" & FormSetup.PropCurSlashDir & "/OutworldzFiles/mysql/my.ini" & """",
            .WindowStyle = ProcessWindowStyle.Hidden,
            .FileName = """" & IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\mysql\bin\mysqld.exe") & """"
        }
        ProcessMySql.StartInfo = pi
        ProcessMySql.EnableRaisingEvents = True
        Try
            ProcessMySql.Start()
            MysqlInterface.IsRunning = True
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try

        ' wait for MySql to come up
        Dim MysqlOk As Boolean
        Dim ctr As Integer = 0
        While Not MysqlOk

            Dim MysqlLog As String = IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\mysql\data")
            If ctr = 60 Then ' about 60 seconds when it fails

                Dim yesno = MsgBox(My.Resources.Mysql_Failed, MsgBoxStyle.YesNo Or MsgBoxStyle.MsgBoxSetForeground, Global.Outworldz.My.Resources.Error_word)
                If (yesno = vbYes) Then
                    Dim files As Array = Nothing
                    Try
                        files = Directory.GetFiles(MysqlLog, "*.err", SearchOption.TopDirectoryOnly)
                    Catch ex As Exception
                        BreakPoint.Show(ex.Message)
                    End Try

                    For Each FileName As String In files
                        Try
                            System.Diagnostics.Process.Start(IO.Path.Combine(Settings.CurrentDirectory, "baretail.exe"), """" & FileName & """")
                        Catch ex As Exception
                            BreakPoint.Show(ex.Message)
                        End Try
                        Application.DoEvents()
                    Next
                End If
                FormSetup.Buttons(FormSetup.StartButton)
                Return False
            End If
            ctr += 1
            ' check again
            Sleep(1000)
            Application.DoEvents()
            MysqlOk = MysqlInterface.IsMySqlRunning()
        End While

        If Not MysqlOk Then Return False

        UpgradeMysql()

        TextPrint(Global.Outworldz.My.Resources.Mysql_is_Running)
        MysqlInterface.IsRunning = True
        MySQLIcon(True)

        PropMysqlExited = False

        Return True

    End Function

#End Region

    Public Sub DeregisterRegions()

        If PropOpensimIsRunning Then
            MsgBox("Opensim is running. Cannot clear the list of registered regions", MsgBoxStyle.Information Or MsgBoxStyle.MsgBoxSetForeground)
            Return
        End If

        Dim Mysql = CheckPort("127.0.0.1", CType(Settings.MySqlRobustDBPort, Integer))
        If Mysql Then
            QueryString("delete from robust.regions;")
            TextPrint(My.Resources.Deregister_All)
        End If

    End Sub

    ''' <summary>Returns Estate Name give an Estate UUID</summary>
    ''' <param name="UUID"></param>
    ''' <returns>Name as string</returns>
    Public Function EstateName(UUID As String) As String

        If Settings.RegionMySqlConnection.Length = 0 Then Return ""

        'Debug.Print(Settings.RegionMySqlConnection)
        Dim name As String = ""
        Dim Val As String = ""

        Try
            Using MysqlConn As New MySqlConnection(Settings.RegionMySqlConnection)
                MysqlConn.Open()
                Dim stm = "Select EstateID from estate_map where regionid = @UUID"
                Using cmd As MySqlCommand = New MySqlCommand(stm, MysqlConn)
                    cmd.Parameters.AddWithValue("@UUID", UUID)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            'Debug.Print("ID = {0}", reader.GetString(0))
                            Val = reader.GetString(0)
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            Console.WriteLine("Error: " & ex.ToString())
            Return ""
        End Try

        Try
            Dim stm1 = "Select EstateName from estate_settings where EstateID = @ID"
            Using MysqlConn As New MySqlConnection(Settings.RegionMySqlConnection)
                MysqlConn.Open()
                Using cmd As MySqlCommand = New MySqlCommand(stm1, MysqlConn)
                    cmd.Parameters.AddWithValue("@ID", Val)
                    Using reader2 As MySqlDataReader = cmd.ExecuteReader()
                        If reader2.Read() Then
                            'Debug.Print("Name = {0}", reader2.GetString(0))
                            name = reader2.GetString(0)
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            Console.WriteLine("Error: " & ex.ToString())
            Return ""
        End Try
        Return name

    End Function

    Public Function GetAgentList() As Dictionary(Of String, String)

        Dim Dict As New Dictionary(Of String, String)
        If Settings.ServerType <> "Robust" Then Return Dict

        Using NewSQLConn As New MySqlConnection(Settings.RobustMysqlConnection)
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
            Catch ex As Exception
                Console.WriteLine("Error: " & ex.ToString())
            End Try
        End Using

        'If Debugger.IsAttached Then
        'Dict.Add("Test User", "Welcome")
        'End If

        Return Dict

    End Function

    Public Function GetHGAgentList() As Dictionary(Of String, String)

        '6f285c43-e656-42d9-b0e9-a78684fee15c;http://outworldz.com:9000/;Ferd Frederix
        Dim Dict As New Dictionary(Of String, String)

        Dim UserStmt = "SELECT UserID, LastRegionID from GridUser where online = 'true'"
        Dim pattern As String = "(.*?);.*;(.*)$"
        Dim Avatar As String
        Dim UUID As String
        Using NewSQLConn As New MySqlConnection(Settings.RobustMysqlConnection)
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
            Catch ex As Exception
                Console.WriteLine("Error: " & ex.ToString())
            End Try
        End Using

        'If Debugger.IsAttached Then
        'Dict.Add("Test User2", "Welcome")
        'End If

        Return Dict

    End Function

    Public Function IsMySqlRunning() As Boolean

        Dim Mysql As Boolean
        If (Settings.ServerType = "Robust") Then
            Mysql = CheckPort(Settings.RobustServer, Settings.MySqlRobustDBPort)
        Else
            Mysql = CheckPort(Settings.RegionServer, Settings.MySqlRegionDBPort)
        End If

        If Mysql Then
            Dim version = QueryString("SELECT VERSION()")
            'Debug.Print("MySQL version: " & version)
            IsRunning() = True
            Return True
        End If
        Return False

    End Function

    Public Sub MySQLIcon(Running As Boolean)

        If Not Running Then
            FormSetup.RestartMysqlIcon.Image = Global.Outworldz.My.Resources.nav_plain_red
        Else
            FormSetup.RestartMysqlIcon.Image = Global.Outworldz.My.Resources.check2
        End If
        Application.DoEvents()

    End Sub

    <CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")>
    Public Function QueryString(SQL As String) As String
        Using MysqlConn = New MySqlConnection(Settings.RobustMysqlConnection)
            Try
                MysqlConn.Open()
                Using cmd As MySqlCommand = New MySqlCommand(SQL, MysqlConn)
                    Dim v As String = Convert.ToString(cmd.ExecuteScalar(), Globalization.CultureInfo.InvariantCulture)
                    Return v
                End Using
            Catch ex As Exception
                Debug.Print(ex.Message)
            End Try
        End Using

        Return ""

    End Function

    Public Function WhereisAgent(agentName As String) As String

        Dim agents = GetAgentList()

        If agents.ContainsKey(agentName) Then
            Return PropRegionClass.FindRegionByName(agents.Item(agentName))
        End If

        Return ""

    End Function

    Private Sub CreateService()

        ' create test program slants the other way:
        Dim testProgram As String = IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Mysql\bin\InstallAsAService.bat")
        DeleteFile(testProgram)

        Try
            Using outputFile As New StreamWriter(testProgram, True)
                outputFile.WriteLine("@REM Program to run Mysql as a Service" & vbCrLf +
            "mysqld.exe --install Mysql --defaults-file=" & """" & FormSetup.PropCurSlashDir & "/OutworldzFiles/mysql/my.ini" & """" & vbCrLf & "net start Mysql" & vbCrLf)
            End Using
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try

    End Sub

    Private Sub CreateStopMySql()

        ' create test program slants the other way:
        Dim testProgram As String = IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Mysql\bin\StopMySQL.bat")
        DeleteFile(testProgram)
        Try
            Using outputFile As New StreamWriter(testProgram, True)
                outputFile.WriteLine("@REM Program to stop Mysql" & vbCrLf +
            "mysqladmin.exe -u root --port " & CStr(Settings.MySqlRobustDBPort) & " shutdown" & vbCrLf & "@pause" & vbCrLf)
            End Using
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try

    End Sub

    Private Function GetRegionName(UUID As String) As String
        Dim Val As String = ""
        Dim MysqlConn = New MySqlConnection(Settings.RobustMysqlConnection)
        Try
            MysqlConn.Open()
            Dim stm = "Select RegionName from regions where uuid = @UUID';"
            Using cmd As New MySqlCommand(stm, MysqlConn)
                cmd.Parameters.AddWithValue("@UUID", UUID)
                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        Debug.Print("Region Name = {0}", reader.GetString(0))
                        Val = reader.GetString(0)
                    End If
                End Using
            End Using
        Catch ex As Exception
            Console.WriteLine("Error: " & ex.ToString())
        Finally
            MysqlConn.Close()
        End Try

        Return Val

    End Function

    Private Sub MakeMysql()

        Dim fname As String = ""
        Dim m As String = IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Mysql\")
        If Not System.IO.File.Exists(m & "\Data\ibdata1") Then
            TextPrint(My.Resources.Create_DB)
            Try
                Using zip As ZipFile = New ZipFile(m & "\Blank-Mysql-Data-folder.zip")
                    Dim extractPath = Path.GetFullPath(Settings.CurrentDirectory) & "\OutworldzFiles\Mysql"
                    If (Not extractPath.EndsWith(Path.DirectorySeparatorChar.ToString(), StringComparison.Ordinal)) Then
                        extractPath += Path.DirectorySeparatorChar
                    End If
                    zip.ExtractAll(extractPath)
                End Using
            Catch ex As Exception
                TextPrint("Unable to extract file: " & fname & ":" & ex.Message)
                Thread.Sleep(3000)
                Application.DoEvents()
            End Try

        End If

    End Sub

    Private Sub Mysql_Exited(ByVal sender As Object, ByVal e As EventArgs) Handles ProcessMySql.Exited

        FormSetup.RestartMysqlIcon.Image = Global.Outworldz.My.Resources.nav_plain_red

        If PropAborting Then Return

        If Settings.RestartOnCrash And MysqlCrashCounter < 10 Then
            MysqlCrashCounter += 1
            PropMysqlExited = True
            Return
        End If
        MysqlCrashCounter = 0
        Dim MysqlLog As String = IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\mysql\data")
        Dim files As Array = Nothing
        Try
            files = Directory.GetFiles(MysqlLog, "*.err", SearchOption.TopDirectoryOnly)
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try

        If files.Length > 0 Then
            Dim yesno = MsgBox(My.Resources.MySql_Exited, MsgBoxStyle.YesNo Or MsgBoxStyle.MsgBoxSetForeground, Global.Outworldz.My.Resources.Error_word)
            If (yesno = vbYes) Then

                For Each FileName As String In files
                    Try
                        System.Diagnostics.Process.Start(IO.Path.Combine(Settings.CurrentDirectory, "baretail.exe"), """" & FileName & """")
                    Catch ex As Exception
                        BreakPoint.Show(ex.Message)
                    End Try
                Next
            End If
        Else
            PropAborting = True
            MsgBox(My.Resources.Error_word, MsgBoxStyle.Information Or MsgBoxStyle.MsgBoxSetForeground, Global.Outworldz.My.Resources.Error_word)
        End If

    End Sub

End Module
