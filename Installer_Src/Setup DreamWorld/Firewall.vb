Imports System.IO

Public Module Firewall

    Function AddFirewallRules() As String

        ' TCP only for 8001 (DiagnosticPort) and both for 8002
        Dim Command As String = "netsh advfirewall firewall  add rule name=""Opensim TCP Port " & Settings.DiagnosticPort & """ dir=in action=allow protocol=TCP localport=" & CStr(Settings.DiagnosticPort) & vbCrLf _
                          & "netsh advfirewall firewall  add rule name=""Opensim HTTP TCP Port " & Settings.HttpPort & """ dir=in action=allow protocol=TCP localport=" & CStr(Settings.HttpPort) & vbCrLf _
                          & "netsh advfirewall firewall  add rule name=""Opensim HTTP UDP Port " & Settings.HttpPort & """ dir=in action=allow protocol=UDP localport=" & CStr(Settings.HttpPort) & vbCrLf

        If Settings.ApacheEnable Then
            Command = Command & "netsh advfirewall firewall  add rule name=""Apache HTTP Web Port " & CStr(Settings.ApachePort) & """ dir=in action=allow protocol=TCP localport=" & CStr(Settings.ApachePort) & vbCrLf
        End If

        If Settings.RemoteAdminPort.Length > 0 Then
            Command = Command & "netsh advfirewall firewall  add rule name=""RemoteAdmin Port " & Settings.RemoteAdminPort & """ dir=in action=allow protocol=TCP localport=" & Settings.RemoteAdminPort & vbCrLf
        End If

        ' Icecast needs both ports for both protocols
        If Settings.SCEnable Then
            Command = Command & "netsh advfirewall firewall  add rule name=""Icecast Port1 UDP " & CStr(Settings.SCPortBase) & """ dir=in action=allow protocol=UDP localport=" & CStr(Settings.SCPortBase) & vbCrLf _
                          & "netsh advfirewall firewall  add rule name=""Icecast Port1 TCP " & CStr(Settings.SCPortBase) & """ dir=in action=allow protocol=TCP localport=" & CStr(Settings.SCPortBase) & vbCrLf _
                          & "netsh advfirewall firewall  add rule name=""Icecast Port2 UDP " & CStr(Settings.SCPortBase1) & """ dir=in action=allow protocol=UDP localport=" & CStr(Settings.SCPortBase1) & vbCrLf _
                          & "netsh advfirewall firewall  add rule name=""Icecast Port2 TCP " & CStr(Settings.SCPortBase1) & """ dir=in action=allow protocol=TCP localport=" & CStr(Settings.SCPortBase1) & vbCrLf
        End If

        If Settings.FirstXMLRegionPort > 1024 Then
            Command = Command & "netsh advfirewall firewall  add rule name=""XMLRegionPort " & CStr(Settings.FirstXMLRegionPort) & """ dir=in action=allow protocol=TCP localport=" & CStr(Settings.FirstXMLRegionPort) & vbCrLf
        End If

        Dim start = CInt("0" & Settings.FirstRegionPort)

        ' regions need both
        For port As Integer = start To Form1.PropMaxPortUsed
            Command = Command & "netsh advfirewall firewall  add rule name=""Region TCP Port " & CStr(port) & """ dir=in action=allow protocol=TCP localport=" & CStr(port) & vbCrLf _
                          & "netsh advfirewall firewall  add rule name=""Region UDP Port " & CStr(port) & """ dir=in action=allow protocol=UDP localport=" & CStr(port) & vbCrLf
        Next

        Return Command

    End Function

    Public Sub BlockIP(Ip As String)

        Dim Command As String = "netsh advfirewall firewall add rule name=""Opensim Deny " & Ip & """ dir=in profile=any action=block protocol=any remoteip=" & Ip & vbCrLf
        Write(Command)

    End Sub

    Function DeleteFirewallRules() As String

        Dim Command As String = "netsh advfirewall firewall  delete rule name=""Opensim TCP Port " & CStr(Settings.DiagnosticPort) & """" & vbCrLf _
                          & "netsh advfirewall firewall  delete rule name=""Opensim HTTP TCP Port " & CStr(Settings.HttpPort) & """" & vbCrLf _
                          & "netsh advfirewall firewall  delete rule name=""Opensim HTTP UDP Port " & CStr(Settings.HttpPort) & """" & vbCrLf

        If Settings.SCEnable Then
            Command += "netsh advfirewall firewall  delete rule name=""Icecast Port1 UDP " & CStr(Settings.SCPortBase) & """" & vbCrLf _
                          & "netsh advfirewall firewall  delete rule name=""Icecast Port1 TCP " & CStr(Settings.SCPortBase) & """" & vbCrLf _
                          & "netsh advfirewall firewall  delete rule name=""Icecast Port2 UDP " & CStr(Settings.SCPortBase1) & """" & vbCrLf _
                          & "netsh advfirewall firewall  delete rule name=""Icecast Port2 TCP " & CStr(Settings.SCPortBase1) & """" & vbCrLf

        End If

        If Settings.ApacheEnable Then
            Command = Command & "netsh advfirewall firewall  delete rule name=""Apache HTTP Web Port " & CStr(Settings.ApachePort) & """" & vbCrLf
        End If

        If Settings.FirstXMLRegionPort > 1024 Then
            Command = Command & "netsh advfirewall firewall  delete rule name=""XMLRegionPort " & CStr(Settings.FirstXMLRegionPort) & """" & vbCrLf
        End If

        If Settings.RemoteAdminPort.Length > 0 Then
            Command = Command & "netsh advfirewall firewall  delete rule name=""RemoteAdmin Port " & Settings.RemoteAdminPort & """ dir=in action=allow protocol=TCP localport=" & Settings.RemoteAdminPort & vbCrLf
        End If

        Dim start = CInt("0" & Settings.FirstRegionPort)
        For Port As Integer = start To Form1.PropMaxPortUsed
            Command = Command & "netsh advfirewall firewall  delete rule name=""Region TCP Port " & CStr(Port) & """" & vbCrLf _
                          & "netsh advfirewall firewall  delete rule name=""Region UDP Port " & CStr(Port) & """" & vbCrLf
        Next

        Return Command

    End Function

    Public Sub ReleaseIp(Ip As String)

        Dim Command As String = "netsh advfirewall firewall delete rule name=""Opensim Deny " & Ip & "" & vbCrLf
        Write(Command)

        Write(Command)

    End Sub

    Sub SetFirewall()

        Dim CMD As String = DeleteFirewallRules() & AddFirewallRules()

        Write(CMD)

    End Sub

    <CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId:="Outworldz.Form1.Log(System.String,System.String)")>
    Private Sub Write(cmd As String)

        Try
            Dim ns As StreamWriter = New StreamWriter(Settings.CurrentDirectory & "\fw.bat", False)
            ns.WriteLine(cmd)
            'If Debugger.IsAttached Then
            'ns.WriteLine("@pause")
            'End If
            ns.Close()
        Catch ex As Exception

            BreakPoint.Show(ex.Message)
        End Try

        Dim pi As ProcessStartInfo = New ProcessStartInfo With {
            .Arguments = "",
            .FileName = Settings.CurrentDirectory & "\fw.bat",
            .WindowStyle = ProcessWindowStyle.Hidden,
            .Verb = "runas"
        }
        Using ProcessFirewall As Process = New Process With {
                .StartInfo = pi
            }

            Try
                ProcessFirewall.Start()
            Catch ex As Exception

                BreakPoint.Show(ex.Message)
                Form1.Log(My.Resources.Error_word, "Could not set firewall:" & ex.Message)
            End Try

        End Using

    End Sub

End Module
