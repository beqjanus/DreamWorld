Public Class FormDisplacement2x2

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

    Private Sub FormDisplacement_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        SetScreen()

        Form1.pSelectedBox = ""
        If Form1.pForceParcel Then
            LoadParcelToolStripMenuItem.Checked = True
            IgnoreParcelToolStripMenuItem.Checked = False
        Else
            LoadParcelToolStripMenuItem.Checked = False
            IgnoreParcelToolStripMenuItem.Checked = True

        End If

        If Form1.pForceTerrain Then
            ForceTerrainToolStripMenuItem.Checked = True
            OriginalTererainToolStripMenuItem.Checked = False
        Else
            ForceTerrainToolStripMenuItem.Checked = False
            OriginalTererainToolStripMenuItem.Checked = True
        End If

        If Form1.pForceMerge Then
            MergeOARToolStripMenuItem.Checked = True
            ClearOARToolStripMenuItem.Checked = False
        Else
            MergeOARToolStripMenuItem.Checked = False
            ClearOARToolStripMenuItem.Checked = True
        End If

        Form1.HelpOnce("Load OAR")

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Form1.pSelectedBox = " --displacement <0,256,0> "
        Me.Close()
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        Form1.pSelectedBox = " --displacement <256,256,0> "
        Me.Close()
    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        Form1.pSelectedBox = " --displacement <0,0,0>  "
        Me.Close()
    End Sub

    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click
        Form1.pSelectedBox = " --displacement <256,0,0> "
        Me.Close()
    End Sub

    Private Sub ClearOARToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClearOARToolStripMenuItem.Click

        Form1.pForceMerge = False
        MergeOARToolStripMenuItem.Checked = False
        ClearOARToolStripMenuItem.Checked = True

    End Sub

    Private Sub MergeOARToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MergeOARToolStripMenuItem.Click

        Form1.pForceMerge = True
        MergeOARToolStripMenuItem.Checked = True
        ClearOARToolStripMenuItem.Checked = False

    End Sub

    Private Sub ForceTerrainToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ForceTerrainToolStripMenuItem.Click

        Form1.pForceTerrain = True
        ForceTerrainToolStripMenuItem.Checked = True
        OriginalTererainToolStripMenuItem.Checked = False

    End Sub

    Private Sub OriginalTererainToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OriginalTererainToolStripMenuItem.Click

        Form1.pForceTerrain = False
        ForceTerrainToolStripMenuItem.Checked = False
        OriginalTererainToolStripMenuItem.Checked = True

    End Sub

    Private Sub LoadParcelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoadParcelToolStripMenuItem.Click

        Form1.pForceParcel = True
        LoadParcelToolStripMenuItem.Checked = True
        IgnoreParcelToolStripMenuItem.Checked = False

    End Sub

    Private Sub IgnoreParcelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles IgnoreParcelToolStripMenuItem.Click

        Form1.pForceParcel = False
        LoadParcelToolStripMenuItem.Checked = False
        IgnoreParcelToolStripMenuItem.Checked = True

    End Sub

    Private Sub HelpToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HelpToolStripMenuItem.Click
        Form1.Help("Load OAR")
    End Sub

    Private Sub SetOwnerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SetOwnerToolStripMenuItem.Click
        Form1.pUserName = InputBox("Enter the First and Last name who will own any unassigned objects", "")
    End Sub

End Class