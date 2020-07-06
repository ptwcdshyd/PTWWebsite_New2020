using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Org.BouncyCastle.Crypto.Tls;
using PTW.DataAccess.Models;
using PTW.DataAccess.Services;


namespace PTWWebsite2.Controllers
{
    public class AboutController : Controller
    {
        private readonly IMasterService _masterService;
        private readonly IAboutServices _aboutService;
        public AboutController(IMasterService masterService,IAboutServices aboutServices)
        {
            _masterService = masterService;
            _aboutService = aboutServices;
        }

        [Route("about")]
        [Route("{culture}/about")]
        public IActionResult About(string culture = "en-US")
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

        [Route("AboutProfile")]
        [Route("{culture}/AboutProfile")]

        public IActionResult GetAboutProfile(string culture = "en-US")
        {
            List<AboutModel> listabout = _aboutService.GetAboutProfile(culture);
            var profile = listabout;
            return Json(new { Profiles = profile }, new JsonSerializerSettings());
        }




        [HttpGet]
        [Route("GetProfiles")]
        public IActionResult GetProfile()
        {
            return View();
        }

        public IActionResult Profile()
        {

            List<AboutModel> listabout = _aboutService.GetProfile();

            var profile = listabout;

            return Json(new { Profiles = profile }, new JsonSerializerSettings());
        }


        [Route("AddProfile")]
        [Route("{ProfileId}-Profile")]
        public IActionResult AddProfile(int ProfileId)
        {
            AboutModel objabout = new AboutModel();

            if(ProfileId > 0)
            {
                objabout = _aboutService.GetProfileByProfileId(ProfileId, "en-US");
            }
            
            objabout.Languagelist = _aboutService.GetLanguages();

            objabout.LanguageCode = "en-US";

            return View(objabout);
        }


        public IActionResult GetContent(int ProfileId,string LanguageCode)
        {
            AboutModel objabout =new AboutModel();

            objabout = _aboutService.GetProfileByProfileId(ProfileId, LanguageCode);

            return Json(new { Profiles = objabout }, new JsonSerializerSettings());
        }

        [HttpPost]
        public IActionResult UpdateProfile(AboutModel objprofile)
        {
            AboutModel objabout = new AboutModel();
            bool i;
            

            objabout.ImageUpload = objprofile.ImageUpload;
            objabout.IsActive = objprofile.IsActive;
            objabout.ImgPath = objprofile.ImgPath;
            objabout.OrderNo = objprofile.OrderNo;
            objabout.ProfileId = objprofile.ProfileId;
            objabout.ProfileTitle = objprofile.ProfileTitle;
            objabout.Description = objprofile.Description;
            objabout.Culture = objprofile.Culture == null ? "en-US": objprofile.Culture;
            
            if (objprofile.ImageUpload != null)
            {

                string CustomerfileName = objprofile.ImageUpload.FileName;
                string createpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "Execs");
                string path = createpath + "\\" + CustomerfileName;
                objabout.ImgPath = "/Images/Execs/" + objprofile.ImageUpload.FileName;

                using (var bits = new FileStream(path, FileMode.Create))
                {
                    objprofile.ImageUpload.CopyToAsync(bits);
                }
               
            }

            if (objabout.ProfileId > 0)
            {
                i = _aboutService.UpdateProfileByProfileId(objabout);
            }
            else
            {
                if(objprofile.ImageUpload != null)
                {
                    i = _aboutService.AddProfileByProfileId(objabout);

                    
                }
                else
                {
                    
                    i = false;
                   
                }
               
            }

            if (i == true )
            {
                return Json(new { ResultCode = 0, Message = "Success" }, new JsonSerializerSettings());
            }
           
            else 
            {
                return Json(new { ResultCode = -25, Message = "failed" }, new JsonSerializerSettings());
            }
        }


    }
}