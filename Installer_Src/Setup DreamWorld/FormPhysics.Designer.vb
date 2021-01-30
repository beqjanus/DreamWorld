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
        Me.PhysicsUbODE = New System.Windows.Forms.RadioButton()
        Me.CheckBox3 = New System.Windows.Forms.CheckBox()
        Me.HybridPhysics = New System.Windows.Forms.RadioButton()
        Me.BulletPhysics = New System.Windows.Forms.RadioButton()
        Me.PhysicsSeparate = New System.Windows.Forms.RadioButton()
        Me.PhysicsNone = New System.Windows.Forms.RadioButton()
        Me.MenuStrip2 = New System.Windows.Forms.MenuStrip()
        Me.ToolStripMenuItem30 = New System.Windows.Forms.ToolStripMenuItem()
        Me.DatabaseSetupToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GroupBox1.SuspendLayout()
        Me.MenuStrip2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.PhysicsUbODE)
        Me.GroupBox1.Controls.Add(Me.CheckBox3)
        Me.GroupBox1.Controls.Add(Me.HybridPhysics)
        Me.GroupBox1.Controls.Add(Me.BulletPhysics)
        Me.GroupBox1.Controls.Add(Me.PhysicsSeparate)
        Me.GroupBox1.Controls.Add(Me.PhysicsNone)
        Me.GroupBox1.Location = New System.Drawing.Point(18, 50)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Size = New System.Drawing.Size(323, 258)
        Me.GroupBox1.TabIndex = 43
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Physics Engine"
        '
        'PhysicsUbODE
        '
        Me.PhysicsUbODE.AutoSize = True
        Me.PhysicsUbODE.Location = New System.Drawing.Point(9, 62)
        Me.PhysicsUbODE.Margin = New System.Windows.Forms.Padding(4)
        Me.PhysicsUbODE.Name = "PhysicsUbODE"
        Me.PhysicsUbODE.Size = New System.Drawing.Size(225, 24)
        Me.PhysicsUbODE.TabIndex = 21
        Me.PhysicsUbODE.TabStop = True
        Me.PhysicsUbODE.Text = Global.Outworldz.My.Resources.Resources.UBODE_words
        Me.PhysicsUbODE.UseVisualStyleBackColor = True
        '
        'CheckBox3
        '
        Me.CheckBox3.AutoSize = True
        Me.CheckBox3.Location = New System.Drawing.Point(9, 227)
        Me.CheckBox3.Name = "CheckBox3"
        Me.CheckBox3.Size = New System.Drawing.Size(89, 24)
        Me.CheckBox3.TabIndex = 20
        Me.CheckBox3.Text = "Ragdoll"
        Me.CheckBox3.UseVisualStyleBackColor = True
        '
        'HybridPhysics
        '
        Me.HybridPhysics.AutoSize = True
        Me.HybridPhysics.Location = New System.Drawing.Point(9, 158)
        Me.HybridPhysics.Margin = New System.Windows.Forms.Padding(4)
        Me.HybridPhysics.Name = "HybridPhysics"
        Me.HybridPhysics.Size = New System.Drawing.Size(79, 24)
        Me.HybridPhysics.TabIndex = 16
        Me.HybridPhysics.TabStop = True
        Me.HybridPhysics.Text = "Hybrid"
        Me.HybridPhysics.UseVisualStyleBackColor = True
        '
        'BulletPhysics
        '
        Me.BulletPhysics.AutoSize = True
        Me.BulletPhysics.Location = New System.Drawing.Point(9, 94)
        Me.BulletPhysics.Margin = New System.Windows.Forms.Padding(4)
        Me.BulletPhysics.Name = "BulletPhysics"
        Me.BulletPhysics.Size = New System.Drawing.Size(134, 24)
        Me.BulletPhysics.TabIndex = 15
        Me.BulletPhysics.TabStop = True
        Me.BulletPhysics.Text = "Bullet physics "
        Me.BulletPhysics.UseVisualStyleBackColor = True
        '
        'PhysicsSeparate
        '
        Me.PhysicsSeparate.AutoSize = True
        Me.PhysicsSeparate.Location = New System.Drawing.Point(9, 126)
        Me.PhysicsSeparate.Margin = New System.Windows.Forms.Padding(4)
        Me.PhysicsSeparate.Name = "PhysicsSeparate"
        Me.PhysicsSeparate.Size = New System.Drawing.Size(263, 24)
        Me.PhysicsSeparate.TabIndex = 13
        Me.PhysicsSeparate.TabStop = True
        Me.PhysicsSeparate.Text = Global.Outworldz.My.Resources.Resources.BP
        Me.PhysicsSeparate.UseVisualStyleBackColor = True
        '
        'PhysicsNone
        '
        Me.PhysicsNone.AutoSize = True
        Me.PhysicsNone.Location = New System.Drawing.Point(9, 30)
        Me.PhysicsNone.Margin = New System.Windows.Forms.Padding(4)
        Me.PhysicsNone.Name = "PhysicsNone"
        Me.PhysicsNone.Size = New System.Drawing.Size(72, 24)
        Me.PhysicsNone.TabIndex = 9
        Me.PhysicsNone.TabStop = True
        Me.PhysicsNone.Text = Global.Outworldz.My.Resources.Resources.None
        Me.PhysicsNone.UseVisualStyleBackColor = True
        '
        'MenuStrip2
        '
        Me.MenuStrip2.GripMargin = New System.Windows.Forms.Padding(2, 2, 0, 2)
        Me.MenuStrip2.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.MenuStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem30})
        Me.MenuStrip2.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip2.Name = "MenuStrip2"
        Me.MenuStrip2.Size = New System.Drawing.Size(354, 33)
        Me.MenuStrip2.TabIndex = 1891
        Me.MenuStrip2.Text = "0"
        '
        'ToolStripMenuItem30
        '
        Me.ToolStripMenuItem30.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DatabaseSetupToolStripMenuItem})
        Me.ToolStripMenuItem30.Image = Global.Outworldz.My.Resources.Resources.question_and_answer
        Me.ToolStripMenuItem30.Name = "ToolStripMenuItem30"
        Me.ToolStripMenuItem30.Size = New System.Drawing.Size(89, 29)
        Me.ToolStripMenuItem30.Text = Global.Outworldz.My.Resources.Resources.Help_word
        '
        'DatabaseSetupToolStripMenuItem
        '
        Me.DatabaseSetupToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.about
        Me.DatabaseSetupToolStripMenuItem.Name = "DatabaseSetupToolStripMenuItem"
        Me.DatabaseSetupToolStripMenuItem.Size = New System.Drawing.Size(151, 34)
        Me.DatabaseSetupToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Help_word
        '
        'FormPhysics
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(144.0!, 144.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(354, 321)
        Me.Controls.Add(Me.MenuStrip2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.Name = "FormPhysics"
        Me.Text = "Physics"
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
    Friend WithEvents MenuStrip2 As MenuStrip
    Friend WithEvents ToolStripMenuItem30 As ToolStripMenuItem
    Friend WithEvents DatabaseSetupToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CheckBox3 As CheckBox
    Friend WithEvents HybridPhysics As RadioButton
    Friend WithEvents BulletPhysics As RadioButton
    Friend WithEvents PhysicsUbODE As RadioButton
End Class
