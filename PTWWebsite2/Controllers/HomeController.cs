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

namespace PTWWebsite2.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMasterService _masterService;
        public HomeController(IMasterService masterService)
        {
            _masterService = masterService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("")]
        [Route("Home")]
        [Route("{culture}/Home")]
        [Route("{culture}/")]
        public IActionResult Home(string culture)
        {
            MasterPage masterPage = new MasterPage();
              DataTable  dtContent = _masterService.GetModuleContent("Home", (culture==null?"en-US": culture));
                masterPage.HtmlContent = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Home")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
            ViewData["Header"] = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Header")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
            ViewData["Footer"] = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Footer")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
            return View(masterPage);
        }

        [Route("Editor")]
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

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

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

    }
}
