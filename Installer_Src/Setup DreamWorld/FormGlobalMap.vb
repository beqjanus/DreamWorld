Public Class FormGlobalMap

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

    Private Sub FormGlobalMap_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.Text = My.Resources.Global_Map

    End Sub

    Private Sub FormGlobalMap_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint

        ControlPaint.DrawGrid(e.Graphics, New Rectangle(1, 1, Me.Width, Me.Height), New Size(25, 26), Color.Blue)

    End Sub

    Private Sub FormGlobalMap_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged

        Me.Refresh()

    End Sub

End Class