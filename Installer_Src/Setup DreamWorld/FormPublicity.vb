Imports System.Text.RegularExpressions

Public Class FormPublicity

    Dim initted As Boolean = False

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

    Private Sub Publicity_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        SetScreen()

        GDPRCheckBox.Checked = Form1.PropMySetting.GDPR()

        Try
            PictureBox9.Image = Bitmap.FromFile(Form1.PropMyFolder & "\OutworldzFiles\Photo.png")
        Catch ex As Exception
            PictureBox9.Image = My.Resources.ClicktoInsertPhoto
        End Try

        Form1.HelpOnce("Publicity")
        initted = True

    End Sub

    Private Sub GDPRCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles GDPRCheckBox.CheckedChanged

        If initted Then
            Form1.PropMySetting.GDPR() = GDPRCheckBox.Checked
            Form1.PropMySetting.SaveSettings()
        End If

    End Sub

    Private Sub PictureBox9_Click(sender As Object, e As EventArgs) Handles PictureBox9.Click
        Dim ofd As New OpenFileDialog With {
            .Filter = "PNG Files (*.PNG,*.png)|*.png;|All Files (*.*)|*.*",
            .FilterIndex = 1,
            .Multiselect = False
        }
        If ofd.ShowDialog = DialogResult.OK Then
            If ofd.FileName.Length > 0 Then

                Dim pattern As Regex = New Regex("PNG$|png$")
                Dim match As Match = pattern.Match(ofd.FileName)
                If Not match.Success Then
                    MsgBox("Must be a PNG file", vbInformation)
                    Return
                End If

                PictureBox9.Image = Nothing
                PictureBox9.Image = Bitmap.FromFile(ofd.FileName)
                Try
                    My.Computer.FileSystem.DeleteFile(Form1.PropMyFolder & "\OutworldzFiles\Photo.png")
                Catch ex As ArgumentException
                    MsgBox("Warn: ex.Message")
                    Return
                Catch ex As IO.PathTooLongException
                    MsgBox("Warn: ex.Message")
                    Return
                Catch ex As NotSupportedException
                    MsgBox("Warn: ex.Message")
                    Return
                Catch ex As Io.IOException
                    MsgBox("Warn: ex.Message")
                    Return
                Catch ex As Security.SecurityException
                    MsgBox("Warn: ex.Message")
                    Return
                Catch ex As UnauthorizedAccessException
                    MsgBox("Warn: ex.Message")
                    Return
                End Try

                Try
                    PictureBox9.Image.Save(Form1.PropMyFolder & "\OutworldzFiles\Photo.png", Imaging.ImageFormat.Png)
                Catch ex As ArgumentNullException
                    MsgBox("Warn: ex.Message")
                    Return
                Catch ex As Runtime.InteropServices.ExternalException
                    MsgBox("Warn: ex.Message")
                    Return
                End Try

                Dim Myupload As New UploadImage
                Myupload.PostContentUploadFile()

            End If
        End If

    End Sub

    Private Sub PublicPhoto_Click(sender As Object, e As EventArgs) Handles PublicPhoto.Click
        Form1.Help("Publicity")
    End Sub

    Private Sub DatabaseSetupToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DatabaseSetupToolStripMenuItem.Click
        Form1.Help("Publicity")
    End Sub
End Class