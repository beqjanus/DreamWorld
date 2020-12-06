Imports System.IO

Public Module Firewall

    Function AddFirewallRules() As String

        ' TCP only for 8001 (DiagnosticPort) and both for 8002
        Dim Command As String = "netsh advfirewall firewall add rule name=""Diagnostics TCP Port " & Settings.DiagnosticPort & """ dir=in action=allow protocol=TCP localport=" & CStr(Settings.DiagnosticPort) & vbCrLf _
                          & "netsh advfirewall firewall add rule name=""Opensim HTTP TCP Port " & Settings.HttpPort & """ dir=in action=allow protocol=TCP localport=" & CStr(Settings.HttpPort) & vbCrLf _
                          & "netsh advfirewall firewall add rule name=""Opensim HTTP UDP Port " & Settings.HttpPort & """ dir=in action=allow protocol=UDP localport=" & CStr(Settings.HttpPort) & vbCrLf

        If Settings.ApacheEnable Then
            Command = Command & "netsh advfirewall firewall add rule name=""Apache HTTP Web Port " & CStr(Settings.ApachePort) & """ dir=in action=allow protocol=TCP localport=" & CStr(Settings.ApachePort) & vbCrLf
        End If

        ' Icecast needs both ports for both protocols
        If Settings.SCEnable Then
            Command = Command & "netsh advfirewall firewall add rule name=""Icecast Port1 UDP " & CStr(Settings.SCPortBase) & """ dir=in action=allow protocol=UDP localport=" & CStr(Settings.SCPortBase) & vbCrLf _
                          & "netsh advfirewall firewall add rule name=""Icecast Port1 TCP " & CStr(Settings.SCPortBase) & """ dir=in action=allow protocol=TCP localport=" & CStr(Settings.SCPortBase) & vbCrLf _
                          & "netsh advfirewall firewall add rule name=""Icecast Port2 UDP " & CStr(Settings.SCPortBase1) & """ dir=in action=allow protocol=UDP localport=" & CStr(Settings.SCPortBase1) & vbCrLf _
                          & "netsh advfirewall firewall add rule name=""Icecast Port2 TCP " & CStr(Settings.SCPortBase1) & """ dir=in action=allow protocol=TCP localport=" & CStr(Settings.SCPortBase1) & vbCrLf
        End If

        ' regions need both

        For Each RegionUUID As String In FormSetup.PropRegionClass.RegionUUIDs

            Dim X As Integer = CInt("0" & FormSetup.PropRegionClass.XMLRegionPort(RegionUUID))
            If X > 0 Then
                Command = Command & "netsh advfirewall firewall add rule name=""XMLRegionPort " & CStr(X) & """ dir=in action=allow protocol=TCP localport=" & CStr(X) & vbCrLf
            End If

            Dim R As Integer = CInt("0" & FormSetup.PropRegionClass.RegionPort(RegionUUID))
            If R > 0 Then
                Command = Command & "netsh advfirewall firewall add rule name=""Region TCP Port " & CStr(R) & """ dir=in action=allow protocol=TCP localport=" & CStr(R) & vbCrLf _
                          & "netsh advfirewall firewall add rule name=""Region UDP Port " & CStr(R) & """ dir=in action=allow protocol=UDP localport=" & CStr(R) & vbCrLf
            End If

        Next

        Return Command

    End Function

    Public Sub BlockIP(ipaddress As String)

        Dim Command As String = "netsh advfirewall firewall delete rule name=""Opensim Deny " & ipaddress & vbCrLf
        Command += "netsh advfirewall firewall add rule name=""Opensim Deny " & ipaddress & """ dir=in profile=any action=block protocol=any remoteip=" & IP() & vbCrLf

        Write(Command)

    End Sub

    Function DeleteFirewallRules() As String

        Dim Command As String = "netsh advfirewall firewall delete rule name=""Diagnostics TCP Port " & CStr(Settings.DiagnosticPort) & """" & vbCrLf _
                          & "netsh advfirewall firewall delete rule name=""Opensim HTTP TCP Port " & CStr(Settings.HttpPort) & """" & vbCrLf _
                          & "netsh advfirewall firewall delete rule name=""Opensim HTTP UDP Port " & CStr(Settings.HttpPort) & """" & vbCrLf

        If Settings.SCEnable Then
            Command += "netsh advfirewall firewall delete rule name=""Icecast Port1 UDP " & CStr(Settings.SCPortBase) & """" & vbCrLf _
                          & "netsh advfirewall firewall delete rule name=""Icecast Port1 TCP " & CStr(Settings.SCPortBase) & """" & vbCrLf _
                          & "netsh advfirewall firewall delete rule name=""Icecast Port2 UDP " & CStr(Settings.SCPortBase1) & """" & vbCrLf _
                          & "netsh advfirewall firewall delete rule name=""Icecast Port2 TCP " & CStr(Settings.SCPortBase1) & """" & vbCrLf

        End If

        If Settings.ApacheEnable Then
            Command = Command & "netsh advfirewall firewall delete rule name=""Apache HTTP Web Port " & CStr(Settings.ApachePort) & """" & vbCrLf
        End If

        For Each RegionUUID As String In FormSetup.PropRegionClass.RegionUUIDs

            Dim X As Integer = CInt("0" & FormSetup.PropRegionClass.XMLRegionPort(RegionUUID))
            If X > 0 Then
                Command = Command & "netsh advfirewall firewall delete rule name=""XMLRegionPort " & CStr(X) & """" & vbCrLf
            End If

            Dim R As Integer = CInt("0" & FormSetup.PropRegionClass.RegionPort(RegionUUID))
            If R > 0 Then
                Command = Command & "netsh advfirewall firewall delete rule name=""Region TCP Port " & CStr(R) & """" & vbCrLf _
                              & "netsh advfirewall firewall delete rule name=""Region UDP Port " & CStr(R) & """" & vbCrLf
            End If
        Next

        Return Command

    End Function

    Public Sub ReleaseIp(Ip As String)

        Dim Command As String = "netsh advfirewall firewall delete rule name=""Opensim Deny " & Ip & "" & vbCrLf
        Write(Command)

    End Sub

    Sub SetFirewall()

        Dim CMD As String = DeleteFirewallRules() & AddFirewallRules()

        Write(CMD)

    End Sub

    Private Sub Write(cmd As String)

        Try
            Using ns As StreamWriter = New StreamWriter(IO.Path.Combine(Settings.CurrentDirectory, "fw.bat"), False)
                ns.WriteLine(cmd)
            End Using
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try

        Dim pi As ProcessStartInfo = New ProcessStartInfo With {
            .Arguments = "",
            .FileName = IO.Path.Combine(Settings.CurrentDirectory, "fw.bat"),
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
                FormSetup.Log(My.Resources.Error_word, "Could not set firewall:" & ex.Message)
            End Try
        End Using

    End Sub

End Module
