Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Threading

Public Class LogReader

    Public Sub New(RegionUUID As String)

#Disable Warning BC42016 ' Implicit conversion
        Dim start As ParameterizedThreadStart = AddressOf Dowork
#Enable Warning BC42016 ' Implicit conversion
        Dim T = New Thread(start)
        T.SetApartmentState(ApartmentState.STA)
        T.Priority = ThreadPriority.Lowest ' UI gets priority
        T.Start(RegionUUID)

    End Sub

    Public Sub Dowork(RegionUUID As String)

        Dim lastMaxOffset As Long = 0
        While True
            Try
                Dim filename = IO.Path.Combine(Settings.OpensimBinPath, $"Regions/{Group_Name(RegionUUID)}/Opensim.log")
                Using reader = New StreamReader(New FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    'start at the end of the file

                    Sleep(1000)
                    'if the file size has not changed, idle
                    If reader.BaseStream.Length = lastMaxOffset Then Continue While

                    'seek to the last max offset
                    reader.BaseStream.Seek(lastMaxOffset, SeekOrigin.Begin)
                    Dim line As String = ""
                    While reader.BaseStream.Length <> lastMaxOffset
                        ScanIssues(reader.ReadLine(), RegionUUID)
                        'update the last max offset
                        lastMaxOffset = reader.BaseStream.Position
                    End While
                End Using
            Catch
                Sleep(1000)
            End Try
        End While

    End Sub

    Private Sub ScanIssues(line As String, RegionUUID As String)

        If line.Length > 0 Then
            Debug.Print(line)
            Dim pattern = New Regex("Error (.*)", RegexOptions.IgnoreCase)
            Dim match As Match = pattern.Match(line)
            If match.Success Then
                Logger(Region_Name(RegionUUID), line, "Opensim")
            End If
            Sleep(1)
        End If
    End Sub

End Class
