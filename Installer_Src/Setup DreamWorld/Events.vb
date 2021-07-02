Imports System.IO
Imports System.Net
Imports MySql.Data.MySqlClient

Module Events
    Public Sub GetEvents()

        If Not Settings.SearchEnabled Then Return

        Dim Simevents As New Dictionary(Of String, String)
        Dim ctr As Integer = 0
        Try
            Using osconnection = New MySqlConnection(Settings.OSSearchConnectionString())
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
            End Using ' os connection
#Disable Warning CA1031 ' Do not catch general exception types
        Catch ex As Exception
#Enable Warning CA1031 ' Do not catch general exception types
            ErrorLog(ex.Message)
        End Try

    End Sub

End Module
