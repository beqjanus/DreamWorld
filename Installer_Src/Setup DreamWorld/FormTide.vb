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

Public Class Tides

#Region "ScreenSize"

    Private ReadOnly Handler As New EventHandler(AddressOf Resize_page)
    Private _screenPosition As ScreenPos

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

    Private Sub BroadcastTideInfo_CheckedChanged(sender As Object, e As EventArgs) Handles BroadcastTideInfo.CheckedChanged
        Settings.BroadcastTideInfo = BroadcastTideInfo.Checked
        Settings.SaveINI(System.Text.Encoding.UTF8)
    End Sub

    Private Sub CycleTimeTextBox_TextChanged(sender As Object, e As EventArgs) Handles CycleTimeTextBox.TextChanged
        Settings.CycleTime = CType(CycleTimeTextBox.Text, Integer)
        Settings.SaveINI(System.Text.Encoding.UTF8)
    End Sub

    Private Sub DatabaseSetupToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DatabaseSetupToolStripMenuItem.Click
        HelpManual("Tides")
    End Sub

    Private Sub IsClosed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Closed

        Form1.PropViewedSettings = True
        Settings.SaveSettings()

    End Sub

    Private Sub Loaded(sender As Object, e As EventArgs) Handles Me.Load

        TideEnabledCheckbox.Checked = CType(Settings.TideEnabled, Boolean)
        TideHighLevelTextBox.Text = Convert.ToString(Settings.TideHighLevel(), Globalization.CultureInfo.InvariantCulture)
        TideLowLevelTextBox.Text = Convert.ToString(Settings.TideLowLevel(), Globalization.CultureInfo.InvariantCulture)
        CycleTimeTextBox.Text = Settings.CycleTime.ToString(Globalization.CultureInfo.InvariantCulture)
        BroadcastTideInfo.Checked = CType(Settings.BroadcastTideInfo, Boolean)
        TideInfoChannelTextBox.Text = CStr(Settings.TideInfoChannel)
        TideHiLoChannelTextBox.Text = CStr(Settings.TideLevelChannel)
        TideInfoDebugCheckBox.Checked = Settings.TideInfoDebug
        SetScreen()
        HelpOnce("Tides")
    End Sub

    Private Sub RunOnBoot_Click(sender As Object, e As EventArgs) Handles RunOnBoot.Click
        HelpManual("Tides")
    End Sub

    Private Sub TideEnabledCheckbox_CheckedChanged(sender As Object, e As EventArgs) Handles TideEnabledCheckbox.CheckedChanged
        Settings.TideEnabled = TideEnabledCheckbox.Checked
        Settings.SaveINI(System.Text.Encoding.UTF8)
    End Sub

    Private Sub TideHghLevelTextBox_TextChanged(sender As Object, e As EventArgs) Handles TideHighLevelTextBox.TextChanged
        Settings.TideHighLevel() = CType(TideHighLevelTextBox.Text, Single)
        Settings.SaveINI(System.Text.Encoding.UTF8)
    End Sub

    Private Sub TideHiLoChannelTextBox_TextChanged(sender As Object, e As EventArgs) Handles TideHiLoChannelTextBox.TextChanged
        Settings.TideLevelChannel = CType(TideHiLoChannelTextBox.Text, Integer)
        Settings.SaveINI(System.Text.Encoding.UTF8)
    End Sub

    Private Sub TideInfoChannelTextBox_TextChanged(sender As Object, e As EventArgs) Handles TideInfoChannelTextBox.TextChanged
        Settings.TideInfoChannel = CType(TideInfoChannelTextBox.Text, Integer)
        Settings.SaveINI(System.Text.Encoding.UTF8)
    End Sub

    Private Sub TideInfoDebugCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles TideInfoDebugCheckBox.CheckedChanged
        Settings.TideInfoDebug = TideInfoDebugCheckBox.Checked
        Settings.SaveINI(System.Text.Encoding.UTF8)
    End Sub

    Private Sub TideLowLevelTextBox_TextChanged(sender As Object, e As EventArgs) Handles TideLowLevelTextBox.TextChanged
        Settings.TideLowLevel() = CType(TideLowLevelTextBox.Text, Single)
        Settings.SaveINI(System.Text.Encoding.UTF8)
    End Sub

#End Region

End Class
