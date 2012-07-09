Namespace Export

Public Class ImageShackUploader
	
    Structure ReturnedURLs
        Dim DirectLinkURL As String
        Dim ShowToFriendsURL As String
        Dim ThumbnailURL As String
        Dim Exitoso As Boolean
    End Structure
    
    Private Function GetReturnedURLsFromHTMLRta(ByVal HTML As String) As ReturnedURLs

        Dim RtaURLs As New ReturnedURLs
        RtaURLs.Exitoso = True
        
        Dim M1 As System.Text.RegularExpressions.Match = System.Text.RegularExpressions.Regex.Match(HTML, "Direct <a.+href=('|"")([^'|^""]+)('|"")>link</a> to image")
        If m1.Success Then
        	RtaURLs.DirectLinkURL = m1.Groups.Item(2).Value        	
        End If
        
        Dim M2 As System.Text.RegularExpressions.MatchCollection = System.Text.RegularExpressions.Regex.Matches(HTML, "\[url=([^\]]+)\]\[img=([^\]]+)\]\[/url\]")
        For Each m As System.Text.RegularExpressions.Match In M2
        	If m.Success Then
        		RtaURLs.ShowToFriendsURL = m.Groups.Item(1).Value
        		If m.Groups.Item(2).Value.IndexOf(".th.") > -1 Then
					RtaURLs.ThumbnailURL = m.Groups.Item(2).Value
				Else
					RtaURLs.DirectLinkURL = m.Groups.Item(2).Value
        		End If        		
        	End If
        Next                

        Return RtaURLs

    End Function
    
    Public Function UploadFileToImageShack(ByVal FileName As String) As ReturnedURLs
        Dim OldValue As Boolean = System.Net.ServicePointManager.Expect100Continue

        Try

            System.Net.ServicePointManager.Expect100Continue = False
            '1. Cookie
            Dim Cookie As New Net.CookieContainer()

            '2. Arguments
            Dim QueryStringArguments As New Dictionary(Of String, String)
            QueryStringArguments.Add("MAX_FILE_SIZE", "3145728")
            QueryStringArguments.Add("refer", "")
            QueryStringArguments.Add("brand", "")
            QueryStringArguments.Add("optimage", "1")
            QueryStringArguments.Add("rembar", "1")
            QueryStringArguments.Add("submit", "host it!")
            QueryStringArguments.Add("optsize", "resample")

            '3. contentType 
            Dim ContentType As String = ""
            Select Case IO.Path.GetExtension(FileName).ToLower
                Case ".jpg"
                    ContentType = "image/jpeg"
                Case ".jpeg"
                    ContentType = "image/jpeg"
                Case ".gif"
                    ContentType = "image/gif"
                Case ".png"
                    ContentType = "image/png"
                Case ".bmp"
                    ContentType = "image/bmp"
                Case ".tif"
                    ContentType = "image/tiff"
                Case ".tiff"
                    ContentType = "image/tiff"
                Case Else
                    ContentType = "image/unknown"
            End Select

            '4. Upload and return Rta
            Dim rta As String = UploadFileEx(FileName, "http://www.imageshack.us/index.php", "fileupload", ContentType, QueryStringArguments, Cookie)            
            Return GetReturnedURLsFromHTMLRta(rta)
        Catch ex As Exception
            Dim Rta As New ReturnedURLs
            Rta.Exitoso = False
            Return Rta
        Finally
            System.Net.ServicePointManager.Expect100Continue = OldValue
        End Try
    End Function
    
    Private Function UploadFileEx(ByVal FileName As String, ByVal URL As String, ByVal FileFormName As String, ByVal ContentType As String, ByVal QueryStringArguments As Dictionary(Of String, String), ByVal Cookies As Net.CookieContainer) As String

        If FileFormName = "" Then FileFormName = "file"
        If ContentType = "" Then ContentType = "application/octet-stream"

        Dim PostData As String = "?"
        If QueryStringArguments IsNot Nothing Then
            For Each kvp As KeyValuePair(Of String, String) In QueryStringArguments
                PostData &= kvp.Key & "=" & kvp.Value & "&"
            Next
        End If


        Dim URI As New Uri(URL + PostData)
        Dim Boundary As String = "----------" + DateTime.Now.Ticks.ToString("x")
        Dim WReq As Net.HttpWebRequest = DirectCast(Net.WebRequest.Create(URI), Net.HttpWebRequest)
        WReq.CookieContainer = Cookies
        WReq.ContentType = "multipart/form-data; boundary=" + Boundary
        WReq.Method = "POST"

        Dim PostHeader As String = String.Format("--" & Boundary & "{0}" & _
        "Content-Disposition: form-data; name=""" & FileFormName _
        & """; filename=""" & IO.Path.GetFileName(FileName) & """{0}" _
        & "Content-Type: " & ContentType & "{0}{0}", vbNewLine)

        Dim PostHeaderBytes As Byte() = System.Text.Encoding.UTF8.GetBytes(PostHeader)
        Dim BoundaryBytes As Byte() = System.Text.Encoding.ASCII.GetBytes(vbNewLine & "--" + Boundary + vbNewLine)

        Dim FileStream As New IO.FileStream(FileName, IO.FileMode.Open, IO.FileAccess.Read)

        WReq.ContentLength = PostHeaderBytes.Length + FileStream.Length + BoundaryBytes.Length

        Dim RequestStream As IO.Stream = WReq.GetRequestStream()
        RequestStream.Write(PostHeaderBytes, 0, PostHeaderBytes.Length)
        Dim buffer As Byte() = New Byte(CInt(Math.Min(4096, CInt(FileStream.Length))) - 1) {}
        Dim bytesRead As Integer = 0
        Do
            bytesRead = FileStream.Read(buffer, 0, buffer.Length)
            If bytesRead = 0 Then Exit Do
            RequestStream.Write(buffer, 0, bytesRead)
            Application.DoEvents()
        Loop
        RequestStream.Write(BoundaryBytes, 0, BoundaryBytes.Length)

        Dim Rta As Net.WebResponse = WReq.GetResponse()
        Dim s As IO.Stream = Rta.GetResponseStream()
        Dim sr As New IO.StreamReader(s)

        Return sr.ReadToEnd()
    End Function
    
End Class

End Namespace