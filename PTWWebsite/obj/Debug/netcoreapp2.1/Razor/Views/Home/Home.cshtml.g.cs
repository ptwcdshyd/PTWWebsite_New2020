#pragma checksum "C:\Users\Test\source\repos\PTWWebsite\PTWWebsite\Views\Home\Home.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "706a12d45c353e84f30cbb84e8615f2f9a5ab115"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Home), @"mvc.1.0.view", @"/Views/Home/Home.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/Home.cshtml", typeof(AspNetCore.Views_Home_Home))]
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
#line 1 "C:\Users\Test\source\repos\PTWWebsite\PTWWebsite\Views\_ViewImports.cshtml"
using PTWWebsite;

#line default
#line hidden
#line 2 "C:\Users\Test\source\repos\PTWWebsite\PTWWebsite\Views\_ViewImports.cshtml"
using PTWWebsite.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"706a12d45c353e84f30cbb84e8615f2f9a5ab115", @"/Views/Home/Home.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ee95bed98027de1563abf2b233c897e3b5d47b42", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Home : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<PTW.DataAccess.Models.MasterPage>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "C:\Users\Test\source\repos\PTWWebsite\PTWWebsite\Views\Home\Home.cshtml"
  
    ViewData["Title"] = "PTW | Leading QA, Localization & Player Support Company";

#line default
#line hidden
            BeginContext(132, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(135, 19, false);
#line 6 "C:\Users\Test\source\repos\PTWWebsite\PTWWebsite\Views\Home\Home.cshtml"
Write(Model.HeaderContent);

#line default
#line hidden
            EndContext();
            BeginContext(154, 11, true);
            WriteLiteral(";\r\n<br />\r\n");
            EndContext();
            BeginContext(166, 19, false);
#line 8 "C:\Users\Test\source\repos\PTWWebsite\PTWWebsite\Views\Home\Home.cshtml"
Write(Model.FooterContent);

#line default
#line hidden
            EndContext();
            BeginContext(185, 5, true);
            WriteLiteral(";\r\n\r\n");
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
