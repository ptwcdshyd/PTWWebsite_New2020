using Microsoft.AspNetCore.Http;
using PTW.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace PTW.DataAccess.Services
{
    public interface IMasterService
    {
        //MasterPage GetDashboardDetails(int LoginUserId, int LanguageID, int ModuleId,string Languagecode);
        int UpdateContentByModelIdAndLanguageId(int moduleId, string languageCode, string contentText, string Metatage, string Title);

        MasterPage GetLanguageandModules();
        string SaveImages(string imagePath, int moduleId, List<IFormFile> files);
        List<Images> GetImageDetails(int moduleId);

        DataTable GetModuleContent(string ModuleName, string Languagecode);
        MasterPage GetModuleContentById(int ModuleId, string Languagecode);
        MasterPage GetNewsAndLabDetails(string serviceType,int languageId);
        DataTable GetAboutProfile(string Languagecode);

        DataTable GetLanguages();

    }
}
