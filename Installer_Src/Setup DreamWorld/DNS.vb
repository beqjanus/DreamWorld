Imports System.Net

Module DNS

    Public Function GetNewDnsName() As String

        Dim client As New WebClient
        Dim Checkname As String
        Try
            Checkname = client.DownloadString("http://outworldz.net/getnewname.plx/?r=" & RandomNumber.Random)
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
            ErrorLog("Error:Cannot get new name:" & ex.Message)
            client.Dispose()
            Return ""
        End Try
        client.Dispose()
        Return Checkname

    End Function

    Public Sub NewDNSName()

        If Settings.DNSName.Length = 0 And Settings.EnableHypergrid Then
            Dim newname = GetNewDnsName()
            If newname.Length >= 0 Then
                If RegisterName(newname) Then
                    Settings.DNSName = newname
                    Settings.PublicIP = newname
                    Settings.SaveSettings()
                    MsgBox(My.Resources.NameAlreadySet, MsgBoxStyle.Information Or MsgBoxStyle.MsgBoxSetForeground, Global.Outworldz.My.Resources.Information_word)
                End If
            End If

        End If

    End Sub

    Public Function RegisterName(DNSName As String) As Boolean

        DNSName = DNSName.Trim

        If DNSName Is Nothing Then Return False

        If DNSName.Length = 0 Then Return False

        Dim Checkname As String = String.Empty

        If IPCheck.IsPrivateIP(DNSName) Then
            Return False
        End If

        Using client As New WebClient ' download client for web pages
            Try
                Checkname = client.DownloadString("http://ns1.outworldz.net/dns.plx" & GetPostData(DNSName))
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
                Try
                    Checkname = client.DownloadString("http://ns2.outworldz.net/dns.plx" & GetPostData(DNSName))
                Catch ex1 As Exception
                    ErrorLog("Warn: Cannot register this DNS Name " & ex1.Message)
                    Return False
                End Try
            End Try
        End Using

        If Checkname = "UPDATE" Then
            Return True
        End If
        If Checkname = "NAK" Then
            MsgBox(DNSName & ":" & My.Resources.DDNS_In_Use)
        End If
        Return False

    End Function

    Public Sub SetPublicIP()

        TextPrint(My.Resources.Setup_Network)

        Settings.WANIP = WANIP()
        Settings.LANIP = PropMyUPnpMap.LocalIP
        Settings.MacAddress = GetMacByIp(Settings.LANIP)

        ' Region Name override
        If Settings.OverrideName.Length > 0 Then
            RegisterName(Settings.OverrideName)
        End If

        ' set up the alternate DNS names
        Dim a As String() = Settings.AltDnsName.Split(",".ToCharArray())
        For Each part As String In a
            If part.Length > 0 Then
                RegisterName(part)
            End If
        Next

        ' WAN USE
        If Settings.DNSName.Length = 0 Then
            Settings.PublicIP = Settings.LANIP
        ElseIf Settings.DNSName.Length > 0 Then
            Settings.PublicIP = Settings.DNSName()
        ElseIf IsPrivateIP(Settings.PublicIP) Then
            ' NAT'd ROUTER
            Settings.PublicIP = Settings.LANIP
        Else
            ' WAN IP such as Contabo without a NAT
            Settings.PublicIP = Settings.WANIP
        End If

        Settings.BaseHostName = Settings.PublicIP
        Settings.ExternalHostName = Settings.PublicIP

        RegisterName(Settings.PublicIP)


    End Sub

End Module
