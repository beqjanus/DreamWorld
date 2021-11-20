#Region "Copyright AGPL3.0"

' Copyright Outworldz, LLC.
' AGPL3.0  https://opensource.org/licenses/AGPL

#End Region

Public Class FormScripts

#Region "Private Fields"

    Private ReadOnly Handler As New EventHandler(AddressOf Resize_page)
    Private _screenPosition As ClassScreenpos
    Dim initted As Boolean

#End Region

#Region "Load/Save"

    Private Sub Loaded(sender As Object, e As EventArgs) Handles Me.Load

        GroupBox1.Text = Global.Outworldz.My.Resources.Script_Engine_word '"Script Engine"
        GroupBox8.Text = Global.Outworldz.My.Resources.Allow_LSL
        LSLCheckbox.Text = Global.Outworldz.My.Resources.Enable_word
        MenuStrip2.Text = Global.Outworldz.My.Resources._0
        Text = Global.Outworldz.My.Resources.Scripts_word
        ToolStripMenuItem30.Image = Global.Outworldz.My.Resources.question_and_answer
        ToolStripMenuItem30.Text = Global.Outworldz.My.Resources.Help_word
        XengineButton.Text = Global.Outworldz.My.Resources.XEngine_word
        YengineButton.Text = Global.Outworldz.My.Resources.YEngine_word
        ScriptsOff.Text = Global.Outworldz.My.Resources.Off

        SetScreen()

        LSLCheckbox.Checked = Settings.LSLHTTP()

        If Settings.ScriptEngine() = "YEngine" Then
            YengineButton.Checked = True
        ElseIf Settings.ScriptEngine() = "XEngine" Then
            XengineButton.Checked = True
        ElseIf Settings.ScriptEngine() = "Off" Then
            ScriptsOff.Checked = True
        Else
            YengineButton.Checked = True
        End If
        initted = True

        HelpOnce("Scripts")

    End Sub

#End Region

#Region "Buttons"

    Private Sub LSLCheckbox_CheckedChanged(sender As Object, e As EventArgs) Handles LSLCheckbox.CheckedChanged

        If initted Then

            Settings.LSLHTTP() = LSLCheckbox.Checked
            Settings.SaveSettings()
        End If

    End Sub

    Private Sub ScriptsOff_CheckedChanged(sender As Object, e As EventArgs) Handles ScriptsOff.CheckedChanged

        If Not initted Then Return
        Settings.ScriptEngine() = "Off"
        Settings.SaveSettings()

    End Sub

    Private Sub XengineButton_CheckedChanged(sender As Object, e As EventArgs) Handles XengineButton.CheckedChanged

        If Not initted Then Return
        Settings.ScriptEngine() = "XEngine"
        Settings.SaveSettings()

    End Sub

    Private Sub YEngineRadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles YengineButton.CheckedChanged

        If Not initted Then Return
        Settings.ScriptEngine() = "YEngine"
        Settings.SaveSettings()

    End Sub

#End Region

#Region "ScreenSize"

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

    Private Sub ToolStripMenuItem30_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem30.Click
        HelpManual("Scripts")
    End Sub

#End Region

End Class
