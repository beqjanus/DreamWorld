#Region "Copyright AGPL3.0"

' Copyright Outworldz, LLC.
' AGPL3.0  https://opensource.org/licenses/AGPL

#End Region

Imports Nwc.XmlRpc

Module RPC

    Public Class AvatarData

        Public AvatarName As String
        Public X As Integer
        Public Y As Integer

    End Class

    'http://opensimulator.org/wiki/RemoteAdmin

    ' known web interfaces
    'http://opensimulator.org/wiki/Known_Web_Interfaces_within_OpenSim

    Public Function RPC_admin_dialog(agentId As String, text As String) As Boolean

        Dim RegionUUID As String = GetRegionFromAgentID(agentId)
        Dim ht As Hashtable = New Hashtable From {
           {"password", Settings.MachineID},
           {"message", text}
        }
        Return SendRPC(RegionUUID, "admin_broadcast", ht)

    End Function
    Public Function RPC_admin_get_agent_list(RegionUUID As String) As AvatarData

        '!!!
        Return Nothing

        Dim ht As Hashtable = New Hashtable From {
           {"password", Settings.MachineID},
           {"region_id", RegionUUID}
        }
        Return GetRPCAvatarPos(RegionUUID, "admin_get_agent_list", ht)

    End Function
    Public Function RPC_admin_get_agent_count(RegionUUID As String) As Integer

        Dim ht As Hashtable = New Hashtable From {
           {"password", Settings.MachineID},
           {"region_id", RegionUUID}
        }
        Return GetRPC(RegionUUID, "admin_get_agent_count", ht)

    End Function

    Public Function RPC_admin_get_avatar_count(RegionUUID As String) As Integer

        Dim ht As Hashtable = New Hashtable From {
           {"password", Settings.MachineID},
           {"region_id", RegionUUID}
        }
        Return GetRPC(RegionUUID, "admin_get_avatar_count", ht)

    End Function
    Public Function RPC_Region_Command(RegionUUID As String, Message As String) As Boolean

        Dim ht As Hashtable = New Hashtable From {
           {"password", Settings.MachineID},
           {"command", Message}
        }
        Debug.Print($"admin_console_command {Message}")
        Application.DoEvents()
        Return SendRPC(RegionUUID, "admin_console_command", ht)

    End Function

    Public Function SendAdminMessage(RegionUUID As String, Message As String) As Boolean

        'http://opensimulator.org/wiki/RemoteAdmin:admin_dialog

        Dim ht As Hashtable = New Hashtable From {
           {"password", Settings.MachineID},
           {"message", Message}
       }
        Log("Info", "Message to " & PropRegionClass.RegionName(RegionUUID) & " of " & Message)

        Return SendRPC(RegionUUID, "admin_dialog", ht)

    End Function

    Public Function SendMessage(RegionUUID As String, Message As String) As Boolean

        'http://opensimulator.org/wiki/RemoteAdmin:admin_broadcast

        Dim ht As Hashtable = New Hashtable From {
           {"password", Settings.MachineID},
           {"message", Message}
       }
        Log("Info", "Message to " & PropRegionClass.RegionName(RegionUUID) & " of " & Message)
        Return SendRPC(RegionUUID, "admin_broadcast", ht)

    End Function

    Public Function ShutDown(RegionUUID As String) As Boolean

        If Settings.MapType = "None" AndAlso PropRegionClass.MapType(RegionUUID).Length = 0 Then
            Dim ht As Hashtable = New Hashtable From {
            {"password", Settings.MachineID},
            {"region_id", RegionUUID}
        }
            Return SendRPC(RegionUUID, "admin_shutdown", ht)
        Else
            ConsoleCommand(RegionUUID, "q")
            ConsoleCommand(RegionUUID, "q")
        End If
        Return True

    End Function

    Public Function TeleportTo(FromRegionUUID As String, ToRegionName As String, AgentID As String) As Boolean

        'http://opensimulator.org/wiki/Remoteadmin:admin_teleport_agent

        Debug.Print("Teleport To:" & ToRegionName)

        Dim ht As Hashtable = New Hashtable From {
            {"password", Settings.MachineID},
            {"region_name", ToRegionName},
            {"agent_id", AgentID}
        }

        If FromRegionUUID.Length > 0 Then
            Return SendRPC(FromRegionUUID, "admin_teleport_agent", ht)
        End If
        Return False

    End Function

    Private Function GetRPC(FromRegionUUID As String, cmd As String, ht As Hashtable) As Integer

        Dim RegionPort = PropRegionClass.GroupPort(FromRegionUUID)
        Dim url = $"http://{Settings.LANIP}:{RegionPort}"


        Dim parameters = New List(Of Hashtable) From {ht}
        Dim RPC = New XmlRpcRequest(cmd, parameters)
        Try
            Dim o = RPC.Invoke(url)
            If o Is Nothing Then
                Return 0
            End If
#Disable Warning BC42016 ' Implicit conversion

            For Each s In o
                'Log("Info", s.Key & ":" & s.Value)
                If s.key = "count" Then
                    Return CInt(s.value)
                End If
            Next
#Enable Warning BC42016 ' Implicit conversion
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try
        Return 0

    End Function

    Private Function GetRPCAvatarPos(FromRegionUUID As String, cmd As String, ht As Hashtable) As AvatarData

        Dim RegionPort = PropRegionClass.GroupPort(FromRegionUUID)
        Dim url = $"http://{Settings.LANIP}:{RegionPort}"


        Dim parameters = New List(Of Hashtable) From {ht}
        Dim RPC = New XmlRpcRequest(cmd, parameters)
        Try
            Dim o = RPC.Invoke(url)
            If o Is Nothing Then
                Return Nothing
            End If
#Disable Warning BC42016 ' Implicit conversion
            Dim result As New AvatarData
            For Each s In o
                Log("Info", s.Key & ":" & s.Value)
                If s.Key = "Avatar" Then
                    result.AvatarName = s.value
                End If
            Next
            Return result
#Enable Warning BC42016 ' Implicit conversion
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try
        Return Nothing

    End Function

    Private Function SendRPC(FromRegionUUID As String, cmd As String, ht As Hashtable) As Boolean

        Dim RegionPort = PropRegionClass.GroupPort(FromRegionUUID)
        Dim url = $"http://{Settings.LANIP}:{RegionPort}"

        Dim parameters = New List(Of Hashtable) From {ht}
        Dim RPC = New XmlRpcRequest(cmd, parameters)
        Try
            Dim o = RPC.Invoke(url)
            If o Is Nothing Then Return True
#Disable Warning BC42016 ' Implicit conversion

            For Each s In o
                'Log("Info", s.Key & ":" & s.Value)
                If s.Key = "success" And s.Value = "True" Then
                    Debug.Print("Teleport Sent")
                    Return True
                End If
                If s.Key = "error" Then BreakPoint.Show(s.Value)
            Next
#Enable Warning BC42016 ' Implicit conversion
        Catch ex As Exception

        End Try
        Return False

    End Function

End Module
