Public Class Handler

    Private WithEvents MyProcess As New Process()

    Dim RegionHandles1 As New Dictionary(Of Integer, String)
    Dim Exitlist1 As New ArrayList()

    Private Sub OpensimProcess_Exited(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyProcess.Exited

        ' Handle any process that exits by stacking it. DoExitHandlerPoll will clean up stack
        Dim pid = 0
        Try
            pid = CType(sender.Id, Integer)
        Catch ex As FormatException
            Debug.Print(ex.Message)
        Finally
        End Try
        Try
            Exitlist1.Add(RegionHandles1.Item(pid))
        Catch ex As exception
        End Try

        Try
            RegionHandles1.Remove(pid)
        Catch ex As ArgumentNullException
        End Try


    End Sub

    Public Function Init(ByRef RegionHandles As Dictionary(Of Integer, String), ByRef ExitList As ArrayList) As Process

        Exitlist1 = ExitList
        RegionHandles1 = RegionHandles
        Return MyProcess

    End Function

End Class