using PTW.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PTW.DataAccess.Services
{
    public interface ILabService
    {
        List<Labs> GetAllLabsDetails(string LanguageCode);

        List<Labs> GetAllLatestInsights(string LanguageCode);

        List<Labs> GetSlider();

        List<Labs> GetFutureLabArticles(string LabIdOrShortDescription);

        List<Labs> GetLabsArticleDetails(string LabIdOrShortDescription);

        bool AddUpdateLabs(string xmNewsData, string Description);

      
        List<Labs> GetLabCampaignArticleDetails(string LabIdOrShortDescription);
    }
}
