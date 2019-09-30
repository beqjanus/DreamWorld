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
Imports IniParser

Public Class MySettings

    Dim parser As FileIniDataParser
    Dim Myparser As FileIniDataParser
    Dim INI As String = ""
    Dim Data As IniParser.Model.IniData
    Dim MyData As IniParser.Model.IniData
    Dim myINI As String = ""
    Dim gFolder As String

    Dim Apachein As New List(Of String)
    Dim Apacheout As New List(Of String)

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
            LoadSettingsIni()
        Else
            myINI = Folder + "\OutworldzFiles\Settings.ini"
            Dim contents = "[Data]" + vbCrLf
            Using outputFile As New StreamWriter(myINI, False)
                outputFile.WriteLine(contents)
            End Using

            LoadSettingsIni()

            AdminFirst() = My.Settings.AdminFirst
            AdminLast() = My.Settings.AdminLast
            AdminEmail() = My.Settings.AdminEmail
            AutoBackup() = My.Settings.AutoBackup
            AutobackupInterval() = My.Settings.AutobackupInterval

            Autostart() = My.Settings.Autostart
            AccountConfirmationRequired() = My.Settings.AccountConfirmationRequired

            BackupFolder() = My.Settings.BackupFolder

            Clouds() = False    ' does not exist in old code, so set it off
            Density() = 0.5
            ConsoleUser() = My.Settings.ConsoleUser
            ConsolePass() = My.Settings.ConsolePass
            CoordX() = My.Settings.CoordX
            CoordY() = My.Settings.CoordY
            ConsoleShow = My.Settings.ConsoleShow

            DiagFailed() = My.Settings.DiagFailed

            DiagnosticPort() = CInt(My.Settings.DiagnosticPort)
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

            HttpPort() = CInt(My.Settings.HttpPort)

            KeepForDays() = My.Settings.KeepForDays

            LoopBackDiag() = My.Settings.LoopBackDiag

            MapType() = My.Settings.MapType
            Myfolder() = Folder
            MyX() = My.Settings.MyX
            MyY() = My.Settings.MyY

            Password() = My.Settings.Password
            Physics() = My.Settings.Physics
            PrivatePort() = CInt(My.Settings.PrivatePort)
            PublicIP() = My.Settings.PublicIP

            AllowGridGods() = CType(My.Settings.allow_grid_gods, Boolean)
            RegionOwnerIsGod() = My.Settings.region_owner_is_god
            RegionManagerIsGod() = My.Settings.region_manager_is_god

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

            SCEnable() = False
            SCPortBase() = 8080
            SCPortBase1() = 8081
            SCPassword() = "A password"
            SCAdminPassword() = "Admin Password"

            SizeX() = My.Settings.SizeX
            SizeY() = My.Settings.SizeY
            SimName() = My.Settings.SimName

            'email
            SmtpHost() = "smtp.gmail.com"
            SmtpPort() = 587
            SmtPropUserName() = My.Settings.SmtPropUserName
            SmtpPassword() = My.Settings.SmtpPassword

            SplashPage() = My.Settings.SplashPage

            UPnPEnabled() = My.Settings.UPnPEnabled
            UPnpDiag() = My.Settings.UPnpDiag

            VivoxEnabled = My.Settings.VivoxEnabled
            VivoxUserName() = My.Settings.Vivox_username
            VivoxPassword() = My.Settings.Vivox_password

            WelcomeRegion() = My.Settings.WelcomeRegion
            WifiEnabled() = My.Settings.WifiEnabled

        End If

        If Theme().Length = 0 Then
            Theme() = "White"
            Form1.CopyWifi("White")
        ElseIf Theme() = "Black" Then
            Form1.CopyWifi("Black")
        ElseIf Theme() = "White" Then
            Form1.CopyWifi("White")
        ElseIf Theme() = "Custom" Then
            Form1.CopyWifi("Custom")
        End If

    End Sub

#End Region

#Region "Functions And Subs"

    Public Sub SetIni(section As String, key As String, value As String)

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

    Public Sub LoadSettingsIni()

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

    Public Sub LoadIni(arg As String, comment As String)

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

    Public Function GetIni(section As String, key As String, D As String) As String

        Dim R = Stripqq(Data(section)(key))
        If R = Nothing Then R = D
        Return R

    End Function

    Public Function GetMyIni(section As String, key As String, Optional D As String = "") As String

        Dim R = Stripqq(MyData(section)(key))
        If R = Nothing Then R = D
        Return R

    End Function

    Public Sub SaveINI()

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

    Shared Function Random() As String

        Dim value As Integer = CInt(Int((600000000 * Rnd()) + 1))
        Random = System.Convert.ToString(value, Form1.Invarient)

    End Function

    Shared Function Stripqq(input As String) As String

        Return Replace(input, """", "")

    End Function

    Public Function GetMySetting(key As String, Optional D As String = "") As String
        Try
            Dim value = GetMyIni("Data", key, D)
            Return value
        Catch
            Return D
        End Try

    End Function

    Public Sub SetMySetting(key As String, value As String)

        SetMyIni("Data", key, value)

    End Sub

#End Region

#Region "Properties"

    Public Property JOpenSimEnabled() As Boolean
        Get
            Return CType(GetMySetting("JOpenSimEnabled", "False"), Boolean)
        End Get
        Set
            SetMySetting("JOpenSimEnabled", CStr(Value))
        End Set
    End Property

    Public Property EventTimerEnabled() As Boolean
        Get
            Return CType(GetMySetting("EventTimerEnabled", "True"), Boolean)
        End Get
        Set
            SetMySetting("EventTimerEnabled", CStr(Value))
        End Set
    End Property

    Public Property OutBoundPermissions() As Boolean
        Get
            Return CType(GetMySetting("OutBoundPermissions", "True"), Boolean)
        End Get
        Set
            SetMySetting("OutBoundPermissions", CStr(Value))
        End Set
    End Property

    Public Property SearchEnabled() As Boolean
        Get
            Return CType(GetMySetting("SearchEnabled", "True"), Boolean)
        End Get
        Set
            SetMySetting("SearchEnabled", CStr(Value))
        End Set
    End Property

    Public Property OverrideName() As String
        Get
            Return GetMySetting("OverrideName", "")
        End Get
        Set
            SetMySetting("OverrideName", Value)
        End Set
    End Property

    Public Property SmartStart() As Boolean
        Get
            Return CType(GetMySetting("SmartStart", "False"), Boolean)
        End Get
        Set
            SetMySetting("SmartStart", CStr(Value))
        End Set

    End Property

    Public Property MinTimerInterval() As Single
        Get
            Try
                Return CType(GetMySetting("MinTimerInterval", "0.2"), Single)
            Catch
                Return 0.2
            End Try
        End Get
        Set
            SetMySetting("MinTimerInterval", CStr(Value))
        End Set
    End Property

    ' fsassets
    Public Property FsAssetsEnabled() As Boolean
        Get
            Return CType(GetMySetting("FsAssetsEnabled", "False"), Boolean)
        End Get
        Set
            SetMySetting("FsAssetsEnabled", CStr(Value))
        End Set
    End Property

    'LocalServiceModule
    Public Property LocalServiceModule() As String
        Get
            Return GetMySetting("LocalServiceModule", "OpenSim.Services.AssetService.dll:AssetService")
        End Get
        Set
            SetMySetting("LocalServiceModule", Value)
        End Set
    End Property

    'BaseDirectory
    Public Property BaseDirectory() As String
        Get
            Return GetMySetting("BaseDirectory", "./fsassets")
        End Get
        Set
            SetMySetting("BaseDirectory", Value)
        End Set
    End Property

    'ShowConsoleStats
    Public Property ShowConsoleStats() As String
        Get
            Return GetMySetting("ShowConsoleStats", "True")
        End Get
        Set
            SetMySetting("ShowConsoleStats", Value)
        End Set
    End Property

    Public Property CacheTimeout() As String
        Get
            Return GetMySetting("CacheTimeout", "4")
        End Get
        Set
            SetMySetting("CacheTimeout", Value)
        End Set
    End Property

    Public Property CacheFolder() As String
        Get
            Return GetMySetting("CacheFolder", ".\assetcache")
        End Get
        Set
            SetMySetting("CacheFolder", Value)
        End Set
    End Property

    Public Property CacheLogLevel() As String
        Get
            Return GetMySetting("CacheLogLevel", "0")
        End Get
        Set
            SetMySetting("CacheLogLevel", Value)
        End Set
    End Property

    Public Property BaseHostName() As String
        Get
            Return GetMySetting("BaseHostName", DNSName)
        End Get
        Set
            SetMySetting("BaseHostName", Value)
        End Set
    End Property

    Public Property ExternalHostName() As String
        Get
            Return GetMySetting("ExternalHostName", DNSName)
        End Get
        Set
            SetMySetting("ExternalHostName", Value)
        End Set
    End Property

    Public Property CacheEnabled() As Boolean
        Get
            Return CType(GetMySetting("CacheEnabled", "True"), Boolean)
        End Get
        Set
            SetMySetting("CacheEnabled", CStr(Value))
        End Set
    End Property

    Public Property OpensimBinPath() As String
        Get
            Return GetMySetting("OpensimBinPath")
        End Get
        Set
            SetMySetting("OpensimBinPath", Value)
        End Set
    End Property

    Public Property WelcomeMessage() As String
        Get
            Return GetMySetting("WelcomeMessage", "Welcome to " & SimName() & " , <USERNAME>")
        End Get
        Set
            SetMySetting("WelcomeMessage", Value)
        End Set

    End Property

    Public Property DeleteScriptsOnStartupOnce() As Boolean
        Get
            Return CType(GetMySetting("DeleteScriptsOnStartupOnce", "True"), Boolean)
        End Get
        Set
            SetMySetting("DeleteScriptsOnStartupOnce", CStr(Value))
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
            Return CType(GetMySetting("SearchLocal", "False"), Boolean)
        End Get
        Set
            SetMySetting("SearchLocal", CStr(Value))
        End Set
    End Property

    Public Property RestartOnCrash() As Boolean
        Get
            Return CType(GetMySetting("RestartOnCrash", "False"), Boolean)
        End Get
        Set
            SetMySetting("RestartOnCrash", CStr(Value))
        End Set
    End Property

    Public Property RestartonPhysics() As Boolean
        Get
            Return CType(GetMySetting("RestartonPhysics", "False"), Boolean)
        End Get
        Set
            SetMySetting("RestartonPhysics", CStr(Value))
        End Set
    End Property

    Public Property RenderMaxHeight() As String
        Get
            Return GetMySetting("RenderMaxHeight", "4000")
        End Get
        Set
            SetMySetting("RenderMaxHeight", Value)
        End Set
    End Property

    Public Property RenderMinHeight() As String
        Get
            Return GetMySetting("RenderMinHeight", "-100")
        End Get
        Set
            SetMySetting("RenderMinHeight", Value)
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

    Public Property SearchMigration() As Integer
        Get
            Return CType(GetMySetting("SearchMigration", "0"), Integer)
        End Get
        Set
            SetMySetting("SearchMigration", CStr(Value))
        End Set
    End Property

    Public Property ApachePort() As Integer
        Get
            Return CType(GetMySetting("ApachePort", "80"), Integer)
        End Get
        Set
            SetMySetting("ApachePort", CType(Value, String))
        End Set
    End Property

    Public Property ApacheService() As Boolean
        Get
            Return CType(GetMySetting("ApacheService", "False"), Boolean)
        End Get
        Set
            SetMySetting("ApacheService", CStr(Value))
        End Set
    End Property

    Public Property ApacheEnable() As Boolean
        Get
            Return CType(GetMySetting("ApacheEnabled", "False"), Boolean)
        End Get
        Set
            SetMySetting("ApacheEnabled", CStr(Value))
        End Set
    End Property

    Public Property RegionListVisible() As Boolean
        Get
            Return CType(GetMySetting("RegionListVisible", "False"), Boolean)
        End Get
        Set
            SetMySetting("RegionListVisible", CStr(Value))
        End Set
    End Property

    Public Property RegionListView() As Integer
        Get
            Return CType(GetMySetting("RegionListView", "2"), Integer)
        End Get
        Set
            SetMySetting("RegionListView", CStr(Value))
        End Set
    End Property

    Public Property Sequential() As Boolean
        Get
            Return CType(GetMySetting("Sequential", "False"), Boolean)
        End Get
        Set
            SetMySetting("Sequential", CStr(Value))
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

    Public Property MySqlRegionDBPort() As Integer
        Get
            Return CInt(GetMySetting("MySqlRegionDBPort", "3306"))
        End Get
        Set
            SetMySetting("MySqlRegionDBPort", CStr(Value))
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
            Return GetMySetting("Theme", "") ' no default, so we copy it.
        End Get
        Set
            SetMySetting("Theme", Value)
        End Set
    End Property

    ' Tides
    Public Property TideInfoDebug() As Boolean
        Get
            Return CType(GetMySetting("TideInfoDebug", "False"), Boolean)
        End Get
        Set
            SetMySetting("TideInfoDebug", CStr(Value))
        End Set
    End Property

    Public Property TideEnabled() As Boolean
        Get
            Return CType(GetMySetting("TideEnabled", "False"), Boolean)
        End Get
        Set
            SetMySetting("TideEnabled", CStr(Value))
        End Set
    End Property

    Public Property TideHighLevel() As String
        Get
            Return GetMySetting("TideHighLevel", "20")
        End Get
        Set
            SetMySetting("TideHighLevel", Value)
        End Set
    End Property

    Public Property TideLowLevel() As String
        Get
            Return GetMySetting("TideLowLevel", "17")
        End Get
        Set
            SetMySetting("TideLowLevel", Value)
        End Set
    End Property

    Public Property CycleTime() As String
        Get
            Return GetMySetting("CycleTime", "900")
        End Get
        Set
            SetMySetting("CycleTime", Value)
        End Set
    End Property

    Public Property BroadcastTideInfo() As Boolean
        Get
            Return CType(GetMySetting("BroadcastTideInfo", "True"), Boolean)
        End Get
        Set
            SetMySetting("BroadcastTideInfo", CStr(Value))
        End Set
    End Property

    Public Property TideInfoChannel() As String
        Get
            Return GetMySetting("TideInfoChannel", "5555")
        End Get
        Set
            SetMySetting("TideInfoChannel", Value)
        End Set
    End Property

    Public Property TideLevelChannel() As String
        Get
            Return GetMySetting("TideLevelChannel", "5556")
        End Get
        Set
            SetMySetting("TideLevelChannel", Value)
        End Set
    End Property

    ' more stuff

    Public Property FirstRegionPort() As Integer
        Get
            Return CInt(GetMySetting("FirstRegionPort", "8004"))
        End Get
        Set
            SetMySetting("FirstRegionPort", CStr(Value))
        End Set
    End Property

    Public Property Myfolder() As String
        Get
            Return GetMySetting("Myfolder") ' no default
        End Get
        Set
            SetMySetting("Myfolder", Value)
        End Set
    End Property

    Public Property BirdsModuleStartup() As Boolean
        Get
            Return CType(GetMySetting("BirdsModuleStartup", "False"), Boolean)
        End Get
        Set
            SetMySetting("BirdsModuleStartup", CStr(Value))
        End Set
    End Property

    Public Property BirdsEnabled() As Boolean
        Get
            Return CType(GetMySetting("BirdsEnabled", "False"), Boolean)
        End Get
        Set
            SetMySetting("BirdsEnabled", CStr(Value))
        End Set
    End Property

    Public Property BirdsFlockSize() As String
        Get
            Return GetMySetting("BirdsFlockSize", "25")
        End Get
        Set
            SetMySetting("BirdsFlockSize", Value)
        End Set
    End Property

    ''' <summary>
    ''' which channel do we listen on for in world commands
    ''' </summary>
    ''' <returns></returns>
    Public Property BirdsChatChannel() As Integer
        Get
            Return CType(GetMySetting("BirdsChatChannel", "118"), Integer)
        End Get
        Set
            SetMySetting("BirdsChatChannel", CStr(Value))
        End Set
    End Property

    ''' <summary>
    ''' 'how far each bird can travel per update
    ''' </summary>
    ''' <returns></returns>
    Public Property BirdsMaxSpeed() As Double
        Get
            Return CType(GetMySetting("BirdsMaxSpeed", "1.0"), Double)
        End Get
        Set
            SetMySetting("BirdsMaxSpeed", CStr(Value))
        End Set
    End Property

    Public Property BirdsMaxForce() As Double
        Get
            Return CType(GetMySetting("BirdsMaxForce", "0.2"), Double)
        End Get
        Set
            SetMySetting("BirdsMaxForce", CStr(Value))
        End Set
    End Property

    ''' <summary>
    ''' max distance for other birds to be considered in the same flock as us
    ''' </summary>
    ''' <returns></returns>
    Public Property BirdsNeighbourDistance() As Double
        Get
            Return CType(GetMySetting("BirdsNeighbourDistance", "25"), Double)
        End Get
        Set
            SetMySetting("BirdsNeighbourDistance", CStr(Value))
        End Set
    End Property

    ''' <summary>
    ''' how far away from other birds we would like to stay
    ''' </summary>
    ''' <returns></returns>
    Public Property BirdsDesiredSeparation() As Double
        Get
            Return CType(GetMySetting("BirdsDesiredSeparation", "5"), Double)
        End Get
        Set
            SetMySetting("BirdsDesiredSeparation", CStr(Value))
        End Set
    End Property

    ''' <summary>
    ''' how close to the edges of things can we get without being worried
    ''' </summary>
    ''' <returns></returns>
    Public Property BirdsTolerance() As Double
        Get
            Return CType(GetMySetting("BirdsTolerance", "25"), Double)
        End Get
        Set
            SetMySetting("BirdsTolerance", CStr(Value))
        End Set
    End Property

    ''' <summary>
    ''' how close to the edge of a region can we get?
    ''' </summary>
    ''' <returns></returns>
    Public Property BirdsBorderSize() As Double
        Get
            Return CType(GetMySetting("BirdsBorderSize", "25"), Double)
        End Get
        Set
            SetMySetting("BirdsBorderSize", CStr(Value))
        End Set
    End Property

    ''' <summary>
    ''' how high are we allowed to flock
    ''' </summary>
    ''' <returns></returns>
    Public Property BirdsMaxHeight() As Double
        Get
            Return CType(GetMySetting("BirdsMaxHeight", "25"), Double)
        End Get
        Set
            SetMySetting("BirdsMaxHeight", CStr(Value))
        End Set
    End Property

    ''' <summary>
    ''' By default the module will create a flock of plain wooden spheres, however this can be
    ''' overridden to the name of an existing prim that needs to already exist in the scene - i.e. be
    ''' rezzed in the region.
    ''' </summary>
    ''' <returns></returns>
    Public Property BirdsPrim() As String
        Get
            Return GetMySetting("BirdsPrim", "SeaGull1")
        End Get
        Set
            SetMySetting("BirdsPrim", Value)
        End Set
    End Property

    Public Property LSLHTTP() As Boolean
        Get
            Return CType(GetMySetting("LSL_HTTP", "False"), Boolean)
        End Get
        Set
            SetMySetting("LSL_HTTP", CStr(Value))
        End Set
    End Property

    Public Property AutoRestartEnabled() As Boolean
        Get
            Return CType(GetMySetting("AutoRestartEnabled", "False"), Boolean)
        End Get
        Set
            SetMySetting("AutoRestartEnabled", CStr(Value))
        End Set
    End Property

    Public Property AutoRestartInterval() As Integer
        Get
            Return CType(GetMySetting("AutoRestartInterval", "0"), Integer)
        End Get
        Set
            SetMySetting("AutoRestartInterval", CStr(Value))
        End Set
    End Property

    Public Property ShowToLocalUsers() As Boolean
        Get
            Return CType(GetMySetting("ShowToLocalUsers", "False"), Boolean)
        End Get
        Set
            SetMySetting("ShowToLocalUsers", CStr(Value))
        End Set
    End Property

    Public Property ShowToForeignUsers() As Boolean
        Get
            Return CType(GetMySetting("ShowToForeignUsers", "False"), Boolean)
        End Get
        Set
            SetMySetting("ShowToForeignUsers", CStr(Value))
        End Set
    End Property

    Public Property TOSEnabled() As Boolean
        Get
            Return CType(GetMySetting("TOSEnabled", "False"), Boolean)
        End Get
        Set
            SetMySetting("TOSEnabled", CStr(Value))
        End Set
    End Property

    Public Property Primlimits() As Boolean
        Get
            Return CType(GetMySetting("Primlimits", "False"), Boolean)
        End Get
        Set
            SetMySetting("Primlimits", CStr(Value))
        End Set
    End Property

    Public Property Suitcase() As Boolean
        Get
            Return CType(GetMySetting("Suitcase", "True"), Boolean)
        End Get
        Set
            SetMySetting("Suitcase", CStr(Value))
        End Set
    End Property

    Public Property GDPR() As Boolean
        Get
            Return CType(GetMySetting("GDPR", "False"), Boolean)
        End Get
        Set
            SetMySetting("GDPR", CStr(Value))
        End Set
    End Property

    Public Property SmtpHost() As String
        Get
            Return CType(GetMySetting("SmtpHost", "smtp.gmail.com"), String)
        End Get
        Set
            SetMySetting("SmtpHost", Value)
        End Set
    End Property

    Public Property SmtpPort() As Integer
        Get
            Return CInt(GetMySetting("SmtpPort", "587"))
        End Get
        Set
            SetMySetting("SmtpPort", CStr(Value))
        End Set
    End Property

    Public Property Clouds() As Boolean
        Get
            Return CType(GetMySetting("Clouds", "False"), Boolean)
        End Get
        Set
            SetMySetting("Clouds", CStr(Value))
        End Set
    End Property

    Public Property Density() As Double
        Get
            Try
                Return CType(GetMySetting("Density", "0.5"), Double)
            Catch
                Return 0.5
            End Try

        End Get
        Set
            SetMySetting("Density", CStr(Value))
        End Set
    End Property

#Disable Warning CA1056 ' Uri properties should not be strings

    Public Property PrivateURL() As String
#Enable Warning CA1056 ' Uri properties should not be strings
        Get
            Return GetMySetting("PrivateURL")   ' no default
        End Get
        Set
            SetMySetting("PrivateURL", CStr(Value))
        End Set
    End Property

    Public Property ConsoleShow() As Boolean
        Get
            Return CType(GetMySetting("ConsoleShow", "False"), Boolean)
        End Get
        Set
            SetMySetting("ConsoleShow", CStr(Value))
        End Set
    End Property

    Public Property AutoBackup() As Boolean
        Get
            Return CType(GetMySetting("AutoBackup", "True"), Boolean)
        End Get
        Set
            SetMySetting("AutoBackup", CStr(Value))
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
            Return CType(GetMySetting("CoordX", "1000"), String)
        End Get
        Set
            SetMySetting("CoordX", Value)
        End Set
    End Property

    Public Property CoordY() As String
        Get
            Return CType(GetMySetting("CoordY", "1000"), String)
        End Get
        Set
            SetMySetting("CoordY", Value)
        End Set
    End Property

    Public Property PrivatePort() As Integer
        Get
            Return CInt(GetMySetting("PrivatePort", "8003"))
        End Get
        Set
            SetMySetting("PrivatePort", CStr(Value))
        End Set
    End Property

    Public Property SizeX() As String
        Get
            Return CType(GetMySetting("SizeX", "256"), String)
        End Get
        Set
            SetMySetting("SizeX", Value)
        End Set
    End Property

    Public Property SizeY() As String
        Get
            Return CType(GetMySetting("SizeY", "256"), String)
        End Get
        Set
            SetMySetting("SizeY", Value)
        End Set
    End Property

    Public Property AutobackupInterval() As String
        Get
            Return CType(GetMySetting("AutobackupInterval", "720"), String)
        End Get
        Set
            SetMySetting("AutobackupInterval", Value)
        End Set
    End Property

    Public Property KeepForDays() As Integer
        Get
            Return CType(GetMySetting("KeepForDays", "7"), Integer)
        End Get
        Set
            SetMySetting("KeepForDays", CStr(Value))
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
            Return CType(GetMySetting("AdminFirst", "Wifi"), String)
        End Get
        Set
            SetMySetting("AdminFirst", Value)
        End Set
    End Property

    Public Property AdminLast() As String
        Get
            Return CType(GetMySetting("AdminLast", "Admin"), String)
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

    Public Property AllowGridGods() As Boolean
        Get
            Return CType(GetMySetting("Allow_grid_gods", "False"), Boolean)
        End Get
        Set
            SetMySetting("Allow_grid_gods", CStr(Value))
        End Set
    End Property

    Public Property RegionOwnerIsGod() As Boolean
        Get
            Return CType(GetMySetting("Region_owner_is_god", "False"), Boolean)
        End Get
        Set
            SetMySetting("Region_owner_is_god", CStr(Value))
        End Set
    End Property

    Public Property RegionManagerIsGod() As Boolean
        Get
            Return CType(GetMySetting("Region_manager_is_god", "False"), Boolean)
        End Get
        Set
            SetMySetting("Region_manager_is_god", CStr(Value))
        End Set
    End Property

    Public Property AccountConfirmationRequired() As Boolean
        Get
            Return CType(GetMySetting("AccountConfirmationRequired", "False"), Boolean)
        End Get
        Set
            SetMySetting("AccountConfirmationRequired", CStr(Value))
        End Set
    End Property

    Public Property SmtPropUserName() As String
        Get
            Return GetMySetting("SmtPropUserName")
        End Get
        Set
            SetMySetting("SmtPropUserName", Value)
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
            Return CType(GetMySetting("RanAllDiags", "False"), Boolean)
        End Get
        Set
            SetMySetting("RanAllDiags", CStr(Value))
        End Set
    End Property

    Public Property DiagFailed() As Boolean
        Get
            Return CType(GetMySetting("DiagFailed"), Boolean)
        End Get
        Set
            SetMySetting("DiagFailed", CStr(Value))
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

    Public Property HttpPort() As Integer
        Get
            Return CInt(GetMySetting("HttpPort", "8002"))
        End Get
        Set
            SetMySetting("HttpPort", CStr(Value))
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
            SetMySetting("LoopBackDiag", CStr(Value))
        End Set
    End Property

    Public Property UPnpDiag() As Boolean
        Get
            Return CType(GetMySetting("UPnpDiag", "False"), Boolean)
        End Get
        Set
            SetMySetting("UPnpDiag", CStr(Value))
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

    Public Property Physics() As Integer
        Get
            Return CType(GetMySetting("Physics"), Integer)
        End Get
        Set
            SetMySetting("Physics", CType(Value, String))
        End Set
    End Property

    Public Property MyX() As Integer
        Get
            Return CType(GetMySetting("MyX"), Integer)
        End Get
        Set
            SetMySetting("MyX", CStr(Value))
        End Set
    End Property

    Public Property MyY() As Integer
        Get
            Return CType(GetMySetting("MyY"), Integer)
        End Get
        Set
            SetMySetting("MyY", CStr(Value))
        End Set
    End Property

    Public Property RobustServer() As String
        Get
            Return CType(GetMySetting("RobustServer"), String)
        End Get
        Set
            SetMySetting("RobustServer", CStr(Value))
        End Set
    End Property

    Public Property VivoxEnabled() As Boolean
        Get
            Return CType(GetMySetting("VivoxEnabled", "False"), Boolean)
        End Get
        Set
            SetMySetting("VivoxEnabled", CStr(Value))
        End Set
    End Property

    Public Property VivoxUserName() As String
        Get
            Return GetMySetting("Vivox_username")
        End Get
        Set
            SetMySetting("Vivox_username", Value)
        End Set
    End Property

    Public Property VivoxPassword() As String
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
            Return GetMySetting("BackupFolder", "AutoBackup")
        End Get
        Set
            SetMySetting("BackupFolder", Value)
        End Set
    End Property

    Public Property WelcomeRegion() As String
        Get
            Return GetMySetting("WelcomeRegion", "Welcome")
        End Get
        Set
            SetMySetting("WelcomeRegion", Value)
        End Set
    End Property

    Public Property GloebitsEnable() As Boolean
        Get
            Return CType(GetMySetting("GloebitsEnable", "False"), Boolean)
        End Get
        Set
            SetMySetting("GloebitsEnable", CStr(Value))
        End Set
    End Property

    Public Property GloebitsMode() As Boolean
        Get
            Return CType(GetMySetting("GloebitsMode", "False"), Boolean)
        End Get
        Set
            SetMySetting("GloebitsMode", CStr(Value))
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

    Public Property MySqlRobustDBPort() As Integer
        Get
            Return CInt(GetMySetting("MySqlRobustDBPort", "3306"))
        End Get
        Set
            SetMySetting("MySqlRobustDBPort", CStr(Value))
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
            Return CType(GetMySetting("WifiEnabled", "True"), Boolean)
        End Get
        Set
            SetMySetting("WifiEnabled", CStr(Value))
        End Set
    End Property

    Public Property DiagnosticPort() As Integer
        Get
            Return CInt(GetMySetting("DiagnosticPort", "8001"))
        End Get
        Set
            SetMySetting("DiagnosticPort", CStr(Value))
        End Set
    End Property

    Public Property RobustDataBaseName() As String
        Get
            Return GetMySetting("RobustMySqlName", "Robust")
        End Get
        Set
            SetMySetting("RobustMySqlName", Value)
        End Set
    End Property

    Public Property RobustUsername() As String
        Get
            Return GetMySetting("RobustMySqlUsername", "robustuser")
        End Get
        Set
            SetMySetting("RobustMySqlUsername", Value)
        End Set
    End Property

    Public Property RobustPassword() As String
        Get
            Return GetMySetting("RobustMySqlPassword", "robustpassword")
        End Get
        Set
            SetMySetting("RobustMySqlPassword", Value)
        End Set
    End Property

    Public Property SimName() As String
        Get
            Return GetMySetting("SimName", "DreamGrid")
        End Get
        Set
            SetMySetting("SimName", Value)
        End Set
    End Property

    Public Property UPnPEnabled() As Boolean
        Get
            Return CType(GetMySetting("UPnPEnabled", "False"), Boolean)
        End Get
        Set
            SetMySetting("UPnPEnabled", CStr(Value))
        End Set
    End Property

    Public Property Autostart() As Boolean
        Get
            Return CType(GetMySetting("Autostart", "False"), Boolean)
        End Get
        Set
            SetMySetting("Autostart", CStr(Value))
        End Set
    End Property

    Public Property EnableHypergrid() As Boolean
        Get
            Return CType(GetMySetting("EnableHypergrid", "True"), Boolean)
        End Get
        Set
            SetMySetting("EnableHypergrid", CStr(Value))
        End Set
    End Property

    Public Property RunOnce() As Boolean
        Get
            Return CType(GetMySetting("RunOnce", "False"), Boolean)
        End Get
        Set
            SetMySetting("RunOnce", CStr(Value))
        End Set
    End Property

    Public Property SCEnable() As Boolean
        Get
            Return CType(GetMySetting("SC_Enable", "False"), Boolean)
        End Get
        Set
            SetMySetting("SC_Enable", CStr(Value))
        End Set
    End Property

    Public Property SCPortBase() As Integer
        Get
            Return CType(GetMySetting("SC_PortBase", "8080"), Integer)
        End Get
        Set
            SetMySetting("SC_PortBase", CStr(Value))
        End Set
    End Property

    Public Property SCPortBase1() As Integer
        Get
            Return CType(GetMySetting("SC_PortBase1", "8081"), Integer)
        End Get
        Set
            SetMySetting("SC_PortBase1", CStr(Value))
        End Set
    End Property

    Public Property SCPassword() As String
        Get
            Return GetMySetting("SC_Password")
        End Get
        Set
            SetMySetting("SC_Password", Value)
        End Set
    End Property

    Public Property SCAdminPassword() As String
        Get
            Return GetMySetting("SC_AdminPassword")
        End Get
        Set
            SetMySetting("SC_AdminPassword", Value)
        End Set
    End Property

#End Region

#Region "Apache"

    ' reader ApacheStrings
    Public Sub LoadLiteralIni(ini As String)

        Apachein.Clear()
        Using Reader As New StreamReader(ini, System.Text.Encoding.ASCII)
            While Reader.EndOfStream = False
                Apachein.Add(Reader.ReadLine())
            End While
        End Using

    End Sub

    Public Sub SetLiteralIni(Name As String, value As String)

        Apacheout.Clear()

        For Each Item As String In Apachein
#Disable Warning CA1307 ' Specify StringComparison
            If Item.StartsWith(Name) Then
#Enable Warning CA1307 ' Specify StringComparison
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
    Public Sub SaveLiteralIni(ini As String, name As String)

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

#Region "Robust"

    Public Function RobustDBConnection() As String

        Return """" _
            & "Data Source=" & RobustServer _
            & ";Database=" & RobustDataBaseName _
            & ";Port=" & CStr(MySqlRobustDBPort) _
            & ";User ID=" & RobustUsername _
            & ";Password=" & RobustPassword _
            & ";Old Guids=true;Allow Zero Datetime=true" _
            & ";Connect Timeout=28800;Command Timeout=28800;" _
            & """"

    End Function

    Public Function RobustMysqlConnection() As String

        Return "server=" & RobustServer _
            & ";database=" & RobustDataBaseName _
            & ";port=" & CStr(MySqlRobustDBPort) _
            & ";user=" & RobustUsername _
            & ";password=" & RobustPassword _
            & ";Old Guids=true;Allow Zero Datetime=true"

    End Function

    Public Function OSSearchConnectionString() As String

        Return "server=" & RobustServer() _
        & ";database=" & "ossearch" _
        & ";port=" & CStr(MySqlRobustDBPort) _
        & ";user=" & RobustUsername _
        & ";password=" & RobustPassword _
        & ";Old Guids=true;Allow Zero Datetime=true;"

    End Function

    Public Function RegionDBConnection() As String

        Return """" _
        & "Data Source=" & RegionServer _
        & ";Database=" & RegionDBName _
        & ";Port=" & CStr(MySqlRegionDBPort) _
        & ";User ID=" & RegionDBUsername _
        & ";Password=" & RegionDbPassword _
        & ";Old Guids=true;Allow Zero Datetime=true" _
        & ";Connect Timeout=28800;Command Timeout=28800;" _
        & """"

    End Function

    Public Function RegionMySqlConnection() As String

        Return "server=" & RegionServer _
        & ";database=" & RegionDBName _
        & ";port=" & CStr(MySqlRegionDBPort) _
        & ";user=" & RegionDBUsername _
        & ";password=" & RegionDbPassword

    End Function

#End Region

End Class