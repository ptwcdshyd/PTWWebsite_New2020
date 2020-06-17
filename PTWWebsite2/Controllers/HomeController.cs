using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using PTW.DataAccess.Models;
using PTW.DataAccess.Services;
using PTWWebsite2.Models;

namespace PTWWebsite2.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMasterService _masterService;
        public HomeController(IMasterService masterService)
        {
            _masterService = masterService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("")]
        [Route("Home")]
        public IActionResult Home()
        {
            MasterPage masterPage = _masterService.GetDashboardDetails(0, 1, 3, "en-US");

            MasterPage masterPage1 = _masterService.GetLanguageandModules();
            masterPage1.HtmlContent = masterPage.HtmlContent;
            ViewBag.HomeContent = "<div class=\"fixed-header\"><div class=\"container\"><nav><a href = \"#\"> Home </a><a href=\"#\">About</a><a href = \"#\" > Products </a><a href=\"#\">Services</a><a href =\"#\"> Contact Us</a></nav></div></div>";
            //ViewBag.HomeContent = "This is Ashok";

            return View(masterPage1);
        }

        [Route("Editor")]
        public IActionResult Editor()
        {
            MasterPage masterPage1 = _masterService.GetLanguageandModules();
            ViewBag.HomeContent = "This is Ashok";
            return View(masterPage1);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult GetContent(int ModuleId, string LanguageCode)
        {
            try
            {
                MasterPage masterPage = _masterService.GetHtmlContentForPage(ModuleId, LanguageCode);
                return Json(masterPage.HtmlContent, new JsonSerializerSettings());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public IActionResult UpdateContentByModelIdAndLanguageId([FromBody] MasterPage masterContent)
        {
            try
            {
                MasterPage masterPage = new MasterPage();
                int resultCode = _masterService.UpdateContentByModelIdAndLanguageId(masterContent.ModuleId, masterContent.LanguageCode, masterContent.Content);
                if (resultCode > 0)
                {
                    masterPage = _masterService.GetHtmlContentForPage(masterContent.ModuleId, masterContent.LanguageCode);
                }
                masterPage.ResultCode = resultCode;
                return Json(masterPage, new JsonSerializerSettings());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public IActionResult GetHeaderPageDetails(int ModuleId, string Languagecode)
        {
            try
            {
                MasterPage masterPage = _masterService.GetHtmlContentForPage(ModuleId, Languagecode);

                return Json(masterPage.HtmlContent, new JsonSerializerSettings());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public IActionResult GetFooterPageDetails(int ModuleId, string Languagecode)
        {
            try
            {
                MasterPage masterPage = _masterService.GetHtmlContentForPage(ModuleId, Languagecode);

                return Json(masterPage.HtmlContent, new JsonSerializerSettings());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
