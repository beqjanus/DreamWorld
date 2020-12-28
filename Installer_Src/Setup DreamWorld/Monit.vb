Imports System.Threading

Module Monit

    Private ReadOnly _exitList As New Dictionary(Of String, String)

#Region "Properties"

    Public ReadOnly Property PropExitList As Dictionary(Of String, String)
        Get
            Return _exitList
        End Get
    End Property

#End Region

    Public Sub StartMonitorThread()

        Dim Monit = New Thread(AddressOf MonitThread)
        Monit.SetApartmentState(ApartmentState.STA)

        ' Monit.Start()
        Monit.Priority = ThreadPriority.BelowNormal ' UI gets priority

    End Sub

    Private Sub MonitThread()

        While (True)
            Application.DoEvents()
            Thread.Sleep(1000)
            If FormSetup.PropAborting Then Continue While

            For Each RegionUUID As String In PropRegionClass.RegionUuids

                If CBool(PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.Booted) _
                    Or (PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.Booting) _
                    Or (PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.RecyclingDown) _
                    Or (PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.ShuttingDown) _
                     Or (PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.Suspended) Then

                    Dim GroupName = PropRegionClass.GroupName(RegionUUID)
                    Dim hwnd = GetHwnd(GroupName)

                    If hwnd = IntPtr.Zero Then
                        Dim RegionName As String = PropRegionClass.RegionName(RegionUUID)
                        Dim s As String = ""
                        If Not PropExitList.TryGetValue(GroupName, s) Then
                            PropExitList.Add(GroupName, "DOS Box exit")
                        End If
                    End If
                End If
            Next
        End While

    End Sub

    Public Function GetHwnd(Groupname As String) As IntPtr

        If Groupname = RobustName() Then
            For Each pList As Process In Process.GetProcessesByName("Robust")
                If pList.ProcessName = "Robust" Then
                    Return pList.MainWindowHandle
                End If
            Next
            Return IntPtr.Zero
        End If

        Dim AllProcesses = Process.GetProcessesByName("Opensim")
        For Each p As Process In AllProcesses
            If p.MainWindowTitle = Groupname Then
                p.Refresh()
                Return p.MainWindowHandle
            End If
            Application.DoEvents()
        Next
        Return IntPtr.Zero

    End Function

    Public Function GetPIDofWindow(Groupname As String) As Integer

        Dim AllProcesses = Process.GetProcesses()
        For Each p As Process In AllProcesses
            If p.MainWindowTitle = Groupname Then
                Return p.Id
            End If
        Next
        Return 0

    End Function

End Module
