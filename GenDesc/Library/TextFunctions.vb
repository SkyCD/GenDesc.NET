Namespace Library.Text

	Public Class TextFunctions
		
		Public Shared Function NCInstr(ByVal Text As String, ByVal What As String, Optional From As Integer = 0) As String
			Return Text.ToLower().IndexOf(What.ToLower(), From)
		End Function
		
	End Class
	
End Namespace
