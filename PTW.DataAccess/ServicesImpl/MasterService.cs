using PTW.DataAccess.Models;
using PTW.DataAccess.Services;
using PTW.DBAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace PTW.DataAccess.ServicesImpl
{
    public sealed partial class MasterService : DBDataAccess, IMasterService
    {
        public MasterPage GetDashboardDetails(int LoginUserId,int LanguageID, int ModuleId)
        {
            CustomCommand command = null;
            MasterPage masterPage = new MasterPage();

            try
            {
                using (command = new CustomCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "GetLandingPageDetails";
                    command.AddParameterWithValue("@L_userId", LoginUserId);
                    command.AddParameterWithValue("@LanguageID", LanguageID);
                    command.AddParameterWithValue("@ModuleID", ModuleId);
                    //  Execute command and get values from output parameters.
                    DataSet dtResult = ExecuteDataSet(command);
                    if (dtResult != null && dtResult.Tables[0].Rows.Count > 0)
                    {
                        masterPage.HeaderContent = Convert.ToString(dtResult.Tables[0].Rows[0]["ModuleName"]);
                        masterPage.HtmlContent = Convert.ToString(dtResult.Tables[0].Rows[0]["Content"]);
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

        public string UpdateContentByModelIdAndLanguageId(int moduleId, int languageId, string contentText)
        {
            string message;
            CustomCommand command = null;
            // MasterPage masterPage = new MasterPage();

            try
            {
                using (command = new CustomCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "UpdateContentByModuleIdAndLanguageId";
                    command.AddParameterWithValue("@Module_Id", moduleId);
                    command.AddParameterWithValue("@Language_ID", languageId);
                    command.AddParameterWithValue("@ContentText", contentText);
                    //  Execute command and get values from output parameters.
                    int result = ExecuteNonQuery(command, false);
                    if (result != 0)
                    {
                        message = "successfully updated"; 
                    }
                    else
                    {
                        message = "not updated successfully";
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
                            if (i==0)
                            {
                                Languages lang = new Languages();
                                lang.LanguageId = 0;
                                lang.Language = "Languages";
                                masterPage.LanguageList.Add(lang);
                            }
                            Languages languages = new Languages();
                            languages.LanguageId= Convert.ToInt32(dtResult.Tables[1].Rows[i]["LanguageId"]);
                            languages.Language = dtResult.Tables[1].Rows[i]["LanguageName"].ToString();
                            languages.LanguageCode= dtResult.Tables[1].Rows[i]["Code"].ToString();
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

    }
}
