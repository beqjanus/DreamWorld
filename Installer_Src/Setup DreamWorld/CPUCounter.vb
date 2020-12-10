Imports System.Diagnostics.Process

Module CPUCOunter

    Public Function GetPerfCounterForProcessId(ByVal processId As Integer, ByVal Optional processCounterName As String = "% Processor Time") As PerformanceCounter

        Dim instance As String = GetInstanceNameForProcessId(processId)
        If String.IsNullOrEmpty(instance) Then Return Nothing
        Return New PerformanceCounter("Process", processCounterName, instance)

    End Function

    Public Function GetInstanceNameForProcessId(ByVal processId As Integer) As String

        Dim process = GetProcessById(processId)
        Dim processName As String = IO.Path.GetFileNameWithoutExtension(process.ProcessName)
        Dim cat As PerformanceCounterCategory = New PerformanceCounterCategory("Process")
        Dim instances As String() = cat.GetInstanceNames().Where(Function(inst) inst.StartsWith(processName)).ToArray()

        For Each instance As String In instances

            Using cnt As PerformanceCounter = New PerformanceCounter("Process", "ID Process", instance, True)
                Dim val As Integer = CInt(cnt.RawValue)

                If val = processId Then
                    Return instance
                End If
            End Using
        Next

        Return Nothing
    End Function

End Module