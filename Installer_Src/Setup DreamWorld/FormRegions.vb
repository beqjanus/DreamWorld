#Region "Copyright AGPL3.0"

' Copyright Outworldz, LLC.
' AGPL3.0  https://opensource.org/licenses/AGPL

#End Region

Option Explicit On

Imports System.Speech.Synthesis
Imports System.Text.RegularExpressions

Public Class FormRegions
#Disable Warning CA2213
    Private ReadOnly RegionForm As New FormRegion
    Private ReadOnly Synth As New SpeechSynthesizer()
    Private initted As Boolean
#Enable Warning CA2213

#Region "ScreenSize"

    Private ReadOnly Handler As New EventHandler(AddressOf Resize_page)
    Private _screenPosition As ClassScreenpos

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

    Private Sub AddRegion_Click(sender As Object, e As EventArgs) Handles AddRegion.Click

        RegionForm.Init("")
        RegionForm.Activate()
        RegionForm.Visible = True
        RegionForm.Select()
        RegionForm.BringToFront()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles NormalizeButton1.Click

        ''' TODO If Opensim is running do not continue

        Dim result As MsgBoxResult = MsgBox(My.Resources.This_Moves, MsgBoxStyle.YesNo Or MsgBoxStyle.MsgBoxSetForeground)
        If result = vbYes Then

            Dim chosen = ChooseRegion(False) ' all regions, running or not

            ' Check for illegal stuff
            Dim RegionUUID As String = FindRegionByName(chosen)
            Dim X = Coord_X(RegionUUID)
            Dim Y = Coord_Y(RegionUUID)
            Dim Err As Boolean = False
            Dim Failed As String
            Dim DeltaX = 1000 - X
            Dim DeltaY = 1000 - Y
            For Each UUID As String In RegionUuids()

                If X + DeltaX <= 0 Then
                    Err = True
                    Failed = Region_Name(UUID)
                End If
                If Y + DeltaY <= 0 Then
                    Err = True
                    Failed = Region_Name(UUID)
                End If
            Next

            If (Err) Then
                MsgBox(My.Resources.Cannot_Normalize)
                Return
            End If

            For Each UUID As String In RegionUuids()
                Coord_X(UUID) = Coord_X(UUID) + DeltaX
                Coord_Y(UUID) = Coord_Y(UUID) + DeltaY
                WriteRegionObject(Group_Name(UUID), Region_Name(UUID))
            Next
            PropChangedRegionSettings = True
        End If
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click

        StartMySQL()
        MysqlInterface.DeregisterRegions(False)

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles MakeSpeechButton.Click

        Dim folder = (IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Apache\htdocs\TTS"))
        DeleteFolder(folder)
        Sleep(10)
        MakeFolder(folder)
        MakeSpeechButton.Text = "Busy"
        MakeSpeech()
        While SpeechBusyFlag
            Sleep(100)
        End While
        MakeSpeechButton.Text = "Make Speech (Wav + Mp3)"

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles WelcomeBox1.SelectedIndexChanged

        Dim value As String = TryCast(WelcomeBox1.SelectedItem, String)
        Settings.WelcomeRegion = value

        Debug.Print("Selected " & value)

    End Sub

    Private Sub ConciergeCheckbox_CheckedChanged(sender As Object, e As EventArgs) Handles ConciergeCheckbox.CheckedChanged
        Settings.Concierge = ConciergeCheckbox.Checked
    End Sub

    Private Sub Form1_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed

        While Synth.State = SynthesizerState.Speaking
            Sleep(100)
        End While
        Synth.Dispose()
        RegionForm.Dispose()
        Settings.SaveSettings()
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click

        Dim f = IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Apache\htdocs\TTS")
        System.Diagnostics.Process.Start("explorer.exe", $"/open, {f}")

    End Sub

    Private Sub Loaded(sender As Object, e As EventArgs) Handles Me.Load

        AddRegion.Text = Global.Outworldz.My.Resources.Add_Region_word
        Button1.Text = Global.Outworldz.My.Resources.ClearReg

        ToolStripMenuItem30.Text = Global.Outworldz.My.Resources.Help_word
        GroupBox2.Text = Global.Outworldz.My.Resources.Default_Region_word
        Label1.Text = Global.Outworldz.My.Resources.EditRegion
        Label2.Text = Global.Outworldz.My.Resources.New_User_Home
        MenuStrip2.Text = Global.Outworldz.My.Resources._0
        NormalizeButton1.Text = Global.Outworldz.My.Resources.NormalizeRegions
        RegionBox.Items.AddRange(New Object() {Global.Outworldz.My.Resources.Choose_Region_word})
        RegionButton.Text = Global.Outworldz.My.Resources.Configger
        TextBox1.Text = $"Each line of text is saved to a wave and mp3 file.{vbCrLf}"
        TextBox1.Text += $"The default voice selection is used.{vbCrLf}"
        TextBox1.Text += $"M: Any line the begins with the letter M and a colon is spoken in a Male voice.{vbCrLf}"
        TextBox1.Text += $"F: A line the begins with the letter F and a colon is spoken in a Female voice.{vbCrLf}"
        Text = Global.Outworldz.My.Resources.Region_word
        ToolStripMenuItem30.Image = Global.Outworldz.My.Resources.question_and_answer
        ToolStripMenuItem30.Text = Global.Outworldz.My.Resources.Help_word
        ConciergeCheckbox.Text = Global.Outworldz.My.Resources.Announce_visitors
        ConciergeCheckbox.Checked = Settings.Concierge

        X.Name = Global.Outworldz.My.Resources.X
        Y.Name = Global.Outworldz.My.Resources.Y
        Z.Name = Global.Outworldz.My.Resources.Z

        LoadWelcomeBox()
        LoadRegionBox()

        X.Text = Settings.HomeVectorX
        Y.Text = Settings.HomeVectorY
        Z.Text = Settings.HomeVectorZ

        ConciergeCheckbox.Checked = Settings.Concierge

        For Each voice In Synth.GetInstalledVoices()
            SpeechBox.Items.Add(voice.VoiceInfo.Name)
        Next
        SpeechBox.Items.Add("No Speech")

        ' set the speech box to the saved voice
        Dim Index = SpeechBox.FindString(Settings.VoiceName)
        SpeechBox.SelectedIndex = Index

        HelpOnce("Regions")
        SetScreen()
        initted = True

    End Sub

    Private Sub LoadRegionBox()
        ' All region load
        RegionBox.Items.Clear()

        Dim L = RegionUuids()
        L.Sort()

        For Each RegionUUID As String In L
            RegionBox.Items.Add(Region_Name(RegionUUID))
        Next

    End Sub

    Private Sub LoadWelcomeBox()

        ' Default welcome region load
        WelcomeBox1.Items.Clear()
        Dim L As New List(Of String)
        For Each RegionUUID As String In RegionUuids()
            L.Add(Region_Name(RegionUUID))
        Next
        If L.Count > 0 Then
            L.Sort()
        End If

        For Each RegionName As String In L
            WelcomeBox1.Items.Add(RegionName)
        Next

        Dim s = WelcomeBox1.FindString(Settings.WelcomeRegion)
        If s > -1 Then
            WelcomeBox1.SelectedIndex = s
        Else
            MsgBox(My.Resources.Choose_Welcome, MsgBoxStyle.Information Or MsgBoxStyle.MsgBoxSetForeground, Global.Outworldz.My.Resources.Choose_Region_word)
            Dim chosen = ChooseRegion(False)
            Dim Index = WelcomeBox1.FindString(chosen)
            WelcomeBox1.SelectedIndex = Index
        End If

    End Sub

    Private Sub MakeSpeech()

        ExpireLogsByAge()

        Dim arrKeywords As String() = Split(TextBox1.Text, vbCrLf)
        Using S As New ChatToSpeech
            For Each l In arrKeywords
                S.Speach(l, True, "file")
            Next
        End Using

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click

        Dim f = IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Apache\htdocs\TTS")
        System.Diagnostics.Process.Start("explorer.exe", $"/open, {f}")

    End Sub

    Private Sub RegionBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RegionBox.SelectedIndexChanged

        Dim value As String = TryCast(RegionBox.SelectedItem, String)
#Disable Warning CA2000 ' Dispose objects before losing scope
        Dim RegionForm As New FormRegion
#Enable Warning CA2000 ' Dispose objects before losing scope
        RegionForm.Init(value)
        RegionForm.Activate()
        RegionForm.Visible = True
        RegionForm.Select()
        RegionForm.BringToFront()

    End Sub

    Private Sub RegionButton1_Click(sender As Object, e As EventArgs) Handles RegionButton.Click

        Dim X As Integer = 300
        Dim Y As Integer = 200
        Dim counter As Integer = 0

        Dim L = RegionUuids()
        L.Sort()

        For Each RegionUUID As String In L

            Dim RegionName = Region_Name(RegionUUID)
#Disable Warning CA2000 ' Dispose objects before losing scope
            Dim RegionForm As New FormRegion
#Enable Warning CA2000 ' Dispose objects before losing scope
            RegionForm.Init(RegionName)
            RegionForm.Activate()
            RegionForm.Visible = True
            RegionForm.Select()
            RegionForm.BringToFront()

            Application.DoEvents()
            counter += 1
            Y += 100
            X += 100

        Next

    End Sub

    Private Sub RegionsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RegionsToolStripMenuItem.Click
        HelpManual("Regions")
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
                S.Speach($"This is {selected}. I will speak the region name and visitor name when I am selected.", False)
            End Using
        Catch ex As Exception
            BreakPoint.Dump(ex)
        End Try

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles X.TextChanged
        Dim digitsOnly = New Regex("[^\d]")
        X.Text = digitsOnly.Replace(X.Text, "")
        Settings.HomeVectorX = X.Text
    End Sub

    Private Sub TextToSpeechToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TextToSpeechToolStripMenuItem.Click
        HelpManual("Text2Speech")
    End Sub

    Private Sub ToolStripMenuItem30_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem30.Click
        'HelpManual("Regions")
    End Sub

    Private Sub Y_TextChanged(sender As Object, e As EventArgs) Handles Y.TextChanged
        Dim digitsOnly = New Regex("[^\d]")
        Y.Text = digitsOnly.Replace(Y.Text, "")
        Settings.HomeVectorY = Y.Text
    End Sub

    Private Sub Z_TextChanged(sender As Object, e As EventArgs) Handles Z.TextChanged
        Dim digitsOnly = New Regex("[^\d]")
        Z.Text = digitsOnly.Replace(Z.Text, "")
        Settings.HomeVectorZ = Z.Text
    End Sub

End Class
