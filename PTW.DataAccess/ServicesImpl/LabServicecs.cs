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
                            //labs.CustomerExperience = Convert.ToBoolean(dtResult.Tables[2].Rows[i]["CustomerExperience"]);
                            //labs.Engineering = Convert.ToBoolean(dtResult.Tables[2].Rows[i]["Engineering"]);
                            //labs.Localization = Convert.ToBoolean(dtResult.Tables[2].Rows[i]["Localization"]);
                            //labs.QualityAssurance = Convert.ToBoolean(dtResult.Tables[2].Rows[i]["QualityAssurance"]);
                            //labs.Campaign = Convert.ToBoolean(dtResult.Tables[2].Rows[i]["Campaign"]);
                            labs.ReadMoreUrl = Convert.ToString(dtResult.Tables[2].Rows[i]["ReadMoreUrl"]);
                            labs.Stopped = Convert.ToInt32(dtResult.Tables[2].Rows[i]["Stopped"]);

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
            try
            {
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

    }
}
