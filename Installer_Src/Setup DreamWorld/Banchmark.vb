Imports System.Diagnostics
Public Class Benchmark

    Private _stopWatch As Stopwatch

    Public Sub Print(Name As String)

        If _stopWatch Is Nothing Then
            Return
        End If
        If Settings.LogBenchmarks Then
            Logger("Benchmark", $"{Name}:{CStr(_stopWatch.Elapsed.TotalMilliseconds / 1000)} {My.Resources.Seconds_word}", "Benchmark")
        End If

        If Not Debugger.IsAttached Then Return
        Debug.Print($"Benchmark: {Name}:{CStr(_stopWatch.Elapsed.TotalMilliseconds / 1000)} {My.Resources.Seconds_word}")

    End Sub

    Public Sub Start()

        _stopWatch = New Stopwatch()
        _stopWatch.Start()
        If Settings.LogBenchmarks Then
            Logger("Benchmark", "---START---", "Benchmark")
        End If

        Debug.Print("Benchmark: ---START---")

    End Sub

End Class
