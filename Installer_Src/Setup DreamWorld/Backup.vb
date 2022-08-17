Imports System.Threading
Imports Ionic.Zip

Module Backup

    ''' <summary>
    ''' Waits until a file stops changing length so we can type again. Quits if DreamGrid  is stopped.
    ''' Waits for 5 minutes or the file stops growing for 30 seconds
    ''' </summary>
    ''' <param name="BackupName">Name of region to watch</param>
    Public Sub WaitforComplete(RegionUUID As String, FolderAndFileName As String)

        If SkipAutobackup(RegionUUID) = "True" Then Return

        If Not Settings.Smart_Start Or Not Smart_Start(RegionUUID) Then
            SequentialPause()
            Return
        End If

        ' pass the two parameters it needs as in object
        Dim Oar = New OarObject With {
            .RegionUUID = RegionUUID,
            .FolderAndFileName = FolderAndFileName
        }

        'if suspended, keep it alive
        If Not Settings.BootOrSuspend Then

            SequentialPause(True)

            Dim w = New WaitForOar()
            w.Data = Oar

            Dim threadDelegate = New ThreadStart(AddressOf w.Dowork)
            Dim newThread = New Thread(threadDelegate)
            newThread.Priority = ThreadPriority.BelowNormal
            newThread.Start()

            'Dim start = New Thread(WaitForOar)
            's tart.SetApartmentState(ApartmentState.STA)
            'start.Priority = ThreadPriority.BelowNormal
            'start.Start(Oar)
            'Else
            '   WaitForOar(Oar)
        End If

    End Sub

#Region "Backups"

    ''' <summary>
    ''' Zip up Regions
    ''' </summary>
    Public Sub BackupINI()

        Dim Name = "Region INI"
        RunningBackupName.TryAdd($"{Name} {My.Resources.Starting_word}", "")

        Dim zipused As Boolean
        'used to zip it, zip it good
        Dim _folder = IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\tmp\Region_" & DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss", Globalization.CultureInfo.InvariantCulture))
        FileIO.FileSystem.CreateDirectory(_folder)

        Dim Foldername = "Region_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss", Globalization.CultureInfo.InvariantCulture)   ' Set default folder
        Dim Bak = IO.Path.Combine(_folder, Foldername & ".zip")

        Using Z = New ZipFile(Bak) With {
                .UseZip64WhenSaving = Zip64Option.AsNecessary,
                .CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression
            }

            Try
                Dim sourcePath = IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Opensim\bin\Regions")
                Dim sourceDirectoryInfo As New System.IO.DirectoryInfo(sourcePath)
                For Each fileSystemInfo In sourceDirectoryInfo.GetDirectories
                    Try
                        Dim folder = fileSystemInfo.FullName
                        Dim Regionpath = IO.Path.Combine(Settings.CurrentDirectory, folder & "\Region")
                        Dim RegionDirectoryInfo As New System.IO.DirectoryInfo(Regionpath)
                        For Each RegionName In RegionDirectoryInfo.GetFileSystemInfos
                            If RegionName.Name.EndsWith(".ini", StringComparison.OrdinalIgnoreCase) Then
                                Dim shortname = RegionName.Name.Replace(".ini", "")
                                Z.AddFile(IO.Path.Combine(Regionpath, RegionName.Name), $"\Regions\{shortname}\Region\")
                                zipused = True
                            End If
                        Next
                    Catch ex As Exception
                        BreakPoint.Print(ex.Message)
                    End Try
                Next
            Catch ex As Exception
                BreakPoint.Print(ex.Message)
            End Try

            Try
                If zipused = True Then
                    Z.Save()
                    Sleep(500)
                    MoveFile(Bak, IO.Path.Combine(BackupPath, Foldername & ".zip"))
                End If

                DeleteFolder(_folder)
            Catch ex As Exception
                BreakPoint.Print(ex.Message)
            End Try

        End Using

        RunningBackupName.TryAdd($"{Name} {My.Resources.Ok}", "")

    End Sub

    ''' <summary>
    ''' Get the path to the Autobackup folder. Make it if necessary
    ''' </summary>
    ''' <returns>file path</returns>
    Public Function BackupPath() As String

        'Autobackup must exist. if not create it
        ' if they set the folder somewhere else, it may have been deleted, so reset it to default
        BackupPath = Settings.BackupFolder
        BackupPath = BackupPath.Replace("\", "/")    ' because Opensim uses Unix-like slashes, that's why

        If Not IO.Directory.Exists(BackupPath) Then
            BackupPath = IO.Path.Combine(Settings.CurrentSlashDir, "OutworldzFiles/Autobackup")
            FileIO.FileSystem.CreateDirectory(BackupPath)
            Settings.BackupFolder = BackupPath
        End If

        Return BackupPath

    End Function

#End Region

#Region "Tasks"

    ''' <summary>
    ''' Background backup all OARS as a thread
    ''' </summary>
    Public Sub BackupAllRegions()

        If Not Settings.BackupOARs Then Return
        For Each RegionUUID In RegionUuids()
            If BackupAbort Then Return
            If Not RegionEnabled(RegionUUID) Then Continue For
            If SkipAutobackup(RegionUUID) = "True" Then Continue For

            RunningBackupName.TryAdd($"{Region_Name(RegionUUID)} {My.Resources.backup_running}", "")

            Dim file = BackupPath() & "/" & Region_Name(RegionUUID) & "_" &
         DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss", Globalization.CultureInfo.InvariantCulture) & ".oar"

            Dim Obj = New TaskObject With {
                .TaskName = TaskName.LaunchBackupper,
                .Command = file
            }
            RebootAndRunTask(RegionUUID, Obj)
            WaitforComplete(RegionUUID, file)
            ShowDOSWindow(RegionUUID, MaybeHideWindow())

        Next

    End Sub

    '' must use console as otherwise Smart Start will shutdown
    Public Sub Backupper(RegionUUID As String, file As String)

        PokeRegionTimer(RegionUUID)
        ConsoleCommand(RegionUUID, $"change region ""{Region_Name(RegionUUID)}""{vbCrLf}save oar ""{file}""")

    End Sub

#End Region

End Module

Public Class OarObject

    Public FolderAndFileName As String
    Public RegionUUID As String

End Class

Public Class WaitForOar

    Public Data As New OarObject

    Public Sub Dowork()
        Dim RegionUUID = Data.RegionUUID
        Dim FolderAndFileName = Data.FolderAndFileName

        Dim Filectr As Integer = 120 ' 2 minutes to wait for oar to start saving
        Const Seconds As Integer = 120 ' wait 2 minutes as it saves to make sure it done. If it stops this long, we are done
        Dim ctr As Integer = 1200 ' 20 minutes to stop saving an OAR or we give up
        Dim s As Long
        Dim oldsize As Long = 0
        Dim same As Integer = 0

        While same < Seconds And PropOpensimIsRunning

            Debug.Print($"Waiting on {Region_Name(RegionUUID)} {CStr(same)}")
            Sleep(1000)

            Try
                Dim fi = New System.IO.FileInfo(FolderAndFileName)
                s = fi.Length
            Catch ex As Exception
                Filectr -= 1
            End Try
            If Filectr = -1 Then Continue While

            ' we get here if a file appears or 2 minutes passes

            If Filectr = 0 Then
                ' 2 minutes - abort!
                Log("Error", $"{Region_Name(RegionUUID)} failed to start saving")
                Return
            End If

            If s = oldsize And s > 0 Then
                same += 1
            Else
                same = 0
            End If
            ctr -= 1

            If ctr = 0 Then
                Log("Error", $"{Region_Name(RegionUUID)} timeout, took too long to save")
                Return
            End If

            oldsize = s

        End While
        RunningBackupName.TryAdd($"{My.Resources.Backup_word} {Region_Name(RegionUUID)} {My.Resources.Finished_with_backup_word}", "")

    End Sub

End Class
