Imports System.Text.RegularExpressions

Public Class FormSmartStart

#Region "ScreenSize"

    Private ReadOnly Handler As New EventHandler(AddressOf Resize_page)

    'The following detects  the location of the form in screen coordinates
    Private _screenPosition As ScreenPos

    Public Property ScreenPosition As ScreenPos
        Get
            Return _screenPosition
        End Get
        Set(value As ScreenPos)
            _screenPosition = value
        End Set
    End Property

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

    Private Sub Form1_Closed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing

        Settings.SaveSettings()

    End Sub

    Private Sub HelpToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HelpToolStripMenuItem.Click

        HelpManual("SmartStart")

    End Sub

    Private Sub Loaded(sender As Object, e As EventArgs) Handles Me.Load

        SmartStartEnabled.Text = Global.Outworldz.My.Resources.Smart_Start_Enable_word
        DelayLabel.Text = Global.Outworldz.My.Resources.SSDelay
        ToolTip1.SetToolTip(Seconds, Global.Outworldz.My.Resources.SecondsTips)

        Me.Text = Global.Outworldz.My.Resources.Smart_Start_word
        GroupBox1.Text = Global.Outworldz.My.Resources.Smart_Start_Enable_word
        SmartStartEnabled.Checked = Settings.SmartStart

        Seconds.Text = CStr(Settings.SmartStartTimeout)
        SetScreen()

    End Sub

    Private Sub Seconds_TextChanged(sender As Object, e As EventArgs) Handles Seconds.TextChanged
        Dim digitsOnly As Regex = New Regex("[^\d]")
        Seconds.Text = digitsOnly.Replace(Seconds.Text, "")
        Settings.SmartStartTimeout = CInt("0" & Seconds.Text)
        If Settings.SmartStartTimeout < 15 Then Settings.SmartStartTimeout = 15
    End Sub

    Private Sub SmartStartEnabled_CheckedChanged(sender As Object, e As EventArgs) Handles SmartStartEnabled.CheckedChanged
        Settings.SmartStart = SmartStartEnabled.Checked
    End Sub

End Class