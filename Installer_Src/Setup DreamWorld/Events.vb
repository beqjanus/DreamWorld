Imports System.IO
Imports System.Net
Imports MySql.Data.MySqlClient

Module Events

    Public Sub GetEvents()

        If Not Settings.EventTimerEnabled Then Return

        Try
            Using osconnection = New MySqlConnection(Settings.OSSearchConnectionString())
                osconnection.Open()

                'if enabled, get the events from Outworldz.com
                Dim Simevents As New Dictionary(Of String, String)

                DeleteEvents(osconnection)

                Using client As New WebClient()
                    Dim Stream = client.OpenRead(Form1.PropDomain() & "/events.txt?r=" & RandomNumber.Random)
                    Using reader = New StreamReader(Stream)
                        While reader.Peek <> -1
                            Dim s = reader.ReadLine

                            ' Split line on comma.
                            Dim array As String() = s.Split("|".ToCharArray())
                            Simevents.Clear()
                            ' Loop over each string received.
                            Dim part As String
                            For Each part In array
                                ' Display to console.
                                Dim a As String() = part.Split("^".ToCharArray())
                                If a.Length = 2 Then
                                    a(1) = a(1).Replace("'", "\'")
                                    a(1) = a(1).Replace("`", vbLf)
                                    'Console.WriteLine("{0}:{1}", a(0), a(1))
                                    Simevents.Add(a(0), a(1))
                                End If

                            Next
                            WriteEvent(osconnection, Simevents)
                        End While
                    End Using ' reader

                End Using ' client
            End Using ' osconnection

        Catch ex As Exception

            BreakPoint.Show(ex.Message)
            Form1.ErrorLog(ex.Message)
        End Try

    End Sub

    <CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")>
    Public Sub WriteEvent(Connection As MySqlConnection, D As Dictionary(Of String, String))

        If D Is Nothing Then Return

        Dim stm = "insert into events (simname,category,creatoruuid, owneruuid,name, gateway,description, dateUTC,duration,covercharge, coveramount,parcelUUID, globalPos,eventflags) values (" _
                        & "'" & D.Item("simname") & "'," _
                        & "'" & D.Item("category") & "'," _
                        & "'" & D.Item("creatoruuid") & "'," _
                        & "'" & D.Item("owneruuid") & "'," _
                        & "'" & D.Item("name") & "'," _
                        & "'" & D.Item("gateway") & "'," _
                        & "'" & D.Item("description") & "'," _
                        & "'" & D.Item("dateUTC") & "'," _
                        & "'" & D.Item("duration") & "'," _
                        & "'" & D.Item("covercharge") & "'," _
                        & "'" & D.Item("coveramount") & "'," _
                        & "'" & D.Item("parcelUUID") & "'," _
                        & "'" & D.Item("globalPos") & "'," _
                        & "'" & D.Item("eventflags") & "')"

#Disable Warning CA2100 ' Review SQL queries for security vulnerabilities
        Using cmd As MySqlCommand = New MySqlCommand(stm, Connection)
#Enable Warning CA2100 ' Review SQL queries for security vulnerabilities
            Dim rowsinserted = cmd.ExecuteNonQuery()
            'Diagnostics.Debug.Print("Insert: {0}", CStr(rowsinserted))
        End Using

    End Sub

    Private Sub DeleteEvents(Connection As MySqlConnection)

        Dim stm = "delete from events"
        Using cmd As MySqlCommand = New MySqlCommand(stm, Connection)
            Dim rowsdeleted = cmd.ExecuteNonQuery()
            Diagnostics.Debug.Print("Rows: {0}", rowsdeleted.ToString(Globalization.CultureInfo.InvariantCulture))
        End Using

    End Sub

End Module
