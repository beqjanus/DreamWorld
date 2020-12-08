Imports System.Threading
Imports System.IO
Imports System.IO.Compression

Module Backups

    Private _startDate As Date
    Private _initted As Boolean

    Public Sub RunBackups()

        If Not _initted Then
            _initted = True
            _startDate = New DateTime()
            _startDate = Date.Now
        End If

        Settings.AutobackupInterval = "1"  ' !!!!! debug 1 minute

        Dim currentdatetime As Date = New DateTime()
        currentdatetime = Date.Now

        Dim originalBoottime As Date = _startDate
        originalBoottime = originalBoottime.AddMinutes(CDbl(Settings.AutobackupInterval))

        Dim x = DateTime.Compare(currentdatetime, originalBoottime) ' debug !!!
        If DateTime.Compare(originalBoottime, currentdatetime) < 0 Then

            _startDate = currentdatetime ' wait another interval

            If Settings.AutoBackup Then

                Dim WebThread = New Thread(AddressOf FullBackup)
                Try
                    WebThread.SetApartmentState(ApartmentState.STA)
                Catch ex As Exception
                    BreakPoint.Show(ex.Message)
                End Try
                WebThread.Start()
                WebThread.Priority = ThreadPriority.BelowNormal
            End If
        End If

    End Sub

    Private Sub FullBackup()

        Dim strFilepath = Settings.CurrentDirectory & "\OutworldzFiles\" & Settings.BackupFolder & "\"  'Specify path details
        Dim directory As New System.IO.DirectoryInfo(strFilepath)
        Dim File As System.IO.FileInfo() = directory.GetFiles()
        Dim File1 As System.IO.FileInfo

        Dim originalBoottime As Date = _startDate
        originalBoottime = originalBoottime.AddMinutes(CDbl(Settings.AutobackupInterval))

        ' get each file's last modified date
        For Each File1 In File
            If File1.Name.StartsWith("Full_Backup_", StringComparison.InvariantCultureIgnoreCase) Then
                Dim strLastModified As Date = System.IO.File.GetLastWriteTime(strFilepath & "\" & File1.Name)
                If DateTime.Compare(originalBoottime, strLastModified) < 0 Then
                    FileStuff.DeleteFile(File1.FullName)
                End If
            End If
        Next

        Dim Foldername = "Full_backup_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss", Globalization.CultureInfo.InvariantCulture)   ' Set default folder
        Dim Dest As String

        If Settings.BackupFolder = "AutoBackup" Then
            Dest = IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\AutoBackup\" & Foldername)
        Else
            Dest = IO.Path.Combine(Settings.BackupFolder, Foldername)
        End If

        If Settings.BackupMysql Then
            Try
                My.Computer.FileSystem.CreateDirectory(Dest)
                My.Computer.FileSystem.CreateDirectory(Dest + "\Opensim_bin_Regions")
            Catch ex As Exception
            End Try

            FileStuff.CopyFolder(IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Opensim\bin\Regions"), IO.Path.Combine(Dest, "Opensim_bin_Regions"))
            Application.DoEvents()

        End If

        If Settings.BackupMysql Then
            Try
                My.Computer.FileSystem.CreateDirectory(Dest)
                My.Computer.FileSystem.CreateDirectory(IO.Path.Combine(Dest, "Mysql_Data"))
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
            End Try
            FileStuff.CopyFolder(IO.Path.Combine(IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Mysql\Data\")), IO.Path.Combine(Dest, "Mysql_Data"))
            Application.DoEvents()
        End If

        If Settings.BackupFSAssets Then
            Try
                My.Computer.FileSystem.CreateDirectory(Dest)
                My.Computer.FileSystem.CreateDirectory(Dest + "\FSAssets")
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
            End Try

            Dim folder As String = "./fsassets"
            If Settings.BaseDirectory = "./fsassets" Then
                folder = Settings.OpensimBinPath & "\FSAssets"
            Else
                folder = Settings.BaseDirectory
            End If
            FileStuff.CopyFolder(folder, IO.Path.Combine(Dest, "FSAssets"))
            Application.DoEvents()
        End If

        If Settings.BackupWifi Then
            Try
                My.Computer.FileSystem.CreateDirectory(Dest)
                My.Computer.FileSystem.CreateDirectory(Dest + "\Opensim_WifiPages-Custom")
                My.Computer.FileSystem.CreateDirectory(Dest + "\Opensim_bin_WifiPages-Custom")
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
            End Try
            FileStuff.CopyFolder(IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Opensim\WifiPages\"), IO.Path.Combine(Dest, "Opensim_WifiPages-Custom"))
            FileStuff.CopyFolder(IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Opensim\bin\WifiPages\"), IO.Path.Combine(Dest, "Opensim_bin_WifiPages-Custom"))
            Application.DoEvents()
        End If

        FileStuff.CopyFile(IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Settings.ini"), IO.Path.Combine(Dest, "Settings.ini"), True)

        ZipFile.CreateFromDirectory(Dest, Settings.CurrentDirectory & "\OutworldzFiles\" & Settings.BackupFolder & "\" & Foldername & ".zip", CompressionLevel.Optimal, False)
        FileStuff.DeleteDirectory(Dest, FileIO.DeleteDirectoryOption.DeleteAllContents)

    End Sub

End Module
