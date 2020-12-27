Imports System.Threading

Module Monit

    Private ReadOnly _exitList As New Dictionary(Of String, String)

    Public ReadOnly Property PropExitList As Dictionary(Of String, String)
        Get
            Return _exitList
        End Get
    End Property

    Public Sub StartMonitorThread()

        Dim Monit = New Thread(AddressOf MonitThread)
        Monit.SetApartmentState(ApartmentState.STA)

        Monit.Start()
        Monit.Priority = ThreadPriority.BelowNormal ' UI gets priority

    End Sub

    Private Sub MonitThread()

        While (True)

            For Each RegionUUID As String In PropRegionClass.RegionUuids

                Dim GroupName = PropRegionClass.GroupName(RegionUUID)

                If CBool(PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.Booted _
                    Or (PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.Booting) _
                    Or (PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.RecyclingDown) _
                    Or (PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.ShuttingDown) _
                     Or (PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.Suspended)) Then

                    Dim hwnd = GetHwnd(GroupName)

                    If hwnd = IntPtr.Zero Then
                        Dim RegionName As String = PropRegionClass.RegionName(RegionUUID)
                        Dim s As String = ""
                        If Not PropExitList.TryGetValue(RegionName, s) Then
                            PropExitList.Add(RegionName, "DOS Box exit")
                        End If
                    End If
                End If

            Next
            Application.DoEvents()
            Thread.Sleep(1000)
        End While

    End Sub

    Public Function GetHwnd(Groupname As String) As IntPtr

        If Groupname = RobustName() Then

            For Each pList As Process In Process.GetProcessesByName("Robust")
                If pList.ProcessName = "Robust" Then
                    Return pList.MainWindowHandle
                End If
            Next

            Dim h As IntPtr = IntPtr.Zero
            Return h

        End If

        Dim Regionlist = PropRegionClass.RegionUuidListByName(Groupname)

        For Each RegionUUID As String In Regionlist
            Dim pid = PropRegionClass.ProcessID(RegionUUID)
            For Each pList As Process In Process.GetProcessesByName("Opensim")
                If pList.Id = pid Then
                    Return pList.MainWindowHandle
                End If
                Application.DoEvents()
            Next
        Next
        Return IntPtr.Zero

    End Function

End Module
