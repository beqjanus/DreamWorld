#Region "Copyright AGPL3.0"

' Copyright Outworldz, LLC.
' AGPL3.0  https://opensource.org/licenses/AGPL

#End Region

Imports System.Text.RegularExpressions

Public Class FormBird

#Region "Private Fields"

    Dim initted As Boolean

#End Region

#Region "ScreenSize"

    Private ReadOnly Handler As New EventHandler(AddressOf Resize_page)

    'The following detects  the location of the form in screen coordinates
    Private _screenPosition As ClassScreenpos

    Public Property ScreenPosition As ClassScreenpos
        Get
            Return _screenPosition
        End Get
        Set(value As ClassScreenpos)
            _screenPosition = value
        End Set
    End Property

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

#Region "Load"

    Private Sub Form1_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed

        If Not initted Then Return

        Settings.BirdsFlockSize = CInt("0" & BirdsFlockSize.Text)
        Settings.SaveSettings()

    End Sub

    Private Sub Loaded(sender As Object, e As EventArgs) Handles Me.Load

        Text = Global.Outworldz.My.Resources.Bird_Module_word
        HelpMenuItem.Image = Global.Outworldz.My.Resources.question_and_answer
        HelpMenuItem.Text = Global.Outworldz.My.Resources.Help_word
        GroupBox1.Text = Global.Outworldz.My.Resources.Bird_Module_word

        BirdsModuleEnable.Checked = Settings.BirdsModuleStartup
        BirdsModuleEnable.Text = Global.Outworldz.My.Resources.Enable_Birds_word
        ToolTip1.SetToolTip(BirdsModuleEnable, Global.Outworldz.My.Resources.Determines)

        BirdsFlockSize.Sorted = False
        Dim i As Integer
        For i = 1 To 100
            BirdsFlockSize.Items.Add(i.ToString(Globalization.CultureInfo.InvariantCulture))
        Next
        BirdsFlockSize.SelectedIndex = Settings.BirdsFlockSize - 1
        FlockLabel.Text = Global.Outworldz.My.Resources.Bird_Flock_Size_word
        ToolTip1.SetToolTip(FlockLabel, Global.Outworldz.My.Resources.Num_Birds)
        ToolTip1.SetToolTip(BirdsFlockSize, Global.Outworldz.My.Resources.tt_BirdsFlockSize)

        ChatChanelTextBox.Text = Settings.BirdsChatChannel.ToString(Globalization.CultureInfo.InvariantCulture)
        ChatLabel.Text = Global.Outworldz.My.Resources.Chat_Channel_word
        ToolTip1.SetToolTip(ChatChanelTextBox, Global.Outworldz.My.Resources.Which_Channel)
        ToolTip1.SetToolTip(ChatLabel, Global.Outworldz.My.Resources.Which_Channel)

        MaxSpeedTextBox.Text = Settings.BirdsMaxSpeed.ToString(Globalization.CultureInfo.InvariantCulture)
        MaxSpeedLabel.Text = Global.Outworldz.My.Resources.Max_Speed
        ToolTip1.SetToolTip(MaxSpeedTextBox, Global.Outworldz.My.Resources.How_Far_Travel)
        ToolTip1.SetToolTip(MaxSpeedLabel, Global.Outworldz.My.Resources.How_Far_Travel)

        MaxForceTextBox.Text = Settings.BirdsMaxForce.ToString(Globalization.CultureInfo.InvariantCulture)
        maxForceLabel.Text = Global.Outworldz.My.Resources.Max_Force
        ToolTip1.SetToolTip(MaxForceTextBox, Global.Outworldz.My.Resources.How_Far_Travel)
        ToolTip1.SetToolTip(maxForceLabel, Global.Outworldz.My.Resources.How_Far_Travel)

        BirdsNeighbourDistanceTextBox.Text = Settings.BirdsNeighbourDistance.ToString(Globalization.CultureInfo.InvariantCulture)
        NeighborLabel.Text = Global.Outworldz.My.Resources.Neighbor_Distance
        ToolTip1.SetToolTip(BirdsNeighbourDistanceTextBox, Global.Outworldz.My.Resources.Max_Dist)
        ToolTip1.SetToolTip(NeighborLabel, Global.Outworldz.My.Resources.Max_Dist)

        DesiredSeparationTextBox.Text = Settings.BirdsDesiredSeparation.ToString(Globalization.CultureInfo.InvariantCulture)
        SeparationLabel.Text = Global.Outworldz.My.Resources.Desired_Separation_word
        ToolTip1.SetToolTip(SeparationLabel, Global.Outworldz.My.Resources.How_Far)
        ToolTip1.SetToolTip(DesiredSeparationTextBox, Global.Outworldz.My.Resources.How_Far)

        BirdsToleranceTextBox.Text = Settings.BirdsTolerance.ToString(Globalization.CultureInfo.InvariantCulture)
        ToleranceLabel.Text = Global.Outworldz.My.Resources.Tolerance
        ToolTip1.SetToolTip(BirdsToleranceTextBox, Global.Outworldz.My.Resources.Tolerance)
        ToolTip1.SetToolTip(ToleranceLabel, Global.Outworldz.My.Resources.Tolerance)

        BirdsBorderSizeTextBox.Text = Settings.BirdsBorderSize.ToString(Globalization.CultureInfo.InvariantCulture)
        BorderLabel.Text = Global.Outworldz.My.Resources.Border_Size
        ToolTip1.SetToolTip(BirdsBorderSizeTextBox, Global.Outworldz.My.Resources.tt_Bird_Border_size)
        ToolTip1.SetToolTip(BorderLabel, Global.Outworldz.My.Resources.tt_Bird_Border_size)

        BirdsMaxHeightTextBox.Text = Settings.BirdsMaxHeight.ToString(Globalization.CultureInfo.InvariantCulture)
        MaxHLabel.Text = Global.Outworldz.My.Resources.Max_Height
        ToolTip1.SetToolTip(BirdsMaxHeightTextBox, Global.Outworldz.My.Resources.tt_BirdsMaxHeightTextBox)
        ToolTip1.SetToolTip(MaxHLabel, Global.Outworldz.My.Resources.tt_BirdsMaxHeightTextBox)

        PrimNameTextBox.Text = Settings.BirdsPrim
        PrimNameLabel.Text = Global.Outworldz.My.Resources.Prim_Name
        ToolTip1.SetToolTip(PrimNameTextBox, Global.Outworldz.My.Resources.Prim_Name)
        ToolTip1.SetToolTip(PrimNameLabel, Global.Outworldz.My.Resources.Prim_Name)

        LoadIARButton.Text = Global.Outworldz.My.Resources.Load_Bird_IAR_word
        ToolTip1.SetToolTip(LoadIARButton, Global.Outworldz.My.Resources.Load_Bird_IAR_word)

        SetScreen()

        HelpOnce("Birds")
        initted = True

    End Sub

#End Region

#Region "Start/Stop"

#End Region

#Region "Birds"

    Private Sub BirdHelp_Click(sender As Object, e As EventArgs)

        HelpManual("Birds")

    End Sub

    Private Sub BirdsBorderSizeTextBox_TextChanged(sender As Object, e As EventArgs) Handles BirdsBorderSizeTextBox.TextChanged

        If Not initted Then Return
        Dim digitsOnly = New Regex("[^\d\.]")
        BirdsBorderSizeTextBox.Text = digitsOnly.Replace(BirdsBorderSizeTextBox.Text, "")

        If Not Double.TryParse(BirdsBorderSizeTextBox.Text, Settings.BirdsBorderSize) Then
            MsgBox(My.Resources.Must_be_A_Number, MsgBoxStyle.Information Or MsgBoxStyle.MsgBoxSetForeground)
        End If

    End Sub

    Private Sub BirdsMaxHeightTextBox_TextChanged(sender As Object, e As EventArgs) Handles BirdsMaxHeightTextBox.TextChanged

        If Not initted Then Return
        Dim digitsOnly = New Regex("[^\d\.]")
        BirdsMaxHeightTextBox.Text = digitsOnly.Replace(BirdsMaxHeightTextBox.Text, "")

        If Not Double.TryParse(BirdsMaxHeightTextBox.Text, Settings.BirdsMaxHeight) Then
            MsgBox(My.Resources.Must_be_A_Number, MsgBoxStyle.Information Or MsgBoxStyle.MsgBoxSetForeground)
        End If

    End Sub

    Private Sub BirdsModuleStartupbox_CheckedChanged(sender As Object, e As EventArgs) Handles BirdsModuleEnable.CheckedChanged

        If Not initted Then Return
        If BirdsModuleEnable.Checked Then
            Settings.BirdsModuleStartup = True
        Else
            Settings.BirdsModuleStartup = False
        End If

    End Sub

    Private Sub BirdsNeighbourDistanceTextBox_TextChanged(sender As Object, e As EventArgs) Handles BirdsNeighbourDistanceTextBox.TextChanged

        If Not initted Then Return
        Dim digitsOnly = New Regex("[^\d\.]")
        BirdsNeighbourDistanceTextBox.Text = digitsOnly.Replace(BirdsNeighbourDistanceTextBox.Text, "")

        If Not Double.TryParse(BirdsNeighbourDistanceTextBox.Text, Settings.BirdsNeighbourDistance) Then
            MsgBox(My.Resources.Must_be_A_Number, MsgBoxStyle.Information Or MsgBoxStyle.MsgBoxSetForeground)
        End If

    End Sub

    Private Sub BirdsToleranceTextBox_TextChanged(sender As Object, e As EventArgs) Handles BirdsToleranceTextBox.TextChanged

        If Not initted Then Return
        Dim digitsOnly = New Regex("[^\d\.]")
        BirdsToleranceTextBox.Text = digitsOnly.Replace(BirdsToleranceTextBox.Text, "")
        If Not Double.TryParse(BirdsToleranceTextBox.Text, Settings.BirdsTolerance) Then
            MsgBox(My.Resources.Must_be_A_Number, MsgBoxStyle.Information Or MsgBoxStyle.MsgBoxSetForeground)
        End If

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles LoadIARButton.Click

        Dim thing As String = IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles/IAR/OpenSimBirds.iar")
        LoadIARContent(thing)

    End Sub

    Private Sub ChatChanelTextBox_TextChanged(sender As Object, e As EventArgs) Handles ChatChanelTextBox.TextChanged

        If Not initted Then Return
        Dim digitsOnly = New Regex("[^\d]")
        ChatChanelTextBox.Text = digitsOnly.Replace(ChatChanelTextBox.Text, "")

        If Not Integer.TryParse(ChatChanelTextBox.Text, Settings.BirdsChatChannel) Then
            MsgBox(My.Resources.Must_be_A_Number, MsgBoxStyle.Information Or MsgBoxStyle.MsgBoxSetForeground)
        End If

    End Sub

    Private Sub DesiredSeparationTextBox_TextChanged(sender As Object, e As EventArgs) Handles DesiredSeparationTextBox.TextChanged

        If Not initted Then Return
        Dim digitsOnly = New Regex("[^\d\.]")
        DesiredSeparationTextBox.Text = digitsOnly.Replace(DesiredSeparationTextBox.Text, "")

        If Double.TryParse(DesiredSeparationTextBox.Text, Settings.BirdsDesiredSeparation) Then
            MsgBox(My.Resources.Must_be_A_Number, MsgBoxStyle.Information Or MsgBoxStyle.MsgBoxSetForeground)
        End If

    End Sub

    Private Sub MaxForceTextBox_TextChanged(sender As Object, e As EventArgs) Handles MaxForceTextBox.TextChanged

        Dim digitsOnly = New Regex("[^\d\.]")
        MaxForceTextBox.Text = digitsOnly.Replace(MaxForceTextBox.Text, "")

        If Not Double.TryParse(MaxForceTextBox.Text, Settings.BirdsMaxForce) Then
            MsgBox(My.Resources.Must_be_A_Number, MsgBoxStyle.Information Or MsgBoxStyle.MsgBoxSetForeground)
        End If

    End Sub

    Private Sub MaxSpeedTextBox_TextChanged(sender As Object, e As EventArgs) Handles MaxSpeedTextBox.TextChanged

        If Not initted Then Return
        Dim digitsOnly = New Regex("[^\d\.]")
        MaxSpeedTextBox.Text = digitsOnly.Replace(MaxSpeedTextBox.Text, "")

        If Not Double.TryParse(MaxSpeedTextBox.Text, Settings.BirdsMaxSpeed) Then
            MsgBox(My.Resources.Must_be_A_Number, MsgBoxStyle.Information Or MsgBoxStyle.MsgBoxSetForeground)
        End If

    End Sub

    Private Sub PrimNameTextBox_TextChanged(sender As Object, e As EventArgs) Handles PrimNameTextBox.TextChanged

        If Not initted Then Return
        Settings.BirdsPrim = PrimNameTextBox.Text

    End Sub

    Private Sub ToolStripMenuItem30_Click(sender As Object, e As EventArgs) Handles HelpMenuItem.Click
        HelpManual("Birds")
    End Sub

#End Region

End Class
