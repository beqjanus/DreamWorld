Public Class FormSearch

    Private Sub FormSearch_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        HypericaRadioButton.Text = Global.Outworldz.My.Resources.HypericaSearch_word
        JOpensimRadioButton.Text = Global.Outworldz.My.Resources.JOpensimSearch_word
        SearchBox.Text = Global.Outworldz.My.Resources.SearchOptions_word
        LocalButton.Text = Global.Outworldz.My.Resources.Local_Search

        Dim installed As Boolean = Joomla.IsjOpensimInstalled()
        If installed Then
            JOpensimRadioButton.Enabled = True
        Else
            JOpensimRadioButton.Enabled = False
            JOpensimRadioButton.Checked = False
        End If

        HelpOnce("Search")

        Select Case Settings.SearchOptions
            Case ""
                RadioButton2.Checked = True
            Case JOpensim
                JOpensimRadioButton.Checked = True
            Case "Local"
                LocalButton.Checked = True
            Case "Hyperica"
                HypericaRadioButton.Checked = True
        End Select

    End Sub

    Private Sub HelpToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ToolStripLabel1.Click

        HelpManual("Search")

    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles HypericaRadioButton.CheckedChanged

        If HypericaRadioButton.Checked Then
            Settings.SearchOptions = Hyperica
            Settings.SaveSettings()
        End If

    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles JOpensimRadioButton.CheckedChanged

        If JOpensimRadioButton.Checked Then
            Settings.SearchOptions = JOpensim
            Settings.SaveSettings()
        End If

    End Sub

    Private Sub RadioButton2_CheckedChanged_1(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged

        If RadioButton2.Checked Then
            Settings.SearchOptions = ""
            Settings.SaveSettings()
        End If


    End Sub

    Private Sub LocalButton_CheckedChanged(sender As Object, e As EventArgs) Handles LocalButton.CheckedChanged

        If LocalButton.Checked Then
            Settings.SearchOptions = "Local"
            Settings.SaveSettings()
        End If


    End Sub
End Class