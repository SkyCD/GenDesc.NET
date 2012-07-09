Namespace VideoTools.ffmpeg

Public Class Tools
	
	Private CommandLine As String = my.Settings.FFMPEG
	
	Private Sub Execute(ByRef CommandLineArgs As ffmpeg.CommandLineArguments)		
		Dim rez As String = Library.Console.RunApplicationSE(Me.CommandLine, CommandLineArgs.ToString())
		'Messagebox.Show(rez)
	End Sub	
	
	Public Sub ExtractFrame(InFileName As String, OutFileName As String, Optional Seconds As Integer = 0)
		Me.ExtractFrame(InFileName, OutFileName, New TimeSpan(0, 0, Seconds))
	End Sub	
	
	Public Sub ExtractFrame(InFileName As String, OutFileName As String, Position As TimeSpan)
		Dim cml As New ffmpeg.CommandLineArguments()
		cml.Overwrite()
		cml.InputFile(infilename)
		cml.DisableAudio()
		'cml.SetFrameRate(Position.TotalSeconds)		
		cml.SeekToPosition(position)
		cml.SetTheNumberOfVideoFramesToRecord(1)
		cml.SpecifyFormat("image2")
		cml.OutFileName(OutFileName)
		Me.Execute(cml)		
	End Sub
	
	Public Function ExtractFrame(InFileName As String, Position As TimeSpan) As System.Drawing.Image
		Dim tFilename As String = IO.Path.GetTempFileName() + ".png"
		Me.ExtractFrame(InFileName, tFilename, Position)		
		Dim sr As IO.FileStream
		Do
			Try
				sr = IO.File.OpenRead(tFilename)
				Exit Do				
			Catch ex As Exception
				'doing nothing
			End Try
		Loop 
		Dim img As System.Drawing.Image = System.Drawing.Image.FromStream(sr)
		sr.Close()
		IO.File.Delete(tFilename)
		Return img		
	End Function
	
	Public Function ExtractFrame(InFileName As String, Optional Seconds As Integer = 0) As System.Drawing.Image
		Return Me.ExtractFrame(InFileName, New TimeSpan(0, 0, Seconds))		
	End Function
	
	Public Function GetMediaInfo(FileName As String) As GenDesc.VideoTools.Info.Media				
		Dim DurationHours As Integer, DurationMinutes As Integer, DurationSeconds As Integer, DurationMiliseconds As Integer
		Dim StartingPosition As Double, Bitrate As Integer, BitrateMode As String
		Dim cml As New ffmpeg.CommandLineArguments()
		cml.InputFile(FileName)		
		cml.Version()		
		Dim out As String = Library.Console.RunApplicationSE(Me.CommandLine, cml.ToString())
		Dim m As System.Text.RegularExpressions.Match = System.Text.RegularExpressions.Regex.Match(out, "Duration:\s+(\d+):(\d+):(\d+).(\d*),\s+start:\s+([^\s,]+),\s+bitrate:\s+(\d+)\s+([^/]+)")
		If m.Success Then
			 DurationHours = val(m.Groups.Item(1).Value)
			 DurationMinutes = val(m.Groups.Item(2).Value)
			 DurationSeconds = val(m.Groups.Item(3).Value)
			 DurationMiliseconds = val(m.Groups.Item(4).Value)
			 StartingPosition = val(m.Groups.Item(5).Value)
			 Bitrate = val(m.Groups.Item(6).Value)
			 BitrateMode = m.Groups.Item(7).Value.Trim().ToLower()
			 If BitrateMode = "mb" Then
			 	Bitrate = Bitrate * 1024
			 ElseIf BitrateMode = "gb" Then
			 	Bitrate = Bitrate * 1024 * 1024
			 ElseIf BitrateMode = "b" Then
			 	Bitrate = Bitrate / 1024
			 End If
		End If
		Dim m2 As System.Text.RegularExpressions.MatchCollection = System.Text.RegularExpressions.Regex.Matches(out, "Stream\s+#(\d+).(\d+|\d+[^:]+):\s+([^:]+):\s+([^\n]+)")
		Dim Streams As New VideoTools.Info.StreamsCollection()
		For Each m3 As System.Text.RegularExpressions.Match In m2			
			If m3.Success Then
				Dim StreamID As Integer = val(m3.Groups.Item(1).Value)
				Dim SubStreamID As Integer = val(m3.Groups.Item(2).Value)
				Dim Type As String = m3.Groups.Item(3).Value.ToLower().Trim()
				Dim AllOtherInfo() As String = m3.Groups.Item(4).Value.ToLower().Trim().Split(",")
				Dim Codec As String = ""
				Dim Hz As Integer
				Dim Fps As Double
				Dim Rate As Double
				Dim ac As VideoTools.Info.Stream.Audio.ChannelsMode = VideoTools.Info.Stream.Audio.ChannelsMode.unknown
				Dim Size As New System.Drawing.Size()				
				For I As Integer = 0 To AllOtherInfo.Length - 1					
					AllOtherInfo(i) = AllOtherInfo(i).Trim()
					If i = 0 Then
						Codec = AllOtherInfo(i)
						Continue For
					End If
					If AllOtherInfo(i).EndsWith("fps") Then
						Fps = val(AllOtherInfo(i))						
						Continue For
					End If
					If AllOtherInfo(i).EndsWith("hz") Then
						Hz = val(AllOtherInfo(i))
						Continue For
					End If
					If AllOtherInfo(i).EndsWith("kb/s") Then						
						Rate = val(AllOtherInfo(i))
						Continue For
					End If
					If  AllOtherInfo(i).EndsWith("tb(r)") Then
						Fps = val(AllOtherInfo(i))
						Rate = val(AllOtherInfo(i))
						Continue For
					End If
					If AllOtherInfo(i) = "stereo" Then
						ac = VideoTools.Info.Stream.Audio.ChannelsMode.stereo
						Continue For						
					End If
					If AllOtherInfo(i) = "mono" Then
						ac = VideoTools.Info.Stream.Audio.ChannelsMode.mono
						Continue For						
					End If
					If AllOtherInfo(i) = "5:1" Then
						ac = VideoTools.Info.Stream.Audio.ChannelsMode.five_with_one
						Continue For						
					End If
					If AllOtherInfo(i) = "joint stereo" Then
						ac = VideoTools.Info.Stream.Audio.ChannelsMode.joint_stereo
						Continue For						
					End If
					m = System.Text.RegularExpressions.Regex.Match(AllOtherInfo(i), "(\d+)x(\d+)")
					If m.Success Then
						size.Width = val(m.Groups.Item(1).Value)
						size.Height = val(m.Groups.Item(2).Value)
						Continue For						
					End If
				Next
				Select Case Type
					Case "video"
						Streams.AddVideoStream(StreamID, SubStreamID, Codec, Size, fps)						
					Case "audio"
						Streams.AddAudioStream(StreamID, SubStreamID, Codec, Hz, ac, Rate)
					Case "subtitle"
						Streams.AddSubtitleStream(StreamID, SubStreamID, Codec)
					Case "attachment"
						Streams.AddAttachmentStream(StreamID, SubStreamID, Codec)
				End Select
			End If
		Next
		Return New VideoTools.Info.Media(FileName, (New IO.FileInfo(FileName)).Extension.Substring(1) , New TimeSpan(DurationHours, DurationMinutes, DurationSeconds, DurationMiliseconds), Bitrate, Streams)		
	End Function
	
	Public Function ExtractSomeFrames(FileName As String, HowMany As Integer) As System.Drawing.Image()
		Dim rez(HowMany - 1) As System.Drawing.Image
		Dim mInfo As Info.Media = Me.GetMediaInfo(FileName)	
		If mInfo.Duration.TotalSeconds = 0 Then
			Return Nothing			
		End if
		'Messagebox.Show(mInfo.Streams.AllVideo.Count)		
		Dim mp As Double = (mInfo.Streams.AllVideo.Item(0).FPS / ( mInfo.Duration.TotalSeconds / HowMany)) * Math.Sqrt(mInfo.Streams.AllVideo.Item(0).FPS)
		If mp < 0.001 Then
			Do 
				mp = mp * 2
			Loop While mp < 0.001			
		End If				
		Dim tmpPath As String = IO.Path.GetTempFileName()
		If IO.Directory.Exists(tmpPath) Then
			IO.Directory.Delete(tmpPath, True)
		ElseIf IO.File.Exists(tmpPath) Then
			IO.File.Delete(tmpPath)
		End If
		Dim tFileName as String = tmpPath + "\%d.png"
		Dim cla As New ffmpeg.CommandLineArguments()
		cla.Overwrite()
		cla.InputFile(FileName)
		cla.SeekToPosition(1)
		cla.SetFrameRate(mp)
		cla.SetTheNumberOfVideoFramesToRecord(HowMany)
		cla.SpecifyFormat("image2")
		cla.OutFileName(tFileName)
		IO.Directory.CreateDirectory(tmpPath)
		Dim out As String = Library.Console.RunApplicationSE(Me.CommandLine, cla.ToString())
		For I As Integer = 0 To HowMany - 1
			Dim fname as String = tmpPath + "\" + (I + 1).ToString() + ".png"
			Me.WaitUntilFileExist(fname)
			rez(i) = System.Drawing.Image.FromFile(fname)
			Me.WaitUntilFileExist(fname)
			Try
				IO.File.Delete(fname)
			Catch ex As Exception
				'Skip deletion
			End Try			
		Next
		Try
			IO.Directory.Delete(tmpPath, True)
		Catch ex As Exception
			
		End Try			
		Return rez
	End Function
	
	Private Sub WaitUntilFileExist(FileName As String)
		Dim sr As IO.Stream		
		Do
			Try
				sr = IO.File.OpenRead(FileName)
				Exit Do				
			Catch ex As Exception
				'doing nothing
				Application.DoEvents()
			End Try
		Loop 
		sr.Close()		
	End Sub
	
'	Public Function ExtractManyFramesAtOnce(InFileName As String, Interval As Integer, HowManyFrames As Long, Optional Seconds As Integer = 0) As System.Drawing.Image()
'		Dim r As New Collections.Generic.List(Of System.Drawing.Image)
'		Dim pFileName as String = IO.Path.GetTempFileName()
'		Dim tFilename As String = pFileName + "%d.png"
'		Dim cml As New ffmpeg.CommandLineArguments()
'		cml.Overwrite()
'		cml.InputFile(infilename)
'		cml.SetFrameRate(Interval)
'		cml.SeekToPosition(Seconds)
'		cml.SetTheNumberOfVideoFramesToRecord(HowManyFrames)
'		cml.SpecifyFormat("image2")
'		cml.OutFileName(OutFileName)		
'	End Function
				
End Class

End Namespace