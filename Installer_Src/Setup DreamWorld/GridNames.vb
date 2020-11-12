Module GridNames

    Public Sub SetServerNames()

        FormSetup.Print(My.Resources.Setup_Network)
        ' setup some defaults
#Disable Warning CA2000 ' Dispose objects before losing scope
        Dim PropMyUPnpMap = New UPnp()
#Enable Warning CA2000 ' Dispose objects before losing scope

        ' all private in case of local mode
        Settings.PublicIP = PropMyUPnpMap.LocalIP
        Settings.PrivateURL = Settings.PublicIP
        Settings.BaseHostName = Settings.PublicIP

        ' Set them back to the DNS name if there is one
        If Settings.DNSName.Length > 0 Then
            Settings.PublicIP = Settings.DNSName
            Settings.BaseHostName = Settings.DNSName
            FormSetup.Print("DNS Name=" & Settings.PublicIP)
        End If

        If Settings.ServerType = "Robust" Then
            Settings.ExternalHostName = Settings.PublicIP
            FormSetup.Print(My.Resources.Server_Type_is & " Robust")
        ElseIf Settings.ServerType = "OsGrid" Then
            Settings.PublicIP = "hg.osgrid.org"
            Settings.ExternalHostName = PublicIP.IP()
            Settings.BaseHostName = Settings.PublicIP
            FormSetup.Print(My.Resources.Server_Type_is & " OSGrid")
        ElseIf Settings.ServerType = "Region" Then
            FormSetup.Print(My.Resources.Server_Type_is & " Region")
            Settings.ExternalHostName = Settings.PublicIP
            Settings.BaseHostName = Settings.PublicIP
        ElseIf Settings.ServerType = "Metro" Then
            Settings.PublicIP = "hg.osgrid.org"
            Settings.ExternalHostName = PublicIP.IP()
            Settings.BaseHostName = Settings.PublicIP
            FormSetup.Print(My.Resources.Server_Type_is & " Metro")
        End If

        If Settings.OverrideName.Length > 0 Then
            Settings.ExternalHostName = Settings.OverrideName
            FormSetup.Print("Region IP=" & Settings.ExternalHostName)
        End If

        Dim n = Settings.DNSName
        If n.Length = 0 Then n = "(none)"

        FormSetup.Print("WAN IP   = " & PublicIP.IP)
        FormSetup.Print("LAN IP   = " & Settings.PrivateURL)
        FormSetup.Print("DNS = " & n)
        FormSetup.Print("Region = " & Settings.ExternalHostName)
        FormSetup.Print("Hostname = " & Settings.BaseHostName)

    End Sub

End Module
