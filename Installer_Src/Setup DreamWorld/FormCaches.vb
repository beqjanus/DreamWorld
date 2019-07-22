
Imports System.Text.RegularExpressions

Public Class FormCaches
    Private gInitted As Boolean = False
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

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        SetScreen()

        If Not Form1.PropOpensimIsRunning() Then
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
        Form1.HelpOnce("Flotsam Cache")

        CacheFolder.Text = Form1.PropMySetting.CacheFolder
        CacheEnabledBox.Checked = Form1.PropMySetting.CacheEnabled
        CacheTimeout.Text = Form1.PropMySetting.CacheTimeout
        LogLevelBox.SelectedIndex = CType(Form1.PropMySetting.CacheLogLevel, Integer)

        Dim folder As String
        If CacheFolder.Text = ".\assetcache" Then
            folder = Form1.PropMySetting.OpensimBinPath & "bin/assetcache"
        Else
            folder = CacheFolder.Text
        End If

        gInitted = True

        Form1.HelpOnce("Cache")
    End Sub

    Private Sub Form_unload() Handles Me.Closing

        Form1.PropMySetting.CacheLogLevel = LogLevelBox.SelectedIndex.ToString(Form1.Usa)
        Form1.PropMySetting.CacheFolder = CacheFolder.Text
        Form1.PropMySetting.CacheEnabled = CacheEnabledBox.Checked
        Form1.PropMySetting.CacheTimeout = CacheTimeout.Text

        Form1.PropMySetting.SaveSettings()

    End Sub


    Private Sub B_Click(sender As Object, e As EventArgs) Handles Button1.Click

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

        If Not Form1.PropOpensimIsRunning() Then
            Form1.Print("All Server Caches cleared")
        Else
            Form1.Print("All Server Caches except Scripts and Avatar bakes were cleared. Opensim must be stopped to clear script and bake caches.")
        End If

        Me.Close()

    End Sub

    Private Sub MapHelp_Click(sender As Object, e As EventArgs) Handles MapHelp.Click
        Form1.Help("Cache")
    End Sub

    Private Sub HelpToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles HelpToolStripMenuItem1.Click
        Form1.Help("Cache")
    End Sub

    Private Sub CacheTimeout_TextChanged(sender As Object, e As EventArgs)
        Dim digitsOnly As Regex = New Regex("[^\d\.]")
        CacheTimeout.Text = digitsOnly.Replace(CacheTimeout.Text, "")
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If Form1.PropOpensimIsRunning Then
            Form1.ConsoleCommand("Robust", "fcache clear")
        End If

    End Sub

    Private Sub CacheEnabledBox_CheckedChanged(sender As Object, e As EventArgs) Handles CacheEnabledBox.CheckedChanged

    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        Form1.Help("Flotsam Cache")
    End Sub

    Private Sub PictureBox1_Click_1(sender As Object, e As EventArgs) Handles PictureBox1.Click

        'Create an instance of the open file dialog box.
        Dim openFileDialog1 As FolderBrowserDialog = New FolderBrowserDialog With {
            .ShowNewFolderButton = True,
            .Description = "Pick folder for cache"
        }
        Dim UserClickedOK As DialogResult = openFileDialog1.ShowDialog

        ' Process input if the user clicked OK.
        If UserClickedOK = DialogResult.OK Then
            Dim thing = openFileDialog1.SelectedPath
            If thing.Length > 0 Then
                Form1.PropMySetting.BackupFolder = thing
                Form1.PropMySetting.SaveSettings()
                CacheFolder.Text = thing
            End If
        End If
    End Sub


End Class