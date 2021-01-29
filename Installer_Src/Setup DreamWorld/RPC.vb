#Region "Copyright AGPL3.0"

' Copyright Outworldz, LLC.
' AGPL3.0  https://opensource.org/licenses/AGPL

'Permission Is hereby granted, free Of charge, to any person obtaining a copy of this software
' And associated documentation files (the "Software"), to deal in the Software without restriction,
'including without limitation the rights To use, copy, modify, merge, publish, distribute, sublicense,
'And/Or sell copies Of the Software, And To permit persons To whom the Software Is furnished To
'Do so, subject To the following conditions:

'The above copyright notice And this permission notice shall be included In all copies Or '
'substantial portions Of the Software.

'THE SOFTWARE Is PROVIDED "AS IS", WITHOUT WARRANTY Of ANY KIND, EXPRESS Or IMPLIED,
' INCLUDING BUT Not LIMITED To THE WARRANTIES Of MERCHANTABILITY, FITNESS For A PARTICULAR
'PURPOSE And NONINFRINGEMENT.In NO Event SHALL THE AUTHORS Or COPYRIGHT HOLDERS BE LIABLE
'For ANY CLAIM, DAMAGES Or OTHER LIABILITY, WHETHER In AN ACTION Of CONTRACT, TORT Or
'OTHERWISE, ARISING FROM, OUT Of Or In CONNECTION With THE SOFTWARE Or THE USE Or OTHER
'DEALINGS IN THE SOFTWARE.Imports System

#End Region

Imports Nwc.XmlRpc

Module RPC

    'http://opensimulator.org/wiki/RemoteAdmin

    Public Function RPC_Region_Command(RegionUUID As String, Message As String) As Boolean

        Dim ht As Hashtable = New Hashtable From {
           {"password", Settings.MachineID},
           {"command", Message}
        }
        Return SendRPC(RegionUUID, "admin_console_command", ht)

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

    Public Function TeleportTo(ToRegionName As String, Name As String) As Boolean

        'http://opensimulator.org/wiki/Remoteadmin:admin_teleport_agent

        Dim n() = Name.Split(New Char() {" "c})

        Dim ht As Hashtable = New Hashtable From {
            {"password", Settings.MachineID},
            {"region_id", ToRegionName},
            {"agent_first_name", n(0)},
            {"agent_last_name", n(1)}
        }

        Dim FromRegionUUID As String = WhereisAgent(Name)

        If FromRegionUUID.Length > 0 Then
            Log("Info", "Teleport to " & ToRegionName)
            Return SendRPC(FromRegionUUID, "admin_teleport_agent", ht)
        End If
        Return False

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
                'Log("Info", s.Key & ":" & s.Value)
                If s.Key = "success" And s.Value = "True" Then Return True
                If s.Key = "error" Then ErrorLog(s.Value)
            Next
#Enable Warning BC42016 ' Implicit conversion
        Catch ex As Exception
            ' BreakPoint.Show(ex.Message)
            'ErrorLog("Cannot send XMLRPC command to " & PropRegionClass.RegionName(RegionUUID))
        End Try
        Return False

    End Function

End Module
