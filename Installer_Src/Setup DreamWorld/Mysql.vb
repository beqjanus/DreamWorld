#Region "Copyright AGPL3.0"

' Copyright Outworldz, LLC.
' AGPL3.0  https://opensource.org/licenses/AGPL

#End Region

Imports System.Globalization
Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Threading
Imports Ionic.Zip
Imports MySql.Data.MySqlClient

Public Module MysqlInterface
    Private WithEvents ProcessMySql As Process = New Process()
    Private _IsRunning As Boolean
    Private _MysqlCrashCounter As Integer
    Private _MysqlExited As Boolean
    Private EstateConnection As MySqlConnection

    Sub New()
        'nothing
    End Sub

#Region "Properties"

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

#End Region

#Region "StartMysql"

    Public Function StartMySQL() As Boolean

        PropAborting = False

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

        ' SAVE INI file
        Dim INI = New LoadIni(IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\mysql\my.ini"), "#", System.Text.Encoding.ASCII)

        If Settings.MysqlRunasaService Then
            INI.SetIni("mysqld", "innodb_max_dirty_pages_pct", "75")
            INI.SetIni("mysqld", "innodb_flush_log_at_trx_commit", "2")
        Else
            ' when we are a service we can wait until we have 75 % of the buffer full before we flush
            ' if not, too dangerous, so we always write at 0% for ACID behavior.
            ' InnoDB tries to flush data from the buffer pool so that the percentage of dirty pages does Not exceed this value. The default value Is 75.
            ' The innodb_max_dirty_pages_pct setting establishes a target for flushing activity. It does Not affect the rate of flushing.

            INI.SetIni("mysqld", "innodb_max_dirty_pages_pct", "0")

            'If Set To 1, InnoDB will flush (fsync) the transaction logs To the
            ' disk at Each commit, which offers full ACID behavior. If you are
            ' willing To compromise this safety, And you are running small
            ' transactions, you may Set this To 0 Or 2 To reduce disk I/O To the
            ' logs. Value 0 means that the log Is only written To the log file And
            ' the log file flushed To disk approximately once per second. Value 2
            ' means the log Is written To the log file at Each commit, but the log
            ' file Is only flushed To disk approximately once per second.

            INI.SetIni("mysqld", "innodb_flush_log_at_trx_commit", "1")
        End If

        INI.SetIni("mysqld", "basedir", $"""{FormSetup.PropCurSlashDir}/OutworldzFiles/Mysql""")
        INI.SetIni("mysqld", "datadir", $"""{FormSetup.PropCurSlashDir}/OutworldzFiles/Mysql/Data""")
        INI.SetIni("mysqld", "port", CStr(Settings.MySqlRobustDBPort))
        INI.SetIni("client", "port", CStr(Settings.MySqlRobustDBPort))

        INI.SaveINI()

        ' create test program slants the other way:
        Dim testProgram As String = IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Mysql\bin\StartManually.bat")
        DeleteFile(testProgram)

        Try
            Using outputFile As New StreamWriter(testProgram, True)
                outputFile.WriteLine("@REM A program to run Mysql manually for troubleshooting." & vbCrLf _
                             & "mysqld.exe --defaults-file=" &
                             """" & FormSetup.PropCurSlashDir & "/OutworldzFiles/mysql/my.ini" & """"
                             )
            End Using
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try

        CreateService()
        CreateStopMySql()

        If Settings.MysqlRunasaService Then

            If Settings.CurrentDirectory <> Settings.MysqlLastDirectory Or Not ServiceExists("MySQLDreamGrid") Then
                Using MysqlProcess As New Process With {
                .EnableRaisingEvents = False
            }
                    MysqlProcess.StartInfo.UseShellExecute = True ' so we can redirect streams
                    MysqlProcess.StartInfo.FileName = IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Mysql\bin\mysqld.exe")
                    MysqlProcess.StartInfo.Arguments = $"--install MySQLDreamGrid --defaults-file=""{FormSetup.PropCurSlashDir}/OutworldzFiles/mysql/my.ini"""
                    MysqlProcess.StartInfo.CreateNoWindow = True
                    MysqlProcess.StartInfo.WorkingDirectory = IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Mysql\bin\")
                    MysqlProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden

                    Try
                        MysqlProcess.Start()
                        MysqlProcess.WaitForExit()
                    Catch ex As Exception
                        BreakPoint.Show(ex.Message)
                        MySQLIcon(False)
                    End Try
                    Application.DoEvents()

                    If MysqlProcess.ExitCode <> 0 Then
                        TextPrint(My.Resources.Mysql_Failed)
                        MySQLIcon(False)
                        Return False
                    Else
                        Settings.MysqlLastDirectory = Settings.CurrentDirectory
                        Settings.SaveSettings()
                    End If

                    Application.DoEvents()
                End Using

            End If

            TextPrint(My.Resources.Mysql_Starting)

            Using MysqlProcess As New Process With {
                        .EnableRaisingEvents = False
                    }
                MysqlProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
                MysqlProcess.StartInfo.FileName = "net"
                MysqlProcess.StartInfo.Arguments = "start MySQLDreamGrid"
                MysqlProcess.StartInfo.UseShellExecute = False
                MysqlProcess.StartInfo.CreateNoWindow = True
                MysqlProcess.StartInfo.RedirectStandardError = True
                MysqlProcess.StartInfo.RedirectStandardOutput = True
                Dim response As String = ""

                Try
                    MysqlProcess.Start()
                    response = MysqlProcess.StandardOutput.ReadToEnd() & MysqlProcess.StandardError.ReadToEnd()
                    MysqlProcess.WaitForExit()
                Catch ex As Exception
                    BreakPoint.Show(ex.Message)
                    TextPrint(My.Resources.Mysql_Failed & ":" & ex.Message)
                End Try
                Application.DoEvents()

                If MysqlProcess.ExitCode <> 0 Then
                    If response.Contains("has already been started") Then
                        TextPrint(My.Resources.Mysql_is_Running & ":" & Settings.MySqlRobustDBPort)
                        MySQLIcon(True)
                        Return True
                    End If
                    TextPrint(My.Resources.Mysql_Failed & ":" & response)
                    MySQLIcon(False)
                    Return False
                Else
                    TextPrint(My.Resources.Mysql_is_Running & ":" & Settings.MySqlRobustDBPort)
                    MySQLIcon(True)
                End If

            End Using
        Else

            Application.DoEvents()
            ' Mysql was not running, so lets start it up.
            Dim pi As ProcessStartInfo = New ProcessStartInfo With {
                .Arguments = $"--defaults-file=""{FormSetup.PropCurSlashDir}/OutworldzFiles/mysql/my.ini""",
                .WindowStyle = ProcessWindowStyle.Hidden,
                .CreateNoWindow = True,
                .FileName = $"""{IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\mysql\bin\mysqld.exe")}"""
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

        End If

        UpgradeMysql()

        TextPrint(Global.Outworldz.My.Resources.Mysql_is_Running)
        MysqlInterface.IsRunning = True
        MySQLIcon(True)

        PropMysqlExited = False

        Return True

    End Function

#End Region

#Region "Public"

    Public Function AssetCount(UUID As String) As Integer

        Try
            Using MysqlConn As New MySqlConnection(Settings.RobustMysqlConnection)
                MysqlConn.Open()
                Dim stm = "select count(*) from inventoryitems where avatarid = @UUID"
                Using cmd As MySqlCommand = New MySqlCommand(stm, MysqlConn)
                    cmd.Parameters.AddWithValue("@UUID", UUID)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            'Debug.Print("ID = {0}", reader.GetString(0))
                            Return reader.GetInt32(0)
                        End If
                    End Using
                End Using
            End Using
        Catch
        End Try
        Return 0

    End Function

    Public Function DeleteOpensimEstateID(UUID As String) As Integer

        If Not IsMySqlRunning() Then Return 0

        EstateConnection = New MySqlConnection(Settings.RegionMySqlConnection)
        EstateConnection.Open()

        Try
            Using EstateConnection
                Dim stm = "delete from opensim.estate_map where RegionID=@UUID"
                Using cmd As MySqlCommand = New MySqlCommand(stm, EstateConnection)
                    cmd.Parameters.AddWithValue("@UUID", UUID)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try
        Return 0

    End Function

    Public Sub DeRegisterPosition(X As Integer, Y As Integer)
        Try
            Using MysqlConn As New MySqlConnection(Settings.RobustMysqlConnection)
                MysqlConn.Open()
                Dim stm = "delete from robust.regions where LocX=@X and LocY=@Y"
                Using cmd As MySqlCommand = New MySqlCommand(stm, MysqlConn)
                    cmd.Parameters.AddWithValue("@X", X * 256)
                    cmd.Parameters.AddWithValue("@Y", Y * 256)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch
        End Try

    End Sub

    ''' <summary>
    ''' deletes all regions from robust.regions
    ''' </summary>
    ''' <param name="force"></param>
    Public Sub DeregisterRegions(force As Boolean)

        If PropOpensimIsRunning And Not force Then
            MsgBox("Opensim is running. Cannot clear the list of registered regions", MsgBoxStyle.Information Or MsgBoxStyle.MsgBoxSetForeground)
            Return
        End If

        Dim Mysql = CheckPort("127.0.0.1", CType(Settings.MySqlRobustDBPort, Integer))
        If Mysql Then
            QueryString("delete from robust.regions;")
            TextPrint(My.Resources.Deregister_All)
        End If

    End Sub

    ''' <summary>
    ''' deletes one region from robust.regions
    ''' </summary>
    ''' <param name="UUID">UUID of region</param>
    Public Sub DeregisterRegionUUID(RegionUUID As String)

        Try
            Using MysqlConn As New MySqlConnection(Settings.RobustMysqlConnection)
                MysqlConn.Open()
                Dim stm = "delete from robust.regions where uuid = @UUID;"
                Using cmd As MySqlCommand = New MySqlCommand(stm, MysqlConn)
                    cmd.Parameters.AddWithValue("@UUID", RegionUUID)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try

    End Sub

    Public Function EstateID(UUID As String) As Integer

        If Not IsMySqlRunning() Then Return 0

        EstateConnection = New MySqlConnection(Settings.RegionMySqlConnection)
        EstateConnection.Open()

        Try
            Using EstateConnection
                Dim stm = "select EstateID from opensim.estate_map where RegionID=@UUID"
                Using cmd As MySqlCommand = New MySqlCommand(stm, EstateConnection)
                    cmd.Parameters.AddWithValue("@UUID", UUID)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            Return CInt(reader.GetInt16("EstateID"))
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try
        Return 0

    End Function

    ''' <summary>Returns Estate Name give an Estate UUID</summary>
    ''' <param name="UUID">Region UUID</param>
    ''' <returns>Estate Name as string</returns>
    Public Function EstateName(UUID As String) As String

        Dim name As String = ""

        If MysqlInterface.IsRunning() Then

            Dim Val As String = ""
            Try
                EstateConnection = New MySqlConnection(Settings.RegionMySqlConnection)
                EstateConnection.Open()

                Using EstateConnection
                    Dim stm = "SELECT estate_settings.EstateName FROM estate_settings estate_settings INNER JOIN estate_map estate_map ON (estate_settings.EstateID = estate_map.EstateID) where regionid = @UUID"
                    Using cmd As MySqlCommand = New MySqlCommand(stm, EstateConnection)
                        cmd.Parameters.AddWithValue("@UUID", UUID)
                        Using reader As MySqlDataReader = cmd.ExecuteReader()
                            If reader.Read() Then
                                'Debug.Print("ID = {0}", reader.GetString(0))
                                Val = reader.GetString(0)
                                Return Val
                            Else
                                Return ""
                            End If
                        End Using
                    End Using
                End Using
            Catch ex As Exception
                Return ""
            End Try
        End If
        Return name
    End Function

    ''' <summary>
    ''' Gets users from useraccounts
    ''' </summary>
    ''' <returns>dictionary of Firstname + Lastname, Region Name</returns>
    Public Function GetAgentList() As Dictionary(Of String, String)

        Dim Dict As New Dictionary(Of String, String)
        If Settings.ServerType <> RobustServerName Then Return Dict
        Try
            Using NewSQLConn As New MySqlConnection(Settings.RobustMysqlConnection)
                Dim stm As String = "SELECT useraccounts.FirstName, useraccounts.LastName, regions.regionName FROM (presence INNER JOIN useraccounts ON presence.UserID = useraccounts.PrincipalID) INNER JOIN regions  ON presence.RegionID = regions.uuid;"
                NewSQLConn.Open()
                Using cmd As New MySqlCommand(stm, NewSQLConn)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            ' Debug.Print(reader.GetString(0) & " " & reader.GetString(1) & " in region " & reader.GetString(2))
                            Dict.Add(reader.GetString(0) & " " & reader.GetString(1), reader.GetString(2))
                        End While
                    End Using
                End Using

            End Using
        Catch ex As Exception
            Console.WriteLine("Error: " & ex.ToString())
        End Try

        'If Debugger.IsAttached Then
        'Dict.Add("Ferd Frederix", "SS")
        'End If

        Return Dict

    End Function

    ''' <summary>
    ''' return List of all users
    ''' </summary>
    ''' <returns> auto complete collection</returns>
    Public Function GetAvatarList() As AutoCompleteStringCollection
        Dim A As New AutoCompleteStringCollection
        Try
            Using MysqlConn As New MySqlConnection(Settings.RobustMysqlConnection)
                MysqlConn.Open()
                Dim stm = "Select firstname, lastname from useraccounts"
                Using cmd As MySqlCommand = New MySqlCommand(stm, MysqlConn)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            Dim f = reader.GetString(0)
                            Dim l = reader.GetString(1)
                            '  Debug.Print("ID = {0} {1}", f, l)
                            If f <> "GRID" And l <> "SERVICES" Then
                                A.Add(f & " " & l)
                            End If
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
        End Try

        Return A

    End Function

    ''' <summary>
    ''' Returns a local avatar UUID give a First and Last name
    ''' </summary>
    ''' <param name="avatarName"></param>
    ''' <returns>Avatar UUID</returns>
    Public Function GetAviUUUD(AvatarName As String) As String

        Try
            Dim parts As String() = AvatarName.Split(" ".ToCharArray())
            Dim Fname = parts(0).Trim
            Dim LName = parts(1).Trim

            Using MysqlConn As New MySqlConnection(Settings.RobustMysqlConnection)
                MysqlConn.Open()
                Dim stm = "Select PrincipalID  from useraccounts where FirstName= @Fname and LastName=@LName "
                Using cmd As MySqlCommand = New MySqlCommand(stm, MysqlConn)
                    cmd.Parameters.AddWithValue("@Fname", Fname)
                    cmd.Parameters.AddWithValue("@Lname", LName)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            'Debug.Print("ID = {0}", reader.GetString(0))
                            Dim Val = reader.GetString(0)
                            Return Val
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
        End Try
        Return ""

    End Function

    Public Function GetEmailList() As Dictionary(Of String, String)
        Dim A As New Dictionary(Of String, String)
        Try
            Using MysqlConn As New MySqlConnection(Settings.RobustMysqlConnection)
                MysqlConn.Open()
                Dim stm = "Select firstname, lastname , email, principalid, userlevel, created from useraccounts"
                Using cmd As MySqlCommand = New MySqlCommand(stm, MysqlConn)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            Dim f = reader.GetString(0)
                            Dim l = reader.GetString(1)
                            Dim e = reader.GetString(2)
                            Dim u = reader.GetString(3)
                            Dim Level = reader.GetString(4)
                            Dim c As Double = reader.GetDouble(5)
                            Dim Birthdate As DateTime = UnixTimeStampToDateTime(c)
                            Dim Age = DateDiff(DateInterval.Day, Birthdate, DateTime.Now)

                            '     Debug.Print("{0} {1} {2}", f, l, e)
                            If f <> "GRID" And l <> "SERVICES" Then
                                A.Add(f & " " & l, e & "|" & u & "|" & Level & "|" & Birthdate.ToString(CultureInfo.CurrentCulture) & "|" & Age)
                            End If
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try
        Debug.Print("Users " & CStr(A.Count))

        Return A

    End Function

    Public Function GetHGAgentList() As Dictionary(Of String, String)

        '6f285c43-e656-42d9-b0e9-a78684fee15c;http://outworldz.com:9000/;Ferd Frederix
        Dim Dict As New Dictionary(Of String, String)

        Dim UserStmt = "SELECT UserID, LastRegionID from GridUser where online = 'true'"
        Dim pattern As String = "(.*?);.*;(.*)$"
        Dim Avatar As String
        Dim UUID As String
        Try
            Using NewSQLConn As New MySqlConnection(Settings.RobustMysqlConnection)
                NewSQLConn.Open()
                Using cmd As MySqlCommand = New MySqlCommand(UserStmt, NewSQLConn)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()

                        While reader.Read()
                            ' Debug.Print(reader.GetString(0))
                            Dim LongName = reader.GetString(0)
                            UUID = reader.GetString(1)
                            For Each m In Regex.Matches(LongName, pattern)
                                ' Debug.Print("Avatar {0}", m.Groups(2).Value)
                                ' Debug.Print("Region UUID {0}", m.Groups(1).Value)
                                Avatar = m.Groups(2).Value.ToString
                                If UUID <> "00000000-0000-0000-0000-000000000000" Then
                                    Dict.Add(Avatar, GetRegionName(UUID))
                                End If
                            Next
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            Console.WriteLine("Error: " & ex.ToString())
        End Try

        Return Dict

    End Function

    ''' <summary>
    ''' Number of prims in this region
    ''' </summary>
    ''' <param name="UUID">Region UUID</param>
    ''' <returns>integer primcount</returns>
    Public Function GetPrimCount(UUID As String) As Integer

        If Not IsMySqlRunning() Then StartMySQL()

        Dim count As Integer
        Try
            Using MysqlConn As New MySqlConnection(Settings.RegionMySqlConnection)
                MysqlConn.Open()
                Dim stm = "select count(*) from prims where regionuuid = @UUID"
                Using cmd As MySqlCommand = New MySqlCommand(stm, MysqlConn)
                    cmd.Parameters.AddWithValue("@UUID", UUID)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            count = CInt(reader.GetString(0))
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try
        Return count

    End Function

    Public Function GetRegionFromAgentID(AgentID As String) As String

        If Settings.ServerType <> RobustServerName Then Return ""
        Try
            Using NewSQLConn As New MySqlConnection(Settings.RobustMysqlConnection)
                NewSQLConn.Open()
                Dim stm As String = "SELECT presence.RegionID FROM presence where presence.userid = @ID;"
                Using cmd As MySqlCommand = New MySqlCommand(stm, NewSQLConn)
                    cmd.Parameters.AddWithValue("@ID", AgentID)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            Return reader.GetString(0)
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            BreakPoint.Show("Error: " & ex.ToString())
        End Try

        Return ""

    End Function

    Public Function IsAgentInRegion(RegionUUID As String) As Boolean

        If Settings.ServerType <> RobustServerName Then Return False

        Dim UserStmt = "SELECT LastRegionID from GridUser where online = 'True' and LastRegionID = @R;  "
        Application.DoEvents()

        Try
            Using NewSQLConn As New MySqlConnection(Settings.RobustMysqlConnection)
                NewSQLConn.Open()
                Using cmd As MySqlCommand = New MySqlCommand(UserStmt, NewSQLConn)
                    cmd.Parameters.AddWithValue("@R", RegionUUID)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            Return True
                        End While
                    End Using
                End Using

                Dim stm As String = "SELECT count(*) FROM presence  INNER JOIN regions ON presence.RegionID = regions.uuid where regions.uuid = @R ;"
                Using cmd As New MySqlCommand(stm, NewSQLConn)
                    cmd.Parameters.AddWithValue("@R", RegionUUID)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            Dim c = CInt("0" & reader.GetString(0))
                            Return c > 0
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            BreakPoint.Show("Error: " & ex.ToString())
        End Try
        Return False

    End Function

    Public Function IsMySqlRunning() As Boolean

        Dim version = QueryString("SELECT VERSION()")
        If version.Length > 0 Then
            IsRunning() = True
            MySQLIcon(True)
            Return True
        End If

        MySQLIcon(False)
        Return False

    End Function

    Public Sub MysqlConsole()

        StartMySQL()

        Using p As Process = New Process()
            Dim pi As ProcessStartInfo = New ProcessStartInfo With {
                .Arguments = $" -u root --port={Settings.MySqlRegionDBPort}",
                .FileName = """" & IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\mysql\bin\mysql.exe") & """",
                .UseShellExecute = True, ' so we can redirect streams and minimize
                .WindowStyle = ProcessWindowStyle.Normal
            }
            p.StartInfo = pi
            Try
                p.Start()
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
            End Try
        End Using
    End Sub

    Public Function MysqlGetPartner(p1 As String, mysetting As MySettings) As String

        If mysetting Is Nothing Then
            Return ""
        End If
        Try
            Dim answer As String = ""
            Using myConnection As MySqlConnection = New MySqlConnection(mysetting.RobustMysqlConnection)
                myConnection.Open()
                Dim Query1 = "Select profilePartner from userprofile where userUUID=@p1;"
                Using myCommand1 As MySqlCommand = New MySqlCommand(Query1) With {
                    .Connection = myConnection
                   }

                    myCommand1.Parameters.AddWithValue("@p1", p1)
                    answer = CStr(myCommand1.ExecuteScalar())
                    ' Debug.Print($"User={p1}, Partner={answer}")
                    If answer Is Nothing Then
                        Return "00000000-0000-0000-0000-000000000000"
                    End If
                    Return answer
                End Using
            End Using
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try

        Return ""

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
        Dim MysqlConn As MySqlConnection
        Try
            If (Settings.ServerType = RobustServerName) Then
                MysqlConn = New MySqlConnection(Settings.RobustMysqlConnection)
            Else
                MysqlConn = New MySqlConnection(Settings.RegionMySqlConnection)
            End If
            Using MysqlConn
                MysqlConn.Open()
                Using cmd As MySqlCommand = New MySqlCommand(SQL, MysqlConn)
                    Dim v As String = Convert.ToString(cmd.ExecuteScalar(), Globalization.CultureInfo.InvariantCulture)
                    Return v
                End Using
            End Using
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try
        Return ""

    End Function

    '' !!! Deprecated
    ''' <summary>
    ''' Returns boolean if a region exists in the regions table
    ''' </summary>
    ''' <param name="UUID">Region UUID</param>
    ''' <returns>True is region is in table</returns>
    Public Function RegionIsRegistered(UUID As String) As Boolean

        Dim count As Integer
        Try
            Using MysqlConn As New MySqlConnection(Settings.RobustMysqlConnection)
                MysqlConn.Open()
                Dim stm = "Select count(*) as cnt from robust.regions where uuid = @UUID"
                Using cmd As MySqlCommand = New MySqlCommand(stm, MysqlConn)
                    cmd.Parameters.AddWithValue("@UUID", UUID)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            count = CInt(reader.GetInt16("cnt"))
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try
        Return CBool(count)

    End Function

    ''' <summary>
    ''' Returns boolean if a region exists in the regions table
    ''' </summary>
    ''' <param name="UUID">Region UUID</param>
    ''' <returns>True is region is in table</returns>
    Public Function RegionIsRegisteredOnline(UUID As String) As Boolean

        Dim count As Integer
        Try
            Using MysqlConn As New MySqlConnection(Settings.RobustMysqlConnection)
                MysqlConn.Open()
                Dim stm = "Select count(*) as cnt from robust.regions where uuid = @UUID and flags & 4 = 4 "
                Using cmd As MySqlCommand = New MySqlCommand(stm, MysqlConn)
                    cmd.Parameters.AddWithValue("@UUID", UUID)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            count = CInt(reader.GetInt16("cnt"))
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try
        Return CBool(count)

    End Function

    Public Sub SetEstate(UUID As String, EstateID As Integer)

        If Not IsMySqlRunning() Then Return
        Dim exists As Boolean

        Try
            Using MysqlConn As New MySqlConnection(Settings.RegionMySqlConnection)
                MysqlConn.Open()
                Dim stm = "Select EstateID from estate_map where regionid = @UUID"
                Using cmd As MySqlCommand = New MySqlCommand(stm, MysqlConn)
                    cmd.Parameters.AddWithValue("@UUID", UUID)
                    Try
                        Using reader As MySqlDataReader = cmd.ExecuteReader()
                            If reader.Read() Then
                                exists = True
                            End If
                        End Using
                    Catch ex As Exception
                        BreakPoint.Show(ex.Message)
                    End Try
                End Using
            End Using

            If Not exists Then
                Try
                    Using MysqlConn1 As New MySqlConnection(Settings.RegionMySqlConnection)
                        MysqlConn1.Open()
                        Dim stm1 = "insert into EstateMap (RegionID, EstateID) values (@UUID, @EID)"
                        Try
                            Using cmd1 As MySqlCommand = New MySqlCommand(stm1, MysqlConn1)
                                cmd1.Parameters.AddWithValue("@UUID", UUID)
                                cmd1.Parameters.AddWithValue("@EID", EstateID)
                                cmd1.BeginExecuteNonQuery()
                            End Using
                        Catch ex As Exception
                            BreakPoint.Show(ex.Message)
                        End Try
                    End Using
                Catch ex As Exception
                    BreakPoint.Show(ex.Message)
                End Try
            End If
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try

    End Sub

    Public Sub SetupLocalSearch()

        If Settings.ServerType <> "Robust" Then Return

        ' modify this to migrate search database upwards a rev
        If Not Settings.SearchMigration = 3 Then

            MysqlInterface.DeleteSearchDatabase()

            TextPrint(My.Resources.Setup_search)
            Dim pi As ProcessStartInfo = New ProcessStartInfo()

            FileIO.FileSystem.CurrentDirectory = IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\mysql\bin")
            pi.FileName = "Create_OsSearch.bat"
            pi.UseShellExecute = True
            pi.CreateNoWindow = False
            pi.WindowStyle = ProcessWindowStyle.Hidden
            Using ProcessMysql As Process = New Process With {
                    .StartInfo = pi
                }

                Try
                    ProcessMysql.Start()
                    ProcessMysql.WaitForExit()
                Catch ex As InvalidOperationException
                    ErrorLog("Error ProcessMysql failed to launch: " & ex.Message)
                    FileIO.FileSystem.CurrentDirectory = Settings.CurrentDirectory
                    Return
                Catch ex As System.ComponentModel.Win32Exception
                    ErrorLog("Error ProcessMysql failed to launch: " & ex.Message)
                    FileIO.FileSystem.CurrentDirectory = Settings.CurrentDirectory
                    Return
                End Try
            End Using

            FileIO.FileSystem.CurrentDirectory = Settings.CurrentDirectory

            Settings.SearchMigration = 3
            Settings.SaveSettings()

        End If

    End Sub

    Public Sub SetupMutelist()

        Dim pi As ProcessStartInfo = New ProcessStartInfo With {
                .FileName = "Create_Mutelist.bat",
                .UseShellExecute = True,
                .CreateNoWindow = True,
                .WindowStyle = ProcessWindowStyle.Minimized,
                .WorkingDirectory = IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\mysql\bin\")
            }
        Using Mutelist As Process = New Process With {
                .StartInfo = pi
            }

            Try
                Mutelist.Start()
                Mutelist.WaitForExit()
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
                ErrorLog("Could not create Mutelist Database: " & ex.Message)
                FileIO.FileSystem.CurrentDirectory = Settings.CurrentDirectory
                Return
            End Try
        End Using

    End Sub

    Public Sub SetupSimStats()

        Dim pi As ProcessStartInfo = New ProcessStartInfo With {
                .FileName = "Create_Simstats.bat",
                .UseShellExecute = True,
                .CreateNoWindow = True,
                .WindowStyle = ProcessWindowStyle.Minimized,
                .WorkingDirectory = IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\mysql\bin\")
            }
        Using Mutelist As Process = New Process With {
                .StartInfo = pi
            }

            Try
                Mutelist.Start()
                Mutelist.WaitForExit()
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
                ErrorLog("Could not create SimStats Database: " & ex.Message)
                FileIO.FileSystem.CurrentDirectory = Settings.CurrentDirectory
                Return
            End Try
        End Using

    End Sub

    Public Sub SetupWordPress()

        Dim pi As ProcessStartInfo = New ProcessStartInfo With {
            .FileName = "Create_WordPress.bat",
            .UseShellExecute = True,
            .CreateNoWindow = True,
            .WindowStyle = ProcessWindowStyle.Minimized,
            .WorkingDirectory = IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\mysql\bin")
        }
        Using MysqlWordpress As Process = New Process With {
            .StartInfo = pi
        }

            Try
                MysqlWordpress.Start()
                MysqlWordpress.WaitForExit()
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
                ErrorLog("Could not create WordPress Database: " & ex.Message)
                FileIO.FileSystem.CurrentDirectory = Settings.CurrentDirectory
                Return
            End Try
        End Using

    End Sub

    Public Function UnixTimeStampToDateTime(unixTimeStamp As Double) As DateTime

        ' Unix time stamp Is seconds past epoch
        Dim dtDateTime As System.DateTime = New DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc)
        dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime()
        Return dtDateTime

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

    Private Sub DeleteSearchDatabase()

        If Not IsMySqlRunning() Then Return

        Try
            Using MysqlConn As New MySqlConnection(Settings.RobustMysqlConnection)
                MysqlConn.Open()
                Dim stm = "drop database ossearch"
                Using cmd As MySqlCommand = New MySqlCommand(stm, MysqlConn)
                    Try
                        cmd.ExecuteNonQuery()
                    Catch ex As Exception
                        BreakPoint.Show(ex.Message)
                    End Try
                End Using
            End Using
        Catch
        End Try

    End Sub

    Private Function GetRegionName(UUID As String) As String
        Dim Val As String = ""
        Using MysqlConn = New MySqlConnection(Settings.RobustMysqlConnection)
            Try
                MysqlConn.Open()
                Dim stm = "Select RegionName from regions where uuid=@UUID;"
                Using cmd As New MySqlCommand(stm, MysqlConn)
                    cmd.Parameters.AddWithValue("@UUID", UUID)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            ' Debug.Print("Region Name = {0}", reader.GetString(0))
                            Val = reader.GetString(0)
                        End If
                    End Using
                End Using
            Catch ex As Exception
                Console.WriteLine("Error: " & ex.ToString())
            End Try
        End Using

        Return Val

    End Function

    Private Sub MakeMysql()

        Dim fname As String = ""
        Dim m As String = IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Mysql")
        If Not System.IO.File.Exists(IO.Path.Combine(m, "Data\ibdata1")) Then
            TextPrint(My.Resources.Create_DB)
            Try
                Using zip As ZipFile = New ZipFile(IO.Path.Combine(m, "Blank-Mysql-Data-folder.zip"))
                    zip.UseZip64WhenSaving = Zip64Option.AsNecessary
                    Dim extractPath = $"{Path.GetFullPath(Settings.CurrentDirectory)}\OutworldzFiles\Mysql"
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

        If files IsNot Nothing Then
            Dim yesno = MsgBox(My.Resources.MySql_Exited, MsgBoxStyle.YesNo Or MsgBoxStyle.MsgBoxSetForeground, Global.Outworldz.My.Resources.Error_word)
            If (yesno = vbYes) Then

                For Each FileName As String In files
                    Try
                        System.Diagnostics.Process.Start(IO.Path.Combine(Settings.CurrentDirectory, "baretail.exe"), $"""{FileName}""")
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

#End Region

#Region "Visitors"

    ''' <summary>
    ''' Is not used as the necessary functions to locate the avatar X and Y do not exist yet
    ''' </summary>
    ''' <param name="AvatarName"></param>
    ''' <param name="RegionName"></param>
    ''' <param name="LocX"></param>
    ''' <param name="LocY"></param>
    Public Sub VisitorCount()

        If FormSetup.Visitor.Count > 0 Then
            Try
                Using MysqlConn1 As New MySqlConnection(Settings.RegionMySqlConnection)
                    MysqlConn1.Open()
                    For Each Visit As KeyValuePair(Of String, String) In FormSetup.Visitor

                        Dim Avatar = Visit.Key
                        Dim RegionName = Visit.Value
                        Dim RegionUUID = PropRegionClass.FindRegionUUIDByName(RegionName)
                        Dim result As AvatarData = RPC_admin_get_agent_list(RegionUUID)

                        If result IsNot Nothing Then

                            Dim stm1 = "insert into Robust.visitor (name, regionname, locationX, locationY) values (@NAME, @REGIONNAME, @LOCX, @LOCY)"
                            Try
#Disable Warning CA2100 ' Review SQL queries for security vulnerabilities
                                Using cmd1 As MySqlCommand = New MySqlCommand(stm1, MysqlConn1)
#Enable Warning CA2100 ' Review SQL queries for security vulnerabilities
                                    cmd1.Parameters.AddWithValue("@NAME", Avatar)
                                    cmd1.Parameters.AddWithValue("@REGIONNAME", RegionName)
                                    cmd1.Parameters.AddWithValue("@LOCX", result.X)
                                    cmd1.Parameters.AddWithValue("@LOCY", result.Y)
                                    cmd1.BeginExecuteNonQuery()
                                End Using
                            Catch ex As Exception
                                BreakPoint.Show(ex.Message)
                            End Try
                        End If
                    Next
                End Using
            Catch ex As Exception
            End Try

        End If
    End Sub

#End Region

End Module
