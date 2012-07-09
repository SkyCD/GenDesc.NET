Namespace Import
	
	Public Class FromAniDBURL
		Inherits Import.Base		
		
		Public Overrides Sub AddAll() 								
			Me.AddTodo_GetRating()			
		End Sub
		
		Public Sub AddTodo_GetRating()
			Me.Actions.Add(AddressOf Me.wkAniDBRatings)			
		End Sub
		
		Private Sub wkAniDBRatings()
			Dim wb As New WebBrowserContentDonwloader()		
			If Me.objDataItem.AniDBProfileLink IsNot Nothing Then						
				If Me.objDataItem.AniDBProfileLink.ToString().Trim().Length > 0 Then	
					Me.Actions.Text = "Getting AniDB.net rating..."					
					Dim AniDbRanks As WebBrowserContentDonwloader.AniDBRating = wb.GetAniDBRating( WebBrowserContentDonwloader.ExtractIDFromAniDBLink(Me.objDataItem.AniDBProfileLink) )		
					Me.objDataItem.AniDBRating = AniDbRanks.Permanent				
				End If
			End If		
		End Sub	
		
	End Class

End Namespace