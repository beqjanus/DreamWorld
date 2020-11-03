#Region "To do"

' Add back in the 5 physics type

#End Region

#Region "Copyright"

' Copyright 2014 Fred Beckhusen for Outworldz.com https://opensource.org/licenses/AGPL

'Permission is hereby granted, free Of charge, to any person obtaining a copy of this software
' And associated documentation files (the "Software"), to deal in the Software without restriction,
'including without limitation the rights To use, copy, modify, merge, publish, distribute, sub license,
'And/Or sell copies Of the Software, And To permit persons To whom the Software Is furnished To
'Do so, subject To the following conditions:

'The above copyright notice And this permission notice shall be included In all copies Or '
'substantial portions Of the Software.

'THE SOFTWARE Is PROVIDED "AS IS", WITHOUT WARRANTY Of ANY KIND, EXPRESS Or IMPLIED,
' INCLUDING BUT Not LIMITED To THE WARRANTIES Of MERCHANTABILITY, FITNESS For A PARTICULAR
'PURPOSE And NONINFRINGEMENT.In NO Event SHALL THE AUTHORS Or COPYRIGHT HOLDERS BE LIABLE
'For ANY CLAIM, DAMAGES Or OTHER LIABILITY, WHETHER In AN ACTION Of CONTRACT, TORT Or
'OTHERWISE, ARISING FROM, OUT Of Or In CONNECTION With THE SOFTWARE Or THE USE Or OTHER
'DEALINGS IN THE SOFTWARE.

#End Region

'Option Strict On

Imports System.Globalization
Imports System.IO
Imports System.Management
Imports System.Net
Imports System.Net.NetworkInformation
Imports System.Net.Sockets
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Threading
Imports Ionic.Zip
Imports IWshRuntimeLibrary

Public Class Form1

#Region "Version"

    Dim _MyVersion As String = "3.72"
    Dim _SearchRev As Integer = 5
    Dim _SimVersion As String = "#ba46b5bf8bd0 libomv master  0.9.2.dev 2020-09-21 2020-10-14 19:44"
    Dim _Domain As String = "http://outworldz.com"

#End Region

#Region "Declarations"

    Private WithEvents ApacheProcess As New Process()
    Private WithEvents IcecastProcess As New Process()
    Private WithEvents ProcessMySql As Process = New Process()
    Private WithEvents RobustProcess As New Process()
    Private WithEvents UpdateProcess As New Process()
    Private ReadOnly _exitList As New Dictionary(Of String, String)
    Private ReadOnly _regionHandles As New Dictionary(Of Integer, String)
    Private ReadOnly D As New Dictionary(Of String, String)
    Private ReadOnly ExitInterval As Integer = 1
    Private ReadOnly Handler As New EventHandler(AddressOf Resize_page)
    Private ReadOnly MyCPUCollection As New List(Of Double)
    Private ReadOnly MyRAMCollection As New List(Of Double)
    Private _Adv As AdvancedForm
    Private _ApacheCrashCounter As Integer
    Private _ApacheExited As Boolean
    Private _ApacheProcessID As Integer
    Private _ApacheUninstalling As Boolean
    Private _ContentIAR As FormOAR
    Private _ContentOAR As FormOAR
    Private _CurSlashDir As String
    Private _DNS_is_registered As Boolean
    Private _DNSSTimer As Integer
    Private _ExitHandlerIsBusy As Boolean
    Private _ForceMerge As Boolean
    Private _ForceParcel As Boolean
    Private _ForceTerrain As Boolean = True
    Private _IcecastCrashCounter As Integer
    Private _IceCastExited As Boolean
    Private _IcecastProcID As Integer
    Private _Initted As Boolean
    Private _IPv4Address As String
    Private _IsRunning As Boolean
    Private _KillSource As Boolean
    Private _MaxPortUsed As Integer
    Private _MaxXMLPortUsed As Integer
    Private _MysqlCrashCounter As Integer
    Private _MysqlExited As Boolean
    Private _myUPnpMap As UPnp
    Private _OpensimBinPath As String
    Private _PropAborting As Boolean
    Private _regionClass As RegionMaker
    Private _regionForm As RegionList
    Private _RestartApache As Boolean
    Private _RestartMysql As Boolean
    Private _RestartRobust As Boolean
    Private _RobustCrashCounter As Integer
    Private _RobustExited As Boolean
    Private _RobustIsStarting As Boolean
    Private _RobustProcID As Integer
    Private _SelectedBox As String = ""
    Private _speed As Double = 50
    Private _StopMysql As Boolean = True
    Private _timerBusy1 As Integer
    Private _UpdateView As Boolean = True
    Private _UserName As String = ""
    Private _viewedSettings As Boolean
    Private BootedList As New List(Of String)
#Disable Warning CA2213 ' Disposable fields should be disposed
    Private cpu As New PerformanceCounter
#Enable Warning CA2213 ' Disposable fields should be disposed

    Private ScreenPosition As ScreenPos

#End Region

#Region "Globals"

    Private speed As Double
    Private speed1 As Double
    Private speed2 As Double
    Private speed3 As Double
    Private Update_version As String
    Private ws As NetServer

#End Region

#Region "Events"

    Public Event ApacheExited As EventHandler

    Public Event Exited As EventHandler

    Public Event RobustExited As EventHandler

#End Region

#Region "Enum"

    Public Enum SHOWWINDOWENUM As Integer
        SWHIDE = 0
        SWNORMAL = 1
        SWSHOWMINIMIZED = 2
        SWMAXIMIZE = 3
        SWSHOWNOACTIVATE = 4
        SWSHOW = 5
        SWMINIMIZE = 6
        SWSHOWMINNOACTIVE = 7
        SWSHOWNA = 8
        SWRESTORE = 9
        SWSHOWDEFAULT = 10
        SWFORCEMINIMIZE = 11

    End Enum

#End Region

#Region "Public Properties"

    Public Property Adv1 As AdvancedForm
        Get
            Return _Adv
        End Get
        Set(value As AdvancedForm)
            _Adv = value
        End Set
    End Property

    Public ReadOnly Property BootedList1 As List(Of String)
        Get
            Return BootedList
        End Get
    End Property

    Public Property ContentIAR As FormOAR
        Get
            Return _ContentIAR
        End Get
        Set(value As FormOAR)
            _ContentIAR = value
        End Set
    End Property

    Public Property ContentOAR As FormOAR
        Get
            Return _ContentOAR
        End Get
        Set(value As FormOAR)
            _ContentOAR = value
        End Set
    End Property

    Public Property Cpu1 As PerformanceCounter
        Get
            Return cpu
        End Get
        Set(value As PerformanceCounter)
            cpu = value
        End Set
    End Property

    Public Property CPUAverageSpeed As Double
        Get
            Return _speed
        End Get
        Set(value As Double)
            _speed = value
        End Set
    End Property

    Public Property PropAborting() As Boolean
        Get
            Return _PropAborting
        End Get
        Set(ByVal Value As Boolean)
            _PropAborting = Value
        End Set
    End Property

    Public Property PropApacheExited() As Boolean
        Get
            Return _ApacheExited
        End Get
        Set(ByVal Value As Boolean)
            _ApacheExited = Value
        End Set
    End Property

    Public Property PropApacheUninstalling() As Boolean
        Get
            Return _ApacheUninstalling
        End Get
        Set(ByVal Value As Boolean)
            _ApacheUninstalling = Value
        End Set
    End Property

    Public Property PropCurSlashDir As String
        Get
            Return _CurSlashDir
        End Get
        Set(value As String)
            _CurSlashDir = value
        End Set
    End Property

    Public Property PropDNSSTimer() As Integer
        Get
            Return _DNSSTimer
        End Get
        Set(ByVal Value As Integer)
            _DNSSTimer = Value
        End Set
    End Property

    Public ReadOnly Property PropDomain As String
        Get
            Return _Domain
        End Get

    End Property

    Public Property PropExitHandlerIsBusy() As Boolean
        Get
            Return _ExitHandlerIsBusy
        End Get
        Set(ByVal Value As Boolean)
            _ExitHandlerIsBusy = Value
        End Set
    End Property

    Public ReadOnly Property PropExitList As Dictionary(Of String, String)
        Get
            Return _exitList
        End Get
    End Property

    Public Property PropForceMerge As Boolean
        Get
            Return _ForceMerge
        End Get
        Set(value As Boolean)
            _ForceMerge = value
        End Set
    End Property

    Public Property PropForceParcel As Boolean
        Get
            Return _ForceParcel
        End Get
        Set(value As Boolean)
            _ForceParcel = value
        End Set
    End Property

    Public Property PropForceTerrain As Boolean
        Get
            Return _ForceTerrain
        End Get
        Set(value As Boolean)
            _ForceTerrain = value
        End Set
    End Property

    Public Property PropgApacheProcessID As Integer
        Get
            Return _ApacheProcessID
        End Get
        Set(value As Integer)
            _ApacheProcessID = value
        End Set
    End Property

    Public Property PropIceCastExited() As Boolean
        Get
            Return _IceCastExited
        End Get
        Set(ByVal Value As Boolean)
            _IceCastExited = Value
        End Set
    End Property

    Public Property PropIcecastProcID As Integer
        Get
            Return _IcecastProcID
        End Get
        Set(value As Integer)
            _IcecastProcID = value
        End Set
    End Property

    Public Property PropInitted() As Boolean
        Get
            Return _Initted
        End Get
        Set(ByVal Value As Boolean)
            _Initted = Value
        End Set
    End Property

    Public Property PropIPv4Address() As String
        Get
            Return _IPv4Address
        End Get
        Set(ByVal Value As String)
            _IPv4Address = Value
        End Set
    End Property

    Public Property PropKillSource As Boolean
        Get
            Return _KillSource
        End Get
        Set(value As Boolean)
            _KillSource = value
        End Set
    End Property

    Public Property PropMaxPortUsed As Integer
        Get
            Return _MaxPortUsed
        End Get
        Set(value As Integer)
            _MaxPortUsed = value
        End Set
    End Property

    Public Property PropMaxXMLPortUsed As Integer
        Get
            Return _MaxXMLPortUsed
        End Get
        Set(value As Integer)
            _MaxXMLPortUsed = value
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

    Public Property PropMyUPnpMap As UPnp
        Get
            Return _myUPnpMap
        End Get
        Set(value As UPnp)
            _myUPnpMap = value
        End Set
    End Property

    Public ReadOnly Property PropMyVersion As String
        Get
            Return _MyVersion
        End Get
    End Property

    Public Property PropOpensimIsRunning() As Boolean
        Get
            Return _IsRunning
        End Get
        Set(ByVal Value As Boolean)
            _IsRunning = Value
        End Set
    End Property

    Public Property PropRegionClass As RegionMaker
        Get
            Return _regionClass
        End Get
        Set(value As RegionMaker)
            _regionClass = value
        End Set
    End Property

    Public Property PropRegionForm As RegionList
        Get
            Return _regionForm
        End Get
        Set(value As RegionList)
            _regionForm = value
        End Set
    End Property

    Public ReadOnly Property PropRegionHandles As Dictionary(Of Integer, String)
        Get
            Return _regionHandles
        End Get
    End Property

    Public Property PropRestartApache() As Boolean
        Get
            Return _RestartApache
        End Get
        Set(ByVal Value As Boolean)
            _RestartApache = Value
        End Set
    End Property

    Public Property PropRestartMySql() As Boolean
        Get
            Return _RestartMysql
        End Get
        Set(ByVal Value As Boolean)
            _RestartMysql = Value
        End Set
    End Property

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

    Public Property PropSelectedBox As String
        Get
            Return _SelectedBox
        End Get
        Set(value As String)
            _SelectedBox = value
        End Set
    End Property

    Public ReadOnly Property PropSimVersion As String
        Get
            Return _SimVersion
        End Get
    End Property

    Public Property PropStopMysql As Boolean
        Get
            Return _StopMysql
        End Get
        Set(value As Boolean)
            _StopMysql = value
        End Set
    End Property

    Public Property PropUpdateView As Boolean
        Get
            Return _UpdateView
        End Get
        Set(value As Boolean)
            _UpdateView = value
        End Set
    End Property

    Public Property PropUseIcons As Boolean

    Public Property PropUserName As String
        Get
            Return _UserName
        End Get
        Set(value As String)
            _UserName = value
        End Set
    End Property

    Public Property PropViewedSettings As Boolean
        Get
            Return _viewedSettings
        End Get
        Set(value As Boolean)
            Diagnostics.Debug.Print("ViewedSettings =" & value)
            _viewedSettings = value
        End Set
    End Property

    Public Property PropWebServer As NetServer
        Get
            Return ws
        End Get
        Set(value As NetServer)
            ws = value
        End Set
    End Property

    Public Property ScreenPosition1 As ScreenPos
        Get
            Return ScreenPosition
        End Get
        Set(value As ScreenPos)
            ScreenPosition = value
        End Set
    End Property

    Public ReadOnly Property SimVersion As String
        Get
            Return _SimVersion
        End Get

    End Property

    Public Property TimerBusy As Integer
        Get
            Return _timerBusy1
        End Get
        Set(value As Integer)
            _timerBusy1 = value
        End Set
    End Property

    Public Property OpensimBinPath As String
        Get
            Return _OpensimBinPath
        End Get
        Set(value As String)
            _OpensimBinPath = value
        End Set
    End Property

#End Region

#Region "Public Shared"

    Public Shared Sub CheckDefaultPorts()

        If Settings.DiagnosticPort = Settings.HttpPort _
        Or Settings.DiagnosticPort = Settings.PrivatePort _
        Or Settings.HttpPort = Settings.PrivatePort Then
            Settings.DiagnosticPort = 8001
            Settings.HttpPort = 8002
            Settings.PrivatePort = 8003

            MsgBox(My.Resources.Port_Error, vbInformation, Global.Outworldz.My.Resources.Error_word)
        End If

    End Sub

    <CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId:="Outworldz.Form1.Log(System.String,System.String)")>
    Public Shared Function CheckPort(ServerAddress As String, Port As Integer) As Boolean

        Log(My.Resources.Info_word, "Checking port " & CStr(Port))
        Dim success As Boolean
        Dim result As IAsyncResult = Nothing
        Using ClientSocket As New TcpClient
            Try
                result = ClientSocket.BeginConnect(ServerAddress, Port, Nothing, Nothing)
                success = result.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(2))
                ClientSocket.EndConnect(result)
            Catch ex As Exception
                ' no Breakpoint needed
                success = False
            End Try

            If success Then
                Log(My.Resources.Info_word, " port probe success on port " & CStr(Port))
                Return True
            End If

        End Using
        Log(My.Resources.Info_word, " port probe fail on port " & CStr(Port))
        Return False

    End Function

    <CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId:="Outworldz.Form1.ErrorLog(System.String)")>
    Public Shared Function ChooseRegion(Optional JustRunning As Boolean = False) As String

        ' Show testDialog as a modal dialog and determine if DialogResult = OK.
        Dim chosen As String = ""
        Using Chooseform As New Choice ' form for choosing a set of regions
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

    Public Shared Function CompareDLLignoreCase(tofind As String, dll As List(Of String)) As Boolean
        If dll Is Nothing Then Return False
        If tofind Is Nothing Then Return False
        For Each filename In dll
            If tofind.ToUpper(Globalization.CultureInfo.InvariantCulture) = filename.ToUpper(Globalization.CultureInfo.InvariantCulture) Then
                Return True
            End If
        Next
        Return False
    End Function

    Public Shared Sub CopyWifi(Page As String)

        If System.IO.Directory.Exists(Settings.CurrentDirectory & "\Outworldzfiles\Opensim\WifiPages") Then
            System.IO.Directory.Delete(Settings.CurrentDirectory & "\Outworldzfiles\Opensim\WifiPages", True)
        End If
        If System.IO.Directory.Exists(Settings.CurrentDirectory & "\Outworldzfiles\Opensim\bin\WifiPages") Then
            System.IO.Directory.Delete(Settings.CurrentDirectory & "\Outworldzfiles\Opensim\bin\WifiPages", True)
        End If

        Try
            My.Computer.FileSystem.CopyDirectory(Settings.CurrentDirectory & "\Outworldzfiles\Opensim\WifiPages-" & Page, Settings.CurrentDirectory & "\Outworldzfiles\Opensim\WifiPages", True)
            My.Computer.FileSystem.CopyDirectory(Settings.CurrentDirectory & "\Outworldzfiles\Opensim\bin\WifiPages-" & Page, Settings.CurrentDirectory & "\Outworldzfiles\Opensim\bin\WifiPages", True)
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try

    End Sub

    Public Shared Function DoGloebits() As Boolean

        'Gloebits.ini
        If Settings.LoadIni(Settings.OpensimBinPath & "Gloebit.ini", ";") Then Return True

        Settings.SetIni("Gloebit", "Enabled", CStr(Settings.GloebitsEnable))
        Settings.SetIni("Gloebit", "GLBShowNewSessionAuthIM", CStr(Settings.GLBShowNewSessionAuthIM))
        Settings.SetIni("Gloebit", "GLBShowNewSessionPurchaseIM", CStr(Settings.GLBShowNewSessionPurchaseIM))
        Settings.SetIni("Gloebit", "GLBShowWelcomeMessage", CStr(Settings.GLBShowWelcomeMessage))

        If Settings.GloebitsMode Then
            Settings.SetIni("Gloebit", "GLBEnvironment", "production")
            Settings.SetIni("Gloebit", "GLBKey", Settings.GLProdKey)
            Settings.SetIni("Gloebit", "GLBSecret", Settings.GLProdSecret)
        Else
            Settings.SetIni("Gloebit", "GLBEnvironment", "sandbox")
            Settings.SetIni("Gloebit", "GLBKey", Settings.GLSandKey)
            Settings.SetIni("Gloebit", "GLBSecret", Settings.GLSandSecret)
        End If

        Settings.SetIni("Gloebit", "GLBOwnerName", Settings.GLBOwnerName)
        Settings.SetIni("Gloebit", "GLBOwnerEmail", Settings.GLBOwnerEmail)

        Settings.SetIni("Gloebit", "GLBSpecificConnectionString", Settings.RobustDBConnection)

        Settings.SaveINI(System.Text.Encoding.UTF8)
        Return False

    End Function

    Public Shared Sub ErrorLog(message As String)
        If Debugger.IsAttached Then
            MsgBox(message, vbInformation)
        End If
        Logger("Error", message, "Error")
    End Sub

    Public Shared Function ExternLocalServerName() As String
        ''' <summary>
        ''' Gets the External Host name which can be either the Public IP or a Host name.
        ''' </summary>
        ''' <returns>Host for regions</returns>
        Dim Host As String

        If Settings.ExternalHostName.Length > 0 Then
            Host = Settings.ExternalHostName
        Else
            Host = Settings.PublicIP
        End If
        Return Host

    End Function

    Public Shared Function GetDlls(fname As String) As List(Of String)

        Dim DllList As New List(Of String)

        If System.IO.File.Exists(fname) Then
            Dim line As String
            Using reader As StreamReader = System.IO.File.OpenText(fname)
                'now loop through each line
                While reader.Peek <> -1
                    line = reader.ReadLine()
                    DllList.Add(line)
                End While
            End Using
        End If
        Return DllList

    End Function

    Public Shared Function GetFilesRecursive(ByVal initial As String) As List(Of String)
        ''' <summary>
        ''' This method starts at the specified directory. It traverses all subdirectories. It
        ''' returns a List of those directories.
        ''' </summary>
        ''' ' This list stores the results.
        Dim result As New List(Of String)

        ' This stack stores the directories to process.
        Dim stack As New Stack(Of String)

        ' Add the initial directory
        stack.Push(initial)

        ' Continue processing for each stacked directory
        Do While (stack.Count > 0)
            ' Get top directory string
            Dim dir As String = stack.Pop

            ' Add all immediate file paths
            Try
                result.AddRange(Directory.GetFiles(dir, "*.dll"))
            Catch ex As Exception

                BreakPoint.Show(ex.Message)
            End Try

            ' Loop through all subdirectories and add them to the stack.
            Dim directoryName As String

            'Save, but skip script engines
            For Each directoryName In Directory.GetDirectories(dir)
                If Not directoryName.Contains("ScriptEngines") And
                    Not directoryName.Contains("fsassets") And
                    Not directoryName.Contains("assetcache") And
                    Not directoryName.Contains("j2kDecodeCache") Then
                    stack.Push(directoryName)
                End If
                Application.DoEvents()
            Next
            Application.DoEvents()
        Loop

        ' Return the list
        Return result
    End Function

    <CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId:="Outworldz.Form1.ErrorLog(System.String)")>
    Public Shared Function GetHostAddresses(hostName As String) As String

        Try
            Dim IPList As IPHostEntry = System.Net.Dns.GetHostEntry(hostName)

            For Each IPaddress In IPList.AddressList
                If (IPaddress.AddressFamily = Sockets.AddressFamily.InterNetwork) Then
                    Dim ip = IPaddress.ToString()
                    Return ip
                End If
                Application.DoEvents()
            Next
            Return String.Empty
        Catch ex As Exception

            BreakPoint.Show(ex.Message)
            ErrorLog("Warn:Unable to resolve name:" & ex.Message)
        End Try
        Return String.Empty

    End Function

    <CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId:="Outworldz.Form1.ErrorLog(System.String)")>
    Public Shared Function GetNewDnsName() As String

        Dim client As New WebClient
        Dim Checkname As String
        Try
            Checkname = client.DownloadString("http://outworldz.net/getnewname.plx/?r=" & RandomNumber.Random)
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
            ErrorLog("Error:Cannot get new name:" & ex.Message)
            client.Dispose()
            Return ""
        End Try
        client.Dispose()
        Return Checkname

    End Function

    Public Shared Function GetOpensimProto() As String
        ''' <summary>Loads the INI file for the proper grid type for parsing</summary>
        ''' <returns>Returns the path to the proper Opensim.ini prototype.</returns>
        Select Case Settings.ServerType
            Case "Robust"
                Settings.LoadIni(Settings.OpensimBinPath & "Opensim.proto", ";")
                Return Settings.OpensimBinPath & "Opensim.proto"
            Case "Region"
                Settings.LoadIni(Settings.OpensimBinPath & "OpensimRegion.proto", ";")
                Return Settings.OpensimBinPath & "OpensimRegion.proto"
            Case "OsGrid"
                Settings.LoadIni(Settings.OpensimBinPath & "OpensimOsGrid.proto", ";")
                Return Settings.OpensimBinPath & "OpensimOsGrid.proto"
            Case "Metro"
                Settings.LoadIni(Settings.OpensimBinPath & "OpensimMetro.proto", ";")
                Return Settings.OpensimBinPath & "OpensimMetro.proto"
        End Select
        ' just in case...
        Settings.LoadIni(Settings.OpensimBinPath & "Opensim.proto", ";")
        Return Settings.OpensimBinPath & "Opensim.proto"

    End Function

    Public Shared Sub Log(category As String, message As String)

        ''' <summary>Log(string) to Outworldz.log</summary>
        ''' <param name="message"></param>
        Logger(category, message, "Outworldz")

    End Sub

    Public Shared Sub Logger(category As String, message As String, file As String)
        Try
            Using outputFile As New StreamWriter(Settings.CurrentDirectory & "\OutworldzFiles\" & file & ".log", True)
                outputFile.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", Globalization.CultureInfo.InvariantCulture) & ":" & category & ":" & message)
                Diagnostics.Debug.Print(message)
            End Using
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try
    End Sub

    <CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId:="Outworldz.Form1.ErrorLog(System.String)")>
    Public Shared Function SetWindowTextCall(myProcess As Process, windowName As String) As Boolean
        ''' <summary>
        ''' SetWindowTextCall is here to wrap the SetWindowtext API call. This call fails when there
        ''' is no hwnd as Windows takes its sweet time to get that. Also, may fail to write the
        ''' title. It has a timer to make sure we do not get stuck
        ''' </summary>
        ''' <param name="hwnd">Handle to the window to change the text on</param>
        ''' <param name="windowName">the name of the Window</param>
        If myProcess Is Nothing Then
            ErrorLog("Process is nothing " & windowName)
            Return False
        End If

        Dim WindowCounter As Integer = 0
        Try
            While myProcess.MainWindowHandle = CType(0, IntPtr)
                Sleep(100)
                WindowCounter += 1
                If WindowCounter > 600 Then '  60 seconds for process to start
                    ErrorLog("Cannot get MainWindowHandle for " & windowName)
                    Return False
                End If
            End While
        Catch ex As Exception

            BreakPoint.Show(ex.Message)
            ErrorLog(windowName & ":" & ex.Message)
            Return False
        End Try

        Sleep(1000)

        WindowCounter = 0
        Dim hwnd As IntPtr = myProcess.MainWindowHandle
        While True
            Dim status = SetWindowText(hwnd, windowName)

            If status And myProcess.MainWindowTitle = windowName Then
                Exit While
            End If

            WindowCounter += 1
            If WindowCounter > 600 Then '  60 seconds
                ErrorLog("Cannot get handle for " & windowName)
                Exit While
            End If

        End While
        Return True

    End Function

    Public Shared Function ShowDOSWindow(handle As IntPtr, command As SHOWWINDOWENUM) As Boolean

        If Settings.ConsoleShow = "None" And command <> SHOWWINDOWENUM.SWMINIMIZE Then
            Return True
        End If

        Dim ctr = 50
        If handle <> IntPtr.Zero Then
            Dim HandleValid As Boolean = False

            While Not HandleValid And ctr > 0
                Try
                    HandleValid = ShowWindow(handle, command)
                    If HandleValid Then Return True
                Catch ex As Exception

                    BreakPoint.Show(ex.Message)
                End Try
                ctr -= 1
                Sleep(100)
                Application.DoEvents()
            End While
        End If
        Return False

    End Function

    Public Shared Sub Sleep(value As Integer)
        ''' <summary>Sleep(ms)</summary>
        ''' <param name="value">millseconds</param>
        ' value is in milliseconds, but we do it in 10 passes so we can doevents() to free up
        ' console
        Dim sleeptime = value / 100  ' now in tenths

        While sleeptime > 0
            Application.DoEvents()
            Thread.Sleep(100)
            sleeptime -= 1
        End While

    End Sub

    Public Shared Function ToLowercaseKeys(Str As String) As String

        If My.Computer.Keyboard.CapsLock Then
            For Pos = 1 To Len(Str)
                Dim C As String = Mid(Str, Pos, 1)
                Mid(Str, Pos) = CStr(IIf(UCase(C) = C, LCase(C), UCase(C)))
            Next
        End If
        Return Str

    End Function

#End Region

#Region "Public Function"

    Public Function AvatarsIsInGroup(groupname As String) As Boolean

        Dim present As Integer = 0
        For Each RegionUUID As String In PropRegionClass.RegionUUIDListByName(groupname)
            present += PropRegionClass.AvatarCount(RegionUUID)
        Next

        Return CType(present, Boolean)

    End Function

    Public Sub BackupDB()

        If Not StartMySQL() Then
            ToolBar(False)
            Buttons(StartButton)
            Print(My.Resources.Stopped_word)
            Return
        End If

        Print(My.Resources.Slow_Backup)
        Using pMySqlBackup As Process = New Process()
            Dim pi As ProcessStartInfo = New ProcessStartInfo With {
            .Arguments = "",
            .WindowStyle = ProcessWindowStyle.Normal,
            .WorkingDirectory = Settings.CurrentDirectory & "\OutworldzFiles\mysql\bin\",
            .FileName = Settings.CurrentDirectory & "\OutworldzFiles\mysql\bin\BackupMysql.bat"
            }
            pMySqlBackup.StartInfo = pi
            Try
                pMySqlBackup.Start()
            Catch ex As Exception

                BreakPoint.Show(ex.Message)
            End Try

        End Using

    End Sub

    Public Function BackupPath() As String

        'Autobackup must exist. if not create it
        ' if they set the folder somewhere else, it may have been deleted, so reset it to default
        If Settings.BackupFolder.ToUpper(Globalization.CultureInfo.InvariantCulture) = "AUTOBACKUP" Then
            BackupPath = PropCurSlashDir & "/OutworldzFiles/AutoBackup/"
            If Not Directory.Exists(BackupPath) Then
                MkDir(BackupPath)
            End If
        Else
            BackupPath = Settings.BackupFolder & "/"
            BackupPath = BackupPath.Replace("\", "/")    ' because Opensim uses Unix-like slashes, that's why

            If Not Directory.Exists(BackupPath) Then
                BackupPath = PropCurSlashDir & "/OutworldzFiles/Autobackup/"

                If Not Directory.Exists(BackupPath) Then
                    MkDir(BackupPath)
                End If

                MsgBox(My.Resources.Autobackup_cannot_be_located & BackupPath)
                Settings.BackupFolder = "AutoBackup"
                Settings.SaveSettings()
            End If
        End If

    End Function

    <CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId:="Outworldz.Form1.ErrorLog(System.String)")>
    <CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId:="Outworldz.Form1.Log(System.String,System.String)")>
    Public Function Boot(Regionclass As RegionMaker, BootName As String) As Boolean
        ''' <summary>Starts Opensim for a given name</summary>
        ''' <param name="BootName">Name of region to start</param>
        ''' <returns>success = true</returns>

        If Regionclass Is Nothing Then Return False
        If RegionMaker.Instance Is Nothing Then Return False

        ' Allow these to change w/o rebooting
        DoOpensimProtoINI()
        DoGloebits()

        Timer1.Interval = 1000
        Timer1.Start() 'Timer starts functioning
        PropOpensimIsRunning() = True

        If PropAborting Then Return True

        Dim RegionUUID As String = Regionclass.FindRegionByName(BootName)
        Dim GroupName = Regionclass.GroupName(RegionUUID)

        If String.IsNullOrEmpty(RegionUUID) Then
            ErrorLog("Cannot find " & BootName & " to boot!")
            Logger("Cannot find", BootName, "Restart")
            Return False
        End If
        Log(My.Resources.Info_word, "Region: Starting Region " & BootName)

        If Regionclass.IsBooted(RegionUUID) Then
            Logger("Already Running", BootName, "Restart")
            Log(My.Resources.Info_word, "Region " & BootName & " already running")
            PropUpdateView = True ' make form refresh
            Return True
        End If

        If Regionclass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.RecyclingUp Then
            Logger("Already warming up", BootName, "Restart")
            Log(My.Resources.Info_word, "Region " & BootName & " skipped as it is already Warming Up")
            PropUpdateView = True ' make form refresh
            Return True
        End If

        If Regionclass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.Booting Then
            Logger("Already booting up", BootName, "Restart")
            Log(My.Resources.Info_word, "Region " & BootName & " skipped as it is already Booting Up")
            PropUpdateView = True ' make form refresh
            Return True
        End If

        If Regionclass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.ShuttingDown Then
            Logger("Cannot boot, shutting down", BootName, "Restart")
            Log(My.Resources.Info_word, "Region " & BootName & " skipped as it is already Shutting Down")
            PropUpdateView = True ' make form refresh
            Return True
        End If

        If Regionclass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.RecyclingDown Then
            Logger("Already Recycling Down", BootName, "Restart")
            Log(My.Resources.Info_word, "Region " & BootName & " skipped as it is already Recycling Down")
            PropUpdateView = True ' make form refresh
            Return True
        End If

        If Regionclass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.Suspended Then
            Logger("Suspended, Resuming it", BootName, "Restart")
            Regionclass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.Resume
            Log(My.Resources.Info_word, "Region " & BootName & " skipped as it is Suspended, Resuming it instead")
            PropUpdateView = True ' make form refresh
            Return True
        End If

        DoRegion(BootName, RegionUUID) ' setup region ini file

        Dim isRegionRunning As Boolean = False

        For Each p In Process.GetProcesses
            Application.DoEvents()
            If p.MainWindowTitle = GroupName Then
                isRegionRunning = True
                Exit For
            End If
        Next

        If Not isRegionRunning Then isRegionRunning = CheckPort("127.0.0.1", Regionclass.GroupPort(RegionUUID))
        Application.DoEvents()
        If isRegionRunning Then
            Print(GroupName & " " & Global.Outworldz.My.Resources.is_already_running_word)
            Logger(My.Resources.is_already_running_word, GroupName, "Restart")
            ' if running, grab it and return
            If Regionclass.ProcessID(RegionUUID) = 0 Then
                Dim listP = Process.GetProcesses
                For Each p In listP
                    Application.DoEvents()
                    If p.MainWindowTitle = GroupName Then
                        If Not PropRegionHandles.ContainsKey(p.Id) Then
                            PropRegionHandles.Add(p.Id, GroupName) ' save in the list of exit events in case it crashes or exits
                        End If

                        For Each RegionUUID In Regionclass.RegionUUIDListByName(GroupName)
                            Regionclass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.Booted ' force it up
                            Regionclass.Timer(RegionUUID) = RegionMaker.REGIONTIMER.StartCounting
                            Regionclass.ProcessID(RegionUUID) = p.Id
                        Next

                        Logger("Located, is already running", BootName, "Restart")
                        Select Case Settings.ConsoleShow
                            Case "True"
                            ' Do nothing, Always Show
                            Case "False"
                                ShowDOSWindow(GetHwnd(GroupName), SHOWWINDOWENUM.SWMINIMIZE)
                            Case "None"
                                ShowDOSWindow(GetHwnd(GroupName), SHOWWINDOWENUM.SWMINIMIZE)
                        End Select
                        PropUpdateView = True ' make form refresh
                        Return True
                    End If
                Next
                ErrorLog("Cannot find Window " & GroupName)
                Logger("Cannot find Window", GroupName, "Restart")
                Return False
            Else
                If Not PropRegionHandles.ContainsKey(Regionclass.ProcessID(RegionUUID)) Then
                    PropRegionHandles.Add(Regionclass.ProcessID(RegionUUID), GroupName) ' save in the list of exit events in case it crashes or exits
                End If

                For Each UUID As String In Regionclass.RegionUUIDListByName(GroupName)
                    Regionclass.Status(UUID) = RegionMaker.SIMSTATUSENUM.Booted ' force it up
                    Regionclass.Timer(UUID) = RegionMaker.REGIONTIMER.StartCounting
                Next

                Logger("Booting Up", BootName, "Restart")

                PropUpdateView = True ' make form refresh
                Return True
            End If
        Else
            If Regionclass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.Suspended Then
                Regionclass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.Resume
                Regionclass.Timer(RegionUUID) = RegionMaker.REGIONTIMER.StartCounting
                Log(My.Resources.Info_word, GroupName & " skipped as it is Suspended, Resuming it instead")
                Logger("Suspended, Resuming it instead", GroupName, "Restart")
                PropUpdateView = True ' make form refresh
                Return True
            End If
        End If

        Environment.SetEnvironmentVariable("OSIM_LOGPATH", Settings.OpensimBinPath() & "Regions\" & GroupName)
        Environment.SetEnvironmentVariable("OSIM_LOGLEVEL", Settings.LogLevel.ToUpperInvariant)

        Dim myProcess As Process = GetNewProcess()

        Print(BootName & " " & Global.Outworldz.My.Resources.Starting_word)

        myProcess.EnableRaisingEvents = True
        myProcess.StartInfo.UseShellExecute = True ' so we can redirect streams
        myProcess.StartInfo.WorkingDirectory = Settings.OpensimBinPath()

        myProcess.StartInfo.FileName = """" & Settings.OpensimBinPath() & "OpenSim.exe" & """"
        myProcess.StartInfo.CreateNoWindow = False

        Select Case Settings.ConsoleShow
            Case "True"
                myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal
            Case "False"
                myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal
            Case "None"
                myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Minimized
        End Select

        myProcess.StartInfo.Arguments = " -inidirectory=" & """" & "./Regions/" & GroupName & """"

        FileStuff.DeleteFile(Settings.OpensimBinPath() & "Regions\" & GroupName & "\Opensim.log")
        FileStuff.DeleteFile(Settings.OpensimBinPath() & "Regions\" & GroupName & "\PID.pid")
        FileStuff.DeleteFile(Settings.OpensimBinPath() & "regions\" & GroupName & "\OpensimConsole.log")
        FileStuff.DeleteFile(Settings.OpensimBinPath() & "regions\" & GroupName & "\OpenSimStats.log")

        SequentialPause()   ' wait for previous region to give us some CPU
        Logger("Booting", GroupName, "Restart")
        Dim ok As Boolean = False
        Try
            ok = myProcess.Start
            Application.DoEvents()
        Catch ex As Exception

            BreakPoint.Show(ex.Message)
            ErrorLog(ex.Message)
        End Try

        If ok Then

            Dim PID = WaitForPID(myProcess)
            ' check if it gave us a PID, if not, it failed.
            If PID = 0 Then
                Regionclass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.Error
                PropUpdateView = True ' make form refresh
                Print("Failed to get a PID for region " & GroupName)
                Logger("No PID", GroupName, "Restart")
                Return False
            End If
            SetWindowTextCall(myProcess, GroupName)
            For Each UUID As String In Regionclass.RegionUUIDListByName(GroupName)
                Log("Debug", "Process started for " & Regionclass.RegionName(UUID) & " PID=" & CStr(myProcess.Id) & " UUID:" & CStr(UUID))
                Regionclass.Status(UUID) = RegionMaker.SIMSTATUSENUM.Booting
                Regionclass.ProcessID(UUID) = PID
            Next

            PropUpdateView = True ' make form refresh

            Log("Debug", "Created Process Number " & CStr(myProcess.Id) & " in  RegionHandles(" & CStr(PropRegionHandles.Count) & ") " & "Group:" & GroupName)
            If Not PropRegionHandles.ContainsKey(myProcess.Id) Then
                PropRegionHandles.Add(myProcess.Id, GroupName) ' save in the list of exit events in case it crashes or exits
            End If

            Buttons(StopButton)
            Return True
        End If
        Logger("Failed to boot ", BootName, "Restart")
        Print("Failed to boot region " & BootName)
        Return False

    End Function

    Public Sub Buttons(b As Button)

        If b Is Nothing Then Return
        ' Turns off all 3 stacked buttons, then enables one of them
        BusyButton.Visible = False
        StopButton.Visible = False
        StartButton.Visible = False

        b.Visible = True

    End Sub

    <CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId:="Outworldz.Form1.ErrorLog(System.String)")>
    Public Sub CheckForUpdates()

        Using client As New WebClient ' download client for web pages
            Print(My.Resources.Checking_for_Updates_word)
            Try
                Update_version = client.DownloadString(PropDomain() & "/Outworldz_Installer/UpdateGrid.plx?fill=1" & GetPostData())
            Catch ex As Exception

                BreakPoint.Show(ex.Message)
                ErrorLog(My.Resources.Wrong & " " & ex.Message)
                Return
            End Try
        End Using

        ' Update Error check could be nothing
        If Update_version.Length = 0 Then Update_version = PropMyVersion

        Try
            Dim uv As Single = 0
            uv = Convert.ToSingle(Update_version, Globalization.CultureInfo.InvariantCulture)
            Dim ToVersion = Convert.ToSingle(Settings.SkipUpdateCheck, Globalization.CultureInfo.InvariantCulture)
            ' could be the same or later version already
            If uv <= ToVersion Then
                Return
            End If
            If uv <= Convert.ToSingle(PropMyVersion, Globalization.CultureInfo.InvariantCulture) Then
                Return
            End If
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
            Return
        End Try

        ' may need to get the new file
        If System.IO.File.Exists(Settings.CurrentDirectory & "\DreamGrid-V" & Update_version & ".zip") Then
            Dim result = MsgBox("V" & Update_version & " " & Global.Outworldz.My.Resources.Update_Downloaded, vbYesNo)
            If result = MsgBoxResult.Yes Then

                Dim BackupForm As New FormBackupCheckboxes
                Dim ret = BackupForm.ShowDialog()
                BackupForm.Dispose()

                If ret = DialogResult.OK Then
                    UpdaterGo("DreamGrid-V" & Update_version & ".zip")
                    Return
                End If
            Else
                Settings.SkipUpdateCheck() = Update_version
                Settings.SaveSettings()
                Return
            End If
        End If

        ' we already have the file
        If System.IO.File.Exists(Settings.CurrentDirectory & "\DreamGrid-V" & Update_version & ".zip") Then
            Return
        End If

        Print(My.Resources.Update_is_available & ":" & Update_version)
        Dim pi As ProcessStartInfo = New ProcessStartInfo With {
                .Arguments = "DreamGrid-V" & Update_version & ".zip",
                .WindowStyle = ProcessWindowStyle.Hidden,
                .FileName = """" & Settings.CurrentDirectory & "\Downloader.exe" & """"
            }

        UpdateProcess.StartInfo = pi
        UpdateProcess.EnableRaisingEvents = True
        Try
            UpdateProcess.Start()
        Catch ex As Exception

            BreakPoint.Show(ex.Message)
            Print(My.Resources.ErrUpdate)
        End Try

    End Sub

    Public Function ConsoleCommand(RegionUUID As String, command As String) As Boolean

        ''' <summary>
        ''' Sends keystrokes to Opensim. Always sends and enter button before to clear and use keys
        ''' </summary>
        ''' <param name="ProcessID">PID of the DOS box</param>
        ''' <param name="command">String</param>
        ''' <returns></returns>
        If command Is Nothing Then Return False
        If command.Length > 0 Then

            Dim ShowDosBox As Boolean = True
            Select Case Settings.ConsoleShow
                Case "True"
                    ShowDosBox = True
                Case "False"
                    ShowDosBox = True
                Case ""
                    ShowDosBox = False
            End Select

            Dim PID As Integer
            If RegionUUID <> RobustName() And RegionUUID <> "Robust" Then

                PID = PropRegionClass.ProcessID(RegionUUID)
                Application.DoEvents()
                Try
                    If PID > 0 And ShowDosBox Then
                        ShowDOSWindow(Process.GetProcessById(PID).MainWindowHandle, SHOWWINDOWENUM.SWRESTORE)
                    End If
                Catch ex As Exception

                    BreakPoint.Show(ex.Message)
                    Return False
                End Try
            Else ' Robust
                PID = PropRobustProcID
                Try
                    If ShowDosBox Then ShowDOSWindow(Process.GetProcessById(PID).MainWindowHandle, SHOWWINDOWENUM.SWRESTORE)
                Catch ex As Exception
                    BreakPoint.Show(ex.Message)
                    Return False
                End Try
            End If

            Application.DoEvents()

            'plus sign(+), caret(^), percent sign (%), tilde (~), And parentheses ()
            command = command.Replace("+", "{+}")
            command = command.Replace("^", "{^}")
            command = command.Replace("%", "{%}")
            command = command.Replace("(", "{(}")
            command = command.Replace(")", "{)}")

            If PID > 0 Then
                Try

                    AppActivate(PID)
                    Application.DoEvents()

                    SendKeys.SendWait(ToLowercaseKeys("{ENTER}" & vbCrLf))
                    SendKeys.SendWait(ToLowercaseKeys(command))
                    Application.DoEvents()
                    Select Case Settings.ConsoleShow
                        Case "True"
                        ' do nothing, already up
                        Case "False"
                            ShowDOSWindow(Process.GetProcessById(PID).MainWindowHandle, SHOWWINDOWENUM.SWMINIMIZE)
                        Case ""
                            ShowDOSWindow(Process.GetProcessById(PID).MainWindowHandle, SHOWWINDOWENUM.SWMINIMIZE)
                    End Select
                Catch ex As Exception
                    'BreakPoint.Show(ex.Message)
                    Return False
                End Try
            End If

        End If

        Return True

    End Function

    Public Sub DelLibrary()

        Print("->Set Library")
        If IO.File.Exists(Settings.OpensimBinPath & "Library\Clothing Library (small).iar") Then
            System.IO.File.Delete(Settings.OpensimBinPath & "Library\Clothing Library (small).iar")
        End If
        If IO.File.Exists(Settings.OpensimBinPath & "Library\Objects Library (small).iar") Then
            System.IO.File.Delete(Settings.OpensimBinPath & "Library\Objects Library (small).iar")
        End If

    End Sub

    Public Function DoBirds() As Boolean

        If Not Settings.BirdsModuleStartup Then Return False
        Print("->Set Birds")
        Dim BirdFile = Settings.OpensimBinPath & "addon-modules\OpenSimBirds\config\OpenSimBirds.ini"

        If IO.File.Exists(BirdFile) Then
            System.IO.File.Delete(BirdFile)
        End If

        Dim BirdData As String = ""

        ' Birds setup per region
        For Each RegionUUID As String In PropRegionClass.RegionUUIDs
            Application.DoEvents()
            Dim RegionName = PropRegionClass.RegionName(RegionUUID)

            If Settings.LoadIni(PropRegionClass.RegionPath(RegionUUID), ";") Then Return True

            If Settings.BirdsModuleStartup And PropRegionClass.Birds(RegionUUID) = "True" Then

                BirdData = BirdData & "[" & RegionName & "]" & vbCrLf &
            ";this Is the default And determines whether the module does anything" & vbCrLf &
            "BirdsModuleStartup = True" & vbCrLf & vbCrLf &
            ";set to false to disable the birds from appearing in this region" & vbCrLf &
            "BirdsEnabled = True" & vbCrLf & vbCrLf &
            ";which channel do we listen on for in world commands" & vbCrLf &
            "BirdsChatChannel = " & CStr(Settings.BirdsChatChannel) & vbCrLf & vbCrLf &
            ";the number of birds to flock" & vbCrLf &
            "BirdsFlockSize = " & CStr(Settings.BirdsFlockSize) & vbCrLf & vbCrLf &
            ";how far each bird can travel per update" & vbCrLf &
            "BirdsMaxSpeed = " & Settings.BirdsMaxSpeed.ToString(Globalization.CultureInfo.InvariantCulture) & vbCrLf & vbCrLf &
            ";the maximum acceleration allowed to the current velocity of the bird" & vbCrLf &
            "BirdsMaxForce = " & Settings.BirdsMaxForce.ToString(Globalization.CultureInfo.InvariantCulture) & vbCrLf & vbCrLf &
            ";max distance for other birds to be considered in the same flock as us" & vbCrLf &
            "BirdsNeighbourDistance = " & Settings.BirdsNeighbourDistance.ToString(Globalization.CultureInfo.InvariantCulture) & vbCrLf & vbCrLf &
            ";how far away from other birds we would Like To stay" & vbCrLf &
            "BirdsDesiredSeparation = " & Settings.BirdsDesiredSeparation.ToString(Globalization.CultureInfo.InvariantCulture) & vbCrLf & vbCrLf &
            ";how close To the edges Of things can we Get without being worried" & vbCrLf &
            "BirdsTolerance = " & Settings.BirdsTolerance.ToString(Globalization.CultureInfo.InvariantCulture) & vbCrLf & vbCrLf &
            ";how close To the edge Of a region can we Get?" & vbCrLf &
            "BirdsBorderSize = " & Settings.BirdsBorderSize.ToString(Globalization.CultureInfo.InvariantCulture) & vbCrLf & vbCrLf &
            ";how high are we allowed To flock" & vbCrLf &
            "BirdsMaxHeight = " & Settings.BirdsMaxHeight.ToString(Globalization.CultureInfo.InvariantCulture) & vbCrLf & vbCrLf &
            ";By Default the Module will create a flock Of plain wooden spheres," & vbCrLf &
            ";however this can be overridden To the name Of an existing prim that" & vbCrLf &
            ";needs To already exist In the scene - i.e. be rezzed In the region." & vbCrLf &
            "BirdsPrim = " & Settings.BirdsPrim & vbCrLf & vbCrLf &
            ";who Is allowed to send commands via chat Or script List of UUIDs Or ESTATE_OWNER Or ESTATE_MANAGER" & vbCrLf &
            ";Or everyone if Not specified" & vbCrLf &
            "BirdsAllowedControllers = ESTATE_OWNER, ESTATE_MANAGER" & vbCrLf & vbCrLf & vbCrLf

            End If
        Next
        IO.File.WriteAllText(BirdFile, BirdData, Encoding.Default) 'The text file will be created if it does not already exist

        Return False

    End Function

    Public Function DoIceCast() As Boolean

        Print("->Set IceCast")
        Dim rgx As New Regex("[^a-zA-Z0-9 ]")
        Dim name As String = rgx.Replace(Settings.SimName, "")

        Dim icecast As String = "<icecast>" & vbCrLf +
                           "<hostname>" & Settings.PublicIP & "</hostname>" & vbCrLf +
                            "<location>" & name & "</location>" & vbCrLf +
                            "<admin>" & Settings.AdminEmail & "</admin>" & vbCrLf +
                            "<shoutcast-mount>/stream</shoutcast-mount>" & vbCrLf +
                            "<listen-socket>" & vbCrLf +
                            "    <port>" & CStr(Settings.SCPortBase) & "</port>" & vbCrLf +
                            "</listen-socket>" & vbCrLf +
                            "<listen-socket>" & vbCrLf +
                            "   <port>" & CStr(Settings.SCPortBase1) & "</port>" & vbCrLf +
                            "   <shoutcast-compat>1</shoutcast-compat>" & vbCrLf +
                            "</listen-socket>" & vbCrLf +
                             "<limits>" & vbCrLf +
                              "   <clients>20</clients>" & vbCrLf +
                              "    <sources>4</sources>" & vbCrLf +
                              "    <queue-size>524288</queue-size>" & vbCrLf +
                              "     <client-timeout>30</client-timeout>" & vbCrLf +
                              "    <header-timeout>15</header-timeout>" & vbCrLf +
                              "    <source-timeout>10</source-timeout>" & vbCrLf +
                              "    <burst-on-connect>1</burst-on-connect>" & vbCrLf +
                              "    <burst-size>65535</burst-size>" & vbCrLf +
                              "</limits>" & vbCrLf +
                              "<authentication>" & vbCrLf +
                                  "<source-password>" & Settings.SCPassword & "</source-password>" & vbCrLf +
                                  "<relay-password>" & Settings.SCPassword & "</relay-password>" & vbCrLf +
                                  "<admin-user>admin</admin-user>" & vbCrLf +
                                  "<admin-password>" & Settings.SCAdminPassword & "</admin-password>" & vbCrLf +
                              "</authentication>" & vbCrLf +
                              "<http-headers>" & vbCrLf +
                              "    <header name=" & """" & "Access-Control-Allow-Origin" & """" & " value=" & """" & "*" & """" & "/>" & vbCrLf +
                              "</http-headers>" & vbCrLf +
                              "<fileserve>1</fileserve>" & vbCrLf +
                              "<paths>" & vbCrLf +
                                  "<logdir>./log</logdir>" & vbCrLf +
                                  "<webroot>./web</webroot>" & vbCrLf +
                                  "<adminroot>./admin</adminroot>" & vbCrLf &  '
                                   "<alias source=" & """" & "/" & """" & " destination=" & """" & "/status.xsl" & """" & "/>" & vbCrLf +
                              "</paths>" & vbCrLf +
                              "<logging>" & vbCrLf +
                                  "<accesslog>access.log</accesslog>" & vbCrLf +
                                  "<errorlog>error.log</errorlog>" & vbCrLf +
                                  "<loglevel>3</loglevel>" & vbCrLf +
                                  "<logsize>10000</logsize>" & vbCrLf +
                              "</logging>" & vbCrLf +
                          "</icecast>" & vbCrLf

        Using outputFile As New StreamWriter(Settings.CurrentDirectory & "\Outworldzfiles\Icecast\icecast_run.xml", False)
            outputFile.WriteLine(icecast)
        End Using

        Return False

    End Function

    Public Function DoOpensimProtoINI() As Boolean

        ' Opensim.ini
        Settings.LoadIni(GetOpensimProto(), ";")

        Select Case Settings.ServerType
            Case "Robust"

                SetupOpensimSearchINI()

                Settings.SetIni("Const", "PrivURL", "http://" & Settings.PrivateURL)
                Settings.SetIni("Const", "GridName", Settings.SimName)

            Case "Region"

                SetupOpensimSearchINI()

            Case "OSGrid"
            Case "Metro"

        End Select

        Settings.SetIni("Const", "ApachePort", CStr(Settings.ApachePort))

        ' Support viewers object cache, default true users may need to reduce viewer bandwidth if
        ' some prims Or terrain parts fail to rez. change to false if you need to use old viewers
        ' that do Not support this feature

        Settings.SetIni("ClientStack.LindenUDP", "SupportViewerObjectsCache", CStr(Settings.SupportViewerObjectsCache))

        'ScriptEngine
        Settings.SetIni("Startup", "DefaultScriptEngine", Settings.ScriptEngine)

        If Settings.ScriptEngine = "XEngine" Then
            Settings.SetIni("Startup", "DefaultScriptEngine", "XEngine")
            Settings.SetIni("XEngine", "Enabled", "True")
            Settings.SetIni("YEngine", "Enabled", "False")
        Else
            Settings.SetIni("Startup", "DefaultScriptEngine", "YEngine")
            Settings.SetIni("XEngine", "Enabled", "False")
            Settings.SetIni("YEngine", "Enabled", "True")
        End If

        ' set new Min Timer Interval for how fast a script can go.
        Settings.SetIni("XEngine", "MinTimerInterval", CStr(Settings.MinTimerInterval))
        Settings.SetIni("YEngine", "MinTimerInterval", CStr(Settings.MinTimerInterval))

        ' all grids requires these setting in Opensim.ini
        Settings.SetIni("Const", "DiagnosticsPort", CStr(Settings.DiagnosticPort))

        ' Get Opensimulator Scripts to date if needed
        If Settings.DeleteScriptsOnStartupLevel <> SimVersion Then

            ClrCache.WipeScripts()
            Settings.DeleteScriptsOnStartupLevel() = SimVersion ' we have scripts cleared to proper Opensim Version
            Settings.SaveSettings()

            Settings.SetIni("XEngine", "DeleteScriptsOnStartup", "True")
        Else
            Settings.SetIni("XEngine", "DeleteScriptsOnStartup", "False")
        End If

        If Settings.LSLHTTP Then
            ' do nothing - let them edit it
        Else
            Settings.SetIni("Network", "OutboundDisallowForUserScriptsExcept", Settings.PrivateURL & "/32")
        End If

        Settings.SetIni("PrimLimitsModule", "EnforcePrimLimits", CStr(Settings.Primlimits))

        If Settings.Primlimits Then
            Settings.SetIni("Permissions", "permissionmodules", "DefaultPermissionsModule, PrimLimitsModule")
        Else
            Settings.SetIni("Permissions", "permissionmodules", "DefaultPermissionsModule")
        End If

        If Settings.GloebitsEnable Then
            Settings.SetIni("Startup", "economymodule", "Gloebit")
        ElseIf Settings.CMS = "JOpensim" Then
            Settings.SetIni("Startup", "economymodule", "jOpenSimMoneyModule")
        Else
            Settings.SetIni("Startup", "economymodule", "BetaGridLikeMoneyModule")
        End If

        ' Main Frame time
        ' This defines the rate of several simulation events.
        ' Default value should meet most needs.
        ' It can be reduced To improve the simulation Of moving objects, with possible increase of CPU and network loads.
        'FrameTime = 0.0909

        Settings.SetIni("Startup", "FrameTime", Convert.ToString(1 / 11, Globalization.CultureInfo.InvariantCulture))

        ' LSL emails
        Settings.SetIni("SMTP", "SMTP_SERVER_HOSTNAME", Settings.SmtpHost)
        Settings.SetIni("SMTP", "SMTP_SERVER_PORT", CStr(Settings.SmtpPort))
        Settings.SetIni("SMTP", "SMTP_SERVER_LOGIN", Settings.SmtPropUserName)
        Settings.SetIni("SMTP", "SMTP_SERVER_PASSWORD", Settings.SmtpPassword)
        Settings.SetIni("SMTP", "host_domain_header_from", Settings.BaseHostName)

        ' the old Clouds
        If Settings.Clouds Then
            Settings.SetIni("Cloud", "enabled", "True")
            Settings.SetIni("Cloud", "density", Settings.Density.ToString(Globalization.CultureInfo.InvariantCulture))
        Else
            Settings.SetIni("Cloud", "enabled", "False")
        End If

        ' Physics choices for meshmerizer, where ODE requires a special one ZeroMesher meshing =
        ' Meshmerizer meshing = ubODEMeshmerizer 0 = none 1 = OpenDynamicsEngine 2 = BulletSim 3 =
        ' BulletSim with threads 4 = ubODE

        Select Case Settings.Physics
            Case 0
                Settings.SetIni("Startup", "meshing", "ZeroMesher")
                Settings.SetIni("Startup", "physics", "basicphysics")
                Settings.SetIni("Startup", "UseSeparatePhysicsThread", "False")
            Case 1
                Settings.SetIni("Startup", "meshing", "Meshmerizer")
                Settings.SetIni("Startup", "physics", "OpenDynamicsEngine")
                Settings.SetIni("Startup", "UseSeparatePhysicsThread", "False")
            Case 2
                Settings.SetIni("Startup", "meshing", "Meshmerizer")
                Settings.SetIni("Startup", "physics", "BulletSim")
                Settings.SetIni("Startup", "UseSeparatePhysicsThread", "False")
            Case 3
                Settings.SetIni("Startup", "meshing", "Meshmerizer")
                Settings.SetIni("Startup", "physics", "BulletSim")
                Settings.SetIni("Startup", "UseSeparatePhysicsThread", "True")
            Case 4
                Settings.SetIni("Startup", "meshing", "ubODEMeshmerizer")
                Settings.SetIni("Startup", "physics", "ubODE")
                Settings.SetIni("Startup", "UseSeparatePhysicsThread", "False")
            Case 5
                Settings.SetIni("Startup", "meshing", "Meshmerizer")
                Settings.SetIni("Startup", "physics", "ubODE")
                Settings.SetIni("Startup", "UseSeparatePhysicsThread", "False")
            Case Else
                Settings.SetIni("Startup", "meshing", "Meshmerizer")
                Settings.SetIni("Startup", "physics", "BulletSim")
                Settings.SetIni("Startup", "UseSeparatePhysicsThread", "True")
        End Select

        Settings.SetIni("Map", "RenderMaxHeight", Convert.ToString(Settings.RenderMaxHeight, Globalization.CultureInfo.InvariantCulture))
        Settings.SetIni("Map", "RenderMinHeight", Convert.ToString(Settings.RenderMinHeight, Globalization.CultureInfo.InvariantCulture))

        If Settings.MapType = "None" Then
            Settings.SetIni("Map", "GenerateMaptiles", "False")
        ElseIf Settings.MapType = "Simple" Then
            Settings.SetIni("Map", "GenerateMaptiles", "True")
            Settings.SetIni("Map", "MapImageModule", "MapImageModule")  ' versus Warp3DImageModule
            Settings.SetIni("Map", "TextureOnMapTile", "False")         ' versus true
            Settings.SetIni("Map", "DrawPrimOnMapTile", "False")
            Settings.SetIni("Map", "TexturePrims", "False")
            Settings.SetIni("Map", "RenderMeshes", "False")
        ElseIf Settings.MapType = "Good" Then
            Settings.SetIni("Map", "GenerateMaptiles", "True")
            Settings.SetIni("Map", "MapImageModule", "Warp3DImageModule")  ' versus MapImageModule
            Settings.SetIni("Map", "TextureOnMapTile", "False")         ' versus true
            Settings.SetIni("Map", "DrawPrimOnMapTile", "False")
            Settings.SetIni("Map", "TexturePrims", "False")
            Settings.SetIni("Map", "RenderMeshes", "False")
        ElseIf Settings.MapType = "Better" Then
            Settings.SetIni("Map", "GenerateMaptiles", "True")
            Settings.SetIni("Map", "MapImageModule", "Warp3DImageModule")  ' versus MapImageModule
            Settings.SetIni("Map", "TextureOnMapTile", "True")         ' versus true
            Settings.SetIni("Map", "DrawPrimOnMapTile", "True")
            Settings.SetIni("Map", "TexturePrims", "False")
            Settings.SetIni("Map", "RenderMeshes", "False")
        ElseIf Settings.MapType = "Best" Then
            Settings.SetIni("Map", "GenerateMaptiles", "True")
            Settings.SetIni("Map", "MapImageModule", "Warp3DImageModule")  ' versus MapImageModule
            Settings.SetIni("Map", "TextureOnMapTile", "True")      ' versus true
            Settings.SetIni("Map", "DrawPrimOnMapTile", "True")
            Settings.SetIni("Map", "TexturePrims", "True")
            Settings.SetIni("Map", "RenderMeshes", "True")
        End If

        ' Voice
        If Settings.VivoxEnabled Then
            Settings.SetIni("VivoxVoice", "enabled", "True")
        Else
            Settings.SetIni("VivoxVoice", "enabled", "False")
        End If
        Settings.SetIni("VivoxVoice", "vivox_admin_user", Settings.VivoxUserName)
        Settings.SetIni("VivoxVoice", "vivox_admin_password", Settings.VivoxPassword)

        Settings.SaveINI(System.Text.Encoding.UTF8)

        Return False
    End Function

    Public Sub DoStopActions()

        Print(My.Resources.Stopping_word)
        Buttons(BusyButton)
        If Not KillAll() Then Return
        Buttons(StartButton)
        Print(My.Resources.Stopped_word)
        Buttons(StartButton)
        ToolBar(False)

    End Sub

    Public Function GetHwnd(Groupname As String) As IntPtr

        If Groupname = RobustName() Then

            For Each pList As Process In Process.GetProcesses()
                If pList.ProcessName = "Robust" Then
                    Return pList.MainWindowHandle
                End If
            Next

            Dim h As IntPtr = IntPtr.Zero
            Return h

        End If

        Dim Regionlist = PropRegionClass.RegionUUIDListByName(Groupname)

        For Each RegionUUID As String In Regionlist
            Dim pid = PropRegionClass.ProcessID(RegionUUID)

            For Each pList As Process In Process.GetProcesses()
                If pList.Id = pid Then
                    Return pList.MainWindowHandle
                End If
                Application.DoEvents()
            Next
        Next
        Return IntPtr.Zero

    End Function

    Public Function GetNewProcess() As Process
        ''' <summary>Creates and exit handler for each region</summary>
        ''' <returns>a process handle</returns>
        Dim handle = New Handler
        Return handle.Init(PropRegionHandles, PropExitList)

    End Function

    Public Function GetPostData() As String

        Dim Grid As String = "Grid"

        Dim data As String = "&MachineID=" & Settings.MachineID() _
        & "&FriendlyName=" & WebUtility.UrlEncode(Settings.SimName) _
        & "&V=" & WebUtility.UrlEncode(PropMyVersion) _
        & "&OV=" & WebUtility.UrlEncode(PropSimVersion) _
        & "&Type=" & CStr(Grid) _
        & "&isPublic=" & CStr(Settings.GDPR()) _
        & "&r=" & RandomNumber.Random()
        Return data

    End Function

    ''' <summary>Check is Robust port 8002 is up</summary>
    ''' <returns>boolean</returns>
    Public Function IsRobustRunning() As Boolean

        Using client As New WebClient ' download client for web pages
            Dim Up As String
            Try
                Up = client.DownloadString("http://" & Settings.RobustServer & ":" & Settings.HttpPort & "/?_Opensim=" & RandomNumber.Random())
            Catch ex As Exception

                If ex.Message.Contains("404") Then Return True
                'BreakPoint.Show(ex.Message)
                Return False
            End Try

            If Up.Length = 0 And PropOpensimIsRunning() Then
                Return False
            End If
        End Using
        Return True

    End Function

    <CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId:="Outworldz.Form1.Log(System.String,System.String)")>
    Public Function KillAll() As Boolean

        If ScanAgents() > 0 Then
            Dim response = MsgBox(My.Resources.Avatars_in_World, vbYesNo)
            If response = vbNo Then Return False
        End If

        AvatarLabel.Text = ""
        PropAborting = True
        ToolBar(False)
        ' close everything as gracefully as possible.

        StopIcecast()

        StopApache(False) ' do not stop if a service

        Dim n As Integer = PropRegionClass.RegionCount()

        Dim TotalRunningRegions As Integer

        For Each RegionUUID As String In PropRegionClass.RegionUUIDs
            If PropRegionClass.IsBooted(RegionUUID) Then
                TotalRunningRegions += 1
            End If

        Next
        Log(My.Resources.Info_word, "Total Enabled Regions=" & CStr(TotalRunningRegions))

        For Each RegionUUID As String In PropRegionClass.RegionUUIDs
            If PropOpensimIsRunning() And PropRegionClass.RegionEnabled(RegionUUID) And
            Not (PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.RecyclingDown _
            Or PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.ShuttingDown _
            Or PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.Stopped) Then
                Print(PropRegionClass.GroupName(RegionUUID) & " " & Global.Outworldz.My.Resources.Stopping_word)
                SequentialPause()

                Dim GroupName = PropRegionClass.GroupName(RegionUUID)
                For Each UUID In PropRegionClass.RegionUUIDListByName(GroupName)
                    PropRegionClass.Status(UUID) = RegionMaker.SIMSTATUSENUM.ShuttingDown
                    PropRegionClass.Timer(UUID) = RegionMaker.REGIONTIMER.Stopped
                Next
                ConsoleCommand(RegionUUID, "q{ENTER}" & vbCrLf)

            End If
            PropUpdateView = True ' make form refresh

        Next

        Dim counter = 600 ' 10 minutes to quit all regions
        Dim last As Integer = PropRegionClass.RegionUUIDs.Count

        ' only wait if the port 8001 is working
        If PropUseIcons Then
            If PropOpensimIsRunning Then Print(My.Resources.Waiting_text)

            While (counter > 0 And PropOpensimIsRunning())
                Sleep(1000)

                counter -= 1
                Dim CountisRunning As Integer = 0

                For Each RegionUUID As String In PropRegionClass.RegionUUIDs
                    If (Not PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.Stopped) _
                        And PropRegionClass.RegionEnabled(RegionUUID) Then

                        Dim GroupName = PropRegionClass.GroupName(RegionUUID)
                        If GroupName.Length > 0 Then
                            For Each p In Process.GetProcesses
                                Application.DoEvents()
                                If p.MainWindowTitle = GroupName Then
                                    CountisRunning += 1
                                    Exit For
                                End If
                            Next
                        End If

                    End If
                    Application.DoEvents()
                    If CountisRunning = 0 Then Exit For
                Next

                If CountisRunning = 1 And last > 1 Then
                    Print(My.Resources.One_region)
                    last = 1
                    PropUpdateView = True ' make form refresh
                Else
                    If CountisRunning <> last Then
                        Print(CStr(CountisRunning) & " " & Global.Outworldz.My.Resources.Regions_Are_Running)
                        last = CountisRunning
                        PropUpdateView = True ' make form refresh
                    End If
                End If

                If CountisRunning = 0 Then
                    counter = 0
                End If

            End While
            PropUpdateView = True ' make form refresh
        End If

        ClearAllRegions()

        StopRobust()
        PropStopMysql = True
        If Not Settings.ApacheService Then StopMysql()

        Timer1.Stop()
        PropOpensimIsRunning() = False

        ToolBar(False)
        Return True

    End Function

    Public Function LoadIARContent(thing As String) As Boolean

        If Not PropOpensimIsRunning() Then
            Print(My.Resources.Not_Running)
            Return False
        End If

        Dim UUID As String = ""

        ' find one that is running
        For Each RegionUUID As String In PropRegionClass.RegionUUIDs
            If PropRegionClass.IsBooted(RegionUUID) Then
                UUID = RegionUUID
                Exit For
            End If
            Application.DoEvents()
        Next
        If UUID.Length = 0 Then
            MsgBox(My.Resources.No_Regions_Ready, vbInformation, Global.Outworldz.My.Resources.Info_word)
            Return False
        End If

        Dim Path As String = InputBox(My.Resources.Folder_To_Save_To_word & " (""/"",  ""/Objects/Somefolder..."")", "Folder Name", "/Objects")

        Dim user As String = InputBox(My.Resources.Enter_1_2)

        Dim password = InputBox(My.Resources.Password_word)
        If user.Length > 0 And password.Length > 0 Then
            ConsoleCommand(UUID, "load iar --merge " & user & " " & Path & " " & password & " " & """" & thing & """" & "{ENTER}" & vbCrLf)
            ConsoleCommand(UUID, "alert IAR content Is loaded{ENTER}" & vbCrLf)
            Print(My.Resources.isLoading & vbCrLf & Path)
        Else
            Print(My.Resources.Canceled_IAR)
        End If
        Me.Focus()
        Return True

    End Function

    <CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId:="Outworldz.Form1.ErrorLog(System.String)")>
    Public Function LoadOARContent(thing As String) As Boolean

        If Not PropOpensimIsRunning() Then
            Print(My.Resources.Not_Running)
            Return False
        End If

        Dim region = ChooseRegion(True)
        If region.Length = 0 Then Return False

        Dim offset = VarChooser(region)
        If offset.Length = 0 Then Return False

        Dim backMeUp = MsgBox(My.Resources.Make_a_backup_word, vbYesNoCancel, Global.Outworldz.My.Resources.Backup_word)
        If backMeUp = vbCancel Then Return False

        Dim testRegionUUID As String = PropRegionClass.FindRegionByName(region)
        If testRegionUUID.Length = 0 Then
            MsgBox(My.Resources.Cannot_find_region_word)
            Return False
        End If
        Dim GroupName = PropRegionClass.GroupName(testRegionUUID)
        Dim once As Boolean = False
        For Each RegionUUID As String In PropRegionClass.RegionUUIDListByName(GroupName)
            Try
                If Not once Then
                    Print(My.Resources.Opensimulator_is_loading & " " & thing)
                    If thing IsNot Nothing Then thing = thing.Replace("\", "/")    ' because Opensim uses UNIX-like slashes, that's why

                    ConsoleCommand(RegionUUID, "change region " & region & "{ENTER}" & vbCrLf)
                    If backMeUp = vbYes Then
                        ConsoleCommand(RegionUUID, "alert " & Global.Outworldz.My.Resources.CPU_Intensive & "{Enter}" & vbCrLf)
                        ConsoleCommand(RegionUUID, "save oar " & BackupPath() & region & "_" & DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss", Globalization.CultureInfo.InvariantCulture) & ".oar" & """" & "{ENTER}" & vbCrLf)
                    End If
                    ConsoleCommand(RegionUUID, "alert " & Global.Outworldz.My.Resources.New_Content & "{ENTER}" & vbCrLf)

                    Dim ForceParcel As String = ""
                    If PropForceParcel() Then ForceParcel = " --force-parcels "
                    Dim ForceTerrain As String = ""
                    If PropForceTerrain Then ForceTerrain = " --force-terrain "
                    Dim ForceMerge As String = ""
                    If PropForceMerge Then ForceMerge = " --merge "
                    Dim UserName As String = ""
                    If PropUserName.Length > 0 Then UserName = " --default-user " & """" & PropUserName & """" & " "

                    ConsoleCommand(RegionUUID, "load oar " & UserName & ForceMerge & ForceTerrain & ForceParcel & offset & """" & thing & """" & "{ENTER}" & vbCrLf)
                    ConsoleCommand(RegionUUID, "alert " & Global.Outworldz.My.Resources.New_is_Done & "{ENTER}" & vbCrLf)
                    ConsoleCommand(RegionUUID, "generate map {ENTER}" & vbCrLf)
                    once = True
                End If
            Catch ex As Exception

                BreakPoint.Show(ex.Message)
                ErrorLog(My.Resources.Error_word & ":" & ex.Message)
            End Try
            Application.DoEvents()
        Next

        Me.Focus()
        Return True

    End Function

    <CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId:="Outworldz.Form1.Log(System.String,System.String)")>
    Public Function OpenRouterPorts() As Boolean

        If Not PropMyUPnpMap.UPnpEnabled And Settings.UPnPEnabled Then
            Settings.UPnPEnabled = False
            Settings.SaveSettings()
            Return False
        End If

        If Not Settings.UPnPEnabled Then
            Return False
        End If

        Print(My.Resources.Open_Router_Ports)

        Log("UPnP", "Local IP seems to be " & PropMyUPnpMap.LocalIP)

        Try

            If Settings.SCEnable Then
                'Icecast 8100-8101

                If PropMyUPnpMap.Exists(Convert.ToInt16(Settings.SCPortBase), UPnp.MyProtocol.TCP) Then
                    PropMyUPnpMap.Remove(Convert.ToInt16(Settings.SCPortBase), UPnp.MyProtocol.TCP)
                End If
                Application.DoEvents()
                If PropMyUPnpMap.Add(PropMyUPnpMap.LocalIP, CType(Settings.SCPortBase, Integer), UPnp.MyProtocol.TCP, "Icecast TCP Public " & Settings.SCPortBase.ToString(Globalization.CultureInfo.InvariantCulture)) Then
                    Print(My.Resources.Icecast_is_Set & ":TCP:" & Settings.SCPortBase.ToString(Globalization.CultureInfo.InvariantCulture))
                End If

                '0 UDP
                If PropMyUPnpMap.Exists(Convert.ToInt16(Settings.SCPortBase), UPnp.MyProtocol.UDP) Then
                    PropMyUPnpMap.Remove(Convert.ToInt16(Settings.SCPortBase), UPnp.MyProtocol.UDP)
                End If
                Application.DoEvents()
                If PropMyUPnpMap.Add(PropMyUPnpMap.LocalIP, CType(Settings.SCPortBase, Integer), UPnp.MyProtocol.UDP, "Icecast UDP Public " & Settings.SCPortBase.ToString(Globalization.CultureInfo.InvariantCulture)) Then
                    Print(My.Resources.Icecast_is_Set & ":UDP:" & Settings.SCPortBase.ToString(Globalization.CultureInfo.InvariantCulture))
                End If

                '1 TCP
                If PropMyUPnpMap.Exists(Convert.ToInt16(Settings.SCPortBase1), UPnp.MyProtocol.TCP) Then
                    PropMyUPnpMap.Remove(Convert.ToInt16(Settings.SCPortBase1), UPnp.MyProtocol.TCP)
                End If
                Application.DoEvents()
                If PropMyUPnpMap.Add(PropMyUPnpMap.LocalIP, CType(Settings.SCPortBase1, Integer), UPnp.MyProtocol.TCP, "Icecast1 TCP Public " & Settings.SCPortBase1.ToString(Globalization.CultureInfo.InvariantCulture)) Then
                    Print(My.Resources.Icecast_is_Set & ":TCP:" & Settings.SCPortBase1.ToString(Globalization.CultureInfo.InvariantCulture))
                End If
                '0 UDP
                If PropMyUPnpMap.Exists(Convert.ToInt16(Settings.SCPortBase1), UPnp.MyProtocol.UDP) Then
                    PropMyUPnpMap.Remove(Convert.ToInt16(Settings.SCPortBase1), UPnp.MyProtocol.UDP)
                End If
                Application.DoEvents()
                If PropMyUPnpMap.Add(PropMyUPnpMap.LocalIP, CType(Settings.SCPortBase1, Integer), UPnp.MyProtocol.UDP, "Icecast1 UDP Public " & Settings.SCPortBase1.ToString(Globalization.CultureInfo.InvariantCulture)) Then
                    Print(My.Resources.Icecast_is_Set & ":UDP:" & Settings.SCPortBase1.ToString(Globalization.CultureInfo.InvariantCulture))
                End If

            End If ' IceCast
            Application.DoEvents()

            If Settings.ApacheEnable Then
                If PropMyUPnpMap.Exists(Settings.ApachePort, UPnp.MyProtocol.TCP) Then
                    PropMyUPnpMap.Remove(Settings.ApachePort, UPnp.MyProtocol.TCP)
                End If
                If PropMyUPnpMap.Add(PropMyUPnpMap.LocalIP, Settings.ApachePort, UPnp.MyProtocol.TCP, "Apache TCP Public " & Settings.SCPortBase.ToString(Globalization.CultureInfo.InvariantCulture)) Then
                    Print(My.Resources.Apache_is_Set & ":TCP:" & Settings.ApachePort.ToString(Globalization.CultureInfo.InvariantCulture))
                End If
            End If
            Application.DoEvents()

            ' 8002 for TCP
            If PropMyUPnpMap.Exists(Convert.ToInt16(Settings.HttpPort, Globalization.CultureInfo.InvariantCulture), UPnp.MyProtocol.TCP) Then
                PropMyUPnpMap.Remove(Convert.ToInt16(Settings.HttpPort, Globalization.CultureInfo.InvariantCulture), UPnp.MyProtocol.TCP)
            End If
            If PropMyUPnpMap.Add(PropMyUPnpMap.LocalIP, Convert.ToInt16(Settings.HttpPort, Globalization.CultureInfo.InvariantCulture), UPnp.MyProtocol.TCP, "Opensim TCP Grid " & Settings.HttpPort) Then
                Print(My.Resources.Grid_TCP_is_set_word & ":" & Settings.HttpPort.ToString(Globalization.CultureInfo.InvariantCulture))
            End If
            Application.DoEvents()

            ' 8002 for UDP
            If PropMyUPnpMap.Exists(Convert.ToInt16(Settings.HttpPort, Globalization.CultureInfo.InvariantCulture), UPnp.MyProtocol.UDP) Then
                PropMyUPnpMap.Remove(Convert.ToInt16(Settings.HttpPort, Globalization.CultureInfo.InvariantCulture), UPnp.MyProtocol.UDP)
            End If
            If PropMyUPnpMap.Add(PropMyUPnpMap.LocalIP, Convert.ToInt16(Settings.HttpPort, Globalization.CultureInfo.InvariantCulture), UPnp.MyProtocol.UDP, "Opensim UDP Grid " & Settings.HttpPort) Then
                Print(My.Resources.Grid_UDP_is_set_word & ":" & Settings.HttpPort.ToString(Globalization.CultureInfo.InvariantCulture))
            End If

            Application.DoEvents()

            For Each RegionUUID As String In PropRegionClass.RegionUUIDs
                Dim R As Integer = PropRegionClass.RegionPort(RegionUUID)

                If PropMyUPnpMap.Exists(R, UPnp.MyProtocol.UDP) Then
                    PropMyUPnpMap.Remove(R, UPnp.MyProtocol.UDP)
                End If
                Application.DoEvents()
                If PropMyUPnpMap.Add(PropMyUPnpMap.LocalIP, R, UPnp.MyProtocol.UDP, "Opensim UDP Region " & PropRegionClass.RegionName(RegionUUID) & " ") Then
                    Print(PropRegionClass.RegionName(RegionUUID) & ":UDP:" & R.ToString(Globalization.CultureInfo.InvariantCulture))
                End If
                Application.DoEvents()

                If PropMyUPnpMap.Exists(R, UPnp.MyProtocol.TCP) Then
                    PropMyUPnpMap.Remove(R, UPnp.MyProtocol.TCP)
                End If
                Application.DoEvents()
                If PropMyUPnpMap.Add(PropMyUPnpMap.LocalIP, R, UPnp.MyProtocol.TCP, "Opensim TCP Region " & PropRegionClass.RegionName(RegionUUID) & " ") Then
                    Print(PropRegionClass.RegionName(RegionUUID) & ":TCP:" & R.ToString(Globalization.CultureInfo.InvariantCulture))
                End If

            Next
        Catch ex As Exception

            BreakPoint.Show(ex.Message)
            Log("UPnP", "UPnP Exception caught:  " & ex.Message)
            Return False
        End Try
        Return True 'successfully added

    End Function

    Public Sub Print(Value As String)

        Log(My.Resources.Info_word, Value)
        TextBox1.Text = TextBox1.Text & vbCrLf & Value
        Trim()

    End Sub

    <CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId:="Outworldz.Form1.ErrorLog(System.String)")>
    Public Function RegisterName(DNSName As String, force As Boolean) As Boolean

        If DNSName Is Nothing Then Return False

        If DNSName.Length = 0 Then
            Return False
        End If

        Dim Checkname As String = String.Empty
        If Settings.ServerType <> "Robust" Then
            Return True
        End If

        If IPCheck.IsPrivateIP(DNSName) Then
            Return False
        End If

        If _DNS_is_registered And Not force Then Return True

        Dim client As New WebClient ' download client for web pages
        Try
            Checkname = client.DownloadString("http://ns1.outworldz.net/dns.plx?GridName=" & DNSName & GetPostData())
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
            Try
                Checkname = client.DownloadString("http://ns2.outworldz.net/dns.plx?GridName=" & DNSName & GetPostData())
            Catch
                ErrorLog("Warn: Cannot register this DNS Name " & ex.Message)
                Return False
            End Try
        Finally
            client.Dispose()
        End Try

        If Checkname = "UPDATE" Then
            _DNS_is_registered = True
            Return True
        End If
        If Checkname = "NAK" Then
            MsgBox(My.Resources.DDNS_In_Use)
        End If
        Return False

    End Function

    Public Sub SendMsg(msg As String)
        Dim hwnd As IntPtr
        If PropOpensimIsRunning() Then
            For Each RegionUUID As String In PropRegionClass.RegionUUIDs
                If PropRegionClass.IsBooted(RegionUUID) Then
                    ConsoleCommand(RegionUUID, "set log level " & msg & "{ENTER}" & vbCrLf)
                    hwnd = GetHwnd(PropRegionClass.GroupName(RegionUUID))
                    ShowDOSWindow(hwnd, Form1.SHOWWINDOWENUM.SWMINIMIZE)
                End If
            Next
            ConsoleCommand(RobustName, "set log level " & msg & "{ENTER}" & vbCrLf)
            ShowDOSWindow(GetHwnd(RobustName), SHOWWINDOWENUM.SWMINIMIZE)
        End If

        Settings.LogLevel = msg
        Settings.SaveSettings()

    End Sub

    Public Sub SequentialPause()

        If Settings.Sequential Then

            For Each RegionUUID As String In PropRegionClass.RegionUUIDs
                If PropOpensimIsRunning() And PropRegionClass.RegionEnabled(RegionUUID) And
                    Not (PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.RecyclingDown _
                    Or PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.ShuttingDown _
                    Or PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.Stopped) Then

                    Dim ctr = 5 * 60  ' 5 minute max to start a region

                    While True

                        If PropRegionClass.RegionEnabled(RegionUUID) _
                            And Not PropAborting _
                            And (PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.RecyclingUp Or
                                PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.ShuttingDown Or
                                PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.RecyclingDown Or
                                PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.Booting) Then
                        Else
                            Exit While
                        End If
                        ctr -= 1
                        If ctr <= 0 Then Exit While
                        Sleep(1000)
                    End While
                End If
            Next
        Else
            Dim ctr = 600 ' 1 minute max to start a region

            While True
                If CPUAverageSpeed < Settings.CPUMAX Then
                    Exit While
                End If
                Sleep(100)
                ctr -= 1
                If ctr <= 0 Then Exit While
            End While

        End If

    End Sub

    <CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId:="Outworldz.Form1.ErrorLog(System.String)")>
    <CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId:="Outworldz.Form1.Log(System.String,System.String)")>
    Public Function SetPublicIP() As Boolean

        ' LAN USE

        If Settings.DNSName.Length > 0 Then
            Settings.PublicIP = Settings.DNSName()
            Settings.SaveSettings()
            Print(My.Resources.Setup_Network)
            Dim ret = RegisterName(Settings.PublicIP, False)
            Dim array As String() = Settings.AltDnsName.Split(",".ToCharArray())
            For Each part As String In array
                RegisterName(part, False)
            Next
            Return ret
        Else
            ' Settings.PublicIP = PropMyUPnpMap.LocalIP
            ' Print(My.Resources.Setup_Network)
            ' Settings.SaveSettings()
            ' Return True
        End If

        ' HG USE

        If Not IPCheck.IsPrivateIP(Settings.DNSName) Then
            Print(My.Resources.Public_IP_Setup_Word)
            Settings.PublicIP = Settings.DNSName
            Settings.SaveSettings()

            Dim UC = Settings.PublicIP.ToUpperInvariant()
            If UC.Contains("OUTWORLDZ.NET") Then
                Print(My.Resources.DynDNS & " http://" & Settings.PublicIP & ":" & Settings.HttpPort)
            End If

            RegisterName(Settings.PublicIP, False)
            Dim array As String() = Settings.AltDnsName.Split(",".ToCharArray())
            For Each part As String In array
                RegisterName(part, False)
            Next

        End If

        If Settings.PublicIP = "localhost" Or Settings.PublicIP = "127.0.0.1" Then
            RegisterName(Settings.PublicIP, False)
            Return True
        End If

        Log(My.Resources.Info_word, "Public IP=" & Settings.PublicIP)
        TestPublicLoopback()
        If Settings.DiagFailed Then

            Using client As New WebClient ' download client for web pages
                Try
                    ' Set Public IP
                    Settings.PublicIP = client.DownloadString("http://api.ipify.org/?r=" & RandomNumber.Random())
                Catch ex As Exception

                    BreakPoint.Show(ex.Message)
                    ErrorLog(My.Resources.Wrong & "@ api.ipify.org")
                    Settings.DiagFailed = True
                End Try
            End Using

            Settings.SaveSettings()
            Return True
        End If

        Settings.PublicIP = PropMyUPnpMap.LocalIP
        Settings.SaveSettings()

        Return False

    End Function

    Public Function SetRegionINI(regionname As String, key As String, value As String) As Boolean

        Dim RegionUUID As String = PropRegionClass.FindRegionByName(regionname)
        If Settings.LoadIni(PropRegionClass.RegionPath(RegionUUID), ";") Then
            Return True
        End If
        Settings.SetIni(regionname, key, value)
        Settings.SaveINI(System.Text.Encoding.UTF8)
        Return False

    End Function

    Public Sub ShowRegionMap()

        Dim region = ChooseRegion(False)
        If region.Length = 0 Then Return

        VarChooser(region, False, False)

    End Sub

    Private Sub SetPath()

        Dim value = Environment.GetEnvironmentVariable("Path")
        Dim PHP = Settings.CurrentDirectory & "\OutworldzFiles\PHP7"
        If value.ToUpper(Globalization.CultureInfo.InvariantCulture).Contains(PHP.ToUpper(Globalization.CultureInfo.InvariantCulture)) Then
            Return
        Else
            value += ";" & PHP
            'setx Path "MyEnvironment" /M
            ApacheProcess.StartInfo.UseShellExecute = True ' so we can redirect streams
            ApacheProcess.StartInfo.FileName = "setx"
            ApacheProcess.StartInfo.CreateNoWindow = True
            ApacheProcess.StartInfo.Arguments = "Path " & """" & value & """" & " /M"
            ApacheProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
            Try
                ApacheProcess.Start()
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
            End Try
            Application.DoEvents()
            ApacheProcess.WaitForExit()

        End If

    End Sub

    Public Function StartApache() As Boolean

        ' Depends upon PHP for home page
        DoPHPDBSetup()

        SetPath()

        Dim SiteMapContents = "<?xml version=""1.0"" encoding=""UTF-8""?>" & vbCrLf
        SiteMapContents += "<urlset xmlns=""http://www.sitemaps.org/schemas/sitemap/0.0909"">" & vbCrLf
        SiteMapContents += "<url>" & vbCrLf
        SiteMapContents += "<loc>http://" & Settings.PublicIP & ":" & Convert.ToString(Settings.ApachePort, Globalization.CultureInfo.InvariantCulture) & "/" & "</loc>" & vbCrLf
        SiteMapContents += "<changefreq>daily</changefreq>" & vbCrLf
        SiteMapContents += "<priority>1.0</priority>" & vbCrLf
        SiteMapContents += "</url>" & vbCrLf
        SiteMapContents += "</urlset>" & vbCrLf

        Using outputFile As New StreamWriter(Settings.CurrentDirectory & "\OutworldzFiles\Apache\htdocs\Sitemap.xml", False)
            outputFile.WriteLine(SiteMapContents)
        End Using

        If Not Settings.ApacheEnable Then
            ApacheIs(False)
            Print(My.Resources.Apache_Disabled)
            Return True
        End If

        If Settings.ApachePort = 80 Then
            ApacheProcess.StartInfo.UseShellExecute = True ' so we can redirect streams
            ApacheProcess.StartInfo.FileName = "net"
            ApacheProcess.StartInfo.CreateNoWindow = True
            ApacheProcess.StartInfo.Arguments = "stop W3SVC"
            ApacheProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden

            Try
                ApacheProcess.Start()
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
            End Try
            Application.DoEvents()
            ApacheProcess.WaitForExit()
        End If

        Print(My.Resources.Checking_Apache_service_word)
        ' Stop MSFT server if we are on port 80 and enabled

        If Settings.ApacheService Then
            PropApacheUninstalling = True
            ApacheProcess.StartInfo.FileName = "sc"
            ApacheProcess.StartInfo.Arguments = "stop " & "ApacheHTTPServer"
            Try
                ApacheProcess.Start()
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
            End Try
            Application.DoEvents()
            ApacheProcess.WaitForExit()

            ApacheProcess.StartInfo.Arguments = "stop " & """" & "Apache HTTP Server" & """"
            Try
                ApacheProcess.Start()
            Catch ex As Exception

                BreakPoint.Show(ex.Message)
            End Try
            Application.DoEvents()
            ApacheProcess.WaitForExit()
            ApacheIs(False)

            'delete really old service
            ApacheProcess.StartInfo.FileName = "sc"
            ApacheProcess.StartInfo.Arguments = " delete  " & """" & "Apache HTTP Server" & """"
            Try
                ApacheProcess.Start()
            Catch ex As Exception
            End Try
            Application.DoEvents()
            ApacheProcess.WaitForExit()

            ApacheProcess.StartInfo.Arguments = " delete ApacheHTTPServer"
            Try
                ApacheProcess.Start()
            Catch ex As Exception

                BreakPoint.Show(ex.Message)
            End Try
            Application.DoEvents()
            ApacheProcess.WaitForExit()

            Sleep(3000)

            Using ApacheProcess As New Process With {
                    .EnableRaisingEvents = False
                }
                ApacheProcess.StartInfo.UseShellExecute = True ' so we can redirect streams
                ApacheProcess.StartInfo.FileName = Settings.CurrentDirectory & "\Outworldzfiles\Apache\bin\httpd.exe"
                ApacheProcess.StartInfo.Arguments = "-k install -n " & """" & "ApacheHTTPServer" & """"
                ApacheProcess.StartInfo.CreateNoWindow = True
                ApacheProcess.StartInfo.WorkingDirectory = Settings.CurrentDirectory & "\Outworldzfiles\Apache\bin\"
                ApacheProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden

                DoApache()

                Try
                    ApacheProcess.Start()
                Catch ex As Exception
                    BreakPoint.Show(ex.Message)
                    ApacheIs(False)
                    Print(My.Resources.ApacheFailed & ":" & ex.Message)
                End Try
                Application.DoEvents()
                ApacheProcess.WaitForExit()

                If ApacheProcess.ExitCode <> 0 Then
                    Print(My.Resources.ApacheFailed)
                    ApacheIs(False)
                Else
                    PropApacheUninstalling = False ' installed now, trap errors
                End If
                Sleep(100)
                Print(My.Resources.Apache_starting)
                ApacheProcess.StartInfo.FileName = "net"
                ApacheProcess.StartInfo.Arguments = "start ApacheHTTPServer"
                ApacheProcess.StartInfo.UseShellExecute = False

                Try
                    ApacheProcess.Start()
                Catch ex As Exception

                    BreakPoint.Show(ex.Message)
                    Print(My.Resources.Apache_Failed & ":" & ex.Message)
                End Try
                Application.DoEvents()
                ApacheProcess.WaitForExit()

                If ApacheProcess.ExitCode <> 0 Then
                    Print(My.Resources.Apache_Failed & ":" & CStr(ApacheProcess.ExitCode))
                    ApacheIs(False)
                Else
                    Print(My.Resources.Apache_running & ":" & Settings.ApachePort)
                    ApacheIs(True)
                End If
            End Using
        Else
            ' Start Apache manually
            Using ApacheProcess As New Process With {
                    .EnableRaisingEvents = True
                }
                ApacheProcess.StartInfo.UseShellExecute = True ' so we can redirect streams
                ApacheProcess.StartInfo.FileName = Settings.CurrentDirectory & "\Outworldzfiles\Apache\bin\httpd.exe"
                ApacheProcess.StartInfo.CreateNoWindow = True
                ApacheProcess.StartInfo.WorkingDirectory = Settings.CurrentDirectory & "\Outworldzfiles\Apache\bin\"
                ApacheProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
                ApacheProcess.StartInfo.Arguments = ""
                Try
                    ApacheProcess.Start()
                Catch ex As Exception

                    BreakPoint.Show(ex.Message)
                    Print(My.Resources.Apache_Failed & ":" & ex.Message)
                    ApacheIs(False)
                    Return False
                End Try

                Application.DoEvents()

                ' wait for PID
                Dim ApachePID = WaitForPID(ApacheProcess)
                If ApachePID = 0 Then
                    ApacheIs(False)
                    Return False
                End If

                ' Wait for Apache to start listening
                PropgApacheProcessID = ApachePID
                Dim counter = 0

                While PropOpensimIsRunning And Not PropAborting
                    counter += 1
                    ' wait 60 seconds for it to start
                    If counter > 600 Then
                        Print(My.Resources.Apache_Failed)
                        ApacheIs(False)
                        Return False
                    End If

                    Dim isRunning = CheckPort(Settings.PrivateURL, CType(Settings.ApachePort, Integer))
                    If isRunning Then
                        Print(My.Resources.Apache_running)
                        ApacheIs(True)
                        PropApacheExited = False
                        Return True
                    End If
                    Sleep(100)
                End While
            End Using
        End If

        Return False

    End Function

    Public Function StartIcecast() As Boolean

        If Not Settings.SCEnable Then
            Print(Global.Outworldz.My.Resources.IceCast_disabled)
            IceCastIs(False)
            Return True
        End If

        ' Check if DOS box exists, first, if so, its running.
        For Each p In Process.GetProcesses
            If p.MainWindowTitle = "Icecast" Then
                PropIcecastProcID = p.Id
                IceCastIs(True)
                Return True
            End If
        Next

        DoIceCast()

        FileStuff.DeleteFile(Settings.CurrentDirectory & "\Outworldzfiles\Icecast\log\access.log")
        FileStuff.DeleteFile(Settings.CurrentDirectory & "\Outworldzfiles\Icecast\log\error.log")

        PropIcecastProcID = 0
        Print(My.Resources.Icecast_starting)
        IcecastProcess.EnableRaisingEvents = True
        IcecastProcess.StartInfo.UseShellExecute = True ' so we can redirect streams
        IcecastProcess.StartInfo.FileName = Settings.CurrentDirectory & "\Outworldzfiles\icecast\icecast.bat"
        IcecastProcess.StartInfo.CreateNoWindow = False
        IcecastProcess.StartInfo.WorkingDirectory = Settings.CurrentDirectory & "\Outworldzfiles\icecast"

        Select Case Settings.ConsoleShow
            Case "True"
                IcecastProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal
            Case "False"
                IcecastProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal
            Case "None"
                IcecastProcess.StartInfo.WindowStyle = ProcessWindowStyle.Minimized
        End Select

        Try
            IcecastProcess.Start()
        Catch ex As Exception

            BreakPoint.Show(ex.Message)
            Print(My.Resources.Icecast_failed & ":" & ex.Message)
            IceCastIs(False)

            Return False
        End Try

        PropIcecastProcID = WaitForPID(IcecastProcess)
        If PropIcecastProcID = 0 Then
            IceCastIs(False)
            Return False
        End If

        SetWindowTextCall(IcecastProcess, "Icecast")
        ShowDOSWindow(IcecastProcess.MainWindowHandle, SHOWWINDOWENUM.SWMINIMIZE)
        IceCastIs(True)

        PropIceCastExited = False
        Return True

    End Function

    Public Function StartMySQL() As Boolean

        If MysqlInterface.IsMySqlRunning() Then
            MysqlInterface.IsRunning = True
            MySqlIs(True)
            PropMysqlExited = False
            Return True
        End If

        ' Build data folder if it does not exist
        MakeMysql()

        MySqlIs(False)
        ' Start MySql in background.

        Print(My.Resources.Mysql_Starting)

        ' SAVE INI file
        If Settings.LoadIni(Settings.CurrentDirectory & "\OutworldzFiles\mysql\my.ini", "#") Then Return True
        Settings.SetIni("mysqld", "basedir", """" & PropCurSlashDir & "/OutworldzFiles/Mysql" & """")
        Settings.SetIni("mysqld", "datadir", """" & PropCurSlashDir & "/OutworldzFiles/Mysql/Data" & """")
        Settings.SetIni("mysqld", "port", CStr(Settings.MySqlRobustDBPort))
        Settings.SetIni("client", "port", CStr(Settings.MySqlRobustDBPort))
        Settings.SaveINI(System.Text.Encoding.ASCII)

        ' create test program slants the other way:
        Dim testProgram As String = Settings.CurrentDirectory & "\OutworldzFiles\Mysql\bin\StartManually.bat"
        FileStuff.DeleteFile(testProgram)

        Try
            Using outputFile As New StreamWriter(testProgram, True)
                outputFile.WriteLine("@REM A program to run Mysql manually for troubleshooting." & vbCrLf _
                                 & "mysqld.exe --defaults-file=" & """" & PropCurSlashDir & "/OutworldzFiles/mysql/my.ini" & """")
            End Using
        Catch ex As Exception

            BreakPoint.Show(ex.Message)
        End Try

        CreateService()
        CreateStopMySql()

        Application.DoEvents()
        ' Mysql was not running, so lets start it up.
        Dim pi As ProcessStartInfo = New ProcessStartInfo With {
            .Arguments = "--defaults-file=" & """" & PropCurSlashDir & "/OutworldzFiles/mysql/my.ini" & """",
            .WindowStyle = ProcessWindowStyle.Hidden,
            .FileName = """" & Settings.CurrentDirectory & "\OutworldzFiles\mysql\bin\mysqld.exe" & """"
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

            Dim MysqlLog As String = Settings.CurrentDirectory & "\OutworldzFiles\mysql\data"
            If ctr = 60 Then ' about 60 seconds when it fails

                Dim yesno = MsgBox(My.Resources.Mysql_Failed, vbYesNo, Global.Outworldz.My.Resources.Error_word)
                If (yesno = vbYes) Then
                    Dim files As Array = Nothing
                    Try
                        files = Directory.GetFiles(MysqlLog, "*.err", SearchOption.TopDirectoryOnly)
                    Catch ex As Exception

                        BreakPoint.Show(ex.Message)
                    End Try

                    For Each FileName As String In files
                        Try
                            System.Diagnostics.Process.Start(Settings.CurrentDirectory & "\baretail.exe", """" & FileName & """")
                        Catch ex As Exception

                            BreakPoint.Show(ex.Message)
                        End Try
                        Application.DoEvents()
                    Next
                End If
                Buttons(StartButton)
                Return False
            End If
            ctr += 1
            ' check again
            Sleep(1000)
            MysqlOk = MysqlInterface.IsMySqlRunning()
        End While

        If Not MysqlOk Then Return False

        PropMysqlExited = False
        MysqlInterface.IsRunning = True
        MySqlIs(True)

        Print(Global.Outworldz.My.Resources.Mysql_is_Running)
        PropMysqlExited = False

        Return True

    End Function

    Public Function StartOpensimulator() As Boolean

        PropExitHandlerIsBusy = False
        PropAborting = False

        If Not StartRobust() Then Return False

        ' Boot them up
        For Each RegionUUID As String In PropRegionClass.RegionUUIDs()
            If PropRegionClass.RegionEnabled(RegionUUID) Then
                Boot(PropRegionClass, PropRegionClass.RegionName(RegionUUID))
                Application.DoEvents()
            End If
        Next

        Return True

    End Function

    Public Function StartRobust() As Boolean

        If Not StartMySQL() Then Return False ' prerequsite
        ' prevent recursion
        If _RobustIsStarting Then
            Return True
        End If

        For Each p In Process.GetProcesses
            If p.MainWindowTitle = RobustName() Then
                PropRobustProcID = p.Id
                Log(My.Resources.Info_word, Global.Outworldz.My.Resources.DosBoxRunning)
                RobustIs(True)

                Select Case Settings.ConsoleShow
                    Case "True"
                    ' Do nothing, Always Show
                    Case "False"
                        ShowDOSWindow(GetHwnd(RobustName), SHOWWINDOWENUM.SWMINIMIZE)
                    Case "None"
                        ShowDOSWindow(GetHwnd(RobustName), SHOWWINDOWENUM.SWMINIMIZE)
                End Select

                Return True
            End If
        Next

        ' Check the HTTP port
        If IsRobustRunning() Then

            Select Case Settings.ConsoleShow
                Case "True"
                    ' Do nothing, Always Show
                Case "False"
                    ShowDOSWindow(GetHwnd(RobustName), SHOWWINDOWENUM.SWMINIMIZE)
                Case "None"
                    ShowDOSWindow(GetHwnd(RobustName), SHOWWINDOWENUM.SWMINIMIZE)
            End Select

            Return True
        End If

        If Settings.ServerType <> "Robust" Then
            RobustIs(True)
            Return True
        End If

        _RobustIsStarting = True
        Environment.SetEnvironmentVariable("OSIM_LOGLEVEL", Settings.LogLevel.ToUpperInvariant)
        PropRobustProcID = 0

        DoSetDefaultSims()

        Print("Robust " & Global.Outworldz.My.Resources.Starting_word)

        RobustProcess.EnableRaisingEvents = True
        RobustProcess.StartInfo.UseShellExecute = True ' so we can redirect streams
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

        RobustProcess.StartInfo.Arguments = "-inifile Robust.HG.ini"
        Try
            RobustProcess.Start()
            Log(My.Resources.Info_word, Global.Outworldz.My.Resources.Robust_running)
        Catch ex As Exception

            BreakPoint.Show(ex.Message)
            Print("Robust " & Global.Outworldz.My.Resources.did_not_start_word & ex.Message)
            KillAll()
            Buttons(StartButton)
            RobustIs(False)
            _RobustIsStarting = False
            Return False
        End Try

        PropRobustProcID = WaitForPID(RobustProcess)
        If PropRobustProcID = 0 Then
            RobustIs(False)
            Log("Error", Global.Outworldz.My.Resources.Robust_failed_to_start)
            _RobustIsStarting = False
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
                Print("Robust " & Global.Outworldz.My.Resources.did_not_start_word)
            End If

            If counter > 600 Then
                Print(My.Resources.Robust_failed_to_start)
                Buttons(StartButton)
                Dim yesno = MsgBox(My.Resources.See_Log, vbYesNo, Global.Outworldz.My.Resources.Error_word)
                If (yesno = vbYes) Then
                    Dim Log As String = """" & Settings.OpensimBinPath & "Robust.log" & """"
                    Try
                        System.Diagnostics.Process.Start(Settings.CurrentDirectory & "\baretail.exe " & Log)
                    Catch ex As Exception

                        BreakPoint.Show(ex.Message)
                    End Try
                End If
                Buttons(StartButton)

                RobustIs(False)
                _RobustIsStarting = False
                Return False
            End If

            Sleep(100)
        End While

        ' wait a bit for robust to stabilize
        Thread.Sleep(2000)
        _RobustIsStarting = False

        Log(My.Resources.Info_word, Global.Outworldz.My.Resources.Robust_running)

        Select Case Settings.ConsoleShow
            Case "True"
                ' Do nothing, Always Show
            Case "False"
                ShowDOSWindow(GetHwnd(RobustName), SHOWWINDOWENUM.SWMINIMIZE)
            Case "None"
                ShowDOSWindow(GetHwnd(RobustName), SHOWWINDOWENUM.SWMINIMIZE)
        End Select

        RobustIs(True)
        Print(Global.Outworldz.My.Resources.Robust_running)
        PropRobustExited = False

        Return True

    End Function

    ''' <summary>
    ''' Startup() Starts opensimulator system Called by Start Button or by AutoStart
    ''' </summary>
    Public Sub Startup()

        Buttons(BusyButton)

        Dim DefaultName As String = ""

        Dim RegionUUID As String = PropRegionClass.FindRegionByName(Settings.WelcomeRegion)
        If RegionUUID.Length = 0 Then
            MsgBox(My.Resources.Default_Welcome, vbInformation)
            Print(My.Resources.Stopped_word)
            Dim FormRegions = New FormRegions
            FormRegions.Activate()
            FormRegions.Select()
            FormRegions.Visible = True
            FormRegions.BringToFront()
            Buttons(StartButton)
            Return
        End If

        Print(My.Resources.Starting_word)
        PropRegionClass.RegionEnabled(RegionUUID) = True

        PropExitHandlerIsBusy = False
        PropAborting = False  ' suppress exit warning messages

        ToolBar(False)

        GridNames.SetServerNames()

        ' clear region error handlers
        PropRegionHandles.Clear()

        If Settings.Language.Length = 0 Then
            Settings.Language = "en-US"
        End If

        My.Application.ChangeUICulture(Settings.Language)
        My.Application.ChangeCulture(Settings.Language)

        If Settings.AutoBackup Then
            ' add 30 minutes to allow time to auto backup and then restart
            Dim BTime As Integer = CInt("0" & Settings.AutobackupInterval)
            If Settings.AutoRestartInterval > 0 And Settings.AutoRestartInterval < BTime Then
                Settings.AutoRestartInterval = BTime + 30
                Print(My.Resources.AutorestartTime & " " & CStr(BTime) & " + 30 min.")
            End If
        End If

        Print("DNS")
        If SetPublicIP() Then
            OpenPorts()
        End If

        Print(My.Resources.Reading_Region_files)

        If Not StartMySQL() Then
            ToolBar(False)
            Buttons(StartButton)
            Print(My.Resources.Stopped_word)
            Return
        End If

        SetupSearch()

        SetupWordPress()

        StartApache()

        StartIcecast()

        UploadPhoto()

        SetBirdsOnOrOff()

        If Not StartRobust() Then
            Buttons(StartButton)
            Print(My.Resources.Stopped_word)
            Return
        End If

        If Not Settings.RunOnce And Settings.ServerType = "Robust" Then

            Using InitialSetup As New FormInitialSetup ' form for use and password
                Dim ret = InitialSetup.ShowDialog()
                If ret = DialogResult.Cancel Then
                    Buttons(StartButton)
                    Print(My.Resources.Stopped_word)
                    Return
                End If

                ' Read the chosen sim name
                ConsoleCommand(RobustName, "create user " & InitialSetup.FirstName & " " & InitialSetup.LastName & " " & InitialSetup.Password & " " & InitialSetup.Email & "{Enter}{Enter}{Enter}{Enter}")
                ShowDOSWindow(GetHwnd(RobustName), SHOWWINDOWENUM.SWMINIMIZE)
                Settings.RunOnce = True
                Settings.SaveSettings()
            End Using
        End If

        ToolBar(True)

        ' Launch the rockets
        Print(My.Resources.Start_Regions_word)
        If Not StartOpensimulator() Then
            Buttons(StartButton)
            Print(My.Resources.Stopped_word)
            Return
        End If

        Buttons(StopButton)
        Print(My.Resources.Grid_address_word & vbCrLf & "http://" & Settings.BaseHostName & ":" & Settings.HttpPort)

        ' done with boot up

    End Sub

    <CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId:="Outworldz.Form1.Logger(System.String,System.String,System.String)")>
    Public Sub StopGroup(Groupname As String)

        For Each RegionUUID As String In PropRegionClass.RegionUUIDListByName(Groupname)
            Logger(My.Resources.Info_word, PropRegionClass.RegionName(RegionUUID) & " is Stopped", "Restart")
            PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.Stopped
            PropRegionClass.Timer(RegionUUID) = RegionMaker.REGIONTIMER.Stopped
        Next
        Logger(My.Resources.Info_word, Groupname & " Group is now stopped", "Restart")
    End Sub

    Public Sub StopRobust()

        Print("Robust " & Global.Outworldz.My.Resources.Stopping_word)
        ConsoleCommand("Robust", "q{ENTER}" & vbCrLf)
        ConsoleCommand(RobustName, "q{ENTER}" & vbCrLf)
        Dim ctr As Integer = 0
        ' wait 60 seconds for robust to quit
        While IsRobustRunning() And ctr < 60
            Sleep(100)
            ctr += 1
        End While

        RobustIs(False)

    End Sub

    Public Sub ToolBar(visible As Boolean)

        AviLabel.Visible = visible
        AvatarLabel.Visible = visible
        PercentCPU.Visible = visible
        PercentRAM.Visible = visible

    End Sub

    <CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId:="Outworldz.Form1.ErrorLog(System.String)")>
    Public Sub UploadCategory()

        If Settings.DNSName.Length = 0 Then Return

        'PHASE 2, upload Description and Categories
        Dim result As String = Nothing
        If Settings.Categories.Length = 0 Then Return

        Using client As New WebClient ' download client for web pages
            Try
                Dim str = PropDomain & "/cgi/UpdateCategory.plx?Category=" & Settings.Categories & "&Description=" & Settings.Description & GetPostData()
                result = client.DownloadString(str)
            Catch ex As Exception

                BreakPoint.Show(ex.Message)
                ErrorLog(My.Resources.Wrong & " " & ex.Message)
            End Try
        End Using

        If result <> "OK" Then
            ErrorLog(My.Resources.Wrong & " " & result)
        End If

    End Sub

    Public Sub UploadPhoto()

        ''' <summary>
        ''' Upload in a separate thread the photo, if any. Cannot be called unless main web server
        ''' is known to be on line.
        ''' </summary>
        If Settings.GDPR() Then
            If System.IO.File.Exists(Settings.CurrentDirectory & "\OutworldzFiles\Photo.png") Then
                UploadCategory()
                Dim Myupload As New UploadImage
                Myupload.PostContentUploadFile()

            End If
        End If

    End Sub

    Public Function VarChooser(RegionName As String, Optional modal As Boolean = True, Optional Map As Boolean = True) As String

        Dim RegionUUID As String = PropRegionClass.FindRegionByName(RegionName)
        Dim size = PropRegionClass.SizeX(RegionUUID)

#Disable Warning CA2000
        Dim VarForm As New FormDisplacement ' form for choosing a region in  a var
#Enable Warning CA2000
        Dim span = Math.Ceiling(size / 256)
        ' Show Dialog as a modal dialog
        VarForm.Init(span, RegionUUID, Map)

        If modal Then
            VarForm.ShowDialog()
            VarForm.Dispose()
        Else
            VarForm.Show()
        End If

        Return PropSelectedBox

    End Function

    Public Sub Viewlog(name As String)
        If name Is Nothing Then Return
        Dim AllLogs As Boolean = False
        Dim path As New List(Of String)

        If name.StartsWith("Region ", StringComparison.InvariantCultureIgnoreCase) Then
            name = Replace(name, "Region ", "", 1, 1)
            name = PropRegionClass.GroupName(PropRegionClass.FindRegionByName(name))
            path.Add("""" & Settings.OpensimBinPath & "Regions\" & name & "\Opensim.log" & """")
        Else
            If name = "All Logs" Then AllLogs = True
            If name = "Robust" Or AllLogs Then path.Add("""" & Settings.OpensimBinPath & "Robust.log" & """")
            If name = "Outworldz" Or AllLogs Then path.Add("""" & Settings.CurrentDirectory & "\Outworldzfiles\Outworldz.log" & """")
            If name = "Error" Or AllLogs Then path.Add("""" & Settings.CurrentDirectory & "\Outworldzfiles\Error.log" & """")
            If name = "UPnP" Or AllLogs Then path.Add("""" & Settings.CurrentDirectory & "\Outworldzfiles\Upnp.log" & """")
            If name = "Icecast" Or AllLogs Then path.Add(" " & """" & Settings.CurrentDirectory & "\Outworldzfiles\Icecast\log\error.log" & """")
            If name = "All Settings" Or AllLogs Then path.Add("""" & Settings.CurrentDirectory & "\Outworldzfiles\Settings.ini" & """")
            If name = "--- Regions ---" Then Return

            If AllLogs Then
                For Each UUID As String In PropRegionClass.RegionUUIDs
                    name = PropRegionClass.GroupName(UUID)
                    path.Add("""" & Settings.OpensimBinPath & "Regions\" & name & "\Opensim.log" & """")
                    Application.DoEvents()
                Next
            End If

            If name = "MySQL" Or AllLogs Then
                Dim MysqlLog As String = Settings.CurrentDirectory & "\OutworldzFiles\mysql\data"
                Dim files As Array
                Try
                    files = Directory.GetFiles(MysqlLog, "*.err", SearchOption.TopDirectoryOnly)

                    For Each FileName As String In files
                        path.Add("""" & FileName & """")
                    Next
                Catch ex As Exception

                    BreakPoint.Show(ex.Message)
                End Try
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
            System.Diagnostics.Process.Start(Settings.CurrentDirectory & "\baretail.exe", logs)
        Catch ex As Exception

            BreakPoint.Show(ex.Message)
        End Try

    End Sub

    Public Function WaitForPID(myProcess As Process) As Integer

        If myProcess Is Nothing Then Return 0

        Dim PID As Integer
        Dim TooMany As Integer = 0
        Dim p As Process = Nothing

        Do While TooMany < 200

            Try
                p = Process.GetProcessById(myProcess.Id)
            Catch ex As Exception

                BreakPoint.Show(ex.Message)
            End Try

            If p IsNot Nothing Then
                If p.ProcessName.Length > 0 Then
                    PID = myProcess.Id
                    Return PID
                End If
            End If

            Sleep(100)
            TooMany += 1
        Loop
        Try
            Print("Cannot get a Process ID from " & myProcess.ProcessName)
        Catch ex As Exception

            BreakPoint.Show(ex.Message)
        End Try

        Return 0

    End Function

    <CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId:="Outworldz.Form1.Log(System.String,System.String)")>
    Private Shared Sub CleanDLLs()

        Dim dlls As List(Of String) = GetDlls(Settings.CurrentDirectory & "/dlls.txt")
        Dim localdlls As List(Of String) = GetFilesRecursive(Settings.OpensimBinPath)
        For Each localdllname In localdlls
            Application.DoEvents()
            Dim x = localdllname.IndexOf("OutworldzFiles", StringComparison.InvariantCulture)
            Dim newlocaldllname = Mid(localdllname, x)
            If Not CompareDLLignoreCase(newlocaldllname, dlls) Then
                Log(My.Resources.Info_word, "Deleting dll " & localdllname)
                FileStuff.DeleteFile(localdllname)
            End If
        Next

    End Sub

    ''' <summary>Deletes old log files</summary>
    Private Shared Sub ClearLogFiles()

        Dim Logfiles = New List(Of String) From {
            Settings.CurrentDirectory & "\OutworldzFiles\Error.log",
            Settings.CurrentDirectory & "\OutworldzFiles\Diagnostics.log",
            Settings.CurrentDirectory & "\OutworldzFiles\Outworldz.log",
            Settings.CurrentDirectory & "\OutworldzFiles\Restart.log",
            Settings.CurrentDirectory & "\OutworldzFiles\Opensim\bin\OpenSimConsoleHistory.txt",
            Settings.CurrentDirectory & "\OutworldzFiles\Diagnostics.log",
            Settings.CurrentDirectory & "\OutworldzFiles\UPnp.log",
            Settings.CurrentDirectory & "\OutworldzFiles\Opensim\bin\Robust.log",
            Settings.CurrentDirectory & "\OutworldzFiles\http.log",
            Settings.CurrentDirectory & "\OutworldzFiles\PHPLog.log",
            Settings.CurrentDirectory & "\http.log"      ' an old mistake
        }

        For Each thing As String In Logfiles
            ' clear out the log files
            FileStuff.DeleteFile(thing)
            Application.DoEvents()
        Next

    End Sub

    Private Shared Sub Create_ShortCut(ByVal sTargetPath As String)
        ' Requires reference to Windows Script Host Object Model
        Dim WshShell As WshShellClass = New WshShellClass
        Dim MyShortcut As IWshShortcut
        ' The shortcut will be created on the desktop
        Dim DesktopFolder As String = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)
        MyShortcut = CType(WshShell.CreateShortcut(DesktopFolder & "\Outworldz.lnk"), IWshShortcut)
        MyShortcut.TargetPath = sTargetPath
        MyShortcut.IconLocation = WshShell.ExpandEnvironmentStrings(CurDir() & "\Start.exe")
        MyShortcut.WorkingDirectory = CurDir()
        MyShortcut.Save()

    End Sub

    Private Shared Sub CreateStopMySql()

        ' create test program slants the other way:
        Dim testProgram As String = Settings.CurrentDirectory & "\OutworldzFiles\Mysql\bin\StopMySQL.bat"
        FileStuff.DeleteFile(testProgram)
        Try
            Using outputFile As New StreamWriter(testProgram, True)
                outputFile.WriteLine("@REM Program to stop Mysql" & vbCrLf +
            "mysqladmin.exe -u root --port " & CStr(Settings.MySqlRobustDBPort) & " shutdown" & vbCrLf & "@pause" & vbCrLf)
            End Using
        Catch ex As Exception

            BreakPoint.Show(ex.Message)
        End Try

    End Sub

    Private Shared Function DoRegion(RegionName As String, RegionUUID As String) As Boolean

        ''' <summary>
        ''' Copy the Opensim proto Write the Region INI with RegionClass Set the Opensim.ini
        ''' </summary>
        ''' <returns>True if error</returns>

        If RegionMaker.SetRegionVars(RegionName, RegionUUID) Then Return True
        RegionMaker.CopyOpensimProto(RegionName)
        If RegionMaker.SetOpensimIni(RegionName, RegionUUID) Then Return True

        Return False

    End Function

    Private Shared Function DoTos() As Boolean

        Try
            Dim reader As System.IO.StreamReader
            reader = System.IO.File.OpenText(Settings.CurrentDirectory + "\tos.html")
            'now loop through each line
            Dim HTML As String = ""
            While reader.Peek <> -1
                HTML = HTML + reader.ReadLine() + vbCrLf
            End While
            reader.Close()

            Using outputFile As New StreamWriter(Settings.CurrentDirectory + "\Outworldzfiles\Opensim\bin\WifiPages\tos.html")
                outputFile.WriteLine(HTML)
            End Using
        Catch ex As Exception

            BreakPoint.Show(ex.Message)
        End Try

        Return False

    End Function

    Private Shared Function GetStateString(state As Integer) As String

        Dim statestring As String = Nothing
        Select Case state
            Case 0
                statestring = "Stopped"
            Case 1
                statestring = "Booting"
            Case 2
                statestring = "Booted"
            Case 3
                statestring = "RecyclingUp"
            Case 4
                statestring = "RecyclingDown"
            Case 5
                statestring = "ShuttingDown"
            Case 6
                statestring = "RestartPending"
            Case 7
                statestring = "RetartingNow"
            Case 8
                statestring = "Resume"
            Case 9
                statestring = "Suspended"
            Case 10
                statestring = "Error"
            Case 11
                statestring = "RestartStage2"
        End Select
        Return statestring

    End Function

    Private Shared Sub KillFiles(AL As List(Of String))

        For Each filename As String In AL
            FileStuff.DeleteFile(Settings.CurrentDirectory & filename)
        Next

    End Sub

    Private Shared Sub KillFolder(AL As List(Of String))

        For Each folder As String In AL
            If IO.Directory.Exists(Settings.CurrentDirectory & folder) Then
                System.IO.Directory.Delete(Settings.CurrentDirectory & folder, True)
            End If
        Next

    End Sub

    Private Shared Sub SetBirdsOnOrOff()

        If Settings.BirdsModuleStartup Then
            Try
                If Not IO.File.Exists(Settings.OpensimBinPath & "OpenSimBirds.Module.dll") Then
                    My.Computer.FileSystem.CopyFile(Settings.OpensimBinPath & "OpenSimBirds.Module.dll.bak", Settings.OpensimBinPath & "OpenSimBirds.Module.dll")
                End If
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
            End Try
        Else
            Try
                If Not IO.File.Exists(Settings.OpensimBinPath & "OpenSimBirds.Module.bak") Then
                    My.Computer.FileSystem.CopyFile(Settings.OpensimBinPath & "OpenSimBirds.Module.dll", Settings.OpensimBinPath & "OpenSimBirds.Module.bak")
                End If
            Catch
            End Try

            FileStuff.DeleteFile(Settings.OpensimBinPath & "\OpenSimBirds.Module.dll")
        End If

    End Sub

    Private Shared Sub SetQuickEditOff()
        Dim pi As ProcessStartInfo = New ProcessStartInfo With {
            .Arguments = "Set-ItemProperty -path HKCU:\Console -name QuickEdit -value 0",
            .FileName = "powershell.exe",
            .WindowStyle = ProcessWindowStyle.Hidden,
            .Verb = "runas"
        }
        Using PowerShell As Process = New Process With {
             .StartInfo = pi
            }

            Try
                PowerShell.Start()
            Catch ex As Exception

                BreakPoint.Show(ex.Message)
            End Try
        End Using

    End Sub

    Private Shared Sub SetupOpensimSearchINI()

        If Settings.SearchEnabled Then
            If Settings.CMS = "JOpensim" Then
                Dim SearchURL = "http://" & Settings.PublicIP & ":" & Settings.ApachePort & "/JOpensim/index.php?option=com_opensim&view=inworldsearch&task=viewer&templ=component&"
                Settings.SetIni("LoginService", "SearchURL", SearchURL)
            Else
                Settings.SetIni("LoginService", "SearchURL", "http://hyperica.com/Search/query.php")
            End If

            ' RegionSnapShot
            Settings.SetIni("DataSnapshot", "index_sims", "True")
            If Settings.CMS = "JOpensim" Then
                Settings.SetIni("DataSnapshot", "data_services", "http://" & Settings.PublicIP & ":" & Settings.ApachePort & "/JOpensim/components/com_opensim/registersearch.php")
            Else
                Settings.SetIni("DataSnapshot", "data_services", "http://hyperica.com/Search/register.php")
            End If
        Else
            Settings.SetIni("Search", "SearchURL", "")
            Settings.SetIni("Search", "SimulatorFeatures", "")
            Settings.SetIni("SimulatorFeatures", "SearchServerURI", "")
            Settings.SetIni("DataSnapshot", "index_sims", "False")
            Settings.SetIni("DataSnapshot", "data_services", "")
        End If

    End Sub

    Private Shared Sub SetupRobustSearchINI()

        If Settings.SearchEnabled Then
            If Settings.CMS = "JOpensim" Then
                Dim SearchURL = "http://" & Settings.PublicIP & ":" & Settings.ApachePort & "/JOpensim/index.php?option=com_opensim&view=inworldsearch&task=viewer&templ=component&"
                Settings.SetIni("LoginService", "SearchURL", SearchURL)
                Settings.SetIni("LoginService", "DestinationGuide", "https://hyperica.com/destination-guide")

                If Settings.GloebitsEnable Then
                    Settings.SetIni("LoginService", "Currency", "G$")
                Else
                    Settings.SetIni("LoginService", "Currency", "J$")
                End If
            Else
                Settings.SetIni("LoginService", "SearchURL", "http://hyperica.com/Search/query.php")
                Settings.SetIni("LoginService", "DestinationGuide", "https://hyperica.com/destination-guide")

                If Settings.GloebitsEnable Then
                    Settings.SetIni("LoginService", "Currency", "G$")
                Else
                    Settings.SetIni("LoginService", "Currency", "$")
                End If

            End If
        Else
            Settings.SetIni("LoginService", "SearchURL", "")
        End If

    End Sub

    Private Shared Sub ShowLog()
        ''' <summary>Shows the log buttons if diags fail</summary>
        Try
            System.Diagnostics.Process.Start(Settings.CurrentDirectory & "\baretail.exe", """" & Settings.CurrentDirectory & "\OutworldzFiles\Outworldz.log" & """")
        Catch ex As Exception

            BreakPoint.Show(ex.Message)
        End Try

    End Sub

    Private Shared Function Stripqq(input As String) As String

        Return Replace(input, """", "")

    End Function

    <CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId:="Outworldz.Form1.Log(System.String,System.String)")>
    Private Shared Sub Zap(processName As String)

        ''' <summary>Kill processes by name</summary>
        ''' <param name="processName"></param>
        ''' <returns></returns>

        ' Kill process by name
        For Each P As Process In System.Diagnostics.Process.GetProcessesByName(processName)
            Log(My.Resources.Info_word, "Stopping process " & processName)
            Try
                P.Kill()
            Catch ex As Exception

                BreakPoint.Show(ex.Message)
            End Try
            Application.DoEvents()
        Next

    End Sub

    Private Sub ApacheIs(Running As Boolean)

        If Not Running Then
            RestartApacheItem.Image = Global.Outworldz.My.Resources.nav_plain_red
        Else
            RestartApacheItem.Image = Global.Outworldz.My.Resources.check2
        End If
        Application.DoEvents()

    End Sub

    Private Sub ApachePictureBox_Click(sender As Object, e As EventArgs)

        If Not CheckApache() Then
            StartApache()
        Else
            StopApache(True) 'Do Stop, even If a service
        End If

    End Sub

    ' Handle Exited event and display process information.
    Private Sub ApacheProcess_Exited(ByVal sender As Object, ByVal e As EventArgs) Handles ApacheProcess.Exited

        If PropAborting Then Return
        If PropApacheUninstalling Then Return

        If Settings.RestartOnCrash And _ApacheCrashCounter < 10 Then
            _ApacheCrashCounter += 1
            PropApacheExited = True
            Return
        End If
        _ApacheCrashCounter = 0
        PropgApacheProcessID = Nothing

        Dim yesno = MsgBox(My.Resources.Apache_Exited, vbYesNo, Global.Outworldz.My.Resources.Error_word)
        If (yesno = vbYes) Then
            Dim Apachelog As String = Settings.CurrentDirectory & "\Outworldzfiles\Apache\logs\error*.log"
            Try
                System.Diagnostics.Process.Start(Settings.CurrentDirectory & "\baretail.exe", """" & Apachelog & """")
            Catch ex As Exception

                BreakPoint.Show(ex.Message)
            End Try
        End If

    End Sub

    Private Sub Backupper()

        For Each RegionUUID As String In PropRegionClass.RegionUUIDs
            If PropRegionClass.IsBooted(RegionUUID) Then
                'Print("Backing up " & PropRegionClass.RegionName(RegionUUID))
                ConsoleCommand(RegionUUID, "change region " & """" & PropRegionClass.RegionName(RegionUUID) & """" & "{ENTER}" & vbCrLf)
                ConsoleCommand(RegionUUID, "save oar  " & """" & BackupPath() & PropRegionClass.RegionName(RegionUUID) & "_" & DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss", Globalization.CultureInfo.InvariantCulture) & ".oar" & """" & "{ENTER}" & vbCrLf)
                SequentialPause()   ' wait for previous region to give us some CPU
                Dim hwnd = GetHwnd(PropRegionClass.GroupName(RegionUUID))
                ShowDOSWindow(hwnd, Form1.SHOWWINDOWENUM.SWMINIMIZE)
            End If
        Next

    End Sub

    Private Sub BusyButton_Click(sender As Object, e As EventArgs) Handles BusyButton.Click

        PropAborting = True
        ClearAllRegions()
        Timer1.Stop()

        PropUpdateView = True ' make form refresh

        PropOpensimIsRunning() = False
        ToolBar(False)
        Print(My.Resources.Stopped_word)
        Buttons(StartButton)

    End Sub

    <CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId:="System.Windows.Forms.Label.set_Text(System.String)")>
    Private Sub Chart()
        ' Graph https://github.com/sinairv/MSChartWrapper
        Try
            ' running average
            speed3 = speed2
            speed2 = speed1
            speed1 = speed
            Try
                speed = Me.Cpu1.NextValue()
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
                Dim pUpdate As Process = New Process()
                Dim pi As ProcessStartInfo = New ProcessStartInfo With {
                    .Arguments = "/ R",
                    .FileName = "loadctr"
                }
                pUpdate.StartInfo = pi
                pUpdate.Start()
                pUpdate.WaitForExit()
                pUpdate.Dispose()
            End Try

            CPUAverageSpeed = (speed + speed1 + speed2 + speed3) / 4

            MyCPUCollection.Add(CPUAverageSpeed)

            If MyCPUCollection.Count > 180 Then MyCPUCollection.RemoveAt(0)

            PercentCPU.Text = String.Format(Globalization.CultureInfo.InvariantCulture, "{0: 0}% CPU", CPUAverageSpeed)
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
            ' ErrorLog(ex.Message)
        End Try

        ''reverse series

        ChartWrapper1.ClearChart()
        Dim CPU1() As Double = MyCPUCollection.ToArray()
        ChartWrapper1.AddLinePlot("CPU", CPU1)

        'RAM

        Dim wql As ObjectQuery = New ObjectQuery("SELECT TotalVisibleMemorySize,FreePhysicalMemory FROM Win32_OperatingSystem")
        Dim searcher As ManagementObjectSearcher = New ManagementObjectSearcher(wql)
        Dim results As ManagementObjectCollection = searcher.Get()
        searcher.Dispose()

        Try
            For Each result In results
                Dim value As Double = (CDbl(result("TotalVisibleMemorySize").ToString) - CDbl(result("FreePhysicalMemory").ToString)) / CDbl(result("TotalVisibleMemorySize").ToString) * 100
                MyRAMCollection.Add(value)
                If MyRAMCollection.Count > 180 Then MyRAMCollection.RemoveAt(0)

                value = Math.Round(value)
                PercentRAM.Text = CStr(value) & "% RAM"

            Next
        Catch ex As Exception

            BreakPoint.Show(ex.Message)
            ErrorLog(ex.Message)
        End Try

        ChartWrapper2.ClearChart()
        Dim RAM() As Double = MyRAMCollection.ToArray()
        ChartWrapper2.AddLinePlot("RAM", RAM)

    End Sub

    Private Function CheckApache() As Boolean
        ''' <summary>Check is Apache port 80 or 8000 is up</summary>
        ''' <returns>boolean</returns>
        Using client As New WebClient ' download client for web pages
            Dim Up As String
            Try
                Up = client.DownloadString("http://" & Settings.PublicIP & ":" & CStr(Settings.ApachePort) & "/?_Opensim=" & RandomNumber.Random)
            Catch ex As Exception

                BreakPoint.Show(ex.Message)
                If ex.Message.Contains("200 OK") Then Return True
                Return False
            End Try
            If Up.Length = 0 And PropOpensimIsRunning() Then
                Return False
            End If

        End Using

        Return True

    End Function

    Private Sub CheckDiagPort()

        PropUseIcons = True
        Print(My.Resources.Check_Diag)
        Dim wsstarted = CheckPort("127.0.0.1", CType(Settings.DiagnosticPort, Integer))
        If wsstarted = False Then
            MsgBox(My.Resources.Diag_Port_word & " " & Settings.DiagnosticPort & ". " & Global.Outworldz.My.Resources.Diag_Broken)
            PropUseIcons = False
        End If

    End Sub

    Private Function CheckIcecast() As Boolean
        ''' <summary>Check is Icecast port 8081 is up</summary>
        ''' <returns>boolean</returns>
        Using client As New WebClient ' download client for web pages
            Dim Up As String
            Try
                Up = client.DownloadString("http://" & Settings.PublicIP & ":" & Settings.SCPortBase & "/?_Opensim=" & RandomNumber.Random())
            Catch ex As Exception

                BreakPoint.Show(ex.Message)
                Return False
            End Try

            If Up.Length = 0 And PropOpensimIsRunning() Then
                Return False
            End If
        End Using
        Return True

    End Function

    Private Sub ClearAllRegions()

        For Each RegionUUID As String In PropRegionClass.RegionUUIDs
            Logger("State is Stopped", PropRegionClass.RegionName(RegionUUID), "Restart")
            PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.Stopped
            PropRegionClass.ProcessID(RegionUUID) = 0
            PropRegionClass.Timer(RegionUUID) = RegionMaker.REGIONTIMER.Stopped
        Next
        Try
            PropExitList.Clear()
            PropRegionClass.ClearStack()
            PropRegionHandles.Clear()
            PropRegionClass.WebserverList.Clear()
        Catch ex As Exception

            BreakPoint.Show(ex.Message)
        End Try

    End Sub

    Private Sub CreateService()

        ' create test program slants the other way:
        Dim testProgram As String = Settings.CurrentDirectory & "\OutworldzFiles\Mysql\bin\InstallAsAService.bat"
        FileStuff.DeleteFile(testProgram)

        Try
            Using outputFile As New StreamWriter(testProgram, True)
                outputFile.WriteLine("@REM Program to run Mysql as a Service" & vbCrLf +
            "mysqld.exe --install Mysql --defaults-file=" & """" & PropCurSlashDir & "/OutworldzFiles/mysql/my.ini" & """" & vbCrLf & "net start Mysql" & vbCrLf)
            End Using
        Catch ex As Exception

            BreakPoint.Show(ex.Message)
        End Try

    End Sub

    Private Sub Debug_Click(sender As Object, e As EventArgs) Handles Debug.Click

        Settings.LogLevel = "DEBUG"
        System.Environment.SetEnvironmentVariable("OSIM_LOGLEVEL", Settings.LogLevel.ToUpperInvariant)
        SendMsg(Settings.LogLevel)

    End Sub

    Private Function DoApache() As Boolean

        If Not Settings.ApacheEnable Then Return False
        Print("->Set Apache")

        ' lean rightward paths for Apache
        Dim ini = Settings.CurrentDirectory & "\Outworldzfiles\Apache\conf\httpd.conf"
        Settings.LoadLiteralIni(ini)
        Settings.SetLiteralIni("Listen", "Listen " & Convert.ToString(Settings.ApachePort, Globalization.CultureInfo.InvariantCulture))
        Settings.SetLiteralIni("Define SRVROOT", "Define SRVROOT " & """" & PropCurSlashDir & "/Outworldzfiles/Apache" & """")
        Settings.SetLiteralIni("DocumentRoot", "DocumentRoot " & """" & PropCurSlashDir & "/Outworldzfiles/Apache/htdocs" & """")
        Settings.SetLiteralIni("Use VDir", "Use VDir " & """" & PropCurSlashDir & "/Outworldzfiles/Apache/htdocs" & """")
        Settings.SetLiteralIni("PHPIniDir", "PHPIniDir " & """" & PropCurSlashDir & "/Outworldzfiles/PHP7" & """")
        Settings.SetLiteralIni("ServerName", "ServerName " & Settings.PublicIP)
        Settings.SetLiteralIni("ServerAdmin", "ServerAdmin " & Settings.AdminEmail)
        Settings.SetLiteralIni("<VirtualHost", "<VirtualHost  *:" & Convert.ToString(Settings.ApachePort, Globalization.CultureInfo.InvariantCulture) & ">")
        Settings.SetLiteralIni("ErrorLog", "ErrorLog " & """|bin/rotatelogs.exe  -l \" & """" & PropCurSlashDir & "/Outworldzfiles/Apache/logs/Error-%Y-%m-%d.log" & "\" & """" & " 86400""")
        Settings.SetLiteralIni("CustomLog", "CustomLog " & """|bin/rotatelogs.exe -l \" & """" & PropCurSlashDir & "/Outworldzfiles/Apache/logs/access-%Y-%m-%d.log" & "\" & """" & " 86400""" & " common env=!dontlog")
        ' needed for Php5 upgrade
        Settings.SetLiteralIni("LoadModule php5_module", "LoadModule php7_module")
        Settings.SetLiteralIni("LoadModule php7_module", "LoadModule php7_module " & """" & PropCurSlashDir & "/OutworldzFiles/PHP7/php7apache2_4.dll" & """")

        Settings.SaveLiteralIni(ini, "httpd.conf")

        If IO.File.Exists(Settings.CurrentDirectory & "\Outworldzfiles\PHP5") Then
            Directory.Delete(Settings.CurrentDirectory & "\Outworldzfiles\PHP5", True)
        End If

        ' lean rightward paths for Apache
        ini = Settings.CurrentDirectory & "\Outworldzfiles\Apache\conf\extra\httpd-ssl.conf"
        Settings.LoadLiteralIni(ini)
        Settings.SetLiteralIni("Listen", "Listen " & Settings.PrivateURL & ":" & "443")
        Settings.SetLiteralIni("ServerName", "ServerName " & Settings.PublicIP)

        Settings.SaveLiteralIni(ini, "httpd-ssl.conf")

        Return False

    End Function

    Private Sub DoDiag()

        If IPCheck.IsPrivateIP(Settings.DNSName) Then
            Logger("INFO", Global.Outworldz.My.Resources.LAN_IP, "Diagnostics")
            Print(My.Resources.LAN_IP)
            Return
        End If

        Print("---------------------------")
        Print(My.Resources.Running_Network)
        Logger("INFO", Global.Outworldz.My.Resources.Running_Network, "Diagnostics")
        Settings.DiagFailed = False

        OpenPorts() ' Open router ports with UPnp
        ProbePublicPort() ' Probe using Outworldz like Canyouseeme.org does on HTTP port
        TestPrivateLoopback()   ' Diagnostics
        TestPublicLoopback()    ' Http port
        TestAllRegionPorts()    ' All Dos boxes, actually

        If Settings.DiagFailed Then
            Logger("Error", Global.Outworldz.My.Resources.Diags_Failed, "Diagnostics")
            Dim answer = MsgBox(My.Resources.Diags_Failed, vbYesNo)
            If answer = vbYes Then
                ShowLog()
            End If
        Else
            NewDNSName()
        End If
        Print("---------------------------")

    End Sub

    Private Function DoEditForeigners() As Boolean

        Print("->Set Residents/Foreigners")
        ' adds a list like 'Region_Test_1 = "DisallowForeigners"' to Gridcommon.ini

        Dim Authorizationlist As String = ""
        For Each RegionUUID As String In PropRegionClass.RegionUUIDs

            Dim RegionName = PropRegionClass.RegionName(RegionUUID)
            '(replace spaces with underscore)
            RegionName = RegionName.Replace(" ", "_")    ' because this is a screwy thing they did in the INI file
            Dim df As Boolean = False
            Dim dr As Boolean = False
            If PropRegionClass.DisallowForeigners(RegionUUID) = "True" Then
                df = True
            End If
            If PropRegionClass.DisallowResidents(RegionUUID) = "True" Then
                dr = True
            End If
            If Not dr And Not df Then

            ElseIf dr And Not df Then
                Authorizationlist += "Region_" & RegionName & " = DisallowResidents" & vbCrLf
            ElseIf Not dr And df Then
                Authorizationlist += "Region_" & RegionName & " = DisallowForeigners" & vbCrLf
            ElseIf dr And df Then
                Authorizationlist += "Region_" & RegionName & " = DisallowResidents " & vbCrLf
            End If
            Application.DoEvents()
        Next

        Dim reader As StreamReader
        Dim line As String
        Dim Output As String = ""

        Try
            reader = System.IO.File.OpenText(Settings.OpensimBinPath & "config-include\GridCommon.ini")
            'now loop through each line
            Dim skip As Boolean = False
            While reader.Peek <> -1
                line = reader.ReadLine()

                If line.StartsWith("; START", StringComparison.InvariantCulture) Then
                    Output += line & vbCrLf
                    Output += Authorizationlist
                    skip = True
                ElseIf line.StartsWith("; END", StringComparison.InvariantCulture) Then
                    Output += line & vbCrLf
                    skip = False
                Else
                    If Not skip Then Output += line & vbCrLf
                End If

            End While

            'close the reader
            reader.Close()

            FileStuff.DeleteFile(Settings.OpensimBinPath & "config-include\GridCommon.ini")

            Using outputFile As New StreamWriter(CType(Settings.OpensimBinPath & "config-include\Gridcommon.ini", String))
                outputFile.Write(Output)
            End Using
        Catch ex As Exception

            BreakPoint.Show(ex.Message)
            ErrorLog(ex.Message)
        End Try

        Return False

    End Function

    Private Function DoFlotsamINI() As Boolean

        If Settings.LoadIni(Settings.OpensimBinPath & "config-include\FlotsamCache.ini", ";") Then Return True
        Print("->Set Flotsam Cache")
        Settings.SetIni("AssetCache", "LogLevel", Settings.CacheLogLevel)
        Settings.SetIni("AssetCache", "CacheDirectory", Settings.CacheFolder)
        Settings.SetIni("AssetCache", "FileCacheEnabled", CStr(Settings.CacheEnabled))
        Settings.SetIni("AssetCache", "FileCacheTimeout", Settings.CacheTimeout)
        Settings.SaveINI(System.Text.Encoding.ASCII)
        Return False

    End Function

    Private Function DoGridCommon() As Boolean

        Print("->Set GridCommon.ini")

        'Choose a GridCommon.ini to use.
        Dim GridCommon As String = "Gridcommon-GridServer.ini"

        Select Case Settings.ServerType
            Case "Robust"
                Try
                    My.Computer.FileSystem.CopyDirectory(Settings.OpensimBinPath & "Library.proto", Settings.OpensimBinPath & "Library", True)
                Catch ex As Exception

                    BreakPoint.Show(ex.Message)
                End Try
                GridCommon = "Gridcommon-GridServer.ini"
            Case "Region"
                Try
                    My.Computer.FileSystem.CopyDirectory(Settings.OpensimBinPath & "Library.proto", Settings.OpensimBinPath & "Library", True)
                Catch ex As Exception

                    BreakPoint.Show(ex.Message)
                End Try
                GridCommon = "Gridcommon-RegionServer.ini"
            Case "OsGrid"
                GridCommon = "Gridcommon-OsGridServer.ini"
            Case "Metro"
                GridCommon = "Gridcommon-MetroServer.ini"

        End Select

        ' Put that gridcommon.ini file in place
        FileStuff.CopyFile(Settings.OpensimBinPath & "config-include\" & GridCommon, IO.Path.Combine(Settings.OpensimBinPath, "config-include\GridCommon.ini"), True)

        If Settings.LoadIni(Settings.OpensimBinPath & "config-include\GridCommon.ini", ";") Then Return True
        Settings.SetIni("HGInventoryAccessModule", "OutboundPermission", CStr(Settings.OutBoundPermissions))
        Settings.SetIni("DatabaseService", "ConnectionString", Settings.RegionDBConnection)

        ' ;; Send visual reminder to local users that their inventories are unavailable while they
        ' are traveling ;; and available when they return. True by default.
        If Settings.Suitcase Then
            Settings.SetIni("HGInventoryAccessModule", "RestrictInventoryAccessAbroad", "true")
        Else
            Settings.SetIni("HGInventoryAccessModule", "RestrictInventoryAccessAbroad", "false")
        End If

        Settings.SaveINI(System.Text.Encoding.UTF8)
        Return False

    End Function

    Private Function DoPHP() As Boolean

        Print("->Set PHP7")
        Dim ini = Settings.CurrentDirectory & "\Outworldzfiles\PHP7\php.ini"
        Settings.LoadLiteralIni(ini)
        Settings.SetLiteralIni("extension_dir", "extension_dir = " & """" & PropCurSlashDir & "/OutworldzFiles/PHP7/ext/""")
        Settings.SetLiteralIni("doc_root", "doc_root = """ & PropCurSlashDir & "/OutworldzFiles/Apache/htdocs/""")

        Settings.SaveLiteralIni(ini, "php.ini")

        Return False

    End Function

    Private Function DoPHPDBSetup() As Boolean

        Print("->Set Maps")
        Dim phptext = "<?php " & vbCrLf &
"/* General Domain */" & vbCrLf &
"$CONF_domain        = " & """" & Settings.PublicIP & """" & "; " & vbCrLf &
"$CONF_port          = " & """" & Settings.HttpPort & """" & "; " & vbCrLf &
"$CONF_sim_domain    = " & """" & "http://" & Settings.PublicIP & "/" & """" & ";" & vbCrLf &
"$CONF_install_path  = " & """" & "/Metromap" & """" & ";   // Installation path " & vbCrLf &
"/* MySQL Database */ " & vbCrLf &
"$CONF_db_server     = " & """" & Settings.RobustServer & """" & "; // Address Of Robust Server " & vbCrLf &
"$CONF_db_port       = " & """" & CStr(Settings.MySqlRobustDBPort) & """" & "; // Robust port " & vbCrLf &
"$CONF_db_user       = " & """" & Settings.RobustUsername & """" & ";  // login " & vbCrLf &
"$CONF_db_pass       = " & """" & Settings.RobustPassword & """" & ";  // password " & vbCrLf &
"$CONF_db_database   = " & """" & Settings.RobustDataBaseName & """" & ";     // Name Of Robust Server " & vbCrLf &
"/* The Coordinates Of the Grid-Center */ " & vbCrLf &
"$CONF_center_coord_x = " & """" & CStr(Settings.MapCenterX) & """" & ";		// the Center-X-Coordinate " & vbCrLf &
"$CONF_center_coord_y = " & """" & CStr(Settings.MapCenterY) & """" & ";		// the Center-Y-Coordinate " & vbCrLf &
"// style-sheet items" & vbCrLf &
"$CONF_style_sheet     = " & """" & "/css/stylesheet.css" & """" & ";          //Link To your StyleSheet" & vbCrLf &
"$CONF_HOME            = " & """" & Settings.CMS & """" & ";          //Link To your Home Folder in htdocs.  WordPress, DreamGrid, Joomla/JOpensim or user assigned folder" & vbCrLf &
"?>"

        Using outputFile As New StreamWriter(Settings.CurrentDirectory & "\OutworldzFiles\Apache\htdocs\MetroMap\includes\config.php", False)
            outputFile.WriteLine(phptext)
        End Using

        phptext = "<?php " & vbCrLf &
"$DB_GRIDNAME = " & """" & Settings.PublicIP & ":" & Settings.HttpPort & """" & ";" & vbCrLf &
"$DB_HOST = " & """" & Settings.RobustServer & """" & ";" & vbCrLf &
"$DB_PORT = " & """" & CStr(Settings.MySqlRobustDBPort) & """" & "; // Robust port " & vbCrLf &
"$DB_USER = " & """" & Settings.RobustUsername & """" & ";" & vbCrLf &
"$DB_PASSWORD = " & """" & Settings.RobustPassword & """" & ";" & vbCrLf &
"$DB_NAME = " & """" & "ossearch" & """" & ";" & vbCrLf &
"?>"

        'Using outputFile As New StreamWriter(Settings.CurrentDirectory & "\OutworldzFiles\Apache\htdocs\Search\databaseinfo.php", False)
        'outputFile.WriteLine(phptext)
        'End Using
        Using outputFile As New StreamWriter(Settings.CurrentDirectory & "\OutworldzFiles\PHP7\databaseinfo.php", False)
            outputFile.WriteLine(phptext)
        End Using

        Return False

    End Function

    Private Function DoRobust() As Boolean

        Print("->Set Robust")

        ' Robust Process
        If Settings.LoadIni(Settings.OpensimBinPath & "Robust.HG.ini", ";") Then
            Return True
        End If

        If Settings.AltDnsName.Length > 0 Then
            Settings.SetIni("Hypergrid", "HomeURIAlias", Settings.AltDnsName)
            Settings.SetIni("Hypergrid", "GatekeeperURIAlias", Settings.AltDnsName)
        End If

        Settings.SetIni("DatabaseService", "ConnectionString", Settings.RobustDBConnection)
        Settings.SetIni("Const", "GridName", Settings.SimName)
        Settings.SetIni("Const", "BaseURL", "http://" & Settings.PublicIP)

        Settings.SetIni("Const", "PrivURL", "http://" & Settings.PrivateURL)
        Settings.SetIni("Const", "PublicPort", Convert.ToString(Settings.HttpPort, Globalization.CultureInfo.InvariantCulture)) ' 8002
        Settings.SetIni("Const", "PrivatePort", Convert.ToString(Settings.PrivatePort, Globalization.CultureInfo.InvariantCulture))
        Settings.SetIni("Const", "http_listener_port", Convert.ToString(Settings.HttpPort, Globalization.CultureInfo.InvariantCulture))
        Settings.SetIni("GridInfoService", "welcome", Settings.SplashPage)

        If Settings.Suitcase() Then
            Settings.SetIni("HGInventoryService", "LocalServiceModule", "OpenSim.Services.HypergridService.dll:HGSuitcaseInventoryService")
        Else
            Settings.SetIni("HGInventoryService", "LocalServiceModule", "OpenSim.Services.HypergridService.dll:HGInventoryService")
        End If

        ' LSL emails
        Settings.SetIni("SMTP", "SMTP_SERVER_HOSTNAME", Settings.SmtpHost)
        Settings.SetIni("SMTP", "SMTP_SERVER_PORT", Convert.ToString(Settings.SmtpPort, Globalization.CultureInfo.InvariantCulture))
        Settings.SetIni("SMTP", "SMTP_SERVER_LOGIN", Settings.SmtPropUserName)
        Settings.SetIni("SMTP", "SMTP_SERVER_PASSWORD", Settings.SmtpPassword)

        SetupRobustSearchINI()

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

        If Settings.CMS = "JOpensim" Then
            Settings.SetIni("ServiceList", "GetTextureConnector", "${Const|PublicPort}/Opensim.Capabilities.Handlers.dll:GetTextureSeverConnector")
        Else
            Settings.SetIni("ServiceList", "GetTextureConnector", "")
        End If

        Settings.SaveINI(System.Text.Encoding.UTF8)

        Return False
    End Function

    Private Function DoSetDefaultSims() As Boolean

        Print("->Set Default Sims")
        ' set the defaults in the INI for the viewer to use. Painful to do as it's a Left hand side
        ' edit must be done before other edits to Robust.HG.ini as this makes the actual
        ' Robust.HG.ifile
        Dim reader As StreamReader
        Dim line As String

        Try
            ' add this sim name as a default to the file as HG regions, and add the other regions as
            ' fallback it may have been deleted
            Dim WelcomeUUID As String = PropRegionClass.FindRegionByName(Settings.WelcomeRegion)

            If WelcomeUUID.Length = 0 Then
                MsgBox(My.Resources.Cannot_locate, vbInformation)
                Return True
            End If

            Dim DefaultName = Settings.WelcomeRegion

            FileStuff.DeleteFile(Settings.OpensimBinPath & "Robust.HG.ini")

            ' Replace the block with a list of regions with the Region_Name = DefaultRegion,
            ' DefaultHGRegion is Welcome Region_Name = FallbackRegion, Persistent if a Smart Start
            ' region and SS is enabled Region_Name = FallbackRegion if not a SmartStart

            Dim RegionSetting As String = Nothing

            ' make a long list of the various regions with region_ at the start
            For Each RegionUUID As String In PropRegionClass.RegionUUIDs
                Dim RegionName = PropRegionClass.RegionName(RegionUUID)
                If RegionName <> Settings.WelcomeRegion Then
                    If Settings.SmartStart And PropRegionClass.SmartStart(RegionUUID) = "True" Then
                        RegionName = RegionName.Replace(" ", "_")    ' because this is a screwy thing they did in the INI file
                        RegionSetting += "Region_" & RegionName & " = " & "FallbackRegion, Persistent" & vbCrLf
                    Else
                        RegionName = RegionName.Replace(" ", "_")    ' because this is a screwy thing they did in the INI file
                        RegionSetting += "Region_" & RegionName & " = " & "FallbackRegion" & vbCrLf
                    End If
                Else
                    RegionName = DefaultName.Replace(" ", "_")    ' because this is a screwy thing they did in the INI file
                    RegionSetting += "Region_" & RegionName & " = " & """" & "DefaultRegion, DefaultHGRegion" & """" & vbCrLf
                End If

            Next

            Dim skip As Boolean = False
            Using outputFile As New StreamWriter(Settings.OpensimBinPath & "Robust.HG.ini")
                reader = System.IO.File.OpenText(Settings.OpensimBinPath & "Robust.HG.ini.proto")
                'now loop through each line
                While reader.Peek <> -1
                    line = reader.ReadLine()
                    Dim Output As String = Nothing
                    'Diagnostics.Debug.Print(line)
                    If line.StartsWith("; START", StringComparison.InvariantCulture) Then
                        Output += line & vbCrLf ' add back on the ; START
                        Output += RegionSetting
                        skip = True
                    ElseIf line.StartsWith("; END", StringComparison.InvariantCulture) Then ' add back on the ; END
                        Output += line & vbCrLf
                        skip = False
                    Else
                        If Not skip Then Output += line & vbCrLf
                    End If

                    'Diagnostics.Debug.Print(Output)
                    outputFile.WriteLine(Output)
                    Application.DoEvents()
                End While
            End Using
            'close your reader
            reader.Close()
        Catch ex As Exception

            BreakPoint.Show(ex.Message)
            MsgBox(My.Resources.no_Default_sim, vbInformation, Global.Outworldz.My.Resources.Settings_word)
            Return True
        End Try

        ' needs to be set up after the above
        If DoRobust() Then Return True

        Return False

    End Function

    Private Sub DoSuspend_Resume(RegionName As String, Optional ResumeSwitch As Boolean = False)

        Dim RegionUUID As String = PropRegionClass.FindRegionByName(RegionName)
        Dim PID = PropRegionClass.ProcessID(RegionUUID)

        Dim R As String
        If ResumeSwitch Then
            R = " -rpid "
            Print(My.Resources.Resuming_word & " " & RegionName)
        Else
            Print(My.Resources.Suspending_word & " " & RegionName)
            R = " -pid "
        End If
        Dim SuspendProcess As New Process()
        Dim pi As ProcessStartInfo = New ProcessStartInfo With {
              .Arguments = R & PID,
              .FileName = """" & Settings.CurrentDirectory & "\NtSuspendProcess64.exe" & """"
          }

        If Debugger.IsAttached Then
            pi.WindowStyle = ProcessWindowStyle.Normal
        Else
            pi.WindowStyle = ProcessWindowStyle.Minimized
        End If

        SuspendProcess.StartInfo = pi

        Try
            SuspendProcess.Start()
            SuspendProcess.WaitForExit()
        Catch ex As Exception

            BreakPoint.Show(ex.Message)
            Print(My.Resources.NTSuspend)
        Finally
            SuspendProcess.Close()
        End Try

        Dim GroupName = PropRegionClass.GroupName(RegionUUID)
        For Each UUID In PropRegionClass.RegionUUIDListByName(GroupName)
            If ResumeSwitch Then
                PropRegionClass.Timer(UUID) = RegionMaker.REGIONTIMER.StartCounting
                PropRegionClass.Status(UUID) = RegionMaker.SIMSTATUSENUM.Booted
            Else
                PropRegionClass.Timer(UUID) = RegionMaker.REGIONTIMER.Stopped
                PropRegionClass.Status(UUID) = RegionMaker.SIMSTATUSENUM.Suspended
            End If
        Next
        PropUpdateView = True ' make form refresh

    End Sub

    Private Function DoTides() As Boolean

        Dim TideData As String = ""
        Dim TideFile = Settings.OpensimBinPath & "addon-modules\OpenSimTide\config\OpenSimTide.ini"
        If IO.File.Exists(TideFile) Then
            System.IO.File.Delete(TideFile)
        End If

        For Each RegionUUID As String In PropRegionClass.RegionUUIDs
            Dim RegionName = PropRegionClass.RegionName(RegionUUID)
            'Tides Setup per region
            If Settings.TideEnabled And PropRegionClass.Tides(RegionUUID) = "True" Then

                TideData = TideData & ";; Set the Tide settings per named region" & vbCrLf &
                    "[" & RegionName & "]" & vbCrLf &
                ";this determines whether the module does anything in this region" & vbCrLf &
                ";# {TideEnabled} {} {Enable the tide to come in And out?} {true false} false" & vbCrLf &
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
                "TideHighWater = " & Convert.ToString(Settings.TideHighLevel(), Globalization.CultureInfo.InvariantCulture) & vbCrLf &
                "TideLowWater = " & Convert.ToString(Settings.TideLowLevel(), Globalization.CultureInfo.InvariantCulture) & vbCrLf &
                vbCrLf &
                ";; how long in seconds for a complete cycle time low->high->low" & vbCrLf &
                "TideCycleTime = " & Convert.ToString(Settings.CycleTime(), Globalization.CultureInfo.InvariantCulture) & vbCrLf &
                    vbCrLf &
                ";; provide tide information on the console?" & vbCrLf &
                "TideInfoDebug = " & CStr(Settings.TideInfoDebug) & vbCrLf &
                    vbCrLf &
                ";; chat tide info to the whole region?" & vbCrLf &
                "TideInfoBroadcast = " & Settings.BroadcastTideInfo() & vbCrLf &
                    vbCrLf &
                ";; which channel to region chat on for the full tide info" & vbCrLf &
                "TideInfoChannel = " & CStr(Settings.TideInfoChannel) & vbCrLf &
                vbCrLf &
                ";; which channel to region chat on for just the tide level in metres" & vbCrLf &
                "TideLevelChannel = " & Convert.ToString(Settings.TideLevelChannel(), Globalization.CultureInfo.InvariantCulture) & vbCrLf &
                    vbCrLf &
                ";; How many times to repeat Tide Warning messages at high/low tide" & vbCrLf &
                "TideAnnounceCount = 1" & vbCrLf & vbCrLf & vbCrLf & vbCrLf
            End If

        Next
        IO.File.WriteAllText(TideFile, TideData, Encoding.Default) 'The text file will be created if it does not already exist

        Return False

    End Function

    Private Function DoWifi() As Boolean

        If Settings.LoadIni(Settings.OpensimBinPath & "Wifi.ini", ";") Then Return True

        Print("->Set Diva Wifi page")

        Settings.SetIni("DatabaseService", "ConnectionString", Settings.RobustDBConnection)

        ' Wifi Section

        If Settings.ServerType = "Robust" Then ' wifi could be on or off
            If (Settings.WifiEnabled) Then
                Settings.SetIni("WifiService", "Enabled", "True")
            Else
                Settings.SetIni("WifiService", "Enabled", "False")
            End If
        Else ' it is always off
            ' shutdown wifi in Attached mode
            Settings.SetIni("WifiService", "Enabled", "False")
        End If

        Settings.SetIni("WifiService", "GridName", Settings.SimName)
        Settings.SetIni("WifiService", "LoginURL", "http://" & Settings.PublicIP & ":" & Settings.HttpPort)
        Settings.SetIni("WifiService", "WebAddress", "http://" & Settings.PublicIP & ":" & Settings.HttpPort)

        ' Wifi Admin'
        Settings.SetIni("WifiService", "AdminFirst", Settings.AdminFirst)    ' Wifi
        Settings.SetIni("WifiService", "AdminLast", Settings.AdminLast)      ' Admin
        Settings.SetIni("WifiService", "AdminPassword", Settings.Password)   ' secret
        Settings.SetIni("WifiService", "AdminEmail", Settings.AdminEmail)    ' send notifications to this person

        'Gmail and other SMTP mailers
        ' Gmail requires you set to set low security access
        Settings.SetIni("WifiService", "SmtpHost", Settings.SmtpHost)
        Settings.SetIni("WifiService", "SmtpPort", Convert.ToString(Settings.SmtpPort, Globalization.CultureInfo.InvariantCulture))
        Settings.SetIni("WifiService", "SmtpUsername", Settings.SmtPropUserName)
        Settings.SetIni("WifiService", "SmtpPassword", Settings.SmtpPassword)

        Settings.SetIni("WifiService", "HomeLocation", Settings.WelcomeRegion & "/" & Settings.HomeVectorX & "/" & Settings.HomeVectorY & "/" & Settings.HomeVectorZ)

        If Settings.AccountConfirmationRequired Then
            Settings.SetIni("WifiService", "AccountConfirmationRequired", "True")
        Else
            Settings.SetIni("WifiService", "AccountConfirmationRequired", "False")
        End If

        Settings.SaveINI(System.Text.Encoding.UTF8)
        Return False

    End Function

    <CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId:="Outworldz.Form1.Logger(System.String,System.String,System.String)")>
    Private Sub ExitHandlerPoll()

        If PropAborting Then Return ' not if we are aborting

        If PropExitHandlerIsBusy Then Return
        PropExitHandlerIsBusy = True

        While BootedList1.Count > 0
            Dim R = BootedList1(0)
            BootedList1.RemoveAt(0)
            Logger("RegionReady Booted:", PropRegionClass.RegionName(R), "Restart")
            PropRegionClass.Timer(R) = RegionMaker.REGIONTIMER.StartCounting
            PropRegionClass.Status(R) = RegionMaker.SIMSTATUSENUM.Booted
            Print(PropRegionClass.RegionName(R) & " " & Global.Outworldz.My.Resources.Running_word)
            PropUpdateView = True
        End While

        Dim GroupName As String
        Dim TimerValue As Integer

        For Each RegionUUID As String In PropRegionClass.RegionUUIDs
            Application.DoEvents()
            ' count up to auto restart, when high enough, restart the sim
            If PropRegionClass.Timer(RegionUUID) >= 0 Then
                PropRegionClass.Timer(RegionUUID) += 1
            End If

            GroupName = PropRegionClass.GroupName(RegionUUID)
            Dim Status = PropRegionClass.Status(RegionUUID)
            ' Logger(GetStateString(Status), GroupName, "Restart")
            Dim RegionName = PropRegionClass.RegionName(RegionUUID)
            Dim GroupList = PropRegionClass.RegionUUIDListByName(GroupName)

            If PropOpensimIsRunning() Then

                'Stopped = 0
                'Booting = 1
                'Booted = 2
                'RecyclingUp = 3
                'RecyclingDown = 4
                'ShuttingDown = 5
                'RestartPending = 6
                'RetartingNow = 7
                '[Resume] = 8
                'Suspended = 9
                '[Error] = 10
                'RestartStage2 = 11

                If Status = RegionMaker.SIMSTATUSENUM.Stopped Then

                    'Stopped = 0
                    'Logger("State is Stopped", GroupName, "Restart")
                    Continue For

                ElseIf Status = RegionMaker.SIMSTATUSENUM.Booting Then

                    'Booting = 1
                    Logger("State is Booting", GroupName, "Restart")
                    Continue For

                ElseIf Status = RegionMaker.SIMSTATUSENUM.Booted Then

                    'Booted = 2
                    'Logger("State is Booted", GroupName, "Restart")

                    ' May be too long running?
                    TimerValue = PropRegionClass.Timer(RegionUUID)
                    If TimerValue >= 0 Then

                        'How Long Region ran in minutes
                        Dim Expired As Double = TimerValue * ExitInterval / 60

                        ' if it is past time and no one is in the sim... Smart shutdown
                        If PropRegionClass.SmartStart(RegionUUID) = "True" _
                            And Settings.SmartStart _
                            And Expired >= 1 _
                            And Not AvatarsIsInGroup(GroupName) Then
                            Logger("State Changed to Suspended", RegionName, "Restart")
                            DoSuspend_Resume(RegionName)
                            Continue For
                        End If

                        ' auto restart timer
                        If Expired >= Settings.AutoRestartInterval() _
                            And Settings.AutoRestartInterval() > 0 _
                            And Not AvatarsIsInGroup(GroupName) Then

                            ' shut down the group when AutoRestartInterval has gone by.
                            Logger("State is Time Exceeded, shutdown", RegionName, "Restart")
                            ShowDOSWindow(GetHwnd(GroupName), SHOWWINDOWENUM.SWRESTORE)
                            SequentialPause()
                            ' shut down all regions in the DOS box
                            For Each UUID In GroupList
                                PropRegionClass.Timer(UUID) = RegionMaker.REGIONTIMER.Stopped
                                PropRegionClass.Status(UUID) = RegionMaker.SIMSTATUSENUM.RecyclingDown
                            Next
                            Logger("State changed to RecyclingDown", GroupName, "Restart")
                            ConsoleCommand(RegionUUID, "q{ENTER}" & vbCrLf)
                            Print(GroupName & " " & Global.Outworldz.My.Resources.Automatic_restart_word)
                            PropUpdateView = True ' make form refresh
                        End If
                    End If
                    Continue For

                ElseIf Status = RegionMaker.SIMSTATUSENUM.RecyclingUp Then

                    'RecyclingUp= 3
                    Logger("State is RecyclingUp", GroupName, "Restart")
                    Continue For

                ElseIf Status = RegionMaker.SIMSTATUSENUM.RecyclingDown Then

                    Logger("State is RecyclingDown", GroupName, "Restart")
                    Continue For

                ElseIf Status = RegionMaker.SIMSTATUSENUM.ShuttingDown Then

                    'ShuttingDown = 5
                    Logger("State is ShuttingDown", PropRegionClass.RegionName(RegionUUID), "Restart")
                    PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.Stopped
                    StopGroup(GroupName)
                    PropUpdateView = True
                    Logger("State changed to Stopped", PropRegionClass.RegionName(RegionUUID), "Restart")
                    Continue For

                    ' if a RestartPending is signaled, boot it up
                ElseIf Status = RegionMaker.SIMSTATUSENUM.RestartPending Then
                    Logger("State is RestartPending", GroupName, "Restart")
                    'RestartPending = 6
                    Boot(PropRegionClass, RegionName)
                    Logger("State is now Booted", PropRegionClass.RegionName(RegionUUID), "Restart")
                    PropUpdateView = True
                    Continue For

                ElseIf Status = RegionMaker.SIMSTATUSENUM.Resume Then

                    '[Resume] = 8
                    Logger("State is Resuming", GroupName, "Restart")
                    DoSuspend_Resume(PropRegionClass.RegionName(RegionUUID), True)
                    For Each R In GroupList
                        Logger("State changed to Booted", PropRegionClass.RegionName(R), "Restart")
                        PropRegionClass.Status(R) = RegionMaker.SIMSTATUSENUM.Booted
                        PropRegionClass.Timer(R) = RegionMaker.REGIONTIMER.StartCounting
                    Next

                    PropUpdateView = True
                    Continue For

                ElseIf Status = RegionMaker.SIMSTATUSENUM.Suspended Then

                    'Suspended = 9
                    Logger("State is Suspended", GroupName, "Restart")
                    Continue For

                ElseIf Status = RegionMaker.SIMSTATUSENUM.Error Then

                    'Error = 10
                    Logger("State is Error", GroupName, "Restart")
                    Continue For

                ElseIf Status = RegionMaker.SIMSTATUSENUM.RestartStage2 Then

                    'RestartStage2 = 11
                    Logger("State is Restart Pending", GroupName, "Restart")
                    Print(GroupName & " " & Global.Outworldz.My.Resources.Restart_Pending_word)
                    For Each R In GroupList
                        PropRegionClass.Status(R) = RegionMaker.SIMSTATUSENUM.RestartPending
                        PropRegionClass.Timer(R) = RegionMaker.REGIONTIMER.Stopped
                        Logger("State changed to RestartPending", PropRegionClass.RegionName(R), "Restart")
                    Next

                    PropUpdateView = True ' make form refresh
                Else
#Disable Warning CA1303
                    Logger("ExitHandlerPoll", "None of the above!", "Restart")
#Enable Warning CA1303
                End If
            End If
        Next

        ' now look at the exit stack

        While PropExitList.Count > 0

            GroupName = PropExitList.Keys.First
            Dim Reason = PropExitList.Item(GroupName)
            PropExitList.Remove(GroupName)

            Logger(Reason, GroupName & " Exited", "Restart")
            Print(GroupName & " " & Reason)

            ' Need a region number and a Name. Name is either a region or a Group. For groups we
            ' need to get a region name from the group
            Dim RegionUUID As String = ""
            Dim GroupList = PropRegionClass.RegionUUIDListByName(GroupName)
            If GroupList.Count > 0 Then
                RegionUUID = GroupList(0)
            Else
                Logger("No UUID", GroupName, "Restart")
            End If

            Dim Status As Integer = PropRegionClass.Status(RegionUUID)
            Logger(GetStateString(Status), GroupName, "Restart")

            'Stopped = 0
            'Booting = 1
            'Booted = 2
            'RecyclingUp = 3
            'RecyclingDown = 4
            'ShuttingDown = 5
            'RestartPending = 6
            'RetartingNow = 7
            '[Resume] = 8
            'Suspended = 9
            '[Error] = 10
            'RestartStage2 = 11

            If Status = RegionMaker.SIMSTATUSENUM.RecyclingDown And Not PropAborting Then
                'RecyclingDown = 4
                Logger("State is RecyclingDown", GroupName, "Restart")
                Print(GroupName & " " & Global.Outworldz.My.Resources.Restart_Queued_word)
                For Each R In GroupList
                    PropRegionClass.Status(R) = RegionMaker.SIMSTATUSENUM.RestartStage2
                    PropRegionClass.Timer(R) = RegionMaker.REGIONTIMER.Stopped
                Next
                Logger("State changed to RestartStage2", PropRegionClass.RegionName(RegionUUID), "Restart")

            ElseIf (Status = RegionMaker.SIMSTATUSENUM.RecyclingUp Or
                Status = RegionMaker.SIMSTATUSENUM.Booting Or
                Status = RegionMaker.SIMSTATUSENUM.Booted) And
                TimerValue >= 0 And
                Not PropAborting Then

                ' Maybe we crashed during warm up or running. Skip prompt if auto restart on crash
                ' and restart the beast

                Logger("Crash", GroupName & " Crashed", "Restart")
                If Settings.RestartOnCrash Then
                    ' shut down all regions in the DOS box
                    Print(GroupName & " " & Global.Outworldz.My.Resources.Quit_unexpectedly)
                    StopGroup(GroupName)
                    Print(GroupName & " " & Global.Outworldz.My.Resources.Restart_Queued_word)
                    For Each R In GroupList
                        PropRegionClass.Status(R) = RegionMaker.SIMSTATUSENUM.RestartStage2
                        PropRegionClass.Timer(R) = RegionMaker.REGIONTIMER.Stopped
                    Next
                Else
                    Print(GroupName & " " & Global.Outworldz.My.Resources.Quit_unexpectedly)
                    Dim yesno = MsgBox(GroupName & " " & Global.Outworldz.My.Resources.Quit_unexpectedly & " " & Global.Outworldz.My.Resources.See_Log, vbYesNo, Global.Outworldz.My.Resources.Error_word)
                    If (yesno = vbYes) Then
                        Try
                            System.Diagnostics.Process.Start(Settings.CurrentDirectory & "\baretail.exe", """" & PropRegionClass.IniPath(RegionUUID) & "Opensim.log" & """")
                        Catch ex As Exception

                            BreakPoint.Show(ex.Message)
                        End Try
                    End If
                    StopGroup(GroupName)

                End If

            End If
            PropUpdateView = True
        End While

        PropExitHandlerIsBusy = False

    End Sub

    Private Sub Fatal1_Click(sender As Object, e As EventArgs) Handles Fatal1.Click

        Settings.LogLevel = "FATAL"
        System.Environment.SetEnvironmentVariable("OSIM_LOGLEVEL", Settings.LogLevel.ToUpperInvariant)
        SendMsg(Settings.LogLevel)

    End Sub

#End Region

#Region "StartStop"

    Private Sub Form_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        cpu.Dispose()

    End Sub

    Private Sub Form1_Closed(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Closed
        ReallyQuit()
    End Sub

    ''' <summary>Form Load is main() for all DreamGrid</summary>
    ''' <param name="sender">Unused</param>
    ''' <param name="e">Unused</param>
    Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        Application.EnableVisualStyles()

        Dim _myFolder As String = My.Application.Info.DirectoryPath

        ' setup a debug path
        If Debugger.IsAttached Then
            ' for debugging when compiling
            _myFolder = _myFolder.Replace("\Installer_Src\Setup DreamWorld\bin\Debug", "")
            _myFolder = _myFolder.Replace("\Installer_Src\Setup DreamWorld\bin\Release", "")
            ' for testing, as the compiler buries itself in ../../../debug
        End If

        PropCurSlashDir = _myFolder.Replace("\", "/")    ' because MySQL uses Unix like slashes, that's why

        If Not System.IO.File.Exists(_myFolder & "\OutworldzFiles\Settings.ini") Then
            Create_ShortCut(_myFolder & "\Start.exe")
            PropViewedSettings = True
        End If

        'Load Settings, if any
        Settings.Init(_myFolder)

        Settings.CurrentDirectory = _myFolder
        Settings.OpensimBinPath() = _myFolder & "\OutworldzFiles\Opensim\bin\"
        Log("Startup:", DisplayObjectInfo(Me))

        My.Application.ChangeUICulture(Settings.Language)
        My.Application.ChangeCulture(Settings.Language)

        Me.Controls.Clear() 'removes all the controls on the form
        InitializeComponent() 'load all the controls again
        FrmHome_Load(sender, e) 'Load everything in your form load event again
        SetScreen()     ' move Form to fit screen from SetXY.ini

    End Sub

    <CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId:="System.Windows.Forms.Form.set_Text(System.String)")>
    Private Sub FrmHome_Load(ByVal sender As Object, ByVal e As EventArgs)

        TextBox1.BackColor = Me.BackColor
        ' initialize the scrolling text box
        TextBox1.SelectionStart = 0
        TextBox1.ScrollToCaret()
        TextBox1.SelectionStart = TextBox1.Text.Length
        TextBox1.ScrollToCaret()

        ' show box styled nicely.
        Application.EnableVisualStyles()
        Buttons(BusyButton)

        ToolBar(False)

        Adv1 = New AdvancedForm

        Me.Show()

        ' Save a random machine ID - we don't want any data to be sent that's personal or
        ' identifiable, but it needs to be unique
        Randomize()
        If Settings.MachineID().Length = 0 Then Settings.MachineID() = RandomNumber.Random  ' a random machine ID may be generated.  Happens only once

        ' WebUI
        ViewWebUI.Visible = Settings.WifiEnabled

        Me.Text += " V" & PropMyVersion

        PropOpensimIsRunning() = False ' true when opensim is running

        Print(My.Resources.Getting_regions_word)
        Application.DoEvents()
        PropRegionClass = RegionMaker.Instance()
        PropInitted = True

        ClearLogFiles() ' clear log files

        If Not IO.File.Exists(Settings.CurrentDirectory & "\BareTail.udm") Then
            IO.File.Copy(Settings.CurrentDirectory & "\BareTail.udm.bak", Settings.CurrentDirectory & "\BareTail.udm")
        End If

        GridNames.SetServerNames()

        CheckDefaultPorts()
        PropMyUPnpMap = New UPnp()
        Application.DoEvents()
        SetQuickEditOff()
        Application.DoEvents()
        SetLoopback()
        Application.DoEvents()
        LoadLocalIAROAR() ' refresh the pull downs.
        Application.DoEvents()
        'mnuShow shows the DOS box for Opensimulator
        Select Case Settings.ConsoleShow
            Case "True"
                mnuShow.Checked = True
                mnuHide.Checked = False
                mnuHideAllways.Checked = False
            Case "False"
                mnuShow.Checked = False
                mnuHide.Checked = True
                mnuHideAllways.Checked = False
            Case "None"
                mnuShow.Checked = False
                mnuHide.Checked = False
                mnuHideAllways.Checked = True
        End Select

        If SetIniData() Then
            MsgBox("Failed to setup")
            Buttons(StartButton)
            Print(My.Resources.Stopped_word)
            Return
        End If

        With Cpu1
            .CategoryName = "Processor"
            .CounterName = "% Processor Time"
            .InstanceName = "_Total"
        End With

        CheckForUpdates()
        Application.DoEvents()

        Print(My.Resources.Setup_Ports_word)
        Application.DoEvents()
        RegionMaker.UpdateAllRegionPorts() ' must be after SetIniData

        'must start after region Class Is instantiated
        PropWebServer = NetServer.GetWebServer

        Print(My.Resources.Starting_WebServer_word)
        Application.DoEvents()
        PropWebServer.StartServer(Settings.CurrentDirectory, Settings)

        CheckDiagPort()

        mnuSettings.Visible = True

        LoadHelp()        ' Help loads once

        KillOldFiles()  ' wipe out DLL's and other oddities

        Print(My.Resources.RefreshingOAR)
        Application.DoEvents()
        LoadLocalIAROAR() ' load IAR and OAR local content

        If Settings.Password = "secret" Then
            Dim Password = New PassGen
            Settings.Password = Password.GeneratePass()
        End If

        Print(My.Resources.Setup_Graphs_word)
        ' Graph fill

        Dim msChart = ChartWrapper1.TheChart
        msChart.ChartAreas(0).AxisX.Maximum = 180
        msChart.ChartAreas(0).AxisX.Minimum = 0
        msChart.ChartAreas(0).AxisY.Maximum = 100
        msChart.ChartAreas(0).AxisY.Minimum = 0
        msChart.ChartAreas(0).AxisY.LabelStyle.Enabled = True
        msChart.ChartAreas(0).AxisX.LabelStyle.Enabled = False
        ChartWrapper1.AddMarkers = True
        ChartWrapper1.MarkerFreq = 60

        msChart = ChartWrapper2.TheChart
        msChart.ChartAreas(0).AxisX.Maximum = 180
        msChart.ChartAreas(0).AxisX.Minimum = 0
        msChart.ChartAreas(0).AxisY.Maximum = 100
        msChart.ChartAreas(0).AxisY.Minimum = 0
        msChart.ChartAreas(0).AxisX.LabelStyle.Enabled = False
        msChart.ChartAreas(0).AxisY.LabelStyle.Enabled = True
        ChartWrapper2.AddMarkers = True
        ChartWrapper2.MarkerFreq = 60

        If Settings.RegionListVisible Then
            ShowRegionform()
        End If

        Print(My.Resources.Checking_MySql_word)
        Application.DoEvents()
        If MysqlInterface.IsMySqlRunning() Then PropStopMysql() = False

        ContentOAR = New FormOAR
        ContentOAR.Init("OAR")

        ContentIAR = New FormOAR
        ContentIAR.Init("IAR")

        Print(My.Resources.Version_word & " " & PropMyVersion)
        Print(My.Resources.Version_word & " " & _SimVersion)

        If Settings.Autostart Then
            Print(My.Resources.Auto_Startup_word)
            Application.DoEvents()
            Startup()
        Else
            Settings.SaveSettings()
            Print(My.Resources.Ready_to_Launch & vbCrLf & Global.Outworldz.My.Resources.Click_Start_2_Begin & vbCrLf)
            Application.DoEvents()
            Buttons(StartButton)
        End If

        HelpOnce("License") ' license on bottom
        HelpOnce("Startup")

    End Sub

#End Region

#Region "Language"

    Private Sub ArabicToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ArabicToolStripMenuItem.Click
        Settings.Language = "ar-IQ"
        Language(sender, e)
    End Sub

    Private Sub BasqueToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BasqueToolStripMenuItem.Click
        Settings.Language = "eu"
        Language(sender, e)
    End Sub

    Private Sub BrazilToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BrazilToolStripMenuItem.Click

        Settings.Language = "pt-BR"
        Language(sender, e)

    End Sub

    Private Sub CatalanToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CatalanToolStripMenuItem.Click
        Settings.Language = "ca-ES"
        Language(sender, e)
    End Sub

    Private Sub ChineseSimplifedToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ChineseSimplifedToolStripMenuItem.Click
        Settings.Language = "zh-CN"
        Language(sender, e)
    End Sub

    Private Sub ChineseTraditionalToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ChineseTraditionalToolStripMenuItem.Click
        Settings.Language = "zh-TW"
        Language(sender, e)
    End Sub

    Private Sub CzechToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CzechToolStripMenuItem.Click
        Settings.Language = "cs"
        Language(sender, e)
    End Sub

    Private Sub DutchToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DutchToolStripMenuItem.Click
        Settings.Language = "nl-NL"
        Language(sender, e)
    End Sub

    Private Sub EnglishToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EnglishToolStripMenuItem.Click
        Settings.Language = "en-US"
        Language(sender, e)
    End Sub

    Private Sub FarsiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FarsiToolStripMenuItem.Click
        Settings.Language = "fa-IR"
        Language(sender, e)
    End Sub

    Private Sub FinnishToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FinnishToolStripMenuItem.Click
        Settings.Language = "fi"
        Language(sender, e)
    End Sub

    Private Sub FrenchToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FrenchToolStripMenuItem.Click
        Settings.Language = "fr"
        Language(sender, e)
    End Sub

    Private Sub GermanToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GermanToolStripMenuItem.Click
        Settings.Language = "de"
        Language(sender, e)
    End Sub

    Private Sub GreekToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GreekToolStripMenuItem.Click
        Settings.Language = "el"
        Language(sender, e)
    End Sub

    Private Sub HebrewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HebrewToolStripMenuItem.Click
        Settings.Language = "he"
        Language(sender, e)
    End Sub

    Private Sub IcelandicToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles IcelandicToolStripMenuItem.Click
        Settings.Language = "is"
        Language(sender, e)
    End Sub

    Private Sub IrishToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles IrishToolStripMenuItem.Click
        Settings.Language = "ga"
        Language(sender, e)
    End Sub

    Private Sub Language(sender As Object, e As EventArgs)
        Settings.SaveSettings()

        For Each ci As CultureInfo In CultureInfo.GetCultures(CultureTypes.NeutralCultures)
            Diagnostics.Debug.Print("")
            Diagnostics.Debug.Print(ci.Name)
            Diagnostics.Debug.Print(ci.TwoLetterISOLanguageName)
            Diagnostics.Debug.Print(ci.ThreeLetterISOLanguageName)
            Diagnostics.Debug.Print(ci.ThreeLetterWindowsLanguageName)
            Diagnostics.Debug.Print(ci.DisplayName)
            Diagnostics.Debug.Print(ci.EnglishName)
        Next

        My.Application.ChangeUICulture(Settings.Language)
        My.Application.ChangeCulture(Settings.Language)
        Me.Controls.Clear() 'removes all the controls on the form
        InitializeComponent() 'load all the controls again
        FrmHome_Load(sender, e) 'Load everything in your form load event again
    End Sub

    Private Sub NorwegianToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NorwegianToolStripMenuItem.Click
        Settings.Language = "no"
        Language(sender, e)
    End Sub

    Private Sub PortgueseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PortgueseToolStripMenuItem.Click
        Settings.Language = "pt"
        Language(sender, e)
    End Sub

    Private Sub RussianToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RussianToolStripMenuItem.Click
        Settings.Language = "ru"
        Language(sender, e)
    End Sub

    Private Sub SpanishToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SpanishToolStripMenuItem.Click
        Settings.Language = "es-MX"
        Language(sender, e)
    End Sub

    Private Sub SwedishToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SwedishToolStripMenuItem.Click
        Settings.Language = "sv"
        Language(sender, e)
    End Sub

#End Region

#Region "Help"

    Private Sub HelpClick(sender As Object, e As EventArgs)

        If sender.Text.toupper(Globalization.CultureInfo.InvariantCulture) <> "DreamGrid Manual.pdf".ToUpper(Globalization.CultureInfo.InvariantCulture) Then HelpManual(sender.Text)

    End Sub

    Private Sub HelpOnIARSToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HelpOnIARSToolStripMenuItem.Click
        Dim webAddress As String = "http://opensimulator.org/wiki/Inventory_Archives"
        Try
            Process.Start(webAddress)
        Catch ex As Exception

            BreakPoint.Show(ex.Message)
        End Try
    End Sub

    Private Sub HelpOnOARsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HelpOnOARsToolStripMenuItem.Click
        Dim webAddress As String = "http://opensimulator.org/wiki/Load_Oar_0.9.0%2B"
        Try
            Process.Start(webAddress)
        Catch ex As Exception

            BreakPoint.Show(ex.Message)
        End Try
    End Sub

    Private Sub HelpStartingUpToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles HelpStartingUpToolStripMenuItem1.Click

        HelpManual("Startup")

    End Sub

    Private Sub HelpToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles HelpToolStripMenuItem1.Click

        HelpManual("Database")

    End Sub

    Private Sub HelpToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles HelpToolStripMenuItem2.Click

        HelpManual("ServerType")

    End Sub

    Private Sub HelpToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles HelpToolStripMenuItem3.Click

        HelpManual("Apache")

    End Sub

    Private Sub HelpToolStripMenuItem4_Click(sender As Object, e As EventArgs) Handles HelpToolStripMenuItem4.Click

        HelpManual("Icecast")

    End Sub

#End Region

#Region "IAR OAR"

    Private Sub IarClick(sender As ToolStripMenuItem)

        If sender.Text = "Web Download Link" Then
            Dim webAddress As String = PropDomain & "/outworldz_installer/IAR"
            Try
                Process.Start(webAddress)
            Catch ex As Exception

                BreakPoint.Show(ex.Message)
            End Try
            Return
        End If

        Dim file As String = Mid(sender.Text, 1, InStr(sender.Text, "|") - 2)
        file = PropDomain() & "/Outworldz_Installer/IAR/" & file 'make a real URL
        If LoadIARContent(file) Then
            Print(My.Resources.isLoading & " " & file)
        End If
        sender.Checked = True

    End Sub

    Private Sub LoadFreeDreamGridOARsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles IslandToolStripMenuItem.Click
        If PropInitted Then
            ContentOAR.Activate()
            ContentOAR.ShowForm()
            ContentOAR.Select()
            ContentOAR.BringToFront()
        End If
    End Sub

#End Region

#Region "Icecast"

    Private Sub IceCast_Exited(ByVal sender As Object, ByVal e As EventArgs) Handles IcecastProcess.Exited

        If PropAborting Then Return

        If Settings.RestartOnCrash And _IcecastCrashCounter < 10 Then
            _IcecastCrashCounter += 1
            PropIceCastExited = True
            Return
        End If
        _IcecastCrashCounter = 0

        Dim yesno = MsgBox(My.Resources.Icecast_Exited, vbYesNo, Global.Outworldz.My.Resources.Error_word)

        If (yesno = vbYes) Then
            Dim IceCastLog As String = Settings.CurrentDirectory & "\Outworldzfiles\Icecast\log\error.log"
            Try
                System.Diagnostics.Process.Start(Settings.CurrentDirectory & "\baretail.exe", """" & IceCastLog & """")
            Catch ex As Exception

                BreakPoint.Show(ex.Message)
            End Try
        End If

    End Sub

    Private Sub IceCastIs(Running As Boolean)

        If Not Running Then
            RestartIcecastItem.Image = Global.Outworldz.My.Resources.nav_plain_red
        Else
            RestartIcecastItem.Image = Global.Outworldz.My.Resources.check2
        End If
        Application.DoEvents()

    End Sub

    Private Sub IceCastPicturebox_Click(sender As Object, e As EventArgs)

        If Not CheckIcecast() Then
            StartIcecast()
        Else
            StopIcecast()
        End If

    End Sub

#End Region

#Region "Exit"

    Private Sub KillOldFiles()

        Dim files As New List(Of String) From {
        "\Shoutcast", ' deprecated
        "\Icecast",   ' moved to Outworldzfiles
        "\Outworldzfiles\Opensim\bin\addins",' moved to Outworldzfiles
        "\Outworldzfiles\Opensim\bin\addin-db-002" ' must be cleared or opensim updates can break.
        }

        If PropKillSource Then
            files.Add("Outworldzfiles\Opensim\.nant")

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

        ' crap load of old DLLS have to be eliminated
        CleanDLLs() ' drop old opensim Dll's

        If PropKillSource Then
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

    Private Sub LoadHelp()

        ' read help files for menu

        Dim folders As Array = Nothing
        Try
            folders = Directory.GetFiles(Settings.CurrentDirectory & "\Outworldzfiles\Help")
        Catch ex As Exception

            BreakPoint.Show(ex.Message)
        End Try

        For Each aline As String In folders
            If aline.EndsWith(".rtf", StringComparison.InvariantCultureIgnoreCase) Then
                aline = System.IO.Path.GetFileNameWithoutExtension(aline)
                Dim HelpMenu As New ToolStripMenuItem With {
                    .Text = aline,
                    .ToolTipText = Global.Outworldz.My.Resources.Click_to_load,
                    .DisplayStyle = ToolStripItemDisplayStyle.Text,
                    .Image = Global.Outworldz.My.Resources.question_and_answer
                }
                AddHandler HelpMenu.Click, New EventHandler(AddressOf HelpClick)
                HelpOnSettingsToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {HelpMenu})
            End If
        Next

        AddLog("All Logs")
        AddLog("Robust")
        AddLog("Error")
        AddLog("Outworldz")
        AddLog("Icecast")
        AddLog("MySQL")
        AddLog("All Settings")
        AddLog("--- Regions ---")
        For Each RegionUUID As String In PropRegionClass.RegionUUIDs
            Dim Name = PropRegionClass.RegionName(RegionUUID)
            AddLog("Region " & Name)
        Next

    End Sub

    <CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId:="System.Windows.Forms.FileDialog.set_Filter(System.String)")>
    Private Sub LoadInventoryIARToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoadInventoryIARToolStripMenuItem.Click

        If PropOpensimIsRunning() Then
            ' Create an instance of the open file dialog box. Set filter options and filter index.
            Dim openFileDialog1 As OpenFileDialog = New OpenFileDialog With {
                .InitialDirectory = """" & Settings.CurrentDirectory & "/" & """",
                .Filter = Global.Outworldz.My.Resources.IAR_Load_and_Save_word & " (*.iar)|*.iar|All Files (*.*)|*.*",
                .FilterIndex = 1,
                .Multiselect = False
            }

            ' Call the ShowDialog method to show the dialog box.
            Dim UserClickedOK As DialogResult = openFileDialog1.ShowDialog

            ' Process input if the user clicked OK.
            If UserClickedOK = DialogResult.OK Then
                Dim thing = openFileDialog1.FileName
                If thing.Length > 0 Then
                    thing = thing.Replace("\", "/")    ' because Opensim uses Unix-like slashes, that's why
                    If LoadIARContent(thing) Then
                        Print(My.Resources.isLoading & " " & thing)
                    End If
                End If
            End If
            openFileDialog1.Dispose()
        Else
            Print(My.Resources.Not_Running)
        End If

    End Sub

    Private Sub LoadLocalIAROAR()
        ''' <summary>Loads OAR and IAR from the menu</summary>
        ''' <remarks>Handles both the IAR/OAR and Autobackup folders</remarks>

        LoadLocalOARSToolStripMenuItem.DropDownItems.Clear()
        Dim MaxFileNum As Integer = 10
        Dim counter = MaxFileNum
        Dim Filename = Settings.CurrentDirectory & "\OutworldzFiles\OAR\"
        Dim OARs As Array = Nothing
        Try
            OARs = Directory.GetFiles(Filename, "*.OAR", SearchOption.TopDirectoryOnly)
        Catch ex As Exception

            BreakPoint.Show(ex.Message)
        End Try

        For Each OAR As String In OARs
            counter -= 1
            If counter > 0 Then
                Dim Name = Path.GetFileName(OAR)
                Dim OarMenu As New ToolStripMenuItem With {
                    .Text = Name,
                    .ToolTipText = Global.Outworldz.My.Resources.Click_to_load,
                    .DisplayStyle = ToolStripItemDisplayStyle.Text
                }
                AddHandler OarMenu.Click, New EventHandler(AddressOf LocalOarClick)
                LoadLocalOARSToolStripMenuItem.Visible = True
                LoadLocalOARSToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {OarMenu})
            End If

        Next

        If Settings.BackupFolder = "AutoBackup" Then
            Filename = Settings.CurrentDirectory & "\OutworldzFiles\AutoBackup\"
        Else
            Filename = Settings.BackupFolder
        End If

        Dim AutoOARs As Array = Nothing
        Try
            AutoOARs = Directory.GetFiles(Filename, "*.OAR", SearchOption.TopDirectoryOnly)
        Catch ex As Exception

            BreakPoint.Show(ex.Message)
        End Try
        counter = MaxFileNum

        If AutoOARs IsNot Nothing Then
            For Each OAR As String In AutoOARs
                counter -= 1
                If counter > 0 Then
                    Dim Name = Path.GetFileName(OAR)
                    Dim OarMenu As New ToolStripMenuItem With {
                        .Text = Name,
                        .ToolTipText = Global.Outworldz.My.Resources.Click_to_load,
                        .DisplayStyle = ToolStripItemDisplayStyle.Text
                    }
                    AddHandler OarMenu.Click, New EventHandler(AddressOf BackupOarClick)
                    LoadLocalOARSToolStripMenuItem.Visible = True
                    LoadLocalOARSToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {OarMenu})
                End If
            Next
        End If

        ' now for the IARs

        Filename = Settings.CurrentDirectory & "\OutworldzFiles\IAR\"
        Dim IARs As Array = Nothing

        Try
            IARs = Directory.GetFiles(Filename, "*.IAR", SearchOption.TopDirectoryOnly)
        Catch ex As Exception

            BreakPoint.Show(ex.Message)
        End Try

        LoadLocalIARsToolStripMenuItem.DropDownItems.Clear()

        counter = MaxFileNum
        For Each IAR As String In IARs
            counter -= 1
            If counter > 0 Then
                Dim Name = Path.GetFileName(IAR)
                Dim IarMenu As New ToolStripMenuItem With {
                    .Text = Name,
                    .ToolTipText = Global.Outworldz.My.Resources.Click_to_load,
                    .DisplayStyle = ToolStripItemDisplayStyle.Text
                }
                AddHandler IarMenu.Click, New EventHandler(AddressOf LocalIarClick)
                LoadLocalIARsToolStripMenuItem.Visible = True
                LoadLocalIARsToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {IarMenu})

            End If

        Next

        If Settings.BackupFolder = "AutoBackup" Then
            Filename = Settings.CurrentDirectory & "\OutworldzFiles\AutoBackup\"
        Else
            Filename = Settings.BackupFolder
        End If

        Dim AutoIARs As Array = Nothing
        Try
            AutoIARs = Directory.GetFiles(Filename, "*.IAR", SearchOption.TopDirectoryOnly)
        Catch ex As Exception

            BreakPoint.Show(ex.Message)
        End Try
        If AutoIARs IsNot Nothing Then
            counter = MaxFileNum
            For Each IAR As String In AutoIARs
                counter -= 1
                If counter > 0 Then
                    Dim Name = Path.GetFileName(IAR)
                    Dim IarMenu As New ToolStripMenuItem With {
                    .Text = Name,
                    .ToolTipText = Global.Outworldz.My.Resources.Click_to_load,
                    .DisplayStyle = ToolStripItemDisplayStyle.Text
                }
                    AddHandler IarMenu.Click, New EventHandler(AddressOf BackupIarClick)
                    LoadLocalIARsToolStripMenuItem.Visible = True
                    LoadLocalIARsToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {IarMenu})
                End If

            Next
        End If

    End Sub

    <CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId:="System.Windows.Forms.FileDialog.set_Filter(System.String)")>
    Private Sub LoadRegionOarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoadRegionOarToolStripMenuItem.Click

        If PropOpensimIsRunning() Then
            Dim chosen = ChooseRegion(True)
            If chosen.Length = 0 Then Return
            Dim RegionUUID As String = PropRegionClass.FindRegionByName(chosen)

            ' Create an instance of the open file dialog box. Set filter options and filter index.
            Using openFileDialog1 As OpenFileDialog = New OpenFileDialog With {
                .InitialDirectory = BackupPath(),
                .Filter = Global.Outworldz.My.Resources.OAR_Load_and_Save & "(*.OAR,*.GZ,*.TGZ)|*.oar;*.gz;*.tgz;*.OAR;*.GZ;*.TGZ|All Files (*.*)|*.*",
                .FilterIndex = 1,
                .Multiselect = False
                }

                ' Call the ShowDialog method to show the dialog box.
                Dim UserClickedOK As DialogResult = openFileDialog1.ShowDialog

                ' Process input if the user clicked OK.
                If UserClickedOK = DialogResult.OK Then

                    Dim offset = VarChooser(chosen)

                    Dim backMeUp = MsgBox(My.Resources.Make_a_backup_word, vbYesNo, Global.Outworldz.My.Resources.Backup_word)
                    Dim thing = openFileDialog1.FileName
                    If thing.Length > 0 Then
                        thing = thing.Replace("\", "/")    ' because Opensim uses UNIX-like slashes, that's why

                        Dim Group = PropRegionClass.GroupName(RegionUUID)
                        For Each UUID In PropRegionClass.RegionUUIDListByName(Group)

                            ConsoleCommand(UUID, "change region " & chosen & "{ENTER}" & vbCrLf)
                            If backMeUp = vbYes Then
                                ConsoleCommand(UUID, "alert " & Global.Outworldz.My.Resources.CPU_Intensive & "{Enter}" & vbCrLf)
                                ConsoleCommand(UUID, "save oar  " & """" & BackupPath() & chosen & "_" & DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss", Globalization.CultureInfo.InvariantCulture) & ".oar" & """" & "{ENTER}" & vbCrLf)
                            End If
                            ConsoleCommand(UUID, "alert " & Global.Outworldz.My.Resources.New_Content & "{ENTER}" & vbCrLf)

                            Dim ForceParcel As String = ""
                            If PropForceParcel() Then ForceParcel = " --force-parcels "
                            Dim ForceTerrain As String = ""
                            If PropForceTerrain Then ForceTerrain = " --force-terrain "
                            Dim ForceMerge As String = ""
                            If PropForceMerge Then ForceMerge = " --merge "
                            Dim UserName As String = ""
                            If PropUserName.Length > 0 Then UserName = " --default-user " & """" & PropUserName & """" & " "

                            ConsoleCommand(UUID, "load oar " & UserName & ForceMerge & ForceTerrain & ForceParcel & offset & """" & thing & """" & "{ENTER}" & vbCrLf)
                            ConsoleCommand(UUID, "alert " & Global.Outworldz.My.Resources.New_is_Done & "{ENTER}" & vbCrLf)
                            ConsoleCommand(UUID, "generate map {ENTER}" & vbCrLf)
                        Next
                    End If
                End If

            End Using
        Else
            Print(My.Resources.Not_Running)
        End If

    End Sub

    Private Sub LocalIarClick(sender As Object, e As EventArgs)

        Dim File As String = Settings.CurrentDirectory & "/OutworldzFiles/IAR/" & sender.Text 'make a real URL
        If LoadIARContent(File) Then
            Print(My.Resources.Opensimulator_is_loading & sender.Text)
        End If

    End Sub

    Private Sub LocalOarClick(sender As Object, e As EventArgs)

        Dim File = Settings.CurrentDirectory & "/OutworldzFiles/OAR/" & sender.Text 'make a real URL
        If LoadOARContent(File) Then
            Print(My.Resources.Opensimulator_is_loading & sender.Text)
        End If

    End Sub

    Private Sub LogViewClick(sender As Object, e As EventArgs)

        Viewlog(sender.Text)

    End Sub

    Private Sub LoopBackToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoopBackToolStripMenuItem.Click

        HelpManual("Loopback Fixes")

    End Sub

    Private Sub MakeMysql()

        Dim m As String = Settings.CurrentDirectory & "\OutworldzFiles\Mysql\"
        If Not System.IO.File.Exists(m & "\Data\ibdata1") Then
            Print(My.Resources.Create_DB)
            Using zip As ZipFile = ZipFile.Read(m & "\Blank-Mysql-Data-folder.zip")
                For Each ZipEntry In zip
                    ZipEntry.Extract(m, Ionic.Zip.ExtractExistingFileAction.OverwriteSilently)
                Next
            End Using
        End If

    End Sub

    Private Sub MnuAbout_Click(sender As System.Object, e As EventArgs) Handles mnuAbout.Click

        Print("(c) 2017 Outworldz,LLC" & vbCrLf & "Version " & PropMyVersion)
        Dim webAddress As String = PropDomain & "/Outworldz_Installer"
        Try
            Process.Start(webAddress)
        Catch ex As Exception

            BreakPoint.Show(ex.Message)
        End Try

    End Sub

    Private Sub MnuExit_Click(sender As System.Object, e As EventArgs) Handles mnuExit.Click
        ReallyQuit()
    End Sub

    Private Sub MnuHide_Click(sender As System.Object, e As EventArgs) Handles mnuHide.Click

        Print(My.Resources.Not_Shown)
        mnuShow.Checked = False
        mnuHide.Checked = True
        mnuHideAllways.Checked = False

        Settings.ConsoleShow = "False"
        Settings.SaveSettings()

    End Sub

    Private Sub MnuHideAllways_Click(sender As Object, e As EventArgs) Handles mnuHideAllways.Click
        Print(My.Resources.Not_Shown)
        mnuShow.Checked = False
        mnuHide.Checked = False
        mnuHideAllways.Checked = True

        Settings.ConsoleShow = "None"
        Settings.SaveSettings()

    End Sub

    Private Sub MoreContentToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MoreContentToolStripMenuItem.Click

        Dim webAddress As String = PropDomain & "/cgi/freesculpts.plx"
        Try
            Process.Start(webAddress)
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try

    End Sub

    Private Sub Mysql_Exited(ByVal sender As Object, ByVal e As EventArgs) Handles ProcessMySql.Exited

        If PropAborting Then Return

        If Settings.RestartOnCrash And _MysqlCrashCounter < 10 Then
            _MysqlCrashCounter += 1
            PropMysqlExited = True
            Return
        End If
        _MysqlCrashCounter = 0
        Dim MysqlLog As String = Settings.CurrentDirectory & "\OutworldzFiles\mysql\data"
        Dim files As Array = Nothing
        Try
            files = Directory.GetFiles(MysqlLog, "*.err", SearchOption.TopDirectoryOnly)
        Catch ex As Exception

            BreakPoint.Show(ex.Message)
        End Try

        If files.Length > 0 Then
            Dim yesno = MsgBox(My.Resources.MySql_Exited, vbYesNo, Global.Outworldz.My.Resources.Error_word)
            If (yesno = vbYes) Then

                For Each FileName As String In files
                    Try
                        System.Diagnostics.Process.Start(Settings.CurrentDirectory & "\baretail.exe", """" & FileName & """")
                    Catch ex As Exception

                        BreakPoint.Show(ex.Message)
                    End Try

                Next
            End If
        Else
            PropAborting = True
            MsgBox(My.Resources.Error_word, vbInformation, Global.Outworldz.My.Resources.Error_word)
        End If

    End Sub

    Private Sub MySqlIs(Running As Boolean)

        If Not Running Then
            MysqlToolStripMenuItem.Image = Global.Outworldz.My.Resources.nav_plain_red
        Else
            MysqlToolStripMenuItem.Image = Global.Outworldz.My.Resources.check2
        End If
        Application.DoEvents()

    End Sub

    Private Sub MysqlPictureBox_Click(sender As Object, e As EventArgs)

        If MysqlInterface.IsMySqlRunning() Then
            PropStopMysql = True
            StopMysql()
        Else
            StartMySQL()
        End If

    End Sub

    Private Sub NewDNSName()

        If Settings.DNSName.Length = 0 And Settings.EnableHypergrid Then
            Dim newname = GetNewDnsName()
            If newname.Length >= 0 Then
                If RegisterName(newname, True) Then
                    Settings.DNSName = newname
                    Settings.PublicIP = newname
                    Settings.SaveSettings()
                    MsgBox(My.Resources.NameAlreadySet, vbInformation, Global.Outworldz.My.Resources.Information_word)
                End If
            End If

        End If

    End Sub

    Private Sub Off1_Click(sender As Object, e As EventArgs) Handles Off1.Click

        Settings.LogLevel = "OFF"
        System.Environment.SetEnvironmentVariable("OSIM_LOGLEVEL", Settings.LogLevel.ToUpperInvariant)
        SendMsg(Settings.LogLevel)

    End Sub

    <CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId:="Outworldz.Form1.Logger(System.String,System.String,System.String)")>
    Private Function OpenPorts() As Boolean

        If OpenRouterPorts() Then ' open UPnp port

            Logger("INFO",
                   "UPNP OK",
                   "Diagnostics")

            Settings.UPnpDiag = True
            Settings.SaveSettings()
            Return True
        Else

            Print(My.Resources.UPNP_Disabled)
            Settings.UPnpDiag = False
            Settings.SaveSettings()
            Return False
        End If

    End Function

    Private Sub PDFManualToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PDFManualToolStripMenuItem.Click
        Dim webAddress As String = Settings.CurrentDirectory & "\Outworldzfiles\Help\Dreamgrid Manual.pdf"
        Try
            Process.Start(webAddress)
        Catch ex As Exception

            BreakPoint.Show(ex.Message)
        End Try
    End Sub

    <CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId:="Outworldz.Form1.ErrorLog(System.String)")>
    <CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId:="Outworldz.Form1.Logger(System.String,System.String,System.String)")>
    Private Sub PortTest(Weblink As String, Port As Integer)

        Dim result As String = ""
        Using client As New WebClient
            Try
                result = client.DownloadString(Weblink)
            Catch ex As WebException  ' not an error as could be a 404 from Diva being off
            Catch ex As Exception

                BreakPoint.Show(ex.Message)
                ErrorLog("Err:Loopback fail:" & result & ":" & ex.Message)
                Logger("Error", "Loopback fail: " & result & ":" & ex.Message, "Diagnostics")
            End Try
        End Using

        If result.Contains("DOCTYPE") Or result.Contains("Ooops!") Or result.Length = 0 Then
            Logger("INFO", Global.Outworldz.My.Resources.Loopback_Passed & " " & Port.ToString(Globalization.CultureInfo.InvariantCulture), "Diagnostics")
            Print(My.Resources.Loopback_Passed & " " & Port.ToString(Globalization.CultureInfo.InvariantCulture))
        Else
            Print(My.Resources.Loopback_Failed & " " & Weblink)
            Logger("INFO", Global.Outworldz.My.Resources.Loopback_Failed & " " & Weblink, "Diagnostics")
            Settings.LoopBackDiag = False
            Settings.DiagFailed = True
        End If

    End Sub

    <CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId:="Outworldz.Form1.ErrorLog(System.String)")>
    <CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId:="Outworldz.Form1.Logger(System.String,System.String,System.String)")>
    Private Sub ProbePublicPort()

        If Settings.ServerType <> "Robust" Then
#Disable Warning ca1303
            Logger("INFO", "Server Is Not Robust", "Diagnostics")
#Enable Warning ca1303

            Return
        End If

        Dim isPortOpen As String = ""
        Using client As New WebClient ' download client for web pages

            ' collect some stats and test loopback with a HTTP_ GET to the webserver. Send unique,
            ' anonymous random ID, both of the versions of Opensim and this program, and the
            ' diagnostics test results See my privacy policy at https://outworldz.com/privacy.htm

            Print(My.Resources.Checking_Router_word)
            Dim Url = PropDomain() & "/cgi/probetest.plx?IP=" & Settings.PublicIP & "&Port=" & Settings.HttpPort & GetPostData()
            Logger("INFO", "Using URL " & Url, "Diagnostics")
            Try
                isPortOpen = client.DownloadString(Url)
            Catch ex As WebException  ' not an error as could be a 404 from Diva being off
                BreakPoint.Show(ex.Message)
                Logger("Error", Global.Outworldz.My.Resources.Wrong & " " & ex.Message, "Diagnostics")
                ErrorLog(My.Resources.Wrong & " " & ex.Message)
            End Try
        End Using

        If isPortOpen = "yes" Then
            Logger("INFO", Global.Outworldz.My.Resources.Incoming_Works, "Diagnostics")
            Print(My.Resources.Incoming_Works)
        Else
            Settings.LoopBackDiag = False
            Settings.DiagFailed = True
            Logger("INFO", Global.Outworldz.My.Resources.Internet_address & " " & Settings.PublicIP & ":" & Settings.HttpPort & Global.Outworldz.My.Resources.Not_Forwarded, "Diagnostics")
            Print(My.Resources.Internet_address & " " & Settings.PublicIP & ":" & Settings.HttpPort & Global.Outworldz.My.Resources.Not_Forwarded)
        End If

    End Sub

    Private Sub ReallyQuit()

        If Not KillAll() Then Return

        cpu.Dispose()

        If PropWebServer IsNot Nothing Then
            PropWebServer.StopWebServer()
        End If

        PropAborting = True
        StopMysql()

        Print("Zzzz...")
        Thread.Sleep(2000)
        End

    End Sub

    '' makes a list of teleports for the prims to use
    Private Sub RegionListHTML()

        'http://localhost:8002/bin/data/teleports.htm
        'Outworldz|Welcome||outworldz.com:9000:Welcome|128,128,96|
        '*|Welcome||outworldz.com9000Welcome|128,128,96|
        Dim HTML As String
        Dim HTMLFILE = Settings.OpensimBinPath & "data\teleports.htm"
        HTML = "Welcome to |" & Settings.SimName & "||" & Settings.PublicIP & ":" & Settings.HttpPort & ":" & Settings.WelcomeRegion & "||" & vbCrLf
        Dim ToSort As New List(Of String)

        For Each RegionUUID As String In PropRegionClass.RegionUUIDs
            If RegionUUID.Length > 0 Then
                If PropRegionClass.Teleport(RegionUUID) = "True" And
                    PropRegionClass.RegionEnabled(RegionUUID) = True And
                    PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.Booted Then
                    ToSort.Add(PropRegionClass.RegionName(RegionUUID))
                End If
            End If

        Next

        ' Acquire keys And sort them.
        ToSort.Sort()

        For Each S As String In ToSort
            HTML = HTML & "*|" & S & "||" & Settings.PublicIP & ":" & Settings.HttpPort & ":" & S & "||" & vbCrLf
        Next

        FileStuff.DeleteFile(HTMLFILE)

        Try
            Using outputFile As New StreamWriter(HTMLFILE, True)
                outputFile.WriteLine(HTML)
            End Using
        Catch ex As Exception

            BreakPoint.Show(ex.Message)
        End Try

    End Sub

    Private Sub RegionsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RegionsToolStripMenuItem.Click

        ShowRegionform()

    End Sub

    Private Sub Resize_page(ByVal sender As Object, ByVal e As EventArgs)
        ScreenPosition1.SaveXY(Me.Left, Me.Top)
        ScreenPosition1.SaveHW(Me.Height, Me.Width)
    End Sub

    Private Sub RestartDOSboxes()

        If PropRestartRobust And PropRobustExited = True Then
            PropRobustExited = False
            RobustIs(False)
            If Not IsRobustRunning() Then
                StartRobust()
            End If
        End If

        If PropMysqlExited Then
            MySqlIs(False)
            StartMySQL()
        End If

        If PropApacheExited Then
            MySqlIs(False)
            StartApache()
        End If

        If PropIceCastExited Then
            IceCastIs(False)
            StartIcecast()
        End If

    End Sub

    Private Sub RestartOneRegionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RestartOneRegionToolStripMenuItem.Click

        If Not PropOpensimIsRunning() Then
            Print(My.Resources.Not_Running)
            Return
        End If
        Dim name = ChooseRegion(True)
        Dim RegionUUID As String = PropRegionClass.FindRegionByName(name)
        If RegionUUID.Length > 0 Then
            ConsoleCommand(RegionUUID, "change region " & name & "{ENTER}" & vbCrLf)
            ConsoleCommand(RegionUUID, "restart region " & name & "{ENTER}" & vbCrLf)
            PropUpdateView = True ' make form refresh
        End If

    End Sub

    Private Sub RestartTheInstanceToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RestartTheInstanceToolStripMenuItem.Click

        If Not PropOpensimIsRunning() Then
            Print(My.Resources.Not_Running)
            Return
        End If
        Dim name = ChooseRegion(True)
        Dim RegionUUID As String = PropRegionClass.FindRegionByName(name)
        If RegionUUID.Length > 0 Then
            ConsoleCommand(RegionUUID, "restart{ENTER}" & vbCrLf)
            PropUpdateView = True ' make form refresh
        End If

    End Sub

    Private Sub RestartToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RestartMysqlItem.Click

        PropAborting = True
        PropStopMysql = True
        StopMysql()
        StartMySQL()
        PropAborting = False

    End Sub

    Private Sub RestartToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles RestartRobustItem.Click

        PropAborting = True
        StopRobust()
        StartRobust()
        PropAborting = False

    End Sub

    Private Sub RestartToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles RestartToolStripMenuItem2.Click

        If Not Settings.ApacheEnable Then
            ApacheIs(False)
            Print(My.Resources.Apache_Disabled)
        End If

        StartApache()
        PropAborting = False

    End Sub

    Private Sub RestartToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles RestartIceCastItem2.Click

        If Not Settings.SCEnable Then
            Settings.SCEnable = True
        End If

        PropAborting = True
        StopIcecast()
        StartIcecast()
        PropAborting = False

    End Sub

    <CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId:="Outworldz.Form1.ErrorLog(System.String)")>
    <CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId:="System.Windows.Forms.FileDialog.set_Filter(System.String)")>
    Private Sub RestoreDatabaseToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles RestoreDatabaseToolStripMenuItem1.Click

        If PropOpensimIsRunning() Then
            Print(My.Resources.Not_Running)
            Return
        End If

        If Not StartMySQL() Then

            ToolBar(False)
            Buttons(StartButton)
            Print(My.Resources.Stopped_word)
            Return
        End If

        ' Create an instance of the open file dialog box. Set filter options and filter index.
        Dim openFileDialog1 As OpenFileDialog = New OpenFileDialog With {
            .InitialDirectory = BackupPath(),
            .Filter = Global.Outworldz.My.Resources.Backup_Folder & "(*.sql)|*.sql|All Files (*.*)|*.*",
            .FilterIndex = 1,
            .Multiselect = False
        }

        ' Call the ShowDialog method to show the dialogbox.
        Dim UserClickedOK As DialogResult = openFileDialog1.ShowDialog
        openFileDialog1.Dispose()
        ' Process input if the user clicked OK.
        If UserClickedOK = DialogResult.OK Then
            Dim thing = openFileDialog1.FileName
            If thing.Length > 0 Then

                Dim yesno = MsgBox(My.Resources.Are_You_Sure, vbYesNo, Global.Outworldz.My.Resources.Restore_word)
                If yesno = vbYes Then

                    FileStuff.DeleteFile(Settings.CurrentDirectory & "\OutworldzFiles\mysql\bin\RestoreMysql.bat")

                    Try
                        Dim filename As String = Settings.CurrentDirectory & "\OutworldzFiles\mysql\bin\RestoreMysql.bat"
                        Using outputFile As New StreamWriter(filename, True)
                            outputFile.WriteLine("@REM A program to restore Mysql from a backup" & vbCrLf _
                                & "mysql -u root opensim <  " & """" & thing & """" _
                                & vbCrLf & "@pause" & vbCrLf)
                        End Using
                    Catch ex As Exception

                        BreakPoint.Show(ex.Message)
                        ErrorLog("Failed to create restore file:" & ex.Message)
                        Return
                    End Try

                    Print(My.Resources.Do_Not_Interrupt_word)
                    Dim pMySqlRestore As Process = New Process()
                    ' pi.Arguments = thing
                    Dim pi As ProcessStartInfo = New ProcessStartInfo With {
                        .WindowStyle = ProcessWindowStyle.Normal,
                        .WorkingDirectory = Settings.CurrentDirectory & "\OutworldzFiles\mysql\bin\",
                        .FileName = Settings.CurrentDirectory & "\OutworldzFiles\mysql\bin\RestoreMysql.bat"
                    }
                    pMySqlRestore.StartInfo = pi

                    Try
                        pMySqlRestore.Start()
                        pMySqlRestore.WaitForExit()
                    Catch ex As Exception

                        BreakPoint.Show(ex.Message)
                    Finally
                        pMySqlRestore.Dispose()
                    End Try

                    Print(My.Resources.Do_Not_Interrupt_word)
                End If
            Else
                Print(My.Resources.Cancelled_word)
            End If
        End If
    End Sub

    Private Sub RevisionHistoryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RevisionHistoryToolStripMenuItem.Click

        HelpManual("Revisions")

    End Sub

    Private Sub RobustPictureBox_Click(sender As Object, e As EventArgs)

        If Not IsRobustRunning() Then
            StartRobust()
        Else
            StopRobust()
        End If

    End Sub

    Private Sub RobustProcess_Exited(ByVal sender As Object, ByVal e As EventArgs) Handles RobustProcess.Exited
        ' Handle Exited event and display process information.
        PropRobustProcID = Nothing
        If PropAborting Then Return

        If PropRestartRobust Then
            PropRobustExited = True
            Return
        End If

        If Settings.RestartOnCrash And _RobustCrashCounter < 10 Then
            PropRobustExited = True
            _RobustCrashCounter += 1
            Return
        End If
        _RobustCrashCounter = 0
        RobustIs(False)

        Dim yesno = MsgBox(My.Resources.Robust_exited, vbYesNo, Global.Outworldz.My.Resources.Error_word)
        If (yesno = vbYes) Then
            Dim MysqlLog As String = Settings.OpensimBinPath & "Robust.log"
            Try
                System.Diagnostics.Process.Start(Settings.CurrentDirectory & "\baretail.exe", """" & MysqlLog & """")
            Catch ex As Exception

                BreakPoint.Show(ex.Message)
            End Try
        End If

    End Sub

    Private Sub SaveInventoryIARToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveInventoryIARToolStripMenuItem.Click

        If PropOpensimIsRunning() Then

            Using SaveIAR As New FormIARSave
                SaveIAR.ShowDialog()
                Dim chosen = SaveIAR.DialogResult()
                If chosen = DialogResult.OK Then

                    Dim itemName = SaveIAR.GObject
                    If itemName.Length = 0 Then
                        MsgBox(My.Resources.MustHaveName)
                        Return
                    End If

                    Dim ToBackup As String

                    Dim BackupName = SaveIAR.GBackupName

                    If Not BackupName.EndsWith(".iar", StringComparison.InvariantCultureIgnoreCase) Then
                        BackupName += ".iar"
                    End If

                    If String.IsNullOrEmpty(SaveIAR.GBackupPath) Or SaveIAR.GBackupPath = "AutoBackup" Then
                        ToBackup = BackupPath() & "" & BackupName
                    Else
                        ToBackup = BackupName
                    End If

                    Dim Name = SaveIAR.GAvatarName
                    Dim Password = SaveIAR.GPassword

                    For Each RegionUUID As String In PropRegionClass.RegionUUIDs
                        If PropRegionClass.IsBooted(RegionUUID) Then
                            ConsoleCommand(RegionUUID, "save iar " _
                                       & Name & " " _
                                       & """" & itemName & """" _
                                       & " " & """" & Password & """" & " " _
                                       & """" & ToBackup & """" _
                                       & "{ENTER}" & vbCrLf
                                      )
                            Exit For
                            Print(My.Resources.Saving_word & " " & BackupPath() & "\" & BackupName)

                        End If
                    Next
                End If
            End Using
        Else
            Print(My.Resources.Not_Running)
        End If

    End Sub

    Private Sub SaveRegionOARToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveRegionOARToolStripMenuItem.Click

        If PropOpensimIsRunning() Then

            Dim chosen = ChooseRegion(True)
            If chosen.Length = 0 Then Return
            Dim RegionUUID As String = PropRegionClass.FindRegionByName(chosen)

            Dim Message, title, defaultValue As String
            Dim myValue As String
            ' Set prompt.
            Message = Global.Outworldz.My.Resources.EnterName
            title = "Backup to OAR"
            defaultValue = chosen & "_" & DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss", Globalization.CultureInfo.InvariantCulture) & ".oar"

            ' Display message, title, and default value.
            myValue = InputBox(Message, title, defaultValue)
            ' If user has clicked Cancel, set myValue to defaultValue
            If myValue.Length = 0 Then Return

            If myValue.EndsWith(".OAR", StringComparison.InvariantCulture) Or myValue.EndsWith(".oar", StringComparison.InvariantCulture) Then
                ' nothing
            Else
                myValue += ".oar"
            End If

            If PropRegionClass.IsBooted(RegionUUID) Then
                Dim Group = PropRegionClass.GroupName(RegionUUID)
                ConsoleCommand(RegionUUID, "alert CPU Intensive Backup Started{ENTER}" & vbCrLf)
                ConsoleCommand(RegionUUID, "change region " & """" & chosen & """" & "{ENTER}" & vbCrLf)
                ConsoleCommand(RegionUUID, "save oar " & """" & BackupPath() & myValue & """" & "{ENTER}" & vbCrLf)
            End If
            Me.Focus()
            Print(My.Resources.Saving_word & " " & BackupPath() & "\" & myValue)
        Else
            Print(My.Resources.Not_Running)
        End If

    End Sub

    <CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId:="System.Windows.Forms.ToolTip.SetToolTip(System.Windows.Forms.Control,System.String)")>
    Private Function ScanAgents() As Integer

        If Not MysqlInterface.IsMySqlRunning() Then Return 0

        ' Scan all the regions
        Dim sbttl As Integer = 0
        Dim A = GetAgentList()
        Dim B = GetHGAgentList()
        Dim C As New Dictionary(Of String, String)

        If Debugger.IsAttached Then
            Try
                A.Add("Ferd Frederix", "SandBox")
                B.Add("Nyira Machabelli", "SandBox")
            Catch ex As Exception
                ' BreakPoint.Show(ex.Message)
            End Try
        End If

        ' Merge the two
        For Each keyname In A
            C.Add(keyname.Key, keyname.Value)
        Next
        For Each keyname In B
            If Not C.ContainsKey(keyname.Key) Then
                C.Add(keyname.Key, keyname.Value)
            End If
        Next

        '; start with zero avatars
        For Each RegionUUID As String In PropRegionClass.RegionUUIDs
            PropRegionClass.AvatarCount(RegionUUID) = 0
        Next

        ToolTip1.SetToolTip(AviLabel, "")
        AvatarLabel.Text = CStr(0)

        For Each NameValue In C
            Dim Avatar = NameValue.Key
            Dim RegionName = NameValue.Value

            If Not D.ContainsKey(Avatar) Then
                Print(Avatar & " is in " & RegionName)
                D.Add(Avatar, RegionName)
            End If
        Next

        Dim Str As String = ""
        For Each NameValue In C
            Dim Avatar = NameValue.Key
            Dim RegionName = NameValue.Value

            Dim RegionUUID As String = PropRegionClass.FindRegionByName(RegionName)
            If RegionUUID.Length > 0 Then
                PropRegionClass.AvatarCount(RegionUUID) += 1
                Str += Avatar & " in " & RegionName & ", "
            End If
        Next

        ToolTip1.SetToolTip(AviLabel, Str)

        Dim E As New List(Of String)
        For Each NameValue In D
            Dim Avatar = NameValue.Key
            Dim RegionName = NameValue.Value

            If Not C.ContainsKey(Avatar) Then
                Print(Avatar & " left " & RegionName)
                E.Add(Avatar)
            End If
        Next
        For Each F In E
            D.Remove(F)
        Next

        Dim total As Integer = C.Count
        AvatarLabel.Text = CStr(total)
        Return sbttl

    End Function

    Private Sub ScriptsResumeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ScriptsResumeToolStripMenuItem.Click
        SendScriptCmd("scripts resume")
    End Sub

    Private Sub ScriptsStartToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ScriptsStartToolStripMenuItem.Click
        SendScriptCmd("scripts start")
    End Sub

    Private Sub ScriptsStopToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ScriptsStopToolStripMenuItem.Click
        SendScriptCmd("scripts stop")
    End Sub

    Private Sub ScriptsSuspendToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ScriptsSuspendToolStripMenuItem.Click
        SendScriptCmd("scripts suspend")
    End Sub

    Private Sub SeePortsInUseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SeePortsInUseToolStripMenuItem.Click

        Using CPortsProcess As New Process
            CPortsProcess.StartInfo.UseShellExecute = True
            CPortsProcess.StartInfo.FileName = Settings.CurrentDirectory & "\Cports.exe"
            CPortsProcess.StartInfo.CreateNoWindow = False
            CPortsProcess.StartInfo.Arguments = ""
            CPortsProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal
            Try
                CPortsProcess.Start()
            Catch ex As Exception

                BreakPoint.Show(ex.Message)
            End Try
        End Using

    End Sub

    Private Sub SendScriptCmd(cmd As String)
        If Not PropOpensimIsRunning() Then
            Print(My.Resources.Not_Running)
            Return
        End If
        Dim rname = ChooseRegion(True)
        Dim RegionUUID As String = PropRegionClass.FindRegionByName(rname)
        If RegionUUID.Length > 0 Then
            ConsoleCommand(RegionUUID, "change region " & rname & "{ENTER}" & vbCrLf)
            ConsoleCommand(RegionUUID, cmd & "{ENTER}" & vbCrLf)
        End If

    End Sub

    ''' <summary>Set up all INI files</summary>
    ''' <returns>true if it fails</returns>
    Private Function SetIniData() As Boolean

        Print(My.Resources.Creating_INI_Files_word)

        If DoSetDefaultSims() Then Return True
        If DoTos() Then Return True
        If DoGridCommon() Then Return True
        If DoEditForeigners() Then Return True
        DelLibrary()
        If DoFlotsamINI() Then Return True
        If DoOpensimProtoINI() Then Return True
        If DoWifi() Then Return True
        If DoGloebits() Then Return True
        If DoTides() Then Return True
        If DoBirds() Then Return True
        If DoPHPDBSetup() Then Return True
        If DoPHP() Then Return True
        If DoApache() Then Return True
        If DoIceCast() Then Return True

        Return False

    End Function

    Private Sub SetLoopback()

        Dim Adapters = NetworkInterface.GetAllNetworkInterfaces()
        For Each adapter As NetworkInterface In Adapters
            Diagnostics.Debug.Print(adapter.Name)

            If adapter.Name = "Loopback" Then
                Print(My.Resources.Setting_Loopback)
                Using LoopbackProcess As New Process
                    LoopbackProcess.StartInfo.UseShellExecute = True ' so we can redirect streams
                    LoopbackProcess.StartInfo.FileName = Settings.CurrentDirectory & "\NAT_Loopback_Tool.bat"
                    LoopbackProcess.StartInfo.CreateNoWindow = False
                    LoopbackProcess.StartInfo.Arguments = "Loopback"
                    LoopbackProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
                    Try
                        LoopbackProcess.Start()
                    Catch ex As Exception

                        BreakPoint.Show(ex.Message)
                    End Try
                    Exit For
                End Using
            End If
        Next

    End Sub

    ''' <summary>Sets H,W and pos of screen on load</summary>
    Private Sub SetScreen()
        '365, 238 default
        ScreenPosition1 = New ScreenPos("Form1")
        AddHandler ResizeEnd, Handler
        Dim xy As List(Of Integer) = ScreenPosition1.GetXY()
        Left = xy.Item(0)
        Top = xy.Item(1)

        Dim hw As List(Of Integer) = ScreenPosition1.GetHW()

        If hw.Item(0) = 0 Then
            Me.Height = 238
        Else
            Try
                Me.Height = hw.Item(0)
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
            End Try
        End If

        If hw.Item(1) = 0 Then
            Me.Width = 365
        Else
            Me.Width = hw.Item(1)

        End If

        ScreenPosition1.SaveHW(Me.Height, Me.Width)

    End Sub

#End Region

#Region "Event Declarations"

#End Region

#Region "Public Enums"

#End Region

#Region "Public Properties"

#Disable Warning CA2227 ' Collection properties should be read only

#End Region

#Region "Things"

    <CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId:="Outworldz.Form1.ErrorLog(System.String)")>
    Private Sub SetupSearch()

        If Settings.ServerType <> "Robust" Then Return

        ' modify this to migrate search database upwards a rev
        If Not Settings.SearchMigration = _SearchRev Then

            MysqlInterface.DeleteSearchDatabase()

            Print(My.Resources.Setup_search)
            Dim pi As ProcessStartInfo = New ProcessStartInfo()

            FileIO.FileSystem.CurrentDirectory = Settings.CurrentDirectory & "\Outworldzfiles\mysql\bin\"
            pi.FileName = "Create_OsSearch.bat"
            pi.UseShellExecute = True
            pi.CreateNoWindow = False
            pi.WindowStyle = ProcessWindowStyle.Minimized

            Using MysqlSearch As Process = New Process With {
                    .StartInfo = pi
                }

                Try
                    MysqlSearch.Start()
                    MysqlSearch.WaitForExit()
                Catch ex As Exception

                    BreakPoint.Show(ex.Message)
                    ErrorLog("Could not create Search Database: " & ex.Message)
                    FileIO.FileSystem.CurrentDirectory = Settings.CurrentDirectory
                    Return
                End Try
            End Using

            FileIO.FileSystem.CurrentDirectory = Settings.CurrentDirectory

            Settings.SearchMigration = _SearchRev
            Settings.SaveSettings()

        End If

    End Sub

    <CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId:="Outworldz.Form1.ErrorLog(System.String)")>
    Private Sub SetupWordPress()

        If Settings.ServerType <> "Robust" Then Return

        Print(My.Resources.Setup_Wordpress)
        Dim pi As ProcessStartInfo = New ProcessStartInfo()

        FileIO.FileSystem.CurrentDirectory = Settings.CurrentDirectory & "\Outworldzfiles\mysql\bin\"
        pi.FileName = "Create_WordPress.bat"
        pi.UseShellExecute = True
        pi.CreateNoWindow = False
        pi.WindowStyle = ProcessWindowStyle.Minimized

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

        FileIO.FileSystem.CurrentDirectory = Settings.CurrentDirectory

    End Sub

    Private Sub ShowHyperGridAddressToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowHyperGridAddressToolStripMenuItem.Click

        Print(My.Resources.Grid_Address_is_word & vbCrLf & "http://" & Settings.PublicIP & ":" & Settings.HttpPort)

    End Sub

    Private Sub ShowRegionform()

        If RegionList.InstanceExists = False Then
            PropRegionForm = New RegionList
            PropRegionForm.Show()
            PropRegionForm.Activate()
            PropRegionForm.Select()
        Else
            PropRegionForm.Show()
            PropRegionForm.Activate()
            PropRegionForm.Select()

        End If

    End Sub

    Private Sub ShowToolStripMenuItem_Click(sender As System.Object, e As EventArgs) Handles mnuShow.Click

        Print(My.Resources.Is_Shown)
        mnuShow.Checked = True
        mnuHide.Checked = False
        mnuHideAllways.Checked = False

        Settings.ConsoleShow = "True"
        Settings.SaveSettings()

    End Sub

    Private Sub ShowUserDetailsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowUserDetailsToolStripMenuItem.Click
        Dim person = InputBox(My.Resources.Enter_1_2)
        If person.Length > 0 Then
            ConsoleCommand(RobustName, "show account " & person & "{ENTER}")
        End If
    End Sub

    Private Sub StartButton_Click(sender As System.Object, e As EventArgs) Handles StartButton.Click
        Startup()
    End Sub

    Private Sub Statmenu(sender As Object, e As EventArgs)
        If PropOpensimIsRunning() Then
            Dim RegionUUID As String = PropRegionClass.FindRegionByName(sender.Text)
            Dim port As String = CStr(PropRegionClass.RegionPort(RegionUUID))
            Dim webAddress As String = "http://localhost:" & Settings.HttpPort & "/bin/data/sim.html?port=" & port
            Try
                Process.Start(webAddress)
            Catch ex As Exception

                BreakPoint.Show(ex.Message)
            End Try
        Else
            Print(My.Resources.Not_Running)
        End If
    End Sub

#End Region

#Region "BootUp"

#End Region

#Region "ExitHandler Polling"

#End Region

#Region "Do"

#End Region

#Region "Stopping"

#End Region

#Region "Logging"

    Private Sub StopApache(force As Boolean)

        If Not Settings.ApacheEnable Then Return
        If Settings.ApacheService And Not force Then Return

        If Settings.ApacheService Then
            Using ApacheProcess As New Process()
                Print(My.Resources.Stopping_Apache)

                ApacheProcess.StartInfo.FileName = "net.exe"
                ApacheProcess.StartInfo.Arguments = "stop ApacheHTTPServer"
                ApacheProcess.StartInfo.CreateNoWindow = True
                ApacheProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
                Try
                    ApacheProcess.Start()
                    ApacheProcess.WaitForExit()
                Catch ex As Exception

                    BreakPoint.Show(ex.Message)
                    Print(My.Resources.ApacheNot_Stopping & ":" & ex.Message)
                End Try

            End Using
        Else
            Zap("httpd")
            Zap("rotatelogs")
        End If

        ApacheIs(False)

    End Sub

    Private Sub StopButton_Click_1(sender As System.Object, e As EventArgs) Handles StopButton.Click

        DoStopActions()

    End Sub

    Private Sub StopIcecast()

        Zap("icecast")
        IceCastIs(False)

    End Sub

    Private Sub StopMysql()

        If Not MysqlInterface.IsMySqlRunning() Then
            Application.DoEvents()
            MysqlInterface.IsRunning = False    ' mark all as not running
            MySqlIs(False)
            Return
        End If

        If Not PropStopMysql Then
            MysqlInterface.IsRunning = True    ' mark all as  running
            MySqlIs(True)
            Print(My.Resources.MySQL_Was_Running)
            Return
        End If

        Print("MySQL " & Global.Outworldz.My.Resources.Stopping_word)

        Dim p As Process = New Process()
        Dim pi As ProcessStartInfo = New ProcessStartInfo With {
            .Arguments = "--port " & CStr(Settings.MySqlRobustDBPort) & " -u root shutdown",
            .FileName = """" & Settings.CurrentDirectory & "\OutworldzFiles\mysql\bin\mysqladmin.exe" & """",
            .UseShellExecute = True, ' so we can redirect streams and minimize
            .WindowStyle = ProcessWindowStyle.Hidden
        }
        p.StartInfo = pi

        Try
            p.Start()
            MysqlInterface.IsRunning = False    ' mark all as not running
        Catch ex As Exception

            BreakPoint.Show(ex.Message)
        End Try
        Application.DoEvents()
        p.WaitForExit()
        p.Close()

        MySqlIs(False)
        If MysqlInterface.IsMySqlRunning() Then
            MysqlInterface.IsRunning = True    ' mark all as  running
            MySqlIs(True)
        End If

    End Sub

    Private Sub TechnicalInfoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TechnicalInfoToolStripMenuItem.Click
        Dim webAddress As String = PropDomain & "/Outworldz_installer/technical.htm"
        Try
            Process.Start(webAddress)
        Catch ex As Exception

            BreakPoint.Show(ex.Message)
        End Try
    End Sub

    <CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId:="Outworldz.Form1.Logger(System.String,System.String,System.String)")>
    Private Sub TestAllRegionPorts()

        Dim result As String = ""
        Dim Len = PropRegionClass.RegionCount()

        Dim Used As New List(Of String)
        ' Boot them up
        For Each RegionUUID As String In PropRegionClass.RegionUUIDs()
            If PropRegionClass.IsBooted(RegionUUID) Then
                Dim RegionName = PropRegionClass.RegionName(RegionUUID)

                If Used.Contains(RegionName) Then Continue For
                Used.Add(RegionName)
                Logger("INFO", "Testing region " & RegionName, "Diagnostics")
                Dim Port = PropRegionClass.GroupPort(RegionUUID)
                Print(My.Resources.Checking_Loopback_word & " " & RegionName)
                Logger("INFO", Global.Outworldz.My.Resources.Checking_Loopback_word & " " & RegionName, "Diagnostics")
                PortTest("http://" & Settings.PublicIP & ":" & Port & "/?_TestLoopback=" & RandomNumber.Random, Port)
            End If
        Next

    End Sub

    <CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId:="Outworldz.Form1.Logger(System.String,System.String,System.String)")>
    Private Sub TestPrivateLoopback()

        Dim result As String = ""
        Print(My.Resources.Checking_LAN_Loopback_word)
        Logger("Info", Global.Outworldz.My.Resources.Checking_LAN_Loopback_word, "Diagnostics")
        Dim weblink = "http://" & Settings.PrivateURL & ":" & Settings.DiagnosticPort & "/?_TestLoopback=" & RandomNumber.Random()
        Logger("Info", "URL= " & weblink, "Diagnostics")
        Using client As New WebClient
            Try
                result = client.DownloadString(weblink)
            Catch ex As Exception

                BreakPoint.Show(ex.Message)
                Logger("Error", ex.Message, "Diagnostics")
            End Try
        End Using

        If result = "Test Completed" Then
            Logger("INFO", Global.Outworldz.My.Resources.Passed_LAN, "Diagnostics")
            Print(My.Resources.Passed_LAN)
        Else
            Logger("INFO", Global.Outworldz.My.Resources.Failed_LAN & " " & weblink & " result was " & result, "Diagnostics")
            Print(My.Resources.Failed_LAN & " " & weblink)
            Settings.LoopBackDiag = False
            Settings.DiagFailed = True
        End If

    End Sub

    <CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId:="Outworldz.Form1.Logger(System.String,System.String,System.String)")>
    Private Sub TestPublicLoopback()

        If IPCheck.IsPrivateIP(Settings.PublicIP) Then

#Disable Warning ca1303
            Logger("INFO", "Local LAN IP", "Diagnostics")
#Enable Warning ca1303

            Return
        End If

        If Settings.ServerType <> "Robust" Then

#Disable Warning ca1303
            Logger("INFO", "Is Not Robust, Test Skipped", "Diagnostics")
#Enable Warning ca1303

            Return
        End If

        Print(My.Resources.Checking_Loopback_word)
        'Logger("INFO", Global.Outworldz.My.Resources.Checking_Loopback_word, "Diagnostics")
        PortTest("http://" & Settings.PublicIP & ":" & Settings.HttpPort & "/?_TestLoopback=" & RandomNumber.Random, Settings.HttpPort)

    End Sub

    Private Sub TextBox1_TextChanged(sender As System.Object, e As EventArgs) Handles TextBox1.TextChanged
        Dim ln As Integer = TextBox1.Text.Length
        TextBox1.SelectionStart = ln
        TextBox1.ScrollToCaret()
    End Sub

#End Region

    Private Sub Trim()
        If TextBox1.Text.Length > TextBox1.MaxLength - 100 Then
            TextBox1.Text = Mid(TextBox1.Text, 500)
        End If
    End Sub

    Private Sub UpdaterGo(Filename As String)

        KillAll()
        StopApache(True) 'really stop it, even if a service
        StopMysql()
        Application.DoEvents()
        Dim pUpdate As Process = New Process()
        Dim pi As ProcessStartInfo = New ProcessStartInfo With {
            .Arguments = Filename,
            .FileName = """" & Settings.CurrentDirectory & "\DreamGridSetup.exe" & """"
        }
        pUpdate.StartInfo = pi
        Print(My.Resources.SeeYouSoon)
        Try
            pUpdate.Start()
        Catch ex As Exception

            BreakPoint.Show(ex.Message)
            ErrorLog(My.Resources.ErrInstall)
        End Try
        End ' program

    End Sub

#Region "Timer"

    ''' <summary>
    ''' Timer runs every second registers DNS,looks for web server stuff that arrives, restarts any
    ''' sims , updates lists of agents builds teleports.html for older teleport checks for crashed
    ''' regions
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As EventArgs) Handles Timer1.Tick

        TimerBusy += 1
        If TimerBusy < 60 And TimerBusy > 1 Then
            Diagnostics.Debug.Print("Ticker busy")
            Return
        End If

        Chart() ' do charts collection each second
        Application.DoEvents()
        If Not PropOpensimIsRunning() Then
            Timer1.Stop()
            TimerBusy = 0
            Return
        End If

        If PropAborting Then
            Timer1.Stop()
            TimerBusy = 0
            Return
        End If

        ' print hourly marks on console
        If PropDNSSTimer Mod 3600 = 0 And PropDNSSTimer > 0 Then

            Dim thisDate As Date = Now
            Dim dt As String = thisDate.ToString(Globalization.CultureInfo.CurrentCulture)
            Print(dt & " " & Global.Outworldz.My.Resources.Running_word & " " & CInt((PropDNSSTimer / 3600)).ToString(Globalization.CultureInfo.InvariantCulture) & " " & Global.Outworldz.My.Resources.Hours_word)

        End If

        If PropDNSSTimer Mod 60 = 0 Then
            ScanAgents() ' update agent count  seconds
            Application.DoEvents()
            RegionListHTML() ' create HTML for older 2.4 region teleport
            Application.DoEvents()
        End If

        PropRegionClass.CheckPost() ' get the stack filled ASAP

        If PropDNSSTimer Mod ExitInterval = 0 And PropDNSSTimer > 0 Then
            ExitHandlerPoll() ' see if any regions have exited and set it up for Region Restart
            Application.DoEvents()
            RestartDOSboxes()
            Application.DoEvents()
        End If

        'hourly for DNS
        If PropDNSSTimer Mod 3600 = 0 Then
            RegisterName(Settings.DNSName, True)
            Dim array As String() = Settings.AltDnsName.Split(",".ToCharArray())
            For Each part As String In array
                RegisterName(part, True)
            Next
        End If

        ' hourly
        If PropDNSSTimer > 0 And PropDNSSTimer Mod 3600 = 0 Or PropDNSSTimer = 120 Then
            GetEvents() ' get the events from the Outworldz main server for all grids
        End If

        PropDNSSTimer += 1
        TimerBusy = 0

    End Sub

#End Region

#Region "Updater"

    <CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId:="Outworldz.Form1.ErrorLog(System.String)")>
    Private Sub UpdaterProcess_Exited(ByVal sender As Object, ByVal e As EventArgs) Handles UpdateProcess.Exited

        Dim ExitCode = UpdateProcess.ExitCode
        If ExitCode = 0 Then
            Settings.SkipUpdateCheck() = Update_version
        Else
            ErrorLog("ExitCode=" & CStr(ExitCode))
        End If

    End Sub

#End Region

#Region "Toolbars"

    <CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId:="System.Windows.Forms.ToolStripItem.set_ToolTipText(System.String)")>
    Public Sub LoadRegionsStatsBar()

        SimulatorStatsToolStripMenuItem.DropDownItems.Clear()
        SimulatorStatsToolStripMenuItem.Visible = False

        If PropRegionClass Is Nothing Then Return

        For Each RegionUUID As String In PropRegionClass.RegionUUIDs

            Dim Menu As New ToolStripMenuItem With {
                .Text = PropRegionClass.RegionName(RegionUUID),
                .ToolTipText = Global.Outworldz.My.Resources.Click_to_View_this_word & " " & PropRegionClass.RegionName(RegionUUID),
                .DisplayStyle = ToolStripItemDisplayStyle.Text
            }
            If PropRegionClass.IsBooted(RegionUUID) Then
                Menu.Enabled = True
            Else
                Menu.Enabled = False
            End If

            AddHandler Menu.Click, New EventHandler(AddressOf Statmenu)
            SimulatorStatsToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {Menu})
            SimulatorStatsToolStripMenuItem.Visible = True

        Next
    End Sub

    Public Sub RobustIs(Running As Boolean)

        If Not Running Then
            RobustToolStripMenuItem.Image = Global.Outworldz.My.Resources.nav_plain_red
        Else
            RobustToolStripMenuItem.Image = Global.Outworldz.My.Resources.check2
        End If
        Application.DoEvents()

    End Sub

    Private Sub AddLog(name As String)
        Dim LogMenu As New ToolStripMenuItem With {
                .Text = name,
                .ToolTipText = Global.Outworldz.My.Resources.Click_to_View_this_word,
                .Size = New Size(269, 26),
                .Image = Global.Outworldz.My.Resources.document_view,
                .DisplayStyle = ToolStripItemDisplayStyle.Text
            }
        AddHandler LogMenu.Click, New EventHandler(AddressOf LogViewClick)
        ViewLogsToolStripMenuItem.Visible = True
        ViewLogsToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {LogMenu})

    End Sub

    Private Sub AddUserToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddUserToolStripMenuItem.Click

        ConsoleCommand(RobustName, "create user{ENTER}")

    End Sub

    Private Sub AdminUIToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ViewWebUI.Click

        If PropOpensimIsRunning() Then
            If Settings.ApacheEnable Then
                Dim webAddress As String = "http://127.0.0.1:" & Convert.ToString(Settings.ApachePort, Globalization.CultureInfo.InvariantCulture)
                Try
                    Process.Start(webAddress)
                Catch ex As Exception

                    BreakPoint.Show(ex.Message)
                End Try
            Else
                Dim webAddress As String = "http://127.0.0.1:" & Settings.HttpPort
                Try
                    Process.Start(webAddress)
                Catch ex As Exception

                    BreakPoint.Show(ex.Message)
                End Try
                Print(My.Resources.User_Name_word & ":" & Settings.AdminFirst & " " & Settings.AdminLast)
                Print(My.Resources.Password_word & ":" & Settings.Password)
            End If
        Else
            If Settings.ApacheEnable Then
                Dim webAddress As String = "http://127.0.0.1:" & Convert.ToString(Settings.ApachePort, Globalization.CultureInfo.InvariantCulture)
                Try
                    Process.Start(webAddress)
                Catch ex As Exception

                    BreakPoint.Show(ex.Message)
                End Try
            Else
                Print(My.Resources.Not_Running)
            End If
        End If

    End Sub

    Private Sub AdvancedSettingsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AdvancedSettingsToolStripMenuItem.Click

        If PropInitted Then
            Adv1.Activate()
            Adv1.Visible = True
            Adv1.Select()
            Adv1.BringToFront()
        End If

    End Sub

    Private Sub AllRegionsOARsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AllTheRegionsOarsToolStripMenuItem.Click

        If Not PropOpensimIsRunning() Then
            Print(My.Resources.Not_Running)
            Return
        End If

        Dim WebThread = New Thread(AddressOf Backupper)
        WebThread.SetApartmentState(ApartmentState.STA)

        WebThread.Start()
        WebThread.Priority = ThreadPriority.BelowNormal ' UI gets priority

    End Sub

    Private Sub AllToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles All.Click

        Settings.LogLevel = "All"
        System.Environment.SetEnvironmentVariable("OSIM_LOGLEVEL", Settings.LogLevel.ToUpperInvariant)
        SendMsg(Settings.LogLevel)

    End Sub

    Private Sub AllUsersAllSimsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles JustOneRegionToolStripMenuItem.Click

        If Not PropOpensimIsRunning() Then
            Print(My.Resources.Not_Running)
            Return
        End If
        Dim RegionName = ChooseRegion(True)
        If RegionName.Length > 0 Then
            Dim Message = InputBox(My.Resources.What_to_say_2_region)
            Dim RegionUUID As String = PropRegionClass.FindRegionByName(RegionName)
            If RegionUUID.Length > 0 Then
                ConsoleCommand(RegionUUID, "change region  " & PropRegionClass.RegionName(RegionUUID) & "{ENTER}" & vbCrLf)
                ConsoleCommand(RegionUUID, "alert " & Message & "{ENTER}" & vbCrLf)
            End If

        End If

    End Sub

    Private Sub BackupCriticalFilesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BackupCriticalFilesToolStripMenuItem.Click

#Disable Warning CA2000 ' Dispose objects before losing scope
        Dim CriticalForm As New FormBackupCheckboxes
#Enable Warning CA2000 ' Dispose objects before losing scope

        CriticalForm.Activate()
        CriticalForm.Visible = True
        CriticalForm.Select()
        CriticalForm.BringToFront()

    End Sub

    Private Sub BackupDatabaseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BackupDatabaseToolStripMenuItem.Click

        BackupDB()

    End Sub

    Private Sub BackupIarClick(sender As Object, e As EventArgs)

        Dim File As String = Settings.CurrentDirectory & "/OutworldzFiles/AutoBackup/" & sender.Text 'make a real URL
        If LoadIARContent(File) Then
            Print(My.Resources.Opensimulator_is_loading & " " & sender.Text & ".  " & Global.Outworldz.My.Resources.Take_time)
        End If

    End Sub

    Private Sub BackupOarClick(sender As Object, e As EventArgs)

        Dim File = Settings.CurrentDirectory & "/OutworldzFiles/AutoBackup/" & sender.Text 'make a real URL
        If LoadOARContent(File) Then
            Print(My.Resources.Opensimulator_is_loading & " " & sender.Text & ".  " & Global.Outworldz.My.Resources.Take_time)
        End If

    End Sub

    Private Sub ChangePasswordToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ChangePasswordToolStripMenuItem.Click

        ConsoleCommand(RobustName, "reset user password{ENTER}")

    End Sub

    Private Sub CheckAndRepairDatabaseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CheckAndRepairDatbaseToolStripMenuItem.Click

        If Not StartMySQL() Then
            ToolBar(False)
            Buttons(StartButton)
            Print(My.Resources.Stopped_word)
            Return
        End If

        Dim pi As ProcessStartInfo = New ProcessStartInfo()

        ChDir(Settings.CurrentDirectory & "\OutworldzFiles\mysql\bin")
        pi.WindowStyle = ProcessWindowStyle.Normal
        pi.Arguments = CStr(Settings.MySqlRobustDBPort)

        pi.FileName = "CheckAndRepair.bat"
        Using pMySqlDiag1 As Process = New Process With {
                .StartInfo = pi
            }
            Try
                pMySqlDiag1.Start()
            Catch ex As Exception

                BreakPoint.Show(ex.Message)
            End Try
            pMySqlDiag1.WaitForExit()
        End Using

        ChDir(Settings.CurrentDirectory)

    End Sub

    Private Sub CheckForUpdatesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CHeckForUpdatesToolStripMenuItem.Click

        CheckForUpdates()

    End Sub

    Private Sub ClothingInventoryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClothingInventoryToolStripMenuItem.Click
        If PropInitted Then
            ContentIAR.Activate()
            ContentIAR.ShowForm()
            ContentIAR.Select()
            ContentIAR.BringToFront()
        End If
    End Sub

    Private Sub ConsoleCOmmandsToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ConsoleCOmmandsToolStripMenuItem1.Click
        Dim webAddress As String = "http://opensimulator.org/wiki/Server_Commands"
        Try
            Process.Start(webAddress)
        Catch ex As Exception

            BreakPoint.Show(ex.Message)
        End Try
    End Sub

    Private Sub DiagnosticsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DiagnosticsToolStripMenuItem.Click

        If Not PropOpensimIsRunning() Then
            Print(My.Resources.Click_Start)
            Return
        End If

        DoDiag()
        If Settings.DiagFailed = True Then
            Print(My.Resources.HG_Failed)
        Else
            Print(My.Resources.HG_Works)
        End If

    End Sub

    Private Sub ErrorToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ErrorToolStripMenuItem.Click

        Settings.LogLevel = "ERROR"
        System.Environment.SetEnvironmentVariable("OSIM_LOGLEVEL", Settings.LogLevel.ToUpperInvariant)
        SendMsg(Settings.LogLevel)

    End Sub

    Private Sub Info_Click(sender As Object, e As EventArgs) Handles Info.Click

        Settings.LogLevel = "INFO"
        System.Environment.SetEnvironmentVariable("OSIM_LOGLEVEL", Settings.LogLevel.ToUpperInvariant)
        SendMsg(Settings.LogLevel)

    End Sub

    Private Sub JobEngineToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles JobEngineToolStripMenuItem.Click
        For Each RegionUUID As String In PropRegionClass.RegionUUIDListByName("*")
            ConsoleCommand(RegionUUID, "debug jobengine status{ENTER}" & vbCrLf)
        Next
    End Sub

    Private Sub JustOneRegionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AllUsersAllSimsToolStripMenuItem.Click

        If Not PropOpensimIsRunning() Then
            Print(My.Resources.Not_Running)
            Return
        End If

        Dim HowManyAreOnline As Integer = 0
        Dim Message = InputBox(My.Resources.What_2_say_To_all)
        If Message.Length > 0 Then
            For Each RegionUUID As String In PropRegionClass.RegionUUIDs
                If PropRegionClass.AvatarCount(RegionUUID) > 0 Then
                    HowManyAreOnline += 1
                    ConsoleCommand(RegionUUID, "change region  " & PropRegionClass.RegionName(RegionUUID) & "{ENTER}" & vbCrLf)
                    ConsoleCommand(RegionUUID, "alert " & Message & "{ENTER}" & vbCrLf)
                End If

            Next
            If HowManyAreOnline = 0 Then
                Print(My.Resources.Nobody_Online)
            Else
                Print(My.Resources.Message_sent_word & ":" & CStr(HowManyAreOnline) & " regions")
            End If
        End If

    End Sub

    ''' <summary>The main startup - done this way so languages can reload the entire form</summary>
    Private Sub JustQuitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles JustQuitToolStripMenuItem.Click

        Print("Zzzz...")
        End

    End Sub

    Private Sub ThreadpoolsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ThreadpoolsToolStripMenuItem.Click
        For Each RegionUUID As String In PropRegionClass.RegionUUIDListByName("*")
            ConsoleCommand(RegionUUID, "show threads{ENTER}" & vbCrLf)
        Next
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        Dim webAddress As String = PropDomain() & "/Outworldz_Installer/PortForwarding.htm"
        Try
            Process.Start(webAddress)
        Catch ex As Exception

            BreakPoint.Show(ex.Message)
        End Try
    End Sub

    Private Sub TroubleshootingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TroubleshootingToolStripMenuItem.Click

        HelpManual("TroubleShooting")

    End Sub

    Private Sub ViewIcecastWebPageToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ViewIcecastWebPageToolStripMenuItem.Click
        If PropOpensimIsRunning() And Settings.SCEnable Then
            Dim webAddress As String = "http://" & Settings.PublicIP & ":" & CStr(Settings.SCPortBase)
            Print(My.Resources.Icecast_Desc & webAddress & "/stream")
            Try
                Process.Start(webAddress)
            Catch ex As Exception

                BreakPoint.Show(ex.Message)
            End Try
        ElseIf Settings.SCEnable = False Then
            Print(My.Resources.Shoutcast_Disabled)
        Else
            Print(My.Resources.Not_Running)
        End If
    End Sub

    Private Sub ViewRegionMapToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ViewRegionMapToolStripMenuItem.Click

        ShowRegionMap()

    End Sub

    Private Sub Warn_Click(sender As Object, e As EventArgs) Handles Warn.Click

        Settings.LogLevel = "WARN"
        System.Environment.SetEnvironmentVariable("OSIM_LOGLEVEL", Settings.LogLevel.ToUpperInvariant)
        SendMsg(Settings.LogLevel)

    End Sub

    Private Sub XengineToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles XengineToolStripMenuItem.Click
        For Each RegionUUID As String In PropRegionClass.RegionUUIDListByName("*")
            ConsoleCommand(RegionUUID, "xengine status{ENTER}" & vbCrLf)
            Application.DoEvents()
        Next
    End Sub

#End Region

End Class
