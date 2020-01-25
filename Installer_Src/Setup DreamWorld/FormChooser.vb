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

Imports System.ComponentModel

Public Class Choice

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

#Region "Public Methods"

    Public Sub FillGrid(type As String, Optional JustRunning As Boolean = False)
        Dim columnHeaderStyle As New DataGridViewCellStyle With {
            .Font = New Font("Verdana", 10, FontStyle.Bold)
        }
        DataGridView.ColumnHeadersDefaultCellStyle = columnHeaderStyle
        OKButton1.Enabled = False
        DataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DataGridView.MultiSelect = False

        DataGridView.Text = My.Resources.Select_word
        Dim PropRegionClass As RegionMaker = RegionMaker.Instance()

        Dim L As New List(Of String)

        ' add to list Unique Name
        If type = "Group" Then
            DataGridView.Rows.Add("! Add New Name")
        End If

        For Each RegionUUID As String In PropRegionClass.RegionUUIDs
            Dim name As String
            If type = "Group" Then
                name = PropRegionClass.GroupName(RegionUUID)
            Else
                name = PropRegionClass.RegionName(RegionUUID)
            End If

            ' Only show running sims option
            If (JustRunning And PropRegionClass.IsBooted(RegionUUID)) Then
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

#End Region

#Region "Private Methods"

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles OKButton1.Click

        Dim selectedRowCount = DataGridView.Rows.GetRowCount(DataGridViewElementStates.Selected)
        If selectedRowCount > 1 Then
            MsgBox(My.Resources.Please_select_only_one_row, vbInformation, My.Resources.Info)
        End If
        If selectedRowCount = 1 Then
            DialogResult = DialogResult.OK
        End If
    End Sub

    Private Sub CancelButton1_Click(sender As Object, e As EventArgs) Handles CancelButton1.Click
        DialogResult = DialogResult.Cancel
    End Sub

    Private Sub CellClick(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles DataGridView.CellClick
        OKButton1.Enabled = True
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        SetScreen()
        BringToFront()

    End Sub

    Private Sub RowClick(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles DataGridView.CellDoubleClick

        If e.RowIndex = -1 Then
            Return
        End If
        OKButton1.Enabled = True
        DialogResult = DialogResult.OK
    End Sub

#End Region

End Class
