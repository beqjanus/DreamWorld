Public Class Handler

    Private WithEvents MyProcess As New Process()

    Dim RegionHandles1 As New Dictionary(Of Integer, String)
    Dim Exitlist1 As New List(Of String)

    Private Sub OpensimProcess_Exited(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyProcess.Exited

        ' Handle any process that exits by stacking it. DoExitHandlerPoll will clean up stack
        Try
            Dim pid = CType(sender.Id, Integer)
            Dim name = RegionHandles1.Item(pid)
            RegionHandles1.Remove(pid)
            Exitlist1.Add(name)
        Catch ex As Exception
            Debug.Print(ex.Message)
        End Try


    End Sub
    Public Function Init(ByRef RegionHandles As Dictionary(Of Integer, String), ByRef ExitList As List(Of String)) As Process

        Exitlist1 = ExitList
        RegionHandles1 = RegionHandles
        Return MyProcess

    End Function


End Class
