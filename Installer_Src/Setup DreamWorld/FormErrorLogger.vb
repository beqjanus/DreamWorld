Public Class FormErrorLogger

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles QuitButton.Click

        End

    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click

        Baretail("""" & IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Logs\Error.log") & """")

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles SendButton.Click

        Try
            Dim path = IO.Path.Combine(CurDir(), "OutworldzFiles\Logs\Error.log")

            Using outputFile As New IO.StreamWriter(IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Logs\Error.log"), True)
                outputFile.WriteLine("********START*************")
                outputFile.WriteLine("[Reason]" & vbCrLf)
                outputFile.WriteLine(ReasonText.Text & vbCrLf)
                If (EmailTextBox.Text.Length > 0) Then
                    outputFile.WriteLine("[Email]" & vbCrLf)
                    outputFile.WriteLine(EmailTextBox.Text & vbCrLf)
                End If
                outputFile.WriteLine("********END*************")
            End Using

            Dim CGI = New Uri(PropHttpsDomain & "/cgi/uploadcrash.plx")
            PostContentUploadFile(path, CGI)
            'DeleteFile(path)
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
            BreakPoint.Dump(ex)
        End Try
    End Sub

End Class