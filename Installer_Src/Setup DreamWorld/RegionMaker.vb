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

    Private Shared FInstance As RegionMaker = Nothing
    Private _Grouplist As New Dictionary(Of String, Integer)
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
        [Resume] = 8
        Suspended = 9
        [Error] = 10

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
            If _Grouplist.ContainsKey(RegionName) Then
                Return _Grouplist.Item(RegionName)
            End If
            Return 0
        End Get
        Set(ByVal Value As Integer)

            Dim RegionName = GroupName(index)
            If _Grouplist.ContainsKey(RegionName) Then
                _Grouplist.Remove(RegionName)
                _Grouplist.Add(RegionName, Value)
            Else
                _Grouplist.Add(RegionName, Value)
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
        Debug.Print("Loaded " + CStr(RegionCount) + " Regions")

    End Sub

#End Region

#Region "Classes"

    ''' <summary>
    ''' Self setting Region Ports Iterate over all regions and set the ports from the starting value
    ''' </summary>
    Public Shared Sub UpdateAllRegionPorts()

        If Form1.PropOpensimIsRunning Then
            Return
        End If

        Form1.Print(My.Resources.Updating_Ports_word)

        Dim Portnumber As Integer = CInt(Form1.Settings.FirstRegionPort())
        For Each RegionNum As Integer In Form1.PropRegionClass.RegionNumbers
            Dim simName = Form1.PropRegionClass.RegionName(RegionNum)
            Form1.Settings.LoadIni(Form1.PropRegionClass.RegionPath(RegionNum), ";")

            Form1.Settings.SetIni(simName, "InternalPort", CStr(Portnumber))
            Form1.PropRegionClass.RegionPort(RegionNum) = Portnumber
            ' Self setting Region Ports
            Form1.PropMaxPortUsed = Portnumber
            Form1.Settings.SaveINI(System.Text.Encoding.UTF8)
            Portnumber += 1
            Application.DoEvents()
        Next

        If Not Form1.Settings.PortsChanged Then Return
        Form1.Print(My.Resources.Setup_Firewall_word)
        Firewall.SetFirewall()   ' must be after UpdateAllRegionPorts

        Form1.Settings.PortsChanged = False

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
#Disable Warning CA1031 ' Do not catch general exception types
                Catch ex As Exception
#Enable Warning CA1031 ' Do not catch general exception types
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
                    Form1.Print(json.region_name & " " & My.Resources.Ready)

                    Dim RegionNumber = FindRegionByName(json.region_name)
                    If RegionNumber < 0 Then
                        Return
                    End If

                    RegionEnabled(RegionNumber) = True
                    Status(RegionNumber) = SIMSTATUSENUM.Booted
                    Form1.PropUpdateView() = True

                    If Debugger.IsAttached = True Then
                        Try
                            '! debug TeleportAvatarDict.Add("Test", "Test User")
                        Catch ex As ArgumentException
                        End Try
                    End If

                    Dim Removelist As New List(Of String)
                    If Form1.Settings.SmartStart Then
                        For Each Keypair In TeleportAvatarDict
                            Application.DoEvents()
                            If Keypair.Value = json.region_name Then
                                Dim AgentName As String = GetAgentNameByUUID(Keypair.Key)
                                If AgentName.Length > 0 Then
                                    Form1.Print(My.Resources.Teleporting_word & " " & AgentName & " -> " & Keypair.Value)
                                    Form1.ConsoleCommand(Form1.Settings.WelcomeRegion, "change region " & json.region_name & "{ENTER}")
                                    Form1.ConsoleCommand(Form1.Settings.WelcomeRegion, "teleport user " & AgentName & " " & json.region_name & "{ENTER}")
                                    Try
                                        Removelist.Add(Keypair.Key)
                                    Catch ex As ArgumentException
                                    End Try
                                End If
                            End If
                        Next
                    End If

                    ' now delete the avatars we just teleported
                    For Each Name In Removelist
                        Try
                            TeleportAvatarDict.Remove(Name)
                        Catch ex As ArgumentNullException
                        End Try
                    Next

                    If Form1.Settings.ConsoleShow = False Then
                        Dim hwnd = Form1.GetHwnd(GroupName(RegionNumber))
                        Form1.ShowDOSWindow(hwnd, Form1.SHOWWINDOWENUM.SWMINIMIZE)
                    End If

                ElseIf json.login = "shutdown" Then

                    Return ' does not work as expected

                    Form1.Print(json.region_name & " " & My.Resources.Stopped_word)

                    Dim RegionNumber = FindRegionByName(json.region_name)
                    If RegionNumber < 0 Then
                        Return
                    End If
                    Timer(RegionNumber) = REGIONTIMER.Stopped
                    If Status(RegionNumber) = SIMSTATUSENUM.RecyclingDown Then
                        Status(RegionNumber) = SIMSTATUSENUM.RestartPending
                        Form1.PropUpdateView = True ' make form refresh
                    Else
                        Status(RegionNumber) = SIMSTATUSENUM.Stopped
                    End If

                    Form1.PropUpdateView() = True
                    Form1.PropExitList.Add(json.region_name)

                End If
#Disable Warning CA1031 ' Do not catch general exception types
            Catch ex As Exception
#Enable Warning CA1031 ' Do not catch general exception types
                Debug.Print(ex.Message)
            End Try
        Next

    End Sub

    Public Function CreateRegion(name As String) As Integer

        Debug.Print("Create Region " + name)
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
            ._MinTimerInterval = 0.090909.ToString(Globalization.CultureInfo.InvariantCulture),
            ._AllowGods = "",
            ._RegionGod = "",
            ._ManagerGod = "",
            ._Birds = "",
            ._Tides = "",
            ._Teleport = "",
            ._RegionSnapShot = "",
            ._DisableGloebits = "",
            ._FrameTime = 0.090909.ToString(Globalization.CultureInfo.InvariantCulture),
            ._SkipAutobackup = "",
            ._RegionSmartStart = ""
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
            Dim RegionNumber As Integer = 0
            folders = Directory.GetDirectories(Form1.PropOpensimBinPath + "bin\Regions")
            For Each FolderName As String In folders
                'Form1.Log(My.Resources.Info,"Region Path:" + FolderName)
                regionfolders = Directory.GetDirectories(FolderName)
                For Each FileName As String In regionfolders

                    Dim fName = ""
                    Try
                        Dim inis = Nothing
                        Try
                            inis = Directory.GetFiles(FileName, "*.ini", SearchOption.TopDirectoryOnly)
                        Catch ex As ArgumentException
                        Catch ex As UnauthorizedAccessException
                        Catch ex As DirectoryNotFoundException
                        Catch ex As PathTooLongException
                        Catch ex As IOException
                        End Try

                        For Each ini As String In inis
                            fName = System.IO.Path.GetFileNameWithoutExtension(ini)

                            ' make a slot to hold the region data
                            CreateRegion(fName)

                            ' must be after Createregion or port blows up
                            Form1.Settings.LoadIni(ini, ";")
                            ' we do not save the above as we are making a new one.

                            RegionEnabled(RegionNumber) = Form1.Settings.GetIni(fName, "Enabled", "True", "Boolean")

                            RegionPath(RegionNumber) = ini ' save the path
                            FolderPath(RegionNumber) = System.IO.Path.GetDirectoryName(ini)

                            Dim theEnd As Integer = FolderPath(RegionNumber).LastIndexOf("\", StringComparison.InvariantCulture)
                            IniPath(RegionNumber) = FolderPath(RegionNumber).Substring(0, theEnd + 1)

                            ' need folder name in case there are more than 1 ini
                            Dim theStart = FolderPath(RegionNumber).IndexOf("Regions\", StringComparison.InvariantCulture) + 8
                            theEnd = FolderPath(RegionNumber).LastIndexOf("\", StringComparison.InvariantCulture)
                            Dim gname = FolderPath(RegionNumber).Substring(theStart, theEnd - theStart)

                            GroupName(RegionNumber) = gname

                            UUID(RegionNumber) = Form1.Settings.GetIni(fName, "RegionUUID", "", "String")
                            SizeX(RegionNumber) = Form1.Settings.GetIni(fName, "SizeX", "256", "Integer")
                            SizeY(RegionNumber) = Form1.Settings.GetIni(fName, "SizeY", "256", "Integer")
                            RegionPort(RegionNumber) = Form1.Settings.GetIni(fName, "InternalPort", "0", "Integer")

                            ' extended props V2.1
                            NonPhysicalPrimMax(RegionNumber) = Form1.Settings.GetIni(fName, "NonPhysicalPrimMax", "1024", "Integer")
                            PhysicalPrimMax(RegionNumber) = Form1.Settings.GetIni(fName, "PhysicalPrimMax", "64", "Integer")
                            ClampPrimSize(RegionNumber) = Form1.Settings.GetIni(fName, "ClampPrimSize", "False", "Boolean")
                            MaxPrims(RegionNumber) = Form1.Settings.GetIni(fName, "MaxPrims", "15000", "Integer")
                            MaxAgents(RegionNumber) = Form1.Settings.GetIni(fName, "MaxAgents", "100", "Integer")

                            ' Location is int,int format.
                            Dim C = Form1.Settings.GetIni(fName, "Location", RandomNumber.Between(2000, 1000) & "," & RandomNumber.Between(2000, 1000))

                            Dim parts As String() = C.Split(New Char() {","c}) ' split at the comma
                            CoordX(RegionNumber) = CInt(parts(0))
                            CoordY(RegionNumber) = CInt(parts(1))

                            ' options parameters coming from INI file can be blank!
                            MinTimerInterval(RegionNumber) = Form1.Settings.GetIni(fName, "MinTimerInterval", "", "String")
                            FrameTime(RegionNumber) = Form1.Settings.GetIni(fName, "FrameTime", "", "String")
                            RegionSnapShot(RegionNumber) = Form1.Settings.GetIni(fName, "RegionSnapShot", "", "String")
                            MapType(RegionNumber) = Form1.Settings.GetIni(fName, "MapType", "", "String")
                            Physics(RegionNumber) = Form1.Settings.GetIni(fName, "Physics", "", "String")
                            MaxPrims(RegionNumber) = Form1.Settings.GetIni(fName, "MaxPrims", "", "String")
                            AllowGods(RegionNumber) = Form1.Settings.GetIni(fName, "AllowGods", "", "String")
                            RegionGod(RegionNumber) = Form1.Settings.GetIni(fName, "RegionGod", "", "String")
                            ManagerGod(RegionNumber) = Form1.Settings.GetIni(fName, "ManagerGod", "", "String")
                            Birds(RegionNumber) = Form1.Settings.GetIni(fName, "Birds", "", "String")
                            Tides(RegionNumber) = Form1.Settings.GetIni(fName, "Tides", "", "String")
                            Teleport(RegionNumber) = Form1.Settings.GetIni(fName, "Teleport", "", "String")
                            DisableGloebits(RegionNumber) = Form1.Settings.GetIni(fName, "DisableGloebits", "", "String")
                            DisallowForeigners(RegionNumber) = Form1.Settings.GetIni(fName, "DisallowForeigners", "", "String")
                            DisallowResidents(RegionNumber) = Form1.Settings.GetIni(fName, "DisallowResidents", "", "String")
                            SkipAutobackup(RegionNumber) = Form1.Settings.GetIni(fName, "SkipAutoBackup", "", "String")
                            Snapshot(RegionNumber) = Form1.Settings.GetIni(fName, "RegionSnapShot", "", "String")

                            Select Case Form1.Settings.GetIni(fName, "SmartStart", "False", "String")
                                Case "True"
                                    SmartStart(RegionNumber) = True
                                Case "False"
                                    SmartStart(RegionNumber) = False
                                Case Else
                                    SmartStart(RegionNumber) = False
                            End Select

                            If initted Then
                                ' restore backups of transient data
                                Dim o = FindBackupByName(fName)
                                If o >= 0 Then
                                    AvatarCount(RegionNumber) = Backup(o)._AvatarCount
                                    ProcessID(RegionNumber) = Backup(o)._ProcessID
                                    Status(RegionNumber) = Backup(o)._Status
                                    LineCounter(RegionNumber) = Backup(o)._LineCounter
                                    Timer(RegionNumber) = Backup(o)._Timer
                                End If
                            End If

                            RegionNumber += 1
                            Application.DoEvents()
                        Next
#Disable Warning CA1031 ' Do not catch general exception types
                    Catch ex As Exception
#Enable Warning CA1031 ' Do not catch general exception types
                        MsgBox(My.Resources.Error_Region + fName + " : " + ex.Message, vbInformation, My.Resources.Error_word)
                        Form1.ErrorLog("Err:Parse file " + fName + ":" + ex.Message)
                    End Try
                Next
            Next

            initted = True
#Disable Warning CA1031 ' Do not catch general exception types
        Catch ex As Exception
#Enable Warning CA1031 ' Do not catch general exception types
            Debug.Print(ex.Message)
        End Try

    End Sub

    Public Function LargestPort() As Integer

        ' locate largest port
        Dim Max As Integer = 0
        Dim Portlist As New Dictionary(Of Integer, String)

        For Each obj As Region_data In RegionList
            Try
                Portlist.Add(obj._RegionPort, obj._RegionName)
            Catch ex As ArgumentNullException
                Debug.Print(ex.Message)
            Catch ex As ArgumentException
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
        If Max = 0 Then
            Max = RandomNumber.Between(2000, 996)
        End If
        Return Max

    End Function

    Public Function LargestY() As Integer

        ' locate largest global coords
        Dim Max As Integer
        For Each obj As Region_data In RegionList
            Dim val = obj._CoordY
            If val > Max Then Max = val
        Next
        If Max = 0 Then
            Max = RandomNumber.Between(2000, 1000)
        End If
        Return Max

    End Function

    Function LowestPort() As Integer
        ' locate lowest port
        Dim Min As Integer = 65536
        Dim Portlist As New Dictionary(Of Integer, String)

        For Each obj As Region_data In RegionList
            Try
                Portlist.Add(obj._RegionPort, obj._RegionName)
            Catch ex As ArgumentNullException
                Form1.ErrorLog("LowestPort" & ex.Message)
            Catch ex As ArgumentException
                Form1.ErrorLog("LowestPort" & ex.Message)
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

        Dim RegionNumber As Integer = FindRegionByName(name)
        If RegionNumber < 0 Then
            MsgBox(My.Resources.Cannot_find_region_word & " " & name, vbInformation, My.Resources.Error_word)
            Return
        End If

        Dim fname As String = RegionList(RegionNumber)._FolderPath

        If (fname.Length = 0) Then
            Dim pathtoWelcome As String = Form1.PropOpensimBinPath + "bin\Regions\" + name + "\Region\"
            fname = pathtoWelcome + name + ".ini"
            If Not Directory.Exists(pathtoWelcome) Then
                Try
                    Directory.CreateDirectory(pathtoWelcome)
                Catch ex As ArgumentException
                Catch ex As IO.PathTooLongException
                Catch ex As NotSupportedException
                Catch ex As UnauthorizedAccessException
                Catch ex As IO.IOException
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
        & "RegionUUID = " & UUID(RegionNumber) & vbCrLf _
        & "Location = " & CoordX(RegionNumber).ToString(Globalization.CultureInfo.InvariantCulture) & "," & CoordY(RegionNumber).ToString(Globalization.CultureInfo.InvariantCulture) & vbCrLf _
        & "InternalAddress = 0.0.0.0" & vbCrLf _
        & "InternalPort = " & RegionPort(RegionNumber) & vbCrLf _
        & "AllowAlternatePorts = False" & vbCrLf _
        & "ExternalHostName = " & Form1.ExternLocalServerName() & vbCrLf _
        & "SizeX = " & CStr(SizeX(RegionNumber)) & vbCrLf _
        & "SizeY = " & CStr(SizeY(RegionNumber)) & vbCrLf _
        & "Enabled = " & CStr(RegionEnabled(RegionNumber)) & vbCrLf _
        & "NonPhysicalPrimMax = " & CStr(NonPhysicalPrimMax(RegionNumber)) & vbCrLf _
        & "PhysicalPrimMax = " & CStr(PhysicalPrimMax(RegionNumber)) & vbCrLf _
        & "ClampPrimSize = " & CStr(ClampPrimSize(RegionNumber)) & vbCrLf _
        & "MaxPrims = " & MaxPrims(RegionNumber) & vbCrLf _
        & "RegionType = Estate" & vbCrLf _
        & "MaxAgents = 100" & vbCrLf & vbCrLf _
        & ";# Dreamgrid extended properties" & vbCrLf _
        & "RegionSnapShot = " & RegionSnapShot(RegionNumber) & vbCrLf _
        & "MapType = " & MapType(RegionNumber) & vbCrLf _
        & "Physics = " & Physics(RegionNumber) & vbCrLf _
        & "AllowGods = " & AllowGods(RegionNumber) & vbCrLf _
        & "RegionGod = " & RegionGod(RegionNumber) & vbCrLf _
        & "ManagerGod = " & ManagerGod(RegionNumber) & vbCrLf _
        & "Birds = " & Birds(RegionNumber) & vbCrLf _
        & "Tides = " & Tides(RegionNumber) & vbCrLf _
        & "Teleport = " & Teleport(RegionNumber) & vbCrLf _
        & "DisableGloebits = " & DisableGloebits(RegionNumber) & vbCrLf _
        & "DisallowForeigners = " & DisallowForeigners(RegionNumber) & vbCrLf _
        & "DisallowResidents = " & DisallowResidents(RegionNumber) & vbCrLf _
        & "MinTimerInterval =" & vbCrLf _
        & "Frametime =" & vbCrLf _
        & "SmartStart =" & SmartStart(RegionNumber) & vbCrLf

        FileStuff.DeleteFile(fname)

        Try
            Using outputFile As New StreamWriter(fname, True)
                outputFile.WriteLine(proto)
            End Using
        Catch ex As IOException
        Catch ex As UnauthorizedAccessException
        Catch ex As ArgumentException
        Catch ex As System.Security.SecurityException
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

#Region "Private Classes"

    ' hold a copy of the Main region data on a per-form basis
    Private Class Region_data

#Region "Public Fields"

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
        Public _ProcessID As Integer = 0
        Public _RegionEnabled As Boolean = True
        Public _RegionName As String = ""
        Public _RegionPath As String = ""  ' The full path to the region ini file
        Public _RegionPort As Integer = 0
        Public _SizeX As Integer = 256
        Public _SizeY As Integer = 256
        Public _Status As Integer = 0
        Public _Timer As Integer = 0
        Public _UUID As String = ""

#End Region

#End Region

#Region "OptionalStorage"

        Public _AllowGods As String = ""

        Public _Birds As String = ""

        Public _DisableGloebits As String = ""

        Public _FrameTime As String = ""

        Public _ManagerGod As String = ""

        Public _MapType As String = ""

        Public _MaxAgents As String = ""

        Public _MaxPrims As String = ""

        Public _MinTimerInterval As String = ""

        Public _NonPhysicalPrimMax As String = ""

        Public _PhysicalPrimMax As String = ""

        Public _Physics As String = "  "

        Public _RegionGod As String = ""

        ''' <summary> <Must be all strings as a blank means use the default </summary>
        Public _RegionSmartStart As String = ""

        Public _RegionSnapShot As String = ""
        Public _SkipAutobackup As String = ""
        Public _Snapshot As String = ""
        Public _Teleport As String = ""
        Public _Tides As String = ""

#End Region

    End Class

#End Region

#Region "Standard INI"

    Public Property AvatarCount(RegionNumber As Integer) As Integer
        Get
            If Bad(RegionNumber) Then Return 0
            Return RegionList(RegionNumber)._AvatarCount
        End Get
        Set(ByVal Value As Integer)
            RegionList(RegionNumber)._AvatarCount = Value
        End Set
    End Property

    Public Property ClampPrimSize(RegionNumber As Integer) As Boolean
        Get
            If Bad(RegionNumber) Then Return False
            Return RegionList(RegionNumber)._ClampPrimSize
        End Get
        Set(ByVal Value As Boolean)
            RegionList(RegionNumber)._ClampPrimSize = Value
        End Set
    End Property

    Public Property CoordX(RegionNumber As Integer) As Integer
        Get
            If Bad(RegionNumber) Then Return 0
            Return RegionList(RegionNumber)._CoordX
        End Get
        Set(ByVal Value As Integer)
            RegionList(RegionNumber)._CoordX = Value
        End Set
    End Property

    Public Property CoordY(RegionNumber As Integer) As Integer
        Get
            If Bad(RegionNumber) Then Return 0
            Return RegionList(RegionNumber)._CoordY
        End Get
        Set(ByVal Value As Integer)
            RegionList(RegionNumber)._CoordY = Value
        End Set
    End Property

    Public Property LineCounter(RegionNumber As Integer) As Integer
        Get
            If Bad(RegionNumber) Then Return 0
            Return RegionList(RegionNumber)._LineCounter
        End Get
        Set(ByVal Value As Integer)
            RegionList(RegionNumber)._LineCounter = Value
        End Set
    End Property

    Public Property MaxAgents(RegionNumber As Integer) As String
        Get
            If Bad(RegionNumber) Then Return "100"
            Return RegionList(RegionNumber)._MaxAgents
        End Get
        Set(ByVal Value As String)
            RegionList(RegionNumber)._MaxAgents = Value
        End Set
    End Property

    Public Property MaxPrims(RegionNumber As Integer) As String
        Get
            If Bad(RegionNumber) Then Return "45000"
            Return RegionList(RegionNumber)._MaxPrims
        End Get
        Set(ByVal Value As String)
            RegionList(RegionNumber)._MaxPrims = Value
        End Set
    End Property

    Public Property NonPhysicalPrimMax(RegionNumber As Integer) As String
        Get
            If Bad(RegionNumber) Then Return "1024"
            Return RegionList(RegionNumber)._NonPhysicalPrimMax
        End Get
        Set(ByVal Value As String)
            RegionList(RegionNumber)._NonPhysicalPrimMax = Value
        End Set
    End Property

    Public Property PhysicalPrimMax(RegionNumber As Integer) As String
        Get
            If Bad(RegionNumber) Then Return "1024"
            Return RegionList(RegionNumber)._PhysicalPrimMax
        End Get
        Set(ByVal Value As String)
            RegionList(RegionNumber)._PhysicalPrimMax = Value
        End Set
    End Property

    Public Property ProcessID(RegionNumber As Integer) As Integer
        Get
            If Bad(RegionNumber) Then Return 0
            Return RegionList(RegionNumber)._ProcessID
        End Get
        Set(ByVal Value As Integer)
            RegionList(RegionNumber)._ProcessID = Value
        End Set
    End Property

    Public Property SizeX(RegionNumber As Integer) As Integer
        Get
            If Bad(RegionNumber) Then Return 256
            Return RegionList(RegionNumber)._SizeX
        End Get
        Set(ByVal Value As Integer)
            RegionList(RegionNumber)._SizeX = Value
        End Set
    End Property

    Public Property SizeY(RegionNumber As Integer) As Integer
        Get
            If Bad(RegionNumber) Then Return 256
            Return RegionList(RegionNumber)._SizeY
        End Get
        Set(ByVal Value As Integer)
            RegionList(RegionNumber)._SizeY = Value
        End Set
    End Property

    Public Property Status(RegionNumber As Integer) As Integer
        Get
            If Bad(RegionNumber) Then Return 0
            Return RegionList(RegionNumber)._Status
        End Get
        Set(ByVal Value As Integer)
            RegionList(RegionNumber)._Status = Value
        End Set
    End Property

    Public Property Timer(RegionNumber As Integer) As Integer
        Get
            If Bad(RegionNumber) Then Return 0
            Return RegionList(RegionNumber)._Timer
        End Get
        Set(ByVal Value As Integer)
            RegionList(RegionNumber)._Timer = Value
        End Set
    End Property

    Public Property UUID(RegionNumber As Integer) As String
        Get
            If Bad(RegionNumber) Then Return ""
            Return RegionList(RegionNumber)._UUID
        End Get
        Set(ByVal Value As String)
            RegionList(RegionNumber)._UUID = Value
        End Set
    End Property

    Private Function Bad(RegionNumber As Integer) As Boolean

        If RegionNumber < 0 Then
            Form1.ErrorLog("Region Number  < 0".ToString(Globalization.CultureInfo.InvariantCulture))
            Return True
        End If
        If RegionNumber + 1 > RegionList.Count Then
            Form1.ErrorLog("Region n is too large:" & RegionNumber.ToString(Globalization.CultureInfo.InvariantCulture))
            Return True
        End If
        Return False

    End Function

#End Region

#Region "Properties"

    Public ReadOnly Property RegionCount() As Integer
        Get
            Return RegionList.Count
        End Get
    End Property

    Public Property FolderPath(RegionNumber As Integer) As String
        Get
            If Bad(RegionNumber) Then Return ""
            Return RegionList(RegionNumber)._FolderPath
        End Get
        Set(ByVal Value As String)
            RegionList(RegionNumber)._FolderPath = Value
        End Set
    End Property

    Public Property GroupName(RegionNumber As Integer) As String
        Get
            If Bad(RegionNumber) Then Return ""
            Return RegionList(RegionNumber)._Group
        End Get
        Set(ByVal Value As String)
            RegionList(RegionNumber)._Group = Value
        End Set
    End Property

    Public Property IniPath(RegionNumber As Integer) As String
        Get
            If Bad(RegionNumber) Then Return ""
            Return RegionList(RegionNumber)._IniPath
        End Get
        Set(ByVal Value As String)
            RegionList(RegionNumber)._IniPath = Value
        End Set
    End Property

    Public Property RegionName(RegionNumber As Integer) As String
        Get
            If Bad(RegionNumber) Then Return ""
            Return RegionList(RegionNumber)._RegionName
        End Get
        Set(ByVal Value As String)
            RegionList(RegionNumber)._RegionName = Value
        End Set
    End Property

    Public Property RegionPath(RegionNumber As Integer) As String
        Get
            If Bad(RegionNumber) Then Return ""
            Return RegionList(RegionNumber)._RegionPath
        End Get
        Set(ByVal Value As String)
            RegionList(RegionNumber)._RegionPath = Value
        End Set
    End Property

    Public Property RegionPort(RegionNumber As Integer) As Integer
        Get
            If Bad(RegionNumber) Then Return 0
            Return RegionList(RegionNumber)._RegionPort
        End Get
        Set(ByVal Value As Integer)
            RegionList(RegionNumber)._RegionPort = Value
        End Set
    End Property

#End Region

#Region "Options"

    Public Property AllowGods(RegionNumber As Integer) As String
        Get
            If Bad(RegionNumber) Then Return ""
            Return RegionList(RegionNumber)._AllowGods
        End Get
        Set(ByVal Value As String)
            RegionList(RegionNumber)._AllowGods = Value
        End Set
    End Property

    Public Property Birds(RegionNumber As Integer) As String
        Get
            If Bad(RegionNumber) Then Return ""
            Return RegionList(RegionNumber)._Birds
        End Get
        Set(ByVal Value As String)
            RegionList(RegionNumber)._Birds = Value
        End Set
    End Property

    Public Property DisableGloebits(RegionNumber As Integer) As String
        Get
            If Bad(RegionNumber) Then Return ""
            Return RegionList(RegionNumber)._DisableGloebits
        End Get
        Set(ByVal Value As String)
            RegionList(RegionNumber)._DisableGloebits = Value
        End Set
    End Property

    Public Property DisallowForeigners(RegionNumber As Integer) As String
        Get
            If Bad(RegionNumber) Then Return ""
            Return RegionList(RegionNumber)._DisallowForeigners
        End Get
        Set(ByVal Value As String)
            RegionList(RegionNumber)._DisallowForeigners = Value
        End Set
    End Property

    Public Property DisallowResidents(RegionNumber As Integer) As String
        Get
            If Bad(RegionNumber) Then Return ""
            Return RegionList(RegionNumber)._DisallowResidents
        End Get
        Set(ByVal Value As String)
            RegionList(RegionNumber)._DisallowResidents = Value
        End Set
    End Property

    Public Property FrameTime(RegionNumber As Integer) As String
        Get
            If Bad(RegionNumber) Then Return ""
            Return RegionList(RegionNumber)._FrameTime
        End Get
        Set(ByVal Value As String)
            If Value Is Nothing Then Return
            Value = Value.Replace(",", ".")
            RegionList(RegionNumber)._FrameTime = Value
        End Set
    End Property

    Public Property ManagerGod(RegionNumber As Integer) As String
        Get
            If Bad(RegionNumber) Then Return ""
            Return RegionList(RegionNumber)._ManagerGod
        End Get
        Set(ByVal Value As String)
            RegionList(RegionNumber)._ManagerGod = Value
        End Set
    End Property

    Public Property MapType(RegionNumber As Integer) As String
        Get
            If Bad(RegionNumber) Then Return ""
            Return RegionList(RegionNumber)._MapType
        End Get
        Set(ByVal Value As String)
            RegionList(RegionNumber)._MapType = Value
        End Set
    End Property

    Public Property MinTimerInterval(RegionNumber As Integer) As String
        Get
            If Bad(RegionNumber) Then Return ""
            Return RegionList(RegionNumber)._MinTimerInterval
        End Get
        Set(ByVal Value As String)
            If Value Is Nothing Then Return
            Value = Value.Replace(",", ".")
            RegionList(RegionNumber)._MinTimerInterval = Value
        End Set
    End Property

    Public Property Physics(RegionNumber As Integer) As String
        Get
            If Bad(RegionNumber) Then Return ""
            Return RegionList(RegionNumber)._Physics
        End Get
        Set(ByVal Value As String)
            RegionList(RegionNumber)._Physics = Value
        End Set
    End Property

    Public Property RegionEnabled(RegionNumber As Integer) As Boolean
        Get
            If Bad(RegionNumber) Then Return False
            Return RegionList(RegionNumber)._RegionEnabled
        End Get
        Set(ByVal Value As Boolean)
            RegionList(RegionNumber)._RegionEnabled = Value
        End Set
    End Property

    Public Property RegionGod(RegionNumber As Integer) As String
        Get
            If Bad(RegionNumber) Then Return ""
            Return RegionList(RegionNumber)._RegionGod
        End Get
        Set(ByVal Value As String)
            RegionList(RegionNumber)._RegionGod = Value
        End Set
    End Property

    Public Property RegionSnapShot(RegionNumber As Integer) As String
        Get
            If Bad(RegionNumber) Then Return ""
            Return RegionList(RegionNumber)._RegionSnapShot
        End Get
        Set(ByVal Value As String)
            RegionList(RegionNumber)._RegionSnapShot = Value
        End Set
    End Property

    Public Property SkipAutobackup(RegionNumber As Integer) As String
        Get
            If Bad(RegionNumber) Then Return ""
            Return RegionList(RegionNumber)._SkipAutobackup
        End Get
        Set(ByVal Value As String)
            RegionList(RegionNumber)._SkipAutobackup = Value
        End Set
    End Property

    Public Property SmartStart(RegionNumber As Integer) As String

        Get
            If Bad(RegionNumber) Then Return ""
            Return RegionList(RegionNumber)._RegionSmartStart
        End Get
        Set(ByVal Value As String)
            RegionList(RegionNumber)._RegionSmartStart = Value
        End Set

    End Property

    Public Property Snapshot(RegionNumber As Integer) As String
        Get
            If Bad(RegionNumber) Then Return ""
            Return RegionList(RegionNumber)._Snapshot
        End Get
        Set(ByVal Value As String)
            RegionList(RegionNumber)._Snapshot = Value
        End Set
    End Property

    Public Property Teleport(RegionNumber As Integer) As String
        Get
            If Bad(RegionNumber) Then Return ""
            Return RegionList(RegionNumber)._Teleport
        End Get
        Set(ByVal Value As String)
            RegionList(RegionNumber)._Teleport = Value
        End Set
    End Property

    Public Property Tides(RegionNumber As Integer) As String
        Get
            If Bad(RegionNumber) Then Return ""
            Return RegionList(RegionNumber)._Tides
        End Get
        Set(ByVal Value As String)
            RegionList(RegionNumber)._Tides = Value
        End Set
    End Property

#End Region

#Region "Functions"

    Public Sub DebugGroup()
        For Each pair In _Grouplist
            Debug.Print("Group name: {0}, httpport: {1}", pair.Key, pair.Value)
        Next
    End Sub

    Public Sub DebugRegions(RegionNumber As Integer)

        Form1.Log("RegionNumber", CStr(RegionNumber) & vbCrLf &
            " PID:" & RegionList(RegionNumber)._ProcessID & vbCrLf &
            " Group:" & RegionList(RegionNumber)._Group & vbCrLf &
            " Region:" & RegionList(RegionNumber)._RegionName & vbCrLf &
            " Status=" & CStr(RegionList(RegionNumber)._Status) & vbCrLf &
            " LineCtr=" & CStr(RegionList(RegionNumber)._LineCounter) & vbCrLf &
           " RegionEnabled=" & RegionList(RegionNumber)._RegionEnabled & vbCrLf &
           " Timer=" & CStr(RegionList(RegionNumber)._Timer))

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
        If Bad(RegionNumber) Then Return False
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
                'Debug.Print("Current Region is " + obj._RegionName)
                Return i
            End If
            i += 1
        Next
        Return -1

    End Function

#End Region

#Region "POST"

    Shared Function CheckPassword(POST As String, Machine As String) As Boolean

        If Machine Is Nothing Then Return False

        ' Returns true is password is blank or matching
        Dim pattern1 As Regex = New Regex("PW=(.*?)&")
        Dim match1 As Match = pattern1.Match(POST)
        If match1.Success Then
            Dim p1 As String = match1.Groups(1).Value
            If p1.Length = 0 Then Return True
            If Machine.ToUpper(Globalization.CultureInfo.InvariantCulture) = p1.ToUpper(Globalization.CultureInfo.InvariantCulture) Then Return True
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
            ' Smart Start AutoStart Region mode
            Debug.Print("Smart Start:" + POST)

            ' Auto Start Telport, AKA Smart Start
            Dim RegionUUID As String = ""
            Dim pattern As Regex = New Regex("ALT=(.*?)/AGENT=(.*)")
            Dim match As Match = pattern.Match(POST)
            If match.Success Then
                RegionUUID = match.Groups(1).Value
                Dim AgentUUID = match.Groups(2).Value
                Dim RegionNumber = FindRegionByUUID(RegionUUID)
                If RegionNumber > -1 And RegionEnabled(RegionNumber) And SmartStart(RegionNumber) Then
                    If Status(RegionNumber) = SIMSTATUSENUM.Booted Then
                        Form1.Print(My.Resources.Someone_is_in_word & " " & RegionName(RegionNumber))
                        Debug.Print("Sending to " & RegionUUID)
                        Return RegionUUID
                    Else
                        Form1.Print(My.Resources.Smart_Start_word & " " & RegionName(RegionNumber))
                        Status(RegionNumber) = SIMSTATUSENUM.Resume
                        Try
                            TeleportAvatarDict.Remove(RegionName(RegionNumber))
                        Catch ex As ArgumentNullException
                        End Try

                        TeleportAvatarDict.Add(AgentUUID, RegionName(RegionNumber))

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
            '"POST /TOS HTTP/1.1" & vbCrLf & "Host: mach.outworldz.net:9201" & vbCrLf & "Connection: keep-alive" & vbCrLf & "Content-Length: 102" & vbCrLf & "Cache-Control: max-age=0" & vbCrLf & "Upgrade-Insecure-Requests: 1" & vbCrLf & "User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.99 Safari/537.36" & vbCrLf & "Origin: http://mach.outworldz.net:9201" & vbCrLf & "Content-Type: application/x-www-form-urlencoded" & vbCrLf & "DNT: 1" & vbCrLf & "Accept: text/html,application/xhtml+xml,application/xml;q=0.0909,image/webp,image/apng,*/*;q=0.8" & vbCrLf & "Referer: http://mach.outworldz.net:9200/wifi/termsofservice.html?uid=acb8fd92-c725-423f-b750-5fd971d73182&sid=40c5b80a-5377-4b97-820c-a0952782a701" & vbCrLf & "Accept-Encoding: gzip, deflate" & vbCrLf & "Accept-Language: en-US,en;q=0.0909" & vbCrLf & vbCrLf &
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
#Disable Warning CA1031 ' Do not catch general exception types
            Catch ex As Exception
#Enable Warning CA1031 ' Do not catch general exception types
                Return "<html><head></head><body>Error</html>"
            End Try

        ElseIf POST.Contains("get_partner") Then
            Debug.Print("get Partner")
            Dim PWok As Boolean = CheckPassword(POST, Settings.MachineID())
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
#Disable Warning CA1031 ' Do not catch general exception types
                    Catch ex As Exception
#Enable Warning CA1031 ' Do not catch general exception types
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
