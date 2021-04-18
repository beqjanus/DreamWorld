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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.MsgButton = New System.Windows.Forms.Button()
        Me.ShowConsoleButton = New System.Windows.Forms.Button()
        Me.EditButton1 = New System.Windows.Forms.Button()
        Me.ViewMapButton = New System.Windows.Forms.Button()
        Me.LoadOAR = New System.Windows.Forms.Button()
        Me.Teleport = New System.Windows.Forms.Button()
        Me.ViewLog = New System.Windows.Forms.Button()
        Me.StatsButton1 = New System.Windows.Forms.Button()
        Me.StartButton = New System.Windows.Forms.Button()
        Me.Restart = New System.Windows.Forms.Button()
        Me.StopButton = New System.Windows.Forms.Button()
        Me.SaveOAR = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.TableLayoutPanel1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 11)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(411, 265)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Region Controls"
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.MsgButton, 0, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.ShowConsoleButton, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.EditButton1, 1, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.ViewMapButton, 1, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.LoadOAR, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Teleport, 0, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.ViewLog, 1, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.StatsButton1, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.StartButton, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Restart, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.StopButton, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.SaveOAR, 1, 2)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(18, 24)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 6
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(374, 235)
        Me.TableLayoutPanel1.TabIndex = 1
        '
        'MsgButton
        '
        Me.MsgButton.Image = Global.Outworldz.My.Resources.Resources.document_text
        Me.MsgButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.MsgButton.Location = New System.Drawing.Point(3, 193)
        Me.MsgButton.Name = "MsgButton"
        Me.MsgButton.Size = New System.Drawing.Size(181, 32)
        Me.MsgButton.TabIndex = 5
        Me.MsgButton.Text = "Message"
        Me.MsgButton.UseVisualStyleBackColor = True
        '
        'ShowConsoleButton
        '
        Me.ShowConsoleButton.Image = Global.Outworldz.My.Resources.Resources.document_view
        Me.ShowConsoleButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ShowConsoleButton.Location = New System.Drawing.Point(3, 3)
        Me.ShowConsoleButton.Name = "ShowConsoleButton"
        Me.ShowConsoleButton.Size = New System.Drawing.Size(181, 32)
        Me.ShowConsoleButton.TabIndex = 0
        Me.ShowConsoleButton.Text = Global.Outworldz.My.Resources.Resources.View_Console_word
        Me.ShowConsoleButton.UseVisualStyleBackColor = True
        '
        'EditButton1
        '
        Me.EditButton1.Image = Global.Outworldz.My.Resources.Resources.document_dirty
        Me.EditButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.EditButton1.Location = New System.Drawing.Point(190, 193)
        Me.EditButton1.Name = "EditButton1"
        Me.EditButton1.Size = New System.Drawing.Size(181, 32)
        Me.EditButton1.TabIndex = 11
        Me.EditButton1.Text = Global.Outworldz.My.Resources.Resources.Edit_word
        Me.EditButton1.UseVisualStyleBackColor = True
        '
        'ViewMapButton
        '
        Me.ViewMapButton.Image = Global.Outworldz.My.Resources.Resources.table
        Me.ViewMapButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ViewMapButton.Location = New System.Drawing.Point(190, 155)
        Me.ViewMapButton.Name = "ViewMapButton"
        Me.ViewMapButton.Size = New System.Drawing.Size(181, 32)
        Me.ViewMapButton.TabIndex = 10
        Me.ViewMapButton.Text = Global.Outworldz.My.Resources.Resources.View_Map_word
        Me.ViewMapButton.UseVisualStyleBackColor = True
        '
        'LoadOAR
        '
        Me.LoadOAR.Image = Global.Outworldz.My.Resources.Resources.package_add
        Me.LoadOAR.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LoadOAR.Location = New System.Drawing.Point(190, 41)
        Me.LoadOAR.Name = "LoadOAR"
        Me.LoadOAR.Size = New System.Drawing.Size(181, 32)
        Me.LoadOAR.TabIndex = 7
        Me.LoadOAR.Text = "Load"
        Me.LoadOAR.UseVisualStyleBackColor = True
        '
        'Teleport
        '
        Me.Teleport.Image = Global.Outworldz.My.Resources.Resources.earth_view
        Me.Teleport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Teleport.Location = New System.Drawing.Point(3, 155)
        Me.Teleport.Name = "Teleport"
        Me.Teleport.Size = New System.Drawing.Size(181, 32)
        Me.Teleport.TabIndex = 4
        Me.Teleport.Text = Global.Outworldz.My.Resources.Resources.Teleport_word
        Me.Teleport.UseVisualStyleBackColor = True
        '
        'ViewLog
        '
        Me.ViewLog.Image = Global.Outworldz.My.Resources.Resources.document_view
        Me.ViewLog.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ViewLog.Location = New System.Drawing.Point(190, 117)
        Me.ViewLog.Name = "ViewLog"
        Me.ViewLog.Size = New System.Drawing.Size(181, 32)
        Me.ViewLog.TabIndex = 9
        Me.ViewLog.Text = Global.Outworldz.My.Resources.Resources.View_Log_word
        Me.ViewLog.UseVisualStyleBackColor = True
        '
        'StatsButton1
        '
        Me.StatsButton1.Image = Global.Outworldz.My.Resources.Resources.chart
        Me.StatsButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.StatsButton1.Location = New System.Drawing.Point(190, 3)
        Me.StatsButton1.Name = "StatsButton1"
        Me.StatsButton1.Size = New System.Drawing.Size(181, 32)
        Me.StatsButton1.TabIndex = 6
        Me.StatsButton1.Text = Global.Outworldz.My.Resources.Resources.View_Statistics_Word
        Me.StatsButton1.UseVisualStyleBackColor = True
        '
        'StartButton
        '
        Me.StartButton.Image = Global.Outworldz.My.Resources.Resources.media_play
        Me.StartButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.StartButton.Location = New System.Drawing.Point(3, 41)
        Me.StartButton.Name = "StartButton"
        Me.StartButton.Size = New System.Drawing.Size(181, 32)
        Me.StartButton.TabIndex = 1
        Me.StartButton.Text = Global.Outworldz.My.Resources.Resources.Start_word
        Me.StartButton.UseVisualStyleBackColor = True
        '
        'Restart
        '
        Me.Restart.Image = Global.Outworldz.My.Resources.Resources.recycle
        Me.Restart.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Restart.Location = New System.Drawing.Point(3, 79)
        Me.Restart.Name = "Restart"
        Me.Restart.Size = New System.Drawing.Size(181, 32)
        Me.Restart.TabIndex = 2
        Me.Restart.Text = Global.Outworldz.My.Resources.Resources.Restart_word
        Me.Restart.UseVisualStyleBackColor = True
        '
        'StopButton
        '
        Me.StopButton.Image = Global.Outworldz.My.Resources.Resources.media_stop_red1
        Me.StopButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.StopButton.Location = New System.Drawing.Point(3, 117)
        Me.StopButton.Name = "StopButton"
        Me.StopButton.Size = New System.Drawing.Size(181, 32)
        Me.StopButton.TabIndex = 3
        Me.StopButton.Text = Global.Outworldz.My.Resources.Resources.Stop_word
        Me.StopButton.UseVisualStyleBackColor = True
        '
        'SaveOAR
        '
        Me.SaveOAR.Image = Global.Outworldz.My.Resources.Resources.package
        Me.SaveOAR.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.SaveOAR.Location = New System.Drawing.Point(190, 79)
        Me.SaveOAR.Name = "SaveOAR"
        Me.SaveOAR.Size = New System.Drawing.Size(181, 32)
        Me.SaveOAR.TabIndex = 8
        Me.SaveOAR.Text = "Save"
        Me.SaveOAR.UseVisualStyleBackColor = True
        '
        'FormRegionPopup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(442, 286)
        Me.Controls.Add(Me.GroupBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FormRegionPopup"
        Me.TopMost = True
        Me.GroupBox1.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
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
    Friend WithEvents MsgButton As Button
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
End Class
