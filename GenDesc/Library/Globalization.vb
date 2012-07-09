Imports System.Globalization

Namespace Library
	
	Public Class Globalization
	
		Public Shared Function GetCountry2LettersCode(Country As String) As String			
			Static cpl As New Collections.Generic.Dictionary(Of String, String)
			If cpl.Count < 1 Then			
				Dim cultures() As CultureInfo = CultureInfo.GetCultures(CultureTypes.AllCultures And Not CultureTypes.NeutralCultures)
				For Each culture As CultureInfo In cultures
					Dim Region As New RegionInfo(culture.LCID)				
					If Not cpl.ContainsKey(Region.EnglishName.ToLower()) Then					
						cpl.Add(region.EnglishName.ToLower(), culture.ThreeLetterWindowsLanguageName.Substring(0, 2).ToUpper())					
					End If				
				Next			
			End If
			country = Country.ToLower()
			If cpl.ContainsKey(country) Then
				Return cpl.Item(country)
			ElseIf country.StartsWith("respublic of") Then
				Return GetCountry2LettersCode(country.Substring("respublic of".Length))
			Else
				Return Nothing			
			End If		
		End Function
	
	End Class
	
End Namespace