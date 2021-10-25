#Region "Copyright AGPL3.0"

' Copyright Outworldz, LLC.
' AGPL3.0  https://opensource.org/licenses/AGPL

#End Region

Public Class FormVoice

#Region "ScreenSize"

    Private ReadOnly Handler As New EventHandler(AddressOf Resize_page)
    Private _screenPosition As ClassScreenpos

    Public Property ScreenPosition As ClassScreenpos
        Get
            Return _screenPosition
        End Get
        Set(value As ClassScreenpos)
            _screenPosition = value
        End Set
    End Property

    'The following detects  the location of the form in screen coordinates
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

#Region "Private Methods"

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles VivoxEnable.CheckedChanged
        Settings.VivoxEnabled = VivoxEnable.Checked
        Settings.SaveSettings()
    End Sub

    Private Sub IsClosed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Closed

        Settings.SaveSettings()

    End Sub

    Private Sub Loaded(sender As Object, e As EventArgs) Handles Me.Load

        GroupBox1.Text = Global.Outworldz.My.Resources.Setup_Voice_Service
        Label1.Text = Global.Outworldz.My.Resources.User_ID_word
        Label2.Text = Global.Outworldz.My.Resources.Password_word
        MenuStrip2.Text = Global.Outworldz.My.Resources._0
        ToolStripMenuItem30.Image = Global.Outworldz.My.Resources.question_and_answer
        ToolStripMenuItem30.Text = Global.Outworldz.My.Resources.Help_word
        VivoxEnable.Text = Global.Outworldz.My.Resources.Enable_word

        Me.Text = Global.Outworldz.My.Resources.Voice_Settings_Word
        VivoxEnable.Checked = Settings.VivoxEnabled
        VivoxPassword.Text = Settings.VivoxPassword
        VivoxUserName.Text = Settings.VivoxUserName
        VivoxPassword.UseSystemPasswordChar = True
        SetScreen()
        HelpOnce("Vivox")

    End Sub

    Private Sub ToolStripMenuItem30_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem30.Click
        HelpManual("Vivox")
    End Sub

    Private Sub VivoxPassword_Clicked(sender As Object, e As EventArgs) Handles VivoxPassword.Click
        VivoxPassword.UseSystemPasswordChar = False
    End Sub

    Private Sub VivoxPassword_TextChanged(sender As Object, e As EventArgs) Handles VivoxPassword.TextChanged
        VivoxPassword.UseSystemPasswordChar = False
        Settings.VivoxPassword = VivoxPassword.Text
        Settings.SaveSettings()
    End Sub

    Private Sub VivoxUserName_TextChanged(sender As Object, e As EventArgs) Handles VivoxUserName.TextChanged

        Settings.VivoxUserName = VivoxUserName.Text
        Settings.SaveSettings()
    End Sub

#End Region

End Class
