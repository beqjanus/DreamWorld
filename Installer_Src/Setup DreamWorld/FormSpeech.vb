Imports System.Speech.Synthesis

Public Class FormSpeech

#Region "ScreenSize"

    Private ReadOnly Handler As New EventHandler(AddressOf Resize_page)
    Private ReadOnly Synth As New SpeechSynthesizer()
    Private _screenPosition As ClassScreenpos
    Private initted As Boolean

    Public Property ScreenPosition As ClassScreenpos
        Get
            Return _screenPosition
        End Get
        Set(value As ClassScreenpos)
            _screenPosition = value
        End Set
    End Property

    'The following detects  the location of the form in screen coordinates
    Private Sub Resize_page(ByVal sender As Object, ByVal e As System.EventArgs)
        'Me.Text = "Form screen position = " & Me.Location.ToString
        ScreenPosition.SaveXY(Me.Left, Me.Top)
    End Sub

    Private Sub SetScreen()

        ScreenPosition = New ClassScreenpos(Me.Name)
        AddHandler ResizeEnd, Handler
        Dim xy As List(Of Integer) = ScreenPosition.GetXY()
        Me.Left = xy.Item(0)
        Me.Top = xy.Item(1)

    End Sub

#End Region

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles MakeSpeechButton.Click

        Dim folder = (IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Apache\htdocs\TTS\Audio"))
        MakeFolder(folder)
        MakeSpeechButton.Text = My.Resources.Busy_word
        MakeSpeech()
        While SpeechBusyFlag
            Sleep(100)
        End While
        MakeSpeechButton.Text = My.Resources.Make_Speech

    End Sub

    Private Sub Form1_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed

        While Synth.State = SynthesizerState.Speaking
            Sleep(100)
        End While
        Synth.Dispose()
        Settings.SaveSettings()
    End Sub

    Private Sub Loaded(sender As Object, e As EventArgs) Handles Me.Load

        APILabel.Text = My.Resources.APIKey
        CacheFolderLabel.Text = My.Resources.ViewCacheFolder
        CacheSizeLabel.Text = My.Resources.CacheSizeInHours
        GroupBoxSpeech.Text = My.Resources.Text2Speech
        SpeakButton.Text = My.Resources.Speak

        ' must be in this order
        TextBox1.Text = $"{My.Resources.Each_Line}{vbCrLf}"
        TextBox1.Text += $"{My.Resources.Male_Voice}{vbCrLf}"
        TextBox1.Text += $"{My.Resources.Female_Voice}{vbCrLf}"
        TextBox1.Text += $"{My.Resources.The_default_voice}{vbCrLf}"

        TextBox2.Text = Settings.TTSHours

#Disable Warning CA1304
        For Each voice In Synth.GetInstalledVoices()
#Enable Warning CA1304
            SpeechBox.Items.Add(voice.VoiceInfo.Name)
        Next
        SpeechBox.Items.Add("No Speech")

        ' set the speech box to the saved voice
        Dim Index = SpeechBox.FindString(Settings.VoiceName)
        SpeechBox.SelectedIndex = Index
        APIKeyTextBox.Text = Settings.APIKey

        HelpOnce("Text2Speech")
        SetScreen()
        initted = True

    End Sub

    Private Sub MakeSpeech()

        ExpireLogsByAge()

        Dim arrKeywords As String() = Split(TextBox1.Text, vbCrLf)
        Using S As New ChatToSpeech
            For Each l In arrKeywords
                Dim T = GetMd5Hash(l)
                Dim Par = New SpeechParameters With {
                    .TTS = l,
                    .FileName = "TTS_" & T,
                    .Voice = Settings.VoiceName,
                    .SaveWave = True
                }
                S.Speach(Par)
            Next
        End Using

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click

        Dim f = IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Apache\htdocs\TTS\Audio")
        System.Diagnostics.Process.Start("explorer.exe", $"/open, {f}")

    End Sub

    Private Sub SpeakButton_Click(sender As Object, e As EventArgs) Handles SpeakButton.Click

        ExpireLogsByAge()

        Dim arrKeywords As String() = Split(TextBox1.Text, vbCrLf)
        Using S As New ChatToSpeech
            For Each l In arrKeywords

                Dim Par = New SpeechParameters With {
                    .TTS = l,
                    .Voice = Settings.VoiceName
                }

                S.Speach(Par) ' speak it out loud
            Next
        End Using

    End Sub

    Private Sub SpeechBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles SpeechBox.SelectedIndexChanged

        If Not initted Then Return

        Dim selected = SpeechBox.SelectedItem.ToString
        Settings.VoiceName = selected
        Settings.SaveSettings()
        If selected = "No Speech" Then Return
        Try
            Synth.SelectVoice(selected)
            Using S As New ChatToSpeech

                Dim Par = New SpeechParameters With {
                    .TTS = $"This is {selected}. I will speak the region name and visitor name when I am selected.",
                    .Voice = Settings.VoiceName
                }

                S.Speach(Par)
            End Using
        Catch ex As Exception
            BreakPoint.Dump(ex)
        End Try

    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles APIKeyTextBox.TextChanged

        If Not initted Then Return
        Settings.APIKey = APIKeyTextBox.Text
        Settings.SaveSettings()

    End Sub

    Private Sub TextBox2_TextChanged_1(sender As Object, e As EventArgs) Handles TextBox2.TextChanged

        If Not initted Then Return
        Settings.TTSHours = CDbl("0" + TextBox2.Text)

    End Sub

    Private Sub ToolStripMenuItem30_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem30.Click

        HelpManual("Text2Speech")

    End Sub

End Class