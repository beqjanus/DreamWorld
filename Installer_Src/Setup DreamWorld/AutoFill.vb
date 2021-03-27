Imports System.Threading

Module AutoFill

    Public Sub Build(Avatars As Dictionary(Of String, String))

        If Not Settings.AutoFill Then Return
        If Avatars.Count = 0 Then Return

        For Each Agent In Avatars
            If Agent.Value.Length > 0 Then

                Dim RegionName = Agent.Value
                Dim RegionUUID = PropRegionClass.FindRegionByName(RegionName)
                Dim X = PropRegionClass.CoordX(RegionUUID)
                Dim Y = PropRegionClass.CoordY(RegionUUID)
                If X = 0 Or Y = 0 Then Continue For

                Dim Size = PropRegionClass.SizeX(RegionUUID)
                Size = CInt(Size / 256)

                Dim o As New Lump With {
                    .X = X,
                    .Y = Y,
                    .Size = Size
                }
                Dim start As ParameterizedThreadStart = AddressOf LandMaker

                Dim _WebThread1 = New Thread(start)
                _WebThread1.SetApartmentState(ApartmentState.STA)
                _WebThread1.Priority = ThreadPriority.BelowNormal
                _WebThread1.Start(o)

            End If

        Next

    End Sub

    Private Sub GenLand(RegionUUID As String)

        RPC_Region_Command(RegionUUID, "terrain fill 23")

    End Sub

    Private Sub GenTrees(RegionUUID As String)

        RPC_Region_Command(RegionUUID, "tree active true")
        RPC_Region_Command(RegionUUID, "tree load Trees/Pine1.xml")
        RPC_Region_Command(RegionUUID, "tree plant Pine1")
        RPC_Region_Command(RegionUUID, "tree rate 1000")
        Sleep(3000)
        RPC_Region_Command(RegionUUID, "tree freeze Pine1 true")
        RPC_Region_Command(RegionUUID, "tree active false")

    End Sub

    Private Sub LandMaker(Spot As Lump)

        Dim RegionXY As New List(Of String)
        For Each RegionUUID In PropRegionClass.RegionUuids
            RegionXY.Add($"{PropRegionClass.CoordX(RegionUUID)} {PropRegionClass.CoordX(RegionUUID)}")
        Next

        Dim xy As New List(Of String)
        Dim X As Integer = CInt(Spot.X)
        Dim Y As Integer = CInt(Spot.Y)
        Dim Size As Integer = CInt(Spot.Size)

        xy.Add($"{X} {Y + 1}") ' North
        xy.Add($"{X + 1} {Y + 1}") ' NorthEast
        xy.Add($"{X + 1} {Y}") ' East
        xy.Add($"{X + 1} {Y - 1}") ' Southeast
        xy.Add($"{X} {Y - 1}") ' South
        xy.Add($"{X - 1} {Y - 1}") ' SW
        xy.Add($"{X - 1} {Y}") ' West
        xy.Add($"{X - 1} {Y + 1}") ' NW

        Dim l As New List(Of String)
        For Each possible As String In xy
            If Not RegionXY.Contains(possible) Then
                Dim parts As String() = possible.Split(New Char() {" "c}) ' split at the space
                Dim nX = CInt(CStr(parts(0).Trim))
                Dim nY = CInt(CStr(parts(1).Trim))
                MakeTempRegion(nX, nY)
            End If
        Next

    End Sub

    Private Sub Landscaper(O As Object)

        WaitForBooted(CStr(O))
        GenLand(CStr(O))
        GenTrees(CStr(O))

    End Sub

    Private Sub MakeTempRegion(X As Integer, Y As Integer)

        Dim shortname = $"Region {X}, {Y}"
        Dim RegionUUID As String
        RegionUUID = PropRegionClass.CreateRegion(shortname)

        If IO.File.Exists(IO.Path.Combine(Settings.OpensimBinPath, $"Regions\{shortname}\Region\{shortname}.ini")) Then
            ReBoot(RegionUUID)
            Return
        End If

        PropRegionClass.CrashCounter(RegionUUID) = 0
        PropRegionClass.CoordX(RegionUUID) = X
        PropRegionClass.CoordY(RegionUUID) = Y
        PropRegionClass.Concierge(RegionUUID) = "True"
        PropRegionClass.SmartStart(RegionUUID) = "True"
        PropRegionClass.Teleport(RegionUUID) = "True"
        PropRegionClass.SizeX(RegionUUID) = 256
        PropRegionClass.SizeY(RegionUUID) = 256
        PropRegionClass.GroupName(RegionUUID) = shortname

        PropRegionClass.RegionIniFilePath(RegionUUID) = IO.Path.Combine(Settings.OpensimBinPath, $"Regions\{shortname}\Region\{shortname}.ini")
        PropRegionClass.RegionIniFolderPath(RegionUUID) = IO.Path.Combine(Settings.OpensimBinPath, $"Regions\{shortname}\Region")
        PropRegionClass.OpensimIniPath(RegionUUID) = IO.Path.Combine(Settings.OpensimBinPath, $"Regions\{shortname}")

        Dim port = PropRegionClass.LargestPort
        PropRegionClass.GroupPort(RegionUUID) = port
        PropRegionClass.RegionPort(RegionUUID) = port
        PropRegionClass.WriteRegionObject(shortname)
        PropUpdateView = True ' make form refresh
        Application.DoEvents()
        ReBoot(RegionUUID)

        ' Wait for it
        WaitForBooting(RegionUUID)
        If EstateName(RegionUUID).Length = 0 Then
            ConsoleCommand(RegionUUID, "{Enter}")
        End If

        If GetPrimCount(RegionUUID) = 0 Then
            Dim start As ParameterizedThreadStart = AddressOf Landscaper
            Dim _WebThread1 = New Thread(start)
            _WebThread1.SetApartmentState(ApartmentState.STA)
            _WebThread1.Priority = ThreadPriority.BelowNormal
            _WebThread1.Start(RegionUUID)
        End If

    End Sub

    Public Class Lump
        Public Size As Integer
        Public X As Integer
        Public Y As Integer
    End Class

End Module
