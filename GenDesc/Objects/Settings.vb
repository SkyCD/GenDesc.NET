Imports Jayrock
Imports Jayrock.Json
Imports Jayrock.Json.Conversion
Imports System.IO

Namespace My
	<HideModuleName> _
	Friend Class MySettingsClass
		
		Private Shared Settings As Jayrock.Json.JsonObject = Nothing		
		Private Shared SettingsFile As String = My.Application.Info.DirectoryPath + "\settings.dat"		
		Private Shared WithEvents tmrSaveSettings As New Timers.Timer(231)				
		
		Private Property piv(PropertyName As String) As Object
			Get
				If Settings Is Nothing Then LoadSettings()
				If settings.Contains(PropertyName) Then
					Return settings.Item(PropertyName)
				Else
					settings.Put(PropertyName, Nothing)					
				End If
				Return Nothing				
			End Get
			Set (value as Object)
				If Settings Is Nothing Then LoadSettings()
				If settings.Contains(PropertyName) Then
					settings.Item(PropertyName) = value
				Else
					settings.Put(PropertyName, value)					
				End If					
				tmrSaveSettings.Stop()
				tmrSaveSettings.Start()				
			End Set
		End Property
		
		Private Shared Sub SaveSettings() Handles tmrSaveSettings.Elapsed						
			IO.File.WriteAllText(SettingsFile, Json.Conversion.JsonConvert.ExportToString(Settings) )
		End Sub
		
		Private Sub LoadSettings() 						
			Settings = New Jayrock.Json.JsonObject()
            If IO.File.Exists(SettingsFile) Then
                Dim content As String = IO.File.ReadAllText(SettingsFile)
                If content.Length > 0 Then
                    Dim tr As IO.TextReader = New IO.StringReader(content)
                    Dim jp As New Jayrock.Json.JsonTextReader(tr)
                    Settings.Import(jp)
                End If
            End If
        End Sub
		
		Public Property FFMPEG As String
			Get
				Return Me.piv("FFMPEG")
			End Get
			Set (Value As String)
				If Not IO.File.Exists(value) Then
					Throw New Exception("FFMPEG file not found!")	
					Exit Property					
				End If
				Me.piv("FFMPEG")	= value
			End Set
		End Property
		
		Public Property VLCPlayer As String
			Get
				Return Me.piv("VLCPlayer")				
			End Get
			Set (Value As String)
				If Not IO.File.Exists(value) Then					
					Throw New Exception("VLC player file not found!")	
					Exit Property					
				End If
				Me.piv("VLCPlayer")	= value
			End Set
		End Property
		
		Public Property AutoSaveSheetWizardValues As Boolean
			Get
				Return Me.piv("AutoSaveSheetWizardValues")
			End Get
			Set (value As Boolean)
				Me.piv("AutoSaveSheetWizardValues") = value
			End Set
		End Property
		
		Public Property CopyGeneratedDataToClipboard As Boolean
			Get
				Return Me.piv("CopyGeneratedDataToClipboard")
			End Get
			Set (value As Boolean)
				Me.piv("CopyGeneratedDataToClipboard") = value
			End Set
		End Property
		
		Public Property StarSheetFillWizardAgain As Boolean
			Get
				Return Me.piv("StarSheetFillWizardAgain")
			End Get
			Set (value As Boolean)
				Me.piv("StarSheetFillWizardAgain") = value
			End Set
		End Property
		
		Public Property SelectedTemplateEngine As String
			Get
				Return Me.piv("SelectedTemplateEngine")
			End Get
			Set (Value As String)				
				Me.piv("SelectedTemplateEngine")	= value
			End Set
		End Property
		
		
		
	End Class
	
	' Register extension in my namespace
	<HideModuleName> _
	Friend Module MySettingsModule
		Private instance As New MySettingsClass
		
		Public ReadOnly Property Settings() As MySettingsClass
			<DebuggerHidden> _
			Get
				Return instance
			End Get
		End Property
	End Module
End Namespace
