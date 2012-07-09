Imports System.Text.RegularExpressions


Namespace Library.Text

	Public Class BBCode
		
		Public Enum TrimBBCodeMethod As SByte
			NoTrim = 0
			TrimV1 = 1
			TrimV2 = 2
		End Enum
		
		Public Shared Function Trim(strTextToReplace As String, Optional TrimBBCodeMethod As TrimBBCodeMethod = TrimBBCodeMethod.TrimV1) As String
			If TrimBBCodeMethod = TrimBBCodeMethod.NoTrim Then Return strTextToReplace		
			'//Define regex
        	Dim regExp As Regex
 
        	'//Regex for URL tag without anchor
        	regExp = New Regex("\[url\]([^\]]+)\[\/url\]")
        	strTextToReplace = regExp.Replace(strTextToReplace, "$1")
 
        	'//Regex for URL with anchor
        	If TrimBBCodeMethod = TrimBBCodeMethod.TrimV1 Then
	        	regExp = New Regex("\[url=([^\]]+)\]([^\]]+)\[\/url\]")
    	    	strTextToReplace = regExp.Replace(strTextToReplace, "$1")
			ElseIf TrimBBCodeMethod = TrimBBCodeMethod.TrimV2 Then
	    		regExp = New Regex("\[url=([^\]]+)\]([^\]]+)\[\/url\]")
    	    	strTextToReplace = regExp.Replace(strTextToReplace, "$2")
    		End If    	
 
         	'//Image regex
        	regExp = New Regex("\[img\=([^\]]+)\]")
        	strTextToReplace = regExp.Replace(strTextToReplace, "$1")
 
        	'//Image regex
        	regExp = New Regex("\[img\]([^\]]+)\[\/img\]")
        	strTextToReplace = regExp.Replace(strTextToReplace, "$1")
        
        	'//Bold text
        	regExp = New Regex("\[b\](.+?)\[\/b\]")
        	strTextToReplace = regExp.Replace(strTextToReplace, "$1")
 
        	'//Italic text
        	regExp = New Regex("\[i\](.+?)\[\/i\]")
        	strTextToReplace = regExp.Replace(strTextToReplace, "$1")
 
        	'//Underline text
        	regExp = New Regex("\[u\](.+?)\[\/u\]")
        	strTextToReplace = regExp.Replace(strTextToReplace, "$1")
        
        	'//Font size
        	regExp = New Regex("\[size=([^\]]+)\]([^\]]+)\[\/size\]")
        	strTextToReplace = regExp.Replace(strTextToReplace, "$2")
        
        	'//Font color
        	regExp = New Regex("\[color=([^\]]+)\]([^\]]+)\[\/color\]")
        	strTextToReplace = regExp.Replace(strTextToReplace, "$2")
        
        	Return strTextToReplace        
		End Function	
		
		Public Shared Function MakeStringsList(Content As String, Optional TrimBBCode as TrimBBCodeMethod = TrimBBCodeMethod.NoTrim) As String()			
			content = Trim(WebBrowserContentDonwloader.StripHTMLComments(content), TrimBBCode)
			Dim rez() As String = Content.Replace(vbcrlf, vbcr).Replace(vblf, vbcr).Replace(vbcr,"<br>").Replace("<br>","<br />").Replace("<br>","<br />").Replace("<br />",",").Replace("<br/>",",").Replace("/",",").Split(",")
			Dim r2 As New ArrayList()
			For Each r3 As String In rez
				If R3.Trim().Length < 1 Then Continue For		
				'r3 = Me.TrimBBCode(r3, TrimBBCode)			
				r2.Add(R3.Trim())
			Next
			ReDim rez(r2.Count-1) 'As String		
			For I As Integer = 0 To r2.Count -1
				rez(i) = r2.Item(i).ToString()
			Next
			Return rez
		End Function
		
	End Class

End Namespace