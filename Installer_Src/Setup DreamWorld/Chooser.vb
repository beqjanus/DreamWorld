Imports System.ComponentModel

Public Class Choice
    Implements IDisposable

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

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        SetScreen()

    End Sub

    Public Sub FillGrid(type As String, Optional JustRunning As Boolean = False)
        Dim columnHeaderStyle As New DataGridViewCellStyle With {
            .Font = New Font("Verdana", 10, FontStyle.Bold)
        }
        DataGridView.ColumnHeadersDefaultCellStyle = columnHeaderStyle
        OKButton1.Enabled = False
        DataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DataGridView.MultiSelect = False

        DataGridView.Text = "Select from..."
        Dim PropRegionClass As RegionMaker = RegionMaker.Instance()

        Dim L As New List(Of String)

        ' add to list Unique Name
        If type = "Group" Then
            DataGridView.Rows.Add("! Add New Name")
        End If

        For Each RegionNumber As Integer In PropRegionClass.RegionNumbers
            Dim name As String
            If type = "Group" Then
                name = PropRegionClass.GroupName(RegionNumber)
            Else
                name = PropRegionClass.RegionName(RegionNumber)
            End If

            ' Only show running sims option
            If (JustRunning And PropRegionClass.IsBooted(RegionNumber)) Then
                If L.Contains(name) Then
                Else
                    If name.Length > 0 Then DataGridView.Rows.Add(name)
                End If
            ElseIf Not JustRunning Then
                If L.Contains(name) Then
                Else
                    If name.Length > 0 Then DataGridView.Rows.Add(name)
                End If
            End If

            L.Add(name)
        Next

        DataGridView.Sort(Group, ListSortDirection.Ascending)

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles OKButton1.Click

        Dim selectedRowCount = DataGridView.Rows.GetRowCount(DataGridViewElementStates.Selected)
        If selectedRowCount > 1 Then
            MsgBox("Please select only one row", vbInformation, "Oops")
        End If
        If selectedRowCount = 1 Then
            DialogResult = DialogResult.OK
        End If
    End Sub

    Private Sub RowClick(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles DataGridView.CellDoubleClick

        If e.RowIndex = -1 Then
            Return
        End If
        OKButton1.Enabled = True
        DialogResult = DialogResult.OK
    End Sub

    Private Sub CellClick(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles DataGridView.CellClick
        OKButton1.Enabled = True
    End Sub

    Private Sub CancelButton1_Click(sender As Object, e As EventArgs) Handles CancelButton1.Click
        DialogResult = DialogResult.Cancel
    End Sub

End Class