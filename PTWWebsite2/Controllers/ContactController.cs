using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LoggerService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PTW.DataAccess.Models;
using PTW.DataAccess.Services;

namespace PTWWebsite2.Controllers
{
    public class ContactController : Controller
    {
        private readonly IMasterService _masterService;
        private readonly IUserService _userService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ILoggerManager _loggerManager;

        public ContactController(IMasterService masterService, IUserService userService, IHostingEnvironment hostingEnvironment, ILoggerManager loggerManager)
        {
            _masterService = masterService;
            _userService = userService;
            _hostingEnvironment = hostingEnvironment;
            _loggerManager = loggerManager;
        }

        [Route("contact")]
        [Route("{culture}/contact")]
        [AllowAnonymous]
        public IActionResult Contact(string culture)
        {
            MasterPage masterPage = new MasterPage();
            //DataTable dtContent = _masterService.GetModuleContent("Contact", (culture == null ? "en-US" : culture));
            //masterPage.HtmlContent = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Contact")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
            //ViewData["Header"] = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Header")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
            //ViewData["Footer"] = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Footer")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();

            DataTable dtContent = _masterService.GetModuleContentSectionwise(14, (culture == null ? "en-US" : culture == "undefined" ? "en-US" : culture));
            List<string> sectionlist = dtContent.Rows.Cast<DataRow>().Where(x => x["ModuleId"].Equals(14)).Select(y => Convert.ToString(y["Content"])).ToList();
            for (int i = 0; i < sectionlist.Count - 1; i++)
            {
                masterPage.HtmlContent += sectionlist[i];
            }



            //List<LocationDetails> list = _masterService.RetrieveLocations((culture == null ? "en-US" : culture));
            //string asiaContent = "";
            //string northAmerica = "";
            //string europeContent = "";
            //foreach (LocationDetails item in list)
            //{
            //    if (item.Region == "Asia" || item.Region == "Asie" || item.Region == "アジア" || item.Region == "아시아" || item.Region == "亚洲")
            //    {
            //        asiaContent = asiaContent + "<div class=\"col-xl-4 col-lg-4 col-md-6 ofc-details\"><p class=\"ofc-country\">" + item.Country + " </p>  <p class=\"ofc-city\">" + item.Location + "</p><address>" + item.Address + " </address><a class=\"btn btn-info btn-lg btn-map\" data-toggle=\"modal\" data-target=\"" + item.Target + "\" data-backdrop=\"static\" data-keyboard=\"false\"><p class=\"ofc-map\"><i class=\"fa fa-map-marker\" aria-hidden=\"true\">" + item.Title + "</i></p></a><div id =\"" + item.TargetLocation + "\" class=\"modal fade\" role=\"dialog\" tabindex=\"-1\"><div class=\"modal-dialog modal-lg\"><div class=\"modal-content\"><div class=\"modal-header\"><p>" + item.GoogleMapHeading + " </p><button type =\"button\" class=\"close\" data-dismiss=\"modal\">×</button></div> <div class=\"modal-body\"><div class=\"w100\"><iframe width =\"100%\" height=\"600\" src=\" " + item.GoogleMap + "\"></iframe></div><br></div></div></div></div></div>";
            //    }


            //    if (item.Region == "North America" || item.Region == "Amérique du Nord" || item.Region == "北米" || item.Region == "북미" || item.Region == "北美")
            //    {
            //        northAmerica = northAmerica + "<div class=\"col-xl-4 col-lg-4 col-md-6 ofc-details\"><p class=\"ofc-country\">" + item.Country + " </p>  <p class=\"ofc-city\">" + item.Location + "</p><address>" + item.Address + " </address><a class=\"btn btn-info btn-lg btn-map\" data-toggle=\"modal\" data-target=\"" + item.Target + "\" data-backdrop=\"static\" data-keyboard=\"false\"><p class=\"ofc-map\"><i class=\"fa fa-map-marker\" aria-hidden=\"true\">" + item.Title + "</i></p></a><div id =\"" + item.TargetLocation + "\" class=\"modal fade\" role=\"dialog\" tabindex=\"-1\"><div class=\"modal-dialog modal-lg\"><div class=\"modal-content\"><div class=\"modal-header\"><p>" + item.GoogleMapHeading + " </p><button type =\"button\" class=\"close\" data-dismiss=\"modal\">×</button></div> <div class=\"modal-body\"><div class=\"w100\"><iframe width =\"100%\" height=\"600\" src=\" " + item.GoogleMap + "\"></iframe></div><br></div></div></div></div></div>";
            //    }

            //    if (item.Region == "Europe" || item.Region == "Europe" || item.Region == "欧州" || item.Region == "유럽" || item.Region == "欧洲")
            //    {
            //        europeContent = europeContent + "<div class=\"col-xl-4 col-lg-4 col-md-6 ofc-details\"><p class=\"ofc-country\">" + item.Country + " </p>  <p class=\"ofc-city\">" + item.Location + "</p><address>" + item.Address + " </address><a class=\"btn btn-info btn-lg btn-map\" data-toggle=\"modal\" data-target=\"" + item.Target + "\" data-backdrop=\"static\" data-keyboard=\"false\"><p class=\"ofc-map\"><i class=\"fa fa-map-marker\" aria-hidden=\"true\">" + item.Title + "</i></p></a><div id =\"" + item.TargetLocation + "\" class=\"modal fade\" role=\"dialog\" tabindex=\"-1\"><div class=\"modal-dialog modal-lg\"><div class=\"modal-content\"><div class=\"modal-header\"><p>" + item.GoogleMapHeading + " </p><button type =\"button\" class=\"close\" data-dismiss=\"modal\">×</button></div> <div class=\"modal-body\"><div class=\"w100\"><iframe width =\"100%\" height=\"600\" src=\" " + item.GoogleMap + "\"></iframe></div><br></div></div></div></div></div>";
            //    }

            //}
            //masterPage.HtmlContent = masterPage.HtmlContent.Replace("<div> Data</div>", asiaContent).Replace("<div> Data1</div>", northAmerica).Replace("<div> Data2</div>", europeContent);

            return View(masterPage);
        }

        [Route("location")]
        [Route("{culture}/Location")]
        [AllowAnonymous]
        public IActionResult Location(string culture)
        {

            List<LocationDetails> list = _masterService.RetrieveLocations((culture == null ? "en-US" : culture));

            List<LocationDetails> asia = new List<LocationDetails>();
            List<LocationDetails> northAmerica = new List<LocationDetails>();
            List<LocationDetails> europe = new List<LocationDetails>();


            foreach (LocationDetails item in list)
            {
                if (item.Region == "Asia" || item.Region == "Asie" || item.Region == "アジア" || item.Region == "아시아" || item.Region == "亚洲")
                {
                    asia.Add(item);
                    //asiaContent = asiaContent + "<div class=\"col-xl-4 col-lg-4 col-md-6 ofc-details\"><p class=\"ofc-country\">" + item.Country + " </p>  <p class=\"ofc-city\">" + item.Location + "</p><address>" + item.Address + " </address><a class=\"btn btn-info btn-lg btn-map\" data-toggle=\"modal\" data-target=\"" + item.Target + "\" data-backdrop=\"static\" data-keyboard=\"false\"><p class=\"ofc-map\"><i class=\"fa fa-map-marker\" aria-hidden=\"true\">" + item.Title + "</i></p></a><div id =\"" + item.TargetLocation + "\" class=\"modal fade\" role=\"dialog\" tabindex=\"-1\"><div class=\"modal-dialog modal-lg\"><div class=\"modal-content\"><div class=\"modal-header\"><p>" + item.GoogleMapHeading + " </p><button type =\"button\" class=\"close\" data-dismiss=\"modal\">×</button></div> <div class=\"modal-body\"><div class=\"w100\"><iframe width =\"100%\" height=\"600\" src=\" " + item.GoogleMap + "\"></iframe></div><br></div></div></div></div></div>";
                }


                if (item.Region == "North America" || item.Region == "Amérique du Nord" || item.Region == "北米" || item.Region == "북미" || item.Region == "北美")
                {
                    northAmerica.Add(item);
                    //northAmerica = northAmerica + "<div class=\"col-xl-4 col-lg-4 col-md-6 ofc-details\"><p class=\"ofc-country\">" + item.Country + " </p>  <p class=\"ofc-city\">" + item.Location + "</p><address>" + item.Address + " </address><a class=\"btn btn-info btn-lg btn-map\" data-toggle=\"modal\" data-target=\"" + item.Target + "\" data-backdrop=\"static\" data-keyboard=\"false\"><p class=\"ofc-map\"><i class=\"fa fa-map-marker\" aria-hidden=\"true\">" + item.Title + "</i></p></a><div id =\"" + item.TargetLocation + "\" class=\"modal fade\" role=\"dialog\" tabindex=\"-1\"><div class=\"modal-dialog modal-lg\"><div class=\"modal-content\"><div class=\"modal-header\"><p>" + item.GoogleMapHeading + " </p><button type =\"button\" class=\"close\" data-dismiss=\"modal\">×</button></div> <div class=\"modal-body\"><div class=\"w100\"><iframe width =\"100%\" height=\"600\" src=\" " + item.GoogleMap + "\"></iframe></div><br></div></div></div></div></div>";
                }

                if (item.Region == "Europe" || item.Region == "Europe" || item.Region == "欧州" || item.Region == "유럽" || item.Region == "欧洲")
                {
                    europe.Add(item);
                    //europeContent = europeContent + "<div class=\"col-xl-4 col-lg-4 col-md-6 ofc-details\"><p class=\"ofc-country\">" + item.Country + " </p>  <p class=\"ofc-city\">" + item.Location + "</p><address>" + item.Address + " </address><a class=\"btn btn-info btn-lg btn-map\" data-toggle=\"modal\" data-target=\"" + item.Target + "\" data-backdrop=\"static\" data-keyboard=\"false\"><p class=\"ofc-map\"><i class=\"fa fa-map-marker\" aria-hidden=\"true\">" + item.Title + "</i></p></a><div id =\"" + item.TargetLocation + "\" class=\"modal fade\" role=\"dialog\" tabindex=\"-1\"><div class=\"modal-dialog modal-lg\"><div class=\"modal-content\"><div class=\"modal-header\"><p>" + item.GoogleMapHeading + " </p><button type =\"button\" class=\"close\" data-dismiss=\"modal\">×</button></div> <div class=\"modal-body\"><div class=\"w100\"><iframe width =\"100%\" height=\"600\" src=\" " + item.GoogleMap + "\"></iframe></div><br></div></div></div></div></div>";
                }

            }
            return Json(new { asiaContent = asia, northAmericaContent = northAmerica, europeContent = europe }, new JsonSerializerSettings());
        }


        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        [Route("contactedit")]
        [HttpGet]
        public IActionResult ContactEdit()
        {
            MasterPage masterPage = _masterService.GetLanguageandModules();

            //masterPage.Languages = masterPage.LanguageList;
            return View(masterPage);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateContactPageByLanguageId(Contact Contact)
        {
            try
            {
                string path = Path.Combine(_hostingEnvironment.WebRootPath, "images/Contact/ContactImages/");
                string deletePath = Path.Combine(_hostingEnvironment.WebRootPath, "images/Contact/PreviewImages/");

                if (Contact.ContactHeader != null)
                {
                    FilePath(Contact.ContactHeader, path + "ContactHeader.png", deletePath + "ContactHeader.png");
                }

                if (Contact.LocationMap != null)
                {
                    FilePath(Contact.LocationMap, path + "LocationMap.svg", deletePath + "LocationMap.svg");
                }

                if (Contact.Frame != null)
                {
                    FilePath(Contact.Frame, path + "Frame.svg", deletePath + "Frame.svg");
                }

                //int resultCode = _masterService.UpdateHomePageByLanguageId(Contact.ModuleId, Contact.LanguageCode, Contact.Description, Contact.MetaDescription, Contact.MetaTitle, Contact.MetaUrl);
                int resultCode = _masterService.UpdateSectionContent(Contact.SectionId, Contact.ModuleId, Contact.LanguageCode, Contact.Description, Contact.MetaDescription, Contact.MetaTitle, Contact.MetaUrl);

                return Json(resultCode, new JsonSerializerSettings());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateContactImagesToPreview(Contact Contact)
        {
            string path = Path.Combine(_hostingEnvironment.WebRootPath, "images/Contact/PreviewImages/");

            if (Contact.ContactHeader != null && Contact.Filename == "ContactHeader")
            {
                FilePath(Contact.ContactHeader, path + "ContactHeader.png", "");
            }

            if (Contact.LocationMap != null && Contact.Filename == "LocationMap")
            {
                FilePath(Contact.LocationMap, path + "LocationMap.svg", "");
            }

            if (Contact.Frame != null && Contact.Filename == "Frame")
            {
                FilePath(Contact.Frame, path + "Frame.svg", "");
            }
            //int resultCode = _masterService.UpdatePreviewPageByLanguageModuleId(Contact.ModuleId, Contact.LanguageCode, Contact.Description, Contact.MetaDescription, Contact.MetaTitle, Contact.MetaUrl);
            int resultCode = _masterService.UpdatePreviewContentByLanguageModuleId(Contact.SectionId, Contact.ModuleId, Contact.LanguageCode, Contact.Description, Contact.MetaDescription, Contact.MetaTitle, Contact.MetaUrl);

            return Json(resultCode, new JsonSerializerSettings());
        }


        [HttpPost]
        public async Task<IActionResult> PreviewContact(Contact Contact)
        {
            string savedPath = Path.Combine(_hostingEnvironment.WebRootPath, "images/Contact/ContactImages/");
            string previewPath = Path.Combine(_hostingEnvironment.WebRootPath, "images/Contact/PreviewImages/");


            if (Contact.ContactHeader != null)
            {
                Contact.Description = Contact.Description.Replace(savedPath + "ContactHeader.png", previewPath + "ContactHeader.png");
            }

            if (Contact.LocationMap != null)
            {
                Contact.Description = Contact.Description.Replace(savedPath + "LocationMap.svg", previewPath + "LocationMap.svg");
            }

            if (Contact.Frame != null)
            {
                Contact.Description = Contact.Description.Replace(savedPath + "Frame.svg", previewPath + "Frame.svg");
            }

            //int resultCode = _masterService.UpdatePreviewPageByLanguageModuleId(Contact.ModuleId, Contact.LanguageCode, Contact.Description, Contact.MetaDescription, Contact.MetaTitle, Contact.MetaUrl);
            int resultCode = _masterService.UpdatePreviewContentByLanguageModuleId(Contact.SectionId, Contact.ModuleId, Contact.LanguageCode, Contact.Description, Contact.MetaDescription, Contact.MetaTitle, Contact.MetaUrl);

            return Json(resultCode, new JsonSerializerSettings());
        }

        [Route("addlocations")]
        public IActionResult AddLocations()
        {
            Main obj = new Main();
            List<Region> regionlists = _userService.RetrieveRegionData();
            obj.regions = regionlists;
            return View(obj);
        }

        [HttpPost]
        public IActionResult AddLocationsPost([FromBody] LocationDetails location)
        {
            try
            {
                int result = _masterService.AddLocation(location);
                if (result > 0)
                {
                    return Json(new { Message = result }, new JsonSerializerSettings());
                }
                else
                {
                    return Json(new { Message = result }, new JsonSerializerSettings());
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public JsonResult Country([FromBody] Region region)
        {
            List<Country> countries = _userService.RetrieveCountryData(region.RegionCode);
            return Json(new { result = countries }, new JsonSerializerSettings());
        }

        [HttpPost]
        public JsonResult City([FromBody] Country country)
        {
            List<Citys> cities = _userService.RetrieveCityData(country.CountryCode);
            return Json(new { result = cities }, new JsonSerializerSettings());
        }


        [Route("alllocations")]

        public IActionResult GetLocations()
        {
            return View();
        }
        public IActionResult LIstLocations()
        {
            List<LocationDetails> listLocations = _userService.RetrieveAllLocations();
            return Json(new { Locations = listLocations }, new JsonSerializerSettings());
        }

        [Route("{LocationId}-location")]
        public IActionResult EditLocationById(int LocationId)
        {
            LocationDetails location = _userService.GetLocationById(LocationId);
            return View(location);
        }
        [HttpPost]
        public IActionResult UpdateLocation([FromBody] LocationDetails loc)
        {
            int resultcode = _userService.UpdateLocation(loc);
            return Json(new { result = resultcode }, new JsonSerializerSettings());
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