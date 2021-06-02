Module IAR

#Region "Load"

    Public Sub LoadIAR()

        If PropOpensimIsRunning() Then
            ' Create an instance of the open file dialog box. Set filter options and filter index.
            Dim openFileDialog1 As OpenFileDialog = New OpenFileDialog With {
                            .InitialDirectory = """" & IO.Path.Combine(Settings.CurrentDirectory, "Outworldzfiles") & """",
                            .Filter = Global.Outworldz.My.Resources.IAR_Load_and_Save_word & " (*.iar)|*.iar|All Files (*.*)|*.*",
                            .FilterIndex = 1,
                            .Multiselect = False
                        }

            ' Call the ShowDialog method to show the dialog box.
            Dim UserClickedOK As DialogResult = openFileDialog1.ShowDialog

            ' Process input if the user clicked OK.
            If UserClickedOK = DialogResult.OK Then
                Dim thing = openFileDialog1.FileName
                If thing.Length > 0 Then
                    thing = thing.Replace("\", "/")    ' because Opensim uses Unix-like slashes, that's why
                    If LoadIARContent(thing) Then
                        TextPrint(My.Resources.isLoading & " " & thing)
                    End If
                End If
            End If
            openFileDialog1.Dispose()
        Else
            TextPrint(My.Resources.Not_Running)
        End If

    End Sub

    Public Function LoadIARContent(thing As String) As Boolean

        ' handles IARS clicks
        If Not PropOpensimIsRunning() Then
            TextPrint(My.Resources.Not_Running)
            Return False
        End If

        Dim UUID As String = ""

        Try
            ' find one that is running
            For Each RegionUUID As String In PropRegionClass.RegionUuids
                If PropRegionClass.IsBooted(RegionUUID) Then
                    UUID = RegionUUID
                    Exit For
                End If
                Application.DoEvents()
            Next
        Catch
        End Try

        Dim out As New Guid
        If Not Guid.TryParse(UUID, out) Then
            MsgBox(My.Resources.No_Regions_Ready, MsgBoxStyle.Information Or MsgBoxStyle.MsgBoxSetForeground, Global.Outworldz.My.Resources.Info_word)
            Return False
        End If

        Using LoadIAR As New FormIARLoad
            LoadIAR.ShowDialog()
            Dim chosen = LoadIAR.DialogResult()
            If chosen = DialogResult.OK Then
                Dim p As String = LoadIAR.GFolder
                If p = "" Then p = "/"
                Dim u As String = LoadIAR.GAvatar
                If u Is Nothing Then
                    TextPrint(My.Resources.Canceled_IAR)
                    Return False
                End If
                If u.Length > 0 Then
                    ConsoleCommand(UUID, "load iar --merge " & u & " " & p & " " & """" & thing & """")
                    SendMessage(UUID, "IAR content Is loading")
                    TextPrint(My.Resources.isLoading & vbCrLf & p)
                Else
                    TextPrint(My.Resources.Canceled_IAR)
                End If
            End If

        End Using
        Return True

    End Function

    Public Sub SaveIARTask()

        If PropOpensimIsRunning() Then

            Using SaveIAR As New FormIARSave
                SaveIAR.ShowDialog()
                Dim chosen = SaveIAR.DialogResult()
                If chosen = DialogResult.OK Then

                    Dim itemName = SaveIAR.GObject
                    If itemName = "/=everything, /Objects/Folder, etc." Then
                        itemName = "/"
                    End If

                    If itemName.Length = 0 Then
                        MsgBox(My.Resources.MustHaveName, MsgBoxStyle.Information Or MsgBoxStyle.MsgBoxSetForeground)
                        Return
                    End If

                    Dim ToBackup As String
                    Dim BackupName = SaveIAR.GBackupName

                    If Not BackupName.EndsWith(".iar", StringComparison.InvariantCultureIgnoreCase) Then
                        BackupName += ".iar"
                    End If

                    If String.IsNullOrEmpty(SaveIAR.GBackupPath) Or SaveIAR.GBackupPath = "AutoBackup" Then
                        ToBackup = IO.Path.Combine(BackupPath(), BackupName)
                    Else
                        ToBackup = BackupName
                    End If

                    Dim Name = SaveIAR.GAvatarName

                    Dim opt As String = "  "
                    If Settings.DNSName.Length > 0 Then
                        opt += " -h " & Settings.DNSName & " "
                    End If

                    Dim Perm As String = ""
                    If Not SaveIAR.GCopy Then
                        Perm += "C"
                    End If

                    If Not SaveIAR.GTransfer Then
                        Perm += "T"
                    End If

                    If Not SaveIAR.GModify Then
                        Perm += "M"
                    End If

                    If Perm.Length > 0 Then
                        opt += " --perm=" & Perm & " "
                    End If

                    For Each RegionUUID As String In PropRegionClass.RegionUuids
                        Try
                            If PropRegionClass.IsBooted(RegionUUID) Then
                                ConsoleCommand(RegionUUID, "save iar " & opt & Name & " " & """" & itemName & """" & " " & """" & ToBackup & """")
                                TextPrint(My.Resources.Saving_word & " " & BackupPath() & "\" & BackupName & ", Region " & PropRegionClass.RegionName(RegionUUID))
                                Exit For
                            End If
                        Catch
                        End Try
                    Next
                End If
            End Using
        Else
            TextPrint(My.Resources.Not_Running)
        End If
    End Sub

#End Region

End Module
