#Region "Copyright AGPL3.0"

' Copyright Outworldz, LLC.
' AGPL3.0  https://opensource.org/licenses/AGPL

#End Region

Module Updater

    Private WithEvents UpdateProcess As New Process()
    Public SkipUntilVersion As Double = 0

#Region "Updater"

    ''' <summary>Checks the Outworldz Web site to see if a new version exist,.</summary>
    Public Sub CheckForUpdates()

        Dim ReleasedVersion As Double
        Dim MyVersion = CDbl(PropMyVersion)

        Using client As New Net.WebClient ' download client for web pages
            TextPrint(My.Resources.Checking_for_Updates_word)
            Try
                ReleasedVersion = CDbl(client.DownloadString(PropHttpsDomain & "/Outworldz_Installer/UpdateGrid.plx" & GetPostData()))
            Catch ex As Exception
                ErrorLog(My.Resources.Wrong & " " & ex.Message)
                Return
            End Try
        End Using

        ' Update Error check could be nothing
        If ReleasedVersion = 0 Then ReleasedVersion = CDbl(PropMyVersion)

        Try
            ' check if less than the last skipped update
            ' could be the same or later version already
            If ReleasedVersion <= Settings.SkipUpdateCheck Then
                Return
            End If

            ' Check if update is <= Current version, if so skip
            If ReleasedVersion <= MyVersion Then
                Return
            End If
        Catch ex As Exception
            BreakPoint.Dump(ex)
            Return
        End Try

        TextPrint(My.Resources.Update_is_available)

        ShowUpdateForm()

    End Sub

    Public Sub ShowUpdateForm()

#Disable Warning CA2000
        Dim FormUpdate = New FormUpdate()
#Enable Warning CA2000
        FormUpdate.Init()
        FormUpdate.BringToFront()
        Dim doUpdate = FormUpdate.ShowDialog()

        If doUpdate = DialogResult.No Then

            '  remind me later
            Settings.SkipUpdateCheck() = CDbl(PropMyVersion)
            Settings.SaveSettings()

        ElseIf doUpdate = DialogResult.Yes Then

            ' Yes, Update
            If FormSetup.DoStopActions() = False Then Return

            Settings.SkipUpdateCheck = 0

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
