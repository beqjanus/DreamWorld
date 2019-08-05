Imports System.Text.RegularExpressions

Public Class FormRestart

    Dim initted As Boolean = False

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

    Private Sub Loaded(sender As Object, e As EventArgs) Handles Me.Load

        AutoRestartBox.Text = Form1.PropMySetting.AutoRestartInterval.ToString(Form1.Usa)
        If AutoRestartBox.Text.Length > 0 Then
            ARTimerBox.Checked = True
        End If
        AutoStartCheckbox.Checked = Form1.PropMySetting.Autostart
        SequentialCheckBox1.Checked = Form1.PropMySetting.Sequential
        RestartOnCrash.Checked = Form1.PropMySetting.RestartOnCrash
        RestartOnPhysicsCrash.Checked = Form1.PropMySetting.RestartonPhysics

        SetScreen()
        Form1.HelpOnce("Restart")

        initted = True ' suppress the install of the startup on formload

    End Sub
    Private Sub IsClosed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Closed

        Form1.PropViewedSettings = True
        Form1.PropMySetting.SaveSettings()

    End Sub

#Region "AutoStart"

    Private Sub AutoStartCheckbox_CheckedChanged(sender As Object, e As EventArgs) Handles AutoStartCheckbox.CheckedChanged

        If Not initted Then Return
        Form1.PropMySetting.Autostart = AutoStartCheckbox.Checked
        Form1.PropMySetting.SaveSettings()

    End Sub

    Private Sub RunOnBoot_Click_1(sender As Object, e As EventArgs) Handles RunOnBoot.Click

        Form1.Help("Restart")

    End Sub

    Private Sub AutoRestartBox_TextChanged(sender As Object, e As EventArgs) Handles AutoRestartBox.TextChanged

        If Not initted Then Return
        Dim digitsOnly As Regex = New Regex("[^\d]")
        AutoRestartBox.Text = digitsOnly.Replace(AutoRestartBox.Text, "")

        Try
            Form1.PropMySetting.AutoRestartInterval = Convert.ToInt16(AutoRestartBox.Text, Form1.Usa)
            Form1.PropMySetting.SaveSettings()
        Catch ex As FormatException
        End Try

    End Sub

    Private Sub ARTimerBox_CheckedChanged(sender As Object, e As EventArgs) Handles ARTimerBox.CheckedChanged

        If Not initted Then Return
        If ARTimerBox.Checked Then
            Dim BTime As Int16 = CType(Form1.PropMySetting.AutobackupInterval, Int16)
            If Form1.PropMySetting.AutoBackup And Form1.PropMySetting.AutoRestartInterval > 0 And Form1.PropMySetting.AutoRestartInterval < BTime Then
                Form1.PropMySetting.AutoRestartInterval = BTime + 30
                AutoRestartBox.Text = (BTime + 30).ToString(Form1.Usa)
                MsgBox("Upping AutoRestart Time to " + BTime.ToString(Form1.Usa) + " + 30 Minutes for Autobackup to complete.", vbInformation)
            End If
        Else
            Form1.PropMySetting.AutoRestartInterval = 0
            AutoRestartBox.Text = "0"
        End If
        Form1.PropMySetting.SaveSettings()

    End Sub

    Private Sub SequentialCheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles SequentialCheckBox1.CheckedChanged

        If Not initted Then Return
        Form1.PropMySetting.Sequential = SequentialCheckBox1.Checked
        Form1.PropMySetting.SaveSettings()

    End Sub

    Private Sub RestartOnCrash_CheckedChanged(sender As Object, e As EventArgs) Handles RestartOnCrash.CheckedChanged
        Form1.PropMySetting.RestartOnCrash = RestartOnCrash.Checked
    End Sub

    Private Sub RestartOnPhysicsCrash_CheckedChanged(sender As Object, e As EventArgs) Handles RestartOnPhysicsCrash.CheckedChanged
        Form1.PropMySetting.RestartonPhysics = RestartOnPhysicsCrash.Checked
    End Sub

    Private Sub DatabaseSetupToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DatabaseSetupToolStripMenuItem.Click
        Form1.Help("Restart")
    End Sub

#End Region

End Class