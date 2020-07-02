using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace PTW.DataAccess.Models
{
    public class News
    {
        public int NewsId { get; set; }

        public int EditNewsId { get; set; }
        public string ShortDescription { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Selection type is required")]
        public int Event { get; set; }
        public string ShortImageName { get; set; }
        public string ShortImageUrl { get; set; }
        public string HeaderImageName { get; set; }
        public string HeaderImageUrl { get; set; }
        public string LongImageName { get; set; }
        public string LongImageUrl { get; set; }
        public string UrlTitle { get; set; }

     
        [Required(ErrorMessage = "News Title is required")]
        public string NewsTitle { get; set; }

        [Required(ErrorMessage = "Published Date is required")]
        public string PublishedDate { get; set; }

        [DisplayFormat(DataFormatString = @"{0:dd\/MM\/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PublishedDate2 { get; set; }
        public string EventStartDate { get; set; }
        public string EventEndDate { get; set; }

        public string Topic { get; set; }

        public int ShortOrder { get; set; }

        public int ModuleId { get; set; }
        public int LanguageId { get; set; }

        public int IsActive { get; set; }


        public bool? ActiveStatus { get; set; }
        public bool? OnNewWebsiteNow { get; set; }

        public bool? SuitedForHomePage { get; set; }

        public bool? CustomerExperience { get; set; }
        public bool? QualityAssurance { get; set; }
        public bool? Localization { get; set; }
        public bool? AudioProduction { get; set; }
        public bool? Engineering { get; set; }
        public string ImageName { get; set; }

        public string MetaTags { get; set; }

        public string Message { get; set; }

        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public string FBUrl { get; set; }
        public string TwitterId { get; set; }
        public string LinkedInId { get; set; }
        public string Reason { get; set; }

        public string EventDate { get; set; }
        public string Location { get; set; }

        [Required(ErrorMessage = "Please upload image and size should be 1520x445 dimensions")]
        public IFormFile HeaderImage { get; set; }

        [Required(ErrorMessage = "Please upload image and size should be 1012x348 dimensions")]
        public IFormFile LongerImage { get; set; }

        [Required(ErrorMessage = "Please upload image and size should be 504x348 dimensions")]
        public IFormFile ShorterImage { get; set; }

        public int ddlSelectValue { get; set; }
        public List<NewsUrlList> NewsListUpdate { get; set; }

        public string LanguageCode { get; set; }
        public List<Languages> Languages { get; set; }


    }
    public class NewsUrlList
    {
        public int NewsId { get; set; }
        public string NewsUrlTitle { get; set; }
        public int IsActive { get; set; }
    }

    public class AddNews
    {
        public int NewsId { get; set; }
        public string ShortDescription { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Selection type is required")]
        public int Event { get; set; }
        public string ShortImageName { get; set; }
        public string ShortImageUrl { get; set; }
        public string HeaderImageName { get; set; }
        public string HeaderImageUrl { get; set; }
        public string LongImageName { get; set; }
        public string LongImageUrl { get; set; }
        public string UrlTitle { get; set; }


        [Required(ErrorMessage = "News Title is required")]
        public string NewsTitle { get; set; }

        [Required(ErrorMessage = "Published Date is required")]
        public string PublishedDate { get; set; }
        public string EventStartDate { get; set; }
        public string EventEndDate { get; set; }

        public string Topic { get; set; }

        public int ShortOrder { get; set; }

        public int ModuleId { get; set; }
        public int LanguageId { get; set; }

        public int IsActive { get; set; }


        public bool ActiveStatus { get; set; }
        public bool? OnNewWebsiteNow { get; set; }

        public bool? SuitedForHomePage { get; set; }

        public bool? CustomerExperience { get; set; }
        public bool? QualityAssurance { get; set; }
        public bool? Localization { get; set; }
        public bool? AudioProduction { get; set; }
        public bool? Engineering { get; set; }
        public string ImageName { get; set; }

        public string MetaTags { get; set; }

        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public string FBUrl { get; set; }
        public string TwitterId { get; set; }
        public string LinkedInId { get; set; }
        public string Reason { get; set; }

        public string EventDate { get; set; }
        public string Location { get; set; }

        [Required(ErrorMessage = "Please upload image and size should be 1520x445 dimensions")]
        public IFormFile HeaderImage { get; set; }

        [Required(ErrorMessage = "Please upload image and size should be 1012x348 dimensions")]
        public IFormFile LongerImage { get; set; }

        [Required(ErrorMessage = "Please upload image and size should be 504x348 dimensions")]
        public IFormFile ShorterImage { get; set; }

        public List<NewsUrlList> NewsListUpdate { get; set; }

    }

    public class EditNews
    {
        public int NewsId { get; set; }
        public string ShortDescription { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Selection type is required")]
        public int Event { get; set; }
        public string ShortImageName { get; set; }
        public string ShortImageUrl { get; set; }
        public string HeaderImageName { get; set; }
        public string HeaderImageUrl { get; set; }
        public string LongImageName { get; set; }
        public string LongImageUrl { get; set; }
        public string UrlTitle { get; set; }


        [Required(ErrorMessage = "News Title is required")]
        public string NewsTitle { get; set; }

        [Required(ErrorMessage = "Published Date is required")]
        public string PublishedDate { get; set; }
        public string EventStartDate { get; set; }
        public string EventEndDate { get; set; }

        public string Topic { get; set; }

        public int ShortOrder { get; set; }

        public int ModuleId { get; set; }
        public int LanguageId { get; set; }

        public int IsActive { get; set; }


        public bool ActiveStatus { get; set; }
        public bool? OnNewWebsiteNow { get; set; }

        public bool? SuitedForHomePage { get; set; }

        public bool? CustomerExperience { get; set; }
        public bool? QualityAssurance { get; set; }
        public bool? Localization { get; set; }
        public bool? AudioProduction { get; set; }
        public bool? Engineering { get; set; }
        public string ImageName { get; set; }

        public string MetaTags { get; set; }

        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public string FBUrl { get; set; }
        public string TwitterId { get; set; }
        public string LinkedInId { get; set; }
        public string Reason { get; set; }

        public string EventDate { get; set; }
        public string Location { get; set; }

        [Required(ErrorMessage = "Please upload image and size should be 1520x445 dimensions")]
        public IFormFile HeaderImage { get; set; }

        [Required(ErrorMessage = "Please upload image and size should be 1012x348 dimensions")]
        public IFormFile LongerImage { get; set; }

        [Required(ErrorMessage = "Please upload image and size should be 504x348 dimensions")]
        public IFormFile ShorterImage { get; set; }

        public List<NewsUrlList> NewsListUpdate { get; set; }

    }
}
