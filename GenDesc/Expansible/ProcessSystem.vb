Namespace Expansible

	Public Class ProcessSystem
		Inherits System.Collections.Generic.Queue(Of Expansible.Delegates.TodoAction)
		
		Public Event Added(Count As Integer)
		Public Event Removed(Count As Integer)
		Public Event Begin(Count As Integer)
		Public Event Finish(Count As Integer)
		Public Event TaskFinished()		
		
		Private _StatusControl As System.Windows.Forms.Control
		Private _MainForm As MainForm
		Private _Data As New System.Collections.Generic.Dictionary(Of String, Object)
		
		Public Property StatusControl As System.Windows.Forms.Control
			Get				
				Return Me._StatusControl				
			End Get
			Set (value As System.Windows.Forms.Control)				
				Me._StatusControl = value				
			End Set
		End Property
		
		Public Property MainForm As MainForm
			Get				
				Return Me._MainForm
			End Get
			Set (value As MainForm)
				Me._MainForm = value				
			End Set
		End Property

		Private Sub wkTaskFinished()
			RaiseEvent TaskFinished()			
		End Sub		
		
		Public Overloads ReadOnly Property Count As Integer 
			Get
				Return MyBase.Count / 2				
			End Get
		End Property
		
		Public Sub Add(Action As Expansible.Delegates.TodoAction)
			Me.Enqueue(Action)
			Me.Enqueue(New Expansible.Delegates.TodoAction(AddressOf wkTaskFinished))			
		End Sub				
		
		Private Overloads Sub Enqueue(Action As Expansible.Delegates.TodoAction)
			MyBase.Enqueue(Action)			
			RaiseEvent Added(Me.Count)
		End Sub
		
		Private Overloads Function Dequeue() As Expansible.Delegates.TodoAction
			Dim rez As Expansible.Delegates.TodoAction = MyBase.Dequeue()			
			RaiseEvent Removed(Me.Count)
			Return rez
		End Function				
		
		Public WriteOnly Property Text As String
			Set (value As String)
				Me.StatusControl.Text = value				
			End Set
		End Property
		
		Public Sub Start()
			RaiseEvent Begin(Me.Count)			
			Do
				Me.Dequeue().Invoke()
			Loop While MyBase.Count > 0			
			RaiseEvent Finish(Me.Count)
		End Sub
		
		Sub New(ByRef StatusControl As Control, ByRef MainForm As MainForm)						
			Me.StatusControl = StatusControl			
			Me.MainForm = MainForm			
		End Sub
		
		Public Property Data(Name As String) As Object
			Get
				If Me._Data.ContainsKey(Name) Then
					Return Me._Data.Item(Name)
				Else
					Return Nothing
				End If
			End Get
			Set (value As Object)
				If Not Me._Data.ContainsKey(Name) Then
					Me._Data.Add(Name, value)
				Else
					Me._Data.Item(Name) = value					
				End If
			End Set
		End Property
		
		Public Function UnSetData(Name As String) As Boolean
			Return Me._Data.Remove(name)			
		End Function
		
		Public Sub LinkObject(ByRef ObjInst As iImportClass) 			
			With ObjInst
				.Actions = Me
				.MainForm = Me.MainForm				
			End With					
		End Sub				
		
		Public Sub AddAllActions(ByRef ObjInst As iImportClass) 
			Me.LinkObject(ObjInst)
			ObjInst.AddAll()
		End Sub
		
	End Class

End Namespace