Module Build
    Public Terrains As New List(Of String)
    Public TreeList As New List(Of String)

    Public Sub GenLand(RegionUUID As String)

        If Settings.TerrainType = "Flat" Then
            RPC_Region_Command(RegionUUID, $"terrain fill {Settings.WaterHeight}")

        ElseIf Settings.TerrainType = "Water" Then
            RPC_Region_Command(RegionUUID, "terrain fill 0")

        ElseIf Settings.TerrainType = "Random" Then

            Dim r = Between(Terrains.Count - 1, 0)
            Dim Type As String = Terrains(r)

            RPC_Region_Command(RegionUUID, Command)

            Dim Fortytwo = Between(5, 1)
            Select Case Fortytwo
                Case 1
                    RPC_Region_Command(RegionUUID, "terrain flip x")
                Case 2
                    RPC_Region_Command(RegionUUID, "terrain flip y")
                Case 3
                    RPC_Region_Command(RegionUUID, "terrain flip x")
                    RPC_Region_Command(RegionUUID, "terrain flip y")
            End Select

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

        Modifiers(RegionUUID)
        'force update - Force the region to send all clients updates about all objects.
        RPC_Region_Command(RegionUUID, "force update")

    End Sub

    Public Sub GenTrees(RegionUUID As String)

        Dim UseTree As New List(Of String)
        For Each t As String In TreeList
            If GetSetting(t) Then
                UseTree.Add(t)
            End If
        Next
        If UseTree.Count = 0 Then Return

        RPC_Region_Command(RegionUUID, "tree active true")
        For Each TT As String In TreeList
            If Not RPC_Region_Command(RegionUUID, $"tree remove {TT}") Then
                Diagnostics.Debug.Print("Error")
                Return
            End If
        Next

        Dim r = Between(UseTree.Count, 0)
        Dim Type As String = UseTree(r)

        Debug.Print($"Planting {PropRegionClass.RegionName(RegionUUID)}")

        For Each NewType In UseTree
            If Not RPC_Region_Command(RegionUUID, $"tree load Trees/{NewType}.xml") Then Return
            If Not RPC_Region_Command(RegionUUID, $"tree freeze {NewType} false") Then Return
            If Not RPC_Region_Command(RegionUUID, $"tree plant {NewType}") Then Return
        Next

        If Not RPC_Region_Command(RegionUUID, "tree rate 1000") Then Return
        Sleep(2000)

        For Each NewType In UseTree
            If Not RPC_Region_Command(RegionUUID, $"tree freeze {NewType} true") Then Return
        Next

        If Not RPC_Region_Command(RegionUUID, "tree active false") Then Return

    End Sub

    Public Sub SurroundingLandMaker(RegionUUID As String)

        Dim Xstep As Integer
        Dim Ystep As Integer

        ' Make a map of occupied areas
        Dim RegionXY As New List(Of String)
        Dim Simcount As Integer
        For Each UUID In PropRegionClass.RegionUuids
            Dim SimSize As Integer = CInt(PropRegionClass.SizeX(RegionUUID) / 256)
            For Xstep = 1 To SimSize
                For Ystep = 1 To SimSize
                    Simcount += 1
                    RegionXY.Add($"{PropRegionClass.CoordX(UUID)}:{PropRegionClass.CoordY(UUID)}")
                Next
            Next
        Next

        TextPrint($"{Simcount} regions exist.")
        Dim X = PropRegionClass.CoordX(RegionUUID)
        Dim Y = PropRegionClass.CoordY(RegionUUID)
        Dim OverallSize As Integer = Settings.SurroundSize
        Dim CurrentSimSize As Integer = CInt(PropRegionClass.SizeX(RegionUUID) / 256)

        Dim xy As New List(Of String)

        Simcount = 0
        ' draw a square around the new sim
        For Xstep = -OverallSize To OverallSize + CurrentSimSize Step 1
            For Ystep = -OverallSize To OverallSize + CurrentSimSize Step 1
                xy.Add($"{X + Xstep}:{Y + Ystep}")
                Simcount += 1
            Next
        Next

        Dim GroupName = SimName(X, Y)
        TextPrint($"{Simcount} regions to be added to {GroupName}.")

        Dim l As New List(Of String)
        Dim LastUUID As String = ""
        For Each possible As String In xy
            If RegionXY.Contains(possible) Then
                Debug.Print($"Skipping {possible}")
            Else
                Dim parts As String() = possible.Split(New Char() {":"c}) ' split at the space
                Dim nX = CInt(CStr(parts(0).Trim))
                Dim nY = CInt(CStr(parts(1).Trim))

                Dim UUID = MakeTempRegion(GroupName, nX, nY)
                If UUID.Length > 0 Then
                    Landscaper(UUID)
                End If
            End If
        Next

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
                ' TODO replace with real estate
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

        Dim shortname = SimName(X, Y)

        Dim RegionUUID As String
        If IO.File.Exists(IO.Path.Combine(Settings.OpensimBinPath, $"Regions\{Group}\Region\{shortname}.ini")) Then
            Return ""
        End If

        'kill it
        DeRegisterPosition(X, Y)

        ' build it
        PropRegionClass.CreateRegion(shortname, "", shortname)

        PropRegionClass.CrashCounter(RegionUUID) = 0
        PropRegionClass.CoordX(RegionUUID) = X
        PropRegionClass.CoordY(RegionUUID) = Y
        PropRegionClass.SmartStart(RegionUUID) = "True"
        PropRegionClass.Teleport(RegionUUID) = "False"
        PropRegionClass.SizeX(RegionUUID) = 256
        PropRegionClass.SizeY(RegionUUID) = 256
        PropRegionClass.GroupName(RegionUUID) = Group

        PropRegionClass.RegionIniFilePath(RegionUUID) = IO.Path.Combine(Settings.OpensimBinPath, $"Regions\{Group}\Region\{shortname}.ini")
        PropRegionClass.RegionIniFolderPath(RegionUUID) = IO.Path.Combine(Settings.OpensimBinPath, $"Regions\{Group}\Region")
        PropRegionClass.OpensimIniPath(RegionUUID) = IO.Path.Combine(Settings.OpensimBinPath, $"Regions\{Group}")

        Dim port = PropRegionClass.LargestPort
        PropRegionClass.GroupPort(RegionUUID) = port
        PropRegionClass.RegionPort(RegionUUID) = port

        '   TODO: delete from disk, use HTTP
        PropRegionClass.WriteRegionObject(Group, shortname)

        Return RegionUUID

    End Function

    Private Sub Modifiers(RegionUUID As String)

        If Settings.LandSmooth Then RPC_Region_Command(RegionUUID, $"smooth {Settings.LandSmoothValue} -taper={Settings.LandTaper}")
        If Settings.LandNoise Then RPC_Region_Command(RegionUUID, "terrain noise 1 0")

    End Sub

    Private Sub RenameSims()

    End Sub

    Private Function SimName(X, Y) As String

        Return $"Sim Surround {X}-{Y}"

    End Function

#Region "Settings"

    Public Sub PutSetting(name As String, value As Boolean)

        Settings.SetMySetting(name, CStr(value))
        Settings.SaveSettings()

    End Sub

    Private Function GetSetting(Box As String) As Boolean
        Dim b As Boolean
        Select Case Settings.GetMySetting(Box)
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

#End Region

End Module
