#pragma checksum "D:\GitHub\ptwcdshyd\PTWWebsite_New2020 old26th june\PTWWebsite2\Views\Services\GetServiceEngineeringcontent.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0ba767a230402ec1b3fb7ea76850b13818232566"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Services_GetServiceEngineeringcontent), @"mvc.1.0.view", @"/Views/Services/GetServiceEngineeringcontent.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Services/GetServiceEngineeringcontent.cshtml", typeof(AspNetCore.Views_Services_GetServiceEngineeringcontent))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "D:\GitHub\ptwcdshyd\PTWWebsite_New2020 old26th june\PTWWebsite2\Views\_ViewImports.cshtml"
using PTWWebsite2;

#line default
#line hidden
#line 2 "D:\GitHub\ptwcdshyd\PTWWebsite_New2020 old26th june\PTWWebsite2\Views\_ViewImports.cshtml"
using PTWWebsite2.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0ba767a230402ec1b3fb7ea76850b13818232566", @"/Views/Services/GetServiceEngineeringcontent.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e06c3cf507bbcfa1d594e9419908f30cddbc47f0", @"/Views/_ViewImports.cshtml")]
    public class Views_Services_GetServiceEngineeringcontent : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<PTW.DataAccess.Models.MasterPage>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "D:\GitHub\ptwcdshyd\PTWWebsite_New2020 old26th june\PTWWebsite2\Views\Services\GetServiceEngineeringcontent.cshtml"
  
    ViewData["Title"] = "GetServiceEngineeringcontent";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
            BeginContext(152, 263, true);
            WriteLiteral(@"<link rel=""stylesheet"" href=""/CSS/videobackground.css"" />
<link rel=""stylesheet"" href=""/CSS/animate.min.css"" />
<link rel=""stylesheet"" href=""/CSS/clean-blog.min.css"" />
<link rel=""stylesheet"" href=""/CSS/custom.css"" />
<h2>GetServiceEngineeringcontent</h2>

");
            EndContext();
            BeginContext(416, 27, false);
#line 12 "D:\GitHub\ptwcdshyd\PTWWebsite_New2020 old26th june\PTWWebsite2\Views\Services\GetServiceEngineeringcontent.cshtml"
Write(Html.Raw(Model.HtmlContent));

#line default
#line hidden
            EndContext();
            BeginContext(443, 1, true);
            WriteLiteral(";");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<PTW.DataAccess.Models.MasterPage> Html { get; private set; }
    }
}
#pragma warning restore 1591
