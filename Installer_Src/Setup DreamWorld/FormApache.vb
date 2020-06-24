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

Imports System.Text.RegularExpressions

Public Class FormApache

#Region "Private Fields"

    Dim initted As Boolean = False

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

#Region "Start/Stop"

    Private Sub Close_form(sender As Object, e As EventArgs) Handles Me.Closed


        Form1.Settings.SaveSettings()

        Form1.PropViewedSettings = True

    End Sub

    Private Sub Loaded(sender As Object, e As EventArgs) Handles Me.Load

        SetScreen()

        ApacheCheckbox.Checked = Form1.Settings.ApacheEnable
        ApachePort.Text = CType(Form1.Settings.ApachePort, String)
        ApacheServiceCheckBox.Checked = Form1.Settings.ApacheService

        If Form1.Settings.SearchLocal Then
            LocalSearchCheckBox.Checked = True
            AllGridSearchCheckBox.Checked = False
        Else
            LocalSearchCheckBox.Checked = False
            AllGridSearchCheckBox.Checked = True
        End If

        '''' set the other bvox and the radios for Different CMS systems. 
        ''' This is used to redirect all access to apache / to the folder listed below
        ''' 
        If Form1.Settings.CMS = "DreamGrid" Then
            EnableDiva.Checked = True
        ElseIf Form1.Settings.CMS = "Wordpress" Then
            EnableWP.Checked = True
        ElseIf Form1.Settings.CMS = "JOpensim" Then
            EnableJOpensim.Checked = True
        Else
            EnableOther.Checked = True
            Other.Text = Form1.Settings.CMS
        End If

        EnableSearchCheckBox.Checked = Form1.Settings.SearchEnabled
        EventsCheckBox.Checked = Form1.Settings.EventTimerEnabled
        Form1.HelpOnce("Apache")
        initted = True

    End Sub

#End Region

#Region "Clickers"

    Private Sub AllGridSearchCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles AllGridSearchCheckBox.CheckedChanged
        If AllGridSearchCheckBox.Checked Then
            Form1.Settings.SearchLocal = False
            LocalSearchCheckBox.Checked = False
        End If
    End Sub

    Private Sub ApacheCheckbox_CheckedChanged(sender As Object, e As EventArgs) Handles ApacheCheckbox.CheckedChanged

        If Not initted Then Return
        Form1.Settings.ApacheEnable = ApacheCheckbox.Checked

    End Sub

    Private Sub ApachePort_TextChanged(sender As Object, e As EventArgs) Handles ApachePort.TextChanged

        If Not initted Then Return

        Dim digitsOnly As Regex = New Regex("[^\d]")
        ApachePort.Text = digitsOnly.Replace(ApachePort.Text, "")
        If ApachePort.Text.Length > 0 Then
            Form1.Settings.ApachePort = CType(ApachePort.Text, Integer)
        End If

    End Sub

    Private Sub ApacheServiceCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles ApacheServiceCheckBox.CheckedChanged

        If Not initted Then Return
        Form1.Settings.ApacheService = ApacheServiceCheckBox.Checked

    End Sub

    Private Sub ApacheToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ApacheToolStripMenuItem.Click
        Form1.Help("Apache")
    End Sub

    Private Sub LocalSearchCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles LocalSearchCheckBox.CheckedChanged
        If LocalSearchCheckBox.Checked Then
            Form1.Settings.SearchLocal = True
            AllGridSearchCheckBox.Checked = False
        End If
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Form1.Help("Apache")
    End Sub

    Private Sub RemoveApache()

        Using ApacheProcess As New Process()
            ApacheProcess.StartInfo.FileName = "sc"
            ApacheProcess.StartInfo.Arguments = "stop " & "ApacheHTTPServer"
            Try
                ApacheProcess.Start()
#Disable Warning CA1031
            Catch ex As Exception
#Enable Warning CA1031
            End Try
            Application.DoEvents()
            ApacheProcess.WaitForExit()
            Form1.Sleep(1000)
            ApacheProcess.StartInfo.Arguments = " delete  " & "ApacheHTTPServer"
            Try
                ApacheProcess.Start()
#Disable Warning CA1031
            Catch ex As Exception
#Enable Warning CA1031
            End Try
            Application.DoEvents()
            ApacheProcess.WaitForExit()
            Form1.Print(My.Resources.Apache_has_been_removed)
        End Using

    End Sub

    Private Sub X86Button_Click(sender As Object, e As EventArgs) Handles X86Button.Click

        Dim InstallProcess As New Process
        InstallProcess.StartInfo.UseShellExecute = True ' so we can redirect streams
        '
        ' all of them
        InstallProcess.StartInfo.FileName = Form1.PropMyFolder & "\MSFT_Runtimes\Visual C++ Redist Installer V56.exe"

        InstallProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal
        Try
            InstallProcess.Start()
#Disable Warning CA1031
        Catch ex As Exception
#Enable Warning CA1031
        End Try

        InstallProcess.WaitForExit()

        InstallProcess.Dispose()

    End Sub

    Private Sub EnableSearchCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles EnableSearchCheckBox.CheckedChanged

        If Not initted Then Return
        Form1.Settings.SearchEnabled = EnableSearchCheckBox.Checked

    End Sub

    Private Sub EventsCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles EventsCheckBox.CheckedChanged

        If Not initted Then Return
        Form1.Settings.EventTimerEnabled = EventsCheckBox.Checked

    End Sub

    Private Sub EnableDiva_CheckedChanged(sender As Object, e As EventArgs) Handles EnableDiva.CheckedChanged

        If Not initted Then Return
        If EnableDiva.Checked Then Form1.Settings.CMS = "DreamGrid"

    End Sub

    Private Sub EnableWP_CheckedChanged(sender As Object, e As EventArgs) Handles EnableWP.CheckedChanged

        If Not initted Then Return
        If EnableWP.Checked Then Form1.Settings.CMS = "Wordpress"

    End Sub

    Private Sub EnableJOpensim_CheckedChanged(sender As Object, e As EventArgs) Handles EnableJOpensim.CheckedChanged

        If Not initted Then Return
        If EnableJOpensim.Checked Then Form1.Settings.CMS = "JOpensim"

    End Sub

    Private Sub EnableOther_CheckedChanged(sender As Object, e As EventArgs) Handles EnableOther.CheckedChanged

        If Not initted Then Return
        If EnableOther.Checked Then Other.Text = Form1.Settings.CMS

    End Sub

    Private Sub Other_TextChanged(sender As Object, e As EventArgs) Handles Other.TextChanged

        If Not initted Then Return
        If Other.Text.Length > 0 Then
            EnableOther.Checked = True
        Else
            EnableOther.Checked = False
            EnableDiva.Checked = True
        End If
        Form1.Settings.CMS = Other.Text

    End Sub



#End Region

End Class
