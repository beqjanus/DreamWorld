#Region "Copyright AGPL3.0"

' Copyright Outworldz, LLC. AGPL3.0 https://opensource.org/licenses/AGPL

#End Region

Public Class FormDisplacement

#Region "ScreenSize"

    Private ReadOnly Handler As New EventHandler(AddressOf Resize_page)
    Private ReadOnly PicClick As New EventHandler(AddressOf PictureBox_Click)
    Private _screenPosition As ScreenPos
    Private RUUID As String

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

        RUUID = RegionUUID
        If map Then
            ToolStrip1.Visible = True
            If PropRegionClass.IsBooted(RUUID) Then
                ExportToolStripMenuItem.Visible = True
            Else
                ExportToolStripMenuItem.Visible = False
            End If
            HelpToolStripMenuItem.Visible = True
        Else
            ToolStrip1.Visible = False
            If PropRegionClass.IsBooted(RUUID) Then
                ExportToolStripMenuItem.Visible = True
            Else
                ExportToolStripMenuItem.Visible = False
            End If
            HelpToolStripMenuItem.Visible = False
        End If

        Dim RegionName = PropRegionClass.RegionName(RUUID)
        Me.Width = (Size * 256) + 60
        Me.Height = (Size * 256) + 100
        If map Then Me.Height += 80 ' for buttons

        Dim intX As Integer
        Dim intY As Integer

        intX = Screen.PrimaryScreen.WorkingArea.Width
        intY = Screen.PrimaryScreen.WorkingArea.Height

        ' Cascade
        Me.Left = MapX
        Me.Top = MapY

        Debug.Print("Top:" & CStr(Me.Top))
        Debug.Print("Left:" & CStr(Me.Left))

        If (Me.Top + Me.Height) > intY Then
            Me.Top = 100
            MapY = 100
        End If

        If (Me.Left + Me.Width) > intX Then
            Me.Left = 100
            MapX = 100
        End If

        MapY += 100
        MapX += 100

        Me.Text = RegionName & " " & CStr(Size) + " X " & CStr(Size)
        Me.Name = "FormDisplacement_" & RUUID
        MakeArray(Size, map)

    End Sub

#End Region

#Region "Private Methods"

    Private Sub FormDisplacement_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        TerrainToolstrip.Text = Global.Outworldz.My.Resources.Terrain_word
        ClearObjects.Text = Global.Outworldz.My.Resources.Clear_and_Load_word
        LoadTerrainMenu.Text = Global.Outworldz.My.Resources.Load_Terrain
        ClearObjects.ToolTipText = Global.Outworldz.My.Resources.Clear_Terrain_tooltip
        LoadTerrainMenu.ToolTipText = Global.Outworldz.My.Resources.Load_Terrain_tooltip

        'help
        HelpToolStripMenuItem.Image = Global.Outworldz.My.Resources.question_and_answer
        HelpToolStripMenuItem.Text = Global.Outworldz.My.Resources.Help_word

        'parcel
        IgnoreParcels.Text = Global.Outworldz.My.Resources.Ignore_Parcel_word
        MergeParcelsMenu.Text = Global.Outworldz.My.Resources.Load_Parcel
        ParcelToolstrip.Text = Global.Outworldz.My.Resources.Parcels
        MergeParcelsMenu.ToolTipText = Global.Outworldz.My.Resources.MergeParcels_tooltip
        ParcelToolstrip.ToolTipText = Global.Outworldz.My.Resources.IgnoreParcels_tooltip

        'terrain
        TerrainToolstrip.Text = Global.Outworldz.My.Resources.Terrain_word
        IgnoreTerrainMenu.Text = Global.Outworldz.My.Resources.Ignore_Terrain_word
        OwnerMenu.Text = Global.Outworldz.My.Resources.Set_Owner_word
        OwnerMenu.ToolTipText = Global.Outworldz.My.Resources.Set_Owner_tooltip

        'objects
        Objects.Text = Global.Outworldz.My.Resources.Objects_word
        MergeObject.Text = Global.Outworldz.My.Resources.Merge_Objects_word
        ClearObjects.Text = Global.Outworldz.My.Resources.Clear_and_Load_word
        ClearObjects.ToolTipText = Global.Outworldz.My.Resources.Clear_objects_tooltip
        MergeObject.ToolTipText = Global.Outworldz.My.Resources.Merge_objectstooltip

        PropSelectedBox = ""
        If PropForceParcel Then
            MergeParcelsMenu.Checked = True
            IgnoreParcels.Checked = False
        Else
            MergeParcelsMenu.Checked = False
            IgnoreParcels.Checked = True
        End If

        If PropForceTerrain Then
            LoadTerrainMenu.Checked = True
            IgnoreTerrainMenu.Checked = False
        Else
            LoadTerrainMenu.Checked = False
            IgnoreTerrainMenu.Checked = True
        End If

        If PropForceMerge Then
            MergeObject.Checked = True
            ClearObjects.Checked = False
        Else
            MergeObject.Checked = False
            ClearObjects.Checked = True
        End If

        HelpOnce("Load OAR")

    End Sub

    Private Sub HelpToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HelpToolStripMenuItem.Click
        HelpManual("Load OAR")
    End Sub

    <CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")>
    Private Sub PictureBox_Click(sender As Object, e As EventArgs)

        If sender Is Nothing Then Return

        Try
            If sender.Tag Is Nothing Then Me.Close()
            Dim tag As String = sender.Tag.ToString
            PropSelectedBox = " --displacement " & tag & " "
        Catch ex As Exception
        End Try

        Close()

    End Sub

#End Region

#Region "Photo"

    Private Sub MakeArray(size As Integer, Optional map As Boolean = True)

        Dim StartAt = 256 * (size - 1)
        For Y = 0 To size - 1

            Dim OffsetY = 20
            If map Then
                OffsetY += 80
            End If
            For X = 0 To size - 1
                Dim OffsetX = 20

                Dim Name As String = $"PictureBox{CStr(X)}{CStr(Y)}"
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
                    PictureBox.Image = MakePhotoOfRegion(X, Y)
                    PictureBox.Tag = "<" & str & ",0>"
                Else
                    PictureBox.Image = MakePhotoOfRegion(X, Y)
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

    Private Function MakePhotoOfRegion(X As Integer, Y As Integer) As Image

        'map-1-1000-1000-objects
        Dim Xcoord = PropRegionClass.CoordX(RUUID) + X
        Dim Ycoord = PropRegionClass.CoordY(RUUID) + Y

        Dim place As String = "map-1-" & Xcoord & "-".ToUpperInvariant & Ycoord & "-objects.jpg"
        Dim RegionPhoto = $"{Settings.OpensimBinPath}maptiles\00000000-0000-0000-0000-000000000000\{place}"
        Debug.Print(RegionPhoto)

        Dim Pic As Image
        Try
            Pic = Bitmap.FromFile(RegionPhoto)
        Catch ex As Exception
            Pic = Global.Outworldz.My.Resources.water
        End Try

        Return Pic

    End Function

    Private Sub OnPrintPage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs)

        'create a memory bitmap and size to the form
        Using bmp As Bitmap = New Bitmap(Me.Width, Me.Height)

            'draw the form on the memory bitmap
            Me.DrawToBitmap(bmp, New Rectangle(0, 0, Me.Width, Me.Height))

            'draw the form image on the printer graphics sized and centered to margins
            Dim ratio As Single = CSng(bmp.Width / bmp.Height)

            If ratio > e.MarginBounds.Width / e.MarginBounds.Height Then
                e.Graphics.DrawImage(bmp,
                e.MarginBounds.Left,
                CInt(e.MarginBounds.Top + (e.MarginBounds.Height / 2) - ((e.MarginBounds.Width / ratio) / 2)),
                e.MarginBounds.Width,
                CInt(e.MarginBounds.Width / ratio))
            Else
                e.Graphics.DrawImage(bmp,
                CInt(e.MarginBounds.Left + (e.MarginBounds.Width / 2) - (e.MarginBounds.Height * ratio / 2)),
                e.MarginBounds.Top,
                CInt(e.MarginBounds.Height * ratio),
                e.MarginBounds.Height)
            End If

        End Using

    End Sub

    Private Sub PrintToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PrintToolStripMenuItem.Click

        PrintToolStripMenuItem.Visible = False
        Dim x As FormBorderStyle = Me.FormBorderStyle
        Me.FormBorderStyle = FormBorderStyle.None

        Dim preview As New PrintPreviewDialog
        Dim pd As New System.Drawing.Printing.PrintDocument
        pd.DefaultPageSettings.Landscape = True
        AddHandler pd.PrintPage, AddressOf OnPrintPage
        preview.Document = pd
        preview.ShowDialog()

        preview.Dispose()
        PrintToolStripMenuItem.Visible = True
        Me.FormBorderStyle = x

    End Sub

#End Region

#Region "Toolbars"

    Private Sub ClearAndLoadToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClearObjects.Click

        ClearObjects.Checked = True
        MergeObject.Checked = False
        PropForceMerge = False

    End Sub

    Private Sub ExportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportToolStripMenuItem.Click

        'export-map [<path>] - Save an image of the world map (default name is exportmap.jpg)

        'Create an instance of the open file dialog box.
        Using openFileDialog1 As FolderBrowserDialog = New FolderBrowserDialog With {
            .ShowNewFolderButton = True,
            .Description = Global.Outworldz.My.Resources.Choose_a_Folder_word
        }
            Dim UserClickedOK As DialogResult = openFileDialog1.ShowDialog
            ' Process input if the user clicked OK.
            If UserClickedOK = DialogResult.OK Then
                Dim thing = openFileDialog1.SelectedPath
                If thing.Length > 0 Then
                    thing = IO.Path.Combine(thing, PropRegionClass.RegionName(RUUID))
                    thing += ".jpg"
                    RPC_Region_Command(RUUID, $"export-map ""{thing}""")
                End If
            End If
        End Using

    End Sub

    Private Sub IgnoreParcelsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles IgnoreParcels.Click

        PropForceParcel = False
        MergeParcelsMenu.Checked = False
        IgnoreParcels.Checked = True

    End Sub

    Private Sub IgnoreTerrainToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles IgnoreTerrainMenu.Click

        PropForceTerrain = False
        LoadTerrainMenu.Checked = False
        IgnoreTerrainMenu.Checked = True

    End Sub

    Private Sub LoadTerrainToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoadTerrainMenu.Click

        PropForceTerrain = True
        LoadTerrainMenu.Checked = True
        IgnoreTerrainMenu.Checked = False

    End Sub

    Private Sub MergeObject_Click(sender As Object, e As EventArgs) Handles MergeObject.Click

        ClearObjects.Checked = False
        MergeObject.Checked = True
        PropForceMerge = True

    End Sub

    Private Sub MergeParcelsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MergeParcelsMenu.Click

        PropForceParcel = True
        MergeParcelsMenu.Checked = True
        IgnoreParcels.Checked = False

    End Sub

    Private Sub ToolStripButton5_Click(sender As Object, e As EventArgs) Handles OwnerMenu.Click
        PropUserName = InputBox(My.Resources.UnassignedPerson, "")
    End Sub

#End Region

End Class
