Public Class FormDatabase

    Dim initted As Boolean = False
    Dim DNSNamebackup As String = ""
    Dim changed As Boolean = False
    Dim ServerType As String = ""
    Dim DNSName As String = ""

#Region "Properties"

    Public Property Initted1 As Boolean
        Get
            Return initted
        End Get
        Set(value As Boolean)
            initted = value
        End Set
    End Property

    Public Property DNSNamebackup1 As String
        Get
            Return DNSNamebackup
        End Get
        Set(value As String)
            DNSNamebackup = value
        End Set
    End Property

    Public Property Changed1 As Boolean
        Get
            Return changed
        End Get
        Set(value As Boolean)
            changed = value
        End Set
    End Property

    Public Property ServerType1 As String
        Get
            Return ServerType
        End Get
        Set(value As String)
            ServerType = value
        End Set
    End Property

    Public Property DNSName1 As String
        Get
            Return DNSName
        End Get
        Set(value As String)
            DNSName = value
        End Set
    End Property

#End Region

#Region "ScreenSize"
    'The following detects  the location of the form in screen coordinates
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

#Region "Load/Exit"

    Private Sub Loaded(sender As Object, e As EventArgs) Handles Me.Load

        'Database
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''
        RegionDbName.Text = Form1.PropMySetting.RegionDBName
        RegionDBUsername.Text = Form1.PropMySetting.RegionDBUsername
        RegionMySqlPassword.Text = Form1.PropMySetting.RegionDbPassword
        RegionServer.Text = Form1.PropMySetting.RegionServer
        MysqlRegionPort.Text = Form1.PropMySetting.RegionPort

        ' Robust DB
        RobustServer.Text = Form1.PropMySetting.RobustServer
        RobustDbName.Text = Form1.PropMySetting.RobustDataBaseName
        RobustDBPassword.Text = Form1.PropMySetting.RobustPassword
        RobustDBUsername.Text = Form1.PropMySetting.RobustUsername
        RobustDbPort.Text = Form1.PropMySetting.MySqlPort.ToString(Form1.Usa)
        RobustDBPassword.UseSystemPasswordChar = True

        SetScreen()

        Select Case Form1.PropMySetting.ServerType
            Case "Robust"
                GridServerButton.Checked = True
            Case "Region"
                GridRegionButton.Checked = True
            Case "OsGrid"
                osGridRadioButton1.Checked = True
            Case "Metro"
                MetroRadioButton2.Checked = True
            Case Else
                GridServerButton.Checked = True
        End Select

        Initted1 = True
        Form1.HelpOnce("Database")
        MsgBox("Changes to this area may require special changes to MySQL. If you change these, you will probably break things. Please read the Help section bvefore making changes!", vbInformation)

    End Sub

    Private Sub Form_exit() Handles Me.Closed
        If Changed1 Then
            SaveAll()
        End If
    End Sub

    Private Sub SaveAll()

        Form1.PropMySetting.ServerType = ServerType1

        If DNSName1.Length > 0 Then
            Form1.PropMySetting.GridServerName = DNSName1
        End If

        Form1.PropMySetting.SaveSettings()
        Changed1 = False ' do not trigger the save a second time

    End Sub

#End Region

#Region "Database"

    Private Sub RobustServer_TextChanged(sender As Object, e As EventArgs) Handles RobustServer.TextChanged

        If Not Initted1 Then Return
        Form1.PropMySetting.RobustServer = RobustServer.Text
        Form1.PropMySetting.SaveSettings()

    End Sub

    Private Sub DatabaseNameUser_TextChanged(sender As Object, e As EventArgs) Handles RegionDbName.TextChanged

        If Not Initted1 Then Return
        Form1.PropMySetting.RegionDBName = RegionDbName.Text
        Form1.PropMySetting.SaveSettings()

    End Sub

    Private Sub DbUsername_TextChanged(sender As Object, e As EventArgs) Handles RegionDBUsername.TextChanged
        If Not Initted1 Then Return
        Form1.PropMySetting.RegionDBUsername = RegionDBUsername.Text
        Form1.PropMySetting.SaveSettings()

    End Sub

    Private Sub DbPassword_Clicked(sender As Object, e As EventArgs) Handles RegionMySqlPassword.Click

        RegionMySqlPassword.UseSystemPasswordChar = False

    End Sub

    Private Sub DbPassword_TextChanged(sender As Object, e As EventArgs) Handles RegionMySqlPassword.TextChanged

        If Not Initted1 Then Return
        Form1.PropMySetting.RegionDbPassword = RegionMySqlPassword.Text
        Form1.PropMySetting.SaveSettings()

    End Sub

    Private Sub TextBox1_TextChanged_1(sender As Object, e As EventArgs) Handles RobustDbName.TextChanged
        If Not Initted1 Then Return
        Form1.PropMySetting.RobustDataBaseName = RobustDbName.Text
        Form1.PropMySetting.SaveSettings()

    End Sub

    Private Sub RobustUsernameTextBox_TextChanged(sender As Object, e As EventArgs) Handles RobustDBUsername.TextChanged

        If Not Initted1 Then Return
        Form1.PropMySetting.RobustUsername = RobustDBUsername.Text
        Form1.PropMySetting.SaveSettings()

    End Sub

    Private Sub RobustPasswordTextBox_TextChanged(sender As Object, e As EventArgs) Handles RobustDBPassword.TextChanged

        If Not Initted1 Then Return
        Form1.PropMySetting.RobustPassword = RobustDBPassword.Text
        Form1.PropMySetting.SaveSettings()

    End Sub

    Private Sub RobustDbPortTextbox_click(sender As Object, e As EventArgs) Handles RobustDBPassword.Click

        RobustDBPassword.UseSystemPasswordChar = False

    End Sub

    Private Sub Database_Click(sender As Object, e As EventArgs) Handles DBHelp.Click

        Form1.Help("Database")

    End Sub

    Private Sub RobustDbPort_TextChanged(sender As Object, e As EventArgs) Handles RobustDbPort.TextChanged

        If Not Initted1 Then Return
        Form1.PropMySetting.MySqlPort = RobustDbPort.Text
        Form1.PropMySetting.SaveSettings()

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles RegionServer.TextChanged

        If Not Initted1 Then Return
        Form1.PropMySetting.RegionServer = RegionServer.Text
        Form1.PropMySetting.SaveSettings()

    End Sub

    Private Sub TextBox1_TextChanged_2(sender As Object, e As EventArgs) Handles MysqlRegionPort.TextChanged

        If Not Initted1 Then Return
        Form1.PropMySetting.RegionPort = MysqlRegionPort.Text
        Form1.PropMySetting.SaveSettings()

    End Sub

#End Region

#Region "Grid type"

    Private Sub SaveButton_Click(sender As Object, e As EventArgs) Handles SaveButton.Click

        SaveAll()
        Me.Close()

    End Sub

    Private Sub ToolStripLabel1_Click(sender As Object, e As EventArgs) Handles ToolStripLabel1.Click

        Form1.Help("Database")

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click

        Form1.Help("GridType")

    End Sub

    Private Sub GridServerButton_CheckedChanged_1(sender As Object, e As EventArgs) Handles GridServerButton.CheckedChanged

        If Not Initted1 Then Return
        If Not GridServerButton.Checked Then Return

        RobustServer.Enabled = True
        RobustDbName.Enabled = True
        RobustDBPassword.Enabled = True
        RobustDbPort.Enabled = True
        RobustDBUsername.Enabled = True
        RobustDBPassword.Enabled = True

        Form1.PropMySetting.DNSName = DNSNamebackup1
        Changed1 = True
        ServerType1 = "Robust"

    End Sub

    Private Sub GridRegionButton_CheckedChanged_1(sender As Object, e As EventArgs) Handles GridRegionButton.CheckedChanged

        If Not Initted1 Then Return
        If Not GridRegionButton.Checked Then Return

        RobustServer.Enabled = True
        RobustDbName.Enabled = True
        RobustDBPassword.Enabled = True
        RobustDbPort.Enabled = True
        RobustDBUsername.Enabled = True
        RobustDBPassword.Enabled = True

        ServerType1 = "Region"
        DNSNamebackup1 = Form1.PropMySetting.DNSName

    End Sub

    Private Sub OsGridRadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles osGridRadioButton1.CheckedChanged

        If Not Initted1 Then Return
        If Not osGridRadioButton1.Checked Then Return

        RobustServer.Enabled = False
        RobustDbName.Enabled = False
        RobustDBPassword.Enabled = False
        RobustDbPort.Enabled = False
        RobustDBUsername.Enabled = False
        RobustDBPassword.Enabled = False

        DNSNamebackup1 = Form1.PropMySetting.DNSName
        ServerType1 = "OsGrid"
        DNSName1 = "hg.osgrid.org"
        Changed1 = True

    End Sub

    Private Sub MetroRadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles MetroRadioButton2.CheckedChanged

        If Not Initted1 Then Return
        If Not MetroRadioButton2.Checked Then Return

        RobustServer.Enabled = False
        RobustDbName.Enabled = False
        RobustDBPassword.Enabled = False
        RobustDbPort.Enabled = False
        RobustDBUsername.Enabled = False
        RobustDBPassword.Enabled = False

        DNSNamebackup1 = Form1.PropMySetting.DNSName
        ServerType1 = "Metro"
        DNSName1 = "http://hg.metro.land"

        Changed1 = True

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim FsAssets As New FormFsAssets
        FsAssets.Show()

    End Sub


#End Region

End Class