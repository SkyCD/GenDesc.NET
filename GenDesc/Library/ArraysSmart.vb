Namespace Library.Arrays
	
	Public Class Smart
	
		Public Shared Function Merge(Of Type)(array1 As Type(), array2 As Type()) As Type()
			Dim nk As New Generic.List(Of Type)		
			If array1 Is Nothing Then
				Dim t1() As type = {}			
				array1 = t1.Clone()
			End If
			If array2 Is Nothing Then
				Dim t2() As type = {}			
				array1 = t2.Clone()
			End If
			For Each item As Type In array1
				nk.Add(item)
			Next
			For Each item As Type In array2			
				If Not nk.Contains(item) Then
					nk.Add(item)
				End If			
			Next
			Dim rez(nk.Count - 1) As Type 
			For I As Integer = 0 To nk.Count -1
				rez(i) = nk.Item(i)			
			Next
			Return rez		
		End Function
		
		Public Shared Function AddToArray(Of Type)(array1 As Type(), Item As Type) As Type()
			Dim k() As Type = {item}
			Return Merge(array1, k)		
		End Function
	
	End Class
	
End Namespace