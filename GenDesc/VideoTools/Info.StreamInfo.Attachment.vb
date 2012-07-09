Namespace VideoTools.Info.Stream
	
	Public Class Attachment
		Inherits VideoTools.Info.Stream.Base		
		
		Public Sub New(StreamID As Integer, SubStreamID As Integer, Codec As String)
			MyBase.Init("attachment", streamid, SubStreamID, Codec)
		End Sub
	
	End Class

End Namespace
