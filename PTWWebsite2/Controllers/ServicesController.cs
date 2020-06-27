using Microsoft.AspNetCore.Mvc;
using PTW.DataAccess.Models;
using PTW.DataAccess.Services;

namespace PTWWebsite2.Controllers
{
    public class ServicesController:Controller
    {
        private readonly IMasterService _masterService;
        public ServicesController(IMasterService masterService)
        {
            _masterService = masterService;
        }
        
        
        [Route("Service")]
        [Route("{culture}/Service")]
        
        public IActionResult GetServicecontent(string culture)
        {
            MasterPage masterPage;
            if (culture == null)
            {
                masterPage = _masterService.GetHtmlContentForPage(4,"en-US");
            }

            else
            {
                masterPage = _masterService.GetHtmlContentForPage(4, culture);
            }
            return View(masterPage);
        }

        [Route("Service_Cx")]
        [Route("{culture}/Service_Cx")]

        public IActionResult GetServicCxecontent(string culture)
        {
            MasterPage masterPage;
            if (culture == null)
            {
                masterPage = _masterService.GetHtmlContentForPage(5, "en-US");
            }

            else
            {
                masterPage = _masterService.GetHtmlContentForPage(5, culture);
            }
            MasterPage masterPage1 = _masterService.GetNewsAndLabDetails("CX", 1);
            masterPage1.HtmlContent = masterPage.HtmlContent;
            string heading = "<div class=\"col-lg-12 col-md-12 col-sm-12 col-sx-12\"><p id=\"ContentPlaceHolder1_P1\">News &amp; LAB Feed</p></div>";
            string content = "";

            content = "<div class=\"col-lg-12 col-md-12 col-sm-12 col-sx-12\"><p id=\"ContentPlaceHolder1_P1\">News &amp; LAB Feed</p></div>";
            foreach (var item in masterPage1.NewsAndLabs)
            {
                content = content + "<div class=\"col-lg-12 col-md-4 col-sm-12 col-sx-12\"><div class=\"g-underline\"></div><h6>" + item.Title + " </h6> <a href=\"/News/" + item.ShortDescription.Replace(" ", "-").Replace("&", "@") + "\" titlevalue=\"4\" class=\"btn btn-default btn-sm btn-r text-lowercase  more pull-down float-left\">more</a></div>";

            }
            masterPage1.HtmlContent = masterPage1.HtmlContent.Replace(heading, content);


            return View(masterPage1);
        }
        

        [Route("Service_QA")]
        [Route("{culture}/Service_QA")]
        //[Route("{culture}/")]
        public IActionResult GetServiceQacontent(string culture)
        {
            MasterPage masterPage;
            if (culture == null)
            {
                masterPage = _masterService.GetHtmlContentForPage(6, "en-US");
            }

            else
            {
                masterPage = _masterService.GetHtmlContentForPage(6, culture);
            }
            return View(masterPage);
        }

        [Route("Localization")]
        [Route("{culture}/Localization")]
        //[Route("{culture}/")]
        public IActionResult GetServiceLocalizationcontent(string culture)
        {
            MasterPage masterPage;
            if (culture == null)
            {
                masterPage = _masterService.GetHtmlContentForPage(7, "en-US");
            }

            else
            {
                masterPage = _masterService.GetHtmlContentForPage(7, culture);
            }
            return View(masterPage);
        }

        [Route("AudioProduction")]

        [Route("{culture}/AudioProduction")]
        //[Route("{culture}/")]
        public IActionResult GetServiceAudioProductioncontent(string culture)
        {
            MasterPage masterPage;
            if (culture == null)
            {
                masterPage = _masterService.GetHtmlContentForPage(8, "en-US");
            }

            else
            {
                masterPage = _masterService.GetHtmlContentForPage(8, culture);
            }
            return View(masterPage);
        }

        [Route("Engineering")]

        [Route("{culture}/Engineering")]
        //[Route("{culture}/")]
        public IActionResult GetServiceEngineeringcontent(string culture)
        {
            MasterPage masterPage;
            if (culture == null)
            {
                masterPage = _masterService.GetHtmlContentForPage(9, "en-US");
            }

            else
            {
                masterPage = _masterService.GetHtmlContentForPage(9, culture);
            }
            return View(masterPage);
        }
    }
}
