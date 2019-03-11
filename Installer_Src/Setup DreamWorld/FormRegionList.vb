
Imports System.IO

Public Class RegionList

#Region "Declarations"

    Dim ViewNotBusy As Boolean
    Dim TheView As Integer = ViewType.Details
    Private Shared FormExists As Boolean = False
    Dim pixels As Integer = 70
    Dim imageListSmall As New ImageList
    Dim imageListLarge As ImageList
    Dim ItemsAreChecked As Boolean = False
    Dim RegionClass As RegionMaker = RegionMaker.Instance(Form1.MysqlConn)
    Dim Timertick As Integer = 0

    Private Enum ViewType As Integer
        Maps = 0
        Icons = 1
        Details = 2
        Avatars = 3
    End Enum

#End Region

#Region "Properties"
    Public Property UpdateView() As Boolean
        Get
            Return Form1.UpdateView
        End Get
        Set(ByVal Value As Boolean)
            Form1.UpdateView = Value
        End Set
    End Property
    ' property exposing FormExists
    Public Shared ReadOnly Property InstanceExists() As Boolean
        Get
            ' Access shared members through the Class name, not an instance.
            Return RegionList.FormExists
        End Get
    End Property
#End Region

#Region "ScreenSize"
    Public ScreenPosition As ScreenPos
    Private Handler As New EventHandler(AddressOf Resize_page)

    'The following detects  the location of the form in screen coordinates
    Private Sub Resize_page(ByVal sender As Object, ByVal e As System.EventArgs)
        'Me.Text = "Form screen position = " + Me.Location.ToString
        ScreenPosition.SaveXY(Me.Left, Me.Top)
        ScreenPosition.SaveHW(Me.Height, Me.Width)
    End Sub
    Private Sub SetScreen()
        Me.Show()
        ScreenPosition = New ScreenPos(Me.Name)
        AddHandler ResizeEnd, Handler
        Dim xy As List(Of Integer) = ScreenPosition.GetXY()
        Me.Left = xy.Item(0)
        Me.Top = xy.Item(1)
        Dim hw As List(Of Integer) = ScreenPosition.GetHW()

        ' Me.Size = New System.Drawing.Size(500, 390)

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

        If TheView = ViewType.Maps And ViewNotBusy Then
            ' Update the drawing based upon the mouse wheel scrolling.
            Dim numberOfTextLinesToMove As Integer = CInt(e.Delta * SystemInformation.MouseWheelScrollLines / 120)

            pixels = pixels + numberOfTextLinesToMove
            'Debug.Print(pixels.ToString)
            If pixels > 256 Then pixels = 256
            If pixels < 10 Then pixels = 10

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

    Enum ICONS
        ' index of 0-4 to display icons
        bootingup = 0
        shuttingdown = 1
        up = 2
        disabled = 3
        stopped = 4
        recyclingdown = 5
        recyclingup = 6
        warning = 7
    End Enum
#Region "Loader"

    Private Sub _Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        pixels = 70

        RegionList.FormExists = True

        AvatarView.Hide()

        ' ListView Setup
        ListView1.AllowDrop = True
        ' Set the view to show details.
        ListView1.View = View.Details
        AvatarView.View = View.Details
        ' Allow the user to edit item text.
        ListView1.LabelEdit = True
        ' Allow the user to rearrange columns.
        ListView1.AllowColumnReorder = True
        ' Display check boxes.
        ListView1.CheckBoxes = True
        AvatarView.CheckBoxes = False
        ' Select the item and subitems when selection is made.
        ListView1.FullRowSelect = True
        ' Display grid lines.
        ListView1.GridLines = True
        AvatarView.GridLines = True
        ' Sort the items in the list in ascending order.
        ListView1.Sorting = SortOrder.Ascending
        AvatarView.Sorting = SortOrder.Ascending

        'Add the items to the ListView.
        ' Connect the ListView.ColumnClick event to the ColumnClick event handler.
        AddHandler Me.ListView1.ColumnClick, AddressOf ColumnClick
        Me.Name = "Region List"
        Me.Text = "Region List"

        ' index  to display icons
        imageListSmall.Images.Add(My.Resources.ResourceManager.GetObject("navigate_up2"))   ' 0 booting up
        imageListSmall.Images.Add(My.Resources.ResourceManager.GetObject("navigate_down2")) ' 1 shutting down
        imageListSmall.Images.Add(My.Resources.ResourceManager.GetObject("check2")) ' 2 okay, up
        imageListSmall.Images.Add(My.Resources.ResourceManager.GetObject("media_stop_red")) ' 3 disabled
        imageListSmall.Images.Add(My.Resources.ResourceManager.GetObject("media_stop"))  ' 4 enabled, stopped
        imageListSmall.Images.Add(My.Resources.ResourceManager.GetObject("navigate_down"))  ' 5 Recycling down
        imageListSmall.Images.Add(My.Resources.ResourceManager.GetObject("navigate_up"))  ' 6 Recycling Up
        imageListSmall.Images.Add(My.Resources.ResourceManager.GetObject("warning"))  ' 6 Unknown

        Form1.UpdateView = True ' make form refresh
        LoadMyListView()

        ListView1.Show()
        Timer1.Interval = 1000 ' check for Form1.UpdateView every second
        Timer1.Start() 'Timer starts functioning
        SetScreen()

        Form1.HelpOnce("RegionList")

    End Sub

    Private Sub SingletonForm_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        RegionList.FormExists = False
    End Sub
#End Region

#Region "Timer"
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick

        If UpdateView() Or Timertick Mod 120 = 0 Then ' force a refresh
            LoadMyListView()
        End If
        Timertick = Timertick + 1

    End Sub
#End Region

#Region "Load List View"
    Public Sub LoadMyListView()

        If TheView = ViewType.Avatars Then
            ShowAvatars()
        Else
            ShowRegions()
        End If

    End Sub
    Private Sub ShowRegions()

        ListView1.Show()
        AvatarView.Hide()

        Try
            ViewNotBusy = False
            ListView1.BeginUpdate()

            imageListLarge = New ImageList()
            If pixels = 0 Then pixels = 20
            imageListLarge.ImageSize = New Size(pixels, pixels)
            ListView1.Clear()
            ListView1.Items.Clear()

            ' Create columns for the items and subitems.
            ' Width of -2 indicates auto-size.
            ListView1.Columns.Add("Enabled", 120, HorizontalAlignment.Center)
            ListView1.Columns.Add("DOS Box", 100, HorizontalAlignment.Center)
            ListView1.Columns.Add("Agents", 50, HorizontalAlignment.Center)
            ListView1.Columns.Add("Status", 80, HorizontalAlignment.Center)
            ListView1.Columns.Add("X", 50, HorizontalAlignment.Center)
            ListView1.Columns.Add("Y", 50, HorizontalAlignment.Center)
            ListView1.Columns.Add("Size", 40, HorizontalAlignment.Center)
            ' optional
            ListView1.Columns.Add("Map", 80, HorizontalAlignment.Center)
            ListView1.Columns.Add("Physics", 120, HorizontalAlignment.Center)
            ListView1.Columns.Add("Birds", 60, HorizontalAlignment.Center)
            ListView1.Columns.Add("Tides", 60, HorizontalAlignment.Center)
            ListView1.Columns.Add("Teleport", 80, HorizontalAlignment.Center)


            Dim Num As Integer = 0

            ' have to get maps by http port + region UUID, not region port + uuid
            ' RegionClass.DebugGroup() ' show the list of groups and http ports.

            For Each X In RegionClass.RegionNumbers
                If RegionClass.RegionName(X) = "Welcome" Then
                    Debug.Print("Welcome")
                End If
                Dim Letter As String = ""
                If RegionClass.Status(X) = RegionMaker.SIM_STATUS.RecyclingDown Then
                    Letter = "Recycling Down"
                    Num = ICONS.recyclingdown
                ElseIf RegionClass.Status(X) = RegionMaker.SIM_STATUS.RecyclingUp Then
                    Letter = "Recycling Up"
                    Num = ICONS.recyclingup
                ElseIf RegionClass.Status(X) = RegionMaker.SIM_STATUS.RestartPending Then
                    Letter = "Restart Pending"
                    Num = ICONS.recyclingup
                ElseIf RegionClass.Status(X) = RegionMaker.SIM_STATUS.RetartingNow Then
                    Letter = "Restarting Now"
                    Num = ICONS.recyclingup
                ElseIf RegionClass.Status(X) = RegionMaker.SIM_STATUS.Booting Then
                    Letter = "Booting"
                    Num = ICONS.bootingup
                ElseIf RegionClass.Status(X) = RegionMaker.SIM_STATUS.ShuttingDown Then
                    Letter = "Stopping"
                    Num = ICONS.shuttingdown
                ElseIf RegionClass.Status(X) = RegionMaker.SIM_STATUS.Booted Then
                    Letter = "Running"
                    Num = ICONS.up
                ElseIf Not RegionClass.RegionEnabled(X) Then
                    Letter = "Disabled"
                    Num = ICONS.disabled
                ElseIf RegionClass.RegionEnabled(X) Then
                    Letter = "Stopped"
                    Num = ICONS.stopped
                Else
                    Num = ICONS.warning ' warning
                End If

                ' maps
                If TheView = ViewType.Maps Then

                    If RegionClass.Status(X) = RegionMaker.SIM_STATUS.Booted Then
                        Dim img As String = "http://127.0.0.1:" + RegionClass.GroupPort(X).ToString + "/" + "index.php?method=regionImage" + RegionClass.UUID(X).Replace("-", "")
                        Debug.Print(img)

                        Dim bmp As Image = LoadImage(img)
                        If bmp Is Nothing Then
                            imageListLarge.Images.Add(My.Resources.ResourceManager.GetObject("OfflineMap"))
                        Else
                            imageListLarge.Images.Add(bmp)
                        End If
                    Else
                        imageListLarge.Images.Add(My.Resources.ResourceManager.GetObject("OfflineMap"))
                    End If
                    Num = X

                End If


                ' Create items and subitems for each item.
                Dim item1 As New ListViewItem(RegionClass.RegionName(X), Num)
                ' Place a check mark next to the item.
                item1.Checked = RegionClass.RegionEnabled(X)
                item1.SubItems.Add(RegionClass.GroupName(X).ToString)
                item1.SubItems.Add(RegionClass.AvatarCount(X).ToString)
                item1.SubItems.Add(Letter)
                item1.SubItems.Add(RegionClass.CoordX(X).ToString)
                item1.SubItems.Add(RegionClass.CoordY(X).ToString)

                Dim size As String = ""
                If RegionClass.SizeX(X) = 256 Then
                    size = "1X1"
                ElseIf RegionClass.SizeX(X) = 512 Then
                    size = "2X2"
                ElseIf RegionClass.SizeX(X) = 768 Then
                    size = "3X3"
                ElseIf RegionClass.SizeX(X) = 1024 Then
                    size = "4X4"
                Else
                    size = RegionClass.SizeX(X).ToString
                End If
                item1.SubItems.Add(size)

                'Map
                If RegionClass.MapType(X).Length > 0 Then
                    item1.SubItems.Add(RegionClass.MapType(X))
                Else
                    item1.SubItems.Add(Form1.MySetting.MapType)
                End If

                ' physics
                Select Case RegionClass.Physics(X)
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
                    Case Else
                        Select Case Form1.MySetting.Physics
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
                            Case Else
                                item1.SubItems.Add("?")
                        End Select
                End Select

                'birds

                If RegionClass.Birds(X) = "True" Then
                    item1.SubItems.Add("Birds")
                Else
                    item1.SubItems.Add("")
                End If

                'Tides
                If RegionClass.Tides(X) = "True" Then
                    item1.SubItems.Add("Tides")
                Else
                    item1.SubItems.Add("")
                End If

                'teleport
                If RegionClass.Teleport(X) = "True" Then
                    item1.SubItems.Add("Teleport")
                Else
                    item1.SubItems.Add("")
                End If

                ListView1.Items.AddRange(New ListViewItem() {item1})

            Next

            'Assign the ImageList objects to the ListView.
            ListView1.LargeImageList = imageListLarge
            ListView1.SmallImageList = imageListSmall

            Me.ListView1.TabIndex = 0

            For i As Integer = 0 To ListView1.Items.Count - 1
                If ListView1.Items(i).Checked Then
                    ListView1.Items(i).ForeColor = SystemColors.ControlText
                Else
                    ListView1.Items(i).ForeColor = SystemColors.GrayText
                End If
            Next i

            ListView1.EndUpdate()
            ViewNotBusy = True
            UpdateView() = False

        Catch ex As Exception
            Form1.Log("Error: RegionList " & ex.Message)
        End Try

    End Sub

    Private Sub ShowAvatars()
        Try
            ViewNotBusy = False

            AvatarView.Show()
            ListView1.Hide()

            AvatarView.BeginUpdate()

            imageListLarge = New ImageList()
            If pixels = 0 Then pixels = 20
            imageListLarge.ImageSize = New Size(pixels, pixels)

            AvatarView.Clear()
            AvatarView.Items.Clear()

            AvatarView.Columns.Add("Agent", 200, HorizontalAlignment.Left)
            AvatarView.Columns.Add("Region", 200, HorizontalAlignment.Center)

            Try
                ' Create items and subitems for each item.
                Dim Index = 0
                Dim L As New Dictionary(Of String, String)
                L = Form1.MysqlConn.GetAgentList()

                If L Is Nothing Then
                Else
                    For Each Agent In L
                        Dim item1 As New ListViewItem(Agent.Key, Index)
                        item1.SubItems.Add(Agent.Value)
                        AvatarView.Items.AddRange(New ListViewItem() {item1})
                        Index = Index + 1
                    Next
                End If

                If Index = 0 Then
                    Dim item1 As New ListViewItem("No Avatars", 0)
                    AvatarView.Items.AddRange(New ListViewItem() {item1})
                End If

            Catch ex As Exception
                Dim item1 As New ListViewItem("No Avatars", 0)
                AvatarView.Items.AddRange(New ListViewItem() {item1})
            End Try

            Me.AvatarView.TabIndex = 0
            AvatarView.EndUpdate()
            AvatarView.Show()

            ViewNotBusy = True
            UpdateView() = False
        Catch ex As Exception
            Form1.Log("Error: RegionList " & ex.Message)
        End Try


    End Sub

    Private Function LoadImage(url As String) As Image
        Dim bmp As Bitmap = Nothing
        Dim request As System.Net.WebRequest = System.Net.WebRequest.Create(url)
        Try
            Dim response As System.Net.WebResponse = request.GetResponse()
            Dim responseStream As System.IO.Stream = response.GetResponseStream()
            bmp = New Bitmap(responseStream)

            'Dim s = bmp.Size
            'Debug.Print(s.Width.ToString + ":" + s.Height.ToString)

            responseStream.Dispose()
        Catch ex As Exception
            Form1.Log("Maps: " + ex.Message + ":" + url)
        End Try

        Return bmp

    End Function


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        RegionClass.GetAllRegions()
        LoadMyListView()

    End Sub
    Private Sub ListCLick(sender As Object, e As EventArgs) Handles ListView1.Click
        Dim regions As ListView.SelectedListViewItemCollection = Me.ListView1.SelectedItems
        Dim item As ListViewItem

        For Each item In regions
            Dim RegionName = item.SubItems(0).Text
            Dim checked As Boolean = item.Checked
            Debug.Print("Clicked row " + RegionName)
            Dim R = RegionClass.FindRegionByName(RegionName)
            If R >= 0 Then
                StartStopEdit(checked, R, RegionName)
            End If
        Next

        UpdateView() = True
    End Sub

    Private Sub AvatarView_Click(sender As Object, e As EventArgs) Handles AvatarView.Click
        Dim regions As ListView.SelectedListViewItemCollection = Me.AvatarView.SelectedItems
        Dim item As ListViewItem

        For Each item In regions
            Dim RegionName = item.SubItems(1).Text
            Debug.Print("Clicked row " + RegionName)
            Dim R = RegionClass.FindRegionByName(RegionName)
            If R >= 0 Then
                Dim webAddress As String = "hop://" & Form1.MySetting.DNSName & ":" & Form1.MySetting.HttpPort & "/" & RegionName
                Process.Start(webAddress)
            End If
        Next

        UpdateView() = True
    End Sub


    Private Sub StartStopEdit(checked As Boolean, n As Integer, RegionName As String)

        ' show it, stop it, start it, or edit it
        Dim hwnd = Form1.getHwnd(RegionClass.GroupName(n))
        Form1.ShowDOSWindow(hwnd, Form1.SHOW_WINDOW.SW_RESTORE)

        Dim Choices As New FormRegionPopup
        Dim chosen As String
        Choices.init(RegionName)
        Choices.ShowDialog()
        Try
            ' Read the chosen sim name
            chosen = Choices.Choice()
            If chosen = "Start" Then

                ' it was stopped, and off, so we start up
                If Not Form1.StartMySQL() Then
                    Form1.ProgressBar1.Value = 0
                    Form1.ProgressBar1.Visible = True
                    Form1.Print("Stopped")
                End If
                Form1.Start_Robust()
                Form1.Log("Starting " + RegionClass.RegionName(n))
                Form1.CopyOpensimProto(RegionClass.RegionName(n))
                Form1.Boot(RegionClass.RegionName(n))
                UpdateView() = True ' force a refresh

            ElseIf chosen = "Stop" Then

                For Each num In RegionClass.RegionListByGroupNum(RegionClass.GroupName(n))
                    ' Ask before killing any people
                    If RegionClass.AvatarCount(num) > 0 Then
                        Dim response As MsgBoxResult

                        If RegionClass.AvatarCount(num) = 1 Then
                            response = MsgBox("There is one avatar in " + RegionClass.RegionName(num) + ".  Do you still want to stop it?", vbYesNo)
                        Else
                            response = MsgBox("There are " + RegionClass.AvatarCount(num).ToString + " avatars in " + RegionClass.RegionName(num) + ".  Do you still want to stop it?", vbYesNo)
                        End If
                        If response = vbYes Then
                            StopRegionNum(num)
                        End If
                    Else
                        StopRegionNum(num)
                    End If
                Next
                UpdateView = True ' make form refresh


            ElseIf chosen = "Edit" Then

                Dim RegionForm As New FormRegion
                RegionForm.Init(RegionClass.RegionName(n))
                RegionForm.Activate()
                RegionForm.Visible = True
                RegionForm.Select()
                UpdateView = True ' make form refresh

            ElseIf chosen = "Recycle" Then

                UpdateView = True ' make form refresh

                Dim h As IntPtr = Form1.getHwnd(RegionClass.GroupName(n))
                Form1.ShowDOSWindow(hwnd, Form1.SHOW_WINDOW.SW_RESTORE)

                Form1.ConsoleCommand(RegionClass.GroupName(n), "q{ENTER}" + vbCrLf)

                Form1.Print("Shutdown " + RegionClass.GroupName(n))

                Form1.gRestartNow = True

                ' shut down all regions in the DOS box

                For Each Y In RegionClass.RegionListByGroupNum(RegionClass.GroupName(n))
                    RegionClass.Timer(Y) = RegionMaker.REGION_TIMER.STOPPED
                    RegionClass.Status(n) = RegionMaker.SIM_STATUS.RecyclingDown ' request a recycle.
                Next

            End If

            If chosen.Length > 0 Then
                Choices.Dispose()
            End If
        Catch ex As Exception
            chosen = ""
        End Try


    End Sub

    Private Sub StopRegionNum(num As Integer)

        Form1.Log("Region:Stopping Region " + RegionClass.RegionName(num))
        If Form1.ConsoleCommand(RegionClass.GroupName(num), "q{ENTER}" + vbCrLf) Then


            RegionClass.Status(num) = RegionMaker.SIM_STATUS.ShuttingDown
        Else
            RegionClass.Status(num) = RegionMaker.SIM_STATUS.Stopped
        End If

    End Sub

    Private Sub ListView1_ItemCheck1(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles ListView1.ItemCheck

        Dim Item As ListViewItem = ListView1.Items.Item(e.Index)
        Dim n As Integer = RegionClass.FindRegionByName(Item.Text)
        If n = -1 Then Return
        Dim GroupName = RegionClass.GroupName(n)
        For Each X In RegionClass.RegionListByGroupNum(GroupName)
            If ViewNotBusy Then
                If (e.CurrentValue = CheckState.Unchecked) Then
                    RegionClass.RegionEnabled(X) = True
                    ' and region file on disk
                    Form1.MySetting.LoadOtherIni(RegionClass.RegionPath(X), ";")
                    Form1.MySetting.SetOtherIni(RegionClass.RegionName(X), "Enabled", "true")
                    Form1.MySetting.SaveOtherINI()
                ElseIf (e.CurrentValue = CheckState.Checked) Then
                    RegionClass.RegionEnabled(X) = False
                    ' and region file on disk
                    Form1.MySetting.LoadOtherIni(RegionClass.RegionPath(X), ";")
                    Form1.MySetting.SetOtherIni(RegionClass.RegionName(X), "Enabled", "false")
                    Form1.MySetting.SaveOtherINI()
                End If
            End If
        Next


        UpdateView() = True ' force a refresh

    End Sub

    ' ColumnClick event handler.
    Private Sub ColumnClick(ByVal o As Object, ByVal e As ColumnClickEventArgs)

        ListView1.SuspendLayout()
        Me.ListView1.Sorting = SortOrder.None
        ' Set the ListViewItemSorter property to a new ListViewItemComparer 
        ' object. Setting this property immediately sorts the 
        ' ListView using the ListViewItemComparer object.
        Me.ListView1.ListViewItemSorter = New ListViewItemComparer(e.Column)
        ListView1.ResumeLayout()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles ViewButton.Click

        ' It may be busy refreshing so lets wait
        While Not ViewNotBusy
            Form1.Sleep(100)
            Application.DoEvents()
        End While

        Debug.Print(Form1.MySetting.MapType)
        If Form1.MySetting.MapType = "None" And TheView = ViewType.Maps Then TheView = ViewType.Avatars

        TheView = TheView + 1
        If TheView > ViewType.Avatars Then TheView = ViewType.Maps

        If TheView = ViewType.Icons Then     '  Just an icon
            ListView1.View = View.SmallIcon
            ListView1.Show()
            AvatarView.Hide()
            ListView1.CheckBoxes = False
            Timer1.Start()
        ElseIf TheView = ViewType.Maps Then ' Maps
            ListView1.View = View.LargeIcon
            ListView1.Show()
            AvatarView.Hide()
            ListView1.CheckBoxes = False
            Timer1.Stop()
        ElseIf TheView = ViewType.Details Then ' Grid list
            ListView1.View = View.Details
            ListView1.Show()
            AvatarView.Hide()
            ListView1.CheckBoxes = True
            Timer1.Start()
        End If


        LoadMyListView()

    End Sub

    Private Sub Addregion_Click(sender As Object, e As EventArgs) Handles Addregion.Click

        Dim RegionForm As New FormRegion
        RegionForm.Init("")
        RegionForm.Activate()
        RegionForm.Visible = True

    End Sub

#End Region

#Region "DragDrop"

    Private Sub ListView1_DragEnter(sender As System.Object, e As System.Windows.Forms.DragEventArgs) Handles ListView1.DragEnter

        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        End If

    End Sub

    Private Sub ListView1_DragDrop(sender As System.Object, e As System.Windows.Forms.DragEventArgs) Handles ListView1.DragDrop

        Dim files() As String = CType(e.Data.GetData(DataFormats.FileDrop), String())

        Dim dirpathname = ""

        dirpathname = PickGroup()
        If dirpathname = "" Then
            Form1.PrintFast("Aborted")
            Return
        End If


        For Each pathname As String In files
            pathname = pathname.Replace("\", "/")
            Dim extension As String = Path.GetExtension(pathname)
            extension = Mid(extension, 2, 5)
            If extension.ToLower = "ini" Then
                Dim filename = Path.GetFileNameWithoutExtension(pathname)
                Dim i = RegionClass.FindRegionByName(filename)
                If i >= 0 Then
                    MsgBox("Region name " + filename + " already exists", vbInformation, "Info")
                    Return
                End If

                If dirpathname = "" Then dirpathname = filename

                Dim NewFilepath = Form1.gPath & "bin\Regions\" + dirpathname + "\Region\"
                If Not Directory.Exists(NewFilepath) Then
                    Directory.CreateDirectory(Form1.gPath & "bin\Regions\" + dirpathname + "\Region")
                End If

                File.Copy(pathname, Form1.gPath & "bin\Regions\" + dirpathname + "\Region\" + filename + ".ini")

            Else
                Form1.Print("Unrecognized file type" + extension + ". Drag and drop any Region.ini files to add them to the system.")
            End If
        Next
        RegionClass.GetAllRegions()
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
            If chosen.Length > 0 Then
                Chooseform.Dispose()
            End If
        Catch ex As Exception
            chosen = ""
        End Try
        Return chosen

    End Function

    Private Sub RegionHelp_Click(sender As Object, e As EventArgs) Handles RegionHelp.Click

        Form1.Help("RegionList")

    End Sub

    Private Sub AllNone_CheckedChanged(sender As Object, e As EventArgs) Handles AllNome.CheckedChanged

        For Each X As ListViewItem In ListView1.Items
            If ItemsAreChecked Then
                X.Checked = CType(CheckState.Unchecked, Boolean)
            Else
                X.Checked = CType(CheckState.Checked, Boolean)
            End If
        Next

        If ItemsAreChecked Then
            ItemsAreChecked = False
        Else
            ItemsAreChecked = True
        End If
        UpdateView = True ' make form refresh

    End Sub

    Private Sub RunAllButton_Click(sender As Object, e As EventArgs) Handles RunAllButton.Click

        Form1.Start_Opensimulator()

    End Sub

    Private Sub StopAllButton_Click(sender As Object, e As EventArgs) Handles StopAllButton.Click
        Form1.KillAll()
    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles RestartButton.Click

        For Each X As Integer In RegionClass.RegionNumbers

            If Form1.OpensimIsRunning() _
                And RegionClass.RegionEnabled(X) _
                And Not (RegionClass.Status(X) = RegionMaker.SIM_STATUS.ShuttingDown _
                Or RegionClass.Status(X) = RegionMaker.SIM_STATUS.RecyclingDown) Then

                Dim hwnd = Form1.GetHwnd(RegionClass.GroupName(X))
                Form1.ShowDOSWindow(hwnd, Form1.SHOW_WINDOW.SW_RESTORE)

                Form1.ConsoleCommand(RegionClass.GroupName(X), "q{ENTER}" + vbCrLf)

                Form1.PrintFast("Restarting " & RegionClass.GroupName(X))
                ' shut down all regions in the DOS box
                For Each Y In RegionClass.RegionListByGroupNum(RegionClass.GroupName(X))
                    RegionClass.Timer(Y) = RegionMaker.REGION_TIMER.STOPPED
                    RegionClass.Status(Y) = RegionMaker.SIM_STATUS.RecyclingDown
                Next
                Form1.gRestartNow = True

                UpdateView = True ' make form refresh

            End If

        Next
        UpdateView = True ' make form refresh
    End Sub

    Private Sub HelpToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HelpToolStripMenuItem.Click
        Form1.Help("RegionList")
    End Sub

#End Region

End Class

#Region "Compare"
' Implements the manual sorting of items by columns.
Class ListViewItemComparer
    Implements IComparer
    Private col As Integer

    Public Sub New()
        col = 1
    End Sub

    Public Sub New(ByVal column As Integer)
        col = column
    End Sub

    Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare

        Return [String].Compare(CType(x, ListViewItem).SubItems(col).Text, CType(y, ListViewItem).SubItems(col).Text)

    End Function

End Class

#End Region
