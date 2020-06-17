using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PTW.DataAccess.Models;
using PTW.DataAccess.Services;

namespace PTWWebsite2.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsEventService _NewsEventService;
        public NewsController(INewsEventService newsEventService)
        {
            _NewsEventService = newsEventService;
        }

      

        [Route("News")]
        public IActionResult News()
        {
            NewsEvents newsEvents = new NewsEvents();
            newsEvents.News = _NewsEventService.GetAllNewsDetails();
            newsEvents.Events = _NewsEventService.GetAllEventDetails();
            return View(newsEvents);
        }
    }
}
