using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PTW.DataAccess.Models;
using PTW.DataAccess.Services;

namespace PTWWebsite2.Controllers
{
    public class CareerJobsController : Controller
    {
        private readonly IMasterService _masterService;
        public CareerJobsController(IMasterService masterService)
        {
            _masterService = masterService;
        }


        [Route("Careers")]
        [Route("{culture}/Careers")]
        public IActionResult Careers(string culture)
        {
            MasterPage masterPage = new MasterPage();
            if (culture == null)
            {
                masterPage = _masterService.GetHtmlContentForPage(10, "en-US");
            }

            else
            {
                masterPage = _masterService.GetHtmlContentForPage(10, culture);
            }

            return View(masterPage);
        }

        [Route("Jobs")]
        [Route("{culture}/Jobs")]
        public IActionResult Jobs(string culture)
        {
            MasterPage masterPage = new MasterPage();
            if (culture == null)
            {
                masterPage = _masterService.GetHtmlContentForPage(11, "en-US");
            }

            else
            {
                masterPage = _masterService.GetHtmlContentForPage(11, culture);
            }

            return View(masterPage);
        }
    }
}
