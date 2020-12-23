Imports System.Diagnostics.Process

Module CPUCOunter

    Private OpensimProcesses() As Process

    Private _counterList As New Dictionary(Of String, PerformanceCounter)

    Public ReadOnly Property CounterList As Dictionary(Of String, PerformanceCounter)
        Get
            Return _counterList
        End Get
    End Property

    Public Sub CalcCPU()

        OpensimProcesses = Process.GetProcessesByName("Opensim")
        Try
            For Each p As Process In OpensimProcesses
                If FormSetup.PropRegionHandles.ContainsKey(p.Id) Then
                    Using counter As PerformanceCounter = GetPerfCounterForProcessId(p.Id)
                        Dim G As String = FormSetup.PropRegionHandles.Item(p.Id)
                        counter.NextValue() ' start the counter
                        If Not CounterList.ContainsKey(G) Then
                            CounterList.Add(G, counter)
                        End If
                    End Using
                End If
            Next
        Catch
        End Try
    End Sub

    Public Function GetPerfCounterForProcessId(ByVal processId As Integer, ByVal Optional processCounterName As String = "% Processor Time") As PerformanceCounter

        Dim instance As String = GetInstanceNameForProcessId(processId)
        If String.IsNullOrEmpty(instance) Then Return Nothing
        Return New PerformanceCounter("Process", processCounterName, instance)

    End Function

    Public Function GetInstanceNameForProcessId(ByVal processId As Integer) As String

        Try
            Dim process = GetProcessById(processId)
            Dim processName As String = IO.Path.GetFileNameWithoutExtension(process.ProcessName)
            Dim cat As PerformanceCounterCategory = New PerformanceCounterCategory("Process")
            Dim instances As String() = cat.GetInstanceNames().Where(Function(inst) inst.StartsWith(processName, System.StringComparison.InvariantCultureIgnoreCase)).ToArray()

            For Each instance As String In instances
                Using cnt As PerformanceCounter = New PerformanceCounter("Process", "ID Process", instance, True)
                    Dim val As Integer = CInt(cnt.RawValue)
                    If val = processId Then
                        Return instance
                    End If
                    Application.DoEvents()
                End Using
            Next
        Catch
        End Try

        Return Nothing
    End Function

End Module