Imports System.Net

Module OpensimWorld

    Private LastTimeChecked As Date = Date.Now()

    Public Sub ScanOpenSimWorld()

        For Each RegionUUID As String In PropRegionClass.RegionUuids

            If PropRegionClass.Status(RegionUUID) <> RegionMaker.SIMSTATUSENUM.Booted Then Continue For
            Dim K = PropRegionClass.OpensimWorldAPIKey(RegionUUID)
            If K.Length = 0 Then Continue For
            Dim pos = Uri.EscapeDataString("<128,128,23>")

            '  30 minute timer and change detector for OpensimAPI
            Dim Delta = DateAndTime.DateDiff(DateInterval.Minute, LastTimeChecked, Date.Now)
            Dim Avatars As Integer = RPC_admin_get_agent_count(RegionUUID)
            Dim URL = $"http://beacon.opensimworld.com/index.php/osgate/beacon/?wk={K}&na={Avatars}&rat=0&pos={pos}"

            If Avatars <> PropRegionClass.InRegion(RegionUUID) Or Delta >= 30 Then
                If PokeOpensimworld(URL) = -1 Then PropRegionClass.OpensimWorldAPIKey(RegionUUID) = ""
                PropRegionClass.InRegion(RegionUUID) = Avatars
                LastTimeChecked = Date.Now
            End If
        Next

    End Sub

    Private Function PokeOpensimworld(URL) As Integer

        Dim Result As String

        Using client As New WebClient ' download client for web pages
            Try
                Result = CType(client.DownloadString(URL), String)
                If Result = "OK" Then Return 1
                If Result = "DISABLE" Then Return -1
                TextPrint($"Opensimworld.com Server said: {vbCrLf}{Result}")
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
            End Try
        End Using

        Return 0

    End Function

End Module

'[2140] OSW Beacon: http://beacon.opensimworld.com/index.php/osgate/beacon/?wk=12345
'[21:40] OSW Beacon: &na=1
'[21:40] OSW Beacon: &r=Sandbox
'[21:40] OSW Beacon: &rat=
'[21:40] OSW Beacon: &pos=%3C130.434860%2C%20132.191223%2C%2022.862379%3E

' TODO:'
'string BASEURL="http://beacon.opensimworld.com/index.php/osgate";
'If (nNew!= nTotalAvis || minsSinceLast > 30 ) {
'nTotalAvis = nNew;
'beaconHttp = llHTTPRequest(
'BASEURL + "/beacon/" +
'   "?wk=" + wkey +
'  "&na=" + (String)nTotalAvis+
' "&r="+llEscapeURL(regionName)+
'"&rat="+s_rating+
'"&pos="+llEscapeURL((string)llGetPos()),
'[HTTP_BODY_MAXLENGTH, 16384], ""
');
'minsSinceLast = 0;
