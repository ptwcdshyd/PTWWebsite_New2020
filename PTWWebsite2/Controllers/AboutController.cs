using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PTW.DataAccess.Models;
using PTW.DataAccess.Services;
using PTWWebsite2.Models;

namespace PTWWebsite2.Controllers
{
    public class AboutController : Controller
    {
        private readonly IMasterService _masterService;
        public AboutController(IMasterService masterService)
        {
            _masterService = masterService;
        }

        [Route("about")]
        [Route("{culture}/about")]
        public IActionResult About(string culture ="en-US")
        {

            MasterPage masterPage = new MasterPage();
            masterPage.Culture = culture;
            DataTable dtContent = _masterService.GetModuleContent("About", (culture == null ? "en-US" : culture));
            masterPage.HtmlContent = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("About")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
            ViewData["Header"] = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Header")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
            ViewData["Footer"] = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Footer")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
            return View(masterPage);
        }
        [HttpGet]
        [Route("{culture}/AboutProfile")]

        public IActionResult AboutProfile(string culture = "en-US")
        {
           DataTable dt = _masterService.GetAboutProfile(culture);
           List<AboutModel> listabout = new List<AboutModel>();
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    AboutModel objAbout = new AboutModel();
                    objAbout.Title = dt.Rows[i]["Title"].ToString();
                    objAbout.Description = dt.Rows[i]["Description"].ToString();
                    objAbout.ImgPath = dt.Rows[i]["ImgPath"].ToString();
                    objAbout.OrderNo =Convert.ToInt32(dt.Rows[i]["OrderNo"]);
                    listabout.Add(objAbout);
                }
            }
            var profile = listabout;

            return Json(new { Profiles = profile }, new JsonSerializerSettings());
        }



    }
}