#Region "Copyright AGPL3.0"

' Copyright Outworldz, LLC.
' AGPL3.0  https://opensource.org/licenses/AGPL

#End Region

Module GridNames

    Public Sub SetServerType()

        If Settings.ServerType = RobustServer Then
            TextPrint(My.Resources.Server_Type_is & " Robust")
        ElseIf Settings.ServerType = OsgridServer Then
            Settings.DNSName = "hg.osgrid.org"
            Settings.BaseHostName = "hg.osgrid.org"
            Settings.ExternalHostName = Settings.PublicIP
            TextPrint(My.Resources.Server_Type_is & " OSGrid")
        ElseIf Settings.ServerType = RegionServer Then
            If Settings.OverrideName.Length > 0 Then
                Settings.ExternalHostName = Settings.OverrideName
            Else
                Settings.ExternalHostName = Settings.PublicIP
            End If
            TextPrint(My.Resources.Server_Type_is & " Region")
        ElseIf Settings.ServerType = MetroServer Then
            Settings.DNSName = "metro.land"
            Settings.ExternalHostName = Settings.PublicIP
            Settings.BaseHostName = "metro.land"
            TextPrint(My.Resources.Server_Type_is & " Metro")
        End If

        If Settings.OverrideName.Length > 0 Then
            Settings.ExternalHostName = Settings.OverrideName
            TextPrint("Region IP=" & Settings.ExternalHostName)
        End If

        Dim n = Settings.DNSName
        If n.Length = 0 Then n = "(none)"

        TextPrint("WAN     = " & Settings.PublicIP)
        TextPrint("LAN IP  = " & Settings.PrivateIP())
        TextPrint("DNS     = " & n)
        TextPrint("Region  = " & Settings.ExternalHostName)
        TextPrint("Login   = " & "http://" & Settings.BaseHostName & ":" & Settings.HttpPort)

    End Sub

End Module
