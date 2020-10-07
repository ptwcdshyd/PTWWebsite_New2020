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
        int UpdateContentByModelIdAndLanguageId(int moduleId, string languageCode, string contentText, string MetaDescription, string Title);

        MasterPage GetLanguageandModules();
        string SaveImages(string imagePath, int moduleId, List<IFormFile> files);
        List<Images> GetImageDetails(int moduleId);

        DataTable GetModuleContent(string ModuleName, string Languagecode);
        MasterPage GetModuleContentById(int ModuleId, string Languagecode);
        MasterPage GetNewsAndLabDetails(string serviceType,int languageId);

        int UsersContact(NewUsers users);
        List<LocationDetails> RetrieveLocations(string lang);

        int AddLocation(LocationDetails obj);

        int UpdateHomePageByLanguageId(int moduleId, string languageCode, string contentText, string MetaDescription, string Title, string MetUrl);
        List<HomeLabs> RetrieveHomeLabs(string language);
        int UpdatePreviewPageByLanguageModuleId(int moduleId, string languageCode, string HtmlContent, string MetaDescription, string MetaTitle, string MetUrl);
        Preview ShowPreivew(int ModuleId, string LanguageCode);
    }
}
