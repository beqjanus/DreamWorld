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

Imports System.Threading

Public Class FormBackupCheckboxes

#Region "ScreenSize"

    Private ReadOnly Handler As New EventHandler(AddressOf Resize_page)

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

    Private Sub bck()
        Dim Foldername = "Full_backup" + "_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss", Globalization.CultureInfo.InvariantCulture)   ' Set default folder
        Dim Dest As String
        If Settings.BackupFolder = "AutoBackup" Then
            Dest = IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\AutoBackup\" & Foldername)
        Else
            Dest = IO.Path.Combine(Settings.BackupFolder, Foldername)
        End If

        If RegionCheckBox.Checked Then
            Try
                My.Computer.FileSystem.CreateDirectory(Dest)
                My.Computer.FileSystem.CreateDirectory(Dest + "\Opensim_bin_Regions")
            Catch ex As Exception
            End Try

            FileStuff.CopyFolder(IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Opensim\bin\Regions"), IO.Path.Combine(Dest, "Opensim_bin_Regions"))
            Application.DoEvents()
        End If

        If MySqlCheckBox.Checked Then
            Try
                My.Computer.FileSystem.CreateDirectory(Dest)
                My.Computer.FileSystem.CreateDirectory(IO.Path.Combine(Dest, "Mysql_Data"))
            Catch ex As Exception

                BreakPoint.Show(ex.Message)
            End Try
            FileStuff.CopyFolder(IO.Path.Combine(IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Mysql\Data\")), IO.Path.Combine(Dest, "Mysql_Data"))
            Application.DoEvents()
        End If

        If FSAssetsCheckBox.Checked Then
            Try
                My.Computer.FileSystem.CreateDirectory(Dest)
                My.Computer.FileSystem.CreateDirectory(Dest + "\FSAssets")
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
            End Try

            Dim folder As String = "./fsassets"
            If Settings.BaseDirectory = "./fsassets" Then
                folder = Settings.OpensimBinPath & "\FSAssets"
            Else
                folder = Settings.BaseDirectory
            End If
            FileStuff.CopyFolder(folder, IO.Path.Combine(Dest, "FSAssets"))
            Application.DoEvents()
        End If

        If CustomCheckBox.Checked Then
            Try
                My.Computer.FileSystem.CreateDirectory(Dest)
                My.Computer.FileSystem.CreateDirectory(Dest + "\Opensim_WifiPages-Custom")
                My.Computer.FileSystem.CreateDirectory(Dest + "\Opensim_bin_WifiPages-Custom")
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
            End Try
            FileStuff.CopyFolder(IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Opensim\WifiPages\"), IO.Path.Combine(Dest, "Opensim_WifiPages-Custom"))
            FileStuff.CopyFolder(IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Opensim\bin\WifiPages\"), IO.Path.Combine(Dest, "Opensim_bin_WifiPages-Custom"))
            Application.DoEvents()
        End If

        If SettingsBox.Checked Then
            FileStuff.CopyFile(IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Settings.ini"), IO.Path.Combine(Dest, "Settings.ini"), True)
        End If
        DialogResult = DialogResult.OK

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If Button1.Text = "Finished" Then Me.Close()

        Button1.Text = My.Resources.Busy_word
        Dim WebThread = New Thread(AddressOf bck)
        Try
            WebThread.SetApartmentState(ApartmentState.STA)
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try
        WebThread.Start()
        WebThread.Priority = ThreadPriority.Highest

        WebThread.Join()
        Button1.Text = My.Resources.Finished_with_backup_word

    End Sub

    Private Sub FormCritical_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        HelpOnce("Backup Manually")

        MySqlCheckBox.Enabled = True
        MySqlCheckBox.Checked = True

        If Settings.FsAssetsEnabled Then
            FSAssetsCheckBox.Enabled = True
            FSAssetsCheckBox.Checked = True
        Else
            FSAssetsCheckBox.Enabled = False
            FSAssetsCheckBox.Checked = False
        End If

    End Sub

    Private Sub HelpToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles HelpToolStripMenuItem1.Click

        HelpManual("Backup Manually")

    End Sub

#End Region

End Class
