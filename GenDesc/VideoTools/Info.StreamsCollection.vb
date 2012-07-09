Namespace VideoTools.Info
	
	Public Class StreamsCollection
		Inherits Collections.Generic.Dictionary(Of Integer, Collections.Generic.Dictionary(Of Integer, VideoTools.Info.Stream.Base) )
		
		Public Overloads Sub Add(Stream As VideoTools.Info.Stream.Base)
			If Not Me.ContainsKey(Stream.StreamID) Then
				MyBase.Add(Stream.StreamID, New Collections.Generic.Dictionary(Of Integer, VideoTools.Info.Stream.Base))				
			End If
			MyBase.Item(Stream.StreamID).Add(Stream.SubStreamID, Stream)						
		End Sub
		
		Public Sub AddVideoStream(StreamID As Integer, SubStreamID As Integer, Codec As String, Size As System.Drawing.Size, FPS As Integer)
			Me.Add(New Stream.Video(StreamID, SubStreamID, Codec, Size, FPS))			
		End Sub
		
		Public Sub AddVideoStream(StreamID As Integer, SubStreamID As Integer, Codec As String, Width as Integer, Height as Integer, FPS As Integer)
			Me.Add(New Stream.Video(StreamID, SubStreamID, Codec, Width, Height, FPS))			
		End Sub
		
		Public Sub AddAudioStream(StreamID As Integer, SubStreamID As Integer, Codec As String, Hz As Double, Channels As GenDesc.VideoTools.Info.Stream.Audio.ChannelsMode, BitRateKbs As Integer)
			Me.Add(New Stream.Audio(StreamID, SubStreamID, Codec, Hz, Channels, BitRateKbs ))
		End Sub
		
		Public Sub AddSubtitleStream(StreamID As Integer, SubStreamID As Integer, Codec As String)
			Me.Add(New Stream.Subtitle(StreamID, SubStreamID, Codec))			
		End Sub
		
		Public Sub AddAttachmentStream(StreamID As Integer, SubStreamID As Integer, Codec As String)
			Me.Add(New Stream.Attachment(StreamID, SubStreamID, Codec))			
		End Sub
		
		Protected Function GetSpecifiedTypeStreams(Of Type)(TypeName As String) As Generic.List(Of Type)			
			Dim rs As New Generic.List(Of Type)
			For Each I As Integer In Me.Keys				
				For Each O As Integer In Me.Item(I).Keys
					If Me.Item(I).Item(o).Type = TypeName Then
						Dim obj as Object = Me.Item(I).Item(o)
						rs.Add(obj)
					End If
				Next
			Next
			Return rs
		End Function
		
		Public ReadOnly Property AllAudio As Generic.List(Of Stream.Audio)
			Get
				Return Me.GetSpecifiedTypeStreams(Of Stream.Audio)("audio")				
			End Get
		End Property
		
		Public ReadOnly Property AllVideo As Generic.List(Of Stream.Video)
			Get
				Return Me.GetSpecifiedTypeStreams(Of Stream.Video)("video")
			End Get
		End Property
		
		Public ReadOnly Property AllAttachments As Generic.List(Of Stream.Attachment)
			Get
				Return Me.GetSpecifiedTypeStreams(Of Stream.Attachment)("attachment")
			End Get
		End Property
		
		Public ReadOnly Property AllSubtitles As Generic.List(Of Stream.Subtitle)
			Get
				Return Me.GetSpecifiedTypeStreams(Of Stream.Subtitle)("subtitle")
			End Get
		End Property
		
	End Class
	
End Namespace