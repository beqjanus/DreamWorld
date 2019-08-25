Imports System.IO
Imports System.Net

Public Class UploadImage

    'Private Delegate Sub UploadStateChange(ByVal Data As String, ByVal Info As UploadInfo)

    Shared Sub UploadError(ByVal Data As String)
        ' Your Upload failure Routine Goes here
        Form1.ErrorLog("Upload Error:" + Data)
    End Sub

    Shared Sub UploadComplete(ByVal Data As String)
        ' Your Upload Success Routine Goes here
        If Data <> "1" Then
            Form1.Log("Error", "Upload Failed. " & Data)
        End If

    End Sub

    Private Class HttpRequestState
        Public Request As HttpWebRequest
        Public Params As Specialized.NameValueCollection
        Public FileName As String

        Public Sub New(ByRef _req As HttpWebRequest, ByVal _param As Specialized.NameValueCollection, ByVal _file As String)
            Me.Request = _req
            Me.Params = _param
            Me.FileName = _file
        End Sub

    End Class

    Private Sub RequestStreamAvailable(ByVal ar As IAsyncResult)
        Dim r_State As HttpRequestState = TryCast(ar.AsyncState, HttpRequestState)
        Dim boundary As String = StrDup(20, "-"c) & Date.Now.ToString("yyyyMMdd-hhmm", Form1.Usa)
        r_State.Request.ContentType = "multipart/form-data; boundary=" & boundary
        Debug.Print("multipart/form-data; boundary=" & boundary)

        Dim reqStream As Stream
        Try
            reqStream = r_State.Request.EndGetRequestStream(ar)
        Catch ex As Exception
            UploadError(ex.Message)
            Return
        End Try

        Dim sw As StreamWriter = New StreamWriter(reqStream)

        ' blank line
        sw.WriteLine()
        Debug.Print("")

        For Each key As String In r_State.Params.Keys

            sw.WriteLine("--" & boundary)
            Debug.Print("--" & boundary)

            sw.WriteLine(String.Format(Form1.Usa, "Content-Disposition: form-data; name=""{0}""", key))
            Debug.Print(String.Format(Form1.Usa, "Content-Disposition: form-data; name=""{0}""", key))
            sw.WriteLine()
            Debug.Print("")
            sw.WriteLine(WebUtility.UrlEncode(r_State.Params(key)))
            Debug.Print(WebUtility.UrlEncode(r_State.Params(key)))
        Next

        sw.WriteLine("--" & boundary)
        Debug.Print("--" & boundary)

        sw.WriteLine(String.Format(Form1.Usa, "Content-Disposition: form-data; name=""{0}""", "FILE1"))
        Debug.Print(String.Format(Form1.Usa, "Content-Disposition: form-data; name=""{0}""", "FILE1"))

        sw.Write(String.Format(Form1.Usa, "filename=""{0}""", WebUtility.UrlEncode(IO.Path.GetFileName(r_State.FileName))))
        Debug.Print(String.Format(Form1.Usa, "filename=""{0}""", WebUtility.UrlEncode(IO.Path.GetFileName(r_State.FileName))))

        sw.WriteLine()
        Debug.Print("")
        sw.WriteLine("Content-Type: application/octet-stream")
        Debug.Print("Content-Type: application/octet-stream")
        sw.WriteLine()
        Debug.Print("")

        Dim fileStream As FileStream = New FileStream(r_State.FileName, FileMode.Open, FileAccess.Read)
        Dim buffer(1024) As Byte, bytesRead As Integer
        sw.Flush()

        Do
            bytesRead = fileStream.Read(buffer, 0, buffer.Length)
            If bytesRead > 0 Then
                sw.BaseStream.Write(buffer, 0, bytesRead)
            End If
        Loop While (bytesRead > 0)
        fileStream.Close()

        sw.BaseStream.Flush()
        sw.Write(vbNewLine & "--" & boundary & "--" & vbNewLine)
        Debug.Print(vbNewLine & "--" & boundary & "--" & vbNewLine)

        sw.Flush() : sw.Close()

        r_State.Request.BeginGetResponse(AddressOf ResponseAvailable, r_State)
    End Sub

    Private Sub ResponseAvailable(ByVal ar As IAsyncResult)
        Dim r_State As HttpRequestState = TryCast(ar.AsyncState, HttpRequestState)
        Dim webResp As HttpWebResponse
        Dim sData As String = String.Empty
        Try
            webResp = CType(r_State.Request.EndGetResponse(ar), HttpWebResponse)
        Catch wex As WebException
            webResp = CType(wex.Response, HttpWebResponse)
        Catch ex As InvalidOperationException
            webResp = Nothing
        Catch ex As ArgumentException
            webResp = Nothing
        End Try

        If webResp IsNot Nothing Then
            Using respReadr As StreamReader = New StreamReader(webResp.GetResponseStream())
                sData = respReadr.ReadToEnd()
            End Using
            If webResp.StatusCode = HttpStatusCode.OK Then
                Call UploadComplete(sData)
            Else
                Call UploadError(sData)
            End If
            webResp.Close()
            webResp = Nothing
        Else
            Call UploadError(sData)
        End If

    End Sub

    Public Sub PostContentUploadFile()

        Try
            Dim URL = New Uri("https://www.outworldz.com/cgi/uploadphoto.plx")
            Dim File = Form1.PropMyFolder & "\OutworldzFiles\Photo.png"
            Dim params As New Specialized.NameValueCollection From {
                {"MachineID", Form1.PropMySetting.MachineID()},
                {"DnsName", Form1.PropMySetting.PublicIP}
            }

            Dim req As Net.HttpWebRequest = CType(HttpWebRequest.Create(URL), HttpWebRequest)
            req.Method = "POST"
            req.KeepAlive = True
            req.ReadWriteTimeout = System.Threading.Timeout.Infinite
            req.Credentials = System.Net.CredentialCache.DefaultCredentials

            Dim ar As IAsyncResult = req.BeginGetRequestStream(AddressOf RequestStreamAvailable,
                New HttpRequestState(req, params, File))
        Catch ex As Exception
            Form1.Log("Error", ex.Message)
        End Try

    End Sub

End Class

'multipart/form-data; boundary=--------------------20190207-0157
'----------------------20190207-0157
'Content-Disposition: form-data; name="MachineID";

'548435328
'----------------------20190207-0157
'Content-Disposition: form-data; name="DnsName";
'
'10.6.1.103
'----------------------20190207-0157
'Content-Disposition: form-data; name="FILE1";
'filename = "Photo.png"
'
'Content-Type: application/ octet - stream
'
'
'----------------------20190207-0157--