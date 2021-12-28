Imports System.Threading
Imports System.Speech.Synthesis
Imports System.Speech.Synthesis.VoiceGender

Module speech
    Public SpeechBusyFlag As Boolean
    Public SpeechList As New Queue(Of String)

    Public Sub Chat2Speech()

        If SpeechList.Count = 0 Then Return
        Dim WebThread = New Thread(AddressOf SpeakArrival)
        WebThread.SetApartmentState(ApartmentState.STA)
        WebThread.Start()

    End Sub

    Private Sub SpeakArrival()

        While SpeechList.Count > 0
            Using S As New ChatToSpeech
                If Settings.VoiceName = "No Speech" Then Return
                Dim ProcessString As String = SpeechList.Dequeue
                S.Speach(ProcessString, True)
            End Using
        End While

    End Sub

End Module

Public Class ChatToSpeech

    Implements IDisposable

    Public WithEvents Speaker As New SpeechSynthesizer

    ReadOnly Interlock As New Object
    Private Counter As Integer
    Private disposedValue As Boolean

    Public Shared Function SpeechBusy() As Boolean

        Return SpeechBusyFlag

    End Function

    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code. Put cleanup code in 'Dispose(disposing As Boolean)' method
        Dispose(disposing:=True)
        GC.SuppressFinalize(Me)
    End Sub

    ''' <summary>
    ''' Speaks the chosen voice (if any) on the server
    ''' </summary>
    ''' <param name="texttospeak">String to speak</param>
    ''' <param name="SaveWave">True: return a path to a wave file, False: speak the text</param>
    ''' <returns>path to wave file</returns>
    Public Function Speach(texttospeak As String, SaveWave As Boolean, Optional FileName As String = "") As String

        Dim pathinfo As String = ""
        If Settings.VoiceName = "No Speech" Then Return pathinfo
        If texttospeak.Length = 0 Then Return pathinfo

        SyncLock Interlock

            While SpeechBusyFlag
                Sleep(1000)
            End While

            Dim fname As String = ""
            Try
                If SaveWave Then
                    Dim filepath = ""
                    DeleteOldWave(IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Apache\htdocs\TTS"))
                    pathinfo = "http://" & Settings.PublicIP & ":" & Convert.ToString(Settings.ApachePort, Globalization.CultureInfo.InvariantCulture) & "/" & "TTS"

                    If FileName.Length = 0 Then
                        Dim g = Guid.NewGuid()
                        fname = g.ToString + ".wav"
                    Else
                        fname = $"{Counter:D10}.wav"
                        Counter += 1
                    End If

                    fname = fname.Replace("<", "")
                    fname = fname.Replace("?", "")
                    fname = fname.Replace("""", "")
                    fname = fname.Replace("/", "")
                    fname = fname.Replace("\", "")
                    fname = fname.Replace("|", "")
                    fname = fname.Replace(">", "")
                    fname = fname.Replace("*", "")

                    pathinfo = IO.Path.Combine(pathinfo, fname)
                    filepath = IO.Path.Combine(Settings.CurrentDirectory, $"Outworldzfiles\Apache\htdocs\TTS")
                    FileIO.FileSystem.CreateDirectory(filepath)
                    filepath = IO.Path.Combine(filepath, fname)

                    Debug.Print(filepath)
                    Speaker.SetOutputToWaveFile(filepath)
                End If

                If texttospeak.StartsWith("M:", System.StringComparison.OrdinalIgnoreCase) Then
                    Speaker.SelectVoiceByHints(Male, System.Speech.Synthesis.VoiceAge.Adult, 0, System.Globalization.CultureInfo.CurrentCulture)
                    texttospeak = texttospeak.Replace("M:", "")
                ElseIf texttospeak.StartsWith("F:", System.StringComparison.OrdinalIgnoreCase) Then
                    Speaker.SelectVoiceByHints(Female, System.Speech.Synthesis.VoiceAge.Adult, 0, System.Globalization.CultureInfo.CurrentCulture)
                    texttospeak = texttospeak.Replace("F:", "")
                Else
                    Speaker.SelectVoice(Settings.VoiceName)
                End If

                SpeechBusyFlag = True
                Speaker.SpeakAsync(texttospeak)
            Catch ex As Exception
                BreakPoint.Dump(ex)
            End Try

            While SpeechBusyFlag
                Sleep(10)
            End While

            If SaveWave Then ConvertWavMP3(fname, True)

        End SyncLock

        pathinfo.Replace("\", "/")
        Return pathinfo

    End Function

    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects)
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override finalizer
            ' TODO: set large fields to null
            disposedValue = True
        End If
    End Sub

    Private Sub ConvertWavMP3(fileName As String, waitFlag As Boolean)
        Dim psi = New System.Diagnostics.ProcessStartInfo With {
            .FileName = IO.Path.Combine(Settings.CurrentDirectory, "Outworldzfiles\Opensim\lame.exe"),
            .Arguments = $"-b 128 --resample 44.1 {fileName} {fileName.Replace(".wav", ".mp3")}",
            .WorkingDirectory = IO.Path.Combine(Settings.CurrentDirectory, "Outworldzfiles\Apache\htdocs\TTS"),
            .WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden
        }
        Dim p As New Process()
        Try
            p = Process.Start(psi)
        Catch ex As Exception
            BreakPoint.Print(ex.Message)
        End Try

        If waitFlag Then
            p.WaitForExit()
        End If
        If p IsNot Nothing Then p.Dispose()

    End Sub

    Private Sub DeleteOldWave(LogPath As String)

        Try
            FileIO.FileSystem.CreateDirectory(LogPath)
            Dim directory As New System.IO.DirectoryInfo(LogPath)
            ' get each file's last modified date
            For Each File As System.IO.FileInfo In directory.GetFiles()
                ' get  file's last modified date
                Dim strLastModified As Date = System.IO.File.GetLastWriteTime(File.FullName)
                Dim Datedifference = DateDiff("h", strLastModified, Date.Now)
                If Datedifference > 1 Then
                    DeleteFile(File.FullName)
                End If
            Next
        Catch
        End Try

    End Sub

    Private Sub EventHandler() Handles Speaker.SpeakCompleted

        SpeechBusyFlag = False

    End Sub

End Class
