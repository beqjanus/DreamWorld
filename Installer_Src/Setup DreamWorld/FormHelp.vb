

Public Class FormHelp

    Dim Document As String
    Public ScreenPosition As ScreenPos
    Private Handler As New EventHandler(AddressOf Resize_page)
    'The following detects  the location of the form in screen coordinates
    Private Sub Resize_page(ByVal sender As Object, ByVal e As System.EventArgs)
        'Me.Text = "Form screen position = " + Me.Location.ToString
        ScreenPosition.SaveXY(Me.Left, Me.Top)
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

    Public Sub Init(Webpage As String)

        SetScreen(Webpage)

        Try
            Dim Page As String = Form1.MyFolder + "\Outworldzfiles\Help\" + Webpage + ".rtf"
            RichTextBox1.LoadFile(Page)
        Catch ex As Exception
            MsgBox("Sorry, Help is not yet available for this.", vbInformation)
            Form1.ErrorLog("Error:" + ex.Message)
            Me.Close()
        End Try
    End Sub
    Private Sub HomeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HomeToolStripMenuItem.Click
        Dim webAddress As String = "https://www.outworldz.com"
        Process.Start(webAddress)
    End Sub

    Private Sub DreamgridToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DreamgridToolStripMenuItem.Click
        Dim webAddress As String = "https://www.outworldz.com/Outworldz_installer/"
        Process.Start(webAddress)
    End Sub

    Private Sub TechnicalInfoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TechnicalInfoToolStripMenuItem.Click
        Dim webAddress As String = "https://www.outworldz.com/Outworldz_installer/technical.htm"
        Process.Start(webAddress)
    End Sub

    Private Sub TroubleshootingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TroubleshootingToolStripMenuItem.Click
        Dim webAddress As String = "https://www.outworldz.com/Outworldz_installer/Manual_TroubleShooting.htm"
        Process.Start(webAddress)
    End Sub

    Private Sub StepbyStepInstallationToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StepbyStepInstallationToolStripMenuItem.Click
        Dim webAddress As String = "https://www.outworldz.com/Outworldz_installer/Startup.htm"
        Process.Start(webAddress)
    End Sub

    Private Sub PortsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PortsToolStripMenuItem.Click
        Dim webAddress As String = "https://www.outworldz.com/Outworldz_installer/PortForwarding.htm"
        Process.Start(webAddress)
    End Sub

    Private Sub LoopbackToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoopbackToolStripMenuItem.Click
        Dim webAddress As String = "https://www.outworldz.com/Outworldz_installer/Loopback.htm"
        Process.Start(webAddress)
    End Sub

    Private Sub DatabaseHelpToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DatabaseHelpToolStripMenuItem.Click
        Dim webAddress As String = "https://www.outworldz.com/Outworldz_installer/Rebuilding_from_a_blank_database.htm"
        Process.Start(webAddress)
    End Sub

    Private Sub SourceCodeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SourceCodeToolStripMenuItem.Click
        Dim webAddress As String = "https://github.com/Outworldz/DreamWorld"
        Process.Start(webAddress)
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub PrintToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles PrintToolStripMenuItem1.Click


        Dim info = New ProcessStartInfo(Form1.MyFolder + "\Outworldzfiles\Help\" + Document + ".rtf")
        info.Verb = "Print"
        info.CreateNoWindow = True
        info.WindowStyle = ProcessWindowStyle.Hidden
        Process.Start(info)


    End Sub
End Class