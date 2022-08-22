Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Threading

Public Class SeekObject
    Public RegionUUID As String
    Public text As String
End Class

Public Class WaitForOAR2Load

    ReadOnly o As New SeekObject
    Dim CTR As Integer
    Dim lastMaxOffset As Long

    Public Sub Scan(RegionUUID As String, text As String)

        Dim startctr As Integer
        o.text = text
        o.RegionUUID = RegionUUID

        Dim filename = IO.Path.Combine(Settings.OpensimBinPath, $"Regions/{Group_Name(o.RegionUUID)}/Opensim.log")
        Const limit = 180000

        ' wait 3 minutes for the file to be created
        While Not File.Exists(filename) And startctr < limit
            Sleep(10)
            startctr += 1
        End While
        If startctr > limit Then Return ' abort

        ' lo file exists
        Using reader = New StreamReader(New FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            'start at the end of the file
            reader.BaseStream.Seek(0, SeekOrigin.End)
            lastMaxOffset = reader.BaseStream.Length
        End Using

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
            Dim filename = IO.Path.Combine(Settings.OpensimBinPath, $"Regions/{Group_Name(RegionUUID)}/Opensim.log")
            Try
                If File.Exists(filename) Then
                    Using reader = New StreamReader(New FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))

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

                    End Using
                Else
                    ' not exists
                    Return
                End If
            Catch
                Return
            End Try

            CTR += 1
            Sleep(1000)
        End While

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
