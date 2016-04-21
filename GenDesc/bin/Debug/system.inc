<%@ Assembly Name="System.Windows.Forms" %>
<%@ Assembly Name="GenDesc.NET" %>
<%@ Import NameSpace="System.Windows.Forms" %>
<%@ Import NameSpace="GenDesc" %>
<script runat="template">
private bool IsSet(System.Object variable){
	if (variable == null) return false;
	if (variable.ToString() == "") return false;
	if (variable.GetType().FullName == "System.Collections.ArrayList") {
		System.Collections.ArrayList v2 = (System.Collections.ArrayList)variable;
		return (v2.Count > 0);
	}
	if (variable.GetType().FullName.StartsWith("System.Int")) {
		return (System.Convert.ToUInt64(variable) > 0);
	}
	return true;
}					
private void tWrite(System.Collections.ArrayList ToWrite, string ItemTemplate, string UseDelimer) {
    string Rez = "";
   	foreach (object Item in ToWrite) { 
   		Rez += UseDelimer + ItemTemplate.Replace("%%item%%", Item.ToString());
   	}
   	if (UseDelimer.Length > 0 && Rez.Length > UseDelimer.Length) {
   		Rez = Rez.Substring(UseDelimer.Length);
   	}
   	Response.Write(Rez);
}
private void tWrite(System.Collections.ArrayList ToWrite, string ItemTemplate) {
	tWrite(ToWrite, ItemTemplate, ", ");
}		
private void tWrite(System.Collections.ArrayList ToWrite) {
	tWrite(ToWrite, "%%item%%");
}			
private void nl() {
	Response.Write("\r\n");
}
private void tWriteLine(System.Collections.ArrayList ToWrite, string ItemTemplate, string UseDelimer) {
	tWrite(ToWrite, ItemTemplate, UseDelimer);
	nl();
}
private void tWriteLine(System.Collections.ArrayList ToWrite, string ItemTemplate) {
	tWrite(ToWrite, ItemTemplate);
	nl();
}
private void tWriteLine(System.Collections.ArrayList ToWrite) {
	tWrite(ToWrite);
	nl();
}
private string Translate(string Text, string fromLanguage, string toLanguage) {
	WebBrowserContentDonwloader wb = new WebBrowserContentDonwloader();
	return wb.Translate(Text, fromLanguage, toLanguage);
}
private string Translate(string Text, string toLanguage) {
	return Translate(Text, "en", toLanguage);
}
private void tWriteTranslated(System.Collections.ArrayList ToWrite, string ItemTemplate, string UseDelimer, string FromLanguage, string ToLanguage, bool MakeSmall) {
    string Rez = "";
    string r2 = "";
   	foreach (object Item in ToWrite) {    		
   		r2 = ItemTemplate.Replace("%%item%%", Item.ToString()).Replace("%%translated%%", Translate(Item.ToString(), FromLanguage, ToLanguage));
   		if (MakeSmall) {
   			r2 = r2.ToLower();
   		}
   		Rez += UseDelimer + r2;
   	}
   	if (UseDelimer.Length > 0 && Rez.Length > UseDelimer.Length) {
   		Rez = Rez.Substring(UseDelimer.Length);
   	}
   	Response.Write(Rez);
}
private void tWriteTranslated(System.Collections.ArrayList ToWrite, string ItemTemplate, string UseDelimer, string ToLanguage) {
	tWriteTranslated(ToWrite, ItemTemplate, UseDelimer, "en", ToLanguage, true);
}
private void tWriteTranslated(System.Collections.ArrayList ToWrite, string ItemTemplate, string ToLanguage) {
	tWriteTranslated(ToWrite, ItemTemplate, ", ", "en", ToLanguage, true);
}
private void tWriteTranslated(System.Collections.ArrayList ToWrite, string ToLanguage) {
	tWriteTranslated(ToWrite, "%%translated%%", ", ", "en", ToLanguage, true);
}
private void tWriteLineTranslated(System.Collections.ArrayList ToWrite, string ToLanguage) {
	tWriteTranslated(ToWrite, ToLanguage);
	nl();
}
private void tWriteTranslated(System.Collections.ArrayList ToWrite, string ToLanguage, bool MakeSmall) {
	tWriteTranslated(ToWrite, "%%translated%%", ", ", "en", ToLanguage, MakeSmall);
}
private void tWriteLineTranslated(System.Collections.ArrayList ToWrite, string ToLanguage, bool MakeSmall) {
	tWriteTranslated(ToWrite, ToLanguage, MakeSmall);
	nl();
}
private void tWriteLineTranslated(System.Collections.ArrayList ToWrite, string ItemTemplate, string ToLanguage) {
	tWriteTranslated(ToWrite, ItemTemplate, ToLanguage);
	nl();
}
private void printYoutubeCodes(System.Collections.ArrayList code, string template) {
   string rez = "";
   foreach (System.Object item in code) {
       rez += item.ToString();
   }
   printYoutubeCodes( rez, template );
}
private void printYoutubeCodes(string code, string template) {   
   System.Collections.Generic.List<string> links = WebBrowserContentDonwloader.ExtractFromEmbededCodeMovies(code);
   foreach (string url in links) {
     Response.Write(template.Replace("%%url%%", url));
   }
}
private void printYoutubeCodes(string[] code, string template) {
   printYoutubeCodes( String.Join("\r\n", code), template );
}
</script>
<%@ Argument Name="Related" Type="System.Collections.ArrayList" %>
<%@ Argument Name="FollowedBy" Type="System.Collections.ArrayList" %>
<%@ Argument Name="PrecededBy" Type="System.Collections.ArrayList" %>
<%@ Argument Name="WritedBy" Type="System.Collections.ArrayList" %>
<%@ Argument Name="AniDBRating" Type="System.Double" %>
<%@ Argument Name="AnimeNewsNetworkRating" Type="System.Double" %>
<%@ Argument Name="TVcomRating" Type="System.Double" %>
<%@ Argument Name="IMDBRating" Type="System.Double" %>
<%@ Argument Name="OtherOpinions" Type="System.Collections.ArrayList" %>
<%@ Argument Name="WhatIThink" Type="System.String" %>
<%@ Argument Name="aka" Type="System.Collections.ArrayList" %>
<%@ Argument Name="ThumbnailURL" Type="System.String" %>
<%@ Argument Name="YoutubeEmbededCodes" Type="System.Collections.ArrayList" %>
<%@ Argument Name="OtherLinks" Type="System.Collections.ArrayList" %>
<%@ Argument Name="AnimeNewsNetworkProfileLink" Type="System.Uri" %>
<%@ Argument Name="AniDBProfileLink" Type="System.Uri" %>
<%@ Argument Name="WikipediaProfileLink" Type="System.Uri" %>
<%@ Argument Name="TVcomProfileLink" Type="System.Uri" %>
<%@ Argument Name="IMDBProfileLink" Type="System.Uri" %>
<%@ Argument Name="WebSite" Type="System.Uri" %>
<%@ Argument Name="Awards" Type="System.Collections.ArrayList" %>
<%@ Argument Name="Nominations" Type="System.Collections.ArrayList" %>
<%@ Argument Name="OnAirOrInProduction" Type="System.Boolean" %>
<%@ Argument Name="LastAirDate" Type="System.DateTime" %>
<%@ Argument Name="FirstAirDate" Type="System.DateTime" %>
<%@ Argument Name="NumOfEpisodes" Type="System.Int32" %>
<%@ Argument Name="NumOfSeasons" Type="System.Int32" %>
<%@ Argument Name="AverageRunTime" Type="System.Int32" %>
<%@ Argument Name="Music" Type="System.Collections.ArrayList" %>
<%@ Argument Name="CreatedBy" Type="System.Collections.ArrayList" %>
<%@ Argument Name="Studio" Type="System.Collections.ArrayList" %>
<%@ Argument Name="Producers" Type="System.Collections.ArrayList" %>
<%@ Argument Name="Director" Type="System.Collections.ArrayList" %>
<%@ Argument Name="Starring" Type="System.Collections.ArrayList" %>
<%@ Argument Name="OriginalChannels" Type="System.Collections.ArrayList" %>
<%@ Argument Name="OriginalLanguages" Type="System.Collections.ArrayList" %>
<%@ Argument Name="CountryOfOrigin" Type="System.Collections.ArrayList" %>
<%@ Argument Name="About" Type="System.String" %>
<%@ Argument Name="Genre" Type="System.Collections.ArrayList" %>
<%@ Argument Name="ImageURL" Type="System.String" %>
