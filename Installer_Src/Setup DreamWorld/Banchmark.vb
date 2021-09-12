Imports System.Diagnostics
Public Class Benchmark

    Private _stopWatch As Stopwatch

    Public Sub Print(Name As String)

        If Not Debugger.IsAttached Then Return

        Debug.Print($"Benchmark: {Name}:{CStr(_stopWatch.Elapsed.TotalMilliseconds / 1000)} {My.Resources.Seconds_word}")

    End Sub

    Public Sub Start()

        _stopWatch = New Stopwatch()
        _stopWatch.Start()
        Debug.Print("Benchmark: ----------------")
    End Sub

End Class
