Imports System.Net

Module Diags

    Private _myUPnpMap As UPnp

    Public Property PropMyUPnpMap As UPnp
        Get
            Return _myUPnpMap
        End Get
        Set(value As UPnp)
            _myUPnpMap = value
        End Set
    End Property

    Public Sub DoDiag()

        If IPCheck.IsPrivateIP(Settings.DNSName) Then
            Logger("INFO", Global.Outworldz.My.Resources.LAN_IP, "Diagnostics")
            TextPrint(My.Resources.LAN_IP)
            Return
        End If

        TextPrint("__________")
        TextPrint(My.Resources.Running_Network)
        Logger("INFO", Global.Outworldz.My.Resources.Running_Network, "Diagnostics")
        Settings.DiagFailed = "False"

        OpenPorts() ' Open router ports with UPnp
        ProbePublicPort() ' Probe using Outworldz like Canyouseeme.org does on HTTP port
        TestPrivateLoopback()   ' Diagnostics
        TestPublicLoopback()    ' Http port
        TestAllRegionPorts()    ' All Dos boxes, actually

        If Settings.DiagFailed = "True" Then
            Logger("Error", Global.Outworldz.My.Resources.Diags_Failed, "Diagnostics")
            Dim answer = MsgBox(My.Resources.Diags_Failed, MsgBoxStyle.YesNo Or MsgBoxStyle.MsgBoxSetForeground)
            If answer = vbYes Then
                ShowLog()
            End If
        Else
            NewDNSName()
        End If
        TextPrint("__________")

    End Sub

    Public Function GetPostData(Optional Name As String = "") As String

        If Name.Length = 0 Then Name = Settings.DNSName   ' optional Alt DNS name can come in
        Dim TotalSize As Double
        For Each RegionUUID As String In RegionUuids()
            TotalSize += SizeX(RegionUUID) / 256 * SizeY(RegionUUID) / 256
        Next

        Dim fs = CreateObject("Scripting.FileSystemObject")
        Dim d As Object = fs.GetDrive(fs.GetDriveName(fs.GetAbsolutePathName("C:")))

        Dim data As String = "?MachineID=" & Settings.MachineID() _
        & "&FriendlyName=" & WebUtility.UrlEncode(Settings.SimName) _
        & "&V=" & WebUtility.UrlEncode(PropMyVersion) _
        & "&OV=" & WebUtility.UrlEncode(PropSimVersion) _
        & "&isPublic=" & CStr(Settings.GDPR()) _
        & "&GridName=" & Name _
        & "&Port=" & CStr(Settings.HttpPort()) _
        & "&Category=" & Settings.Categories _
        & "&Description=" & Settings.Description _
        & "&IP=" & Settings.PublicIP _
        & "&ServerType=" & Settings.ServerType _
        & "&MAC=" & Settings.MacAddress _
        & "&ID0=" & CreateMD5(CStr(d.SerialNumber)) _
        & "&RegionSize=" & CStr(TotalSize) _
        & "&r=" & RandomNumber.Random()
        Return data

    End Function

    Public Function OpenPorts() As Boolean

        If OpenRouterPorts() Then ' open UPnp port
            Logger("INFO", "UPNP OK", "Diagnostics")
            Settings.UPnpDiag = True
            Settings.SaveSettings()
            Return True
        Else
            TextPrint(My.Resources.UPNP_Disabled)
            Settings.UPnpDiag = False
            Settings.SaveSettings()
            Return False
        End If

    End Function

    Public Function OpenRouterPorts() As Boolean

        If Not PropMyUPnpMap.UPnpEnabled And Settings.UPnPEnabled Then
            Settings.UPnPEnabled = False
            Settings.SaveSettings()
            Return False
        End If

        If Not Settings.UPnPEnabled Then
            Return False
        End If

        TextPrint(My.Resources.Open_Router_Ports)

        Log("UPnP", "Local IP seems to be " & PropMyUPnpMap.LocalIP)

        Try

            If Settings.SCEnable Then
                'Icecast 8100-8101

                If PropMyUPnpMap.Exists(Convert.ToInt16(Settings.SCPortBase), UPnp.MyProtocol.TCP) Then
                    PropMyUPnpMap.Remove(Convert.ToInt16(Settings.SCPortBase), UPnp.MyProtocol.TCP)
                End If
                Application.DoEvents()
                If PropMyUPnpMap.Add(PropMyUPnpMap.LocalIP, CType(Settings.SCPortBase, Integer), UPnp.MyProtocol.TCP, "Icecast TCP Public " & Settings.SCPortBase.ToString(Globalization.CultureInfo.InvariantCulture)) Then
                    TextPrint("--> " & My.Resources.Icecast_is_Set & ":TCP:" & Settings.SCPortBase.ToString(Globalization.CultureInfo.InvariantCulture))
                End If
                If Not PropOpensimIsRunning() Then Return False
                '0 UDP
                If PropMyUPnpMap.Exists(Convert.ToInt16(Settings.SCPortBase), UPnp.MyProtocol.UDP) Then
                    PropMyUPnpMap.Remove(Convert.ToInt16(Settings.SCPortBase), UPnp.MyProtocol.UDP)
                End If
                Application.DoEvents()
                If PropMyUPnpMap.Add(PropMyUPnpMap.LocalIP, CType(Settings.SCPortBase, Integer), UPnp.MyProtocol.UDP, "Icecast UDP Public " & Settings.SCPortBase.ToString(Globalization.CultureInfo.InvariantCulture)) Then
                    TextPrint("--> " & My.Resources.Icecast_is_Set & ":UDP:" & Settings.SCPortBase.ToString(Globalization.CultureInfo.InvariantCulture))
                End If
                If Not PropOpensimIsRunning() Then Return False

                '1 TCP
                If PropMyUPnpMap.Exists(Convert.ToInt16(Settings.SCPortBase1), UPnp.MyProtocol.TCP) Then
                    PropMyUPnpMap.Remove(Convert.ToInt16(Settings.SCPortBase1), UPnp.MyProtocol.TCP)
                End If
                Application.DoEvents()
                If PropMyUPnpMap.Add(PropMyUPnpMap.LocalIP, CType(Settings.SCPortBase1, Integer), UPnp.MyProtocol.TCP, "Icecast1 TCP Public " & Settings.SCPortBase1.ToString(Globalization.CultureInfo.InvariantCulture)) Then
                    TextPrint("--> " & My.Resources.Icecast_is_Set & ":TCP:" & Settings.SCPortBase1.ToString(Globalization.CultureInfo.InvariantCulture))
                End If
                If Not PropOpensimIsRunning() Then Return False

                '0 UDP
                If PropMyUPnpMap.Exists(Convert.ToInt16(Settings.SCPortBase1), UPnp.MyProtocol.UDP) Then
                    PropMyUPnpMap.Remove(Convert.ToInt16(Settings.SCPortBase1), UPnp.MyProtocol.UDP)
                End If
                Application.DoEvents()
                If Not PropOpensimIsRunning() Then Return False

                If PropMyUPnpMap.Add(PropMyUPnpMap.LocalIP, CType(Settings.SCPortBase1, Integer), UPnp.MyProtocol.UDP, "Icecast1 UDP Public " & Settings.SCPortBase1.ToString(Globalization.CultureInfo.InvariantCulture)) Then
                    TextPrint("--> " & My.Resources.Icecast_is_Set & ":UDP:" & Settings.SCPortBase1.ToString(Globalization.CultureInfo.InvariantCulture))
                End If

            End If ' IceCast
            Application.DoEvents()

            If Settings.ApacheEnable Then
                If PropMyUPnpMap.Exists(Settings.ApachePort, UPnp.MyProtocol.TCP) Then
                    PropMyUPnpMap.Remove(Settings.ApachePort, UPnp.MyProtocol.TCP)
                End If
                If PropMyUPnpMap.Add(PropMyUPnpMap.LocalIP, Settings.ApachePort, UPnp.MyProtocol.TCP, "Apache TCP Public " & Settings.SCPortBase.ToString(Globalization.CultureInfo.InvariantCulture)) Then
                    TextPrint("--> " & My.Resources.Apache_is_Set & ":TCP:" & Settings.ApachePort.ToString(Globalization.CultureInfo.InvariantCulture))
                End If
            End If
            Application.DoEvents()
            If Not PropOpensimIsRunning() Then Return False

            ' 8001 for Diagnostics
            If PropMyUPnpMap.Exists(Convert.ToInt16(Settings.DiagnosticPort, Globalization.CultureInfo.InvariantCulture), UPnp.MyProtocol.TCP) Then
                PropMyUPnpMap.Remove(Convert.ToInt16(Settings.DiagnosticPort, Globalization.CultureInfo.InvariantCulture), UPnp.MyProtocol.TCP)
            End If
            If PropMyUPnpMap.Add(PropMyUPnpMap.LocalIP, Convert.ToInt16(Settings.DiagnosticPort, Globalization.CultureInfo.InvariantCulture), UPnp.MyProtocol.TCP, "Opensim TCP Grid " & Settings.HttpPort) Then
                TextPrint("--> " & My.Resources.Diag_TCP_is_set_word & ":" & Settings.DiagnosticPort.ToString(Globalization.CultureInfo.InvariantCulture))
            End If
            Application.DoEvents()
            If Not PropOpensimIsRunning() Then Return False

            ' 8002 for TCP
            If PropMyUPnpMap.Exists(Convert.ToInt16(Settings.HttpPort, Globalization.CultureInfo.InvariantCulture), UPnp.MyProtocol.TCP) Then
                PropMyUPnpMap.Remove(Convert.ToInt16(Settings.HttpPort, Globalization.CultureInfo.InvariantCulture), UPnp.MyProtocol.TCP)
            End If
            If PropMyUPnpMap.Add(PropMyUPnpMap.LocalIP, Convert.ToInt16(Settings.HttpPort, Globalization.CultureInfo.InvariantCulture), UPnp.MyProtocol.TCP, "Opensim TCP Grid " & Settings.HttpPort) Then
                TextPrint("--> " & My.Resources.Grid_TCP_is_set_word & ":" & Settings.HttpPort.ToString(Globalization.CultureInfo.InvariantCulture))
            End If
            Application.DoEvents()
            If Not PropOpensimIsRunning() Then Return False

            ' 8002 for UDP
            If PropMyUPnpMap.Exists(Convert.ToInt16(Settings.HttpPort, Globalization.CultureInfo.InvariantCulture), UPnp.MyProtocol.UDP) Then
                PropMyUPnpMap.Remove(Convert.ToInt16(Settings.HttpPort, Globalization.CultureInfo.InvariantCulture), UPnp.MyProtocol.UDP)
            End If
            If PropMyUPnpMap.Add(PropMyUPnpMap.LocalIP, Convert.ToInt16(Settings.HttpPort, Globalization.CultureInfo.InvariantCulture), UPnp.MyProtocol.UDP, "Opensim UDP Grid " & Settings.HttpPort) Then
                TextPrint("--> " & My.Resources.Grid_UDP_is_set_word & ":" & Settings.HttpPort.ToString(Globalization.CultureInfo.InvariantCulture))
            End If

            Application.DoEvents()

            For Each RegionUUID As String In RegionUuids()
                Dim R As Integer = Region_Port(RegionUUID)

                If PropMyUPnpMap.Exists(R, UPnp.MyProtocol.UDP) Then
                    PropMyUPnpMap.Remove(R, UPnp.MyProtocol.UDP)
                End If
                Application.DoEvents()
                If Not PropOpensimIsRunning() Then Return False

                If PropMyUPnpMap.Add(PropMyUPnpMap.LocalIP, R, UPnp.MyProtocol.UDP, "Opensim UDP Region " & Region_Name(RegionUUID) & " ") Then
                    TextPrint("--> " & Region_Name(RegionUUID) & ":UDP:" & R.ToString(Globalization.CultureInfo.InvariantCulture))
                End If
                Application.DoEvents()
                If Not PropOpensimIsRunning() Then Return False

                If PropMyUPnpMap.Exists(R, UPnp.MyProtocol.TCP) Then
                    PropMyUPnpMap.Remove(R, UPnp.MyProtocol.TCP)
                End If
                Application.DoEvents()
                If Not PropOpensimIsRunning() Then Return False

                If PropMyUPnpMap.Add(PropMyUPnpMap.LocalIP, R, UPnp.MyProtocol.TCP, "Opensim TCP Region " & Region_Name(RegionUUID) & " ") Then
                    TextPrint("--> " & Region_Name(RegionUUID) & ":TCP:" & R.ToString(Globalization.CultureInfo.InvariantCulture))
                End If
                Application.DoEvents()
                If Not PropOpensimIsRunning() Then Return False

            Next
        Catch ex As Exception
            BreakPoint.Dump(ex)
            Log("UPnP", "UPnP Exception caught:  " & ex.Message)
            Return False
        End Try
        Return True 'successfully added

    End Function

    Public Sub PortTest(Weblink As String, Port As Integer)

        Dim result As String = ""
        Using client As New WebClient
            Try
                result = client.DownloadString(Weblink)
            Catch ex As WebException  ' not an error as could be a 404 from Diva being off
            Catch ex As Exception
                BreakPoint.Dump(ex)
                ErrorLog("Err:Loopback fail:" & result & ":" & ex.Message)
                Logger("Error", "Loopback fail: " & result & ":" & ex.Message, "Diagnostics")
            End Try
        End Using

        If result.Contains("DOCTYPE") Or result.Contains("Ooops!") Or result.Length = 0 Then
            Logger("INFO", Global.Outworldz.My.Resources.Loopback_Passed & " " & Port.ToString(Globalization.CultureInfo.InvariantCulture), "Diagnostics")
            TextPrint(My.Resources.Loopback_Passed & " " & Port.ToString(Globalization.CultureInfo.InvariantCulture))
        Else
            TextPrint(My.Resources.Loopback_Failed & " " & Weblink)
            Logger("INFO", Global.Outworldz.My.Resources.Loopback_Failed & " " & Weblink, "Diagnostics")
            Settings.LoopBackDiag = False
            Settings.DiagFailed = "True"
        End If

    End Sub

    Public Sub TestPublicLoopback()

        If IPCheck.IsPrivateIP(Settings.PublicIP) Then
            Logger("INFO", "Local LAN IP", "Diagnostics")
            Return
        End If

        If Settings.ServerType <> RobustServerName Then
            Logger("INFO", "Is Not Robust, Test Skipped", "Diagnostics")
            Return
        End If

        TextPrint(My.Resources.Checking_Loopback_word)
        'Logger("INFO", Global.Outworldz.My.Resources.Checking_Loopback_word, "Diagnostics")
        PortTest("http://" & Settings.PublicIP & ":" & Settings.HttpPort & "/?_TestLoopback=" & RandomNumber.Random, Settings.HttpPort)

    End Sub

    Private Sub ProbePublicPort()

        If Settings.ServerType <> RobustServerName Then
            Logger("INFO", "Server Is Not Robust", "Diagnostics")
            Return
        End If

        Dim isPortOpen As String = ""
        Using client As New WebClient ' download client for web pages

            ' collect some stats and test loopback with a HTTP_ GET to the webserver. Send unique, anonymous random ID, both of the versions of Opensim and this program, and the diagnostics test
            ' results See my privacy policy at https://outworldz.com/privacy.htm

            TextPrint(My.Resources.Checking_Router_word)
            Dim Url = PropDomain & "/cgi/probetest.plx" & GetPostData()
            Logger("INFO", "Using URL " & Url, "Diagnostics")
            Try
                isPortOpen = client.DownloadString(Url)
            Catch ex As WebException  ' not an error as could be a 404 from Diva being off
                BreakPoint.Dump(ex)
                Logger("Error", Global.Outworldz.My.Resources.Wrong & " " & ex.Message, "Diagnostics")
                ErrorLog(My.Resources.Wrong & " " & ex.Message)
            End Try
        End Using

        If isPortOpen = "yes" Then
            Logger("INFO", Global.Outworldz.My.Resources.Incoming_Works, "Diagnostics")
            TextPrint(My.Resources.Incoming_Works)
        Else
            Settings.LoopBackDiag = False
            Settings.DiagFailed = "True"
            Logger("INFO", Global.Outworldz.My.Resources.Internet_address & " " & Settings.PublicIP & ":" & Settings.HttpPort & Global.Outworldz.My.Resources.Not_Forwarded, "Diagnostics")
            TextPrint(My.Resources.Internet_address & " " & Settings.PublicIP & ":" & Settings.HttpPort & Global.Outworldz.My.Resources.Not_Forwarded)
        End If

    End Sub

    Private Sub TestAllRegionPorts()

        Dim result As String = ""
        Dim Len = RegionCount()

        Dim Used As New List(Of String)
        ' Boot them up
        For Each RegionUUID As String In RegionUuids()
            If IsBooted(RegionUUID) Then
                Dim RegionName = Region_Name(RegionUUID)

                If Used.Contains(RegionName) Then Continue For
                Used.Add(RegionName)
                Logger("INFO", "Testing region " & RegionName, "Diagnostics")
                Dim Port = GroupPort(RegionUUID)
                TextPrint(My.Resources.Checking_Loopback_word & " " & RegionName)
                Logger("INFO", Global.Outworldz.My.Resources.Checking_Loopback_word & " " & RegionName, "Diagnostics")
                PortTest("http://" & Settings.PublicIP & ":" & Port & "/?_TestLoopback=" & RandomNumber.Random, Port)
            End If
        Next

    End Sub

    Private Sub TestPrivateLoopback()

        Dim result As String = ""
        TextPrint(My.Resources.Checking_LAN_Loopback_word)
        Logger("Info", Global.Outworldz.My.Resources.Checking_LAN_Loopback_word, "Diagnostics")
        Dim weblink = "http://" & Settings.LANIP() & ":" & Settings.DiagnosticPort & "/?_TestLoopback=" & RandomNumber.Random()
        Logger("Info", "URL= " & weblink, "Diagnostics")
        Using client As New WebClient
            Try
                result = client.DownloadString(weblink)
            Catch ex As Exception
                BreakPoint.Dump(ex)
                Logger("Error", ex.Message, "Diagnostics")
            End Try
        End Using

        If result = "Test Completed" Then
            Logger("INFO", Global.Outworldz.My.Resources.Passed_LAN, "Diagnostics")
            TextPrint(My.Resources.Passed_LAN)
        Else
            Logger("INFO", Global.Outworldz.My.Resources.Failed_LAN & " " & weblink & " result was " & result, "Diagnostics")
            TextPrint(My.Resources.Failed_LAN & " " & weblink)
            Settings.LoopBackDiag = False
            Settings.DiagFailed = "True"
        End If

    End Sub

End Module
