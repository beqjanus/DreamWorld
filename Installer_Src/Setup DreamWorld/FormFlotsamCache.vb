#Region "Copyright AGPL3.0"

' Copyright Outworldz, LLC.
' AGPL3.0  https://opensource.org/licenses/AGPL

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

#Region "Private Fields"

    Private gInitted As Boolean

#End Region

#Region "Private Methods"

    Private Sub CacheEnabledBox_CheckedChanged(sender As Object, e As EventArgs) Handles CacheEnabledBox.CheckedChanged

    End Sub

    Private Sub Form_Load() Handles Me.Load

        Button1.Text = Global.Outworldz.My.Resources.Clear_Cache_word
        DatabaseSetupToolStripMenuItem.Image = Global.Outworldz.My.Resources.about
        DatabaseSetupToolStripMenuItem.Text = Global.Outworldz.My.Resources.Help_word
        GroupBox1.Text = Global.Outworldz.My.Resources.Asset_Cache_word
        Label1.Text = Global.Outworldz.My.Resources.Cache_Directory_word
        Label2.Text = Global.Outworldz.My.Resources.Log_Level
        Label4.Text = Global.Outworldz.My.Resources.Cache_Enabled_word
        Label5.Text = Global.Outworldz.My.Resources.Timeout_in_hours_word
        Label6.Text = Global.Outworldz.My.Resources.Current_Size '"Current Size on Disk"
        MenuStrip2.Text = Global.Outworldz.My.Resources._0
        PictureBox1.BackgroundImage = Global.Outworldz.My.Resources.folder
        Text = Global.Outworldz.My.Resources.Asset_Cache_word
        ToolStripMenuItem30.Image = Global.Outworldz.My.Resources.question_and_answer
        ToolStripMenuItem30.Text = Global.Outworldz.My.Resources.Help_word
        ToolTip1.SetToolTip(PictureBox1, Global.Outworldz.My.Resources.Click_to_change_the_folder)

        HelpOnce("Flotsam Cache")

        CacheFolder.Text = Settings.CacheFolder
        CacheEnabledBox.Checked = Settings.CacheEnabled
        CacheTimeout.Text = Settings.CacheTimeout
        LogLevelBox.SelectedIndex = CType(Settings.CacheLogLevel, Integer)

        Dim fsize As Double
        Dim folder As String
        If CacheFolder.Text = ".\assetcache" Then
            folder = Settings.OpensimBinPath & "/assetcache"
        Else
            folder = CacheFolder.Text
        End If

        For Each file As String In My.Computer.FileSystem.GetFiles(folder)
            Dim finnfo As New System.IO.FileInfo(file)
            fsize += finnfo.Length
        Next
        fsize /= 1024
        Text = String.Format(Globalization.CultureInfo.InvariantCulture, "{0: 0} Kb", fsize)
        CacheSizeLabel.Text = Text

        gInitted = True

    End Sub

    Private Sub Form_unload() Handles Me.Closing

        Settings.CacheLogLevel = LogLevelBox.SelectedIndex.ToString(Globalization.CultureInfo.InvariantCulture)
        Settings.CacheFolder = CacheFolder.Text
        Settings.CacheEnabled = CacheEnabledBox.Checked
        Settings.CacheTimeout = CacheTimeout.Text
        FormSetup.PropViewedSettings = True
        Settings.SaveSettings()

    End Sub

#End Region

End Class
