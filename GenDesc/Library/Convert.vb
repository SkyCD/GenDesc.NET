Namespace Library
	
	Public Class Convert
		
		Public Shared Function ArrayToArrayList(ByVal data As Object) As ArrayList					
			Dim n As New ArrayList()		
			If data Is Nothing Then Return n
			For Each item As Object In data			
				n.Add(item)
			Next
			Return n		
		End Function
		
	End Class
	
End Namespace