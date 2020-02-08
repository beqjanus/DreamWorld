Imports System.IO

Public Module Firewall

    Function AddFirewallRules() As String

        ' TCP only for 8001 (DiagnosticPort) and both for 8002
        Dim Command As String = "netsh advfirewall firewall  add rule name=""Opensim TCP Port " & Form1.Settings.DiagnosticPort & """ dir=in action=allow protocol=TCP localport=" & CStr(Form1.Settings.DiagnosticPort) & vbCrLf _
                          & "netsh advfirewall firewall  add rule name=""Opensim HTTP TCP Port " & Form1.Settings.HttpPort & """ dir=in action=allow protocol=TCP localport=" & CStr(Form1.Settings.HttpPort) & vbCrLf _
                          & "netsh advfirewall firewall  add rule name=""Opensim HTTP UDP Port " & Form1.Settings.HttpPort & """ dir=in action=allow protocol=UDP localport=" & CStr(Form1.Settings.HttpPort) & vbCrLf

        If Form1.Settings.ApacheEnable Then
            Command = Command & "netsh advfirewall firewall  add rule name=""Apache HTTP Web Port " & CStr(Form1.Settings.ApachePort) & """ dir=in action=allow protocol=TCP localport=" & CStr(Form1.Settings.ApachePort) & vbCrLf
        End If

        ' Icecast needs both ports for both protocols
        If Form1.Settings.SCEnable Then
            Command = Command & "netsh advfirewall firewall  add rule name=""Icecast Port1 UDP " & CStr(Form1.Settings.SCPortBase) & """ dir=in action=allow protocol=UDP localport=" & CStr(Form1.Settings.SCPortBase) & vbCrLf _
                          & "netsh advfirewall firewall  add rule name=""Icecast Port1 TCP " & CStr(Form1.Settings.SCPortBase) & """ dir=in action=allow protocol=TCP localport=" & CStr(Form1.Settings.SCPortBase) & vbCrLf _
                          & "netsh advfirewall firewall  add rule name=""Icecast Port2 UDP " & CStr(Form1.Settings.SCPortBase1) & """ dir=in action=allow protocol=UDP localport=" & CStr(Form1.Settings.SCPortBase1) & vbCrLf _
                          & "netsh advfirewall firewall  add rule name=""Icecast Port2 TCP " & CStr(Form1.Settings.SCPortBase1) & """ dir=in action=allow protocol=TCP localport=" & CStr(Form1.Settings.SCPortBase1) & vbCrLf
        End If

        Dim start = CInt(Form1.Settings.FirstRegionPort)

        ' regions need both
        For port As Integer = start To Form1.PropMaxPortUsed
            Command = Command & "netsh advfirewall firewall  add rule name=""Region TCP Port " & CStr(port) & """ dir=in action=allow protocol=TCP localport=" & CStr(port) & vbCrLf _
                          & "netsh advfirewall firewall  add rule name=""Region UDP Port " & CStr(port) & """ dir=in action=allow protocol=UDP localport=" & CStr(port) & vbCrLf
        Next

        Return Command

    End Function

    Function DeleteFirewallRules() As String

        Dim Command As String = "netsh advfirewall firewall  delete rule name=""Opensim TCP Port " & CStr(Form1.Settings.DiagnosticPort) & """" & vbCrLf _
                          & "netsh advfirewall firewall  delete rule name=""Opensim HTTP TCP Port " & CStr(Form1.Settings.HttpPort) & """" & vbCrLf _
                          & "netsh advfirewall firewall  delete rule name=""Opensim HTTP UDP Port " & CStr(Form1.Settings.HttpPort) & """" & vbCrLf

        If Form1.Settings.SCEnable Then
            Command += "netsh advfirewall firewall  delete rule name=""Icecast Port1 UDP " & CStr(Form1.Settings.SCPortBase) & """" & vbCrLf _
                          & "netsh advfirewall firewall  delete rule name=""Icecast Port1 TCP " & CStr(Form1.Settings.SCPortBase) & """" & vbCrLf _
                          & "netsh advfirewall firewall  delete rule name=""Icecast Port2 UDP " & CStr(Form1.Settings.SCPortBase1) & """" & vbCrLf _
                          & "netsh advfirewall firewall  delete rule name=""Icecast Port2 TCP " & CStr(Form1.Settings.SCPortBase1) & """" & vbCrLf

        End If
        If Form1.Settings.ApacheEnable Then
            Command = Command & "netsh advfirewall firewall  delete rule name=""Apache HTTP Web Port " & CStr(Form1.Settings.ApachePort) & """" & vbCrLf
        End If

        Dim start = CInt(Form1.Settings.FirstRegionPort)
        For Port As Integer = start To Form1.PropMaxPortUsed
            Command = Command & "netsh advfirewall firewall  delete rule name=""Region TCP Port " & CStr(Port) & """" & vbCrLf _
                          & "netsh advfirewall firewall  delete rule name=""Region UDP Port " & CStr(Port) & """" & vbCrLf
        Next

        Return Command

    End Function

    Sub SetFirewall()

        Dim CMD As String = DeleteFirewallRules() & AddFirewallRules()

        Try
            Dim ns As StreamWriter = New StreamWriter(Form1.PropMyFolder & "\fw.bat", False)
            ns.WriteLine(CMD)
            'If Debugger.IsAttached Then
            'ns.WriteLine("@pause")
            'End If
            ns.Close()
        Catch ex As IOException
        Catch ex As UnauthorizedAccessException
        Catch ex As ArgumentException
        Catch ex As System.Security.SecurityException
        End Try
        Dim Windowstyle As ProcessWindowStyle
        Windowstyle = ProcessWindowStyle.Hidden

        Dim pi As ProcessStartInfo = New ProcessStartInfo With {
            .Arguments = "",
            .FileName = Form1.PropMyFolder & "\fw.bat",
            .WindowStyle = Windowstyle,
            .Verb = "runas"
        }
        Using ProcessFirewall As Process = New Process With {
                .StartInfo = pi
            }

            Try
                ProcessFirewall.Start()
            Catch ex As InvalidOperationException
                Form1.Log(My.Resources.Error_word, "Could not set firewall:" & ex.Message)
            Catch ex As System.ComponentModel.Win32Exception
                Form1.Log(My.Resources.Error_word, "Could not set firewall:" & ex.Message)
            End Try

        End Using

    End Sub

End Module
