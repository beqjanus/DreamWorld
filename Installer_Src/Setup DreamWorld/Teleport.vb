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
                Dim Name = PropRegionClass.RegionName(RegionToUUID)
                Dim status = PropRegionClass.Status(RegionToUUID)
                Debug.Print($"Teleport to {Name} = {GetStateString(status)}")

                If status = RegionMaker.SIMSTATUSENUM.Booted Then
                    Dim DestinationName = PropRegionClass.RegionName(RegionToUUID)
                    If DestinationName.Length > 0 Then

                        Dim FromRegionUUID As String = GetRegionFromAgentID(AgentID)
                        Dim fromName = PropRegionClass.RegionName(FromRegionUUID)
                        Logger("Teleport", $"Teleport from {fromName} to {DestinationName} initiated", "Teleport")

                        If TeleportTo(FromRegionUUID, DestinationName, AgentID) Then
                            Logger("Teleport", $"{DestinationName} teleport command sent", "Teleport")
                        Else
                            Logger("Teleport", $"{DestinationName} failed to receive teleport", "Teleport")
                        End If
                        Fin.Add(AgentID)
                    Else
                        BreakPoint.Show("Unable to locate region " & RegionToUUID)
                    End If
                End If
            Next
        Catch
        End Try
        ' rem from to list as they have moved on
        For Each str As String In Fin
            Logger("Teleport Done", str, "Teleport")
            TeleportAvatarDict.Remove(str)
        Next
        Fin.Clear()

    End Sub

End Module
