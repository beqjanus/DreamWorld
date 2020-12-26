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

Public Class FormHelp

#Region "ScreenSize"

    Private Document As String

    Private _screenPosition As ScreenPos
    Private Handler As New EventHandler(AddressOf Resize_page)

    Private Sub Loaded(sender As Object, e As EventArgs) Handles Me.Load

    End Sub

    Private Sub SetScreen(Webpage As String)

        Me.Show()
        Document = Webpage
        Me.Name = Webpage
        ScreenPosition = New ScreenPos(Me.Name)
        AddHandler ResizeEnd, Handler
        Dim xy As List(Of Integer) = ScreenPosition.GetXY()
        Me.Left = xy.Item(0) + 25
        Me.Top = xy.Item(1) + 25

    End Sub

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

#End Region

#Region "Public Methods"

    Public Sub Init(Webpage As String)

        SetScreen(Webpage)

        DreamgridToolStripMenuItem.Image = Global.Outworldz.My.Resources.cube_blue
        DreamgridToolStripMenuItem.Text = Global.Outworldz.My.Resources.Home_word
        ExitToolStripMenuItem.Image = Global.Outworldz.My.Resources.exit_icon
        ExitToolStripMenuItem.Text = Global.Outworldz.My.Resources.Exit__word
        FileToolStripMenuItem.Text = Global.Outworldz.My.Resources.File_word
        HomeToolStripMenuItem.Image = Global.Outworldz.My.Resources.about
        LoopbackToolStripMenuItem.Image = Global.Outworldz.My.Resources.replace2
        LoopbackToolStripMenuItem.Text = Global.Outworldz.My.Resources.Loopback_Help
        PrintToolStripMenuItem.Text = Global.Outworldz.My.Resources.Print
        PrintToolStripMenuItem1.Image = Global.Outworldz.My.Resources.printer3
        PrintToolStripMenuItem1.Text = Global.Outworldz.My.Resources.Print
        SourceCodeToolStripMenuItem.Image = Global.Outworldz.My.Resources.transform
        SourceCodeToolStripMenuItem.Text = Global.Outworldz.My.Resources.Source_Code_word
        Text = Global.Outworldz.My.Resources.Help_word
        WebSiteToolStripMenuItem.Image = Global.Outworldz.My.Resources.about
        WebSiteToolStripMenuItem.Text = Global.Outworldz.My.Resources.More_Help

        Dim u As New Uri(IO.Path.Combine(Settings.CurrentDirectory, "Outworldzfiles\Help\" & Webpage + ".htm"))
        WebBrowser1.Navigate(u)

    End Sub

#End Region

#Region "Private Methods"

    Private Sub DreamgridToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DreamgridToolStripMenuItem.Click
        Dim webAddress As String = "https://outworldz.com/Outworldz_installer/"
        Try
            Process.Start(webAddress)
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
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
            BreakPoint.Show(ex.Message)
        End Try
    End Sub

    Private Sub PrintToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles PrintToolStripMenuItem1.Click

        Dim info = New ProcessStartInfo(IO.Path.Combine(Settings.CurrentDirectory, "Outworldzfiles\HelpFiles\" & Document & ".htm")) With {
            .Verb = "Print",
            .CreateNoWindow = True,
            .WindowStyle = ProcessWindowStyle.Hidden
        }
        Try
            Process.Start(info)
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try
    End Sub

    Private Sub SourceCodeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SourceCodeToolStripMenuItem.Click

        Dim webAddress As String = "https://github.com/Outworldz/DreamWorld"
        Try
            Process.Start(webAddress)
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try

    End Sub

#End Region

End Class
