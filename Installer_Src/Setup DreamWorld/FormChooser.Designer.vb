<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Choice
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Choice))
        Me.DataGridView = New System.Windows.Forms.DataGridView()
        Me.Group = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CancelButton1 = New System.Windows.Forms.Button()
        Me.OKButton1 = New System.Windows.Forms.Button()
        CType(Me.DataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridView
        '
        Me.DataGridView.AllowUserToAddRows = False
        Me.DataGridView.AllowUserToDeleteRows = False
        Me.DataGridView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.DataGridView.ColumnHeadersHeight = 34
        Me.DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.DataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Group})
        Me.DataGridView.Location = New System.Drawing.Point(-2, -3)
        Me.DataGridView.MultiSelect = False
        Me.DataGridView.Name = "DataGridView"
        Me.DataGridView.ReadOnly = True
        Me.DataGridView.RowHeadersWidth = 40
        Me.DataGridView.Size = New System.Drawing.Size(233, 274)
        Me.DataGridView.TabIndex = 2
        '
        'Group
        '
        Me.Group.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Group.HeaderText = "Group"
        Me.Group.MaxInputLength = 50
        Me.Group.MinimumWidth = 8
        Me.Group.Name = My.Resources.Group_word
        Me.Group.ReadOnly = True
        Me.Group.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Group.ToolTipText = Global.Outworldz.My.Resources.Click_2_Choose
        '
        'CancelButton1
        '
        Me.CancelButton1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.CancelButton1.Location = New System.Drawing.Point(0, 273)
        Me.CancelButton1.Margin = New System.Windows.Forms.Padding(2)
        Me.CancelButton1.Name = "CancelButton1"
        Me.CancelButton1.Size = New System.Drawing.Size(231, 24)
        Me.CancelButton1.TabIndex = 6
        Me.CancelButton1.Text = Global.Outworldz.My.Resources.Cancel_word
        Me.CancelButton1.UseVisualStyleBackColor = True
        '
        'OKButton1
        '
        Me.OKButton1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.OKButton1.Location = New System.Drawing.Point(0, 297)
        Me.OKButton1.Margin = New System.Windows.Forms.Padding(2)
        Me.OKButton1.Name = "OKButton1"
        Me.OKButton1.Size = New System.Drawing.Size(231, 24)
        Me.OKButton1.TabIndex = 5
        Me.OKButton1.Text = Global.Outworldz.My.Resources.Ok
        Me.OKButton1.UseVisualStyleBackColor = True
        '
        'Choice
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(231, 321)
        Me.Controls.Add(Me.CancelButton1)
        Me.Controls.Add(Me.OKButton1)
        Me.Controls.Add(Me.DataGridView)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Choice"
        Me.Text = My.Resources.Choose_Region_word
        Me.TopMost = True
        CType(Me.DataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DataGridView As DataGridView
    Friend WithEvents Group As DataGridViewTextBoxColumn
    Friend WithEvents CancelButton1 As Button
    Friend WithEvents OKButton1 As Button
End Class
