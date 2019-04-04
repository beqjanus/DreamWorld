Public Class FormIARSave

    Public gObject As String = "/"
    Public gAvatarName As String = ""
    Public gBackupPath As String = ""
    Public gBackupName As String = ""
    Public gPassword As String = ""


    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        BackupNameTextBox.Text = "Backup_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".iar"
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If Not gBackupName.ToLower.EndsWith(".iar") Then
            MsgBox("Add 'filename.iar' to the path to save to.")
            Return
        End If
        If gAvatarName.Length = 0 Or Not gAvatarName.Contains(" ") Then
            MsgBox("Must have an avatar's 'First Last' name.")
            Return
        End If
        If gPassword.Length = 0 Then
            MsgBox("Must have a password for this avatar.")
            Return
        End If

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
    End Sub

    Private Sub ObjectNameBox_TextChanged(sender As Object, e As EventArgs) Handles ObjectNameBox.TextChanged
        gObject = ObjectNameBox.Text
    End Sub

    Private Sub BackupNameTextBox_TextChanged(sender As Object, e As EventArgs) Handles BackupNameTextBox.TextChanged
        gBackupName = BackupNameTextBox.Text
    End Sub

    Private Sub Password_TextChanged(sender As Object, e As EventArgs) Handles Password.TextChanged
        gPassword = Password.Text
    End Sub

    Private Sub name_TextChanged(sender As Object, e As EventArgs) Handles AviName.TextChanged
        gAvatarName = AviName.Text
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Dim ofd As New FolderBrowserDialog

        If ofd.ShowDialog = DialogResult.OK Then
            If ofd.SelectedPath <> String.Empty Then
                gBackupPath = ofd.SelectedPath
            End If
        End If

    End Sub
End Class