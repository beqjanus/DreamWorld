Imports MySql.Data.MySqlClient

Imports System.Text.RegularExpressions
Imports System.IO
Imports Newtonsoft.Json
Imports System.Net

Public Class RegionMaker

#Region "Declarations"

#Disable Warning IDE0044 ' Add readonly modifier
    Private MysqlConn As Mysql    ' object lets us query Mysql database
#Enable Warning IDE0044 ' Add readonly modifier
    Public RegionList As New ArrayList()
    Public Grouplist As New Dictionary(Of String, Integer)

    Private initted As Boolean = False
    Private Shared FInstance As RegionMaker = Nothing
    Dim Backup As New ArrayList()
    Dim json As JSON_result

    Dim WebserverList As New List(Of String)


    Public Enum SIM_STATUS As Integer
        Stopped = 0
        Booting = 1
        Booted = 2
        RecyclingUp = 3
        RecyclingDown = 4
        ShuttingDown = 5
        RestartPending = 6
        RetartingNow = 7

    End Enum

    Public Enum REGION_TIMER As Integer
        Stopped = -1
        Start_Counting = 0
    End Enum

    ''' <summary>
    ''' Regionclass.IsBooted() checks if a region is up
    ''' </summary>
    ''' <param name="RegionNumber"></param>
    ''' <returns>boolean</returns>
    Public Function IsBooted(RegionNumber As Integer) As Boolean
        If Status(RegionNumber) = SIM_STATUS.Booted Then
            Return True
        End If
        Return False
    End Function

    Public Sub DebugGroup()
        For Each pair In Grouplist
            Debug.Print("Group name: {0}, httpport: {1}", pair.Key, pair.Value)
        Next
    End Sub

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

    Public Shared ReadOnly Property Instance(MysqlConn As Mysql) As RegionMaker
        Get
            If (FInstance Is Nothing) Then
                FInstance = New RegionMaker(MysqlConn)
            End If
            Return FInstance
        End Get
    End Property

    Private Sub New(conn As Mysql)

        MysqlConn = conn
        GetAllRegions()
        If RegionCount() = 0 Then
            CreateRegion("Welcome")
            Form1.MySetting.WelcomeRegion = "Welcome"
            WriteRegionObject("Welcome")
            GetAllRegions()
            Form1.MySetting.WelcomeRegion = "Welcome"
            Form1.MySetting.SaveSettings()
        End If
        ' RegionDump()

        Debug.Print("Loaded " + RegionCount.ToString + " Regions")

    End Sub

#End Region

#Region "Classes"
    Public Class JSON_result
        Public alert As String
        Public login As String
        Public region_name As String
        Public region_id As String
    End Class

    ' hold a copy of the Main region data on a per-form basis
    Private Class Region_data
        Public _RegionPath As String = ""  ' The full path to the region ini file
        Public _FolderPath As String = ""   ' the path to the folde r that holds the region ini
        Public _Group As String = ""       ' the folder name that holds the region(s), can be different named
        Public _IniPath As String = ""      ' the folder that hold the Opensim.ini, above 'Region'
        Public _RegionName As String = ""
        Public _UUID As String = ""
        Public _CoordX As Integer = 0
        Public _CoordY As Integer = 0
        Public _RegionPort As Integer = 0
        Public _SizeX As Integer = 0
        Public _SizeY As Integer = 0
        Public _RegionEnabled As Boolean = False ' Will run or not
        Public _NonPhysicalPrimMax As Integer
        Public _PhysicalPrimMax As Integer
        Public _ClampPrimSize As Boolean
        Public _MaxPrims As String
        Public _MaxAgents As Integer

        ' RAM vars, not from files
        Public _AvatarCount As Integer = 0
        Public _ProcessID As Integer = 0
        Public _Status As Integer = 0
        Public _Timer As Integer
        Public _LineCounter As Integer

        'extended vars
        Public _RegionSnapShot As String
        Public _MapType As String
        Public _physics As String
        Public _AllowGods As String
        Public _RegionGod As String
        Public _ManagerGod As String
        Public _Birds As String = ""
        Public _Tides As String = ""
        Public _Teleport As String = ""

    End Class

#End Region

#Region "Properties"

    Public Property GroupName(n As Integer) As String
        Get
            Try
                Return CType(RegionList(n)._Group, String)
            Catch
                Return ""
            End Try

        End Get
        Set(ByVal Value As String)
            RegionList(n)._Group = Value
        End Set
    End Property
    Public Property NonPhysicalPrimMax(n As Integer) As Integer
        Get
            Return CType(RegionList(n)._NonphysicalPrimMax, Integer)
        End Get
        Set(ByVal Value As Integer)
            RegionList(n)._NonphysicalPrimMax = Value.ToString
        End Set
    End Property
    Public Property PhysicalPrimMax(n As Integer) As Integer
        Get
            Return CType(RegionList(n)._PhysicalPrimMax, Integer)
        End Get
        Set(ByVal Value As Integer)
            RegionList(n)._PhysicalPrimMax = Value.ToString
        End Set
    End Property
    Public Property ClampPrimSize(n As Integer) As Boolean
        Get
            Return CType(RegionList(n)._ClampPrimSize, Boolean)
        End Get
        Set(ByVal Value As Boolean)
            RegionList(n)._ClampPrimSize = Value.ToString
        End Set
    End Property
    Public Property MaxPrims(n As Integer) As String
        Get
            Return CType(RegionList(n)._MaxPrims, String)
        End Get
        Set(ByVal Value As String)
            RegionList(n)._MaxPrims = Value
        End Set
    End Property
    Public Property MaxAgents(n As Integer) As Integer
        Get
            Return CType(RegionList(n)._MaxAgents, Integer)
        End Get
        Set(ByVal Value As Integer)
            RegionList(n)._MaxAgents = Value.ToString
        End Set
    End Property
    Public Property Timer(n As Integer) As Integer
        Get
            Try
                Return CType(RegionList(n)._Timer, Integer)
            Catch
                Return 0
            End Try

        End Get
        Set(ByVal Value As Integer)
            RegionList(n)._Timer = Value.ToString
        End Set
    End Property
    Public Property LineCounter(n As Integer) As Integer
        Get
            Return CType(RegionList(n)._LineCounter, Integer)
        End Get
        Set(ByVal Value As Integer)
            RegionList(n)._LineCounter = Value
        End Set
    End Property
    Public Property Status(n As Integer) As Integer
        Get
            Return CType(RegionList(n)._Status, Integer)
        End Get
        Set(ByVal Value As Integer)
            RegionList(n)._Status = Value
        End Set
    End Property

    Public ReadOnly Property RegionCount() As Integer
        Get
            Return RegionList.Count
        End Get
    End Property
    ''' ''''''''''''''''''' PATHS ''''''''''''''''''''
    Public Property IniPath(n As Integer) As String
        Get
            Return RegionList(n)._IniPath.ToString
        End Get
        Set(ByVal Value As String)
            RegionList(n)._IniPath = Value
        End Set
    End Property
    Public Property RegionPath(n As Integer) As String
        Get
            Return RegionList(n)._RegionPath.ToString
        End Get
        Set(ByVal Value As String)
            RegionList(n)._RegionPath = Value
        End Set
    End Property
    Public Property FolderPath(n As Integer) As String
        Get
            Return RegionList(n)._FolderPath.ToString
        End Get
        Set(ByVal Value As String)
            RegionList(n)._FolderPath = Value
        End Set
    End Property
    Public Property RegionEnabled(n As Integer) As Boolean
        Get
            Return CType(RegionList(n)._RegionEnabled, Boolean)
        End Get
        Set(ByVal Value As Boolean)
            RegionList(n)._RegionEnabled = Value.ToString
        End Set
    End Property
    Public Property ProcessID(n As Integer) As Integer
        Get
            Try
                Return CType(RegionList(n)._ProcessID, Integer)
            Catch
                Return 0
            End Try

        End Get
        Set(ByVal Value As Integer)
            RegionList(n)._ProcessID = Value.ToString
        End Set
    End Property
    Public Property AvatarCount(n As Integer) As Integer
        Get
            Return CType(RegionList(n)._AvatarCount, Integer)
        End Get
        Set(ByVal Value As Integer)
            RegionList(n)._AvatarCount = Value.ToString
        End Set
    End Property
    Public Property RegionName(n As Integer) As String
        Get
            Return RegionList(n)._RegionName.ToString
        End Get
        Set(ByVal Value As String)
            RegionList(n)._RegionName = Value.ToString
        End Set
    End Property
    Public Property UUID(n As Integer) As String
        Get
            Return RegionList(n)._UUID.ToString
        End Get
        Set(ByVal Value As String)
            RegionList(n)._UUID = Value.ToString
        End Set
    End Property
    Public Property SizeX(n As Integer) As Integer
        Get
            Return CType(RegionList(n)._SizeX, Integer)
        End Get
        Set(ByVal Value As Integer)
            RegionList(n)._SizeX = Value.ToString
        End Set
    End Property
    Public Property SizeY(n As Integer) As Integer
        Get
            Return CType(RegionList(n)._SizeY, Integer)
        End Get
        Set(ByVal Value As Integer)
            RegionList(n)._SizeY = Value.ToString
        End Set
    End Property
    Public Property RegionPort(n As Integer) As Integer
        Get
            Try
                Return CType(RegionList(n)._RegionPort, Integer)
            Catch
                Form1.ErrorLog("Bad region port: " + RegionList(n)._RegionPort.ToString)
            End Try
            Return 0
        End Get
        Set(ByVal Value As Integer)
            RegionList(n)._RegionPort = Value.ToString
        End Set
    End Property
    Public Property CoordX(n As Integer) As Integer
        Get
            Return CType(RegionList(n)._CoordX, Integer)
        End Get
        Set(ByVal Value As Integer)
            RegionList(n)._CoordX = Value.ToString
        End Set
    End Property
    Public Property CoordY(n As Integer) As Integer
        Get
            Return CType(RegionList(n)._CoordY, Integer)
        End Get
        Set(ByVal Value As Integer)
            RegionList(n)._CoordY = Value.ToString
        End Set
    End Property
    Public Property RegionSnapShot(n As Integer) As String
        Get
            Return CType(RegionList(n)._RegionSnapShot, String)
        End Get
        Set(ByVal Value As String)
            RegionList(n)._RegionSnapShot = Value
        End Set
    End Property
    Public Property MapType(n As Integer) As String
        Get
            Return CType(RegionList(n)._MapType, String)
        End Get
        Set(ByVal Value As String)
            RegionList(n)._MapType = Value
        End Set
    End Property
    Public Property Physics(n As Integer) As String
        Get
            Return CType(RegionList(n)._physics, String)
        End Get
        Set(ByVal Value As String)
            RegionList(n)._physics = Value
        End Set
    End Property
    Public Property AllowGods(n As Integer) As String
        Get
            Return CType(RegionList(n)._AllowGods, String)
        End Get
        Set(ByVal Value As String)
            RegionList(n)._AllowGods = Value
        End Set
    End Property
    Public Property RegionGod(n As Integer) As String
        Get
            Return CType(RegionList(n)._RegionGod, String)
        End Get
        Set(ByVal Value As String)
            RegionList(n)._RegionGod = Value
        End Set
    End Property
    Public Property ManagerGod(n As Integer) As String
        Get
            Return CType(RegionList(n)._ManagerGod, String)
        End Get
        Set(ByVal Value As String)
            RegionList(n)._ManagerGod = Value
        End Set
    End Property
    Public Property Birds(n As Integer) As String
        Get
            Return RegionList(n)._Birds.ToString
        End Get
        Set(ByVal Value As String)
            RegionList(n)._Birds = Value
        End Set
    End Property
    Public Property Tides(n As Integer) As String
        Get
            Return RegionList(n)._Tides.ToString
        End Get
        Set(ByVal Value As String)
            RegionList(n)._Tides = Value.ToString
        End Set
    End Property
    Public Property Teleport(n As Integer) As String
        Get
            Return RegionList(n)._Teleport.ToString
        End Get
        Set(ByVal Value As String)
            RegionList(n)._Teleport = Value.ToString
        End Set
    End Property

#End Region

#Region "Functions"

    Public Sub RegionDump()

        If Not Form1.gDebug Then Return
        Dim ctr = 0
        For Each r As Region_data In RegionList
            DebugRegions(ctr)
            ctr = ctr + 1
        Next

    End Sub

    Public Sub DebugRegions(n As Integer)

        Form1.Log("RegionNumber", n.ToString + vbCrLf +
            " PID:" + RegionList(n)._ProcessID.ToString + vbCrLf +
            " Group:" + RegionList(n)._Group.ToString + vbCrLf +
            " Region:" + RegionList(n)._RegionName.ToString + vbCrLf +
            " Status=" + RegionList(n)._Status.ToString + vbCrLf +
            " LineCtr=" + RegionList(n)._LineCounter.ToString + vbCrLf +
           " RegionEnabled=" + RegionList(n)._RegionEnabled.ToString + vbCrLf +
           " Timer=" + RegionList(n)._Timer.ToString)

    End Sub

    Public Function RegionListByGroupNum(Gname As String) As List(Of Integer)

        Dim L As New List(Of Integer)
        Dim ctr = 0
        For Each n As Region_data In RegionList
            If n._Group = Gname Or Gname = "*" Then
                L.Add(ctr)
            End If
            ctr = ctr + 1
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
            ctr = ctr + 1
        Next
        'Debug.Print("List Len = " + L.Count.ToString)
        Return L
    End Function

    Public Function FindRegionByName(Name As String) As Integer

        Dim i As Integer = 0
        For Each obj As Region_data In RegionList
            If Name = obj._RegionName Then
                Debug.Print("Current Region is " + obj._RegionName)
                Return i
            End If
            i = i + 1
        Next

        'RegionDump()
        Return -1

    End Function


    Public Function FindGroupNamebyRegionName(RegionName As String) As String

        Dim i As Integer = 0
        For Each obj As Region_data In RegionList
            If RegionName = obj._Group Then
                Return obj._Group
            End If
            i = i + 1
        Next

        Return ""
    End Function

    Public Function FindRegionByProcessID(PID As Integer) As Integer

        Dim i As Integer = 0
        For Each obj As Region_data In RegionList
            If PID = obj._ProcessID Then
                Debug.Print("Current Region is " + obj._RegionName)
                Return i
            End If
            i = i + 1
        Next
        Form1.ErrorLog("PID not found:" + PID.ToString)
        Return -1

    End Function

    Public Function FindBackupByName(Name As String) As Integer

        Dim i As Integer = 0
        For Each obj As Region_data In Backup
            If Name = obj._RegionName Then
                ' Debug.Print("Current Backup is " + obj._RegionName)
                Return i
            End If
            i = i + 1
        Next
        Return -1

    End Function

    Public Function CreateRegion(name As String) As Integer

        If RegionList.Contains(name) Then
            Return RegionList.Count - 1
        End If
        ' Debug.Print("Create Region " + name)
        Dim r As New Region_data
        r._RegionName = name
        r._RegionEnabled = True
        r._UUID = Guid.NewGuid().ToString
        r._SizeX = 256
        r._SizeY = 256
        r._CoordX = LargestX() + 4
        r._CoordY = LargestY() + 0
        r._RegionPort = CType(Form1.MySetting.PrivatePort, Integer) + 1 '8003 + 1
        r._ProcessID = 0
        r._AvatarCount = 0
        r._Status = SIM_STATUS.Stopped
        r._LineCounter = 0
        r._Timer = 0
        r._NonPhysicalPrimMax = 1024
        r._PhysicalPrimMax = 64
        r._ClampPrimSize = False
        r._MaxPrims = "45000"
        r._MaxAgents = 100

        RegionList.Add(r)
        'RegionDump()
        Return RegionList.Count - 1

    End Function

    Public Sub GetAllRegions()

        Backup.Clear()

        For Each thing As Region_data In RegionList
            Backup.Add(thing)
        Next

        RegionList.Clear()

        Dim folders() As String
        Dim regionfolders() As String
        Dim n As Integer = 0
        folders = Directory.GetDirectories(Form1.gOpensimBinPath + "bin\Regions")
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
                        Form1.MySetting.LoadOtherIni(ini, ";")
                        ' we do not save the above as we are making a new one.

                        Try
                            RegionEnabled(n) = CType(Form1.MySetting.GetIni(fName, "Enabled"), Boolean)
                        Catch ex As Exception
                            RegionEnabled(n) = True
                        End Try

                        RegionPath(n) = ini ' save the path
                        FolderPath(n) = System.IO.Path.GetDirectoryName(ini)

                        Dim theEnd As Integer = FolderPath(n).LastIndexOf("\")
                        IniPath(n) = FolderPath(n).Substring(0, theEnd + 1)

                        ' need folder name in case there are more than 1 ini
                        Dim theStart = FolderPath(n).IndexOf("Regions\") + 8
                        theEnd = FolderPath(n).LastIndexOf("\")
                        Dim gname = FolderPath(n).Substring(theStart, theEnd - theStart)

                        GroupName(n) = gname

                        UUID(n) = Form1.MySetting.GetIni(fName, "RegionUUID")
                        SizeX(n) = Convert.ToInt16(Form1.MySetting.GetIni(fName, "SizeX"))
                        SizeY(n) = Convert.ToInt16(Form1.MySetting.GetIni(fName, "SizeY"))
                        RegionPort(n) = Convert.ToInt16(Form1.MySetting.GetIni(fName, "InternalPort"))

                        ' extended props V2.1
                        NonPhysicalPrimMax(n) = Convert.ToInt16(Form1.MySetting.GetIni(fName, "NonPhysicalPrimMax", "1024"))
                        PhysicalPrimMax(n) = Convert.ToInt16(Form1.MySetting.GetIni(fName, "PhysicalPrimMax", "64"))
                        ClampPrimSize(n) = Convert.ToBoolean(Form1.MySetting.GetIni(fName, "ClampPrimSize", "False"))
                        MaxPrims(n) = Form1.MySetting.GetIni(fName, "MaxPrims", "45000")
                        MaxAgents(n) = Convert.ToInt16(Form1.MySetting.GetIni(fName, "MaxAgents", "100"))

                        ' Location is int,int format.
                        Dim C = Form1.MySetting.GetIni(fName, "Location")

                        Dim parts As String() = C.Split(New Char() {","c}) ' split at the comma
                        CoordX(n) = CType(parts(0), Integer)
                        CoordY(n) = CType(parts(1), Integer)

                        RegionSnapShot(n) = Form1.MySetting.GetIni(fName, "RegionSnapShot")
                        MapType(n) = Form1.MySetting.GetIni(fName, "MapType")
                        Physics(n) = Form1.MySetting.GetIni(fName, "Physics")
                        MaxPrims(n) = Form1.MySetting.GetIni(fName, "MaxPrims", "15000")
                        AllowGods(n) = Form1.MySetting.GetIni(fName, "AllowGods")
                        RegionGod(n) = Form1.MySetting.GetIni(fName, "RegionGod")
                        ManagerGod(n) = Form1.MySetting.GetIni(fName, "ManagerGod")
                        Birds(n) = Form1.MySetting.GetIni(fName, "Birds")
                        Tides(n) = Form1.MySetting.GetIni(fName, "Tides")
                        Teleport(n) = Form1.MySetting.GetIni(fName, "Teleport")

                        If initted Then

                            ' restore backups of transient data 
                            Try ' 6 temp vars from backup
                                Dim o = FindBackupByName(fName)

                                If o >= 0 Then
                                    AvatarCount(n) = CType(Backup(o)._AvatarCount, Integer)
                                    ProcessID(n) = CType(Backup(o)._ProcessID, Integer)
                                    Status(n) = CType(Backup(o)._Status, Integer)
                                    LineCounter(n) = CType(Backup(o)._LineCounter, Integer)
                                    Timer(n) = CType(Backup(o)._Timer, Integer)
                                End If

                            Catch
                            End Try

                        End If

                        n = n + 1
                        Application.DoEvents()
                    Next

                Catch ex As Exception
                    MsgBox("Error: Cannot understand the contents of region file " + fName + " : " + ex.Message, vbInformation, "Error")
                    Form1.ErrorLog("Err:Parse file " + fName + ":" + ex.Message)
                End Try
            Next
        Next

        initted = True
    End Sub

    Public Sub WriteRegionObject(name As String)

        Dim n As Integer = FindRegionByName(name)
        If n < 0 Then
            MsgBox("Cannot find region " + name, vbInformation, "Error")
            Return
        End If

        Dim fname As String = RegionList(n)._FolderPath.ToString

        If (fname = "") Then
            Dim pathtoWelcome As String = Form1.gOpensimBinPath + "bin\Regions\" + name + "\Region\"
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

        Dim proto = "; * Regions configuration file; " + vbCrLf _
        + "; Automatically changed and read by Dreamworld. Edits are allowed" + vbCrLf _
        + "; Rule1: The File name must match the [RegionName]" + vbCrLf _
        + "; Rule2: Only one region per INI file." + vbCrLf _
        + ";" + vbCrLf _
        + "[" + name + "]" + vbCrLf _
        + "RegionUUID = " + UUID(n) + vbCrLf _
        + "Location = " + CoordX(n).ToString & "," & CoordY(n).ToString + vbCrLf _
        + "InternalAddress = 0.0.0.0" + vbCrLf _
        + "InternalPort = " + RegionPort(n).ToString + vbCrLf _
        + "AllowAlternatePorts = False" + vbCrLf _
        + "ExternalHostName = " + Form1.MySetting.PublicIP + vbCrLf _
        + "SizeX = " + SizeX(n).ToString + vbCrLf _
        + "SizeY = " + SizeY(n).ToString + vbCrLf _
        + "Enabled = " + RegionEnabled(n).ToString + vbCrLf _
        + "NonPhysicalPrimMax = " + NonPhysicalPrimMax(n).ToString + vbCrLf _
        + "PhysicalPrimMax = " + PhysicalPrimMax(n).ToString + vbCrLf _
        + "ClampPrimSize = " + ClampPrimSize(n).ToString + vbCrLf _
        + "MaxPrims = " + MaxPrims(n) + vbCrLf _
        + "RegionType = Estate" + vbCrLf + vbCrLf _
        + ";# Dreamgrid extended properties" + vbCrLf _
        + "RegionSnapShot = " + RegionSnapShot(n) + vbCrLf _
        + "MapType = " + MapType(n) + vbCrLf _
        + "Physics = " + Physics(n) + vbCrLf _
        + "AllowGods = " + AllowGods(n) + vbCrLf _
        + "RegionGod = " + RegionGod(n) + vbCrLf _
        + "ManagerGod = " + ManagerGod(n) + vbCrLf _
        + "Birds = " + Birds(n) + vbCrLf _
        + "Tides = " + Tides(n) + vbCrLf _
        + "Teleport = " + Teleport(n) + vbCrLf

        Try
            My.Computer.FileSystem.DeleteFile(fname)
        Catch
        End Try

        Using outputFile As New StreamWriter(fname, True)
            outputFile.WriteLine(proto)
        End Using


    End Sub

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

    Public Function LargestPort() As Integer

        ' locate largest port
        Dim Max As Integer = 0
        Dim Portlist As New Dictionary(Of Integer, String)

        Dim counter As Integer = 0
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

    Function LowestPort() As Integer
        ' locate lowest port
        Dim Min As Integer = 65536
        Dim Portlist As New Dictionary(Of Integer, String)

        Dim counter As Integer = 0
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

    ''' <summary>
    ''' Self setting Region Ports
    ''' Iterate over all regions and set the ports from the starting value
    ''' </summary>
    Public Sub UpdateAllRegionPorts()

        If Form1.OpensimIsRunning Then
            'Form1.Log("Trying to update all region ports while running')")
            Return
        End If

        Dim Portnumber As Integer = CType(Form1.MySetting.FirstRegionPort(), Integer)
        For Each RegionNum As Integer In Form1.RegionClass.RegionNumbers
            Dim simName = Form1.RegionClass.RegionName(RegionNum)
            Form1.MySetting.LoadOtherIni(Form1.RegionClass.RegionPath(RegionNum), ";")
            Form1.MySetting.SetOtherIni(simName, "InternalPort", Portnumber.ToString)
            Form1.RegionClass.RegionPort(RegionNum) = Portnumber
            ' Self setting Region Ports
            Form1.gMaxPortUsed = Portnumber
            Form1.MySetting.SaveOtherINI()
            Portnumber = Portnumber + 1
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

                ' This search returns the substring between two strings, so 
                ' the first index Is moved to the character just after the first string.
                Dim POST As String = Uri.UnescapeDataString(ProcessString)
                Dim first As Integer = POST.IndexOf("{")
                Dim last As Integer = POST.LastIndexOf("}")
                Dim rawJSON = POST.Substring(first, last - first + 1)
                WebserverList.RemoveAt(LOOPVAR)

                Try
                    json = JsonConvert.DeserializeObject(Of JSON_result)(rawJSON)
                Catch ex As Exception
                    Debug.Print(ex.Message)
                    Continue For
                    Return
                End Try

                '		rawJSON	"{""alert"":""region_ready"",""login"":""disabled"",""region_name"":""Welcome"",""region_id"":""365d804a-0df1-46cf-8acf-4320a3df3fca""}"	String
                '       rawJSON "{""alert"":""region_ready"",""login"":""enabled"",""region_name"":""Welcome"",""region_id"":""365d804a-0df1-46cf-8acf-4320a3df3fca""}"	String
                '		rawJSON	"{""alert"":""region_ready"",""login"":""shutdown"",""region_name"":""Welcome"",""region_id"":""365d804a-0df1-46cf-8acf-4320a3df3fca""}"	String

                If json.login = "enabled" Then
                    Form1.Print("Region " & json.region_name & " is ready")

                    Dim n = FindRegionByName(json.region_name)
                    If n < 0 Then
                        Return
                    End If

                    RegionEnabled(n) = True
                    Status(n) = SIM_STATUS.Booted
                    UUID(n) = json.region_id

                    Form1.UpdateView() = True

                    If Form1.MySetting.ConsoleShow = False Then
                        Dim hwnd = Form1.GetHwnd(GroupName(n))
                        Form1.ShowDOSWindow(hwnd, Form1.SHOW_WINDOW.SW_MINIMIZE)
                    End If


                ElseIf json.login = "shutdown" Then

                    Return ' does not work as expected

                    Form1.Print("Region " & json.region_name & " shutdown")

                    Dim n = FindRegionByName(json.region_name)
                    If n < 0 Then
                        Return
                    End If
                    Timer(n) = REGION_TIMER.Stopped
                    If Status(n) = SIM_STATUS.RecyclingDown Then
                        Status(n) = SIM_STATUS.RestartPending
                        Form1.UpdateView = True ' make form refresh
                    Else
                        Status(n) = SIM_STATUS.Stopped
                    End If

                    UUID(n) = ""
                    Form1.UpdateView() = True
                    Form1.ExitList.Add(json.region_name)

                End If

            Catch ex As Exception
                Debug.Print(ex.Message)
            End Try
        Next

    End Sub

#End Region

#Region "POST"

    Public Function ParsePost(POST As String, MySetting As MySettings) As String


        ' set Region.Booted to true if the POST from the region indicates it is online
        ' requires a section in Opensim.ini where [RegionReady] has this:


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
        '{"alert":"region_ready","login":"enabled","region_name":"Region 2","region_id":"19f6adf0-5f35-4106-bcb8-dc3f2e846b89"}}
        'POST / Region%202 HTTP/1.1
        'Content-Type: Application/ json
        'Host:   tea.outworldz.net : 8001
        'Content-Length:  118
        'Connection: Keep-Alive
        '
        '{"alert":"region_ready","login":"enabled","region_name":"Region 2","region_id":"19f6adf0-5f35-4106-bcb8-dc3f2e846b89"}

        ' we want region name, UUID and server_startup
        ' could also be a probe from the outworldz to check if ports are open.

        ' WarmingUp(0) = True
        ' ShuttingDown(1) = True

        ' alerts need to be fast so we stash them on a list and process them on a 10 second timer.

        If (POST.Contains("""alert"":""region_ready""")) Then

            WebserverList.Add(POST)

            ' proto for testing AutoStart Region mode
        ElseIf POST.Contains("UUID") Then
            Debug.Print("UUID:" + POST)
            Return POST

            ' currently unused as is only in standalones
        ElseIf POST.Contains("TOS") Then
            Debug.Print("UUID:" + POST)
            '"POST /TOS HTTP/1.1" & vbCrLf & "Host: mach.outworldz.net:9201" & vbCrLf & "Connection: keep-alive" & vbCrLf & "Content-Length: 102" & vbCrLf & "Cache-Control: max-age=0" & vbCrLf & "Upgrade-Insecure-Requests: 1" & vbCrLf & "User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.99 Safari/537.36" & vbCrLf & "Origin: http://mach.outworldz.net:9201" & vbCrLf & "Content-Type: application/x-www-form-urlencoded" & vbCrLf & "DNT: 1" & vbCrLf & "Accept: text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8" & vbCrLf & "Referer: http://mach.outworldz.net:9200/wifi/termsofservice.html?uid=acb8fd92-c725-423f-b750-5fd971d73182&sid=40c5b80a-5377-4b97-820c-a0952782a701" & vbCrLf & "Accept-Encoding: gzip, deflate" & vbCrLf & "Accept-Language: en-US,en;q=0.9" & vbCrLf & vbCrLf & 
            '"action-accept=Accept&uid=acb8fd92-c725-423f-b750-5fd971d73182&sid=40c5b80a-5377-4b97-820c-a0952782a701"

            If Not Form1.MySetting.StandAlone() Then
                Return "<html><head></head><body>Error</html>"
            End If

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

                    ' Only works in Standalone, anyway.
                    ' Not implemented at all in Grid mode - the Diva DLL Diva is stubbed off.
                    Dim result As Integer = 1

                    Dim Str As String = "server=" + MySetting.RobustServer _
                            + ";database=" + MySetting.RobustDataBaseName _
                            + ";port=" + MySetting.MySqlPort _
                            + ";user=" + MySetting.RobustUsername _
                            + ";password=" + MySetting.RobustPassword _
                            + ";Old Guids=true;Allow Zero Datetime=true;"

                    Dim myConnection As MySqlConnection = New MySqlConnection(Str)

                    Dim Query1 = "update opensim.griduser set TOS = 1 where UserID = @p1; "
                    Dim myCommand1 As MySqlCommand = New MySqlCommand(Query1)
                    myCommand1.Connection = myConnection
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
            Dim PWok As Boolean = CheckPassword(POST, MySetting.MachineID().ToLower)
            If Not PWok Then Return ""

            Dim pattern1 As Regex = New Regex("User=(.*)")
            Dim match1 As Match = pattern1.Match(POST)
            Dim p1 As String = ""
            If match1.Success Then
                p1 = match1.Groups(1).Value
                Dim s = GetPartner(p1, MySetting)
                Debug.Print(s)
                Return s
            Else
                Debug.Print("No partner")
                Return "00000000-0000-0000-0000-000000000000"
            End If

            ' Partner prim
        ElseIf POST.Contains("set_partner") Then
            Debug.Print("set Partner")
            Dim PWok As Boolean = CheckPassword(POST, MySetting.MachineID().ToLower)
            If Not PWok Then Return ""

            Dim pattern1 As Regex = New Regex("User=(.*?)&")
            Dim match1 As Match = pattern1.Match(POST)
            If match1.Success Then
                Dim p1 As String = ""
                Dim p2 As String = ""
                p1 = match1.Groups(1).Value
                Dim pattern2 As Regex = New Regex("Partner=(.*)")
                Dim match2 As Match = pattern2.Match(POST)
                If match2.Success Then
                    p2 = match2.Groups(1).Value
                End If
                Dim result As New Guid
                If Guid.TryParse(p1, result) And Guid.TryParse(p1, result) Then
                    Try

                        Dim Partner = GetPartner(p1, MySetting)
                        Debug.Print("Partner=" + p2)

                        Dim Str As String = "server=" + MySetting.RobustServer _
                            + ";database=" + MySetting.RobustDataBaseName _
                            + ";port=" + MySetting.MySqlPort _
                            + ";user=" + MySetting.RobustUsername _
                            + ";password=" + MySetting.RobustPassword _
                            + ";Old Guids=true;Allow Zero Datetime=true;"

                        Dim myConnection As MySqlConnection = New MySqlConnection(Str)

                        Dim Query1 = "update robust.userprofile set profilepartner=@p2 where userUUID = @p1; "
                        Dim myCommand1 As MySqlCommand = New MySqlCommand(Query1)
                        myCommand1.Connection = myConnection
                        myConnection.Open()
                        myCommand1.Prepare()

                        myCommand1.Parameters.AddWithValue("p1", p1)
                        myCommand1.Parameters.AddWithValue("p2", p2)

                        myCommand1.ExecuteScalar()
                        myConnection.Close()
                        Debug.Print(Partner)
                        Return Partner
                    Catch ex As Exception
                        Debug.Print(ex.Message)
                    End Try
                End If
            End If
            Debug.Print("NULL response")
            Return ""

        Else
            Return "Test Completed"
        End If

        Return ""

    End Function

    Public Function RegionListHTML(Setting As MySettings) As String

        'redirect from http://localhost:8002/bin/data/teleports.htm
        'to http://localhost:8001/teleports.htm
        'Outworldz|Welcome||www.outworldz.com:9000:Welcome|128,128,96|
        '*|Welcome||www.outworldz.com9000Welcome|128,128,96|
        Dim HTML As String

        HTML = "Welcome to |" + Setting.SimName + "||" + Setting.PublicIP + ":" + Setting.HttpPort + ":" + Setting.WelcomeRegion + "||" + vbCrLf
        Dim ToSort As New List(Of String)
        For Each X As Integer In RegionNumbers()
            If IsBooted(X) And Teleport(X) = "True" Then
                ToSort.Add(RegionName(X))
            End If
        Next

        ToSort.Sort()

        For Each S As String In ToSort
            Dim X = FindRegionByName(S)
            HTML = HTML + "*|" + RegionName(X) + "||" + Setting.PublicIP + ":" + Setting.HttpPort + ":" + RegionName(X) + "||" + vbCrLf
        Next

        Return HTML

    End Function

    Function Right(value As String, length As Integer) As String
        ' Get rightmost characters of specified length.
        Return value.Substring(value.Length - length)
    End Function

    Function CheckPassword(POST As String, Machine As String) As Boolean

        ' Returns true is password is blank or matching
        Dim pattern1 As Regex = New Regex("PW=(.*?)&")
        Dim match1 As Match = pattern1.Match(POST)
        If match1.Success Then
            Dim p1 As String = match1.Groups(1).Value
            If p1 = "" Then Return True
            If Machine = p1.ToLower Then Return True
        End If
        Return False

    End Function

    Function GetPartner(p1 As String, Mysetting As MySettings) As String

        Dim Str As String = "server=" + Mysetting.RobustServer _
                                + ";database=" + Mysetting.RobustDataBaseName _
                                + ";port=" + Mysetting.MySqlPort _
                                + ";user=" + Mysetting.RobustUsername _
                                + ";password=" + Mysetting.RobustPassword _
                                + ";Old Guids=true;Allow Zero Datetime=true;"

        Dim myConnection As MySqlConnection = New MySqlConnection(Str)
        Dim Query1 = "Select profilepartner from robust.userprofile where userUUID=@p1;"
        Dim myCommand1 As MySqlCommand = New MySqlCommand(Query1)
        myCommand1.Connection = myConnection
        myConnection.Open()
        myCommand1.Prepare()
        myCommand1.Parameters.AddWithValue("p1", p1)
        Dim a = Convert.ToString(myCommand1.ExecuteScalar())
        Debug.Print("User=" + p1 + ", Partner=" + a)

        myConnection.Close()
        Return a

    End Function
#End Region

End Class
