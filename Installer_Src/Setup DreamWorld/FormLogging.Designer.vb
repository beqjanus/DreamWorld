<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormLogging
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormLogging))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.RadioFatal = New System.Windows.Forms.RadioButton()
        Me.RadioError = New System.Windows.Forms.RadioButton()
        Me.RadioWarn = New System.Windows.Forms.RadioButton()
        Me.RadioInfo = New System.Windows.Forms.RadioButton()
        Me.RadioDebug = New System.Windows.Forms.RadioButton()
        Me.RadioOff = New System.Windows.Forms.RadioButton()
        Me.DeleteOnBoot = New System.Windows.Forms.RadioButton()
        Me.KeepLog = New System.Windows.Forms.RadioButton()
        Me.DeletebyAge = New System.Windows.Forms.GroupBox()
        Me.Date_Time_Checkbox = New System.Windows.Forms.CheckBox()
        Me.LogBenchmarks = New System.Windows.Forms.CheckBox()
        Me.AnalyzeButton = New System.Windows.Forms.Button()
        Me.ViewLogButton = New System.Windows.Forms.Button()
        Me.BaretailButton = New System.Windows.Forms.RadioButton()
        Me.OutputViewerButton = New System.Windows.Forms.RadioButton()
        Me.QuickmanagerPictureBox = New System.Windows.Forms.PictureBox()
        Me.BaretailPictureBox = New System.Windows.Forms.PictureBox()
        Me.MenuStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.DeletebyAge.SuspendLayout()
        CType(Me.QuickmanagerPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BaretailPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(28, 28)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HelpToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Padding = New System.Windows.Forms.Padding(4, 1, 0, 1)
        Me.MenuStrip1.Size = New System.Drawing.Size(317, 34)
        Me.MenuStrip1.TabIndex = 0
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.about
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(72, 32)
        Me.HelpToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Help_word
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.RadioFatal)
        Me.GroupBox1.Controls.Add(Me.RadioError)
        Me.GroupBox1.Controls.Add(Me.RadioWarn)
        Me.GroupBox1.Controls.Add(Me.RadioInfo)
        Me.GroupBox1.Controls.Add(Me.RadioDebug)
        Me.GroupBox1.Controls.Add(Me.RadioOff)
        Me.GroupBox1.Location = New System.Drawing.Point(11, 93)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(2, 1, 2, 1)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(2, 1, 2, 1)
        Me.GroupBox1.Size = New System.Drawing.Size(100, 185)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Log Level"
        '
        'RadioFatal
        '
        Me.RadioFatal.AutoSize = True
        Me.RadioFatal.Location = New System.Drawing.Point(13, 123)
        Me.RadioFatal.Name = "RadioFatal"
        Me.RadioFatal.Size = New System.Drawing.Size(48, 17)
        Me.RadioFatal.TabIndex = 4
        Me.RadioFatal.TabStop = True
        Me.RadioFatal.Text = Global.Outworldz.My.Resources.Resources.Fatal_word
        Me.RadioFatal.UseVisualStyleBackColor = True
        '
        'RadioError
        '
        Me.RadioError.AutoSize = True
        Me.RadioError.Location = New System.Drawing.Point(13, 100)
        Me.RadioError.Name = "RadioError"
        Me.RadioError.Size = New System.Drawing.Size(47, 17)
        Me.RadioError.TabIndex = 3
        Me.RadioError.TabStop = True
        Me.RadioError.Text = Global.Outworldz.My.Resources.Resources.Error_word
        Me.RadioError.UseVisualStyleBackColor = True
        '
        'RadioWarn
        '
        Me.RadioWarn.AutoSize = True
        Me.RadioWarn.Location = New System.Drawing.Point(13, 77)
        Me.RadioWarn.Name = "RadioWarn"
        Me.RadioWarn.Size = New System.Drawing.Size(51, 17)
        Me.RadioWarn.TabIndex = 2
        Me.RadioWarn.TabStop = True
        Me.RadioWarn.Text = Global.Outworldz.My.Resources.Resources.Warn_word
        Me.RadioWarn.UseVisualStyleBackColor = True
        '
        'RadioInfo
        '
        Me.RadioInfo.AutoSize = True
        Me.RadioInfo.Location = New System.Drawing.Point(13, 53)
        Me.RadioInfo.Name = "RadioInfo"
        Me.RadioInfo.Size = New System.Drawing.Size(43, 17)
        Me.RadioInfo.TabIndex = 1
        Me.RadioInfo.TabStop = True
        Me.RadioInfo.Text = Global.Outworldz.My.Resources.Resources.Info_word
        Me.RadioInfo.UseVisualStyleBackColor = True
        '
        'RadioDebug
        '
        Me.RadioDebug.AutoSize = True
        Me.RadioDebug.Location = New System.Drawing.Point(13, 29)
        Me.RadioDebug.Name = "RadioDebug"
        Me.RadioDebug.Size = New System.Drawing.Size(57, 17)
        Me.RadioDebug.TabIndex = 0
        Me.RadioDebug.TabStop = True
        Me.RadioDebug.Text = Global.Outworldz.My.Resources.Resources.Debug_word
        Me.RadioDebug.UseVisualStyleBackColor = True
        '
        'RadioOff
        '
        Me.RadioOff.AutoSize = True
        Me.RadioOff.Location = New System.Drawing.Point(13, 147)
        Me.RadioOff.Name = "RadioOff"
        Me.RadioOff.Size = New System.Drawing.Size(39, 17)
        Me.RadioOff.TabIndex = 5
        Me.RadioOff.TabStop = True
        Me.RadioOff.Text = Global.Outworldz.My.Resources.Resources.Off
        Me.RadioOff.UseVisualStyleBackColor = True
        '
        'DeleteOnBoot
        '
        Me.DeleteOnBoot.AutoSize = True
        Me.DeleteOnBoot.Checked = True
        Me.DeleteOnBoot.Location = New System.Drawing.Point(17, 29)
        Me.DeleteOnBoot.Name = "DeleteOnBoot"
        Me.DeleteOnBoot.Size = New System.Drawing.Size(96, 17)
        Me.DeleteOnBoot.TabIndex = 0
        Me.DeleteOnBoot.TabStop = True
        Me.DeleteOnBoot.Text = "Delete on Boot"
        Me.DeleteOnBoot.UseVisualStyleBackColor = True
        '
        'KeepLog
        '
        Me.KeepLog.AutoSize = True
        Me.KeepLog.Location = New System.Drawing.Point(17, 53)
        Me.KeepLog.Name = "KeepLog"
        Me.KeepLog.Size = New System.Drawing.Size(50, 17)
        Me.KeepLog.TabIndex = 1
        Me.KeepLog.Text = "Keep"
        Me.KeepLog.UseVisualStyleBackColor = True
        '
        'DeletebyAge
        '
        Me.DeletebyAge.Controls.Add(Me.Date_Time_Checkbox)
        Me.DeletebyAge.Controls.Add(Me.LogBenchmarks)
        Me.DeletebyAge.Controls.Add(Me.AnalyzeButton)
        Me.DeletebyAge.Controls.Add(Me.ViewLogButton)
        Me.DeletebyAge.Controls.Add(Me.KeepLog)
        Me.DeletebyAge.Controls.Add(Me.DeleteOnBoot)
        Me.DeletebyAge.Location = New System.Drawing.Point(127, 93)
        Me.DeletebyAge.Name = "DeletebyAge"
        Me.DeletebyAge.Size = New System.Drawing.Size(178, 213)
        Me.DeletebyAge.TabIndex = 2
        Me.DeletebyAge.TabStop = False
        Me.DeletebyAge.Text = "Log Files"
        '
        'Date_Time_Checkbox
        '
        Me.Date_Time_Checkbox.AutoSize = True
        Me.Date_Time_Checkbox.Location = New System.Drawing.Point(16, 101)
        Me.Date_Time_Checkbox.Name = "Date_Time_Checkbox"
        Me.Date_Time_Checkbox.Size = New System.Drawing.Size(126, 17)
        Me.Date_Time_Checkbox.TabIndex = 4
        Me.Date_Time_Checkbox.Text = "Show Date and Time"
        Me.Date_Time_Checkbox.UseVisualStyleBackColor = True
        '
        'LogBenchmarks
        '
        Me.LogBenchmarks.AutoSize = True
        Me.LogBenchmarks.Location = New System.Drawing.Point(17, 78)
        Me.LogBenchmarks.Name = "LogBenchmarks"
        Me.LogBenchmarks.Size = New System.Drawing.Size(106, 17)
        Me.LogBenchmarks.TabIndex = 5
        Me.LogBenchmarks.Text = "Log Benchmarks"
        Me.LogBenchmarks.UseVisualStyleBackColor = True
        '
        'AnalyzeButton
        '
        Me.AnalyzeButton.Image = Global.Outworldz.My.Resources.Resources.folder
        Me.AnalyzeButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.AnalyzeButton.Location = New System.Drawing.Point(6, 169)
        Me.AnalyzeButton.Name = "AnalyzeButton"
        Me.AnalyzeButton.Size = New System.Drawing.Size(156, 27)
        Me.AnalyzeButton.TabIndex = 3
        Me.AnalyzeButton.Text = "Analyze Log"
        Me.AnalyzeButton.UseVisualStyleBackColor = True
        '
        'ViewLogButton
        '
        Me.ViewLogButton.Image = Global.Outworldz.My.Resources.Resources.folder
        Me.ViewLogButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ViewLogButton.Location = New System.Drawing.Point(6, 136)
        Me.ViewLogButton.Name = "ViewLogButton"
        Me.ViewLogButton.Size = New System.Drawing.Size(156, 27)
        Me.ViewLogButton.TabIndex = 2
        Me.ViewLogButton.Text = "View"
        Me.ViewLogButton.UseVisualStyleBackColor = True
        '
        'BaretailButton
        '
        Me.BaretailButton.AutoSize = True
        Me.BaretailButton.Location = New System.Drawing.Point(25, 48)
        Me.BaretailButton.Name = "BaretailButton"
        Me.BaretailButton.Size = New System.Drawing.Size(60, 17)
        Me.BaretailButton.TabIndex = 4
        Me.BaretailButton.Text = "Baretail"
        Me.BaretailButton.UseVisualStyleBackColor = True
        '
        'OutputViewerButton
        '
        Me.OutputViewerButton.AutoSize = True
        Me.OutputViewerButton.Location = New System.Drawing.Point(134, 48)
        Me.OutputViewerButton.Name = "OutputViewerButton"
        Me.OutputViewerButton.Size = New System.Drawing.Size(89, 17)
        Me.OutputViewerButton.TabIndex = 5
        Me.OutputViewerButton.Text = "OutputViewer"
        Me.OutputViewerButton.UseVisualStyleBackColor = True
        Me.OutputViewerButton.Visible = False
        '
        'QuickmanagerPictureBox
        '
        Me.QuickmanagerPictureBox.Image = Global.Outworldz.My.Resources.Resources.edge
        Me.QuickmanagerPictureBox.Location = New System.Drawing.Point(236, 38)
        Me.QuickmanagerPictureBox.Name = "QuickmanagerPictureBox"
        Me.QuickmanagerPictureBox.Size = New System.Drawing.Size(33, 27)
        Me.QuickmanagerPictureBox.TabIndex = 6
        Me.QuickmanagerPictureBox.TabStop = False
        Me.QuickmanagerPictureBox.Visible = False
        '
        'BaretailPictureBox
        '
        Me.BaretailPictureBox.Image = Global.Outworldz.My.Resources.Resources.edge
        Me.BaretailPictureBox.Location = New System.Drawing.Point(91, 37)
        Me.BaretailPictureBox.Name = "BaretailPictureBox"
        Me.BaretailPictureBox.Size = New System.Drawing.Size(33, 27)
        Me.BaretailPictureBox.TabIndex = 7
        Me.BaretailPictureBox.TabStop = False
        '
        'FormLogging
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(317, 315)
        Me.Controls.Add(Me.BaretailPictureBox)
        Me.Controls.Add(Me.QuickmanagerPictureBox)
        Me.Controls.Add(Me.OutputViewerButton)
        Me.Controls.Add(Me.BaretailButton)
        Me.Controls.Add(Me.DeletebyAge)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FormLogging"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.DeletebyAge.ResumeLayout(False)
        Me.DeletebyAge.PerformLayout()
        CType(Me.QuickmanagerPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BaretailPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents HelpToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents RadioFatal As RadioButton
    Friend WithEvents RadioError As RadioButton
    Friend WithEvents RadioWarn As RadioButton
    Friend WithEvents RadioInfo As RadioButton
    Friend WithEvents RadioOff As RadioButton
    Friend WithEvents RadioDebug As RadioButton
    Friend WithEvents DeleteOnBoot As RadioButton
    Friend WithEvents KeepLog As RadioButton
    Friend WithEvents DeletebyAge As GroupBox
    Friend WithEvents ViewLogButton As Button
    Friend WithEvents AnalyzeButton As Button
    Friend WithEvents Date_Time_Checkbox As CheckBox
    Friend WithEvents LogBenchmarks As CheckBox
    Friend WithEvents BaretailButton As RadioButton
    Friend WithEvents OutputViewerButton As RadioButton
    Friend WithEvents QuickmanagerPictureBox As PictureBox
    Friend WithEvents BaretailPictureBox As PictureBox
End Class
