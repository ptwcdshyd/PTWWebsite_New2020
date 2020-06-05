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


    }
}
