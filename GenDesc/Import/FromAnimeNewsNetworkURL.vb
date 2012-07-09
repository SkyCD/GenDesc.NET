Namespace Import
	
	Public Class FromAnimeNewsNetworkURL
		Inherits Import.Base		
		
		Public Overrides Sub AddAll() 								
			Me.AddTodo_GetRating()
		End Sub
		
		Public Sub AddTodo_GetRating()
			Me.Actions.Add(AddressOf Me.wkAnimeNewsNetworkRatings)
		End Sub
		
		Private Sub wkAnimeNewsNetworkRatings()
			Dim wb As New WebBrowserContentDonwloader()	
			If Me.objDataItem.AnimeNewsNetworkProfileLink IsNot Nothing Then						
				If Me.objDataItem.AnimeNewsNetworkProfileLink.ToString().Trim().Length > 0 Then	
					Me.Actions.Text = "Getting AnimeNewsNetwork rating..."
					Dim AnimeNewsNetworkRating as WebBrowserContentDonwloader.AnimeNewsNetworkRating = wb.GetAnimeNewsNetworkRating( WebBrowserContentDonwloader.ExtractIDFromAnimeNewsNetworkLink(Me.objDataItem.AnimeNewsNetworkProfileLink) )
					Me.objDataItem.AnimeNewsNetworkRating = AnimeNewsNetworkRating.WeightedMean			
				End If
			End If				
		End Sub	
		
	End Class

End Namespace