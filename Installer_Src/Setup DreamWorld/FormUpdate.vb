Public Class FormUpdate

    Public Sub Init(str As String)

        RichTextBox1.Text = My.Resources.NewVersion
        Using client As New Net.WebClient ' download client for web pages
            TextPrint(My.Resources.Checking_for_Updates_word)
            Try
                RichTextBox3.Text = client.DownloadString(PropDomain & "/Outworldz_Installer/Revisions.txt")
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