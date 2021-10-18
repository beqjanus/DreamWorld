#Region "Copyright AGPL3.0"

' Copyright Outworldz, LLC.
' AGPL3.0  https://opensource.org/licenses/AGPL

#End Region

Public Class FormServerType

#Region "Private Fields"

    Dim Changed As Boolean
    Dim initted As Boolean
    Dim ServerType As String = ""

#End Region

#Region "ScreenSize"

    Private ReadOnly Handler As New EventHandler(AddressOf Resize_page)

    'The following detects  the location of the form in screen coordinates
    Private _screenPosition As ClassScreenpos

    Public Property ScreenPosition As ClassScreenpos
        Get
            Return _screenPosition
        End Get
        Set(value As ClassScreenpos)
            _screenPosition = value
        End Set
    End Property

    Private Sub Resize_page(ByVal sender As Object, ByVal e As System.EventArgs)
        'Me.Text = "Form screen position = " + Me.Location.ToString
        ScreenPosition.SaveXY(Me.Left, Me.Top)
    End Sub

    Private Sub SetScreen()

        ScreenPosition = New ClassScreenpos(Me.Name)
        AddHandler ResizeEnd, Handler
        Dim xy As List(Of Integer) = ScreenPosition.GetXY()
        Me.Left = xy.Item(0)
        Me.Top = xy.Item(1)
    End Sub

#End Region

#Region "Private Methods"

    Private Sub Form_exit() Handles Me.Closed

        If Changed Then
            Dim result = MsgBox(My.Resources.Save_changes_word, MsgBoxStyle.YesNo Or MsgBoxStyle.MsgBoxSetForeground, My.Resources.Save_changes_word)
            If result = vbYes Then
                SaveAll()
                DoGridCommon()
            End If
        End If

    End Sub

    Private Sub Loaded(sender As Object, e As EventArgs) Handles Me.Load

        GridRegionButton.Text = Global.Outworldz.My.Resources.Region_Server_word
        GridServerButton.Text = Global.Outworldz.My.Resources.Grid_Server_With_Robust_word
        GroupBox1.Text = Global.Outworldz.My.Resources.Server_Type_word
        HelpToolStripMenuItem.Image = Global.Outworldz.My.Resources.question_and_answer
        HelpToolStripMenuItem.Text = Global.Outworldz.My.Resources.Help_word
        MetroRadioButton2.Text = Global.Outworldz.My.Resources.MetroOrg
        osGridRadioButton1.Text = Global.Outworldz.My.Resources.OSGrid_Region_Server

        SetScreen()

        Select Case Settings.ServerType
            Case RobustServerName
                GridServerButton.Checked = True
            Case RegionServerName
                GridRegionButton.Checked = True
            Case OsgridServer
                osGridRadioButton1.Checked = True
            Case MetroServer
                MetroRadioButton2.Checked = True
            Case Else
                GridServerButton.Checked = True
        End Select

        initted = True

        HelpOnce("ServerType")
    End Sub

    Private Sub SaveAll()

        Settings.ServerType = ServerType
        Settings.SaveSettings()
        SetServerType()

        Changed = False ' do not trigger the save a second time

    End Sub

#End Region

#Region "Radio Buttons"

    Private Sub GridRegionButton_CheckedChanged_1(sender As Object, e As EventArgs) Handles GridRegionButton.CheckedChanged

        If Not initted Then Return
        If Not GridRegionButton.Checked Then Return

        ServerType = RegionServerName
        ' do not override for grid servers
        Changed = True

    End Sub

    Private Sub GridServerButton_CheckedChanged_1(sender As Object, e As EventArgs) Handles GridServerButton.CheckedChanged

        If Not initted Then Return
        If Not GridServerButton.Checked Then Return

        Changed = True
        ServerType = RobustServerName

    End Sub

    Private Sub HelpToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HelpToolStripMenuItem.Click
        HelpManual("ServerType")
    End Sub

    Private Sub MetroRadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles MetroRadioButton2.CheckedChanged

        If Not initted Then Return
        If Not MetroRadioButton2.Checked Then Return

        ServerType = MetroServer

        Changed = True

    End Sub

    Private Sub OsGridRadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles osGridRadioButton1.CheckedChanged

        If Not initted Then Return
        If Not osGridRadioButton1.Checked Then Return

        ServerType = OsgridServer

        Changed = True

    End Sub

#End Region

End Class
