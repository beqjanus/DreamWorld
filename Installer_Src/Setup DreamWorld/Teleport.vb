#Region "Copyright AGPL3.0"

' Copyright Outworldz, LLC.
' AGPL3.0  https://opensource.org/licenses/AGPL

#End Region

Module Teleport

    Public TeleportAvatarDict As New Dictionary(Of String, String)

    Public Sub TeleportAgents()

        If Settings.SmartStart Then
            For Each Keypair In TeleportAvatarDict

                Dim AgentName = Keypair.Key
                Dim RegionToUUID = Keypair.Value

                If AgentName.Length > 0 Then
                    Dim DestinationName = PropRegionClass.RegionName(RegionToUUID)
                    If DestinationName.Length > 0 Then
                        TextPrint(My.Resources.Teleporting_word & " " & AgentName & " -> " & PropRegionClass.RegionName(RegionToUUID))
                        TeleportTo(DestinationName, AgentName)
                    Else
                        BreakPoint.Show("Unable to locate region " & RegionToUUID)
                    End If
                End If
            Next
        End If

        TeleportAvatarDict.Clear()

    End Sub

End Module
