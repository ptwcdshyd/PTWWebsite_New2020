using PTW.DataAccess.Models;
using PTW.DataAccess.Services;
using PTW.DBAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace PTW.DataAccess.ServicesImpl
{
    public class LabServicecs: DBDataAccess,ILabService
    {
        public List<Labs> GetAllLabsDetails(string LanguageCode)
        {
            CustomCommand command = null;
            List<Labs> LabsList = new List<Labs>();
            try
            {
                using (command = new CustomCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Proc_RetrieveAllLabs";
                    //  Execute command and get values from output parameters.
                    DataTable dtResult = ExecuteTable(command);
                    command.AddParameterWithValue("@p_Languagecode", LanguageCode);
                    if (dtResult != null && dtResult.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtResult.Rows.Count; i++)
                        {
                            Labs labs = new Labs();

                            labs.LabId = Convert.ToInt32(dtResult.Rows[i]["LabId"]);
                            labs.ServiceTypeId = Convert.ToInt32(dtResult.Rows[i]["ServiceTypeId"]);
                            //labs.LanguageCode = Convert.ToString(dtResult.Rows[i]["LanguageCode"]);
                            labs.Name = Convert.ToString(dtResult.Rows[i]["Name"]);
                            labs.ShortDescription = Convert.ToString(dtResult.Rows[i]["ShortDescription"]);
                            labs.Description = Convert.ToString(dtResult.Rows[i]["Description"]);
                            labs.ImageName = Convert.ToString(dtResult.Rows[i]["ImageName"]);
                            labs.ImageUrl = Convert.ToString(dtResult.Rows[i]["ImageUrl"]);
                            labs.IsActive = Convert.ToInt32(dtResult.Rows[i]["IsActive"]);
                            //labs.OnNewWebsiteNow = Convert.ToBoolean(dtResult.Rows[i]["OnNewWebsiteNow"]);
                            labs.DesktopImageUrl = Convert.ToString(dtResult.Rows[i]["DesktopImageUrl"]);
                            labs.TabImageNameHorizondaUrl = Convert.ToString(dtResult.Rows[i]["TabImageNameHorizondaUrl"]);
                            labs.TabImageNamVerticalUrl = Convert.ToString(dtResult.Rows[i]["TabImageNamVerticalUrl"]);
                            labs.MobileImageNameUrl = Convert.ToString(dtResult.Rows[i]["MobileImageNameUrl"]);
                            labs.DefaultImageUrl = Convert.ToString(dtResult.Rows[i]["DefaultImageUrl"]);
                            labs.ImageAlternateText = Convert.ToString(dtResult.Rows[i]["ImageAlternateText"]);
                            
                            labs.Stopped = Convert.ToInt32(dtResult.Rows[i]["Stopped"]);
                            labs.LabType = Convert.ToString(dtResult.Rows[i]["LabType"]);
                            labs.MetaTags = Convert.ToString(dtResult.Rows[i]["MetaTags"]);
                            
                            labs.CustomerExperienceImageUrl = Convert.ToString(dtResult.Rows[i]["CustomerExperienceImageUrl"]);
                            labs.QualityAssuranceImageUrl = Convert.ToString(dtResult.Rows[i]["QualityAssuranceImageUrl"]);
                            labs.LocalizationImageUrl = Convert.ToString(dtResult.Rows[i]["LocalizationImageUrl"]);
                            labs.AudioProductionImageUrl = Convert.ToString(dtResult.Rows[i]["AudioProductionImageUrl"]);
                            labs.EngineeringImageUrl = Convert.ToString(dtResult.Rows[i]["EngineeringImageUrl"]);
                            labs.CampaignImageUrl = Convert.ToString(dtResult.Rows[i]["CampaignImageUrl"]);
                            labs.CEandQAImageUrl = Convert.ToString(dtResult.Rows[i]["CEandQAImageUrl"]);


                            labs.FilterCustomerExperience = Convert.ToString(dtResult.Rows[i]["FilterCustomerExperience"]);
                            labs.FilterQualityAssurance = Convert.ToString(dtResult.Rows[i]["FilterQualityAssurance"]);
                            labs.FilterLocalization = Convert.ToString(dtResult.Rows[i]["FilterLocalization"]);
                            labs.FilterAudioProduction = Convert.ToString(dtResult.Rows[i]["FilterAudioProduction"]);
                            labs.FilterEngineering = Convert.ToString(dtResult.Rows[i]["FilterEngineering"]);
                            labs.FilterCampaign = Convert.ToString(dtResult.Rows[i]["FilterCampaign"]);

                            LabsList.Add(labs);
                        }
                    }

                }
                return LabsList;
            }
            catch(Exception ex) {
                throw ex;
            }
            finally
            {
                if (command != null) command.Dispose();
                command = null;
            }
        }

        public List<Labs> GetAllLatestInsights(string LanguageCode)
        {
            CustomCommand command = null;
            List<Labs> LabsList = new List<Labs>();
            try
            {
                using (command = new CustomCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Proc_RetrieveLatestInsights";
                    //  Execute command and get values from output parameters.
                    command.AddParameterWithValue("@p_Languagecode", LanguageCode);
                    DataTable dtResult = ExecuteTable(command);
                    
                    if (dtResult != null && dtResult.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtResult.Rows.Count; i++)
                        {
                            Labs labs = new Labs();

                            labs.LabId = Convert.ToInt32(dtResult.Rows[i]["LabId"]);
                            labs.ServiceTypeId = Convert.ToInt32(dtResult.Rows[i]["ServiceTypeId"]);
                            labs.Name = Convert.ToString(dtResult.Rows[i]["Name"]);
                            labs.ShortDescription = Convert.ToString(dtResult.Rows[i]["ShortDescription"]);
                            labs.Description = Convert.ToString(dtResult.Rows[i]["Description"]);
                            labs.ImageName = Convert.ToString(dtResult.Rows[i]["ImageName"]);
                            labs.ImageUrl = Convert.ToString(dtResult.Rows[i]["ImageUrl"]);
                            labs.IsActive = Convert.ToInt32(dtResult.Rows[i]["IsActive"]);
                            //labs.OnNewWebsiteNow = Convert.ToBoolean(dtResult.Rows[i]["OnNewWebsiteNow"]);
                            labs.DesktopImageUrl = Convert.ToString(dtResult.Rows[i]["DesktopImageUrl"]);
                            labs.TabImageNameHorizondaUrl = Convert.ToString(dtResult.Rows[i]["TabImageNameHorizondaUrl"]);
                            labs.TabImageNamVerticalUrl = Convert.ToString(dtResult.Rows[i]["TabImageNamVerticalUrl"]);
                            labs.MobileImageNameUrl = Convert.ToString(dtResult.Rows[i]["MobileImageNameUrl"]);
                            labs.DefaultImageUrl = Convert.ToString(dtResult.Rows[i]["DefaultImageUrl"]);
                            labs.ImageAlternateText = Convert.ToString(dtResult.Rows[i]["ImageAlternateText"]);
                            //labs.ShortOrder = Convert.ToInt32(dtResult.Rows[i]["ShortOrder"]);
                            //labs.Reason = Convert.ToString(dtResult.Rows[i]["Reason"]);
                            //labs.MetaTags = Convert.ToString(dtResult.Rows[i]["MetaTags"]);
                            labs.LabType = Convert.ToString(dtResult.Rows[i]["LabType"]);
                            labs.CustomerExperienceImageUrl = Convert.ToString(dtResult.Rows[i]["CustomerExperienceImageUrl"]);
                            labs.QualityAssuranceImageUrl = Convert.ToString(dtResult.Rows[i]["QualityAssuranceImageUrl"]);
                            labs.LocalizationImageUrl = Convert.ToString(dtResult.Rows[i]["LocalizationImageUrl"]);
                            labs.AudioProductionImageUrl = Convert.ToString(dtResult.Rows[i]["AudioProductionImageUrl"]);
                            labs.EngineeringImageUrl = Convert.ToString(dtResult.Rows[i]["EngineeringImageUrl"]);
                            labs.CampaignImageUrl = Convert.ToString(dtResult.Rows[i]["CampaignImageUrl"]);
                            labs.LatestInsightImageUrl = Convert.ToString(dtResult.Rows[i]["LatestInsightImageUrl"]);
                            labs.Stopped = Convert.ToInt32(dtResult.Rows[i]["Stopped"]);

                            LabsList.Add(labs);
                        }
                    }

                }
                return LabsList;
            }


            catch(Exception ex) 
            {
                throw ex;
            }

            finally
            {
                if (command != null) command.Dispose();
                command = null;

            }
        }

        public List<Labs> GetSlider()
        {
            CustomCommand command = null;
            List<Labs> LabsList = new List<Labs>();
            try
            {
                using (command = new CustomCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Proc_RetrieveAllSlighter";
                    //  Execute command and get values from output parameters.
                    DataTable dtResult = ExecuteTable(command);                   
                    if (dtResult != null && dtResult.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtResult.Rows.Count; i++)
                        {
                            Labs labs = new Labs();
                           
                            //labs.LabId = Convert.ToInt32(dtResult.Rows[i]["LabId"]);
                            //labs.ServiceTypeId = Convert.ToInt32(dtResult.Rows[i]["ServiceTypeId"]);
                            //labs.LanguageCode = Convert.ToString(dtResult.Rows[i]["LanguageCode"]);
                            //labs.Name = Convert.ToString(dtResult.Rows[i]["Name"]);
                            //labs.ShortDescription = Convert.ToString(dtResult.Rows[i]["ShortDescription"]);
                            //labs.Description = Convert.ToString(dtResult.Rows[i]["Description"]);
                            //labs.ImageName = Convert.ToString(dtResult.Rows[i]["ImageName"]);
                            //labs.ImageUrl = Convert.ToString(dtResult.Rows[i]["ImageUrl"]);
                            //labs.IsActive = Convert.ToInt32(dtResult.Rows[i]["IsActive"]);
                            labs.Desk_ImageURL = Convert.ToString(dtResult.Rows[i]["Desk_ImageURL"]);
                            labs.TabV_ImageURL = Convert.ToString(dtResult.Rows[i]["TabV_ImageURL"]);
                            labs.TabH_ImageURL = Convert.ToString(dtResult.Rows[i]["TabH_ImageURL"]);
                            labs.Mobile_ImageURL = Convert.ToString(dtResult.Rows[i]["Mobile_ImageURL"]);
                            //labs.DefaultImageUrl = Convert.ToString(dtResult.Rows[i]["DefaultImageUrl"]);
                            labs.ImageAlternateText = Convert.ToString(dtResult.Rows[i]["ImageAlternateText"]);
                            labs.DeskL_ImageURL = Convert.ToString(dtResult.Rows[i]["DeskL_ImageURL"]);
                            
                            LabsList.Add(labs);
                        }
                    }

                }
                return LabsList;
            }


            catch (Exception ex) { 
                throw ex; 
            }

            finally
            {
                if (command != null) command.Dispose();
                command = null;

            }
        }

        public List<Labs> GetFutureLabArticles(string LabIdOrShortDescription)
        {
            CustomCommand command = null;
            List<Labs> LabsList = new List<Labs>();
            try
            {
                using (command = new CustomCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "RetrievTopFutureLABS";
                    command.AddParameterWithValue("p_LabIdOrShortDescription", LabIdOrShortDescription);
                    //  Execute command and get values from output parameters.
                    DataSet dtResult = ExecuteDataSet(command);                    
                    if (dtResult != null && dtResult.Tables[2].Rows.Count > 0)
                    {
                        for (int i = 0; i < dtResult.Tables[2].Rows.Count; i++)
                        {
                            Labs labs = new Labs();
                            labs.LabId = Convert.ToInt32(dtResult.Tables[2].Rows[i]["LabId"]);
                            labs.Name = Convert.ToString(dtResult.Tables[2].Rows[i]["Name"]);
                            labs.ShortDescription = Convert.ToString(dtResult.Tables[2].Rows[i]["ShortDescription"]);
                            labs.Description = Convert.ToString(dtResult.Tables[2].Rows[i]["Description"]);
                            labs.IsActive = Convert.ToInt32(dtResult.Tables[2].Rows[i]["IsActive"]);
                            labs.DefaultImageUrl = Convert.ToString(dtResult.Tables[2].Rows[i]["DefaultImageUrl"]);

                            labs.LabType = Convert.ToString(dtResult.Tables[2].Rows[i]["LabType"]);                            
                            labs.ReadMoreUrl = Convert.ToString(dtResult.Tables[2].Rows[i]["ReadMoreUrl"]);
                            labs.Stopped = Convert.ToInt32(dtResult.Tables[2].Rows[i]["Stopped"]);

                            //labs.CustomerExperience = Convert.ToBoolean(dtResult.Tables[2].Rows[i]["CustomerExperience"] !=DBNull.Value ? (dtResult.Tables[2].Rows[i]["CustomerExperience"]) : 0);
                            //labs.CustomerExperience = Convert.ToBoolean(dtResult.Tables[2].Rows[i]["CustomerExperience"]);
                            //labs.Engineering = Convert.ToBoolean(dtResult.Tables[2].Rows[i]["Engineering"]);
                            //labs.Localization = Convert.ToBoolean(dtResult.Tables[2].Rows[i]["Localization"]);
                            //labs.QualityAssurance = Convert.ToBoolean(dtResult.Tables[2].Rows[i]["QualityAssurance"]);
                            //labs.Campaign = Convert.ToBoolean(dtResult.Tables[2].Rows[i]["Campaign"]);
                            //labs.AudioProduction = Convert.ToBoolean(dtResult.Tables[2].Rows[i]["AudioProduction"]);

                            LabsList.Add(labs);
                        }
                    }

                }
                return LabsList;
            }


            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                if (command != null) command.Dispose();
                command = null;

            }
        }

        public List<Labs> GetLabsArticleDetails(string LabIdOrShortDescription)
        {
            CustomCommand command = null;
            List<Labs> LabsList = new List<Labs>();
            //try
            //{
                using (command = new CustomCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Proc_RetrieveLabDetailsById";
                    command.AddParameterWithValue("p_LabIdOrShortDescription", LabIdOrShortDescription);
                    //  Execute command and get values from output parameters.
                    DataTable dtResult = ExecuteTable(command);
                  
                    if (dtResult != null && dtResult.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtResult.Rows.Count; i++)
                        {
                            Labs labs = new Labs();

                            labs.LabId = Convert.ToInt32(dtResult.Rows[i]["LabId"]);
                            labs.ServiceTypeId = Convert.ToInt32(dtResult.Rows[i]["ServiceTypeId"]);
                            //labs.LanguageCode = Convert.ToString(dtResult.Rows[i]["LanguageCode"]);
                            labs.Name = Convert.ToString(dtResult.Rows[i]["Name"]);
                            labs.ShortDescription = Convert.ToString(dtResult.Rows[i]["ShortDescription"]);
                            labs.Description = Convert.ToString(dtResult.Rows[i]["Description"]);
                            labs.ImageName = Convert.ToString(dtResult.Rows[i]["ImageName"]);
                            labs.ImageUrl = Convert.ToString(dtResult.Rows[i]["ImageUrl"]);
                            //labs.IsActive = Convert.ToInt32(dtResult.Rows[i]["IsActive"]);
                            //labs.OnNewWebsiteNow = Convert.ToBoolean(dtResult.Rows[i]["OnNewWebsiteNow"]);
                            labs.DesktopImageUrl = Convert.ToString(dtResult.Rows[i]["DesktopImageUrl"]);
                            labs.TabImageNameHorizondaUrl = Convert.ToString(dtResult.Rows[i]["TabImageNameHorizondaUrl"]);
                            labs.TabImageNamVerticalUrl = Convert.ToString(dtResult.Rows[i]["TabImageNamVerticalUrl"]);
                            labs.MobileImageNameUrl = Convert.ToString(dtResult.Rows[i]["MobileImageNameUrl"]);
                            labs.DefaultImageUrl = Convert.ToString(dtResult.Rows[i]["DefaultImageUrl"]);
                            labs.ImageAlternateText = Convert.ToString(dtResult.Rows[i]["ImageAlternateText"]);                      
                            labs.LabType = Convert.ToString(dtResult.Rows[i]["LabType"]);
                            // labs.MetaTags = Convert.ToString(dtResult.Rows[i]["MetaTags"]);
                            labs.DetailedName = Convert.ToString(dtResult.Rows[i]["DetailedName"]);
                            labs.DetailedDescription = Convert.ToString(dtResult.Rows[i]["DetailedDescription"]);
                            labs.DetailedShortOrder = Convert.ToString(dtResult.Rows[i]["DetailedShortOrder"]);

                            

                            LabsList.Add(labs);
                        }
                    }

                }
                return LabsList;
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            //finally
            //{
            //    if (command != null) command.Dispose();
            //    command = null;
            //}
        }

        public bool AddUpdateLabs(string xmNewsData, string Description)
        {
            CustomCommand command = null;
            NewsDetails newsDetails = new NewsDetails();
            bool result = false;
            try
            {
                using (command = new CustomCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "InsertLabs";
                    command.AddParameterWithValue("@LabsData", xmNewsData);
                    command.AddParameterWithValue("@_Description", Description);

                    int i = ExecuteNonQuery(command, false);
                    if (i > 0)
                    {
                        result = true;
                    }

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


        public List<Labs> GetLabCampaignArticleDetails(string LabIdOrShortDescription)
        {
            CustomCommand command = null;
            List<Labs> LabsList = new List<Labs>();
            try
            {
                using (command = new CustomCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Proc_RetrieveLabDetailsById";
                    command.AddParameterWithValue("@p_LabIdOrShortDescription", LabIdOrShortDescription);
                    //  Execute command and get values from output parameters.
                    DataTable dtResult = ExecuteTable(command);
                    if (dtResult != null && dtResult.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtResult.Rows.Count; i++)
                        {
                            Labs labs = new Labs();

                            labs.LabId = Convert.ToInt32(dtResult.Rows[i]["LabId"]);
                            labs.ServiceTypeId = Convert.ToInt32(dtResult.Rows[i]["ServiceTypeId"]);
                            //labs.LanguageCode = Convert.ToString(dtResult.Rows[i]["LanguageCode"]);
                            labs.Name = Convert.ToString(dtResult.Rows[i]["Name"]);
                            labs.ShortDescription = Convert.ToString(dtResult.Rows[i]["ShortDescription"]);
                            labs.Description = Convert.ToString(dtResult.Rows[i]["Description"]);
                            labs.ImageName = Convert.ToString(dtResult.Rows[i]["ImageName"]);
                            labs.ImageUrl = Convert.ToString(dtResult.Rows[i]["ImageUrl"]);
                            //labs.IsActive = Convert.ToInt32(dtResult.Rows[i]["IsActive"]);
                            //labs.OnNewWebsiteNow = Convert.ToBoolean(dtResult.Rows[i]["OnNewWebsiteNow"]);
                            labs.DesktopImageUrl = Convert.ToString(dtResult.Rows[i]["DesktopImageUrl"]);
                            labs.TabImageNameHorizondaUrl = Convert.ToString(dtResult.Rows[i]["TabImageNameHorizondaUrl"]);
                            labs.TabImageNamVerticalUrl = Convert.ToString(dtResult.Rows[i]["TabImageNamVerticalUrl"]);
                            labs.MobileImageNameUrl = Convert.ToString(dtResult.Rows[i]["MobileImageNameUrl"]);
                            labs.DefaultImageUrl = Convert.ToString(dtResult.Rows[i]["DefaultImageUrl"]);
                            labs.ImageAlternateText = Convert.ToString(dtResult.Rows[i]["ImageAlternateText"]);
                            labs.LabType = Convert.ToString(dtResult.Rows[i]["LabType"]);
                            // labs.MetaTags = Convert.ToString(dtResult.Rows[i]["MetaTags"]);
                            
                            labs.DetailedDescription = Convert.ToString(dtResult.Rows[i]["DetailedDescription"]);
                            labs.DetailedShortOrder = Convert.ToString(dtResult.Rows[i]["DetailedShortOrder"]);


                            LabsList.Add(labs);
                        }
                    }

                }
                return LabsList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (command != null) command.Dispose();
                command = null;
            }
        }

        public Labs GetAllLabsForUpdate()
        {
            CustomCommand command = null;
            Labs LabsList = new Labs();

            try
            {
                using (command = new CustomCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "GetAllLabsForUpdate";
                    //  Execute command and get values from output parameters.
                    DataTable dtResult = ExecuteTable(command);
                    if (dtResult != null && dtResult.Rows.Count > 0)
                    {
                        LabsList.LabListUpdate = new List<LabUrlList>();
                        for (int i = 0; i < dtResult.Rows.Count; i++)
                        {
                            LabUrlList labsUrlList = new LabUrlList();
                            labsUrlList.LabId = Convert.ToInt32(dtResult.Rows[i]["LabId"]);
                            labsUrlList.LabUrlTitle = Convert.ToString(dtResult.Rows[i]["ShortDescription"]);
                            labsUrlList.IsActive = Convert.ToInt32(dtResult.Rows[i]["IsActive"]);
                            LabsList.LabListUpdate.Add(labsUrlList);
                        }
                    }
                }
                return LabsList;
            }
            catch (Exception ex) { throw; }

            finally
            {
                if (command != null) command.Dispose();
                command = null;

            }
        }

        public bool UpdateLabs(int LabId, string xmNewsData, string Description, string LanguageCode)
        {
            CustomCommand command = null;
            NewsDetails newsDetails = new NewsDetails();
            bool result = false;
            try
            {
                using (command = new CustomCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "UpdateLabs";
                    command.AddParameterWithValue("@Lab_Id", LabId);
                    command.AddParameterWithValue("@LabsData", xmNewsData);
                    command.AddParameterWithValue("@_Description", Description);
                    command.AddParameterWithValue("@LanguageCode", LanguageCode);

                    int i = ExecuteNonQuery(command, false);
                    if (i > 0)
                    {
                        result = true;
                    }

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
        public Labs GetLabsDetailsByLabId(int LabId, string LanguageCode = "en-US")
        {
            CustomCommand command = null;
            Labs labs = new Labs();

            try
            {
                using (command = new CustomCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "GetLabsDetailsByLabId";
                    command.AddParameterWithValue("@Labs_Id", LabId);
                    command.AddParameterWithValue("@LanguageCode", LanguageCode);

                    //  Execute command and get values from output parameters.
                    DataSet dtResult = ExecuteDataSet(command);
                    if (dtResult != null && dtResult.Tables[dtResult.Tables.Count - 1].Rows.Count > 0)
                    {
                        labs.LabId = Convert.ToInt32(dtResult.Tables[dtResult.Tables.Count - 1].Rows[0]["LabId"]);
                        labs.EditLabId = Convert.ToString(dtResult.Tables[dtResult.Tables.Count - 1].Rows[0]["LabId"]);
                        labs.ServiceTypeId = Convert.ToInt32(dtResult.Tables[dtResult.Tables.Count - 1].Rows[0]["ServiceTypeId"]);
                        labs.Name = Convert.ToString(dtResult.Tables[dtResult.Tables.Count - 1].Rows[0]["LabTitle"]);                     
                       
                        labs.ShortDescription = Convert.ToString(dtResult.Tables[dtResult.Tables.Count - 1].Rows[0]["ShortDescription"]);                       
                        labs.Description = Convert.ToString(dtResult.Tables[dtResult.Tables.Count - 1].Rows[0]["Description"]);
                        labs.Topic = Convert.ToString(dtResult.Tables[dtResult.Tables.Count - 1].Rows[0]["Topic"]);
                        labs.StartDate = Convert.ToString(dtResult.Tables[dtResult.Tables.Count - 1].Rows[0]["StartDate"]);
                        labs.EndDate = Convert.ToString(dtResult.Tables[dtResult.Tables.Count - 1].Rows[0]["EndDate"]);
                        labs.OnNewWebsiteNow = Convert.ToBoolean(dtResult.Tables[dtResult.Tables.Count - 1].Rows[0]["OnNewWebsiteNow"]);
                       // labs.SuitedForHomePage = Convert.ToBoolean(dtResult.Tables[dtResult.Tables.Count - 1].Rows[0]["SuitedForHomePage"]);
                        labs.CustomerExperience = Convert.ToBoolean(dtResult.Tables[dtResult.Tables.Count - 1].Rows[0]["CustomerExperience"]);
                        labs.QualityAssurance = Convert.ToBoolean(dtResult.Tables[dtResult.Tables.Count - 1].Rows[0]["QualityAssurance"]);
                        labs.Localization = Convert.ToBoolean(dtResult.Tables[dtResult.Tables.Count - 1].Rows[0]["Localization"]);
                        labs.AudioProduction = Convert.ToBoolean(dtResult.Tables[dtResult.Tables.Count - 1].Rows[0]["AudioProduction"]);
                        labs.Engineering = Convert.ToBoolean(dtResult.Tables[dtResult.Tables.Count - 1].Rows[0]["Engineering"]);
                        labs.ActiveStatus = Convert.ToBoolean(dtResult.Tables[dtResult.Tables.Count - 1].Rows[0]["IsActive"]);
                        labs.DesktopImageUrl = Convert.ToString(dtResult.Tables[dtResult.Tables.Count - 1].Rows[0]["DesktopImageUrl"]);
                        labs.TabImageNameHorizondaUrl = Convert.ToString(dtResult.Tables[dtResult.Tables.Count - 1].Rows[0]["TabImageNameHorizondaUrl"]);
                        labs.TabImageNamVerticalUrl = Convert.ToString(dtResult.Tables[dtResult.Tables.Count - 1].Rows[0]["TabImageNamVerticalUrl"]);
                       // labs.DefaultImageUrl = Convert.ToString(dtResult.Tables[dtResult.Tables.Count - 1].Rows[0]["DefaultImageUrl"]);
                        labs.MobileImageNameUrl = Convert.ToString(dtResult.Tables[dtResult.Tables.Count - 1].Rows[0]["MobileImageNameUrl"]);
                        labs.MetaTitle = Convert.ToString(dtResult.Tables[dtResult.Tables.Count - 1].Rows[0]["MetaTitle"]);
                        //labs.MetaDescription = Convert.ToString(dtResult.Tables[dtResult.Tables.Count - 1].Rows[0]["MetaDescription"]);
                        //labs.ShortOrder = Convert.ToInt32(dtResult.Tables[dtResult.Tables.Count - 1].Rows[0]["ShortOrder"]);

                    }

                }
                return labs;
            }


            catch (Exception ex) { throw; }

            finally
            {
                if (command != null) command.Dispose();
                command = null;

            }
        }
    }
}
