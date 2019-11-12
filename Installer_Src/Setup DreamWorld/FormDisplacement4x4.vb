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

Public Class FormDisplacement

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

    Private Sub ClearOARToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClearOARToolStripMenuItem.Click

        Form1.PropForceMerge = False
        MergeOARToolStripMenuItem.Checked = False
        ClearOARToolStripMenuItem.Checked = True

    End Sub

    Private Sub ForceTerrainToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ForceTerrainToolStripMenuItem.Click

        Form1.PropForceTerrain = True
        ForceTerrainToolStripMenuItem.Checked = True
        ClearTerrainToolStripMenuItem.Checked = False

    End Sub

    Private Sub FormDisplacement_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetScreen()
        Form1.PropSelectedBox = ""

        If Form1.PropForceParcel Then
            LoadParcelToolStripMenuItem.Checked = True
            IgnoreParcelToolStripMenuItem.Checked = False
        Else
            LoadParcelToolStripMenuItem.Checked = False
            IgnoreParcelToolStripMenuItem.Checked = True

        End If

        If Form1.PropForceTerrain Then
            ForceTerrainToolStripMenuItem.Checked = True
            ClearTerrainToolStripMenuItem.Checked = False
        Else
            ForceTerrainToolStripMenuItem.Checked = False
            ClearTerrainToolStripMenuItem.Checked = True
        End If

        If Form1.PropForceMerge Then
            MergeOARToolStripMenuItem.Checked = True
            ClearOARToolStripMenuItem.Checked = False
        Else
            MergeOARToolStripMenuItem.Checked = False
            ClearOARToolStripMenuItem.Checked = True
        End If
        Form1.HelpOnce("Load OAR")
    End Sub

    Private Sub HrlpToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HrlpToolStripMenuItem.Click
        Form1.Help("Load OAR")
    End Sub

    Private Sub IgnoreParcelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles IgnoreParcelToolStripMenuItem.Click

        Form1.PropForceParcel = False
        LoadParcelToolStripMenuItem.Checked = False
        IgnoreParcelToolStripMenuItem.Checked = True

    End Sub

    Private Sub LoadParcelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoadParcelToolStripMenuItem.Click

        Form1.PropForceParcel = True
        LoadParcelToolStripMenuItem.Checked = True
        IgnoreParcelToolStripMenuItem.Checked = False

    End Sub

    Private Sub MergeOARToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MergeOARToolStripMenuItem.Click

        Form1.PropForceMerge = True
        MergeOARToolStripMenuItem.Checked = True
        ClearOARToolStripMenuItem.Checked = False

    End Sub

    Private Sub OriginalTererainToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClearTerrainToolStripMenuItem.Click

        Form1.PropForceTerrain = False
        ForceTerrainToolStripMenuItem.Checked = False
        ClearTerrainToolStripMenuItem.Checked = True

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Form1.PropSelectedBox = " --displacement <0,768,0> "
        Me.Close()
    End Sub

    Private Sub PictureBox10_Click(sender As Object, e As EventArgs) Handles PictureBox10.Click
        Form1.PropSelectedBox = " --displacement <512,256,0> "
        Me.Close()
    End Sub

    Private Sub PictureBox11_Click(sender As Object, e As EventArgs) Handles PictureBox11.Click
        Form1.PropSelectedBox = " --displacement <256,256,0> "
        Me.Close()
    End Sub

    Private Sub PictureBox12_Click(sender As Object, e As EventArgs) Handles PictureBox12.Click
        Form1.PropSelectedBox = " --displacement <0,256,0> "
        Me.Close()
    End Sub

    Private Sub PictureBox13_Click(sender As Object, e As EventArgs) Handles PictureBox13.Click
        Form1.PropSelectedBox = " --displacement <768,0,0> "
        Me.Close()
    End Sub

    Private Sub PictureBox14_Click(sender As Object, e As EventArgs) Handles PictureBox14.Click
        Form1.PropSelectedBox = " --displacement <512,0,0> "
        Me.Close()
    End Sub

    Private Sub PictureBox15_Click(sender As Object, e As EventArgs) Handles PictureBox15.Click
        Form1.PropSelectedBox = " --displacement <256,0,0> "
        Me.Close()
    End Sub

    Private Sub PictureBox16_Click(sender As Object, e As EventArgs) Handles PictureBox16.Click
        Form1.PropSelectedBox = " --displacement <0,0,0>  "
        Me.Close()
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        Form1.PropSelectedBox = " --displacement <256,768,0> "
        Me.Close()
    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        Form1.PropSelectedBox = " --displacement <512,768,0> "
        Me.Close()
    End Sub

    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click
        Form1.PropSelectedBox = " --displacement <768,768,0> "
        Me.Close()
    End Sub

    Private Sub PictureBox5_Click(sender As Object, e As EventArgs) Handles PictureBox5.Click
        Form1.PropSelectedBox = " --displacement <768,512,0> "
        Me.Close()
    End Sub

    Private Sub PictureBox6_Click(sender As Object, e As EventArgs) Handles PictureBox6.Click
        Form1.PropSelectedBox = " --displacement <512,512,0> "
        Me.Close()
    End Sub

    Private Sub PictureBox7_Click(sender As Object, e As EventArgs) Handles PictureBox7.Click
        Form1.PropSelectedBox = " --displacement <256,512,0> "
        Me.Close()
    End Sub

    Private Sub PictureBox8_Click(sender As Object, e As EventArgs) Handles PictureBox8.Click
        Form1.PropSelectedBox = " --displacement <0,512,0> "
        Me.Close()
    End Sub

    Private Sub PictureBox9_Click(sender As Object, e As EventArgs) Handles PictureBox9.Click
        Form1.PropSelectedBox = " --displacement <768,256,0> "
        Me.Close()
    End Sub

    Private Sub SetOwnerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SetOwnerToolStripMenuItem.Click
        Form1.PropUserName = InputBox(My.Resources.UnassignedPerson, "")
    End Sub

#End Region

End Class
