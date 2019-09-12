Imports System.IO

Public Module Firewall

    Sub SetFirewall()

        Dim CMD As String = DeleteFirewallRules() & AddFirewallRules()

        Dim ns As StreamWriter = New StreamWriter(Form1.PropMyFolder & "\fw.bat", False)
        ns.WriteLine(CMD)
        ns.Close()

        Dim pi As ProcessStartInfo = New ProcessStartInfo With {
            .Arguments = "",
            .FileName = Form1.PropMyFolder & "\fw.bat",
            .WindowStyle = ProcessWindowStyle.Hidden,
            .Verb = "runas"
        }
        Dim ProcessFirewall As Process = New Process With {
            .StartInfo = pi
        }

        Try
            ProcessFirewall.Start()
        Catch ex As ObjectDisposedException
            Form1.Log("Error", "Could not set firewall:" & ex.Message)
        Catch ex As InvalidOperationException
            Form1.Log("Error", "Could not set firewall:" & ex.Message)
        Catch ex As System.ComponentModel.Win32Exception
            Form1.Log("Error", "Could not set firewall:" & ex.Message)
        End Try
    End Sub

    Function AddFirewallRules() As String

        ' TCP only for 8001 (DiagnosticPort) and both for 8002
        Dim Command As String = "netsh advfirewall firewall  add rule name=""Opensim TCP Port " & Form1.PropMySetting.DiagnosticPort & """ dir=in action=allow protocol=TCP localport=" & CStr(Form1.PropMySetting.DiagnosticPort) & vbCrLf _
                          & "netsh advfirewall firewall  add rule name=""Opensim HTTP TCP Port " & Form1.PropMySetting.HttpPort & """ dir=in action=allow protocol=TCP localport=" & CStr(Form1.PropMySetting.HttpPort) & vbCrLf _
                          & "netsh advfirewall firewall  add rule name=""Opensim HTTP UDP Port " & Form1.PropMySetting.HttpPort & """ dir=in action=allow protocol=UDP localport=" & CStr(Form1.PropMySetting.HttpPort) & vbCrLf

        If Form1.PropMySetting.ApacheEnable Then
            Command = Command & "netsh advfirewall firewall  add rule name=""Apache HTTP Web Port " & CStr(Form1.PropMySetting.ApachePort) & """ dir=in action=allow protocol=TCP localport=" & CStr(Form1.PropMySetting.ApachePort) & vbCrLf
        End If

        ' Icecast needs both ports for both protocols
        If Form1.PropMySetting.SCEnable Then
            Command = Command & "netsh advfirewall firewall  add rule name=""Icecast Port1 UDP " & CStr(Form1.PropMySetting.SCPortBase) & """ dir=in action=allow protocol=UDP localport=" & CStr(Form1.PropMySetting.SCPortBase) & vbCrLf _
                          & "netsh advfirewall firewall  add rule name=""Icecast Port1 TCP " & CStr(Form1.PropMySetting.SCPortBase) & """ dir=in action=allow protocol=TCP localport=" & CStr(Form1.PropMySetting.SCPortBase) & vbCrLf _
                          & "netsh advfirewall firewall  add rule name=""Icecast Port2 UDP " & CStr(Form1.PropMySetting.SCPortBase1) & """ dir=in action=allow protocol=UDP localport=" & CStr(Form1.PropMySetting.SCPortBase1) & vbCrLf _
                          & "netsh advfirewall firewall  add rule name=""Icecast Port2 TCP " & CStr(Form1.PropMySetting.SCPortBase1) & """ dir=in action=allow protocol=TCP localport=" & CStr(Form1.PropMySetting.SCPortBase1) & vbCrLf
        End If

        Dim RegionNumber As Integer = 0
        Dim start = CInt(Form1.PropMySetting.FirstRegionPort)

        ' regions need both
        For RegionNumber = start To Form1.PropMaxPortUsed
            Command = Command & "netsh advfirewall firewall  add rule name=""Region TCP Port " & CStr(RegionNumber) & """ dir=in action=allow protocol=TCP localport=" & CStr(RegionNumber) & vbCrLf _
                          & "netsh advfirewall firewall  add rule name=""Region UDP Port " & CStr(RegionNumber) & """ dir=in action=allow protocol=UDP localport=" & CStr(RegionNumber) & vbCrLf
        Next

        Return Command

    End Function

    Function DeleteFirewallRules() As String

        Dim Command As String = "netsh advfirewall firewall  delete rule name=""Opensim TCP Port " & CStr(Form1.PropMySetting.DiagnosticPort) & """" & vbCrLf _
                          & "netsh advfirewall firewall  delete rule name=""Opensim UDP Port " & CStr(Form1.PropMySetting.DiagnosticPort) & """" & vbCrLf _
                          & "netsh advfirewall firewall  delete rule name=""Opensim HTTP TCP Port " & CStr(Form1.PropMySetting.HttpPort) & """" & vbCrLf _
                          & "netsh advfirewall firewall  delete rule name=""Opensim HTTP UDP Port " & CStr(Form1.PropMySetting.HttpPort) & """" & vbCrLf _
                          & "netsh advfirewall firewall  delete rule name=""Icecast Port1 UDP " & CStr(Form1.PropMySetting.SCPortBase) & """" & vbCrLf _
                          & "netsh advfirewall firewall  delete rule name=""Icecast Port1 TCP " & CStr(Form1.PropMySetting.SCPortBase) & """" & vbCrLf _
                          & "netsh advfirewall firewall  delete rule name=""Icecast Port2 UDP " & CStr(Form1.PropMySetting.SCPortBase1) & """" & vbCrLf _
                          & "netsh advfirewall firewall  delete rule name=""Icecast Port2 TCP " & CStr(Form1.PropMySetting.SCPortBase1) & """" & vbCrLf

        If Form1.PropMySetting.ApacheEnable Then
            Command = Command & "netsh advfirewall firewall  delete rule name=""Apache HTTP Web Port " & CStr(Form1.PropMySetting.ApachePort) & """" & vbCrLf
        End If

        Dim RegionNumber As Integer = 0
        Dim start = CInt(Form1.PropMySetting.FirstRegionPort)

        For RegionNumber = start To Form1.PropMaxPortUsed
            Command = Command & "netsh advfirewall firewall  delete rule name=""Region TCP Port " & CStr(RegionNumber) & """" & vbCrLf _
                          & "netsh advfirewall firewall  delete rule name=""Region UDP Port " & CStr(RegionNumber) & """" & vbCrLf
        Next

        Return Command

    End Function

End Module