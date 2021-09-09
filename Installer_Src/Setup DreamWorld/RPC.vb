#Region "Copyright AGPL3.0"

' Copyright Outworldz, LLC.
' AGPL3.0  https://opensource.org/licenses/AGPL

#End Region

Imports Nwc.XmlRpc

Module RPC

    Public Function RPC_admin_dialog(agentId As String, text As String) As Boolean

        Dim RegionUUID As String = GetRegionFromAgentID(agentId)
        Dim ht = New Hashtable From {
           {"password", Settings.MachineID},
           {"message", text}
        }
        Return SendRPC(RegionUUID, "admin_broadcast", ht)

    End Function

    ''' http://opensimulator.org/wiki/Remoteadmin:admin_get_agent_count
    ''' 
    Public Function RPC_admin_get_agent_count(RegionUUID As String) As Integer

        Dim ht = New Hashtable From {
           {"password", Settings.MachineID},
           {"region_id", RegionUUID}
        }
        Return GetRPC(RegionUUID, "admin_get_agent_count", ht)

    End Function

    ''' <summary>
    ''' Returns Object with Root, Regions and Agents
    ''' </summary>
    ''' <param name="RegionUUID"></param>
    ''' <returns>Object</returns>
    ''' 
    'http://opensimulator.org/wiki/Remoteadmin:admin_get_agents
    Public Function admin_get_agents(RegionUUID As String) As Integer

        Dim ht = New Hashtable From {
           {"password", Settings.MachineID},
           {"region_id", RegionUUID}
        }
        Dim o As Object = GetRPCAsObject(RegionUUID, "admin_get_agents", ht)
        If o Is Nothing Then Return 0

        Dim regions As Object = o.Root
        Dim c As Integer
        For Each agent As Object In CType(regions, IEnumerable(Of Object))
            For Each agents As Object In CType(agent, IEnumerable(Of Object))
                c += 1
            Next
        Next
        Return c

    End Function

    'http://opensimulator.org/wiki/RemoteAdmin
    ' New function only in Dreamgrid's version of Opensimulator
    ''' <summary>
    ''' Returns count of avatars in a region less NPCs'
    ''' </summary>
    ''' <param name="RegionUUID">RegionUUID</param>
    ''' <returns>integer</returns>
    Public Function RPC_admin_get_avatar_count(RegionUUID As String) As Integer

        Dim ht = New Hashtable From {
           {"password", Settings.MachineID},
           {"region_id", RegionUUID}
        }
        Return GetRPC(RegionUUID, "admin_get_avatar_count", ht)

    End Function

    Public Function RPC_Region_Command(RegionUUID As String, Message As String) As Boolean

        Dim ht = New Hashtable From {
           {"password", Settings.MachineID},
           {"command", Message}
        }
        Debug.Print($"admin_console_command {Message}")
        Application.DoEvents()
        Return SendRPC(RegionUUID, "admin_console_command", ht)

    End Function

    Public Function SendAdminMessage(RegionUUID As String, Message As String) As Boolean

        'http://opensimulator.org/wiki/RemoteAdmin:admin_dialog

        Dim ht = New Hashtable From {
           {"password", Settings.MachineID},
           {"message", Message}
        }
        Log("Info", "Message to " & PropRegionClass.RegionName(RegionUUID) & " of " & Message)

        Return SendRPC(RegionUUID, "admin_dialog", ht)

    End Function

    Public Function SendMessage(RegionUUID As String, Message As String) As Boolean

        'http://opensimulator.org/wiki/RemoteAdmin:admin_broadcast

        Dim ht = New Hashtable From {
           {"password", Settings.MachineID},
           {"message", Message}
       }
        Log("Info", "Message to " & PropRegionClass.RegionName(RegionUUID) & " of " & Message)
        Return SendRPC(RegionUUID, "admin_broadcast", ht)

    End Function

    Public Function ShutDown(RegionUUID As String) As Boolean

        If Settings.MapType = "None" AndAlso PropRegionClass.MapType(RegionUUID).Length = 0 Then
            Dim ht = New Hashtable From {
            {"password", Settings.MachineID},
            {"region_id", RegionUUID}
        }
            Return SendRPC(RegionUUID, "admin_shutdown", ht)
        Else
            ConsoleCommand(RegionUUID, "q{ENTER}")
            ConsoleCommand(RegionUUID, "q{ENTER}")
        End If
        Return True

    End Function

    Public Function TeleportTo(FromRegionUUID As String, ToRegionName As String, AgentID As String) As Boolean

        'http://opensimulator.org/wiki/Remoteadmin:admin_teleport_agent

        Debug.Print("Teleport To:" & ToRegionName)

        Dim ht = New Hashtable From {
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
        Try

            Dim RPC = New XmlRpcRequest(cmd, parameters)
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
        Catch
        End Try
        Return 0

    End Function

    Private Function GetRPCAsObject(FromRegionUUID As String, cmd As String, ht As Hashtable) As Root

        Dim RegionPort = PropRegionClass.GroupPort(FromRegionUUID)
        Dim url = $"http://{Settings.LANIP}:{RegionPort}"

        Dim parameters = New List(Of Hashtable) From {ht}
        Try
            Dim RPC = New XmlRpcRequest(cmd, parameters)
            Dim o As Root = CType(RPC.Invoke(url), Root)
            Return o
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try
        Return Nothing

    End Function

    Public Function RPC_admin_get_agent_list(RegionUUID As String) As AvatarData

        Dim result As New AvatarData With {
            .AvatarName = "",
            .X = 0,
            .Y = 0
        }

        Dim ht = New Hashtable From {
           {"password", Settings.MachineID},
           {"region_id", RegionUUID}
        }

        Try
            Dim o = GetRPCAsObject(RegionUUID, "admin_get_agents", ht)
            If o Is Nothing Then Return result
            If Not o.success Then Return result

            For Each region In o.regions
                For Each agent In region.agent
                    result.AvatarName = agent.name
                    result.X = agent.pos_x
                    result.Y = agent.pos_y
                Next
            Next

        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try
        Return result

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

    Public Class AvatarData

        Public AvatarName As String
        Public X As Single
        Public Y As Single

    End Class

    Public Class Root
        Public success As Boolean
        Public regions As List(Of CRegions)
    End Class

    Public Class CRegions
        Public name As String
        Public id As String
        Public agent As List(Of CAgents)
    End Class

    Public Class CAgents
        Public name As String       ' Name of Avatar
        Public type As String       ' NPC or User
        Public id As String         ' uuid of region
        Public current_parcel_id As String ' UUID of parcel the agent is currently over
        Public pos_x As Single      ' X position of the agent
        Public pos_y As Single      ' Y position Of the agent	
        Public pos_z As Single      ' Z position Of the agent	
        Public vel_x As Single      ' X velocity Of the agent	
        Public vel_y As Single      ' Y velocity Of the agent	
        Public v As Single          ' Z velocity Of the agent	
        Public lookat_x As Single   ' X gaze direction Of the agent	
        Public lookat_y As Single   ' X gaze direction Of the agent	
        Public lookat_z As Single   ' X gaze direction Of the agent	
        Public is_sat_on As Boolean ' True If the agent Is sat On the ground	
        Public is_sat_o As Boolean  ' True If the agent Is sat On an Object	
        Public is_flying As Boolean ' True If the agent Is flying


    End Class
End Module
