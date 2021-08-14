Imports System.Text.RegularExpressions
Imports System.Threading
Module ChatToSpeech

    Public SpeechList As New Queue(Of String)
    Public Sub Chat2Speech()

        If SpeechList.Count = 0 Then Return
        Dim WebThread = New Thread(AddressOf SpeakArrival)
        WebThread.SetApartmentState(ApartmentState.STA)
        WebThread.Start()

    End Sub
    Private Sub SpeakArrival()

        While SpeechList.Count > 0

            Try
                Dim ProcessString As String = SpeechList.Dequeue ' recover the POST 

                If Settings.VoiceName <> "No Speech" Then Continue While

                Dim POST As String = Uri.UnescapeDataString(ProcessString)
                '{0} avatar name, {1} region name, {2} number of avatars
                'http://127.0.0.1:${Const|DiagnosticsPort}/broker/{0}/{1}/{2}"
                Dim Region As String = ""
                Dim Avatar As String = ""
                Dim Num As String = ""
                Dim M As Match = Regex.Match(POST, ".*/broker/(.*?)/(.*?)/(.*?)")
                If M.Groups(1).Success Then
                    Region = M.Groups(1).Value
                End If
                If M.Groups(2).Success Then
                    Avatar = M.Groups(2).Value
                End If
                If M.Groups(3).Success Then
                    Num = M.Groups(3).Value
                End If

                Dim SaveWave As Boolean = False

                Speach($"{Avatar} is in Region {Region}", SaveWave)

            Catch
            End Try

        End While

    End Sub

End Module
