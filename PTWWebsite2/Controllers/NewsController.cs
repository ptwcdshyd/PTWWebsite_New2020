using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PTW.DataAccess.Models;
using PTW.DataAccess.Services;
using PTW.DBAccess;

namespace PTWWebsite2.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsEventService _NewsEventService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IMasterService _masterService;

        public NewsController(INewsEventService newsEventService,IHostingEnvironment hostingEnvironment, IMasterService masterService)
        {
            _NewsEventService = newsEventService;
            _hostingEnvironment = hostingEnvironment;
            _masterService = masterService;
        }



        [Route("News")]
        [Route("{culture}/News")]
        public IActionResult News(string culture)
        {
            NewsEvents newsEvents = new NewsEvents();
            newsEvents.News = _NewsEventService.GetAllNewsDetails();
            newsEvents.Events = _NewsEventService.GetAllEventDetails();
            return View(newsEvents);
        }

        [Route("News/{NewsTitleUrl}")]
        [Route("{cultures}/News/{NewsTitleUrl}")]
        public IActionResult NewsArticles(string cultures, string NewsTitleUrl)
        {
            if (!string.IsNullOrEmpty(NewsTitleUrl))
            {
                NewsTitleUrl = NewsTitleUrl.Replace("-", " ");
            }
            NewsDetails newsDetails = _NewsEventService.GetNewsDetailsByTitle(NewsTitleUrl, cultures == "" ? "en-US" : cultures);
           
            return View(newsDetails);
        }


        

        [Route("AddNews")]
        public IActionResult AddNewsEvents()
        {
            News news = new News();

            //news = _NewsEventService.GetAllNewsAndEventDetailsForUpdate();

            return View(news);
        }

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
        [HttpPost]
        public IActionResult AddUpdateNews(News news)
        {
            //News newsList = _NewsEventService.GetAllNewsAndEventDetailsForUpdate();
            if (ModelState.IsValid)
            {

                string uniqueFileName = string.Empty;
                string headerImageFolder = string.Empty;
                string LorgeImageFolder = string.Empty;
                string smallImageFolder = string.Empty;

                if (news.HeaderImage != null)
                {
                    headerImageFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images/News/2020/Header");
                    LorgeImageFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images/News/2020/Large");
                    smallImageFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images/News/2020/Small");
                    
                    news.HeaderImageUrl = "/Images/News/2020/Header/";
                    news.HeaderImageName = news.HeaderImage.FileName;
                    string filePath = Path.Combine(headerImageFolder, news.HeaderImage.FileName);
                    news.HeaderImage.CopyTo(new FileStream(filePath, FileMode.Create));

                    news.LongImageUrl = "/Images/News/2020/Large/";
                    news.LongImageName = news.LongerImage.FileName;
                    string filePath2 = Path.Combine(LorgeImageFolder, news.LongerImage.FileName);
                    news.HeaderImage.CopyTo(new FileStream(filePath2, FileMode.Create));

                    news.ShortImageUrl = "/Images/News/2020/Small/";
                    news.ShortImageName = news.ShorterImage.FileName;
                    string filePath3 = Path.Combine(smallImageFolder, news.ShorterImage.FileName);
                    news.HeaderImage.CopyTo(new FileStream(filePath3, FileMode.Create));

                }

                string newsXmlData = CustomNewsXml(news);
                bool result = _NewsEventService.AddUpdateNews(newsXmlData, news.Description);
                ViewBag.IsAddedSuccessfully = result;
                ModelState.Clear();
            }

            return View("AddNewsEvents");
        }


        [Route("EditNewss/GetNews")]
        [HttpPost]
        public IActionResult GetNews(News news1)
        {
            MasterPage masterPage1 = _masterService.GetLanguageandModules();
            
            //News news = new News();
            ModelState.Clear();
            News ddlNewsTitles = _NewsEventService.GetAllNewsAndEventDetailsForUpdate();
            News news = _NewsEventService.GetNewsAndEventDetailsByNewsId(news1.NewsId, news1.LanguageCode=="Please Select"?"en-US": news1.LanguageCode);
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
            MasterPage masterPage1 = _masterService.GetLanguageandModules();
            News newsList = _NewsEventService.GetAllNewsAndEventDetailsForUpdate();
            //ModelState.ClearValidationState(news.HeaderImage);
            ModelState.Remove("HeaderImage");
            ModelState.Remove("LongerImage");
            ModelState.Remove("ShorterImage");
            if (ModelState.IsValid)
            {

                string uniqueFileName = string.Empty;
                string headerImageFolder = string.Empty;
                string LorgeImageFolder = string.Empty;
                string smallImageFolder = string.Empty;

                if (news.HeaderImage != null)
                {
                    headerImageFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images/News/2020/Header");

                    news.HeaderImageUrl = "/Images/News/2020/Header/";
                    news.HeaderImageName = news.HeaderImage.FileName;
                    string filePath = Path.Combine(headerImageFolder, news.HeaderImage.FileName);
                    news.HeaderImage.CopyTo(new FileStream(filePath, FileMode.Create));

                }
                if (news.LongerImage != null)
                {
                    LorgeImageFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images/News/2020/Large");
                   
                    news.LongImageUrl = "/Images/News/2020/Large/";
                    news.LongImageName = news.LongerImage.FileName;
                    string filePath2 = Path.Combine(LorgeImageFolder, news.LongerImage.FileName);
                    news.HeaderImage.CopyTo(new FileStream(filePath2, FileMode.Create));

                }
                if (news.ShorterImage != null)
                {
                    smallImageFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images/News/2020/Small");

                    news.ShortImageUrl = "/Images/News/2020/Small/";
                    news.ShortImageName = news.ShorterImage.FileName;
                    string filePath3 = Path.Combine(smallImageFolder, news.ShorterImage.FileName);
                    news.HeaderImage.CopyTo(new FileStream(filePath3, FileMode.Create));
                }

                string newsXmlData = CustomNewsXml(news);
                bool result = _NewsEventService.UpdateNews(news.EditNewsId, newsXmlData, news.Description, news.LanguageCode == null ? "en-US" : news.LanguageCode);
                //ViewBag.IsAddedSuccessfully = result;
                newsList.Message = "<label id=\"lblMessage2\" class=\"text-danger\">Updated sucessfully</label>";
                ModelState.Clear();
            }
            newsList.Languages = masterPage1.LanguageList;
            newsList.NewsId = news.NewsId;//ddl selected value
            newsList.LanguageCode = news.LanguageCode;
            return View("EditNewsEvents", newsList);
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

    }
}
