Public Class Handler

    Private WithEvents MyProcess As New Process()
    Dim gName As String = ""

    Private Sub OpensimProcess_Exited(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyProcess.Exited

        ' Handle any process that exits by stacking it. DoExitHandlerPoll will clean up stack
        Try
            Form1.ExitList.Add(gName)
        Catch ex As Exception
            Debug.Print(ex.Message)
        End Try

    End Sub
    Public Function Init(Name As String) As Process
        gName = Name ' save for exit
        Return MyProcess
    End Function

End Class
