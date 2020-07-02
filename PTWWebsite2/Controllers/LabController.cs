using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using PTW.DataAccess.Models;
using PTW.DataAccess.Services;

namespace PTWWebsite2.Controllers
{
    
    public class LabController : Controller
    {
        private readonly ILabService _LabEventService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IMasterService _masterService;

        public LabController(ILabService LabEventService, IHostingEnvironment hostingEnvironment, IMasterService masterService)
        {
            _LabEventService =LabEventService;
            _hostingEnvironment = hostingEnvironment;
            _masterService = masterService;
        }

        [Route("LAB")]
        [Route("{culture}/LAB")]
        public IActionResult Index(string culture = "en-US")
        {
            LabsEvents LabsEvents = new LabsEvents();
            LabsEvents.allLabs = _LabEventService.GetAllLabsDetails(culture);
            
            LabsEvents.labInsights = _LabEventService.GetAllLatestInsights(culture);
            LabsEvents.Labs = _LabEventService.GetSlider();
            GetServicecontent(culture);
            return View(LabsEvents);


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
        [Route("LAB/{LabShortDescription}")]
        [Route("{culture}/LAB/{LabShortDescription}")]
        public IActionResult LabArticles(string LabShortDescription,string culture = "en-US")        
        {
            List<string> ShortDescription = LabShortDescription.Split('-').ToList();
            ShortDescription.RemoveAt(0);
            string strShortDescription = string.Join(" ", ShortDescription);
            LabsEvents LabsEvents = new LabsEvents();
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
            
            GetServicecontent(culture);
            return View(LabsEvents);
        }


        [Route("AddLabs")]
        public IActionResult AddLabArticles()
        {
            Labs labs = new Labs();

            //labs = _LabEventService.AddUpdatLabs(Labs labs);

            return View(labs);
        }

        [Route("Labs/{LabTitleUrl}")]
        [HttpPost]
        public IActionResult AddUpdateNews(News news)
        {
           
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
                bool result = _LabEventService.AddUpdateLabs(newsXmlData, news.Description);
                ViewBag.IsAddedSuccessfully = result;
                ModelState.Clear();
            }

            return View("AddNewsEvents");
        }

        [NonAction]
        public string CustomNewsXml(News news)
        {
            StringBuilder xml = new StringBuilder();
            xml.Append("<Labs>");
            if (news != null)
            {
               
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
    }
}