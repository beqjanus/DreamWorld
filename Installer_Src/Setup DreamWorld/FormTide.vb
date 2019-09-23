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

Public Class Tides

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

        TideEnabledCheckbox.Checked = CType(Form1.PropMySetting.TideEnabled, Boolean)
        TideHighLevelTextBox.Text = Form1.PropMySetting.TideHighLevel()
        TideLowLevelTextBox.Text = Form1.PropMySetting.TideLowLevel()
        CycleTimeTextBox.Text = Form1.PropMySetting.CycleTime.ToString(Form1.InVarient)
        BroadcastTideInfo.Checked = CType(Form1.PropMySetting.BroadcastTideInfo, Boolean)
        TideInfoChannelTextBox.Text = Form1.PropMySetting.TideInfoChannel
        TideHiLoChannelTextBox.Text = Form1.PropMySetting.TideLevelChannel
        TideInfoDebugCheckBox.Checked = Form1.PropMySetting.TideInfoDebug
        SetScreen()
        Form1.HelpOnce("Tides")
    End Sub

    Private Sub IsClosed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Closed

        Form1.PropViewedSettings = True
        Form1.PropMySetting.SaveSettings()

    End Sub

    Private Sub TideEnabledCheckbox_CheckedChanged(sender As Object, e As EventArgs) Handles TideEnabledCheckbox.CheckedChanged
        Form1.PropMySetting.TideEnabled = TideEnabledCheckbox.Checked
        Form1.PropMySetting.SaveINI()
    End Sub

    Private Sub TideHghLevelTextBox_TextChanged(sender As Object, e As EventArgs) Handles TideHighLevelTextBox.TextChanged
        Form1.PropMySetting.TideHighLevel() = TideHighLevelTextBox.Text
        Form1.PropMySetting.SaveINI()
    End Sub

    Private Sub TideLowLevelTextBox_TextChanged(sender As Object, e As EventArgs) Handles TideLowLevelTextBox.TextChanged
        Form1.PropMySetting.TideLowLevel() = TideLowLevelTextBox.Text
        Form1.PropMySetting.SaveINI()
    End Sub

    Private Sub CycleTimeTextBox_TextChanged(sender As Object, e As EventArgs) Handles CycleTimeTextBox.TextChanged
        Form1.PropMySetting.CycleTime = CycleTimeTextBox.Text
        Form1.PropMySetting.SaveINI()
    End Sub

    Private Sub BroadcastTideInfo_CheckedChanged(sender As Object, e As EventArgs) Handles BroadcastTideInfo.CheckedChanged
        Form1.PropMySetting.BroadcastTideInfo = BroadcastTideInfo.Checked
        Form1.PropMySetting.SaveINI()
    End Sub

    Private Sub TideInfoChannelTextBox_TextChanged(sender As Object, e As EventArgs) Handles TideInfoChannelTextBox.TextChanged
        Form1.PropMySetting.TideInfoChannel = TideInfoChannelTextBox.Text
        Form1.PropMySetting.SaveINI()
    End Sub

    Private Sub TideHiLoChannelTextBox_TextChanged(sender As Object, e As EventArgs) Handles TideHiLoChannelTextBox.TextChanged
        Form1.PropMySetting.TideLevelChannel = TideHiLoChannelTextBox.Text
        Form1.PropMySetting.SaveINI()
    End Sub

    Private Sub TideInfoDebugCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles TideInfoDebugCheckBox.CheckedChanged
        Form1.PropMySetting.TideInfoDebug = TideInfoDebugCheckBox.Checked
        Form1.PropMySetting.SaveINI()
    End Sub

    Private Sub RunOnBoot_Click(sender As Object, e As EventArgs) Handles RunOnBoot.Click
        Form1.Help("Tides")
    End Sub

    Private Sub DatabaseSetupToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DatabaseSetupToolStripMenuItem.Click
        Form1.Help("Tides")
    End Sub

End Class