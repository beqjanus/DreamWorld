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

Public Class FormPublicity

#Region "GLobals"

    Private initted As Boolean

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

#Region "Publicity Checkbox"

    Private Sub GDPRCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles GDPRCheckBox.CheckedChanged

        If initted Then
            Settings.GDPR() = GDPRCheckBox.Checked
            Settings.SaveSettings()
        End If

    End Sub

#End Region

#Region "Start/Stop"

    Private Sub Publicity_Close(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Closed

        Dim category As String = ""
        For Each i In CategoryCheckbox.CheckedItems
            If i.length > 0 Then category += CStr(i) & ","
        Next

        Settings.Categories = category

        Dim tmp = DescriptionBox.Text.Replace(vbCrLf, "<br>")
        Settings.Description = tmp

        Form1.UploadCategory()
        Settings.SaveSettings()

    End Sub

    Private Sub Publicity_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        SetScreen()

        GDPRCheckBox.Checked = Settings.GDPR()

        Try
            PictureBox9.Image = Bitmap.FromFile(Settings.CurrentDirectory & "\OutworldzFiles\Photo.png")
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
            PictureBox9.Image = Global.Outworldz.My.Resources.ClicktoInsertPhoto
        End Try
        Dim tmp = Settings.Description
        tmp = tmp.Replace("<br>", vbCrLf)
        DescriptionBox.Text = tmp

#Disable Warning BC42016 ' Implicit conversion
        Dim cats = Settings.Categories.Split(",")
#Enable Warning BC42016 ' Implicit conversion

        For Each itemname In cats
            Dim Index = CategoryCheckbox.FindStringExact(itemname)
            If Index > -1 Then
                CategoryCheckbox.SetSelected(Index, True)
            End If
        Next

        HelpOnce("Publicity")
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
        HelpManual("Publicity")
    End Sub

    <CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId:="Outworldz.Form1.ErrorLog(System.String)")>
    Private Sub PictureBox9_Click(sender As Object, e As EventArgs) Handles PictureBox9.Click
        Dim ofd As New OpenFileDialog With {
            .Filter = Global.Outworldz.My.Resources.picfilter,
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

                    FileStuff.DeleteFile(Settings.CurrentDirectory & "\OutworldzFiles\Photo.png")
                    Dim newBitmap = New Bitmap(PictureBox9.Image)
                    newBitmap.Save(Settings.CurrentDirectory & "\OutworldzFiles\Photo.png", Imaging.ImageFormat.Png)
                    newBitmap.Dispose()
                Catch ex As Exception

                    BreakPoint.Show(ex.Message)
                    Form1.ErrorLog("Save Photo " & ex.Message)
                    Return
                End Try

                Form1.UploadPhoto()

            End If
        End If

    End Sub

    Private Sub PublicPhoto_Click(sender As Object, e As EventArgs) Handles PublicPhoto.Click
        HelpManual("Publicity")
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles DescriptionBox.TextChanged

        If (DescriptionBox.Text.Length > 1024) Then
            DescriptionBox.Text = Mid(DescriptionBox.Text, 1024)
        End If

    End Sub

#End Region

End Class
