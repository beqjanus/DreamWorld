Module Maps

    Public Sub DeleteMaps(RegionUUID As String)

        Dim path = IO.Path.Combine(Settings.OpensimBinPath(), "maptiles\00000000-0000-0000-0000-000000000000")

        For i = 1 To 8
            For XX = 0 To PropRegionClass.SizeX(RegionUUID) / 256
                For YY = 0 To PropRegionClass.SizeY(RegionUUID) / 256
                    Dim x1 = PropRegionClass.CoordX(RegionUUID) + XX
                    Dim y1 = PropRegionClass.CoordY(RegionUUID) + YY
                    Dim file = IO.Path.Combine(path, $"map-{i}-{x1}-{y1}-objects.jpg")
                    Debug.Print($"Delete map-{i}-{x1}-{y1}-objects.jpg")
                    DeleteFile(file)
                Next
            Next
        Next

    End Sub

End Module
