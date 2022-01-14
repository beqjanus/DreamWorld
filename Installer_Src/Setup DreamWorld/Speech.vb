Imports System.Threading
Imports System.Speech.Synthesis
Imports System.Speech.Synthesis.VoiceGender
Imports System.Text.RegularExpressions
Imports System.Security.Cryptography
Imports System.Text

Module Speech

    Public SpeechBusyFlag As Boolean

    Public SpeechCounter As Integer

    Public SpeechList As New Queue(Of String)

    Dim S As New ChatToSpeech

    Public Sub Chat2Speech()

        If SpeechList.Count = 0 Then Return
        Dim WebThread = New Thread(AddressOf SpeakArrival)
        WebThread.SetApartmentState(ApartmentState.STA)
        WebThread.Start()

    End Sub

    Public Sub DeleteOldWave(LogPath As String)

        Try
            IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Apache\htdocs\TTS\Audio")
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

    Public Function GetMd5Hash(input As String) As String
        ' Create a new instance of the MD5 object.
        Dim md5Hasher As MD5 = MD5.Create()

        ' Convert the input string to a byte array and compute the hash.
        Dim data As Byte() = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input))

        ' Create a new String builder to collect the bytes
        ' and create a string.
        Dim sBuilder As New StringBuilder()

        ' Loop through each byte of the hashed data
        ' and format each one as a hexadecimal string.
        Dim i As Integer
        For i = 0 To data.Length - 1
            sBuilder.Append(data(i).ToString("x2"))
        Next i

        ' Return the hexadecimal string.
        Return sBuilder.ToString()

    End Function

    Public Function Text2Speech(POST As String) As String

        Dim P = GetParam(POST, "Password")
        Dim M = Settings.MachineID
        If M <> P Then Return $"Bad Password {P}"
        Dim OutLoud = GetParam(POST, "Speak")
        Dim Save2File As Boolean
        If OutLoud.Length = 0 Then
            Save2File = True
        End If

        Dim Par = New SpeechParameters With {
            .Voice = GetParam(POST, "Voice"),
            .TTS = GetParam(POST, "TTS"),
            .Sex = GetParam(POST, "Sex"),
            .SaveWave = Save2File
        }

        Dim Filename = S.Speach(Par)
        Return Filename

    End Function

    Private Function GetParam(POST As String, Param As String) As String

        ' Get Name=Value pairs
        Try
            Dim pattern = New Regex($"{Param}=(.*?)&|{Param}=(.*)", RegexOptions.IgnoreCase)
            Dim match As Match = pattern.Match(POST)
            If match.Success Then
                If match.Groups(1).Value.Length > 0 Then Return Uri.UnescapeDataString(match.Groups(1).Value)
                Return Uri.UnescapeDataString(match.Groups(2).Value)
            End If
        Catch
        End Try

        Return ""

    End Function

    Private Sub SpeakArrival()

        While SpeechList.Count > 0
            If Settings.VoiceName = "No Speech" Then Return
            Dim ProcessString As String = SpeechList.Dequeue

            Dim Par = New SpeechParameters With {
                .TTS = ProcessString,
                .Voice = Settings.VoiceName
            }

            S.Speach(Par)
        End While

    End Sub

    Public Class ChatToSpeech

        Implements IDisposable

        Public WithEvents Speaker As New SpeechSynthesizer

        ReadOnly Interlock As New Object

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
        Public Function Speach(Params As SpeechParameters) As String

            Dim HttpPathInfo As String = ""
            If Settings.VoiceName = "No Speech" Then Return ""
            If Params.TTS.Length = 0 Then Return ""

            SyncLock Interlock

                While SpeechBusyFlag
                    Sleep(1000)
                End While

                Dim fname As String = ""
                Try

                    Dim DiskFilepath = ""
                    HttpPathInfo = $"http://{Settings.PublicIP}:{Convert.ToString(Settings.ApachePort, Globalization.CultureInfo.InvariantCulture)}/TTS/Data"

                    ' Numbers if from form, else MD5 for security

                    If Params.FileName IsNot Nothing Then
                        fname = Params.FileName + ".wav"
                    Else
                        fname = $"TTS_{SpeechCounter:D10}.wav"
                        SpeechCounter += 1
                    End If

                    ' Make path to Cache
                    HttpPathInfo = IO.Path.Combine(HttpPathInfo, fname)
                    HttpPathInfo = HttpPathInfo.Replace("\", "/")
                    HttpPathInfo = HttpPathInfo.Replace(".wav", ".mp3")

                    DiskFilepath = IO.Path.Combine(Settings.CurrentDirectory, $"Outworldzfiles\Apache\htdocs\TTS\Audio")
                    If Not FileIO.FileSystem.FileExists(DiskFilepath) Then
                        FileIO.FileSystem.CreateDirectory(DiskFilepath)
                    End If

                    DiskFilepath = IO.Path.Combine(DiskFilepath, fname)
                    Debug.Print(DiskFilepath)

                    ' check if it is in cache
                    Dim Mp3FilePath = DiskFilepath.Replace(".wav", ".mp3")
                    If FileIO.FileSystem.FileExists(Mp3FilePath) Then
                        Return HttpPathInfo
                    End If

                    If Params.SaveWave Then
                        Speaker.SetOutputToWaveFile(DiskFilepath)
                    Else
                        Speaker.SetOutputToDefaultAudioDevice()
                    End If

                    'M: text , F: text will choose the first  Male or female voice
                    If CBool(Params.TTS.StartsWith("M:", System.StringComparison.OrdinalIgnoreCase)) Then
                        Speaker.SelectVoiceByHints(Male, System.Speech.Synthesis.VoiceAge.Adult, 0, System.Globalization.CultureInfo.CurrentCulture)
                        Params.TTS = Params.TTS.Replace("M:", "")

                    ElseIf CBool(Params.TTS.StartsWith("F:", System.StringComparison.OrdinalIgnoreCase)) Then
                        Speaker.SelectVoiceByHints(Female, System.Speech.Synthesis.VoiceAge.Adult, 0, System.Globalization.CultureInfo.CurrentCulture)
                        Params.TTS = Params.TTS.Replace("F:", "")

                    ElseIf Params.Sex = "M" Then
                        Speaker.SelectVoiceByHints(Male, System.Speech.Synthesis.VoiceAge.Adult, 0, System.Globalization.CultureInfo.CurrentCulture)

                    ElseIf Params.Sex = "F" Then
                        Speaker.SelectVoiceByHints(Female, System.Speech.Synthesis.VoiceAge.Adult, 0, System.Globalization.CultureInfo.CurrentCulture)

                    ElseIf Params.TTS.Contains(":") Then
                        SpecificVoice(Params)

                    ElseIf Params.Voice.Length > 0 Then
                        ' Code can call for a specific voice
#Disable Warning CA1304
                        For Each voice In Speaker.GetInstalledVoices()
                            If voice.VoiceInfo.Name.Contains(Params.Voice) Then
                                Speaker.SelectVoice(voice.VoiceInfo.Name)
                                Exit For
                            End If
                        Next
#Enable Warning CA1304

                        ' Lines can begin with a subset of a voice name, such as "Zira:'
                    Else
                        Speaker.SelectVoice(Settings.VoiceName)
                    End If

                    SpeechBusyFlag = True
                    Speaker.SpeakAsync(Params.TTS)
                Catch ex As Exception
                    BreakPoint.Dump(ex)
                End Try

                While SpeechBusyFlag
                    Sleep(10)
                End While

                If Params.FileName IsNot Nothing Then ConvertWavMP3(fname, True)

            End SyncLock

            Return HttpPathInfo

        End Function

        ''' <summary>
        ''' Line with a subset of the voice name and a colon will choose that voice
        ''' Zira: This line will be spoken using Zira's voice
        ''' </summary>

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

        Private Sub EventHandler() Handles Speaker.SpeakCompleted

            SpeechBusyFlag = False

        End Sub

        Private Sub SpecificVoice(ByVal Param As SpeechParameters)

            Dim pattern = New Regex("(.*?):", RegexOptions.IgnoreCase)
            Dim match As Match = pattern.Match(Param.TTS)
            If match.Success Then
                Dim Str = match.Groups(1).Value
                If Str.Length > 0 Then

#Disable Warning CA1304
                    For Each voice In Speaker.GetInstalledVoices()
#Enable Warning CA1304
                        If voice.VoiceInfo.Name.Contains(Str) Then
                            Speaker.SelectVoice(voice.VoiceInfo.Name)
                            Param.TTS = Param.TTS.Replace(Str & ":", "")
                            Exit For
                        End If
                    Next
                End If
            End If

        End Sub

    End Class

    Public Class SpeechParameters
        Public FileName As String
        Public SaveWave As Boolean
        Public Sex As String
        Public TTS As String
        Public Voice As String
    End Class

End Module
