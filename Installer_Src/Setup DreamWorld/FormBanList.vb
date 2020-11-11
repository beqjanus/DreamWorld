Imports System.Globalization
Imports System.Net
Imports System.Text.RegularExpressions

Public Class FormBanList

    Dim Saveneeded As Boolean

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

    Public Sub LoadCollectionData() Handles Me.Load

        SetScreen()
        GetData()
        BringToFront()

        HelpOnce("BanList")

    End Sub

    <CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId:="Outworldz.Form1.ErrorLog(System.String)")>
    Private Sub Q() Handles Me.Closing

        If Saveneeded = False Then Return

        Dim MACString As String = ""
        Dim ViewerString As String = ""
        Dim GridString As String = ""
        Dim BanListString As String = ""
        Try

            For Each row As DataGridViewRow In DataGridView1.Rows
                Dim s As String
                Dim t As String

                ' handle DBNull and empty lines

                If IsDBNull(row.Cells(0).Value.ToString) Or row.Cells(0).Value.ToString Is Nothing Then
                    s = ""
                Else
                    s = row.Cells(0).Value.ToString.Trim
                End If
                If IsDBNull(row.Cells(1).Value.ToString) Or row.Cells(1).Value.ToString Is Nothing Then
                    t = ""
                Else
                    t = row.Cells(1).Value.ToString.Trim
                End If

                ' save back to Ban List
                If (s.Length + t.Length) > 0 Then BanListString += s & "=" & t & "|"
                Debug.Print(s)

                ' ban grid Addresses
                Dim pattern2 As Regex = New Regex("^https?\:\/\/.*?\:\d+$")
                Dim match2 As Match = pattern2.Match(s)
                If match2.Success And Not s.StartsWith("#", System.StringComparison.InvariantCulture) Then
                    GridString += s & ","   ' delimiter is a comma for grids
                    Continue For
                End If

                ' Ban IP's
                Dim I As System.Net.IPAddress = Nothing
                If IPAddress.TryParse(s, I) Then
                    Firewall.BlockIP(s)
                    Continue For
                End If

                ' ban MAC Addresses with and without caps and :
                Dim pattern1 As Regex = New Regex("^[a-f0-9A-F][a-f0-9A-F][-:]+")
                Dim match1 As Match = pattern1.Match(s)
                If match1.Success And Not s.StartsWith("#", System.StringComparison.InvariantCulture) Then
                    MACString += s & " " ' delimiter is a " " and  not a pipe
                    Continue For
                End If

                ' none of the above
                If s.Length > 0 And Not s.StartsWith("#", System.StringComparison.InvariantCulture) Then
                    ViewerString += s & "|"
                End If

            Next

            If Settings.ServerType = "Robust" Then
                ' Robust Process only
                If Settings.LoadIni(Settings.OpensimBinPath & "Robust.HG.ini.proto", ";") Then
                    MsgBox(My.Resources.Error_word)
                    Return
                End If

                ' Ban grids
                If GridString.Length > 0 Then
                    GridString = Mid(GridString, 1, GridString.Length - 1)
                End If
                Settings.SetIni("GatekeeperService", "AllowExcept", GridString)

                ' Ban Macs
                If MACString.Length > 0 Then
                    MACString = Mid(MACString, 1, MACString.Length - 1)
                End If
                Settings.SetIni("LoginService", "DeniedMacs", MACString)
                Settings.SetIni("GatekeeperService", "DeniedMacs", MACString)

                'Ban Viewers
                If ViewerString.Length > 0 Then
                    ViewerString = Mid(ViewerString, 1, ViewerString.Length - 1)
                End If
                If ViewerString.Length > 0 Then
                    ViewerString = Mid(ViewerString, 1, ViewerString.Length - 1)
                End If

                Settings.SetIni("AccessControl", "DeniedClients", ViewerString)

                Settings.SaveINI(System.Text.Encoding.UTF8)

                If Form1.IsRobustRunning() Then
                    Me.Hide()
                    Form1.PropAborting = True
                    Form1.StopRobust()
                    Form1.StartRobust()
                    Form1.PropAborting = False
                End If
            End If
            ' Do not catch general exception types
        Catch ex As Exception
            ' Do not catch general exception types
            BreakPoint.Show(ex.Message)
            '
        Finally
            Settings.BanList = BanListString
            Settings.SaveSettings()
        End Try

        Application.DoEvents()

    End Sub

#End Region

#Region "Private Subs"

    Private Sub DataGridView1_CellContentAdded(sender As Object, e As DataGridViewRowEventArgs) Handles DataGridView1.UserAddedRow

        Saveneeded = True

    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellEndEdit

        Saveneeded = True

    End Sub

    Private Sub DataGridView1_ColumnWidthChanged(sender As Object, e As DataGridViewColumnEventArgs) Handles DataGridView1.ColumnWidthChanged

        Dim w As Integer = e.Column.Width
        Dim name As String = e.Column.Name.ToString(Globalization.CultureInfo.CurrentCulture)

        colsize.PutSize(name, w)
        Diagnostics.Debug.Print(name & " " & w.ToString(Globalization.CultureInfo.InvariantCulture))
        colsize.SaveFormSettings()

    End Sub

    Private Sub DataGridView1_Delrow(sender As Object, e As DataGridViewRowEventArgs) Handles DataGridView1.UserDeletedRow

        Saveneeded = True

    End Sub

    <CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId:="Outworldz.Form1.ErrorLog(System.String)")>
    Private Sub GetData()

        Try

            ' Populate a New data table And bind it to the BindingSource.
            Dim table As DataTable = New DataTable

            'Create column.
            Dim column1 As DataColumn = New DataColumn With {
                .DataType = System.Type.GetType("System.String"),
                .ColumnName = Global.Outworldz.My.Resources.Banned_word,
                .AutoIncrement = False,
                .Caption = Global.Outworldz.My.Resources.Banned_word,
                .ReadOnly = False,
                .Unique = False
            }
            ' Add the column to the table.
            table.Columns.Add(column1)

            Dim column2 As DataColumn = New DataColumn With {
                .DataType = System.Type.GetType("System.String"),
                .ColumnName = Global.Outworldz.My.Resources.Comment_or_Notes_Word,
                .AutoIncrement = False,
                .Caption = Global.Outworldz.My.Resources.Comment_or_Notes_Word,
                .ReadOnly = False,
                .Unique = False
            }
            ' Add the column to the table.
            table.Columns.Add(column2)

            'i.e, Firestorm-Release 4.6.7.42398

            table.Locale = CultureInfo.InvariantCulture

            Dim filename As String = ""

            If System.IO.File.Exists(IO.Path.Combine(Settings.CurrentDirectory, "/Outworldzfiles/BanList.txt")) Then
                filename = IO.Path.Combine(Settings.CurrentDirectory, "/Outworldzfiles/BanList.txt")
                Saveneeded = True
            ElseIf Settings.BanList.Length > 0 Then

                Dim words() = Settings.BanList.Split("|".ToCharArray)

                For index As Integer = 0 To words.Length - 1
                    Dim elems() As String = words(index).Split("="c)
                    If elems.Length = 1 Then
                        table.Rows.Add(elems(0).Trim, "")
                    ElseIf elems.Length = 2 Then
                        table.Rows.Add(elems(0).Trim, elems(1).Trim)
                    End If
                    ' remove all IPs from firewall as they are read - new ones or edited ones will be saved back on clode
                    Dim I As System.Net.IPAddress = Nothing
                    If IPAddress.TryParse(elems(0).Trim, I) Then
                        Firewall.ReleaseIp(elems(0).Trim)
                        Saveneeded = True
                    End If
                Next
            Else
                filename = IO.Path.Combine(Settings.CurrentDirectory, "/Outworldzfiles/Opensim/BanListProto.txt")
            End If

            If filename.Length > 0 Then
                Dim line As String
                Using reader As IO.StreamReader = System.IO.File.OpenText(filename)
                    'now loop through each line
                    While reader.Peek <> -1
                        line = reader.ReadLine()
                        If line.Length > 1 Then
                            Dim words() = line.Split("|".ToCharArray)
                            table.Rows.Add(words(0), words(1))

                            ' remove all IPs from firewall as they are read - new ones or edited ones will be saved back on clode
                            Dim I As System.Net.IPAddress = Nothing
                            If IPAddress.TryParse(words(0), I) Then Firewall.ReleaseIp(words(0))

                        End If
                    End While
                End Using
            End If

            ' kill the old file, we store in Settings.ini now.
            If System.IO.File.Exists(IO.Path.Combine(Settings.CurrentDirectory, "/Outworldzfiles/BanList.txt")) Then
                My.Computer.FileSystem.DeleteFile(IO.Path.Combine(Settings.CurrentDirectory, "/Outworldzfiles/BanList.txt"))
            End If

            DataGridView1.DataSource = table
            DataGridView1.Columns(0).Width = colsize.ColumnWidth(My.Resources.Banned_word, 240)
            DataGridView1.Columns(1).Width = colsize.ColumnWidth(My.Resources.Comment_or_Notes_Word, 500)
            ' Do not catch general exception types
        Catch ex As Exception
            ' Do not catch general exception types
            BreakPoint.Show(ex.Message)
            Form1.ErrorLog("Banlist:" & ex.Message)
        End Try

    End Sub

    Private Sub HelpToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HelpToolStripMenuItem.Click

        HelpManual("BanList")

    End Sub

#End Region

End Class
