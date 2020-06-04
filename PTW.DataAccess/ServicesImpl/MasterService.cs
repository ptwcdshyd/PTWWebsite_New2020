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
        public MasterPage GetDashboardDetails(int LoginUserId,int LanguageID)
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
                    command.AddParameterWithValue("@ModuleID", 1);
                    //  Execute command and get values from output parameters.
                    DataSet dtResult = ExecuteDataSet(command);
                    if (dtResult != null && dtResult.Tables[0].Rows.Count > 0)
                    {
                        masterPage.HeaderContent = Convert.ToString(dtResult.Tables[0].Rows[0]["HeaderContent"]);
                        masterPage.FooterContent = Convert.ToString(dtResult.Tables[0].Rows[0]["FooterContent"]);
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
