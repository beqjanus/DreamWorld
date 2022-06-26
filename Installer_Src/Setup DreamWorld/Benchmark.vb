Public Class Benchmark

    Private _stopWatch As Stopwatch

    Public Sub Print(Name As String)


        If _stopWatch Is Nothing Then
            _stopWatch = New Stopwatch()
            _stopWatch.Start()
        End If

        If Settings.LogBenchmarks Then
            Logger("Benchmark", $"{Name}:{CStr(_stopWatch.Elapsed.TotalMilliseconds / 1000)} {My.Resources.Seconds_word}", "Benchmark")
        Else
            If Debugger.IsAttached Then
                Debug.Print($"Benchmark: {Name}:{CStr(_stopWatch.Elapsed.TotalMilliseconds / 1000)} {My.Resources.Seconds_word}")
            End If
        End If
        _stopWatch = New Stopwatch()
        _stopWatch.Start()
    End Sub

End Class
