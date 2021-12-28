#Region "Copyright AGPL3.0"

' Copyright Outworldz, LLC.
' AGPL3.0  https://opensource.org/licenses/AGPL

#End Region

Imports System.IO

Module Logging

    Public Sub ErrorLog(message As String)

        Logger("Error", "--------------------------", "Error")
        Logger("Error", message, "Error")
        If Debugger.IsAttached Then
            BreakPoint.Print(message)
        End If

        ' Create a StackTrace that captures
        'filename, line number, And column
        'information for the current thread.
        Dim st = New StackTrace(True)
        Dim i As Integer

        For i = 0 To st.FrameCount - 1 Step 1
            'Note that high up the call stack, there Is only one stack frame.
            Dim sf As StackFrame = st.GetFrame(i)
            Logger("StackFrame", sf.GetFileLineNumber().ToString(Globalization.CultureInfo.InvariantCulture) & ":" & sf.GetMethod().ToString, "Error")
        Next

    End Sub

    Public Sub Log(category As String, message As String)

        ''' <summary>Log(string) to Outworldz.log</summary>
        ''' <param name="message"></param>
        Logger(category, message, "Outworldz")

    End Sub

    Public Sub Logger(category As String, message As String, file As String)
        Dim s = $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", Globalization.CultureInfo.InvariantCulture)}:{category}:{message}"
        Debug.Print(s)
        Try
            Using outputFile As New StreamWriter(IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Logs\" & file & ".log"), True)
                outputFile.WriteLine(s)
            End Using
        Catch ex As Exception
        End Try
    End Sub

    Public Sub ShowLog()
        ''' <summary>Shows the log buttons if diags fail</summary>
        Try
            System.Diagnostics.Process.Start(IO.Path.Combine(Settings.CurrentDirectory, "baretail.exe"), """" & IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Logs\Outworldz.log") & """")
        Catch ex As Exception
            BreakPoint.Dump(ex)
        End Try

    End Sub

    Public Sub Viewlog(name As String)
        If name Is Nothing Then Return
        Dim AllLogs As Boolean = False
        Dim path As New List(Of String)

        If name.StartsWith("Region ", StringComparison.OrdinalIgnoreCase) Then
            name = Replace(name, "Region ", "", 1, 1)
            name = Group_Name(FindRegionByName(name))
            path.Add("""" & Settings.OpensimBinPath & "Regions\" & name & "\Opensim.log" & """")
        Else
            If name = "All Logs" Then AllLogs = True
            If name = "Robust" Or AllLogs Then path.Add("""" & Settings.OpensimBinPath & "Robust.log" & """")
            If name = "Outworldz" Or AllLogs Then path.Add("""" & IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Logs\Outworldz.log") & """")
            If name = "Error" Or AllLogs Then path.Add("""" & IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Logs\Error.log") & """")
            If name = "UPnP" Or AllLogs Then path.Add("""" & IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Logs\Upnp.log") & """")
            If name = "Icecast" Or AllLogs Then path.Add(" " & """" & IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Icecast\log\error.log") & """")
            If name = "All Settings" Or AllLogs Then path.Add("""" & IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Settings.ini") & """")
            If name = "--- Regions ---" Then Return

            If AllLogs Then
                For Each UUID As String In RegionUuids()
                    name = Group_Name(UUID)
                    path.Add("""" & Settings.OpensimBinPath & "Regions\" & name & "\Opensim.log" & """")
                    Application.DoEvents()
                Next
            End If

            If name = "MySQL" Or AllLogs Then
                Dim MysqlLog As String = IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\mysql\data")
                Dim files As Array
                Try
                    files = Directory.GetFiles(MysqlLog, "*.err", SearchOption.TopDirectoryOnly)

                    For Each FileName As String In files
                        path.Add("""" & FileName & """")
                    Next
                Catch ex As Exception
                    BreakPoint.Dump(ex)
                End Try
            End If
        End If
        ' Filter distinct elements, and convert back into list.
        Dim result As List(Of String) = path.Distinct().ToList

        Dim logs As String = ""
        For Each item In result
            Log("View", item)
            logs = logs & " " & item
        Next

        Try
            System.Diagnostics.Process.Start(IO.Path.Combine(Settings.CurrentDirectory, "baretail.exe"), logs)
        Catch ex As Exception
            BreakPoint.Dump(ex)
        End Try

    End Sub

End Module
