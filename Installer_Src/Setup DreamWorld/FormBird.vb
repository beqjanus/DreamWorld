#Region "Copyright"

' Copyright 2014 Fred Beckhusen for outworldz.com https://opensource.org/licenses/AGPL

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

Imports System.Text.RegularExpressions

Public Class BirdForm



#Region "Private Fields"

    Dim changed As Boolean = False
    Dim initted As Boolean = False

#End Region

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



#Region "Private Methods"

    Private Sub BirdHelp_Click(sender As Object, e As EventArgs) Handles BirdHelp.Click

        Form1.Help("Birds")

    End Sub

    Private Sub BirdsBorderSizeTextBox_TextChanged(sender As Object, e As EventArgs) Handles BirdsBorderSizeTextBox.TextChanged

        If Not initted Then Return
        Dim digitsOnly As Regex = New Regex("[^\d\.]")
        BirdsBorderSizeTextBox.Text = digitsOnly.Replace(BirdsBorderSizeTextBox.Text, "")

        If Not Double.TryParse(BirdsBorderSizeTextBox.Text, Settings.BirdsBorderSize) Then
            MsgBox(My.Resources.Must_be_A_Number, vbInformation)
        End If

        changed = True

    End Sub

    Private Sub BirdsMaxHeightTextBox_TextChanged(sender As Object, e As EventArgs) Handles BirdsMaxHeightTextBox.TextChanged

        If Not initted Then Return
        Dim digitsOnly As Regex = New Regex("[^\d\.]")
        BirdsMaxHeightTextBox.Text = digitsOnly.Replace(BirdsMaxHeightTextBox.Text, "")

        If Not Double.TryParse(BirdsMaxHeightTextBox.Text, Settings.BirdsMaxHeight) Then
            MsgBox(My.Resources.Must_be_A_Number, vbInformation)
        End If

        changed = True
    End Sub

    Private Sub BirdsModuleStartupbox_CheckedChanged(sender As Object, e As EventArgs) Handles BirdsModuleStartupbox.CheckedChanged

        If Not initted Then Return
        If BirdsModuleStartupbox.Checked Then
            Settings.BirdsModuleStartup = True
        Else
            Settings.BirdsModuleStartup = False
        End If
        changed = True

    End Sub

    Private Sub BirdsNeighbourDistanceTextBox_TextChanged(sender As Object, e As EventArgs) Handles BirdsNeighbourDistanceTextBox.TextChanged

        If Not initted Then Return
        Dim digitsOnly As Regex = New Regex("[^\d\.]")
        BirdsNeighbourDistanceTextBox.Text = digitsOnly.Replace(BirdsNeighbourDistanceTextBox.Text, "")

        If Not Double.TryParse(BirdsNeighbourDistanceTextBox.Text, Settings.BirdsNeighbourDistance) Then
            MsgBox(My.Resources.Must_be_A_Number, vbInformation)
        End If
        changed = True

    End Sub

    Private Sub BirdsToleranceTextBox_TextChanged(sender As Object, e As EventArgs) Handles BirdsToleranceTextBox.TextChanged

        If Not initted Then Return
        Dim digitsOnly As Regex = New Regex("[^\d\.]")
        BirdsToleranceTextBox.Text = digitsOnly.Replace(BirdsToleranceTextBox.Text, "")
        If Not Double.TryParse(BirdsToleranceTextBox.Text, Settings.BirdsTolerance) Then
            MsgBox(My.Resources.Must_be_A_Number, vbInformation)
        End If
        changed = True
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim thing As String = Form1.PropMyFolder + "/Outworldzfiles/IAR/OpenSimBirds.iar"
        Form1.LoadIARContent(thing)

    End Sub

    Private Sub ChatChanelTextBox_TextChanged(sender As Object, e As EventArgs) Handles ChatChanelTextBox.TextChanged

        If Not initted Then Return
        Dim digitsOnly As Regex = New Regex("[^\d]")
        ChatChanelTextBox.Text = digitsOnly.Replace(ChatChanelTextBox.Text, "")

        If Not Integer.TryParse(ChatChanelTextBox.Text, Settings.BirdsChatChannel) Then
            MsgBox(My.Resources.Must_be_A_Number, vbInformation)
        End If
        changed = True

    End Sub

    Private Sub DatabaseSetupToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DatabaseSetupToolStripMenuItem.Click

        Form1.Help("Birds")

    End Sub

    Private Sub DesiredSeparationTextBox_TextChanged(sender As Object, e As EventArgs) Handles DesiredSeparationTextBox.TextChanged

        If Not initted Then Return
        Dim digitsOnly As Regex = New Regex("[^\d\.]")
        DesiredSeparationTextBox.Text = digitsOnly.Replace(DesiredSeparationTextBox.Text, "")

        If Double.TryParse(DesiredSeparationTextBox.Text, Settings.BirdsDesiredSeparation) Then
            MsgBox(My.Resources.Must_be_A_Number, vbInformation)
        End If
        changed = True

    End Sub

    Private Sub Form1_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed

        If Not initted Then Return
        If changed Then
            Form1.PropViewedSettings = True
        End If
        Settings.BirdsFlockSize = CInt("0" & BirdsFlockSizeDomain.Text)
        Settings.SaveSettings()

    End Sub

    Private Sub Loaded(sender As Object, e As EventArgs) Handles Me.Load

        ' This call is required by the designer. InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        SetScreen()

        BirdsModuleStartupbox.Checked = Settings.BirdsModuleStartup

        BirdsFlockSizeDomain.Sorted = False

        Dim i As Integer
        For i = 1 To 100
            BirdsFlockSizeDomain.Items.Add(i.ToString(Globalization.CultureInfo.InvariantCulture))
        Next
        BirdsFlockSizeDomain.SelectedIndex = Settings.BirdsFlockSize - 1

        ChatChanelTextBox.Text = Settings.BirdsChatChannel.ToString(Globalization.CultureInfo.InvariantCulture)
        MaxSpeedTextBox.Text = Settings.BirdsMaxSpeed.ToString(Globalization.CultureInfo.InvariantCulture)
        MaxForceTextBox.Text = Settings.BirdsMaxForce.ToString(Globalization.CultureInfo.InvariantCulture)
        BirdsNeighbourDistanceTextBox.Text = Settings.BirdsNeighbourDistance.ToString(Globalization.CultureInfo.InvariantCulture)
        DesiredSeparationTextBox.Text = Settings.BirdsDesiredSeparation.ToString(Globalization.CultureInfo.InvariantCulture)
        BirdsToleranceTextBox.Text = Settings.BirdsTolerance.ToString(Globalization.CultureInfo.InvariantCulture)
        BirdsBorderSizeTextBox.Text = Settings.BirdsBorderSize.ToString(Globalization.CultureInfo.InvariantCulture)
        BirdsMaxHeightTextBox.Text = Settings.BirdsMaxHeight.ToString(Globalization.CultureInfo.InvariantCulture)
        PrimNameTextBox.Text = Settings.BirdsPrim

        Form1.HelpOnce("Birds")
        initted = True
    End Sub

    Private Sub MaxForceTextBox_TextChanged(sender As Object, e As EventArgs) Handles MaxForceTextBox.TextChanged

        Dim digitsOnly As Regex = New Regex("[^\d\.]")
        MaxForceTextBox.Text = digitsOnly.Replace(MaxForceTextBox.Text, "")

        If Not Double.TryParse(MaxForceTextBox.Text, Settings.BirdsMaxForce) Then
            MsgBox(My.Resources.Must_be_A_Number, vbInformation)
        End If
        changed = True

    End Sub

    Private Sub MaxSpeedTextBox_TextChanged(sender As Object, e As EventArgs) Handles MaxSpeedTextBox.TextChanged

        If Not initted Then Return
        Dim digitsOnly As Regex = New Regex("[^\d\.]")
        MaxSpeedTextBox.Text = digitsOnly.Replace(MaxSpeedTextBox.Text, "")

        If Not Double.TryParse(MaxSpeedTextBox.Text, Settings.BirdsMaxSpeed) Then
            MsgBox(My.Resources.Must_be_A_Number, vbInformation)
        End If

        changed = True

    End Sub

    Private Sub PrimNameTextBox_TextChanged(sender As Object, e As EventArgs) Handles PrimNameTextBox.TextChanged

        If Not initted Then Return
        Settings.BirdsPrim = PrimNameTextBox.Text
        changed = True

    End Sub

#End Region

End Class
