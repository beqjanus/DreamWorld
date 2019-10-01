Public Class FormServerType
    Dim initted As Boolean = False
    Dim Changed As Boolean = False
    Dim ServerType As String = ""
    Dim DNSName As String = ""
    Dim BaseHostName As String = ""

#Region "ScreenSize"

    'The following detects  the location of the form in screen coordinates
    Private _screenPosition As ScreenPos

    Private Handler As New EventHandler(AddressOf Resize_page)

    Public Property ScreenPosition As ScreenPos
        Get
            Return _screenPosition
        End Get
        Set(value As ScreenPos)
            _screenPosition = value
        End Set
    End Property

    Private Sub Resize_page(ByVal sender As Object, ByVal e As System.EventArgs)
        'Me.Text = "Form screen position = " + Me.Location.ToString
        ScreenPosition.SaveXY(Me.Left, Me.Top)
    End Sub

    Private Sub SetScreen()
        Me.Show()
        ScreenPosition = New ScreenPos(Me.Name)
        AddHandler ResizeEnd, Handler
        Dim xy As List(Of Integer) = ScreenPosition.GetXY()
        Me.Left = xy.Item(0)
        Me.Top = xy.Item(1)
    End Sub

#End Region

    Private Sub Loaded(sender As Object, e As EventArgs) Handles Me.Load
        SetScreen()

        Select Case Form1.Settings.ServerType
            Case "Robust"
                GridServerButton.Checked = True
            Case "Region"
                GridRegionButton.Checked = True
            Case "OsGrid"
                osGridRadioButton1.Checked = True
            Case "Metro"
                MetroRadioButton2.Checked = True
            Case Else
                GridServerButton.Checked = True
        End Select

        initted = True

        Form1.HelpOnce("ServerType")
    End Sub

    Private Sub Form_exit() Handles Me.Closed
        If Changed Then
            Dim result = MsgBox("Do you wish to save your changes?", vbYesNo)
            If result = vbYes Then
                Form1.PropViewedSettings = True
                SaveAll()
            End If
        End If
    End Sub

    Private Sub SaveAll()

        Form1.Settings.ServerType = ServerType
        Form1.Settings.BaseHostName = BaseHostName
        Form1.Settings.DNSName = DNSName

        Form1.PropViewedSettings = True
        Form1.Settings.SaveSettings()
        Changed = False ' do not trigger the save a second time

    End Sub

#Region "Radio Buttons"

    Private Sub GridServerButton_CheckedChanged_1(sender As Object, e As EventArgs) Handles GridServerButton.CheckedChanged

        If Not initted Then Return
        If Not GridServerButton.Checked Then Return

        Changed = True
        ServerType = "Robust"

    End Sub

    Private Sub GridRegionButton_CheckedChanged_1(sender As Object, e As EventArgs) Handles GridRegionButton.CheckedChanged

        If Not initted Then Return
        If Not GridRegionButton.Checked Then Return

        ServerType = "Region"
        ' do not override for grid servers
        Changed = True

    End Sub

    Private Sub OsGridRadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles osGridRadioButton1.CheckedChanged

        If Not initted Then Return
        If Not osGridRadioButton1.Checked Then Return

        ServerType = "OsGrid"
        DNSName = "hg.osgrid.org"
        BaseHostName = "hg.osgrid.org"

        Changed = True

    End Sub

    Private Sub MetroRadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles MetroRadioButton2.CheckedChanged

        If Not initted Then Return
        If Not MetroRadioButton2.Checked Then Return

        ServerType = "Metro"
        DNSName = "hg.metro.land"
        BaseHostName = "hg.metro.land"

        Changed = True

    End Sub

    Private Sub ServerTypeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ServerTypeToolStripMenuItem.Click

        Form1.Help("ServerType")

    End Sub

    Private Sub SaveButton_Click(sender As Object, e As EventArgs) Handles SaveButton.Click

        SaveAll()
        Close()

    End Sub

#End Region

End Class