#Region "Copyright"

' Copyright 2014 Fred Beckhusen for www.Outworldz.com https://opensource.org/licenses/AGPL

'Permission Is hereby granted, free Of charge, to any person obtaining a copy of this software
' And associated documentation files (the "Software"), to deal in the Software without restriction,
'including without limitation the rights To use, copy, modify, merge, publish, distribute, sublicense,
'And/Or sell copies Of the Software, And To permit persons To whom the Software Is furnished To
'Do so, subject To the following conditions:

'The above copyright notice And this permission notice shall be included In all copies Or '
'substantial portions Of the Software.

'THE SOFTWARE Is PROVIDED "AS IS", WITHOUT WARRANTY Of ANY KIND, EXPRESS Or IMPLIED,
' INCLUDING BUT Not LIMITED To THE WARRANTIES Of MERCHANTABILITY, FITNESS For A PARTICULAR
'PURPOSE And NONINFRINGEMENT.In NO Event SHALL THE AUTHORS Or COPYRIGHT HOLDERS BE LIABLE
'For ANY CLAIM, DAMAGES Or OTHER LIABILITY, WHETHER In AN ACTION Of CONTRACT, TORT Or
'OTHERWISE, ARISING FROM, OUT Of Or In CONNECTION With THE SOFTWARE Or THE USE Or OTHER
'DEALINGS IN THE SOFTWARE.Imports System

#End Region

Public Class FormPermissions

#Region "Private Fields"

    Private _screenPosition As ScreenPos
    Private Handler As New EventHandler(AddressOf Resize_page)
    Dim initted As Boolean = False

#End Region

#Region "Public Properties"

    Public Property ScreenPosition As ScreenPos
        Get
            Return _screenPosition
        End Get
        Set(value As ScreenPos)
            _screenPosition = value
        End Set
    End Property

#End Region

#Region "Private Methods"

    Private Sub IsClosed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Closed

        Form1.PropViewedSettings = True
        Form1.Settings.SaveSettings()

    End Sub

    Private Sub Loaded(sender As Object, e As EventArgs) Handles Me.Load

        EnableMaxPrims.Checked = Form1.Settings.Primlimits()

        'gods
        AllowGods.Checked = Form1.Settings.AllowGridGods
        RegionGod.Checked = Form1.Settings.RegionOwnerIsGod
        ManagerGod.Checked = Form1.Settings.RegionManagerIsGod
        Clouds.Checked = Form1.Settings.Clouds
        LSLCheckbox.Checked = Form1.Settings.LSLHTTP()
        Dim var As Double = Form1.Settings.Density

        If var = -1 Then var = 5

        Dim v As Integer = CInt(var * 10)
        If (var > 9) Then v = 9
        If (var < 0) Then v = 0
        DomainUpDown1.SelectedIndex = v

        OutBoundPermissionsCheckbox.Checked = Form1.Settings.OutBoundPermissions

        SetScreen()
        Form1.HelpOnce("Permissions")
        initted = True

    End Sub

    'The following detects  the location of the form in screen coordinates
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

#Region "Subs"

    Private Sub AllowGods_CheckedChanged(sender As Object, e As EventArgs) Handles AllowGods.CheckedChanged

        If Not initted Then Return
        Form1.Settings.AllowGridGods = AllowGods.Checked
        Form1.Settings.SaveSettings()

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles Clouds.CheckedChanged

        If Not initted Then Return
        Form1.Settings.Clouds = Clouds.Checked
        Form1.Settings.SaveSettings()

    End Sub

    Private Sub DatabaseSetupToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DatabaseSetupToolStripMenuItem.Click
        Form1.Help("Permissions")
    End Sub

    Private Sub DomainUpDown1_SelectedItemChanged(sender As Object, e As EventArgs) Handles DomainUpDown1.SelectedItemChanged

        If initted Then
            Dim var As Double = CType(DomainUpDown1.SelectedIndex, Double)

            If var = -1 Then var = 0.5
            var /= 10
            If (var > 1) Then var = 1
            If (var < 0) Then var = 0
            Debug.Print(var.ToString(Form1.Invarient))

            Form1.Settings.Density = var
            Form1.Settings.SaveSettings()
        End If

    End Sub

    Private Sub EnableMaxPrims_CheckedChanged(sender As Object, e As EventArgs) Handles EnableMaxPrims.CheckedChanged

        If initted Then
            Form1.Settings.Primlimits() = EnableMaxPrims.Checked
            Form1.Settings.SaveSettings()
        End If

    End Sub

    Private Sub HGExportCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles OutBoundPermissionsCheckbox.CheckedChanged

        If Not initted Then Return
        Form1.Settings.OutBoundPermissions = OutBoundPermissionsCheckbox.Checked
        Form1.Settings.SaveSettings()

    End Sub

    Private Sub LSLCheckbox_CheckedChanged(sender As Object, e As EventArgs) Handles LSLCheckbox.CheckedChanged

        If initted Then
            Form1.Settings.LSLHTTP() = LSLCheckbox.Checked
            Form1.Settings.SaveSettings()
        End If

    End Sub

    Private Sub ManagerGod_CheckedChanged_1(sender As Object, e As EventArgs) Handles ManagerGod.CheckedChanged

        If Not initted Then Return
        Form1.Settings.RegionManagerIsGod = ManagerGod.Checked
        Form1.Settings.SaveSettings()

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles GodHelp.Click

        Form1.Help("Permissions")

    End Sub

    Private Sub RegionGod_CheckedChanged_1(sender As Object, e As EventArgs) Handles RegionGod.CheckedChanged

        If Not initted Then Return
        Form1.Settings.RegionOwnerIsGod = RegionGod.Checked
        Form1.Settings.SaveSettings()

    End Sub

#End Region

End Class
