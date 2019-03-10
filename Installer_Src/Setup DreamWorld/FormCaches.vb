Imports System.IO

Public Class FormCaches


#Region "ScreenSize"
    Public ScreenPosition As ScreenPos
    Private Handler As New EventHandler(AddressOf Resize_page)

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

    Private Sub Cache_load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        SetScreen()

    End Sub


    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Form1.OpensimIsRunning() Then
            CheckBox1.Enabled = True
            CheckBox2.Enabled = True

            CheckBox1.Checked = True
            CheckBox2.Checked = True
            CheckBox3.Checked = True
            CheckBox4.Checked = True
            CheckBox5.Checked = True
        Else
            CheckBox1.Enabled = False
            CheckBox2.Enabled = False
            CheckBox1.Checked = False
            CheckBox2.Checked = False
            CheckBox3.Checked = True
            CheckBox4.Checked = True
            CheckBox5.Checked = True
        End If
        Form1.HelpOnce("Cache")
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If CheckBox1.Checked Then

            If Not Form1.OpensimIsRunning() Then
                Dim fCount As Integer = Directory.GetFiles(Form1.gPath & "bin\ScriptEngines\", "*", SearchOption.AllDirectories).Length
                Dim folders() = Directory.GetFiles(Form1.gPath & "bin\ScriptEngines\", "*", SearchOption.AllDirectories)
                Form1.Print("Clearing Script cache. This may take a long time!")
                Dim ctr As Integer = 0
                For Each script As String In folders
                    Dim ext = Path.GetExtension(script)
                    If ext.ToLower <> ".state" And ext.ToLower <> ".keep" Then
                        Try
                            My.Computer.FileSystem.DeleteFile(script)
                        Catch
                        End Try

                        ctr = ctr + 1
                        Form1.PrintFast("Deleting " & ctr.ToString + " of " + fCount.ToString & " scripts")
                        Application.DoEvents()
                    End If
                Next

            End If
            If CheckBox2.Checked Then
                Form1.Print("Clearing bake cache")
                Try
                    My.Computer.FileSystem.DeleteDirectory(Form1.gPath & "bin\bakes\", FileIO.DeleteDirectoryOption.DeleteAllContents)
                Catch
                End Try
            End If

        End If

        If CheckBox3.Checked Then
            Form1.Print("Clearing Asset cache. This may take a long time!")
            Dim fCount As Integer = Directory.GetFiles(Form1.gPath & "bin\Assetcache\", "*", SearchOption.AllDirectories).Length

            Try
                Dim folders() = Directory.GetDirectories(Form1.gPath & "bin\Assetcache\", "*", SearchOption.AllDirectories)
                Dim ctr As Integer = 0
                For Each folder As String In folders
                    My.Computer.FileSystem.DeleteDirectory(folder, FileIO.DeleteDirectoryOption.DeleteAllContents)
                    ctr = ctr + 1
                    Form1.PrintFast("Deleteing " & ctr.ToString + " of " + fCount.ToString & " assets")
                    Application.DoEvents()
                Next
            Catch
            End Try
        End If

        If CheckBox4.Checked Then

            Try
                Form1.Print("Clearing Image cache.")
                Dim fCount As Integer = Directory.GetFiles(Form1.gPath & "bin\j2kDecodeCache\", "*", SearchOption.AllDirectories).Length

                Dim folders() = IO.Directory.GetDirectories(Form1.gPath & "bin\j2kDecodeCache\")
                Dim ctr = 0
                For Each folder As String In folders
                    My.Computer.FileSystem.DeleteDirectory(folder, FileIO.DeleteDirectoryOption.DeleteAllContents)
                    ctr = ctr + 1
                    Form1.PrintFast("Deleteing " & ctr.ToString + " of " + fCount.ToString & " images")
                    Application.DoEvents()
                Next

            Catch
            End Try
        End If
        If CheckBox5.Checked Then
            Try
                Form1.Print("Clearing Mesh cache")
                Dim fCount As Integer = Directory.GetFiles(Form1.gPath & "bin\MeshCache\", "*", SearchOption.AllDirectories).Length

                Dim folders() = Directory.GetFiles(Form1.gPath & "bin\MeshCache\", "*", SearchOption.AllDirectories)
                Dim ctr As Integer = 0
                For Each folder As String In folders
                    My.Computer.FileSystem.DeleteDirectory(Form1.gPath & "bin\MeshCache\", FileIO.DeleteDirectoryOption.DeleteAllContents)
                    ctr = ctr + 1
                    Form1.PrintFast(ctr.ToString + " of " + fCount.ToString)
                    Application.DoEvents()
                Next
            Catch
            End Try
        End If


        If Not Form1.OpensimIsRunning() Then
            Form1.Print("All Server Caches cleared")
        Else
            Form1.Print("All Server Caches except Scripts and Avatar bakes were cleared. Opensim must be stopped to clear script and bake caches.")
        End If

        Me.Close()


    End Sub

    Private Sub MapHelp_Click(sender As Object, e As EventArgs) Handles MapHelp.Click
        Form1.Help("Cache")
    End Sub
End Class