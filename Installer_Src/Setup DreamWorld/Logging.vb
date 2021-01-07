Imports System.IO

Module Logging

    Public Sub ErrorLog(message As String)
        If Debugger.IsAttached Then
            BreakPoint.Show(message)
        End If
        Logger("Error", message, "Error")
    End Sub

    Public Sub Log(category As String, message As String)

        ''' <summary>Log(string) to Outworldz.log</summary>
        ''' <param name="message"></param>
        Logger(category, message, "Outworldz")

    End Sub

    Public Sub Logger(category As String, message As String, file As String)
        Try
            Using outputFile As New StreamWriter(IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\" & file & ".log"), True)
                outputFile.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", Globalization.CultureInfo.InvariantCulture) & ":" & category & ":" & message)
            End Using
        Catch ex As Exception
        End Try
    End Sub

End Module
