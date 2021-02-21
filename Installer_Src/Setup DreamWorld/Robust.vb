#Region "Copyright AGPL3.0"

' Copyright Outworldz, LLC.
' AGPL3.0  https://opensource.org/licenses/AGPL

#End Region

Imports System.Net
Imports System.Text.RegularExpressions

Module Robust

    Private WithEvents RobustProcess As New Process()
    Private _RestartRobust As Boolean
    Private _RobustCrashCounter As Integer
    Private _RobustExited As Boolean
    Private _RobustIconStarting As Boolean
    Private _RobustProcID As Integer

    Public Property PropRestartRobust As Boolean
        Get
            Return _RestartRobust
        End Get
        Set(value As Boolean)
            _RestartRobust = value
        End Set
    End Property

    Public Property PropRobustExited() As Boolean
        Get
            Return _RobustExited
        End Get
        Set(ByVal Value As Boolean)
            _RobustExited = Value
        End Set
    End Property

    Public Property PropRobustProcID As Integer
        Get
            Return _RobustProcID
        End Get
        Set(value As Integer)
            _RobustProcID = value
        End Set
    End Property

    Public Property RobustCrashCounter As Integer
        Get
            Return _RobustCrashCounter
        End Get
        Set(value As Integer)
            _RobustCrashCounter = value
        End Set
    End Property

    Public Property RobustIsStarting As Boolean
        Get
            Return _RobustIconStarting
        End Get
        Set(value As Boolean)
            _RobustIconStarting = value
        End Set
    End Property

#Region "Robust"

    Public Function ChooseRegion(Optional JustRunning As Boolean = False) As String

        ' Show testDialog as a modal dialog and determine if DialogResult = OK.
        Dim chosen As String = ""
        Using Chooseform As New FormChooser ' form for choosing a set of regions
            Chooseform.FillGrid("Region", JustRunning)  ' populate the grid with either Group or RegionName
            Dim ret = Chooseform.ShowDialog()
            If ret = DialogResult.Cancel Then Return ""
            Try
                ' Read the chosen sim name
                chosen = Chooseform.DataGridView.CurrentCell.Value.ToString()
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
                ErrorLog("Warn: Could not choose a displayed region. " & ex.Message)
            End Try
        End Using
        Return chosen

    End Function

    Public Sub RobustIcon(Running As Boolean)

        If Not Running Then
            FormSetup.RestartRobustIcon.Image = Global.Outworldz.My.Resources.nav_plain_red
        Else
            FormSetup.RestartRobustIcon.Image = Global.Outworldz.My.Resources.check2
        End If
        Application.DoEvents()

    End Sub

    Public Function StartRobust() As Boolean

        If Not StartMySQL() Then Return False ' prerequsite

        ' prevent recursion
        Dim ctr = 300
        While RobustIsStarting And ctr > 0
            Sleep(1000)
            ctr -= 1
        End While

        For Each p In Process.GetProcessesByName("Robust")
            Try
                If p.MainWindowTitle = RobustName() Then
                    PropRobustProcID = p.Id
                    Log(My.Resources.Info_word, Global.Outworldz.My.Resources.DosBoxRunning)
                    RobustIcon(True)
                    p.EnableRaisingEvents = True
                    AddHandler p.Exited, AddressOf RobustProcess_Exited
                    Return True
                End If
            Catch ex As Exception
            End Try
        Next

        ' Check the HTTP port
        If IsRobustRunning() Then
            RobustIcon(True)
            Return True
        End If

        If Settings.ServerType <> RobustServer Then
            RobustIcon(True)
            Return True
        End If

        RobustIsStarting = True

        PropRobustProcID = 0

        If DoRobust() Then Return False

        TextPrint("Robust " & Global.Outworldz.My.Resources.Starting_word)

        RobustProcess.EnableRaisingEvents = True
        RobustProcess.StartInfo.UseShellExecute = True
        RobustProcess.StartInfo.Arguments = "-inifile Robust.HG.ini"

        RobustProcess.StartInfo.FileName = Settings.OpensimBinPath & "robust.exe"
        RobustProcess.StartInfo.CreateNoWindow = False
        RobustProcess.StartInfo.WorkingDirectory = Settings.OpensimBinPath

        Select Case Settings.ConsoleShow
            Case "True"
                RobustProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal
            Case "False"
                RobustProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal
            Case "None"
                RobustProcess.StartInfo.WindowStyle = ProcessWindowStyle.Minimized
        End Select

        Try
            RobustProcess.Start()
            Log(My.Resources.Info_word, Global.Outworldz.My.Resources.Robust_running)
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
            TextPrint("Robust " & Global.Outworldz.My.Resources.did_not_start_word & ex.Message)
            FormSetup.KillAll()
            FormSetup.Buttons(FormSetup.StartButton)
            RobustIcon(False)
            RobustIsStarting = False
            Return False
        End Try

        PropRobustProcID = WaitForPID(RobustProcess)
        If PropRobustProcID = 0 Then
            RobustIcon(False)
            Log("Error", Global.Outworldz.My.Resources.Robust_failed_to_start)
            RobustIsStarting = False
            Return False
        End If

        SetWindowTextCall(RobustProcess, RobustName)

        ' Wait for Robust to start listening
        Dim counter = 0
        While Not IsRobustRunning() And PropOpensimIsRunning
            Log("Error", Global.Outworldz.My.Resources.Waiting_on_Robust)
            Application.DoEvents()
            counter += 1
            ' wait a minute for it to start
            If counter Mod 5 = 0 Then
                TextPrint("Robust " & Global.Outworldz.My.Resources.did_not_start_word)
            End If

            If counter > 600 Then
                TextPrint(My.Resources.Robust_failed_to_start)
                FormSetup.Buttons(FormSetup.StartButton)
                Dim yesno = MsgBox(My.Resources.See_Log, MsgBoxStyle.YesNo Or MsgBoxStyle.MsgBoxSetForeground, Global.Outworldz.My.Resources.Error_word)
                If (yesno = vbYes) Then
                    Dim Log As String = """" & Settings.OpensimBinPath & "Robust.log" & """"
                    Try
                        System.Diagnostics.Process.Start(IO.Path.Combine(Settings.CurrentDirectory, "baretail.exe " & Log))
                    Catch ex As Exception
                        BreakPoint.Show(ex.Message)
                    End Try
                End If
                FormSetup.Buttons(FormSetup.StartButton)

                RobustIcon(False)
                RobustIsStarting = False
                Return False
            End If

            Sleep(100)
        End While

        RobustIsStarting = False
        Log(My.Resources.Info_word, Global.Outworldz.My.Resources.Robust_running)
        ShowDOSWindow(GetHwnd(RobustName), MaybeHideWindow())
        RobustIcon(True)
        TextPrint(Global.Outworldz.My.Resources.Robust_running)
        PropRobustExited = False

        Return True

    End Function

    Public Sub StopRobust()

        If Settings.ServerType <> RobustServer Then Return

        TextPrint("Robust " & Global.Outworldz.My.Resources.Stopping_word)
        ConsoleCommand(RobustName, "q{ENTER}" & vbCrLf & "q{ENTER}" & vbCrLf)
        Dim ctr As Integer = 0
        ' wait 60 seconds for robust to quit
        While IsRobustRunning() And ctr < 60
            Application.DoEvents()
            Sleep(1000)
            ctr += 1
        End While

        RobustIcon(False)

    End Sub

#End Region

    Public Sub DoBanList()

        Dim MACString As String = ""
        Dim ViewerString As String = ""
        Dim GridString As String = ""
        Dim Bans As String = Settings.BanList

        Dim Banlist As String()
        Banlist = Bans.Split("|".ToCharArray())
        For Each str As String In Banlist

            Dim a() = str.Split("=".ToCharArray())
            Dim s = a(0)

            Dim pattern1 As Regex = New Regex("^#")
            Dim match1 As Match = pattern1.Match(s)
            If match1.Success Then
                Continue For
            End If

            ' ban grid Addresses
            Dim pattern2 As Regex = New Regex("^http")
            Dim match2 As Match = pattern2.Match(s)
            If match2.Success Then
                GridString += s & ","   ' delimiter is a comma for grids
                Continue For
            End If

            ' Ban IP's

            Dim pattern3 As Regex = New Regex("^\d+\.\d+\.\d+\.\d+")
            Dim match3 As Match = pattern3.Match(s)
            If match3.Success Then
                Firewall.BlockIP(s)
                Continue For
            End If

            ' ban MAC Addresses with and without caps and :
            Dim pattern4 As Regex = New Regex("^[a-f0-9A-F]{32}")
            Dim match4 As Match = pattern4.Match(s)
            If match4.Success Then
                MACString += s & " " ' delimiter is a " " and  not a pipe
                Continue For
            End If

            ' none of the above
            If s.Length > 0 Then
                ViewerString += s & "|"
            End If

        Next

        ' Ban grids
        If GridString.Length > 0 Then
            GridString = Mid(GridString, 1, GridString.Length - 1)
        End If
        Settings.SetIni("GatekeeperService", "AllowExcept", GridString)

        ' Ban Macs
        If MACString.Length > 0 Then
            MACString = Mid(MACString, 1, MACString.Length - 1)
        End If
        Settings.SetIni("LoginService", "DeniedMacs", MACString)
        Settings.SetIni("GatekeeperService", "DeniedMacs", MACString)

        'Ban Viewers
        If ViewerString.Length > 0 Then
            ViewerString = Mid(ViewerString, 1, ViewerString.Length - 1)
        End If
        If ViewerString.Length > 0 Then
            ViewerString = Mid(ViewerString, 1, ViewerString.Length - 1)
        End If

        Settings.SetIni("AccessControl", "DeniedClients", ViewerString)

    End Sub

    Public Function DoRobust() As Boolean

        TextPrint("->Set Robust")

        If DoSetDefaultSims() Then Return True

        ' Robust Process
        Dim INI = Settings.LoadIni(Settings.OpensimBinPath & "Robust.HG.ini", ";")

        If Settings.AltDnsName.Length > 0 Then
            Settings.SetIni("Hypergrid", "HomeURIAlias", Settings.AltDnsName)
            Settings.SetIni("Hypergrid", "GatekeeperURIAlias", Settings.AltDnsName)
        End If

        Settings.SetIni("Const", "GridName", Settings.SimName)
        Settings.SetIni("Const", "BaseURL", "http://" & Settings.PublicIP)

        DoBanList()

        Settings.SetIni("Const", "PrivURL", "http://" & Settings.LANIP())
        Settings.SetIni("Const", "PublicPort", Convert.ToString(Settings.HttpPort, Globalization.CultureInfo.InvariantCulture)) ' 8002
        Settings.SetIni("Const", "PrivatePort", Convert.ToString(Settings.PrivatePort, Globalization.CultureInfo.InvariantCulture))
        Settings.SetIni("Const", "http_listener_port", Convert.ToString(Settings.HttpPort, Globalization.CultureInfo.InvariantCulture))
        Settings.SetIni("Const", "ApachePort", Convert.ToString(Settings.ApachePort, Globalization.CultureInfo.InvariantCulture))

        If Settings.Suitcase() Then
            Settings.SetIni("HGInventoryService", "LocalServiceModule", "OpenSim.Services.HypergridService.dll:HGSuitcaseInventoryService")
        Else
            Settings.SetIni("HGInventoryService", "LocalServiceModule", "OpenSim.Services.InventoryService.dll:XInventoryService")
        End If

        ' LSL emails
        Settings.SetIni("SMTP", "SMTP_SERVER_HOSTNAME", Settings.SmtpHost)
        Settings.SetIni("SMTP", "SMTP_SERVER_PORT", Convert.ToString(Settings.SmtpPort, Globalization.CultureInfo.InvariantCulture))
        Settings.SetIni("SMTP", "SMTP_SERVER_LOGIN", Settings.SmtPropUserName)
        Settings.SetIni("SMTP", "SMTP_SERVER_PASSWORD", Settings.SmtpPassword)

        SetupRobustSearchINI()

        SetupMoney()

        Settings.SetIni("LoginService", "WelcomeMessage", Settings.WelcomeMessage)

        'FSASSETS
        If Settings.FsAssetsEnabled Then
            Settings.SetIni("AssetService", "LocalServiceModule", "OpenSim.Services.FSAssetService.dll:FSAssetConnector")
            Settings.SetIni("HGAssetService", "LocalServiceModule", "OpenSim.Services.HypergridService.dll:HGFSAssetService")
        Else
            Settings.SetIni("AssetService", "LocalServiceModule", "OpenSim.Services.AssetService.dll:AssetService")
            Settings.SetIni("HGAssetService", "LocalServiceModule", "OpenSim.Services.HypergridService.dll:HGAssetService")
        End If

        Settings.SetIni("AssetService", "BaseDirectory", Settings.BaseDirectory & "/data")
        Settings.SetIni("AssetService", "SpoolDirectory", Settings.BaseDirectory & "/tmp")
        Settings.SetIni("AssetService", "ShowConsoleStats", Settings.ShowConsoleStats)

        Settings.SetIni("SmartStart", "Enabled", CStr(Settings.SmartStart))
        Settings.SetIni("ServiceList", "GetTextureConnector", """" & "${Const|PublicPort}/Opensim.Capabilities.Handlers.dll:GetTextureServerConnector" & """")

        If Settings.CMS = JOpensim Then
            Settings.SetIni("ServiceList", "UserProfilesServiceConnector", "")
            Settings.SetIni("UserProfilesService", "Enabled", "False")
            Settings.SetIni("GridInfoService", "welcome", "${Const|BaseURL}:${Const|ApachePort}/jOpensim/index.php?option=com_opensim")
            Settings.SetIni("GridInfoService", "economy", "${Const|BaseURL}:${Const|ApachePort}/jOpensim/components/com_opensim/")
        Else
            Settings.SetIni("ServiceList", "UserProfilesServiceConnector", "${Const|PublicPort}/OpenSim.Server.Handlers.dll:UserProfilesConnector")
            Settings.SetIni("UserProfilesService", "Enabled", "True")
            Settings.SetIni("GridInfoService", "welcome", Settings.SplashPage)
            Settings.SetIni("GridInfoService", "economy", "${Const|BaseURL}:${Const|PublicPort}")
        End If

        Settings.SetIni("DatabaseService", "ConnectionString", Settings.RobustDBConnection)

        Settings.SaveINI(INI, System.Text.Encoding.UTF8)

        Dim src = IO.Path.Combine(Settings.CurrentDirectory, "Outworldzfiles\Opensim\bin\Robust.exe.config.proto")
        Dim Dest = IO.Path.Combine(Settings.CurrentDirectory, "Outworldzfiles\Opensim\bin\Robust.exe.config")
        CopyFileFast(src, Dest)
        Dim anini = IO.Path.Combine(Settings.CurrentDirectory, "Outworldzfiles\Opensim\bin\Robust.exe.config")
        Settings.Grep(anini, Settings.LogLevel)

        Return False

    End Function

    ''' <summary>Check is Robust port 8002 is up</summary>
    ''' <returns>boolean</returns>
    Public Function IsRobustRunning() As Boolean

        Log("INFO", "Checking Robust")
        Using client As New WebClient ' download client for web pages
            Dim Up As String
            Try
                Up = client.DownloadString("http://" & Settings.RobustServer & ":" & Settings.HttpPort & "/?_Opensim=" & RandomNumber.Random())
            Catch ex As Exception
                Log("INFO", "Robust is running")
                If ex.Message.Contains("404") Then
                    Log("INFO", "Robust is running")
                    Return True
                End If
                Log("INFO", "Robust is not running")
                Return False
            End Try

            If Up.Length = 0 And PropOpensimIsRunning() Then
                Log("INFO", "Robust is not running")
                Return False
            End If
        End Using
        Log("INFO", "Robust is running")
        Return True

    End Function

    Private Sub RobustProcess_Exited(ByVal sender As Object, ByVal e As EventArgs) Handles RobustProcess.Exited

        FormSetup.RestartRobustIcon.Image = Global.Outworldz.My.Resources.nav_plain_red

        ' Handle Exited event and display process information.
        PropRobustProcID = Nothing
        If PropAborting Then Return

        If PropRestartRobust Then
            PropRobustExited = True
            Return
        End If

        If Settings.RestartOnCrash And RobustCrashCounter < 10 Then
            PropRobustExited = True
            RobustCrashCounter += 1
            Return
        End If
        RobustCrashCounter = 0
        RobustIcon(False)

        Dim yesno = MsgBox(My.Resources.Robust_exited, MsgBoxStyle.YesNo Or MsgBoxStyle.MsgBoxSetForeground, Global.Outworldz.My.Resources.Error_word)
        If (yesno = vbYes) Then
            Dim MysqlLog As String = Settings.OpensimBinPath & "Robust.log"
            Try
                System.Diagnostics.Process.Start(IO.Path.Combine(Settings.CurrentDirectory, "baretail.exe"), """" & MysqlLog & """")
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
            End Try
        End If

    End Sub

    Private Sub SetupMoney()

        If Settings.GloebitsEnable Then
            Settings.SetIni("LoginService", "Currency", "G$")
            CopyFileFast(IO.Path.Combine(Settings.OpensimBinPath, "Gloebit.dll.bak"), IO.Path.Combine(Settings.OpensimBinPath, "Gloebit.dll"))
            'DeleteFile(IO.Path.Combine(Settings.OpensimBinPath, "jOpenSim.Money.dll"))
        ElseIf Settings.GloebitsEnable = False And Settings.CMS = JOpensim Then
            Settings.SetIni("LoginService", "Currency", "jO$")
            CopyFileFast(IO.Path.Combine(Settings.OpensimBinPath, "jOpensim.Money.dll.bak"), IO.Path.Combine(Settings.OpensimBinPath, "jOpensim.Money.dll"))
            DeleteFile(IO.Path.Combine(Settings.OpensimBinPath, "Gloebit.dll"))
        Else
            Settings.SetIni("LoginService", "Currency", "$")
            DeleteFile(IO.Path.Combine(Settings.OpensimBinPath, "Gloebit.dll"))
            'DeleteFile(IO.Path.Combine(Settings.OpensimBinPath, "jOpenSim.Money.dll"))
        End If

    End Sub

    Private Sub SetupRobustSearchINI()

        If Settings.CMS = JOpensim And Settings.SearchOptions = JOpensim Then
            Dim SearchURL = "http://" & Settings.PublicIP & ":" & Settings.ApachePort & "/jOpensim/index.php?option=com_opensim&view=inworldsearch&task=viewersearch&tmpl=component&"
            Settings.SetIni("LoginService", "SearchURL", SearchURL)
            Settings.SetIni("LoginService", "DestinationGuide", "http://" & Settings.PublicIP & ":" & Settings.ApachePort & "/jOpensim/index.php?option=com_opensim&view=guide&tmpl=component")
        ElseIf Settings.SearchOptions = Hyperica Then
            Settings.SetIni("LoginService", "SearchURL", "http://hyperica.com/Search/query.php")
            Settings.SetIni("LoginService", "DestinationGuide", "http://hyperica.com/destination-guide")
        Else
            Settings.SetIni("LoginService", "SearchURL", "")
            Settings.SetIni("LoginService", "DestinationGuide", "")
        End If

    End Sub

End Module
