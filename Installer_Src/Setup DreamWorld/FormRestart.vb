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

Public Class FormRestart

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

        Settings.AutoRestartEnabled = ARTimerBox.Checked
        Settings.Autostart = AutoStartCheckbox.Checked
        Try
            Settings.AutoRestartInterval = Convert.ToInt16(AutoRestartBox.Text, Globalization.CultureInfo.InvariantCulture)
#Disable Warning CA1031
        Catch
#Enable Warning CA1031
            Settings.AutoRestartInterval = 0
        End Try

        Settings.RestartOnCrash = RestartOnCrash.Checked
        Settings.Sequential = SequentialCheckBox1.Checked

        Form1.PropViewedSettings = True
        Settings.SaveSettings()

    End Sub

    Private Sub Loaded(sender As Object, e As EventArgs) Handles Me.Load

        AutoRestartBox.Text = Settings.AutoRestartInterval.ToString(Globalization.CultureInfo.InvariantCulture)

        ARTimerBox.Checked = Settings.AutoRestartEnabled

        AutoStartCheckbox.Checked = Settings.Autostart
        SequentialCheckBox1.Checked = Settings.Sequential

        ' Restart on Physics crash is only good for Bullet  in a thread as if it crashed any other way, 
        ' it will restart under AutoRestart

        Debug.Print(Settings.Physics)
        If Settings.Physics <> 4 Then
            RestartOnCrash.Checked = False
        Else
            RestartOnCrash.Checked = True
        End If

        SetScreen()
        Form1.HelpOnce("Restart")

        initted = True ' suppress the install of the startup on formload

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

#Region "AutoStart"

    Private Sub ARTimerBox_CheckedChanged(sender As Object, e As EventArgs) Handles ARTimerBox.CheckedChanged

        If Not initted Then Return
        If ARTimerBox.Checked Then
            Dim BTime As Int16 = CType(Settings.AutobackupInterval, Int16)
            If Settings.AutoBackup And Settings.AutoRestartInterval > 0 And Settings.AutoRestartInterval < BTime Then
                Settings.AutoRestartInterval = BTime + 30
                AutoRestartBox.Text = (BTime + 30).ToString(Globalization.CultureInfo.InvariantCulture)
                MsgBox(My.Resources.Increasing_time & " " & BTime.ToString(Globalization.CultureInfo.InvariantCulture) & " + 30 Minutes for Autobackup to complete.", vbInformation)
            End If

        Else
            Settings.AutoRestartEnabled = False
            Settings.AutoRestartInterval = 0
        End If
        Settings.SaveSettings()

    End Sub

    Private Sub AutoRestartBox_TextChanged(sender As Object, e As EventArgs) Handles AutoRestartBox.TextChanged

        If Not initted Then Return
        Dim digitsOnly As Regex = New Regex("[^\d]")
        AutoRestartBox.Text = digitsOnly.Replace(AutoRestartBox.Text, "")

    End Sub



    Private Sub DatabaseSetupToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DatabaseSetupToolStripMenuItem.Click

        Form1.Help("Restart")

    End Sub


    Private Sub RunOnBoot_Click_1(sender As Object, e As EventArgs) Handles RunOnBoot.Click

        Form1.Help("Restart")

    End Sub



#End Region

End Class
