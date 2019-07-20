Imports System.Text.RegularExpressions

Public Class FormFlotsamCache

    Private gInitted As Boolean = False

    Private Sub Form_Load() Handles Me.Load

        Form1.HelpOnce("Flotsam Cache")

        CacheFolder.Text = Form1.PropMySetting.CacheFolder
        CacheEnabledBox.Checked = Form1.PropMySetting.CacheEnabled
        CacheTimeout.Text = Form1.PropMySetting.CacheTimeout
        LogLevelBox.SelectedIndex = CType(Form1.PropMySetting.CacheLogLevel, Integer)

        Dim fsize As Double
        Dim folder As String
        If CacheFolder.Text = ".\assetcache" Then
            folder = Form1.PropMySetting.OpensimBinPath & "bin/assetcache"
        Else
            folder = CacheFolder.Text
        End If

        For Each file As String In My.Computer.FileSystem.GetFiles(folder)
            Dim finnfo As New System.IO.FileInfo(file)
            fsize += finnfo.Length
        Next
        fsize /= 1024
        Text = String.Format(Form1.Usa, "{0: 0} Kb", fsize)
        CacheSizeLabel.Text = Text

        gInitted = True

    End Sub
    Private Sub Form_unload() Handles Me.Closing

        Form1.PropMySetting.CacheLogLevel = LogLevelBox.SelectedIndex.ToString(Form1.Usa)
        Form1.PropMySetting.CacheFolder = CacheFolder.Text
        Form1.PropMySetting.CacheEnabled = CacheEnabledBox.Checked
        Form1.PropMySetting.CacheTimeout = CacheTimeout.Text

        Form1.PropMySetting.SaveSettings()

    End Sub

    Private Sub CacheTimeout_TextChanged(sender As Object, e As EventArgs)
        Dim digitsOnly As Regex = New Regex("[^\d\.]")
        CacheTimeout.Text = digitsOnly.Replace(CacheTimeout.Text, "")
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs)
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

    Private Sub DatabaseSetupToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DatabaseSetupToolStripMenuItem.Click

        Form1.Help("Flotsam Cache")

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If Form1.PropOpensimIsRunning Then
            Form1.ConsoleCommand("Robust", "fcache clear")
        End If

    End Sub


End Class