#Region "Copyright AGPL3.0"

' Copyright Outworldz, LLC.
' AGPL3.0  https://opensource.org/licenses/AGPL

#End Region

Module BreakPoint
    Public Sub Print(Message As String)
        Diagnostics.Debug.Print(Message)
    End Sub

    Public Sub Show(ex As Exception)

        Dim Message As String
        Message = ex.Message
        Dim st As StackTrace = New StackTrace(ex, True)
        For Each sf As StackFrame In st.GetFrames
            If sf.GetFileLineNumber() > 0 Then
                Message &= "Line:" & sf.GetFileLineNumber() & " Filename: " & IO.Path.GetFileName(sf.GetFileName) & Environment.NewLine
            End If
        Next
        Diagnostics.Debug.Print(Message)

    End Sub

End Module
