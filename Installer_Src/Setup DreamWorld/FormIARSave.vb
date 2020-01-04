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

Public Class FormIARSave

#Region "Private Fields"

    Private _gAvatarName As String = ""
    Private _gBackupName As String = ""
    Private _gBackupPath As String = ""
    Private _gObject As String = "/"
    Private _gPassword As String = ""

#End Region

#Region "Public Constructors"

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        BackupNameTextBox.Text = "Backup_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss", Globalization.CultureInfo.InvariantCulture) + ".iar"

    End Sub

#End Region

#Region "Public Properties"

    Public Property GAvatarName As String
        Get
            Return _gAvatarName
        End Get
        Set(value As String)
            _gAvatarName = value
        End Set
    End Property

    Public Property GBackupName As String
        Get
            Return _gBackupName
        End Get
        Set(value As String)
            _gBackupName = value
        End Set
    End Property

    Public Property GBackupPath As String
        Get
            Return _gBackupPath
        End Get
        Set(value As String)
            _gBackupPath = value
        End Set
    End Property

    Public Property GObject As String
        Get
            Return _gObject
        End Get
        Set(value As String)
            _gObject = value
        End Set
    End Property

    Public Property GPassword As String
        Get
            Return _gPassword
        End Get
        Set(value As String)
            _gPassword = value
        End Set
    End Property

#End Region

#Region "Private Methods"

    Private Sub AviName_TextChanged(sender As Object, e As EventArgs) Handles AviName.TextChanged
        GAvatarName = AviName.Text
    End Sub

    Private Sub BackupNameTextBox_TextChanged_1(sender As Object, e As EventArgs) Handles BackupNameTextBox.TextChanged
        GBackupName = BackupNameTextBox.Text
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click

        If GAvatarName.Length = 0 Or Not GAvatarName.Contains(" ") Then
            MsgBox(My.Resources.Enter_1_2)
            Return
        End If
        If GPassword.Length = 0 Then
            MsgBox(My.Resources.Password_word)
            Return
        End If

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
    End Sub

    Private Sub HelpToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles HelpToolStripMenuItem1.Click

        Form1.Help("SaveIar")

    End Sub

    Private Sub ObjectNameBox_TextChanged_1(sender As Object, e As EventArgs) Handles ObjectNameBox.TextChanged
        GObject = ObjectNameBox.Text
    End Sub

    Private Sub Password_TextChanged_1(sender As Object, e As EventArgs) Handles Password.TextChanged
        GPassword = Password.Text
    End Sub

    Private Sub PictureBox1_Click_1(sender As Object, e As EventArgs) Handles PictureBox1.Click

        Dim ofd As New FolderBrowserDialog

        If ofd.ShowDialog = DialogResult.OK Then
            If ofd.SelectedPath.Length > 0 Then
                GBackupPath = ofd.SelectedPath
                BackupNameTextBox.Text = ofd.SelectedPath
            End If
        End If
        ofd.Dispose()

    End Sub

#End Region

End Class
