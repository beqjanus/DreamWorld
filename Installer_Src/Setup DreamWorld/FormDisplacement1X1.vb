Public Class FormDisplacement1X1

#Region "ScreenSize"
    Public ScreenPosition As ScreenPos
    Private Handler As New EventHandler(AddressOf Resize_page)

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
    Private Sub FormDisplacement_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        SetScreen()

        Form1.gSelectedBox = ""
        If Form1.gForceParcel Then
            LoadParcelToolStripMenuItem.Checked = True
            IgnoreParcelToolStripMenuItem.Checked = False
        Else
            LoadParcelToolStripMenuItem.Checked = False
            IgnoreParcelToolStripMenuItem.Checked = True

        End If

        If Form1.gForceTerrain Then
            ForceTerrainToolStripMenuItem.Checked = True
            OriginalTererainToolStripMenuItem.Checked = False
        Else
            ForceTerrainToolStripMenuItem.Checked = False
            OriginalTererainToolStripMenuItem.Checked = True
        End If

        If Form1.gForceMerge Then
            MergeOARToolStripMenuItem.Checked = True
            ClearOARToolStripMenuItem.Checked = False
        Else
            MergeOARToolStripMenuItem.Checked = False
            ClearOARToolStripMenuItem.Checked = True
        End If

        Form1.HelpOnce("Load OAR")

    End Sub

    Public Sub Init(RegionNumber As Integer)

        'Dim RegionPhoto = New RegionPhoto(Name)

        'map-1-1000-1000-objects
        Dim Xcoord = Form1.RegionClass.CoordX(RegionNumber)
        Dim Ycoord = Form1.RegionClass.CoordY(RegionNumber)

        Dim place As String = "map-1-" & Xcoord & "-" & Ycoord & "-objects.jpg"
        Dim RegionPhoto = Form1.gOpensimBinPath & "\bin\maptiles\00000000-0000-0000-0000-000000000000\" & place
        Try
            Dim Pic As Image = Bitmap.FromFile(RegionPhoto)
            PictureBox3.Image = Pic
            Pic = Nothing
        Catch ex As Exception
            PictureBox3.Image = My.Resources.water
        End Try


    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        Form1.gSelectedBox = " --displacement <0,0,0>  "
        Me.Close()
    End Sub


    Private Sub ClearOARToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClearOARToolStripMenuItem.Click

        Form1.gForceMerge = False
        MergeOARToolStripMenuItem.Checked = False
        ClearOARToolStripMenuItem.Checked = True

    End Sub

    Private Sub MergeOARToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MergeOARToolStripMenuItem.Click

        Form1.gForceMerge = True
        MergeOARToolStripMenuItem.Checked = True
        ClearOARToolStripMenuItem.Checked = False

    End Sub

    Private Sub ForceTerrainToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ForceTerrainToolStripMenuItem.Click

        Form1.gForceTerrain = True
        ForceTerrainToolStripMenuItem.Checked = True
        OriginalTererainToolStripMenuItem.Checked = False

    End Sub

    Private Sub OriginalTererainToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OriginalTererainToolStripMenuItem.Click

        Form1.gForceTerrain = False
        ForceTerrainToolStripMenuItem.Checked = False
        OriginalTererainToolStripMenuItem.Checked = True

    End Sub

    Private Sub LoadParcelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoadParcelToolStripMenuItem.Click

        Form1.gForceParcel = True
        LoadParcelToolStripMenuItem.Checked = True
        IgnoreParcelToolStripMenuItem.Checked = False

    End Sub

    Private Sub IgnoreParcelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles IgnoreParcelToolStripMenuItem.Click

        Form1.gForceParcel = False
        LoadParcelToolStripMenuItem.Checked = False
        IgnoreParcelToolStripMenuItem.Checked = True

    End Sub

    Private Sub HelpToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HelpToolStripMenuItem.Click
        Form1.Help("Load OAR")
    End Sub

    Private Sub SetOwnerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SetOwnerToolStripMenuItem.Click
        Form1.gUserName = InputBox("Enter the First and Last name who will own any unassigned objects", "")
    End Sub


End Class