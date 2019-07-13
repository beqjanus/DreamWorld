Public Class FormDatabase

    Dim initted As Boolean = False
    Dim DNSNamebackup As String = ""
    Dim changed As Boolean = False
    Dim ServerType As String = ""
    Dim DNSName As String = ""

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

#Region "Load/Exit"

    Private Sub Loaded(sender As Object, e As EventArgs) Handles Me.Load

        'Database
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''
        RegionDbName.Text = Form1.MySetting.RegionDBName
        RegionDBUsername.Text = Form1.MySetting.RegionDBUsername
        RegionMySqlPassword.Text = Form1.MySetting.RegionDbPassword
        RegionServer.Text = Form1.MySetting.RegionServer
        MysqlRegionPort.Text = Form1.MySetting.RegionPort

        ' Robust DB
        RobustServer.Text = Form1.MySetting.RobustServer
        RobustDbName.Text = Form1.MySetting.RobustDataBaseName
        RobustDBPassword.Text = Form1.MySetting.RobustPassword
        RobustDBUsername.Text = Form1.MySetting.RobustUsername
        RobustDbPort.Text = Form1.MySetting.MySqlPort.ToString(Form1.usa)
        RobustDBPassword.UseSystemPasswordChar = True

        SetScreen()

        Select Case Form1.MySetting.ServerType
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

        initted = True
        Form1.HelpOnce("Database")
        MsgBox("Changes to this area may require special changes to MySQL. If you change these, you will probably break things. Please read the Help section bvefore making changes!", vbInformation)

    End Sub

    Private Sub Form_exit() Handles Me.Closed
        If changed Then
            SaveAll()
        End If
    End Sub

    Private Sub SaveAll()

        Form1.MySetting.ServerType = ServerType

        If DNSName.Length > 0 Then
            Form1.MySetting.GridServerName = DNSName
        End If

        Form1.MySetting.SaveSettings()
        changed = False ' do not trigger the save a second time

    End Sub

#End Region

#Region "Database"

    Private Sub RobustServer_TextChanged(sender As Object, e As EventArgs) Handles RobustServer.TextChanged

        If Not initted Then Return
        Form1.MySetting.RobustServer = RobustServer.Text
        Form1.MySetting.SaveSettings()

    End Sub

    Private Sub DatabaseNameUser_TextChanged(sender As Object, e As EventArgs) Handles RegionDbName.TextChanged

        If Not initted Then Return
        Form1.MySetting.RegionDBName = RegionDbName.Text
        Form1.MySetting.SaveSettings()

    End Sub

    Private Sub DbUsername_TextChanged(sender As Object, e As EventArgs) Handles RegionDBUsername.TextChanged
        If Not initted Then Return
        Form1.MySetting.RegionDBUsername = RegionDBUsername.Text
        Form1.MySetting.SaveSettings()

    End Sub

    Private Sub DbPassword_Clicked(sender As Object, e As EventArgs) Handles RegionMySqlPassword.Click

        RegionMySqlPassword.UseSystemPasswordChar = False

    End Sub

    Private Sub DbPassword_TextChanged(sender As Object, e As EventArgs) Handles RegionMySqlPassword.TextChanged

        If Not initted Then Return
        Form1.MySetting.RegionDbPassword = RegionMySqlPassword.Text
        Form1.MySetting.SaveSettings()

    End Sub

    Private Sub TextBox1_TextChanged_1(sender As Object, e As EventArgs) Handles RobustDbName.TextChanged
        If Not initted Then Return
        Form1.MySetting.RobustDataBaseName = RobustDbName.Text
        Form1.MySetting.SaveSettings()

    End Sub

    Private Sub RobustUsernameTextBox_TextChanged(sender As Object, e As EventArgs) Handles RobustDBUsername.TextChanged

        If Not initted Then Return
        Form1.MySetting.RobustUsername = RobustDBUsername.Text
        Form1.MySetting.SaveSettings()

    End Sub

    Private Sub RobustPasswordTextBox_TextChanged(sender As Object, e As EventArgs) Handles RobustDBPassword.TextChanged

        If Not initted Then Return
        Form1.MySetting.RobustPassword = RobustDBPassword.Text
        Form1.MySetting.SaveSettings()

    End Sub

    Private Sub RobustDbPortTextbox_click(sender As Object, e As EventArgs) Handles RobustDBPassword.Click

        RobustDBPassword.UseSystemPasswordChar = False

    End Sub

    Private Sub Database_Click(sender As Object, e As EventArgs) Handles DBHelp.Click

        Form1.Help("Database")

    End Sub

    Private Sub RobustDbPort_TextChanged(sender As Object, e As EventArgs) Handles RobustDbPort.TextChanged

        If Not initted Then Return
        Form1.MySetting.MySqlPort = RobustDbPort.Text
        Form1.MySetting.SaveSettings()

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles RegionServer.TextChanged

        If Not initted Then Return
        Form1.MySetting.RegionServer = RegionServer.Text
        Form1.MySetting.SaveSettings()

    End Sub

    Private Sub TextBox1_TextChanged_2(sender As Object, e As EventArgs) Handles MysqlRegionPort.TextChanged

        If Not initted Then Return
        Form1.MySetting.RegionPort = MysqlRegionPort.Text
        Form1.MySetting.SaveSettings()

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

        If Not initted Then Return
        If Not GridServerButton.Checked Then Return

        RobustServer.Enabled = True
        RobustDbName.Enabled = True
        RobustDBPassword.Enabled = True
        RobustDbPort.Enabled = True
        RobustDBUsername.Enabled = True
        RobustDBPassword.Enabled = True
        Form1.MySetting.DNSName = DNSNamebackup
        changed = True
        ServerType = "Robust"

    End Sub

    Private Sub GridRegionButton_CheckedChanged_1(sender As Object, e As EventArgs) Handles GridRegionButton.CheckedChanged

        If GridRegionButton.Checked Then
            RobustServer.Enabled = False
            RobustDBPassword.Enabled = False
            RobustDbPort.Enabled = False
            RobustDBUsername.Enabled = False
            RobustDbName.Enabled = False
            ServerType = "Region"
            DNSNamebackup = Form1.MySetting.DNSName
        Else
            RobustServer.Enabled = True
            RobustDBPassword.Enabled = True
            RobustDbPort.Enabled = True
            RobustDBUsername.Enabled = True
            RobustDbName.Enabled = True
        End If

    End Sub

    Private Sub OsGridRadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles osGridRadioButton1.CheckedChanged

        If Not initted Then Return

        RobustServer.Enabled = False
        RobustDbName.Enabled = False
        RobustDBPassword.Enabled = False
        RobustDbPort.Enabled = False
        RobustDBUsername.Enabled = False
        RobustDBPassword.Enabled = False
        DNSNamebackup = Form1.MySetting.DNSName
        ServerType = "OsGrid"
        DNSName = "hg.osgrid.org"
        changed = True

    End Sub

    Private Sub MetroRadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles MetroRadioButton2.CheckedChanged

        If Not initted Then Return
        If Not MetroRadioButton2.Checked Then Return

        RobustServer.Enabled = False
        RobustDbName.Enabled = False
        RobustDBPassword.Enabled = False
        RobustDbPort.Enabled = False
        RobustDBUsername.Enabled = False
        RobustDBPassword.Enabled = False
        DNSNamebackup = Form1.MySetting.DNSName
        ServerType = "Metro"
        DNSName = "http://hg.metro.land"

        changed = True

    End Sub

#End Region

End Class