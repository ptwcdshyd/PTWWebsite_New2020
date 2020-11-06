using LoggerService;
using Microsoft.AspNetCore.Http;
using PTW.DataAccess.Models;
using PTW.DataAccess.Services;
using PTW.DBAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;

namespace PTW.DataAccess.ServicesImpl
{
    public class NewsEventService : DBDataAccess, INewsEventService
    {
        private readonly ILoggerManager _loggerManager;
        public NewsEventService(ILoggerManager loggerManager)
        {
            _loggerManager = loggerManager;
        }
        public List<News> GetAllNewsDetails()
        {
            CustomCommand command = null;
            List<News> newsList = new List<News>();

            try
            {
                using (command = new CustomCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "RetrieveAllNews";
                    //  Execute command and get values from output parameters.
                    DataTable dtResult = ExecuteTable(command);
                    if (dtResult != null && dtResult.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtResult.Rows.Count; i++)
                        {
                            News news = new News();

                            news.NewsId = Convert.ToInt32(dtResult.Rows[i]["NewsId"]);
                            news.PublishedDate = Convert.ToString(dtResult.Rows[i]["PublishedDate"]);
                            news.LanguageCode = Convert.ToString(dtResult.Rows[i]["LanguageCode"]);
                            news.NewsTitle = Convert.ToString(dtResult.Rows[i]["NewsTitle"]);
                            news.UrlTitle = Convert.ToString(dtResult.Rows[i]["UrlTitle"]);
                            news.ShortDescription = Convert.ToString(dtResult.Rows[i]["ShortDescription"]);
                            news.Description = Convert.ToString(dtResult.Rows[i]["Description"]);
                            news.HeaderImageName = Convert.ToString(dtResult.Rows[i]["HeaderImageName"]);
                            news.HeaderImageUrl = Convert.ToString(dtResult.Rows[i]["HeaderImageUrl"]);
                            news.LongImageName = Convert.ToString(dtResult.Rows[i]["LongImageName"]);
                            news.LongImageUrl = Convert.ToString(dtResult.Rows[i]["LongImageUrl"]);
                            news.ShortImageName = Convert.ToString(dtResult.Rows[i]["ShortImageName"]);
                            news.ShortImageUrl = Convert.ToString(dtResult.Rows[i]["ShortImageUrl"]);
                            news.IsActive = Convert.ToInt32(dtResult.Rows[i]["IsActive"]);
                            news.OnNewWebsiteNow = Convert.ToBoolean(dtResult.Rows[i]["OnNewWebsiteNow"]);
                            news.Event = Convert.ToInt32(dtResult.Rows[i]["Event"]);
                            news.ShortOrder = Convert.ToInt32(dtResult.Rows[i]["ShortOrder"]);
                            //news.ModuleId = Convert.ToInt32(dtResult.Rows[i]["ModuleId"]);
                            news.FBUrl = Convert.ToString(dtResult.Rows[i]["FBUrl"]);
                            news.TwitterId = Convert.ToString(dtResult.Rows[i]["TwitterId"]);
                            news.LinkedInId = Convert.ToString(dtResult.Rows[i]["LinkedInId"]);
                            news.Reason = Convert.ToString(dtResult.Rows[i]["Reason"]);
                            news.MetaTags = Convert.ToString(dtResult.Rows[i]["MetaTags"]);

                            newsList.Add(news);
                        }
                    }

                }
                
            }


            catch { throw; }

            finally
            {
                if (command != null) command.Dispose();
                command = null;
            }
            return newsList;
        }

        public List<News> GetAllEventDetails()
        {
            CustomCommand command = null;
            List<News> newsList = new List<News>();

            try
            {
                using (command = new CustomCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "RetrieveAllEvents";
                    //  Execute command and get values from output parameters.
                    DataTable dtResult = ExecuteTable(command);
                    if (dtResult != null && dtResult.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtResult.Rows.Count; i++)
                        {
                            News news = new News();

                            news.NewsId = Convert.ToInt32(dtResult.Rows[i]["NewsId"]);
                            news.PublishedDate = Convert.ToString(dtResult.Rows[i]["PublishedDate"]);
                            news.NewsTitle = Convert.ToString(dtResult.Rows[i]["NewsTitle"]);
                            news.UrlTitle = Convert.ToString(dtResult.Rows[i]["UrlTitle"]);
                            news.ShortDescription = Convert.ToString(dtResult.Rows[i]["ShortDescription"]);
                            news.Description = Convert.ToString(dtResult.Rows[i]["Description"]);
                            news.HeaderImageName = Convert.ToString(dtResult.Rows[i]["HeaderImageName"]);
                            news.HeaderImageUrl = Convert.ToString(dtResult.Rows[i]["HeaderImageUrl"]);
                            news.LongImageName = Convert.ToString(dtResult.Rows[i]["LongImageName"]);
                            news.LongImageUrl = Convert.ToString(dtResult.Rows[i]["LongImageUrl"]);
                            news.ShortImageName = Convert.ToString(dtResult.Rows[i]["ShortImageName"]);
                            news.ShortImageUrl = Convert.ToString(dtResult.Rows[i]["ShortImageUrl"]);
                            news.IsActive = Convert.ToInt32(dtResult.Rows[i]["IsActive"]);
                            //news.OnNewWebsiteNow = Convert.ToInt32(dtResult.Rows[i]["OnNewWebsiteNow"]);
                            news.Event = Convert.ToInt32(dtResult.Rows[i]["Event"]);
                            news.ShortOrder = Convert.ToInt32(dtResult.Rows[i]["ShortOrder"]);
                            //news.ModuleId = Convert.ToInt32(dtResult.Rows[i]["ModuleId"]);
                            news.FBUrl = Convert.ToString(dtResult.Rows[i]["FBUrl"]);
                            news.TwitterId = Convert.ToString(dtResult.Rows[i]["TwitterId"]);
                            news.LinkedInId = Convert.ToString(dtResult.Rows[i]["LinkedInId"]);
                            news.Reason = Convert.ToString(dtResult.Rows[i]["Reason"]);
                            news.MetaTags = Convert.ToString(dtResult.Rows[i]["MetaTags"]);
                            news.EventDate = Convert.ToString(dtResult.Rows[i]["EventDate"]);
                            news.Location = Convert.ToString(dtResult.Rows[i]["Location"]);

                            newsList.Add(news);
                        }
                    }

                }
                
            }


            catch { throw; }

            finally
            {
                if (command != null) command.Dispose();
                command = null;
            }
            return newsList;
        }

        public NewsDetails GetNewsDetailsByTitle(string NewsUrlTitle, string LanguageCode)
        {
            CustomCommand command = null;
            NewsDetails newsDetails = new NewsDetails();
            try
            {
                using (command = new CustomCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "RetrieveNewsDetailsByTitle";
                    command.AddParameterWithValue("@NewsTitle", NewsUrlTitle);
                    command.AddParameterWithValue("@LanguageCode", LanguageCode);

                    //  Execute command and get values from output parameters.
                    DataSet dtResult = ExecuteDataSet(command);


                    if (dtResult != null)
                    {

                        if (dtResult.Tables[dtResult.Tables.Count - 1] != null && dtResult.Tables[dtResult.Tables.Count - 1].Rows.Count > 0)
                        {
                            newsDetails.Type = Convert.ToString(dtResult.Tables[dtResult.Tables.Count - 1].Rows[0]["Type"]);
                            newsDetails.NewsId = Convert.ToInt32(dtResult.Tables[dtResult.Tables.Count - 1].Rows[0]["NewsId"]);
                            newsDetails.PublishedDate = Convert.ToString(dtResult.Tables[dtResult.Tables.Count - 1].Rows[0]["PublishedDate"]);
                            newsDetails.NewsTitle = Convert.ToString(dtResult.Tables[dtResult.Tables.Count - 1].Rows[0]["NewsTitle"]);
                            newsDetails.Description = Convert.ToString(dtResult.Tables[dtResult.Tables.Count - 1].Rows[0]["Description"]);
                            newsDetails.HeaderImageName = Convert.ToString(dtResult.Tables[dtResult.Tables.Count - 1].Rows[0]["HeaderImageName"]);
                            newsDetails.HeaderImageUrl = Convert.ToString(dtResult.Tables[dtResult.Tables.Count - 1].Rows[0]["HeaderImageUrl"]);
                            newsDetails.IsActive = Convert.ToInt32(dtResult.Tables[dtResult.Tables.Count - 1].Rows[0]["IsActive"]);
                            newsDetails.ShortDescription = Convert.ToString(dtResult.Tables[dtResult.Tables.Count - 1].Rows[0]["ShortDescription"]);
                            newsDetails.MetaTags = Convert.ToString(dtResult.Tables[dtResult.Tables.Count - 1].Rows[0]["MetaTags"]);
                            newsDetails.PreviousNewsUrl = Convert.ToString(dtResult.Tables[dtResult.Tables.Count - 1].Rows[0]["PreviousNewsTitle"]).Replace("<br>", " ").Replace("<br/>", " ");
                            newsDetails.NextNewsUrl = Convert.ToString(dtResult.Tables[dtResult.Tables.Count - 1].Rows[0]["NextNewsTitle"]).Replace("<br>", " ").Replace("<br/>", " ");

                        }
                    }

                }
                
            }


            catch { throw; }

            finally
            {
                if (command != null) command.Dispose();
                command = null;
            }
            return newsDetails;
        }

        public bool AddUpdateNews(string xmNewsData, string Description)
        {
            CustomCommand command = null;
            NewsDetails newsDetails = new NewsDetails();
            bool result = false;
            try
            {
                using (command = new CustomCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "InsertNews";
                    command.AddParameterWithValue("@NewsData", xmNewsData);
                    command.AddParameterWithValue("@_Description", Description);

                    int i = ExecuteNonQuery(command, false);
                    if (i > 0)
                    {
                        result = true;
                    }

                }
                
            }


            catch { throw; }

            finally
            {
                if (command != null) command.Dispose();
                command = null;
            }
            return result;
        }

        public bool UpdateNews(int NewsId, string xmNewsData, string Description, string LanguageCode)
        {
            CustomCommand command = null;
            NewsDetails newsDetails = new NewsDetails();
            bool result = false;
            try
            {
                using (command = new CustomCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "UpdateNews";
                    command.AddParameterWithValue("@News_Id", NewsId);
                    command.AddParameterWithValue("@NewsData", xmNewsData);
                    command.AddParameterWithValue("@_Description", Description);
                    command.AddParameterWithValue("@LanguageCode", LanguageCode);

                    int i = ExecuteNonQuery(command, false);
                    if (i > 0)
                    {
                        result = true;
                    }

                }
                
            }


            catch { throw; }

            finally
            {
                if (command != null) command.Dispose();
                command = null;
            }
            return result;
        }

        public News GetAllNewsAndEventDetailsForUpdate()
        {
            CustomCommand command = null;
            News newsList = new News();

            try
            {
                using (command = new CustomCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "GetAllNewsAndEventsForUpdate";
                    //  Execute command and get values from output parameters.
                    DataTable dtResult = ExecuteTable(command);
                    if (dtResult != null && dtResult.Rows.Count > 0)
                    {
                        newsList.NewsListUpdate = new List<NewsUrlList>();
                        for (int i = 0; i < dtResult.Rows.Count; i++)
                        {
                          
                            NewsUrlList newsUrlList = new NewsUrlList();

                            newsUrlList.NewsId = Convert.ToInt32(dtResult.Rows[i]["NewsId"]);
                            
                            newsUrlList.NewsUrlTitle = Convert.ToString(dtResult.Rows[i]["ShortDescription"]);

                            newsUrlList.IsActive = Convert.ToInt32(dtResult.Rows[i]["IsActive"]);


                            newsList.NewsListUpdate.Add(newsUrlList);
                        }
                    }

                }
                
            }


            catch(Exception ex) { throw; }

            finally
            {
                if (command != null) command.Dispose();
                command = null;
            }
            return newsList;
        }

        public News GetNewsAndEventDetailsByNewsId(int NewsId, string LanguageCode)
        {
            CustomCommand command = null;
            News news = new News();

            try
            {
                using (command = new CustomCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "GetNewsAndEventDetailsByNewsId";
                    command.AddParameterWithValue("@News_Id", NewsId);
                    command.AddParameterWithValue("@LanguageCode", LanguageCode);

                    //  Execute command and get values from output parameters.
                    DataSet dtResult = ExecuteDataSet(command);
                    if (dtResult != null && dtResult.Tables[dtResult.Tables.Count-1].Rows.Count > 0)
                    {
                        news.NewsId = Convert.ToInt32(dtResult.Tables[dtResult.Tables.Count - 1].Rows[0]["NewsId"]);
                        news.EditNewsId= Convert.ToInt32(dtResult.Tables[dtResult.Tables.Count - 1].Rows[0]["NewsId"]);
                        news.Event = Convert.ToInt32(dtResult.Tables[dtResult.Tables.Count - 1].Rows[0]["EventType"]);
                        news.PublishedDate = Convert.ToString(dtResult.Tables[dtResult.Tables.Count - 1].Rows[0]["PublishedDate"]);
                       // news.PublishedDate2 = Convert.ToDateTime(dtResult.Rows[0]["PublishedDate"]);
                        news.NewsTitle = Convert.ToString(dtResult.Tables[dtResult.Tables.Count - 1].Rows[0]["NewsTitle"]);
                        news.ShortDescription = Convert.ToString(dtResult.Tables[dtResult.Tables.Count - 1].Rows[0]["ShortDescription"]);
                        news.UrlTitle = Convert.ToString(dtResult.Tables[dtResult.Tables.Count - 1].Rows[0]["ShortDescription"]);
                        news.Description = Convert.ToString(dtResult.Tables[dtResult.Tables.Count - 1].Rows[0]["Description"]);
                        news.EventStartDate = Convert.ToString(dtResult.Tables[dtResult.Tables.Count - 1].Rows[0]["EventStartDate"]);
                        news.EventEndDate = Convert.ToString(dtResult.Tables[dtResult.Tables.Count - 1].Rows[0]["EventEndDate"]);
                        news.OnNewWebsiteNow = Convert.ToBoolean(dtResult.Tables[dtResult.Tables.Count - 1].Rows[0]["OnNewWebsiteNow"]);
                        news.SuitedForHomePage = Convert.ToBoolean(dtResult.Tables[dtResult.Tables.Count - 1].Rows[0]["SuitedForHomePage"]);
                        news.CustomerExperience = Convert.ToBoolean(dtResult.Tables[dtResult.Tables.Count - 1].Rows[0]["CustomerExperience"]);
                        news.QualityAssurance = Convert.ToBoolean(dtResult.Tables[dtResult.Tables.Count - 1].Rows[0]["QualityAssurance"]);
                        news.Localization = Convert.ToBoolean(dtResult.Tables[dtResult.Tables.Count - 1].Rows[0]["Localization"]);
                        news.AudioProduction = Convert.ToBoolean(dtResult.Tables[dtResult.Tables.Count - 1].Rows[0]["AudioProduction"]);
                        news.Engineering = Convert.ToBoolean(dtResult.Tables[dtResult.Tables.Count - 1].Rows[0]["Engineering"]);
                        news.ActiveStatus = Convert.ToBoolean(dtResult.Tables[dtResult.Tables.Count - 1].Rows[0]["IsActive"]);
                        news.HeaderImageName = Convert.ToString(dtResult.Tables[dtResult.Tables.Count - 1].Rows[0]["HeaderImageName"]);
                        news.HeaderImageUrl = Convert.ToString(dtResult.Tables[dtResult.Tables.Count - 1].Rows[0]["HeaderImageUrl"]);
                        news.LongImageName = Convert.ToString(dtResult.Tables[dtResult.Tables.Count - 1].Rows[0]["LongImageName"]);
                        news.LongImageUrl = Convert.ToString(dtResult.Tables[dtResult.Tables.Count - 1].Rows[0]["LongImageUrl"]);
                        news.ShortImageName = Convert.ToString(dtResult.Tables[dtResult.Tables.Count - 1].Rows[0]["ShortImageName"]);
                        news.ShortImageUrl = Convert.ToString(dtResult.Tables[dtResult.Tables.Count - 1].Rows[0]["ShortImageUrl"]);
                        news.MetaTitle = Convert.ToString(dtResult.Tables[dtResult.Tables.Count - 1].Rows[0]["MetaTitle"]);
                        news.MetaDescription = Convert.ToString(dtResult.Tables[dtResult.Tables.Count - 1].Rows[0]["MetaDescription"]);
                        news.ShortOrder = Convert.ToInt32(dtResult.Tables[dtResult.Tables.Count - 1].Rows[0]["ShortOrder"]);
                        news.ProductDevelopment = Convert.ToBoolean(dtResult.Tables[dtResult.Tables.Count - 1].Rows[0]["ProductDevelopment"]);
                        news.TalentSolution = Convert.ToBoolean(dtResult.Tables[dtResult.Tables.Count - 1].Rows[0]["TalentSolution"]);
                        news.PlayerSupport = Convert.ToBoolean(dtResult.Tables[dtResult.Tables.Count - 1].Rows[0]["PlayerSupport"]);
                        news.SpeechTech = Convert.ToBoolean(dtResult.Tables[dtResult.Tables.Count - 1].Rows[0]["SpeechTech"]);
                        news.LocalizationQA = Convert.ToBoolean(dtResult.Tables[dtResult.Tables.Count - 1].Rows[0]["LocalizationQA"]);
                        news.Topic = Convert.ToString(dtResult.Tables[dtResult.Tables.Count - 1].Rows[0]["Topic"]);

                    }

                }
                
            }


            catch (Exception ex) { throw; }

            finally
            {
                if (command != null) command.Dispose();
                command = null;
            }
            return news;
        }
    }
}
