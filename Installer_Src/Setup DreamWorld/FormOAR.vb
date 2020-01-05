Imports Newtonsoft.Json
Imports System.Net
Imports System.IO

Public Class FormOAR

#Region "Private Fields"

    Private _type As String = Nothing
    Private initSize = 275
    Private k As Integer = 50

#End Region

#Region "ScreenSize"

    Private _screenPosition As ScreenPos
    Private Handler As New EventHandler(AddressOf Resize_page)
    Private json() As JSONresult

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
        ScreenPosition.SaveHW(Me.Height, Me.Width)
        Redraw()

    End Sub

    Private Sub SetScreen()
        Me.Show()
        ScreenPosition = New ScreenPos(Me.Name)
        AddHandler ResizeEnd, Handler
        Dim xy As List(Of Integer) = ScreenPosition.GetXY()
        Me.Left = xy.Item(0)
        Me.Top = xy.Item(1)
        Dim hw As List(Of Integer) = ScreenPosition.GetHW()

        If hw.Item(0) = 0 Then
            Me.Height = initSize * 4
        Else
            Me.Height = hw.Item(0)
        End If
        If hw.Item(1) = 0 Then
            Me.Width = initSize
        Else
            Me.Width = hw.Item(1)
        End If

    End Sub

#End Region

#Region "Start/Stop"

    Public Sub Init(type As String)
        _type = type

        Dim result As String = Nothing
        Using client As New WebClient ' download client for web pages
            Try
                Dim str = Form1.SecureDomain() & "/outworldz_installer/JSON/" & _type & ".json?r=1" & Form1.GetPostData()
                result = client.DownloadString(str)
            Catch ex As ArgumentNullException
                Form1.ErrorLog(My.Resources.Wrong & " " & ex.Message)
            Catch ex As WebException
                Form1.ErrorLog(My.Resources.Wrong & " " & ex.Message)
            Catch ex As NotSupportedException
                Form1.ErrorLog(My.Resources.Wrong & " " & ex.Message)
            End Try
        End Using

        json = JsonConvert.DeserializeObject(Of JSONresult())(result)

        Dim gdTextColumn As New DataGridViewTextBoxColumn

        DataGridView.DefaultCellStyle.WrapMode = DataGridViewTriState.True

        Dim gdImageColumn As New DataGridViewImageColumn
        DataGridView.Columns.Add(gdImageColumn)

        DataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.Fill
        DataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing

        'add 10 px p[adding to bottom
        DataGridView.RowTemplate.DefaultCellStyle.Padding = New Padding(0, 0, 0, 0)

        DataGridView.RowHeadersVisible = False
        DataGridView.Width = initSize
        DataGridView.ColumnHeadersHeight = initSize
        DataGridView.ShowCellToolTips = True
        DataGridView.AllowUserToAddRows = False
        SetScreen()
        Redraw()

    End Sub

    Private Sub Form_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        SetScreen()
        Form1.HelpOnce("Load OAR-IAR")

    End Sub

#End Region

#Region "Private"

    Private Sub DataGridView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView.CellContentClick

        Dim File = json(e.RowIndex).name
        File = Form1.PropDomain() & "/Outworldz_Installer/" & _type & "/" & File 'make a real URL
        If File.EndsWith(".oar") Or File.EndsWith(".gz") Or File.EndsWith(".tgz") Then
            Form1.LoadOARContent(File)
        ElseIf File.EndsWith(".iar") Then
            Form1.LoadIARContent(File)
        End If

    End Sub

    Private Function GetImageFromURL(ByVal url As Uri) As Image

        Dim retVal As Image = Nothing

        Try
            Dim req As System.Net.WebRequest = System.Net.WebRequest.Create(url)
            Using request As System.Net.WebResponse = req.GetResponse
                Using stream As System.IO.Stream = request.GetResponseStream
                    retVal = New Bitmap(System.Drawing.Image.FromStream(stream))
                End Using
            End Using
            req = Nothing
        Catch ex As Exception
            Form1.Log("Warn", ex.Message)
            retVal = My.Resources.NoImage
        End Try

        Return retVal

    End Function

    Private Function GetTextFromURL(ByVal url As Uri) As String

        Dim retVal As String = Nothing

        Try
            Using client As WebClient = New WebClient()
                Return client.DownloadString(url)
            End Using
        Catch ex As Exception
            Form1.Log("Warn", ex.Message)
        End Try
        Return ""

    End Function

    Private Sub Redraw()

        DataGridView.Height = Me.Height - 100
        DataGridView.RowTemplate.Height = Me.Width - k
        DataGridView.RowTemplate.MinimumHeight = Me.Width - k

        DataGridView.Width = Me.Width - 50
        DataGridView.Columns(0).Width = Me.Width - k
        DataGridView.ColumnHeadersHeight = Me.Width - k

        Dim ctr = 0

        Try

            DataGridView.Rows.Clear()
            For Each item In json
                Debug.Print("Item:" & item.name)
                'If item.name.Contains("arrakis") Then
                'Dim bp = 1
                'End If
                Dim bmp As Bitmap = New Bitmap(Me.Width - k - 50, Me.Width - k - 50)
                Dim img As Image = My.Resources.NoImage
                If item.photo.Length > 0 Then
                    Dim link As Uri = New Uri("https://www.outworldz.com/outworldz_installer/" & _type & "/" & item.photo)
                    img = GetImageFromURL(link)
                End If

                Using g As Graphics = Graphics.FromImage(bmp)
                    g.DrawImage(img, 0, 0, bmp.Width, bmp.Height)
                End Using

                Dim size = Format(item.size / (1024 * 1024), "###0.00")
                Dim str = item.name & vbCrLf & size & "MB" & vbCrLf & item.license
                DataGridView.Rows.Add(bmp)
                Dim cell As DataGridViewCell = DataGridView.Rows(ctr).Cells(0)
                cell.ToolTipText = str

                ctr += 1
            Next
        Catch ex As Exception
            Form1.Log("Error", ex.Message)
        End Try

        For Each x As DataGridViewRow In DataGridView.Rows
            x.MinimumHeight = Me.Width - k
        Next

    End Sub

    Private Sub ToolStripMenuItem30_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem30.Click
        Form1.Help("Load OAR-IAR")
    End Sub

    Private Class JSONresult

#Region "Public Fields"

        Public [Date] As String
        Public license As String
        Public name As String
        Public photo As String
        Public size As String

#End Region

    End Class

#End Region

End Class
