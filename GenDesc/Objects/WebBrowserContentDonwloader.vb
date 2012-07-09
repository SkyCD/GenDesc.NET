Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Xml
Imports System.Xml.Schema
Imports System.Xml.XPath
Imports System.Text.RegularExpressions

Public Class WebBrowserContentDonwloader
			
	Private Downloading As Boolean = False
	
	Private WithEvents wb As New WebBrowser
	
	Public Structure AniDBRating
		Dim Permanent As Double
		Dim Temporary As Double 
		Dim Reviews As Double 		
	End Structure
	
	Public Structure AnimeNewsNetworkRating
		Dim ArithmeticMean As Double
		Dim WeightedMean As Double
		Dim BayesianEstimate As Double 
		Dim MedianMean As Double 
	End Structure
	
	Public Structure IMDBRating
		Dim WeightedAverage As Double
		Dim ArithmeticMean As Double
		Dim Median As Double 		
	End Structure
	
	Public Structure TVcomRating
		Dim Rating as Double
	End Structure
	
	Public Enum AnimeNewsNetworkItemType as SByte
		anime = 0
		manga = 1
	End Enum		
	
	Public Shared Function ExtractIDFromTVcomLink(Uri As Uri) As Integer
		Dim m As Match = regex.Match(Uri.ToString(), "show/(\d+)/")
		If m.Success Then
			Return val(m.Groups.Item(1).Value)
		End If
		Return -1			
	End Function
	
	Public Shared Function ExtractIDFromIMDBLink(Uri As Uri) As Integer
		Dim m As Match = regex.Match(Uri.ToString(), "/tt(\d+)/")		
		If m.Success Then
			Return val(m.Groups.Item(1).Value)
		End If
		Return -1			
	End Function
		
	Public Shared Function StripHTML(htmlString As String ) As String 		
		Dim pattern As String = "<(.|\n)*?>"
		Return Regex.Replace(htmlString, pattern, String.Empty)	
	End Function
	
	Public Function GetTVcomRating(ID As Integer) As TVcomRating		
		Dim ratings_content As String = Me.DownloadWebDocument( GetTVcomShowSumaryUrl(id) )					
		Dim rez as New TVcomRating()
		If ratings_content IsNot Nothing Then
			Dim m As RegularExpressions.Match = regex.Match(ratings_content,"<div class=""COLUMN_A"">[^<]+<div id=""page_head"">Show Summary</div>[^<]+<div class=""COLUMN_A1"">[^<]+<div class=""MODULE first"" id=""show_rating"">[^<]+<h2>Show Score</h2>[^<]+<div class=""global_rating score90"">[^<]+<div class=""[^""]+"">[^<]+</div>[^<]+<div class=""global_score"">[^<]+<span class=""number"">([^<]+)")
		 	If (m.Success) Then		 	
				rez.Rating = val(m.Groups.Item(1).Value)
			Else
				ratings_content = StripHTML(ratings_content)		
				clipboard.SetText(ratings_content)
				m = regex.Match(ratings_content,"Show Score\s+([^\s]+)")
				If (m.Success) Then		 	
					rez.Rating = val(m.Groups.Item(1).Value)
		 		End If		 		
		 	End If			
		End If
		Return rez		
	End Function	
	
	Public Function GetIMDBRating(ID As Integer) As IMDBRating
		Dim ratings_content As String = DownloadWebDocument( GetIMDBPageUrl(id, "title", "ratings") )			
		Dim rez As New IMDBRating()		
		If ratings_content IsNot Nothing Then
			ratings_content = StripHTML(ratings_content)
			Dim m As RegularExpressions.Match = regex.Match(ratings_content,"IMDb users have given a weighted average vote of\s+([^/]+)\s+/")
		 	If (m.Success) Then		 	
				rez.WeightedAverage = val(m.Groups.Item(1).Value)
		 	End If	
		 	m = regex.Match(ratings_content,"Arithmetic mean = ([^\s]+)")
		 	If (m.Success) Then
				rez.ArithmeticMean = val(m.Groups.Item(1).Value)
		 	End If	
		 	m = regex.Match(ratings_content,"Median = ([^\s]+)")
		 	If (m.Success) Then
				rez.Median = val(m.Groups.Item(1).Value)
		 	End If	
		End If
		Return rez		
	End Function	
	
	Public Shared Function GetAnimeNewsNetworkPageUrl(ID As Integer, Optional Type As AnimeNewsNetworkItemType = AnimeNewsNetworkItemType.anime ) As Uri
		return new Uri(	"http://www.animenewsnetwork.com/encyclopedia/" + Type.ToString() + ".php?id=" + Id.ToString())
	End Function
	
	Public Function GetAnimeNewsNetworkRating(AnimeId As Integer, Optional Type as AnimeNewsNetworkItemType = AnimeNewsNetworkItemType.anime ) As AnimeNewsNetworkRating
		 Dim ratings_content As String = Me.DownloadWebDocument( GetAnimeNewsNetworkPageUrl(AnimeId, type) )	
		 Dim rez As New AnimeNewsNetworkRating()		 
		 Dim m As RegularExpressions.Match = regex.Match(ratings_content,"<B>Median rating:</B> ([^<]+)")		  
		 If (m.Success) Then		 	
			rez.MedianMean = math.Round(val(m.Groups.Item(1).Value)	, 2)
		 End If	
		 m = regex.Match(ratings_content,"<B>Arithmetic mean:</B>\s+([^ ]+)")		 
		 If (m.Success) Then
			rez.ArithmeticMean = math.Round(val(m.Groups.Item(1).Value)	, 2)
		 End If	
		 m = regex.Match(ratings_content,"<B>Weighted mean:</B>\s+([^ ]+)")		 
		 If (m.Success) Then
			rez.WeightedMean = math.Round(val(m.Groups.Item(1).Value), 2)
		 End If	
		 m = regex.Match(ratings_content,"<B>Bayesian estimate:</B>\s+([^ ]+)")
		 If (m.Success) Then
			rez.BayesianEstimate = math.Round(val(m.Groups.Item(1).Value), 2)
		 End If	
		 Return rez		 
	End Function
	
	Public Shared Function GetIMDBPageUrl(ID As Integer, Optional PageType As String = "title", Optional PageSubType As String = "") As Uri		
		Return New Uri("http://www.imdb.com/" + PageType.tostring().tolower() + "/tt" + ID.ToString() + "/" + PageSubType)		
	End Function
	
	Public Shared Function GetTVcomShowSumaryUrl(ShowID As Integer) As Uri		
		Return New Uri("http://www.tv.com/show/" + ShowID.tostring() + "/summary.html")		
	End Function
	
	Public Shared Function GetWikipediaFilePathLink(FileName As String, Optional WikipediaLanguage As String = "en") As Uri 		
		Return New Uri ("http://"+ WikipediaLanguage.ToLower() + ".wikipedia.org/wiki/Special:FilePath/" + FileName.replace(" ","_"))
	End Function
	
	Public Shared Function GetWikipediaArticleLinkByPageName(PageName As String, Optional WikipediaLanguage As String = "en") As uri 					
		Return New Uri( "http://" + WikipediaLanguage.ToLower() + ".wikipedia.org/wiki/" + PageName.Replace(" ", "_") )		
	End Function
	
	Public Shared Function ExtractIDFromAnimeNewsNetworkLink(Uri As Uri) As Integer 		
		Dim m As Match = regex.Match(Uri.ToString(), "id=(\d+)")		
		If m.Success Then
			Return val(m.Groups.Item(1).Value)
		End If
		Return -1			
	End Function
	
	Public Shared Function ExtractIDFromAniDBLink(Uri As Uri) As Integer		
		Dim m As Match = regex.Match(Uri.ToString(), "aid=(\d+)")
		If m.Success Then
			Return val(m.Value.Substring("aid=".Length))
		End If
		Return -1		
	End Function
	
	Private Sub RunSpecialTimer(Name As String, HowLong As Int16)
		Static SpecialTimers As New Dictionary(Of String, Integer)	
		Do 
			application.DoEvents()		
		Loop While SpecialTimers.ContainsKey(Name)
		SpecialTimers.Add(name, howlong)
		Dim t As DateTime = DateTime.Now()
		Do
			application.DoEvents()
			SpecialTimers.Item(name) -= 1
		Loop While DateTime.Now.Ticks > (t.Ticks + SpecialTimers.Item(name))
		SpecialTimers.Remove(name)		
	End Sub
		
	Public Function DownloadWikipediaString(Uri As String, Optional UseRedirects as Boolean = true) As String
		Dim Rez as String ="" 
		Dim webRequest As System.Net.HttpWebRequest = DirectCast(System.Net.WebRequest.Create(New Uri(uri)), HttpWebRequest)		
		webRequest.Credentials = System.Net.CredentialCache.DefaultCredentials
		webRequest.Accept = "text/xml"
		Try  
    		Dim webResponse As System.Net.HttpWebResponse = DirectCast(webRequest.GetResponse(), HttpWebResponse)    
    		Dim responseStream As System.IO.Stream = webResponse.GetResponseStream()
    		Dim reader As System.Xml.XmlReader = New XmlTextReader(responseStream)
    		Dim NS As String = "http://www.mediawiki.org/xml/export-0.3/"
    		Dim doc As New XPathDocument(reader)
    		reader.Close()
    		webResponse.Close()    
    		Dim myXPahtNavigator As XPathNavigator = doc.CreateNavigator()
		    Dim nodesText As XPathNodeIterator = myXPahtNavigator.SelectDescendants("text", NS, False)
		    While nodesText.MoveNext()
                rez = nodesText.Current.InnerXml & " "
    		End While
		Catch ex As Exception
			rez = ex.ToString()   
		End Try		
		If UseRedirects Then
			Dim rstring as String = Me.GetWikipediaStringForRedirect(rez)
			If rstring <> "" Then
				rez = Me.DownloadWikipediaString(GetWikipediaExportURL(rstring))
			End If
		End If
		Return rez		
	End Function
	
	
	Public Function GetAniDBRating(AnimeId As Integer) As AniDBRating
		Dim rez As New AniDBRating()
		Dim ratings_content As String = Me.DownloadWebDocument("http://anidb.net/perl-bin/animedb.pl?show=animevotes&aid=" + AnimeId.ToString())
		Dim reviews_content As String = Me.DownloadWebDocument("http://anidb.net/perl-bin/animedb.pl?show=animeatt&aid=" + AnimeId.ToString())
		If ratings_content IsNot Nothing Then
			If ratings_content.Trim().Length > 0 Then
				Dim ratings_matches As RegularExpressions.MatchCollection = regex.Matches(ratings_content, "(\w+) votes\s+\(([^\)]+)\)")
				If ratings_matches.Count > 0 Then
					For Each m2 As Match In ratings_matches						
						Select Case m2.Groups.Item(1).Value.ToString().ToLower().Trim()
							Case "permanent"								
								rez.Permanent = val(m2.Groups.Item(2).Value)
							Case "temporary"
								rez.Temporary = val(m2.Groups.Item(2).Value)
						End Select
					Next
				End If
			End If
		End If
		If reviews_content IsNot Nothing Then
			If reviews_content.Trim().Length > 0 Then				
				Dim reviews_match As RegularExpressions.Match = regex.Match(reviews_content, "Reviews for [^\(]+\(([^\)]+)\)")
				If reviews_match.Success Then
					rez.Reviews = val(reviews_match.Groups.Item(1).Value)					
				End If
			End If
		End If		
		Return rez		
	End Function
	
	Public Function DownloadAniDBXML(Uri As String) As Xml.XmlDocument		
		Return Me.DownloadAniDBXML(New Uri(uri))				
	End Function
	
	Public Shared Function GetAniDBXMLLink(AnimeId As Integer) As Uri		
		Return New Uri("http://anidb.net/perl-bin/animedb.pl?show=xml&aid=" + AnimeId.ToString() + "&t=anime&client=" + application.ProductName + "&clientver=" + application.ProductVersion.Split(".")(0).ToString())		
	End Function
	
	Public Function DownloadAniDBXML(AnimeId As Integer) As Xml.XmlDocument
		Return DownloadAniDBXML(GetAniDBXMLLink(AnimeId))			
	End Function
		
	Public Function DownloadAniDBXML(Uri As Uri) As Xml.XmlDocument	
		me.RunSpecialTimer("AniDB.net Query", 5000)		
		Dim webRequest As System.Net.HttpWebRequest = DirectCast(System.Net.WebRequest.Create(uri), HttpWebRequest)		
		webRequest.Credentials = System.Net.CredentialCache.DefaultCredentials
		webRequest.Accept = "text/xml"
		Dim webResponse As System.Net.HttpWebResponse = DirectCast(webRequest.GetResponse(), HttpWebResponse)    		
    	Dim responseStream As System.IO.Stream = webResponse.GetResponseStream()    		
    	Dim sread as new StreamReader(responseStream)
    	Dim allData As String = sread.ReadToEnd().Trim()    	
    	Dim xd As New Xml.XmlDocument()
    	xd.LoadXml(allData)
    	responseStream.Close()    				
		Return xd
	End Function
	
	Public Function GetWikipediaStringForRedirect(content As String) As String
		If content.Trim().StartsWith("#REDIRECT") Then
			Dim rm As Match
			rm = regex.Match(content, "\[\[(.*)\]\]")
			If rm.Success Then
				Return rm.ToString().Substring(2, rm.ToString().Length - 4)						
			End If			
		End If
		Return ""		
	End Function
		
	Public Shared Function GetWikipediaExportURL(WhatExport As String) As Uri		
		Return New Uri("http://en.wikipedia.org/wiki/Special:Export/" + whatexport)		
	End Function
	
	Public Function DownloadWikipediaString(Uri As Uri) As String
		Return Me.DownloadWikipediaString(uri.ToString())		
	End Function
	
	Public Function DownloadWebDocument(uri As uri) As  String
		return me.DownloadWebDocument(uri.ToString())
	End Function
	
	Public Function DownloadWebDocument(uri As String) As  String				
		downloading = True
		Me.wb.ScriptErrorsSuppressed = True		
		Me.wb.Navigate(uri)
		Dim FirstTime As DateTime = DateTime.Now
		Do		
			Application.DoEvents()			
		Loop While Downloading And DateTime.Now.Subtract(FirstTime).TotalMinutes < 3		
		Return Me.wb.DocumentText		
	End Function	
	
	Private Sub wbDocumentFinished() Handles wb.DocumentCompleted
		Downloading = False		
	End Sub	
	
	Public Shared Function StripHTMLComments(strTextToReplace As String) As String		
		Dim regExp as New Regex("<!(?:--[\s\S]*?--\s*)?>\s*")
        strTextToReplace = regExp.Replace(strTextToReplace, "")
        Return strTextToReplace        
	End Function
	
	Public Function Translate(Text As String, FromLanguage As String, ToLanguage As String) As String
		Dim rez As String = ""
		Dim url As String = "http://ajax.googleapis.com/ajax/services/language/translate?v=1.0&q="
		url += System.Web.HttpUtility.UrlEncode(Text)
		url += "&langpair="
		url += System.Web.HttpUtility.UrlEncode(fromLanguage.ToLower() + "|" + toLanguage.ToLower())
		Dim rq As New Net.WebClient()
		rq.Credentials = System.Net.CredentialCache.DefaultCredentials		
		Dim content As String = rq.DownloadString(url)
		Dim tr As IO.TextReader = New StringReader(content)		
		Dim jp As New Jayrock.Json.JsonTextReader(tr)
		Dim jr As New Jayrock.Json.JsonObject()
		jr.Import(jp)
		Dim citem As Object = jr.Item("responseData")
		If citem IsNot Nothing Then
			content = citem.ToString()
			jr.Clear()			
			jp = New Jayrock.Json.JsonTextReader(DirectCast(New StringReader(content), IO.TextReader))			
			jr.Import(jp)
		End If
		citem = jr.Item("translatedText")
		If citem IsNot Nothing Then
			rez = citem.ToString()
		End If		
		Return rez
	End Function
	
	Public Shared Function ExtractFromEmbededCodeMovies(Code As String) As Collections.Generic.List(Of String)
		Dim rez As New Collections.Generic.List(Of String)()		
		Dim m2 As RegularExpressions.MatchCollection 
		Dim rgx() As String = { _
								"<param\s+name=""movie""\s+value=""([^""]+)""", _
								"<param\s+value=""([^""]+)""\s+name=""movie""", _
								"<embed.+src=""([^""]+)""" _
							  }		
		For Each item As String In rgx
			m2 = regex.Matches(code, item)
			For Each m As RegularExpressions.Match In m2
				If m.Success Then			
					Dim nm as String = m.Groups.Item(1).Value.Trim()
					If nm.Length > 0 And Not rez.Contains(nm) Then						
						rez.add(nm)
					End If					
				End If			
			Next			
		Next		
		Return rez		
	End Function
	
End Class