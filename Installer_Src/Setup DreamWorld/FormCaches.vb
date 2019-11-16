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

Imports System.Text.RegularExpressions

Public Class FormCaches

#Region "Private Fields"

    Private gInitted As Boolean = False

#End Region

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

#Region "Private Methods"

    Private Sub B_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If CheckBox1.Checked Then
            ClrCache.WipeScripts()
        End If

        If CheckBox2.Checked Then
            ClrCache.WipeBakes()
        End If

        If CheckBox3.Checked Then
            ClrCache.WipeAssets()
        End If

        If CheckBox4.Checked Then
            ClrCache.WipeImage()
        End If
        If CheckBox5.Checked Then
            ClrCache.WipeMesh()
        End If

        If Not Form1.PropOpensimIsRunning() Then
            Form1.Print(My.Resources.All_Caches_Cleared_word)
        Else
            Form1.Print(My.Resources.Cache_Most_Cleared)
        End If

        Me.Close()

    End Sub

    Private Sub CacheEnabledBox_CheckedChanged(sender As Object, e As EventArgs) Handles CacheEnabledBox.CheckedChanged
        If Not gInitted Then Return
        Form1.PropViewedSettings = True
    End Sub

    Private Sub CacheTimeout_TextChanged(sender As Object, e As EventArgs)
        If Not gInitted Then Return
        Dim digitsOnly As Regex = New Regex("[^\d\.]")
        CacheTimeout.Text = digitsOnly.Replace(CacheTimeout.Text, "")
        Form1.PropViewedSettings = True
    End Sub

    Private Sub Form_unload() Handles Me.Closing

        Form1.Settings.CacheLogLevel = LogLevelBox.SelectedIndex.ToString(Globalization.CultureInfo.InvariantCulture)
        Form1.Settings.CacheFolder = CacheFolder.Text
        Form1.Settings.CacheEnabled = CacheEnabledBox.Checked
        Form1.Settings.CacheTimeout = CacheTimeout.Text
        Form1.Settings.SupportViewerObjectsCache = ViewerCacheCheckbox.Checked

        Form1.PropViewedSettings = True
        Form1.Settings.SaveSettings()

    End Sub

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

        CacheFolder.Text = Form1.Settings.CacheFolder
        CacheEnabledBox.Checked = Form1.Settings.CacheEnabled
        CacheTimeout.Text = Form1.Settings.CacheTimeout
        LogLevelBox.SelectedIndex = CType(Form1.Settings.CacheLogLevel, Integer)

        Dim folder As String
        If CacheFolder.Text = ".\assetcache" Then
            folder = Form1.Settings.OpensimBinPath & "bin/assetcache"
        Else
            folder = CacheFolder.Text
        End If

        ViewerCacheCheckbox.Checked = Form1.Settings.SupportViewerObjectsCache

        gInitted = True

        Form1.HelpOnce("Cache")
    End Sub

    Private Sub HelpToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles HelpToolStripMenuItem1.Click
        Form1.Help("Cache")
    End Sub

    Private Sub MapHelp_Click(sender As Object, e As EventArgs) Handles MapHelp.Click
        Form1.Help("Cache")
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
                Form1.Settings.BackupFolder = thing
                Form1.Settings.SaveSettings()
                CacheFolder.Text = thing
                Form1.PropViewedSettings = True
            End If
        End If
        openFileDialog1.Dispose()

    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        Form1.Help("Flotsam Cache")
    End Sub

    Private Sub ViewerCacheCheckbox_CheckedChanged(sender As Object, e As EventArgs) Handles ViewerCacheCheckbox.CheckedChanged

        ' Support viewers object cache, default true Users may need to reduce viewer bandwidth if
        ' some prims Or terrain parts fail to rez. Change to false if you need to use old viewers
        ' that do not support this feature
        Form1.Settings.SupportViewerObjectsCache = ViewerCacheCheckbox.Checked

    End Sub

#End Region

End Class