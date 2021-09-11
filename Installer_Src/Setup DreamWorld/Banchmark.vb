Public Class Benchmark

    Private _startDate As Date

    Public Sub Print(Name As String)

        If Not Debugger.IsAttached Then Return
        Dim seconds = DateAndTime.DateDiff(DateInterval.Second, _startDate, DateTime.Now)
        Debug.Print($"Benchmark: {Name}:{CStr(seconds)} {My.Resources.Seconds_word}")

    End Sub

    Public Sub Start()

        _startDate = Date.Now

    End Sub

End Class
