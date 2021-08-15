Imports System.Speech.Synthesis

Public Module Speech

    Dim WithEvents Spk As New SpeechSynthesizer()
    Dim B As Integer
    Private Sub EventHandler() Handles Spk.SpeakCompleted
        B = 0
    End Sub
    Public Function SpeechBusy() As Integer

        Return B

    End Function

    ''' <summary>
    ''' Speaks the chosen voice (if any) on the server
    ''' </summary>
    ''' <param name="texttospeak">String to speak</param>
    ''' <param name="SaveWave">True: return a path to a wave file, False: speak the text</param>
    ''' <returns>path to wav file</returns>
    Public Function Speach(texttospeak As String, SaveWave As Boolean) As String

        Dim pathinfo As String = ""

        While B > 0
            Sleep(1000)
            B -= 1
        End While

        Try
            Spk.SelectVoice(Settings.VoiceName)

            If SaveWave Then
                pathinfo = "http://" & Settings.PublicIP & ":" & Convert.ToString(Settings.ApachePort, Globalization.CultureInfo.InvariantCulture) & "/" & "TTS"
                Dim fname = Random() + ".wav"
                pathinfo = IO.Path.Combine(pathinfo, fname)
                Dim filepath = IO.Path.Combine(Settings.CurrentDirectory, $"Outworldzfiles\Apache\htdocs\TTS")
                FileIO.FileSystem.CreateDirectory(filepath)
                filepath = IO.Path.Combine(filepath, fname)
                Spk.SetOutputToWaveFile(filepath)
            End If

            If B <= 0 Then
                B = 30 ' max 30 seconds to speak
            End If
            Spk.SpeakAsync(texttospeak)
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try

        Return pathinfo

    End Function




End Module
