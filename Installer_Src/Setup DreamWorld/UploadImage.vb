#Region "Copyright"

' Copyright 2014 Fred Beckhusen for outworldz.com https://opensource.org/licenses/AGPL

'Permission Is hereby granted, free Of charge, to any person obtaining a copy of this software
' And associated documentation files (the "Software"), to deal in the Software without restriction,
'including without limitation the rights To use, copy, modify, merge, publish, distribute, sublicense,
'And/Or sell copies Of the Software, And To permit persons To whom the Software Is furnished To
'Do so, subject To the following conditions:

'The above copyright notice And this permission notice shall be included In all copies Or '
'substantial portions Of the Software.

'THE SOFTWARE Is PROVIDED "AS IS", WITHOUT WARRANTY Of ANY KIND, EXPRESS Or IMPLIED,
' INCLUDING BUT Not LIMITED To THE WARRANTIES Of MERCHANTABILITY, FITNESS For A PARTICULAR
'PURPOSE And NONINFRINGEMENT.In NO Event SHALL THE AUTHORS Or COPYRIGHT HOLDERS BE LIABLE
'For ANY CLAIM, DAMAGES Or OTHER LIABILITY, WHETHER In AN ACTION Of CONTRACT, TORT Or
'OTHERWISE, ARISING FROM, OUT Of Or In CONNECTION With THE SOFTWARE Or THE USE Or OTHER
'DEALINGS IN THE SOFTWARE.Imports System

#End Region

Imports System.IO
Imports System.Net

Public Class UploadImage

    'Private Delegate Sub UploadStateChange(ByVal Data As String, ByVal Info As UploadInfo)

#Region "Public Methods"

    Shared Sub UploadComplete(ByVal Data As String)
        ' Your Upload Success Routine Goes here
        If Data <> "1" Then
            Form1.Log(My.Resources.Error_word, "Upload Failed. " & Data)
        End If

    End Sub

    Shared Sub UploadError(ByVal Data As String)
        ' Your Upload failure Routine Goes here
        Form1.ErrorLog("Upload Error:" + Data)
    End Sub

    Public Sub PostContentUploadFile()

        Try
            Dim URL = New Uri("https://outworldz.com/cgi/uploadphoto.plx")

            Dim File = Form1.PropMyFolder & "\OutworldzFiles\Photo.png"
            Dim params As New Specialized.NameValueCollection From {
                {"MachineID", Settings.MachineID()},
                {"DnsName", Settings.PublicIP}
            }

            Dim req As Net.HttpWebRequest = CType(HttpWebRequest.Create(URL), HttpWebRequest)
            req.Method = "POST"
            req.KeepAlive = True
            req.ReadWriteTimeout = System.Threading.Timeout.Infinite
            req.Credentials = System.Net.CredentialCache.DefaultCredentials

            Dim ar As IAsyncResult = req.BeginGetRequestStream(AddressOf RequestStreamAvailable,
                New HttpRequestState(req, params, File))
#Disable Warning CA1031
        Catch ex As exception
#Enable Warning CA1031

            Form1.Log(My.Resources.Error_word, ex.Message)
        End Try

    End Sub

#End Region

#Region "Private Methods"

    Private Sub RequestStreamAvailable(ByVal ar As IAsyncResult)
        Dim r_State As HttpRequestState = TryCast(ar.AsyncState, HttpRequestState)
        Dim boundary As String = StrDup(20, "-"c) & Date.Now.ToString("yyyyMMdd-hhmm", Globalization.CultureInfo.InvariantCulture)
        r_State.Request.ContentType = "multipart/form-data; boundary=" & boundary
        Debug.Print("multipart/form-data; boundary=" & boundary)

        Dim reqStream As Stream
        Try
            reqStream = r_State.Request.EndGetRequestStream(ar)
#Disable Warning CA1031
        Catch ex As exception
#Enable Warning CA1031

            UploadError(ex.Message)
            Return
        End Try

        Try
            Dim sw As StreamWriter = New StreamWriter(reqStream)

            ' blank line
            sw.WriteLine()
            Debug.Print("")

            For Each key As String In r_State.Params.Keys
                sw.WriteLine("--" & boundary)
                Debug.Print("--" & boundary)

                sw.WriteLine(String.Format(Globalization.CultureInfo.InvariantCulture, "Content-Disposition: form-data; name=""{0}""", key))
                Debug.Print(String.Format(Globalization.CultureInfo.InvariantCulture, "Content-Disposition: form-data; name=""{0}""", key))
                sw.WriteLine()
                Debug.Print("")
                sw.WriteLine(WebUtility.UrlEncode(r_State.Params(key)))
                Debug.Print(WebUtility.UrlEncode(r_State.Params(key)))
            Next

            sw.WriteLine("--" & boundary)
            Debug.Print("--" & boundary)

            sw.WriteLine(String.Format(Globalization.CultureInfo.InvariantCulture, "Content-Disposition: form-data; name=""{0}""", "FILE1"))
            Debug.Print(String.Format(Globalization.CultureInfo.InvariantCulture, "Content-Disposition: form-data; name=""{0}""", "FILE1"))

            sw.Write(String.Format(Globalization.CultureInfo.InvariantCulture, "filename=""{0}""", WebUtility.UrlEncode(IO.Path.GetFileName(r_State.FileName))))
            Debug.Print(String.Format(Globalization.CultureInfo.InvariantCulture, "filename=""{0}""", WebUtility.UrlEncode(IO.Path.GetFileName(r_State.FileName))))

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
            fileStream = Nothing

            sw.BaseStream.Flush()
            sw.Write(vbNewLine & "--" & boundary & "--" & vbNewLine)
            Debug.Print(vbNewLine & "--" & boundary & "--" & vbNewLine)

            sw.Flush() : sw.Close()
#Disable Warning CA1031
        Catch ex As Exception
#Enable Warning CA1031
        End Try

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
        Else
            Call UploadError(sData)
        End If

    End Sub

#End Region

#Region "Private Classes"

    Private Class HttpRequestState

#Region "Public Fields"

        Public FileName As String
        Public Params As Specialized.NameValueCollection
        Public Request As HttpWebRequest

#End Region

#Region "Public Constructors"

        Public Sub New(ByRef _req As HttpWebRequest, ByVal _param As Specialized.NameValueCollection, ByVal _file As String)
            Me.Request = _req
            Me.Params = _param
            Me.FileName = _file
        End Sub

#End Region

    End Class

#End Region

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
