#Region "Copyright AGPL3.0"

' Copyright Outworldz, LLC.
' AGPL3.0  https://opensource.org/licenses/AGPL

#End Region

Public Class FormIARSave

#Region "Private Fields"

    Private _gAvatarName As String = ""
    Private _gBackupName As String = ""
    Private _gBackupPath As String = ""
    Private _gObject As String = "/"

#End Region

#Region "Public Constructors"

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        Button1.Text = Global.Outworldz.My.Resources.Save_IAR_word
        Button2.Text = Global.Outworldz.My.Resources.Cancel_word
        GroupBox1.Text = Global.Outworldz.My.Resources.Save_Inventory_IAR_word '"Save Inventory IAR"
        HelpToolStripMenuItem.Image = Global.Outworldz.My.Resources.question_and_answer
        HelpToolStripMenuItem.Text = Global.Outworldz.My.Resources.Help_word
        Label1.Text = Global.Outworldz.My.Resources.Object_Path_and_name
        Label2.Text = Global.Outworldz.My.Resources.Backup_Name
        Label3.Text = Global.Outworldz.My.Resources.Avatar_Name_word
        PictureBox1.Image = Global.Outworldz.My.Resources.folder
        Text = Global.Outworldz.My.Resources.Save_Inventory_IAR_word
        ToolTip1.SetToolTip(AviName, Global.Outworldz.My.Resources.Avatar_First_and_Last_Name_word)
        ToolTip1.SetToolTip(ObjectNameBox, Global.Outworldz.My.Resources.Enter_Name)

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

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
    End Sub

    Private Sub HelpToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HelpToolStripMenuItem.Click
        HelpManual("SaveIar")
    End Sub

    Private Sub HelpToolStripMenuItem1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub ObjectNameBox_TextChanged_1(sender As Object, e As EventArgs) Handles ObjectNameBox.TextChanged
        GObject = ObjectNameBox.Text
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
