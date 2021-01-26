Imports System.Threading
Imports System.IO

Imports Ionic.Zip

Public Class Backups

    Private _folder As String
    Private _initted As Boolean
    Private _startDate As Date
    Private _WebThread1 As Thread
    Private _WebThread2 As Thread
    Private _WebThread3 As Thread

#Region "Public"

    Public Sub New()

        ' used to zip it, zip it good
        _folder = IO.Path.Combine(Settings.CurrentDirectory, "tmp\" & CStr(RandomNumber.Random))
        If Not Directory.Exists(_folder) Then MkDir(_folder)

    End Sub

#End Region

#Region "SQL Backup"

    Public Sub BackupSQLDB(DBName As String)

        If Not StartMySQL() Then
            FormSetup.ToolBar(False)
            FormSetup.Buttons(FormSetup.StartButton)
            TextPrint(My.Resources.Stopped_word)
            Return
        End If

        SQLBackup(DBName)

    End Sub

    Public Sub RunSQLBackup(OP As Object)

        If OP Is Nothing Then Return

        Try
            Dim Name As String = OP.ToString

            Dim currentdatetime As Date = Date.Now()
            Dim whenrun As String = currentdatetime.ToString("yyyy-MM-dd_HH_mm_ss", Globalization.CultureInfo.InvariantCulture)

            Dim _filename = Name & "_" & whenrun & ".sql"
            Dim SQLFile = IO.Path.Combine(_folder, _filename)

            ' we must write this to the file so it knows what database to use.
            Using outputFile As New StreamWriter(SQLFile)
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
        & " --result-file=" & """" & SQLFile & """" _
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

            Dim Bak = IO.Path.Combine(_folder, _filename & ".zip")
            FileStuff.DeleteFile(Bak)

            Using Zip As ZipFile = New ZipFile(Bak)
                Zip.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression
                Zip.AddFile(SQLFile)
                Zip.Save()
            End Using
            Sleep(10000)
            FileStuff.MoveFile(Bak, IO.Path.Combine(BackupPath(), _filename & ".zip"))
            Sleep(5000)
            FileStuff.DeleteFile(SQLFile)
            Sleep(1000)
            FileStuff.DeleteFolder(_folder)
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try

    End Sub

    Public Sub SQLBackup(DBName As String)

        Dim currentdatetime As Date = Date.Now()
        TextPrint(currentdatetime.ToLocalTime & vbCrLf & DBName & " " & My.Resources.Slow_Backup)
        _WebThread1 = New Thread(AddressOf RunSQLBackup)
        _WebThread1.SetApartmentState(ApartmentState.STA)
        _WebThread1.Start(DBName)
        _WebThread1.Priority = ThreadPriority.BelowNormal

    End Sub

#End Region

#Region "File Backup"

    Public Sub RunAllBackups(Optional TimerBased As Boolean = False)

        Dim currentdatetime As Date = Date.Now
        If TimerBased Then
            TextPrint(currentdatetime.ToLocalTime & " Backup Running")
            _WebThread2 = New Thread(AddressOf FullBackupThread)
            _WebThread2.SetApartmentState(ApartmentState.STA)
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

        If DateTime.Compare(currentdatetime, originalBoottime) > 0 Then
            _startDate = currentdatetime ' wait another interval

            If Settings.AutoBackup Then
                TextPrint(currentdatetime.ToLocalTime & " Auto Backup Running")
                _WebThread3 = New Thread(AddressOf FullBackupThread)
                _WebThread3.SetApartmentState(ApartmentState.STA)
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

    Private Sub FullBackupThread()

        Dim Foldername = "Full_backup_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss", Globalization.CultureInfo.InvariantCulture)   ' Set default folder
        Dim Bak = IO.Path.Combine(_folder, Foldername & ".zip")
        FileStuff.DeleteFile(Bak)
        Sleep(1000)
        Using Z As ZipFile = New ZipFile(Bak)
            Z.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression
            Sleep(1000)
            Try
                If Settings.BackupWifi Then
                    Z.AddDirectory(IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Opensim\WifiPages-Custom\"), "WifiPages-Custom")
                    Z.Save()
                    Sleep(1000)
                End If
            Catch ex As Exception
                Dim a = 1
            End Try

            Try
                If Settings.BackupRegion Then
                    Z.AddDirectory(IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Opensim\bin\Regions"), "Regions")
                    Z.Save()
                    Sleep(1000)
                End If
            Catch ex As Exception
                Dim a = 1
            End Try

            Try
                If Settings.BackupMysql Then
                    FileStuff.CopyFolder(IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Mysql\Data"),
                                        IO.Path.Combine(_folder, "MySQLData"))

                    Z.AddDirectory(IO.Path.Combine(_folder, "MySQLData"))
                    Z.Save()
                    Sleep(5000)
                    FileStuff.DeleteDirectory(IO.Path.Combine(_folder, "MySQLData"), FileIO.DeleteDirectoryOption.DeleteAllContents)
                End If
            Catch ex As Exception
                Dim a = 1
            End Try

            Try
                If Settings.BackupFSAssets Then
                    Dim f As String
                    If Settings.BaseDirectory = "./fsassets" Then
                        f = Settings.OpensimBinPath & "\FSAssets"
                    Else
                        f = Settings.BaseDirectory
                    End If

                    Z.AddDirectory(IO.Path.Combine(Settings.CurrentDirectory, f))
                    Z.Save()
                    Sleep(1000)
                End If
            Catch ex As Exception
                Dim a = 1
            End Try

            Try
                Z.Save()
                Sleep(5000)
                FileStuff.MoveFile(Bak, IO.Path.Combine(BackupPath, Foldername & ".zip"))
                Sleep(5000)
                FileStuff.DeleteFolder(_folder)
            Catch ex As Exception
                Dim a = 1
            End Try

            Try
                If Settings.BackupSQL Then
                    Dim A As New Backups
                    A.BackupSQLDB(Settings.RegionDBName)
                    Dim B As New Backups
                    B.BackupSQLDB(Settings.RobustDataBaseName)
                End If
            Catch ex As Exception
                Dim a = 1
            End Try



        End Using

    End Sub

#End Region

End Class
