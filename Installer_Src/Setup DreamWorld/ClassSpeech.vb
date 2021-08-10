Imports System.Speech.Synthesis

Public Class Speech

    Dim s1 As SpeechSynthesizer = New SpeechSynthesizer()

    Public Function SpeachTest(texttospeak As String, SaveWave As Boolean) As String

        Dim pathinfo As String = ""
#Disable Warning CA1304 ' Specify CultureInfo
        For Each voice In s1.GetInstalledVoices()
#Enable Warning CA1304 ' Specify CultureInfo
            s1.SelectVoice(voice.VoiceInfo.Name)
            Console.WriteLine(s1.Volume)
            Console.WriteLine(voice.VoiceInfo.Name)

            pathinfo = "http://" & Settings.PublicIP & ":" & Convert.ToString(Settings.ApachePort, Globalization.CultureInfo.InvariantCulture) & "/" & "TTS"
            Dim fname = Random() + ".wav"
            pathinfo = IO.Path.Combine(pathinfo, fname)

            Dim filepath = IO.Path.Combine(Settings.CurrentDirectory, $"Outworldzfiles\Apache\htdocs\TTS")
            FileIO.FileSystem.CreateDirectory(filepath)
            filepath = IO.Path.Combine(filepath, fname)
            If (SaveWave) Then s1.SetOutputToWaveFile(filepath)
            s1.Speak(voice.VoiceInfo.Name & "" & texttospeak)
        Next

        Return pathinfo

    End Function
End Class
