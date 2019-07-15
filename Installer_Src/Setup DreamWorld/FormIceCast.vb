Imports System.Text.RegularExpressions

Public Class Icecast

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

    Private Sub SC_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ShoutcastPort.Text = Form1.PropMySetting.SCPortBase.ToString(Form1.Usa)
        ShoutcastPort1.Text = Form1.PropMySetting.SCPortBase1.ToString(Form1.Usa)
        AdminPassword.Text = Form1.PropMySetting.SCAdminPassword
        ShoutcastPassword.Text = Form1.PropMySetting.SCPassword
        ShoutcastEnable.Checked = Form1.PropMySetting.SCEnable

        AdminPassword.UseSystemPasswordChar = True
        ShoutcastPassword.UseSystemPasswordChar = True
        SetScreen()
        Form1.HelpOnce("Icecast")

    End Sub

    Public Sub ShoutcastPortTextChanged(sender As Object, e As EventArgs) Handles ShoutcastPort.TextChanged

        Dim digitsOnly As Regex = New Regex("[^\d]")
        ShoutcastPort.Text = digitsOnly.Replace(ShoutcastPort.Text, "")
        Try
            Form1.PropMySetting.SCPortBase = CType(ShoutcastPort.Text, Integer)
        Catch
        End Try

    End Sub

    Private Sub AdminPassword_TextChanged(sender As Object, e As EventArgs) Handles AdminPassword.TextChanged

        Form1.PropMySetting.SCAdminPassword = AdminPassword.Text

    End Sub

    Private Sub AdminPassword_Click(sender As Object, e As EventArgs) Handles AdminPassword.Click

        AdminPassword.UseSystemPasswordChar = False

    End Sub

    Private Sub ShoutcastEnable_CheckedChanged(sender As Object, e As EventArgs) Handles ShoutcastEnable.CheckedChanged

        Form1.PropMySetting.SCEnable = ShoutcastEnable.Checked

    End Sub

    Private Sub ShoutcastPassword_TextChanged(sender As Object, e As EventArgs) Handles ShoutcastPassword.TextChanged

        Form1.PropMySetting.SCPassword = ShoutcastPassword.Text

    End Sub

    Private Sub ShoutcastPassword_CLickChanged(sender As Object, e As EventArgs) Handles ShoutcastPassword.Click

        ShoutcastPassword.UseSystemPasswordChar = False

    End Sub

    Private Sub FormisClosed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed

        Form1.PropMySetting.SaveSettings()
        Form1.SaveIceCast()

    End Sub

    Private Sub LoadURL_Click(sender As Object, e As EventArgs) Handles LoadURL.Click
        If Form1.PropOpensimIsRunning() Then
            Dim webAddress As String = "http://" + Form1.PropMySetting.PublicIP + ":" + ShoutcastPort.Text
            Form1.Print("Icecast lets you stream music into your sim. The address will be " + webAddress)
            Process.Start(webAddress)
        ElseIf Form1.PropMySetting.SCEnable = False Then
            Form1.Print("Shoutcast is not Enabled.")
        Else
            Form1.Print("Shoutcast is not running. Click Start to boot the system.")
        End If

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Form1.Help("Icecast")
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles ShoutcastPort1.TextChanged

        Dim digitsOnly As Regex = New Regex("[^\d]")
        ShoutcastPort1.Text = digitsOnly.Replace(ShoutcastPort1.Text, "")
        Try
            Form1.PropMySetting.SCPortBase1 = CType(ShoutcastPort1.Text, Integer)
        Catch
        End Try

    End Sub

End Class