Imports System.Collections.Concurrent
Imports System.Threading

Module CPUCounter

    Private ReadOnly _counterList As New Dictionary(Of String, PerformanceCounter)

    Private ReadOnly _CPUValues As New Dictionary(Of String, Double)

    Private ReadOnly _regionHandles As New ConcurrentDictionary(Of Integer, String)

    Private ReadOnly O As New CPUStuff With {
            .CounterList = CounterList,
            .CPUValues = CPUValues,
            .PropInstanceHandles = PropInstanceHandles
        }

    Private _PCList As Dictionary(Of Integer, PerformanceCounter)

    Public ReadOnly Property CPUValues As Dictionary(Of String, Double)
        Get
            Return _CPUValues
        End Get
    End Property

    Public ReadOnly Property PropInstanceHandles As ConcurrentDictionary(Of Integer, String)
        Get
            Return _regionHandles
        End Get
    End Property

    Private ReadOnly Property CounterList As Dictionary(Of String, PerformanceCounter)
        Get
            Return _counterList
        End Get
    End Property

    Private ReadOnly Property PCList As Dictionary(Of Integer, PerformanceCounter)
        Get
            If _PCList Is Nothing Then _PCList = New Dictionary(Of Integer, PerformanceCounter)
            Return _PCList
        End Get
    End Property

    Public Sub AddCPU(PID As Integer, GName As String)

        If Not CounterList.ContainsKey(GName) Then
            Try
                Using counter As PerformanceCounter = GetPerfCounterForProcessId(PID)
                    If counter IsNot Nothing Then
                        CounterList.Add(GName, counter)
                        counter.NextValue() ' start the counter
                    End If
                End Using
            Catch ex As Exception
            End Try
        End If

    End Sub

    Public Sub CalcCPU()

        '  While PropOpensimIsRunning
        Dim AllProcesses() = Process.GetProcessesByName("Opensim")
        Try
            Application.DoEvents()

            For Each p As Process In AllProcesses
                If PropInstanceHandles.ContainsKey(p.Id) Then
                    Dim Gname As String = PropInstanceHandles.Item(p.Id)
                    Dim c As PerformanceCounter = Nothing

                    If Not CounterList.ContainsKey(Gname) Then
                        Try
                            Using counter As PerformanceCounter = GetPerfCounterForProcessId(p.Id)
                                If counter IsNot Nothing Then
                                    Debug.Print($"> CounterList {CStr(CounterList.Count)}")
                                    CounterList.Add(Gname, counter)
                                    counter.NextValue() ' start the counter
                                End If
                            End Using
                        Catch ex As Exception
                            CounterList.Item(Gname).Close()
                            CounterList.Remove(Gname)
                            CPUValues.Remove(Gname)
                            Continue For
                        End Try
                    End If

                    If Not CPUValues.ContainsKey(Gname) Then
                        Debug.Print($"> CPUValues {CStr(CPUValues.Count)}")
                        CPUValues.Add(Gname, 0)
                    Else
                        Dim a As Double
                        Try
                            a = Convert.ToDouble(CounterList.Item(Gname).NextValue(), Globalization.CultureInfo.InvariantCulture)
                        Catch ex As Exception
                            CounterList.Item(Gname).Close()
                        End Try

                        Dim b = (a / Environment.ProcessorCount)
                        CPUValues.Item(Gname) = Math.Round(b, 3)
                        Debug.Print($"> CPU {Gname} = {CStr(Math.Round(b, 3))}")
                    End If
                Else
                    Debug.Print($"> PropInstanceHandles {CStr(PropInstanceHandles.Count)}")
                    PropInstanceHandles.TryAdd(p.Id, p.MainWindowTitle)
                End If

            Next
        Catch
            Try
                O.CounterList.Clear()
                O.CPUValues.Clear()
            Catch
            End Try
        End Try
        'Thread.Sleep(5000)

        ' End While

    End Sub

    Private Function GetInstanceNameForProcessId(ByVal processId As Integer) As String

        Try
            Dim process = CachedProcess(processId)

            Dim processName As String = IO.Path.GetFileNameWithoutExtension(process.ProcessName)
            Dim cat As New PerformanceCounterCategory("Process")
            Dim instances As String() = cat.GetInstanceNames().Where(Function(inst) inst.StartsWith(processName, System.StringComparison.OrdinalIgnoreCase)).ToArray()

            For Each instance As String In instances
                Using cnt = New PerformanceCounter("Process", "ID Process", instance, True)
                    Dim val As Integer = CInt(cnt.RawValue)
                    If val = processId Then
                        Return instance
                    End If
                End Using
            Next
        Catch
        End Try

        Return Nothing
    End Function

    Private Function GetPerfCounterForProcessId(ByVal processId As Integer) As PerformanceCounter

        Dim processCounterName As String = "% Processor Time"
        If PCList.ContainsKey(processId) Then
            Return PCList.Item(processId)
        End If

        Dim instance As String = GetInstanceNameForProcessId(processId)
        If String.IsNullOrEmpty(instance) Then Return Nothing

        Dim PC = New PerformanceCounter("Process", processCounterName, instance)
        PCList.Add(processId, PC)
        Return PC

    End Function

    Public Class CPUStuff
        Public CounterList As Dictionary(Of String, PerformanceCounter)
        Public CPUValues As Dictionary(Of String, Double)
        Public PropInstanceHandles As ConcurrentDictionary(Of Integer, String)
    End Class

End Module