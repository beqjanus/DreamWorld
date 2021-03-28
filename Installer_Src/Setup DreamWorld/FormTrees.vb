Public Class FormTrees

#Region "Load/Save"

    Private Sub FormTrees_Close(sender As Object, e As EventArgs) Handles Me.Closing
        Settings.SaveSettings()
    End Sub

    Private Sub FormTrees_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Select Case Settings.TerrainType
            Case "Flat"
                Flat.Checked = True
            Case "Random"
                Rand.Checked = True
            Case "AI"
                AI.Checked = True
            Case Else
                Flat.Checked = True
        End Select

        GetSetting(BeachGrass)
        GetSetting(Cypress1)
        GetSetting(Cypress2)
        GetSetting(Eelgrass)
        GetSetting(Eucalyptus)
        GetSetting(Fern)
        GetSetting(Grass0)
        GetSetting(Grass1)
        GetSetting(Grass2)
        GetSetting(Grass3)
        GetSetting(Grass4)
        GetSetting(Kelp1)
        GetSetting(Kelp2)
        GetSetting(Oak)
        GetSetting(Palm1)
        GetSetting(Palm2)
        GetSetting(Pine1)
        GetSetting(Pine2)
        GetSetting(Plumeria)
        GetSetting(SeaSword)
        GetSetting(TropicalBush1)
        GetSetting(TropicalBush2)
        GetSetting(Undergrowth)
        GetSetting(WinterAspen)
        GetSetting(WinterPine1)
        GetSetting(WinterPine2)

        HelpOnce("Landscaping")

    End Sub

    Private Sub GetSetting(Box As CheckBox)
        Dim b As Boolean
        Select Case Settings.GetMySetting(Box.Name)
            Case ""
                b = True
            Case "True"
                b = True
            Case "False"
                b = False
            Case Else
                b = False
        End Select

        Box.Checked = b
    End Sub

    Private Sub HelpToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HelpToolStripMenuItem.Click
        HelpManual("Landscaping")
    End Sub

    Private Sub PutSetting(name As String, value As Boolean)

        Settings.SetMySetting(name, CStr(value))
        Settings.SaveSettings()

    End Sub

#End Region

#Region "Terrain"

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

#Region "Test"

    Public Sub Test_Click(sender As Object, e As EventArgs) Handles ApplyButtton.Click

        Dim name = ChooseRegion(True)
        Dim RegionUUID As String = PropRegionClass.FindRegionByName(name)
        If RegionUUID.Length = 0 Then Return

        Dim backupname = IO.Path.Combine(Settings.OpensimBinPath, "Terrains")
        If Not IO.File.Exists(backupname) Then
            RPC_Region_Command(RegionUUID, $"terrain save {backupname}")
        End If

        RPC_Region_Command(RegionUUID, "tree active true")
        For Each Type As String In TreeList
            If Not RPC_Region_Command(RegionUUID, $"tree remove {Type}") Then
                Diagnostics.Debug.Print("Error")
                Return
            End If
        Next

        If RegionUUID.Length > 0 Then
            If Not RPC_Region_Command(RegionUUID, "terrain fill 23") Then
                Diagnostics.Debug.Print("Error")
                Return
            End If
            GenLand(RegionUUID)
            Application.DoEvents()
            GenTrees(RegionUUID)
            Application.DoEvents()
        Else
            MsgBox("No region named Test is running", vbInformation)
        End If

    End Sub

    Private Sub All_CheckedChanged(sender As Object, e As EventArgs) Handles All.CheckedChanged

        If All.Checked Then
            None.Checked = False
            SetSetting(BeachGrass)
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
            SetSetting(Undergrowth)
            SetSetting(WinterAspen)
            SetSetting(WinterPine1)
            SetSetting(WinterPine2)
        End If

    End Sub

    Private Sub None_CheckedChanged(sender As Object, e As EventArgs) Handles None.CheckedChanged

        If None.Checked Then
            All.Checked = False

            ClrSetting(BeachGrass)
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
            ClrSetting(Undergrowth)
            ClrSetting(WinterAspen)
            ClrSetting(WinterPine1)
            ClrSetting(WinterPine2)
        End If

    End Sub

    Private Sub Revert_Click(sender As Object, e As EventArgs) Handles RevertButton.Click

        Dim name = ChooseRegion(True)
        Dim RegionUUID As String = PropRegionClass.FindRegionByName(name)
        If RegionUUID.Length = 0 Then Return

        Dim backupname = IO.Path.Combine(Settings.OpensimBinPath, "Terrains")
        If IO.File.Exists(backupname) Then
            RPC_Region_Command(RegionUUID, $"terrain load {backupname}")
        End If

    End Sub

#End Region

#Region "Checkboxes"

    Private Sub BeachGrass_CheckedChanged(sender As Object, e As EventArgs) Handles BeachGrass.CheckedChanged
        Dim thing As CheckBox = CType(sender, CheckBox)
        PutSetting(thing.Name, thing.Checked)
    End Sub

    Private Sub ClrSetting(check As CheckBox)
        check.Checked = False
    End Sub

    Private Sub Cypress1_CheckedChanged(sender As Object, e As EventArgs) Handles Cypress1.CheckedChanged
        Dim thing As CheckBox = CType(sender, CheckBox)
        PutSetting(thing.Name, thing.Checked)
    End Sub

    Private Sub Cypress1_Hover(sender As Object, e As EventArgs) Handles Cypress1.MouseHover
        PictureBox1.Image = My.Resources.Cypress1
    End Sub

    Private Sub Cypress2_CheckedChanged(sender As Object, e As EventArgs) Handles Cypress2.CheckedChanged
        Dim thing As CheckBox = CType(sender, CheckBox)
        PutSetting(thing.Name, thing.Checked)
    End Sub

    Private Sub Cypress2_Hover(sender As Object, e As EventArgs) Handles Cypress2.MouseHover
        PictureBox1.Image = My.Resources.Cypress2
    End Sub

    Private Sub Eelgrass_CheckedChanged(sender As Object, e As EventArgs) Handles Eelgrass.CheckedChanged
        Dim thing As CheckBox = CType(sender, CheckBox)
        PutSetting(thing.Name, thing.Checked)
    End Sub

    Private Sub Eucalyptus_CheckedChanged(sender As Object, e As EventArgs) Handles Eucalyptus.CheckedChanged
        Dim thing As CheckBox = CType(sender, CheckBox)
        PutSetting(thing.Name, thing.Checked)
    End Sub

    Private Sub Fern_CheckedChanged(sender As Object, e As EventArgs) Handles Fern.CheckedChanged
        Dim thing As CheckBox = CType(sender, CheckBox)
        PutSetting(thing.Name, thing.Checked)
    End Sub

    Private Sub Grass0_CheckedChanged(sender As Object, e As EventArgs) Handles Grass0.CheckedChanged
        Dim thing As CheckBox = CType(sender, CheckBox)
        PutSetting(thing.Name, thing.Checked)
    End Sub

    Private Sub Grass1_CheckedChanged(sender As Object, e As EventArgs) Handles Grass1.CheckedChanged
        Dim thing As CheckBox = CType(sender, CheckBox)
        PutSetting(thing.Name, thing.Checked)
    End Sub

    Private Sub Grass2_CheckedChanged(sender As Object, e As EventArgs) Handles Grass2.CheckedChanged
        Dim thing As CheckBox = CType(sender, CheckBox)
        PutSetting(thing.Name, thing.Checked)
    End Sub

    Private Sub Grass3_CheckedChanged(sender As Object, e As EventArgs) Handles Grass3.CheckedChanged
        Dim thing As CheckBox = CType(sender, CheckBox)
        PutSetting(thing.Name, thing.Checked)
    End Sub

    Private Sub Grass4_CheckedChanged(sender As Object, e As EventArgs) Handles Grass4.CheckedChanged
        Dim thing As CheckBox = CType(sender, CheckBox)
        PutSetting(thing.Name, thing.Checked)
    End Sub

    Private Sub Kelp1_CheckedChanged(sender As Object, e As EventArgs) Handles Kelp1.CheckedChanged
        Dim thing As CheckBox = CType(sender, CheckBox)
        PutSetting(thing.Name, thing.Checked)
    End Sub

    Private Sub Kelp2_CheckedChanged(sender As Object, e As EventArgs) Handles Kelp2.CheckedChanged
        Dim thing As CheckBox = CType(sender, CheckBox)
        PutSetting(thing.Name, thing.Checked)
    End Sub

    Private Sub Oak_CheckedChanged(sender As Object, e As EventArgs) Handles Oak.CheckedChanged
        Dim thing As CheckBox = CType(sender, CheckBox)
        PutSetting(thing.Name, thing.Checked)
    End Sub

    Private Sub Palm1_CheckedChanged(sender As Object, e As EventArgs) Handles Palm1.CheckedChanged
        Dim thing As CheckBox = CType(sender, CheckBox)
        PutSetting(thing.Name, thing.Checked)
    End Sub

    Private Sub Palm2_CheckedChanged(sender As Object, e As EventArgs) Handles Palm2.CheckedChanged
        Dim thing As CheckBox = CType(sender, CheckBox)
        PutSetting(thing.Name, thing.Checked)
    End Sub

    Private Sub Pine1_CheckedChanged(sender As Object, e As EventArgs) Handles Pine1.CheckedChanged
        Dim thing As CheckBox = CType(sender, CheckBox)
        PutSetting(thing.Name, thing.Checked)
    End Sub

    Private Sub Pine2_CheckedChanged(sender As Object, e As EventArgs) Handles Pine2.CheckedChanged
        Dim thing As CheckBox = CType(sender, CheckBox)
        PutSetting(thing.Name, thing.Checked)
    End Sub

    Private Sub Plumeria_CheckedChanged(sender As Object, e As EventArgs) Handles Plumeria.CheckedChanged
        Dim thing As CheckBox = CType(sender, CheckBox)
        PutSetting(thing.Name, thing.Checked)
    End Sub

    Private Sub SeaSword_CheckedChanged(sender As Object, e As EventArgs) Handles SeaSword.CheckedChanged
        Dim thing As CheckBox = CType(sender, CheckBox)
        PutSetting(thing.Name, thing.Checked)
    End Sub

    Private Sub SetSetting(check As CheckBox)
        check.Checked = True
    End Sub

    Private Sub TropicalBush1_CheckedChanged(sender As Object, e As EventArgs) Handles TropicalBush1.CheckedChanged
        Dim thing As CheckBox = CType(sender, CheckBox)
        PutSetting(thing.Name, thing.Checked)
    End Sub

    Private Sub TropicalBush2_CheckedChanged(sender As Object, e As EventArgs) Handles TropicalBush2.CheckedChanged
        Dim thing As CheckBox = CType(sender, CheckBox)
        PutSetting(thing.Name, thing.Checked)
    End Sub

    Private Sub Undergrowth_CheckedChanged(sender As Object, e As EventArgs) Handles Undergrowth.CheckedChanged
        Dim thing As CheckBox = CType(sender, CheckBox)
        PutSetting(thing.Name, thing.Checked)
    End Sub

    Private Sub WinterAspen_CheckedChanged(sender As Object, e As EventArgs) Handles WinterAspen.CheckedChanged
        Dim thing As CheckBox = CType(sender, CheckBox)
        PutSetting(thing.Name, thing.Checked)
    End Sub

    Private Sub WinterPine1_CheckedChanged(sender As Object, e As EventArgs) Handles WinterPine1.CheckedChanged
        Dim thing As CheckBox = CType(sender, CheckBox)
        PutSetting(thing.Name, thing.Checked)
    End Sub

    Private Sub WinterPine2_CheckedChanged(sender As Object, e As EventArgs) Handles WinterPine2.CheckedChanged
        Dim thing As CheckBox = CType(sender, CheckBox)
        PutSetting(thing.Name, thing.Checked)
    End Sub

#End Region

End Class
