Imports System.Text.RegularExpressions

Public Class FormFlotsamCache

    Private gInitted As Boolean = False

    Private Sub Form_Load() Handles Me.Load

        CacheFolder.Text = Form1.MySetting.CacheFolder
        CacheEnabledBox.Checked = Form1.MySetting.CacheEnabled
        CacheTimeout.Text = Form1.MySetting.CacheTimeout
        LogLevelBox.SelectedIndex = CType(Form1.MySetting.CacheLogLevel, Integer)

        Dim fsize As Double
        Dim folder As String
        If CacheFolder.Text = ".\assetcache" Then
            folder = Form1.MySetting.OpensimBinPath & "bin/assetcache"
        Else
            folder = CacheFolder.Text
        End If

        For Each file As String In My.Computer.FileSystem.GetFiles(folder)
            Dim finnfo As New System.IO.FileInfo(file)
            fsize += finnfo.Length
        Next
        fsize = fsize / 1024
        Text = String.Format(Form1.usa, "{0: 0} Kb", fsize)
        CacheSizeLabel.Text = Text

        gInitted = True

    End Sub
    Private Sub Form_unload() Handles Me.Closing

        Form1.MySetting.CacheLogLevel = LogLevelBox.SelectedIndex.ToString(Form1.usa)
        Form1.MySetting.CacheFolder = CacheFolder.Text
        Form1.MySetting.CacheEnabled = CacheEnabledBox.Checked
        Form1.MySetting.CacheTimeout = CacheTimeout.Text

        Form1.MySetting.SaveSettings()

    End Sub

    Private Sub CacheTimeout_TextChanged(sender As Object, e As EventArgs) Handles CacheTimeout.TextChanged
        Dim digitsOnly As Regex = New Regex("[^\d\.]")
        CacheTimeout.Text = digitsOnly.Replace(CacheTimeout.Text, "")
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
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
                Form1.MySetting.BackupFolder = thing
                Form1.MySetting.SaveSettings()
                CacheFolder.Text = thing
            End If
        End If
    End Sub


End Class