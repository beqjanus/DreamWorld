#Region "Copyright AGPL3.0"

' Copyright Outworldz, LLC.
' AGPL3.0  https://opensource.org/licenses/AGPL

#End Region

Module Teleport

    Public Fin As New List(Of String)
    Public TeleportAvatarDict As New Dictionary(Of String, String)

    ''' <summary>Check if region is ready for l;ogin</summary>
    ''' <returns>true if up</returns>
    Public Function IsRegionReady(Port As Integer) As Boolean

        Dim up As Boolean
        up = CheckPort(Settings.LANIP, Port)
        Return up

    End Function
    ''' <summary>
    '''  TODO re-entrant lock needed
    '''  
    ''' </summary>
    ''' 
    Dim TPLock As New Object
    Public Sub TeleportAgents()


        SyncLock TPLock

            'If Not Settings.SmartStart Then Return
            Try
                For Each Keypair In TeleportAvatarDict
                    Dim AgentID = Keypair.Key
                    Dim RegionToUUID = Keypair.Value
                    Dim status = PropRegionClass.Status(RegionToUUID)
                    Dim Port As Integer = PropRegionClass.GroupPort(RegionToUUID)

                    If status = ClassRegionMaker.SIMSTATUSENUM.Stopped Then
                        Fin.Add(AgentID) ' cancel this, the region went away

                    ElseIf status = ClassRegionMaker.SIMSTATUSENUM.Booted Then

                        If IsRegionReady(Port) And RegionIsRegisteredOnline(RegionToUUID) Then
                            Dim DestinationName = PropRegionClass.RegionName(RegionToUUID)
                            Dim FromRegionUUID As String = GetRegionFromAgentID(AgentID)
                            Dim fromName = PropRegionClass.RegionName(FromRegionUUID)
                            If fromName.Length > 0 Then
                                Bench.Print("Teleport Initiated")
                                RPC_admin_dialog(AgentID, $"{PropRegionClass.RegionName(RegionToUUID)} will be ready in {CStr(Settings.TeleportSleepTime)} seconds.")
                                Threading.Thread.Sleep(Settings.TeleportSleepTime * 1000)
                                If TeleportTo(FromRegionUUID, DestinationName, AgentID) Then
                                    Logger("Teleport", $"{DestinationName} teleport command sent", "Teleport")
                                Else
                                    Logger("Teleport", $"{DestinationName} failed to receive teleport", "Teleport")
                                    BreakPoint.Show("Unable to locate region " & RegionToUUID)
                                    RPC_admin_dialog(AgentID, $"Unable to locate region {PropRegionClass.RegionName(RegionToUUID)}.")
                                End If
                                Fin.Add(AgentID)
                            Else
                                Fin.Add(AgentID) ' cancel this, the agent is not anywhere  we can get to
                            End If
                        End If
                    End If

                Next
            Catch
            End Try
            ' rem from the to list as they have moved on
            For Each str As String In Fin
                Logger("Teleport Done", str, "Teleport")
                TeleportAvatarDict.Remove(str)
                Bench.Print("Teleport Finished")
            Next
            Fin.Clear()

        End SyncLock

    End Sub

End Module
