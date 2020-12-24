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

Public Class FormRegionlist

#Region "Declarations"

    Private Shared _FormExists As Boolean
    Private ReadOnly colsize As New ScreenPos("Region List")
    Private _ImageListLarge As ImageList
#Disable Warning CA2213 ' Disposable fields should be disposed
    Private _ImageListSmall As New ImageList
#Enable Warning CA2213 ' Disposable fields should be disposed
    Private initted As Boolean
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
            Return FormExists1
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
            Return FormSetup.PropUpdateView
        End Get
        Set(ByVal Value As Boolean)
            FormSetup.PropUpdateView = Value
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
                Me.Width = 1800
            Else
                Me.Width = hw.Item(1)
            End If
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
            Me.Height = 460
            Me.Width = 1800
            Me.Left = 100
            Me.Top = 100
        End Try

    End Sub

#End Region

#Region "Layout"

    Private Sub ListView1_ColumnWidthChanged(sender As Object, e As ColumnWidthChangedEventArgs) Handles ListView1.ColumnWidthChanged

        Dim w = ListView1.Columns(e.ColumnIndex).Width
        Dim name = ListView1.Columns(e.ColumnIndex).Name
        If name.Length = 0 Or w = 0 Then Return
        If TheView1 = ViewType.Icons Then Return
        ScreenPosition.PutSize(name, w)
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
        ErrorIcon = 15
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

    Private Sub LoadForm(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        AddRegionButton.Text = Global.Outworldz.My.Resources.Resources.Add_word
        AllNone.Text = Global.Outworldz.My.Resources.Resources.AllNone_word
        AvatarsButton.Text = Global.Outworldz.My.Resources.Resources.Avatars_word
        DetailsButton.Text = Global.Outworldz.My.Resources.Resources.Details_word
        HelpToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.question_and_answer
        HelpToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Help_word
        HelpToolStripMenuItem1.Image = Global.Outworldz.My.Resources.Resources.about
        HelpToolStripMenuItem1.Text = Global.Outworldz.My.Resources.Resources.Help_word
        IconsButton.Text = Global.Outworldz.My.Resources.Resources.Icons_word
        ImportButton.Text = Global.Outworldz.My.Resources.Resources.Import_word
        MapsButton.Text = Global.Outworldz.My.Resources.Resources.Maps_word
        RefreshButton.Text = Global.Outworldz.My.Resources.Resources.Refresh_word
        RestartButton.Text = Global.Outworldz.My.Resources.Resources.Restart_All_word
        RunAllButton.Text = Global.Outworldz.My.Resources.Resources.Run_All_word
        StopAllButton.Text = Global.Outworldz.My.Resources.Resources.Stop_All_word
        ToolTip1.SetToolTip(AddRegionButton, Global.Outworldz.My.Resources.Resources.Add_Region_word)
        ToolTip1.SetToolTip(AllNone, Global.Outworldz.My.Resources.Resources.Selectallnone)
        ToolTip1.SetToolTip(AvatarsButton, Global.Outworldz.My.Resources.Resources.ListAvatars)
        ToolTip1.SetToolTip(DetailsButton, Global.Outworldz.My.Resources.Resources.View_Details)
        ToolTip1.SetToolTip(IconsButton, Global.Outworldz.My.Resources.Resources.View_as_Icons)
        ToolTip1.SetToolTip(ImportButton, Global.Outworldz.My.Resources.Resources.Importtext)
        ToolTip1.SetToolTip(ListView1, Global.Outworldz.My.Resources.Resources.ClickStartStoptxt)
        ToolTip1.SetToolTip(MapsButton, Global.Outworldz.My.Resources.Resources.View_Maps)
        ToolTip1.SetToolTip(RefreshButton, Global.Outworldz.My.Resources.Resources.Reload)
        ToolTip1.SetToolTip(RestartButton, Global.Outworldz.My.Resources.Resources.Restart_All_Checked)
        ToolTip1.SetToolTip(RunAllButton, Global.Outworldz.My.Resources.Resources.StartAll)
        ToolTip1.SetToolTip(StopAllButton, Global.Outworldz.My.Resources.Resources.Stopsall)
        ToolTip1.ToolTipTitle = Global.Outworldz.My.Resources.Resources.Row

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

        Dim ctr = 0

        ListView1.Columns.Add(My.Resources.Enable_word, colsize.ColumnWidth("Column" & ctr & "_" & CStr(TheView), 120), HorizontalAlignment.Left)
        ListView1.Columns(ctr).Name = "Column" & ctr & "_" & CStr(TheView)
        ctr += 1
        ListView1.Columns.Add(My.Resources.DOS_Box_word, colsize.ColumnWidth("Column" & ctr & "_" & CStr(TheView), 120), HorizontalAlignment.Left)
        ListView1.Columns(ctr).Name = "Column" & ctr & "_" & CStr(TheView)
        ctr += 1
        ListView1.Columns.Add(My.Resources.Agents_word, colsize.ColumnWidth("Column" & ctr & "_" & CStr(TheView), 50), HorizontalAlignment.Center)
        ListView1.Columns(ctr).Name = "Column" & ctr & "_" & CStr(TheView)
        ctr += 1
        ListView1.Columns.Add(My.Resources.Status_word, colsize.ColumnWidth("Column" & ctr & "_" & CStr(TheView), 120), HorizontalAlignment.Center)
        ListView1.Columns(ctr).Name = "Column" & ctr & "_" & CStr(TheView)
        ctr += 1
        ListView1.Columns.Add(My.Resources.RAM_Word, colsize.ColumnWidth("Column" & ctr & "_" & CStr(TheView), 80), HorizontalAlignment.Center)
        ListView1.Columns(ctr).Name = "Column" & ctr & "_" & CStr(TheView)
        ctr += 1
        ListView1.Columns.Add(My.Resources.CPU_word, colsize.ColumnWidth("Column" & ctr & "_" & CStr(TheView), 60), HorizontalAlignment.Center)
        ListView1.Columns(ctr).Name = "Column" & ctr & "_" & CStr(TheView)
        ctr += 1
        ListView1.Columns.Add("X".ToUpperInvariant, colsize.ColumnWidth("Column" & ctr & "_" & CStr(TheView), 50), HorizontalAlignment.Center)
        ListView1.Columns(ctr).Name = "Column" & ctr & "_" & CStr(TheView)
        ctr += 1
        ListView1.Columns.Add("Y".ToUpperInvariant, colsize.ColumnWidth("Column" & ctr & "_" & CStr(TheView), 50), HorizontalAlignment.Center)
        ListView1.Columns(ctr).Name = "Column" & ctr & "_" & CStr(TheView)
        ctr += 1
        ListView1.Columns.Add(My.Resources.Size_word, colsize.ColumnWidth("Column" & ctr & "_" & CStr(TheView), 40), HorizontalAlignment.Center)
        ListView1.Columns(ctr).Name = "Column" & ctr & "_" & CStr(TheView)
        ctr += 1
        ListView1.Columns.Add(My.Resources.Estate_word, colsize.ColumnWidth("Column" & ctr & "_" & CStr(TheView), 100), HorizontalAlignment.Left)
        ListView1.Columns(ctr).Name = "Column" & ctr & "_" & CStr(TheView)
        ctr += 1
        ListView1.Columns.Add(My.Resources.Region_Ports_word, colsize.ColumnWidth("Column" & ctr & "_" & CStr(TheView), 50), HorizontalAlignment.Center)
        ListView1.Columns(ctr).Name = "Column" & ctr & "_" & CStr(TheView)
        ctr += 1
        ListView1.Columns.Add(My.Resources.XMLRPC, colsize.ColumnWidth("Column" & ctr & "_" & CStr(TheView), 50), HorizontalAlignment.Center)
        ListView1.Columns(ctr).Name = "Column" & ctr & "_" & CStr(TheView)
        ctr += 1
        ListView1.Columns.Add(My.Resources.Remote_Admin_word, colsize.ColumnWidth("Column" & ctr & "_" & CStr(TheView), 50), HorizontalAlignment.Center)
        ListView1.Columns(ctr).Name = "Column" & ctr & "_" & CStr(TheView)
        ctr += 1

        ' optional
        ListView1.Columns.Add(My.Resources.Scripts_word, colsize.ColumnWidth("Column" & ctr & "_" & CStr(TheView), 80), HorizontalAlignment.Center)
        ListView1.Columns(ctr).Name = "Column" & ctr & "_" & CStr(TheView)
        ctr += 1
        ListView1.Columns.Add(My.Resources.Maps_word, colsize.ColumnWidth("Column" & ctr & "_" & CStr(TheView), 80), HorizontalAlignment.Center)
        ListView1.Columns(ctr).Name = "Column" & ctr & "_" & CStr(TheView)
        ctr += 1
        ListView1.Columns.Add(My.Resources.Physics_word, colsize.ColumnWidth("Column" & ctr & "_" & CStr(TheView), 120), HorizontalAlignment.Center)
        ListView1.Columns(ctr).Name = "Column" & ctr & "_" & CStr(TheView)
        ctr += 1
        ListView1.Columns.Add(My.Resources.Birds_word, colsize.ColumnWidth("Column" & ctr & "_" & CStr(TheView) & "_" & CStr(TheView), 60), HorizontalAlignment.Center)
        ListView1.Columns(ctr).Name = "Column" & ctr & "_" & CStr(TheView)
        ctr += 1
        ListView1.Columns.Add(My.Resources.Tides_word, colsize.ColumnWidth("Column" & ctr & "_" & CStr(TheView), 60), HorizontalAlignment.Center)
        ListView1.Columns(ctr).Name = "Column" & ctr & "_" & CStr(TheView)
        ctr += 1
        ListView1.Columns.Add(My.Resources.Teleport_word, colsize.ColumnWidth("Column" & ctr, 65), HorizontalAlignment.Center)
        ListView1.Columns(ctr).Name = "Column" & ctr & "_" & CStr(TheView)
        ctr += 1
        ListView1.Columns.Add(My.Resources.Smart_Start_word, colsize.ColumnWidth("Column" & ctr & "_" & CStr(TheView), 80), HorizontalAlignment.Center)
        ListView1.Columns(ctr).Name = "Column" & ctr & "_" & CStr(TheView)
        ctr += 1
        ListView1.Columns.Add(My.Resources.Allow_Or_Disallow_Gods_word, colsize.ColumnWidth("Column" & ctr, 75), HorizontalAlignment.Center)
        ListView1.Columns(ctr).Name = "Column" & ctr & "_" & CStr(TheView)
        ctr += 1
        ListView1.Columns.Add(My.Resources.Owner_God, colsize.ColumnWidth("Column" & ctr & "_" & CStr(TheView), 75), HorizontalAlignment.Center)
        ListView1.Columns(ctr).Name = "Column" & ctr & "_" & CStr(TheView)
        ctr += 1
        ListView1.Columns.Add(My.Resources.Manager_God_word, colsize.ColumnWidth("Column" & ctr & "_" & CStr(TheView), 80), HorizontalAlignment.Center)
        ListView1.Columns(ctr).Name = "Column" & ctr & "_" & CStr(TheView)
        ctr += 1
        ListView1.Columns.Add(My.Resources.No_Autobackup, colsize.ColumnWidth("Column" & ctr & "_" & CStr(TheView), 90), HorizontalAlignment.Center)
        ListView1.Columns(ctr).Name = "Column" & ctr & "_" & CStr(TheView)
        ctr += 1
        ListView1.Columns.Add(My.Resources.Publicity_Word, colsize.ColumnWidth("Column" & ctr & "_" & CStr(TheView), 80), HorizontalAlignment.Center)
        ListView1.Columns(ctr).Name = "Column" & ctr & "_" & CStr(TheView)
        ctr += 1
        ListView1.Columns.Add(My.Resources.Script_Rate_word, colsize.ColumnWidth("Column" & ctr & "_" & CStr(TheView), 80), HorizontalAlignment.Center)
        ListView1.Columns(ctr).Name = "Column" & ctr & "_" & CStr(TheView)
        ctr += 1
        ListView1.Columns.Add(My.Resources.Frame_Rate_word, colsize.ColumnWidth("Column" & ctr & "_" & CStr(TheView), 80), HorizontalAlignment.Center)
        ListView1.Columns(ctr).Name = "Column" & ctr & "_" & CStr(TheView)
        ctr += 1

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
        ImageListSmall1.Images.Add(My.Resources.ResourceManager.GetObject("replace2", Globalization.CultureInfo.InvariantCulture))  '  13- Pending
        ImageListSmall1.Images.Add(My.Resources.ResourceManager.GetObject("media_pause", Globalization.CultureInfo.InvariantCulture))  '  14- Suspended
        ImageListSmall1.Images.Add(My.Resources.ResourceManager.GetObject("error_icon", Globalization.CultureInfo.InvariantCulture))  '  15- Error
        FormSetup.PropUpdateView = True ' make form refresh

        ViewBusy = False
        Timer1.Interval = 250 ' check for Form1.PropUpdateView every second
        Timer1.Start() 'Timer starts functioning

        ShowTitle()
        initted = True

    End Sub

    Private Sub MyListView_AfterLabelEdit(sender As Object, e As System.Windows.Forms.LabelEditEventArgs) Handles ListView1.AfterLabelEdit

        Debug.Print(e.Label)

    End Sub

    Private Sub ShowTitle()

        Dim TotalSize As Double
        Dim RegionCount As Integer
        Dim TotalRegionCount As Integer
        For Each RegionUUID As String In FormSetup.PropRegionClass.RegionUuids
            TotalSize += FormSetup.PropRegionClass.SizeX(RegionUUID) / 256 * FormSetup.PropRegionClass.SizeY(RegionUUID) / 256
            If FormSetup.PropRegionClass.RegionEnabled(RegionUUID) Then RegionCount += 1
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

    Private Sub ShowRegions()

        Try
            If ViewBusy = True Then
                Return
            End If

            ViewBusy = True
            ListView1.BeginUpdate()

            'CalcCPU()

            ImageListLarge1 = New ImageList()

            If Pixels1 = 0 Then Pixels1 = 24
            ImageListLarge1.ImageSize = New Size(Pixels1, Pixels1)

            ListView1.Items.Clear()
            ImageListSmall1.ImageSize = New Drawing.Size(20, 20)
            Dim p As PerformanceCounter = Nothing

            Try
                For Each RegionUUID As String In FormSetup.PropRegionClass.RegionUuids

                    Dim Num As Integer = 0
                    Dim Groupname As String = FormSetup.PropRegionClass.GroupName(RegionUUID)
                    Dim Status = FormSetup.PropRegionClass.Status(RegionUUID)

                    Dim Letter As String = ""
                    If Status = RegionMaker.SIMSTATUSENUM.Stopped _
                        And FormSetup.PropRegionClass.SmartStart(RegionUUID) = "True" Then
                        Letter = "Waiting"
                        Num = DGICON.SmartStart
                    ElseIf Status = RegionMaker.SIMSTATUSENUM.Error Then
                        Letter = "Error"
                        Num = DGICON.ErrorIcon
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
                    ElseIf Status = RegionMaker.SIMSTATUSENUM.Booted And FormSetup.PropRegionClass.AvatarCount(RegionUUID) = 1 Then
                        Letter = "Running"
                        Num = DGICON.user1
                    ElseIf Status = RegionMaker.SIMSTATUSENUM.Booted And FormSetup.PropRegionClass.AvatarCount(RegionUUID) > 1 Then
                        Letter = "Running"
                        Num = DGICON.user2
                    ElseIf Status = RegionMaker.SIMSTATUSENUM.Booted Then
                        If FormSetup.PropRegionClass.RegionName(RegionUUID) = Settings.WelcomeRegion Then
                            Num = DGICON.Home
                            Letter = "Running"
                        Else
                            Letter = "Running"
                            Num = DGICON.up
                        End If
                    ElseIf Not FormSetup.PropRegionClass.RegionEnabled(RegionUUID) Then
                        Letter = "Disabled"
                        If FormSetup.PropRegionClass.RegionName(RegionUUID) = Settings.WelcomeRegion Then
                            Num = DGICON.HomeOffline
                        Else
                            Num = DGICON.disabled
                        End If
                    ElseIf FormSetup.PropRegionClass.RegionEnabled(RegionUUID) Then
                        Letter = "Stopped"
                        Num = DGICON.stopped
                    Else
                        Num = DGICON.warning ' warning
                    End If

                    ' Create items and sub items for each item. Place a check mark next to the item.
                    Dim item1 As New ListViewItem(FormSetup.PropRegionClass.RegionName(RegionUUID), Num) With
                        {
                            .Checked = FormSetup.PropRegionClass.RegionEnabled(RegionUUID)
                        }

                    item1.SubItems.Add(FormSetup.PropRegionClass.GroupName(RegionUUID).ToString(Globalization.CultureInfo.InvariantCulture))
                    item1.SubItems.Add(FormSetup.PropRegionClass.AvatarCount(RegionUUID).ToString(Globalization.CultureInfo.InvariantCulture))

                    item1.SubItems.Add(Letter)
                    Dim fmtXY = "00000" ' 65536
                    Dim fmtRam = "0.0" ' 9999 MB
                    ' RAM

                    If Status = RegionMaker.SIMSTATUSENUM.Booting _
                        Or Status = RegionMaker.SIMSTATUSENUM.Booted _
                        Or Status = RegionMaker.SIMSTATUSENUM.RecyclingUp _
                        Or Status = RegionMaker.SIMSTATUSENUM.RecyclingDown _
                        Then

                        Try
                            Dim PID = FormSetup.PropRegionClass.ProcessID(RegionUUID)
                            Dim component1 As Process = Process.GetProcessById(PID)
                            Dim Memory As Double = (component1.WorkingSet64 / 1024) / 1024
                            item1.SubItems.Add(Memory.ToString("0.0", Globalization.CultureInfo.InvariantCulture))
                        Catch ex As Exception
                            item1.SubItems.Add("0")
                        End Try
                    Else
                        item1.SubItems.Add("0")
                    End If

                    Dim cpupercent As Double = 0

                    If CPUValues.TryGetValue(Groupname, cpupercent) Then
                    Else
                        cpupercent = 0
                    End If
                    item1.SubItems.Add(CStr(cpupercent))

                    item1.SubItems.Add(FormSetup.PropRegionClass.CoordX(RegionUUID).ToString(fmtXY, Globalization.CultureInfo.InvariantCulture))
                    item1.SubItems.Add(FormSetup.PropRegionClass.CoordY(RegionUUID).ToString(fmtXY, Globalization.CultureInfo.InvariantCulture))

                    ' Size of region
                    Dim s As Double = FormSetup.PropRegionClass.SizeX(RegionUUID) / 256
                    Dim size As String = CStr(s) & "X" & CStr(s)
                    item1.SubItems.Add(size)

                    ' add estate name
                    Dim Estate = "-".ToUpperInvariant
                    If MysqlInterface.IsRunning() Then
                        Estate = MysqlInterface.EstateName(FormSetup.PropRegionClass.RegionUUID(RegionUUID))
                    End If
                    item1.SubItems.Add(Estate)

                    item1.SubItems.Add(FormSetup.PropRegionClass.RegionPort(RegionUUID).ToString(Globalization.CultureInfo.InvariantCulture))
                    item1.SubItems.Add(FormSetup.PropRegionClass.XmlRegionPort(RegionUUID).ToString(Globalization.CultureInfo.InvariantCulture))
                    item1.SubItems.Add(FormSetup.PropRegionClass.RemoteAdminPort(RegionUUID).ToString(Globalization.CultureInfo.InvariantCulture))

                    'Scripts XEngine or YEngine
                    Select Case FormSetup.PropRegionClass.ScriptEngine(RegionUUID)
                        Case "YEngine"
                            item1.SubItems.Add(My.Resources.YEngine_word)
                        Case "XEngine"
                            item1.SubItems.Add(My.Resources.XEngine_word)
                        Case Else
                            item1.SubItems.Add("-".ToUpperInvariant)
                    End Select

                    'Map
                    If FormSetup.PropRegionClass.MapType(RegionUUID).Length > 0 Then
                        item1.SubItems.Add(FormSetup.PropRegionClass.MapType(RegionUUID))
                    Else
                        item1.SubItems.Add("-".ToUpperInvariant)
                    End If

                    ' physics
                    Select Case FormSetup.PropRegionClass.Physics(RegionUUID)
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

                    If FormSetup.PropRegionClass.Birds(RegionUUID) = "True" Then
                        item1.SubItems.Add(My.Resources.Yes_word)
                    Else
                        item1.SubItems.Add("-".ToUpperInvariant)
                    End If

                    'Tides
                    If FormSetup.PropRegionClass.Tides(RegionUUID) = "True" Then
                        item1.SubItems.Add(My.Resources.Yes_word)
                    Else
                        item1.SubItems.Add("-".ToUpperInvariant)
                    End If

                    'teleport
                    If FormSetup.PropRegionClass.Teleport(RegionUUID) = "True" Then
                        item1.SubItems.Add(My.Resources.Yes_word)
                    Else
                        item1.SubItems.Add("-".ToUpperInvariant)
                    End If

                    If FormSetup.PropRegionClass.SmartStart(RegionUUID) = "True" Then
                        item1.SubItems.Add(My.Resources.Yes_word)
                    Else
                        item1.SubItems.Add("-".ToUpperInvariant)
                    End If

                    If FormSetup.PropRegionClass.AllowGods(RegionUUID).Length > 0 Then
                        item1.SubItems.Add(FormSetup.PropRegionClass.AllowGods(RegionUUID))
                    Else
                        item1.SubItems.Add("-".ToUpperInvariant)
                    End If

                    If FormSetup.PropRegionClass.RegionGod(RegionUUID).Length > 0 Then
                        item1.SubItems.Add(FormSetup.PropRegionClass.RegionGod(RegionUUID))
                    Else
                        item1.SubItems.Add("-".ToUpperInvariant)
                    End If

                    If FormSetup.PropRegionClass.ManagerGod(RegionUUID).Length > 0 Then
                        item1.SubItems.Add(FormSetup.PropRegionClass.ManagerGod(RegionUUID))
                    Else
                        item1.SubItems.Add("-".ToUpperInvariant)
                    End If

                    If FormSetup.PropRegionClass.SkipAutobackup(RegionUUID).Length > 0 Then
                        item1.SubItems.Add(FormSetup.PropRegionClass.SkipAutobackup(RegionUUID))
                    Else
                        item1.SubItems.Add("-".ToUpperInvariant)
                    End If

                    If FormSetup.PropRegionClass.RegionSnapShot(RegionUUID).Length > 0 Then
                        item1.SubItems.Add(FormSetup.PropRegionClass.RegionSnapShot(RegionUUID))
                    Else
                        item1.SubItems.Add("-".ToUpperInvariant)
                    End If

                    If FormSetup.PropRegionClass.MinTimerInterval(RegionUUID).Length > 0 Then
                        item1.SubItems.Add(FormSetup.PropRegionClass.MinTimerInterval(RegionUUID))
                    Else
                        item1.SubItems.Add("-".ToUpperInvariant)
                    End If

                    If FormSetup.PropRegionClass.FrameTime(RegionUUID).Length > 0 Then
                        item1.SubItems.Add(FormSetup.PropRegionClass.FrameTime(RegionUUID))
                    Else
                        item1.SubItems.Add("-".ToUpperInvariant)
                    End If

                    ' maps
                    If TheView1 = ViewType.Maps Then

                        If Status = RegionMaker.SIMSTATUSENUM.Booted Then
                            Dim img As String = "http://127.0.0.1:" + FormSetup.PropRegionClass.GroupPort(RegionUUID).ToString(Globalization.CultureInfo.InvariantCulture) + "/" + "index.php?method=regionImage" + FormSetup.PropRegionClass.RegionUUID(RegionUUID).Replace("-".ToUpperInvariant, "")
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
                        Using colsize As New ScreenPos(MyBase.Name & "ColumnSize")
                            Dim w = colsize.ColumnWidth(CStr(col.name))
                            If w > 0 Then col.Width = w
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
                FormSetup.Log(My.Resources.Error_word, " RegionList " & ex.Message)
            End Try
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
            FormSetup.Log(My.Resources.Error_word, " RegionList " & ex.Message)
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
            Dim RegionUUID As String = FormSetup.PropRegionClass.FindRegionByName(RegionName)
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

        FormSetup.PropRegionClass.GetAllRegions()
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

        HelpManual("RegionList")

    End Sub

    Private Sub ListClick(sender As Object, e As EventArgs) Handles ListView1.Click

        Dim regions As ListView.SelectedListViewItemCollection = Me.ListView1.SelectedItems
        Dim item As ListViewItem
        For Each item In regions
            Dim RegionName = item.SubItems(0).Text
            Dim RegionUUID As String = FormSetup.PropRegionClass.FindRegionByName(RegionName)
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

        Dim UUID As String = FormSetup.PropRegionClass.FindRegionByName(Item.Text)
        If UUID.Length = 0 Then Return
        Dim GroupName = FormSetup.PropRegionClass.GroupName(UUID)

        For Each RegionUUID In FormSetup.PropRegionClass.RegionUuidListByName(GroupName)

            If (e.NewValue = CheckState.Unchecked) Then
                FormSetup.PropRegionClass.RegionEnabled(RegionUUID) = False
            Else
                FormSetup.PropRegionClass.RegionEnabled(RegionUUID) = True
            End If
            Settings.LoadIni(FormSetup.PropRegionClass.RegionPath(RegionUUID), ";")
            Settings.SetIni(FormSetup.PropRegionClass.RegionName(RegionUUID), "Enabled", CStr(FormSetup.PropRegionClass.RegionEnabled(RegionUUID)))
            Settings.SaveINI(System.Text.Encoding.UTF8)
        Next
        ShowTitle()
        PropUpdateView() = True

    End Sub

    Private Sub ShowAvatars()
        Try
            ViewBusy = True
            AvatarView.Show()
            ListView1.Hide()

            AvatarView.BeginUpdate()
            AvatarView.Items.Clear()

            Dim Index = 0

            ' Create items and sub items for each item.
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
            FormSetup.Log(My.Resources.Error_word, " RegionList " & ex.Message)
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

            FormSetup.Buttons(FormSetup.BusyButton)

            If Not FormSetup.StartMySQL() Then
                FormSetup.Print(My.Resources.Stopped_word)
            End If
            FormSetup.StartRobust()
            FormSetup.Log("Starting", FormSetup.PropRegionClass.RegionName(RegionUUID))

            FormSetup.PropAborting = False
            FormSetup.PropRegionClass.CrashCounter(RegionUUID) = 0
            FormSetup.Boot(FormSetup.PropRegionClass.RegionName(RegionUUID))
            Application.DoEvents()
            FormSetup.Timer1.Interval = 1000
            FormSetup.Timer1.Start() 'Timer starts functioning
            FormSetup.Buttons(FormSetup.StopButton)
            FormSetup.PropOpensimIsRunning() = True
            FormSetup.ToolBar(True)

        ElseIf chosen = "Stop" Then

            ' if any avatars in any region, give them a choice.
            Dim StopIt As Boolean = True
            For Each num In FormSetup.PropRegionClass.RegionUuidListByName(FormSetup.PropRegionClass.GroupName(RegionUUID))
                ' Ask before killing any people
                If FormSetup.PropRegionClass.AvatarCount(num) > 0 Then
                    Dim response As MsgBoxResult
                    If FormSetup.PropRegionClass.AvatarCount(num) = 1 Then
                        response = MsgBox(My.Resources.OneAvatar & " " & FormSetup.PropRegionClass.RegionName(num) & " " & Global.Outworldz.My.Resources.Do_you_still_want_to_Stop_word, vbYesNo)
                    Else
                        response = MsgBox(FormSetup.PropRegionClass.AvatarCount(num).ToString(Globalization.CultureInfo.InvariantCulture) + " " & Global.Outworldz.My.Resources.people_are_in & " " + FormSetup.PropRegionClass.RegionName(num) + ". " & Global.Outworldz.My.Resources.Do_you_still_want_to_Stop_word, vbYesNo)
                    End If
                    If response = vbNo Then
                        StopIt = False
                    End If
                End If
            Next

            If (StopIt) Then

                Dim hwnd As IntPtr = FormSetup.GetHwnd(FormSetup.PropRegionClass.GroupName(RegionUUID))
                If FormSetup.ShowDOSWindow(hwnd, FormSetup.SHOWWINDOWENUM.SWRESTORE) Then
                    FormSetup.SequentialPause()

                    ' shut down all regions in the DOS box
                    For Each RegionUUID In FormSetup.PropRegionClass.RegionUuidListByName(FormSetup.PropRegionClass.GroupName(RegionUUID))
                        FormSetup.PropRegionClass.Timer(RegionUUID) = RegionMaker.REGIONTIMER.Stopped
                        FormSetup.PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.ShuttingDown ' request a Stop
                    Next

                    FormSetup.Print(My.Resources.Not_Running & " " & Global.Outworldz.My.Resources.Stopping_word)
                    FormSetup.ConsoleCommand(RegionUUID, "q{ENTER}" + vbCrLf)
                Else
                    ' shut down all regions in the DOS box
                    For Each UUID As String In FormSetup.PropRegionClass.RegionUuidListByName(FormSetup.PropRegionClass.GroupName(RegionUUID))
                        FormSetup.PropRegionClass.Timer(UUID) = RegionMaker.REGIONTIMER.Stopped
                        FormSetup.PropRegionClass.Status(UUID) = RegionMaker.SIMSTATUSENUM.Stopped ' already shutting down
                    Next
                End If

                PropUpdateView = True ' make form refresh
            End If

        ElseIf chosen = "Console" Then
            Dim PID = FormSetup.PropRegionClass.ProcessID(RegionUUID)
            If PID > 0 Then
                Dim hwnd = FormSetup.GetHwnd(FormSetup.PropRegionClass.GroupName(RegionUUID))

                Dim tmp As String = Settings.ConsoleShow
                'temp show console
                Settings.ConsoleShow = "True"
                FormSetup.ShowDOSWindow(hwnd, FormSetup.SHOWWINDOWENUM.SWRESTORE)
                Settings.ConsoleShow = tmp

            End If

        ElseIf chosen = "Edit" Then

#Disable Warning CA2000 ' Dispose objects before losing scope
            Dim RegionForm As New FormRegion
#Enable Warning CA2000 ' Dispose objects before losing scope
            RegionForm.BringToFront()
            RegionForm.Init(RegionName)
            RegionForm.Activate()
            RegionForm.Visible = True
            RegionForm.Select()

        ElseIf chosen = "Recycle" Then

            FormSetup.SequentialPause()

            ' shut down all regions in the DOS box
            Dim GroupName = FormSetup.PropRegionClass.GroupName(RegionUUID)
            FormSetup.Logger("RecyclingDown", GroupName, "Restart")
            For Each UUID In FormSetup.PropRegionClass.RegionUuidListByName(GroupName)
                FormSetup.PropRegionClass.Timer(UUID) = RegionMaker.REGIONTIMER.Stopped
                FormSetup.PropRegionClass.Status(UUID) = RegionMaker.SIMSTATUSENUM.RecyclingDown ' request a recycle.
                FormSetup.Logger("RecyclingDown", FormSetup.PropRegionClass.RegionName(UUID), "Restart")
            Next

            FormSetup.Print(My.Resources.Recycle1 & "  " + FormSetup.PropRegionClass.GroupName(RegionUUID))
            FormSetup.ConsoleCommand(RegionUUID, "q{ENTER}" + vbCrLf)
            PropUpdateView = True ' make form refresh

        ElseIf chosen = "Teleport" Then
            'secondlife://http|!!hg.osgrid.org|80+Lbsa+Plaza

            Dim link = "secondlife://http|!!" & Settings.PublicIP & "|" & Settings.HttpPort & "+" & RegionName
            Try
                System.Diagnostics.Process.Start(link)
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
            End Try

        End If

    End Sub

#End Region

#Region "Clicks"

    Private Shared Function PickGroup() As String

        Dim Chooseform As New FormChooser ' form for choosing a set of regions
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
                RegionUUID = FormSetup.PropRegionClass.FindRegionByName(name)
                FormSetup.PropRegionClass.RegionEnabled(RegionUUID) = X.Checked
                Settings.LoadIni(FormSetup.PropRegionClass.RegionPath(RegionUUID), ";")
                Settings.SetIni(FormSetup.PropRegionClass.RegionName(RegionUUID), "Enabled", CStr(X.Checked))
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

        FormSetup.PropOpensimIsRunning() = True
        FormSetup.StartMySQL()
        FormSetup.StartRobust()
        FormSetup.Timer1.Interval = 1000
        FormSetup.Timer1.Start() 'Timer starts functioning
        FormSetup.TimerBusy = 0

        For Each RegionUUID As String In FormSetup.PropRegionClass.RegionUuids
            Application.DoEvents()

            Dim GroupName = FormSetup.PropRegionClass.GroupName(RegionUUID)
            Dim Status = FormSetup.PropRegionClass.Status(RegionUUID)
            If FormSetup.PropRegionClass.RegionEnabled(RegionUUID) And
                    Not FormSetup.AvatarsIsInGroup(GroupName) And
                    Not FormSetup.PropAborting And
                    (Status = RegionMaker.SIMSTATUSENUM.Booting _
                    Or Status = RegionMaker.SIMSTATUSENUM.Booted _
                    Or Status = RegionMaker.SIMSTATUSENUM.Stopped) Then

                Dim hwnd = FormSetup.GetHwnd(GroupName)
                FormSetup.ShowDOSWindow(hwnd, FormSetup.SHOWWINDOWENUM.SWRESTORE)
                FormSetup.SequentialPause()
                FormSetup.Print(My.Resources.Not_Running & " " & Global.Outworldz.My.Resources.Restarting_word)

                FormSetup.ConsoleCommand(RegionUUID, "q{ENTER}" + vbCrLf)

                If Status = RegionMaker.SIMSTATUSENUM.Stopped Then
                    For Each UUID As String In FormSetup.PropRegionClass.RegionUuidListByName(GroupName)
                        FormSetup.PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.RestartPending
                    Next
                Else
                    ' shut down all regions in the DOS box
                    For Each UUID As String In FormSetup.PropRegionClass.RegionUuidListByName(GroupName)
                        FormSetup.PropRegionClass.Timer(UUID) = RegionMaker.REGIONTIMER.Stopped
                        FormSetup.PropRegionClass.Status(UUID) = RegionMaker.SIMSTATUSENUM.RecyclingDown
                    Next
                End If

                'PropUpdateView = True ' make form refresh
            End If
        Next

    End Sub

    Private Sub HelpToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HelpToolStripMenuItem.Click
        HelpManual("RegionList")
    End Sub

    Private Sub RunAllButton_Click(sender As Object, e As EventArgs) Handles RunAllButton.Click

        FormSetup.Startup()

    End Sub

    Private Sub StopAllButton_Click(sender As Object, e As EventArgs) Handles StopAllButton.Click

        FormSetup.DoStopActions()

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
                    FormSetup.Print(My.Resources.Aborted_word)
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
                        Dim RegionUUID As String = FormSetup.PropRegionClass.FindRegionByName(filename)

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
                        FormSetup.Print(My.Resources.Unrecognized & " " & extension & ". ")
                    End If
                Next

                FormSetup.PropRegionClass.GetAllRegions()
                LoadMyListView()
            End If
        End If
        ofd.Dispose()

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

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
