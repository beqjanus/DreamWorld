Imports System.IO
Imports IniParser

Public Class MySettings

    Dim parser As FileIniDataParser
    Dim Myparser As FileIniDataParser
    Dim INI As String = ""
    Dim Data As IniParser.Model.IniData
    Dim MyData As IniParser.Model.IniData
    Dim myINI As String = ""
    Dim gFolder As String
#Disable Warning IDE0044 ' Add readonly modifier
    Dim Apachein As New List(Of String)
    Dim Apacheout As New List(Of String)
#Enable Warning IDE0044 ' Add readonly modifier

#Region "New"
    Public Sub New()

        parser = New FileIniDataParser()
        parser.Parser.Configuration.SkipInvalidLines = True
        parser.Parser.Configuration.AssigmentSpacer = ""

    End Sub

    Public Sub Init(Folder As String)
        gFolder = Folder
        myINI = Folder + "\OutworldzFiles\Settings.ini"
        If File.Exists(myINI) Then
            LoadMyIni()
        Else
            myINI = Folder + "\OutworldzFiles\Settings.ini"
            Dim contents = "[Data]" + vbCrLf
            Using outputFile As New StreamWriter(myINI, False)
                outputFile.WriteLine(contents)
            End Using

            LoadMyIni()

            AdminFirst() = My.Settings.AdminFirst
            AdminLast() = My.Settings.AdminLast
            AdminEmail() = My.Settings.AdminEmail
            AutoBackup() = My.Settings.AutoBackup
            AutobackupInterval() = My.Settings.AutobackupInterval
            AutoLoad() = My.Settings.AutoLoad
            Autostart() = My.Settings.Autostart
            AccountConfirmationRequired() = My.Settings.AccountConfirmationRequired

            BackupFolder() = My.Settings.BackupFolder
            BootStart() = My.Settings.BootStart

            ChatTime() = My.Settings.ChatTime
            Clouds() = False    ' does not exist in old code, so set it off
            Density() = 0.5
            ConsoleUser() = My.Settings.ConsoleUser
            ConsolePass() = My.Settings.ConsolePass
            CoordX() = My.Settings.CoordX
            CoordY() = My.Settings.CoordY
            ConsoleShow = My.Settings.ConsoleShow

            DiagFailed() = My.Settings.DiagFailed

            DiagnosticPort() = My.Settings.DiagnosticPort
            DNSName() = My.Settings.DnsName

            EnableHypergrid() = My.Settings.EnableHypergrid

            GLProdSecret() = My.Settings.GLProdSecret
            GLProdKey() = My.Settings.GLProdKey
            GLBOwnerName() = My.Settings.GLBOwnerName
            GLBOwnerEmail() = My.Settings.GLBOwnerEmail
            GLSandSecret() = My.Settings.GLSandSecret
            GLSandKey() = My.Settings.GLSandKey
            GloebitsMode() = My.Settings.GloebitsMode
            GloebitsEnable() = My.Settings.GloebitsEnable

            HttpPort() = My.Settings.HttpPort

            ImageNum() = My.Settings.ImageNum

            KeepForDays() = My.Settings.KeepForDays

            LoopBackDiag() = My.Settings.LoopBackDiag

            MapType() = My.Settings.MapType

            MySqlPort() = My.Settings.MySqlPort
            MyX() = My.Settings.MyX
            MyY() = My.Settings.MyY

            Password() = My.Settings.Password
            Physics() = My.Settings.Physics.ToString
            PrivatePort() = My.Settings.PrivatePort
            PublicIP() = My.Settings.PublicIP

            Allow_grid_gods() = CType(My.Settings.allow_grid_gods, Boolean)
            Region_owner_is_god() = My.Settings.region_owner_is_god
            Region_manager_is_god() = My.Settings.region_manager_is_god

            RanAllDiags() = My.Settings.RanAllDiags

            RegionDBName() = My.Settings.RegionDBName
            RegionDbPassword() = My.Settings.RegionDbPassword
            RegionDBUsername() = My.Settings.RegionDBUsername

            ' Robust
            RobustServer() = My.Settings.RobustServer
            RobustPassword() = My.Settings.RobustPassword
            RobustUsername() = My.Settings.RobustUsername
            RobustDataBaseName() = My.Settings.RobustDataBaseName
            RunOnce() = My.Settings.RunOnce

            SC_Enable() = False
            SC_PortBase() = 8080
            SC_PortBase1() = 8081
            SC_Password() = "A password"
            SC_AdminPassword() = "Admin Password"
            SC_Show() = True

            SizeX() = My.Settings.SizeX
            SizeY() = My.Settings.SizeY
            SimName() = My.Settings.SimName
            SkipUpdateCheck() = My.Settings.SkipUpdateCheck

            'email
            SmtpHost() = "smtp.gmail.com"
            SmtpPort() = "587"
            SmtpUsername() = My.Settings.SmtpUsername
            SmtpPassword() = My.Settings.SmtpPassword

            SplashPage() = My.Settings.SplashPage

            TimerInterval() = My.Settings.TimerInterval

            UPnPEnabled() = My.Settings.UPnPEnabled
            UPnpDiag() = My.Settings.UPnpDiag

            VivoxEnabled = My.Settings.VivoxEnabled
            Vivox_UserName() = My.Settings.Vivox_username
            Vivox_password() = My.Settings.Vivox_password

            WelcomeRegion() = My.Settings.WelcomeRegion
            WifiEnabled() = My.Settings.WifiEnabled

        End If

        ' new  vars have to exist before we can read them, and this hack is the only way to set this is to test if they do exist

        Try
            Dim x = Clouds()
        Catch ex As Exception
            Clouds() = False
            Density() = 0.5
        End Try

        Try
            Dim x = Allow_grid_gods()
        Catch ex As Exception
            Allow_grid_gods() = False
        End Try

        Try
            Dim x = GDPR()
        Catch ex As Exception
            GDPR() = False
        End Try

        Try
            Dim x = Suitcase()
        Catch ex As Exception
            Suitcase() = True
        End Try

        Try
            Dim x = Primlimits()
        Catch ex As Exception
            Primlimits() = False
        End Try

        Try
            Dim x = ShowToLocalUsers()
        Catch ex As Exception
            ShowToLocalUsers() = False
        End Try

        Try
            Dim x = ShowToForeignUsers()
        Catch ex As Exception
            ShowToForeignUsers() = False
        End Try

        Try
            Dim x = TOSEnabled()
        Catch ex As Exception
            TOSEnabled() = False
        End Try

        Try
            Dim x = StandAlone()
        Catch ex As Exception
            StandAlone() = False
        End Try

        Try
            Dim x = AutoRestartInterval()
        Catch ex As Exception
            AutoRestartInterval() = 0
        End Try


        Try
            Dim x = LSL_HTTP()
        Catch ex As Exception
            LSL_HTTP() = False
        End Try

        '=============== Tides
        Try
            Dim x As Boolean = TideEnabled()
        Catch ex As Exception
            TideEnabled() = False
        End Try

        Try
            Dim x As String = TideHighLevel()
            If x = "" Then TideHighLevel() = "20"
        Catch ex As Exception
            TideHighLevel() = "20"
        End Try

        Try
            Dim x As String = TideLowLevel()
            If x = "" Then TideLowLevel() = "17"
        Catch ex As Exception
            TideLowLevel() = "17"
        End Try

        Try
            Dim x As Boolean = TideInfoDebug()
        Catch ex As Exception
            TideInfoDebug() = False
        End Try

        Try
            Dim x As String = CycleTime()
            If x = "" Then CycleTime() = "900"
        Catch ex As Exception
            CycleTime() = "900"
        End Try

        Try
            Dim x As Boolean = BroadcastTideInfo()
        Catch ex As Exception
            BroadcastTideInfo() = False
        End Try

        Try
            Dim x = TideInfoChannel()
            If x = "" Then TideInfoChannel() = "5555"
        Catch ex As Exception
            TideInfoChannel() = "5555"
        End Try

        Try
            Dim x = TideLevelChannel()
            If x = "" Then TideLevelChannel() = "5556"
        Catch ex As Exception
            TideLevelChannel() = "5556"
        End Try

        'Birds: this is the default and determines whether the module does anything
        Try
            Dim x = BirdsModuleStartup()
        Catch ex As Exception
            BirdsModuleStartup() = False
        End Try

        Try
            Dim x = BirdsEnabled()
        Catch ex As Exception
            BirdsEnabled() = False
        End Try

        'the number of birds to flock
        Try
            Dim x = BirdsFlockSize()
            If x = "" Then BirdsFlockSize() = "25"
        Catch ex As Exception
            BirdsFlockSize() = "25"
        End Try

        'which channel do we listen on for in world commands
        Try
            Dim x = BirdsChatChannel()
            If x = 0 Then BirdsChatChannel() = 118
        Catch ex As Exception
            BirdsChatChannel() = 118
        End Try

        'how far each bird can travel per update
        Try
            Dim x = BirdsMaxSpeed()
            If x = 0 Then BirdsMaxSpeed() = 1.0
        Catch ex As Exception
            BirdsMaxSpeed() = 1.0
        End Try

        'the maximum acceleration allowed to the current velocity of the bird
        Try
            Dim x = BirdsMaxForce()
            If x = 0 Then BirdsMaxForce() = 0.2
        Catch ex As Exception
            BirdsMaxForce() = 0.2
        End Try

        'max distance for other birds to be considered in the same flock as us
        Try
            Dim x = BirdsNeighbourDistance()
            If x = 0 Then BirdsNeighbourDistance() = 25
        Catch ex As Exception
            BirdsNeighbourDistance() = 25
        End Try

        'how far away from other birds we would like to stay
        Try
            Dim x = BirdsDesiredSeparation()
            If x = 0 Then BirdsDesiredSeparation() = 5
        Catch ex As Exception
            BirdsDesiredSeparation() = 5
        End Try

        'how close to the edges of things can we get without being worried
        Try
            Dim x = BirdsTolerance()
            If x = 0 Then BirdsTolerance() = 25
        Catch ex As Exception
            BirdsTolerance() = 25
        End Try

        'how close to the edge of a region can we get?
        Try
            Dim x = BirdsBorderSize()
            If x = 0 Then BirdsBorderSize() = 25
        Catch ex As Exception
            BirdsBorderSize() = 25
        End Try

        'how high are we allowed to flock
        Try
            Dim x = BirdsMaxHeight()
            If x = 0 Then BirdsMaxHeight() = 45
        Catch ex As Exception
            BirdsMaxHeight() = 45
        End Try

        'By default the module will create a flock of plain wooden spheres, however this can be overridden to the name of an existing prim that
        ' needs to already exist in the scene - i.e. be rezzed in the region.

        If BirdsPrim() = "" Then
            BirdsPrim() = "SeaGull1"
        End If

        ' check for default
        If (SmtpHost() = "") Then SmtpHost() = "smtp.gmail.com"
        If (SmtpPort() = "") Then SmtpPort() = "587"

        If Theme() = "" Then
            Theme() = "White"
            Form1.CopyWifi("White")
        ElseIf Theme() = "Black" Then
            Form1.CopyWifi("Black")
        ElseIf Theme() = "White" Then
            Form1.CopyWifi("White")
        ElseIf Theme() = "Custom" Then
            Form1.CopyWifi("Custom")
        End If

        'Boot sims one right after the other
        Try
            Dim x = Sequential()
        Catch ex As Exception
            Sequential() = False
        End Try

        Try
            Dim x = RegionListView()
        Catch ex As Exception
            RegionListView() = 2
        End Try

        Try
            Dim x = RegionListVisible()
        Catch ex As Exception
            RegionListVisible() = False
        End Try

        Try
            Dim x = ApacheService()
        Catch ex As Exception
            ApacheService() = True
        End Try


        Try
            Dim x = ApacheEnable()
        Catch ex As Exception
            ApacheEnable() = False
        End Try

        Try
            Dim x = SearchInstalled()
        Catch ex As Exception
            SearchInstalled() = False
        End Try

        Try
            Dim x = RestartOnCrash()
        Catch ex As Exception
            RestartOnCrash() = True
        End Try

        Try
            Dim x = RestartonPhysics()
        Catch ex As Exception
            RestartonPhysics() = True
        End Try

        Try
            Dim x = SearchLocal()
        Catch ex As Exception
            SearchLocal() = False
        End Try

        Try
            Dim x = DeleteScriptsOnStartup()
        Catch ex As Exception
            DeleteScriptsOnStartup() = False
        End Try

    End Sub


#End Region

#Region "Functions And Subs"

    Public Sub SetOtherIni(section As String, key As String, value As String)

        ' sets values into any INI file
        Try
            Form1.Log("Info", "Writing section [" + section + "] " + key + "=" + value)
            Data(section)(key) = value ' replace it 
        Catch ex As Exception
            Form1.ErrorLog(ex.Message)
        End Try

    End Sub

    Public Sub SetMyIni(section As String, key As String, value As String)

        ' sets values into any INI file
        Try
            Form1.Log("Info", "Writing section [" + section + "] " + key + "=" + value)
            MyData(section)(key) = value ' replace it 
        Catch ex As Exception
            Form1.ErrorLog(ex.Message)
        End Try

    End Sub

    Public Sub LoadMyIni()

        Myparser = New FileIniDataParser()

        Myparser.Parser.Configuration.SkipInvalidLines = True
        parser.Parser.Configuration.AssigmentSpacer = ""
        Myparser.Parser.Configuration.CommentString = ";" ' Opensim uses semicolons
        Try
            Form1.Log("Info", "Loading Settings.ini")
            MyData = Myparser.ReadFile(gFolder + "\OutworldzFiles\Settings.ini", System.Text.Encoding.ASCII)
        Catch ex As Exception
            Form1.ErrorLog("Failed to load Settings.ini")
        End Try

    End Sub

    Public Sub LoadOtherIni(arg As String, comment As String)

        parser = New FileIniDataParser()

        parser.Parser.Configuration.SkipInvalidLines = True
        parser.Parser.Configuration.AssigmentSpacer = ""
        parser.Parser.Configuration.CommentString = comment ' Opensim uses semicolons
        Try

            Data = parser.ReadFile(arg, System.Text.Encoding.ASCII)
        Catch ex As Exception
            MsgBox("Error in arg:" + ex.Message)
        End Try
        INI = arg
    End Sub

#End Region

#Region "GetSet"


    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="section"></param>
    ''' <returns></returns>
    ''' 
    Public Function GetIni(section As String, key As String, Optional D As String = "") As String

        Dim R = Stripqq(Data(section)(key))
        If R = Nothing Then R = D
        Return R

    End Function

    Public Function GetMyIni(section As String, key As String, Optional D As String = "") As String

        Dim R = Stripqq(MyData(section)(key))
        If R = Nothing Then R = D
        Return R

    End Function

    Public Sub SaveOtherINI()

        Try
            parser.WriteFile(INI, Data, System.Text.Encoding.ASCII)
        Catch ex As Exception
            Form1.ErrorLog("Error:" + ex.Message)
        End Try

    End Sub

    Public Sub SaveSettings()

        Try
            Myparser.WriteFile(myINI, MyData, System.Text.Encoding.ASCII)
        Catch ex As Exception
            MsgBox("Unable to save settings to " + myINI)
            Form1.ErrorLog("Error:" + ex.Message)
        End Try

    End Sub

    Public Function Random() As String

        Dim value As Integer = CInt(Int((600000000 * Rnd()) + 1))
        Random = System.Convert.ToString(value)

    End Function

    Private Function Stripqq(input As String) As String

        Return Replace(input, """", "")

    End Function

    Public Function GetMySetting(key As String, Optional D As String = "") As String
        Try
            Dim value = GetMyIni("Data", key, D)
            Return value
        Catch
            Return ""
        End Try

    End Function
    Public Sub SetMySetting(key As String, value As String)

        SetMyIni("Data", key, value)

    End Sub


#End Region

#Region "Properties"


    Public Property DeleteScriptsOnStartup() As Boolean
        Get
            Return CType(GetMySetting("DeleteScriptsOnStartup"), Boolean)
        End Get
        Set
            SetMySetting("DeleteScriptsOnStartup", Value.ToString)
        End Set
    End Property

    Public Property ServerType() As String
        Get
            Return GetMySetting("ServerType", "Robust")
        End Get
        Set
            SetMySetting("ServerType", Value)
        End Set
    End Property


    Public Property SearchLocal() As Boolean
        Get
            Return CType(GetMySetting("SearchLocal"), Boolean)
        End Get
        Set
            SetMySetting("SearchLocal", Value.ToString)
        End Set
    End Property
    Public Property RestartOnCrash() As Boolean
        Get
            Return CType(GetMySetting("RestartOnCrash"), Boolean)
        End Get
        Set
            SetMySetting("RestartOnCrash", Value.ToString)
        End Set
    End Property

    Public Property RestartonPhysics() As Boolean
        Get
            Return CType(GetMySetting("RestartonPhysics"), Boolean)
        End Get
        Set
            SetMySetting("RestartonPhysics", Value.ToString)
        End Set
    End Property

    Public Property MapCenterY() As String
        Get
            Return GetMySetting("MapCenterY", "1000")
        End Get
        Set
            SetMySetting("MapCenterY", Value)
        End Set
    End Property

    Public Property MapCenterX() As String
        Get
            Return GetMySetting("MapCenterX", "1000")
        End Get
        Set
            SetMySetting("MapCenterX", Value)
        End Set
    End Property


    Public Property SearchInstalled() As Boolean
        Get
            Return CType(GetMySetting("SearchInstalled"), Boolean)
        End Get
        Set
            SetMySetting("SearchInstalled", Value.ToString)
        End Set
    End Property

    Public Property ApachePort() As String
        Get
            Return GetMySetting("ApachePort", "80")
        End Get
        Set
            SetMySetting("ApachePort", Value)
        End Set
    End Property

    Public Property ApacheService() As Boolean
        Get
            Return CType(GetMySetting("ApacheService"), Boolean)
        End Get
        Set
            SetMySetting("ApacheService", Value.ToString)
        End Set
    End Property

    Public Property ApacheEnable() As Boolean
        Get
            Return CType(GetMySetting("ApacheEnabled"), Boolean)
        End Get
        Set
            SetMySetting("ApacheEnabled", Value.ToString)
        End Set
    End Property

    Public Property RegionListVisible() As Boolean
        Get
            Return CType(GetMySetting("RegionListVisible"), Boolean)
        End Get
        Set
            SetMySetting("RegionListVisible", Value.ToString)
        End Set
    End Property
    Public Property RegionListView() As Integer
        Get
            Return CType(GetMySetting("RegionListView"), Integer)
        End Get
        Set
            SetMySetting("RegionListView", Value.ToString)
        End Set
    End Property
    Public Property Sequential() As Boolean
        Get
            Return CType(GetMySetting("Sequential"), Boolean)
        End Get
        Set
            SetMySetting("Sequential", Value.ToString)
        End Set
    End Property

    Public Property HomeVectorX() As String
        Get
            Return GetMySetting("HomeVectorX", "128")
        End Get
        Set
            SetMySetting("HomeVectorX", Value)
        End Set
    End Property
    Public Property HomeVectorY() As String
        Get
            Return GetMySetting("HomeVectorY", "128")
        End Get
        Set
            SetMySetting("HomeVectorY", Value)
        End Set
    End Property
    Public Property HomeVectorZ() As String
        Get
            Return GetMySetting("HomeVectorZ", "24")
        End Get
        Set
            SetMySetting("HomeVectorZ", Value)
        End Set
    End Property

    Public Property RegionPort() As String
        Get
            Return GetMySetting("RegionPort", MySqlPort())
        End Get
        Set
            SetMySetting("RegionPort", Value)
        End Set
    End Property

    Public Property RegionServer() As String
        Get
            Return GetMySetting("RegionServer", "127.0.0.1")
        End Get
        Set
            SetMySetting("RegionServer", Value)
        End Set
    End Property

    Public Property Theme() As String
        Get
            Return GetMySetting("Theme")
        End Get
        Set
            SetMySetting("Theme", Value)
        End Set
    End Property

    ' Tides
    Public Property TideInfoDebug() As Boolean
        Get
            Return CType(GetMySetting("TideInfoDebug"), Boolean)
        End Get
        Set
            SetMySetting("TideInfoDebug", Value.ToString)
        End Set
    End Property

    Public Property TideEnabled() As Boolean
        Get
            Return CType(GetMySetting("TideEnabled"), Boolean)
        End Get
        Set
            SetMySetting("TideEnabled", Value.ToString)
        End Set
    End Property

    Public Property TideHighLevel() As String
        Get
            Return GetMySetting("TideHighLevel")
        End Get
        Set
            SetMySetting("TideHighLevel", Value)
        End Set
    End Property

    Public Property TideLowLevel() As String
        Get
            Return GetMySetting("TideLowLevel")
        End Get
        Set
            SetMySetting("TideLowLevel", Value)
        End Set
    End Property

    Public Property CycleTime() As String
        Get
            Return GetMySetting("CycleTime")
        End Get
        Set
            SetMySetting("CycleTime", Value)
        End Set
    End Property

    Public Property BroadcastTideInfo() As Boolean
        Get
            Return CType(GetMySetting("BroadcastTideInfo"), Boolean)
        End Get
        Set
            SetMySetting("BroadcastTideInfo", Value.ToString)
        End Set
    End Property
    Public Property TideInfoChannel() As String
        Get
            Return GetMySetting("TideInfoChannel")
        End Get
        Set
            SetMySetting("TideInfoChannel", Value)
        End Set
    End Property
    Public Property TideLevelChannel() As String
        Get
            Return GetMySetting("TideLevelChannel")
        End Get
        Set
            SetMySetting("TideLevelChannel", Value)
        End Set
    End Property

    ' more stuff

    Public Property FirstRegionPort() As String
        Get
            Return GetMySetting("FirstRegionPort")
        End Get
        Set
            SetMySetting("FirstRegionPort", Value)
        End Set
    End Property

    Public Property Myfolder() As String
        Get
            Return GetMySetting("Myfolder")
        End Get
        Set
            SetMySetting("Myfolder", Value)
        End Set
    End Property

    Public Property BirdsModuleStartup() As Boolean
        Get
            Return CType(GetMySetting("BirdsModuleStartup"), Boolean)
        End Get
        Set
            SetMySetting("BirdsModuleStartup", Value.ToString)
        End Set
    End Property


    Public Property BirdsEnabled() As Boolean
        Get
            Return CType(GetMySetting("BirdsEnabled"), Boolean)
        End Get
        Set
            SetMySetting("BirdsEnabled", Value.ToString)
        End Set
    End Property


    Public Property BirdsFlockSize() As String
        Get
            Return GetMySetting("BirdsFlockSize")
        End Get
        Set
            SetMySetting("BirdsFlockSize", Value)
        End Set
    End Property
    Public Property BirdsChatChannel() As Integer
        Get
            Return CType(GetMySetting("BirdsChatChannel"), Integer)
        End Get
        Set
            SetMySetting("BirdsChatChannel", Value.ToString)
        End Set
    End Property
    Public Property BirdsMaxSpeed() As Double
        Get
            Return CType(GetMySetting("BirdsMaxSpeed"), Double)
        End Get
        Set
            SetMySetting("BirdsMaxSpeed", Value.ToString)
        End Set
    End Property
    Public Property BirdsMaxForce() As Double
        Get
            Return CType(GetMySetting("BirdsMaxForce"), Double)
        End Get
        Set
            SetMySetting("BirdsMaxForce", Value.ToString)
        End Set
    End Property
    Public Property BirdsNeighbourDistance() As Double
        Get
            Return CType(GetMySetting("BirdsNeighbourDistance"), Double)
        End Get
        Set
            SetMySetting("BirdsNeighbourDistance", Value.ToString)
        End Set
    End Property
    Public Property BirdsDesiredSeparation() As Double
        Get
            Return CType(GetMySetting("BirdsDesiredSeparation"), Double)
        End Get
        Set
            SetMySetting("BirdsDesiredSeparation", Value.ToString)
        End Set
    End Property
    Public Property BirdsTolerance() As Double
        Get
            Return CType(GetMySetting("BirdsTolerance"), Double)
        End Get
        Set
            SetMySetting("BirdsTolerance", Value.ToString)
        End Set
    End Property
    Public Property BirdsBorderSize() As Double
        Get
            Return CType(GetMySetting("BirdsBorderSize"), Double)
        End Get
        Set
            SetMySetting("BirdsBorderSize", Value.ToString)
        End Set
    End Property
    Public Property BirdsMaxHeight() As Double
        Get
            Return CType(GetMySetting("BirdsMaxHeight"), Double)
        End Get
        Set
            SetMySetting("BirdsMaxHeight", Value.ToString)
        End Set
    End Property
    Public Property BirdsPrim() As String
        Get
            Return GetMySetting("BirdsPrim")
        End Get
        Set
            SetMySetting("BirdsPrim", Value)
        End Set
    End Property


    Public Property LSL_HTTP() As Boolean
        Get
            Return CType(GetMySetting("LSL_HTTP"), Boolean)
        End Get
        Set
            SetMySetting("LSL_HTTP", Value.ToString)
        End Set
    End Property

    Public Property AutoRestartInterval() As Integer
        Get
            Return CType(GetMySetting("AutoRestartInterval"), Integer)
        End Get
        Set
            SetMySetting("AutoRestartInterval", Value.ToString)
        End Set
    End Property
    Public Property StandAlone() As Boolean
        Get
            Return CType(GetMySetting("StandAlone"), Boolean)
        End Get
        Set
            SetMySetting("StandAlone", Value.ToString)
        End Set
    End Property
    Public Property ShowToLocalUsers() As Boolean
        Get
            Return CType(GetMySetting("ShowToLocalUsers"), Boolean)
        End Get
        Set
            SetMySetting("ShowToLocalUsers", Value.ToString)
        End Set
    End Property
    Public Property ShowToForeignUsers() As Boolean
        Get
            Return CType(GetMySetting("ShowToForeignUsers"), Boolean)
        End Get
        Set
            SetMySetting("ShowToForeignUsers", Value.ToString)
        End Set
    End Property
    Public Property TOSEnabled() As Boolean
        Get
            Return CType(GetMySetting("TOSEnabled"), Boolean)
        End Get
        Set
            SetMySetting("TOSEnabled", Value.ToString)
        End Set
    End Property
    Public Property Primlimits() As Boolean
        Get
            Return CType(GetMySetting("Primlimits"), Boolean)
        End Get
        Set
            SetMySetting("Primlimits", Value.ToString)
        End Set
    End Property
    Public Property Suitcase() As Boolean
        Get
            Return CType(GetMySetting("Suitcase"), Boolean)
        End Get
        Set
            SetMySetting("Suitcase", Value.ToString)
        End Set
    End Property
    Public Property GDPR() As Boolean
        Get
            Return CType(GetMySetting("GDPR"), Boolean)
        End Get
        Set
            SetMySetting("GDPR", Value.ToString)
        End Set
    End Property
    Public Property SmtpHost() As String
        Get
            Return CType(GetMySetting("SmtpHost"), String)
        End Get
        Set
            SetMySetting("SmtpHost", Value)
        End Set
    End Property
    Public Property SmtpPort() As String
        Get
            Return CType(GetMySetting("SmtpPort"), String)
        End Get
        Set
            SetMySetting("SmtpPort", Value)
        End Set
    End Property
    Public Property Clouds() As Boolean
        Get
            Return CType(GetMySetting("Clouds"), Boolean)
        End Get
        Set
            SetMySetting("Clouds", Value.ToString)
        End Set
    End Property
    Public Property Density() As Double
        Get
            Return CType(GetMySetting("Density"), Double)
        End Get
        Set
            SetMySetting("Density", Value.ToString)
        End Set
    End Property
    <CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings")>
    Public Property PrivateURL() As String
        Get
            Return GetMySetting("PrivateURL")
        End Get
        Set
            SetMySetting("PrivateURL", Value.ToString)
        End Set
    End Property
    Public Property ConsoleShow() As Boolean
        Get
            Return CType(GetMySetting("ConsoleShow"), Boolean)
        End Get
        Set
            SetMySetting("ConsoleShow", Value.ToString)
        End Set
    End Property
    Public Property AutoBackup() As Boolean
        Get
            Return CType(GetMySetting("AutoBackup"), Boolean)
        End Get
        Set
            SetMySetting("AutoBackup", Value.ToString)
        End Set
    End Property
    Public Property PublicIP() As String
        Get
            Return CType(GetMySetting("PublicIP"), String)
        End Get
        Set
            SetMySetting("PublicIP", Value)
        End Set
    End Property
    Public Property CoordX() As String
        Get
            Return CType(GetMySetting("CoordX"), String)
        End Get
        Set
            SetMySetting("CoordX", Value)
        End Set
    End Property
    Public Property CoordY() As String
        Get
            Return CType(GetMySetting("CoordY"), String)
        End Get
        Set
            SetMySetting("CoordY", Value)
        End Set
    End Property
    Public Property PrivatePort() As String
        Get
            Return CType(GetMySetting("PrivatePort"), String)
        End Get
        Set
            SetMySetting("PrivatePort", Value)
        End Set
    End Property
    Public Property SizeX() As String
        Get
            Return CType(GetMySetting("SizeX"), String)
        End Get
        Set
            SetMySetting("SizeX", Value)
        End Set
    End Property
    Public Property SizeY() As String
        Get
            Return CType(GetMySetting("SizeY"), String)
        End Get
        Set
            SetMySetting("SizeY", Value)
        End Set
    End Property
    Public Property AutobackupInterval() As String
        Get
            Return CType(GetMySetting("AutobackupInterval"), String)
        End Get
        Set
            SetMySetting("AutobackupInterval", Value)
        End Set
    End Property
    Public Property KeepForDays() As Integer
        Get
            Return CType(GetMySetting("KeepForDays"), Integer)
        End Get
        Set
            SetMySetting("KeepForDays", Value.ToString)
        End Set
    End Property
    Public Property Password() As String
        Get
            Return CType(GetMySetting("Password"), String)
        End Get
        Set
            SetMySetting("Password", Value)
        End Set
    End Property
    Public Property AdminFirst() As String
        Get
            Return CType(GetMySetting("AdminFirst"), String)
        End Get
        Set
            SetMySetting("AdminFirst", Value)
        End Set
    End Property
    Public Property AdminLast() As String
        Get
            Return CType(GetMySetting("AdminLast"), String)
        End Get
        Set
            SetMySetting("AdminLast", Value)
        End Set
    End Property
    Public Property AdminEmail() As String
        Get
            Return CType(GetMySetting("AdminEmail"), String)
        End Get
        Set
            SetMySetting("AdminEmail", Value)
        End Set
    End Property
    Public Property ConsoleUser() As String
        Get
            Return CType(GetMySetting("ConsoleUser"), String)
        End Get
        Set
            SetMySetting("ConsoleUser", Value)
        End Set
    End Property
    Public Property ConsolePass() As String
        Get
            Return CType(GetMySetting("ConsolePass"), String)
        End Get
        Set
            SetMySetting("ConsolePass", Value)
        End Set
    End Property
    Public Property ChatTime() As Integer
        Get
            Try
                Return CType(GetMySetting("ChatTime"), Integer)
            Catch
                Return 1000
            End Try

        End Get
        Set
            SetMySetting("ChatTime", Value.ToString)
        End Set
    End Property
    Public Property ViewerInstalled() As Boolean
        Get
            Return CType(GetMySetting("ViewerInstalled"), Boolean)
        End Get
        Set
            SetMySetting("ViewerInstalled", Value.ToString)
        End Set
    End Property
    Public Property ImageNum() As Integer
        Get
            Return CType(GetMySetting("ImageNum"), Integer)
        End Get
        Set
            SetMySetting("ImageNum", Value.ToString)
        End Set
    End Property
    Public Property Allow_grid_gods() As Boolean
        Get
            Return CType(GetMySetting("Allow_grid_gods"), Boolean)
        End Get
        Set
            SetMySetting("Allow_grid_gods", Value.ToString)
        End Set
    End Property
    Public Property Region_owner_is_god() As Boolean
        Get
            Return CType(GetMySetting("Region_owner_is_god"), Boolean)
        End Get
        Set
            SetMySetting("Region_owner_is_god", Value.ToString)
        End Set
    End Property
    Public Property Region_manager_is_god() As Boolean
        Get
            Return CType(GetMySetting("Region_manager_is_god"), Boolean)
        End Get
        Set
            SetMySetting("Region_manager_is_god", Value.ToString)
        End Set
    End Property
    Public Property TimerInterval() As Integer
        Get
            Try
                Return CType(GetMySetting("TimerInterval"), Integer)
            Catch
                Return 1000
            End Try

        End Get
        Set
            SetMySetting("TimerInterval", Value.ToString)
        End Set
    End Property
    Public Property AccountConfirmationRequired() As Boolean
        Get
            Return CType(GetMySetting("AccountConfirmationRequired"), Boolean)
        End Get
        Set
            SetMySetting("AccountConfirmationRequired", Value.ToString)
        End Set
    End Property
    Public Property SmtpUsername() As String
        Get
            Return GetMySetting("SmtpUsername")
        End Get
        Set
            SetMySetting("SmtpUsername", Value)
        End Set
    End Property
    Public Property SmtpPassword() As String
        Get
            Return GetMySetting("SmtpPassword")
        End Get
        Set
            SetMySetting("SmtpPassword", Value)
        End Set
    End Property
    Public Property RanAllDiags() As Boolean
        Get
            Return CType(GetMySetting("RanAllDiags"), Boolean)
        End Get
        Set
            SetMySetting("RanAllDiags", Value.ToString)
        End Set
    End Property
    Public Property SkipUpdateCheck() As Boolean
        Get
            Return CType(GetMySetting("SkipUpdateCheck"), Boolean)
        End Get
        Set
            SetMySetting("SkipUpdateCheck", Value.ToString)
        End Set
    End Property
    Public Property DiagFailed() As Boolean
        Get
            Return CType(GetMySetting("DiagFailed"), Boolean)
        End Get
        Set
            SetMySetting("DiagFailed", Value.ToString)
        End Set
    End Property
    Public Property DNSName() As String
        Get
            Return GetMySetting("DnsName")
        End Get
        Set
            SetMySetting("DnsName", Value)
        End Set
    End Property
    Public Property HttpPort() As String
        Get
            Return GetMySetting("HttpPort")
        End Get
        Set
            SetMySetting("HttpPort", Value)
        End Set
    End Property
    Public Property MachineID() As String
        Get
            Return GetMySetting("MachineID")
        End Get
        Set
            SetMySetting("MachineID", Value)
        End Set
    End Property
    Public Property LoopBackDiag() As Boolean
        Get
            Return CType(GetMySetting("LoopBackDiag"), Boolean)
        End Get
        Set
            SetMySetting("LoopBackDiag", Value.ToString)
        End Set
    End Property
    Public Property UPnpDiag() As Boolean
        Get
            Return CType(GetMySetting("UPnpDiag"), Boolean)
        End Get
        Set
            SetMySetting("UPnpDiag", Value.ToString)
        End Set
    End Property
    Public Property SplashPage() As String
        Get
            Return GetMySetting("SplashPage")
        End Get
        Set
            SetMySetting("SplashPage", Value)
        End Set
    End Property
    Public Property Physics() As String
        Get
            Return GetMySetting("Physics")
        End Get
        Set
            SetMySetting("Physics", Value)
        End Set
    End Property
    Public Property MyX() As Integer
        Get
            Return CType(GetMySetting("MyX"), Integer)
        End Get
        Set
            SetMySetting("MyX", Value.ToString)
        End Set
    End Property
    Public Property MyY() As Integer
        Get
            Return CType(GetMySetting("MyY"), Integer)
        End Get
        Set
            SetMySetting("MyY", Value.ToString)
        End Set
    End Property
    Public Property RobustServer() As String
        Get
            Return CType(GetMySetting("RobustServer"), String)
        End Get
        Set
            SetMySetting("RobustServer", Value.ToString)
        End Set
    End Property
    Public Property VivoxEnabled() As Boolean
        Get
            Return CType(GetMySetting("VivoxEnabled"), Boolean)
        End Get
        Set
            SetMySetting("VivoxEnabled", Value.ToString)
        End Set
    End Property
    Public Property Vivox_UserName() As String
        Get
            Return GetMySetting("Vivox_username")
        End Get
        Set
            SetMySetting("Vivox_username", Value)
        End Set
    End Property
    Public Property Vivox_password() As String
        Get
            Return GetMySetting("Vivox_password")
        End Get
        Set
            SetMySetting("Vivox_password", Value)
        End Set
    End Property
    Public Property MapType() As String
        Get
            Return GetMySetting("MapType")
        End Get
        Set
            SetMySetting("MapType", Value)
        End Set
    End Property
    Public Property BackupFolder() As String
        Get
            Return GetMySetting("BackupFolder")
        End Get
        Set
            SetMySetting("BackupFolder", Value)
        End Set
    End Property
    Public Property WelcomeRegion() As String
        Get
            Return GetMySetting("WelcomeRegion")
        End Get
        Set
            SetMySetting("WelcomeRegion", Value)
        End Set
    End Property
    Public Property GloebitsEnable() As Boolean
        Get
            Return CType(GetMySetting("GloebitsEnable"), Boolean)
        End Get
        Set
            SetMySetting("GloebitsEnable", Value.ToString)
        End Set
    End Property
    Public Property GloebitsMode() As Boolean
        Get
            Return CType(GetMySetting("GloebitsMode"), Boolean)
        End Get
        Set
            SetMySetting("GloebitsMode", Value.ToString)
        End Set
    End Property
    Public Property GLSandKey() As String
        Get
            Return GetMySetting("GLSandKey")
        End Get
        Set
            SetMySetting("GLSandKey", Value)
        End Set
    End Property

    Public Property GLSandSecret() As String
        Get
            Return GetMySetting("GLSandSecret")
        End Get
        Set
            SetMySetting("GLSandSecret", Value)
        End Set
    End Property
    Public Property GLBOwnerEmail() As String
        Get
            Return GetMySetting("GLBOwnerEmail")
        End Get
        Set
            SetMySetting("GLBOwnerEmail", Value)
        End Set
    End Property
    Public Property GLBOwnerName() As String
        Get
            Return GetMySetting("GLBOwnerName")
        End Get
        Set
            SetMySetting("GLBOwnerName", Value)
        End Set
    End Property
    Public Property GLProdKey() As String
        Get
            Return GetMySetting("GLProdKey")
        End Get
        Set
            SetMySetting("GLProdKey", Value)
        End Set
    End Property
    Public Property MySqlPort() As String
        Get
            Return GetMySetting("MySqlPort")
        End Get
        Set
            SetMySetting("MySqlPort", Value)
        End Set
    End Property
    Public Property GLProdSecret() As String
        Get
            Return GetMySetting("GLProdSecret")
        End Get
        Set
            SetMySetting("GLProdSecret", Value)
        End Set
    End Property
    Public Property RegionDbPassword() As String
        Get
            Return GetMySetting("RegionDbPassword")
        End Get
        Set
            SetMySetting("RegionDbPassword", Value)
        End Set
    End Property
    Public Property RegionDBUsername() As String
        Get
            Return GetMySetting("RegionDBUsername")
        End Get
        Set
            SetMySetting("RegionDBUsername", Value)
        End Set
    End Property
    Public Property RegionDBName() As String
        Get
            Return GetMySetting("RegionDBName")
        End Get
        Set
            SetMySetting("RegionDBName", Value)
        End Set
    End Property
    Public Property WifiEnabled() As Boolean
        Get
            Return CType(GetMySetting("WifiEnabled"), Boolean)
        End Get
        Set
            SetMySetting("WifiEnabled", Value.ToString)
        End Set
    End Property
    Public Property DiagnosticPort() As String
        Get
            Return GetMySetting("DiagnosticPort")
        End Get
        Set
            SetMySetting("DiagnosticPort", Value)
        End Set
    End Property
    Public Property RobustDataBaseName() As String
        Get
            Return GetMySetting("RobustMySqlName")
        End Get
        Set
            SetMySetting("RobustMySqlName", Value)
        End Set
    End Property
    Public Property RobustUsername() As String
        Get
            Return GetMySetting("RobustMySqlUsername")
        End Get
        Set
            SetMySetting("RobustMySqlUsername", Value)
        End Set
    End Property
    Public Property RobustPassword() As String
        Get
            Return GetMySetting("RobustMySqlPassword")
        End Get
        Set
            SetMySetting("RobustMySqlPassword", Value)
        End Set
    End Property
    Public Property SimName() As String
        Get
            Return GetMySetting("SimName")
        End Get
        Set
            SetMySetting("SimName", Value)
        End Set
    End Property
    Public Property UPnPEnabled() As Boolean
        Get
            Return CType(GetMySetting("UPnPEnabled"), Boolean)
        End Get
        Set
            SetMySetting("UPnPEnabled", Value.ToString)
        End Set
    End Property
    Public Property Autostart() As Boolean
        Get
            Return CType(GetMySetting("Autostart"), Boolean)
        End Get
        Set
            SetMySetting("Autostart", Value.ToString)
        End Set
    End Property
    Public Property BootStart() As Boolean
        Get
            Return CType(GetMySetting("BootStart"), Boolean)
        End Get
        Set
            SetMySetting("BootStart", Value.ToString)
        End Set
    End Property
    Public Property EnableHypergrid() As Boolean
        Get
            Return CType(GetMySetting("EnableHypergrid"), Boolean)
        End Get
        Set
            SetMySetting("EnableHypergrid", Value.ToString)
        End Set
    End Property
    Public Property AutoLoad() As Boolean
        Get
            Return CType(GetMySetting("AutoLoad"), Boolean)
        End Get
        Set
            SetMySetting("AutoLoad", Value.ToString)
        End Set
    End Property
    Public Property RunOnce() As Boolean
        Get
            Return CType(GetMySetting("RunOnce"), Boolean)
        End Get
        Set
            SetMySetting("RunOnce", Value.ToString)
        End Set
    End Property
    Public Property SC_Enable() As Boolean
        Get
            Return CType(GetMySetting("SC_Enable"), Boolean)
        End Get
        Set
            SetMySetting("SC_Enable", Value.ToString)
        End Set
    End Property
    Public Property SC_PortBase() As Integer
        Get
            Return CType(GetMySetting("PortBase"), Integer)
        End Get
        Set
            SetMySetting("PortBase", Value.ToString)
        End Set
    End Property
    Public Property SC_PortBase1() As Integer
        Get
            Try ' required as it has been added
                Return CType(GetMySetting("PortBase1"), Integer)
            Catch
                Return 8081
            End Try

        End Get
        Set
            SetMySetting("PortBase1", Value.ToString)
        End Set
    End Property
    Public Property SC_Password() As String
        Get
            Return GetMySetting("SC_Password")
        End Get
        Set
            SetMySetting("SC_Password", Value)
        End Set
    End Property
    Public Property SC_AdminPassword() As String
        Get
            Return GetMySetting("SC_AdminPassword")
        End Get
        Set
            SetMySetting("SC_AdminPassword", Value)
        End Set
    End Property
    Public Property SC_Show() As Boolean
        Get
            Return CType(GetMySetting("SC_Show"), Boolean)
        End Get
        Set
            SetMySetting("SC_Show", Value.ToString)
        End Set
    End Property

#End Region

#Region "Apache"
    ' reader ApacheStrings
    Public Sub LoadApacheIni(ini As String)

        Apachein.Clear()
        Using Reader As New StreamReader(ini, System.Text.Encoding.ASCII)
            While Reader.EndOfStream = False
                Apachein.Add(Reader.ReadLine())
            End While
        End Using

    End Sub

    Public Sub SetApacheIni(Name As String, value As String)

        Apacheout.Clear()

        For Each Item As String In Apachein
            If Item.StartsWith(Name) Then
                Apacheout.Add(Name & " " & value)
            Else
                Apacheout.Add(Item)
            End If
        Next

        Apachein.Clear()
        For Each item In Apacheout
            Apachein.Add(item)
        Next
    End Sub

    'writer of ApacheStrings
    Public Sub SaveApacheINI(ini As String, name As String)

        ' make a backup
        Try
            My.Computer.FileSystem.DeleteFile(ini & ".bak")
        Catch ex As Exception
            Debug.Print(ex.Message)
        End Try

        Try
            My.Computer.FileSystem.RenameFile(ini, name & ".bak")
            Dim file As System.IO.StreamWriter
            file = My.Computer.FileSystem.OpenTextFileWriter(ini, True)
            For Each Item As String In Apachein
                file.WriteLine(Item)
            Next
            file.Close()
        Catch
        End Try


    End Sub
#End Region
End Class