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

Public Class FormFlotsamCache

    Private gInitted As Boolean = False

    Private Sub Form_Load() Handles Me.Load

        Form1.HelpOnce("Flotsam Cache")

        CacheFolder.Text = Form1.Settings.CacheFolder
        CacheEnabledBox.Checked = Form1.Settings.CacheEnabled
        CacheTimeout.Text = Form1.Settings.CacheTimeout
        LogLevelBox.SelectedIndex = CType(Form1.Settings.CacheLogLevel, Integer)

        Dim fsize As Double
        Dim folder As String
        If CacheFolder.Text = ".\assetcache" Then
            folder = Form1.Settings.OpensimBinPath & "bin/assetcache"
        Else
            folder = CacheFolder.Text
        End If

        For Each file As String In My.Computer.FileSystem.GetFiles(folder)
            Dim finnfo As New System.IO.FileInfo(file)
            fsize += finnfo.Length
        Next
        fsize /= 1024
        Text = String.Format(Form1.InVarient, "{0: 0} Kb", fsize)
        CacheSizeLabel.Text = Text

        gInitted = True

    End Sub

    Private Sub Form_unload() Handles Me.Closing

        Form1.Settings.CacheLogLevel = LogLevelBox.SelectedIndex.ToString(Form1.InVarient)
        Form1.Settings.CacheFolder = CacheFolder.Text
        Form1.Settings.CacheEnabled = CacheEnabledBox.Checked
        Form1.Settings.CacheTimeout = CacheTimeout.Text
        Form1.PropViewedSettings = True
        Form1.Settings.SaveSettings()

    End Sub

    Private Sub CacheEnabledBox_CheckedChanged(sender As Object, e As EventArgs) Handles CacheEnabledBox.CheckedChanged

    End Sub

End Class