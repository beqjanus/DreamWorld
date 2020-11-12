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

Public Class FormDisplacement

#Region "ScreenSize"

    Private _screenPosition As ScreenPos
    Private Handler As New EventHandler(AddressOf Resize_page)
    Private PicClick As New EventHandler(AddressOf PictureBox_Click)

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

#Region "Public Methods"

    Public Sub Init(Size As Integer, RegionUUID As String, Optional map As Boolean = True)

        If map Then
            ToolStripMenuItem1.Visible = True
            HelpToolStripMenuItem.Visible = True
        Else
            ToolStripMenuItem1.Visible = False
            HelpToolStripMenuItem.Visible = False
        End If

        Me.Width = Size * 256 + 60
        Me.Height = Size * 256 + 100

        Dim RegionName = FormSetup.PropRegionClass.RegionName(RegionUUID)
        Me.Text = RegionName & " " & CStr(Size) + " X " & CStr(Size)
        Me.Name = "FormDisplacement_" & RegionUUID
        MakeArray(Size, RegionUUID, map)

    End Sub

#End Region

#Region "Private Methods"

    Private Sub ClearOARToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClearOARToolStripMenuItem.Click

        FormSetup.PropForceMerge = False
        MergeOARToolStripMenuItem.Checked = False
        ClearOARToolStripMenuItem.Checked = True

    End Sub

    Private Sub ForceTerrainToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ForceTerrainToolStripMenuItem.Click

        FormSetup.PropForceTerrain = True
        ForceTerrainToolStripMenuItem.Checked = True
        OriginalTererainToolStripMenuItem.Checked = False

    End Sub

    Private Sub FormDisplacement_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        SetScreen()

        FormSetup.PropSelectedBox = ""
        If FormSetup.PropForceParcel Then
            LoadParcelToolStripMenuItem.Checked = True
            IgnoreParcelToolStripMenuItem.Checked = False
        Else
            LoadParcelToolStripMenuItem.Checked = False
            IgnoreParcelToolStripMenuItem.Checked = True

        End If

        If FormSetup.PropForceTerrain Then
            ForceTerrainToolStripMenuItem.Checked = True
            OriginalTererainToolStripMenuItem.Checked = False
        Else
            ForceTerrainToolStripMenuItem.Checked = False
            OriginalTererainToolStripMenuItem.Checked = True
        End If

        If FormSetup.PropForceMerge Then
            MergeOARToolStripMenuItem.Checked = True
            ClearOARToolStripMenuItem.Checked = False
        Else
            MergeOARToolStripMenuItem.Checked = False
            ClearOARToolStripMenuItem.Checked = True
        End If

        HelpOnce("Load OAR")

    End Sub

    Private Sub HelpToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HelpToolStripMenuItem.Click
        HelpManual("Load OAR")
    End Sub

    Private Sub IgnoreParcelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles IgnoreParcelToolStripMenuItem.Click

        FormSetup.PropForceParcel = False
        LoadParcelToolStripMenuItem.Checked = False
        IgnoreParcelToolStripMenuItem.Checked = True

    End Sub

    Private Sub LoadParcelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoadParcelToolStripMenuItem.Click

        FormSetup.PropForceParcel = True
        LoadParcelToolStripMenuItem.Checked = True
        IgnoreParcelToolStripMenuItem.Checked = False

    End Sub

    Private Sub MergeOARToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MergeOARToolStripMenuItem.Click

        FormSetup.PropForceMerge = True
        MergeOARToolStripMenuItem.Checked = True
        ClearOARToolStripMenuItem.Checked = False

    End Sub

    Private Sub OriginalTererainToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OriginalTererainToolStripMenuItem.Click

        FormSetup.PropForceTerrain = False
        ForceTerrainToolStripMenuItem.Checked = False
        OriginalTererainToolStripMenuItem.Checked = True

    End Sub

    Private Sub PictureBox_Click(sender As Object, e As EventArgs)

        Dim tag As String = sender.Tag.ToString
        FormSetup.PropSelectedBox = " --displacement " & tag & " "
        Me.Close()
    End Sub

    Private Sub SetOwnerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SetOwnerToolStripMenuItem.Click
        FormSetup.PropUserName = InputBox(My.Resources.UnassignedPerson, "")
    End Sub

#End Region

    Private Shared Function MakePhotoOfRegion(regionUUID As String, X As Integer, Y As Integer) As Image

        'map-1-1000-1000-objects
        Dim Xcoord = FormSetup.PropRegionClass.CoordX(regionUUID) + X
        Dim Ycoord = FormSetup.PropRegionClass.CoordY(regionUUID) + Y

        Dim place As String = "map-1-" & Xcoord & "-".ToUpperInvariant & Ycoord & "-objects.jpg"
        Dim RegionPhoto = Settings.OpensimBinPath & "maptiles\00000000-0000-0000-0000-000000000000\" & place
        Debug.Print(RegionPhoto)
        'RegionPhoto = "E:\Outworldz Dreamgrid\OutworldzFiles\Opensim\bin\maptiles\00000000-0000-0000-0000-000000000000\Anthony-ward-grid.jpg"
        Dim Pic As Image
        Try
            Pic = Bitmap.FromFile(RegionPhoto)
        Catch ex As Exception

            Pic = Global.Outworldz.My.Resources.water
        End Try

        Return Pic

    End Function

    Private Sub MakeArray(size As Integer, RegionUUID As String, Optional map As Boolean = True)

        Dim StartAt = 256 * (size - 1)
        For Y = 0 To size - 1
            Dim OffsetY = 20
            For X = 0 To size - 1
                Dim OffsetX = 20

                Dim Name = "PictureBox" & CStr(X) & CStr(Y)
                Dim PictureBox As New PictureBox With {
                    .BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
                }
                If map Then
                    PictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
                Else
                    PictureBox.BorderStyle = System.Windows.Forms.BorderStyle.None
                End If

                PictureBox.ErrorImage = Global.Outworldz.My.Resources.water
                PictureBox.InitialImage = Global.Outworldz.My.Resources.water

                If map Then
                    ' make an image of the region with X,Y text on it.
                    Dim str = CStr(X * 256) & "," & CStr(Y * 256)
                    PictureBox.Image = MakePhotoOfRegion(RegionUUID, X, Y)
                    PictureBox.Tag = "<" & str & ",0>"
                Else
                    PictureBox.Image = MakePhotoOfRegion(RegionUUID, X, Y)
                End If

                Dim X1 = OffsetX + (X * 256)
                Dim Y1 = OffsetY + StartAt - (Y * 256)
                PictureBox.Location = New System.Drawing.Point(X1, Y1)
                PictureBox.Margin = New System.Windows.Forms.Padding(0, 0, 0, 5)
                PictureBox.Name = Name
                PictureBox.Size = New System.Drawing.Size(256, 256)
                PictureBox.TabIndex = X + (Y * X)
                PictureBox.TabStop = False

                Me.Controls.Add(PictureBox)
                ToolTip1.SetToolTip(PictureBox, Global.Outworldz.My.Resources.Click_To_Load_Here)
                AddHandler PictureBox.Click, PicClick

                OffsetX += 256
            Next
            OffsetY += 256
        Next

    End Sub

End Class
