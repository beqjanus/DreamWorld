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

Public Class FormPhysics

#Region "Private Fields"

    Dim initted As Boolean

#End Region

#Region "ScreenSize"

    Private ReadOnly Handler As New EventHandler(AddressOf Resize_page)
    Private _screenPosition As ScreenPos

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

    Private Sub Physics_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        DatabaseSetupToolStripMenuItem.Image = Global.Outworldz.My.Resources.about
        DatabaseSetupToolStripMenuItem.Text = Global.Outworldz.My.Resources.Help_word
        GroupBox1.Text = Global.Outworldz.My.Resources.Physics_Engine
        MenuStrip2.Text = Global.Outworldz.My.Resources._0
        PhysicsNone.Text = Global.Outworldz.My.Resources.None
        PhysicsSeparate.Text = Global.Outworldz.My.Resources.BP
        PhysicsubODE.Text = Global.Outworldz.My.Resources.UBODE_words
        Text = Global.Outworldz.My.Resources.Physics_word
        ToolStripMenuItem30.Image = Global.Outworldz.My.Resources.question_and_answer
        ToolStripMenuItem30.Text = Global.Outworldz.My.Resources.Help_word

        SetScreen()

        Select Case Settings.Physics
            Case 0 : PhysicsNone.Checked = True
            Case 1 : PhysicsSeparate.Checked = True
            Case 2 : PhysicsSeparate.Checked = True
            Case 3 : PhysicsSeparate.Checked = True
            Case 4 : PhysicsubODE.Checked = True
            Case 5 : PhysicsubODE.Checked = True
            Case Else : PhysicsSeparate.Checked = True
        End Select

        HelpOnce("Physics")
        initted = True

    End Sub

#End Region

#Region "Physics"

    Private Sub DatabaseSetupToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DatabaseSetupToolStripMenuItem.Click
        HelpManual("Physics")
    End Sub

    Private Sub GodHelp_Click(sender As Object, e As EventArgs)
        HelpManual("Physics")
    End Sub

    Private Sub IsClosed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Closed

        FormSetup.PropViewedSettings = True
        Settings.SaveSettings()

    End Sub

    Private Sub PhysicsNone_CheckedChanged(sender As Object, e As EventArgs) Handles PhysicsNone.CheckedChanged
        If Not initted Then Return
        If PhysicsNone.Checked Then
            Settings.Physics = 0
            Settings.SaveSettings()
        End If
    End Sub

    Private Sub PhysicsSeparate_CheckedChanged(sender As Object, e As EventArgs) Handles PhysicsSeparate.CheckedChanged
        If Not initted Then Return
        If PhysicsSeparate.Checked Then
            Settings.Physics = 3
            Settings.SaveSettings()
        End If
    End Sub

    Private Sub PhysicsubODE_CheckedChanged(sender As Object, e As EventArgs) Handles PhysicsubODE.CheckedChanged
        If Not initted Then Return
        If PhysicsubODE.Checked Then
            Settings.Physics = 4
            Settings.SaveSettings()
        End If
    End Sub

#End Region

End Class
