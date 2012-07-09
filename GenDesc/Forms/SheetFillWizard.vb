Imports System.Runtime.InteropServices
Imports System.Drawing
Imports System.Drawing.Imaging

Public Partial Class SheetFillWizard	
	
	Public Sub New()
		' The Me.InitializeComponent call is required for Windows Forms designer support.
		Me.InitializeComponent()
		
		'
		' TODO : Add constructor code after InitializeComponents
		'
		Me.checkBox1.Checked = IIF(my.Settings.CopyGeneratedDataToClipboard, True, False)
		Me.checkBox2.Checked = IIF(my.Settings.StarSheetFillWizardAgain, True, False)
	End Sub
	
	Sub CheckBox1CheckedChanged(sender As Object, e As EventArgs)
		Me.checkBox2.Enabled = Me.checkBox1.Checked
	End Sub
	
	Sub Button1Click(sender As Object, e As EventArgs)
		Dim fo As New OpenFileDialog()
		fo.Filter = "All video files|*.avi;*.divx;*.flv;*.mpg;*.mpe;*.mpeg;*.mp4;*.wmv;*.qt;*.mkv;*.ogm|All Files|*.*"
		If fo.ShowDialog() = DialogResult.OK Then
			me.textBox5.Text = fo.FileName
		End If
	End Sub
	
	Public ReadOnly Shared Property Instance As SheetFillWizard		
		Get
			Static obj As SheetFillWizard  = New SheetFillWizard()			
			Return obj			
		End Get
	End Property
	
	Sub WizardControl1CancelButtonClick(sender As Object, e As EventArgs)
		Me.Hide()
		Me.MainForm.Show()
		Me.MainForm.Activate()		
	End Sub		
		
	Sub WizardControl1NextButtonClick(sender As Object, tArgs As WizardBase.GenericCancelEventArgs(Of WizardBase.WizardControl))
		'Me.wizardControl1.CurrentStepIndex	+= 1
	End Sub
		
	Sub WizardControl1BackButtonClick(sender As Object, e As System.ComponentModel.CancelEventArgs)
		'Me.wizardControl1.CurrentStepIndex	-= 1
	End Sub
		
	Sub SheetFillWizardLoad(sender As Object, e As EventArgs)
		
	End Sub
		
	Sub SheetFillWizardShown(sender As Object, e As EventArgs)
	
	End Sub
	
	Private Function IsNormalURL(Text As String) As Boolean
		If text.Trim().Length > 0 Then
			Try 
				Dim k As New Uri(text)
			Catch ex As Exception
				Return False				
			End Try
		Else
			Return False			
		End If
		Return True		
	End Function
	
	Private ReadOnly Property MainForm As MainForm
		Get
			Return My.Application.OpenForms.Item("MainForm")
		End Get
	End Property
	
	Private Sub wkSetWikiVar()
		Me.mainform.objDataItem.WikipediaProfileLink = New Uri(Me.textBox1.Text)		
	End Sub
	
	Private Sub wkSetAboutVar()
		Me.mainform.objDataItem.About = Me.textBox2.Text		
	End Sub
	
	Private Sub wkSetWhatIThinkVar()
		Me.mainform.objDataItem.WhatIThink = Me.textBox3.Text		
	End Sub
	
	Private Sub wkSetYoutubeVar()
		Me.mainform.objDataItem.YoutubeEmbededCodes = Me.textBox4.Text.Split(vbCrLf)		
	End Sub
	
	Private Sub wkSheetClear() 
		Me.mainform.objDataItem.Clear()
	End Sub
	
	Private ImageFileForUpload As String = ""
	
	Private Sub wkSetTrumbnailVar()
		If ImageFileForUpload.Trim().Length > 0 Then
			Me.MainForm.PBar.ToDo.Text = "Uploading image to ImageShack..."
			Dim ex As New Export.ImageShackUploader()
			Dim retURL As Export.ImageShackUploader.ReturnedURLs = ex.UploadFileToImageShack(ImageFileForUpload)
			If retURL.Exitoso Then
				Me.MainForm.objDataItem.ThumbnailURL = New Uri(retURL.DirectLinkURL)
			End If
			Try
				IO.File.Delete(ImageFileForUpload)
			Catch ex2 As Exception
			
			End Try
		End If
	End Sub	
	
	Private Sub wkGenTrumbnailVar()			
		Dim ffmpeg As New VideoTools.ffmpeg.Tools()
		Me.MainForm.PBar.ToDo.Text = "Extracting frames..."
		Me.ImageFileForUpload = ""		
		Dim rez() As System.Drawing.Image = ffmpeg.ExtractSomeFrames(Me.textBox5.Text, 16)		
		If rez Is Nothing Then Exit Sub				
		Me.MainForm.PBar.ToDo.Text = "Generating thumbnail image..."
		Dim bmp As New System.Drawing.Bitmap(640, 480)		
		Dim I As Integer = 0
		Dim mw As Double = bmp.Width / math.Sqrt(rez.Length)		
		Dim mh As Double = bmp.Height / math.Sqrt(rez.Length)
		Using gr As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(bmp)
			For Y As Double = 0 To (bmp.Height-1) Step mh
				For X As Double = 0 To (bmp.Width-1) Step mw				
					Dim lc As Double = mw / rez(i).Width					
					Dim lh As Integer = rez(i).Height * lc
					Dim ly As Integer = (mh - lh) / 2
					If ly < 0 Then
						lh = mh
					End If					
					gr.FillRectangle(New System.Drawing.SolidBrush(Color.Black), New Rectangle(x+2, y+2, mw-4, mh-4))
					gr.DrawImage(rez(i), New Rectangle(X, Y+ly, mw, lh))
					gr.DrawRectangle(New Pen(Color.Black, 1), New Rectangle(x+2, y+2, mw-4, mh-4))
					gr.DrawRectangle(New Pen(Color.White, 2), New Rectangle( x, y, mw, mh))					
					I += 1
				Next	
				application.DoEvents()
			Next
			Dim fnt As New System.Drawing.Font("Arial", 8, FontStyle.Bold)			
			Dim brs As New System.Drawing.SolidBrush(Color.FromArgb(255, 255, 255, 255))
			Dim brs2 As New System.Drawing.SolidBrush(Color.FromArgb(180, 0, 0, 0))
			Dim txt As String = String.Format("Created with {0} {1}", Application.ProductName, Application.ProductVersion.ToString())
			Dim stx As SizeF = gr.MeasureString(txt, fnt)
			gr.FillRectangle(brs2, bmp.Width - stx.Width - 14, bmp.Height - stx.Height - 12, stx.Width + 14, stx.Height + 12)
			gr.DrawString(txt, fnt, brs, New Point(bmp.Width - stx.Width - 7, bmp.Height - stx.Height - 5) )
			gr.Flush()
		End Using
		Dim tFileName As String = IO.Path.GetTempFileName()
		bmp.Save(tFileName, ImageFormat.Png)
		Me.ImageFileForUpload = tFileName + ".png"		
		IO.File.Move(tFileName, Me.ImageFileForUpload)		
	End Sub
	
	Private Sub wkCopyGeneratedDataToClipboard()		
		Clipboard.SetText(Me.MainForm.GeneratedContent)		
	End Sub	
	
	Private Sub wkShowAgain()		
		Me.Show()
		Me.Activate()		
		'Me.MainForm.Hide()
		Call Me.SheetFillWizardShown(Nothing, Nothing)
		Me.wizardControl1.CurrentStepIndex += 1
	End Sub	
		
	Sub WizardControl1FinishButtonClick(sender As Object, e As EventArgs)			
		With Me.MainForm.PBar.ToDo			
			.Add(New Expansible.Delegates.TodoAction(AddressOf Me.wkSheetClear))
			If Me.IsNormalURL(Me.textBox1.Text) Then
				.Add(New Expansible.Delegates.TodoAction(AddressOf Me.wkSetWikiVar))
				.AddAllActions(New Import.FromWikipediaURL())
				.AddAllActions(New Import.FromAniDBURL())
				.AddAllActions(New Import.FromAnimeNewsNetworkURL())
				.AddAllActions(New Import.FromIMDBURL())
				.AddAllActions(New Import.FromTVcomURL())
			End If
			If Me.textBox2.Text.Length > 0 Then
				.Add(New Expansible.Delegates.TodoAction(AddressOf Me.wkSetAboutVar))
			End If
			If Me.textBox3.Text.Length > 0 Then
				.Add(New Expansible.Delegates.TodoAction(AddressOf Me.wkSetWhatIThinkVar))
			End If
			If Me.textBox4.Text.Length > 0 Then
				.Add(New Expansible.Delegates.TodoAction(AddressOf Me.wkSetYoutubeVar))
			End If
			If Me.textBox5.Text.Length > 0 And IO.File.Exists(My.Settings.FFMPEG) Then				
				.Add(New Expansible.Delegates.TodoAction(AddressOf Me.wkGenTrumbnailVar))
				.Add(New Expansible.Delegates.TodoAction(AddressOf Me.wkSetTrumbnailVar))
			End If
			.Add(New Expansible.Delegates.TodoAction(AddressOf Me.MainForm.RegenerateContent))
			If Me.checkBox1.Checked Then
				.Add(New Expansible.Delegates.TodoAction(AddressOf Me.wkCopyGeneratedDataToClipboard))
				If Me.checkBox2.Checked Then					
					.Add(New Expansible.Delegates.TodoAction(AddressOf Me.wkShowAgain))					
				End If				
			End If
			If my.Settings.AutoSaveSheetWizardValues Then
				my.Settings.CopyGeneratedDataToClipboard = Me.checkBox1.Checked
				my.Settings.StarSheetFillWizardAgain = me.checkBox2.Checked
			End If
			
			'Me.MainForm.Show()
			Me.Hide()
			Me.MainForm.Activate()
			
			.Start()
			
		End With							
	End Sub
		
	Sub SheetFillWizardVisibleChanged(sender As Object, e As EventArgs)
		
	End Sub
	
	Public Overloads Sub Show()		
		'parent.Show()
		Me.Visible = True		
		Me.Left = Me.MainForm.Left + (Me.MainForm.Width - Me.Width) / 2
		Me.Left = Me.MainForm.Top + (Me.MainForm.Height - Me.Height) / 2
		If clipboard.GetText().Trim().StartsWith("http://en.wikipedia.org/wiki/") Then
			Me.textBox1.Text = clipboard.GetText().Trim()
		Else
			Me.textBox1.Text = ""
		End If
		Me.textBox2.Text = ""
		Me.textBox3.Text = ""
		Me.textBox4.Text = ""
		Me.textBox5.Text = ""
		Me.checkBox1.Checked = my.Settings.CopyGeneratedDataToClipboard
 		Me.checkBox2.Checked = my.Settings.StarSheetFillWizardAgain 		
 		Me.wizardControl1.CurrentStepIndex = 0
	End Sub
	
	Sub IsStartClick(sender As Object, e As EventArgs)
		
	End Sub
End Class
