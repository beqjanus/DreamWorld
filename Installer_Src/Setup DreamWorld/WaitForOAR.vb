Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Threading

Public Class SeekObject
    Public RegionUUID As String
    Public text As String
End Class

Public Class WaitForOAR2Load

    ReadOnly o As New SeekObject
    Private CTR As Integer
    Private Filename As String
    Private lastMaxOffset As Long
    Private reader As StreamReader

    Public Sub New(RegionUUID As String, text As String)

        o.text = text
        o.RegionUUID = RegionUUID

        Filename = IO.Path.Combine(Settings.OpensimBinPath, $"Regions/{Group_Name(o.RegionUUID)}/Opensim.log")
        If Not File.Exists(Filename) Then
            ' Create or overwrite the file.
            Dim fs As FileStream = File.Create(Filename)

            ' Add text to the file.
            Dim info As Byte() = New System.Text.UTF8Encoding(True).GetBytes("-----START-------")
            fs.Write(info, 0, info.Length)
            fs.Close()
        End If

        reader = New StreamReader(New FileStream(Filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
        'start at the end of the file
        reader.BaseStream.Seek(0, SeekOrigin.End)
        lastMaxOffset = reader.BaseStream.Length

    End Sub

    Public Sub Scan()

        ' file exists

#Disable Warning BC42016 ' Implicit conversion
        Dim start As ParameterizedThreadStart = AddressOf SeekOar
#Enable Warning BC42016 ' Implicit conversion
        Dim T = New Thread(start)
        T.SetApartmentState(ApartmentState.STA)
        T.Priority = ThreadPriority.Lowest ' UI gets priority
        T.Start(o)

    End Sub

    Public Sub SeekOar(o As SeekObject)

        Dim RegionUUID As String = o.RegionUUID
        Dim text = o.text

        While CTR < 30 * 60 ' 30 minutes to save
            PokeRegionTimer(RegionUUID)
            Try
                'seek to the last max offset
                reader.BaseStream.Seek(lastMaxOffset, SeekOrigin.Begin)
                While reader.BaseStream.Length <> lastMaxOffset
                    If ScanForPattern(reader.ReadLine(), text) Then
                        Return
                    End If
                    'update the last max offset
                    lastMaxOffset = reader.BaseStream.Position
                    Application.DoEvents()
                End While
            Catch
                reader.Close()
                Return
            End Try

            CTR += 1
            Sleep(1000)
        End While
        reader.Close()

    End Sub

    Private Function ScanForPattern(line As String, text As String) As Boolean

        If line.Length > 0 Then
            Debug.Print(line)
            Dim pattern = New Regex(text, RegexOptions.IgnoreCase)
            Dim match = pattern.Match(line)
            If match.Success Then
                Return True
            End If
        End If
        Return False
    End Function

End Class
