#Region "Copyright AGPL3.0"

' Copyright Outworldz, LLC.
' AGPL3.0  https://opensource.org/licenses/AGPL

#End Region

Imports System.IO
Imports System.Threading
Imports Ionic.Zip

Public Class Backups
    Implements IDisposable

    Private _folder As String
    Private _initted As Boolean
    Private _startDate As Date
    Private _WebThread1 As Thread
    Private _WebThread3 As Thread

    Private Shared Sub Break(msg As String)

        Diagnostics.Debug.Print(msg)

    End Sub

#Region "SQL Backup"

    Public Sub RunAllSQLBackups()

        If Directory.Exists(IO.Path.Combine(Settings.CurrentDirectory, $"OutworldzFiles/Mysql/Data/{Settings.RegionDBName}")) Then DoBackup(Settings.RegionDBName)

        'its possible they are the same db when ported from a DreamWorld
        If Settings.RegionDBName <> Settings.RobustDatabaseName Then
            If Directory.Exists(IO.Path.Combine(Settings.CurrentDirectory, $"OutworldzFiles/Mysql/Data/{Settings.RobustDatabaseName}")) Then DoBackup(Settings.RobustDatabaseName)
        End If

        If Directory.Exists(IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles/Mysql/Data/WordPress")) Then DoBackup("WordPress")
        If Directory.Exists(IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles/Mysql/Data/Joomla")) Then DoBackup("Joomla")
        If Directory.Exists(IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles/Mysql/Data/ossearch")) Then DoBackup("ossearch")
        If Directory.Exists(IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles/Mysql/Data/osmodules")) Then DoBackup("osmodules")

    End Sub

    Public Sub SqlBackup()

        Dim currentdatetime As Date = Date.Now()

        _WebThread1 = New Thread(AddressOf RunAllSQLBackups)
        _WebThread1.SetApartmentState(ApartmentState.STA)
        _WebThread1.Priority = ThreadPriority.BelowNormal
        _WebThread1.Start()

    End Sub

    Private Sub DoBackup(Name As String)

        RunningBackups = Name
        Try
            Dim currentdatetime As Date = Date.Now()
            Dim whenrun As String = currentdatetime.ToString("yyyy-MM-dd_HH_mm_ss", Globalization.CultureInfo.InvariantCulture)

            'used to zip it, zip it good
            _folder = IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\tmp\Backup_" & DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss", Globalization.CultureInfo.InvariantCulture))
            FileIO.FileSystem.CreateDirectory(_folder)

            Dim _filename = "Backup_" & Name & "_" & whenrun & ".sql"
            Dim SQLFile = IO.Path.Combine(_folder, _filename)

            ' we must write this to the file so it knows what database to use.
            Using outputFile As New StreamWriter(SQLFile, False)
                outputFile.Write("use " & Name & ";" + vbCrLf)
            End Using

            Using ProcessSqlDump As New Process With {
                .EnableRaisingEvents = True
            }

                Dim port As String
                Dim host As String
                Dim password As String
                Dim user As String
                Dim dbname As String
                If Name = Settings.RobustDatabaseName Or
                   Name = "Joomla" Or
                   Name = "ossearch" Or
                   Name = "osmodules" Or
                   Name = "WordPress" Then
                    port = CStr(Settings.MySqlRobustDBPort)
                    host = Settings.RobustServerIP
                    user = Settings.RobustUserName
                    password = Settings.RobustPassword
                    dbname = Name
                ElseIf Name = Settings.RegionDBName Then
                    port = CStr(Settings.MySqlRegionDBPort)
                    host = Settings.RegionServer
                    user = Settings.RegionDBUserName
                    password = Settings.RegionDbPassword
                    dbname = Settings.RegionDBName
                Else
                    ErrorLog($"Bad name for database backup of {Name}")
                    Return
                End If

                Dim options = " --host=" & host & " --port=" & port _
                    & " --opt --hex-blob --add-drop-table --allow-keywords  --skip-lock-tables --compress --quick " _
                    & " -u" & user _
                    & " -p" & password _
                    & " --verbose --log-error=Mysqldump.log " _
                    & " --result-file=" & """" & SQLFile & """" _
                    & " " & dbname
                Debug.Print(options)
                '--host=127.0.0.1 --port=3306 --opt --hex-blob --add-drop-table --allow-keywords  -uroot
                ' --verbose --log-error=Mysqldump.log
                ' --result-file="C:\Opensim\Outworldz_Dreamgrid\OutworldzFiles\AutoBackup\tmp\opensim_2020-12-09_00_25_24.sql"
                ' opensim

                Dim pi = New ProcessStartInfo With {
                .Arguments = options,
                .FileName = """" & IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\mysql\bin\mysqldump.exe") & """",
                .WorkingDirectory = IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\mysql\bin"),
                .UseShellExecute = False,
                .RedirectStandardError = True,
                .RedirectStandardOutput = True,
                .CreateNoWindow = True
                }

                ProcessSqlDump.StartInfo = pi

                Try
                    ProcessSqlDump.Start()
                Catch ex As Exception
                    Break(ex.Message)
                End Try
                ProcessSqlDump.WaitForExit()
            End Using

            Dim Bak = IO.Path.Combine(_folder, _filename & ".zip")
            DeleteFile(Bak)

            Using Zip = New ZipFile(Bak)
                Zip.UseZip64WhenSaving = Zip64Option.AsNecessary
                Zip.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression
                Zip.AddFile(SQLFile, "/")
                Zip.Save()
            End Using
            ' stupid windows does not flush buffers!
            While Not File.Exists(Bak)
                Sleep(100)
            End While
            MoveFile(Bak, IO.Path.Combine(BackupPath(), _filename & ".zip"))

            Sleep(100)
            DeleteFile(SQLFile)
            Sleep(100)
            DeleteFolder(_folder)

        Catch ex As Exception
            Break(ex.Message)
        End Try
        RunningBackups = ""

    End Sub

#End Region

#Region "File Backup"

    ReadOnly IARLock As New Object
    Private disposedValue As Boolean

    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code. Put cleanup code in 'Dispose(disposing As Boolean)' method
        Dispose(disposing:=True)
        GC.SuppressFinalize(Me)
    End Sub

    Public Sub RunAllBackups(run As Boolean)

        Dim currentdatetime As Date = Date.Now
        If run Then
            TextPrint($"{currentdatetime.ToLocalTime} {My.Resources.AutomaticBackupIsRunning}")
            RunFullBackupThread()
            Return
        End If

        If Not _initted Then
            _initted = True
            _startDate = New DateTime()
            _startDate = Date.Now
            Settings.StartDate = _startDate
            Settings.SaveSettings()
        End If

        Dim originalBoottime As Date = Settings.StartDate
        originalBoottime = originalBoottime.AddMinutes(CDbl(Settings.AutobackupInterval))

        If DateTime.Compare(currentdatetime, originalBoottime) > 0 Then
            Settings.StartDate = currentdatetime ' wait another interval
            Settings.SaveSettings()
            If Settings.AutoBackup Then
                TextPrint($"{currentdatetime.ToLocalTime} {My.Resources.AutomaticBackupIsRunning}")
                RunFullBackupThread()
            End If
        End If

    End Sub

    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects)
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override finalizer
            ' TODO: set large fields to null
            disposedValue = True
        End If
    End Sub

    Private Shared Sub BackupFsassets()

        If Settings.BackupFSAssets Then
            Dim Name = "FsAssets"
            RunningBackups = Name
            Try
                    Dim f As String
                    If Settings.BaseDirectory.ToUpper(Globalization.CultureInfo.InvariantCulture) = "./FSASSETS" Then
                        f = IO.Path.Combine(Settings.OpensimBinPath, "FSAssets")
                    Else
                        f = Settings.BaseDirectory
                    End If
                    If Directory.Exists(f) Then
                        Dim dest = IO.Path.Combine(BackupPath, "FSassets")
                        Dim args = $"""{f}"" ""{dest}"" /E /M /TBD /IM /J "

                        '/E  Everything including empty folders
                        '/M Modified with the A bit set, then clears the bit
                        '/TBD wait for share names to be defined
                        '/IM include files with modified times
                        '/J Unbuffered IO

                        Dim rev = System.Environment.OSVersion.Version.Major
                        ' Only on 10
                        If rev = 10 Then args += "/LFSM:50M"

                        Dim win = IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "robocopy.exe")
                        Debug.Print(args)
                        Using ProcessRobocopy As New Process With {
                            .EnableRaisingEvents = True
                        }
                            Dim pi = New ProcessStartInfo With {
                            .Arguments = args,
                            .FileName = win,
                            .UseShellExecute = True,
                            .CreateNoWindow = True
                        }
                            ProcessRobocopy.StartInfo = pi

                            If Settings.ShowFsAssetBackup Then
                                ProcessRobocopy.StartInfo.WindowStyle = ProcessWindowStyle.Normal
                                ProcessRobocopy.StartInfo.CreateNoWindow = False
                            Else
                                ProcessRobocopy.StartInfo.WindowStyle = ProcessWindowStyle.Minimized
                                ProcessRobocopy.StartInfo.CreateNoWindow = True
                            End If

                            Try
                                ProcessRobocopy.Start()
                            Catch ex As Exception
                                Break(ex.Message)
                            End Try
                        End Using
                    End If
                Catch ex As Exception
                    Break(ex.Message)
                End Try
            RunningBackups = ""
        End If

    End Sub

    ''' <summary>
    ''' Run all backups in sequence
    ''' </summary>
    Private Sub FullBackupThread()

        RunMainZip()
        RunAllSQLBackups()
        BackupFsassets()

    End Sub

    ''' <summary>
    '''  Save IARs as a background task
    ''' </summary>
    Private Sub RunBackupIARThread()

        If Settings.BackupIARs Then
            Dim Name = "IAR"
            RunningBackups = Name
            SyncLock IARLock
                ' Make IAR options
                Dim RegionName = "TEMP"
                Dim RegionUUID = FindRegionByName(RegionName)
                If RegionUUID.Length = 0 Then
                    RegionUUID = CreateRegionStruct(RegionName)
                    WriteRegionObject(RegionName, RegionName)
                    Settings.SaveSettings()
                    PropChangedRegionSettings = True
                    GetAllRegions(False)
                    Estate(RegionUUID) = "SimSurround"
                    SetEstate(RegionUUID, 1999)
                End If

                Dim obj As New TaskObject With {
                    .TaskName = FormSetup.TaskName.SaveAllIARS
                }
                FormSetup.RebootAndRunTask(RegionUUID, obj)
            End SyncLock
            RunningBackups = ""
        End If

    End Sub

    Private Sub RunFullBackupThread()

        RunBackupIARThread()

        _WebThread3 = New Thread(AddressOf FullBackupThread)
        _WebThread3.SetApartmentState(ApartmentState.STA)
        _WebThread3.Priority = ThreadPriority.BelowNormal
        _WebThread3.Start()

    End Sub

    Private Sub RunMainZip()

        Dim Name = "Settings"
        RunningBackups = Name
        Dim zipused As Boolean
        'used to zip it, zip it good
        _folder = IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\tmp\Backup_" & DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss", Globalization.CultureInfo.InvariantCulture))
        FileIO.FileSystem.CreateDirectory(_folder)

        Dim Foldername = "Backup_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss", Globalization.CultureInfo.InvariantCulture)   ' Set default folder
        Dim Bak = IO.Path.Combine(_folder, Foldername & ".zip")

        Using Z = New ZipFile(Bak) With {
                .UseZip64WhenSaving = Zip64Option.AsNecessary,
                .CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression
            }

            Try
                If Settings.BackupWifi Then
                    Z.AddDirectory(IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Apache\htdocs\jOpensim\"), "jOpensim")
                    Z.AddDirectory(IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Opensim\WifiPages-Custom\"), "WifiPages-Custom")
                    zipused = True
                End If
            Catch ex As Exception
                Break(ex.Message)
            End Try

            If Settings.BackupSettings Then
                Try
                    Z.AddFile(IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Settings.ini"), "Settings")
                    Z.AddFile(IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\XYSettings.ini"), "Settings")

                    Dim fs = IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Opensim\bin\LocalUserStatistics.db")
                    If File.Exists(fs) Then Z.AddFile(fs, "Stats Database in bin")
                    fs = IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Photo.png")
                    If File.Exists(fs) Then Z.AddFile(fs, "Photos")
                    fs = IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\NewBlack.png")
                    If File.Exists(fs) Then Z.AddFile(fs, "Photos")
                    fs = IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\NewWhite.png")
                    If File.Exists(fs) Then Z.AddFile(fs, "Photos")
                    fs = IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\NewCustom.png")
                    If File.Exists(fs) Then Z.AddFile(fs, "Photos")
                    zipused = True
                Catch ex As Exception
                    Break(ex.Message)
                End Try
            End If

            If Settings.BackupRegion Then
                Dim sourcePath = IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Opensim\bin\Regions")
                Dim sourceDirectoryInfo As New System.IO.DirectoryInfo(sourcePath)
                For Each fileSystemInfo In sourceDirectoryInfo.GetDirectories
                    Try
                        Dim folder = fileSystemInfo.FullName
                        Dim Regionpath = IO.Path.Combine(Settings.CurrentDirectory, folder & "\Region")
                        Dim RegionDirectoryInfo As New System.IO.DirectoryInfo(Regionpath)
                        For Each Region In RegionDirectoryInfo.GetFileSystemInfos
                            If Name.EndsWith(".ini", StringComparison.OrdinalIgnoreCase) Then
                                Dim shortname = Region.Name.Replace("ini", "")
                                Z.AddFile(IO.Path.Combine(Regionpath, Region.Name), $"\Regions\{shortname}\Region\")
                                zipused = True
                            End If
                        Next
                    Catch ex As Exception
                        Break(ex.Message)
                    End Try
                Next
            End If

            Try
                If zipused = True Then
                    Z.Save()
                    Sleep(5000)
                    MoveFile(Bak, IO.Path.Combine(BackupPath, Foldername & ".zip"))
                    Sleep(1000)
                End If

                DeleteFolder(_folder)
            Catch ex As Exception
                Break(ex.Message)
            End Try

        End Using
        RunningBackups = ""

    End Sub

#End Region

End Class
