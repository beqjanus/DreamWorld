Public Class Benchmark

    Private _startDate As Date

    Public Sub Start()

        _startDate = Date.Now

    End Sub

    Public Sub Print(Name As String)

        Dim seconds = DateAndTime.DateDiff(DateInterval.Second, _startDate, DateTime.Now)
        Debug.Print($"{Name}:{CStr(seconds)} {My.Resources.Seconds_word}")
        _startDate = Date.Now

    End Sub



End Class
