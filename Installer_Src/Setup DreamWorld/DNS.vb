Imports System.Net

Module DNS

    Private _DNSisRgistered As Boolean

    Private Property DNSisRgistered As Boolean
        Get
            Return _DNSisRgistered
        End Get
        Set(value As Boolean)
            _DNSisRgistered = value
        End Set
    End Property

    Public Function RegisterName(DNSName As String, force As Boolean) As Boolean

        If DNSName Is Nothing Then Return False

        If DNSName.Length < 3 Then Return False

        Dim Checkname As String = String.Empty
        If Settings.ServerType <> "Robust" Then
            Return True
        End If

        If IPCheck.IsPrivateIP(DNSName) Then
            Return False
        End If

        If DNSisRgistered And Not force Then Return True

        Using client As New WebClient ' download client for web pages
            Try
                Checkname = client.DownloadString("http://ns1.outworldz.net/dns.plx" & FormSetup.GetPostData(DNSName))
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
                Try
                    Checkname = client.DownloadString("http://ns2.outworldz.net/dns.plx" & FormSetup.GetPostData(DNSName))
                Catch ex1 As Exception
                    ErrorLog("Warn: Cannot register this DNS Name " & ex1.Message)
                    Return False
                End Try
            End Try
        End Using

        If Checkname = "UPDATE" Then
            DNSisRgistered = True
            Return True
        End If
        If Checkname = "NAK" Then
            MsgBox(DNSName & ":" & My.Resources.DDNS_In_Use)
        End If
        Return False

    End Function

    Public Function SetPublicIP() As Boolean

        ' LAN USE

        If Settings.DNSName.Length > 0 Then
            Settings.PublicIP = Settings.DNSName()
            Settings.SaveSettings()
            FormSetup.Print(My.Resources.Setup_Network)
            Dim ret = RegisterName(Settings.PublicIP, False)
            Dim array As String() = Settings.AltDnsName.Split(",".ToCharArray())
            For Each part As String In array
                If part.Length > 0 Then
                    RegisterName(part, False)
                End If
            Next
            Return ret
        Else
            Settings.PublicIP = FormSetup.PropMyUPnpMap.LocalIP
            FormSetup.Print(My.Resources.Setup_Network)
            Settings.SaveSettings()
        End If

        ' HG USE

        If Not IPCheck.IsPrivateIP(Settings.DNSName) Then
            FormSetup.Print(My.Resources.Public_IP_Setup_Word)
            If Settings.DNSName.Length > 3 Then
                Settings.PublicIP = Settings.DNSName
                Settings.SaveSettings()
            End If

            Dim UC = Settings.PublicIP.ToUpperInvariant()
            If UC.Contains("OUTWORLDZ.NET") Or UC.Contains("INWORLDZ.NET") Then
                FormSetup.Print(My.Resources.DynDNS & " http://" & Settings.PublicIP & ":" & Settings.HttpPort)
            End If

            RegisterName(Settings.PublicIP, False)
            Dim array As String() = Settings.AltDnsName.Split(",".ToCharArray())
            For Each part As String In array
                RegisterName(part, False)
            Next

        End If

        If Settings.PublicIP.ToUpperInvariant() = "LOCALHOST" Or Settings.PublicIP = "127.0.0.1" Then
            RegisterName(Settings.PublicIP, False)
            Return True
        End If

        Log(My.Resources.Info_word, "Public IP=" & Settings.PublicIP)
        FormSetup.TestPublicLoopback()
        If Settings.DiagFailed = "False" Then

            Using client As New WebClient ' download client for web pages
                Try
                    ' Set Public IP
                    Settings.PublicIP = client.DownloadString("http://api.ipify.org/?r=" & RandomNumber.Random())
                Catch ex As Exception
                    BreakPoint.Show(ex.Message)
                    ErrorLog(My.Resources.Wrong & "@ api.ipify.org")
                    Settings.DiagFailed = "True"
                End Try
            End Using

            Settings.SaveSettings()
            Return True
        End If

        Settings.PublicIP = FormSetup.PropMyUPnpMap.LocalIP
        Settings.SaveSettings()

        Return False

    End Function

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
                If RegisterName(newname, True) Then
                    Settings.DNSName = newname
                    Settings.PublicIP = newname
                    Settings.SaveSettings()
                    MsgBox(My.Resources.NameAlreadySet, vbInformation, Global.Outworldz.My.Resources.Information_word)
                End If
            End If

        End If

    End Sub

End Module
