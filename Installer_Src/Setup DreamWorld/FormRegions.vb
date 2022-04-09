#Region "Copyright AGPL3.0"

' Copyright Outworldz, LLC.
' AGPL3.0  https://opensource.org/licenses/AGPL

#End Region

Option Explicit On

Imports System.Text.RegularExpressions

Public Class FormRegions

    Private initted As Boolean

#Disable Warning CA2213
    Private RegionForm As New FormRegion
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

#Region "Buttons"

    Private Sub AddRegion_Click(sender As Object, e As EventArgs) Handles Button_AddRegion.Click

        Try
            RegionForm.Dispose()
            RegionForm = New FormRegion
            RegionForm.Init("")
            RegionForm.Activate()
            RegionForm.Visible = True
            RegionForm.Select()
            RegionForm.BringToFront()
        Catch
        End Try

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button_Normalize.Click

        If PropOpensimIsRunning Then
            TextPrint("Opensim is Running!")
            Return
        End If

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

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button_Clear.Click

        StartMySQL()
        MysqlInterface.DeregisterRegions(False)

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles WelcomeBox1.SelectedIndexChanged

        Dim value As String = TryCast(WelcomeBox1.SelectedItem, String)
        Settings.WelcomeRegion = value

        Debug.Print("Selected " & value)

    End Sub

    Private Sub ConciergeCheckbox_CheckedChanged(sender As Object, e As EventArgs) Handles CheckboxConcierge.CheckedChanged

        Settings.Concierge = CheckboxConcierge.Checked

    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs)

        Dim f = IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Apache\htdocs\TTS\Audio")
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

    Private Sub RegionButton1_Click(sender As Object, e As EventArgs) Handles Button_Region.Click

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

    Private Sub ToolStripMenuItem30_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem30.Click
        HelpManual("Regions")
    End Sub

#End Region

#Region "Startup"

    Private Sub Form1_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed

        RegionForm.Dispose()
        Settings.SaveSettings()

    End Sub

    Private Sub Loaded(sender As Object, e As EventArgs) Handles Me.Load

        Button_AddRegion.Text = Global.Outworldz.My.Resources.Add_Region_word
        Button_Clear.Text = Global.Outworldz.My.Resources.ClearReg
        Button_Normalize.Text = Global.Outworldz.My.Resources.NormalizeRegions
        Button_Region.Text = Global.Outworldz.My.Resources.Configger
        CheckboxConcierge.Checked = Settings.Concierge
        CheckboxConcierge.Checked = Settings.Concierge
        CheckboxConcierge.Text = Global.Outworldz.My.Resources.Announce_visitors
        GroupBoxChat.Text = Global.Outworldz.My.Resources.Chat_Channel_word
        GroupBoxConcierge.Text = Global.Outworldz.My.Resources.Concierge_word
        GroupBoxRegion.Text = Global.Outworldz.My.Resources.Default_Region_word
        Label_whisper_distance.Text = Global.Outworldz.My.Resources.Whisper_distance
        LabelEditRegion.Text = Global.Outworldz.My.Resources.EditRegion
        LabelNewUser.Text = Global.Outworldz.My.Resources.New_User_Home
        labelSay.Text = Global.Outworldz.My.Resources.Say_distance
        LabelShout.Text = Global.Outworldz.My.Resources.Shout_distance
        MenuStrip2.Text = Global.Outworldz.My.Resources._0
        RegionBox.Items.AddRange(New Object() {Global.Outworldz.My.Resources.Choose_Region_word})
        TextBox_Whisper_distance.Text = Settings.WhisperDistance
        TextBox_Say_Distance.Text = Settings.SayDistance
        TextBox_Shout_Distance.Text = Settings.ShoutDistance
        Text = Global.Outworldz.My.Resources.Region_word
        TextBoxX.Name = Global.Outworldz.My.Resources.X
        TextBoxX.Text = Settings.HomeVectorX
        TextBoxY.Name = Global.Outworldz.My.Resources.Y
        TextBoxY.Text = Settings.HomeVectorY
        TextBoxZ.Name = Global.Outworldz.My.Resources.Z
        TextBoxZ.Text = Settings.HomeVectorZ
        ToolStripMenuItem30.Image = Global.Outworldz.My.Resources.question_and_answer
        ToolStripMenuItem30.Text = Global.Outworldz.My.Resources.Help_word
        ToolStripMenuItem30.Text = Global.Outworldz.My.Resources.Help_word

        LoadWelcomeBox()
        LoadRegionBox()

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

#End Region

#Region "TextBoxes"

    Private Sub Button1_Click_2(sender As Object, e As EventArgs) Handles ClearFarmButton.Click

        Dim chosen = ChooseRegion(True) ' all regions, running or not
        If chosen.Length > 0 Then
            Dim RegionUUID = FindRegionByName(chosen)
            Dim File = IO.Path.Combine(Settings.OpensimBinPath, "SFCleanup.txt")
            Dim fileReader = My.Computer.FileSystem.ReadAllText(File, System.Text.Encoding.ASCII)
            Dim Commands As String() = fileReader.Split("\n".ToCharArray())

            For Each line In Commands
                RPC_Region_Command(RegionUUID, line)
            Next
        End If

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBoxX.TextChanged
        Dim digitsOnly = New Regex("[^\d]")
        TextBoxX.Text = digitsOnly.Replace(TextBoxX.Text, "")
        Settings.HomeVectorX = TextBoxX.Text
    End Sub

    Private Sub TextBox1_TextChanged_1(sender As Object, e As EventArgs) Handles TextBox_Say_Distance.TextChanged

        Dim digitsOnly = New Regex("[^\d]")
        TextBox_Say_Distance.Text = digitsOnly.Replace(TextBox_Say_Distance.Text, "")
        Settings.SayDistance = TextBox_Say_Distance.Text

    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox_Shout_Distance.TextChanged

        Dim digitsOnly = New Regex("[^\d]")
        TextBox_Shout_Distance.Text = digitsOnly.Replace(TextBox_Shout_Distance.Text, "")
        Settings.ShoutDistance = TextBox_Shout_Distance.Text

    End Sub

    Private Sub Whisper_distance_TextChanged(sender As Object, e As EventArgs) Handles TextBox_Whisper_distance.TextChanged

        Dim digitsOnly = New Regex("[^\d]")
        TextBox_Whisper_distance.Text = digitsOnly.Replace(TextBox_Whisper_distance.Text, "")
        Settings.WhisperDistance = TextBox_Whisper_distance.Text

    End Sub

    Private Sub Y_TextChanged(sender As Object, e As EventArgs) Handles TextBoxY.TextChanged
        Dim digitsOnly = New Regex("[^\d]")
        TextBoxY.Text = digitsOnly.Replace(TextBoxY.Text, "")
        Settings.HomeVectorY = TextBoxY.Text
    End Sub

    Private Sub Z_TextChanged(sender As Object, e As EventArgs) Handles TextBoxZ.TextChanged
        Dim digitsOnly = New Regex("[^\d]")
        TextBoxZ.Text = digitsOnly.Replace(TextBoxZ.Text, "")
        Settings.HomeVectorZ = TextBoxZ.Text
    End Sub

#End Region

End Class
