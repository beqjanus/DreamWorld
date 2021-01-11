<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormRegionPopup
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormRegionPopup))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.LoadOAR = New System.Windows.Forms.Button()
        Me.Restart = New System.Windows.Forms.Button()
        Me.ViewMapButton = New System.Windows.Forms.Button()
        Me.ShowConsoleButton = New System.Windows.Forms.Button()
        Me.ViewLog = New System.Windows.Forms.Button()
        Me.StatsButton1 = New System.Windows.Forms.Button()
        Me.Teleport = New System.Windows.Forms.Button()
        Me.EditButton1 = New System.Windows.Forms.Button()
        Me.StartButton = New System.Windows.Forms.Button()
        Me.SaveOAR = New System.Windows.Forms.Button()
        Me.StopButton = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.LoadOAR)
        Me.GroupBox1.Controls.Add(Me.Restart)
        Me.GroupBox1.Controls.Add(Me.ViewMapButton)
        Me.GroupBox1.Controls.Add(Me.ShowConsoleButton)
        Me.GroupBox1.Controls.Add(Me.ViewLog)
        Me.GroupBox1.Controls.Add(Me.StatsButton1)
        Me.GroupBox1.Controls.Add(Me.Teleport)
        Me.GroupBox1.Controls.Add(Me.EditButton1)
        Me.GroupBox1.Controls.Add(Me.StartButton)
        Me.GroupBox1.Controls.Add(Me.SaveOAR)
        Me.GroupBox1.Controls.Add(Me.StopButton)
        Me.GroupBox1.Location = New System.Drawing.Point(16, 14)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox1.Size = New System.Drawing.Size(467, 324)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Region Controls"
        '
        'LoadOAR
        '
        Me.LoadOAR.Image = Global.Outworldz.My.Resources.package_add
        Me.LoadOAR.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LoadOAR.Location = New System.Drawing.Point(239, 76)
        Me.LoadOAR.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.LoadOAR.Name = "LoadOAR"
        Me.LoadOAR.Size = New System.Drawing.Size(211, 39)
        Me.LoadOAR.TabIndex = 6
        Me.LoadOAR.Text = "Load"
        Me.LoadOAR.UseVisualStyleBackColor = True
        '
        'Restart
        '
        Me.Restart.Image = Global.Outworldz.My.Resources.recycle
        Me.Restart.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Restart.Location = New System.Drawing.Point(25, 123)
        Me.Restart.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Restart.Name = "Restart"
        Me.Restart.Size = New System.Drawing.Size(205, 39)
        Me.Restart.TabIndex = 3
        Me.Restart.Text = Global.Outworldz.My.Resources.Restart_word
        Me.Restart.UseVisualStyleBackColor = True
        '
        'ViewMapButton
        '
        Me.ViewMapButton.Image = Global.Outworldz.My.Resources.table
        Me.ViewMapButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ViewMapButton.Location = New System.Drawing.Point(239, 216)
        Me.ViewMapButton.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ViewMapButton.Name = "ViewMapButton"
        Me.ViewMapButton.Size = New System.Drawing.Size(205, 39)
        Me.ViewMapButton.TabIndex = 9
        Me.ViewMapButton.Text = Global.Outworldz.My.Resources.View_Map_word
        Me.ViewMapButton.UseVisualStyleBackColor = True
        '
        'ShowConsoleButton
        '
        Me.ShowConsoleButton.Image = Global.Outworldz.My.Resources.document_view
        Me.ShowConsoleButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ShowConsoleButton.Location = New System.Drawing.Point(25, 30)
        Me.ShowConsoleButton.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ShowConsoleButton.Name = "ShowConsoleButton"
        Me.ShowConsoleButton.Size = New System.Drawing.Size(205, 39)
        Me.ShowConsoleButton.TabIndex = 1
        Me.ShowConsoleButton.Text = Global.Outworldz.My.Resources.View_Console_word
        Me.ShowConsoleButton.UseVisualStyleBackColor = True
        '
        'ViewLog
        '
        Me.ViewLog.Image = Global.Outworldz.My.Resources.document_view
        Me.ViewLog.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ViewLog.Location = New System.Drawing.Point(239, 169)
        Me.ViewLog.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ViewLog.Name = "ViewLog"
        Me.ViewLog.Size = New System.Drawing.Size(205, 39)
        Me.ViewLog.TabIndex = 8
        Me.ViewLog.Text = Global.Outworldz.My.Resources.View_Log_word
        Me.ViewLog.UseVisualStyleBackColor = True
        '
        'StatsButton1
        '
        Me.StatsButton1.Image = Global.Outworldz.My.Resources.chart
        Me.StatsButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.StatsButton1.Location = New System.Drawing.Point(239, 30)
        Me.StatsButton1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.StatsButton1.Name = "StatsButton1"
        Me.StatsButton1.Size = New System.Drawing.Size(205, 39)
        Me.StatsButton1.TabIndex = 4
        Me.StatsButton1.Text = Global.Outworldz.My.Resources.View_Statistics_Word
        Me.StatsButton1.UseVisualStyleBackColor = True
        '
        'Teleport
        '
        Me.Teleport.Image = Global.Outworldz.My.Resources.earth_view
        Me.Teleport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Teleport.Location = New System.Drawing.Point(26, 216)
        Me.Teleport.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Teleport.Name = "Teleport"
        Me.Teleport.Size = New System.Drawing.Size(205, 39)
        Me.Teleport.TabIndex = 5
        Me.Teleport.Text = Global.Outworldz.My.Resources.Teleport_word
        Me.Teleport.UseVisualStyleBackColor = True
        '
        'EditButton1
        '
        Me.EditButton1.Image = Global.Outworldz.My.Resources.document_dirty
        Me.EditButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.EditButton1.Location = New System.Drawing.Point(239, 263)
        Me.EditButton1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.EditButton1.Name = "EditButton1"
        Me.EditButton1.Size = New System.Drawing.Size(205, 39)
        Me.EditButton1.TabIndex = 10
        Me.EditButton1.Text = Global.Outworldz.My.Resources.Edit_word
        Me.EditButton1.UseVisualStyleBackColor = True
        '
        'StartButton
        '
        Me.StartButton.Image = Global.Outworldz.My.Resources.media_play
        Me.StartButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.StartButton.Location = New System.Drawing.Point(25, 76)
        Me.StartButton.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.StartButton.Name = "StartButton"
        Me.StartButton.Size = New System.Drawing.Size(205, 39)
        Me.StartButton.TabIndex = 2
        Me.StartButton.Text = Global.Outworldz.My.Resources.Start_word
        Me.StartButton.UseVisualStyleBackColor = True
        '
        'SaveOAR
        '
        Me.SaveOAR.Image = Global.Outworldz.My.Resources.package
        Me.SaveOAR.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.SaveOAR.Location = New System.Drawing.Point(239, 122)
        Me.SaveOAR.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.SaveOAR.Name = "SaveOAR"
        Me.SaveOAR.Size = New System.Drawing.Size(211, 39)
        Me.SaveOAR.TabIndex = 7
        Me.SaveOAR.Text = "Save"
        Me.SaveOAR.UseVisualStyleBackColor = True
        '
        'StopButton
        '
        Me.StopButton.Image = Global.Outworldz.My.Resources.media_stop_red1
        Me.StopButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.StopButton.Location = New System.Drawing.Point(25, 170)
        Me.StopButton.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.StopButton.Name = "StopButton"
        Me.StopButton.Size = New System.Drawing.Size(205, 39)
        Me.StopButton.TabIndex = 5
        Me.StopButton.Text = Global.Outworldz.My.Resources.Stop_word
        Me.StopButton.UseVisualStyleBackColor = True
        '
        'FormRegionPopup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(499, 352)
        Me.Controls.Add(Me.GroupBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FormRegionPopup"
        Me.TopMost = True
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents EditButton1 As Button
    Friend WithEvents StartButton As Button
    Friend WithEvents SaveOAR As Button
    Friend WithEvents StopButton As Button
    Friend WithEvents Teleport As Button
    Friend WithEvents StatsButton1 As Button
    Friend WithEvents ViewLog As Button
    Friend WithEvents ShowConsoleButton As Button
    Friend WithEvents ViewMapButton As Button
    Friend WithEvents LoadOAR As Button
    Friend WithEvents Restart As Button
End Class
