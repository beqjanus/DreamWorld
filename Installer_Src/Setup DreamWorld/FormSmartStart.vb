' TODO New terrain commands
'   set terrain heights <corner> <min> <max> [<x>] [<y>] - Sets the terrain texture heights on corner #<corner> to <min>/<max>, if <x> Or <y> are specified, it will only set it on regions with a matching coordinate. Specify -1 in <x> Or <y> to wildcard that coordinate. Corner # SW = 0, NW = 1, SE = 2, NE = 3.
'   set terrain texture <number> <uuid> [<x>] [<y>] - Sets the terrain <number> to <uuid>, if <x> Or <y> are specified, it will only set it on regions with a matching coordinate. Specify -1 in <x> Or <y> to wild card that coordinate.

Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Threading
Imports System.Xml

Public Class FormSmartStart

    Private ReadOnly _TerrainList As New List(Of Image)
    Private ReadOnly _TerrainName As New List(Of String)
    Private ReadOnly Handler As New EventHandler(AddressOf Resize_page)
    Private _abort As Boolean
    Private _Index As Integer
    Private _initialized As Boolean
    Private _initted As Boolean
    Private _SelectedPlant As String

#Region "ScreenSize"

    'The following detects  the location of the form in screen coordinates
    Private _screenPosition As ScreenPos

    Private Sub Resize_page(ByVal sender As Object, ByVal e As System.EventArgs)
        'Me.Text = "Form screen position = " + Me.Location.ToString
        ScreenPosition.SaveXY(Me.Left, Me.Top)
    End Sub

    Private Sub SetScreen()
        Me.Show()
        ScreenPosition = New ScreenPos(Me.Name)
        AddHandler ResizeEnd, Handler
        Dim xy As List(Of Integer) = ScreenPosition.GetXY()
        Me.Left = xy.Item(0)
        Me.Top = xy.Item(1)
    End Sub

#End Region

#Region "Properties"

    Public Property Abort As Boolean
        Get
            Return _abort
        End Get
        Set(value As Boolean)
            _abort = value
        End Set
    End Property

    Public Property ScreenPosition As ScreenPos
        Get
            Return _screenPosition
        End Get
        Set(value As ScreenPos)
            _screenPosition = value
        End Set
    End Property

#End Region

#Region "Scrolling text box"

    Public Sub ProgressPrint(Value As String)
        Log(My.Resources.Info_word, Value)
        TextBox1.Text += Value & vbCrLf
        If TextBox1.Text.Length > TextBox1.MaxLength - 1000 Then
            TextBox1.Text = Mid(TextBox1.Text, 1000)
        End If
    End Sub

    Private Sub TextBox1_Changed(sender As System.Object, e As EventArgs)
        Dim ln As Integer = TextBox1.Text.Length
        TextBox1.SelectionStart = ln
        TextBox1.ScrollToCaret()
    End Sub

#End Region

#Region "Check boxes"

    Private Sub All_CheckedChanged(sender As Object, e As EventArgs) Handles All.CheckedChanged

        If All.Checked Then
            None.Checked = False
            SetSetting(BeachGrass1)
            SetSetting(Cypress1)
            SetSetting(Cypress2)
            SetSetting(Eelgrass)
            SetSetting(Eucalyptus)
            SetSetting(Fern)
            SetSetting(Grass0)
            SetSetting(Grass1)
            SetSetting(Grass2)
            SetSetting(Grass3)
            SetSetting(Grass4)
            SetSetting(Kelp1)
            SetSetting(Kelp2)
            SetSetting(Oak)
            SetSetting(Palm1)
            SetSetting(Palm2)
            SetSetting(Pine1)
            SetSetting(Pine2)
            SetSetting(Plumeria)
            SetSetting(SeaSword)
            SetSetting(TropicalBush1)
            SetSetting(TropicalBush2)
            SetSetting(Undergrowth1)
            SetSetting(WinterAspen)
            SetSetting(WinterPine1)
            SetSetting(WinterPine2)
        End If

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs)
        Settings.AutoFill = RegionMakerEnableCHeckbox.Checked
    End Sub

    Private Sub ClrSetting(check As CheckBox)
        check.Checked = False
    End Sub

    Private Sub None_CheckedChanged(sender As Object, e As EventArgs) Handles None.CheckedChanged

        If None.Checked Then
            All.Checked = False

            ClrSetting(BeachGrass1)
            ClrSetting(Cypress1)
            ClrSetting(Cypress2)
            ClrSetting(Eelgrass)
            ClrSetting(Eucalyptus)
            ClrSetting(Fern)
            ClrSetting(Grass0)
            ClrSetting(Grass1)
            ClrSetting(Grass2)
            ClrSetting(Grass3)
            ClrSetting(Grass4)
            ClrSetting(Kelp1)
            ClrSetting(Kelp2)
            ClrSetting(Oak)
            ClrSetting(Palm1)
            ClrSetting(Palm2)
            ClrSetting(Pine1)
            ClrSetting(Pine2)
            ClrSetting(Plumeria)
            ClrSetting(SeaSword)
            ClrSetting(TropicalBush1)
            ClrSetting(TropicalBush2)
            ClrSetting(Undergrowth1)
            ClrSetting(WinterAspen)
            ClrSetting(WinterPine1)
            ClrSetting(WinterPine2)
        End If

    End Sub

#End Region

#Region "Photos"

    Private Function GetPic(Photoname As String) As Image

        Dim path = IO.Path.Combine(Settings.OpensimBinPath, "Trees")
        path = IO.Path.Combine(path, Photoname & ".jpg")

        Try
            Using fs As FileStream = New System.IO.FileStream(path, FileMode.Open, FileAccess.Read)
                Dim I As Image = Image.FromStream(fs)
                Return I
            End Using
        Catch
        End Try

        Return My.Resources.NoImage

    End Function

#End Region

#Region "Plants"

    Private Sub BeachGrass_CheckedChanged(sender As Object, e As EventArgs) Handles BeachGrass1.CheckedChanged
        Dim thing As CheckBox = CType(sender, CheckBox)
        PutSetting(thing.Name, thing.Checked)
        PictureBox1.Image = GetPic(thing.Name)
    End Sub

    Private Sub BeachGrass_MouseHoverd(sender As Object, e As EventArgs) Handles BeachGrass1.MouseHover
        Dim thing As CheckBox = CType(sender, CheckBox)
        PictureBox1.Image = GetPic(thing.Name)
    End Sub

    Private Sub Cypress1_CheckedChanged(sender As Object, e As EventArgs) Handles Cypress1.CheckedChanged
        Dim thing As CheckBox = CType(sender, CheckBox)
        PutSetting(thing.Name, thing.Checked)
        PictureBox1.Image = GetPic(thing.Name)
    End Sub

    Private Sub Cypress1_Hover(sender As Object, e As EventArgs) Handles Cypress1.MouseHover
        Dim thing As CheckBox = CType(sender, CheckBox)
        PictureBox1.Image = GetPic(thing.Name)
    End Sub

    Private Sub Cypress2_CheckedChanged(sender As Object, e As EventArgs) Handles Cypress2.CheckedChanged
        Dim thing As CheckBox = CType(sender, CheckBox)
        PutSetting(thing.Name, thing.Checked)
        PictureBox1.Image = GetPic(thing.Name)
    End Sub

    Private Sub Cypress2_Hover(sender As Object, e As EventArgs) Handles Cypress2.MouseHover
        Dim thing As CheckBox = CType(sender, CheckBox)
        PictureBox1.Image = GetPic(thing.Name)
    End Sub

    Private Sub Eelgrass_CheckedChanged(sender As Object, e As EventArgs) Handles Eelgrass.CheckedChanged
        Dim thing As CheckBox = CType(sender, CheckBox)
        PutSetting(thing.Name, thing.Checked)
        PictureBox1.Image = GetPic(thing.Name)
    End Sub

    Private Sub Eelgrass_Hover(sender As Object, e As EventArgs) Handles Eelgrass.MouseHover
        Dim thing As CheckBox = CType(sender, CheckBox)
        PictureBox1.Image = GetPic(thing.Name)
    End Sub

    Private Sub Eucalyptus_CheckedChanged(sender As Object, e As EventArgs) Handles Eucalyptus.CheckedChanged
        Dim thing As CheckBox = CType(sender, CheckBox)
        PutSetting(thing.Name, thing.Checked)
        PictureBox1.Image = GetPic(thing.Name)
    End Sub

    Private Sub Eucalyptus_Hover(sender As Object, e As EventArgs) Handles Eucalyptus.MouseHover
        Dim thing As CheckBox = CType(sender, CheckBox)
        PictureBox1.Image = GetPic(thing.Name)
    End Sub

    Private Sub Fern_CheckedChanged(sender As Object, e As EventArgs) Handles Fern.CheckedChanged
        Dim thing As CheckBox = CType(sender, CheckBox)
        PutSetting(thing.Name, thing.Checked)
        PictureBox1.Image = GetPic(thing.Name)
    End Sub

    Private Sub Fern_Hover(sender As Object, e As EventArgs) Handles Fern.MouseHover
        Dim thing As CheckBox = CType(sender, CheckBox)
        PictureBox1.Image = GetPic(thing.Name)
    End Sub

    Private Sub Grass0_CheckedChanged(sender As Object, e As EventArgs) Handles Grass0.CheckedChanged
        Dim thing As CheckBox = CType(sender, CheckBox)
        PutSetting(thing.Name, thing.Checked)
        PictureBox1.Image = GetPic(thing.Name)
    End Sub

    Private Sub Grass0_Hover(sender As Object, e As EventArgs) Handles Grass0.MouseHover
        Dim thing As CheckBox = CType(sender, CheckBox)
        PictureBox1.Image = GetPic(thing.Name)
    End Sub

    Private Sub Grass1_CheckedChanged(sender As Object, e As EventArgs) Handles Grass1.CheckedChanged
        Dim thing As CheckBox = CType(sender, CheckBox)
        PutSetting(thing.Name, thing.Checked)
        PictureBox1.Image = GetPic(thing.Name)
    End Sub

    Private Sub Grass1_Hover(sender As Object, e As EventArgs) Handles Grass1.MouseHover
        Dim thing As CheckBox = CType(sender, CheckBox)
        PictureBox1.Image = GetPic(thing.Name)
    End Sub

    Private Sub Grass2_CheckedChanged(sender As Object, e As EventArgs) Handles Grass2.CheckedChanged
        Dim thing As CheckBox = CType(sender, CheckBox)
        PutSetting(thing.Name, thing.Checked)
        PictureBox1.Image = GetPic(thing.Name)
    End Sub

    Private Sub Grass2_Hover(sender As Object, e As EventArgs) Handles Grass2.MouseHover
        Dim thing As CheckBox = CType(sender, CheckBox)
        PictureBox1.Image = GetPic(thing.Name)
    End Sub

    Private Sub Grass3_CheckedChanged(sender As Object, e As EventArgs) Handles Grass3.CheckedChanged
        Dim thing As CheckBox = CType(sender, CheckBox)
        PutSetting(thing.Name, thing.Checked)
    End Sub

    Private Sub Grass3_Hover(sender As Object, e As EventArgs) Handles Grass3.MouseHover
        Dim thing As CheckBox = CType(sender, CheckBox)
        PictureBox1.Image = GetPic(thing.Name)
    End Sub

    Private Sub Grass4_CheckedChanged(sender As Object, e As EventArgs) Handles Grass4.CheckedChanged
        Dim thing As CheckBox = CType(sender, CheckBox)
        PutSetting(thing.Name, thing.Checked)
        PictureBox1.Image = GetPic(thing.Name)
    End Sub

    Private Sub Grass4_Hover(sender As Object, e As EventArgs) Handles Grass4.MouseHover
        Dim thing As CheckBox = CType(sender, CheckBox)
        PictureBox1.Image = GetPic(thing.Name)
    End Sub

    Private Sub Kelp1_CheckedChanged(sender As Object, e As EventArgs) Handles Kelp1.CheckedChanged
        Dim thing As CheckBox = CType(sender, CheckBox)
        PutSetting(thing.Name, thing.Checked)
        PictureBox1.Image = GetPic(thing.Name)
    End Sub

    Private Sub Kelp1_Hover(sender As Object, e As EventArgs) Handles Kelp1.MouseHover
        Dim thing As CheckBox = CType(sender, CheckBox)
        PictureBox1.Image = GetPic(thing.Name)
    End Sub

    Private Sub Kelp2_CheckedChanged(sender As Object, e As EventArgs) Handles Kelp2.CheckedChanged
        Dim thing As CheckBox = CType(sender, CheckBox)
        PutSetting(thing.Name, thing.Checked)
        PictureBox1.Image = GetPic(thing.Name)
    End Sub

    Private Sub Kelp2_Hover(sender As Object, e As EventArgs) Handles Kelp2.MouseHover
        Dim thing As CheckBox = CType(sender, CheckBox)
        PictureBox1.Image = GetPic(thing.Name)
    End Sub

    Private Sub Oak_CheckedChanged(sender As Object, e As EventArgs) Handles Oak.CheckedChanged
        Dim thing As CheckBox = CType(sender, CheckBox)
        PutSetting(thing.Name, thing.Checked)
        PictureBox1.Image = GetPic(thing.Name)
    End Sub

    Private Sub Oak_MouseHover(sender As Object, e As EventArgs) Handles Oak.MouseHover
        Dim thing As CheckBox = CType(sender, CheckBox)
        PictureBox1.Image = GetPic(thing.Name)
    End Sub

    Private Sub Palm1_CheckedChanged(sender As Object, e As EventArgs) Handles Palm1.CheckedChanged
        Dim thing As CheckBox = CType(sender, CheckBox)
        PutSetting(thing.Name, thing.Checked)
        PictureBox1.Image = GetPic(thing.Name)
    End Sub

    Private Sub Palm1_MouseHover(sender As Object, e As EventArgs) Handles Palm1.MouseHover
        Dim thing As CheckBox = CType(sender, CheckBox)
        PictureBox1.Image = GetPic(thing.Name)
    End Sub

    Private Sub Palm2_CheckedChanged(sender As Object, e As EventArgs) Handles Palm2.CheckedChanged
        Dim thing As CheckBox = CType(sender, CheckBox)
        PutSetting(thing.Name, thing.Checked)
        PictureBox1.Image = GetPic(thing.Name)
    End Sub

    Private Sub Palm2_MouseHover(sender As Object, e As EventArgs) Handles Palm2.MouseHover
        Dim thing As CheckBox = CType(sender, CheckBox)
        PictureBox1.Image = GetPic(thing.Name)
    End Sub

    Private Sub Pine1_CheckedChanged(sender As Object, e As EventArgs) Handles Pine1.CheckedChanged
        Dim thing As CheckBox = CType(sender, CheckBox)
        PutSetting(thing.Name, thing.Checked)
        PictureBox1.Image = GetPic(thing.Name)
    End Sub

    Private Sub Pine1_MouseHover(sender As Object, e As EventArgs) Handles Pine1.MouseHover
        Dim thing As CheckBox = CType(sender, CheckBox)
        PictureBox1.Image = GetPic(thing.Name)
    End Sub

    Private Sub Pine2_CheckedChanged(sender As Object, e As EventArgs) Handles Pine2.CheckedChanged
        Dim thing As CheckBox = CType(sender, CheckBox)
        PutSetting(thing.Name, thing.Checked)
        PictureBox1.Image = GetPic(thing.Name)
    End Sub

    Private Sub Pine2_MouseHover(sender As Object, e As EventArgs) Handles Pine2.MouseHover
        Dim thing As CheckBox = CType(sender, CheckBox)
        PictureBox1.Image = GetPic(thing.Name)
    End Sub

    Private Sub Plumeria_CheckedChanged(sender As Object, e As EventArgs) Handles Plumeria.CheckedChanged
        Dim thing As CheckBox = CType(sender, CheckBox)
        PutSetting(thing.Name, thing.Checked)
        PictureBox1.Image = GetPic(thing.Name)

    End Sub

    Private Sub Plumeria_MouseHover(sender As Object, e As EventArgs) Handles Plumeria.MouseHover
        Dim thing As CheckBox = CType(sender, CheckBox)
        PictureBox1.Image = GetPic(thing.Name)
    End Sub

    Private Sub SeaSword_CheckedChanged(sender As Object, e As EventArgs) Handles SeaSword.CheckedChanged
        Dim thing As CheckBox = CType(sender, CheckBox)
        PutSetting(thing.Name, thing.Checked)
        PictureBox1.Image = GetPic(thing.Name)
    End Sub

    Private Sub SeaSword_MouseHover(sender As Object, e As EventArgs) Handles SeaSword.MouseHover
        Dim thing As CheckBox = CType(sender, CheckBox)
        PictureBox1.Image = GetPic(thing.Name)
    End Sub

    Private Sub TropicalBush1_CheckedChanged(sender As Object, e As EventArgs) Handles TropicalBush1.CheckedChanged
        Dim thing As CheckBox = CType(sender, CheckBox)
        PutSetting(thing.Name, thing.Checked)
        PictureBox1.Image = GetPic(thing.Name)
    End Sub

    Private Sub TropicalBush1_MouseHover(sender As Object, e As EventArgs) Handles TropicalBush1.MouseHover
        Dim thing As CheckBox = CType(sender, CheckBox)
        PictureBox1.Image = GetPic(thing.Name)
    End Sub

    Private Sub TropicalBush2_CheckedChanged(sender As Object, e As EventArgs) Handles TropicalBush2.CheckedChanged
        Dim thing As CheckBox = CType(sender, CheckBox)
        PutSetting(thing.Name, thing.Checked)
        PictureBox1.Image = GetPic(thing.Name)
    End Sub

    Private Sub TropicalBush2_MouseHover(sender As Object, e As EventArgs) Handles TropicalBush2.MouseHover
        Dim thing As CheckBox = CType(sender, CheckBox)
        PictureBox1.Image = GetPic(thing.Name)
    End Sub

    Private Sub Undergrowth_CheckedChanged(sender As Object, e As EventArgs) Handles Undergrowth1.CheckedChanged
        Dim thing As CheckBox = CType(sender, CheckBox)
        PutSetting(thing.Name, thing.Checked)
        PictureBox1.Image = GetPic(thing.Name)
    End Sub

    Private Sub Undergrowth_MouseHover(sender As Object, e As EventArgs) Handles Undergrowth1.MouseHover
        Dim thing As CheckBox = CType(sender, CheckBox)
        PictureBox1.Image = GetPic(thing.Name)
    End Sub

    Private Sub WinterAspen_CheckedChanged(sender As Object, e As EventArgs) Handles WinterAspen.CheckedChanged
        Dim thing As CheckBox = CType(sender, CheckBox)
        PutSetting(thing.Name, thing.Checked)
        PictureBox1.Image = GetPic(thing.Name)
    End Sub

    Private Sub WinterAspen_Hover(sender As Object, e As EventArgs) Handles WinterAspen.MouseHover
        Dim thing As CheckBox = CType(sender, CheckBox)
        PictureBox1.Image = GetPic(thing.Name)
    End Sub

    Private Sub WinterPine1_CheckedChanged(sender As Object, e As EventArgs) Handles WinterPine1.CheckedChanged
        Dim thing As CheckBox = CType(sender, CheckBox)
        PutSetting(thing.Name, thing.Checked)
        PictureBox1.Image = GetPic(thing.Name)
    End Sub

    Private Sub WinterPine1_MouseHover(sender As Object, e As EventArgs) Handles WinterPine1.MouseHover
        Dim thing As CheckBox = CType(sender, CheckBox)
        PictureBox1.Image = GetPic(thing.Name)
    End Sub

    Private Sub WinterPine2_CheckedChanged(sender As Object, e As EventArgs) Handles WinterPine2.CheckedChanged
        Dim thing As CheckBox = CType(sender, CheckBox)
        PutSetting(thing.Name, thing.Checked)
        PictureBox1.Image = GetPic(thing.Name)
    End Sub

    Private Sub WinterPine2_MouseHover(sender As Object, e As EventArgs) Handles WinterPine2.MouseHover
        Dim thing As CheckBox = CType(sender, CheckBox)
        PictureBox1.Image = GetPic(thing.Name)
    End Sub

#End Region

#Region "Start/Stop"

    Private Sub FormTrees_Close(sender As Object, e As EventArgs) Handles Me.Closing
        Settings.SaveSettings()
    End Sub

    Private Sub FormTrees_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        _initialized = False
        SetScreen()
        SmartStartEnabled.Text = Global.Outworldz.My.Resources.Smart_Start_Enable_word
        DelayLabel.Text = Global.Outworldz.My.Resources.SSDelay
        ToolTip1.SetToolTip(Seconds, Global.Outworldz.My.Resources.SecondsTips)
        RegionMakerEnableCHeckbox.Checked = Settings.AutoFill
        Me.Text = Global.Outworldz.My.Resources.Smart_Start_word
        SmartStartEnabled.Checked = Settings.SmartStart
        Label1.Text = My.Resources.Enter_1_2
        TabPage1.Text = My.Resources.Smart_Start_word
        TabPage2.Text = My.Resources.Landscaping
        TabPage3.Text = My.Resources.LandMaker
        ListBox2.SelectedIndex = Settings.Skirtsize - 1

        SmoothTextBox.Text = CStr(Settings.LandStrength)
        TaperTextBox.Text = CStr(Settings.LandTaper)

        AviName.Text = Settings.SurroundOwner

        If AviName.Text.Length = 0 Then
            AviName.BackColor = Color.Red
        End If

        Seconds.Text = CStr(Settings.SmartStartTimeout)
        SetScreen()

        FlatLandLevel.Text = CStr(Settings.FlatLandLevel)

        Select Case Settings.TerrainType
            Case "Flat"
                Flat.Checked = True
            Case "Random"
                Rand.Checked = True
            Case "AI"
                AI.Checked = True
            Case "Water"
                Water.Checked = True
            Case Else
                OptionRadioButton.Checked = True
        End Select

        With AviName
            .AutoCompleteCustomSource = MysqlInterface.GetAvatarList()
            .AutoCompleteMode = AutoCompleteMode.Suggest
            .AutoCompleteSource = AutoCompleteSource.CustomSource
        End With

        GetSetting(BeachGrass1.Text)
        GetSetting(Cypress1.Text)
        GetSetting(Cypress2.Text)
        GetSetting(Eelgrass.Text)
        GetSetting(Eucalyptus.Text)
        GetSetting(Fern.Text)
        GetSetting(Grass0.Text)
        GetSetting(Grass1.Text)
        GetSetting(Grass2.Text)
        GetSetting(Grass3.Text)
        GetSetting(Grass4.Text)
        GetSetting(Kelp1.Text)
        GetSetting(Kelp2.Text)
        GetSetting(Oak.Text)
        GetSetting(Palm1.Text)
        GetSetting(Palm2.Text)
        GetSetting(Pine1.Text)
        GetSetting(Pine2.Text)
        GetSetting(Plumeria.Text)
        GetSetting(SeaSword.Text)
        GetSetting(TropicalBush1.Text)
        GetSetting(TropicalBush2.Text)
        GetSetting(Undergrowth1.Text)
        GetSetting(WinterAspen.Text)
        GetSetting(WinterPine1.Text)
        GetSetting(WinterPine2.Text)

        Select Case Settings.Skirtsize
            Case 1
                PictureBox4.Image = My.Resources._3x3
            Case 2
                PictureBox4.Image = My.Resources._5x5
            Case 3
                PictureBox4.Image = My.Resources._7x7
        End Select

        LoadTerrainList()

        Dim n = 0
        Dim s As Boolean
        For Each RegionUUID In PropRegionClass.RegionUuids
            Dim name = PropRegionClass.RegionName(RegionUUID)
            ParkingSpot.Items.Add(name)
            If name = Settings.ParkingLot Then
                ParkingSpot.SelectedIndex = n
                s = True
            End If
            n += 1
        Next

        ' If Debugger.IsAttached Then
        ' debug
        ' LandMaker("7408caab-9a55-4a9b-aa1a-584d95063c43")
        ' End If

        HelpOnce("SmartStart")
        _initialized = True
    End Sub

    Private Sub LoadTerrainList()

        Dim Terrainfolder = IO.Path.Combine(Settings.OpensimBinPath, "Terrains")
        Dim directory As New System.IO.DirectoryInfo(Terrainfolder)
        Dim File As System.IO.FileInfo() = directory.GetFiles()
        Dim File1 As System.IO.FileInfo

        For Each File1 In File
            If File1.Name.EndsWith(".jpg", StringComparison.InvariantCultureIgnoreCase) Then
                Dim pic = Bitmap.FromFile(File1.FullName)
                Dim newImage = New Bitmap(256, 256)
                Dim gr = Graphics.FromImage(newImage)
                gr.DrawImageUnscaled(pic, 0, 0)

                _TerrainList.Add(newImage)
                _TerrainName.Add(File1.FullName)
                If PictureBox3.Image Is Nothing Then
                    PictureBox3.Image = newImage
                End If

            End If
        Next

    End Sub

#End Region

#Region "Help"

    Private Sub HelpToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HelpToolStripMenuItem.Click
        HelpManual("Landscaping")
    End Sub

    Private Sub LandscapingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LandscapingToolStripMenuItem.Click
        HelpManual("Landscaping")
    End Sub

    Private Sub RegionMakingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RegionMakingToolStripMenuItem.Click
        HelpManual("Terrain")
    End Sub

    Private Sub SmartStartToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SmartStartToolStripMenuItem.Click
        HelpManual("SmartStart")
    End Sub

#End Region

    Private Function GetSetting(tree As String) As Boolean
        Dim b As Boolean
        Select Case Settings.GetMySetting(tree)
            Case ""
                b = True
            Case "True"
                b = True
            Case "False"
                b = False
            Case Else
                b = False
        End Select
        Return b

    End Function

    Private Sub ListBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox2.SelectedIndexChanged

        If Not _initialized Then Return
        Settings.Skirtsize = CInt(ListBox2.SelectedItem.ToString)
        Select Case Settings.Skirtsize
            Case 1
                PictureBox4.Image = My.Resources._3x3
            Case 2
                PictureBox4.Image = My.Resources._5x5
            Case 3
                PictureBox4.Image = My.Resources._7x7
        End Select

    End Sub

    Private Sub LoadAllFreeOARs()

        If ApplyTerrainEffectButton.Text <> My.Resources.Apply_word Then
            TextPrint(My.Resources.Stopping_word)
            Abort = True
        End If

        ApplyTerrainEffectButton.Text = My.Resources.Stop_word

        Dim Caution = MsgBox(My.Resources.CautionOAR, vbYesNoCancel Or MsgBoxStyle.MsgBoxSetForeground Or MsgBoxStyle.Critical, My.Resources.Caution_word)
        If Caution <> MsgBoxResult.Yes Then Return

        If Abort Then
            ApplyTerrainEffectButton.Text = My.Resources.Apply_word
            Return
        End If

        Dim Estate = InputBox(My.Resources.WhatEstateName, My.Resources.WhatEstate, "Outworldz")

        If Abort Then
            ApplyTerrainEffectButton.Text = My.Resources.Apply_word
            Return
        End If

        Dim CoordX = CStr(PropRegionClass.LargestX() + 18)
        Dim CoordY = CStr(PropRegionClass.LargestY() + 18)

        Dim coord = InputBox(My.Resources.WheretoStart, My.Resources.StartingLocation, CoordX & "," & CoordY)

        If Abort Then
            ApplyTerrainEffectButton.Text = My.Resources.Apply_word
            Return
        End If

        Dim pattern As Regex = New Regex("(\d+),(\d+)")
        Dim match As Match = pattern.Match(coord)
        If Not match.Success Then
            MsgBox(My.Resources.BadCoordinates, MsgBoxStyle.Exclamation Or MsgBoxStyle.MsgBoxSetForeground, My.Resources.Error_word)
            Return
        End If

        Dim X As Integer = CInt(match.Groups(1).Value)
        Dim Y As Integer = CInt(match.Groups(2).Value)
        Dim StartX As Integer = X

        If Abort Then
            ApplyTerrainEffectButton.Text = My.Resources.Apply_word
            Return
        End If

        If Not PropOpensimIsRunning() Then
            MysqlInterface.DeregisterRegions(False)
        End If

        FormSetup.Buttons(FormSetup.BusyButton)
        PropOpensimIsRunning() = True
        If Not StartMySQL() Then Return
        If Not StartRobust() Then Return
        If FormSetup.Timer1.Enabled = False Then
            FormSetup.Timer1.Interval = 1000
            FormSetup.Timer1.Start() 'Timer starts functioning
        End If

        Dim Max As Integer
        Try
            For Each J In FormSetup.ContentOAR.GetJson

                If Abort Then Exit For
                Dim Name = J.Name

                Dim shortname = IO.Path.GetFileNameWithoutExtension(Name)
                Dim RegionUUID As String
                Dim p = IO.Path.Combine(Settings.OpensimBinPath, $"Regions\{shortname}\Region\{shortname}.ini")

                If Not IO.File.Exists(p) Then
                    ProgressPrint($"{My.Resources.Add_Region_word} {J.Name} ")
                    RegionUUID = PropRegionClass.CreateRegion(shortname)
                Else
                    RegionUUID = PropRegionClass.FindRegionByName(shortname)
                    If GetPrimCount(RegionUUID) > 0 Then
                        ProgressPrint($"{J.Name} {My.Resources.Ok} ")
                        Continue For
                    End If
                End If

                Dim g = New Guid
                If Not Guid.TryParse(RegionUUID, g) Then
                    Continue For
                End If

                PropRegionClass.CrashCounter(RegionUUID) = 0

                ' setup parameters for the load
                Dim sizerow As Integer = 256

                ' convert 1,2,3 to 256, 512, etc
                Dim pattern1 As Regex = New Regex("(.*?)-(\d+)[xX](\d+)")
                Dim match1 As Match = pattern1.Match(Name)
                If match1.Success Then
                    Name = match1.Groups(1).Value
                    sizerow = CInt(match1.Groups(3).Value) * 256
                End If

                PropRegionClass.CoordX(RegionUUID) = X
                PropRegionClass.CoordY(RegionUUID) = Y

                PropRegionClass.SmartStart(RegionUUID) = "True"
                PropRegionClass.Teleport(RegionUUID) = "True"
                PropRegionClass.SizeX(RegionUUID) = sizerow
                PropRegionClass.SizeY(RegionUUID) = sizerow
                PropRegionClass.GroupName(RegionUUID) = shortname

                PropRegionClass.RegionIniFilePath(RegionUUID) = IO.Path.Combine(Settings.OpensimBinPath, $"Regions\{shortname}\Region\{shortname}.ini")
                PropRegionClass.RegionIniFolderPath(RegionUUID) = IO.Path.Combine(Settings.OpensimBinPath, $"Regions\{shortname}\Region")
                PropRegionClass.OpensimIniPath(RegionUUID) = IO.Path.Combine(Settings.OpensimBinPath, $"Regions\{shortname}")

                Dim port = PropRegionClass.LargestPort
                PropRegionClass.GroupPort(RegionUUID) = port
                PropRegionClass.RegionPort(RegionUUID) = port
                PropRegionClass.WriteRegionObject(shortname, shortname)

                PropRegionClass.GetAllRegions()
                Firewall.SetFirewall()

                PropUpdateView = True ' make form refresh
                Application.DoEvents()

                Dim RegionName = PropRegionClass.RegionName(RegionUUID)
                If RegionName = Settings.WelcomeRegion Then Continue For

                ProgressPrint($"{My.Resources.Start_word} {RegionName}")

                If Abort Then Exit For

                ReBoot(RegionUUID)

                ' Wait for it to start booting
                If Not WaitForBooting(RegionUUID) Then Continue For
                If Abort Then Exit For

                If sizerow > Max Then Max = sizerow
                X += CInt((sizerow / 256) + 1)
                If X > StartX + 50 Then
                    X = StartX
                    Y += CInt((Max / 256) + 1)
                    sizerow = 256
                End If

                If Settings.Sequential Then
                    If Not WaitForBooted(RegionUUID) Then Continue For
                End If

                If Abort Then Exit For

                If GetPrimCount(RegionUUID) = 0 Then
                    Dim File = $"{PropDomain}/Outworldz_Installer/OAR/{J.Name}"
                    PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.NoError

                    ' TODO estate set owner <estate ID> <owner UUID> - Change the owner of an estate. This command supports two forms; this one uses the owner's UUID.
                    ' TODO Estate create < Owner UUID> <estate name> - Must be a user UUID, m which you can get from 'show names'
                    If EstateName(RegionUUID).Length = 0 Then
                        ConsoleCommand(RegionUUID, Estate)
                    End If

                    ConsoleCommand(RegionUUID, $"change region ""{RegionName}""")
                    ConsoleCommand(RegionUUID, $"load oar --force-terrain --force-parcels ""{File}""")

                    If Settings.MapType <> "None" Or PropRegionClass.MapType(RegionUUID).Length > 0 Then
                        ConsoleCommand(RegionUUID, "generate map")
                        Sleep(10000) ' wait a bit to let it make a mas
                    End If

                    ConsoleCommand(RegionUUID, "backup")
                    ConsoleCommand(RegionUUID, "alert Power off!")
                    PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.ShuttingDownForGood

                    ConsoleCommand(RegionUUID, "quit")
                    ConsoleCommand(RegionUUID, "quit")
                    Sleep(100)
                Else
                    PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.ShuttingDownForGood
                    ConsoleCommand(RegionUUID, "quit")
                    ConsoleCommand(RegionUUID, "quit")
                End If

                PropUpdateView = True

                If Abort Then Exit For

                PropUpdateView = True
                Dim ctr = 120
                If Settings.Sequential Then
                    If Abort Then Exit For
                    While PropRegionClass.Status(RegionUUID) <> RegionMaker.SIMSTATUSENUM.Stopped AndAlso Not Abort
                        Sleep(1000)
                        Application.DoEvents()
                        ctr -= 1
                        If ctr = 0 Then Exit While
                    End While
                End If
                ProgressPrint($"{RegionName} {My.Resources.Loaded_word}")
            Next

            If Abort Then
                TextPrint(My.Resources.Stopped_word)
            End If
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try

        ApplyTerrainEffectButton.Text = My.Resources.Apply_word

        TextPrint(My.Resources.New_is_Done)
        Settings.SaveSettings()

    End Sub

    Private Sub LoadTerrain_Click(sender As Object, e As EventArgs) Handles LoadTerrain.Click

        'load menu
        Dim RegionName = ChooseRegion(True)
        If RegionName.Length = 0 Then Return

        Dim RegionUUID As String = PropRegionClass.FindRegionByName(RegionName)

        RPC_Region_Command(RegionUUID, $"change region ""{RegionName}""")

        Dim Terrainfolder = IO.Path.Combine(Settings.OpensimBinPath, "Terrains")
        ' Create an instance of the open file dialog box. Set filter options and filter index.
        Using openFileDialog1 As OpenFileDialog = New OpenFileDialog With {
            .InitialDirectory = Terrainfolder,
            .Filter = Global.Outworldz.My.Resources.OAR_Load_and_Save & "(*.png,*.r32,*.raw)|*.png;*.r32;*.raw;|All Files (*.*)|*.*",
            .FilterIndex = 1,
            .Multiselect = False
            }

            ' Call the ShowDialog method to show the dialog box.
            Dim UserClickedOK As DialogResult = openFileDialog1.ShowDialog

            ' Process input if the user clicked OK.
            If UserClickedOK = DialogResult.OK Then

                Dim thing = openFileDialog1.FileName
                If thing.Length > 0 Then
                    If Not IO.File.Exists($"""{Terrainfolder}\{Name}-Backup.r32""") Then
                        RPC_Region_Command(RegionUUID, $"terrain save ""{Terrainfolder}\{RegionName}-Backup.r32""")
                    End If
                    If Not IO.File.Exists($"""{Terrainfolder}\{Name}-Backup.jpg""") Then
                        RPC_Region_Command(RegionUUID, $"terrain save ""{Terrainfolder}\{RegionName}-Backup.jpg""")
                    End If
                    RPC_Region_Command(RegionUUID, $"terrain load ""{thing}""")
                End If
            End If

        End Using

    End Sub

#Region "MakeXML"

    Private Sub LoadPlant(Name As String)

        _SelectedPlant = Name
        _initted = False

        PictureBox2.Image = GetPic(Name)

        Dim Setup As XmlReaderSettings = New XmlReaderSettings With {
            .IgnoreWhitespace = True
        }

        Dim path = IO.Path.Combine(Settings.OpensimBinPath, "Trees\" & Name & ".xml")
        If File.Exists(path) Then

#Disable Warning CA3075 ' Insecure DTD processing in XML
            Using TextReader As XmlTextReader = New XmlTextReader(path)
#Enable Warning CA3075 ' Insecure DTD processing in XML
                ' Read until end of file
                While (TextReader.Read())
                    Dim nType As XmlNodeType = TextReader.NodeType
                    ' If node type us a declaration
                    If (nType = XmlNodeType.XmlDeclaration) Then
                        Console.WriteLine("Declaration:" + TextReader.Name.ToString())
                    End If
                    ' if node type Is a comment
                    If (nType = XmlNodeType.Comment) Then
                        Console.WriteLine("Comment:" + TextReader.Name.ToString())
                    End If
                    ' if node type us an attribute
                    If (nType = XmlNodeType.Attribute) Then
                        Console.WriteLine("Attribute:" + TextReader.Name.ToString())
                    End If
                    ' if node type Is an element
                    If (nType = XmlNodeType.Element) Then
                        Console.WriteLine("Element:" + TextReader.Name.ToString())
                        If TextReader.Name.ToString() = "m_tree_quantity" Then
                            Qty.Text = TextReader.ReadInnerXml
                        ElseIf TextReader.Name.ToString() = "m_treeline_low" Then
                            TreeLineLow.Text = TextReader.ReadInnerXml
                        ElseIf TextReader.Name.ToString() = "m_treeline_high" Then
                            TreeLineHight.Text = TextReader.ReadInnerXml
                            Dim v = TextReader.Value
                        ElseIf TextReader.Name.ToString() = "m_initial_scale" Then
                            Dim X = TextReader.ReadInnerXml
                            Dim pattern As Regex
                            pattern = New Regex("<X>(.*?)</X>")
                            Dim matchX As Match = pattern.Match(X)
                            pattern = New Regex("<Y>(.*?)</Y>")
                            Dim matchY As Match = pattern.Match(X)
                            pattern = New Regex("<Z>(.*?)</Z>")
                            Dim matchZ As Match = pattern.Match(X)
                            StartSizeX.Text = matchX.Groups(1).Value
                            StartSizeY.Text = matchY.Groups(1).Value
                            StartSizeZ.Text = matchZ.Groups(1).Value

                        ElseIf TextReader.Name.ToString() = "m_maximum_scale" Then
                            Dim X = TextReader.ReadInnerXml
                            Dim pattern As Regex
                            pattern = New Regex("<X>(.*?)</X>")
                            Dim matchX As Match = pattern.Match(X)
                            pattern = New Regex("<Y>(.*?)</Y>")
                            Dim matchY As Match = pattern.Match(X)
                            pattern = New Regex("<Z>(.*?)</Z>")
                            Dim matchZ As Match = pattern.Match(X)

                            EndsizeX.Text = matchX.Groups(1).Value
                            EndsizeY.Text = matchY.Groups(1).Value
                            EndsizeZ.Text = matchZ.Groups(1).Value

                            ' if node type Is an entity
                        ElseIf (nType = XmlNodeType.Entity) Then
                            Console.WriteLine("Entity:" + TextReader.Name.ToString())

                            ' if node type Is a Process Instruction
                        ElseIf (nType = XmlNodeType.Entity) Then
                            Console.WriteLine("Entity:" + TextReader.Name.ToString())

                            ' if node type a document
                        ElseIf (nType = XmlNodeType.DocumentType) Then
                            Console.WriteLine("Document:" + TextReader.Name.ToString())

                            ' if node type Is white space
                        ElseIf (nType = XmlNodeType.Whitespace) Then
                            Console.WriteLine("WhiteSpace:" + TextReader.Name.ToString())
                        End If
                    End If

                End While
            End Using

        End If
        _initted = True

    End Sub

    Private Sub MakeSetting()

        Dim Size = 256
        Dim XMLName = _SelectedPlant
        If XMLName Is Nothing Then Return

        Dim quant = CInt("0" & Qty.Text)
        Dim TreelineLow = CInt("0" & Me.TreeLineLow.Text)
        Dim TreelineHigh = CInt("0" & TreeLineHight.Text)
        Dim StartX = CInt("0" & StartSizeX.Text)
        Dim StartY = CInt("0" & StartSizeY.Text)
        Dim StartZ = CInt("0" & StartSizeZ.Text)
        Dim EndX = CInt("0" & EndsizeX.Text)
        Dim EndY = CInt("0" & EndsizeY.Text)
        Dim EndZ = CInt("0" & EndsizeZ.Text)

        Dim xml As String = $"<Copse>
  <m_name>{XMLName}</m_name>
  <m_frozen>false</m_frozen>
  <m_tree_type>{XMLName}</m_tree_type>
  <m_tree_quantity>{quant}</m_tree_quantity>
  <m_treeline_low>{TreelineLow}</m_treeline_low>
  <m_treeline_high>{TreelineHigh}</m_treeline_high>
  <m_seed_point>
    <X>{Size / 2}</X>
    <Y>{Size / 2}</Y>
    <Z>0</Z>
  </m_seed_point>
  <m_range>{Size - 5}</m_range>
  <m_initial_scale>
    <X>{StartX}</X>
    <Y>{StartY}</Y>
    <Z>{StartZ}</Z>
  </m_initial_scale>
  <m_maximum_scale>
    <X>{EndX}</X>
    <Y>{EndY}</Y>
    <Z>{EndZ}</Z>
  </m_maximum_scale>
  <m_rate>
    <X>0.01</X>
    <Y>0.01</Y>
    <Z>0.01</Z>
  </m_rate>
</Copse>
  "
        Dim output = IO.Path.Combine(Settings.OpensimBinPath, $"Trees/{XMLName}.xml")

        Try
            Using Writer As New StreamWriter(output)
                Writer.Write(xml)
            End Using
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try
    End Sub

    Private Sub Maketypes(FileName As System.IO.FileInfo, RegionUUID As String)

        Dim Terrainfolder = IO.Path.Combine(Settings.OpensimBinPath, "Terrains")
        Dim extension = IO.Path.GetExtension(FileName.Name)
        Select Case extension
            Case "raw"
            Case "r32"
            Case "jpg"
            Case "png"
            Case "ter"
            Case Else
                Return
        End Select

        Dim Rname = PropRegionClass.RegionName(RegionUUID)
        RPC_Region_Command(RegionUUID, $"change region ""{Rname}""")

        Dim RegionName = FileName.Name
        RPC_Region_Command(RegionUUID, $"terrain load ""{Terrainfolder}\{FileName}""")

        RegionName = RegionName.Replace($"{extension}", "")

        If Not IO.File.Exists($"{Terrainfolder}\{RegionName}.r32") Then RPC_Region_Command(RegionUUID, $"terrain save ""{Terrainfolder}\{RegionName}.r32""")
        If Not IO.File.Exists($"{Terrainfolder}\{RegionName}.jpg") Then RPC_Region_Command(RegionUUID, $"terrain save ""{Terrainfolder}\{RegionName}.jpg""")
        If Not IO.File.Exists($"{Terrainfolder}\{RegionName}.png") Then RPC_Region_Command(RegionUUID, $"terrain save ""{Terrainfolder}\{RegionName}.png""")
        If Not IO.File.Exists($"{Terrainfolder}\{RegionName}.ter") Then RPC_Region_Command(RegionUUID, $"terrain save ""{Terrainfolder}\{RegionName}.ter""")

    End Sub

#End Region

#Region "4Choices"

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles Flat.CheckedChanged
        Settings.TerrainType = "Flat"
        TerrainPic.Image = My.Resources.flatland
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles Rand.CheckedChanged
        Settings.TerrainType = "Random"
        TerrainPic.Image = My.Resources.Random
    End Sub

    Private Sub RadioButton3_CheckedChanged(sender As Object, e As EventArgs) Handles AI.CheckedChanged
        Settings.TerrainType = "AI"
        TerrainPic.Image = My.Resources.AI
    End Sub

    Private Sub Water_CheckedChanged(sender As Object, e As EventArgs) Handles Water.CheckedChanged
        Settings.TerrainType = "Water"
        TerrainPic.Image = My.Resources.water
    End Sub

#End Region

#Region "Tool strip"

    Private Sub RebuildTerrainsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RebuildTerrainsToolStripMenuItem.Click

        Dim RegionName = ChooseRegion(True)
        If RegionName.Length = 0 Then Return

        Dim RegionUUID As String = PropRegionClass.FindRegionByName(RegionName)

        Dim Terrainfolder = IO.Path.Combine(Settings.OpensimBinPath, "Terrains")
        Dim directory As New System.IO.DirectoryInfo(Terrainfolder)
        Dim File As System.IO.FileInfo() = directory.GetFiles()
        Dim File1 As System.IO.FileInfo
        For Each File1 In File
            Maketypes(File1, RegionUUID)
        Next
    End Sub

    Private Sub SaveAllTerrain_Click(sender As Object, e As EventArgs) Handles SaveAllTerrain.Click
        'Save all
        Dim Terrainfolder = IO.Path.Combine(Settings.OpensimBinPath, "Terrains")
        For Each RegionUUID In PropRegionClass.RegionUuids
            Dim RegionName = PropRegionClass.RegionName(RegionUUID)

            If PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.Booted Then
                RPC_Region_Command(RegionUUID, $"change region {RegionName}")
                RPC_Region_Command(RegionUUID, $"terrain save ""{Terrainfolder}\{RegionName}.r32""")
                RPC_Region_Command(RegionUUID, $"terrain save ""{Terrainfolder}\{RegionName}.jpg""")
            End If
        Next

    End Sub

    Private Sub Seconds_TextChanged(sender As Object, e As EventArgs) Handles Seconds.TextChanged
        Dim digitsOnly As Regex = New Regex("[^\d]")
        Seconds.Text = digitsOnly.Replace(Seconds.Text, "")
        Settings.SmartStartTimeout = CInt("0" & Seconds.Text)
        If Settings.SmartStartTimeout < 15 Then Settings.SmartStartTimeout = 15
        ProgressPrint("Minimum time is 15 seconds. Default is 30 seconds.")
    End Sub

    Private Sub ToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles SaveTerrain.Click

        'Save menu
        If Not _initialized Then Return
        Dim RegionName = ChooseRegion(True)
        If RegionName.Length = 0 Then Return
        Dim RegionUUID As String = PropRegionClass.FindRegionByName(RegionName)

        Dim Terrainfolder = IO.Path.Combine(Settings.OpensimBinPath, "Terrains")

        If Not IO.File.Exists($"{Terrainfolder}\{RegionName}.r32") Then RPC_Region_Command(RegionUUID, $"terrain save ""{Terrainfolder}\{RegionName}.r32""")
        If Not IO.File.Exists($"{Terrainfolder}\{RegionName}.jpg") Then RPC_Region_Command(RegionUUID, $"terrain save ""{Terrainfolder}\{RegionName}.jpg""")

    End Sub

#End Region

#Region "SmartStart"

    Private Sub SetSetting(check As CheckBox)
        check.Checked = True
    End Sub

    Private Sub SmartStartEnabled_CheckedChanged(sender As Object, e As EventArgs) Handles SmartStartEnabled.CheckedChanged
        If Not _initialized Then Return
        Settings.SmartStart = SmartStartEnabled.Checked
        ProgressPrint("Smart Start is " & CStr(SmartStartEnabled.Checked))
    End Sub

#End Region

#Region "All/None"

#End Region

#Region "PictureBox"

    Private Sub ApplyButton_Click(sender As Object, e As EventArgs) Handles ApplyTerrainEffectButton.Click

        'AI or .r32
        Dim name = ChooseRegion(True)
        Dim RegionUUID As String = PropRegionClass.FindRegionByName(name)
        If RegionUUID.Length = 0 Then Return

        If Not RPC_Region_Command(RegionUUID, $"change region ""{name}""") Then Return

        Dim backupname = IO.Path.Combine(Settings.OpensimBinPath, "Terrains")
        If IO.File.Exists($"{backupname}\{name}-Backup.r32") Then
            DeleteFile($"{backupname}\{name}-Backup.r32")
        End If

        If Not RPC_Region_Command(RegionUUID, $"terrain save ""{backupname}\{name}-Backup.r32""") Then Return

        If IO.File.Exists($"{backupname}\{name}-Backup.jpg") Then
            DeleteFile($"{backupname}\{name}-Backup.jpg")
        End If
        If Not RPC_Region_Command(RegionUUID, $"terrain save ""{backupname}\{name}-Backup.jpg""") Then Return

        If RegionUUID.Length > 0 Then
            GenLand(RegionUUID)
        End If

    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles NextButton.Click

        _Index += 1
        If _Index >= _TerrainList.Count Then _Index = 0
        PictureBox3.Image = _TerrainList.Item(_Index)
        LabelName.Text = IO.Path.GetFileNameWithoutExtension(_TerrainName.Item(_Index))

    End Sub

    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PrevButton.Click

        _Index -= 1
        If _Index < 0 Then _Index = _TerrainList.Count - 1
        PictureBox3.Image = _TerrainList.Item(_Index)
        LabelName.Text = IO.Path.GetFileNameWithoutExtension(_TerrainName.Item(_Index))

    End Sub

    Private Sub TerrainApply_Click(sender As Object, e As EventArgs) Handles TerrainApply.Click
        ' from photo
        Dim backupname = IO.Path.Combine(Settings.OpensimBinPath, "Terrains")
        Dim name = ChooseRegion(True)
        Dim RegionUUID As String = PropRegionClass.FindRegionByName(name)
        If RegionUUID.Length = 0 Then Return

        Dim Tname = _TerrainName.Item(_Index)
        Tname = Tname.Replace(".jpg", ".r32")
        If IO.File.Exists(Tname) Then
            If Not RPC_Region_Command(RegionUUID, $"change region ""{name}""") Then Return
            If Not RPC_Region_Command(RegionUUID, $"terrain load ""{Tname}""") Then Return
        End If

    End Sub

#End Region

#Region "Apply/Freeze"

    Private Sub ApplyPlantButton_Click(sender As Object, e As EventArgs) Handles ApplyPlantButton.Click
        'plant apply
        Dim name = ChooseRegion(True)
        Dim RegionUUID As String = PropRegionClass.FindRegionByName(name)
        If RegionUUID.Length = 0 Then Return

        RPC_Region_Command(RegionUUID, $"change region ""{name}""")

        Dim backupname = IO.Path.Combine(Settings.OpensimBinPath, "Terrains")
        If IO.File.Exists($"{backupname}\{name}-Backup.r32") Then
            DeleteFile($"{backupname}\{name}-Backup.r32")
        End If
        RPC_Region_Command(RegionUUID, $"terrain save ""{backupname}\{name}-Backup.r32""")
        If IO.File.Exists($"{backupname}\{name}-Backup.jpg") Then
            DeleteFile($"{backupname}\{name}-Backup.jpg")
        End If
        RPC_Region_Command(RegionUUID, $"terrain save ""{backupname}\{name}-Backup.jpg""")
        If RegionUUID.Length > 0 Then
            GenTrees(RegionUUID)
        End If

    End Sub

    Private Sub FreezeButton_Click(sender As Object, e As EventArgs) Handles FreezeButton.Click

        Dim name = ChooseRegion(True)
        Dim RegionUUID As String = PropRegionClass.FindRegionByName(name)
        If RegionUUID.Length = 0 Then Return

        Dim backupname = IO.Path.Combine(Settings.OpensimBinPath, "Terrains")
        CopyFileFast($"{backupname}\{name}-Backup.r32", $"{backupname}\{name}.r32")
        CopyFileFast($"{backupname}\{name}-Backup.jpg", $"{backupname}\{name}.jpg")

    End Sub

    Private Sub Revert_Click(sender As Object, e As EventArgs) Handles RevertButton.Click

        Dim name = ChooseRegion(True)
        Dim RegionUUID As String = PropRegionClass.FindRegionByName(name)
        If RegionUUID.Length = 0 Then Return

        Dim backupname = IO.Path.Combine(Settings.OpensimBinPath, "Terrains")
        RPC_Region_Command(RegionUUID, $"change region ""{name}""")
        If IO.File.Exists(backupname) Then
            RPC_Region_Command(RegionUUID, $"terrain load ""{backupname}\{name}-Backup.r32""")
        End If

    End Sub

#End Region

#Region "Radio"

    Private Sub Grass0Radio_CheckedChanged(sender As Object, e As EventArgs) Handles Grass0Radio.CheckedChanged
        LoadPlant(CStr(sender.text))
    End Sub

    Private Sub RadioButton1_CheckedChanged_1(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        LoadPlant(CStr(sender.text))
    End Sub

    Private Sub RadioButton10_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton10.CheckedChanged
        LoadPlant(CStr(sender.text))
    End Sub

    Private Sub RadioButton11_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton11.CheckedChanged
        LoadPlant(CStr(sender.text))
    End Sub

    Private Sub RadioButton12_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton12.CheckedChanged
        LoadPlant(CStr(sender.text))
    End Sub

    Private Sub RadioButton13_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton13.CheckedChanged
        LoadPlant(CStr(sender.text))
    End Sub

    Private Sub RadioButton14_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton14.CheckedChanged
        LoadPlant(CStr(sender.text))
    End Sub

    Private Sub RadioButton15_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton15.CheckedChanged
        LoadPlant(CStr(sender.text))
    End Sub

    Private Sub RadioButton16_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton16.CheckedChanged
        LoadPlant(CStr(sender.text))
    End Sub

    Private Sub RadioButton17_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton17.CheckedChanged
        LoadPlant(CStr(sender.text))
    End Sub

    Private Sub RadioButton18_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton18.CheckedChanged
        LoadPlant(CStr(sender.text))
    End Sub

    Private Sub RadioButton19_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton19.CheckedChanged
        LoadPlant(CStr(sender.text))
    End Sub

    Private Sub RadioButton2_CheckedChanged_1(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        LoadPlant(CStr(sender.text))
    End Sub

    Private Sub RadioButton20_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton20.CheckedChanged
        LoadPlant(CStr(sender.text))
    End Sub

    Private Sub RadioButton21_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton21.CheckedChanged
        LoadPlant(CStr(sender.text))
    End Sub

    Private Sub RadioButton22_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton22.CheckedChanged
        LoadPlant(CStr(sender.text))
    End Sub

    Private Sub RadioButton23_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton23.CheckedChanged
        LoadPlant(CStr(sender.text))
    End Sub

    Private Sub RadioButton24_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton24.CheckedChanged
        LoadPlant(CStr(sender.text))
    End Sub

    Private Sub RadioButton25_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton25.CheckedChanged
        LoadPlant(CStr(sender.text))
    End Sub

    Private Sub RadioButton26_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton26.CheckedChanged
        LoadPlant(CStr(sender.text))
    End Sub

    Private Sub RadioButton3_CheckedChanged_1(sender As Object, e As EventArgs) Handles RadioButton3.CheckedChanged
        LoadPlant(CStr(sender.text))
    End Sub

    Private Sub RadioButton4_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton4.CheckedChanged
        LoadPlant(CStr(sender.text))
    End Sub

    Private Sub RadioButton5_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton5.CheckedChanged
        LoadPlant(CStr(sender.text))
    End Sub

    Private Sub RadioButton6_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton6.CheckedChanged
        LoadPlant(CStr(sender.text))
    End Sub

    Private Sub RadioButton7_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton7.CheckedChanged
        LoadPlant(CStr(sender.text))
    End Sub

    Private Sub RadioButton8_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton8.CheckedChanged
        LoadPlant(CStr(sender.text))
    End Sub

    Private Sub RadioButton9_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton9.CheckedChanged
        LoadPlant(CStr(sender.text))
    End Sub

#End Region

#Region "Size boxes"

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles ApplyEdit.Click
        MakeSetting()
    End Sub

    Private Sub Flat_TextChanged(sender As Object, e As EventArgs) Handles FlatLandLevel.TextChanged
        Dim digitsOnly As Regex = New Regex("[^\d\.]")
        FlatLandLevel.Text = digitsOnly.Replace(FlatLandLevel.Text, "")
        If Convert.ToSingle("0" & FlatLandLevel.Text, Globalization.CultureInfo.InvariantCulture) > 100 Then FlatLandLevel.Text = CStr(100)
        Settings.FlatLandLevel = CDbl("0" & FlatLandLevel.Text)
    End Sub

    Private Sub Noise_CheckedChanged(sender As Object, e As EventArgs) Handles Noise.CheckedChanged
        Settings.LandNoise = Noise.Checked
        Settings.SaveSettings()
    End Sub

    Private Sub OptionRadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles OptionRadioButton.CheckedChanged
        Settings.TerrainType = "Option"
        TerrainPic.Image = My.Resources.NoImage
    End Sub

    Private Sub ParkingSpot_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ParkingSpot.SelectedIndexChanged
        Settings.ParkingLot = ParkingSpot.SelectedItem.ToString
        ProgressPrint("Arrivals to powered-off sims are redirected to " & ParkingSpot.SelectedItem.ToString)
        Settings.SaveSettings()
    End Sub

    Private Sub RegionMakerEnableCHeckbox_CheckedChanged(sender As Object, e As EventArgs) Handles RegionMakerEnableCHeckbox.CheckedChanged
        If Not _initialized Then Return
        Settings.AutoFill = RegionMakerEnableCHeckbox.Checked
        Settings.SaveSettings()
    End Sub

    Private Sub Smooth_CheckedChanged(sender As Object, e As EventArgs) Handles Smooth.CheckedChanged
        If Not _initialized Then Return
        Settings.LandSmooth = Smooth.Checked
        Settings.SaveSettings()
    End Sub

    Private Sub Smooth_TextChanged_2(sender As Object, e As EventArgs) Handles SmoothTextBox.TextChanged
        If Not _initialized Then Return
        Dim digitsOnly As Regex = New Regex("[^\d\.]")
        SmoothTextBox.Text = digitsOnly.Replace(SmoothTextBox.Text, "")
        If Convert.ToSingle("0" & SmoothTextBox.Text, Globalization.CultureInfo.InvariantCulture) > 1 Then SmoothTextBox.Text = CStr(1)
        Settings.LandStrength = CDbl("0" & SmoothTextBox.Text)
        Settings.SaveSettings()
    End Sub

#End Region

#Region "Editor"

    Private Sub BulkLoadRegionsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BulkLoadRegionsToolStripMenuItem.Click
        LoadAllFreeOARs()
    End Sub

    Private Sub EndsizeX_TextChanged(sender As Object, e As EventArgs) Handles EndsizeX.TextChanged
        If Not _initted Then Return
        Dim digitsOnly As Regex = New Regex("[^\d\.]")
        EndsizeX.Text = digitsOnly.Replace(EndsizeX.Text, "")
        If Convert.ToSingle("0" & EndsizeX.Text, Globalization.CultureInfo.InvariantCulture) > 255 Then EndsizeX.Text = CStr(255)
    End Sub

    Private Sub EndsizeY_TextChanged(sender As Object, e As EventArgs) Handles EndsizeY.TextChanged
        If Not _initted Then Return
        Dim digitsOnly As Regex = New Regex("[^\d\.]")
        EndsizeY.Text = digitsOnly.Replace(EndsizeY.Text, "")
        If Convert.ToSingle("0" & EndsizeY.Text, Globalization.CultureInfo.InvariantCulture) > 255 Then EndsizeY.Text = CStr(255)
        MakeSetting()
    End Sub

    Private Sub EndsizeZ_TextChanged(sender As Object, e As EventArgs) Handles EndsizeZ.TextChanged
        If Not _initted Then Return
        Dim digitsOnly As Regex = New Regex("[^\d\.]")
        EndsizeZ.Text = digitsOnly.Replace(EndsizeZ.Text, "")
        If Convert.ToSingle("0" & EndsizeZ.Text, Globalization.CultureInfo.InvariantCulture) > 255 Then EndsizeZ.Text = CStr(255)
    End Sub

    Private Sub MinLandHeight_TextChanged(sender As Object, e As EventArgs) Handles TreeLineLow.TextChanged
        If Not _initted Then Return
        Dim digitsOnly As Regex = New Regex("[^\d\.]")
        TreeLineLow.Text = digitsOnly.Replace(TreeLineLow.Text, "")
        If CInt(TreeLineLow.Text) < 0 Then TreeLineLow.Text = CStr(0)
        MakeSetting()
    End Sub

    Private Sub Qty_TextChanged(sender As Object, e As EventArgs) Handles Qty.TextChanged
        If Not _initted Then Return
        Dim digitsOnly As Regex = New Regex("[^\d]")
        Qty.Text = digitsOnly.Replace(Qty.Text, "")
        If CInt(Qty.Text) > 500 Then Qty.Text = CStr(500)
        MakeSetting()
    End Sub

    Private Sub StartSize_TextChanged(sender As Object, e As EventArgs) Handles StartSizeX.TextChanged
        If Not _initted Then Return
        Dim digitsOnly As Regex = New Regex("[^\d\.]")
        StartSizeX.Text = digitsOnly.Replace(StartSizeX.Text, "")
        If Convert.ToSingle("0" & StartSizeX.Text, Globalization.CultureInfo.InvariantCulture) < 0 Then StartSizeX.Text = CStr(0)
        MakeSetting()
    End Sub

    Private Sub StartSizeY_TextChanged(sender As Object, e As EventArgs) Handles StartSizeY.TextChanged
        If Not _initted Then Return
        Dim digitsOnly As Regex = New Regex("[^\d\.]")
        StartSizeY.Text = digitsOnly.Replace(StartSizeY.Text, "")
        If Convert.ToSingle("0" & StartSizeY.Text, Globalization.CultureInfo.InvariantCulture) < 0 Then StartSizeY.Text = CStr(0)
        MakeSetting()
    End Sub

    Private Sub StartSizeZ_TextChanged(sender As Object, e As EventArgs) Handles StartSizeZ.TextChanged
        If Not _initted Then Return
        Dim digitsOnly As Regex = New Regex("[^\d\.]")
        StartSizeZ.Text = digitsOnly.Replace(StartSizeZ.Text, "")
        If Convert.ToSingle("0" & StartSizeZ.Text, Globalization.CultureInfo.InvariantCulture) < 0 Then StartSizeZ.Text = CStr(0)
        MakeSetting()
    End Sub

    Private Sub TaperTextBox_TextChanged(sender As Object, e As EventArgs) Handles TaperTextBox.TextChanged
        If Not _initted Then Return
        Dim digitsOnly As Regex = New Regex("[^\d\.]")
        TaperTextBox.Text = digitsOnly.Replace(TaperTextBox.Text, "")
        If Convert.ToSingle("0" & TaperTextBox.Text, Globalization.CultureInfo.InvariantCulture) > 1 Then TaperTextBox.Text = CStr(1)
        Settings.LandTaper = CDbl("0" & TaperTextBox.Text)
        MakeSetting()
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TreeLineHight.TextChanged
        If Not _initted Then Return
        Dim digitsOnly As Regex = New Regex("[^\d\.]")
        TreeLineHight.Text = digitsOnly.Replace(TreeLineHight.Text, "")
        If CInt(TreeLineHight.Text) > 255 Then TreeLineHight.Text = CStr(255)
        MakeSetting()
    End Sub

    Private Sub ViewTerrainFolderToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ViewTerrainFolderToolStripMenuItem.Click

        Process.Start("explorer.exe", IO.Path.Combine(Settings.OpensimBinPath, "Terrains"))

    End Sub

#End Region

#Region "Help"

    Private Sub AvatarNameTextBox_TextChanged(sender As Object, e As EventArgs) Handles AviName.TextChanged

        If Not _initialized Then Return
        If AviName.Text.Length > 0 Then
            AviName.BackColor = Color.White
        End If

        If AviName.Text.Length > 0 Then
            Settings.SurroundOwner = AviName.Text
            Settings.SaveSettings()

            If IsMySqlRunning() Then
                Dim AvatarUUID As String = GetAviUUUD(AviName.Text)
                If AvatarUUID.Length > 0 Then
                    Dim INI = Settings.LoadIni(IO.Path.Combine(Settings.OpensimBinPath, "Estates\Estates.ini"), ";")
                    Settings.SetIni("SimSurround", "Owner", AvatarUUID)
                    Settings.SaveINI(INI, System.Text.Encoding.ASCII)
                End If
            End If
        End If

    End Sub

    Private Sub HelpLandcapingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HelpLandcapingToolStripMenuItem.Click
        HelpManual("Landscaping")
    End Sub

    Private Sub HelpPlantEditorToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HelpPlantEditorToolStripMenuItem.Click
        HelpManual("Plant Editor")
    End Sub

    Private Sub HelpSmartStartToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HelpSmartStartToolStripMenuItem.Click
        HelpManual("SmartStart")
    End Sub

    Private Sub HelpTerrainsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HelpTerrainsToolStripMenuItem.Click
        HelpManual("Terrains")
    End Sub

#End Region

End Class