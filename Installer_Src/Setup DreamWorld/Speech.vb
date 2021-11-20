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
            Dim ProcessString As String = SpeechList.Dequeue
            If Settings.VoiceName = "No Speech" Then Continue While
            Dim SaveWave As Boolean = False
            Speach(ProcessString, SaveWave)
        End While

    End Sub

End Module
