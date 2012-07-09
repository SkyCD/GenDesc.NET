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
		Me.lblDescription = New System.Windows.Forms.Label
		Me.pbBar = New System.Windows.Forms.ProgressBar
		Me.label1 = New System.Windows.Forms.Label
		Me.SuspendLayout
		'
		'lblDescription
		'
		Me.lblDescription.AutoSize = true
		Me.lblDescription.Location = New System.Drawing.Point(12, 9)
		Me.lblDescription.Name = "lblDescription"
		Me.lblDescription.Size = New System.Drawing.Size(35, 13)
		Me.lblDescription.TabIndex = 0
		Me.lblDescription.Text = "label1"
		'
		'pbBar
		'
		Me.pbBar.Location = New System.Drawing.Point(12, 37)
		Me.pbBar.Name = "pbBar"
		Me.pbBar.Size = New System.Drawing.Size(424, 22)
		Me.pbBar.TabIndex = 1
		'
		'label1
		'
		Me.label1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
						Or System.Windows.Forms.AnchorStyles.Left)  _
						Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.label1.AutoSize = true
		Me.label1.BackColor = System.Drawing.Color.Transparent
		Me.label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(186,Byte))
		Me.label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(60,Byte),Integer), CType(CType(60,Byte),Integer), CType(CType(60,Byte),Integer), CType(CType(60,Byte),Integer))
		Me.label1.Location = New System.Drawing.Point(442, 42)
		Me.label1.Name = "label1"
		Me.label1.Size = New System.Drawing.Size(27, 13)
		Me.label1.TabIndex = 2
		Me.label1.Text = "0 %"
		Me.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'ProgressBar
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(505, 87)
		Me.ControlBox = false
		Me.Controls.Add(Me.label1)
		Me.Controls.Add(Me.pbBar)
		Me.Controls.Add(Me.lblDescription)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
		Me.MaximumSize = New System.Drawing.Size(507, 89)
		Me.MinimumSize = New System.Drawing.Size(507, 89)
		Me.Name = "ProgressBar"
		Me.ShowInTaskbar = false
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		AddHandler Load, AddressOf Me.ProgressBarLoad
		Me.ResumeLayout(false)
		Me.PerformLayout
	End Sub
	Private label1 As System.Windows.Forms.Label
	Private pbBar As System.Windows.Forms.ProgressBar
	Private lblDescription As System.Windows.Forms.Label
End Class
