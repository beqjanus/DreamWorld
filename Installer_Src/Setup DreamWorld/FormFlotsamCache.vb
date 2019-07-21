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

    Private Sub CacheEnabledBox_CheckedChanged(sender As Object, e As EventArgs) Handles CacheEnabledBox.CheckedChanged

    End Sub
End Class