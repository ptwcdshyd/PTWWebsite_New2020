﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Org.BouncyCastle.Crypto.Tls;
using PTW.DataAccess.Models;
using PTW.DataAccess.Services;
using System.Security.Claims;
using LoggerService;

namespace PTWWebsite2.Controllers
{
    public class AboutController : Controller
    {
        private readonly IMasterService _masterService;
        private readonly IAboutServices _aboutService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ILoggerManager _loggerManager;
        public AboutController(IMasterService masterService,IAboutServices aboutServices, IHostingEnvironment hostingEnvironment, ILoggerManager loggerManager)
        {
            _masterService = masterService;
            _aboutService = aboutServices;
            _hostingEnvironment = hostingEnvironment;
            _loggerManager = loggerManager;
        }

        [Route("about")]
        [Route("{culture}/about")]
        public IActionResult About(string culture = "en-US")
        {

            MasterPage masterPage = new MasterPage();
            masterPage.Culture = culture;
            //DataTable dtContent = _masterService.GetModuleContent("About", (culture == null ? "en-US" : culture));
            DataTable dtContent = _masterService.GetModuleContentSectionwise(13, (culture == null ? "en-US" : culture == "undefined" ? "en-US" : culture));
            List<string> sectionlist = dtContent.Rows.Cast<DataRow>().Where(x => x["ModuleId"].Equals(13)).Select(y => Convert.ToString(y["Content"])).ToList();
            for (int i = 0; i < sectionlist.Count - 1; i++)
            {
                masterPage.HtmlContent += sectionlist[i];
            }
            //masterPage.HtmlContent = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleId"]).Equals(13)).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
            ViewData["Header"] = dtContent.Rows.Cast<DataRow>().Where(x => x["ModuleId"].Equals(1)).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
            ViewData["Footer"] = dtContent.Rows.Cast<DataRow>().Where(x => x["ModuleId"].Equals(2)).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
            return View(masterPage);
        }
        [HttpGet]

        [Route("aboutprofile")]
        [Route("{culture}/aboutprofile")]

        public IActionResult GetAboutProfile(string culture = "en-US")
        {
            List<AboutModel> listabout = _aboutService.GetAboutProfile(culture);
            var profile = listabout;
            return Json(new { Profiles = profile }, new JsonSerializerSettings());
        }



        [Authorize]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpGet]
        [Route("getprofiles")]
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


        [Route("addprofile")]
        [Route("{ProfileId}-profile")]
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
            bool result;

            objprofile.Culture = objprofile.Culture == null ? "en-US": objprofile.Culture;
            
            if (objprofile.ImageUpload != null)
            {
                string createpath = Path.Combine(_hostingEnvironment.WebRootPath, "images/About/Profiles/");
                string CustomerfileName = objprofile.ImageUpload.FileName;

                objprofile.ImgPath = "/Images/About/Profiles/" + objprofile.ImageUpload.FileName;

                //Image file uploading
                FilePath(objprofile.ImageUpload, createpath + objprofile.ImageUpload.FileName, "","");

            }

            if (objprofile.ProfileId > 0)
            {
                result = _aboutService.UpdateProfileByProfileId(objprofile);
            }
            else
            {
                if(objprofile.ImageUpload != null)
                {
                    result = _aboutService.AddProfileByProfileId(objprofile);
                }
                else
                {
                    result = false;
                }
               
            }

            if (result == true )
            {
                return Json(new { ResultCode = 0, Message = "Success" }, new JsonSerializerSettings());
            }
           
            else 
            {
                return Json(new { ResultCode = -25, Message = "failed" }, new JsonSerializerSettings());
            }
        }

        [Route("aboutedit")]
        [HttpGet]
        public IActionResult AboutEdit()
        {
            MasterPage masterPage1 = _masterService.GetLanguageandModules();
            AboutModel about = new AboutModel();
            about.Languages = masterPage1.LanguageList;
            return View(about);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAboutContent(int ModuleId, string LanguageCode)
        {
            try
            {
                MasterPage masterPage = _masterService.GetModuleContentById(ModuleId, LanguageCode);
                return Json(masterPage, new JsonSerializerSettings());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAboutPageByLanguageId(AboutModel About)
        {
            try
            {
                string path = Path.Combine(_hostingEnvironment.WebRootPath, "images/About/AboutImages/");
                string deletePath = Path.Combine(_hostingEnvironment.WebRootPath, "images/About/PreviewImages/");
                string backUpPath = Path.Combine(_hostingEnvironment.WebRootPath, "images/About/Backup/");

                MasterPage master = _masterService.GetModuleContentById(About.ModuleId, About.LanguageCode);
                System.IO.File.WriteAllText(backUpPath + "About_" + Guid.NewGuid(), master.HtmlContent);

                if (About.AboutPageHeader != null)
                {
                    FilePath(About.AboutPageHeader, path + "AboutPageHeader.png", deletePath + "AboutPageHeader.png", backUpPath + Guid.NewGuid()+ "AboutPageHeader.png");
                }


                //int resultCode = _masterService.UpdateHomePageByLanguageId(About.ModuleId, About.LanguageCode, About.Description, About.MetaDescription, About.MetaTitle, About.MetaUrl);
                int resultCode = _masterService.UpdateSectionContent(About.SectionId, About.ModuleId, About.LanguageCode, About.Description, About.MetaDescription, About.MetaTitle, About.MetaUrl,About.IsActive,About.ShortOrder);

                return Json(resultCode, new JsonSerializerSettings());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAboutImagesToPreview(AboutModel About)
        {
            string path = Path.Combine(_hostingEnvironment.WebRootPath, "images/About/PreviewImages/");

            if (About.AboutPageHeader != null && About.Filename == "AboutPageHeader")
            {
                FilePath(About.AboutPageHeader, path + "AboutPageHeader.png", "","");
            }

            //if (About.Frame != null && About.Filename == "Frame")
            //{
            //    FilePath(About.Frame, path + "Frame.svg", "");
            //}

            return Json(1, new JsonSerializerSettings());
        }

        [HttpPost]
        public async Task<IActionResult> PreviewAbout(AboutModel About)
        {
            string savedPath = Path.Combine(_hostingEnvironment.WebRootPath, "images/About/AboutImages/");
            string previewPath = Path.Combine(_hostingEnvironment.WebRootPath, "images/About/PreviewImages/");
          

            if (About.AboutPageHeader != null && About.Filename == "AboutPageHeader")
            {
                About.Description = About.Description.Replace(savedPath + "AboutPageHeader.png", previewPath + "AboutPageHeader.png");
            }
            int resultCode = _masterService.UpdatePreviewContentByLanguageModuleId(About.SectionId,About.ModuleId, About.LanguageCode, About.Description, About.MetaDescription, About.MetaTitle, About.MetaUrl);

            return Json(resultCode, new JsonSerializerSettings());
        }

        public async void FilePath(IFormFile file, string path, string deletePath,string backUpPath)
        {
            _loggerManager.LogInfo(string.Format("Controller:About, Action :FilePath Data: file: {0}, path: {1}, deltepath:{2} ", file.Name, path, deletePath));

            if (!path.Contains("PreviewImages"))
            {
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Move(path, backUpPath);
                }
            }

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            if (deletePath != null && deletePath != "")
            {
                if (System.IO.File.Exists(deletePath))
                {
                    System.IO.File.Delete(deletePath);
                }
            }
        }

    }
}