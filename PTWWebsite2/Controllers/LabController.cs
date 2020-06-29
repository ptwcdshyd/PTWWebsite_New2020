using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using PTW.DataAccess.Models;
using PTW.DataAccess.Services;

namespace PTWWebsite2.Controllers
{
    
    public class LabController : Controller
    {
        private readonly ILabService _LabEventService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IMasterService _masterService;

        public LabController(ILabService LabEventService, IHostingEnvironment hostingEnvironment, IMasterService masterService)
        {
            _LabEventService =LabEventService;
            _hostingEnvironment = hostingEnvironment;
            _masterService = masterService;
        }

        [Route("LAB")]
        [Route("{culture}/LAB")]
        public IActionResult Index(string culture = "en-US")
        {
            LabsEvents LabsEvents = new LabsEvents();
            LabsEvents.allLabs = _LabEventService.GetAllLabsDetails(culture);
            LabsEvents.labInsights = _LabEventService.GetAllLatestInsights(culture);
            LabsEvents.Labs = _LabEventService.GetSlider();
            return View(LabsEvents);
        }
    }
}