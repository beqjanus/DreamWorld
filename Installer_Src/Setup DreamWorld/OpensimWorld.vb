Imports System.IO
Imports System.Net

Module OpensimWorld


    Public Sub ScanOpenSimWorld(Force As Boolean)

        For Each RegionUUID As String In PropRegionClass.RegionUuids

            If PropRegionClass.OpensimWorldAPIKey(RegionUUID).Length = 0 Then Continue For
            If Not PropRegionClass.RegionEnabled(RegionUUID) Then Continue For

            If PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.Booted Or
                (PropRegionClass.SmartStart(RegionUUID) = "True" And Settings.SmartStart) Then

                Dim Avatars As Integer = RPC_admin_get_avatar_count(RegionUUID)
                If Avatars <> PropRegionClass.InRegion(RegionUUID) Then
                    PropRegionClass.InRegion(RegionUUID) = Avatars
                    SendToOpensimWorld(RegionUUID, Avatars)
                    PropRegionClass.InRegion(RegionUUID) = Avatars
                ElseIf Force Then
                    PropRegionClass.InRegion(RegionUUID) = Avatars
                    SendToOpensimWorld(RegionUUID, Avatars)
                End If
            End If

        Next

    End Sub

    Public Sub SendToOpensimWorld(RegionUUID As String, Avatars As Integer)

        Dim k = PropRegionClass.OpensimWorldAPIKey(RegionUUID)
        If k.Length = 0 Then Return

        Dim Regionname = PropRegionClass.RegionName(RegionUUID)
        Dim pos = Uri.EscapeDataString("<128,128,23>")
        Logger("OpensimWorld", $"Update {Regionname}", "Outworldz")
        Dim URL = $"http://beacon.opensimworld.com/index.php/osgate/beacon/?wk={k}&na={Avatars}&rat=0&r={Regionname}&pos={pos}"
        ' If he says to delete it, we do so.
        If Poke(URL) = -1 Then PropRegionClass.OpensimWorldAPIKey(RegionUUID) = ""

    End Sub

    <CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")>
    Private Function Poke(URL As String) As Integer

        ' Create a New 'HttpWebRequest' Object to the mentioned URL.
        Dim myHttpWebRequest As HttpWebRequest = CType(WebRequest.Create(URL), HttpWebRequest)
        Dim outputData As String = ""
        ' Assign the response object of 'HttpWebRequest' to a 'HttpWebResponse' variable.
        Try
            Using myHttpWebResponse As HttpWebResponse = CType(myHttpWebRequest.GetResponse(), HttpWebResponse)
                'Debug.Print($"{vbCrLf}The HttpHeaders are {vbCrLf}Name {0}", myHttpWebRequest.Headers)
                ' Print the HTML contents of the page to the console.
                Using streamResponse As Stream = myHttpWebResponse.GetResponseStream()
                    Using streamRead As New StreamReader(streamResponse)
                        Dim readBuff As Char() = New Char(255) {}
                        Dim count As Integer = streamRead.Read(readBuff, 0, 256)
                        outputData = New String(readBuff, 0, count)
                        While count > 0
                            count = streamRead.Read(readBuff, 0, 256)
                        End While
                    End Using
                End Using
            End Using
        Catch
        End Try

        If outputData = "OK" Then Return 1
        If outputData = "DISABLE" Then Return -1
        Return 0

    End Function

End Module
