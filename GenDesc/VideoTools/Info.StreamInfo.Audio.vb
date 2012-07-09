Namespace VideoTools.Info.Stream
	
	Public Class Audio
		Inherits VideoTools.Info.Stream.Base
		
		Public Enum ChannelsMode As Integer
			unknown = 0		
			mono = 1
			stereo = 2
			five_with_one = 3
			joint_stereo = 4
		End Enum
		
		Private _Hz As Double
		Private _Channels As GenDesc.VideoTools.Info.Stream.Audio.ChannelsMode = GenDesc.VideoTools.Info.Stream.Audio.ChannelsMode.unknown		
		Private _BitRateKbs As Integer 
		
		Public ReadOnly Property Hz As Double
			Get
				Return Me._Hz				
			End Get
		End Property
		
		Public ReadOnly Property Channels As GenDesc.VideoTools.Info.Stream.Audio.ChannelsMode
			Get
				Return Me._Channels
			End Get
		End Property
		
		Public ReadOnly Property BitRateKbs As Integer
			Get
				Return Me._BitRateKbs
			End Get
		End Property
		
		Public Sub New(StreamID As Integer, SubStreamID As Integer, Codec As String, Hz As Double, Channels As GenDesc.VideoTools.Info.Stream.Audio.ChannelsMode, BitRateKbs As Integer)
			MyBase.Init("audio", streamid, SubStreamID, Codec)
			Me._Channels = Channels
			Me._Hz = Hz
			Me._BitRateKbs = BitRateKbs
		End Sub
	
	End Class

End Namespace
