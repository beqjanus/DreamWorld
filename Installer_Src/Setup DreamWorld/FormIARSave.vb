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

        BackupNameTextBox.Text = "Backup_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss", Form1.Usa) + ".iar"
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)

        If Not GBackupName.ToLower(Form1.Usa).EndsWith(".iar") Then
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

    Private Sub ObjectNameBox_TextChanged(sender As Object, e As EventArgs)
        GObject = ObjectNameBox.Text
    End Sub

    Private Sub BackupNameTextBox_TextChanged(sender As Object, e As EventArgs)
        GBackupName = BackupNameTextBox.Text
    End Sub

    Private Sub Password_TextChanged(sender As Object, e As EventArgs)
        GPassword = Password.Text
    End Sub

    Private Sub Name_TextChanged(sender As Object, e As EventArgs)
        GAvatarName = AviName.Text
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs)
        Dim ofd As New FolderBrowserDialog

        If ofd.ShowDialog = DialogResult.OK Then
            If ofd.SelectedPath.Length > 0 Then
                GBackupPath = ofd.SelectedPath
            End If
        End If

    End Sub

    Private Sub HelpToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles HelpToolStripMenuItem1.Click

        Form1.Help("SaveIar")

    End Sub
End Class