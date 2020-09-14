Module BreakPoint

    Public Sub Show(msg As String)
        Diagnostics.Debug.Print(msg)
        Form1.Log(My.Resources.Info_word, msg)
    End Sub

End Module
