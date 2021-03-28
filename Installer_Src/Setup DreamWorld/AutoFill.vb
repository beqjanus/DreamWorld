Imports System.Threading

Module AutoFill

    Public TreeList As New List(Of String)
    ReadOnly Terrains As New List(Of String)

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

    Public Sub GenLand(RegionUUID As String)

        If Settings.TerrainType = "Flat" Then
            RPC_Region_Command(RegionUUID, "terrain fill 23")
            RPC_Region_Command(RegionUUID, "generate map")

        ElseIf Settings.TerrainType = "Water" Then
            RPC_Region_Command(RegionUUID, "terrain fill 1")

        ElseIf Settings.TerrainType = "Random" Then
            Try
                Dim r = Between(Terrains.Count - 1, 0)
                Dim Type As String = Terrains(r)
                Debug.Print($"Terrain for {PropRegionClass.RegionName(RegionUUID)} set to {Type}")
                RPC_Region_Command(RegionUUID, $"terrain load {Type}")

                ' TODO:" terrain flip
            Catch
            End Try

        ElseIf Settings.TerrainType = "AI" Then
            Dim min = Between(40, 30)
            Dim taper = Between(35, 10)

            RPC_Region_Command(RegionUUID, $"terrain modify min {min} -ell=128,128,120 -taper=-{taper}")

            Dim r = Between(5, 0)
            Select Case r
                Case 1
                    RPC_Region_Command(RegionUUID, $"terrain modify smooth 0.5 -taper=0.6")
                Case 2
                    RPC_Region_Command(RegionUUID, $"terrain modify smooth 0.8 -taper=.2")
                Case 3
                    RPC_Region_Command(RegionUUID, $"terrain modify min {min} -rec=128,128,120 -taper=-{taper}")
                Case 4
                    RPC_Region_Command(RegionUUID, $"terrain noise 1 0")
            End Select

        End If

    End Sub

    Public Sub GenTrees(RegionUUID As String)

        Dim UseTree As New List(Of String)
        For Each t In TreeList
            If GetSetting(t) Then
                UseTree.Add(t)
            End If
        Next
        If UseTree.Count = 0 Then Return
        Dim r = Between(UseTree.Count, 0)
        Dim Type As String = UseTree(r)

        Debug.Print($"Planting {PropRegionClass.RegionName(RegionUUID)}")
        If Not RPC_Region_Command(RegionUUID, "tree active true") Then Return
        If Not RPC_Region_Command(RegionUUID, $"tree load Trees/{Type}.xml") Then Return
        If Not RPC_Region_Command(RegionUUID, $"tree plant {Type}") Then Return
        If Not RPC_Region_Command(RegionUUID, "tree rate 1000") Then Return
        Sleep(3000)
        If Not RPC_Region_Command(RegionUUID, $"tree freeze {Type} true") Then Return
        If Not RPC_Region_Command(RegionUUID, "tree active false") Then Return

    End Sub

    Public Sub InitTrees()

        Dim TerrainDirectoryInfo As New System.IO.DirectoryInfo(IO.Path.Combine(Settings.OpensimBinPath, "Terrains"))
        Dim fileSystemInfo As System.IO.FileSystemInfo
        For Each fileSystemInfo In TerrainDirectoryInfo.GetFileSystemInfos
            Dim n = fileSystemInfo.Name
            If n.EndsWith(".r32", StringComparison.InvariantCultureIgnoreCase) Or
               n.EndsWith(".png", StringComparison.InvariantCultureIgnoreCase) Or
               n.EndsWith(".raw", StringComparison.InvariantCultureIgnoreCase) Then
                Dim terrain = fileSystemInfo.FullName
                Terrains.Add(terrain)
            End If
        Next
        Debug.Print($"{Terrains.Count} Terrains")

        Dim TreeDirectoryInfo As New System.IO.DirectoryInfo(IO.Path.Combine(Settings.OpensimBinPath, "Trees"))
        For Each fileSystemInfo In TreeDirectoryInfo.GetFileSystemInfos
            Dim n = fileSystemInfo.Name
            If n.EndsWith(".xml", StringComparison.InvariantCultureIgnoreCase) Then
                Dim part = IO.Path.GetFileName(n)
                part = part.Replace(".xml", "")
                TreeList.Add(part)
            End If
        Next
        Debug.Print($"{TreeList.Count} Trees")

    End Sub

    Private Function GetSetting(tree As String) As Boolean
        Dim b As Boolean
        Select Case Settings.GetMySetting(tree)
            Case ""
                b = True
            Case "True"
                b = True
            Case "False"
                b = False
            Case Else
                b = False
        End Select
        Return b

    End Function

    Private Sub LandMaker(Spot As Lump)

        Dim RegionXY As New List(Of String)
        For Each RegionUUID In PropRegionClass.RegionUuids
            RegionXY.Add($"{PropRegionClass.CoordX(RegionUUID)}-{PropRegionClass.CoordY(RegionUUID)}")
        Next

        'Debug.Print($"{RegionXY.Count} regions")

        Dim xy As New List(Of String)
        Dim X As Integer = CInt(Spot.X)
        Dim Y As Integer = CInt(Spot.Y)
        Dim Group = $"Sim Surround {X}-{Y}"

        Dim Size As Integer = CInt(Spot.Size)

        xy.Add($"{X}-{Y + 1}") ' North
        xy.Add($"{X + 1}-{Y + 1}") ' NorthEast
        xy.Add($"{X + 1}-{Y}") ' East
        xy.Add($"{X + 1}-{Y - 1}") ' Southeast
        xy.Add($"{X}-{Y - 1}") ' South
        xy.Add($"{X - 1}-{Y - 1}") ' SW
        xy.Add($"{X - 1}-{Y}") ' West
        xy.Add($"{X - 1}-{Y + 1}") ' NW

        Dim l As New List(Of String)
        Dim LastUUID As String = ""
        For Each possible As String In xy
            If RegionXY.Contains(possible) Then
                Debug.Print($"Skipping {possible}")
            Else
                Dim parts As String() = possible.Split(New Char() {"-"c}) ' split at the space
                Dim nX = CInt(CStr(parts(0).Trim))
                Dim nY = CInt(CStr(parts(1).Trim))
                LastUUID = MakeTempRegion(Group, nX, nY)
            End If
        Next

        Landscaper(LastUUID)
        PropUpdateView = True ' make form refresh

    End Sub

    Private Sub Landscaper(RegionUUID As String)

        If RegionUUID.Length = 0 Then Return

        ReBoot(RegionUUID)

        ' Wait for it
        WaitForBooting(RegionUUID)
        If EstateName(RegionUUID).Length = 0 Then
            Dim i = 10
            While i > 0
                ConsoleCommand(RegionUUID, "{enter")

                i -= 1
            End While
        End If

        WaitForBooted(RegionUUID)
        Dim Group = PropRegionClass.GroupName(RegionUUID)
        For Each UUID In PropRegionClass.RegionUuidListByName(Group)
            GenLand(RegionUUID)
            Application.DoEvents()
            GenTrees(RegionUUID)
            Application.DoEvents()
            RPC_Region_Command(RegionUUID, "generate map")
        Next

    End Sub

    Private Function MakeTempRegion(Group As String, X As Integer, Y As Integer) As String

        Dim shortname = $"Sim Surround {X}-{Y}"
        Dim RegionUUID As String
        RegionUUID = PropRegionClass.CreateRegion(shortname)

        If IO.File.Exists(IO.Path.Combine(Settings.OpensimBinPath, $"Regions\{Group}\Region\{shortname}.ini")) Then
            Return RegionUUID
        End If

        'kill it
        DeRegisterPosition(X, Y)

        ' build it
        PropRegionClass.CreateRegion(shortname)

        PropRegionClass.CrashCounter(RegionUUID) = 0
        PropRegionClass.CoordX(RegionUUID) = X
        PropRegionClass.CoordY(RegionUUID) = Y
        PropRegionClass.Concierge(RegionUUID) = "True"
        PropRegionClass.SmartStart(RegionUUID) = "True"
        PropRegionClass.Teleport(RegionUUID) = "False"
        PropRegionClass.SizeX(RegionUUID) = 256
        PropRegionClass.SizeY(RegionUUID) = 256
        PropRegionClass.GroupName(RegionUUID) = Group
        PropRegionClass.Concierge(RegionUUID) = "False"

        PropRegionClass.RegionIniFilePath(RegionUUID) = IO.Path.Combine(Settings.OpensimBinPath, $"Regions\{Group}\Region\{shortname}.ini")
        PropRegionClass.RegionIniFolderPath(RegionUUID) = IO.Path.Combine(Settings.OpensimBinPath, $"Regions\{Group}\Region")
        PropRegionClass.OpensimIniPath(RegionUUID) = IO.Path.Combine(Settings.OpensimBinPath, $"Regions\{Group}")

        Dim port = PropRegionClass.LargestPort
        PropRegionClass.GroupPort(RegionUUID) = port
        PropRegionClass.RegionPort(RegionUUID) = port

        ' TODO: delete from disk, use HTTP
        PropRegionClass.WriteRegionObject(Group, shortname)

        Return RegionUUID

    End Function

    Public Class LandStuff
        Public Name As String
        Public RC As RegionMaker
    End Class

    Public Class Lump
        Public Size As Integer
        Public X As Integer
        Public Y As Integer
    End Class

End Module
