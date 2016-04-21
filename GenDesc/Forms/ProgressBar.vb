'
' Created by SharpDevelop.
' User: Administrator
' Date: 2009.03.19
' Time: 13:34
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Public Partial Class ProgressBar	
	
	Public WithEvents ToDo As Expansible.ProcessSystem
	
	Public Event Finish()	
	
	Public Sub New(ByRef MainForm As MainForm)		
		' The Me.InitializeComponent call is required for Windows Forms designer support.
		Me.InitializeComponent()
		
		'
		' TODO : Add constructor code after InitializeComponents
		'
		Me.ToDo = New Expansible.ProcessSystem(Me, MainForm)		
	End Sub

    Sub ProgressBarLoad(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Public Overrides Property Text As String		
		Get
			On Error Resume Next			
			Return Me.lblDescription.Text
		End Get
		Set (value as String)
			Me.lblDescription.Text = value
			Me.Refresh()			
			Me.Update()
			Application.DoEvents()
		End Set
	End Property
	
	Public Property Minimum As Integer
		Get
			Return Me.pbBar.Minimum
		End Get
		Set (val As Integer)
			Me.pbBar.Minimum = val			
		End Set
	End Property
	
	Public Property Maximum As Integer
		Get
			Return Me.pbBar.Maximum
		End Get
		Set (val As Integer)
			Me.pbBar.Maximum = val			
		End Set
	End Property
	
	Public Property Value As Integer
		Get
			Return Me.pbBar.Value
		End Get
		Set (val As Integer)
			Me.pbBar.Value = val
			Me.label1.Text = Math.Round(100 / (Me.Maximum - Me.Minimum) * Me.Value, 2).ToString("##0.00") + " %"
			Me.label1.BackColor = System.Drawing.Color.Transparent
			Application.DoEvents()
		End Set
	End Property
	
	Private Sub ToDo_Begin(Count As Integer) Handles ToDo.Begin
		Me.Minimum = 0
		Me.Maximum = Count
		Me.Value = 0
		Me.label1.Text = (0).ToString("##0.00") + " %"
		Me.Show()
		Me.Refresh()			
		Me.Update()
		Application.DoEvents()
	End Sub	
	
	Private Sub ToDo_Finish(Count As Integer) Handles ToDo.Finish
		RaiseEvent Finish()
		Me.Hide()		
		'Me.tabControl1.Enabled = True
		'Me.toolStrip1.Enabled = True
		'Me.toolStripProgressBar1.Visible = False
	End Sub
	
	Private Sub ToDo_FinishedCurrentStep() Handles ToDo.TaskFinished
		Me.Value += 1
		'Me.toolStripProgressBar1.Value += 1
	End Sub
	
End Class
