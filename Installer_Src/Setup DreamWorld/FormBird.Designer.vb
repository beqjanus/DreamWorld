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
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.PrimNameTextBox = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.BirdsMaxHeightTextBox = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.BirdsBorderSizeTextBox = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.BirdsToleranceTextBox = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.DesiredSeparationTextBox = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.BirdsNeighbourDistanceTextBox = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.MaxForceTextBox = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.MaxSpeedTextBox = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ChatChanelTextBox = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.BirdsFlockSizeDomain = New System.Windows.Forms.DomainUpDown()
        Me.BirdsModuleStartupbox = New System.Windows.Forms.CheckBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Button1 = New System.Windows.Forms.Button()
        Me.MenuStrip2 = New System.Windows.Forms.MenuStrip()
        Me.ToolStripMenuItem30 = New System.Windows.Forms.ToolStripMenuItem()
        Me.GroupBox1.SuspendLayout()
        Me.MenuStrip2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.PrimNameTextBox)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.BirdsMaxHeightTextBox)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.BirdsBorderSizeTextBox)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.BirdsToleranceTextBox)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.DesiredSeparationTextBox)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.BirdsNeighbourDistanceTextBox)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.MaxForceTextBox)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.MaxSpeedTextBox)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.ChatChanelTextBox)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.BirdsFlockSizeDomain)
        Me.GroupBox1.Controls.Add(Me.BirdsModuleStartupbox)
        Me.GroupBox1.Location = New System.Drawing.Point(17, 75)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(317, 331)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Bird Module"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(85, 216)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(123, 13)
        Me.Label7.TabIndex = 24
        Me.Label7.Text = "Tolerance (default=25.0)"
        Me.ToolTip1.SetToolTip(Me.Label7, Global.Outworldz.My.Resources.Resources.Tolerance)
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(85, 293)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(148, 13)
        Me.Label11.TabIndex = 23
        Me.Label11.Text = "Prim Name (default=SeaGull1)"
        Me.ToolTip1.SetToolTip(Me.Label11, Global.Outworldz.My.Resources.Resources.How_High)
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
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(85, 268)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(129, 13)
        Me.Label10.TabIndex = 21
        Me.Label10.Text = "Max Height (default=45.0)"
        '
        'BirdsMaxHeightTextBox
        '
        Me.BirdsMaxHeightTextBox.Location = New System.Drawing.Point(7, 261)
        Me.BirdsMaxHeightTextBox.Name = "BirdsMaxHeightTextBox"
        Me.BirdsMaxHeightTextBox.Size = New System.Drawing.Size(43, 20)
        Me.BirdsMaxHeightTextBox.TabIndex = 10
        Me.BirdsMaxHeightTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(85, 241)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(129, 13)
        Me.Label9.TabIndex = 19
        Me.Label9.Text = "Border Size (default=25.0)"
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
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(85, 189)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(159, 13)
        Me.Label6.TabIndex = 13
        Me.Label6.Text = "Desired Separation (default=5.0)"
        Me.ToolTip1.SetToolTip(Me.Label6, Global.Outworldz.My.Resources.Resources.How_Far)
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
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(85, 164)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(163, 13)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "Neighbor Distance (default=25.0)"
        Me.ToolTip1.SetToolTip(Me.Label5, Global.Outworldz.My.Resources.Resources.How_Far)
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
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(85, 133)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(119, 13)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Max Force (default=0.2)"
        Me.ToolTip1.SetToolTip(Me.Label4, Global.Outworldz.My.Resources.Resources.Max_Accel)
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
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(85, 108)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(123, 13)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Max Speed (default=1.0)"
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
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(85, 81)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(71, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Chat Channel"
        Me.ToolTip1.SetToolTip(Me.Label2, Global.Outworldz.My.Resources.Resources.Which_Channel)
        '
        'ChatChanelTextBox
        '
        Me.ChatChanelTextBox.Location = New System.Drawing.Point(7, 77)
        Me.ChatChanelTextBox.Name = "ChatChanelTextBox"
        Me.ChatChanelTextBox.Size = New System.Drawing.Size(43, 20)
        Me.ChatChanelTextBox.TabIndex = 3
        Me.ChatChanelTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(85, 55)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(77, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Bird Flock Size"
        Me.ToolTip1.SetToolTip(Me.Label1, Global.Outworldz.My.Resources.Resources.Num_Birds)
        '
        'BirdsFlockSizeDomain
        '
        Me.BirdsFlockSizeDomain.Location = New System.Drawing.Point(7, 52)
        Me.BirdsFlockSizeDomain.Name = "BirdsFlockSizeDomain"
        Me.BirdsFlockSizeDomain.Size = New System.Drawing.Size(60, 20)
        Me.BirdsFlockSizeDomain.TabIndex = 2
        Me.BirdsFlockSizeDomain.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'BirdsModuleStartupbox
        '
        Me.BirdsModuleStartupbox.AutoSize = True
        Me.BirdsModuleStartupbox.Location = New System.Drawing.Point(7, 20)
        Me.BirdsModuleStartupbox.Name = "BirdsModuleStartupbox"
        Me.BirdsModuleStartupbox.Size = New System.Drawing.Size(118, 17)
        Me.BirdsModuleStartupbox.TabIndex = 1
        Me.BirdsModuleStartupbox.Text = Global.Outworldz.My.Resources.Resources.Enable_Birds_word
        Me.ToolTip1.SetToolTip(Me.BirdsModuleStartupbox, Global.Outworldz.My.Resources.Resources.Determines)
        Me.BirdsModuleStartupbox.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Image = Global.Outworldz.My.Resources.Resources.package_add
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button1.Location = New System.Drawing.Point(27, 37)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(170, 32)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = Global.Outworldz.My.Resources.Resources.Load_Bird_IAR_word
        Me.Button1.UseVisualStyleBackColor = True
        '
        'MenuStrip2
        '
        Me.MenuStrip2.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.MenuStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem30})
        Me.MenuStrip2.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip2.Name = "MenuStrip2"
        Me.MenuStrip2.Padding = New System.Windows.Forms.Padding(4, 1, 0, 1)
        Me.MenuStrip2.Size = New System.Drawing.Size(345, 30)
        Me.MenuStrip2.TabIndex = 1890
        Me.MenuStrip2.Text = "0"
        '
        'ToolStripMenuItem30
        '
        Me.ToolStripMenuItem30.Image = Global.Outworldz.My.Resources.Resources.about
        Me.ToolStripMenuItem30.Name = "ToolStripMenuItem30"
        Me.ToolStripMenuItem30.Size = New System.Drawing.Size(68, 28)
        Me.ToolStripMenuItem30.Text = Global.Outworldz.My.Resources.Resources.Help_word
        '
        'FormBird
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(345, 425)
        Me.Controls.Add(Me.MenuStrip2)
        Me.Controls.Add(Me.Button1)
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
    Friend WithEvents BirdsModuleStartupbox As CheckBox
    Friend WithEvents Label1 As Label
    Friend WithEvents BirdsFlockSizeDomain As DomainUpDown
    Friend WithEvents Label2 As Label
    Friend WithEvents ChatChanelTextBox As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents MaxSpeedTextBox As TextBox
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents Label4 As Label
    Friend WithEvents MaxForceTextBox As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents BirdsNeighbourDistanceTextBox As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents DesiredSeparationTextBox As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents BirdsToleranceTextBox As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents BirdsBorderSizeTextBox As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents BirdsMaxHeightTextBox As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents PrimNameTextBox As TextBox
    Friend WithEvents Button1 As Button
    Friend WithEvents MenuStrip2 As MenuStrip
    Friend WithEvents ToolStripMenuItem30 As ToolStripMenuItem
    Friend WithEvents Label7 As Label
End Class
