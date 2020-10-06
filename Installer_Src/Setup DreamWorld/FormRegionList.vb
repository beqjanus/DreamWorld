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

Imports System.IO
Imports System.Text.RegularExpressions

Public Class RegionList

#Region "Declarations"

    Private Shared _FormExists As Boolean
    Private ReadOnly colsize As New ScreenPos("Region List")
    Private _ImageListLarge As ImageList
#Disable Warning CA2213
    Private _ImageListSmall As New ImageList
#Enable Warning CA2213
    Private initted = False
    Private ItemsAreChecked As Boolean

    Private pixels As Integer = 70
    Private TheView As Integer = ViewType.Details

    Private ViewNotBusy As Boolean

    Private Enum ViewType As Integer
        Maps = 0
        Icons = 1
        Details = 2
        Avatars = 3
    End Enum

#End Region

#Region "Properties"

    Public Shared Property FormExists1 As Boolean
        Get
            Return _FormExists
        End Get
        Set(value As Boolean)
            _FormExists = value
        End Set
    End Property

    ' property exposing FormExists
    Public Shared ReadOnly Property InstanceExists() As Boolean
        Get
            ' Access shared members through the Class name, not an instance.
            Return RegionList.FormExists1
        End Get
    End Property

    Public Property ImageListLarge1 As ImageList
        Get
            Return _ImageListLarge
        End Get
        Set(value As ImageList)
            _ImageListLarge = value
        End Set
    End Property

    Public Property ImageListSmall1 As ImageList
        Get
            Return _ImageListSmall
        End Get
        Set(value As ImageList)
            _ImageListSmall = value
        End Set
    End Property

    Public Property ItemsAreChecked1 As Boolean
        Get
            Return ItemsAreChecked
        End Get
        Set(value As Boolean)
            ItemsAreChecked = value
        End Set
    End Property

    Public Property Pixels1 As Integer
        Get
            Return pixels
        End Get
        Set(value As Integer)
            pixels = value
        End Set
    End Property

    Public Property ScreenPosition As ScreenPos
        Get
            Return _screenPosition
        End Get
        Set(value As ScreenPos)
            _screenPosition = value
        End Set
    End Property

    Public Property TheView1 As Integer
        Get
            Return TheView
        End Get
        Set(value As Integer)
            TheView = value
        End Set
    End Property

    Public Property ViewBusy As Boolean
        Get
            Return ViewNotBusy
        End Get
        Set(value As Boolean)
            ViewNotBusy = value
        End Set
    End Property

    Shared Property PropUpdateView() As Boolean
        Get
            Return Form1.PropUpdateView
        End Get
        Set(ByVal Value As Boolean)
            Form1.PropUpdateView = Value
        End Set
    End Property

#End Region

#Region "ScreenSize"

    Private ReadOnly Handler As New EventHandler(AddressOf Resize_page)
    Private _screenPosition As ScreenPos

    'The following detects  the location of the form in screen coordinates
    Private Sub Resize_page(ByVal sender As Object, ByVal e As System.EventArgs)

        ScreenPosition.SaveXY(Me.Left, Me.Top)
        ScreenPosition.SaveHW(Me.Height, Me.Width)

    End Sub

    Private Sub SetScreen()

        Me.Show()
        Try
            ScreenPosition = New ScreenPos(MyBase.Name)
            AddHandler ResizeEnd, Handler
            Dim xy As List(Of Integer) = ScreenPosition.GetXY()
            Me.Left = xy.Item(0)
            Me.Top = xy.Item(1)
            Dim hw As List(Of Integer) = ScreenPosition.GetHW()
            '1106, 460
            If hw.Item(0) = 0 Then
                Me.Height = 460
            Else
                Me.Height = hw.Item(0)
            End If
            If hw.Item(1) = 0 Then
                Me.Width = 1106
            Else
                Me.Width = hw.Item(1)
            End If
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
            Me.Height = 460
            Me.Width = 1106
            Me.Left = 100
            Me.Top = 100
        End Try

    End Sub

#End Region

#Region "Layout"

    Private Sub ListView1_ColumnWidthChanged(sender As Object, e As ColumnWidthChangedEventArgs) Handles ListView1.ColumnWidthChanged

        Dim w = ListView1.Columns(e.ColumnIndex).Width
        Dim name = ListView1.Columns(e.ColumnIndex).Text

        ScreenPosition.PutSize(name & TheView1.ToString(Globalization.CultureInfo.InvariantCulture), w)
        Diagnostics.Debug.Print(name & TheView1.ToString(Globalization.CultureInfo.InvariantCulture) & " " & w.ToString(Globalization.CultureInfo.InvariantCulture))
        ScreenPosition.SaveFormSettings()

    End Sub

    Private Sub Panel1_MouseWheel(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles ListView1.MouseWheel

        If TheView1 = ViewType.Maps And Not ViewBusy Then
            ' Update the drawing based upon the mouse wheel scrolling.
            Dim numberOfTextLinesToMove As Integer = CInt(e.Delta * SystemInformation.MouseWheelScrollLines / 120)

            Pixels1 += numberOfTextLinesToMove
            'Debug.Print(pixels.ToString)
            If Pixels1 > 256 Then Pixels1 = 256
            If Pixels1 < 10 Then Pixels1 = 10

            LoadMyListView()
        End If

    End Sub

    Private Sub RegionList_Layout(sender As Object, e As LayoutEventArgs) Handles Me.Layout

        Dim X = Me.Width - 45
        Dim Y = Me.Height - 150
        ListView1.Size = New System.Drawing.Size(X, Y)
        AvatarView.Size = New System.Drawing.Size(X, Y)

    End Sub

#End Region

#Region "Public Enums"

    ' icons image list layout
    Enum DGICON
        bootingup = 0
        shuttingdown = 1
        up = 2
        disabled = 3
        stopped = 4
        recyclingdown = 5
        recyclingup = 6
        warning = 7
        user1 = 8
        user2 = 9
        SmartStart = 10
        Home = 11
        HomeOffline = 12
        Pending = 13
        Suspended = 14
    End Enum

#End Region

#Region "Loader"

    Private Sub Form_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        Settings.RegionListVisible = False
        Settings.SaveSettings()
        FormExists1 = False
        _ImageListSmall.Dispose()
        colsize.Dispose()

    End Sub

    <CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId:="System.Windows.Forms.ListView+ColumnHeaderCollection.Add(System.String,System.Int32,System.Windows.Forms.HorizontalAlignment)")>
    Private Sub LoadForm(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ViewBusy = True
        FormExists1 = True
        Pixels1 = 70

        AllNone.Checked = True

        ListView1.Visible = False
        ListView1.LabelWrap = True
        ListView1.AutoArrange = True

        Settings.RegionListVisible = True
        Settings.SaveSettings()

        Me.Name = "Region List"
        Me.Text = Global.Outworldz.My.Resources.Region_List
        AvatarView.Hide()
        AvatarView.CheckBoxes = False

        ' Set the view to show details.
        TheView1 = Settings.RegionListView()
        SetScreen()

        Dim W As View
        If TheView1 = ViewType.Details Then
            W = View.Details
            ListView1.CheckBoxes = True
        ElseIf TheView1 = ViewType.Icons Then
            W = View.SmallIcon
            ListView1.CheckBoxes = False
        ElseIf TheView1 = ViewType.Maps Then
            ListView1.CheckBoxes = False
            W = View.LargeIcon
        End If

        ListView1.View = W
        AvatarView.View = View.Details

        ' Allow the user to edit item text.
        ListView1.LabelEdit = True
        AvatarView.LabelEdit = False

        ' Allow the user to rearrange columns.
        ListView1.AllowColumnReorder = True
        AvatarView.AllowColumnReorder = False

        Me.ListView1.ListViewItemSorter = New ListViewItemComparer(2)
        Me.AvatarView.ListViewItemSorter = New ListViewItemComparer(1)

        ' Select the item and sub items when selection is made.
        ListView1.FullRowSelect = True
        AvatarView.FullRowSelect = True

        ' Display grid lines.
        ListView1.GridLines = True
        AvatarView.GridLines = True

        ' Sort the items in the list in ascending order.
        ListView1.Sorting = SortOrder.Ascending
        AvatarView.Sorting = SortOrder.Ascending

        ListView1.AllowColumnReorder = True

        ' Create columns for details, icons
        ListView1.Columns.Add(My.Resources.Enable_word, colsize.ColumnWidth(My.Resources.Enable_word & "2", 120), HorizontalAlignment.Center)

        ' for details
        ListView1.Columns.Add(My.Resources.DOS_Box_word, colsize.ColumnWidth(My.Resources.DOS_Box_word & "2", 120), HorizontalAlignment.Left)
        ListView1.Columns.Add(My.Resources.Agents_word, colsize.ColumnWidth(My.Resources.Agents_word & "2", 50), HorizontalAlignment.Center)
        ListView1.Columns.Add(My.Resources.Status_word, colsize.ColumnWidth(My.Resources.Status_word & "2", 120), HorizontalAlignment.Center)
        ListView1.Columns.Add(My.Resources.RAM_Word, colsize.ColumnWidth(My.Resources.RAM_Word & "2", 80), HorizontalAlignment.Center)
        ListView1.Columns.Add(My.Resources.Region_Ports_word, colsize.ColumnWidth(My.Resources.Region_Ports_word & "2", 50), HorizontalAlignment.Center)
        ListView1.Columns.Add(My.Resources.XMLRPC, colsize.ColumnWidth(My.Resources.XMLRPC, 50), HorizontalAlignment.Center)
        ListView1.Columns.Add("X".ToUpperInvariant, colsize.ColumnWidth("X".ToUpperInvariant & "2", 50), HorizontalAlignment.Center)
        ListView1.Columns.Add("Y".ToUpperInvariant, colsize.ColumnWidth("Y".ToUpperInvariant & "2", 50), HorizontalAlignment.Center)
        ListView1.Columns.Add(My.Resources.Size_word, colsize.ColumnWidth(My.Resources.Size_word & "2", 40), HorizontalAlignment.Center)
        ListView1.Columns.Add(My.Resources.Estate_word, colsize.ColumnWidth(My.Resources.Estate_word & "2", 100), HorizontalAlignment.Left)

        ' optional
        ListView1.Columns.Add(My.Resources.Scripts_word, colsize.ColumnWidth(My.Resources.Scripts_word & "2", 80), HorizontalAlignment.Center)
        ListView1.Columns.Add(My.Resources.Maps_word, colsize.ColumnWidth(My.Resources.Maps_word & "2", 80), HorizontalAlignment.Center)
        ListView1.Columns.Add(My.Resources.Physics_word, colsize.ColumnWidth(My.Resources.Physics_word & "2", 120), HorizontalAlignment.Center)
        ListView1.Columns.Add(My.Resources.Birds_word, colsize.ColumnWidth(My.Resources.Birds_word & "2", 60), HorizontalAlignment.Center)
        ListView1.Columns.Add(My.Resources.Tides_word, colsize.ColumnWidth(My.Resources.Tides_word & "2", 60), HorizontalAlignment.Center)
        ListView1.Columns.Add(My.Resources.Teleport_word, colsize.ColumnWidth(My.Resources.Teleport_word & "2", 65), HorizontalAlignment.Center)
        ListView1.Columns.Add(My.Resources.Smart_Start_word, colsize.ColumnWidth(My.Resources.Smart_Start_word & "2", 80), HorizontalAlignment.Center)
        ListView1.Columns.Add(My.Resources.Allow_Or_Disallow_Gods_word, colsize.ColumnWidth(My.Resources.Allow_Or_Disallow_Gods_word & "2", 75), HorizontalAlignment.Center)
        ListView1.Columns.Add(My.Resources.Owner_God, colsize.ColumnWidth(My.Resources.Owner_God & "2", 75), HorizontalAlignment.Center)
        ListView1.Columns.Add(My.Resources.Manager_God_word, colsize.ColumnWidth(My.Resources.Manager_God_word & "2", 80), HorizontalAlignment.Center)
        ListView1.Columns.Add(My.Resources.No_Autobackup, colsize.ColumnWidth(My.Resources.No_Autobackup & "2", 90), HorizontalAlignment.Center)
        ListView1.Columns.Add(My.Resources.Publicity_Word, colsize.ColumnWidth(My.Resources.Publicity_Word & "2", 80), HorizontalAlignment.Center)
        ListView1.Columns.Add(My.Resources.Script_Rate_word, colsize.ColumnWidth(My.Resources.Script_Rate_word & "2", 80), HorizontalAlignment.Center)
        ListView1.Columns.Add(My.Resources.Frame_Rate_word, colsize.ColumnWidth(My.Resources.Frame_Rate_word & "2", 80), HorizontalAlignment.Center)

        'Add the items to the ListView.
        ' Connect the ListView.ColumnClick event to the ColumnClick event handler.
        AddHandler Me.ListView1.ColumnClick, AddressOf ColumnClick

        AvatarView.Columns.Add(My.Resources.Agents_word, 150, HorizontalAlignment.Center)
        AvatarView.Columns.Add(My.Resources.Region_word, 150, HorizontalAlignment.Center)
        AvatarView.Columns.Add(My.Resources.Type_word, 80, HorizontalAlignment.Center)

        ' index to display icons
        ImageListSmall1.Images.Add(My.Resources.ResourceManager.GetObject("navigate_up2", Globalization.CultureInfo.InvariantCulture))   ' 0 booting up
        ImageListSmall1.Images.Add(My.Resources.ResourceManager.GetObject("navigate_down2", Globalization.CultureInfo.InvariantCulture)) ' 1 shutting down
        ImageListSmall1.Images.Add(My.Resources.ResourceManager.GetObject("check2", Globalization.CultureInfo.InvariantCulture)) ' 2 okay, up
        ImageListSmall1.Images.Add(My.Resources.ResourceManager.GetObject("media_stop_red", Globalization.CultureInfo.InvariantCulture)) ' 3 disabled
        ImageListSmall1.Images.Add(My.Resources.ResourceManager.GetObject("media_stop", Globalization.CultureInfo.InvariantCulture))  ' 4 enabled, stopped
        ImageListSmall1.Images.Add(My.Resources.ResourceManager.GetObject("navigate_down", Globalization.CultureInfo.InvariantCulture))  ' 5 Recycling down
        ImageListSmall1.Images.Add(My.Resources.ResourceManager.GetObject("navigate_up", Globalization.CultureInfo.InvariantCulture))  ' 6 Recycling Up
        ImageListSmall1.Images.Add(My.Resources.ResourceManager.GetObject("warning", Globalization.CultureInfo.InvariantCulture))  ' 7 Unknown
        ImageListSmall1.Images.Add(My.Resources.ResourceManager.GetObject("user2", Globalization.CultureInfo.InvariantCulture))  ' 8 - 1 User
        ImageListSmall1.Images.Add(My.Resources.ResourceManager.GetObject("users1", Globalization.CultureInfo.InvariantCulture))  ' 9 - 2 user
        ImageListSmall1.Images.Add(My.Resources.ResourceManager.GetObject("refresh", Globalization.CultureInfo.InvariantCulture))  ' 10 - 2 user
        ImageListSmall1.Images.Add(My.Resources.ResourceManager.GetObject("home", Globalization.CultureInfo.InvariantCulture))  '  11- home
        ImageListSmall1.Images.Add(My.Resources.ResourceManager.GetObject("home_02", Globalization.CultureInfo.InvariantCulture))  '  12- home _offline
        ImageListSmall1.Images.Add(My.Resources.ResourceManager.GetObject("media_pause", Globalization.CultureInfo.InvariantCulture))  '  13- Suspended
        ImageListSmall1.Images.Add(My.Resources.ResourceManager.GetObject("error_icon", Globalization.CultureInfo.InvariantCulture))  '  14- Error
        Form1.PropUpdateView = True ' make form refresh

        ViewBusy = False
        Timer1.Interval = 250 ' check for Form1.PropUpdateView every second
        Timer1.Start() 'Timer starts functioning

        ShowTitle()
        initted = True

    End Sub

    Private Sub MyListView_AfterLabelEdit(sender As Object, e As System.Windows.Forms.LabelEditEventArgs) Handles ListView1.AfterLabelEdit

        Debug.Print(e.Label)

    End Sub

    <CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId:="System.Windows.Forms.Form.set_Text(System.String)")>
    Private Sub ShowTitle()

        Dim TotalSize As Integer
        Dim RegionCount As Integer
        Dim TotalRegionCount As Integer
        For Each RegionUUID As String In Form1.PropRegionClass.RegionUUIDs
            TotalSize += Form1.PropRegionClass.SizeX(RegionUUID) / 256 * Form1.PropRegionClass.SizeY(RegionUUID) / 256
            If Form1.PropRegionClass.RegionEnabled(RegionUUID) Then RegionCount += 1
            TotalRegionCount += 1
        Next
        Me.Text = "Regions:  " & CStr(TotalRegionCount) & ".  Enabled: " & CStr(RegionCount) & ". Total Area: " & CStr(TotalSize) & " Regions"

    End Sub

#End Region

#Region "Timer"

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick

        If PropUpdateView() Then ' force a refresh
            If ViewBusy = True Then
                Return
            End If
            LoadMyListView()
        End If

    End Sub

#End Region

#Region "LoadListView"

    Private Sub LoadMyListView()
        BringToFront()
        If TheView1 = ViewType.Avatars Then
            ShowAvatars()
        Else
            ShowRegions()
        End If

    End Sub

    <CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId:="Outworldz.Form1.Log(System.String,System.String)")>
    <CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId:="System.Windows.Forms.ListViewItem+ListViewSubItemCollection.Add(System.String)")>
    Private Sub ShowRegions()

        Try
            If ViewBusy = True Then
                Return
            End If

            ViewBusy = True
            ListView1.BeginUpdate()

            ImageListLarge1 = New ImageList()

            If Pixels1 = 0 Then Pixels1 = 24
            ImageListLarge1.ImageSize = New Size(Pixels1, Pixels1)

            ListView1.Items.Clear()
            ImageListSmall1.ImageSize = New Drawing.Size(20, 20)

            Try
                For Each RegionUUID As String In Form1.PropRegionClass.RegionUUIDs

                    Dim Num As Integer = 0
                    Dim Groupname As String = Form1.PropRegionClass.GroupName(RegionUUID)
                    Dim Status = Form1.PropRegionClass.Status(RegionUUID)

                    Dim Letter As String = ""
                    If Status = RegionMaker.SIMSTATUSENUM.Stopped _
                        And Form1.PropRegionClass.SmartStart(RegionUUID) = "True" Then
                        Letter = "Waiting"
                        Num = DGICON.SmartStart
                    ElseIf Status = RegionMaker.SIMSTATUSENUM.Suspended Then
                        Letter = "Suspended"
                        Num = DGICON.Suspended
                    ElseIf Status = RegionMaker.SIMSTATUSENUM.RecyclingDown Then
                        Letter = "Recycling Down"
                        Num = DGICON.recyclingdown
                    ElseIf Status = RegionMaker.SIMSTATUSENUM.RecyclingUp Then
                        Letter = "Recycling Up"
                        Num = DGICON.recyclingup
                    ElseIf Status = RegionMaker.SIMSTATUSENUM.RestartPending Then
                        Letter = "Restart Pending"
                        Num = DGICON.recyclingup
                    ElseIf Status = RegionMaker.SIMSTATUSENUM.RetartingNow Then
                        Letter = "Restarting Now"
                        Num = DGICON.recyclingup
                    ElseIf Status = RegionMaker.SIMSTATUSENUM.Booting Then
                        Letter = "Booting"
                        Num = DGICON.bootingup
                    ElseIf Status = RegionMaker.SIMSTATUSENUM.ShuttingDown Then
                        Letter = "Stopping"
                        Num = DGICON.shuttingdown
                    ElseIf Status = RegionMaker.SIMSTATUSENUM.RestartStage2 Then
                        Letter = "Pending"
                        Num = DGICON.Pending
                    ElseIf Status = RegionMaker.SIMSTATUSENUM.Booted And Form1.PropRegionClass.AvatarCount(RegionUUID) = 1 Then
                        Letter = "Running"
                        Num = DGICON.user1
                    ElseIf Status = RegionMaker.SIMSTATUSENUM.Booted And Form1.PropRegionClass.AvatarCount(RegionUUID) > 1 Then
                        Letter = "Running"
                        Num = DGICON.user2
                    ElseIf Status = RegionMaker.SIMSTATUSENUM.Booted Then
                        If Form1.PropRegionClass.RegionName(RegionUUID) = Settings.WelcomeRegion Then
                            Num = DGICON.Home
                            Letter = "Running"
                        Else
                            Letter = "Running"
                            Num = DGICON.up
                        End If
                    ElseIf Not Form1.PropRegionClass.RegionEnabled(RegionUUID) Then
                        Letter = "Disabled"
                        If Form1.PropRegionClass.RegionName(RegionUUID) = Settings.WelcomeRegion Then
                            Num = DGICON.HomeOffline
                        Else
                            Num = DGICON.disabled
                        End If
                    ElseIf Form1.PropRegionClass.RegionEnabled(RegionUUID) Then
                        Letter = "Stopped"
                        Num = DGICON.stopped
                    Else
                        Num = DGICON.warning ' warning
                    End If

                    ' Create items and subitems for each item. Place a check mark next to the item.
                    Dim item1 As New ListViewItem(Form1.PropRegionClass.RegionName(RegionUUID), Num) With
                        {
                            .Checked = Form1.PropRegionClass.RegionEnabled(RegionUUID)
                        }

                    item1.SubItems.Add(Form1.PropRegionClass.GroupName(RegionUUID).ToString(Globalization.CultureInfo.InvariantCulture))
                    item1.SubItems.Add(Form1.PropRegionClass.AvatarCount(RegionUUID).ToString(Globalization.CultureInfo.InvariantCulture))

                    item1.SubItems.Add(Letter)
                    Dim fmtXY = "00000" ' 65536
                    Dim fmtRam = "0000." ' 9999 MB
                    ' RAM

                    If Status = RegionMaker.SIMSTATUSENUM.Booting _
                        Or Status = RegionMaker.SIMSTATUSENUM.Booted _
                        Or Status = RegionMaker.SIMSTATUSENUM.RecyclingUp _
                        Or Status = RegionMaker.SIMSTATUSENUM.RecyclingDown _
                        Then

                        Try
                            Dim PID = Form1.PropRegionClass.ProcessID(RegionUUID)
                            Dim component1 As Process = Process.GetProcessById(PID)
                            Dim Memory As Double = (component1.WorkingSet64 / 1024) / 1024
                            item1.SubItems.Add(FormatNumber(Memory.ToString(fmtRam, Globalization.CultureInfo.InvariantCulture)))
                        Catch ex As Exception

                            BreakPoint.Show(ex.Message)
                            item1.SubItems.Add("0".ToUpperInvariant)
                        End Try
                    Else
                        item1.SubItems.Add("0".ToUpperInvariant)
                    End If

                    item1.SubItems.Add(Form1.PropRegionClass.RegionPort(RegionUUID).ToString(Globalization.CultureInfo.InvariantCulture))
                    item1.SubItems.Add(Form1.PropRegionClass.XMLRegionPort(RegionUUID).ToString(Globalization.CultureInfo.InvariantCulture))
                    item1.SubItems.Add(Form1.PropRegionClass.CoordX(RegionUUID).ToString(fmtXY, Globalization.CultureInfo.InvariantCulture))
                    item1.SubItems.Add(Form1.PropRegionClass.CoordY(RegionUUID).ToString(fmtXY, Globalization.CultureInfo.InvariantCulture))

                    ' Size of region
                    Dim s As Integer = Form1.PropRegionClass.SizeX(RegionUUID) / 256
                    Dim size As String = s & "X" & s
                    item1.SubItems.Add(size)

                    ' add estate name
                    Dim Estate = "-".ToUpperInvariant
                    If MysqlInterface.IsRunning() Then
                        Estate = MysqlInterface.EstateName(Form1.PropRegionClass.UUID(RegionUUID))
                    End If
                    item1.SubItems.Add(Estate)

                    'Scripts XEngine or YEngine
                    Select Case Form1.PropRegionClass.ScriptEngine(RegionUUID)
                        Case "YEngine"
                            item1.SubItems.Add(My.Resources.YEngine_word)
                        Case "XEngine"
                            item1.SubItems.Add(My.Resources.XEngine_word)
                        Case Else
                            item1.SubItems.Add("-".ToUpperInvariant)
                    End Select

                    'Map
                    If Form1.PropRegionClass.MapType(RegionUUID).Length > 0 Then
                        item1.SubItems.Add(Form1.PropRegionClass.MapType(RegionUUID))
                    Else
                        item1.SubItems.Add("-".ToUpperInvariant)
                    End If

                    ' physics
                    Select Case Form1.PropRegionClass.Physics(RegionUUID)
                        Case ""
                            item1.SubItems.Add("-".ToUpperInvariant)
                        Case "0"
                            item1.SubItems.Add(My.Resources.None)
                        Case "1"
                            item1.SubItems.Add(My.Resources.ODE_word_NT)
                        Case "2"
                            item1.SubItems.Add(My.Resources.Bullet_word_NT)
                        Case "3"
                            item1.SubItems.Add(My.Resources.Bullet_Threaded_word)
                        Case "4"
                            item1.SubItems.Add(My.Resources.ubODE_word)
                        Case "5"
                            item1.SubItems.Add(My.Resources.ubODE_Hybrid_word)
                        Case Else
                            item1.SubItems.Add("-".ToUpperInvariant)
                    End Select

                    'birds

                    If Form1.PropRegionClass.Birds(RegionUUID) = "True" Then
                        item1.SubItems.Add(My.Resources.Yes_word)
                    Else
                        item1.SubItems.Add("-".ToUpperInvariant)
                    End If

                    'Tides
                    If Form1.PropRegionClass.Tides(RegionUUID) = "True" Then
                        item1.SubItems.Add(My.Resources.Yes_word)
                    Else
                        item1.SubItems.Add("-".ToUpperInvariant)
                    End If

                    'teleport
                    If Form1.PropRegionClass.Teleport(RegionUUID) = "True" Then
                        item1.SubItems.Add(My.Resources.Yes_word)
                    Else
                        item1.SubItems.Add("-".ToUpperInvariant)
                    End If

                    If Form1.PropRegionClass.SmartStart(RegionUUID) = "True" Then
                        item1.SubItems.Add(My.Resources.Yes_word)
                    Else
                        item1.SubItems.Add("-".ToUpperInvariant)
                    End If

                    If Form1.PropRegionClass.AllowGods(RegionUUID).Length > 0 Then
                        item1.SubItems.Add(Form1.PropRegionClass.AllowGods(RegionUUID))
                    Else
                        item1.SubItems.Add("-".ToUpperInvariant)
                    End If

                    If Form1.PropRegionClass.RegionGod(RegionUUID).Length > 0 Then
                        item1.SubItems.Add(Form1.PropRegionClass.RegionGod(RegionUUID))
                    Else
                        item1.SubItems.Add("-".ToUpperInvariant)
                    End If

                    If Form1.PropRegionClass.ManagerGod(RegionUUID).Length > 0 Then
                        item1.SubItems.Add(Form1.PropRegionClass.ManagerGod(RegionUUID))
                    Else
                        item1.SubItems.Add("-".ToUpperInvariant)
                    End If

                    If Form1.PropRegionClass.SkipAutobackup(RegionUUID).Length > 0 Then
                        item1.SubItems.Add(Form1.PropRegionClass.SkipAutobackup(RegionUUID))
                    Else
                        item1.SubItems.Add("-".ToUpperInvariant)
                    End If

                    If Form1.PropRegionClass.RegionSnapShot(RegionUUID).Length > 0 Then
                        item1.SubItems.Add(Form1.PropRegionClass.RegionSnapShot(RegionUUID))
                    Else
                        item1.SubItems.Add("-".ToUpperInvariant)
                    End If

                    If Form1.PropRegionClass.MinTimerInterval(RegionUUID).Length > 0 Then
                        item1.SubItems.Add(Form1.PropRegionClass.MinTimerInterval(RegionUUID))
                    Else
                        item1.SubItems.Add("-".ToUpperInvariant)
                    End If

                    If Form1.PropRegionClass.FrameTime(RegionUUID).Length > 0 Then
                        item1.SubItems.Add(Form1.PropRegionClass.FrameTime(RegionUUID))
                    Else
                        item1.SubItems.Add("-".ToUpperInvariant)
                    End If

                    ' maps
                    If TheView1 = ViewType.Maps Then

                        If Status = RegionMaker.SIMSTATUSENUM.Booted Then
                            Dim img As String = "http://127.0.0.1:" + Form1.PropRegionClass.GroupPort(RegionUUID).ToString(Globalization.CultureInfo.InvariantCulture) + "/" + "index.php?method=regionImage" + Form1.PropRegionClass.UUID(RegionUUID).Replace("-".ToUpperInvariant, "")
                            Dim bmp As Image = Nothing

                            Try
                                bmp = LoadImage(img)
                            Catch ex As Exception
                                BreakPoint.Show(ex.Message)
                            End Try

                            If bmp Is Nothing Then
                                ImageListLarge1.Images.Add(My.Resources.ResourceManager.GetObject("OfflineMap", Globalization.CultureInfo.InvariantCulture))
                            Else
                                ImageListLarge1.Images.Add(bmp)
                            End If
                        Else
                            ImageListLarge1.Images.Add(My.Resources.ResourceManager.GetObject("OfflineMap", Globalization.CultureInfo.InvariantCulture))
                        End If

                    End If

                    ListView1.Items.AddRange(New ListViewItem() {item1})
                Next

                If TheView1 = ViewType.Icons Then
                    ListView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)
                ElseIf TheView1 = ViewType.Details Then
                    For Each col In ListView1.Columns
                        Dim name = col.Text
                        Using colsize As New ScreenPos(MyBase.Name & "ColumnSize")
                            col.Width = colsize.ColumnWidth(name & TheView1.ToString(Globalization.CultureInfo.InvariantCulture))
                        End Using
                    Next

                    For i As Integer = 0 To ListView1.Items.Count - 1
                        If ListView1.Items(i).Checked Then
                            ListView1.Items(i).ForeColor = SystemColors.ControlText
                        Else
                            ListView1.Items(i).ForeColor = SystemColors.GrayText
                        End If
                    Next i
                Else
                    ListView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.None)
                End If

                'Assign the ImageList objects to the ListView.
                ListView1.LargeImageList = ImageListLarge1
                ListView1.SmallImageList = ImageListSmall1

                Me.ListView1.TabIndex = 0
            Catch ex As Exception

                BreakPoint.Show(ex.Message)
                Form1.Log(My.Resources.Error_word, " RegionList " & ex.Message)
            End Try
        Catch ex As Exception

            BreakPoint.Show(ex.Message)
            Form1.Log(My.Resources.Error_word, " RegionList " & ex.Message)
        End Try

        ListView1.EndUpdate()
        PropUpdateView() = False
        ViewBusy = False
        ListView1.Show()
        AvatarView.Hide()

    End Sub

#End Region

#Region "Click Methods"

    Private Sub Addregion_Click(sender As Object, e As EventArgs) Handles AddRegionButton.Click

#Disable Warning CA2000 ' Dispose objects before losing scope
        Dim RegionForm As New FormRegion
#Enable Warning CA2000 ' Dispose objects before losing scope
        RegionForm.BringToFront()
        RegionForm.Init("")
        RegionForm.Activate()
        Application.DoEvents()
        RegionForm.Visible = True
        RegionForm.Select()

    End Sub

    Private Sub AvatarView_Click(sender As Object, e As EventArgs) Handles AvatarView.Click

        Dim regions As ListView.SelectedListViewItemCollection = Me.AvatarView.SelectedItems
        Dim item As ListViewItem

        For Each item In regions
            Dim RegionName = item.SubItems(1).Text
            Dim RegionUUID As String = Form1.PropRegionClass.FindRegionByName(RegionName)
            If RegionUUID.Length > 0 Then
                ' TODO: Needs to be HGV3
                Dim webAddress As String = "hop://" & Settings.DNSName & ":" & Settings.HttpPort & "/" & RegionName
                Try
                    Dim result = Process.Start(webAddress)
                Catch ex As Exception

                    BreakPoint.Show(ex.Message)
                End Try
            End If
        Next
        PropUpdateView() = True

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles RefreshButton.Click

        Form1.PropRegionClass.GetAllRegions()
        LoadMyListView()
        ShowTitle()

    End Sub

    ' ColumnClick event handler.
    Private Sub ColumnClick(ByVal o As Object, ByVal e As ColumnClickEventArgs)

        ListView1.SuspendLayout()
        Me.ListView1.Sorting = SortOrder.None

        ' Set the ListViewItemSorter property to a new ListViewItemComparer object. Setting this property immediately sorts the ListView using the ListViewItemComparer object.
        Me.ListView1.ListViewItemSorter = New ListViewItemComparer(e.Column)

        ListView1.ResumeLayout()

    End Sub

    Private Sub HelpToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles HelpToolStripMenuItem1.Click

        Form1.Help("RegionList")

    End Sub

    Private Sub ListClick(sender As Object, e As EventArgs) Handles ListView1.Click

        Dim regions As ListView.SelectedListViewItemCollection = Me.ListView1.SelectedItems
        Dim item As ListViewItem
        For Each item In regions
            Dim RegionName = item.SubItems(0).Text
            Dim RegionUUID As String = Form1.PropRegionClass.FindRegionByName(RegionName)
            If RegionUUID.Length > 0 Then
                StartStopEdit(RegionUUID, RegionName)
            End If
        Next

    End Sub

    Private Sub ListView1_ItemCheck1(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles ListView1.ItemCheck

        Dim Item As ListViewItem = Nothing
        Try
            Item = ListView1.Items.Item(e.Index)
        Catch ex As Exception

            BreakPoint.Show(ex.Message)
        End Try
        If Item.Text.Length = 0 Then Return

        Dim UUID As String = Form1.PropRegionClass.FindRegionByName(Item.Text)
        If UUID.Length = 0 Then Return
        Dim GroupName = Form1.PropRegionClass.GroupName(UUID)

        For Each RegionUUID In Form1.PropRegionClass.RegionUUIDListByName(GroupName)

            If (e.NewValue = CheckState.Unchecked) Then
                Form1.PropRegionClass.RegionEnabled(RegionUUID) = False
            Else
                Form1.PropRegionClass.RegionEnabled(RegionUUID) = True
            End If
            Settings.LoadIni(Form1.PropRegionClass.RegionPath(RegionUUID), ";")
            Settings.SetIni(Form1.PropRegionClass.RegionName(RegionUUID), "Enabled", Form1.PropRegionClass.RegionEnabled(RegionUUID))
            Settings.SaveINI(System.Text.Encoding.UTF8)
        Next
        ShowTitle()
        PropUpdateView() = True

    End Sub

    <CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId:="Outworldz.Form1.Log(System.String,System.String)")>
    <CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId:="System.Windows.Forms.ListViewItem+ListViewSubItemCollection.Add(System.String)")>
    Private Sub ShowAvatars()
        Try
            ViewBusy = True
            AvatarView.Show()
            ListView1.Hide()

            AvatarView.BeginUpdate()
            AvatarView.Items.Clear()

            Dim Index = 0

            ' Create items and subitems for each item.
            Dim L As New Dictionary(Of String, String)

            If MysqlInterface.IsMySqlRunning() Then
                L = MysqlInterface.GetAgentList()
            End If

            ' If Debugger.IsAttached Then
            'L.Add("Ferd Frederix", "Welcome")
            'End If

            For Each Agent In L
                Dim item1 As New ListViewItem(Agent.Key, Index)
                item1.SubItems.Add(Agent.Value)
                item1.SubItems.Add(My.Resources.Local)
                AvatarView.Items.AddRange(New ListViewItem() {item1})
                Index += 1
            Next

            If L.Count = 0 Then
                Dim item1 As New ListViewItem(My.Resources.No_Avatars, Index)
                item1.SubItems.Add("-".ToUpperInvariant)
                item1.SubItems.Add(My.Resources.Local_Grid)
                AvatarView.Items.AddRange(New ListViewItem() {item1})
                Index += 1
            End If

            ' Hypergrid
            '
            ' Create items and subitems for each item.
            Dim M As New Dictionary(Of String, String)
            If MysqlInterface.IsMySqlRunning() Then
                M = GetHGAgentList()
            End If

            ' If Debugger.IsAttached Then
            ' M.Add("Nyira Machabelli", "SandBox")
            'End If

            For Each Agent In M
                If Agent.Value.Length > 0 Then
                    Dim item1 As New ListViewItem(Agent.Key, Index)
                    item1.SubItems.Add(Agent.Value)
                    item1.SubItems.Add(My.Resources.Hypergrid_word)
                    AvatarView.Items.AddRange(New ListViewItem() {item1})
                    Index += 1
                End If
            Next

            If Index = 0 Then
                Dim item1 As New ListViewItem(My.Resources.No_Avatars, Index)
                item1.SubItems.Add("-".ToUpperInvariant)
                item1.SubItems.Add(My.Resources.Hypergrid_word)
                AvatarView.Items.AddRange(New ListViewItem() {item1})
            End If

            Me.AvatarView.TabIndex = 0
            AvatarView.EndUpdate()

            AvatarView.Show()
            AvatarView.Visible = True
            ViewBusy = False
            PropUpdateView() = False
        Catch ex As Exception

            BreakPoint.Show(ex.Message)
            Form1.Log(My.Resources.Error_word, " RegionList " & ex.Message)
        End Try

    End Sub

#End Region

#Region "Private Methods"

    Private Shared Function LoadImage(S As String) As Image

        Dim bmp As Bitmap = Nothing
        Dim u As New Uri(S)
        Dim request As System.Net.WebRequest = Net.WebRequest.Create(u)
        Dim response As System.Net.WebResponse = Nothing
        Try
            response = request.GetResponse()
        Catch ex As Exception

            BreakPoint.Show(ex.Message)
        End Try

        Dim responseStream As System.IO.Stream = Nothing
        Try
            responseStream = response.GetResponseStream()
        Catch ex As Exception

            BreakPoint.Show(ex.Message)
        End Try

        If responseStream IsNot Nothing Then
            bmp = New Bitmap(responseStream)
            responseStream.Dispose()
        End If

        Return bmp

    End Function

    Private Shared Sub StartStopEdit(RegionUUID As String, RegionName As String)

        Dim Choices As New FormRegionPopup
        Dim chosen As String = ""
        Choices.Init(RegionName)
        Choices.BringToFront()
        Choices.ShowDialog()
        Application.DoEvents()
        ' Read the chosen sim name
        chosen = Choices.Choice()
        Choices.Dispose()

        If chosen = "Start" Then

            Form1.Buttons(Form1.BusyButton)

            If Not Form1.StartMySQL() Then
                Form1.Print(My.Resources.Stopped_word)
            End If
            Form1.StartRobust()
            Form1.Log("Starting", Form1.PropRegionClass.RegionName(RegionUUID))

            Form1.PropAborting = False

            Form1.Boot(Form1.PropRegionClass, Form1.PropRegionClass.RegionName(RegionUUID))
            Application.DoEvents()
            Form1.Timer1.Interval = 1000
            Form1.Timer1.Start() 'Timer starts functioning
            Form1.Buttons(Form1.StopButton)
            Form1.PropOpensimIsRunning() = True
            Form1.ToolBar(True)

        ElseIf chosen = "Stop" Then

            ' if any avatars in any region, give them a choice.
            Dim StopIt As Boolean = True
            For Each num In Form1.PropRegionClass.RegionUUIDListByName(Form1.PropRegionClass.GroupName(RegionUUID))
                ' Ask before killing any people
                If Form1.PropRegionClass.AvatarCount(num) > 0 Then
                    Dim response As MsgBoxResult
                    If Form1.PropRegionClass.AvatarCount(num) = 1 Then
                        response = MsgBox(My.Resources.OneAvatar & " " & Form1.PropRegionClass.RegionName(num) & " " & Global.Outworldz.My.Resources.Do_you_still_want_to_Stop_word, vbYesNo)
                    Else
                        response = MsgBox(Form1.PropRegionClass.AvatarCount(num).ToString(Globalization.CultureInfo.InvariantCulture) + " " & Global.Outworldz.My.Resources.people_are_in & " " + Form1.PropRegionClass.RegionName(num) + ". " & Global.Outworldz.My.Resources.Do_you_still_want_to_Stop_word, vbYesNo)
                    End If
                    If response = vbNo Then
                        StopIt = False
                    End If
                End If
            Next

            If (StopIt) Then

                Dim hwnd As IntPtr = Form1.GetHwnd(Form1.PropRegionClass.GroupName(RegionUUID))
                If Form1.ShowDOSWindow(hwnd, Form1.SHOWWINDOWENUM.SWRESTORE) Then
                    Form1.SequentialPause()

                    ' shut down all regions in the DOS box
                    For Each RegionUUID In Form1.PropRegionClass.RegionUUIDListByName(Form1.PropRegionClass.GroupName(RegionUUID))
                        Form1.PropRegionClass.Timer(RegionUUID) = RegionMaker.REGIONTIMER.Stopped
                        Form1.PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.ShuttingDown ' request a Stop
                    Next

                    Form1.Print(My.Resources.Not_Running & " " & Global.Outworldz.My.Resources.Stopping_word)
                    Form1.ConsoleCommand(RegionUUID, "q{ENTER}" + vbCrLf)
                Else
                    ' shut down all regions in the DOS box
                    For Each UUID As String In Form1.PropRegionClass.RegionUUIDListByName(Form1.PropRegionClass.GroupName(RegionUUID))
                        Form1.PropRegionClass.Timer(UUID) = RegionMaker.REGIONTIMER.Stopped
                        Form1.PropRegionClass.Status(UUID) = RegionMaker.SIMSTATUSENUM.Stopped ' already shutting down
                    Next
                End If

                PropUpdateView = True ' make form refresh
            End If

        ElseIf chosen = "Console" Then
            Dim PID = Form1.PropRegionClass.ProcessID(RegionUUID)
            If PID > 0 Then
                Dim hwnd = Form1.GetHwnd(Form1.PropRegionClass.GroupName(RegionUUID))

                Dim tmp As String = Settings.ConsoleShow
                'temp show console
                Settings.ConsoleShow = "True"
                Form1.ShowDOSWindow(hwnd, Form1.SHOWWINDOWENUM.SWRESTORE)
                Settings.ConsoleShow = tmp

            End If

        ElseIf chosen = "Edit" Then

            Dim RegionForm As New FormRegion
            RegionForm.BringToFront()
            RegionForm.Init(RegionName)
            RegionForm.Activate()
            RegionForm.Visible = True
            RegionForm.Select()

        ElseIf chosen = "Recycle" Then

            Form1.SequentialPause()

            ' shut down all regions in the DOS box
            Dim GroupName = Form1.PropRegionClass.GroupName(RegionUUID)
            Form1.Logger("RecyclingDown", GroupName, "Restart")
            For Each UUID In Form1.PropRegionClass.RegionUUIDListByName(GroupName)
                Form1.PropRegionClass.Timer(UUID) = RegionMaker.REGIONTIMER.Stopped
                Form1.PropRegionClass.Status(UUID) = RegionMaker.SIMSTATUSENUM.RecyclingDown ' request a recycle.
                Form1.Logger("RecyclingDown", Form1.PropRegionClass.RegionName(UUID), "Restart")
            Next

            Form1.Print(My.Resources.Recycle1 & "  " + Form1.PropRegionClass.GroupName(RegionUUID))
            Form1.ConsoleCommand(RegionUUID, "q{ENTER}" + vbCrLf)
            PropUpdateView = True ' make form refresh

        ElseIf chosen = "Teleport" Then
            'secondlife://http|!!hg.osgrid.org|80+Lbsa+Plaza

            Dim link = "secondlife://http|!!" & Settings.PublicIP & "|" & Settings.HttpPort & "+" & RegionName
            Try
                System.Diagnostics.Process.Start(link)
#Disable Warning CA103
            Catch ex As Exception

                BreakPoint.Show(ex.Message)
            End Try

        End If

    End Sub

#End Region

#Region "Clicks"

    Private Shared Function PickGroup() As String

        Dim Chooseform As New Choice ' form for choosing a set of regions
        ' Show testDialog as a modal dialog and determine if DialogResult = OK.

        Chooseform.FillGrid("Group")
        Chooseform.BringToFront()
        Dim chosen As String
        Chooseform.ShowDialog()
        Try
            ' Read the chosen GROUP name
            chosen = Chooseform.DataGridView.CurrentCell.Value.ToString()
        Catch ex As Exception

            BreakPoint.Show(ex.Message)
            chosen = ""
        End Try

        Chooseform.Dispose()

        Return chosen

    End Function

    Private Sub AllNone_CheckedChanged(sender As Object, e As EventArgs) Handles AllNone.CheckedChanged

        If Not initted Then Return

        For Each X As ListViewItem In ListView1.Items
            Dim RegionUUID As String
            If ItemsAreChecked1 Then
                X.Checked = CType(CheckState.Checked, Boolean)
            Else
                X.Checked = CType(CheckState.Unchecked, Boolean)
            End If
            Dim name = X.Text
            If name.Length > 0 Then
                'Dim name = X.SubItems(1).Text
                RegionUUID = Form1.PropRegionClass.FindRegionByName(name)
                Form1.PropRegionClass.RegionEnabled(RegionUUID) = X.Checked
                Settings.LoadIni(Form1.PropRegionClass.RegionPath(RegionUUID), ";")
                Settings.SetIni(Form1.PropRegionClass.RegionName(RegionUUID), "Enabled", X.Checked)
                Settings.SaveINI(System.Text.Encoding.UTF8)
            End If

        Next

        ShowTitle()

        If ItemsAreChecked1 Then
            ItemsAreChecked1 = False
        Else
            ItemsAreChecked1 = True
        End If
        PropUpdateView = True ' make form refresh

    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles RestartButton.Click

        Form1.PropOpensimIsRunning() = True
        Form1.StartMySQL()
        Form1.StartRobust()
        Form1.Timer1.Interval = 1000
        Form1.Timer1.Start() 'Timer starts functioning
        Form1.TimerBusy = 0

        For Each RegionUUID As String In Form1.PropRegionClass.RegionUUIDs
            Dim GroupName = Form1.PropRegionClass.GroupName(RegionUUID)
            Dim Status = Form1.PropRegionClass.Status(RegionUUID)
            If Form1.PropRegionClass.RegionEnabled(RegionUUID) And
                Not Form1.AvatarsIsInGroup(GroupName) And
                Not Form1.PropAborting And
                (Status = RegionMaker.SIMSTATUSENUM.Booting _
                Or Status = RegionMaker.SIMSTATUSENUM.Booted _
                Or Status = RegionMaker.SIMSTATUSENUM.Stopped) Then

                Dim hwnd = Form1.GetHwnd(GroupName)
                Form1.ShowDOSWindow(hwnd, Form1.SHOWWINDOWENUM.SWRESTORE)
                Form1.SequentialPause()
                Form1.Print(My.Resources.Not_Running & " " & Global.Outworldz.My.Resources.Restarting_word)
                Form1.ConsoleCommand(RegionUUID, "q{ENTER}" + vbCrLf)

                If Status = RegionMaker.SIMSTATUSENUM.Stopped Then
                    For Each UUID As String In Form1.PropRegionClass.RegionUUIDListByName(GroupName)
                        Form1.PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.RestartPending
                    Next
                Else
                    ' shut down all regions in the DOS box
                    For Each UUID As String In Form1.PropRegionClass.RegionUUIDListByName(GroupName)
                        Form1.PropRegionClass.Timer(UUID) = RegionMaker.REGIONTIMER.Stopped
                        Form1.PropRegionClass.Status(UUID) = RegionMaker.SIMSTATUSENUM.RecyclingDown
                    Next
                End If

                PropUpdateView = True ' make form refresh
            End If
        Next

    End Sub

    Private Sub HelpToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HelpToolStripMenuItem.Click
        Form1.Help("RegionList")
    End Sub

    Private Sub RunAllButton_Click(sender As Object, e As EventArgs) Handles RunAllButton.Click

        Form1.Startup()

    End Sub

    Private Sub StopAllButton_Click(sender As Object, e As EventArgs) Handles StopAllButton.Click

        Form1.DoStopActions()

    End Sub

    Private Sub ViewAvatars_Click(sender As Object, e As EventArgs) Handles AvatarsButton.Click

        Settings.RegionListView() = ViewType.Avatars
        Settings.SaveSettings()
        TheView1 = ViewType.Avatars
        ListView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.None)
        SetScreen()
        ListView1.View = View.Details
        ListView1.Hide()
        AvatarView.Show()
        LoadMyListView()
        Timer1.Start()

    End Sub

    Private Sub ViewCompact_Click(sender As Object, e As EventArgs) Handles IconsButton.Click

        Settings.RegionListView() = ViewType.Icons
        Settings.SaveSettings()
        TheView1 = ViewType.Icons
        ListView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)
        SetScreen()
        ListView1.View = View.SmallIcon
        ListView1.Show()
        AvatarView.Hide()
        ListView1.CheckBoxes = False
        Timer1.Start()
        LoadMyListView()

    End Sub

    Private Sub ViewDetail_Click(sender As Object, e As EventArgs) Handles DetailsButton.Click

        Settings.RegionListView() = ViewType.Details
        Settings.SaveSettings()
        TheView1 = ViewType.Details
        ListView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.None)
        SetScreen()
        ListView1.View = View.Details
        ListView1.Show()
        AvatarView.Hide()
        ListView1.CheckBoxes = True
        Timer1.Start()
        LoadMyListView()

    End Sub

    Private Sub ViewMaps_Click(sender As Object, e As EventArgs) Handles MapsButton.Click

        Settings.RegionListView() = ViewType.Maps
        Settings.SaveSettings()
        TheView1 = ViewType.Maps
        ListView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.None)
        SetScreen()
        ListView1.View = View.LargeIcon
        ListView1.Show()
        AvatarView.Hide()
        ListView1.CheckBoxes = False
        Timer1.Stop()
        LoadMyListView()

    End Sub

#End Region

#Region "Mysql"

    Private Shared Function GetRegionsName(Region As String) As String

        Dim p1 As String = ""
        Using reader = New StreamReader(Region)
            While reader.Peek <> -1 And p1.Length = 0
                Dim line = reader.ReadLine
                Dim pattern1 As Regex = New Regex("^ *\[(.*?)\] *$")
                Dim match1 As Match = pattern1.Match(line)
                If match1.Success Then
                    p1 = match1.Groups(1).Value
                End If
            End While
        End Using
        Return p1

    End Function

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles ImportButton.Click

        Dim ofd As New OpenFileDialog With {
            .InitialDirectory = "\",
            .Filter = Global.Outworldz.My.Resources.INI_Filter,
            .FilterIndex = 2,
            .RestoreDirectory = True,
            .Multiselect = True
        }

        If ofd.ShowDialog = DialogResult.OK Then
            If ofd.CheckFileExists Then

                Dim dirpathname = PickGroup()
                If dirpathname.Length = 0 Then
                    Form1.Print(My.Resources.Aborted_word)
                    ofd.Dispose()
                    Return
                End If

                If dirpathname = "! Add New Name" Then
                    dirpathname = InputBox(My.Resources.Enter_Dos_Name)
                End If
                If dirpathname.Length = 0 Then
                    ofd.Dispose()
                    Return
                End If

                For Each ofdFilename As String In ofd.FileNames

                    Dim noquotes As Regex = New Regex("'")
                    dirpathname = noquotes.Replace(dirpathname, "")

                    Dim extension As String = Path.GetExtension(ofdFilename)
                    extension = Mid(extension, 2, 5)
                    If extension.ToUpper(Globalization.CultureInfo.InvariantCulture) = "INI" Then

                        Dim filename = GetRegionsName(ofdFilename)
                        Dim RegionUUID As String = Form1.PropRegionClass.FindRegionByName(filename)

                        If RegionUUID.Length > 0 Then
                            MsgBox(My.Resources.Region_Already_Exists, vbInformation, Global.Outworldz.My.Resources.Info_word)
                            ofd.Dispose()
                            Return
                        End If

                        If dirpathname.Length = 0 Then dirpathname = filename

                        Dim NewFilepath = Settings.OpensimBinPath & "Regions\" + dirpathname + "\Region\"
                        If Not Directory.Exists(NewFilepath) Then
                            Try
                                Directory.CreateDirectory(Settings.OpensimBinPath & "Regions\" + dirpathname + "\Region")
                            Catch ex As Exception
                                BreakPoint.Show(ex.Message)
                            End Try
                        End If
                        File.Copy(ofdFilename, Settings.OpensimBinPath & "Regions\" + dirpathname + "\Region\" + filename + ".ini")
                    Else
                        Form1.Print(My.Resources.Unrecognized & " " & extension & ". ")
                    End If
                Next

                Form1.PropRegionClass.GetAllRegions()
                LoadMyListView()
            End If
        End If
        ofd.Dispose()

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles RestartRobustButton.Click

        Form1.PropRestartRobust = True
        Form1.StopRobust()

    End Sub

    Private Sub Button3_Click_1(sender As Object, e As EventArgs) Handles StatsButton.Click

        Dim regionname = Form1.ChooseRegion(True)
        If regionname.Length = 0 Then Return
        Dim RegionUUID As String = Form1.PropRegionClass.FindRegionByName(regionname)
        Dim RegionPort = Form1.PropRegionClass.GroupPort(RegionUUID)
        Dim webAddress As String = "http://" & Settings.PublicIP & ":" & CType(RegionPort, String) & "/SStats/"
        Try
            Process.Start(webAddress)
        Catch ex As Exception
            BreakPoint.Show(ex.Message)

        End Try

    End Sub

#End Region

End Class

#Region "Compare"

' Implements the manual sorting of items by columns.
Class ListViewItemComparer
    Implements IComparer
#Disable Warning IDE0044 ' Add readonly modifier

#Region "Private Fields"

    Private col As Integer

#End Region

#Enable Warning IDE0044 ' Add readonly modifier

#Region "Public Constructors"

    Public Sub New()
        col = 1
    End Sub

    Public Sub New(ByVal column As Integer)
        col = column
    End Sub

#End Region

#Region "Public Methods"

    Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare

        Dim a = CType(x, ListViewItem).SubItems(col).Text
        Dim b = CType(y, ListViewItem).SubItems(col).Text

        Return [String].Compare(a, b, StringComparison.InvariantCultureIgnoreCase)

    End Function

#End Region

End Class

#End Region
