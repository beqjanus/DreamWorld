<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormPhysics
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormPhysics))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.PhysicsSeparate = New System.Windows.Forms.RadioButton()
        Me.PhysicsNone = New System.Windows.Forms.RadioButton()
        Me.PhysicsubODE = New System.Windows.Forms.RadioButton()
        Me.MenuStrip2 = New System.Windows.Forms.MenuStrip()
        Me.ToolStripMenuItem30 = New System.Windows.Forms.ToolStripMenuItem()
        Me.DatabaseSetupToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GroupBox1.SuspendLayout()
        Me.MenuStrip2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.PhysicsSeparate)
        Me.GroupBox1.Controls.Add(Me.PhysicsNone)
        Me.GroupBox1.Controls.Add(Me.PhysicsubODE)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 33)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(215, 104)
        Me.GroupBox1.TabIndex = 43
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = Global.Outworldz.My.Resources.Resources.Physics_Engine
        '
        'PhysicsSeparate
        '
        Me.PhysicsSeparate.AutoSize = True
        Me.PhysicsSeparate.Location = New System.Drawing.Point(6, 63)
        Me.PhysicsSeparate.Name = "PhysicsSeparate"
        Me.PhysicsSeparate.Size = New System.Drawing.Size(177, 17)
        Me.PhysicsSeparate.TabIndex = 13
        Me.PhysicsSeparate.TabStop = True
        Me.PhysicsSeparate.Text = Global.Outworldz.My.Resources.Resources.BP
        Me.PhysicsSeparate.UseVisualStyleBackColor = True
        '
        'PhysicsNone
        '
        Me.PhysicsNone.AutoSize = True
        Me.PhysicsNone.Location = New System.Drawing.Point(6, 20)
        Me.PhysicsNone.Name = "PhysicsNone"
        Me.PhysicsNone.Size = New System.Drawing.Size(51, 17)
        Me.PhysicsNone.TabIndex = 9
        Me.PhysicsNone.TabStop = True
        Me.PhysicsNone.Text = Global.Outworldz.My.Resources.Resources.None
        Me.PhysicsNone.UseVisualStyleBackColor = True
        '
        'PhysicsubODE
        '
        Me.PhysicsubODE.AutoSize = True
        Me.PhysicsubODE.Location = New System.Drawing.Point(6, 40)
        Me.PhysicsubODE.Name = "PhysicsubODE"
        Me.PhysicsubODE.Size = New System.Drawing.Size(153, 17)
        Me.PhysicsubODE.TabIndex = 11
        Me.PhysicsubODE.TabStop = True
        Me.PhysicsubODE.Text = Global.Outworldz.My.Resources.Resources.UBODE_words
        Me.PhysicsubODE.UseVisualStyleBackColor = True
        '
        'MenuStrip2
        '
        Me.MenuStrip2.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem30})
        Me.MenuStrip2.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip2.Name = "MenuStrip2"
        Me.MenuStrip2.Padding = New System.Windows.Forms.Padding(4, 1, 0, 1)
        Me.MenuStrip2.Size = New System.Drawing.Size(236, 26)
        Me.MenuStrip2.TabIndex = 1891
        Me.MenuStrip2.Text = Global.Outworldz.My.Resources.Resources._0
        '
        'ToolStripMenuItem30
        '
        Me.ToolStripMenuItem30.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DatabaseSetupToolStripMenuItem})
        Me.ToolStripMenuItem30.Image = Global.Outworldz.My.Resources.Resources.question_and_answer
        Me.ToolStripMenuItem30.Name = "ToolStripMenuItem30"
        Me.ToolStripMenuItem30.Size = New System.Drawing.Size(64, 24)
        Me.ToolStripMenuItem30.Text = Global.Outworldz.My.Resources.Resources.Help_word
        '
        'DatabaseSetupToolStripMenuItem
        '
        Me.DatabaseSetupToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.about
        Me.DatabaseSetupToolStripMenuItem.Name = "DatabaseSetupToolStripMenuItem"
        Me.DatabaseSetupToolStripMenuItem.Size = New System.Drawing.Size(184, 26)
        Me.DatabaseSetupToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Help_word
        '
        'FormPhysics
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(236, 149)
        Me.Controls.Add(Me.MenuStrip2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "FormPhysics"
        Me.Text = Global.Outworldz.My.Resources.Resources.Physics_word
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.MenuStrip2.ResumeLayout(False)
        Me.MenuStrip2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents PhysicsSeparate As RadioButton
    Friend WithEvents PhysicsNone As RadioButton
    Friend WithEvents PhysicsubODE As RadioButton
    Friend WithEvents MenuStrip2 As MenuStrip
    Friend WithEvents ToolStripMenuItem30 As ToolStripMenuItem
    Friend WithEvents DatabaseSetupToolStripMenuItem As ToolStripMenuItem
End Class
