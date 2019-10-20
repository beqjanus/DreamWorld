#Region "Copyright"

' Copyright 2014 Fred Beckhusen for www.Outworldz.com https://opensource.org/licenses/AGPL

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

    Private Shared _FormExists As Boolean = False
    Dim _ImageListLarge As ImageList
    Dim _ImageListSmall As New ImageList
    Dim ItemsAreChecked As Boolean = False
    Dim pixels As Integer = 70
    Dim PropRegionClass As RegionMaker = RegionMaker.Instance()
    Dim TheView As Integer = ViewType.Details
    Private Timertick As Integer = 0
    Dim ViewNotBusy As Boolean

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

    Shared Property PropUpdateView() As Boolean
        Get
            Return Form1.PropUpdateView
        End Get
        Set(ByVal Value As Boolean)
            Form1.PropUpdateView = Value
        End Set
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

    Public Property PropRegionClass1 As RegionMaker
        Get
            Return PropRegionClass
        End Get
        Set(value As RegionMaker)
            PropRegionClass = value
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

    Public Property Timertick1 As Integer
        Get
            Return Timertick
        End Get
        Set(value As Integer)
            Timertick = value
        End Set
    End Property

    Public Property ViewNotBusy1 As Boolean
        Get
            Return ViewNotBusy
        End Get
        Set(value As Boolean)
            ViewNotBusy = value
        End Set
    End Property

#End Region

#Region "ScreenSize"

    Private _screenPosition As ScreenPos
    Private Handler As New EventHandler(AddressOf Resize_page)

    'The following detects  the location of the form in screen coordinates
    Private Sub Resize_page(ByVal sender As Object, ByVal e As System.EventArgs)

        ScreenPosition.SaveXY(Me.Left, Me.Top)
        ScreenPosition.SaveHW(Me.Height, Me.Width)
    End Sub

    Private Sub SetScreen(View As Integer)
        Me.Show()
        ScreenPosition = New ScreenPos(MyBase.Name & View.ToString(Form1.Invarient))
        AddHandler ResizeEnd, Handler
        Dim xy As List(Of Integer) = ScreenPosition.GetXY()
        Me.Left = xy.Item(0)
        Me.Top = xy.Item(1)
        Dim hw As List(Of Integer) = ScreenPosition.GetHW()

        If hw.Item(0) = 0 Then
            Me.Height = 400
        Else
            Me.Height = hw.Item(0)
        End If
        If hw.Item(1) = 0 Then
            Me.Width = 560
        Else
            Me.Width = hw.Item(1)
        End If

    End Sub

#End Region

#Region "Layout"

    Private Sub Panel1_MouseWheel(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles ListView1.MouseWheel

        If TheView1 = ViewType.Maps And ViewNotBusy1 Then
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
        Dim Y = Me.Height - 125
        ListView1.Size = New System.Drawing.Size(X, Y)
        AvatarView.Size = New System.Drawing.Size(X, Y)

    End Sub

#End Region

    ' icons imagelist layout
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

    End Enum

#Region "Loader"

    Private Sub LoadForm(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Pixels1 = 70
        RegionList.FormExists1 = True

        Form1.Settings.RegionListVisible = True
        Form1.Settings.SaveSettings()

        AvatarView.Hide()
        AvatarView.CheckBoxes = False

        ' Set the view to show details.
        TheView1 = Form1.Settings.RegionListView()
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
        ListView1.LabelEdit = False
        AvatarView.LabelEdit = False

        ' Allow the user to rearrange columns.
        ListView1.AllowColumnReorder = True
        AvatarView.AllowColumnReorder = False

        Me.ListView1.ListViewItemSorter = New ListViewItemComparer(2)
        Me.AvatarView.ListViewItemSorter = New ListViewItemComparer(1)

        ' Select the item and subitems when selection is made.
        ListView1.FullRowSelect = True
        AvatarView.FullRowSelect = True

        ' Display grid lines.
        ListView1.GridLines = True
        AvatarView.GridLines = True

        ' Sort the items in the list in ascending order.
        ListView1.Sorting = SortOrder.Ascending
        AvatarView.Sorting = SortOrder.Ascending

        ' Create columns for the items and subitems. Width of -2 indicates auto-size.
        ListView1.Columns.Add("Enabled", 120, HorizontalAlignment.Center)
        ListView1.Columns.Add("DOS Box", 100, HorizontalAlignment.Center)
        ListView1.Columns.Add("Agents", 50, HorizontalAlignment.Center)
        ListView1.Columns.Add("Status", 120, HorizontalAlignment.Center)
        ListView1.Columns.Add("RAM", 80, HorizontalAlignment.Center)
        ListView1.Columns.Add("X", 50, HorizontalAlignment.Center)
        ListView1.Columns.Add("Y", 50, HorizontalAlignment.Center)
        ListView1.Columns.Add("Size", 40, HorizontalAlignment.Center)
        ListView1.Columns.Add("Estate", 100, HorizontalAlignment.Center)

        ' optional
        ListView1.Columns.Add("Map", 80, HorizontalAlignment.Center)
        ListView1.Columns.Add("Physics", 120, HorizontalAlignment.Center)
        ListView1.Columns.Add("Birds", 60, HorizontalAlignment.Center)
        ListView1.Columns.Add("Tides", 60, HorizontalAlignment.Center)
        ListView1.Columns.Add("Teleport", 60, HorizontalAlignment.Center)
        ListView1.Columns.Add("SmartStart", 80, HorizontalAlignment.Center)

        'Add the items to the ListView.
        ' Connect the ListView.ColumnClick event to the ColumnClick event handler.
        AddHandler Me.ListView1.ColumnClick, AddressOf ColumnClick
        Me.Name = "Region List"
        Me.Text = "Region List"

        AvatarView.Columns.Add("Agent", 150, HorizontalAlignment.Left)
        AvatarView.Columns.Add("Region", 150, HorizontalAlignment.Center)
        AvatarView.Columns.Add("Type", 80, HorizontalAlignment.Center)

        ' index to display icons
        ImageListSmall1.Images.Add(My.Resources.ResourceManager.GetObject("navigate_up2", Form1.Invarient))   ' 0 booting up
        ImageListSmall1.Images.Add(My.Resources.ResourceManager.GetObject("navigate_down2", Form1.Invarient)) ' 1 shutting down
        ImageListSmall1.Images.Add(My.Resources.ResourceManager.GetObject("check2", Form1.Invarient)) ' 2 okay, up
        ImageListSmall1.Images.Add(My.Resources.ResourceManager.GetObject("media_stop_red", Form1.Invarient)) ' 3 disabled
        ImageListSmall1.Images.Add(My.Resources.ResourceManager.GetObject("media_stop", Form1.Invarient))  ' 4 enabled, stopped
        ImageListSmall1.Images.Add(My.Resources.ResourceManager.GetObject("navigate_down", Form1.Invarient))  ' 5 Recycling down
        ImageListSmall1.Images.Add(My.Resources.ResourceManager.GetObject("navigate_up", Form1.Invarient))  ' 6 Recycling Up
        ImageListSmall1.Images.Add(My.Resources.ResourceManager.GetObject("warning", Form1.Invarient))  ' 7 Unknown
        ImageListSmall1.Images.Add(My.Resources.ResourceManager.GetObject("user2", Form1.Invarient))  ' 8 - 1 User
        ImageListSmall1.Images.Add(My.Resources.ResourceManager.GetObject("users1", Form1.Invarient))  ' 9 - 2 user
        ImageListSmall1.Images.Add(My.Resources.ResourceManager.GetObject("refresh", Form1.Invarient))  ' 10 - 2 user
        ImageListSmall1.Images.Add(My.Resources.ResourceManager.GetObject("home", Form1.Invarient))  '  11- home
        ImageListSmall1.Images.Add(My.Resources.ResourceManager.GetObject("home_02", Form1.Invarient))  '  12- home _offline
        Form1.PropUpdateView = True ' make form refresh

        LoadMyListView()
        Timer1.Interval = 1000 ' check for Form1.PropUpdateView every second
        Timer1.Start() 'Timer starts functioning

        SetScreen(TheView1)

        Form1.HelpOnce("RegionList")

    End Sub

    Private Sub Form_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        RegionList.FormExists1 = False
        Form1.Settings.RegionListVisible = False
        Form1.Settings.SaveSettings()
        _ImageListSmall.Dispose()

    End Sub

#End Region

#Region "Timer"

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick

        If PropUpdateView() Or Timertick1 Mod 120 = 0 Then ' force a refresh
            LoadMyListView()
        End If
        Timertick1 += 1

    End Sub

#End Region

#Region "Load List View"

    Shared Function LoadImage(S As String) As Image
        Dim bmp As Bitmap = Nothing
        Dim u As New Uri(S)
        Dim request As System.Net.WebRequest = Net.WebRequest.Create(u)
        Try
            Dim response As System.Net.WebResponse = request.GetResponse()
            Dim responseStream As System.IO.Stream = response.GetResponseStream()
            bmp = New Bitmap(responseStream)
            responseStream.Dispose()
        Catch ex As Exception
            Form1.Log("Maps", ex.Message + ":" + S)
        End Try

        Return bmp

    End Function

    Public Sub LoadMyListView()

        If TheView1 = ViewType.Avatars Then
            ShowAvatars()
        Else
            ShowRegions()
        End If

    End Sub

    Private Sub Addregion_Click(sender As Object, e As EventArgs) Handles Addregion.Click

        Dim RegionForm As New FormRegion
        RegionForm.Init("")
        RegionForm.Activate()
        RegionForm.Visible = True

    End Sub

    Private Sub AvatarView_Click(sender As Object, e As EventArgs) Handles AvatarView.Click

        Dim regions As ListView.SelectedListViewItemCollection = Me.AvatarView.SelectedItems
        Dim item As ListViewItem

        For Each item In regions
            Try
                Dim RegionName = item.SubItems(1).Text
                Debug.Print("Clicked row " + RegionName)
                Dim R = PropRegionClass1.FindRegionByName(RegionName)
                If R >= 0 Then
                    Dim webAddress As String = "hop://" & Form1.Settings.DNSName & ":" & Form1.Settings.HttpPort & "/" & RegionName
                    Dim result = Process.Start(webAddress)
                End If
            Catch ex As Exception
                ' Form1.Log("Error", ex.Message)
            End Try
        Next
        PropUpdateView() = True

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        PropRegionClass1.GetAllRegions()
        LoadMyListView()

    End Sub

    ' ColumnClick event handler.
    Private Sub ColumnClick(ByVal o As Object, ByVal e As ColumnClickEventArgs)

        ListView1.SuspendLayout()
        Me.ListView1.Sorting = SortOrder.None

        ' Set the ListViewItemSorter property to a new ListViewItemComparer object. Setting this
        ' property immediately sorts the ListView using the ListViewItemComparer object.
        Me.ListView1.ListViewItemSorter = New ListViewItemComparer(e.Column)

        ListView1.ResumeLayout()

    End Sub

    Private Sub ListClick(sender As Object, e As EventArgs) Handles ListView1.Click
        Dim regions As ListView.SelectedListViewItemCollection = Me.ListView1.SelectedItems
        Dim item As ListViewItem

        For Each item In regions
            Dim RegionName = item.SubItems(0).Text

            Debug.Print("Clicked row " + RegionName)
            Dim R = PropRegionClass1.FindRegionByName(RegionName)
            If R >= 0 Then
                StartStopEdit(R, RegionName)
            End If
        Next

    End Sub

    Private Sub ListView1_ItemCheck1(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles ListView1.ItemCheck

        Dim Item As ListViewItem = ListView1.Items.Item(e.Index)
        Dim n As Integer = PropRegionClass1.FindRegionByName(Item.Text)
        If n = -1 Then Return
        Dim GroupName = PropRegionClass1.GroupName(n)
        For Each X In PropRegionClass1.RegionListByGroupNum(GroupName)
            If ViewNotBusy1 Then
                If (e.CurrentValue = CheckState.Unchecked) Then
                    PropRegionClass1.RegionEnabled(X) = True
                    ' and region file on disk
                    Form1.Settings.LoadIni(PropRegionClass1.RegionPath(X), ";")
                    Form1.Settings.SetIni(PropRegionClass1.RegionName(X), "Enabled", "true")
                    Form1.Settings.SaveINI()
                ElseIf (e.CurrentValue = CheckState.Checked) Then
                    PropRegionClass1.RegionEnabled(X) = False
                    ' and region file on disk
                    Form1.Settings.LoadIni(PropRegionClass1.RegionPath(X), ";")
                    Form1.Settings.SetIni(PropRegionClass1.RegionName(X), "Enabled", "false")
                    Form1.Settings.SaveINI()
                End If
            End If
        Next

        PropUpdateView() = True ' force a refresh

    End Sub

    Private Sub ShowAvatars()
        Try
            ViewNotBusy1 = False

            AvatarView.Show()
            ListView1.Hide()

            AvatarView.BeginUpdate()
            AvatarView.Items.Clear()

            Dim Index = 0
            Try
                ' Create items and subitems for each item.

                Dim L As New Dictionary(Of String, String)

                L = MysqlInterface.GetAgentList()

                For Each Agent In L
                    Dim item1 As New ListViewItem(Agent.Key, Index)
                    item1.SubItems.Add(Agent.Value)
                    item1.SubItems.Add("Local")
                    AvatarView.Items.AddRange(New ListViewItem() {item1})
                    Index += 1
                Next

                If L.Count = 0 Then
                    Dim item1 As New ListViewItem("No Avatars", Index)
                    item1.SubItems.Add("-")
                    item1.SubItems.Add("Local Grid")
                    AvatarView.Items.AddRange(New ListViewItem() {item1})
                    Index += 1
                End If
            Catch ex As Exception
                Dim item1 As New ListViewItem("No Avatars", Index)
                item1.SubItems.Add("-")
                item1.SubItems.Add("Local Grid")
                AvatarView.Items.AddRange(New ListViewItem() {item1})
                Index += 1
            End Try

            Index = 0
            ' Hypergrid
            Try
                ' Create items and subitems for each item.
                Dim L As New Dictionary(Of String, String)
                L = MysqlInterface.GetHGAgentList()

                For Each Agent In L
                    If Agent.Value.Length > 0 Then
                        Dim item1 As New ListViewItem(Agent.Key, Index)
                        item1.SubItems.Add(Agent.Value)
                        item1.SubItems.Add("Hypergrid")
                        AvatarView.Items.AddRange(New ListViewItem() {item1})
                        Index += 1
                    End If
                Next

                If Index = 0 Then
                    Dim item1 As New ListViewItem("No Avatars", Index)
                    item1.SubItems.Add("-")
                    item1.SubItems.Add("Hypergrid")
                    AvatarView.Items.AddRange(New ListViewItem() {item1})
                End If
            Catch
                Dim item1 As New ListViewItem("No Avatars", Index)
                item1.SubItems.Add("-")
                item1.SubItems.Add("Hypergrid")
                AvatarView.Items.AddRange(New ListViewItem() {item1})
            End Try

            Me.AvatarView.TabIndex = 0
            AvatarView.EndUpdate()
            AvatarView.Show()

            ViewNotBusy1 = True
            PropUpdateView() = False
        Catch ex As Exception
            Form1.Log("Error", " RegionList " & ex.Message)
        End Try

    End Sub

    Private Sub ShowRegions()

        Dim MysqlIsRunning = False
        If Form1.CheckMysql Then
            MysqlIsRunning = True
        End If

        ListView1.Show()
        AvatarView.Hide()

        Try
            ViewNotBusy1 = False
            ListView1.BeginUpdate()

            ImageListLarge1 = New ImageList()

            If Pixels1 = 0 Then Pixels1 = 20
            ImageListLarge1.ImageSize = New Size(Pixels1, Pixels1)
            ListView1.Items.Clear()

            Dim Num As Integer = 0

            ' have to get maps by http port + region UUID, not region port + uuid

            For Each X In PropRegionClass1.RegionNumbers

                Dim Letter As String = ""
                If PropRegionClass1.Status(X) = RegionMaker.SIMSTATUSENUM.Stopped _
                        And PropRegionClass1.SmartStart(X) Then
                    Letter = "Waiting"
                    Num = DGICON.SmartStart
                ElseIf PropRegionClass1.Status(X) = RegionMaker.SIMSTATUSENUM.RecyclingDown Then
                    Letter = "Recycling Down"
                    Num = DGICON.recyclingdown
                ElseIf PropRegionClass1.Status(X) = RegionMaker.SIMSTATUSENUM.RecyclingUp Then
                    Letter = "Recycling Up"
                    Num = DGICON.recyclingup
                ElseIf PropRegionClass1.Status(X) = RegionMaker.SIMSTATUSENUM.RestartPending Then
                    Letter = "Restart Pending"
                    Num = DGICON.recyclingup
                ElseIf PropRegionClass1.Status(X) = RegionMaker.SIMSTATUSENUM.RetartingNow Then
                    Letter = "Restarting Now"
                    Num = DGICON.recyclingup
                ElseIf PropRegionClass1.Status(X) = RegionMaker.SIMSTATUSENUM.Booting Then
                    Letter = "Booting"
                    Num = DGICON.bootingup
                ElseIf PropRegionClass1.Status(X) = RegionMaker.SIMSTATUSENUM.ShuttingDown Then
                    Letter = "Stopping"
                    Num = DGICON.shuttingdown
                ElseIf PropRegionClass1.Status(X) = RegionMaker.SIMSTATUSENUM.Booted And PropRegionClass1.AvatarCount(X) = 1 Then
                    Letter = "Running"
                    Num = DGICON.user1
                ElseIf PropRegionClass1.Status(X) = RegionMaker.SIMSTATUSENUM.Booted And PropRegionClass1.AvatarCount(X) > 1 Then
                    Letter = "Running"
                    Num = DGICON.user2
                ElseIf PropRegionClass1.Status(X) = RegionMaker.SIMSTATUSENUM.Booted Then
                    If PropRegionClass1.RegionName(X) = Form1.Settings.WelcomeRegion Then
                        Num = DGICON.Home
                        Letter = "Running"
                    Else
                        Letter = "Running"
                        Num = DGICON.up
                    End If
                ElseIf Not PropRegionClass1.RegionEnabled(X) Then
                    Letter = "Disabled"
                    If PropRegionClass1.RegionName(X) = Form1.Settings.WelcomeRegion Then
                        Num = DGICON.HomeOffline
                    Else
                        Num = DGICON.disabled
                    End If
                ElseIf PropRegionClass1.RegionEnabled(X) Then
                    Letter = "Stopped"
                    Num = DGICON.stopped
                Else
                    Num = DGICON.warning ' warning
                End If

                ' maps
                If TheView1 = ViewType.Maps Then

                    If PropRegionClass1.Status(X) = RegionMaker.SIMSTATUSENUM.Booted Then
                        Dim img As String = "http://127.0.0.1:" + PropRegionClass1.GroupPort(X).ToString(Form1.Invarient) + "/" + "index.php?method=regionImage" + PropRegionClass1.UUID(X).Replace("-", "")
                        Debug.Print(img)

                        Dim bmp As Image = LoadImage(img)
                        If bmp Is Nothing Then
                            ImageListLarge1.Images.Add(My.Resources.ResourceManager.GetObject("OfflineMap", Form1.Invarient))
                        Else
                            ImageListLarge1.Images.Add(bmp)
                        End If
                    Else
                        ImageListLarge1.Images.Add(My.Resources.ResourceManager.GetObject("OfflineMap", Form1.Invarient))
                    End If
                    Num = X

                End If

                ' Create items and subitems for each item. Place a check mark next to the item.
                Dim item1 As New ListViewItem(PropRegionClass1.RegionName(X), Num) With {
                        .Checked = PropRegionClass1.RegionEnabled(X)
                    }
                item1.SubItems.Add(PropRegionClass1.GroupName(X).ToString(Form1.Invarient))
                item1.SubItems.Add(PropRegionClass1.AvatarCount(X).ToString(Form1.Invarient))
                item1.SubItems.Add(Letter)
                Dim fmtXY = "00000" ' 65536
                Dim fmtRam = "0000." ' 9999 MB
                ' RAM
                Try
                    Dim PID = PropRegionClass1.ProcessID(X)
                    Dim component1 As Process = Process.GetProcessById(PID)
                    If Form1.PropRegionHandles.ContainsKey(PID) Then
                        Dim NotepadMemory As Double = (component1.WorkingSet64 / 1024) / 1024
                        item1.SubItems.Add(FormatNumber(NotepadMemory.ToString(fmtRam, Form1.Invarient)))
                    Else
                        item1.SubItems.Add("0")
                    End If
                Catch ex As Exception
                    item1.SubItems.Add("0")
                End Try
                item1.SubItems.Add(PropRegionClass1.CoordX(X).ToString(fmtXY, Form1.Invarient))
                item1.SubItems.Add(PropRegionClass1.CoordY(X).ToString(fmtXY, Form1.Invarient))

                Dim size As String = ""
                If PropRegionClass1.SizeX(X) = 256 Then
                    size = "1X1"
                ElseIf PropRegionClass1.SizeX(X) = 512 Then
                    size = "2X2"
                ElseIf PropRegionClass1.SizeX(X) = 768 Then
                    size = "3X3"
                ElseIf PropRegionClass1.SizeX(X) = 1024 Then
                    size = "4X4"
                Else
                    size = PropRegionClass1.SizeX(X).ToString(Form1.Invarient)
                End If
                item1.SubItems.Add(size)

                ' add estate name
                Dim Estate = "-"
                If MysqlIsRunning Then
                    Estate = MysqlInterface.EstateName(PropRegionClass1.UUID(X))
                End If
                item1.SubItems.Add(Estate)

                'Map
                If PropRegionClass1.MapType(X).Length > 0 Then
                    item1.SubItems.Add(PropRegionClass1.MapType(X))
                Else
                    item1.SubItems.Add(Form1.Settings.MapType)
                End If

                ' physics
                Select Case PropRegionClass1.Physics(X)
                    Case ""
                        item1.SubItems.Add("-")
                    Case "0"
                        item1.SubItems.Add("None")
                    Case "1"
                        item1.SubItems.Add("ODE")
                    Case "2"
                        item1.SubItems.Add("Bullet")
                    Case "3"
                        item1.SubItems.Add("Bullet/Threaded")
                    Case "4"
                        item1.SubItems.Add("ubODE")
                    Case "5"
                        item1.SubItems.Add("ubODE Hybrid")
                    Case Else
                        item1.SubItems.Add("-")
                End Select

                'birds

                If PropRegionClass1.Birds(X) = "True" Then
                    item1.SubItems.Add("Yes")
                Else
                    item1.SubItems.Add("-")
                End If

                'Tides
                If PropRegionClass1.Tides(X) = "True" Then
                    item1.SubItems.Add("Yes")
                Else
                    item1.SubItems.Add("-")
                End If

                'teleport
                If PropRegionClass1.Teleport(X) = "True" Or
                    PropRegionClass1.RegionName(X) = Form1.Settings.WelcomeRegion Then
                    item1.SubItems.Add("Yes")
                Else
                    item1.SubItems.Add("-")
                End If

                If PropRegionClass1.RegionName(X) = Form1.Settings.WelcomeRegion Then
                    item1.SubItems.Add("Home")
                Else
                    If PropRegionClass1.SmartStart(X) = True Then
                        item1.SubItems.Add("Yes")
                    Else
                        item1.SubItems.Add("-")
                    End If
                End If

                ListView1.Items.AddRange(New ListViewItem() {item1})

            Next

            'Assign the ImageList objects to the ListView.
            ListView1.LargeImageList = ImageListLarge1
            ListView1.SmallImageList = ImageListSmall1

            Me.ListView1.TabIndex = 0

            For i As Integer = 0 To ListView1.Items.Count - 1
                If ListView1.Items(i).Checked Then
                    ListView1.Items(i).ForeColor = SystemColors.ControlText
                Else
                    ListView1.Items(i).ForeColor = SystemColors.GrayText
                End If
            Next i

            ListView1.EndUpdate()
            ViewNotBusy1 = True
            PropUpdateView() = False
        Catch ex As Exception
            Form1.Log("Error", " RegionList " & ex.Message)
            PropRegionClass1.GetAllRegions()
        End Try

    End Sub

    Private Sub StartStopEdit(n As Integer, RegionName As String)

        ' show it, stop it, start it, or edit it
        Dim hwnd = Form1.GetHwnd(PropRegionClass1.GroupName(n))
        Form1.ShowDOSWindow(hwnd, Form1.SHOWWINDOWENUM.SWRESTORE)

        Dim Choices As New FormRegionPopup
        Dim chosen As String = ""
        Choices.Init(RegionName)
        Choices.ShowDialog()
        ' Read the chosen sim name
        chosen = Choices.Choice()
        Choices.Dispose()

        If chosen = "Start" Then

            ' it was stopped, and off, so we start up
            If Not Form1.StartMySQL() Then
                Form1.ProgressBar1.Value = 0
                Form1.ProgressBar1.Visible = True
                Form1.Print("Stopped")
            End If
            Form1.StartRobust()
            Form1.Log("Starting", PropRegionClass1.RegionName(n))
            Form1.CopyOpensimProto(PropRegionClass1.RegionName(n))
            Form1.Boot(PropRegionClass1, PropRegionClass1.RegionName(n), True)
            Form1.Timer1.Start() 'Timer starts functioning
            PropUpdateView() = True ' force a refresh

        ElseIf chosen = "Stop" Then

            ' if any avatars in any region, give them a choice.
            Dim StopIt As Boolean = True
            For Each num In PropRegionClass1.RegionListByGroupNum(PropRegionClass1.GroupName(n))
                ' Ask before killing any people
                If PropRegionClass1.AvatarCount(num) > 0 Then
                    Dim response As MsgBoxResult
                    If PropRegionClass1.AvatarCount(num) = 1 Then
                        response = MsgBox("There is one avatar in " + PropRegionClass1.RegionName(num) + ".  Do you still want to stop it?", vbYesNo)
                    Else
                        response = MsgBox("There are " + PropRegionClass1.AvatarCount(num).ToString(Form1.Invarient) + " avatars in " + PropRegionClass1.RegionName(num) + ".  Do you still want to stop it?", vbYesNo)
                    End If
                    If response = vbNo Then
                        StopIt = False
                    End If
                End If
            Next

            If (StopIt) Then
                Dim regionNum = PropRegionClass1.FindRegionByName(RegionName)
                Dim h As IntPtr = Form1.GetHwnd(PropRegionClass1.GroupName(n))
                If Form1.ShowDOSWindow(hwnd, Form1.SHOWWINDOWENUM.SWRESTORE) Then
                    Form1.SequentialPause()
                    Form1.ConsoleCommand(PropRegionClass1.GroupName(regionNum), "q{ENTER}" + vbCrLf)
                    Form1.Print("Stopping " + PropRegionClass1.GroupName(regionNum))
                    ' shut down all regions in the DOS box
                    For Each regionNum In PropRegionClass1.RegionListByGroupNum(PropRegionClass1.GroupName(regionNum))
                        PropRegionClass1.Timer(regionNum) = RegionMaker.REGIONTIMER.Stopped
                        PropRegionClass1.Status(regionNum) = RegionMaker.SIMSTATUSENUM.ShuttingDown ' request a recycle.
                    Next
                Else
                    ' shut down all regions in the DOS box
                    For Each regionNum In PropRegionClass1.RegionListByGroupNum(PropRegionClass1.GroupName(regionNum))
                        PropRegionClass1.Timer(regionNum) = RegionMaker.REGIONTIMER.Stopped
                        PropRegionClass1.Status(regionNum) = RegionMaker.SIMSTATUSENUM.Stopped ' already shutting down
                    Next
                End If

                PropUpdateView = True ' make form refresh
            End If

            PropUpdateView = True ' make form refresh

        ElseIf chosen = "Edit" Then

            Dim RegionForm As New FormRegion
            RegionForm.Init(PropRegionClass1.RegionName(n))
            RegionForm.Activate()
            RegionForm.Visible = True
            RegionForm.Select()

        ElseIf chosen = "Recycle" Then
            'Dim h As IntPtr = Form1.GetHwnd(PropRegionClass.GroupName(n))
            Form1.SequentialPause()
            Form1.ConsoleCommand(PropRegionClass1.GroupName(n), "q{ENTER}" + vbCrLf)
            Form1.Print("Recycle " + PropRegionClass1.GroupName(n))
            Form1.PropRestartNow = True

            ' shut down all regions in the DOS box

            For Each RegionNum In PropRegionClass1.RegionListByGroupNum(PropRegionClass1.GroupName(n))
                PropRegionClass1.Timer(RegionNum) = RegionMaker.REGIONTIMER.Stopped
                PropRegionClass1.Status(RegionNum) = RegionMaker.SIMSTATUSENUM.RecyclingDown ' request a recycle.
            Next
            PropUpdateView = True ' make form refresh

        End If

    End Sub

    Private Sub StopRegionNum(num As Integer)
        Form1.SequentialPause()
        Dim hwnd = Form1.GetHwnd(PropRegionClass1.GroupName(num))
        Form1.Log("Region", "Stopping Region " + PropRegionClass1.GroupName(num))
        Form1.ConsoleCommand(PropRegionClass1.GroupName(num), "q{ENTER}" + vbCrLf)
    End Sub

#End Region

#Region "Clicks"

    Private Sub AllNone_CheckedChanged(sender As Object, e As EventArgs) Handles AllNome.CheckedChanged

        For Each X As ListViewItem In ListView1.Items
            If ItemsAreChecked1 Then
                X.Checked = CType(CheckState.Unchecked, Boolean)
            Else
                X.Checked = CType(CheckState.Checked, Boolean)
            End If
        Next

        If ItemsAreChecked1 Then
            ItemsAreChecked1 = False
        Else
            ItemsAreChecked1 = True
        End If
        PropUpdateView = True ' make form refresh

    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles RestartButton.Click

        For Each X As Integer In PropRegionClass1.RegionNumbers

            If Form1.PropOpensimIsRunning() _
                And PropRegionClass1.RegionEnabled(X) _
                And Not PropRegionClass1.Status(X) = RegionMaker.SIMSTATUSENUM.ShuttingDown _
                And Not PropRegionClass1.Status(X) = RegionMaker.SIMSTATUSENUM.RecyclingDown Then

                Dim hwnd = Form1.GetHwnd(PropRegionClass1.GroupName(X))
                If Form1.ShowDOSWindow(hwnd, Form1.SHOWWINDOWENUM.SWRESTORE) Then
                    Form1.SequentialPause()
                    Form1.ConsoleCommand(PropRegionClass1.GroupName(X), "q{ENTER}" + vbCrLf)
                    Form1.Print("Restarting " & PropRegionClass1.GroupName(X))
                End If

                ' shut down all regions in the DOS box
                For Each Y In PropRegionClass1.RegionListByGroupNum(PropRegionClass1.GroupName(X))
                    PropRegionClass1.Timer(Y) = RegionMaker.REGIONTIMER.Stopped
                    PropRegionClass1.Status(Y) = RegionMaker.SIMSTATUSENUM.RecyclingDown
                Next
                Form1.PropRestartNow = True

                PropUpdateView = True ' make form refresh
            End If
        Next
        PropUpdateView = True ' make form refresh
    End Sub

    Private Sub HelpToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HelpToolStripMenuItem.Click
        Form1.Help("RegionList")
    End Sub

    Private Sub MapsToolStripMenuItem_Click(sender As Object, e As EventArgs)

        Form1.Settings.RegionListView() = ViewType.Maps
        Form1.Settings.SaveSettings()
        TheView1 = ViewType.Maps
        ListView1.View = View.LargeIcon
        ListView1.Show()
        AvatarView.Hide()
        ListView1.CheckBoxes = False
        Timer1.Stop()
        LoadMyListView()
    End Sub

    Private Function PickGroup() As String

        Dim Chooseform As New Choice ' form for choosing a set of regions
        ' Show testDialog as a modal dialog and determine if DialogResult = OK.

        Chooseform.FillGrid("Group")

        Dim chosen As String
        Chooseform.ShowDialog()
        Try
            ' Read the chosen GROUP name
            chosen = Chooseform.DataGridView.CurrentCell.Value.ToString()
        Catch ex As Exception
            chosen = ""
        End Try

        Chooseform.Dispose()

        Return chosen

    End Function

    Private Sub RunAllButton_Click(sender As Object, e As EventArgs) Handles RunAllButton.Click

        Form1.Startup(True)

    End Sub

    Private Sub StopAllButton_Click(sender As Object, e As EventArgs) Handles StopAllButton.Click
        Form1.KillAll()
    End Sub

    Private Sub ViewAvatars_Click(sender As Object, e As EventArgs) Handles ViewAvatars.Click

        Form1.Settings.RegionListView() = ViewType.Avatars
        Form1.Settings.SaveSettings()
        TheView1 = ViewType.Avatars
        SetScreen(TheView1)
        ListView1.View = View.Details
        ListView1.Hide()
        AvatarView.Show()
        LoadMyListView()
        Timer1.Start()
    End Sub

    Private Sub ViewCompact_Click(sender As Object, e As EventArgs) Handles ViewCompact.Click

        Form1.Settings.RegionListView() = ViewType.Icons
        Form1.Settings.SaveSettings()
        TheView1 = ViewType.Icons
        SetScreen(TheView1)
        ListView1.View = View.SmallIcon
        ListView1.Show()
        AvatarView.Hide()
        ListView1.CheckBoxes = False
        Timer1.Start()
        LoadMyListView()
    End Sub

    Private Sub ViewDetail_Click(sender As Object, e As EventArgs) Handles ViewDetail.Click

        Form1.Settings.RegionListView() = ViewType.Details
        Form1.Settings.SaveSettings()
        TheView1 = ViewType.Details
        SetScreen(TheView1)
        ListView1.View = View.Details
        ListView1.Show()
        AvatarView.Hide()
        ListView1.CheckBoxes = True
        Timer1.Start()
        LoadMyListView()
    End Sub

    Private Sub ViewMaps_Click(sender As Object, e As EventArgs) Handles ViewMaps.Click

        Form1.Settings.RegionListView() = ViewType.Maps
        Form1.Settings.SaveSettings()
        TheView1 = ViewType.Maps
        SetScreen(TheView1)
        ListView1.View = View.LargeIcon
        ListView1.Show()
        AvatarView.Hide()
        ListView1.CheckBoxes = False
        Timer1.Stop()
        LoadMyListView()
    End Sub

#End Region

#Region "Mysql"

    Private Sub HelpToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles HelpToolStripMenuItem1.Click
        Form1.Help("RegionList")
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Dim ofd As New OpenFileDialog
        ofd.InitialDirectory = "c:\\"
        ofd.Filter = "ini files (*.ini)|*.ini|All files (*.*)|*.*"
        ofd.FilterIndex = 2
        ofd.RestoreDirectory = True

        If ofd.ShowDialog = DialogResult.OK Then
            If ofd.CheckFileExists Then

                Dim dirpathname = PickGroup()
                If dirpathname.Length = 0 Then
                    Form1.Print("Aborted")
                    ofd.Dispose()
                    Return
                End If

                If dirpathname = "! Add New Name" Then
                    dirpathname = InputBox("Enter the New Dos Box name")
                End If
                If dirpathname.Length = 0 Then
                    ofd.Dispose()
                    Return
                End If

                Dim extension As String = Path.GetExtension(ofd.FileName)
                extension = Mid(extension, 2, 5)
                If extension.ToLower(Form1.Invarient) = "ini" Then

                    Dim filename = GetRegionsName(ofd.FileName)

                    Dim i = PropRegionClass1.FindRegionByName(filename)
                    If i >= 0 Then
                        MsgBox("Region name " + filename + " already exists", vbInformation, "Info")
                        ofd.Dispose()
                        Return
                    End If

                    If dirpathname.Length = 0 Then dirpathname = filename

                    Dim NewFilepath = Form1.PropOpensimBinPath & "bin\Regions\" + dirpathname + "\Region\"
                    If Not Directory.Exists(NewFilepath) Then
                        Directory.CreateDirectory(Form1.PropOpensimBinPath & "bin\Regions\" + dirpathname + "\Region")
                    End If

                    File.Copy(ofd.FileName, Form1.PropOpensimBinPath & "bin\Regions\" + dirpathname + "\Region\" + filename + ".ini")
                Else
                    Form1.Print("Unrecognized file type" + extension + ". Import Region.ini file to the system.")
                End If

                PropRegionClass1.GetAllRegions()
                LoadMyListView()
            End If
        End If
        ofd.Dispose()

    End Sub

    Private Function GetRegionsName(Region As String) As String

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

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles RestartRobustButton.Click
        Form1.PropRestartRobust = True
        Form1.ConsoleCommand("Robust", "q{ENTER}" + vbCrLf)
    End Sub

    Private Sub Button3_Click_1(sender As Object, e As EventArgs) Handles Button3.Click

        Dim regionname = Form1.ChooseRegion(True)
        If regionname.Length = 0 Then Return
        Dim RegionNum = PropRegionClass.FindRegionByName(regionname)
        Dim RegionPort = PropRegionClass.GroupPort(RegionNum)
        Dim webAddress As String = "http://" & Form1.Settings.PublicIP & ":" & CType(RegionPort, String) & "/SStats/"
        Process.Start(webAddress)

    End Sub

#End Region

End Class

#Region "Compare"

' Implements the manual sorting of items by columns.
Class ListViewItemComparer
    Implements IComparer
#Disable Warning IDE0044 ' Add readonly modifier
    Private col As Integer
#Enable Warning IDE0044 ' Add readonly modifier

    Public Sub New()
        col = 1
    End Sub

    Public Sub New(ByVal column As Integer)
        col = column
    End Sub

    Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare

        Dim a = CType(x, ListViewItem).SubItems(col).Text
        Dim b = CType(y, ListViewItem).SubItems(col).Text

        Return [String].Compare(a, b, StringComparison.InvariantCultureIgnoreCase)

    End Function

End Class

#End Region