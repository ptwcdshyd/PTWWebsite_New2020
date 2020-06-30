using System;
using System.Collections.Generic;
using System.Text;

namespace PTW.DataAccess.Models
{
    public class Labs
    {
        public int LabId { get; set; }
        public int ServiceTypeId { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string ImageName { get; set; }
        public string ImageUrl { get; set; }
        public string Topic { get; set; }
        public bool? OnNewWebsiteNow { get; set; }
        public bool? SuitedForHomePage { get; set; }
        public bool? CustomerExperience { get; set; }
        public bool? QualityAssurance { get; set; }
        public bool? Localization { get; set; }
        public bool? AudioProduction { get; set; }
        public bool? Engineering { get; set; }
        public bool? Campaign { get; set; }
        public int Event { get; set; }
        public string MetaTags { get; set; }
        public string DesktopImageUrl { get; set; }
        public string TabImageNameHorizondaUrl { get; set; }
        public string TabImageNamVerticalUrl { get; set; }
        public string MobileImageNameUrl { get; set; }
        public string DefaultImageUrl { get; set; }
        public string Reason { get; set; }
        public int ShortOrder { get; set; }
        public int IsActive { get; set; }
        public string LabType { get; set; }
        public string ImageAlternateText { get; set; }
        public string LanguageCode { get; set; }
        public int SliderId { get; set; }

        public string DeskL_ImageURL { get; set; }

        public string Desk_ImageURL { get; set; }
        public string TabH_ImageURL { get; set; }
        public string TabV_ImageURL { get; set; }
        public string Mobile_ImageURL { get; set; }
       
        public string CustomerExperienceImageUrl { get; set; }
        public string QualityAssuranceImageUrl { get; set; }
        public string LocalizationImageUrl { get; set; }
        public string AudioProductionImageUrl { get; set; }
        public string EngineeringImageUrl { get; set; }
        public string CampaignImageUrl { get; set; }
        public string LatestInsightImageUrl { get; set; }
        public int Stopped { get; set; }

        public string DetailedDescription { get; set; }

        public string DetailedName { get; set; }
        public string DetailedShortOrder { get; set; }
        public string ReadMoreUrl { get; set; }
        public List<Languages> Languages { get; set; }

        public List<Labs> LabsList { get; set; }

       
    }

    

}