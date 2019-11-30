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
    Private _ImageListLarge As ImageList
    Private _ImageListSmall As New ImageList
    Private colsize = New ScreenPos(MyBase.Name & "ColumnSize")
    Private ItemsAreChecked As Boolean = False
    Private MysqlIsRunning As Boolean = False
    Private pixels As Integer = 70
    Private TheView As Integer = ViewType.Details
    Private Timertick As Integer = 0
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

    Public Property MysqlIsRunning1 As Boolean
        Get
            Return MysqlIsRunning
        End Get
        Set(value As Boolean)
            MysqlIsRunning = value
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
        ScreenPosition = New ScreenPos(MyBase.Name & View.ToString(Globalization.CultureInfo.InvariantCulture))
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

    End Sub

#End Region

#Region "Layout"

    Private Sub ListView1_ColumnWidthChanged(sender As Object, e As ColumnWidthChangedEventArgs) Handles ListView1.ColumnWidthChanged

        Dim w = ListView1.Columns(e.ColumnIndex).Width
        Dim name = ListView1.Columns(e.ColumnIndex).Text
        Using colsize As New ScreenPos(MyBase.Name & "ColumnSize")
            colsize.putSize(name, w)
            colsize.SaveFormSettings()
        End Using

    End Sub

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
        Suspended = 13

    End Enum

#End Region

#Region "Loader"

    Private Sub Form_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        Form1.Settings.RegionListVisible = False
        Form1.Settings.SaveSettings()
        _ImageListLarge.Dispose()
        _ImageListSmall.Dispose()

    End Sub

    Private Sub LoadForm(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Pixels1 = 70

        ListView1.Visible = False
        ListView1.LabelWrap = True
        ListView1.AutoArrange = True

        Form1.Settings.RegionListVisible = True
        Form1.Settings.SaveSettings()

        Form1.PropRegionClass.GetAllRegions()
        Me.Name = "Region List"
        Me.Text = My.Resources.Region_List
        AvatarView.Hide()
        AvatarView.CheckBoxes = False

        Application.DoEvents()

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

        ' Create columns for the items and subitems.
        ListView1.Columns.Add(My.Resources.Enable_word, colsize.ColumnWidth(My.Resources.Enable_word, 120), HorizontalAlignment.Center)
        ListView1.Columns.Add(My.Resources.DOS_Box_word, colsize.ColumnWidth(My.Resources.DOS_Box_word, 120), HorizontalAlignment.Left)
        ListView1.Columns.Add(My.Resources.Agents_word, colsize.ColumnWidth(My.Resources.Agents_word, 50), HorizontalAlignment.Center)
        ListView1.Columns.Add(My.Resources.Status_word, colsize.ColumnWidth(My.Resources.Status_word, 120), HorizontalAlignment.Center)
        ListView1.Columns.Add(My.Resources.RAM_Word, colsize.ColumnWidth(My.Resources.RAM_Word, 80), HorizontalAlignment.Center)
        ListView1.Columns.Add("X".ToUpperInvariant, colsize.ColumnWidth("X".ToUpperInvariant, 50), HorizontalAlignment.Center)
        ListView1.Columns.Add("Y".ToUpperInvariant, colsize.ColumnWidth("Y".ToUpperInvariant, 50), HorizontalAlignment.Center)
        ListView1.Columns.Add(My.Resources.Size_word, colsize.ColumnWidth(My.Resources.Size_word, 40), HorizontalAlignment.Center)
        ListView1.Columns.Add(My.Resources.Estate_word, colsize.ColumnWidth(My.Resources.Estate_word, 100), HorizontalAlignment.Left)

        ' optional
        ListView1.Columns.Add(My.Resources.Maps_word, colsize.ColumnWidth(My.Resources.Maps_word, 80), HorizontalAlignment.Center)
        ListView1.Columns.Add(My.Resources.Physics_word, colsize.ColumnWidth(My.Resources.Physics_word, 120), HorizontalAlignment.Center)
        ListView1.Columns.Add(My.Resources.Birds_word, colsize.ColumnWidth(My.Resources.Birds_word, 60), HorizontalAlignment.Center)
        ListView1.Columns.Add(My.Resources.Tides_word, colsize.ColumnWidth(My.Resources.Tides_word, 60), HorizontalAlignment.Center)
        ListView1.Columns.Add(My.Resources.Teleport_word, colsize.ColumnWidth(My.Resources.Teleport_word, 65), HorizontalAlignment.Center)
        ListView1.Columns.Add(My.Resources.Smart_Start_word, colsize.ColumnWidth(My.Resources.Smart_Start_word, 80), HorizontalAlignment.Center)
        ListView1.Columns.Add(My.Resources.Allow_Or_Disallow_Gods_word, colsize.ColumnWidth(My.Resources.Allow_Or_Disallow_Gods_word, 75), HorizontalAlignment.Center)
        ListView1.Columns.Add(My.Resources.Owner_God, colsize.ColumnWidth(My.Resources.Owner_God, 75), HorizontalAlignment.Center)
        ListView1.Columns.Add(My.Resources.Manager_God_word, colsize.ColumnWidth(My.Resources.Manager_God_word, 80), HorizontalAlignment.Center)
        ListView1.Columns.Add(My.Resources.No_Autobackup, colsize.ColumnWidth(My.Resources.No_Autobackup, 90), HorizontalAlignment.Center)
        ListView1.Columns.Add(My.Resources.Publicity_Word, colsize.ColumnWidth(My.Resources.Publicity_Word, 80), HorizontalAlignment.Center)

        ListView1.Columns.Add(My.Resources.Script_Rate_word, colsize.ColumnWidth(My.Resources.Script_Rate_word, 80), HorizontalAlignment.Center)
        ListView1.Columns.Add(My.Resources.Frame_Rate_word, colsize.ColumnWidth(My.Resources.Frame_Rate_word, 80), HorizontalAlignment.Center)

        'Add the items to the ListView.
        ' Connect the ListView.ColumnClick event to the ColumnClick event handler.
        AddHandler Me.ListView1.ColumnClick, AddressOf ColumnClick

        AvatarView.Columns.Add(My.Resources.Agents_word, 150, HorizontalAlignment.Center)
        AvatarView.Columns.Add(My.Resources.Region, 150, HorizontalAlignment.Center)
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
        Form1.PropUpdateView = True ' make form refresh

        Timer1.Interval = 1000 ' check for Form1.PropUpdateView every second
        Timer1.Start() 'Timer starts functioning

        SetScreen(TheView1)

        Form1.HelpOnce("RegionList")

    End Sub

    Private Sub MyListView_AfterLabelEdit(sender As Object, e As System.Windows.Forms.LabelEditEventArgs) Handles ListView1.AfterLabelEdit

        Debug.Print(e.Label)

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

#Region "Public Methods"

    Shared Function LoadImage(S As String) As Image
        Dim bmp As Bitmap = Nothing
        Dim u As New Uri(S)
        Dim request As System.Net.WebRequest = Net.WebRequest.Create(u)
        Dim response As System.Net.WebResponse = Nothing
        Try
            response = request.GetResponse()
        Catch ex As NotImplementedException
        End Try

        Dim responseStream As System.IO.Stream = Nothing
        Try
            responseStream = response.GetResponseStream()
        Catch ex As NotSupportedException
        End Try

        If responseStream IsNot Nothing Then
            bmp = New Bitmap(responseStream)
            responseStream.Dispose()
        End If

        Return bmp

    End Function

    Public Sub LoadMyListView()

        MysqlIsRunning = False
        If Form1.CheckMysql Then
            MysqlIsRunning = True
        End If
        If TheView1 = ViewType.Avatars Then
            ShowAvatars()
        Else
            ShowRegions()
        End If

    End Sub

#End Region

#Region "Private Methods"

    Private Sub Addregion_Click(sender As Object, e As EventArgs) Handles Addregion.Click

#Disable Warning CA2000 ' Dispose objects before losing scope
        Dim RegionForm As New FormRegion
#Enable Warning CA2000 ' Dispose objects before losing scope
        RegionForm.Init("")
        RegionForm.Activate()
        RegionForm.Visible = True
        RegionForm.Select()
        RegionForm.BringToFront()

    End Sub

    Private Sub AvatarView_Click(sender As Object, e As EventArgs) Handles AvatarView.Click

        Dim regions As ListView.SelectedListViewItemCollection = Me.AvatarView.SelectedItems
        Dim item As ListViewItem

        For Each item In regions
            Dim RegionName = item.SubItems(1).Text
            Dim R = Form1.PropRegionClass.FindRegionByName(RegionName)
            If R >= 0 Then
                Dim webAddress As String = "hop://" & Form1.Settings.DNSName & ":" & Form1.Settings.HttpPort & "/" & RegionName
                Try
                    Dim result = Process.Start(webAddress)
                Catch ex As ObjectDisposedException
                Catch ex As FileNotFoundException
                Catch ex As System.ComponentModel.Win32Exception
                End Try
            End If
        Next
        PropUpdateView() = True

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Form1.PropRegionClass.GetAllRegions()
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

    Private Sub HelpToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles HelpToolStripMenuItem1.Click
        Form1.Help("RegionList")
    End Sub

    Private Sub ListClick(sender As Object, e As EventArgs) Handles ListView1.Click

        Dim regions As ListView.SelectedListViewItemCollection = Me.ListView1.SelectedItems
        Dim item As ListViewItem
        For Each item In regions
            Dim RegionName = item.SubItems(0).Text
            Dim R = Form1.PropRegionClass.FindRegionByName(RegionName)
            If R >= 0 Then
                StartStopEdit(R, RegionName)
            End If
        Next

    End Sub

    Private Sub ListView1_ItemCheck1(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles ListView1.ItemCheck

        Dim Item As ListViewItem = Nothing
        Try
            Item = ListView1.Items.Item(e.Index)
        Catch ex As ArgumentOutOfRangeException
        End Try

        Dim n As Integer = Form1.PropRegionClass.FindRegionByName(Item.Text)
        If n = -1 Then Return
        Dim GroupName = Form1.PropRegionClass.GroupName(n)
        For Each X In Form1.PropRegionClass.RegionListByGroupNum(GroupName)
            If ViewNotBusy1 Then
                If (e.CurrentValue = CheckState.Unchecked) Then
                    Form1.PropRegionClass.RegionEnabled(X) = True
                    ' and region file on disk
                    Form1.Settings.LoadIni(Form1.PropRegionClass.RegionPath(X), ";")
                    Form1.Settings.SetIni(Form1.PropRegionClass.RegionName(X), "Enabled", "True")
                    Form1.Settings.SaveINI()
                ElseIf (e.CurrentValue = CheckState.Checked) Then
                    Form1.PropRegionClass.RegionEnabled(X) = False
                    ' and region file on disk
                    Form1.Settings.LoadIni(Form1.PropRegionClass.RegionPath(X), ";")
                    Form1.Settings.SetIni(Form1.PropRegionClass.RegionName(X), "Enabled", "False")
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

            ' Create items and subitems for each item.
            Dim L As New Dictionary(Of String, String)

            If MysqlIsRunning1 Then
                L = MysqlInterface.GetAgentList()
            End If

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

            Index = 0

            ' Hypergrid
            '
            ' Create items and subitems for each item.
            Dim M As New Dictionary(Of String, String)
            If MysqlIsRunning1 Then
                M = GetHGAgentList()
            End If

            For Each Agent In M
                If Agent.Value.Length > 0 Then
                    Dim item1 As New ListViewItem(Agent.Key, Index)
                    item1.SubItems.Add(Agent.Value)
                    item1.SubItems.Add(My.Resources.HG_word_NT)
                    AvatarView.Items.AddRange(New ListViewItem() {item1})
                    Index += 1
                End If
            Next

            If Index = 0 Then
                Dim item1 As New ListViewItem(My.Resources.No_Avatars, Index)
                item1.SubItems.Add("-".ToUpperInvariant)
                item1.SubItems.Add(My.Resources.HG_word_NT)
                AvatarView.Items.AddRange(New ListViewItem() {item1})
            End If

            Me.AvatarView.TabIndex = 0
            AvatarView.EndUpdate()

            AvatarView.Show()

            ViewNotBusy1 = True
            PropUpdateView() = False
#Disable Warning CA1031 ' Do not catch general exception types
        Catch ex As Exception
#Enable Warning CA1031 ' Do not catch general exception types
            Form1.Log(My.Resources.Error_word, " RegionList " & ex.Message)
        End Try

    End Sub

#End Region

#Region "Private Methods"

    Private Sub ShowRegions()

        Try

            Dim MysqlIsRunning = False
            If Form1.CheckMysql Then
                MysqlIsRunning = True
            End If
            ViewNotBusy1 = False
            ListView1.BeginUpdate()

            ImageListLarge1 = New ImageList()

            If Pixels1 = 0 Then Pixels1 = 20
            ImageListLarge1.ImageSize = New Size(Pixels1, Pixels1)

            ListView1.Items.Clear()

            Dim Num As Integer = 0

            ' have to get maps by http port + region UUID, not region port + uuid
            Try

                For Each X In Form1.PropRegionClass.RegionNumbers

                    Dim Letter As String = ""
                    If Form1.PropRegionClass.Status(X) = RegionMaker.SIMSTATUSENUM.Stopped _
                        And Form1.PropRegionClass.SmartStart(X) Then
                        Letter = "Waiting"
                        Num = DGICON.SmartStart
                    ElseIf Form1.PropRegionClass.Status(X) = RegionMaker.SIMSTATUSENUM.Suspended Then
                        Letter = "Suspended"
                        Num = DGICON.Suspended
                    ElseIf Form1.PropRegionClass.Status(X) = RegionMaker.SIMSTATUSENUM.RecyclingDown Then
                        Letter = "Recycling Down"
                        Num = DGICON.recyclingdown
                    ElseIf Form1.PropRegionClass.Status(X) = RegionMaker.SIMSTATUSENUM.RecyclingUp Then
                        Letter = "Recycling Up"
                        Num = DGICON.recyclingup
                    ElseIf Form1.PropRegionClass.Status(X) = RegionMaker.SIMSTATUSENUM.RestartPending Then
                        Letter = "Restart Pending"
                        Num = DGICON.recyclingup
                    ElseIf Form1.PropRegionClass.Status(X) = RegionMaker.SIMSTATUSENUM.RetartingNow Then
                        Letter = "Restarting Now"
                        Num = DGICON.recyclingup
                    ElseIf Form1.PropRegionClass.Status(X) = RegionMaker.SIMSTATUSENUM.Booting Then
                        Letter = "Booting"
                        Num = DGICON.bootingup
                    ElseIf Form1.PropRegionClass.Status(X) = RegionMaker.SIMSTATUSENUM.ShuttingDown Then
                        Letter = "Stopping"
                        Num = DGICON.shuttingdown
                    ElseIf Form1.PropRegionClass.Status(X) = RegionMaker.SIMSTATUSENUM.Booted And Form1.PropRegionClass.AvatarCount(X) = 1 Then
                        Letter = "Running"
                        Num = DGICON.user1
                    ElseIf Form1.PropRegionClass.Status(X) = RegionMaker.SIMSTATUSENUM.Booted And Form1.PropRegionClass.AvatarCount(X) > 1 Then
                        Letter = "Running"
                        Num = DGICON.user2
                    ElseIf Form1.PropRegionClass.Status(X) = RegionMaker.SIMSTATUSENUM.Booted Then
                        If Form1.PropRegionClass.RegionName(X) = Form1.Settings.WelcomeRegion Then
                            Num = DGICON.Home
                            Letter = "Running"
                        Else
                            Letter = "Running"
                            Num = DGICON.up
                        End If
                    ElseIf Not Form1.PropRegionClass.RegionEnabled(X) Then
                        Letter = "Disabled"
                        If Form1.PropRegionClass.RegionName(X) = Form1.Settings.WelcomeRegion Then
                            Num = DGICON.HomeOffline
                        Else
                            Num = DGICON.disabled
                        End If
                    ElseIf Form1.PropRegionClass.RegionEnabled(X) Then
                        Letter = "Stopped"
                        Num = DGICON.stopped
                    Else
                        Num = DGICON.warning ' warning
                    End If

                    If TheView1 = ViewType.Icons Then
                        ListView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)
                    Else
                        ListView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.None)
                    End If
                    ' maps
                    If TheView1 = ViewType.Maps Then

                        If Form1.PropRegionClass.Status(X) = RegionMaker.SIMSTATUSENUM.Booted Then
                            Dim img As String = "http://127.0.0.1:" + Form1.PropRegionClass.GroupPort(X).ToString(Globalization.CultureInfo.InvariantCulture) + "/" + "index.php?method=regionImage" + Form1.PropRegionClass.UUID(X).Replace("-".ToUpperInvariant, "")

                            Dim bmp As Image = LoadImage(img)
                            If bmp Is Nothing Then
                                ImageListLarge1.Images.Add(My.Resources.ResourceManager.GetObject("OfflineMap", Globalization.CultureInfo.InvariantCulture))
                            Else
                                ImageListLarge1.Images.Add(bmp)
                            End If
                        Else
                            ImageListLarge1.Images.Add(My.Resources.ResourceManager.GetObject("OfflineMap", Globalization.CultureInfo.InvariantCulture))
                        End If
                        Num = X

                    End If

                    ' Create items and subitems for each item. Place a check mark next to the item.
                    Dim item1 As New ListViewItem(Form1.PropRegionClass.RegionName(X), Num) With {
                        .Checked = Form1.PropRegionClass.RegionEnabled(X)
                    }
                    item1.SubItems.Add(Form1.PropRegionClass.GroupName(X).ToString(Globalization.CultureInfo.InvariantCulture))

                    item1.SubItems.Add(Form1.PropRegionClass.AvatarCount(X).ToString(Globalization.CultureInfo.InvariantCulture))
                    item1.SubItems.Add(Letter)
                    Dim fmtXY = "00000" ' 65536
                    Dim fmtRam = "0000." ' 9999 MB
                    ' RAM
                    Try
                        Dim PID = Form1.PropRegionClass.ProcessID(X)
                        Dim component1 As Process = Process.GetProcessById(PID)
                        If Form1.PropRegionHandles.ContainsKey(PID) Then
                            Dim Memory As Double = (component1.WorkingSet64 / 1024) / 1024
                            item1.SubItems.Add(FormatNumber(Memory.ToString(fmtRam, Globalization.CultureInfo.InvariantCulture)))
                        Else
                            item1.SubItems.Add("0".ToUpperInvariant)
                        End If
#Disable Warning CA1031 ' Do not catch general exception types
                    Catch ex As Exception
#Enable Warning CA1031 ' Do not catch general exception types
                        item1.SubItems.Add("0".ToUpperInvariant)
                    End Try
                    item1.SubItems.Add(Form1.PropRegionClass.CoordX(X).ToString(fmtXY, Globalization.CultureInfo.InvariantCulture))
                    item1.SubItems.Add(Form1.PropRegionClass.CoordY(X).ToString(fmtXY, Globalization.CultureInfo.InvariantCulture))

                    Dim size As String = ""
                    If Form1.PropRegionClass.SizeX(X) = 256 Then
                        size = "1X1"
                    ElseIf Form1.PropRegionClass.SizeX(X) = 512 Then
                        size = "2X2"
                    ElseIf Form1.PropRegionClass.SizeX(X) = 768 Then
                        size = "3X3"
                    ElseIf Form1.PropRegionClass.SizeX(X) = 1024 Then
                        size = "4X4"
                    Else
                        size = Form1.PropRegionClass.SizeX(X).ToString(Globalization.CultureInfo.InvariantCulture)
                    End If
                    item1.SubItems.Add(size)

                    ' add estate name
                    Dim Estate = "-".ToUpperInvariant
                    If MysqlIsRunning Then
                        Estate = MysqlInterface.EstateName(Form1.PropRegionClass.UUID(X))
                    End If
                    item1.SubItems.Add(Estate)

                    'Map
                    If Form1.PropRegionClass.MapType(X).Length > 0 Then
                        item1.SubItems.Add(Form1.PropRegionClass.MapType(X))
                    Else
                        item1.SubItems.Add("-".ToUpperInvariant)
                    End If

                    ' physics
                    Select Case Form1.PropRegionClass.Physics(X)
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

                    If Form1.PropRegionClass.Birds(X) = "True" Then
                        item1.SubItems.Add(My.Resources.Yes_word)
                    Else
                        item1.SubItems.Add("-".ToUpperInvariant)
                    End If

                    'Tides
                    If Form1.PropRegionClass.Tides(X) = "True" Then
                        item1.SubItems.Add(My.Resources.Yes_word)
                    Else
                        item1.SubItems.Add("-".ToUpperInvariant)
                    End If

                    'teleport
                    If Form1.PropRegionClass.Teleport(X) = "True" Then
                        item1.SubItems.Add(My.Resources.Yes_word)
                    Else
                        item1.SubItems.Add("-".ToUpperInvariant)
                    End If

                    If Form1.PropRegionClass.RegionName(X) = Form1.Settings.WelcomeRegion Then
                        item1.SubItems.Add(My.Resources.HomeText)
                    Else
                        If Form1.PropRegionClass.SmartStart(X) = "True" Then
                            item1.SubItems.Add(My.Resources.Yes_word)
                        Else
                            item1.SubItems.Add("-".ToUpperInvariant)
                        End If
                    End If

                    If Form1.PropRegionClass.AllowGods(X).Length > 0 Then
                        item1.SubItems.Add(Form1.PropRegionClass.AllowGods(X))
                    Else
                        item1.SubItems.Add("-".ToUpperInvariant)
                    End If

                    If Form1.PropRegionClass.RegionGod(X).Length > 0 Then
                        item1.SubItems.Add(Form1.PropRegionClass.RegionGod(X))
                    Else
                        item1.SubItems.Add("-".ToUpperInvariant)
                    End If

                    If Form1.PropRegionClass.ManagerGod(X).Length > 0 Then
                        item1.SubItems.Add(Form1.PropRegionClass.ManagerGod(X))
                    Else
                        item1.SubItems.Add("-".ToUpperInvariant)
                    End If

                    If Form1.PropRegionClass.SkipAutobackup(X).Length > 0 Then
                        item1.SubItems.Add(Form1.PropRegionClass.SkipAutobackup(X))
                    Else
                        item1.SubItems.Add("-".ToUpperInvariant)
                    End If

                    If Form1.PropRegionClass.RegionSnapShot(X).Length > 0 Then
                        item1.SubItems.Add(Form1.PropRegionClass.RegionSnapShot(X))
                    Else
                        item1.SubItems.Add("-".ToUpperInvariant)
                    End If

                    If Form1.PropRegionClass.MinTimerInterval(X).Length > 0 Then
                        item1.SubItems.Add(Form1.PropRegionClass.MinTimerInterval(X))
                    Else
                        item1.SubItems.Add("-".ToUpperInvariant)
                    End If

                    If Form1.PropRegionClass.FrameTime(X).Length > 0 Then
                        item1.SubItems.Add(Form1.PropRegionClass.FrameTime(X))
                    Else
                        item1.SubItems.Add("-".ToUpperInvariant)
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
#Disable Warning CA1031 ' Do not catch general exception types
            Catch ex As Exception
#Enable Warning CA1031 ' Do not catch general exception types
                Form1.Log(My.Resources.Error_word, " RegionList " & ex.Message)
                Form1.PropRegionClass.GetAllRegions()
                PropUpdateView() = False
            End Try
#Disable Warning CA1031 ' Do not catch general exception types
        Catch ex As Exception
#Enable Warning CA1031 ' Do not catch general exception types
            Form1.Log(My.Resources.Error_word, " RegionList " & ex.Message)
            Form1.PropRegionClass.GetAllRegions()
            PropUpdateView() = False
        End Try

        ListView1.Show()
        AvatarView.Hide()

    End Sub

    Private Sub StartStopEdit(n As Integer, RegionName As String)

        ' show it, stop it, start it, or edit it
        Dim hwnd = Form1.GetHwnd(Form1.PropRegionClass.GroupName(n))
        Form1.ShowDOSWindow(hwnd, Form1.SHOWWINDOWENUM.SWRESTORE)
        Form1.Sleep(1000)
        Dim Choices As New FormRegionPopup
        Dim chosen As String = ""
        Choices.Init(RegionName)
        Choices.ShowDialog()
        Choices.Activate()
        Choices.BringToFront()

        ' Read the chosen sim name
        chosen = Choices.Choice()
        Choices.Dispose()

        If chosen = "Start" Then

            ' it was stopped, and off, so we start up
            If Not Form1.StartMySQL() Then
                Form1.ProgressBar1.Value = 0
                Form1.ProgressBar1.Visible = True
                Form1.Print(My.Resources.Stopped_word)
            End If
            Form1.StartRobust()
            Form1.Log("Starting", Form1.PropRegionClass.RegionName(n))
            Form1.Boot(Form1.PropRegionClass, Form1.PropRegionClass.RegionName(n))
            Form1.Timer1.Start() 'Timer starts functioning

        ElseIf chosen = "Stop" Then

            ' if any avatars in any region, give them a choice.
            Dim StopIt As Boolean = True
            For Each num In Form1.PropRegionClass.RegionListByGroupNum(Form1.PropRegionClass.GroupName(n))
                ' Ask before killing any people
                If Form1.PropRegionClass.AvatarCount(num) > 0 Then
                    Dim response As MsgBoxResult
                    If Form1.PropRegionClass.AvatarCount(num) = 1 Then
                        response = MsgBox(My.Resources.OneAvatar + Form1.PropRegionClass.RegionName(num) + My.Resources.Do_you_still_want_to_Stop_word, vbYesNo)
                    Else
                        response = MsgBox(Form1.PropRegionClass.AvatarCount(num).ToString(Globalization.CultureInfo.InvariantCulture) + " " & My.Resources.people_are_in & " " + Form1.PropRegionClass.RegionName(num) + ". " & My.Resources.Do_you_still_want_to_Stop_word, vbYesNo)
                    End If
                    If response = vbNo Then
                        StopIt = False
                    End If
                End If
            Next

            If (StopIt) Then
                Dim regionNum = Form1.PropRegionClass.FindRegionByName(RegionName)
                Dim h As IntPtr = Form1.GetHwnd(Form1.PropRegionClass.GroupName(n))
                If Form1.ShowDOSWindow(hwnd, Form1.SHOWWINDOWENUM.SWRESTORE) Then
                    Form1.SequentialPause()
                    Form1.ConsoleCommand(Form1.PropRegionClass.GroupName(regionNum), "q{ENTER}" + vbCrLf)
                    Form1.Print(My.Resources.Stopping_word & " " + Form1.PropRegionClass.GroupName(regionNum))
                    ' shut down all regions in the DOS box
                    For Each regionNum In Form1.PropRegionClass.RegionListByGroupNum(Form1.PropRegionClass.GroupName(regionNum))
                        Form1.PropRegionClass.Timer(regionNum) = RegionMaker.REGIONTIMER.Stopped
                        Form1.PropRegionClass.Status(regionNum) = RegionMaker.SIMSTATUSENUM.ShuttingDown ' request a recycle.
                    Next
                Else
                    ' shut down all regions in the DOS box
                    For Each regionNum In Form1.PropRegionClass.RegionListByGroupNum(Form1.PropRegionClass.GroupName(regionNum))
                        Form1.PropRegionClass.Timer(regionNum) = RegionMaker.REGIONTIMER.Stopped
                        Form1.PropRegionClass.Status(regionNum) = RegionMaker.SIMSTATUSENUM.Stopped ' already shutting down
                    Next
                End If

                PropUpdateView = True ' make form refresh
            End If

        ElseIf chosen = "Edit" Then

            Dim RegionForm As New FormRegion
            RegionForm.Init(Form1.PropRegionClass.RegionName(n))
            RegionForm.Activate()
            RegionForm.Visible = True
            RegionForm.Select()
            RegionForm.BringToFront()

        ElseIf chosen = "Recycle" Then

            Form1.SequentialPause()
            Form1.ConsoleCommand(Form1.PropRegionClass.GroupName(n), "q{ENTER}" + vbCrLf)
            Form1.Print(My.Resources.Recycle1 & "  " + Form1.PropRegionClass.GroupName(n))
            Form1.PropRestartNow = True

            ' shut down all regions in the DOS box

            For Each RegionNum In Form1.PropRegionClass.RegionListByGroupNum(Form1.PropRegionClass.GroupName(n))
                Form1.PropRegionClass.Timer(RegionNum) = RegionMaker.REGIONTIMER.Stopped
                Form1.PropRegionClass.Status(RegionNum) = RegionMaker.SIMSTATUSENUM.RecyclingDown ' request a recycle.
            Next
            PropUpdateView = True ' make form refresh

        ElseIf chosen = "Teleport" Then
            Dim link = "hop:   " & Form1.Settings.PublicIP & ":" & Form1.Settings.HttpPort & "/" & RegionName
            Try
                System.Diagnostics.Process.Start(link)
            Catch ex As InvalidOperationException
            Catch ex As System.ComponentModel.Win32Exception
            End Try

        End If

    End Sub

    Private Sub StopRegionNum(num As Integer)
        Form1.SequentialPause()
        Dim hwnd = Form1.GetHwnd(Form1.PropRegionClass.GroupName(num))
        Form1.Log("Region", "Stopping Region " + Form1.PropRegionClass.GroupName(num))
        Form1.ConsoleCommand(Form1.PropRegionClass.GroupName(num), "q{ENTER}" + vbCrLf)
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

        Form1.PropRestartNow = True

        For Each X As Integer In Form1.PropRegionClass.RegionNumbers

            If Form1.PropOpensimIsRunning() _
                And Form1.PropRegionClass.RegionEnabled(X) _
                And Not Form1.PropRegionClass.Status(X) = RegionMaker.SIMSTATUSENUM.ShuttingDown _
                And Not Form1.PropRegionClass.Status(X) = RegionMaker.SIMSTATUSENUM.RecyclingDown Then

                Dim hwnd = Form1.GetHwnd(Form1.PropRegionClass.GroupName(X))
                If Form1.ShowDOSWindow(hwnd, Form1.SHOWWINDOWENUM.SWRESTORE) Then
                    Form1.SequentialPause()
                    Form1.ConsoleCommand(Form1.PropRegionClass.GroupName(X), "q{ENTER}" + vbCrLf)
                    Form1.Print(My.Resources.Restarting_word & " " & Form1.PropRegionClass.GroupName(X))
                End If

                ' shut down all regions in the DOS box
                For Each Y In Form1.PropRegionClass.RegionListByGroupNum(Form1.PropRegionClass.GroupName(X))
                    Form1.PropRegionClass.Timer(Y) = RegionMaker.REGIONTIMER.Stopped
                    Form1.PropRegionClass.Status(Y) = RegionMaker.SIMSTATUSENUM.RecyclingDown
                Next

                PropUpdateView = True ' make form refresh
            End If
        Next

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
        Catch ex As InvalidOperationException
            chosen = ""
        Catch ex As ArgumentOutOfRangeException
            chosen = ""
        End Try

        Chooseform.Dispose()

        Return chosen

    End Function

    Private Sub RunAllButton_Click(sender As Object, e As EventArgs) Handles RunAllButton.Click

        Form1.Startup()

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

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Dim ofd As New OpenFileDialog
        ofd.InitialDirectory = "c\\"
        ofd.Filter = My.Resources.INI_Filter
        ofd.FilterIndex = 2
        ofd.RestoreDirectory = True

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

                Dim extension As String = Path.GetExtension(ofd.FileName)
                extension = Mid(extension, 2, 5)
                If extension.ToUpper(Globalization.CultureInfo.InvariantCulture) = "INI" Then

                    Dim filename = GetRegionsName(ofd.FileName)

                    Dim i = Form1.PropRegionClass.FindRegionByName(filename)
                    If i >= 0 Then
                        MsgBox(My.Resources.Region_Already_Exists, vbInformation, My.Resources.Info)
                        ofd.Dispose()
                        Return
                    End If

                    If dirpathname.Length = 0 Then dirpathname = filename

                    Dim NewFilepath = Form1.PropOpensimBinPath & "bin\Regions\" + dirpathname + "\Region\"
                    If Not Directory.Exists(NewFilepath) Then
                        Try
                            Directory.CreateDirectory(Form1.PropOpensimBinPath & "bin\Regions\" + dirpathname + "\Region")
                        Catch ex As ArgumentException
                        Catch ex As IO.PathTooLongException
                        Catch ex As NotSupportedException
                        Catch ex As UnauthorizedAccessException
                        Catch ex As IO.IOException
                        End Try
                    End If

                    File.Copy(ofd.FileName, Form1.PropOpensimBinPath & "bin\Regions\" + dirpathname + "\Region\" + filename + ".ini")
                Else
                    Form1.Print(My.Resources.Unrecognized & " " & extension & ". ")
                End If

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

    Private Sub Button3_Click_1(sender As Object, e As EventArgs) Handles Button3.Click

        Dim regionname = Form1.ChooseRegion(True)
        If regionname.Length = 0 Then Return
        Dim RegionNum = Form1.PropRegionClass.FindRegionByName(regionname)
        Dim RegionPort = Form1.PropRegionClass.GroupPort(RegionNum)
        Dim webAddress As String = "http://" & Form1.Settings.PublicIP & ":" & CType(RegionPort, String) & "/SStats/"
        Try
            Process.Start(webAddress)
        Catch ex As InvalidOperationException
        Catch ex As System.ComponentModel.Win32Exception
        End Try
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
