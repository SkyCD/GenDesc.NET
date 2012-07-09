

Namespace Import
	
	Public Class FromTVcomURL
		Inherits Import.Base		
		
		Public Overrides Sub AddAll() 								
			Me.AddTodo_GetRating()	
		End Sub
		
		Public Sub AddTodo_GetRating()
			Me.Actions.Add(AddressOf Me.wkTVRatings)
		End Sub
				
		Private Sub wkTVRatings()
			Dim wb As New WebBrowserContentDonwloader()	
			If Me.objDataItem.TVcomProfileLink IsNot Nothing Then						
				If Me.objDataItem.TVcomProfileLink.ToString().Trim().Length > 0 Then	
					Me.Actions.Text = "Getting TV.com rating..."
					Dim TVcomRating As WebBrowserContentDonwloader.TVcomRating = wb.GetTVcomRating( WebBrowserContentDonwloader.ExtractIDFromTVcomLink(Me.objDataItem.TVcomProfileLink) )	
					Me.objDataItem.TVcomRating = TVcomRating.Rating
				End If		
			End If
		End Sub
		
	End Class

End Namespace