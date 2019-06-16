Imports System.Text.RegularExpressions

Public Class BirdForm
    Dim changed As Boolean = False

#Region "ScreenSize"

    Private _screenPosition As ScreenPos
    Private Handler As New EventHandler(AddressOf Resize_page)

    Public Property ScreenPosition As ScreenPos
        Get
            Return _screenPosition
        End Get
        Set(value As ScreenPos)
            _screenPosition = value
        End Set
    End Property

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

    Private Sub Loaded(sender As Object, e As EventArgs) Handles Me.Load

        ' This call is required by the designer.
        'InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        SetScreen()

        BirdsModuleStartupbox.Checked = Form1.MySetting.BirdsModuleStartup

        BirdsFlockSizeDomain.Sorted = False

        Dim i As Integer
        For i = 1 To 100
            BirdsFlockSizeDomain.Items.Add(i.ToString(Form1.usa))
        Next
        BirdsFlockSizeDomain.SelectedIndex = CType(Form1.MySetting.BirdsFlockSize, Integer) - 1

        ChatChanelTextBox.Text = Form1.MySetting.BirdsChatChannel.ToString(Form1.usa)
        MaxSpeedTextBox.Text = Form1.MySetting.BirdsMaxSpeed.ToString(Form1.usa)
        MaxForceTextBox.Text = Form1.MySetting.BirdsMaxForce.ToString(Form1.usa)
        BirdsNeighbourDistanceTextBox.Text = Form1.MySetting.BirdsNeighbourDistance.ToString(Form1.usa)
        DesiredSeparationTextBox.Text = Form1.MySetting.BirdsDesiredSeparation.ToString(Form1.usa)
        BirdsToleranceTextBox.Text = Form1.MySetting.BirdsTolerance.ToString(Form1.usa)
        BirdsBorderSizeTextBox.Text = Form1.MySetting.BirdsBorderSize.ToString(Form1.usa)
        BirdsMaxHeightTextBox.Text = Form1.MySetting.BirdsMaxHeight.ToString(Form1.usa)
        PrimNameTextBox.Text = Form1.MySetting.BirdsPrim

        Form1.HelpOnce("Birds")

    End Sub

    Private Sub Form1_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        If changed Then
            MsgBox("These changes go into effect only when Opensim and Robust are both restarted", vbInformation)
        End If
        Form1.MySetting.BirdsFlockSize = BirdsFlockSizeDomain.Text
        Form1.MySetting.SaveSettings()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim thing As String = Form1.MyFolder + "/Outworldzfiles/IAR/OpenSimBirds.iar"
        Form1.LoadIARContent(thing)

    End Sub

    Private Sub BirdHelp_Click(sender As Object, e As EventArgs) Handles BirdHelp.Click

        Form1.Help("Birds")

    End Sub

    Private Sub BirdsModuleStartupbox_CheckedChanged(sender As Object, e As EventArgs) Handles BirdsModuleStartupbox.CheckedChanged

        If BirdsModuleStartupbox.Checked Then
            Form1.MySetting.BirdsModuleStartup = True
        Else
            Form1.MySetting.BirdsModuleStartup = False
        End If
        changed = True

    End Sub

    Private Sub ChatChanelTextBox_TextChanged(sender As Object, e As EventArgs) Handles ChatChanelTextBox.TextChanged

        Dim digitsOnly As Regex = New Regex("[^\d]")
        ChatChanelTextBox.Text = digitsOnly.Replace(ChatChanelTextBox.Text, "")
        Try
            Form1.MySetting.BirdsChatChannel = CInt(ChatChanelTextBox.Text)
            changed = True
        Catch ex As Exception
            MsgBox(ex.Message, vbInformation)
        End Try

    End Sub

    Private Sub MaxSpeedTextBox_TextChanged(sender As Object, e As EventArgs) Handles MaxSpeedTextBox.TextChanged

        Dim digitsOnly As Regex = New Regex("[^\d\.]")
        MaxSpeedTextBox.Text = digitsOnly.Replace(MaxSpeedTextBox.Text, "")
        Try
            Form1.MySetting.BirdsMaxSpeed = Convert.ToDouble(MaxSpeedTextBox.Text, Form1.usa)
            changed = True
        Catch ex As Exception
            MsgBox(ex.Message, vbInformation)
        End Try

    End Sub

    Private Sub MaxForceTextBox_TextChanged(sender As Object, e As EventArgs) Handles MaxForceTextBox.TextChanged

        Dim digitsOnly As Regex = New Regex("[^\d\.]")
        MaxForceTextBox.Text = digitsOnly.Replace(MaxForceTextBox.Text, "")
        Try
            Form1.MySetting.BirdsMaxForce = Convert.ToDouble(MaxForceTextBox.Text, Form1.usa)
            changed = True
        Catch ex As Exception
            MsgBox(ex.Message, vbInformation)
        End Try

    End Sub

    Private Sub BirdsNeighbourDistanceTextBox_TextChanged(sender As Object, e As EventArgs) Handles BirdsNeighbourDistanceTextBox.TextChanged

        Dim digitsOnly As Regex = New Regex("[^\d\.]")
        BirdsNeighbourDistanceTextBox.Text = digitsOnly.Replace(BirdsNeighbourDistanceTextBox.Text, "")
        Try
            Form1.MySetting.BirdsNeighbourDistance = Convert.ToDouble(BirdsNeighbourDistanceTextBox.Text, Form1.usa)
            changed = True
        Catch ex As Exception
            MsgBox(ex.Message, vbInformation)
        End Try

    End Sub

    Private Sub DesiredSeparationTextBox_TextChanged(sender As Object, e As EventArgs) Handles DesiredSeparationTextBox.TextChanged

        Dim digitsOnly As Regex = New Regex("[^\d\.]")
        DesiredSeparationTextBox.Text = digitsOnly.Replace(DesiredSeparationTextBox.Text, "")
        Try
            Form1.MySetting.BirdsDesiredSeparation = Convert.ToDouble(DesiredSeparationTextBox.Text, Form1.usa)
            changed = True
        Catch ex As Exception
            MsgBox(ex.Message, vbInformation)
        End Try

    End Sub

    Private Sub BirdsToleranceTextBox_TextChanged(sender As Object, e As EventArgs) Handles BirdsToleranceTextBox.TextChanged

        Dim digitsOnly As Regex = New Regex("[^\d\.]")
        BirdsToleranceTextBox.Text = digitsOnly.Replace(BirdsToleranceTextBox.Text, "")
        Try
            Form1.MySetting.BirdsTolerance = Convert.ToDouble(BirdsToleranceTextBox.Text, Form1.usa)
            changed = True
        Catch ex As Exception
            MsgBox(ex.Message, vbInformation)
        End Try

    End Sub

    Private Sub BirdsBorderSizeTextBox_TextChanged(sender As Object, e As EventArgs) Handles BirdsBorderSizeTextBox.TextChanged

        Dim digitsOnly As Regex = New Regex("[^\d\.]")
        BirdsBorderSizeTextBox.Text = digitsOnly.Replace(BirdsBorderSizeTextBox.Text, "")
        Try
            Form1.MySetting.BirdsBorderSize = Convert.ToDouble(BirdsBorderSizeTextBox.Text, Form1.usa)
            changed = True
        Catch ex As Exception
            MsgBox(ex.Message, vbInformation)
        End Try

    End Sub

    Private Sub BirdsMaxHeightTextBox_TextChanged(sender As Object, e As EventArgs) Handles BirdsMaxHeightTextBox.TextChanged

        Dim digitsOnly As Regex = New Regex("[^\d\.]")
        BirdsMaxHeightTextBox.Text = digitsOnly.Replace(BirdsMaxHeightTextBox.Text, "")
        Try
            Form1.MySetting.BirdsMaxHeight = Convert.ToDouble(BirdsMaxHeightTextBox.Text, Form1.usa)
            changed = True
        Catch ex As Exception
            MsgBox(ex.Message, vbInformation)
        End Try

    End Sub

    Private Sub PrimNameTextBox_TextChanged(sender As Object, e As EventArgs) Handles PrimNameTextBox.TextChanged

        Form1.MySetting.BirdsPrim = PrimNameTextBox.Text
        changed = True

    End Sub

End Class