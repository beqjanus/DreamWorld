#Region "Copyright"

' Copyright 2014 Fred Beckhusen for Outworldz.com https://opensource.org/licenses/AGPL

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
'DEALINGS IN THE SOFTWARE.

#End Region

'Option Strict On

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
Imports MySql.Data.MySqlClient

Public Class Form1

#Region "Version"

    Private _MyVersion As String = "3.33"
    Private _SimVersion As String = "066a6fbaa1 (changes on lludp acks and resends, 2019-12-18)"

#End Region

#Region "Fields"

    Private WithEvents ApacheProcess As New Process()
    Private WithEvents IcecastProcess As New Process()
    Private WithEvents ProcessMySql As Process = New Process()
    Private WithEvents RobustProcess As New Process()
    Private WithEvents UpdateProcess As New Process()

#End Region

#Region "Private Fields"

    Private _DNS_is_registered = False
    Private _ApacheCrashCounter As Integer = 0
    Private _ApacheExited As Boolean = False
    Private _ApacheProcessID As Integer = 0
    Private _ApacheUninstalling As Boolean = False
    Private _ContentIAR As FormOAR
    Private _ContentOAR As FormOAR
    Private _CPUMAX As Single = 75
    Private _CurSlashDir As String
    Private _debugOn As Boolean = False
    Private _DNSSTimer As Integer = 0
    Private _Domain As String = "http://outworldz.com"
    Private _ExitHandlerIsBusy As Boolean = False
    Private _exitList As New ArrayList()
    Private _ForceMerge As Boolean = False
    Private _ForceParcel As Boolean = False
    Private _ForceTerrain As Boolean = True
    Private _IcecastCrashCounter As Integer = 0
    Private _IceCastExited As Boolean = False
    Private _IcecastProcID As Integer = 0
    Private _Initted As Boolean = False
    Private _IPv4Address As String
    Private _IsRunning As Boolean = False
    Private _KillSource As Boolean = False
    Private _MaxPortUsed As Integer = 0
    Private _myFolder As String
    Private _mySetting As New MySettings
    Private _MysqlCrashCounter As Integer = 0
    Private _MysqlExited As Boolean = False
    Private _myUPnpMap As UPnp

    Private _OpensimBinPath As String
    Private _PropAborting As Boolean = False
    Private _regionClass As RegionMaker
    Private _regionForm As RegionList
    Private _regionHandles As New Dictionary(Of Integer, String)
    Private _RestartApache As Boolean = False
    Private _RestartMysql As Boolean = False
    Private _RestartNow As Boolean = False
    Private _RestartRobust As Boolean
    Private _RobustCrashCounter As Integer = 0
    Private _RobustExited As Boolean = False
    Private _RobustIsStarting As Boolean = False
    Private _RobustProcID As Integer = 0
    Private _SelectedBox As String = ""
    Private _speed As Double = 50
    Private _StopMysql As Boolean = True
    Private _UpdateView As Boolean = True
    Private _UserName As String = ""
    Private _viewedSettings As Boolean = False
    Private D As New Dictionary(Of String, String)
    Private Handler As New EventHandler(AddressOf Resize_page)
    Private MyCPUCollection(181) As Double
    Private MyRAMCollection(181) As Double
    Private speed As Single = 0
    Private speed1 As Single = 0
    Private speed2 As Single = 0
    Private speed3 As Single = 0
    Private Update_version As String = Nothing
    Private ws As NetServer

#End Region

#Region "No Warning"

#Disable Warning CA2213
    Private Adv As New AdvancedForm
    Private newScreenPosition As ScreenPos
    Private ScreenPosition As ScreenPos
    Private cpu As New PerformanceCounter
#Disable Warning CA2213

#End Region


#Region "Public Events"

    Public Event ApacheExited As EventHandler

    Public Event Exited As EventHandler

    Public Event RobustExited As EventHandler

#End Region

#Region "Public Enums"

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

#End Region

#Region "Public Properties"

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

    Public Property PropCPUMAX As Single
        Get
            Return _CPUMAX
        End Get
        Set(value As Single)
            _CPUMAX = value
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

    Public Property PropDebug As Boolean
        Get
            Return _debugOn
        End Get
        Set(value As Boolean)
            _debugOn = value
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

    Public Property PropDomain As String
        Get
            Return _Domain
        End Get
        Set(value As String)
            _Domain = value
        End Set
    End Property

    Public Property PropExitHandlerIsBusy() As Boolean
        Get
            Return _ExitHandlerIsBusy
        End Get
        Set(ByVal Value As Boolean)
            _ExitHandlerIsBusy = Value
        End Set
    End Property

    Public ReadOnly Property PropExitList As ArrayList
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

    Public Property PropMyFolder As String
        Get
            Return _myFolder
        End Get
        Set(value As String)
            _myFolder = value
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

    Public Property PropMyVersion As String
        Get
            Return _MyVersion
        End Get
        Set(value As String)
            _MyVersion = value
        End Set
    End Property

    Public Property PropOpensimBinPath As String
        Get
            Return _OpensimBinPath
        End Get
        Set(value As String)
            _OpensimBinPath = value
        End Set
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

    Public Property PropRestartNow As Boolean
        Get
            Return _RestartNow
        End Get
        Set(value As Boolean)
            _RestartNow = value
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

    Public Property PropSimVersion As String
        Get
            Return _SimVersion
        End Get
        Set(value As String)
            _SimVersion = value
        End Set
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

    Public Property PropUseIcons As Boolean = False

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


    Public Property Settings As MySettings
        Get
            Return _mySetting
        End Get
        Set(value As MySettings)
            _mySetting = value
        End Set
    End Property

    Public Property SimVersion As String
        Get
            Return _SimVersion
        End Get
        Set(value As String)
            _SimVersion = value
        End Set
    End Property

#End Region

#Region "Public Methods"

    Public Shared Function CompareDLLignoreCase(tofind As String, dll As List(Of String)) As Boolean
        If dll Is Nothing Then Return False
        For Each filename In dll
            If tofind.ToUpper(Globalization.CultureInfo.InvariantCulture) = filename.ToUpper(Globalization.CultureInfo.InvariantCulture) Then Return True
        Next
        Return False
    End Function

    Public Shared Sub DeleteEvents(Connection As MySqlConnection)

        Dim stm = "delete from events"
        Using cmd As MySqlCommand = New MySqlCommand(stm, Connection)
            Dim rowsdeleted = cmd.ExecuteNonQuery()
            Diagnostics.Debug.Print("Rows: {0}", rowsdeleted.ToString(Globalization.CultureInfo.InvariantCulture))
        End Using

    End Sub

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

    ''' <summary>This method starts at the specified directory. It traverses all subdirectories. It returns a List of those directories.</summary>
    Public Shared Function GetFilesRecursive(ByVal initial As String) As List(Of String)
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

            ' Add all immediate file paths
            Try
                result.AddRange(Directory.GetFiles(dir, "*.dll"))
            Catch ex As ArgumentException
            Catch ex As UnauthorizedAccessException
            Catch ex As DirectoryNotFoundException
            Catch ex As PathTooLongException
            Catch ex As IOException
            End Try

            ' Loop through all subdirectories and add them to the stack.
            Dim directoryName As String

            'Save, but skip scriptengines
            For Each directoryName In Directory.GetDirectories(dir)
                If Not directoryName.Contains("ScriptEngines") Then
                    stack.Push(directoryName)
                Else
                    Diagnostics.Debug.Print("Skipping script")
                End If
                'Application.doevents()
            Next
            'Application.doevents()
        Loop

        ' Return the list
        Return result
    End Function

    Public Shared Sub Help(page As String)

        FormHelp.Activate()
        FormHelp.Visible = True
        FormHelp.Init(page)
        FormHelp.Select()
        FormHelp.BringToFront()

    End Sub

    Public Shared Function ShowDOSWindow(handle As IntPtr, command As SHOWWINDOWENUM) As Boolean

        Dim ctr = 50
        If handle <> IntPtr.Zero Then
            Dim x = False

            While Not x And ctr > 0
                Try
                    x = NativeMethods.ShowWindow(handle, command)
                    If x Then Return True
#Disable Warning CA1031 ' Do not catch general exception types
                Catch ex As Exception
#Enable Warning CA1031 ' Do not catch general exception types
                End Try
                ctr -= 1
                Sleep(100)
            End While
        End If
        Return False

    End Function

    ''' <summary>Sleep(ms)</summary>
    ''' <param name="value">millseconds</param>
    Public Shared Sub Sleep(value As Integer)

        ' value is in milliseconds, but we do it in 10 passes so we can doevents() to free up console
        Dim sleeptime = value / 100  ' now in tenths

        While sleeptime > 0
            'Application.doevents()
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

    Public Shared Sub WriteEvent(Connection As MySqlConnection, D As Dictionary(Of String, String))

        If D Is Nothing Then Return

        Dim stm = "insert into events (simname,category,creatoruuid, owneruuid,name, description, dateUTC,duration,covercharge, coveramount,parcelUUID, globalPos,gateway,eventflags) values (" _
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
                        & "'" & D.Item("gateway") & "'," _
                        & "'" & D.Item("eventflags") & "')"

#Disable Warning CA2100 ' Review SQL queries for security vulnerabilities
        Using cmd As MySqlCommand = New MySqlCommand(stm, Connection)
#Enable Warning CA2100 ' Review SQL queries for security vulnerabilities
            Dim rowsinserted = cmd.ExecuteNonQuery()
            Diagnostics.Debug.Print("Insert: {0}", CStr(rowsinserted))
        End Using

    End Sub

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
            .WorkingDirectory = PropMyFolder & "\OutworldzFiles\mysql\bin\",
            .FileName = PropMyFolder & "\OutworldzFiles\mysql\bin\BackupMysql.bat"
            }
            pMySqlBackup.StartInfo = pi
            Try
                pMySqlBackup.Start()
            Catch ex As InvalidOperationException
            Catch ex As System.ComponentModel.Win32Exception

            End Try

        End Using

    End Sub

    ''' <summary>Starts Opensim for a given name</summary>
    ''' <param name="BootName">Name of region to start</param>
    ''' <returns>success = true</returns>
    Public Function Boot(Regionclass As RegionMaker, BootName As String) As Boolean

        If Regionclass Is Nothing Then Return False
        If RegionMaker.Instance Is Nothing Then Return False

        If PropAborting Then Return True

        Dim RegionUUID As String = Regionclass.FindRegionByName(BootName)
        Dim GroupName = Regionclass.GroupName(RegionUUID)

        If RegionUUID = "" Then
            ErrorLog("Cannot find " & BootName & " to boot!")
            Return False
        End If
        Log(My.Resources.Info, "Region: Starting Region " & BootName)
        If Regionclass.IsBooted(RegionUUID) Then
            Log(My.Resources.Info, "Region " & BootName & " already running")
            PropUpdateView = True ' make form refresh
            Return True
        End If

        If Regionclass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.RecyclingUp Then
            Log(My.Resources.Info, "Region " & BootName & " skipped as it is already Warming Up")
            PropUpdateView = True ' make form refresh
            Return True
        End If

        If Regionclass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.Booting Then
            Log(My.Resources.Info, "Region " & BootName & " skipped as it is already Booting Up")
            PropUpdateView = True ' make form refresh
            Return True
        End If

        If Regionclass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.ShuttingDown Then

            Log(My.Resources.Info, "Region " & BootName & " skipped as it is already Shutting Down")
            PropUpdateView = True ' make form refresh
            Return True
        End If

        If Regionclass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.RecyclingDown Then
            Log(My.Resources.Info, "Region " & BootName & " skipped as it is already Recycling Down")
            PropUpdateView = True ' make form refresh
            Return True
        End If

        If Regionclass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.Suspended Then
            Regionclass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.Resume
            Log(My.Resources.Info, "Region " & BootName & " skipped as it is Suspended, Resuming it instead")
            PropUpdateView = True ' make form refresh
            Return True
        End If

        'Application.doevents()

        DoRegion(BootName)
        Dim isRegionRunning = CheckPort("127.0.0.1", Regionclass.GroupPort(RegionUUID))
        If isRegionRunning Then
            Print(BootName & " " & My.Resources.is_already_running_word)

            ' if running, grab it and return
            If Regionclass.ProcessID(RegionUUID) = 0 Then
                Dim listP = Process.GetProcesses
                For Each p In listP
                    'Application.doevents()
                    If p.MainWindowTitle = GroupName Then
                        If Not PropRegionHandles.ContainsKey(p.Id) Then
                            PropRegionHandles.Add(p.Id, GroupName) ' save in the list of exit events in case it crashes or exits
                        End If
                        Dim thisname = GroupName
                        For Each RegionUUID In Regionclass.RegionUUIDListByName(thisname)
                            Regionclass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.Booted ' force it up
                            Regionclass.ProcessID(RegionUUID) = p.Id
                            'Application.doevents()
                        Next
                        PropUpdateView = True ' make form refresh
                        Return True
                    End If
                Next
                Return False
            Else
                If Not PropRegionHandles.ContainsKey(Regionclass.ProcessID(RegionUUID)) Then
                    PropRegionHandles.Add(Regionclass.ProcessID(RegionUUID), GroupName) ' save in the list of exit events in case it crashes or exits
                End If

                For Each UUID As String In Regionclass.RegionUUIDListByName(GroupName)
                    Regionclass.Status(UUID) = RegionMaker.SIMSTATUSENUM.Booted ' force it up
                    'Application.doevents()
                Next
                PropUpdateView = True ' make form refresh
                Return True
            End If

        End If

        Environment.SetEnvironmentVariable("OSIM_LOGPATH", Settings.OpensimBinPath() & "bin\Regions\" & GroupName)
        Environment.SetEnvironmentVariable("OSIM_LOGLEVEL", Settings.LogLevel.ToUpperInvariant)

        Dim myProcess As Process = GetNewProcess()

        Print(My.Resources.Starting_word & " " & GroupName)

        myProcess.EnableRaisingEvents = True
        myProcess.StartInfo.UseShellExecute = True ' so we can redirect streams
        myProcess.StartInfo.WorkingDirectory = Settings.OpensimBinPath() & "bin"

        myProcess.StartInfo.FileName = """" & Settings.OpensimBinPath() & "bin\OpenSim.exe" & """"
        myProcess.StartInfo.CreateNoWindow = False
        myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal
        myProcess.StartInfo.Arguments = " -inidirectory=" & """" & "./Regions/" & GroupName & """"

        FileStuff.DeleteFile(Settings.OpensimBinPath() & "bin\Regions\" & GroupName & "\Opensim.log")
        FileStuff.DeleteFile(Settings.OpensimBinPath() & "bin\Regions\" & GroupName & "\PID.pid")
        FileStuff.DeleteFile(Settings.OpensimBinPath() & "bin\regions\" & GroupName & "\OpensimConsole.log")
        FileStuff.DeleteFile(Settings.OpensimBinPath() & "bin\regions\" & GroupName & "\OpenSimStats.log")

        SequentialPause()   ' wait for previous region to give us some CPU

        Dim ok As Boolean = False
        Try
            ok = myProcess.Start
        Catch ex As InvalidOperationException
            ErrorLog(ex.Message)
        Catch ex As System.ComponentModel.Win32Exception
            ErrorLog(ex.Message)
        End Try

        If ok Then
            Dim PID = WaitForPID(myProcess)
            ' check if it gave us a PID, if not, it failed.
            If PID = 0 Then
                Regionclass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.Error
                PropUpdateView = True ' make form refresh
                Return False
            End If

            For Each UUID As String In Regionclass.RegionUUIDListByName(GroupName)
                Log("Debug", "Process started for " & Regionclass.RegionName(UUID) & " PID=" & CStr(myProcess.Id) & " UUID:" & CStr(UUID))
                Regionclass.Status(UUID) = RegionMaker.SIMSTATUSENUM.Booting
                Regionclass.ProcessID(UUID) = PID
                Regionclass.Timer(UUID) = RegionMaker.REGIONTIMER.StartCounting
                'Application.doevents()
            Next

            PropUpdateView = True ' make form refresh
            'Application.doevents()
            SetWindowTextCall(myProcess, GroupName)

            Log("Debug", "Created Process Number " & CStr(myProcess.Id) & " in  RegionHandles(" & CStr(PropRegionHandles.Count) & ") " & "Group:" & GroupName)
            If Not PropRegionHandles.ContainsKey(myProcess.Id) Then
                PropRegionHandles.Add(myProcess.Id, GroupName) ' save in the list of exit events in case it crashes or exits
            End If

            PropOpensimIsRunning() = True
            Buttons(StopButton)

            Return True
        End If

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

    Public Sub CheckDefaultPorts()

        If Settings.DiagnosticPort = Settings.HttpPort _
        Or Settings.DiagnosticPort = Settings.PrivatePort _
        Or Settings.HttpPort = Settings.PrivatePort Then
            Settings.DiagnosticPort = 8001
            Settings.HttpPort = 8002
            Settings.PrivatePort = 8003

            MsgBox(My.Resources.Port_Error, vbInformation, My.Resources.Error_word)
        End If

    End Sub

    Public Sub CheckForUpdates()

        Using client As New WebClient ' download client for web pages
            Print(My.Resources.Checking_for_Updates_word)
            Try
                Update_version = client.DownloadString(PropDomain() & "/Outworldz_Installer/UpdateGrid.plx?fill=1" & GetPostData())
            Catch ex As ArgumentNullException
                ErrorLog(My.Resources.Wrong & " " & ex.Message)
                Return
            Catch ex As WebException
                ErrorLog(My.Resources.Wrong & " " & ex.Message)
                Return
            Catch ex As NotSupportedException
                ErrorLog(My.Resources.Wrong & " " & ex.Message)
                Return
            End Try
        End Using
        If Update_version.Length = 0 Then Update_version = "0"
        Dim Delta As Single = 0
        Try
            Delta = Convert.ToSingle(Update_version, Globalization.CultureInfo.InvariantCulture) - Convert.ToSingle(PropMyVersion, Globalization.CultureInfo.InvariantCulture)
        Catch ex As FormatException
        Catch ex As OverflowException
        End Try

        If Delta > 0 Then

            If System.IO.File.Exists(PropMyFolder & "\DreamGrid-V" & CStr(Update_version) & ".zip") Then
                Dim result = MsgBox("V" & Update_version & My.Resources.Update_Downloaded, vbYesNo)
                If result = vbOK Then
                    UpdaterGo("DreamGrid-V" & Convert.ToString(Update_version, Globalization.CultureInfo.InvariantCulture) & ".zip")
                End If
                Return
            End If

            Print(My.Resources.Update_is_available & ":" & Update_version)
            Dim pi As ProcessStartInfo = New ProcessStartInfo With {
                .Arguments = "DreamGrid-V" & Convert.ToString(Update_version, Globalization.CultureInfo.InvariantCulture) & ".zip",
                .FileName = """" & PropMyFolder & "\Downloader.exe" & """"
            }

            If Debugger.IsAttached Then
                pi.WindowStyle = ProcessWindowStyle.Normal
            Else
                pi.WindowStyle = ProcessWindowStyle.Minimized
            End If

            UpdateProcess.StartInfo = pi
            UpdateProcess.EnableRaisingEvents = True
            Try
                UpdateProcess.Start()
            Catch ex As InvalidOperationException
                Print(My.Resources.ErrUpdate)
            Catch ex As ComponentModel.Win32Exception
                Print(My.Resources.ErrUpdate)
            End Try
        End If

    End Sub

    Public Function CheckPort(ServerAddress As String, Port As Integer) As Boolean

        Log(My.Resources.Info, "Checking port " & CStr(Port))
        Using ClientSocket As New TcpClient
            Try
                ClientSocket.Connect(ServerAddress, Port)
            Catch ex As ArgumentNullException
                Log(My.Resources.Info, "Argument Null " & ex.Message)
                Return False
            Catch ex As ArgumentOutOfRangeException
                Log(My.Resources.Info, "Argument Out of Range " & ex.Message)
                Return False
            Catch ex As SocketException
                Log(My.Resources.Info, "Socket Exception& ex.message" & ex.Message)
                Return False

            End Try

            If ClientSocket.Connected Then
                Log(My.Resources.Info, " port probe success on port " & CStr(Port))
                Return True
            End If
        End Using
        Log(My.Resources.Info, " port probe fail on port " & CStr(Port))
        Return False

    End Function

    Public Function ChooseRegion(Optional JustRunning As Boolean = False) As String

        ' Show testDialog as a modal dialog and determine if DialogResult = OK.
        Dim chosen As String = ""
        Using Chooseform As New Choice ' form for choosing a set of regions
            Chooseform.FillGrid("Region", JustRunning)  ' populate the grid with either Group or RegionName
            Dim ret = Chooseform.ShowDialog()
            If ret = DialogResult.Cancel Then Return ""
            Try
                ' Read the chosen sim name
                chosen = Chooseform.DataGridView.CurrentCell.Value.ToString()
#Disable Warning CA1031 ' Do not catch general exception types
            Catch ex As Exception
#Enable Warning CA1031 ' Do not catch general exception types
                ErrorLog("Warn: Could not choose a displayed region. " & ex.Message)
            End Try
        End Using
        Return chosen

    End Function

    ''' <summary>Sends keystrokes to Opensim. Always sends and enter button before to clear and use keys</summary>
    ''' <param name="ProcessID">PID of the DOS box</param>
    ''' <param name="command">String</param>
    ''' <returns></returns>
    Public Function ConsoleCommand(RegionUUID As String, command As String) As Boolean

        If command Is Nothing Then Return False
        If command.Length > 0 Then

            Dim PID As Integer
            If RegionUUID <> "Robust" Then

                PID = PropRegionClass.ProcessID(RegionUUID)
                Try
                    If PID > 0 Then ShowDOSWindow(Process.GetProcessById(PID).MainWindowHandle, SHOWWINDOWENUM.SWRESTORE)
#Disable Warning CA1031 ' Do not catch general exception types
                Catch ex As Exception
#Enable Warning CA1031 ' Do not catch general exception types
                    Return False
                End Try
            Else
                PID = PropRobustProcID
                Try
                    ShowDOSWindow(Process.GetProcessById(PID).MainWindowHandle, SHOWWINDOWENUM.SWRESTORE)
#Disable Warning CA1031 ' Do not catch general exception types
                Catch ex As Exception
#Enable Warning CA1031 ' Do not catch general exception types
                    Return False
                End Try
            End If

            Try
                'plus sign(+), caret(^), percent sign (%), tilde (~), And parentheses ()
                command = command.Replace("+", "{+}")
                command = command.Replace("^", "{^}")
                command = command.Replace("%", "{%}")
                command = command.Replace("(", "{(}")
                command = command.Replace(")", "{)}")
            Catch ex As ArgumentNullException
            Catch ex As ArgumentException
            End Try
            Try
                AppActivate(PID)
                SendKeys.SendWait(ToLowercaseKeys("{ENTER}" & vbCrLf))
                SendKeys.SendWait(ToLowercaseKeys(command))
#Disable Warning CA1031 ' Do not catch general exception types
            Catch
                Return False
#Enable Warning CA1031 ' Do not catch general exception types
            End Try

        End If

        Return True

    End Function

    Public Sub CopyOpensimProto(name As String)

        Dim RegionUUID As String = PropRegionClass.FindRegionByName(name)
        If RegionUUID.Length > 0 Then Opensimproto(RegionUUID)

    End Sub

    Public Sub CopyWifi(Page As String)
        Try
            System.IO.Directory.Delete(PropOpensimBinPath & "WifiPages", True)
            System.IO.Directory.Delete(PropOpensimBinPath & "bin\WifiPages", True)
        Catch ex As IOException
        Catch ex As UnauthorizedAccessException
        Catch ex As ArgumentNullException
        Catch ex As ArgumentException
        End Try

        Try
            My.Computer.FileSystem.CopyDirectory(PropOpensimBinPath & "WifiPages-" & Page, PropOpensimBinPath & "WifiPages", True)
            My.Computer.FileSystem.CopyDirectory(PropOpensimBinPath & "bin\WifiPages-" & Page, PropOpensimBinPath & "\bin\WifiPages", True)
        Catch ex As DirectoryNotFoundException
        Catch ex As PathTooLongException
        Catch ex As IOException
        Catch ex As UnauthorizedAccessException
        Catch ex As ArgumentNullException
        Catch ex As ArgumentException
        Catch ex As InvalidOperationException
        Catch ex As NotSupportedException
        Catch ex As System.Security.SecurityException
        End Try

    End Sub

    Public Function DelLibrary() As Boolean

        Print("->Set Library")
        Try
            System.IO.File.Delete(PropOpensimBinPath & "bin\Library\Clothing Library (small).iar")
            System.IO.File.Delete(PropOpensimBinPath & "bin\Library\Objects Library (small).iar")
        Catch ex As IOException
        Catch ex As UnauthorizedAccessException
        Catch ex As ArgumentNullException
        Catch ex As ArgumentException
        End Try

        Return False

    End Function

    Public Function DoGloebits() As Boolean

        'Gloebits.ini
        If Settings.LoadIni(PropOpensimBinPath & "bin\Gloebit.ini", ";") Then Return True
        Print("->Set Globits")
        If Settings.GloebitsEnable Then
            Settings.SetIni("Gloebit", "Enabled", "True")
        Else
            Settings.SetIni("Gloebit", "Enabled", "False")
        End If

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

    Public Sub ErrorLog(message As String)
        If Debugger.IsAttached Then
            'MsgBox(message, vbInformation)
        End If
        Logger(My.Resources.Error_word, message, My.Resources.Error_word)
    End Sub

    ''' <summary>Gets the External Host name which can be either the Public IP or a Host name.</summary>
    ''' <returns>Host for regions</returns>
    Public Function ExternLocalServerName() As String

        Dim Host As String

        If Settings.ExternalHostName.Length > 0 Then
            Host = Settings.ExternalHostName
        Else
            Host = Settings.PublicIP
        End If
        Return Host

    End Function

    Public Sub ForceStopGroup(Groupname As String)

        For Each RegionUUID As String In PropRegionClass.RegionUUIDListByName(Groupname)
            ' Called by a sim restart, do not change status
            PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.Stopped
            Log(My.Resources.Info, PropRegionClass.RegionName(RegionUUID) & " Stopped")
            ' End If
            PropRegionClass.Timer(RegionUUID) = RegionMaker.REGIONTIMER.Stopped
            'Application.doevents()
        Next
        Log(My.Resources.Info, Groupname & " Group is now stopped")
        PropUpdateView = True ' make form refresh

    End Sub

    Public Function GetHostAddresses(hostName As String) As String

        Try
            Dim IPList As IPHostEntry = System.Net.Dns.GetHostEntry(hostName)

            For Each IPaddress In IPList.AddressList
                If (IPaddress.AddressFamily = Sockets.AddressFamily.InterNetwork) Then
                    Dim ip = IPaddress.ToString()
                    Return ip
                End If
                'Application.doevents()
            Next
            Return String.Empty
#Disable Warning CA1031 ' Do not catch general exception types
        Catch ex As Exception
#Enable Warning CA1031 ' Do not catch general exception types
            ErrorLog("Warn:Unable to resolve name:" & ex.Message)
        End Try
        Return String.Empty

    End Function

    Public Function GetHwnd(Groupname As String) As IntPtr

        If Groupname = "Robust" Then
            Dim h As IntPtr
            Try
                h = RobustProcess.MainWindowHandle
            Catch ex As InvalidOperationException
                h = IntPtr.Zero
            Catch ex As NotSupportedException
                h = IntPtr.Zero
            End Try
            Return h
        End If

        Dim Regionlist = PropRegionClass.RegionUUIDListByName(Groupname)

        For Each RegionUUID As String In Regionlist
            Dim pid = PropRegionClass.ProcessID(RegionUUID)

            Dim ctr = 20   ' 2 seconds
            Dim found As Boolean = False
            While Not found And ctr > 0
                Sleep(100)

                For Each pList As Process In Process.GetProcesses()
                    If pList.Id = pid Then
                        Return pList.MainWindowHandle
                    End If
                    'Application.doevents()
                    ctr -= 1
                Next
            End While
        Next
        Return IntPtr.Zero

    End Function

    Public Function GetNewDnsName() As String

        Dim client As New WebClient
        Dim Checkname As String
        Try
            Checkname = client.DownloadString("http://outworldz.net/getnewname.plx/?r=" & RandomNumber.Random)
        Catch ex As ArgumentNullException
            ErrorLog("Error:Cannot get new name:" & ex.Message)
            client.Dispose()
            Return ""
        Catch ex As WebException
            ErrorLog("Error:Cannot get new name:" & ex.Message)
            client.Dispose()
            Return ""
        Catch ex As NotSupportedException
            ErrorLog("Error:Cannot get new name:" & ex.Message)
            client.Dispose()
            Return ""
        End Try
        client.Dispose()
        Return Checkname

    End Function

    ''' <summary>Creates and exit handler for each region</summary>
    ''' <returns>a process handle</returns>
    Public Function GetNewProcess() As Process

        Dim handle = New Handler
        Return handle.Init(PropRegionHandles, PropExitList)

    End Function

    ''' <summary>Loads the INI file for the proper grid type for parsing</summary>
    ''' <returns>Returns the path to the proper Opensim.ini prototype.</returns>
    Public Function GetOpensimProto() As String

        Select Case Settings.ServerType
            Case "Robust"
                Settings.LoadIni(PropOpensimBinPath & "bin\Opensim.proto", ";")
                Return PropOpensimBinPath & "bin\Opensim.proto"
            Case "Region"
                Settings.LoadIni(PropOpensimBinPath & "bin\OpensimRegion.proto", ";")
                Return PropOpensimBinPath & "bin\OpensimRegion.proto"
            Case "OsGrid"
                Settings.LoadIni(PropOpensimBinPath & "bin\OpensimOsGrid.proto", ";")
                Return PropOpensimBinPath & "bin\OpensimOsGrid.proto"
            Case "Metro"
                Settings.LoadIni(PropOpensimBinPath & "bin\OpensimMetro.proto", ";")
                Return PropOpensimBinPath & "bin\OpensimMetro.proto"
        End Select
        ' just in case...
        Settings.LoadIni(PropOpensimBinPath & "bin\Opensim.proto", ";")
        Return PropOpensimBinPath & "bin\Opensim.proto"

    End Function

    Public Function GetPostData() As String

        Dim UPnp As String = "Fail"
        If Settings.UPnpDiag Then
            UPnp = "Pass"
        End If
        Dim Loopb As String = "Fail"
        If Settings.LoopBackDiag Then
            Loopb = "Pass"
        End If

        Dim Grid As String = "Grid"

        Dim data As String = "&MachineID=" & Settings.MachineID() _
        & "&FriendlyName=" & WebUtility.UrlEncode(Settings.SimName) _
        & "&V=" & WebUtility.UrlEncode(Convert.ToString(PropMyVersion, Globalization.CultureInfo.InvariantCulture)) _
        & "&OV=" & WebUtility.UrlEncode(CStr(PropSimVersion)) _
        & "&uPnp=" & CStr(UPnp) _
        & "&Loop=" & CStr(Loopb) _
        & "&Type=" & CStr(Grid) _
        & "&Ver=" & CStr(PropUseIcons) _
        & "&isPublic=" & CStr(Settings.GDPR()) _
        & "&r=" & RandomNumber.Random()
        Return data

    End Function

    Public Sub HelpOnce(Webpage As String)

        newScreenPosition = New ScreenPos(Webpage)
        If Not newScreenPosition.Exists() Then
            ' Set the new form's desktop location so it appears below and to the right of the current form.
#Disable Warning CA2000 ' Dispose objects before losing scope
            Dim FormHelp As New FormHelp
#Enable Warning CA2000 ' Dispose objects before losing scope
            FormHelp.Activate()
            FormHelp.Visible = True
            FormHelp.Init(Webpage)
            Try
                FormHelp.Select()
                FormHelp.BringToFront()
            Catch
            End Try

        End If

    End Sub

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
        'Application.doevents()
        StopApache(False) ' do not stop if a service
        'Application.doevents()
        Dim n As Integer = PropRegionClass.RegionCount()

        Dim TotalRunningRegions As Integer

        For Each RegionUUID As String In PropRegionClass.RegionUUIDs
            If PropRegionClass.IsBooted(RegionUUID) Then
                TotalRunningRegions += 1
            End If
            'Application.doevents()
        Next
        Log(My.Resources.Info, "Total Enabled Regions=" & CStr(TotalRunningRegions))

        For Each RegionUUID As String In PropRegionClass.RegionUUIDs
            If PropOpensimIsRunning() And PropRegionClass.RegionEnabled(RegionUUID) And
            Not (PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.RecyclingDown _
            Or PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.ShuttingDown _
            Or PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.Stopped) Then
                Print(My.Resources.Stopping_word & " " & PropRegionClass.RegionName(RegionUUID))
                SequentialPause()
                ConsoleCommand(RegionUUID, "q{ENTER}" & vbCrLf)
                PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.ShuttingDown
                PropRegionClass.Timer(RegionUUID) = RegionMaker.REGIONTIMER.Stopped
                PropUpdateView = True ' make form refresh
            End If
            'Application.doevents()
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
                    If (Not PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.Stopped) And PropRegionClass.RegionEnabled(RegionUUID) Then
                        If CheckPort(Settings.PrivateURL, PropRegionClass.GroupPort(RegionUUID)) Then
                            CountisRunning += 1
                        Else
                            StopGroup(PropRegionClass.GroupName(RegionUUID))
                            PropUpdateView = True ' make form refresh
                        End If
                    End If
                    'Application.doevents()
                    If CountisRunning = 0 Then Exit For
                Next

                If CountisRunning = 1 And last > 1 Then
                    Print(My.Resources.One_region)
                    last = 1
                    PropUpdateView = True ' make form refresh
                Else
                    If CountisRunning <> last Then
                        Print(CStr(CountisRunning) & " " & My.Resources.Regions_Are_Running)
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

        StopAllRegions()

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
            'Application.doevents()
        Next
        If UUID.Length = 0 Then
            MsgBox(My.Resources.No_Regions_Ready, vbInformation, My.Resources.Info)
            Return False
        End If

        Dim Path As String = InputBox(My.Resources.Folder_To_Save_To_word & " (""/"",  ""/Objects/Somefolder..."")", "Folder Name", "/Objects")

        Dim user = InputBox(My.Resources.Enter_1_2)
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

    Public Function LoadOARContent(thing As String) As Boolean

        If Not PropOpensimIsRunning() Then
            Print(My.Resources.Not_Running)
            Return False
        End If

        Dim region = ChooseRegion(True)
        If region.Length = 0 Then Return False

        Dim offset = VarChooser(region)
        If offset.Length = 0 Then Return False

        Dim backMeUp = MsgBox(My.Resources.Make_a_backup_word, vbYesNoCancel, My.Resources.Backup_word)
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
                        ConsoleCommand(RegionUUID, "alert " & My.Resources.CPU_Intensive & "{Enter}" & vbCrLf)
                        ConsoleCommand(RegionUUID, "save oar " & BackupPath() & "Backup_" & DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss", Globalization.CultureInfo.InvariantCulture) & ".oar" & """" & "{ENTER}" & vbCrLf)
                    End If
                    ConsoleCommand(RegionUUID, "alert " & My.Resources.New_Content & "{ENTER}" & vbCrLf)

                    Dim ForceParcel As String = ""
                    If PropForceParcel() Then ForceParcel = " --force-parcels "
                    Dim ForceTerrain As String = ""
                    If PropForceTerrain Then ForceTerrain = " --force-terrain "
                    Dim ForceMerge As String = ""
                    If PropForceMerge Then ForceMerge = " --merge "
                    Dim UserName As String = ""
                    If PropUserName.Length > 0 Then UserName = " --default-user " & """" & PropUserName & """" & " "

                    ConsoleCommand(RegionUUID, "load oar " & UserName & ForceMerge & ForceTerrain & ForceParcel & offset & """" & thing & """" & "{ENTER}" & vbCrLf)
                    ConsoleCommand(RegionUUID, "alert " & My.Resources.New_is_Done & "{ENTER}" & vbCrLf)
                    ConsoleCommand(RegionUUID, "generate map {ENTER}" & vbCrLf)
                    once = True
                End If
#Disable Warning CA1031 ' Do not catch general exception types
            Catch ex As Exception
#Enable Warning CA1031 ' Do not catch general exception types
                ErrorLog(My.Resources.Error_word & ":" & ex.Message)
            End Try
            'Application.doevents()
        Next

        Me.Focus()
        Return True

    End Function

    Public Sub LoadRegionsStatsBar()

        SimulatorStatsToolStripMenuItem.DropDownItems.Clear()
        SimulatorStatsToolStripMenuItem.Visible = False

        If PropRegionClass Is Nothing Then Return

        For Each RegionUUID As String In PropRegionClass.RegionUUIDs

            Dim Menu As New ToolStripMenuItem With {
                .Text = PropRegionClass.RegionName(RegionUUID),
                .ToolTipText = My.Resources.Click_to_View_this_word & " " & PropRegionClass.RegionName(RegionUUID),
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

    ''' <summary>Log(string) to Outworldz.log</summary>
    ''' <param name="message"></param>
    Public Sub Log(category As String, message As String)
        Logger(category, message, "Outworldz")
    End Sub

    Public Sub Logger(category As String, message As String, file As String)
        Try
            Using outputFile As New StreamWriter(PropMyFolder & "\OutworldzFiles\" & file & ".log", True)
                outputFile.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", Globalization.CultureInfo.InvariantCulture) & ":" & category & ":" & message)
                Diagnostics.Debug.Print(message)
            End Using
        Catch ex As IOException
        Catch ex As UnauthorizedAccessException
        Catch ex As ArgumentException
        Catch ex As System.Security.SecurityException
        Catch ex As ObjectDisposedException
        End Try
    End Sub

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
                PropMyUPnpMap.Add(PropMyUPnpMap.LocalIP, CType(Settings.SCPortBase, Integer), UPnp.MyProtocol.TCP, "Icecast TCP Public " & Settings.SCPortBase.ToString(Globalization.CultureInfo.InvariantCulture))
                'Application.doevents()
                If PropMyUPnpMap.Exists(Convert.ToInt16(Settings.SCPortBase1), UPnp.MyProtocol.TCP) Then
                    PropMyUPnpMap.Remove(Convert.ToInt16(Settings.SCPortBase1), UPnp.MyProtocol.TCP)
                End If
                PropMyUPnpMap.Add(PropMyUPnpMap.LocalIP, CType(Settings.SCPortBase1, Integer), UPnp.MyProtocol.TCP, "Icecast1 TCP Public " & Settings.SCPortBase.ToString(Globalization.CultureInfo.InvariantCulture))
                Print(My.Resources.Icecast_is_Set & ":" & Settings.SCPortBase1.ToString(Globalization.CultureInfo.InvariantCulture))
            End If

            If Settings.ApachePort > 0 Then
                If PropMyUPnpMap.Exists(Settings.ApachePort, UPnp.MyProtocol.TCP) Then
                    PropMyUPnpMap.Remove(Settings.ApachePort, UPnp.MyProtocol.TCP)
                End If
                PropMyUPnpMap.Add(PropMyUPnpMap.LocalIP, Settings.ApachePort, UPnp.MyProtocol.TCP, "Icecast1 TCP Public " & Settings.SCPortBase.ToString(Globalization.CultureInfo.InvariantCulture))
                Print(My.Resources.Apache_is_Set & ":" & Settings.ApachePort.ToString(Globalization.CultureInfo.InvariantCulture))
            End If

            ' 8002 for TCP and UDP
            If PropMyUPnpMap.Exists(Convert.ToInt16(Settings.HttpPort, Globalization.CultureInfo.InvariantCulture), UPnp.MyProtocol.TCP) Then
                PropMyUPnpMap.Remove(Convert.ToInt16(Settings.HttpPort, Globalization.CultureInfo.InvariantCulture), UPnp.MyProtocol.TCP)
            End If
            PropMyUPnpMap.Add(PropMyUPnpMap.LocalIP, Convert.ToInt16(Settings.HttpPort, Globalization.CultureInfo.InvariantCulture), UPnp.MyProtocol.TCP, "Opensim TCP Grid " & Settings.HttpPort)
            Print(My.Resources.Grid_TCP_is_set & ":" & Settings.HttpPort.ToString(Globalization.CultureInfo.InvariantCulture))

            If PropMyUPnpMap.Exists(Convert.ToInt16(Settings.HttpPort, Globalization.CultureInfo.InvariantCulture), UPnp.MyProtocol.UDP) Then
                PropMyUPnpMap.Remove(Convert.ToInt16(Settings.HttpPort, Globalization.CultureInfo.InvariantCulture), UPnp.MyProtocol.UDP)
            End If
            PropMyUPnpMap.Add(PropMyUPnpMap.LocalIP, Convert.ToInt16(Settings.HttpPort, Globalization.CultureInfo.InvariantCulture), UPnp.MyProtocol.UDP, "Opensim UDP Grid " & Settings.HttpPort)
            Print(My.Resources.Grid_UDP_is_set & ":" & Settings.HttpPort.ToString(Globalization.CultureInfo.InvariantCulture))

            For Each RegionUUID As String In PropRegionClass.RegionUUIDs
                Dim R As Integer = PropRegionClass.RegionPort(RegionUUID)
                'Application.doevents()

                If PropMyUPnpMap.Exists(R, UPnp.MyProtocol.UDP) Then
                    PropMyUPnpMap.Remove(R, UPnp.MyProtocol.UDP)
                    'Application.doevents()
                End If

                PropMyUPnpMap.Add(PropMyUPnpMap.LocalIP, R, UPnp.MyProtocol.UDP, "Opensim UDP Region " & PropRegionClass.RegionName(RegionUUID) & " ")
                Print(PropRegionClass.RegionName(RegionUUID) & " UDP:" & R.ToString(Globalization.CultureInfo.InvariantCulture))
                'Application.doevents()
                If PropMyUPnpMap.Exists(R, UPnp.MyProtocol.TCP) Then
                    PropMyUPnpMap.Remove(R, UPnp.MyProtocol.TCP)
                    'Application.doevents()
                End If
                PropMyUPnpMap.Add(PropMyUPnpMap.LocalIP, R, UPnp.MyProtocol.TCP, "Opensim TCP Region " & PropRegionClass.RegionName(RegionUUID) & " ")
                Print(PropRegionClass.RegionName(RegionUUID) & " TCP:" & R.ToString(Globalization.CultureInfo.InvariantCulture))

            Next

#Disable Warning CA1031 ' Do not catch general exception types
        Catch e As Exception
#Enable Warning CA1031 ' Do not catch general exception types
            Log("UPnP", "UPnP Exception caught:  " & e.Message)
            Return False
        End Try
        Return True 'successfully added

    End Function

    Public Function Opensimproto(RegionUUID As String) As Boolean

        Dim regionName = PropRegionClass.RegionName(RegionUUID)
        Dim pathname = PropRegionClass.IniPath(RegionUUID)

        If Settings.LoadIni(GetOpensimProto(), ";") Then Return True

        Settings.SetIni("Const", "BaseHostname", Settings.BaseHostName)

        Settings.SetIni("Const", "PublicPort", CStr(Settings.HttpPort)) ' 8002
        Settings.SetIni("Const", "PrivURL", "http://" & CStr(Settings.PrivateURL)) ' local IP
        Settings.SetIni("Const", "http_listener_port", CStr(PropRegionClass.RegionPort(RegionUUID))) ' varies with region

        ' set new Min Timer Interval for how fast a script can go. Can be set in region files as a float, or nothing
        Dim Xtime As Double = 1 / 11   '1/11 of a second is as fast as she can go
        If PropRegionClass.MinTimerInterval(RegionUUID).Length > 0 Then
            If Not Double.TryParse(PropRegionClass.MinTimerInterval(RegionUUID), Xtime) Then
                Xtime = 1.0 / 11.0
            End If
        End If
        Settings.SetIni("XEngine", "MinTimerInterval", Convert.ToString(Xtime, Globalization.CultureInfo.InvariantCulture))
        Settings.SetIni("YEngine", "MinTimerInterval", Convert.ToString(Xtime, Globalization.CultureInfo.InvariantCulture))

        Dim name = PropRegionClass.RegionName(RegionUUID)

        ' save the http listener port away for the group
        PropRegionClass.GroupPort(RegionUUID) = PropRegionClass.RegionPort(RegionUUID)

        Settings.SetIni("Const", "PrivatePort", CStr(Settings.PrivatePort)) '8003
        Settings.SetIni("Const", "RegionFolderName", CStr(PropRegionClass.GroupName(RegionUUID)))
        Settings.SaveINI(System.Text.Encoding.UTF8)

        Try
            My.Computer.FileSystem.CopyFile(GetOpensimProto(), pathname & "Opensim.ini", True)
        Catch ex As FileNotFoundException
        Catch ex As PathTooLongException
        Catch ex As IOException
        Catch ex As UnauthorizedAccessException
        Catch ex As ArgumentNullException
        Catch ex As ArgumentException
        Catch ex As InvalidOperationException
        Catch ex As NotSupportedException
        Catch ex As System.Security.SecurityException
        End Try

        Return False

    End Function

    Public Sub Print(Value As String)

        Log(My.Resources.Info, Value)
        TextBox1.Text = TextBox1.Text & vbCrLf & Value
        Trim()

    End Sub

    Public Function RegisterDNS(force As Boolean) As Boolean

        If Settings.DNSName.Length = 0 Then
            Return True
        End If


        If IPCheck.IsPrivateIP(Settings.DNSName) Then
            Return True
        End If

        If _DNS_is_registered And Not force Then Return True
        _DNS_is_registered = True

        Dim client As New WebClient
        Dim Checkname As String

        Try
            Checkname = client.DownloadString("http://outworldz.net/dns.plx?GridName=" & Settings.DNSName & GetPostData())
        Catch ex As ArgumentNullException
            ErrorLog("Warn: Cannot check the DNS Name " & ex.Message)
            Return False
        Catch ex As Net.WebException
            ErrorLog("Warn: Cannot check the DNS Name " & ex.Message)
            Return False
        Catch ex As NotSupportedException
            ErrorLog("Warn: Cannot check the DNS Name " & ex.Message)
            Return False
        Finally
            client.Dispose()
        End Try

        If Checkname = "UPDATED" Then Return True
        Return False

    End Function

    Public Function RegisterName(name As String) As String

        Dim Checkname As String = String.Empty
        If Settings.ServerType <> "Robust" Then
            Return name
        End If
        Dim client As New WebClient ' download client for web pages
        Try
            Checkname = client.DownloadString("http://outworldz.net/dns.plx/?GridName=" & name & GetPostData())
        Catch ex As ArgumentNullException
            ErrorLog("Warn: Cannot register the DNS Name " & ex.Message)
            Return ""
        Catch ex As Net.WebException
            ErrorLog("Warn: Cannot register the DNS Name " & ex.Message)
            Return ""
        Catch ex As NotSupportedException
            ErrorLog("Warn: Cannot register the DNS Name " & ex.Message)
            Return ""
        Finally
            client.Dispose()
        End Try
        If Checkname = "UPDATED" Then
            Return name
        End If
        If Checkname = "NAK" Then
            MsgBox(My.Resources.DDNS_In_Use)
        End If
        Return ""

    End Function

    Public Function SaveIceCast() As Boolean

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
                                  "<admin-password>" & Settings.SCPassword & "</admin-password>" & vbCrLf +
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

        Using outputFile As New StreamWriter(PropMyFolder & "\Outworldzfiles\Icecast\icecast_run.xml", False)
            outputFile.WriteLine(icecast)
        End Using

        Return False

    End Function

    Public Sub SendMsg(msg As String)
        Dim hwnd As IntPtr
        If PropOpensimIsRunning() Then
            For Each RegionUUID As String In PropRegionClass.RegionUUIDs
                If PropRegionClass.IsBooted(RegionUUID) Then
                    ConsoleCommand(RegionUUID, "set log level " & msg & "{ENTER}" & vbCrLf)
                    hwnd = GetHwnd(PropRegionClass.GroupName(RegionUUID))
                    Form1.ShowDOSWindow(hwnd, Form1.SHOWWINDOWENUM.SWMINIMIZE)
                End If
            Next
            ConsoleCommand("Robust", "set log level " & msg & "{ENTER}" & vbCrLf)
            Form1.ShowDOSWindow(GetHwnd("Robust"), SHOWWINDOWENUM.SWMINIMIZE)
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

                    Dim ctr = 600 ' 1 minute max to start a region
                    Dim WaitForIt = True
                    While WaitForIt
                        Sleep(100)
                        If PropRegionClass.RegionEnabled(RegionUUID) _
                            And Not PropAborting _
                            And (PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.RecyclingUp Or
                                PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.ShuttingDown Or
                                PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.RecyclingDown Or
                                PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.Booting) Then
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
                If CPUAverageSpeed < PropCPUMAX Then
                    WaitForIt = False
                End If
                ctr -= 1
                If ctr <= 0 Then WaitForIt = False
            End While

        End If

    End Sub

    Public Function SetPublicIP() As Boolean

        ' LAN USE
        If Settings.EnableHypergrid Then

            If Settings.DNSName.Length > 0 Then
                Settings.PublicIP = Settings.DNSName()
                Settings.SaveSettings()
                Print(My.Resources.Setup_Network)
                Dim ret = RegisterDNS(False)
                Return ret
            Else
                Settings.PublicIP = PropMyUPnpMap.LocalIP
                Print(My.Resources.Setup_Network)
                Dim ret = RegisterDNS(False)
                Settings.SaveSettings()
                Return ret
            End If

        End If

        ' HG USE

        If Not IPCheck.IsPrivateIP(Settings.DNSName) Then
            Print(My.Resources.Public_IP_Setup_Word)
            Settings.PublicIP = Settings.DNSName
            Settings.SaveSettings()
#Disable Warning CA1308 ' Normalize strings to uppercase
            Dim x = Settings.PublicIP.ToLower(Globalization.CultureInfo.InvariantCulture)
#Enable Warning CA1308 ' Normalize strings to uppercase
            If x.Contains("outworldz.net") Then
                Print(My.Resources.DynDNS & " http://" & Settings.PublicIP & ":" & Settings.HttpPort)
            End If

            If RegisterDNS(False) Then
                Return True
            End If

        End If

        If Settings.PublicIP = "localhost" Or Settings.PublicIP = "127.0.0.1" Then
            RegisterDNS(False)
            Return True
        End If

        Log(My.Resources.Info, "Public IP=" & Settings.PublicIP)
        TestPublicLoopback()
        If Settings.DiagFailed Then

            Using client As New WebClient ' download client for web pages
                Try
                    ' Set Public IP
                    Settings.PublicIP = client.DownloadString("http://api.ipify.org/?r=" & RandomNumber.Random())
                Catch ex As ArgumentNullException
                    ErrorLog(My.Resources.Wrong & " api.ipify.org")
                    Settings.DiagFailed = True
                Catch ex As WebException
                    ErrorLog(My.Resources.Wrong & " api.ipify.org")
                    Settings.DiagFailed = True
                Catch ex As NotSupportedException
                    ErrorLog(My.Resources.Wrong & " api.ipify.org")
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

    ''' <summary>
    ''' SetWindowTextCall is here to wrap the SetWindowtext API call. This call fails when there is no hwnd as Windows takes its sweet time to get that. Also, may fail to write the title. It has a
    ''' timer to make sure we do not get stuck
    ''' </summary>
    ''' <param name="hwnd">Handle to the window to change the text on</param>
    ''' <param name="windowName">the name of the Window</param>
    Public Function SetWindowTextCall(myProcess As Process, windowName As String) As Boolean

        If myProcess Is Nothing Then
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
        Catch ex As PlatformNotSupportedException
            Return False
        Catch ex As InvalidOperationException
            Return False
        Catch ex As NotSupportedException
            Return False
        End Try

        WindowCounter = 0

        Dim hwnd As IntPtr = myProcess.MainWindowHandle
        Dim status = False
        While status = False
            Sleep(100)
            SetWindowText(hwnd, windowName)
            status = NativeMethods.SetWindowText(hwnd, windowName)
            WindowCounter += 1
            If WindowCounter > 50 Then '  5 seconds
                ErrorLog("Cannot get handle for " & windowName)
                Exit While
            End If
            'Application.doevents()
        End While
        Return True

    End Function

    Public Sub ShowRegionMap()

        Dim region = ChooseRegion(False)
        If region.Length = 0 Then Return

        VarChooser(region, False, False)

    End Sub

    Public Function StartApache() As Boolean

        If Settings.SearchEnabled Then
            Dim SiteMapContents = "<?xml version=""1.0"" encoding=""UTF-8""?>" & vbCrLf
            SiteMapContents += "<urlset xmlns=""http://www.sitemaps.org/schemas/sitemap/0.0909"">" & vbCrLf
            SiteMapContents += "<url>" & vbCrLf
            SiteMapContents += "<loc>http://" & Settings.PublicIP & ":" & Convert.ToString(Settings.ApachePort, Globalization.CultureInfo.InvariantCulture) & "/" & "</loc>" & vbCrLf
            SiteMapContents += "<changefreq>daily</changefreq>" & vbCrLf
            SiteMapContents += "<priority>1.0</priority>" & vbCrLf
            SiteMapContents += "</url>" & vbCrLf
            SiteMapContents += "</urlset>" & vbCrLf

            Using outputFile As New StreamWriter(PropMyFolder & "\OutworldzFiles\Apache\htdocs\Sitemap.xml", False)
                outputFile.WriteLine(SiteMapContents)
            End Using
        End If

        If Not Settings.ApacheEnable Then
            ApachePictureBox.Image = My.Resources.nav_plain_blue
            ToolTip1.SetToolTip(ApachePictureBox, My.Resources.Disabled_word)
            Print(My.Resources.Apache_Disabled)
            Return True
        End If

        ApachePictureBox.Image = My.Resources.navigate_open
        ToolTip1.SetToolTip(ApachePictureBox, My.Resources.Starting_word)
        'Application.doevents()

        If Settings.ApachePort = 80 Then
            ApacheProcess.StartInfo.UseShellExecute = True ' so we can redirect streams
            ApacheProcess.StartInfo.FileName = "net"
            ApacheProcess.StartInfo.CreateNoWindow = True
            ApacheProcess.StartInfo.Arguments = "stop W3SVC"
            ApacheProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
            Try
                ApacheProcess.Start()
            Catch ex As InvalidOperationException
            Catch ex As System.ComponentModel.Win32Exception
            End Try
            'Application.doevents()
            ApacheProcess.WaitForExit()
        End If

        Print(My.Resources.Checking_Apache_service_word)
        ' Stop MSFT server if we are on port 80 and enabled

        Dim Running = CheckPort(Settings.PrivateURL, CType(Settings.ApachePort, Integer))
        If Running Then
            Print(My.Resources.Apache_running)
            ApachePictureBox.Image = My.Resources.nav_plain_green
            ToolTip1.SetToolTip(ApachePictureBox, My.Resources.Apache_running)
            PropApacheExited = False
            Return True
        End If
        'Application.doevents()

        If Settings.ApacheService Then
            PropApacheUninstalling = True
            ApacheProcess.StartInfo.FileName = "sc"
            ApacheProcess.StartInfo.Arguments = "stop " & "ApacheHTTPServer"
            Try
                ApacheProcess.Start()
            Catch ex As InvalidOperationException
            Catch ex As System.ComponentModel.Win32Exception
            End Try
            'Application.doevents()
            ApacheProcess.WaitForExit()

            ApacheProcess.StartInfo.Arguments = "stop " & """" & "Apache HTTP Server" & """"
            Try
                ApacheProcess.Start()
            Catch ex As InvalidOperationException
            Catch ex As System.ComponentModel.Win32Exception
            End Try
            'Application.doevents()
            ApacheProcess.WaitForExit()

            ApacheProcess.StartInfo.FileName = "sc"
            ApacheProcess.StartInfo.Arguments = " delete  " & """" & "Apache HTTP Server" & """"
            Try
                ApacheProcess.Start()
            Catch ex As InvalidOperationException
            Catch ex As System.ComponentModel.Win32Exception
            End Try
            'Application.doevents()
            ApacheProcess.WaitForExit()

            ApacheProcess.StartInfo.Arguments = " delete  " & "ApacheHTTPServer"
            Try
                ApacheProcess.Start()
            Catch ex As InvalidOperationException
            Catch ex As System.ComponentModel.Win32Exception
            End Try
            'Application.doevents()
            ApacheProcess.WaitForExit()

            Sleep(3000)

            Using ApacheProcess As New Process With {
                    .EnableRaisingEvents = False
                }
                ApacheProcess.StartInfo.UseShellExecute = True ' so we can redirect streams
                ApacheProcess.StartInfo.FileName = PropMyFolder & "\Outworldzfiles\Apache\bin\httpd.exe"
                ApacheProcess.StartInfo.Arguments = "-k install -n " & """" & "ApacheHTTPServer" & """"
                ApacheProcess.StartInfo.CreateNoWindow = True
                ApacheProcess.StartInfo.WorkingDirectory = PropMyFolder & "\Outworldzfiles\Apache\bin\"
                ApacheProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
                Try
                    ApacheProcess.Start()
                Catch ex As InvalidOperationException
                    Print(My.Resources.ApacheFailed & ":" & ex.Message)
                Catch ex As System.ComponentModel.Win32Exception
                    Print(My.Resources.ApacheFailed & ":" & ex.Message)
                End Try
                'Application.doevents()
                ApacheProcess.WaitForExit()

                If ApacheProcess.ExitCode <> 0 Then
                    Print(My.Resources.ApacheFailed)
                Else
                    PropApacheUninstalling = False ' installed now, trap errors
                End If
                Sleep(100)
                Print(My.Resources.Apache_starting)
                ApacheProcess.StartInfo.FileName = "net"
                ApacheProcess.StartInfo.Arguments = "start ApacheHTTPServer"

                Try
                    ApacheProcess.Start()
                Catch ex As InvalidOperationException
                    Print(My.Resources.Apache_Failed & ":" & ex.Message)
                Catch ex As System.ComponentModel.Win32Exception
                    Print(My.Resources.Apache_Failed & ":" & ex.Message)
                End Try
                'Application.doevents()
                ApacheProcess.WaitForExit()

                If ApacheProcess.ExitCode <> 0 Then
                    Print(My.Resources.Apache_Failed & ":" & CStr(ApacheProcess.ExitCode))
                Else
                    ApachePictureBox.Image = My.Resources.nav_plain_green
                    ToolTip1.SetToolTip(ApachePictureBox, My.Resources.Apache_running)
                    'Application.doevents()
                End If
            End Using
        Else
            ' Start Apache manually
            Using ApacheProcess As New Process With {
                    .EnableRaisingEvents = True
                }
                ApacheProcess.StartInfo.UseShellExecute = True ' so we can redirect streams
                ApacheProcess.StartInfo.FileName = PropMyFolder & "\Outworldzfiles\Apache\bin\httpd.exe"
                ApacheProcess.StartInfo.CreateNoWindow = True
                ApacheProcess.StartInfo.WorkingDirectory = PropMyFolder & "\Outworldzfiles\Apache\bin\"
                ApacheProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
                ApacheProcess.StartInfo.Arguments = ""
                Try
                    ApacheProcess.Start()
                Catch ex As InvalidOperationException
                    Print(My.Resources.Apache_Failed & ":" & ex.Message)
                    ApachePictureBox.Image = My.Resources.nav_plain_red
                    ToolTip1.SetToolTip(ApachePictureBox, My.Resources.Apache_Failed)
                    'Application.doevents()
                    Return False
                Catch ex As System.ComponentModel.Win32Exception
                    Print(My.Resources.Apache_Failed & ":" & ex.Message)
                    ApachePictureBox.Image = My.Resources.nav_plain_red
                    ToolTip1.SetToolTip(ApachePictureBox, My.Resources.Apache_Failed)
                    'Application.doevents()
                    Return False
                End Try

                'Application.doevents()

                ' wait for PID
                Dim ApachePID = WaitForPID(ApacheProcess)
                If ApachePID = 0 Then
                    ApachePictureBox.Image = My.Resources.error_icon
                    ToolTip1.SetToolTip(ApachePictureBox, My.Resources.Apache_Failed)
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
                        Return False
                    End If
                    'Application.doevents()

                    Dim isRunning = CheckPort(Settings.PrivateURL, CType(Settings.ApachePort, Integer))
                    If isRunning Then
                        Print(My.Resources.Apache_running)
                        ApachePictureBox.Image = My.Resources.nav_plain_green
                        ToolTip1.SetToolTip(ApachePictureBox, My.Resources.Apache_running)
                        PropApacheExited = False
                        'Application.doevents()
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
            IceCastPicturebox.Image = My.Resources.nav_plain_blue
            ToolTip1.SetToolTip(IceCastPicturebox, My.Resources.IceCast_disabled)
            Return True
        End If

        Dim IceCastRunning = CheckPort(Settings.PublicIP, Settings.SCPortBase)
        'Application.doevents()

        If IceCastRunning Then
            IceCastPicturebox.Image = My.Resources.nav_plain_green
            ToolTip1.SetToolTip(IceCastPicturebox, My.Resources.Icecast_Started)
            Return True
        End If

        IceCastPicturebox.Image = My.Resources.navigate_open

        FileStuff.DeleteFile(PropMyFolder & "\Outworldzfiles\Icecast\log\access.log")
        FileStuff.DeleteFile(PropMyFolder & "\Outworldzfiles\Icecast\log\error.log")

        PropIcecastProcID = 0
        Print(My.Resources.Icecast_starting)
        IcecastProcess.EnableRaisingEvents = True
        IcecastProcess.StartInfo.UseShellExecute = True ' so we can redirect streams
        IcecastProcess.StartInfo.FileName = PropMyFolder & "\Outworldzfiles\icecast\icecast.bat"
        IcecastProcess.StartInfo.CreateNoWindow = False
        IcecastProcess.StartInfo.WorkingDirectory = PropMyFolder & "\Outworldzfiles\icecast"

        If Settings.ConsoleShow Then
            IcecastProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal
        Else
            IcecastProcess.StartInfo.WindowStyle = ProcessWindowStyle.Minimized
        End If

        Try
            IcecastProcess.Start()
        Catch ex As InvalidOperationException
            Print(My.Resources.Icecast_failed & ":" & ex.Message)
            IceCastPicturebox.Image = My.Resources.nav_plain_red
            ToolTip1.SetToolTip(IceCastPicturebox, My.Resources.Icecast_failed)
            Return False
        Catch ex As System.ComponentModel.Win32Exception
            Print(My.Resources.Icecast_failed & ":" & ex.Message)
            IceCastPicturebox.Image = My.Resources.nav_plain_red
            ToolTip1.SetToolTip(IceCastPicturebox, My.Resources.Icecast_failed)
            Return False
        End Try
        'Application.doevents()

        PropIcecastProcID = WaitForPID(IcecastProcess)
        If PropIcecastProcID = 0 Then
            IceCastPicturebox.Image = My.Resources.error_icon
            ToolTip1.SetToolTip(IceCastPicturebox, My.Resources.Icecast_failed)
            Return False
        End If

        SetWindowTextCall(IcecastProcess, "Icecast")
        ShowDOSWindow(IcecastProcess.MainWindowHandle, SHOWWINDOWENUM.SWMINIMIZE)

        IceCastPicturebox.Image = My.Resources.nav_plain_green
        ToolTip1.SetToolTip(IceCastPicturebox, My.Resources.Icecast_Started)

        PropIceCastExited = False
        Return True

    End Function

    Public Function StartMySQL() As Boolean

        If MysqlInterface.IsMySqlRunning() Then
            MysqlInterface.IsRunning = True
            MysqlPictureBox.Image = My.Resources.nav_plain_green
            ToolTip1.SetToolTip(MysqlPictureBox, My.Resources.Mysql_is_Running)
            PropMysqlExited = False
            Return True
        End If

        ' Build data folder if it does not exist
        MakeMysql()

        MysqlPictureBox.Image = My.Resources.navigate_open
        ToolTip1.SetToolTip(MysqlPictureBox, My.Resources.Stopped_word)
        'Application.doevents()
        ' Start MySql in background.

        Print(My.Resources.Mysql_Starting)

        ' SAVE INI file
        If Settings.LoadIni(PropMyFolder & "\OutworldzFiles\mysql\my.ini", "#") Then Return True
        Settings.SetIni("mysqld", "basedir", """" & PropCurSlashDir & "/OutworldzFiles/Mysql" & """")
        Settings.SetIni("mysqld", "datadir", """" & PropCurSlashDir & "/OutworldzFiles/Mysql/Data" & """")
        Settings.SetIni("mysqld", "port", CStr(Settings.MySqlRobustDBPort))
        Settings.SetIni("client", "port", CStr(Settings.MySqlRobustDBPort))
        Settings.SaveINI(System.Text.Encoding.ASCII)

        ' create test program slants the other way:
        Dim testProgram As String = PropMyFolder & "\OutworldzFiles\Mysql\bin\StartManually.bat"
        FileStuff.DeleteFile(testProgram)

        Try
            Using outputFile As New StreamWriter(testProgram, True)
                outputFile.WriteLine("@REM A program to run Mysql manually for troubleshooting." & vbCrLf _
                                 & "mysqld.exe --defaults-file=" & """" & PropCurSlashDir & "/OutworldzFiles/mysql/my.ini" & """")
            End Using
        Catch ex As IOException
        Catch ex As UnauthorizedAccessException
        Catch ex As ArgumentException
        Catch ex As System.Security.SecurityException
        Catch ex As ObjectDisposedException

        End Try

        CreateService()
        CreateStopMySql()

        'Application.doevents()
        ' Mysql was not running, so lets start it up.
        Dim pi As ProcessStartInfo = New ProcessStartInfo With {
            .Arguments = "--defaults-file=" & """" & PropCurSlashDir & "/OutworldzFiles/mysql/my.ini" & """",
            .WindowStyle = ProcessWindowStyle.Hidden,
            .FileName = """" & PropMyFolder & "\OutworldzFiles\mysql\bin\mysqld.exe" & """"
        }
        ProcessMySql.StartInfo = pi
        ProcessMySql.EnableRaisingEvents = True
        Try
            ProcessMySql.Start()
            MysqlInterface.IsRunning = True
        Catch ex As ObjectDisposedException
        Catch ex As InvalidOperationException
        Catch ex As System.ComponentModel.Win32Exception
        End Try

        PropOpensimIsRunning = False

        ' wait for MySql to come up
        Dim MysqlOk As Boolean
        Dim ctr As Integer = 0
        While Not MysqlOk And Not PropAborting

            'Application.doevents()

            Dim MysqlLog As String = PropMyFolder & "\OutworldzFiles\mysql\data"
            If ctr = 60 Then ' about 60 seconds when it fails

                Dim yesno = MsgBox(My.Resources.Mysql_Failed, vbYesNo, My.Resources.Error_word)
                If (yesno = vbYes) Then
                    Dim files As Array = Nothing
                    Try
                        files = Directory.GetFiles(MysqlLog, "*.err", SearchOption.TopDirectoryOnly)
                    Catch ex As ArgumentException
                    Catch ex As UnauthorizedAccessException
                    Catch ex As DirectoryNotFoundException
                    Catch ex As PathTooLongException
                    Catch ex As IOException
                    End Try

                    For Each FileName As String In files
                        Try
                            System.Diagnostics.Process.Start(PropMyFolder & "\baretail.exe", """" & FileName & """")
                        Catch ex As InvalidOperationException
                        Catch ex As System.ComponentModel.Win32Exception
                        End Try
                        'Application.doevents()
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
        MysqlPictureBox.Image = My.Resources.nav_plain_green
        ToolTip1.SetToolTip(MysqlPictureBox, My.Resources.Mysql_is_Running)
        PropMysqlExited = False

        Return True

    End Function

    Public Function StartOpensimulator() As Boolean

        PropExitHandlerIsBusy = False
        PropAborting = False
        Timer1.Start() 'Timer starts functioning

        If Not StartRobust() Then Return False

        ' Boot them up
        For Each RegionUUID As String In PropRegionClass.RegionUUIDs()
            If PropRegionClass.RegionEnabled(RegionUUID) Then
                Boot(PropRegionClass, PropRegionClass.RegionName(RegionUUID))
                'Application.doevents()
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
        If CheckRobust() Then
            RobustPictureBox.Image = My.Resources.nav_plain_green
            ToolTip1.SetToolTip(RobustPictureBox, My.Resources.Robust_running)
            For Each p In Process.GetProcesses
                If p.MainWindowTitle = "Robust" Then
                    PropRobustProcID = p.Id
                    Log(My.Resources.Info, My.Resources.DosBoxRunning)
                    Return True
                End If
                'Application.doevents()
            Next
        End If
        RobustPictureBox.Image = My.Resources.navigate_open

        ToolTip1.SetToolTip(RobustPictureBox, "Robust " & My.Resources.is_Off)
        If Settings.ServerType <> "Robust" Then
            Log(My.Resources.Info, My.Resources.Running_as_a_Region_Server_word)
            Return True
        End If

        If Settings.RobustServer <> "127.0.0.1" And Settings.RobustServer <> "localhost" Then
            Print("Robust:" & Settings.RobustServer)
            RobustPictureBox.Image = My.Resources.nav_plain_green
            ToolTip1.SetToolTip(RobustPictureBox, My.Resources.Robust_running)
            Log(My.Resources.Info, My.Resources.Robust_not_Running)
            Return True
        End If

        _RobustIsStarting = True

        Environment.SetEnvironmentVariable("OSIM_LOGLEVEL", Settings.LogLevel.ToUpperInvariant)
        PropRobustProcID = 0
        Print(My.Resources.Starting_word & " Robust")

        RobustProcess.EnableRaisingEvents = True
        RobustProcess.StartInfo.UseShellExecute = True ' so we can redirect streams
        RobustProcess.StartInfo.FileName = PropOpensimBinPath & "bin\robust.exe"

        RobustProcess.StartInfo.CreateNoWindow = False
        RobustProcess.StartInfo.WorkingDirectory = PropOpensimBinPath & "bin"
        RobustProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal
        RobustProcess.StartInfo.Arguments = "-inifile Robust.HG.ini"
        Try
            RobustProcess.Start()
            Log(My.Resources.Info, My.Resources.Robust_running)
        Catch ex As InvalidOperationException
            Print("Robust " & My.Resources.did_not_start_word & ex.Message)
            KillAll()
            Buttons(StartButton)
            RobustPictureBox.Image = My.Resources.nav_plain_red
            ToolTip1.SetToolTip(RobustPictureBox, "Robust " & My.Resources.did_not_start_word & ex.Message)
            _RobustIsStarting = False
            Return False
        Catch ex As System.ComponentModel.Win32Exception
            Print("Robust " & My.Resources.did_not_start_word & ex.Message)
            KillAll()
            Buttons(StartButton)
            RobustPictureBox.Image = My.Resources.nav_plain_red
            ToolTip1.SetToolTip(RobustPictureBox, "Robust " & My.Resources.did_not_start_word & ex.Message)
            Buttons(StartButton)
            _RobustIsStarting = False
            Return False
        End Try

        PropRobustProcID = WaitForPID(RobustProcess)
        If PropRobustProcID = 0 Then
            RobustPictureBox.Image = My.Resources.error_icon
            ToolTip1.SetToolTip(RobustPictureBox, My.Resources.Robust_failed_to_start)
            Log("Error", My.Resources.Robust_failed_to_start)
            _RobustIsStarting = False
            Return False
        End If

        SetWindowTextCall(RobustProcess, "Robust")

        ' Wait for Robust to start listening
        Dim counter = 0
        While Not CheckRobust() And PropOpensimIsRunning
            Log("Error", My.Resources.Waiting_on_Robust)
            'Application.doevents()
            counter += 1
            ' wait a minute for it to start
            If counter > 600 Then
                Print(My.Resources.Robust_failed_to_start)
                Buttons(StartButton)
                Dim yesno = MsgBox(My.Resources.See_Log, vbYesNo, My.Resources.Error_word)
                If (yesno = vbYes) Then
                    Dim Log As String = """" & PropOpensimBinPath & "bin\Robust.log" & """"
                    Try
                        System.Diagnostics.Process.Start(PropMyFolder & "\baretail.exe " & Log)
                    Catch ex As InvalidOperationException
                    Catch ex As System.ComponentModel.Win32Exception
                    End Try
                End If
                Buttons(StartButton)
                RobustPictureBox.Image = My.Resources.nav_plain_red
                ToolTip1.SetToolTip(RobustPictureBox, My.Resources.Robust_failed_to_start)
                _RobustIsStarting = False
                Return False
            End If
            'Application.doevents()
            Sleep(100)
        End While

        _RobustIsStarting = False
        Log(My.Resources.Info, My.Resources.Robust_running)
        If Settings.ConsoleShow = False Then
            ShowDOSWindow(GetHwnd("Robust"), SHOWWINDOWENUM.SWMINIMIZE)
        End If

        RobustPictureBox.Image = My.Resources.nav_plain_green
        ToolTip1.SetToolTip(RobustPictureBox, My.Resources.Robust_running)

        PropRobustExited = False
        'Application.doevents()

        Return True

    End Function

    ''' <summary>Startup() Starts opensimulator system Called by Start Button or by AutoStart</summary>
    Public Sub Startup()

        Print(My.Resources.Version_word & " " & PropMyVersion)

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
        'Application.doevents()
        If Settings.AutoBackup Then
            ' add 30 minutes to allow time to auto backup and then restart
            Dim BTime As Integer = CInt(Settings.AutobackupInterval)
            If Settings.AutoRestartInterval > 0 And Settings.AutoRestartInterval < BTime Then
                Settings.AutoRestartInterval = BTime + 30
                Print(My.Resources.AutorestartTime & CStr(BTime) & " + 30.")
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

        StartApache()

        StartIcecast()

        UploadPhoto()

        ' old files to clean up

        If Settings.BirdsModuleStartup Then
            Try
                My.Computer.FileSystem.CopyFile(PropOpensimBinPath & "\bin\OpenSimBirds.Module.bak", PropOpensimBinPath & "\bin\OpenSimBirds.Module.dll")
            Catch ex As ArgumentNullException
            Catch ex As ArgumentException
            Catch ex As FileNotFoundException
            Catch ex As PathTooLongException
            Catch ex As IOException
            Catch ex As NotSupportedException
            Catch ex As UnauthorizedAccessException
            Catch ex As System.Security.SecurityException
            End Try
        Else
            FileStuff.DeleteFile(PropOpensimBinPath & "\bin\OpenSimBirds.Module.dll")
        End If

        If Not StartRobust() Then
            Buttons(StartButton)
            Print(My.Resources.Stopped_word)
            Return
        End If

        If Not Settings.RunOnce And Settings.ServerType = "Robust" Then
            ConsoleCommand("Robust", "create user{ENTER}")
            'Application.doevents()
            MsgBox(My.Resources.Please_type, vbInformation, My.Resources.Information)

            If Settings.ConsoleShow = False Then
                ShowDOSWindow(GetHwnd("Robust"), SHOWWINDOWENUM.SWMINIMIZE)
            End If

            Settings.RunOnce = True
            Settings.SaveSettings()
        End If

        Timer1.Interval = 1000
        Timer1.Start() 'Timer starts functioning

        PropOpensimIsRunning() = True
        ToolBar(True)

        ' Launch the rockets
        Print(My.Resources.Start_Regions_word)
        If Not StartOpensimulator() Then
            Buttons(StartButton)
            Print(My.Resources.Stopped_word)
            Return
        End If

        Buttons(StopButton)
        Print(My.Resources.Grid_address & vbCrLf & "http://" & Settings.BaseHostName & ":" & Settings.HttpPort)

        ' done with boot up

    End Sub

    Public Sub StopGroup(Groupname As String)

        For Each RegionUUID As String In PropRegionClass.RegionUUIDListByName(Groupname)
            ' Called by a sim restart, do not change status

            PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.Stopped
            Log(My.Resources.Info, PropRegionClass.RegionName(RegionUUID) & " Stopped")

            PropRegionClass.Timer(RegionUUID) = RegionMaker.REGIONTIMER.Stopped
            'Application.doevents()
        Next
        Log(My.Resources.Info, Groupname & " Group is now stopped")
        PropUpdateView = True ' make form refresh

    End Sub

    Public Sub StopRobust()

        ConsoleCommand("Robust", "q{ENTER}" & vbCrLf)
        Dim ctr As Integer = 0
        ' wait 60 seconds for robust to quit
        While CheckRobust() And ctr < 60
            Sleep(1000)
            ctr += 1
            'Application.doevents()
        End While

        RobustPictureBox.Image = My.Resources.nav_plain_red
        ToolTip1.SetToolTip(RobustPictureBox, My.Resources.Stopped_word)

        ' trust, but verify
        If ctr >= 60 Then
            RobustPictureBox.Image = My.Resources.nav_plain_red
            ToolTip1.SetToolTip(RobustPictureBox, My.Resources.Stopped_word)
        End If

    End Sub

    Public Sub ToolBar(visible As Boolean)

        AviLabel.Visible = visible
        AvatarLabel.Visible = visible
        PercentCPU.Visible = visible
        PercentRAM.Visible = visible

    End Sub

    Public Sub UploadCategory()

        'PHASE 2, upload Description and Categories
        Dim result As String = Nothing
        If Settings.Categories.Length = 0 Then Return

        Using client As New WebClient ' download client for web pages
            Try
                Dim str = PropDomain & "/cgi/UpdateCategory.plx?Category=" & Settings.Categories & "&Description=" & Settings.Description & GetPostData()
                result = client.DownloadString(str)
            Catch ex As ArgumentNullException
                ErrorLog(My.Resources.Wrong & " " & ex.Message)
            Catch ex As WebException
                ErrorLog(My.Resources.Wrong & " " & ex.Message)
            Catch ex As NotSupportedException
                ErrorLog(My.Resources.Wrong & " " & ex.Message)
            End Try
        End Using

        If result <> "OK" Then
            ErrorLog(My.Resources.Wrong & " " & result)
        End If

    End Sub

    ''' <summary>Upload in a separate thread the photo, if any. Cannot be called unless main web server is known to be on line.</summary>
    Public Sub UploadPhoto()

        If Settings.GDPR() Then
            If System.IO.File.Exists(PropMyFolder & "\OutworldzFiles\Photo.png") Then
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
#Disable Warning CA2213
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
            path.Add("""" & PropOpensimBinPath & "bin\Regions\" & name & "\Opensim.log" & """")
        Else
            If name = "All Logs" Then AllLogs = True
            If name = "Robust" Or AllLogs Then path.Add("""" & PropOpensimBinPath & "bin\Robust.log" & """")
            If name = "Outworldz" Or AllLogs Then path.Add("""" & PropMyFolder & "\Outworldzfiles\Outworldz.log" & """")
            If name = "Error" Or AllLogs Then path.Add("""" & PropMyFolder & "\Outworldzfiles\Error.log" & """")
            If name = "UPnP" Or AllLogs Then path.Add("""" & PropMyFolder & "\Outworldzfiles\Upnp.log" & """")
            If name = "Icecast" Or AllLogs Then path.Add(" " & """" & PropMyFolder & "\Outworldzfiles\Icecast\log\error.log" & """")
            If name = "All Settings" Or AllLogs Then path.Add("""" & PropMyFolder & "\Outworldzfiles\Settings.ini" & """")
            If name = "--- Regions ---" Then Return

            If AllLogs Then
                For Each UUID As String In PropRegionClass.RegionUUIDs
                    name = PropRegionClass.GroupName(UUID)
                    path.Add("""" & PropOpensimBinPath & "bin\Regions\" & name & "\Opensim.log" & """")
                    'Application.doevents()
                Next
            End If

            If name = "MySQL" Or AllLogs Then
                Dim MysqlLog As String = PropMyFolder & "\OutworldzFiles\mysql\data"
                Dim files As Array = Nothing
                Try
                    files = Directory.GetFiles(MysqlLog, "*.err", SearchOption.TopDirectoryOnly)
                Catch ex As ArgumentException
                Catch ex As UnauthorizedAccessException
                Catch ex As DirectoryNotFoundException
                Catch ex As PathTooLongException
                Catch ex As IOException
                End Try

                For Each FileName As String In files
                    path.Add("""" & FileName & """")
                    'Application.doevents()
                Next

            End If
        End If
        ' Filter distinct elements, and convert back into list.
        Dim result As List(Of String) = path.Distinct().ToList

        Dim logs As String = ""
        For Each item In result
            Log("View", item)
            logs = logs & " " & item
            'Application.doevents()
        Next

        Try
            System.Diagnostics.Process.Start(PropMyFolder & "\baretail.exe", logs)
        Catch ex As InvalidOperationException
        Catch ex As System.ComponentModel.Win32Exception
        End Try

    End Sub

    Public Function WaitForPID(myProcess As Process) As Integer

        If myProcess Is Nothing Then Return 0

        Dim PID As Integer = 0
        Dim TooMany As Integer = 0
        Dim p As Process = Nothing

        Do While TooMany < 200

            Try
                p = Process.GetProcessById(myProcess.Id)
            Catch ex As ArgumentException
            Catch ex As InvalidOperationException
            End Try

            If p Is Nothing Then Return 0

            If p.ProcessName.Length > 0 Then
                PID = p.Id
                Exit Do
            End If

            Sleep(100)
            TooMany += 1
        Loop

        If PID = 0 Then
            Print("Cannot get a Process ID from " & myProcess.ProcessName)
        End If

        Return PID

    End Function

#End Region

#Region "Private Methods"

    Private Sub AddLog(name As String)
        Dim LogMenu As New ToolStripMenuItem With {
                .Text = name,
                .ToolTipText = My.Resources.Click_to_View_this_word,
                .Size = New Size(269, 26),
                .Image = My.Resources.Resources.document_view,
                .DisplayStyle = ToolStripItemDisplayStyle.Text
            }
        AddHandler LogMenu.Click, New EventHandler(AddressOf LogViewClick)
        ViewLogsToolStripMenuItem.Visible = True
        ViewLogsToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {LogMenu})

    End Sub

    Private Sub AddUserToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddUserToolStripMenuItem.Click
        ConsoleCommand("Robust", "create user{ENTER}")
    End Sub

    Private Sub AdminUIToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ViewWebUI.Click

        If PropOpensimIsRunning() Then
            If Settings.ApacheEnable Then
                Dim webAddress As String = "http://127.0.0.1:" & Convert.ToString(Settings.ApachePort, Globalization.CultureInfo.InvariantCulture)
                Try
                    Process.Start(webAddress)
                Catch ex As InvalidOperationException
                Catch ex As System.ComponentModel.Win32Exception
                End Try
            Else
                Dim webAddress As String = "http://127.0.0.1:" & Settings.HttpPort
                Try
                    Process.Start(webAddress)
                Catch ex As InvalidOperationException
                Catch ex As System.ComponentModel.Win32Exception
                End Try
                Print(My.Resources.User_Name_word & ":" & Settings.AdminFirst & " " & Settings.AdminLast)
                Print(My.Resources.Password_word & ":" & Settings.Password)
            End If
        Else
            If Settings.ApacheEnable Then
                Dim webAddress As String = "http://127.0.0.1:" & Convert.ToString(Settings.ApachePort, Globalization.CultureInfo.InvariantCulture)
                Try
                    Process.Start(webAddress)
                Catch ex As InvalidOperationException
                Catch ex As System.ComponentModel.Win32Exception
                End Try
            Else
                Print(My.Resources.Not_Running)
            End If
        End If

    End Sub

    Private Sub AdvancedSettingsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AdvancedSettingsToolStripMenuItem.Click

        If PropInitted Then
            Adv.Activate()
            Adv.Visible = True
            Adv.Select()
            Adv.BringToFront()
        End If

    End Sub

    Private Sub AllRegionsOARsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AllTheRegionsOarsToolStripMenuItem.Click

        If Not PropOpensimIsRunning() Then
            Print(My.Resources.Not_Running)
            Return
        End If

        For Each RegionUUID As String In PropRegionClass.RegionUUIDs
            If PropRegionClass.IsBooted(RegionUUID) Then

                Print("Backing up " & PropRegionClass.RegionName(RegionUUID))
                ConsoleCommand(RegionUUID, "change region " & """" & PropRegionClass.RegionName(RegionUUID) & """" & "{ENTER}" & vbCrLf)
                ConsoleCommand(RegionUUID, "save oar  " & """" & BackupPath() & PropRegionClass.RegionName(RegionUUID) & "_" & DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss", Globalization.CultureInfo.InvariantCulture) & ".oar" & """" & "{ENTER}" & vbCrLf)

                Sleep(15000)
                SequentialPause()   ' wait for previous region to give us some CPU
                Dim hwnd = GetHwnd(PropRegionClass.GroupName(RegionUUID))
                Form1.ShowDOSWindow(hwnd, Form1.SHOWWINDOWENUM.SWMINIMIZE)
            End If
        Next

    End Sub

    Private Sub AllToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles All.Click
        SendMsg("all")
    End Sub

    Private Sub AllUsersAllSimsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles JustOneRegionToolStripMenuItem.Click

        If Not PropOpensimIsRunning() Then
            Print(My.Resources.Not_Running)
            Return
        End If
        Dim rname = ChooseRegion(True)
        If rname.Length > 0 Then
            Dim Message = InputBox(My.Resources.What_to_say_2_region)
            Dim RegionUUID As String = PropRegionClass.FindRegionByName(rname)
            If RegionUUID.Length > 0 Then
                ConsoleCommand(RegionUUID, "change region  " & PropRegionClass.RegionName(RegionUUID) & "{ENTER}" & vbCrLf)
                ConsoleCommand(RegionUUID, "alert " & Message & "{ENTER}" & vbCrLf)
            End If

        End If

    End Sub

    Private Sub ApachePictureBox_Click(sender As Object, e As EventArgs) Handles ApachePictureBox.Click

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

        Dim yesno = MsgBox(My.Resources.Apache_Exited, vbYesNo, My.Resources.Error_word)
        If (yesno = vbYes) Then
            Dim Apachelog As String = PropMyFolder & "\Outworldzfiles\Apache\logs\error*.log"
            Try
                System.Diagnostics.Process.Start(PropMyFolder & "\baretail.exe", """" & Apachelog & """")
            Catch ex As InvalidOperationException
            Catch ex As System.ComponentModel.Win32Exception
            End Try
        End If

    End Sub

    ''' <summary>query MySQL to find any avatars in the DOS box so we can stop it, or not</summary>
    ''' <param name="groupname"></param>
    ''' <returns></returns>
    Private Function AvatarsIsInGroup(groupname As String) As Boolean

        Dim present As Integer = 0
        For Each RegionUUID As String In PropRegionClass.RegionUUIDListByName(groupname)
            present += PropRegionClass.AvatarCount(RegionUUID)
            'Application.doevents()
        Next

        Return CType(present, Boolean)

    End Function

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

    Private Sub BackupIarClick(sender As ToolStripMenuItem, e As EventArgs)

        Dim File As String = PropMyFolder & "/OutworldzFiles/AutoBackup/" & sender.Text 'make a real URL
        If LoadIARContent(File) Then
            Print(My.Resources.Opensimulator_is_loading & " " & sender.Text & ".  " & My.Resources.Take_time)
        End If

    End Sub

    Private Sub BackupOarClick(sender As ToolStripMenuItem, e As EventArgs)

        Dim File = PropMyFolder & "/OutworldzFiles/AutoBackup/" & sender.Text 'make a real URL
        If LoadOARContent(File) Then
            Print(My.Resources.Opensimulator_is_loading & " " & sender.Text & ".  " & My.Resources.Take_time)
        End If

    End Sub

    Private Function BackupPath() As String

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

    Private Sub BasqueToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BasqueToolStripMenuItem.Click
        Settings.Language = "eu"
        Language(sender, e)
    End Sub

    Private Sub BusyButton_Click(sender As Object, e As EventArgs) Handles BusyButton.Click

        PropAborting = True
        StopAllRegions()
        Timer1.Stop()

        PropUpdateView = True ' make form refresh

        PropOpensimIsRunning() = False
        ToolBar(False)
        Print(My.Resources.Stopped_word)
        Buttons(StartButton)

    End Sub

    Private Sub CatalanToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CatalanToolStripMenuItem.Click
        Settings.Language = "ca-ES"
        Language(sender, e)
    End Sub

    Private Sub ChangePasswordToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ChangePasswordToolStripMenuItem.Click
        ConsoleCommand("Robust", "reset user password{ENTER}")
    End Sub

    'End Sub
    Private Sub Chart()
        ' Graph https://github.com/sinairv/MSChartWrapper
        Try
            ' running average
            speed3 = speed2
            speed2 = speed1
            speed1 = speed
            Try
                speed = cpu.NextValue()
            Catch ex As Exception

                Dim pUpdate As Process = New Process()
                Dim pi As ProcessStartInfo = New ProcessStartInfo With {
                    .Arguments = "/ R",
                    .FileName = "loadctr"
                }
                pUpdate.StartInfo = pi

                Try
                    pUpdate.Start()
                    pUpdate.WaitForExit()
                Catch ex1 As InvalidOperationException
                Catch ex1 As ComponentModel.Win32Exception
                End Try
            End Try

            CPUAverageSpeed = (speed + speed1 + speed2 + speed3) / 4

            Dim i = 180
            While i >= 0
                MyCPUCollection(i + 1) = MyCPUCollection(i)
                i -= 1
            End While

            MyCPUCollection(0) = speed
            PercentCPU.Text = String.Format(Globalization.CultureInfo.InvariantCulture, "{0: 0}% CPU", CPUAverageSpeed)
#Disable Warning CA1031 ' Do not catch general exception types
        Catch ex As Exception
#Enable Warning CA1031 ' Do not catch general exception types
            ErrorLog(ex.Message)
        End Try

        ''reverse series

        ChartWrapper1.ClearChart()
        ChartWrapper1.AddLinePlot("CPU", MyCPUCollection)

        'RAM

        Dim wql As ObjectQuery = New ObjectQuery("SELECT TotalVisibleMemorySize,FreePhysicalMemory FROM Win32_OperatingSystem")
        Dim searcher As ManagementObjectSearcher = New ManagementObjectSearcher(wql)
        Dim results As ManagementObjectCollection = searcher.Get()
        searcher.Dispose()

        Try
            For Each result In results
                Dim value = (CDbl(result("TotalVisibleMemorySize").ToString) - CDbl(result("FreePhysicalMemory").ToString)) / CDbl(result("TotalVisibleMemorySize").ToString) * 100

                Dim j = 180
                While j >= 0
                    MyRAMCollection(j + 1) = MyRAMCollection(j)
                    j -= 1
                End While
                MyRAMCollection(0) = CDbl(value)
                value = Math.Round(value)
                PercentRAM.Text = CStr(value) & "% RAM"
                'Application.doevents()
            Next
#Disable Warning CA1031 ' Do not catch general exception types
        Catch ex As Exception
#Enable Warning CA1031 ' Do not catch general exception types
            Log(My.Resources.Error_word, ex.Message)
        End Try

        ChartWrapper2.ClearChart()
        ChartWrapper2.AddLinePlot("RAM", MyRAMCollection)

    End Sub

    Private Sub CheckAndRepairDatbaseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CheckAndRepairDatbaseToolStripMenuItem.Click

        If Not StartMySQL() Then
            ToolBar(False)
            Buttons(StartButton)
            Print(My.Resources.Stopped_word)
            Return
        End If

        Dim pi As ProcessStartInfo = New ProcessStartInfo()

        ChDir(PropMyFolder & "\OutworldzFiles\mysql\bin")
        pi.WindowStyle = ProcessWindowStyle.Normal
        pi.Arguments = CStr(Settings.MySqlRobustDBPort)

        pi.FileName = "CheckAndRepair.bat"
        Using pMySqlDiag1 As Process = New Process With {
                .StartInfo = pi
            }
            Try
                pMySqlDiag1.Start()
            Catch ex As InvalidOperationException
            Catch ex As System.ComponentModel.Win32Exception

            End Try
            pMySqlDiag1.WaitForExit()
        End Using

        ChDir(PropMyFolder)

    End Sub

    ''' <summary>Check is Apache port 80 or 8000 is up</summary>
    ''' <returns>boolean</returns>
    Private Function CheckApache() As Boolean

        Using client As New WebClient ' download client for web pages
            Dim Up As String
            Try
                Up = client.DownloadString("http://" & Settings.PublicIP & ":" & CStr(Settings.ApachePort) & "/?_Opensim=" & RandomNumber.Random)
            Catch ex As ArgumentNullException
                If ex.Message.Contains("200 OK") Then Return True
                Return False
            Catch ex As WebException
                If ex.Message.Contains("200 OK") Then Return True
                Return False
            Catch ex As NotSupportedException
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
            MsgBox(My.Resources.Diag_Port_word & " " & Settings.DiagnosticPort & ". " & My.Resources.Diag_Broken)
            PropUseIcons = False
        End If

    End Sub

    Private Sub CheckForUpdatesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CHeckForUpdatesToolStripMenuItem.Click

        CheckForUpdates()

    End Sub

    ''' <summary>Check is Icecast port 8081 is up</summary>
    ''' <returns>boolean</returns>
    Private Function CheckIcecast() As Boolean

        Using client As New WebClient ' download client for web pages
            Dim Up As String
            Try
                Up = client.DownloadString("http://" & Settings.PublicIP & ":" & Settings.SCPortBase & "/?_Opensim=" & RandomNumber.Random())
            Catch ex As ArgumentNullException
                Return False
            Catch ex As WebException
                Return False
            Catch ex As NotSupportedException
                Return False
            End Try

            If Up.Length = 0 And PropOpensimIsRunning() Then
                Return False
            End If
        End Using
        Return True

    End Function

    ''' <summary>Check is Robust port 8002 is up</summary>
    ''' <returns>boolean</returns>
    Private Function CheckRobust() As Boolean

        Using client As New WebClient ' download client for web pages
            Dim Up As String
            Try
                Up = client.DownloadString("http://" & Settings.RobustServer & ":" & Settings.HttpPort & "/?_Opensim=" & RandomNumber.Random())
            Catch ex As ArgumentNullException
                If ex.Message.Contains("404") Then Return True
                Return False
            Catch ex As WebException
                If ex.Message.Contains("404") Then Return True
                Return False
            Catch ex As NotSupportedException
                If ex.Message.Contains("404") Then Return True
                Return False
            End Try

            If Up.Length = 0 And PropOpensimIsRunning() Then
                Return False
            End If
        End Using
        Return True

    End Function

    Private Sub ChineseSimplifedToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ChineseSimplifedToolStripMenuItem.Click
        Settings.Language = "zh-CN"
        Language(sender, e)
    End Sub

    Private Sub ChineseTraditionalToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ChineseTraditionalToolStripMenuItem.Click
        Settings.Language = "zh-TW"
        Language(sender, e)
    End Sub

    Private Sub CleanDLLs()

        Dim dlls As List(Of String) = GetDlls(PropMyFolder & "/dlls.txt")
        Dim localdlls As List(Of String) = GetFilesRecursive(PropOpensimBinPath & "bin")
        For Each localdllname In localdlls
            'Application.doevents()
            Dim x = localdllname.IndexOf("OutworldzFiles", StringComparison.InvariantCulture)
            Dim newlocaldllname = Mid(localdllname, x)
            If Not CompareDLLignoreCase(newlocaldllname, dlls) Then
                Log(My.Resources.Info, "Deleting dll " & localdllname)
                FileStuff.DeleteFile(localdllname)
            End If
            'Application.doevents()
        Next

    End Sub

    ''' <summary>Deletes old log files</summary>
    Private Sub ClearLogFiles()

        Dim Logfiles = New List(Of String) From {
            PropMyFolder & "\OutworldzFiles\Error.log",
            PropMyFolder & "\OutworldzFiles\Outworldz.log",
            PropMyFolder & "\OutworldzFiles\Opensim\bin\OpenSimConsoleHistory.txt",
            PropMyFolder & "\OutworldzFiles\Diagnostics.log",
            PropMyFolder & "\OutworldzFiles\UPnp.log",
            PropMyFolder & "\OutworldzFiles\Opensim\bin\Robust.log",
            PropMyFolder & "\OutworldzFiles\http.log",
            PropMyFolder & "\OutworldzFiles\PHPLog.log",
            PropMyFolder & "\http.log"      ' an old mistake
        }

        For Each thing As String In Logfiles
            ' clear out the log files
            FileStuff.DeleteFile(thing)
            'Application.doevents()
        Next

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
        Catch ex As InvalidOperationException
        Catch ex As System.ComponentModel.Win32Exception
        End Try
    End Sub

    Private Sub Create_ShortCut(ByVal sTargetPath As String)
        ' Requires reference to Windows Script Host Object Model
        Dim WshShell As WshShellClass = New WshShellClass
        Dim MyShortcut As IWshShortcut
        ' The shortcut will be created on the desktop
        Dim DesktopFolder As String = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)
        MyShortcut = CType(WshShell.CreateShortcut(DesktopFolder & "\Outworldz.lnk"), IWshShortcut)
        MyShortcut.TargetPath = sTargetPath
        MyShortcut.IconLocation = WshShell.ExpandEnvironmentStrings(PropMyFolder & "\Start.exe")
        MyShortcut.WorkingDirectory = PropMyFolder
        MyShortcut.Save()

    End Sub

    Private Sub CreateService()

        ' create test program slants the other way:
        Dim testProgram As String = PropMyFolder & "\OutworldzFiles\Mysql\bin\InstallAsAService.bat"
        FileStuff.DeleteFile(testProgram)

        Try
            Using outputFile As New StreamWriter(testProgram, True)
                outputFile.WriteLine("@REM Program to run Mysql as a Service" & vbCrLf +
            "mysqld.exe --install Mysql --defaults-file=" & """" & PropCurSlashDir & "/OutworldzFiles/mysql/my.ini" & """" & vbCrLf & "net start Mysql" & vbCrLf)
            End Using
        Catch ex As IOException
        Catch ex As UnauthorizedAccessException
        Catch ex As ArgumentException
        Catch ex As System.Security.SecurityException
        Catch ex As ObjectDisposedException
        End Try

    End Sub

    Private Sub CreateStopMySql()

        ' create test program slants the other way:
        Dim testProgram As String = PropMyFolder & "\OutworldzFiles\Mysql\bin\StopMySQL.bat"
        FileStuff.DeleteFile(testProgram)
        Try
            Using outputFile As New StreamWriter(testProgram, True)
                outputFile.WriteLine("@REM Program to stop Mysql" & vbCrLf +
            "mysqladmin.exe -u root --port " & CStr(Settings.MySqlRobustDBPort) & " shutdown" & vbCrLf & "@pause" & vbCrLf)
            End Using
        Catch ex As IOException
        Catch ex As UnauthorizedAccessException
        Catch ex As ArgumentException
        Catch ex As System.Security.SecurityException
        Catch ex As ObjectDisposedException
        End Try

    End Sub

    Private Sub CzechToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CzechToolStripMenuItem.Click
        Settings.Language = "cs"
        Language(sender, e)
    End Sub

    Private Sub Debug_Click(sender As Object, e As EventArgs) Handles Debug.Click
        SendMsg("debug")
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

    Private Function DoApache() As Boolean

        If Not Settings.ApacheEnable Then Return False
        Print("->Set Apache")
        ' lean rightward paths for Apache
        Dim ini = PropMyFolder & "\Outworldzfiles\Apache\conf\httpd.conf"
        Settings.LoadLiteralIni(ini)
        Settings.SetLiteralIni("Listen", "Listen " & Convert.ToString(Settings.ApachePort, Globalization.CultureInfo.InvariantCulture))
        Settings.SetLiteralIni("ServerRoot", "ServerRoot " & """" & PropCurSlashDir & "/Outworldzfiles/Apache" & """")
        Settings.SetLiteralIni("DocumentRoot", "DocumentRoot " & """" & PropCurSlashDir & "/Outworldzfiles/Apache/htdocs" & """")
        Settings.SetLiteralIni("Use VDir", "Use VDir " & """" & PropCurSlashDir & "/Outworldzfiles/Apache/htdocs" & """")
        Settings.SetLiteralIni("PHPIniDir", "PHPIniDir " & """" & PropCurSlashDir & "/Outworldzfiles/PHP7" & """")
        Settings.SetLiteralIni("ServerName", "ServerName " & Settings.PublicIP)
        Settings.SetLiteralIni("ServerAdmin", "ServerAdmin " & Settings.AdminEmail)
        Settings.SetLiteralIni("<VirtualHost", "<VirtualHost  *:" & Convert.ToString(Settings.ApachePort, Globalization.CultureInfo.InvariantCulture) & ">")
        Settings.SetLiteralIni("ErrorLog", "ErrorLog " & """|bin/rotatelogs.exe  -l \" & """" & PropCurSlashDir & "/Outworldzfiles/Apache/logs/Error-%Y-%m-%d.log" & "\" & """" & " 86400""")
        Settings.SetLiteralIni("CustomLog", "CustomLog " & """|bin/rotatelogs.exe -l \" & """" & PropCurSlashDir & "/Outworldzfiles/Apache/logs/access-%Y-%m-%d.log" & "\" & """" & " 86400""" & " common env=!dontlog""")
        ' needed for Php5 upgrade
        Settings.SetLiteralIni("LoadModule php5_module", "LoadModule php7_module")
        Settings.SetLiteralIni("LoadModule php7_module", "LoadModule php7_module " & """" & PropCurSlashDir & "/Outworldzfiles/PHP7/php7apache2_4.dll" & """")

        Settings.SaveLiteralIni(ini, "httpd.conf")

        Try
            Directory.Delete(PropMyFolder & "\Outworldzfiles\PHP5", True)
        Catch ex As DirectoryNotFoundException
        Catch ex As IOException
        Catch ex As UnauthorizedAccessException
        Catch ex As ArgumentException
        End Try

        ' lean rightward paths for Apache
        ini = PropMyFolder & "\Outworldzfiles\Apache\conf\extra\httpd-ssl.conf"
        Settings.LoadLiteralIni(ini)
        Settings.SetLiteralIni("Listen", "Listen " & Settings.PrivateURL & ":" & "443")
        Settings.SetLiteralIni("DocumentRoot", "DocumentRoot " & """" & PropCurSlashDir & "/Outworldzfiles/Apache/htdocs""")
        Settings.SetLiteralIni("ServerName", "ServerName " & Settings.PublicIP)
        Settings.SetLiteralIni("SSLSessionCache", "SSLSessionCache shmcb:""" & PropCurSlashDir & "/Outworldzfiles/Apache/logs" & "/ssl_scache(512000)""")
        Settings.SaveLiteralIni(ini, "httpd-ssl.conf")
        Return False

    End Function

    Private Function DoBirds() As Boolean

        If Not Settings.BirdsModuleStartup Then Return False
        Print("->Set Birds")
        Dim BirdFile = PropOpensimBinPath & "bin\addon-modules\OpenSimBirds\config\OpenSimBirds.ini"
        Try
            System.IO.File.Delete(BirdFile)
        Catch ex As IOException
        Catch ex As UnauthorizedAccessException
        Catch ex As ArgumentNullException
        Catch ex As ArgumentException
        End Try

        Dim BirdData As String = ""

        ' Birds setup per region
        For Each RegionUUID As String In PropRegionClass.RegionUUIDs

            Dim simName = PropRegionClass.RegionName(RegionUUID)

            If Settings.LoadIni(PropRegionClass.RegionPath(RegionUUID), ";") Then Return True

            If Settings.BirdsModuleStartup And PropRegionClass.Birds(RegionUUID) = "True" Then

                BirdData = BirdData & "[" & simName & "]" & vbCrLf &
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

    Private Sub DoDiag()

        If IPCheck.IsPrivateIP(Settings.DNSName) Then
            Print(My.Resources.LAN_IP)
            Return
        End If

        Print("---------------------------")
        Print(My.Resources.Running_Network)

        Settings.DiagFailed = False

        OpenPorts() ' Open router ports with UPnp
        ProbePublicPort() ' Probe using Outworldz like Canyouseeme.org does on HTTP port
        TestPrivateLoopback()   ' Diagnostics
        TestPublicLoopback()    ' Http port
        TestAllRegionPorts()    ' All Dos boxes, actually

        If Settings.DiagFailed Then
            Dim answer = MsgBox(My.Resources.Diags_Failed, vbYesNo)
            If answer = vbYes Then
                ShowLog()
            End If
        Else
            NewDNSName()
        End If
        Print("---------------------------")

    End Sub

    Private Function DoFlotsamINI() As Boolean


        If Settings.LoadIni(PropOpensimBinPath & "bin\config-include\FlotsamCache.ini", ";") Then Return True
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
        Dim GridCommon As String = "GridcommonGridServer"

        Select Case Settings.ServerType
            Case "Robust"
                My.Computer.FileSystem.CopyDirectory(PropOpensimBinPath & "bin\Library.proto", PropOpensimBinPath & "bin\Library", True)
                GridCommon = "Gridcommon-GridServer.ini"
            Case "Region"
                My.Computer.FileSystem.CopyDirectory(PropOpensimBinPath & "bin\Library.proto", PropOpensimBinPath & "bin\Library", True)
                GridCommon = "Gridcommon-RegionServer.ini"
            Case "OsGrid"
                GridCommon = "Gridcommon-OsGridServer.ini"
            Case "Metro"
                GridCommon = "Gridcommon-MetroServer.ini"

        End Select

        ' Put that gridcommon.ini file in place
        FileStuff.CopyFile(PropOpensimBinPath & "bin\config-include\" & GridCommon, IO.Path.Combine(PropOpensimBinPath, "bin\config-include\GridCommon.ini"), True)

        If Settings.LoadIni(PropOpensimBinPath & "bin\config-include\GridCommon.ini", ";") Then Return True
        Settings.SetIni("HGInventoryAccessModule", "OutboundPermission", CStr(Settings.OutBoundPermissions))
        Settings.SetIni("DatabaseService", "ConnectionString", Settings.RegionDBConnection)

        Settings.SaveINI(System.Text.Encoding.UTF8)
        Return False

    End Function

    Private Function DoOpensimINI() As Boolean

        ' Opensim.ini
        If Settings.LoadIni(GetOpensimProto(), ";") Then Return True
        Print("->Set Opensim.Proto")
        Select Case Settings.ServerType
            Case "Robust"
                If Settings.SearchEnabled Then
                    ' RegionSnapShot
                    Settings.SetIni("DataSnapshot", "index_sims", "True")
                    If Settings.SearchLocal Then
                        Settings.SetIni("DataSnapshot", "data_services", "${Const|BaseURL}:" & CStr(Settings.ApachePort) & "/Search/register.php;http://www.hyperica.com/Search/register.php")
                        Settings.SetIni("Search", "SearchURL", "${Const|BaseURL}:" & CStr(Settings.ApachePort) & "/Search/query.php")
                        Settings.SetIni("Search", "SimulatorFeatures", "${Const|BaseURL}:" & CStr(Settings.ApachePort) & "/Search/query.php")
                    Else
                        Settings.SetIni("DataSnapshot", "data_services", "http://www.hyperica.com/Search/register.php")
                        Settings.SetIni("Search", "SearchURL", "http://www.hyperica.com/Search/query.php")
                        Settings.SetIni("Search", "SimulatorFeatures", "http://www.hyperica.com/Search/query.php")
                    End If
                Else
                    Settings.SetIni("DataSnapshot", "index_sims", "False")
                End If

                Settings.SetIni("Const", "PrivURL", "http://" & Settings.PrivateURL)
                Settings.SetIni("Const", "GridName", Settings.SimName)

            Case "Region"
            Case "OSGrid"
            Case "Metro"

        End Select

        ' Support viewers object cache, default true users may need to reduce viewer bandwidth if some prims Or terrain parts fail to rez. change to false if you need to use old viewers that do Not
        ' support this feature

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
            KillOldFiles()  ' wipe out DLL's
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

        Settings.SetIni("Network", "ExternalHostNameForLSL", Settings.BaseHostName)
        Settings.SetIni("PrimLimitsModule", "EnforcePrimLimits", CStr(Settings.Primlimits))

        If Settings.Primlimits Then
            Settings.SetIni("Permissions", "permissionmodules", "DefaultPermissionsModule, PrimLimitsModule")
        Else
            Settings.SetIni("Permissions", "permissionmodules", "DefaultPermissionsModule")
        End If

        If Settings.GloebitsEnable Then
            Settings.SetIni("Startup", "economymodule", "Gloebit")
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

        ' Gods

        If (Settings.RegionOwnerIsGod Or Settings.RegionManagerIsGod) Then
            Settings.SetIni("Permissions", "allow_grid_gods", "True")
        Else
            Settings.SetIni("Permissions", "allow_grid_gods", "False")
        End If

        ' Physics choices for meshmerizer, where Ubit's ODE requires a special one ZeroMesher meshing = Meshmerizer meshing = ubODEMeshmerizer

        ' 0 = physics = none 1 = OpenDynamicsEngine 2 = physics = BulletSim 3 = physics = BulletSim with threads 4 = physics = ubODE

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

    Private Function DoPHP() As Boolean

        Print("->Set PHP7")
        Dim ini = PropMyFolder & "\Outworldzfiles\PHP7\php.ini"
        Settings.LoadLiteralIni(ini)
        Settings.SetLiteralIni("extension_dir", "extension_dir = " & """" & PropCurSlashDir & "/OutworldzFiles/PHP7/ext""")
        Settings.SetLiteralIni("doc_root", "doc_root = """ & PropCurSlashDir & "/OutworldzFiles/Apache/htdocs""")
        Settings.SaveLiteralIni(ini, "php.ini")

        Return False

    End Function

    Private Function DoRegion(simName As String) As Boolean

        'Regions - write all region.ini files with public IP and Public port
        ' has to be bound late so regions data is there.

        CopyOpensimProto(simName)

        Dim RegionUUID As String = PropRegionClass.FindRegionByName(simName)

        If Settings.LoadIni(PropRegionClass.RegionPath(RegionUUID), ";") Then Return True

        ' Autobackup
        If Settings.AutoBackup And PropRegionClass.SkipAutobackup(RegionUUID) = "" Then
            Settings.SetIni(simName, "AutoBackup", "True")
        Else
            Settings.SetIni(simName, "AutoBackup", "False")
        End If

        Settings.SetIni(simName, "InternalPort", Convert.ToString(PropRegionClass.RegionPort(RegionUUID), Globalization.CultureInfo.InvariantCulture))
        Settings.SetIni(simName, "ExternalHostName", ExternLocalServerName())

        ' not a standard INI, only use by the Dreamers
        If PropRegionClass.RegionEnabled(RegionUUID) Then
            Settings.SetIni(simName, "Enabled", "True")
        Else
            Settings.SetIni(simName, "Enabled", "False")
        End If

        ' Extended in v 2.1

        Select Case PropRegionClass.NonPhysicalPrimMax(RegionUUID)
            Case ""
                Settings.SetIni(simName, "NonPhysicalPrimMax", 1024.ToString(Globalization.CultureInfo.InvariantCulture))
            Case Else
                Settings.SetIni(simName, "NonPhysicalPrimMax", PropRegionClass.NonPhysicalPrimMax(RegionUUID))
        End Select

        Select Case PropRegionClass.PhysicalPrimMax(RegionUUID)
            Case ""
                Settings.SetIni(simName, "PhysicalPrimMax", 64.ToString(Globalization.CultureInfo.InvariantCulture))
            Case Else
                Settings.SetIni(simName, "PhysicalPrimMax", PropRegionClass.PhysicalPrimMax(RegionUUID))
        End Select

        If (Settings.Primlimits) Then
            Select Case PropRegionClass.MaxPrims(RegionUUID)
                Case ""
                    Settings.SetIni(simName, "MaxPrims", 45000.ToString(Globalization.CultureInfo.InvariantCulture))
                Case Else
                    Settings.SetIni(simName, "MaxPrims", PropRegionClass.MaxPrims(RegionUUID))
            End Select
        Else
            Select Case PropRegionClass.MaxPrims(RegionUUID)
                Case ""
                    Settings.SetIni(simName, "MaxPrims", 45000.ToString(Globalization.CultureInfo.InvariantCulture))
                Case Else
                    Settings.SetIni(simName, "MaxPrims", PropRegionClass.MaxPrims(RegionUUID))
            End Select
        End If

        Select Case PropRegionClass.MaxAgents(RegionUUID)
            Case ""
                Settings.SetIni(simName, "MaxAgents", 100.ToString(Globalization.CultureInfo.InvariantCulture))
            Case Else
                Settings.SetIni(simName, "MaxAgents", PropRegionClass.MaxAgents(RegionUUID))
        End Select

        Settings.SetIni(simName, "ClampPrimSize", Convert.ToString(PropRegionClass.ClampPrimSize(RegionUUID), Globalization.CultureInfo.InvariantCulture))

        ' Optional Extended in v 2.31 optional things
        If PropRegionClass.MapType(RegionUUID) = "None" Then
            Settings.SetIni(simName, "GenerateMaptiles", "False")
        ElseIf PropRegionClass.MapType(RegionUUID) = "Simple" Then
            Settings.SetIni(simName, "GenerateMaptiles", "True")
            Settings.SetIni(simName, "MapImageModule", "MapImageModule")  ' versus Warp3DImageModule
            Settings.SetIni(simName, "TextureOnMapTile", "False")         ' versus True
            Settings.SetIni(simName, "DrawPrimOnMapTile", "False")
            Settings.SetIni(simName, "TexturePrims", "False")
            Settings.SetIni(simName, "RenderMeshes", "False")
        ElseIf PropRegionClass.MapType(RegionUUID) = "Good" Then
            Settings.SetIni(simName, "GenerateMaptiles", "True")
            Settings.SetIni(simName, "MapImageModule", "Warp3DImageModule")  ' versus MapImageModule
            Settings.SetIni(simName, "TextureOnMapTile", "False")         ' versus True
            Settings.SetIni(simName, "DrawPrimOnMapTile", "False")
            Settings.SetIni(simName, "TexturePrims", "False")
            Settings.SetIni(simName, "RenderMeshes", "False")
        ElseIf PropRegionClass.MapType(RegionUUID) = "Better" Then
            Settings.SetIni(simName, "GenerateMaptiles", "True")
            Settings.SetIni(simName, "MapImageModule", "Warp3DImageModule")  ' versus MapImageModule
            Settings.SetIni(simName, "TextureOnMapTile", "True")         ' versus True
            Settings.SetIni(simName, "DrawPrimOnMapTile", "True")
            Settings.SetIni(simName, "TexturePrims", "False")
            Settings.SetIni(simName, "RenderMeshes", "False")
        ElseIf PropRegionClass.MapType(RegionUUID) = "Best" Then
            Settings.SetIni(simName, "GenerateMaptiles", "True")
            Settings.SetIni(simName, "MapImageModule", "Warp3DImageModule")  ' versus MapImageModule
            Settings.SetIni(simName, "TextureOnMapTile", "True")      ' versus True
            Settings.SetIni(simName, "DrawPrimOnMapTile", "True")
            Settings.SetIni(simName, "TexturePrims", "True")
            Settings.SetIni(simName, "RenderMeshes", "True")
        Else
            Settings.SetIni(simName, "GenerateMaptiles", "")
            Settings.SetIni(simName, "MapImageModule", "")  ' versus MapImageModule
            Settings.SetIni(simName, "TextureOnMapTile", "")      ' versus True
            Settings.SetIni(simName, "DrawPrimOnMapTile", "")
            Settings.SetIni(simName, "TexturePrims", "")
            Settings.SetIni(simName, "RenderMeshes", "")
        End If

        Settings.SetIni(simName, "DisableGloebits", PropRegionClass.DisableGloebits(RegionUUID))

        Settings.SetIni(simName, "RegionSnapShot", PropRegionClass.RegionSnapShot(RegionUUID))
        Settings.SetIni(simName, "Birds", PropRegionClass.Birds(RegionUUID))
        Settings.SetIni(simName, "Tides", PropRegionClass.Tides(RegionUUID))
        Settings.SetIni(simName, "Teleport", PropRegionClass.Teleport(RegionUUID))
        Settings.SetIni(simName, "DisallowForeigners", PropRegionClass.DisallowForeigners(RegionUUID))
        Settings.SetIni(simName, "DisallowResidents", PropRegionClass.DisallowResidents(RegionUUID))
        Settings.SetIni(simName, "SkipAutoBackup", PropRegionClass.SkipAutobackup(RegionUUID))
        Settings.SetIni(simName, "Physics", PropRegionClass.Physics(RegionUUID))
        Settings.SetIni(simName, "FrameTime", PropRegionClass.FrameTime(RegionUUID))

        Settings.SaveINI(System.Text.Encoding.UTF8)

        ' Opensim.ini in Region Folder specific to this region
        If Settings.LoadIni(PropOpensimBinPath & "bin\Regions\" & PropRegionClass.GroupName(RegionUUID) & "\Opensim.ini", ";") Then
            Return True
        End If

        ' Autobackup
        If Settings.AutoBackup Then
            Settings.SetIni("AutoBackupModule", "AutoBackup", "True")
        End If

        If Settings.AutoBackup And PropRegionClass.SkipAutobackup(RegionUUID) = "" Then
            Settings.SetIni("AutoBackupModule", "AutoBackup", "True")
        End If

        If Settings.AutoBackup And PropRegionClass.SkipAutobackup(RegionUUID) = "True" Then
            Settings.SetIni("AutoBackupModule", "AutoBackup", "False")
        End If

        If Not Settings.AutoBackup Then
            Settings.SetIni("AutoBackupModule", "AutoBackup", "False")
        End If

        Settings.SetIni("AutoBackupModule", "AutoBackupInterval", Settings.AutobackupInterval)
        Settings.SetIni("AutoBackupModule", "AutoBackupKeepFilesForDays", Convert.ToString(Settings.KeepForDays, Globalization.CultureInfo.InvariantCulture))
        Settings.SetIni("AutoBackupModule", "AutoBackupDir", BackupPath())

        If PropRegionClass.MapType(RegionUUID) = "Simple" Then
            Settings.SetIni("Map", "GenerateMaptiles", "True")
            Settings.SetIni("Map", "MapImageModule", "MapImageModule")  ' versus Warp3DImageModule
            Settings.SetIni("Map", "TextureOnMapTile", "False")         ' versus True
            Settings.SetIni("Map", "DrawPrimOnMapTile", "False")
            Settings.SetIni("Map", "TexturePrims", "False")
            Settings.SetIni("Map", "RenderMeshes", "False")
        ElseIf PropRegionClass.MapType(RegionUUID) = "Good" Then
            Settings.SetIni(simName, "GenerateMaptiles", "True")
            Settings.SetIni("Map", "MapImageModule", "Warp3DImageModule")  ' versus MapImageModule
            Settings.SetIni("Map", "TextureOnMapTile", "False")         ' versus True
            Settings.SetIni("Map", "DrawPrimOnMapTile", "False")
            Settings.SetIni("Map", "TexturePrims", "False")
            Settings.SetIni("Map", "RenderMeshes", "False")
        ElseIf PropRegionClass.MapType(RegionUUID) = "Better" Then
            Settings.SetIni("Map", "GenerateMaptiles", "True")
            Settings.SetIni("Map", "MapImageModule", "Warp3DImageModule")  ' versus MapImageModule
            Settings.SetIni("Map", "TextureOnMapTile", "True")         ' versus True
            Settings.SetIni("Map", "DrawPrimOnMapTile", "True")
            Settings.SetIni("Map", "TexturePrims", "False")
            Settings.SetIni("Map", "RenderMeshes", "False")
        ElseIf PropRegionClass.MapType(RegionUUID) = "Best" Then
            Settings.SetIni("Map", "GenerateMaptiles", "True")
            Settings.SetIni("Map", "MapImageModule", "Warp3DImageModule")  ' versus MapImageModule
            Settings.SetIni("Map", "TextureOnMapTile", "True")      ' versus True
            Settings.SetIni("Map", "DrawPrimOnMapTile", "True")
            Settings.SetIni("Map", "TexturePrims", "True")
            Settings.SetIni("Map", "RenderMeshes", "True")
        End If

        Select Case PropRegionClass.Physics(RegionUUID)
            Case ""
                Settings.SetIni("Startup", "meshing", "Meshmerizer")
                Settings.SetIni("Startup", "physics", "BulletSim")
                Settings.SetIni("Startup", "UseSeparatePhysicsThread", "True")
            Case "0"
                Settings.SetIni("Startup", "meshing", "ZeroMesher")
                Settings.SetIni("Startup", "physics", "basicphysics")
                Settings.SetIni("Startup", "UseSeparatePhysicsThread", "False")
            Case "1"
                Settings.SetIni("Startup", "meshing", "Meshmerizer")
                Settings.SetIni("Startup", "physics", "OpenDynamicsEngine")
                Settings.SetIni("Startup", "UseSeparatePhysicsThread", "False")
            Case "2"
                Settings.SetIni("Startup", "meshing", "Meshmerizer")
                Settings.SetIni("Startup", "physics", "BulletSim")
                Settings.SetIni("Startup", "UseSeparatePhysicsThread", "False")
            Case "3"
                Settings.SetIni("Startup", "meshing", "Meshmerizer")
                Settings.SetIni("Startup", "physics", "BulletSim")
                Settings.SetIni("Startup", "UseSeparatePhysicsThread", "True")
            Case "4"
                Settings.SetIni("Startup", "meshing", "ubODEMeshmerizer")
                Settings.SetIni("Startup", "physics", "ubODE")
                Settings.SetIni("Startup", "UseSeparatePhysicsThread", "False")
            Case "5"
                Settings.SetIni("Startup", "meshing", "Meshmerizer")
                Settings.SetIni("Startup", "physics", "ubODE")
                Settings.SetIni("Startup", "UseSeparatePhysicsThread", "False")
            Case Else
                ' do nothing
        End Select

        Select Case PropRegionClass.AllowGods(RegionUUID)
            Case ""
                Settings.SetIni("Permissions", "allow_grid_gods", CStr(Settings.AllowGridGods))
            Case "False"
                Settings.SetIni("Permissions", "allow_grid_gods", "False")
            Case "True"
                Settings.SetIni("Permissions", "allow_grid_gods", "True")
        End Select

        Select Case PropRegionClass.RegionGod(RegionUUID)
            Case ""
                Settings.SetIni("Permissions", "region_owner_is_god", CStr(Settings.RegionOwnerIsGod))
            Case "False"
                Settings.SetIni("Permissions", "region_owner_is_god", "False")
            Case "True"
                Settings.SetIni("Permissions", "region_owner_is_god", "True")
        End Select

        Select Case PropRegionClass.ManagerGod(RegionUUID)
            Case ""
                Settings.SetIni("Permissions", "region_manager_is_god", CStr(Settings.RegionManagerIsGod))
            Case "False"
                Settings.SetIni("Permissions", "region_manager_is_god", "False")
            Case "True"
                Settings.SetIni("Permissions", "region_manager_is_god", "True")
        End Select

        ' no main setting for these
        Settings.SetIni("SmartStart", "Enabled", PropRegionClass.SmartStart(RegionUUID))
        If PropRegionClass.DisallowForeigners(RegionUUID).Length > 0 Then
            Settings.SetIni("DisallowForeigners", "Enabled", Convert.ToString(PropRegionClass.DisallowForeigners(RegionUUID), Globalization.CultureInfo.InvariantCulture))
        End If

        If PropRegionClass.DisallowResidents(RegionUUID).Length > 0 Then
            Settings.SetIni("DisallowResidents", "Enabled", Convert.ToString(PropRegionClass.DisallowResidents(RegionUUID), Globalization.CultureInfo.InvariantCulture))
        End If

        ' V3.15
        If PropRegionClass.NonPhysicalPrimMax(RegionUUID).Length > 0 Then
            Settings.SetIni("Startup", "NonPhysicalPrimMax", Convert.ToString(PropRegionClass.NonPhysicalPrimMax(RegionUUID), Globalization.CultureInfo.InvariantCulture))
        End If

        If PropRegionClass.PhysicalPrimMax(RegionUUID).Length > 0 Then
            Settings.SetIni("Startup", "PhysicalPrimMax", Convert.ToString(PropRegionClass.PhysicalPrimMax(RegionUUID), Globalization.CultureInfo.InvariantCulture))
        End If

        If PropRegionClass.MinTimerInterval(RegionUUID).Length > 0 Then
            Settings.SetIni("XEngine", "MinTimerInterval", Convert.ToString(PropRegionClass.MinTimerInterval(RegionUUID), Globalization.CultureInfo.InvariantCulture))
        End If

        If PropRegionClass.FrameTime(RegionUUID).Length > 0 Then
            Settings.SetIni("Startup", "FrameTime", Convert.ToString(PropRegionClass.FrameTime(RegionUUID), Globalization.CultureInfo.InvariantCulture))
        End If

        If PropRegionClass.DisableGloebits(RegionUUID) = "True" Then
            Settings.SetIni("Startup", "economymodule", "BetaGridLikeMoneyModule")
        End If

        ' Search
        Select Case PropRegionClass.Snapshot(RegionUUID)
            Case ""
                Settings.SetIni("DataSnapshot", "index_sims", CStr(Settings.SearchEnabled))
            Case "True"
                Settings.SetIni("DataSnapshot", "index_sims", "True")
            Case "False"
                Settings.SetIni("DataSnapshot", "index_sims", "False")
        End Select

        'ScriptEngine Overrides
        If PropRegionClass.ScriptEngine(RegionUUID) = "XEngine" Then
            Settings.SetIni("Startup", "DefaultScriptEngine", "XEngine")
            Settings.SetIni("XEngine", "Enabled", "True")
            Settings.SetIni("YEngine", "Enabled", "False")
        End If

        If PropRegionClass.ScriptEngine(RegionUUID) = "YEngine" Then
            Settings.SetIni("Startup", "DefaultScriptEngine", "YEngine")
            Settings.SetIni("XEngine", "Enabled", "False")
            Settings.SetIni("YEngine", "Enabled", "True")
        End If

        Settings.SaveINI(System.Text.Encoding.UTF8)

        Return False
    End Function

    Private Function DoRobust() As Boolean

        Print("->Set Robust")
        If Settings.ServerType = "Robust" Then
            ' Robust Process
            If Settings.LoadIni(PropOpensimBinPath & "bin\Robust.HG.ini", ";") Then
                Return True
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

            If Settings.SearchLocal Then
                Settings.SetIni("LoginService", "SearchURL", "${Const|BaseURL}:" & Convert.ToString(Settings.ApachePort, Globalization.CultureInfo.InvariantCulture) & "/Search/query.php")
            Else
                Settings.SetIni("LoginService", "SearchURL", "http://www.hyperica.com/Search/query.php")
            End If

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

            Settings.SaveINI(System.Text.Encoding.UTF8)

        End If

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
              .FileName = """" & PropMyFolder & "\NtSuspendProcess64.exe" & """"
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
        Catch ex As InvalidOperationException
            Print(My.Resources.NTSuspend)
        Catch ex As ComponentModel.Win32Exception
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

        Print("->Set Tides")
        Dim TideData As String = ""
        Dim TideFile = PropOpensimBinPath & "bin\addon-modules\OpenSimTide\config\OpenSimTide.ini"
        Try
            System.IO.File.Delete(TideFile)
        Catch ex As IOException
        Catch ex As UnauthorizedAccessException
        Catch ex As ArgumentNullException
        Catch ex As ArgumentException
        End Try

        For Each RegionUUID As String In PropRegionClass.RegionUUIDs
            Dim simName = PropRegionClass.RegionName(RegionUUID)
            'Tides Setup per region
            If Settings.TideEnabled And PropRegionClass.Tides(RegionUUID) = "True" Then

                TideData = TideData & ";; Set the Tide settings per named region" & vbCrLf &
                    "[" & simName & "]" & vbCrLf &
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

    Private Function DoTos() As Boolean

        ' TOSModule is disabled in Grids
        If (False) Then
            If Settings.LoadIni(PropOpensimBinPath & "bin\DivaTOS.ini", ";") Then
                Return True
            End If

            'Disable it as it is broken for now.

            'Settings.SetIni("TOSModule", "Enabled", Settings.TOSEnabled)
            Settings.SetIni("TOSModule", "Enabled", CStr(False))
            'Settings.SetIni("TOSModule", "Message", Settings.TOSMessage)
            'Settings.SetIni("TOSModule", "Timeout", Settings.TOSTimeout)
            Settings.SetIni("TOSModule", "ShowToLocalUsers", CStr(Settings.ShowToLocalUsers))
            Settings.SetIni("TOSModule", "ShowToForeignUsers", CStr(Settings.ShowToForeignUsers))
            Settings.SetIni("TOSModule", "TOS_URL", "http://" & Settings.PublicIP & ":" & Settings.HttpPort & "/wifi/termsofservice.html")
            Settings.SaveINI(System.Text.Encoding.UTF8)
        End If
        Return False
    End Function

    Private Function DoWifi() As Boolean

        If Settings.LoadIni(PropOpensimBinPath & "bin\Wifi.ini", ";") Then Return True

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

    Private Sub DutchToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DutchToolStripMenuItem.Click
        Settings.Language = "nl-NL"
        Language(sender, e)
    End Sub

    Private Function EditForeigners() As Boolean

        Print("->Set Residents/Foreigners")
        ' adds a list like 'Region_Test_1 = "DisallowForeigners"' to Gridcommon.ini

        Dim Authorizationlist As String = ""
        For Each RegionUUID As String In PropRegionClass.RegionUUIDs

            Dim simName = PropRegionClass.RegionName(RegionUUID)
            '(replace spaces with underscore)
            simName = simName.Replace(" ", "_")    ' because this is a screwy thing they did in the INI file
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
                Authorizationlist += "Region_" & simName & " = DisallowResidents" & vbCrLf
            ElseIf Not dr And df Then
                Authorizationlist += "Region_" & simName & " = DisallowForeigners" & vbCrLf
            ElseIf dr And df Then
                Authorizationlist += "Region_" & simName & " = DisallowResidents " & vbCrLf
            End If
            'Application.doevents()
        Next

        Dim reader As StreamReader
        Dim line As String
        Dim Output As String = ""

        reader = System.IO.File.OpenText(PropOpensimBinPath & "bin\config-include\GridCommon.ini")
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

        FileStuff.DeleteFile(PropOpensimBinPath & "bin\config-include\GridCommon.ini")

        Using outputFile As New StreamWriter(PropOpensimBinPath & "bin\config-include\Gridcommon.ini")
            outputFile.Write(Output)
        End Using

        Return False

    End Function

    Private Sub EnglishToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EnglishToolStripMenuItem.Click
        Settings.Language = "en-US"
        Language(sender, e)
    End Sub

    Private Sub ErrorToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ErrorToolStripMenuItem.Click
        SendMsg("error")
    End Sub

    Private Sub ExitHandlerPoll()

        If PropAborting Then Return ' not if we are aborting

        If PropRestartRobust And PropRobustExited = True Then
            PropRobustExited = False
            RobustPictureBox.Image = My.Resources.nav_plain_red
            ToolTip1.SetToolTip(RobustPictureBox, My.Resources.Robust_exited)

            If Not CheckRobust() Then
                StartRobust()
                Return
            End If
        End If
        ' From the cross-threaded exited function. These can only be set if Settings.RestartOnCrash is true
        If PropMysqlExited Then
            MysqlPictureBox.Image = My.Resources.nav_plain_red
            ToolTip1.SetToolTip(MysqlPictureBox, My.Resources.MySql_Exited)
            StartMySQL()
            Return
        End If

        If PropApacheExited Then
            ApachePictureBox.Image = My.Resources.nav_plain_red
            ToolTip1.SetToolTip(ApachePictureBox, My.Resources.Apache_Exited)
            StartApache()
            Return
        End If

        If PropIceCastExited Then
            IceCastPicturebox.Image = My.Resources.nav_plain_red
            ToolTip1.SetToolTip(ApachePictureBox, My.Resources.Apache_Exited)
            StartIcecast()
            Return
        End If

        Dim GroupName As String
        Dim TimerValue As Integer

        For Each RegionUUID As String In PropRegionClass.RegionUUIDs

            ' count up to auto restart , when high enough, restart the sim
            If PropRegionClass.Timer(RegionUUID) >= 0 Then
                PropRegionClass.Timer(RegionUUID) = PropRegionClass.Timer(RegionUUID) + 1
            End If
            GroupName = PropRegionClass.GroupName(RegionUUID)

            ' too long running, possible shutdown
            If PropOpensimIsRunning() And Not PropAborting And PropRegionClass.Timer(RegionUUID) >= 0 Then
                TimerValue = PropRegionClass.Timer(RegionUUID)

                ' if it is past time and no one is in the sim... Smart shutdown
                If PropRegionClass.SmartStart(RegionUUID) = "True" And Settings.SmartStart And (TimerValue * 6) >= 60 And Not AvatarsIsInGroup(GroupName) Then
                    DoSuspend_Resume(PropRegionClass.RegionName(RegionUUID))
                End If

                ' auto restart timer
                If (TimerValue / 30) >= (Settings.AutoRestartInterval()) _
                    And Settings.AutoRestartInterval() > 0 _
                    And Not AvatarsIsInGroup(GroupName) _
                    And PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.Booted Then
                    ' shut down the group when one minute has gone by, or multiple thereof.

                    If ShowDOSWindow(GetHwnd(GroupName), SHOWWINDOWENUM.SWRESTORE) Then
                        SequentialPause()
                        ConsoleCommand(RegionUUID, "q{ENTER}" & vbCrLf)
                        Print(My.Resources.Automatic_restart_word & GroupName)
                        ' shut down all regions in the DOS box
                        For Each UUID In PropRegionClass.RegionUUIDListByName(GroupName)
                            PropRegionClass.Timer(UUID) = RegionMaker.REGIONTIMER.Stopped
                            PropRegionClass.Status(UUID) = RegionMaker.SIMSTATUSENUM.RecyclingDown
                        Next
                    Else
                        ' shut down all regions in the DOS box
                        For Each UUID In PropRegionClass.RegionUUIDListByName(GroupName)
                            PropRegionClass.Timer(UUID) = RegionMaker.REGIONTIMER.Stopped
                            PropRegionClass.Status(UUID) = RegionMaker.SIMSTATUSENUM.Stopped
                        Next
                    End If
                    PropUpdateView = True ' make form refresh
                End If
            End If

        Next

        ' Exit Handler

        ' now look at the exit stack
        If PropExitList.Count = 0 Then Return
        If PropExitHandlerIsBusy Then Return
        PropExitHandlerIsBusy = True
        Dim RegionName As String = Nothing
        While PropExitList.Count > 0
            Try
                RegionName = PropExitList(0).ToString()
                PropExitList.RemoveAt(0)
            Catch ex As Exception
            End Try

            Dim RegionList = PropRegionClass.RegionUUIDListByName(RegionName)
            ' Need a region number and a Name. Name is either a region or a Group. For groups we
            ' need to get a region name from the group
            GroupName = RegionName ' assume a group
            Dim RegionUUID As String = PropRegionClass.FindRegionByName(RegionName)

            If RegionUUID.Length > 0 Then
                GroupName = PropRegionClass.GroupName(RegionUUID) ' Yup, Get Name of the Dos box
            Else
                ' Nope, grab the first region, Group name is already set
                RegionUUID = RegionList(0)

            End If
            RegionList = PropRegionClass.RegionUUIDListByName(GroupName)
            If GroupName.Length = 0 Then Continue While ' may have been deleted

            If PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.Stopped Then
                ' already stoppped, from exit event or JSON via Region Ready module
                Continue While
            End If
            Print(RegionName & " " & My.Resources.Shutdown_word)
            Dim Status = PropRegionClass.Status(RegionUUID)
            TimerValue = PropRegionClass.Timer(RegionUUID)

            'Auto restart phase begins
            If PropOpensimIsRunning() And Status = RegionMaker.SIMSTATUSENUM.RecyclingDown Then
                Print(My.Resources.Restart_Queued_for_word & " " & GroupName)
                For Each UUID In RegionList
                    PropRegionClass.Status(UUID) = RegionMaker.SIMSTATUSENUM.RestartPending
                Next
                PropUpdateView = True ' make form refresh
                Continue While
            End If

            ' if a resume is signaled, unsuspend it
            If PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.Resume And Not PropAborting Then
                DoSuspend_Resume(PropRegionClass.RegionName(RegionUUID), True)
                PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.Booted
                PropUpdateView = True
                Continue While
            End If

            ' if a RestartPending is signaled, boot it up
            If PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.RestartPending And Not PropAborting Then
                Boot(PropRegionClass, PropRegionClass.RegionName(RegionUUID))
                PropUpdateView = True
                Continue While
            End If

            If Status = RegionMaker.SIMSTATUSENUM.ShuttingDown Then
                PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.Stopped
                PropUpdateView = True
                Continue While
            End If

            ' Maybe we crashed during warm up or running. Skip prompt if auto restart on crash and restart the beast
            If (Status = RegionMaker.SIMSTATUSENUM.RecyclingUp _
                Or Status = RegionMaker.SIMSTATUSENUM.Booting) _
                Or PropRegionClass.IsBooted(RegionUUID) _
                And TimerValue >= 0 _
                And PropRestartNow = False Then

                If Settings.RestartOnCrash Then
                    ' shut down all regions in the DOS box
                    Print(GroupName & " " & My.Resources.Quit_unexpectedly)
                    For Each UUID In PropRegionClass.RegionUUIDListByName(GroupName)
                        PropRegionClass.Timer(UUID) = RegionMaker.REGIONTIMER.Stopped
                        PropRegionClass.Status(UUID) = RegionMaker.SIMSTATUSENUM.RestartPending
                    Next
                    PropUpdateView = True
                Else
                    Print(GroupName & " " & My.Resources.Quit_unexpectedly)
                    Dim yesno = MsgBox(GroupName & " " & My.Resources.Quit_unexpectedly & " " & My.Resources.See_Log, vbYesNo, My.Resources.Error_word)
                    If (yesno = vbYes) Then
                        Try
                            System.Diagnostics.Process.Start(PropMyFolder & "\baretail.exe", """" & PropRegionClass.IniPath(RegionUUID) & "Opensim.log" & """")
                        Catch ex As InvalidOperationException
                        Catch ex As System.ComponentModel.Win32Exception
                        End Try
                    End If
                    StopGroup(GroupName)
                    PropUpdateView = True
                End If
            End If

        End While

        PropExitHandlerIsBusy = False

    End Sub

    Private Sub Fatal1_Click(sender As Object, e As EventArgs) Handles Fatal1.Click
        SendMsg("fatal")
    End Sub

    Private Sub FinnishToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FinnishToolStripMenuItem.Click
        Settings.Language = "fi"
        Language(sender, e)
    End Sub

    Private Sub Form1_Closed(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Closed
        ReallyQuit()
    End Sub

    ''' <summary>Fires when the form changes size or position</summary>
    Private Sub Form1_Layout(sender As Object, e As LayoutEventArgs) Handles Me.Layout
        Dim Y = Me.Height - 100
        TextBox1.Size = New Size(TextBox1.Size.Width, Y)
    End Sub

    ''' <summary>Form Load is main() for all DreamGrid</summary>
    ''' <param name="sender">Unused</param>
    ''' <param name="e">Unused</param>
    Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        Me.Hide()

        Application.EnableVisualStyles()
        ' setup a debug path
        PropMyFolder = My.Application.Info.DirectoryPath

        If Debugger.IsAttached Then
            ' for debugging when compiling
            PropDebug = True
            PropMyFolder = PropMyFolder.Replace("\Installer_Src\Setup DreamWorld\bin\Debug", "")
            PropMyFolder = PropMyFolder.Replace("\Installer_Src\Setup DreamWorld\bin\Release", "")
            ' for testing, as the compiler buries itself in ../../../debug
            Log("Startup:", DisplayObjectInfo(Me))
        End If



        PropCurSlashDir = PropMyFolder.Replace("\", "/")    ' because MySQL uses Unix like slashes, that's why
        PropOpensimBinPath() = PropMyFolder & "\OutworldzFiles\Opensim\"

        If Not System.IO.File.Exists(PropMyFolder & "\OutworldzFiles\Settings.ini") Then
            Print(My.Resources.Install_Icon)
            Create_ShortCut(PropMyFolder & "\Start.exe")
            PropViewedSettings = True
        End If

        Settings.Init(PropMyFolder)
        Settings.Myfolder = PropMyFolder
        Settings.OpensimBinPath = PropOpensimBinPath

        My.Application.ChangeUICulture(Settings.Language)
        My.Application.ChangeCulture(Settings.Language)

        Me.Controls.Clear() 'removes all the controls on the form
        InitializeComponent() 'load all the controls again
        FrmHome_Load(sender, e) 'Load everything in your form load event again

    End Sub

    Private Sub FrenchToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FrenchToolStripMenuItem.Click
        Settings.Language = "fr"
        Language(sender, e)
    End Sub

    Private Sub FrmHome_Load(ByVal sender As Object, ByVal e As EventArgs)

        SetScreen()     ' move Form to fit screen from SetXY.ini

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

        Me.Show()

        ContentOAR = New FormOAR
        ContentOAR.Init("OAR")

        ContentIAR = New FormOAR
        ContentIAR.Init("IAR")

        ' Save a random machine ID - we don't want any data to be sent that's personal or identifiable, but it needs to be unique
        Randomize()
        If Settings.MachineID().Length = 0 Then Settings.MachineID() = RandomNumber.Random  ' a random machine ID may be generated.  Happens only once

        ' WebUI
        ViewWebUI.Visible = Settings.WifiEnabled

        Me.Text += " V" & PropMyVersion

        PropOpensimIsRunning() = False ' true when opensim is running

        Print(My.Resources.Getting_regions_word)

        PropRegionClass = RegionMaker.Instance()
        PropInitted = True

        ClearLogFiles() ' clear log files

        If Not IO.File.Exists(PropMyFolder & "\BareTail.udm") Then
            IO.File.Copy(PropMyFolder & "\BareTail.udm.bak", PropMyFolder & "\BareTail.udm")
        End If

        GridNames.SetServerNames()

        CheckDefaultPorts()
        PropMyUPnpMap = New UPnp()

        SetQuickEditOff()

        SetLoopback()

        'mnuShow shows the DOS box for Opensimulator
        mnuShow.Checked = Settings.ConsoleShow
        mnuHide.Checked = Not Settings.ConsoleShow

        If SetIniData() Then
            Buttons(StartButton)
            Print(My.Resources.Stopped_word)
            Return
        End If

        With cpu
            .CategoryName = "Processor"
            .CounterName = "% Processor Time"
            .InstanceName = "_Total"
        End With

        CheckForUpdates()

        Print(My.Resources.Setup_Ports_word)
        RegionMaker.UpdateAllRegionPorts() ' must be after SetIniData

        'must start after region Class Is instantiated
        PropWebServer = NetServer.GetWebServer

        Print(My.Resources.Starting_WebServer_word)
        PropWebServer.StartServer(PropMyFolder, Settings)

        CheckDiagPort()

        mnuSettings.Visible = True

        LoadHelp()        ' Help loads once

        Print(My.Resources.RefreshingOAR)
        'SetIAROARContent() ' load IAR and OAR web content
        LoadLocalIAROAR() ' load IAR and OAR local content

        If Settings.Password = "secret" Then
            Dim Password = New PassGen
            Settings.Password = Password.GeneratePass()
        End If

        Print(My.Resources.Setup_Graphs_word)
        ' Graph fill
        Dim i = 0
        While i < 180
            MyCPUCollection(i) = 0
            i += 1
        End While

        Dim msChart = ChartWrapper1.TheChart
        msChart.ChartAreas(0).AxisX.Maximum = 180
        msChart.ChartAreas(0).AxisX.Minimum = 0
        msChart.ChartAreas(0).AxisY.Maximum = 100
        msChart.ChartAreas(0).AxisY.Minimum = 0
        msChart.ChartAreas(0).AxisY.LabelStyle.Enabled = True
        msChart.ChartAreas(0).AxisX.LabelStyle.Enabled = False
        ChartWrapper1.AddMarkers = True
        ChartWrapper1.MarkerFreq = 60

        i = 0
        While i < 180
            MyRAMCollection(i) = 0
            i += 1
        End While

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

        If MysqlInterface.IsMySqlRunning() Then PropStopMysql() = False

        If Settings.Autostart Then
            Print(My.Resources.Auto_Startup_word)
            Startup()
        Else
            Settings.SaveSettings()
            Print(My.Resources.Ready_to_Launch & vbCrLf & My.Resources.Click_Start_2_Begin & vbCrLf)
            Buttons(StartButton)
        End If

        HelpOnce("License") ' license on bottom
        HelpOnce("Startup")

    End Sub

    Private Sub GermanToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GermanToolStripMenuItem.Click
        Settings.Language = "de"
        Language(sender, e)
    End Sub

    Private Sub GetEvents()

        If Not Settings.SearchEnabled Then Return

        Dim Simevents As New Dictionary(Of String, String)
        Dim ctr As Integer = 0
        Try
            Using osconnection = New MySqlConnection(Settings.OSSearchConnectionString())
                Try
                    osconnection.Open()
                Catch ex As InvalidOperationException
#Disable Warning CA1303 ' Do not pass literals as localized parameters
                    Log(My.Resources.Error_word, "Failed to Connect to Search Database")
#Enable Warning CA1303 ' Do not pass literals as localized parameters
                    Return
                Catch ex As MySqlException
#Disable Warning CA1303 ' Do not pass literals as localized parameters
                    Log(My.Resources.Error_word, "Failed to Connect to Search Database")
#Enable Warning CA1303 ' Do not pass literals as localized parameters
                    Return
                End Try
                DeleteEvents(osconnection)

                Using client As New WebClient()
                    Dim Stream = client.OpenRead(PropDomain() & "/events.txt?r=" & RandomNumber.Random)
                    Using reader = New StreamReader(Stream)
                        While reader.Peek <> -1
                            Dim s = reader.ReadLine

                            ctr += 1
                            ' Split line on comma.
                            Dim array As String() = s.Split("|".ToCharArray())
                            Simevents.Clear()
                            ' Loop over each string received.
                            Dim part As String
                            For Each part In array
                                ' Display to console.
                                Dim a As String() = part.Split("^".ToCharArray())
                                If a.Length = 2 Then
                                    a(1) = a(1).Replace("'", "\'")
                                    a(1) = a(1).Replace("`", vbLf)
                                    'Console.WriteLine("{0}:{1}", a(0), a(1))
                                    Simevents.Add(a(0), a(1))
                                End If

                            Next
                            WriteEvent(osconnection, Simevents)
                        End While
                    End Using ' reader

                End Using ' client
            End Using ' osconnection
#Disable Warning CA1031 ' Do not catch general exception types
        Catch ex As Exception
#Enable Warning CA1031 ' Do not catch general exception types
            ErrorLog(ex.Message)
        End Try

    End Sub

    Private Sub GreekToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GreekToolStripMenuItem.Click
        Settings.Language = "el"
        Language(sender, e)
    End Sub

    Private Sub HebrewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HebrewToolStripMenuItem.Click
        Settings.Language = "he"
        Language(sender, e)
    End Sub

    Private Sub HelpClick(sender As ToolStripMenuItem, e As EventArgs)

        If sender.Text <> "Dreamgrid Manual.pdf" Then Help(sender.Text)

    End Sub

    Private Sub HelpOnIARSToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HelpOnIARSToolStripMenuItem.Click
        Dim webAddress As String = "http://opensimulator.org/wiki/Inventory_Archives"
        Try
            Process.Start(webAddress)
        Catch ex As InvalidOperationException
        Catch ex As System.ComponentModel.Win32Exception
        End Try
    End Sub

    Private Sub HelpOnOARsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HelpOnOARsToolStripMenuItem.Click
        Dim webAddress As String = "http://opensimulator.org/wiki/Load_Oar_0.9.0%2B"
        Try
            Process.Start(webAddress)
        Catch ex As InvalidOperationException
        Catch ex As System.ComponentModel.Win32Exception
        End Try
    End Sub

    Private Sub HelpStartingUpToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles HelpStartingUpToolStripMenuItem1.Click

        Help("Startup")

    End Sub

    Private Sub IarClick(sender As ToolStripMenuItem)

        If sender.Text = "Web Download Link" Then
            Dim webAddress As String = PropDomain & "/outworldz_installer/IAR"
            Try
                Process.Start(webAddress)
            Catch ex As InvalidOperationException
            Catch ex As System.ComponentModel.Win32Exception
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

    Private Sub IceCast_Exited(ByVal sender As Object, ByVal e As EventArgs) Handles IcecastProcess.Exited

        If PropAborting Then Return

        If Settings.RestartOnCrash And _IcecastCrashCounter < 10 Then
            _IcecastCrashCounter += 1
            PropIceCastExited = True
            Return
        End If
        _IcecastCrashCounter = 0

        Dim yesno = MsgBox(My.Resources.Icecast_Exited, vbYesNo, My.Resources.Error_word)

        If (yesno = vbYes) Then
            Dim IceCastLog As String = PropMyFolder & "\Outworldzfiles\Icecast\log\error.log"
            Try
                System.Diagnostics.Process.Start(PropMyFolder & "\baretail.exe", """" & IceCastLog & """")
            Catch ex As InvalidOperationException
            Catch ex As System.ComponentModel.Win32Exception
            End Try
        End If

    End Sub

    Private Sub IceCastPicturebox_Click(sender As Object, e As EventArgs) Handles IceCastPicturebox.Click

        If Not CheckIcecast() Then
            StartIcecast()
        Else
            StopIcecast()
        End If

    End Sub

    Private Sub IcelandicToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles IcelandicToolStripMenuItem.Click
        Settings.Language = "is"
        Language(sender, e)
    End Sub

    Private Sub Info_Click(sender As Object, e As EventArgs) Handles Info.Click
        SendMsg("info")
    End Sub

    Private Sub IrishToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles IrishToolStripMenuItem.Click
        Settings.Language = "ga"
        Language(sender, e)
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
                Print(My.Resources.Message_sent & ":" & CStr(HowManyAreOnline) & " regions")
            End If
        End If

    End Sub

    ''' <summary>The main starup - done this way so languages can reload the entire form</summary>
    Private Sub JustQuitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles JustQuitToolStripMenuItem.Click

        Print("Zzzz...")
        End

    End Sub

    Private Sub KillFiles(AL As List(Of String))

        For Each filename As String In AL
            FileStuff.DeleteFile(PropMyFolder & filename)

        Next

    End Sub

    Private Sub KillFolder(AL As List(Of String))

        For Each folder As String In AL
            Try
                System.IO.Directory.Delete(PropMyFolder & folder, True)
            Catch ex As IOException
            Catch ex As UnauthorizedAccessException
            Catch ex As ArgumentNullException
            Catch ex As ArgumentException
            End Try
        Next

    End Sub

    Private Sub KillOldFiles()

        Dim files As New List(Of String) From {
        "\Shoutcast", ' deprecated
        "\Icecast",   ' moved to Outworldzfiles
        "\Outworldzfiles\Opensim\bin\addins"
        }

        If PropKillSource Then
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

    Private Sub Language(sender As Object, e As EventArgs)
        Settings.SaveSettings()
        My.Application.ChangeUICulture(Settings.Language)
        My.Application.ChangeCulture(Settings.Language)
        Me.Controls.Clear() 'removes all the controls on the form
        InitializeComponent() 'load all the controls again
        FrmHome_Load(sender, e) 'Load everything in your form load event again
    End Sub

    Private Sub LoadFreeDreamGridOARsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles IslandToolStripMenuItem.Click
        If PropInitted Then
            ContentOAR.Activate()
            ContentOAR.ShowForm()
            ContentOAR.Select()
            ContentOAR.BringToFront()
        End If
    End Sub

    Private Sub LoadHelp()

        ' read help files for menu

        Dim folders As Array = Nothing
        Try
            folders = Directory.GetFiles(PropMyFolder & "\Outworldzfiles\Help")
        Catch ex As ArgumentException
        Catch ex As UnauthorizedAccessException
        Catch ex As DirectoryNotFoundException
        Catch ex As PathTooLongException
        Catch ex As IOException
        End Try

        For Each aline As String In folders
            If aline.EndsWith(".rtf", StringComparison.InvariantCultureIgnoreCase) Then
                aline = System.IO.Path.GetFileNameWithoutExtension(aline)
                Dim HelpMenu As New ToolStripMenuItem With {
                    .Text = aline,
                    .ToolTipText = My.Resources.Click_to_load,
                    .DisplayStyle = ToolStripItemDisplayStyle.Text,
                    .Image = My.Resources.question_and_answer
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

    Private Sub LoadInventoryIARToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoadInventoryIARToolStripMenuItem.Click

        If PropOpensimIsRunning() Then
            ' Create an instance of the open file dialog box. Set filter options and filter index.
            Dim openFileDialog1 As OpenFileDialog = New OpenFileDialog With {
                .InitialDirectory = """" & PropMyFolder & "/" & """",
                .Filter = My.Resources.IAR_Load_and_Save & " (*.iar)|*.iar|All Files (*.*)|*.*",
                .FilterIndex = 1,
                .Multiselect = False
            }

            ' Call the ShowDialog method to show the dialogbox.
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
        Dim Filename = PropMyFolder & "\OutworldzFiles\OAR\"
        Dim OARs As Array = Nothing
        Try
            OARs = Directory.GetFiles(Filename, "*.OAR", SearchOption.TopDirectoryOnly)
        Catch ex As ArgumentException
        Catch ex As UnauthorizedAccessException
        Catch ex As DirectoryNotFoundException
        Catch ex As PathTooLongException
        Catch ex As IOException
        End Try

        For Each OAR As String In OARs
            counter -= 1
            If counter > 0 Then
                Dim Name = Path.GetFileName(OAR)
                Dim OarMenu As New ToolStripMenuItem With {
                    .Text = Name,
                    .ToolTipText = My.Resources.Click_to_load,
                    .DisplayStyle = ToolStripItemDisplayStyle.Text
                }
                AddHandler OarMenu.Click, New EventHandler(AddressOf LocalOarClick)
                LoadLocalOARSToolStripMenuItem.Visible = True
                LoadLocalOARSToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {OarMenu})
            End If

        Next

        If Settings.BackupFolder = "AutoBackup" Then
            Filename = PropMyFolder & "\OutworldzFiles\AutoBackup\"
        Else
            Filename = Settings.BackupFolder
        End If

        Dim AutoOARs As Array = Nothing
        Try
            AutoOARs = Directory.GetFiles(Filename, "*.OAR", SearchOption.TopDirectoryOnly)
        Catch ex As ArgumentException
        Catch ex As UnauthorizedAccessException
        Catch ex As DirectoryNotFoundException
        Catch ex As PathTooLongException
        Catch ex As IOException
        End Try
        counter = MaxFileNum

        If AutoOARs IsNot Nothing Then
            For Each OAR As String In AutoOARs
                counter -= 1
                If counter > 0 Then
                    Dim Name = Path.GetFileName(OAR)
                    Dim OarMenu As New ToolStripMenuItem With {
                        .Text = Name,
                        .ToolTipText = My.Resources.Click_to_load,
                        .DisplayStyle = ToolStripItemDisplayStyle.Text
                    }
                    AddHandler OarMenu.Click, New EventHandler(AddressOf BackupOarClick)
                    LoadLocalOARSToolStripMenuItem.Visible = True
                    LoadLocalOARSToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {OarMenu})
                End If
            Next
        End If

        ' now for the IARs

        Filename = PropMyFolder & "\OutworldzFiles\IAR\"
        Dim IARs As Array = Nothing

        Try
            IARs = Directory.GetFiles(Filename, "*.IAR", SearchOption.TopDirectoryOnly)
        Catch ex As ArgumentException
        Catch ex As UnauthorizedAccessException
        Catch ex As DirectoryNotFoundException
        Catch ex As PathTooLongException
        Catch ex As IOException
        End Try

        LoadLocalIARsToolStripMenuItem.DropDownItems.Clear()

        counter = MaxFileNum
        For Each IAR As String In IARs
            counter -= 1
            If counter > 0 Then
                Dim Name = Path.GetFileName(IAR)
                Dim IarMenu As New ToolStripMenuItem With {
                    .Text = Name,
                    .ToolTipText = My.Resources.Click_to_load,
                    .DisplayStyle = ToolStripItemDisplayStyle.Text
                }
                AddHandler IarMenu.Click, New EventHandler(AddressOf LocalIarClick)
                LoadLocalIARsToolStripMenuItem.Visible = True
                LoadLocalIARsToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {IarMenu})

            End If

        Next

        If Settings.BackupFolder = "AutoBackup" Then
            Filename = PropMyFolder & "\OutworldzFiles\AutoBackup\"
        Else
            Filename = Settings.BackupFolder
        End If

        Dim AutoIARs As Array = Nothing
        Try
            AutoIARs = Directory.GetFiles(Filename, "*.IAR", SearchOption.TopDirectoryOnly)
        Catch ex As ArgumentException
        Catch ex As UnauthorizedAccessException
        Catch ex As DirectoryNotFoundException
        Catch ex As PathTooLongException
        Catch ex As IOException
        End Try
        If AutoIARs IsNot Nothing Then
            counter = MaxFileNum
            For Each IAR As String In AutoIARs
                counter -= 1
                If counter > 0 Then
                    Dim Name = Path.GetFileName(IAR)
                    Dim IarMenu As New ToolStripMenuItem With {
                    .Text = Name,
                    .ToolTipText = My.Resources.Click_to_load,
                    .DisplayStyle = ToolStripItemDisplayStyle.Text
                }
                    AddHandler IarMenu.Click, New EventHandler(AddressOf BackupIarClick)
                    LoadLocalIARsToolStripMenuItem.Visible = True
                    LoadLocalIARsToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {IarMenu})
                End If

            Next
        End If

    End Sub

    Private Sub LoadRegionOarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoadRegionOarToolStripMenuItem.Click

        If PropOpensimIsRunning() Then
            Dim chosen = ChooseRegion(True)
            If chosen.Length = 0 Then Return
            Dim RegionUUID As String = PropRegionClass.FindRegionByName(chosen)

            ' Create an instance of the open file dialog box. Set filter options and filter index.
            Using openFileDialog1 As OpenFileDialog = New OpenFileDialog With {
                .InitialDirectory = BackupPath(),
                .Filter = My.Resources.OAR_Load_and_Save & "(*.OAR,*.GZ,*.TGZ)|*.oar;*.gz;*.tgz;*.OAR;*.GZ;*.TGZ|All Files (*.*)|*.*",
                .FilterIndex = 1,
                .Multiselect = False
                }

                ' Call the ShowDialog method to show the dialogbox.
                Dim UserClickedOK As DialogResult = openFileDialog1.ShowDialog

                ' Process input if the user clicked OK.
                If UserClickedOK = DialogResult.OK Then

                    Dim offset = VarChooser(chosen)

                    Dim backMeUp = MsgBox(My.Resources.Make_a_backup_word, vbYesNo, My.Resources.Backup_word)
                    Dim thing = openFileDialog1.FileName
                    If thing.Length > 0 Then
                        thing = thing.Replace("\", "/")    ' because Opensim uses UNIX-like slashes, that's why

                        Dim Group = PropRegionClass.GroupName(RegionUUID)
                        For Each UUID In PropRegionClass.RegionUUIDListByName(Group)

                            ConsoleCommand(UUID, "change region " & chosen & "{ENTER}" & vbCrLf)
                            If backMeUp = vbYes Then
                                ConsoleCommand(UUID, "alert " & My.Resources.CPU_Intensive & "{Enter}" & vbCrLf)
                                ConsoleCommand(UUID, "save oar  " & """" & BackupPath() & "Backup_" & DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss", Globalization.CultureInfo.InvariantCulture) & ".oar" & """" & "{ENTER}" & vbCrLf)
                            End If
                            ConsoleCommand(UUID, "alert " & My.Resources.New_Content & "{ENTER}" & vbCrLf)

                            Dim ForceParcel As String = ""
                            If PropForceParcel() Then ForceParcel = " --force-parcels "
                            Dim ForceTerrain As String = ""
                            If PropForceTerrain Then ForceTerrain = " --force-terrain "
                            Dim ForceMerge As String = ""
                            If PropForceMerge Then ForceMerge = " --merge "
                            Dim UserName As String = ""
                            If PropUserName.Length > 0 Then UserName = " --default-user " & """" & PropUserName & """" & " "

                            ConsoleCommand(UUID, "load oar " & UserName & ForceMerge & ForceTerrain & ForceParcel & offset & """" & thing & """" & "{ENTER}" & vbCrLf)
                            ConsoleCommand(UUID, "alert " & My.Resources.New_is_Done & "{ENTER}" & vbCrLf)
                            ConsoleCommand(UUID, "generate map {ENTER}" & vbCrLf)
                        Next
                    End If
                End If

            End Using
        Else
            Print(My.Resources.Not_Running)
        End If

    End Sub

    Private Sub LocalIarClick(sender As ToolStripMenuItem, e As EventArgs)

        Dim File As String = PropMyFolder & "/OutworldzFiles/IAR/" & sender.Text 'make a real URL
        If LoadIARContent(File) Then
            Print(My.Resources.Opensimulator_is_loading & sender.Text)
        End If

    End Sub

    Private Sub LocalOarClick(sender As ToolStripMenuItem, e As EventArgs)

        Dim File = PropMyFolder & "/OutworldzFiles/OAR/" & sender.Text 'make a real URL
        If LoadOARContent(File) Then
            Print(My.Resources.Opensimulator_is_loading & sender.Text)
        End If

    End Sub

    Private Sub LogViewClick(sender As ToolStripMenuItem, e As EventArgs)

        Viewlog(sender.Text)

    End Sub

    Private Sub LoopBackToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoopBackToolStripMenuItem.Click

        Help("Loopback Fixes")

    End Sub

    Private Sub MakeMysql()

        Dim m As String = PropMyFolder & "\OutworldzFiles\Mysql\"
        If Not System.IO.File.Exists(m & "\Data\ibdata1") Then
            Print(My.Resources.Create_DB)
            Using zip As ZipFile = ZipFile.Read(m & "\Blank-Mysql-Data-folder.zip")
                For Each ZipEntry In zip

                    ZipEntry.Extract(m, Ionic.Zip.ExtractExistingFileAction.OverwriteSilently)
                Next
            End Using
        End If

    End Sub

    Private Function MapSetup() As Boolean

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
"?>"

        Using outputFile As New StreamWriter(PropMyFolder & "\OutworldzFiles\Apache\htdocs\MetroMap\includes\config.php", False)
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

        Using outputFile As New StreamWriter(PropMyFolder & "\OutworldzFiles\Apache\htdocs\Search\databaseinfo.php", False)
            outputFile.WriteLine(phptext)
        End Using
        Using outputFile As New StreamWriter(PropMyFolder & "\OutworldzFiles\PHP7\databaseinfo.php", False)
            outputFile.WriteLine(phptext)
        End Using

        Return False

    End Function

    Private Sub MnuAbout_Click(sender As System.Object, e As EventArgs) Handles mnuAbout.Click

        Print("(c) 2017 Outworldz,LLC" & vbCrLf & "Version " & PropMyVersion)
        Dim webAddress As String = PropDomain & "/Outworldz_Installer"
        Try
            Process.Start(webAddress)
        Catch ex As InvalidOperationException
        Catch ex As System.ComponentModel.Win32Exception
        End Try

    End Sub

    Private Sub MnuExit_Click(sender As System.Object, e As EventArgs) Handles mnuExit.Click
        ReallyQuit()
    End Sub

    Private Sub MnuHide_Click(sender As System.Object, e As EventArgs) Handles mnuHide.Click
        Print(My.Resources.Not_Shown)
        mnuShow.Checked = False
        mnuHide.Checked = True

        Settings.ConsoleShow = mnuShow.Checked
        Settings.SaveSettings()

    End Sub

    Private Sub MoreContentToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MoreContentToolStripMenuItem.Click

        Dim webAddress As String = PropDomain & "/cgi/freesculpts.plx"
        Try
            Process.Start(webAddress)
        Catch ex As InvalidOperationException
        Catch ex As System.ComponentModel.Win32Exception
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
        Dim MysqlLog As String = PropMyFolder & "\OutworldzFiles\mysql\data"
        Dim files As Array = Nothing
        Try
            files = Directory.GetFiles(MysqlLog, "*.err", SearchOption.TopDirectoryOnly)
        Catch ex As ArgumentException
        Catch ex As UnauthorizedAccessException
        Catch ex As DirectoryNotFoundException
        Catch ex As PathTooLongException
        Catch ex As IOException
        End Try

        If files.Length > 0 Then
            Dim yesno = MsgBox(My.Resources.MySql_Exited, vbYesNo, My.Resources.Error_word)
            If (yesno = vbYes) Then

                For Each FileName As String In files
                    Try
                        System.Diagnostics.Process.Start(PropMyFolder & "\baretail.exe", """" & FileName & """")
                    Catch ex As InvalidOperationException
                    Catch ex As System.ComponentModel.Win32Exception
                    End Try

                Next
            End If
        Else
            PropAborting = True
            MsgBox(My.Resources.Error_word, vbInformation, My.Resources.Error_word)
        End If

    End Sub

    Private Sub MysqlPictureBox_Click(sender As Object, e As EventArgs) Handles MysqlPictureBox.Click

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
                If RegisterName(newname).Length >= 0 Then

                    Settings.DNSName = newname
                    Settings.PublicIP = newname
                    Settings.SaveSettings()
                    MsgBox(My.Resources.NameAlreadySet, vbInformation, My.Resources.Information)
                End If
            End If

        End If

    End Sub

    Private Sub NorwegianToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NorwegianToolStripMenuItem.Click
        Settings.Language = "no"
        Language(sender, e)
    End Sub

    Private Sub OarClick(sender As ToolStripMenuItem)

        If sender.Text = "Web Download Link" Then
            Dim webAddress As String = PropDomain & "/outworldz_installer/OAR"
            Try
                Process.Start(webAddress)
            Catch ex As InvalidOperationException
            Catch ex As System.ComponentModel.Win32Exception
            End Try
            Return
        End If

        Dim File As String = Mid(CStr(sender.Text), 1, InStr(sender.Text, "|") - 2)
        File = PropDomain() & "/Outworldz_Installer/OAR/" & File 'make a real URL
        LoadOARContent(File)
        sender.Checked = True

    End Sub

    Private Sub Off1_Click(sender As Object, e As EventArgs) Handles Off1.Click
        SendMsg("off")
    End Sub

    Private Function OpenPorts() As Boolean

        If OpenRouterPorts() Then ' open UPnp port
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
        Dim webAddress As String = PropMyFolder & "\Outworldzfiles\Help\Dreamgrid Manual.pdf"
        Try
            Process.Start(webAddress)
        Catch ex As InvalidOperationException
        Catch ex As System.ComponentModel.Win32Exception
        End Try
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click

        If PictureBox1.AccessibleName = "Open".ToUpperInvariant Then
            Me.Width = 645
            Me.Height = 435
            PictureBox1.Image = My.Resources.Arrow2Left
            PictureBox1.AccessibleName = "Close".ToUpperInvariant
        Else
            PictureBox1.Image = My.Resources.Arrow2Right
            Me.Width = 385
            Me.Height = 240
            PictureBox1.AccessibleName = "Open".ToUpperInvariant
        End If

        Resize_page(sender, e)

    End Sub

    Private Sub PortgueseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PortgueseToolStripMenuItem.Click
        Settings.Language = "pt"
        Language(sender, e)
    End Sub

    Private Sub PortTest(Weblink As String, Port As Integer)

        Dim result As String = ""
        Using client As New WebClient
            Try
                result = client.DownloadString(Weblink)
            Catch ex As ArgumentNullException
                ErrorLog("Err:Loopback fail:" & result & ":" & ex.Message)
            Catch ex As WebException  ' not an error as could be a 404 from Diva being off
            Catch ex As NotSupportedException
                ErrorLog("Err:Loopback fail:" & result & ":" & ex.Message)
            End Try
        End Using

        If result.Contains("DOCTYPE") Or result.Contains("Ooops!") Or result.Length = 0 Then
            Print(My.Resources.Loopback_Passed & " " & Port.ToString(Globalization.CultureInfo.InvariantCulture))
        Else
            Print(My.Resources.Loopback_Failed & " " & Weblink)
            Settings.LoopBackDiag = False
            Settings.DiagFailed = True
        End If

    End Sub

    Private Sub ProbePublicPort()

        If Settings.ServerType <> "Robust" Then
            Return
        End If

        Dim isPortOpen As String = ""
        Using client As New WebClient ' download client for web pages

            ' collect some stats and test loopback with a HTTP_ GET to the webserver. Send unique, anonymous random ID, both of the versions of Opensim and this program, and the diagnostics test
            ' results See my privacy policy at https://outworldz.com/privacy.htm

            Print(My.Resources.Checking_Router_word)
            Dim Url = PropDomain() & "/cgi/probetest.plx?IP=" & Settings.PublicIP & "&Port=" & Settings.HttpPort & GetPostData()
            Try
                isPortOpen = client.DownloadString(Url)
            Catch ex As ArgumentNullException
                ErrorLog(My.Resources.Wrong & " " & ex.Message)
            Catch ex As WebException
                ErrorLog(My.Resources.Wrong & " " & ex.Message)
            Catch ex As NotSupportedException
                ErrorLog(My.Resources.Wrong & " " & ex.Message)
            End Try
        End Using

        If isPortOpen = "yes" Then
            Print(My.Resources.Incoming_Works)
        Else
            Settings.LoopBackDiag = False
            Settings.DiagFailed = True
            Print(My.Resources.Internet_address & " " & Settings.PublicIP & ":" & Settings.HttpPort & My.Resources.Not_Forwarded)
        End If

    End Sub

    Private Sub ReallyQuit()

        If Not KillAll() Then Return
        PropWebServer.StopWebServer()
        PropAborting = True
        StopMysql()

        Print("Zzzz...")
        Sleep(2000)
        End

    End Sub

    '' makes a list of teleports for the prims to use
    Private Sub RegionListHTML()

        'http://localhost:8002/bin/data/teleports.htm
        'Outworldz|Welcome||outworldz.com:9000:Welcome|128,128,96|
        '*|Welcome||outworldz.com9000Welcome|128,128,96|
        Dim HTML As String
        Dim HTMLFILE = PropOpensimBinPath & "bin\data\teleports.htm"
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
        Catch ex As IOException
        Catch ex As UnauthorizedAccessException
        Catch ex As ArgumentException
        Catch ex As System.Security.SecurityException
        Catch ex As ObjectDisposedException
        End Try

    End Sub

    Private Sub RegionsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RegionsToolStripMenuItem.Click
        ShowRegionform()
    End Sub

    Private Sub Resize_page(ByVal sender As Object, ByVal e As EventArgs)
        ScreenPosition.SaveXY(Me.Left, Me.Top)
        ScreenPosition.SaveHW(Me.Height, Me.Width)
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
            .Filter = My.Resources.Backup_Folder & "(*.sql)|*.sql|All Files (*.*)|*.*",
            .FilterIndex = 1,
            .Multiselect = False
        }

        ' Call the ShowDialog method to show the dialogbox.
        Dim UserClickedOK As DialogResult = openFileDialog1.ShowDialog

        ' Process input if the user clicked OK.
        If UserClickedOK = DialogResult.OK Then
            Dim thing = openFileDialog1.FileName
            If thing.Length > 0 Then

                Dim yesno = MsgBox(My.Resources.Are_You_Sure, vbYesNo, My.Resources.Restore_word)
                If yesno = vbYes Then

                    FileStuff.DeleteFile(PropMyFolder & "\OutworldzFiles\mysql\bin\RestoreMysql.bat")

                    Try
                        Dim filename As String = PropMyFolder & "\OutworldzFiles\mysql\bin\RestoreMysql.bat"
                        Using outputFile As New StreamWriter(filename, True)
                            outputFile.WriteLine("@REM A program to restore Mysql from a backup" & vbCrLf _
                                & "mysql -u root opensim <  " & """" & thing & """" _
                                & vbCrLf & "@pause" & vbCrLf)
                        End Using
#Disable Warning CA1031 ' Do not catch general exception types
                    Catch ex As Exception
#Enable Warning CA1031 ' Do not catch general exception types
                        ErrorLog("Failed to create restore file:" & ex.Message)
                        Return
                    End Try

                    Print(My.Resources.Do_Not_Interrupt_word)
                    Dim pMySqlRestore As Process = New Process()
                    ' pi.Arguments = thing
                    Dim pi As ProcessStartInfo = New ProcessStartInfo With {
                        .WindowStyle = ProcessWindowStyle.Normal,
                        .WorkingDirectory = PropMyFolder & "\OutworldzFiles\mysql\bin\",
                        .FileName = PropMyFolder & "\OutworldzFiles\mysql\bin\RestoreMysql.bat"
                    }
                    pMySqlRestore.StartInfo = pi

                    Try
                        pMySqlRestore.Start()
                    Catch ex As InvalidOperationException
                    Catch ex As System.ComponentModel.Win32Exception

                    End Try

                    Print(My.Resources.Do_Not_Interrupt_word)
                End If
            Else
                Print(My.Resources.Cancelled_word)
            End If
        End If
    End Sub

    Private Sub RevisionHistoryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RevisionHistoryToolStripMenuItem.Click

        Help("Revisions")

    End Sub

    Private Sub RobustPictureBox_Click(sender As Object, e As EventArgs) Handles RobustPictureBox.Click

        If Not CheckRobust() Then
            StartRobust()
        Else
            StopRobust()
        End If

    End Sub

    ' Handle Exited event and display process information.
    Private Sub RobustProcess_Exited(ByVal sender As Object, ByVal e As EventArgs) Handles RobustProcess.Exited

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
        RobustPictureBox.Image = My.Resources.nav_plain_red
        Dim yesno = MsgBox(My.Resources.Robust_exited, vbYesNo, My.Resources.Error_word)
        If (yesno = vbYes) Then
            Dim MysqlLog As String = PropOpensimBinPath & "bin\Robust.log"
            Try
                System.Diagnostics.Process.Start(PropMyFolder & "\baretail.exe", """" & MysqlLog & """")
            Catch ex As InvalidOperationException
            Catch ex As System.ComponentModel.Win32Exception
            End Try
        End If

    End Sub

    Private Sub RunDataSnapshot()

        If Not Settings.SearchLocal Then Return
        Diagnostics.Debug.Print("Scanning Data snapshot")
        Dim pi As ProcessStartInfo = New ProcessStartInfo()

        FileIO.FileSystem.CurrentDirectory = PropMyFolder & "\Outworldzfiles\Apache\htdocs\Search"
        pi.FileName = "Run_parser.bat"
        pi.UseShellExecute = False  ' needed to make window hidden
        pi.WindowStyle = ProcessWindowStyle.Hidden
        Dim ProcessPHP As Process = New Process With {
            .StartInfo = pi
        }
        ProcessPHP.StartInfo.CreateNoWindow = True
        Using ProcessPHP
            Try
                ProcessPHP.Start()
                ProcessPHP.WaitForExit()
            Catch ex As InvalidOperationException
                FileIO.FileSystem.CurrentDirectory = PropMyFolder
                ErrorLog("Error ProcessPHP failed to launch: " & ex.Message)
            Catch ex As System.ComponentModel.Win32Exception
                FileIO.FileSystem.CurrentDirectory = PropMyFolder
                ErrorLog("Error ProcessPHP failed to launch: " & ex.Message)
            End Try
        End Using

    End Sub

    Private Sub RussianToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RussianToolStripMenuItem.Click
        Settings.Language = "ru"
        Language(sender, e)
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
            Message = My.Resources.EnterName
            title = "Backup to OAR"
            defaultValue = chosen & "_" & DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss", Globalization.CultureInfo.InvariantCulture) & ".oar"

            ' Display message, title, and default value.
            myValue = InputBox(Message, title, defaultValue)
            ' If user has clicked Cancel, set myValue to defaultValue
            If myValue.Length = 0 Then Return

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

    Private Function ScanAgents() As Integer

        If Not MysqlInterface.IsMySqlRunning() Then Return 0

        ' Scan all the regions
        Dim sbttl As Integer = 0
        Dim A = GetAgentList()
        Dim B = GetHGAgentList()
        Dim C As New Dictionary(Of String, String)

        If Debugger.IsAttached Then
            Try
                A.Add("Ferd Frederix", "Welcome")
                B.Add("Nyira Machabelli", "SandBox")
            Catch ex As Exception
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
            CPortsProcess.StartInfo.FileName = PropMyFolder & "\Cports.exe"
            CPortsProcess.StartInfo.CreateNoWindow = False
            CPortsProcess.StartInfo.Arguments = ""
            CPortsProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal
            Try
                CPortsProcess.Start()
            Catch ex As InvalidOperationException
            Catch ex As System.ComponentModel.Win32Exception
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

    Private Function SetDefaultSims() As Boolean

        Print("->Set Default Sims")
        ' set the defaults in the INI for the viewer to use. Painful to do as it's a Left hand side edit must be done before other edits to Robust.HG.ini as this makes the actual Robust.HG.ifile
        Dim reader As StreamReader
        Dim line As String

        Try
            ' add this sim name as a default to the file as HG regions, and add the other regions as fallback it may have been deleted
            Dim WelcomeUUID As String = PropRegionClass.FindRegionByName(Settings.WelcomeRegion)

            If WelcomeUUID.Length = 0 Then
                MsgBox(My.Resources.Cannot_locate, vbInformation)
                Return True
            End If

            Dim DefaultName = Settings.WelcomeRegion

            FileStuff.DeleteFile(PropOpensimBinPath & "bin\Robust.HG.ini")

            ' Replace the block with a list of regions with the Region_Name = DefaultRegion, DefaultHGRegion is Welcome Region_Name = FallbackRegion, Persistent if a Snart Start region and SS is
            ' enabled Region_Name = FallbackRegion if not a SmartStart

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
                    RegionSetting += "Region_" & Settings.WelcomeRegion & " = " & """" & "DefaultRegion, DefaultHGRegion" & """" & vbCrLf
                End If

            Next

            Dim skip As Boolean = False
            Using outputFile As New StreamWriter(PropOpensimBinPath & "bin\Robust.HG.ini")
                reader = System.IO.File.OpenText(PropOpensimBinPath & "bin\Robust.HG.ini.proto")
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

                End While
            End Using
            'close your reader
            reader.Close()
#Disable Warning CA1031 ' Do not catch general exception types
        Catch ex As Exception
#Enable Warning CA1031 ' Do not catch general exception types
            MsgBox(My.Resources.no_Default_sim, vbInformation, My.Resources.Settings_word)
            Return True
        End Try

        ' needs to be set up after the above
        If DoRobust() Then Return True

        Return False

    End Function

    ''' <summary>Set up all INI files</summary>
    ''' <returns>true if it fails</returns>
    Private Function SetIniData() As Boolean

        Print(My.Resources.Creating_INI_Files_word)

        If SetDefaultSims() Then Return True
        If DoTos() Then Return True
        If DoGridCommon() Then Return True
        If EditForeigners() Then Return True
        If DelLibrary() Then Return True
        If DoFlotsamINI() Then Return True
        If DoOpensimINI() Then Return True
        If DoWifi() Then Return True
        If DoGloebits() Then Return True
        If DoTides() Then Return True
        If DoBirds() Then Return True
        If MapSetup() Then Return True
        If DoPHP() Then Return True
        If DoApache() Then Return True
        If SaveIceCast() Then Return True

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
                    LoopbackProcess.StartInfo.FileName = PropMyFolder & "\NAT_Loopback_Tool.bat"
                    LoopbackProcess.StartInfo.CreateNoWindow = False
                    LoopbackProcess.StartInfo.Arguments = "Loopback"
                    LoopbackProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
                    Try
                        LoopbackProcess.Start()
                        LoopbackProcess.WaitForExit()
                    Catch ex As InvalidOperationException
                    Catch ex As System.ComponentModel.Win32Exception
                    End Try
                    Exit For
                End Using
            End If
        Next

    End Sub

    Private Sub SetQuickEditOff()
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
            Catch ex As InvalidOperationException
            Catch ex As System.ComponentModel.Win32Exception
            End Try
        End Using

    End Sub

    ''' <summary>Sets H,W and pos of screen on load</summary>
    Private Sub SetScreen()
        '366, 236
        ScreenPosition = New ScreenPos("Form1")
        AddHandler ResizeEnd, Handler
        Dim xy As List(Of Integer) = ScreenPosition.GetXY()
        Left = xy.Item(0)
        Top = xy.Item(1)

        Dim hw As List(Of Integer) = ScreenPosition.GetHW()

        If hw.Item(0) = 0 Then
            Me.Height = 240
        Else
            Me.Height = hw.Item(0)
        End If

        If hw.Item(1) = 0 Then
            Me.Width = 385
        Else
            Me.Width = hw.Item(1)

            If Me.Width > 390 Then
                PictureBox1.Image = My.Resources.Arrow2Left
                PictureBox1.AccessibleName = "Close".ToUpperInvariant
            Else
                PictureBox1.Image = My.Resources.Arrow2Right
                PictureBox1.AccessibleName = "Open".ToUpperInvariant
            End If

        End If

        ScreenPosition.SaveHW(Me.Height, Me.Width)

    End Sub

    Private Sub SetupSearch()

        If Settings.ServerType <> "Robust" Then Return

        ' modify this to migrate search datbase upwards a rev
        If Not Settings.SearchMigration = 3 Then

            MysqlInterface.DeleteSearchDatabase()

            Print(My.Resources.Setup_search)
            Dim pi As ProcessStartInfo = New ProcessStartInfo()

            FileIO.FileSystem.CurrentDirectory = PropMyFolder & "\Outworldzfiles\mysql\bin\"
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
                Catch ex As InvalidOperationException
                    ErrorLog("Could not create Search Database: " & ex.Message)
                    FileIO.FileSystem.CurrentDirectory = PropMyFolder
                    Return
                Catch ex As System.ComponentModel.Win32Exception
                    ErrorLog("Could not create Search Database: " & ex.Message)
                    FileIO.FileSystem.CurrentDirectory = PropMyFolder
                    Return
                End Try
            End Using

            FileIO.FileSystem.CurrentDirectory = PropMyFolder

            Settings.SearchMigration = 3
            Settings.SaveSettings()

        End If

    End Sub

    Private Sub ShowHyperGridAddressToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowHyperGridAddressToolStripMenuItem.Click

        Print(My.Resources.Grid_Address_is_word & vbCrLf & "http://" & Settings.PublicIP & ":" & Settings.HttpPort)

    End Sub

    ''' <summary>Shows the log buttons if diags fail</summary>
    Private Sub ShowLog()
        Try
            System.Diagnostics.Process.Start(PropMyFolder & "\baretail.exe", """" & PropMyFolder & "\OutworldzFiles\Outworldz.log" & """")
        Catch ex As InvalidOperationException
        Catch ex As System.ComponentModel.Win32Exception
        End Try

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

        Settings.ConsoleShow = mnuShow.Checked
        Settings.SaveSettings()

    End Sub

    Private Sub ShowUserDetailsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowUserDetailsToolStripMenuItem.Click
        Dim person = InputBox(My.Resources.Enter_1_2)
        If person.Length > 0 Then
            ConsoleCommand("Robust", "show account " & person & "{ENTER}")
        End If
    End Sub

    Private Sub SpanishToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SpanishToolStripMenuItem.Click
        Settings.Language = "es-MX"
        Language(sender, e)
    End Sub

    ''' <summary>Start Button on main form</summary>
    Private Sub StartButton_Click(sender As System.Object, e As EventArgs) Handles StartButton.Click
        Startup()
    End Sub

    Private Sub Statmenu(sender As ToolStripMenuItem, e As EventArgs)
        If PropOpensimIsRunning() Then
            Dim RegionUUID As String = PropRegionClass.FindRegionByName(sender.Text)
            Dim port As String = CStr(PropRegionClass.RegionPort(RegionUUID))
            Dim webAddress As String = "http://localhost:" & Settings.HttpPort & "/bin/data/sim.html?port=" & port
            Try
                Process.Start(webAddress)
            Catch ex As InvalidOperationException
            Catch ex As System.ComponentModel.Win32Exception
            End Try
        Else
            Print(My.Resources.Not_Running)
        End If
    End Sub

    Private Sub StopAllRegions()

        For Each RegionUUID As String In PropRegionClass.RegionUUIDs
            PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.Stopped
            PropRegionClass.ProcessID(RegionUUID) = 0
            PropRegionClass.Timer(RegionUUID) = RegionMaker.REGIONTIMER.Stopped
        Next
        Try
            PropExitList.Clear()
            PropRegionHandles.Clear()
        Catch ex As NotSupportedException
        End Try

    End Sub

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
                Catch ex As InvalidOperationException
                    Print(My.Resources.ApacheNot_Stopping & ":" & ex.Message)
                Catch ex As System.ComponentModel.Win32Exception
                    Print(My.Resources.ApacheNot_Stopping & ":" & ex.Message)
                End Try

            End Using
        Else
            Zap("httpd")
            Zap("rotatelogs")
        End If

        ApachePictureBox.Image = My.Resources.nav_plain_red
        ToolTip1.SetToolTip(ApachePictureBox, My.Resources.Stopped_word)

    End Sub

    Private Sub StopButton_Click_1(sender As System.Object, e As EventArgs) Handles StopButton.Click

        Print(My.Resources.Stopping_word)
        Buttons(BusyButton)
        If Not KillAll() Then Return
        Buttons(StartButton)
        Print(My.Resources.Stopped_word)
        Buttons(StartButton)
        ToolBar(False)

    End Sub

    Private Sub StopIcecast()

        Zap("icecast")
        IceCastPicturebox.Image = My.Resources.nav_plain_red
        ToolTip1.SetToolTip(IceCastPicturebox, My.Resources.Stopped_word)

    End Sub

    Private Sub StopMysql()


        If Not MysqlInterface.IsMySqlRunning() Then
            'Application.doevents()
            MysqlInterface.IsRunning = False    ' mark all as not running
            MysqlPictureBox.Image = My.Resources.nav_plain_red
            ToolTip1.SetToolTip(MysqlPictureBox, My.Resources.Stopped_word)
            'Application.doevents()
            Return
        End If

        If Not PropStopMysql Then
            MysqlInterface.IsRunning = True    ' mark all as not running
            MysqlPictureBox.Image = My.Resources.nav_plain_green
            ToolTip1.SetToolTip(MysqlPictureBox, My.Resources.Running)
            'Application.doevents()
            Print(My.Resources.MySQL_Was_Running)
            Return
        End If

        Print(My.Resources.Stopping_word & " MySQL")

        Dim p As Process = New Process()
        Dim pi As ProcessStartInfo = New ProcessStartInfo With {
            .Arguments = "--port " & CStr(Settings.MySqlRobustDBPort) & " -u root shutdown",
            .FileName = """" & PropMyFolder & "\OutworldzFiles\mysql\bin\mysqladmin.exe" & """",
            .UseShellExecute = True, ' so we can redirect streams and minimize
            .WindowStyle = ProcessWindowStyle.Hidden
        }
        p.StartInfo = pi

        Try
            p.Start()
            MysqlInterface.IsRunning = False    ' mark all as not running
        Catch ex As InvalidOperationException
        Catch ex As System.ComponentModel.Win32Exception
        End Try
        'Application.doevents()
        p.WaitForExit()
        p.Close()
        MysqlPictureBox.Image = My.Resources.nav_plain_red
        ToolTip1.SetToolTip(MysqlPictureBox, My.Resources.Stopped_word)

        If MysqlInterface.IsMySqlRunning() Then
            MysqlInterface.IsRunning = True    ' mark all as not running
            MysqlPictureBox.Image = My.Resources.nav_plain_green
            ToolTip1.SetToolTip(MysqlPictureBox, My.Resources.Running)
            'Application.doevents()
        End If

        'Application.doevents()

    End Sub

    Private Function Stripqq(input As String) As String

        Return Replace(input, """", "")

    End Function

    Private Sub SwedishToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SwedishToolStripMenuItem.Click
        Settings.Language = "sv"
        Language(sender, e)
    End Sub

    Private Sub TechnicalInfoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TechnicalInfoToolStripMenuItem.Click
        Dim webAddress As String = PropDomain & "/Outworldz_installer/technical.htm"
        Try
            Process.Start(webAddress)
        Catch ex As InvalidOperationException
        Catch ex As System.ComponentModel.Win32Exception
        End Try
    End Sub

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

                Dim Port = PropRegionClass.GroupPort(RegionUUID)
                Print(My.Resources.Checking_Loopback_word & " " & RegionName)
                PortTest("http://" & Settings.PublicIP & ":" & Port & "/?_TestLoopback=" & RandomNumber.Random, Port)

            End If
        Next

    End Sub

    Private Sub TestPrivateLoopback()

        Dim result As String = ""
        Print(My.Resources.Checking_LAN_Loopback_word)
        Dim weblink = "http://" & Settings.PrivateURL & ":" & Settings.DiagnosticPort & "/?_TestLoopback=" & RandomNumber.Random()
        Using client As New WebClient
            Try
                result = client.DownloadString(weblink)
            Catch ex As ArgumentNullException
            Catch ex As WebException
            Catch ex As NotSupportedException
            End Try
        End Using

        If result = "Test Completed" Then
            Print(My.Resources.Passed_LAN)
        Else
            Print(My.Resources.Failed_LAN & " " & weblink)
            Settings.LoopBackDiag = False
            Settings.DiagFailed = True
        End If

    End Sub

    Private Sub TestPublicLoopback()

        If IPCheck.IsPrivateIP(Settings.PublicIP) Then
            Return
        End If

        If Settings.ServerType <> "Robust" Then

            Return
        End If
        Print(My.Resources.Checking_Loopback_word)
        PortTest("http://" & Settings.PublicIP & ":" & Settings.HttpPort & "/?_TestLoopback=" & RandomNumber.Random, Settings.HttpPort)

    End Sub

    Private Sub TextBox1_TextChanged(sender As System.Object, e As EventArgs) Handles TextBox1.TextChanged
        Dim ln As Integer = TextBox1.Text.Length
        TextBox1.SelectionStart = ln
        TextBox1.ScrollToCaret()
    End Sub

    Private Sub ThreadpoolsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ThreadpoolsToolStripMenuItem.Click
        For Each RegionUUID As String In PropRegionClass.RegionUUIDListByName("*")
            ConsoleCommand(RegionUUID, "show threads{ENTER}" & vbCrLf)
        Next
    End Sub

    ''' <summary>
    ''' Timer runs every second registers DNS,looks for web server stuff that arrives, restarts any sims , updates lists of agents builds teleports.html for older teleport checks for crashed regions
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As EventArgs) Handles Timer1.Tick

        Chart() ' do charts collection each second

        If Not PropOpensimIsRunning() Then
            Timer1.Stop()
            Return
        End If

        If PropAborting Then Return

        ' Check for a restart RegionRestart requires MOD 2 seconds to slow it down a bit
        If PropDNSSTimer Mod 2 = 0 Then
            PropRegionClass.CheckPost()
            ExitHandlerPoll() ' see if any regions have exited and set it up for Region Restart
        End If

        If PropDNSSTimer Mod 15 = 0 Then
            ScanAgents() ' update agent count each 15 seconds
            RegionListHTML() ' create HTML for older 2.4 region teleporters
        End If

        ' every 5 minutes
        If PropDNSSTimer Mod 300 = 0 Then
            CrashDetector.Find()
            RunDataSnapshot() ' Fetch assets marked for search- the Snapshot module itself only checks ever 10
        End If

        'hourly
        If PropDNSSTimer Mod 3600 = 0 Then
            RegisterDNS(True)
            LoadLocalIAROAR() ' refresh the pulldowns.            
        End If

        If Settings.EventTimerEnabled And PropDNSSTimer Mod 3600 = 0 Then
            GetEvents() ' get the events from the Outworldz main server for all grids
        End If

        PropDNSSTimer += 1

    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        Dim webAddress As String = PropDomain() & "/Outworldz_Installer/PortForwarding.htm"
        Try
            Process.Start(webAddress)
        Catch ex As InvalidOperationException
        Catch ex As System.ComponentModel.Win32Exception
        End Try
    End Sub

    Private Sub ToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem2.Click
        Print(My.Resources.StartUPNP)
        Dim pi As ProcessStartInfo = New ProcessStartInfo With {
                .Arguments = "",
                .FileName = PropMyFolder & "\UPnpPortForwardManager.exe",
                .WindowStyle = ProcessWindowStyle.Normal
            }
        Using ProcessUpnp As Process = New Process With {
                .StartInfo = pi
            }
            Try
                ProcessUpnp.Start()
            Catch ex As InvalidOperationException
                ErrorLog("ErrorUPnp failed to launch: " & ex.Message)
            Catch ex As System.ComponentModel.Win32Exception
                ErrorLog("ErrorUPnp failed to launch: " & ex.Message)
            End Try
        End Using

    End Sub

    Private Sub Trim()
        If TextBox1.Text.Length > TextBox1.MaxLength - 100 Then
            TextBox1.Text = Mid(TextBox1.Text, 500)
        End If
    End Sub

    Private Sub TroubleshootingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TroubleshootingToolStripMenuItem.Click

        Help("TroubleShooting")

    End Sub

    Private Sub UpdaterGo(Filename As String)

        KillAll()
        StopApache(True) 'really stop it, even if a service
        StopMysql()
        'Application.doevents()
        Dim pUpdate As Process = New Process()
        Dim pi As ProcessStartInfo = New ProcessStartInfo With {
            .Arguments = Filename,
            .FileName = """" & PropMyFolder & "\DreamGridSetup.exe" & """"
        }
        pUpdate.StartInfo = pi
        Print(My.Resources.SeeYouSoon)
        Try
            pUpdate.Start()
        Catch ex As InvalidOperationException
            ErrorLog(My.Resources.ErrInstall)
        Catch ex As ComponentModel.Win32Exception
            ErrorLog(My.Resources.ErrInstall)
        End Try
        End ' program

    End Sub

    Private Sub UpdaterProcess_Exited(ByVal sender As Object, ByVal e As EventArgs) Handles UpdateProcess.Exited

        Dim ExitCode = UpdateProcess.ExitCode
        If ExitCode = 0 Then
            UpdaterGo("DreamGrid-V" & Convert.ToString(Update_version, Globalization.CultureInfo.InvariantCulture) & ".zip")
        Else
            ErrorLog("ExitCode=" & CStr(ExitCode))
        End If

    End Sub

    Private Sub ViewIcecastWebPageToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ViewIcecastWebPageToolStripMenuItem.Click
        If PropOpensimIsRunning() And Settings.SCEnable Then
            Dim webAddress As String = "http://" & Settings.PublicIP & ":" & CStr(Settings.SCPortBase)
            Print(My.Resources.Icecast_Desc & webAddress & "/stream")
            Try
                Process.Start(webAddress)
            Catch ex As InvalidOperationException
            Catch ex As System.ComponentModel.Win32Exception
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
        SendMsg("warn")
    End Sub

    Private Sub XengineToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles XengineToolStripMenuItem.Click
        For Each RegionUUID As String In PropRegionClass.RegionUUIDListByName("*")
            ConsoleCommand(RegionUUID, "xengine status{ENTER}" & vbCrLf)
            'Application.doevents()
        Next
    End Sub

    ''' <summary>Kill processes by name</summary>
    ''' <param name="processName"></param>
    ''' <returns></returns>
    Private Sub Zap(processName As String)

        ' Kill process by name
        For Each P As Process In System.Diagnostics.Process.GetProcessesByName(processName)
            Log(My.Resources.Info, "Stopping process " & processName)
            Try
                P.Kill()
            Catch ex As NotSupportedException
            Catch ex As InvalidOperationException
            Catch ex As System.ComponentModel.Win32Exception
            End Try
            'Application.doevents()
        Next

    End Sub

#End Region

End Class
