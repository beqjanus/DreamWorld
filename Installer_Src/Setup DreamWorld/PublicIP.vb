#Region "Copyright AGPL3.0"

' Copyright Outworldz, LLC.
' AGPL3.0  https://opensource.org/licenses/AGPL

#End Region

Module PublicIP

    Public Function IP() As String

        Dim ipaddress As String = "127.0.0.1"
        Dim client As New System.Net.WebClient ' downloadclient for web page
        Try
            ipaddress = client.DownloadString("http://api.ipify.org/?r=" + RandomNumber.Random())
        Catch ex As Exception
        Finally
            client.Dispose()
        End Try
        Return ipaddress
    End Function

End Module
