Public Class FormVoice

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
        Me.Text = "Vivox Voice Settings"
        VivoxEnable.Checked = Form1.MySetting.VivoxEnabled
        VivoxPassword.Text = Form1.MySetting.Vivox_password
        VivoxUserName.Text = Form1.MySetting.Vivox_UserName
        VivoxPassword.UseSystemPasswordChar = True
        SetScreen()
        Form1.HelpOnce("Vivox")
    End Sub
    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles VivoxEnable.CheckedChanged
        Form1.MySetting.VivoxEnabled = VivoxEnable.Checked
        Form1.MySetting.SaveSettings()
    End Sub

    Private Sub RequestPassword_Click(sender As Object, e As EventArgs) Handles RequestPassword.Click
        Dim webAddress As String = "https://support.vivox.com/opensim/"
        Process.Start(webAddress)
    End Sub

    Private Sub VivoxUserName_TextChanged(sender As Object, e As EventArgs) Handles VivoxUserName.TextChanged
        Form1.MySetting.Vivox_UserName = VivoxUserName.Text
        Form1.MySetting.SaveSettings()
    End Sub

    Private Sub VivoxPassword_TextChanged(sender As Object, e As EventArgs) Handles VivoxPassword.TextChanged
        VivoxPassword.UseSystemPasswordChar = False
        Form1.MySetting.Vivox_password = VivoxPassword.Text
        Form1.MySetting.SaveSettings()
    End Sub
    Private Sub VivoxPassword_Clicked(sender As Object, e As EventArgs) Handles VivoxPassword.Click
        VivoxPassword.UseSystemPasswordChar = False
    End Sub

    Private Sub RunOnBoot_Click(sender As Object, e As EventArgs) Handles RunOnBoot.Click
        Form1.Help("Vivox")
    End Sub
End Class