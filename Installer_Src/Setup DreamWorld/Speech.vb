Imports System.Threading
Imports System.Speech.Synthesis
Imports System.Speech.Synthesis.VoiceGender

Module ChatToSpeech

    Public WithEvents Speaker As New SpeechSynthesizer
    Public SpeechList As New Queue(Of String)
    ReadOnly Interlock As New Object
    Private Counter As Integer
    Dim SpeechBusyFlag As Boolean

    Public Sub Chat2Speech()

        If SpeechList.Count = 0 Then Return
        Dim WebThread = New Thread(AddressOf SpeakArrival)
        WebThread.SetApartmentState(ApartmentState.STA)
        WebThread.Start()

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

            Try
                If SaveWave Then
                    DeleteOldWave(IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Apache\htdocs\TTS"))
                    pathinfo = "http://" & Settings.PublicIP & ":" & Convert.ToString(Settings.ApachePort, Globalization.CultureInfo.InvariantCulture) & "/" & "TTS"
                    Dim fname As String = ""
                    If FileName.Length = 0 Then
                        Dim out As New Guid
                        Dim g = Guid.NewGuid()
                        fname = g.ToString + ".wav"
                    Else
                        fname = $"{Counter:D5}.wav"
                        Counter += 1
                    End If

                    fname.Replace("<", "")
                    fname.Replace("?", "")
                    fname.Replace("""", "")
                    fname.Replace("/", "")
                    fname.Replace("\", "")
                    fname.Replace("|", "")
                    fname.Replace(">", "")
                    fname.Replace("*", "")

                    pathinfo = IO.Path.Combine(pathinfo, fname)
                    Dim filepath = IO.Path.Combine(Settings.CurrentDirectory, $"Outworldzfiles\Apache\htdocs\TTS")
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
                End If

                SpeechBusyFlag = True
                Speaker.SpeakAsync(texttospeak)
            Catch ex As Exception
                BreakPoint.Show(ex)
            End Try

        End SyncLock
        pathinfo.Replace("\", "/")
        Return pathinfo

    End Function

    Public Function SpeechBusy() As Boolean

        Return SpeechBusyFlag

    End Function

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

    Private Sub SpeakArrival()

        While SpeechList.Count > 0
            Dim ProcessString As String = SpeechList.Dequeue
            If Settings.VoiceName = "No Speech" Then Return
            Speach(ProcessString, True)
        End While

    End Sub

End Module
