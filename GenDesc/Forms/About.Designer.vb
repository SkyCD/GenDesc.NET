'
' Created by SharpDevelop.
' User: Administrator
' Date: 2009.03.18
' Time: 21:17
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Partial Class About
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(About))
        Me.webBrowser1 = New System.Windows.Forms.WebBrowser()
        Me.SuspendLayout()
        '
        'webBrowser1
        '
        Me.webBrowser1.AllowNavigation = False
        Me.webBrowser1.AllowWebBrowserDrop = False
        Me.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.webBrowser1.IsWebBrowserContextMenuEnabled = False
        Me.webBrowser1.Location = New System.Drawing.Point(0, 0)
        Me.webBrowser1.MinimumSize = New System.Drawing.Size(20, 20)
        Me.webBrowser1.Name = "webBrowser1"
        Me.webBrowser1.ScriptErrorsSuppressed = True
        Me.webBrowser1.Size = New System.Drawing.Size(399, 259)
        Me.webBrowser1.TabIndex = 0
        Me.webBrowser1.WebBrowserShortcutsEnabled = False
        '
        'About
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(399, 259)
        Me.Controls.Add(Me.webBrowser1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "About"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "About"
        Me.ResumeLayout(False)

    End Sub
    Private webBrowser1 As System.Windows.Forms.WebBrowser
End Class
