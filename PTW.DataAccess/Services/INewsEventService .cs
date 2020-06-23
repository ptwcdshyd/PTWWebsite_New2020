using Microsoft.AspNetCore.Http;
using PTW.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PTW.DataAccess.Services
{
    public interface INewsEventService
    {
        List<News> GetAllNewsDetails();
        List<News> GetAllEventDetails();
        NewsDetails GetNewsDetailsByTitle(string NewsUrlTitle, string LanguageCode);
        bool AddUpdateNews(string xmNewsData, string Description);
        News GetAllNewsAndEventDetailsForUpdate();

        News GetNewsAndEventDetailsByNewsId(int NewsId, string LanguageCode);

        bool UpdateNews(int NewsId, string xmNewsData, string Description, string LanguageCode);


    }
}
