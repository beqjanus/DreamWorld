Public Class FormErrorLogger

    Private Sub FormLoad(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load

        QuitButton.Text = My.Resources.Quit_Now_Word
        SendButton.Text = My.Resources.Send_and_quit_word
        Label1.Text = My.Resources.Crash_Message

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles SendButton.Click

        Dim Myupload As New UploadImage
        Dim CGI = New Uri("https://outworldz.com/cgi/uploadcrash.plx")
        Dim path = IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Error.log")
        Myupload.PostContentUploadFile(path, CGI)

        FileStuff.DeleteFile(path)
        End

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles QuitButton.Click

        End

    End Sub

    Private Sub ReasonText_TextChanged(sender As Object, e As EventArgs) Handles ReasonText.LostFocus

        Dim path = IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Error.log")

        Using outputFile As New IO.StreamWriter(IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Error.log"), True)
            outputFile.WriteLine("[Reason]" & vbCrLf)
            outputFile.WriteLine(ReasonText.Text & vbCrLf)
        End Using

        Using outputFile As New IO.StreamWriter(IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Error.log"), True)

            Dim reader As System.IO.StreamReader
            reader = System.IO.File.OpenText(IO.Path.Combine(Settings.CurrentDirectory, "Outworldzfiles\Settings.ini"))
            'now loop through each line
            While reader.Peek <> -1
                outputFile.WriteLine(reader.ReadLine & vbCrLf)
            End While
            reader.Close()
        End Using

    End Sub

    Private Sub ReasonText_TextChanged_1(sender As Object, e As EventArgs) Handles ReasonText.TextChanged

    End Sub

End Class