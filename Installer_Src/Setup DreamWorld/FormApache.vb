#Region "Copyright AGPL3.0"

' Copyright Outworldz, LLC.
' AGPL3.0  https://opensource.org/licenses/AGPL

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

Imports System.Text.RegularExpressions

Public Class FormApache

#Region "Private Fields"

    Private Const DreamGrid As String = "DreamGrid"
    Private Const JOpensim As String = "JOpensim"
    Private Const WordPress As String = "WordPress"
    Dim initted As Boolean

#End Region

#Region "FormPos"

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

#Region "Start/Stop"

    Private Sub Close_form(sender As Object, e As EventArgs) Handles Me.Closed

        Settings.SaveSettings()

        FormSetup.PropChangedRegionSettings = True

    End Sub

    Private Sub Loaded(sender As Object, e As EventArgs) Handles Me.Load

        ApacheCheckbox.Text = Global.Outworldz.My.Resources.EnableApache
        EnableDiva.Text = Global.Outworldz.My.Resources.EnableDiva
        EnableJOpensim.Text = Global.Outworldz.My.Resources.JOpensim_word
        EnableOther.Text = Global.Outworldz.My.Resources.EnableOther_Word
        EnableWP.Text = My.Resources.WordPress_Word
        GroupBox2.Text = My.Resources.Apache_word
        GroupBox3.Text = My.Resources.Content_Manager_Word
        HelpToolStripMenuItem.Image = Global.Outworldz.My.Resources.question_and_answer
        HelpToolStripMenuItem.Text = Global.Outworldz.My.Resources.Help_word
        Label3.Text = My.Resources.Web_Port
        Text = My.Resources.Apache_word
        Sitemap.Text = Global.Outworldz.My.Resources.Automatic_Site_map_word

        SetScreen()

        ApacheCheckbox.Checked = Settings.ApacheEnable
        ApachePort.Text = CType(Settings.ApachePort, String)

        '''' set the other box and the radios for Different CMS systems.
        ''' This is used to redirect all access to apache / to the folder listed below
        If Settings.CMS = DreamGrid Then
            EnableDiva.Checked = True
        ElseIf Settings.CMS = WordPress Then
            EnableWP.Checked = True
        ElseIf Settings.CMS = JOpensim Then
            EnableJOpensim.Checked = True
        Else
            EnableOther.Checked = True
            Other.Text = Settings.CMS
        End If

        Sitemap.Checked = Settings.SiteMap

        HelpOnce("Apache")
        initted = True

    End Sub

#End Region

#Region "Clickers"

    Private Shared Sub RemoveApache()

        Using ApacheProcess As New Process()
            ApacheProcess.StartInfo.FileName = "sc"
            ApacheProcess.StartInfo.Arguments = "stop " & "ApacheHTTPServer"
            ApacheProcess.StartInfo.CreateNoWindow = True
            Try
                ApacheProcess.Start()
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
            End Try
            Application.DoEvents()
            ApacheProcess.WaitForExit()
            Sleep(1000)
            ApacheProcess.StartInfo.Arguments = " delete  " & "ApacheHTTPServer"
            ApacheProcess.StartInfo.CreateNoWindow = True
            Try
                ApacheProcess.Start()
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
            End Try
            Application.DoEvents()
            ApacheProcess.WaitForExit()
            TextPrint(My.Resources.Apache_has_been_removed)
        End Using

    End Sub

    Private Sub ApacheCheckbox_CheckedChanged(sender As Object, e As EventArgs) Handles ApacheCheckbox.CheckedChanged

        If Not initted Then Return
        Settings.ApacheEnable = ApacheCheckbox.Checked

    End Sub

    Private Sub ApachePort_TextChanged(sender As Object, e As EventArgs) Handles ApachePort.TextChanged

        If Not initted Then Return

        Dim digitsOnly As Regex = New Regex("[^\d]")
        ApachePort.Text = digitsOnly.Replace(ApachePort.Text, "")
        If ApachePort.Text.Length > 0 Then
            Settings.ApachePort = CType(ApachePort.Text, Integer)
        End If

    End Sub

    Private Sub EnableDiva_CheckedChanged(sender As Object, e As EventArgs) Handles EnableDiva.CheckedChanged

        If Not initted Then Return
        If EnableDiva.Checked Then Settings.CMS = DreamGrid

    End Sub

    Private Sub EnableJOpensim_CheckedChanged(sender As Object, e As EventArgs) Handles EnableJOpensim.CheckedChanged

        If Not initted Then Return

        If Not EnableJOpensim.Checked Then Return

        Dim installed As Boolean = Joomla.IsjOpensimInstalled()

        If Not installed Then
            MsgBox("That folder has no content. Install Joomla and JOpensim first, then enable this.")
            EnableDiva.Checked = True
            Return
        End If

        If EnableJOpensim.Checked Then Settings.CMS = JOpensim

    End Sub

    Private Sub EnableOther_CheckedChanged(sender As Object, e As EventArgs) Handles EnableOther.CheckedChanged

        If Not initted Then Return
        If Not EnableOther.Checked Then Return

        Dim Exist As Boolean
        Try
            If Other.Text.Length > 0 Then
                Dim folders() = IO.Directory.GetFiles(IO.Path.Combine(Settings.CurrentDirectory, "Outworldzfiles\Apache\htdocs\" & Other.Text))
                If folders.Length > 1 Then
                    Exist = True
                End If
            End If
        Catch
        End Try

        If Not Exist Then
            MsgBox("That folder has no content. Install a CMS and then enable 'Other'.")
            EnableDiva.Checked = True
            Return
        End If

        If EnableOther.Checked Then
            Settings.CMS = Other.Text
        End If

    End Sub

    Private Sub EnableWP_CheckedChanged(sender As Object, e As EventArgs) Handles EnableWP.CheckedChanged

        If Not initted Then Return
        If Not EnableWP.Checked Then Return

        Dim Exist As Boolean
        Try
            Dim folders() = IO.Directory.GetFiles(IO.Path.Combine(Settings.CurrentDirectory, "Outworldzfiles\Apache\htdocs\WordPress"))
            If folders.Length > 1 Then
                Exist = True
            End If
        Catch
        End Try

        If Not Exist Then
            MsgBox("That folder has no content. Install WordPress then enable WordPress.")
            EnableDiva.Checked = True
            Return
        End If

        If EnableWP.Checked Then Settings.CMS = WordPress

    End Sub

    Private Sub Other_TextChanged(sender As Object, e As EventArgs) Handles Other.TextChanged

        If Not initted Then Return
        Settings.CMS = Other.Text

    End Sub

    Private Sub Sitemap_CheckedChanged(sender As Object, e As EventArgs) Handles Sitemap.CheckedChanged

        If Not initted Then Return
        Settings.SiteMap = Sitemap.Checked

    End Sub

    Private Sub HelpToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HelpToolStripMenuItem.Click
        HelpManual("Apache")
    End Sub

#End Region

End Class
