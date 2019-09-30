Public Class FormJOpenSim

#Region "ScreenSize"

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

    Private Sub LoadForm(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        SetScreen()

        CheckBox1.Checked = Form1.Settings.JOpenSimEnabled
        Form1.HelpOnce("JOpensim")

    End Sub

    Private Sub Form_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        Form1.Settings.SaveSettings()

    End Sub

#End Region

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged

        Form1.Settings.JOpenSimEnabled = CheckBox1.Checked

    End Sub

End Class