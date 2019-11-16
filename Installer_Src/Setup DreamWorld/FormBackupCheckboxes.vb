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

Public Class FormBackupCheckboxes

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

#Region "Private Methods"

    Private Shared Sub CpyFile(From As String, Dest As String)

        If From.EndsWith("Opensim.ini", StringComparison.InvariantCulture) Then Return
        If From.EndsWith("OpenSim.log", StringComparison.InvariantCulture) Then Return
        If From.EndsWith("OpenSimStats.log", StringComparison.InvariantCulture) Then Return
        If From.EndsWith("PID.pid", StringComparison.InvariantCulture) Then Return
        If From.EndsWith("DataSnapshot", StringComparison.InvariantCulture) Then Return

        'Create the file stream for the source file
        Dim streamRead As New System.IO.FileStream(From, System.IO.FileMode.Open)
        'Create the file stream for the destination file
        Dim streamWrite As New System.IO.FileStream(Dest, System.IO.FileMode.Create)
        'Determine the size in bytes of the source file (-1 as our position starts at 0)
        Dim lngLen As Long = streamRead.Length - 1
        Dim byteBuffer(1048576) As Byte   'our stream buffer
        Dim intBytesRead As Integer    'number of bytes read

        While streamRead.Position < lngLen    'keep streaming until EOF
            'Read from the Source
            intBytesRead = (streamRead.Read(byteBuffer, 0, 1048576))
            'Write to the Target
            streamWrite.Write(byteBuffer, 0, intBytesRead)

            Application.DoEvents()    'do it
        End While

        'Clean up
        streamWrite.Flush()
        streamWrite.Close()
        streamRead.Close()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim Foldername = "Full_backup" + "_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss", Globalization.CultureInfo.InvariantCulture)   ' Set default folder
        Dim Dest As String
        If Form1.Settings.BackupFolder = "AutoBackup" Then
            Dest = Form1.Settings.Myfolder + "\OutworldzFiles\AutoBackup\" + Foldername
        Else
            Dest = Form1.Settings.BackupFolder + "\" + Foldername
        End If
        Try
            If RegionCheckBox.Checked Then
                My.Computer.FileSystem.CreateDirectory(Dest)
                My.Computer.FileSystem.CreateDirectory(Dest + "\Opensim_bin_Regions")
                PrintStatus("Backing up Regions Folder")
                FileStuff.CopyFolder(Form1.Settings.Myfolder + "\OutworldzFiles\Opensim\bin\Regions", Dest + "\Opensim_bin_Regions")
                Application.DoEvents()
            End If

            If MySqlCheckBox.Checked Then
                My.Computer.FileSystem.CreateDirectory(Dest)
                My.Computer.FileSystem.CreateDirectory(Dest + "\Mysql_Data")
                PrintStatus("Backing up MySql\Data Folder")
                FileStuff.CopyFolder(Form1.Settings.Myfolder + "\OutworldzFiles\Mysql\Data\", Dest + "\Mysql_Data")
                Application.DoEvents()
            End If

            If FSAssetsCheckBox.Checked Then
                My.Computer.FileSystem.CreateDirectory(Dest)
                My.Computer.FileSystem.CreateDirectory(Dest + "\FSAssets")

                Dim folder As String = "./fsassets"
                If Form1.Settings.BaseDirectory = "./fsassets" Then
                    folder = Form1.Settings.OpensimBinPath & "bin\FSAssets"
                Else
                    folder = Form1.Settings.BaseDirectory
                End If

                PrintStatus("Backing up FSAssets Folder")
                FileStuff.CopyFolder(folder, Dest + "\FSAssets")
                Application.DoEvents()
            End If

            If CustomCheckBox.Checked Then
                My.Computer.FileSystem.CreateDirectory(Dest)
                My.Computer.FileSystem.CreateDirectory(Dest + "\Opensim_WifiPages-Custom")
                My.Computer.FileSystem.CreateDirectory(Dest + "\Opensim_bin_WifiPages-Custom")
                PrintStatus("Backing up Wifi Folders")
                FileStuff.CopyFolder(Form1.Settings.Myfolder + "\OutworldzFiles\Opensim\WifiPages\", Dest + "\Opensim_WifiPages-Custom")
                FileStuff.CopyFolder(Form1.Settings.Myfolder + "\OutworldzFiles\Opensim\bin\WifiPages\", Dest + "\Opensim_bin_WifiPages-Custom")
                Application.DoEvents()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        If SettingsBox.Checked Then
            PrintStatus(My.Resources.Backing_up_Settings_word)
            FileStuff.CopyFile(Form1.Settings.Myfolder + "\OutworldzFiles\Settings.ini", Dest + "\Settings.ini", True)
        End If
        PrintStatus(My.Resources.Finished_with_backup & Dest)
        DialogResult = DialogResult.OK

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        DialogResult = DialogResult.OK
        Me.Close()

    End Sub

    Private Sub FormCritical_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Form1.HelpOnce("Backup Manually")

        TextBox1.BackColor = Me.BackColor
        ' init the scrolling text box
        TextBox1.SelectionStart = 0
        TextBox1.ScrollToCaret()
        TextBox1.SelectionStart = TextBox1.Text.Length
        TextBox1.ScrollToCaret()

        If Not Form1.CheckMysql Then
            MySqlCheckBox.Enabled = True
            MySqlCheckBox.Checked = True
        Else
            MySqlCheckBox.Enabled = False
            MySqlCheckBox.Checked = False
        End If

        If Form1.Settings.FsAssetsEnabled Then
            FSAssetsCheckBox.Enabled = True
            FSAssetsCheckBox.Checked = True
        Else
            FSAssetsCheckBox.Enabled = False
            FSAssetsCheckBox.Checked = False
        End If

    End Sub

    Private Sub HelpToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles HelpToolStripMenuItem1.Click

        Form1.Help("Backup Manually")

    End Sub

    Private Sub PrintStatus(Value As String)

        TextBox1.Text = TextBox1.Text & vbCrLf & Value
        Trim()

    End Sub

    Private Sub TextBox1_TextChanged(sender As System.Object, e As System.EventArgs)
        Dim ln As Integer = TextBox1.Text.Length
        TextBox1.SelectionStart = ln
        TextBox1.ScrollToCaret()
    End Sub

    Private Sub Trim()
        If TextBox1.Text.Length > TextBox1.MaxLength - 1000 Then
            TextBox1.Text = Mid(TextBox1.Text, 14000)
        End If
    End Sub

#End Region

End Class