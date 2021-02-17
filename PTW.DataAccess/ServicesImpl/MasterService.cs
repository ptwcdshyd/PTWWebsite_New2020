using LoggerService;
using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;
using PTW.DataAccess.Models;
using PTW.DataAccess.Services;
using PTW.DBAccess;
using System;
using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace PTW.DataAccess.ServicesImpl
{
    public sealed partial class MasterService : DBDataAccess, IMasterService
    {
        private readonly ILoggerManager _loggerManager;
        public MasterService(ILoggerManager loggerManager)
        {
            _loggerManager = loggerManager;
        }

        //public MasterPage GetDashboardDetails(int LoginUserId, int LanguageID, int ModuleId,string Languagecode)
        //{
        //    CustomCommand command = null;
        //    MasterPage masterPage = new MasterPage();

        //    try
        //    {
        //        using (command = new CustomCommand())
        //        {
        //            command.CommandType = CommandType.StoredProcedure;
        //            command.CommandText = "GetLandingPageDetails";
        //            command.AddParameterWithValue("@L_userId", LoginUserId);
        //            //command.AddParameterWithValue("@LanguageID", LanguageID);
        //            command.AddParameterWithValue("@ModuleID", ModuleId);
        //            command.AddParameterWithValue("@Languagecode", Languagecode);
        //            //  Execute command and get values from output parameters.
        //            DataSet dtResult = ExecuteDataSet(command);
        //            if (dtResult != null && dtResult.Tables[0].Rows.Count > 0)
        //            {
        //                masterPage.ModuleName = Convert.ToString(dtResult.Tables[0].Rows[0]["ModuleName"]);
        //                masterPage.HtmlContent = Convert.ToString(dtResult.Tables[0].Rows[0]["Content"]);
        //            }

        //        }
        //        return masterPage;
        //    }


        //    catch { throw; }

        //    finally
        //    {
        //        if (command != null) command.Dispose();
        //        command = null;

        //    }
        //}

        public DataTable GetModuleContent(string ModuleName, string Languagecode)
        {
            CustomCommand command = null;
            DataTable dtResult = new DataTable();

            try
            {
                using (command = new CustomCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "GetModuleContent";
                    command.AddParameterWithValue("@ModuleName", ModuleName);
                    command.AddParameterWithValue("@Languagecode", Languagecode);
                    //  Execute command and get values from output parameters.
                    dtResult = ExecuteTable(command);

                }
            }
            catch (Exception exception)
            {
                _loggerManager.LogError(string.Format("Method: GetModuleContent, ErrorMessage: file: {0} ", exception));
            }
            finally
            {
                if (command != null) command.Dispose();
                command = null;
            }
            return dtResult;
        }

        public MasterPage GetModuleContentById(int ModuleId, string Languagecode)
        {
            CustomCommand command = null;
            MasterPage masterPage = new MasterPage();

            try
            {
                using (command = new CustomCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "GetModuleContentById";
                    command.AddParameterWithValue("@ModuleID", ModuleId);
                    command.AddParameterWithValue("@Languagecode", Languagecode);
                    //  Execute command and get values from output parameters.
                    DataSet dtResult = ExecuteDataSet(command);
                    if (dtResult != null && dtResult.Tables[0].Rows.Count > 0)
                    {
                        // masterPage.HeaderContent = Convert.ToString(dtResult.Tables[0].Rows[0]["ModuleName"]);
                        masterPage.HtmlContent = Convert.ToString(dtResult.Tables[0].Rows[0]["Content"]);
                        masterPage.MetaDescription = Convert.ToString(dtResult.Tables[0].Rows[0]["MetaDescription"]);
                        masterPage.MetaTitle = Convert.ToString(dtResult.Tables[0].Rows[0]["MetaTitle"]);
                        masterPage.MetaUrl = Convert.ToString(dtResult.Tables[0].Rows[0]["MetaUrl"]);
                    }
                }
            }
            catch (Exception exception)
            {
                _loggerManager.LogError(string.Format("Method: GetModuleContentById, ErrorMessage: file: {0} ", exception));
            }
            finally
            {
                if (command != null) command.Dispose();
                command = null;
            }
            return masterPage;
        }

        public int UpdateContentByModelIdAndLanguageId(int moduleId, string languageCode, string contentText, string Metatage, string Title)
        {
            int resultCode = 0;
            CustomCommand command = null;
            // MasterPage masterPage = new MasterPage();

            try
            {
                using (command = new CustomCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "UpdateContentByModuleIdAndLanguageId";
                    command.AddParameterWithValue("@Module_Id", moduleId);
                    command.AddParameterWithValue("@Language_Code", languageCode);
                    command.AddParameterWithValue("@ContentText", contentText);
                    command.AddParameterWithValue("@MetaDescription", Metatage);
                    command.AddParameterWithValue("@MetaTitle", Title);

                    //  Execute command and get values from output parameters.
                    int result = ExecuteNonQuery(command, false);
                    if (result > 0)
                    {
                        resultCode = 1;
                    }
                }
            }

            catch (Exception exception)
            {
                _loggerManager.LogError(string.Format("Method: UpdateContentByModelIdAndLanguageId, ErrorMessage: file: {0} ", exception));
            }

            finally
            {
                if (command != null) command.Dispose();
                command = null;
            }
            return resultCode;
        }

        public MasterPage GetLanguageandModules()
        {
            CustomCommand command = null;
            MasterPage masterPage = new MasterPage();

            try
            {
                using (command = new CustomCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "RetrieveModulesAndLanguages";
                    //  Execute command and get values from output parameters.
                    DataSet dtResult = ExecuteDataSet(command);
                    if (dtResult != null && dtResult.Tables[1].Rows.Count > 0)
                    {
                        masterPage.LanguageList = new List<Languages>();
                        for (int i = 0; i < dtResult.Tables[1].Rows.Count; i++)
                        {
                            Languages languages = new Languages();
                            languages.LanguageId = Convert.ToInt32(dtResult.Tables[1].Rows[i]["LanguageId"]);
                            languages.Language = dtResult.Tables[1].Rows[i]["LanguageName"].ToString();
                            languages.LanguageCode = dtResult.Tables[1].Rows[i]["Code"].ToString();
                            masterPage.LanguageList.Add(languages);
                        }
                        masterPage.ModuleList = new List<Module>();
                        for (int i = 0; i < dtResult.Tables[0].Rows.Count; i++)
                        {
                            if (i == 0)
                            {
                                Module mode = new Module();
                                mode.ModuleId = 0;
                                mode.ModuleName = "Module";
                                masterPage.ModuleList.Add(mode);
                            }

                            Module module = new Module();
                            module.ModuleId = Convert.ToInt32(dtResult.Tables[0].Rows[i]["ModuleId"]);
                            module.ModuleName = dtResult.Tables[0].Rows[i]["ModuleName"].ToString();

                            masterPage.ModuleList.Add(module);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                _loggerManager.LogError(string.Format("Method: GetLanguageandModules, ErrorMessage: file: {0} ", exception));
            }
            finally
            {
                if (command != null) command.Dispose();
                command = null;
            }
            return masterPage;
        }

        public MasterPage GetNewsAndLabDetails(string serviceType, int languageId)
        {

            MasterPage masterPage = new MasterPage();
            CustomCommand command = null;
            // MasterPage masterPage = new MasterPage();

            try
            {
                using (command = new CustomCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "RetrieveNewsAndLabsForService";
                    command.AddParameterWithValue("@ServiceType", serviceType);
                    command.AddParameterWithValue("@LanguageId", languageId);

                    masterPage.NewsAndLabs = new List<NewsAndLabs>();
                    //  Execute command and get values from output parameters.
                    DataSet result = ExecuteDataSet(command);

                    if (result != null && result.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < result.Tables[0].Rows.Count; i++)
                        {
                            NewsAndLabs obj = new NewsAndLabs();
                            obj.Type = result.Tables[0].Rows[i]["Type"].ToString();
                            obj.Title = result.Tables[0].Rows[i]["Title"].ToString();
                            obj.ShortDescription = result.Tables[0].Rows[i]["ShortDescription"].ToString();
                            masterPage.NewsAndLabs.Add(obj);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                _loggerManager.LogError(string.Format("Method: GetNewsAndLabDetails, ErrorMessage: file: {0} ", exception));
            }
            finally
            {
                if (command != null) command.Dispose();
                command = null;
            }
            return masterPage;

        }

        public int UsersContact(NewUsers users)
        {
            CustomCommand command = null;

            int result = 0;
            try
            {
                using (command = new CustomCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "UsersContactInformation";
                    command.AddParameterWithValue("@FirstName", users.FirstName);
                    command.AddParameterWithValue("@LastName", users.LastName);
                    command.AddParameterWithValue("@Email", users.Email);
                    command.AddParameterWithValue("@ContactNumber", users.ContactNumber);
                    command.AddParameterWithValue("@CompanyName", users.CompanyName);
                    command.AddParameterWithValue("@AreaOfInterest", users.AreaOfInterest);
                    command.AddParameterWithValue("@HearAbout", users.HearAbout);
                    command.AddParameterWithValue("@ContactMessage", users.ContactMessage);
                    result = ExecuteNonQuery(command, false);
                }
            }
            catch (Exception exception)
            {
                _loggerManager.LogError(string.Format("Method: UsersContact, ErrorMessage: file: {0} ", exception));
            }

            finally
            {
                if (command != null) command.Dispose();
                command = null;
            }

            return result;

        }
        public List<LocationDetails> RetrieveLocations(string lang)
        {
            CustomCommand command = null;
            List<LocationDetails> locationslist = new List<LocationDetails>();
            try
            {
                using (command = new CustomCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "RetrieveLocations";
                    command.AddParameterWithValue("@languagecode", lang);
                    //
                    DataTable result = ExecuteTable(command);

                    if (result != null && result.Rows.Count > 0)
                    {
                        for (int i = 0; i < result.Rows.Count; i++)
                        {
                            LocationDetails obj = new LocationDetails();
                            obj.ID = Convert.ToInt32(result.Rows[i]["ID"]);
                            obj.Region = Convert.ToString(result.Rows[i]["Region"]);
                            obj.Country = Convert.ToString(result.Rows[i]["Country"]);
                            obj.Location = Convert.ToString(result.Rows[i]["Location"]);
                            obj.Address = Convert.ToString(result.Rows[i]["Address"]);
                            obj.Website = Convert.ToString(result.Rows[i]["Website"]);
                            obj.Title = Convert.ToString(result.Rows[i]["Title"]);
                            obj.GoogleMapHeading = Convert.ToString(result.Rows[i]["GoogleMapHeading"]);
                            obj.GoogleMap = Convert.ToString(result.Rows[i]["GoogleMap"]);
                            obj.Target = Convert.ToString(result.Rows[i]["Target"]);
                            obj.TargetLocation = Convert.ToString(result.Rows[i]["TargetLocation"]);

                            locationslist.Add(obj);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                _loggerManager.LogError(string.Format("Method: RetrieveLocations, ErrorMessage: file: {0} ", exception));
            }
            finally
            {
                if (command != null) command.Dispose();
                command = null;
            }
            return locationslist;
        }
        public int AddLocation(LocationDetails obj)
        {
            CustomCommand command = null;
            int result = 0;
            try
            {
                using (command = new CustomCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "AddLocation";
                    command.AddParameterWithValue("@Region", obj.Region);
                    command.AddParameterWithValue("@Country", obj.Country);
                    command.AddParameterWithValue("@Location", obj.Location);
                    command.AddParameterWithValue("@Address", obj.Address);
                    command.AddParameterWithValue("@Title", obj.Title);
                    command.AddParameterWithValue("@GoogleMapHeading", obj.GoogleMapHeading);
                    command.AddParameterWithValue("@GoogleMap", obj.GoogleMap);
                    command.AddParameterWithValue("@languagecode", obj.Language);
                    result = ExecuteNonQuery(command, false);
                }
            }
            catch (Exception exception)
            {
                _loggerManager.LogError(string.Format("Method: AddLocation, ErrorMessage: file: {0} ", exception));
            }
            finally
            {
                if (command != null) command.Dispose();
                command = null;
            }
            return result;
        }


        public int UpdateHomePageByLanguageId(int moduleId, string languageCode, string contentText, string Metatage, string Title, string MetUrl)
        {
            int resultCode = 0;
            CustomCommand command = null;
            // MasterPage masterPage = new MasterPage();

            try
            {
                using (command = new CustomCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "UpdateNewContentByModuleIdAndLanguageId";
                    command.AddParameterWithValue("@Module_Id", moduleId);
                    command.AddParameterWithValue("@Language_Code", languageCode);
                    command.AddParameterWithValue("@ContentText", contentText);
                    command.AddParameterWithValue("@MetaDescription", Metatage);
                    command.AddParameterWithValue("@MetaTitle", Title);
                    command.AddParameterWithValue("@MetaUrl", MetUrl);

                    //  Execute command and get values from output parameters.
                    int result = ExecuteNonQuery(command, false);
                    if (result > 0)
                    {
                        resultCode = 1;
                    }
                }
            }
            catch (Exception exception)
            {
                _loggerManager.LogError(string.Format("Method: UpdateHomePageByLanguageId, ErrorMessage: file: {0} ", exception));
            }

            finally
            {
                if (command != null) command.Dispose();
                command = null;
            }
            return resultCode;
        }

        public List<HomeLabs> RetrieveHomeLabs(string language)
        {
            CustomCommand command = null;
            List<HomeLabs> homeLabs = new List<HomeLabs>();
            try
            {
                using (command = new CustomCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "GetHomeLabs";
                    command.AddParameterWithValue("@languageCode", language);
                    //
                    DataTable result = ExecuteTable(command);

                    if (result != null && result.Rows.Count > 0)
                    {
                        for (int i = 0; i < result.Rows.Count; i++)
                        {
                            HomeLabs obj = new HomeLabs();
                            obj.Title = Convert.ToString(result.Rows[i]["Title"]);
                            obj.ImagePath = Convert.ToString(result.Rows[i]["ImagePath"]);
                            obj.ShortOrder = Convert.ToInt32(result.Rows[i]["ShortOrder"]);
                            obj.NavigateUrl = Convert.ToString(result.Rows[i]["NavigateUrl"]);

                            homeLabs.Add(obj);

                        }
                    }
                }
            }
            catch (Exception exception)
            {
                _loggerManager.LogError(string.Format("Method: RetrieveHomeLabs, ErrorMessage: file: {0} ", exception));
            }
            finally
            {
                if (command != null) command.Dispose();
                command = null;
            }
            return homeLabs;


        }

        public int UpdatePreviewPageByLanguageModuleId(int moduleId, string languageCode, string HtmlContent, string MetaDescription, string MetaTitle, string MetUrl)
        {
            int resultCode = 0;
            CustomCommand command = null;
            // MasterPage masterPage = new MasterPage();

            try
            {
                using (command = new CustomCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "UpdatePreviewPageByLanguageModuleId";
                    command.AddParameterWithValue("@_moduleId", moduleId);
                    command.AddParameterWithValue("@_languageCode", languageCode);
                    command.AddParameterWithValue("@htmlContent", HtmlContent);
                    command.AddParameterWithValue("@metaTitle", MetaTitle);
                    command.AddParameterWithValue("@metaDescription", MetaDescription);
                    command.AddParameterWithValue("@metaUrl", MetUrl);

                    //  Execute command and get values from output parameters.
                    int result = ExecuteNonQuery(command, false);
                    if (result > 0)
                    {
                        resultCode = 1;
                    }

                }

            }
            catch (Exception exception)
            {
                _loggerManager.LogError(string.Format("Method: UpdatePreviewPageByLanguageModuleId, ErrorMessage: file: {0} ", exception));
            }
            finally
            {
                if (command != null) command.Dispose();
                command = null;
            }
            return resultCode;
        }

        public Preview ShowPreivew(int ModuleId, string LanguageCode)
        {
            CustomCommand command = null;
            Preview preview = new Preview();
            try
            {
                using (command = new CustomCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "GetPreviewPageByLanguageAndModuleId";
                    command.AddParameterWithValue("@_moduleId", ModuleId);
                    command.AddParameterWithValue("@_languageCode", LanguageCode);
                    //
                    DataTable result = ExecuteTable(command);

                    if (result != null && result.Rows.Count > 0)
                    {
                        preview.MetaTitle = Convert.ToString(result.Rows[0]["MetaTitle"]);
                        preview.MetaDescription = Convert.ToString(result.Rows[0]["MetaDescription"]);
                        preview.MetaUrl = Convert.ToString(result.Rows[0]["MetaUrl"]);
                        preview.HtmlContent = Convert.ToString(result.Rows[0]["HtmlContent"]);
                    }
                }
            }
            catch (Exception exception)
            {
                _loggerManager.LogError(string.Format("Method: ShowPreivew, ErrorMessage: file: {0} ", exception));
            }
            finally
            {
                if (command != null) command.Dispose();
                command = null;
            }
            return preview;


        }

        public List<Sections> GetSubModuleList(int ModuleId, string Languagecode)
        {
            CustomCommand command = null;
            DataTable dtResult = new DataTable();
            List<Sections> sectionList = new List<Sections>();

            try
            {
                using (command = new CustomCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "GetSubModuleList";
                    command.AddParameterWithValue("@_ModuleId", ModuleId);
                    command.AddParameterWithValue("@_languageCode", Languagecode);
                    //  Execute command and get values from output parameters.
                    dtResult = ExecuteTable(command);
                    if (dtResult != null && dtResult.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtResult.Rows.Count; i++)
                        {
                            Sections section = new Sections();
                            section.SectionId = Convert.ToInt32(dtResult.Rows[i]["SectionId"]);
                            section.SectionName = Convert.ToString(dtResult.Rows[i]["SectionName"]);
                            sectionList.Add(section);
                        }
                    }

                }
            }
            catch (Exception exception)
            {
                _loggerManager.LogError(string.Format("Method: GetSubModuleList, ErrorMessage: file: {0} ", exception));
            }
            finally
            {
                if (command != null) command.Dispose();
                command = null;
            }
            return sectionList;
        }

        public ArrayList GetSubModuleContent(int ModuleId, string Languagecode, int SectionId)
        {
            CustomCommand command = null;
            DataTable dtResult = new DataTable();
            ArrayList result = new ArrayList();

            try
            {
                using (command = new CustomCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "GetSubModuleContent";
                    command.AddParameterWithValue("@_ModuleId", ModuleId);
                    command.AddParameterWithValue("@_languageCode", Languagecode);
                    command.AddParameterWithValue("@_sectionId", SectionId);
                    //  Execute command and get values from output parameters.
                    dtResult = ExecuteTable(command);
                    result.Add(Convert.ToString(dtResult.Rows[0][0]));//html code
                    result.Add(Convert.ToBoolean(dtResult.Rows[0][1]));//IsActive
                    result.Add(Convert.ToInt32(dtResult.Rows[0][2]));//Short Order
                }
            }
            catch (Exception exception)
            {
                _loggerManager.LogError(string.Format("Method: GetSubModuleContent, ErrorMessage: file: {0} ", exception));
            }
            finally
            {
                if (command != null) command.Dispose();
                command = null;
            }
            return result;
        }

        public MasterPage GetSEODetails(int ModuleId, string Languagecode, int SectionId)
        {
            CustomCommand command = null;
            DataTable dtResult = new DataTable();
            MasterPage masterPage = new MasterPage();
            try
            {
                using (command = new CustomCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "GetSEODetails";
                    command.AddParameterWithValue("@_ModuleId", ModuleId);
                    command.AddParameterWithValue("@_languageCode", Languagecode);
                    command.AddParameterWithValue("@_sectionId", SectionId);
                    //  Execute command and get values from output parameters.
                    dtResult = ExecuteTable(command);
                    if (dtResult != null && dtResult.Rows.Count > 0)
                    {
                        // masterPage.HeaderContent = Convert.ToString(dtResult.Tables[0].Rows[0]["ModuleName"]);
                        //masterPage.HtmlContent = Convert.ToString(dtResult.Rows[0]["Content"]);
                        masterPage.MetaDescription = Convert.ToString(dtResult.Rows[0]["MetaDescription"]);
                        masterPage.MetaTitle = Convert.ToString(dtResult.Rows[0]["MetaTitle"]);
                        masterPage.MetaUrl = Convert.ToString(dtResult.Rows[0]["MetaUrl"]);
                    }
                }
            }
            catch (Exception exception)
            {
                _loggerManager.LogError(string.Format("Method: GetSubModuleContent, ErrorMessage: file: {0} ", exception));
            }
            finally
            {
                if (command != null) command.Dispose();
                command = null;
            }
            return masterPage;
        }

        public int UpdateSectionContent(int SectionId, int moduleId, string languageCode, string contentText, string Metatage, string Title, string MetUrl,bool IsActive,int ShortOrder)
        {
            int resultCode = 0;
            CustomCommand command = null;
            // MasterPage masterPage = new MasterPage();

            try
            {
                using (command = new CustomCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    // command.CommandText = "UpdateNewContentByModuleIdAndLanguageId";
                    command.CommandText = "UpdateSectionContent";
                    command.AddParameterWithValue("@_SectionId", SectionId);
                    command.AddParameterWithValue("@_ModuleId", moduleId);
                    command.AddParameterWithValue("@_languageCode", languageCode);
                    command.AddParameterWithValue("@ContentText", contentText);
                    command.AddParameterWithValue("@MetaDescription", Metatage);
                    command.AddParameterWithValue("@MetaTitle", Title);
                    command.AddParameterWithValue("@MetaUrl", MetUrl);
                    command.AddParameterWithValue("@_IsActive", IsActive);
                    command.AddParameterWithValue("@_ShortOrder", ShortOrder);
                    //  Execute command and get values from output parameters.
                    int result = ExecuteNonQuery(command, false);
                    if (result > 0)
                    {
                        resultCode = 1;
                    }
                }
            }
            catch (Exception exception)
            {
                _loggerManager.LogError(string.Format("Method: UpdateHomePageByLanguageId, ErrorMessage: file: {0} ", exception));
            }

            finally
            {
                if (command != null) command.Dispose();
                command = null;
            }
            return resultCode;
        }

        public DataTable GetModuleContentSectionwise(int ModuleId, string Languagecode)
        {
            CustomCommand command = null;
            DataTable dtResult = new DataTable();

            try
            {
                using (command = new CustomCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "GetModuleContentSectionWise";
                    command.AddParameterWithValue("@ModuleId", ModuleId);
                    command.AddParameterWithValue("@Languagecode", Languagecode);
                    //  Execute command and get values from output parameters.
                    dtResult = ExecuteTable(command);

                }
            }
            catch (Exception exception)
            {
                _loggerManager.LogError(string.Format("Method: GetModuleContent, ErrorMessage: file: {0} ", exception));
            }
            finally
            {
                if (command != null) command.Dispose();
                command = null;
            }
            return dtResult;
        }

        public int UpdatePreviewContentByLanguageModuleId(int SectionId, int moduleId, string languageCode, string HtmlContent, string MetaDescription, string MetaTitle, string MetUrl)
        {
            int resultCode = 0;
            CustomCommand command = null;
            // MasterPage masterPage = new MasterPage();

            try
            {
                using (command = new CustomCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "UpdatePreviewPageByLanguageModuleId";
                    command.AddParameterWithValue("@_sectionId", SectionId);
                    command.AddParameterWithValue("@_moduleId", moduleId);
                    command.AddParameterWithValue("@_languageCode", languageCode);
                    command.AddParameterWithValue("@htmlContent", HtmlContent);
                    command.AddParameterWithValue("@metaTitle", MetaTitle);
                    command.AddParameterWithValue("@metaDescription", MetaDescription);
                    command.AddParameterWithValue("@metaUrl", MetUrl);

                    //  Execute command and get values from output parameters.
                    int result = ExecuteNonQuery(command, false);
                    if (result > 0)
                    {
                        resultCode = 1;
                    }

                }

            }
            catch (Exception exception)
            {
                _loggerManager.LogError(string.Format("Method: UpdatePreviewPageByLanguageModuleId, ErrorMessage: file: {0} ", exception));
            }
            finally
            {
                if (command != null) command.Dispose();
                command = null;
            }
            return resultCode;
        }

        public string GetPreviewSubContent(int ModuleId, string Languagecode, int SectionId)
        {
            CustomCommand command = null;
            DataTable dtResult = new DataTable();
            string result = string.Empty;

            try
            {
                using (command = new CustomCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "GetPreviewPageSectoinContent";
                    command.AddParameterWithValue("@_ModuleId", ModuleId);
                    command.AddParameterWithValue("@_languageCode", Languagecode);
                    command.AddParameterWithValue("@_sectionId", SectionId);
                    //  Execute command and get values from output parameters.
                    dtResult = ExecuteTable(command);
                    result = Convert.ToString(dtResult.Rows[0][0]);
                }
            }
            catch (Exception exception)
            {
                _loggerManager.LogError(string.Format("Method: GetPreviewSubContent, ErrorMessage: file: {0} ", exception));
            }
            finally
            {
                if (command != null) command.Dispose();
                command = null;
            }
            return result;
        }

    }
}
