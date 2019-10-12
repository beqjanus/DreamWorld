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

Imports System.IO

Imports System.Text.RegularExpressions
Imports MySql.Data.MySqlClient
Imports Newtonsoft.Json

Public Class RegionMaker

#Region "Declarations"

    Public Grouplist As New Dictionary(Of String, Integer)
    Private Shared FInstance As RegionMaker = Nothing
#Disable Warning CA1051 ' Do not declare visible instance fields
#Enable Warning CA1051 ' Do not declare visible instance fields
    Dim Backup As New ArrayList()
    Private initted As Boolean = False
    Dim json As New JSONresult
    Private RegionList As New List(Of Region_data)
    Dim TeleportAvatarDict As New Dictionary(Of String, String)
    Dim WebserverList As New List(Of String)

    Public Enum REGIONTIMER As Integer
        Stopped = -1
        StartCounting = 0
    End Enum

    Public Enum SIMSTATUSENUM As Integer
        Stopped = 0
        Booting = 1
        Booted = 2
        RecyclingUp = 3
        RecyclingDown = 4
        ShuttingDown = 5
        RestartPending = 6
        RetartingNow = 7
        Autostart = 8

    End Enum

#End Region

#Region "Property"

    Public Shared ReadOnly Property Instance() As RegionMaker
        Get
            If (FInstance Is Nothing) Then
                FInstance = New RegionMaker()
            End If
            Return FInstance
        End Get
    End Property

    Public Property GroupPort(index As Integer) As Integer
        Get
            Dim RegionName = GroupName(index)
            If Grouplist.ContainsKey(RegionName) Then
                Return Grouplist.Item(RegionName)
            End If
            Return 0
        End Get
        Set(ByVal Value As Integer)
            Dim RegionName = GroupName(index)
            If Grouplist.ContainsKey(RegionName) Then
                Grouplist.Remove(RegionName)
                Grouplist.Add(RegionName, Value)
            Else
                Grouplist.Add(RegionName, Value)
            End If

            'DebugGroup
        End Set
    End Property

#End Region

#Region "Start/Stop"

    Private Sub New()

        GetAllRegions()
        If RegionCount() = 0 Then
            CreateRegion("Welcome")
            Form1.Settings.WelcomeRegion = "Welcome"
            WriteRegionObject("Welcome")
            GetAllRegions()
            Form1.Settings.WelcomeRegion = "Welcome"
            Form1.Settings.SaveSettings()
        End If
        ' RegionDump()

        Debug.Print("Loaded " + CStr(RegionCount) + " Regions")

    End Sub

#End Region

#Region "Classes"

#Disable Warning CA1051 ' Do not declare visible instance fields

    Private Class JSONresult
        Public alert As String
        Public login As String
        Public region_id As String
        Public region_name As String
    End Class

    ''' <summary>
    ''' Self setting Region Ports Iterate over all regions and set the ports from the starting value
    ''' </summary>
    Public Shared Sub UpdateAllRegionPorts()

        If Form1.PropOpensimIsRunning Then
            Return
        End If

        Dim Portnumber As Integer = CInt(Form1.Settings.FirstRegionPort())
        For Each RegionNum As Integer In Form1.PropRegionClass.RegionNumbers
            Dim simName = Form1.PropRegionClass.RegionName(RegionNum)
            Form1.Settings.LoadIni(Form1.PropRegionClass.RegionPath(RegionNum), ";")

            Form1.Settings.SetIni(simName, "InternalPort", CStr(Portnumber))
            Form1.PropRegionClass.RegionPort(RegionNum) = Portnumber
            ' Self setting Region Ports
            Form1.PropMaxPortUsed = Portnumber
            Form1.Settings.SaveINI()
            Portnumber += 1
        Next

    End Sub

    Public Sub CheckPost()

        ' Delete off end of list so we don't skip over one
        If WebserverList.Count = 0 Then Return

        WebserverList.Reverse()

        For LOOPVAR = WebserverList.Count - 1 To 0 Step -1
            If WebserverList.Count = 0 Then Return
            Try
                Dim ProcessString As String = WebserverList(LOOPVAR) ' recover the PID as string

                ' This search returns the substring between two strings, so the first index Is moved
                ' to the character just after the first string.
                Dim POST As String = Uri.UnescapeDataString(ProcessString)
                Dim first As Integer = POST.IndexOf("{", StringComparison.InvariantCulture)
                Dim last As Integer = POST.LastIndexOf("}", StringComparison.InvariantCulture)
                Dim rawJSON = POST.Substring(first, last - first + 1)
                WebserverList.RemoveAt(LOOPVAR)

                Try
                    json = JsonConvert.DeserializeObject(Of JSONresult)(rawJSON)
                Catch ex As Exception
                    Debug.Print(ex.Message)
                    Continue For
                    Return
                End Try

                ' rawJSON
                ' "{""alert"":""region_ready"",""login"":""disabled"",""region_name"":""Welcome"",""region_id"":""365d804a-0df1-46cf-8acf-4320a3df3fca""}"
                ' String rawJSON
                ' "{""alert"":""region_ready"",""login"":""enabled"",""region_name"":""Welcome"",""region_id"":""365d804a-0df1-46cf-8acf-4320a3df3fca""}"
                ' String rawJSON
                ' "{""alert"":""region_ready"",""login"":""shutdown"",""region_name"":""Welcome"",""region_id"":""365d804a-0df1-46cf-8acf-4320a3df3fca""}" String

                If json.login = "enabled" Then
                    Form1.Print("Region " & json.region_name & " is ready")

                    Dim n = FindRegionByName(json.region_name)
                    If n < 0 Then
                        Return
                    End If

                    RegionEnabled(n) = True
                    Status(n) = SIMSTATUSENUM.Booted
                    Form1.PropUpdateView() = True

                    If Debugger.IsAttached = True Then
                        Try
                            'TeleportAvatarDict.Add("Test", "Test User")
                        Catch ex As Exception
                            Debug.Print("Already In list to add")
                        End Try
                    End If

                    Dim Removelist As New List(Of String)
                    If Form1.Settings.SmartStart Then
                        For Each Keypair In TeleportAvatarDict
                            Application.DoEvents()
                            If Keypair.Value = json.region_name Then
                                Dim AgentName As String = GetAgentNameByUUID(Keypair.Key)
                                If AgentName.Length > 0 Then
                                    Form1.Print("Teleporting " & AgentName & " to " & Keypair.Value)
                                    Form1.ConsoleCommand(Form1.Settings.WelcomeRegion, "change region " & json.region_name & "{ENTER}")
                                    Form1.ConsoleCommand(Form1.Settings.WelcomeRegion, "teleport user " & AgentName & " " & json.region_name & "{ENTER}")
                                    Try
                                        Removelist.Add(Keypair.Key)
                                    Catch ex As Exception
                                        Debug.Print("Already In list to remove")
                                    End Try
                                End If
                            End If
                        Next
                    End If

                    ' now delete the avatars we just teleported
                    For Each Name In Removelist
                        Try
                            TeleportAvatarDict.Remove(Name)
                        Catch ex As Exception
                        End Try
                    Next

                    If Form1.Settings.ConsoleShow = False Then
                        Dim hwnd = Form1.GetHwnd(GroupName(n))
                        Form1.ShowDOSWindow(hwnd, Form1.SHOWWINDOWENUM.SWMINIMIZE)
                    End If

                ElseIf json.login = "shutdown" Then

                    Return ' does not work as expected

                    Form1.Print("Region " & json.region_name & " shutdown")

                    Dim n = FindRegionByName(json.region_name)
                    If n < 0 Then
                        Return
                    End If
                    Timer(n) = REGIONTIMER.Stopped
                    If Status(n) = SIMSTATUSENUM.RecyclingDown Then
                        Status(n) = SIMSTATUSENUM.RestartPending
                        Form1.PropUpdateView = True ' make form refresh
                    Else
                        Status(n) = SIMSTATUSENUM.Stopped
                    End If

                    Form1.PropUpdateView() = True
                    Form1.PropExitList.Add(json.region_name)

                End If
            Catch ex As Exception
                Debug.Print(ex.Message)
            End Try
        Next

    End Sub

    Public Function CreateRegion(name As String) As Integer

        'If RegionList.Contains(name) Then
        ' Return RegionList.Count - 1
        'End If
        ' Debug.Print("Create Region " + name)
        Dim r As New Region_data With {
            ._RegionName = name,
            ._RegionEnabled = True,
            ._UUID = Guid.NewGuid().ToString,
            ._SizeX = 256,
            ._SizeY = 256,
            ._CoordX = LargestX() + 4,
            ._CoordY = LargestY() + 0,
            ._RegionPort = Form1.Settings.PrivatePort + 1, '8003 + 1
            ._ProcessID = 0,
            ._AvatarCount = 0,
            ._Status = SIMSTATUSENUM.Stopped,
            ._LineCounter = 0,
            ._Timer = 0,
            ._NonPhysicalPrimMax = 1024,
            ._PhysicalPrimMax = 64,
            ._ClampPrimSize = False,
            ._MaxPrims = 15000,
            ._MaxAgents = 100,
            ._MapType = "",
            ._MinTimerInterval = 0.2,
            ._AllowGods = "",
            ._RegionGod = "",
            ._ManagerGod = "",
            ._Birds = "",
            ._Tides = "",
            ._Teleport = "",
            ._RegionSnapShot = "",
            ._DisableGloebits = "",
            ._FrameTime = "",
            ._RegionSmartStart = False
        }

        RegionList.Add(r)
        'RegionDump()
        Return RegionList.Count - 1

    End Function

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

    Public Sub GetAllRegions()
        Try
            Backup.Clear()

            For Each thing As Region_data In RegionList
                Backup.Add(thing)
            Next

            RegionList.Clear()

            Dim folders() As String
            Dim regionfolders() As String
            Dim n As Integer = 0
            folders = Directory.GetDirectories(Form1.PropOpensimBinPath + "bin\Regions")
            For Each FolderName As String In folders
                'Form1.Log("Info","Region Path:" + FolderName)
                regionfolders = Directory.GetDirectories(FolderName)
                For Each FileName As String In regionfolders

                    Dim fName = ""
                    Try
                        'Form1.Log("Info","Loading region from " + FolderName)
                        Dim inis = Directory.GetFiles(FileName, "*.ini", SearchOption.TopDirectoryOnly)

                        For Each ini As String In inis
                            fName = System.IO.Path.GetFileNameWithoutExtension(ini)

                            ' make a slot to hold the region data
                            CreateRegion(fName)

                            ' must be after Createregion or port blows up
                            Form1.Settings.LoadIni(ini, ";")
                            ' we do not save the above as we are making a new one.

                            RegionEnabled(n) = CBool(Form1.Settings.GetIni(fName, "Enabled", "True"))

                            RegionPath(n) = ini ' save the path
                            FolderPath(n) = System.IO.Path.GetDirectoryName(ini)

                            Dim theEnd As Integer = FolderPath(n).LastIndexOf("\", StringComparison.InvariantCulture)
                            IniPath(n) = FolderPath(n).Substring(0, theEnd + 1)

                            ' need folder name in case there are more than 1 ini
                            Dim theStart = FolderPath(n).IndexOf("Regions\", StringComparison.InvariantCulture) + 8
                            theEnd = FolderPath(n).LastIndexOf("\", StringComparison.InvariantCulture)
                            Dim gname = FolderPath(n).Substring(theStart, theEnd - theStart)

                            GroupName(n) = gname

                            UUID(n) = Form1.Settings.GetIni(fName, "RegionUUID", "")
                            SizeX(n) = CInt(Form1.Settings.GetIni(fName, "SizeX", "256"))
                            SizeY(n) = CInt(Form1.Settings.GetIni(fName, "SizeY", "256"))
                            RegionPort(n) = CInt(Form1.Settings.GetIni(fName, "InternalPort", "0"))

                            ' extended props V2.1
                            NonPhysicalPrimMax(n) = CInt(Form1.Settings.GetIni(fName, "NonPhysicalPrimMax", "1024"))
                            PhysicalPrimMax(n) = CInt(Form1.Settings.GetIni(fName, "PhysicalPrimMax", "64"))
                            ClampPrimSize(n) = CBool(Form1.Settings.GetIni(fName, "ClampPrimSize", "False"))
                            MaxPrims(n) = CInt(Form1.Settings.GetIni(fName, "MaxPrims", "15000"))
                            MaxAgents(n) = CInt(Form1.Settings.GetIni(fName, "MaxAgents", "100"))

                            ' Location is int,int format.
                            Dim C = Form1.Settings.GetIni(fName, "Location", "1000,1000")

                            Dim parts As String() = C.Split(New Char() {","c}) ' split at the comma
                            CoordX(n) = CInt(parts(0))
                            CoordY(n) = CInt(parts(1))

                            ' options params coming from INI file can be blank!
                            MinTimerInterval(n) = CSng(Form1.Settings.GetIni(fName, "MinTimerInterval", CStr(1 / 11)))
                            RegionSnapShot(n) = Form1.Settings.GetIni(fName, "RegionSnapShot", "")
                            MapType(n) = Form1.Settings.GetIni(fName, "MapType", "")
                            Physics(n) = Form1.Settings.GetIni(fName, "Physics", "")
                            MaxPrims(n) = CInt(Form1.Settings.GetIni(fName, "MaxPrims", ""))
                            AllowGods(n) = Form1.Settings.GetIni(fName, "AllowGods", "")
                            RegionGod(n) = Form1.Settings.GetIni(fName, "RegionGod", "")
                            ManagerGod(n) = Form1.Settings.GetIni(fName, "ManagerGod", "")
                            Birds(n) = Form1.Settings.GetIni(fName, "Birds", "")
                            Tides(n) = Form1.Settings.GetIni(fName, "Tides", "")
                            Teleport(n) = Form1.Settings.GetIni(fName, "Teleport", "")
                            DisableGloebits(n) = Form1.Settings.GetIni(fName, "DisableGloebits", "")
                            DisallowForeigners(n) = Form1.Settings.GetIni(fName, "DisallowForeigners", "")
                            DisallowResidents(n) = Form1.Settings.GetIni(fName, "DisallowResidents", "")

                            Snapshot(n) = Form1.Settings.GetIni(fName, "RegionSnapShot", "")

                            Select Case Form1.Settings.GetIni(fName, "SmartStart", "False")
                                Case "True"
                                    SmartStart(n) = True
                                Case "False"
                                    SmartStart(n) = False
                                Case Else
                                    SmartStart(n) = False
                            End Select

                            If initted Then

                                ' restore backups of transient data
                                Try ' 6 temp vars from backup
                                    Dim o = FindBackupByName(fName)

                                    If o >= 0 Then
                                        AvatarCount(n) = CInt(Backup(o)._AvatarCount)
                                        ProcessID(n) = CInt(Backup(o)._ProcessID)
                                        Status(n) = CInt(Backup(o)._Status)
                                        LineCounter(n) = CInt(Backup(o)._LineCounter)
                                        Timer(n) = CInt(Backup(o)._Timer)
                                    End If
                                Catch
                                End Try

                            End If

                            n += 1
                            Application.DoEvents()
                        Next
                    Catch ex As Exception
                        MsgBox("Error: Cannot understand the contents of region file " + fName + " : " + ex.Message, vbInformation, "Error")
                        Form1.ErrorLog("Err:Parse file " + fName + ":" + ex.Message)
                    End Try
                Next
            Next

            initted = True
        Catch
        End Try

    End Sub

    Public Function LargestPort() As Integer

        ' locate largest port
        Dim Max As Integer = 0
        Dim Portlist As New Dictionary(Of Integer, String)

        For Each obj As Region_data In RegionList
            Try
                Portlist.Add(obj._RegionPort, obj._RegionName)
            Catch ex As Exception
                Debug.Print(ex.Message)
            End Try
        Next

        If Portlist.Count = 0 Then
            Return 0
        End If

        For Each thing In Portlist
            If thing.Key > Max Then
                Max = thing.Key ' max is always the current value
            End If

            If Not Portlist.ContainsKey(Max + 1) Then
                Return Max  ' Found a blank spot at Max + 1 so return Max
            End If
        Next

        Return Max

    End Function

    Public Function LargestX() As Integer

        ' locate largest global coords
        Dim Max As Integer
        For Each obj As Region_data In RegionList
            If obj._CoordX > Max Then Max = obj._CoordX
        Next
        If Max = 0 Then Max = 996 ' (1000 - 4 so 1st region ends up at 1000)
        Return Max

    End Function

    Public Function LargestY() As Integer

        ' locate largest global coords
        Dim Max As Integer
        For Each obj As Region_data In RegionList
            Dim val = obj._CoordY
            If val > Max Then Max = val
        Next
        If Max = 0 Then Max = 1000
        Return Max

    End Function

    Function LowestPort() As Integer
        ' locate lowest port
        Dim Min As Integer = 65536
        Dim Portlist As New Dictionary(Of Integer, String)

        For Each obj As Region_data In RegionList
            Try
                Portlist.Add(obj._RegionPort, obj._RegionName)
            Catch ex As Exception
                Debug.Print(ex.Message)
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

    Public Sub WriteRegionObject(name As String)

        Dim n As Integer = FindRegionByName(name)
        If n < 0 Then
            MsgBox("Cannot find region " + name, vbInformation, "Error")
            Return
        End If

        Dim fname As String = RegionList(n)._FolderPath

        If (fname.Length = 0) Then
            Dim pathtoWelcome As String = Form1.PropOpensimBinPath + "bin\Regions\" + name + "\Region\"
            fname = pathtoWelcome + name + ".ini"
            If Not Directory.Exists(pathtoWelcome) Then
                Try
                    Directory.CreateDirectory(pathtoWelcome)
                Catch ex As Exception
                    Form1.ErrorLog("WriteRegionObject " & ex.Message)
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
        & "RegionUUID = " & UUID(n) & vbCrLf _
        & "Location = " & CStr(CoordX(n)) & "," & CoordY(n) & vbCrLf _
        & "InternalAddress = 0.0.0.0" & vbCrLf _
        & "InternalPort = " & RegionPort(n) & vbCrLf _
        & "AllowAlternatePorts = False" & vbCrLf _
        & "ExternalHostName = " & Form1.ExternLocalServerName() & vbCrLf _
        & "SizeX = " & CStr(SizeX(n)) & vbCrLf _
        & "SizeY = " & CStr(SizeY(n)) & vbCrLf _
        & "Enabled = " & CStr(RegionEnabled(n)) & vbCrLf _
        & "NonPhysicalPrimMax = " & CStr(NonPhysicalPrimMax(n)) & vbCrLf _
        & "PhysicalPrimMax = " & CStr(PhysicalPrimMax(n)) & vbCrLf _
        & "ClampPrimSize = " & CStr(ClampPrimSize(n)) & vbCrLf _
        & "MaxPrims = " & MaxPrims(n) & vbCrLf _
        & "RegionType = Estate" & vbCrLf _
        & "MaxAgents = 100" & vbCrLf & vbCrLf _
        & ";# Dreamgrid extended properties" & vbCrLf _
        & "RegionSnapShot = " & RegionSnapShot(n) & vbCrLf _
        & "MapType = " & MapType(n) & vbCrLf _
        & "Physics = " & Physics(n) & vbCrLf _
        & "AllowGods = " & AllowGods(n) & vbCrLf _
        & "RegionGod = " & RegionGod(n) & vbCrLf _
        & "ManagerGod = " & ManagerGod(n) & vbCrLf _
        & "Birds = " & Birds(n) & vbCrLf _
        & "Tides = " & Tides(n) & vbCrLf _
        & "Teleport = " & Teleport(n) & vbCrLf _
        & "DisableGloebits = " & DisableGloebits(n) & vbCrLf _
        & "DisallowForeigners = " & DisallowForeigners(n) & vbCrLf _
        & "DisallowResidents = " & DisallowResidents(n) & vbCrLf _
        & "MinTimerInterval =" & vbCrLf _
        & "Frametime =" & vbCrLf _
        & "SmartStart =" & SmartStart(n) & vbCrLf

        FileStuff.DeleteFile(fname)

        Using outputFile As New StreamWriter(fname, True)
            outputFile.WriteLine(proto)
        End Using

    End Sub

    ' hold a copy of the Main region data on a per-form basis
    Private Class Region_data

        Public _AvatarCount As Integer = 0
        Public _ClampPrimSize As Boolean = False
        Public _CoordX As Integer = 1000
        Public _CoordY As Integer = 1000
        Public _DisallowForeigners As String = ""
        Public _DisallowResidents As String = ""
        Public _FolderPath As String = ""
        Public _Group As String = ""  ' the path to the folder that holds the region ini
        Public _IniPath As String = "" ' the folder name that holds the region(s), can be different named
        Public _LineCounter As Integer = 0
        Public _MaxAgents As Integer = 100
        Public _MaxPrims As Integer = 15000
        Public _MinTimerInterval As Single = 1 / 11
        Public _NonPhysicalPrimMax As Integer = 1024
        Public _PhysicalPrimMax As Integer
        Public _ProcessID As Integer = 0
        Public _RegionEnabled As Boolean = False
        Public _RegionName As String = ""
        Public _RegionPath As String = ""  ' The full path to the region ini file
        Public _RegionPort As Integer = 0
        Public _RegionSmartStart As Boolean = False
        Public _SizeX As Integer = 256
        Public _SizeY As Integer = 256
        Public _Status As Integer = 0
        Public _Timer As Integer = 0
        Public _UUID As String = ""

#End Region

#Region "OptionalStorage"

        ''' <summary>
        ''' <Must be all strings as a blank means use the default
        ''' </summary>
        'extended vars
        Public _AllowGods As String = ""

        Public _Birds As String = ""
        Public _DisableGloebits As String = ""
        Public _FrameTime As String = ""
        Public _ManagerGod As String = ""
        Public _MapType As String = ""
        Public _Physics As String = "  "
        Public _RegionGod As String = ""
        Public _RegionSnapShot As String = ""
        Public _Snapshot As String = ""
        Public _Teleport As String = ""
        Public _Tides As String = ""

#End Region

    End Class

#Region "Properties"

    Public ReadOnly Property RegionCount() As Integer
        Get
            Return RegionList.Count
        End Get
    End Property

    Public Property AllowGods(n As Integer) As String
        Get
            Return RegionList(n)._AllowGods
        End Get
        Set(ByVal Value As String)
            RegionList(n)._AllowGods = Value
        End Set
    End Property

    Public Property AvatarCount(n As Integer) As Integer
        Get
            Return CInt(RegionList(n)._AvatarCount)
        End Get
        Set(ByVal Value As Integer)
            RegionList(n)._AvatarCount = Value
        End Set
    End Property

    Public Property Birds(n As Integer) As String
        Get
            Return RegionList(n)._Birds
        End Get
        Set(ByVal Value As String)
            RegionList(n)._Birds = Value
        End Set
    End Property

    Public Property ClampPrimSize(n As Integer) As Boolean
        Get
            Return CBool(RegionList(n)._ClampPrimSize)
        End Get
        Set(ByVal Value As Boolean)
            RegionList(n)._ClampPrimSize = Value
        End Set
    End Property

    Public Property CoordX(n As Integer) As Integer
        Get
            Return CInt(RegionList(n)._CoordX)
        End Get
        Set(ByVal Value As Integer)
            RegionList(n)._CoordX = Value
        End Set
    End Property

    Public Property CoordY(n As Integer) As Integer
        Get
            Return CInt(RegionList(n)._CoordY)
        End Get
        Set(ByVal Value As Integer)
            RegionList(n)._CoordY = Value
        End Set
    End Property

    Public Property DisallowForeigners(n As Integer) As String
        Get
            Return RegionList(n)._DisallowForeigners
        End Get
        Set(ByVal Value As String)
            RegionList(n)._DisallowForeigners = Value
        End Set
    End Property

    Public Property DisallowResidents(n As Integer) As String
        Get
            Return RegionList(n)._DisallowResidents
        End Get
        Set(ByVal Value As String)
            RegionList(n)._DisallowResidents = Value
        End Set
    End Property

    Public Property FolderPath(n As Integer) As String
        Get
            Return CStr(RegionList(n)._FolderPath)
        End Get
        Set(ByVal Value As String)
            RegionList(n)._FolderPath = Value
        End Set
    End Property

    Public Property GroupName(n As Integer) As String
        Get
            Return RegionList(n)._Group
        End Get
        Set(ByVal Value As String)
            RegionList(n)._Group = Value
        End Set
    End Property

    ''' ''''''''''''''''''' PATHS ''''''''''''''''''''
    Public Property IniPath(n As Integer) As String
        Get
            Return CStr(RegionList(n)._IniPath)
        End Get
        Set(ByVal Value As String)
            RegionList(n)._IniPath = Value
        End Set
    End Property

    Public Property LineCounter(n As Integer) As Integer
        Get
            Return CInt(RegionList(n)._LineCounter)
        End Get
        Set(ByVal Value As Integer)
            RegionList(n)._LineCounter = Value
        End Set
    End Property

    Public Property ManagerGod(n As Integer) As String
        Get
            Return RegionList(n)._ManagerGod
        End Get
        Set(ByVal Value As String)
            RegionList(n)._ManagerGod = Value
        End Set
    End Property

    Public Property MapType(n As Integer) As String
        Get
            Return RegionList(n)._MapType
        End Get
        Set(ByVal Value As String)
            RegionList(n)._MapType = Value
        End Set
    End Property

    Public Property MaxAgents(n As Integer) As Integer
        Get
            Return RegionList(n)._MaxAgents
        End Get
        Set(ByVal Value As Integer)
            RegionList(n)._MaxAgents = Value
        End Set
    End Property

    Public Property MaxPrims(n As Integer) As Integer
        Get
            Return RegionList(n)._MaxPrims
        End Get
        Set(ByVal Value As Integer)
            RegionList(n)._MaxPrims = Value
        End Set
    End Property

    Public Property MinTimerInterval(n As Integer) As Single
        Get
            Return RegionList(n)._MinTimerInterval
        End Get
        Set(ByVal Value As Single)
            RegionList(n)._MinTimerInterval = Value
        End Set
    End Property

    Public Property NonPhysicalPrimMax(n As Integer) As Integer
        Get
            Return RegionList(n)._NonPhysicalPrimMax
        End Get
        Set(ByVal Value As Integer)
            RegionList(n)._NonPhysicalPrimMax = Value
        End Set
    End Property

    Public Property PhysicalPrimMax(n As Integer) As Integer
        Get
            Return RegionList(n)._PhysicalPrimMax
        End Get
        Set(ByVal Value As Integer)
            RegionList(n)._PhysicalPrimMax = Value
        End Set
    End Property

    Public Property Physics(n As Integer) As String
        Get
            Return RegionList(n)._Physics
        End Get
        Set(ByVal Value As String)
            RegionList(n)._Physics = Value
        End Set
    End Property

    Public Property ProcessID(n As Integer) As Integer
        Get
            Try
                Return RegionList(n)._ProcessID
            Catch
                Return 0
            End Try

        End Get
        Set(ByVal Value As Integer)
            RegionList(n)._ProcessID = Value
        End Set
    End Property

    Public Property RegionEnabled(n As Integer) As Boolean
        Get
            Return RegionList(n)._RegionEnabled
        End Get
        Set(ByVal Value As Boolean)
            RegionList(n)._RegionEnabled = Value
        End Set
    End Property

    Public Property RegionGod(n As Integer) As String
        Get
            Return RegionList(n)._RegionGod
        End Get
        Set(ByVal Value As String)
            RegionList(n)._RegionGod = Value
        End Set
    End Property

    Public Property RegionName(n As Integer) As String
        Get
            Return RegionList(n)._RegionName
        End Get
        Set(ByVal Value As String)
            RegionList(n)._RegionName = Value
        End Set
    End Property

    Public Property RegionPath(n As Integer) As String
        Get
            Return RegionList(n)._RegionPath
        End Get
        Set(ByVal Value As String)
            RegionList(n)._RegionPath = Value
        End Set
    End Property

    Public Property RegionPort(n As Integer) As Integer
        Get
            Try
                Return RegionList(n)._RegionPort
            Catch
                Form1.ErrorLog("Bad region port: " + CStr(RegionList(n)._RegionPort))
            End Try
            Return 0
        End Get
        Set(ByVal Value As Integer)
            RegionList(n)._RegionPort = Value
        End Set
    End Property

    Public Property SizeX(n As Integer) As Integer
        Get
            Return RegionList(n)._SizeX
        End Get
        Set(ByVal Value As Integer)
            RegionList(n)._SizeX = Value
        End Set
    End Property

    Public Property SizeY(n As Integer) As Integer
        Get
            Return RegionList(n)._SizeY
        End Get
        Set(ByVal Value As Integer)
            RegionList(n)._SizeY = Value
        End Set
    End Property

    Public Property SmartStart(n As Integer) As Boolean

        Get
            Return RegionList(n)._RegionSmartStart
        End Get
        Set(ByVal Value As Boolean)
            RegionList(n)._RegionSmartStart = Value
        End Set

    End Property

    Public Property Snapshot(n As Integer) As String
        Get
            Return RegionList(n)._Snapshot
        End Get
        Set(ByVal Value As String)
            RegionList(n)._Snapshot = Value
        End Set
    End Property

    Public Property Status(n As Integer) As Integer
        Get
            Return RegionList(n)._Status
        End Get
        Set(ByVal Value As Integer)
            RegionList(n)._Status = Value
        End Set
    End Property

    Public Property Timer(n As Integer) As Integer
        Get
            Try
                Return RegionList(n)._Timer
            Catch
                Return 0
            End Try

        End Get
        Set(ByVal Value As Integer)
            RegionList(n)._Timer = Value
        End Set
    End Property

    Public Property UUID(n As Integer) As String
        Get
            Return RegionList(n)._UUID
        End Get
        Set(ByVal Value As String)
            RegionList(n)._UUID = Value
        End Set
    End Property

#End Region

#Region "Options"

    Public Property FrameTime(n As Integer) As String
        Get
            If RegionList(n)._FrameTime.Length = 0 Then
                Return CStr(1 / 11)
            End If
            Return RegionList(n)._FrameTime
        End Get
        Set(ByVal Value As String)
            RegionList(n)._FrameTime = Value
        End Set
    End Property

    Public Property DisableGloebits(n As Integer) As String
        Get
            Return RegionList(n)._DisableGloebits
        End Get
        Set(ByVal Value As String)
            RegionList(n)._DisableGloebits = Value
        End Set
    End Property

    Public Property RegionSnapShot(n As Integer) As String
        Get
            Return RegionList(n)._RegionSnapShot
        End Get
        Set(ByVal Value As String)
            RegionList(n)._RegionSnapShot = Value
        End Set
    End Property

    Public Property Teleport(n As Integer) As String
        Get
            Return RegionList(n)._Teleport
        End Get
        Set(ByVal Value As String)
            RegionList(n)._Teleport = Value
        End Set
    End Property

    Public Property Tides(n As Integer) As String
        Get
            Return RegionList(n)._Tides
        End Get
        Set(ByVal Value As String)
            RegionList(n)._Tides = Value
        End Set
    End Property

#End Region

#Region "Functions"

    Public Sub DebugGroup()
        For Each pair In Grouplist
            Debug.Print("Group name: {0}, httpport: {1}", pair.Key, pair.Value)
        Next
    End Sub

    Public Sub DebugRegions(n As Integer)

        Form1.Log("RegionNumber", CStr(n) & vbCrLf &
            " PID:" & RegionList(n)._ProcessID & vbCrLf &
            " Group:" & RegionList(n)._Group & vbCrLf &
            " Region:" & RegionList(n)._RegionName & vbCrLf &
            " Status=" & CStr(RegionList(n)._Status) & vbCrLf &
            " LineCtr=" & CStr(RegionList(n)._LineCounter) & vbCrLf &
           " RegionEnabled=" & RegionList(n)._RegionEnabled & vbCrLf &
           " Timer=" & CStr(RegionList(n)._Timer))

    End Sub

    Public Function FindRegionByName(Name As String) As Integer

        Dim i As Integer = 0
        For Each obj As Region_data In RegionList
            If Name = obj._RegionName Then
                Debug.Print("Current Region is " + obj._RegionName)
                Return i
            End If
            i += 1
        Next

        'RegionDump()
        Return -1

    End Function

    Public Function IsBooted(RegionNumber As Integer) As Boolean
        If Status(RegionNumber) = SIMSTATUSENUM.Booted Then
            Return True
        End If
        Return False
    End Function

    Public Sub RegionDump()

        If Not Form1.PropDebug Then Return
        Dim ctr = 0
        For Each r As Region_data In RegionList
            DebugRegions(ctr)
            ctr += 1
        Next

    End Sub

    Public Function RegionListByGroupNum(Gname As String) As List(Of Integer)

        Dim L As New List(Of Integer)
        Dim ctr = 0
        For Each n As Region_data In RegionList
            If n._Group = Gname Or Gname = "*" Then
                L.Add(ctr)
            End If
            ctr += 1
        Next
        If (L.Count = 0) Then
            Debug.Print(" Not found:" & Gname)
        End If
        Return L

    End Function

    Public Function RegionNumbers() As List(Of Integer)
        Dim L As New List(Of Integer)
        Dim ctr = 0
        For Each n As Region_data In RegionList
            L.Add(ctr)
            ctr += 1
        Next
        'Debug.Print("List Len = " + L.Count.ToString)
        Return L
    End Function

    Private Function FindRegionByUUID(Name As String) As Integer

        Dim i As Integer = 0
        For Each obj As Region_data In RegionList
            If Name = obj._UUID Then
                Debug.Print("Current Region is " + obj._RegionName)
                Return i
            End If
            i += 1
        Next
        Return -1

    End Function

#End Region

#Region "POST"

    Shared Function CheckPassword(POST As String, Machine As String) As Boolean

        ' Returns true is password is blank or matching
        Dim pattern1 As Regex = New Regex("PW=(.*?)&")
        Dim match1 As Match = pattern1.Match(POST)
        If match1.Success Then
            Dim p1 As String = match1.Groups(1).Value
            If p1.Length = 0 Then Return True
            If Machine = p1.ToLower(Form1.Invarient) Then Return True
        End If
        Return False

    End Function

    Shared Function GetAgentNameByUUID(UUID As String) As String

        If Form1.Settings.ServerType <> "Robust" Then Return ""

        Dim myConnection As MySqlConnection = New MySqlConnection(Form1.Settings.RobustMysqlConnection)
        Dim Query1 = "Select userid from robust.griduser where userid like @p1;"
        Dim Name As String = ""
        Using myCommand1 As MySqlCommand = New MySqlCommand(Query1) With {
                .Connection = myConnection
            }
            myConnection.Open()
            myCommand1.Prepare()
            myCommand1.Parameters.AddWithValue("p1", UUID & "%")
            Name = CStr(myCommand1.ExecuteScalar())
        End Using

        Debug.Print("User=" + UUID + ", name=" + Name)
        Dim pattern As Regex = New Regex(".*?;.*?;(.*)")
        Dim match As Match = pattern.Match(Name)
        If match.Success Then
            Name = match.Groups(1).Value
            Debug.Print("User=" + UUID + ", name=" + Name)
            myConnection.Close()
            Return Name
        End If
        myConnection.Close()
        Return ""
    End Function

    Shared Function GetPartner(p1 As String, Mysetting As MySettings) As String

        If Mysetting Is Nothing Then
            Return ""
        End If
        Dim myConnection As MySqlConnection = New MySqlConnection(Mysetting.RobustMysqlConnection)
        Dim Query1 = "Select profilepartner from robust.userprofile where userUUID=@p1;"
        Dim myCommand1 As MySqlCommand = New MySqlCommand(Query1) With {
            .Connection = myConnection
        }
        myConnection.Open()
        myCommand1.Prepare()
        myCommand1.Parameters.AddWithValue("p1", p1)
        Dim a = CStr(myCommand1.ExecuteScalar())
        Debug.Print("User=" + p1 + ", Partner=" + a)

        myConnection.Close()
        myCommand1.Dispose()
        Return a

    End Function

    Public Function ParsePost(POST As String, Settings As MySettings) As String

        If Settings Is Nothing Then Return "<html><head></head><body>Error</html>"
        If POST Is Nothing Then Return "<html><head></head><body>Error</html>"
        ' set Region.Booted to true if the POST from the region indicates it is online requires a
        ' section in Opensim.ini where [RegionReady] has this:

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
        '{"alert":"region_ready","login":"enabled","region_name":"Region 2","region_id":"19f6adf0-5f35-4106-bcb8-dc3f2e846b89"}

        ' we want region name, UUID and server_startup could also be a probe from the outworldz to
        ' check if ports are open.

        ' WarmingUp(0) = True ShuttingDown(1) = True

        ' alerts need to be fast so we stash them on a list and process them on a 10 second timer.

        If (POST.Contains("""alert"":""region_ready""")) Then

            WebserverList.Add(POST)

        ElseIf POST.Contains("ALT=") Then
            ' mart Start AutoStart Region mode
            Debug.Print("Smart Start:" + POST)

            ' Auto Start Telport, AKA Smart Start
            Dim RegionUUID As String = ""
            Dim pattern As Regex = New Regex("ALT=(.*?)/AGENT=(.*)")
            Dim match As Match = pattern.Match(POST)
            If match.Success Then
                RegionUUID = match.Groups(1).Value
                Dim AgentUUID = match.Groups(2).Value
                Dim n = FindRegionByUUID(RegionUUID)
                If n > -1 And RegionEnabled(n) And SmartStart(n) Then
                    If Status(n) = SIMSTATUSENUM.Booted Then
                        Form1.Print("Avatar in " & RegionName(n))
                        Debug.Print("Sending to " & RegionUUID)
                        Return RegionUUID
                    Else
                        Form1.Print("Smart Start " & RegionName(n))
                        Status(n) = SIMSTATUSENUM.Autostart
                        Try
                            TeleportAvatarDict.Remove(RegionName(n))
                        Catch ex As Exception
                        End Try

                        TeleportAvatarDict.Add(AgentUUID, RegionName(n))

                        ' redirect to welcome
                        Dim wname = Settings.WelcomeRegion
                        Dim RegionNum As Integer = FindRegionByName(wname)
                        Debug.Print("Sending to " & UUID(RegionNum))
                        Return UUID(RegionNum)
                    End If
                    'other states we can ignore as eventually it will be Stopped or Running
                End If
            End If

            ' HG Sim, perhaps,. it is not found, not enabled, not Smart Start,let it work normally
            Return RegionUUID

        ElseIf POST.Contains("TOS") Then
            ' currently unused as is only in standalones
            Debug.Print("UUID:" + POST)
            '"POST /TOS HTTP/1.1" & vbCrLf & "Host: mach.outworldz.net:9201" & vbCrLf & "Connection: keep-alive" & vbCrLf & "Content-Length: 102" & vbCrLf & "Cache-Control: max-age=0" & vbCrLf & "Upgrade-Insecure-Requests: 1" & vbCrLf & "User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.99 Safari/537.36" & vbCrLf & "Origin: http://mach.outworldz.net:9201" & vbCrLf & "Content-Type: application/x-www-form-urlencoded" & vbCrLf & "DNT: 1" & vbCrLf & "Accept: text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8" & vbCrLf & "Referer: http://mach.outworldz.net:9200/wifi/termsofservice.html?uid=acb8fd92-c725-423f-b750-5fd971d73182&sid=40c5b80a-5377-4b97-820c-a0952782a701" & vbCrLf & "Accept-Encoding: gzip, deflate" & vbCrLf & "Accept-Language: en-US,en;q=0.9" & vbCrLf & vbCrLf &
            '"action-accept=Accept&uid=acb8fd92-c725-423f-b750-5fd971d73182&sid=40c5b80a-5377-4b97-820c-a0952782a701"

            Return "<html><head></head><body>Error</html>"

            Dim uid As Guid
            Dim sid As Guid

            Try
                POST = POST.Replace("{ENTER}", "")
                POST = POST.Replace("\r", "")

                Dim pattern As Regex = New Regex("uid=([0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12})")
                Dim match As Match = pattern.Match(POST)
                If match.Success Then
                    uid = Guid.Parse(match.Groups(1).Value)
                End If

                Dim pattern2 As Regex = New Regex("sid=([0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12})")
                Dim match2 As Match = pattern2.Match(POST)
                If match2.Success Then
                    sid = Guid.Parse(match2.Groups(1).Value)
                End If

                If match.Success And match2.Success Then

                    ' Only works in Standalone, anyway. Not implemented at all in Grid mode - the
                    ' Diva DLL Diva is stubbed off.
                    Dim result As Integer = 1

                    Dim myConnection As MySqlConnection = New MySqlConnection(Form1.Settings.RobustMysqlConnection)

                    Dim Query1 = "update opensim.griduser set TOS = 1 where UserID = @p1; "
                    Dim myCommand1 As MySqlCommand = New MySqlCommand(Query1) With {
                        .Connection = myConnection
                    }
                    myConnection.Open()
                    myCommand1.Prepare()
                    myCommand1.Parameters.AddWithValue("p1", uid.ToString())
                    myCommand1.ExecuteScalar()
                    myConnection.Close()
                    Return "<html><head></head><body>Welcome! You can close this window.</html>"
                Else
                    Return "<html><head></head><body>Test Passed</html>"
                End If
            Catch ex As Exception
                Return "<html><head></head><body>Error</html>"
            End Try

        ElseIf POST.Contains("get_partner") Then
            Debug.Print("get Partner")
            Dim PWok As Boolean = CheckPassword(POST, Settings.MachineID().ToLower(Form1.Invarient))
            If Not PWok Then Return ""

            Dim pattern1 As Regex = New Regex("User=(.*)")
            Dim match1 As Match = pattern1.Match(POST)
            Dim p1 As String
            If match1.Success Then
                p1 = match1.Groups(1).Value
                Dim s = GetPartner(p1, Settings)
                Debug.Print(s)
                Return s
            Else
                Debug.Print("No partner")
                Return "00000000-0000-0000-0000-000000000000"
            End If

            ' Partner prim
        ElseIf POST.Contains("set_partner") Then
            Debug.Print("set Partner")
            Dim PWok As Boolean = CheckPassword(POST, CStr(Settings.MachineID()))
            If Not PWok Then Return ""

            Dim pattern1 As Regex = New Regex("User=(.*?)&")
            Dim match1 As Match = pattern1.Match(POST)
            If match1.Success Then

                Dim p2 As String = ""
                Dim p1 = match1.Groups(1).Value
                Dim pattern2 As Regex = New Regex("Partner=(.*)")
                Dim match2 As Match = pattern2.Match(POST)
                If match2.Success Then
                    p2 = match2.Groups(1).Value
                End If
                Dim result As New Guid
                If Guid.TryParse(p1, result) And Guid.TryParse(p1, result) Then
                    Dim Partner = GetPartner(p1, Settings)
                    Debug.Print("Partner=" + p2)

                    Try
                        Dim myConnection As MySqlConnection = New MySqlConnection(Settings.RobustMysqlConnection)
                        Dim Query1 = "update robust.userprofile set profilepartner=@p2 where userUUID = @p1; "
                        Using myCommand1 As MySqlCommand = New MySqlCommand(Query1) With {
                                .Connection = myConnection
                            }
                            myConnection.Open()
                            myCommand1.Prepare()

                            myCommand1.Parameters.AddWithValue("p1", p1)
                            myCommand1.Parameters.AddWithValue("p2", p2)

                            myCommand1.ExecuteScalar()
                            myConnection.Close()
                        End Using
                    Catch ex As Exception
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

End Class