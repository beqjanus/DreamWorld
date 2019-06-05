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

Imports System.IO
Imports System.Management
Imports System.Net
Imports System.Net.Sockets
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Threading
Imports IWshRuntimeLibrary
Imports MySql.Data.MySqlClient
Imports System.Net.NetworkInformation

Public Class Form1

#Region "Declarations"

    ReadOnly gMyVersion As String = "2.94"
    ReadOnly gSimVersion As String = "0.9.0 2018-05-04"
    ReadOnly KillSource As Boolean = False      ' set to true to delete all source for Opensim

    Private _gDebug As Boolean = False  ' set by code to log some events in when running a debugger
    Private gExitHandlerIsBusy As Boolean = False

    ReadOnly gCPUMAX As Single = 75 ' max CPU % can be used when booting or we wait til it gets lower

    ' not https, which breaks stuff
    Private _gDomain As String = "http://www.outworldz.com"

    Private _gOpensimBinPath As String ' Holds path to Opensim folder
    Private _regionHandles As New Dictionary(Of Integer, String)
    Private _myFolder As String   ' Holds the current folder that we are running in
    Private _gCurSlashDir As String '  holds the current directory info in Unix format for MySQL and Apache
    Private _gIsRunning As Boolean = False ' used in OpensimIsRunning property
    Private _gChatTime As Integer     'amount of coffee the fairy had. Time for the chatty fairy to be read
    Dim client As New System.Net.WebClient ' downloadclient for web pages
    Public Shared MysqlConn As MysqlInterface

    ' with events
    Private WithEvents ApacheProcess As New Process()

    Public Event ApacheExited As EventHandler

    Dim gApacheProcessID As Integer = 0

    Private WithEvents ProcessMySql As Process = New Process()

    Public Event Exited As EventHandler

    Private WithEvents RobustProcess As New Process()

    Public Event RobustExited As EventHandler

    Private _exitList As New ArrayList()

    ' robust global PID
    Private _gRobustProcID As Integer

    Private _gRobustConnStr As String = ""
    Private _gMaxPortUsed As Integer = 0  'Max number of port used past 8004
    Dim gContentAvailable As Boolean = False ' assume there is no OAR and IAR data available
    Private _myUPnpMap As UPnp        ' UPNP gAborting
    Dim ws As NetServer             ' Port 8001 Webserver
    Private _gAborting As Boolean = False    ' Allows an Abort when Stopping is clicked

    Dim gDNSSTimer As Integer = 0    ' ping server every hour
    Dim gUseIcons As Boolean = True     ' if 8001 is blocked
    Dim gIPv4Address As String          ' global IPV4
    Private _mySetting As New MySettings  ' all settings from Settings.ini

    ' Shoutcast
    Dim gIcecastProcID As Integer = 0

    Private WithEvents IcecastProcess As New Process()
    Dim Adv As AdvancedForm

    Private _formCaches As New FormCaches

    ' Region
    Private _regionClass As RegionMaker   ' Global RegionClass

    Private _regionForm As RegionList

    Dim gStopMysql As Boolean = True    'lets us detct if Mysql is a service so we do not shut it down
    Private _gUpdateView As Boolean = True 'Region Form Refresh
    Dim gInitted As Boolean = False
    Private _gRestartNow As Boolean = False ' set true if a person clicks a restart button to get a sim restarted when auto restart is off
    Private _gSelectedBox As String = ""
    Private _gForceParcel As Boolean = False
    Private _gForceTerrain As Boolean = False
    Private _gForceMerge As Boolean = False
    Private _gUserName As String = ""

    ' Graph
    Dim cpu As New PerformanceCounter

    Dim speed As Single
    Dim speed1 As Single
    Dim speed2 As Single
    Dim speed3 As Single
    Dim MyCPUCollection As New Collection
    Private _viewedSettings As Boolean = False
    Dim MyRAMCollection As New Collection

    'Crashing
    Dim LogSearch As New CrashDetector()

    Public Enum SHOWWINDOWENUM As Integer
        SWHIDE = 0
        SWSHOWNORMAL = 1
        SWNORMAL = 1
        SWSHOWMINIMIZED = 2
        SWSHOWMAXIMIZED = 3
        SWMAXIMIZE = 3
        SWSHOWNOACTIVATE = 4
        SWSHOW = 5
        SWMINIMIZE = 6
        SWSHOWMINNOACTIVE = 7
        SWSHOWNA = 8
        SWRESTORE = 9
        SWSHOWDEFAULT = 10
        SWFORCEMINIMIZE = 11
        SWMAX = 11
    End Enum

    <CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA2101:SpecifyMarshalingForPInvokeStringArguments", MessageId:="1")>
    <CodeAnalysis.SuppressMessage("Microsoft.Interoperability", "CA1401:PInvokesShouldNotBeVisible")>
    <CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")>
    <DllImport("user32.dll")>
    Shared Function SetWindowText(ByVal hwnd As IntPtr, ByVal windowName As String) As Boolean
    End Function

    Dim newScreenPosition As ScreenPos

#End Region

#Region "ScreenSize"

    Private ScreenPosition As ScreenPos
    Private Handler As New EventHandler(AddressOf Resize_page)

    'The following detects  the location of the form in screen coordinates
    Private Sub Resize_page(ByVal sender As Object, ByVal e As System.EventArgs)
        'Me.Text = "Form screen position = " + Me.Location.ToString
        ScreenPosition.SaveXY(Me.Left, Me.Top)
        ScreenPosition.SaveHW(Me.Height, Me.Width)
    End Sub

    Private Sub SetScreen()

        ScreenPosition = New ScreenPos("Form1")
        AddHandler ResizeEnd, Handler
        Dim xy As List(Of Integer) = ScreenPosition.GetXY()
        Left = xy.Item(0)
        Top = xy.Item(1)

        Dim hw As List(Of Integer) = ScreenPosition.GetHW()

        If hw.Item(0) = 0 Then
            Me.Height = 180
        Else
            Me.Height = hw.Item(0)
        End If

        If hw.Item(1) = 0 Then
            Me.Width = 320
        Else
            Me.Width = hw.Item(1)
        End If

        ScreenPosition.SaveHW(Me.Height, Me.Width)

    End Sub

    Private Sub Form1_Layout(sender As Object, e As LayoutEventArgs) Handles Me.Layout

        Dim Y = Me.Height - 100
        TextBox1.Size = New System.Drawing.Size(TextBox1.Size.Width, Y)

    End Sub

#End Region

#Region "Properties"

    Public Property UpdateView() As Boolean
        Get
            Return GUpdateView
        End Get
        Set(ByVal Value As Boolean)
            GUpdateView = Value
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
            Return GIsRunning
        End Get
        Set(ByVal Value As Boolean)
            GIsRunning = Value
        End Set
    End Property

    Public Property GDebug As Boolean
        Get
            Return _gDebug
        End Get
        Set(value As Boolean)
            _gDebug = value
        End Set
    End Property

    Public Property GDomain As String
        Get
            Return _gDomain
        End Get
        Set(value As String)
            _gDomain = value
        End Set
    End Property

    Public Property GOpensimBinPath As String
        Get
            Return GOpensimBinPath1
        End Get
        Set(value As String)
            GOpensimBinPath1 = value
        End Set
    End Property

    Public Property GOpensimBinPath1 As String
        Get
            Return _gOpensimBinPath
        End Get
        Set(value As String)
            _gOpensimBinPath = value
        End Set
    End Property

    Public ReadOnly Property RegionHandles As Dictionary(Of Integer, String)
        Get
            Return _regionHandles
        End Get
    End Property

    Public Property MyFolder As String
        Get
            Return _myFolder
        End Get
        Set(value As String)
            _myFolder = value
        End Set
    End Property

    Public Property GCurSlashDir As String
        Get
            Return _gCurSlashDir
        End Get
        Set(value As String)
            _gCurSlashDir = value
        End Set
    End Property

    Public Property GIsRunning As Boolean
        Get
            Return _gIsRunning
        End Get
        Set(value As Boolean)
            _gIsRunning = value
        End Set
    End Property

    Public Property GChatTime As Integer
        Get
            Return _gChatTime
        End Get
        Set(value As Integer)
            _gChatTime = value
        End Set
    End Property

    Public ReadOnly Property ExitList As ArrayList
        Get
            Return _exitList
        End Get
    End Property

    Public Property GRobustProcID As Integer
        Get
            Return _gRobustProcID
        End Get
        Set(value As Integer)
            _gRobustProcID = value
        End Set
    End Property

    Public Property GRobustConnStr As String
        Get
            Return _gRobustConnStr
        End Get
        Set(value As String)
            _gRobustConnStr = value
        End Set
    End Property

    Public Property GMaxPortUsed As Integer
        Get
            Return GMaxPortUsed1
        End Get
        Set(value As Integer)
            GMaxPortUsed1 = value
        End Set
    End Property

    Public Property GMaxPortUsed1 As Integer
        Get
            Return _gMaxPortUsed
        End Get
        Set(value As Integer)
            _gMaxPortUsed = value
        End Set
    End Property

    Public Property MyUPnpMap As UPnp
        Get
            Return _myUPnpMap
        End Get
        Set(value As UPnp)
            _myUPnpMap = value
        End Set
    End Property

    Public Property GAborting As Boolean
        Get
            Return _gAborting
        End Get
        Set(value As Boolean)
            _gAborting = value
        End Set
    End Property

    Public Property MySetting As MySettings
        Get
            Return _mySetting
        End Get
        Set(value As MySettings)
            _mySetting = value
        End Set
    End Property

    Public Property FormCaches As FormCaches
        Get
            Return _formCaches
        End Get
        Set(value As FormCaches)
            _formCaches = value
        End Set
    End Property

    Public Property RegionClass As RegionMaker
        Get
            Return _regionClass
        End Get
        Set(value As RegionMaker)
            _regionClass = value
        End Set
    End Property

    Public Property RegionForm As RegionList
        Get
            Return _regionForm
        End Get
        Set(value As RegionList)
            _regionForm = value
        End Set
    End Property

    Public Property GUpdateView As Boolean
        Get
            Return _gUpdateView
        End Get
        Set(value As Boolean)
            _gUpdateView = value
        End Set
    End Property

    Public Property GRestartNow As Boolean
        Get
            Return _gRestartNow
        End Get
        Set(value As Boolean)
            _gRestartNow = value
        End Set
    End Property

    Public Property GSelectedBox As String
        Get
            Return _gSelectedBox
        End Get
        Set(value As String)
            _gSelectedBox = value
        End Set
    End Property

    Public Property GForceParcel As Boolean
        Get
            Return _gForceParcel
        End Get
        Set(value As Boolean)
            _gForceParcel = value
        End Set
    End Property

    Public Property GForceTerrain As Boolean
        Get
            Return _gForceTerrain
        End Get
        Set(value As Boolean)
            _gForceTerrain = value
        End Set
    End Property

    Public Property GForceMerge As Boolean
        Get
            Return _gForceMerge
        End Get
        Set(value As Boolean)
            _gForceMerge = value
        End Set
    End Property

    Public Property GUserName As String
        Get
            Return _gUserName
        End Get
        Set(value As String)
            _gUserName = value
        End Set
    End Property

    Public Property ViewedSettings As Boolean
        Get
            Return _viewedSettings
        End Get
        Set(value As Boolean)
            _viewedSettings = value
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
        Me.Hide()
        Application.EnableVisualStyles()

        ToolBar(False)  ' hide the avatar, RAM, CPU toolbar

        ' setup a debug path
        MyFolder = My.Application.Info.DirectoryPath

        If Debugger.IsAttached = True Then
            ' for debugging when compiling
            GDebug = True
            MyFolder = MyFolder.Replace("\Installer_Src\Setup DreamWorld\bin\Debug", "")
            MyFolder = MyFolder.Replace("\Installer_Src\Setup DreamWorld\bin\Release", "")
            ' for testing, as the compiler buries itself in ../../../debug
        End If
        GCurSlashDir = MyFolder.Replace("\", "/")    ' because Mysql uses unix like slashes, that's why
        GOpensimBinPath = MyFolder & "\OutworldzFiles\Opensim\"

        Log("Info", "Running")

        ' init the scrolling text box
        TextBox1.SelectionStart = 0
        TextBox1.ScrollToCaret()
        TextBox1.SelectionStart = TextBox1.Text.Length
        TextBox1.ScrollToCaret()

        KillOldFiles()

        MySetting.Init(MyFolder)

        MySetting.Myfolder = MyFolder

        ' Save a random machine ID - we don't want any data to be sent that's personal or identifiable,  but it needs to be unique
        Randomize()
        If MySetting.MachineID().Length = 0 Then MySetting.MachineID() = Random()  ' a random machine ID may be generated.  Happens only once

        'hide progress
        ProgressBar1.Visible = True
        ToolBar(False)

        ProgressBar1.Minimum = 0
        ProgressBar1.Maximum = 100
        ProgressBar1.Value = 0

        TextBox1.BackColor = Me.BackColor

        Buttons(BusyButton)
        SetScreen()     ' move Form to fit screen from SetXY.ini
        Me.Show()

        ' WebUI
        ViewWebUI.Visible = MySetting.WifiEnabled

        Me.Text = "Dreamgrid V" + gMyVersion

        GChatTime = MySetting.ChatTime

        OpensimIsRunning() = False ' true when opensim is running
        Me.Show()

        RegionClass = RegionMaker.Instance(MysqlConn)

        Adv = New AdvancedForm
        gInitted = True

        ClearLogFiles() ' clear log fles

        If Not IO.File.Exists(MyFolder & "\BareTail.udm") Then
            IO.File.Copy(MyFolder & "\BareTail.udm.bak", MyFolder & "\BareTail.udm")
        End If

        MyUPnpMap = New UPnp(MyFolder)

        MySetting.PublicIP = MyUPnpMap.LocalIP
        MySetting.PrivateURL = MySetting.PublicIP

        ' Set them back to the DNS name if there is one
        If MySetting.DNSName.Length > 0 Then
            MySetting.PublicIP = MySetting.DNSName
        End If

        If (MySetting.SplashPage.Length = 0) Then
            MySetting.SplashPage = GDomain + "/Outworldz_installer/Welcome.htm"
        End If

        ProgressBar1.Value = 100
        ProgressBar1.Value = 0

        CheckForUpdates()

        CheckDefaultPorts()

        ' must start after region Class is instantiated
        ws = NetServer.GetWebServer

        Print("Starting DreamGrid HTTP Server ")
        ws.StartServer(MyFolder, MySetting)

        OpenPorts()

        CheckDiagPort()

        SetQuickEditOff()

        SetLoopback()

        If Not SetIniData() Then Return

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

        Print("Setup Graphs")
        ' Graph fill
        Dim i = 180
        While i > 0
            MyCPUCollection.Add(0)
            i -= 1
        End While

        Dim msChart = ChartWrapper1.TheChart
        msChart.ChartAreas(0).AxisX.Maximum = 180
        msChart.ChartAreas(0).AxisX.Minimum = 0
        msChart.ChartAreas(0).AxisY.Maximum = 100
        msChart.ChartAreas(0).AxisY.Minimum = 0
        msChart.ChartAreas(0).AxisY.LabelStyle.Enabled = False
        ChartWrapper1.AddMarkers = True
        ChartWrapper1.MarkerFreq = 60
        'msChart.ChartAreas(0).AxisY.CLabels.RemoveAt(0)

        i = 180
        While i > 0
            MyRAMCollection.Add(0)
            i -= 1
        End While

        msChart = ChartWrapper2.TheChart
        msChart.ChartAreas(0).AxisX.Maximum = 180
        msChart.ChartAreas(0).AxisX.Minimum = 0
        msChart.ChartAreas(0).AxisY.Maximum = 100
        msChart.ChartAreas(0).AxisY.Minimum = 0
        msChart.ChartAreas(0).AxisY.LabelStyle.Enabled = False
        ChartWrapper2.AddMarkers = True
        ChartWrapper2.MarkerFreq = 60

        If MySetting.RegionListVisible Then
            ShowRegionform()
        End If
        Sleep(2000)
        Print("Check MySql")
        Dim isMySqlRunning = CheckPort("127.0.0.1", CType(MySetting.MySqlPort, Integer))
        If isMySqlRunning Then gStopMysql = False

        ' Find out if the system is already installed
        If System.IO.File.Exists(MyFolder & "\OutworldzFiles\Settings.ini") Then
            Application.DoEvents()

            Buttons(StartButton)
            ProgressBar1.Value = 100

            If MySetting.Autostart Then
                Print("Auto Startup")
                Startup()
            Else
                MySetting.SaveSettings()
                Print("Ready to Launch!" + vbCrLf + "Click 'Start' to begin your adventure in Opensimulator.")
            End If
        Else

            Print("Installing Desktop icon clicky thingy")
            Create_ShortCut(MyFolder & "\Start.exe")
            BumpProgress10()
            MySetting.SaveSettings()
            Print("Ready to Launch!")
            Buttons(StartButton)

        End If

        HelpOnce("License") ' license on bottom
        HelpOnce("Startup")

        ProgressBar1.Value = 100

    End Sub

    Private Sub SetLoopback()

        Dim Adapters = NetworkInterface.GetAllNetworkInterfaces()
        For Each adapter As NetworkInterface In Adapters
            If adapter.Name = "Loopback" Then

                Print("Setting Loopback to WAN IP address")

                Dim LoopbackProcess As New Process
                LoopbackProcess.StartInfo.UseShellExecute = True ' so we can redirect streams
                LoopbackProcess.StartInfo.FileName = MyFolder & "\NAT_Loopback_Tool.bat"
                LoopbackProcess.StartInfo.CreateNoWindow = False
                LoopbackProcess.StartInfo.Arguments = """" & adapter.Name & """"
                LoopbackProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal
                LoopbackProcess.Start()
            End If
        Next

    End Sub
    Private Sub KillFolder(AL As List(Of String))

        For Each folder As String In AL
            Try
                System.IO.Directory.Delete(MyFolder & folder, True)
            Catch ex As Exception
                Diagnostics.Debug.Print(ex.Message)
            End Try
        Next

    End Sub

    Private Sub KillFiles(AL As List(Of String))

        For Each filename As String In AL
            Try
                My.Computer.FileSystem.DeleteFile(MyFolder & filename)
            Catch ex As Exception
                Diagnostics.Debug.Print(ex.Message)
            End Try
        Next

    End Sub

    Private Sub KillOldFiles()

        Dim files As New List(Of String) From {
        "\Shoutcast", ' deprecated
        "\Icecast",   ' moved to Outworldzfiles
        "\Outworldzfiles\Opensim\bin\addins"
        }

        If KillSource Then
            files.Add("Outworldzfiles\Opensim\.nant")
            files.Add("Outworldzfiles\Opensim\addon-modules")
            files.Add("Outworldzfiles\Opensim\doc")
            files.Add("Outworldzfiles\Opensim\Opensim")
            files.Add("Outworldzfiles\Opensim\Prebuild")
            files.Add("Outworldzfiles\Opensim\share")
            files.Add("Outworldzfiles\Opensim\Thirdparty")

        End If

        KillFolder(files)   ' wipe these folders out
        files.Clear() ' now do a list of files to clean up

        ' necessary to kill these off as it is a badly behaved
        files.Add("\Outworldzfiles\Opensim\bin\OpenSim.Additional.AutoRestart.dll")
        files.Add("\Outworldzfiles\Opensim\bin\OpenSim.Additional.AutoRestart.pdb")
        files.Add("\Outworldzfiles\Opensim\bin\config-include\Birds.ini") ' no need for birds yet
        files.Add("SET_externalIP-Log.txt")

        ' crapload of old DLLS have to be eliminated
        CleanDLLs() ' drop old opensim dll's

        If KillSource Then
            files.Add("\Outworldzfiles\Opensim\BUILDING.md")
            files.Add("\Outworldzfiles\Opensim\compile.bat")
            files.Add("\Outworldzfiles\Opensim\Makefile")
            files.Add("\Outworldzfiles\Opensim\nant-color")
            files.Add("\Outworldzfiles\Opensim\OpenSim.build")
            files.Add("\Outworldzfiles\Opensim\OpenSim.sln")
            files.Add("\Outworldzfiles\Opensim\prebuild.xml")
            files.Add("\Outworldzfiles\Opensim\runprebuild.bat")
            files.Add("\Outworldzfiles\Opensim\runprebuild.sh")
            files.Add("\Outworldzfiles\Opensim\TESTING.txt")
        End If

        KillFiles(files)   ' wipe these files out

    End Sub

    Private Sub TextBox1_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox1.TextChanged
        Dim ln As Integer = TextBox1.Text.Length
        TextBox1.SelectionStart = ln
        TextBox1.ScrollToCaret()
    End Sub

    ''' <summary>
    ''' Start Button on main form
    ''' </summary>
    Private Sub StartButton_Click(sender As System.Object, e As System.EventArgs) Handles StartButton.Click
        Startup()
    End Sub

    ''' <summary>
    ''' Startup() Starts opensimulator system
    ''' Called by Start Button or by AutoStart
    ''' </summary>
    Public Sub Startup()

        TextBox1.AllowDrop = True

        With cpu
            .CategoryName = "Processor"
            .CounterName = "% Processor Time"
            .InstanceName = "_Total"
        End With

        Print("Starting...")
        gExitHandlerIsBusy = False
        GAborting = False  ' suppress exit warning messages
        ProgressBar1.Value = 0
        ProgressBar1.Visible = True
        ToolBar(False)
        Buttons(BusyButton)

        Print("Setup Ports")
        RegionClass.UpdateAllRegionPorts() ' must be done before we are running

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

        If ViewedSettings Then
            Print("Read Region INI files")
            RegionClass.GetAllRegions()

            If SetPublicIP() Then
                OpenPorts()
            End If

            If Not SetIniData() Then Return   ' set up the INI files
        End If

        If Not StartMySQL() Then
            ProgressBar1.Value = 0
            ProgressBar1.Visible = True
            ToolBar(False)
            Buttons(StartButton)
            Print("Stopped")
            Return
        End If

        SetupSearch()

        StartApache()

        ' old files to clean up
        Try
            If MySetting.BirdsModuleStartup Then
                My.Computer.FileSystem.CopyFile(GOpensimBinPath + "\bin\OpenSimBirds.Module.bak", GOpensimBinPath + "\bin\OpenSimBirds.Module.dll")
            Else
                My.Computer.FileSystem.DeleteFile(GOpensimBinPath + "\bin\OpenSimBirds.Module.dll")
            End If
        Catch ex As Exception
        End Try

        If Not StartRobust() Then
            Return
        End If

        If Not MySetting.RunOnce Then
            ConsoleCommand("Robust", "create user{ENTER}")
            MsgBox("Please type the Grid Owner's avatar name into the Robust window. Press <enter> for UUID and Model name. Then press this OK button", vbInformation, "Info")
            MySetting.RunOnce = True
            MySetting.SaveSettings()
        End If

        Timer1.Interval = 1000
        Timer1.Start() 'Timer starts functioning

        ' Launch the rockets
        Print("Start Regions")
        If Not StartOpensimulator() Then
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
        ToolBar(True)
    End Sub

    Private Sub Form1_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        ReallyQuit()
    End Sub

    Private Sub MnuExit_Click(sender As System.Object, e As System.EventArgs) Handles mnuExit.Click
        ReallyQuit()
    End Sub

    Private Sub JustQuitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles JustQuitToolStripMenuItem.Click
        ProgressBar1.Value = 0
        Print("Zzzz...")
        End
    End Sub

    Private Sub ReallyQuit()

        If Not KillAll() Then Return
        ws.StopWebServer()
        GAborting = True
        StopMysql()
        Print("I'll tell you my next dream when I wake up.")
        ProgressBar1.Value = 0
        Print("Zzzz...")
        End

    End Sub

    Shared Function ShowDOSWindow(handle As IntPtr, command As SHOWWINDOWENUM) As Boolean

        Dim ctr = 50
        If handle <> IntPtr.Zero Then
            Dim x = False

            While Not x And ctr > 0
                Sleep(100)
                Try
                    x = ShowWindow(handle, command)
                    If x Then Return True
                Catch ex As Exception
                End Try
                ctr -= 1
            End While
        End If
        Return False

    End Function

    Public Declare Function ShowWindow Lib "user32.dll" (ByVal hWnd As IntPtr, ByVal nCmdShow As SHOWWINDOWENUM) As Boolean

    Public Function KillAll() As Boolean

#Disable Warning CA1820 ' Test for empty strings using string length
        If AvatarLabel.Text <> "" Then
#Enable Warning CA1820 ' Test for empty strings using string length
            If CType(AvatarLabel.Text, Integer) > 0 Then
                Dim response = MsgBox("There are " & AvatarLabel.Text & " avatars in world! Do you really wish to quit?", vbYesNo)
                If response = vbNo Then Return False
            End If
        End If

        AvatarLabel.Text = ""
        GAborting = True
        ProgressBar1.Value = 100
        ProgressBar1.Visible = True
        ToolBar(False)
        ' close everything as gracefully as possible.

        StopIcecast()
        StopApache()

        Dim n As Integer = RegionClass.RegionCount()
        Diagnostics.Debug.Print("N=" + n.ToString())

        Dim TotalRunningRegions As Integer

        For Each Regionnumber As Integer In RegionClass.RegionNumbers
            If RegionClass.IsBooted(Regionnumber) Then
                TotalRunningRegions += 1
            End If
        Next
        Log("Info", "Total Enabled Regions=" + TotalRunningRegions.ToString)

        For Each X As Integer In RegionClass.RegionNumbers
            If OpensimIsRunning() And RegionClass.RegionEnabled(X) And
            Not (RegionClass.Status(X) = RegionMaker.SIMSTATUSENUM.RecyclingDown _
            Or RegionClass.Status(X) = RegionMaker.SIMSTATUSENUM.ShuttingDown _
            Or RegionClass.Status(X) = RegionMaker.SIMSTATUSENUM.Stopped) Then
                Print(RegionClass.RegionName(X) & " is going down now")
                SequentialPause()
                ConsoleCommand(RegionClass.GroupName(X), "q{ENTER}" + vbCrLf)
                RegionClass.Status(X) = RegionMaker.SIMSTATUSENUM.ShuttingDown
                RegionClass.Timer(X) = RegionMaker.REGIONTIMER.Stopped
                UpdateView = True ' make form refresh
            End If
        Next

        Dim counter = 600 ' 10 minutes to quit all regions

        ' only wait if the port 8001 is working
        If gUseIcons Then
            If OpensimIsRunning Then Print("Waiting for all regions to exit")

            While (counter > 0 And OpensimIsRunning())
                ' decrement progress bar according to the ratio of what we had / what we have now
                counter -= 1
                Dim CountisRunning As Integer = 0

                For Each X In RegionClass.RegionNumbers
                    If (Not RegionClass.Status(X) = RegionMaker.SIMSTATUSENUM.Stopped) And RegionClass.RegionEnabled(X) Then
                        If CheckPort(MySetting.PrivateURL, RegionClass.GroupPort(X)) Then
                            CountisRunning += 1
                        Else
                            StopGroup(RegionClass.GroupName(X))
                        End If
                        'UpdateView = True ' make form refresh
                    End If
                    Sleep(1000)
                    If CountisRunning = 1 Then
                        Print("1 region is still running")
                    Else
                        Print(CountisRunning.ToString & " regions are still running")
                    End If
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

        StopAllRegions()

        UpdateView = True ' make form refresh

        ConsoleCommand("Robust", "q{ENTER}" + vbCrLf)

        ' cannot load OAR or IAR, either
        IslandToolStripMenuItem.Visible = False
        ClothingInventoryToolStripMenuItem.Visible = False
        Timer1.Stop()
        OpensimIsRunning() = False
        Me.AllowDrop = False
        ProgressBar1.Value = 0
        ProgressBar1.Visible = False
        ToolBar(False)
        Return True

    End Function

    ''' <summary>
    ''' Kill processes by name
    ''' </summary>
    ''' <param name="processName"></param>
    ''' <returns></returns>
    Private Sub Zap(processName As String)

        ' Kill process by name
        For Each P As Process In System.Diagnostics.Process.GetProcessesByName(processName)
            Try
                Log("Info", "Stopping process " + processName)
                P.Kill()
            Catch ex As Exception
                Log("Info", "failed to stop " + processName)
            End Try
        Next

    End Sub

    Private Sub CleanDLLs()

        Dim dlls As List(Of String) = GetDlls(MyFolder & "/dlls.txt")
        Dim localdlls As List(Of String) = GetFilesRecursive(GOpensimBinPath & "bin")
        For Each localdllname In localdlls

            'Diagnostics.Debug.Print(localdllname)

            'For Each thing In dlls
            ' Diagnostics.Debug.Print(thing)
            'Next

            Dim x = localdllname.IndexOf("OutworldzFiles")
            Dim newlocaldllname = Mid(localdllname, x)
            If Not CompareDLLignoreCase(newlocaldllname, dlls) Then
                Log("INFO", "Deleting dll " & localdllname)
                My.Computer.FileSystem.DeleteFile(localdllname)
            End If
        Next

    End Sub

    Shared Function CompareDLLignoreCase(tofind As String, dll As List(Of String)) As Boolean
        For Each filename In dll
            If tofind.ToLower = filename.ToLower Then Return True
        Next
        Return False
    End Function

    ''' <summary>
    ''' This method starts at the specified directory.
    ''' It traverses all subdirectories.
    ''' It returns a List of those directories.
    ''' </summary>
    Shared Function GetFilesRecursive(ByVal initial As String) As List(Of String)
        ' This list stores the results.
        Dim result As New List(Of String)

        ' This stack stores the directories to process.
        Dim stack As New Stack(Of String)

        ' Add the initial directory
        stack.Push(initial)

        ' Continue processing for each stacked directory
        Do While (stack.Count > 0)
            ' Get top directory string
            Dim dir As String = stack.Pop
            Try
                ' Add all immediate file paths
                result.AddRange(Directory.GetFiles(dir, "*.dll"))

                ' Loop through all subdirectories and add them to the stack.
                Dim directoryName As String = ""

                'Save, but skip scriptengines
                For Each directoryName In Directory.GetDirectories(dir)
                    If Not directoryName.Contains("ScriptEngines") Then
                        stack.Push(directoryName)
                    Else
                        Diagnostics.Debug.Print("Skipping script")
                    End If
                Next
            Catch ex As Exception
            End Try
        Loop

        ' Return the list
        Return result
    End Function

    Shared Function GetDlls(fname As String) As List(Of String)

        Dim DllList As New List(Of String)

        If System.IO.File.Exists(fname) Then
            Dim reader As System.IO.StreamReader
            Dim line As String

            reader = System.IO.File.OpenText(fname)
            'now loop through each line
            While reader.Peek <> -1
                line = reader.ReadLine()
                DllList.Add(line)
            End While
        End If
        Return DllList

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
        Application.DoEvents()

    End Sub

    Private Sub Create_ShortCut(ByVal sTargetPath As String)
        ' Requires reference to Windows Script Host Object Model
        Dim WshShell As WshShellClass = New WshShellClass
        Dim MyShortcut As IWshRuntimeLibrary.IWshShortcut
        Log("Info", "creating shortcut on desktop")
        ' The shortcut will be created on the desktop
        Dim DesktopFolder As String = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)
        MyShortcut = CType(WshShell.CreateShortcut(DesktopFolder & "\Outworldz.lnk"), IWshRuntimeLibrary.IWshShortcut)
        MyShortcut.TargetPath = sTargetPath
        MyShortcut.IconLocation = WshShell.ExpandEnvironmentStrings(MyFolder & "\Start.exe")
        MyShortcut.WorkingDirectory = MyFolder
        MyShortcut.Save()

    End Sub

    Public Sub Print(Value As String)

        Log("Info", "" + Value)
        TextBox1.Text = TextBox1.Text & vbCrLf & Value
        Trim()

    End Sub

    Private Sub Trim()
        If TextBox1.Text.Length > TextBox1.MaxLength - 100 Then
            TextBox1.Text = Mid(TextBox1.Text, 500)
        End If
    End Sub

    Private Sub MnuAbout_Click(sender As System.Object, e As System.EventArgs) Handles mnuAbout.Click

        Print("(c) 2017 Outworldz,LLC" + vbCrLf + "Version " + gMyVersion)
        Dim webAddress As String = GDomain + "/Outworldz_Installer"
        Process.Start(webAddress)

    End Sub

    Private Sub StopButton_Click_1(sender As System.Object, e As System.EventArgs) Handles StopButton.Click

        Print("Stopping")
        Buttons(BusyButton)
        If Not KillAll() Then Return
        Buttons(StartButton)
        Print("Stopped")
        ProgressBar1.Visible = False
        ToolBar(False)

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

    Shared Function Random() As String
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

    Public Sub SaveIceCast()

        Dim rgx As New Regex("[^a-zA-Z0-9 ]")
        Dim name As String = rgx.Replace(MySetting.SimName, "")

        Dim icecast As String = "<icecast>" + vbCrLf +
                           "<hostname>" + MySetting.PublicIP + "</hostname>" + vbCrLf +
                            "<location>" + name + "</location>" + vbCrLf +
                            "<admin>" + MySetting.AdminEmail + "</admin>" + vbCrLf +
                            "<shoutcast-mount>/stream</shoutcast-mount>" + vbCrLf +
                            "<listen-socket>" + vbCrLf +
                            "    <port>" + MySetting.SCPortBase.ToString() + "</port>" + vbCrLf +
                            "</listen-socket>" + vbCrLf +
                            "<listen-socket>" + vbCrLf +
                            "   <port>" + MySetting.SCPortBase1.ToString() + "</port>" + vbCrLf +
                            "   <shoutcast-compat>1</shoutcast-compat>" + vbCrLf +
                            "</listen-socket>" + vbCrLf +
                             "<limits>" + vbCrLf +
                              "   <clients>20</clients>" + vbCrLf +
                              "    <sources>4</sources>" + vbCrLf +
                              "    <queue-size>524288</queue-size>" + vbCrLf +
                              "     <client-timeout>30</client-timeout>" + vbCrLf +
                              "    <header-timeout>15</header-timeout>" + vbCrLf +
                              "    <source-timeout>10</source-timeout>" + vbCrLf +
                              "    <burst-on-connect>1</burst-on-connect>" + vbCrLf +
                              "    <burst-size>65535</burst-size>" + vbCrLf +
                              "</limits>" + vbCrLf +
                              "<authentication>" + vbCrLf +
                                  "<source-password>" + MySetting.SCPassword + "</source-password>" + vbCrLf +
                                  "<relay-password>" + MySetting.SCPassword + "</relay-password>" + vbCrLf +
                                  "<admin-user>admin</admin-user>" + vbCrLf +
                                  "<admin-password>" + MySetting.SCPassword + "</admin-password>" + vbCrLf +
                              "</authentication>" + vbCrLf +
                              "<http-headers>" + vbCrLf +
                              "    <header name=" + """" + "Access-Control-Allow-Origin" + """" + " value=" + """" + "*" + """" + "/>" + vbCrLf +
                              "</http-headers>" + vbCrLf +
                              "<fileserve>1</fileserve>" + vbCrLf +
                              "<paths>" + vbCrLf +
                                  "<logdir>./log</logdir>" + vbCrLf +
                                  "<webroot>./web</webroot>" + vbCrLf +
                                  "<adminroot>./admin</adminroot>" + vbCrLf +  '
                                   "<alias source=" + """" + "/" + """" + " destination=" + """" + "/status.xsl" + """" + "/>" + vbCrLf +
                              "</paths>" + vbCrLf +
                              "<logging>" + vbCrLf +
                                  "<accesslog>access.log</accesslog>" + vbCrLf +
                                  "<errorlog>error.log</errorlog>" + vbCrLf +
                                  "<loglevel>3</loglevel>" + vbCrLf +
                                  "<logsize>10000</logsize>" + vbCrLf +
                              "</logging>" + vbCrLf +
                          "</icecast>" + vbCrLf

        Using outputFile As New StreamWriter(MyFolder + "\Outworldzfiles\Icecast\icecast_run.xml", False)
            outputFile.WriteLine(icecast)
        End Using

    End Sub

    Public Sub CopyWifi(Page As String)
        Try
            System.IO.Directory.Delete(GOpensimBinPath + "WifiPages", True)
            System.IO.Directory.Delete(GOpensimBinPath + "bin\WifiPages", True)
        Catch ex As Exception
            Log("Info", "" & ex.Message)
        End Try

        Try
            My.Computer.FileSystem.CopyDirectory(GOpensimBinPath + "WifiPages-" + Page, GOpensimBinPath + "WifiPages", True)
            My.Computer.FileSystem.CopyDirectory(GOpensimBinPath + "bin\WifiPages-" + Page, GOpensimBinPath + "\bin\WifiPages", True)
        Catch ex As Exception
            ErrorLog(ex.Message)
        End Try

    End Sub

    Private Sub SetDefaultSims()

        Dim reader As System.IO.StreamReader
        Dim line As String

        Try
            ' add this sim name as a default to the file as HG regions, and add the other regions as fallback

            ' it may have been deleted
            Dim o As Integer = RegionClass.FindRegionByName(MySetting.WelcomeRegion)

            If o < 0 Then
                o = 0
            End If

            ' save to disk
            Dim DefaultName = RegionClass.RegionName(o)
            MySetting.WelcomeRegion = DefaultName
            MySetting.SaveSettings()

            '(replace spaces with underscore)
            DefaultName = DefaultName.Replace(" ", "_")    ' because this is a screwy thing they did in the INI file

            Dim onceflag As Boolean = False ' only do the DefaultName
            Dim counter As Integer = 0

            Try
                My.Computer.FileSystem.DeleteFile(GOpensimBinPath + "bin\Robust.tmp")
            Catch ex As Exception
                'Nothing to do, this was just cleanup
            End Try

            Using outputFile As New StreamWriter(GOpensimBinPath + "bin\Robust.tmp")
                reader = System.IO.File.OpenText(GOpensimBinPath + "bin\Robust.HG.ini")
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
                    My.Computer.FileSystem.DeleteFile(GOpensimBinPath + "bin\Robust.HG.ini.bak")
                Catch ex As Exception
                    'Nothing to do, this was just cleanup
                End Try
                My.Computer.FileSystem.RenameFile(GOpensimBinPath + "bin\Robust.HG.ini", "Robust.HG.ini.bak")
                My.Computer.FileSystem.RenameFile(GOpensimBinPath + "bin\Robust.tmp", "Robust.HG.ini")
            Catch ex As Exception
                ErrorLog("Error:Set Default sims could not rename the file:" + ex.Message)
                My.Computer.FileSystem.RenameFile(GOpensimBinPath + "bin\Robust.HG.ini.bak", "Robust.HG.ini")
            End Try
        Catch ex As Exception
            MsgBox("Could not set default sim for visitors. ", vbInformation, "Settings")
        End Try

    End Sub

    ''' <summary>
    '''  Loads the INI file for the proper grid type for parsing
    ''' </summary>
    ''' <returns>
    ''' Returns the path to the proper Opensim.ini prototype.
    ''' </returns>
    Function GetOpensimProto() As String

        Select Case MySetting.ServerType
            Case "Robust"
                MySetting.LoadOtherIni(GOpensimBinPath + "bin\Opensim.proto", ";")
                Return GOpensimBinPath + "bin\Opensim.proto"
            Case "Region"
                MySetting.LoadOtherIni(GOpensimBinPath + "bin\OpensimRegion.proto", ";")
                Return GOpensimBinPath + "bin\OpensimRegion.proto"
            Case "OsGrid"
                MySetting.LoadOtherIni(GOpensimBinPath + "bin\OpensimOsGrid.proto", ";")
                Return GOpensimBinPath + "bin\OpensimOsGrid.proto"
            Case "Metro"
                MySetting.LoadOtherIni(GOpensimBinPath + "bin\OpensimMetro.proto", ";")
                Return GOpensimBinPath + "bin\OpensimMetro.proto"
        End Select
        Return Nothing

    End Function

    Sub DelLibrary()

        Try
            System.IO.File.Delete(GOpensimBinPath + "bin\Library\Clothing Library (small).iar")
            System.IO.File.Delete(GOpensimBinPath + "bin\Library\Objects Library (small).iar")
        Catch ex As Exception
            Diagnostics.Debug.Print(ex.Message)
        End Try
    End Sub

    Private Function SetIniData() As Boolean

        'mnuShow shows the DOS box for Opensimulator
        mnuShow.Checked = MySetting.ConsoleShow
        mnuHide.Checked = Not MySetting.ConsoleShow

        Print("Creating INI Files")

        If MySetting.ConsoleShow Then
            Log("Info", "Console will be shown")
        Else
            Log("Info", "Console will not be shown")
        End If

        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ' set the defaults in the INI for the viewer to use. Painful to do as it's a Left hand side edit

        SetDefaultSims()
        ''''''''''''''''''''''''''''''''''''''''''''''''

        ' TOSModule
        If (False) Then
            MySetting.LoadOtherIni(GOpensimBinPath + "bin\DivaTOS.ini", ";")

            'Disable it as it is broken for now.

            'MySetting.SetOtherIni("TOSModule", "Enabled", MySetting.TOSEnabled)
            MySetting.SetOtherIni("TOSModule", "Enabled", False.ToString)
            'MySetting.SetOtherIni("TOSModule", "Message", MySetting.TOSMessage)
            'MySetting.SetOtherIni("TOSModule", "Timeout", MySetting.TOSTimeout)
            MySetting.SetOtherIni("TOSModule", "ShowToLocalUsers", MySetting.ShowToLocalUsers.ToString)
            MySetting.SetOtherIni("TOSModule", "ShowToForeignUsers", MySetting.ShowToForeignUsers.ToString)
            MySetting.SetOtherIni("TOSModule", "TOS_URL", "http://" + MySetting.PublicIP + ":" + MySetting.HttpPort + "/wifi/termsofservice.html")
            MySetting.SaveOtherINI()
        End If

        ' Choose a GridCommon.ini to use.
        Dim GridCommon As String = "GridcommonGridServer"
        DelLibrary()
        Select Case MySetting.ServerType
            Case "Robust"
                My.Computer.FileSystem.CopyDirectory(GOpensimBinPath + "bin\Library.proto", GOpensimBinPath + "bin\Library", True)
                GridCommon = "Gridcommon-GridServer.ini"
            Case "Region"
                My.Computer.FileSystem.CopyDirectory(GOpensimBinPath + "bin\Library.proto", GOpensimBinPath + "bin\Library", True)
                GridCommon = "Gridcommon-RegionServer.ini"
            Case "OsGrid"
                GridCommon = "Gridcommon-OsGridServer.ini"
            Case "Metro"
                GridCommon = "Gridcommon-MetroServer.ini"
        End Select

        ' Put that gridcommon.ini file in place
        IO.File.Copy(GOpensimBinPath + "bin\config-include\" & GridCommon, IO.Path.Combine(GOpensimBinPath, "bin\config-include\GridCommon.ini"), True)

        ' load and patch it up for MySQL
        MySetting.LoadOtherIni(GOpensimBinPath + "bin\config-include\Gridcommon.ini", ";")
        Dim ConnectionString = """" _
        + "Data Source=" + MySetting.RegionServer _
        + ";Database=" + MySetting.RegionDBName _
        + ";Port=" + MySetting.RegionPort _
        + ";User ID=" + MySetting.RegionDBUsername _
        + ";Password=" + MySetting.RegionDbPassword _
        + ";Old Guids=true;Allow Zero Datetime=true;" _
        + ";Connect Timeout=28800;Command Timeout=28800;" _
        + """"
        MySetting.SetOtherIni("DatabaseService", "ConnectionString", ConnectionString)
        MySetting.SaveOtherINI()

        ''''''''''''''''''''''''''''''''''''''''''
        If MySetting.ServerType = "Robust" Then
            ' Robust Process
            MySetting.LoadOtherIni(GOpensimBinPath + "bin\Robust.HG.ini", ";")

            ConnectionString = """" _
            + "Data Source=" + MySetting.RobustServer _
            + ";Database=" + MySetting.RobustDataBaseName _
            + ";Port=" + MySetting.MySqlPort _
            + ";User ID=" + MySetting.RobustUsername _
            + ";Password=" + MySetting.RobustPassword _
            + ";Old Guids=true;Allow Zero Datetime=true;" _
            + ";Connect Timeout=28800;Command Timeout=28800;" _
            + """"

            MySetting.SetOtherIni("DatabaseService", "ConnectionString", ConnectionString)
            MySetting.SetOtherIni("Const", "GridName", MySetting.SimName)
            MySetting.SetOtherIni("Const", "BaseURL", "http://" & MySetting.PublicIP)
            MySetting.SetOtherIni("Const", "PrivURL", "http://" & MySetting.PrivateURL)
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

            If MySetting.SearchLocal Then
                MySetting.SetOtherIni("LoginService", "SearchURL", "${Const|BaseURL}:" & MySetting.ApachePort & "/Search/query.php")
            Else
                MySetting.SetOtherIni("LoginService", "SearchURL", "http://www.hyperica.com/Search/query.php")
            End If

            MySetting.SetOtherIni("LoginService", "WelcomeMessage", MySetting.WelcomeMessage)

            MySetting.SaveOtherINI()

        End If

        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ' Opensim.ini
        MySetting.LoadOtherIni(GetOpensimProto(), ";")

        Select Case MySetting.ServerType
            Case "Robust"
                If MySetting.SearchLocal Then
                    MySetting.SetOtherIni("Search", "SearchURL", "${Const|BaseURL}:" & MySetting.ApachePort & "/Search/query.php")
                    MySetting.SetOtherIni("Search", "SimulatorFeatures", "${Const|BaseURL}:" & MySetting.ApachePort & "/Search/query.php")
                    MySetting.SetOtherIni("Search", "SimulatorFeatures", "${Const|BaseURL}:" & MySetting.ApachePort & "/Search/query.php")
                Else
                    MySetting.SetOtherIni("DataSnapshot", "data_services", "http://www.hyperica.com/Search/register.php")
                    MySetting.SetOtherIni("Search", "SearchURL", "http://www.hyperica.com/Search/query.php")
                    MySetting.SetOtherIni("Search", "SimulatorFeatures", "http://www.hyperica.com/Search/query.php")
                End If

                MySetting.SetOtherIni("Const", "PrivURL", "http://" & MySetting.PrivateURL)
                MySetting.SetOtherIni("Const", "GridName", MySetting.SimName)
            Case "Region"
            Case "OSGrid"
            Case "Metro"

        End Select

        '' all grids requires these setting in Opensim.ini
        MySetting.SetOtherIni("Const", "DiagnosticsPort", MySetting.DiagnosticPort)

        ' once and only once toggle to get Opensim 2.91
        If MySetting.DeleteScriptsOnStartupOnce() Then
            Dim Clr As New ClrCache()
            Clr.WipeScripts()
            Clr.WipeBakes()
            Clr.WipeAssets()
            Clr.WipeImage()
            Clr.WipeMesh()
            MySetting.SetOtherIni("XEngine", "DeleteScriptsOnStartup", "False")
        End If

        If MySetting.LSLHTTP Then
            ' do nothing - let them edit it
        Else
            MySetting.SetOtherIni("Network", "OutboundDisallowForUserScriptsExcept", MySetting.PrivateURL + "/32")
        End If

        MySetting.SetOtherIni("Network", "ExternalHostNameForLSL", MySetting.PublicIP)
        MySetting.SetOtherIni("DataSnapshot", "index_sims", "true")
        MySetting.SetOtherIni("PrimLimitsModule", "EnforcePrimLimits", CType(MySetting.Primlimits, String))

        If MySetting.Primlimits Then
            MySetting.SetOtherIni("Permissions", "permissionmodules", "DefaultPermissionsModule, PrimLimitsModule")
        Else
            MySetting.SetOtherIni("Permissions", "permissionmodules", "DefaultPermissionsModule")
        End If

        If MySetting.GloebitsEnable Then
            MySetting.SetOtherIni("Startup", "economymodule", "Gloebit")
        Else
            MySetting.SetOtherIni("Startup", "economymodule", "BetaGridLikeMoneyModule")
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

        If (MySetting.RegionOwnerIsGod Or MySetting.RegionManagerIsGod) Then
            MySetting.SetOtherIni("Permissions", "allow_grid_gods", "true")
        Else
            MySetting.SetOtherIni("Permissions", "allow_grid_gods", "false")
        End If

        If (MySetting.RegionOwnerIsGod) Then
            MySetting.SetOtherIni("Permissions", "region_owner_is_god", "true")
        Else
            MySetting.SetOtherIni("Permissions", "region_owner_is_god", "false")
        End If

        If (MySetting.RegionManagerIsGod) Then
            MySetting.SetOtherIni("Permissions", "region_manager_is_god", "true")
        Else
            MySetting.SetOtherIni("Permissions", "region_manager_is_god", "false")
        End If

        If (MySetting.AllowGridGods) Then
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
            Case "5"
                MySetting.SetOtherIni("Startup", "meshing", "Meshmerizer")
                MySetting.SetOtherIni("Startup", "physics", "ubODE")
                MySetting.SetOtherIni("Startup", "UseSeparatePhysicsThread", "false")
            Case Else
                MySetting.SetOtherIni("Startup", "meshing", "Meshmerizer")
                MySetting.SetOtherIni("Startup", "physics", "BulletSim")
                MySetting.SetOtherIni("Startup", "UseSeparatePhysicsThread", "true")
        End Select

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
            Log("Info", "Auto backup is On")
            MySetting.SetOtherIni("AutoBackupModule", "AutoBackup", "true")
        Else
            Log("Info", "Auto backup is Off")
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
        MySetting.SetOtherIni("VivoxVoice", "vivox_admin_user", MySetting.VivoxUserName)
        MySetting.SetOtherIni("VivoxVoice", "vivox_admin_password", MySetting.VivoxPassword)

        MySetting.SaveOtherINI()

        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ' Other Settings
        DoWifi()
        DoGloebits()
        CopyOpensimProto()
        DoRegions()
        MapSetup()
        DoPHP()
        DoApache()

        Return True

    End Function

    Public Sub DoGloebits()

        'Gloebits.ini
        MySetting.LoadOtherIni(GOpensimBinPath + "bin\Gloebit.ini", ";")
        If MySetting.GloebitsEnable Then

            MySetting.SetOtherIni("Gloebit", "Enabled", "true")
        Else
            MySetting.SetOtherIni("Gloebit", "Enabled", "false")
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

        MySetting.LoadOtherIni(GOpensimBinPath + "bin\Wifi.ini", ";")

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

        If MySetting.ServerType = "Robust" Then ' wifi could be on or off
            If (MySetting.WifiEnabled) Then
                MySetting.SetOtherIni("WifiService", "Enabled", "True")
            Else
                MySetting.SetOtherIni("WifiService", "Enabled", "False")
            End If
        Else ' it is always off
            ' shutdown wifi in Attached mode
            MySetting.SetOtherIni("WifiService", "Enabled", "False")
        End If

        MySetting.SetOtherIni("WifiService", "GridName", MySetting.SimName)
        MySetting.SetOtherIni("WifiService", "LoginURL", "http://" & MySetting.PublicIP & ":" & MySetting.HttpPort)
        MySetting.SetOtherIni("WifiService", "WebAddress", "http://" & MySetting.PublicIP & ":" & MySetting.HttpPort)

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

    Sub CopyOpensimProto(Optional name As String = "")

        If name.Length > 0 Then
            Dim X = RegionClass.FindRegionByName(name)
            If (X > -1) Then Opensimproto(X)
        Else
            ' COPY OPENSIM.INI prototype to all region folders and set the Sim Name
            For Each X As Integer In RegionClass.RegionNumbers
                Opensimproto(X)
            Next
        End If

    End Sub

    Sub Opensimproto(X As Integer)

        Dim regionName = RegionClass.RegionName(X)
        Dim pathname = RegionClass.IniPath(X)
        Diagnostics.Debug.Print(regionName)

        Try

            MySetting.LoadOtherIni(GetOpensimProto(), ";")

            MySetting.SetOtherIni("Const", "BaseHostname", MySetting.PublicIP)
            MySetting.SetOtherIni("Const", "PublicPort", MySetting.HttpPort) ' 8002
            MySetting.SetOtherIni("Const", "PrivURL", "http://" & MySetting.PrivateURL) ' local IP
            MySetting.SetOtherIni("Const", "http_listener_port", RegionClass.RegionPort(X).ToString) ' varies with region
            Dim name = RegionClass.RegionName(X)

            ' save the http listener port away for the group
            RegionClass.GroupPort(X) = RegionClass.RegionPort(X)

            MySetting.SetOtherIni("Const", "PrivatePort", MySetting.PrivatePort) '8003
            MySetting.SetOtherIni("Const", "RegionFolderName", RegionClass.GroupName(X))
            MySetting.SaveOtherINI()

            My.Computer.FileSystem.CopyFile(GetOpensimProto(), pathname + "Opensim.ini", True)

        Catch ex As Exception
            Print("Error: Failed to set the Opensim.ini for sim " + regionName + ":" + ex.Message)
            ErrorLog("Error: Failed to set the Opensim.ini for sim " + regionName + ":" + ex.Message)
        End Try

    End Sub

#End Region

#Region "Regions"

    Private Sub DoRegions()
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Regions - write all region.ini files with public IP and Public port

        Dim BirdFile = GOpensimBinPath + "bin\addon-modules\OpenSimBirds\config\OpenSimBirds.ini"

        System.IO.File.Delete(BirdFile)

        Dim TideFile = GOpensimBinPath + "bin\addon-modules\OpenSimTide\config\OpenSimTide.ini"

        System.IO.File.Delete(TideFile)

        ' has to be bound late so regions data is there.

        Dim fPort As String = MySetting.FirstRegionPort()
        If fPort.Length = 0 Then
            fPort = RegionClass.LowestPort().ToString
            MySetting.FirstRegionPort = fPort
            MySetting.SaveSettings()
        End If

        ' Self setting Region Ports
        Dim FirstPort As Integer = CType(MySetting.FirstRegionPort(), Integer)
        Dim BirdData As String = ""
        Dim TideData As String = ""

        For Each RegionNum As Integer In RegionClass.RegionNumbers

            Dim simName = RegionClass.RegionName(RegionNum)

            MySetting.LoadOtherIni(RegionClass.RegionPath(RegionNum), ";")
            MySetting.SetOtherIni(simName, "InternalPort", RegionClass.RegionPort(RegionNum).ToString)
            MySetting.SetOtherIni(simName, "ExternalHostName", MySetting.PublicIP)

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
            MySetting.LoadOtherIni(GOpensimBinPath + "bin\Regions\" + RegionClass.GroupName(RegionNum) + "\Opensim.ini", ";")

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
                Case "5"
                    MySetting.SetOtherIni("Startup", "meshing", "Meshmerizer")
                    MySetting.SetOtherIni("Startup", "physics", "ubODE")
                    MySetting.SetOtherIni("Startup", "UseSeparatePhysicsThread", "False")
                Case Else
                    ' do nothing
            End Select

            If RegionClass.RegionGod(RegionNum) = "True" Or RegionClass.ManagerGod(RegionNum) = "True" Then
                MySetting.SetOtherIni("Permissions", "allow_grid_gods", "True")
            Else
                MySetting.SetOtherIni("Permissions", "allow_grid_gods", MySetting.AllowGridGods.ToString)
            End If

            If RegionClass.RegionGod(RegionNum) = "True" Then
                MySetting.SetOtherIni("Permissions", "region_owner_is_god", "True")
            Else
                MySetting.SetOtherIni("Permissions", "region_owner_is_god", MySetting.RegionOwnerIsGod.ToString)
            End If

            If RegionClass.ManagerGod(RegionNum) = "True" Then
                MySetting.SetOtherIni("Permissions", "region_manager_is_god", "True")
            Else
                MySetting.SetOtherIni("Permissions", "region_manager_is_god", MySetting.RegionManagerIsGod.ToString)
            End If

            If RegionClass.AllowGods(RegionNum) = "True" Then
                MySetting.SetOtherIni("Permissions", "allow_grid_gods", "True")
            Else
                MySetting.SetOtherIni("Permissions", "allow_grid_gods", MySetting.AllowGridGods.ToString)
            End If

            MySetting.SaveOtherINI()

            If MySetting.BirdsModuleStartup And RegionClass.Birds(RegionNum) = "True" Then

                BirdData = BirdData & "[" + simName + "]" + vbCrLf &
            ";this Is the default And determines whether the module does anything" & vbCrLf &
            "BirdsModuleStartup = True" & vbCrLf & vbCrLf &
            ";set to false to disable the birds from appearing in this region" & vbCrLf &
            "BirdsEnabled = True" & vbCrLf & vbCrLf &
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

            End If

            If MySetting.TideEnabled And RegionClass.Tides(RegionNum) = "True" Then

                TideData = TideData & ";; Set the Tide settings per named region" & vbCrLf &
        "[" + simName + "]" + vbCrLf &
    ";this determines whether the module does anything in this region" & vbCrLf &
    ";# {TideEnabled} {} {Enable the tide to come in and out?} {true false} false" & vbCrLf &
    "TideEnabled = True" & vbCrLf &
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
            End If

        Next
        Diagnostics.Debug.Print(BirdFile)
        IO.File.WriteAllText(BirdFile, BirdData, Encoding.Default) 'The text file will be created if it does not already exist
        IO.File.WriteAllText(TideFile, TideData, Encoding.Default) 'The text file will be created if it does not already exist

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
        Or MySetting.HttpPort = MySetting.PrivatePort Then
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

    Private Sub ClearCachesToolStripMenuItem_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub ToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem2.Click
        Print("Starting UPnp Control Panel")
        Dim pi As ProcessStartInfo = New ProcessStartInfo With {
            .Arguments = "",
            .FileName = MyFolder & "\UPnpPortForwardManager.exe",
            .WindowStyle = ProcessWindowStyle.Normal
        }
        Dim ProcessUpnp As Process = New Process With {
            .StartInfo = pi
        }
        Try
            ProcessUpnp.Start()
        Catch ex As Exception
            ErrorLog("ErrorUPnp failed to launch: " + ex.Message)
        End Try
    End Sub

    Private Sub StopAllRegions()

        For Each X As Integer In RegionClass.RegionNumbers
            RegionClass.Status(X) = RegionMaker.SIMSTATUSENUM.Stopped
            RegionClass.ProcessID(X) = 0
            RegionClass.Timer(X) = RegionMaker.REGIONTIMER.Stopped
        Next

        ExitList.Clear()

    End Sub

    Public Sub ToolBar(visible As Boolean)

        Label3.Visible = visible
        Label4.Visible = visible
        AvatarLabel.Visible = visible
        PercentCPU.Visible = visible
        PercentRAM.Visible = visible

    End Sub

    Private Sub BusyButton_Click(sender As Object, e As EventArgs) Handles BusyButton.Click

        StopAllRegions()

        UpdateView = True ' make form refresh
        ' cannot load OAR or IAR, either
        IslandToolStripMenuItem.Visible = False
        ClothingInventoryToolStripMenuItem.Visible = False
        Timer1.Stop()
        OpensimIsRunning() = False
        Me.AllowDrop = False
        ProgressBar1.Value = 0
        ProgressBar1.Visible = False
        ToolBar(False)

        Print("Dreamgrid Stopped/Aborted")
        Buttons(StopButton)
        Timer1.Enabled = False
        GAborting = True

    End Sub

    Private Sub AdminUIToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ViewWebUI.Click
        If OpensimIsRunning() Then
            If MySetting.ApacheEnable Then
                Dim webAddress As String = "http://127.0.0.1:" + MySetting.ApachePort
                Process.Start(webAddress)
            Else
                Dim webAddress As String = "http://127.0.0.1:" + MySetting.HttpPort
                Process.Start(webAddress)
                Print("Log in as '" + MySetting.AdminFirst + " " + MySetting.AdminLast + "' with a password of " + MySetting.Password + " to add user accounts.")
            End If
        Else
            If MySetting.ApacheEnable Then
                Dim webAddress As String = "http://127.0.0.1:" + MySetting.ApachePort
                Process.Start(webAddress)
            Else
                Print("Opensim is not running. Cannot open the Web Interface.")
            End If
        End If

    End Sub

    Private Sub LoopBackToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoopBackToolStripMenuItem.Click

        Help("Loopback Fixes")

    End Sub

    Private Sub MoreContentToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MoreContentToolStripMenuItem.Click

        Dim webAddress As String = GDomain + "/cgi/freesculpts.plx"
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
        Dim webAddress As String = GDomain + "/Outworldz_Installer/PortForwarding.htm"
        Process.Start(webAddress)
    End Sub

#End Region

#Region "Apache"

    Public Sub StartApache()

        If Not MySetting.ApacheEnable Then
            Print("Apache web server is not enabled")
            Return
        End If

        Print("Setup Apache")
        ' Stop MSFT server if we are on port 80 and enabled

        ApacheProcess.StartInfo.UseShellExecute = True ' so we can redirect streams
        ApacheProcess.StartInfo.FileName = "net"
        ApacheProcess.StartInfo.CreateNoWindow = True
        ApacheProcess.StartInfo.Arguments = "stop W3SVC"
        ApacheProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
        ApacheProcess.Start()
        ApacheProcess.WaitForExit()

        Dim ApacheRunning = CheckPort(MySetting.PrivateURL, CType(MySetting.ApachePort, Integer))
        If ApacheRunning Then Return

        If MySetting.ApacheService Then

            Print("Checking Apache service")
            Try
                Dim ApacheProcess As New Process With {
                    .EnableRaisingEvents = True
                }
                ApacheProcess.StartInfo.UseShellExecute = True ' so we can redirect streams
                ApacheProcess.StartInfo.FileName = MyFolder + "\Outworldzfiles\Apache\bin\InstallApache.bat"
                ApacheProcess.StartInfo.CreateNoWindow = True
                ApacheProcess.StartInfo.WorkingDirectory = MyFolder + "\Outworldzfiles\Apache\bin\"
                ApacheProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
                ApacheProcess.Start()
                ApacheProcess.WaitForExit()
                Dim code = ApacheProcess.ExitCode
                If code <> 0 Then
                    MsgBox("Apache failed to install and start as a service:" & code.ToString, vbInformation, "Error")
                End If
                Sleep(100)
                ApacheRunning = CheckPort(MySetting.PublicIP, CType(MySetting.ApachePort, Integer))
                If Not ApacheRunning Then
                    MsgBox("Apache installed but port " & MySetting.ApachePort & " is not responding. Check your firewall and router port forward settings.", vbInformation, "Error")
                End If
            Catch ex As Exception
                Print("Error InstallApache did Not start " + ex.Message)
            End Try
        Else

            ' Start Apache  manually
            Try
                Dim ApacheProcess2 As New Process With {
                    .EnableRaisingEvents = True
                }
                ApacheProcess2.StartInfo.UseShellExecute = True ' so we can redirect streams
                ApacheProcess2.StartInfo.FileName = MyFolder + "\Outworldzfiles\Apache\bin\httpd.exe"
                ApacheProcess2.StartInfo.CreateNoWindow = True
                ApacheProcess2.StartInfo.WorkingDirectory = MyFolder + "\Outworldzfiles\Apache\bin\"
                ApacheProcess2.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
                ApacheProcess2.StartInfo.Arguments = ""
                ApacheProcess2.Start()
                gApacheProcessID = ApacheProcess2.Id
            Catch ex As Exception
                Print("Error: Apache did not start: " + ex.Message)
                ErrorLog("Error: Apache did not start: " + ex.Message)
                Return
            End Try

            ' Wait for Apache to start listening

            Dim counter = 0
            While Not IsApacheRunning() And OpensimIsRunning
                Application.DoEvents()
                BumpProgress(1)
                counter += 1
                ' wait a minute for it to start
                If counter > 100 Then
                    Print("Error:Apache failed to start")
                    Return
                End If
                Application.DoEvents()
                Sleep(100)
            End While

            Print("Apache webserver is running")

        End If

    End Sub

    ''' <summary>
    ''' Check is Apache  port 80 or 8000 is up
    ''' </summary>
    ''' <returns>boolean</returns>
    Private Function IsApacheRunning() As Boolean

        Dim Up As String
        Try
            Up = client.DownloadString("http://" & MySetting.PublicIP & ":" & MySetting.ApachePort + "/?_Opensim=" + Random())
        Catch ex As Exception
            If ex.Message.Contains("200 OK") Then Return True
            Return False
        End Try
        If Up.Length = 0 And OpensimIsRunning() Then
            Return False
        End If

        Return True

    End Function

    Private Sub MapSetup()

        Dim phptext = "<?php" & vbCrLf &
"/* General Domain */" & vbCrLf &
"$CONF_domain        = " & """" & MySetting.PublicIP & """" & "; " & vbCrLf &
"$CONF_port          = " & """" & MySetting.HttpPort & """" & "; " & vbCrLf &
"$CONF_sim_domain    = " & """" & "http//" & MySetting.PublicIP & "/" & """" & ";" & vbCrLf &
"$CONF_install_path  = " & """" & "/Metromap" & """" & ";   // Installation path " & vbCrLf &
"/* MySQL Database */ " & vbCrLf &
"$CONF_db_server     = " & """" & MySetting.RobustServer & """" & "; // Address Of Robust Server " & vbCrLf &
"$CONF_db_user       = " & """" & MySetting.RobustUsername & """" & ";  // login " & vbCrLf &
"$CONF_db_pass       = " & """" & MySetting.RobustPassword & """" & ";  // password " & vbCrLf &
"$CONF_db_database   = " & """" & MySetting.RobustDataBaseName & """" & ";     // Name Of Robust Server " & vbCrLf &
"/* The Coordinates Of the Grid-Center */ " & vbCrLf &
"$CONF_center_coord_x = " & """" & MySetting.MapCenterX & """" & ";		// the Center-X-Coordinate " & vbCrLf &
"$CONF_center_coord_y = " & """" & MySetting.MapCenterY & """" & ";		// the Center-Y-Coordinate " & vbCrLf &
"// style-sheet items" & vbCrLf &
"$CONF_style_sheet     = " & """" & "/css/stylesheet.css" & """" & ";          //Link To your StyleSheet" & vbCrLf &
"?>"

        Using outputFile As New StreamWriter(MyFolder & "\OutworldzFiles\Apache\htdocs\MetroMap\includes\config.php", False)
            outputFile.WriteLine(phptext)
        End Using

    End Sub

    Private Sub KillApache()

        If Not MySetting.ApacheEnable Then Return

        If MySetting.ApacheService Then
            Dim ApacheProcess As New Process()
            Print("Stopping Apache ")
            Try
                ApacheProcess.StartInfo.FileName = "net.exe"
                ApacheProcess.StartInfo.Arguments = "stop ApacheHTTPServer"
                ApacheProcess.StartInfo.CreateNoWindow = True
                ApacheProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
                ApacheProcess.Start()
                ApacheProcess.WaitForExit()
                Dim code = ApacheProcess.ExitCode
                If code <> 0 Then
                    Log("Info", "No Apache to stop")
                End If
            Catch ex As Exception
                Print("Error Apache did not stop" + ex.Message)
            End Try
        Else
            Zap("httpd")
            Zap("rotatelogs")
        End If

    End Sub

    Private Sub StopApache()

        If Not MySetting.ApacheEnable Then Return

        Print("Stopping Apache ")

        If Not MySetting.ApacheService Then
            Zap("httpd")
            Zap("rotatelogs")
        End If

    End Sub

    Private Sub DoPHP()

        Dim ini = MyFolder & "\Outworldzfiles\PHP5\php.ini"
        MySetting.LoadApacheIni(ini)
        MySetting.SetApacheIni("extension_dir", " = """ & GCurSlashDir & "/OutworldzFiles/PHP5/ext""")
        MySetting.SaveApacheINI(ini, "php.ini")

    End Sub

    Private Sub DoApache()

        If Not MySetting.ApacheEnable Then Return

        ' lean rightward paths for Apache
        Dim ini = MyFolder & "\Outworldzfiles\Apache\conf\httpd.conf"
        MySetting.LoadApacheIni(ini)
        MySetting.SetApacheIni("Listen", MySetting.ApachePort)
        MySetting.SetApacheIni("ServerRoot", """" & GCurSlashDir & "/Outworldzfiles/Apache" & """")
        MySetting.SetApacheIni("DocumentRoot", """" & GCurSlashDir & "/Outworldzfiles/Apache/htdocs" & """")
        MySetting.SetApacheIni("Use VDir", """" & GCurSlashDir & "/Outworldzfiles/Apache/htdocs" & """")
        MySetting.SetApacheIni("PHPIniDir", """" & GCurSlashDir & "/Outworldzfiles/PHP5" & """")
        MySetting.SetApacheIni("ServerName", MySetting.PrivateURL)
        MySetting.SetApacheIni("ErrorLog", """|bin/rotatelogs.exe  -l \" & """" & GCurSlashDir & "/Outworldzfiles/Apache/logs/Error-%Y-%m-%d.log" & "\" & """" & " 86400""")
        MySetting.SetApacheIni("CustomLog", """|bin/rotatelogs.exe -l \" & """" & GCurSlashDir & "/Outworldzfiles/Apache/logs/access-%Y-%m-%d.log" & "\" & """" & " 86400""" & " common env=!dontlog""")
        MySetting.SetApacheIni("LoadModule php5_module", """" & GCurSlashDir & "/Outworldzfiles/PHP5/php5apache2_4.dll" & """")
        MySetting.SaveApacheINI(ini, "httpd.conf")

        ' lean rightward paths for Apache
        ini = MyFolder & "\Outworldzfiles\Apache\conf\extra\httpd-ssl.conf"
        MySetting.LoadApacheIni(ini)
        MySetting.SetApacheIni("Listen", MySetting.PrivateURL & ":" & "443")
        MySetting.SetApacheIni("extension_dir", """" & GCurSlashDir & "/OutworldzFiles/PHP5/ext""")
        MySetting.SetApacheIni("DocumentRoot", """" & GCurSlashDir & "/Outworldzfiles/Apache/htdocs""")
        MySetting.SetApacheIni("ServerName", MySetting.PublicIP)
        MySetting.SetApacheIni("SSLSessionCache", "shmcb:""" & GCurSlashDir & "/Outworldzfiles/Apache/logs" & "/ssl_scache(512000)""")
        MySetting.SaveApacheINI(ini, "httpd-ssl.conf")

    End Sub

#End Region

#Region "Icecast"

    Public Sub StartIcecast()

        If Not MySetting.SCEnable Then
            Return
        End If

        Dim IceCastRunning = CheckPort(MySetting.PublicIP, MySetting.SCPortBase)
        If IceCastRunning Then Return

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

            If MySetting.SCShow Then
                IcecastProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal
            Else
                IcecastProcess.StartInfo.WindowStyle = ProcessWindowStyle.Minimized
            End If

            'IcecastProcess.StartInfo.Arguments = "-c .\icecast_run.xml"
            IcecastProcess.Start()
            gIcecastProcID = IcecastProcess.Id

            SetWindowTextCall(IcecastProcess, "Icecast")
            ShowDOSWindow(IcecastProcess.MainWindowHandle, SHOWWINDOWENUM.SWMINIMIZE)
        Catch ex As Exception
            Print("Error: Icecast did not start: " + ex.Message)
            ErrorLog("Error: Icecast did not start: " + ex.Message)
        End Try

    End Sub

#End Region

#Region "Robust"

    Public Function StartRobust() As Boolean

        If IsRobustRunning() Then
            Return True
        End If

        If MySetting.ServerType <> "Robust" Then Return True

        Print("Setup Robust")
        If MySetting.RobustServer <> "127.0.0.1" And MySetting.RobustServer <> "localhost" Then
            Print("Using Robust on " & MySetting.RobustServer)
            Return True
        End If

        GRobustProcID = Nothing
        Print("Starting Robust")

        Try
            RobustProcess.EnableRaisingEvents = True
            RobustProcess.StartInfo.UseShellExecute = True ' so we can redirect streams
            RobustProcess.StartInfo.FileName = GOpensimBinPath + "bin\robust.exe"

            RobustProcess.StartInfo.CreateNoWindow = False
            RobustProcess.StartInfo.WorkingDirectory = GOpensimBinPath + "bin"
            RobustProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal
            RobustProcess.StartInfo.Arguments = "-inifile Robust.HG.ini"
            RobustProcess.Start()
            GRobustProcID = RobustProcess.Id

            SetWindowTextCall(RobustProcess, "Robust")
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
            counter += 1
            ' wait a minute for it to start
            If counter > 100 Then
                Print("Error:Robust failed to start")
                Buttons(StartButton)
                Dim yesno = MsgBox("Robust did not start. Do you want to see the log file?", vbYesNo, "Error")
                If (yesno = vbYes) Then
                    Dim Log As String = """" + GOpensimBinPath + "bin\Robust.log" + """"
                    System.Diagnostics.Process.Start(MyFolder + "\baretail.exe " & Log)
                End If
                Buttons(StartButton)
                Return False
            End If
            Application.DoEvents()
            Sleep(100)

        End While

        If MySetting.ConsoleShow = False Then
            ShowDOSWindow(GetHwnd("Robust"), SHOWWINDOWENUM.SWMINIMIZE)
        End If

        Log("Info", "Robust is running")

        Return True

    End Function

#End Region

#Region "Opensimulator"

    Public Function StartOpensimulator() As Boolean

        gExitHandlerIsBusy = False
        GAborting = False
        Timer1.Start() 'Timer starts functioning
        StartRobust()

        Dim Len = RegionClass.RegionCount()
        Dim counter = 1
        ProgressBar1.Value = CType(counter / Len, Integer)
        Try
            ' Boot them up
            For Each x In RegionClass.RegionNumbers()
                If RegionClass.RegionEnabled(x) Then
                    Boot(RegionClass.RegionName(x))
                    ProgressBar1.Value = CType(counter / Len * 100, Integer)
                    counter += 1
                End If
            Next
        Catch ex As Exception
            Diagnostics.Debug.Print(ex.Message)
            Print("Unable to boot some regions")
        End Try

        MySetting.DeleteScriptsOnStartupOnce() = False
        MySetting.SaveSettings()

        Return True

    End Function

#End Region

#Region "Exited"

    ' Handle Exited event and display process information.
    Private Sub ApacheProcess_Exited(ByVal sender As Object, ByVal e As System.EventArgs) Handles ApacheProcess.Exited

        gApacheProcessID = Nothing

        If GAborting Then Return
        Dim yesno = MsgBox("Apache exited.", vbYesNo, "Error")

    End Sub

    ' Handle Exited event and display process information.
    Private Sub RobustProcess_Exited(ByVal sender As Object, ByVal e As System.EventArgs) Handles RobustProcess.Exited

        GRobustProcID = Nothing

        If GAborting Then Return
        Dim yesno = MsgBox("Robust exited. Do you want to see the error log file?", vbYesNo, "Error")
        If (yesno = vbYes) Then
            Dim MysqlLog As String = GOpensimBinPath + "bin\Robust.log"
            System.Diagnostics.Process.Start(MyFolder + "\baretail.exe", """" & MysqlLog & """")
        End If

    End Sub

    Private Sub Mysql_Exited(ByVal sender As Object, ByVal e As System.EventArgs) Handles ProcessMySql.Exited

        If GAborting Then Return

        OpensimIsRunning() = False

        Dim yesno = MsgBox("Mysql exited. Do you want to see the error log file?", vbYesNo, "Error")
        If (yesno = vbYes) Then
            Dim MysqlLog As String = MyFolder + "\OutworldzFiles\mysql\data"
            Dim files() As String
            files = Directory.GetFiles(MysqlLog, "*.err", SearchOption.TopDirectoryOnly)
            For Each FileName As String In files
                System.Diagnostics.Process.Start(MyFolder + "\baretail.exe", """" & FileName & """")
            Next
        End If
    End Sub

    Private Sub IceCast_Exited(ByVal sender As Object, ByVal e As System.EventArgs) Handles IcecastProcess.Exited

        If GAborting Then Return
        Dim yesno = MsgBox("Icecast quit. Do you want to see the error log file?", vbYesNo, "Error")
        If (yesno = vbYes) Then
            Dim IceCastLog As String = MyFolder + "\Outworldzfiles\Icecast\log\error.log"
            System.Diagnostics.Process.Start(MyFolder + "\baretail.exe", """" & IceCastLog & """")
        End If

    End Sub

#End Region

#Region "ExitHandlers"

    Private Sub ExitHandlerPoll()

        ' 10 Second ticker
        If gExitHandlerIsBusy Then Return
        If GAborting Then Return

        Dim GroupName As String
        Dim RegionNumber As Integer
        Dim TimerValue As Integer

        For Each X As Integer In RegionClass.RegionNumbers
            'Application.DoEvents()
            ' count up to auto restart , when high enough, restart the sim
            If RegionClass.Timer(X) >= 0 Then
                RegionClass.Timer(X) = RegionClass.Timer(X) + 1
            End If

            If OpensimIsRunning() And Not GAborting And RegionClass.Timer(X) >= 0 Then
                TimerValue = RegionClass.Timer(X)
                ' if it is past time and no one is in the sim...
                GroupName = RegionClass.GroupName(X)
                If (TimerValue / 6) >= (MySetting.AutoRestartInterval()) And MySetting.AutoRestartInterval() > 0 And Not AvatarsIsInGroup(GroupName) Then
                    ' shut down the group when one minute has gone by, or multiple thereof.
                    Try
                        If ShowDOSWindow(GetHwnd(GroupName), SHOWWINDOWENUM.SWRESTORE) Then
                            SequentialPause()
                            ConsoleCommand(RegionClass.GroupName(X), "q{ENTER}" + vbCrLf)
                            Print("AutoRestarting " + GroupName)
                            ' shut down all regions in the DOS box
                            For Each Y In RegionClass.RegionListByGroupNum(GroupName)
                                RegionClass.Timer(Y) = RegionMaker.REGIONTIMER.Stopped
                                RegionClass.Status(Y) = RegionMaker.SIMSTATUSENUM.RecyclingDown
                            Next
                        Else
                            ' shut down all regions in the DOS box
                            For Each Y In RegionClass.RegionListByGroupNum(GroupName)
                                RegionClass.Timer(Y) = RegionMaker.REGIONTIMER.Stopped
                                RegionClass.Status(Y) = RegionMaker.SIMSTATUSENUM.Stopped
                            Next
                        End If
                        UpdateView = True ' make form refresh
                    Catch ex As Exception
                        ErrorLog(ex.Message)
                        ' shut down all regions in the DOS box
                        For Each Y In RegionClass.RegionListByGroupNum(GroupName)
                            RegionClass.Timer(Y) = RegionMaker.REGIONTIMER.Stopped
                            RegionClass.Status(Y) = RegionMaker.SIMSTATUSENUM.RecyclingDown
                        Next
                    End Try
                End If

            End If

            ' if a restart is signalled, boot it up
            If RegionClass.Status(X) = RegionMaker.SIMSTATUSENUM.RestartPending And Not GAborting Then
                Boot(RegionClass.RegionName(X))
            End If

        Next

        GRestartNow = False
        If ExitList.Count = 0 Then Return
        If gExitHandlerIsBusy Then Return
        gExitHandlerIsBusy = True

        Dim RegionName = ExitList(0).ToString
        Try
            ExitList.RemoveAt(0)
        Catch
            Log("Error", "This should not happen as exitlist was not zero")
        End Try

        Print(RegionName & " shutdown")
        Dim RegionList = RegionClass.RegionListByGroupNum(RegionName)
        ' Need a region number and a Name
        ' name is either a region or a Group. For groups we need to get a region name from the group
        GroupName = RegionName ' assume a group
        RegionNumber = RegionClass.FindRegionByName(RegionName)

        If RegionNumber >= 0 Then
            GroupName = RegionClass.GroupName(RegionNumber) ' Yup, Get Name of the Dos box
        Else
            ' Nope, grab the first region, Group name is already set
            RegionNumber = RegionList(0)
        End If

        Dim Status = RegionClass.Status(RegionNumber)
        TimerValue = RegionClass.Timer(RegionNumber)

        'Auto restart phase begins
        If OpensimIsRunning() And Status = RegionMaker.SIMSTATUSENUM.RecyclingDown Then
            Print("Restart Queued for " + GroupName)
            For Each R In RegionList
                RegionClass.Status(R) = RegionMaker.SIMSTATUSENUM.RestartPending
            Next
            UpdateView = True ' make form refresh
        End If
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ' Maybe we crashed during warmup or runniung.  Skip prompt if auto restart on crash and restart the beast
        If (Status = RegionMaker.SIMSTATUSENUM.RecyclingUp _
            Or Status = RegionMaker.SIMSTATUSENUM.Booting) _
            Or RegionClass.IsBooted(RegionNumber) _
            And TimerValue >= 0 Then

            If MySetting.RestartOnCrash Then
                ' shut down all regions in the DOS box
                Print("DOS Box " + GroupName + " quit unexpectedly.  Restarting now...")
                For Each Y In RegionClass.RegionListByGroupNum(GroupName)
                    RegionClass.Timer(Y) = RegionMaker.REGIONTIMER.Stopped
                    RegionClass.Status(Y) = RegionMaker.SIMSTATUSENUM.RestartPending
                Next
            Else
                Dim yesno = MsgBox("DOS Box " + GroupName + " quit unexpectedly. Do you want to see the log file?", vbYesNo, "Error")
                If (yesno = vbYes) Then
                    System.Diagnostics.Process.Start(MyFolder + "\baretail.exe", """" & RegionClass.IniPath(RegionNumber) + "Opensim.log" & """")
                End If
                StopGroup(GroupName)
            End If

        End If

        gExitHandlerIsBusy = False

    End Sub

    Public Sub StopGroup(Groupname As String)

        For Each RegionNumber In RegionClass.RegionListByGroupNum(Groupname)
            ' Called by a sim restart, do not change status 
            'If Not RegionClass.Status(RegionNumber) = RegionMaker.SIMSTATUSENUM.RecyclingDown Then
            RegionClass.Status(RegionNumber) = RegionMaker.SIMSTATUSENUM.Stopped
            Log("Info", RegionClass.RegionName(RegionNumber) + " Stopped")
            'End If
            RegionClass.Timer(RegionNumber) = RegionMaker.REGIONTIMER.Stopped
        Next
        Log("Info", Groupname + " Group is now stopped")
        UpdateView = True ' make form refresh

    End Sub

    Public Sub ForceStopGroup(Groupname As String)

        For Each RegionNumber In RegionClass.RegionListByGroupNum(Groupname)

            ' Called by a sim restart, do not change status
            'If Not RegionClass.Status(RegionNumber) = RegionMaker.SIMSTATUSENUM.RecyclingDown Then
            RegionClass.Status(RegionNumber) = RegionMaker.SIMSTATUSENUM.Stopped
            Log("Info", RegionClass.RegionName(RegionNumber) + " Stopped")
            ' End If

            RegionClass.Timer(RegionNumber) = RegionMaker.REGIONTIMER.Stopped
        Next
        Log("Info", Groupname + " Group is now stopped")
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

        If GAborting Then Return True

        OpensimIsRunning() = True

        Buttons(StopButton)

        Log("Info", "Region: Starting Region " + BootName)

        Dim RegionNumber = RegionClass.FindRegionByName(BootName)
        If RegionClass.IsBooted(RegionNumber) Then
            Log("Info", "Region " + BootName + " skipped as it is already Booted")
            Return True
        End If

        If RegionClass.Status(RegionNumber) = RegionMaker.SIMSTATUSENUM.RecyclingUp Then
            Log("Info", "Region " + BootName + " skipped as it is already Warming Up")
            Return True
        End If

        If RegionClass.Status(RegionNumber) = RegionMaker.SIMSTATUSENUM.Booting Then
            Log("Info", "Region " + BootName + " skipped as it is already Booted Up")
            Return True
        End If

        If RegionClass.Status(RegionNumber) = RegionMaker.SIMSTATUSENUM.ShuttingDown Then
            Log("Info", "Region " + BootName + " skipped as it is already Shutting Down")
            Return True
        End If

        If RegionClass.Status(RegionNumber) = RegionMaker.SIMSTATUSENUM.RecyclingDown Then
            Log("Info", "Region " + BootName + " skipped as it is already Recycling Down")
            Return True
        End If

        Application.DoEvents()
        Dim isRegionRunning = CheckPort("127.0.0.1", RegionClass.GroupPort(RegionNumber))
        If isRegionRunning Then
            Log("Info", "Region " + BootName + " failed to start as it is already running")
            RegionClass.Status(RegionNumber) = RegionMaker.SIMSTATUSENUM.Booted ' force it up
            Return False
        End If

        Environment.SetEnvironmentVariable("OSIM_LOGPATH", GOpensimBinPath + "bin\Regions\" + RegionClass.GroupName(RegionNumber))

        Dim myProcess As Process = GetNewProcess()
        Dim Groupname = RegionClass.GroupName(RegionNumber)
        Print("Starting " + Groupname)
        Try
            myProcess.EnableRaisingEvents = True
            myProcess.StartInfo.UseShellExecute = True ' so we can redirect streams
            myProcess.StartInfo.WorkingDirectory = GOpensimBinPath + "bin"

            myProcess.StartInfo.FileName = """" + GOpensimBinPath + "bin\OpenSim.exe" + """"
            myProcess.StartInfo.CreateNoWindow = False
            myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal
            myProcess.StartInfo.Arguments = " -inidirectory=" & """" & "./Regions/" & RegionClass.GroupName(RegionNumber) + """"

            Try
                My.Computer.FileSystem.DeleteFile(GOpensimBinPath + "bin\Regions\" & RegionClass.GroupName(RegionNumber) & "\Opensim.log")
            Catch
            End Try

            Try
                My.Computer.FileSystem.DeleteFile(GOpensimBinPath + "bin\Regions\" & RegionClass.GroupName(RegionNumber) & "\PID.pid")
            Catch
            End Try

            Try
                My.Computer.FileSystem.DeleteFile(GOpensimBinPath + "bin\regions\" & RegionClass.GroupName(RegionNumber) & "\OpensimConsole.log")
            Catch ex As Exception
            End Try

            Try
                My.Computer.FileSystem.DeleteFile(GOpensimBinPath + "bin\regions\" & RegionClass.GroupName(RegionNumber) & "\OpenSimStats.log")
            Catch ex As Exception
            End Try

            SequentialPause()

            If myProcess.Start() Then
                For Each num In RegionClass.RegionListByGroupNum(Groupname)
                    Log("Debug", "Process started for " + RegionClass.RegionName(num) + " PID=" + myProcess.Id.ToString + " Num:" + num.ToString)
                    RegionClass.Status(num) = RegionMaker.SIMSTATUSENUM.Booting
                    RegionClass.ProcessID(num) = myProcess.Id
                    RegionClass.Timer(num) = RegionMaker.REGIONTIMER.StartCounting
                Next

                UpdateView = True ' make form refresh
                Application.DoEvents()
                SetWindowTextCall(myProcess, RegionClass.GroupName(RegionNumber))

                Log("Debug", "Created Process Number " + myProcess.Id.ToString + " in  RegionHandles(" + RegionHandles.Count.ToString + ") " + "Group:" + Groupname)
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
                System.Diagnostics.Process.Start(MyFolder + "\baretail.exe", """" & RegionClass.IniPath(RegionNumber) + "Opensim.log" & """")
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

        Dim Up As String
        Try
            Up = client.DownloadString("http://" & MySetting.RobustServer & ":" & MySetting.HttpPort + "/?_Opensim=" + Random())
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
        MyFolder + "\OutworldzFiles\PHPLog.log",
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
    Public Sub Log(category As String, message As String)
        Logger(category, message, "Outworldz")
    End Sub

    Public Sub ErrorLog(message As String)
        Logger("Error", message, "Error")
    End Sub

    Sub Logger(category As String, message As String, file As String)
        Try
            Using outputFile As New StreamWriter(MyFolder & "\OutworldzFiles\" + file + ".log", True)
                outputFile.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + category & ":" & message)
                Diagnostics.Debug.Print(message)
            End Using
        Catch
        End Try
    End Sub

    ''' <summary>
    ''' Shows the log buttons if diags fail
    ''' </summary>
    Private Sub ShowLog()

        System.Diagnostics.Process.Start(MyFolder + "\baretail.exe", """" + MyFolder + "/OutworldzFiles/Outworldz.log" + """")

    End Sub

#End Region

#Region "Subs"

    ''' <summary>
    ''' SetWindowTextCall is here to wrap the SetWindowtext API call.  This call fails when there is no
    ''' hwnd as Windows takes its sweet time to get that. Also, may fail to write the title. It has a  timer to make sure we do not get stuck
    ''' </summary>
    ''' <param name="hwnd">Handle to the window to change the text on</param>
    ''' <param name="windowName">the name of the Window </param>
    '''
    Public Function SetWindowTextCall(myProcess As Process, windowName As String) As Boolean

        Dim WindowCounter As Integer = 0
        While myProcess.MainWindowHandle = CType(0, IntPtr)
            Diagnostics.Debug.Print(windowName & " Handle = 0")
            Sleep(100)
            WindowCounter += 1
            If WindowCounter > 200 Then '  20 seconds for process to start
                ErrorLog("Cannot get MainWindowHandle for " & windowName)
                Return False
            End If
        End While

        WindowCounter = 0

        Dim hwnd As IntPtr = myProcess.MainWindowHandle
        If CType(hwnd, Integer) = 0 Then
            ErrorLog("hwnd = 0")
        End If
        Dim status = False
        While status = False
            Sleep(100)
            SetWindowText(hwnd, windowName)
            status = SetWindowText(hwnd, windowName)
            WindowCounter += 1
            If WindowCounter > 600 Then '  60 seconds
                ErrorLog("Cannot get handle for " & windowName)
                Exit While
            End If
            Application.DoEvents()
        End While
        Return True

    End Function

    Public Function GetHwnd(Groupname As String) As IntPtr

        If Groupname = "Robust" Then
            Return RobustProcess.MainWindowHandle
        End If

        Dim Regionlist = RegionClass.RegionListByGroupNum(Groupname)

        For Each X In Regionlist
            Dim pid = RegionClass.ProcessID(X)

            Dim ctr = 20   ' 2 seconds
            Dim found As Boolean = False
            While Not found And ctr > 0
                Sleep(100)

                For Each pList As Process In Process.GetProcesses()
                    If pList.Id = pid Then
                        Return pList.MainWindowHandle
                    End If
                    Application.DoEvents()
                    ctr -= 1
                Next
            End While
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

        If name <> "Robust" Then
            Dim ID = RegionClass.FindRegionByName(name)
            Dim PID = RegionClass.ProcessID(ID)
            Try
                If PID > 0 Then ShowDOSWindow(Process.GetProcessById(PID).MainWindowHandle, SHOWWINDOWENUM.SWRESTORE)
            Catch ex As Exception
                Diagnostics.Debug.Print("Catch:" & ex.Message)
            End Try
        Else
            Try
                ShowDOSWindow(Process.GetProcessById(GRobustProcID).MainWindowHandle, SHOWWINDOWENUM.SWRESTORE)
            Catch ex As Exception
                Diagnostics.Debug.Print("Catch:" & ex.Message)
            End Try
        End If

        Try
            'plus sign(+), caret(^), percent sign (%), tilde (~), And parentheses ()
            command = command.Replace("+", "{+}")
            command = command.Replace("^", "{^}")
            command = command.Replace("%", "{%}")
            command = command.Replace("(", "{(}")
            command = command.Replace(")", "{)}")

            AppActivate(name)
            SendKeys.SendWait(SendableKeys("{ENTER}" + vbCrLf))
            SendKeys.SendWait(SendableKeys(command))
        Catch ex As Exception
            ' ErrorLog("Error:" + ex.Message)
            Diagnostics.Debug.Print("Cannot find window " + name)
            'RegionClass.RegionDump()
            Me.Focus()
            Return False
        End Try
        Me.Focus()
        'Application.DoEvents()
        Return True

    End Function

    ''' <summary>
    ''' Sleep(ms)
    ''' </summary>
    ''' <param name="value">millseconds</param>
    Shared Sub Sleep(value As Integer)

        ' value is in milliseconds, but we do it in 10 passes so we can doevents() to free up console
        Dim sleeptime = value / 10  ' now in tenths
        Dim counter = 10
        While counter > 0
            Application.DoEvents()
            Thread.Sleep(CType(sleeptime, Integer))
            counter -= 1
        End While

    End Sub

    'Private Sub Chart1_Customize(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChartWrapper1.TheChart.Customize

    '   ChartWrapper1.ChartAreas(0).AxisY.CustomLabels.RemoveAt(2) 'Will remove the third label

    'End Sub

    Private Sub Chart()
        ' Graph https://github.com/sinairv/MSChartWrapper
        Try
            '  running average
            speed3 = speed2
            speed2 = speed1
            speed1 = speed
            speed = cpu.NextValue()

            Dim newspeed As Single = (speed + speed1 + speed2 + speed3) / 4

            MyCPUCollection.Add(newspeed)
            PercentCPU.Text = String.Format("{0: 0}% CPU", newspeed)
            MyCPUCollection.Remove(1) ' drop 1st, older  item
        Catch ex As Exception
            ErrorLog(ex.Message)
        End Try

        'reverse series
        Dim series(180) As Double
        Dim j = 180
        Dim k = 1
        While j > 0
            series(k) = CType(MyCPUCollection(j), Double)
            j -= 1
            k += 1
        End While

        ChartWrapper1.ClearChart()
        ChartWrapper1.AddLinePlot("CPU", series)

        'RAM

        Dim ramseries(180) As Double
        Dim wql As ObjectQuery = New ObjectQuery("SELECT TotalVisibleMemorySize,FreePhysicalMemory FROM Win32_OperatingSystem")
        Dim searcher As ManagementObjectSearcher = New ManagementObjectSearcher(wql)
        Dim results As ManagementObjectCollection = searcher.Get()
        Try
            For Each result In results
                Dim value = ((result("TotalVisibleMemorySize") - result("FreePhysicalMemory")) / result("TotalVisibleMemorySize")) * 100
                MyRAMCollection.Add(value)
                PercentRAM.Text = CType(value, Integer).ToString()
            Next
        Catch ex As Exception
            Log("Error", ex.Message)
        End Try

        MyRAMCollection.Remove(1) ' drop 1st, older  item

        j = 180
        k = 1
        While j > 0
            ramseries(k) = CType(MyRAMCollection(j), Double)
            j -= 1
            k += 1
        End While

        ChartWrapper2.ClearChart()
        ChartWrapper2.AddLinePlot("RAM", ramseries)

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

        Chart() ' do charts collection each second

        If Not OpensimIsRunning() Then
            Timer1.Stop()
            Return
        End If
        gDNSSTimer += 1

        ' hourly
        If gDNSSTimer Mod 3600 = 0 Or gDNSSTimer = 1 Then
            RegisterDNS()
            GetEvents() ' get the events from the Outworldz main server for all grids
        End If

        If GAborting Then Return

        ' 10 seconds check for a restart
        ' RegionRestart requires this MOD 10 as it changed there to one minute
        If gDNSSTimer Mod 10 = 0 Then
            RegionClass.CheckPost()
            ScanAgents() ' update agent count
            ExitHandlerPoll() ' see if any regions have exited and set it up for Region Restart
        End If

        If gDNSSTimer Mod 60 = 0 Then
            RegionListHTML() ' create HTML for region teleporters
            LogSearch.Find()
        End If

        If gDNSSTimer Mod 300 = 0 Then
            RunDataSnapshot() ' Fetch assets marked for search every 5 minutes
        End If

    End Sub

    '' makes a list of teleports for the prims to use
    Private Sub RegionListHTML()

        'http://localhost:8002/bin/data/teleports.htm
        'Outworldz|Welcome||www.outworldz.com:9000:Welcome|128,128,96|
        '*|Welcome||www.outworldz.com9000Welcome|128,128,96|
        Dim HTML As String
        Dim HTMLFILE = GOpensimBinPath & "bin\data\teleports.htm"
        HTML = "Welcome to |" + MySetting.SimName + "||" + MySetting.PublicIP + ":" + MySetting.HttpPort + ":" + MySetting.WelcomeRegion + "||" + vbCrLf
        Dim ToSort As New List(Of String)
        For Each Regionnumber As Integer In RegionClass.RegionNumbers
            If RegionClass.IsBooted(Regionnumber) And RegionClass.Teleport(Regionnumber) = "True" Then
                ToSort.Add(RegionClass.RegionName(Regionnumber))
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
    ''' quiery MySQL to find any avatars in the DOS bos so we can stop it, or not
    ''' </summary>
    ''' <param name="groupname"></param>
    ''' <returns></returns>
    Private Function AvatarsIsInGroup(groupname As String) As Boolean

        Dim present As Integer = 0
        For Each RegionNum As Integer In RegionClass.RegionListByGroupNum(groupname)
            present += RegionClass.AvatarCount(RegionNum)
        Next

        Return CType(present, Boolean)

    End Function

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

            If RegionClass.IsBooted(RegionNumber) Then
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
            ' Set filter options and filter index.
            Dim openFileDialog1 As OpenFileDialog = New OpenFileDialog With {
                .InitialDirectory = BackupPath(),
                .Filter = "Opensim OAR(*.OAR,*.GZ,*.TGZ)|*.oar;*.gz;*.tgz;*.OAR;*.GZ;*.TGZ|All Files (*.*)|*.*",
                .FilterIndex = 1,
                .Multiselect = False
            }

            ' Call the ShowDialog method to show the dialogbox.
            Dim UserClickedOK As DialogResult = openFileDialog1.ShowDialog

            ' Process input if the user clicked OK.
            If UserClickedOK = DialogResult.OK Then

                Dim offset = VarChooser(chosen)
                If offset.Length = 0 Then Return

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

                        Dim ForceParcel As String = ""
                        If GForceParcel Then ForceParcel = " --force-parcels "
                        Dim ForceTerrain As String = ""
                        If GForceTerrain Then ForceTerrain = " --force-terrain "
                        Dim ForceMerge As String = ""
                        If GForceMerge Then ForceMerge = " --merge "
                        Dim UserName As String = ""
                        If GUserName.Length > 0 Then UserName = " --default-user " & """" & GUserName & """" & " "

                        ConsoleCommand(RegionClass.GroupName(Y), "load oar " & UserName & ForceMerge & ForceTerrain & ForceParcel & offset & """" & thing & """" & "{ENTER}" & vbCrLf)
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
            BackupPath = GCurSlashDir + "/OutworldzFiles/AutoBackup/"
            If Not Directory.Exists(BackupPath) Then
                MkDir(BackupPath)
            End If
        Else
            BackupPath = MySetting.BackupFolder + "/"
            BackupPath = BackupPath.Replace("\", "/")    ' because Opensim uses unix-like slashes, that's why

            If Not Directory.Exists(BackupPath) Then

                BackupPath = GCurSlashDir + "/OutworldzFiles/Autobackup/"

                If Not Directory.Exists(BackupPath) Then
                    MkDir(BackupPath)
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
            If RegionClass.IsBooted(RegionNumber) Then
                Dim Group = RegionClass.GroupName(RegionNumber)
                For Each Y In RegionClass.RegionListByGroupNum(Group)
                    If Not L.Contains(RegionClass.RegionName(Y)) Then
                        ConsoleCommand(RegionClass.GroupName(RegionNumber), "change region " + """" + RegionClass.RegionName(Y) + """" + "{ENTER}" + vbCrLf)
                        ConsoleCommand(RegionClass.GroupName(RegionNumber), "save oar  " + """" + BackupPath() + RegionClass.RegionName(Y) + "_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".oar" + """" + "{ENTER}" + vbCrLf)
                        L.Add(RegionClass.RegionName(Y))
                    End If
                Next
            End If
            n += 1
        Next

    End Sub

    Private Sub LoadInventoryIARToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoadInventoryIARToolStripMenuItem.Click

        If OpensimIsRunning() Then
            ' Create an instance of the open file dialog box.
            ' Set filter options and filter index.
            Dim openFileDialog1 As OpenFileDialog = New OpenFileDialog With {
                .InitialDirectory = """" + MyFolder + "/" + """",
                .Filter = "Inventory IAR (*.iar)|*.iar|All Files (*.*)|*.*",
                .FilterIndex = 1,
                .Multiselect = False
            }

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

            Dim SaveIAR As New FormIARSave
            SaveIAR.ShowDialog()
            Dim chosen = SaveIAR.DialogResult()
            If chosen = DialogResult.OK Then

                Dim itemName = SaveIAR.GObject
                If itemName.Length = 0 Then
                    MsgBox("Must have an object to save")
                    Return
                End If

                Dim ToBackup As String

                Dim BackupName = SaveIAR.GBackupName
                If Not BackupName.ToLower.EndsWith(".iar") Then
                    BackupName += ".iar"
                End If

                If String.IsNullOrEmpty(SaveIAR.GBackupPath) Or SaveIAR.GBackupPath = "AutoBackup" Then
                    ToBackup = BackupPath() & "" & BackupName
                Else
                    ToBackup = SaveIAR.GBackupPath & BackupName
                End If

                Dim Name = SaveIAR.GAvatarName

                Dim Password = SaveIAR.GPassword

                Dim flag As Boolean = False
                For Each RegionNumber As Integer In RegionClass.RegionNumbers
                    Dim GName = RegionClass.GroupName(RegionNumber)
                    Dim RNUm = RegionClass.FindRegionByName(GName)
                    If RegionClass.IsBooted(RegionNumber) And Not flag Then
                        ConsoleCommand(RegionClass.GroupName(RegionNumber), "save iar " _
                                   & Name & " " _
                                   & """" & itemName & """" _
                                   & " " & """" & Password & """" & " " _
                                   & """" + ToBackup + """" _
                                   & "{ENTER}" + vbCrLf
                                  )
                        flag = True
                        Print("Saving " + BackupName + " to " + BackupPath())
                    End If
                Next
            End If

            SaveIAR.Dispose()
        Else
            Print("Opensim Is not running. Cannot make an IAR now.")
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
        If size = 256 Then  ' 1x1
            Dim VarForm As New FormDisplacement1X1 ' form for choosing a  region in  a var
            ' Show testDialog as a modal dialog and determine if DialogResult = OK.
            VarForm.Init(RegionNumber)
            VarForm.ShowDialog()
        ElseIf size = 512 Then  ' 2x2
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

        Return GSelectedBox

    End Function

    Private Function LoadOARContent(thing As String) As Boolean

        If Not OpensimIsRunning() Then
            Print("Opensim has to be started to load an OAR file.")
            Return False
        End If

        Dim region = ChooseRegion(True)
        If region.Length = 0 Then Return False

        Dim offset = VarChooser(region)
        If offset.Length = 0 Then Return False

        Dim backMeUp = MsgBox("Make a backup first?", vbYesNo, "Backup?")
        Dim num = RegionClass.FindRegionByName(region)
        If num < 0 Then
            MsgBox("Cannot find region")
            Return False
        End If
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
                    If GForceParcel Then ForceParcel = " --force-parcels "
                    Dim ForceTerrain As String = ""
                    If GForceTerrain Then ForceTerrain = " --force-terrain "
                    Dim ForceMerge As String = ""
                    If GForceMerge Then ForceMerge = " --merge "
                    Dim UserName As String = ""
                    If GUserName.Length > 0 Then UserName = " --default-user " & """" & GUserName & """" & " "

                    ConsoleCommand(RegionClass.GroupName(Y), "load oar " & UserName & ForceMerge & ForceTerrain & ForceParcel & offset & """" & thing & """" & "{ENTER}" & vbCrLf)
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
            If RegionClass.IsBooted(RegionNum) Then
                num = RegionNum
            End If
        Next
        If num = -1 Then
            MsgBox("No regions are ready, so cannot load the IAR", vbInformation, "Info")
            Return False
        End If

        Dim Path As String = InputBox("Folder to save  Inventory (""/"", ""/Objects"", ""/Objects/Somefolder..."")", "Folder Name", "/Objects")

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

    Private Sub TextBox1_DragDrop(sender As System.Object, e As System.Windows.Forms.DragEventArgs) Handles TextBox1.DragEnter

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

    Private Sub PictureBox1_DragEnter(sender As System.Object, e As System.Windows.Forms.DragEventArgs)

        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        End If

    End Sub

    Private Sub SetIAROARContent()

        IslandToolStripMenuItem.Visible = False
        ClothingInventoryToolStripMenuItem.Visible = False

        Print("Refreshing OAR Downloadable Content")
        Dim oars As String = ""
        Try
            oars = client.DownloadString(GDomain + "/Outworldz_Installer/Content.plx?type=OAR&r=" + Random())
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
                Log("Info", "" + line)
                Dim OarMenu As New ToolStripMenuItem With {
                    .Text = line,
                    .ToolTipText = "Click to load this content",
                    .DisplayStyle = ToolStripItemDisplayStyle.Text
                }
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

            If aline.EndsWith(".rtf") Then
                aline = System.IO.Path.GetFileNameWithoutExtension(aline)
                Dim HelpMenu As New ToolStripMenuItem With {
                    .Text = aline,
                    .ToolTipText = "Click to load this content",
                    .DisplayStyle = ToolStripItemDisplayStyle.Text,
                    .Image = My.Resources.question_and_answer
                }
                AddHandler HelpMenu.Click, New EventHandler(AddressOf HelpClick)
                HelpOnSettingsToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {HelpMenu})
            End If

        Next

        Log("Info", "OARS loaded")
        Print("Refreshing IAR Inventory Content")
        Dim iars As String = ""
        Try
            iars = client.DownloadString(GDomain + "/Outworldz_Installer/Content.plx?type=IAR&r=" + Random())
        Catch ex As Exception
            ErrorLog("Info:No IARS, dang, something is wrong with the Internet :-(")
            Return
        End Try

        Dim iarreader = New System.IO.StringReader(iars)
        ContentSeen = False
        While Not ContentSeen
            line = iarreader.ReadLine()
            If line <> Nothing Then
                Log("Info", "" + line)
                Dim IarMenu As New ToolStripMenuItem With {
                    .Text = line,
                    .ToolTipText = "Click to load this content the next time the simulator is started",
                    .DisplayStyle = ToolStripItemDisplayStyle.Text
                }
                AddHandler IarMenu.Click, New EventHandler(AddressOf IarClick)
                ClothingInventoryToolStripMenuItem.Visible = True
                ClothingInventoryToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {IarMenu})
                gContentAvailable = True
            Else
                ContentSeen = True
            End If
        End While

        Log("Info", " IARS loaded")

        BumpProgress10()
        AddLog("All Logs")
        AddLog("Robust")
        AddLog("Outworldz")
        AddLog("UPnP")
        AddLog("Icecast")
        AddLog("MySQL")
        AddLog("All Settings")
        AddLog("--- Regions ---")
        For Each X In RegionClass.RegionNumbers
            Dim Name = RegionClass.RegionName(X)
            AddLog("Region " & Name)
        Next

        BumpProgress10()

    End Sub

    ''' <summary>
    ''' Upload in a seperate thraed the photo, if any.  Cannot be called unless main web server is known to be online.
    ''' </summary>
    Private Sub UploadPhoto()

        If System.IO.File.Exists(MyFolder & "\OutworldzFiles\Photo.png") Then
            Dim Myupload As New UploadImage
            Myupload.PostContentUploadFile()
        End If

    End Sub

    Private Sub AddLog(name As String)
        Dim LogMenu As New ToolStripMenuItem With {
            .Text = name,
            .ToolTipText = "Click to view this log",
            .Size = New System.Drawing.Size(269, 26),
            .Image = My.Resources.Resources.document_view,
            .DisplayStyle = ToolStripItemDisplayStyle.Text
        }
        AddHandler LogMenu.Click, New EventHandler(AddressOf LogViewClick)
        ViewLogsToolStripMenuItem.Visible = True
        ViewLogsToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {LogMenu})

    End Sub

    Private Sub OarClick(sender As Object, e As EventArgs)

        Dim File As String = Mid(CType(sender.text, String), 1, InStr(CType(sender.text, String), "|") - 2)
        File = GDomain + "/Outworldz_Installer/OAR/" + File 'make a real URL
        LoadOARContent(File)
        sender.checked = True

    End Sub

    Private Sub IarClick(sender As Object, e As EventArgs)

        Dim file As String = Mid(CType(sender.text, String), 1, InStr(CType(sender.text, String), "|") - 2)
        file = GDomain + "/Outworldz_Installer/IAR/" + file 'make a real URL
        If LoadIARContent(file) Then
            Print("Opensimulator will load " + file + ".  This may take time to load.")
        End If
        sender.checked = True

    End Sub

#End Region

#Region "Updates"

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
        Print("Finished with backup at " + Dest)
        Return True

    End Function

    Private Sub CHeckForUpdatesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CHeckForUpdatesToolStripMenuItem.Click

        CheckForUpdates()

    End Sub

    Private Sub UpdaterGo()

        Dim msg = MsgBox("Make a backup of important files and the database first? ", vbYesNo)
        Dim okay As Boolean
        If msg = vbYes Then
            okay = MakeBackup()
        End If

        KillApache()
        StopMysql()

        Dim fileloaded As String = Download()
        If fileloaded.Length > 0 Then
            Dim pUpdate As Process = New Process()
            Dim pi As ProcessStartInfo = New ProcessStartInfo With {
                .Arguments = "",
                .FileName = """" + MyFolder + "\" + fileloaded + """"
            }
            pUpdate.StartInfo = pi
            Try
                Print("I'll see you again when I wake up all fresh and new!")
                Log("Info", "Launch Updater and exiting")
                pUpdate.Start()
            Catch ex As Exception
                ErrorLog("Error: Could not launch " + fileloaded + ". Perhaps you can can exit this program and launch it manually.")
            End Try
            End ' program
        Else
            Print("Uh Oh!  The files I need could not be found online. The gnomes have absconded with them! Please check later.")
        End If

    End Sub

    Private Function Download() As String

        Dim fileName As String = "UpdateGrid.exe"

        Try
            My.Computer.FileSystem.DeleteFile(MyFolder + "\" + fileName)
        Catch
            Log("Error", " Could Not delete " + MyFolder + "\" + fileName)
        End Try

        Try
            fileName = client.DownloadString(GDomain + "/Outworldz_Installer/GetUpdaterGrid.plx?fill=1" + GetPostData())
        Catch
            MsgBox("Could Not fetch an update. Please Try again, later", vbInformation, "Info")
            Return ""
        End Try

        Try
            Dim myWebClient As New WebClient()
            Print("Downloading New updater, this will take a moment")
            ' The DownloadFile() method downloads the Web resource and saves it into the current file-system folder.
            myWebClient.DownloadFile(GDomain + "/Outworldz_Installer/" + fileName, fileName)
        Catch e As Exception
            MsgBox("Could Not fetch an update. Please Try again, later", vbInformation, "Info")
            Log("Warn", e.Message)
            Return ""
        End Try
        Return fileName

    End Function

    Sub CheckForUpdates()

        Print("Checking for Updates")
        Dim Update As String = ""

        Try
            Update = client.DownloadString(GDomain + "/Outworldz_Installer/UpdateGrid.plx?fill=1" + GetPostData())
        Catch ex As Exception
            ErrorLog("Dang:The Outworldz web site is down")
        End Try
        If (Update.Length = 0) Then Update = "0"

        Try
            If Convert.ToSingle(Update) > Convert.ToSingle(gMyVersion) Then
                If MsgBox("A dreamier version " + Update + " is available. Update Now?", vbYesNo) = vbYes Then UpdaterGo()
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
            Return False
        End Try

        If ClientSocket.Connected Then
            Log("Info", " port probe success on port " + Port.ToString)
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

            Log("Info", "Public IP=" + MySetting.PublicIP)
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
            Log("Info", "Public IP=" + "127.0.0.1")
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
        Dim loopbacktest As String = "http://" + MySetting.PublicIP + ":" + MySetting.HttpPort + "/?_TestLoopback=" + Random()
        Try
            Log("Info", loopbacktest)
            result = client.DownloadString(loopbacktest)
        Catch ex As Exception
            ErrorLog("Err:Loopback fail:" + result + ":" + ex.Message)
            Return False
        End Try

        BumpProgress10()

        If MySetting.PublicIP = MyUPnpMap.LocalIP() Then Return False

        If result = "Test Completed" Then
            Log("Info", "Passed:" + result)
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

        If Not OpensimIsRunning() Then
            Print("Cannot run diagnostics unless Opensimulator is running. Click 'Start' and try again.")
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

            Dim Url = GDomain + "/cgi/probetest.plx?IP=" + MySetting.PublicIP + "&Port=" + MySetting.HttpPort + GetPostData()
            Log("Info", Url)
            isPortOpen = client.DownloadString(Url)
        Catch ex As Exception
            ErrorLog("Dang:The Outworldz web site cannot find a path back")
            MySetting.DiagFailed = True
        End Try

        BumpProgress10()

        If isPortOpen = "yes" Then
            MySetting.PublicIP = MySetting.PublicIP
            Print("Public IP set to " + MySetting.PublicIP)
            MySetting.SaveSettings()
            Return True
        Else
            Log("Warn", "Failed:" + isPortOpen)
            MySetting.DiagFailed = True
            Print("Internet address " + MySetting.PublicIP + ":" + MySetting.HttpPort + " appears to not be forwarded to this machine in your router, so Hypergrid is not available. This can possibly be fixed by 'Port Forwards' in your router.  See Help->Port Forwards.")
#Disable Warning BC42025 ' Access of shared member, constant member, enum member or nested type through an instance
            MySetting.PublicIP = MyUPnpMap.LocalIP() ' failed, so try the machine address
#Enable Warning BC42025 ' Access of shared member, constant member, enum member or nested type through an instance
            MySetting.SaveSettings()
            Log("Info", "IP set to " + MySetting.PublicIP)
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

        ProbePublicPort()
        TestPublicLoopback()
        If MySetting.DiagFailed Then
            Dim answer = MsgBox("Diagnostics failed. Do you want to see the log?", vbYesNo)
            If answer = vbOK Then ShowLog()
        Else
            NewDNSName()
        End If
        Log("Info", "Diagnostics set the Grid address to " + MySetting.PublicIP)

    End Sub

    Private Sub CheckDiagPort()

        gUseIcons = True
        Print("Check Diagnostics port")
        Dim wsstarted = CheckPort("127.0.0.1", CType(MySetting.DiagnosticPort, Integer))
        If wsstarted = False Then
            MsgBox("Diagnostics port " + MySetting.DiagnosticPort + " is not working, As Dreamgrid is not running at a high enough security level,  or blocked by firewall or anti virus, so region icons are disabled.", vbInformation, "There is a problem")
            gUseIcons = False
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
            Log("UPnP", "UPnP is not working in the router")
            MySetting.UPnPEnabled = False
            MySetting.SaveSettings()
            Return False
        End If

        If Not MySetting.UPnPEnabled Then
            Return False
        End If

        Log("UPnP", "Local IP seems to be " + MyUPnpMap.LocalIP)

        Try

            If MySetting.SCEnable Then
                'Icecast 8080
                If MyUPnpMap.Exists(Convert.ToInt16(MySetting.SCPortBase), UPnp.Protocol.TCP) Then
                    MyUPnpMap.Remove(Convert.ToInt16(MySetting.SCPortBase), UPnp.Protocol.TCP)
                End If
                MyUPnpMap.Add(MyUPnpMap.LocalIP, CType(MySetting.SCPortBase, Integer), UPnp.Protocol.TCP, "Icecast TCP Public " + MySetting.SCPortBase.ToString)
                Print("Icecast Port is set to " + MySetting.SCPortBase.ToString)
                BumpProgress10()
            End If

            'diagnostics 8001
            If MyUPnpMap.Exists(Convert.ToInt16(MySetting.DiagnosticPort), UPnp.Protocol.TCP) Then
                MyUPnpMap.Remove(Convert.ToInt16(MySetting.DiagnosticPort), UPnp.Protocol.TCP)
            End If
            MyUPnpMap.Add(MyUPnpMap.LocalIP, Convert.ToInt16(MySetting.DiagnosticPort), UPnp.Protocol.TCP, "Opensim TCP Public " + MySetting.DiagnosticPort)
            Print("Diagnostic Port is set to " + MySetting.DiagnosticPort)
            BumpProgress10()

            ' 8002 for TCP and UDP
            If MyUPnpMap.Exists(Convert.ToInt16(MySetting.HttpPort), UPnp.Protocol.TCP) Then
                MyUPnpMap.Remove(Convert.ToInt16(MySetting.HttpPort), UPnp.Protocol.TCP)
            End If
            MyUPnpMap.Add(MyUPnpMap.LocalIP, Convert.ToInt16(MySetting.HttpPort), UPnp.Protocol.TCP, "Opensim TCP Grid " + MySetting.HttpPort)
            Print("Grid Port is set to " + MySetting.HttpPort)
            BumpProgress10()

            If MyUPnpMap.Exists(Convert.ToInt16(MySetting.HttpPort), UPnp.Protocol.UDP) Then
                MyUPnpMap.Remove(Convert.ToInt16(MySetting.HttpPort), UPnp.Protocol.UDP)
            End If
            MyUPnpMap.Add(MyUPnpMap.LocalIP, Convert.ToInt16(MySetting.HttpPort), UPnp.Protocol.UDP, "Opensim UDP Grid " + MySetting.HttpPort)
            Print("Grid Port is set to " + MySetting.HttpPort)
            BumpProgress10()

            For Each X In RegionClass.RegionNumbers
                Dim R As Integer = RegionClass.RegionPort(X)

                If MyUPnpMap.Exists(R, UPnp.Protocol.UDP) Then
                    MyUPnpMap.Remove(R, UPnp.Protocol.UDP)
                End If
                MyUPnpMap.Add(MyUPnpMap.LocalIP, R, UPnp.Protocol.UDP, "Opensim UDP Region " & RegionClass.RegionName(X) & " ")
                Print("Region " + RegionClass.RegionName(X) + " is set to " + Convert.ToString(R))
                BumpProgress(1)

                If MyUPnpMap.Exists(R, UPnp.Protocol.TCP) Then
                    MyUPnpMap.Remove(R, UPnp.Protocol.TCP)
                End If
                MyUPnpMap.Add(MyUPnpMap.LocalIP, R, UPnp.Protocol.TCP, "Opensim TCP Region " & RegionClass.RegionName(X) & " ")
                Print("Region " + RegionClass.RegionName(X) + " is set to " + Convert.ToString(R))
                BumpProgress(1)
            Next

            If MyUPnpMap.Exists(Convert.ToInt16(MySetting.SCPortBase), UPnp.Protocol.TCP) Then
                MyUPnpMap.Remove(Convert.ToInt16(MySetting.SCPortBase), UPnp.Protocol.TCP)
            End If
            MyUPnpMap.Add(MyUPnpMap.LocalIP, MySetting.SCPortBase, UPnp.Protocol.TCP, "Icecast TCP" + MySetting.SCPortBase.ToString)
            Print("Icecast Port is set to " + MySetting.SCPortBase.ToString)

            BumpProgress10()
        Catch e As Exception
            Log("UPnP", "UPnP Exception caught:  " + e.Message)
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
        If MySetting.DNSName.Length = 0 Then
            m = ""
        End If

        Dim data As String = "&MachineID=" + m _
        & "&FriendlyName=" & MySetting.SimName _
        & "&V=" & gMyVersion.ToString _
        & "&OV=" & gSimVersion.ToString _
        & "&uPnp=" & UPnp.ToString _
        & "&Loop=" & Loopb.ToString _
        & "&Type=" & Grid.ToString _
        & "&Ver=" & gMyVersion.ToString _
        & "&isPublic=" & MySetting.GDPR().ToString _
        & "&r=" & Random()
        Return data

    End Function

    Private Function OpenPorts() As Boolean

        Print("Check Router Ports")
        Try
            If OpenRouterPorts() Then ' open UPnp port
                Log("Info", "UPnP: Ok")
                MySetting.UPnpDiag = True
                MySetting.SaveSettings()
                BumpProgress10()
                Return True
            Else
                Log("UPnP", "Fail or disabled")
                MySetting.UPnpDiag = False
                MySetting.SaveSettings()
                BumpProgress10()
                Return False
            End If
        Catch e As Exception
            Log("Error", " UPnP Exception: " + e.Message)
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
            ToolBar(False)
            Buttons(StartButton)
            Print("Stopped")
            Return
        End If

        Dim pi As ProcessStartInfo = New ProcessStartInfo()

        ChDir(MyFolder & "\OutworldzFiles\mysql\bin")
        pi.WindowStyle = ProcessWindowStyle.Normal
        pi.Arguments = MySetting.MySqlPort

        pi.FileName = "CheckAndRepair.bat"
        Dim pMySqlDiag1 As Process = New Process With {
            .StartInfo = pi
        }
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
            ToolBar(False)
            Buttons(StartButton)
            Print("Stopped")
            Return
        End If

        ' Create an instance of the open file dialog box.
        ' Set filter options and filter index.
        Dim openFileDialog1 As OpenFileDialog = New OpenFileDialog With {
            .InitialDirectory = BackupPath(),
            .Filter = "BackupFile (*.sql)|*.sql|All Files (*.*)|*.*",
            .FilterIndex = 1,
            .Multiselect = False
        }

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
                    ' pi.Arguments = thing
                    Dim pi As ProcessStartInfo = New ProcessStartInfo With {
                        .WindowStyle = ProcessWindowStyle.Normal,
                        .WorkingDirectory = MyFolder & "\OutworldzFiles\mysql\bin\",
                        .FileName = MyFolder & "\OutworldzFiles\mysql\bin\RestoreMysql.bat"
                    }
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
            ToolBar(False)
            Buttons(StartButton)
            Print("Stopped")
            Return
        End If

        Print("Starting a slow but extensive Database Backup => Autobackup folder")
        Dim pMySqlBackup As Process = New Process()
        Dim pi As ProcessStartInfo = New ProcessStartInfo With {
            .Arguments = "",
            .WindowStyle = ProcessWindowStyle.Normal,
            .WorkingDirectory = MyFolder & "\OutworldzFiles\mysql\bin\",
            .FileName = MyFolder & "\OutworldzFiles\mysql\bin\BackupMysql.bat"
        }
        pMySqlBackup.StartInfo = pi

        pMySqlBackup.Start()

        Print("")
    End Sub

    Public Function StartMySQL() As Boolean

        ' Check for MySql operation
        If GRobustConnStr.Length = 0 Then

            GRobustConnStr = "server=" + MySetting.RobustServer() _
            + ";database=" + MySetting.RobustDataBaseName _
            + ";port=" + MySetting.MySqlPort _
            + ";user=" + MySetting.RobustUsername _
            + ";password=" + MySetting.RobustPassword _
            + ";Old Guids=true;Allow Zero Datetime=true;"

            MysqlConn = New MysqlInterface(GRobustConnStr)

        End If

        Dim isMySqlRunning = CheckPort(MySetting.RobustServer(), CType(MySetting.MySqlPort, Integer))

        If isMySqlRunning Then Return True
        ' Start MySql in background.

        BumpProgress10()
        Dim StartValue = ProgressBar1.Value
        Print("Starting MySql Database")

        ' SAVE INI file
        MySetting.LoadOtherIni(MyFolder & "\OutworldzFiles\mysql\my.ini", "#")
        MySetting.SetOtherIni("mysqld", "basedir", """" + GCurSlashDir + "/OutworldzFiles/Mysql" + """")
        MySetting.SetOtherIni("mysqld", "datadir", """" + GCurSlashDir + "/OutworldzFiles/Mysql/Data" + """")
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
                                 + "mysqld.exe --defaults-file=" + """" + GCurSlashDir + "/OutworldzFiles/mysql/my.ini" + """")
            End Using
        Catch ex As Exception
            ErrorLog("Error:Cannot write:" + ex.Message)
        End Try

        CreateService()
        CreateStopMySql()

        BumpProgress(5)

        ' Mysql was not running, so lets start it up.
        Dim pi As ProcessStartInfo = New ProcessStartInfo With {
            .Arguments = "--defaults-file=" + """" + GCurSlashDir + "/OutworldzFiles/mysql/my.ini" + """",
            .WindowStyle = ProcessWindowStyle.Hidden,
            .FileName = """" + MyFolder & "\OutworldzFiles\mysql\bin\mysqld.exe" + """"
        }
        ProcessMySql.StartInfo = pi
        ProcessMySql.EnableRaisingEvents = True
        ProcessMySql.Start()

        ' wait for MySql to come up
        Dim MysqlOk As Boolean
        While Not MysqlOk And OpensimIsRunning And Not GAborting

            BumpProgress(1)
            Application.DoEvents()

            Dim MysqlLog As String = MyFolder + "\OutworldzFiles\mysql\data"
            If ProgressBar1.Value = 100 Then ' about 30 seconds when it fails

                Dim yesno = MsgBox("The database did not start. Do you want to see the log file?", vbYesNo, "Error")
                If (yesno = vbYes) Then
                    Dim files() As String
                    files = Directory.GetFiles(MysqlLog, "*.err", SearchOption.TopDirectoryOnly)
                    For Each FileName As String In files
                        System.Diagnostics.Process.Start(MyFolder + "\baretail.exe", """" & FileName & """")
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
            "mysqld.exe --install Mysql --defaults-file=" + """" + GCurSlashDir + "/OutworldzFiles/mysql/my.ini" + """" + vbCrLf + "net start Mysql" + vbCrLf)
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
            Log("Info", "MySQL was not running")
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

        Dim isMySqlRunning = CheckPort("127.0.0.1", CType(MySetting.MySqlPort, Integer))
        If Not isMySqlRunning Then Return

        If Not gStopMysql Then
            Print("MySQL was running when I woke up, so I am leaving MySQL on.")
            Return
        End If

        Print("Stopping MySql")

        Try
            MysqlConn.Dispose()
        Catch
        End Try

        Dim p As Process = New Process()
        Dim pi As ProcessStartInfo = New ProcessStartInfo With {
            .Arguments = "--port " + MySetting.MySqlPort + " -u root shutdown",
            .FileName = """" + MyFolder + "\OutworldzFiles\mysql\bin\mysqladmin.exe" + """",
            .UseShellExecute = True, ' so we can redirect streams and minimize
            .WindowStyle = ProcessWindowStyle.Hidden
        }
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

        If MySetting.ServerType <> "Robust" Then
            Return True
        End If

        If MySetting.DNSName.Length = 0 Then
            Return True
        End If

        If IsPrivateIP(MySetting.DNSName) Then
            Return True
        End If

        'Print("Checking " + "http://" + MySetting.DNSName + ":" + MySetting.HttpPort)

        Dim client As New System.Net.WebClient
        Dim Checkname As String

        Try
            Application.DoEvents()
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
        If MySetting.ServerType <> "Robust" Then
            Return name
        End If

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

        If MySetting.DNSName.Length = 0 And MySetting.EnableHypergrid Then
            Dim newname = GetNewDnsName()
            If newname.Length >= 0 Then
                If RegisterName(newname).Length >= 0 Then
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

            Dim Menu As New ToolStripMenuItem With {
                .Text = RegionClass.RegionName(RegionNum),
                .ToolTipText = "Click to view stats on " + RegionClass.RegionName(RegionNum),
                .DisplayStyle = ToolStripItemDisplayStyle.Text
            }
            If RegionClass.IsBooted(RegionNum) Then
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

    Private Sub ShowRegionform()

        If RegionList.InstanceExists = False Then
            RegionForm = New RegionList
            RegionForm.Show()
            RegionForm.Activate()
        Else
            RegionForm.Show()
            RegionForm.Activate()
        End If

    End Sub

    Private Sub RegionsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RegionsToolStripMenuItem.Click

        ShowRegionform()

    End Sub

    Private Sub ScanAgents()
        ' Scan all the regions
        Dim sbttl As Integer = 0
        Try
            For Each RegionNum As Integer In RegionClass.RegionNumbers
                If RegionClass.IsBooted(RegionNum) Then
                    Dim count As Integer = MysqlConn.IsUserPresent(RegionClass.UUID(RegionNum))
                    sbttl += count
                    If count > 0 Then
                        Diagnostics.Debug.Print("Avatar in region " & RegionClass.RegionName(RegionNum))
                    End If
                    RegionClass.AvatarCount(RegionNum) = count
                    'Debug.Print(RegionClass.AvatarCount(X).ToString + " avatars in region " + RegionClass.RegionName(X))
                Else
                    RegionClass.AvatarCount(RegionNum) = 0
                End If
            Next
        Catch
        End Try

        AvatarLabel.Text = sbttl.ToString
    End Sub

#End Region

#Region "Alerts"

    Private Sub SendMsg(msg As String)

        If Not OpensimIsRunning() Then Print("Opensim is not running")

        For Each Regionnumber As Integer In RegionClass.RegionNumbers
            If RegionClass.IsBooted(Regionnumber) Then
                ConsoleCommand(RegionClass.GroupName(Regionnumber), "set log level " + msg + "{ENTER}" + vbCrLf)
            End If
        Next
        ConsoleCommand("Robust", "set log level " + msg + "{ENTER}" + vbCrLf)

    End Sub

    Private Sub AllToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles All.Click
        SendMsg("all")
    End Sub

    Private Sub Info_Click(sender As Object, e As EventArgs) Handles Info.Click
        SendMsg("info")
    End Sub

    Private Sub Debug_Click(sender As Object, e As EventArgs) Handles Debug.Click
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
        If OpensimIsRunning() And MySetting.SCEnable Then
            Dim webAddress As String = "http://" + MySetting.PublicIP + ":" + MySetting.SCPortBase.ToString
            Print("Icecast lets you stream music into your sim. The Music URL is " + webAddress + "/stream")
            Process.Start(webAddress)
        ElseIf MySetting.SCEnable = False Then
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
                    HowManyAreOnline += 1
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
        ConsoleCommand("Robust", "create user{ENTER}")
    End Sub

    Private Sub ChangePasswordToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ChangePasswordToolStripMenuItem.Click
        ConsoleCommand("Robust", "reset user password{ENTER}")
    End Sub

    Private Sub ShowUserDetailsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowUserDetailsToolStripMenuItem.Click
        Dim person = InputBox("Enter the first and last name of the user:")
        If person.Length > 0 Then
            ConsoleCommand("Robust", "show account " + person + "{ENTER}")
        End If
    End Sub

#End Region

#Region "LocalOARIAR"

    Private Sub LoadLocalIAROAR()
        ''' <summary>
        ''' Loads OAR and IAR from the menu
        ''' </summary>
        ''' <remarks>Handles both the IAR/OAR and Autobackup folders</remarks>

        Dim MaxFileNum As Integer = 10
        Dim counter = MaxFileNum
        Dim Filename = MyFolder + "\OutworldzFiles\OAR\"
        Dim OARs As Array = Directory.GetFiles(Filename, "*.OAR", SearchOption.TopDirectoryOnly)

        For Each OAR As String In OARs
            counter -= 1
            If counter > 0 Then
                Dim Name = Path.GetFileName(OAR)
                Dim OarMenu As New ToolStripMenuItem With {
                    .Text = Name,
                    .ToolTipText = "Click to load this content",
                    .DisplayStyle = ToolStripItemDisplayStyle.Text
                }
                AddHandler OarMenu.Click, New EventHandler(AddressOf LocalOarClick)
                LoadLocalOARSToolStripMenuItem.Visible = True
                LoadLocalOARSToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {OarMenu})
                Log("Info", "Set OAR " & Name)
            End If

        Next

        If MySetting.BackupFolder = "AutoBackup" Then
            Filename = MyFolder + "\OutworldzFiles\AutoBackup\"
        Else
            Filename = MySetting.BackupFolder
        End If

        Log("Info", "Auto OAR")
        Try
            Dim AutoOARs As Array = Directory.GetFiles(Filename, "*.OAR", SearchOption.TopDirectoryOnly)
            counter = MaxFileNum

            For Each OAR As String In AutoOARs
                counter -= 1
                If counter > 0 Then
                    Dim Name = Path.GetFileName(OAR)
                    Dim OarMenu As New ToolStripMenuItem With {
                        .Text = Name,
                        .ToolTipText = "Click to load this content",
                        .DisplayStyle = ToolStripItemDisplayStyle.Text
                    }
                    AddHandler OarMenu.Click, New EventHandler(AddressOf BackupOarClick)
                    LoadLocalOARSToolStripMenuItem.Visible = True
                    LoadLocalOARSToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {OarMenu})
                    Log("Info", Name)
                End If

            Next
        Catch
        End Try
        ' now for the IARs

        Log("Info", "Local IAR")
        Filename = MyFolder + "\OutworldzFiles\IAR\"
        Dim IARs As Array = Directory.GetFiles(Filename, "*.IAR", SearchOption.TopDirectoryOnly)
        counter = MaxFileNum
        For Each IAR As String In IARs
            counter -= 1
            If counter > 0 Then
                Dim Name = Path.GetFileName(IAR)
                Dim IarMenu As New ToolStripMenuItem With {
                    .Text = Name,
                    .ToolTipText = "Click to load this content",
                    .DisplayStyle = ToolStripItemDisplayStyle.Text
                }
                AddHandler IarMenu.Click, New EventHandler(AddressOf LocalIarClick)
                LoadLocalIARsToolStripMenuItem.Visible = True
                LoadLocalIARsToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {IarMenu})
                Log("Info", Name)
            End If

        Next

        If MySetting.BackupFolder = "AutoBackup" Then
            Filename = MyFolder + "\OutworldzFiles\AutoBackup\"
        Else
            Filename = MySetting.BackupFolder
        End If

        Try
            Log("Info", "Auto IAR")
            Dim AutoIARs As Array = Directory.GetFiles(Filename, "*.IAR", SearchOption.TopDirectoryOnly)
            counter = MaxFileNum
            For Each IAR As String In AutoIARs
                counter -= 1
                If counter > 0 Then
                    Dim Name = Path.GetFileName(IAR)
                    Dim IarMenu As New ToolStripMenuItem With {
                        .Text = Name,
                        .ToolTipText = "Click to load this content",
                        .DisplayStyle = ToolStripItemDisplayStyle.Text
                    }
                    AddHandler IarMenu.Click, New EventHandler(AddressOf BackupIarClick)
                    LoadLocalIARsToolStripMenuItem.Visible = True
                    LoadLocalIARsToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {IarMenu})
                    Log("Info", Name)
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

    Shared Sub Help(page As String)

        ' Set the new form's desktop location so it appears below and
        ' to the right of the current form.
        Dim FormHelp As New FormHelp
        FormHelp.Activate()
        FormHelp.Visible = True
        FormHelp.Init(page)

    End Sub

    Public Sub HelpOnce(Webpage As String)

        newScreenPosition = New ScreenPos(Webpage)
        If Not newScreenPosition.Exists() Then
            ' Set the new form's desktop location so it appears below and
            ' to the right of the current form.
            Dim FormHelp As New FormHelp

            FormHelp.Activate()
            FormHelp.Visible = True
            FormHelp.Init(Webpage)
        End If

    End Sub

    Private Sub HelpStartingUpToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles HelpStartingUpToolStripMenuItem1.Click

        Help("Startup")

    End Sub

    Private Sub HelpClick(sender As Object, e As EventArgs)

        If sender.text.ToString <> "Dreamgrid Manual.pdf" Then Help(sender.text.ToString)

    End Sub

    Private Sub LogViewClick(sender As Object, e As EventArgs)

        Dim name As String = sender.text.ToString()

        Viewlog(name)
    End Sub

    Public Sub Viewlog(name As String)

        Dim AllLogs As Boolean = False
        Dim path As New List(Of String)

        If name.StartsWith("Region ") Then
            name = Replace(name, "Region ", "", 1, 1)
            name = RegionClass.GroupName(RegionClass.FindRegionByName(name))
            path.Add("""" & GOpensimBinPath & "bin\Regions\" & name & "\Opensim.log" & """")
        Else
            If name = "All Logs" Then AllLogs = True
            If name = "Robust" Or AllLogs Then path.Add("""" & GOpensimBinPath & "bin\Robust.log" & """")
            If name = "Outworldz" Or AllLogs Then path.Add("""" & MyFolder & "\Outworldzfiles\Outworldz.log" & """")
            If name = "UPnP" Or AllLogs Then path.Add("""" & MyFolder & "\Outworldzfiles\Upnp.log" & """")
            If name = "Icecast" Or AllLogs Then path.Add(" " & """" & MyFolder & "\Outworldzfiles\Icecast\log\error.log" & """")
            If name = "All Settings" Or AllLogs Then path.Add("""" & MyFolder & "\Outworldzfiles\Settings.ini" & """")
            If name = "--- Regions ---" Then Return

            If AllLogs Then
                For Each Regionnumber In RegionClass.RegionNumbers
                    name = RegionClass.GroupName(Regionnumber)
                    path.Add("""" & GOpensimBinPath & "bin\Regions\" & name & "\Opensim.log" & """")
                Next
            End If

            If name = "MySQL" Or AllLogs Then
                Dim MysqlLog As String = MyFolder & "\OutworldzFiles\mysql\data"
                Dim files() As String
                files = Directory.GetFiles(MysqlLog, "*.err", SearchOption.TopDirectoryOnly)
                For Each FileName As String In files
                    path.Add("""" & FileName & """")
                Next

            End If
        End If
        ' Filter distinct elements, and convert back into list.
        Dim result As List(Of String) = path.Distinct().ToList

        Dim logs As String = ""
        For Each item In result
            Log("View", item)
            logs = logs & " " & item
        Next

        Try
            System.Diagnostics.Process.Start(MyFolder + "\baretail.exe", logs)
        Catch
        End Try

    End Sub

    Private Sub RevisionHistoryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RevisionHistoryToolStripMenuItem.Click
        Help("Revisions")
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

#Region "Capslock"

    Shared Function SendableKeys(Str As String) As String

        If My.Computer.Keyboard.CapsLock Then
            For Pos = 1 To Len(Str)
                Dim C As String = Mid(Str, Pos, 1)
                Mid(Str, Pos) = CType(IIf(UCase(C) = C, LCase(C), UCase(C)), String)
            Next
        End If
        Return Str

    End Function

#End Region

#Region "QuickEdit"

    Private Sub SetQuickEditOff()
        Dim pi As ProcessStartInfo = New ProcessStartInfo With {
            .Arguments = "Set-ItemProperty -path HKCU:\Console -name QuickEdit -value 0",
            .FileName = "powershell.exe",
            .WindowStyle = ProcessWindowStyle.Hidden,
            .Verb = "runas"
        }
        Dim PowerShell As Process = New Process With {
            .StartInfo = pi
        }

        Try
            PowerShell.Start()
        Catch ex As Exception
            Log("Error", "Could not set Quickedit Off:" + ex.Message)
        End Try

    End Sub

#End Region

#Region "Sequential"

    Public Sub SequentialPause()

        If MySetting.Sequential Then

            For Each X As Integer In RegionClass.RegionNumbers
                If OpensimIsRunning() And RegionClass.RegionEnabled(X) And
                    Not (RegionClass.Status(X) = RegionMaker.SIMSTATUSENUM.RecyclingDown _
                    Or RegionClass.Status(X) = RegionMaker.SIMSTATUSENUM.ShuttingDown _
                    Or RegionClass.Status(X) = RegionMaker.SIMSTATUSENUM.Stopped) Then

                    Dim ctr = 600 ' 1 minute max to start a region
                    Dim WaitForIt = True
                    While WaitForIt
                        Sleep(100)
                        If RegionClass.RegionEnabled(X) _
                    And Not GAborting _
                    And (RegionClass.Status(X) = RegionMaker.SIMSTATUSENUM.RecyclingUp Or
                        RegionClass.Status(X) = RegionMaker.SIMSTATUSENUM.ShuttingDown Or
                        RegionClass.Status(X) = RegionMaker.SIMSTATUSENUM.RecyclingDown Or
                        RegionClass.Status(X) = RegionMaker.SIMSTATUSENUM.Booting) Then
                            WaitForIt = True
                        Else
                            WaitForIt = False
                        End If
                        ctr -= 1
                        If ctr <= 0 Then WaitForIt = False
                    End While
                End If
            Next
        Else
            Dim ctr = 600 ' 1 minute max to start a region
            Dim WaitForIt = True
            While WaitForIt
                Sleep(100)
                If cpu.NextValue() < gCPUMAX Then
                    WaitForIt = False
                    ctr -= 1
                    If ctr <= 0 Then WaitForIt = False
                End If
            End While

        End If

    End Sub

#End Region

#Region "Firewall"

    Private Function DeleteFirewallRules() As String

        Dim Command As String = "netsh advfirewall firewall  delete rule name=""Opensim TCP Port " & MySetting.DiagnosticPort & """" & vbCrLf _
                          & "netsh advfirewall firewall  delete rule name=""Opensim UDP Port " & MySetting.DiagnosticPort & """" & vbCrLf _
                          & "netsh advfirewall firewall  delete rule name=""Opensim HTTP TCP Port " & MySetting.HttpPort & """" & vbCrLf _
                          & "netsh advfirewall firewall  delete rule name=""Opensim HTTP UDP Port " & MySetting.HttpPort & """" & vbCrLf _
                          & "netsh advfirewall firewall  delete rule name=""Icecast Port1 UDP " & MySetting.SCPortBase.ToString & """" & vbCrLf _
                          & "netsh advfirewall firewall  delete rule name=""Icecast Port1 TCP " & MySetting.SCPortBase.ToString & """" & vbCrLf _
                          & "netsh advfirewall firewall  delete rule name=""Icecast Port2 UDP " & MySetting.SCPortBase1.ToString & """" & vbCrLf _
                          & "netsh advfirewall firewall  delete rule name=""Icecast Port2 TCP " & MySetting.SCPortBase1.ToString & """" & vbCrLf

        If MySetting.ApacheEnable Then
            Command = Command + "netsh advfirewall firewall  delete rule name=""Opensim HTTP Web Port " & MySetting.HttpPort & """" & vbCrLf
        End If

        Dim RegionNumber As Integer = 0
        Dim start = CInt(MySetting.FirstRegionPort)

        For RegionNumber = start To GMaxPortUsed
            Command = Command + "netsh advfirewall firewall  delete rule name=""Region TCP Port " & RegionNumber.ToString & """" & vbCrLf _
                          & "netsh advfirewall firewall  delete rule name=""Region UDP Port " & RegionNumber.ToString & """" & vbCrLf
        Next

        Return Command

    End Function

    Private Function AddFirewallRules() As String

        ' TCP only for 8001 (DiagnosticPort) and both for 8002
        Dim Command As String = "netsh advfirewall firewall  add rule name=""Opensim TCP Port " & MySetting.DiagnosticPort & """ dir=in action=allow protocol=TCP localport=" & MySetting.DiagnosticPort & vbCrLf _
                          & "netsh advfirewall firewall  add rule name=""Opensim HTTP TCP Port " & MySetting.HttpPort & """ dir=in action=allow protocol=TCP localport=" & MySetting.HttpPort & vbCrLf _
                          & "netsh advfirewall firewall  add rule name=""Opensim HTTP UDP Port " & MySetting.HttpPort & """ dir=in action=allow protocol=UDP localport=" & MySetting.HttpPort & vbCrLf

        If MySetting.ApacheEnable Then
            Command = Command + "netsh advfirewall firewall  add rule name=""Opensim HTTP Web Port " & MySetting.HttpPort & """ dir=in action=allow protocol=TCP localport=" & MySetting.ApachePort & vbCrLf
        End If

        ' Icecast needs both ports for both protocols
        If MySetting.SCEnable Then
            Command = Command & "netsh advfirewall firewall  add rule name=""Icecast Port1 UDP " & MySetting.SCPortBase.ToString & """ dir=in action=allow protocol=UDP localport=" & MySetting.SCPortBase & vbCrLf _
                          & "netsh advfirewall firewall  add rule name=""Icecast Port1 TCP " & MySetting.SCPortBase.ToString & """ dir=in action=allow protocol=TCP localport=" & MySetting.SCPortBase & vbCrLf _
                          & "netsh advfirewall firewall  add rule name=""Icecast Port2 UDP " & MySetting.SCPortBase1.ToString & """ dir=in action=allow protocol=UDP localport=" & MySetting.SCPortBase1 & vbCrLf _
                          & "netsh advfirewall firewall  add rule name=""Icecast Port2 TCP " & MySetting.SCPortBase1.ToString & """ dir=in action=allow protocol=TCP localport=" & MySetting.SCPortBase1 & vbCrLf
        End If

        Dim RegionNumber As Integer = 0
        Dim start = CInt(MySetting.FirstRegionPort)

        ' regions need both
        For RegionNumber = start To GMaxPortUsed
            Command = Command + "netsh advfirewall firewall  add rule name=""Region TCP Port " & RegionNumber.ToString & """ dir=in action=allow protocol=TCP localport=" & RegionNumber.ToString & vbCrLf _
                          & "netsh advfirewall firewall  add rule name=""Region UDP Port " & RegionNumber.ToString & """ dir=in action=allow protocol=UDP localport=" & RegionNumber.ToString & vbCrLf
        Next

        Return Command

    End Function

    Public Sub SetFirewall()

        Print("Setup Firewall")
        Dim CMD As String = DeleteFirewallRules() & AddFirewallRules()

        Dim ns As StreamWriter = New StreamWriter(MyFolder + "\fw.bat", False)
        ns.WriteLine(CMD)
        ns.Close()

        Dim pi As ProcessStartInfo = New ProcessStartInfo With {
            .Arguments = "",
            .FileName = MyFolder + "\fw.bat",
            .WindowStyle = ProcessWindowStyle.Hidden,
            .Verb = "runas"
        }
        Dim ProcessFirewall As Process = New Process With {
            .StartInfo = pi
        }

        Try
            ProcessFirewall.Start()
        Catch ex As Exception
            Log("Error", "Could not set firewall:" + ex.Message)
        End Try
    End Sub

    Private Sub PDFManualToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PDFManualToolStripMenuItem.Click
        Dim webAddress As String = MyFolder & "\Outworldzfiles\Help\Dreamgrid Manual.pdf"
        Process.Start(webAddress)
    End Sub

#End Region

#Region "Search"

    Private Sub SetupSearch()

        If MySetting.ServerType = "Metro" Or MySetting.ServerType = "OsGrid" Then Return

        Print("Setting up search")
        Dim pi As ProcessStartInfo = New ProcessStartInfo()

        FileIO.FileSystem.CurrentDirectory = MyFolder & "\Outworldzfiles\mysql\bin\"
        pi.FileName = "Create_OsSearch.bat"
        pi.UseShellExecute = False
        pi.WindowStyle = ProcessWindowStyle.Hidden
        Dim ProcessMysql As Process = New Process With {
            .StartInfo = pi
        }

        Try
            ProcessMysql.Start()
            ProcessMysql.WaitForExit()
        Catch ex As Exception
            ErrorLog("Error ProcessMysql failed to launch: " + ex.Message)
            FileIO.FileSystem.CurrentDirectory = MyFolder
            Return
        End Try
        FileIO.FileSystem.CurrentDirectory = MyFolder
        MySetting.SearchInstalled = True
        MySetting.SaveSettings()

    End Sub

    Private Sub RunDataSnapshot()

        Diagnostics.Debug.Print("Scanning Datasnapshot")
        Dim pi As ProcessStartInfo = New ProcessStartInfo()

        FileIO.FileSystem.CurrentDirectory = MyFolder & "\Outworldzfiles\PHP5\"
        pi.FileName = "Run_parser.bat"
        pi.UseShellExecute = False  ' needed to make window hidden
        pi.WindowStyle = ProcessWindowStyle.Hidden
        Dim ProcessPHP As Process = New Process With {
            .StartInfo = pi
        }
        ProcessPHP.StartInfo.CreateNoWindow = True

        Try
            ProcessPHP.Start()
            ProcessPHP.WaitForExit()
        Catch ex As Exception
            ErrorLog("Error ProcessPHP failed to launch: " + ex.Message)
            FileIO.FileSystem.CurrentDirectory = MyFolder
        End Try
    End Sub

    <CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")>
    Private Sub GetEvents()

        If Not MySetting.ApacheEnable Then Return

        Dim Simevents As New Dictionary(Of String, String)
        Dim ossearch As String = "server=" + MySetting.RobustServer() _
        + ";database=" + "ossearch" _
        + ";port=" + MySetting.MySqlPort _
        + ";user=" + MySetting.RobustUsername _
        + ";password=" + MySetting.RobustPassword _
        + ";Old Guids=true;Allow Zero Datetime=true;"

        Dim osconnection As MySqlConnection = New MySqlConnection(ossearch)
        Try
            osconnection.Open()
        Catch ex As Exception
            Log("Error", "Failed to Connect to OsSearch")
            Return
        End Try

        DeleteEvents(osconnection)
        Dim ctr As Integer = 0
        Try

            Using client As New WebClient()
                Using Stream = client.OpenRead(GDomain + "/events.txt?r=" & Random())
                    Using reader = New StreamReader(Stream)

                        While reader.Peek <> -1
                            Dim s = reader.ReadLine
                            '"owneruuid^00000000-0000-0000-0000-000000000001|coveramount^0|creatoruuid^00000000-0000-0000-0000-000000000001|covercharge^0|eventflags^0|name^TEMPELRITTERambiente bei der Teststrecke fuer Avatare in Deutsch im Greenworld Grid|dateUTC^1554958800|duration^1440|description^Teste einmal, wie fit Du bereits in virtuellen Welten bist. Und entdecke dabei das  Greenworld Grid Kannst Du laufen, die Kamerakontrolle, etwas bauen und schnell reagieren? Dann versuche Dein Glueck auf der Teststrecke im Tempelritterambiente auf der Sim vhs im OSGrid! Die Teststrecke hat 6 Stationen. Du kannst jederzeit abbrechen oder neu beginnen. Es macht Spass und hilft Dir, Dich besser als Newbie, Anfaenger oder Fortgeschrittener einzustufen. Die Teststrecke beginnt beim roten Infostaender im Garten von StartPunkt. Klicke darauf und loese die erste Aufgabe. Danach wirst Du zur naechsten Station teleportiert. Viel Glueck. StartPunkt in virtueller Welt - Ihr Das macht Sinn!|globalPos^128,128,25|simname^http://greenworld.online:9022:startpunkt|category^0|parcelUUID^00000000-0000-0000-0000-000000000001|"

                            ' Split line on comma.
                            Dim array As String() = s.Split("|".ToCharArray())
                            Simevents.Clear()
                            ' Loop over each string received.
                            Dim part As String
                            For Each part In array
                                ' Display to console.
                                Dim a As String() = part.Split("^".ToCharArray())
                                If a.Count = 2 Then
                                    a(1) = a(1).Replace("'", "\'")
                                    a(1) = a(1).Replace("`", vbLf)
                                    Console.WriteLine("{0}:{1}", a(0), a(1))
                                    Simevents.Add(a(0), a(1))
                                    ctr += 1

                                End If
                            Next
                            Diagnostics.Debug.Print("Items: {0}", Simevents.Count)
                            WriteEvent(osconnection, Simevents)
                        End While
                    End Using
                End Using
            End Using
            osconnection.Close()
            'Print(ctr.ToString & " hypevents received")
        Catch
        End Try

    End Sub

    Shared Sub DeleteEvents(Connection As MySqlConnection)

        Dim stm = "delete from events"
        Dim cmd As MySqlCommand = New MySqlCommand(stm, Connection)
        Dim rowsdeleted = cmd.ExecuteNonQuery()
        Diagnostics.Debug.Print("Rows: {0}", rowsdeleted.ToString)

    End Sub

    <CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")>
    Shared Sub WriteEvent(Connection As MySqlConnection, D As Dictionary(Of String, String))

        Try
            Dim stm = "insert into events (simname,category,creatoruuid, owneruuid,name, description, dateUTC,duration,covercharge, coveramount,parcelUUID, globalPos,eventflags) values (" _
                        & "'" & D.Item("simname") & "'," _
                        & "'" & D.Item("category") & "'," _
                        & "'" & D.Item("creatoruuid") & "'," _
                        & "'" & D.Item("owneruuid") & "'," _
                        & "'" & D.Item("name") & "'," _
                        & "'" & D.Item("description") & "'," _
                        & "'" & D.Item("dateUTC") & "'," _
                        & "'" & D.Item("duration") & "'," _
                        & "'" & D.Item("covercharge") & "'," _
                        & "'" & D.Item("coveramount") & "'," _
                        & "'" & D.Item("parcelUUID") & "'," _
                        & "'" & D.Item("globalPos") & "'," _
                        & "'" & D.Item("eventflags") & "')"

            Dim cmd As MySqlCommand = New MySqlCommand(stm, Connection)
            Dim rowsinserted = cmd.ExecuteNonQuery()
            Diagnostics.Debug.Print("Insert: {0}", rowsinserted.ToString)
        Catch ex As Exception

            Diagnostics.Debug.Print(ex.Message)
            For Each Keyvaluepair In D
                Diagnostics.Debug.Print("Key = {0}, Value = {1}", Keyvaluepair.Key, Keyvaluepair.Value)
            Next

        End Try

    End Sub

#End Region

End Class