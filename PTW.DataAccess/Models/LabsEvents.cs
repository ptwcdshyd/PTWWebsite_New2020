using System;
using System.Collections.Generic;
using System.Text;

namespace PTW.DataAccess.Models
{
    public class LabsEvents
    {
        public List<Labs> Labs { get; set; }

        public List<Labs> labInsights { get; set; }

        public List<Labs> allLabs { get; set; }

        public List<Labs> FutureLabArticles { get; set; }

        public List<Labs> LabArticledetails { get; set; }
        public string HtmlContent { get; set; }
    }
}
