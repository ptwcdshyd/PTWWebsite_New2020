using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace PTW.DataAccess.Models
{
    public class AboutModel
    {
        public int ProfileId { get; set; }
        public string ProfileTitle { get; set; }
        public string Description { get; set; }
        public string ImgPath { get; set; }

        public string Filename { get; set; }
        public IFormFile ImageUpload { get; set; }

        public IFormFile AboutPageHeader { get; set; }
        public IFormFile Frame { get; set; }
        public int OrderNo { get; set; }
        public bool? IsActive { get; set; }

        public string LanguageCode { get; set; }
        public string Culture { get; set; }

        public int ModuleId { get; set; }
        public int SectionId { get; set; }
        public string MetaTitle { get; set; }
        public string MetaUrl { get; set; }
        public string MetaDescription { get; set; }
        public List<Languages> Languages { get; set; }
        public List<Language> Languagelist { get; set; }
        public List<AboutProfile> AboutProfilelist { get; set; }
       
    }

    public class Language
    {
        public int LanguageId { get; set; }
        public string LanguageName { get; set; }
        public string LanguageCode { get; set; }
    }
    public class AboutProfile
    {
        public int AboutProfileId { get; set; }
        public string AboutProfileTitle { get; set; }

    }
}
