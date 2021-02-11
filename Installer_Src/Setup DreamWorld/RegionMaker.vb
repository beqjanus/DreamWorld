#Region "Copyright AGPL3.0"

' Copyright Outworldz, LLC.
' AGPL3.0  https://opensource.org/licenses/AGPL

#End Region

Imports System.IO

Imports System.Text.RegularExpressions
Imports MySql.Data.MySqlClient
Imports Newtonsoft.Json

Public Class RegionMaker

#Region "Declarations"

#Disable Warning CA1051 ' Do not declare visible instance fields
    Public WebserverList As New List(Of String)
#Enable Warning CA1051 ' Do not declare visible instance fields
    Private Const Hyperica As String = "Hyperica"
    Private Const JOpensim As String = "JOpensim"
    Private Shared FInstance As RegionMaker
    Private ReadOnly _Grouplist As New Dictionary(Of String, Integer)
    ReadOnly Backup As New List(Of RegionMaker.Region_data)
    Private ReadOnly RegionList As New Dictionary(Of String, Region_data)

    Private _GetAllRegionsIsBusy As Boolean
    Private _RegionListIsInititalized As Boolean
    Dim json As New JSONresult

    Public Enum SIMSTATUSENUM As Integer

        Stopped = 0
        Booting = 1
        Booted = 2
        RecyclingUp = 3
        RecyclingDown = 4
        ShuttingDown = 5
        RestartPending = 6
        RetartingNow = 7
        [Resume] = 8
        Suspended = 9
        [Error] = 10
        RestartStage2 = 11
        NoLogin = 13

    End Enum

#End Region

#Region "Instance"

    Public Shared ReadOnly Property Instance() As RegionMaker
        Get
            If (FInstance Is Nothing) Then
                FInstance = New RegionMaker()
            End If
            Return FInstance
        End Get
    End Property

#End Region

#Region "Public Properties"

    Public Property GetAllRegionsIsBusy As Boolean
        Get
            Return _GetAllRegionsIsBusy
        End Get
        Set(value As Boolean)
            _GetAllRegionsIsBusy = value
        End Set
    End Property

#End Region

#Region "Start/Stop"

    Public Sub ClearStack()

        WebserverList.Clear()

    End Sub

    Public Function Init() As Boolean

        If GetAllRegions() = -1 Then Return False
        If RegionCount() = 0 Then
            CreateRegion("Welcome")
            Settings.WelcomeRegion = "Welcome"
            WriteRegionObject("Welcome")
            Settings.SaveSettings()
            If GetAllRegions() = -1 Then Return False
        End If
        Debug.Print("Loaded " + CStr(RegionCount) + " Regions")
        Return True

    End Function

#End Region

#Region "Subs"

    Public Sub CheckPost()

        If WebserverList.Count = 0 Then Return

        While WebserverList.Count > 0

            Try
                Dim ProcessString As String = WebserverList(0) ' recover the PID as string
                WebserverList.RemoveAt(0)

                ' This search returns the substring between two strings, so the first index Is moved to the character just after the first string.
                Dim POST As String = Uri.UnescapeDataString(ProcessString)
                Dim first As Integer = POST.IndexOf("{", StringComparison.InvariantCulture)
                Dim last As Integer = POST.LastIndexOf("}", StringComparison.InvariantCulture)

                Dim rawJSON As String = ""
                If first > -1 And last > -1 Then
                    rawJSON = POST.Substring(first, last - first + 1)
                Else
                    Logger("RegionReady", "Malformed Web request: " & POST, "Restart")
                    Continue While
                End If

                Try
                    json = JsonConvert.DeserializeObject(Of JSONresult)(rawJSON)
                Catch ex As Exception
                    BreakPoint.Show(ex.Message)
                    Debug.Print(ex.Message)
                    Logger("RegionReady", "Malformed JSON: " & ProcessString, "Restart")
                    Continue While
                End Try

                ' rawJSON "{""alert"":""region_ready"",""login"":""disabled"",""region_name"":""Welcome"",""region_id"":""365d804a-0df1-46cf-8acf-4320a3df3fca""}" String rawJSON
                ' "{""alert"":""region_ready"",""login"":""enabled"",""region_name"":""Welcome"",""region_id"":""365d804a-0df1-46cf-8acf-4320a3df3fca""}" String rawJSON
                ' "{""alert"":""region_ready"",""login"":""shutdown"",""region_name"":""Welcome"",""region_id"":""365d804a-0df1-46cf-8acf-4320a3df3fca""}" String

                '2020-12-27 04:31:52:RegionReady: Enabled:Aurora
                '2020-12-27 04:31:52:RegionReady Heard: Aurora
                '2020-12-27 04:31:52:Ready : Aurora
                '2020-12-27 04:31:52:RegionReady Booted: Aurora

                If json.login = "enabled" Then

                    Logger("RegionReady: Enabled", json.region_name, "Restart")
                    Dim uuid As String = FindRegionByName(json.region_name)
                    If uuid.Length = 0 Then
                        Logger("RegionReady Error, no UUID", json.region_name, "Restart")
                        Continue While
                    End If

                    Dim GName = GroupName(uuid)

                    Dim GroupList = RegionUuidListByName(GName)
                    For Each RUUID As String In GroupList
                        If Not FormSetup.BootedList1.Contains(RUUID) Then
                            FormSetup.BootedList1.Add(RUUID)
                        End If
                    Next

                ElseIf json.login = "shutdown" Then

                    Logger("Shutdown", json.region_name, "Restart")
                    Continue While   ' this bit below interferes with restarting multiple regions in a DOS box

                ElseIf json.login = "disabled" Then

                    Logger("RegionReady", json.region_name & " disabled login", "Restart")
                    Continue While
                Else
                    Logger("RegionReady", "Unsupported method:" & json.login, "Restart")
                    Continue While
                End If
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
                Logger("RegionReady", "Exception:" & ex.Message, "Restart")
                WebserverList.Clear()
            End Try
        End While

        PropUpdateView() = True

    End Sub

    Public Function CreateRegion(name As String, Optional UUID As String = "") As String

        If String.IsNullOrEmpty(UUID) Then UUID = Guid.NewGuid().ToString

        Debug.Print("Create Region " + name)
        Dim r As New Region_data With {
            ._RegionName = name,
            ._RegionEnabled = True,
            ._UUID = UUID,
            ._SizeX = 256,
            ._SizeY = 256,
            ._CoordX = LargestX() + 8,
            ._CoordY = LargestY() + 0,
            ._RegionPort = 0,
            ._ProcessID = 0,
            ._AvatarCount = 0,
            ._Status = SIMSTATUSENUM.Stopped,
            ._Timer = Date.Now,
            ._NonPhysicalPrimMax = "1024",
            ._PhysicalPrimMax = "64",
            ._ClampPrimSize = False,
            ._MaxPrims = "15000",
            ._MaxAgents = "100",
            ._MapType = "",
            ._MinTimerInterval = "",
            ._GodDefault = "",
            ._AllowGods = "",
            ._RegionGod = "",
            ._ManagerGod = "",
            ._Birds = "",
            ._Tides = "",
            ._Teleport = "",
            ._RegionSnapShot = "",
            ._DisableGloebits = "",
            ._FrameTime = "",
            ._SkipAutobackup = "",
            ._ScriptEngine = "",
            ._RegionSmartStart = "0",
            ._CrashCounter = 0
        }

        RegionList.Add(r._UUID, r)
        'RegionDump()
        Debug.Print("Region count is " & CStr(RegionList.Count - 1))
        Return r._UUID

    End Function

    Public Sub DeleteRegion(UUID As String)

        If RegionList.ContainsKey(UUID) Then
            RegionList.Remove(UUID)
        End If

    End Sub

    Public Function FindBackupByName(Name As String) As Integer

        Dim i As Integer = 0
        For Each obj As Region_data In Backup
            If Name = obj._RegionName Then
                ' Debug.Print("Current Backup is " + obj._RegionName)
                Return i
            End If
            i += 1
        Next
        Return -1

    End Function

    Public Function GetAllRegions() As Integer

        If PropOpensimIsRunning Then Return 0

        Dim ctr = 600
        While GetAllRegionsIsBusy And ctr > 0
            Sleep(100)
            ctr -= 1
        End While

        GetAllRegionsIsBusy = True
        Try
            Backup.Clear()
            Dim pair As KeyValuePair(Of String, Region_data)

            For Each pair In RegionList
                Backup.Add(pair.Value)
            Next

            RegionList.Clear()

            Dim uuid As String = ""
            Dim folders() = Directory.GetDirectories(Settings.OpensimBinPath + "Regions")
            System.Array.Sort(folders)

            For Each FolderName As String In folders

                Dim regionfolders = Directory.GetDirectories(FolderName)
                For Each FileName As String In regionfolders

                    If FileName.EndsWith("DataSnapshot", StringComparison.InvariantCulture) Then Continue For

                    Dim fName = ""
                    Try
                        Dim inis() As String = Nothing
                        Try
                            inis = Directory.GetFiles(FileName, "*.ini", SearchOption.TopDirectoryOnly)
                        Catch ex As Exception
                            BreakPoint.Show(ex.Message)
                        End Try

                        For Each ini As String In inis
                            fName = System.IO.Path.GetFileNameWithoutExtension(ini)

                            Dim i = Settings.LoadIni(ini, ";")

                            uuid = CStr(Settings.GetIni(fName, "RegionUUID", "", "String"))
                            Dim SomeUUID As New Guid
                            If Not Guid.TryParse(uuid, SomeUUID) Then
                                MsgBox("Cannot read uuid in INI file for " & fName)
                                Return 0
                            End If

                            CreateRegion(fName, uuid)

                            ' we do not save the above as we are making a new one.
                            RegionEnabled(uuid) = CBool(Settings.GetIni(fName, "Enabled", "True", "Boolean"))

                            RegionPath(uuid) = ini ' save the path
                            FolderPath(uuid) = System.IO.Path.GetDirectoryName(ini)

                            Dim theEnd As Integer = FolderPath(uuid).LastIndexOf("\", StringComparison.InvariantCulture)
                            IniPath(uuid) = FolderPath(uuid).Substring(0, theEnd + 1)

                            Dim M As Match = Regex.Match(FolderName, ".*\\(.*)$")
                            If M.Groups(1).Success Then
                                Debug.Print(M.Groups(1).Value)
                                GroupName(uuid) = M.Groups(1).Value
                            Else
                                MsgBox("Cannot locate Dos Box name for  " & fName)
                                Return 0
                            End If

                            SizeX(uuid) = CInt(Settings.GetIni(fName, "SizeX", "256", "Integer"))
                            SizeY(uuid) = CInt(Settings.GetIni(fName, "SizeY", "256", "Integer"))

                            ' extended props V2.1
                            NonPhysicalPrimMax(uuid) = CStr(Settings.GetIni(fName, "NonPhysicalPrimMax", "1024", "Integer"))
                            PhysicalPrimMax(uuid) = CStr(Settings.GetIni(fName, "PhysicalPrimMax", "64", "Integer"))
                            ClampPrimSize(uuid) = CBool(Settings.GetIni(fName, "ClampPrimSize", "False", "Boolean"))
                            MaxPrims(uuid) = CStr(Settings.GetIni(fName, "MaxPrims", "15000", "Integer"))
                            MaxAgents(uuid) = CStr(Settings.GetIni(fName, "MaxAgents", "100", "Integer"))

                            ' Location is int,int format.
                            Dim C As String = CStr(Settings.GetIni(fName, "Location", RandomNumber.Between(1010, 990) & "," & RandomNumber.Between(2000, 1000)))
                            Dim parts As String() = C.Split(New Char() {","c}) ' split at the comma
                            CoordX(uuid) = CInt("0" & CStr(parts(0).Trim))
                            CoordY(uuid) = CInt("0" & CStr(parts(1).Trim))

                            ' options parameters coming from INI file can be blank!
                            MinTimerInterval(uuid) = CStr(Settings.GetIni(fName, "MinTimerInterval", "", "String"))
                            FrameTime(uuid) = CStr(Settings.GetIni(fName, "FrameTime", "", "String"))
                            RegionSnapShot(uuid) = CStr(Settings.GetIni(fName, "RegionSnapShot", "", "String"))
                            MapType(uuid) = CStr(Settings.GetIni(fName, "MapType", "", "String"))
                            Physics(uuid) = CStr(Settings.GetIni(fName, "Physics", "", "String"))
                            MaxPrims(uuid) = CStr(Settings.GetIni(fName, "MaxPrims", "", "String"))
                            GodDefault(uuid) = CStr(Settings.GetIni(fName, "GodDefault", "True", "String"))
                            AllowGods(uuid) = CStr(Settings.GetIni(fName, "AllowGods", "", "String"))
                            RegionGod(uuid) = CStr(Settings.GetIni(fName, "RegionGod", "", "String"))
                            ManagerGod(uuid) = CStr(Settings.GetIni(fName, "ManagerGod", "", "String"))
                            Birds(uuid) = CStr(Settings.GetIni(fName, "Birds", "", "String"))
                            Tides(uuid) = CStr(Settings.GetIni(fName, "Tides", "", "String"))
                            Teleport(uuid) = CStr(Settings.GetIni(fName, "Teleport", "", "String"))
                            DisableGloebits(uuid) = CStr(Settings.GetIni(fName, "DisableGloebits", "", "String"))
                            DisallowForeigners(uuid) = CStr(Settings.GetIni(fName, "DisallowForeigners", "", "String"))
                            DisallowResidents(uuid) = CStr(Settings.GetIni(fName, "DisallowResidents", "", "String"))
                            SkipAutobackup(uuid) = CStr(Settings.GetIni(fName, "SkipAutoBackup", "", "String"))
                            Snapshot(uuid) = CStr(Settings.GetIni(fName, "RegionSnapShot", "", "String"))
                            ScriptEngine(uuid) = CStr(Settings.GetIni(fName, "ScriptEngine", "", "String"))
                            GDPR(uuid) = CStr(Settings.GetIni(fName, "Publicity", "", "String"))

                            Select Case CStr(Settings.GetIni(fName, "SmartStart", "False", "String"))
                                Case "True"
                                    SmartStart(uuid) = "True"
                                Case "False"
                                    SmartStart(uuid) = "False"
                                Case Else
                                    SmartStart(uuid) = ""
                            End Select

                            RegionPort(uuid) = PropRegionClass.LargestPort
                            GroupPort(uuid) = RegionPort(uuid)

                            Diagnostics.Debug.Print("Assign Port:" & CStr(GroupPort(uuid)))

                            If _RegionListIsInititalized Then
                                ' restore backups of transient data
                                Dim o = FindBackupByName(fName)
                                If o >= 0 Then
                                    AvatarCount(uuid) = Backup(o)._AvatarCount
                                    ProcessID(uuid) = Backup(o)._ProcessID
                                    Status(uuid) = Backup(o)._Status
                                    Timer(uuid) = Backup(o)._Timer
                                    CrashCounter(uuid) = Backup(o)._CrashCounter
                                    If Backup(o)._GroupPort > 0 Then
                                        GroupPort(uuid) = Backup(o)._GroupPort
                                    End If
                                End If

                            End If
                            Settings.SaveINI(ini, System.Text.Encoding.UTF8)
                            Application.DoEvents()
                        Next
                    Catch ex As Exception
                        BreakPoint.Show(ex.Message)
                        MsgBox(My.Resources.Error_Region + fName + " : " + ex.Message, MsgBoxStyle.Information Or MsgBoxStyle.MsgBoxSetForeground, Global.Outworldz.My.Resources.Error_word)
                        ErrorLog("Err:Parse file " + fName + ":" + ex.Message)
                        GetAllRegionsIsBusy = False
                        Return -1
                    End Try

                Next
            Next

            _RegionListIsInititalized = True
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
            Debug.Print(ex.Message)
            GetAllRegionsIsBusy = False
            Return -1
        End Try
        GetAllRegionsIsBusy = False
        Return RegionList.Count

    End Function

    Public Function LargestPort() As Integer

        ' locate largest port
        Dim MaxNum As Integer = Settings.FirstRegionPort - 1
        Dim pair As KeyValuePair(Of String, Region_data)

        For Each pair In RegionList
            If pair.Value._RegionPort > MaxNum Then
                MaxNum = pair.Value._RegionPort
            End If
        Next

        Return MaxNum + 1

    End Function

    Public Function LargestX() As Integer

        ' locate largest global coords
        Dim Max As Integer
        Dim pair As KeyValuePair(Of String, Region_data)

        For Each pair In RegionList
            Dim val = pair.Value._CoordX
            If val > Max Then Max = val
        Next
        If Max = 0 Then
            Max = RandomNumber.Between(1010, 990)
        End If
        Return Max

    End Function

    Public Function LargestY() As Integer

        ' locate largest global coords
        Dim Max As Integer
        Dim pair As KeyValuePair(Of String, Region_data)

        For Each pair In RegionList
            Dim val = pair.Value._CoordY
            If val > Max Then Max = val
        Next
        If Max = 0 Then
            Max = RandomNumber.Between(1010, 990)
        End If
        Return Max

    End Function

    Function LowestPort() As Integer
        ' locate lowest port
        Dim Min As Integer = 65536
        Dim Portlist As New Dictionary(Of Integer, String)
        Dim pair As KeyValuePair(Of String, Region_data)

        For Each pair In RegionList
            Try
                Portlist.Add(pair.Value._RegionPort, pair.Value._RegionName)
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
            End Try
        Next

        If Portlist.Count = 0 Then
            Return 8004
        End If

        For Each thing In Portlist
            If thing.Key < Min Then
                Min = thing.Key ' Min is always the current value
            End If

        Next
        If Min = 65536 Then Return 8004

        Return Min
    End Function

    ''' <summary>Self setting Region Ports Iterate over all regions and set the ports from the starting value</summary>
    Public Sub UpdateAllRegionPorts()

        TextPrint("-> " & My.Resources.Updating_Ports_word)

        Dim Portnumber As Integer = Settings.FirstRegionPort()

        For Each uuid As String In RegionUuids()
            Dim Name = RegionName(uuid)
            Dim ini = Settings.LoadIni(RegionPath(uuid), ";")

            Settings.SetIni(Name, "InternalPort", CStr(Portnumber))
            RegionPort(uuid) = Portnumber

            GroupPort(uuid) = Portnumber
            Diagnostics.Debug.Print("Assign Port:" & CStr(GroupPort(uuid)))

            Settings.SaveINI(ini, System.Text.Encoding.UTF8)
            Portnumber += 1
        Next

        TextPrint(My.Resources.Setup_Firewall_word)
        Firewall.SetFirewall()   ' must be after UpdateAllRegionPorts

    End Sub

    Public Sub WriteRegionObject(name As String)

        Dim uuid As String = FindRegionByName(name)
        If uuid.Length = 0 Then
            MsgBox(My.Resources.Cannot_find_region_word & " " & name, MsgBoxStyle.Information Or MsgBoxStyle.MsgBoxSetForeground, Global.Outworldz.My.Resources.Error_word)
            Return
        End If

        Dim fname As String = RegionList(uuid)._FolderPath

        If (fname.Length = 0) Then
            Dim pathtoWelcome As String = Settings.OpensimBinPath + "\Regions\" + name + "\Region\"
            fname = pathtoWelcome + name + ".ini"
            If Not Directory.Exists(pathtoWelcome) Then
                Try
                    Directory.CreateDirectory(pathtoWelcome)
                Catch ex As Exception
                    BreakPoint.Show(ex.Message)
                End Try
            End If
        Else
            fname = fname + "\" + name + ".ini"
        End If

        Dim proto = "; * Regions configuration file; " & vbCrLf _
        & "; Automatically changed and read by Dreamworld. Edits are allowed" & vbCrLf _
        & "; Rule1: The File name must match the [RegionName]" & vbCrLf _
        & "; Rule2: Only one region per INI file." & vbCrLf _
        & ";" & vbCrLf _
        & "[" & name & "]" & vbCrLf _
        & "RegionUUID = " & uuid & vbCrLf _
        & "Location = " & CoordX(uuid).ToString(Globalization.CultureInfo.InvariantCulture) & "," & CoordY(uuid).ToString(Globalization.CultureInfo.InvariantCulture) & vbCrLf _
        & "InternalAddress = 0.0.0.0" & vbCrLf _
        & "InternalPort = " & RegionPort(uuid) & vbCrLf _
        & "AllowAlternatePorts = False" & vbCrLf _
        & "ExternalHostName = " & Settings.ExternalHostName() & vbCrLf _
        & "SizeX = " & CStr(SizeX(uuid)) & vbCrLf _
        & "SizeY = " & CStr(SizeY(uuid)) & vbCrLf _
        & "Enabled = " & CStr(RegionEnabled(uuid)) & vbCrLf _
        & "NonPhysicalPrimMax = " & CStr(NonPhysicalPrimMax(uuid)) & vbCrLf _
        & "PhysicalPrimMax = " & CStr(PhysicalPrimMax(uuid)) & vbCrLf _
        & "ClampPrimSize = " & CStr(ClampPrimSize(uuid)) & vbCrLf _
        & "MaxPrims = " & MaxPrims(uuid) & vbCrLf _
        & "RegionType = Estate" & vbCrLf _
        & "MaxAgents = 100" & vbCrLf & vbCrLf _
        & ";# Dreamgrid extended properties" & vbCrLf _
        & "RegionSnapShot = " & RegionSnapShot(uuid) & vbCrLf _
        & "MapType = " & MapType(uuid) & vbCrLf _
        & "Physics = " & Physics(uuid) & vbCrLf _
        & "GodDefault = " & GodDefault(uuid) & vbCrLf _
        & "AllowGods = " & AllowGods(uuid) & vbCrLf _
        & "RegionGod = " & RegionGod(uuid) & vbCrLf _
        & "ManagerGod = " & ManagerGod(uuid) & vbCrLf _
        & "Birds = " & Birds(uuid) & vbCrLf _
        & "Tides = " & Tides(uuid) & vbCrLf _
        & "Teleport = " & Teleport(uuid) & vbCrLf _
        & "DisableGloebits = " & DisableGloebits(uuid) & vbCrLf _
        & "DisallowForeigners = " & DisallowForeigners(uuid) & vbCrLf _
        & "DisallowResidents = " & DisallowResidents(uuid) & vbCrLf _
        & "MinTimerInterval =" & MinTimerInterval(uuid) & vbCrLf _
        & "Frametime =" & FrameTime(uuid) & vbCrLf _
        & "ScriptEngine =" & ScriptEngine(uuid) & vbCrLf _
        & "Publicity =" & GDPR(uuid) & vbCrLf _
        & "SmartStart =" & SmartStart(uuid) & vbCrLf

        DeleteFile(fname)

        Try
            Using outputFile As New StreamWriter(fname, True)
                outputFile.WriteLine(proto)
            End Using
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try

    End Sub

    Private Class JSONresult

#Region "Public Fields"

        Public alert As String
        Public login As String
        Public region_id As String
        Public region_name As String

#End Region

    End Class

#Region "Region_data"

    ' hold a copy of the Main region data on a per-form basis
    Private Class Region_data

#Region "Public Fields"

        Public _AvatarCount As Integer
        Public _ClampPrimSize As Boolean
        Public _CoordX As Integer = 1000
        Public _CoordY As Integer = 1000
        Public _DisallowForeigners As String = ""
        Public _DisallowResidents As String = ""
        Public _FolderPath As String = ""
        Public _Group As String = ""  ' the path to the folder that holds the region ini
        Public _IniPath As String = "" ' the folder name that holds the region(s), can be different named
        Public _ProcessID As Integer
        Public _RegionEnabled As Boolean = True
        Public _RegionName As String = ""
        Public _RegionPath As String = ""  ' The full path to the region ini file
        Public _RegionPort As Integer
        Public _SizeX As Integer = 256
        Public _SizeY As Integer = 256
        Public _Status As Integer
        Public _Timer As Date
        Public _UUID As String = ""

#End Region

#End Region

#Region "OptionalStorage"

        Public _AllowGods As String = ""
        Public _Birds As String = ""
        Public _CrashCounter As Integer
        Public _DisableGloebits As String = ""
        Public _FrameTime As String = ""
        Public _GDPR As String = ""
        Public _GodDefault As String = ""
        Public _GroupPort As Integer
        Public _ManagerGod As String = ""
        Public _MapType As String = ""
        Public _MaxAgents As String = ""
        Public _MaxPrims As String = ""
        Public _MinTimerInterval As String = ""
        Public _NonPhysicalPrimMax As String = ""
        Public _PhysicalPrimMax As String = ""
        Public _Physics As String = "  "
        Public _RegionGod As String = ""
        Public _RegionSmartStart As String = ""
        Public _RegionSnapShot As String = ""
        Public _ScriptEngine As String = ""
        Public _SkipAutobackup As String = ""
        Public _Snapshot As String = ""
        Public _Teleport As String = ""
        Public _Tides As String = ""

#End Region

    End Class

#End Region

#Region "Standard INI"

    Public Property AvatarCount(uuid As String) As Integer
        Get
            If uuid Is Nothing Then Return 0
            If Bad(uuid) Then Return 0
            Return RegionList(uuid)._AvatarCount
        End Get
        Set(ByVal Value As Integer)
            If uuid Is Nothing Then Return
            If Bad(uuid) Then Return
            RegionList(uuid)._AvatarCount = Value
        End Set
    End Property

    Public Property ClampPrimSize(uuid As String) As Boolean
        Get
            If uuid Is Nothing Then Return False
            If Bad(uuid) Then Return False
            Return RegionList(uuid)._ClampPrimSize
        End Get
        Set(ByVal Value As Boolean)
            If uuid Is Nothing Then Return
            If Bad(uuid) Then Return
            RegionList(uuid)._ClampPrimSize = Value
        End Set
    End Property

    Public Property CoordX(uuid As String) As Integer
        Get
            If uuid Is Nothing Then Return 0
            If Bad(uuid) Then Return 0
            Return RegionList(uuid)._CoordX
        End Get
        Set(ByVal Value As Integer)
            If uuid Is Nothing Then Return
            If Bad(uuid) Then Return
            RegionList(uuid)._CoordX = Value
        End Set
    End Property

    Public Property CoordY(uuid As String) As Integer
        Get
            If uuid Is Nothing Then Return 0
            If Bad(uuid) Then Return 0
            Return RegionList(uuid)._CoordY
        End Get
        Set(ByVal Value As Integer)
            If uuid Is Nothing Then Return
            If Bad(uuid) Then Return
            RegionList(uuid)._CoordY = Value
        End Set
    End Property

    Public Property CrashCounter(uuid As String) As Integer
        Get
            If uuid Is Nothing Then Return -1
            If Bad(uuid) Then Return -1
            Return RegionList(uuid)._CrashCounter
        End Get
        Set(ByVal Value As Integer)
            If uuid Is Nothing Then Return
            If Bad(uuid) Then Return
            RegionList(uuid)._CrashCounter = Value
        End Set
    End Property

    Public Property MaxAgents(uuid As String) As String
        Get
            If uuid Is Nothing Then Return "100"
            If Bad(uuid) Then Return "100"
            If String.IsNullOrEmpty(RegionList(uuid)._MaxAgents) Then RegionList(uuid)._MaxAgents = "100"

            Return RegionList(uuid)._MaxAgents
        End Get
        Set(ByVal Value As String)
            If uuid Is Nothing Then Return
            If Bad(uuid) Then Return
            RegionList(uuid)._MaxAgents = Value
        End Set
    End Property

    Public Property MaxPrims(uuid As String) As String
        Get
            If uuid Is Nothing Then Return "45000"
            If Bad(uuid) Then Return "45000"
            If String.IsNullOrEmpty(RegionList(uuid)._MaxPrims) Then
                RegionList(uuid)._MaxPrims = "45000"
            End If
            Return RegionList(uuid)._MaxPrims
        End Get
        Set(ByVal Value As String)
            If uuid Is Nothing Then Return
            If Bad(uuid) Then Return
            RegionList(uuid)._MaxPrims = Value
        End Set
    End Property

    Public Property NonPhysicalPrimMax(uuid As String) As String
        Get
            If uuid Is Nothing Then Return "1024"
            If Bad(uuid) Then Return "1024"
            Return RegionList(uuid)._NonPhysicalPrimMax
        End Get
        Set(ByVal Value As String)
            If uuid Is Nothing Then Return
            If Bad(uuid) Then Return
            RegionList(uuid)._NonPhysicalPrimMax = Value
        End Set
    End Property

    Public Property PhysicalPrimMax(uuid As String) As String
        Get
            If uuid Is Nothing Then Return "1024"
            If Bad(uuid) Then Return "1024"
            Return RegionList(uuid)._PhysicalPrimMax
        End Get
        Set(ByVal Value As String)
            If uuid Is Nothing Then Return
            If Bad(uuid) Then Return
            RegionList(uuid)._PhysicalPrimMax = Value
        End Set
    End Property

    Public Property ProcessID(uuid As String) As Integer
        Get
            If uuid Is Nothing Then Return 0
            If Bad(uuid) Then Return 0
            Return RegionList(uuid)._ProcessID
        End Get
        Set(ByVal Value As Integer)
            If uuid Is Nothing Then Return
            If Bad(uuid) Then Return
            RegionList(uuid)._ProcessID = Value
        End Set
    End Property

    Public Property SizeX(uuid As String) As Integer
        Get
            If uuid Is Nothing Then Return 256
            If Bad(uuid) Then Return 256
            Return RegionList(uuid)._SizeX
        End Get
        Set(ByVal Value As Integer)
            If uuid Is Nothing Then Return
            If Bad(uuid) Then Return
            RegionList(uuid)._SizeX = Value
        End Set
    End Property

    Public Property SizeY(uuid As String) As Integer
        Get
            If uuid Is Nothing Then Return 256
            If Bad(uuid) Then Return 256
            Return RegionList(uuid)._SizeY
        End Get
        Set(ByVal Value As Integer)
            If uuid Is Nothing Then Return
            If Bad(uuid) Then Return
            RegionList(uuid)._SizeY = Value
        End Set
    End Property

    Public Property Status(uuid As String) As Integer
        Get
            If uuid Is Nothing Then Return -1
            If Bad(uuid) Then Return -1
            Return RegionList(uuid)._Status
        End Get
        Set(ByVal Value As Integer)
            If uuid Is Nothing Then Return
            If Bad(uuid) Then Return
            RegionList(uuid)._Status = Value
        End Set
    End Property

    Public Property Timer(uuid As String) As Date
        Get
            Return RegionList(uuid)._Timer
        End Get
        Set(ByVal Value As Date)
            RegionList(uuid)._Timer = Value
        End Set
    End Property

#End Region

#Region "Properties"

    Public ReadOnly Property RegionCount() As Integer
        Get
            Return RegionList.Count
        End Get
    End Property

    Public Property FolderPath(uuid As String) As String
        Get
            If uuid Is Nothing Then Return ""
            If Bad(uuid) Then Return ""
            Return RegionList(uuid)._FolderPath
        End Get
        Set(ByVal Value As String)
            If uuid Is Nothing Then Return
            If Bad(uuid) Then Return
            RegionList(uuid)._FolderPath = Value
        End Set
    End Property

    Public Property GroupName(uuid As String) As String
        Get
            If uuid Is Nothing Then Return ""
            If Bad(uuid) Then Return ""
            Return RegionList(uuid)._Group
        End Get
        Set(ByVal Value As String)
            If uuid Is Nothing Then Return
            If Bad(uuid) Then Return
            RegionList(uuid)._Group = Value
        End Set
    End Property

    Public Property GroupPort(uuid As String) As Integer
        Get
            Dim GN As String = GroupName(uuid)
            If _Grouplist.ContainsKey(GN) Then
                Return _Grouplist.Item(GN)
            End If
            Return LargestPort()
        End Get

        Set(ByVal Value As Integer)

            Dim GN As String = GroupName(uuid)
            If _Grouplist.ContainsKey(GN) Then
                _Grouplist.Item(GN) = Value
            Else
                _Grouplist.Add(GN, Value)
            End If

        End Set
    End Property

    Public Property IniPath(uuid As String) As String
        Get
            If uuid Is Nothing Then Return ""
            If Bad(uuid) Then Return ""
            Return RegionList(uuid)._IniPath
        End Get
        Set(ByVal Value As String)
            If uuid Is Nothing Then Return
            If Bad(uuid) Then Return
            RegionList(uuid)._IniPath = Value
        End Set
    End Property

    Public Property RegionName(uuid As String) As String
        Get
            If uuid Is Nothing Then Return ""
            If Bad(uuid) Then Return ""
            Return RegionList(uuid)._RegionName
        End Get
        Set(ByVal Value As String)
            If uuid Is Nothing Then Return
            If Bad(uuid) Then Return
            RegionList(uuid)._RegionName = Value
        End Set
    End Property

    Public Property RegionPath(uuid As String) As String
        Get
            If uuid Is Nothing Then Return ""
            If Bad(uuid) Then Return ""
            Return RegionList(uuid)._RegionPath
        End Get
        Set(ByVal Value As String)
            If uuid Is Nothing Then Return
            If Bad(uuid) Then Return
            RegionList(uuid)._RegionPath = Value
        End Set
    End Property

    Public Property RegionPort(uuid As String) As Integer
        Get
            If uuid Is Nothing Then Return -1
            If Bad(uuid) Then Return -1
            Return RegionList(uuid)._RegionPort
        End Get
        Set(ByVal Value As Integer)
            If uuid Is Nothing Then Return
            If Bad(uuid) Then Return
            RegionList(uuid)._RegionPort = Value
        End Set
    End Property

#End Region

#Region "Options"

    Public Property AllowGods(uuid As String) As String
        Get
            If uuid Is Nothing Then Return ""
            If Bad(uuid) Then Return ""
            Return RegionList(uuid)._AllowGods
        End Get
        Set(ByVal Value As String)
            If uuid Is Nothing Then Return
            If Bad(uuid) Then Return
            RegionList(uuid)._AllowGods = Value
        End Set
    End Property

    Public Property Birds(uuid As String) As String
        Get
            If uuid Is Nothing Then Return ""
            If Bad(uuid) Then Return ""
            Return RegionList(uuid)._Birds
        End Get
        Set(ByVal Value As String)
            If uuid Is Nothing Then Return
            If Bad(uuid) Then Return
            RegionList(uuid)._Birds = Value
        End Set
    End Property

    Public Property DisableGloebits(uuid As String) As String
        Get
            If uuid Is Nothing Then Return ""
            If Bad(uuid) Then Return ""
            Return RegionList(uuid)._DisableGloebits
        End Get
        Set(ByVal Value As String)
            If uuid Is Nothing Then Return
            If Bad(uuid) Then Return
            RegionList(uuid)._DisableGloebits = Value
        End Set
    End Property

    Public Property DisallowForeigners(uuid As String) As String
        Get
            If uuid Is Nothing Then Return ""
            If Bad(uuid) Then Return ""
            Return RegionList(uuid)._DisallowForeigners
        End Get
        Set(ByVal Value As String)
            If uuid Is Nothing Then Return
            If Bad(uuid) Then Return
            RegionList(uuid)._DisallowForeigners = Value
        End Set
    End Property

    Public Property DisallowResidents(uuid As String) As String
        Get
            If uuid Is Nothing Then Return ""
            If Bad(uuid) Then Return ""
            Return RegionList(uuid)._DisallowResidents
        End Get
        Set(ByVal Value As String)
            If uuid Is Nothing Then Return
            If Bad(uuid) Then Return
            RegionList(uuid)._DisallowResidents = Value
        End Set
    End Property

    Public Property FrameTime(uuid As String) As String
        Get
            If uuid Is Nothing Then Return ""
            If Bad(uuid) Then Return ""
            Return RegionList(uuid)._FrameTime
        End Get
        Set(ByVal Value As String)
            If uuid Is Nothing Then Return
            If Bad(uuid) Then Return
            If Value Is Nothing Then Return
            Value = Value.Replace(",", ".")
            RegionList(uuid)._FrameTime = Value
        End Set
    End Property

    Public Property GDPR(uuid As String) As String
        Get
            If uuid Is Nothing Then Return ""
            If Bad(uuid) Then Return ""
            Return RegionList(uuid)._GDPR
        End Get
        Set(ByVal Value As String)
            If uuid Is Nothing Then Return
            If Bad(uuid) Then Return
            RegionList(uuid)._GDPR = Value
        End Set
    End Property

    Public Property GodDefault(uuid As String) As String
        Get
            If uuid Is Nothing Then Return "True"
            If Bad(uuid) Then Return "True"
            Return RegionList(uuid)._GodDefault
        End Get
        Set(ByVal Value As String)
            If uuid Is Nothing Then Return
            If Bad(uuid) Then Return
            RegionList(uuid)._GodDefault = Value
        End Set
    End Property

    Public Property ManagerGod(uuid As String) As String
        Get
            If uuid Is Nothing Then Return ""
            If Bad(uuid) Then Return ""
            Return RegionList(uuid)._ManagerGod
        End Get
        Set(ByVal Value As String)
            If uuid Is Nothing Then Return
            If Bad(uuid) Then Return
            RegionList(uuid)._ManagerGod = Value
        End Set
    End Property

    Public Property MapType(uuid As String) As String
        Get
            If uuid Is Nothing Then Return ""
            If Bad(uuid) Then Return ""
            Return RegionList(uuid)._MapType
        End Get
        Set(ByVal Value As String)
            If uuid Is Nothing Then Return
            If Bad(uuid) Then Return
            RegionList(uuid)._MapType = Value
        End Set
    End Property

    Public Property MinTimerInterval(uuid As String) As String
        Get
            If uuid Is Nothing Then Return ""
            If Bad(uuid) Then Return ""
            Return RegionList(uuid)._MinTimerInterval
        End Get
        Set(ByVal Value As String)
            If uuid Is Nothing Then Return
            If Bad(uuid) Then Return
            If Value Is Nothing Then Return
            Value = Value.Replace(",", ".")
            RegionList(uuid)._MinTimerInterval = Value
        End Set
    End Property

    Public Property Physics(uuid As String) As String
        Get
            If uuid Is Nothing Then Return ""
            If Bad(uuid) Then Return ""
            Return RegionList(uuid)._Physics
        End Get
        Set(ByVal Value As String)
            If uuid Is Nothing Then Return
            If Bad(uuid) Then Return
            RegionList(uuid)._Physics = Value
        End Set
    End Property

    Public Property RegionEnabled(uuid As String) As Boolean
        Get
            If uuid Is Nothing Then Return False
            If Bad(uuid) Then Return False
            Return RegionList(uuid)._RegionEnabled
        End Get
        Set(ByVal Value As Boolean)
            If uuid Is Nothing Then Return
            If Bad(uuid) Then Return
            RegionList(uuid)._RegionEnabled = Value
        End Set
    End Property

    Public Property RegionGod(uuid As String) As String
        Get
            If uuid Is Nothing Then Return ""
            If Bad(uuid) Then Return ""
            Return RegionList(uuid)._RegionGod
        End Get
        Set(ByVal Value As String)
            If uuid Is Nothing Then Return
            If Bad(uuid) Then Return
            RegionList(uuid)._RegionGod = Value
        End Set
    End Property

    Public Property RegionSnapShot(uuid As String) As String
        Get
            If uuid Is Nothing Then Return ""
            If Bad(uuid) Then Return ""
            Return RegionList(uuid)._RegionSnapShot
        End Get
        Set(ByVal Value As String)
            If uuid Is Nothing Then Return
            If Bad(uuid) Then Return
            RegionList(uuid)._RegionSnapShot = Value
        End Set
    End Property

    Public Property ScriptEngine(uuid As String) As String
        Get
            If uuid Is Nothing Then Return ""
            If Bad(uuid) Then Return ""
            Return RegionList(uuid)._ScriptEngine
        End Get
        Set(ByVal Value As String)
            If uuid Is Nothing Then Return
            If Bad(uuid) Then Return
            RegionList(uuid)._ScriptEngine = Value
        End Set
    End Property

    Public Property SkipAutobackup(uuid As String) As String
        Get
            If uuid Is Nothing Then Return ""
            If Bad(uuid) Then Return ""
            Return RegionList(uuid)._SkipAutobackup
        End Get
        Set(ByVal Value As String)
            If uuid Is Nothing Then Return
            If Bad(uuid) Then Return
            RegionList(uuid)._SkipAutobackup = Value
        End Set
    End Property

    Public Property SmartStart(uuid As String) As String

        Get
            If uuid Is Nothing Then Return ""
            If Bad(uuid) Then Return ""
            Return RegionList(uuid)._RegionSmartStart
        End Get
        Set(ByVal Value As String)
            If uuid Is Nothing Then Return
            If Bad(uuid) Then Return
            RegionList(uuid)._RegionSmartStart = Value
        End Set

    End Property

    Public Property Snapshot(uuid As String) As String
        Get
            If uuid Is Nothing Then Return ""
            If Bad(uuid) Then Return ""
            Return RegionList(uuid)._Snapshot
        End Get
        Set(ByVal Value As String)
            If uuid Is Nothing Then Return
            If Bad(uuid) Then Return
            RegionList(uuid)._Snapshot = Value
        End Set
    End Property

    Public Property Teleport(uuid As String) As String
        Get
            If uuid Is Nothing Then Return ""
            If Bad(uuid) Then Return ""
            Return RegionList(uuid)._Teleport
        End Get
        Set(ByVal Value As String)
            If uuid Is Nothing Then Return
            If Bad(uuid) Then Return
            RegionList(uuid)._Teleport = Value
        End Set
    End Property

    Public Property Tides(uuid As String) As String
        Get
            If uuid Is Nothing Then Return ""
            If Bad(uuid) Then Return ""
            Return RegionList(uuid)._Tides
        End Get
        Set(ByVal Value As String)
            If uuid Is Nothing Then Return
            If Bad(uuid) Then Return
            RegionList(uuid)._Tides = Value
        End Set
    End Property

#End Region

#Region "Functions"

    Public Sub DebugGroup()

        For Each pair In _Grouplist
            Debug.Print("Group name: {0}, http port: {1}", pair.Key, pair.Value)
        Next

    End Sub

    Public Sub DebugRegions(region As String)

        Log("uuid", CStr(region) & vbCrLf &
            " PID:" & RegionList(region)._ProcessID & vbCrLf &
            " Group:" & RegionList(region)._Group & vbCrLf &
            " Region:" & RegionList(region)._RegionName & vbCrLf &
            " Status=" & CStr(RegionList(region)._Status) & vbCrLf &
            " Crashes=" & CStr(RegionList(region)._CrashCounter) & vbCrLf &
           " RegionEnabled=" & RegionList(region)._RegionEnabled & vbCrLf &
           " Timer=" & CStr(RegionList(region)._Timer))

    End Sub

    Public Function FindRegionByName(name As String) As String

        Dim pair As KeyValuePair(Of String, Region_data)
        For Each pair In RegionList
            If name = pair.Value._RegionName Then               '
                Return pair.Value._UUID
            End If
        Next
        'RegionDump()
        Return ""

    End Function

    Public Function IsBooted(uuid As String) As Boolean
        If uuid Is Nothing Then Return False
        If Bad(uuid) Then Return False
        If Status(uuid) = SIMSTATUSENUM.Booted Then
            Return True
        End If
        Return False

    End Function

    Public Sub RegionDump()

        If Not Debugger.IsAttached Then Return
        Dim pair As KeyValuePair(Of String, Region_data)
        For Each pair In RegionList
            DebugRegions(pair.Value._UUID)
        Next

    End Sub

    Public Function RegionUuidListByName(Gname As String) As List(Of String)

        Dim L As New List(Of String)

        Dim pair As KeyValuePair(Of String, Region_data)
        For Each pair In RegionList
            If pair.Value._Group = Gname Or Gname = "*" Then
                L.Add(pair.Value._UUID)
            End If
        Next

        Return L

    End Function

    Public Function RegionUuids() As List(Of String)

        Dim L As New List(Of String)
        Dim pair As KeyValuePair(Of String, Region_data)

        For Each pair In RegionList
            L.Add(pair.Value._UUID)
        Next

        Return L

    End Function

    Private Function FindRegionUUIDByName(name As String) As String

        Dim pair As KeyValuePair(Of String, Region_data)

        For Each pair In RegionList
            If name = pair.Value._UUID Then Return pair.Value._UUID
        Next

        Return ""

    End Function

#End Region

#Region "POST"

    Shared Function CheckPassword(post As String, machine As String) As Boolean

        If machine Is Nothing Then Return False

        ' Returns true is password is blank or matching
        Dim pattern1 As Regex = New Regex("PW=(.*?)&")
        Dim match1 As Match = pattern1.Match(post)
        If match1.Success Then
            Dim p1 As String = match1.Groups(1).Value
            If p1.Length = 0 Then Return True
            If machine.ToUpper(Globalization.CultureInfo.InvariantCulture) = p1.ToUpper(Globalization.CultureInfo.InvariantCulture) Then Return True
        End If
        Return False

    End Function

    'TODO: Move to Mysql
    Shared Function GetAgentNameByUUID(uuid As String) As String

        If Settings.ServerType <> RobustServer Then Return ""
        Dim name As String = ""
        Using myConnection As MySqlConnection = New MySqlConnection(Settings.RobustMysqlConnection)
            Dim Query1 = "Select userid from robust.griduser where userid like @p1;"

            Using myCommand1 As MySqlCommand = New MySqlCommand(Query1) With {
                .Connection = myConnection
            }
                myConnection.Open()
                myCommand1.Prepare()
                myCommand1.Parameters.AddWithValue("p1", uuid & "%")
                name = CStr(myCommand1.ExecuteScalar())
            End Using

        End Using

        Debug.Print("User=" + uuid + ", name=" + name)
        Dim pattern As Regex = New Regex(".*?;.*?;(.*)")
        Dim match As Match = pattern.Match(name)
        If match.Success Then
            name = match.Groups(1).Value
            Debug.Print("User=" + uuid + ", name=" + name)
            Return name
        End If

        Return ""
    End Function

    'TODO: Move to Mysql
    Shared Function GetPartner(p1 As String, mysetting As MySettings) As String

        If mysetting Is Nothing Then
            Return ""
        End If

        Dim answer As String = ""
        Using myConnection As MySqlConnection = New MySqlConnection(mysetting.RobustMysqlConnection)
            Dim Query1 = "Select profilepartner from robust.userprofile where userUUID=@p1;"
            Using myCommand1 As MySqlCommand = New MySqlCommand(Query1) With {
                .Connection = myConnection
            }
                myConnection.Open()
                myCommand1.Prepare()
                myCommand1.Parameters.AddWithValue("p1", p1)
                answer = CStr(myCommand1.ExecuteScalar())
                Debug.Print("User=" + p1 + ", Partner=" + answer)
            End Using
        End Using

        Return answer

    End Function

    Public Function ParsePost(post As String, settings As MySettings) As String

        If settings Is Nothing Then Return "<html><head></head><body>Error</html>"
        If post Is Nothing Then Return "<html><head></head><body>Error</html>"
        ' set Region.Booted to true if the POST from the region indicates it is online requires a section in Opensim.ini where [RegionReady] has this:

        '[RegionReady]

        '; Enable this module to get notified once all items And scripts in the region have been completely loaded And compiled
        'Enabled = True

        '; Channel on which to signal region readiness through a message
        '; formatted as follows: "{server_startup|oar_file_load},{0|1},n,[oar error]"
        '; - the first field indicating whether this Is an initial server startup
        '; - the second field Is a number indicating whether the OAR file loaded ok (1 == ok, 0 == error)
        '; - the third field Is a number indicating how many scripts failed to compile
        '; - "oar error" if supplied, provides the error message from the OAR load
        'channel_notify = -800

        '; - disallow logins while scripts are loading
        '; Instability can occur on regions with 100+ scripts if users enter before they have finished loading
        'login_disable = True

        '; - send an alert as json to a service
        'alert_uri = ${Const|BaseURL}:${Const|DiagnosticsPort}/${Const|RegionFolderName}

        ' POST = "GET Region name HTTP...{server_startup|oar_file_load},{0|1},n,[oar error]"
        '{"alert":"region_ready","login":"enabled","region_name":"Region 2","RegionId":"19f6adf0-5f35-4106-bcb8-dc3f2e846b89"}}
        'POST / Region%202 HTTP/1.1
        'Content-Type: Application/ json
        'Host:   tea.outworldz.net : 8001
        'Content-Length:  118
        'Connection: Keep-Alive
        '
        '"{""alert"":""region_ready"",""login"":""disabled"",""region_name"":""8021"",""region_id"":""c46ee5e5-5bb8-4cb5-8efd-eff44a0c7160""}"
        '"{"alert":"region_ready","login":"enabled","region_name":"Region 2","region_id":"19f6adf0-5f35-4106-bcb8-dc3f2e846b89"}
        '"{""alert"":""region_ready"",""login"":""shutdown"",""region_name"":""8021"",""region_id"":""c46ee5e5-5bb8-4cb5-8efd-eff44a0c7160""}"
        ' we want region name, UUID and server_startup could also be a probe from the outworldz to check if ports are open.

        ' WarmingUp(0) = True ShuttingDown(1) = True

        ' alerts need to be fast so we stash them on a list and process them on a 10 second timer.

        If (post.Contains("""alert"":""region_ready""")) Then

            WebserverList.Add(post)

        ElseIf post.Contains("ALT=") Then
            ' Smart Start AutoStart Region mode
            Debug.Print("Smart Start:" + post)

            ' Smart Start
            Dim uuid As String = ""
            Dim pattern As Regex = New Regex("ALT=(.*?)&AGENT=(.*)")
            Dim match As Match = pattern.Match(post)

            If match.Success Then
                uuid = match.Groups(1).Value
                Dim AgentName = match.Groups(2).Value
                If uuid.Length > 0 And RegionEnabled(uuid) And SmartStart(uuid) = "True" Then
                    If Status(uuid) = SIMSTATUSENUM.Booted Then
                        TextPrint(My.Resources.Someone_is_in_word & " " & RegionName(uuid))
                        Return uuid
                    Else
                        TextPrint(My.Resources.Smart_Start_word & " " & RegionName(uuid))
                        Status(uuid) = SIMSTATUSENUM.Resume

                        TeleportAvatarDict.Add(AgentName, uuid)

                        ' redirect to welcome
                        Dim wname = settings.WelcomeRegion
                        Dim WelcomeRegionUUID As String = FindRegionByName(wname)
                        Return WelcomeRegionUUID
                    End If
                    'other states we can ignore as eventually it will be Stopped or Running
                End If
            End If

            ' HG Sim, perhaps,. it is not found, not enabled, not Smart Start,let it work normally
            Return uuid

        ElseIf post.Contains("TOS") Then
            ' currently unused as is only in stand alones
            Debug.Print("UUID:" + post)
            '"POST /TOS HTTP/1.1" & vbCrLf & "Host: mach.outworldz.net:9201" & vbCrLf & "Connection: keep-alive" & vbCrLf & "Content-Length: 102" & vbCrLf & "Cache-Control: max-age=0" & vbCrLf & "Upgrade-Insecure-Requests: 1" & vbCrLf & "User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.99 Safari/537.36" & vbCrLf & "Origin: http://mach.outworldz.net:9201" & vbCrLf & "Content-Type: application/x-www-form-urlencoded" & vbCrLf & "DNT: 1" & vbCrLf & "Accept: text/html,application/xhtml+xml,application/xml;q=0.0909,image/webp,image/apng,*/*;q=0.8" & vbCrLf & "Referer: http://mach.outworldz.net:9200/wifi/termsofservice.html?uid=acb8fd92-c725-423f-b750-5fd971d73182&sid=40c5b80a-5377-4b97-820c-a0952782a701" & vbCrLf & "Accept-Encoding: gzip, deflate" & vbCrLf & "Accept-Language: en-US,en;q=0.0909" & vbCrLf & vbCrLf &
            '"action-accept=Accept&uid=acb8fd92-c725-423f-b750-5fd971d73182&sid=40c5b80a-5377-4b97-820c-a0952782a701"

            Return "<html><head></head><body>Error</html>"

            Dim uid As Guid
            Dim sid As Guid

            Try
                post = post.Replace("{ENTER}", "")
                post = post.Replace("\r", "")

                Dim pattern As Regex = New Regex("uid=([0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12})")
                Dim match As Match = pattern.Match(post)
                If match.Success Then
                    uid = Guid.Parse(match.Groups(1).Value)
                End If

                Dim pattern2 As Regex = New Regex("sid=([0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12})")
                Dim match2 As Match = pattern2.Match(post)
                If match2.Success Then
                    sid = Guid.Parse(match2.Groups(1).Value)
                End If

                If match.Success And match2.Success Then

                    ' Only works in Standalone, anyway. Not implemented at all in Grid mode - the Diva DLL Diva is stubbed off.
                    Dim result As Integer = 1

                    Dim myConnection As MySqlConnection = New MySqlConnection(settings.RobustMysqlConnection)

                    Dim Query1 = "update opensim.griduser set TOS = 1 where UserID = @p1; "
                    Dim myCommand1 As MySqlCommand = New MySqlCommand(Query1) With {
                        .Connection = myConnection
                    }
                    myConnection.Open()
                    myCommand1.Prepare()
                    myCommand1.Parameters.AddWithValue("p1", uid.ToString())
                    myCommand1.ExecuteScalar()
                    myConnection.Close()
                    myConnection.Dispose()
                    Return "<html><head></head><body>Welcome! You can close this window.</html>"
                Else
                    Return "<html><head></head><body>Test Passed</html>"
                End If
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
                Return "<html><head></head><body>Error</html>"
            End Try

        ElseIf post.ToUpperInvariant.Contains("GET_PARTNER") Then
            Debug.Print("get Partner")
            Dim PWok As Boolean = CheckPassword(post, settings.MachineID())
            If Not PWok Then Return ""

            Dim pattern1 As Regex = New Regex("User=(.*)", RegexOptions.IgnoreCase)
            Dim match1 As Match = pattern1.Match(post)
            Dim p1 As String
            If match1.Success Then
                p1 = match1.Groups(1).Value
                Dim s = GetPartner(p1, settings)
                Debug.Print(s)
                Return s
            Else
                Debug.Print("No partner")
                Return "00000000-0000-0000-0000-000000000000"
            End If

            ' Partner prim
        ElseIf post.ToUpperInvariant.Contains("SET_PARTNER") Then
            Debug.Print("set Partner")
            Dim PWok As Boolean = CheckPassword(post, CStr(settings.MachineID()))
            If Not PWok Then Return ""

            Dim pattern1 As Regex = New Regex("User=(.*?)&", RegexOptions.IgnoreCase)
            Dim match1 As Match = pattern1.Match(post)
            If match1.Success Then

                Dim p2 As String = ""
                Dim p1 = match1.Groups(1).Value
                Dim pattern2 As Regex = New Regex("Partner=(.*)", RegexOptions.IgnoreCase)
                Dim match2 As Match = pattern2.Match(post)
                If match2.Success Then
                    p2 = match2.Groups(1).Value
                End If
                Dim result As New Guid
                If Guid.TryParse(p1, result) And Guid.TryParse(p1, result) Then
                    Dim Partner = GetPartner(p1, settings)
                    Debug.Print("Partner=" + p2)

                    Try
                        Using myConnection As MySqlConnection = New MySqlConnection(settings.RobustMysqlConnection)
                            Dim Query1 = "update robust.userprofile set profilepartner=@p2 where userUUID = @p1; "
                            Using myCommand1 As MySqlCommand = New MySqlCommand(Query1) With {
                                    .Connection = myConnection
                                }
                                myConnection.Open()
                                myCommand1.Prepare()

                                myCommand1.Parameters.AddWithValue("p1", p1)
                                myCommand1.Parameters.AddWithValue("p2", p2)

                                myCommand1.ExecuteScalar()

                            End Using
                        End Using
                    Catch ex As Exception
                        BreakPoint.Show(ex.Message)
                        Debug.Print(ex.Message)
                    End Try

                    Debug.Print(Partner)
                    Return Partner

                End If
            End If
            Debug.Print("NULL response")
            Return ""
        Else
            Return "Test Completed"
        End If

        Return ""

    End Function

#End Region

#Region "Opensim.ini writers"

    Public Function CopyOpensimProto(uuid As String) As Boolean

        '============== Opensim.ini =====================
        Dim pathname = IniPath(uuid)

        Dim Name = RegionName(uuid)
        Dim Group = GroupName(uuid)

        ' copy the prototype to the regions Opensim.ini

        CopyFileFast(GetOpensimProto(), IO.Path.Combine(pathname, "Opensim.ini"))

        ' Load Opensim.ini in Region Folder specific to this region
        Dim INI = Settings.LoadIni(IO.Path.Combine(pathname, "Opensim.ini"), ";")

        Settings.SetIni("RemoteAdmin", "port", CStr(GroupPort(uuid)))
        Settings.SetIni("RemoteAdmin", "access_password", Settings.MachineID)

        Settings.SetIni("Const", "PrivatePort", CStr(Settings.PrivatePort)) '8003
        Settings.SetIni("Const", "RegionFolderName", GroupName(uuid))
        Settings.SetIni("Const", "BaseHostname", Settings.BaseHostName)
        Settings.SetIni("Const", "PublicPort", CStr(Settings.HttpPort)) ' 8002
        Settings.SetIni("Const", "PrivURL", "http://" & CStr(Settings.LANIP())) ' local IP
        Settings.SetIni("Const", "http_listener_port", CStr(GroupPort(uuid))) ' varies with region

        Select Case Settings.ServerType
            Case RobustServer
                SetupOpensimSearchINI()
                Settings.SetIni("Const", "PrivURL", "http://" & Settings.LANIP())
                Settings.SetIni("Const", "GridName", Settings.SimName)
                SetupOpensimIM()
            Case RegionServer
                SetupOpensimSearchINI()
                SetupOpensimIM()
            Case OsgridServer
            Case MetroServer
        End Select

        If Settings.CMS = JOpensim And Settings.ServerType = RobustServer Then
            Settings.SetIni("UserProfiles", "ProfileServiceURL", "")
            Settings.SetIni("Groups", "Module", "GroupsModule")
            Settings.SetIni("Groups", "ServicesConnectorModule", """" & "XmlRpcGroupsServicesConnector" & """")
            Settings.SetIni("Groups", "MessagingModule", "GroupsMessagingModule")
            Settings.SetIni("Groups", "GroupsServerURI", "http://" & Settings.PublicIP & ":" & Settings.ApachePort & "/jOpensim/index.php?option=com_opensim&view=interface")
        End If

        Settings.SetIni("Const", "ApachePort", CStr(Settings.ApachePort))

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
        If Settings.DeleteScriptsOnStartupLevel <> PropSimVersion Then

            WipeScripts()
            Settings.DeleteScriptsOnStartupLevel() = PropSimVersion ' we have scripts cleared to proper Opensim Version

            Settings.SetIni("XEngine", "DeleteScriptsOnStartup", "True")
        Else
            Settings.SetIni("XEngine", "DeleteScriptsOnStartup", "False")
        End If

        If Settings.LSLHTTP Then
            ' do nothing - let them edit it
        Else
            Settings.SetIni("Network", "OutboundDisallowForUserScriptsExcept", Settings.LANIP() & ":" & Settings.DiagnosticPort)
        End If

        Settings.SetIni("PrimLimitsModule", "EnforcePrimLimits", CStr(Settings.Primlimits))

        If Settings.Primlimits Then
            Settings.SetIni("Permissions", "permissionmodules", "DefaultPermissionsModule, PrimLimitsModule")
        Else
            Settings.SetIni("Permissions", "permissionmodules", "DefaultPermissionsModule")
        End If

        If Settings.GloebitsEnable Then
            Settings.SetIni("Startup", "economymodule", "Gloebit")
            Settings.SetIni("Economy", "CurrencyURL", "")
        ElseIf Settings.CMS = JOpensim Then
            Settings.SetIni("Startup", "economymodule", "jOpenSimMoneyModule")
            Settings.SetIni("Economy", "CurrencyURL", "${Const|BaseURL}:${Const|ApachePort}/jOpensim/index.php?option=com_opensim&view=interface")
        Else
            Settings.SetIni("Startup", "economymodule", "BetaGridLikeMoneyModule")
            Settings.SetIni("Economy", "CurrencyURL", "")
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
            Settings.SetIni("Cloud", "density", CStr(Settings.Density))
        Else
            Settings.SetIni("Cloud", "enabled", "False")
        End If

        ' Physics choices for meshmerizer, where ODE requires a special one ZeroMesher meshing = Meshmerizer meshing = ubODEMeshmerizer 0 = none 1 = OpenDynamicsEngine 2 = BulletSim 3 = BulletSim with
        ' threads 4 = ubODE

        Select Case Settings.Physics
            Case 0
                Settings.SetIni("Startup", "meshing", "ZeroMesher")
                Settings.SetIni("Startup", "physics", "basicphysics")
                Settings.SetIni("Startup", "UseSeparatePhysicsThread", "False")
                Settings.SetIni("ODEPhysicsSettings", "use_NINJA_physics_joints", "False")
            Case 1
                Settings.SetIni("Startup", "meshing", "Meshmerizer")
                Settings.SetIni("Startup", "physics", "OpenDynamicsEngine")
                Settings.SetIni("Startup", "UseSeparatePhysicsThread", "False")
                Settings.SetIni("ODEPhysicsSettings", "use_NINJA_physics_joints", CStr(Settings.NinjaRagdoll))
            Case 2
                Settings.SetIni("Startup", "meshing", "Meshmerizer")
                Settings.SetIni("Startup", "physics", "BulletSim")
                Settings.SetIni("Startup", "UseSeparatePhysicsThread", "False")
                Settings.SetIni("ODEPhysicsSettings", "use_NINJA_physics_joints", "False")
            Case 3
                Settings.SetIni("Startup", "meshing", "Meshmerizer")
                Settings.SetIni("Startup", "physics", "BulletSim")
                Settings.SetIni("Startup", "UseSeparatePhysicsThread", "True")
                Settings.SetIni("ODEPhysicsSettings", "use_NINJA_physics_joints", "False")
            Case 4
                Settings.SetIni("Startup", "meshing", "ubODEMeshmerizer")
                Settings.SetIni("Startup", "physics", "ubODE")
                Settings.SetIni("Startup", "UseSeparatePhysicsThread", "False")
                Settings.SetIni("ODEPhysicsSettings", "use_NINJA_physics_joints", CStr(Settings.NinjaRagdoll))
            Case 5
                Settings.SetIni("Startup", "meshing", "Meshmerizer")
                Settings.SetIni("Startup", "physics", "ubODE")
                Settings.SetIni("Startup", "UseSeparatePhysicsThread", "False")
                Settings.SetIni("ODEPhysicsSettings", "use_NINJA_physics_joints", CStr(Settings.NinjaRagdoll))
            Case Else
                Settings.SetIni("Startup", "meshing", "Meshmerizer")
                Settings.SetIni("Startup", "physics", "BulletSim")
                Settings.SetIni("Startup", "UseSeparatePhysicsThread", "True")
                Settings.SetIni("ODEPhysicsSettings", "use_NINJA_physics_joints", "False")
        End Select

        Settings.SetIni("Map", "RenderMaxHeight", CStr(Settings.RenderMaxHeight))
        Settings.SetIni("Map", "RenderMinHeight", CStr(Settings.RenderMinHeight))

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

        ' set new Min Timer Interval for how fast a script can go. Can be set in region files as a float, or nothing
        Dim Xtime As Double = 1 / 11   '1/11 of a second is as fast as she can go
        If MinTimerInterval(uuid).Length > 0 Then
            If Not Double.TryParse(MinTimerInterval(uuid), Xtime) Then
                Xtime = 1.0 / 11.0
            End If
        End If
        Settings.SetIni("XEngine", "MinTimerInterval", CStr(Xtime))
        Settings.SetIni("YEngine", "MinTimerInterval", CStr(Xtime))

        ' Gloebit
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

        If Settings.ServerType = RobustServer Then
            Settings.SetIni("Gloebit", "GLBSpecificConnectionString", Settings.RobustDBConnection)
        Else
            Settings.SetIni("Gloebit", "GLBSpecificConnectionString", Settings.RegionDBConnection)
        End If

        ' Autobackup
        Settings.SetIni("AutoBackupModule", "AutoBackup", "True")

        If Settings.AutoBackup And String.IsNullOrEmpty(SkipAutobackup(uuid)) Then
            Settings.SetIni("AutoBackupModule", "AutoBackup", "True")
        End If

        If Settings.AutoBackup And SkipAutobackup(uuid) = "True" Then
            Settings.SetIni("AutoBackupModule", "AutoBackup", "False")
        End If

        If Not Settings.AutoBackup Then
            Settings.SetIni("AutoBackupModule", "AutoBackup", "False")
        End If

        If Not Settings.BackupOARs Then
            Settings.SetIni("AutoBackupModule", "AutoBackup", "False")
        End If

        Settings.SetIni("AutoBackupModule", "AutoBackupInterval", Settings.AutobackupInterval)
        Settings.SetIni("AutoBackupModule", "AutoBackupKeepFilesForDays", CStr(Settings.KeepForDays))
        Settings.SetIni("AutoBackupModule", "AutoBackupDir", BackupPath())

        Select Case Physics(uuid)
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

        'Gods
        If String.IsNullOrEmpty(GodDefault(uuid)) _
            Or GodDefault(uuid) = "False" Then

            Select Case AllowGods(uuid)
                Case ""
                    Settings.SetIni("Permissions", "allow_grid_gods", CStr(Settings.AllowGridGods))
                Case "False"
                    Settings.SetIni("Permissions", "allow_grid_gods", "False")
                Case "True"
                    Settings.SetIni("Permissions", "allow_grid_gods", "True")
            End Select

            Select Case RegionGod(uuid)
                Case ""
                    Settings.SetIni("Permissions", "region_owner_is_god", CStr(Settings.RegionOwnerIsGod))
                Case "False"
                    Settings.SetIni("Permissions", "region_owner_is_god", "False")
                Case "True"
                    Settings.SetIni("Permissions", "region_owner_is_god", "True")
            End Select

            Select Case ManagerGod(uuid)
                Case ""
                    Settings.SetIni("Permissions", "region_manager_is_god", CStr(Settings.RegionManagerIsGod))
                Case "False"
                    Settings.SetIni("Permissions", "region_manager_is_god", "False")
                Case "True"
                    Settings.SetIni("Permissions", "region_manager_is_god", "True")
            End Select
        Else
            Settings.SetIni("Permissions", "allow_grid_gods", "False")
            Settings.SetIni("Permissions", "region_manager_is_god", "False")
            Settings.SetIni("Permissions", "allow_grid_gods", "True")
        End If

        ' Prims

        If NonPhysicalPrimMax(uuid).Length > 0 Then
            Settings.SetIni("Startup", "NonPhysicalPrimMax", CStr(NonPhysicalPrimMax(uuid)))
        End If

        If PhysicalPrimMax(uuid).Length > 0 Then
            Settings.SetIni("Startup", "PhysicalPrimMax", CStr(PhysicalPrimMax(uuid)))
        End If

        If MinTimerInterval(uuid).Length > 0 Then
            Settings.SetIni("XEngine", "MinTimerInterval", CStr(MinTimerInterval(uuid)))
        End If

        If FrameTime(uuid).Length > 0 Then
            Settings.SetIni("Startup", "FrameTime", CStr(FrameTime(uuid)))
        End If

        If DisallowForeigners(uuid) = "True" Then
            Settings.SetIni("DisallowForeigners", "Enabled", CStr(DisallowForeigners(uuid)))
        End If

        If DisallowResidents(uuid) = "True" Then
            Settings.SetIni("DisallowResidents", "Enabled", CStr(DisallowResidents(uuid)))
        End If

        ' replace with a PHP module
        If DisableGloebits(uuid) = "True" Then
            Settings.SetIni("Startup", "economymodule", "BetaGridLikeMoneyModule")
        End If

        ' Search
        Select Case Snapshot(uuid)
            Case ""
                Settings.SetIni("DataSnapshot", "index_sims", CStr(Settings.SearchEnabled))
            Case "True"
                Settings.SetIni("DataSnapshot", "index_sims", "True")
            Case "False"
                Settings.SetIni("DataSnapshot", "index_sims", "False")
        End Select

        'ScriptEngine Overrides
        If ScriptEngine(uuid) = "XEngine" Then
            Settings.SetIni("Startup", "DefaultScriptEngine", "XEngine")
            Settings.SetIni("XEngine", "Enabled", "True")
            Settings.SetIni("YEngine", "Enabled", "False")
        End If

        If ScriptEngine(uuid) = "YEngine" Then
            Settings.SetIni("Startup", "DefaultScriptEngine", "YEngine")
            Settings.SetIni("XEngine", "Enabled", "False")
            Settings.SetIni("YEngine", "Enabled", "True")
        End If

        Settings.SetIni("SmartStart", "Enabled", SmartStart(uuid))

        Settings.SaveINI(INI, System.Text.Encoding.UTF8)

        '============== Region.ini =====================
        ' Opensim.ini in Region Folder specific to this region
        INI = Settings.LoadIni(RegionPath(uuid), ";")

        Settings.SetIni(Name, "InternalPort", CStr(RegionPort(uuid)))
        Settings.SetIni(Name, "ExternalHostName", Settings.ExternalHostName())

        Settings.SetIni(Name, "ClampPrimSize", CStr(ClampPrimSize(uuid)))

        ' not a standard  only use by the Dreamers
        If RegionEnabled(uuid) Then
            Settings.SetIni(Name, "Enabled", "True")
        Else
            Settings.SetIni(Name, "Enabled", "False")
        End If

        Select Case NonPhysicalPrimMax(uuid)
            Case ""
                Settings.SetIni(Name, "NonPhysicalPrimMax", CStr(1024))
            Case Else
                Settings.SetIni(Name, "NonPhysicalPrimMax", NonPhysicalPrimMax(uuid))
        End Select

        Select Case PhysicalPrimMax(uuid)
            Case ""
                Settings.SetIni(Name, "PhysicalPrimMax", CStr(64))
            Case Else
                Settings.SetIni(Name, "PhysicalPrimMax", PhysicalPrimMax(uuid))
        End Select

        If Settings.Primlimits Then
            Select Case MaxPrims(uuid)
                Case ""
                    Settings.SetIni(Name, "MaxPrims", CStr(45000))
                Case Else
                    Settings.SetIni(Name, "MaxPrims", MaxPrims(uuid))
            End Select
        Else
            Select Case MaxPrims(uuid)
                Case ""
                    Settings.SetIni(Name, "MaxPrims", CStr(45000))
                Case Else
                    Settings.SetIni(Name, "MaxPrims", MaxPrims(uuid))
            End Select
        End If

        Select Case MaxAgents(uuid)
            Case ""
                Settings.SetIni(Name, "MaxAgents", CStr(100))
            Case Else
                Settings.SetIni(Name, "MaxAgents", MaxAgents(uuid))
        End Select

        ' Maps
        If MapType(uuid) = "None" Then
            Settings.SetIni(Name, "GenerateMaptiles", "False")
        ElseIf MapType(uuid) = "Simple" Then
            Settings.SetIni(Name, "GenerateMaptiles", "True")
            Settings.SetIni(Name, "MapImageModule", "MapImageModule")  ' versus Warp3DImageModule
            Settings.SetIni(Name, "TextureOnMapTile", "False")         ' versus True
            Settings.SetIni(Name, "DrawPrimOnMapTile", "False")
            Settings.SetIni(Name, "TexturePrims", "False")
            Settings.SetIni(Name, "RenderMeshes", "False")
        ElseIf MapType(uuid) = "Good" Then
            Settings.SetIni(Name, "GenerateMaptiles", "True")
            Settings.SetIni(Name, "MapImageModule", "Warp3DImageModule")  ' versus MapImageModule
            Settings.SetIni(Name, "TextureOnMapTile", "False")         ' versus True
            Settings.SetIni(Name, "DrawPrimOnMapTile", "False")
            Settings.SetIni(Name, "TexturePrims", "False")
            Settings.SetIni(Name, "RenderMeshes", "False")
        ElseIf MapType(uuid) = "Better" Then
            Settings.SetIni(Name, "GenerateMaptiles", "True")
            Settings.SetIni(Name, "MapImageModule", "Warp3DImageModule")  ' versus MapImageModule
            Settings.SetIni(Name, "TextureOnMapTile", "True")         ' versus True
            Settings.SetIni(Name, "DrawPrimOnMapTile", "True")
            Settings.SetIni(Name, "TexturePrims", "False")
            Settings.SetIni(Name, "RenderMeshes", "False")
        ElseIf MapType(uuid) = "Best" Then
            Settings.SetIni(Name, "GenerateMaptiles", "True")
            Settings.SetIni(Name, "MapImageModule", "Warp3DImageModule")  ' versus MapImageModule
            Settings.SetIni(Name, "TextureOnMapTile", "True")      ' versus True
            Settings.SetIni(Name, "DrawPrimOnMapTile", "True")
            Settings.SetIni(Name, "TexturePrims", "True")
            Settings.SetIni(Name, "RenderMeshes", "True")
        Else
            Settings.SetIni(Name, "GenerateMaptiles", "")
            Settings.SetIni(Name, "MapImageModule", "")  ' versus MapImageModule
            Settings.SetIni(Name, "TextureOnMapTile", "")      ' versus True
            Settings.SetIni(Name, "DrawPrimOnMapTile", "")
            Settings.SetIni(Name, "TexturePrims", "")
            Settings.SetIni(Name, "RenderMeshes", "")
        End If

        'Options and overrides
        Settings.SetIni(Name, "DisableGloebits", DisableGloebits(uuid))
        Settings.SetIni(Name, "RegionSnapShot", RegionSnapShot(uuid))
        Settings.SetIni(Name, "Birds", Birds(uuid))
        Settings.SetIni(Name, "Tides", Tides(uuid))
        Settings.SetIni(Name, "Teleport", Teleport(uuid))
        Settings.SetIni(Name, "DisallowForeigners", DisallowForeigners(uuid))
        Settings.SetIni(Name, "DisallowResidents", DisallowResidents(uuid))
        Settings.SetIni(Name, "SkipAutoBackup", SkipAutobackup(uuid))
        Settings.SetIni(Name, "Physics", Physics(uuid))
        Settings.SetIni(Name, "FrameTime", FrameTime(uuid))

        Settings.SaveINI(INI, System.Text.Encoding.UTF8)

        Settings.SaveSettings()

        Return False

    End Function

    Private Shared Sub SetupOpensimIM()

        Dim URL = "http://" & Settings.PublicIP & ":" & Settings.ApachePort
        If Settings.CMS = JOpensim Then
            Settings.SetIni("Messaging", "OfflineMessageModule", "OfflineMessageModule")
            Settings.SetIni("Messaging", "OfflineMessageURL", URL & "/jOpensim/index.php?option=com_opensim&view=interface&messaging=")
            Settings.SetIni("Messaging", "MuteListURL", URL & "/jOpensim/index.php?option=com_opensim&view=interface&messaging=")
        Else
            Settings.SetIni("Messaging", "OfflineMessageModule", "Offline Message Module V2")
            Settings.SetIni("Messaging", "OfflineMessageURL", "")
            Settings.SetIni("Messaging", "MuteListURL", "http://" & Settings.PublicIP & ":" & Settings.HttpPort)
        End If
    End Sub

    Private Shared Sub SetupOpensimSearchINI()

        ' RegionSnapShot
        Settings.SetIni("DataSnapshot", "index_sims", "True")
        If Settings.CMS = JOpensim And Settings.JOpensimSearch = JOpensim Then
            Settings.SetIni("DataSnapshot", "data_services", "")
        ElseIf Settings.JOpensimSearch = Hyperica Then
            Settings.SetIni("DataSnapshot", "data_services", "http://hyperica.com/Search/register.php")
        Else
            Settings.SetIni("DataSnapshot", "data_services", "")
        End If

        If Settings.CMS = JOpensim And Settings.JOpensimSearch = JOpensim Then
            Dim SearchURL = "http://" & Settings.PublicIP & ":" & Settings.ApachePort & "/jOpensim/index.php?option=com_opensim&view=interface"
            Settings.SetIni("Search", "SearchURL", SearchURL)
            Settings.SetIni("LoginService", "SearchURL", SearchURL)
            CopyFileFast(IO.Path.Combine(Settings.OpensimBinPath, "jOpensimProfile.Modules.dll.bak"), IO.Path.Combine(Settings.OpensimBinPath, "jOpensimProfile.Modules.dll"))
            CopyFileFast(IO.Path.Combine(Settings.OpensimBinPath, "jOpensimSearch.Modulesdll.bak"), IO.Path.Combine(Settings.OpensimBinPath, "jOpensimSearch.Modulesdll"))
        ElseIf Settings.JOpensimSearch = Hyperica Then
            Dim SearchURL = "http://hyperica.com/Search/query.php"
            Settings.SetIni("Search", "SearchURL", SearchURL)
            Settings.SetIni("LoginService", "SearchURL", SearchURL)
            DeleteFile(IO.Path.Combine(Settings.OpensimBinPath, "jOpensimProfile.Modules.dll"))
            DeleteFile(IO.Path.Combine(Settings.OpensimBinPath, "jOpensimSearch.Modules.dll"))
        Else
            Settings.SetIni("Search", "SearchURL", "")
            Settings.SetIni("LoginService", "SearchURL", "")
            DeleteFile(IO.Path.Combine(Settings.OpensimBinPath, "jOpensimProfile.Modules.dll"))
            DeleteFile(IO.Path.Combine(Settings.OpensimBinPath, "jOpensimSearch.Modules.dll"))
        End If

    End Sub

    Private Function Bad(uuid As String) As Boolean

        If RegionList.ContainsKey(uuid) Then
            Return False
        End If

        If String.IsNullOrEmpty(uuid) Then
            ErrorLog("Region UUID Zero".ToString(Globalization.CultureInfo.InvariantCulture))
            Return True
        End If

        ErrorLog("Region UUID does not exist. " & CStr(uuid))
        Return True

    End Function

#End Region

End Class
