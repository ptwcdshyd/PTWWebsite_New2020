#pragma checksum "C:\GitHub\PTWWebsite_New2020\PTWWebsite2\Views\Shared\Footer\_Footer.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "87abc1eb121f1e84280c46d4a98ebc53165cc5a4"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_Footer__Footer), @"mvc.1.0.view", @"/Views/Shared/Footer/_Footer.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Shared/Footer/_Footer.cshtml", typeof(AspNetCore.Views_Shared_Footer__Footer))]
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
#line 1 "C:\GitHub\PTWWebsite_New2020\PTWWebsite2\Views\_ViewImports.cshtml"
using PTWWebsite2;

#line default
#line hidden
#line 2 "C:\GitHub\PTWWebsite_New2020\PTWWebsite2\Views\_ViewImports.cshtml"
using PTWWebsite2.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"87abc1eb121f1e84280c46d4a98ebc53165cc5a4", @"/Views/Shared/Footer/_Footer.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e06c3cf507bbcfa1d594e9419908f30cddbc47f0", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_Footer__Footer : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 2 "C:\GitHub\PTWWebsite_New2020\PTWWebsite2\Views\Shared\Footer\_Footer.cshtml"
  
    PTW.DataAccess.ServicesImpl.MasterService masterService = new PTW.DataAccess.ServicesImpl.MasterService();

    var data = masterService.GetDashboardDetails(0, 1, 2);


#line default
#line hidden
            BeginContext(185, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(221, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(340, 11475, true);
            WriteLiteral(@"
    <div class=""row"">
        <div id=""cxcontactus"" style=""padding-top:189px !important;"" class=""col-lg-12 gray"">
            <h5 id=""ContentPlaceHolder1_H16"" class=""mob-disp getInTouch"">Get in touch</h5>
            <a href=""contact"" id=""ContentPlaceHolder1_A7"" titlevalue=""6"" class=""btn btn-default btn-sm btn-r vjobs f-contact"" title=""Contact Us"">Contact Us</a>
        </div>
    </div>
    <section class=""panel position-relative footer-parent"">
        <!-- Footer -->
        <footer class=""text-white"">
            <div class=""row no-padding"">
                <div class=""col-lg-2 col-md-4 col-sm-12 order-lg-1 order-md-1  order-sm-1 v-excmob footer-info poppins-footer f1"">
                    <ul class=""pull-xs-right nav navbar-nav navbar-toggleable-sm"">
                        <li class=""nav-item"">
                            <a href=""home"" id=""Footer1_A1"" class=""nav-link"" titlevalue=""1"" style=""color: rgb(255, 255, 255);"">Home</a>
                        </li>
                        <li cl");
            WriteLiteral(@"ass=""nav-item"">
                            <a href=""services-industries"" id=""Footer1_A2"" class=""nav-link"" titlevalue=""2"" style=""color: rgb(255, 255, 255);"">Services &amp; Industries</a>
                        </li>
                        <li class=""nav-item"">
                            <a href=""about"" id=""Footer1_A3"" class=""nav-link"" titlevalue=""3"" style=""color: rgb(255, 255, 255);"">About</a>
                        </li>
                        <li class=""nav-item"">
                            <a href=""news-events"" id=""Footer1_A4"" class=""nav-link"" titlevalue=""4"" style=""color: rgb(255, 255, 255);"">News &amp; Events</a>
                        </li>
                    </ul>
                </div>

                <div class=""col-lg-2 col-md-4 col-sm-12 order-lg-2 order-md-4 order-sm-2 v-excmob footer-info poppins-footer  f2"">
                    <ul class=""pull-xs-right nav navbar-nav navbar-toggleable-sm"">
                        <li class=""nav-item"">
                            <a href=""c");
            WriteLiteral(@"areers"" id=""Footer1_A5"" class=""nav-link"" titlevalue=""5"" style=""color: rgb(255, 255, 255);"">Careers</a>
                        </li>
                        <li class=""nav-item"">
                            <a href=""contact"" id=""Footer1_A6"" class=""nav-link contact"" titlevalue=""6"" style=""color: rgb(255, 255, 255);"">Contact &amp; Locations</a>
                        </li>
                        <li class=""nav-item"">
                            <a href=""lab"" id=""Footer1_A7"" class=""nav-link"" titlevalue=""7"" style=""color: rgb(255, 255, 255);"">Lab</a>
                        </li>
                    </ul>
                </div>

                <div class=""col-lg-2 col-md-4 col-sm-12  order-lg-3 order-md-5 order-sm-3 order-sx-2 footer-social poppins-footer  mob-hidden f3"">
                    <ul class=""pull-xs-right nav navbar-nav navbar-toggleable-sm"">
                        <li class=""nav-item"">
                            <a href=""PDF/PTW_New_Website_Terms_of_Usage_2018.pdf"" id=""Footer1_A8"" cla");
            WriteLiteral(@"ss=""nav-link"" target=""_blank"" rel=""noopener noreferrer"" style=""color: rgb(255, 255, 255);"">Website Terms of Usage</a>
                        </li>
                        <li class=""nav-item"">
                            <a href=""PDF/PTW_Privacy_Policy.pdf"" id=""Footer1_A9"" class=""nav-link"" target=""_blank"" rel=""noopener noreferrer"" style=""color: rgb(255, 255, 255);"">Privacy Policy</a>
                        </li>
                        <li class=""nav-item"">
                            <a href=""PDF/PTW_Modern_Slavery_Statement_2018.pdf"" id=""Footer1_A18"" class=""nav-link"" target=""_blank"" rel=""noopener noreferrer"" style=""color: rgb(255, 255, 255);"">Modern Slavery Statement</a>
                        </li>
                        <li class=""nav-item"">
                            <a href=""PDF/Corporate_Social_Responsibility_V2.pdf"" id=""Footer1_A21"" class=""nav-link"" target=""_blank"" rel=""noopener noreferrer"" style=""color: rgb(255, 255, 255);"">CSR Policy (India)</a>
                        </li>
        ");
            WriteLiteral(@"                <li class=""nav-item"">
                            <a href=""PDF/2019_Gender_Pay_Gap_Report.pdf"" id=""Footer1_A23"" class=""nav-link"" target=""_blank"" rel=""noopener noreferrer"" style=""color: rgb(255, 255, 255);"">Gender Pay Gap Report</a>
                        </li>
                    </ul>
                </div>

                <div class=""col-lg-2 col-md-4 col-sm-4 order-lg-4 order-md-2 order-sm-4 order-sx-1 footer-newsletter poppins-footer f4"">
                    <ul class=""pull-xs-right nav navbar-nav navbar-toggleable-sm"">
                        <li class=""nav-item""><a href=""https://twitter.com/PTW"" target=""_blank"" rel=""noopener noreferrer"" class=""twitter nav-link"" title=""Twitter"" style=""color: rgb(255, 255, 255);""><i class=""fa fa-twitter""></i><span class=""m-l-20"">Twitter</span></a></li>
                        <li class=""nav-item""><a href=""https://www.facebook.com/PTWintl/"" target=""_blank"" rel=""noopener noreferrer"" class=""facebook nav-link"" title=""Facebook"" style=""color: rgb(255,");
            WriteLiteral(@" 255, 255);""><i class=""fa fa-facebook""></i><span class=""m-l-20"">&nbsp;&nbsp;&nbsp;Facebook</span></a></li>
                        <li class=""nav-item""><a href=""https://www.linkedin.com/company/ptwintl/"" target=""_blank"" rel=""noopener noreferrer"" class=""linkedin nav-link"" title=""LinkedIn"" style=""color: rgb(255, 255, 255);""><i class=""fa fa-linkedin-square""></i><span class=""m-l-20"">&nbsp;LinkedIn</span></a></li>
                    </ul>
                </div>

                <div class=""col-lg-2 col-md-4 col-sm-12 order-lg-5 order-md-3 order-sm-5 order-sx-3 footer-address poppins-footer f5"">
                    <p id=""Footer1_P1"">Pole To Win International<br>Corporate Headquarters<br>125 E. Sir Francis Drake Blvd<br>Suite 401, Larkspur, CA 94939, USA</p>
                </div>


                <div class=""backtop pull-right"" style="""">
                    <a href=""#top""><img src=""/Images/up-arrow.png"" class=""a-to-top""></a>
                </div>
                <div class=""iso pull-right"">
      ");
            WriteLiteral(@"              <img src=""/Images/Logo/ISO.png"" title=""ISO 27001"" class=""no-margin-bottom text-white text-center iso-logo"">
                </div>


                <div class=""col-lg-12 col-md-4 col-sm-12 order-lg-6 order-md-6 order-sm-6 order-sx-6 footer-copyright"">
                    <p id=""Footer1_P2"" class=""poppins-copyright v-tab"">Copyright © 2020.</p>
                    <p id=""Footer1_P3"" class=""poppins-copyright v-tab"">All Rights Reserved. Pole To Win International is a group of subsidiaries of <a class=""Pitcrew"" href=""http://www.poletowin-pitcrew-holdings.co.jp/en/"" target=""_blank"" rel=""noopener noreferrer"">Poletowin Pitcrew Holdings Inc.</a></p>
                    <p id=""Footer1_P4"" class=""poppins-copyright v-mob"">Copyright © 2020. All Rights Reserved. Pole To Win International is a group of subsidiaries of <a class=""Pitcrew"" href=""http://www.poletowin-pitcrew-holdings.co.jp/en/"" target=""_blank"" rel=""noopener noreferrer"">Poletowin Pitcrew Holdings Inc.</a></p>
                </div>
      ");
            WriteLiteral(@"      </div>
        </footer>
        <div class=""col-lg-12 col-md-4 col-sm-4 order-lg-6 order-md-6 order-sm-6 order-sx-6 footer-copyright"">
            <p id=""Footer1_P5"" class=""poppins-copyright v-d"">Copyright © 2020. All Rights Reserved. Pole To Win International is a group of subsidiaries of <a class=""Pitcrew"" href=""http://www.poletowin-pitcrew-holdings.co.jp/en/"" target=""_blank"" rel=""noopener noreferrer"">Poletowin Pitcrew Holdings Inc.</a></p>
        </div>
        <div id=""mobile-footer"" class=""p-40px v-mob"">
            <ul class=""navbar-nav"">
                <li class=""nav-item-t dropdown"">
                    <a class=""dropdown-toggle color-black"" href=""#"" id=""bd-versions"" data-toggle=""dropdown"" aria-haspopup=""true"" aria-expanded=""true"">
                        <span id=""Footer1_fLanguage"">English</span>
                    </a>

                    <div class=""dropdown-menu dropdown-menu-right "" aria-labelledby=""bd-versions"">
                        <p onclick=""languageChange('en-US')");
            WriteLiteral(@""" class=""dropdown-item"" meta:resourcekey=""EnglishListItem"">English</p>
                        <div class=""dropdown-divider""></div>
                        <p onclick=""languageChange('fr-CA')"" class=""dropdown-item"" meta:resourcekey=""CAFrenchListItem"">Canadian French</p>
                        <div class=""dropdown-divider""></div>
                        <p onclick=""languageChange('ja')"" class=""dropdown-item"" meta:resourcekey=""JapaneseListItem"">Japanese 日本語</p>
                        <div class=""dropdown-divider""></div>
                        <p onclick=""languageChange('ko')"" class=""dropdown-item"" meta:resourcekey=""KoreanListItem"">Korean 한국어</p>
                        <div class=""dropdown-divider""></div>

                        <p onclick=""languageChange('zh-CHS')"" class=""dropdown-item"" meta:resourcekey=""CHSListItem"">Simplified Chinese 简体中文</p>

                    </div>
                </li>
                <li class=""nav-item-t"">
                    <a href=""home"" id=""Footer1_A10"" class=""n");
            WriteLiteral(@"av-link"" titlevalue=""1"">Home</a>
                </li>
                <li class=""nav-item-t"">
                    <a href=""services-industries"" id=""Footer1_A11"" class=""nav-link"" titlevalue=""2"">Services &amp; Industries</a>
                </li>
                <li class=""nav-item-t"">
                    <a href=""about"" id=""Footer1_A12"" class=""nav-link"" titlevalue=""3"">About</a>
                </li>
                <li class=""nav-item-t"">
                    <a href=""news-events"" id=""Footer1_A13"" class=""nav-link"" titlevalue=""4"">News &amp; Events</a>
                </li>
                <li class=""nav-item-t"">
                    <a href=""careers"" id=""Footer1_A14"" class=""nav-link"" titlevalue=""5"">Careers</a>
                </li>
                <li class=""nav-item-t"">
                    <a href=""contact"" id=""Footer1_A15"" class=""nav-link"" titlevalue=""6"">Contact &amp; Locations</a>
                </li>
                <li class=""nav-item-t navitem1"">
                    <a href=""lab"" id=""Foo");
            WriteLiteral(@"ter1_A20"" class=""nav-link"" titlevalue=""7"">Lab</a>
                </li>
                <li class=""nav-item-t navitem2"">
                    <a href=""PDF/PTW_New_Website_Terms_of_Usage_2018.pdf"" id=""Footer1_A16"" titlevalue=""8"" target=""_blank"" rel=""noopener noreferrer"">Website Terms of Usage</a>
                </li>
                <li class=""nav-item-t"">
                    <a href=""PDF/PTW_Privacy_Policy.pdf"" id=""Footer1_A17"" titlevalue=""9"" target=""_blank"" rel=""noopener noreferrer"">Privacy Policy</a>
                </li>
                <li class=""nav-item-t"">
                    <a href=""PDF/PTW_Modern_Slavery_Statement_2018.pdf"" id=""Footer1_A19"" target=""_blank"" rel=""noopener noreferrer"">Modern Slavery Statement</a>
                </li>
                <li class=""nav-item-t"">
                    <a id=""A22"" href=""../PDF/Corporate_Social_Responsibility_V2.pdf"" target=""_blank"" rel=""noopener noreferrer"">CSR Policy (India)</a>
                </li>
                <li class=""nav-item"">
       ");
            WriteLiteral("             <a id=\"A24\" href=\"../PDF/2019_Gender_Pay_Gap_Report.pdf\" target=\"_blank\" rel=\"noopener noreferrer\">Gender Pay Gap Report</a>\r\n                </li>\r\n            </ul>\r\n        </div>\r\n    </section>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
