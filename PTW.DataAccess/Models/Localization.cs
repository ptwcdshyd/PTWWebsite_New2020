using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace PTW.DataAccess.Models
{
    public class Localization
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
        public IFormFile LocalizatonHearder { get; set; }
        public IFormFile GameTranslation { get; set; }
        public IFormFile LocalizationAndCulturalization { get; set; }
        public IFormFile PostEditingMachineTranslation { get; set; }
        public IFormFile MarketingAndAdvertisingTranslation { get; set; }

        public IFormFile Frame { get; set; }
        public bool IsActive { get; set; }
        public string ShortOrder { get; set; }
    }
}
