'
' Created by SharpDevelop.
' User: Administrator
' Date: 2009.03.19
' Time: 12:00
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Partial Class Config
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
		Me.label1 = New System.Windows.Forms.Label
		Me.textBox1 = New System.Windows.Forms.TextBox
		Me.button1 = New System.Windows.Forms.Button
		Me.checkBox1 = New System.Windows.Forms.CheckBox
		Me.groupBox1 = New System.Windows.Forms.GroupBox
		Me.checkBox3 = New System.Windows.Forms.CheckBox
		Me.checkBox2 = New System.Windows.Forms.CheckBox
		Me.button2 = New System.Windows.Forms.Button
		Me.button3 = New System.Windows.Forms.Button
		Me.button4 = New System.Windows.Forms.Button
		Me.groupBox1.SuspendLayout
		Me.SuspendLayout
		'
		'label1
		'
		Me.label1.AutoSize = true
		Me.label1.Location = New System.Drawing.Point(12, 9)
		Me.label1.Name = "label1"
		Me.label1.Size = New System.Drawing.Size(82, 13)
		Me.label1.TabIndex = 0
		Me.label1.Text = "ffmpeg location:"
		'
		'textBox1
		'
		Me.textBox1.Location = New System.Drawing.Point(12, 25)
		Me.textBox1.Name = "textBox1"
		Me.textBox1.Size = New System.Drawing.Size(478, 20)
		Me.textBox1.TabIndex = 1
		'
		'button1
		'
		Me.button1.Location = New System.Drawing.Point(415, 51)
		Me.button1.Name = "button1"
		Me.button1.Size = New System.Drawing.Size(75, 23)
		Me.button1.TabIndex = 2
		Me.button1.Text = "Browse..."
		Me.button1.UseVisualStyleBackColor = true
		AddHandler Me.button1.Click, AddressOf Me.Button1Click
		'
		'checkBox1
		'
		Me.checkBox1.AutoSize = true
		Me.checkBox1.Location = New System.Drawing.Point(18, 80)
		Me.checkBox1.Name = "checkBox1"
		Me.checkBox1.Size = New System.Drawing.Size(255, 17)
		Me.checkBox1.TabIndex = 3
		Me.checkBox1.Text = "Autosave Sheet Fill Wizard's checkboxes values"
		Me.checkBox1.UseVisualStyleBackColor = true
		AddHandler Me.checkBox1.CheckedChanged, AddressOf Me.CheckBox1CheckedChanged
		'
		'groupBox1
		'
		Me.groupBox1.Controls.Add(Me.checkBox3)
		Me.groupBox1.Controls.Add(Me.checkBox2)
		Me.groupBox1.Location = New System.Drawing.Point(12, 80)
		Me.groupBox1.Name = "groupBox1"
		Me.groupBox1.Size = New System.Drawing.Size(478, 78)
		Me.groupBox1.TabIndex = 4
		Me.groupBox1.TabStop = false
		Me.groupBox1.Text = "groupBox1"
		'
		'checkBox3
		'
		Me.checkBox3.AutoSize = true
		Me.checkBox3.Location = New System.Drawing.Point(20, 46)
		Me.checkBox3.Name = "checkBox3"
		Me.checkBox3.Size = New System.Drawing.Size(110, 17)
		Me.checkBox3.TabIndex = 5
		Me.checkBox3.Text = "Start wizard again"
		Me.checkBox3.UseVisualStyleBackColor = true
		'
		'checkBox2
		'
		Me.checkBox2.AutoSize = true
		Me.checkBox2.Location = New System.Drawing.Point(20, 23)
		Me.checkBox2.Name = "checkBox2"
		Me.checkBox2.Size = New System.Drawing.Size(167, 17)
		Me.checkBox2.TabIndex = 4
		Me.checkBox2.Text = "Copy generated text clipboard"
		Me.checkBox2.UseVisualStyleBackColor = true
		'
		'button2
		'
		Me.button2.Location = New System.Drawing.Point(415, 185)
		Me.button2.Name = "button2"
		Me.button2.Size = New System.Drawing.Size(75, 23)
		Me.button2.TabIndex = 5
		Me.button2.Text = "Cancel"
		Me.button2.UseVisualStyleBackColor = true
		AddHandler Me.button2.Click, AddressOf Me.Button2Click
		'
		'button3
		'
		Me.button3.Location = New System.Drawing.Point(334, 185)
		Me.button3.Name = "button3"
		Me.button3.Size = New System.Drawing.Size(75, 23)
		Me.button3.TabIndex = 6
		Me.button3.Text = "Ok"
		Me.button3.UseVisualStyleBackColor = true
		AddHandler Me.button3.Click, AddressOf Me.Button3Click
		'
		'button4
		'
		Me.button4.Location = New System.Drawing.Point(334, 51)
		Me.button4.Name = "button4"
		Me.button4.Size = New System.Drawing.Size(75, 23)
		Me.button4.TabIndex = 7
		Me.button4.Text = "Auto Find"
		Me.button4.UseVisualStyleBackColor = true
		AddHandler Me.button4.Click, AddressOf Me.Button4Click
		'
		'Config
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(502, 222)
		Me.Controls.Add(Me.button4)
		Me.Controls.Add(Me.button3)
		Me.Controls.Add(Me.button2)
		Me.Controls.Add(Me.checkBox1)
		Me.Controls.Add(Me.button1)
		Me.Controls.Add(Me.textBox1)
		Me.Controls.Add(Me.label1)
		Me.Controls.Add(Me.groupBox1)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
		Me.MaximizeBox = false
		Me.MinimizeBox = false
		Me.Name = "Config"
		Me.ShowInTaskbar = false
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = "Config"
		AddHandler Load, AddressOf Me.ConfigLoad
		AddHandler Activated, AddressOf Me.ConfigActivated
		Me.groupBox1.ResumeLayout(false)
		Me.groupBox1.PerformLayout
		Me.ResumeLayout(false)
		Me.PerformLayout
	End Sub
	Private button4 As System.Windows.Forms.Button
	Private button3 As System.Windows.Forms.Button
	Private button2 As System.Windows.Forms.Button
	Private checkBox2 As System.Windows.Forms.CheckBox
	Private checkBox3 As System.Windows.Forms.CheckBox
	Private groupBox1 As System.Windows.Forms.GroupBox
	Private checkBox1 As System.Windows.Forms.CheckBox
	Private button1 As System.Windows.Forms.Button
	Private textBox1 As System.Windows.Forms.TextBox
	Private label1 As System.Windows.Forms.Label
End Class
