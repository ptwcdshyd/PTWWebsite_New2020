using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PTW.DataAccess.Models;
using PTW.DataAccess.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using LoggerService;

namespace PTWWebsite2.Controllers
{
    public class ServicesController : Controller
    {
        private readonly IMasterService _masterService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ILoggerManager _loggerManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ServicesController(IMasterService masterService, IHostingEnvironment hostingEnvironment, ILoggerManager loggerManager, IHttpContextAccessor httpContextAccessor)
        {
            _masterService = masterService;
            _hostingEnvironment = hostingEnvironment;
            _loggerManager = loggerManager;
            _httpContextAccessor = httpContextAccessor;
        }


        [Route("service")]
        [Route("{culture}/service")]

        public IActionResult GetServicecontent(string culture)
        {
            MasterPage masterPage = new MasterPage();
            try
            {
                DataTable dtContent = _masterService.GetModuleContent("Services", (culture == null ? "en-US" : culture));
                masterPage.HtmlContent = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Services")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
                ViewData["Header"] = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Header")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
                ViewData["Footer"] = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Footer")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
            }
            catch (Exception exception)
            {
                _loggerManager.LogError(string.Format("Controller:Services, Action :GetServicecontent, ErrorMessage: file: {0} ", exception));

            }
            return View(masterPage);
        }

        [Route("service_cx")]
        [Route("{culture}/service_cx")]

        public IActionResult GetServicCxecontent(string culture)
        {
            MasterPage masterPage = new MasterPage();
            MasterPage masterPage1 = _masterService.GetNewsAndLabDetails("CX", 1);
            try
            {
                DataTable dtContent = _masterService.GetModuleContent("Services_CX", (culture == null ? "en-US" : culture));
                masterPage.HtmlContent = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Services_CX")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
                ViewData["Header"] = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Header")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
                ViewData["Footer"] = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Footer")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
                masterPage1.HtmlContent = masterPage.HtmlContent;
            }
            catch (Exception exception)
            {
                _loggerManager.LogError(string.Format("Controller:Services, Action :GetServicCxecontent, ErrorMessage: file: {0} ", exception));

            }

            return View(masterPage1);
        }

        public string Content(List<NewsAndLabs> newsandlabs, string content, string language)
        {
            foreach (var item in newsandlabs)
            {
                if (item.Type == "News")
                {
                    content = content + "<div class=\"col-lg-12 col-md-4 col-sm-12 col-sx-12\"><div class=\"g-underline\"></div><h6>" + item.Title + " </h6> <a href=\"/News/" + item.ShortDescription.Replace(" ", "-").Replace("&", "@") + "\" titlevalue=\"4\" class=\"btn btn-default btn-sm btn-r text-lowercase  more pull-down float-left\">" + language + "</a></div>";
                }
                if (item.Type == "Labs")
                {
                    content = content + "<div class=\"col-lg-12 col-md-4 col-sm-12 col-sx-12\"><div class=\"g-underline\"></div><h6>" + item.Title + " </h6> <a href=\"/Lab/" + item.ShortDescription.Replace(" ", "-").Replace("&", "@") + "\" titlevalue=\"4\" class=\"btn btn-default btn-sm btn-r text-lowercase  more pull-down float-left\">" + language + "</a></div>";
                }
            }
            return content;
        }
        [Route("service_qa")]
        [Route("{culture}/service_qa")]
        //[Route("{culture}/")]
        public IActionResult GetServiceQacontent(string culture)
        {
            MasterPage masterPage = new MasterPage();
            MasterPage masterPage1 = _masterService.GetNewsAndLabDetails("QA", 1);
            try
            {
                DataTable dtContent = _masterService.GetModuleContent("Services_QA", (culture == null ? "en-US" : culture));
                masterPage.HtmlContent = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Services_QA")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
                ViewData["Header"] = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Header")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
                ViewData["Footer"] = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Footer")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();

                masterPage1.HtmlContent = masterPage.HtmlContent;
                string heading = "<div class=\"col-lg-12 col-md-12 col-sm-12 col-sx-12\"><p id=\"ContentPlaceHolder1_P1\">News &amp; LAB Feed</p></div>";

                switch (culture)
                {
                    case null:
                        string content = "";

                        content = "<div class=\"col-lg-12 col-md-12 col-sm-12 col-sx-12\"><p id=\"ContentPlaceHolder1_P1\">News &amp; LAB Feed</p></div>";
                        string englishContent = Content(masterPage1.NewsAndLabs, content, "more");

                        masterPage1.HtmlContent = masterPage1.HtmlContent.Replace(heading, englishContent);

                        break;
                    case "fr-CA":
                        string content1 = "";

                        content1 = "<div class=\"col-lg-12 col-md-12 col-sm-12 col-sx-12\"><p id=\"ContentPlaceHolder1_P1\">Fil LABO &amp; Infos</p></div>";
                        string frenchContent = Content(masterPage1.NewsAndLabs, content1, "Plus");

                        masterPage1.HtmlContent = masterPage1.HtmlContent.Replace(heading, frenchContent);

                        break;
                    case "ko":
                        string content2 = "";

                        content2 = "<div class=\"col-lg-12 col-md-12 col-sm-12 col-sx-12\"><p id=\"ContentPlaceHolder1_P1\">소식 및 LAB 피드</p></div>";
                        string koreanContent = Content(masterPage1.NewsAndLabs, content2, "더 보기");

                        masterPage1.HtmlContent = masterPage1.HtmlContent.Replace(heading, koreanContent);

                        break;
                    case "ja":
                        string content3 = "";

                        content3 = "<div class=\"col-lg-12 col-md-12 col-sm-12 col-sx-12\"><p id=\"ContentPlaceHolder1_P1\">ニュースとLABフィード</p></div>";
                        string japaneseContent = Content(masterPage1.NewsAndLabs, content3, "詳細");

                        masterPage1.HtmlContent = masterPage1.HtmlContent.Replace(heading, japaneseContent);

                        break;
                    case "zh-CHS":
                        string content4 = "";

                        content4 = "<div class=\"col-lg-12 col-md-12 col-sm-12 col-sx-12\"><p id=\"ContentPlaceHolder1_P1\">新闻 &amp; 实验反馈</p></div>";
                        string simplifiedChineseContent = Content(masterPage1.NewsAndLabs, content4, "更多");

                        masterPage1.HtmlContent = masterPage1.HtmlContent.Replace(heading, simplifiedChineseContent);

                        break;
                    case "zh-CHT":
                        string content5 = "";

                        content5 = "<div class=\"col-lg-12 col-md-12 col-sm-12 col-sx-12\"><p id=\"ContentPlaceHolder1_P1\">News &amp; LAB Feed</p></div>";
                        string traditionalChinese = Content(masterPage1.NewsAndLabs, content5, "more");

                        masterPage1.HtmlContent = masterPage1.HtmlContent.Replace(heading, traditionalChinese);

                        break;
                }
            }
            catch (Exception exception)
            {
                _loggerManager.LogError(string.Format("Controller:Services, Action :GetServiceQacontent, ErrorMessage: file: {0} ", exception));

            }
            return View(masterPage1);
        }

        [Route("localization")]
        [Route("{culture}/localization")]
        //[Route("{culture}/")]
        public IActionResult GetServiceLocalizationcontent(string culture)
        {
            MasterPage masterPage = new MasterPage();
            MasterPage masterPage1 = _masterService.GetNewsAndLabDetails("LOCALIZATION", 1);
            try
            {
                DataTable dtContent = _masterService.GetModuleContent("Localization", (culture == null ? "en-US" : culture));
                masterPage.HtmlContent = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Localization")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
                ViewData["Header"] = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Header")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
                ViewData["Footer"] = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Footer")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();

                masterPage1.HtmlContent = masterPage.HtmlContent;
               
            }
            catch (Exception exception)
            {
                _loggerManager.LogError(string.Format("Controller:Services, Action :GetServiceLocalizationcontent, ErrorMessage: file: {0} ", exception));

            }
            return View(masterPage1);
        }

        [Route("AudioProduction11")]

        [Route("{culture}/AudioProduction11")]
        //[Route("{culture}/")]
        public IActionResult GetServiceAudioProductioncontent(string culture)
        {
            MasterPage masterPage = new MasterPage();
            MasterPage masterPage1 = _masterService.GetNewsAndLabDetails("AUDIO", 1);

            string host = _httpContextAccessor.HttpContext.Request.Host.Value;
            string path =  _httpContextAccessor.HttpContext.Request.Path.Value;

            try
            {
                DataTable dtContent = _masterService.GetModuleContent("Audio Production", (culture == null ? "en-US" : culture));
                masterPage.HtmlContent = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Audio Production")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
                ViewData["Header"] = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Header")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
                ViewData["Footer"] = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Footer")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();

                masterPage1.HtmlContent = masterPage.HtmlContent;
                string heading = "<div class=\"col-lg-12 col-md-12 col-sm-12 col-sx-12\"><p id=\"ContentPlaceHolder1_P1\">News &amp; LAB Feed</p></div>";
                switch (culture)
                {
                    case null:
                        string content = "";

                        content = "<div class=\"col-lg-12 col-md-12 col-sm-12 col-sx-12\"><p id=\"ContentPlaceHolder1_P1\">News &amp; LAB Feed</p></div>";
                        string englishContent = Content(masterPage1.NewsAndLabs, content, "more");

                        masterPage1.HtmlContent = masterPage1.HtmlContent.Replace(heading, englishContent);

                        break;
                    case "fr-CA":
                        string content1 = "";

                        content1 = "<div class=\"col-lg-12 col-md-12 col-sm-12 col-sx-12\"><p id=\"ContentPlaceHolder1_P1\">Fil LABO &amp; Infos</p></div>";
                        string frenchContent = Content(masterPage1.NewsAndLabs, content1, "Plus");

                        masterPage1.HtmlContent = masterPage1.HtmlContent.Replace(heading, frenchContent);

                        break;
                    case "ko":
                        string content2 = "";

                        content2 = "<div class=\"col-lg-12 col-md-12 col-sm-12 col-sx-12\"><p id=\"ContentPlaceHolder1_P1\">소식 및 LAB 피드</p></div>";
                        string koreanContent = Content(masterPage1.NewsAndLabs, content2, "더 보기");

                        masterPage1.HtmlContent = masterPage1.HtmlContent.Replace(heading, koreanContent);

                        break;
                    case "ja":
                        string content3 = "";

                        content3 = "<div class=\"col-lg-12 col-md-12 col-sm-12 col-sx-12\"><p id=\"ContentPlaceHolder1_P1\">ニュースとLABフィード</p></div>";
                        string japaneseContent = Content(masterPage1.NewsAndLabs, content3, "詳細");

                        masterPage1.HtmlContent = masterPage1.HtmlContent.Replace(heading, japaneseContent);

                        break;
                    case "zh-CHS":
                        string content4 = "";

                        content4 = "<div class=\"col-lg-12 col-md-12 col-sm-12 col-sx-12\"><p id=\"ContentPlaceHolder1_P1\">新闻 &amp; 实验反馈</p></div>";
                        string simplifiedChineseContent = Content(masterPage1.NewsAndLabs, content4, "更多");

                        masterPage1.HtmlContent = masterPage1.HtmlContent.Replace(heading, simplifiedChineseContent);

                        break;
                    case "zh-CHT":
                        string content5 = "";

                        content5 = "<div class=\"col-lg-12 col-md-12 col-sm-12 col-sx-12\"><p id=\"ContentPlaceHolder1_P1\">News &amp; LAB Feed</p></div>";
                        string traditionalChinese = Content(masterPage1.NewsAndLabs, content5, "more");

                        masterPage1.HtmlContent = masterPage1.HtmlContent.Replace(heading, traditionalChinese);

                        break;
                }
            }
            catch (Exception exception)
            {
                _loggerManager.LogError(string.Format("Controller:Services, Action :GetServiceAudioProductioncontent, ErrorMessage: file: {0} ", exception));

            }
            return View(masterPage1);
        }

        [Route("ProductDevelopment22")]

        [Route("{culture}/ProductDevelopment22")]
        //[Route("{culture}/")]
        public IActionResult GetServiceEngineeringcontent(string culture)
        {
            MasterPage masterPage = new MasterPage();
            MasterPage masterPage1 = _masterService.GetNewsAndLabDetails("ENGINEERING", 1);
            try
            {
                DataTable dtContent = _masterService.GetModuleContent("ProductDevelopment", (culture == null ? "en-US" : culture));
                masterPage.HtmlContent = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("ProductDevelopment")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
                ViewData["Header"] = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Header")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
                ViewData["Footer"] = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Footer")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();

                masterPage1.HtmlContent = masterPage.HtmlContent;
                string heading = "<div class=\"col-lg-12 col-md-12 col-sm-12 col-sx-12\"><p id=\"ContentPlaceHolder1_P1\">News &amp; LAB Feed</p></div>";
                switch (culture)
                {
                    case null:
                        string content = "";

                        content = "<div class=\"col-lg-12 col-md-12 col-sm-12 col-sx-12\"><p id=\"ContentPlaceHolder1_P1\">News &amp; LAB Feed</p></div>";
                        string englishContent = Content(masterPage1.NewsAndLabs, content, "more");

                        masterPage1.HtmlContent = masterPage1.HtmlContent.Replace(heading, englishContent);

                        break;
                    case "fr-CA":
                        string content1 = "";

                        content1 = "<div class=\"col-lg-12 col-md-12 col-sm-12 col-sx-12\"><p id=\"ContentPlaceHolder1_P1\">Fil LABO &amp; Infos</p></div>";
                        string frenchContent = Content(masterPage1.NewsAndLabs, content1, "Plus");

                        masterPage1.HtmlContent = masterPage1.HtmlContent.Replace(heading, frenchContent);

                        break;
                    case "ko":
                        string content2 = "";

                        content2 = "<div class=\"col-lg-12 col-md-12 col-sm-12 col-sx-12\"><p id=\"ContentPlaceHolder1_P1\">소식 및 LAB 피드</p></div>";
                        string koreanContent = Content(masterPage1.NewsAndLabs, content2, "더 보기");

                        masterPage1.HtmlContent = masterPage1.HtmlContent.Replace(heading, koreanContent);

                        break;
                    case "ja":
                        string content3 = "";

                        content3 = "<div class=\"col-lg-12 col-md-12 col-sm-12 col-sx-12\"><p id=\"ContentPlaceHolder1_P1\">ニュースとLABフィード</p></div>";
                        string japaneseContent = Content(masterPage1.NewsAndLabs, content3, "詳細");

                        masterPage1.HtmlContent = masterPage1.HtmlContent.Replace(heading, japaneseContent);

                        break;
                    case "zh-CHS":
                        string content4 = "";

                        content4 = "<div class=\"col-lg-12 col-md-12 col-sm-12 col-sx-12\"><p id=\"ContentPlaceHolder1_P1\">新闻 &amp; 实验反馈</p></div>";
                        string simplifiedChineseContent = Content(masterPage1.NewsAndLabs, content4, "更多");

                        masterPage1.HtmlContent = masterPage1.HtmlContent.Replace(heading, simplifiedChineseContent);

                        break;
                    case "zh-CHT":
                        string content5 = "";

                        content5 = "<div class=\"col-lg-12 col-md-12 col-sm-12 col-sx-12\"><p id=\"ContentPlaceHolder1_P1\">News &amp; LAB Feed</p></div>";
                        string traditionalChinese = Content(masterPage1.NewsAndLabs, content5, "more");

                        masterPage1.HtmlContent = masterPage1.HtmlContent.Replace(heading, traditionalChinese);

                        break;
                }
            }
            catch (Exception exception)
            {
                _loggerManager.LogError(string.Format("Controller:Services, Action :GetServiceEngineeringcontent, ErrorMessage: file: {0} ", exception));

            }
            return View(masterPage1);
        }


        public IActionResult ServicesList()
        {
            return View("ServicesList");
        }

        public IActionResult GetService(string serviceName)
        {
            MasterPage masterPage = _masterService.GetLanguageandModules();
            QualityAssurance qualityAssurance = new QualityAssurance();
            ProductDevelopment productDevelopment = new ProductDevelopment();
            AudioProduction audioProduction = new AudioProduction();
            Localization localization = new Localization();
            PlayerSupport playerSupport = new PlayerSupport();
            SpeechTech speechTech = new SpeechTech();
            LocalizationQA localizationQA = new LocalizationQA();
            TalentSolution talentSolution = new TalentSolution();
            try
            {
                DataTable dtContent = _masterService.GetModuleContent(serviceName, "en-US");
                masterPage.HtmlContent = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Services_QA")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
                // ViewData["Header"] = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Header")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
                // ViewData["Footer"] = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Footer")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();

                if (serviceName == "Services_QA")
                {
                    qualityAssurance.Languages = masterPage.LanguageList;
                    qualityAssurance.Description = masterPage.HtmlContent;
                    return View("QualityAssuranceEdit", qualityAssurance);
                }
                else if (serviceName == "ProductDevelopment")
                {
                    productDevelopment.Languages = masterPage.LanguageList;
                    productDevelopment.Description = masterPage.HtmlContent;
                    return View("ProductDevelopmentEdit", productDevelopment);
                }
                else if (serviceName == "Audio Production")
                {
                    audioProduction.Languages = masterPage.LanguageList;
                    audioProduction.Description = masterPage.HtmlContent;
                    return View("AudioProductionEdit", audioProduction);
                }
                else if (serviceName == "Localization")
                {
                    localization.Languages = masterPage.LanguageList;
                    localization.Description = masterPage.HtmlContent;
                    return View("LocalizationEdit", localization);
                }
                else if (serviceName == "PlayerSupport")
                {
                    playerSupport.Languages = masterPage.LanguageList;
                    playerSupport.Description = masterPage.HtmlContent;
                    return View("PlayerSupportEdit", playerSupport);
                }
                else if (serviceName == "SpeechTech")
                {
                    speechTech.Languages = masterPage.LanguageList;
                    speechTech.Description = masterPage.HtmlContent;
                    return View("SpeechTechEdit", speechTech);
                }
                else if (serviceName == "LocalizationQA")
                {
                    localizationQA.Languages = masterPage.LanguageList;
                    localizationQA.Description = masterPage.HtmlContent;
                    return View("LocalizationQAEdit", localizationQA);
                }
                else if (serviceName == "TalentSolution")
                {
                    talentSolution.Languages = masterPage.LanguageList;
                    talentSolution.Description = masterPage.HtmlContent;
                    return View("TalentSolutionEdit", talentSolution);
                }

            }
            catch (Exception exception)
            {
                _loggerManager.LogError(string.Format("Controller:Services, Action :GetService, ErrorMessage: file: {0} ", exception));

            }
            return View("QualityAssuranceEdit", qualityAssurance);

        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetContent(int ModuleId, string LanguageCode)
        {
            MasterPage masterPage = null;
            try
            {
                masterPage = _masterService.GetModuleContentById(ModuleId, LanguageCode);

            }
            catch (Exception exception)
            {
                _loggerManager.LogError(string.Format("Controller:Services, Action :GetContent, ErrorMessage: file: {0} ", exception));
            }
            return Json(masterPage, new JsonSerializerSettings());
        }

        [HttpPost]
        public async Task<IActionResult> UpdateQAPageByLanguageId(QualityAssurance QualityAssurance)
        {
            int resultCode = 0;
            try
            {
                string path = Path.Combine(_hostingEnvironment.WebRootPath, "images/QualityAssurance/QA/");
                string deletePath = Path.Combine(_hostingEnvironment.WebRootPath, "images/QualityAssurance/PreviewImages/");
                string backUpPath = Path.Combine(_hostingEnvironment.WebRootPath, "images/QualityAssurance/Backup/");
               

                if (QualityAssurance.QualityAssuranceHearder != null)
                {
                    FilePath(QualityAssurance.QualityAssuranceHearder, path + "QualityAssuranceHearder.png", deletePath + "QualityAssuranceHearder.png", backUpPath + Guid.NewGuid() + "QualityAssuranceHearder.png");
                }
                if (QualityAssurance.Functionality != null)
                {
                    FilePath(QualityAssurance.Functionality, path + "Functionality.svg", deletePath + "Functionality.svg", backUpPath + Guid.NewGuid() + "Functionality.svg");
                }
                if (QualityAssurance.HardwareCompatiblityTesting != null)
                {
                    FilePath(QualityAssurance.HardwareCompatiblityTesting, path + "HardwareCompatiblityTesting.svg", deletePath + "HardwareCompatiblityTesting.svg", backUpPath + Guid.NewGuid() + "HardwareCompatiblityTesting.svg");
                }
                if (QualityAssurance.CertificationTesting != null)
                {
                    FilePath(QualityAssurance.CertificationTesting, path + "CertificationTesting.svg", deletePath + "CertificationTesting.svg", backUpPath + Guid.NewGuid() + "CertificationTesting.svg");
                }
                if (QualityAssurance.SecurityTesting != null)
                {
                    FilePath(QualityAssurance.SecurityTesting, path + "SecurityTesting.svg", deletePath + "SecurityTesting.svg", backUpPath + Guid.NewGuid() + "SecurityTesting.svg");
                }
                if (QualityAssurance.QualityAssuranceAutomation != null)
                {
                    FilePath(QualityAssurance.QualityAssuranceAutomation, path + "QualityAssuranceAutomation.svg", deletePath + "QualityAssuranceAutomation.svg", backUpPath + Guid.NewGuid() + "QualityAssuranceAutomation.svg");
                }
                if (QualityAssurance.UserAccesptanceTesting != null)
                {
                    FilePath(QualityAssurance.UserAccesptanceTesting, path + "UserAccesptanceTesting.svg", deletePath + "UserAccesptanceTesting.svg", backUpPath + Guid.NewGuid() + "UserAccesptanceTesting.svg");
                }
                if (QualityAssurance.Bug1 != null)
                {
                    FilePath(QualityAssurance.Bug1, path + "Bug1.png", deletePath + "Bug1.png", backUpPath + Guid.NewGuid() + "Bug1.png");
                }
                if (QualityAssurance.Bug2 != null)
                {
                    FilePath(QualityAssurance.Bug2, path + "Bug2.png", deletePath + "Bug2.png", backUpPath + Guid.NewGuid() + "Bug2.png");
                }
                if (QualityAssurance.Bug3 != null)
                {
                    FilePath(QualityAssurance.Bug3, path + "Bug3.png", deletePath + "Bug3.png", backUpPath + Guid.NewGuid() + "Bug3.png");
                }
                if (QualityAssurance.Bug4 != null)
                {
                    FilePath(QualityAssurance.Bug4, path + "Bug4.png", deletePath + "Bug4.png", backUpPath + Guid.NewGuid() + "Bug4.png");
                }
                if (QualityAssurance.HelpFullLink != null)
                {
                    FilePath(QualityAssurance.HelpFullLink, path + "HelpFullLink.png", deletePath + "HelpFullLink.png", backUpPath + Guid.NewGuid() + "HelpFullLink.png");
                }
                if (QualityAssurance.Frame != null)
                {
                    FilePath(QualityAssurance.Frame, path + "Frame.svg", deletePath + "Frame.svg", backUpPath + Guid.NewGuid() + "Frame.svg");
                }

                resultCode = _masterService.UpdateHomePageByLanguageId(QualityAssurance.ModuleId, QualityAssurance.LanguageCode, QualityAssurance.Description, QualityAssurance.MetaDescription, QualityAssurance.MetaTitle, QualityAssurance.MetaUrl);


            }
            catch (Exception exception)
            {
                _loggerManager.LogError(string.Format("Controller:Services, Action :UpdateQAPageByLanguageId, ErrorMessage: file: {0} ", exception));
            }
            return Json(resultCode, new JsonSerializerSettings());
        }

        [HttpPost]
        public async Task<IActionResult> UpdateQAImagesToPreview(QualityAssurance QualityAssurance)
        {
            int resultCode = 0;
            string path = Path.Combine(_hostingEnvironment.WebRootPath, "images/QualityAssurance/PreviewImages/");

            try
            {
                if (QualityAssurance.QualityAssuranceHearder != null && QualityAssurance.Filename == "QualityAssuranceHearder")
                {
                    FilePath(QualityAssurance.QualityAssuranceHearder, path + "QualityAssuranceHearder.png", "", "");
                }
                if (QualityAssurance.Functionality != null && QualityAssurance.Filename == "Functionality")
                {
                    FilePath(QualityAssurance.Functionality, path + "Functionality.svg", "", "");
                }
                if (QualityAssurance.HardwareCompatiblityTesting != null && QualityAssurance.Filename == "HardwareCompatiblityTesting")
                {
                    FilePath(QualityAssurance.HardwareCompatiblityTesting, path + "HardwareCompatiblityTesting.svg", "", "");
                }
                if (QualityAssurance.CertificationTesting != null && QualityAssurance.Filename == "CertificationTesting")
                {
                    FilePath(QualityAssurance.CertificationTesting, path + "CertificationTesting.svg", "", "");
                }
                if (QualityAssurance.SecurityTesting != null && QualityAssurance.Filename == "SecurityTesting")
                {
                    FilePath(QualityAssurance.CertificationTesting, path + "SecurityTesting.svg", "", "");
                }
                if (QualityAssurance.QualityAssuranceAutomation != null && QualityAssurance.Filename == "QualityAssuranceAutomation")
                {
                    FilePath(QualityAssurance.QualityAssuranceAutomation, path + "QualityAssuranceAutomation.svg", "", "");
                }
                if (QualityAssurance.UserAccesptanceTesting != null && QualityAssurance.Filename == "UserAccesptanceTesting")
                {
                    FilePath(QualityAssurance.UserAccesptanceTesting, path + "UserAccesptanceTesting.svg", "", "");
                }
                if (QualityAssurance.Bug1 != null && QualityAssurance.Filename == "Bug1")
                {
                    FilePath(QualityAssurance.Bug1, path + "Bug1.png", "", "");
                }
                if (QualityAssurance.Bug2 != null && QualityAssurance.Filename == "Bug2")
                {
                    FilePath(QualityAssurance.Bug2, path + "Bug2.png", "", "");
                }
                if (QualityAssurance.Bug3 != null && QualityAssurance.Filename == "Bug3")
                {
                    FilePath(QualityAssurance.Bug3, path + "Bug3.png", "", "");
                }
                if (QualityAssurance.Bug4 != null && QualityAssurance.Filename == "Bug4")
                {
                    FilePath(QualityAssurance.Bug4, path + "Bug4.png", "", "");
                }
                if (QualityAssurance.HelpFullLink != null && QualityAssurance.Filename == "HelpFullLink")
                {
                    FilePath(QualityAssurance.HelpFullLink, path + "HelpFullLink.png", "", "");
                }
                if (QualityAssurance.Frame != null && QualityAssurance.Filename == "Frame")
                {
                    FilePath(QualityAssurance.Frame, path + "Frame.svg", "", "");
                }
                resultCode = _masterService.UpdatePreviewPageByLanguageModuleId(QualityAssurance.ModuleId, QualityAssurance.LanguageCode, QualityAssurance.Description, QualityAssurance.MetaDescription, QualityAssurance.MetaTitle, QualityAssurance.MetaUrl);

            }
            catch (Exception exception)
            {
                _loggerManager.LogError(string.Format("Controller:Services, Action :UpdateQAImagesToPreview, ErrorMessage: file: {0} ", exception));
            }
            return Json(resultCode, new JsonSerializerSettings());
        }


        [HttpPost]
        public async Task<IActionResult> PreviewQA(QualityAssurance QualityAssurance)
        {
            int resultCode = 0;
            string path = Path.Combine(_hostingEnvironment.WebRootPath, "images/QualityAssurance/QA/");
            string previewPath = Path.Combine(_hostingEnvironment.WebRootPath, "images/QualityAssurance/PreviewImages/");

            try
            {
                if (QualityAssurance.QualityAssuranceHearder != null)
                {
                    QualityAssurance.Description = QualityAssurance.Description.Replace(path + "QualityAssuranceHearder.png", previewPath + "QualityAssuranceHearder.png");
                }
                if (QualityAssurance.Functionality != null)
                {
                    QualityAssurance.Description = QualityAssurance.Description.Replace(path + "Functionality.svg", previewPath + "Functionality.svg");
                }
                if (QualityAssurance.HardwareCompatiblityTesting != null)
                {
                    QualityAssurance.Description = QualityAssurance.Description.Replace(path + "HardwareCompatiblityTesting.svg", previewPath + "HardwareCompatiblityTesting.svg");
                }
                if (QualityAssurance.CertificationTesting != null)
                {
                    QualityAssurance.Description = QualityAssurance.Description.Replace(path + "CertificationTesting.svg", previewPath + "CertificationTesting.svg");
                }
                if (QualityAssurance.SecurityTesting != null)
                {
                    QualityAssurance.Description = QualityAssurance.Description.Replace(path + "SecurityTesting.svg", previewPath + "SecurityTesting.svg");
                }
                if (QualityAssurance.QualityAssuranceAutomation != null)
                {
                    QualityAssurance.Description = QualityAssurance.Description.Replace(path + "QualityAssuranceAutomation.svg", previewPath + "QualityAssuranceAutomation.svg");
                }
                if (QualityAssurance.UserAccesptanceTesting != null)
                {
                    QualityAssurance.Description = QualityAssurance.Description.Replace(path + "UserAccesptanceTesting.svg", previewPath + "UserAccesptanceTesting.svg");
                }
                if (QualityAssurance.Bug1 != null)
                {
                    QualityAssurance.Description = QualityAssurance.Description.Replace(path + "Bug1.png", previewPath + "Bug1.png");
                }
                if (QualityAssurance.Bug2 != null)
                {
                    QualityAssurance.Description = QualityAssurance.Description.Replace(path + "Bug2.png", previewPath + "Bug2.png");
                }
                if (QualityAssurance.Bug3 != null)
                {
                    QualityAssurance.Description = QualityAssurance.Description.Replace(path + "Bug3.png", previewPath + "Bug3.png");
                }
                if (QualityAssurance.Bug4 != null)
                {
                    QualityAssurance.Description = QualityAssurance.Description.Replace(path + "Bug4.png", previewPath + "Bug4.png");
                }
                if (QualityAssurance.HelpFullLink != null)
                {
                    QualityAssurance.Description = QualityAssurance.Description.Replace(path + "HelpFullLink.png", previewPath + "HelpFullLink.png");
                }
                if (QualityAssurance.Frame != null)
                {
                    QualityAssurance.Description = QualityAssurance.Description.Replace(path + "Frame.svg", previewPath + "Frame.svg");
                }



                resultCode = _masterService.UpdatePreviewPageByLanguageModuleId(QualityAssurance.ModuleId, QualityAssurance.LanguageCode, QualityAssurance.Description, QualityAssurance.MetaDescription, QualityAssurance.MetaTitle, QualityAssurance.MetaUrl);

            }
            catch (Exception exception)
            {
                _loggerManager.LogError(string.Format("Controller:Services, Action :PreviewQA, ErrorMessage: file: {0} ", exception));
            }
            return Json(resultCode, new JsonSerializerSettings());
        }

        #region Product Development

        [HttpPost]
        public async Task<IActionResult> UpdateProductDevelopmentPageByLanguageId(ProductDevelopment ProductDevelopment)
        {
            int resultCode = 0;
            string path = Path.Combine(_hostingEnvironment.WebRootPath, "images/ProductDevelopment/PD/");
            string deletePath = Path.Combine(_hostingEnvironment.WebRootPath, "images/ProductDevelopment/PreviewImages/");
            string backUpPath = Path.Combine(_hostingEnvironment.WebRootPath, "images/ProductDevelopment/Backup/");

            try
            {
                
                MasterPage master = _masterService.GetModuleContentById(ProductDevelopment.ModuleId, ProductDevelopment.LanguageCode);
                System.IO.File.WriteAllText(backUpPath + "ProductDevelopment_" + Guid.NewGuid(), master.HtmlContent);

                if (ProductDevelopment.ProductDevelopmentHearder != null)
                {
                    FilePath(ProductDevelopment.ProductDevelopmentHearder, path + "ProductDevelopmentHearder.png", deletePath + "ProductDevelopmentHearder.png", backUpPath + Guid.NewGuid() + "ProductDevelopmentHearder.png");
                }
                if (ProductDevelopment.Android != null)
                {
                    FilePath(ProductDevelopment.Android, path + "Android.png", deletePath + "Android.png", backUpPath + Guid.NewGuid() + "Android.png");
                }
                if (ProductDevelopment.IOS != null)
                {
                    FilePath(ProductDevelopment.IOS, path + "IOS.svg", deletePath + "IOS.svg", backUpPath + Guid.NewGuid() + "IOS.svg");
                }
                if (ProductDevelopment.PC != null)
                {
                    FilePath(ProductDevelopment.PC, path + "PC.svg", deletePath + "PC.svg", backUpPath + Guid.NewGuid() + "PC.svg");
                }
                if (ProductDevelopment.Cloud != null)
                {
                    FilePath(ProductDevelopment.Cloud, path + "Cloud.svg", deletePath + "Cloud.svg", backUpPath + Guid.NewGuid() + "Cloud.svg");
                }
                if (ProductDevelopment.Unity != null)
                {
                    FilePath(ProductDevelopment.Unity, path + "Unity.png", deletePath + "Unity.png", backUpPath + Guid.NewGuid() + "Unity.png");
                }
                if (ProductDevelopment.Unreal != null)
                {
                    FilePath(ProductDevelopment.Unreal, path + "Unreal.png", deletePath + "Unreal.png", backUpPath + Guid.NewGuid() + "Unreal.png");
                }
                if (ProductDevelopment.GameMakerStudio != null)
                {
                    FilePath(ProductDevelopment.GameMakerStudio, path + "GameMakerStudio.png", deletePath + "GameMakerStudio.png", backUpPath + Guid.NewGuid() + "GameMakerStudio.png");
                }
                if (ProductDevelopment.HTML5 != null)
                {
                    FilePath(ProductDevelopment.HTML5, path + "HTML5.png", deletePath + "HTML5.png", backUpPath + Guid.NewGuid() + "HTML5.png");
                }
                if (ProductDevelopment.Porting != null)
                {
                    FilePath(ProductDevelopment.Porting, path + "Porting.png", deletePath + "Porting.png", backUpPath + Guid.NewGuid() + "Porting.png");
                }
                if (ProductDevelopment.LiveOps != null)
                {
                    FilePath(ProductDevelopment.LiveOps, path + "LiveOps.png", deletePath + "LiveOps.png", backUpPath + Guid.NewGuid() + "LiveOps.png");
                }
                if (ProductDevelopment.GameDevelopment != null)
                {
                    FilePath(ProductDevelopment.GameDevelopment, path + "GameDevelopment.png", deletePath + "GameDevelopment.png", backUpPath + Guid.NewGuid() + "GameDevelopment.png");
                }
                if (ProductDevelopment.Frame != null)
                {
                    FilePath(ProductDevelopment.Frame, path + "Frame.svg", deletePath + "Frame.svg", backUpPath + Guid.NewGuid() + "Frame.svg");
                }

                resultCode = _masterService.UpdateHomePageByLanguageId(ProductDevelopment.ModuleId, ProductDevelopment.LanguageCode, ProductDevelopment.Description, ProductDevelopment.MetaDescription, ProductDevelopment.MetaTitle, ProductDevelopment.MetaUrl);

               
            }
            catch (Exception exception)
            {
                _loggerManager.LogError(string.Format("Controller:Services, Action :UpdateProductDevelopmentPageByLanguageId, ErrorMessage: file: {0} ", exception));
            }
            return Json(resultCode, new JsonSerializerSettings());
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProductDevelopmentImagesToPreview(ProductDevelopment ProductDevelopment)
        {
            string path = Path.Combine(_hostingEnvironment.WebRootPath, "images/ProductDevelopment/PreviewImages/");

            try
            {
                if (ProductDevelopment.ProductDevelopmentHearder != null && ProductDevelopment.Filename == "ProductDevelopmentHearder")
                {
                    FilePath(ProductDevelopment.ProductDevelopmentHearder, path + "ProductDevelopmentHearder.png", "", "");
                }
                if (ProductDevelopment.Android != null && ProductDevelopment.Filename == "Android")
                {
                    FilePath(ProductDevelopment.Android, path + "Android.png", "", "");
                }
                if (ProductDevelopment.IOS != null && ProductDevelopment.Filename == "IOS")
                {
                    FilePath(ProductDevelopment.IOS, path + "IOS.svg", "", "");
                }
                if (ProductDevelopment.PC != null && ProductDevelopment.Filename == "PC")
                {
                    FilePath(ProductDevelopment.PC, path + "PC.svg", "", "");
                }
                if (ProductDevelopment.Cloud != null && ProductDevelopment.Filename == "Cloud")
                {
                    FilePath(ProductDevelopment.Cloud, path + "Cloud.svg", "", "");
                }
                if (ProductDevelopment.Unity != null && ProductDevelopment.Filename == "Unity")
                {
                    FilePath(ProductDevelopment.Unity, path + "Unity.png", "", "");
                }
                if (ProductDevelopment.Unreal != null && ProductDevelopment.Filename == "Unreal")
                {
                    FilePath(ProductDevelopment.Unreal, path + "Unreal.png", "", "");
                }
                if (ProductDevelopment.GameMakerStudio != null && ProductDevelopment.Filename == "GameMakerStudio")
                {
                    FilePath(ProductDevelopment.GameMakerStudio, path + "GameMakerStudio.png", "", "");
                }
                if (ProductDevelopment.HTML5 != null && ProductDevelopment.Filename == "HTML5")
                {
                    FilePath(ProductDevelopment.HTML5, path + "HTML5.png", "", "");
                }
                if (ProductDevelopment.Porting != null && ProductDevelopment.Filename == "Porting")
                {
                    FilePath(ProductDevelopment.Porting, path + "Porting.png", "", "");
                }
                if (ProductDevelopment.LiveOps != null && ProductDevelopment.Filename == "LiveOps")
                {
                    FilePath(ProductDevelopment.LiveOps, path + "LiveOps.png", "", "");
                }
                if (ProductDevelopment.GameDevelopment != null && ProductDevelopment.Filename == "GameDevelopment")
                {
                    FilePath(ProductDevelopment.GameDevelopment, path + "GameDevelopment.png", "", "");
                }
                if (ProductDevelopment.Frame != null && ProductDevelopment.Filename == "Frame")
                {
                    FilePath(ProductDevelopment.Frame, path + "Frame.svg", "", "");
                }
            }
            catch (Exception exception)
            {
                _loggerManager.LogError(string.Format("Controller:Services, Action :UpdateProductDevelopmentImagesToPreview, ErrorMessage: file: {0} ", exception));
            }

            return Json(1, new JsonSerializerSettings());
        }


        [HttpPost]
        public async Task<IActionResult> PreviewProductDevelopment(ProductDevelopment ProductDevelopment)
        {
            int resultCode = 0;
            string savedPath = Path.Combine(_hostingEnvironment.WebRootPath, "images/ProductDevelopment/PD/");
            string previewPath = Path.Combine(_hostingEnvironment.WebRootPath, "images/ProductDevelopment/PreviewImages/");

            try
            {
                if (ProductDevelopment.ProductDevelopmentHearder != null)
                {
                    ProductDevelopment.Description = ProductDevelopment.Description.Replace(savedPath + "ProductDevelopmentHearder.png", previewPath + "ProductDevelopmentHearder.png");
                }
                if (ProductDevelopment.Android != null)
                {
                    ProductDevelopment.Description = ProductDevelopment.Description.Replace(savedPath + "Android.png", previewPath + "Android.png");
                }
                if (ProductDevelopment.IOS != null)
                {
                    ProductDevelopment.Description = ProductDevelopment.Description.Replace(savedPath + "IOS.svg", previewPath + "IOS.png");
                }
                if (ProductDevelopment.PC != null)
                {
                    ProductDevelopment.Description = ProductDevelopment.Description.Replace(savedPath + "PC.svg", previewPath + "PC.png");
                }
                if (ProductDevelopment.Cloud != null)
                {
                    ProductDevelopment.Description = ProductDevelopment.Description.Replace(savedPath + "Cloud.svg", previewPath + "Cloud.svg");
                }
                if (ProductDevelopment.Unity != null)
                {
                    ProductDevelopment.Description = ProductDevelopment.Description.Replace(savedPath + "Unity.png", previewPath + "Unity.png");
                }
                if (ProductDevelopment.Unreal != null)
                {
                    ProductDevelopment.Description = ProductDevelopment.Description.Replace(savedPath + "Unreal.png", previewPath + "Unreal.png");
                }
                if (ProductDevelopment.GameMakerStudio != null)
                {
                    ProductDevelopment.Description = ProductDevelopment.Description.Replace(savedPath + "GameMakerStudio.png", previewPath + "GameMakerStudio.png");
                }
                if (ProductDevelopment.HTML5 != null)
                {
                    ProductDevelopment.Description = ProductDevelopment.Description.Replace(savedPath + "HTML5.png", previewPath + "HTML5.png");
                }
                if (ProductDevelopment.Porting != null)
                {
                    ProductDevelopment.Description = ProductDevelopment.Description.Replace(savedPath + "Porting.png", previewPath + "Porting.png");
                }
                if (ProductDevelopment.LiveOps != null)
                {
                    ProductDevelopment.Description = ProductDevelopment.Description.Replace(savedPath + "LiveOps.png", previewPath + "LiveOps.png");
                }
                if (ProductDevelopment.GameDevelopment != null)
                {
                    ProductDevelopment.Description = ProductDevelopment.Description.Replace(savedPath + "GameDevelopment.png", previewPath + "GameDevelopment.png");
                }
                if (ProductDevelopment.Frame != null)
                {
                    ProductDevelopment.Description = ProductDevelopment.Description.Replace(savedPath + "Frame.svg", previewPath + "Frame.svg");
                }


                resultCode = _masterService.UpdatePreviewPageByLanguageModuleId(ProductDevelopment.ModuleId, ProductDevelopment.LanguageCode, ProductDevelopment.Description, ProductDevelopment.MetaDescription, ProductDevelopment.MetaTitle, ProductDevelopment.MetaUrl);

            }
            catch (Exception exception)
            {
                _loggerManager.LogError(string.Format("Controller:Services, Action :PreviewProductDevelopment, ErrorMessage: file: {0} ", exception));
            }
            return Json(resultCode, new JsonSerializerSettings());
        }

        #endregion

        #region Audio Production

        [HttpPost]
        public async Task<IActionResult> UpdateAudioProductionPageByLanguageId(AudioProduction AudioProduction)
        {
            int resultCode = 0;
            try
            {
               
                string path = Path.Combine(_hostingEnvironment.WebRootPath, "images/AudioProduction/AP/");
                string deletePath = Path.Combine(_hostingEnvironment.WebRootPath, "images/AudioProduction/PreviewImages/");
                string backUpPath = Path.Combine(_hostingEnvironment.WebRootPath, "images/AudioProduction/Backup/");
                

                if (AudioProduction.AudioProductionHearder != null)
                {
                    FilePath(AudioProduction.AudioProductionHearder, path + "AudioProductionHearder.png", deletePath + "AudioProductionHearder.png", backUpPath + Guid.NewGuid() + "AudioProductionHearder.png");
                }
                if (AudioProduction.Casting != null)
                {
                    FilePath(AudioProduction.Casting, path + "Casting.svg", deletePath + "Casting.svg", backUpPath + Guid.NewGuid() + "Casting.svg");
                }
                if (AudioProduction.ProductManagement != null)
                {
                    FilePath(AudioProduction.ProductManagement, path + "ProductManagement.svg", deletePath + "ProductManagement.svg", backUpPath + Guid.NewGuid() + "ProductManagement.svg");
                }
                if (AudioProduction.PerformanceDirection != null)
                {
                    FilePath(AudioProduction.PerformanceDirection, path + "PerformanceDirection.svg", deletePath + "PerformanceDirection.svg", backUpPath + Guid.NewGuid() + "PerformanceDirection.svg");
                }
                if (AudioProduction.Audio != null)
                {
                    FilePath(AudioProduction.Audio, path + "Audio.svg", deletePath + "Audio.svg", backUpPath + Guid.NewGuid() + "Audio.svg");
                }
                if (AudioProduction.OnLocalRecording != null)
                {
                    FilePath(AudioProduction.OnLocalRecording, path + "OnLocalRecording.svg", deletePath + "OnLocalRecording.svg", backUpPath + Guid.NewGuid() + "OnLocalRecording.svg");
                }
                if (AudioProduction.PostProductionMastering != null)
                {
                    FilePath(AudioProduction.PostProductionMastering, path + "PostProductionMastering.svg", deletePath + "PostProductionMastering.svg", backUpPath + Guid.NewGuid() + "PostProductionMastering.svg");
                }
                if (AudioProduction.AudioLocalization != null)
                {
                    FilePath(AudioProduction.AudioLocalization, path + "AudioLocalization.svg", deletePath + "AudioLocalization.svg", backUpPath + Guid.NewGuid() + "AudioLocalization.svg");
                }
                if (AudioProduction.AudioCharacter != null)
                {
                    FilePath(AudioProduction.AudioCharacter, path + "AudioCharacter.png", deletePath + "AudioCharacter.png", backUpPath + Guid.NewGuid() + "AudioCharacter.png");
                }
                if (AudioProduction.CastingEdgeGlobalstudios != null)
                {
                    FilePath(AudioProduction.CastingEdgeGlobalstudios, path + "CastingEdgeGlobalstudios.png", deletePath + "CastingEdgeGlobalstudios.png", backUpPath + Guid.NewGuid() + "CastingEdgeGlobalstudios.png");
                }
                if (AudioProduction.DedicatedProductManagement != null)
                {
                    FilePath(AudioProduction.DedicatedProductManagement, path + "DedicatedProductManagement.png", deletePath + "DedicatedProductManagement.png", backUpPath + Guid.NewGuid() + "DedicatedProductManagement.png");
                }
                if (AudioProduction.Frame != null)
                {
                    FilePath(AudioProduction.Frame, path + "Frame.svg", deletePath + "Frame.svg", backUpPath + Guid.NewGuid() + "Frame.svg");
                }

                resultCode = _masterService.UpdateHomePageByLanguageId(AudioProduction.ModuleId, AudioProduction.LanguageCode, AudioProduction.Description, AudioProduction.MetaDescription, AudioProduction.MetaTitle, AudioProduction.MetaUrl);

              
            }
            catch (Exception exception)
            {
                _loggerManager.LogError(string.Format("Controller:Services, Action :UpdateAudioProductionPageByLanguageId, ErrorMessage: file: {0} ", exception));
            }
            return Json(resultCode, new JsonSerializerSettings());
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAudioProductionImagesToPreview(AudioProduction AudioProduction)
        {
            string path = Path.Combine(_hostingEnvironment.WebRootPath, "images/AudioProduction/PreviewImages/");

            try
            {
                if (AudioProduction.AudioProductionHearder != null && AudioProduction.Filename == "AudioProductionHearder")
                {
                    FilePath(AudioProduction.AudioProductionHearder, path + "AudioProductionHearder.png", "", "");
                }
                if (AudioProduction.Casting != null && AudioProduction.Filename == "Casting")
                {
                    FilePath(AudioProduction.Casting, path + "Casting.svg", "", "");
                }
                if (AudioProduction.ProductManagement != null && AudioProduction.Filename == "ProductManagement")
                {
                    FilePath(AudioProduction.ProductManagement, path + "ProductManagement.svg", "", "");
                }
                if (AudioProduction.PerformanceDirection != null && AudioProduction.Filename == "PerformanceDirection")
                {
                    FilePath(AudioProduction.PerformanceDirection, path + "PerformanceDirection.svg", "", "");
                }
                if (AudioProduction.Audio != null && AudioProduction.Filename == "Audio")
                {
                    FilePath(AudioProduction.Audio, path + "Audio.svg", "", "");
                }
                if (AudioProduction.OnLocalRecording != null && AudioProduction.Filename == "OnLocalRecording")
                {
                    FilePath(AudioProduction.OnLocalRecording, path + "OnLocalRecording.svg", "", "");
                }
                if (AudioProduction.PostProductionMastering != null && AudioProduction.Filename == "PostProductionMastering")
                {
                    FilePath(AudioProduction.PostProductionMastering, path + "PostProductionMastering.svg", "", "");
                }
                if (AudioProduction.AudioLocalization != null && AudioProduction.Filename == "AudioLocalization")
                {
                    FilePath(AudioProduction.AudioLocalization, path + "AudioLocalization.svg", "", "");
                }
                if (AudioProduction.AudioCharacter != null && AudioProduction.Filename == "AudioCharacter")
                {
                    FilePath(AudioProduction.AudioCharacter, path + "AudioCharacter.png", "", "");
                }
                if (AudioProduction.CastingEdgeGlobalstudios != null && AudioProduction.Filename == "CastingEdgeGlobalstudios")
                {
                    FilePath(AudioProduction.CastingEdgeGlobalstudios, path + "CastingEdgeGlobalstudios.png", "", "");
                }
                if (AudioProduction.DedicatedProductManagement != null && AudioProduction.Filename == "DedicatedProductManagement")
                {
                    FilePath(AudioProduction.DedicatedProductManagement, path + "DedicatedProductManagement.png", "", "");
                }
                if (AudioProduction.Frame != null && AudioProduction.Filename == "Frame")
                {
                    FilePath(AudioProduction.Frame, path + "Frame.svg", "", "");
                }

            }
            catch (Exception exception)
            {
                _loggerManager.LogError(string.Format("Controller:Services, Action :UpdateAudioProductionImagesToPreview, ErrorMessage: file: {0} ", exception));
            }
            return Json(1, new JsonSerializerSettings());
        }


        [HttpPost]
        public async Task<IActionResult> PreviewAudioProduction(AudioProduction AudioProduction)
        {
            int resultCode = 0;
            string savedPath = Path.Combine(_hostingEnvironment.WebRootPath, "images/AudioProduction/AP/");
            string previewPath = Path.Combine(_hostingEnvironment.WebRootPath, "images/AudioProduction/PreviewImages/");

            try
            {
                if (AudioProduction.AudioProductionHearder != null)
                {
                    AudioProduction.Description = AudioProduction.Description.Replace(savedPath + "AudioProductionHearder.png", previewPath + "AudioProductionHearder.png");
                }
                if (AudioProduction.Casting != null)
                {
                    AudioProduction.Description = AudioProduction.Description.Replace(savedPath + "Casting.svg", previewPath + "Casting.svg");
                }
                if (AudioProduction.ProductManagement != null)
                {
                    AudioProduction.Description = AudioProduction.Description.Replace(savedPath + "ProductManagement.svg", previewPath + "ProductManagement.svg");
                }
                if (AudioProduction.PerformanceDirection != null)
                {
                    AudioProduction.Description = AudioProduction.Description.Replace(savedPath + "PerformanceDirection.svg", previewPath + "PerformanceDirection.svg");
                }
                if (AudioProduction.Audio != null)
                {
                    AudioProduction.Description = AudioProduction.Description.Replace(savedPath + "Audio.svg", previewPath + "Audio.svg");
                }
                if (AudioProduction.OnLocalRecording != null)
                {
                    AudioProduction.Description = AudioProduction.Description.Replace(savedPath + "OnLocalRecording.svg", previewPath + "OnLocalRecording.svg");
                }
                if (AudioProduction.PostProductionMastering != null)
                {
                    AudioProduction.Description = AudioProduction.Description.Replace(savedPath + "PostProductionMastering.svg", previewPath + "PostProductionMastering.svg");
                }
                if (AudioProduction.AudioLocalization != null)
                {
                    AudioProduction.Description = AudioProduction.Description.Replace(savedPath + "AudioLocalization.svg", previewPath + "AudioLocalization.svg");
                }
                if (AudioProduction.AudioCharacter != null)
                {
                    AudioProduction.Description = AudioProduction.Description.Replace(savedPath + "AudioCharacter.png", previewPath + "AudioCharacter.png");
                }
                if (AudioProduction.CastingEdgeGlobalstudios != null)
                {
                    AudioProduction.Description = AudioProduction.Description.Replace(savedPath + "CastingEdgeGlobalstudios.png", previewPath + "CastingEdgeGlobalstudios.png");
                }
                if (AudioProduction.DedicatedProductManagement != null)
                {
                    AudioProduction.Description = AudioProduction.Description.Replace(savedPath + "DedicatedProductManagement.png", previewPath + "DedicatedProductManagement.png");
                }
                if (AudioProduction.Frame != null)
                {
                    AudioProduction.Description = AudioProduction.Description.Replace(savedPath + "Frame.svg", previewPath + "Frame.svg");
                }

                resultCode = _masterService.UpdatePreviewPageByLanguageModuleId(AudioProduction.ModuleId, AudioProduction.LanguageCode, AudioProduction.Description, AudioProduction.MetaDescription, AudioProduction.MetaTitle, AudioProduction.MetaUrl);

            }
            catch (Exception exception)
            {
                _loggerManager.LogError(string.Format("Controller:Services, Action :PreviewAudioProduction, ErrorMessage: file: {0} ", exception));
            }
            return Json(resultCode, new JsonSerializerSettings());
        }

        #endregion

        #region Localization

        [HttpPost]
        public async Task<IActionResult> UpdateLocalizationPageByLanguageId(Localization Localization)
        {
            int resultCode = 0;
            try
            {
                string path = Path.Combine(_hostingEnvironment.WebRootPath, "images/Localization/Local/");
                string deletePath = Path.Combine(_hostingEnvironment.WebRootPath, "images/Localization/PreviewImages/");
                string backUpPath = Path.Combine(_hostingEnvironment.WebRootPath, "images/Localization/Backup/");
                

                if (Localization.LocalizatonHearder != null)
                {
                    FilePath(Localization.LocalizatonHearder, path + "LocalizatonHearder.png", deletePath + "LocalizatonHearder.png", backUpPath + Guid.NewGuid() + "LocalizatonHearder.png");
                }
                if (Localization.GameTranslation != null)
                {
                    FilePath(Localization.GameTranslation, path + "GameTranslation.svg", deletePath + "GameTranslation.svg", backUpPath + Guid.NewGuid() + "GameTranslation.svg");
                }
                if (Localization.LocalizationAndCulturalization != null)
                {
                    FilePath(Localization.LocalizationAndCulturalization, path + "LocalizationAndCulturalization.svg", deletePath + "LocalizationAndCulturalization.svg", backUpPath + Guid.NewGuid() + "LocalizationAndCulturalization.svg");
                }
                if (Localization.PostEditingMachineTranslation != null)
                {
                    FilePath(Localization.PostEditingMachineTranslation, path + "PostEditingMachineTranslation.svg", deletePath + "PostEditingMachineTranslation.svg", backUpPath + Guid.NewGuid() + "PostEditingMachineTranslation.svg");
                }
                if (Localization.MarketingAndAdvertisingTranslation != null)
                {
                    FilePath(Localization.MarketingAndAdvertisingTranslation, path + "MarketingAndAdvertisingTranslation.svg", deletePath + "MarketingAndAdvertisingTranslation.svg", backUpPath + Guid.NewGuid() + "MarketingAndAdvertisingTranslation.svg");
                }
                if (Localization.Frame != null)
                {
                    FilePath(Localization.Frame, path + "Frame.svg", deletePath + "Frame.svg", backUpPath + Guid.NewGuid() + "Frame.svg");
                }

                resultCode = _masterService.UpdateHomePageByLanguageId(Localization.ModuleId, Localization.LanguageCode, Localization.Description, Localization.MetaDescription, Localization.MetaTitle, Localization.MetaUrl);

            }
            catch (Exception exception)
            {
                _loggerManager.LogError(string.Format("Controller:Services, Action :UpdateLocalizationPageByLanguageId, ErrorMessage: file: {0} ", exception));
            }

            return Json(resultCode, new JsonSerializerSettings());

        }

        [HttpPost]
        public async Task<IActionResult> UpdateLocalizationImagesToPreview(Localization Localization)
        {
            string path = Path.Combine(_hostingEnvironment.WebRootPath, "images/Localization/PreviewImages/");

            try
            {
                if (Localization.LocalizatonHearder != null && Localization.Filename == "LocalizatonHearder")
                {
                    FilePath(Localization.LocalizatonHearder, path + "LocalizatonHearder.png", "", "");
                }
                if (Localization.GameTranslation != null && Localization.Filename == "GameTranslation")
                {
                    FilePath(Localization.GameTranslation, path + "GameTranslation.svg", "", "");
                }
                if (Localization.LocalizationAndCulturalization != null && Localization.Filename == "LocalizationAndCulturalization")
                {
                    FilePath(Localization.LocalizationAndCulturalization, path + "LocalizationAndCulturalization.svg", "", "");
                }
                if (Localization.PostEditingMachineTranslation != null && Localization.Filename == "PostEditingMachineTranslation")
                {
                    FilePath(Localization.PostEditingMachineTranslation, path + "PostEditingMachineTranslation.svg", "", "");
                }
                if (Localization.MarketingAndAdvertisingTranslation != null && Localization.Filename == "MarketingAndAdvertisingTranslation")
                {
                    FilePath(Localization.MarketingAndAdvertisingTranslation, path + "MarketingAndAdvertisingTranslation.svg", "", "");
                }

                if (Localization.Frame != null && Localization.Filename == "Frame")
                {
                    FilePath(Localization.Frame, path + "Frame.svg", "", "");
                }

            }
            catch (Exception exception)
            {
                _loggerManager.LogError(string.Format("Controller:Services, Action :UpdateLocalizationImagesToPreview, ErrorMessage: file: {0} ", exception));
            }
            return Json(1, new JsonSerializerSettings());
        }


        [HttpPost]
        public async Task<IActionResult> PreviewLocalization(Localization Localization)
        {
            int resultCode = 0;
            string savedPath = Path.Combine(_hostingEnvironment.WebRootPath, "images/Localization/Local/");
            string previewPath = Path.Combine(_hostingEnvironment.WebRootPath, "images/Localization/PreviewImages/");

            try
            {
                if (Localization.LocalizatonHearder != null)
                {
                    Localization.Description = Localization.Description.Replace(savedPath + "LocalizationHearder.png", previewPath + "LocalizatonHearder.png");
                }
                if (Localization.GameTranslation != null)
                {
                    Localization.Description = Localization.Description.Replace(savedPath + "GameTranslation.svg", previewPath + "GameTranslation.svg");
                }
                if (Localization.LocalizationAndCulturalization != null)
                {
                    Localization.Description = Localization.Description.Replace(savedPath + "LocalizationAndCulturalization.svg", previewPath + "LocalizationAndCulturalization.svg");
                }
                if (Localization.PostEditingMachineTranslation != null)
                {
                    Localization.Description = Localization.Description.Replace(savedPath + "PostEditingMachineTranslation.svg", previewPath + "PostEditingMachineTranslation.svg");
                }
                if (Localization.MarketingAndAdvertisingTranslation != null)
                {
                    Localization.Description = Localization.Description.Replace(savedPath + "MarketingAndAdvertisingTranslation.svg", previewPath + "MarketingAndAdvertisingTranslation.svg");
                }

                if (Localization.Frame != null)
                {
                    Localization.Description = Localization.Description.Replace(savedPath + "Frame.svg", previewPath + "Frame.svg");
                }

                resultCode = _masterService.UpdatePreviewPageByLanguageModuleId(Localization.ModuleId, Localization.LanguageCode, Localization.Description, Localization.MetaDescription, Localization.MetaTitle, Localization.MetaUrl);

            }
            catch (Exception exception)
            {
                _loggerManager.LogError(string.Format("Controller:Services, Action :PreviewLocalization, ErrorMessage: file: {0} ", exception));
            }
            return Json(resultCode, new JsonSerializerSettings());
        }

        #endregion

        #region Player Support

        [HttpPost]
        public async Task<IActionResult> UpdatePlayerSupportPageByLanguageId(PlayerSupport PlayerSupport)
        {
            int resultCode = 0;
            try
            {
                string path = Path.Combine(_hostingEnvironment.WebRootPath, "images/PlayerSupport/PS/");
                string deletePath = Path.Combine(_hostingEnvironment.WebRootPath, "images/PlayerSupport/PreviewImages/");
                string backUpPath = Path.Combine(_hostingEnvironment.WebRootPath, "images/PlayerSupport/Backup/");
                

                if (PlayerSupport.PlayerSupportHearder != null)
                {
                    FilePath(PlayerSupport.PlayerSupportHearder, path + "PlayerSupportHearder.png", deletePath + "PlayerSupportHearder.png", backUpPath + Guid.NewGuid() + "PlayerSupportHearder.png");
                }
                if (PlayerSupport.AnyPlatform != null)
                {
                    FilePath(PlayerSupport.AnyPlatform, path + "AnyPlatform.svg", deletePath + "AnyPlatform.svg", backUpPath + Guid.NewGuid() + "AnyPlatform.svg");
                }
                if (PlayerSupport.AnyLanguage != null)
                {
                    FilePath(PlayerSupport.AnyLanguage, path + "AnyLanguage.svg", deletePath + "AnyLanguage.svg", backUpPath + Guid.NewGuid() + "AnyLanguage.svg");
                }
                if (PlayerSupport.AnyComplexityLevel != null)
                {
                    FilePath(PlayerSupport.AnyComplexityLevel, path + "AnyComplexityLevel.svg", deletePath + "AnyComplexityLevel.svg", backUpPath + Guid.NewGuid() + "AnyComplexityLevel.svg");
                }
                if (PlayerSupport.CompletelyWhiteLebeled != null)
                {
                    FilePath(PlayerSupport.CompletelyWhiteLebeled, path + "CompletelyWhiteLebeled.svg", deletePath + "CompletelyWhiteLebeled.svg", backUpPath + Guid.NewGuid() + "CompletelyWhiteLebeled.svg");
                }
                if (PlayerSupport.CommunityModeration != null)
                {
                    FilePath(PlayerSupport.CommunityModeration, path + "CommunityModeration.svg", deletePath + "CommunityModeration.svg", backUpPath + Guid.NewGuid() + "CommunityModeration.svg");
                }
                if (PlayerSupport.CommunityNature != null)
                {
                    FilePath(PlayerSupport.CommunityNature, path + "CommunityNature.svg", deletePath + "CommunityNature.svg", backUpPath + Guid.NewGuid() + "CommunityNature.svg");
                }
                if (PlayerSupport.CommunityAnalysis != null)
                {
                    FilePath(PlayerSupport.CommunityAnalysis, path + "CommunityAnalysis.svg", deletePath + "CommunityAnalysis.svg", backUpPath + Guid.NewGuid() + "CommunityAnalysis.svg");
                }
                if (PlayerSupport.MachineTranslationforPlayerSupport != null)
                {
                    FilePath(PlayerSupport.MachineTranslationforPlayerSupport, path + "MachineTranslationforPlayerSupport.svg", deletePath + "MachineTranslationforPlayerSupport.svg", backUpPath + Guid.NewGuid() + "MachineTranslationforPlayerSupport.svg");
                }
                if (PlayerSupport.HumanTouch != null)
                {
                    FilePath(PlayerSupport.HumanTouch, path + "HumanTouch.png", deletePath + "HumanTouch.png", backUpPath + Guid.NewGuid() + "HumanTouch.png");
                }
                if (PlayerSupport.SupportHoned != null)
                {
                    FilePath(PlayerSupport.SupportHoned, path + "SupportHoned.png", deletePath + "SupportHoned.png", backUpPath + Guid.NewGuid() + "SupportHoned.png");
                }
                if (PlayerSupport.GamerforGamer != null)
                {
                    FilePath(PlayerSupport.GamerforGamer, path + "GamerforGamer.png", deletePath + "GamerforGamer.png", backUpPath + Guid.NewGuid() + "GamerforGamer.png");
                }
                if (PlayerSupport.Frame != null)
                {
                    FilePath(PlayerSupport.Frame, path + "Frame.svg", deletePath + "Frame.svg", backUpPath + Guid.NewGuid() + "Frame.svg");
                }

                resultCode = _masterService.UpdateHomePageByLanguageId(PlayerSupport.ModuleId, PlayerSupport.LanguageCode, PlayerSupport.Description, PlayerSupport.MetaDescription, PlayerSupport.MetaTitle, PlayerSupport.MetaUrl);

            }
            catch (Exception exception)
            {
                _loggerManager.LogError(string.Format("Controller:Services, Action :UpdatePlayerSupportPageByLanguageId, ErrorMessage: file: {0} ", exception));
            }
            return Json(resultCode, new JsonSerializerSettings());

        }

        [HttpPost]
        public async Task<IActionResult> UpdatePlayerSupportImagesToPreview(PlayerSupport PlayerSupport)
        {
            string path = Path.Combine(_hostingEnvironment.WebRootPath, "images/PlayerSupport/PreviewImages/");

            try
            {
                if (PlayerSupport.PlayerSupportHearder != null && PlayerSupport.Filename == "PlayerSupportHearder")
                {
                    FilePath(PlayerSupport.PlayerSupportHearder, path + "PlayerSupportHearder.png", "", "");
                }
                if (PlayerSupport.AnyPlatform != null && PlayerSupport.Filename == "AnyPlatform")
                {
                    FilePath(PlayerSupport.AnyPlatform, path + "AnyPlatform.svg", "", "");
                }
                if (PlayerSupport.AnyLanguage != null && PlayerSupport.Filename == "AnyLanguage")
                {
                    FilePath(PlayerSupport.AnyLanguage, path + "AnyLanguage.svg", "", "");
                }
                if (PlayerSupport.AnyComplexityLevel != null && PlayerSupport.Filename == "AnyComplexityLevel")
                {
                    FilePath(PlayerSupport.AnyComplexityLevel, path + "AnyComplexityLevel.svg", "", "");
                }
                if (PlayerSupport.CompletelyWhiteLebeled != null && PlayerSupport.Filename == "CompletelyWhiteLebeled")
                {
                    FilePath(PlayerSupport.CompletelyWhiteLebeled, path + "CompletelyWhiteLebeled.svg", "", "");
                }
                if (PlayerSupport.CommunityModeration != null && PlayerSupport.Filename == "CommunityModeration")
                {
                    FilePath(PlayerSupport.CommunityModeration, path + "CommunityModeration.svg", "", "");
                }
                if (PlayerSupport.CommunityNature != null && PlayerSupport.Filename == "CommunityNature")
                {
                    FilePath(PlayerSupport.CommunityNature, path + "CommunityNature.svg", "", "");
                }
                if (PlayerSupport.CommunityAnalysis != null && PlayerSupport.Filename == "CommunityAnalysis")
                {
                    FilePath(PlayerSupport.CommunityAnalysis, path + "CommunityAnalysis.svg", "", "");
                }
                if (PlayerSupport.MachineTranslationforPlayerSupport != null && PlayerSupport.Filename == "MachineTranslationforPlayerSupport")
                {
                    FilePath(PlayerSupport.MachineTranslationforPlayerSupport, path + "MachineTranslationforPlayerSupport.svg", "", "");
                }
                if (PlayerSupport.HumanTouch != null && PlayerSupport.Filename == "HumanTouch")
                {
                    FilePath(PlayerSupport.HumanTouch, path + "HumanTouch.png", "", "");
                }
                if (PlayerSupport.SupportHoned != null && PlayerSupport.Filename == "SupportHoned")
                {
                    FilePath(PlayerSupport.SupportHoned, path + "SupportHoned.png", "", "");
                }
                if (PlayerSupport.GamerforGamer != null && PlayerSupport.Filename == "GamerforGamer")
                {
                    FilePath(PlayerSupport.SupportHoned, path + "GamerforGamer.png", "", "");
                }
                if (PlayerSupport.Frame != null && PlayerSupport.Filename == "Frame")
                {
                    FilePath(PlayerSupport.Frame, path + "Frame.svg", "", "");
                }
            }
            catch (Exception exception)
            {
                _loggerManager.LogError(string.Format("Controller:Services, Action :UpdatePlayerSupportImagesToPreview, ErrorMessage: file: {0} ", exception));
            }

            return Json(1, new JsonSerializerSettings());
        }


        [HttpPost]
        public async Task<IActionResult> PreviewPlayerSupport(PlayerSupport PlayerSupport)
        {
            int resultCode = 0;
            string savedPath = Path.Combine(_hostingEnvironment.WebRootPath, "images/PlayerSupport/PS/");
            string previewPath = Path.Combine(_hostingEnvironment.WebRootPath, "images/PlayerSupport/PreviewImages/");

            try
            {
                if (PlayerSupport.PlayerSupportHearder != null)
                {
                    PlayerSupport.Description = PlayerSupport.Description.Replace(savedPath + "PlayerSupportHearder.png", previewPath + "PlayerSupportHearder.png");
                }
                if (PlayerSupport.AnyPlatform != null)
                {
                    PlayerSupport.Description = PlayerSupport.Description.Replace(savedPath + "AnyPlatform.svg", previewPath + "AnyPlatform.svg");
                }
                if (PlayerSupport.AnyLanguage != null)
                {
                    PlayerSupport.Description = PlayerSupport.Description.Replace(savedPath + "AnyLanguage.svg", previewPath + "AnyLanguage.svg");
                }
                if (PlayerSupport.AnyComplexityLevel != null)
                {
                    PlayerSupport.Description = PlayerSupport.Description.Replace(savedPath + "AnyComplexityLevel.svg", previewPath + "AnyComplexityLevel.svg");
                }
                if (PlayerSupport.CompletelyWhiteLebeled != null)
                {
                    PlayerSupport.Description = PlayerSupport.Description.Replace(savedPath + "CompletelyWhiteLebeled.svg", previewPath + "CompletelyWhiteLebeled.svg");
                }
                if (PlayerSupport.CommunityModeration != null)
                {
                    PlayerSupport.Description = PlayerSupport.Description.Replace(savedPath + "CommunityModeration.svg", previewPath + "CommunityModeration.svg");
                }
                if (PlayerSupport.CommunityNature != null)
                {
                    PlayerSupport.Description = PlayerSupport.Description.Replace(savedPath + "CommunityNature.svg", previewPath + "CommunityNature.svg");
                }
                if (PlayerSupport.CommunityAnalysis != null)
                {
                    PlayerSupport.Description = PlayerSupport.Description.Replace(savedPath + "CommunityAnalysis.svg", previewPath + "CommunityAnalysis.svg");
                }
                if (PlayerSupport.MachineTranslationforPlayerSupport != null)
                {
                    PlayerSupport.Description = PlayerSupport.Description.Replace(savedPath + "MachineTranslationforPlayerSupport.svg", previewPath + "MachineTranslationforPlayerSupport.svg");
                }
                if (PlayerSupport.HumanTouch != null)
                {
                    PlayerSupport.Description = PlayerSupport.Description.Replace(savedPath + "HumanTouch.png", previewPath + "HumanTouch.png");
                }
                if (PlayerSupport.SupportHoned != null)
                {
                    PlayerSupport.Description = PlayerSupport.Description.Replace(savedPath + "SupportHoned.png", previewPath + "SupportHoned.png");
                }
                if (PlayerSupport.GamerforGamer != null)
                {
                    PlayerSupport.Description = PlayerSupport.Description.Replace(savedPath + "GamerforGamer.png", previewPath + "GamerforGamer.png");
                }
                if (PlayerSupport.Frame != null)
                {
                    PlayerSupport.Description = PlayerSupport.Description.Replace(savedPath + "Frame.svg", previewPath + "Frame.svg");
                }

                resultCode = _masterService.UpdatePreviewPageByLanguageModuleId(PlayerSupport.ModuleId, PlayerSupport.LanguageCode, PlayerSupport.Description, PlayerSupport.MetaDescription, PlayerSupport.MetaTitle, PlayerSupport.MetaUrl);

            }
            catch (Exception exception)
            {
                _loggerManager.LogError(string.Format("Controller:Services, Action :PreviewPlayerSupport, ErrorMessage: file: {0} ", exception));
            }
            return Json(PlayerSupport, new JsonSerializerSettings());
        }

        #endregion


        #region Speech Tech

        [HttpPost]
        public async Task<IActionResult> UpdateSpeechTechPageByLanguageId(SpeechTech SpeechTech)
        {
            int resultCode = 0;
            try
            {
                string path = Path.Combine(_hostingEnvironment.WebRootPath, "images/SpeechTech/ST/");
                string deletePath = Path.Combine(_hostingEnvironment.WebRootPath, "images/SpeechTech/PreviewImages/");
                string backUpPath = Path.Combine(_hostingEnvironment.WebRootPath, "images/SpeechTech/Backup/");
                

                if (SpeechTech.SpeechTechHeader != null)
                {
                    FilePath(SpeechTech.SpeechTechHeader, path + "SpeechTechHeader.png", deletePath + "SpeechTechHeader.png", backUpPath + Guid.NewGuid() + "SpeechTechHeader.png");
                }
                if (SpeechTech.VolumenativeSpeaker != null)
                {
                    FilePath(SpeechTech.VolumenativeSpeaker, path + "VolumenativeSpeaker.svg", deletePath + "VolumenativeSpeaker.svg", backUpPath + Guid.NewGuid() + "VolumenativeSpeaker.svg");
                }
                if (SpeechTech.StreamlinedCapture != null)
                {
                    FilePath(SpeechTech.StreamlinedCapture, path + "StreamlinedCapture.svg", deletePath + "StreamlinedCapture.svg", backUpPath + Guid.NewGuid() + "StreamlinedCapture.svg");
                }
                if (SpeechTech.UnderstandYourVoiceDataRequirements != null)
                {
                    FilePath(SpeechTech.UnderstandYourVoiceDataRequirements, path + "UnderstandYourVoiceDataRequirements.svg", deletePath + "UnderstandYourVoiceDataRequirements.svg", backUpPath + Guid.NewGuid() + "UnderstandYourVoiceDataRequirements.svg");
                }
                if (SpeechTech.CreatedandUploadScriptsofRequiredVoiceData != null)
                {
                    FilePath(SpeechTech.CreatedandUploadScriptsofRequiredVoiceData, path + "CreatedandUploadScriptsofRequiredVoiceData.svg", deletePath + "CreatedandUploadScriptsofRequiredVoiceData.svg", backUpPath + Guid.NewGuid() + "CreatedandUploadScriptsofRequiredVoiceData.svg");
                }

                if (SpeechTech.Sendprojectouttodesiredvoicetalent != null)
                {
                    FilePath(SpeechTech.Sendprojectouttodesiredvoicetalent, path + "Sendprojectouttodesiredvoicetalent.svg", deletePath + "Sendprojectouttodesiredvoicetalent.svg", backUpPath + Guid.NewGuid() + "Sendprojectouttodesiredvoicetalent.svg");
                }
                if (SpeechTech.QualityAssetsandformatAudioFiles != null)
                {
                    FilePath(SpeechTech.QualityAssetsandformatAudioFiles, path + "QualityAssetsandformatAudioFiles.svg", deletePath + "QualityAssetsandformatAudioFiles.svg", backUpPath + Guid.NewGuid() + "QualityAssetsandformatAudioFiles.svg");
                }
                if (SpeechTech.DeliverAudioFiles != null)
                {
                    FilePath(SpeechTech.DeliverAudioFiles, path + "DeliverAudioFiles.svg", deletePath + "DeliverAudioFiles.svg", backUpPath + Guid.NewGuid() + "DeliverAudioFiles.svg");
                }

                if (SpeechTech.Frame != null)
                {
                    FilePath(SpeechTech.Frame, path + "Frame.svg", deletePath + "Frame.svg", backUpPath + Guid.NewGuid() + "Frame.svg");
                }

                resultCode = _masterService.UpdateHomePageByLanguageId(SpeechTech.ModuleId, SpeechTech.LanguageCode, SpeechTech.Description, SpeechTech.MetaDescription, SpeechTech.MetaTitle, SpeechTech.MetaUrl);
            }
            catch (Exception exception)
            {
                _loggerManager.LogError(string.Format("Controller:Services, Action :UpdateSpeechTechPageByLanguageId, ErrorMessage: file: {0} ", exception));
            }
            return Json(resultCode, new JsonSerializerSettings());
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSpeechTechImagesToPreview(SpeechTech SpeechTech)
        {
            string path = Path.Combine(_hostingEnvironment.WebRootPath, "images/SpeechTech/PreviewImages/");

            try
            {
                if (SpeechTech.SpeechTechHeader != null && SpeechTech.Filename == "SpeechTechHeader")
                {
                    FilePath(SpeechTech.SpeechTechHeader, path + "SpeechTechHeader.png", "", "");
                }
                if (SpeechTech.VolumenativeSpeaker != null && SpeechTech.Filename == "VolumenativeSpeaker")
                {
                    FilePath(SpeechTech.VolumenativeSpeaker, path + "VolumenativeSpeaker.svg", "", "");
                }
                if (SpeechTech.StreamlinedCapture != null && SpeechTech.Filename == "StreamlinedCapture")
                {
                    FilePath(SpeechTech.StreamlinedCapture, path + "StreamlinedCapture.svg", "", "");
                }
                if (SpeechTech.UnderstandYourVoiceDataRequirements != null && SpeechTech.Filename == "UnderstandYourVoiceDataRequirements")
                {
                    FilePath(SpeechTech.UnderstandYourVoiceDataRequirements, path + "UnderstandYourVoiceDataRequirements.svg", "", "");
                }
                if (SpeechTech.CreatedandUploadScriptsofRequiredVoiceData != null && SpeechTech.Filename == "CreatedandUploadScriptsofRequiredVoiceData")
                {
                    FilePath(SpeechTech.CreatedandUploadScriptsofRequiredVoiceData, path + "CreatedandUploadScriptsofRequiredVoiceData.svg", "", "");
                }
                if (SpeechTech.Sendprojectouttodesiredvoicetalent != null && SpeechTech.Filename == "Sendprojectouttodesiredvoicetalent")
                {
                    FilePath(SpeechTech.Sendprojectouttodesiredvoicetalent, path + "Sendprojectouttodesiredvoicetalent.svg", "", "");
                }
                if (SpeechTech.QualityAssetsandformatAudioFiles != null && SpeechTech.Filename == "QualityAssetsandformatAudioFiles")
                {
                    FilePath(SpeechTech.QualityAssetsandformatAudioFiles, path + "QualityAssetsandformatAudioFiles.svg", "", "");
                }
                if (SpeechTech.DeliverAudioFiles != null && SpeechTech.Filename == "DeliverAudioFiles")
                {
                    FilePath(SpeechTech.DeliverAudioFiles, path + "DeliverAudioFiles.svg", "", "");
                }

                if (SpeechTech.Frame != null && SpeechTech.Filename == "Frame")
                {
                    FilePath(SpeechTech.Frame, path + "Frame.svg", "", "");
                }
            }
            catch (Exception exception)
            {
                _loggerManager.LogError(string.Format("Controller:Services, Action :UpdateSpeechTechImagesToPreview, ErrorMessage: file: {0} ", exception));
            }

            return Json(1, new JsonSerializerSettings());
        }


        [HttpPost]
        public async Task<IActionResult> PreviewSpeechTech(SpeechTech SpeechTech)
        {
            int resultCode = 0;
            string savedPath = Path.Combine(_hostingEnvironment.WebRootPath, "images/SpeechTech/ST/");
            string previewPath = Path.Combine(_hostingEnvironment.WebRootPath, "images/SpeechTech/PreviewImages/");

            try
            {
                if (SpeechTech.SpeechTechHeader != null)
                {
                    SpeechTech.Description = SpeechTech.Description.Replace(savedPath + "SpeechTechHeader.png", previewPath + "SpeechTechHeader.png");
                }
                if (SpeechTech.VolumenativeSpeaker != null)
                {
                    SpeechTech.Description = SpeechTech.Description.Replace(savedPath + "VolumenativeSpeaker.svg", previewPath + "VolumenativeSpeaker.svg");
                }
                if (SpeechTech.StreamlinedCapture != null)
                {
                    SpeechTech.Description = SpeechTech.Description.Replace(savedPath + "StreamlinedCapture.svg", previewPath + "StreamlinedCapture.svg");
                }
                if (SpeechTech.UnderstandYourVoiceDataRequirements != null)
                {
                    SpeechTech.Description = SpeechTech.Description.Replace(savedPath + "UnderstandYourVoiceDataRequirements.svg", previewPath + "UnderstandYourVoiceDataRequirements.svg");
                }
                if (SpeechTech.CreatedandUploadScriptsofRequiredVoiceData != null)
                {
                    SpeechTech.Description = SpeechTech.Description.Replace(savedPath + "CreatedandUploadScriptsofRequiredVoiceData.svg", previewPath + "CreatedandUploadScriptsofRequiredVoiceData.svg");
                }
                if (SpeechTech.Sendprojectouttodesiredvoicetalent != null)
                {
                    SpeechTech.Description = SpeechTech.Description.Replace(savedPath + "Sendprojectouttodesiredvoicetalent.svg", previewPath + "Sendprojectouttodesiredvoicetalent.svg");
                }
                if (SpeechTech.QualityAssetsandformatAudioFiles != null)
                {
                    SpeechTech.Description = SpeechTech.Description.Replace(savedPath + "QualityAssetsandformatAudioFiles.svg", previewPath + "QualityAssetsandformatAudioFiles.svg");
                }
                if (SpeechTech.DeliverAudioFiles != null)
                {
                    SpeechTech.Description = SpeechTech.Description.Replace(savedPath + "DeliverAudioFiles.svg", previewPath + "DeliverAudioFiles.svg");
                }
                if (SpeechTech.Frame != null)
                {
                    SpeechTech.Description = SpeechTech.Description.Replace(savedPath + "Frame.svg", previewPath + "Frame.svg");
                }

                resultCode = _masterService.UpdatePreviewPageByLanguageModuleId(SpeechTech.ModuleId, SpeechTech.LanguageCode, SpeechTech.Description, SpeechTech.MetaDescription, SpeechTech.MetaTitle, SpeechTech.MetaUrl);

            }
            catch (Exception exception)
            {
                _loggerManager.LogError(string.Format("Controller:Services, Action :PreviewSpeechTech, ErrorMessage: file: {0} ", exception));
            }
            return Json(resultCode, new JsonSerializerSettings());
        }

        #endregion

        #region LocalizationQA

        [HttpPost]
        public async Task<IActionResult> UpdateLocalizationQAPageByLanguageId(LocalizationQA LocalizationQA)
        {
            int resultCode = 0;
            try
            {
                string path = Path.Combine(_hostingEnvironment.WebRootPath, "images/LocalizationQA/Local/");
                string deletePath = Path.Combine(_hostingEnvironment.WebRootPath, "images/LocalizationQA/PreviewImages/");
                string backUpPath = Path.Combine(_hostingEnvironment.WebRootPath, "images/LocalizationQA/Backup/");
               

                if (LocalizationQA.LocalizatonQAHearder != null)
                {
                    FilePath(LocalizationQA.LocalizatonQAHearder, path + "LocalizatonQAHearder.png", deletePath + "LocalizatonQAHearder.png", backUpPath + Guid.NewGuid() + "LocalizatonQAHearder.png");
                }
                if (LocalizationQA.LinguisticTesting != null)
                {
                    FilePath(LocalizationQA.LinguisticTesting, path + "LinguisticTesting.svg", deletePath + "LinguisticTesting.svg", backUpPath + Guid.NewGuid() + "LinguisticTesting.svg");
                }
                if (LocalizationQA.LinguisticCertification != null)
                {
                    FilePath(LocalizationQA.LinguisticCertification, path + "LinguisticCertification.svg", deletePath + "LinguisticCertification.svg", backUpPath + Guid.NewGuid() + "LinguisticCertification.svg");
                }
                if (LocalizationQA.Frame != null)
                {
                    FilePath(LocalizationQA.Frame, path + "Frame.svg", deletePath + "Frame.svg", backUpPath + Guid.NewGuid() + "Frame.svg");
                }

                resultCode = _masterService.UpdateHomePageByLanguageId(LocalizationQA.ModuleId, LocalizationQA.LanguageCode, LocalizationQA.Description, LocalizationQA.MetaDescription, LocalizationQA.MetaTitle, LocalizationQA.MetaUrl);

                
            }
            catch (Exception exception)
            {
                _loggerManager.LogError(string.Format("Controller:Services, Action :UpdateLocalizationQAPageByLanguageId, ErrorMessage: file: {0} ", exception));
            }
            return Json(resultCode, new JsonSerializerSettings());
        }

        [HttpPost]
        public async Task<IActionResult> UpdateLocalizationQAImagesToPreview(LocalizationQA LocalizationQA)
        {
            string path = Path.Combine(_hostingEnvironment.WebRootPath, "images/LocalizationQA/PreviewImages/");

            try
            {
                if (LocalizationQA.LocalizatonQAHearder != null && LocalizationQA.Filename == "LocalizatonQAHearder")
                {
                    FilePath(LocalizationQA.LocalizatonQAHearder, path + "LocalizatonQAHearder.png", "", "");
                }

                if (LocalizationQA.LinguisticTesting != null && LocalizationQA.Filename == "LinguisticTesting")
                {
                    FilePath(LocalizationQA.LinguisticTesting, path + "LinguisticTesting.svg", "", "");
                }

                if (LocalizationQA.LinguisticCertification != null && LocalizationQA.Filename == "LinguisticCertification")
                {
                    FilePath(LocalizationQA.LinguisticCertification, path + "LinguisticCertification.svg", "", "");
                }

                if (LocalizationQA.Frame != null && LocalizationQA.Filename == "Frame")
                {
                    FilePath(LocalizationQA.Frame, path + "Frame.svg", "", "");
                }

            }
            catch (Exception exception)
            {
                _loggerManager.LogError(string.Format("Controller:Services, Action :UpdateLocalizationQAImagesToPreview, ErrorMessage: file: {0} ", exception));
            }
            return Json(1, new JsonSerializerSettings());
        }


        [HttpPost]
        public async Task<IActionResult> PreviewLocalizationQA(LocalizationQA LocalizationQA)
        {
            int resultCode = 0;
            string savedPath = Path.Combine(_hostingEnvironment.WebRootPath, "images/LocalizationQA/Local/");
            string previewPath = Path.Combine(_hostingEnvironment.WebRootPath, "images/LocalizationQA/PreviewImages/");
            try
            {

                if (LocalizationQA.LocalizatonQAHearder != null)
                {
                    LocalizationQA.Description = LocalizationQA.Description.Replace(savedPath + "LocalizatonQAHearder.png", previewPath + "LocalizatonQAHearder.png");
                }
                if (LocalizationQA.LinguisticTesting != null)
                {
                    LocalizationQA.Description = LocalizationQA.Description.Replace(savedPath + "LinguisticTesting.svg", previewPath + "LinguisticTesting.svg");
                }
                if (LocalizationQA.LinguisticCertification != null)
                {
                    LocalizationQA.Description = LocalizationQA.Description.Replace(savedPath + "LinguisticCertification.svg", previewPath + "LinguisticCertification.svg");
                }

                if (LocalizationQA.Frame != null)
                {
                    LocalizationQA.Description = LocalizationQA.Description.Replace(savedPath + "Frame.svg", previewPath + "Frame.svg");
                }

                resultCode = _masterService.UpdatePreviewPageByLanguageModuleId(LocalizationQA.ModuleId, LocalizationQA.LanguageCode, LocalizationQA.Description, LocalizationQA.MetaDescription, LocalizationQA.MetaTitle, LocalizationQA.MetaUrl);

            }
            catch (Exception exception)
            {
                _loggerManager.LogError(string.Format("Controller:Services, Action :PreviewLocalizationQA, ErrorMessage: file: {0} ", exception));
            }
            return Json(resultCode, new JsonSerializerSettings());
        }

        #endregion

        #region TalentSolution

        [HttpPost]
        public async Task<IActionResult> UpdateTalentSolutionPageByLanguageId(TalentSolution TalentSolution)
        {
            int resultCode = 0;
            try
            {
                string path = Path.Combine(_hostingEnvironment.WebRootPath, "images/TalentSolution/TS/");
                string deletePath = Path.Combine(_hostingEnvironment.WebRootPath, "images/TalentSolution/PreviewImages/");
                string backUpPath = Path.Combine(_hostingEnvironment.WebRootPath, "images/TalentSolution/Backup/");
                
                if (TalentSolution.TalentSolutionHearder != null)
                {
                    FilePath(TalentSolution.TalentSolutionHearder, path + "TalentSolutionHearder.png", deletePath + "TalentSolutionHearder.png", backUpPath + Guid.NewGuid() + "TalentSolutionHearder.png");
                }

                if (TalentSolution.VettedAndVerified != null)
                {
                    FilePath(TalentSolution.VettedAndVerified, path + "VettedAndVerified.png", deletePath + "VettedAndVerified.png", backUpPath + Guid.NewGuid() + "VettedAndVerified.png");
                }
                if (TalentSolution.FasterDevlivery != null)
                {
                    FilePath(TalentSolution.FasterDevlivery, path + "FasterDevlivery.png", deletePath + "FasterDevlivery.png", backUpPath + Guid.NewGuid() + "FasterDevlivery.png");
                }
                if (TalentSolution.FlexibleSolutions != null)
                {
                    FilePath(TalentSolution.FlexibleSolutions, path + "FlexibleSolutions.png", deletePath + "FlexibleSolutions.png", backUpPath + Guid.NewGuid() + "FlexibleSolutions.png");
                }
                if (TalentSolution.Unity != null)
                {
                    FilePath(TalentSolution.Unity, path + "Unity.png", deletePath + "Unity.png", backUpPath + Guid.NewGuid() + "Unity.png");
                }
                if (TalentSolution.Unreal != null)
                {
                    FilePath(TalentSolution.Unreal, path + "Unreal.png", deletePath + "Unreal.png", backUpPath + Guid.NewGuid() + "Unreal.png");
                }
                if (TalentSolution.GameMakerStudio != null)
                {
                    FilePath(TalentSolution.GameMakerStudio, path + "GameMakerStudio.png", deletePath + "GameMakerStudio.png", backUpPath + Guid.NewGuid() + "GameMakerStudio.png");
                }
                if (TalentSolution.HTML != null)
                {
                    FilePath(TalentSolution.HTML, path + "HTML.png", deletePath + "HTML.png", backUpPath + Guid.NewGuid() + "HTML.png");
                }
                if (TalentSolution.GameDevelopment != null)
                {
                    FilePath(TalentSolution.GameDevelopment, path + "GameDevelopment.png", deletePath + "GameDevelopment.png", backUpPath + Guid.NewGuid() + "GameDevelopment.png");
                }
                if (TalentSolution.CreativeandDesign != null)
                {
                    FilePath(TalentSolution.CreativeandDesign, path + "CreativeandDesign.png", deletePath + "CreativeandDesign.png", backUpPath + Guid.NewGuid() + "CreativeandDesign.png");
                }
                if (TalentSolution.Infrastructure != null)
                {
                    FilePath(TalentSolution.Infrastructure, path + "Infrastructure.png", deletePath + "Infrastructure.png", backUpPath + Guid.NewGuid() + "Infrastructure.png");
                }
                if (TalentSolution.RiskAndSecurity != null)
                {
                    FilePath(TalentSolution.RiskAndSecurity, path + "RiskAndSecurity.png", deletePath + "RiskAndSecurity.png", backUpPath + Guid.NewGuid() + "RiskAndSecurity.png");
                }
                if (TalentSolution.DataManagement != null)
                {
                    FilePath(TalentSolution.DataManagement, path + "DataManagement.png", deletePath + "DataManagement.png", backUpPath + Guid.NewGuid() + "DataManagement.png");
                }
                if (TalentSolution.ProjectManagement != null)
                {
                    FilePath(TalentSolution.ProjectManagement, path + "ProjectManagement.png", deletePath + "ProjectManagement.png", backUpPath + Guid.NewGuid() + "ProjectManagement.png");
                }

                if (TalentSolution.Frame != null)
                {
                    FilePath(TalentSolution.Frame, path + "Frame.svg", deletePath + "Frame.svg", backUpPath + Guid.NewGuid() + "Frame.svg");
                }

                resultCode = _masterService.UpdateHomePageByLanguageId(TalentSolution.ModuleId, TalentSolution.LanguageCode, TalentSolution.Description, TalentSolution.MetaDescription, TalentSolution.MetaTitle, TalentSolution.MetaUrl);

               
            }
            catch (Exception exception)
            {
                _loggerManager.LogError(string.Format("Controller:Services, Action :UpdateTalentSolutionPageByLanguageId, ErrorMessage: file: {0} ", exception));
            }
            return Json(resultCode, new JsonSerializerSettings());
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTalentSolutionImagesToPreview(TalentSolution TalentSolution)
        {
            string path = Path.Combine(_hostingEnvironment.WebRootPath, "images/TalentSolution/PreviewImages/");

            try
            {
                if (TalentSolution.TalentSolutionHearder != null && TalentSolution.Filename == "TalentSolutionHearder")
                {
                    FilePath(TalentSolution.TalentSolutionHearder, path + "TalentSolutionHearder.png", "", "");
                }

                if (TalentSolution.VettedAndVerified != null && TalentSolution.Filename == "VettedAndVerified")
                {
                    FilePath(TalentSolution.VettedAndVerified, path + "VettedAndVerified.png", "", "");
                }
                if (TalentSolution.FasterDevlivery != null && TalentSolution.Filename == "FasterDevlivery")
                {
                    FilePath(TalentSolution.FasterDevlivery, path + "FasterDevlivery.png", "", "");
                }
                if (TalentSolution.FlexibleSolutions != null && TalentSolution.Filename == "FlexibleSolutions")
                {
                    FilePath(TalentSolution.FlexibleSolutions, path + "FlexibleSolutions.png", "", "");
                }
                if (TalentSolution.Unity != null && TalentSolution.Filename == "Unity")
                {
                    FilePath(TalentSolution.Unity, path + "Unity.png", "", "");
                }
                if (TalentSolution.Unreal != null && TalentSolution.Filename == "Unreal")
                {
                    FilePath(TalentSolution.Unreal, path + "Unreal.png", "", "");
                }
                if (TalentSolution.GameMakerStudio != null && TalentSolution.Filename == "GameMakerStudio")
                {
                    FilePath(TalentSolution.GameMakerStudio, path + "GameMakerStudio.png", "", "");
                }
                if (TalentSolution.HTML != null && TalentSolution.Filename == "HTML")
                {
                    FilePath(TalentSolution.HTML, path + "HTML.png", "", "");
                }
                if (TalentSolution.GameDevelopment != null && TalentSolution.Filename == "GameDevelopment")
                {
                    FilePath(TalentSolution.GameDevelopment, path + "GameDevelopment.png", "", "");
                }
                if (TalentSolution.CreativeandDesign != null && TalentSolution.Filename == "CreativeandDesign")
                {
                    FilePath(TalentSolution.CreativeandDesign, path + "CreativeandDesign.png", "", "");
                }
                if (TalentSolution.Infrastructure != null && TalentSolution.Filename == "Infrastructure")
                {
                    FilePath(TalentSolution.Infrastructure, path + "Infrastructure.png", "", "");
                }
                if (TalentSolution.RiskAndSecurity != null && TalentSolution.Filename == "RiskAndSecurity")
                {
                    FilePath(TalentSolution.RiskAndSecurity, path + "RiskAndSecurity.png", "", "");
                }
                if (TalentSolution.DataManagement != null && TalentSolution.Filename == "DataManagement")
                {
                    FilePath(TalentSolution.DataManagement, path + "DataManagement.png", "", "");
                }
                if (TalentSolution.ProjectManagement != null && TalentSolution.Filename == "ProjectManagement")
                {
                    FilePath(TalentSolution.ProjectManagement, path + "ProjectManagement.png", "", "");
                }

                if (TalentSolution.Frame != null && TalentSolution.Filename == "Frame")
                {
                    FilePath(TalentSolution.Frame, path + "Frame.svg", "", "");
                }
            }
            catch (Exception exception)
            {
                _loggerManager.LogError(string.Format("Controller:Services, Action :UpdateTalentSolutionImagesToPreview, ErrorMessage: file: {0} ", exception));
            }

            return Json(1, new JsonSerializerSettings());
        }


        [HttpPost]
        public async Task<IActionResult> PreviewTalentSolution(TalentSolution TalentSolution)
        {
            int resultCode = 0;
            string savedPath = Path.Combine(_hostingEnvironment.WebRootPath, "images/TalentSolution/TS/");
            string previewPath = Path.Combine(_hostingEnvironment.WebRootPath, "images/TalentSolution/PreviewImages/");


            try
            {
                if (TalentSolution.TalentSolutionHearder != null)
                {
                    TalentSolution.Description = TalentSolution.Description.Replace(savedPath + "TalentSolutionHearder.png", previewPath + "TalentSolutionHearder.png");
                }

                if (TalentSolution.VettedAndVerified != null)
                {
                    TalentSolution.Description = TalentSolution.Description.Replace(savedPath + "VettedAndVerified.png", previewPath + "VettedAndVerified.png");
                }
                if (TalentSolution.FasterDevlivery != null)
                {
                    TalentSolution.Description = TalentSolution.Description.Replace(savedPath + "FasterDevlivery.png", previewPath + "FasterDevlivery.png");
                }
                if (TalentSolution.FlexibleSolutions != null)
                {
                    TalentSolution.Description = TalentSolution.Description.Replace(savedPath + "FlexibleSolutions.png", previewPath + "FlexibleSolutions.png");
                }
                if (TalentSolution.Unity != null)
                {
                    TalentSolution.Description = TalentSolution.Description.Replace(savedPath + "Unity.png", previewPath + "Unity.png");
                }
                if (TalentSolution.Unreal != null)
                {
                    TalentSolution.Description = TalentSolution.Description.Replace(savedPath + "Unreal.png", previewPath + "Unreal.png");
                }
                if (TalentSolution.GameMakerStudio != null)
                {
                    TalentSolution.Description = TalentSolution.Description.Replace(savedPath + "GameMakerStudio.png", previewPath + "GameMakerStudio.png");
                }
                if (TalentSolution.HTML != null)
                {
                    TalentSolution.Description = TalentSolution.Description.Replace(savedPath + "HTML.png", previewPath + "HTML.png");
                }
                if (TalentSolution.GameDevelopment != null)
                {
                    TalentSolution.Description = TalentSolution.Description.Replace(savedPath + "GameDevelopment.png", previewPath + "GameDevelopment.png");
                }
                if (TalentSolution.CreativeandDesign != null)
                {
                    TalentSolution.Description = TalentSolution.Description.Replace(savedPath + "CreativeandDesign.png", previewPath + "CreativeandDesign.png");
                }
                if (TalentSolution.Infrastructure != null)
                {
                    TalentSolution.Description = TalentSolution.Description.Replace(savedPath + "Infrastructure.png", previewPath + "Infrastructure.png");
                }
                if (TalentSolution.RiskAndSecurity != null)
                {
                    TalentSolution.Description = TalentSolution.Description.Replace(savedPath + "RiskAndSecurity.png", previewPath + "RiskAndSecurity.png");
                }
                if (TalentSolution.DataManagement != null)
                {
                    TalentSolution.Description = TalentSolution.Description.Replace(savedPath + "DataManagement.png", previewPath + "DataManagement.png");
                }
                if (TalentSolution.ProjectManagement != null)
                {
                    TalentSolution.Description = TalentSolution.Description.Replace(savedPath + "ProjectManagement.png", previewPath + "ProjectManagement.png");
                }

                if (TalentSolution.Frame != null)
                {
                    TalentSolution.Description = TalentSolution.Description.Replace(savedPath + "Frame.svg", previewPath + "Frame.svg");
                }

                resultCode = _masterService.UpdatePreviewPageByLanguageModuleId(TalentSolution.ModuleId, TalentSolution.LanguageCode, TalentSolution.Description, TalentSolution.MetaDescription, TalentSolution.MetaTitle, TalentSolution.MetaUrl);

            }
            catch (Exception exception)
            {
                _loggerManager.LogError(string.Format("Controller:Services, Action :PreviewTalentSolution, ErrorMessage: file: {0} ", exception));
            }
            return Json(resultCode, new JsonSerializerSettings());
        }

        #endregion

        public async void FilePath(IFormFile file, string path, string deletePath, string backUpPath)
        {
            _loggerManager.LogInfo(string.Format("Controller:Services, Action :FilePath Data: file: {0}, path: {1}, backUpPath:{2} ", file.Name, path, backUpPath));

            try
            {
                if (!path.Contains("PreviewImages"))
                {
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Move(path, backUpPath);
                    }
                }
                if (file != null)
                {
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
            catch (Exception exception)
            {

                _loggerManager.LogError(string.Format("Controller:Services, Action :FilePath ErrorMessage: file: {0} ", exception));

            }

        }

        #region Views

        [Route("qualityassurance")]
        [Route("{culture}/qualityassurance")]
        public IActionResult GetService_qualityAsurance(string culture)
        {
            MasterPage masterPage = new MasterPage();
            try
            {
                DataTable dtContent = _masterService.GetModuleContent("Services_QA", (culture == null ? "en-US" : culture));
                masterPage.HtmlContent = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Services_QA")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
                ViewData["Header"] = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Header")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
                ViewData["Footer"] = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Footer")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();

            }
            catch (Exception exception)
            {
                _loggerManager.LogError(string.Format("Controller:Services, Action :GetService_qualityAsurance, ErrorMessage: file: {0} ", exception));
            }
            return View("QualityAssurance", masterPage);
        }

        [Route("audioproduction")]
        [Route("{culture}/audioproduction")]
        public IActionResult GetService_audioProduction(string culture)
        {
            MasterPage masterPage = new MasterPage();
            try
            {
                DataTable dtContent = _masterService.GetModuleContent("Audio Production", (culture == null ? "en-US" : culture));
                masterPage.HtmlContent = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Audio Production")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
                ViewData["Header"] = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Header")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
                ViewData["Footer"] = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Footer")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();

            }
            catch (Exception exception)
            {
                _loggerManager.LogError(string.Format("Controller:Services, Action :GetService_audioProduction, ErrorMessage: file: {0} ", exception));
            }
            return View("AudioProduction", masterPage);
        }

        //[Route("Localization")]
        //[Route("{culture}/Localization")]
        //public IActionResult GetService_Localization(string culture)
        //{
        //    MasterPage masterPage = new MasterPage();
        //    try
        //    {
        //        DataTable dtContent = _masterService.GetModuleContent("Localization", (culture == null ? "en-US" : culture));
        //        masterPage.HtmlContent = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("LocalizationQA")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
        //        ViewData["Header"] = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Header")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
        //        ViewData["Footer"] = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Footer")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();

        //    }
        //    catch (Exception exception)
        //    {
        //        _loggerManager.LogError(string.Format("Controller:Services, Action :GetService_Localization, ErrorMessage: file: {0} ", exception));
        //    }
        //    return View("Localization", masterPage);
        //}

        [Route("speechtech")]
        [Route("{culture}/speechtech")]
        public IActionResult GetService_SpeechTech(string culture)
        {
            MasterPage masterPage = new MasterPage();
            try
            {
                DataTable dtContent = _masterService.GetModuleContent("SpeechTech", (culture == null ? "en-US" : culture));
                masterPage.HtmlContent = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("SpeechTech")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
                ViewData["Header"] = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Header")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
                ViewData["Footer"] = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Footer")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();

            }
            catch (Exception exception)
            {
                _loggerManager.LogError(string.Format("Controller:Services, Action :GetService_SpeechTech, ErrorMessage: file: {0} ", exception));
            }
            return View("SpeechTech", masterPage);
        }

        [Route("productdevelopment")]
        [Route("{culture}/productdevelopment")]
        public IActionResult GetService_ProdDev(string culture)
        {
            MasterPage masterPage = new MasterPage();
            try
            {
                DataTable dtContent = _masterService.GetModuleContent("ProductDevelopment", (culture == null ? "en-US" : culture));
                masterPage.HtmlContent = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("ProductDevelopment")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
                ViewData["Header"] = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Header")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
                ViewData["Footer"] = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Footer")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();

            }
            catch (Exception exception)
            {
                _loggerManager.LogError(string.Format("Controller:Services, Action :GetService_ProdDev, ErrorMessage: file: {0} ", exception));
            }
            return View("ProductDevelopment", masterPage);
        }

        [Route("localizationqa")]
        [Route("{culture}/localizationqa")]
        public IActionResult GetService_Localization_1(string culture)
        {
            MasterPage masterPage = new MasterPage();
            try
            {
                DataTable dtContent = _masterService.GetModuleContent("LocalizationQA", (culture == null ? "en-US" : culture));
                masterPage.HtmlContent = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("LocalizationQA")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
                ViewData["Header"] = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Header")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
                ViewData["Footer"] = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Footer")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();

            }
            catch (Exception exception)
            {
                _loggerManager.LogError(string.Format("Controller:Services, Action :GetService_Localization_1, ErrorMessage: file: {0} ", exception));
            }
            return View("LocalizationQA", masterPage);
        }

        [Route("playersupport")]
        [Route("{culture}/playersupport")]
        public IActionResult GetService_PlayerSupport(string culture)
        {
            MasterPage masterPage = new MasterPage();
            try
            {
                DataTable dtContent = _masterService.GetModuleContent("PlayerSupport", (culture == null ? "en-US" : culture));
                masterPage.HtmlContent = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("PlayerSupport")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
                ViewData["Header"] = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Header")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
                ViewData["Footer"] = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Footer")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();

            }
            catch (Exception exception)
            {
                _loggerManager.LogError(string.Format("Controller:Services, Action :GetService_PlayerSupport, ErrorMessage: file: {0} ", exception));
            }
            return View("PlayerSupport", masterPage);
        }

        [Route("talentsolution")]
        [Route("{culture}/talentsolution")]
        public IActionResult GetService_TalentSolution(string culture)
        {
            MasterPage masterPage = new MasterPage();
            try
            {
                DataTable dtContent = _masterService.GetModuleContent("TalentSolution", (culture == null ? "en-US" : culture));
                masterPage.HtmlContent = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("TalentSolution")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
                ViewData["Header"] = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Header")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
                ViewData["Footer"] = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Footer")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();

            }
            catch (Exception exception)
            {
                _loggerManager.LogError(string.Format("Controller:Services, Action :GetService_TalentSolution, ErrorMessage: file: {0} ", exception));
            }
            return View("TalentSolution", masterPage);
        }

        #endregion

        [Route("servicesbackup")]
        [HttpGet]
        public IActionResult DownloadBackup(int ModuleId, string LanguageCode,string ServiceName)
        {

            string serviceBackUpPath = Path.Combine(_hostingEnvironment.WebRootPath, "images/"+ ServiceName + "/Backup/");
            string filePath = ServiceName+"_" + Guid.NewGuid() + ".html";
            MasterPage master = _masterService.GetModuleContentById(ModuleId, LanguageCode);
            System.IO.File.WriteAllText(serviceBackUpPath + filePath, master.HtmlContent);
            return Json(filePath, new JsonSerializerSettings());
        }

    }
}
