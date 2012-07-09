Namespace VideoTools.Info
	
	Public Class Media
		
		Private _FileName As String 
		Private _Type As String
		Private _Duration As TimeSpan
		Private _BitrateKbs As Integer
		Private _Streams As StreamsCollection		
		
		Public ReadOnly Property FileName As String
			Get
				Return Me._FileName				
			End Get
		End Property
		
		Public ReadOnly Property Type As String
			Get
				Return Me._Type		
			End Get
		End Property
		
		Public ReadOnly Property Duration As TimeSpan
			Get
				Return Me._Duration	
			End Get
		End Property
		
		Public ReadOnly Property BitrateKbs As Integer
			Get
				Return Me._BitrateKbs
			End Get
		End Property
		
		Public ReadOnly Property Streams As StreamsCollection
			Get
				Return Me._Streams
			End Get
		End Property
		
		Public Sub New(FileName As String, Type As String, Duration As TimeSpan, BitrateKbs As Integer, Streams As StreamsCollection)
			Me._FileName = filename
			Me._Type = type
			Me._Duration = duration
			Me._BitrateKbs = BitrateKbs
			Me._Streams = Streams			
		End Sub
	
	End Class

End Namespace
