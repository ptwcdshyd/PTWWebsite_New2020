using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Design.Internal;
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


        [Route("LAB/{LabShortDescription}")]
        [HttpPost]
        public IActionResult AddUpdateLabs(Labs labs)
        {

            //if (ModelState.IsValid)
            //{

                string uniqueFileName = string.Empty;
                string MobileImage = string.Empty;
                string DesktopImage = string.Empty;
                string TabImageHorizental = string.Empty;
                string TabImageVertical = string.Empty;
                string Readmore = string.Empty;
                string Labtypeimg = string.Empty;
                string Mobileurl = string.Empty;
                if (labs.DesktopImage != null)
                {
                    if (labs.LabTypeId == 1)
                    {
                        Labtypeimg = "Articles";

                    }
                    else
                    {
                        Labtypeimg = "CaseStudies";

                    }

                    MobileImage = Path.Combine(_hostingEnvironment.WebRootPath, "images/Lab/" + Labtypeimg + "/376");
                    DesktopImage = Path.Combine(_hostingEnvironment.WebRootPath, "images/Lab/" + Labtypeimg + "/1920");
                    TabImageHorizental = Path.Combine(_hostingEnvironment.WebRootPath, "images/Lab/" + Labtypeimg + "/1112");
                    TabImageVertical = Path.Combine(_hostingEnvironment.WebRootPath, "images/Lab/" + Labtypeimg + "/834");
                    Readmore = Path.Combine(_hostingEnvironment.WebRootPath, "images/Lab/" + Labtypeimg + "/320");
                
                    labs.ImageUrl = "/Images/Lab/" + Labtypeimg + "/376/";
                    labs.ImageName = labs.MobileImage.FileName;

                    labs.MobileImageNameUrl = "/Images/Lab/" + Labtypeimg + "/376/"+ labs.MobileImage.FileName + "";
                    labs.MobileName = labs.MobileImage.FileName;                 

                    string filePath = Path.Combine(MobileImage, labs.MobileImage.FileName);
                    labs.MobileImage.CopyTo(new FileStream(filePath, FileMode.Create));


                    labs.DesktopImageUrl = "/Images/Lab/" + Labtypeimg + "/1920/" + labs.DesktopImage.FileName + "";
                    labs.DesktopName = labs.DesktopImage.FileName;
                    string filePath2 = Path.Combine(DesktopImage, labs.DesktopImage.FileName);
                    labs.DesktopImage.CopyTo(new FileStream(filePath2, FileMode.Create));

                    labs.TabImageNameHorizondaUrl = "/Images/Lab/" + Labtypeimg + "/1112/" + labs.TabImageHorizonal.FileName + "";
                    labs.TabImgHorizonalname = labs.TabImageHorizonal.FileName;
                    string filePath3 = Path.Combine(TabImageHorizental, labs.TabImageHorizonal.FileName);
                    labs.TabImageHorizonal.CopyTo(new FileStream(filePath3, FileMode.Create));

                    labs.TabImageNamVerticalUrl = "/Images/Lab/" + Labtypeimg + "/834/" + labs.TabImageNamVertical.FileName + "";
                    labs.TabImgVerticalname = labs.TabImageNamVertical.FileName;
                    string filePath4 = Path.Combine(TabImageVertical, labs.TabImageNamVertical.FileName);
                    labs.TabImageNamVertical.CopyTo(new FileStream(filePath4, FileMode.Create));

                    labs.ReadMoreUrl = "/Images/Lab/" + Labtypeimg + "/320/" + labs.ReadMore.FileName + "";
                    labs.ReadMorename = labs.ReadMore.FileName;
                    string filePath5 = Path.Combine(Readmore, labs.ReadMore.FileName);
                    labs.ReadMore.CopyTo(new FileStream(filePath5, FileMode.Create));
                }

                string labsXmlData = CustomNewsXml(labs);
                bool result = _LabEventService.AddUpdateLabs(labsXmlData, labs.Description);
                ViewBag.IsAddedSuccessfully = result;
                ModelState.Clear();
            //}

            return View("AddLabArticles",labs);
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
                xml.Append(string.Format("<Description>{0}</Description>", labs.Description));
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

                xml.Append(string.Format("<DesktopImageUrl>{0}</DesktopImageUrl>", labs.DesktopImageUrl));
                xml.Append(string.Format("<TabImageNameHorizondaUrl>{0}</TabImageNameHorizondaUrl>", labs.TabImageNameHorizondaUrl));
                xml.Append(string.Format("<TabImageNamVerticalUrl>{0}</TabImageNamVerticalUrl>", labs.TabImageNamVerticalUrl));
                xml.Append(string.Format("<MobileImageNameUrl>{0}</MobileImageNameUrl>", labs.MobileImageNameUrl));
                xml.Append(string.Format("<ReadMoreUrl>{0}</ReadMoreUrl>", labs.ReadMoreUrl));

                xml.Append(string.Format("<ShortOrder>{0}</ShortOrder>", labs.ShortOrder));
                xml.Append(string.Format("<LabType>{0}</LabType>", labs.LabType));
                xml.Append(string.Format("<StartDate>{0}</StartDate>", labs.StartDate != "" ? Convert.ToDateTime(labs.StartDate) : DateTime.Now));
                xml.Append(string.Format("<EndDate>{0}</EndDate>", labs.EndDate != "" ? Convert.ToDateTime(labs.StartDate) : DateTime.Now));
               
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
    }
}