Imports System.Collections
Imports System.Windows.Forms

Public Class ListViewColumnSorter
    Implements IComparer

    Private ColumnToSort As Integer
    Private OrderOfSort As SortOrder
    Private ObjectCompare As CaseInsensitiveComparer

    ''' <summary>
    ''' Compares two items in the List view of regions, and returns a  >, < or equal.

    ''' </summary>
    ''' <param name="col"></param>
    ''' <param name="o"></param>
    Public Sub New(ByVal col As Integer, o As SortOrder)
        ColumnToSort = col
        Order = o
        ObjectCompare = New CaseInsensitiveComparer()
    End Sub

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

    Public Property SortColumn As Integer
        Set(ByVal value As Integer)
            ColumnToSort = value
        End Set
        Get
            Return ColumnToSort
        End Get
    End Property

    Public Property Order As SortOrder
        Set(ByVal value As SortOrder)
            OrderOfSort = value
        End Set
        Get
            Return OrderOfSort
        End Get
    End Property

End Class
