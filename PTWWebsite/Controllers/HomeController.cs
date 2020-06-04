using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PTW.DataAccess.Models;
using PTW.DataAccess.Services;
using PTWWebsite.Models;

namespace PTWWebsite.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMasterService _masterService;
        public HomeController(IMasterService masterService)
        {
            _masterService = masterService;
        }

        public IActionResult Home()
        {
            MasterPage masterPage = _masterService.GetDashboardDetails(0, 1);
            return View(masterPage);
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
    }
}
