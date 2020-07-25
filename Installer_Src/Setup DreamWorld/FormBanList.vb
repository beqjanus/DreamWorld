Imports System.Globalization
Imports System.Net
Imports System.Text.RegularExpressions

Public Class FormBanList

    Dim Saveneeded As Boolean = False

#Region "ScreenSize"

    'The following detects  the location of the form in screen coordinates
    Private _screenPosition As ScreenPos

    Private colsize As New ScreenPos("BanList")
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
        ScreenPosition.SaveHW(Me.Height, Me.Width)
    End Sub

    Private Sub SetScreen()

        Me.Show()
        ScreenPosition = New ScreenPos(MyBase.Name)
        AddHandler ResizeEnd, Handler
        Dim xy As List(Of Integer) = ScreenPosition.GetXY()
        Me.Left = xy.Item(0)
        Me.Top = xy.Item(1)
        Dim hw As List(Of Integer) = ScreenPosition.GetHW()

        If hw.Item(0) = 0 Then
            Me.Height = 356
        Else
            Me.Height = hw.Item(0)
        End If
        If hw.Item(1) = 0 Then
            Me.Width = 826
        Else
            Me.Width = hw.Item(1)
        End If

    End Sub

#End Region

#Region "Start Stop"

    Private Sub Q() Handles Me.Closing

        If Saveneeded = False Then Return

        Dim MACString As String = ""
        Dim ViewerString As String = ""
        Dim GridString As String = ""
        Dim fname = My.Computer.FileSystem.OpenTextFileWriter(Form1.PropMyFolder & "/Outworldzfiles/BanList.txt", False)
        Try

            For Each row As DataGridViewRow In DataGridView1.Rows
                Dim s As String
                Dim t As String

                ' handle DBNull and empty lines

                If IsDBNull(row.Cells(0).Value) Or row.Cells(0).Value Is Nothing Then
                    s = ""
                Else
                    s = row.Cells(0).Value
                End If
                If IsDBNull(row.Cells(1).Value) Or row.Cells(1).Value Is Nothing Then
                    t = ""
                Else
                    t = row.Cells(1).Value
                End If

                Debug.Print("BanList Add " & s)
                ' save to Ban List
                fname.WriteLine(s & "|" & t)

                ' Ban IP's
                Dim I As System.Net.IPAddress = Nothing
                If IPAddress.TryParse(s, I) Then
                    Firewall.BlockIP(s)
                    Continue For
                End If

                ' ban MAC Addresses with and without caps and :
                Dim pattern1 As Regex = New Regex("[0-9a-zA-Z]:")
                Dim match1 As Match = pattern1.Match(s)
                If match1.Success Then
                    MACString += s & " "
                    Continue For
                End If

                ' ban grid Addresses
                Dim pattern2 As Regex = New Regex("^http:\/\/\w+:\d+$")
                Dim match2 As Match = pattern2.Match(s)
                If match2.Success Then
                    GridString += s & " "
                    Continue For
                End If

                ' none of the above, must be a viewer
                If s.Length > 0 Then ViewerString += s & "|"

            Next

            If Settings.ServerType = "Robust" Then
                ' Robust Process only
                If Settings.LoadIni(Form1.PropOpensimBinPath & "bin\Robust.HG.ini.proto", ";") Then
                    MsgBox(My.Resources.Error_word)
                    Return
                End If

                ' Ban grids
                Settings.SetIni("LoginService", "AllowExcept", GridString)
                Settings.SetIni("UserAgentService", "AllowExcept_Level_200", GridString)

                ' Ban Macs
                Settings.SetIni("LoginService", "DeniedMacs", MACString)
                Settings.SetIni("GateKeeper", "DeniedMacs", MACString)

                'Ban Viewers
                Settings.SetIni("AccessControl", "DeniedClients", ViewerString)

                Settings.SaveINI(System.Text.Encoding.UTF8)

                If Form1.IsRobustRunning() Then
                    Form1.PropAborting = True
                    Form1.StopRobust()
                    Form1.StartRobust()
                    Form1.PropAborting = False
                End If
            End If
        Catch ex As Exception
            Form1.ErrorLog("Ban List:" & ex.Message)
        Finally
            fname.Close()
        End Try

    End Sub

    Public Sub LoadCollectionData() Handles Me.Load

        SetScreen()
        GetData()
        BringToFront()

        Form1.HelpOnce("BanList")

    End Sub

#End Region

    Private Sub HelpToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HelpToolStripMenuItem.Click

        Form1.Help("BanList")

    End Sub

    Private Sub GetData()

        Try

            ' Populate a New data table And bind it to the BindingSource.
            Dim table As DataTable = New DataTable

            'Create column.
            Dim column1 As DataColumn = New DataColumn()
            column1.DataType = System.Type.GetType("System.String")
            column1.ColumnName = "Banned"
            column1.AutoIncrement = False
            column1.Caption = My.Resources.Banned_word
            column1.ReadOnly = False
            column1.Unique = False
            ' Add the column to the table.
            table.Columns.Add(column1)

            Dim column2 As DataColumn = New DataColumn()
            column2.DataType = System.Type.GetType("System.String")
            column2.ColumnName = "Comment"
            column2.AutoIncrement = False
            column2.Caption = My.Resources.Comment_or_Notes_Word
            column2.ReadOnly = False
            column2.Unique = False
            ' Add the column to the table.
            table.Columns.Add(column2)

            'i.e, Firestorm-Release 4.6.7.42398

            table.Locale = CultureInfo.InvariantCulture

            Dim filename As String
            If System.IO.File.Exists(Form1.PropMyFolder & "/Outworldzfiles/BanList.txt") Then
                filename = Form1.PropMyFolder & "/Outworldzfiles/BanList.txt"
            Else
                filename = Form1.PropMyFolder & "/Outworldzfiles/Opensim/BanListProto.txt"
            End If

            Dim line As String
            Using reader As IO.StreamReader = System.IO.File.OpenText(filename)
                'now loop through each line
                While reader.Peek <> -1
                    line = reader.ReadLine()
                    If line.Length > 1 Then
                        Dim words() = line.Split("|")
                        table.Rows.Add(words(0), words(1))

                        ' remove all IPs from firewall as they are read - new ones or edited ones will be saved back on clode
                        Dim I As System.Net.IPAddress = Nothing
                        If IPAddress.TryParse(words(0), I) Then Firewall.ReleaseIp(words(0))

                    End If
                End While
            End Using

            DataGridView1.DataSource = table

            DataGridView1.Columns(0).Width = colsize.ColumnWidth("Banned", 240)
            DataGridView1.Columns(1).Width = colsize.ColumnWidth("Comment", 500)
        Catch ex As Exception

        End Try

    End Sub

    Private Sub DataGridView1_ColumnWidthChanged(sender As Object, e As DataGridViewColumnEventArgs) Handles DataGridView1.ColumnWidthChanged

        Dim w As String = e.Column.Width.ToString(Globalization.CultureInfo.InvariantCulture)
        Dim name As String = e.Column.Name.ToString(Globalization.CultureInfo.CurrentCulture)

        colsize.putSize(name, w)
        Diagnostics.Debug.Print(name & " " & w.ToString(Globalization.CultureInfo.InvariantCulture))
        colsize.SaveFormSettings()

    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellEndEdit

        Saveneeded = True

    End Sub

    Private Sub DataGridView1_CellContentAdded(sender As Object, e As DataGridViewRowEventArgs) Handles DataGridView1.UserAddedRow

        Saveneeded = True

    End Sub

    Private Sub DataGridView1_Delrow(sender As Object, e As DataGridViewRowEventArgs) Handles DataGridView1.UserDeletedRow

        Saveneeded = True

    End Sub

End Class