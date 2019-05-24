Public Class FormIARSave

    Private _gObject As String = "/"
    Private _gAvatarName As String = ""
    Private _gBackupPath As String = ""
    Private _gBackupName As String = ""
    Private _gPassword As String = ""

    Public Property GObject As String
        Get
            Return _gObject
        End Get
        Set(value As String)
            _gObject = value
        End Set
    End Property

    Public Property GAvatarName As String
        Get
            Return _gAvatarName
        End Get
        Set(value As String)
            _gAvatarName = value
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

    Public Property GBackupName As String
        Get
            Return _gBackupName
        End Get
        Set(value As String)
            _gBackupName = value
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

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        BackupNameTextBox.Text = "Backup_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".iar"
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If Not GBackupName.ToLower.EndsWith(".iar") Then
            MsgBox("Add 'filename.iar' to the path to save to.")
            Return
        End If
        If GAvatarName.Length = 0 Or Not GAvatarName.Contains(" ") Then
            MsgBox("Must have an avatar's 'First Last' name.")
            Return
        End If
        If GPassword.Length = 0 Then
            MsgBox("Must have a password for this avatar.")
            Return
        End If

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
    End Sub

    Private Sub ObjectNameBox_TextChanged(sender As Object, e As EventArgs) Handles ObjectNameBox.TextChanged
        GObject = ObjectNameBox.Text
    End Sub

    Private Sub BackupNameTextBox_TextChanged(sender As Object, e As EventArgs) Handles BackupNameTextBox.TextChanged
        GBackupName = BackupNameTextBox.Text
    End Sub

    Private Sub Password_TextChanged(sender As Object, e As EventArgs) Handles Password.TextChanged
        GPassword = Password.Text
    End Sub

    Private Sub name_TextChanged(sender As Object, e As EventArgs) Handles AviName.TextChanged
        GAvatarName = AviName.Text
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Dim ofd As New FolderBrowserDialog

        If ofd.ShowDialog = DialogResult.OK Then
            If ofd.SelectedPath.Length > 0 Then
                GBackupPath = ofd.SelectedPath
            End If
        End If

    End Sub
End Class