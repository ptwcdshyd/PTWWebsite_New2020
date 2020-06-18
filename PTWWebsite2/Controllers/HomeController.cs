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
        [Route("{culture}/Home")]
        [Route("{culture}/")]
        public IActionResult Home(string culture)
        {
            MasterPage masterPage = new MasterPage();
            if (culture == null)
            {
                masterPage = _masterService.GetHtmlContentForPage(3, "en-US");
            }

            else
            {
                masterPage = _masterService.GetHtmlContentForPage(3,culture);
            }

            return View(masterPage);
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
