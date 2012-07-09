Imports System.Text.RegularExpressions
Imports System.Globalization
Imports GenDesc.Library.Text


Namespace Import

	Class FromWikipediaURL		
		Inherits Import.Base		
		
		Public Overrides Sub AddAll() 
	
			Me.AddTodo_GetWikipediaData()
			Me.AddTodo_AnimeLinksCodesWikiReplace()			
			Me.AddTodo_AnimeNewsNetworkCodesReplace()
			Me.AddTodo_IMDBCodesReplace()
			Me.AddTodo_TVcomCodesReplace()
			Me.AddTodo_FlagsCodesReplace()
			Me.AddTodo_ImageCodesReplace()
			Me.AddTodo_CollectLinks()
			Me.AddTodo_LanguageFlagsReplace()
			Me.AddTodo_NoWrapReplace()
			Me.AddTodo_ExtractInfobox()
		
		End Sub
		
		Private Property WikipediaContent As String
			Get
				Return Me.Actions.Data("Wikipedia")				
			End Get
			Set (value As String)
				Me.Actions.Data("Wikipedia") = value
			End Set
		End Property
		
		Public Sub AddTodo_GetWikipediaData()
			Me.Actions.Add(AddressOf Me.wkGetWikipediaData)			
		End Sub
		
		Private Sub wkGetWikipediaData()			
			me.Actions.Text = "Getting initial info..."
			Dim uri As Uri = Me.objDataItem.WikipediaProfileLink
			Dim wb As New WebBrowserContentDonwloader()	
			Dim ub As New UriBuilder(uri)	
			Dim url As String = WebBrowserContentDonwloader.GetWikipediaExportURL(Io.Path.GetFileName(ub.Path.Replace("/","\"))).ToString()
			Me.WikipediaContent = wb.DownloadWikipediaString(url)		
			Me.WikipediaContent = web.HttpUtility.HtmlDecode(Me.WikipediaContent)		
		End Sub
		
		Public Sub AddTodo_AnimeNewsNetworkCodesReplace()
			Me.Actions.Add(AddressOf Me.wkAnimeNewsNetworkWikiReplace)
		End Sub
		
		Private Sub wkAnimeNewsNetworkWikiReplace()
			Me.Actions.Text = "Replacing AnimeNewsNetwork links..."					
			Dim mc As MatchCollection = regex.Matches(WikipediaContent, "{{\s*ann([^\|]+|[^|]*)\|([^}]+)}}")
			Dim flagName As String 		
			For Each mc2 As Match In mc
				If mc2.Success Then
					Dim items() As String = mc2.Groups.Item(2).Value.Split("|")
					For Each item As String In items
						If item.IndexOf("=") > -1 Then
							Dim parts() As String = item.Split("=")
							If parts(0).Trim().ToLower() = "id" Then
								flagname = String.Concat("[" , _
							   		WebBrowserContentDonwloader.GetAnimeNewsNetworkPageUrl(val(parts(1))) , _
							   	"]")
								WikipediaContent = WikipediaContent.Replace(mc2.Captures.Item(0).Value.ToString(), flagname)
								Exit For							
							End If
						Else
							If val(item.Trim()).ToString() = item.Trim() Then
								flagname = String.Concat("[" , _
							   		WebBrowserContentDonwloader.GetAnimeNewsNetworkPageUrl(val(item)) , _
							   	"]")
								WikipediaContent = WikipediaContent.Replace(mc2.Captures.Item(0).Value.ToString(), flagname)
								Exit For							
							End If
						End If						
					Next					
				End If						
			Next	
		End Sub
		
		Public Sub AddTodo_AnimeLinksCodesWikiReplace()
			Me.Actions.Add(AddressOf Me.wkAnimeLinksCodesWikiReplace)
		End Sub
		
		Private Sub wkAnimeLinksCodesWikiReplace()
			Me.Actions.Text = "Replacing AnimeLinksCodes links..."					
			Dim mc As MatchCollection = regex.Matches(WikipediaContent, "{{\s*anime\-links([^\|]+|[^|]*)\|([^}]+)}}")			
			For Each mc2 As Match In mc
				If mc2.Success Then
					Dim items() As String = mc2.Groups.Item(2).Value.Split("|")
					Dim rez As String = ""
					For Each item As String In items
						Dim parts() As String = item.Split("=")
						Select Case parts(0).Trim().ToLower()
							Case "ann"
								rez += String.Concat("[" , _
							   		WebBrowserContentDonwloader.GetAnimeNewsNetworkPageUrl(val(parts(1))) , _
							    "]")
							Case "imdb"
								rez += String.Concat("[" , _
							   		WebBrowserContentDonwloader.GetAnimeNewsNetworkPageUrl(val(parts(1))) , _
							    "]")
							Case "tvcom"
								rez += String.Concat("[" , _
							   		WebBrowserContentDonwloader.GetTVcomShowSumaryUrl(val(parts(1))) , _
							    "]")
						End Select						
					Next
					WikipediaContent = WikipediaContent.Replace(mc2.Captures.Item(0).Value.ToString(), rez)
				End If						
			Next	
		End Sub		
		
		Public Sub AddTodo_IMDBCodesReplace()
			Me.Actions.Add(AddressOf Me.wkIMDBWikiReplace)
		End Sub
		
		Private Sub wkIMDBWikiReplace()
			me.Actions.Text = "Replacing IMDB.com links..."	
			Dim mc As MatchCollection = regex.Matches(WikipediaContent, "{{\s*imdb([^\|]+|[^|]*)\|([^}]+)}}")
			Dim flagName As String 		
			For Each mc2 As Match In mc
				If mc2.Success Then
					Dim items() As String = mc2.Groups.Item(2).Value.Split("|")
					For Each item As String In items
						Dim parts() As String = item.Split("=")
						If parts(0).Trim().ToLower() = "id" Then
							flagname = String.Concat("[" , _
							   WebBrowserContentDonwloader.GetIMDBPageUrl(val(parts(1))) , _
							   "]")
							WikipediaContent = WikipediaContent.Replace(mc2.Captures.Item(0).Value.ToString(), flagname)
							Exit For							
						End If
					Next					
				End If						
			Next
		End Sub	
		
		Public Sub AddTodo_TVcomCodesReplace()
			Me.Actions.Add(AddressOf Me.wkTVcomWikiReplace)
		End Sub
		
		Private Sub wkTVcomWikiReplace()
			Me.Actions.Text = "Replacing TV.com links..."	
			Dim mc As MatchCollection = regex.Matches(WikipediaContent, "{{\s*tv\.com([^\|]+|[^|]*)\|([^}]+)}}")
			Dim flagName As String 		
			For Each mc2 As Match In mc
				If mc2.Success Then
					Dim items() As String = mc2.Groups.Item(2).Value.Split("|")
					For Each item As String In items
						Dim parts() As String = item.Split("=")
						If parts(0).Trim().ToLower() = "id" Then
							flagname = String.Concat("[" , _
							   WebBrowserContentDonwloader.GetTVcomShowSumaryUrl(val(parts(1))) , _
							   "]")
							WikipediaContent = WikipediaContent.Replace(mc2.Captures.Item(0).Value.ToString(), flagname)
							Exit For							
						End If
					Next					
				End If						
			Next			
		End Sub
		
		Public Sub AddTodo_FlagsCodesReplace()
			Me.Actions.Add(AddressOf Me.wkFlagsWikiReplace)
		End Sub
		
		Private Sub wkFlagsWikiReplace()
			me.Actions.Text = "Replacing flags..."
			Dim mc As MatchCollection = regex.Matches(Me.WikipediaContent, "{{\s*flagicon\s*\|([^}}]*)}}")
			Dim flagName As String, flagName2 As String, I As Integer		
			For Each mc2 As Match In mc
				If mc2.Success Then
					flagName = mc2.Captures.Item(0).Value.ToString().Substring("{{flagicon|".Length, mc2.Captures.Item(0).Value.ToString().Length - "{{flagicon|".Length - 2)
					'flagname = flagname.Replace(" ", "_")
					i = flagName.IndexOf("|")
					If i > -1 Then
						flagname = flagname.Substring(0, i)					
					End If
					flagname = flagname.Substring(0, 1).ToUpper() + flagname.Substring(1).ToLower()						
					flagname2 = Library.Globalization.GetCountry2LettersCode(flagname)				
					If flagname2 Is Nothing Then 
						'flagname = ""
					ElseIf flagname2 = "" Then 
						'do nothing
					Else
						flagname = ""'"[img]http://www.codeproject.com/script/Geo/Images/" + flagname2 + ".gif[/img] "' + flagname
					End If				
					Me.WikipediaContent = Me.WikipediaContent.Replace(mc2.Captures.Item(0).Value.ToString(), flagname)				
				End If						
			Next			
		End Sub
		
		Public Sub AddTodo_ImageCodesReplace()
			Me.Actions.Add(AddressOf Me.wkImageCodeWikiReplace)
		End Sub
	
		Private Sub wkImageCodeWikiReplace()
			me.Actions.Text = "Replacing image codes..."
			Dim ImageName As String, I as Integer	
			Dim mc As MatchCollection = regex.Matches(Me.WikipediaContent, "\[\[Image:[^\]]*\]\]")
			For Each mc2 As Match In mc
				If mc2.Success Then
					ImageName = mc2.Captures.Item(0).Value.ToString().Substring("[[Image:".Length, mc2.Captures.Item(0).Value.ToString().Length - "[[Image:".Length - 2)
					i = ImageName.IndexOf("|")
					If i > -1 Then
						ImageName = ImageName.Substring(0, i)
					End If						
					Me.WikipediaContent = Me.WikipediaContent.Replace(mc2.Captures.Item(0).Value.ToString(), _
					"[img]" + WebBrowserContentDonwloader.GetWikipediaFilePathLink(ImageName).ToString() + "[/img]")
				End If
			Next 		
		End Sub
		
		Public Sub AddTodo_CollectLinks()
			Me.Actions.Add(AddressOf Me.wkLinksCollectorWiki)
		End Sub
	
		Private Sub wkLinksCollectorWiki()
			me.Actions.Text = "Replacing/collecting article links..."
			Dim mc As MatchCollection = regex.Matches(Me.WikipediaContent, "\[[^\]]*\]")
			Dim ImageName As String, I As Integer 	
			Dim flagname As String		
			For Each mc2 As Match In mc
				If mc2.Success Then
					If mc2.Captures.Item(0).Value.ToString().Substring(0, 2) = "[[" Then					
						ImageName = mc2.Captures.Item(0).Value.ToString().Substring(2, mc2.Captures.Item(0).Value.ToString().Length - 3)
						Dim parts() As String = ImageName.Split("|")	
						If parts.Length = 2 Then
							ImageName = String.Concat( "[url=" , WebBrowserContentDonwloader.GetWikipediaArticleLinkByPageName(parts(0)).tostring() , "]" , parts(1) ,"[/url]")						
						Else
							ImageName = String.Concat( "[url=" , WebBrowserContentDonwloader.GetWikipediaArticleLinkByPageName(ImageName).tostring(), "]" , ImageName ,"[/url]")						
						End If
						Me.WikipediaContent = Me.WikipediaContent.Replace(mc2.Captures.Item(0).Value.ToString() + "]", ImageName)
					Else
						If mc2.Captures.Item(0).Value.ToString().StartsWith("[img]") Then Continue For				
						If mc2.Captures.Item(0).Value.ToString().StartsWith("[/img]") Then Continue For
						ImageName = mc2.Captures.Item(0).Value.ToString().Substring(1, mc2.Captures.Item(0).Value.ToString().Length - 2)
						i = ImageName.IndexOf(" ")
						If i > -1 Then
							flagname = imagename.Substring(i)
							imagename = imagename.Substring(0, i)
						Else
							flagname = ""
						End If					
						Me.objDataItem.AutoSetExternalLink(imagename, flagname)
						If flagname = "" Then
							imagename = "[url=" + imagename + "]" + imagename + "[/url]"
						Else
							imagename = "[url=" + imagename + "]" + flagname + "[/url]"
						End If
						Me.WikipediaContent = Me.WikipediaContent.Replace(mc2.Captures.Item(0).Value.ToString(), ImageName)
					End If				
				End If
			Next 		
		End Sub
		
		Public Sub AddTodo_NoWrapReplace()
			Me.Actions.Add(AddressOf Me.wkNowrapReplace)
		End Sub
		
		Public Sub wkNowrapReplace()
			Me.Actions.Text = "Replacing nowrap codes at Wikipedia..."			
			Me.WikipediaContent = System.Text.RegularExpressions.Regex.Replace(Me.WikipediaContent, "{{\s*nowrap\s*\|([^}]*)}}", "$1")
		End Sub
		
		Public Sub AddTodo_LanguageFlagsReplace()
			Me.Actions.Add(AddressOf Me.wkLanguageFlagsWikiReplace)
		End Sub
		
		Private Sub wkLanguageFlagsWikiReplace()
			Me.Actions.Text = "Replacing language flags..."			
			Dim cultures() As CultureInfo = CultureInfo.GetCultures(CultureTypes.AllCultures And Not CultureTypes.NeutralCultures)
			For Each culture As CultureInfo In cultures
				Dim Region As New RegionInfo(culture.LCID)				
				Me.WikipediaContent = Me.WikipediaContent.replace("{{" + Region.Name + "}}", Region.EnglishName)
				Me.WikipediaContent = Me.WikipediaContent.replace("{{" + Region.DisplayName + "}}", Region.EnglishName)
				Me.WikipediaContent = Me.WikipediaContent.replace("{{" + Region.NativeName + "}}", Region.EnglishName)
				Me.WikipediaContent = Me.WikipediaContent.replace("{{" + Region.ThreeLetterWindowsRegionName.Substring(0, 2) + "}}", Region.EnglishName)
				Me.WikipediaContent = Me.WikipediaContent.replace("{{" + Region.ThreeLetterWindowsRegionName + "}}", Region.EnglishName)
				Me.WikipediaContent = Me.WikipediaContent.replace("{{" + Region.ThreeLetterISORegionName + "}}", Region.EnglishName)
				Me.WikipediaContent = Me.WikipediaContent.replace("{{" + Region.TwoLetterISORegionName + "}}", Region.EnglishName)
			Next
		End Sub
		
		Public Sub AddTodo_ExtractInfobox()
			Me.Actions.Add(AddressOf Me.wkInfoboxWikiExtract)
		End Sub
		
		Private Sub wkInfoboxWikiExtract()
			me.Actions.Text = "Extracting data from infoboxes..."
			Dim mc As MatchCollection = regex.Matches(Me.WikipediaContent, "{{((\s+)infobox|infobox)(\s+)(.[^{}]*)}}", RegexOptions.IgnoreCase)
			Dim InfoBoxes As New Collections.Generic.List(Of String)
			Dim possibleParts() As String = {"television", "animanga/anime", "animanga/header" }
			Dim i As Integer, imagename As String, flagname As String		
			Dim isAnime As Boolean, isFromDateSet As Boolean = False			
			Me.objDataItem.NumOfSeasons = 0
			Me.objDataItem.NumOfEpisodes = 0
			Me.objDataItem.OriginalChannels = New String() {}
			Me.objDataItem.Director = New String() {}
			Me.objDataItem.Studio = New String() {}
			For Each mc2 As Match In mc
				If mc2.Success Then				
					Dim parts() As String = mc2.Groups.Item(4).Value.Split("|")
					parts(0) = parts(0).Trim().ToLower()
					If array.IndexOf(Of String)(possibleParts, parts(0)) < 0 Then Continue For
					isAnime = False
					If parts(0) = "animanga/header" Then 
						Me.objDataItem.CountryOfOrigin = Library.Arrays.Smart.AddToArray(Me.objDataItem.CountryOfOrigin, "Japan")					
					elseIf parts(0) = "animanga/anime" Then 
						Me.objDataItem.CountryOfOrigin = Library.Arrays.Smart.AddToArray(Me.objDataItem.CountryOfOrigin, "Japan")
						Me.objDataItem.NumOfSeasons += 1
						isAnime = True																	
					End If				
					For Each part As String In parts
						i = part.IndexOf("=")
						If i < 0 Then Continue For					
						imagename = web.HttpUtility.HtmlDecode(part.Substring(0, i).Trim())
						flagname = web.HttpUtility.HtmlDecode(part.Substring(i + 1).Trim())
						If flagname.Trim().Length = 0 Then Continue	For						
						Select Case imagename
							Case "image"
								Me.objDataItem.ImageURL = Library.Text.BBCode.Trim(flagname)
							Case "genre", "format"
								Me.objDataItem.Genre = Library.Text.BBCode.MakeStringsList(flagname, Library.Text.bbCode.TrimBBCodeMethod.TrimV2 )
							Case "director"
								Me.objDataItem.Director = Library.Arrays.Smart.Merge(Of String)(Me.objDataItem.Director, Library.Text.BBCode.MakeStringsList(flagname, Library.Text.bbCode.TrimBBCodeMethod.TrimV2))
							Case "studio", "company"
								Me.objDataItem.Studio =  Library.Arrays.Smart.Merge(Of String)(Me.objDataItem.Studio, Library.Text.BBCode.MakeStringsList(flagname, Library.Text.bbCode.TrimBBCodeMethod.TrimV2))
							Case "network"
								Me.objDataItem.OriginalChannels = Library.Arrays.Smart.Merge(Of String)(Me.objDataItem.OriginalChannels, Library.Text.BBCode.MakeStringsList(flagname, Library.Text.bbCode.TrimBBCodeMethod.TrimV2))
							Case "first", "first_aired"
								If isFromDateSet Then Exit Select								
								Try
									Dim kp() As String = flagname.Replace(vbcrlf, vbcr).Replace(vblf, vbcr).Replace(vbcr,"<br>").Replace("<br>","<br />").Replace("<br>","<br />").Replace("<br />","<br/>").Split("<br/>")								
									Me.objDataItem.FirstAirDate = convert.ToDateTime( Library.Text.BBCode.Trim( kp(0).Trim(), Library.Text.BBCode.TrimBBCodeMethod.TrimV2))
									isFromDateSet = True									
								Catch ex As Exception
									
								End Try							
							Case "last", "last_aired"
								If flagname = "current" Or flagname = "now" Or flagname = "running" Or flagname = "present" Then
									Me.objDataItem.LastAirDate = Nothing								
									Me.objDataItem.OnAirOrInProduction = True
								Else
									Try
										Dim kp() As String = flagname.Replace(vbcrlf, vbcr).Replace(vblf, vbcr).Replace(vbcr,"<br>").Replace("<br>","<br />").Replace("<br>","<br />").Replace("<br />","<br/>").Split("<br/>")								
										Me.objDataItem.LastAirDate = convert.ToDateTime( Library.Text.BBCode.Trim( kp(0).Trim(), Library.Text.BBCode.TrimBBCodeMethod.TrimV2))
									Catch ex As Exception
									
									End Try										 
									Me.objDataItem.OnAirOrInProduction = False								
								End If							
							Case "episodes", "num_episodes", "num_series"
								If isAnime Then
									Me.objDataItem.NumOfEpisodes += val(flagname)
								Else
									Me.objDataItem.NumOfEpisodes = val(flagname)								
								End If
							Case "runtime"								
								Me.objDataItem.AverageRunTime = val(flagname)
							Case "creator"
								Me.objDataItem.CreatedBy = Library.Text.BBCode.MakeStringsList(flagname, Library.Text.BBCode.TrimBBCodeMethod.TrimV2)
							Case "voices", "starring"
								Me.objDataItem.Starring = Library.Arrays.Smart.Merge(Of String)(Me.objDataItem.Starring, Library.Text.BBCode.MakeStringsList(flagname, Library.Text.BBCode.TrimBBCodeMethod.TrimV2))
							Case "theme_music_composer"
								Me.objDataItem.Music = Library.Text.BBCode.MakeStringsList(flagname, Library.Text.BBCode.TrimBBCodeMethod.TrimV2)
							Case "country"
								Me.objDataItem.CountryOfOrigin = Library.Arrays.Smart.Merge(Me.objDataItem.CountryOfOrigin, Library.Text.BBCode.MakeStringsList(flagname, Library.Text.BBCode.TrimBBCodeMethod.TrimV2))
							Case "num_seasons"
								Me.objDataItem.NumOfSeasons = val(flagname)
							Case "website"
								Me.objDataItem.WebSite = New Uri(flagname)		
							Case "ja_kanji", "ja_romaji", "show_name_2"
								Me.objDataItem.aka = Library.Arrays.Smart.Merge(Me.objDataItem.aka, Library.Text.BBCode.MakeStringsList(flagname, Library.Text.BBCode.TrimBBCodeMethod.TrimV2))
							Case "writer"
								Me.objDataItem.WritedBy = Library.Text.BBCode.MakeStringsList(flagname, Library.Text.BBCode.TrimBBCodeMethod.TrimV2)															
							Case "preceded_by"
								Me.objDataItem.PrecededBy = Library.Text.BBCode.MakeStringsList(flagname, Library.Text.BBCode.TrimBBCodeMethod.TrimV2)
							Case "followed_by"
								Me.objDataItem.FollowedBy = Library.Text.BBCode.MakeStringsList(flagname, Library.Text.BBCode.TrimBBCodeMethod.TrimV2)
							Case "related"
								Me.objDataItem.Related = Library.Text.BBCode.MakeStringsList(flagname, Library.Text.BBCode.TrimBBCodeMethod.TrimV2)							
						End Select					
					Next
				End If
			Next
		End Sub
		
	End Class
	
End Namespace