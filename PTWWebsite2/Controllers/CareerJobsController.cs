using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PTW.DataAccess.Models;
using PTW.DataAccess.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Hosting;
using LoggerService;
using System.IO;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace PTWWebsite2.Controllers
{
    public class CareerJobsController : Controller
    {
        private readonly IMasterService _masterService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ILoggerManager _loggerManager;
        public CareerJobsController(IMasterService masterService, IHostingEnvironment hostingEnvironment, ILoggerManager loggerManager)
        {
            _masterService = masterService;
            _hostingEnvironment = hostingEnvironment;
            _loggerManager = loggerManager;
        }


        [Route("Careers")]
        [Route("{culture}/Careers")]
        public IActionResult Careers(string culture)
        {
            MasterPage masterPage = new MasterPage();
            DataTable dtContent = _masterService.GetModuleContent("Careers", (culture == null ? "en-US" : culture));
            masterPage.HtmlContent = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Careers")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
            ViewData["Header"] = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Header")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
            ViewData["Footer"] = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Footer")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
            return View(masterPage);
           
        }

        [Route("Jobs")]
        [Route("{culture}/Jobs")]
        public IActionResult Jobs(string culture)
        {
            MasterPage masterPage = new MasterPage();
            DataTable dtContent = _masterService.GetModuleContent("Jobs", (culture == null ? "en-US" : culture));
            masterPage.HtmlContent = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Jobs")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
            ViewData["Header"] = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Header")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
            ViewData["Footer"] = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Footer")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
            return View(masterPage);
           
        }

        //[Authorize]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        [Route("CareersEdit")]
        [HttpGet]
        public IActionResult CareersEdit()
        {
            MasterPage masterPage = _masterService.GetLanguageandModules();

            //masterPage.Languages = masterPage.LanguageList;
            return View(masterPage);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCareersPageByLanguageId(Careers Careers)
        {
            try
            {
                string path = Path.Combine(_hostingEnvironment.WebRootPath, "images/Careers/CareerImages/");
                string deletePath = Path.Combine(_hostingEnvironment.WebRootPath, "images/Careers/PreviewImages/");

                if (Careers.CareerHeader != null)
                {
                    FilePath(Careers.CareerHeader, path + "CareerHeader.png", deletePath + "CareerHeader.png");
                }

                if (Careers.Deborah != null)
                {
                    FilePath(Careers.Deborah, path + "Deborah.png", deletePath + "Deborah.png");
                }

                if (Careers.Frame != null)
                {
                    FilePath(Careers.Frame, path + "Frame.svg", deletePath + "Frame.svg");
                }

                int resultCode = _masterService.UpdateHomePageByLanguageId(Careers.ModuleId, Careers.LanguageCode, Careers.Description, Careers.MetaDescription, Careers.MetaTitle, Careers.MetaUrl);

                return Json(resultCode, new JsonSerializerSettings());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCareersImagesToPreview(Careers Careers)
        {
            string path = Path.Combine(_hostingEnvironment.WebRootPath, "images/Careers/PreviewImages/");

            if (Careers.CareerHeader != null && Careers.Filename == "CareerHeader")
            {
                FilePath(Careers.CareerHeader, path + "CareerHeader.png", "");
            }

            if (Careers.Deborah != null && Careers.Filename == "Deborah")
            {
                FilePath(Careers.Deborah, path + "Deborah.png", "");
            }
           
            if (Careers.Frame != null && Careers.Filename == "Frame")
            {
                FilePath(Careers.Frame, path + "Frame.svg", "");
            }
            int resultCode = _masterService.UpdatePreviewPageByLanguageModuleId(Careers.ModuleId, Careers.LanguageCode, Careers.Description, Careers.MetaDescription, Careers.MetaTitle, Careers.MetaUrl);

            return Json(resultCode, new JsonSerializerSettings());
        }


        [HttpPost]
        public async Task<IActionResult> PreviewCareers(Careers Careers)
        {
            string savedPath = Path.Combine(_hostingEnvironment.WebRootPath, "images/Careers/CareerImages/");
            string previewPath = Path.Combine(_hostingEnvironment.WebRootPath, "images/Careers/PreviewImages/");


            if (Careers.CareerHeader != null)
            {
                Careers.Description = Careers.Description.Replace(savedPath + "CareerHeader.png", previewPath + "CareerHeader.png");
            }

            if (Careers.Deborah != null)
            {
                Careers.Description = Careers.Description.Replace(savedPath + "Deborah.png", previewPath + "Deborah.png");
            }

            if (Careers.Frame != null)
            {
                Careers.Description = Careers.Description.Replace(savedPath + "Frame.svg", previewPath + "Frame.svg");
            }

            int resultCode = _masterService.UpdatePreviewPageByLanguageModuleId(Careers.ModuleId, Careers.LanguageCode, Careers.Description, Careers.MetaDescription, Careers.MetaTitle, Careers.MetaUrl);

            return Json(resultCode, new JsonSerializerSettings());
        }

        public async void FilePath(IFormFile file, string path, string deletePath)
        {
            try
            {
                _loggerManager.LogInfo(string.Format("Method :FilePath Data: file: {0}, path: {1}, deltepath:{2} ", file.Name, path, deletePath));
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
            catch (Exception exception)
            {
                _loggerManager.LogError("Method :FilePath error:" + exception.Message);

            }
        }
    }
}
