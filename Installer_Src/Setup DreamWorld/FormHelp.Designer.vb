<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormHelp
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
    <CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId:="System.Windows.Forms.ToolStripItem.set_Text(System.String)")>
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormHelp))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PrintToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PrintToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.WebSiteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HomeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DreamgridToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TechnicalInfoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TroubleshootingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StepbyStepInstallationToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DatabaseHelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PortsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LoopbackToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SourceCodeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RichTextBox1 = New System.Windows.Forms.RichTextBox()
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument()
        Me.PrintDocument = New System.Drawing.Printing.PrintDocument()
        Me.PrintDialog1 = New System.Windows.Forms.PrintDialog()
        Me.PrintPreviewDialog1 = New System.Windows.Forms.PrintPreviewDialog()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.PrintToolStripMenuItem, Me.ToolStripMenuItem1, Me.WebSiteToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(664, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = ""
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExitToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.FileToolStripMenuItem.Text = Global.Outworldz.My.Resources.File_word
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Image = Global.Outworldz.My.Resources.exit_icon
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(92, 22)
        Me.ExitToolStripMenuItem.Text = Global.Outworldz.My.Resources.Exit__word
        '
        'PrintToolStripMenuItem
        '
        Me.PrintToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PrintToolStripMenuItem1})
        Me.PrintToolStripMenuItem.Name = "PrintToolStripMenuItem"
        Me.PrintToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
        Me.PrintToolStripMenuItem.Text = Global.Outworldz.My.Resources.Print
        '
        'PrintToolStripMenuItem1
        '
        Me.PrintToolStripMenuItem1.Image = Global.Outworldz.My.Resources.printer3
        Me.PrintToolStripMenuItem1.Name = "PrintToolStripMenuItem1"
        Me.PrintToolStripMenuItem1.Size = New System.Drawing.Size(152, 22)
        Me.PrintToolStripMenuItem1.Text = Global.Outworldz.My.Resources.Print
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(12, 20)
        '
        'WebSiteToolStripMenuItem
        '
        Me.WebSiteToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HomeToolStripMenuItem, Me.DreamgridToolStripMenuItem, Me.TechnicalInfoToolStripMenuItem, Me.TroubleshootingToolStripMenuItem, Me.StepbyStepInstallationToolStripMenuItem, Me.DatabaseHelpToolStripMenuItem, Me.PortsToolStripMenuItem, Me.LoopbackToolStripMenuItem, Me.SourceCodeToolStripMenuItem})
        Me.WebSiteToolStripMenuItem.Image = Global.Outworldz.My.Resources.about
        Me.WebSiteToolStripMenuItem.Name = "WebSiteToolStripMenuItem"
        Me.WebSiteToolStripMenuItem.Size = New System.Drawing.Size(248, 20)
        Me.WebSiteToolStripMenuItem.Text = Global.Outworldz.My.Resources.More_Help
        '
        'HomeToolStripMenuItem
        '
        Me.HomeToolStripMenuItem.Image = Global.Outworldz.My.Resources.about
        Me.HomeToolStripMenuItem.Name = "HomeToolStripMenuItem"
        Me.HomeToolStripMenuItem.Size = New System.Drawing.Size(202, 22)
        Me.HomeToolStripMenuItem.Text = "Outworldz.com"
        '
        'DreamgridToolStripMenuItem
        '
        Me.DreamgridToolStripMenuItem.Image = Global.Outworldz.My.Resources.cube_blue
        Me.DreamgridToolStripMenuItem.Name = "DreamgridToolStripMenuItem"
        Me.DreamgridToolStripMenuItem.Size = New System.Drawing.Size(202, 22)
        Me.DreamgridToolStripMenuItem.Text = Global.Outworldz.My.Resources.Home_word
        '
        'TechnicalInfoToolStripMenuItem
        '
        Me.TechnicalInfoToolStripMenuItem.Image = Global.Outworldz.My.Resources.gear
        Me.TechnicalInfoToolStripMenuItem.Name = "TechnicalInfoToolStripMenuItem"
        Me.TechnicalInfoToolStripMenuItem.Size = New System.Drawing.Size(202, 22)
        Me.TechnicalInfoToolStripMenuItem.Text = Global.Outworldz.My.Resources.TechInfo
        '
        'TroubleshootingToolStripMenuItem
        '
        Me.TroubleshootingToolStripMenuItem.Image = Global.Outworldz.My.Resources.gear_run
        Me.TroubleshootingToolStripMenuItem.Name = "TroubleshootingToolStripMenuItem"
        Me.TroubleshootingToolStripMenuItem.Size = New System.Drawing.Size(202, 22)
        Me.TroubleshootingToolStripMenuItem.Text = Global.Outworldz.My.Resources.Troubleshooting_word
        '
        'StepbyStepInstallationToolStripMenuItem
        '
        Me.StepbyStepInstallationToolStripMenuItem.Image = Global.Outworldz.My.Resources.document_connection
        Me.StepbyStepInstallationToolStripMenuItem.Name = "StepbyStepInstallationToolStripMenuItem"
        Me.StepbyStepInstallationToolStripMenuItem.Size = New System.Drawing.Size(202, 22)
        Me.StepbyStepInstallationToolStripMenuItem.Text = Global.Outworldz.My.Resources.Starting_up
        '
        'DatabaseHelpToolStripMenuItem
        '
        Me.DatabaseHelpToolStripMenuItem.Image = Global.Outworldz.My.Resources.data
        Me.DatabaseHelpToolStripMenuItem.Name = "DatabaseHelpToolStripMenuItem"
        Me.DatabaseHelpToolStripMenuItem.Size = New System.Drawing.Size(202, 22)
        Me.DatabaseHelpToolStripMenuItem.Text = Global.Outworldz.My.Resources.Database_Help_word
        '
        'PortsToolStripMenuItem
        '
        Me.PortsToolStripMenuItem.Image = Global.Outworldz.My.Resources.earth_network
        Me.PortsToolStripMenuItem.Name = "PortsToolStripMenuItem"
        Me.PortsToolStripMenuItem.Size = New System.Drawing.Size(202, 22)
        Me.PortsToolStripMenuItem.Text = Global.Outworldz.My.Resources.Port_Forwarding_Help
        '
        'LoopbackToolStripMenuItem
        '
        Me.LoopbackToolStripMenuItem.Image = Global.Outworldz.My.Resources.replace2
        Me.LoopbackToolStripMenuItem.Name = "LoopbackToolStripMenuItem"
        Me.LoopbackToolStripMenuItem.Size = New System.Drawing.Size(202, 22)
        Me.LoopbackToolStripMenuItem.Text = Global.Outworldz.My.Resources.Loopback_Help
        '
        'SourceCodeToolStripMenuItem
        '
        Me.SourceCodeToolStripMenuItem.Image = Global.Outworldz.My.Resources.transform
        Me.SourceCodeToolStripMenuItem.Name = "SourceCodeToolStripMenuItem"
        Me.SourceCodeToolStripMenuItem.Size = New System.Drawing.Size(202, 22)
        Me.SourceCodeToolStripMenuItem.Text = Global.Outworldz.My.Resources.Source_Code_word
        '
        'RichTextBox1
        '
        Me.RichTextBox1.BackColor = System.Drawing.SystemColors.Window
        Me.RichTextBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RichTextBox1.Location = New System.Drawing.Point(0, 24)
        Me.RichTextBox1.Name = "RichTextBox1"
        Me.RichTextBox1.Size = New System.Drawing.Size(664, 449)
        Me.RichTextBox1.TabIndex = 1
        Me.RichTextBox1.Text = ""
        '
        'PrintDialog1
        '
        Me.PrintDialog1.UseEXDialog = True
        '
        'PrintPreviewDialog1
        '
        Me.PrintPreviewDialog1.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.ClientSize = New System.Drawing.Size(400, 300)
        Me.PrintPreviewDialog1.Enabled = True
        Me.PrintPreviewDialog1.Icon = CType(resources.GetObject("PrintPreviewDialog1.Icon"), System.Drawing.Icon)
        Me.PrintPreviewDialog1.Name = "PrintPreviewDialog1"
        Me.PrintPreviewDialog1.Visible = False
        '
        'FormHelp
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(664, 473)
        Me.Controls.Add(Me.RichTextBox1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "FormHelp"
        Me.Text = Global.Outworldz.My.Resources.Help_word
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents WebSiteToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents HomeToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DreamgridToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TechnicalInfoToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TroubleshootingToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents StepbyStepInstallationToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PortsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents LoopbackToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DatabaseHelpToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SourceCodeToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents RichTextBox1 As RichTextBox
    Friend WithEvents FileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PrintToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PrintToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents PrintDocument1 As Printing.PrintDocument
    Friend WithEvents PrintDocument As Printing.PrintDocument
    Friend WithEvents PrintDialog1 As PrintDialog
    Friend WithEvents PrintPreviewDialog1 As PrintPreviewDialog
End Class
