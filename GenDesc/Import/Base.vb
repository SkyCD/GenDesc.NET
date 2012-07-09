Namespace Import
	
	Public MustInherit Class Base
		Implements Expansible.iImportClass
		
		Private _MainForm As MainForm
		Private _Actions As Expansible.ProcessSystem			
		
		Public MustOverride Sub AddAll() Implements GenDesc.Expansible.iImportClass.AddAll
		
		Public Property MainForm() As MainForm Implements GenDesc.Expansible.iImportClass.MainForm
			Get
				Return Me._MainForm				
			End Get
			Set (value As MainForm)				
				 Me._MainForm = value				 
			End Set
		End Property
		
		Public Property Actions() As Expansible.ProcessSystem Implements GenDesc.Expansible.iImportClass.Actions		
			Get
				Return Me._Actions			
			End Get
			Set (value As Expansible.ProcessSystem)
				 Me._Actions = value				 
			End Set
		End Property
		
		Protected Friend ReadOnly Property objDataItem As ObjDataItem
			Get
				Return Me.MainForm.objDataItem				
			End Get			
		End Property
	
	End Class

End Namespace