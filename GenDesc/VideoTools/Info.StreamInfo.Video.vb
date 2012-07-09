Namespace VideoTools.Info.Stream
	
	Public Class Video
		Inherits VideoTools.Info.Stream.Base		
		
		Private _Size As System.Drawing.Size
		Private _Fps As Double 
		
		Public ReadOnly Property Size As System.Drawing.Size
			Get
				Return Me._Size				
			End Get
		End Property
		
		Public ReadOnly Property Width As Integer
			Get
				Return Me._Size.Width
			End Get
		End Property
		
		Public ReadOnly Property Height As Integer
			Get
				Return Me._Size.Height
			End Get
		End Property
		
		Public ReadOnly Property FPS As Integer
			Get
				Return Me._Fps	
			End Get
		End Property
		
		Sub New(StreamID As Integer, SubStreamID As Integer, Codec As String, Size As System.Drawing.Size, FPS As Integer)
			MyBase.Init("video", streamid, SubStreamID, Codec)
			Me._Fps = fps
			me._Size = Size
		End Sub
		
		Sub New(StreamID As Integer, SubStreamID As Integer, Codec As String, Width As Integer, Height As Integer, FPS As Integer)
			Me.New(StreamID, SubStreamID, Codec, New System.Drawing.Size(width, height), FPS)			
		End Sub
	
	End Class

End Namespace
