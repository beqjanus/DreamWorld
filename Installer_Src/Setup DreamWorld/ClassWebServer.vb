Imports System.IO
Imports System.Net
Imports System.Net.Sockets
Imports System.Text
Imports System.Threading
Imports System.Xml


Public Class NetServer
    Private running As Boolean = False
    Private LocalTCPListener As TcpListener
    Dim listen As Boolean = True
    Private LocalAddress As IPAddress
    Private WebThread As Thread
    Private Shared blnFlag As Boolean
    Private Shared singleWebserver As NetServer
    Private Myfolder As String
    Private IP As String = Nothing
    Private MyPort As Integer
    Dim RegionClass As RegionMaker = RegionMaker.Instance(Form1.MysqlConn)
    Dim Setting As MySettings

    Public Sub StartServer(pathinfo As String, MySetting As MySettings, IP As String, Port As Integer)

        ' stash some globs
        Setting = MySetting
        MyPort = Port
        Myfolder = pathinfo
        LocalAddress = IPAddress.Parse(IP)

        If running Then Return

        Try
            Log("Info:Starting Diagnostic Webserver")
            WebThread = New Thread(AddressOf Looper)
            WebThread.SetApartmentState(ApartmentState.STA)
            WebThread.Start()
            running = True
        Catch ex As Exception
            Log(ex.Message)
        End Try

    End Sub

    Private Sub Looper()

        Log("Info:IP:" + LocalAddress.ToString)
        listen = True

        Dim listener = New System.Net.HttpListener()
        listener.Start()
        Dim prefixes(0) As String

        prefixes(0) = "http://" + LocalAddress.ToString + ":" + MyPort.ToString + "/"

        Try
            For Each s As String In prefixes
                listener.Prefixes.Add(s)
            Next
        Catch
        End Try

        Dim result As IAsyncResult
        While listen
            result = listener.BeginGetContext((AddressOf ListenerCallback), listener)
            result.AsyncWaitHandle.WaitOne()
        End While

        listener.Close()
        running = False

    End Sub

    Public Sub StopWebServer()

        Log("Info:Stopping Webserver")
        listen = False
        Application.DoEvents()
        WebThread.Abort()
        'WebThread.Join()
        Log("Info:Shutdown Complete")

    End Sub

    Friend Shared Function GetWebServer() As NetServer

        If Not blnFlag Then
            singleWebserver = New NetServer
            blnFlag = True
            Return singleWebserver
        Else
            Return singleWebserver
        End If

    End Function

    Public Sub Log(message As String)
        Debug.Print(message)
        Try
            Using outputFile As New StreamWriter(Myfolder & "\Outworldzfiles\Http.log", True)
                outputFile.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + message)
            End Using
        Catch ex As Exception
            Debug.Print(ex.Message)
        End Try
    End Sub

    Public Sub ListenerCallback(ByVal result As IAsyncResult)

        Try
            Dim listener As HttpListener = CType(result.AsyncState, HttpListener)
            ' Call EndGetContext to signal the completion of the asynchronous operation.
            Dim context As HttpListenerContext = listener.EndGetContext(result)

            ' The data sent by client
            Dim request As HttpListenerRequest = context.Request

            ' Get Parameters:
            ' Debug.Print("Client data content type:" + request.ContentType)

            'Dim baseUri = New Uri(request.Url.OriginalString)
            ' Debug.Print("Query String:" + baseUri.Query)

            'Dim qs = request.QueryString

            ' print out all name value pairs
            ' For Each s In qs.AllKeys
            'Debug.Print(s + ":" + qs.Get(s))
            ' Next

            Dim body As System.IO.Stream = request.InputStream
            Dim encoding As System.Text.Encoding = request.ContentEncoding
            Dim reader As System.IO.StreamReader = New System.IO.StreamReader(body, encoding)
            'If (request.ContentType <> "") Then
            'Debug.Print("Client data content type {0}", request.ContentType)
            'End If

            'Debug.Print("Client data content length {0}", request.ContentLength64)
            Dim responseString As String = ""
            ' process the input

            Debug.Print("No client data was sent with the request.")
            Dim Uri = request.Url.OriginalString
            If Uri.Contains("teleports.htm") Then
                responseString = RegionClass.RegionListHTML(Setting)
            Else

                If (request.HasEntityBody) Then
                    Dim POST As String = reader.ReadToEnd()
                    responseString = RegionClass.ParsePost(POST, Setting)
                Else
                    responseString = RegionClass.ParsePost(Uri, Setting)
                End If


                body.Close()
                End If

                ''''''''''''''''''''''''''''''''''''''''''''''

                'Log(responseString)

                ' Get the response object to send our confirmation.
                Dim response As HttpListenerResponse = context.Response

            ' Construct a minimal response string.
            Dim buffer As Byte() = System.Text.Encoding.UTF8.GetBytes(responseString)
            ' Get the response OutputStream and write the response to it.
            response.ContentLength64 = buffer.Length
            ' Identify the content type.
            response.ContentType = "text/html"
            Dim output As System.IO.Stream = response.OutputStream
            output.Write(buffer, 0, buffer.Length)
            ' Properly flush and close the output stream
            output.Flush()
            output.Close()

            'If you are finished with the request, it should be closed also.
            response.Close()

        Catch ex As Exception
            Application.Exit()
        End Try

    End Sub


End Class


