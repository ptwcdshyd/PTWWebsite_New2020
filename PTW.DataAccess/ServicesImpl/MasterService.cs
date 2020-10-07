using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;
using PTW.DataAccess.Models;
using PTW.DataAccess.Services;
using PTW.DBAccess;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace PTW.DataAccess.ServicesImpl
{
    public sealed partial class MasterService : DBDataAccess, IMasterService
    {
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
                    //if (dtResult != null && dtResult.Rows.Count > 0)
                    //{
                    //    masterPage.HtmlContent = dtResult.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals(ModuleName)).Select(y => Convert.ToString(y["Content"])).FirstOrDefault(); 
                    //    masterPage.HeaderContent = dtResult.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Header")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
                    //    masterPage.FooterContent = dtResult.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Footer")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
                    //}


                }
                return dtResult;
            }


            catch { throw; }

            finally
            {
                if (command != null) command.Dispose();
                command = null;

            }
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
                return masterPage;
            }


            catch { throw; }

            finally
            {
                if (command != null) command.Dispose();
                command = null;

            }
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

                return resultCode;
            }




            catch { throw; }



            finally
            {
                if (command != null) command.Dispose();
                command = null;



            }
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
                return masterPage;
            }


            catch { throw; }

            finally
            {
                if (command != null) command.Dispose();
                command = null;

            }
        }

        public string SaveImages(string imagePath, int moduleId, List<IFormFile> files)
        {
            string message;
            CustomCommand command = null;
            CustomXmlHelper customXmlHelper = new CustomXmlHelper();
            // MasterPage masterPage = new MasterPage();

            try
            {
                using (command = new CustomCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Insert_Images";

                    command.AddParameterWithValue("@Image_Path", imagePath);
                    command.AddParameterWithValue("@Module_Id", moduleId);
                    command.AddParameterWithValue("@Image_Data", customXmlHelper.CustomImagesXml(files));
                    //  Execute command and get values from output parameters.
                    int result = ExecuteNonQuery(command, false);
                    if (result != 0)
                    {
                        message = "successfully saved";
                    }
                    else
                    {
                        message = "not saved";
                    }



                }
                return message;
            }




            catch { throw; }



            finally
            {
                if (command != null) command.Dispose();
                command = null;



            }
        }

        public List<Images> GetImageDetails(int moduleId)
        {
            CustomCommand command = null;
            List<Images> images = new List<Images>();

            try
            {
                using (command = new CustomCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "RetrieveImages";
                    command.AddParameterWithValue("@Module_Id", moduleId);
                    //  Execute command and get values from output parameters.
                    DataSet dtResult = ExecuteDataSet(command);
                    if (dtResult != null && dtResult.Tables[0].Rows.Count > 0)
                    {

                        for (int i = 0; i < dtResult.Tables[0].Rows.Count; i++)
                        {

                            Images imag = new Images();
                            imag.ImageName = dtResult.Tables[0].Rows[i]["ImageName"].ToString();
                            imag.ImagePath = dtResult.Tables[0].Rows[i]["Imagepath"].ToString();
                            images.Add(imag);
                        }

                    }

                }
                return images;
            }


            catch { throw; }

            finally
            {
                if (command != null) command.Dispose();
                command = null;

            }

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

                return masterPage;
            }




            catch { throw; }



            finally
            {
                if (command != null) command.Dispose();
                command = null;



            }

        }

        public int UsersContact(NewUsers users)
        {
            CustomCommand command = null;

            int result=0;
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
                return result;
            }


            catch { throw; }

            finally
            {
                if (command != null) command.Dispose();
                command = null;

            }

        }
        public List<LocationDetails> RetrieveLocations(string lang)
        {
            CustomCommand command = null;
            try
            {
                using (command = new CustomCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "RetrieveLocations";
                    command.AddParameterWithValue("@languagecode", lang);
                    //
                    DataTable result=ExecuteTable(command);

                    List<LocationDetails> locationslist = new List<LocationDetails>();

                    if (result != null && result.Rows.Count > 0)
                    {
                        for (int i = 0; i < result.Rows.Count; i++)
                        {
                            LocationDetails obj = new LocationDetails();
                            obj.Region = result.Rows[i]["Region"].ToString();
                            obj.Country = result.Rows[i]["Country"].ToString();
                            obj.Location = result.Rows[i]["Location"].ToString();
                            obj.Address =result.Rows[i]["Address"].ToString();
                            obj.Website = result.Rows[i]["Website"].ToString();
                            obj.Title = result.Rows[i]["Title"].ToString();
                            obj.GoogleMapHeading = result.Rows[i]["GoogleMapHeading"].ToString();
                            obj.GoogleMap = result.Rows[i]["GoogleMap"].ToString();
                            obj.Target = result.Rows[i]["Target"].ToString();
                            obj.TargetLocation = result.Rows[i]["TargetLocation"].ToString();
                           
                            locationslist.Add(obj);

                        }
                    }

                    return locationslist;
                }

       
            }




            catch { throw; }



            finally
            {
                if (command != null) command.Dispose();
                command = null;



            }



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
                return result;
            }


            catch { throw; }

            finally
            {
                if (command != null) command.Dispose();
                command = null;

            }
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

                return resultCode;
            }


            catch(Exception exception) { throw exception; }

            finally
            {
                if (command != null) command.Dispose();
                command = null;
            }
        }

        public List<HomeLabs> RetrieveHomeLabs(string language)
        {
            CustomCommand command = null;
            try
            {
                using (command = new CustomCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "GetHomeLabs";
                    command.AddParameterWithValue("@languageCode", language);
                    //
                    DataTable result = ExecuteTable(command);

                    List<HomeLabs> homeLabs = new List<HomeLabs>();

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

                    return homeLabs;
                }


            }




            catch { throw; }



            finally
            {
                if (command != null) command.Dispose();
                command = null;



            }



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

                return resultCode;
            }


            catch (Exception exception) { throw exception; }

            finally
            {
                if (command != null) command.Dispose();
                command = null;
            }
        }

        public Preview ShowPreivew(int ModuleId,string LanguageCode)
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

                    return preview;
                }


            }




            catch { throw; }



            finally
            {
                if (command != null) command.Dispose();
                command = null;



            }



        }

    }
}
