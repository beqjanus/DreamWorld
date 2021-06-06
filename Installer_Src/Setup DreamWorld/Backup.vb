Module Backup
    Private _lastbackup As Integer

    Public Function BackupPath() As String

        'Autobackup must exist. if not create it
        ' if they set the folder somewhere else, it may have been deleted, so reset it to default
        BackupPath = Settings.BackupFolder
        BackupPath = BackupPath.Replace("\", "/")    ' because Opensim uses Unix-like slashes, that's why

        If Not IO.Directory.Exists(BackupPath) Then
            BackupPath = IO.Path.Combine(FormSetup.PropCurSlashDir, "OutworldzFiles/Autobackup")
            FileIO.FileSystem.CreateDirectory(BackupPath)
            Settings.BackupFolder = BackupPath
        End If

        Return BackupPath

    End Function

    Public Sub Backupper()

        For Each RegionUUID As String In PropRegionClass.RegionUuids

            ReBoot(RegionUUID)
            WaitForBooted(RegionUUID)

            ConsoleCommand(RegionUUID, "change region " & """" & PropRegionClass.RegionName(RegionUUID) & """")
            ConsoleCommand(RegionUUID, "save oar  " & """" & BackupPath() & "/" & PropRegionClass.RegionName(RegionUUID) & "_" &
                               DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss", Globalization.CultureInfo.InvariantCulture) & ".oar {ENTER}" & """")

        Next

    End Sub

    Public Function BackupsRunning(dt As String) As String

        Dim folder = IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\tmp")
        Dim Running() As String
        Dim count As Integer = 0
        Try
            Running = IO.Directory.GetDirectories(folder)
            count = Running.Length
        Catch
        End Try

        Dim text As String = ""

        If _lastbackup <> count Then
            If _lastbackup = 0 And count = 1 Then
                text = dt & " " & CStr(count) & " " & "backup running"
                text = dt & " " & CStr(count) & " " & "backup running"
            ElseIf _lastbackup > 0 And count > 1 Then
                text = dt & " " & CStr(count) & " " & "backups running"
            ElseIf _lastbackup > 0 And count = 0 Then
                text = dt & " backup completed"
            End If
        End If
        _lastbackup = count
        Return text

    End Function

End Module
