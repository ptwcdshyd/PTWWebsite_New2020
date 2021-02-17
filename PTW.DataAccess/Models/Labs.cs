using Microsoft.AspNetCore.Http;
using Org.BouncyCastle.Asn1.Mozilla;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Text;

namespace PTW.DataAccess.Models
{
    public class Labs
    {
        public int ModuleId { get; set; }
        public int LabId { get; set; }
        public int SectionId { get; set; }
        public int ServiceTypeId { get; set; }
        public string EditLabId { get; set; }
        // [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

       // [Required(ErrorMessage = "Short Description is required")]
        public string ShortDescription { get; set; }

        //[Required(ErrorMessage = "Description is required")]
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

        public bool? ProductDevelopment { get; set; }
        public bool? TalentSolution { get; set; }
        public bool? PlayerSupport { get; set; }
        public bool? SpeechTech { get; set; }
        public bool? LocalizationQA { get; set; }
        public int Event { get; set; }
        public string MetaTags { get; set; }
        public string MetaUrl { get; set; }
        public string DesktopImageUrl { get; set; }
        public string TabImageNameHorizondaUrl { get; set; }
        public string TabImageNamVerticalUrl { get; set; }
        public string MobileImageNameUrl { get; set; }
        public string DefaultImageUrl { get; set; }
        public string Reason { get; set; }
        public int ShortOrder { get; set; }
        public int IsActive { get; set; }
        public bool IsActiveLab { get; set; }

        // [Required(ErrorMessage = "Lab Type is required")]
        public string LabType { get; set; }
        public int LabTypeId { get; set; }
        public string ImageAlternateText { get; set; }
        public string LanguageCode { get; set; }
        public int SliderId { get; set; }
        public string DeskL_ImageURL { get; set; }
        public string Desk_ImageURL { get; set; }
        public string TabH_ImageURL { get; set; }
        public string TabV_ImageURL { get; set; }
        public string Mobile_ImageURL { get; set; }
        public string CEandQAImageUrl { get; set; }
        public string CustomerExperienceImageUrl { get; set; }
        public string QualityAssuranceImageUrl { get; set; }
        public string LocalizationImageUrl { get; set; }
        public string AudioProductionImageUrl { get; set; }
        public string EngineeringImageUrl { get; set; }
        public string CampaignImageUrl { get; set; }
        public string LatestInsightImageUrl { get; set; }
        public string Image { get; set; }
        public int Stopped { get; set; }
        public string DetailedDescription { get; set; }
        public string DetailedName { get; set; }
        public string DetailedShortOrder { get; set; }
        public string ReadMoreUrl { get; set; }
        public string FilterCustomerExperience { get; set; }
        public string FilterQualityAssurance { get; set; }
        public string FilterLocalization { get; set; }
        public string FilterAudioProduction { get; set; }
        public string FilterEngineering { get; set; }
        public string FilterCampaign { get; set; }
        //[Required(ErrorMessage = "Service Type is required")]
        public string ServiceType { get; set; }
        public string StartDate { get; set; }//current date
        public string EndDate { get; set; }//sleected
        public string DesktopName { get; set; }

        public string MobileName { get; set; }

        public string TabImgHorizonalname { get; set; }

        public string TabImgVerticalname { get; set; }

        public string ReadMorename { get; set; }

        public string MetaTitle { get; set; }

        public string MetaDescription { get; set; }

        //[Required(ErrorMessage = "Please upload image and size should be 376x360 dimensions")]
        public IFormFile MobileImage { get; set; }

       // [Required(ErrorMessage = "Please upload image and size should be 1920x750 dimensions")]
        public IFormFile DesktopImage { get; set; }

       /// [Required(ErrorMessage = "Please upload image and size should be 1960x400 dimensions")]
        public IFormFile TabImageHorizonal { get; set; }

       // [Required(ErrorMessage = "Please upload image and size should be 800x350 dimensions")]
        public IFormFile TabImageNamVertical { get; set; }

        public IFormFile LabHeader { get; set; }
        public IFormFile LabImage { get; set; }
        public string Filename { get; set; }

        // [Required(ErrorMessage = "Please upload image and size should be 320x220 dimensions")]

        public string Customer_Experience { get; set; }
        public string Quality_Assurance { get; set; }
        public string _Localization { get; set; }
        public string _AudioProduction { get; set; }
        public string _Engineering { get; set; }
        public string _Campaign { get; set; }

        public IFormFile ReadMore { get; set; }


        public string FileString { get; set; }

        public bool? ActiveStatus { get; set; }

        public string Message { get; set; }
        public List<Languages> Languages { get; set; }
        public List<Labs> LabsList { get; set; }

        public List<LabUrlList> LabListUpdate { get; set; }

    }
    public class LabsArticles
    {

        public int ServiceType { get; set; } //dropdown
        public string Name { get; set; }// testbpx
        public string Description { get; set; }//texteditor
        public string ShortDescription { get; set; } //textbox
        public string ImageName { get; set; } //insert no need to show on browser
        public string ImageUrl { get; set; } //insert no need to show on browser
        public string Topic { get; set; } //textbox //not mandatory
        public bool? OnNewWebsiteNow { get; set; }
        public bool? SuitedForHomePage { get; set; }
        public bool? CustomerExperience { get; set; }
        public bool? QualityAssurance { get; set; }
        public bool? Localization { get; set; }
        public bool? AudioProduction { get; set; }
        public bool? Engineering { get; set; }
        public bool? Campaign { get; set; }
        public string MetaTags { get; set; }
        [Required(ErrorMessage = "Please upload image and size should be 960x400 dimensions")]
        public IFormFile DesktopImageUrl { get; set; }

        public IFormFile DesktopImage { get; set; }


       [Required(ErrorMessage = "Please upload image and size should be 960x800 dimensions")]
        public string TabImageNameHorizondaUrl { get; set; }

        public IFormFile TabImageHorizonal { get; set; }

        [Required(ErrorMessage = "Please upload image and size should be 1520x445 dimensions")]
        public string TabImageNamVerticalUrl { get; set; }

        public IFormFile TabImageNamVertical { get; set; }
        public string MobileImageNameUrl { get; set; }

        public IFormFile MobileImage { get; set; }
        public string ReadMoreUrl { get; set; }

        public IFormFile ReadMore { get; set; }

        [Required(ErrorMessage = "Please upload image and size should be 1520x445 dimensions")]
        public IFormFile Image { get; set; }
        public IFormFile LatestInsightImageUrl { get; set; }
        public string DefaultImageUrl { get; set; }
        public string IsLatestInsight { get; set; }
        public string StartDate { get; set; }//current date
        public string EndDate { get; set; }//sleected
        public string ShortOrder { get; set; }//no npublic stringeed to show on brouwserwser
        public string LabType { get; set; }
        public string IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string metatitle { get; set; }
        public string metadescription { get; set; }
        
        public int LabTypeId { get; set; }
    }

    public class EditLabs
    {
        public int ServiceType { get; set; } //dropdown
        public string Name { get; set; }// testbpx
        public string Description { get; set; }//texteditor
        public string ShortDescription { get; set; } //textbox
        public string ImageName { get; set; } //insert no need to show on browser
        public string ImageUrl { get; set; } //insert no need to show on browser
        public string Topic { get; set; } //textbox //not mandatory
        public bool? OnNewWebsiteNow { get; set; }
        public bool? SuitedForHomePage { get; set; }
        public bool? CustomerExperience { get; set; }
        public bool? QualityAssurance { get; set; }
        public bool? Localization { get; set; }
        public bool? AudioProduction { get; set; }
        public bool? Engineering { get; set; }
        public bool? Campaign { get; set; }
        public string MetaTags { get; set; }
        [Required(ErrorMessage = "Please upload image and size should be 960x400 dimensions")]
        public IFormFile DesktopImageUrl { get; set; }
        [Required(ErrorMessage = "Please upload image and size should be 960x800 dimensions")]
        public string TabImageNameHorizondaUrl { get; set; }
        [Required(ErrorMessage = "Please upload image and size should be 1520x445 dimensions")]
        public string TabImageNamVerticalUrl { get; set; }
        public string MobileImageNameUrl { get; set; }
        public string ReadMoreUrl { get; set; }
        public string LatestInsightImageUrl { get; set; }
        public string DefaultImageUrl { get; set; }

        [Required(ErrorMessage = "Please upload image and size should be 1520x445 dimensions")]
        public IFormFile Image { get; set; }
        public string IsLatestInsight { get; set; }
        public string StartDate { get; set; }//current date
        public string EndDate { get; set; }//sleected
        public string ShortOrder { get; set; }//no npublic stringeed to show on brouwserwser
        public string LabType { get; set; }
        public string IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string metatitle { get; set; }
        public string metadescription { get; set; }
        public string LabTypeId { get; set; }

        public IFormFile MobileImage { get; set; }

        public IFormFile DesktopImage { get; set; }
        public IFormFile TabImageHorizonal { get; set; }

        public IFormFile TabImageNamVertical { get; set; }

        public IFormFile ReadMore { get; set; }

        //public int LabTypeId { get; set; }
    }

    public class LabUrlList
    {
        public int LabId { get; set; }
        public string LabUrlTitle { get; set; }
        public int IsActive { get; set; }
    }
}