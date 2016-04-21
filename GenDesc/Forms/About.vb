Public Partial Class About
	Public Sub New()
        ' The Me.InitializeComponent call is required for Windows Forms designer support.
        Me.InitializeComponent()
    End Sub

    Sub AboutLoad(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim sw As New IO.StringWriter()

        sw.WriteLine("<?xml version=""1.0"" encoding=""UTF-8""?>")
        sw.WriteLine("<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN""")
        sw.WriteLine("""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">")
        sw.WriteLine("<html xmlns=""http://www.w3.org/1999/xhtml"">")
        sw.WriteLine("<head>")
        sw.WriteLine("<title>About</title>")
        sw.WriteLine("<style type=""text/css"">")
        sw.WriteLine("  *, body, html { font-family: Arial; font-size: 12px; margin: 0; padding: 0; }")
        sw.WriteLine("	div.field{display: block; width: 121px; float: left; border-style: none; }")
        sw.WriteLine("  div.value{display: block; width: 180px; border-style: none; }")
        sw.WriteLine("  .line { display: block; width: 369px; }")
        'sw.WriteLine("  .main .line { margin-left: 10px; border-bottom-color: blue; border-bottom-style: dashed; border-bottom-width: 1px;}")
        sw.WriteLine("  .thanks .line, .license .line, .main .line { margin-left: 12px; border-bottom-color: blue; border-bottom-style: dashed; border-bottom-width: 1px;}")
        'sw.WriteLine("  .license .line { margin-left: 10px; border-bottom-color: green; border-bottom-style: dashed; border-bottom-width: 1px;}")
        sw.WriteLine("  .head { display: block; font-size: 18px; font-family: System; font-weight: bold; color: white; text-align: center; vertical-align: middle; }")
        'sw.WriteLine("  .head .main { background-color: blue; padding: 10px; }")
        'sw.WriteLine("  .head .thanks { background-color: red; padding: 10px; }")
        sw.WriteLine("  .head .license, .head .thanks, .head .main { font-size: 14px; font-family: Arial; background-color: green; padding: 7px; }")
        sw.WriteLine("</style>")
        sw.WriteLine("</head>")
        sw.WriteLine("<body>")

        'sw.WriteLine("<h2>About " + my.Application.Info.ProductName  + "</h2>")

        Me.WriteBlock(sw, "Application", "main")

        sw.WriteLine("<div class=""main"">")
        Me.WriteItemPart(sw, "Assembly Name", My.Application.Info.AssemblyName)
        Me.WriteItemLink(sw, "Creator", "https://github.com/MekDrop/GenDesc.NET", "MekDrop")
        Me.WriteItemPart(sw, "Copyright", My.Application.Info.Copyright)
        Me.WriteItemPart(sw, "Description", My.Application.Info.Description)
        Me.WriteItemPart(sw, "Version", My.Application.Info.Version.ToString())
        sw.WriteLine("</div>")

        Me.WriteBlock(sw, "Thanks!", "thanks")

        sw.WriteLine("<div class=""thanks"">")
        Me.WriteCopPart(sw, "Lucian Baciu", "http://studentclub.ro/lucians_weblog/archive/2007/03/18/retrieve-data-from-wikipedia-using-c.aspx", "Wikipedia data import technique")
        Me.WriteCopPart(sw, "Roy Osherove", "http://weblogs.asp.net/rosherove/archive/2003/05/13/6963.aspx", "StripHTML function")
        Me.WriteCopPart(sw, "Stefan Sarstedt", "http://www.stefansarstedt.com/", "TemplateMaschine class")
        Me.WriteCopPart(sw, "C26000-", "http://social.msdn.microsoft.com/Forums/en-US/netfxnetcom/thread/03efc98c-68e2-410c-bf25-d5facacbd920/", "ImageShackUploader class")
        Me.WriteCopPart(sw, "Manish Ranjan Kumar", "http://www.codeproject.com/KB/cs/WizardDemo.aspx", "WizardBase control")
        Me.WriteCopPart(sw, "WebHeadStart.org", "http://www.webheadstart.org", "HTML help")
        Me.WriteCopPart(sw, "W3Schools", "http://w3schools.com/", "HTML help")
        Me.WriteCopPart(sw, "azizatif", "http://jayrock.berlios.de/", "JayRock library for JSON data")
        Me.WriteCopPart(sw, "Kamal Patel", "http://www.KamalPatel.net", "CSharpToVBConverter(1.2)")
        Me.WriteCopPart(sw, "Arthur @ xsamplex", "http://www.hoogervorst.ca/arthur/?p=2009", "FFMPEG and .NET tutorial and some code")
        Me.WriteCopPart(sw, "FFMPEG Team", "http://www.ffmpeg.org/", "fantastic command line tool")
        Me.WriteCopPart(sw, "Tripp", "http://tripp.arrozcru.org/", "ffmpeg Win32 builds")
        Me.WriteCopPart(sw, "developerFusion", "http://www.developerfusion.com/tools/convert/csharp-to-vb/", "C#-VB.NET-C# online convertor")
        Me.WriteCopPart(sw, "SharpDevelop", "http://www.icsharpcode.net/OpenSource/SD/", "cool IDE (there this application was written)")
        Me.WriteCopPart(sw, "ImageShack", "http://www.imageshack.us", "cool free image hosting service")
        sw.WriteLine("</div>")

        Me.WriteBlock(sw, "License", "license")
        sw.WriteLine("<div class=""license"">")
        sw.WriteLine("<div class=""line"">")
        sw.WriteLine("This application uses <a href=""http://www.gnu.org/licenses/gpl-3.0.html"" target=""_blank"">GNU General Public License version 3 (GPLv3)</a> license.")
        sw.WriteLine("</div>")
        sw.WriteLine("</div>")

        sw.WriteLine("</body>")
        sw.WriteLine("</html>")

        Me.webBrowser1.DocumentText = sw.ToString()

    End Sub

    Private Sub WriteBlock(ByRef sw As  IO.StringWriter, Name As String, Type As String)
		sw.WriteLine("<div class=""head"">")
		sw.WriteLine("	<div class=""" + Type + """>")
		sw.WriteLine("	" + web.HttpUtility.HtmlEncode(Name))
		sw.WriteLine("	</div>")
		sw.WriteLine("</div>")
	End Sub
	
	Private Sub WriteCopPart(ByRef sw As  IO.StringWriter, Name As String, URL As String, ForWhat As String)		
		sw.WriteLine("<div class=""line"">")
		sw.WriteLine("  <div class=""field"">")
		sw.WriteLine("    <a href=""" + URL + """ target=""_blank"">" + web.HttpUtility.HtmlEncode(Name) + "</a>")
		sw.WriteLine("  </div>")
		sw.WriteLine("  <div class=""value"">")
		sw.WriteLine("    for " + web.HttpUtility.HtmlEncode(ForWhat))
		sw.WriteLine("  </div>")
		sw.WriteLine("</div>")
	End Sub
	
	Private Sub WriteItemLink(ByRef sw As IO.StringWriter, Name As String, URL as String, Value As String)
		sw.WriteLine("<div class=""line"">")
		sw.WriteLine("  <div class=""field"">")
		sw.WriteLine("    " + Name)
		sw.WriteLine("  </div>")
		sw.WriteLine("  <div class=""value"">")
		sw.WriteLine("    <a href=""" + URL + """ target=""_blank"">" + web.HttpUtility.HtmlEncode(Value) + "</a>")
		sw.WriteLine("  </div>")
		sw.WriteLine("</div>")
	End Sub
	
	Private Sub WriteItemPart(ByRef sw As  IO.StringWriter, Name As String, Value As String)
		sw.WriteLine("<div class=""line"">")
		sw.WriteLine("  <div class=""field"">")
		sw.WriteLine("    " + Name)
		sw.WriteLine("  </div>")
		sw.WriteLine("  <div class=""value"">")
		sw.WriteLine("    " + web.HttpUtility.HtmlEncode(Value))
		sw.WriteLine("  </div>")
		sw.WriteLine("</div>")
	End Sub
	
End Class
