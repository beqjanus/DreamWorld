Public Class Benchmark

    Private _stopWatch As Stopwatch

    Public Sub Print(Name As String)

        If _stopWatch Is Nothing Then
            Return
        End If

        If Settings.LogBenchmarks Then
            Logger("Benchmark", $"{Name}:{CStr(_stopWatch.Elapsed.TotalMilliseconds / 1000)} {My.Resources.Seconds_word}", "Benchmark")
        Else
            If Debugger.IsAttached Then
                Debug.Print($"Benchmark: {Name}:{CStr(_stopWatch.Elapsed.TotalMilliseconds / 1000)} {My.Resources.Seconds_word}")
            End If
        End If
        _stopWatch = New Stopwatch()

    End Sub

    Public Sub Start(Name As String)

        _stopWatch = New Stopwatch()
        _stopWatch.Start()
        If Settings.LogBenchmarks Then
            Logger("Benchmark", Name, "Benchmark")
        Else
            Debug.Print("Benchmark:" & Name)
        End If

    End Sub

End Class
