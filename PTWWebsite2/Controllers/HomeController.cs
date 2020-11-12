using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using PTW.DataAccess.Models;
using PTW.DataAccess.Services;
using PTWWebsite2.Models;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using System.Security.Claims;
using LoggerService;

namespace PTWWebsite2.Controllers
{
    [Authorize]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public class HomeController : Controller
    {
        private readonly IMasterService _masterService;
        private readonly IUserService _userService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ILoggerManager _loggerManager;
        public HomeController(IMasterService masterService, IUserService userService, IHostingEnvironment hostingEnvironment, ILoggerManager loggerManager)
        {
            _masterService = masterService;
            _userService = userService;
            _hostingEnvironment = hostingEnvironment;
            _loggerManager = loggerManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("")]
        [Route("home")]
        [Route("{culture}/home")]
        [Route("{culture}/")]
        [AllowAnonymous]
        public IActionResult Home(string culture)
        {
            string appendLabs = string.Empty;
            MasterPage masterPage = new MasterPage();
            //DataTable dtContent = _masterService.GetModuleContent("Home", (culture == null ? "en-US" : culture == "undefined" ? "en-US" : culture));
            DataTable dtContent = _masterService.GetModuleContentSectionwise(3, (culture == null ? "en-US" : culture == "undefined" ? "en-US" : culture));
            //masterPage.HtmlContent = dtContent.Rows.Cast<DataRow>().Where(x =>x["ModuleId"].Equals(3)).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
            List<string> sectionlist= dtContent.Rows.Cast<DataRow>().Where(x => x["ModuleId"].Equals(3)).Select(y => Convert.ToString(y["Content"])).ToList();
            for (int i = 0; i < sectionlist.Count-1; i++)
            {
                masterPage.HtmlContent += sectionlist[i];
            }
            masterPage.LanguageCode = culture == null ? "en-US" : culture == "undefined" ? "en-US" : culture;
            List<HomeLabs> homeLabs = _masterService.RetrieveHomeLabs(culture == null ? "en-US" : culture == "undefined" ? "en-US" : culture);
            ViewData["Header"] = dtContent.Rows.Cast<DataRow>().Where(x =>x["ModuleId"].Equals(1)).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
            ViewData["Footer"] = dtContent.Rows.Cast<DataRow>().Where(x =>x["ModuleId"].Equals(2)).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
            masterPage.MetaDescription = Convert.ToString(dtContent.Rows[0]["MetaDescription"]);
            masterPage.MetaTitle = Convert.ToString(dtContent.Rows[0]["MetaTitle"]);
            masterPage.MetaUrl = Convert.ToString(dtContent.Rows[0]["MetaDescription"]);
            //appendLabs = "<div class=\"row helpfulDivs\" style=\"Background-color:#fff;\">";
            //for (int i = 0; i < homeLabs.Count; i++)
            //{
            //    appendLabs += "<div class=\"col-md-4\"><div class=\"animationDiv\">" +
            //            "<img class=\"btnImage\" src=\"" + homeLabs[i].ImagePath + "\">" +
            //            "<div class=\"squaredDiv\" style=\"background-image: url(../Images/Homepage/HomeImages/Frame.svg);\">" +
            //             "<div class=\"squaredText\">" +
            //                 "<p>" + homeLabs[i].Title + "</p>" +
            //             "</div>" +
            //             "<div class=\"MoreDiv\">" +
            //                 "<p>More..</p>" +
            //             "</div>" +
            //       "  </div>" +
            //     "</div>" +
            //" </div>";
            //}
            //appendLabs += "</div>";
            //masterPage.HtmlContent = masterPage.HtmlContent.Replace("<div class=\"row helpfulDivs\"></div>", appendLabs);

            return View(masterPage);
        }

        [AllowAnonymous]
        public IActionResult GetHompageLabOrNewsArticles(string LanguageCode)
        {
            List<HomeLabs> homeLabs = _masterService.RetrieveHomeLabs(LanguageCode);
           return Json(homeLabs, new JsonSerializerSettings());
        }

        [Route("editor")]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
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



        public IActionResult Privacy()
        {
            return View();
        }
        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetContent(int ModuleId, string LanguageCode)
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
        public async Task<IActionResult> UpdateContentByModelIdAndLanguageId(MasterPage masterContent)
        {
            try
            {
                var file = Request.Form.Files.Count() == 0 ? null : Request.Form.Files[0];
                string[] uploadedFiles = new string[] { };
                if (masterContent.FileString != null)
                {
                    uploadedFiles = masterContent.FileString.Split(',');
                }

                if (uploadedFiles.Any(z => z == "file1"))
                {
                    var fileExtension = Path.GetExtension(file.FileName);
                    switch (masterContent.ModuleId)
                    {
                        case 5:

                            if (fileExtension == ".png")
                            {
                                string createpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "hover-over");
                                string path = createpath + "\\" + "HOME-ICONS-white.png";
                                FilePath(file, path, "","");

                            }
                            else
                            {
                                return Json(new { message = "file should be in png format" });
                            }

                            break;
                        case 6:
                            if (fileExtension == ".png")
                            {
                                string createpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "hover-over");
                                string path = createpath + "\\" + "HOME-ICONS-white2.png";
                                FilePath(file, path, "", "");

                            }
                            else
                            {
                                return Json(new { message = "file should be in png format" });
                            }
                            break;

                        case 7:
                            if (fileExtension == ".png")
                            {
                                string createpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "hover-over");
                                string path = createpath + "\\" + "HOME-ICONS-white3.png";
                                FilePath(file, path, "", "");


                            }
                            else
                            {
                                return Json(new { message = "file should be in png format" });
                            }
                            break;
                        case 8:
                            if (fileExtension == ".png")
                            {
                                string createpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "hover-over");
                                string path = createpath + "\\" + "HOME-ICONS-white4.png";
                                FilePath(file, path, "", "");


                            }
                            else
                            {
                                return Json(new { message = "file should be in png format" });
                            }
                            break;
                        case 9:
                            if (fileExtension == ".png")
                            {
                                string createpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "hover-over");
                                string path = createpath + "\\" + "HOME-ICONS-white5.png";
                                FilePath(file, path, "", "");

                            }
                            else
                            {
                                return Json(new { message = "file should be in png format" });
                            }
                            break;
                    }
                }


                if (uploadedFiles.Any(z => z == "file2"))
                {
                    var file1 = Request.Form.Files[Request.Form.Files.Count() > 1 ? 1 : 0];
                    var fileExtension = Path.GetExtension(file1.FileName);

                    switch (masterContent.ModuleId)
                    {
                        case 5:

                            if (fileExtension == ".png")
                            {
                                string createpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "hover-over");
                                string path = createpath + "\\" + "Serv_ind-ICONS-grey.png";
                                FilePath1(file1, path);

                            }
                            else
                            {
                                return Json(new { message = "file should be in png format" });
                            }
                            break;
                        case 6:
                            if (fileExtension == ".png")
                            {
                                string createpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "hover-over");
                                string path = createpath + "\\" + "Serv_ind-ICONS-grey2.png";

                                FilePath1(file1, path);


                            }
                            else
                            {
                                return Json(new { message = "file should be in png format" });
                            }
                            break;

                        case 7:
                            if (fileExtension == ".png")
                            {
                                string createpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "hover-over");
                                string path = createpath + "\\" + "Serv_ind-ICONS-grey3.png";
                                FilePath1(file1, path);


                            }
                            else
                            {
                                return Json(new { message = "file should be in png format" });
                            }

                            break;
                        case 8:
                            if (fileExtension == ".png")
                            {
                                string createpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "hover-over");
                                string path = createpath + "\\" + "Serv_ind-ICONS-grey4.png";
                                FilePath1(file1, path);

                            }
                            else
                            {
                                return Json(new { message = "file should be in png format" });
                            }
                            break;
                        case 9:
                            if (fileExtension == ".png")
                            {
                                string createpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "hover-over");
                                string path = createpath + "\\" + "Serv_ind-ICONS-grey5.png";
                                FilePath1(file1, path);

                            }
                            else
                            {
                                return Json(new { message = "file should be in png format" });
                            }
                            break;
                    }

                }

                MasterPage masterPage = new MasterPage();
                int resultCode = _masterService.UpdateContentByModelIdAndLanguageId(masterContent.ModuleId, masterContent.LanguageCode, masterContent.Content, masterContent.Metatage, masterContent.MetaTitle);
                //if (resultCode > 0)
                //{
                //    masterPage = _masterService.GetModuleContent(masterContent.ModuleId, masterContent.LanguageCode);
                //}
                masterPage.ResultCode = resultCode;
                return Json(masterPage, new JsonSerializerSettings());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //[HttpGet]
        //public IActionResult GetHeaderPageDetails(int ModuleId, string Languagecode)
        //{

        //    MasterPage masterPage = _masterService.GetModuleContent(ModuleId, Languagecode);

        //    return Json(masterPage.HtmlContent, new JsonSerializerSettings());



        //}

        //[HttpGet]
        //public IActionResult GetFooterPageDetails(int ModuleId, string Languagecode)
        //{
        //    MasterPage masterPage = _masterService.GetModuleContent(ModuleId, Languagecode);

        //    return Json(masterPage.HtmlContent, new JsonSerializerSettings());


        //}

        [HttpGet]
        [Route("imagesupload")]
        public IActionResult ImagesUpload()
        {
            return View();
        }
        [HttpPost]
        [Route("image/imagesupload")]
        [ActionName("imagesupload")]
        public IActionResult ImagesUpload_Post(Services model)
        {
            try
            {
                if (model.Customerexperience.FileName != null)
                {
                    string CustomerfileName = model.Customerexperience.FileName;
                    string createpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "hover-over");
                    string path = createpath + "\\" + CustomerfileName;
                    using (var bits = new FileStream(path, FileMode.Create))
                    {
                        model.Customerexperience.CopyToAsync(bits);
                    }
                }
                if (model.Audioproduction.FileName != null)
                {
                    string AudioproductionfileName = model.Customerexperience.FileName;
                    string createpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "hover-over");
                    string path = createpath + "\\" + AudioproductionfileName;
                    using (var bits = new FileStream(path, FileMode.Create))
                    {
                        model.Customerexperience.CopyToAsync(bits);
                    }
                }

                if (model.Qualityassurance.FileName != null)
                {
                    string QualityassurancefileName = model.Customerexperience.FileName;
                    string createpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "hover-over");
                    string path = createpath + "\\" + QualityassurancefileName;
                    using (var bits = new FileStream(path, FileMode.Create))
                    {
                        model.Customerexperience.CopyToAsync(bits);
                    }
                }
                if (model.Localization.FileName != null)
                {
                    string LocalizationfileName = model.Customerexperience.FileName;
                    string createpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "hover-over");
                    string path = createpath + "\\" + LocalizationfileName;
                    using (var bits = new FileStream(path, FileMode.Create))
                    {
                        model.Customerexperience.CopyToAsync(bits);
                    }
                }

                if (model.Engineering.FileName != null)
                {
                    string EngineeringfileName = model.Customerexperience.FileName;
                    string createpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "hover-over");
                    string path = createpath + "\\" + EngineeringfileName;
                    using (var bits = new FileStream(path, FileMode.Create))
                    {
                        model.Customerexperience.CopyToAsync(bits);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return View();
        }

        public IActionResult UploadImage(List<IFormFile> files)
        {
            try
            {
                if (files.Count >= 1)
                {
                    foreach (IFormFile file in files)
                    {
                        string CustomerfileName = file.FileName;
                        string createpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "hover-over");
                        string path = createpath + "\\" + CustomerfileName;

                        using (var bits = new FileStream(path, FileMode.Create))
                        {
                            file.CopyToAsync(bits);
                        }

                    }
                    return Json(new { Message = "Success" }, new JsonSerializerSettings());
                }

                else
                {
                    return Json(new { errorMessage = "Upload Failed" }, new JsonSerializerSettings());
                }


            }
            catch (Exception ex)
            {

            }
            return Json("", new JsonSerializerSettings());
        }

        public async void FilePath(IFormFile file, string path, string deletePath, string backUpPath)
        {
            try
            {
                _loggerManager.LogInfo(string.Format("Method :FilePath Data: file: {0}, path: {1}, deltepath:{2} ", file.Name, path, deletePath));
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
            catch (Exception exception) 
            {
                _loggerManager.LogError("Method :FilePath error:"+ exception.Message);

            }
        }

        public async void FilePath1(IFormFile file1, string path)
        {
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file1.CopyToAsync(stream);
            }

        }
        [HttpPost]
        [AllowAnonymous]
        public IActionResult ContactPost(NewUsers users)
        {
            try
            {
                if (Mails.ValidateEmailID(users.Email))
                {
                    string path = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Views", "Email.html");
                    System.Net.WebRequest objRequest = System.Net.HttpWebRequest.Create(path);

                    System.IO.StreamReader sr = new System.IO.StreamReader(objRequest.GetResponse().GetResponseStream());

                    string htmlBody = sr.ReadToEnd();
                    sr.Close();

                    System.Net.Mail.Attachment inlineLogo = new System.Net.Mail.Attachment(System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images/Logo", "PTW.png"));

                    System.Threading.Thread emailThread = new System.Threading.Thread(delegate ()
                    {
                        Mails.funSendEnquiryMail(users, htmlBody, inlineLogo);
                    });
                    emailThread.IsBackground = true;
                    emailThread.Start();

                    _masterService.UsersContact(users);


                    return Json(new { message = "Request has been sent successfully." }, new JsonSerializerSettings());
                }

                else
                {
                    return Json(new { message = "Invalid EmailId." }, new JsonSerializerSettings());
                }

            }

            catch (Exception ex)
            {
                throw;
            }



        }

       

        //[ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]

        [Route("edithome")]
        [HttpGet]
        public IActionResult Home_New()
        {
            MasterPage masterPage1 = _masterService.GetLanguageandModules();
            Home home = new Home();
            home.Languages = masterPage1.LanguageList;
           
            return View(home);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateHomePageByLanguageId(Home HomeContent)
        {
            try
            {
                

                _loggerManager.LogInfo("Method :UpdateHomePageByLanguageId Data: " + JsonConvert.SerializeObject(HomeContent));
                string path = Path.Combine(_hostingEnvironment.WebRootPath, "images/Homepage/HomeImages/");
                string deletePath = Path.Combine(_hostingEnvironment.WebRootPath, "images/Homepage/PreviewImages/");
                string backUpPath = Path.Combine(_hostingEnvironment.WebRootPath, "images/Homepage/Backup/");
               

                if (HomeContent.PageBackgroundImage != null)
                {
                    FilePath(HomeContent.PageBackgroundImage, path + "PageBackgroundImage.png", deletePath + "PageBackgroundImage.png", backUpPath + Guid.NewGuid() + "_" + "PageBackgroundImage.png");
                }
                if (HomeContent.CustomerExperience != null)
                {
                    FilePath(HomeContent.CustomerExperience, path + "CustomerExperience.png", deletePath + "CustomerExperience.png", backUpPath + Guid.NewGuid() + "_" + "CustomerExperience.png");
                }
                if (HomeContent.Squad != null)
                {
                    FilePath(HomeContent.Squad, path + "Squad.png", deletePath + "Squad.png", backUpPath + Guid.NewGuid()+"_" + "Squad.png");
                }
                if (HomeContent.QualityAssurance != null)
                {
                    FilePath(HomeContent.QualityAssurance, path + "QualityAssurance.png", deletePath + "QualityAssurance.png", backUpPath + Guid.NewGuid() + "_" + "QualityAssurance.png");
                }
               
                if (HomeContent.Localization != null)
                {
                    FilePath(HomeContent.Localization, path + "Localization.png", deletePath + "Localization.png", backUpPath + Guid.NewGuid() + "_" + "Localization.png");
                }

                if (HomeContent.AudioProduction != null)
                {
                    FilePath(HomeContent.AudioProduction, path + "AudioProduction.png", deletePath + "AudioProduction.png", backUpPath + Guid.NewGuid() + "_" + "AudioProduction.png");
                }

                if (HomeContent.ProductDevelopment != null)
                {
                    FilePath(HomeContent.ProductDevelopment, path + "ProductDevelopment.png", deletePath + "ProductDevelopment.png", backUpPath + Guid.NewGuid() + "_" + "ProductDevelopment.png");
                }
                if (HomeContent.LocationMap != null)
                {
                    FilePath(HomeContent.LocationMap, path + "LocationMap.svg", deletePath + "LocationMap.svg", backUpPath + Guid.NewGuid() + "_" + "LocationMap.svg");
                }
                if (HomeContent.KickStartImage != null)
                {
                    FilePath(HomeContent.KickStartImage, path + "KickStartImage.png", deletePath + "KickStartImage.png", backUpPath + Guid.NewGuid() + "_" + "KickStartImage.png");
                }
                if (HomeContent.CareersImage != null)
                {
                    FilePath(HomeContent.CareersImage, path + "CareersImage.svg", deletePath + "CareersImage.svg", backUpPath + Guid.NewGuid() + "_" + "CareersImage.svg");
                }
                if (HomeContent.Frame != null)
                {
                    FilePath(HomeContent.Frame, path + "Frame.svg", deletePath + "Frame.svg", backUpPath + Guid.NewGuid() + "_" + "Frame.svg");
                }

                //int resultCode = _masterService.UpdateHomePageByLanguageId(HomeContent.ModuleId, HomeContent.LanguageCode, HomeContent.Description, HomeContent.MetaDescription, HomeContent.MetaTitle, HomeContent.MetaUrl);
                int resultCode = _masterService.UpdateSectionContent(HomeContent.SectionId, HomeContent.ModuleId, HomeContent.LanguageCode, HomeContent.Description, HomeContent.MetaDescription, HomeContent.MetaTitle, HomeContent.MetaUrl);

                return Json(resultCode, new JsonSerializerSettings());
            }
            catch (Exception ex)
            {
                _loggerManager.LogError("Method:  UpdateHomePageByLanguageId: " + ex.Message);

                throw ex;
            }
        }


        [HttpPost]
        public async Task<IActionResult> PreviewHome(Home HomeContent)
        {
            string path = Path.Combine(_hostingEnvironment.WebRootPath, "images/Homepage/PreviewImages/");

            if (HomeContent.PageBackgroundImage != null)
            {
                HomeContent.Description = HomeContent.Description.Replace("/HomeImages/PageBackgroundImage.png", "/PreviewImages/PageBackgroundImage.png");
            }
            if (HomeContent.CustomerExperience != null)
            {
                HomeContent.Description = HomeContent.Description.Replace("/HomeImages/CustomerExperience.png", "/PreviewImages/CustomerExperience.png");
            }
            if (HomeContent.Squad != null)
            {
                HomeContent.Description = HomeContent.Description.Replace("/HomeImages/Squad.png", "/PreviewImages/Squad.png");
            }
            if (HomeContent.QualityAssurance != null)
            {
                HomeContent.Description = HomeContent.Description.Replace("/HomeImages/QualityAssurance.png", "/PreviewImages/QualityAssurance.png");
            }
            if (HomeContent.Localization != null)
            {
                HomeContent.Description = HomeContent.Description.Replace("/HomeImages/Localization.png", "/PreviewImages/Localization.png");
            }
            if (HomeContent.AudioProduction != null)
            {
                HomeContent.Description = HomeContent.Description.Replace("/HomeImages/AudioProduction.png", "/PreviewImages/AudioProduction.png");
            }
            if (HomeContent.LocationMap != null)
            {
                HomeContent.Description = HomeContent.Description.Replace("/HomeImages/LocationMap.svg", "/PreviewImages/LocationMap.svg");
            }
            if (HomeContent.ProductDevelopment != null)
            {
                HomeContent.Description = HomeContent.Description.Replace("/HomeImages/ProductDevelopment.png", "/PreviewImages/ProductDevelopment.png");
            }
            if (HomeContent.ProductDevelopmentOnMouseHover != null)
            {
                HomeContent.Description = HomeContent.Description.Replace("/HomeImages/ProductDevelopmentOnMouseHover.png", "/PreviewImages/ProductDevelopmentOnMouseHover.png");
            }
            if (HomeContent.KickStartImage != null)
            {
                HomeContent.Description = HomeContent.Description.Replace("/HomeImages/KickStartImage.png", "/PreviewImages/KickStartImage.png");
            }
            if (HomeContent.CareersImage != null)
            {
                HomeContent.Description = HomeContent.Description.Replace("/HomeImages/CareersImage.svg", "/PreviewImages/CareersImage.svg");
            }
            if (HomeContent.Frame != null)
            {
                HomeContent.Description = HomeContent.Description.Replace("/HomeImages/Frame.svg", "/PreviewImages/Frame.svg");
            }

            int resultCode = _masterService.UpdatePreviewContentByLanguageModuleId(HomeContent.SectionId,HomeContent.ModuleId, HomeContent.LanguageCode, HomeContent.Description, HomeContent.MetaDescription, HomeContent.MetaTitle, HomeContent.MetaUrl);

            return Json(resultCode, new JsonSerializerSettings());
        }

        [HttpPost]
        public async Task<IActionResult> UpdateImagesToPreview(Home HomeContent)
        {
            _loggerManager.LogInfo("Method :UpdateImagesToPreview Data: " + JsonConvert.SerializeObject(HomeContent));
            string path = Path.Combine(_hostingEnvironment.WebRootPath, "images/Homepage/PreviewImages/");

            try
            {
                if (HomeContent.PageBackgroundImage != null && HomeContent.Filename == "PageBackgroundImage")
                {
                    FilePath(HomeContent.PageBackgroundImage, path + "PageBackgroundImage.png", "","");
                }
                if (HomeContent.CustomerExperience != null && HomeContent.Filename == "CustomerExperience")
                {
                    FilePath(HomeContent.CustomerExperience, path + "CustomerExperience.png", "","");
                }
                if (HomeContent.Squad != null && HomeContent.Filename == "Squad")
                {
                    FilePath(HomeContent.Squad, path + "Squad.png", "","");
                }
                if (HomeContent.QualityAssurance != null && HomeContent.Filename == "QualityAssurance")
                {
                    FilePath(HomeContent.QualityAssurance, path + "QualityAssurance.png", "","");
                }
                
                if (HomeContent.Localization != null && HomeContent.Filename == "Localization")
                {
                    FilePath(HomeContent.Localization, path + "Localization.png", "","");
                }
              
                if (HomeContent.AudioProduction != null && HomeContent.Filename == "AudioProduction")
                {
                    FilePath(HomeContent.AudioProduction, path + "AudioProduction.png", "","");
                }
               
                if (HomeContent.ProductDevelopment != null && HomeContent.Filename == "ProductDevelopment")
                {
                    FilePath(HomeContent.ProductDevelopment, path + "ProductDevelopment.png", "","");
                }
               
                if (HomeContent.LocationMap != null && HomeContent.Filename == "LocationMap")
                {
                    FilePath(HomeContent.LocationMap, path + "LocationMap.svg", "", "");
                }
                if (HomeContent.KickStartImage != null && HomeContent.Filename == "KickStartImage")
                {
                    FilePath(HomeContent.KickStartImage, path + "KickStartImage.png", "","");
                }
                if (HomeContent.CareersImage != null && HomeContent.Filename == "CareersImage")
                {
                    FilePath(HomeContent.CareersImage, path + "CareersImage.svg", "", "");
                }
                if (HomeContent.Frame != null && HomeContent.Filename == "Frame")
                {
                    FilePath(HomeContent.Frame, path + "Frame.svg", "", "");
                }
            }
            catch (Exception exception)
            {
                _loggerManager.LogError("Method:  UpdateImagesToPreview: " + exception.Message);

                throw;
            }

            return Json(1, new JsonSerializerSettings());
        }


        //News CMS updation
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpPost]
        public async Task<IActionResult> UpdateNewsPageByLanguageId(News News)
        {
            try
            {
                _loggerManager.LogInfo("Method :UpdateNewsPageByLanguageId Data: " + JsonConvert.SerializeObject(News));
                string path = Path.Combine(_hostingEnvironment.WebRootPath, "images/News/NewsImages/");
                string deletePath = Path.Combine(_hostingEnvironment.WebRootPath, "images/News/PreviewImages/");
                string BackupPath = Path.Combine(_hostingEnvironment.WebRootPath, "images/News/BackupImages/");


                if (News.NewsImage != null)
                {
                    FilePath(News.NewsImage, path + "NewsImage.png", deletePath + "NewsImage.png", BackupPath);
                }

                int resultCode = _masterService.UpdateHomePageByLanguageId(News.ModuleId, News.LanguageCode, News.Description, News.MetaDescription, News.MetaTitle, News.MetaUrl);

                return Json(resultCode, new JsonSerializerSettings());
            }
            catch (Exception ex)
            {
                _loggerManager.LogError("Method:  UpdateNewsPageByLanguageId: " + ex.Message);

                throw ex;
            }
        }

        //Labs CMS updation
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpPost]
        public async Task<IActionResult> UpdateLabsPageByLanguageId(Labs Labs)
        {
            try
            {
                _loggerManager.LogInfo("Method :UpdateLabsPageByLanguageId Data: " + JsonConvert.SerializeObject(Labs));
                string path = Path.Combine(_hostingEnvironment.WebRootPath, "images/Lab/LabImages/");
                string deletePath = Path.Combine(_hostingEnvironment.WebRootPath, "images/Lab/PreviewImages/");
                string BackupPath = Path.Combine(_hostingEnvironment.WebRootPath, "images/Lab/BackupImages/");


                if (Labs.LabImage != null)
                {
                    FilePath(Labs.LabImage, path + "LabImage.png", deletePath + "LabImage.png", BackupPath);
                }
                if (Labs.LabHeader != null)
                {
                    FilePath(Labs.LabHeader, path + "LabHeader.png", deletePath + "LabHeader.png", BackupPath);
                }

                int resultCode = _masterService.UpdateHomePageByLanguageId(Labs.ModuleId, Labs.LanguageCode, Labs.Description, Labs.MetaDescription, Labs.MetaTitle, Labs.MetaUrl);

                return Json(resultCode, new JsonSerializerSettings());
            }
            catch (Exception ex)
            {
                _loggerManager.LogError("Method:  UpdateLabsPageByLanguageId: " + ex.Message);

                throw ex;
            }
        }

        [Route("downloadbackup")]
        [HttpGet]
        public IActionResult DownloadBackup(int ModuleId,string LanguageCode,int SectionId, string fileName)
        {
            
            string backUpPath = Path.Combine(_hostingEnvironment.WebRootPath, "images/Homepage/Backup/");
            //string filePath = "home" + Guid.NewGuid() + ".html";
            string filePath = fileName+"_" + Guid.NewGuid() + ".html";
            //MasterPage master = _masterService.GetModuleContentById(ModuleId, LanguageCode);
            string htmlContent = _masterService.GetSubModuleContent(ModuleId, LanguageCode, SectionId);
            System.IO.File.WriteAllText(backUpPath+filePath, htmlContent);
            return Json(filePath, new JsonSerializerSettings());
        }

    
        [Route("GetSections")]
        [HttpGet]
        public IActionResult Getsections(int ModuleId, string LanguageCode)
        {
            List<Sections> SectionList = _masterService.GetSubModuleList(ModuleId, LanguageCode);
            return Json(SectionList, new JsonSerializerSettings());
        }

        [Route("GetSubContent")]
        [HttpGet]
        public IActionResult GetSubContent(int ModuleId, string LanguageCode, int SectionId)
        {
            string SectionList = _masterService.GetSubModuleContent(ModuleId, LanguageCode, SectionId);
            return Json(SectionList, new JsonSerializerSettings());
        }

        [Route("GetSEODetails")]
        [HttpGet]
        public IActionResult GetSEODetails(int ModuleId, string LanguageCode, int SectionId)
        {
            MasterPage masterPage = _masterService.GetSEODetails(ModuleId, LanguageCode, SectionId);
            return Json(masterPage, new JsonSerializerSettings());
        }

    }
}
