using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace PTW.DataAccess.Models
{
    public class PlayerSupport
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
        public IFormFile PlayerSupportHearder { get; set; }
        public IFormFile AnyPlatform { get; set; }
        public IFormFile AnyLanguage { get; set; }
        public IFormFile AnyComplexityLevel { get; set; }
        public IFormFile CompletelyWhiteLebeled { get; set; }
        public IFormFile CommunityModeration { get; set; }
        public IFormFile CommunityNature { get; set; }
        public IFormFile CommunityAnalysis { get; set; }
        public IFormFile MachineTranslationforPlayerSupport { get; set; }
        public IFormFile HumanTouch { get; set; }
        public IFormFile SupportHoned { get; set; }
        public IFormFile GamerforGamer { get; set; }
        public IFormFile Frame { get; set; }
        public bool IsActive { get; set; }
        public string ShortOrder { get; set; }
    }
}
