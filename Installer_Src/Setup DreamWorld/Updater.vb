#Region "Copyright AGPL3.0"

' Copyright Outworldz, LLC.
' AGPL3.0  https://opensource.org/licenses/AGPL

#End Region

Module Updater

    Private WithEvents UpdateProcess As New Process()

#Region "Updater"

    ''' <summary>Checks the Outworldz Web site to see if a new version exist,.</summary>
    Public Sub CheckForUpdates()

        Dim Update_To_version As String

        Using client As New Net.WebClient ' download client for web pages
            TextPrint(My.Resources.Checking_for_Updates_word)
            Try
                Update_To_version = client.DownloadString(PropHttpsDomain & "/Outworldz_Installer/UpdateGrid.plx" & GetPostData())
            Catch ex As Exception
                ErrorLog(My.Resources.Wrong & " " & ex.Message)
                Return
            End Try
        End Using

        ' Update Error check could be nothing
        If Update_To_version.Length = 0 Then Update_To_version = PropMyVersion

        Try
            ' check if less than the last skipped update
            Dim uv As Single = Convert.ToSingle(Update_To_version, Globalization.CultureInfo.InvariantCulture)
            Dim ToVersion = Convert.ToSingle(Settings.SkipUpdateCheck, Globalization.CultureInfo.InvariantCulture)
            ' could be the same or later version already
            If uv <= ToVersion Then
                Return
            End If

            ' Check if update is <= Current version, if so skip
            If uv <= Convert.ToSingle(PropMyVersion, Globalization.CultureInfo.InvariantCulture) Then
                Return
            End If
        Catch ex As Exception
            BreakPoint.Dump(ex)
            Return
        End Try

        TextPrint($"{My.Resources.Update_is_available}: {PropMyVersion}==>{Update_To_version}")

        If CDbl(Settings.SkipUpdateCheck()) = -1 Then Return

        ShowUpdateForm(Update_To_version)

    End Sub

    Public Sub ShowUpdateForm(Update_To_version As String)

#Disable Warning CA2000
        Dim FormUpdate = New FormUpdate()
#Enable Warning CA2000
        FormUpdate.Init($"{PropMyVersion}==>{Update_To_version} {vbCrLf}")
        FormUpdate.BringToFront()
        Dim doUpdate = FormUpdate.ShowDialog()

        If doUpdate = DialogResult.OK Then
            ' do not show again
            Settings.SkipUpdateCheck() = "-1"
            Settings.SaveSettings()
        ElseIf doUpdate = DialogResult.No Then
            '  remind me later
            Settings.SkipUpdateCheck() = Update_To_version
            Settings.SaveSettings()
        ElseIf doUpdate = DialogResult.Yes Then
            ' Yes, Update

            If FormSetup.DoStopActions() = False Then Return

            Dim pi As ProcessStartInfo = New ProcessStartInfo With {
                .WindowStyle = ProcessWindowStyle.Normal,
                .FileName = IO.Path.Combine(Settings.CurrentDirectory, "DreamGridUpdater.exe")
            }

            UpdateProcess.StartInfo = pi
            Try
                UpdateProcess.Start()
                End
            Catch ex As Exception
                BreakPoint.Dump(ex)
                TextPrint(My.Resources.ErrUpdate)
            End Try
        End If

    End Sub

#End Region

End Module
