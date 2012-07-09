<%@ Include File="system.inc" %>
<% if (IsSet(ImageURL)) { 
	%>[img]<%=ImageURL%>[/img]<% nl(); nl();
   } 
if (IsSet(aka)) {
    %>[b]aka:[/b] <% tWriteLine(aka);
}
if (IsSet(Genre)) {
    %>[b]Genre:[/b] <% tWriteLine(Genre);
}
if (IsSet(CountryOfOrigin)) {
    %>[b]Country of origin:[/b] <% tWriteLine(CountryOfOrigin);
}
if (IsSet(OriginalLanguages)) {
    %>[b]Original language:[/b] <% tWriteLine(OriginalLanguages);
}
if (IsSet(Studio)) {
    %>[b]Studio:[/b] <% tWriteLine(Studio);
}
if (IsSet(CreatedBy)) {
    %>[b]Created by:[/b] <% tWriteLine(CreatedBy);
}
if (IsSet(Director)) {
    %>[b]Director:[/b] <% tWriteLine(Director);
}
if (IsSet(WritedBy)) {
    %>[b]Writed by:[/b] <% tWriteLine(WritedBy);
}
if (IsSet(Starring)) {
    %>[b]Starring:[/b] <% tWriteLine(Starring);
}
if (IsSet(Music)) {
    %>[b]Music:[/b] <% tWriteLine(Music);
}
if (IsSet(Producers)) {
    %>[b]Producers:[/b] <% tWriteLine(Producers);
}
if (IsSet(NumOfSeasons)) {
	%>[b]Number of seasons<% if (OnAirOrInProduction) { %>(as known now)<% } %>:[/b] <%=NumOfSeasons%> <% nl();
}
if (IsSet(NumOfEpisodes)) {
	%>[b]Number of episodes<% if (OnAirOrInProduction) { %>(as known now)<% } %>:[/b] <%=NumOfEpisodes%> <% nl();
}
if (IsSet(AverageRunTime)) {
	%>[b]Average running time:[/b] <%=AverageRunTime%> min. <% nl();
}
if (IsSet(OriginalChannels)) {
    %>[b]Original channel(s):[/b] <% tWriteLine(OriginalChannels);
}
if (IsSet(FirstAirDate)) {
	%>[b]Original running time:[/b] <% Response.Write(FirstAirDate.ToShortDateString()); %> - <%
	if (!OnAirOrInProduction) {
		Response.Write(LastAirDate.ToShortDateString());
	} else {
		Response.Write("now");
	}	
	nl();
}
if (IsSet(Related) || IsSet(FollowedBy) || IsSet(PrecededBy)) {
	%>[b]Related:[/b] <%
	if (IsSet(PrecededBy)) {
    	tWrite(PrecededBy, "[url=http://www.linkomanija.net/browse.php?search=%%item%%]%%item%%[/url] (preceded)"); 
	}
	if (IsSet(FollowedBy)) {
		if (IsSet(PrecededBy)) {
			%>, <%
		}
    	tWrite(FollowedBy, "[url=http://www.linkomanija.net/browse.php?search=%%item%%]%%item%%[/url] (followed)"); 
	}
	if (IsSet(Related)) {
		if (IsSet(PrecededBy) || IsSet(FollowedBy)) {
			%>, <%
		}
    	tWrite(Related, "[url=http://www.linkomanija.net/browse.php?search=%%item%%]%%item%%[/url]"); 
	}
	nl();
}
if (IsSet(WikipediaProfileLink) || IsSet(AnimeNewsNetworkProfileLink) || IsSet(AniDBProfileLink) || IsSet(TVcomProfileLink) || IsSet(IMDBProfileLink) || IsSet(WebSite) || IsSet(OtherLinks) ) {
	bool kbl = false;
	%>[b]Info:[/b] <% 
	if (IsSet(WikipediaProfileLink)) {
		%>[url=<%=WikipediaProfileLink.ToString()%>]Wikipedia[/url]<%
		kbl = true;
	}
	if (IsSet(WebSite)) {
		if (kbl) {
			%>, <%
		}
		%>[url=<%=WebSite.ToString()%>]Oficialus tinklalapis[/url]<%
		kbl = true;
	}
	if (IsSet(IMDBProfileLink)) {
		if (kbl) {
			%>, <%
		}
		%>[url=<%=IMDBProfileLink.ToString()%>]IMDB.com[/url]<% if (IMDBRating > 0) {%> (rating - <%=IMDBRating%>)<%}
		kbl = true;
	}
	if (IsSet(TVcomProfileLink)) {
		if (kbl) {
			%>, <%
		}
		%>[url=<%=TVcomProfileLink.ToString()%>]TV.com[/url]<% if (TVcomRating > 0) {%> (rating - <%=TVcomRating%>)<%}
		kbl = true;
	}
	if (IsSet(AnimeNewsNetworkProfileLink)) {
		if (kbl) {
			%>, <%
		}
		%>[url=<%=AnimeNewsNetworkProfileLink.ToString()%>]Anime News Network[/url]<% if (AnimeNewsNetworkRating > 0) {%> (rating - <%=AnimeNewsNetworkRating%>)<%}
		kbl = true;
	}
	if (IsSet(AniDBProfileLink)) {
		if (kbl) {
			%>, <%
		}
		%>[url=<%=AniDBProfileLink.ToString()%>]AniDB.net[/url]<% if (AniDBRating > 0) {%> (rating - <%=AniDBRating%>)<%}
		kbl = true;
	}
	nl();
	if (IsSet(OtherLinks)) {
		for(int i=0; i<OtherLinks.Count; i++) {
			if (kbl) {
				%>, <%
			}
			Response.Write("[url=");
			Response.Write(((GenDesc.ObjDataItem.OtherLink)OtherLinks[i]).URL.ToString());
			Response.Write("]");
			Response.Write(((GenDesc.ObjDataItem.OtherLink)OtherLinks[i]).Name);
			Response.Write("[/url]");
			kbl = true;
		}
	}
}
if (IsSet(About)) {
%>

[b]About[/b]
	
<%=About%> 
<% }
if (IsSet(WhatIThink)) {
%>

[b]What I think[/b]
	
<%=WhatIThink%> 
<% }
if (IsSet(OtherOpinions)) {
%>

[b]Others opinions[/b]
<%for(int i=0; i<OtherOpinions.Count; i++) {			
		Response.Write("[cite=");
		Response.Write(((GenDesc.ObjDataItem.Opinion)OtherOpinions[i]).Who.ToString());
		Response.Write(" @ ");
		Response.Write(((GenDesc.ObjDataItem.Opinion)OtherOpinions[i]).SiteName.ToString());
		Response.Write("]");			
		Response.Write( String.Join("\r\n", ((GenDesc.ObjDataItem.Opinion)OtherOpinions[i]).Opinion) );
		Response.Write("[/cite]");			
   }
   nl();
}
if (IsSet(ThumbnailURL)) {
%>

[b]Thumbnails[/b]

[img]<%=ThumbnailURL%>[/img]
<%
}
if (IsSet(YoutubeEmbededCodes)) {
%>

[b]Related movie[/b]

<%
printYoutubeCodes(YoutubeEmbededCodes, "[youtube]%%url%%[/youtube]\r\n");
}
if (IsSet(Awards) || IsSet(Nominations)) {
%>

[b]Awards and nominations[/b]

<%
	if (IsSet(Awards)) {
		tWriteLine(Awards, "[li]%%item%%[/li]");
	}
	if (IsSet(Nominations)) {
		tWriteLine(Nominations, "[li]%%item%%[/li]");
	}
}
%>