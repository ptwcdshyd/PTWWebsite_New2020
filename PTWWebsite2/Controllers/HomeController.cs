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
using System.IO;
using System.Data;
using Microsoft.AspNetCore.Authorization;

namespace PTWWebsite2.Controllers
{
    [Authorize]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public class HomeController : Controller
    {
        private readonly IMasterService _masterService;
        private readonly IUserService _userService;
        public HomeController(IMasterService masterService, IUserService userService)
        {
            _masterService = masterService;
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("")]
        [Route("Home")]
        [Route("{culture}/Home")]
        [Route("{culture}/")]
        [AllowAnonymous]
        public IActionResult Home(string culture)
        {
            MasterPage masterPage = new MasterPage();
            DataTable dtContent = _masterService.GetModuleContent("Home", (culture == null ? "en-US" : culture));
            masterPage.HtmlContent = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Home")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
            ViewData["Header"] = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Header")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
            ViewData["Footer"] = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Footer")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
            return View(masterPage);
        }

        [Route("Editor")]
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

        [Route("Contact")]
        [Route("{culture}/Contact")]
        [AllowAnonymous]
        public IActionResult Contact(string culture)
        {
            MasterPage masterPage = new MasterPage();
            DataTable dtContent = _masterService.GetModuleContent("Contact", (culture == null ? "en-US" : culture));
            masterPage.HtmlContent = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Contact")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
            ViewData["Header"] = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Header")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
            ViewData["Footer"] = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Footer")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();

            List<LocationDetails> list = _masterService.RetrieveLocations((culture == null ? "en-US" : culture));
            string asiaContent = "";
            string northAmerica = "";
            string europeContent = "";
            foreach (LocationDetails item in list)
            {
                if (item.Region == "Asia" || item.Region == "Asie" || item.Region == "アジア" || item.Region == "아시아" || item.Region == "亚洲")
                {
                    asiaContent = asiaContent + "<div class=\"col-xl-4 col-lg-4 col-md-6 ofc-details\"><p class=\"ofc-country\">" + item.Country + " </p>  <p class=\"ofc-city\">" + item.Location + "</p><address>" + item.Address + " </address><a class=\"btn btn-info btn-lg btn-map\" data-toggle=\"modal\" data-target=\"" + item.Target + "\" data-backdrop=\"static\" data-keyboard=\"false\"><p class=\"ofc-map\"><i class=\"fa fa-map-marker\" aria-hidden=\"true\">" + item.Title + "</i></p></a><div id =\"" + item.TargetLocation + "\" class=\"modal fade\" role=\"dialog\" tabindex=\"-1\"><div class=\"modal-dialog modal-lg\"><div class=\"modal-content\"><div class=\"modal-header\"><p>" + item.GoogleMapHeading + " </p><button type =\"button\" class=\"close\" data-dismiss=\"modal\">×</button></div> <div class=\"modal-body\"><div class=\"w100\"><iframe width =\"100%\" height=\"600\" src=\" " + item.GoogleMap + "\"></iframe></div><br></div></div></div></div></div>";
                }


                if (item.Region == "North America" || item.Region == "Amérique du Nord" || item.Region == "北米" || item.Region == "북미" || item.Region == "北美")
                {
                    northAmerica = northAmerica + "<div class=\"col-xl-4 col-lg-4 col-md-6 ofc-details\"><p class=\"ofc-country\">" + item.Country + " </p>  <p class=\"ofc-city\">" + item.Location + "</p><address>" + item.Address + " </address><a class=\"btn btn-info btn-lg btn-map\" data-toggle=\"modal\" data-target=\"" + item.Target + "\" data-backdrop=\"static\" data-keyboard=\"false\"><p class=\"ofc-map\"><i class=\"fa fa-map-marker\" aria-hidden=\"true\">" + item.Title + "</i></p></a><div id =\"" + item.TargetLocation + "\" class=\"modal fade\" role=\"dialog\" tabindex=\"-1\"><div class=\"modal-dialog modal-lg\"><div class=\"modal-content\"><div class=\"modal-header\"><p>" + item.GoogleMapHeading + " </p><button type =\"button\" class=\"close\" data-dismiss=\"modal\">×</button></div> <div class=\"modal-body\"><div class=\"w100\"><iframe width =\"100%\" height=\"600\" src=\" " + item.GoogleMap + "\"></iframe></div><br></div></div></div></div></div>";
                }

                if (item.Region == "Europe" || item.Region == "Europe" || item.Region == "欧州" || item.Region == "유럽" || item.Region == "欧洲")
                {
                    europeContent = europeContent + "<div class=\"col-xl-4 col-lg-4 col-md-6 ofc-details\"><p class=\"ofc-country\">" + item.Country + " </p>  <p class=\"ofc-city\">" + item.Location + "</p><address>" + item.Address + " </address><a class=\"btn btn-info btn-lg btn-map\" data-toggle=\"modal\" data-target=\"" + item.Target + "\" data-backdrop=\"static\" data-keyboard=\"false\"><p class=\"ofc-map\"><i class=\"fa fa-map-marker\" aria-hidden=\"true\">" + item.Title + "</i></p></a><div id =\"" + item.TargetLocation + "\" class=\"modal fade\" role=\"dialog\" tabindex=\"-1\"><div class=\"modal-dialog modal-lg\"><div class=\"modal-content\"><div class=\"modal-header\"><p>" + item.GoogleMapHeading + " </p><button type =\"button\" class=\"close\" data-dismiss=\"modal\">×</button></div> <div class=\"modal-body\"><div class=\"w100\"><iframe width =\"100%\" height=\"600\" src=\" " + item.GoogleMap + "\"></iframe></div><br></div></div></div></div></div>";
                }




            }
            masterPage.HtmlContent = masterPage.HtmlContent.Replace("<div> Data</div>", asiaContent).Replace("<div> Data1</div>", northAmerica).Replace("<div> Data2</div>", europeContent);

            return View(masterPage);
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
                                FilePath(file, path);

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
                                FilePath(file, path);

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
                                FilePath(file, path);


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
                                FilePath(file, path);


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
                                FilePath(file, path);

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
        [Route("ImagesUpload")]
        public IActionResult ImagesUpload()
        {
            return View();
        }
        [HttpPost]
        [Route("Image/ImagesUpload")]
        [ActionName("ImagesUpload")]
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

        [Route("AddLocations")]
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


        [Route("AllLocations")]

        public IActionResult GetLocations()
        {
            return View();
        }
        public IActionResult LIstLocations()
        {
            List<LocationDetails> listLocations = _userService.RetrieveAllLocations();
            return Json(new { Locations = listLocations }, new JsonSerializerSettings());
        }

        [Route("{LocationId}-Location")]
        public IActionResult EditLocationById(int LocationId)
        {
           LocationDetails location= _userService.GetLocationById(LocationId);
            return View(location);
        }
        [HttpPost]
        public IActionResult UpdateLocation([FromBody] LocationDetails loc)
        {
            int resultcode= _userService.UpdateLocation(loc);
            return Json(new { result= resultcode},new JsonSerializerSettings());
        }
    }
}
