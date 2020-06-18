using System;
using System.Collections.Generic;
using System.Text;

namespace PTW.DataAccess.Models
{
    public class NewsDetails
    {
        public int NewsId { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public int Event { get; set; }
        public string ShortImageName { get; set; }
        public string ShortImageUrl { get; set; }
        public string HeaderImageName { get; set; }
        public string HeaderImageUrl { get; set; }
        public string LongImageName { get; set; }
        public string LongImageUrl { get; set; }
        public string UrlTitle { get; set; }
        public string NewsTitle { get; set; }

        public string PublishedDate { get; set; }

        public int ShortOrder { get; set; }

        public int ModuleId { get; set; }
        public int LanguageId { get; set; }

        public int IsActive { get; set; }

        public string MetaTags { get; set; }
        public string NextNewsUrl { get; set; }
        public string PreviousNewsUrl { get; set; }
        public string LinkedInId { get; set; }
        public string Reason { get; set; }

        public string EventDate { get; set; }
        public string Location { get; set; }

    }
}
