Imports System.IO
Imports System.Net

Module OpensimWorld

    Private LastTimeChecked As Date = Date.Now()

    Public Sub ScanOpenSimWorld(Force As Boolean)

        For Each RegionUUID As String In PropRegionClass.RegionUuids

            If PropRegionClass.Status(RegionUUID) <> RegionMaker.SIMSTATUSENUM.Booted Then Continue For

            '  30 minute timer and change detector for OpensimAPI
            Dim Delta = DateAndTime.DateDiff(DateInterval.Minute, LastTimeChecked, Date.Now)
            Dim Avatars As Integer = RPC_admin_get_agent_count(RegionUUID)
            PropRegionClass.InRegion(RegionUUID) = Avatars
            ' force an update for 30 minutes, at 1st boot when no one is there, and in another place, when it is booted, or when avatars leave or go.
            If Avatars <> PropRegionClass.InRegion(RegionUUID) Or Delta >= 30 Or Force Then
                SendToOpensimWorld(RegionUUID, Avatars)
            End If
        Next

    End Sub

    Public Sub SendToOpensimWorld(RegionUUID As String, Avatars As Integer)

        Dim k = PropRegionClass.OpensimWorldAPIKey(RegionUUID)
        If k.Length = 0 Then Return

        Dim Regionname = PropRegionClass.RegionName(RegionUUID)
        Dim pos = Uri.EscapeDataString("<128,128,23>")

        Dim URL = $"http://beacon.opensimworld.com/index.php/osgate/beacon/?wk={k}&na={Avatars}&rat=0&r={Regionname}&pos={pos}"
        ' If he says to delete it, we do so.
        If Poke(URL) = -1 Then PropRegionClass.OpensimWorldAPIKey(RegionUUID) = ""
        LastTimeChecked = Date.Now

    End Sub

    <CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")>
    Private Function Poke(URL As String) As Integer

        ' Create a New 'HttpWebRequest' Object to the mentioned URL.
        Dim myHttpWebRequest As HttpWebRequest = CType(WebRequest.Create(URL), HttpWebRequest)
        Dim outputData As String
        ' Assign the response object of 'HttpWebRequest' to a 'HttpWebResponse' variable.
        Dim myHttpWebResponse As HttpWebResponse = CType(myHttpWebRequest.GetResponse(), HttpWebResponse)
        'Debug.Print($"{vbCrLf}The HttpHeaders are {vbCrLf}Name {0}", myHttpWebRequest.Headers)
        ' Print the HTML contents of the page to the console.
        Using streamResponse As Stream = myHttpWebResponse.GetResponseStream()
            Using streamRead As StreamReader = New StreamReader(streamResponse)
                Dim readBuff As Char() = New Char(255) {}
                Dim count As Integer = streamRead.Read(readBuff, 0, 256)
                outputData = New String(readBuff, 0, count)
                While count > 0
                    count = streamRead.Read(readBuff, 0, 256)
                End While
            End Using
        End Using
        'Release the HttpWebResponse Resource.

        myHttpWebResponse.Close()
        If outputData = "OK" Then Return 1
        If outputData = "DISABLE" Then Return -1
        Return 0

    End Function

End Module
