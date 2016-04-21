'
' Created by SharpDevelop.
' User: Administrator
' Date: 2009.03.19
' Time: 13:34
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Partial Class ProgressBar
	Inherits System.Windows.Forms.Form
	
	''' <summary>
	''' Designer variable used to keep track of non-visual components.
	''' </summary>
	Private components As System.ComponentModel.IContainer
	
	''' <summary>
	''' Disposes resources used by the form.
	''' </summary>
	''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		If disposing Then
			If components IsNot Nothing Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(disposing)
	End Sub
	
	''' <summary>
	''' This method is required for Windows Forms designer support.
	''' Do not change the method contents inside the source code editor. The Forms designer might
	''' not be able to load this method if it was changed manually.
	''' </summary>
	Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ProgressBar))
        Me.lblDescription = New System.Windows.Forms.Label()
        Me.label1 = New System.Windows.Forms.Label()
        Me.TabledLayout = New System.Windows.Forms.TableLayoutPanel()
        Me.pbBar = New System.Windows.Forms.ProgressBar()
        Me.TabledLayout.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblDescription
        '
        Me.lblDescription.AutoEllipsis = True
        Me.lblDescription.AutoSize = True
        Me.lblDescription.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblDescription.Location = New System.Drawing.Point(3, 3)
        Me.lblDescription.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(477, 22)
        Me.lblDescription.TabIndex = 0
        Me.lblDescription.Text = "label1"
        '
        'label1
        '
        Me.label1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.label1.AutoSize = True
        Me.label1.BackColor = System.Drawing.Color.Transparent
        Me.label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(186, Byte))
        Me.label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(60, Byte), Integer), CType(CType(60, Byte), Integer), CType(CType(60, Byte), Integer), CType(CType(60, Byte), Integer))
        Me.label1.Location = New System.Drawing.Point(486, 25)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(75, 25)
        Me.label1.TabIndex = 2
        Me.label1.Text = "0 %"
        Me.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TabledLayout
        '
        Me.TabledLayout.ColumnCount = 2
        Me.TabledLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 85.72653!))
        Me.TabledLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.27347!))
        Me.TabledLayout.Controls.Add(Me.pbBar, 0, 1)
        Me.TabledLayout.Controls.Add(Me.label1, 1, 1)
        Me.TabledLayout.Controls.Add(Me.lblDescription, 0, 0)
        Me.TabledLayout.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabledLayout.Location = New System.Drawing.Point(0, 0)
        Me.TabledLayout.Name = "TabledLayout"
        Me.TabledLayout.RowCount = 2
        Me.TabledLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TabledLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TabledLayout.Size = New System.Drawing.Size(564, 50)
        Me.TabledLayout.TabIndex = 3
        '
        'pbBar
        '
        Me.pbBar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pbBar.Location = New System.Drawing.Point(3, 28)
        Me.pbBar.Name = "pbBar"
        Me.pbBar.Size = New System.Drawing.Size(477, 19)
        Me.pbBar.TabIndex = 2
        '
        'ProgressBar
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(564, 50)
        Me.ControlBox = False
        Me.Controls.Add(Me.TabledLayout)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ProgressBar"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.TabledLayout.ResumeLayout(False)
        Me.TabledLayout.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Private label1 As System.Windows.Forms.Label
    Private lblDescription As System.Windows.Forms.Label
    Friend WithEvents TabledLayout As TableLayoutPanel
    Private WithEvents pbBar As Windows.Forms.ProgressBar
End Class
