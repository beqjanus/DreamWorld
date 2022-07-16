Module FreezeThaw

#Region "FreezeThaw"

    ''' <summary>
    '''
    ''' </summary>
    ''' <param name="RegionUUID"></param>
    ''' <param name="Arg">True to freeze or False rpid to thaw</param>
    ''' <param name="PID">Process ID </param>
    ''' <returns></returns>
    Public Sub FreezeThaw(RegionUUID As String, Arg As Boolean)

        Dim PID = ProcessID(RegionUUID)
        Dim ft As String
        If Arg Then
            'ShowDOSWindow(GetHwnd(Group_Name(RegionUUID)), MaybeHideWindow())
            ft = $"-pid {CStr(PID)}"
            RegionStatus(RegionUUID) = SIMSTATUSENUM.Suspended
            SetRegionOffline(RegionUUID)
        Else
            'ShowDOSWindow(GetHwnd(Group_Name(RegionUUID)), MaybeShowWindow())
            RegionStatus(RegionUUID) = SIMSTATUSENUM.Resume
            SetRegionOnline(RegionUUID)
            ft = $"-rpid {CStr(PID)}"
        End If

        Using SuspendProcess As New Process()
            Dim pi = New ProcessStartInfo With {
                    .Arguments = ft,
                    .FileName = """" & IO.Path.Combine(Settings.CurrentDirectory, "NtSuspendProcess64.exe") & """"
                }

            pi.CreateNoWindow = True
            pi.WindowStyle = ProcessWindowStyle.Hidden

            SuspendProcess.StartInfo = pi

            Try
                SuspendProcess.Start()
            Catch ex As Exception
            End Try
        End Using

        PokeRegionTimer(RegionUUID)
        PropUpdateView = True ' make form refresh

    End Sub

    ''' <summary>
    ''' Suspends region
    ''' </summary>
    ''' <param name="RegionUUID">RegionUUID</param>
    Public Sub PauseRegion(RegionUUID As String)

        If Settings.Smart_Start Then
            BreakPoint.Print($"Pausing {Region_Name(RegionUUID)}")
        End If

        FreezeThaw(RegionUUID, True)

    End Sub

    ''' <summary>
    ''' Resumes Region from frozen state
    ''' </summary>
    ''' <param name="RegionUUID">RegionUUID</param>
    ''' <returns>0 if success</returns>
    Public Function ResumeRegion(RegionUUID As String) As Boolean

        FreezeThaw(RegionUUID, False)
        If ProcessID(RegionUUID) = 0 Then
            ProcessID(RegionUUID) = GetPIDofWindow(Group_Name(RegionUUID))
            If ProcessID(RegionUUID) = 0 Then
                Return True
            End If
        End If

        ReBoot(RegionUUID)
        TeleportAgents()
        Return True

    End Function

#End Region

End Module
