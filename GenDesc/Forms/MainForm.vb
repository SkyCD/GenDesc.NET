'
' Created by SharpDevelop.
' User: Administrator
' Date: 2009.03.10
' Time: 14:22
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Globalization
Imports System.CodeDom
Imports System.CodeDom.Compiler

Public Partial Class MainForm		
	
	Public objDataItem As New ObjDataItem()
	
	Public WithEvents PBar As New GenDesc.ProgressBar(Me)			
	
	Public Sub New()
		' The Me.InitializeComponent call is required for Windows Forms designer support.
		Me.InitializeComponent()
		
		'
		' TODO : Add constructor code after InitializeComponents
		'
		Me.propertyGrid1.SelectedObject = Me.objDataItem
		Dim m As New GenDesc.VideoTools.ffmpeg.Tools()
		'Dim k As VideoTools.Info.Media = m.GetMediaInfo("H:\Batman - The Brave and the Bold - 14 - Mystery In Space! [A-T].avi")
		'Messagebox.Show(CType(k.Streams.Item(0).Item(0), VideoTools.Info.Stream.Video).Width)		
		
	End Sub
		
	Sub PBar_Finished() Handles PBar.Finish
		Me.propertyGrid1.Update()
		Me.propertyGrid1.Refresh()				
		Me.RegenerateContent()		
	End Sub
		
	Public Function ParseTemplate(FileName As String) As String 		
		Dim data As Collections.Generic.Dictionary(Of String, Object) = Me.objDataItem.GetPropertiesNamesAndValuesDictonary()
		Dim types As Collections.Generic.Dictionary(Of String, System.Type) = Me.objDataItem.GetPropertiesNamesAndTypesDictonary()
		Dim rez As String = ""
		Dim fname as String = "system.inc"
		Dim sr As New IO.StreamWriter(fname)		
		sr.WriteLine( IO.File.ReadAllText( IO.Directory.GetParent(Application.ExecutablePath).FullName + "\Scripts\TemplateThings.inc") )
		For Each pair As Collections.Generic.KeyValuePair(Of String, System.Type) In types
			If pair.Value.FullName.EndsWith("[]") Or pair.Value.FullName.StartsWith("System.Collections.Generic.List") Then								
				Dim al As ArrayList = Library.Convert.ArrayToArrayList(data.Item(pair.Key))
				data.Remove(pair.Key)
				data.Add(pair.Key, al.Clone())
				sr.WriteLine("<%@ Argument Name="""+ pair.Key +""" Type=""System.Collections.ArrayList"" %>")
			Else
				sr.WriteLine("<%@ Argument Name="""+ pair.Key +""" Type=""" + pair.Value.FullName + """ %>")
			End If			
		Next
		sr.Close()		
		Dim sce As New TemplateMaschine.Template(FileName)
		Return sce.Generate(data)
	End Function
	
	Private isLoading As Boolean = True

    Sub MainFormLoad(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim Path As String = IO.Directory.GetParent(Application.ExecutablePath).FullName + "\Templates"
        Dim files() As String = IO.Directory.GetFiles(Path, "*.tpl")
        Dim finfo As IO.FileInfo
        For Each item As String In files
            finfo = New IO.FileInfo(item)
            Dim tplName As String = finfo.Name.Substring(0, finfo.Name.Length - finfo.Extension.Length)
            Dim I As Integer = Me.tscbGeneratedTextLanguage.Items.Add(tplName)
            If tplName = My.Settings.SelectedTemplateEngine Then
                Me.tscbGeneratedTextLanguage.SelectedIndex = I
            End If
        Next
        If Me.tscbGeneratedTextLanguage.SelectedIndex < 0 Then
            Me.tscbGeneratedTextLanguage.SelectedIndex = 0
        End If
        isLoading = False
    End Sub

    Sub TscbGeneratedTextLanguageTextChanged(sender As Object, e As EventArgs) Handles tscbGeneratedTextLanguage.TextChanged
        Call TabControl1SelectedIndexChanged(sender, e)
    End Sub

    Sub ToolStripButton3Click(sender As Object, e As EventArgs) Handles toolStripButton3.Click
        Me.objDataItem.Clear()
        Me.propertyGrid1.Update()
        Me.propertyGrid1.Refresh()
    End Sub

    Public Sub RegenerateContent()
        Me.PBar.ToDo.Text = "Regenerating content..."
        Me.textBox1.Text = ""
        Me.textBox1.Text = ParseTemplate(IO.Directory.GetParent(Application.ExecutablePath).FullName + "\Templates\" + Me.tscbGeneratedTextLanguage.Text + ".tpl")
        My.Settings.SelectedTemplateEngine = Me.tscbGeneratedTextLanguage.Text
    End Sub

    Public ReadOnly Property GeneratedContent() As String
        Get
            Return Me.textBox1.Text
        End Get
    End Property

    Sub TabControl1SelectedIndexChanged(sender As Object, e As EventArgs) Handles tabControl1.SelectedIndexChanged
        If Me.tabControl1.SelectedIndex = 1 Then
            Me.PBar.ToDo.Add(New Expansible.Delegates.TodoAction(AddressOf Me.RegenerateContent))
            Me.PBar.ToDo.Start()
            'Me.RegenerateContent()
        End If
        Me.toolStripButton3.Enabled = (Me.tabControl1.SelectedIndex = 0)
    End Sub

    Sub ToolStripButton2Click(sender As Object, e As EventArgs) Handles toolStripButton2.Click
        If Me.objDataItem.WikipediaProfileLink Is Nothing Then
            MessageBox.Show("You must specify Wikipedia profile url first!")
            Exit Sub
        End If

        With Me.PBar.ToDo
            .AddAllActions(New Import.FromWikipediaURL())
            .AddAllActions(New Import.FromAniDBURL())
            .AddAllActions(New Import.FromAnimeNewsNetworkURL())
            .AddAllActions(New Import.FromIMDBURL())
            .AddAllActions(New Import.FromTVcomURL())
        End With

        Me.PBar.ToDo.Start()
    End Sub

    Sub ToolStripButton4Click(sender As Object, e As EventArgs) Handles toolStripButton4.Click
        If Not Me.tabControl1.SelectedIndex = 1 Then
            Me.tabControl1.SelectedIndex = 1
        End If
        Clipboard.SetText(Me.textBox1.Text)
    End Sub

    Sub ToolStripButton1Click(sender As Object, e As EventArgs) Handles toolStripButton1.Click
        SheetFillWizard.Instance.Show()
    End Sub

    Sub ToolStripButton5Click(sender As Object, e As EventArgs) Handles toolStripButton5.Click
        Dim ab As New About()
        ab.ShowDialog()
    End Sub

    Sub ToolStripButton6Click(sender As Object, e As EventArgs) Handles toolStripButton6.Click
        Dim cf As New Config(Me)
        cf.ShowDialog()
    End Sub

    Sub MainFormActivated(sender As Object, e As EventArgs) Handles MyBase.Activated
        If Me.PBar.Visible Then
            Me.PBar.Focus()
            Exit Sub
        End If
        If SheetFillWizard.Instance.Visible Then
            SheetFillWizard.Instance.Focus()
            Exit Sub
        End If
    End Sub

    Sub TscbGeneratedTextLanguageSelectedIndexChanged(sender As Object, e As EventArgs) Handles tscbGeneratedTextLanguage.SelectedIndexChanged
        If isLoading Then Exit Sub
        Me.PBar.ToDo.Add(New Expansible.Delegates.TodoAction(AddressOf Me.RegenerateContent))
        Me.PBar.ToDo.Start()
    End Sub

End Class
