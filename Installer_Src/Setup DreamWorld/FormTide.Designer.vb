<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormTide
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId:="System.Windows.Forms.Control.set_Text(System.String)")>
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormTide))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.RunOnBoot = New System.Windows.Forms.PictureBox()
        Me.TideInfoDebugCheckBox = New System.Windows.Forms.CheckBox()
        Me.BroadcastTideInfo = New System.Windows.Forms.CheckBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TideHiLoChannelTextBox = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TideInfoChannelTextBox = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.CycleTimeTextBox = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TideLowLevelTextBox = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TideHighLevelTextBox = New System.Windows.Forms.TextBox()
        Me.TideEnabledCheckbox = New System.Windows.Forms.CheckBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.MenuStrip2 = New System.Windows.Forms.MenuStrip()
        Me.ToolStripMenuItem30 = New System.Windows.Forms.ToolStripMenuItem()
        Me.DatabaseSetupToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GroupBox1.SuspendLayout()
        CType(Me.RunOnBoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.RunOnBoot)
        Me.GroupBox1.Controls.Add(Me.TideInfoDebugCheckBox)
        Me.GroupBox1.Controls.Add(Me.BroadcastTideInfo)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.TideHiLoChannelTextBox)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.TideInfoChannelTextBox)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.CycleTimeTextBox)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.TideLowLevelTextBox)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.TideHighLevelTextBox)
        Me.GroupBox1.Controls.Add(Me.TideEnabledCheckbox)
        Me.GroupBox1.Location = New System.Drawing.Point(15, 46)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox1.Size = New System.Drawing.Size(354, 290)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Global Tide Settings"
        '
        'RunOnBoot
        '
        Me.RunOnBoot.Image = Global.Outworldz.My.Resources.about
        Me.RunOnBoot.Location = New System.Drawing.Point(254, 74)
        Me.RunOnBoot.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.RunOnBoot.Name = "RunOnBoot"
        Me.RunOnBoot.Size = New System.Drawing.Size(38, 42)
        Me.RunOnBoot.TabIndex = 1860
        Me.RunOnBoot.TabStop = False
        '
        'TideInfoDebugCheckBox
        '
        Me.TideInfoDebugCheckBox.AutoSize = True
        Me.TideInfoDebugCheckBox.Location = New System.Drawing.Point(34, 254)
        Me.TideInfoDebugCheckBox.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TideInfoDebugCheckBox.Name = "TideInfoDebugCheckBox"
        Me.TideInfoDebugCheckBox.Size = New System.Drawing.Size(212, 21)
        Me.TideInfoDebugCheckBox.TabIndex = 7
        Me.TideInfoDebugCheckBox.Text = Global.Outworldz.My.Resources.Send_Debug_Info
        Me.ToolTip1.SetToolTip(Me.TideInfoDebugCheckBox, Global.Outworldz.My.Resources.Provide_Info)
        Me.TideInfoDebugCheckBox.UseVisualStyleBackColor = True
        '
        'BroadcastTideInfo
        '
        Me.BroadcastTideInfo.AutoSize = True
        Me.BroadcastTideInfo.Location = New System.Drawing.Point(156, 40)
        Me.BroadcastTideInfo.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.BroadcastTideInfo.Name = "BroadcastTideInfo"
        Me.BroadcastTideInfo.Size = New System.Drawing.Size(153, 21)
        Me.BroadcastTideInfo.TabIndex = 4
        Me.BroadcastTideInfo.Text = Global.Outworldz.My.Resources.Broadcast_Tide_Info
        Me.ToolTip1.SetToolTip(Me.BroadcastTideInfo, Global.Outworldz.My.Resources.Broadcast_Tide_Chat)
        Me.BroadcastTideInfo.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(26, 221)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(129, 17)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Tide Hi/Lo Channel"
        '
        'TideHiLoChannelTextBox
        '
        Me.TideHiLoChannelTextBox.Location = New System.Drawing.Point(176, 216)
        Me.TideHiLoChannelTextBox.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TideHiLoChannelTextBox.Name = "TideHiLoChannelTextBox"
        Me.TideHiLoChannelTextBox.Size = New System.Drawing.Size(59, 22)
        Me.TideHiLoChannelTextBox.TabIndex = 6
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(30, 188)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(119, 17)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Tide Info Channel"
        '
        'TideInfoChannelTextBox
        '
        Me.TideInfoChannelTextBox.Location = New System.Drawing.Point(176, 184)
        Me.TideInfoChannelTextBox.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TideInfoChannelTextBox.Name = "TideInfoChannelTextBox"
        Me.TideInfoChannelTextBox.Size = New System.Drawing.Size(59, 22)
        Me.TideInfoChannelTextBox.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(30, 151)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(149, 17)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Cycle Time in seconds"
        '
        'CycleTimeTextBox
        '
        Me.CycleTimeTextBox.Location = New System.Drawing.Point(176, 148)
        Me.CycleTimeTextBox.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.CycleTimeTextBox.Name = "CycleTimeTextBox"
        Me.CycleTimeTextBox.Size = New System.Drawing.Size(59, 22)
        Me.CycleTimeTextBox.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.CycleTimeTextBox, Global.Outworldz.My.Resources.Cycle_time_text)
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(30, 119)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(113, 17)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Low Water Level"
        '
        'TideLowLevelTextBox
        '
        Me.TideLowLevelTextBox.Location = New System.Drawing.Point(176, 115)
        Me.TideLowLevelTextBox.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TideLowLevelTextBox.Name = "TideLowLevelTextBox"
        Me.TideLowLevelTextBox.Size = New System.Drawing.Size(59, 22)
        Me.TideLowLevelTextBox.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.TideLowLevelTextBox, Global.Outworldz.My.Resources.Low_High)
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(30, 85)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(117, 17)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "High Water Level"
        '
        'TideHighLevelTextBox
        '
        Me.TideHighLevelTextBox.Location = New System.Drawing.Point(176, 81)
        Me.TideHighLevelTextBox.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TideHighLevelTextBox.Name = "TideHighLevelTextBox"
        Me.TideHighLevelTextBox.Size = New System.Drawing.Size(59, 22)
        Me.TideHighLevelTextBox.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.TideHighLevelTextBox, Global.Outworldz.My.Resources.High_Water_Level_text)
        '
        'TideEnabledCheckbox
        '
        Me.TideEnabledCheckbox.AutoSize = True
        Me.TideEnabledCheckbox.Location = New System.Drawing.Point(34, 40)
        Me.TideEnabledCheckbox.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TideEnabledCheckbox.Name = "TideEnabledCheckbox"
        Me.TideEnabledCheckbox.Size = New System.Drawing.Size(74, 21)
        Me.TideEnabledCheckbox.TabIndex = 0
        Me.TideEnabledCheckbox.Text = Global.Outworldz.My.Resources.Enable_word
        Me.TideEnabledCheckbox.UseVisualStyleBackColor = True
        '
        'ToolTip1
        '
        Me.ToolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        Me.ToolTip1.ToolTipTitle = Global.Outworldz.My.Resources.Tide_Enable
        '
        'MenuStrip2
        '
        Me.MenuStrip2.ImageScalingSize = New System.Drawing.Size(16, 16)
        Me.MenuStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem30})
        Me.MenuStrip2.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip2.Name = "MenuStrip2"
        Me.MenuStrip2.Padding = New System.Windows.Forms.Padding(5, 1, 0, 1)
        Me.MenuStrip2.Size = New System.Drawing.Size(398, 26)
        Me.MenuStrip2.TabIndex = 1886
        Me.MenuStrip2.Text = "0"
        '
        'ToolStripMenuItem30
        '
        Me.ToolStripMenuItem30.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DatabaseSetupToolStripMenuItem})
        Me.ToolStripMenuItem30.Image = Global.Outworldz.My.Resources.question_and_answer
        Me.ToolStripMenuItem30.Name = "ToolStripMenuItem30"
        Me.ToolStripMenuItem30.Size = New System.Drawing.Size(75, 24)
        Me.ToolStripMenuItem30.Text = Global.Outworldz.My.Resources.Help_word
        '
        'DatabaseSetupToolStripMenuItem
        '
        Me.DatabaseSetupToolStripMenuItem.Image = Global.Outworldz.My.Resources.about
        Me.DatabaseSetupToolStripMenuItem.Name = "DatabaseSetupToolStripMenuItem"
        Me.DatabaseSetupToolStripMenuItem.Size = New System.Drawing.Size(124, 26)
        Me.DatabaseSetupToolStripMenuItem.Text = Global.Outworldz.My.Resources.Help_word
        '
        'FormTide
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(120.0!, 120.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.ClientSize = New System.Drawing.Size(398, 364)
        Me.Controls.Add(Me.MenuStrip2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.MaximizeBox = False
        Me.Name = "FormTide"
        Me.Text = "Tides"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.RunOnBoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip2.ResumeLayout(False)
        Me.MenuStrip2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents TideEnabledCheckbox As CheckBox
    Friend WithEvents Label2 As Label
    Friend WithEvents TideLowLevelTextBox As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents TideHighLevelTextBox As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents CycleTimeTextBox As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents TideInfoChannelTextBox As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents TideHiLoChannelTextBox As TextBox
    Friend WithEvents BroadcastTideInfo As CheckBox
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents TideInfoDebugCheckBox As CheckBox
    Friend WithEvents RunOnBoot As PictureBox
    Friend WithEvents MenuStrip2 As MenuStrip
    Friend WithEvents ToolStripMenuItem30 As ToolStripMenuItem
    Friend WithEvents DatabaseSetupToolStripMenuItem As ToolStripMenuItem
End Class
