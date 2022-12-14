Public Class Language

#Region "ScreenSize"

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
        ScreenPosition.SaveHW(Me.Height, Me.Width)
    End Sub

    Private Sub SetScreen()

        ScreenPosition = New ClassScreenpos(Me.Name)
        AddHandler ResizeEnd, Handler
        Dim xy As List(Of Integer) = ScreenPosition.GetXY()
        Me.Left = xy.Item(0)
        Me.Top = xy.Item(1)

        Dim hw As List(Of Integer) = ScreenPosition.GetHW()

        If hw.Item(0) = 0 Then
            Me.Height = 300
        Else
            Me.Height = hw.Item(0)
        End If
        If hw.Item(1) = 0 Then
            Me.Width = 968
        Else
            Me.Width = hw.Item(1)
        End If

    End Sub

#End Region

    Private Sub Arabic_Click(sender As Object, e As EventArgs) Handles Arabic_Saudi.Click
        Settings.Language = "ar-SA"
        Language(sender, e)
    End Sub

    Private Sub Basque_Click(sender As Object, e As EventArgs) Handles Basque.Click
        Settings.Language = "eu"
        Language(sender, e)
    End Sub

    Private Sub Brazilian_Click(sender As Object, e As EventArgs) Handles Brazilian.Click
        Settings.Language = "pt-BR"
        Language(sender, e)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Irish.Click
        Settings.Language = "ga"
        Language(sender, e)
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Finnish.Click
        Settings.Language = "fi"
        Language(sender, e)
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Polish.Click
        Settings.Language = "pl"
        Language(sender, e)
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Hebrew.Click
        Settings.Language = "he"
        Language(sender, e)
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Mexico.Click
        Settings.Language = "es-MX"
        Language(sender, e)
    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles China.Click
        Settings.Language = "zh-CN"
        Language(sender, e)
    End Sub

    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles French.Click
        Settings.Language = "fr"
        Language(sender, e)
    End Sub

    Private Sub Button16_Click(sender As Object, e As EventArgs) Handles Iceland.Click
        Settings.Language = "is"
        Language(sender, e)
    End Sub

    Private Sub Button17_Click(sender As Object, e As EventArgs) Handles Dutch.Click
        Settings.Language = "nl-NL"
        Language(sender, e)
    End Sub

    Private Sub Button18_Click(sender As Object, e As EventArgs) Handles Portuguese.Click
        Settings.Language = "pt"
        Language(sender, e)
    End Sub

    Private Sub Button19_Click(sender As Object, e As EventArgs) Handles Swedish.Click
        Settings.Language = "sv"
        Language(sender, e)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Germany.Click
        Settings.Language = "de"
        Language(sender, e)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Russian.Click
        Settings.Language = "ru"
        Language(sender, e)
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles czech.Click
        Settings.Language = "cs"
        Language(sender, e)
    End Sub

    Private Sub Button4_Click_1(sender As Object, e As EventArgs) Handles ChineseHK.Click
        Settings.Language = "zh-Hans-HK"
        Language(sender, e)
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Norway.Click
        Settings.Language = "no"
        Language(sender, e)
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Spain.Click
        Settings.Language = "es-ES"
        Language(sender, e)
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Persian.Click
        Settings.Language = "fa-IR"
        Language(sender, e)
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Taiwan.Click
        Settings.Language = "zh-TW"
        Language(sender, e)
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Greek.Click
        Settings.Language = "el"
        Language(sender, e)
    End Sub

    Private Sub Catalan_Click(sender As Object, e As EventArgs) Handles Catalan.Click
        Settings.Language = "ca-ES"
        Language(sender, e)
    End Sub

    Private Sub Danish_Click(sender As Object, e As EventArgs) Handles Danish.Click
        Settings.Language = "da"
        Language(sender, e)
    End Sub

    Private Sub English_Click(sender As Object, e As EventArgs) Handles English.Click
        Settings.Language = "en"
        Language(sender, e)
    End Sub

    Private Sub Italian_Click(sender As Object, e As EventArgs) Handles Italian.Click
        Settings.Language = "it"
        Language(sender, e)
    End Sub

    Private Sub Khazak_Click(sender As Object, e As EventArgs) Handles Khazak.Click
        Settings.Language = "ru-KZ"
        Language(sender, e)
    End Sub

    Private Sub L(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load

        Arabic_Saudi.Text = Global.Outworldz.My.Resources.Arabic_Saudi
        English.Text = Global.Outworldz.My.Resources.English
        Arabic_Saudi.Text = Global.Outworldz.My.Resources.Arabic_Saudi
        Basque.Text = Global.Outworldz.My.Resources.Basque_word
        Brazilian.Text = Global.Outworldz.My.Resources.Brazilian
        Catalan.Text = Global.Outworldz.My.Resources.Catalan
        czech.Text = Global.Outworldz.My.Resources.Czech
        China.Text = Global.Outworldz.My.Resources.Chinese_Simplifed
        Taiwan.Text = Global.Outworldz.My.Resources.Chinese_Traditional
        French.Text = Global.Outworldz.My.Resources.French
        Germany.Text = Global.Outworldz.My.Resources.German
        Greek.Text = Global.Outworldz.My.Resources.Greek
        Hebrew.Text = Global.Outworldz.My.Resources.Hebrew
        Iceland.Text = Global.Outworldz.My.Resources.Icelandic
        Khazak.Text = Global.Outworldz.My.Resources.Khazak
        Dutch.Text = Global.Outworldz.My.Resources.Dutch
        Norway.Text = Global.Outworldz.My.Resources.Norwegian
        Persian.Text = Global.Outworldz.My.Resources.Farsi
        Polish.Text = Global.Outworldz.My.Resources.Polish
        Portuguese.Text = Global.Outworldz.My.Resources.Portuguese
        Russian.Text = Global.Outworldz.My.Resources.Russian
        Mexico.Text = Global.Outworldz.My.Resources.Spanish_Mexico
        Spain.Text = Global.Outworldz.My.Resources.Spanish_Spain
        Swedish.Text = Global.Outworldz.My.Resources.Swedish

        SetScreen()

    End Sub

    Private Sub Language(sender As Object, e As EventArgs)
        Settings.SaveSettings()

        'For Each ci As CultureInfo In CultureInfo.GetCultures(CultureTypes.NeutralCultures)
        'Breakpoint.Print("")
        'Breakpoint.Print(ci.Name)
        'Breakpoint.Print(ci.TwoLetterISOLanguageName)
        'Breakpoint.Print(ci.ThreeLetterISOLanguageName)
        'Breakpoint.Print(ci.ThreeLetterWindowsLanguageName)
        'Breakpoint.Print(ci.DisplayName)
        'Breakpoint.Print(ci.EnglishName)
        'Next

        My.Application.ChangeUICulture(Settings.Language)
        My.Application.ChangeCulture(Settings.Language)
        Me.Controls.Clear() 'removes all the controls on the form
        InitializeComponent() 'load all the controls again
        FormSetup.Reload(sender, e) 'Load everything in your form load event again
    End Sub

    Private Sub Turkish_Click(sender As Object, e As EventArgs) Handles Turkish.Click
        Settings.Language = "tr"
        Language(sender, e)
    End Sub

    Private Sub VIetnamese_Click(sender As Object, e As EventArgs) Handles VIetnamese.Click
        Settings.Language = "vi"
        Language(sender, e)
    End Sub

End Class