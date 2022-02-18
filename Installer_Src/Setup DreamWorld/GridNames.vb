#Region "Copyright AGPL3.0"

' Copyright Outworldz, LLC.
' AGPL3.0  https://opensource.org/licenses/AGPL

#End Region

Module GridNames

    Public Sub SetServerType()

        Dim n = Settings.DNSName
        If n.Length = 0 Then n = "(none)"

        If Settings.ServerType = RobustServerName Then
            TextPrint("--> " & My.Resources.Server_Type_is & " Robust")
        ElseIf Settings.ServerType = OsgridServer Then
            Settings.DNSName = "hg.osgrid.org"
            Settings.BaseHostName = "hg.osgrid.org"
            Settings.ExternalHostName = WANIP()
            TextPrint("--> " & My.Resources.Server_Type_is & " OSGrid")
        ElseIf Settings.ServerType = RegionServerName Then
            If Settings.OverrideName.Length > 0 Then
                Settings.ExternalHostName = Settings.OverrideName
            Else
                Settings.ExternalHostName = Settings.PublicIP
            End If
            TextPrint("--> " & My.Resources.Server_Type_is & " Region")
        ElseIf Settings.ServerType = MetroServer Then
            Settings.DNSName = "hg.metro.land"
            Settings.ExternalHostName = Settings.PublicIP
            Settings.BaseHostName = "hg.metro.land"
            TextPrint("--> " & My.Resources.Server_Type_is & " Metro")
        End If

        If Settings.OverrideName.Length > 0 Then
            Settings.ExternalHostName = Settings.OverrideName
            TextPrint("--> Region IP=" & Settings.ExternalHostName)
        ElseIf Settings.ServerType = RobustName Then
            Settings.ExternalHostName = Settings.PublicIP
            TextPrint("--> Region IP=" & Settings.ExternalHostName)
        End If

        TextPrint("--> WAN IP = " & Settings.WANIP)
        TextPrint("--> DNS = " & n)
        TextPrint("--> WAN = " & Settings.PublicIP)
        TextPrint("--> LAN IP = " & Settings.LANIP())
        TextPrint("--> Region IP= " & Settings.ExternalHostName)
        If Settings.ServerType = RobustServerName Then
            TextPrint("--> Login = " & "http://" & Settings.BaseHostName & ":" & Settings.HttpPort)
        ElseIf Settings.ServerType = RegionServerName Then
            TextPrint("--> Login = " & "http://" & Settings.BaseHostName & ":" & Settings.HttpPort)
        ElseIf Settings.ServerType = OsgridServer Then
            TextPrint("--> Login = " & "http://" & Settings.BaseHostName & ":80")
        ElseIf Settings.ServerType = MetroServer Then
            TextPrint("--> Login = " & "http://" & Settings.BaseHostName & ":80")
        End If

    End Sub

End Module
