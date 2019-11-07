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

Imports System.Text.RegularExpressions

Public Class FormPorts

#Region "Private Fields"

    Dim initted As Boolean

#End Region

#Region "FormPos"

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

    'The following detects  the location of the form in screen coordinates
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

#Region "Private Methods"

    Private Sub IsClosed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Closed

        Form1.PropViewedSettings = True
        Form1.Settings.SaveSettings()

    End Sub

    Private Sub Loaded(sender As Object, e As EventArgs) Handles Me.Load

        SetScreen()

        FirstRegionPort.Text = CStr(Form1.Settings.FirstRegionPort())
        MaxP.Text = "Highest used: " + Form1.PropMaxPortUsed.ToString(Form1.Invarient)
        uPnPEnabled.Checked = Form1.Settings.UPnPEnabled

        'ports
        DiagnosticPort.Text = CStr(Form1.Settings.DiagnosticPort)
        PrivatePort.Text = CStr(Form1.Settings.PrivatePort)
        HTTPPort.Text = CStr(Form1.Settings.HttpPort)

        ExternalHostName.Text = Form1.Settings.OverrideName

        Form1.HelpOnce("Ports")
        initted = True

    End Sub

    Private Sub UPnPEnabled_CheckedChanged(sender As Object, e As EventArgs) Handles uPnPEnabled.CheckedChanged

        If Not initted Then Return
        Form1.Settings.UPnPEnabled = uPnPEnabled.Checked
        Form1.Settings.SaveSettings()

    End Sub

#End Region

#Region "Ports"

    Private Sub DatabaseSetupToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DatabaseSetupToolStripMenuItem.Click
        Form1.Help("Ports")
    End Sub

    Private Sub DiagnosticPort_TextChanged(sender As Object, e As EventArgs) Handles DiagnosticPort.TextChanged

        If Not initted Then Return

        Dim digitsOnly As Regex = New Regex("[^\d]")
        DiagnosticPort.Text = digitsOnly.Replace(DiagnosticPort.Text, "")

        Form1.Settings.DiagnosticPort = CInt(DiagnosticPort.Text)
        Form1.Settings.SaveSettings()
        Form1.CheckDefaultPorts()

    End Sub

    Private Sub ExternalHostName_TextChanged(sender As Object, e As EventArgs) Handles ExternalHostName.TextChanged

        If Not initted Then Return

        Form1.Settings.OverrideName = ExternalHostName.Text

    End Sub

    Private Sub FirstRegionPort_TextChanged_1(sender As Object, e As EventArgs) Handles FirstRegionPort.TextChanged

        If Not initted Then Return

        Dim digitsOnly As Regex = New Regex("[^\d]")
        FirstRegionPort.Text = digitsOnly.Replace(FirstRegionPort.Text, "")
        Form1.Settings.FirstRegionPort() = CInt(FirstRegionPort.Text)
        Form1.Settings.SaveSettings()
        Form1.Settings.PortsChanged = True

    End Sub

    Private Sub HTTPPort_TextChanged(sender As Object, e As EventArgs) Handles HTTPPort.TextChanged

        If Not initted Then Return

        Dim digitsOnly As Regex = New Regex("[^\d]")
        HTTPPort.Text = digitsOnly.Replace(HTTPPort.Text, "")
        Form1.Settings.HttpPort = CInt(HTTPPort.Text)
        Form1.Settings.SaveSettings()
        Form1.CheckDefaultPorts()

    End Sub

    Private Sub PrivatePort_TextChanged(sender As Object, e As EventArgs) Handles PrivatePort.TextChanged

        If Not initted Then Return

        Dim digitsOnly As Regex = New Regex("[^\d]")
        PrivatePort.Text = digitsOnly.Replace(PrivatePort.Text, "")
        Form1.Settings.PrivatePort = CInt(PrivatePort.Text)
        Form1.Settings.SaveSettings()
        Form1.CheckDefaultPorts()

    End Sub

    Private Sub Upnp_Click(sender As Object, e As EventArgs) Handles Upnp.Click
        Form1.Help("Ports")
    End Sub

#End Region

End Class
