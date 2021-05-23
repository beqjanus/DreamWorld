#Region "Copyright AGPL3.0"

' Copyright Outworldz, LLC.
' AGPL3.0  https://opensource.org/licenses/AGPL

#End Region

Imports System.Net

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
                Dim Port As Integer = PropRegionClass.GroupPort(RegionToUUID)
                Dim DestinationName = PropRegionClass.RegionName(RegionToUUID)
                If status = RegionMaker.SIMSTATUSENUM.Booting Then
                    PokeRegionTimer(RegionToUUID)
                End If
                If status = RegionMaker.SIMSTATUSENUM.Booted And IsRegionReady(Port) Then
                    Dim FromRegionUUID As String = GetRegionFromAgentID(AgentID)
                    Dim fromName = PropRegionClass.RegionName(FromRegionUUID)
                    If fromName.Length > 0 Then

                        Logger("Teleport", $"Teleport from {fromName} to {DestinationName} initiated", "Teleport")
                        ' Double Check region
                        If RegionIsRegisteredOnline(RegionToUUID) Then
                            If TeleportTo(FromRegionUUID, DestinationName, AgentID) Then
                                Logger("Teleport", $"{DestinationName} teleport command sent", "Teleport")
                                Fin.Add(AgentID)
                            Else
                                Logger("Teleport", $"{DestinationName} failed to receive teleport", "Teleport")
                                BreakPoint.Show("Unable to locate region " & RegionToUUID)
                                Fin.Add(AgentID)
                            End If
                        Else
                            BreakPoint.Show("Region is not registered online yet it was ready for logins?:" & RegionToUUID)
                            'Fin.Add(AgentID)
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


    ''' <summary>Check if region is ready for l;ogin</summary>
    ''' <returns>true if up</returns>
    Public Function IsRegionReady(Port As Integer) As Boolean

        Using client As New WebClient ' download client for web pages
            Dim Up As String
            Try
                Up = client.DownloadString($"http://{Settings.PublicIP}:{CStr(Port)}/index.php")
            Catch ex As Exception
                ' If ex.Message.Contains("404") Then
                '   RobustIcon(True)
                '   Log("INFO", "Robust is running")
                '   Return True
                ' End If
                Return False
            End Try

            If Up.Length = 0 And PropOpensimIsRunning() Then
                Return False
            End If
        End Using

        Return True

    End Function

End Module
