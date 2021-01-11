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

Public Class FormGloebits

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

    Private Sub FormisClosed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed

        FormSetup.PropViewedSettings = True
        DoGloebits()

    End Sub

    Private Sub Loaded(sender As Object, e As EventArgs) Handles Me.Load

        Button4.Text = Global.Outworldz.My.Resources.Free_Account
        GLBShowNewSessionAuthIMCheckBox.Text = Global.Outworldz.My.Resources.GLBShowNewSessionAuthIM_text
        GLBShowNewSessionPurchaseIMCheckBox.Text = Global.Outworldz.My.Resources.GLBShowNewSessionPurchaseIM_text
        GLBShowWelcomeMessageCheckBox.Text = Global.Outworldz.My.Resources.GLBShowWelcomeMessage_text
        GloebitsEnabled.Text = Global.Outworldz.My.Resources.EnableGloebit_word
        HelpToolStripMenuItem.Image = Global.Outworldz.My.Resources.question_and_answer
        HelpToolStripMenuItem.Text = Global.Outworldz.My.Resources.Help_word
        HelpToolStripMenuItem1.Image = Global.Outworldz.My.Resources.about
        HelpToolStripMenuItem1.Text = Global.Outworldz.My.Resources.Help_word
        PictureBox3.Image = Global.Outworldz.My.Resources.about
        ProductionButton.Text = Global.Outworldz.My.Resources.Production_Mode_Word
        ProductionCreateAppButton.Text = Global.Outworldz.My.Resources.CreateApp
        ProductionCreateButton.Text = Global.Outworldz.My.Resources.Create_Account
        ProductionReqAppButton.Text = Global.Outworldz.My.Resources.Request_App
        SandBoxCreateAppButton.Text = Global.Outworldz.My.Resources.CreateApp
        SandBoxReqAppButton.Text = Global.Outworldz.My.Resources.Request_App
        SandBoxSignUpButton.Text = Global.Outworldz.My.Resources.Create_Sandbox_word
        SandboxButton.Text = Global.Outworldz.My.Resources.Sandbox_Mode_word

        ContactEmailTextBox.Text = Settings.GLBOwnerEmail
        OwnerNameTextbox.Text = Settings.GLBOwnerName

        SandboxButton.Checked = Not Settings.GloebitsMode
        ProductionButton.Checked = Settings.GloebitsMode

        SandKeyTextBox.Text = Settings.GLSandKey
        SandSecretTextBox.UseSystemPasswordChar = True
        SandSecretTextBox.Text = Settings.GLSandSecret

        ProdKeyTextBox.Text = Settings.GLProdKey
        ProdSecretTextBox.UseSystemPasswordChar = True
        ProdSecretTextBox.Text = Settings.GLProdSecret

        GLBShowNewSessionAuthIMCheckBox.Checked = Settings.GLBShowNewSessionAuthIM
        GLBShowNewSessionPurchaseIMCheckBox.Checked = Settings.GLBShowNewSessionPurchaseIM
        GLBShowWelcomeMessageCheckBox.Checked = Settings.GLBShowWelcomeMessage

        GloebitsEnabled.Checked = Settings.GloebitsEnable
        SetScreen()

    End Sub

#End Region

#Region "Mode"

    Private Sub ProductionButton_CheckedChanged_1(sender As Object, e As EventArgs) Handles ProductionButton.CheckedChanged
        If ProductionButton.Checked = True Then
            SandboxButton.Checked = False
            Settings.GloebitsMode = True
            Settings.SaveSettings()
        End If
    End Sub

    Private Sub SandboxButton_CheckedChanged_1(sender As Object, e As EventArgs) Handles SandboxButton.CheckedChanged
        If SandboxButton.Checked = True Then
            ProductionButton.Checked = False
            Settings.GloebitsMode = False
            Settings.SaveSettings()
        End If
    End Sub

#End Region

#Region "Sandbox"

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles SandBoxSignUpButton.Click
        Dim webAddress As String = "https://sandbox.gloebit.com/signup/"
        Try
            Process.Start(webAddress)
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles SandBoxReqAppButton.Click
        Dim webAddress As String = "https://sandbox.gloebit.com/merchant-signup/"
        Try
            Process.Start(webAddress)
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try
    End Sub

    Private Sub CreateAppButton2_Click(sender As Object, e As EventArgs) Handles SandBoxCreateAppButton.Click
        Dim webAddress As String = "https://www.gloebit.com"
        Try
            Process.Start(webAddress)
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles SandKeyTextBox.TextChanged
        Settings.GLSandKey = SandKeyTextBox.Text
        Settings.SaveSettings()
    End Sub

    Private Sub TextBox2_click(sender As Object, e As EventArgs) Handles SandSecretTextBox.Click
        SandSecretTextBox.UseSystemPasswordChar = False
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles SandSecretTextBox.TextChanged
        Settings.GLSandSecret = SandSecretTextBox.Text
        Settings.SaveSettings()
    End Sub

#End Region

#Region "Production"

    Private Sub ProdKeyTextBox_Click(sender As Object, e As EventArgs) Handles ProdKeyTextBox.Click

        ProdKeyTextBox.UseSystemPasswordChar = False

    End Sub

    Private Sub ProdKeyTextBox_TextChanged(sender As Object, e As EventArgs) Handles ProdKeyTextBox.TextChanged

        Settings.GLProdKey = ProdKeyTextBox.Text
        Settings.SaveSettings()

    End Sub

    Private Sub ProdSecretTextBox_Click(sender As Object, e As EventArgs) Handles ProdSecretTextBox.Click

        ProdSecretTextBox.UseSystemPasswordChar = False

    End Sub

    Private Sub ProdSecretTextBox_TextChanged(sender As Object, e As EventArgs) Handles ProdSecretTextBox.TextChanged

        Settings.GLProdSecret = ProdSecretTextBox.Text
        Settings.SaveSettings()

    End Sub

    Private Sub ProductionCreateAppButton_Click(sender As Object, e As EventArgs) Handles ProductionCreateAppButton.Click
        Dim webAddress As String = "https://www.gloebit.com/merchant-tools/"
        Try
            Process.Start(webAddress)
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try
    End Sub

    Private Sub ProductionCreateButton_Click(sender As Object, e As EventArgs) Handles ProductionCreateButton.Click
        Dim webAddress As String = "https://www.gloebit.com/signup/"
        Try
            Process.Start(webAddress)
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try
    End Sub

    Private Sub ProductionReqAppButton_Click(sender As Object, e As EventArgs) Handles ProductionReqAppButton.Click
        Dim webAddress As String = "https://www.gloebit.com/merchant-signup/"
        Try
            Process.Start(webAddress)
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try
    End Sub

#End Region

#Region "OwnerInfo"

    Private Sub ContactEmailTextBox_TextChanged(sender As Object, e As EventArgs) Handles ContactEmailTextBox.TextChanged

        Settings.GLBOwnerEmail = ContactEmailTextBox.Text
        Settings.SaveSettings()

    End Sub

    Private Sub GloebitsEnabled_CheckedChanged(sender As Object, e As EventArgs) Handles GloebitsEnabled.CheckedChanged

        Settings.GloebitsEnable = GloebitsEnabled.Checked
        Settings.SaveSettings()

    End Sub

    Private Sub OwnerNameTextbox_TextChanged(sender As Object, e As EventArgs) Handles OwnerNameTextbox.TextChanged

        Settings.GLBOwnerName = OwnerNameTextbox.Text
        Settings.SaveSettings()

    End Sub

#End Region

#Region "Help"

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim webAddress As String = "http://dev.gloebit.com/opensim/"
        Try
            Process.Start(webAddress)
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try
    End Sub

    Private Sub GLBShowNewSessionAuthIMCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles GLBShowNewSessionAuthIMCheckBox.CheckedChanged

        Settings.GLBShowNewSessionAuthIM = GLBShowNewSessionAuthIMCheckBox.Checked
        Settings.SaveSettings()

    End Sub

    Private Sub GLBShowNewSessionPurchaseIMCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles GLBShowNewSessionPurchaseIMCheckBox.CheckedChanged

        Settings.GLBShowNewSessionPurchaseIM = GLBShowNewSessionPurchaseIMCheckBox.Checked
        Settings.SaveSettings()

    End Sub

    Private Sub GLBShowWelcomeMessageCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles GLBShowWelcomeMessageCheckBox.CheckedChanged

        Settings.GLBShowWelcomeMessage = GLBShowWelcomeMessageCheckBox.Checked
        Settings.SaveSettings()

    End Sub

    Private Sub HelpToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles HelpToolStripMenuItem1.Click

        Dim webAddress As String = "http://dev.gloebit.com/opensim/"
        Try
            Process.Start(webAddress)
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try

    End Sub

#End Region

End Class
