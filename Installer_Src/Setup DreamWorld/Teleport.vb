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

        Using client As New Net.WebClient ' download client for web pages
            Dim Up As String
            Try
                Up = client.DownloadString($"http://{Settings.PublicIP}:{CStr(Port)}/index.php") ' ?version does not work, give back a 302 to Opensimulator.orgm though
            Catch ex As Exception
                Return False
            End Try

            If Up.Length = 0 And PropOpensimIsRunning() Then
                Return False
            End If
        End Using

        Return True

    End Function

    Public Sub TeleportAgents()

        If Not Settings.SmartStart Then Return
        Try
            For Each Keypair In TeleportAvatarDict
                Dim AgentID = Keypair.Key
                Dim RegionToUUID = Keypair.Value
                Dim status = PropRegionClass.Status(RegionToUUID)
                Dim Port As Integer = PropRegionClass.GroupPort(RegionToUUID)
                Dim DestinationName = PropRegionClass.RegionName(RegionToUUID)

                If status = RegionMaker.SIMSTATUSENUM.Stopped Then
                    Fin.Add(AgentID) ' cancel this, the region went away

                ElseIf status = RegionMaker.SIMSTATUSENUM.Booted And
                    IsRegionReady(Port) And
                    RegionIsRegisteredOnline(RegionToUUID) Then
                    Sleep(4000)
                    Dim FromRegionUUID As String = GetRegionFromAgentID(AgentID)
                    Dim fromName = PropRegionClass.RegionName(FromRegionUUID)
                    If fromName.Length > 0 Then
                        Logger("Teleport", $"Teleport from {fromName} to {DestinationName} initiated", "Teleport")
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
