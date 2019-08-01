Imports System.IO
Imports System.Net
Imports System.Threading
Imports MySql.Data.MySqlClient

Public Class NetServer
    Private running As Boolean = False

    Dim listen As Boolean = True

    Private WebThread As Thread
    Private Shared blnFlag As Boolean
    Private Shared singleWebserver As NetServer
    Private PropMyFolder As String

    Private MyPort As String

    Dim PropRegionClass As RegionMaker = RegionMaker.Instance()

    Dim Setting As MySettings

    Public Property PropRegionClass1 As RegionMaker
        Get
            Return PropRegionClass
        End Get
        Set(value As RegionMaker)
            PropRegionClass = value
        End Set
    End Property

    Public Sub StartServer(pathinfo As String, PropMySetting As MySettings)

        ' stash some globs
        Setting = PropMySetting
        MyPort = PropMySetting.DiagnosticPort
        PropMyFolder = pathinfo

        If running Then Return

        Try
            Log("Info", "Starting Diagnostic Webserver")
            WebThread = New Thread(AddressOf Looper)
            WebThread.SetApartmentState(ApartmentState.STA)
            WebThread.Start()
            running = True
        Catch ex As Exception
            Log("Error", ex.Message)
        End Try

    End Sub

    Private Sub Looper()

        listen = True

        Dim listener = New System.Net.HttpListener()
        listener.Prefixes.Clear()
        'listener.Prefixes.Add("http://" + LocalAddress.ToString(Form1.usa) + ":" + MyPort.ToString(Form1.usa) + "/")
        listener.Prefixes.Add("http://+:" + MyPort + "/")

        Try
            listener.Start() ' Throws Exception
        Catch ex As Exception
            If ex.Message.Contains("Access is denied") Then
                Log("Error", ex.Message)
                Return
            Else
                Throw
            End If
        End Try

        Dim result As IAsyncResult
        While listen
            result = listener.BeginGetContext((AddressOf ListenerCallback), listener)
            result.AsyncWaitHandle.WaitOne()
        End While

        listener.Close()
        running = False
        Log("Info", "Webserver thread shutdown")

    End Sub

    Public Sub StopWebServer()

        Log("Info", "Stopping Webserver")
        listen = False
        Application.DoEvents()
        WebThread.Abort()
        'WebThread.Join()
        Log("Info", "Shutdown Complete")

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

    Private Sub Log(category As String, message As String)
        Debug.Print(message)
        Try
            Using outputFile As New StreamWriter(PropMyFolder & "\Outworldzfiles\Http.log", True)
                outputFile.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", Form1.Usa) + ":" & category & ":" & message)
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

            Dim body As System.IO.Stream = request.InputStream
            Dim encoding As System.Text.Encoding = request.ContentEncoding
            Dim reader As System.IO.StreamReader = New System.IO.StreamReader(body, encoding)

            Dim responseString As String = ""
            ' process the input

            Dim Uri = request.Url.OriginalString
            Dim lcUri = LCase(Uri)

            If lcUri.Contains("teleports.htm") Then
                responseString = RegionListHTML(Setting, PropRegionClass1)
            Else
                If (request.HasEntityBody) Then
                    Dim POST As String = reader.ReadToEnd()
                    responseString = PropRegionClass1.ParsePost(POST, Setting)
                Else
                    responseString = PropRegionClass1.ParsePost(Uri, Setting)
                End If

                body.Close()
            End If


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
        Catch
        End Try
    End Sub


    Private Function RegionListHTML(PropMySetting As MySettings, PropRegionClass As RegionMaker) As String

        'redirect from http://localhost:8002/bin/data/teleports.htm
        'to http://localhost:8001/teleports.htm
        'Outworldz|Welcome||www.outworldz.com:9000:Welcome|128,128,96|
        '*|Welcome||www.outworldz.com9000Welcome|128,128,96|
        Dim HTML As String

        HTML = "Welcome to |" + PropMySetting.SimName + "||" + PropMySetting.PublicIP + ":" + PropMySetting.HttpPort + ":" + PropMySetting.WelcomeRegion + "||" + vbCrLf

        Dim NewSQLConn As New MySqlConnection(PropMySetting.RobustConnStr)
        Diagnostics.Debug.Print("Conn:" & PropMySetting.RobustConnStr)
        Dim UserStmt = "SELECT regionName from REGIONS"

        Dim ToSort As New List(Of String)
        Try
            NewSQLConn.Open()
            Dim cmd As MySqlCommand = New MySqlCommand(UserStmt, NewSQLConn)
            Dim reader As MySqlDataReader = cmd.ExecuteReader()

            While reader.Read()
                Dim LongName = reader.GetString(0)

                Diagnostics.Debug.Print("regionname {0}:", LongName)

                Dim RegionNumber = PropRegionClass.FindRegionByName(LongName)
                If RegionNumber >= 0 Then
                    If LCase(PropRegionClass.Teleport(RegionNumber)) = "true" Then
                        ToSort.Add(LongName)
                    End If
                End If
            End While
        Catch ex As Exception
            Console.WriteLine("Error: " & ex.ToString())
        Finally
            NewSQLConn.Close()
        End Try

        ' Acquire keys And sort them.
        ToSort.Sort()

        For Each S As String In ToSort
            HTML = HTML + "*|" & S & "||" & PropMySetting.PublicIP & ":" & PropMySetting.HttpPort & ":" & S & "||" + vbCrLf
        Next

        Return HTML

    End Function
End Class