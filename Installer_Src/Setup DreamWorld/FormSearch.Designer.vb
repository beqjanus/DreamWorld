<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormSearch
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormSearch))
        Me.SearchBox = New System.Windows.Forms.GroupBox()
        Me.RadioButton2 = New System.Windows.Forms.RadioButton()
        Me.JOpensimRadioButton = New System.Windows.Forms.RadioButton()
        Me.HypericaRadioButton = New System.Windows.Forms.RadioButton()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel()
        Me.SearchBox.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'SearchBox
        '
        Me.SearchBox.Controls.Add(Me.RadioButton2)
        Me.SearchBox.Controls.Add(Me.JOpensimRadioButton)
        Me.SearchBox.Controls.Add(Me.HypericaRadioButton)
        Me.SearchBox.Location = New System.Drawing.Point(22, 48)
        Me.SearchBox.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.SearchBox.Name = "SearchBox"
        Me.SearchBox.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.SearchBox.Size = New System.Drawing.Size(226, 139)
        Me.SearchBox.TabIndex = 4
        Me.SearchBox.TabStop = False
        Me.SearchBox.Text = "Search Options"
        '
        'RadioButton2
        '
        Me.RadioButton2.AutoSize = True
        Me.RadioButton2.Location = New System.Drawing.Point(29, 41)
        Me.RadioButton2.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(97, 20)
        Me.RadioButton2.TabIndex = 6
        Me.RadioButton2.TabStop = True
        Me.RadioButton2.Text = "No Search"
        Me.RadioButton2.UseVisualStyleBackColor = True
        '
        'JOpensimRadioButton
        '
        Me.JOpensimRadioButton.AutoSize = True
        Me.JOpensimRadioButton.Location = New System.Drawing.Point(29, 98)
        Me.JOpensimRadioButton.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.JOpensimRadioButton.Name = "JOpensimRadioButton"
        Me.JOpensimRadioButton.Size = New System.Drawing.Size(140, 20)
        Me.JOpensimRadioButton.TabIndex = 5
        Me.JOpensimRadioButton.TabStop = True
        Me.JOpensimRadioButton.Text = Global.Outworldz.My.Resources.Resources.JOpensimSearch_word
        Me.JOpensimRadioButton.UseVisualStyleBackColor = True
        '
        'HypericaRadioButton
        '
        Me.HypericaRadioButton.AutoSize = True
        Me.HypericaRadioButton.Location = New System.Drawing.Point(29, 71)
        Me.HypericaRadioButton.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.HypericaRadioButton.Name = "HypericaRadioButton"
        Me.HypericaRadioButton.Size = New System.Drawing.Size(134, 20)
        Me.HypericaRadioButton.TabIndex = 4
        Me.HypericaRadioButton.TabStop = True
        Me.HypericaRadioButton.Text = Global.Outworldz.My.Resources.Resources.HypericaSearch_word
        Me.HypericaRadioButton.UseVisualStyleBackColor = True
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton1, Me.ToolStripLabel1})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(259, 33)
        Me.ToolStrip1.TabIndex = 5
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton1.Image = Global.Outworldz.My.Resources.Resources.about
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(34, 28)
        Me.ToolStripButton1.Text = "ToolStripButton1"
        '
        'ToolStripLabel1
        '
        Me.ToolStripLabel1.Name = "ToolStripLabel1"
        Me.ToolStripLabel1.Size = New System.Drawing.Size(49, 28)
        Me.ToolStripLabel1.Text = "Help"
        '
        'FormSearch
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(259, 197)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.SearchBox)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Name = "FormSearch"
        Me.SearchBox.ResumeLayout(False)
        Me.SearchBox.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents SearchBox As GroupBox
    Friend WithEvents RadioButton2 As RadioButton
    Friend WithEvents JOpensimRadioButton As RadioButton
    Friend WithEvents HypericaRadioButton As RadioButton
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents ToolStripButton1 As ToolStripButton
    Friend WithEvents ToolStripLabel1 As ToolStripLabel
End Class
