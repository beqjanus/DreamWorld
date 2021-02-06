#Region "Copyright AGPL3.0"

' Copyright Outworldz, LLC.
' AGPL3.0  https://opensource.org/licenses/AGPL

#End Region

Module GridNames

    Public Sub SetServerNames()

        TextPrint(My.Resources.Setup_Network)
        ' setup some defaults
#Disable Warning CA2000 ' Dispose objects before losing scope
        Dim PropMyUPnpMap = New UPnp()
#Enable Warning CA2000 ' Dispose objects before losing scope

        ' all private in case of local mode
        Settings.PublicIP = PropMyUPnpMap.LocalIP
        Settings.PrivateIP = Settings.PublicIP
        Settings.BaseHostName = Settings.PublicIP

        If Settings.ServerType = RobustServer Then
            ' Set them back to the DNS name if there is one
            If Settings.DNSName.Length > 0 Then
                Settings.PublicIP = Settings.DNSName
                Settings.BaseHostName = Settings.DNSName
                TextPrint("DNS Name=" & Settings.PublicIP)
            End If

            Settings.ExternalHostName = Settings.PublicIP
            TextPrint(My.Resources.Server_Type_is & " Robust")

        ElseIf Settings.ServerType = OsgridServer Then

            Settings.DNSName = "hg.osgrid.org"
            Settings.BaseHostName = "hg.osgrid.org"
            Settings.ExternalHostName = Settings.PublicIP
            TextPrint(My.Resources.Server_Type_is & " OSGrid")

        ElseIf Settings.ServerType = RegionServer Then

            ' Leave DNS Name alone - its the robust machine
            Settings.ExternalHostName = Settings.PublicIP
            Settings.BaseHostName = Settings.DNSName
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

        TextPrint("WAN IP  = " & Settings.PublicIP)
        TextPrint("LAN IP  = " & Settings.PrivateIP())
        TextPrint("DNS     = " & n)
        TextPrint("Region  = " & Settings.ExternalHostName)
        TextPrint("Login   = " & Settings.BaseHostName)

    End Sub

End Module
