#Region "Copyright"

' Copyright 2014 Fred Beckhusen for www.Outworldz.com https://opensource.org/licenses/AGPL

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
Imports System.Threading
Imports MySql.Data.MySqlClient

Public Class NetServer

#Region "Private Fields"

    Private Shared blnFlag As Boolean
    Private Shared singleWebserver As NetServer
    Dim listen As Boolean = True
    Private MyPort As String
    Private PropMyFolder As String
    Dim PropRegionClass As RegionMaker = RegionMaker.Instance()
    Private running As Boolean = False
    Dim Setting As MySettings
    Private WebThread As Thread

#End Region

#Region "Public Properties"

    Public Property PropRegionClass1 As RegionMaker
        Get
            Return PropRegionClass
        End Get
        Set(value As RegionMaker)
            PropRegionClass = value
        End Set
    End Property

#End Region

#Region "Public Methods"

    Public Sub ListenerCallback(ByVal result As IAsyncResult)
        If result Is Nothing Then Return
        Try
            Dim listener As HttpListener = CType(result.AsyncState, HttpListener)
            ' Call EndGetContext to signal the completion of the asynchronous operation.
            Dim context As HttpListenerContext = listener.EndGetContext(result)

            ' The data sent by client
            Dim request As HttpListenerRequest = context.Request

            Dim body As Stream = request.InputStream
            Dim responseString As String = ""
            Using reader As System.IO.StreamReader = New System.IO.StreamReader(body, request.ContentEncoding)

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

                End If

            End Using

            ' Get the response object to send our confirmation.
            Using response As HttpListenerResponse = context.Response
                ' Construct a minimal response string.
                Dim buffer As Byte() = System.Text.Encoding.UTF8.GetBytes(responseString)
                ' Get the response OutputStream and write the response to it.
                response.ContentLength64 = buffer.Length
                ' Identify the content type.
                response.ContentType = "text/html"
                Using output As System.IO.Stream = response.OutputStream
                    output.Write(buffer, 0, buffer.Length)
                    output.Flush()
                End Using

            End Using
        Catch
        End Try
    End Sub

    Public Sub StartServer(pathinfo As String, Settings As MySettings)

        ' stash some globs
        Setting = Settings
        MyPort = CStr(Form1.Settings.DiagnosticPort)
        PropMyFolder = pathinfo

        If running Then Return

        Log("Info", "Starting Diagnostic Webserver")
        WebThread = New Thread(AddressOf Looper)
        Try
            WebThread.SetApartmentState(ApartmentState.STA)
        Catch ex As ArgumentException
            Log(My.Resources.Err, ex.Message)
        Catch ex As ThreadStartException
            Log(My.Resources.Err, ex.Message)
        Catch ex As InvalidOperationException
            Log(My.Resources.Err, ex.Message)
        End Try
        WebThread.Start()
        running = True

    End Sub

    Public Sub StopWebServer()

        Log("Info", "Stopping Webserver")
        listen = False
        Application.DoEvents()
        WebThread.Abort()
        'WebThread.Join()
        Log("Info", "Shutdown Complete")

    End Sub

#End Region

#Region "Internal Methods"

    Friend Shared Function GetWebServer() As NetServer

        If Not blnFlag Then
            singleWebserver = New NetServer
            blnFlag = True
            Return singleWebserver
        Else
            Return singleWebserver
        End If

    End Function

#End Region

#Region "Private Methods"

    Private Shared Function RegionListHTML(Settings As MySettings, PropRegionClass As RegionMaker) As String

        'redirect from http://localhost:8002/bin/data/teleports.htm
        'to http://localhost:8001/teleports.htm
        'Outworldz|Welcome||www.outworldz.com:9000:Welcome|128,128,96|
        '*|Welcome||www.outworldz.com9000Welcome|128,128,96|
        Dim HTML As String

        HTML = "Welcome to |" & Settings.SimName & "||" & CStr(Settings.PublicIP) & ":" & CStr(Settings.HttpPort) & ":" & Settings.WelcomeRegion & "||" & vbCrLf
        Dim ToSort As New List(Of String)

        Using NewSQLConn As New MySqlConnection(Settings.RobustMysqlConnection)
            Diagnostics.Debug.Print("Conn:" & Settings.RobustMysqlConnection)
            Dim UserStmt = "SELECT regionName from REGIONS"
            Try
                NewSQLConn.Open()
            Catch ex As InvalidOperationException
            Catch ex As MySqlException
                Return HTML
            End Try

            Using cmd As New MySqlCommand(UserStmt, NewSQLConn)
                Dim reader As MySqlDataReader = cmd.ExecuteReader()

                While reader.Read()
                    Dim LongName = reader.GetString(0)
                    Dim RegionNumber = PropRegionClass.FindRegionByName(LongName)
                    If RegionNumber >= 0 Then
                        If PropRegionClass.Teleport(RegionNumber) = "True" Then
                            ToSort.Add(LongName)
                        End If
                    End If
                End While
            End Using
        End Using

        ' Acquire keys And sort them.
        ToSort.Sort()

        For Each S As String In ToSort
            HTML = HTML & "*|" & S & "||" & Settings.PublicIP & ":" & Settings.HttpPort & ":" & S & "||" & vbCrLf
        Next

        Return HTML

    End Function

    Private Sub Log(category As String, message As String)
        Debug.Print(message)
        Try
            Using outputFile As New StreamWriter(PropMyFolder & "\Outworldzfiles\Http.log", True)
                outputFile.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", Form1.Invarient) & ":" & category & ":" & message)
            End Using
        Catch ex As UnauthorizedAccessException
        Catch ex As ArgumentNullException
        Catch ex As ArgumentException
        Catch ex As DirectoryNotFoundException
        Catch ex As PathTooLongException
        Catch ex As IOException
        Catch ex As System.Security.SecurityException
        Catch ex As ObjectDisposedException
        End Try
    End Sub

    Private Sub Looper()

        listen = True

        Using listener As New System.Net.HttpListener()
            listener.Prefixes.Clear()
            listener.Prefixes.Add("http://+:" & MyPort & "/")

            Try
                listener.Start() ' Throws Exception
            Catch ex As HttpListenerException
                Log(My.Resources.Err, ex.Message)
                Return
            Catch ex As ObjectDisposedException
                Log(My.Resources.Err, ex.Message)
                Return
            End Try

            Dim result As IAsyncResult
            While listen
                result = listener.BeginGetContext((AddressOf ListenerCallback), listener)
                result.AsyncWaitHandle.WaitOne()
            End While

        End Using

        running = False
        Log("Info", "Webserver thread shutdown")

    End Sub

#End Region

End Class
