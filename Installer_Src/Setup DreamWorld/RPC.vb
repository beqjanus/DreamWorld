Imports Nwc.XmlRpc

Module RPC

    'http://opensimulator.org/wiki/RemoteAdmin

    Public Function Console_command(RegionUUID As String, Message As String) As Boolean

        Dim ht As Hashtable = New Hashtable From {
           {"password", Settings.MachineID},
           {"command", Message}
        }
        Return SendRPC(RegionUUID, "admin_console_command", ht)

    End Function

    Public Function ShutDown(RegionUUID As String) As Boolean

        Dim sd = ""
        Dim ms = 0
        If PropRegionClass.AvatarCount(RegionUUID) > 0 Then
            sd = "shutdown"
            ms = 60000
        End If

        Dim ht As Hashtable = New Hashtable From {
            {"password", Settings.MachineID},
            {"region_id", RegionUUID},
            {"shutdown", sd},
            {"milliseconds", ms}
        }

        Return SendRPC(RegionUUID, "admin_shutdown", ht)

    End Function

    Public Function Teleport(FromRegionUUID As String, ToRegionName As String, Fname As String, LName As String) As Boolean

        'http://opensimulator.org/wiki/Remoteadmin:admin_teleport_agent

        Dim ht As Hashtable = New Hashtable From {
            {"password", Settings.MachineID},
            {"region_id", ToRegionName},
            {"agent_first_name", Fname},
            {"agent_last_name", LName}
        }
        Log("Info", "Teleport to " & ToRegionName)
        Return SendRPC(FromRegionUUID, "admin_teleport_agent", ht)

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

    Private Function SendRPC(RegionUUID As String, cmd As String, ht As Hashtable) As Boolean

        Dim RegionPort = PropRegionClass.GroupPort(RegionUUID)
        Dim url = "http://127.0.0.1:" & RegionPort

        Dim parameters = New List(Of Hashtable) From {ht}
        Dim RPC = New XmlRpcRequest(cmd, parameters)
        Try
            Dim o = RPC.Invoke(url)
            If o Is Nothing Then Return True
#Disable Warning BC42016 ' Implicit conversion
            For Each s In o
                Log("Info", s.Key & ":" & s.Value)
                If s.Key = "success" And s.Value = "True" Then Return True
                If s.Key = "error" Then ErrorLog(s.Value)
            Next
#Enable Warning BC42016 ' Implicit conversion
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
            ErrorLog("Cannot send XMLRPC command to " & PropRegionClass.RegionName(RegionUUID))
        End Try
        Return False

    End Function

End Module
