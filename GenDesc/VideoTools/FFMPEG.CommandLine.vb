Namespace VideoTools.ffmpeg
	
	Public Class CommandLineArguments
			
		Private Arguments As IO.StringWriter = New IO.StringWriter()
		
		Public Sub InputFile(FileName As String)
			Me.Arguments.Write("-i """ + FileName + """ ")			
		End Sub
		
		Public Sub Version()
			Me.Arguments.Write("-version ")
		End Sub
		
		Public Sub License()
			Me.Arguments.Write("-L ")
		End Sub
		
		Public Sub Help()
			Me.Arguments.Write("-H ")
		End Sub
		
		Public Sub AvaibleFormats()
			Me.Arguments.Write("-formats ")
		End Sub
		
		Public Sub DisableAudio()
			Me.Arguments.Write("-an ")			
		End Sub
		
		Public Sub SeekToPosition(Seconds As Integer)
			Me.Arguments.Write("-ss ")
			Me.Arguments.Write(Seconds)
			Me.Arguments.Write(" ")
		End Sub		
		
		Public Sub SeekToPosition(Hours As Integer, Minutes As Integer, Seconds As Integer)			
			Me.Arguments.Write("-ss ")
			Me.Arguments.Write(Hours)
			Me.Arguments.Write(":")
			Me.Arguments.Write(Minutes)
			Me.Arguments.Write(":")
			Me.Arguments.Write(Seconds)
			Me.Arguments.Write(" ")
		End Sub
		
		Public Sub SeekToPosition(Hours As Integer, Minutes As Integer, Seconds As Integer, Miliseconds as Integer)
			Me.Arguments.Write("-ss ")
			Me.Arguments.Write(Hours)
			Me.Arguments.Write(":")
			Me.Arguments.Write(Minutes)
			Me.Arguments.Write(":")
			Me.Arguments.Write(Seconds)
			Me.Arguments.Write(".")
			Me.Arguments.Write(Miliseconds)
			Me.Arguments.Write(" ")
		End Sub
		
		Public Sub SeekToPosition(Position As TimeSpan)
			Me.SeekToPosition(Position.Hours, Position.Minutes, Position.Seconds, Position.Milliseconds)			
		End Sub
		
		Public Sub Overwrite()
			Me.Arguments.Write("-y ")
		End Sub
		
		Public Sub SetTheNumberOfVideoFramesToRecord(Count As Long)
			Me.Arguments.Write("-vframes ")
			Me.Arguments.Write(Count)
			Me.Arguments.Write(" ")
		End Sub		
		
		Public Sub SpecifyFormat(Format As String)			
			Me.Arguments.Write("-f ")
			Me.Arguments.Write(Format)
			Me.Arguments.Write(" ")
		End Sub
		
		Public Sub SetFrameRate(Count As Double)			
			Me.Arguments.Write("-r ")
			'Dim CC As System.Globalization.CultureInfo = System.Globalization.CultureInfo.CurrentCulture.Clone()			
			'System.Threading.Thread.CurrentThread.CurrentCulture = 
			Dim culc As New System.Globalization.CultureInfo("en-us", False)			
			Me.Arguments.Write( Count.ToString(culc) )
			'System.Threading.Thread.CurrentThread.CurrentCulture = CC.Clone()						
			Me.Arguments.Write(" ")
		End Sub
		
		Public Sub OutFileName(Filename As String)
			Me.Arguments.Write("""")
			Me.Arguments.Write(Filename)
			Me.Arguments.Write(""" ")
		End Sub
		
		Public Overrides Function ToString() As String 			
			Return Me.Arguments.ToString()			
		End Function	
			
	End Class
	
End Namespace
