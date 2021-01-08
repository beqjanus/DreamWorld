Imports Nwc.XmlRpc

Module RPC

    Public Function ShutDown(RegionUUID As String) As Boolean
        Dim RegionPort = PropRegionClass.GroupPort(RegionUUID)
        Dim url = "http://127.0.0.1:" & RegionPort

        Dim ht As Hashtable = New Hashtable From {
            {"password", Settings.MachineID},
            {"region_id", RegionUUID}
        }

        Dim parameters = New List(Of Hashtable) From {ht}
        Dim RPC = New XmlRpcRequest("admin_shutdown", parameters)
        Try
            RPC.Invoke(url)
            Return True
        Catch ex As Exception
            'BreakPoint.Show(ex.Message)
        End Try
        Return False

    End Function

End Module
