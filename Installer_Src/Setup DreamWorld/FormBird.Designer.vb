<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormBird
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormBird))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.ToleranceLabel = New System.Windows.Forms.Label()
        Me.PrimNameLabel = New System.Windows.Forms.Label()
        Me.PrimNameTextBox = New System.Windows.Forms.TextBox()
        Me.MaxHLabel = New System.Windows.Forms.Label()
        Me.BirdsMaxHeightTextBox = New System.Windows.Forms.TextBox()
        Me.BorderLabel = New System.Windows.Forms.Label()
        Me.BirdsBorderSizeTextBox = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.BirdsToleranceTextBox = New System.Windows.Forms.TextBox()
        Me.SeparationLabel = New System.Windows.Forms.Label()
        Me.DesiredSeparationTextBox = New System.Windows.Forms.TextBox()
        Me.NeighborLabel = New System.Windows.Forms.Label()
        Me.BirdsNeighbourDistanceTextBox = New System.Windows.Forms.TextBox()
        Me.maxForceLabel = New System.Windows.Forms.Label()
        Me.MaxForceTextBox = New System.Windows.Forms.TextBox()
        Me.MaxSpeedLabel = New System.Windows.Forms.Label()
        Me.MaxSpeedTextBox = New System.Windows.Forms.TextBox()
        Me.ChatLabel = New System.Windows.Forms.Label()
        Me.ChatChanelTextBox = New System.Windows.Forms.TextBox()
        Me.FlockLabel = New System.Windows.Forms.Label()
        Me.BirdsFlockSize = New System.Windows.Forms.DomainUpDown()
        Me.BirdsModuleEnable = New System.Windows.Forms.CheckBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.LoadIARButton = New System.Windows.Forms.Button()
        Me.MenuStrip2 = New System.Windows.Forms.MenuStrip()
        Me.HelpMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GroupBox1.SuspendLayout()
        Me.MenuStrip2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.ToleranceLabel)
        Me.GroupBox1.Controls.Add(Me.PrimNameLabel)
        Me.GroupBox1.Controls.Add(Me.PrimNameTextBox)
        Me.GroupBox1.Controls.Add(Me.MaxHLabel)
        Me.GroupBox1.Controls.Add(Me.BirdsMaxHeightTextBox)
        Me.GroupBox1.Controls.Add(Me.BorderLabel)
        Me.GroupBox1.Controls.Add(Me.BirdsBorderSizeTextBox)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.BirdsToleranceTextBox)
        Me.GroupBox1.Controls.Add(Me.SeparationLabel)
        Me.GroupBox1.Controls.Add(Me.DesiredSeparationTextBox)
        Me.GroupBox1.Controls.Add(Me.NeighborLabel)
        Me.GroupBox1.Controls.Add(Me.BirdsNeighbourDistanceTextBox)
        Me.GroupBox1.Controls.Add(Me.maxForceLabel)
        Me.GroupBox1.Controls.Add(Me.MaxForceTextBox)
        Me.GroupBox1.Controls.Add(Me.MaxSpeedLabel)
        Me.GroupBox1.Controls.Add(Me.MaxSpeedTextBox)
        Me.GroupBox1.Controls.Add(Me.ChatLabel)
        Me.GroupBox1.Controls.Add(Me.ChatChanelTextBox)
        Me.GroupBox1.Controls.Add(Me.FlockLabel)
        Me.GroupBox1.Controls.Add(Me.BirdsFlockSize)
        Me.GroupBox1.Controls.Add(Me.BirdsModuleEnable)
        Me.GroupBox1.Location = New System.Drawing.Point(16, 82)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(317, 331)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Bird Module"
        '
        'ToleranceLabel
        '
        Me.ToleranceLabel.AutoSize = True
        Me.ToleranceLabel.Location = New System.Drawing.Point(85, 216)
        Me.ToleranceLabel.Name = "ToleranceLabel"
        Me.ToleranceLabel.Size = New System.Drawing.Size(123, 13)
        Me.ToleranceLabel.TabIndex = 24
        Me.ToleranceLabel.Text = "Tolerance (default=25.0)"
        Me.ToolTip1.SetToolTip(Me.ToleranceLabel, Global.Outworldz.My.Resources.Resources.Tolerance)
        '
        'PrimNameLabel
        '
        Me.PrimNameLabel.AutoSize = True
        Me.PrimNameLabel.Location = New System.Drawing.Point(85, 293)
        Me.PrimNameLabel.Name = "PrimNameLabel"
        Me.PrimNameLabel.Size = New System.Drawing.Size(148, 13)
        Me.PrimNameLabel.TabIndex = 23
        Me.PrimNameLabel.Text = "Prim Name (default=SeaGull1)"
        Me.ToolTip1.SetToolTip(Me.PrimNameLabel, Global.Outworldz.My.Resources.Resources.How_High)
        '
        'PrimNameTextBox
        '
        Me.PrimNameTextBox.Location = New System.Drawing.Point(7, 287)
        Me.PrimNameTextBox.Name = "PrimNameTextBox"
        Me.PrimNameTextBox.Size = New System.Drawing.Size(72, 20)
        Me.PrimNameTextBox.TabIndex = 11
        Me.PrimNameTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.PrimNameTextBox, Global.Outworldz.My.Resources.Resources.Prim_Name)
        '
        'MaxHLabel
        '
        Me.MaxHLabel.AutoSize = True
        Me.MaxHLabel.Location = New System.Drawing.Point(85, 268)
        Me.MaxHLabel.Name = "MaxHLabel"
        Me.MaxHLabel.Size = New System.Drawing.Size(129, 13)
        Me.MaxHLabel.TabIndex = 21
        Me.MaxHLabel.Text = "Max Height (default=45.0)"
        '
        'BirdsMaxHeightTextBox
        '
        Me.BirdsMaxHeightTextBox.Location = New System.Drawing.Point(7, 261)
        Me.BirdsMaxHeightTextBox.Name = "BirdsMaxHeightTextBox"
        Me.BirdsMaxHeightTextBox.Size = New System.Drawing.Size(43, 20)
        Me.BirdsMaxHeightTextBox.TabIndex = 10
        Me.BirdsMaxHeightTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'BorderLabel
        '
        Me.BorderLabel.AutoSize = True
        Me.BorderLabel.Location = New System.Drawing.Point(85, 242)
        Me.BorderLabel.Name = "BorderLabel"
        Me.BorderLabel.Size = New System.Drawing.Size(129, 13)
        Me.BorderLabel.TabIndex = 19
        Me.BorderLabel.Text = "Border Size (default=25.0)"
        '
        'BirdsBorderSizeTextBox
        '
        Me.BirdsBorderSizeTextBox.Location = New System.Drawing.Point(7, 235)
        Me.BirdsBorderSizeTextBox.Name = "BirdsBorderSizeTextBox"
        Me.BirdsBorderSizeTextBox.Size = New System.Drawing.Size(43, 20)
        Me.BirdsBorderSizeTextBox.TabIndex = 9
        Me.BirdsBorderSizeTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(85, 216)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(0, 13)
        Me.Label8.TabIndex = 17
        '
        'BirdsToleranceTextBox
        '
        Me.BirdsToleranceTextBox.Location = New System.Drawing.Point(7, 209)
        Me.BirdsToleranceTextBox.Name = "BirdsToleranceTextBox"
        Me.BirdsToleranceTextBox.Size = New System.Drawing.Size(43, 20)
        Me.BirdsToleranceTextBox.TabIndex = 8
        Me.BirdsToleranceTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'SeparationLabel
        '
        Me.SeparationLabel.AutoSize = True
        Me.SeparationLabel.Location = New System.Drawing.Point(85, 189)
        Me.SeparationLabel.Name = "SeparationLabel"
        Me.SeparationLabel.Size = New System.Drawing.Size(159, 13)
        Me.SeparationLabel.TabIndex = 13
        Me.SeparationLabel.Text = "Desired Separation (default=5.0)"
        Me.ToolTip1.SetToolTip(Me.SeparationLabel, Global.Outworldz.My.Resources.Resources.How_Far)
        '
        'DesiredSeparationTextBox
        '
        Me.DesiredSeparationTextBox.Location = New System.Drawing.Point(7, 183)
        Me.DesiredSeparationTextBox.Name = "DesiredSeparationTextBox"
        Me.DesiredSeparationTextBox.Size = New System.Drawing.Size(43, 20)
        Me.DesiredSeparationTextBox.TabIndex = 7
        Me.DesiredSeparationTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.DesiredSeparationTextBox, Global.Outworldz.My.Resources.Resources.How_Far)
        '
        'NeighborLabel
        '
        Me.NeighborLabel.AutoSize = True
        Me.NeighborLabel.Location = New System.Drawing.Point(85, 164)
        Me.NeighborLabel.Name = "NeighborLabel"
        Me.NeighborLabel.Size = New System.Drawing.Size(163, 13)
        Me.NeighborLabel.TabIndex = 11
        Me.NeighborLabel.Text = "Neighbor Distance (default=25.0)"
        Me.ToolTip1.SetToolTip(Me.NeighborLabel, Global.Outworldz.My.Resources.Resources.How_Far)
        '
        'BirdsNeighbourDistanceTextBox
        '
        Me.BirdsNeighbourDistanceTextBox.Location = New System.Drawing.Point(7, 157)
        Me.BirdsNeighbourDistanceTextBox.Name = "BirdsNeighbourDistanceTextBox"
        Me.BirdsNeighbourDistanceTextBox.Size = New System.Drawing.Size(43, 20)
        Me.BirdsNeighbourDistanceTextBox.TabIndex = 6
        Me.BirdsNeighbourDistanceTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.BirdsNeighbourDistanceTextBox, Global.Outworldz.My.Resources.Resources.Max_Dist)
        '
        'maxForceLabel
        '
        Me.maxForceLabel.AutoSize = True
        Me.maxForceLabel.Location = New System.Drawing.Point(85, 133)
        Me.maxForceLabel.Name = "maxForceLabel"
        Me.maxForceLabel.Size = New System.Drawing.Size(119, 13)
        Me.maxForceLabel.TabIndex = 9
        Me.maxForceLabel.Text = "Max Force (default=0.2)"
        Me.ToolTip1.SetToolTip(Me.maxForceLabel, Global.Outworldz.My.Resources.Resources.Max_Accel)
        '
        'MaxForceTextBox
        '
        Me.MaxForceTextBox.Location = New System.Drawing.Point(7, 131)
        Me.MaxForceTextBox.Name = "MaxForceTextBox"
        Me.MaxForceTextBox.Size = New System.Drawing.Size(43, 20)
        Me.MaxForceTextBox.TabIndex = 5
        Me.MaxForceTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.MaxForceTextBox, Global.Outworldz.My.Resources.Resources.How_Far_Travel)
        '
        'MaxSpeedLabel
        '
        Me.MaxSpeedLabel.AutoSize = True
        Me.MaxSpeedLabel.Location = New System.Drawing.Point(85, 108)
        Me.MaxSpeedLabel.Name = "MaxSpeedLabel"
        Me.MaxSpeedLabel.Size = New System.Drawing.Size(123, 13)
        Me.MaxSpeedLabel.TabIndex = 7
        Me.MaxSpeedLabel.Text = "Max Speed (default=1.0)"
        '
        'MaxSpeedTextBox
        '
        Me.MaxSpeedTextBox.Location = New System.Drawing.Point(7, 105)
        Me.MaxSpeedTextBox.Name = "MaxSpeedTextBox"
        Me.MaxSpeedTextBox.Size = New System.Drawing.Size(43, 20)
        Me.MaxSpeedTextBox.TabIndex = 4
        Me.MaxSpeedTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.MaxSpeedTextBox, Global.Outworldz.My.Resources.Resources.How_Far_Travel)
        '
        'ChatLabel
        '
        Me.ChatLabel.AutoSize = True
        Me.ChatLabel.Location = New System.Drawing.Point(85, 81)
        Me.ChatLabel.Name = "ChatLabel"
        Me.ChatLabel.Size = New System.Drawing.Size(71, 13)
        Me.ChatLabel.TabIndex = 5
        Me.ChatLabel.Text = "Chat Channel"
        Me.ToolTip1.SetToolTip(Me.ChatLabel, Global.Outworldz.My.Resources.Resources.Which_Channel)
        '
        'ChatChanelTextBox
        '
        Me.ChatChanelTextBox.Location = New System.Drawing.Point(7, 77)
        Me.ChatChanelTextBox.Name = "ChatChanelTextBox"
        Me.ChatChanelTextBox.Size = New System.Drawing.Size(43, 20)
        Me.ChatChanelTextBox.TabIndex = 3
        Me.ChatChanelTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'FlockLabel
        '
        Me.FlockLabel.AutoSize = True
        Me.FlockLabel.Location = New System.Drawing.Point(85, 55)
        Me.FlockLabel.Name = "FlockLabel"
        Me.FlockLabel.Size = New System.Drawing.Size(77, 13)
        Me.FlockLabel.TabIndex = 3
        Me.FlockLabel.Text = "Bird Flock Size"
        Me.ToolTip1.SetToolTip(Me.FlockLabel, Global.Outworldz.My.Resources.Resources.Num_Birds)
        '
        'BirdsFlockSize
        '
        Me.BirdsFlockSize.Location = New System.Drawing.Point(7, 52)
        Me.BirdsFlockSize.Name = "BirdsFlockSize"
        Me.BirdsFlockSize.Size = New System.Drawing.Size(60, 20)
        Me.BirdsFlockSize.TabIndex = 2
        Me.BirdsFlockSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'BirdsModuleEnable
        '
        Me.BirdsModuleEnable.AutoSize = True
        Me.BirdsModuleEnable.Location = New System.Drawing.Point(7, 20)
        Me.BirdsModuleEnable.Name = "BirdsModuleEnable"
        Me.BirdsModuleEnable.Size = New System.Drawing.Size(118, 17)
        Me.BirdsModuleEnable.TabIndex = 1
        Me.BirdsModuleEnable.Text = Global.Outworldz.My.Resources.Resources.Enable_Birds_word
        Me.ToolTip1.SetToolTip(Me.BirdsModuleEnable, Global.Outworldz.My.Resources.Resources.Determines)
        Me.BirdsModuleEnable.UseVisualStyleBackColor = True
        '
        'LoadIARButton
        '
        Me.LoadIARButton.Image = Global.Outworldz.My.Resources.Resources.package_add
        Me.LoadIARButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LoadIARButton.Location = New System.Drawing.Point(27, 37)
        Me.LoadIARButton.Name = "LoadIARButton"
        Me.LoadIARButton.Size = New System.Drawing.Size(170, 32)
        Me.LoadIARButton.TabIndex = 0
        Me.LoadIARButton.Text = Global.Outworldz.My.Resources.Resources.Load_Bird_IAR_word
        Me.LoadIARButton.UseVisualStyleBackColor = True
        '
        'MenuStrip2
        '
        Me.MenuStrip2.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.MenuStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HelpMenuItem})
        Me.MenuStrip2.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip2.Name = "MenuStrip2"
        Me.MenuStrip2.Padding = New System.Windows.Forms.Padding(4, 1, 0, 1)
        Me.MenuStrip2.Size = New System.Drawing.Size(345, 30)
        Me.MenuStrip2.TabIndex = 1890
        Me.MenuStrip2.Text = "0"
        '
        'HelpMenuItem
        '
        Me.HelpMenuItem.Image = Global.Outworldz.My.Resources.Resources.about
        Me.HelpMenuItem.Name = "HelpMenuItem"
        Me.HelpMenuItem.Size = New System.Drawing.Size(68, 28)
        Me.HelpMenuItem.Text = Global.Outworldz.My.Resources.Resources.Help_word
        '
        'FormBird
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(345, 425)
        Me.Controls.Add(Me.MenuStrip2)
        Me.Controls.Add(Me.LoadIARButton)
        Me.Controls.Add(Me.GroupBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "FormBird"
        Me.Text = "Bird Module"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.MenuStrip2.ResumeLayout(False)
        Me.MenuStrip2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents BirdsModuleEnable As CheckBox
    Friend WithEvents FlockLabel As Label
    Friend WithEvents BirdsFlockSize As DomainUpDown
    Friend WithEvents ChatLabel As Label
    Friend WithEvents ChatChanelTextBox As TextBox
    Friend WithEvents MaxSpeedLabel As Label
    Friend WithEvents MaxSpeedTextBox As TextBox
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents maxForceLabel As Label
    Friend WithEvents MaxForceTextBox As TextBox
    Friend WithEvents NeighborLabel As Label
    Friend WithEvents BirdsNeighbourDistanceTextBox As TextBox
    Friend WithEvents SeparationLabel As Label
    Friend WithEvents DesiredSeparationTextBox As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents BirdsToleranceTextBox As TextBox
    Friend WithEvents BorderLabel As Label
    Friend WithEvents BirdsBorderSizeTextBox As TextBox
    Friend WithEvents MaxHLabel As Label
    Friend WithEvents BirdsMaxHeightTextBox As TextBox
    Friend WithEvents PrimNameLabel As Label
    Friend WithEvents PrimNameTextBox As TextBox
    Friend WithEvents LoadIARButton As Button
    Friend WithEvents MenuStrip2 As MenuStrip
    Friend WithEvents HelpMenuItem As ToolStripMenuItem
    Friend WithEvents ToleranceLabel As Label
End Class
