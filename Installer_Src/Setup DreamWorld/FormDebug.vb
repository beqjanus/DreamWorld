Imports System.IO
Imports System.Text.RegularExpressions

Public Class FormDebug

    Private _backup As Boolean
    Private _command As String
    Private _value As Boolean

#Region "FormPos"

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
        ScreenPosition = New ScreenPos(Me.Name)
        AddHandler ResizeEnd, Handler
        Dim xy As List(Of Integer) = ScreenPosition.GetXY()
        Me.Left = xy.Item(0)
        Me.Top = xy.Item(1)
    End Sub

#End Region

#Region "Properties"

    Public Property Backup As Boolean
        Get
            Return _backup
        End Get
        Set(value As Boolean)
            _backup = value
        End Set
    End Property

    Public Property Command As String
        Get
            Return _command
        End Get
        Set(value As String)
            _command = value
        End Set
    End Property

    Public Property Value As Boolean
        Get
            Return _value
        End Get
        Set(value As Boolean)
            _value = value
        End Set
    End Property

#End Region

#Region "Scrolling text box"

    Public Sub ProgressPrint(Value As String)
        Log(My.Resources.Info_word, Value)
        TextBox1.Text += Value & vbCrLf
        If TextBox1.Text.Length > TextBox1.MaxLength - 1000 Then
            TextBox1.Text = Mid(TextBox1.Text, 1000)
        End If
    End Sub

    Private Sub TextBox1_Changed(sender As System.Object, e As EventArgs) Handles TextBox1.TextChanged
        Dim ln As Integer = TextBox1.Text.Length
        TextBox1.SelectionStart = ln
        TextBox1.ScrollToCaret()
    End Sub

#End Region

#Region "Set"

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles ApplyButton.Click

        If Command = My.Resources.SmartStartEnable Then
            If Value Then
                ProgressPrint(My.Resources.SSisEnabled)
                Settings.SSVisible = True
                Settings.SmartStart = True
                HelpManual("SmartStart")
            Else
                ProgressPrint(My.Resources.SSisDisabled)
                Settings.SSVisible = False
                Settings.SmartStart = False
            End If
            Settings.SaveSettings()

        ElseIf Command = $"{My.Resources.Debug_word} Off" Then

            If Value Then
                Settings.StatusInterval = 0
                ProgressPrint($"{My.Resources.Debug_word} Off")
            Else
                ProgressPrint("Unchanged")
            End If
            Settings.SaveSettings()

        ElseIf Command = $"{My.Resources.Debug_word} 1 Minute" Then

            If Value Then
                Settings.StatusInterval = 60
                ProgressPrint($"{My.Resources.Debug_word} 1 Minute")
            Else
                Settings.StatusInterval = 0
                ProgressPrint($"{My.Resources.Debug_word} Off")
            End If
            Settings.SaveSettings()

        ElseIf Command = $"{My.Resources.Debug_word} 10 Minutes" Then

            If Value Then
                Settings.StatusInterval = 600
                ProgressPrint($"{My.Resources.Debug_word} 10 Minutes")
            Else
                Settings.StatusInterval = 0
                ProgressPrint($"{My.Resources.Debug_word} Off")
            End If
            Settings.SaveSettings()

        ElseIf Command = $"{My.Resources.Debug_word} 60 Minutes" Then

            If Value Then
                Settings.StatusInterval = 3600
                ProgressPrint($"{My.Resources.Debug_word} 60 Minutes")
            Else
                Settings.StatusInterval = 0
                ProgressPrint($"{My.Resources.Debug_word} Off")
            End If
            Settings.SaveSettings()

        ElseIf Command = $"{My.Resources.Debug_word} 24 Hours" Then

            If Value Then
                Settings.StatusInterval = 60 * 60 * 24
                ProgressPrint($"{My.Resources.Debug_word} 24 Hours")
            Else
                Settings.StatusInterval = 0
                ProgressPrint($"{My.Resources.Debug_word} Off")
            End If
            Settings.SaveSettings()

        End If

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged

        Command = CStr(ComboBox1.SelectedItem)
        Value = RadioTrue.Checked

    End Sub

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load


        ComboBox1.Items.Add(My.Resources.SmartStartEnable)

        RadioTrue.Checked = False
        RadioFalse.Checked = True

        RadioTrue.Text = My.Resources.True_word
        RadioFalse.Text = My.Resources.False_word

        ComboBox1.Items.Add($"{My.Resources.Debug_word} Off")
        ComboBox1.Items.Add($"{My.Resources.Debug_word} 1 Minute")
        ComboBox1.Items.Add($"{My.Resources.Debug_word} 10 Minutes")
        ComboBox1.Items.Add($"{My.Resources.Debug_word} 60 Minutes")
        ComboBox1.Items.Add($"{My.Resources.Debug_word} 24 hours")

        SetScreen()

    End Sub

#End Region

#Region "Radio"

    Private Sub RadioTrue_CheckedChanged(sender As Object, e As EventArgs) Handles RadioTrue.CheckedChanged

        Value = RadioTrue.Checked

    End Sub

#End Region

End Class