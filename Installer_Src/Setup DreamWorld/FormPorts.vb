#Region "Copyright AGPL3.0"

' Copyright Outworldz, LLC.
' AGPL3.0  https://opensource.org/licenses/AGPL

#End Region

Imports System.Text.RegularExpressions

Public Class FormPorts

#Region "Private Fields"

    Dim initted As Boolean

#End Region

#Region "FormPos"

    Private ReadOnly Handler As New EventHandler(AddressOf Resize_page)
    Private _screenPosition As ClassScreenpos

    Public Property ScreenPosition As ClassScreenpos
        Get
            Return _screenPosition
        End Get
        Set(value As ClassScreenpos)
            _screenPosition = value
        End Set
    End Property

    'The following detects  the location of the form in screen coordinates
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

    Private Sub IsClosed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Closed

        Settings.SaveSettings()

    End Sub

    Private Sub Loaded(sender As Object, e As EventArgs) Handles Me.Load

        GroupBoxA.Text = Global.Outworldz.My.Resources.Ports
        Label26.Text = Global.Outworldz.My.Resources.Region_Port_Start
        Label4.Text = Global.Outworldz.My.Resources.Http_Port_word
        Label5.Text = Global.Outworldz.My.Resources.Diagnostics_port_word
        Label7.Text = Global.Outworldz.My.Resources.Private_Port_Word
        MaxP.Text = Global.Outworldz.My.Resources.Highest_Used_word
        MenuStrip2.Text = Global.Outworldz.My.Resources._0
        OverrideNameLabel.Text = Global.Outworldz.My.Resources.External
        Text = Global.Outworldz.My.Resources.Region_Ports_word
        ToolStripMenuItem30.Image = Global.Outworldz.My.Resources.question_and_answer
        ToolStripMenuItem30.Text = Global.Outworldz.My.Resources.Help_word
        ToolTip1.SetToolTip(DiagnosticPort, Global.Outworldz.My.Resources.Default_8001_word)
        ToolTip1.SetToolTip(ExternalHostName, Global.Outworldz.My.Resources.External_text)
        ToolTip1.SetToolTip(FirstRegionPort, Global.Outworldz.My.Resources.Default_8004_word)
        ToolTip1.SetToolTip(HTTPPort, Global.Outworldz.My.Resources.Default_8002_word)
        ToolTip1.SetToolTip(OverrideNameLabel, Global.Outworldz.My.Resources.External_text)
        ToolTip1.SetToolTip(PrivatePort, Global.Outworldz.My.Resources.Default_8003_word)
        ToolTip1.SetToolTip(uPnPEnabled, Global.Outworldz.My.Resources.UPnP_Enabled_text)
        Upnp.Image = Global.Outworldz.My.Resources.about
        uPnPEnabled.Text = Global.Outworldz.My.Resources.UPnP_Enabled_word

        SetScreen()

        FirstRegionPort.Text = CStr(Settings.FirstRegionPort())
        MaxP.Text = Global.Outworldz.My.Resources.Highest_Used_word & " " & CStr(LargestPort())

        uPnPEnabled.Checked = Settings.UPnPEnabled

        'ports
        DiagnosticPort.Text = CStr(Settings.DiagnosticPort)
        PrivatePort.Text = CStr(Settings.PrivatePort)
        HTTPPort.Text = CStr(Settings.HttpPort)

        ' only used for region servers that are not behind a NAT

        ExternalHostName.Text = Settings.OverrideName
        If Settings.ServerType <> RobustServerName Then
            ExternalHostName.Enabled = True
        Else
            ExternalHostName.Text = ""
            ExternalHostName.Enabled = False
        End If

        HelpOnce("Ports")
        initted = True

    End Sub

    Private Sub UPnPEnabled_CheckedChanged(sender As Object, e As EventArgs) Handles uPnPEnabled.CheckedChanged

        If Not initted Then Return
        Settings.UPnPEnabled = uPnPEnabled.Checked
        Settings.SaveSettings()

    End Sub

#End Region

#Region "Ports"

    Private Sub DiagnosticPort_TextChanged(sender As Object, e As EventArgs) Handles DiagnosticPort.TextChanged

        If Not initted Then Return

        Dim digitsOnly = New Regex("[^\d]")
        DiagnosticPort.Text = digitsOnly.Replace(DiagnosticPort.Text, "")

        Settings.DiagnosticPort = CInt("0" & DiagnosticPort.Text)
        Settings.SaveSettings()
        CheckDefaultPorts()

    End Sub

    Private Sub ExternalHostName_TextChanged(sender As Object, e As EventArgs) Handles ExternalHostName.TextChanged

        If Not initted Then Return

        Settings.OverrideName = ExternalHostName.Text

    End Sub

    Private Sub FirstRegionPort_TextChanged_1(sender As Object, e As EventArgs) Handles FirstRegionPort.TextChanged

        If Not initted Then Return

        Dim digitsOnly = New Regex("[^\d]")
        FirstRegionPort.Text = digitsOnly.Replace(FirstRegionPort.Text, "")
        Settings.FirstRegionPort() = CInt("0" & FirstRegionPort.Text)
        Settings.SaveSettings()

        FirstRegionPort.Text = CStr(Settings.FirstRegionPort())
        MaxP.Text = Global.Outworldz.My.Resources.Highest_Used_word & " " & CStr(LargestPort())

    End Sub

    Private Sub HTTPPort_TextChanged(sender As Object, e As EventArgs) Handles HTTPPort.TextChanged

        If Not initted Then Return

        Dim digitsOnly = New Regex("[^\d]")
        HTTPPort.Text = digitsOnly.Replace(HTTPPort.Text, "")
        Settings.HttpPort = CInt("0" & HTTPPort.Text)
        Settings.SaveSettings()
        CheckDefaultPorts()

    End Sub

    Private Sub PrivatePort_TextChanged(sender As Object, e As EventArgs) Handles PrivatePort.TextChanged

        If Not initted Then Return

        Dim digitsOnly = New Regex("[^\d]")
        PrivatePort.Text = digitsOnly.Replace(PrivatePort.Text, "")
        Settings.PrivatePort = CInt("0" & PrivatePort.Text)
        Settings.SaveSettings()
        CheckDefaultPorts()

    End Sub

    Private Sub ToolStripMenuItem30_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem30.Click
        HelpManual("Ports")
    End Sub

    Private Sub Upnp_Click(sender As Object, e As EventArgs) Handles Upnp.Click

        HelpManual("Ports")

    End Sub

#End Region

End Class
