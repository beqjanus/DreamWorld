Imports System.Text.RegularExpressions

Public Class BirdForm

#Region "ScreenSize"
    Public ScreenPosition As ScreenPos
    Private Handler As New EventHandler(AddressOf Resize_page)

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
            BirdsFlockSizeDomain.Items.Add(i.ToString)
        Next
        BirdsFlockSizeDomain.SelectedIndex = CType(Form1.MySetting.BirdsFlockSize, Integer) - 1

        ChatChanelTextBox.Text = Form1.MySetting.BirdsChatChannel.ToString
        MaxSpeedTextBox.Text = Form1.MySetting.BirdsMaxSpeed.ToString
        MaxForceTextBox.Text = Form1.MySetting.BirdsMaxForce.ToString
        BirdsNeighbourDistanceTextBox.Text = Form1.MySetting.BirdsNeighbourDistance.ToString
        DesiredSeparationTextBox.Text = Form1.MySetting.BirdsDesiredSeparation.ToString
        BirdsToleranceTextBox.Text = Form1.MySetting.BirdsTolerance.ToString
        BirdsBorderSizeTextBox.Text = Form1.MySetting.BirdsBorderSize.ToString
        BirdsMaxHeightTextBox.Text = Form1.MySetting.BirdsMaxHeight.ToString
        PrimNameTextBox.Text = Form1.MySetting.BirdsPrim

        Form1.HelpOnce("Birds")

    End Sub

    Private Sub Form1_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed

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

    End Sub

    Private Sub ChatChanelTextBox_TextChanged(sender As Object, e As EventArgs) Handles ChatChanelTextBox.TextChanged

        Dim digitsOnly As Regex = New Regex("[^\d]")
        ChatChanelTextBox.Text = digitsOnly.Replace(ChatChanelTextBox.Text, "")
        Try
            Dim rgx As New Regex("[^0-9]")
            ChatChanelTextBox.Text = rgx.Replace(ChatChanelTextBox.Text, "")
            Form1.MySetting.BirdsChatChannel = CInt(ChatChanelTextBox.Text)
        Catch
        End Try

    End Sub

    Private Sub MaxSpeedTextBox_TextChanged(sender As Object, e As EventArgs) Handles MaxSpeedTextBox.TextChanged

        Dim digitsOnly As Regex = New Regex("[^\d]")
        MaxSpeedTextBox.Text = digitsOnly.Replace(MaxSpeedTextBox.Text, "")

        Try
            Dim rgx As New Regex("[^0-9]")
            MaxSpeedTextBox.Text = rgx.Replace(MaxSpeedTextBox.Text, "")
            Form1.MySetting.BirdsMaxSpeed = Convert.ToDouble(MaxSpeedTextBox.Text)
        Catch
        End Try

    End Sub

    Private Sub MaxForceTextBox_TextChanged(sender As Object, e As EventArgs) Handles MaxForceTextBox.TextChanged

        Dim digitsOnly As Regex = New Regex("[^\d]")
        MaxForceTextBox.Text = digitsOnly.Replace(MaxForceTextBox.Text, "")
        Try
            Form1.MySetting.BirdsMaxForce = Convert.ToDouble(MaxForceTextBox.Text)
        Catch
        End Try

    End Sub

    Private Sub BirdsNeighbourDistanceTextBox_TextChanged(sender As Object, e As EventArgs) Handles BirdsNeighbourDistanceTextBox.TextChanged

        Dim digitsOnly As Regex = New Regex("[^\d]")
        BirdsNeighbourDistanceTextBox.Text = digitsOnly.Replace(BirdsNeighbourDistanceTextBox.Text, "")
        Try
            Dim rgx As New Regex("[^0-9]")
            BirdsNeighbourDistanceTextBox.Text = rgx.Replace(BirdsNeighbourDistanceTextBox.Text, "")
            Form1.MySetting.BirdsNeighbourDistance = Convert.ToDouble(BirdsNeighbourDistanceTextBox.Text)
        Catch
        End Try

    End Sub

    Private Sub DesiredSeparationTextBox_TextChanged(sender As Object, e As EventArgs) Handles DesiredSeparationTextBox.TextChanged

        Dim digitsOnly As Regex = New Regex("[^\d]")
        DesiredSeparationTextBox.Text = digitsOnly.Replace(DesiredSeparationTextBox.Text, "")
        Try
            Dim rgx As New Regex("[^0-9]")
            DesiredSeparationTextBox.Text = rgx.Replace(DesiredSeparationTextBox.Text, "")
            Form1.MySetting.BirdsDesiredSeparation = Convert.ToDouble(DesiredSeparationTextBox.Text)
        Catch
        End Try

    End Sub

    Private Sub BirdsToleranceTextBox_TextChanged(sender As Object, e As EventArgs) Handles BirdsToleranceTextBox.TextChanged

        Dim digitsOnly As Regex = New Regex("[^\d]")
        BirdsToleranceTextBox.Text = digitsOnly.Replace(BirdsToleranceTextBox.Text, "")
        Try
            Dim rgx As New Regex("[^0-9]")
            BirdsToleranceTextBox.Text = rgx.Replace(BirdsToleranceTextBox.Text, "")
            Form1.MySetting.BirdsTolerance = Convert.ToDouble(BirdsToleranceTextBox.Text)
        Catch
        End Try

    End Sub

    Private Sub BirdsBorderSizeTextBox_TextChanged(sender As Object, e As EventArgs) Handles BirdsBorderSizeTextBox.TextChanged

        Dim digitsOnly As Regex = New Regex("[^\d]")
        BirdsBorderSizeTextBox.Text = digitsOnly.Replace(BirdsBorderSizeTextBox.Text, "")
        Try
            Dim rgx As New Regex("[^0-9]")
            BirdsBorderSizeTextBox.Text = rgx.Replace(BirdsBorderSizeTextBox.Text, "")
            Form1.MySetting.BirdsBorderSize = Convert.ToDouble(BirdsBorderSizeTextBox.Text)
        Catch
        End Try

    End Sub

    Private Sub BirdsMaxHeightTextBox_TextChanged(sender As Object, e As EventArgs) Handles BirdsMaxHeightTextBox.TextChanged

        Dim digitsOnly As Regex = New Regex("[^\d]")
        BirdsMaxHeightTextBox.Text = digitsOnly.Replace(BirdsMaxHeightTextBox.Text, "")
        Try
            Dim rgx As New Regex("[^0-9]")
            BirdsMaxHeightTextBox.Text = rgx.Replace(BirdsMaxHeightTextBox.Text, "")
            Form1.MySetting.BirdsMaxHeight = Convert.ToDouble(BirdsMaxHeightTextBox.Text)
        Catch
        End Try

    End Sub

    Private Sub PrimNameTextBox_TextChanged(sender As Object, e As EventArgs) Handles PrimNameTextBox.TextChanged

        Form1.MySetting.BirdsPrim = PrimNameTextBox.Text

    End Sub



End Class