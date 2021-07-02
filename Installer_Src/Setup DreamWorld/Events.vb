Imports System.IO
Imports System.Net
Imports MySql.Data.MySqlClient

Module Events
    Public Sub GetEvents()

        If Not Settings.SearchEnabled Then Return

        Dim Simevents As New Dictionary(Of String, String)
        Dim ctr As Integer = 0
        Try
            Using osconnection = New MySqlConnection(Settings.RobustDBConnection())
                Try
                    osconnection.Open()
                Catch ex As InvalidOperationException
#Disable Warning CA1303 ' Do not pass literals as localized parameters
                    Log(My.Resources.Error_word, "Failed to Connect to Search Database")
#Enable Warning CA1303 ' Do not pass literals as localized parameters
                    Return
                Catch ex As MySqlException
#Disable Warning CA1303 ' Do not pass literals as localized parameters
                    Log(My.Resources.Error_word, "Failed to Connect to Search Database")
#Enable Warning CA1303 ' Do not pass literals as localized parameters
                    Return
                End Try
                DeleteEvents(osconnection)

                Using client As New WebClient()
                    Dim Stream = client.OpenRead(PropDomain() & "/events.txt?r=" & RandomNumber.Random)
                    Using reader = New StreamReader(Stream)
                        While reader.Peek <> -1
                            Dim s = reader.ReadLine

                            ctr += 1
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
            End Using ' connection
#Disable Warning CA1031 ' Do not catch general exception types
        Catch ex As Exception
#Enable Warning CA1031 ' Do not catch general exception types
            ErrorLog(ex.Message)
        End Try

    End Sub


    Public Sub DeleteEvents(Connection As MySqlConnection)

        Dim stm = "delete from events"
        Using cmd As MySqlCommand = New MySqlCommand(stm, Connection)
            Dim rowsdeleted = cmd.ExecuteNonQuery()
            Diagnostics.Debug.Print("Rows: {0}", rowsdeleted.ToString(Globalization.CultureInfo.InvariantCulture))
        End Using

    End Sub


    Public Sub WriteEvent(Connection As MySqlConnection, D As Dictionary(Of String, String))

        If D Is Nothing Then Return

        Dim stm = "insert into events (simname,category,creatoruuid, owneruuid,name, description, dateUTC,duration,covercharge, coveramount,parcelUUID, globalPos,gateway,eventflags) _
                        values (@simname,@category,@creatoruuid,@owneruuid,@name,@description,@dateUTC,@duration,@covercharge,@coveramount,@parcelUUID,@globalPos,@gateway,@eventflags)"

        Using cmd1 As MySqlCommand = New MySqlCommand(stm, Connection)
            cmd1.Parameters.AddWithValue("@simname", D.Item("simname"))
            cmd1.Parameters.AddWithValue("@category", D.Item("category"))
            cmd1.Parameters.AddWithValue("@creatoruuid", D.Item("creatoruuid"))
            cmd1.Parameters.AddWithValue("@owneruuid", D.Item("owneruuid"))
            cmd1.Parameters.AddWithValue("@name", D.Item("name"))
            cmd1.Parameters.AddWithValue("@description", D.Item("description"))
            cmd1.Parameters.AddWithValue("@dateUTC", D.Item("dateUTC"))
            cmd1.Parameters.AddWithValue("@duration", D.Item("duration"))
            cmd1.Parameters.AddWithValue("@covercharge", D.Item("covercharge"))
            cmd1.Parameters.AddWithValue("@parcelUUID", D.Item("parcelUUID"))
            cmd1.Parameters.AddWithValue("@globalPos", D.Item("globalPos"))
            cmd1.Parameters.AddWithValue("@gateway", D.Item("gateway"))
            cmd1.Parameters.AddWithValue("@eventflags", D.Item("eventflags"))
            cmd1.BeginExecuteNonQuery()
        End Using


    End Sub

End Module
