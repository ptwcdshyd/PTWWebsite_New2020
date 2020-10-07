using System;
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
using Newtonsoft.Json;
using PTW.DataAccess.Models;
using PTW.DataAccess.Services;
using PTW.DBAccess;
using System.Security.Claims;
using LoggerService;

namespace PTWWebsite2.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsEventService _NewsEventService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IMasterService _masterService;
        private readonly ILoggerManager _loggerManager;

        public NewsController(INewsEventService newsEventService, IHostingEnvironment hostingEnvironment, IMasterService masterService, ILoggerManager loggerManager)
        {
            _NewsEventService = newsEventService;
            _hostingEnvironment = hostingEnvironment;
            _masterService = masterService;
            _loggerManager = loggerManager;
        }



        [Route("News")]
        [Route("{culture}/News")]
        public IActionResult News(string culture)
        {
            MasterPage masterPage = new MasterPage();
            DataTable dtContent = _masterService.GetModuleContent("Home", (culture == null ? "en-US" : culture));
            masterPage.HtmlContent = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Home")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
            ViewData["Header"] = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Header")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
            ViewData["Footer"] = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Footer")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();


            NewsEvents newsEvents = new NewsEvents();
            newsEvents.News = _NewsEventService.GetAllNewsDetails();
            newsEvents.Events = _NewsEventService.GetAllEventDetails();
            return View(newsEvents);
        }



        [Route("AllNews")]
        [Route("{culture}/AllNews")]
        public IActionResult AllNews()
        {


            NewsEvents newsEvents = new NewsEvents();
            newsEvents.News = _NewsEventService.GetAllNewsDetails();

            return Json(newsEvents.News, new JsonSerializerSettings());
        }

        [Route("News/{NewsTitleUrl}")]
        [Route("{cultures}/News/{NewsTitleUrl}")]
        public IActionResult NewsArticles(string cultures, string NewsTitleUrl)
        {
            MasterPage masterPage = new MasterPage();
            DataTable dtContent = _masterService.GetModuleContent("Home", (cultures == null ? "en-US" : cultures));
            masterPage.HtmlContent = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Home")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
            ViewData["Header"] = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Header")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
            ViewData["Footer"] = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Footer")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();

            if (!string.IsNullOrEmpty(NewsTitleUrl))
            {
                NewsTitleUrl = NewsTitleUrl.Replace("-", " ");
            }
            NewsDetails newsDetails = _NewsEventService.GetNewsDetailsByTitle(NewsTitleUrl, cultures == "" ? "en-US" : cultures);

            return View(newsDetails);
        }



        [Authorize]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        [Route("AddNews")]
        [HttpGet]
        public IActionResult AddNewsEvents()
        {
            return View();
        }

        [Authorize]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        [Route("EditNews")]
        public IActionResult EditNewsEvents()
        {
            MasterPage masterPage1 = _masterService.GetLanguageandModules();
            News news = new News();

            news = _NewsEventService.GetAllNewsAndEventDetailsForUpdate();

            news.Languages = masterPage1.LanguageList;
            return View(news);
        }

        [Route("Newss/{NewsTitleUrl}")]
        [Authorize]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpPost]
        public IActionResult AddNews(News news)
        {
            string headerImageFolder = string.Empty;
            string LorgeImageFolder = string.Empty;
            string smallImageFolder = string.Empty;

            headerImageFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images/News/2020/Header");
            LorgeImageFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images/News/2020/Large");
            smallImageFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images/News/2020/Small");

            var file1 = Request.Form.Files.Count() == 0 ? null : Request.Form.Files[0];
            news.HeaderImageUrl = "/Images/News/2020/Header/";
            news.HeaderImageName = file1.FileName;
            string filePath1 = Path.Combine(headerImageFolder, file1.FileName);
            if (System.IO.File.Exists(filePath1))
            {
                System.IO.File.Delete(filePath1);
            }
            file1.CopyToAsync(new FileStream(filePath1, FileMode.Create));

            var file2 = Request.Form.Files[1];
            news.LongImageUrl = "/Images/News/2020/Large/";
            news.LongImageName = file2.FileName;
            string filePath2 = Path.Combine(LorgeImageFolder, file2.FileName);
            if (System.IO.File.Exists(filePath2))
            {
                System.IO.File.Delete(filePath2);
            }
            file2.CopyToAsync(new FileStream(filePath2, FileMode.Create));

            var file3 = Request.Form.Files[2];
            news.ShortImageUrl = "/Images/News/2020/Small/";
            news.ShortImageName = file3.FileName;
            string filePath3 = Path.Combine(smallImageFolder, file3.FileName);
            if (System.IO.File.Exists(filePath3))
            {
                System.IO.File.Delete(filePath3);
            }
            file3.CopyToAsync(new FileStream(filePath3, FileMode.Create));

            string newsXmlData = CustomNewsXml(news);
            bool result = _NewsEventService.AddUpdateNews(newsXmlData, news.Description);
            return Json(result, new JsonSerializerSettings());
        }


        [Route("EditNewss/GetNews")]
        [HttpPost]
        public IActionResult GetNews(News news1)
        {
            MasterPage masterPage1 = _masterService.GetLanguageandModules();

            //News news = new News();
            ModelState.Clear();
            News ddlNewsTitles = _NewsEventService.GetAllNewsAndEventDetailsForUpdate();
            News news = _NewsEventService.GetNewsAndEventDetailsByNewsId(news1.NewsId, news1.LanguageCode == "Please Select" ? "en-US" : news1.LanguageCode);
            news.NewsId = news1.NewsId;//ddl selected value
            news.NewsListUpdate = ddlNewsTitles.NewsListUpdate;
            news.Languages = masterPage1.LanguageList;
            news.LanguageCode = news1.LanguageCode;

            return View("EditNewsEvents", news);
        }

        [Route("EditNews/{NewsTitleUrl}")]
        [HttpPost]
        public IActionResult UpdateNews(News news)
        {
            string headerImageFolder = string.Empty;
            string LorgeImageFolder = string.Empty;
            string smallImageFolder = string.Empty;


            string[] uploadedFiles = new string[] { };
            if (news.FileString != null)
            {
                uploadedFiles = news.FileString.Split(',');
            }

            if (uploadedFiles.Any(z => z == "file1"))
            {
                var HeaderImage = Request.Form.Files.Count() == 0 ? null : Request.Form.Files[0];
                headerImageFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images/News/2020/Header");
                news.HeaderImageUrl = "/Images/News/2020/Header/";
                news.HeaderImageName = HeaderImage.FileName;
                string filePath1 = Path.Combine(headerImageFolder, HeaderImage.FileName);
                if (System.IO.File.Exists(filePath1))
                {
                    System.IO.File.Delete(filePath1);
                }
                HeaderImage.CopyTo(new FileStream(filePath1, FileMode.Create));
            }
            if (uploadedFiles.Any(z => z == "file2"))
            {
                var LongerImage = Request.Form.Files[Request.Form.Files.Count() > 1 ? 1 : 0];
                LorgeImageFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images/News/2020/Large");
                news.LongImageUrl = "/Images/News/2020/Large/";
                news.LongImageName = LongerImage.FileName;
                string filePath2 = Path.Combine(LorgeImageFolder, LongerImage.FileName);
                if (System.IO.File.Exists(filePath2))
                {
                    System.IO.File.Delete(filePath2);
                }
                LongerImage.CopyTo(new FileStream(filePath2, FileMode.Create));
            }
            if (uploadedFiles.Any(z => z == "file3"))
            {
                var ShorterImage = Request.Form.Files[Request.Form.Files.Count() > 2 ? 2 : 1];
                smallImageFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images/News/2020/Small");
                news.ShortImageUrl = "/Images/News/2020/Small/";
                news.ShortImageName = ShorterImage.FileName;
                string filePath3 = Path.Combine(smallImageFolder, ShorterImage.FileName);
                if (System.IO.File.Exists(filePath3))
                {
                    System.IO.File.Delete(filePath3);
                }
                ShorterImage.CopyTo(new FileStream(filePath3, FileMode.Create));
            }
            string newsXmlData = CustomNewsXml(news);
            bool result = _NewsEventService.UpdateNews(news.EditNewsId, newsXmlData, news.Description, news.LanguageCode == null ? "en-US" : news.LanguageCode);
            return Json(result, new JsonSerializerSettings());
        }

        [NonAction]
        public string CustomNewsXml(News news)
        {
            StringBuilder xml = new StringBuilder();
            xml.Append("<News>");
            if (news != null)
            {
                xml.Append(string.Format("<PublishedDate>{0}</PublishedDate>", Convert.ToDateTime(news.PublishedDate)));
                //xml.Append(string.Format("<Description>{0}</Description>", news.Description));
                xml.Append(string.Format("<Title>{0}</Title>", news.NewsTitle));
                xml.Append(string.Format("<ShortDescription>{0}</ShortDescription>", news.UrlTitle.Replace("-", " ")));
                xml.Append(string.Format("<Topic>{0}</Topic>", news.Topic));
                xml.Append(string.Format("<OnNewWebsiteNow>{0}</OnNewWebsiteNow>", Convert.ToInt32(news.OnNewWebsiteNow)));
                xml.Append(string.Format("<SuitedForHomePage>{0}</SuitedForHomePage>", Convert.ToInt32(news.SuitedForHomePage)));
                xml.Append(string.Format("<CustomerExperience>{0}</CustomerExperience>", Convert.ToInt32(news.CustomerExperience)));
                xml.Append(string.Format("<QualityAssurance>{0}</QualityAssurance>", Convert.ToInt32(news.QualityAssurance)));
                xml.Append(string.Format("<Localization>{0}</Localization>", Convert.ToInt32(news.Localization)));
                xml.Append(string.Format("<AudioProduction>{0}</AudioProduction>", Convert.ToInt32(news.AudioProduction)));
                xml.Append(string.Format("<Engineering>{0}</Engineering>", Convert.ToInt32(news.Engineering)));
                xml.Append(string.Format("<Event>{0}</Event>", news.Event));
                xml.Append(string.Format("<HeaderImageName>{0}</HeaderImageName>", news.HeaderImageName));
                xml.Append(string.Format("<HeaderImageUrl>{0}</HeaderImageUrl>", news.HeaderImageUrl));
                xml.Append(string.Format("<LongImageName>{0}</LongImageName>", news.LongImageName));
                xml.Append(string.Format("<LongImageUrl>{0}</LongImageUrl>", news.LongImageUrl));
                xml.Append(string.Format("<ShortImageName>{0}</ShortImageName>", news.ShortImageName));
                xml.Append(string.Format("<ShortImageUrl>{0}</ShortImageUrl>", news.ShortImageUrl));
                xml.Append(string.Format("<ShortOrder>{0}</ShortOrder>", news.ShortOrder));
                xml.Append(string.Format("<EventStartDate>{0}</EventStartDate>", news.EventStartDate != "" ? Convert.ToDateTime(news.EventStartDate) : DateTime.Now));
                xml.Append(string.Format("<EventEndDate>{0}</EventEndDate>", news.EventEndDate != "" ? Convert.ToDateTime(news.EventStartDate) : DateTime.Now));
                xml.Append(string.Format("<IsActive>{0}</IsActive>", Convert.ToInt32(news.ActiveStatus)));
                xml.Append(string.Format("<CreatedBy>{0}</CreatedBy>", 1));
                xml.Append(string.Format("<MetaTitle>{0}</MetaTitle>", news.MetaTitle));
                xml.Append(string.Format("<MetaDescription>{0}</MetaDescription>", news.MetaDescription));

            }
            xml.Append("</News>");
            return xml.ToString();
        }

        //[Authorize]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        [Route("NewsDashboard")]
        [HttpGet]
        public IActionResult NewsDashboard()
        {
            MasterPage masterPage1 = _masterService.GetLanguageandModules();
            News news = new News();

            news = _NewsEventService.GetAllNewsAndEventDetailsForUpdate();

            news.Languages = masterPage1.LanguageList;
            return View(news);
        }


        [Authorize]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpPost]
        public IActionResult UpdateNewsDashboard(News news)
        {
            string headerImageFolder = string.Empty;
            string LorgeImageFolder = string.Empty;
            string smallImageFolder = string.Empty;

            headerImageFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images/News/2020/Header");
            LorgeImageFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images/News/2020/Large");
            smallImageFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images/News/2020/Small");

            var file1 = Request.Form.Files.Count() == 0 ? null : Request.Form.Files[0];
            news.HeaderImageUrl = "/Images/News/2020/Header/";
            news.HeaderImageName = file1.FileName;
            string filePath1 = Path.Combine(headerImageFolder, file1.FileName);
            if (System.IO.File.Exists(filePath1))
            {
                System.IO.File.Delete(filePath1);
            }
            file1.CopyToAsync(new FileStream(filePath1, FileMode.Create));

            string newsXmlData = CustomNewsXml(news);
            bool result = _NewsEventService.AddUpdateNews(newsXmlData, news.Description);
            return Json(result, new JsonSerializerSettings());
        }

        
        //[ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        //[HttpPost]
        //public async Task<IActionResult> UpdateNewsPageByLanguageId(News News)
        //{
        //    try
        //    {
        //        _loggerManager.LogInfo("Method :UpdateHomePageByLanguageId Data: " + JsonConvert.SerializeObject(News));
        //        string path = Path.Combine(_hostingEnvironment.WebRootPath, "images/News/NewsImages/");
        //        string deletePath = Path.Combine(_hostingEnvironment.WebRootPath, "images/News/PreviewImages/");

                
        //        if (News.ImageNewsKasturiRangan != null)
        //        {
        //            FilePath(News.ImageNewsKasturiRangan, path + "ImageNewsKasturiRangan.png", deletePath + "ImageNewsKasturiRangan.png");
        //        }

        //        int resultCode = _masterService.UpdateHomePageByLanguageId(News.ModuleId, News.LanguageCode, News.Description, News.MetaDescription, News.MetaTitle, News.MetaUrl);

        //        return Json(resultCode, new JsonSerializerSettings());
        //    }
        //    catch (Exception ex)
        //    {
        //        _loggerManager.LogError("Method:  UpdateHomePageByLanguageId: " + ex.Message);

        //        throw ex;
        //    }
        //}

        [HttpGet]
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

        [HttpPost]
        public async Task<IActionResult> Preview(News News)
        {

            if (News.ImageNewsKasturiRangan != null)
            {
                News.Description = News.Description.Replace("/News/ImageNewsKasturiRangan.png", "/PreviewImages/ImageNewsKasturiRangan.png");
            }
           

            int resultCode = _masterService.UpdatePreviewPageByLanguageModuleId(News.ModuleId, News.LanguageCode, News.Description, News.MetaDescription, News.MetaTitle, News.MetaUrl);

            return Json(resultCode, new JsonSerializerSettings());
        }

    }
}
