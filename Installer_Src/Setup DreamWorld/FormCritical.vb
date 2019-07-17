Public Class FormCritical


#Region "ScreenSize"
    'The following detects  the location of the form in screen coordinates
    Private _screenPosition As ScreenPos

    Public Property ScreenPosition As ScreenPos
        Get
            Return _screenPosition
        End Get
        Set(value As ScreenPos)
            _screenPosition = value
        End Set
    End Property
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
    Private Sub FormCritical_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim Foldername = "Full_backup" + "_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss", Form1.Usa)   ' Set default folder
        Dim Dest As String
        If Form1.PropMySetting.BackupFolder = "AutoBackup" Then
            Dest = Form1.PropMyFolder + "\OutworldzFiles\AutoBackup\" + Foldername
        Else
            Dest = Form1.PropMySetting.BackupFolder + "\" + Foldername
        End If
        Try


            If RegionCheckBox.Checked Then
                My.Computer.FileSystem.CreateDirectory(Dest)
                My.Computer.FileSystem.CreateDirectory(Dest + "\Opensim_bin_Regions")
                Form1.Print("Backing up Regions Folder")
                My.Computer.FileSystem.CopyDirectory(Form1.PropMyFolder + "\OutworldzFiles\Opensim\bin\Regions", Dest + "\Opensim_bin_Regions")
            End If
            If MySqlCheckBox.Checked Then
                My.Computer.FileSystem.CreateDirectory(Dest)
                My.Computer.FileSystem.CreateDirectory(Dest + "\Mysql_Data")
                Form1.Print("Backing up MySql\Data Folder")
                My.Computer.FileSystem.CopyDirectory(Form1.PropMyFolder + "\OutworldzFiles\Mysql\Data\", Dest + "\Mysql_Data")

            End If
            If FSAssetsCheckBox.Checked Then
                My.Computer.FileSystem.CreateDirectory(Dest)
                My.Computer.FileSystem.CreateDirectory(Dest + "\FSassets")
                My.Computer.FileSystem.CopyDirectory(Form1.PropMyFolder + "\OutworldzFiles\Opensim\bin\fsassets\", Dest + "\Mysql_Data")
            End If
            If CustomCheckBox.Checked Then
                My.Computer.FileSystem.CreateDirectory(Dest)
                My.Computer.FileSystem.CreateDirectory(Dest + "\Opensim_WifiPages-Custom")
                My.Computer.FileSystem.CreateDirectory(Dest + "\Opensim_bin_WifiPages-Custom")
                Form1.Print("Backing up Wifi Folders")
                My.Computer.FileSystem.CopyDirectory(Form1.PropMyFolder + "\OutworldzFiles\Opensim\WifiPages\", Dest + "\Opensim_WifiPages-Custom")
                My.Computer.FileSystem.CopyDirectory(Form1.PropMyFolder + "\OutworldzFiles\Opensim\bin\WifiPages\", Dest + "\Opensim_bin_WifiPages-Custom")

            End If

            If SettingsBox.Checked Then
                Form1.Print("Backing up Settings")
                My.Computer.FileSystem.CopyFile(Form1.PropMyFolder + "\OutworldzFiles\Settings.ini", Dest + "\Settings.ini")
            End If
            Form1.Print("Finished with backup at " + Dest)
        Catch ex As Exception
            MsgBox("Something went wrong: " + ex.Message)
        End Try
        DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        DialogResult = DialogResult.OK
        Me.Close()

    End Sub
End Class