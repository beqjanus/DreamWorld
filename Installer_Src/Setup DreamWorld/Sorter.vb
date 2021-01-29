#Region "Copyright AGPL3.0"

' Copyright Outworldz, LLC.
' AGPL3.0  https://opensource.org/licenses/AGPL

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

Imports System.Collections
Imports System.Windows.Forms

Public Class ListViewColumnSorter
    Implements IComparer

    Private ColumnToSort As Integer
    Private ObjectCompare As CaseInsensitiveComparer
    Private OrderOfSort As SortOrder

    Public Sub New(ByVal col As Integer, o As SortOrder)
        ColumnToSort = col
        Order = o
        ObjectCompare = New CaseInsensitiveComparer()
    End Sub

    Public Property Order As SortOrder
        Set(ByVal value As SortOrder)
            OrderOfSort = value
        End Set
        Get
            Return OrderOfSort
        End Get
    End Property

    Public Property SortColumn As Integer
        Set(ByVal value As Integer)
            ColumnToSort = value
        End Set
        Get
            Return ColumnToSort
        End Get
    End Property

    ''' <summary>
    ''' required to suppress a possible recursion
    ''' https://docs.microsoft.com/en-us/dotnet/fundamentals/code-analysis/quality-rules/ca1033
    ''' </summary>
    ''' <param name="x"></param>
    ''' <param name="y"></param>
    ''' <returns> 1 if >, -1 if <, 0 if the ssame </returns>
    Public Function Compare(x As Object, y As Object) As Integer

        If x Is Nothing Then Return 0
        If y Is Nothing Then Return 0

        Dim compareResult As Integer
        Dim listviewX, listviewY As ListViewItem
        listviewX = CType(x, ListViewItem)
        listviewY = CType(y, ListViewItem)
        Dim a = listviewX.SubItems(ColumnToSort).Text
        Dim b = listviewY.SubItems(ColumnToSort).Text
        compareResult = ObjectCompare.Compare(a, b)

        If Order = SortOrder.Ascending Then
            Return compareResult
        ElseIf Order = SortOrder.Descending Then
            Return (-compareResult)
        Else
            Return 0
        End If

    End Function

    ''' <summary>
    ''' The sorter is based on https://docs.microsoft.com/en-us/troubleshoot/dotnet/csharp/sort-listview-by-column
    ''' </summary>
    ''' <param name="x"></param>
    ''' <param name="y"></param>
    ''' <returns> 1 if >, -1 if <, 0 if the ssame </returns>
    Private Function IComparer_Compare(x As Object, y As Object) As Integer Implements IComparer.Compare

        If x Is Nothing Then Return 0
        If y Is Nothing Then Return 0

        Dim compareResult As Integer
        Dim listviewX, listviewY As ListViewItem
        listviewX = CType(x, ListViewItem)
        listviewY = CType(y, ListViewItem)
        Dim a = listviewX.SubItems(ColumnToSort).Text
        Dim b = listviewY.SubItems(ColumnToSort).Text
        compareResult = ObjectCompare.Compare(a, b)

        If Order = SortOrder.Ascending Then
            Return compareResult
        ElseIf Order = SortOrder.Descending Then
            Return (-compareResult)
        Else
            Return 0
        End If
    End Function

End Class
