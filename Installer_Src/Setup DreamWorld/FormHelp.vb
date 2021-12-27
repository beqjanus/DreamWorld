#Region "Copyright AGPL3.0"

' Copyright Outworldz, LLC.
' AGPL3.0  https://opensource.org/licenses/AGPL

#End Region

Public Class FormHelp

#Region "ScreenSize"

    Private ReadOnly Handler As New EventHandler(AddressOf Resize_page)
    Private _screenPosition As ClassScreenpos
    Private Document As String

    Public Property ScreenPosition As ClassScreenpos
        Get
            Return _screenPosition
        End Get
        Set(value As ClassScreenpos)
            _screenPosition = value
        End Set
    End Property

    Private Sub Loaded(sender As Object, e As EventArgs) Handles Me.Load

    End Sub

    'The following detects  the location of the form in screen coordinates
    Private Sub Resize_page(ByVal sender As Object, ByVal e As System.EventArgs)
        'Me.Text = "Form screen position = " + Me.Location.ToString
        ScreenPosition.SaveXY(Me.Left, Me.Top)
    End Sub

    Private Sub SetScreen(Webpage As String)

        Document = Webpage
        Me.Name = Webpage
        ScreenPosition = New ClassScreenpos(Me.Name)
        AddHandler ResizeEnd, Handler
        Dim xy As List(Of Integer) = ScreenPosition.GetXY()
        Me.Left = xy.Item(0) + 25
        Me.Top = xy.Item(1) + 25

    End Sub

#End Region

#Region "Public Methods"

    Public Sub Init(Webpage As String)

        SetScreen(Webpage)

        DreamgridToolStripMenuItem.Image = Global.Outworldz.My.Resources.cube_green
        DreamgridToolStripMenuItem.Text = Global.Outworldz.My.Resources.Home_word
        ExitToolStripMenuItem.Image = Global.Outworldz.My.Resources.exit_icon
        ExitToolStripMenuItem.Text = Global.Outworldz.My.Resources.Exit__word
        FileToolStripMenuItem.Text = Global.Outworldz.My.Resources.File_word
        HomeToolStripMenuItem.Image = Global.Outworldz.My.Resources.about
        PrintToolStripMenuItem1.Image = Global.Outworldz.My.Resources.printer3
        SourceCodeToolStripMenuItem.Image = Global.Outworldz.My.Resources.transform
        SourceCodeToolStripMenuItem.Text = Global.Outworldz.My.Resources.Source_Code_word
        Text = Global.Outworldz.My.Resources.Help_word
        WebSiteToolStripMenuItem.Image = Global.Outworldz.My.Resources.about
        WebSiteToolStripMenuItem.Text = Global.Outworldz.My.Resources.More_Help

        Dim u As New Uri(IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Help\" & Webpage + ".htm"))
        WebBrowser1.Navigate(u)
        Me.Show()

    End Sub

#End Region

#Region "Private Methods"

    Private Sub DreamgridToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DreamgridToolStripMenuItem.Click
        Dim webAddress As String = "https://outworldz.com/Outworldz_installer/"
        Try
            Process.Start(webAddress)
        Catch ex As Exception
            BreakPoint.DUmp(ex)
        End Try
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub HomeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HomeToolStripMenuItem.Click
        Dim webAddress As String = "https://outworldz.com"
        Try
            Process.Start(webAddress)
        Catch ex As Exception
            BreakPoint.DUmp(ex)
        End Try
    End Sub

    Private Sub PrintToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles PrintToolStripMenuItem1.Click

        Dim path = IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Help\" & Document & ".htm")
        Dim info = New ProcessStartInfo(path) With {
            .Verb = "Print",
            .WindowStyle = ProcessWindowStyle.Normal
        }
        Try
            Process.Start(info)
        Catch ex As Exception
            BreakPoint.DUmp(ex)
        End Try
    End Sub

    Private Sub SourceCodeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SourceCodeToolStripMenuItem.Click

        Dim webAddress As String = "https://github.com/Outworldz/DreamWorld"
        Try
            Process.Start(webAddress)
        Catch ex As Exception
            BreakPoint.DUmp(ex)
        End Try

    End Sub

#End Region

End Class
