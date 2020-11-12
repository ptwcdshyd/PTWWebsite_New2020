using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using PTW.DataAccess.Models;
using PTW.DataAccess.Services;
using System.Security.Claims;
using LoggerService;

namespace PTWWebsite2.Controllers
{
    public class PreviewController : Controller
    {
        private readonly IMasterService _masterService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ILoggerManager _loggerManager;

        public PreviewController(IMasterService masterService, IHostingEnvironment hostingEnvironment, ILoggerManager loggerManager)
        {
            _masterService = masterService;
            _hostingEnvironment = hostingEnvironment;
            _loggerManager = loggerManager;
        }

        [HttpGet]
        public IActionResult PagePreview(int ModuleId, string LanguageCode)
        {

            Preview preview = _masterService.ShowPreivew(ModuleId, LanguageCode);
            DataTable dtContent = _masterService.GetModuleContent("Home", LanguageCode);
            ViewData["Header"] = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Header")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();

            return View("PreviewHome", preview);
        }

        [HttpGet]
        public IActionResult PreviewPage(int ModuleId, string LanguageCode, int SectionId)
        {
            Preview preview = new Preview();
            DataTable dtContent = _masterService.GetModuleContentSectionwise(ModuleId, LanguageCode);
            string previewSectionHtml = _masterService.GetPreviewSubContent(ModuleId, LanguageCode, SectionId);

            for (int i = 0; i < dtContent.Rows.Count; i++)
            {
                if (Convert.ToInt32(dtContent.Rows[i]["ModuleId"]) == ModuleId && Convert.ToInt32(dtContent.Rows[i]["SectionId"]) == SectionId)
                {
                    preview.HtmlContent += previewSectionHtml;
                }
                else if (Convert.ToInt32(dtContent.Rows[i]["ModuleId"]) == ModuleId)
                {
                    preview.HtmlContent += Convert.ToString(dtContent.Rows[i]["Content"]);
                }
                
            }
            ViewData["Header"] = dtContent.Rows.Cast<DataRow>().Where(x => x["ModuleId"].Equals(1)).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
            ViewData["Footer"] = dtContent.Rows.Cast<DataRow>().Where(x => x["ModuleId"].Equals(2)).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();

            return View("PreviewHome", preview);
        }

    }
}
