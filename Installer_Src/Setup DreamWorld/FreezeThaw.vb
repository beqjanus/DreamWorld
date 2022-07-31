Module FreezeThaw

#Region "FreezeThaw"

    ''' <summary>
    ''' Freeze a region
    ''' </summary>
    ''' <param name="RegionUUID">Region UUID</param>

    Public Sub Freeze(RegionUUID As String)

        If Smart_Start(RegionUUID) And RegionEnabled(RegionUUID) = True Then
            Dim PID = ProcessID(RegionUUID)
            ShowDOSWindow(GetHwnd(Group_Name(RegionUUID)), MaybeHideWindow())
            RegionStatus(RegionUUID) = SIMSTATUSENUM.Suspended
            'SetRegionOffline(RegionUUID)
            NtSuspendProcess(CachedProcess(PID).Handle)
        End If

    End Sub

    ''' <summary>
    ''' Suspends region
    ''' </summary>
    ''' <param name="RegionUUID">RegionUUID</param>
    Public Sub PauseRegion(RegionUUID As String)

        If Settings.Smart_Start And Smart_Start(RegionUUID) Then
            BreakPoint.Print($"Pausing {Region_Name(RegionUUID)}")
            Freeze(RegionUUID)
        End If

    End Sub

    ''' <summary>
    ''' Resumes Region from frozen state
    ''' </summary>
    ''' <param name="RegionUUID">RegionUUID</param>
    ''' <returns>0 if success</returns>
    Public Function ResumeRegion(RegionUUID As String) As Boolean

        PokeRegionTimer(RegionUUID)
        If ProcessID(RegionUUID) = 0 Then
            ProcessID(RegionUUID) = GetPIDofWindow(Group_Name(RegionUUID))
        End If

        ReBoot(RegionUUID)
        TeleportAgents()
        Return True

    End Function

    ''' <summary>
    ''' Thaw a region
    ''' </summary>
    ''' <param name="RegionUUID">Region UUID</param>
    Public Sub Thaw(RegionUUID As String)

        If Smart_Start(RegionUUID) And RegionEnabled(RegionUUID) = True Then
            Dim PID = ProcessID(RegionUUID)
            ShowDOSWindow(GetHwnd(Group_Name(RegionUUID)), MaybeShowWindow())
            RegionStatus(RegionUUID) = SIMSTATUSENUM.Resume
            'SetRegionOnline(RegionUUID)    
            NtResumeProcess(CachedProcess(PID).Handle)
        End If

    End Sub

    ''' <summary>
    ''' Runs region
    ''' </summary>
    ''' <param name="RegionUUID">RegionUUID</param>
    Public Sub UnPauseRegion(RegionUUID As String)

        If Settings.Smart_Start And Smart_Start(RegionUUID) Then
            BreakPoint.Print($"Thaw {Region_Name(RegionUUID)}")
            Thaw(RegionUUID)
        End If

    End Sub

#End Region

End Module
