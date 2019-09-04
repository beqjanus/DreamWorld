#Region "Copyright"

' Copyright 2014 Fred Beckhusen for www.Outworldz.com https://opensource.org/licenses/AGPL

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

    Dim initted As Boolean

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

    Private Sub Physics_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        SetScreen()

        Select Case Form1.PropMySetting.Physics
            Case 0 : PhysicsNone.Checked = True
            Case 1 : PhysicsODE.Checked = True
            Case 2 : PhysicsBullet.Checked = True
            Case 3 : PhysicsSeparate.Checked = True
            Case 4 : PhysicsubODE.Checked = True
            Case 5 : PhysicsHybrid.Checked = True
            Case Else : PhysicsSeparate.Checked = True
        End Select

        Form1.HelpOnce("Physics")
        initted = True

    End Sub

#Region "Physics"

    Private Sub IsClosed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Closed

        Form1.PropViewedSettings = True
        Form1.PropMySetting.SaveSettings()

    End Sub

    Private Sub PhysicsNone_CheckedChanged(sender As Object, e As EventArgs) Handles PhysicsNone.CheckedChanged
        If Not initted Then Return
        If PhysicsNone.Checked Then
            Form1.PropMySetting.Physics = 0
            Form1.PropMySetting.SaveSettings()
        End If
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles PhysicsODE.CheckedChanged
        If Not initted Then Return
        If PhysicsODE.Checked Then
            Form1.PropMySetting.Physics = 1
            Form1.PropMySetting.SaveSettings()
        End If
    End Sub

    Private Sub PhysicsBullet_CheckedChanged(sender As Object, e As EventArgs) Handles PhysicsBullet.CheckedChanged
        If Not initted Then Return
        If PhysicsBullet.Checked Then
            Form1.PropMySetting.Physics = 2
            Form1.PropMySetting.SaveSettings()
        End If
    End Sub

    Private Sub PhysicsSeparate_CheckedChanged(sender As Object, e As EventArgs) Handles PhysicsSeparate.CheckedChanged
        If Not initted Then Return
        If PhysicsSeparate.Checked Then
            Form1.PropMySetting.Physics = 3
            Form1.PropMySetting.SaveSettings()
        End If
    End Sub

    Private Sub PhysicsubODE_CheckedChanged(sender As Object, e As EventArgs) Handles PhysicsubODE.CheckedChanged
        If Not initted Then Return
        If PhysicsubODE.Checked Then
            Form1.PropMySetting.Physics = 4
            Form1.PropMySetting.SaveSettings()
        End If
    End Sub

    Private Sub PhysicsHybrid_CheckedChanged(sender As Object, e As EventArgs) Handles PhysicsHybrid.CheckedChanged
        If Not initted Then Return
        If PhysicsHybrid.Checked Then
            Form1.PropMySetting.Physics = 5
            Form1.PropMySetting.SaveSettings()
        End If
    End Sub

    Private Sub GodHelp_Click(sender As Object, e As EventArgs) Handles GodHelp.Click
        Form1.Help("Physics")
    End Sub

    Private Sub DatabaseSetupToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DatabaseSetupToolStripMenuItem.Click
        Form1.Help("Physics")
    End Sub

#End Region

End Class