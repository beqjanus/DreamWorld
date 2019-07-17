Public Class FormFsAssets

#Region "Declarations"
    Dim initted As Boolean = False
    Dim _changed As Boolean = False
#End Region

#Region "ScreenSize"
    'The following detects  the location of the form in screen coordinates
    Private _screenPosition As ScreenPos

    Private Handler As New EventHandler(AddressOf Resize_page)
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

#Region "Properties"

    Public Property ScreenPosition As ScreenPos
        Get
            Return _screenPosition
        End Get
        Set(value As ScreenPos)
            _screenPosition = value
        End Set
    End Property
    Public Property Changed As Boolean
        Get
            Return _changed
        End Get
        Set(value As Boolean)
            _changed = value
        End Set
    End Property
    Public Property Initted1 As Boolean
        Get
            Return initted
        End Get
        Set(value As Boolean)
            initted = value
        End Set
    End Property

#End Region

#Region "LoadSave"
    Private Sub Loaded(sender As Object, e As EventArgs) Handles Me.Load

        Form1.HelpOnce("FSAssets")

        EnableFsAssetsCheckbox.Checked = Form1.PropMySetting.FsAssetsEnabled
        DataFolder.Text = Form1.PropMySetting.BaseDirectory
        SpoolPath.Text = Form1.PropMySetting.SpoolDirectory
        ShowStatsCheckBox.Checked = CType(Form1.PropMySetting.ShowConsoleStats, Boolean)

        Initted1 = True

    End Sub

    Private Sub Form_exit() Handles Me.Closed

        If Changed Then
            Dim result As DialogResult = MsgBox("Form has changed! Save, or Abort?", vbOKCancel)
            If result = vbOK Then
                Form1.PropMySetting.SaveSettings()
            End If
        End If

    End Sub


#End Region

#Region "Subs"

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles EnableFsAssetsCheckbox.CheckedChanged
        If Not initted Then Return
        Form1.PropMySetting.FsAssetsEnabled = EnableFsAssetsCheckbox.Checked
        Changed = True
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Form1.Help("FSAssets")
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click

        'Create an instance of the open file dialog box.
        Dim openFileDialog1 As FolderBrowserDialog = New FolderBrowserDialog With {
            .ShowNewFolderButton = True,
            .Description = "Pick folder for fsassets (default is ./fsassets/data in Opensim/bin)"
        }
        Dim UserClickedOK As DialogResult = openFileDialog1.ShowDialog

        ' Process input if the user clicked OK.
        If UserClickedOK = DialogResult.OK Then
            Dim thing = openFileDialog1.SelectedPath
            If thing.Length > 0 Then
                Form1.PropMySetting.BaseDirectory = thing
                Form1.PropMySetting.SaveSettings()
                DataFolder.Text = thing
                Changed = True
            End If
        End If
    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click

        'Create an instance of the open file dialog box.
        Dim openFileDialog1 As FolderBrowserDialog = New FolderBrowserDialog With {
            .ShowNewFolderButton = True,
            .Description = "Pick folder for Spooling (default is ./fsassets/tmp in Opensim/bin)"
        }
        Dim UserClickedOK As DialogResult = openFileDialog1.ShowDialog

        ' Process input if the user clicked OK.
        If UserClickedOK = DialogResult.OK Then
            Dim thing = openFileDialog1.SelectedPath
            If thing.Length > 0 Then
                Form1.PropMySetting.SpoolDirectory = thing
                Form1.PropMySetting.SaveSettings()
                SpoolPath.Text = thing
                Changed = True
            End If
        End If

    End Sub

    Private Sub CheckBox1_CheckedChanged_1(sender As Object, e As EventArgs) Handles ShowStatsCheckBox.CheckedChanged

        If Not initted Then Return
        Form1.PropMySetting.ShowConsoleStats = ShowStatsCheckBox.Checked.ToString(Form1.Usa)
        Changed = True

    End Sub

    Private Sub SaveButton_Click(sender As Object, e As EventArgs) Handles SaveButton.Click

        Form1.PropMySetting.SaveSettings()
        Me.Close()
    End Sub




#End Region

End Class