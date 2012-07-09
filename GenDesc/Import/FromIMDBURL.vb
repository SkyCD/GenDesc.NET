

Namespace Import
	
	Public Class FromIMDBURL
		Inherits Import.Base		
		
		Public Overrides Sub AddAll() 								
			Me.AddTodo_GetRating()	
		End Sub
		
		Public Sub AddTodo_GetRating()
			Me.Actions.Add(AddressOf Me.wkIMDBcomRatings)
		End Sub
		
		Private Sub wkIMDBcomRatings()
			Dim wb As New WebBrowserContentDonwloader()	
			If Me.objDataItem.IMDBProfileLink IsNot Nothing Then						
				If Me.objDataItem.IMDBProfileLink.ToString().Trim().Length > 0 Then	
					Me.Actions.Text = "Getting IMDB rating..."
					Dim IMDBRating As WebBrowserContentDonwloader.IMDBRating = wb.GetIMDBRating( WebBrowserContentDonwloader.ExtractIDFromIMDBLink(Me.objDataItem.IMDBProfileLink) )				
					Me.objDataItem.IMDBRating = IMDBRating.WeightedAverage
				End If
			End If			
		End Sub
		
	End Class

End Namespace