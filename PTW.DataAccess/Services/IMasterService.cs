using Microsoft.AspNetCore.Http;
using PTW.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PTW.DataAccess.Services
{
    public interface IMasterService
    {
        MasterPage GetDashboardDetails(int LoginUserId, int LanguageID, int ModuleId);
        string UpdateContentByModelIdAndLanguageId(int moduleId, int languageId, string contentText);

        MasterPage GetLanguageandModules();
        string SaveImages(string imagePath, int moduleId, List<IFormFile> files);
        List<Images> GetImageDetails(int moduleId);
    }
}
