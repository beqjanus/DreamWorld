Public Class Gloebits

#Region "Globals"

    Dim Initted As Boolean = False

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

#Region "Load/Quit"

    Private Sub Loaded(sender As Object, e As EventArgs) Handles Me.Load

        ContactEmailTextBox.Text = Form1.MySetting.GLBOwnerEmail
        OwnerNameTextbox.Text = Form1.MySetting.GLBOwnerName

        SandboxButton.Checked = Not Form1.MySetting.GloebitsMode
        ProductionButton.Checked = Form1.MySetting.GloebitsMode

        SandKeyTextBox.Text = Form1.MySetting.GLSandKey
        SandSecretTextBox.UseSystemPasswordChar = True
        SandSecretTextBox.Text = Form1.MySetting.GLSandSecret

        ProdKeyTextBox.Text = Form1.MySetting.GLProdKey
        ProdSecretTextBox.UseSystemPasswordChar = True
        ProdSecretTextBox.Text = Form1.MySetting.GLProdSecret

        GloebitsEnabled.Checked = Form1.MySetting.GloebitsEnable
        SetScreen()
        Initted = True
    End Sub

    Private Sub FormisClosed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed

        Form1.DoGloebits()

    End Sub

#End Region

#Region "Mode"

    Private Sub SandboxButton_CheckedChanged_1(sender As Object, e As EventArgs) Handles SandboxButton.CheckedChanged
        If SandboxButton.Checked = True Then
            ProductionButton.Checked = False
            Form1.MySetting.GloebitsMode = False
            Form1.MySetting.SaveSettings()
        End If
    End Sub

    Private Sub ProductionButton_CheckedChanged_1(sender As Object, e As EventArgs) Handles ProductionButton.CheckedChanged
        If ProductionButton.Checked = True Then
            SandboxButton.Checked = False
            Form1.MySetting.GloebitsMode = True
            Form1.MySetting.SaveSettings()
        End If
    End Sub

#End Region

#Region "Sandbox"

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles SandKeyTextBox.TextChanged
        Form1.MySetting.GLSandKey = SandKeyTextBox.Text
        Form1.MySetting.SaveSettings()
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles SandSecretTextBox.TextChanged
        Form1.MySetting.GLSandSecret = SandSecretTextBox.Text
        Form1.MySetting.SaveSettings()
    End Sub

    Private Sub TextBox2_click(sender As Object, e As EventArgs) Handles SandSecretTextBox.Click
        SandSecretTextBox.UseSystemPasswordChar = False
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles SandBoxSignUpButton.Click
        Dim webAddress As String = "https://sandbox.gloebit.com/signup/"
        Process.Start(webAddress)
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles SandBoxReqAppButton.Click
        Dim webAddress As String = "https://sandbox.gloebit.com/merchant-signup/"
        Process.Start(webAddress)
    End Sub

    Private Sub CreateAppButton2_Click(sender As Object, e As EventArgs) Handles SandBoxCreateAppButton.Click
        Dim webAddress As String = "https://www.gloebit.com"
        Process.Start(webAddress)
    End Sub

#End Region

#Region "Production"

    Private Sub ProdSecretTextBox_Click(sender As Object, e As EventArgs) Handles ProdSecretTextBox.Click
        ProdSecretTextBox.UseSystemPasswordChar = False
    End Sub

    Private Sub ProdSecretTextBox_TextChanged(sender As Object, e As EventArgs) Handles ProdSecretTextBox.TextChanged
        Form1.MySetting.GLProdSecret = ProdSecretTextBox.Text
        Form1.MySetting.SaveSettings()
    End Sub

    Private Sub ProdKeyTextBox_Click(sender As Object, e As EventArgs) Handles ProdKeyTextBox.Click
        ProdKeyTextBox.UseSystemPasswordChar = False
    End Sub

    Private Sub ProdKeyTextBox_TextChanged(sender As Object, e As EventArgs) Handles ProdKeyTextBox.TextChanged
        Form1.MySetting.GLProdKey = ProdKeyTextBox.Text
        Form1.MySetting.SaveSettings()
    End Sub

    Private Sub ProductionCreateButton_Click(sender As Object, e As EventArgs) Handles ProductionCreateButton.Click
        Dim webAddress As String = "https://www.gloebit.com/signup/"
        Process.Start(webAddress)
    End Sub

    Private Sub ProductionReqAppButton_Click(sender As Object, e As EventArgs) Handles ProductionReqAppButton.Click
        Dim webAddress As String = "https://www.gloebit.com/merchant-signup/"
        Process.Start(webAddress)
    End Sub

    Private Sub ProductionCreateAppButton_Click(sender As Object, e As EventArgs) Handles ProductionCreateAppButton.Click
        Dim webAddress As String = "https://www.gloebit.com/merchant-tools/"
        Process.Start(webAddress)
    End Sub

#End Region

#Region "OwnerInfo"

    Private Sub OwnerNameTextbox_TextChanged(sender As Object, e As EventArgs) Handles OwnerNameTextbox.TextChanged
        Form1.MySetting.GLBOwnerName = OwnerNameTextbox.Text
        Form1.MySetting.SaveSettings()

    End Sub

    Private Sub ContactEmailTextBox_TextChanged(sender As Object, e As EventArgs) Handles ContactEmailTextBox.TextChanged

        Form1.MySetting.GLBOwnerEmail = ContactEmailTextBox.Text
        Form1.MySetting.SaveSettings()

    End Sub

    Private Sub GloebitsEnabled_CheckedChanged(sender As Object, e As EventArgs) Handles GloebitsEnabled.CheckedChanged

        Form1.MySetting.GloebitsEnable = GloebitsEnabled.Checked
        Form1.MySetting.SaveSettings()

    End Sub

#End Region

#Region "Help"

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim webAddress As String = "http://dev.gloebit.com/opensim/"
        Process.Start(webAddress)
    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        Dim webAddress As String = "http://dev.gloebit.com/monetize/"
        Process.Start(webAddress)
    End Sub

#End Region

End Class