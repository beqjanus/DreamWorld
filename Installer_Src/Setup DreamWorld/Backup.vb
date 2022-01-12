Module Backup
    Private _lastbackup As Integer

#Region "Backups"

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
            ElseIf _lastbackup > 0 And count > 1 Then
                text = dt & " " & CStr(count) & " " & "backups running"
            ElseIf _lastbackup > 0 And count = 0 Then
                text = dt & " backup completed"
            End If
        End If
        _lastbackup = count
        Return text

    End Function

#End Region

#Region "Tasks"

    Public Sub BackupAllRegions()

        Dim L = RegionUuids()
        L.Sort()

        For Each RegionUUID As String In L
            If Not RegionEnabled(RegionUUID) Then Continue For
            If SkipAutobackup(RegionUUID) = "True" Then Continue For

            Dim Obj = New TaskObject With {
                .TaskName = FormSetup.TaskName.RPCBackupper,
                .Command = ""
            }
            FormSetup.RebootAndRunTask(RegionUUID, Obj)
        Next
    End Sub

    Public Sub RPCBackupper(RegionUUID As String)

        ConsoleCommand(RegionUUID, "change region " & """" & Region_Name(RegionUUID) & """")
        ConsoleCommand(RegionUUID, "save oar " & """" & BackupPath() & "/" & Region_Name(RegionUUID) & "_" &
                           DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss", Globalization.CultureInfo.InvariantCulture) & ".oar" & """")

    End Sub

#End Region

End Module
