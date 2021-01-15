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

Public Class FormRegionPopup

#Region "Private Fields"

    Private _RegionName As String = ""
    Dim gPick As String = ""

#End Region

#Region "ScreenSize"

    Private ReadOnly Handler As New EventHandler(AddressOf Resize_page)
    Private _screenPosition As ScreenPos

    Public Property ScreenPosition As ScreenPos
        Get
            Return _screenPosition
        End Get
        Set(value As ScreenPos)
            _screenPosition = value
        End Set
    End Property

    'The following detects  the location of the form in screen coordinates
    Private Sub Resize_page(ByVal sender As Object, ByVal e As System.EventArgs)
        'Me.Text = "Form screen position = " + Me.Location.ToString
        ScreenPosition.SaveXY(Me.Left, Me.Top)
    End Sub

    Private Sub SetScreen()
        Me.Show()
        ScreenPosition = New ScreenPos(MyBase.Name)
        AddHandler ResizeEnd, Handler
        Dim xy As List(Of Integer) = ScreenPosition.GetXY()
        Me.Left = xy.Item(0)
        Me.Top = xy.Item(1)
    End Sub

#End Region

#Region "Public Methods"

    Public Function Choice() As String
        Return gPick
    End Function

    Public Sub Init(RegionName As String)

        If Settings.ServerType = "Robust" Then
            ViewMapButton.Enabled = True
        Else
            ViewMapButton.Enabled = False
        End If

        _RegionName = RegionName

        Dim RegionUUID As String = PropRegionClass.FindRegionByName(RegionName)
        Me.Text = RegionName
        GroupBox1.Text = PropRegionClass.GroupName(RegionUUID)

        If Not PropRegionClass.RegionEnabled(RegionUUID) Then
            ShowConsoleButton.Enabled = False
            StatsButton1.Enabled = False
            StartButton.Enabled = False
            StopButton.Enabled = True
            SaveOAR.Enabled = False
            Teleport.Enabled = False
            LoadOAR.Enabled = False
            MsgButton.Enabled = False
            EditButton1.Enabled = True
        Else

            If PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.Suspended Then
                'TODO: unsuspend region

                ShowConsoleButton.Enabled = True
                StatsButton1.Enabled = False
                StartButton.Enabled = True
                StopButton.Enabled = True
                SaveOAR.Enabled = True
                LoadOAR.Enabled = True
                Teleport.Enabled = True
                EditButton1.Enabled = False
                MsgButton.Enabled = False

            End If

            If PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.Booted Then
                ShowConsoleButton.Enabled = True
                StatsButton1.Enabled = True
                StartButton.Enabled = False
                StopButton.Enabled = True
                SaveOAR.Enabled = True
                LoadOAR.Enabled = True
                Teleport.Enabled = True
                EditButton1.Enabled = True
                MsgButton.Enabled = True
            End If

            If PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.RecyclingDown Or
            PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.ShuttingDown Then
                ShowConsoleButton.Enabled = True
                StatsButton1.Enabled = False
                StartButton.Enabled = False
                StopButton.Enabled = True
                LoadOAR.Enabled = False
                SaveOAR.Enabled = False
                Teleport.Enabled = False
                MsgButton.Enabled = False
                EditButton1.Enabled = True
            End If

            If PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.Booting Or
                PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.RecyclingUp Then
                ShowConsoleButton.Enabled = True
                StatsButton1.Enabled = False
                StartButton.Enabled = False
                StopButton.Enabled = True
                SaveOAR.Enabled = False
                LoadOAR.Enabled = False
                Teleport.Enabled = False
                MsgButton.Enabled = False
                EditButton1.Enabled = False
            End If

            ' stopped
            If PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.Stopped Then
                ShowConsoleButton.Enabled = False
                StatsButton1.Enabled = False
                StartButton.Enabled = True
                StopButton.Enabled = True
                SaveOAR.Enabled = False
                LoadOAR.Enabled = False
                Teleport.Enabled = False
                EditButton1.Enabled = True
            End If
        End If

        BringToFront()

    End Sub

#End Region

#Region "Private Methods"

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Teleport.Click
        gPick = "Teleport"
        DialogResult = DialogResult.OK
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles ViewLog.Click

        Dim UUID = PropRegionClass.FindRegionByName(_RegionName)
        Dim GroupName As String = PropRegionClass.GroupName(UUID)
        Dim path = IO.Path.Combine(Settings.CurrentDirectory, "Outworldzfiles\Opensim\bin\Regions\" & GroupName & "\OpenSim.log")

        Dim Baretail As New Process
        Baretail.StartInfo.UseShellExecute = True ' so we can redirect streams
        Baretail.StartInfo.FileName = "baretail.exe"
        Baretail.StartInfo.Arguments = """" & path & """"
        Baretail.StartInfo.WorkingDirectory = Settings.CurrentDirectory
        Baretail.StartInfo.WindowStyle = ProcessWindowStyle.Normal
        Try
            Baretail.Start()
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles StatsButton1.Click

        Dim RegionNum = PropRegionClass.FindRegionByName(_RegionName)
        Dim RegionPort = PropRegionClass.GroupPort(RegionNum)
        Dim webAddress As String = "http://" & Settings.PublicIP & ":" & CType(RegionPort, String) & "/SStats/"
        Try
            Process.Start(webAddress)
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try
    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles ShowConsoleButton.Click
        gPick = "Console"
        DialogResult = DialogResult.OK
    End Sub

    Private Sub EditButton1_Click(sender As Object, e As EventArgs) Handles EditButton1.Click
        gPick = "Edit"
        DialogResult = DialogResult.OK
    End Sub

    Private Sub Popup_load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ViewLog.Image = Global.Outworldz.My.Resources.document_view
        ViewLog.Text = Global.Outworldz.My.Resources.View_Log_word
        EditButton1.Image = Global.Outworldz.My.Resources.document_dirty
        EditButton1.Text = Global.Outworldz.My.Resources.Edit_word
        GroupBox1.Text = Global.Outworldz.My.Resources.Region_Controls
        Restart.Text = Global.Outworldz.My.Resources.Restart_word
        ShowConsoleButton.Text = Global.Outworldz.My.Resources.View_Console_word
        StartButton.Text = Global.Outworldz.My.Resources.Start_word
        Teleport.Text = Global.Outworldz.My.Resources.Teleport_word
        StatsButton1.Text = Global.Outworldz.My.Resources.View_Statistics_Word
        StopButton.Text = Global.Outworldz.My.Resources.Stop_word
        ViewMapButton.Text = Global.Outworldz.My.Resources.View_Map_word
        LoadOAR.Text = Global.Outworldz.My.Resources.Load_Region_OAR
        SaveOAR.Text = Global.Outworldz.My.Resources.Save_Region_OAR_word
        MsgButton.Text = Global.Outworldz.My.Resources.Send_Alert_Message_word
        SetScreen()

    End Sub

    Private Sub RecycleButton2_Click(sender As Object, e As EventArgs) Handles SaveOAR.Click
        gPick = "Save"
        DialogResult = DialogResult.OK
    End Sub

    Private Sub StartButton3_Click(sender As Object, e As EventArgs) Handles StartButton.Click
        gPick = "Start"
        DialogResult = DialogResult.OK
    End Sub

    Private Sub StopButton1_Click(sender As Object, e As EventArgs) Handles StopButton.Click
        gPick = "Stop"
        DialogResult = DialogResult.OK
    End Sub

    Private Sub ViewMapButton_Click(sender As Object, e As EventArgs) Handles ViewMapButton.Click

        FormSetup.VarChooser(_RegionName, False, False)

    End Sub

    Private Sub Load_Click(sender As Object, e As EventArgs) Handles LoadOAR.Click
        gPick = "Load"
        DialogResult = DialogResult.OK
    End Sub

    Private Sub Button2_Click_2(sender As Object, e As EventArgs) Handles Restart.Click
        gPick = "Restart"
        DialogResult = DialogResult.OK
    End Sub

    Private Sub MsgButton_Click(sender As Object, e As EventArgs) Handles MsgButton.Click

        If Not PropOpensimIsRunning() Then
            TextPrint(My.Resources.Not_Running)
            Return
        End If

        Dim Message = InputBox(My.Resources.What_to_say_2_region)
        Dim RegionUUID As String = PropRegionClass.FindRegionByName(_RegionName)
        If RegionUUID.Length > 0 Then
            SendMessage(RegionUUID, Message)
        End If

    End Sub

#End Region

End Class
