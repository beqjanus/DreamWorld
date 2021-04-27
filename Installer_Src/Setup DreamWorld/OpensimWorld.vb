Imports System.IO
Imports System.Net

Module OpensimWorld

    Private LastTimeChecked As Date = Date.Now()

    Public Sub ScanOpenSimWorld()

        For Each RegionUUID As String In PropRegionClass.RegionUuids

            If PropRegionClass.Status(RegionUUID) <> RegionMaker.SIMSTATUSENUM.Booted Then Continue For
            Dim K = PropRegionClass.OpensimWorldAPIKey(RegionUUID)
            If K.Length = 0 Then Continue For
            Dim pos = Uri.EscapeDataString("<128,128,23>")
            Dim RegionName = PropRegionClass.RegionName(RegionUUID)

            '  30 minute timer and change detector for OpensimAPI
            Dim Delta = DateAndTime.DateDiff(DateInterval.Minute, LastTimeChecked, Date.Now)
            Dim Avatars As Integer = RPC_admin_get_agent_count(RegionUUID)

            Dim URL = $"http://beacon.opensimworld.com/index.php/osgate/beacon/?wk={K}&na={Avatars}&rat=0&r={RegionName}&pos={pos}"
            If Avatars <> PropRegionClass.InRegion(RegionUUID) Or Delta >= 30 Then
                If Poke(URL, RegionUUID) = -1 Then PropRegionClass.OpensimWorldAPIKey(RegionUUID) = ""
                PropRegionClass.InRegion(RegionUUID) = Avatars
                LastTimeChecked = Date.Now
            End If
        Next

    End Sub

    Private Function Poke(URL As String, RegionUUID As String) As Integer

        ' Create a New 'HttpWebRequest' Object to the mentioned URL.
        Dim myHttpWebRequest As HttpWebRequest = CType(WebRequest.Create(URL), HttpWebRequest)

        ' Assign the response object of 'HttpWebRequest' to a 'HttpWebResponse' variable.
        Dim myHttpWebResponse As HttpWebResponse = CType(myHttpWebRequest.GetResponse(), HttpWebResponse)
        'Debug.Print($"{vbCrLf}The HttpHeaders are {vbCrLf}Name {0}", myHttpWebRequest.Headers)
        ' Print the HTML contents of the page to the console.
        Dim streamResponse As Stream = myHttpWebResponse.GetResponseStream()
        Dim streamRead As StreamReader = New StreamReader(streamResponse)
        Dim readBuff As Char() = New Char(255) {}

        Dim count As Integer = streamRead.Read(readBuff, 0, 256)
        Dim outputData As String = New String(readBuff, 0, count)
        While count > 0
            count = streamRead.Read(readBuff, 0, 256)
        End While
        ' Close the Stream object.
        streamResponse.Close()
        streamRead.Close()
        'Release the HttpWebResponse Resource.

        myHttpWebResponse.Close()
        If outputData = "OK" Then Return 1
        If outputData = "DISABLE" Then Return -1
        Return 0

    End Function

End Module
