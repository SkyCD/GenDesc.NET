' ---------------------------------------------------------------
'    TemplateMaschine - an open source template engine for C#
'
'    written by Stefan Sarstedt (http://www.stefansarstedt.com/)
'    Released under GNU Lesser General Public License (LGPL),
'    see file 'copying' for details about the license
'
'    History:
'        - initial release (version 0.5) on Oct 28th, 2004
'        - minor bugfixes (version 0.6) on March 29th, 2005
'        - updated to support referencing assemblies that are 
'          installed in the GAC (version 0.7) on January 12th, 2007
'          Thanks to William.Manning@ips-sendero.com
'        - Added support for generic arguments. 
'          Ability to pass dictionary of arguments relaxing ordered 
'          object[] requirement. Version 0.8 on March 18th, 2007
'          Thanks to vijay.santhanam@gmail.com
'   --------------------------------------------------------------- 

Imports System.CodeDom.Compiler
Imports System.IO
Imports System.Reflection
Imports System.Text
Imports System.Text.RegularExpressions
Imports Microsoft.CSharp


Namespace TemplateMaschine
    ''' <summary>
    ''' Template Compiler Exception
    ''' </summary>
    Public Class TemplateCompilerException
        Inherits Exception
        ''' <summary>
        ''' Template Compiler Exception 
        ''' </summary>
        ''' <param name="msg">Error message</param>
        Public Sub New(ByVal msg As String)
            MyBase.New(msg)
        End Sub
    End Class
    
    
    ''' <summary>
    ''' Template class, used to load and execute a template
    ''' TODO: default values, precompilation? 
    ''' typed arguments i.e. Dictionary<string,Type>?? host assembly would need to reference the same assemblies
    ''' TODO: publically exposed arguments for lookup? Argument from Assembly
    ''' </summary>
    Public Class Template
        Private generatorObject As Object = Nothing
        
        
        ''' <summary>
        ''' Load an embedded template from assembly
        ''' </summary>
        ''' <param name="embeddedTemplate">Name of template</param>
        ''' <param name="assembly">Assembly to load template from</param>
        Public Sub New(ByVal embeddedTemplate As String, ByVal assembly As Assembly)
            Me.ReadFileFromResources(embeddedTemplate, assembly)
            Me.ProcessTemplate(embeddedTemplate, Nothing)
        End Sub
        
        ''' <summary>
        ''' Load an embedded template from assembly
        ''' </summary>
        ''' <param name="embeddedTemplate">Name of template</param>
        ''' <param name="assembly">Assembly to load template from</param>
        ''' <param name="fileNameForDebugging">File to write generated template code to - for debugging purposes</param>
        Public Sub New(ByVal embeddedTemplate As String, ByVal assembly As Assembly, ByVal fileNameForDebugging As String)
            Me.ReadFileFromResources(embeddedTemplate, assembly)
            Me.ProcessTemplate(embeddedTemplate, fileNameForDebugging)
        End Sub
        
        ''' <summary>
        ''' Load a template from file
        ''' </summary>
        ''' <param name="templateFile">Template filename</param>
        Public Sub New(ByVal templateFile As String)
            Me.ReadFile(templateFile)
            Me.ProcessTemplate(templateFile, Nothing)
        End Sub
        
        ''' <summary>
        ''' Load a template from file
        ''' </summary>
        ''' <param name="templateFile">Template filename</param>
        ''' <param name="fileNameForDebugging">File to write generated template code to - for debugging purposes</param>
        Public Sub New(ByVal templateFile As String, ByVal fileNameForDebugging As String)
            Me.ReadFile(templateFile)
            Me.ProcessTemplate(templateFile, fileNameForDebugging)
        End Sub
        
        ''' <summary>
        ''' Load a template from file
        ''' </summary>
        ''' <param name="templateFile">Template filename</param>
        ''' <param name="TemplateSubData">aditional template data for sufixing</param>
        ''' <param name="fileNameForDebugging">File to write generated template code to - for debugging purposes</param>
        Public Sub New(ByVal templateFile As String, TemplateSubData As String(), Optional ByVal fileNameForDebugging As String = Nothing)        	
            
            Using ms As New MemoryStream()
            	Dim sw As New StreamWriter(ms)
            	Dim sr As New StreamReader(templateFile)
            	For Each item As String In TemplateSubData
					sw.Write( item )
            	Next 
            	sw.Write( sr.ReadToEnd() )
            	sr.Close()
            	ms.Position = 0
            	sr = New StreamReader(ms)                        
            
            	Me.ReadFile(sr)                                    
            	Me.ProcessTemplate(templateFile, fileNameForDebugging)
            	ms.Close()
            End Using            
            
        End Sub
        
        Private Enum Token
            LDirective
            RTag
            LTag
            LAssignment
            LScript
            RScript
            [String]
            Backslash
            Newline
            QuotationMark
            Eof
        End Enum
        Private Structure TokenInfo
            Public token As Token
            Public s As String
            
            Public Sub New(ByVal token As Token, ByVal s As String)
                Me.token = token
                Me.s = s
            End Sub
        End Structure
        
        Private linePos As Integer = 1, columnPos As Integer = 1
        Private resultString As New StringBuilder(500000)
        Private resultStringScript As New StringBuilder(500000)
        Private sourceStringOriginal As New StringBuilder(500000)
        Private sourceString As StringBuilder
        Private assemblies As New List(Of String)()
        Private usings As New List(Of String)()
        Private arguments As New Dictionary(Of String, String)()
        ' Key=name Value=type
        
        
        '
'		private struct Argument
'		{
'			public string name;
'			public string type;
'
'			public Argument(string name, string type)
'			{
'				this.name = name;
'				this.type = type;
'			}
'		}
'
        
        
        Private Function IndexOfStopToken() As Integer
            Dim i As Integer = 0
            While i < sourceString.Length
                If (sourceString(i) = "<"c) OrElse (sourceString(i) = "\"c) OrElse (sourceString(i) = "%"c) OrElse (sourceString(i) = ControlChars.Cr) OrElse (sourceString(i) = """"c) Then
                    Exit While
                End If
                i += 1
            End While
            'if (i == sourceString.Length)
            '	throw new ApplicationException("IndexOfStopToken");
            If i = 0 Then
                Return 1
            End If
            
            Return i
        End Function
        
        Private Function IndexOf(ByVal c As Char) As Integer
            Dim i As Integer = 0
            While i < sourceString.Length
                If sourceString(i) = c Then
                    Exit While
                End If
                i += 1
            End While
            
            Return i
        End Function
        
        Private Function NextToken() As TokenInfo
            Dim token__1 As Token = Token.Eof
            Dim tokenVal As String = Nothing
            Dim pos As Integer = 0
            
            If sourceString.Length = 0 Then
                Return New TokenInfo(Token.Eof, Nothing)
            End If
            
            On Error Resume Next            
            
            Select Case sourceString(0)
                Case "<"c
                    ' Comment?
                    If sourceString.ToString(0, 5).IndexOf("<!--") = 0 Then
                        ' skip comment
                        pos += sourceString.ToString(0, 100).IndexOf("-->") + 3
                        sourceString.Remove(0, pos)
                       Return NextToken()
                    End If
                    ' LDirective, LTag or LAssignment?
                    If sourceString(1) = "%"c Then
                        ' LDirective
                        If sourceString(2) = "@"c Then
                            pos += 3
                            token__1 = Token.LDirective
                            Exit Select
                        End If
                        ' LAssignment
                        If sourceString(2) = "="c Then
                            pos += 3
                            token__1 = Token.LAssignment
                            Exit Select
                        End If
                        ' LTag
                        pos += 2
                        token__1 = Token.LTag
                        Exit Select
                    End If
                    If sourceString.ToString(0, 7) = "<script" Then
                        pos += IndexOf(">"c) + 1
                        token__1 = Token.LScript
                        Exit Select
                    End If
                    If sourceString.ToString(0, 9) = "</script>" Then
                        pos += 9
                        token__1 = Token.RScript
                        Exit Select
                    End If
                    token__1 = Token.[String]
                    Dim stPos As Integer = IndexOfStopToken()
                    tokenVal = sourceString.ToString(pos, stPos)
                    pos += stPos
                Case "%"c
                    ' RTag?
                    If sourceString(1) = ">"c Then
                        pos += 2
                        token__1 = Token.RTag
                        Exit Select
                    End If
                    token__1 = Token.[String]
                    Dim stPos As Integer = IndexOfStopToken()
                    tokenVal = sourceString.ToString(pos, stPos)
                    pos += stPos
                Case """"c
                    token__1 = Token.QuotationMark
                    tokenVal = """"
                    pos += 1
                    Exit Select
                Case "\"c
                    token__1 = Token.Backslash
                    tokenVal = "\"
                    pos += 1
                    Exit Select
                Case ControlChars.Cr
                    ' Newline?
                    If sourceString(1) = ControlChars.Lf Then
                        pos += 2
                        linePos += 1
                        token__1 = Token.Newline
                        Exit Select
                    End If
                    token__1 = Token.[String]
                    Dim stPos As Integer = IndexOfStopToken()
                    tokenVal = sourceString.ToString(pos, stPos)
                    pos += stPos
                Case Else
                    token__1 = Token.[String]
                    Dim stPos As Integer = IndexOfStopToken()
                    tokenVal = sourceString.ToString(pos, stPos)
                    pos += stPos
                    Exit Select
            End Select
            
            If tokenVal Is Nothing Then
                tokenVal = sourceString.ToString(0, pos)
            End If
            If token__1 = Token.Newline Then
                columnPos = 1
            Else
                columnPos += pos
            End If
            sourceString.Remove(0, pos)
            
            Return New TokenInfo(token__1, tokenVal)
        End Function
        
        Private Sub ParseTemplateAssignment()
            Dim tokenInfo As TokenInfo
            While (InlineAssignHelper(tokenInfo, NextToken())).token <> Token.RTag
                If (tokenInfo.token <> Token.[String]) AndAlso (tokenInfo.token <> Token.QuotationMark) Then
                    Throw New ApplicationException("invalid syntax")
                End If
                resultString.Append(tokenInfo.s)
            End While
        End Sub
        
        Private Sub ParseTemplateTagBlock()
            Dim tokenInfo As TokenInfo
            
            While (InlineAssignHelper(tokenInfo, NextToken())).token <> Token.RTag
                Select Case tokenInfo.token
                    Case Token.LAssignment
                        resultString.Append("Response.Write(")
                        ParseTemplateAssignment()
                        resultString.Append(");" & vbCr & vbLf)
                        Exit Select
                    Case Token.[String], Token.QuotationMark, Token.Backslash
                        resultString.Append(tokenInfo.s)
                        Exit Select
                    Case Token.Newline
                        resultString.Append(vbCr & vbLf)
                        Exit Select
                    Case Else
                        Throw New ApplicationException("invalid syntax")
                End Select
            End While
        End Sub
        
        Private Sub ParseTemplate(ByVal templateFile As String)
            Dim isBlockOpen As Boolean = False
            
            Dim tokenInfo As TokenInfo, lastTokenInfo As New TokenInfo(Token.Eof, Nothing)
            While (InlineAssignHelper(tokenInfo, NextToken())).token <> Token.Eof
                Select Case tokenInfo.token
                    Case Token.LDirective
                        ' ignore directives
                        Dim s As String = ""
                        While (InlineAssignHelper(tokenInfo, NextToken())).token <> Token.RTag
                            s += tokenInfo.s
                        End While
                        s = s.Trim(New Char() {" "c})
                        
                        If s.StartsWith("Import") Then
                            Dim directiveRegex As New Regex("[ ]*Import[ ]*NameSpace=""(?<namespace>[a-zA-Z0-9._]+)""", RegexOptions.IgnoreCase)
                            Dim match As Match = directiveRegex.Match(s)
                            If match Is Nothing Then
                                Throw New ApplicationException("invalid syntax in 'Import' expression")
                            End If
                            Dim namspaceToImport As String = match.Groups("namespace").Value
                            usings.Add(namspaceToImport)
                        ElseIf s.StartsWith("Assembly") Then
                            Dim directiveRegex As New Regex("[ ]*Assembly[ ]*Name=""(?<assemblyName>[a-zA-Z0-9._]+)""", RegexOptions.IgnoreCase)
                            Dim match As Match = directiveRegex.Match(s)
                            If match Is Nothing Then
                                Throw New ApplicationException("invalid syntax in 'Assembly' expression")
                            End If
                            Dim assemblyToImport As String = match.Groups("assemblyName").Value
                            assemblies.Add(assemblyToImport)
                        ElseIf s.StartsWith("Argument") Then
                            Dim directiveRegex As New Regex("[ ]*Argument[ ]+Name=""(?<name>[a-zA-Z0-9_]+)""[ ]+Type=""(?<type>[\<>a-zA-Z0-9.\]\[]+)""", RegexOptions.IgnoreCase)
                            Dim match As Match = directiveRegex.Match(s)
                            Console.WriteLine(directiveRegex.Match("Argument=""strings"" Type=""List<string>"""))
                            If match Is Nothing Then
                                Throw New ApplicationException("invalid syntax in 'Argument' expression")
                            End If
                            Dim argumentName As String = match.Groups("name").Value
                            Dim argumentType As String = match.Groups("type").Value
                            
                            If Not arguments.ContainsKey(argumentName) Then
								arguments.Add(argumentName, argumentType)                            	
                            End If                                                                                   
                        Else
                            Throw New ApplicationException("invalid syntax in directive")
                        End If
                        Exit Select
                    Case Token.LAssignment
                        If isBlockOpen Then
                            resultString.Append("""+")
                            ParseTemplateAssignment()
                            resultString.Append("+""")
                        Else
                            resultString.Append("Response.Write(")
                            ParseTemplateAssignment()
                            resultString.Append(");" & vbCr & vbLf)
                        End If
                        Exit Select
                    Case Token.LScript
                        While (InlineAssignHelper(tokenInfo, NextToken())).token <> Token.RScript
                            resultStringScript.Append(tokenInfo.s)
                        End While
                        Exit Select
                    Case Token.LTag
                        If isBlockOpen Then
                            Dim i As Integer
                            For i = resultString.Length - 1 To 0 Step -1
                                If (resultString(i) <> " "c) AndAlso (resultString(i) <> ControlChars.Tab) Then
                                    Exit For
                                End If
                            Next
                            If (resultString(i) = """"c) AndAlso (resultString(i - 1) = "("c) Then
                                resultString.Remove(i - 15, resultString.Length - i + 15)
                            Else
                                resultString.Append(""");" & vbCr & vbLf)
                            End If
                            isBlockOpen = False
                        End If
                        ParseTemplateTagBlock()
                        Exit Select
                    Case Token.[String]
                        If Not isBlockOpen Then
                            resultString.Append("Response.Write(""")
                        End If
                        isBlockOpen = True
                        resultString.Append(tokenInfo.s)
                        Exit Select
                    Case Token.QuotationMark
                        If Not isBlockOpen Then
                            resultString.Append("Response.Write(""")
                        End If
                        isBlockOpen = True                        
                        resultString.Append("\""")
                        Exit Select
                    Case Token.Backslash
                        resultString.Append("\\")
                        Exit Select
                    Case Token.Newline
                        If isBlockOpen Then
                        	resultString.Append(""");Response.WriteLine();" + vbcrlf + "#line " + linePos.ToString() + " """ + templateFile + """" + vbcrlf)                       	
                            'resultString.Append(((""");Response.WriteLine();" & vbCr & vbLf & "#line ") + linePos.ToString() & " """) + templateFile & """" & vbCr & vbLf)
                        Else
                            If (lastTokenInfo.token = Token.Newline) OrElse (lastTokenInfo.token = Token.[String]) Then                            	
                                resultString.Append("Response.WriteLine();" & vbcrlf & "#line " + linePos.ToString() & " """ + templateFile & """" & vbCrlf)
                            End If
                        End If
                        isBlockOpen = False
                        Exit Select
                    Case Else
                        Exit Select
                End Select
                lastTokenInfo = tokenInfo
            End While
        End Sub
                       
        Private Sub ReadFile(ByVal templateFile As String)
        	If Not File.Exists(templateFile) Then
                Throw New FileNotFoundException("Template file '" & templateFile & "' could not be found.")
            End If
        	Dim sr As New StreamReader(templateFile)
        	Me.ReadFile(sr)        	
        End Sub
        
        Private Sub ReadFile(ByRef DataStream As StreamReader) 
            Using sr As StreamReader = DataStream 
                sourceString = New StringBuilder()
                
                ' process include directives
                Dim s As String
                While (InlineAssignHelper(s, sr.ReadLine())) IsNot Nothing
                    If s.IndexOf("Include") <> -1 Then
                        Dim directiveRegex As New Regex("<%@[ ]+Include[ ]+File=""(?<fileName>[a-zA-Z0-9. -\\]+)""[ ]*%>", RegexOptions.IgnoreCase)
                        Dim match As Match = directiveRegex.Match(s)
                        If match Is Nothing Then
                            Throw New ApplicationException("invalid syntax in 'Include' expression")
                        End If
                        Dim fileName As String = match.Groups("fileName").Value
                        Using sr_includeFile As New StreamReader(fileName)
                            sourceString.Insert(0, sr_includeFile.ReadToEnd())
                        End Using
                    Else
                        sourceString.Append(s & vbCr & vbLf)
                    End If
                End While
            End Using
            
            sourceStringOriginal.Append(sourceString.ToString())
        End Sub
        
        Private Sub ReadFileFromResources(ByVal templateFile As String, ByVal assembly As Assembly)
            Dim found As Boolean = False
            For Each s As String In assembly.GetManifestResourceNames()
                If s.ToLower().EndsWith(templateFile.ToLower()) Then
                    templateFile = s
                    found = True
                    Exit For
                End If
            Next
            
            If Not found Then
                Throw New FileNotFoundException("Template '" & templateFile & "' could not be found as an embedded resource in assembly.")
            End If
            
            Dim fileStream As Stream = assembly.GetManifestResourceStream(templateFile)
            Using sr As New StreamReader(fileStream)
                sourceString = New StringBuilder()
                ' process include directives
                Dim s As String
                While (InlineAssignHelper(s, sr.ReadLine())) IsNot Nothing
                    If s.IndexOf("Include") <> -1 Then
                        Dim directiveRegex As New Regex("<%@[ ]+Include[ ]+File=""(?<fileName>[a-zA-Z0-9. -\\]+)""[ ]*%>", RegexOptions.IgnoreCase)
                        Dim match As Match = directiveRegex.Match(s)
                        If match Is Nothing Then
                            Throw New ApplicationException("invalid syntax in 'Include' expression")
                        End If
                        Dim fileName As String = match.Groups("fileName").Value
                        Dim resourceNames As String() = assembly.GetManifestResourceNames()
                        Dim resourceName As String = Array.Find(Of String)(resourceNames, Function(ByVal fn As String) fn.EndsWith(fileName))

                        If resourceName Is Nothing Then
                            Throw New FileNotFoundException("Included file '" & fileName & "' not found in resource.")
                        End If
                        
                        Dim includefileStream As Stream = assembly.GetManifestResourceStream(resourceName)
                        Using sr_includeFile As New StreamReader(includefileStream)
                            sourceString.Insert(0, sr_includeFile.ReadToEnd())
                        End Using
                    Else
                        sourceString.Append(s & vbCr & vbLf)
                    End If
                End While
            End Using
            
            sourceStringOriginal.Append(sourceString.ToString())
        End Sub
        
        Private Function GetLine(ByVal resultString As StringBuilder, ByVal lineNo As Integer) As String
            Dim lineCount As Integer = 1
            Dim charPos As Integer = 0
            Dim line As String = ""
            While True
                If resultString(charPos) = ControlChars.Lf Then
                    lineCount += 1
                    If lineCount = lineNo Then
                        Dim endPos As Integer = charPos
                        While endPos < resultString.Length
                            endPos += 1
                            If resultString(endPos) = ControlChars.Lf Then
                                Exit While
                            End If
                        End While
                        line = resultString.ToString(charPos, endPos - charPos)
                    End If
                End If
                charPos += 1
                If charPos >= resultString.Length Then
                    Exit While
                End If
            End While
            
            Return line
        End Function
        
        Private Sub ProcessTemplate(ByVal templateFile As String, ByVal fileNameForDebugging As String)
            ParseTemplate(templateFile)
            
            Dim argNo As Integer = 0
            For Each arg As KeyValuePair(Of String, String) In arguments
                resultString.Insert(0, "this." & _
                					    arg.Key & _
                					    " = (" & _
                					    arg.Value.ToString() & _
                					    ")args[" & argNo.ToString() & "];" & vbCr & vbLf)
                argNo += 1
            Next
            resultString.Insert(0, "try {" & vbCr & vbLf)
            resultString.Insert(0, "public StringBuilder Generate(object[] args) {" & vbCr & vbLf)
            resultString.Insert(0, "public void SetOutput(string outputFile, bool outputToString) { Response.Init(outputFile,outputToString); }" & vbCr & vbLf)
            resultString.Insert(0, "public TempGeneratorClass() {}" & vbCr & vbLf)
            For Each argument As KeyValuePair(Of String, String) In arguments
                resultString.Insert(0, ("private " & argument.Value & " ") + argument.Key & ";" & vbCr & vbLf)
            Next
            resultString.Insert(0, resultStringScript)
            resultString.Insert(0, "class TempGeneratorClass {" & vbCr & vbLf)
            resultString.Insert(0, "}" & vbCr & vbLf)
            resultString.Insert(0, "public static void Init(string outputFile,bool outputToString) { if (outputFile != null) outputStream = new StreamWriter(outputFile); if (outputToString) outputString = new StringBuilder();}" & vbCr & vbLf)
            resultString.Insert(0, "public static void Cleanup() {if (outputStream != null) outputStream.Close();}" & vbCr & vbLf)
            resultString.Insert(0, "public static void Write(string s) {if (outputStream != null) outputStream.Write(s); if (outputString != null) outputString.Append(s);} public static void WriteLine(string s) {if (outputStream != null) outputStream.WriteLine(s); if (outputString != null) {outputString.Append(s);outputString.Append(""\r\n"");}} public static void WriteLine() {if (outputStream != null) outputStream.WriteLine(); if (outputString != null) outputString.Append(""\r\n"");}" & vbCr & vbLf)
            resultString.Insert(0, "public static void Flush() {if (outputStream != null) outputStream.Flush();}" & vbCr & vbLf)
            resultString.Insert(0, "public static StringBuilder Result {get { return outputString; }}" & vbCr & vbLf)
            resultString.Insert(0, "private static StreamWriter outputStream = null;" & vbCr & vbLf)
            resultString.Insert(0, "private static StringBuilder outputString  = null;" & vbCr & vbLf)
            resultString.Insert(0, "internal class Response {" & vbCr & vbLf)
            resultString.Insert(0, "namespace TempGenerator {" & vbCr & vbLf)
            For Each usingExpr As String In usings
                resultString.Insert(0, "using " & usingExpr & ";" & vbCr & vbLf)
            Next
            resultString.Insert(0, "using System.Diagnostics;" & vbCr & vbLf)
            resultString.Insert(0, "using System.Text;" & vbCr & vbLf)
            resultString.Insert(0, "using System.IO;" & vbCr & vbLf)
            resultString.Insert(0, "using System;" & vbCr & vbLf)
            resultString.Append("} catch(Exception ex) { Response.Flush(); throw ex; };" & vbCr & vbLf)
            resultString.Append("Response.Cleanup();" & vbCr & vbLf)
            resultString.Append("return Response.Result;" & vbCr & vbLf)
            resultString.Append("} } }" & vbCr & vbLf)
            
            If Not [String].IsNullOrEmpty(fileNameForDebugging) Then
                Dim debugFile As New StreamWriter(fileNameForDebugging)
                debugFile.Write(resultString.ToString())
                debugFile.Close()
            End If
            
            ' compile assembly in memory with a yet unknown name
            Dim codeProvider As New CSharpCodeProvider()
            Dim parameters As System.CodeDom.Compiler.CompilerParameters = New CompilerParameters()
            parameters.GenerateExecutable = False
            parameters.GenerateInMemory = True
            'parameters.OutputAssembly = "Generator.dll";
            parameters.ReferencedAssemblies.Add("System.dll")            
            For Each assembly As String In assemblies
                parameters.ReferencedAssemblies.Add(Me.ResolveAssemblyPath(assembly))
            Next            
            Dim results As CompilerResults = codeProvider.CompileAssemblyFromSource(parameters, resultString.ToString())
            If results.Errors.Count > 0 Then
                generatorObject = Nothing
                Dim errs As String = ""                
                For Each CompErr As CompilerError In results.Errors
                    errs += (((("Template: " & CompErr.FileName) + Environment.NewLine & "Line number: ") + CompErr.Line.ToString() + Environment.NewLine & "Error: ") + CompErr.ErrorNumber & " '") + CompErr.ErrorText & "'"
                    Dim line As String = GetLine(sourceStringOriginal, CompErr.Line)
                    Dim message As String = "Error compiling template: " + Environment.NewLine + errs + Environment.NewLine + "Line: '" + line.ToString() + "'"
                    MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Next               
            Else
                Dim generatorAssembly As Assembly = results.CompiledAssembly
                generatorObject = generatorAssembly.CreateInstance("TempGenerator.TempGeneratorClass", False, System.Reflection.BindingFlags.CreateInstance, Nothing, Nothing, Nothing, _
                	Nothing)
            End If                 
        End Sub                
        
        Private Function ResolveAssemblyPath(ByVal name As String) As String
            name = name.ToLower()
            
            For Each assembly As Assembly In AppDomain.CurrentDomain.GetAssemblies()
                If Me.IsDynamicAssembly(assembly) Then
                    Continue For
                End If
                
                If Path.GetFileNameWithoutExtension(assembly.Location).ToLower().Equals(name) Then
                    Return assembly.Location
                End If
            Next
            
            If (file.Exists(name & ".dll")) Then
            	Return Path.GetFullPath(name & ".dll")
			Else
				Return Path.GetFullPath(name & ".exe")
            End If                       
        End Function
        
        Private Function IsDynamicAssembly(ByVal assembly As Assembly) As Boolean
            Return assembly.ManifestModule.Name.StartsWith("<")
        End Function
        
        ''' <summary>
        ''' Creates an ordered argument array from a named Dictionary collection
        ''' </summary>
        ''' <param name="args">Dictionary of Template Arguments keyed by argument name</param>
        ''' <returns>Array of objects for arguments(in order as they are defined in template)</returns>
        Private Function CreateOrderedArgumentArray(ByVal args As Dictionary(Of String, Object)) As Object()
            
            Dim newargs As Object() = New Object(arguments.Count - 1) {}
            Dim n As Integer = 0
            
            For Each arg As KeyValuePair(Of String, String) In arguments
                
                If args.ContainsKey(arg.Key) Then
                    newargs(n) = args(arg.Key)
                Else
                    Throw New ArgumentException("Template Argument " & arg.Key & " was not specified")
                End If
                n += 1
            Next
            
            Return newargs
            
        End Function
        
        
        ''' <summary>
        ''' Processes a template
        ''' </summary>
        ''' <param name="args">Dictionary of Template Arguments keyed by argument name</param>
        ''' <returns>Result of processed template</returns>
        Public Function Generate(ByVal args As Dictionary(Of String, Object)) As String
            Return Generate(CreateOrderedArgumentArray(args), Nothing, True)
        End Function
        
        ''' <summary>
        ''' Processes a template
        ''' </summary>
        ''' <param name="args">Template Arguments (in order as they are defined in template)</param>
        ''' <param name="outputFile">File to write results to</param>
        ''' <param name="outputToString">Flag if output should be written to a string</param>
        ''' <returns>Result of processed template, if outputToString is set to true</returns>
        Public Function Generate(ByVal args As Object(), ByVal outputFile As String, ByVal outputToString As Boolean) As String
            If generatorObject Is Nothing Then
                ' Throw New ApplicationException("please load a template first")
                Return ""
            End If

            Try
                generatorObject.[GetType]().InvokeMember("SetOutput", BindingFlags.Instance Or BindingFlags.[Public] Or BindingFlags.InvokeMethod, Nothing, generatorObject, New Object() {outputFile, outputToString})
                Dim str As Object = generatorObject.[GetType]().InvokeMember("Generate", BindingFlags.Instance Or BindingFlags.[Public] Or BindingFlags.InvokeMethod, Nothing, generatorObject, New Object() {args})
                If str Is Nothing Then
                    Return Nothing
                End If
                
                Return DirectCast(str, StringBuilder).ToString()
            Catch ex As Exception
                '                if (ex is TargetInvocationException)
                '                    Console.WriteLine(ex.InnerException.ToString());
                '                else
                If TypeOf ex Is TargetInvocationException Then
                    Throw ex.InnerException
                End If
                Throw ex
            End Try
        End Function
        
        ''' <summary>
        ''' Processes a template
        ''' </summary>
        ''' <param name="args">Template Arguments (in order as they are defined in template)</param>
        ''' <param name="outputFile">File to write results to</param>
        Public Sub Generate(ByVal args As Object(), ByVal outputFile As String)
            Generate(args, outputFile, False)
        End Sub
        
        ''' <summary>
        ''' Processes a template
        ''' </summary>
        ''' <param name="args">Template Arguments (in order as they are defined in template)</param>
        ''' <returns>Result of processed template</returns>
        Public Function Generate(ByVal args As Object()) As String
            Return Generate(args, Nothing, True)
        End Function
        Private Shared Function InlineAssignHelper(Of T)(ByRef target As T, ByVal value As T) As T
            target = value
            Return value
        End Function
    End Class
End Namespace