
#Region "Copyright"
' Copyright 2014 Fred Beckhusen for www.Outworldz.com
' https://opensource.org/licenses/AGPL

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

Imports System.Net
Imports System.IO
Imports System.Net.Sockets
Imports IWshRuntimeLibrary
Imports System.Threading
Imports System.Runtime.InteropServices
Imports System.Text

Public Class Form1

#Region "Declarations"

    Dim gMyVersion As String = "2.77"
    Dim gSimVersion As String = "0.9.1"

    ' edit this to compile and run in the correct folder root
    Dim gDebugPath As String = "\Opensim\Outworldz DreamGrid Source"  ' no slash at end
    Public gDebug As Boolean = False  ' set by code to log some events in when in a debugger

    Dim gCPUMAX As Double = 80 ' max CPU % can be used when booting or we wait til it gets lower 
    ' not https, which breaks stuff
    Public gDomain As String = "http://www.outworldz.com"
    Public gPath As String ' Holds path to Opensim folder

    Public RegionHandles As New Dictionary(Of Integer, String)
    Public MyFolder As String   ' Holds the current folder that we are running in
    Dim gCurSlashDir As String '  holds the current directory info in Unix format for MySQL
    Public gIsRunning As Boolean = False ' used in OpensimIsRunning property
    Dim Arnd As Random = New Random()
    Public gChatTime As Integer     'amount of coffee the fairy had. Time for the chatty fairy to be read
    Dim client As New System.Net.WebClient ' downloadclient for web pages
    Public Shared MysqlConn As Mysql

    ' with events
    Private WithEvents ProcessMySql As Process = New Process()
    Public Event Exited As EventHandler
    Private WithEvents RobustProcess As New Process()
    Public Event RobustExited As EventHandler

    Dim Data As IniParser.Model.IniData
    Dim parser As IniParser.FileIniDataParser

    ' robust global PID
    Public gRobustProcID As Integer
    Public gRobustConnStr As String = ""

    Public gMaxPortUsed As Integer = 0  'Max number of port used past 8004

    Private images As List(Of Image) = New List(Of Image) From {My.Resources.tangled, My.Resources.wp_habitat, My.Resources.wp_Mooferd,
                             My.Resources.wp_To_Piers_Anthony,
                             My.Resources.wp_wavy_love_of_animals, My.Resources.wp_zebra,
                             My.Resources.wp_Que, My.Resources.wp_1, My.Resources.wp_2,
                             My.Resources.wp_3, My.Resources.wp_4, My.Resources.wp_5,
                             My.Resources.wp_6, My.Resources.wp_7, My.Resources.wp_8,
                             My.Resources.wp_9, My.Resources.wp_10, My.Resources.wp_11,
                             My.Resources.wp_12, My.Resources.wp_13, My.Resources.wp_14,
                             My.Resources.wp_15, My.Resources.wp_16, My.Resources.wp_17,
                             My.Resources.wp_18, My.Resources.wp_19, My.Resources.wp_20,
                             My.Resources.wp_21, My.Resources.wp_22, My.Resources.wp_23,
                             My.Resources.wp_24, My.Resources.wp_25, My.Resources.wp_26,
                             My.Resources.wp_27, My.Resources.wp_28, My.Resources.wp_29,
                             My.Resources.wp_30, My.Resources.wp_31, My.Resources.wp_32,
                             My.Resources.wp_33, My.Resources.wp_34, My.Resources.wp_35,
                             My.Resources.wp_36, My.Resources.wp_37, My.Resources.wp_38,
                             My.Resources.wp_39, My.Resources.wp_40, My.Resources.wp_41,
                             My.Resources.wp_42, My.Resources.wp_43, My.Resources.wp_44,
                             My.Resources.wp_45, My.Resources.wp_46, My.Resources.wp_47,
                             My.Resources.wp_48, My.Resources.wp_49, My.Resources.wp_50,
                             My.Resources.wp_51, My.Resources.wp_52, My.Resources.wp_53,
                             My.Resources.wp_54, My.Resources.wp_55, My.Resources.wp_56,
                             My.Resources.wp_57, My.Resources.fairy
                            }

    Dim gContentAvailable As Boolean = False ' assume there is no OAR and IAR data available
    Public MyUPnpMap As UPnp        ' UPNP Declaration
    Dim ws As NetServer             ' Port 8001 Webserver
    Public gStopping As Boolean = False    ' Allows an Abort when Stopping is clicked
    Dim Timertick As Integer        ' counts the seconds until wallpaper changes

    Dim gDNSSTimer As Integer = 0    ' ping server every hour
    Dim gUseIcons As Boolean = True     ' if 8001 is blocked
    Dim gIPv4Address As String          ' global IPV4
    Public MySetting As New MySettings  ' all settings from Settings.ini
    Public gExiting As Boolean = False  ' we are quitting

    ' Shoutcast
    Dim gIcecastProcID As Integer = 0
    Private WithEvents IcecastProcess As New Process()
    Dim Adv As AdvancedForm
    ' Help Form for RTF files
    Public FormHelp As New FormHelp
    Dim FormCaches As New FormCaches
    Public FormPersonality As FormPersonality

    ' Region 
    Public RegionClass As RegionMaker   ' Global RegionClass
    Public RegionForm As RegionList
    Dim ExitList As New List(Of String)

    ' Mysql
    Dim gStopMysql As Boolean = True    'lets us detct if Mysql is a service so we do not shut it down

    Public gUpdateView As Boolean = True 'Region Form Refresh

    Dim gInitted As Boolean = False

    Dim gWindowCounter As Integer = 0
    Public gRestartNow As Boolean = False ' set true if a person clicks a restart button to get a sim restarted when auto restart is off
    Public gSelectedBox As String = ""
    Public gForceParcel As Boolean = True
    Public gForceTerrain As Boolean = True
    Public gForceMerge As Boolean = True

    <CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA2101:SpecifyMarshalingForPInvokeStringArguments", MessageId:="1")>
    <CodeAnalysis.SuppressMessage("Microsoft.Interoperability", "CA1401:PInvokesShouldNotBeVisible")>
    <CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")>
    <DllImport("user32.dll")>
    Shared Function SetWindowText(ByVal hwnd As IntPtr, ByVal windowName As String) As Boolean
    End Function

    Dim cpu As New PerformanceCounter()


    ''' <summary>
    ''' SetWindowTextCall is here to wrap the SetWindowtext API call.  This call fails when there is no 
    ''' hwnd as Windows takes its sweet time to get that. It has a global timer to make sure we do not get stuck
    ''' </summary>
    ''' <param name="hwnd">Handle to the window to change the text on</param>
    ''' <param name="windowName">the name of the DOS Window such asRobust, or a region name</param>
    ''' 
    Public Function SetWindowTextCall(ByVal hwnd As IntPtr, ByVal windowName As String) As Boolean

        Dim status As Boolean = False
        Try
            status = SetWindowText(hwnd, windowName)
        Catch ex As Exception
            ' can fail to be a window
        End Try


        If Not status Then
            '
            gWindowCounter = gWindowCounter + 1
            If gWindowCounter > 20 Then
                gWindowCounter = 0 ' prep for next window
                status = True
            End If
        Else ' if true, we did it
            gWindowCounter = 0 ' prep for next window
        End If

        Return status

    End Function

    <Flags()>
    Private Enum REGION_TIMER As Integer
        STOPPED = -3
        RESTARTING = -2
        RESTART_PENDING = -1
        START_COUNTING = 0
    End Enum

#End Region

#Region "ScreenSize"
    Public ScreenPosition As ScreenPos
    Private Handler As New EventHandler(AddressOf resize_page)

    'The following detects  the location of the form in screen coordinates
    Private Sub resize_page(ByVal sender As Object, ByVal e As System.EventArgs)
        'Me.Text = "Form screen position = " + Me.Location.ToString
        ScreenPosition.SaveXY(Me.Left, Me.Top)
    End Sub
    Private Sub SetScreen()
        Me.Show()
        ScreenPosition = New ScreenPos(Me.Name)
        AddHandler ResizeEnd, Handler
        Dim xy As List(Of Integer) = ScreenPosition.GetXY()
        Me.Left = xy.Item(0)
        Me.Top = xy.Item(1)
    End Sub

#End Region

#Region "Properties"

    Public Property UpdateView() As Boolean
        Get
            Return gUpdateView
        End Get
        Set(ByVal Value As Boolean)
            gUpdateView = Value
        End Set
    End Property

    Public Property IPv4Address() As String
        Get
            Return gIPv4Address
        End Get
        Set(ByVal Value As String)
            gIPv4Address = Value
        End Set
    End Property

    Public Property OpensimIsRunning() As Boolean
        Get
            Return gIsRunning
        End Get
        Set(ByVal Value As Boolean)
            gIsRunning = Value
        End Set
    End Property

#End Region

#Region "StartStop"

    ''' <summary>
    ''' Form Load is main() for all Dreamgrid
    ''' </summary>
    ''' <param name="sender">Unused</param>
    ''' <param name="e">Unused</param>
    ''' 
    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Application.EnableVisualStyles()

        PictureBox1.Size = TextBox1.Size

        MyFolder = My.Application.Info.DirectoryPath

        If MyFolder.Contains("Source") Then
            ' for debugging when compiling
            gDebug = True
            MyFolder = gDebugPath ' for testing, as the compiler buries itself in ../../../debug
        End If
        gCurSlashDir = MyFolder.Replace("\", "/")    ' because Mysql uses unix like slashes, that's why
        gPath = MyFolder & "\OutworldzFiles\Opensim\"

        SetScreen()     ' move Form to fit screen from SetXY.ini

        ' Kill Shoutcast
        Try
            My.Computer.FileSystem.DeleteDirectory(MyFolder + "\Shoutcast", FileIO.DeleteDirectoryOption.DeleteAllContents)
        Catch
        End Try

        Try
            My.Computer.FileSystem.DeleteFile(gPath + "\bin\OpenSim.Additional.AutoRestart.dll")
        Catch
        End Try
        Try
            My.Computer.FileSystem.DeleteFile(gPath + "\bin\OpenSim.Additional.AutoRestart.pdb")
        Catch
        End Try

        MySetting.Init(MyFolder)

        MySetting.Myfolder = MyFolder

        ' Save a random machine ID - we don't want any data to be sent that's personal or identifiable,  but it needs to be unique
        Randomize()
        If MySetting.MachineID() = "" Then MySetting.MachineID() = Random()  ' a random machine ID may be generated.  Happens only once


        'hide progress
        ProgressBar1.Visible = True
        ProgressBar1.Minimum = 0
        ProgressBar1.Maximum = 100
        ProgressBar1.Value = 0

        TextBox1.BackColor = Me.BackColor
        TextBox1.AllowDrop = True

        PictureBox1.Enabled = True
        PictureBox1.AllowDrop = True

        LogButton.Hide()
        IgnoreButton.Hide()
        Buttons(BusyButton)

        ' hide updater
        UpdaterGo.Visible = False
        UpdaterCancel.Visible = False

        ' WebUI
        ViewWebUI.Visible = MySetting.WifiEnabled

        Me.Text = "Dreamgrid V" + gMyVersion

        gChatTime = MySetting.ChatTime

        OpensimIsRunning() = False ' true when opensim is running
        Me.Show()

        RegionClass = RegionMaker.Instance(MysqlConn)

        Adv = New AdvancedForm
        gInitted = True

        SaySomething()

        ClearLogFiles() ' clear log fles

        Try
            System.IO.Directory.Delete(MyFolder + "\Icecast", True)
        Catch
        End Try
        Try
            System.IO.Directory.Delete(MyFolder + "\Outworldzfiles\Opensim\bin\config-include\Birds.ini", True)
        Catch
        End Try

        MyUPnpMap = New UPnp(MyFolder)

        MySetting.PublicIP = MyUPnpMap.LocalIP
        MySetting.PrivateURL = MySetting.PublicIP

        ' Set them back to the DNS name if there is one
        If MySetting.DNSName.Length > 0 Then
            MySetting.PublicIP = MySetting.DNSName
        End If

        If (MySetting.SplashPage = "") Then
            MySetting.SplashPage = gDomain + "/Outworldz_installer/Welcome.htm"
        End If

        ProgressBar1.Value = 100
        ProgressBar1.Value = 0

        If Not MySetting.SkipUpdateCheck Then
            CheckForUpdates()
        End If

        CheckDefaultPorts()

        ' must start after region Class is instantiated
        ws = NetServer.GetWebServer
        Log("Info:Starting Web Server ")

        ws.StartServer(MyFolder, MySetting, MySetting.PrivateURL, CType(MySetting.DiagnosticPort, Integer))

        ' Run diagnostics, maybe
        If Not MySetting.RanAllDiags Then
            DoDiag()
            MySetting.RanAllDiags = True
        End If

        OpenPorts()

        If Not SetIniData() Then Return


        FormPersonality = New FormPersonality

        RegionClass.UpdateAllRegionPorts() ' must be after SetIniData
        SetFirewall()   ' must be after UpdateAllRegionPorts

        mnuSettings.Visible = True
        SetIAROARContent() ' load IAR and OAR web content
        LoadLocalIAROAR() ' load IAR and OAR local content

        If MySetting.Password = "secret" Then
            BumpProgress10()
            Dim Password = New PassGen
            MySetting.Password = Password.GeneratePass()
        End If

        ' Find out if the viewer is installed
        If System.IO.File.Exists(MyFolder & "\OutworldzFiles\Settings.ini") Then
            Application.DoEvents()

            Buttons(StartButton)
            ProgressBar1.Value = 100

            If MySetting.Autostart Then
                Print("Auto Startup")
                Startup()
            Else
                MySetting.SaveSettings()
                Print("Ready to Launch! Click 'Start' to begin your adventure in Opensimulator.")
            End If

        Else

            Print("Installing Desktop icon clicky thingy")
            Create_ShortCut(MyFolder & "\Start.exe")
            BumpProgress10()
            MySetting.SaveSettings()
            Print("Ready to Launch!")
            Buttons(StartButton)

        End If

        Dim isMySqlRunning = CheckPort("127.0.0.1", CType(MySetting.MySqlPort, Integer))
        If isMySqlRunning Then gStopMysql = False

        HelpOnce("Startup")
        HelpOnce("License")

        ProgressBar1.Value = 100

    End Sub

    Public Sub SetFirewall()

        Dim Command As String = "netsh advfirewall firewall add rule name=""Opensim TCP Port " & MySetting.DiagnosticPort & """ dir=in action=allow protocol=TCP localport=" & MySetting.DiagnosticPort & vbCrLf _
                              & "netsh advfirewall firewall add rule name=""Opensim UDP Port " & MySetting.DiagnosticPort & """ dir=in action=allow protocol=UDP localport=" & MySetting.DiagnosticPort & vbCrLf _
                              & "netsh advfirewall firewall add rule name=""Opensim HTTP TCP Port " & MySetting.HttpPort & """ dir=in action=allow protocol=TCP localport=" & MySetting.HttpPort & vbCrLf _
                              & "netsh advfirewall firewall add rule name=""Opensim HTTP UDP Port " & MySetting.HttpPort & """ dir=in action=allow protocol=UDP localport=" & MySetting.HttpPort & vbCrLf


        Dim RegionNumber As Integer = 0
        Dim start = CInt(MySetting.FirstRegionPort)

        For RegionNumber = start To gMaxPortUsed
            Command = Command + "netsh advfirewall firewall add rule name=""Region TCP Port " & RegionNumber.ToString & """ dir=in action=allow protocol=TCP localport=" & RegionNumber.ToString & vbCrLf _
                    & "netsh advfirewall firewall add rule name=""Region UDP Port " & RegionNumber.ToString & """ dir=in action=allow protocol=UDP localport=" & RegionNumber.ToString & vbCrLf
        Next

        Dim ns As StreamWriter = New StreamWriter(MyFolder + "\fw.bat", False)
        ns.WriteLine(Command)
        ns.Close()

        Dim pi As ProcessStartInfo = New ProcessStartInfo()
        pi.Arguments = ""
        pi.FileName = MyFolder + "\fw.bat"
        pi.WindowStyle = ProcessWindowStyle.Hidden
        pi.Verb = "runas"
        Dim ProcessFirewall As Process = New Process()
        ProcessFirewall.StartInfo = pi

        Try
            ProcessFirewall.Start()
        Catch ex As Exception
            Log("Error:Could not set firewall:" + ex.Message)
        End Try


    End Sub

    ''' <summary>
    ''' Start Button on main form
    ''' </summary>
    Private Sub StartButton_Click(sender As System.Object, e As System.EventArgs) Handles StartButton.Click
        Startup()
        'Print("")
    End Sub

    ''' <summary>
    ''' Startup() Starts opensimulator system
    ''' Called by Start Button or by AutoStart
    ''' </summary>
    Private Sub Startup()


        With cpu
            .CategoryName = "Processor"
            .CounterName = "% Processor Time"
            .InstanceName = "_Total"
        End With

        PrintFast("Starting...")
        gExiting = False  ' suppress exit warning messages
        ProgressBar1.Value = 0
        ProgressBar1.Visible = True
        Buttons(BusyButton)

        RegionClass.UpdateAllRegionPorts() ' must be donbe before we are running
        SetFirewall()   ' must be after UpdateAllRegionPorts

        ' clear region error handlers

        RegionHandles.Clear()

        If MySetting.AutoBackup Then
            ' add 30 minutes to allow time to auto backup and then restart
            Dim BTime As Int16 = CType(MySetting.AutobackupInterval, Int16)
            If MySetting.AutoRestartInterval > 0 And MySetting.AutoRestartInterval < BTime Then
                MySetting.AutoRestartInterval = BTime + 30
                Print("Upping AutoRestart Time to " + BTime.ToString + " + 30 Minutes.")
            End If
        End If


        OpensimIsRunning() = True

        RegionClass.GetAllRegions()

        If SetPublicIP() Then
            OpenPorts()
        End If

        If Not SetIniData() Then Return   ' set up the INI files

        If Not StartMySQL() Then
            ProgressBar1.Value = 0
            ProgressBar1.Visible = True
            Buttons(StartButton)
            Print("Stopped")
            Return
        End If


        Timer1.Interval = 1000
        Timer1.Start() 'Timer starts functioning

        If Not Start_Robust() Then
            Return
        End If

        If Not MySetting.RunOnce Then
            RobustCommand("create user{ENTER}")
            MsgBox("Please type the Grid Owner's avatar name into the Robust window. Press <enter> for UUID and Model name. Then press this OK button", vbInformation, "Info")
            MySetting.RunOnce = True
            MySetting.SaveSettings()
        End If

        Try
            If MySetting.BirdsModuleStartup Then
                My.Computer.FileSystem.CopyFile(gPath + "\bin\OpenSimBirds.Module.bak", gPath + "\bin\OpenSimBirds.Module.dll")
            Else
                My.Computer.FileSystem.DeleteFile(gPath + "\bin\OpenSimBirds.Module.dll")
            End If

        Catch ex As Exception
            'Log(ex.Message)
        End Try


        ' make sure all regions are stopped
        For Each X As Integer In RegionClass.RegionNumbers
            RegionClass.Timer(X) = REGION_TIMER.STOPPED
        Next

        ' Launch the rockets
        If Not Start_Opensimulator() Then
            Return
        End If

        StartIcecast()

        ' show the IAR and OAR menu when we are up 
        If gContentAvailable Then
            IslandToolStripMenuItem.Visible = True
            ClothingInventoryToolStripMenuItem.Visible = True
        End If

        Buttons(StopButton)
        ProgressBar1.Value = 100
        Print("Grid address is" + vbCrLf + "http://" + MySetting.PublicIP + ":" + MySetting.HttpPort)

        ' done with bootup
        ProgressBar1.Visible = False

        Me.AllowDrop = True

    End Sub

    Private Sub Form1_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed

        Shutdown()
        End

    End Sub

    Private Sub MnuExit_Click(sender As System.Object, e As System.EventArgs) Handles mnuExit.Click

        Shutdown()
        End

    End Sub
    Private Sub Shutdown()

        Try
            Log("Stopping Webserver")
            ws.StopWebServer()
        Catch
        End Try

        ProgressBar1.Value = 95
        KillAll()
        ProgressBar1.Value = 10
        StopMysql()
        Print("I'll tell you my next dream when I wake up.")
        ProgressBar1.Value = 5
        Print("Zzzz...")
        ProgressBar1.Value = 0

    End Sub

    Private Declare Function ShowWindow Lib "user32.dll" (ByVal hWnd As IntPtr, ByVal nCmdShow As SHOW_WINDOW) As Boolean

    <Flags()>
    Private Enum SHOW_WINDOW As Integer
        SW_HIDE = 0
        SW_SHOWNORMAL = 1
        SW_NORMAL = 1
        SW_SHOWMINIMIZED = 2
        SW_SHOWMAXIMIZED = 3
        SW_MAXIMIZE = 3
        SW_SHOWNOACTIVATE = 4
        SW_SHOW = 5
        SW_MINIMIZE = 6
        SW_SHOWMINNOACTIVE = 7
        SW_SHOWNA = 8
        SW_RESTORE = 9
        SW_SHOWDEFAULT = 10
        SW_FORCEMINIMIZE = 11
        SW_MAX = 11
    End Enum

    Private Sub KillAll()

        gExiting = True ' force msgbox is anything exists

        gStopping = True
        ProgressBar1.Value = 100
        ProgressBar1.Visible = True
        ' close everything as gracefully as possible.

        StopIcecast()

        Dim n As Integer = RegionClass.RegionCount()
        Diagnostics.Debug.Print("N=" + n.ToString())

        Dim TotalRunningRegions As Integer
        ' shows all windows during shutdown
        For Each X As Integer In RegionClass.RegionNumbers
            If RegionClass.Booted(X) Then
                TotalRunningRegions = TotalRunningRegions + 1
            End If
        Next
        Log("Total Enabled Regions=" + TotalRunningRegions.ToString)


        For Each X As Integer In RegionClass.RegionNumbers
            Application.DoEvents()

            If OpensimIsRunning() And RegionClass.RegionEnabled(X) And Not RegionClass.ShuttingDown(X) Then
                Dim hwnd = getHwnd(RegionClass.GroupName(X))
                If hwnd <> IntPtr.Zero Then ShowWindow(hwnd, SHOW_WINDOW.SW_RESTORE)

                ConsoleCommand(RegionClass.GroupName(X), "q{ENTER}" + vbCrLf)
                ConsoleCommand(RegionClass.GroupName(X), "Q{ENTER}" + vbCrLf)


                RegionClass.ShuttingDown(X) = True
                RegionClass.Booted(X) = False
                RegionClass.WarmingUp(X) = False

                RegionClass.Timer(X) = REGION_TIMER.STOPPED
                UpdateView = True ' make form refresh
                Sleep(1000)
            End If
        Next

        Dim counter = 600 ' 10 minutes to quit all regions

        ' only wait if the port 8001 is working
        If gUseIcons Then
            If OpensimIsRunning Then Print("Waiting for all regions to exit")

            While (counter > 0 And OpensimIsRunning())
                ' decrement progress bar according to the ratio of what we had / what we have now
                counter = counter - 1
                Dim CountisRunning As Integer = 0

                For Each X In RegionClass.RegionNumbers
                    If RegionClass.ShuttingDown(X) = True Then
                        PrintFast("Checking " + RegionClass.RegionName(X))

                        If CheckPort(MySetting.PrivateURL, RegionClass.GroupPort(X)) Then
                            CountisRunning = CountisRunning + 1
                        Else
                            For Each Y In RegionClass.RegionListByGroupNum(RegionClass.GroupName(X))
                                RegionClass.ShuttingDown(Y) = False
                                RegionClass.Booted(Y) = False
                                RegionClass.WarmingUp(Y) = False
                                If RegionClass.Timer(Y) >= 0 Then
                                    RegionClass.Timer(Y) = REGION_TIMER.STOPPED
                                End If
                            Next
                        End If

                        UpdateView = True ' make form refresh
                    End If
                    Application.DoEvents()
                Next

                If CountisRunning = 0 Then
                    counter = 0
                    ProgressBar1.Value = 0
                End If

                Dim v As Double = CountisRunning / TotalRunningRegions * 100
                If v >= 0 And v <= 100 Then
                    ProgressBar1.Value = CType(v, Integer)
                    Diagnostics.Debug.Print("V=" + ProgressBar1.Value.ToString)
                End If

                UpdateView = True ' make form refresh

                Application.DoEvents()

            End While
        End If

        RegionHandles.Clear()

        For Each X As Integer In RegionClass.RegionNumbers
            RegionClass.ShuttingDown(X) = False
            RegionClass.Booted(X) = False
            RegionClass.WarmingUp(X) = False
            RegionClass.ProcessID(X) = 0
            RegionClass.Timer(X) = REGION_TIMER.STOPPED
        Next
        UpdateView = True ' make form refresh

        ' show robust last, try-catch in case it crashed.
        Try
            ShowWindow(Process.GetProcessById(gRobustProcID).MainWindowHandle, SHOW_WINDOW.SW_RESTORE)
        Catch
        End Try

        If gRobustProcID > 0 Then
            ConsoleCommand("Robust", "q{ENTER}" + vbCrLf)
            ConsoleCommand("Robust", "Q{ENTER}" + vbCrLf)
        End If

        ' cannot load OAR or IAR, either
        IslandToolStripMenuItem.Visible = False
        ClothingInventoryToolStripMenuItem.Visible = False
        Timer1.Stop()
        OpensimIsRunning() = False
        Me.AllowDrop = False

        ProgressBar1.Value = 0

    End Sub

    Private Function Zap(processName As String) As Boolean
        ' Kill process by name
        For Each P As Process In System.Diagnostics.Process.GetProcessesByName(processName)
            Try
                Log("Info:Stopping process " + processName)
                P.Kill()
                Return True
            Catch ex As Exception
                Log("Info:failed to stop " + processName)
                Return False
            End Try
        Next
        Zap = False
    End Function

#End Region

#Region "Menus"

    Private Sub ConsoleCOmmandsToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ConsoleCOmmandsToolStripMenuItem1.Click
        Dim webAddress As String = "http://opensimulator.org/wiki/Server_Commands"
        Process.Start(webAddress)
    End Sub

    Public Sub Buttons(button As System.Object)
        ' Turns off all 4 stacked buttons, then enables one of them
        BusyButton.Visible = False
        StopButton.Visible = False
        StartButton.Visible = False
        InstallButton.Visible = False
        button.Visible = True
    End Sub

    Private Sub Create_ShortCut(ByVal sTargetPath As String)
        ' Requires reference to Windows Script Host Object Model
        Dim WshShell As WshShellClass = New WshShellClass
        Dim MyShortcut As IWshRuntimeLibrary.IWshShortcut
        Log("Info:creating shortcut on desktop")
        ' The shortcut will be created on the desktop
        Dim DesktopFolder As String = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)
        MyShortcut = CType(WshShell.CreateShortcut(DesktopFolder & "\Outworldz.lnk"), IWshRuntimeLibrary.IWshShortcut)
        MyShortcut.TargetPath = sTargetPath
        MyShortcut.IconLocation = WshShell.ExpandEnvironmentStrings(MyFolder & "\Start.exe")
        MyShortcut.WorkingDirectory = MyFolder
        MyShortcut.Save()

    End Sub

    Public Sub Print(Value As String)

        Log("Info:" + Value)
        PictureBox1.Visible = False
        TextBox1.Text = Value
        TextBox1.Visible = True
        Application.DoEvents()
        Sleep(gChatTime)  ' time to read


    End Sub
    Public Sub PrintFast(Value As String)

        'Log("Info:" + Value)
        PictureBox1.Visible = False
        TextBox1.Visible = True
        TextBox1.Text = Value
        'Application.DoEvents()

    End Sub

    Private Sub MnuAbout_Click(sender As System.Object, e As System.EventArgs) Handles mnuAbout.Click

        Print("(c) 2017 Outworldz,LLC" + vbCrLf + "Version " + gMyVersion)
        Dim webAddress As String = gDomain + "/Outworldz_Installer"
        Process.Start(webAddress)

    End Sub

    Private Sub StopButton_Click_1(sender As System.Object, e As System.EventArgs) Handles StopButton.Click

        Print("Stopping")
        ProgressBar1.Value = 100
        Buttons(BusyButton)

        KillAll()
        Buttons(StartButton)
        Print("Stopped")
        ProgressBar1.Value = 0
        ProgressBar1.Visible = False
    End Sub

    Private Sub ShowToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles mnuShow.Click

        Print("The Opensimulator Console will be shown when Opensim is running.")
        mnuShow.Checked = True
        mnuHide.Checked = False

        MySetting.ConsoleShow = mnuShow.Checked
        MySetting.SaveSettings()

        If OpensimIsRunning() Then
            Print("The Opensimulator Console will be shown the next time the system is started.")
        End If

    End Sub

    Private Sub MnuHide_Click(sender As System.Object, e As System.EventArgs) Handles mnuHide.Click
        Print("The Opensimulator Console will not be shown. You can still interact with it with Help->Opensim Console")
        mnuShow.Checked = False
        mnuHide.Checked = True

        MySetting.ConsoleShow = mnuShow.Checked
        MySetting.SaveSettings()

        If OpensimIsRunning() Then
            Print("The Opensimulator Console will not be shown. Change will occur when Opensim is restarted")
        End If

    End Sub

    Public Function Random() As String
        Dim value As Integer = CInt(Int((600000000 * Rnd()) + 1))
        Random = System.Convert.ToString(value)
    End Function

    Private Sub WebUIToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs)
        Print("The Web UI lets you add or view settings for the default avatar. ")
        If OpensimIsRunning() Then
            Dim webAddress As String = "http://127.0.0.1:" + MySetting.HttpPort
            Process.Start(webAddress)
        End If
    End Sub

#End Region

#Region "INI"

    Public Sub CopyWifi(Page As String)
        Try
            System.IO.Directory.Delete(gPath + "WifiPages", True)
            System.IO.Directory.Delete(gPath + "bin\WifiPages", True)
        Catch ex As Exception
            Log("Info:" & ex.Message)
        End Try

        Try
            My.Computer.FileSystem.CopyDirectory(gPath + "WifiPages-" + Page, gPath + "WifiPages", True)
            My.Computer.FileSystem.CopyDirectory(gPath + "bin\WifiPages-" + Page, gPath + "\bin\WifiPages", True)
        Catch ex As Exception
            Log("Info:" & ex.Message)
        End Try


    End Sub

    Private Sub SetDefaultSims()

        Dim reader As System.IO.StreamReader
        Dim line As String
        Dim DefaultName As String = ""

        Try
            ' add this sim name as a default to the file as HG regions, and add the other regions as fallback

            ' it may have been deleted
            Dim o As Integer = RegionClass.FindRegionByName(MySetting.WelcomeRegion)

            If o < 0 Then
                o = 0
            End If

            ' save to disk
            DefaultName = RegionClass.RegionName(o)
            MySetting.WelcomeRegion = DefaultName
            MySetting.SaveSettings()

            '(replace spaces with underscore)
            DefaultName = DefaultName.Replace(" ", "_")    ' because this is a screwy thing they did in the INI file

            Dim onceflag As Boolean = False ' only do the DefaultName
            Dim counter As Integer = 0

            Try
                My.Computer.FileSystem.DeleteFile(gPath + "bin\Robust.tmp")
            Catch ex As Exception
                'Nothing to do, this was just cleanup
            End Try

            Using outputFile As New StreamWriter(gPath + "bin\Robust.tmp")
                reader = System.IO.File.OpenText(gPath + "bin\Robust.HG.ini")
                'now loop through each line
                While reader.Peek <> -1
                    line = reader.ReadLine()

                    If line.Contains("DefaultHGRegion, FallbackRegion") Then
                        ' flag lets us skip multi-lines
                        If onceflag = False Then
                            onceflag = True
                            line = "Region_" + DefaultName + " = " + """" + "DefaultRegion, DefaultHGRegion, FallbackRegion" + """"
                            outputFile.WriteLine(line)
                        End If
                    Else
                        outputFile.WriteLine(line)
                    End If

                End While
            End Using
            'close your reader
            reader.Close()

            Try
                Try
                    My.Computer.FileSystem.DeleteFile(gPath + "bin\Robust.HG.ini.bak")
                Catch ex As Exception
                    'Nothing to do, this was just cleanup
                End Try
                My.Computer.FileSystem.RenameFile(gPath + "bin\Robust.HG.ini", "Robust.HG.ini.bak")
                My.Computer.FileSystem.RenameFile(gPath + "bin\Robust.tmp", "Robust.HG.ini")
            Catch ex As Exception
                ErrorLog("Error:Set Default sims could not rename the file:" + ex.Message)
                My.Computer.FileSystem.RenameFile(gPath + "bin\Robust.HG.ini.bak", "Robust.HG.ini")
            End Try

        Catch ex As Exception
            MsgBox("Could not set default sim for visitors. Check the Common Settings panel.", vbInformation, "Settings")
        End Try

    End Sub

    Private Function SetIniData() As Boolean

        'mnuShow shows the DOS box for Opensimulator
        mnuShow.Checked = MySetting.ConsoleShow
        mnuHide.Checked = Not MySetting.ConsoleShow

        If MySetting.ConsoleShow Then
            Log("Info:Console will be shown")
        Else
            Log("Info:Console will not be shown")
        End If

        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ' set the defaults in the INI for the viewer to use. Painful to do as it's a Left hand side edit 

        SetDefaultSims()
        ''''''''''''''''''''''''''''''''''''''''''''''''

        ' TOSModule
        MySetting.LoadOtherIni(gPath + "bin\DivaTOS.ini", ";")

        'Disable it as it is broken for now.

        'MySetting.SetOtherIni("TOSModule", "Enabled", MySetting.TOSEnabled)
        MySetting.SetOtherIni("TOSModule", "Enabled", False.ToString)
        'MySetting.SetOtherIni("TOSModule", "Message", MySetting.TOSMessage)
        'MySetting.SetOtherIni("TOSModule", "Timeout", MySetting.TOSTimeout)
        MySetting.SetOtherIni("TOSModule", "ShowToLocalUsers", MySetting.ShowToLocalUsers.ToString)
        MySetting.SetOtherIni("TOSModule", "ShowToForeignUsers", MySetting.ShowToForeignUsers.ToString)
        MySetting.SetOtherIni("TOSModule", "TOS_URL", "http://" + MySetting.PublicIP + ":" + MySetting.HttpPort + "/wifi/termsofservice.html")
        MySetting.SaveOtherINI()

        MySetting.LoadOtherIni(gPath + "bin\config-include\Gridcommon.ini", ";")
        Dim ConnectionString = """" _
            + "Data Source=" + MySetting.RegionServer _
            + ";Database=" + MySetting.RegionDBName _
            + ";Port=" + MySetting.RegionPort _
            + ";User ID=" + MySetting.RegionDBUsername _
            + ";Password=" + MySetting.RegionDbPassword _
            + ";Old Guids=true;Allow Zero Datetime=true;" _
            + """"
        MySetting.SetOtherIni("DatabaseService", "ConnectionString", ConnectionString)
        MySetting.SaveOtherINI()

        ''''''''''''''''''''''''''''''''''''''''''
        ' Robust Process
        MySetting.LoadOtherIni(gPath + "bin\Robust.HG.ini", ";")

        ConnectionString = """" _
            + "Data Source=" + MySetting.RobustServer _
            + ";Database=" + MySetting.RobustDataBaseName _
            + ";Port=" + MySetting.MySqlPort _
            + ";User ID=" + MySetting.RobustUsername _
            + ";Password=" + MySetting.RobustPassword _
            + ";Old Guids=true;Allow Zero Datetime=true;" _
            + """"

        MySetting.SetOtherIni("DatabaseService", "ConnectionString", ConnectionString)
        MySetting.SetOtherIni("Const", "GridName", MySetting.SimName)
        MySetting.SetOtherIni("Const", "BaseURL", "http://" & MySetting.PublicIP)
        MySetting.SetOtherIni("Const", "PrivURL", "http://" + MySetting.PrivateURL)
        MySetting.SetOtherIni("Const", "PublicPort", MySetting.HttpPort) ' 8002
        MySetting.SetOtherIni("Const", "PrivatePort", MySetting.PrivatePort)
        MySetting.SetOtherIni("Const", "http_listener_port", MySetting.HttpPort)
        MySetting.SetOtherIni("GridInfoService", "welcome", MySetting.SplashPage)

        If MySetting.Suitcase() Then
            MySetting.SetOtherIni("HGInventoryService", "LocalServiceModule", "OpenSim.Services.HypergridService.dll:HGSuitcaseInventoryService")
        Else
            MySetting.SetOtherIni("HGInventoryService", "LocalServiceModule", "OpenSim.Services.HypergridService.dll:HGInventoryService")
        End If

        ' LSL emails
        MySetting.SetOtherIni("SMTP", "SMTP_SERVER_HOSTNAME", MySetting.SmtpHost)
        MySetting.SetOtherIni("SMTP", "SMTP_SERVER_PORT", MySetting.SmtpPort)
        MySetting.SetOtherIni("SMTP", "SMTP_SERVER_LOGIN", MySetting.SmtpUsername)
        MySetting.SetOtherIni("SMTP", "SMTP_SERVER_PASSWORD", MySetting.SmtpPassword)

        MySetting.SaveOtherINI()



        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ' Opensim.ini
        MySetting.LoadOtherIni(gPath + "bin\Opensim.proto", ";")

        If MySetting.LSL_HTTP Then
            ' do nothing - let them edit it
        Else
            MySetting.SetOtherIni("Network", "OutboundDisallowForUserScriptsExcept", MySetting.PrivateURL + "/32")
        End If
        MySetting.SetOtherIni("Network", "ExternalHostNameForLSL", MySetting.PublicIP)

        MySetting.SetOtherIni("DataSnapshot", "index_sims", MySetting.DataSnapshot().ToString)

        MySetting.SetOtherIni("PrimLimitsModule", "EnforcePrimLimits", CType(MySetting.Primlimits, String))

        If MySetting.Primlimits Then
            MySetting.SetOtherIni("Permissions", "permissionmodules", "DefaultPermissionsModule, PrimLimitsModule")
        Else
            MySetting.SetOtherIni("Permissions", "permissionmodules", "DefaultPermissionsModule")
        End If

        If MySetting.GDPR Then
            MySetting.SetOtherIni("DataSnapshot", "indexsims", "true")
        Else
            MySetting.SetOtherIni("DataSnapshot", "indexsims", "false")
        End If


        If MySetting.GloebitsEnable Then
            MySetting.SetOtherIni("Startup", "economymodule", "Gloebit")
        Else
            MySetting.SetOtherIni("Startup", "economymodule", "")
        End If

        ' LSL emails
        MySetting.SetOtherIni("SMTP", "SMTP_SERVER_HOSTNAME", MySetting.SmtpHost)
        MySetting.SetOtherIni("SMTP", "SMTP_SERVER_PORT", MySetting.SmtpPort)
        MySetting.SetOtherIni("SMTP", "SMTP_SERVER_LOGIN", MySetting.SmtpUsername)
        MySetting.SetOtherIni("SMTP", "SMTP_SERVER_PASSWORD", MySetting.SmtpPassword)
        MySetting.SetOtherIni("SMTP", "host_domain_header_from", MySetting.PublicIP)

        ' the old Clouds
        If MySetting.Clouds Then
            MySetting.SetOtherIni("Cloud", "enabled", "true")
            MySetting.SetOtherIni("Cloud", "density", MySetting.Density.ToString)
        Else
            MySetting.SetOtherIni("Cloud", "enabled", "false")
            MySetting.SetOtherIni("Cloud", "density", MySetting.Density.ToString)
        End If

        ' Gods

        If (MySetting.Region_owner_is_god Or MySetting.Region_manager_is_god) Then
            MySetting.SetOtherIni("Permissions", "allow_grid_gods", "true")
        Else
            MySetting.SetOtherIni("Permissions", "allow_grid_gods", "false")
        End If

        If (MySetting.Region_owner_is_god) Then
            MySetting.SetOtherIni("Permissions", "region_owner_is_god", "true")
        Else
            MySetting.SetOtherIni("Permissions", "region_owner_is_god", "false")
        End If

        If (MySetting.Region_manager_is_god) Then
            MySetting.SetOtherIni("Permissions", "region_manager_is_god", "true")
        Else
            MySetting.SetOtherIni("Permissions", "region_manager_is_god", "false")
        End If

        If (MySetting.Allow_grid_gods) Then
            MySetting.SetOtherIni("Permissions", "allow_grid_gods", "true")
        Else
            MySetting.SetOtherIni("Permissions", "allow_grid_gods", "false")
        End If


        ' Physics
        ' choices for meshmerizer, where Ubit's ODE requires a special one
        ' mesging = ZeroMesher
        ' meshing = Meshmerizer
        ' meshing = ubODEMeshmerizer

        ' 0 = physics = none
        ' 1 = OpenDynamicsEngine
        ' 2 = physics = BulletSim
        ' 3 = physics = BulletSim with threads
        ' 4 = physics = ubODE

        Select Case MySetting.Physics
            Case "0"
                MySetting.SetOtherIni("Startup", "meshing", "ZeroMesher")
                MySetting.SetOtherIni("Startup", "physics", "basicphysics")
                MySetting.SetOtherIni("Startup", "UseSeparatePhysicsThread", "false")
            Case "1"
                MySetting.SetOtherIni("Startup", "meshing", "Meshmerizer")
                MySetting.SetOtherIni("Startup", "physics", "OpenDynamicsEngine")
                MySetting.SetOtherIni("Startup", "UseSeparatePhysicsThread", "false")
            Case "2"
                MySetting.SetOtherIni("Startup", "meshing", "Meshmerizer")
                MySetting.SetOtherIni("Startup", "physics", "BulletSim")
                MySetting.SetOtherIni("Startup", "UseSeparatePhysicsThread", "false")
            Case "3"
                MySetting.SetOtherIni("Startup", "meshing", "Meshmerizer")
                MySetting.SetOtherIni("Startup", "physics", "BulletSim")
                MySetting.SetOtherIni("Startup", "UseSeparatePhysicsThread", "true")
            Case "4"
                MySetting.SetOtherIni("Startup", "meshing", "ubODEMeshmerizer")
                MySetting.SetOtherIni("Startup", "physics", "ubODE")
                MySetting.SetOtherIni("Startup", "UseSeparatePhysicsThread", "false")
            Case Else
                MySetting.SetOtherIni("Startup", "meshing", "Meshmerizer")
                MySetting.SetOtherIni("Startup", "physics", "BulletSim")
                MySetting.SetOtherIni("Startup", "UseSeparatePhysicsThread", "true")
        End Select

        MySetting.SetOtherIni("Const", "DiagnosticsPort", MySetting.DiagnosticPort)
        MySetting.SetOtherIni("Const", "GridName", MySetting.SimName)

        If MySetting.MapType = "None" Then
            MySetting.SetOtherIni("Map", "GenerateMaptiles", "false")
        ElseIf MySetting.MapType = "Simple" Then
            MySetting.SetOtherIni("Map", "GenerateMaptiles", "true")
            MySetting.SetOtherIni("Map", "MapImageModule", "MapImageModule")  ' versus Warp3DImageModule
            MySetting.SetOtherIni("Map", "TextureOnMapTile", "false")         ' versus true
            MySetting.SetOtherIni("Map", "DrawPrimOnMapTile", "false")
            MySetting.SetOtherIni("Map", "TexturePrims", "false")
            MySetting.SetOtherIni("Map", "RenderMeshes", "false")
        ElseIf MySetting.MapType = "Good" Then
            MySetting.SetOtherIni("Map", "GenerateMaptiles", "true")
            MySetting.SetOtherIni("Map", "MapImageModule", "Warp3DImageModule")  ' versus MapImageModule
            MySetting.SetOtherIni("Map", "TextureOnMapTile", "false")         ' versus true
            MySetting.SetOtherIni("Map", "DrawPrimOnMapTile", "false")
            MySetting.SetOtherIni("Map", "TexturePrims", "false")
            MySetting.SetOtherIni("Map", "RenderMeshes", "false")
        ElseIf MySetting.MapType = "Better" Then
            MySetting.SetOtherIni("Map", "GenerateMaptiles", "true")
            MySetting.SetOtherIni("Map", "MapImageModule", "Warp3DImageModule")  ' versus MapImageModule
            MySetting.SetOtherIni("Map", "TextureOnMapTile", "true")         ' versus true
            MySetting.SetOtherIni("Map", "DrawPrimOnMapTile", "true")
            MySetting.SetOtherIni("Map", "TexturePrims", "false")
            MySetting.SetOtherIni("Map", "RenderMeshes", "false")
        ElseIf MySetting.MapType = "Best" Then
            MySetting.SetOtherIni("Map", "GenerateMaptiles", "true")
            MySetting.SetOtherIni("Map", "MapImageModule", "Warp3DImageModule")  ' versus MapImageModule
            MySetting.SetOtherIni("Map", "TextureOnMapTile", "true")      ' versus true
            MySetting.SetOtherIni("Map", "DrawPrimOnMapTile", "true")
            MySetting.SetOtherIni("Map", "TexturePrims", "true")
            MySetting.SetOtherIni("Map", "RenderMeshes", "true")
        End If


        ' Autobackup
        If MySetting.AutoBackup Then
            Log("Info:Auto backup is On")
            MySetting.SetOtherIni("AutoBackupModule", "AutoBackup", "true")
        Else
            Log("Info:Auto backup is Off")
            MySetting.SetOtherIni("AutoBackupModule", "AutoBackup", "false")
        End If

        MySetting.SetOtherIni("AutoBackupModule", "AutoBackupInterval", MySetting.AutobackupInterval)
        MySetting.SetOtherIni("AutoBackupModule", "AutoBackupKeepFilesForDays", MySetting.KeepForDays.ToString)
        MySetting.SetOtherIni("AutoBackupModule", "AutoBackupDir", BackupPath())

        ' Voice
        If MySetting.VivoxEnabled Then
            MySetting.SetOtherIni("VivoxVoice", "enabled", "true")
        Else
            MySetting.SetOtherIni("VivoxVoice", "enabled", "false")
        End If
        MySetting.SetOtherIni("VivoxVoice", "vivox_admin_user", MySetting.Vivox_UserName)
        MySetting.SetOtherIni("VivoxVoice", "vivox_admin_password", MySetting.Vivox_password)

        MySetting.SaveOtherINI()

        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ' Wifi Settings

        DoWifi()

        DoGloebits()

        CopyOpensimProto()

        DoRegions()

        Return True


    End Function




    Public Sub DoGloebits()

        'Gloebits.ini
        MySetting.LoadOtherIni(gPath + "bin\Gloebit.ini", ";")
        If MySetting.GloebitsEnable Then

            MySetting.SetOtherIni("Gloebit", "Enabled", "True")
        Else
            MySetting.SetOtherIni("Gloebit", "Enabled", "False")
        End If

        If MySetting.GloebitsMode Then
            MySetting.SetOtherIni("Gloebit", "GLBEnvironment", "production")
            MySetting.SetOtherIni("Gloebit", "GLBKey", MySetting.GLProdKey)
            MySetting.SetOtherIni("Gloebit", "GLBSecret", MySetting.GLProdSecret)
        Else
            MySetting.SetOtherIni("Gloebit", "GLBEnvironment", "sandbox")
            MySetting.SetOtherIni("Gloebit", "GLBKey", MySetting.GLSandKey)
            MySetting.SetOtherIni("Gloebit", "GLBSecret", MySetting.GLSandSecret)
        End If

        MySetting.SetOtherIni("Gloebit", "GLBOwnerName", MySetting.GLBOwnerName)
        MySetting.SetOtherIni("Gloebit", "GLBOwnerEmail", MySetting.GLBOwnerEmail)

        Dim ConnectionString = """" + "Data Source = " + MySetting.RobustServer _
            + ";Database=" + MySetting.RobustDataBaseName _
            + ";Port=" + MySetting.MySqlPort _
            + ";User ID=" + MySetting.RobustUsername _
            + ";Password=" + MySetting.RobustPassword _
            + ";Old Guids=True;Allow Zero Datetime=True;" + """"


        MySetting.SetOtherIni("Gloebit", "GLBSpecificConnectionString", ConnectionString)

        MySetting.SaveOtherINI()

    End Sub


    Private Sub DoWifi()

        MySetting.LoadOtherIni(gPath + "bin\Wifi.ini", ";")

        Dim ConnectionString = """" _
                + "Data Source=" + "127.0.0.1" _
                + ";Database=" + MySetting.RobustDataBaseName _
                + ";Port=" + MySetting.MySqlPort _
                + ";User ID=" + MySetting.RobustUsername _
                + ";Password=" + MySetting.RobustPassword _
                + ";Old Guids=True;Allow Zero Datetime=True;" _
                + """"

        MySetting.SetOtherIni("DatabaseService", "ConnectionString", ConnectionString)

        ' Wifi Section

        If (MySetting.WifiEnabled) Then
            MySetting.SetOtherIni("WifiService", "Enabled", "True")
        Else
            MySetting.SetOtherIni("WifiService", "Enabled", "False")
        End If

        MySetting.SetOtherIni("WifiService", "GridName", MySetting.SimName)
        MySetting.SetOtherIni("WifiService", "LoginURL", "http://" + MySetting.PublicIP + ":" + MySetting.HttpPort)
        MySetting.SetOtherIni("WifiService", "WebAddress", "http://" + MySetting.PublicIP + ":" + MySetting.HttpPort)

        ' Wifi Admin'
        MySetting.SetOtherIni("WifiService", "AdminFirst", MySetting.AdminFirst)    ' Wifi
        MySetting.SetOtherIni("WifiService", "AdminLast", MySetting.AdminLast)      ' Admin
        MySetting.SetOtherIni("WifiService", "AdminPassword", MySetting.Password)   ' secret
        MySetting.SetOtherIni("WifiService", "AdminEmail", MySetting.AdminEmail)    ' send notificatins to this person

        'Gmail and other SMTP mailers
        ' Gmail requires you set to set low security access
        MySetting.SetOtherIni("WifiService", "SmtpHost", MySetting.SmtpHost)
        MySetting.SetOtherIni("WifiService", "SmtpPort", MySetting.SmtpPort)
        MySetting.SetOtherIni("WifiService", "SmtpUsername", MySetting.SmtpUsername)
        MySetting.SetOtherIni("WifiService", "SmtpPassword", MySetting.SmtpPassword)


        MySetting.SetOtherIni("WifiService", "HomeLocation", MySetting.WelcomeRegion & "/" + MySetting.HomeVectorX & "/" & MySetting.HomeVectorY & "/" & MySetting.HomeVectorZ)

        If MySetting.AccountConfirmationRequired Then
            MySetting.SetOtherIni("WifiService", "AccountConfirmationRequired", "true")
        Else
            MySetting.SetOtherIni("WifiService", "AccountConfirmationRequired", "false")
        End If

        MySetting.SaveOtherINI()

    End Sub

    Function CopyOpensimProto() As Boolean

        ' COPY OPENSIM.INI prototype to all region folders and set the Sim Name

        For Each X As Integer In RegionClass.RegionNumbers
            Diagnostics.Debug.Print("Count " + X.ToString)
            Dim regionName = RegionClass.RegionName(X)
            Dim pathname = RegionClass.IniPath(X)
            Diagnostics.Debug.Print(regionName)

            Try
                MySetting.LoadOtherIni(gPath + "bin\Opensim.proto", ";")
                MySetting.SetOtherIni("Const", "BaseHostname", MySetting.PublicIP)
                MySetting.SetOtherIni("Const", "PrivURL", "http://" + MySetting.PrivateURL)
                MySetting.SetOtherIni("Const", "PublicPort", MySetting.HttpPort) ' 8002
                MySetting.SetOtherIni("Const", "http_listener_port", RegionClass.RegionPort(X).ToString) ' varies with region
                Dim name = RegionClass.RegionName(X)

                ' save the http listener port away for the group
                RegionClass.GroupPort(X) = RegionClass.RegionPort(X)

                MySetting.SetOtherIni("Const", "PrivatePort", MySetting.PrivatePort) '8003
                MySetting.SetOtherIni("Const", "RegionFolderName", RegionClass.GroupName(X))
                MySetting.SetOtherIni("Const", "PrivatePort", MySetting.PrivatePort) '8003
                MySetting.SaveOtherINI()

                My.Computer.FileSystem.CopyFile(gPath + "bin\Opensim.proto", pathname + "Opensim.ini", True)

            Catch ex As Exception
                Print("Error:CopyOpensimProto failed to set the Opensim.ini for sim " + regionName + ":" + ex.Message)
                ErrorLog("Error:CopyOpensimProto Failed to set the Opensim.ini for sim " + regionName + ":" + ex.Message)
                Return False
            End Try

        Next

        Return True

    End Function
#End Region

#Region "Regions"

    Private Sub DoRegions()
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Regions - write all region.ini files with public IP and Public port

        Dim BirdFile = MyFolder + "\OutworldzFiles\Opensim\bin\addon-modules\OpenSimBirds\config\OpenSimBirds.ini"
        Try
            System.IO.File.Delete(BirdFile)
        Catch ex As Exception
        End Try
        Dim TideFile = MyFolder + "\OutworldzFiles\Opensim\bin\addon-modules\OpenSimTide\config\OpenSimTide.ini"
        Try
            System.IO.File.Delete(TideFile)
        Catch ex As Exception
        End Try

        ' has to be bound late so regions data is there.

        Dim fPort As String = MySetting.FirstRegionPort()
        If fPort = "" Then
            fPort = RegionClass.LowestPort().ToString
            MySetting.FirstRegionPort = fPort
            MySetting.SaveSettings()
        End If

        ' Self setting Region Ports
        Dim FirstPort As Integer = CType(MySetting.FirstRegionPort(), Integer)

        For Each RegionNum As Integer In RegionClass.RegionNumbers

            Dim simName = RegionClass.RegionName(RegionNum)

            MySetting.LoadOtherIni(RegionClass.RegionPath(RegionNum), ";")
            MySetting.SetOtherIni(simName, "InternalPort", RegionClass.RegionPort(RegionNum).ToString)
            MySetting.SetOtherIni(simName, "ExternalHostName", Convert.ToString(MySetting.PublicIP))

            ' not a standard INI, only use by the Dreamers
            If RegionClass.RegionEnabled(RegionNum) Then
                MySetting.SetOtherIni(simName, "Enabled", "True")
            Else
                MySetting.SetOtherIni(simName, "Enabled", "False")
            End If

            ' Extended in v 2.1
            MySetting.SetOtherIni(simName, "NonPhysicalPrimMax", Convert.ToString(RegionClass.NonPhysicalPrimMax(RegionNum)))
            MySetting.SetOtherIni(simName, "PhysicalPrimMax", Convert.ToString(RegionClass.PhysicalPrimMax(RegionNum)))
            If (MySetting.Primlimits) Then
                MySetting.SetOtherIni(simName, "MaxPrims", Convert.ToString(RegionClass.MaxPrims(RegionNum)))
            Else
                MySetting.SetOtherIni(simName, "MaxPrims", "")
            End If
            MySetting.SetOtherIni(simName, "MaxAgents", Convert.ToString(RegionClass.MaxAgents(RegionNum)))
            MySetting.SetOtherIni(simName, "ClampPrimSize", Convert.ToString(RegionClass.ClampPrimSize(RegionNum)))

            ' Extended in v 2.31 optional things

            If RegionClass.MapType(RegionNum) = "None" Then
                MySetting.SetOtherIni(simName, "GenerateMaptiles", "False")
            ElseIf RegionClass.MapType(RegionNum) = "Simple" Then
                MySetting.SetOtherIni(simName, "GenerateMaptiles", "True")
                MySetting.SetOtherIni(simName, "MapImageModule", "MapImageModule")  ' versus Warp3DImageModule
                MySetting.SetOtherIni(simName, "TextureOnMapTile", "False")         ' versus True
                MySetting.SetOtherIni(simName, "DrawPrimOnMapTile", "False")
                MySetting.SetOtherIni(simName, "TexturePrims", "False")
                MySetting.SetOtherIni(simName, "RenderMeshes", "False")
            ElseIf RegionClass.MapType(RegionNum) = "Good" Then
                MySetting.SetOtherIni(simName, "GenerateMaptiles", "True")
                MySetting.SetOtherIni(simName, "MapImageModule", "Warp3DImageModule")  ' versus MapImageModule
                MySetting.SetOtherIni(simName, "TextureOnMapTile", "False")         ' versus True
                MySetting.SetOtherIni(simName, "DrawPrimOnMapTile", "False")
                MySetting.SetOtherIni(simName, "TexturePrims", "False")
                MySetting.SetOtherIni(simName, "RenderMeshes", "False")
            ElseIf RegionClass.MapType(RegionNum) = "Better" Then
                MySetting.SetOtherIni(simName, "GenerateMaptiles", "True")
                MySetting.SetOtherIni(simName, "MapImageModule", "Warp3DImageModule")  ' versus MapImageModule
                MySetting.SetOtherIni(simName, "TextureOnMapTile", "True")         ' versus True
                MySetting.SetOtherIni(simName, "DrawPrimOnMapTile", "True")
                MySetting.SetOtherIni(simName, "TexturePrims", "False")
                MySetting.SetOtherIni(simName, "RenderMeshes", "False")
            ElseIf RegionClass.MapType(RegionNum) = "Best" Then
                MySetting.SetOtherIni(simName, "GenerateMaptiles", "True")
                MySetting.SetOtherIni(simName, "MapImageModule", "Warp3DImageModule")  ' versus MapImageModule
                MySetting.SetOtherIni(simName, "TextureOnMapTile", "True")      ' versus True
                MySetting.SetOtherIni(simName, "DrawPrimOnMapTile", "True")
                MySetting.SetOtherIni(simName, "TexturePrims", "True")
                MySetting.SetOtherIni(simName, "RenderMeshes", "True")
            Else
                MySetting.SetOtherIni(simName, "GenerateMaptiles", "")
                MySetting.SetOtherIni(simName, "MapImageModule", "")  ' versus MapImageModule
                MySetting.SetOtherIni(simName, "TextureOnMapTile", "")      ' versus True
                MySetting.SetOtherIni(simName, "DrawPrimOnMapTile", "")
                MySetting.SetOtherIni(simName, "TexturePrims", "")
                MySetting.SetOtherIni(simName, "RenderMeshes", "")
            End If

            MySetting.SetOtherIni(simName, "Physics", RegionClass.Physics(RegionNum))
            MySetting.SetOtherIni(simName, "MaxPrims", RegionClass.MaxPrims(RegionNum))
            MySetting.SetOtherIni(simName, "AllowGods", RegionClass.AllowGods(RegionNum))
            MySetting.SetOtherIni(simName, "RegionGod", RegionClass.RegionGod(RegionNum))
            MySetting.SetOtherIni(simName, "ManagerGod", RegionClass.ManagerGod(RegionNum))
            MySetting.SetOtherIni(simName, "RegionSnapShot", RegionClass.RegionSnapShot(RegionNum).ToString)
            MySetting.SetOtherIni(simName, "Birds", RegionClass.Birds(RegionNum))
            MySetting.SetOtherIni(simName, "Tides", RegionClass.Tides(RegionNum))
            MySetting.SetOtherIni(simName, "Teleport", RegionClass.Teleport(RegionNum))

            MySetting.SaveOtherINI()

            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            ' region.ini
            MySetting.LoadOtherIni(gPath + "bin\Regions\" + RegionClass.GroupName(RegionNum) + "\Opensim.ini", ";")

            If RegionClass.MapType(RegionNum) = "Simple" Then
                MySetting.SetOtherIni("Map", "GenerateMaptiles", "True")
                MySetting.SetOtherIni("Map", "MapImageModule", "MapImageModule")  ' versus Warp3DImageModule
                MySetting.SetOtherIni("Map", "TextureOnMapTile", "False")         ' versus True
                MySetting.SetOtherIni("Map", "DrawPrimOnMapTile", "False")
                MySetting.SetOtherIni("Map", "TexturePrims", "False")
                MySetting.SetOtherIni("Map", "RenderMeshes", "False")
            ElseIf RegionClass.MapType(RegionNum) = "Good" Then
                MySetting.SetOtherIni(simName, "GenerateMaptiles", "True")
                MySetting.SetOtherIni("Map", "MapImageModule", "Warp3DImageModule")  ' versus MapImageModule
                MySetting.SetOtherIni("Map", "TextureOnMapTile", "False")         ' versus True
                MySetting.SetOtherIni("Map", "DrawPrimOnMapTile", "False")
                MySetting.SetOtherIni("Map", "TexturePrims", "False")
                MySetting.SetOtherIni("Map", "RenderMeshes", "False")
            ElseIf RegionClass.MapType(RegionNum) = "Better" Then
                MySetting.SetOtherIni("Map", "GenerateMaptiles", "True")
                MySetting.SetOtherIni("Map", "MapImageModule", "Warp3DImageModule")  ' versus MapImageModule
                MySetting.SetOtherIni("Map", "TextureOnMapTile", "True")         ' versus True
                MySetting.SetOtherIni("Map", "DrawPrimOnMapTile", "True")
                MySetting.SetOtherIni("Map", "TexturePrims", "False")
                MySetting.SetOtherIni("Map", "RenderMeshes", "False")
            ElseIf RegionClass.MapType(RegionNum) = "Best" Then
                MySetting.SetOtherIni("Map", "GenerateMaptiles", "True")
                MySetting.SetOtherIni("Map", "MapImageModule", "Warp3DImageModule")  ' versus MapImageModule
                MySetting.SetOtherIni("Map", "TextureOnMapTile", "True")      ' versus True
                MySetting.SetOtherIni("Map", "DrawPrimOnMapTile", "True")
                MySetting.SetOtherIni("Map", "TexturePrims", "True")
                MySetting.SetOtherIni("Map", "RenderMeshes", "True")
            End If


            Select Case RegionClass.Physics(RegionNum)
                Case "0"
                    MySetting.SetOtherIni("Startup", "meshing", "ZeroMesher")
                    MySetting.SetOtherIni("Startup", "physics", "basicphysics")
                    MySetting.SetOtherIni("Startup", "UseSeparatePhysicsThread", "False")
                Case "1"
                    MySetting.SetOtherIni("Startup", "meshing", "Meshmerizer")
                    MySetting.SetOtherIni("Startup", "physics", "OpenDynamicsEngine")
                    MySetting.SetOtherIni("Startup", "UseSeparatePhysicsThread", "False")
                Case "2"
                    MySetting.SetOtherIni("Startup", "meshing", "Meshmerizer")
                    MySetting.SetOtherIni("Startup", "physics", "BulletSim")
                    MySetting.SetOtherIni("Startup", "UseSeparatePhysicsThread", "False")
                Case "3"
                    MySetting.SetOtherIni("Startup", "meshing", "Meshmerizer")
                    MySetting.SetOtherIni("Startup", "physics", "BulletSim")
                    MySetting.SetOtherIni("Startup", "UseSeparatePhysicsThread", "True")
                Case "4"
                    MySetting.SetOtherIni("Startup", "meshing", "ubODEMeshmerizer")
                    MySetting.SetOtherIni("Startup", "physics", "ubODE")
                    MySetting.SetOtherIni("Startup", "UseSeparatePhysicsThread", "False")
                Case Else
                    ' do nothing
            End Select


            If RegionClass.RegionGod(RegionNum) = "True" Or RegionClass.ManagerGod(RegionNum) = "True" Then
                MySetting.SetOtherIni("Permissions", "allow_grid_gods", "True")
            Else
                MySetting.SetOtherIni("Permissions", "allow_grid_gods", MySetting.Allow_grid_gods.ToString)
            End If

            If RegionClass.RegionGod(RegionNum) = "True" Then
                MySetting.SetOtherIni("Permissions", "region_owner_is_god", "True")
            Else
                MySetting.SetOtherIni("Permissions", "region_owner_is_god", MySetting.Region_owner_is_god.ToString)
            End If

            If RegionClass.ManagerGod(RegionNum) = "True" Then
                MySetting.SetOtherIni("Permissions", "region_manager_is_god", "True")
            Else
                MySetting.SetOtherIni("Permissions", "region_manager_is_god", MySetting.Region_manager_is_god.ToString)
            End If

            If RegionClass.AllowGods(RegionNum) = "True" Then
                MySetting.SetOtherIni("Permissions", "allow_grid_gods", "True")
            Else
                MySetting.SetOtherIni("Permissions", "allow_grid_gods", MySetting.Allow_grid_gods.ToString)
            End If

            MySetting.SaveOtherINI()

            If MySetting.BirdsModuleStartup Then
                Dim Birds As String = ""
                If RegionClass.Birds(RegionNum) = "True" Then
                    Birds = "True"
                Else
                    Birds = "False"
                End If


                Dim BirdData As String = "[" + simName + "]" + vbCrLf &
                ";this Is the default And determines whether the module does anything" & vbCrLf &
                "BirdsModuleStartup = True" & vbCrLf & vbCrLf &
                ";set to false to disable the birds from appearing in this region" & vbCrLf &
                "BirdsEnabled = " & Birds & vbCrLf & vbCrLf &
                ";which channel do we listen on for in world commands" & vbCrLf &
                "BirdsChatChannel = " + MySetting.BirdsChatChannel.ToString() & vbCrLf & vbCrLf &
                ";the number of birds to flock" & vbCrLf &
                "BirdsFlockSize = " + MySetting.BirdsFlockSize.ToString() & vbCrLf & vbCrLf &
                ";how far each bird can travel per update" & vbCrLf &
                "BirdsMaxSpeed = " + MySetting.BirdsMaxSpeed.ToString() & vbCrLf & vbCrLf &
                ";the maximum acceleration allowed to the current velocity of the bird" & vbCrLf &
                "BirdsMaxForce = " + MySetting.BirdsMaxForce.ToString & vbCrLf & vbCrLf &
                ";max distance for other birds to be considered in the same flock as us" & vbCrLf &
                "BirdsNeighbourDistance = " + MySetting.BirdsNeighbourDistance.ToString() & vbCrLf & vbCrLf &
                ";how far away from other birds we would Like To stay" & vbCrLf &
                "BirdsDesiredSeparation = " + MySetting.BirdsDesiredSeparation.ToString() & vbCrLf & vbCrLf &
                ";how close To the edges Of things can we Get without being worried" & vbCrLf &
                "BirdsTolerance = " + MySetting.BirdsTolerance.ToString() & vbCrLf & vbCrLf &
                ";how close To the edge Of a region can we Get?" & vbCrLf &
                "BirdsBorderSize = " + MySetting.BirdsBorderSize.ToString() & vbCrLf & vbCrLf &
                ";how high are we allowed To flock" & vbCrLf &
                "BirdsMaxHeight = " + MySetting.BirdsMaxHeight.ToString() & vbCrLf & vbCrLf &
                ";By Default the Module will create a flock Of plain wooden spheres," & vbCrLf &
                ";however this can be overridden To the name Of an existing prim that" & vbCrLf &
                ";needs To already exist In the scene - i.e. be rezzed In the region." & vbCrLf &
                "BirdsPrim = " + MySetting.BirdsPrim & vbCrLf & vbCrLf &
                ";who Is allowed to send commands via chat Or script List of UUIDs Or ESTATE_OWNER Or ESTATE_MANAGER" & vbCrLf &
                ";Or everyone if Not specified" & vbCrLf &
                "BirdsAllowedControllers = ESTATE_OWNER, ESTATE_MANAGER" & vbCrLf & vbCrLf & vbCrLf


                IO.File.AppendAllText(BirdFile, BirdData, Encoding.Default) 'The text file will be created if it does not already exist  

            End If

            If MySetting.TideEnabled Then

                Dim Tides As String = ""
                If RegionClass.Tides(RegionNum) = "True" Then
                    Tides = "True"
                Else
                    Tides = "False"
                End If

                Dim TideData As String = ";; Set the Tide settings per named region" & vbCrLf &
                "[" + simName + "]" + vbCrLf &
            ";this determines whether the module does anything in this region" & vbCrLf &
            ";# {TideEnabled} {} {Enable the tide to come in and out?} {true false} false" & vbCrLf &
            "TideEnabled = " & Tides & vbCrLf &
                vbCrLf &
            ";; Tides currently only work on single regions And varregions (non megaregions) " & vbCrLf &
            ";# surrounded completely by water" & vbCrLf &
            ";; Anything else will produce weird results where you may see a big" & vbCrLf &
            ";; vertical 'step' in the ocean" & vbCrLf &
            ";; update the tide every x simulator frames" & vbCrLf &
            "TideUpdateRate = 50" & vbCrLf &
                vbCrLf &
            ";; low And high water marks in metres" & vbCrLf &
            "TideHighWater = " & MySetting.TideHighLevel() & vbCrLf &
            "TideLowWater = " & MySetting.TideLowLevel() & vbCrLf &
            vbCrLf &
            ";; how long in seconds for a complete cycle time low->high->low" & vbCrLf &
            "TideCycleTime = " & MySetting.CycleTime() & vbCrLf &
                vbCrLf &
            ";; provide tide information on the console?" & vbCrLf &
            "TideInfoDebug = " & MySetting.TideInfoDebug.ToString & vbCrLf &
                vbCrLf &
            ";; chat tide info to the whole region?" & vbCrLf &
            "TideInfoBroadcast = " & MySetting.BroadcastTideInfo() & vbCrLf &
                vbCrLf &
            ";; which channel to region chat on for the full tide info" & vbCrLf &
            "TideInfoChannel = " & MySetting.TideInfoChannel & vbCrLf &
            vbCrLf &
            ";; which channel to region chat on for just the tide level in metres" & vbCrLf &
            "TideLevelChannel = " & MySetting.TideLevelChannel() & vbCrLf &
                vbCrLf &
            ";; How many times to repeat Tide Warning messages at high/low tide" & vbCrLf &
            "TideAnnounceCount = 1" & vbCrLf & vbCrLf & vbCrLf & vbCrLf


                IO.File.AppendAllText(TideFile, TideData, Encoding.Default) 'The text file will be created if it does not already exist 

            End If

        Next

    End Sub

    Public Sub SetRegionINI(regionname As String, key As String, value As String)

        Dim X = RegionClass.FindRegionByName(regionname)
        MySetting.LoadOtherIni(RegionClass.RegionPath(X), ";")
        MySetting.SetOtherIni(regionname, key, value)
        MySetting.SaveOtherINI()

    End Sub
    Public Sub CheckDefaultPorts()
        Try
            If MySetting.DiagnosticPort = MySetting.HttpPort _
            Or MySetting.DiagnosticPort = MySetting.PrivatePort _
            Or MySetting.HttpPort = MySetting.PrivatePort _
            Or CType(MySetting.HttpPort, Double) < 1024 Then
                MySetting.DiagnosticPort = "8001"
                MySetting.HttpPort = "8002"
                MySetting.PrivatePort = "8003"

                MsgBox("Port conflict detected. Sim Ports have been reset to the defaults", vbInformation, "Error")
            End If

        Catch
        End Try


    End Sub


#End Region

#Region "ToolBars"

    Private Sub ClearCachesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClearCachesToolStripMenuItem.Click

        ' Set the new form's desktop location so it appears below and
        ' to the right of the current form.
        FormCaches.Close()
        FormCaches = New FormCaches
        FormCaches.Activate()
        FormCaches.Visible = True

    End Sub


    Private Sub ToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem2.Click
        Print("Starting UPnp Control Panel")
        Dim pi As ProcessStartInfo = New ProcessStartInfo()
        pi.Arguments = ""
        pi.FileName = MyFolder & "\UPnpPortForwardManager.exe"
        pi.WindowStyle = ProcessWindowStyle.Normal
        Dim ProcessUpnp As Process = New Process()
        ProcessUpnp.StartInfo = pi
        Try
            ProcessUpnp.Start()
        Catch ex As Exception
            ErrorLog("ErrorUPnp failed to launch: " + ex.Message)
        End Try
    End Sub

    Private Sub BusyButton_Click(sender As Object, e As EventArgs) Handles BusyButton.Click

        If gStopping = True Then

            Print("Stopped")
            For Each X As Integer In RegionClass.RegionNumbers
                RegionClass.ShuttingDown(X) = False
                RegionClass.Booted(X) = False
                RegionClass.WarmingUp(X) = False
                RegionClass.ProcessID(X) = 0
                RegionClass.Timer(X) = REGION_TIMER.STOPPED
            Next

            Buttons(StartButton)

            Print("Opensim Is Stopped")
            ProgressBar1.Value = 0
            ProgressBar1.Visible = False
            Return
        End If

        Print("Stopping")
        Application.DoEvents()

        Buttons(StartButton)
        Print("Opensim Is Aborted")
        ProgressBar1.Value = 0
        ProgressBar1.Visible = False


    End Sub

    Private Sub AdminUIToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ViewWebUI.Click
        If OpensimIsRunning() Then
            Dim webAddress As String = "http://127.0.0.1:" + MySetting.HttpPort
            Process.Start(webAddress)
            Print("Log in as '" + MySetting.AdminFirst + " " + MySetting.AdminLast + "' with a password of " + MySetting.Password + " to add user accounts.")
        Else
            Print("Opensim is not running. Cannot open the Web Interface.")
        End If
    End Sub

    Private Sub LoopBackToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoopBackToolStripMenuItem.Click
        Dim webAddress As String = gDomain + "/Outworldz_Installer/Loopback.htm"
        Process.Start(webAddress)
    End Sub

    Private Sub MoreContentToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MoreContentToolStripMenuItem.Click
        Dim webAddress As String = gDomain + "/cgi/freesculpts.plx"
        Process.Start(webAddress)
        Print("Drag and drop Backup.Oar, or any OAR or IAR files to load into your Sim")
    End Sub

    Private Sub AdvancedSettingsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AdvancedSettingsToolStripMenuItem.Click

        If gInitted Then
            Adv.Activate()
            Adv.Visible = True
        End If

    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        Dim webAddress As String = gDomain + "/Outworldz_Installer/PortForwarding.htm"
        Process.Start(webAddress)
    End Sub
#End Region

#Region "Robust"

    Public Sub RobustCommand(command As String)

        Try
            Dim p = Process.GetProcessById(gRobustProcID)
            ShowWindow(p.MainWindowHandle, SHOW_WINDOW.SW_RESTORE)
            AppActivate(gRobustProcID)
            SendKeys.SendWait(command)
        Catch ex As Exception
            Log("Warn:" + ex.Message)
        End Try

    End Sub

    Public Sub StartIcecast()

        If Not MySetting.SC_Enable Then
            Return
        End If

        Try
            My.Computer.FileSystem.DeleteFile(MyFolder + "\Outworldzfiles\Icecast\log\access.log")
        Catch ex As Exception
        End Try

        Try
            My.Computer.FileSystem.DeleteFile(MyFolder + "\Outworldzfiles\Icecast\log\error.log")
        Catch ex As Exception
        End Try

        gIcecastProcID = 0
        Print("Starting Icecast")

        Try
            IcecastProcess.EnableRaisingEvents = True
            IcecastProcess.StartInfo.UseShellExecute = True ' so we can redirect streams
            IcecastProcess.StartInfo.FileName = MyFolder + "\Outworldzfiles\icecast\icecast.bat"
            IcecastProcess.StartInfo.CreateNoWindow = False
            IcecastProcess.StartInfo.WorkingDirectory = MyFolder + "\Outworldzfiles\icecast"

            If MySetting.SC_Show Then
                IcecastProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal
            Else
                IcecastProcess.StartInfo.WindowStyle = ProcessWindowStyle.Minimized
            End If

            'IcecastProcess.StartInfo.Arguments = "-c .\icecast_run.xml"
            IcecastProcess.Start()
            gIcecastProcID = IcecastProcess.Id

            While Not SetWindowTextCall(IcecastProcess.MainWindowHandle, "Icecast")
                Sleep(1000)
            End While

            Try
                ShowWindow(IcecastProcess.MainWindowHandle, SHOW_WINDOW.SW_MINIMIZE)
            Catch
            End Try

        Catch ex As Exception
            Print("Error: Icecast did not start: " + ex.Message)
            ErrorLog("Error: Icecast did not start: " + ex.Message)
        End Try

    End Sub

    Public Function Start_Robust() As Boolean

        If IsRobustRunning() Then
            'Print("Robust is already running")
            Return True
        End If

        gRobustProcID = Nothing
        Print("Starting Robust")

        Try
            RobustProcess.EnableRaisingEvents = True
            RobustProcess.StartInfo.UseShellExecute = True ' so we can redirect streams
            RobustProcess.StartInfo.FileName = gPath + "bin\robust.exe"

            RobustProcess.StartInfo.CreateNoWindow = False
            RobustProcess.StartInfo.WorkingDirectory = gPath + "bin"
            RobustProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal
            RobustProcess.StartInfo.Arguments = "-inifile Robust.HG.ini"
            RobustProcess.Start()
            gRobustProcID = RobustProcess.Id

            Sleep(1000)

            While Not SetWindowTextCall(RobustProcess.MainWindowHandle, "Robust")
                Sleep(1000)
            End While


        Catch ex As Exception
            Print("Error: Robust did not start: " + ex.Message)
            ErrorLog("Error: Robust did not start: " + ex.Message)
            KillAll()
            Buttons(StartButton)
            Return False
        End Try

        ' Wait for Opensim to start listening 

        Dim counter = 0
        While Not IsRobustRunning() And OpensimIsRunning
            Application.DoEvents()
            BumpProgress(1)
            counter = counter + 1
            ' wait a minute for it to start
            If counter > 100 Then
                Print("Error:Robust failed to start")
                KillAll()
                Buttons(StartButton)
                Dim yesno = MsgBox("Robust did not start. Do you want to see the log file?", vbYesNo, "Error")
                If (yesno = vbYes) Then
                    Dim Log As String = """" + MyFolder + "\OutworldzFiles\Opensim\bin\Robust.log" + """"
                    System.Diagnostics.Process.Start("wordpad.exe", Log)
                End If
                Buttons(StartButton)
                Return False
            End If
            Application.DoEvents()
            Sleep(100)

        End While

        If MySetting.ConsoleShow = False Then
            Try
                Dim p = Process.GetProcessById(gRobustProcID)
                ShowWindow(p.MainWindowHandle, SHOW_WINDOW.SW_MINIMIZE)
            Catch
            End Try

        End If

        Log("Info:Robust is running")


        Return True

    End Function


#End Region

#Region "Opensimulator"

    Public Function Start_Opensimulator() As Boolean

        'If OpensimIsRunning() = False Then Return True
        gStopping = False
        For Each X As Integer In RegionClass.RegionNumbers
            RegionClass.ShuttingDown(X) = False
            RegionClass.Booted(X) = False
            RegionClass.WarmingUp(X) = False
            RegionClass.ProcessID(X) = 0
            RegionClass.Timer(X) = REGION_TIMER.STOPPED
        Next

        Try
            ' Boot them up
            For Each x In RegionClass.RegionNumbers()
                If RegionClass.RegionEnabled(x) And Not gStopping Then
                    If Not Boot(RegionClass.RegionName(x)) Then
                        'Print("Boot skipped for " + RegionClass.RegionName(x))
                    End If

                    If MySetting.Sequential Then
                        Dim ctr = 60 * 3 ' 3 minute max to start a region
                        Dim WaitForIt = True
                        While WaitForIt
                            Sleep(1000)

                            If RegionClass.RegionEnabled(x) _
                                And Not gStopping _
                                And RegionClass.WarmingUp(x) _
                                And Not RegionClass.ShuttingDown(x) _
                                And Not RegionClass.Booted(x) Then
                                WaitForIt = True
                            Else
                                WaitForIt = False
                            End If
                            ctr = ctr - 1
                            If ctr <= 0 Then WaitForIt = False

                        End While
                    Else

                        Dim ctr = 60 * 3 ' 3 minute max to start a region
                        Dim WaitForIt = True
                        While WaitForIt
                            Sleep(1000)
                            Dim CPUTime = cpu.NextValue()
                            Diagnostics.Debug.Print(CPUTime.ToString)

                            If CPUTime < gCPUMAX Then
                                WaitForIt = False
                            End If

                            ctr = ctr - 1
                            If ctr <= 0 Then WaitForIt = False

                        End While
                    End If

                End If

                Application.DoEvents()
            Next


        Catch ex As Exception
            Diagnostics.Debug.Print(ex.Message)
            Print("Unable to boot some regions")
        End Try


        Return True

    End Function



#End Region

#Region "Exited"
    ' Handle Exited event and display process information.
    Private Sub RobustProcess_Exited(ByVal sender As Object, ByVal e As System.EventArgs) Handles RobustProcess.Exited

        gRobustProcID = Nothing

        If gExiting Then Return
        Dim yesno = MsgBox("Robust exited. Do you want to see the error log file?", vbYesNo, "Error")
        If (yesno = vbYes) Then
            Dim MysqlLog As String = MyFolder + "\OutworldzFiles\Opensim\bin\Robust.log"
            System.Diagnostics.Process.Start("notepad.exe", MysqlLog)
        End If

    End Sub

    Private Sub Mysql_Exited(ByVal sender As Object, ByVal e As System.EventArgs) Handles ProcessMySql.Exited

        If gExiting Then Return

        OpensimIsRunning() = False

        Dim yesno = MsgBox("Mysql exited. Do you want to see the error log file?", vbYesNo, "Error")
        If (yesno = vbYes) Then
            Dim MysqlLog As String = MyFolder + "\OutworldzFiles\mysql\data"
            Dim files() As String
            files = Directory.GetFiles(MysqlLog, "*.err", SearchOption.TopDirectoryOnly)
            For Each FileName As String In files
                System.Diagnostics.Process.Start("notepad.exe", FileName)
            Next
        End If
    End Sub

    Private Sub IceCast_Exited(ByVal sender As Object, ByVal e As System.EventArgs) Handles IcecastProcess.Exited

        If gExiting Then Return
        Dim yesno = MsgBox("Icecast quit. Do you want to see the error log file?", vbYesNo, "Error")
        If (yesno = vbYes) Then
            Dim IceCastLog As String = MyFolder + "\Outworldzfiles\Icecast\log\error.log"
            System.Diagnostics.Process.Start("notepad.exe", IceCastLog)
        End If

    End Sub
#End Region

#Region "ExitHandlers"

    Private Sub DoExitHandlerPoll()


        ' Delete off end of list so we don't skip over one
        If ExitList.Count = 0 Then Return

        ExitList.Reverse()

        ' do first to last by counting backwards
        For LOOPVAR = ExitList.Count - 1 To 0 Step -1
            Dim RegionName As String
            Try
                RegionName = ExitList(LOOPVAR) ' recover the PID as integer
                If RegionName <> "Form1" Then
                    Log("Info:Shutdown of " & RegionName & " Detected")
                End If

            Catch
                ExitList.Clear()
                Return
            End Try


            ' find any region in the dos box that exited.
            Dim RegionNumber As Integer = -1
            Dim LNames As New List(Of Integer)
            LNames = RegionClass.RegionListByGroupNum(RegionName)

            For Each R In LNames
                RegionNumber = R
            Next
            ' must have been found, its possible none was found, and that would be 0, the first one, so we skip that.
            If LNames.Count < 0 Then
                Log("impossible error")
            Else

                Dim Groupname = RegionClass.GroupName(RegionNumber)
                Dim ShouldIRestart = RegionClass.Timer(RegionNumber)
                Log("Info:" + Groupname + " Exited with Timer status " + ShouldIRestart.ToString)
                UpdateView = True ' make form refresh
                ' Maybe we crashed during warmup.  Skip prompt if auto restarting
                If RegionClass.WarmingUp(RegionNumber) = True And ShouldIRestart >= 0 Then
                    Dim yesno = MsgBox(RegionClass.RegionName(RegionNumber) + " in DOS Box " + Groupname + " quit while booting up. Do you want to see the log file?", vbYesNo, "Error")
                    If (yesno = vbYes) Then
                        System.Diagnostics.Process.Start("notepad.exe", RegionClass.IniPath(RegionNumber) + "Opensim.log")
                        ShouldIRestart = RegionClass.Timer(RegionNumber)
                    End If
                    StopGroup(Groupname)

                ElseIf RegionClass.Booted(RegionNumber) = True And ShouldIRestart > 0 Then
                    ' prompt if crashed.  Skip prompt if auto restarting
                    Dim yesno = MsgBox(RegionClass.RegionName(RegionNumber) + " in DOS Box " + Groupname + " quit unexpectedly. Do you want to see the log file?", vbYesNo, "Error")
                    If (yesno = vbYes) Then
                        System.Diagnostics.Process.Start("notepad.exe", RegionClass.IniPath(RegionNumber) + "Opensim.log")
                        ShouldIRestart = RegionClass.Timer(RegionNumber)
                    End If
                    StopGroup(Groupname)
                Else
                    StopGroup(Groupname)
                End If

                ' Auto restart if negative 1
                If ShouldIRestart = REGION_TIMER.RESTART_PENDING And OpensimIsRunning() And Not gExiting Then
                    UpdateView = True ' make form refresh
                    PrintFast("Restart Queued for " + Groupname)
                    RegionClass.Timer(RegionNumber) = REGION_TIMER.RESTARTING ' signal a restart is needed (-2)
                Else
                    PrintFast(Groupname + " stopped")
                End If

                Try
                    ExitList.RemoveAt(LOOPVAR)
                Catch ex As Exception
                    ErrorLog("Error:Something fucky in region RemoveAt:" + ex.Message)
                    ErrorLog("LOOPVAR:" & LOOPVAR.ToString & " Count: " & ExitList.Count)
                End Try
            End If

        Next

    End Sub

    Private Sub StopGroup(Groupname As String)

        Log(Groupname + " Group is now stopped, time is unchanged")
        For Each X In RegionClass.RegionListByGroupNum(Groupname)
            Log(RegionClass.RegionName(X) + " Stopped")
            RegionClass.Booted(X) = False
            RegionClass.WarmingUp(X) = False
            RegionClass.ShuttingDown(X) = False
        Next

        UpdateView = True ' make form refresh

    End Sub
    ''' <summary>
    ''' Creates and exit handler for each region
    ''' </summary>
    ''' <returns>a process handle</returns>
    Public Function GetNewProcess() As Process

        Dim handle = New Handler
        Return handle.Init(RegionHandles, ExitList)

    End Function
    ''' <summary>
    ''' Starts Opensim for a given name
    ''' </summary>
    ''' <param name="BootName"> Name of region to start</param>
    ''' <returns>success = true</returns>
    Public Function Boot(BootName As String) As Boolean

        If gStopping Then Return True

        OpensimIsRunning() = True
        gExiting = False
        Buttons(StopButton)

        Log("Region: Starting Region " + BootName)

        Dim RegionNumber = RegionClass.FindRegionByName(BootName)
        If RegionClass.Booted(RegionNumber) Then
            Log("Region " + BootName + " skipped as it is already booted")
            Return True
        End If

        If RegionClass.WarmingUp(RegionNumber) Then
            Log("Region " + BootName + " skipped as it is already WarmingUp")
            Return True
        End If

        If RegionClass.ShuttingDown(RegionNumber) Then
            Log("Region " + BootName + " skipped as it is already ShuttingDown")
            Return True
        End If

        Dim isRegionRunning = CheckPort("127.0.0.1", RegionClass.GroupPort(RegionNumber))
        If isRegionRunning Then
            Log("Region " + BootName + "failed to start as it is already running")
            RegionClass.WarmingUp(RegionNumber) = False
            RegionClass.Booted(RegionNumber) = True
            RegionClass.ShuttingDown(RegionNumber) = False
            Return False
        End If

        Environment.SetEnvironmentVariable("OSIM_LOGPATH", gPath + "bin\Regions\" + RegionClass.GroupName(RegionNumber))

        Dim myProcess As Process = GetNewProcess()
        Dim Groupname = RegionClass.GroupName(RegionNumber)
        Print("Starting " + Groupname)
        Try
            myProcess.EnableRaisingEvents = True
            myProcess.StartInfo.UseShellExecute = True ' so we can redirect streams
            myProcess.StartInfo.WorkingDirectory = gPath + "bin"

            myProcess.StartInfo.FileName = """" + gPath + "bin\OpenSim.exe" + """"
            myProcess.StartInfo.CreateNoWindow = False

            myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal

            myProcess.StartInfo.Arguments = " -inidirectory=" & """" & "./Regions/" & RegionClass.GroupName(RegionNumber) + """"

            Try
                My.Computer.FileSystem.DeleteFile(gPath + "bin\Regions\" & RegionClass.GroupName(RegionNumber) & "\Opensim.log")
            Catch
            End Try

            Try
                My.Computer.FileSystem.DeleteFile(gPath + "bin\Regions\" & RegionClass.GroupName(RegionNumber) & "\PID.pid")
            Catch
            End Try

            Try
                My.Computer.FileSystem.DeleteFile(gPath + "bin\regions\" & RegionClass.GroupName(RegionNumber) & "\OpensimConsole.log")
            Catch ex As Exception
            End Try

            Try
                My.Computer.FileSystem.DeleteFile(gPath + "bin\regions\" & RegionClass.GroupName(RegionNumber) & "\OpenSimStats.log")
            Catch ex As Exception
            End Try

            If myProcess.Start() Then
                For Each num In RegionClass.RegionListByGroupNum(Groupname)
                    Log("PID:Setting booted status for " + RegionClass.RegionName(num) + " PID=" + myProcess.Id.ToString + " Num:" + num.ToString)
                    RegionClass.WarmingUp(num) = True
                    RegionClass.Booted(num) = False
                    RegionClass.ShuttingDown(num) = False
                    RegionClass.ProcessID(num) = myProcess.Id
                    RegionClass.Timer(num) = REGION_TIMER.START_COUNTING
                Next

                UpdateView = True ' make form refresh

                Sleep(1000)
                While Not SetWindowTextCall(myProcess.MainWindowHandle, RegionClass.GroupName(RegionNumber))
                    Sleep(1000)
                End While

                Log("Created Process Number " + myProcess.Id.ToString + " in  RegionHandles(" + RegionHandles.Count.ToString + ") " + "Group:" + Groupname)
                RegionHandles.Add(myProcess.Id, Groupname) ' save in the list of exit events in case it crashes or exits

                Return True
            End If

        Catch ex As Exception
            If ex.Message.Contains("Process has exited") Then Return False
            Print("Oops! " + BootName + " did Not start")
            ErrorLog(ex.Message)
            UpdateView = True ' make form refresh
            Dim yesno = MsgBox("Oops! " + BootName + " in DOS box " + Groupname + " did not boot. Do you want to see the log file?", vbYesNo, "Error")
            If (yesno = vbYes) Then
                System.Diagnostics.Process.Start("notepad.exe", RegionClass.IniPath(RegionNumber) + "Opensim.log")
            End If

            Return False
        End Try

        Return False

    End Function
    ''' <summary>
    ''' Check is Robust port 8002 is up
    ''' </summary>
    ''' <returns>boolean</returns>
    Private Function IsRobustRunning() As Boolean

        Dim Up As String = String.Empty
        Try
            Up = client.DownloadString("http://127.0.0.1:" + MySetting.HttpPort + "/?_Opensim=" + Random())
        Catch ex As Exception
            If ex.Message.Contains("404") Then Return True
            Return False
        End Try
        If Up.Length = 0 And OpensimIsRunning() Then
            Return False
        End If

        Return True

    End Function

#End Region

#Region "Logging"
    ''' <summary>
    ''' Deletes old log files
    ''' </summary>
    Private Sub ClearLogFiles()

        Dim Logfiles = New List(Of String) From {
            MyFolder + "\OutworldzFiles\Error.log",
            MyFolder + "\OutworldzFiles\Outworldz.log",
            MyFolder + "\OutworldzFiles\Opensim\bin\OpenSimConsoleHistory.txt",
            MyFolder + "\OutworldzFiles\Diagnostics.log",
            MyFolder + "\OutworldzFiles\UPnp.log",
            MyFolder + "\OutworldzFiles\Opensim\bin\Robust.log",
            MyFolder + "\OutworldzFiles\http.log",
            MyFolder + "\http.log"      ' an old mistake
        }

        For Each thing As String In Logfiles
            ' clear out the log files
            Try
                My.Computer.FileSystem.DeleteFile(thing)
            Catch ex As Exception
            End Try
        Next

    End Sub
    ''' <summary>
    ''' Log(string) to Outworldz.log
    ''' </summary>
    ''' <param name="message"></param>
    Public Sub Log(message As String)
        Logger(message, "Outworldz")
    End Sub
    Public Sub ErrorLog(message As String)
        Logger(message, "Error")
    End Sub

    Sub Logger(message As String, file As String)
        Try
            Using outputFile As New StreamWriter(MyFolder & "\OutworldzFiles\" + file + ".log", True)
                outputFile.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + message)
                Diagnostics.Debug.Print(message)
            End Using
        Catch
        End Try
    End Sub
    ''' <summary>
    ''' Shows the log buttons if diags fail
    ''' </summary>
    Private Sub ShowLog()

        LogButton.Show()
        IgnoreButton.Show()

    End Sub

    Private Sub ShowLogButton_Click(sender As Object, e As EventArgs) Handles LogButton.Click

        System.Diagnostics.Process.Start("wordpad.exe", """" + MyFolder + "/OutworldzFiles/Outworldz.log" + """")

        LogButton.Hide()
        IgnoreButton.Hide()

    End Sub

    Private Sub IgnoreButton_Click(sender As Object, e As EventArgs) Handles IgnoreButton.Click

        LogButton.Hide()
        IgnoreButton.Hide()

    End Sub

#End Region

#Region "Subs"

    Public Function getHwnd(name As String) As IntPtr

        For Each pList As Process In Process.GetProcesses()
            If pList.MainWindowTitle.Contains(name) Then
                Return pList.MainWindowHandle
            End If
        Next
        Return IntPtr.Zero

    End Function

    ''' <summary>
    ''' Sends keystrokes to Opensim.
    ''' Always sends and enter button before to clear and use keys
    ''' </summary>
    ''' <param name="ProcessID">PID of the DOS box</param>
    ''' <param name="command">String</param>
    ''' <returns></returns>
    Public Function ConsoleCommand(name As String, command As String) As Boolean

        Try
            Dim hwnd = getHwnd(name)
            If hwnd <> IntPtr.Zero Then
                ShowWindow(hwnd, SHOW_WINDOW.SW_RESTORE)
            Else
                Return False
            End If


            'plus sign(+), caret(^), percent sign (%), tilde (~), And parentheses ()
            command = command.Replace("+", "{+}")
            command = command.Replace("^", "{^}")
            command = command.Replace("%", "{%}")
            command = command.Replace("(", "{(}")
            command = command.Replace(")", "{)}")
            AppActivate(name)
            SendKeys.SendWait("{ENTER}" + vbCrLf)
            SendKeys.SendWait(command)

        Catch ex As Exception
            ErrorLog("Error:" + ex.Message)
            Diagnostics.Debug.Print("Cannot find window " + name)
            RegionClass.RegionDump()
            Return False
        End Try
        Me.Focus()
        Application.DoEvents()
        Return True

    End Function


    Private Sub SaySomething()
        Dim Prefix() As String = {
                                  "Mmmm?  Yawns ...",
                                  "Yawns, and stretches ...",
                                  "Wakes up and rolls over ...",
                                  "You look more beautiful every time I wake up.",
                                  "Zzzz...  Ooooh, I need coffee before I go to work.",
                                  "Nooo... is it already time to wake up?",
                                  "Mmmm, I was sleeping...",
                                  "What a dream that was!",
                                  "Do you ever dream of better worlds? I just did.",
                                  "Huh?",
                                  "Mumbles...",
                                  "Hello beautiful",
                                  "Act on dreams with an open mind.",
                                  "Believe in the beauty of your dreams.",
                                  "A dream doesn't become reality through magic"
                                }

        Dim Array() As String = {
                 "I dreamt we were both flying a dragon in the Outworldz. You flamed me. I tried to get even.  I lost LOL ",
                 "I dreamt we were chatting at OsGrid.org. It's the largest hypergrid-enabled virtual world.",
                 "I dreamt some friends and you were riding a rollercoaster in the Great Canadian Grid.",
                 "I dreamt I was watching a pretty particle exhibit with you on the Metropolis grid.",
                 "I dreamt we walked into a bar discussing politics in Hebrew and Arabic using a free translator.",
                 "I dreamt you took the hypergrid safari to visit the mountains of Africa in the Virunga sim.",
                 "I dreamt you won a race while riding a silly cow at the Outworldz 'Frankie' sim.",
                 "I dreamt you are a wonderful singer. I loved to hear your voice singing into the voice-chat system.",
                 "I remember in my dream that the spaceport at Gravity sim in OsGrid was really hopping. And floating. And then I fell. ",
                 "I was dreaming that you were a mermaid in the Lost Worlds.",
                 "I deamt that you made a pile of prims that you simply will not believe!",
                 "I dreamed that I asked when you were going to straighten out the castle. You said, 'Why? Is it tilted?'",
                 "I saw a dog in my dream. I dreamt you made a 'mesh' of it.",
                 "I dreamt I saw a man without any pants firmly attached to an eagle flying in the air. Always rez before you attach!",
                 "I forgot the dream already. I remember I woke up in it.",
                 "I was thinking I had no clothes on. No shirt, shoes, or hair. The worst part was there was no facelight! I looked hideous!",
                 "I dreamt that I was floating in a river and a scripted mesh crocodile chased me.",
                 "I dreamt I drove our car into the ocean. You found a pose ball, and we both grabbed onto it and were saved.",
                 "I dreamed that there was a animated mesh zebra in my bathtub.",
                 "I had dreamed a fairy was my best friend.",
                 "I dreamed that there were non-player characters living in my house, so I decided to fly away. ",
                 "I had a dream that there were pimples all over my face. So I switched skins and looked perfect!",
                 "I had a dream where I had lost my free mesh boots, so I was asking everybody where I got them on the hypergrid.",
                 "I had a dream that we were sitting on my roof and we stood up and both fell off. But I hit Page Up and flew away."
                  }

        Randomize()

        Dim value1 As Integer = CInt(Int((Prefix.Length - 1) * Rnd()))
        Dim value2 As Integer = CInt(Int((Array.Length - 1) * Rnd()))
        Dim whattosay = Prefix(value1) + vbCrLf + vbCrLf + Array(value2) + " ... and then I woke up."
        Print(whattosay)

    End Sub
    ''' <summary>
    ''' Sleep(ms)
    ''' </summary>
    ''' <param name="value">millseconds</param>
    Sub Sleep(value As Integer)

        ' value is in milliseconds, but we do it in 10 passes so we can doevents() to free up console
        Dim sleeptime = value / 10  ' now in tenths
        Dim counter = 10
        While counter > 0
            Application.DoEvents()
            Thread.Sleep(CType(sleeptime, Integer))
            counter = counter - 1
        End While

    End Sub
    ''' <summary>
    ''' Draws a gif
    ''' </summary>
    Public Sub PaintImage()

        If MySetting.TimerInterval > 0 Then  ' is it enabled?

            Dim randomFruit = images(Arnd.Next(0, images.Count))
            ProgressBar1.Visible = False
            TextBox1.Visible = False
            PictureBox1.Enabled = True
            PictureBox1.Image = randomFruit
            PictureBox1.Visible = True
        Else
            PictureBox1.Visible = False
        End If

    End Sub

    ''' <summary>
    ''' Timer runs every second
    ''' registers DNS,looks for web server stuff that arrives,
    ''' restarts any sims , updates lists of agents
    ''' builds teleports.html for older teleports
    ''' checks for crashesd regions
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick

        If Not OpensimIsRunning() Then
            Timer1.Stop()
            Return
        End If

        gDNSSTimer = gDNSSTimer + 1

        ' hourly
        If gDNSSTimer Mod 3600 = 0 Then
            RegisterDNS()
        End If

        If Not gExiting Then RegionClass.CheckPost()

        ' 10 seconds check for a restart
        ' RegionRestart requires this MOD 10 as it changed there to one minute
        If gDNSSTimer Mod 10 = 0 Then

            DoExitHandlerPoll() ' see if any regions have exited and set it up for Region Restart
            If Not gExiting Then
                RegionRestart() ' check for reboot 
                ScanAgents() ' update agent count
            End If

        End If

        If gDNSSTimer Mod 60 = 0 Then
            RegionListHTML() ' create HTML for region teleporters
        End If

    End Sub

    '' makes a list of teleports for the prims to use
    Private Sub RegionListHTML()

        'http://localhost:8002/bin/data/teleports.htm
        'Outworldz|Welcome||www.outworldz.com:9000:Welcome|128,128,96|
        '*|Welcome||www.outworldz.com9000Welcome|128,128,96|
        Dim HTML As String
        Dim HTMLFILE = MyFolder & "\OutworldzFiles\Opensim\bin\data\teleports.htm"
        HTML = "Welcome to |" + MySetting.SimName + "||" + MySetting.PublicIP + ":" + MySetting.HttpPort + ":" + MySetting.WelcomeRegion + "||" + vbCrLf
        Dim ToSort As New List(Of String)
        For Each X As Integer In RegionClass.RegionNumbers
            If RegionClass.Booted(X) And RegionClass.Teleport(X) = "True" Then
                ToSort.Add(RegionClass.RegionName(X))
            End If
        Next

        ToSort.Sort()

        For Each S As String In ToSort
            Dim X = RegionClass.FindRegionByName(S)
            HTML = HTML + "*|" + RegionClass.RegionName(X) + "||" + MySetting.PublicIP + ":" + MySetting.HttpPort + ":" + RegionClass.RegionName(X) + "||" + vbCrLf
        Next

        Try
            My.Computer.FileSystem.DeleteFile(HTMLFILE)
        Catch
        End Try

        Try
            Using outputFile As New StreamWriter(HTMLFILE, True)
                outputFile.WriteLine(HTML)
            End Using
        Catch ex As Exception
            ErrorLog("Error:Failed to create file:" + ex.Message)
        End Try

    End Sub

    ''' <summary>
    ''' restarts and regions that have timed out
    ''' </summary>
    Private Sub RegionRestart()

        If MySetting.AutoRestartInterval() = 0 And Not gRestartNow Then Return

        For Each X As Integer In RegionClass.RegionNumbers

            Application.DoEvents()

            If OpensimIsRunning() And Not gExiting And RegionClass.Timer(X) >= REGION_TIMER.START_COUNTING _
                And (RegionClass.Booted(X) Or RegionClass.WarmingUp(X) Or RegionClass.ShuttingDown(X)) Then

                Dim timervalue As Integer = RegionClass.Timer(X)
                ' if it is past time and no one is in the sim...
                Dim Groupname = RegionClass.GroupName(X)
                If timervalue / 6 >= MySetting.AutoRestartInterval() And MySetting.AutoRestartInterval() > 0 And Not AvatarsIsInGroup(Groupname) Then
                    ' shut down the group when one minute has gone by, or multiple thereof.
                    Try

                        Dim hwnd = getHwnd(Groupname)
                        If hwnd <> IntPtr.Zero Then ShowWindow(hwnd, SHOW_WINDOW.SW_RESTORE)

                        ConsoleCommand(RegionClass.GroupName(X), "q{ENTER}" + vbCrLf)
                        ConsoleCommand(RegionClass.GroupName(X), "Q{ENTER}" + vbCrLf)
                        ConsoleCommand(RegionClass.GroupName(X), "q{ENTER}" + vbCrLf)
                        Print("AutoRestarting " + Groupname)
                        RegionClass.Timer(X) = REGION_TIMER.RESTART_PENDING

                        ' shut down all regions in the DOS box
                        For Each Y In RegionClass.RegionListByGroupNum(Groupname)
                            RegionClass.Timer(Y) = REGION_TIMER.RESTART_PENDING
                            RegionClass.Booted(Y) = False
                            RegionClass.WarmingUp(Y) = False
                            RegionClass.ShuttingDown(Y) = True
                        Next

                        UpdateView = True ' make form refresh
                    Catch ex As Exception
                        ErrorLog(ex.Message)
                        RegionClass.RegionDump()

                        ' shut down all regions in the DOS box
                        For Each Y In RegionClass.RegionListByGroupNum(Groupname)
                            RegionClass.Timer(Y) = REGION_TIMER.RESTART_PENDING
                            RegionClass.Booted(Y) = False
                            RegionClass.WarmingUp(Y) = False
                            RegionClass.ShuttingDown(Y) = False
                        Next

                    End Try

                End If

                ' count up to auto restart , when high enough, restart the sim
                If RegionClass.Timer(X) >= REGION_TIMER.START_COUNTING Then
                    RegionClass.Timer(X) = RegionClass.Timer(X) + 1
                End If

            End If

        Next

        For Each X As Integer In RegionClass.RegionNumbers
            ' if a restart is signalled, boot it up
            If RegionClass.Timer(X) = REGION_TIMER.RESTARTING Then
                Boot(RegionClass.RegionName(X))
                gRestartNow = False
            End If

        Next


    End Sub

    ''' <summary>
    ''' quiery MySQL to find any avatars in the DOS bos so we can stop it, or not
    ''' </summary>
    ''' <param name="groupname"></param>
    ''' <returns></returns>
    Private Function AvatarsIsInGroup(groupname As String) As Boolean

        Dim present As Integer = 0
        For Each RegionNum As Integer In RegionClass.RegionListByGroupNum(groupname)
            present = present + RegionClass.AvatarCount(RegionNum)
        Next

        Return CType(present, Boolean)

    End Function

    ''' <summary>
    '''  get next picture
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click

        Dim randomFruit = images(Arnd.Next(0, images.Count))
        ProgressBar1.Visible = False
        TextBox1.Visible = False
        PictureBox1.Enabled = True
        PictureBox1.Image = randomFruit
        PictureBox1.Visible = True
        Timertick = 0   ' rest for next pass

    End Sub

    Private Sub ShowHyperGridAddressToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowHyperGridAddressToolStripMenuItem.Click

        Print("Grid address is " + vbCrLf + "http://" + MySetting.PublicIP + ":" + MySetting.HttpPort)

    End Sub

    Private Sub BumpProgress(bump As Integer)

        Dim nextval As Integer = ProgressBar1.Value + bump
        If nextval > 100 Then
            nextval = 100
        End If
        ProgressBar1.Value = nextval

        Application.DoEvents()
    End Sub

    Private Sub BumpProgress10()

        Dim nextval As Integer = ProgressBar1.Value + 10
        If nextval > 100 Then
            nextval = 100
        End If
        ProgressBar1.Value = nextval
        Application.DoEvents()

    End Sub

    Private Function Stripqq(input As String) As String

        Return Replace(input, """", "")

    End Function

#End Region

#Region "IAROAR"

    Private Sub SaveRegionOARToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveRegionOARToolStripMenuItem.Click

        If OpensimIsRunning() Then

            Dim chosen = ChooseRegion(True)
            If chosen.Length = 0 Then Return
            Dim RegionNumber As Integer = RegionClass.FindRegionByName(chosen)

            Dim Message, title, defaultValue As String
            Dim myValue As String
            ' Set prompt.
            Message = "Enter a name for your backup:"
            title = "Backup to OAR"
            defaultValue = chosen + "_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".oar"

            ' Display message, title, and default value.
            myValue = InputBox(Message, title, defaultValue)
            ' If user has clicked Cancel, set myValue to defaultValue 
            If myValue.Length = 0 Then Return

            If RegionClass.Booted(RegionNumber) Then
                Dim Group = RegionClass.GroupName(RegionNumber)

                ConsoleCommand(RegionClass.GroupName(RegionNumber), "alert CPU Intensive Backup Started{ENTER}" + vbCrLf)
                ConsoleCommand(RegionClass.GroupName(RegionNumber), "change region " + """" + chosen + """" + "{ENTER}" + vbCrLf)
                ConsoleCommand(RegionClass.GroupName(RegionNumber), "save oar " + """" + BackupPath() + myValue + """" + "{ENTER}" + vbCrLf)

            End If
            Me.Focus()
            Print("Saving " + myValue + " to " + BackupPath())
        Else
            Print("Opensim is not running. Cannot make a backup now.")
        End If

    End Sub

    Private Sub LoadRegionOarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoadRegionOarToolStripMenuItem.Click

        If OpensimIsRunning() Then
            Dim chosen = ChooseRegion(True)
            If chosen.Length = 0 Then Return
            Dim RegionNumber As Integer = RegionClass.FindRegionByName(chosen)

            ' Create an instance of the open file dialog box.
            Dim openFileDialog1 As OpenFileDialog = New OpenFileDialog

            ' Set filter options and filter index.
            openFileDialog1.InitialDirectory = BackupPath()
            openFileDialog1.Filter = "Opensim OAR(*.OAR,*.GZ,*.TGZ)|*.oar;*.gz;*.tgz;*.OAR;*.GZ;*.TGZ|All Files (*.*)|*.*"
            openFileDialog1.FilterIndex = 1
            openFileDialog1.Multiselect = False

            ' Call the ShowDialog method to show the dialogbox.
            Dim UserClickedOK As DialogResult = openFileDialog1.ShowDialog

            ' Process input if the user clicked OK.
            If UserClickedOK = DialogResult.OK Then
                Dim backMeUp = MsgBox("Make a backup first and then load the new content?", vbYesNo, "Backup?")
                Dim thing = openFileDialog1.FileName
                If thing.Length > 0 Then
                    thing = thing.Replace("\", "/")    ' because Opensim uses unix-like slashes, that's why

                    Dim Group = RegionClass.GroupName(RegionNumber)
                    For Each Y In RegionClass.RegionListByGroupNum(Group)

                        ConsoleCommand(RegionClass.GroupName(Y), "change region " + chosen + "{ENTER}" + vbCrLf)
                        If backMeUp = vbYes Then
                            ConsoleCommand(RegionClass.GroupName(Y), "alert CPU Intensive Backup Started{ENTER}" + vbCrLf)
                            ConsoleCommand(RegionClass.GroupName(Y), "save oar  " + """" + BackupPath() + "Backup_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".oar" + """" + "{ENTER}" + vbCrLf)
                        End If
                        ConsoleCommand(RegionClass.GroupName(Y), "alert New content Is loading..{ENTER}" + vbCrLf)
                        ConsoleCommand(RegionClass.GroupName(Y), "load oar --force-terrain --force-parcels " + """" + thing + """" + "{ENTER}" + vbCrLf)
                        ConsoleCommand(RegionClass.GroupName(Y), "alert New content just loaded." + "{ENTER}" + vbCrLf)

                    Next
                End If
            End If
        Else
            Print("Opensim Is Not running. Cannot load the OAR file.")
        End If

    End Sub
    Private Function BackupPath() As String

        'Autobackup must exist. if not create it
        ' if they set the folder somewhere else, it may have been deleted, so reset it to default
        If MySetting.BackupFolder = "AutoBackup" Then
            BackupPath = gCurSlashDir + "/OutworldzFiles/AutoBackup/"
            If Not Directory.Exists(BackupPath) Then
                MkDir(BackupPath)
                MkDir(BackupPath + "MySQL")
                MkDir(BackupPath + "Regions")
            End If
        Else
            BackupPath = MySetting.BackupFolder + "/"
            BackupPath = BackupPath.Replace("\", "/")    ' because Opensim uses unix-like slashes, that's why

            If Not Directory.Exists(BackupPath) Then

                BackupPath = gCurSlashDir + "/OutworldzFiles/Autobackup/"

                If Not Directory.Exists(BackupPath) Then
                    MkDir(BackupPath)
                    MkDir(BackupPath + "MySQL")
                    MkDir(BackupPath + "Regions")
                End If
                MsgBox("Autoback folder cannot be located, so It has been reset to the default:" + BackupPath)
                MySetting.BackupFolder = "AutoBackup"
                MySetting.SaveSettings()
            End If
        End If

    End Function

    Private Sub AllRegionsOARsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AllTheRegionsOarsToolStripMenuItem.Click

        If Not OpensimIsRunning() Then
            Print("Opensim Is Not running. Cannot save an OAR at this time.")
            Return
        End If

        Dim n As Integer = 0
        Dim L As New List(Of String)

        For Each RegionNumber In RegionClass.RegionNumbers
            If RegionClass.Booted(RegionNumber) Then

                Dim Group = RegionClass.GroupName(RegionNumber)

                For Each Y In RegionClass.RegionListByGroupNum(Group)
                    If Not L.Contains(RegionClass.RegionName(Y)) Then
                        ConsoleCommand(RegionClass.GroupName(RegionNumber), "change region " + """" + RegionClass.RegionName(Y) + """" + "{ENTER}" + vbCrLf)
                        ConsoleCommand(RegionClass.GroupName(RegionNumber), "save oar  " + """" + BackupPath() + RegionClass.RegionName(Y) + "_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".oar" + """" + "{ENTER}" + vbCrLf)
                        L.Add(RegionClass.RegionName(Y))
                    End If
                Next

            End If

            n = n + 1

        Next

    End Sub

    Private Sub LoadInventoryIARToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoadInventoryIARToolStripMenuItem.Click

        If OpensimIsRunning() Then
            ' Create an instance of the open file dialog box.
            Dim openFileDialog1 As OpenFileDialog = New OpenFileDialog

            ' Set filter options and filter index.
            openFileDialog1.InitialDirectory = """" + MyFolder + "/" + """"
            openFileDialog1.Filter = "Inventory IAR (*.iar)|*.iar|All Files (*.*)|*.*"
            openFileDialog1.FilterIndex = 1
            openFileDialog1.Multiselect = False

            ' Call the ShowDialog method to show the dialogbox.
            Dim UserClickedOK As DialogResult = openFileDialog1.ShowDialog

            ' Process input if the user clicked OK.
            If UserClickedOK = DialogResult.OK Then
                Dim thing = openFileDialog1.FileName
                If thing.Length > 0 Then
                    thing = thing.Replace("\", "/")    ' because Opensim uses unix-like slashes, that's why
                    If LoadIARContent(thing) Then
                        Print("Opensimulator will load " + thing + ".  This may take time to load.")
                    End If
                End If
            End If
        Else
            Print("Opensim Is Not running. Cannot load an IAR at this time.")
        End If

    End Sub

    Private Sub SaveInventoryIARToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveInventoryIARToolStripMenuItem.Click

        If OpensimIsRunning() Then
            Dim Message, title, defaultValue As String

            '''''''''''''''''''''''
            ' Object Name to back up
            Dim itemName As String
            ' Set prompt.
            Message = "Enter the Object name ('/' will  backup everything, and '/Objects/box' will back up box in folder Objects) :"
            title = "Backup Name?"
            defaultValue = "/"   ' Set default value.

            ' Display message, title, and default value.
            itemName = InputBox(Message, title, defaultValue)
            ' If user has clicked Cancel, set myValue to defaultValue 
            If itemName.Length = 0 Then Return

            '''''''''''''''''''''''
            ' Name of the IAR
            Dim backupName As String
            Message = "Backup name? :"
            title = "Backup Name?"
            defaultValue = "backup" + "_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".iar"   ' Set default value.

            ' Display message, title, and default value.
            backupName = InputBox(Message, title, defaultValue)
            ' If user has clicked Cancel, set myValue to defaultValue 
            If itemName.Length = 0 Then Return

            '''''''''''''''''''''''
            ' Avatar
            Dim Name As String
            Message = "Avatar FirstName and Lastname:"
            title = "FirstName LastName"
            defaultValue = ""   ' Set default value.

            ' Display message, title, and default value.
            Name = InputBox(Message, title, defaultValue)
            ' If user has clicked Cancel, set myValue to defaultValue 
            If Name.Length = 0 Then Return

            '''''''''''''''''''''''
            ' Password
            Dim Password As String
            Message = "Avatar's Password?:"
            title = "Password needed"
            defaultValue = ""   ' Set default value.

            ' Display message, title, and default value.
            Password = InputBox(Message, title, defaultValue)

            Dim r As Integer = 0
            For Each d As Integer In RegionClass.RegionNumbers
                Dim GName = RegionClass.GroupName(d)
                Dim RNUm = RegionClass.FindRegionByName(GName)
                If RegionClass.Booted(d) And r = 0 Then
                    ConsoleCommand(RegionClass.GroupName(d), "save iar " + Name + " """ + itemName + """" + " " + Password + " " + """" + BackupPath() + backupName + """" + "{ENTER}" + vbCrLf)
                    r = r + 1
                    Me.Focus()
                    Print("Saving " + backupName + " to " + BackupPath())
                End If
            Next

        Else
            Print("Opensim Is Not running. Cannot make a backup now.")
        End If

    End Sub

    Public Function ChooseRegion(Optional JustRunning As Boolean = False) As String

        Dim Chooseform As New Choice ' form for choosing a set of regions
        ' Show testDialog as a modal dialog and determine if DialogResult = OK.

        Chooseform.FillGrid("Region", JustRunning)  ' populate the grid with either Group or RegionName

        Dim chosen As String
        Dim ret = Chooseform.ShowDialog()

        Try
            ' Read the chosen sim name
            chosen = Chooseform.DataGridView.CurrentCell.Value.ToString()
        Catch ex As Exception
            ErrorLog("Warn: Could not chose a displayed region. " + ex.Message)
            chosen = ""
        End Try
        If ret = DialogResult.Cancel Then Return ""
        Return chosen

    End Function
    Public Function VarChooser(RegionName As String) As String

        Dim RegionNumber = RegionClass.FindRegionByName(RegionName)
        Dim size = RegionClass.SizeX(RegionNumber)
        If size = 512 Then  ' 2x2
            Dim VarForm As New FormDisplacement2x2 ' form for choosing a  region in  a var
            ' Show testDialog as a modal dialog and determine if DialogResult = OK.
            VarForm.ShowDialog()
        ElseIf size = 768 Then ' 3x3
            Dim VarForm As New FormDisplacement3x3 ' form for choosing a  region in  a var
            ' Show testDialog as a modal dialog and determine if DialogResult = OK.
            VarForm.ShowDialog()
        ElseIf size = 1024 Then ' 4x4
            Dim VarForm As New FormDisplacement ' form for choosing a region in  a var
            ' Show testDialog as a modal dialog and determine if DialogResult = OK.
            VarForm.ShowDialog()
        Else
            Return ""
        End If

        Dim displacement As String = gSelectedBox
        Return displacement

    End Function
    Private Function LoadOARContent(thing As String) As Boolean

        If Not OpensimIsRunning() Then
            Print("Opensim has to be started to load an OAR file.")
            Return False
        End If

        Dim region = ChooseRegion(True)
        If region.Length = 0 Then Return False

        Dim offset = VarChooser(region)

        Dim backMeUp = MsgBox("Make a backup first?", vbYesNo, "Backup?")
        Dim num = RegionClass.FindRegionByName(region)
        Dim GroupName = RegionClass.GroupName(num)
        Dim once As Boolean = False
        For Each Y In RegionClass.RegionListByGroupNum(GroupName)
            Try
                If Not once Then
                    Print("Opensimulator will load " + thing + ". This may take some time.")
                    thing = thing.Replace("\", "/")    ' because Opensim uses unix-like slashes, that's why

                    ConsoleCommand(RegionClass.GroupName(Y), "change region " + region + "{ENTER}" + vbCrLf)
                    If backMeUp = vbYes Then
                        ConsoleCommand(RegionClass.GroupName(Y), "alert CPU Intensive Backup Started {ENTER}" + vbCrLf)
                        ConsoleCommand(RegionClass.GroupName(Y), "save oar " + BackupPath() + "Backup_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".oar" + """" + "{ENTER}" + vbCrLf)
                    End If
                    ConsoleCommand(RegionClass.GroupName(Y), "alert New content Is loading ...{ENTER}" + vbCrLf)

                    Dim ForceParcel As String = ""
                    If gForceParcel Then ForceParcel = " --force-parcels "
                    Dim ForceTerrain As String = ""
                    If gForceTerrain Then ForceTerrain = " --force-terrain "
                    Dim ForceMerge As String = ""
                    If gForceMerge Then ForceMerge = " --merge "

                    ConsoleCommand(RegionClass.GroupName(Y), "load oar " & ForceMerge & ForceTerrain & ForceParcel & offset & """" & thing & """" & "{ENTER}" & vbCrLf)
                    ConsoleCommand(RegionClass.GroupName(Y), "alert New content just loaded. {ENTER}" + vbCrLf)
                    once = True
                End If

            Catch ex As Exception
                ErrorLog("Error:  " + ex.Message)
            End Try
        Next

        Me.Focus()
        Return True

    End Function

    Public Function LoadIARContent(thing As String) As Boolean

        If Not OpensimIsRunning() Then
            Print("Opensim is not running. Cannot load an IAR at this time.")
            Return False
        End If

        Dim num As Integer = -1

        ' find one that is running
        For Each RegionNum In RegionClass.RegionNumbers
            If RegionClass.Booted(RegionNum) Then
                num = RegionNum
            End If
        Next
        If num = -1 Then
            MsgBox("No regions are ready, so cannot load the IAR", vbInformation, "Info")
            Return False
        End If

        Dim Path As String = InputBox("Folder to save in Inventory (""/"", ""/Objects"", ""/Objects/Somefolder..."")", "Folder Name", "/Objects")

        Dim user = InputBox("First and Last name that will get this IAR?")
        Dim password = InputBox("Password for user " + user + "?")
        If user.Length > 0 And password.Length > 0 Then
            ConsoleCommand(RegionClass.GroupName(num), "load iar --merge " & user & " " & Path & " " & password & " " & """" & thing & """" & "{ENTER}" & vbCrLf)
            ConsoleCommand(RegionClass.GroupName(num), "alert IAR content Is loaded{ENTER}" + vbCrLf)
            Print("Opensim Is loading your item. You will find it in Inventory in " & Path & " soon.")
        Else
            Print("Load IAR cancelled - must use the full user name and password.")
        End If
        Me.Focus()
        Return True

    End Function

    Private Sub TextBox1_DragDrop(sender As System.Object, e As System.Windows.Forms.DragEventArgs) Handles TextBox1.DragDrop

        Dim files() As String = CType(e.Data.GetData(DataFormats.FileDrop), String())
        For Each pathname As String In files
            pathname = pathname.Replace("\", "/")
            Dim extension = Path.GetExtension(pathname)
            extension = Mid(extension, 2, 5)
            If extension.ToLower = "iar" Then
                If LoadIARContent(pathname) Then
                    Print("Opensimulator will load " + pathname + ".  This may take time to load.")
                End If
            ElseIf extension.ToLower = "oar" Or extension.ToLower = "gz" Or extension.ToLower = "tgz" Then
                If LoadOARContent(pathname) Then
                    Print("Opensimulator will load " + pathname + ".  This may take time to load.")
                End If
            Else
                Print("Unrecognized file type:  " + extension + ".  Drag And drop any OAR, GZ, TGZ, Or IAR files to load them when the sim starts")
            End If
        Next

    End Sub

    Private Sub TextBox1_DragEnter(sender As System.Object, e As System.Windows.Forms.DragEventArgs) Handles TextBox1.DragEnter

        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        End If

    End Sub

    Private Sub PictureBox1_DragDrop(sender As System.Object, e As System.Windows.Forms.DragEventArgs) Handles PictureBox1.DragDrop

        Dim files() As String = CType(e.Data.GetData(DataFormats.FileDrop), String())
        For Each pathname As String In files
            pathname = pathname.Replace("\", "/")
            Dim extension = Path.GetExtension(pathname)
            extension = Mid(extension, 2, 5)
            If extension.ToLower = "iar" Then
                If LoadIARContent(pathname) Then
                    Print("Opensimulator will load " + pathname + ".  This may take time to load.")
                End If
            ElseIf extension.ToLower = "oar" Or extension.ToLower = "gz" Or extension.ToLower = "tgz" Then
                If LoadOARContent(pathname) Then
                    Print("Opensimulator will load " + pathname + ".  This may take time to load.")
                End If
            Else
                Print("Unrecognized file type:" + extension + ".  Drag And drop any OAR, GZ, TGZ, Or IAR files to load them when the sim starts")
            End If
        Next

    End Sub

    Private Sub PictureBox1_DragEnter(sender As System.Object, e As System.Windows.Forms.DragEventArgs) Handles PictureBox1.DragEnter

        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        End If

    End Sub

    Private Sub SetIAROARContent()

        IslandToolStripMenuItem.Visible = False
        ClothingInventoryToolStripMenuItem.Visible = False

        Print("Dreaming up New content for your sim")
        Dim oars As String = ""
        Try
            oars = client.DownloadString(gDomain + "/Outworldz_Installer/Content.plx?type=OAR&r=" + Random())
        Catch ex As Exception
            ErrorLog("No Oars, dang, something Is wrong with the Internet :-(")
            Return
        End Try

        UploadPhoto()

        Application.DoEvents()
        Dim oarreader = New System.IO.StringReader(oars)
        Dim line As String = ""
        Dim ContentSeen As Boolean = False
        While Not ContentSeen
            line = oarreader.ReadLine()
            If line <> Nothing Then
                Log("Info:" + line)
                Dim OarMenu As New ToolStripMenuItem
                OarMenu.Text = line
                OarMenu.ToolTipText = "Click to load this content"
                OarMenu.DisplayStyle = ToolStripItemDisplayStyle.Text
                AddHandler OarMenu.Click, New EventHandler(AddressOf OarClick)
                IslandToolStripMenuItem.Visible = True
                IslandToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {OarMenu})
                gContentAvailable = True
            Else
                ContentSeen = True
            End If
        End While
        BumpProgress10()

        ' read help files for menu
        line = ""
        Dim folders() = IO.Directory.GetFiles(MyFolder & "\Outworldzfiles\Help")

        For Each aline As String In folders
            aline = System.IO.Path.GetFileNameWithoutExtension(aline)
            Log("Loading Help:" + aline)
            Dim HelpMenu As New ToolStripMenuItem
            HelpMenu.Text = aline
            HelpMenu.ToolTipText = "Click to load this content"
            HelpMenu.DisplayStyle = ToolStripItemDisplayStyle.Text
            HelpMenu.Image = My.Resources.question_and_answer
            AddHandler HelpMenu.Click, New EventHandler(AddressOf HelpClick)
            HelpOnSettingsToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {HelpMenu})
        Next

        Log("Info:OARS loaded")
        Print("Dreaming up some clothes and items for your avatar")
        Dim iars As String = ""
        Try
            iars = client.DownloadString(gDomain + "/Outworldz_Installer/Content.plx?type=IAR&r=" + Random())
        Catch ex As Exception
            ErrorLog("Info:No IARS, dang, something is wrong with the Internet :-(")
            Return
        End Try

        Dim iarreader = New System.IO.StringReader(iars)
        ContentSeen = False
        While Not ContentSeen
            line = iarreader.ReadLine()
            If line <> Nothing Then
                Log("Info:" + line)
                Dim IarMenu As New ToolStripMenuItem
                IarMenu.Text = line
                IarMenu.ToolTipText = "Click to load this content the next time the simulator is started"
                IarMenu.DisplayStyle = ToolStripItemDisplayStyle.Text
                AddHandler IarMenu.Click, New EventHandler(AddressOf IarClick)
                ClothingInventoryToolStripMenuItem.Visible = True
                ClothingInventoryToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {IarMenu})
                gContentAvailable = True
            Else
                ContentSeen = True
            End If
        End While


        Log("Info: IARS loaded")

        BumpProgress10()

        AddLog("Robust")
        AddLog("Outworldz")
        AddLog("UPnP")
        AddLog("Icecast")
        AddLog("MySQL")
        AddLog("All Settings")
        AddLog("-------")
        For Each X In RegionClass.RegionNumbers
            Dim Name = RegionClass.RegionName(X)
            AddLog(Name)
        Next

        BumpProgress10()

    End Sub

    ''' <summary>
    ''' Upload in a sperate tyhred the photo, if any.  Cannot be called unless main web server is known to be online.
    ''' </summary>
    Private Sub UploadPhoto()

        If System.IO.File.Exists(MyFolder & "\OutworldzFiles\Photo.png") Then
            Dim Myupload As New UploadImage
            Myupload.PostContent_UploadFile()
        End If

    End Sub

    Private Sub AddLog(name As String)
        Dim LogMenu As New ToolStripMenuItem
        LogMenu.Text = name
        LogMenu.ToolTipText = "Click to view this log"
        LogMenu.DisplayStyle = ToolStripItemDisplayStyle.Text
        AddHandler LogMenu.Click, New EventHandler(AddressOf LogViewClick)
        ViewLogsToolStripMenuItem.Visible = True
        ViewLogsToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {LogMenu})
    End Sub

    Private Sub OarClick(sender As Object, e As EventArgs)

        Dim File As String = Mid(CType(sender.text, String), 1, InStr(CType(sender.text, String), "|") - 2)
        File = gDomain + "/Outworldz_Installer/OAR/" + File 'make a real URL
        LoadOARContent(File)
        sender.checked = True

    End Sub

    Private Sub IarClick(sender As Object, e As EventArgs)

        Dim file As String = Mid(CType(sender.text, String), 1, InStr(CType(sender.text, String), "|") - 2)
        file = gDomain + "/Outworldz_Installer/IAR/" + file 'make a real URL
        If LoadIARContent(file) Then
            Print("Opensimulator will load " + file + ".  This may take time to load.")
        End If
        sender.checked = True

    End Sub

#End Region

#Region "Updates"

    Private Sub CheckForUpdatesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CHeckForUpdatesToolStripMenuItem.Click
        CheckForUpdates()
        MySetting.SkipUpdateCheck = False
        ' check for updates is on if we ever check for updates 
    End Sub

    Private Sub UpdaterCancel_Click(sender As Object, e As EventArgs) Handles UpdaterCancel.Click

        UpdaterGo.Visible = False
        UpdaterCancel.Visible = False
        MySetting.SkipUpdateCheck = True
        Print("Update scan is off. You can still check for updates in the Help Menu.")

    End Sub

    Private Function MakeBackup() As Boolean

        Dim Foldername = "Full_backup" + "_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss")   ' Set default folder
        Dim Dest As String
        If MySetting.BackupFolder = "AutoBackup" Then
            Dest = MyFolder + "\OutworldzFiles\AutoBackup\" + Foldername
        Else
            Dest = MySetting.BackupFolder + "\" + Foldername
        End If
        Print("Making a backup at " + Dest)
        My.Computer.FileSystem.CreateDirectory(Dest)
        My.Computer.FileSystem.CreateDirectory(Dest + "\Opensim_bin_Regions")
        My.Computer.FileSystem.CreateDirectory(Dest + "\Mysql_Data")
        My.Computer.FileSystem.CreateDirectory(Dest + "\Opensim_WifiPages-Custom")
        My.Computer.FileSystem.CreateDirectory(Dest + "\Opensim_bin_WifiPages-Custom")

        Print("Backing up Regions Folder")
        Try
            My.Computer.FileSystem.CopyDirectory(MyFolder + "\OutworldzFiles\Opensim\bin\Regions", Dest + "\Regions")
            Print("Backing up MySql\Data Folder")
            My.Computer.FileSystem.CopyDirectory(MyFolder + "\OutworldzFiles\Mysql\Data\", Dest + "\Mysql_Data")
            Print("Backing up Wifi Folders")
            My.Computer.FileSystem.CopyDirectory(MyFolder + "\OutworldzFiles\Opensim\WifiPages\", Dest + "\Opensim_WifiPages-Custom")
            My.Computer.FileSystem.CopyDirectory(MyFolder + "\OutworldzFiles\Opensim\bin\WifiPages\", Dest + "\Opensim_bin_WifiPages-Custom")
            My.Computer.FileSystem.CopyFile(MyFolder + "\OutworldzFiles\Settings.ini", Dest + "\Settings.ini")
        Catch ex As Exception
            ErrorLog("Err:" + ex.Message)
            Return False
        End Try
        PrintFast("Finished with backup at " + Dest)
        Return True

    End Function

    Private Sub UpdaterGo_Click(sender As Object, e As EventArgs) Handles UpdaterGo.Click

        Dim msg = MsgBox("Make a backup of important files and the database first? ", vbYesNo)
        Dim okay As Boolean
        If msg = vbYes Then
            okay = MakeBackup()
        End If


        StopMysql()

        UpdaterGo.Enabled = False
        UpdaterCancel.Visible = False
        Dim fileloaded As String = Download()
        If fileloaded.Length > 0 Then
            Dim pUpdate As Process = New Process()
            Dim pi As ProcessStartInfo = New ProcessStartInfo()
            pi.Arguments = ""
            pi.FileName = """" + MyFolder + "\" + fileloaded + """"
            pUpdate.StartInfo = pi
            Try
                Print("I'll see you again when I wake up all fresh and new!")
                Log("Info:Launch Updater and exiting")
                pUpdate.Start()
            Catch ex As Exception
                ErrorLog("Error: Could not launch " + fileloaded + ". Perhaps you can can exit this program and launch it manually.")

            End Try
            End ' program
        Else
            Print("Uh Oh!  The files I need could not be found online. The gnomes have absconded with them!   Please check later.")
            UpdaterGo.Visible = False
            UpdaterGo.Enabled = True
        End If

    End Sub

    Private Function Download() As String

        Dim fileName As String = "UpdateGrid.exe"

        Try
            My.Computer.FileSystem.DeleteFile(MyFolder + "\" + fileName)
        Catch
            Log("Warn:Could not delete " + MyFolder + "\" + fileName)
        End Try

        Try
            fileName = client.DownloadString(gDomain + "/Outworldz_Installer/GetUpdaterGrid.plx?fill=1" + GetPostData())
        Catch
            MsgBox("Could not fetch an update. Please try again, later", vbInformation, "Info")
            Return ""
        End Try

        Try
            Dim myWebClient As New WebClient()
            Print("Downloading new updater, this will take a moment")
            ' The DownloadFile() method downloads the Web resource and saves it into the current file-system folder.
            myWebClient.DownloadFile(gDomain + "/Outworldz_Installer/" + fileName, fileName)
        Catch e As Exception
            MsgBox("Could not fetch an update. Please try again, later", vbInformation, "Info")
            Log("Warn:" + e.Message)
            Return ""
        End Try
        Return fileName

    End Function

    Sub CheckForUpdates()

        Dim Update As String = ""
        Dim isPortOpen As String = ""

        MySetting.SkipUpdateCheck = False

        Try
            Update = client.DownloadString(gDomain + "/Outworldz_Installer/UpdateGrid.plx?fill=1" + GetPostData())
        Catch ex As Exception
            ErrorLog("Dang:The Outworldz web site is down")
        End Try
        If (Update = "") Then Update = "0"

        Try
            If Convert.ToSingle(Update) > Convert.ToSingle(gMyVersion) Then
                Print("A dreamier version " + Update + " is available.")
                UpdaterGo.Visible = True
                UpdaterGo.Enabled = True
                UpdaterCancel.Visible = True
            Else
                Print("I am the dreamiest version available, at V " + gMyVersion)
            End If
        Catch
        End Try
        BumpProgress10()

    End Sub

#End Region

#Region "Diagnostics"

    Public Function CheckPort(ServerAddress As String, Port As Integer) As Boolean

        Dim ClientSocket As New TcpClient

        Try
            ClientSocket.Connect(ServerAddress, Port)
        Catch ex As Exception
            Log("Info:Unable to connect to server:" + ex.Message)
            Return False
        End Try

        If ClientSocket.Connected Then
            Log("Info: port probe success on port " + Port.ToString)
            ClientSocket.Close()
            Return True
        End If
        CheckPort = False

    End Function

    Public Function SetPublicIP() As Boolean

        ' LAN USE
        If MySetting.EnableHypergrid Then
            BumpProgress10()
            If MySetting.DNSName.Length > 0 Then
                MySetting.PublicIP = MySetting.DNSName
                MySetting.SaveSettings()
                Return True
            Else

#Disable Warning BC42025 ' Access of shared member, constant member, enum member or nested type through an instance
                MySetting.PublicIP = MyUPnpMap.LocalIP
#Enable Warning BC42025 ' Access of shared member, constant member, enum member or nested type through an instance

                MySetting.SaveSettings()
                Return False
            End If

        End If

        '  HG USE
        If Not IsPrivateIP(MySetting.DNSName) Then
            BumpProgress10()
            MySetting.PublicIP = MySetting.DNSName
            MySetting.SaveSettings()

            If MySetting.PublicIP.ToLower.Contains("outworldz.net") Then
                Print("Registering DynDNS address http://" + MySetting.PublicIP + ":" + MySetting.HttpPort)
            End If

            If RegisterDNS() Then
                Return True
            End If

        End If

        If MySetting.PublicIP = "localhost" Or MySetting.PublicIP = "127.0.0.1" Then
            Return True
        End If


        'V 2.44

        Try

            Log("Info:Public IP=" + MySetting.PublicIP)
            If TestPublicLoopback() Then
                ' Set Public IP
                Dim ip As String = client.DownloadString("http://api.ipify.org/?r=" + Random())
                BumpProgress10()

                MySetting.PublicIP = ip
                MySetting.SaveSettings()
                Return True
            End If

        Catch ex As Exception
            ErrorLog("Hmm, I cannot reach the Internet? Uh. Okay, continuing." + ex.Message)
            MySetting.DiagFailed = True
            Log("Info:Public IP=" + "127.0.0.1")
        End Try

        MySetting.PublicIP = MyUPnpMap.LocalIP
        MySetting.SaveSettings()

        BumpProgress10()
        Return False

    End Function

    Private Function TestPublicLoopback() As Boolean

        'If IsPrivateIP(MySetting.PublicIP) Then
        ' MySetting.DiagFailed = True
        'Return False
        'End If

        'Print("Running Loopback Test")
        Dim result As String = ""
        Dim loopbacktest As String = "http://" + MySetting.PublicIP + ":" + MySetting.DiagnosticPort + "/?_TestLoopback=" + Random()
        Try
            Log(loopbacktest)
            result = client.DownloadString(loopbacktest)
        Catch ex As Exception
            ErrorLog("Err:Loopback fail:" + result + ":" + ex.Message)
            Return False
        End Try

        BumpProgress10()

        If MySetting.PublicIP = MyUPnpMap.LocalIP() Then Return False

        If result = "Test Completed" Then
            Log("Passed:" + result)
            MySetting.LoopBackDiag = True
            MySetting.SaveSettings()
            Return True
        Else

            MySetting.LoopBackDiag = False
            MySetting.DiagFailed = True
#Disable Warning BC42025 ' Access of shared member, constant member, enum member or nested type through an instance
            MySetting.PublicIP = MyUPnpMap.LocalIP()
#Enable Warning BC42025 ' Access of shared member, constant member, enum member or nested type through an instance

            MySetting.SaveSettings()
        End If
        Return False

    End Function

    Private Sub DiagnosticsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DiagnosticsToolStripMenuItem.Click

        If OpensimIsRunning() Then
            Print("Cannot run dignostics while Opensimulator is running. Click 'Stop' and try again.")
            Return
        End If
        ProgressBar1.Value = 0
        DoDiag()
        If MySetting.DiagFailed = True Then
            Print("Hypergrid Diagnostics failed. These can be re-run at any time. Ip is set for LAN use only. See Help->Network Diagnostics', 'Loopback', and 'Port Forwards'")
        Else
            Print("Tests passed, Hypergrid should be working.")
        End If
        ProgressBar1.Value = 100

    End Sub

    Private Function ProbePublicPort() As Boolean

        If IsPrivateIP(MySetting.DNSName) Then
            Return True
        End If
        Print("Checking Network Connectivity")

        Dim isPortOpen As String = ""
        Try
            ' collect some stats and test loopback with a HTTP_ GET to the webserver.
            ' Send unique, anonymous random ID, both of the versions of Opensim and this program, and the diagnostics test results 
            ' See my privacy policy at https://www.outworldz.com/privacy.htm

            Dim Url = gDomain + "/cgi/probetest.plx?IP=" + MySetting.PublicIP + "&Port=" + MySetting.DiagnosticPort + GetPostData()
            'Log(Url)
            isPortOpen = client.DownloadString(Url)
        Catch ex As Exception
            ErrorLog("Dang:The Outworldz web site cannot find a path back")
            MySetting.DiagFailed = True
        End Try

        BumpProgress10()

        If isPortOpen = "yes" Then
            MySetting.PublicIP = MySetting.PublicIP
            Log("Public IP set to " + MySetting.PublicIP)
            MySetting.SaveSettings()
            Return True
        Else
            Log("Failed:" + isPortOpen)
            MySetting.DiagFailed = True
            Print("Internet address " + MySetting.PublicIP + ":" + MySetting.DiagnosticPort + " appears to not be forwarded to this machine in your router, so Hypergrid is not available. This can possibly be fixed by 'Port Forwards' in your router.  See Help->Port Forwards.")
#Disable Warning BC42025 ' Access of shared member, constant member, enum member or nested type through an instance
            MySetting.PublicIP = MyUPnpMap.LocalIP() ' failed, so try the machine address
#Enable Warning BC42025 ' Access of shared member, constant member, enum member or nested type through an instance
            MySetting.SaveSettings()
            Log("IP set to " + MySetting.PublicIP)
            Return False
        End If

    End Function

    Private Sub DoDiag()

        If IsPrivateIP(MySetting.DNSName) Then
            Return
        End If

        Print("Running Network Diagnostics, please wait")

        MySetting.DiagFailed = False

        OpenPorts() ' Open router ports with UPnp
        CheckDiagPort()
        ProbePublicPort()
        TestPublicLoopback()
        If MySetting.DiagFailed Then
            ShowLog()
        Else
            NewDNSName()
        End If
        Log("Diagnostics set the Grid address to " + MySetting.PublicIP)


    End Sub


    Private Sub CheckDiagPort()
        gUseIcons = True

        Dim wsstarted = CheckPort(MyUPnpMap.LocalIP, CType(MySetting.DiagnosticPort, Integer))
        If wsstarted = False Then
            MsgBox("Diagnostics port " + MySetting.DiagnosticPort + " is not working or blocked by firewall or anti virus, so region icons are disabled.", vbInformation, "Cannot HG")
            gUseIcons = False
            MySetting.DiagFailed = True
            MySetting.SaveSettings()
        End If

    End Sub

    Private Shared Function IsPrivateIP(CheckIP As String) As Boolean

        ''' <summary>
        ''' Checks to see if an IP address is a local IP address.
        ''' </summary>
        ''' <param name="CheckIP">The IP address to check, or localhost.</param>
        ''' <returns>Boolean</returns>
        ''' <remarks></remarks>
        ''' 
        If CheckIP = "localhost" Then Return True

        Dim Quad1, Quad2 As Integer

        Try
            Quad1 = CInt(CheckIP.Substring(0, CheckIP.IndexOf(".")))
            Quad2 = CInt(CheckIP.Substring(CheckIP.IndexOf(".") + 1).Substring(0, CheckIP.IndexOf(".")))
        Catch
        End Try

        Select Case Quad1
            Case 10
                Return True
            Case 172
                If Quad2 >= 16 And Quad2 <= 31 Then Return True
            Case 192
                If Quad2 = 168 Then Return True
        End Select
        Return False
    End Function

#End Region

#Region "UPnP"

    Function OpenRouterPorts() As Boolean

        If Not MyUPnpMap.UPnpEnabled And MySetting.UPnPEnabled Then
            Log("UPnP is not working in the router")
            MySetting.UPnPEnabled = False
            MySetting.SaveSettings()
            Return False
        End If

        If Not MySetting.UPnPEnabled Then

            Return True
        End If

        Log("Local IP seems to be " + MyUPnpMap.LocalIP)

        Try

            If MySetting.SC_Enable Then
                'Icecast 8080
                If MyUPnpMap.Exists(Convert.ToInt16(MySetting.SC_PortBase), UPnp.Protocol.TCP) Then
                    MyUPnpMap.Remove(Convert.ToInt16(MySetting.SC_PortBase), UPnp.Protocol.TCP)
                End If
                MyUPnpMap.Add(MyUPnpMap.LocalIP, CType(MySetting.SC_PortBase, Integer), UPnp.Protocol.TCP, "Icecast TCP Public " + MySetting.SC_PortBase.ToString)
                PrintFast("Icecast Port is set to " + MySetting.SC_PortBase.ToString)
                BumpProgress10()
            End If

            'diagnostics 8001
            If MyUPnpMap.Exists(Convert.ToInt16(MySetting.DiagnosticPort), UPnp.Protocol.TCP) Then
                MyUPnpMap.Remove(Convert.ToInt16(MySetting.DiagnosticPort), UPnp.Protocol.TCP)
            End If
            MyUPnpMap.Add(MyUPnpMap.LocalIP, Convert.ToInt16(MySetting.DiagnosticPort), UPnp.Protocol.TCP, "Opensim TCP Public " + MySetting.DiagnosticPort)
            PrintFast("Diagnostic Port is set to " + MySetting.DiagnosticPort)
            BumpProgress10()

            ' 8002 for TCP and UDP
            If MyUPnpMap.Exists(Convert.ToInt16(MySetting.HttpPort), UPnp.Protocol.TCP) Then
                MyUPnpMap.Remove(Convert.ToInt16(MySetting.HttpPort), UPnp.Protocol.TCP)
            End If
            MyUPnpMap.Add(MyUPnpMap.LocalIP, Convert.ToInt16(MySetting.HttpPort), UPnp.Protocol.TCP, "Opensim TCP Grid " + MySetting.HttpPort)
            PrintFast("Grid Port is set to " + MySetting.HttpPort)
            BumpProgress10()

            If MyUPnpMap.Exists(Convert.ToInt16(MySetting.HttpPort), UPnp.Protocol.UDP) Then
                MyUPnpMap.Remove(Convert.ToInt16(MySetting.HttpPort), UPnp.Protocol.UDP)
            End If
            MyUPnpMap.Add(MyUPnpMap.LocalIP, Convert.ToInt16(MySetting.HttpPort), UPnp.Protocol.UDP, "Opensim UDP Grid " + MySetting.HttpPort)
            PrintFast("Grid Port is set to " + MySetting.HttpPort)
            BumpProgress10()

            For Each X In RegionClass.RegionNumbers
                Dim R As Integer = RegionClass.RegionPort(X)

                If MyUPnpMap.Exists(R, UPnp.Protocol.UDP) Then
                    MyUPnpMap.Remove(R, UPnp.Protocol.UDP)
                End If
                MyUPnpMap.Add(MyUPnpMap.LocalIP, R, UPnp.Protocol.UDP, "Opensim UDP Region " & RegionClass.RegionName(X) & " ")
                PrintFast("Region " + RegionClass.RegionName(X) + " is set to " + Convert.ToString(R))
                BumpProgress(1)

                If MyUPnpMap.Exists(R, UPnp.Protocol.TCP) Then
                    MyUPnpMap.Remove(R, UPnp.Protocol.TCP)
                End If
                MyUPnpMap.Add(MyUPnpMap.LocalIP, R, UPnp.Protocol.TCP, "Opensim TCP Region " & RegionClass.RegionName(X) & " ")
                PrintFast("Region " + RegionClass.RegionName(X) + " is set to " + Convert.ToString(R))
                BumpProgress(1)
            Next

            If MyUPnpMap.Exists(Convert.ToInt16(MySetting.SC_PortBase), UPnp.Protocol.TCP) Then
                MyUPnpMap.Remove(Convert.ToInt16(MySetting.SC_PortBase), UPnp.Protocol.TCP)
            End If
            MyUPnpMap.Add(MyUPnpMap.LocalIP, MySetting.SC_PortBase, UPnp.Protocol.TCP, "Icecast TCP" + MySetting.SC_PortBase.ToString)
            PrintFast("Icecast Port is set to " + MySetting.SC_PortBase.ToString)


            BumpProgress10()

        Catch e As Exception
            Log("UPnP: UPnP Exception caught:  " + e.Message)
            Return False
        End Try
        Return True 'successfully added

    End Function


    Private Function GetPostData() As String

        Dim UPnp As String = "Fail"
        If MySetting.UPnpDiag Then
            UPnp = "Pass"
        End If
        Dim Loopb As String = "Fail"
        If MySetting.LoopBackDiag Then
            Loopb = "Pass"
        End If

        Dim Grid As String = "Grid"
        If MySetting.StandAlone() Then Grid = "Standalone"

        ' no DNS password used if DNS name is null
        Dim m = MySetting.MachineID()
        If MySetting.DNSName = "" Then
            m = ""
        End If

        Dim data As String = "&MachineID=" + m _
            + "&V=" + gMyVersion.ToString _
            + "&OV=" + gSimVersion.ToString _
            + "&uPnp=" + UPnp.ToString _
            + "&Loop=" + Loopb.ToString _
            + "&Type=" + Grid.ToString _
            + "&Ver=" + gMyVersion.ToString _
            + "&isPublic=" + MySetting.GDPR().ToString _
            + "&r=" + Random()
        Return data

    End Function

    Private Function OpenPorts() As Boolean

        Try
            If OpenRouterPorts() Then ' open UPnp port
                Log("UPnP: Ok")
                MySetting.UPnpDiag = True
                MySetting.SaveSettings()
                BumpProgress10()
                Return True
            Else
                Log("UPnP: fail")
                MySetting.UPnpDiag = False
                MySetting.SaveSettings()

                BumpProgress10()
                Return False
            End If
        Catch e As Exception
            Log("Error: UPnP Exception: " + e.Message)
            MySetting.UPnpDiag = False
            MySetting.SaveSettings()
            BumpProgress10()
            Return False
        End Try

    End Function

#End Region

#Region "MySQL"

    Private Sub CheckAndRepairDatbaseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CheckAndRepairDatbaseToolStripMenuItem.Click

        If Not StartMySQL() Then
            ProgressBar1.Value = 0
            ProgressBar1.Visible = True
            Buttons(StartButton)
            Print("Stopped")
            Return
        End If

        Dim pi As ProcessStartInfo = New ProcessStartInfo()

        ChDir(MyFolder & "\OutworldzFiles\mysql\bin")
        pi.WindowStyle = ProcessWindowStyle.Normal
        pi.Arguments = MySetting.MySqlPort
        pi.FileName = "Repair_ISAM.bat"
        Dim pMySqlDiag As Process = New Process()
        pMySqlDiag.StartInfo = pi
        pMySqlDiag.Start()
        pMySqlDiag.WaitForExit()

        pi.FileName = "CheckAndRepair.bat"
        Dim pMySqlDiag1 As Process = New Process()
        pMySqlDiag1.StartInfo = pi
        pMySqlDiag1.Start()
        pMySqlDiag1.WaitForExit()


        ChDir(MyFolder)

    End Sub

    Private Sub RestoreDatabaseToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles RestoreDatabaseToolStripMenuItem1.Click

        If OpensimIsRunning() Then
            Print("Cannot restore when Opensim is running. Click [Stop] and try again.")
            Return
        End If

        If Not StartMySQL() Then
            ProgressBar1.Value = 0
            ProgressBar1.Visible = True
            Buttons(StartButton)
            Print("Stopped")
            Return
        End If

        ' Create an instance of the open file dialog box.
        Dim openFileDialog1 As OpenFileDialog = New OpenFileDialog

        ' Set filter options and filter index.
        openFileDialog1.InitialDirectory = BackupPath()
        openFileDialog1.Filter = "BackupFile (*.sql)|*.sql|All Files (*.*)|*.*"
        openFileDialog1.FilterIndex = 1
        openFileDialog1.Multiselect = False

        ' Call the ShowDialog method to show the dialogbox.
        Dim UserClickedOK As DialogResult = openFileDialog1.ShowDialog

        ' Process input if the user clicked OK.
        If UserClickedOK = DialogResult.OK Then
            Dim thing = openFileDialog1.FileName
            If thing.Length > 0 Then

                Dim yesno = MsgBox("Are you sure? Your database will re-loaded from the backup and all existing content replaced. Avatars, sims, inventory, all of it.", vbYesNo, "Restore?")
                If yesno = vbYes Then
                    ' thing = thing.Replace("\", "/")    ' because Opensim uses unix-like slashes, that's why

                    Try
                        My.Computer.FileSystem.DeleteFile(MyFolder & "\OutworldzFiles\mysql\bin\RestoreMysql.bat")
                    Catch
                    End Try
                    Try
                        Dim filename As String = MyFolder & "\OutworldzFiles\mysql\bin\RestoreMysql.bat"
                        Using outputFile As New StreamWriter(filename, True)
                            outputFile.WriteLine("@REM A program to restore Mysql from a backup" + vbCrLf _
                                    + "mysql -u root opensim <  " + """" + thing + """" _
                                    + vbCrLf + "@pause" + vbCrLf)
                        End Using
                    Catch ex As Exception
                        ErrorLog("Failed to create restore file:" + ex.Message)
                        Return
                    End Try

                    Print("Starting restore - do not interrupt!")
                    Dim pMySqlRestore As Process = New Process()
                    Dim pi As ProcessStartInfo = New ProcessStartInfo()

                    ' pi.Arguments = thing
                    pi.WindowStyle = ProcessWindowStyle.Normal
                    pi.WorkingDirectory = MyFolder & "\OutworldzFiles\mysql\bin\"

                    pi.FileName = MyFolder & "\OutworldzFiles\mysql\bin\RestoreMysql.bat"
                    pMySqlRestore.StartInfo = pi
                    pMySqlRestore.Start()
                    Print("")
                End If
            Else
                Print("Restore cancelled")
            End If
        End If
    End Sub

    Private Sub BackupDatabaseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BackupDatabaseToolStripMenuItem.Click

        If Not StartMySQL() Then
            ProgressBar1.Value = 0
            ProgressBar1.Visible = True
            Buttons(StartButton)
            Print("Stopped")
            Return
        End If

        Print("Starting a slow but extensive Database Backup => Autobackup folder")
        Dim pMySqlBackup As Process = New Process()
        Dim pi As ProcessStartInfo = New ProcessStartInfo()
        pi.Arguments = ""
        pi.WindowStyle = ProcessWindowStyle.Normal
        pi.WorkingDirectory = MyFolder & "\OutworldzFiles\mysql\bin\"
        pi.FileName = MyFolder & "\OutworldzFiles\mysql\bin\BackupMysql.bat"
        pMySqlBackup.StartInfo = pi

        pMySqlBackup.Start()

        Print("")
    End Sub

    Public Function StartMySQL() As Boolean

        ' Check for MySql operation
        If gRobustConnStr = "" Then

            gRobustConnStr = "server=" + MySetting.RobustServer() _
                + ";database=" + MySetting.RobustDataBaseName _
                + ";port=" + MySetting.MySqlPort _
                + ";user=" + MySetting.RobustUsername _
                + ";password=" + MySetting.RobustPassword _
                + ";Old Guids=true;Allow Zero Datetime=true;"

            MysqlConn = New Mysql(gRobustConnStr)

        End If

        Dim isMySqlRunning = CheckPort(MySetting.RobustServer(), CType(MySetting.MySqlPort, Integer))

        If isMySqlRunning Then Return True
        ' Start MySql in background.

        BumpProgress10()
        Dim StartValue = ProgressBar1.Value
        Print("Starting Database")

        ' SAVE INI file
        MySetting.LoadOtherIni(MyFolder & "\OutworldzFiles\mysql\my.ini", "#")
        MySetting.SetOtherIni("mysqld", "basedir", """" + gCurSlashDir + "/OutworldzFiles/Mysql" + """")
        MySetting.SetOtherIni("mysqld", "datadir", """" + gCurSlashDir + "/OutworldzFiles/Mysql/Data" + """")
        MySetting.SetOtherIni("mysqld", "port", MySetting.MySqlPort)
        MySetting.SetOtherIni("client", "port", MySetting.MySqlPort)
        MySetting.SaveOtherINI()

        ' create test program 
        ' slants the other way:
        Dim testProgram As String = MyFolder & "\OutworldzFiles\Mysql\bin\StartManually.bat"
        Try
            My.Computer.FileSystem.DeleteFile(testProgram)
        Catch ex As Exception
            ErrorLog("Error: Cannot Delete File: " + ex.Message)
        End Try
        Try
            Using outputFile As New StreamWriter(testProgram, True)
                outputFile.WriteLine("@REM A program to run Mysql manually for troubleshooting." + vbCrLf _
                                     + "mysqld.exe --defaults-file=" + """" + gCurSlashDir + "/OutworldzFiles/mysql/my.ini" + """")
            End Using
        Catch ex As Exception
            ErrorLog("Error:Cannot write:" + ex.Message)
        End Try

        CreateService()
        CreateStopMySql()

        BumpProgress(5)

        ' Mysql was not running, so lets start it up.
        Dim pi As ProcessStartInfo = New ProcessStartInfo()

        pi.Arguments = "--defaults-file=" + """" + gCurSlashDir + "/OutworldzFiles/mysql/my.ini" + """"
        pi.WindowStyle = ProcessWindowStyle.Hidden
        pi.FileName = """" + MyFolder & "\OutworldzFiles\mysql\bin\mysqld.exe" + """"
        ProcessMySql.StartInfo = pi
        ProcessMySql.EnableRaisingEvents = True
        ProcessMySql.Start()

        ' wait for MySql to come up
        Dim MysqlOk As Boolean
        While Not MysqlOk And OpensimIsRunning And Not gExiting

            BumpProgress(1)
            Application.DoEvents()

            Dim MysqlLog As String = MyFolder + "\OutworldzFiles\mysql\data"
            If ProgressBar1.Value = 100 Then ' about 30 seconds when it fails

                Dim yesno = MsgBox("The database did not start. Do you want to see the log file?", vbYesNo, "Error")
                If (yesno = vbYes) Then
                    Dim files() As String
                    files = Directory.GetFiles(MysqlLog, "*.err", SearchOption.TopDirectoryOnly)
                    For Each FileName As String In files
                        System.Diagnostics.Process.Start("notepad.exe", FileName)
                    Next
                End If
                Buttons(StartButton)
                Return False
            End If

            ' check again
            Sleep(2000)
            MysqlOk = CheckMysql()
        End While

        If Not OpensimIsRunning Then Return False

        Return True

    End Function

    Private Sub CreateService()

        ' create test program 
        ' slants the other way:
        Dim testProgram As String = MyFolder & "\OutworldzFiles\Mysql\bin\InstallAsAService.bat"
        Try
            My.Computer.FileSystem.DeleteFile(testProgram)
        Catch ex As Exception
            ErrorLog("Delete File: " + ex.Message)
        End Try
        Try
            Using outputFile As New StreamWriter(testProgram, True)
                outputFile.WriteLine("@REM Program to run Mysql as a Service" + vbCrLf +
                "mysqld.exe --install Mysql --defaults-file=" + """" + gCurSlashDir + "/OutworldzFiles/mysql/my.ini" + """" + vbCrLf + "net start Mysql" + vbCrLf)
            End Using
        Catch ex As Exception
            ErrorLog("Error:Install As A Service" + ex.Message)
        End Try

    End Sub

    Private Sub CreateStopMySql()

        ' create test program 
        ' slants the other way:
        Dim testProgram As String = MyFolder & "\OutworldzFiles\Mysql\bin\StopMySQL.bat"
        Try
            My.Computer.FileSystem.DeleteFile(testProgram)
        Catch ex As Exception
            ErrorLog("Delete File: " + ex.Message)
        End Try
        Try
            Using outputFile As New StreamWriter(testProgram, True)
                outputFile.WriteLine("@REM Program to stop Mysql" + vbCrLf +
                "mysqladmin.exe -u root --port " + MySetting.MySqlPort + " shutdown" + vbCrLf + "@pause" + vbCrLf)
            End Using
        Catch ex As Exception
            ErrorLog("Error:StopMySQL.bat" + ex.Message)
        End Try

    End Sub

    Function CheckMysql() As Boolean

        Dim version As String = Nothing
        Try
            version = MysqlConn.IsMySqlRunning()
        Catch
            Log("MySQL was not running")
        End Try

        If version Is Nothing Then
            Return False
        End If
        Return True

    End Function

    Private Sub StopIcecast()

        Zap("icecast")

    End Sub

    Private Sub StopMysql()

        If Not gStopMysql Then
            Print("MySQL was running when I woke up, so I am leaving MySQL on.")
            Return
        End If

        Dim isMySqlRunning = CheckPort("127.0.0.1", CType(MySetting.MySqlPort, Integer))

        If Not isMySqlRunning Then Return


        Print("Stopping MySql")

        Try
            MysqlConn.Dispose()
        Catch
        End Try

        Dim p As Process = New Process()
        Dim pi As ProcessStartInfo = New ProcessStartInfo()
        pi.Arguments = "--port " + MySetting.MySqlPort + " -u root shutdown"
        pi.FileName = """" + MyFolder + "\OutworldzFiles\mysql\bin\mysqladmin.exe" + """"
        pi.UseShellExecute = True ' so we can redirect streams and minimize
        pi.WindowStyle = ProcessWindowStyle.Hidden
        p.StartInfo = pi

        Try
            p.Start()
            p.WaitForExit()
            p.Close()
        Catch ex As Exception
            ErrorLog("Error: failed to stop MySQL:" + ex.Message)
        End Try

    End Sub

#End Region

#Region "DNS"

    Public Function RegisterDNS() As Boolean

        If MySetting.DNSName = String.Empty Then
            Return True
        End If

        If IsPrivateIP(MySetting.DNSName) Then
            Return True
        End If

        PrintFast("Checking " + "http://" + MySetting.DNSName + ":" + MySetting.HttpPort)

        Dim client As New System.Net.WebClient
        Dim Checkname As String = String.Empty

        Try
            Checkname = client.DownloadString("http://outworldz.net/dns.plx?GridName=" + MySetting.DNSName + GetPostData())
        Catch ex As Exception
            ErrorLog("Warn:Cannot check the DNS Name" + ex.Message)
            Return False
        End Try
        If Checkname = "UPDATED" Then Return True
        Return False

    End Function

    Public Function DoGetHostAddresses(hostName As [String]) As String

        Try
            Dim IPList As System.Net.IPHostEntry = System.Net.Dns.GetHostEntry(hostName)

            For Each IPaddress In IPList.AddressList
                If (IPaddress.AddressFamily = Sockets.AddressFamily.InterNetwork) Then
                    Dim ip = IPaddress.ToString()
                    Return ip
                End If
            Next
            Return String.Empty

        Catch ex As Exception
            ErrorLog("Warn:Unable to resolve name:" + ex.Message)
        End Try
        Return String.Empty

    End Function

    Public Function GetNewDnsName() As String

        Dim client As New System.Net.WebClient
        Dim Checkname As String = String.Empty
        Try
            Checkname = client.DownloadString("http://outworldz.net/getnewname.plx/?r=" + Random())
        Catch ex As Exception
            ErrorLog("Error:Cannot get new name:" + ex.Message)
        End Try
        Return Checkname

    End Function

    Public Function RegisterName(name As String) As String

        Dim Checkname As String = String.Empty

        Try
            Checkname = client.DownloadString("http://outworldz.net/dns.plx/?GridName=" + name + GetPostData())

        Catch ex As Exception
            ErrorLog("Error: Cannot check the DNS Name" + ex.Message)
        End Try
        If Checkname = "NEW" Or Checkname = "UPDATED" Then
            Return name
        End If
        If Checkname = "NAK" Then
            MsgBox("Dynamic DNS Name already in use. Maybe you are using the wrong password?")
        End If
        Return ""

    End Function

    Private Sub NewDNSName()

        If MySetting.DNSName = "" And MySetting.EnableHypergrid Then
            Dim newname = GetNewDnsName()
            If newname <> "" Then
                If RegisterName(newname) <> "" Then
                    BumpProgress10()
                    MySetting.DNSName = newname
                    MySetting.PublicIP = newname
                    MySetting.SaveSettings()
                    MsgBox("Your system's name has been set to " + newname + ". You can change the name in the DNS menu at any time", vbInformation, "Info")
                End If
            End If
            BumpProgress10()
        End If

    End Sub


#End Region

#Region "Regions"

    Public Sub LoadRegionsStatsBar()

        SimulatorStatsToolStripMenuItem.DropDownItems.Clear()
        SimulatorStatsToolStripMenuItem.Visible = False

        If RegionClass Is Nothing Then Return

        For Each RegionNum In RegionClass.RegionNumbers

            Dim Menu As New ToolStripMenuItem
            Menu.Text = RegionClass.RegionName(RegionNum)
            Menu.ToolTipText = "Click to view stats on " + RegionClass.RegionName(RegionNum)
            Menu.DisplayStyle = ToolStripItemDisplayStyle.Text
            If RegionClass.Booted(RegionNum) Then
                Menu.Enabled = True
            Else
                Menu.Enabled = False
            End If

            AddHandler Menu.Click, New EventHandler(AddressOf Statmenu)
            SimulatorStatsToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {Menu})
            SimulatorStatsToolStripMenuItem.Visible = True

        Next
    End Sub


    Private Sub Statmenu(sender As Object, e As EventArgs)
        If OpensimIsRunning() Then
            Dim regionnum = RegionClass.FindRegionByName(CType(sender.text, String))
            Dim port As String = RegionClass.RegionPort(regionnum).ToString
            Dim webAddress As String = "http://localhost:" + MySetting.HttpPort + "/bin/data/sim.html?port=" + port
            Process.Start(webAddress)
        Else
            Print("Opensim is not running. Cannot open the Web Interface.")
        End If
    End Sub

    Private Sub RegionsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RegionsToolStripMenuItem.Click

        If RegionList.InstanceExists = False Then
            RegionForm = New RegionList
            RegionForm.Show()
            RegionForm.Activate()
        Else
            RegionForm.Show()
            RegionForm.Activate()
        End If

    End Sub

    Private Sub ScanAgents()
        ' Scan all the regions

        For Each X As Integer In RegionClass.RegionNumbers
            If RegionClass.Booted(X) Then
                RegionClass.AvatarCount(X) = MysqlConn.IsUserPresent(RegionClass.UUID(X))
                'Debug.Print(RegionClass.AvatarCount(X).ToString + " avatars in region " + RegionClass.RegionName(X))
            Else
                RegionClass.AvatarCount(X) = 0
            End If
        Next

    End Sub

#End Region

#Region "Alerts"

    Private Sub SendMsg(msg As String)

        If Not OpensimIsRunning() Then Print("Opensim is not running")

        For Each X As Integer In RegionClass.RegionNumbers
            If RegionClass.Booted(X) Then
                ConsoleCommand(RegionClass.GroupName(X), "set log level " + msg + "{ENTER}" + vbCrLf)
            End If
        Next

    End Sub
    Private Sub AllToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles All.Click
        SendMsg("all")
    End Sub
    Private Sub Info_Click(sender As Object, e As EventArgs) Handles Info.Click
        SendMsg("info")
    End Sub
    Private Sub debug_Click(sender As Object, e As EventArgs) Handles Debug.Click
        SendMsg("debug")
    End Sub
    Private Sub Warn_Click(sender As Object, e As EventArgs) Handles Warn.Click
        SendMsg("warn")
    End Sub
    Private Sub ErrorToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ErrorToolStripMenuItem.Click
        SendMsg("error")
    End Sub
    Private Sub Fatal1_Click(sender As Object, e As EventArgs) Handles Fatal1.Click
        SendMsg("fatal")
    End Sub
    Private Sub Off1_Click(sender As Object, e As EventArgs) Handles Off1.Click
        SendMsg("off")
    End Sub

    Private Sub ViewIcecastWebPageToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ViewIcecastWebPageToolStripMenuItem.Click
        If OpensimIsRunning() And MySetting.SC_Enable Then
            Dim webAddress As String = "http://" + MySetting.PublicIP + ":" + MySetting.SC_PortBase.ToString
            Print("Icecast lets you stream music into your sim. The Music URL is " + webAddress + "/stream")
            Process.Start(webAddress)
        ElseIf MySetting.SC_Enable = False Then
            Print("Shoutcast is not Enabled.")
        Else
            Print("Opensim is not running. Click Start to boot the system.")
        End If
    End Sub

    Private Sub RestartOneRegionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RestartOneRegionToolStripMenuItem.Click
        If Not OpensimIsRunning() Then
            Print("Opensim is not running")
            Return
        End If
        Dim name = ChooseRegion(True)
        Dim X = RegionClass.FindRegionByName(name)
        If X > -1 Then
            ConsoleCommand(RegionClass.GroupName(X), "change region " + name + "{ENTER}" + vbCrLf)
            ConsoleCommand(RegionClass.GroupName(X), "restart region " + name + "{ENTER}" + vbCrLf)
            UpdateView = True ' make form refresh
        End If

    End Sub

    Private Sub RestartTheInstanceToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RestartTheInstanceToolStripMenuItem.Click
        If Not OpensimIsRunning() Then
            Print("Opensim is not running")
            Return
        End If
        Dim name = ChooseRegion(True)
        Dim X = RegionClass.FindRegionByName(name)
        If X > -1 Then
            ConsoleCommand(RegionClass.GroupName(X), "restart{ENTER}" + vbCrLf)
            UpdateView = True ' make form refresh
        End If

    End Sub

    Private Sub SendScriptCmd(cmd As String)
        If Not OpensimIsRunning() Then
            Print("Opensim is not running")
            Return
        End If
        Dim rname = ChooseRegion(True)
        Dim X = RegionClass.FindRegionByName(rname)
        If X > -1 Then
            ConsoleCommand(RegionClass.GroupName(X), "change region " + rname + "{ENTER}" + vbCrLf)
            ConsoleCommand(RegionClass.GroupName(X), cmd + "{ENTER}" + vbCrLf)
        End If

    End Sub

    Private Sub ScriptsStopToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ScriptsStopToolStripMenuItem.Click
        SendScriptCmd("scripts stop")
    End Sub

    Private Sub ScriptsStartToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ScriptsStartToolStripMenuItem.Click
        SendScriptCmd("scripts start")
    End Sub

    Private Sub ScriptsSuspendToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ScriptsSuspendToolStripMenuItem.Click
        SendScriptCmd("scripts suspend")
    End Sub

    Private Sub ScriptsResumeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ScriptsResumeToolStripMenuItem.Click
        SendScriptCmd("scripts resume")
    End Sub

    Private Sub AllUsersAllSimsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles JustOneRegionToolStripMenuItem.Click

        If Not OpensimIsRunning() Then
            Print("Opensim is not running")
            Return
        End If
        Dim rname = ChooseRegion(True)
        If rname.Length > 0 Then
            Dim Message = InputBox("What do you want to say to this region?")
            Dim X = RegionClass.FindRegionByName(rname)
            ConsoleCommand(RegionClass.GroupName(X), "change region  " + RegionClass.RegionName(X) + "{ENTER}" + vbCrLf)
            ConsoleCommand(RegionClass.GroupName(X), "alert " + Message + "{ENTER}" + vbCrLf)
        End If

    End Sub

    Private Sub JustOneRegionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AllUsersAllSimsToolStripMenuItem.Click

        If Not OpensimIsRunning() Then
            Print("Opensim is not running")
            Return
        End If

        Dim HowManyAreOnline As Integer = 0
        Dim Message = InputBox("What do you want to say to everyone online?")
        If Message.Length > 0 Then
            For Each X As Integer In RegionClass.RegionNumbers
                If RegionClass.AvatarCount(X) > 0 Then
                    HowManyAreOnline = HowManyAreOnline + 1
                    ConsoleCommand(RegionClass.GroupName(X), "change region  " + RegionClass.RegionName(X) + "{ENTER}" + vbCrLf)
                    ConsoleCommand(RegionClass.GroupName(X), "alert " + Message + "{ENTER}" + vbCrLf)
                End If

            Next
            If HowManyAreOnline = 0 Then
                Print("Nobody is online")
            Else
                Print("Message sent to " + HowManyAreOnline.ToString() + " regions")
            End If
        End If

    End Sub

    Private Sub AddUserToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddUserToolStripMenuItem.Click
        RobustCommand("create user{ENTER}")
    End Sub

    Private Sub ChangePasswordToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ChangePasswordToolStripMenuItem.Click
        RobustCommand("reset user password{ENTER}")
    End Sub

    Private Sub ShowUserDetailsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowUserDetailsToolStripMenuItem.Click
        Dim person = InputBox("Enter the first and last name of the user:")
        If person.Length > 0 Then
            RobustCommand("show account " + person + "{ENTER}")
        End If
    End Sub


#End Region

#Region "LocalOARIAR"

    Private Sub LoadLocalIAROAR()
        ''' <summary>
        ''' Loads OAR and IAR from the menu
        ''' </summary>
        ''' <remarks>Handles both the IAR/OAR and Autobackup folders</remarks>
        Log("Local OAR")
        Dim MaxFileNum As Integer = 10
        Dim counter = MaxFileNum
        Dim Filename = MyFolder + "\OutworldzFiles\OAR\"
        Dim OARs As Array = Directory.GetFiles(Filename, "*.OAR", SearchOption.TopDirectoryOnly)

        For Each OAR As String In OARs
            counter = counter - 1
            If counter > 0 Then
                Dim Name = Path.GetFileName(OAR)
                Dim OarMenu As New ToolStripMenuItem
                OarMenu.Text = Name
                OarMenu.ToolTipText = "Click to load this content"
                OarMenu.DisplayStyle = ToolStripItemDisplayStyle.Text
                AddHandler OarMenu.Click, New EventHandler(AddressOf LocalOarClick)
                LoadLocalOARSToolStripMenuItem.Visible = True
                LoadLocalOARSToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {OarMenu})
                Log(Name)
            End If

        Next

        If MySetting.BackupFolder = "AutoBackup" Then
            Filename = MyFolder + "\OutworldzFiles\AutoBackup\"
        Else
            Filename = MySetting.BackupFolder
        End If

        Log("Auto OAR")
        Try
            Dim AutoOARs As Array = Directory.GetFiles(Filename, "*.OAR", SearchOption.TopDirectoryOnly)
            counter = MaxFileNum

            For Each OAR As String In AutoOARs
                counter = counter - 1
                If counter > 0 Then
                    Dim Name = Path.GetFileName(OAR)
                    Dim OarMenu As New ToolStripMenuItem
                    OarMenu.Text = Name
                    OarMenu.ToolTipText = "Click to load this content"
                    OarMenu.DisplayStyle = ToolStripItemDisplayStyle.Text
                    AddHandler OarMenu.Click, New EventHandler(AddressOf BackupOarClick)
                    LoadLocalOARSToolStripMenuItem.Visible = True
                    LoadLocalOARSToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {OarMenu})
                    Log(Name)
                End If

            Next
        Catch
        End Try
        ' now for the IARs

        Log("Local IAR")
        Filename = MyFolder + "\OutworldzFiles\IAR\"
        Dim IARs As Array = Directory.GetFiles(Filename, "*.IAR", SearchOption.TopDirectoryOnly)
        counter = MaxFileNum
        For Each IAR As String In IARs
            counter = counter - 1
            If counter > 0 Then
                Dim Name = Path.GetFileName(IAR)
                Dim IarMenu As New ToolStripMenuItem
                IarMenu.Text = Name
                IarMenu.ToolTipText = "Click to load this content"
                IarMenu.DisplayStyle = ToolStripItemDisplayStyle.Text
                AddHandler IarMenu.Click, New EventHandler(AddressOf LocalIarClick)
                LoadLocalIARsToolStripMenuItem.Visible = True
                LoadLocalIARsToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {IarMenu})
                Log(Name)
            End If

        Next

        If MySetting.BackupFolder = "AutoBackup" Then
            Filename = MyFolder + "\OutworldzFiles\AutoBackup\"
        Else
            Filename = MySetting.BackupFolder
        End If

        Try
            Log("Auto IAR")
            Dim AutoIARs As Array = Directory.GetFiles(Filename, "*.IAR", SearchOption.TopDirectoryOnly)
            counter = MaxFileNum
            For Each IAR As String In AutoIARs
                counter = counter - 1
                If counter > 0 Then
                    Dim Name = Path.GetFileName(IAR)
                    Dim IarMenu As New ToolStripMenuItem
                    IarMenu.Text = Name
                    IarMenu.ToolTipText = "Click to load this content"
                    IarMenu.DisplayStyle = ToolStripItemDisplayStyle.Text
                    AddHandler IarMenu.Click, New EventHandler(AddressOf BackupIarClick)
                    LoadLocalIARsToolStripMenuItem.Visible = True
                    LoadLocalIARsToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {IarMenu})
                    Log(Name)
                End If

            Next
        Catch
        End Try


    End Sub

    Private Sub LocalOarClick(sender As Object, e As EventArgs)

        Dim File = MyFolder + "/OutworldzFiles/OAR/" + sender.text.ToString 'make a real URL
        If LoadOARContent(File) Then
            Print("Opensimulator will load " + sender.text.ToString + ".  This may take time to load.")
        End If

    End Sub
    Private Sub BackupOarClick(sender As Object, e As EventArgs)

        Dim File = MyFolder + "/OutworldzFiles/AutoBackup/" + sender.text.ToString 'make a real URL
        If LoadOARContent(File) Then
            Print("Opensimulator will load " + sender.text.ToString + ".  This may take time to load.")
        End If

    End Sub

    Private Sub LocalIarClick(sender As Object, e As EventArgs)

        Dim File As String = MyFolder + "/OutworldzFiles/IAR/" + sender.text.ToString 'make a real URL
        If LoadIARContent(File) Then
            Print("Opensimulator will load " + sender.text.ToString + ".  This may take time to load.")
        End If

    End Sub

    Private Sub BackupIarClick(sender As Object, e As EventArgs)

        Dim File As String = MyFolder + "/OutworldzFiles/AutoBackup/" + sender.text.ToString 'make a real URL
        If LoadIARContent(File) Then
            Print("Opensimulator will load " + sender.text.ToString + ".  This may take time to load.")
        End If


    End Sub

    Private Sub HelpOnOARsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HelpOnOARsToolStripMenuItem.Click
        Dim webAddress As String = "http://opensimulator.org/wiki/Load_Oar_0.9.0%2B"
        Process.Start(webAddress)
    End Sub

    Private Sub HelpOnIARSToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HelpOnIARSToolStripMenuItem.Click
        Dim webAddress As String = "http://opensimulator.org/wiki/Inventory_Archives"
        Process.Start(webAddress)
    End Sub

    Private Sub BackupCriticalFilesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BackupCriticalFilesToolStripMenuItem.Click

        Dim msg = MsgBox("Make a backup of important files and the database? ", vbYesNo)
        Dim okay As Boolean
        If msg = vbYes Then
            okay = MakeBackup()
        End If

    End Sub

    Private Sub TechnicalInfoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TechnicalInfoToolStripMenuItem.Click
        Dim webAddress As String = "https://www.outworldz.com/Outworldz_installer/technical.htm"
        Process.Start(webAddress)
    End Sub

    Private Sub TroubleshootingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TroubleshootingToolStripMenuItem.Click
        Dim webAddress As String = "https://www.outworldz.com/Outworldz_installer/Manual_TroubleShooting.htm"
        Process.Start(webAddress)
    End Sub


#End Region

#Region "Help"
    Public Sub Help(page As String)
        ' Set the new form's desktop location so it appears below and
        ' to the right of the current form.
        FormHelp.Close()
        FormHelp = New FormHelp
        FormHelp.Activate()
        FormHelp.Visible = True
        FormHelp.Init(page)

    End Sub

    Public Sub HelpOnce(Webpage As String)

        ScreenPosition = New ScreenPos(Webpage)

        If Not ScreenPosition.Exists() Then
            ' Set the new form's desktop location so it appears below and
            ' to the right of the current form.
            FormHelp.Close()
            FormHelp = New FormHelp
            FormHelp.Activate()
            FormHelp.Visible = True
            FormHelp.Init(Webpage)

        End If
    End Sub

    Private Sub HelpStartingUpToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles HelpStartingUpToolStripMenuItem1.Click

        Help("Startup")

    End Sub

    Private Sub HelpClick(sender As Object, e As EventArgs)

        Help(sender.text.ToString)

    End Sub

    Private Sub LogViewClick(sender As Object, e As EventArgs)
        Dim name = sender.text.ToString()
        Dim path = MyFolder + "\Outworldzfiles\Opensim\bin\Regions\" + name + "\Opensim.log"
        If name = "Robust" Then path = MyFolder + "\Outworldzfiles\Opensim\bin\Robust.log"
        If name = "Outworldz" Then path = MyFolder + "\Outworldzfiles\Outworldz.log"
        If name = "UPnP" Then path = MyFolder + "\Outworldzfiles\Upnp.log"
        If name = "Icecast" Then path = MyFolder + "\Outworldzfiles\Icecast\log\error.log"
        If name = "All Settings" Then path = MyFolder + "\Outworldzfiles\Settings.ini"
        If name = "-------" Then Return

        If name = "MySQL" Then
            Dim MysqlLog As String = MyFolder + "\OutworldzFiles\mysql\data"
            Dim files() As String
            files = Directory.GetFiles(MysqlLog, "*.err", SearchOption.TopDirectoryOnly)
            For Each FileName As String In files
                System.Diagnostics.Process.Start("notepad.exe", FileName)
            Next
            Return
        End If

        System.Diagnostics.Process.Start("notepad.exe", path)

    End Sub

    Private Sub RevisionHistoryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RevisionHistoryToolStripMenuItem.Click
        Dim path = MyFolder + "/revisions.txt"
        System.Diagnostics.Process.Start("notepad.exe", path)
    End Sub

    Private Sub ThreadpoolsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ThreadpoolsToolStripMenuItem.Click
        For Each RegionNum As Integer In RegionClass.RegionListByGroupNum("*")
            ConsoleCommand(RegionClass.RegionName(RegionNum), "show threads{ENTER}" + vbCrLf)
        Next
    End Sub

    Private Sub XengineToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles XengineToolStripMenuItem.Click
        For Each RegionNum As Integer In RegionClass.RegionListByGroupNum("*")
            ConsoleCommand(RegionClass.RegionName(RegionNum), "xengine status{ENTER}" + vbCrLf)
        Next
    End Sub

    Private Sub JobEngineToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles JobEngineToolStripMenuItem.Click
        For Each RegionNum As Integer In RegionClass.RegionListByGroupNum("*")
            ConsoleCommand(RegionClass.RegionName(RegionNum), "debug jobengine status{ENTER}" + vbCrLf)
        Next
    End Sub

#End Region

End Class

