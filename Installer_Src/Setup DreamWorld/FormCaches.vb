Imports System.IO

Public Class FormCaches

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

    Private Sub Cache_load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        SetScreen()

    End Sub

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Form1.pOpensimIsRunning() Then
            CheckBox1.Enabled = True
            CheckBox2.Enabled = True

            CheckBox1.Checked = True
            CheckBox2.Checked = True
            CheckBox3.Checked = True
            CheckBox4.Checked = True
            CheckBox5.Checked = True
        Else
            CheckBox1.Enabled = False
            CheckBox2.Enabled = False
            CheckBox1.Checked = False
            CheckBox2.Checked = False
            CheckBox3.Checked = True
            CheckBox4.Checked = True
            CheckBox5.Checked = True
        End If
        Form1.HelpOnce("Cache")
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim Clr As New ClrCache()

        If CheckBox1.Checked Then
            Clr.WipeScripts()
        End If

        If CheckBox2.Checked Then
            Clr.WipeBakes()
        End If

        If CheckBox3.Checked Then
            Clr.WipeAssets()
        End If

        If CheckBox4.Checked Then
            Clr.WipeImage()
        End If
        If CheckBox5.Checked Then
            Clr.WipeMesh()
        End If

        If Not Form1.pOpensimIsRunning() Then
            Form1.Print("All Server Caches cleared")
        Else
            Form1.Print("All Server Caches except Scripts and Avatar bakes were cleared. Opensim must be stopped to clear script and bake caches.")
        End If

        Me.Close()

    End Sub

    Private Sub MapHelp_Click(sender As Object, e As EventArgs) Handles MapHelp.Click
        Form1.Help("Cache")
    End Sub

    Private Sub MoreCacheButton_Click(sender As Object, e As EventArgs) Handles MoreCacheButton.Click

        Dim Flotsam As New FormFlotsamCache
        Flotsam.Show()

    End Sub
End Class