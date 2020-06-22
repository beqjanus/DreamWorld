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

Imports System.Net
Imports System.Text.RegularExpressions

Public Class FormDNSName

#Region "Private Fields"

    Dim changed As Boolean = False
    Dim initted As Boolean = False

#End Region

#Region "ScreenSize"

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

#Region "Load"

    Private Sub DNS_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetScreen()
        Me.Text = My.Resources.Dynamic_DNS_word
        DNSNameBox.Text = Form1.Settings.DNSName
        UniqueId.Text = Form1.Settings.MachineID()
        EnableHypergrid.Checked = Form1.Settings.EnableHypergrid
        SuitcaseCheckbox.Checked = Form1.Settings.Suitcase
        NextNameButton.Enabled = True

        Form1.HelpOnce("DNS")

        initted = True

    End Sub

#End Region

#Region "Buttons"

    Private Sub Closeme() Handles Me.Closed

        If changed Then
            Dim resp = MsgBox(My.Resources.Changes_Made, vbYesNo)
            If resp = vbYes Then SaveAll()
        End If

    End Sub

    Private Sub DynDNSPassword_Click(sender As Object, e As EventArgs) Handles DynDNSHelp.Click

        Form1.Help("DNS")

    End Sub

    Private Sub EnableHypergrid_CheckedChanged(sender As Object, e As EventArgs) Handles EnableHypergrid.CheckedChanged

        If Not initted Then Return
        Form1.Settings.EnableHypergrid = EnableHypergrid.Checked
        changed = True

    End Sub

    Private Sub HelpToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles HelpToolStripMenuItem1.Click
        Form1.Help("DNS")
    End Sub

    Private Sub NextNameButton_Click(sender As Object, e As EventArgs) Handles NextNameButton.Click

        NextNameButton.Text = My.Resources.Busy_word
        DNSNameBox.Text = String.Empty
        'Application.doevents()
        Dim newname = Form1.GetNewDnsName()
        NextNameButton.Text = My.Resources.Next1
        If newname.Length = 0 Then
            MsgBox(My.Resources.Please_enter, vbInformation, My.Resources.Info)
            NextNameButton.Enabled = False
        Else
            NextNameButton.Enabled = True
            DNSNameBox.Text = newname
        End If

        changed = True

    End Sub


    Private Sub SaveAll()

        NextNameButton.Text = My.Resources.Saving_word
        Dim address As System.Net.IPAddress = Nothing

        If DNSNameBox.Text.Length = 0 Then
            Form1.Settings.PublicIP = IP()
        Else
            Try
                If IPAddress.TryParse(DNSNameBox.Text, address) Then
                    Form1.Settings.PublicIP = IP()
                Else
                    Dim IP = Form1.GetHostAddresses(DNSNameBox.Text)
                End If
            Catch ex As ArgumentNullException
            End Try
        End If

        Form1.Settings.SaveSettings()
        changed = False ' suppress prompts
        Form1.PropViewedSettings = True
        Me.Close()

    End Sub

    Private Sub SaveButton_Click(sender As Object, e As EventArgs) Handles SaveButton1.Click

        SaveAll()

    End Sub

    Private Sub SuitcaseCheckbox_CheckedChanged(sender As Object, e As EventArgs) Handles SuitcaseCheckbox.CheckedChanged

        If Not initted Then Return
        Form1.Settings.Suitcase() = SuitcaseCheckbox.Checked

        If Not SuitcaseCheckbox.Checked Then
            MsgBox(My.Resources.Disabling_Suitcase)
        End If
        changed = True

    End Sub


    Private Sub TestButton1_Click(sender As Object, e As EventArgs) Handles TestButton1.Click

        NextNameButton.Text = My.Resources.Busy_word
        Form1.RegisterName(DNSNameBox.Text)

        Dim address As System.Net.IPAddress = Nothing
        If DNSNameBox.Text.Length = 0 Then
            Form1.Settings.PublicIP = IP()
        Else
            Try
                If IPAddress.TryParse(DNSNameBox.Text, address) Then
                    MsgBox(DNSNameBox.Text + " " & My.Resources.resolved & " " + IP(), vbInformation, My.Resources.Info)
                Else
                    Dim IP = Form1.GetHostAddresses(DNSNameBox.Text)
                    If IP.Length = 0 Then
                        MsgBox(My.Resources.Cannot_resolve_word & " " & DNSNameBox.Text, vbInformation, My.Resources.Error_word)
                    Else
                        MsgBox(DNSNameBox.Text + " " & My.Resources.resolved & IP, vbInformation, My.Resources.Info)
                    End If
                End If
            Catch ex As ArgumentNullException
            End Try
        End If

        NextNameButton.Text = My.Resources.Next1

    End Sub


    Private Sub TextBox1_LostFocus(sender As Object, e As EventArgs) Handles DNSNameBox.TextChanged

        If DNSNameBox.Text.Length > 0 Then

            DNSNameBox.Text = DNSNameBox.Text.Replace("http://", "")
            DNSNameBox.Text = DNSNameBox.Text.Replace("https://", "")

            DNSNameBox.Text = Regex.Replace(DNSNameBox.Text, ":\d+", "") ' no :8002 on end.

            Dim rgx As New Regex("[^a-zA-Z0-9\.\-]")
            DNSNameBox.Text = rgx.Replace(DNSNameBox.Text, "")

        End If

    End Sub

    Private Sub UniqueId_TextChanged_1(sender As Object, e As EventArgs) Handles UniqueId.TextChanged

        If Not initted Then Return
        Form1.Settings.MachineID() = UniqueId.Text
        changed = True

    End Sub

#End Region

End Class
