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
Imports System.Globalization
Imports System.Net.WebUtility
Imports Outworldz

Public Class Form1

#Region "Declarations"

    Private _gUseIcons As Boolean = False
    Private _MyVersion As String = "3.02"
    Private _SimVersion As String = "0.9.0 2018-06-07 #38e937f91b08a2e52"
    Private _KillSource As Boolean = False      ' set to true to delete all source for Opensim
    Private _DNSSTimer As Integer = 0
    Private _debugOn As Boolean = False  ' set by code to log some events in when running a debugger
    Private _ExitHandlerIsBusy As Boolean = False
    Private _CPUMAX As Single = 75 ' max CPU % can be used when booting or we wait til it gets lower
    Private _ContentAvailable As Boolean = False ' for Web content

    Private _usa As CultureInfo = New CultureInfo("en-US")

    ' not https, which breaks stuff
    Private _Domain As String = "http://www.outworldz.com"

    Private _OpensimBinPath As String ' Holds path to Opensim folder
    Private _regionHandles As New Dictionary(Of Integer, String)
    Private _myFolder As String   ' Holds the current folder that we are running in
    Private _CurSlashDir As String '  holds the current directory info in Unix format for MySQL and Apache
    Private _IsRunning As Boolean = False ' used in pOpensimIsRunning property

    Dim client As New System.Net.WebClient ' downloadclient for web pages

    ' with events
    Private WithEvents ApacheProcess As New Process()

    Public Event ApacheExited As EventHandler

    Dim gApacheProcessID As Integer = 0

    Private WithEvents ProcessMySql As Process = New Process()

    Private _IcecastProcID As Integer
    Public Event Exited As EventHandler

    Private WithEvents RobustProcess As New Process()

    Public Event RobustExited As EventHandler

    Private _exitList As New ArrayList()
    Private _RobustProcID As Integer
    Private _RobustConnStr As String = ""
    Private _MaxPortUsed As Integer = 0  'Max number of port used past 8004
    Private _myUPnpMap As UPnp        ' UPNP pAborting
    Dim ws As NetServer             ' Port 8001 Webserver
    Private _Aborting As Boolean = False    ' Allows an Abort when Stopping is clicked

    Private _IPv4Address As String          ' global IPV4
    Private _mySetting As New MySettings  ' all settings from Settings.ini

    Private WithEvents IcecastProcess As New Process()
    Dim Adv As AdvancedForm

    Private _formCaches As New FormCaches

    ' Region
    Private _regionClass As RegionMaker   ' Global RegionClass
    Private _regionForm As RegionList
    Private _StopMysql As Boolean = True    'lets us detect if Mysql is a service so we do not shut it down
    Private _UpdateView As Boolean = True 'Region Form Refresh
    Private _Initted As Boolean = False
    Private _RestartNow As Boolean = False ' set true if a person clicks a restart button to get a sim restarted when auto restart is off
    Private _SelectedBox As String = ""
    Private _ForceParcel As Boolean = False
    Private _ForceTerrain As Boolean = False
    Private _ForceMerge As Boolean = False
    Private _UserName As String = ""

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
        'Me.Text = "Form screen position = " + Me.Location.ToString(usa)
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


    Public Property pContentAvailable() As Boolean
        Get
            Return _ContentAvailable
        End Get
        Set(ByVal Value As Boolean)
            _ContentAvailable = Value
        End Set
    End Property



    Public Property pDNSSTimer() As Integer
        Get
            Return _DNSSTimer
        End Get
        Set(ByVal Value As Integer)
            _DNSSTimer = Value
        End Set
    End Property


    Public Property pExitHandlerIsBusy() As Boolean
        Get
            Return _ExitHandlerIsBusy
        End Get
        Set(ByVal Value As Boolean)
            _ExitHandlerIsBusy = Value
        End Set
    End Property

    Public Property pInitted() As Boolean
        Get
            Return _Initted
        End Get
        Set(ByVal Value As Boolean)
            _Initted = Value
        End Set
    End Property

    Public Property pIPv4Address() As String
        Get
            Return _IPv4Address
        End Get
        Set(ByVal Value As String)
            _IPv4Address = Value
        End Set
    End Property

    Public Property pOpensimIsRunning() As Boolean
        Get
            Return _IsRunning
        End Get
        Set(ByVal Value As Boolean)
            _IsRunning = Value
        End Set
    End Property

    Public Property pDebug As Boolean
        Get
            Return _debugOn
        End Get
        Set(value As Boolean)
            _debugOn = value
        End Set
    End Property

    Public Property pDomain As String
        Get
            Return _Domain
        End Get
        Set(value As String)
            _Domain = value
        End Set
    End Property

    Public Property pOpensimBinPath As String
        Get
            Return _OpensimBinPath
        End Get
        Set(value As String)
            _OpensimBinPath = value
        End Set
    End Property

    Public ReadOnly Property pRegionHandles As Dictionary(Of Integer, String)
        Get
            Return _regionHandles
        End Get
    End Property

    Public Property pMyFolder As String
        Get
            Return _myFolder
        End Get
        Set(value As String)
            _myFolder = value
        End Set
    End Property

    Public Property pCurSlashDir As String
        Get
            Return _CurSlashDir
        End Get
        Set(value As String)
            _CurSlashDir = value
        End Set
    End Property


    Public ReadOnly Property pExitList As ArrayList
        Get
            Return _exitList
        End Get
    End Property

    Public Property pRobustProcID As Integer
        Get
            Return _RobustProcID
        End Get
        Set(value As Integer)
            _RobustProcID = value
        End Set
    End Property

    Public Property pRobustConnStr As String
        Get
            Return _RobustConnStr
        End Get
        Set(value As String)
            _RobustConnStr = value
        End Set
    End Property

    Public Property pMaxPortUsed As Integer
        Get
            Return _MaxPortUsed
        End Get
        Set(value As Integer)
            _MaxPortUsed = value
        End Set
    End Property

    Public Property pMyUPnpMap As UPnp
        Get
            Return _myUPnpMap
        End Get
        Set(value As UPnp)
            _myUPnpMap = value
        End Set
    End Property

    Public Property pAborting As Boolean
        Get
            Return _Aborting
        End Get
        Set(value As Boolean)
            _Aborting = value
        End Set
    End Property

    Public Property pMySetting As MySettings
        Get
            Return _mySetting
        End Get
        Set(value As MySettings)
            _mySetting = value
        End Set
    End Property

    Public Property pFormCaches As FormCaches
        Get
            Return _formCaches
        End Get
        Set(value As FormCaches)
            _formCaches = value
        End Set
    End Property

    Public Property pRegionClass As RegionMaker
        Get
            Return _regionClass
        End Get
        Set(value As RegionMaker)
            _regionClass = value
        End Set
    End Property

    Public Property pRegionForm As RegionList
        Get
            Return _regionForm
        End Get
        Set(value As RegionList)
            _regionForm = value
        End Set
    End Property

    Public Property pUpdateView As Boolean
        Get
            Return _UpdateView
        End Get
        Set(value As Boolean)
            _UpdateView = value
        End Set
    End Property

    Public Property pRestartNow As Boolean
        Get
            Return _RestartNow
        End Get
        Set(value As Boolean)
            _RestartNow = value
        End Set
    End Property

    Public Property pSelectedBox As String
        Get
            Return _SelectedBox
        End Get
        Set(value As String)
            _SelectedBox = value
        End Set
    End Property

    Public Property pForceParcel As Boolean
        Get
            Return _ForceParcel
        End Get
        Set(value As Boolean)
            _ForceParcel = value
        End Set
    End Property

    Public Property pForceTerrain As Boolean
        Get
            Return _ForceTerrain
        End Get
        Set(value As Boolean)
            _ForceTerrain = value
        End Set
    End Property

    Public Property pForceMerge As Boolean
        Get
            Return _ForceMerge
        End Get
        Set(value As Boolean)
            _ForceMerge = value
        End Set
    End Property

    Public Property pUserName As String
        Get
            Return _UserName
        End Get
        Set(value As String)
            _UserName = value
        End Set
    End Property

    Public Property pViewedSettings As Boolean
        Get
            Return _viewedSettings
        End Get
        Set(value As Boolean)
            _viewedSettings = value
        End Set
    End Property

    Public Property pUseIcons As Boolean
        Get
            Return _gUseIcons
        End Get
        Set(value As Boolean)
            _gUseIcons = value
        End Set
    End Property

    Public Property pMyVersion As String
        Get
            Return _MyVersion
        End Get
        Set(value As String)
            _MyVersion = value
        End Set
    End Property

    Public Property pSimVersion As String
        Get
            Return _SimVersion
        End Get
        Set(value As String)
            _SimVersion = value
        End Set
    End Property

    Public Property pKillSource As Boolean
        Get
            Return _KillSource
        End Get
        Set(value As Boolean)
            _KillSource = value
        End Set
    End Property

    Public Property pCPUMAX As Single
        Get
            Return _CPUMAX
        End Get
        Set(value As Single)
            _CPUMAX = value
        End Set
    End Property

    Public Property Usa As CultureInfo
        Get
            Return _usa
        End Get
        Set(value As CultureInfo)
            _usa = value
        End Set
    End Property

    Public Property pWs As NetServer
        Get
            Return ws
        End Get
        Set(value As NetServer)
            ws = value
        End Set
    End Property

    Public Property pStopMysql As Boolean
        Get
            Return _StopMysql
        End Get
        Set(value As Boolean)
            _StopMysql = value
        End Set
    End Property

    Public Property pIcecastProcID As Integer
        Get
            Return _IcecastProcID
        End Get
        Set(value As Integer)
            _IcecastProcID = value
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
        ' show box styled nicely.
        Application.EnableVisualStyles()
        Buttons(BusyButton)
        ProgressBar1.Visible = True
        ToolBar(False)

        ProgressBar1.Minimum = 0
        ProgressBar1.Maximum = 100
        ProgressBar1.Value = 0
        TextBox1.BackColor = Me.BackColor
        ' init the scrolling text box
        TextBox1.SelectionStart = 0
        TextBox1.ScrollToCaret()
        TextBox1.SelectionStart = TextBox1.Text.Length
        TextBox1.ScrollToCaret()

        Me.Show()

        ' setup a debug path
        pMyFolder = My.Application.Info.DirectoryPath

        If Debugger.IsAttached = True Then
            ' for debugging when compiling
            pDebug = True
            pMyFolder = pMyFolder.Replace("\Installer_Src\Setup DreamWorld\bin\Debug", "")
            pMyFolder = pMyFolder.Replace("\Installer_Src\Setup DreamWorld\bin\Release", "")
            ' for testing, as the compiler buries itself in ../../../debug
        End If

        pCurSlashDir = pMyFolder.Replace("\", "/")    ' because Mysql uses unix like slashes, that's why
        pOpensimBinPath() = pMyFolder & "\OutworldzFiles\Opensim\"

        If Not System.IO.File.Exists(pMyFolder & "\OutworldzFiles\Settings.ini") Then
            Print("Installing Desktop icon clicky thingy")
            Create_ShortCut(pMyFolder & "\Start.exe")
            BumpProgress10()
        End If

        pMySetting.Init(pMyFolder)
        pMySetting.Myfolder = pMyFolder
        pMySetting.OpensimBinPath = pOpensimBinPath

        SetScreen()     ' move Form to fit screen from SetXY.ini

        ' Save a random machine ID - we don't want any data to be sent that's personal or identifiable,  but it needs to be unique
        Randomize()
        If pMySetting.MachineID().Length = 0 Then pMySetting.MachineID() = Random()  ' a random machine ID may be generated.  Happens only once

        ' WebUI
        ViewWebUI.Visible = pMySetting.WifiEnabled

        Me.Text = "Dreamgrid V" + pMyVersion

        pOpensimIsRunning() = False ' true when opensim is running
        Me.Show()

        pRegionClass = RegionMaker.Instance()

        Adv = New AdvancedForm
        pInitted = True

        ClearLogFiles() ' clear log fles

        If Not IO.File.Exists(pMyFolder & "\BareTail.udm") Then
            IO.File.Copy(pMyFolder & "\BareTail.udm.bak", pMyFolder & "\BareTail.udm")
        End If

        pMyUPnpMap = New UPnp(pMyFolder)

        pMySetting.PublicIP = pMyUPnpMap.LocalIP
        pMySetting.PrivateURL = pMySetting.PublicIP

        ' Set them back to the DNS name if there is one
        If pMySetting.DNSName.Length > 0 Then
            pMySetting.PublicIP = pMySetting.DNSName
            pMySetting.GridServerName = pMySetting.DNSName
        End If

        If (pMySetting.SplashPage.Length = 0) Then
            pMySetting.SplashPage = pDomain() + "/Outworldz_installer/Welcome.htm"
        End If

        ProgressBar1.Value = 100
        ProgressBar1.Value = 0

        CheckForUpdates()

        CheckDefaultPorts()

        OpenPorts()

        SetQuickEditOff()

        SetLoopback()

        If Not SetIniData() Then Return

        pRobustConnStr = "server=" + pMySetting.RobustServer() _
            + ";database=" + pMySetting.RobustDataBaseName _
            + ";port=" + pMySetting.MySqlPort _
            + ";user=" + pMySetting.RobustUsername _
            + ";password=" + pMySetting.RobustPassword _
            + ";Old Guids=true;Allow Zero Datetime=true;"

        ' stash  for threading in Web server
        pMySetting.RobustConnStr = pRobustConnStr

        'must start after region Class Is instantiated
        pWs = NetServer.GetWebServer

        Print("Starting DreamGrid HTTP Server ")
        pWs.StartServer(pMyFolder, pMySetting)

        CheckDiagPort()

        pRegionClass.UpdateAllRegionPorts() ' must be after SetIniData
        SetFirewall()   ' must be after UpdateAllRegionPorts

        mnuSettings.Visible = True
        SetIAROARContent() ' load IAR and OAR web content
        LoadLocalIAROAR() ' load IAR and OAR local content

        If pMySetting.Password = "secret" Then
            BumpProgress10()
            Dim Password = New PassGen
            pMySetting.Password = Password.GeneratePass()
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

        If pMySetting.RegionListVisible Then
            ShowRegionform()
        End If
        Sleep(2000)
        Print("Check MySql")
        Dim isMySqlRunning = CheckPort("127.0.0.1", CType(pMySetting.MySqlPort, Integer))
        If isMySqlRunning Then pStopMysql() = False


        Buttons(StartButton)
        ProgressBar1.Value = 100

        If pMySetting.Autostart Then
            Print("Auto Startup")
            Startup()
        Else
            pMySetting.SaveSettings()
            Print("Ready to Launch!" + vbCrLf + "Click 'Start' to begin your adventure in Opensimulator.")
        End If

        HelpOnce("License") ' license on bottom
        HelpOnce("Startup")

        ProgressBar1.Value = 100

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
        pExitHandlerIsBusy = False
        pAborting = False  ' suppress exit warning messages
        ProgressBar1.Value = 0
        ProgressBar1.Visible = True
        ToolBar(False)
        Buttons(BusyButton)

        ' Set them back to the DNS name if there is one
        If pMySetting.DNSName.Length > 0 Then
            pMySetting.PublicIP = pMySetting.DNSName
            pMySetting.GridServerName = pMySetting.DNSName
        End If

        Print("Setup Ports")
        pRegionClass.UpdateAllRegionPorts() ' must be done before we are running

        SetFirewall()   ' must be after UpdateAllRegionPorts

        ' clear region error handlers
        pRegionHandles.Clear()

        If pMySetting.AutoBackup Then
            ' add 30 minutes to allow time to auto backup and then restart
            Dim BTime As Int16 = CType(pMySetting.AutobackupInterval, Int16)
            If pMySetting.AutoRestartInterval > 0 And pMySetting.AutoRestartInterval < BTime Then
                pMySetting.AutoRestartInterval = BTime + 30
                Print("Upping AutoRestart Time to " + BTime.ToString(Usa) + " + 30 Minutes.")
            End If
        End If

        pOpensimIsRunning() = True

        If pViewedSettings Then
            Print("Read Region INI files")
            pRegionClass.GetAllRegions()

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
            If pMySetting.BirdsModuleStartup Then
                My.Computer.FileSystem.CopyFile(pOpensimBinPath + "\bin\OpenSimBirds.Module.bak", pOpensimBinPath + "\bin\OpenSimBirds.Module.dll")
            Else
                My.Computer.FileSystem.DeleteFile(pOpensimBinPath + "\bin\OpenSimBirds.Module.dll")
            End If
        Catch ex As Exception
        End Try

        If Not StartRobust() Then
            Return
        End If

        If Not pMySetting.RunOnce And pMySetting.ServerType = "Robust" Then
            ConsoleCommand("Robust", "create user{ENTER}")
            MsgBox("Please type the Grid Owner's avatar name into the Robust window. Press <enter> for UUID and Model name. Then press this OK button", vbInformation, "Info")
            pMySetting.RunOnce = True
            pMySetting.SaveSettings()
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
        If pContentAvailable Then
            IslandToolStripMenuItem.Visible = True
            ClothingInventoryToolStripMenuItem.Visible = True
        End If

        Buttons(StopButton)
        ProgressBar1.Value = 100
        Print("Grid address is" + vbCrLf + "http://" + pMySetting.GridServerName + ":" + pMySetting.HttpPort)

        ' done with bootup
        ProgressBar1.Visible = False
        ToolBar(True)
    End Sub

    Private Sub Form1_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        ReallyQuit()
    End Sub


    Private Sub SetLoopback()

        Dim Adapters = NetworkInterface.GetAllNetworkInterfaces()
        For Each adapter As NetworkInterface In Adapters
            If adapter.Name = "Loopback" Then

                Print("Setting Loopback to WAN IP address")

                Dim LoopbackProcess As New Process
                LoopbackProcess.StartInfo.UseShellExecute = True ' so we can redirect streams
                LoopbackProcess.StartInfo.FileName = pMyFolder & "\NAT_Loopback_Tool.bat"
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
                System.IO.Directory.Delete(pMyFolder & folder, True)
            Catch ex As Exception
                Diagnostics.Debug.Print(ex.Message)
            End Try
        Next

    End Sub

    Private Sub KillFiles(AL As List(Of String))

        For Each filename As String In AL
            Try
                My.Computer.FileSystem.DeleteFile(pMyFolder & filename)
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

        If pKillSource Then
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

        If pKillSource Then
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
        pWs.StopWebServer()
        pAborting = True
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
        pAborting = True
        ProgressBar1.Value = 100
        ProgressBar1.Visible = True
        ToolBar(False)
        ' close everything as gracefully as possible.

        StopIcecast()
        StopApache()

        Dim n As Integer = pRegionClass.RegionCount()
        Diagnostics.Debug.Print("N=" + n.ToString(Usa))

        Dim TotalRunningRegions As Integer

        For Each Regionnumber As Integer In pRegionClass.RegionNumbers
            If pRegionClass.IsBooted(Regionnumber) Then
                TotalRunningRegions += 1
            End If
        Next
        Log("Info", "Total Enabled Regions=" + TotalRunningRegions.ToString(Usa))

        For Each X As Integer In pRegionClass.RegionNumbers
            If pOpensimIsRunning() And pRegionClass.RegionEnabled(X) And
            Not (pRegionClass.Status(X) = RegionMaker.SIMSTATUSENUM.RecyclingDown _
            Or pRegionClass.Status(X) = RegionMaker.SIMSTATUSENUM.ShuttingDown _
            Or pRegionClass.Status(X) = RegionMaker.SIMSTATUSENUM.Stopped) Then
                Print(pRegionClass.RegionName(X) & " is stopping")
                SequentialPause()
                ConsoleCommand(pRegionClass.GroupName(X), "q{ENTER}" + vbCrLf)
                pRegionClass.Status(X) = RegionMaker.SIMSTATUSENUM.ShuttingDown
                pRegionClass.Timer(X) = RegionMaker.REGIONTIMER.Stopped
                pUpdateView = True ' make form refresh
            End If
        Next

        Dim counter = 600 ' 10 minutes to quit all regions

        ' only wait if the port 8001 is working
        If pUseIcons Then
            If pOpensimIsRunning Then Print("Waiting for all regions to exit")

            While (counter > 0 And pOpensimIsRunning())
                ' decrement progress bar according to the ratio of what we had / what we have now
                counter -= 1
                Dim CountisRunning As Integer = 0

                For Each X In pRegionClass.RegionNumbers
                    If (Not pRegionClass.Status(X) = RegionMaker.SIMSTATUSENUM.Stopped) And pRegionClass.RegionEnabled(X) Then
                        If CheckPort(pMySetting.PrivateURL, pRegionClass.GroupPort(X)) Then
                            CountisRunning += 1
                        Else
                            StopGroup(pRegionClass.GroupName(X))
                        End If
                        'pUpdateView = True ' make form refresh
                    End If


                    If CountisRunning = 0 Then Exit For
                Next

                If CountisRunning = 1 Then
                    Print("1 region is still running")
                    Sleep(1000)
                Else
                    Print(CountisRunning.ToString(Usa) & " regions are still running")
                End If

                If CountisRunning = 0 Then
                    counter = 0
                    ProgressBar1.Value = 0
                End If

                Dim v As Double = CountisRunning / TotalRunningRegions * 100
                If v >= 0 And v <= 100 Then
                    ProgressBar1.Value = CType(v, Integer)
                    Diagnostics.Debug.Print("V=" + ProgressBar1.Value.ToString(Usa))
                End If
                pUpdateView = True ' make form refresh
                Application.DoEvents()
            End While
        End If

        pRegionHandles.Clear()

        StopAllRegions()

        pUpdateView = True ' make form refresh

        ConsoleCommand("Robust", "q{ENTER}" + vbCrLf)

        ' cannot load OAR or IAR, either
        IslandToolStripMenuItem.Visible = False
        ClothingInventoryToolStripMenuItem.Visible = False
        Timer1.Stop()
        pOpensimIsRunning() = False
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
            Log("Info", "Stopping process " + processName)
            P.Kill()
        Next

    End Sub

    Private Sub CleanDLLs()

        Dim dlls As List(Of String) = GetDlls(pMyFolder & "/dlls.txt")
        Dim localdlls As List(Of String) = GetFilesRecursive(pOpensimBinPath & "bin")
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
            If tofind.ToLower(Form1.Usa) = filename.ToLower(Form1.Usa) Then Return True
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
        MyShortcut.IconLocation = WshShell.ExpandEnvironmentStrings(pMyFolder & "\Start.exe")
        MyShortcut.WorkingDirectory = pMyFolder
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

        Print("(c) 2017 Outworldz,LLC" + vbCrLf + "Version " + pMyVersion)
        Dim webAddress As String = pDomain + "/Outworldz_Installer"
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

        pMySetting.ConsoleShow = mnuShow.Checked
        pMySetting.SaveSettings()

        If pOpensimIsRunning() Then
            Print("The Opensimulator Console will be shown the next time the system is started.")
        End If

    End Sub

    Private Sub MnuHide_Click(sender As System.Object, e As System.EventArgs) Handles mnuHide.Click
        Print("The Opensimulator Console will not be shown. You can still interact with it with Help->Opensim Console")
        mnuShow.Checked = False
        mnuHide.Checked = True

        pMySetting.ConsoleShow = mnuShow.Checked
        pMySetting.SaveSettings()

        If pOpensimIsRunning() Then
            Print("The Opensimulator Console will not be shown. Change will occur when Opensim is restarted")
        End If

    End Sub

    Shared Function Random() As String
        Dim value As Integer = CInt(Int((600000000 * Rnd()) + 1))
        Random = System.Convert.ToString(value, Form1.Usa)
    End Function

    Private Sub WebUIToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs)
        Print("The Web UI lets you add or view settings for the default avatar. ")
        If pOpensimIsRunning() Then
            Dim webAddress As String = "http://127.0.0.1:" + pMySetting.HttpPort
            Process.Start(webAddress)
        End If
    End Sub

#End Region

#Region "INI"

    Public Sub SaveIceCast()

        Dim rgx As New Regex("[^a-zA-Z0-9 ]")
        Dim name As String = rgx.Replace(pMySetting.SimName, "")

        Dim icecast As String = "<icecast>" + vbCrLf +
                           "<hostname>" + pMySetting.PublicIP + "</hostname>" + vbCrLf +
                            "<location>" + name + "</location>" + vbCrLf +
                            "<admin>" + pMySetting.AdminEmail + "</admin>" + vbCrLf +
                            "<shoutcast-mount>/stream</shoutcast-mount>" + vbCrLf +
                            "<listen-socket>" + vbCrLf +
                            "    <port>" + pMySetting.SCPortBase.ToString(Usa) + "</port>" + vbCrLf +
                            "</listen-socket>" + vbCrLf +
                            "<listen-socket>" + vbCrLf +
                            "   <port>" + pMySetting.SCPortBase1.ToString(Usa) + "</port>" + vbCrLf +
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
                                  "<source-password>" + pMySetting.SCPassword + "</source-password>" + vbCrLf +
                                  "<relay-password>" + pMySetting.SCPassword + "</relay-password>" + vbCrLf +
                                  "<admin-user>admin</admin-user>" + vbCrLf +
                                  "<admin-password>" + pMySetting.SCPassword + "</admin-password>" + vbCrLf +
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

        Using outputFile As New StreamWriter(pMyFolder + "\Outworldzfiles\Icecast\icecast_run.xml", False)
            outputFile.WriteLine(icecast)
        End Using

    End Sub

    Public Sub CopyWifi(Page As String)
        Try
            System.IO.Directory.Delete(pOpensimBinPath + "WifiPages", True)
            System.IO.Directory.Delete(pOpensimBinPath + "bin\WifiPages", True)
        Catch ex As Exception
            Log("Info", ex.Message)
        End Try

        Try
            My.Computer.FileSystem.CopyDirectory(pOpensimBinPath + "WifiPages-" + Page, pOpensimBinPath + "WifiPages", True)
            My.Computer.FileSystem.CopyDirectory(pOpensimBinPath + "bin\WifiPages-" + Page, pOpensimBinPath + "\bin\WifiPages", True)
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
            Dim o As Integer = pRegionClass.FindRegionByName(pMySetting.WelcomeRegion)

            If o < 0 Then
                o = 0
            End If

            ' save to disk
            Dim DefaultName = pRegionClass.RegionName(o)
            pMySetting.WelcomeRegion = DefaultName
            pMySetting.SaveSettings()

            '(replace spaces with underscore)
            DefaultName = DefaultName.Replace(" ", "_")    ' because this is a screwy thing they did in the INI file

            Dim onceflag As Boolean = False ' only do the DefaultName
            Dim counter As Integer = 0

            Try
                My.Computer.FileSystem.DeleteFile(pOpensimBinPath + "bin\Robust.tmp")
            Catch ex As Exception
                'Nothing to do, this was just cleanup
            End Try

            Using outputFile As New StreamWriter(pOpensimBinPath + "bin\Robust.tmp")
                reader = System.IO.File.OpenText(pOpensimBinPath + "bin\Robust.HG.ini")
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
                    My.Computer.FileSystem.DeleteFile(pOpensimBinPath + "bin\Robust.HG.ini.bak")
                Catch ex As Exception
                    'Nothing to do, this was just cleanup
                End Try
                My.Computer.FileSystem.RenameFile(pOpensimBinPath + "bin\Robust.HG.ini", "Robust.HG.ini.bak")
                My.Computer.FileSystem.RenameFile(pOpensimBinPath + "bin\Robust.tmp", "Robust.HG.ini")
            Catch ex As Exception
                ErrorLog("Error:Set Default sims could not rename the file:" + ex.Message)
                My.Computer.FileSystem.RenameFile(pOpensimBinPath + "bin\Robust.HG.ini.bak", "Robust.HG.ini")
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

        Select Case pMySetting.ServerType
            Case "Robust"
                pMySetting.LoadOtherIni(pOpensimBinPath + "bin\Opensim.proto", ";")
                Return pOpensimBinPath + "bin\Opensim.proto"
            Case "Region"
                pMySetting.LoadOtherIni(pOpensimBinPath + "bin\OpensimRegion.proto", ";")
                Return pOpensimBinPath + "bin\OpensimRegion.proto"
            Case "OsGrid"
                pMySetting.LoadOtherIni(pOpensimBinPath + "bin\OpensimOsGrid.proto", ";")
                Return pOpensimBinPath + "bin\OpensimOsGrid.proto"
            Case "Metro"
                pMySetting.LoadOtherIni(pOpensimBinPath + "bin\OpensimMetro.proto", ";")
                Return pOpensimBinPath + "bin\OpensimMetro.proto"
        End Select
        Return Nothing

    End Function

    Sub DelLibrary()

        Try
            System.IO.File.Delete(pOpensimBinPath + "bin\Library\Clothing Library (small).iar")
            System.IO.File.Delete(pOpensimBinPath + "bin\Library\Objects Library (small).iar")
        Catch ex As Exception
            Diagnostics.Debug.Print(ex.Message)
        End Try
    End Sub

    Private Function SetIniData() As Boolean

        'mnuShow shows the DOS box for Opensimulator
        mnuShow.Checked = pMySetting.ConsoleShow
        mnuHide.Checked = Not pMySetting.ConsoleShow

        Print("Creating INI Files")

        If pMySetting.ConsoleShow Then
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
            pMySetting.LoadOtherIni(pOpensimBinPath + "bin\DivaTOS.ini", ";")

            'Disable it as it is broken for now.

            'pMySetting.SetOtherIni("TOSModule", "Enabled", pMySetting.TOSEnabled)
            pMySetting.SetOtherIni("TOSModule", "Enabled", False.ToString(Usa))
            'pMySetting.SetOtherIni("TOSModule", "Message", pMySetting.TOSMessage)
            'pMySetting.SetOtherIni("TOSModule", "Timeout", pMySetting.TOSTimeout)
            pMySetting.SetOtherIni("TOSModule", "ShowToLocalUsers", pMySetting.ShowToLocalUsers.ToString(Usa))
            pMySetting.SetOtherIni("TOSModule", "ShowToForeignUsers", pMySetting.ShowToForeignUsers.ToString(Usa))
            pMySetting.SetOtherIni("TOSModule", "TOS_URL", "http://" + pMySetting.PublicIP + ":" + pMySetting.HttpPort + "/wifi/termsofservice.html")
            pMySetting.SaveOtherINI()
        End If

        ' Choose a GridCommon.ini to use.
        Dim GridCommon As String = "GridcommonGridServer"
        DelLibrary()
        Select Case pMySetting.ServerType
            Case "Robust"
                My.Computer.FileSystem.CopyDirectory(pOpensimBinPath + "bin\Library.proto", pOpensimBinPath + "bin\Library", True)
                GridCommon = "Gridcommon-GridServer.ini"
            Case "Region"
                My.Computer.FileSystem.CopyDirectory(pOpensimBinPath + "bin\Library.proto", pOpensimBinPath + "bin\Library", True)
                GridCommon = "Gridcommon-RegionServer.ini"
            Case "OsGrid"
                GridCommon = "Gridcommon-OsGridServer.ini"
            Case "Metro"
                GridCommon = "Gridcommon-MetroServer.ini"
        End Select

        ' Put that gridcommon.ini file in place
        IO.File.Copy(pOpensimBinPath + "bin\config-include\" & GridCommon, IO.Path.Combine(pOpensimBinPath, "bin\config-include\GridCommon.ini"), True)

        ' load and patch it up for MySQL
        pMySetting.LoadOtherIni(pOpensimBinPath + "bin\config-include\Gridcommon.ini", ";")
        Dim ConnectionString = """" _
        + "Data Source=" + pMySetting.RegionServer _
        + ";Database=" + pMySetting.RegionDBName _
        + ";Port=" + pMySetting.RegionPort _
        + ";User ID=" + pMySetting.RegionDBUsername _
        + ";Password=" + pMySetting.RegionDbPassword _
        + ";Old Guids=true;Allow Zero Datetime=true;" _
        + ";Connect Timeout=28800;Command Timeout=28800;" _
        + """"
        pMySetting.SetOtherIni("DatabaseService", "ConnectionString", ConnectionString)
        pMySetting.SaveOtherINI()

        ''''''''''''''''''''''''''''''''''''''''''
        If pMySetting.ServerType = "Robust" Then
            ' Robust Process
            pMySetting.LoadOtherIni(pOpensimBinPath + "bin\Robust.HG.ini", ";")

            ConnectionString = """" _
            + "Data Source=" + pMySetting.RobustServer _
            + ";Database=" + pMySetting.RobustDataBaseName _
            + ";Port=" + pMySetting.MySqlPort _
            + ";User ID=" + pMySetting.RobustUsername _
            + ";Password=" + pMySetting.RobustPassword _
            + ";Old Guids=true;Allow Zero Datetime=true;" _
            + ";Connect Timeout=28800;Command Timeout=28800;" _
            + """"

            pMySetting.SetOtherIni("DatabaseService", "ConnectionString", ConnectionString)
            pMySetting.SetOtherIni("Const", "GridName", pMySetting.SimName)
            pMySetting.SetOtherIni("Const", "BaseURL", "http://" & pMySetting.PublicIP)
            pMySetting.SetOtherIni("Const", "PrivURL", "http://" & pMySetting.PrivateURL)
            pMySetting.SetOtherIni("Const", "PublicPort", pMySetting.HttpPort) ' 8002
            pMySetting.SetOtherIni("Const", "PrivatePort", pMySetting.PrivatePort)
            pMySetting.SetOtherIni("Const", "http_listener_port", pMySetting.HttpPort)
            pMySetting.SetOtherIni("GridInfoService", "welcome", pMySetting.SplashPage)

            If pMySetting.Suitcase() Then
                pMySetting.SetOtherIni("HGInventoryService", "LocalServiceModule", "OpenSim.Services.HypergridService.dll:HGSuitcaseInventoryService")
            Else
                pMySetting.SetOtherIni("HGInventoryService", "LocalServiceModule", "OpenSim.Services.HypergridService.dll:HGInventoryService")
            End If

            ' LSL emails
            pMySetting.SetOtherIni("SMTP", "SMTP_SERVER_HOSTNAME", pMySetting.SmtpHost)
            pMySetting.SetOtherIni("SMTP", "SMTP_SERVER_PORT", pMySetting.SmtpPort)
            pMySetting.SetOtherIni("SMTP", "SMTP_SERVER_LOGIN", pMySetting.SmtpUsername)
            pMySetting.SetOtherIni("SMTP", "SMTP_SERVER_PASSWORD", pMySetting.SmtpPassword)

            If pMySetting.SearchLocal Then
                pMySetting.SetOtherIni("LoginService", "SearchURL", "${Const|BaseURL}:" & pMySetting.ApachePort & "/Search/query.php")
            Else
                pMySetting.SetOtherIni("LoginService", "SearchURL", "http://www.hyperica.com/Search/query.php")
            End If

            pMySetting.SetOtherIni("LoginService", "WelcomeMessage", pMySetting.WelcomeMessage)

            'FSASSETS
            If pMySetting.FsAssetsEnabled Then
                pMySetting.SetOtherIni("AssetService", "LocalServiceModule", "OpenSim.Services.FSAssetService.dll:FSAssetConnector")
            Else
                pMySetting.SetOtherIni("AssetService", "LocalServiceModule", "OpenSim.Services.AssetService.dll:AssetService")
            End If

            pMySetting.SetOtherIni("AssetService", "BaseDirectory", pMySetting.BaseDirectory)
            pMySetting.SetOtherIni("AssetService", "SpoolDirectory", pMySetting.SpoolDirectory)
            pMySetting.SetOtherIni("AssetService", "FallbackService", pMySetting.FallbackService)
            pMySetting.SetOtherIni("AssetService", "DaysBetweenAccessTimeUpdates", pMySetting.DaysBetweenAccessTimeUpdates)
            pMySetting.SetOtherIni("AssetService", "ShowConsoleStats", pMySetting.ShowConsoleStats)
            pMySetting.SetOtherIni("AssetService", "StorageProvider", pMySetting.StorageProvider)
            pMySetting.SetOtherIni("AssetService", "ConnectionString", pMySetting.ConnectionString)
            pMySetting.SetOtherIni("AssetService", "Realm", pMySetting.Realm)
            pMySetting.SetOtherIni("AssetService", "AllowRemoteDelete", pMySetting.AllowRemoteDelete)
            pMySetting.SetOtherIni("AssetService", "AllowRemoteDeleteAllTypes", pMySetting.AllowRemoteDeleteAllTypes)

            pMySetting.SaveOtherINI()

        End If

        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '''' Flotsam Cache.ini
        pMySetting.LoadOtherIni(pOpensimBinPath & "bin\config-include\FlotsamCache.ini", ";")
        pMySetting.SetOtherIni("AssetCache", "LogLevel", pMySetting.CacheLogLevel)
        pMySetting.SetOtherIni("AssetCache", "CacheDirectory", pMySetting.CacheFolder)
        pMySetting.SetOtherIni("AssetCache", "FileCacheEnabled", CType(pMySetting.CacheEnabled, String))
        pMySetting.SetOtherIni("AssetCache", "FileCacheTimeout", pMySetting.CacheTimeout)
        pMySetting.SaveOtherINI()

        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ' Opensim.ini
        pMySetting.LoadOtherIni(GetOpensimProto(), ";")

        Select Case pMySetting.ServerType
            Case "Robust"
                If pMySetting.SearchLocal Then
                    pMySetting.SetOtherIni("Search", "SearchURL", "${Const|BaseURL}:" & pMySetting.ApachePort & "/Search/query.php")
                    pMySetting.SetOtherIni("Search", "SimulatorFeatures", "${Const|BaseURL}:" & pMySetting.ApachePort & "/Search/query.php")
                    pMySetting.SetOtherIni("Search", "SimulatorFeatures", "${Const|BaseURL}:" & pMySetting.ApachePort & "/Search/query.php")
                Else
                    pMySetting.SetOtherIni("DataSnapshot", "data_services", "http://www.hyperica.com/Search/register.php")
                    pMySetting.SetOtherIni("Search", "SearchURL", "http://www.hyperica.com/Search/query.php")
                    pMySetting.SetOtherIni("Search", "SimulatorFeatures", "http://www.hyperica.com/Search/query.php")
                End If

                pMySetting.SetOtherIni("Const", "PrivURL", "http://" & pMySetting.PrivateURL)
                pMySetting.SetOtherIni("Const", "GridName", pMySetting.SimName)
            Case "Region"
            Case "OSGrid"
            Case "Metro"

        End Select

        '' all grids requires these setting in Opensim.ini
        pMySetting.SetOtherIni("Const", "DiagnosticsPort", pMySetting.DiagnosticPort)

        ' once and only once toggle to get Opensim 2.91
        If pMySetting.DeleteScriptsOnStartupOnce() Then
            KillOldFiles()  ' wipe out DLL's
            Dim Clr As New ClrCache()
            pMySetting.SetOtherIni("XEngine", "DeleteScriptsOnStartup", "True")
        Else
            pMySetting.SetOtherIni("XEngine", "DeleteScriptsOnStartup", "False")
        End If

        If pMySetting.LSLHTTP Then
            ' do nothing - let them edit it
        Else
            pMySetting.SetOtherIni("Network", "OutboundDisallowForUserScriptsExcept", pMySetting.PrivateURL + "/32")
        End If

        pMySetting.SetOtherIni("Network", "ExternalHostNameForLSL", pMySetting.GridServerName)
        pMySetting.SetOtherIni("DataSnapshot", "index_sims", "true")
        pMySetting.SetOtherIni("PrimLimitsModule", "EnforcePrimLimits", CType(pMySetting.Primlimits, String))

        If pMySetting.Primlimits Then
            pMySetting.SetOtherIni("Permissions", "permissionmodules", "DefaultPermissionsModule, PrimLimitsModule")
        Else
            pMySetting.SetOtherIni("Permissions", "permissionmodules", "DefaultPermissionsModule")
        End If

        If pMySetting.GloebitsEnable Then
            pMySetting.SetOtherIni("Startup", "economymodule", "Gloebit")
        Else
            pMySetting.SetOtherIni("Startup", "economymodule", "BetaGridLikeMoneyModule")
        End If

        ' LSL emails
        pMySetting.SetOtherIni("SMTP", "SMTP_SERVER_HOSTNAME", pMySetting.SmtpHost)
        pMySetting.SetOtherIni("SMTP", "SMTP_SERVER_PORT", pMySetting.SmtpPort)
        pMySetting.SetOtherIni("SMTP", "SMTP_SERVER_LOGIN", pMySetting.SmtpUsername)
        pMySetting.SetOtherIni("SMTP", "SMTP_SERVER_PASSWORD", pMySetting.SmtpPassword)
        pMySetting.SetOtherIni("SMTP", "host_domain_header_from", pMySetting.GridServerName)

        ' the old Clouds
        If pMySetting.Clouds Then
            pMySetting.SetOtherIni("Cloud", "enabled", "true")
            pMySetting.SetOtherIni("Cloud", "density", pMySetting.Density.ToString(Usa))
        Else
            pMySetting.SetOtherIni("Cloud", "enabled", "false")
            pMySetting.SetOtherIni("Cloud", "density", pMySetting.Density.ToString(Usa))
        End If

        ' Gods

        If (pMySetting.RegionOwnerIsGod Or pMySetting.RegionManagerIsGod) Then
            pMySetting.SetOtherIni("Permissions", "allow_grid_gods", "true")
        Else
            pMySetting.SetOtherIni("Permissions", "allow_grid_gods", "false")
        End If

        If (pMySetting.RegionOwnerIsGod) Then
            pMySetting.SetOtherIni("Permissions", "region_owner_is_god", "true")
        Else
            pMySetting.SetOtherIni("Permissions", "region_owner_is_god", "false")
        End If

        If (pMySetting.RegionManagerIsGod) Then
            pMySetting.SetOtherIni("Permissions", "region_manager_is_god", "true")
        Else
            pMySetting.SetOtherIni("Permissions", "region_manager_is_god", "false")
        End If

        If (pMySetting.AllowGridGods) Then
            pMySetting.SetOtherIni("Permissions", "allow_grid_gods", "true")
        Else
            pMySetting.SetOtherIni("Permissions", "allow_grid_gods", "false")
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

        Select Case pMySetting.Physics
            Case "0"
                pMySetting.SetOtherIni("Startup", "meshing", "ZeroMesher")
                pMySetting.SetOtherIni("Startup", "physics", "basicphysics")
                pMySetting.SetOtherIni("Startup", "UseSeparatePhysicsThread", "false")
            Case "1"
                pMySetting.SetOtherIni("Startup", "meshing", "Meshmerizer")
                pMySetting.SetOtherIni("Startup", "physics", "OpenDynamicsEngine")
                pMySetting.SetOtherIni("Startup", "UseSeparatePhysicsThread", "false")
            Case "2"
                pMySetting.SetOtherIni("Startup", "meshing", "Meshmerizer")
                pMySetting.SetOtherIni("Startup", "physics", "BulletSim")
                pMySetting.SetOtherIni("Startup", "UseSeparatePhysicsThread", "false")
            Case "3"
                pMySetting.SetOtherIni("Startup", "meshing", "Meshmerizer")
                pMySetting.SetOtherIni("Startup", "physics", "BulletSim")
                pMySetting.SetOtherIni("Startup", "UseSeparatePhysicsThread", "true")
            Case "4"
                pMySetting.SetOtherIni("Startup", "meshing", "ubODEMeshmerizer")
                pMySetting.SetOtherIni("Startup", "physics", "ubODE")
                pMySetting.SetOtherIni("Startup", "UseSeparatePhysicsThread", "false")
            Case "5"
                pMySetting.SetOtherIni("Startup", "meshing", "Meshmerizer")
                pMySetting.SetOtherIni("Startup", "physics", "ubODE")
                pMySetting.SetOtherIni("Startup", "UseSeparatePhysicsThread", "false")
            Case Else
                pMySetting.SetOtherIni("Startup", "meshing", "Meshmerizer")
                pMySetting.SetOtherIni("Startup", "physics", "BulletSim")
                pMySetting.SetOtherIni("Startup", "UseSeparatePhysicsThread", "true")
        End Select

        If pMySetting.MapType = "None" Then
            pMySetting.SetOtherIni("Map", "GenerateMaptiles", "false")
        ElseIf pMySetting.MapType = "Simple" Then
            pMySetting.SetOtherIni("Map", "GenerateMaptiles", "true")
            pMySetting.SetOtherIni("Map", "MapImageModule", "MapImageModule")  ' versus Warp3DImageModule
            pMySetting.SetOtherIni("Map", "TextureOnMapTile", "false")         ' versus true
            pMySetting.SetOtherIni("Map", "DrawPrimOnMapTile", "false")
            pMySetting.SetOtherIni("Map", "TexturePrims", "false")
            pMySetting.SetOtherIni("Map", "RenderMeshes", "false")
        ElseIf pMySetting.MapType = "Good" Then
            pMySetting.SetOtherIni("Map", "GenerateMaptiles", "true")
            pMySetting.SetOtherIni("Map", "MapImageModule", "Warp3DImageModule")  ' versus MapImageModule
            pMySetting.SetOtherIni("Map", "TextureOnMapTile", "false")         ' versus true
            pMySetting.SetOtherIni("Map", "DrawPrimOnMapTile", "false")
            pMySetting.SetOtherIni("Map", "TexturePrims", "false")
            pMySetting.SetOtherIni("Map", "RenderMeshes", "false")
        ElseIf pMySetting.MapType = "Better" Then
            pMySetting.SetOtherIni("Map", "GenerateMaptiles", "true")
            pMySetting.SetOtherIni("Map", "MapImageModule", "Warp3DImageModule")  ' versus MapImageModule
            pMySetting.SetOtherIni("Map", "TextureOnMapTile", "true")         ' versus true
            pMySetting.SetOtherIni("Map", "DrawPrimOnMapTile", "true")
            pMySetting.SetOtherIni("Map", "TexturePrims", "false")
            pMySetting.SetOtherIni("Map", "RenderMeshes", "false")
        ElseIf pMySetting.MapType = "Best" Then
            pMySetting.SetOtherIni("Map", "GenerateMaptiles", "true")
            pMySetting.SetOtherIni("Map", "MapImageModule", "Warp3DImageModule")  ' versus MapImageModule
            pMySetting.SetOtherIni("Map", "TextureOnMapTile", "true")      ' versus true
            pMySetting.SetOtherIni("Map", "DrawPrimOnMapTile", "true")
            pMySetting.SetOtherIni("Map", "TexturePrims", "true")
            pMySetting.SetOtherIni("Map", "RenderMeshes", "true")
        End If

        ' Autobackup
        If pMySetting.AutoBackup Then
            Log("Info", "Auto backup is On")
            pMySetting.SetOtherIni("AutoBackupModule", "AutoBackup", "true")
        Else
            Log("Info", "Auto backup is Off")
            pMySetting.SetOtherIni("AutoBackupModule", "AutoBackup", "false")
        End If

        pMySetting.SetOtherIni("AutoBackupModule", "AutoBackupInterval", pMySetting.AutobackupInterval)
        pMySetting.SetOtherIni("AutoBackupModule", "AutoBackupKeepFilesForDays", pMySetting.KeepForDays.ToString(Usa))
        pMySetting.SetOtherIni("AutoBackupModule", "AutoBackupDir", BackupPath())

        ' Voice
        If pMySetting.VivoxEnabled Then
            pMySetting.SetOtherIni("VivoxVoice", "enabled", "true")
        Else
            pMySetting.SetOtherIni("VivoxVoice", "enabled", "false")
        End If
        pMySetting.SetOtherIni("VivoxVoice", "vivox_admin_user", pMySetting.VivoxUserName)
        pMySetting.SetOtherIni("VivoxVoice", "vivox_admin_password", pMySetting.VivoxPassword)

        pMySetting.SaveOtherINI()

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
        pMySetting.LoadOtherIni(pOpensimBinPath + "bin\Gloebit.ini", ";")
        If pMySetting.GloebitsEnable Then

            pMySetting.SetOtherIni("Gloebit", "Enabled", "true")
        Else
            pMySetting.SetOtherIni("Gloebit", "Enabled", "false")
        End If

        If pMySetting.GloebitsMode Then
            pMySetting.SetOtherIni("Gloebit", "GLBEnvironment", "production")
            pMySetting.SetOtherIni("Gloebit", "GLBKey", pMySetting.GLProdKey)
            pMySetting.SetOtherIni("Gloebit", "GLBSecret", pMySetting.GLProdSecret)
        Else
            pMySetting.SetOtherIni("Gloebit", "GLBEnvironment", "sandbox")
            pMySetting.SetOtherIni("Gloebit", "GLBKey", pMySetting.GLSandKey)
            pMySetting.SetOtherIni("Gloebit", "GLBSecret", pMySetting.GLSandSecret)
        End If

        pMySetting.SetOtherIni("Gloebit", "GLBOwnerName", pMySetting.GLBOwnerName)
        pMySetting.SetOtherIni("Gloebit", "GLBOwnerEmail", pMySetting.GLBOwnerEmail)

        Dim ConnectionString = """" + "Data Source = " + pMySetting.RobustServer _
        + ";Database=" + pMySetting.RobustDataBaseName _
        + ";Port=" + pMySetting.MySqlPort _
        + ";User ID=" + pMySetting.RobustUsername _
        + ";Password=" + pMySetting.RobustPassword _
        + ";Old Guids=True;Allow Zero Datetime=True;" + """"

        pMySetting.SetOtherIni("Gloebit", "GLBSpecificConnectionString", ConnectionString)

        pMySetting.SaveOtherINI()

    End Sub

    Private Sub DoWifi()

        pMySetting.LoadOtherIni(pOpensimBinPath + "bin\Wifi.ini", ";")

        Dim ConnectionString = """" _
            + "Data Source=" + "127.0.0.1" _
            + ";Database=" + pMySetting.RobustDataBaseName _
            + ";Port=" + pMySetting.MySqlPort _
            + ";User ID=" + pMySetting.RobustUsername _
            + ";Password=" + pMySetting.RobustPassword _
            + ";Old Guids=True;Allow Zero Datetime=True;" _
            + """"

        pMySetting.SetOtherIni("DatabaseService", "ConnectionString", ConnectionString)

        ' Wifi Section

        If pMySetting.ServerType = "Robust" Then ' wifi could be on or off
            If (pMySetting.WifiEnabled) Then
                pMySetting.SetOtherIni("WifiService", "Enabled", "True")
            Else
                pMySetting.SetOtherIni("WifiService", "Enabled", "False")
            End If
        Else ' it is always off
            ' shutdown wifi in Attached mode
            pMySetting.SetOtherIni("WifiService", "Enabled", "False")
        End If

        pMySetting.SetOtherIni("WifiService", "GridName", pMySetting.SimName)
        pMySetting.SetOtherIni("WifiService", "LoginURL", "http://" & pMySetting.PublicIP & ":" & pMySetting.HttpPort)
        pMySetting.SetOtherIni("WifiService", "WebAddress", "http://" & pMySetting.PublicIP & ":" & pMySetting.HttpPort)

        ' Wifi Admin'
        pMySetting.SetOtherIni("WifiService", "AdminFirst", pMySetting.AdminFirst)    ' Wifi
        pMySetting.SetOtherIni("WifiService", "AdminLast", pMySetting.AdminLast)      ' Admin
        pMySetting.SetOtherIni("WifiService", "AdminPassword", pMySetting.Password)   ' secret
        pMySetting.SetOtherIni("WifiService", "AdminEmail", pMySetting.AdminEmail)    ' send notificatins to this person

        'Gmail and other SMTP mailers
        ' Gmail requires you set to set low security access
        pMySetting.SetOtherIni("WifiService", "SmtpHost", pMySetting.SmtpHost)
        pMySetting.SetOtherIni("WifiService", "SmtpPort", pMySetting.SmtpPort)
        pMySetting.SetOtherIni("WifiService", "SmtpUsername", pMySetting.SmtpUsername)
        pMySetting.SetOtherIni("WifiService", "SmtpPassword", pMySetting.SmtpPassword)

        pMySetting.SetOtherIni("WifiService", "HomeLocation", pMySetting.WelcomeRegion & "/" + pMySetting.HomeVectorX & "/" & pMySetting.HomeVectorY & "/" & pMySetting.HomeVectorZ)

        If pMySetting.AccountConfirmationRequired Then
            pMySetting.SetOtherIni("WifiService", "AccountConfirmationRequired", "true")
        Else
            pMySetting.SetOtherIni("WifiService", "AccountConfirmationRequired", "false")
        End If

        pMySetting.SaveOtherINI()

    End Sub

    Sub CopyOpensimProto(Optional name As String = "")

        If name.Length > 0 Then
            Dim X = pRegionClass.FindRegionByName(name)
            If (X > -1) Then Opensimproto(X)
        Else
            ' COPY OPENSIM.INI prototype to all region folders and set the Sim Name
            For Each X As Integer In pRegionClass.RegionNumbers
                Opensimproto(X)
            Next
        End If

    End Sub

    Sub Opensimproto(X As Integer)

        Dim regionName = pRegionClass.RegionName(X)
        Dim pathname = pRegionClass.IniPath(X)
        Diagnostics.Debug.Print(regionName)

        Try

            pMySetting.LoadOtherIni(GetOpensimProto(), ";")

            pMySetting.SetOtherIni("Const", "BaseHostname", pMySetting.GridServerName)
            pMySetting.SetOtherIni("Const", "PublicPort", pMySetting.HttpPort) ' 8002
            pMySetting.SetOtherIni("Const", "PrivURL", "http://" & pMySetting.PrivateURL) ' local IP
            pMySetting.SetOtherIni("Const", "http_listener_port", pRegionClass.RegionPort(X).ToString(Usa)) ' varies with region
            Dim name = pRegionClass.RegionName(X)

            ' save the http listener port away for the group
            pRegionClass.GroupPort(X) = pRegionClass.RegionPort(X)

            pMySetting.SetOtherIni("Const", "PrivatePort", pMySetting.PrivatePort) '8003
            pMySetting.SetOtherIni("Const", "RegionFolderName", pRegionClass.GroupName(X))
            pMySetting.SaveOtherINI()

            My.Computer.FileSystem.CopyFile(GetOpensimProto(), pathname + "Opensim.ini", True)

        Catch ex As Exception
            Print("Error: Failed to set the Opensim.ini for sim " + regionName + ":" + ex.Message)
            ErrorLog("Error: Failed to set the Opensim.ini for sim " + regionName + ":" + ex.Message)
        End Try

    End Sub

#End Region

#Region "Regions"

    ''' <summary>
    ''' Gets the External Host name which can be either the Public IP or a Host name. 
    ''' </summary>
    ''' <returns>Host for regions</returns>
    Public Function ExternLocalServerName() As String

        Dim Host As String = pMySetting.PublicIP

        If pMySetting.ExternalHostName.Length > 0 Then
            Host = pMySetting.ExternalHostName
        Else
            Host = pMySetting.PublicIP
        End If
        Return Host

    End Function
    Private Sub DoRegions()
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Regions - write all region.ini files with public IP and Public port

        Dim BirdFile = pOpensimBinPath + "bin\addon-modules\OpenSimBirds\config\OpenSimBirds.ini"

        System.IO.File.Delete(BirdFile)

        Dim TideFile = pOpensimBinPath + "bin\addon-modules\OpenSimTide\config\OpenSimTide.ini"

        System.IO.File.Delete(TideFile)

        ' has to be bound late so regions data is there.

        Dim fPort As String = pMySetting.FirstRegionPort()
        If fPort.Length = 0 Then
            fPort = pRegionClass.LowestPort().ToString(Usa)
            pMySetting.FirstRegionPort = fPort
            pMySetting.SaveSettings()
        End If

        ' Self setting Region Ports
        Dim FirstPort As Integer = CType(pMySetting.FirstRegionPort(), Integer)
        Dim BirdData As String = ""
        Dim TideData As String = ""

        For Each RegionNum As Integer In pRegionClass.RegionNumbers

            Dim simName = pRegionClass.RegionName(RegionNum)

            pMySetting.LoadOtherIni(pRegionClass.RegionPath(RegionNum), ";")

            pMySetting.SetOtherIni(simName, "InternalPort", pRegionClass.RegionPort(RegionNum).ToString(Usa))
            pMySetting.SetOtherIni(simName, "ExternalHostName", ExternLocalServerName())

            ' not a standard INI, only use by the Dreamers
            If pRegionClass.RegionEnabled(RegionNum) Then
                pMySetting.SetOtherIni(simName, "Enabled", "True")
            Else
                pMySetting.SetOtherIni(simName, "Enabled", "False")
            End If

            ' Extended in v 2.1
            pMySetting.SetOtherIni(simName, "NonPhysicalPrimMax", Convert.ToString(pRegionClass.NonPhysicalPrimMax(RegionNum), Usa))
            pMySetting.SetOtherIni(simName, "PhysicalPrimMax", Convert.ToString(pRegionClass.PhysicalPrimMax(RegionNum), Usa))
            If (pMySetting.Primlimits) Then
                pMySetting.SetOtherIni(simName, "MaxPrims", Convert.ToString(pRegionClass.MaxPrims(RegionNum), Usa))
            Else
                pMySetting.SetOtherIni(simName, "MaxPrims", "")
            End If
            pMySetting.SetOtherIni(simName, "MaxAgents", Convert.ToString(pRegionClass.MaxAgents(RegionNum), Usa))
            pMySetting.SetOtherIni(simName, "ClampPrimSize", Convert.ToString(pRegionClass.ClampPrimSize(RegionNum), Usa))

            ' Extended in v 2.31 optional things
            If pRegionClass.MapType(RegionNum) = "None" Then
                pMySetting.SetOtherIni(simName, "GenerateMaptiles", "False")
            ElseIf pRegionClass.MapType(RegionNum) = "Simple" Then
                pMySetting.SetOtherIni(simName, "GenerateMaptiles", "True")
                pMySetting.SetOtherIni(simName, "MapImageModule", "MapImageModule")  ' versus Warp3DImageModule
                pMySetting.SetOtherIni(simName, "TextureOnMapTile", "False")         ' versus True
                pMySetting.SetOtherIni(simName, "DrawPrimOnMapTile", "False")
                pMySetting.SetOtherIni(simName, "TexturePrims", "False")
                pMySetting.SetOtherIni(simName, "RenderMeshes", "False")
            ElseIf pRegionClass.MapType(RegionNum) = "Good" Then
                pMySetting.SetOtherIni(simName, "GenerateMaptiles", "True")
                pMySetting.SetOtherIni(simName, "MapImageModule", "Warp3DImageModule")  ' versus MapImageModule
                pMySetting.SetOtherIni(simName, "TextureOnMapTile", "False")         ' versus True
                pMySetting.SetOtherIni(simName, "DrawPrimOnMapTile", "False")
                pMySetting.SetOtherIni(simName, "TexturePrims", "False")
                pMySetting.SetOtherIni(simName, "RenderMeshes", "False")
            ElseIf pRegionClass.MapType(RegionNum) = "Better" Then
                pMySetting.SetOtherIni(simName, "GenerateMaptiles", "True")
                pMySetting.SetOtherIni(simName, "MapImageModule", "Warp3DImageModule")  ' versus MapImageModule
                pMySetting.SetOtherIni(simName, "TextureOnMapTile", "True")         ' versus True
                pMySetting.SetOtherIni(simName, "DrawPrimOnMapTile", "True")
                pMySetting.SetOtherIni(simName, "TexturePrims", "False")
                pMySetting.SetOtherIni(simName, "RenderMeshes", "False")
            ElseIf pRegionClass.MapType(RegionNum) = "Best" Then
                pMySetting.SetOtherIni(simName, "GenerateMaptiles", "True")
                pMySetting.SetOtherIni(simName, "MapImageModule", "Warp3DImageModule")  ' versus MapImageModule
                pMySetting.SetOtherIni(simName, "TextureOnMapTile", "True")      ' versus True
                pMySetting.SetOtherIni(simName, "DrawPrimOnMapTile", "True")
                pMySetting.SetOtherIni(simName, "TexturePrims", "True")
                pMySetting.SetOtherIni(simName, "RenderMeshes", "True")
            Else
                pMySetting.SetOtherIni(simName, "GenerateMaptiles", "")
                pMySetting.SetOtherIni(simName, "MapImageModule", "")  ' versus MapImageModule
                pMySetting.SetOtherIni(simName, "TextureOnMapTile", "")      ' versus True
                pMySetting.SetOtherIni(simName, "DrawPrimOnMapTile", "")
                pMySetting.SetOtherIni(simName, "TexturePrims", "")
                pMySetting.SetOtherIni(simName, "RenderMeshes", "")
            End If

            pMySetting.SetOtherIni(simName, "Physics", pRegionClass.Physics(RegionNum))
            pMySetting.SetOtherIni(simName, "MaxPrims", pRegionClass.MaxPrims(RegionNum))
            pMySetting.SetOtherIni(simName, "AllowGods", pRegionClass.AllowGods(RegionNum))
            pMySetting.SetOtherIni(simName, "RegionGod", pRegionClass.RegionGod(RegionNum))
            pMySetting.SetOtherIni(simName, "ManagerGod", pRegionClass.ManagerGod(RegionNum))
            pMySetting.SetOtherIni(simName, "RegionSnapShot", pRegionClass.RegionSnapShot(RegionNum).ToString(Usa))
            pMySetting.SetOtherIni(simName, "Birds", pRegionClass.Birds(RegionNum))
            pMySetting.SetOtherIni(simName, "Tides", pRegionClass.Tides(RegionNum))
            pMySetting.SetOtherIni(simName, "Teleport", pRegionClass.Teleport(RegionNum))

            ' V2.31 upwards for smart start
            pMySetting.SetOtherIni(simName, "SmartStart", pRegionClass.SmartStart(RegionNum).ToString(Usa))

            pMySetting.SaveOtherINI()

            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            ' region.ini
            pMySetting.LoadOtherIni(pOpensimBinPath + "bin\Regions\" + pRegionClass.GroupName(RegionNum) + "\Opensim.ini", ";")

            If pRegionClass.MapType(RegionNum) = "Simple" Then
                pMySetting.SetOtherIni("Map", "GenerateMaptiles", "True")
                pMySetting.SetOtherIni("Map", "MapImageModule", "MapImageModule")  ' versus Warp3DImageModule
                pMySetting.SetOtherIni("Map", "TextureOnMapTile", "False")         ' versus True
                pMySetting.SetOtherIni("Map", "DrawPrimOnMapTile", "False")
                pMySetting.SetOtherIni("Map", "TexturePrims", "False")
                pMySetting.SetOtherIni("Map", "RenderMeshes", "False")
            ElseIf pRegionClass.MapType(RegionNum) = "Good" Then
                pMySetting.SetOtherIni(simName, "GenerateMaptiles", "True")
                pMySetting.SetOtherIni("Map", "MapImageModule", "Warp3DImageModule")  ' versus MapImageModule
                pMySetting.SetOtherIni("Map", "TextureOnMapTile", "False")         ' versus True
                pMySetting.SetOtherIni("Map", "DrawPrimOnMapTile", "False")
                pMySetting.SetOtherIni("Map", "TexturePrims", "False")
                pMySetting.SetOtherIni("Map", "RenderMeshes", "False")
            ElseIf pRegionClass.MapType(RegionNum) = "Better" Then
                pMySetting.SetOtherIni("Map", "GenerateMaptiles", "True")
                pMySetting.SetOtherIni("Map", "MapImageModule", "Warp3DImageModule")  ' versus MapImageModule
                pMySetting.SetOtherIni("Map", "TextureOnMapTile", "True")         ' versus True
                pMySetting.SetOtherIni("Map", "DrawPrimOnMapTile", "True")
                pMySetting.SetOtherIni("Map", "TexturePrims", "False")
                pMySetting.SetOtherIni("Map", "RenderMeshes", "False")
            ElseIf pRegionClass.MapType(RegionNum) = "Best" Then
                pMySetting.SetOtherIni("Map", "GenerateMaptiles", "True")
                pMySetting.SetOtherIni("Map", "MapImageModule", "Warp3DImageModule")  ' versus MapImageModule
                pMySetting.SetOtherIni("Map", "TextureOnMapTile", "True")      ' versus True
                pMySetting.SetOtherIni("Map", "DrawPrimOnMapTile", "True")
                pMySetting.SetOtherIni("Map", "TexturePrims", "True")
                pMySetting.SetOtherIni("Map", "RenderMeshes", "True")
            End If

            Select Case pRegionClass.Physics(RegionNum)
                Case "0"
                    pMySetting.SetOtherIni("Startup", "meshing", "ZeroMesher")
                    pMySetting.SetOtherIni("Startup", "physics", "basicphysics")
                    pMySetting.SetOtherIni("Startup", "UseSeparatePhysicsThread", "False")
                Case "1"
                    pMySetting.SetOtherIni("Startup", "meshing", "Meshmerizer")
                    pMySetting.SetOtherIni("Startup", "physics", "OpenDynamicsEngine")
                    pMySetting.SetOtherIni("Startup", "UseSeparatePhysicsThread", "False")
                Case "2"
                    pMySetting.SetOtherIni("Startup", "meshing", "Meshmerizer")
                    pMySetting.SetOtherIni("Startup", "physics", "BulletSim")
                    pMySetting.SetOtherIni("Startup", "UseSeparatePhysicsThread", "False")
                Case "3"
                    pMySetting.SetOtherIni("Startup", "meshing", "Meshmerizer")
                    pMySetting.SetOtherIni("Startup", "physics", "BulletSim")
                    pMySetting.SetOtherIni("Startup", "UseSeparatePhysicsThread", "True")
                Case "4"
                    pMySetting.SetOtherIni("Startup", "meshing", "ubODEMeshmerizer")
                    pMySetting.SetOtherIni("Startup", "physics", "ubODE")
                    pMySetting.SetOtherIni("Startup", "UseSeparatePhysicsThread", "False")
                Case "5"
                    pMySetting.SetOtherIni("Startup", "meshing", "Meshmerizer")
                    pMySetting.SetOtherIni("Startup", "physics", "ubODE")
                    pMySetting.SetOtherIni("Startup", "UseSeparatePhysicsThread", "False")
                Case Else
                    ' do nothing
            End Select

            If pRegionClass.AllowGods(RegionNum) = "True" Then
                pMySetting.SetOtherIni("Permissions", "allow_grid_gods", "True")
            Else
                pMySetting.SetOtherIni("Permissions", "allow_grid_gods", pMySetting.AllowGridGods.ToString(Usa))
            End If

            If pRegionClass.RegionGod(RegionNum) = "True" Then
                pMySetting.SetOtherIni("Permissions", "region_owner_is_god", "True")
            Else
                pMySetting.SetOtherIni("Permissions", "region_owner_is_god", pMySetting.RegionOwnerIsGod.ToString(Usa))
            End If

            If pRegionClass.ManagerGod(RegionNum) = "True" Then
                pMySetting.SetOtherIni("Permissions", "region_manager_is_god", "True")
            Else
                pMySetting.SetOtherIni("Permissions", "region_manager_is_god", pMySetting.RegionManagerIsGod.ToString(Usa))
            End If

            pMySetting.SetOtherIni("AutoLoadTeleport", "Enabled", pRegionClass.SmartStart(RegionNum).ToString(Usa))


            pMySetting.SaveOtherINI()



            If pMySetting.BirdsModuleStartup And pRegionClass.Birds(RegionNum) = "True" Then

                BirdData = BirdData & "[" + simName + "]" + vbCrLf &
            ";this Is the default And determines whether the module does anything" & vbCrLf &
            "BirdsModuleStartup = True" & vbCrLf & vbCrLf &
            ";set to false to disable the birds from appearing in this region" & vbCrLf &
            "BirdsEnabled = True" & vbCrLf & vbCrLf &
            ";which channel do we listen on for in world commands" & vbCrLf &
            "BirdsChatChannel = " + pMySetting.BirdsChatChannel.ToString(Usa) & vbCrLf & vbCrLf &
            ";the number of birds to flock" & vbCrLf &
            "BirdsFlockSize = " + pMySetting.BirdsFlockSize.ToString(Usa) & vbCrLf & vbCrLf &
            ";how far each bird can travel per update" & vbCrLf &
            "BirdsMaxSpeed = " + pMySetting.BirdsMaxSpeed.ToString(Usa) & vbCrLf & vbCrLf &
            ";the maximum acceleration allowed to the current velocity of the bird" & vbCrLf &
            "BirdsMaxForce = " + pMySetting.BirdsMaxForce.ToString(Usa) & vbCrLf & vbCrLf &
            ";max distance for other birds to be considered in the same flock as us" & vbCrLf &
            "BirdsNeighbourDistance = " + pMySetting.BirdsNeighbourDistance.ToString(Usa) & vbCrLf & vbCrLf &
            ";how far away from other birds we would Like To stay" & vbCrLf &
            "BirdsDesiredSeparation = " + pMySetting.BirdsDesiredSeparation.ToString(Usa) & vbCrLf & vbCrLf &
            ";how close To the edges Of things can we Get without being worried" & vbCrLf &
            "BirdsTolerance = " + pMySetting.BirdsTolerance.ToString(Usa) & vbCrLf & vbCrLf &
            ";how close To the edge Of a region can we Get?" & vbCrLf &
            "BirdsBorderSize = " + pMySetting.BirdsBorderSize.ToString(Usa) & vbCrLf & vbCrLf &
            ";how high are we allowed To flock" & vbCrLf &
            "BirdsMaxHeight = " + pMySetting.BirdsMaxHeight.ToString(Usa) & vbCrLf & vbCrLf &
            ";By Default the Module will create a flock Of plain wooden spheres," & vbCrLf &
            ";however this can be overridden To the name Of an existing prim that" & vbCrLf &
            ";needs To already exist In the scene - i.e. be rezzed In the region." & vbCrLf &
            "BirdsPrim = " + pMySetting.BirdsPrim & vbCrLf & vbCrLf &
            ";who Is allowed to send commands via chat Or script List of UUIDs Or ESTATE_OWNER Or ESTATE_MANAGER" & vbCrLf &
            ";Or everyone if Not specified" & vbCrLf &
            "BirdsAllowedControllers = ESTATE_OWNER, ESTATE_MANAGER" & vbCrLf & vbCrLf & vbCrLf

            End If

            If pMySetting.TideEnabled And pRegionClass.Tides(RegionNum) = "True" Then

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
    "TideHighWater = " & pMySetting.TideHighLevel() & vbCrLf &
    "TideLowWater = " & pMySetting.TideLowLevel() & vbCrLf &
    vbCrLf &
    ";; how long in seconds for a complete cycle time low->high->low" & vbCrLf &
    "TideCycleTime = " & pMySetting.CycleTime() & vbCrLf &
        vbCrLf &
    ";; provide tide information on the console?" & vbCrLf &
    "TideInfoDebug = " & pMySetting.TideInfoDebug.ToString(Usa) & vbCrLf &
        vbCrLf &
    ";; chat tide info to the whole region?" & vbCrLf &
    "TideInfoBroadcast = " & pMySetting.BroadcastTideInfo() & vbCrLf &
        vbCrLf &
    ";; which channel to region chat on for the full tide info" & vbCrLf &
    "TideInfoChannel = " & pMySetting.TideInfoChannel & vbCrLf &
    vbCrLf &
    ";; which channel to region chat on for just the tide level in metres" & vbCrLf &
    "TideLevelChannel = " & pMySetting.TideLevelChannel() & vbCrLf &
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

        Dim X = pRegionClass.FindRegionByName(regionname)
        pMySetting.LoadOtherIni(pRegionClass.RegionPath(X), ";")
        pMySetting.SetOtherIni(regionname, key, value)
        pMySetting.SaveOtherINI()

    End Sub

    Public Sub CheckDefaultPorts()
        Try
            If pMySetting.DiagnosticPort = pMySetting.HttpPort _
        Or pMySetting.DiagnosticPort = pMySetting.PrivatePort _
        Or pMySetting.HttpPort = pMySetting.PrivatePort Then
                pMySetting.DiagnosticPort = "8001"
                pMySetting.HttpPort = "8002"
                pMySetting.PrivatePort = "8003"

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
            .FileName = pMyFolder & "\UPnpPortForwardManager.exe",
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

        For Each X As Integer In pRegionClass.RegionNumbers
            pRegionClass.Status(X) = RegionMaker.SIMSTATUSENUM.Stopped
            pRegionClass.ProcessID(X) = 0
            pRegionClass.Timer(X) = RegionMaker.REGIONTIMER.Stopped
        Next

        pExitList.Clear()

    End Sub

    Public Sub ToolBar(visible As Boolean)

        Label3.Visible = visible
        AvatarLabel.Visible = visible
        PercentCPU.Visible = visible
        PercentRAM.Visible = visible

    End Sub

    Private Sub BusyButton_Click(sender As Object, e As EventArgs) Handles BusyButton.Click

        StopAllRegions()

        pUpdateView = True ' make form refresh
        ' cannot load OAR or IAR, either
        IslandToolStripMenuItem.Visible = False
        ClothingInventoryToolStripMenuItem.Visible = False
        Timer1.Stop()
        pOpensimIsRunning() = False
        Me.AllowDrop = False
        ProgressBar1.Value = 0
        ProgressBar1.Visible = False
        ToolBar(False)

        Print("Dreamgrid Stopped/Aborted")
        Buttons(StopButton)
        Timer1.Enabled = False
        pAborting = True

    End Sub

    Private Sub AdminUIToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ViewWebUI.Click
        If pOpensimIsRunning() Then
            If pMySetting.ApacheEnable Then
                Dim webAddress As String = "http://127.0.0.1:" + pMySetting.ApachePort
                Process.Start(webAddress)
            Else
                Dim webAddress As String = "http://127.0.0.1:" + pMySetting.HttpPort
                Process.Start(webAddress)
                Print("Log in as '" + pMySetting.AdminFirst + " " + pMySetting.AdminLast + "' with a password of " + pMySetting.Password + " to add user accounts.")
            End If
        Else
            If pMySetting.ApacheEnable Then
                Dim webAddress As String = "http://127.0.0.1:" + pMySetting.ApachePort
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

        Dim webAddress As String = pDomain + "/cgi/freesculpts.plx"
        Process.Start(webAddress)
        Print("Drag and drop Backup.Oar, or any OAR or IAR files to load into your Sim")

    End Sub

    Private Sub AdvancedSettingsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AdvancedSettingsToolStripMenuItem.Click

        If pInitted Then
            Adv.Activate()
            Adv.Visible = True
        End If

    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        Dim webAddress As String = pDomain() + "/Outworldz_Installer/PortForwarding.htm"
        Process.Start(webAddress)
    End Sub

#End Region

#Region "Apache"

    Public Sub StartApache()

        If Not pMySetting.ApacheEnable Then
            Print("Apache web server is not enabled.")
            Return
        End If

        Print("Setup Apache")
        ' Stop MSFT server if we are on port 80 and enabled

        If pMySetting.ApachePort = "80" Then
            ApacheProcess.StartInfo.UseShellExecute = True ' so we can redirect streams
            ApacheProcess.StartInfo.FileName = "net"
            ApacheProcess.StartInfo.CreateNoWindow = True
            ApacheProcess.StartInfo.Arguments = "stop W3SVC"
            ApacheProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
            ApacheProcess.Start()
            ApacheProcess.WaitForExit()
        End If

        Dim ApacheRunning = CheckPort(pMySetting.PrivateURL, CType(pMySetting.ApachePort, Integer))
        If ApacheRunning Then Return

        If pMySetting.ApacheService Then

            Print("Checking Apache service")
            Try
                Dim ApacheProcess As New Process With {
                    .EnableRaisingEvents = True
                }
                ApacheProcess.StartInfo.UseShellExecute = True ' so we can redirect streams
                ApacheProcess.StartInfo.FileName = pMyFolder + "\Outworldzfiles\Apache\bin\InstallApache.bat"
                ApacheProcess.StartInfo.CreateNoWindow = True
                ApacheProcess.StartInfo.WorkingDirectory = pMyFolder + "\Outworldzfiles\Apache\bin\"
                ApacheProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
                ApacheProcess.Start()
                ApacheProcess.WaitForExit()
                Dim code = ApacheProcess.ExitCode
                If code <> 0 Then
                    MsgBox("Apache failed to install and start as a service:" & code.ToString(Usa), vbInformation, "Error")
                End If
                Sleep(100)
                ApacheRunning = CheckPort(pMySetting.PublicIP, CType(pMySetting.ApachePort, Integer))
                If Not ApacheRunning Then
                    MsgBox("Apache installed but port " & pMySetting.ApachePort & " is not responding. Check your firewall and router port forward settings.", vbInformation, "Error")
                End If
            Catch ex As Exception
                Print("Install Apache error" & ex.Message)
            End Try
        Else

            ' Start Apache  manually
            Try
                Dim ApacheProcess2 As New Process With {
                    .EnableRaisingEvents = True
                }
                ApacheProcess2.StartInfo.UseShellExecute = True ' so we can redirect streams
                ApacheProcess2.StartInfo.FileName = pMyFolder + "\Outworldzfiles\Apache\bin\httpd.exe"
                ApacheProcess2.StartInfo.CreateNoWindow = True
                ApacheProcess2.StartInfo.WorkingDirectory = pMyFolder + "\Outworldzfiles\Apache\bin\"
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
            While Not IsApacheRunning() And pOpensimIsRunning
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
            Up = client.DownloadString("http://" & pMySetting.PublicIP & ":" & pMySetting.ApachePort + "/?_Opensim=" + Random())
        Catch ex As Exception
            If ex.Message.Contains("200 OK") Then Return True
            Return False
        End Try
        If Up.Length = 0 And pOpensimIsRunning() Then
            Return False
        End If

        Return True

    End Function

    Private Sub MapSetup()

        Dim phptext = "<?php" & vbCrLf &
"/* General Domain */" & vbCrLf &
"$CONF_domain        = " & """" & pMySetting.PublicIP & """" & "; " & vbCrLf &
"$CONF_port          = " & """" & pMySetting.HttpPort & """" & "; " & vbCrLf &
"$CONF_sim_domain    = " & """" & "http//" & pMySetting.PublicIP & "/" & """" & ";" & vbCrLf &
"$CONF_install_path  = " & """" & "/Metromap" & """" & ";   // Installation path " & vbCrLf &
"/* MySQL Database */ " & vbCrLf &
"$CONF_db_server     = " & """" & pMySetting.RobustServer & """" & "; // Address Of Robust Server " & vbCrLf &
"$CONF_db_user       = " & """" & pMySetting.RobustUsername & """" & ";  // login " & vbCrLf &
"$CONF_db_pass       = " & """" & pMySetting.RobustPassword & """" & ";  // password " & vbCrLf &
"$CONF_db_database   = " & """" & pMySetting.RobustDataBaseName & """" & ";     // Name Of Robust Server " & vbCrLf &
"/* The Coordinates Of the Grid-Center */ " & vbCrLf &
"$CONF_center_coord_x = " & """" & pMySetting.MapCenterX & """" & ";		// the Center-X-Coordinate " & vbCrLf &
"$CONF_center_coord_y = " & """" & pMySetting.MapCenterY & """" & ";		// the Center-Y-Coordinate " & vbCrLf &
"// style-sheet items" & vbCrLf &
"$CONF_style_sheet     = " & """" & "/css/stylesheet.css" & """" & ";          //Link To your StyleSheet" & vbCrLf &
"?>"

        Using outputFile As New StreamWriter(pMyFolder & "\OutworldzFiles\Apache\htdocs\MetroMap\includes\config.php", False)
            outputFile.WriteLine(phptext)
        End Using


        phptext = "<?php" & vbCrLf &
"$DB_HOST = " & """" & pMySetting.RobustServer & """" & ";" & vbCrLf &
"$DB_USER = " & """" & pMySetting.RobustUsername & """" & ";" & vbCrLf &
"$DB_PASSWORD = " & """" & pMySetting.RobustPassword & """" & ";" & vbCrLf &
"$DB_NAME = " & """" & "ossearch" & """" & ";" & vbCrLf &
"?>" & vbCrLf

        Using outputFile As New StreamWriter(pMyFolder & "\OutworldzFiles\Apache\htdocs\Search\databaseinfo.php", False)
            outputFile.WriteLine(phptext)
        End Using

    End Sub

    Private Sub KillApache()

        If Not pMySetting.ApacheEnable Then Return

        If pMySetting.ApacheService Then
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
                Print("Error Apache did Not stop" + ex.Message)
            End Try
        Else
            Zap("httpd")
            Zap("rotatelogs")
        End If

    End Sub

    Private Sub StopApache()

        If Not pMySetting.ApacheEnable Then Return

        Print("Stopping Apache ")

        If Not pMySetting.ApacheService Then
            Zap("httpd")
            Zap("rotatelogs")
        End If

    End Sub

    Private Sub DoPHP()

        Dim ini = pMyFolder & "\Outworldzfiles\PHP5\php.ini"
        pMySetting.LoadApacheIni(ini)
        pMySetting.SetApacheIni("extension_dir", " = """ & pCurSlashDir & "/OutworldzFiles/PHP5/ext""")
        pMySetting.SaveApacheINI(ini, "php.ini")

    End Sub

    Private Sub DoApache()

        If Not pMySetting.ApacheEnable Then Return

        ' lean rightward paths for Apache
        Dim ini = pMyFolder & "\Outworldzfiles\Apache\conf\httpd.conf"
        pMySetting.LoadApacheIni(ini)
        pMySetting.SetApacheIni("Listen", pMySetting.ApachePort)
        pMySetting.SetApacheIni("ServerRoot", """" & pCurSlashDir & "/Outworldzfiles/Apache" & """")
        pMySetting.SetApacheIni("DocumentRoot", """" & pCurSlashDir & "/Outworldzfiles/Apache/htdocs" & """")
        pMySetting.SetApacheIni("Use VDir", """" & pCurSlashDir & "/Outworldzfiles/Apache/htdocs" & """")
        pMySetting.SetApacheIni("PHPIniDir", """" & pCurSlashDir & "/Outworldzfiles/PHP5" & """")
        pMySetting.SetApacheIni("ServerName", pMySetting.PrivateURL)
        pMySetting.SetApacheIni("ErrorLog", """|bin/rotatelogs.exe  -l \" & """" & pCurSlashDir & "/Outworldzfiles/Apache/logs/Error-%Y-%m-%d.log" & "\" & """" & " 86400""")
        pMySetting.SetApacheIni("CustomLog", """|bin/rotatelogs.exe -l \" & """" & pCurSlashDir & "/Outworldzfiles/Apache/logs/access-%Y-%m-%d.log" & "\" & """" & " 86400""" & " common env=!dontlog""")
        pMySetting.SetApacheIni("LoadModule php5_module", """" & pCurSlashDir & "/Outworldzfiles/PHP5/php5apache2_4.dll" & """")
        pMySetting.SaveApacheINI(ini, "httpd.conf")

        ' lean rightward paths for Apache
        ini = pMyFolder & "\Outworldzfiles\Apache\conf\extra\httpd-ssl.conf"
        pMySetting.LoadApacheIni(ini)
        pMySetting.SetApacheIni("Listen", pMySetting.PrivateURL & ":" & "443")
        pMySetting.SetApacheIni("extension_dir", """" & pCurSlashDir & "/OutworldzFiles/PHP5/ext""")
        pMySetting.SetApacheIni("DocumentRoot", """" & pCurSlashDir & "/Outworldzfiles/Apache/htdocs""")
        pMySetting.SetApacheIni("ServerName", pMySetting.PublicIP)
        pMySetting.SetApacheIni("SSLSessionCache", "shmcb:""" & pCurSlashDir & "/Outworldzfiles/Apache/logs" & "/ssl_scache(512000)""")
        pMySetting.SaveApacheINI(ini, "httpd-ssl.conf")

    End Sub

#End Region

#Region "Icecast"

    Public Sub StartIcecast()

        If Not pMySetting.SCEnable Then
            Return
        End If

        Dim IceCastRunning = CheckPort(pMySetting.PublicIP, pMySetting.SCPortBase)
        If IceCastRunning Then Return

        Try
            My.Computer.FileSystem.DeleteFile(pMyFolder + "\Outworldzfiles\Icecast\log\access.log")
        Catch ex As Exception
        End Try

        Try
            My.Computer.FileSystem.DeleteFile(pMyFolder + "\Outworldzfiles\Icecast\log\error.log")
        Catch ex As Exception
        End Try

        pIcecastProcID = 0
        Print("Starting Icecast")

        Try
            IcecastProcess.EnableRaisingEvents = True
            IcecastProcess.StartInfo.UseShellExecute = True ' so we can redirect streams
            IcecastProcess.StartInfo.FileName = pMyFolder + "\Outworldzfiles\icecast\icecast.bat"
            IcecastProcess.StartInfo.CreateNoWindow = False
            IcecastProcess.StartInfo.WorkingDirectory = pMyFolder + "\Outworldzfiles\icecast"

            If pMySetting.ConsoleShow Then
                IcecastProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal
            Else
                IcecastProcess.StartInfo.WindowStyle = ProcessWindowStyle.Minimized
            End If

            'IcecastProcess.StartInfo.Arguments = "-c .\icecast_run.xml"
            IcecastProcess.Start()
            pIcecastProcID = IcecastProcess.Id

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

        If pMySetting.ServerType <> "Robust" Then Return True

        Print("Setup Robust")
        If pMySetting.RobustServer <> "127.0.0.1" And pMySetting.RobustServer <> "localhost" Then
            Print("Using Robust on " & pMySetting.RobustServer)
            Return True
        End If

        pRobustProcID = Nothing
        Print("Starting Robust")

        Try
            RobustProcess.EnableRaisingEvents = True
            RobustProcess.StartInfo.UseShellExecute = True ' so we can redirect streams
            RobustProcess.StartInfo.FileName = pOpensimBinPath + "bin\robust.exe"

            RobustProcess.StartInfo.CreateNoWindow = False
            RobustProcess.StartInfo.WorkingDirectory = pOpensimBinPath + "bin"
            RobustProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal
            RobustProcess.StartInfo.Arguments = "-inifile Robust.HG.ini"
            RobustProcess.Start()
            pRobustProcID = RobustProcess.Id

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
        While Not IsRobustRunning() And pOpensimIsRunning
            Application.DoEvents()
            BumpProgress(1)
            counter += 1
            ' wait a minute for it to start
            If counter > 100 Then
                Print("Error:Robust failed to start")
                Buttons(StartButton)
                Dim yesno = MsgBox("Robust did not start. Do you want to see the log file?", vbYesNo, "Error")
                If (yesno = vbYes) Then
                    Dim Log As String = """" + pOpensimBinPath + "bin\Robust.log" + """"
                    System.Diagnostics.Process.Start(pMyFolder + "\baretail.exe " & Log)
                End If
                Buttons(StartButton)
                Return False
            End If
            Application.DoEvents()
            Sleep(100)

        End While

        If pMySetting.ConsoleShow = False Then
            ShowDOSWindow(GetHwnd("Robust"), SHOWWINDOWENUM.SWMINIMIZE)
        End If

        Log("Info", "Robust is running")

        Return True

    End Function

#End Region

#Region "Opensimulator"

    Public Function StartOpensimulator() As Boolean

        pExitHandlerIsBusy = False
        pAborting = False
        Timer1.Start() 'Timer starts functioning

        RunDataSnapshot() ' Fetch assets marked for search every hour

        StartRobust()

        Dim Len = pRegionClass.RegionCount()
        Dim counter = 1
        ProgressBar1.Value = CType(counter / Len, Integer)
        Try
            ' Boot them up
            For Each x In pRegionClass.RegionNumbers()
                If pRegionClass.RegionEnabled(x) Then
                    Boot(pRegionClass, pRegionClass.RegionName(x))
                    ProgressBar1.Value = CType(counter / Len * 100, Integer)
                    counter += 1
                End If
            Next
        Catch ex As Exception
            Diagnostics.Debug.Print(ex.Message)
            Print("Unable to boot some regions")
        End Try

        pMySetting.DeleteScriptsOnStartupOnce() = False
        pMySetting.SaveSettings()

        Return True

    End Function

#End Region

#Region "Exited"

    ' Handle Exited event and display process information.
    Private Sub ApacheProcess_Exited(ByVal sender As Object, ByVal e As System.EventArgs) Handles ApacheProcess.Exited

        gApacheProcessID = Nothing

        If pAborting Then Return
        Dim yesno = MsgBox("Apache exited.", vbYesNo, "Error")

    End Sub

    ' Handle Exited event and display process information.
    Private Sub RobustProcess_Exited(ByVal sender As Object, ByVal e As System.EventArgs) Handles RobustProcess.Exited

        pRobustProcID = Nothing

        If pAborting Then Return
        Dim yesno = MsgBox("Robust exited. Do you want to see the error log file?", vbYesNo, "Error")
        If (yesno = vbYes) Then
            Dim MysqlLog As String = pOpensimBinPath + "bin\Robust.log"
            System.Diagnostics.Process.Start(pMyFolder + "\baretail.exe", """" & MysqlLog & """")
        End If

    End Sub

    Private Sub Mysql_Exited(ByVal sender As Object, ByVal e As System.EventArgs) Handles ProcessMySql.Exited

        If pAborting Then Return

        pOpensimIsRunning() = False

        Dim yesno = MsgBox("Mysql exited. Do you want to see the error log file?", vbYesNo, "Error")
        If (yesno = vbYes) Then
            Dim MysqlLog As String = pMyFolder + "\OutworldzFiles\mysql\data"
            Dim files() As String
            files = Directory.GetFiles(MysqlLog, "*.err", SearchOption.TopDirectoryOnly)
            For Each FileName As String In files
                System.Diagnostics.Process.Start(pMyFolder + "\baretail.exe", """" & FileName & """")
            Next
        End If
    End Sub

    Private Sub IceCast_Exited(ByVal sender As Object, ByVal e As System.EventArgs) Handles IcecastProcess.Exited

        If pAborting Then Return
        Dim yesno = MsgBox("Icecast quit. Do you want to see the error log file?", vbYesNo, "Error")
        If (yesno = vbYes) Then
            Dim IceCastLog As String = pMyFolder + "\Outworldzfiles\Icecast\log\error.log"
            System.Diagnostics.Process.Start(pMyFolder + "\baretail.exe", """" & IceCastLog & """")
        End If

    End Sub

#End Region

#Region "ExitHandlers"

    Private Sub ExitHandlerPoll()

        ' 10 Second ticker
        If pExitHandlerIsBusy Then Return
        If pAborting Then Return

        Dim GroupName As String
        Dim RegionNumber As Integer
        Dim TimerValue As Integer

        For Each X As Integer In pRegionClass.RegionNumbers
            'Application.DoEvents()
            ' count up to auto restart , when high enough, restart the sim
            If pRegionClass.Timer(X) >= 0 Then
                pRegionClass.Timer(X) = pRegionClass.Timer(X) + 1
            End If

            If pOpensimIsRunning() And Not pAborting And pRegionClass.Timer(X) >= 0 Then
                TimerValue = pRegionClass.Timer(X)
                ' if it is past time and no one is in the sim...
                GroupName = pRegionClass.GroupName(X)
                If (TimerValue / 6) >= (pMySetting.AutoRestartInterval()) And pMySetting.AutoRestartInterval() > 0 And Not AvatarsIsInGroup(GroupName) Then
                    ' shut down the group when one minute has gone by, or multiple thereof.
                    Try
                        If ShowDOSWindow(GetHwnd(GroupName), SHOWWINDOWENUM.SWRESTORE) Then
                            SequentialPause()
                            ConsoleCommand(pRegionClass.GroupName(X), "q{ENTER}" + vbCrLf)
                            Print("AutoRestarting " + GroupName)
                            ' shut down all regions in the DOS box
                            For Each Y In pRegionClass.RegionListByGroupNum(GroupName)
                                pRegionClass.Timer(Y) = RegionMaker.REGIONTIMER.Stopped
                                pRegionClass.Status(Y) = RegionMaker.SIMSTATUSENUM.RecyclingDown
                            Next
                        Else
                            ' shut down all regions in the DOS box
                            For Each Y In pRegionClass.RegionListByGroupNum(GroupName)
                                pRegionClass.Timer(Y) = RegionMaker.REGIONTIMER.Stopped
                                pRegionClass.Status(Y) = RegionMaker.SIMSTATUSENUM.Stopped
                            Next
                        End If
                        pUpdateView = True ' make form refresh
                    Catch ex As Exception
                        ErrorLog(ex.Message)
                        ' shut down all regions in the DOS box
                        For Each Y In pRegionClass.RegionListByGroupNum(GroupName)
                            pRegionClass.Timer(Y) = RegionMaker.REGIONTIMER.Stopped
                            pRegionClass.Status(Y) = RegionMaker.SIMSTATUSENUM.RecyclingDown
                        Next
                    End Try
                End If

            End If

            ' if a restart is signalled, boot it up
            If pRegionClass.Status(X) = RegionMaker.SIMSTATUSENUM.Autostart And Not pAborting Then
                pUpdateView = True
                Boot(pRegionClass, pRegionClass.RegionName(X))
                pUpdateView = True
            End If


            ' if a restart is signalled, boot it up
            If pRegionClass.Status(X) = RegionMaker.SIMSTATUSENUM.RestartPending And Not pAborting Then
                pUpdateView = True
                Boot(pRegionClass, pRegionClass.RegionName(X))
                pUpdateView = True
            End If

        Next

        pRestartNow = False
        If pExitList.Count = 0 Then Return
        If pExitHandlerIsBusy Then Return
        pExitHandlerIsBusy = True

        Dim RegionName = pExitList(0).ToString()
        pExitList.RemoveAt(0)

        Print(RegionName & " shutdown")
        Dim RegionList = pRegionClass.RegionListByGroupNum(RegionName)
        ' Need a region number and a Name
        ' name is either a region or a Group. For groups we need to get a region name from the group
        GroupName = RegionName ' assume a group
        RegionNumber = pRegionClass.FindRegionByName(RegionName)

        If RegionNumber >= 0 Then
            GroupName = pRegionClass.GroupName(RegionNumber) ' Yup, Get Name of the Dos box
        Else
            ' Nope, grab the first region, Group name is already set
            RegionNumber = RegionList(0)
        End If

        Dim Status = pRegionClass.Status(RegionNumber)
        TimerValue = pRegionClass.Timer(RegionNumber)

        'Auto restart phase begins
        If pOpensimIsRunning() And Status = RegionMaker.SIMSTATUSENUM.RecyclingDown Then
            Print("Restart Queued for " + GroupName)
            For Each R In RegionList
                pRegionClass.Status(R) = RegionMaker.SIMSTATUSENUM.RestartPending
            Next
            pUpdateView = True ' make form refresh
        End If
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ' Maybe we crashed during warmup or runniung.  Skip prompt if auto restart on crash and restart the beast
        If (Status = RegionMaker.SIMSTATUSENUM.RecyclingUp _
            Or Status = RegionMaker.SIMSTATUSENUM.Booting) _
            Or pRegionClass.IsBooted(RegionNumber) _
            And TimerValue >= 0 Then

            If pMySetting.RestartOnCrash Then
                pUpdateView = True
                ' shut down all regions in the DOS box
                Print("DOS Box " + GroupName + " quit unexpectedly.  Restarting now...")
                For Each Y In pRegionClass.RegionListByGroupNum(GroupName)
                    pRegionClass.Timer(Y) = RegionMaker.REGIONTIMER.Stopped
                    pRegionClass.Status(Y) = RegionMaker.SIMSTATUSENUM.RestartPending
                Next
            Else
                Dim yesno = MsgBox("DOS Box " + GroupName + " quit unexpectedly. Do you want to see the log file?", vbYesNo, "Error")
                If (yesno = vbYes) Then
                    System.Diagnostics.Process.Start(pMyFolder + "\baretail.exe", """" & pRegionClass.IniPath(RegionNumber) + "Opensim.log" & """")
                End If
                StopGroup(GroupName)
                pUpdateView = True
            End If

        End If

        If Status = RegionMaker.SIMSTATUSENUM.ShuttingDown Then
            pRegionClass.Status(RegionNumber) = RegionMaker.SIMSTATUSENUM.Stopped
            pUpdateView = True
        End If


        pExitHandlerIsBusy = False

    End Sub

    Public Sub StopGroup(Groupname As String)

        For Each RegionNumber In pRegionClass.RegionListByGroupNum(Groupname)
            ' Called by a sim restart, do not change status 
            'If Not pRegionClass.Status(RegionNumber) = RegionMaker.SIMSTATUSENUM.RecyclingDown Then
            pRegionClass.Status(RegionNumber) = RegionMaker.SIMSTATUSENUM.Stopped
            Log("Info", pRegionClass.RegionName(RegionNumber) + " Stopped")
            'End If
            pRegionClass.Timer(RegionNumber) = RegionMaker.REGIONTIMER.Stopped
        Next
        Log("Info", Groupname + " Group is now stopped")
        pUpdateView = True ' make form refresh

    End Sub

    Public Sub ForceStopGroup(Groupname As String)

        For Each RegionNumber In pRegionClass.RegionListByGroupNum(Groupname)

            ' Called by a sim restart, do not change status
            'If Not pRegionClass.Status(RegionNumber) = RegionMaker.SIMSTATUSENUM.RecyclingDown Then
            pRegionClass.Status(RegionNumber) = RegionMaker.SIMSTATUSENUM.Stopped
            Log("Info", pRegionClass.RegionName(RegionNumber) + " Stopped")
            ' End If

            pRegionClass.Timer(RegionNumber) = RegionMaker.REGIONTIMER.Stopped
        Next
        Log("Info", Groupname + " Group is now stopped")
        pUpdateView = True ' make form refresh

    End Sub

    ''' <summary>
    ''' Creates and exit handler for each region
    ''' </summary>
    ''' <returns>a process handle</returns>
    Public Function GetNewProcess() As Process

        Dim handle = New Handler
        Return handle.Init(pRegionHandles, pExitList)

    End Function

    ''' <summary>
    ''' Starts Opensim for a given name
    ''' </summary>
    ''' <param name="BootName"> Name of region to start</param>
    ''' <returns>success = true</returns>
    Public Function Boot(Regionclass As RegionMaker, BootName As String, Optional UserAgent As String = "") As Boolean

        If pAborting Then Return True

        pOpensimIsRunning() = True

        Buttons(StopButton)

        Log("Info", "Region: Starting Region " + BootName)

        Dim RegionNumber = pRegionClass.FindRegionByName(BootName)
        If pRegionClass.IsBooted(RegionNumber) Then
            Log("Info", "Region " + BootName + " skipped as it is already Booted")
            Return True
        End If

        If pRegionClass.Status(RegionNumber) = RegionMaker.SIMSTATUSENUM.RecyclingUp Then
            Log("Info", "Region " + BootName + " skipped as it is already Warming Up")
            Return True
        End If

        If pRegionClass.Status(RegionNumber) = RegionMaker.SIMSTATUSENUM.Booting Then
            Log("Info", "Region " + BootName + " skipped as it is already Booted Up")
            Return True
        End If

        If pRegionClass.Status(RegionNumber) = RegionMaker.SIMSTATUSENUM.ShuttingDown Then
            Log("Info", "Region " + BootName + " skipped as it is already Shutting Down")
            Return True
        End If

        If pRegionClass.Status(RegionNumber) = RegionMaker.SIMSTATUSENUM.RecyclingDown Then
            Log("Info", "Region " + BootName + " skipped as it is already Recycling Down")
            Return True
        End If

        Application.DoEvents()
        Dim isRegionRunning = CheckPort("127.0.0.1", pRegionClass.GroupPort(RegionNumber))
        If isRegionRunning Then
            Log("Info", "Region " + BootName + " failed to start as it is already running")
            pRegionClass.Status(RegionNumber) = RegionMaker.SIMSTATUSENUM.Booted ' force it up
            Return False
        End If

        Environment.SetEnvironmentVariable("OSIM_LOGPATH", pMySetting.OpensimBinPath() + "bin\Regions\" + pRegionClass.GroupName(RegionNumber))

        Dim myProcess As Process = GetNewProcess()
        Dim Groupname = pRegionClass.GroupName(RegionNumber)
        Print("Starting " + Groupname)
        Try
            myProcess.EnableRaisingEvents = True
            myProcess.StartInfo.UseShellExecute = True ' so we can redirect streams
            myProcess.StartInfo.WorkingDirectory = pMySetting.OpensimBinPath() + "bin"

            myProcess.StartInfo.FileName = """" + pMySetting.OpensimBinPath() + "bin\OpenSim.exe" + """"
            myProcess.StartInfo.CreateNoWindow = False
            myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal
            myProcess.StartInfo.Arguments = " -inidirectory=" & """" & "./Regions/" & pRegionClass.GroupName(RegionNumber) + """"

            Try
                My.Computer.FileSystem.DeleteFile(pMySetting.OpensimBinPath() + "bin\Regions\" & pRegionClass.GroupName(RegionNumber) & "\Opensim.log")
            Catch
            End Try

            Try
                My.Computer.FileSystem.DeleteFile(pMySetting.OpensimBinPath() + "bin\Regions\" & pRegionClass.GroupName(RegionNumber) & "\PID.pid")
            Catch
            End Try

            Try
                My.Computer.FileSystem.DeleteFile(pMySetting.OpensimBinPath() + "bin\regions\" & pRegionClass.GroupName(RegionNumber) & "\OpensimConsole.log")
            Catch ex As Exception
            End Try

            Try
                My.Computer.FileSystem.DeleteFile(pMySetting.OpensimBinPath() + "bin\regions\" & pRegionClass.GroupName(RegionNumber) & "\OpenSimStats.log")
            Catch ex As Exception
            End Try

            If UserAgent.Length = 0 Then SequentialPause()

            If myProcess.Start() Then
                For Each num In pRegionClass.RegionListByGroupNum(Groupname)
                    Log("Debug", "Process started for " + pRegionClass.RegionName(num) + " PID=" + myProcess.Id.ToString(Usa) + " Num:" + num.ToString(Usa))
                    pRegionClass.Status(num) = RegionMaker.SIMSTATUSENUM.Booting
                    pRegionClass.ProcessID(num) = myProcess.Id
                    pRegionClass.Timer(num) = RegionMaker.REGIONTIMER.StartCounting
                Next

                pUpdateView = True ' make form refresh
                Application.DoEvents()
                SetWindowTextCall(myProcess, pRegionClass.GroupName(RegionNumber))

                Log("Debug", "Created Process Number " + myProcess.Id.ToString(Usa) + " in  RegionHandles(" + pRegionHandles.Count.ToString(Usa) + ") " + "Group:" + Groupname)
                pRegionHandles.Add(myProcess.Id, Groupname) ' save in the list of exit events in case it crashes or exits

                If UserAgent.Length > 0 Then
                    TeleportAgent(UserAgent)
                End If

            End If

            Return True

        Catch ex As Exception
            If ex.Message.Contains("Process has exited") Then Return False
            Print("Oops! " + BootName + " did Not start:" & ex.Message)
            ErrorLog(ex.Message)
            pUpdateView = True ' make form refresh
            Dim yesno = MsgBox("Oops! " + BootName + " in DOS box " + Groupname + " did not boot. Do you want to see the log file?", vbYesNo, "Error")
            If (yesno = vbYes) Then
                System.Diagnostics.Process.Start(pMyFolder + "\baretail.exe", """" & pRegionClass.IniPath(RegionNumber) + "Opensim.log" & """")
            End If

            Return False
        End Try

        Return False

    End Function

    Private Sub TeleportAgent(agentUUID As String)
        '!!!
    End Sub

    ''' <summary>
    ''' Check is Robust port 8002 is up
    ''' </summary>
    ''' <returns>boolean</returns>
    Private Function IsRobustRunning() As Boolean

        Dim Up As String
        Try
            Up = client.DownloadString("http://" & pMySetting.RobustServer & ":" & pMySetting.HttpPort + "/?_Opensim=" + Random())
        Catch ex As Exception
            If ex.Message.Contains("404") Then Return True
            Return False
        End Try
        If Up.Length = 0 And pOpensimIsRunning() Then
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
        pMyFolder + "\OutworldzFiles\Error.log",
        pMyFolder + "\OutworldzFiles\Outworldz.log",
        pMyFolder + "\OutworldzFiles\Opensim\bin\OpenSimConsoleHistory.txt",
        pMyFolder + "\OutworldzFiles\Diagnostics.log",
        pMyFolder + "\OutworldzFiles\UPnp.log",
        pMyFolder + "\OutworldzFiles\Opensim\bin\Robust.log",
        pMyFolder + "\OutworldzFiles\http.log",
        pMyFolder + "\OutworldzFiles\PHPLog.log",
        pMyFolder + "\http.log"      ' an old mistake
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
            Using outputFile As New StreamWriter(pMyFolder & "\OutworldzFiles\" + file + ".log", True)
                outputFile.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", Usa) + ":" + category & ":" & message)
                Diagnostics.Debug.Print(message)
            End Using
        Catch
        End Try
    End Sub

    ''' <summary>
    ''' Shows the log buttons if diags fail
    ''' </summary>
    Private Sub ShowLog()

        System.Diagnostics.Process.Start(pMyFolder + "\baretail.exe", """" + pMyFolder + "\OutworldzFiles\Outworldz.log" + """")

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

        Dim Regionlist = pRegionClass.RegionListByGroupNum(Groupname)

        For Each X In Regionlist
            Dim pid = pRegionClass.ProcessID(X)

            Dim ctr = 20   ' 2 seconds
            Dim found As Boolean = False
            While Not found And ctr > 0
                Sleep(100)

                For Each pList As Process In Process.GetProcesses()
                    If pList.Id = pid Then
                        Return pList.MainWindowHandle
                    End If
                    Application.DoEvents()
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
            Dim ID = pRegionClass.FindRegionByName(name)
            Dim PID = pRegionClass.ProcessID(ID)
            Try
                If PID > 0 Then ShowDOSWindow(Process.GetProcessById(PID).MainWindowHandle, SHOWWINDOWENUM.SWRESTORE)
            Catch ex As Exception
                Diagnostics.Debug.Print("Catch:" & ex.Message)
            End Try
        Else
            Try
                ShowDOSWindow(Process.GetProcessById(pRobustProcID).MainWindowHandle, SHOWWINDOWENUM.SWRESTORE)
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
            'pRegionClass.RegionDump()
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

            MyCPUCollection.Remove(1) ' drop 1st, older  item
            MyCPUCollection.Add(newspeed)
            PercentCPU.Text = String.Format(Usa, "{0: 0}% CPU", newspeed)

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
        searcher.Dispose()

        Try
            For Each result In results
                Dim value = ((result("TotalVisibleMemorySize") - result("FreePhysicalMemory")) / result("TotalVisibleMemorySize")) * 100
                MyRAMCollection.Add(value)
                PercentRAM.Text = CType(value, Integer).ToString(Usa) & "% RAM"
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

        If Not pOpensimIsRunning() Then
            Timer1.Stop()
            Return
        End If
        pDNSSTimer += 1

        ' hourly
        If pDNSSTimer Mod 3600 = 0 Or pDNSSTimer = 1 Then
            RegisterDNS()
            GetEvents() ' get the events from the Outworldz main server for all grids
        End If

        If pAborting Then Return

        ' 10 seconds check for a restart
        ' RegionRestart requires this MOD 10 as it changed there to one minute
        If pDNSSTimer Mod 10 = 0 Then
            pRegionClass.CheckPost()
            ScanAgents() ' update agent count
            ExitHandlerPoll() ' see if any regions have exited and set it up for Region Restart
        End If

        If pDNSSTimer Mod 60 = 0 Then
            RegionListHTML() ' create HTML for region teleporters
            LogSearch.Find()
        End If

        If pDNSSTimer Mod 3600 = 0 Then
            RunDataSnapshot() ' Fetch assets marked for search every hour
        End If

    End Sub

    '' makes a list of teleports for the prims to use
    Private Sub RegionListHTML()

        'http://localhost:8002/bin/data/teleports.htm
        'Outworldz|Welcome||www.outworldz.com:9000:Welcome|128,128,96|
        '*|Welcome||www.outworldz.com9000Welcome|128,128,96|
        Dim HTML As String
        Dim HTMLFILE = pOpensimBinPath & "bin\data\teleports.htm"
        HTML = "Welcome to |" + pMySetting.SimName + "||" + pMySetting.PublicIP + ":" + pMySetting.HttpPort + ":" + pMySetting.WelcomeRegion + "||" + vbCrLf


        Dim NewSQLConn As New MySqlConnection(pRobustConnStr())
        Dim UserStmt = "SELECT regionName from REGIONS"

        Dim ToSort As New List(Of String)
        Try
            NewSQLConn.Open()
            Dim cmd As MySqlCommand = New MySqlCommand(UserStmt, NewSQLConn)
            Dim reader As MySqlDataReader = cmd.ExecuteReader()

            While reader.Read()
                Dim LongName = reader.GetString(0)

                Diagnostics.Debug.Print("regionname {0}>", LongName)
                ToSort.Add(LongName)
            End While
        Catch ex As MySqlException
            Console.WriteLine("Error: " & ex.ToString())
        Finally
            NewSQLConn.Close()
        End Try

        ' Acquire keys And sort them.
        ToSort.Sort()

        For Each S As String In ToSort
            HTML = HTML + "*|" & S & "||" & pMySetting.PublicIP & ":" & pMySetting.HttpPort & ":" & S & "||" + vbCrLf
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
        For Each RegionNum As Integer In pRegionClass.RegionListByGroupNum(groupname)
            present += pRegionClass.AvatarCount(RegionNum)
        Next

        Return CType(present, Boolean)

    End Function

    Private Sub ShowHyperGridAddressToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowHyperGridAddressToolStripMenuItem.Click

        Print("Grid address is " + vbCrLf + "http://" + pMySetting.PublicIP + ":" + pMySetting.HttpPort)

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

        If pOpensimIsRunning() Then

            Dim chosen = ChooseRegion(True)
            If chosen.Length = 0 Then Return
            Dim RegionNumber As Integer = pRegionClass.FindRegionByName(chosen)

            Dim Message, title, defaultValue As String
            Dim myValue As String
            ' Set prompt.
            Message = "Enter a name for your backup:"
            title = "Backup to OAR"
            defaultValue = chosen + "_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss", Usa) + ".oar"

            ' Display message, title, and default value.
            myValue = InputBox(Message, title, defaultValue)
            ' If user has clicked Cancel, set myValue to defaultValue
            If myValue.Length = 0 Then Return

            If pRegionClass.IsBooted(RegionNumber) Then
                Dim Group = pRegionClass.GroupName(RegionNumber)
                ConsoleCommand(pRegionClass.GroupName(RegionNumber), "alert CPU Intensive Backup Started{ENTER}" + vbCrLf)
                ConsoleCommand(pRegionClass.GroupName(RegionNumber), "change region " + """" + chosen + """" + "{ENTER}" + vbCrLf)
                ConsoleCommand(pRegionClass.GroupName(RegionNumber), "save oar " + """" + BackupPath() + myValue + """" + "{ENTER}" + vbCrLf)
            End If
            Me.Focus()
            Print("Saving " + myValue + " to " + BackupPath())
        Else
            Print("Opensim is not running. Cannot make a backup now.")
        End If

    End Sub

    Private Sub LoadRegionOarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoadRegionOarToolStripMenuItem.Click

        If pOpensimIsRunning() Then
            Dim chosen = ChooseRegion(True)
            If chosen.Length = 0 Then Return
            Dim RegionNumber As Integer = pRegionClass.FindRegionByName(chosen)

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

                    Dim Group = pRegionClass.GroupName(RegionNumber)
                    For Each Y In pRegionClass.RegionListByGroupNum(Group)

                        ConsoleCommand(pRegionClass.GroupName(Y), "change region " + chosen + "{ENTER}" + vbCrLf)
                        If backMeUp = vbYes Then
                            ConsoleCommand(pRegionClass.GroupName(Y), "alert CPU Intensive Backup Started{ENTER}" + vbCrLf)
                            ConsoleCommand(pRegionClass.GroupName(Y), "save oar  " + """" + BackupPath() + "Backup_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss", Usa) + ".oar" + """" + "{ENTER}" + vbCrLf)
                        End If
                        ConsoleCommand(pRegionClass.GroupName(Y), "alert New content Is loading..{ENTER}" + vbCrLf)

                        Dim ForceParcel As String = ""
                        If pForceParcel() Then ForceParcel = " --force-parcels "
                        Dim ForceTerrain As String = ""
                        If pForceTerrain Then ForceTerrain = " --force-terrain "
                        Dim ForceMerge As String = ""
                        If pForceMerge Then ForceMerge = " --merge "
                        Dim UserName As String = ""
                        If pUserName.Length > 0 Then UserName = " --default-user " & """" & pUserName & """" & " "

                        ConsoleCommand(pRegionClass.GroupName(Y), "load oar " & UserName & ForceMerge & ForceTerrain & ForceParcel & offset & """" & thing & """" & "{ENTER}" & vbCrLf)
                        ConsoleCommand(pRegionClass.GroupName(Y), "alert New content just loaded." + "{ENTER}" + vbCrLf)

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
        If pMySetting.BackupFolder = "AutoBackup" Then
            BackupPath = pCurSlashDir + "/OutworldzFiles/AutoBackup/"
            If Not Directory.Exists(BackupPath) Then
                MkDir(BackupPath)
            End If
        Else
            BackupPath = pMySetting.BackupFolder + "/"
            BackupPath = BackupPath.Replace("\", "/")    ' because Opensim uses unix-like slashes, that's why

            If Not Directory.Exists(BackupPath) Then

                BackupPath = pCurSlashDir + "/OutworldzFiles/Autobackup/"

                If Not Directory.Exists(BackupPath) Then
                    MkDir(BackupPath)
                End If

                MsgBox("Autoback folder cannot be located, so It has been reset to the default:" + BackupPath)
                pMySetting.BackupFolder = "AutoBackup"
                pMySetting.SaveSettings()
            End If
        End If

    End Function

    Private Sub AllRegionsOARsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AllTheRegionsOarsToolStripMenuItem.Click

        If Not pOpensimIsRunning() Then
            Print("Opensim Is Not running. Cannot save an OAR at this time.")
            Return
        End If

        Dim n As Integer = 0
        Dim L As New List(Of String)

        For Each RegionNumber In pRegionClass.RegionNumbers
            If pRegionClass.IsBooted(RegionNumber) Then
                Dim Group = pRegionClass.GroupName(RegionNumber)
                For Each Y In pRegionClass.RegionListByGroupNum(Group)
                    If Not L.Contains(pRegionClass.RegionName(Y)) Then
                        ConsoleCommand(pRegionClass.GroupName(RegionNumber), "change region " + """" + pRegionClass.RegionName(Y) + """" + "{ENTER}" + vbCrLf)
                        ConsoleCommand(pRegionClass.GroupName(RegionNumber), "save oar  " + """" + BackupPath() + pRegionClass.RegionName(Y) + "_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss", Usa) + ".oar" + """" + "{ENTER}" + vbCrLf)
                        L.Add(pRegionClass.RegionName(Y))
                    End If
                Next
            End If
            n += 1
        Next

    End Sub

    Private Sub LoadInventoryIARToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoadInventoryIARToolStripMenuItem.Click

        If pOpensimIsRunning() Then
            ' Create an instance of the open file dialog box.
            ' Set filter options and filter index.
            Dim openFileDialog1 As OpenFileDialog = New OpenFileDialog With {
                .InitialDirectory = """" + pMyFolder + "/" + """",
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

        If pOpensimIsRunning() Then

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
                If Not BackupName.ToLower(Usa).EndsWith(".iar") Then
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
                For Each RegionNumber As Integer In pRegionClass.RegionNumbers
                    Dim GName = pRegionClass.GroupName(RegionNumber)
                    Dim RNUm = pRegionClass.FindRegionByName(GName)
                    If pRegionClass.IsBooted(RegionNumber) And Not flag Then
                        ConsoleCommand(pRegionClass.GroupName(RegionNumber), "save iar " _
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

        Dim RegionNumber = pRegionClass.FindRegionByName(RegionName)
        Dim size = pRegionClass.SizeX(RegionNumber)
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

        Return pSelectedBox

    End Function

    Private Function LoadOARContent(thing As String) As Boolean

        If Not pOpensimIsRunning() Then
            Print("Opensim has to be started to load an OAR file.")
            Return False
        End If

        Dim region = ChooseRegion(True)
        If region.Length = 0 Then Return False

        Dim offset = VarChooser(region)
        If offset.Length = 0 Then Return False

        Dim backMeUp = MsgBox("Make a backup first?", vbYesNo, "Backup?")
        Dim num = pRegionClass.FindRegionByName(region)
        If num < 0 Then
            MsgBox("Cannot find region")
            Return False
        End If
        Dim GroupName = pRegionClass.GroupName(num)
        Dim once As Boolean = False
        For Each Y In pRegionClass.RegionListByGroupNum(GroupName)
            Try
                If Not once Then
                    Print("Opensimulator will load " + thing + ". This may take some time.")
                    thing = thing.Replace("\", "/")    ' because Opensim uses unix-like slashes, that's why

                    ConsoleCommand(pRegionClass.GroupName(Y), "change region " + region + "{ENTER}" + vbCrLf)
                    If backMeUp = vbYes Then
                        ConsoleCommand(pRegionClass.GroupName(Y), "alert CPU Intensive Backup Started {ENTER}" + vbCrLf)
                        ConsoleCommand(pRegionClass.GroupName(Y), "save oar " + BackupPath() + "Backup_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss", Usa) + ".oar" + """" + "{ENTER}" + vbCrLf)
                    End If
                    ConsoleCommand(pRegionClass.GroupName(Y), "alert New content Is loading ...{ENTER}" + vbCrLf)

                    Dim ForceParcel As String = ""
                    If pForceParcel() Then ForceParcel = " --force-parcels "
                    Dim ForceTerrain As String = ""
                    If pForceTerrain Then ForceTerrain = " --force-terrain "
                    Dim ForceMerge As String = ""
                    If pForceMerge Then ForceMerge = " --merge "
                    Dim UserName As String = ""
                    If pUserName.Length > 0 Then UserName = " --default-user " & """" & pUserName & """" & " "

                    ConsoleCommand(pRegionClass.GroupName(Y), "load oar " & UserName & ForceMerge & ForceTerrain & ForceParcel & offset & """" & thing & """" & "{ENTER}" & vbCrLf)
                    ConsoleCommand(pRegionClass.GroupName(Y), "alert New content just loaded. {ENTER}" + vbCrLf)
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

        If Not pOpensimIsRunning() Then
            Print("Opensim is not running. Cannot load an IAR at this time.")
            Return False
        End If

        Dim num As Integer = -1

        ' find one that is running
        For Each RegionNum In pRegionClass.RegionNumbers
            If pRegionClass.IsBooted(RegionNum) Then
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
            ConsoleCommand(pRegionClass.GroupName(num), "load iar --merge " & user & " " & Path & " " & password & " " & """" & thing & """" & "{ENTER}" & vbCrLf)
            ConsoleCommand(pRegionClass.GroupName(num), "alert IAR content Is loaded{ENTER}" + vbCrLf)
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
            If extension.ToLower(Usa) = "iar" Then
                If LoadIARContent(pathname) Then
                    Print("Opensimulator will load " + pathname + ".  This may take time to load.")
                End If
            ElseIf extension.ToLower(Usa) = "oar" Or extension.ToLower(Usa) = "gz" Or extension.ToLower(Usa) = "tgz" Then
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
            oars = client.DownloadString(pDomain() & "/Outworldz_Installer/Content.plx?type=OAR&r=" & Random())
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
                pContentAvailable = True
            Else
                ContentSeen = True
            End If
        End While
        BumpProgress10()

        ' read help files for menu
        line = ""
        Dim folders() = IO.Directory.GetFiles(pMyFolder & "\Outworldzfiles\Help")

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
            iars = client.DownloadString(pDomain() + "/Outworldz_Installer/Content.plx?type=IAR&r=" + Random())
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
                pContentAvailable = True
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
        For Each X In pRegionClass.RegionNumbers
            Dim Name = pRegionClass.RegionName(X)
            AddLog("Region " & Name)
        Next

        BumpProgress10()

    End Sub

    ''' <summary>
    ''' Upload in a seperate thraed the photo, if any.  Cannot be called unless main web server is known to be online.
    ''' </summary>
    Private Sub UploadPhoto()

        If System.IO.File.Exists(pMyFolder & "\OutworldzFiles\Photo.png") Then
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
        File = pDomain() + "/Outworldz_Installer/OAR/" + File 'make a real URL
        LoadOARContent(File)
        sender.checked = True

    End Sub

    Private Sub IarClick(sender As Object, e As EventArgs)

        Dim file As String = Mid(CType(sender.text, String), 1, InStr(CType(sender.text, String), "|") - 2)
        file = pDomain() + "/Outworldz_Installer/IAR/" + file 'make a real URL
        If LoadIARContent(file) Then
            Print("Opensimulator will load " + file + ".  This may take time to load.")
        End If
        sender.checked = True

    End Sub

#End Region

#Region "Updates"

    Private Function MakeBackup() As Boolean

        Dim Foldername = "Full_backup" + "_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss", Usa)   ' Set default folder
        Dim Dest As String
        If pMySetting.BackupFolder = "AutoBackup" Then
            Dest = pMyFolder + "\OutworldzFiles\AutoBackup\" + Foldername
        Else
            Dest = pMySetting.BackupFolder + "\" + Foldername
        End If
        Print("Making a backup at " + Dest)
        My.Computer.FileSystem.CreateDirectory(Dest)
        My.Computer.FileSystem.CreateDirectory(Dest + "\Opensim_bin_Regions")
        My.Computer.FileSystem.CreateDirectory(Dest + "\Mysql_Data")
        My.Computer.FileSystem.CreateDirectory(Dest + "\Opensim_WifiPages-Custom")
        My.Computer.FileSystem.CreateDirectory(Dest + "\Opensim_bin_WifiPages-Custom")

        Print("Backing up Regions Folder")
        Try
            My.Computer.FileSystem.CopyDirectory(pMyFolder + "\OutworldzFiles\Opensim\bin\Regions", Dest + "\Opensim_bin_Regions")
            Print("Backing up MySql\Data Folder")
            My.Computer.FileSystem.CopyDirectory(pMyFolder + "\OutworldzFiles\Mysql\Data\", Dest + "\Mysql_Data")
            Print("Backing up Wifi Folders")
            My.Computer.FileSystem.CopyDirectory(pMyFolder + "\OutworldzFiles\Opensim\WifiPages\", Dest + "\Opensim_WifiPages-Custom")
            My.Computer.FileSystem.CopyDirectory(pMyFolder + "\OutworldzFiles\Opensim\bin\WifiPages\", Dest + "\Opensim_bin_WifiPages-Custom")
            My.Computer.FileSystem.CopyFile(pMyFolder + "\OutworldzFiles\Settings.ini", Dest + "\Settings.ini")
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
                .FileName = """" + pMyFolder + "\" + fileloaded + """"
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
            My.Computer.FileSystem.DeleteFile(pMyFolder + "\" + fileName)
        Catch
            Log("Error", " Could Not delete " + pMyFolder + "\" + fileName)
        End Try

        Try
            fileName = client.DownloadString(pDomain() + "/Outworldz_Installer/GetUpdaterGrid.plx?fill=1" + GetPostData())
        Catch
            MsgBox("Could Not fetch an update. Please Try again, later", vbInformation, "Info")
            Return ""
        End Try

        Try
            Dim myWebClient As New WebClient()
            Print("Downloading New updater, this will take a moment")
            ' The DownloadFile() method downloads the Web resource and saves it into the current file-system folder.
            myWebClient.DownloadFile(pDomain() + "/Outworldz_Installer/" + fileName, fileName)
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
            Update = client.DownloadString(pDomain() + "/Outworldz_Installer/UpdateGrid.plx?fill=1" + GetPostData())
        Catch ex As Exception
            ErrorLog("Dang:The Outworldz web site is down")
        End Try
        If (Update.Length = 0) Then Update = "0"

        Try
            If Convert.ToSingle(Update, Usa) > Convert.ToSingle(pMyVersion, Usa) Then
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
            Log("Info", " port probe success on port " + Port.ToString(Usa))
            ClientSocket.Close()
            Return True
        End If
        CheckPort = False

    End Function

    Public Function SetPublicIP() As Boolean

        ' LAN USE
        If pMySetting.EnableHypergrid Then
            BumpProgress10()
            If pMySetting.DNSName.Length > 0 Then
                pMySetting.PublicIP = pMySetting.DNSName()
                pMySetting.SaveSettings()
                Return True
            Else

#Disable Warning BC42025 ' Access of shared member, constant member, enum member or nested type through an instance
                pMySetting.PublicIP = pMyUPnpMap.LocalIP
#Enable Warning BC42025 ' Access of shared member, constant member, enum member or nested type through an instance

                pMySetting.SaveSettings()
                Return False
            End If

        End If

        '  HG USE
        If Not IsPrivateIP(pMySetting.DNSName) Then
            BumpProgress10()
            pMySetting.PublicIP = pMySetting.DNSName
            pMySetting.SaveSettings()

            If pMySetting.PublicIP.ToLower(Usa).Contains("outworldz.net") Then
                Print("Registering DynDNS address http://" + pMySetting.PublicIP + ":" + pMySetting.HttpPort)
            End If

            If RegisterDNS() Then
                Return True
            End If

        End If

        If pMySetting.PublicIP = "localhost" Or pMySetting.PublicIP = "127.0.0.1" Then
            Return True
        End If

        'V 2.44

        Try
            Log("Info", "Public IP=" + pMySetting.PublicIP)
            If TestPublicLoopback() Then
                ' Set Public IP
                Dim ip As String = client.DownloadString("http://api.ipify.org/?r=" + Random())
                BumpProgress10()

                pMySetting.PublicIP = ip
                pMySetting.SaveSettings()
                Return True
            End If
        Catch ex As Exception
            ErrorLog("Hmm, I cannot reach the Internet? Uh. Okay, continuing." + ex.Message)
            pMySetting.DiagFailed = True
            Log("Info", "Public IP=" + "127.0.0.1")
        End Try

        pMySetting.PublicIP = pMyUPnpMap.LocalIP
        pMySetting.SaveSettings()

        BumpProgress10()
        Return False

    End Function

    Private Function TestPublicLoopback() As Boolean

        'If IsPrivateIP(pMySetting.PublicIP) Then
        ' pMySetting.DiagFailed = True
        'Return False
        'End If

        'Print("Running Loopback Test")
        Dim result As String = ""
        Dim loopbacktest As String = "http://" + pMySetting.PublicIP + ":" + pMySetting.HttpPort + "/?_TestLoopback=" + Random()
        Try
            Log("Info", loopbacktest)
            result = client.DownloadString(loopbacktest)
        Catch ex As Exception
            ErrorLog("Err:Loopback fail:" + result + ":" + ex.Message)
            Return False
        End Try

        BumpProgress10()

        If pMySetting.PublicIP = pMyUPnpMap.LocalIP() Then Return False

        If result = "Test Completed" Then
            Log("Info", "Passed:" + result)
            pMySetting.LoopBackDiag = True
            pMySetting.SaveSettings()
            Return True
        Else

            pMySetting.LoopBackDiag = False
            pMySetting.DiagFailed = True
#Disable Warning BC42025 ' Access of shared member, constant member, enum member or nested type through an instance
            pMySetting.PublicIP = pMyUPnpMap.LocalIP()
#Enable Warning BC42025 ' Access of shared member, constant member, enum member or nested type through an instance

            pMySetting.SaveSettings()
        End If
        Return False

    End Function

    Private Sub DiagnosticsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DiagnosticsToolStripMenuItem.Click

        If Not pOpensimIsRunning() Then
            Print("Cannot run diagnostics unless Opensimulator is running. Click 'Start' and try again.")
            Return
        End If
        ProgressBar1.Value = 0
        DoDiag()
        If pMySetting.DiagFailed = True Then
            Print("Hypergrid Diagnostics failed. These can be re-run at any time. Ip is set for LAN use only. See Help->Network Diagnostics', 'Loopback', and 'Port Forwards'")
        Else
            Print("Tests passed, Hypergrid should be working.")
        End If
        ProgressBar1.Value = 100

    End Sub

    Private Function ProbePublicPort() As Boolean

        If IsPrivateIP(pMySetting.DNSName) Then
            Return True
        End If
        Print("Checking Network Connectivity")

        Dim isPortOpen As String = ""
        Try
            ' collect some stats and test loopback with a HTTP_ GET to the webserver.
            ' Send unique, anonymous random ID, both of the versions of Opensim and this program, and the diagnostics test results
            ' See my privacy policy at https://www.outworldz.com/privacy.htm

            Dim Url = pDomain() + "/cgi/probetest.plx?IP=" + pMySetting.PublicIP + "&Port=" + pMySetting.HttpPort + GetPostData()
            Log("Info", Url)
            isPortOpen = client.DownloadString(Url)
        Catch ex As Exception
            ErrorLog("Dang:The Outworldz web site cannot find a path back")
            pMySetting.DiagFailed = True
        End Try

        BumpProgress10()

        If isPortOpen = "yes" Then
            pMySetting.PublicIP = pMySetting.PublicIP
            Print("Public IP set to " + pMySetting.PublicIP)
            pMySetting.SaveSettings()
            Return True
        Else
            Log("Warn", "Failed:" + isPortOpen)
            pMySetting.DiagFailed = True
            Print("Internet address " + pMySetting.PublicIP + ":" + pMySetting.HttpPort + " appears to not be forwarded to this machine in your router, so Hypergrid is not available. This can possibly be fixed by 'Port Forwards' in your router.  See Help->Port Forwards.")
#Disable Warning BC42025 ' Access of shared member, constant member, enum member or nested type through an instance
            pMySetting.PublicIP = pMyUPnpMap.LocalIP() ' failed, so try the machine address
#Enable Warning BC42025 ' Access of shared member, constant member, enum member or nested type through an instance
            pMySetting.SaveSettings()
            Log("Info", "IP set to " + pMySetting.PublicIP)
            Return False
        End If

    End Function

    Private Sub DoDiag()

        If IsPrivateIP(pMySetting.DNSName) Then
            Return
        End If

        Print("Running Network Diagnostics, please wait")

        pMySetting.DiagFailed = False

        OpenPorts() ' Open router ports with UPnp

        ProbePublicPort()
        TestPublicLoopback()
        If pMySetting.DiagFailed Then
            Dim answer = MsgBox("Diagnostics failed. Do you want to see the log?", vbYesNo)
            If answer = vbYes Then
                ShowLog()
            End If
        Else
            NewDNSName()
        End If
        Log("Info", "Diagnostics set the Grid address to " + pMySetting.PublicIP)

    End Sub

    Private Sub CheckDiagPort()

        pUseIcons = True
        Print("Check Diagnostics port")
        Dim wsstarted = CheckPort("127.0.0.1", CType(pMySetting.DiagnosticPort, Integer))
        If wsstarted = False Then
            MsgBox("Diagnostics port " + pMySetting.DiagnosticPort + " is not working, As Dreamgrid is not running at a high enough security level,  or blocked by firewall or anti virus, so region icons are disabled.", vbInformation, "There is a problem")
            pUseIcons = False
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

        If Not pMyUPnpMap.UPnpEnabled And pMySetting.UPnPEnabled Then
            Log("UPnP", "UPnP is not working in the router")
            pMySetting.UPnPEnabled = False
            pMySetting.SaveSettings()
            Return False
        End If

        If Not pMySetting.UPnPEnabled Then
            Return False
        End If

        Log("UPnP", "Local IP seems to be " + pMyUPnpMap.LocalIP)

        Try
            If pMySetting.SCEnable Then
                'Icecast 8080
                If pMyUPnpMap.Exists(Convert.ToInt16(pMySetting.SCPortBase), UPnp.Protocol.TCP) Then
                    pMyUPnpMap.Remove(Convert.ToInt16(pMySetting.SCPortBase), UPnp.Protocol.TCP)
                End If
                pMyUPnpMap.Add(pMyUPnpMap.LocalIP, CType(pMySetting.SCPortBase, Integer), UPnp.Protocol.TCP, "Icecast TCP Public " + pMySetting.SCPortBase.ToString(Usa))
                Print("Icecast Port is set to " + pMySetting.SCPortBase.ToString(Usa))
                BumpProgress10()
            End If

            ' 8002 for TCP and UDP
            If pMyUPnpMap.Exists(Convert.ToInt16(pMySetting.HttpPort, Usa), UPnp.Protocol.TCP) Then
                pMyUPnpMap.Remove(Convert.ToInt16(pMySetting.HttpPort, Usa), UPnp.Protocol.TCP)
            End If
            pMyUPnpMap.Add(pMyUPnpMap.LocalIP, Convert.ToInt16(pMySetting.HttpPort, Usa), UPnp.Protocol.TCP, "Opensim TCP Grid " + pMySetting.HttpPort)
            Print("Grid TCP Port is set to " + pMySetting.HttpPort)
            BumpProgress10()

            If pMyUPnpMap.Exists(Convert.ToInt16(pMySetting.HttpPort, Usa), UPnp.Protocol.UDP) Then
                pMyUPnpMap.Remove(Convert.ToInt16(pMySetting.HttpPort, Usa), UPnp.Protocol.UDP)
            End If
            pMyUPnpMap.Add(pMyUPnpMap.LocalIP, Convert.ToInt16(pMySetting.HttpPort, Usa), UPnp.Protocol.UDP, "Opensim UDP Grid " + pMySetting.HttpPort)
            Print("Grid UDP Port is set to " + pMySetting.HttpPort)
            BumpProgress10()

            For Each X In pRegionClass.RegionNumbers
                Dim R As Integer = pRegionClass.RegionPort(X)

                If pMyUPnpMap.Exists(R, UPnp.Protocol.UDP) Then
                    pMyUPnpMap.Remove(R, UPnp.Protocol.UDP)
                End If
                pMyUPnpMap.Add(pMyUPnpMap.LocalIP, R, UPnp.Protocol.UDP, "Opensim UDP Region " & pRegionClass.RegionName(X) & " ")
                Print("Region UDP " + pRegionClass.RegionName(X) + " is set to " + Convert.ToString(R, Usa))
                BumpProgress(1)

                If pMyUPnpMap.Exists(R, UPnp.Protocol.TCP) Then
                    pMyUPnpMap.Remove(R, UPnp.Protocol.TCP)
                End If
                pMyUPnpMap.Add(pMyUPnpMap.LocalIP, R, UPnp.Protocol.TCP, "Opensim TCP Region " & pRegionClass.RegionName(X) & " ")
                Print("Region TCP " + pRegionClass.RegionName(X) + " is set to " + Convert.ToString(R, Usa))
                BumpProgress(1)
            Next

            If pMyUPnpMap.Exists(Convert.ToInt16(pMySetting.SCPortBase), UPnp.Protocol.TCP) Then
                pMyUPnpMap.Remove(Convert.ToInt16(pMySetting.SCPortBase), UPnp.Protocol.TCP)
            End If
            pMyUPnpMap.Add(pMyUPnpMap.LocalIP, pMySetting.SCPortBase, UPnp.Protocol.TCP, "Icecast TCP" + pMySetting.SCPortBase.ToString(Usa))
            Print("Icecast Port is set to " + pMySetting.SCPortBase.ToString(Usa))

            BumpProgress10()
        Catch e As Exception
            Log("UPnP", "UPnP Exception caught:  " + e.Message)
            Return False
        End Try
        Return True 'successfully added

    End Function

    Private Function GetPostData() As String

        Dim UPnp As String = "Fail"
        If pMySetting.UPnpDiag Then
            UPnp = "Pass"
        End If
        Dim Loopb As String = "Fail"
        If pMySetting.LoopBackDiag Then
            Loopb = "Pass"
        End If

        Dim Grid As String = "Grid"

        ' no DNS password used if DNS name is null
        Dim m = pMySetting.MachineID()
        If pMySetting.DNSName.Length = 0 Then
            m = ""
        End If

        Dim data As String = "&MachineID=" + m _
        & "&FriendlyName=" & WebUtility.UrlEncode(pMySetting.SimName) _
        & "&V=" & WebUtility.UrlEncode(pMyVersion.ToString(Usa)) _
        & "&OV=" & WebUtility.UrlEncode(pSimVersion.ToString(Usa)) _
        & "&uPnp=" & UPnp.ToString(Usa) _
        & "&Loop=" & Loopb.ToString(Usa) _
        & "&Type=" & Grid.ToString(Usa) _
        & "&Ver=" & pMyVersion.ToString(Usa) _
        & "&isPublic=" & pMySetting.GDPR().ToString(Usa) _
        & "&r=" & Random()
        Return data

    End Function

    Private Function OpenPorts() As Boolean

        Print("Check Router Ports")
        Try
            If OpenRouterPorts() Then ' open UPnp port
                Log("Info", "UPnP: Ok")
                pMySetting.UPnpDiag = True
                pMySetting.SaveSettings()
                BumpProgress10()
                Return True
            Else
                Log("UPnP", "Fail or disabled")
                pMySetting.UPnpDiag = False
                pMySetting.SaveSettings()
                BumpProgress10()
                Return False
            End If
        Catch e As Exception
            Log("Error", " UPnP Exception: " + e.Message)
            pMySetting.UPnpDiag = False
            pMySetting.SaveSettings()
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

        ChDir(pMyFolder & "\OutworldzFiles\mysql\bin")
        pi.WindowStyle = ProcessWindowStyle.Normal
        pi.Arguments = pMySetting.MySqlPort

        pi.FileName = "CheckAndRepair.bat"
        Dim pMySqlDiag1 As Process = New Process With {
            .StartInfo = pi
        }
        pMySqlDiag1.Start()
        pMySqlDiag1.WaitForExit()

        ChDir(pMyFolder)

    End Sub

    Private Sub RestoreDatabaseToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles RestoreDatabaseToolStripMenuItem1.Click

        If pOpensimIsRunning() Then
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
                        My.Computer.FileSystem.DeleteFile(pMyFolder & "\OutworldzFiles\mysql\bin\RestoreMysql.bat")
                    Catch
                    End Try
                    Try
                        Dim filename As String = pMyFolder & "\OutworldzFiles\mysql\bin\RestoreMysql.bat"
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
                        .WorkingDirectory = pMyFolder & "\OutworldzFiles\mysql\bin\",
                        .FileName = pMyFolder & "\OutworldzFiles\mysql\bin\RestoreMysql.bat"
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
            .WorkingDirectory = pMyFolder & "\OutworldzFiles\mysql\bin\",
            .FileName = pMyFolder & "\OutworldzFiles\mysql\bin\BackupMysql.bat"
        }
        pMySqlBackup.StartInfo = pi

        pMySqlBackup.Start()

        Print("")
    End Sub

    Public Function StartMySQL() As Boolean


        Dim isMySqlRunning = CheckPort(pMySetting.RobustServer(), CType(pMySetting.MySqlPort, Integer))

        If isMySqlRunning Then Return True
        ' Start MySql in background.

        BumpProgress10()
        Dim StartValue = ProgressBar1.Value
        Print("Starting MySql Database")

        ' SAVE INI file
        pMySetting.LoadOtherIni(pMyFolder & "\OutworldzFiles\mysql\my.ini", "#")
        pMySetting.SetOtherIni("mysqld", "basedir", """" + pCurSlashDir + "/OutworldzFiles/Mysql" + """")
        pMySetting.SetOtherIni("mysqld", "datadir", """" + pCurSlashDir + "/OutworldzFiles/Mysql/Data" + """")
        pMySetting.SetOtherIni("mysqld", "port", pMySetting.MySqlPort)
        pMySetting.SetOtherIni("client", "port", pMySetting.MySqlPort)
        pMySetting.SaveOtherINI()

        ' create test program
        ' slants the other way:
        Dim testProgram As String = pMyFolder & "\OutworldzFiles\Mysql\bin\StartManually.bat"
        Try
            My.Computer.FileSystem.DeleteFile(testProgram)
        Catch ex As Exception
            ErrorLog("Error: Cannot Delete File: " + ex.Message)
        End Try
        Try
            Using outputFile As New StreamWriter(testProgram, True)
                outputFile.WriteLine("@REM A program to run Mysql manually for troubleshooting." + vbCrLf _
                                 + "mysqld.exe --defaults-file=" + """" + pCurSlashDir + "/OutworldzFiles/mysql/my.ini" + """")
            End Using
        Catch ex As Exception
            ErrorLog("Error:Cannot write:" + ex.Message)
        End Try

        CreateService()
        CreateStopMySql()

        BumpProgress(5)

        ' Mysql was not running, so lets start it up.
        Dim pi As ProcessStartInfo = New ProcessStartInfo With {
            .Arguments = "--defaults-file=" + """" + pCurSlashDir + "/OutworldzFiles/mysql/my.ini" + """",
            .WindowStyle = ProcessWindowStyle.Hidden,
            .FileName = """" + pMyFolder & "\OutworldzFiles\mysql\bin\mysqld.exe" + """"
        }
        ProcessMySql.StartInfo = pi
        ProcessMySql.EnableRaisingEvents = True
        ProcessMySql.Start()

        ' wait for MySql to come up
        Dim MysqlOk As Boolean
        While Not MysqlOk And pOpensimIsRunning And Not pAborting

            BumpProgress(1)
            Application.DoEvents()

            Dim MysqlLog As String = pMyFolder + "\OutworldzFiles\mysql\data"
            If ProgressBar1.Value = 100 Then ' about 30 seconds when it fails

                Dim yesno = MsgBox("The database did not start. Do you want to see the log file?", vbYesNo, "Error")
                If (yesno = vbYes) Then
                    Dim files() As String
                    files = Directory.GetFiles(MysqlLog, "*.err", SearchOption.TopDirectoryOnly)
                    For Each FileName As String In files
                        System.Diagnostics.Process.Start(pMyFolder + "\baretail.exe", """" & FileName & """")
                    Next
                End If
                Buttons(StartButton)
                Return False
            End If

            ' check again
            Sleep(2000)
            MysqlOk = CheckMysql()
        End While

        If Not pOpensimIsRunning Then Return False

        Return True

    End Function

    Private Sub CreateService()

        ' create test program
        ' slants the other way:
        Dim testProgram As String = pMyFolder & "\OutworldzFiles\Mysql\bin\InstallAsAService.bat"
        Try
            My.Computer.FileSystem.DeleteFile(testProgram)
        Catch ex As Exception
            ErrorLog("Delete File: " + ex.Message)
        End Try
        Try
            Using outputFile As New StreamWriter(testProgram, True)
                outputFile.WriteLine("@REM Program to run Mysql as a Service" + vbCrLf +
            "mysqld.exe --install Mysql --defaults-file=" + """" + pCurSlashDir + "/OutworldzFiles/mysql/my.ini" + """" + vbCrLf + "net start Mysql" + vbCrLf)
            End Using
        Catch ex As Exception
            ErrorLog("Error:Install As A Service" + ex.Message)
        End Try

    End Sub

    Private Sub CreateStopMySql()

        ' create test program
        ' slants the other way:
        Dim testProgram As String = pMyFolder & "\OutworldzFiles\Mysql\bin\StopMySQL.bat"
        Try
            My.Computer.FileSystem.DeleteFile(testProgram)
        Catch ex As Exception
            ErrorLog("Delete File: " + ex.Message)
        End Try
        Try
            Using outputFile As New StreamWriter(testProgram, True)
                outputFile.WriteLine("@REM Program to stop Mysql" + vbCrLf +
            "mysqladmin.exe -u root --port " + pMySetting.MySqlPort + " shutdown" + vbCrLf + "@pause" + vbCrLf)
            End Using
        Catch ex As Exception
            ErrorLog("Error:StopMySQL.bat" + ex.Message)
        End Try

    End Sub

    Function CheckMysql() As Boolean

        Dim version As String = Nothing
        Try
            Dim MysqlConn As New MysqlInterface(pRobustConnStr())
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

        Dim isMySqlRunning = CheckPort("127.0.0.1", CType(pMySetting.MySqlPort, Integer))
        If Not isMySqlRunning Then Return

        If Not pStopMysql Then
            Print("MySQL was running when I woke up, so I am leaving MySQL on.")
            Return
        End If

        Print("Stopping MySql")


        Dim p As Process = New Process()
        Dim pi As ProcessStartInfo = New ProcessStartInfo With {
            .Arguments = "--port " + pMySetting.MySqlPort + " -u root shutdown",
            .FileName = """" + pMyFolder + "\OutworldzFiles\mysql\bin\mysqladmin.exe" + """",
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

        If pMySetting.ServerType <> "Robust" Then
            Return True
        End If

        If pMySetting.DNSName.Length = 0 Then
            Return True
        End If

        If IsPrivateIP(pMySetting.DNSName) Then
            Return True
        End If

        'Print("Checking " + "http://" + pMySetting.DNSName + ":" + pMySetting.HttpPort)

        Dim client As New System.Net.WebClient
        Dim Checkname As String

        Try
            Application.DoEvents()
            Checkname = client.DownloadString("http://outworldz.net/dns.plx?GridName=" + pMySetting.DNSName + GetPostData())
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
        If pMySetting.ServerType <> "Robust" Then
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

        If pMySetting.DNSName.Length = 0 And pMySetting.EnableHypergrid Then
            Dim newname = GetNewDnsName()
            If newname.Length >= 0 Then
                If RegisterName(newname).Length >= 0 Then
                    BumpProgress10()
                    pMySetting.DNSName = newname
                    pMySetting.PublicIP = newname
                    pMySetting.SaveSettings()
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

        If pRegionClass Is Nothing Then Return

        For Each RegionNum In pRegionClass.RegionNumbers

            Dim Menu As New ToolStripMenuItem With {
                .Text = pRegionClass.RegionName(RegionNum),
                .ToolTipText = "Click to view stats on " + pRegionClass.RegionName(RegionNum),
                .DisplayStyle = ToolStripItemDisplayStyle.Text
            }
            If pRegionClass.IsBooted(RegionNum) Then
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
        If pOpensimIsRunning() Then
            Dim regionnum = pRegionClass.FindRegionByName(CType(sender.text, String))
            Dim port As String = pRegionClass.RegionPort(regionnum).ToString(Usa)
            Dim webAddress As String = "http://localhost:" + pMySetting.HttpPort + "/bin/data/sim.html?port=" + port
            Process.Start(webAddress)
        Else
            Print("Opensim is not running. Cannot open the Web Interface.")
        End If
    End Sub

    Private Sub ShowRegionform()

        If RegionList.InstanceExists = False Then
            pRegionForm = New RegionList
            pRegionForm.Show()
            pRegionForm.Activate()
        Else
            pRegionForm.Show()
            pRegionForm.Activate()
        End If

    End Sub

    Private Sub RegionsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RegionsToolStripMenuItem.Click

        ShowRegionform()

    End Sub

    Private Sub ScanAgents()
        ' Scan all the regions
        Dim sbttl As Integer = 0
        Try
            For Each RegionNum As Integer In pRegionClass.RegionNumbers
                If pRegionClass.IsBooted(RegionNum) Then
                    Dim MysqlConn As New MysqlInterface(pRobustConnStr())
                    Dim count As Integer = MysqlConn.IsUserPresent(pRegionClass.UUID(RegionNum))
                    sbttl += count
                    If count > 0 Then
                        Diagnostics.Debug.Print("Avatar in region " & pRegionClass.RegionName(RegionNum))
                    End If
                    pRegionClass.AvatarCount(RegionNum) = count
                    'Debug.Print(pRegionClass.AvatarCount(X).ToString(usa) + " avatars in region " + pRegionClass.RegionName(X))
                Else
                    pRegionClass.AvatarCount(RegionNum) = 0
                End If
            Next
        Catch
        End Try

        AvatarLabel.Text = sbttl.ToString(Usa)
    End Sub

#End Region

#Region "Alerts"

    Private Sub SendMsg(msg As String)

        If Not pOpensimIsRunning() Then Print("Opensim is not running")

        For Each Regionnumber As Integer In pRegionClass.RegionNumbers
            If pRegionClass.IsBooted(Regionnumber) Then
                ConsoleCommand(pRegionClass.GroupName(Regionnumber), "set log level " + msg + "{ENTER}" + vbCrLf)
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
        If pOpensimIsRunning() And pMySetting.SCEnable Then
            Dim webAddress As String = "http://" + pMySetting.PublicIP + ":" + pMySetting.SCPortBase.ToString(Usa)
            Print("Icecast lets you stream music into your sim. The Music URL is " + webAddress + "/stream")
            Process.Start(webAddress)
        ElseIf pMySetting.SCEnable = False Then
            Print("Shoutcast is not Enabled.")
        Else
            Print("Opensim is not running. Click Start to boot the system.")
        End If
    End Sub

    Private Sub RestartOneRegionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RestartOneRegionToolStripMenuItem.Click
        If Not pOpensimIsRunning() Then
            Print("Opensim is not running")
            Return
        End If
        Dim name = ChooseRegion(True)
        Dim X = pRegionClass.FindRegionByName(name)
        If X > -1 Then
            ConsoleCommand(pRegionClass.GroupName(X), "change region " + name + "{ENTER}" + vbCrLf)
            ConsoleCommand(pRegionClass.GroupName(X), "restart region " + name + "{ENTER}" + vbCrLf)
            pUpdateView = True ' make form refresh
        End If

    End Sub

    Private Sub RestartTheInstanceToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RestartTheInstanceToolStripMenuItem.Click
        If Not pOpensimIsRunning() Then
            Print("Opensim is not running")
            Return
        End If
        Dim name = ChooseRegion(True)
        Dim X = pRegionClass.FindRegionByName(name)
        If X > -1 Then
            ConsoleCommand(pRegionClass.GroupName(X), "restart{ENTER}" + vbCrLf)
            pUpdateView = True ' make form refresh
        End If

    End Sub

    Private Sub SendScriptCmd(cmd As String)
        If Not pOpensimIsRunning() Then
            Print("Opensim is not running")
            Return
        End If
        Dim rname = ChooseRegion(True)
        Dim X = pRegionClass.FindRegionByName(rname)
        If X > -1 Then
            ConsoleCommand(pRegionClass.GroupName(X), "change region " + rname + "{ENTER}" + vbCrLf)
            ConsoleCommand(pRegionClass.GroupName(X), cmd + "{ENTER}" + vbCrLf)
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

        If Not pOpensimIsRunning() Then
            Print("Opensim is not running")
            Return
        End If
        Dim rname = ChooseRegion(True)
        If rname.Length > 0 Then
            Dim Message = InputBox("What do you want to say to this region?")
            Dim X = pRegionClass.FindRegionByName(rname)
            ConsoleCommand(pRegionClass.GroupName(X), "change region  " + pRegionClass.RegionName(X) + "{ENTER}" + vbCrLf)
            ConsoleCommand(pRegionClass.GroupName(X), "alert " + Message + "{ENTER}" + vbCrLf)
        End If

    End Sub

    Private Sub JustOneRegionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AllUsersAllSimsToolStripMenuItem.Click

        If Not pOpensimIsRunning() Then
            Print("Opensim is not running")
            Return
        End If

        Dim HowManyAreOnline As Integer = 0
        Dim Message = InputBox("What do you want to say to everyone online?")
        If Message.Length > 0 Then
            For Each X As Integer In pRegionClass.RegionNumbers
                If pRegionClass.AvatarCount(X) > 0 Then
                    HowManyAreOnline += 1
                    ConsoleCommand(pRegionClass.GroupName(X), "change region  " + pRegionClass.RegionName(X) + "{ENTER}" + vbCrLf)
                    ConsoleCommand(pRegionClass.GroupName(X), "alert " + Message + "{ENTER}" + vbCrLf)
                End If

            Next
            If HowManyAreOnline = 0 Then
                Print("Nobody is online")
            Else
                Print("Message sent to " + HowManyAreOnline.ToString(Usa) + " regions")
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
        Dim Filename = pMyFolder + "\OutworldzFiles\OAR\"
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

        If pMySetting.BackupFolder = "AutoBackup" Then
            Filename = pMyFolder + "\OutworldzFiles\AutoBackup\"
        Else
            Filename = pMySetting.BackupFolder
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
        Filename = pMyFolder + "\OutworldzFiles\IAR\"
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

        If pMySetting.BackupFolder = "AutoBackup" Then
            Filename = pMyFolder + "\OutworldzFiles\AutoBackup\"
        Else
            Filename = pMySetting.BackupFolder
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

        Dim File = pMyFolder + "/OutworldzFiles/OAR/" + sender.text.ToString() 'make a real URL
        If LoadOARContent(File) Then
            Print("Opensimulator will load " + sender.text.ToString() + ".  This may take time to load.")
        End If

    End Sub

    Private Sub BackupOarClick(sender As Object, e As EventArgs)

        Dim File = pMyFolder + "/OutworldzFiles/AutoBackup/" + sender.text.ToString() 'make a real URL
        If LoadOARContent(File) Then
            Print("Opensimulator will load " + sender.text.ToString() + ".  This may take time to load.")
        End If

    End Sub

    Private Sub LocalIarClick(sender As Object, e As EventArgs)

        Dim File As String = pMyFolder + "/OutworldzFiles/IAR/" + sender.text.ToString() 'make a real URL
        If LoadIARContent(File) Then
            Print("Opensimulator will load " & sender.text.ToString() & ".  This may take time to load.")
        End If

    End Sub

    Private Sub BackupIarClick(sender As Object, e As EventArgs)

        Dim File As String = pMyFolder & "/OutworldzFiles/AutoBackup/" & sender.text.ToString() 'make a real URL
        If LoadIARContent(File) Then
            Print("Opensimulator will load " + sender.text.ToString() + ".  This may take time to load.")
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

        If sender.text.ToString() <> "Dreamgrid Manual.pdf" Then Help(sender.text.ToString())

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
            name = pRegionClass.GroupName(pRegionClass.FindRegionByName(name))
            path.Add("""" & pOpensimBinPath & "bin\Regions\" & name & "\Opensim.log" & """")
        Else
            If name = "All Logs" Then AllLogs = True
            If name = "Robust" Or AllLogs Then path.Add("""" & pOpensimBinPath & "bin\Robust.log" & """")
            If name = "Outworldz" Or AllLogs Then path.Add("""" & pMyFolder & "\Outworldzfiles\Outworldz.log" & """")
            If name = "UPnP" Or AllLogs Then path.Add("""" & pMyFolder & "\Outworldzfiles\Upnp.log" & """")
            If name = "Icecast" Or AllLogs Then path.Add(" " & """" & pMyFolder & "\Outworldzfiles\Icecast\log\error.log" & """")
            If name = "All Settings" Or AllLogs Then path.Add("""" & pMyFolder & "\Outworldzfiles\Settings.ini" & """")
            If name = "--- Regions ---" Then Return

            If AllLogs Then
                For Each Regionnumber In pRegionClass.RegionNumbers
                    name = pRegionClass.GroupName(Regionnumber)
                    path.Add("""" & pOpensimBinPath & "bin\Regions\" & name & "\Opensim.log" & """")
                Next
            End If

            If name = "MySQL" Or AllLogs Then
                Dim MysqlLog As String = pMyFolder & "\OutworldzFiles\mysql\data"
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
            System.Diagnostics.Process.Start(pMyFolder + "\baretail.exe", logs)
        Catch
        End Try

    End Sub

    Private Sub RevisionHistoryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RevisionHistoryToolStripMenuItem.Click
        Help("Revisions")
    End Sub

    Private Sub ThreadpoolsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ThreadpoolsToolStripMenuItem.Click
        For Each RegionNum As Integer In pRegionClass.RegionListByGroupNum("*")
            ConsoleCommand(pRegionClass.RegionName(RegionNum), "show threads{ENTER}" + vbCrLf)
        Next
    End Sub

    Private Sub XengineToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles XengineToolStripMenuItem.Click
        For Each RegionNum As Integer In pRegionClass.RegionListByGroupNum("*")
            ConsoleCommand(pRegionClass.RegionName(RegionNum), "xengine status{ENTER}" + vbCrLf)
        Next
    End Sub

    Private Sub JobEngineToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles JobEngineToolStripMenuItem.Click
        For Each RegionNum As Integer In pRegionClass.RegionListByGroupNum("*")
            ConsoleCommand(pRegionClass.RegionName(RegionNum), "debug jobengine status{ENTER}" + vbCrLf)
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

        If pMySetting.Sequential Then

            For Each X As Integer In pRegionClass.RegionNumbers
                If pOpensimIsRunning() And pRegionClass.RegionEnabled(X) And
                    Not (pRegionClass.Status(X) = RegionMaker.SIMSTATUSENUM.RecyclingDown _
                    Or pRegionClass.Status(X) = RegionMaker.SIMSTATUSENUM.ShuttingDown _
                    Or pRegionClass.Status(X) = RegionMaker.SIMSTATUSENUM.Stopped) Then

                    Dim ctr = 600 ' 1 minute max to start a region
                    Dim WaitForIt = True
                    While WaitForIt
                        Sleep(100)
                        If pRegionClass.RegionEnabled(X) _
                    And Not pAborting _
                    And (pRegionClass.Status(X) = RegionMaker.SIMSTATUSENUM.RecyclingUp Or
                        pRegionClass.Status(X) = RegionMaker.SIMSTATUSENUM.ShuttingDown Or
                        pRegionClass.Status(X) = RegionMaker.SIMSTATUSENUM.RecyclingDown Or
                        pRegionClass.Status(X) = RegionMaker.SIMSTATUSENUM.Booting) Then
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
                If cpu.NextValue() < pCPUMAX Then
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

        Dim Command As String = "netsh advfirewall firewall  delete rule name=""Opensim TCP Port " & pMySetting.DiagnosticPort & """" & vbCrLf _
                          & "netsh advfirewall firewall  delete rule name=""Opensim UDP Port " & pMySetting.DiagnosticPort & """" & vbCrLf _
                          & "netsh advfirewall firewall  delete rule name=""Opensim HTTP TCP Port " & pMySetting.HttpPort & """" & vbCrLf _
                          & "netsh advfirewall firewall  delete rule name=""Opensim HTTP UDP Port " & pMySetting.HttpPort & """" & vbCrLf _
                          & "netsh advfirewall firewall  delete rule name=""Icecast Port1 UDP " & pMySetting.SCPortBase.ToString(Usa) & """" & vbCrLf _
                          & "netsh advfirewall firewall  delete rule name=""Icecast Port1 TCP " & pMySetting.SCPortBase.ToString(Usa) & """" & vbCrLf _
                          & "netsh advfirewall firewall  delete rule name=""Icecast Port2 UDP " & pMySetting.SCPortBase1.ToString(Usa) & """" & vbCrLf _
                          & "netsh advfirewall firewall  delete rule name=""Icecast Port2 TCP " & pMySetting.SCPortBase1.ToString(Usa) & """" & vbCrLf

        If pMySetting.ApacheEnable Then
            Command = Command + "netsh advfirewall firewall  delete rule name=""Opensim HTTP Web Port " & pMySetting.HttpPort & """" & vbCrLf
        End If

        Dim RegionNumber As Integer = 0
        Dim start = CInt(pMySetting.FirstRegionPort)

        For RegionNumber = start To pMaxPortUsed
            Command = Command + "netsh advfirewall firewall  delete rule name=""Region TCP Port " & RegionNumber.ToString(Usa) & """" & vbCrLf _
                          & "netsh advfirewall firewall  delete rule name=""Region UDP Port " & RegionNumber.ToString(Usa) & """" & vbCrLf
        Next

        Return Command

    End Function

    Private Function AddFirewallRules() As String

        ' TCP only for 8001 (DiagnosticPort) and both for 8002
        Dim Command As String = "netsh advfirewall firewall  add rule name=""Opensim TCP Port " & pMySetting.DiagnosticPort & """ dir=in action=allow protocol=TCP localport=" & pMySetting.DiagnosticPort & vbCrLf _
                          & "netsh advfirewall firewall  add rule name=""Opensim HTTP TCP Port " & pMySetting.HttpPort & """ dir=in action=allow protocol=TCP localport=" & pMySetting.HttpPort & vbCrLf _
                          & "netsh advfirewall firewall  add rule name=""Opensim HTTP UDP Port " & pMySetting.HttpPort & """ dir=in action=allow protocol=UDP localport=" & pMySetting.HttpPort & vbCrLf

        If pMySetting.ApacheEnable Then
            Command = Command + "netsh advfirewall firewall  add rule name=""Opensim HTTP Web Port " & pMySetting.HttpPort & """ dir=in action=allow protocol=TCP localport=" & pMySetting.ApachePort & vbCrLf
        End If

        ' Icecast needs both ports for both protocols
        If pMySetting.SCEnable Then
            Command = Command & "netsh advfirewall firewall  add rule name=""Icecast Port1 UDP " & pMySetting.SCPortBase.ToString(Usa) & """ dir=in action=allow protocol=UDP localport=" & pMySetting.SCPortBase & vbCrLf _
                          & "netsh advfirewall firewall  add rule name=""Icecast Port1 TCP " & pMySetting.SCPortBase.ToString(Usa) & """ dir=in action=allow protocol=TCP localport=" & pMySetting.SCPortBase & vbCrLf _
                          & "netsh advfirewall firewall  add rule name=""Icecast Port2 UDP " & pMySetting.SCPortBase1.ToString(Usa) & """ dir=in action=allow protocol=UDP localport=" & pMySetting.SCPortBase1 & vbCrLf _
                          & "netsh advfirewall firewall  add rule name=""Icecast Port2 TCP " & pMySetting.SCPortBase1.ToString(Usa) & """ dir=in action=allow protocol=TCP localport=" & pMySetting.SCPortBase1 & vbCrLf
        End If

        Dim RegionNumber As Integer = 0
        Dim start = CInt(pMySetting.FirstRegionPort)

        ' regions need both
        For RegionNumber = start To pMaxPortUsed
            Command = Command + "netsh advfirewall firewall  add rule name=""Region TCP Port " & RegionNumber.ToString(Usa) & """ dir=in action=allow protocol=TCP localport=" & RegionNumber.ToString(Usa) & vbCrLf _
                          & "netsh advfirewall firewall  add rule name=""Region UDP Port " & RegionNumber.ToString(Usa) & """ dir=in action=allow protocol=UDP localport=" & RegionNumber.ToString(Usa) & vbCrLf
        Next

        Return Command

    End Function

    Public Sub SetFirewall()

        Print("Setup Firewall")
        Dim CMD As String = DeleteFirewallRules() & AddFirewallRules()

        Dim ns As StreamWriter = New StreamWriter(pMyFolder + "\fw.bat", False)
        ns.WriteLine(CMD)
        ns.Close()

        Dim pi As ProcessStartInfo = New ProcessStartInfo With {
            .Arguments = "",
            .FileName = pMyFolder + "\fw.bat",
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
        Dim webAddress As String = pMyFolder & "\Outworldzfiles\Help\Dreamgrid Manual.pdf"
        Process.Start(webAddress)
    End Sub

#End Region

#Region "Search"

    Private Sub SetupSearch()

        If pMySetting.ServerType = "Metro" Or pMySetting.ServerType = "OsGrid" Then Return

        Print("Setting up search")
        Dim pi As ProcessStartInfo = New ProcessStartInfo()

        FileIO.FileSystem.CurrentDirectory = pMyFolder & "\Outworldzfiles\mysql\bin\"
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
            FileIO.FileSystem.CurrentDirectory = pMyFolder
            Return
        End Try
        FileIO.FileSystem.CurrentDirectory = pMyFolder
        pMySetting.SearchInstalled = True
        pMySetting.SaveSettings()

    End Sub

    Private Sub RunDataSnapshot()

        If Not pMySetting.SearchLocal Then Return
        Diagnostics.Debug.Print("Scanning Datasnapshot")
        Dim pi As ProcessStartInfo = New ProcessStartInfo()

        FileIO.FileSystem.CurrentDirectory = pMyFolder & "\Outworldzfiles\PHP5\"
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
            FileIO.FileSystem.CurrentDirectory = pMyFolder
        End Try
    End Sub

    <CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")>
    Private Sub GetEvents()

        If Not pMySetting.ApacheEnable Then Return

        Dim Simevents As New Dictionary(Of String, String)
        Dim ossearch As String = "server=" + pMySetting.RobustServer() _
        + ";database=" + "ossearch" _
        + ";port=" + pMySetting.MySqlPort _
        + ";user=" + pMySetting.RobustUsername _
        + ";password=" + pMySetting.RobustPassword _
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
                Using Stream = client.OpenRead(pDomain() + "/events.txt?r=" & Random())
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
            'Print(ctr.ToString(usa) & " hypevents received")
        Catch
        End Try

    End Sub

    Shared Sub DeleteEvents(Connection As MySqlConnection)

        Dim stm = "delete from events"
        Dim cmd As MySqlCommand = New MySqlCommand(stm, Connection)
        Dim rowsdeleted = cmd.ExecuteNonQuery()
        Diagnostics.Debug.Print("Rows: {0}", rowsdeleted.ToString(Form1.Usa))

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
            Diagnostics.Debug.Print("Insert: {0}", rowsinserted.ToString(Form1.Usa))
        Catch ex As Exception

            Diagnostics.Debug.Print(ex.Message)
            For Each Keyvaluepair In D
                Diagnostics.Debug.Print("Key = {0}, Value = {1}", Keyvaluepair.Key, Keyvaluepair.Value)
            Next

        End Try

    End Sub


#End Region

End Class