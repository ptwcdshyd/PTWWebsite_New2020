using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

       
        public IActionResult Home()
        {
            MasterPage masterPage = _masterService.GetDashboardDetails(0, 1,3);

            MasterPage masterPage1 = _masterService.GetLanguageandModules();
            masterPage1.HtmlContent = masterPage.HtmlContent;

            //ViewBag.HomeContent = "<div class=\"fixed-header\"><div class=\"container\"><nav><a href = \"#\"> Home </a><a href=\"#\">About</a><a href = \"#\" > Products </a><a href=\"#\">Services</a><a href =\"#\"> Contact Us</a></nav></div></div>";
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
        public IActionResult GetContent(int ModuleId, int LanguageId)
        {
            try
            {
                MasterPage masterPage = _masterService.GetDashboardDetails(0, LanguageId, ModuleId);
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
                string message = _masterService.UpdateContentByModelIdAndLanguageId(masterContent.ModuleId, masterContent.LanguageId, masterContent.Content);
                MasterPage masterPage = _masterService.GetDashboardDetails(0, masterContent.LanguageId, masterContent.ModuleId);
                return Json(masterPage.HtmlContent, new JsonSerializerSettings());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IActionResult NewsandEvents()
        {
            return View();
        }
    }
}
