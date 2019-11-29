Module GridNames

    Public Sub SetServerNames()
        'GridNames.SetServerNames
        ' setup some defaults
#Disable Warning CA2000 ' Dispose objects before losing scope
        Dim PropMyUPnpMap = New UPnp()
#Enable Warning CA2000 ' Dispose objects before losing scope

        ' all private in case of local mode
        Form1.Settings.PublicIP = PropMyUPnpMap.LocalIP
        Form1.Settings.PrivateURL = Form1.Settings.PublicIP
        Form1.Settings.BaseHostName = Form1.Settings.PublicIP

        ' Set them back to the DNS name if there is one
        If Form1.Settings.DNSName.Length > 0 Then
            Form1.Settings.PublicIP = Form1.Settings.DNSName
            Form1.Settings.BaseHostName = Form1.Settings.DNSName
            Form1.Print("DNS Name=" & Form1.Settings.PublicIP)
        End If

        If Form1.Settings.ServerType = "Robust" Then
            Form1.Settings.ExternalHostName = Form1.Settings.PublicIP
            Form1.Print("Robust")
        ElseIf Form1.Settings.ServerType = "OsGrid" Then
            Form1.Settings.PublicIP = "hg.osgrid.org"
            Form1.Settings.ExternalHostName = PublicIP.IP()
            Form1.Settings.BaseHostName = Form1.Settings.PublicIP
            Form1.Print("OSGrid")
        ElseIf Form1.Settings.ServerType = "Region" Then
            Form1.Print("Region")
            Form1.Settings.ExternalHostName = Form1.Settings.PublicIP
            Form1.Settings.BaseHostName = Form1.Settings.PublicIP
        ElseIf Form1.Settings.ServerType = "Metro" Then
            Form1.Settings.PublicIP = "hg.osgrid.org"
            Form1.Settings.ExternalHostName = PublicIP.IP()
            Form1.Settings.BaseHostName = Form1.Settings.PublicIP
            Form1.Print("Metro")
        End If

        If Form1.Settings.OverrideName.Length > 0 Then
            Form1.Settings.ExternalHostName = Form1.Settings.OverrideName
            Form1.Print("Region IP=" & Form1.Settings.ExternalHostName)
        End If

        Dim n = Form1.Settings.DNSName
        If n.Length = 0 Then n = "(none)"

        Form1.Print("WAN IP   = " & PublicIP.IP)
        Form1.Print("LAN IP   = " & Form1.Settings.PrivateURL)
        Form1.Print("DNS = " & n)
        Form1.Print("Region = " & Form1.Settings.ExternalHostName)
        Form1.Print("Hostname = " & Form1.Settings.BaseHostName)

    End Sub

End Module
