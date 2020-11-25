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

Imports System.Text.RegularExpressions

Public Class FormDatabase

#Region "Private Fields"

    Dim changed As Boolean
    Dim DNSName As String = ""
    Dim initted As Boolean

#End Region

#Region "Properties"

    Public Property Changed1 As Boolean
        Get
            Return changed
        End Get
        Set(value As Boolean)
            changed = value
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

    Public Property Initted1 As Boolean
        Get
            Return initted
        End Get
        Set(value As Boolean)
            initted = value
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

    Private Sub Form_exit() Handles Me.Closed
        If Changed1 Then
            FormSetup.PropViewedSettings = True
            SaveAll()
        End If
    End Sub

    Private Sub Loaded(sender As Object, e As EventArgs) Handles Me.Load

        Translate.Run(Name)
        'Database

        RegionDbName.Text = Settings.RegionDBName
        RegionDBUsername.Text = Settings.RegionDBUsername
        RegionMySqlPassword.Text = Settings.RegionDbPassword
        RegionServer.Text = Settings.RegionServer
        MysqlRegionPort.Text = CStr(Settings.MySqlRegionDBPort)

        ' Robust DB
        RobustServer.Text = Settings.RobustServer
        RobustDbName.Text = Settings.RobustDataBaseName
        RobustDBPassword.Text = Settings.RobustPassword
        RobustDBUsername.Text = Settings.RobustUsername
        RobustDbPort.Text = Settings.MySqlRobustDBPort.ToString(Globalization.CultureInfo.InvariantCulture)
        RobustDBPassword.UseSystemPasswordChar = True

        SetScreen()

        Initted1 = True
        HelpOnce("Database")
        HelpOnce("ServerType")

    End Sub

    Private Sub SaveAll()

        FormSetup.PropViewedSettings = True
        Settings.SaveSettings()
        Changed1 = False ' do not trigger the save a second time

    End Sub

#End Region

#Region "Database"

    Private Sub Database_Click(sender As Object, e As EventArgs) Handles DBHelp.Click

        HelpManual("Database")

    End Sub

    Private Sub DatabaseNameUser_TextChanged(sender As Object, e As EventArgs) Handles RegionDbName.TextChanged

        If Not Initted1 Then Return
        Settings.RegionDBName = RegionDbName.Text
        Settings.SaveSettings()

    End Sub

    Private Sub DbPassword_Clicked(sender As Object, e As EventArgs) Handles RegionMySqlPassword.Click

        RegionMySqlPassword.UseSystemPasswordChar = False

    End Sub

    Private Sub DbPassword_TextChanged(sender As Object, e As EventArgs) Handles RegionMySqlPassword.TextChanged

        If Not Initted1 Then Return
        Settings.RegionDbPassword = RegionMySqlPassword.Text
        Settings.SaveSettings()

    End Sub

    Private Sub DbUsername_TextChanged(sender As Object, e As EventArgs) Handles RegionDBUsername.TextChanged
        If Not Initted1 Then Return
        Settings.RegionDBUsername = RegionDBUsername.Text
        Settings.SaveSettings()

    End Sub

    Private Sub RobustDbPort_TextChanged(sender As Object, e As EventArgs) Handles RobustDbPort.TextChanged

        If Not Initted1 Then Return
        Dim digitsOnly As Regex = New Regex("[^\d]")
        RobustDbPort.Text = digitsOnly.Replace(RobustDbPort.Text, "")
        Settings.MySqlRobustDBPort = CInt("0" & RobustDbPort.Text)
        Settings.SaveSettings()

    End Sub

    Private Sub RobustDbPortTextbox_click(sender As Object, e As EventArgs) Handles RobustDBPassword.Click

        RobustDBPassword.UseSystemPasswordChar = False

    End Sub

    Private Sub RobustPasswordTextBox_TextChanged(sender As Object, e As EventArgs) Handles RobustDBPassword.TextChanged

        If Not Initted1 Then Return
        Settings.RobustPassword = RobustDBPassword.Text
        Settings.SaveSettings()

    End Sub

    Private Sub RobustServer_TextChanged(sender As Object, e As EventArgs) Handles RobustServer.TextChanged

        If Not Initted1 Then Return
        Settings.RobustServer = RobustServer.Text
        Settings.SaveSettings()

    End Sub

    Private Sub RobustUsernameTextBox_TextChanged(sender As Object, e As EventArgs) Handles RobustDBUsername.TextChanged

        If Not Initted1 Then Return
        Settings.RobustUsername = RobustDBUsername.Text
        Settings.SaveSettings()

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles RegionServer.TextChanged

        If Not Initted1 Then Return
        Settings.RegionServer = RegionServer.Text
        Settings.SaveSettings()

    End Sub

    Private Sub TextBox1_TextChanged_1(sender As Object, e As EventArgs) Handles RobustDbName.TextChanged
        If Not Initted1 Then Return
        Settings.RobustDataBaseName = RobustDbName.Text
        Settings.SaveSettings()

    End Sub

    Private Sub TextBox1_TextChanged_2(sender As Object, e As EventArgs) Handles MysqlRegionPort.TextChanged

        If Not Initted1 Then Return
        Dim digitsOnly As Regex = New Regex("[^\d]")
        MysqlRegionPort.Text = digitsOnly.Replace(MysqlRegionPort.Text, "")

        Settings.MySqlRegionDBPort = CInt("0" & MysqlRegionPort.Text)
        Settings.SaveSettings()

    End Sub

#End Region

#Region "Grid type"

    Private Shared Sub Button2_Click(sender As Object, e As EventArgs) Handles ClearRegionTable.Click

        MysqlInterface.DeregisterRegions()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

#Disable Warning CA2000 ' Dispose objects before losing scope
        Dim FsAssets As New FormFsAssets
#Enable Warning CA2000 ' Dispose objects before losing scope
        FsAssets.Show()
        FsAssets.Select()

    End Sub

    Private Sub DatabaseSetupToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DatabaseSetupToolStripMenuItem.Click
        HelpManual("Database")
    End Sub

    Private Sub DataOnlyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DataOnlyToolStripMenuItem.Click

        FormSetup.BackupDB()

    End Sub

    Private Sub Dbnameindex_Click(sender As Object, e As EventArgs) Handles Dbnameindex.Click

    End Sub

    Private Sub FullSQLBackupToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FullSQLBackupToolStripMenuItem.Click

#Disable Warning CA2000 ' Dispose objects before losing scope
        Dim CriticalForm = New FormBackupCheckboxes
#Enable Warning CA2000 ' Dispose objects before losing scope
        CriticalForm.Activate()
        CriticalForm.Visible = True
        CriticalForm.Select()
        CriticalForm.BringToFront()

    End Sub

    Private Sub Label15_Click(sender As Object, e As EventArgs) Handles Label15.Click

    End Sub

    Private Sub Label16_Click(sender As Object, e As EventArgs) Handles Label16.Click

    End Sub

    Private Sub Label8_Click(sender As Object, e As EventArgs) Handles Label8.Click

    End Sub

    Private Sub Label9_Click(sender As Object, e As EventArgs) Handles Label9.Click

    End Sub

    Private Sub ToolStripLabel1_Click(sender As Object, e As EventArgs)

        HelpManual("Database")

    End Sub

    Private Sub StartToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StartToolStripMenuItem.Click

        FormSetup.StartMySQL()

    End Sub

    Private Sub StopToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StopToolStripMenuItem.Click

        FormSetup.StopMysql()

    End Sub

    Private Sub ConsoleToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ConsoleToolStripMenuItem.Click

        Dim p As Process = New Process()
        Dim pi As ProcessStartInfo = New ProcessStartInfo With {
            .Arguments = " -u root ",
            .FileName = """" & IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\mysql\bin\mysql.exe") & """",
            .UseShellExecute = True, ' so we can redirect streams and minimize
            .WindowStyle = ProcessWindowStyle.Normal
        }
        p.StartInfo = pi
        Try
            p.Start()
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try

    End Sub

#End Region

End Class
