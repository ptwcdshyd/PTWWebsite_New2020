
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace PTW.DataAccess.Models
{
    public class Home
    {
        public int LanguageId { get; set; }
        public int ModuleId { get; set; }
        public string LanguageCode { get; set; }
        public string Metatage { get; set; }
        public string MetaTitle { get; set; }
        public string MetaUrl { get; set; }
        public string Description { get; set; }
        public string MetaDescription { get; set; }

        public string Header { get; set; }
        public string Footer { get; set; }
        public string Preview { get; set; }

        public string Filename { get; set; }

        public List<Languages> Languages { get; set; }
        public IFormFile CareersImage { get; set; }
        public IFormFile KickStartImage { get; set; }
        public IFormFile ProductDevelopmentOnMouseHover { get; set; }
        public IFormFile ProductDevelopment { get; set; }
        public IFormFile AudioProductionOnMouseHover { get; set; }
        public IFormFile AudioProduction { get; set; }
        public IFormFile LocalizationOnMouseHover { get; set; }
        public IFormFile Localization { get; set; }
        public IFormFile QualityAssuranceOnMouseHover { get; set; }
        public IFormFile QualityAssurance { get; set; }
        public IFormFile CustomerExperienceOnMouseHover { get; set; }
        public IFormFile CustomerExperience { get; set; }
        public IFormFile PageBackgroundImage { get; set; }
        public IFormFile Frame { get; set; }
        public IFormFile LocationMap { get; set; }
    }
}
