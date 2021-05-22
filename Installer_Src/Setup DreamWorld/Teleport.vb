#Region "Copyright AGPL3.0"

' Copyright Outworldz, LLC.
' AGPL3.0  https://opensource.org/licenses/AGPL

#End Region

Module Teleport

    Public Fin As New List(Of String)
    Public TeleportAvatarDict As New Dictionary(Of String, String)

    Public Sub TeleportAgents()

        If Not Settings.SmartStart Then Return
        Try
            For Each Keypair In TeleportAvatarDict
                Dim AgentID = Keypair.Key
                Dim RegionToUUID = Keypair.Value
                Dim status = PropRegionClass.Status(RegionToUUID)
                Dim DestinationName = PropRegionClass.RegionName(RegionToUUID)
                If status = RegionMaker.SIMSTATUSENUM.Booting Then
                    PokeRegionTimer(RegionToUUID)
                End If
                If status = RegionMaker.SIMSTATUSENUM.Booted And RegionIsRegisteredOnline(RegionToUUID) Then

                    Debug.Print($"Teleport to {DestinationName} = {GetStateString(status)}")
                    Dim FromRegionUUID As String = GetRegionFromAgentID(AgentID)
                    Dim fromName = PropRegionClass.RegionName(FromRegionUUID)
                    Logger("Teleport", $"Teleport from {fromName} to {DestinationName} initiated", "Teleport")

                    ' Make sure they are still in the grid and send them onward
                    If fromName.Length > 0 Then
                        If TeleportTo(FromRegionUUID, DestinationName, AgentID) Then
                            Logger("Teleport", $"{DestinationName} teleport command sent", "Teleport")
                            Fin.Add(AgentID)
                        Else
                            Logger("Teleport", $"{DestinationName} failed to receive teleport", "Teleport")
                            BreakPoint.Show("Unable to locate region " & RegionToUUID)
                            Fin.Add(AgentID)
                        End If
                    Else
                        Fin.Add(AgentID) ' cancel this, the agent is not anywhere online we can get to
                    End If

                ElseIf status = RegionMaker.SIMSTATUSENUM.Stopped Then
                    Fin.Add(AgentID) ' cancel this, the region went away
                End If
            Next
        Catch
        End Try
        ' rem from the to list as they have moved on
        For Each str As String In Fin
            Logger("Teleport Done", str, "Teleport")
            TeleportAvatarDict.Remove(str)
        Next
        Fin.Clear()

    End Sub

End Module
