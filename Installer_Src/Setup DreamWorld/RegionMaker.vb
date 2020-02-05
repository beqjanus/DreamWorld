#Region "Copyright"

' Copyright 2014 Fred Beckhusen for outworldz.com https://opensource.org/licenses/AGPL

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
    Private _RegionListIsInititalized As Boolean = False
    Dim Backup As New ArrayList()
    Dim json As New JSONresult
    Private RegionList As New Dictionary(Of String, Region_data)
    Dim TeleportAvatarDict As New Dictionary(Of String, String)
    Dim WebserverList As New List(Of String)

    Public Enum REGIONTIMER As Integer
        Paused = -2
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
        RestartStage2 = 11
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

#Region "Start/Stop"

    Public Sub ClearStack()

        WebserverList.Clear()

    End Sub
    Private Sub New()

        GetAllRegions()
        If RegionCount() = 0 Then
            CreateRegion("Welcome")
            Form1.Settings.WelcomeRegion = "Welcome"
            WriteRegionObject("Welcome")
            Form1.Settings.WelcomeRegion = "Welcome"
            Form1.Settings.SaveSettings()
        End If
        Debug.Print("Loaded " + CStr(RegionCount) + " Regions")

    End Sub

#End Region

#Region "Subs"

    ''' <summary>Self setting Region Ports Iterate over all regions and set the ports from the starting value</summary>
    Public Shared Sub UpdateAllRegionPorts()

        Form1.Print(My.Resources.Updating_Ports_word)

        Dim Portnumber As Integer = CInt(Form1.Settings.FirstRegionPort())
        For Each RegionUUID As String In Form1.PropRegionClass.RegionUUIDs
            Dim RegionName = Form1.PropRegionClass.RegionName(RegionUUID)
            Form1.Settings.LoadIni(Form1.PropRegionClass.RegionPath(RegionUUID), ";")

            Form1.Settings.SetIni(RegionName, "InternalPort", CStr(Portnumber))
            Form1.PropRegionClass.RegionPort(RegionUUID) = Portnumber
            ' Self setting Region Ports
            Form1.PropMaxPortUsed = Portnumber
            Form1.Settings.SaveINI(System.Text.Encoding.UTF8)
            Portnumber += 1

        Next

        Form1.Print(My.Resources.Setup_Firewall_word)
        Firewall.SetFirewall()   ' must be after UpdateAllRegionPorts

    End Sub

    Public Sub CheckPost()

        ' Delete off end of list so we don't skip over one
        If WebserverList.Count = 0 Then Return

        WebserverList.Reverse()

        While WebserverList.Count > 0

            Try
                Dim ProcessString As String = WebserverList(0) ' recover the PID as string

                ' This search returns the substring between two strings, so the first index Is moved to the character just after the first string.
                Dim POST As String = Uri.UnescapeDataString(ProcessString)
                Dim first As Integer = POST.IndexOf("{", StringComparison.InvariantCulture)
                Dim last As Integer = POST.LastIndexOf("}", StringComparison.InvariantCulture)
                Dim rawJSON = POST.Substring(first, last - first + 1)
                WebserverList.RemoveAt(0)

                Try
                    json = JsonConvert.DeserializeObject(Of JSONresult)(rawJSON)
#Disable Warning CA1031 ' Do not catch general exception types
                Catch ex As Exception
#Enable Warning CA1031 ' Do not catch general exception types
                    Debug.Print(ex.Message)
                    Continue While
                End Try

                ' rawJSON "{""alert"":""region_ready"",""login"":""disabled"",""region_name"":""Welcome"",""region_id"":""365d804a-0df1-46cf-8acf-4320a3df3fca""}" String rawJSON
                ' "{""alert"":""region_ready"",""login"":""enabled"",""region_name"":""Welcome"",""region_id"":""365d804a-0df1-46cf-8acf-4320a3df3fca""}" String rawJSON
                ' "{""alert"":""region_ready"",""login"":""shutdown"",""region_name"":""Welcome"",""region_id"":""365d804a-0df1-46cf-8acf-4320a3df3fca""}" String

                If json.login = "enabled" Then
                    Form1.Print(json.region_name & " " & My.Resources.Ready)
                    Form1.Logger("Ready", json.region_name, "Restart")
                    Dim RegionUUID As String = FindRegionByName(json.region_name)
                    If RegionUUID.Length = 0 Then
                        Continue While
                    End If

                    RegionEnabled(RegionUUID) = True
                    Status(RegionUUID) = SIMSTATUSENUM.Booted
                    Timer(RegionUUID) = RegionMaker.REGIONTIMER.StartCounting
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

                                    Dim Welcome = Form1.Settings.WelcomeRegion
                                    Dim UUID = FindRegionByName(Welcome)
                                    Form1.ConsoleCommand(UUID, "change region " & json.region_name & "{ENTER}")
                                    Form1.ConsoleCommand(UUID, "teleport user " & AgentName & " " & json.region_name & "{ENTER}")
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

                    If Form1.Settings.ConsoleShow = "False" Or Form1.Settings.ConsoleShow = "" Then
                        Dim hwnd = Form1.GetHwnd(GroupName(RegionUUID))
                        Form1.ShowDOSWindow(hwnd, Form1.SHOWWINDOWENUM.SWMINIMIZE)
                    End If

                ElseIf json.login = "shutdown" Then

                    'Return '!!!  does not work as expected at the moment fkb

                    Form1.Print(json.region_name & " " & My.Resources.Stopped_word)

                    Dim RegionUUID As String = FindRegionByName(json.region_name)
                    If RegionUUID.Length = 0 Then
                        Return
                    End If
                    Form1.PropExitList.Add(json.region_name, "RegionReady: shutdown")
                    Form1.Logger("RegionReady", json.region_name & " shutdown", "Restart")
                End If
#Disable Warning CA1031 ' Do not catch general exception types
            Catch ex As Exception
#Enable Warning CA1031 ' Do not catch general exception types
                Debug.Print(ex.Message)
            End Try
        End While

    End Sub

    Public Function CreateRegion(name As String, Optional UUID As String = "") As String

        If UUID Is Nothing Then UUID = Guid.NewGuid().ToString
        If UUID = "" Then UUID = Guid.NewGuid().ToString

        Debug.Print("Create Region " + name)
        Dim r As New Region_data With {
            ._RegionName = name,
            ._RegionEnabled = True,
            ._UUID = UUID,
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
            ._MinTimerInterval = 0.2.ToString(Globalization.CultureInfo.InvariantCulture),
            ._GodDefault = "True",
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
            ._ScriptEngine = "",
            ._RegionSmartStart = ""
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
        Try
            Backup.Clear()
            Dim pair As KeyValuePair(Of String, Region_data)

            For Each pair In RegionList
                Backup.Add(pair.Value)
            Next

            RegionList.Clear()

            Dim folders() As String
            Dim regionfolders() As String
            Dim RegionUUID As String = ""
            folders = Directory.GetDirectories(Form1.PropOpensimBinPath + "bin\Regions")
            For Each FolderName As String In folders

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

                            Form1.Settings.LoadIni(ini, ";")

                            RegionUUID = Form1.Settings.GetIni(fName, "RegionUUID", "", "String")
                            Dim SomeUUID As New Guid
                            If Not Guid.TryParse(RegionUUID, SomeUUID) Then
                                MsgBox("Cannot read RegionUUID in INI file for  " & fName)
                            End If

                            CreateRegion(fName, RegionUUID)

                            ' we do not save the above as we are making a new one.
                            RegionEnabled(RegionUUID) = Form1.Settings.GetIni(fName, "Enabled", "True", "Boolean")

                            RegionPath(RegionUUID) = ini ' save the path
                            FolderPath(RegionUUID) = System.IO.Path.GetDirectoryName(ini)

                            Dim theEnd As Integer = FolderPath(RegionUUID).LastIndexOf("\", StringComparison.InvariantCulture)
                            IniPath(RegionUUID) = FolderPath(RegionUUID).Substring(0, theEnd + 1)

                            ' need folder name in case there are more than 1 ini
                            Dim theStart = FolderPath(RegionUUID).IndexOf("Regions\", StringComparison.InvariantCulture) + 8
                            theEnd = FolderPath(RegionUUID).LastIndexOf("\", StringComparison.InvariantCulture)
                            Dim gname = FolderPath(RegionUUID).Substring(theStart, theEnd - theStart)

                            GroupName(RegionUUID) = gname

                            UUID(RegionUUID) = Form1.Settings.GetIni(fName, "RegionUUID", "", "String")
                            SizeX(RegionUUID) = Form1.Settings.GetIni(fName, "SizeX", "256", "Integer")
                            SizeY(RegionUUID) = Form1.Settings.GetIni(fName, "SizeY", "256", "Integer")
                            RegionPort(RegionUUID) = Form1.Settings.GetIni(fName, "InternalPort", "0", "Integer")

                            ' extended props V2.1
                            NonPhysicalPrimMax(RegionUUID) = Form1.Settings.GetIni(fName, "NonPhysicalPrimMax", "1024", "Integer")
                            PhysicalPrimMax(RegionUUID) = Form1.Settings.GetIni(fName, "PhysicalPrimMax", "64", "Integer")
                            ClampPrimSize(RegionUUID) = Form1.Settings.GetIni(fName, "ClampPrimSize", "False", "Boolean")
                            MaxPrims(RegionUUID) = Form1.Settings.GetIni(fName, "MaxPrims", "15000", "Integer")
                            MaxAgents(RegionUUID) = Form1.Settings.GetIni(fName, "MaxAgents", "100", "Integer")

                            ' Location is int,int format.
                            Dim C = Form1.Settings.GetIni(fName, "Location", RandomNumber.Between(2000, 1000) & "," & RandomNumber.Between(2000, 1000))

                            Dim parts As String() = C.Split(New Char() {","c}) ' split at the comma
                            CoordX(RegionUUID) = CInt(parts(0))
                            CoordY(RegionUUID) = CInt(parts(1))

                            ' options parameters coming from INI file can be blank!
                            MinTimerInterval(RegionUUID) = Form1.Settings.GetIni(fName, "MinTimerInterval", "", "String")
                            FrameTime(RegionUUID) = Form1.Settings.GetIni(fName, "FrameTime", "", "String")
                            RegionSnapShot(RegionUUID) = Form1.Settings.GetIni(fName, "RegionSnapShot", "", "String")
                            MapType(RegionUUID) = Form1.Settings.GetIni(fName, "MapType", "", "String")
                            Physics(RegionUUID) = Form1.Settings.GetIni(fName, "Physics", "", "String")
                            MaxPrims(RegionUUID) = Form1.Settings.GetIni(fName, "MaxPrims", "", "String")
                            GodDefault(RegionUUID) = Form1.Settings.GetIni(fName, "GodDefault", "True", "String")
                            AllowGods(RegionUUID) = Form1.Settings.GetIni(fName, "AllowGods", "", "String")
                            RegionGod(RegionUUID) = Form1.Settings.GetIni(fName, "RegionGod", "", "String")
                            ManagerGod(RegionUUID) = Form1.Settings.GetIni(fName, "ManagerGod", "", "String")
                            Birds(RegionUUID) = Form1.Settings.GetIni(fName, "Birds", "", "String")
                            Tides(RegionUUID) = Form1.Settings.GetIni(fName, "Tides", "", "String")
                            Teleport(RegionUUID) = Form1.Settings.GetIni(fName, "Teleport", "", "String")
                            DisableGloebits(RegionUUID) = Form1.Settings.GetIni(fName, "DisableGloebits", "", "String")
                            DisallowForeigners(RegionUUID) = Form1.Settings.GetIni(fName, "DisallowForeigners", "", "String")
                            DisallowResidents(RegionUUID) = Form1.Settings.GetIni(fName, "DisallowResidents", "", "String")
                            SkipAutobackup(RegionUUID) = Form1.Settings.GetIni(fName, "SkipAutoBackup", "", "String")
                            Snapshot(RegionUUID) = Form1.Settings.GetIni(fName, "RegionSnapShot", "", "String")
                            ScriptEngine(RegionUUID) = Form1.Settings.GetIni(fName, "ScriptEngine", "", "String")

                            Select Case Form1.Settings.GetIni(fName, "SmartStart", "False", "String")
                                Case "True"
                                    SmartStart(RegionUUID) = True
                                Case "False"
                                    SmartStart(RegionUUID) = False
                                Case Else
                                    SmartStart(RegionUUID) = False
                            End Select

                            If _RegionListIsInititalized Then
                                ' restore backups of transient data
                                Dim o = FindBackupByName(fName)
                                If o >= 0 Then
                                    AvatarCount(RegionUUID) = Backup(o)._AvatarCount
                                    ProcessID(RegionUUID) = Backup(o)._ProcessID
                                    Status(RegionUUID) = Backup(o)._Status
                                    LineCounter(RegionUUID) = Backup(o)._LineCounter
                                    Timer(RegionUUID) = Backup(o)._Timer
                                End If
                            End If

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

            _RegionListIsInititalized = True
#Disable Warning CA1031 ' Do not catch general exception types
        Catch ex As Exception
#Enable Warning CA1031 ' Do not catch general exception types
            Debug.Print(ex.Message)
        End Try
        Return RegionList.Count

    End Function

    Public Function LargestPort() As Integer

        ' locate largest port
        Dim MaxNum As Integer = 0
        Dim Portlist As New Dictionary(Of String, Integer)
        Dim pair As KeyValuePair(Of String, Region_data)

        For Each pair In RegionList
            Try
                Portlist.Add(pair.Key, pair.Value._RegionPort)
            Catch ex As ArgumentNullException
                Debug.Print(ex.Message)
            Catch ex As ArgumentException
                Debug.Print(ex.Message)
            End Try
        Next

        If Portlist.Count = 0 Then
            Return 0
        End If

        For Each thing As KeyValuePair(Of String, Integer) In Portlist
            If thing.Value > MaxNum Then
                MaxNum = thing.Value ' max is always the current value
            End If

            If Not Portlist.ContainsKey(MaxNum + 1) Then
                Return MaxNum  ' Found a blank spot at Max + 1 so return Max
            End If
        Next

        Return MaxNum

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
            Max = RandomNumber.Between(5000, 996)
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
            Max = RandomNumber.Between(5000, 1000)
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

        Dim RegionUUID As String = FindRegionByName(name)
        If RegionUUID.Length = 0 Then
            MsgBox(My.Resources.Cannot_find_region_word & " " & name, vbInformation, My.Resources.Error_word)
            Return
        End If

        Dim fname As String = RegionList(RegionUUID)._FolderPath

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
        & "RegionUUID = " & UUID(RegionUUID) & vbCrLf _
        & "Location = " & CoordX(RegionUUID).ToString(Globalization.CultureInfo.InvariantCulture) & "," & CoordY(RegionUUID).ToString(Globalization.CultureInfo.InvariantCulture) & vbCrLf _
        & "InternalAddress = 0.0.0.0" & vbCrLf _
        & "InternalPort = " & RegionPort(RegionUUID) & vbCrLf _
        & "AllowAlternatePorts = False" & vbCrLf _
        & "ExternalHostName = " & Form1.ExternLocalServerName() & vbCrLf _
        & "SizeX = " & CStr(SizeX(RegionUUID)) & vbCrLf _
        & "SizeY = " & CStr(SizeY(RegionUUID)) & vbCrLf _
        & "Enabled = " & CStr(RegionEnabled(RegionUUID)) & vbCrLf _
        & "NonPhysicalPrimMax = " & CStr(NonPhysicalPrimMax(RegionUUID)) & vbCrLf _
        & "PhysicalPrimMax = " & CStr(PhysicalPrimMax(RegionUUID)) & vbCrLf _
        & "ClampPrimSize = " & CStr(ClampPrimSize(RegionUUID)) & vbCrLf _
        & "MaxPrims = " & MaxPrims(RegionUUID) & vbCrLf _
        & "RegionType = Estate" & vbCrLf _
        & "MaxAgents = 100" & vbCrLf & vbCrLf _
        & ";# Dreamgrid extended properties" & vbCrLf _
        & "RegionSnapShot = " & RegionSnapShot(RegionUUID) & vbCrLf _
        & "MapType = " & MapType(RegionUUID) & vbCrLf _
        & "Physics = " & Physics(RegionUUID) & vbCrLf _
        & "GodDefault = " & GodDefault(RegionUUID) & vbCrLf _
        & "AllowGods = " & AllowGods(RegionUUID) & vbCrLf _
        & "RegionGod = " & RegionGod(RegionUUID) & vbCrLf _
        & "ManagerGod = " & ManagerGod(RegionUUID) & vbCrLf _
        & "Birds = " & Birds(RegionUUID) & vbCrLf _
        & "Tides = " & Tides(RegionUUID) & vbCrLf _
        & "Teleport = " & Teleport(RegionUUID) & vbCrLf _
        & "DisableGloebits = " & DisableGloebits(RegionUUID) & vbCrLf _
        & "DisallowForeigners = " & DisallowForeigners(RegionUUID) & vbCrLf _
        & "DisallowResidents = " & DisallowResidents(RegionUUID) & vbCrLf _
        & "MinTimerInterval =" & MinTimerInterval(RegionUUID) & vbCrLf _
        & "Frametime =" & FrameTime(RegionUUID) & vbCrLf _
        & "ScriptEngine =" & ScriptEngine(RegionUUID) & vbCrLf _
        & "SmartStart =" & SmartStart(RegionUUID) & vbCrLf

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

#Region "Region_data"

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
        Public _GodDefault As String = "True"
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

    Public Property AvatarCount(RegionUUID As String) As Integer
        Get
            If RegionUUID Is Nothing Then Return 0
            If Bad(RegionUUID) Then Return 0
            Return RegionList(RegionUUID)._AvatarCount
        End Get
        Set(ByVal Value As Integer)
            If RegionUUID Is Nothing Then Return
            If Bad(RegionUUID) Then Return
            RegionList(RegionUUID)._AvatarCount = Value
        End Set
    End Property

    Public Property ClampPrimSize(RegionUUID As String) As Boolean
        Get
            If RegionUUID Is Nothing Then Return False
            If Bad(RegionUUID) Then Return False
            Return RegionList(RegionUUID)._ClampPrimSize
        End Get
        Set(ByVal Value As Boolean)
            If RegionUUID Is Nothing Then Return
            If Bad(RegionUUID) Then Return
            RegionList(RegionUUID)._ClampPrimSize = Value
        End Set
    End Property

    Public Property CoordX(RegionUUID As String) As Integer
        Get
            If RegionUUID Is Nothing Then Return 0
            If Bad(RegionUUID) Then Return 0
            Return RegionList(RegionUUID)._CoordX
        End Get
        Set(ByVal Value As Integer)
            If RegionUUID Is Nothing Then Return
            If Bad(RegionUUID) Then Return
            RegionList(RegionUUID)._CoordX = Value
        End Set
    End Property

    Public Property CoordY(RegionUUID As String) As Integer
        Get
            If RegionUUID Is Nothing Then Return 0
            If Bad(RegionUUID) Then Return 0
            Return RegionList(RegionUUID)._CoordY
        End Get
        Set(ByVal Value As Integer)
            If RegionUUID Is Nothing Then Return
            If Bad(RegionUUID) Then Return
            RegionList(RegionUUID)._CoordY = Value
        End Set
    End Property

    Public Property LineCounter(RegionUUID As String) As Integer
        Get
            If RegionUUID Is Nothing Then Return 0
            If Bad(RegionUUID) Then Return 0
            Return RegionList(RegionUUID)._LineCounter
        End Get
        Set(ByVal Value As Integer)
            If RegionUUID Is Nothing Then Return
            RegionList(RegionUUID)._LineCounter = Value
        End Set
    End Property

    Public Property MaxAgents(RegionUUID As String) As String
        Get
            If RegionUUID Is Nothing Then Return "100"
            If Bad(RegionUUID) Then Return "100"
            If RegionList(RegionUUID)._MaxAgents = "" Then RegionList(RegionUUID)._MaxAgents = 100

            Return RegionList(RegionUUID)._MaxAgents
        End Get
        Set(ByVal Value As String)
            If RegionUUID Is Nothing Then Return
            If Bad(RegionUUID) Then Return
            RegionList(RegionUUID)._MaxAgents = Value
        End Set
    End Property

    Public Property MaxPrims(RegionUUID As String) As String
        Get
            If RegionUUID Is Nothing Then Return "45000"
            If Bad(RegionUUID) Then Return "45000"
            If RegionList(RegionUUID)._MaxPrims = "" Then RegionList(RegionUUID)._MaxPrims = 45000
            Return RegionList(RegionUUID)._MaxPrims
        End Get
        Set(ByVal Value As String)
            If RegionUUID Is Nothing Then Return
            If Bad(RegionUUID) Then Return
            RegionList(RegionUUID)._MaxPrims = Value
        End Set
    End Property

    Public Property NonPhysicalPrimMax(RegionUUID As String) As String
        Get
            If RegionUUID Is Nothing Then Return "1024"
            If Bad(RegionUUID) Then Return "1024"
            Return RegionList(RegionUUID)._NonPhysicalPrimMax
        End Get
        Set(ByVal Value As String)
            If RegionUUID Is Nothing Then Return
            If Bad(RegionUUID) Then Return
            RegionList(RegionUUID)._NonPhysicalPrimMax = Value
        End Set
    End Property

    Public Property PhysicalPrimMax(RegionUUID As String) As String
        Get
            If RegionUUID Is Nothing Then Return "1024"
            If Bad(RegionUUID) Then Return "1024"
            Return RegionList(RegionUUID)._PhysicalPrimMax
        End Get
        Set(ByVal Value As String)
            If RegionUUID Is Nothing Then Return
            If Bad(RegionUUID) Then Return
            RegionList(RegionUUID)._PhysicalPrimMax = Value
        End Set
    End Property

    Public Property ProcessID(RegionUUID As String) As Integer
        Get
            If RegionUUID Is Nothing Then Return 0
            If Bad(RegionUUID) Then Return 0
            Return RegionList(RegionUUID)._ProcessID
        End Get
        Set(ByVal Value As Integer)
            If RegionUUID Is Nothing Then Return
            If Bad(RegionUUID) Then Return
            RegionList(RegionUUID)._ProcessID = Value
        End Set
    End Property

    Public Property SizeX(RegionUUID As String) As Integer
        Get
            If RegionUUID Is Nothing Then Return 256
            If Bad(RegionUUID) Then Return 256
            Return RegionList(RegionUUID)._SizeX
        End Get
        Set(ByVal Value As Integer)
            If RegionUUID Is Nothing Then Return
            If Bad(RegionUUID) Then Return
            RegionList(RegionUUID)._SizeX = Value
        End Set
    End Property

    Public Property SizeY(RegionUUID As String) As Integer
        Get
            If RegionUUID Is Nothing Then Return 256
            If Bad(RegionUUID) Then Return 256
            Return RegionList(RegionUUID)._SizeY
        End Get
        Set(ByVal Value As Integer)
            If RegionUUID Is Nothing Then Return
            If Bad(RegionUUID) Then Return
            RegionList(RegionUUID)._SizeY = Value
        End Set
    End Property

    Public Property Status(RegionUUID As String) As Integer
        Get
            If RegionUUID Is Nothing Then Return -1
            If Bad(RegionUUID) Then Return -1
            Return RegionList(RegionUUID)._Status
        End Get
        Set(ByVal Value As Integer)
            If RegionUUID Is Nothing Then Return
            If Bad(RegionUUID) Then Return
            RegionList(RegionUUID)._Status = Value
        End Set
    End Property

    Public Property Timer(RegionUUID As String) As Integer
        Get
            If RegionUUID Is Nothing Then Return -1
            If Bad(RegionUUID) Then Return -1
            Return RegionList(RegionUUID)._Timer
        End Get
        Set(ByVal Value As Integer)
            If RegionUUID Is Nothing Then Return
            If Bad(RegionUUID) Then Return
            RegionList(RegionUUID)._Timer = Value
        End Set
    End Property

    Public Property UUID(RegionUUID As String) As String
        Get
            If RegionUUID Is Nothing Then Return ""
            If RegionUUID Is Nothing Then Return ""
            If Bad(RegionUUID) Then Return ""
            Return RegionList(RegionUUID)._UUID
        End Get
        Set(ByVal Value As String)
            If RegionUUID Is Nothing Then Return
            If Bad(RegionUUID) Then Return
            RegionList(RegionUUID)._UUID = Value
        End Set
    End Property

#End Region

#Region "Properties"

    Public Property GroupPort(RegionUUID As String) As Integer
        Get
            Dim GN As String = GroupName(RegionUUID)
            If _Grouplist.ContainsKey(GN) Then
                Return _Grouplist.Item(GN)
            End If
            Return 0
        End Get

        Set(ByVal Value As Integer)
            Dim GN As String = GroupName(RegionUUID)
            If _Grouplist.ContainsKey(GN) Then
                If Value > _Grouplist.Item(GN) Then
                    _Grouplist.Item(GN) = Value
                End If

            Else
                    _Grouplist.Add(GN, Value)
            End If

            'DebugGroup
        End Set
    End Property

    Public ReadOnly Property RegionCount() As Integer
        Get
            Return RegionList.Count
        End Get
    End Property

    Public Property FolderPath(RegionUUID As String) As String
        Get
            If RegionUUID Is Nothing Then Return ""
            If Bad(RegionUUID) Then Return ""
            Return RegionList(RegionUUID)._FolderPath
        End Get
        Set(ByVal Value As String)
            If RegionUUID Is Nothing Then Return
            If Bad(RegionUUID) Then Return
            RegionList(RegionUUID)._FolderPath = Value
        End Set
    End Property

    Public Property GroupName(RegionUUID As String) As String
        Get
            If RegionUUID Is Nothing Then Return ""
            If Bad(RegionUUID) Then Return ""
            Return RegionList(RegionUUID)._Group
        End Get
        Set(ByVal Value As String)
            If RegionUUID Is Nothing Then Return
            If Bad(RegionUUID) Then Return
            RegionList(RegionUUID)._Group = Value
        End Set
    End Property

    Public Property IniPath(RegionUUID As String) As String
        Get
            If RegionUUID Is Nothing Then Return ""
            If Bad(RegionUUID) Then Return ""
            Return RegionList(RegionUUID)._IniPath
        End Get
        Set(ByVal Value As String)
            If RegionUUID Is Nothing Then Return
            If Bad(RegionUUID) Then Return
            RegionList(RegionUUID)._IniPath = Value
        End Set
    End Property

    Public Property RegionName(RegionUUID As String) As String
        Get
            If RegionUUID Is Nothing Then Return ""
            If Bad(RegionUUID) Then Return ""
            Return RegionList(RegionUUID)._RegionName
        End Get
        Set(ByVal Value As String)
            If RegionUUID Is Nothing Then Return
            If Bad(RegionUUID) Then Return
            RegionList(RegionUUID)._RegionName = Value
        End Set
    End Property

    Public Property RegionPath(RegionUUID As String) As String
        Get
            If RegionUUID Is Nothing Then Return ""
            If Bad(RegionUUID) Then Return ""
            Return RegionList(RegionUUID)._RegionPath
        End Get
        Set(ByVal Value As String)
            If RegionUUID Is Nothing Then Return
            If Bad(RegionUUID) Then Return
            RegionList(RegionUUID)._RegionPath = Value
        End Set
    End Property

    Public Property RegionPort(RegionUUID As String) As Integer
        Get
            If RegionUUID Is Nothing Then Return -1
            If Bad(RegionUUID) Then Return -1
            Return RegionList(RegionUUID)._RegionPort
        End Get
        Set(ByVal Value As Integer)
            If RegionUUID Is Nothing Then Return
            If Bad(RegionUUID) Then Return
            RegionList(RegionUUID)._RegionPort = Value
        End Set
    End Property

#End Region

#Region "Options"

    Public Property AllowGods(RegionUUID As String) As String
        Get
            If RegionUUID Is Nothing Then Return ""
            If Bad(RegionUUID) Then Return ""
            Return RegionList(RegionUUID)._AllowGods
        End Get
        Set(ByVal Value As String)
            If RegionUUID Is Nothing Then Return
            If Bad(RegionUUID) Then Return
            RegionList(RegionUUID)._AllowGods = Value
        End Set
    End Property

    Public Property Birds(RegionUUID As String) As String
        Get
            If RegionUUID Is Nothing Then Return ""
            If Bad(RegionUUID) Then Return ""
            Return RegionList(RegionUUID)._Birds
        End Get
        Set(ByVal Value As String)
            If RegionUUID Is Nothing Then Return
            If Bad(RegionUUID) Then Return
            RegionList(RegionUUID)._Birds = Value
        End Set
    End Property

    Public Property DisableGloebits(RegionUUID As String) As String
        Get
            If RegionUUID Is Nothing Then Return ""
            If Bad(RegionUUID) Then Return ""
            Return RegionList(RegionUUID)._DisableGloebits
        End Get
        Set(ByVal Value As String)
            If RegionUUID Is Nothing Then Return
            If Bad(RegionUUID) Then Return
            RegionList(RegionUUID)._DisableGloebits = Value
        End Set
    End Property

    Public Property DisallowForeigners(RegionUUID As String) As String
        Get
            If RegionUUID Is Nothing Then Return ""
            If Bad(RegionUUID) Then Return ""
            Return RegionList(RegionUUID)._DisallowForeigners
        End Get
        Set(ByVal Value As String)
            If RegionUUID Is Nothing Then Return
            If Bad(RegionUUID) Then Return
            RegionList(RegionUUID)._DisallowForeigners = Value
        End Set
    End Property

    Public Property DisallowResidents(RegionUUID As String) As String
        Get
            If RegionUUID Is Nothing Then Return ""
            If Bad(RegionUUID) Then Return ""
            Return RegionList(RegionUUID)._DisallowResidents
        End Get
        Set(ByVal Value As String)
            If RegionUUID Is Nothing Then Return
            If Bad(RegionUUID) Then Return
            RegionList(RegionUUID)._DisallowResidents = Value
        End Set
    End Property

    Public Property FrameTime(RegionUUID As String) As String
        Get
            If RegionUUID Is Nothing Then Return ""
            If Bad(RegionUUID) Then Return ""
            Return RegionList(RegionUUID)._FrameTime
        End Get
        Set(ByVal Value As String)
            If RegionUUID Is Nothing Then Return
            If Bad(RegionUUID) Then Return
            If Value Is Nothing Then Return
            Value = Value.Replace(",", ".")
            RegionList(RegionUUID)._FrameTime = Value
        End Set
    End Property

    Public Property ManagerGod(RegionUUID As String) As String
        Get
            If RegionUUID Is Nothing Then Return ""
            If Bad(RegionUUID) Then Return ""
            Return RegionList(RegionUUID)._ManagerGod
        End Get
        Set(ByVal Value As String)
            If RegionUUID Is Nothing Then Return
            If Bad(RegionUUID) Then Return
            RegionList(RegionUUID)._ManagerGod = Value
        End Set
    End Property

    Public Property MapType(RegionUUID As String) As String
        Get
            If RegionUUID Is Nothing Then Return ""
            If Bad(RegionUUID) Then Return ""
            Return RegionList(RegionUUID)._MapType
        End Get
        Set(ByVal Value As String)
            If RegionUUID Is Nothing Then Return
            If Bad(RegionUUID) Then Return
            RegionList(RegionUUID)._MapType = Value
        End Set
    End Property

    Public Property MinTimerInterval(RegionUUID As String) As String
        Get
            If RegionUUID Is Nothing Then Return ""
            If Bad(RegionUUID) Then Return ""
            Return RegionList(RegionUUID)._MinTimerInterval
        End Get
        Set(ByVal Value As String)
            If RegionUUID Is Nothing Then Return
            If Bad(RegionUUID) Then Return
            If Value Is Nothing Then Return
            Value = Value.Replace(",", ".")
            RegionList(RegionUUID)._MinTimerInterval = Value
        End Set
    End Property

    Public Property GodDefault(RegionUUID As String) As String
        Get
            If RegionUUID Is Nothing Then Return "True"
            If Bad(RegionUUID) Then Return True
            Return RegionList(RegionUUID)._GodDefault
        End Get
        Set(ByVal Value As String)
            If RegionUUID Is Nothing Then Return
            If Bad(RegionUUID) Then Return
            RegionList(RegionUUID)._GodDefault = Value
        End Set
    End Property

    Public Property Physics(RegionUUID As String) As String
        Get
            If RegionUUID Is Nothing Then Return ""
            If Bad(RegionUUID) Then Return ""
            Return RegionList(RegionUUID)._Physics
        End Get
        Set(ByVal Value As String)
            If RegionUUID Is Nothing Then Return
            If Bad(RegionUUID) Then Return
            RegionList(RegionUUID)._Physics = Value
        End Set
    End Property

    Public Property RegionEnabled(RegionUUID As String) As Boolean
        Get
            If RegionUUID Is Nothing Then Return ""
            If Bad(RegionUUID) Then Return False
            Return RegionList(RegionUUID)._RegionEnabled
        End Get
        Set(ByVal Value As Boolean)
            If RegionUUID Is Nothing Then Return
            If Bad(RegionUUID) Then Return
            RegionList(RegionUUID)._RegionEnabled = Value
        End Set
    End Property

    Public Property RegionGod(RegionUUID As String) As String
        Get
            If RegionUUID Is Nothing Then Return ""
            If Bad(RegionUUID) Then Return ""
            Return RegionList(RegionUUID)._RegionGod
        End Get
        Set(ByVal Value As String)
            If RegionUUID Is Nothing Then Return
            If Bad(RegionUUID) Then Return
            RegionList(RegionUUID)._RegionGod = Value
        End Set
    End Property

    Public Property RegionSnapShot(RegionUUID As String) As String
        Get
            If RegionUUID Is Nothing Then Return ""
            If Bad(RegionUUID) Then Return ""
            Return RegionList(RegionUUID)._RegionSnapShot
        End Get
        Set(ByVal Value As String)
            If RegionUUID Is Nothing Then Return
            If Bad(RegionUUID) Then Return
            RegionList(RegionUUID)._RegionSnapShot = Value
        End Set
    End Property

    Public Property ScriptEngine(RegionUUID As String) As String
        Get
            If RegionUUID Is Nothing Then Return ""
            If Bad(RegionUUID) Then Return ""
            Return RegionList(RegionUUID)._ScriptEngine
        End Get
        Set(ByVal Value As String)
            If RegionUUID Is Nothing Then Return
            If Bad(RegionUUID) Then Return
            RegionList(RegionUUID)._ScriptEngine = Value
        End Set
    End Property

    Public Property SkipAutobackup(RegionUUID As String) As String
        Get
            If RegionUUID Is Nothing Then Return ""
            If Bad(RegionUUID) Then Return ""
            Return RegionList(RegionUUID)._SkipAutobackup
        End Get
        Set(ByVal Value As String)
            If RegionUUID Is Nothing Then Return
            If Bad(RegionUUID) Then Return
            RegionList(RegionUUID)._SkipAutobackup = Value
        End Set
    End Property

    Public Property SmartStart(RegionUUID As String) As String

        Get
            If RegionUUID Is Nothing Then Return ""
            If Bad(RegionUUID) Then Return ""
            Return RegionList(RegionUUID)._RegionSmartStart
        End Get
        Set(ByVal Value As String)
            If RegionUUID Is Nothing Then Return
            If Bad(RegionUUID) Then Return
            RegionList(RegionUUID)._RegionSmartStart = Value
        End Set

    End Property

    Public Property Snapshot(RegionUUID As String) As String
        Get
            If RegionUUID Is Nothing Then Return ""
            If Bad(RegionUUID) Then Return ""
            Return RegionList(RegionUUID)._Snapshot
        End Get
        Set(ByVal Value As String)
            If RegionUUID Is Nothing Then Return
            If Bad(RegionUUID) Then Return
            RegionList(RegionUUID)._Snapshot = Value
        End Set
    End Property

    Public Property Teleport(RegionUUID As String) As String
        Get
            If RegionUUID Is Nothing Then Return ""
            If Bad(RegionUUID) Then Return ""
            Return RegionList(RegionUUID)._Teleport
        End Get
        Set(ByVal Value As String)
            If RegionUUID Is Nothing Then Return
            If Bad(RegionUUID) Then Return
            RegionList(RegionUUID)._Teleport = Value
        End Set
    End Property

    Public Property Tides(RegionUUID As String) As String
        Get
            If RegionUUID Is Nothing Then Return ""
            If Bad(RegionUUID) Then Return ""
            Return RegionList(RegionUUID)._Tides
        End Get
        Set(ByVal Value As String)
            If RegionUUID Is Nothing Then Return
            If Bad(RegionUUID) Then Return
            RegionList(RegionUUID)._Tides = Value
        End Set
    End Property

#End Region

#Region "Functions"

    Public Sub DebugGroup()
        For Each pair In _Grouplist
            Debug.Print("Group name: {0}, http port: {1}", pair.Key, pair.Value)
        Next
    End Sub

    Public Sub DebugRegions(RegionUUID As String)

        Form1.Log("RegionUUID", CStr(RegionUUID) & vbCrLf &
            " PID:" & RegionList(RegionUUID)._ProcessID & vbCrLf &
            " Group:" & RegionList(RegionUUID)._Group & vbCrLf &
            " Region:" & RegionList(RegionUUID)._RegionName & vbCrLf &
            " Status=" & CStr(RegionList(RegionUUID)._Status) & vbCrLf &
            " LineCtr=" & CStr(RegionList(RegionUUID)._LineCounter) & vbCrLf &
           " RegionEnabled=" & RegionList(RegionUUID)._RegionEnabled & vbCrLf &
           " Timer=" & CStr(RegionList(RegionUUID)._Timer))

    End Sub

    Public Function FindRegionByName(Name As String) As String

        Dim pair As KeyValuePair(Of String, Region_data)
        For Each pair In RegionList
            If Name = pair.Value._RegionName Then
                Debug.Print("Current Region is " + pair.Value._RegionName)
                Return pair.Value._UUID
            End If
        Next
        'RegionDump()
        Return ""

    End Function

    Public Function IsBooted(RegionUUID As String) As Boolean
        If RegionUUID Is Nothing Then Return False
        If Bad(RegionUUID) Then Return False
        If Status(RegionUUID) = SIMSTATUSENUM.Booted Then
            Return True
        End If
        Return False

    End Function

    Public Sub RegionDump()

        If Not Form1.PropDebug Then Return
        Dim ctr = 0
        Dim pair As KeyValuePair(Of String, Region_data)
        For Each pair In RegionList
            DebugRegions(pair.Value._UUID)
        Next

    End Sub

    Public Function RegionUUIDListByName(Gname As String) As List(Of String)

        Dim L As New List(Of String)

        Dim pair As KeyValuePair(Of String, Region_data)
        For Each pair In RegionList
            If pair.Value._Group = Gname Or Gname = "*" Then
                L.Add(pair.Value._UUID)
            End If
        Next
        If (L.Count = 0) Then
            L.Add(FindRegionByName(Gname))
        End If
        Return L

    End Function

    Public Function RegionUUIDs() As List(Of String)

        Dim L As New List(Of String)
        Dim pair As KeyValuePair(Of String, Region_data)

        For Each pair In RegionList
            L.Add(pair.Value._UUID)
        Next
        Return L

    End Function

    Private Function Bad(RegionUUID As String) As Boolean

        If RegionList.ContainsKey(RegionUUID) Then
            Return False
        End If

        If RegionUUID = "" Then
            Form1.ErrorLog("Region UUID Zero".ToString(Globalization.CultureInfo.InvariantCulture))
            Return True
        End If

        Form1.ErrorLog("Region UUID does not exist. " & CStr(RegionUUID))
        Return True

    End Function

    Private Function FindRegionUUIDByName(Name As String) As String

        Dim pair As KeyValuePair(Of String, Region_data)

        For Each pair In RegionList
            If Name = pair.Value._UUID Then Return pair.Value._UUID
        Next

        Return ""

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

    'TODO: Move to Mysql
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

    'TODO: Move to Mysql
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
                If RegionUUID.Length > 0 And RegionEnabled(RegionUUID) And SmartStart(RegionUUID) Then
                    If Status(RegionUUID) = SIMSTATUSENUM.Booted Then
                        Form1.Print(My.Resources.Someone_is_in_word & " " & RegionName(RegionUUID))
                        Debug.Print("Sending to " & RegionUUID)
                        Return RegionUUID
                    Else
                        Form1.Print(My.Resources.Smart_Start_word & " " & RegionName(RegionUUID))
                        Status(RegionUUID) = SIMSTATUSENUM.Resume
                        Try
                            TeleportAvatarDict.Remove(RegionName(RegionUUID))
                        Catch ex As ArgumentNullException
                        End Try

                        TeleportAvatarDict.Add(AgentUUID, RegionName(RegionUUID))

                        ' redirect to welcome
                        Dim wname = Settings.WelcomeRegion
                        Dim WelcomeRegionUUID As String = FindRegionByName(wname)
                        Debug.Print("Sending to " & UUID(WelcomeRegionUUID))
                        Return UUID(WelcomeRegionUUID)
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

                    ' Only works in Standalone, anyway. Not implemented at all in Grid mode - the Diva DLL Diva is stubbed off.
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

        ElseIf POST.ToUpperInvariant.Contains("GET_PARTNER") Then
            Debug.Print("get Partner")
            Dim PWok As Boolean = CheckPassword(POST, Settings.MachineID())
            If Not PWok Then Return ""

            Dim pattern1 As Regex = New Regex("User=(.*)", RegexOptions.IgnoreCase)
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
        ElseIf POST.ToUpperInvariant.Contains("SET_PARTNER") Then
            Debug.Print("set Partner")
            Dim PWok As Boolean = CheckPassword(POST, CStr(Settings.MachineID()))
            If Not PWok Then Return ""

            Dim pattern1 As Regex = New Regex("User=(.*?)&", RegexOptions.IgnoreCase)
            Dim match1 As Match = pattern1.Match(POST)
            If match1.Success Then

                Dim p2 As String = ""
                Dim p1 = match1.Groups(1).Value
                Dim pattern2 As Regex = New Regex("Partner=(.*)", RegexOptions.IgnoreCase)
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

#Region "Opensim.ini writers"

    Public Shared Function SetRegionVars(RegionName As String, RegionUUID As String) As Boolean

        ' edit the region INI
        If Form1.Settings.LoadIni(Form1.PropRegionClass.RegionPath(RegionUUID), ";") Then Return True

        ' Autobackup
        If Form1.Settings.AutoBackup And Form1.PropRegionClass.SkipAutobackup(RegionUUID) = "" Then
            Form1.Settings.SetIni(RegionName, "AutoBackup", "True")
        Else
            Form1.Settings.SetIni(RegionName, "AutoBackup", "False")
        End If

        Form1.Settings.SetIni(RegionName, "InternalPort", Convert.ToString(Form1.PropRegionClass.RegionPort(RegionUUID), Globalization.CultureInfo.InvariantCulture))
        Form1.Settings.SetIni(RegionName, "ExternalHostName", Form1.ExternLocalServerName())

        ' not a standard INI, only use by the Dreamers
        If Form1.PropRegionClass.RegionEnabled(RegionUUID) Then
            Form1.Settings.SetIni(RegionName, "Enabled", "True")
        Else
            Form1.Settings.SetIni(RegionName, "Enabled", "False")
        End If

        ' Extended in v 2.1

        Select Case Form1.PropRegionClass.NonPhysicalPrimMax(RegionUUID)
            Case ""
                Form1.Settings.SetIni(RegionName, "NonPhysicalPrimMax", 1024.ToString(Globalization.CultureInfo.InvariantCulture))
            Case Else
                Form1.Settings.SetIni(RegionName, "NonPhysicalPrimMax", Form1.PropRegionClass.NonPhysicalPrimMax(RegionUUID))
        End Select

        Select Case Form1.PropRegionClass.PhysicalPrimMax(RegionUUID)
            Case ""
                Form1.Settings.SetIni(RegionName, "PhysicalPrimMax", 64.ToString(Globalization.CultureInfo.InvariantCulture))
            Case Else
                Form1.Settings.SetIni(RegionName, "PhysicalPrimMax", Form1.PropRegionClass.PhysicalPrimMax(RegionUUID))
        End Select

        If (Form1.Settings.Primlimits) Then
            Select Case Form1.PropRegionClass.MaxPrims(RegionUUID)
                Case ""
                    Form1.Settings.SetIni(RegionName, "MaxPrims", 45000.ToString(Globalization.CultureInfo.InvariantCulture))
                Case Else
                    Form1.Settings.SetIni(RegionName, "MaxPrims", Form1.PropRegionClass.MaxPrims(RegionUUID))
            End Select
        Else
            Select Case Form1.PropRegionClass.MaxPrims(RegionUUID)
                Case ""
                    Form1.Settings.SetIni(RegionName, "MaxPrims", 45000.ToString(Globalization.CultureInfo.InvariantCulture))
                Case Else
                    Form1.Settings.SetIni(RegionName, "MaxPrims", Form1.PropRegionClass.MaxPrims(RegionUUID))
            End Select
        End If

        Select Case Form1.PropRegionClass.MaxAgents(RegionUUID)
            Case ""
                Form1.Settings.SetIni(RegionName, "MaxAgents", 100.ToString(Globalization.CultureInfo.InvariantCulture))
            Case Else
                Form1.Settings.SetIni(RegionName, "MaxAgents", Form1.PropRegionClass.MaxAgents(RegionUUID))
        End Select

        Form1.Settings.SetIni(RegionName, "ClampPrimSize", Convert.ToString(Form1.PropRegionClass.ClampPrimSize(RegionUUID), Globalization.CultureInfo.InvariantCulture))

        ' Optional Extended in v 2.31 optional things
        If Form1.PropRegionClass.MapType(RegionUUID) = "None" Then
            Form1.Settings.SetIni(RegionName, "GenerateMaptiles", "False")
        ElseIf Form1.PropRegionClass.MapType(RegionUUID) = "Simple" Then
            Form1.Settings.SetIni(RegionName, "GenerateMaptiles", "True")
            Form1.Settings.SetIni(RegionName, "MapImageModule", "MapImageModule")  ' versus Warp3DImageModule
            Form1.Settings.SetIni(RegionName, "TextureOnMapTile", "False")         ' versus True
            Form1.Settings.SetIni(RegionName, "DrawPrimOnMapTile", "False")
            Form1.Settings.SetIni(RegionName, "TexturePrims", "False")
            Form1.Settings.SetIni(RegionName, "RenderMeshes", "False")
        ElseIf Form1.PropRegionClass.MapType(RegionUUID) = "Good" Then
            Form1.Settings.SetIni(RegionName, "GenerateMaptiles", "True")
            Form1.Settings.SetIni(RegionName, "MapImageModule", "Warp3DImageModule")  ' versus MapImageModule
            Form1.Settings.SetIni(RegionName, "TextureOnMapTile", "False")         ' versus True
            Form1.Settings.SetIni(RegionName, "DrawPrimOnMapTile", "False")
            Form1.Settings.SetIni(RegionName, "TexturePrims", "False")
            Form1.Settings.SetIni(RegionName, "RenderMeshes", "False")
        ElseIf Form1.PropRegionClass.MapType(RegionUUID) = "Better" Then
            Form1.Settings.SetIni(RegionName, "GenerateMaptiles", "True")
            Form1.Settings.SetIni(RegionName, "MapImageModule", "Warp3DImageModule")  ' versus MapImageModule
            Form1.Settings.SetIni(RegionName, "TextureOnMapTile", "True")         ' versus True
            Form1.Settings.SetIni(RegionName, "DrawPrimOnMapTile", "True")
            Form1.Settings.SetIni(RegionName, "TexturePrims", "False")
            Form1.Settings.SetIni(RegionName, "RenderMeshes", "False")
        ElseIf Form1.PropRegionClass.MapType(RegionUUID) = "Best" Then
            Form1.Settings.SetIni(RegionName, "GenerateMaptiles", "True")
            Form1.Settings.SetIni(RegionName, "MapImageModule", "Warp3DImageModule")  ' versus MapImageModule
            Form1.Settings.SetIni(RegionName, "TextureOnMapTile", "True")      ' versus True
            Form1.Settings.SetIni(RegionName, "DrawPrimOnMapTile", "True")
            Form1.Settings.SetIni(RegionName, "TexturePrims", "True")
            Form1.Settings.SetIni(RegionName, "RenderMeshes", "True")
        Else
            Form1.Settings.SetIni(RegionName, "GenerateMaptiles", "")
            Form1.Settings.SetIni(RegionName, "MapImageModule", "")  ' versus MapImageModule
            Form1.Settings.SetIni(RegionName, "TextureOnMapTile", "")      ' versus True
            Form1.Settings.SetIni(RegionName, "DrawPrimOnMapTile", "")
            Form1.Settings.SetIni(RegionName, "TexturePrims", "")
            Form1.Settings.SetIni(RegionName, "RenderMeshes", "")
        End If

        Form1.Settings.SetIni(RegionName, "DisableGloebits", Form1.PropRegionClass.DisableGloebits(RegionUUID))
        Form1.Settings.SetIni(RegionName, "RegionSnapShot", Form1.PropRegionClass.RegionSnapShot(RegionUUID))
        Form1.Settings.SetIni(RegionName, "Birds", Form1.PropRegionClass.Birds(RegionUUID))
        Form1.Settings.SetIni(RegionName, "Tides", Form1.PropRegionClass.Tides(RegionUUID))
        Form1.Settings.SetIni(RegionName, "Teleport", Form1.PropRegionClass.Teleport(RegionUUID))
        Form1.Settings.SetIni(RegionName, "DisallowForeigners", Form1.PropRegionClass.DisallowForeigners(RegionUUID))
        Form1.Settings.SetIni(RegionName, "DisallowResidents", Form1.PropRegionClass.DisallowResidents(RegionUUID))
        Form1.Settings.SetIni(RegionName, "SkipAutoBackup", Form1.PropRegionClass.SkipAutobackup(RegionUUID))
        Form1.Settings.SetIni(RegionName, "Physics", Form1.PropRegionClass.Physics(RegionUUID))
        Form1.Settings.SetIni(RegionName, "FrameTime", Form1.PropRegionClass.FrameTime(RegionUUID))

        Form1.Settings.SaveINI(System.Text.Encoding.UTF8)

        Return False

        '''
    End Function
    Public Shared Function SetOpensimIni(RegionName As String, RegionUUID As String) As Boolean

        ' Opensim.ini in Region Folder specific to this region
        If Form1.Settings.LoadIni(Form1.PropOpensimBinPath & "bin\Regions\" & Form1.PropRegionClass.GroupName(RegionUUID) & "\Opensim.ini", ";") Then
            Return True
        End If

        ' Autobackup
        If Form1.Settings.AutoBackup Then
            Form1.Settings.SetIni("AutoBackupModule", "AutoBackup", "True")
        End If

        If Form1.Settings.AutoBackup And Form1.PropRegionClass.SkipAutobackup(RegionUUID) = "" Then
            Form1.Settings.SetIni("AutoBackupModule", "AutoBackup", "True")
        End If

        If Form1.Settings.AutoBackup And Form1.PropRegionClass.SkipAutobackup(RegionUUID) = "True" Then
            Form1.Settings.SetIni("AutoBackupModule", "AutoBackup", "False")
        End If

        If Not Form1.Settings.AutoBackup Then
            Form1.Settings.SetIni("AutoBackupModule", "AutoBackup", "False")
        End If

        Form1.Settings.SetIni("AutoBackupModule", "AutoBackupInterval", Form1.Settings.AutobackupInterval)
        Form1.Settings.SetIni("AutoBackupModule", "AutoBackupKeepFilesForDays", Convert.ToString(Form1.Settings.KeepForDays, Globalization.CultureInfo.InvariantCulture))
        Form1.Settings.SetIni("AutoBackupModule", "AutoBackupDir", Form1.BackupPath())

        If Form1.PropRegionClass.MapType(RegionUUID) = "Simple" Then
            Form1.Settings.SetIni("Map", "GenerateMaptiles", "True")
            Form1.Settings.SetIni("Map", "MapImageModule", "MapImageModule")  ' versus Warp3DImageModule
            Form1.Settings.SetIni("Map", "TextureOnMapTile", "False")         ' versus True
            Form1.Settings.SetIni("Map", "DrawPrimOnMapTile", "False")
            Form1.Settings.SetIni("Map", "TexturePrims", "False")
            Form1.Settings.SetIni("Map", "RenderMeshes", "False")
        ElseIf Form1.PropRegionClass.MapType(RegionUUID) = "Good" Then
            Form1.Settings.SetIni(RegionName, "GenerateMaptiles", "True")
            Form1.Settings.SetIni("Map", "MapImageModule", "Warp3DImageModule")  ' versus MapImageModule
            Form1.Settings.SetIni("Map", "TextureOnMapTile", "False")         ' versus True
            Form1.Settings.SetIni("Map", "DrawPrimOnMapTile", "False")
            Form1.Settings.SetIni("Map", "TexturePrims", "False")
            Form1.Settings.SetIni("Map", "RenderMeshes", "False")
        ElseIf Form1.PropRegionClass.MapType(RegionUUID) = "Better" Then
            Form1.Settings.SetIni("Map", "GenerateMaptiles", "True")
            Form1.Settings.SetIni("Map", "MapImageModule", "Warp3DImageModule")  ' versus MapImageModule
            Form1.Settings.SetIni("Map", "TextureOnMapTile", "True")         ' versus True
            Form1.Settings.SetIni("Map", "DrawPrimOnMapTile", "True")
            Form1.Settings.SetIni("Map", "TexturePrims", "False")
            Form1.Settings.SetIni("Map", "RenderMeshes", "False")
        ElseIf Form1.PropRegionClass.MapType(RegionUUID) = "Best" Then
            Form1.Settings.SetIni("Map", "GenerateMaptiles", "True")
            Form1.Settings.SetIni("Map", "MapImageModule", "Warp3DImageModule")  ' versus MapImageModule
            Form1.Settings.SetIni("Map", "TextureOnMapTile", "True")      ' versus True
            Form1.Settings.SetIni("Map", "DrawPrimOnMapTile", "True")
            Form1.Settings.SetIni("Map", "TexturePrims", "True")
            Form1.Settings.SetIni("Map", "RenderMeshes", "True")
        End If

        Select Case Form1.PropRegionClass.Physics(RegionUUID)
            Case ""
                Form1.Settings.SetIni("Startup", "meshing", "Meshmerizer")
                Form1.Settings.SetIni("Startup", "physics", "BulletSim")
                Form1.Settings.SetIni("Startup", "UseSeparatePhysicsThread", "True")
            Case "0"
                Form1.Settings.SetIni("Startup", "meshing", "ZeroMesher")
                Form1.Settings.SetIni("Startup", "physics", "basicphysics")
                Form1.Settings.SetIni("Startup", "UseSeparatePhysicsThread", "False")
            Case "1"
                Form1.Settings.SetIni("Startup", "meshing", "Meshmerizer")
                Form1.Settings.SetIni("Startup", "physics", "OpenDynamicsEngine")
                Form1.Settings.SetIni("Startup", "UseSeparatePhysicsThread", "False")
            Case "2"
                Form1.Settings.SetIni("Startup", "meshing", "Meshmerizer")
                Form1.Settings.SetIni("Startup", "physics", "BulletSim")
                Form1.Settings.SetIni("Startup", "UseSeparatePhysicsThread", "False")
            Case "3"
                Form1.Settings.SetIni("Startup", "meshing", "Meshmerizer")
                Form1.Settings.SetIni("Startup", "physics", "BulletSim")
                Form1.Settings.SetIni("Startup", "UseSeparatePhysicsThread", "True")
            Case "4"
                Form1.Settings.SetIni("Startup", "meshing", "ubODEMeshmerizer")
                Form1.Settings.SetIni("Startup", "physics", "ubODE")
                Form1.Settings.SetIni("Startup", "UseSeparatePhysicsThread", "False")
            Case "5"
                Form1.Settings.SetIni("Startup", "meshing", "Meshmerizer")
                Form1.Settings.SetIni("Startup", "physics", "ubODE")
                Form1.Settings.SetIni("Startup", "UseSeparatePhysicsThread", "False")
            Case Else
                ' do nothing
        End Select

        If Form1.PropRegionClass.GodDefault(RegionUUID) = "" _
            Or Form1.PropRegionClass.GodDefault(RegionUUID) = "False" Then

            Select Case Form1.PropRegionClass.AllowGods(RegionUUID)
                Case ""
                    Form1.Settings.SetIni("Permissions", "allow_grid_gods", CStr(Form1.Settings.AllowGridGods))
                Case "False"
                    Form1.Settings.SetIni("Permissions", "allow_grid_gods", "False")
                Case "True"
                    Form1.Settings.SetIni("Permissions", "allow_grid_gods", "True")
            End Select


            Select Case Form1.PropRegionClass.RegionGod(RegionUUID)
                Case ""
                    Form1.Settings.SetIni("Permissions", "region_owner_is_god", CStr(Form1.Settings.RegionOwnerIsGod))
                Case "False"
                    Form1.Settings.SetIni("Permissions", "region_owner_is_god", "False")
                Case "True"
                    Form1.Settings.SetIni("Permissions", "region_owner_is_god", "True")
            End Select

            Select Case Form1.PropRegionClass.ManagerGod(RegionUUID)
                Case ""
                    Form1.Settings.SetIni("Permissions", "region_manager_is_god", CStr(Form1.Settings.RegionManagerIsGod))
                Case "False"
                    Form1.Settings.SetIni("Permissions", "region_manager_is_god", "False")
                Case "True"
                    Form1.Settings.SetIni("Permissions", "region_manager_is_god", "True")
            End Select

        Else
            Form1.Settings.SetIni("Permissions", "allow_grid_gods", "False")
            Form1.Settings.SetIni("Permissions", "region_manager_is_god", "False")
            Form1.Settings.SetIni("Permissions", "allow_grid_gods", "True")
        End If

        ' V3.15
        If Form1.PropRegionClass.NonPhysicalPrimMax(RegionUUID).Length > 0 Then
            Form1.Settings.SetIni("Startup", "NonPhysicalPrimMax", Convert.ToString(Form1.PropRegionClass.NonPhysicalPrimMax(RegionUUID), Globalization.CultureInfo.InvariantCulture))
        End If

        If Form1.PropRegionClass.PhysicalPrimMax(RegionUUID).Length > 0 Then
            Form1.Settings.SetIni("Startup", "PhysicalPrimMax", Convert.ToString(Form1.PropRegionClass.PhysicalPrimMax(RegionUUID), Globalization.CultureInfo.InvariantCulture))
        End If

        If Form1.PropRegionClass.MinTimerInterval(RegionUUID).Length > 0 Then
            Form1.Settings.SetIni("XEngine", "MinTimerInterval", Convert.ToString(Form1.PropRegionClass.MinTimerInterval(RegionUUID), Globalization.CultureInfo.InvariantCulture))
        End If

        If Form1.PropRegionClass.FrameTime(RegionUUID).Length > 0 Then
            Form1.Settings.SetIni("Startup", "FrameTime", Convert.ToString(Form1.PropRegionClass.FrameTime(RegionUUID), Globalization.CultureInfo.InvariantCulture))
        End If


        ' no FALSE setting for these
        Form1.Settings.SetIni("SmartStart", "Enabled", Form1.PropRegionClass.SmartStart(RegionUUID))

        If Form1.PropRegionClass.DisallowForeigners(RegionUUID) = "True" Then
            Form1.Settings.SetIni("DisallowForeigners", "Enabled", Convert.ToString(Form1.PropRegionClass.DisallowForeigners(RegionUUID), Globalization.CultureInfo.InvariantCulture))
        End If


        If Form1.PropRegionClass.DisallowResidents(RegionUUID) = "True" Then
            Form1.Settings.SetIni("DisallowResidents", "Enabled", Convert.ToString(Form1.PropRegionClass.DisallowResidents(RegionUUID), Globalization.CultureInfo.InvariantCulture))
        End If

        ' replace with a PHP module
        If Form1.PropRegionClass.DisableGloebits(RegionUUID) = "True" Then
            Form1.Settings.SetIni("Startup", "economymodule", "BetaGridLikeMoneyModule")
        End If

        ' Search
        Select Case Form1.PropRegionClass.Snapshot(RegionUUID)
            Case ""
                Form1.Settings.SetIni("DataSnapshot", "index_sims", CStr(Form1.Settings.SearchEnabled))
            Case "True"
                Form1.Settings.SetIni("DataSnapshot", "index_sims", "True")
            Case "False"
                Form1.Settings.SetIni("DataSnapshot", "index_sims", "False")
        End Select

        'ScriptEngine Overrides
        If Form1.PropRegionClass.ScriptEngine(RegionUUID) = "XEngine" Then
            Form1.Settings.SetIni("Startup", "DefaultScriptEngine", "XEngine")
            Form1.Settings.SetIni("XEngine", "Enabled", "True")
            Form1.Settings.SetIni("YEngine", "Enabled", "False")
        End If

        If Form1.PropRegionClass.ScriptEngine(RegionUUID) = "YEngine" Then
            Form1.Settings.SetIni("Startup", "DefaultScriptEngine", "YEngine")
            Form1.Settings.SetIni("XEngine", "Enabled", "False")
            Form1.Settings.SetIni("YEngine", "Enabled", "True")
        End If

        Form1.Settings.SaveINI(System.Text.Encoding.UTF8)

        Return False


    End Function

    Public Shared Sub CopyOpensimProto(name As String)

        Dim RegionUUID As String = Form1.PropRegionClass.FindRegionByName(name)
        If RegionUUID.Length > 0 Then Opensimproto(RegionUUID)

    End Sub


    Public Shared Function Opensimproto(RegionUUID As String) As Boolean

        Dim regionName = Form1.PropRegionClass.RegionName(RegionUUID)
        Dim pathname = Form1.PropRegionClass.IniPath(RegionUUID)

        If Form1.Settings.LoadIni(Form1.GetOpensimProto(), ";") Then Return True

        Form1.Settings.SetIni("Const", "BaseHostname", Form1.Settings.BaseHostName)

        Form1.Settings.SetIni("Const", "PublicPort", CStr(Form1.Settings.HttpPort)) ' 8002
        Form1.Settings.SetIni("Const", "PrivURL", "http://" & CStr(Form1.Settings.PrivateURL)) ' local IP
        Form1.Settings.SetIni("Const", "http_listener_port", CStr(Form1.PropRegionClass.RegionPort(RegionUUID))) ' varies with region

        ' set new Min Timer Interval for how fast a script can go. Can be set in region files as a float, or nothing
        Dim Xtime As Double = 1 / 11   '1/11 of a second is as fast as she can go
        If Form1.PropRegionClass.MinTimerInterval(RegionUUID).Length > 0 Then
            If Not Double.TryParse(Form1.PropRegionClass.MinTimerInterval(RegionUUID), Xtime) Then
                Xtime = 1.0 / 11.0
            End If
        End If
        Form1.Settings.SetIni("XEngine", "MinTimerInterval", Convert.ToString(Xtime, Globalization.CultureInfo.InvariantCulture))
        Form1.Settings.SetIni("YEngine", "MinTimerInterval", Convert.ToString(Xtime, Globalization.CultureInfo.InvariantCulture))

        Dim name = Form1.PropRegionClass.RegionName(RegionUUID)

        ' save the http listener port away for the group
        Form1.PropRegionClass.GroupPort(RegionUUID) = Form1.PropRegionClass.RegionPort(RegionUUID)

        Form1.Settings.SetIni("Const", "PrivatePort", CStr(Form1.Settings.PrivatePort)) '8003
        Form1.Settings.SetIni("Const", "RegionFolderName", Form1.PropRegionClass.GroupName(RegionUUID))
        Form1.Settings.SaveINI(System.Text.Encoding.UTF8)

        Try
            My.Computer.FileSystem.CopyFile(Form1.GetOpensimProto(), pathname & "Opensim.ini", True)
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

#End Region

End Class
