Public Class FormScripts



#Region "Private Fields"

    Private _screenPosition As ScreenPos
    Private Handler As New EventHandler(AddressOf Resize_page)
    Dim initted As Boolean = False

#End Region

#Region "Load/Save"

    Private Sub Loaded(sender As Object, e As EventArgs) Handles Me.Load

        SetScreen()

        LSLCheckbox.Checked = Form1.Settings.LSLHTTP()

        If Form1.Settings.ScriptEngine() = "YEngine" Then
            YengineButton.Checked = True
            XengineButton.Checked = False
        Else
            XengineButton.Checked = True
            YengineButton.Checked = False
        End If
        initted = True

        Form1.HelpOnce("Scripts")

    End Sub

#End Region

#Region "Buttons"

    Private Sub LSLCheckbox_CheckedChanged(sender As Object, e As EventArgs) Handles LSLCheckbox.CheckedChanged

        If initted Then

            Form1.Settings.LSLHTTP() = LSLCheckbox.Checked
            Form1.Settings.SaveSettings()
        End If

    End Sub

    Private Sub XengineButton_CheckedChanged(sender As Object, e As EventArgs) Handles XengineButton.CheckedChanged

        If Not initted Then Return
        Form1.Settings.ScriptEngine() = "XEngine"
        Form1.Settings.SaveSettings()

    End Sub

    Private Sub YEngineRadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles YengineButton.CheckedChanged

        If Not initted Then Return
        Form1.Settings.ScriptEngine() = "YEngine"
        Form1.Settings.SaveSettings()

    End Sub

#End Region

#Region "ScreenSize"

    Public Property ScreenPosition As ScreenPos
        Get
            Return _screenPosition
        End Get
        Set(value As ScreenPos)
            _screenPosition = value
        End Set
    End Property

    Private Sub DatabaseSetupToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DatabaseSetupToolStripMenuItem.Click
        Form1.Help("Scripts")
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

    Private Sub ToolStripMenuItem30_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem30.Click
        Form1.Help("Scripts")
    End Sub

#End Region

End Class
