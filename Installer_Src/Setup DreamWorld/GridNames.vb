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

        ' Set them back to the DNS name if there is one
        If Settings.DNSName.Length > 0 Then
            Settings.PublicIP = Settings.DNSName
            Settings.BaseHostName = Settings.DNSName
            TextPrint("DNS Name=" & Settings.PublicIP)
        End If

        If Settings.ServerType = "Robust" Then
            Settings.ExternalHostName = Settings.PublicIP
            TextPrint(My.Resources.Server_Type_is & " Robust")
        ElseIf Settings.ServerType = "OsGrid" Then
            Settings.PublicIP = "hg.osgrid.org"
            Settings.ExternalHostName = PublicIP.IP()
            Settings.BaseHostName = Settings.PublicIP
            TextPrint(My.Resources.Server_Type_is & " OSGrid")
        ElseIf Settings.ServerType = "Region" Then
            TextPrint(My.Resources.Server_Type_is & " Region")
            Settings.ExternalHostName = Settings.PublicIP
            Settings.BaseHostName = Settings.PublicIP
        ElseIf Settings.ServerType = "Metro" Then
            Settings.PublicIP = "hg.osgrid.org"
            Settings.ExternalHostName = PublicIP.IP()
            Settings.BaseHostName = Settings.PublicIP
            TextPrint(My.Resources.Server_Type_is & " Metro")
        End If

        If Settings.OverrideName.Length > 0 Then
            Settings.ExternalHostName = Settings.OverrideName
            TextPrint("Region IP=" & Settings.ExternalHostName)
        End If

        Dim n = Settings.DNSName
        If n.Length = 0 Then n = "(none)"

        TextPrint("WAN IP   = " & PublicIP.IP)
        TextPrint("LAN IP   = " & Settings.PrivateIP())
        TextPrint("DNS = " & n)
        TextPrint("Region = " & Settings.ExternalHostName)
        TextPrint("Hostname = " & Settings.BaseHostName)

    End Sub

End Module
