using Microsoft.AspNetCore.Http;
using PTW.DataAccess.Models;
using PTW.DataAccess.Services;
using PTW.DBAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace PTW.DataAccess.ServicesImpl
{
    public class NewsEventService : DBDataAccess, INewsEventService
    {
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
                            news.NewsTitle = Convert.ToString(dtResult.Rows[i]["NewsTitle"]);
                            news.UrlTitle = Convert.ToString(dtResult.Rows[i]["UrlTitle"]);
                            news.Description = Convert.ToString(dtResult.Rows[i]["Description"]);
                            news.HeaderImageName = Convert.ToString(dtResult.Rows[i]["HeaderImageName"]);
                            news.HeaderImageUrl = Convert.ToString(dtResult.Rows[i]["HeaderImageUrl"]);
                            news.LongImageName = Convert.ToString(dtResult.Rows[i]["LongImageName"]);
                            news.LongImageUrl = Convert.ToString(dtResult.Rows[i]["LongImageUrl"]);
                            news.ShortImageName = Convert.ToString(dtResult.Rows[i]["ShortImageName"]);
                            news.ShortImageUrl = Convert.ToString(dtResult.Rows[i]["ShortImageUrl"]);
                            news.IsActive = Convert.ToInt32(dtResult.Rows[i]["IsActive"]);
                            news.OnNewWebsiteNow = Convert.ToInt32(dtResult.Rows[i]["OnNewWebsiteNow"]);
                            news.Event = Convert.ToInt32(dtResult.Rows[i]["Event"]);
                            news.ShortOrder = Convert.ToInt32(dtResult.Rows[i]["ShortOrder"]);
                            news.ModuleId = Convert.ToInt32(dtResult.Rows[i]["ModuleId"]);
                            news.FBUrl = Convert.ToString(dtResult.Rows[i]["FBUrl"]);
                            news.TwitterId = Convert.ToString(dtResult.Rows[i]["TwitterId"]);
                            news.LinkedInId = Convert.ToString(dtResult.Rows[i]["LinkedInId"]);
                            news.Reason = Convert.ToString(dtResult.Rows[i]["Reason"]);
                            news.MetaTags = Convert.ToString(dtResult.Rows[i]["MetaTags"]);

                            newsList.Add(news);
                        }
                    }

                }
                return newsList;
            }


            catch { throw; }

            finally
            {
                if (command != null) command.Dispose();
                command = null;

            }
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
                return newsList;
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
