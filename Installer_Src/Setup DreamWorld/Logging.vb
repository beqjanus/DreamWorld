#Region "Copyright AGPL3.0"

' Copyright Outworldz, LLC.
' AGPL3.0  https://opensource.org/licenses/AGPL

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

    Public Sub ShowLog()
        ''' <summary>Shows the log buttons if diags fail</summary>
        Try
            System.Diagnostics.Process.Start(IO.Path.Combine(Settings.CurrentDirectory, "baretail.exe"), """" & IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Outworldz.log") & """")
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try

    End Sub

    Public Sub Viewlog(name As String)
        If name Is Nothing Then Return
        Dim AllLogs As Boolean = False
        Dim path As New List(Of String)

        If name.StartsWith("Region ", StringComparison.InvariantCultureIgnoreCase) Then
            name = Replace(name, "Region ", "", 1, 1)
            name = PropRegionClass.GroupName(PropRegionClass.FindRegionByName(name))
            path.Add("""" & Settings.OpensimBinPath & "Regions\" & name & "\Opensim.log" & """")
        Else
            If name = "All Logs" Then AllLogs = True
            If name = "Robust" Or AllLogs Then path.Add("""" & Settings.OpensimBinPath & "Robust.log" & """")
            If name = "Outworldz" Or AllLogs Then path.Add("""" & IO.Path.Combine(Settings.CurrentDirectory, "Outworldzfiles\Outworldz.log") & """")
            If name = "Error" Or AllLogs Then path.Add("""" & IO.Path.Combine(Settings.CurrentDirectory, "Outworldzfiles\Error.log") & """")
            If name = "UPnP" Or AllLogs Then path.Add("""" & IO.Path.Combine(Settings.CurrentDirectory, "Outworldzfiles\Upnp.log") & """")
            If name = "Icecast" Or AllLogs Then path.Add(" " & """" & IO.Path.Combine(Settings.CurrentDirectory, "Outworldzfiles\Icecast\log\error.log") & """")
            If name = "All Settings" Or AllLogs Then path.Add("""" & IO.Path.Combine(Settings.CurrentDirectory, "Outworldzfiles\Settings.ini") & """")
            If name = "--- Regions ---" Then Return

            If AllLogs Then
                For Each UUID As String In PropRegionClass.RegionUuids
                    name = PropRegionClass.GroupName(UUID)
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
                    BreakPoint.Show(ex.Message)
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
            BreakPoint.Show(ex.Message)
        End Try

    End Sub

End Module
