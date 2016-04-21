'
' Created by SharpDevelop.
' User: Administrator
' Date: 2009.03.10
' Time: 14:22
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Partial Class MainForm
	Inherits System.Windows.Forms.Form

    ''' <summary>
    ''' Designer variable used to keep track of non-visual components.
    ''' </summary>
    Private WithEvents components As System.ComponentModel.IContainer

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Me.tabPage2 = New System.Windows.Forms.TabPage()
        Me.textBox1 = New System.Windows.Forms.TextBox()
        Me.tabPage1 = New System.Windows.Forms.TabPage()
        Me.propertyGrid1 = New System.Windows.Forms.PropertyGrid()
        Me.tabControl1 = New System.Windows.Forms.TabControl()
        Me.tscbGeneratedTextLanguage = New System.Windows.Forms.ToolStripComboBox()
        Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.toolStripButton2 = New System.Windows.Forms.ToolStripButton()
        Me.toolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.toolStripButton3 = New System.Windows.Forms.ToolStripButton()
        Me.toolStripButton4 = New System.Windows.Forms.ToolStripButton()
        Me.toolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.toolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.toolStripButton6 = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.toolStripButton5 = New System.Windows.Forms.ToolStripButton()
        Me.tabPage2.SuspendLayout()
        Me.tabPage1.SuspendLayout()
        Me.tabControl1.SuspendLayout()
        Me.toolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'tabPage2
        '
        Me.tabPage2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.tabPage2.Controls.Add(Me.textBox1)
        Me.tabPage2.Location = New System.Drawing.Point(4, 25)
        Me.tabPage2.Name = "tabPage2"
        Me.tabPage2.Size = New System.Drawing.Size(798, 471)
        Me.tabPage2.TabIndex = 1
        Me.tabPage2.Text = "Generated Data"
        Me.tabPage2.UseVisualStyleBackColor = True
        '
        'textBox1
        '
        Me.textBox1.BackColor = System.Drawing.SystemColors.Window
        Me.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.textBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.textBox1.Location = New System.Drawing.Point(0, 0)
        Me.textBox1.Multiline = True
        Me.textBox1.Name = "textBox1"
        Me.textBox1.ReadOnly = True
        Me.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.textBox1.Size = New System.Drawing.Size(794, 467)
        Me.textBox1.TabIndex = 1
        Me.textBox1.WordWrap = False
        '
        'tabPage1
        '
        Me.tabPage1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.tabPage1.Controls.Add(Me.propertyGrid1)
        Me.tabPage1.Location = New System.Drawing.Point(4, 25)
        Me.tabPage1.Name = "tabPage1"
        Me.tabPage1.Size = New System.Drawing.Size(798, 471)
        Me.tabPage1.TabIndex = 0
        Me.tabPage1.Text = "Data Sheet"
        Me.tabPage1.UseVisualStyleBackColor = True
        '
        'propertyGrid1
        '
        Me.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.propertyGrid1.HelpVisible = False
        Me.propertyGrid1.Location = New System.Drawing.Point(0, 0)
        Me.propertyGrid1.Name = "propertyGrid1"
        Me.propertyGrid1.Size = New System.Drawing.Size(794, 467)
        Me.propertyGrid1.TabIndex = 1
        Me.propertyGrid1.UseCompatibleTextRendering = True
        '
        'tabControl1
        '
        Me.tabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons
        Me.tabControl1.Controls.Add(Me.tabPage1)
        Me.tabControl1.Controls.Add(Me.tabPage2)
        Me.tabControl1.HotTrack = True
        Me.tabControl1.Location = New System.Drawing.Point(0, 25)
        Me.tabControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.tabControl1.Name = "tabControl1"
        Me.tabControl1.SelectedIndex = 0
        Me.tabControl1.Size = New System.Drawing.Size(806, 500)
        Me.tabControl1.TabIndex = 9
        '
        'tscbGeneratedTextLanguage
        '
        Me.tscbGeneratedTextLanguage.AutoToolTip = True
        Me.tscbGeneratedTextLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.tscbGeneratedTextLanguage.DropDownWidth = 231
        Me.tscbGeneratedTextLanguage.Name = "tscbGeneratedTextLanguage"
        Me.tscbGeneratedTextLanguage.Size = New System.Drawing.Size(231, 25)
        Me.tscbGeneratedTextLanguage.Sorted = True
        Me.tscbGeneratedTextLanguage.ToolTipText = "Generated Data Template"
        '
        'toolStripSeparator1
        '
        Me.toolStripSeparator1.Name = "toolStripSeparator1"
        Me.toolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'toolStripButton2
        '
        Me.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.toolStripButton2.Image = CType(resources.GetObject("toolStripButton2.Image"), System.Drawing.Image)
        Me.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.toolStripButton2.Name = "toolStripButton2"
        Me.toolStripButton2.Size = New System.Drawing.Size(73, 22)
        Me.toolStripButton2.Text = "Wiki Import"
        Me.toolStripButton2.ToolTipText = "Import Sheet Data From English Wikipedia URL"
        '
        'toolStripButton1
        '
        Me.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.toolStripButton1.Image = CType(resources.GetObject("toolStripButton1.Image"), System.Drawing.Image)
        Me.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.toolStripButton1.Name = "toolStripButton1"
        Me.toolStripButton1.Size = New System.Drawing.Size(97, 22)
        Me.toolStripButton1.Text = "Sheet Fill Wizard"
        '
        'toolStripButton3
        '
        Me.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.toolStripButton3.Image = CType(resources.GetObject("toolStripButton3.Image"), System.Drawing.Image)
        Me.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.toolStripButton3.Name = "toolStripButton3"
        Me.toolStripButton3.Size = New System.Drawing.Size(70, 22)
        Me.toolStripButton3.Text = "Clear Sheet"
        '
        'toolStripButton4
        '
        Me.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.toolStripButton4.Image = CType(resources.GetObject("toolStripButton4.Image"), System.Drawing.Image)
        Me.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.toolStripButton4.Name = "toolStripButton4"
        Me.toolStripButton4.Size = New System.Drawing.Size(96, 22)
        Me.toolStripButton4.Text = "GD to Clipboard"
        Me.toolStripButton4.ToolTipText = "Copy generated data to clipboard"
        '
        'toolStrip1
        '
        Me.toolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tscbGeneratedTextLanguage, Me.toolStripSeparator1, Me.toolStripButton2, Me.toolStripButton1, Me.toolStripButton3, Me.toolStripButton4, Me.toolStripSeparator3, Me.toolStripButton6, Me.toolStripSeparator4, Me.toolStripButton5})
        Me.toolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.toolStrip1.Name = "toolStrip1"
        Me.toolStrip1.Size = New System.Drawing.Size(798, 25)
        Me.toolStrip1.TabIndex = 11
        '
        'toolStripSeparator3
        '
        Me.toolStripSeparator3.Name = "toolStripSeparator3"
        Me.toolStripSeparator3.Size = New System.Drawing.Size(6, 25)
        '
        'toolStripButton6
        '
        Me.toolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.toolStripButton6.Image = CType(resources.GetObject("toolStripButton6.Image"), System.Drawing.Image)
        Me.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.toolStripButton6.Name = "toolStripButton6"
        Me.toolStripButton6.Size = New System.Drawing.Size(47, 22)
        Me.toolStripButton6.Text = "Config"
        '
        'toolStripSeparator4
        '
        Me.toolStripSeparator4.Name = "toolStripSeparator4"
        Me.toolStripSeparator4.Size = New System.Drawing.Size(6, 25)
        '
        'toolStripButton5
        '
        Me.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.toolStripButton5.Image = CType(resources.GetObject("toolStripButton5.Image"), System.Drawing.Image)
        Me.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.toolStripButton5.Name = "toolStripButton5"
        Me.toolStripButton5.Size = New System.Drawing.Size(44, 22)
        Me.toolStripButton5.Text = "About"
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(798, 514)
        Me.Controls.Add(Me.toolStrip1)
        Me.Controls.Add(Me.tabControl1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(814, 553)
        Me.Name = "MainForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "GenDesc.NET"
        Me.tabPage2.ResumeLayout(False)
        Me.tabPage2.PerformLayout()
        Me.tabPage1.ResumeLayout(False)
        Me.tabControl1.ResumeLayout(False)
        Me.toolStrip1.ResumeLayout(False)
        Me.toolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents toolStripSeparator4 As ToolStripSeparator
    Private WithEvents toolStripButton6 As ToolStripButton
    Private WithEvents toolStripButton5 As ToolStripButton
    Private WithEvents toolStripSeparator3 As ToolStripSeparator

    Private WithEvents tscbGeneratedTextLanguage As ToolStripComboBox
    Private WithEvents toolStripButton4 As ToolStripButton
    Private WithEvents toolStripButton3 As ToolStripButton
    Private WithEvents toolStripButton1 As ToolStripButton
    Private WithEvents toolStripButton2 As ToolStripButton
    Private WithEvents toolStripSeparator2 As ToolStripSeparator
    Private WithEvents toolStripSeparator1 As ToolStripSeparator
    Private WithEvents toolStrip1 As ToolStrip
    Private WithEvents tabPage2 As TabPage
    Private WithEvents tabPage1 As TabPage
    Private WithEvents tabControl1 As TabControl
    Private WithEvents propertyGrid1 As PropertyGrid
    Private WithEvents textBox1 As TextBox


End Class
