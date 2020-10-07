using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace PTWWebsite2.Controllers
{
    public class DashBoardController : Controller
    {
        [Route("DashBoard")]
        public IActionResult DashBoardDetails()
        {
            return View();
        }
    }
}
