Imports System.Threading
Imports System.IO
Imports System.IO.Compression

Module Backups

    Private _WebThread1 As Thread
    Private _WebThread2 As Thread
    Private _WebThread3 As Thread

    Private _OpensimBackupRunning As Boolean
    Private _startDate As Date
    Private _initted As Boolean
    Private _Busy As Boolean
    Private _folder As String
    Private _filename As String

    Public Sub ClearFlags()

        _Busy = False
        _OpensimBackupRunning = False

    End Sub

    Private Sub Zipup()

        Dim f = _filename.Replace(".sql", ".zip")
        Try
            ZipFile.CreateFromDirectory(IO.Path.Combine(_folder, "tmp"), IO.Path.Combine(_folder, f), CompressionLevel.Optimal, False)
        Catch
        End Try
        Thread.Sleep(5000)
        FileStuff.DeleteDirectory(IO.Path.Combine(_folder, "tmp"), FileIO.DeleteDirectoryOption.DeleteAllContents)

    End Sub

    Public Function BackupRunning() As Boolean

        Dim isrunning As Boolean
        Try
            If _WebThread1.IsAlive Then isrunning = True
        Catch
        End Try

        Try
            If _WebThread2.IsAlive Then isrunning = True
        Catch
        End Try

        Try
            If _WebThread3.IsAlive Then isrunning = True
        Catch
        End Try
        Return isrunning

    End Function

    Public Sub SQLBackup()

        If _Busy = True Then
            FormSetup.Print("Backup is already running")
            Return
        End If
        _Busy = True
        FormSetup.Print(My.Resources.Slow_Backup)
        BackupMysql(Settings.RegionDBName)

    End Sub

    Private Sub Exited(sender As Object, e As System.EventArgs)

        If Not _OpensimBackupRunning Then
            _OpensimBackupRunning = True
            Zipup()
            BackupMysql(Settings.RobustDataBaseName)
        Else
            Zipup()
            _OpensimBackupRunning = False
            _Busy = False
        End If

    End Sub

    Public Function BackupPath() As String

        'Autobackup must exist. if not create it
        ' if they set the folder somewhere else, it may have been deleted, so reset it to default
        If Settings.BackupFolder.ToUpper(Globalization.CultureInfo.InvariantCulture) = "AUTOBACKUP" Then
            BackupPath = IO.Path.Combine(FormSetup.PropCurSlashDir, "OutworldzFiles/AutoBackup")
            Settings.BackupFolder = BackupPath
            If Not Directory.Exists(BackupPath) Then
                MkDir(BackupPath)
            End If
        Else
            BackupPath = Settings.BackupFolder
            BackupPath = BackupPath.Replace("\", "/")    ' because Opensim uses Unix-like slashes, that's why
            Settings.BackupFolder = BackupPath
            If Not Directory.Exists(BackupPath) Then
                BackupPath = IO.Path.Combine(FormSetup.PropCurSlashDir, "OutworldzFiles/Autobackup")
                If Not Directory.Exists(BackupPath) Then
                    MkDir(BackupPath)
                End If
                MsgBox(My.Resources.Autobackup_cannot_be_located & BackupPath)
            End If
        End If
        Return BackupPath

    End Function

    Public Sub RunSQLBackup(OP As Object)

        Dim Name As String = OP.ToString

        Dim currentdatetime As Date = Date.Now()
        Dim whenrun As String = currentdatetime.ToString("yyyy-MM-dd_HH_mm_ss", Globalization.CultureInfo.InvariantCulture)

        Dim what = Name & "_" & whenrun & ".sql"
        ' used to zip it, zip if good
        _folder = Backups.BackupPath
        _filename = what

        ' make sure this is empty as we use it again and might have crashed
        FileStuff.DeleteDirectory(_folder & "\tmp\", FileIO.DeleteDirectoryOption.DeleteAllContents)
        Try
            MkDir(_folder & "\" & "tmp")
        Catch
        End Try

        ' we must write this to the file so it knows what database to use.
        Using outputFile As New StreamWriter(_folder & "\tmp\" & what)
            outputFile.Write("use " & Name & ";" + vbCrLf)
        End Using

        Dim ProcessSqlDump As Process = New Process With {
            .EnableRaisingEvents = True
        }

        AddHandler ProcessSqlDump.Exited, AddressOf Exited

        Dim port As String = ""
        Dim host As String = ""
        Dim password As String = ""
        Dim user As String = ""
        Dim dbname As String = ""
        If OP = Settings.RobustDataBaseName Then
            port = CStr(Settings.MySqlRobustDBPort)
            host = Settings.RobustServer
            user = Settings.RobustUsername
            password = Settings.RobustPassword
            dbname = Settings.RobustDataBaseName
        Else
            port = CStr(Settings.MySqlRegionDBPort)
            host = Settings.RegionServer
            user = Settings.RegionDBUsername
            password = Settings.RegionDbPassword
            dbname = Settings.RegionDBName
        End If

        Dim options = " --host=" & host & " --port=" & port _
        & " --opt --hex-blob --add-drop-table --allow-keywords  " _
        & " -u" & user _
        & " -p" & password _
        & " --verbose --log-error=Mysqldump.log " _
        & " --result-file=" & """" & Backups.BackupPath & "\tmp\" & what & """" _
        & " " & dbname
        Debug.Print(options)
        '--host=127.0.0.1 --port=3306 --opt --hex-blob --add-drop-table --allow-keywords  -uroot
        ' --verbose --log-error=Mysqldump.log
        ' --result-file="C:\Opensim\Outworldz_Dreamgrid\OutworldzFiles\AutoBackup\tmp\opensim_2020-12-09_00_25_24.sql"
        ' opensim

        Dim pi As ProcessStartInfo = New ProcessStartInfo With {
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
            BreakPoint.Show(ex.Message)
            Return
        End Try

        ProcessSqlDump.WaitForExit()
        _Busy = False

    End Sub

    Public Sub BackupMysql(name As String)

        _WebThread1 = New Thread(AddressOf RunSQLBackup)
        _WebThread1.SetApartmentState(ApartmentState.STA)
        _WebThread1.Start(name)
        _WebThread1.Priority = ThreadPriority.BelowNormal

    End Sub

    Public Sub RunBackups(Optional force As Boolean = False)

        Dim currentdatetime As Date = Date.Now

        If force Then
            FormSetup.Print(currentdatetime.ToLocalTime & " Backup Running")
            _WebThread2 = New Thread(AddressOf FullBackup)
            Try
                _WebThread2.SetApartmentState(ApartmentState.STA)
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
            End Try
            _WebThread2.Start()
            _WebThread2.Priority = ThreadPriority.BelowNormal
            Return
        End If

        If Not _initted Then
            _initted = True
            _startDate = New DateTime()
            _startDate = Date.Now
        End If

        Dim originalBoottime As Date = _startDate
        originalBoottime = originalBoottime.AddMinutes(CDbl(Settings.AutobackupInterval))

        Dim x = DateTime.Compare(currentdatetime, originalBoottime)
        If DateTime.Compare(currentdatetime, originalBoottime) > 0 Then

            _startDate = currentdatetime ' wait another interval

            If Settings.AutoBackup Then
                FormSetup.Print(currentdatetime.ToLocalTime & " Auto Backup Running")
                _WebThread3 = New Thread(AddressOf FullBackup)
                Try
                    _WebThread3.SetApartmentState(ApartmentState.STA)
                Catch ex As Exception
                    BreakPoint.Show(ex.Message)
                End Try
                _WebThread3.Start()
                _WebThread3.Priority = ThreadPriority.BelowNormal
            End If
        End If

        ' delete old files
        originalBoottime = _startDate

        Dim directory As New System.IO.DirectoryInfo(Backups.BackupPath)
        Dim File As System.IO.FileInfo() = directory.GetFiles()
        Dim File1 As System.IO.FileInfo

        ' get each file's last modified date
        For Each File1 In File
            If File1.Name.StartsWith("Full_Backup_", StringComparison.InvariantCultureIgnoreCase) Then
                Dim strLastModified As Date = System.IO.File.GetLastWriteTime(Backups.BackupPath & "\" & File1.Name)
                strLastModified = strLastModified.AddDays(CDbl(Settings.KeepForDays))
                Dim y = DateTime.Compare(currentdatetime, strLastModified)
                If DateTime.Compare(currentdatetime, strLastModified) > 0 Then
                    FileStuff.DeleteFile(File1.FullName)
                End If
            End If
        Next

    End Sub

    Private Sub FullBackup()

        Dim Foldername = "Full_backup_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss", Globalization.CultureInfo.InvariantCulture)   ' Set default folder

        Dim Destination = IO.Path.Combine(Backups.BackupPath & "\tmp", Foldername)

        Try
            If Not Directory.Exists(Destination) Then
                MkDir(Destination)
            End If
        Catch
            Return
        End Try

        If Settings.BackupMysql Then
            Try
                My.Computer.FileSystem.CreateDirectory(IO.Path.Combine(Destination, "Opensim_bin_Regions"))
            Catch ex As Exception
            End Try

            FileStuff.CopyFolder(IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Opensim\bin\Regions"), IO.Path.Combine(Destination, "Opensim_bin_Regions"))
            Application.DoEvents()

        End If

        If Settings.BackupMysql Then
            Try
                My.Computer.FileSystem.CreateDirectory(IO.Path.Combine(Destination, "Mysql_Data"))
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
            End Try
            FileStuff.CopyFolder(IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Mysql\Data"), IO.Path.Combine(Destination, "Mysql_Data"))
            Application.DoEvents()
        End If

        If Settings.BackupFSAssets Then
            Try
                My.Computer.FileSystem.CreateDirectory(IO.Path.Combine(Destination, "FSAssets"))
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
            End Try

            Dim folder As String = "./fsassets"
            If Settings.BaseDirectory = "./fsassets" Then
                folder = Settings.OpensimBinPath & "\FSAssets"
            Else
                folder = Settings.BaseDirectory
            End If
            FileStuff.CopyFolder(folder, IO.Path.Combine(Destination, "FSAssets"))
            Application.DoEvents()
        End If

        If Settings.BackupWifi Then
            Try
                My.Computer.FileSystem.CreateDirectory(IO.Path.Combine(Destination, "Opensim_WifiPages-Custom"))
                My.Computer.FileSystem.CreateDirectory(IO.Path.Combine(Destination, "Opensim_bin_WifiPages-Custom"))
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
            End Try
            FileStuff.CopyFolder(IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Opensim\WifiPages\"), IO.Path.Combine(Destination, "Opensim_WifiPages-Custom"))
            FileStuff.CopyFolder(IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Opensim\bin\WifiPages\"), IO.Path.Combine(Destination, "Opensim_bin_WifiPages-Custom"))
            Application.DoEvents()
        End If

        FileStuff.CopyFile(IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Settings.ini"), IO.Path.Combine(Destination, "Settings.ini"), True)
        FileStuff.CopyFile(IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Photo.png"), IO.Path.Combine(Destination, "Photo.png"), True)

        Dim Bak = IO.Path.Combine(Backups.BackupPath, Foldername & ".zip")
        FileStuff.DeleteFile(Bak)
        ZipFile.CreateFromDirectory(Destination, Bak, CompressionLevel.Optimal, False)
        Thread.Sleep(1000)
        FileStuff.DeleteDirectory(IO.Path.Combine(Backups.BackupPath, "tmp"), FileIO.DeleteDirectoryOption.DeleteAllContents)

    End Sub

End Module
