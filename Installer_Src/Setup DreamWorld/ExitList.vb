Imports System.Threading
Imports System.Collections.Concurrent

Module ExitList

    Public exitList As New ConcurrentDictionary(Of String, String)

    ''' <summary>
    ''' Checks if a region died, and calculates CPU counters, which is a very time consuming process
    ''' </summary>
    Public Sub StartThreads()

        Return
        If ThreadsArerunning Then Return
        ThreadsArerunning = True

#Disable Warning BC42016 ' Implicit conversion
        'Dim start1 As ParameterizedThreadStart = AddressOf CalcCPU
#Enable Warning BC42016 ' Implicit conversion
        'Dim Thread1 = New Thread(start1)
        'Thread1.SetApartmentState(ApartmentState.STA)
        'Thread1.Priority = ThreadPriority.Normal

        'Thread1.Start()
        'Sleep(100)

#Disable Warning BC42016 ' Implicit conversion
        Dim start2 As ParameterizedThreadStart = AddressOf DidItDie
#Enable Warning BC42016 ' Implicit conversion
        Dim Thread2 = New Thread(start2)
        Thread2.SetApartmentState(ApartmentState.STA)
        Thread2.Priority = ThreadPriority.Lowest ' UI gets priority
        Thread2.Start()

    End Sub

    Private Sub DidItDie()

        While PropOpensimIsRunning

            Dim ListofPIDs = RegionPIDs()
            Dim processes = Process.GetProcessesByName("Opensim")
            For Each p In processes
                If Not ListofPIDs.Contains(p.Id) Then
                    PropInstanceHandles.TryAdd(p.Id, p.MainWindowTitle)
                End If
            Next

            For Each RegionUUID As String In RegionUuids()
                Application.DoEvents()
                Dim RegionName = Region_Name(RegionUUID)
                If Not PropOpensimIsRunning() Then Return
                If Not RegionEnabled(RegionUUID) Then Continue For
                Dim status = RegionStatus(RegionUUID)
                Debug.Print($"{RegionName} {GetStateString(status)}")
                If CBool((status = SIMSTATUSENUM.Booted) Or
                        (status = SIMSTATUSENUM.Booting) Or
                        (status = SIMSTATUSENUM.RecyclingDown) Or
                        (status = SIMSTATUSENUM.NoError) Or
                        (status = SIMSTATUSENUM.ShuttingDownForGood) Or
                        (status = SIMSTATUSENUM.Suspended)) Then
                    BreakPoint.Print($"{RegionName} {GetStateString(status)}")
                    Dim Groupname = Group_Name(RegionUUID)
                    If GetHwnd(Groupname) = IntPtr.Zero Then
                        If Not CheckPort(Settings.PublicIP, GroupPort(RegionUUID)) Then
                            If Not exitList.ContainsKey(Groupname) Then
                                exitList.TryAdd(Groupname, "Exit")
                            End If
                        End If
                    End If
                End If
            Next
            Sleep(1000)
        End While

    End Sub

    '
End Module
