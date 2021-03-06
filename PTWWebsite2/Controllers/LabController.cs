﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Design.Internal;
using MySqlX.XDevAPI;
using Newtonsoft.Json;
using PTW.DataAccess.Models;
using PTW.DataAccess.Services;
using Renci.SshNet;
using System.Security.Claims;
using LoggerService;

namespace PTWWebsite2.Controllers
{

    public class LabController : Controller
    {
        private readonly ILabService _LabEventService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IMasterService _masterService;
        private readonly ILoggerManager _loggerManager;

        public LabController(ILabService LabEventService, IHostingEnvironment hostingEnvironment, IMasterService masterService, ILoggerManager loggerManager)
        {
            _LabEventService = LabEventService;
            _hostingEnvironment = hostingEnvironment;
            _masterService = masterService;
            _loggerManager = loggerManager;
        }

        [Route("lab")]
        [Route("{culture}/lab")]
        public IActionResult Index(string culture = "en-US")
        {
            MasterPage masterPage = new MasterPage();
            LabsEvents LabsEvents = new LabsEvents();
            LabsEvents.allLabs = _LabEventService.GetAllLabsDetails(culture);
            //  DataTable dtContent = _masterService.GetModuleContent("Labs", (culture == null ? "en-US" : culture));
            DataTable dtContent = _masterService.GetModuleContentSectionwise(19, (culture == null ? "en-US" : culture == "undefined" ? "en-US" : culture));
            List<string> sectionlist = dtContent.Rows.Cast<DataRow>().Where(x => x["ModuleId"].Equals(19)).Select(y => Convert.ToString(y["Content"])).ToList();
            for (int i = 0; i < sectionlist.Count-1; i++)
            {
                LabsEvents.HtmlContent += sectionlist[i];
            }
            ViewData["Header"] = dtContent.Rows.Cast<DataRow>().Where(x => x["ModuleId"].Equals(1)).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
            ViewData["Footer"] = dtContent.Rows.Cast<DataRow>().Where(x => x["ModuleId"].Equals(2)).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
            LabsEvents.labInsights = _LabEventService.GetAllLatestInsights(culture);
            LabsEvents.Labs = _LabEventService.GetSlider();
            //LabsEvents.HtmlContent= dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Labs")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
           // GetServicecontent(culture);
            return View(LabsEvents);


        }

        [Route("labs")]
        [Route("{culture}/labs")]
        public IActionResult Labs(string culture = "en-US")
        {
            LabsEvents LabsEvents = new LabsEvents();
            LabsEvents.allLabs = _LabEventService.GetAllLabsDetails(culture);
            LabsEvents.labInsights = _LabEventService.GetAllLatestInsights(culture);

            return Json(LabsEvents, new JsonSerializerSettings());
        }

        public IActionResult GetServicecontent(string culture)
        {
            MasterPage masterPage = new MasterPage();
            DataTable dtContent = _masterService.GetModuleContent("LAB", (culture == null ? "en-US" : culture));
            masterPage.HtmlContent = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("LAB")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
            ViewData["Header"] = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Header")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
            ViewData["Footer"] = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Footer")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
            return View(masterPage);
        }
        [Route("lab/{LabShortDescription}")]
        [Route("{culture}/lab/{LabShortDescription}")]
        public IActionResult LabArticles(string LabShortDescription, string culture = "en-US")
        {
            LabsEvents LabsEvents = new LabsEvents();
            List<string> ShortDescription = LabShortDescription.Split('-').ToList();
            if (ShortDescription.Contains("Article") || ShortDescription.Contains("Case Study") || ShortDescription.Contains("Campaign Article"))
            {
                ShortDescription.RemoveAt(0);
                string strShortDescription = string.Join(" ", ShortDescription);
                if (ShortDescription.Count > 0)
                {
                    //string culture = "en-US";                
                    LabsEvents.LabArticledetails = _LabEventService.GetLabsArticleDetails(strShortDescription);
                    LabsEvents.FutureLabArticles = _LabEventService.GetFutureLabArticles(strShortDescription);
                }
                else
                {
                    LabsEvents.LabArticledetails = _LabEventService.GetLabsArticleDetails(LabShortDescription);
                    LabsEvents.FutureLabArticles = _LabEventService.GetFutureLabArticles(LabShortDescription);
                }
            }
            else
            {
                string strShortDescription = string.Join(" ", ShortDescription);
                LabsEvents.LabArticledetails = _LabEventService.GetLabsArticleDetails(strShortDescription);
                LabsEvents.FutureLabArticles = _LabEventService.GetFutureLabArticles(strShortDescription);

            }



            GetServicecontent(culture);
            return View(LabsEvents);
        }


        [Authorize]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        [Route("addlabs")]
        public IActionResult AddLabArticles()
        {
            Labs labs = new Labs();            
            GetLabs(labs);          
            return View(labs);
        }


        [Route("lab/{LabShortDescription}")]
        [HttpPost]
        public IActionResult AddUpdateLabs(Labs labs)
        {
            bool result = false;
            try
            {
                string uniqueFileName = string.Empty;
                string MobileImage = string.Empty;
                string DesktopImage = string.Empty;
                string TabImageHorizental = string.Empty;
                string TabImageVertical = string.Empty;
                string Readmore = string.Empty;
                string Labtypeimg = string.Empty;
                string Mobileurl = string.Empty;

                //var data = (Labs)ViewData["Uplabdetails"];
                


                //MobileImage = Path.Combine(_hostingEnvironment.WebRootPath, "images/Lab/" + Labtypeimg + "/376");
                //DesktopImage = Path.Combine(_hostingEnvironment.WebRootPath, "images/Lab/" + Labtypeimg + "/1920");
                //TabImageHorizental = Path.Combine(_hostingEnvironment.WebRootPath, "images/Lab/" + Labtypeimg + "/1112");
                //TabImageVertical = Path.Combine(_hostingEnvironment.WebRootPath, "images/Lab/" + Labtypeimg + "/834");
                //Readmore = Path.Combine(_hostingEnvironment.WebRootPath, "images/Lab/" + Labtypeimg + "/320");

                if (labs.LabTypeId == 1)
                {
                    Labtypeimg = "Articles";

                }
                else
                {
                    Labtypeimg = "CaseStudies";

                }

                if (labs.LabId == 0)
                {                  

                    if (Request.Form.Files.Count() > 0)
                    {
                        string[] uploadedFiles = new string[] { };
                        if (labs.FileString != null)
                        {
                            uploadedFiles = labs.FileString.Split(',');
                        }
                        #region FileUpload
                        if (uploadedFiles.Any(z => z == "file1"))
                        {
                            var file1 = Request.Form.Files.Count() == 0 ? null : Request.Form.Files[0];
                            var fileExtension = file1.FileName.Substring(file1.FileName.LastIndexOf('.') + 1);
                            if (fileExtension == "png")
                            {
                                DesktopImage = Path.Combine(_hostingEnvironment.WebRootPath, "images/Lab/" + Labtypeimg + "/1920");

                                labs.DesktopImageUrl = "/Images/Lab/" + Labtypeimg + "/1920/" + file1.FileName + "";
                                labs.DesktopName = file1.FileName;
                                string filePath1 = Path.Combine(DesktopImage, labs.DesktopName);
                                FilePath1(file1, filePath1);
                            }
                            else
                            {
                                return Json(new { message = "file should be in png format" });
                            }
                        }
                        if (uploadedFiles.Any(z => z == "file2"))
                        {
                            var file2 = Request.Form.Files.Count() == 0 ? null : Request.Form.Files[1];

                            var fileExtension = file2.FileName.Substring(file2.FileName.LastIndexOf('.') + 1);
                            if (fileExtension == "png")
                            {
                                TabImageHorizental = Path.Combine(_hostingEnvironment.WebRootPath, "images/Lab/" + Labtypeimg + "/1112");
                                labs.TabImageNameHorizondaUrl = "/Images/Lab/" + Labtypeimg + "/1112/" + file2.FileName + "";
                                labs.TabImgHorizonalname = file2.FileName;
                                string filePath2 = Path.Combine(TabImageHorizental, labs.TabImgHorizonalname);
                                FilePath1(file2, filePath2);
                            }
                            else
                            {
                                return Json(new { message = "file should be in png format" });
                            }
                        }
                        if (uploadedFiles.Any(z => z == "file3"))
                        {
                            var file3 = Request.Form.Files.Count() == 0 ? null : Request.Form.Files[2];

                            var fileExtension = file3.FileName.Substring(file3.FileName.LastIndexOf('.') + 1);
                            if (fileExtension == "png")
                            {
                                TabImageVertical = Path.Combine(_hostingEnvironment.WebRootPath, "images/Lab/" + Labtypeimg + "/834");
                                labs.TabImageNamVerticalUrl = "/Images/Lab/" + Labtypeimg + "/834/" + file3.FileName + "";
                                labs.TabImgVerticalname = file3.FileName;
                                string filePath3 = Path.Combine(TabImageVertical, labs.TabImgVerticalname);
                                FilePath1(file3, filePath3);
                            }
                            else
                            {
                                return Json(new { message = "file should be in png format" });
                            }
                        }
                        if (uploadedFiles.Any(z => z == "file4"))
                        {
                            var file4 = Request.Form.Files.Count() == 0 ? null : Request.Form.Files[3];

                            var fileExtension = file4.FileName.Substring(file4.FileName.LastIndexOf('.') + 1);
                            if (fileExtension == "png")
                            {
                                MobileImage = Path.Combine(_hostingEnvironment.WebRootPath, "images/Lab/" + Labtypeimg + "/376");

                                labs.ImageUrl = "/Images/Lab/" + Labtypeimg + "/376/";
                                labs.ImageName = file4.FileName;

                                labs.MobileImageNameUrl = "/Images/Lab/" + Labtypeimg + "/376/" + file4.FileName + "";
                                labs.MobileName = file4.FileName;
                                string filePath4 = Path.Combine(MobileImage, labs.MobileName);
                                FilePath1(file4, filePath4);
                            }
                            else
                            {
                                return Json(new { message = "file should be in png format" });
                            }

                        }
                        if (uploadedFiles.Any(z => z == "file5"))
                        {
                            var file5 = Request.Form.Files.Count() == 0 ? null : Request.Form.Files[4];

                            var fileExtension = file5.FileName.Substring(file5.FileName.LastIndexOf('.') + 1);
                            if (fileExtension == "png")
                            {
                                Readmore = Path.Combine(_hostingEnvironment.WebRootPath, "images/Lab/" + Labtypeimg + "/320");

                                labs.ReadMoreUrl = "/Images/Lab/" + Labtypeimg + "/320/" + file5.FileName + "";
                                labs.ReadMorename = file5.FileName;
                                string filePath5 = Path.Combine(Readmore, labs.ReadMorename);
                                FilePath1(file5, filePath5);
                            }
                            else
                            {
                                return Json(new { message = "file should be in png format" });
                            }

                        }
                        #endregion
                        #region FileUpload Existing

                        //labs.ImageUrl = "/Images/Lab/" + Labtypeimg + "/376/";
                        //labs.ImageName = labs.MobileImage.FileName;

                        //labs.MobileImageNameUrl = "/Images/Lab/" + Labtypeimg + "/376/" + labs.MobileImage.FileName + "";
                        //labs.MobileName = labs.MobileImage.FileName;

                        //string filePath = Path.Combine(MobileImage, labs.MobileImage.FileName);
                        //labs.MobileImage.CopyTo(new FileStream(filePath, FileMode.Create));


                        //labs.DesktopImageUrl = "/Images/Lab/" + Labtypeimg + "/1920/" + labs.DesktopImage.FileName + "";
                        //labs.DesktopName = labs.DesktopImage.FileName;
                        //string filePath2 = Path.Combine(DesktopImage, labs.DesktopImage.FileName);
                        //labs.DesktopImage.CopyTo(new FileStream(filePath2, FileMode.Create));

                        //labs.TabImageNameHorizondaUrl = "/Images/Lab/" + Labtypeimg + "/1112/" + labs.TabImageHorizonal.FileName + "";
                        //labs.TabImgHorizonalname = labs.TabImageHorizonal.FileName;
                        //string filePath3 = Path.Combine(TabImageHorizental, labs.TabImageHorizonal.FileName);
                        //labs.TabImageHorizonal.CopyTo(new FileStream(filePath3, FileMode.Create));

                        //labs.TabImageNamVerticalUrl = "/Images/Lab/" + Labtypeimg + "/834/" + labs.TabImageNamVertical.FileName + "";
                        //labs.TabImgVerticalname = labs.TabImageNamVertical.FileName;
                        //string filePath4 = Path.Combine(TabImageVertical, labs.TabImageNamVertical.FileName);
                        //labs.TabImageNamVertical.CopyTo(new FileStream(filePath4, FileMode.Create));

                        //labs.ReadMoreUrl = "/Images/Lab/" + Labtypeimg + "/320/" + labs.ReadMore.FileName + "";
                        //labs.ReadMorename = labs.ReadMore.FileName;
                        //string filePath5 = Path.Combine(Readmore, labs.ReadMore.FileName);
                        //labs.ReadMore.CopyTo(new FileStream(filePath5, FileMode.Create));
                        #endregion
                    }

                    string labsXmlData = CustomNewsXml(labs);
                    result = _LabEventService.AddUpdateLabs(labsXmlData, labs.Description);
                  // ViewBag.IsAddedSuccessfully = result;                   
                }
                else
                {
                    string labsXmlData = CustomNewsXml(labs);
                    result = _LabEventService.UpdateLabs(labs.LabId, labsXmlData, labs.Description, labs.LanguageCode="en-US");
                   
                }

                return Json(Convert.ToInt32(result), new JsonSerializerSettings());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [NonAction]
        public string CustomNewsXml(Labs labs)
        {
            StringBuilder xml = new StringBuilder();
            xml.Append("<Labs>");
            if (labs != null)
            {

                xml.Append(string.Format("<ServiceTypeId>{0}</ServiceTypeId>", labs.ServiceTypeId));
                xml.Append(string.Format("<Name>{0}</Name>", labs.Name));
                //xml.Append(string.Format("<Description>{0}</Description>", labs.Description));
                xml.Append(string.Format("<ShortDescription>{0}</ShortDescription>", labs.ShortDescription.Replace("-", " ")));
                xml.Append(string.Format("<ImageName>{0}</ImageName>", labs.ImageName));
                xml.Append(string.Format("<ImageUrl>{0}</ImageUrl>", labs.ImageUrl));
                xml.Append(string.Format("<Topic>{0}</Topic>", labs.Topic));

                xml.Append(string.Format("<OnNewWebsiteNow>{0}</OnNewWebsiteNow>", Convert.ToInt32(labs.OnNewWebsiteNow)));
                xml.Append(string.Format("<SuitedForHomePage>{0}</SuitedForHomePage>", Convert.ToInt32(labs.SuitedForHomePage)));
                xml.Append(string.Format("<CustomerExperience>{0}</CustomerExperience>", Convert.ToInt32(labs.CustomerExperience)));
                xml.Append(string.Format("<QualityAssurance>{0}</QualityAssurance>", Convert.ToInt32(labs.QualityAssurance)));
                xml.Append(string.Format("<Localization>{0}</Localization>", Convert.ToInt32(labs.Localization)));
                xml.Append(string.Format("<AudioProduction>{0}</AudioProduction>", Convert.ToInt32(labs.AudioProduction)));
                xml.Append(string.Format("<Engineering>{0}</Engineering>", Convert.ToInt32(labs.Engineering)));
                xml.Append(string.Format("<Campaign>{0}</Campaign>", Convert.ToInt32(labs.Campaign)));

                xml.Append(string.Format("<ProductDevelopment>{0}</ProductDevelopment>", Convert.ToInt32(labs.ProductDevelopment)));
                xml.Append(string.Format("<TalentSolution>{0}</TalentSolution>", Convert.ToInt32(labs.TalentSolution)));
                xml.Append(string.Format("<PlayerSupport>{0}</PlayerSupport>", Convert.ToInt32(labs.PlayerSupport)));
                xml.Append(string.Format("<SpeechTech>{0}</SpeechTech>", Convert.ToInt32(labs.SpeechTech)));
                xml.Append(string.Format("<LocalizationQA>{0}</LocalizationQA>", Convert.ToInt32(labs.LocalizationQA)));

                xml.Append(string.Format("<DesktopImageUrl>{0}</DesktopImageUrl>", labs.DesktopImageUrl));
                xml.Append(string.Format("<TabImageNameHorizondaUrl>{0}</TabImageNameHorizondaUrl>", labs.TabImageNameHorizondaUrl));
                xml.Append(string.Format("<TabImageNamVerticalUrl>{0}</TabImageNamVerticalUrl>", labs.TabImageNamVerticalUrl));
                xml.Append(string.Format("<MobileImageNameUrl>{0}</MobileImageNameUrl>", labs.MobileImageNameUrl));
                xml.Append(string.Format("<ReadMoreUrl>{0}</ReadMoreUrl>", labs.ReadMoreUrl));

                //xml.Append(string.Format("<ShortOrder>{0}</ShortOrder>", labs.ShortOrder));
                xml.Append(string.Format("<LabType>{0}</LabType>", labs.LabType));
                xml.Append(string.Format("<StartDate>{0}</StartDate>", labs.StartDate != "" ? Convert.ToDateTime(labs.StartDate) : DateTime.Now));
                xml.Append(string.Format("<EndDate>{0}</EndDate>", labs.EndDate != "" ? Convert.ToDateTime(labs.StartDate) :DateTime.Now));

                xml.Append(string.Format("<MetaTitle>{0}</MetaTitle>", labs.MetaTitle));
                xml.Append(string.Format("<MetaDescription>{0}</MetaDescription>", labs.MetaDescription));
                xml.Append(string.Format("<IsActive>{0}</IsActive>", Convert.ToInt32(labs.ActiveStatus)));
                xml.Append(string.Format("<CreatedBy>{0}</CreatedBy>", 1));
            }
            xml.Append("</Labs>");
            return xml.ToString();
        }

        //[Route("LAB/{LabShortDescription}")]
        //[Route("{culture}/LAB/{LabShortDescription}")]
        public IActionResult LabCampaignArticles(string LabShortDescription)
        {
            LabsEvents LabsEvents = new LabsEvents();
            LabsEvents.LabArticledetails = _LabEventService.GetLabCampaignArticleDetails("game localizatio strategy");

            return View(LabsEvents);
        }
        public async void FilePath(IFormFile file, string path)
        {
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
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

        //[Route("EditLabs/GetLabs")]
        //[HttpPost]
        public IActionResult GetLabs(Labs labs)
        {
            MasterPage masterPage1 = _masterService.GetLanguageandModules();            
            Labs ddlLabsTitles = _LabEventService.GetAllLabsForUpdate();           
            labs.LabListUpdate = ddlLabsTitles.LabListUpdate;
            labs.Languages = masterPage1.LanguageList;            

            return View("AddLabArticles", labs);
        }
        //[Route("LAB/{LabTitle}")]
        [HttpPost]
        public IActionResult UpdateLabs(Labs labs)
        {
            try
            {
                MasterPage masterPage1 = _masterService.GetLanguageandModules();                
                Labs labslist = _LabEventService.GetAllLabsForUpdate();                

                string uniqueFileName = string.Empty;
                string MobileImage = string.Empty;
                string DesktopImage = string.Empty;
                string TabImageHorizental = string.Empty;
                string TabImageVertical = string.Empty;
                string Readmore = string.Empty;
                string Labtypeimg = string.Empty;
                string Mobileurl = string.Empty;
                MobileImage = Path.Combine(_hostingEnvironment.WebRootPath, "images/Lab/" + Labtypeimg + "/376");
                DesktopImage = Path.Combine(_hostingEnvironment.WebRootPath, "images/Lab/" + Labtypeimg + "/1920");
                TabImageHorizental = Path.Combine(_hostingEnvironment.WebRootPath, "images/Lab/" + Labtypeimg + "/1112");
                TabImageVertical = Path.Combine(_hostingEnvironment.WebRootPath, "images/Lab/" + Labtypeimg + "/834");
                Readmore = Path.Combine(_hostingEnvironment.WebRootPath, "images/Lab/" + Labtypeimg + "/320");
                if (labs.LabTypeId == 0)
                {
                    Labtypeimg = "Articles";

                }
                else
                {
                    Labtypeimg = "CaseStudies";

                }
                var file = Request.Form.Files.Count() == 0 ? null : Request.Form.Files[0];

                if (Request.Form.Files.Count() > 0)
                {
                    string[] uploadedFiles = new string[] { };
                    if (labs.FileString != null)
                    {
                        uploadedFiles = labs.FileString.Split(',');
                    }
                    if (uploadedFiles.Any(z => z == "file1"))
                    {

                        var fileExtension = file.FileName.Substring(file.FileName.LastIndexOf('.') + 1);
                        if (fileExtension == "png")
                        {
                            DesktopImage = Path.Combine(_hostingEnvironment.WebRootPath, "images/Lab/" + Labtypeimg + "/1920");

                            labs.DesktopImageUrl = "/Images/Lab/" + Labtypeimg + "/1920/" + file.FileName + "";
                            labs.DesktopName = file.FileName;
                            string filePath2 = Path.Combine(DesktopImage, labs.DesktopName);
                        }
                        else
                        {
                            return Json(new { message = "file should be in png format" });
                        }
                    }
                    if (uploadedFiles.Any(z => z == "file2"))
                    {
                        var file1 = Request.Form.Files.Count() == 0 ? null : Request.Form.Files[1];

                        var fileExtension = file1.FileName.Substring(file1.FileName.LastIndexOf('.') + 1);
                        if (fileExtension == "png")
                        {
                            TabImageHorizental = Path.Combine(_hostingEnvironment.WebRootPath, "images/Lab/" + Labtypeimg + "/1112");
                            labs.TabImageNameHorizondaUrl = "/Images/Lab/" + Labtypeimg + "/1112/" + file1.FileName + "";
                            labs.TabImgHorizonalname = file1.FileName;
                            string filePath3 = Path.Combine(TabImageHorizental, labs.TabImgHorizonalname);
                        }
                        else
                        {
                            return Json(new { message = "file should be in png format" });
                        }
                    }
                    if (uploadedFiles.Any(z => z == "file3"))
                    {
                        var file2 = Request.Form.Files.Count() == 0 ? null : Request.Form.Files[2];

                        var fileExtension = file2.FileName.Substring(file2.FileName.LastIndexOf('.') + 1);
                        if (fileExtension == "png")
                        {
                            TabImageVertical = Path.Combine(_hostingEnvironment.WebRootPath, "images/Lab/" + Labtypeimg + "/834");
                            labs.TabImageNamVerticalUrl = "/Images/Lab/" + Labtypeimg + "/834/" + file2.FileName + "";
                            labs.TabImgVerticalname = file2.FileName;
                            string filePath4 = Path.Combine(TabImageVertical, labs.TabImgVerticalname);
                        }
                        else
                        {
                            return Json(new { message = "file should be in png format" });
                        }
                    }
                    if (uploadedFiles.Any(z => z == "file4"))
                    {
                        var file3 = Request.Form.Files.Count() == 0 ? null : Request.Form.Files[3];

                        var fileExtension = file3.FileName.Substring(file3.FileName.LastIndexOf('.') + 1);
                        if (fileExtension == "png")
                        {
                            MobileImage = Path.Combine(_hostingEnvironment.WebRootPath, "images/Lab/" + Labtypeimg + "/376");

                            labs.ImageUrl = "/Images/Lab/" + Labtypeimg + "/376/";
                            labs.ImageName = file3.FileName;

                            labs.MobileImageNameUrl = "/Images/Lab/" + Labtypeimg + "/376/" + file3.FileName + "";
                            labs.MobileName = file3.FileName;
                            string filePath = Path.Combine(MobileImage, labs.MobileName);
                        }
                        else
                        {
                            return Json(new { message = "file should be in png format" });
                        }

                    }
                    if (uploadedFiles.Any(z => z == "file5"))
                    {
                        var file4 = Request.Form.Files.Count() == 0 ? null : Request.Form.Files[4];

                        var fileExtension = file4.FileName.Substring(file4.FileName.LastIndexOf('.') + 1);
                        if (fileExtension == "png")
                        {
                            Readmore = Path.Combine(_hostingEnvironment.WebRootPath, "images/Lab/" + Labtypeimg + "/320");

                            labs.ReadMoreUrl = "/Images/Lab/" + Labtypeimg + "/320/" + file4.FileName + "";
                            labs.ReadMorename = file4.FileName;
                            string filePath5 = Path.Combine(Readmore, labs.ReadMorename);
                        }
                        else
                        {
                            return Json(new { message = "file should be in png format" });
                        }

                    }
                }
                string labsXmlData = CustomNewsXml(labs);
                bool result = _LabEventService.UpdateLabs(labs.LabId, labsXmlData, labs.Description, labs.LanguageCode);
                ViewBag.IsAddedSuccessfully = result;

                labslist.Languages = masterPage1.LanguageList;
                labslist.LabId = labs.LabId;//ddl selected value
                labslist.LanguageCode = labs.LanguageCode;

                return View("AddLabArticles", labs);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        [Route("lab/{LabId}/{LanguageCode}-getlabdata")]
        public IActionResult GetLabdata(int LabId, string LanguageCode)
        {
            try
            {
                Labs labs = new Labs();
                //MasterPage masterPage1 = _masterService.GetLanguageandModules();               
               // Labs ddlLabsTitles = _LabEventService.GetAllLabsForUpdate();
                 labs = _LabEventService.GetLabsDetailsByLabId(LabId, LanguageCode );
              
                labs.EditLabId = LabId.ToString();

                return Json(labs);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [Authorize]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        [Route("editlab")]
        public IActionResult EditLab()
        {
            Labs labs = new Labs();
            MasterPage masterPage1 = _masterService.GetLanguageandModules();
            Labs ddlLabsTitles = _LabEventService.GetAllLabsForUpdate();
            labs.LabListUpdate = ddlLabsTitles.LabListUpdate;
            labs.Languages = masterPage1.LanguageList;
            return View(labs);
        }



        //[Authorize]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        [Route("labsdashboard")]
        [HttpGet]
        public IActionResult LabDashboard()
        {
            MasterPage masterPage1 = _masterService.GetLanguageandModules();
            Labs labs = new Labs();

            //labs = _LabEventService.GetAllLabsDetails("en-US");

            labs.Languages = masterPage1.LanguageList;
            return View(labs);
        }

        [Route("updatelabsimagestopreview")]
        [HttpPost]
        public async Task<IActionResult> UpdateLabsImagesToPreview(Labs Labs)
        {
            string path = Path.Combine(_hostingEnvironment.WebRootPath, "images/Lab/PreviewImages/");

            if (Labs.LabHeader != null && Labs.Filename == "LabHeader")
            {
                FilePath(Labs.LabHeader, path + "LabHeader.png", "");
            }

            if (Labs.LabImage != null && Labs.Filename == "LabImage")
            {
                FilePath(Labs.LabImage, path + "LabImage.png", "");
            }

            return Json(1, new JsonSerializerSettings());
        }

        [Route("previewlabs")]
        [HttpPost]
        public async Task<IActionResult> PreviewLabs(Labs Labs)
        {
            try
            {
                _loggerManager.LogInfo("Action :PreviewLabs Data: " + JsonConvert.SerializeObject(Labs));
                string path = Path.Combine(_hostingEnvironment.WebRootPath, "images/Lab/LabImages/");
                string previewPath = Path.Combine(_hostingEnvironment.WebRootPath, "images/Lab/PreviewImages/");
                
                if (Labs.LabImage != null)
                {
                    Labs.Description = Labs.Description.Replace(path + "LabImage.png", previewPath + "LabImage.png");
                }
                if (Labs.LabHeader != null)
                {
                    Labs.Description = Labs.Description.Replace(path + "LabHeader.png", previewPath + "LabHeader.png");
                }

                // int resultCode = _masterService.UpdatePreviewPageByLanguageModuleId(Labs.ModuleId, Labs.LanguageCode, Labs.Description, Labs.MetaDescription, Labs.MetaTitle, Labs.MetaUrl);
                int resultCode = _masterService.UpdatePreviewContentByLanguageModuleId(Labs.SectionId, Labs.ModuleId, Labs.LanguageCode, Labs.Description, Labs.MetaDescription, Labs.MetaTitle, Labs.MetaUrl);

                return Json(resultCode, new JsonSerializerSettings());
            }
            catch (Exception ex)
            {
                _loggerManager.LogError("Action:  PreviewLabs: " + ex.Message);

                throw ex;
            }
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