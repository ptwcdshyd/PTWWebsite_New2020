using Microsoft.AspNetCore.Http;
using PTW.DataAccess.Models;
using System;
using System.Collections;
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

        List<Sections> GetSubModuleList(int ModuleId, string Languagecode);
        ArrayList GetSubModuleContent(int ModuleId, string Languagecode, int SectionId);
        MasterPage GetSEODetails(int ModuleId, string Languagecode, int SectionId);

        int UpdateSectionContent(int SectionId, int moduleId, string languageCode, string contentText, string Metatage, string Title, string MetUrl, bool IsActive, int ShortOrder);

        DataTable GetModuleContentSectionwise(int ModuleId, string Languagecode);

        int UpdatePreviewContentByLanguageModuleId(int SectionId, int moduleId, string languageCode, string HtmlContent, string MetaDescription, string MetaTitle, string MetUrl);
        string GetPreviewSubContent(int ModuleId, string Languagecode, int SectionId);
    }
}
