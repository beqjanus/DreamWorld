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

Imports System.Net
Imports System.Text.RegularExpressions

Public Class FormPublicity

#Region "GLobals"

    Private initted As Boolean = False

#End Region

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

#Region "Pubicity Checkbox"

    Private Sub GDPRCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles GDPRCheckBox.CheckedChanged

        If initted Then
            Form1.Settings.GDPR() = GDPRCheckBox.Checked
            Form1.Settings.SaveSettings()
        End If

    End Sub

#End Region

#Region "Start/Stop"

    Private Sub Publicity_Close(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Closed

        Dim category As String = ""
        For Each i In CategoryCheckbox.CheckedItems
            If i.length > 0 Then category += i & ","
        Next

        Form1.Settings.Categories = category

        Dim tmp = DescriptionBox.Text.Replace(vbCrLf, "<br>")
        Form1.Settings.Description = tmp

        Form1.UploadCategory()
        Form1.Settings.SaveSettings()

    End Sub

    Private Sub Publicity_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        SetScreen()

        GDPRCheckBox.Checked = Form1.Settings.GDPR()

        Try
            PictureBox9.Image = Bitmap.FromFile(Form1.PropMyFolder & "\OutworldzFiles\Photo.png")
#Disable Warning CA1031
        Catch ex As Exception
#Enable Warning CA1031

            PictureBox9.Image = My.Resources.ClicktoInsertPhoto
        End Try
        Dim tmp = Form1.Settings.Description
        tmp = tmp.Replace("<br>", vbCrLf)
        DescriptionBox.Text = tmp

        Dim cats = Form1.Settings.Categories.Split(",")

        For Each itemname In cats
            Dim Index = CategoryCheckbox.FindStringExact(itemname)
            If Index > -1 Then
                CategoryCheckbox.SetSelected(Index, True)
            End If
        Next

        Form1.HelpOnce("Publicity")
        initted = True

    End Sub

#End Region

#Region "Clicks"

    Private Sub CategoryCheckbox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CategoryCheckbox.SelectedIndexChanged

        Dim Index = CategoryCheckbox.SelectedIndex

        If CategoryCheckbox.GetItemChecked(Index) Then
            CategoryCheckbox.SetItemCheckState(Index, CheckState.Unchecked)
        Else
            CategoryCheckbox.SetItemCheckState(Index, CheckState.Checked)
        End If

    End Sub

    Private Sub DatabaseSetupToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DatabaseSetupToolStripMenuItem.Click
        Form1.Help("Publicity")
    End Sub

    Private Sub PictureBox9_Click(sender As Object, e As EventArgs) Handles PictureBox9.Click
        Dim ofd As New OpenFileDialog With {
            .Filter = My.Resources.picfilter,
            .FilterIndex = 1,
            .Multiselect = False
        }
        If ofd.ShowDialog = DialogResult.OK Then
            If ofd.FileName.Length > 0 Then

                Dim pattern As Regex = New Regex("PNG$|png$")
                Dim match As Match = pattern.Match(ofd.FileName)
                If Not match.Success Then
                    MsgBox(My.Resources.Must_PNG, vbInformation)
                    Return
                End If

                PictureBox9.Image = Nothing
                Try
                    PictureBox9.Image = Bitmap.FromFile(ofd.FileName)
#Disable Warning CA1031
                Catch ex As Exception
#Enable Warning CA1031

                End Try

                FileStuff.DeleteFile(Form1.PropMyFolder & "\OutworldzFiles\Photo.png")

                Using newBitmap = New Bitmap(PictureBox9.Image)
                    Try
                        newBitmap.Save(Form1.PropMyFolder & "\OutworldzFiles\Photo.png", Imaging.ImageFormat.Png)
#Disable Warning CA1031
                    Catch ex As Exception
#Enable Warning CA1031

                        MsgBox(ex.Message)
                        Return
                    End Try
                End Using

                Form1.UploadPhoto()

            End If
        End If

    End Sub

    Private Sub PublicPhoto_Click(sender As Object, e As EventArgs) Handles PublicPhoto.Click
        Form1.Help("Publicity")
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles DescriptionBox.TextChanged

        If (DescriptionBox.Text.Length > 1024) Then
            DescriptionBox.Text = Mid(DescriptionBox.Text, 1024)
        End If

    End Sub

#End Region

End Class
