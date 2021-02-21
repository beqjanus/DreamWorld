Imports System.IO

Public Class FormErrorLogger

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles QuitButton.Click

        End

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles SendButton.Click

        Try
            Dim path = IO.Path.Combine(CurDir(), "OutworldzFiles\Logs\Error.log")
            Logit("Sending")
            Using outputFile As New IO.StreamWriter(IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Logs\Error.log"), True)
                outputFile.WriteLine("[Reason]" & vbCrLf)
                outputFile.WriteLine(ReasonText.Text & vbCrLf)
                If (EmailTextBox.Text.Length > 0) Then
                    outputFile.WriteLine("[Email]" & vbCrLf)
                    outputFile.WriteLine(EmailTextBox.Text & vbCrLf)
                End If
            End Using
            Logit(ReasonText.Text)
            Dim CGI = New Uri("https://outworldz.com/cgi/uploadcrash.plx")
            PostContentUploadFile(path, CGI)
            DeleteFile(path)
        Catch
        End Try

        End

    End Sub

    Private Sub FormLoad(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load

        QuitButton.Text = My.Resources.Cancel_word
        SendButton.Text = My.Resources.Send_report
        TextBox2.Text = My.Resources.Quit_Message0
        Label1.Text = My.Resources.Quit_Message1
        PrivacyButton.Text = My.Resources.Privacy_policy

        Application.DoEvents()

    End Sub

    Private Sub PrivacyButton_Click(sender As Object, e As EventArgs) Handles PrivacyButton.Click

        Dim webAddress As String = "https://outworldz.com/privacy.htm"
        Try
            Process.Start(webAddress)
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try
    End Sub

    Private Sub ReasonText_TextChanged(sender As Object, e As EventArgs) Handles ReasonText.TextChanged

    End Sub
End Class