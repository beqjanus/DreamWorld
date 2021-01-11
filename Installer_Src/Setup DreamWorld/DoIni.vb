Imports System.IO
Imports System.Text.RegularExpressions

Module DoIni

    ''' <summary>Set up all INI files</summary>
    ''' <returns>true if it fails</returns>
    Public Function SetIniData() As Boolean

        TextPrint(My.Resources.Creating_INI_Files_word)

        If DoRobust() Then Return True
        If DoTos() Then Return True
        If DoGridCommon() Then Return True
        If DoEditForeigners() Then Return True
        If DelLibrary() Then Return True
        If DoFlotsamINI() Then Return True
        If DoWifi() Then Return True
        If DoGloebits() Then Return True
        If DoWhoGotWhat() Then Return True
        If DoTides() Then Return True
        If DoBirds() Then Return True
        If DoPHPDBSetup() Then Return True
        If DoPHP() Then Return True
        If DoApache() Then Return True
        If DoIceCast() Then Return True

        Return False

    End Function

    Public Function DoApache() As Boolean

        If Not Settings.ApacheEnable Then Return False
        TextPrint("->Set Apache")

        ' lean rightward paths for Apache
        Dim ini = IO.Path.Combine(Settings.CurrentDirectory, "Outworldzfiles\Apache\conf\httpd.conf")
        Settings.LoadLiteralIni(ini)
        Settings.SetLiteralIni("Listen", "Listen " & Convert.ToString(Settings.ApachePort, Globalization.CultureInfo.InvariantCulture))
        Settings.SetLiteralIni("Define SRVROOT", "Define SRVROOT " & """" & FormSetup.PropCurSlashDir & "/Outworldzfiles/Apache" & """")
        Settings.SetLiteralIni("DocumentRoot", "DocumentRoot " & """" & FormSetup.PropCurSlashDir & "/Outworldzfiles/Apache/htdocs" & """")
        Settings.SetLiteralIni("Use VDir", "Use VDir " & """" & FormSetup.PropCurSlashDir & "/Outworldzfiles/Apache/htdocs" & """")
        Settings.SetLiteralIni("PHPIniDir", "PHPIniDir " & """" & FormSetup.PropCurSlashDir & "/Outworldzfiles/PHP7" & """")
        Settings.SetLiteralIni("ServerName", "ServerName " & Settings.PublicIP)
        Settings.SetLiteralIni("ServerAdmin", "ServerAdmin " & Settings.AdminEmail)
        Settings.SetLiteralIni("<VirtualHost", "<VirtualHost  *:" & Convert.ToString(Settings.ApachePort, Globalization.CultureInfo.InvariantCulture) & ">")
        Settings.SetLiteralIni("ErrorLog", "ErrorLog " & """|bin/rotatelogs.exe  -l \" & """" & FormSetup.PropCurSlashDir & "/Outworldzfiles/Apache/logs/Error-%Y-%m-%d.log" & "\" & """" & " 86400""")
        Settings.SetLiteralIni("CustomLog", "CustomLog " & """|bin/rotatelogs.exe -l \" & """" & FormSetup.PropCurSlashDir & "/Outworldzfiles/Apache/logs/access-%Y-%m-%d.log" & "\" & """" & " 86400""" & " common env=!dontlog")
        Settings.SetLiteralIni("LoadModule php7_module", "LoadModule php7_module " & """" & FormSetup.PropCurSlashDir & "/OutworldzFiles/PHP7/php7apache2_4.dll" & """")

        Settings.SaveLiteralIni(ini, "httpd.conf")

        FileStuff.DeleteFolder(IO.Path.Combine(Settings.CurrentDirectory, "Outworldzfiles\PHP5"))

        ' lean rightward paths for Apache
        ini = IO.Path.Combine(Settings.CurrentDirectory, "Outworldzfiles\Apache\conf\extra\httpd-ssl.conf")
        Settings.LoadLiteralIni(ini)
        Settings.SetLiteralIni("Listen", "Listen " & Settings.PrivateURL & ":" & "443")
        Settings.SetLiteralIni("ServerName", "ServerName " & Settings.PublicIP)

        Settings.SaveLiteralIni(ini, "httpd-ssl.conf")

        Return False

    End Function

    Private Function DoPHP() As Boolean

        TextPrint("->Set PHP7")
        Dim ini = IO.Path.Combine(Settings.CurrentDirectory, "Outworldzfiles\PHP7\php.ini")
        Settings.LoadLiteralIni(ini)
        Settings.SetLiteralIni("extension_dir", "extension_dir = " & """" & FormSetup.PropCurSlashDir & "/OutworldzFiles/PHP7/ext/""")
        Settings.SetLiteralIni("doc_root", "doc_root = """ & FormSetup.PropCurSlashDir & "/OutworldzFiles/Apache/htdocs/""")

        Settings.SaveLiteralIni(ini, "php.ini")

        Return False

    End Function

    Public Function DoPHPDBSetup() As Boolean

        TextPrint("->Set Maps")
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
"$CONF_HOME            = " & """" & Settings.CMS & """" & ";          //Link To your Home Folder in htdocs.  WordPress, DreamGrid, JOpensim/jOpensim or user assigned folder" & vbCrLf &
"?>"

        Using outputFile As New StreamWriter(IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Apache\htdocs\MetroMap\includes\config.php"), False)
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

        Using outputFile As New StreamWriter(IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\PHP7\databaseinfo.php"), False)
            outputFile.WriteLine(phptext)
        End Using

        Return False

    End Function

    Private Function DoTides() As Boolean

        Dim TideData As String = ""
        Dim TideFile = Settings.OpensimBinPath & "addon-modules\OpenSimTide\config\OpenSimTide.ini"

        FileStuff.DeleteFile(TideFile)

        For Each RegionUUID As String In PropRegionClass.RegionUuids
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
        IO.File.WriteAllText(TideFile, TideData, System.Text.Encoding.Default) 'The text file will be created if it does not already exist

        Return False

    End Function

    Private Function DoWifi() As Boolean

        ' There are two Wifi's so search will work

        If Settings.LoadIni(Settings.OpensimBinPath & "config-addon-opensim\Wifi.ini", ";") Then Return True
        TextPrint("->Set Diva Wifi page")
        Settings.SetIni("DatabaseService", "ConnectionString", Settings.RobustDBConnection)
        Settings.SaveINI(System.Text.Encoding.UTF8)

        If Settings.LoadIni(Settings.OpensimBinPath & "Wifi.ini", ";") Then Return True

        Settings.SetIni("DatabaseService", "ConnectionString", Settings.RobustDBConnection)

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

    Private Function DoFlotsamINI() As Boolean

        If Settings.LoadIni(Settings.OpensimBinPath & "config-include\FlotsamCache.ini", ";") Then Return True
        TextPrint("->Set Flotsam Cache")
        Settings.SetIni("AssetCache", "LogLevel", Settings.CacheLogLevel)
        Settings.SetIni("AssetCache", "CacheDirectory", Settings.CacheFolder)
        Settings.SetIni("AssetCache", "FileCacheEnabled", CStr(Settings.CacheEnabled))
        Settings.SetIni("AssetCache", "FileCacheTimeout", Settings.CacheTimeout)
        Settings.SaveINI(System.Text.Encoding.ASCII)
        Return False

    End Function

    Private Function DoEditForeigners() As Boolean

        TextPrint("->Set Residents/Foreigners")
        ' adds a list like 'Region_Test_1 = "DisallowForeigners"' to Gridcommon.ini

        Dim Authorizationlist As String = ""
        For Each RegionUUID As String In PropRegionClass.RegionUuids

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

    Private Function DoTos() As Boolean

        Try
            Dim reader As System.IO.StreamReader
            reader = System.IO.File.OpenText(IO.Path.Combine(Settings.CurrentDirectory, "tos.html"))
            'now loop through each line
            Dim HTML As String = ""
            While reader.Peek <> -1
                HTML = HTML + reader.ReadLine() + vbCrLf
            End While
            reader.Close()

            Using outputFile As New StreamWriter(IO.Path.Combine(Settings.CurrentDirectory + "\Outworldzfiles\Opensim\bin\WifiPages\tos.html"))
                outputFile.WriteLine(HTML)
            End Using
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try

        Return False

    End Function

    Public Function DoSetDefaultSims() As Boolean

        TextPrint("->Set Default Sims")
        ' set the defaults in the INI for the viewer to use. Painful to do as it's a Left hand side edit must be done before other edits to Robust.HG.ini as this makes the actual Robust.HG.ifile
        Dim reader As IO.StreamReader
        Dim line As String

        Try
            ' add this sim name as a default to the file as HG regions, and add the other regions as fallback it may have been deleted
            Dim WelcomeUUID As String = PropRegionClass.FindRegionByName(Settings.WelcomeRegion)

            If WelcomeUUID.Length = 0 Then
                MsgBox(My.Resources.Cannot_locate, vbInformation)
                Return True
            End If

            Dim DefaultName = Settings.WelcomeRegion

            FileStuff.DeleteFile(Settings.OpensimBinPath & "Robust.HG.ini")

            ' Replace the block with a list of regions with the Region_Name = DefaultRegion, DefaultHGRegion is Welcome Region_Name = FallbackRegion, Persistent if a Smart Start region and SS is
            ' enabled Region_Name = FallbackRegion if not a SmartStart

            Dim RegionSetting As String = Nothing

            ' make a long list of the various regions with region_ at the start
            For Each RegionUUID As String In PropRegionClass.RegionUuids
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

        Return False

    End Function

    Public Function Stripqq(input As String) As String

        ' remove double quotes and any comments ";"
        Return Replace(input, """", "")

    End Function

    Public Function DoGridCommon() As Boolean

        TextPrint("->Set GridCommon.ini")

        'Choose a GridCommon.ini to use.
        Dim GridCommon As String = "Gridcommon-GridServer.ini"

        Select Case Settings.ServerType
            Case "Robust"
                Try
                    My.Computer.FileSystem.CopyDirectory(Settings.OpensimBinPath & "Library.proto", Settings.OpensimBinPath & "Library", True)
                Catch ex As Exception
                    BreakPoint.Show(ex.Message)
                End Try
                If Settings.CMS = JOpensim Then
                    GridCommon = "Gridcommon-GridServer-JOpensim.ini"
                Else
                    GridCommon = "Gridcommon-GridServer.ini"
                End If

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
        FileStuff.CopyFile(IO.Path.Combine(Settings.OpensimBinPath & "config-include\", GridCommon), IO.Path.Combine(Settings.OpensimBinPath, "config-include\GridCommon.ini"), True)

        If Settings.LoadIni(Settings.OpensimBinPath & "config-include\GridCommon.ini", ";") Then Return True
        Settings.SetIni("HGInventoryAccessModule", "OutboundPermission", CStr(Settings.OutBoundPermissions))
        Settings.SetIni("DatabaseService", "ConnectionString", Settings.RegionDBConnection)

        ' ;; Send visual reminder to local users that their inventories are unavailable while they are traveling ;; and available when they return. True by default.
        If Settings.Suitcase Then
            Settings.SetIni("HGInventoryAccessModule", "RestrictInventoryAccessAbroad", "true")
        Else
            Settings.SetIni("HGInventoryAccessModule", "RestrictInventoryAccessAbroad", "false")
        End If

        Settings.SaveINI(System.Text.Encoding.UTF8)
        Return False

    End Function

    Public Function DoBirds() As Boolean

        If Not Settings.BirdsModuleStartup Then Return False
        TextPrint("->Set Birds")
        Dim BirdFile = Settings.OpensimBinPath & "addon-modules\OpenSimBirds\config\OpenSimBirds.ini"

        FileStuff.DeleteFile(BirdFile)

        Dim BirdData As String = ""

        ' Birds setup per region
        For Each RegionUUID As String In PropRegionClass.RegionUuids
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
        IO.File.WriteAllText(BirdFile, BirdData, System.Text.Encoding.Default) 'The text file will be created if it does not already exist

        Return False

    End Function

    Public Function DoIceCast() As Boolean

        TextPrint("->Set IceCast")
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

        Using outputFile As New IO.StreamWriter(IO.Path.Combine(Settings.CurrentDirectory, "Outworldzfiles\Icecast\icecast_run.xml"), False)
            outputFile.WriteLine(icecast)
        End Using

        Return False

    End Function

    Public Function DoGloebits() As Boolean

        'Gloebits.ini
        If Settings.LoadIni(Settings.OpensimBinPath & "config-addon-opensim\Gloebit.ini", ";") Then Return True

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

        If Settings.ServerType = "Robust" Then
            Settings.SetIni("Gloebit", "GLBSpecificConnectionString", Settings.RobustDBConnection)
        Else
            Settings.SetIni("Gloebit", "GLBSpecificConnectionString", Settings.RegionDBConnection)
        End If

        Settings.SaveINI(System.Text.Encoding.UTF8)
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

    Public Function GetOpensimProto() As String
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

    Public Function DoWhoGotWhat() As Boolean

        If Settings.LoadIni(Settings.OpensimBinPath & "config-addon-opensim\WhoGotWhat.ini", ";") Then Return True
        Settings.SetIni("WhoGotWhat", "MachineID", Settings.MachineID)
        Settings.SaveINI(System.Text.Encoding.UTF8)
        Return False

    End Function

End Module
