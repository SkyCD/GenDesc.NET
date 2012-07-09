Namespace VideoTools.Info.Stream
	
	Public MustInherit Class Base	
		
		Private _StreamID As Integer
		Private _SubStreamID As Integer
		Private _Codec As String
		Private _Type As String		
		
		Public ReadOnly Property StreamID As Integer
			Get
				Return Me._StreamID				
			End Get
		End Property		
		
		Public ReadOnly Property SubStreamID As Integer
			Get
				Return Me._SubStreamID		
			End Get
		End Property
		
		Public ReadOnly Property Codec As String
			Get
				Return Me._Codec
			End Get
		End Property
		
		Public ReadOnly Property Type As String			
			Get
				Return Me._Type
			End Get
		End Property
		
		Protected Friend Sub Init(Type As String, StreamID As Integer, SubStreamID As Integer, Codec As String)			
			Me._StreamID = StreamID
			Me._SubStreamID = SubStreamID
			Me._Codec = Codec
			Me._Type = Type.ToLower().Trim()			
		End Sub
	
	End Class

End Namespace