<%@ Include File="system.inc" %>
<% if (IsSet(ImageURL)) { 
	%>[img]<%=ImageURL%>[/img]<% nl(); nl();
   } 
if (IsSet(aka)) {
    %>[b]aka:[/b] <% tWriteLine(aka);
}
if (IsSet(Genre)) {
    %>[b]Žanras:[/b] <% tWriteLineTranslated(Genre, "lt");
}
if (IsSet(CountryOfOrigin)) {
    %>[b]Kilmės šalis:[/b] <% tWriteLineTranslated(CountryOfOrigin, "lt", false);
}
if (IsSet(OriginalLanguages)) {
    %>[b]Originali kalba:[/b] <% tWriteLineTranslated(OriginalLanguages, "lt");
}
if (IsSet(Studio)) {
    %>[b]Studija:[/b] <% tWriteLine(Studio);
}
if (IsSet(CreatedBy)) {
    %>[b]Sukūrė:[/b] <% tWriteLine(CreatedBy);
}
if (IsSet(Director)) {
    %>[b]Režisierius(-iai):[/b] <% tWriteLine(Director);
}
if (IsSet(WritedBy)) {
    %>[b]Scenaristas(-ai):[/b] <% tWriteLine(WritedBy);
}
if (IsSet(Starring)) {
    %>[b]Vaidina:[/b] <% tWriteLine(Starring);
}
if (IsSet(Music)) {
    %>[b]Muzika:[/b] <% tWriteLine(Music);
}
if (IsSet(Producers)) {
    %>[b]Prodiuseris(-iai):[/b] <% tWriteLine(Producers);
}
if (IsSet(NumOfSeasons)) {
	%>[b]Sezonų skaičius<% if (OnAirOrInProduction) { %>(šiuo metu žinomas)<% } %>:[/b] <%=NumOfSeasons%> <% nl();
}
if (IsSet(NumOfEpisodes)) {
	%>[b]Epizodų skaičius<% if (OnAirOrInProduction) { %>(šiuo metu žinomas)<% } %>:[/b] <%=NumOfEpisodes%> <% nl();
}
if (IsSet(AverageRunTime)) {
	%>[b]Vidutinė serijos trukmė:[/b] <%=AverageRunTime%> min. <% nl();
}
if (IsSet(OriginalChannels)) {
    %>[b]Originalus rodymo kanalas(-ai):[/b] <% tWriteLine(OriginalChannels);
}
if (IsSet(FirstAirDate)) {
	%>[b]Originalus rodymo laikas:[/b] <% Response.Write(FirstAirDate.ToShortDateString()); %> - <%
	if (!OnAirOrInProduction) {
		Response.Write(LastAirDate.ToShortDateString());
	} else {
		Response.Write("dabar");
	}	
	nl();
}
if (IsSet(Related) || IsSet(FollowedBy) || IsSet(PrecededBy)) {
	%>[b]Susiję:[/b] <%
	if (IsSet(PrecededBy)) {
    	tWrite(PrecededBy, "[url=http://www.linkomanija.net/browse.php?search=%%item%%]%%item%%[/url] (priešistorė)"); 
	}
	if (IsSet(FollowedBy)) {
		if (IsSet(PrecededBy)) {
			%>, <%
		}
    	tWrite(FollowedBy, "[url=http://www.linkomanija.net/browse.php?search=%%item%%]%%item%%[/url] (tęsinys)"); 
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
		%>[url=<%=IMDBProfileLink.ToString()%>]IMDB.com[/url]<% if (IMDBRating > 0) {%> (įvertinimas - <%=IMDBRating%>)<%}
		kbl = true;
	}
	if (IsSet(TVcomProfileLink)) {
		if (kbl) {
			%>, <%
		}
		%>[url=<%=TVcomProfileLink.ToString()%>]TV.com[/url]<% if (TVcomRating > 0) {%> (įvertinimas - <%=TVcomRating%>)<%}
		kbl = true;
	}
	if (IsSet(AnimeNewsNetworkProfileLink)) {
		if (kbl) {
			%>, <%
		}
		%>[url=<%=AnimeNewsNetworkProfileLink.ToString()%>]Anime News Network[/url]<% if (AnimeNewsNetworkRating > 0) {%> (įvertinimas - <%=AnimeNewsNetworkRating%>)<%}
		kbl = true;
	}
	if (IsSet(AniDBProfileLink)) {
		if (kbl) {
			%>, <%
		}
		%>[url=<%=AniDBProfileLink.ToString()%>]AniDB.net[/url]<% if (AniDBRating > 0) {%> (įvertinimas - <%=AniDBRating%>)<%}
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

[b]Apie[/b]
	
<%=About%> 
<% }
if (IsSet(WhatIThink)) {
%>

[b]Mano nuomonė[/b]
	
<%=WhatIThink%> 
<% }
if (IsSet(OtherOpinions)) {
%>

[b]Kitos nuomonės[/b]
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

[b]Kadrų pavyzdžiai[/b]

[img]<%=ThumbnailURL%>[/img]
<%
}
if (IsSet(YoutubeEmbededCodes)) {
%>

[b]Susijęs filmukas[/b]

<%
printYoutubeCodes(YoutubeEmbededCodes, "[youtube]%%url%%[/youtube]\r\n");
}
if (IsSet(Awards) || IsSet(Nominations)) {
%>

[b]Apdovanojimai ir nominacijos[/b]

<%
	if (IsSet(Awards)) {
		tWriteLineTranslated(Awards, "[li]%%translated%%[/li]", "lt");
	}
	if (IsSet(Nominations)) {
		tWriteLineTranslated(Nominations, "[li]%%translated%%[/li]", "lt");
	}
}
%>