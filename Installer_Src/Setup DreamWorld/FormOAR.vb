Imports System.Net
Imports System.Threading
Imports Newtonsoft.Json

Public Class FormOAR

#Region "JSON"

    Public Class JSONresult

        Public [Date] As String
        Public Cache As Image
        Public license As String
        Public name As String
        Public photo As String
        Public size As String
        Public str As String

    End Class

#End Region

#Region "Private Fields"

    Private _initted As Boolean = False
    Private _type As String = Nothing
    Private imgSize As Integer = 256
    Private initSize As Integer = 512
    Private k As Integer = 50
    Private NumColumns As Integer

#End Region

#Region "Public Fields"

    Private json() As JSONresult

#End Region

#Region "Draw"

    Public Sub Redraw()

        Dim gdTextColumn As New DataGridViewTextBoxColumn
        DataGridView.DefaultCellStyle.WrapMode = DataGridViewTriState.True

        Dim gdImageColumn As New DataGridViewImageColumn
        DataGridView.Columns.Add(gdImageColumn)
        DataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.Fill
        DataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing

        'add 10 px padding to bottom
        DataGridView.RowTemplate.DefaultCellStyle.Padding = New Padding(5, 5, 5, 5)

        DataGridView.RowHeadersVisible = False
        DataGridView.Width = initSize
        DataGridView.ColumnHeadersHeight = initSize
        DataGridView.ShowCellToolTips = True
        DataGridView.AllowUserToAddRows = False

        NumColumns = Math.Ceiling(Me.Width / imgSize)
        If NumColumns = 0 Then
            NumColumns = 1
        End If

        DataGridView.Height = Me.Height - 100
        DataGridView.RowTemplate.Height = Math.Ceiling((Me.Width - k) / NumColumns)
        DataGridView.RowTemplate.MinimumHeight = Math.Ceiling((Me.Width - k) / NumColumns)

        DataGridView.Width = Me.Width - 50
        'DataGridView.Columns(0).Width = Me.Width - k
        DataGridView.ColumnHeadersHeight = Math.Ceiling((Me.Width - k) / NumColumns)

        DataGridView.SuspendLayout()

        DataGridView.Columns.Clear()
        DataGridView.Rows.Clear()
        DataGridView.ClearSelection()

        For index = 1 To NumColumns 'How many do you want?
            Dim col As New DataGridViewImageColumn
            With col
                .Width = (Me.Width - k) / NumColumns
                .Name = "Details" & CStr(index)
            End With
            DataGridView.Columns.Insert(0, col)
        Next
        Dim column = 0
        Dim rowcounter = 0
        If json IsNot Nothing Then
            Dim cnt = json.Length
            Me.Text = CStr(cnt) & " Items"

            For Each item In json
                Application.DoEvents()
                Debug.Print("Item:" & item.name)

                If column = 0 Then DataGridView.Rows.Add()
                Save(item, rowcounter, column)

                column += 1
                If column = NumColumns Then
                    rowcounter += 1
                    column = 0
                End If
            Next
        Else
            DataGridView.Rows.Add()
            While column < NumColumns
                DataGridView.Rows(rowcounter).Cells(column).Value = My.Resources.NoImage
                column += 1
            End While
        End If

        While column < NumColumns And column > 0
            DataGridView.Rows(rowcounter).Cells(column).Value = My.Resources.Blank256
            column += 1
        End While

        For Each x As DataGridViewRow In DataGridView.Rows
            x.MinimumHeight = (Me.Width - k) / NumColumns
        Next
        DataGridView.ResumeLayout()
        DataGridView.Show()

    End Sub

    Private Sub DataGridView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView.CellClick

        Try
            Dim File As String = json(e.ColumnIndex + (NumColumns * e.RowIndex)).name
            File = Form1.PropDomain() & "/Outworldz_Installer/" & _type & "/" & File 'make a real URL
            If File.EndsWith(".oar", StringComparison.InvariantCultureIgnoreCase) Or
                File.EndsWith(".gz", StringComparison.InvariantCultureIgnoreCase) Or
                File.EndsWith(".tgz", StringComparison.InvariantCultureIgnoreCase) Then
                Form1.LoadOARContent(File)
            ElseIf File.EndsWith(".iar", StringComparison.InvariantCultureIgnoreCase) Then
                Form1.LoadIARContent(File)
            End If
        Catch ex As System.IndexOutOfRangeException
        End Try

    End Sub

#End Region

#Region "IO"

    Private Shared Function GetImageFromURL(ByVal url As Uri) As Image

        Dim req As WebRequest = System.Net.WebRequest.Create(url)
        Try
            Using request As System.Net.WebResponse = req.GetResponse
                Using stream As IO.Stream = request.GetResponseStream
                    Return New Bitmap(System.Drawing.Image.FromStream(stream))
                End Using
            End Using
        Catch ex As NotImplementedException
            Form1.Log("Warn", ex.Message)
        Catch ex As NotSupportedException
            Form1.Log("Warn", ex.Message)
        Catch ex As ArgumentNullException
            Form1.Log("Warn", ex.Message)
        Catch ex As ArgumentException
            Form1.Log("Warn", ex.Message)
        Catch ex As System.Security.SecurityException
            Form1.Log("Warn", ex.Message)
        Catch ex As Exception
            Form1.Log("Warn", ex.Message)
        End Try

        Return Nothing

    End Function

    Private Function GetTextFromURL(ByVal url As Uri) As String

        Dim retVal As String = Nothing
        Try
            Using client As WebClient = New WebClient()
                Return client.DownloadString(url)
            End Using
        Catch ex As ArgumentNullException
            Form1.Log("Warn", ex.Message)
        Catch ex As WebException
            Form1.Log("Warn", ex.Message)
        Catch ex As NotSupportedException
            Form1.Log("Warn", ex.Message)
        End Try
        Return ""

    End Function

    Private Sub Save(item As JSONresult, row As Integer, col As Integer)
        Try
            If item.Cache.Width > 0 Then
                DataGridView.Rows(row).Cells(col).Value = item.Cache
                DataGridView.Rows(row).Cells(col).ToolTipText = item.str
            Else
                DataGridView.Rows(row).Cells(col).Value = NoImage(item)
            End If
        Catch ex As System.NullReferenceException
            Form1.Log("Error", ex.Message)
        End Try

    End Sub

    Private Sub ToolStripMenuItem30_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem30.Click

        If _type = "IAR" Then Form1.Help("Load IAR")
        If _type = "OAR" Then Form1.Help("Load OAR")

    End Sub

#End Region

#Region "ScreenSize"

    Private _screenPosition As ScreenPos
    Dim aHeight As Integer
    Dim aWidth As Integer
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
        If Not _initted Then Return
        ScreenPosition.SaveXY(Me.Left, Me.Top)
        ScreenPosition.SaveHW(Me.Height, Me.Width)

        If Height <> aHeight Or Width <> aWidth Then
            aHeight = Height
            aWidth = Width
            Redraw()
        End If

    End Sub

    Private Sub SetScreen()

        ScreenPosition = New ScreenPos(Me.Name)
        AddHandler ResizeEnd, Handler
        Dim xy As List(Of Integer) = ScreenPosition.GetXY()
        Me.Left = xy.Item(0)
        Me.Top = xy.Item(1)
        Dim hw As List(Of Integer) = ScreenPosition.GetHW()

        If hw.Item(0) = 0 Then
            Me.Height = initSize
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
        Me.Hide()

        InitiateThread()

    End Sub

    Public Sub ShowForm()

        Me.Show()
        Redraw()
        If _type = "OAR" Then Form1.HelpOnce("Load OAR")
        If _type = "IAR" Then Form1.HelpOnce("Load IAR")
    End Sub

    Private Function DoWork() As JSONresult

        json = GetData()
        json = ImageToJson(json)

        _initted = True
        Return Nothing

    End Function

    Private Sub Form_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        Me.Hide()
        DataGridView.Hide()
        SetScreen()

    End Sub

    Private Sub Form1_Closed(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles MyBase.FormClosing

        Me.Hide()
        e.Cancel = True

    End Sub

    Private Sub InitiateThread()

        Dim WebThread As New Thread(DirectCast(Function() DoWork(), ThreadStart))

        Try
            WebThread.SetApartmentState(ApartmentState.STA)
        Catch ex As ArgumentException
            Form1.Log(My.Resources.Error_word, ex.Message)
        Catch ex As ThreadStartException
            Form1.Log(My.Resources.Error_word, ex.Message)
        Catch ex As InvalidOperationException
            Form1.Log(My.Resources.Error_word, ex.Message)
        End Try
        WebThread.Start()

    End Sub

#End Region

#Region "Data"

    Private Function GetData()

        Dim result As String = Nothing
        Using client As New WebClient ' download client for web pages
            Try
                Dim str = Form1.SecureDomain() & "/outworldz_installer/JSON/" & _type & ".json?r=1" & Form1.GetPostData()
                result = client.DownloadString(str)
            Catch ex As ArgumentNullException
                Form1.ErrorLog(My.Resources.Wrong & " " & ex.Message)
                Return Nothing
            Catch ex As WebException
                Form1.ErrorLog(My.Resources.Wrong & " " & ex.Message)
                Return Nothing
            Catch ex As NotSupportedException
                Form1.ErrorLog(My.Resources.Wrong & " " & ex.Message)
                Return Nothing
            End Try
        End Using
        Try
            json = JsonConvert.DeserializeObject(Of JSONresult())(result)
        Catch
            Return Nothing
        End Try
        Return json

    End Function

    Private Sub RefreshToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RefreshToolStripMenuItem.Click

        DataGridView.Hide()
        json = GetData()
        json = ImageToJson(json)
        Redraw()

    End Sub

#End Region

#Region "Imagery"

    Private Shared Function NoImage(item As JSONresult) As Image

        Dim bmp = My.Resources.Blank256
        Dim drawFont As Font = New Font("Arial", 12)

        Dim newImage = New Bitmap(256, 256)
        Try
            Dim gr = Graphics.FromImage(newImage)
            gr.DrawImageUnscaled(bmp, 0, 0)

            gr.DrawString(item.name, drawFont, Brushes.Black, 30, 100)
        Catch ex As Exception
            Dim bp = 1
        End Try

        Return newImage

    End Function

    Private Function ImageToJson(ByVal json() As JSONresult)

        If json IsNot Nothing Then

            For Each item In json
                Application.DoEvents()
                Debug.Print("Item:" & item.name)

                Dim bmp As Bitmap = New Bitmap(imgSize, imgSize)
                If item.Cache IsNot Nothing Then
                    Using g As Graphics = Graphics.FromImage(bmp)
                        g.DrawImage(item.Cache, 0, 0, bmp.Width, bmp.Height)
                    End Using
                Else
                    Dim img As Image = Nothing
                    If item.photo.Length > 0 Then
                        Try
                            Dim link As Uri = New Uri("https://www.outworldz.com/Outworldz_installer/" & _type & "/" & item.photo)
                            img = GetImageFromURL(link)
                        Catch ex As Exception
                        End Try
                    End If

                    If img Is Nothing Then
                        img = NoImage(item)
                    End If
                    Try
                        Using g As Graphics = Graphics.FromImage(bmp)
                            g.DrawImage(img, 0, 0, bmp.Width, bmp.Height)
                        End Using
                    Catch ex As Exception
                        Dim bp = 1
                    End Try

                End If
                item.Cache = bmp

                item.size = Format(item.size / (1024 * 1024), "###0.00")
                item.str = item.name & vbCrLf & item.size & "MB" & vbCrLf & item.license

            Next
        End If

        Return json

    End Function

#End Region

End Class
