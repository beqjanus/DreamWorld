Public Class FormUpdate

    Public Sub Init()

        InstallButton.Text = My.Resources.UpdateNow
        Button2.Text = My.Resources.RemindMeLater
        RichTextBox3.SelectAll()
        RichTextBox3.SelectionIndent += 15 ' play With this values To match yours
        RichTextBox3.SelectionRightIndent += 15 ' this too
        RichTextBox3.SelectionLength = 0
        ' this Is a little hack because without this
        ' I've got the first line of my richTB selected anyway.
        RichTextBox3.SelectionBackColor = RichTextBox3.BackColor

        Dim BetaVersion As Double
        Dim ReleasedVersion As Double
        Dim Revisions As String
        Dim MyVersion = CDbl(PropMyVersion)
        Dim textbox As String = ""

        Using client As New Net.WebClient ' download client for web pages
            TextPrint(My.Resources.Checking_for_Updates_word)
            Try
                Revisions = vbCrLf & client.DownloadString(PropHttpsDomain & "/Outworldz_Installer/revisions.txt")
            Catch ex As Exception
                TextPrint("There is something wrong with the Internet. Try again later.")
                Return
            End Try
        End Using

        Using client As New Net.WebClient ' download client for web pages
            Try
                ReleasedVersion = CDbl(client.DownloadString(PropHttpsDomain & "/Outworldz_Installer/UpdateGrid.plx" & GetPostData()))
                If CStr(ReleasedVersion).Length < 4 Then
                    textbox &= $"Released Version is {ReleasedVersion}{vbCrLf}"
                End If
            Catch ex As Exception
                TextPrint(My.Resources.Wrong & " " & ex.Message)
                Return
            End Try
        End Using

        ' Update Error check could be nothing
        If ReleasedVersion = 0 Then ReleasedVersion = CDbl(PropMyVersion)

        Using client As New Net.WebClient ' download client for web pages
            Try
                BetaVersion = CDbl(client.DownloadString(PropHttpsDomain & "/Outworldz_Installer/Grid/version.txt" & GetPostData()))
                If CStr(ReleasedVersion).Length >= 4 Then
                    textbox &= $"Beta Version is {BetaVersion}{vbCrLf}"
                End If
            Catch ex As Exception
                TextPrint(My.Resources.Wrong & " " & ex.Message)
                Return
            End Try
        End Using

        ' Debug
        'ReleasedVersion = 4.7
        'BetaVersion = 4.9

        ' Update Error check could be nothing
        If BetaVersion = 0 Then BetaVersion = CDbl(PropMyVersion)

        Try
            ' check if less than the last skipped update
            ' could be the same or later version already
            If BetaVersion > Settings.SkipUpdateCheck Then
                textbox = $"An Update to {BetaVersion} is available.{vbCrLf}{vbCrLf}"
            ElseIf ReleasedVersion > Settings.SkipUpdateCheck Then
                textbox = $"An Update to {BetaVersion} is available.{vbCrLf}{vbCrLf}"
            Else
                textbox = $"Update notifications are off. The system will let you know when a new update is available.{vbCrLf}{vbCrLf}"
            End If

            InstallButton.Enabled = False
            ' Check if update is > Current version
            If ReleasedVersion > MyVersion Or BetaVersion > MyVersion Then
                If CStr(BetaVersion).Length >= 4 Then
                    InstallButton.Text = $"Install Beta {BetaVersion}"
                Else
                    InstallButton.Text = $"Install Release {BetaVersion}"
                End If
                InstallButton.Enabled = True
            ElseIf BetaVersion = MyVersion Then
                InstallButton.Text = $"Reinstall {MyVersion}"
                InstallButton.Enabled = True
            End If

            RichTextBox3.Text = textbox & Revisions
        Catch ex As Exception
            BreakPoint.Dump(ex)
            Return
        End Try

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Settings.DotnetUpgraded() = False
        UpgradeDotNet() ' one time run for Dot net prerequisits

        Settings.VisitorsEnabled = False
        SetupPerl() ' Ditto

        Settings.VisitorsEnabledModules = False
        SetupPerlModules() ' Perl Language interpreter

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        DialogResult = DialogResult.No
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles InstallButton.Click
        DialogResult = DialogResult.Yes
    End Sub

End Class