Module FreezeThaw

#Region "FreezeThaw"

    ''' <summary>
    ''' Freeze a region
    ''' </summary>
    ''' <param name="RegionUUID">Region UUID</param>

    Public Sub Freeze(RegionUUID As String)

        If Smart_Start(RegionUUID) And RegionEnabled(RegionUUID) = True Then
            Dim PID = ProcessID(RegionUUID)
            ShowDOSWindow(RegionUUID, MaybeHideWindow())
            RegionStatus(RegionUUID) = SIMSTATUSENUM.Suspended
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
    ''' Thaw a region
    ''' </summary>
    ''' <param name="RegionUUID">Region UUID</param>
    Public Sub Thaw(RegionUUID As String)


        Dim PID = ProcessID(RegionUUID)
        RegionStatus(RegionUUID) = SIMSTATUSENUM.Resume
        Try
            NtResumeProcess(CachedProcess(PID).Handle)
        Catch
        End Try

    End Sub

    ''' <summary>
    ''' Runs region
    ''' </summary>
    ''' <param name="RegionUUID">RegionUUID</param>
    Public Sub UnPauseRegion(RegionUUID As String)
        Thaw(RegionUUID)
    End Sub

#End Region

End Module
