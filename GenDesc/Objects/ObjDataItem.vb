Imports System
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Net
Imports System.Reflection

Public Class ObjDataItem
	
	Public Structure OtherLink
		Private _SiteName As String
		Private _URL As Uri
		Public Property Name As String 
			Get
				Return Me._SiteName				
			End Get
			Set (value As String)
				me._SiteName = value
			End Set
		End Property
		Public Property URL As Uri 
			Get
				Return Me._URL
				
			End Get
			Set (value As Uri)
				me._URL = value
			End Set
		End Property
	End Structure
	
	Public Structure Opinion
		Private _Who As String		
		Private _SiteName As String
		Private _URL As Uri
		Private _Opinion As String()
		
		<Browsable(false)> _
		Public ReadOnly Property Name As String 			
			Get
				Return Me._Who & " @ " & Me._SiteName
			End Get		
		End Property			
		<Browsable(false)> _
		Public Property SiteName As String 
			Get
				Return Me._SiteName
				
			End Get
			Set (value As String)
				me._SiteName = value
			End Set
		End Property
		Public Property URL As Uri 
			Get
				Return Me._URL				
			End Get
			Set (value As Uri)
				Me._URL = value
				Me.SiteName = value.Host
				If Me.SiteName.Substring(0, 4) = "www." Then 
					Me.SiteName = Me.SiteName.Substring(4)
				ElseIf Me.SiteName.Substring(0, 4) = "ftp." Then 
					Me.SiteName = Me.SiteName.Substring(4)
				End If
			End Set
		End Property
		Public Property Who As String 
			Get
				Return Me._Who
			End Get
			Set (value As String)
				me._Who = value
			End Set
		End Property
		Public Property Opinion As String()
			Get
				Return Me._Opinion
			End Get
			Set (value As String())
				me._Opinion = value
			End Set
		End Property
	End Structure	
		
	Private _Image As String 	
	Private _Genre As String()	
	Private _About As String 
	Private _Countries As String()
	Private _Languages As String()
	Private _Channels As String()
	Private _Starring As String()
	Private _Director As String()
	Private _Producers As String()
	Private _Studio As String()
	Private _Creators As String()
	Private _Music As String()
	Private _RunTime As Integer 
	Private _Seasons As Integer	
	Private _Episodes As Integer
	Private _FirstAired As Date
	Private _LastAired As Date 
	Private _OnAir As Boolean 
	Private _Nominations As String()
	Private _Awards As String()
	Private _WebSite As Uri
	Private _IMDBLink As Uri 
	Private _TVLink As Uri
	Private _WikipediaLink As Uri 
	Private _AniDBLink As Uri
	Private _ANNLink As Uri
	Private _OtherLinks As Collections.Generic.List(Of OtherLink)	
	Private _Youtube As String()
    Private _TrumbnailsURL As String
    Private _aka As String()
	Private _WhatIThink As String
	Private _OtherOpinions As Collections.Generic.List(Of Opinion)	
	Private _IMDBrating As Double
	Private _TVrating As Double
	Private _ANNrating As Double
	Private _AniDBrating As Double
	Private _Writer As String()
	Private _Preceded_by As String()
	Private _Followed_by As String()
	Private _Related As String()	
	
	Private __WebBrowser As New Net.WebClient()		

	Sub New()
		Me.Clear()		
	End Sub
	
	<Category("Relations")> _
	Public Property Related As String()
		Get
			Return Me._Related
		End Get
		Set (value As String())
			me._Related = value
		End Set
	End Property
	
	<Category("Relations")> _
	Public Property FollowedBy As String()
		Get
			Return Me._Followed_by
		End Get
		Set (value As String())
			me._Followed_by = value
		End Set
	End Property
	
	<Category("Relations")> _
	Public Property PrecededBy As String()
		Get
			Return Me._Preceded_by
		End Get
		Set (value As String())
			me._Preceded_by = value
		End Set
	End Property
	
	<Category("Info")> _
	Public Property WritedBy As String()
		Get			
			Return Me._Writer			
		End Get
		Set (value As String())
			me._Writer = value
		End Set
	End Property

	<Category("Ratings")> _
	Public Property AniDBRating As Double 
		Get
			Return Me._anidbRating			
		End Get
		Set (value As Double)
			Me._anidbRating = value 				
		End Set
	End Property

	<Category("Ratings")> _
	Public Property AnimeNewsNetworkRating As Double 
		Get
			Return Me._ANNRating			
		End Get
		Set (value As Double)
			Me._ANNRating = value 				
		End Set
	End Property

	<Category("Ratings")> _
	Public Property TVcomRating As Double 
		Get
			Return Me._TVRating			
		End Get
		Set (value As Double)
			Me._TVRating = value 				
		End Set
	End Property

	<Category("Ratings")> _
	Public Property IMDBRating As Double 
		Get
			Return Me._IMDBrating			
		End Get
		Set (value As Double)
			Me._IMDBrating = value 				
		End Set
	End Property

	<Category("Description")> _
	Public Property OtherOpinions As Collections.Generic.List(Of Opinion)
		Get
			Return Me._OtherOpinions			
		End Get
		Set (value As Collections.Generic.List(Of Opinion))
			Me._OtherOpinions = value 				
		End Set
	End Property

	<Category("Description")> _
	Public Property WhatIThink As String
		Get
			Return Me._WhatIThink			
		End Get
		Set (value As String)
			Me._WhatIThink = value 				
		End Set
	End Property

	<Category("Info")> _
	Public Property aka As String()
		Get
			Return Me._aka			
		End Get
		Set (value As String())
			Me._aka = value 				
		End Set
	End Property

    <Category("Images")>
    Public Property ThumbnailURL As String
        Get
            Return Me._TrumbnailsURL
        End Get
        Set(value As String)
            Me._TrumbnailsURL = value
        End Set
    End Property

    <Category("Media")> _
	Public Property YoutubeEmbededCodes As String()
		Get
			Return Me._Youtube			
		End Get
		Set (value As String())
			Me._Youtube = value 				
		End Set
	End Property

	<Category("Links")> _
	Public Property OtherLinks As Collections.Generic.List(Of OtherLink)
		Get
			Return Me._OtherLinks			
		End Get
		Set (value As Collections.Generic.List(Of OtherLink))
			Me._OtherLinks = value 				
		End Set
	End Property

	<Category("Links")> _
	Public Property AnimeNewsNetworkProfileLink As Uri
		Get
			Return Me._annlink			
		End Get
		Set (value As Uri)
			Me._annlink = value 				
		End Set
	End Property

	<Category("Links")> _
	Public Property AniDBProfileLink As Uri
		Get
			Return Me._anidblink			
		End Get
		Set (value As Uri)
			Me._anidblink = value 				
		End Set
	End Property

	<Category("Links")> _
	Public Property WikipediaProfileLink As Uri
		Get
			Return Me._wikipedialink			
		End Get
		Set (value As Uri)
			Me._wikipedialink = value 				
		End Set
	End Property

	<Category("Links")> _
	Public Property TVcomProfileLink As Uri
		Get
			Return Me._TVLink
		End Get
		Set (value As Uri)
			Me._tvlink = value 				
		End Set
	End Property

	<Category("Links")> _
	Public Property IMDBProfileLink As Uri
		Get
			Return Me._imdblink			
		End Get
		Set (value As Uri)
			Me._imdblink = value 				
		End Set
	End Property

	<Category("Links")> _
	Public Property WebSite As Uri
		Get
			Return Me._website
		End Get
		Set (value As Uri)
			Me._website = value 				
		End Set
	End Property

	<Category("Achievements")> _
	Public Property Awards As String()
		Get
			Return Me._awards
		End Get
		Set (value As String())			
			Me._awards = value 				
		End Set
	End Property

	<Category("Achievements")> _
	Public Property Nominations As String()
		Get
			Return Me._nominations
		End Get
		Set (value As String())			
			Me._nominations = value 				
		End Set
	End Property

	<Category("Info")> _
	Public Property OnAirOrInProduction As Boolean
		Get
			Return Me._OnAir
		End Get
		Set (value As Boolean)
			me._OnAir = value
		End Set
	End Property

	<Category("Info")> _
	Public Property LastAirDate As Date		
		Get
			Return Me._LastAired
		End Get
		Set (value As Date)
			me._LastAired = value
		End Set
	End Property

	<Category("Info")> _
	Public Property FirstAirDate As Date		
		Get
			Return Me._FirstAired
		End Get
		Set (value As Date)
			me._FirstAired = value
		End Set
	End Property
	
	<Category("Info")> _
	Public Property NumOfEpisodes As Integer
		Get
			Return Me._Episodes
		End Get
		Set (value As Integer)
			me._Episodes = value
		End Set
	End Property
	
	<Category("Info")> _
	Public Property NumOfSeasons As Integer
		Get
			Return Me._Seasons
		End Get
		Set (value As Integer)
			me._Seasons = value
		End Set
	End Property
	
	<Category("Info")> _
	Public Property AverageRunTime As Integer
		Get
			Return Me._RunTime			
		End Get
		Set (value As Integer)
			me._RunTime = value
		End Set
	End Property
	
	<Category("Info")> _
	Public Property Music As String()
		Get
			Return Me._Music					
		End Get
		Set (value As String())			
			Me._Music = value 				
		End Set
	End Property
	
	<Category("Info")> _
	Public Property CreatedBy As String()
		Get
			Return Me._Creators					
		End Get
		Set (value As String())			
			Me._Creators = value 				
		End Set
	End Property
	
	<Category("Info")> _
	Public Property Studio As String()
		Get
			Return Me._Studio					
		End Get
		Set (value As String())			
			Me._Studio = value 				
		End Set
	End Property
	
	<Category("Info")> _
	Public Property Producers As String()
		Get
			Return Me._Producers					
		End Get
		Set (value As String())			
			Me._Producers = value 				
		End Set
	End Property
	
	<Category("Info")> _
	Public Property Director As String()
		Get
			Return Me._director				
		End Get
		Set (value As String())			
			Me._director = value 				
		End Set
	End Property
	
	<Category("Info")> _
	Public Property Starring As String()
		Get
			Return Me._starring				
		End Get
		Set (value As String())			
			Me._starring = value
		End Set
	End Property
	
	<Category("Info")> _
	Public Property OriginalChannels As String()
		Get
			Return Me._channels
		End Get
		Set (value As String())			
			Me._channels = value 				
		End Set
	End Property
	
	<Category("Info")> _
	Public Property OriginalLanguages As String()
		Get
			Return Me._languages
		End Get
		Set (value As String())			
			Me._languages = value 				
		End Set
	End Property
	
	<Category("Info")> _
	Public Property CountryOfOrigin As String()
		Get
			Return Me._countries				
		End Get
		Set (value As String())			
			Me._countries = value 				
		End Set
	End Property
	
	<Category("Description")> _
	Public Property About As String
		Get
			Return Me._About								
		End Get
		Set (value As String)
			Me._about = value 							
		End Set
	End Property
	
	<Category("Info")> _
	Public Property Genre As String()
		Get
			Return Me._Genre					
		End Get
		Set (value As String())			
			Me._Genre = value 				
		End Set
	End Property	
	
	<Category("Images")> _
	Public Property ImageURL As String
		Get
			Return Me._Image					
		End Get
		Set (value As String)			
			Me._Image = value 	
			If value Isnot Nothing Then
				'Dim ms as New IO.MemoryStream(me.__WebBrowser.DownloadData(Me._Image))
				'ms.Position = 0
				'Me._ImagePreview = image.FromStream(ms)
				'ms.Close()				
			Else
			 	'Me._ImagePreview = Nothing 			 	
			End If			
		End Set
	End Property
	
	Public Sub AutoSetExternalLink(Url As Uri, Optional Description As String = "")
		If url.Scheme.ToLower() <> "http" Then Exit Sub		
		Dim d2 as String = Description.ToLower().Trim()
		Dim I As Integer = d2.IndexOf("official website")	
		If i > -1 Then
			Me.WebSite = url
			Exit Sub			
		End If
		I = d2.IndexOf("official site")
		If i > -1 Then
			Me.WebSite = url
			Exit Sub			
		End If
		I = d2.IndexOf("official")
		If i > -1 Then
			If Me.WebSite Is Nothing Then				
				Me.WebSite = url
				Exit Sub							
			End If						
			If Me.WebSite.ToString() = "" Then
				Me.WebSite = url
				Exit Sub							
			End If			
		End If
		Dim host As String = Url.Host.ToLower()
		If host.Substring(0, 4) = "www." Then
			host = host.Substring(4)
		End If
		Select Case host
			Case "anidb.net"
				'MessageBox.Show(host)
				Me.AniDBProfileLink = url
			Case "animenewsnetwork.com"
				'MessageBox.Show(host)
				Me.AnimeNewsNetworkProfileLink = url
			Case "imdb.com"
				'MessageBox.Show(host)
				Me.IMDBProfileLink = url
			Case "tv.com"
				'MessageBox.Show(host)
				Me.TVcomProfileLink = url							
		End Select
	End Sub
	
	Public Sub AutoSetExternalLink(Url As String, Optional Description As String = "")		
		Try
			Me.AutoSetExternalLink(New Uri(url), description)
		Catch ex As Exception
		
		End Try		
	End Sub
	
	Public Function GetPropertiesNamesAndTypesDictonary() As Collections.Generic.Dictionary(Of String, System.Type)
		Dim rez As New Collections.Generic.Dictionary(Of String, System.Type)
		For Each Prop As PropertyInfo In Me.GetType().GetProperties()
			If Not prop.CanWrite Then Continue For			
			rez.Add(prop.Name, prop.PropertyType )
		Next		
		Return rez		
	End Function
	
	Public Function GetPropertiesNamesAndValuesDictonary() As Collections.Generic.Dictionary(Of String, Object)
		Dim rez As New Collections.Generic.Dictionary(Of String, Object)
		For Each Prop As PropertyInfo In Me.GetType().GetProperties()
			If Not prop.CanWrite Then Continue For	
			rez.Add(prop.Name, Prop.GetValue(Me, Nothing))
		Next		
		Return rez		
	End Function
	
	Public Sub Clear()			
		Me.ImageURL = ""		
		Me._Genre = Nothing
		Me._About = ""
		Me._Countries = Nothing 
		Me._Languages = Nothing 
		Me._Channels = Nothing 
		Me._Starring = Nothing 
		Me._Director = Nothing 
		Me._Producers = Nothing 
		Me._Studio = Nothing 
		Me._Creators = Nothing 
		Me._Music = Nothing 
		Me._RunTime = 0
		Me._Seasons = 1
		Me._Episodes = 0
		Me._FirstAired = Nothing		
		Me._LastAired = Date.Now()	
		Me._OnAir = False	
		Me._Nominations = Nothing 
		Me._Awards = Nothing 
		Me._WebSite = Nothing 
		Me._IMDBLink = Nothing 
		Me._TVLink = Nothing 		
		Me._WikipediaLink = Nothing
		Me._AniDBLink = Nothing 
		Me._ANNLink = Nothing 
		Me._OtherLinks =  New Collections.Generic.List(Of OtherLink)			
		Me._Youtube = Nothing
		Me._TrumbnailsURL = Nothing 
		Me._aka = Nothing
		Me._WhatIThink = Nothing 
		Me._OtherOpinions = New Collections.Generic.List(Of Opinion)	
		Me._IMDBrating = 0
		Me._TVrating = 0
		Me._ANNrating = 0
		Me._AniDBrating  =0
		Me._Writer = Nothing		
		Me._Preceded_by = Nothing
		Me._Followed_by = Nothing
		Me._Related = Nothing		
	End Sub
		
End Class