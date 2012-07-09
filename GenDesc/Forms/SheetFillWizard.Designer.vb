'
' Created by SharpDevelop.
' User: Administrator
' Date: 2009.03.17
' Time: 22:51
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Partial Class SheetFillWizard
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SheetFillWizard))
		Me.wizardControl1 = New WizardBase.WizardControl
		Me.isStart = New WizardBase.StartStep
		Me.isWikipedia = New WizardBase.IntermediateStep
		Me.textBox1 = New System.Windows.Forms.TextBox
		Me.label1 = New System.Windows.Forms.Label
		Me.isAbout = New WizardBase.IntermediateStep
		Me.textBox2 = New System.Windows.Forms.TextBox
		Me.label2 = New System.Windows.Forms.Label
		Me.isWhatIThink = New WizardBase.IntermediateStep
		Me.textBox3 = New System.Windows.Forms.TextBox
		Me.label3 = New System.Windows.Forms.Label
		Me.isYoutube = New WizardBase.IntermediateStep
		Me.textBox4 = New System.Windows.Forms.TextBox
		Me.label4 = New System.Windows.Forms.Label
		Me.isTrumbnails = New WizardBase.IntermediateStep
		Me.button1 = New System.Windows.Forms.Button
		Me.textBox5 = New System.Windows.Forms.TextBox
		Me.label5 = New System.Windows.Forms.Label
		Me.fsFinish = New WizardBase.FinishStep
		Me.checkBox2 = New System.Windows.Forms.CheckBox
		Me.checkBox1 = New System.Windows.Forms.CheckBox
		Me.label6 = New System.Windows.Forms.Label
		Me.textBox6 = New System.Windows.Forms.TextBox
		Me.label7 = New System.Windows.Forms.Label
		Me.label8 = New System.Windows.Forms.Label
		Me.isStart.SuspendLayout
		Me.isWikipedia.SuspendLayout
		Me.isAbout.SuspendLayout
		Me.isWhatIThink.SuspendLayout
		Me.isYoutube.SuspendLayout
		Me.isTrumbnails.SuspendLayout
		Me.fsFinish.SuspendLayout
		Me.SuspendLayout
		'
		'wizardControl1
		'
		Me.wizardControl1.BackButtonEnabled = false
		Me.wizardControl1.BackButtonVisible = true
		Me.wizardControl1.CancelButtonEnabled = true
		Me.wizardControl1.CancelButtonVisible = true
		Me.wizardControl1.EulaButtonEnabled = false
		Me.wizardControl1.EulaButtonText = "eula"
		Me.wizardControl1.EulaButtonVisible = false
		Me.wizardControl1.HelpButtonEnabled = false
		Me.wizardControl1.HelpButtonVisible = false
		Me.wizardControl1.Location = New System.Drawing.Point(0, 0)
		Me.wizardControl1.Name = "wizardControl1"
		Me.wizardControl1.NextButtonEnabled = true
		Me.wizardControl1.NextButtonVisible = true
		Me.wizardControl1.Size = New System.Drawing.Size(553, 318)
		Me.wizardControl1.WizardSteps.AddRange(New WizardBase.WizardStep() {Me.isStart, Me.isWikipedia, Me.isAbout, Me.isWhatIThink, Me.isYoutube, Me.isTrumbnails, Me.fsFinish})
		AddHandler Me.wizardControl1.FinishButtonClick, AddressOf Me.WizardControl1FinishButtonClick
		AddHandler Me.wizardControl1.NextButtonClick, AddressOf Me.WizardControl1NextButtonClick
		AddHandler Me.wizardControl1.BackButtonClick, AddressOf Me.WizardControl1BackButtonClick
		AddHandler Me.wizardControl1.CancelButtonClick, AddressOf Me.WizardControl1CancelButtonClick
		'
		'isStart
		'
		Me.isStart.BindingImage = CType(resources.GetObject("isStart.BindingImage"),System.Drawing.Image)
		Me.isStart.Controls.Add(Me.label8)
		Me.isStart.Icon = CType(resources.GetObject("isStart.Icon"),System.Drawing.Image)
		Me.isStart.Name = "isStart"
		Me.isStart.Subtitle = ""
		Me.isStart.Title = "Welcome to the Sheet Fill Wizard."
		AddHandler Me.isStart.Click, AddressOf Me.IsStartClick
		'
		'isWikipedia
		'
		Me.isWikipedia.BindingImage = CType(resources.GetObject("isWikipedia.BindingImage"),System.Drawing.Image)
		Me.isWikipedia.Controls.Add(Me.textBox1)
		Me.isWikipedia.Controls.Add(Me.label1)
		Me.isWikipedia.ForeColor = System.Drawing.SystemColors.ControlText
		Me.isWikipedia.Name = "isWikipedia"
		Me.isWikipedia.Subtitle = "Enter Wikipedia arcticle URL"
		Me.isWikipedia.Title = "Wikipedia URL"
		'
		'textBox1
		'
		Me.textBox1.Location = New System.Drawing.Point(39, 92)
		Me.textBox1.Name = "textBox1"
		Me.textBox1.Size = New System.Drawing.Size(500, 20)
		Me.textBox1.TabIndex = 1
		'
		'label1
		'
		Me.label1.AutoSize = true
		Me.label1.Location = New System.Drawing.Point(13, 75)
		Me.label1.Name = "label1"
		Me.label1.Size = New System.Drawing.Size(219, 13)
		Me.label1.TabIndex = 0
		Me.label1.Text = "Enter URL from english Wikipedia site article:"
		'
		'isAbout
		'
		Me.isAbout.BindingImage = CType(resources.GetObject("isAbout.BindingImage"),System.Drawing.Image)
		Me.isAbout.Controls.Add(Me.textBox2)
		Me.isAbout.Controls.Add(Me.label2)
		Me.isAbout.ForeColor = System.Drawing.SystemColors.ControlText
		Me.isAbout.Name = "isAbout"
		Me.isAbout.Subtitle = "Enter description"
		Me.isAbout.Title = "About"
		'
		'textBox2
		'
		Me.textBox2.Location = New System.Drawing.Point(38, 97)
		Me.textBox2.Multiline = true
		Me.textBox2.Name = "textBox2"
		Me.textBox2.Size = New System.Drawing.Size(500, 195)
		Me.textBox2.TabIndex = 3
		'
		'label2
		'
		Me.label2.AutoSize = true
		Me.label2.Location = New System.Drawing.Point(12, 80)
		Me.label2.Name = "label2"
		Me.label2.Size = New System.Drawing.Size(89, 13)
		Me.label2.TabIndex = 2
		Me.label2.Text = "Enter description:"
		'
		'isWhatIThink
		'
		Me.isWhatIThink.BindingImage = CType(resources.GetObject("isWhatIThink.BindingImage"),System.Drawing.Image)
		Me.isWhatIThink.Controls.Add(Me.textBox3)
		Me.isWhatIThink.Controls.Add(Me.label3)
		Me.isWhatIThink.ForeColor = System.Drawing.SystemColors.ControlText
		Me.isWhatIThink.Name = "isWhatIThink"
		Me.isWhatIThink.Subtitle = "Enter your opinion"
		Me.isWhatIThink.Title = "What I Think"
		'
		'textBox3
		'
		Me.textBox3.Location = New System.Drawing.Point(38, 91)
		Me.textBox3.Multiline = true
		Me.textBox3.Name = "textBox3"
		Me.textBox3.Size = New System.Drawing.Size(500, 201)
		Me.textBox3.TabIndex = 3
		'
		'label3
		'
		Me.label3.AutoSize = true
		Me.label3.Location = New System.Drawing.Point(12, 74)
		Me.label3.Name = "label3"
		Me.label3.Size = New System.Drawing.Size(92, 13)
		Me.label3.TabIndex = 2
		Me.label3.Text = "Give your opinion:"
		'
		'isYoutube
		'
		Me.isYoutube.BindingImage = CType(resources.GetObject("isYoutube.BindingImage"),System.Drawing.Image)
		Me.isYoutube.Controls.Add(Me.textBox4)
		Me.isYoutube.Controls.Add(Me.label4)
		Me.isYoutube.ForeColor = System.Drawing.SystemColors.ControlText
		Me.isYoutube.Name = "isYoutube"
		Me.isYoutube.Subtitle = "Paste Flash movies HTML code"
		Me.isYoutube.Title = "Flash movies code"
		'
		'textBox4
		'
		Me.textBox4.Location = New System.Drawing.Point(38, 95)
		Me.textBox4.Multiline = true
		Me.textBox4.Name = "textBox4"
		Me.textBox4.Size = New System.Drawing.Size(500, 197)
		Me.textBox4.TabIndex = 3
		'
		'label4
		'
		Me.label4.AutoSize = true
		Me.label4.Location = New System.Drawing.Point(12, 78)
		Me.label4.Name = "label4"
		Me.label4.Size = New System.Drawing.Size(161, 13)
		Me.label4.TabIndex = 2
		Me.label4.Text = "Paste Flash movies HTML code:"
		'
		'isTrumbnails
		'
		Me.isTrumbnails.BindingImage = CType(resources.GetObject("isTrumbnails.BindingImage"),System.Drawing.Image)
		Me.isTrumbnails.Controls.Add(Me.button1)
		Me.isTrumbnails.Controls.Add(Me.textBox5)
		Me.isTrumbnails.Controls.Add(Me.label5)
		Me.isTrumbnails.ForeColor = System.Drawing.SystemColors.ControlText
		Me.isTrumbnails.Name = "isTrumbnails"
		Me.isTrumbnails.Subtitle = "Select movie file for generating trumbnails"
		Me.isTrumbnails.Title = "Movie trumbnails"
		'
		'button1
		'
		Me.button1.Location = New System.Drawing.Point(464, 121)
		Me.button1.Name = "button1"
		Me.button1.Size = New System.Drawing.Size(75, 23)
		Me.button1.TabIndex = 4
		Me.button1.Text = "Browse..."
		Me.button1.UseVisualStyleBackColor = true
		AddHandler Me.button1.Click, AddressOf Me.Button1Click
		'
		'textBox5
		'
		Me.textBox5.Location = New System.Drawing.Point(38, 95)
		Me.textBox5.Name = "textBox5"
		Me.textBox5.Size = New System.Drawing.Size(500, 20)
		Me.textBox5.TabIndex = 3
		'
		'label5
		'
		Me.label5.AutoSize = true
		Me.label5.Location = New System.Drawing.Point(12, 78)
		Me.label5.Name = "label5"
		Me.label5.Size = New System.Drawing.Size(158, 13)
		Me.label5.TabIndex = 2
		Me.label5.Text = "Select file for making trumbnails:"
		'
		'fsFinish
		'
		Me.fsFinish.BindingImage = CType(resources.GetObject("fsFinish.BindingImage"),System.Drawing.Image)
		Me.fsFinish.Controls.Add(Me.checkBox2)
		Me.fsFinish.Controls.Add(Me.checkBox1)
		Me.fsFinish.Controls.Add(Me.label6)
		Me.fsFinish.Name = "fsFinish"
		'
		'checkBox2
		'
		Me.checkBox2.AutoSize = true
		Me.checkBox2.Location = New System.Drawing.Point(12, 103)
		Me.checkBox2.Name = "checkBox2"
		Me.checkBox2.Size = New System.Drawing.Size(203, 17)
		Me.checkBox2.TabIndex = 2
		Me.checkBox2.Text = "When finished show this wizard again"
		Me.checkBox2.UseVisualStyleBackColor = true
		'
		'checkBox1
		'
		Me.checkBox1.AutoSize = true
		Me.checkBox1.Location = New System.Drawing.Point(12, 80)
		Me.checkBox1.Name = "checkBox1"
		Me.checkBox1.Size = New System.Drawing.Size(186, 17)
		Me.checkBox1.TabIndex = 1
		Me.checkBox1.Text = "Copy generated code to clipboard"
		Me.checkBox1.UseVisualStyleBackColor = true
		AddHandler Me.checkBox1.CheckedChanged, AddressOf Me.CheckBox1CheckedChanged
		'
		'label6
		'
		Me.label6.AutoSize = true
		Me.label6.BackColor = System.Drawing.Color.Transparent
		Me.label6.Font = New System.Drawing.Font("Palatino Linotype", 18!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(186,Byte))
		Me.label6.Location = New System.Drawing.Point(12, 11)
		Me.label6.Name = "label6"
		Me.label6.Size = New System.Drawing.Size(118, 32)
		Me.label6.TabIndex = 0
		Me.label6.Text = "Fill Sheet!"
		'
		'textBox6
		'
		Me.textBox6.Location = New System.Drawing.Point(39, 92)
		Me.textBox6.Name = "textBox6"
		Me.textBox6.Size = New System.Drawing.Size(500, 20)
		Me.textBox6.TabIndex = 1
		'
		'label7
		'
		Me.label7.AutoSize = true
		Me.label7.Location = New System.Drawing.Point(13, 75)
		Me.label7.Name = "label7"
		Me.label7.Size = New System.Drawing.Size(219, 13)
		Me.label7.TabIndex = 0
		Me.label7.Text = "Enter URL from english Wikipedia site article:"
		'
		'label8
		'
		Me.label8.AutoSize = true
		Me.label8.Location = New System.Drawing.Point(175, 66)
		Me.label8.Name = "label8"
		Me.label8.Size = New System.Drawing.Size(224, 13)
		Me.label8.TabIndex = 0
		Me.label8.Text = "This wizard will let you more easier to fill sheet."
		'
		'SheetFillWizard
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(551, 317)
		Me.Controls.Add(Me.wizardControl1)
		Me.Name = "SheetFillWizard"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = "SheetFillWizard"
		AddHandler Load, AddressOf Me.SheetFillWizardLoad
		AddHandler Shown, AddressOf Me.SheetFillWizardShown
		AddHandler VisibleChanged, AddressOf Me.SheetFillWizardVisibleChanged
		Me.isStart.ResumeLayout(false)
		Me.isStart.PerformLayout
		Me.isWikipedia.ResumeLayout(false)
		Me.isWikipedia.PerformLayout
		Me.isAbout.ResumeLayout(false)
		Me.isAbout.PerformLayout
		Me.isWhatIThink.ResumeLayout(false)
		Me.isWhatIThink.PerformLayout
		Me.isYoutube.ResumeLayout(false)
		Me.isYoutube.PerformLayout
		Me.isTrumbnails.ResumeLayout(false)
		Me.isTrumbnails.PerformLayout
		Me.fsFinish.ResumeLayout(false)
		Me.fsFinish.PerformLayout
		Me.ResumeLayout(false)
	End Sub
	Private label8 As System.Windows.Forms.Label
	Private label7 As System.Windows.Forms.Label
	Private textBox6 As System.Windows.Forms.TextBox
	Private isStart As WizardBase.StartStep
	Private label6 As System.Windows.Forms.Label
	Private checkBox1 As System.Windows.Forms.CheckBox
	Private checkBox2 As System.Windows.Forms.CheckBox
	Private fsFinish As WizardBase.FinishStep
	Private label5 As System.Windows.Forms.Label
	Private textBox5 As System.Windows.Forms.TextBox
	Private button1 As System.Windows.Forms.Button
	Private isTrumbnails As WizardBase.IntermediateStep
	Private label4 As System.Windows.Forms.Label
	Private textBox4 As System.Windows.Forms.TextBox
	Private isYoutube As WizardBase.IntermediateStep
	Private label3 As System.Windows.Forms.Label
	Private textBox3 As System.Windows.Forms.TextBox
	Private isWhatIThink As WizardBase.IntermediateStep
	Private label2 As System.Windows.Forms.Label
	Private textBox2 As System.Windows.Forms.TextBox
	Private isAbout As WizardBase.IntermediateStep
	Private label1 As System.Windows.Forms.Label
	Private textBox1 As System.Windows.Forms.TextBox
	Private isWikipedia As WizardBase.IntermediateStep
	Private wizardControl1 As WizardBase.WizardControl
	
	
End Class
