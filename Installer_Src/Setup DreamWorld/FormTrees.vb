Public Class FormTrees

    Private _Regionname As String
    Private _RegionUUID As String

    Private Sub FormTrees_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.Hide()
        _Regionname = ChooseRegion(True)
        _RegionUUID = PropRegionClass.FindRegionByName(_Regionname)
        If _RegionUUID.Length > 0 Then
            Me.Show()
            Me.Text = _Regionname
        Else
            Me.Close()
        End If

    End Sub

End Class
