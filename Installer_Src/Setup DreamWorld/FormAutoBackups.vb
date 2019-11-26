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

Public Class FormAutoBackups

#Region "FormPos"

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

    Private Sub Loaded(sender As Object, e As EventArgs) Handles Me.Load

        AutoBackupKeepFilesForDays.Text = CStr(Form1.Settings.KeepForDays)

        '0 = Hourly
        '1 = 12 Hour
        '2 = Daily
        '3 = 2 days
        '4 = 3 days
        '5 = 4 days
        '6 = 6 days
        '7 = 7 days
        '8 = Weekly
        ' default= 1

        If CType(Form1.Settings.AutobackupInterval, Double) = 60 Then
            AutoBackupInterval.SelectedIndex = 0
        ElseIf CType(Form1.Settings.AutobackupInterval, Double) = 12 * 60 Then
            AutoBackupInterval.SelectedIndex = 1
        ElseIf CType(Form1.Settings.AutobackupInterval, Double) = 24 * 60 Then
            AutoBackupInterval.SelectedIndex = 2
        ElseIf CType(Form1.Settings.AutobackupInterval, Double) = 2 * 24 * 60 Then
            AutoBackupInterval.SelectedIndex = 3
        ElseIf CType(Form1.Settings.AutobackupInterval, Double) = 3 * 24 * 60 Then
            AutoBackupInterval.SelectedIndex = 4
        ElseIf CType(Form1.Settings.AutobackupInterval, Double) = 4 * 24 * 60 Then
            AutoBackupInterval.SelectedIndex = 5
        ElseIf CType(Form1.Settings.AutobackupInterval, Double) = 5 * 24 * 60 Then
            AutoBackupInterval.SelectedIndex = 6
        ElseIf CType(Form1.Settings.AutobackupInterval, Double) = 6 * 24 * 60 Then
            AutoBackupInterval.SelectedIndex = 7
        ElseIf CType(Form1.Settings.AutobackupInterval, Double) = 7 * 60 * 24 Then
            AutoBackupInterval.SelectedIndex = 8
        Else
            AutoBackupInterval.SelectedIndex = 1
        End If

        BaseFolder.Text = Form1.Settings.BackupFolder
        AutoBackup.Checked = Form1.Settings.AutoBackup
        Form1.HelpOnce("Backup")
        SetScreen()

    End Sub

#End Region

#Region "Changes"

    Private Sub ABEnabled_CheckedChanged(sender As Object, e As EventArgs) Handles AutoBackup.CheckedChanged

        Form1.PropViewedSettings = True
        Form1.Settings.AutoBackup = AutoBackup.Checked
        Form1.Settings.SaveSettings()

    End Sub

    Private Sub AutoBackupInterval_SelectedIndexChanged(sender As Object, e As EventArgs) Handles AutoBackupInterval.SelectedIndexChanged
        Dim text = AutoBackupInterval.SelectedItem.ToString()
        '0 = Hourly
        '1 = 12 Hour
        '2 = Daily
        '3 = 2 days
        '4 = 3 days
        '5 = 4 days
        '6 = 6 days
        '7 = 7 days
        '8 = Weekly
        ' default= 1

        Dim Interval As Integer = 60 * 12
        If text = "Hourly" Then
            Interval = 60
        ElseIf text = "12 Hour" Then
            Interval = 60 * 12
        ElseIf text = "Daily" Then
            Interval = 60 * 24
        ElseIf text = "2 days" Then
            Interval = 2 * 60 * 24
        ElseIf text = "3 days" Then
            Interval = 3 * 60 * 24
        ElseIf text = "4 days" Then
            Interval = 4 * 60 * 24
        ElseIf text = "5 days" Then
            Interval = 5 * 60 * 24
        ElseIf text = "6 days" Then
            Interval = 6 * 60 * 24
        ElseIf text = "Weekly" Then
            Interval = 7 * 60 * 24
        End If

        Form1.Settings.AutobackupInterval = CStr(Interval)
        Form1.PropViewedSettings = True
        Form1.Settings.SaveSettings()
    End Sub

    Private Sub AutoBackupKeepFilesForDays_TextChanged(sender As Object, e As EventArgs) Handles AutoBackupKeepFilesForDays.TextChanged

        Dim digitsOnly As Regex = New Regex("[^\d]")
        AutoBackupKeepFilesForDays.Text = digitsOnly.Replace(AutoBackupKeepFilesForDays.Text, "")

        Try
            If CInt(AutoBackupKeepFilesForDays.Text) > 0 Then
                Form1.Settings.KeepForDays = CInt(AutoBackupKeepFilesForDays.Text)
                Form1.Settings.SaveSettings()
            End If
        Catch ex As Exception
            MsgBox(My.Resources.Must_be_Days, vbInformation)
            Form1.Settings.KeepForDays = 30
            Form1.Settings.SaveSettings()
        End Try
        Form1.PropViewedSettings = True
    End Sub

    Private Sub BackupFolder_clicked(sender As Object, e As EventArgs) Handles BaseFolder.Click

        Backup()

    End Sub

#End Region

#Region "Help"

    Private Sub AutoBackupHelp_Click(sender As Object, e As EventArgs) Handles AutoBackupHelp.Click

        Form1.Help("Backup")

    End Sub

    Private Sub Backup()

        'Create an instance of the open file dialog box.
        Dim openFileDialog1 As FolderBrowserDialog = New FolderBrowserDialog With {
            .ShowNewFolderButton = True,
            .Description = "Pick folder for backups"
        }
        Dim UserClickedOK As DialogResult = openFileDialog1.ShowDialog
        openFileDialog1.Dispose()

        ' Process input if the user clicked OK.
        If UserClickedOK = DialogResult.OK Then
            Dim thing = openFileDialog1.SelectedPath
            If thing.Length > 0 Then
                Form1.Settings.BackupFolder = thing
                Form1.Settings.SaveSettings()
                BaseFolder.Text = thing
            End If
        End If

    End Sub

    Private Sub DataOnlyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DataOnlyToolStripMenuItem.Click

        Form1.BackupDB()

    End Sub

    Private Sub FullSQLBackupToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FullSQLBackupToolStripMenuItem.Click

        Dim CriticalForm = New FormBackupCheckboxes
        CriticalForm.Activate()
        CriticalForm.Visible = True

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click

        Backup()

    End Sub

    Private Sub ServerTypeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ServerTypeToolStripMenuItem.Click
        Form1.Help("Backup")
    End Sub

#End Region

End Class
