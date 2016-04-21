'
' Created by SharpDevelop.
' User: Administrator
' Date: 2009.03.19
' Time: 12:00
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Public Partial Class Config
	
	Private MainForm As  MainForm
	Private LocationsForCheck as New Collections.Generic.Queue(Of String)
	
	Public Sub New(ByRef MainForm As MainForm)		
		' The Me.InitializeComponent call is required for Windows Forms designer support.
		Me.InitializeComponent()
		
		'
		' TODO : Add constructor code after InitializeComponents
		'
		Me.textBox1.Text = my.Settings.FFMPEG
		If my.Settings.AutoSaveSheetWizardValues Then
			Me.checkBox1.Checked = True
		Else
			Me.checkBox1.Checked = False
		End If
		If my.Settings.CopyGeneratedDataToClipboard Then
			Me.checkBox2.Checked = True
		Else
			Me.checkBox2.Checked = False			
		End If
		If my.Settings.StarSheetFillWizardAgain Then
			Me.checkBox3.Checked = True
		Else
			Me.checkBox3.Checked = False			
		End If
		Me.MainForm = MainForm		
	End Sub

    Sub Button1Click(sender As Object, e As EventArgs) Handles button1.Click
        Dim Od As New OpenFileDialog
        Od.Title = "Select ffmpeg.exe..."
        Od.Filter = "ffmpeg.exe|ffmpeg*.exe"
        Od.CheckFileExists = True
        Od.CheckPathExists = True
        Od.FileName = Me.textBox1.Text
        If Od.ShowDialog() = DialogResult.OK Then
            Me.textBox1.Text = Od.FileName
        End If
    End Sub

    Sub CheckBox1CheckedChanged(sender As Object, e As EventArgs) Handles checkBox1.CheckedChanged
        Me.groupBox1.Enabled = Not Me.checkBox1.Checked
    End Sub


    Sub Button2Click(sender As Object, e As EventArgs) Handles button2.Click
        Me.Close()
    End Sub

    Sub Button3Click(sender As Object, e As EventArgs) Handles button3.Click
        Try
            My.Settings.AutoSaveSheetWizardValues = Me.checkBox1.Checked
            My.Settings.CopyGeneratedDataToClipboard = Me.checkBox2.Checked
            My.Settings.StarSheetFillWizardAgain = Me.checkBox3.Checked
            My.Settings.FFMPEG = Me.textBox1.Text
        Catch ex As Exception

        End Try
        Me.Close()
    End Sub

    Sub ConfigLoad(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub wkFoundTheSame()
        Me.MainForm.PBar.ToDo.Text = "Found."
    End Sub

    Private Sub wkScanDir()
        Dim Dir As String = Me.LocationsForCheck.Dequeue()
        If IO.File.Exists(Me.textBox1.Text) Then
            Me.MainForm.PBar.ToDo.Text = "Found."
            Exit Sub
        End If
        If IO.File.Exists(Dir + "\ffmpeg.exe") Then
            Me.MainForm.PBar.ToDo.Text = "Found."
            Me.textBox1.Text = Dir + "\ffmpeg.exe"
            Exit Sub
        End If
        Me.MainForm.PBar.ToDo.Text = String.Format("Scanning '{0}'...", Dir)
        Dim ddInfo As New IO.DirectoryInfo(Dir)
        Try
            For Each Dir2 As IO.DirectoryInfo In ddInfo.GetDirectories()
                Me.LocationsForCheck.Enqueue(Dir2.FullName)
                Application.DoEvents()
                Me.wkScanDir()
            Next
        Catch ex As Exception
            'namas
        End Try
    End Sub

    Sub Button4Click(sender As Object, e As EventArgs) Handles button4.Click
        Dim Found As Boolean = False
        If IO.File.Exists(Me.textBox1.Text) Then
            Dim fInfo As New IO.FileInfo(Me.textBox1.Text)
            If fInfo.Extension.ToLower() = "exe" Or fInfo.Extension.ToLower() = ".exe" Then
                Me.MainForm.PBar.ToDo.Add(New GenDesc.Expansible.Delegates.TodoAction(AddressOf Me.wkFoundTheSame))
                Found = True
            End If
        End If
        If Not Found Then

            Me.LocationsForCheck.Enqueue(Application.ExecutablePath)
            Me.MainForm.PBar.ToDo.Add(New GenDesc.Expansible.Delegates.TodoAction(AddressOf Me.wkScanDir))

            Me.LocationsForCheck.Enqueue(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData))
            Me.MainForm.PBar.ToDo.Add(New GenDesc.Expansible.Delegates.TodoAction(AddressOf Me.wkScanDir))

            Me.LocationsForCheck.Enqueue(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles))
            Me.MainForm.PBar.ToDo.Add(New GenDesc.Expansible.Delegates.TodoAction(AddressOf Me.wkScanDir))

            Me.LocationsForCheck.Enqueue(Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFiles))
            Me.MainForm.PBar.ToDo.Add(New GenDesc.Expansible.Delegates.TodoAction(AddressOf Me.wkScanDir))

            Me.LocationsForCheck.Enqueue(Environment.GetFolderPath(Environment.SpecialFolder.Desktop))
            Me.MainForm.PBar.ToDo.Add(New GenDesc.Expansible.Delegates.TodoAction(AddressOf Me.wkScanDir))

            Me.LocationsForCheck.Enqueue(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory))
            Me.MainForm.PBar.ToDo.Add(New GenDesc.Expansible.Delegates.TodoAction(AddressOf Me.wkScanDir))

            Me.LocationsForCheck.Enqueue(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData))
            Me.MainForm.PBar.ToDo.Add(New GenDesc.Expansible.Delegates.TodoAction(AddressOf Me.wkScanDir))

            Me.LocationsForCheck.Enqueue(Environment.GetFolderPath(Environment.SpecialFolder.Personal))
            Me.MainForm.PBar.ToDo.Add(New GenDesc.Expansible.Delegates.TodoAction(AddressOf Me.wkScanDir))

            Me.LocationsForCheck.Enqueue(Environment.GetFolderPath(Environment.SpecialFolder.System))
            Me.MainForm.PBar.ToDo.Add(New GenDesc.Expansible.Delegates.TodoAction(AddressOf Me.wkScanDir))

            Me.LocationsForCheck.Enqueue(Environment.GetFolderPath(Environment.SpecialFolder.Startup))
            Me.MainForm.PBar.ToDo.Add(New GenDesc.Expansible.Delegates.TodoAction(AddressOf Me.wkScanDir))

            '			For Each Drv As String In IO.Directory.GetLogicalDrives()
            '				Dim dInfo As New IO.DriveInfo(drv)
            '				If dInfo.DriveType = IO.DriveType.Unknown Or dInfo.DriveType = IO.DriveType.Network Or dInfo.DriveType = IO.DriveType.Removable  Or dInfo.DriveType = IO.DriveType.NoRootDirectory Then
            '					Continue For
            '				End If										
            '				Dim ddInfo As New IO.DirectoryInfo(drv + "\")
            '				Try
            '					For Each Dir2 As IO.DirectoryInfo In ddInfo.GetDirectories()
            '						Me.LocationsForCheck.Enqueue(dir2.FullName)
            '						Me.MainForm.PBar.ToDo.Add(New GenDesc.Expansible.Delegates.TodoAction(AddressOf Me.wkScanDir))
            '					Next					
            '				Catch ex As Exception
            '					'Do nothing
            '				End Try				
            '			Next
        End If
        Me.MainForm.PBar.ToDo.Start()
    End Sub


    Sub ConfigActivated(sender As Object, e As EventArgs) Handles MyBase.Activated
        If Me.MainForm.PBar.Visible Then
            Me.MainForm.PBar.Focus()
        End If
    End Sub
End Class
