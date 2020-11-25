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

Imports System.IO

Public Class TosForm

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

#Region "Private Methods"

    Private Sub ApplyButton_Click(sender As Object, e As EventArgs) Handles ApplyButton.Click

        Try
            Using outputFile As New StreamWriter(IO.Path.Combine(FileSystem.CurDir() & "\tos.html"))
                outputFile.WriteLine(Editor1.BodyHtml)
            End Using

            Using outputFile As New StreamWriter(IO.Path.Combine(FileSystem.CurDir(), "Outworldzfiles\opensim\bin\WifiPages\tos.html"))
                outputFile.WriteLine(Editor1.BodyHtml)
            End Using
        Catch ex As Exception

            BreakPoint.Show(ex.Message)
        End Try

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If FormSetup.PropOpensimIsRunning() Then
            Dim webAddress As String = "http://" & CStr(Settings.PublicIP) & ":" & CStr(Settings.HttpPort) & "/wifi/termsofservice.html"
            Try
                Process.Start(webAddress)
            Catch ex As Exception

                BreakPoint.Show(ex.Message)
            End Try
        Else
            MsgBox(My.Resources.Not_Running)
        End If

    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles ShowToLocalUsersCheckbox.CheckedChanged

        Settings.ShowToLocalUsers = ShowToLocalUsersCheckbox.Checked
        Settings.SaveSettings()

    End Sub

    Private Sub DatabaseSetupToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DatabaseSetupToolStripMenuItem.Click

        HelpManual("TOS")

    End Sub

    Private Sub Form1_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed

        'nothing

    End Sub

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Translate.Run(Name)
        Dim reader As System.IO.StreamReader
        reader = System.IO.File.OpenText(IO.Path.Combine(FileSystem.CurDir(), "tos.html"))
        'now loop through each line
        Dim HTML As String = ""
        While reader.Peek <> -1
            HTML = HTML + reader.ReadLine() + vbCrLf
        End While
        reader.Close()
        Editor1.BodyHtml = HTML

        ShowToLocalUsersCheckbox.Checked = Settings.ShowToLocalUsers
        ShowToHGUsersCheckbox.Checked = Settings.ShowToForeignUsers
        TOSEnable.Checked = Settings.TOSEnabled
        SetScreen()

        HelpOnce("TOS")

    End Sub

    Private Sub SaveButton_Click(sender As Object, e As EventArgs) Handles SaveButton.Click

        Try
            Using outputFile As New StreamWriter(IO.Path.Combine(FileSystem.CurDir(), "tos.html"))
                outputFile.WriteLine(Editor1.BodyHtml)
            End Using

            Using outputFile As New StreamWriter(IO.Path.Combine(FileSystem.CurDir(), "Outworldzfiles\opensim\bin\WifiPages\tos.html"))
                outputFile.WriteLine(Editor1.BodyHtml)
            End Using
        Catch ex As Exception

            BreakPoint.Show(ex.Message)
        End Try

        Me.Close()

    End Sub

    Private Sub ShowToHGUsersCheckbox_CheckedChanged(sender As Object, e As EventArgs) Handles ShowToHGUsersCheckbox.CheckedChanged

        Settings.ShowToForeignUsers = ShowToHGUsersCheckbox.Checked
        Settings.SaveSettings()

    End Sub

    Private Sub TOSEnable_CheckedChanged(sender As Object, e As EventArgs) Handles TOSEnable.CheckedChanged

        Settings.TOSEnabled = TOSEnable.Checked
        Settings.SaveSettings()

    End Sub

#End Region

End Class
