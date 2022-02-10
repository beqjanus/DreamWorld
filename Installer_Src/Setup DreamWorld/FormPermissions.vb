#Region "Copyright AGPL3.0"

' Copyright Outworldz, LLC.
' AGPL3.0  https://opensource.org/licenses/AGPL

#End Region

Public Class FormPermissions

#Region "Private Fields"

    Private ReadOnly Handler As New EventHandler(AddressOf Resize_page)
    Private _screenPosition As ClassScreenpos
    Dim initted As Boolean

#End Region

#Region "Public Properties"

    Public Property ScreenPosition As ClassScreenpos
        Get
            Return _screenPosition
        End Get
        Set(value As ClassScreenpos)
            _screenPosition = value
        End Set
    End Property

#End Region

#Region "Private Methods"

    Private Sub IsClosed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Closed

        Settings.SaveSettings()

    End Sub

    Private Sub Loaded(sender As Object, e As EventArgs) Handles Me.Load

        AllowGods.Text = Global.Outworldz.My.Resources.Allow_Or_Disallow_Gods_word
        Clouds.Text = Global.Outworldz.My.Resources.Enable_word
        EnableMaxPrims.Text = Global.Outworldz.My.Resources.Max_Prims
        GroupBox1.Text = Global.Outworldz.My.Resources.Export_Permission_word '"Export Permission"
        GroupBox4.Text = Global.Outworldz.My.Resources.Permissions_word '"Permissions"
        CloudBox.Text = Global.Outworldz.My.Resources.Clouds_word '"Clouds"
        LimitsBox.Text = Global.Outworldz.My.Resources.Prim_Limits '"Prim Limits"
        ManagerGod.Text = Global.Outworldz.My.Resources.Region_manager_god
        MenuStrip2.Text = Global.Outworldz.My.Resources._0
        OutBoundPermissionsCheckbox.Text = Global.Outworldz.My.Resources.Allow_Items_to_leave_word
        RegionGod.Text = Global.Outworldz.My.Resources.Allow_Region_Owner_Gods_word
        Text = Global.Outworldz.My.Resources.Permissions_word
        ToolStripMenuItem30.Image = Global.Outworldz.My.Resources.question_and_answer
        ToolStripMenuItem30.Text = Global.Outworldz.My.Resources.Help_word
        ToolTip1.SetToolTip(AllowGods, Global.Outworldz.My.Resources.AllowGodsTooltip)
        ToolTip1.SetToolTip(Clouds, Global.Outworldz.My.Resources.Allow_cloud)
        ToolTip1.SetToolTip(EnableMaxPrims, Global.Outworldz.My.Resources.Max_PrimLimit)
        ToolTip1.SetToolTip(ManagerGod, Global.Outworldz.My.Resources.Region_Manager_is_God)
        ToolTip1.SetToolTip(OutBoundPermissionsCheckbox, Global.Outworldz.My.Resources.Allow_objects)
        ToolTip1.SetToolTip(RegionGod, Global.Outworldz.My.Resources.Region_Owner_Is_God_word)

        EnableMaxPrims.Checked = Settings.Primlimits()

        'gods
        AllowGods.Checked = Settings.AllowGridGods
        RegionGod.Checked = Settings.RegionOwnerIsGod
        ManagerGod.Checked = Settings.RegionManagerIsGod
        Clouds.Checked = Settings.Clouds

        Dim var As Double = Settings.Density

        If var = -1 Then var = 5

        Dim v As Integer = CInt("0" & (var * 10))
        If (v > 9) Then v = 9
        If (v < 0) Then v = 0
        DomainUpDown1.SelectedIndex = v

        OutBoundPermissionsCheckbox.Checked = Settings.OutboundPermissions

        SetScreen()
        HelpOnce("Permissions")
        initted = True

    End Sub

    'The following detects  the location of the form in screen coordinates
    Private Sub Resize_page(ByVal sender As Object, ByVal e As System.EventArgs)
        'Me.Text = "Form screen position = " + Me.Location.ToString
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

#Region "Subs"

    Private Sub AllowGods_CheckedChanged(sender As Object, e As EventArgs) Handles AllowGods.CheckedChanged

        If Not initted Then Return
        Settings.AllowGridGods = AllowGods.Checked
        Settings.SaveSettings()

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles Clouds.CheckedChanged

        If Not initted Then Return
        Settings.Clouds = Clouds.Checked
        Settings.SaveSettings()

    End Sub

    Private Sub DomainUpDown1_SelectedItemChanged(sender As Object, e As EventArgs) Handles DomainUpDown1.SelectedItemChanged

        If initted Then
            Try
                Dim var As Double = CType(DomainUpDown1.SelectedIndex, Double)

                If var = -1 Then var = 0.5
                var /= 10
                If (var > 1) Then var = 1
                If (var < 0) Then var = 0
                Debug.Print(var.ToString(Globalization.CultureInfo.InvariantCulture))

                Settings.Density = var
                Settings.SaveSettings()
            Catch
                Settings.Density = 0.5
                Settings.SaveSettings()
            End Try

        End If

    End Sub

    Private Sub EnableMaxPrims_CheckedChanged(sender As Object, e As EventArgs) Handles EnableMaxPrims.CheckedChanged

        If initted Then
            Settings.Primlimits() = EnableMaxPrims.Checked
            Settings.SaveSettings()
        End If

    End Sub

    Private Sub HGExportCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles OutBoundPermissionsCheckbox.CheckedChanged

        If Not initted Then Return
        Settings.OutboundPermissions = OutBoundPermissionsCheckbox.Checked
        Settings.SaveSettings()

    End Sub

    Private Sub ManagerGod_CheckedChanged_1(sender As Object, e As EventArgs) Handles ManagerGod.CheckedChanged

        If Not initted Then Return
        Settings.RegionManagerIsGod = ManagerGod.Checked
        Settings.SaveSettings()

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs)

        HelpManual("Permissions")

    End Sub

    Private Sub RegionGod_CheckedChanged_1(sender As Object, e As EventArgs) Handles RegionGod.CheckedChanged

        If Not initted Then Return
        Settings.RegionOwnerIsGod = RegionGod.Checked
        Settings.SaveSettings()

    End Sub

    Private Sub ToolStripMenuItem30_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem30.Click
        HelpManual("Permissions")
    End Sub

#End Region

End Class
