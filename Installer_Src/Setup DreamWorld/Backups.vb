Imports System.Threading
Imports System.IO
Imports System.IO.Compression

Public Class Backups

    Private _WebThread1 As Thread
    Private _WebThread2 As Thread
    Private _WebThread3 As Thread

    Private _startDate As Date
    Private _initted As Boolean
    Private _folder As String
    Private _filename As String

    Public Sub New()

    End Sub

    Public Sub BackupSQLDB(DBName As String)

        If Not FormSetup.StartMySQL() Then
            FormSetup.ToolBar(False)
            FormSetup.Buttons(FormSetup.StartButton)
            FormSetup.Print(My.Resources.Stopped_word)
            Return
        End If

        ' used to zip it, zip it good
        _folder = IO.Path.Combine(Settings.CurrentDirectory, "tmp\" & CStr(RandomNumber.Random))
        If Not System.IO.Directory.Exists(_folder) Then MkDir(_folder)

        SQLBackup(DBName)

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

    Public Sub SQLBackup(DBName As String)

        Dim currentdatetime As Date = Date.Now()
        FormSetup.Print(currentdatetime.ToLocalTime & vbCrLf & DBName & " " & My.Resources.Slow_Backup)
        BackupMysql(DBName)

    End Sub

    Public Sub RunSQLBackup(OP As Object)

        If OP Is Nothing Then Return

        Dim Name As String = OP.ToString

        Dim currentdatetime As Date = Date.Now()
        Dim whenrun As String = currentdatetime.ToString("yyyy-MM-dd_HH_mm_ss", Globalization.CultureInfo.InvariantCulture)

        _filename = Name & "_" & whenrun & ".sql"

        ' we must write this to the file so it knows what database to use.
        Using outputFile As New StreamWriter(IO.Path.Combine(_folder, _filename))
            outputFile.Write("use " & Name & ";" + vbCrLf)
        End Using

        Dim ProcessSqlDump As Process = New Process With {
            .EnableRaisingEvents = True
        }

        Dim port As String
        Dim host As String
        Dim password As String
        Dim user As String
        Dim dbname As String
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
        & " --result-file=" & """" & IO.Path.Combine(_folder, _filename) & """" _
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
            OpensimBackupRunning += 1
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
            Return
        End Try

        ProcessSqlDump.WaitForExit()

        Dim Bak = IO.Path.Combine(BackupPath, _filename & ".zip")
        FileStuff.DeleteFile(Bak)

        ZipFile.CreateFromDirectory(_folder, Bak, CompressionLevel.Optimal, False)
        Thread.Sleep(1000)
        FileStuff.DeleteDirectory(_folder, FileIO.DeleteDirectoryOption.DeleteAllContents)

        OpensimBackupRunning -= 1

    End Sub

    Public Sub BackupMysql(name As String)

        _WebThread1 = New Thread(AddressOf RunSQLBackup)
        _WebThread1.SetApartmentState(ApartmentState.STA)
        _WebThread1.Start(name)
        _WebThread1.Priority = ThreadPriority.BelowNormal

    End Sub

    Public Sub RunAllBackups(Optional force As Boolean = False)

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

        Dim directory As New System.IO.DirectoryInfo(BackupPath)
        Dim File As System.IO.FileInfo() = directory.GetFiles()
        Dim File1 As System.IO.FileInfo

        ' get each file's last modified date
        For Each File1 In File
            If File1.Name.StartsWith("Full_Backup_", StringComparison.InvariantCultureIgnoreCase) Then
                Dim strLastModified As Date = System.IO.File.GetLastWriteTime(BackupPath() & "\" & File1.Name)
                strLastModified = strLastModified.AddDays(CDbl(Settings.KeepForDays))
                Dim y = DateTime.Compare(currentdatetime, strLastModified)
                If DateTime.Compare(currentdatetime, strLastModified) > 0 Then
                    FileStuff.DeleteFile(File1.FullName)
                End If
            End If
        Next

    End Sub

    Private Sub FullBackup()

        ' used to zip it, zip it good
        _folder = IO.Path.Combine(Settings.CurrentDirectory, "tmp\" & CStr(RandomNumber.Random))
        If Not System.IO.Directory.Exists(_folder) Then MkDir(_folder)

        Dim Foldername = "Full_backup_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss", Globalization.CultureInfo.InvariantCulture)   ' Set default folder
        Dim Destination = IO.Path.Combine(BackupPath() & "\tmp", Foldername)
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

        Dim Bak = IO.Path.Combine(BackupPath, Foldername & ".zip")
        FileStuff.DeleteFile(Bak)
        ZipFile.CreateFromDirectory(_folder, Bak, CompressionLevel.Optimal, False)
        Thread.Sleep(1000)
        FileStuff.DeleteDirectory(_folder, FileIO.DeleteDirectoryOption.DeleteAllContents)

    End Sub

End Class
