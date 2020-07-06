using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;
using PTW.DataAccess.Models;
using PTW.DataAccess.Services;
using PTW.DBAccess;
using System;
using System.Collections.Generic;
using System.Text;
using System.Buffers.Text;
using System.Data;
using System.Linq;




namespace PTW.DataAccess.ServicesImpl
{

    public sealed partial class AboutServices : DBDataAccess, IAboutServices
    {

        public List<AboutModel> GetAboutProfile(string Languagecode)
        {
            CustomCommand command = null;
            DataTable dt = new DataTable();
            List<AboutModel> listabout = new List<AboutModel>();

            try
            {
                using (command = new CustomCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "GetAboutProfilebyLangCode";

                    command.AddParameterWithValue("@Languagecode", Languagecode);
                    //  Execute command and get values from output parameters.
                    dt = ExecuteTable(command);

                   
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            AboutModel objAbout = new AboutModel();
                            objAbout.ProfileTitle = dt.Rows[i]["Title"].ToString();
                            objAbout.Description = dt.Rows[i]["Description"].ToString();
                            objAbout.ImgPath = dt.Rows[i]["ImgPath"].ToString();
                            objAbout.OrderNo = Convert.ToInt32(dt.Rows[i]["OrderNo"]);
                            listabout.Add(objAbout);
                        }
                    }

                }
                return listabout;
            }


            catch { throw; }

            finally
            {
                if (command != null) command.Dispose();
                command = null;

            }
        }
        public List<AboutModel> GetProfile()
        {
            CustomCommand command = null;
            DataTable dt = new DataTable();
            List<AboutModel> listabout = new List<AboutModel>();

            try
            {
                using (command = new CustomCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "GetAboutProfile";


                    //  Execute command and get values from output parameters.
                    dt = ExecuteTable(command);

                
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            AboutModel objAbout = new AboutModel();
                            objAbout.ProfileId = Convert.ToInt32(dt.Rows[i]["ProfileId"]);
                            objAbout.ProfileTitle = dt.Rows[i]["Title"].ToString();
                            objAbout.Description = dt.Rows[i]["Description"].ToString();
                            objAbout.ImgPath = dt.Rows[i]["ImgPath"].ToString();
                            objAbout.OrderNo = Convert.ToInt32(dt.Rows[i]["OrderNo"]);
                            objAbout.IsActive = Convert.ToBoolean(dt.Rows[i]["IsActive"]);
                            listabout.Add(objAbout);
                        }
                    }
                }
                return listabout;
            }


            catch { throw; }

            finally
            {
                if (command != null) command.Dispose();
                command = null;

            }
        }



        public List<Language> GetLanguages()
        {
            CustomCommand command = null;
            DataTable dt = new DataTable();
            List<Language> Languagelist = new List<Language>();
            try
            {
                using (command = new CustomCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "RetrieveLanguages";

                    //  Execute command and get values from output parameters.
                    dt = ExecuteTable(command);

                    if (dt != null && dt.Rows.Count > 0)
                    {

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            Language languages = new Language();
                            languages.LanguageId = Convert.ToInt32(dt.Rows[i]["LanguageId"]);
                            languages.LanguageName = dt.Rows[i]["Name"].ToString();
                            languages.LanguageCode = dt.Rows[i]["ShortForm"].ToString();
                            Languagelist.Add(languages);
                        }
                    }


                    }
                return Languagelist;
            }


            catch { throw; }

            finally
            {
                if (command != null) command.Dispose();
                command = null;

            }
        }



        public AboutModel GetProfileByProfileId(int ProfileId, string LanguageCode)
        {

            CustomCommand command = null;
            DataTable dt = new DataTable();
            AboutModel objAbout = new AboutModel();

            try
            {
                using (command = new CustomCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.AddParameterWithValue("@AboutProfileId", ProfileId);
                    command.AddParameterWithValue("@Languagecode", LanguageCode);
                    command.CommandText = "GetAboutProfileByProfileId";


                    //  Execute command and get values from output parameters.
                    dt = ExecuteTable(command);


                    if (dt != null && dt.Rows.Count >0)
                    {
                            objAbout.ProfileId = Convert.ToInt32(dt.Rows[0]["ProfileId"]);
                            objAbout.ProfileTitle = dt.Rows[0]["Title"].ToString();
                            objAbout.Description = dt.Rows[0]["Description"].ToString();
                            objAbout.ImgPath = dt.Rows[0]["ImgPath"].ToString();
                            objAbout.OrderNo = Convert.ToInt32(dt.Rows[0]["OrderNo"]);
                            objAbout.IsActive = Convert.ToBoolean(dt.Rows[0]["IsActive"]);

                    }
                }
                return objAbout;
            }


            catch { throw; }

            finally
            {
                if (command != null) command.Dispose();
                command = null;

            }

        }



        public bool AddProfileByProfileId(AboutModel objabout)
        {
            CustomCommand command = null;
           
            bool result = false;
            try
            {
                using (command = new CustomCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "InsertAboutProfile";
                   
                    //command.AddParameterWithValue("@p_ProfileId", objabout.ProfileId);
                    command.AddParameterWithValue("@p_ProfileTitle", objabout.ProfileTitle);
                    command.AddParameterWithValue("@p_ImgPath", objabout.ImgPath);
                    command.AddParameterWithValue("@p_IsActive", objabout.IsActive);
                    
                    command.AddParameterWithValue("@p_OrderNo", objabout.OrderNo);
                    command.AddParameterWithValue("@p_Culture", objabout.Culture);
                    command.AddParameterWithValue("@p_Description", objabout.Description);

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

        public bool UpdateProfileByProfileId(AboutModel objabout)
        {
            CustomCommand command = null;

            bool result = false;
            try
            {
                using (command = new CustomCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "UpdateProfileByProfileId";

                    command.AddParameterWithValue("@p_ProfileId", objabout.ProfileId);
                    command.AddParameterWithValue("@p_OrderNo", objabout.OrderNo);
                    command.AddParameterWithValue("@p_Culture", objabout.Culture);
                    command.AddParameterWithValue("@p_IsActive", objabout.IsActive);
                    command.AddParameterWithValue("@p_ImgPath", objabout.ImgPath);
                    command.AddParameterWithValue("@p_ProfileTitle", objabout.ProfileTitle);
                    command.AddParameterWithValue("@p_Description", objabout.Description);

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


    }
}
