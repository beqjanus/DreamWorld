Public Class FormUpdate

    Public Sub Init(str As String)

        Button3.Text = My.Resources.UpdateNow
        Button2.Text = My.Resources.RemindMeLater
        Button1.Text = My.Resources.DoNotShowAgain
        RichTextBox3.SelectAll()
        RichTextBox3.SelectionIndent += 15 ' play With this values To match yours
        RichTextBox3.SelectionRightIndent += 15 ' this too
        RichTextBox3.SelectionLength = 0
        ' this Is a little hack because without this
        ' I've got the first line of my richTB selected anyway.
        RichTextBox3.SelectionBackColor = RichTextBox3.BackColor

        Using client As New Net.WebClient ' download client for web pages
            TextPrint(My.Resources.Checking_for_Updates_word)
            Try
                RichTextBox3.Text = client.DownloadString(PropHttpsDomain & "/Outworldz_Installer/Revisions.txt")
            Catch ex As Exception
                RichTextBox3.Text = str
                Return
            End Try
        End Using

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        DialogResult = DialogResult.OK
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        DialogResult = DialogResult.No
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        DialogResult = DialogResult.Yes
    End Sub

End Class