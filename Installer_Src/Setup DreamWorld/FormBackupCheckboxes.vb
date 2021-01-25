#Region "Copyright"

' Copyright 2014 Fred Beckhusen for outworldz.com https://opensource.org/licenses/AGPL

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

Public Class FormBackupCheckboxes

    Private initted As Boolean

#Region "ScreenSize"

    Private ReadOnly Handler As New EventHandler(AddressOf Resize_page)

    'The following detects  the location of the form in screen coordinates
    Private _screenPosition As ScreenPos

    Public Property ScreenPosition As ScreenPos
        Get
            Return _screenPosition
        End Get
        Set(value As ScreenPos)
            _screenPosition = value
        End Set
    End Property

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

    Private Sub BackupSQlCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles BackupSQlCheckBox.CheckedChanged

        If Not initted Then Return
        Settings.BackupSQL = BackupSQlCheckBox.Checked
        Settings.SaveSettings()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Button1.Text = My.Resources.Running_word
        Dim b As New Backups()
        b.RunAllBackups(True)
        Application.DoEvents()
        Threading.Thread.Sleep(2000)
        Me.Close()

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles BackupOarsCheckBox.CheckedChanged

        If Not initted Then Return
        Settings.BackupOARs = BackupOarsCheckBox.Checked
        Settings.SaveSettings()

    End Sub

    Private Sub CustomCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles CustomCheckBox.CheckedChanged

        If Not initted Then Return
        Settings.BackupWifi = CustomCheckBox.Checked
        Settings.SaveSettings()

    End Sub

    Private Sub FSAssetsCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles FSAssetsCheckBox.CheckedChanged

        If Not initted Then Return
        Settings.BackupFSAssets = FSAssetsCheckBox.Checked
        Settings.SaveSettings()

    End Sub

    Private Sub HelpToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles HelpToolStripMenuItem1.Click

        HelpManual("Backup Manually")

    End Sub

    Private Sub Loaded(sender As Object, e As EventArgs) Handles Me.Load

        Button1.Text = Global.Outworldz.My.Resources.Backup_word
        CustomCheckBox.Text = Global.Outworldz.My.Resources.Backup_Custom
        FSAssetsCheckBox.Text = Global.Outworldz.My.Resources.Backup_FSAssets
        GroupBox1.Text = Global.Outworldz.My.Resources.Backup_word
        HelpToolStripMenuItem.Image = Global.Outworldz.My.Resources.question_and_answer
        HelpToolStripMenuItem.Text = Global.Outworldz.My.Resources.Help_word
        HelpToolStripMenuItem1.Image = Global.Outworldz.My.Resources.about
        HelpToolStripMenuItem1.Text = Global.Outworldz.My.Resources.Help_word
        MySqlCheckBox.Text = Global.Outworldz.My.Resources.Backup_Mysql
        RegionCheckBox.Text = Global.Outworldz.My.Resources.Backup_Region
        Text = Global.Outworldz.My.Resources.System_Backup_word

        HelpOnce("Backup Manually")

        If Settings.FsAssetsEnabled Then
            FSAssetsCheckBox.Enabled = True
            FSAssetsCheckBox.Checked = True
        Else
            FSAssetsCheckBox.Enabled = False
            FSAssetsCheckBox.Checked = False
            Settings.BackupFSAssets = False
        End If

        RegionCheckBox.Checked = Settings.BackupRegion
        MySqlCheckBox.Checked = Settings.BackupMysql
        FSAssetsCheckBox.Checked = Settings.BackupFSAssets
        CustomCheckBox.Checked = Settings.BackupWifi
        BackupOarsCheckBox.Checked = Settings.BackupOARs

        BackupSQlCheckBox.Checked = Settings.BackupSQL

        initted = True

    End Sub

    Private Sub MySqlCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles MySqlCheckBox.CheckedChanged

        If Not initted Then Return
        Settings.BackupMysql = MySqlCheckBox.Checked
        Settings.SaveSettings()

    End Sub

    Private Sub RegionCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles RegionCheckBox.CheckedChanged

        If Not initted Then Return
        Settings.BackupRegion = RegionCheckBox.Checked
        Settings.SaveSettings()

    End Sub

#End Region

End Class
