Namespace Library
	
	Public Class Console

		' source: http://www.dreamincode.net/forums/showtopic63047.htm
		Public Shared Function RunApplication(ByVal _Path As String, Optional ByVal _Args As String = "") As String			
			Dim Run_Console As String = ""			
        	Dim _Prog As New Process
        	Try
            	With _Prog.StartInfo
                	.WorkingDirectory = _Path.Substring(0, _Path.LastIndexOf("\") + 1)
                	.FileName = .WorkingDirectory & _Path.Substring(_Path.LastIndexOf("\") + 1)
                	.CreateNoWindow = True
                	.UseShellExecute = False
                	.RedirectStandardOutput = True
                	.RedirectStandardError = True
                	.RedirectStandardInput = True
                	.Arguments = _Args
            	End With
            	_Prog.Start()
            	Do
					Dim content As String = _Prog.StandardOutput.ReadLine()
					If content.Trim().Length > 0 Then
						Run_Console += content + vbcrlf						
					End If
					Application.DoEvents()
            	Loop While _Prog.Responding And Not _Prog.HasExited            	
            	_Prog.WaitForExit()
        	Catch ex As Exception
            	Throw ex
        	End Try
        	Return Run_Console        	
    	End Function
    	
    	Public Shared Sub RunApplicationNoOutput(ByVal _Path As String, Optional ByVal _Args As String = "")
        	Dim _Prog As New Process
        	Try
            	With _Prog.StartInfo
                	.WorkingDirectory = _Path.Substring(0, _Path.LastIndexOf("\") + 1)
                	.FileName = .WorkingDirectory & _Path.Substring(_Path.LastIndexOf("\") + 1)
                	.CreateNoWindow = True
                	.UseShellExecute = False
                	.RedirectStandardOutput = True
                	.RedirectStandardError = True
                	.RedirectStandardInput = True
                	.Arguments = _Args
            	End With
            	_Prog.Start()
            	_Prog.WaitForExit()
        	Catch ex As Exception
            	Throw ex
        	End Try
    	End Sub
    	
    	Public Shared Function RunApplicationSE(ByVal _Path As String, Optional ByVal _Args As String = "") As String
    		Dim Rez As New IO.StringWriter()
            Dim nprocess As Process =  New Process()             
            nprocess.StartInfo.FileName = _Path
            nprocess.StartInfo.Arguments = _Args
            nprocess.EnableRaisingEvents = False
            nprocess.StartInfo.UseShellExecute = False
            nprocess.StartInfo.CreateNoWindow = True
            nprocess.StartInfo.RedirectStandardOutput = True
            nprocess.StartInfo.RedirectStandardError = True
            nprocess.Start()
            Dim d As IO.StreamReader = nprocess.StandardError            
            Do 
            	Rez.WriteLine(d.ReadLine())
            	If System.DateTime.Now.Ticks Mod 2 = 0 Then
					Application.DoEvents()					
            	End If            	
            Loop While Not d.EndOfStream
            nprocess.WaitForExit()
            nprocess.Close()
            Return rez.ToString()            
		End Function
	
	End Class

End Namespace
