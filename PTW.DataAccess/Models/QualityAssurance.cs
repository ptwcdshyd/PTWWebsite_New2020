using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace PTW.DataAccess.Models
{
    public class QualityAssurance
    {
        public int LanguageId { get; set; }
        public int ModuleId { get; set; }
        public string LanguageCode { get; set; }
        public string Metatage { get; set; }
        public string MetaTitle { get; set; }
        public string MetaUrl { get; set; }
        public string Description { get; set; }
        public string MetaDescription { get; set; }

        public string Preview { get; set; }

        public string Filename { get; set; }

        public List<Languages> Languages { get; set; }
        public IFormFile QualityAssuranceHearder { get; set; }
        public IFormFile Functionality { get; set; }
        public IFormFile HardwareCompatiblityTesting { get; set; }
        public IFormFile CertificationTesting { get; set; }
        public IFormFile QualityAssuranceAutomation { get; set; }
        public IFormFile UserAccesptanceTesting { get; set; }
        public IFormFile Bug1 { get; set; }
        public IFormFile Bug2 { get; set; }
        public IFormFile Bug3 { get; set; }
        public IFormFile Bug4 { get; set; }
        public IFormFile HelpFullLink { get; set; }
        public IFormFile Frame { get; set; }
        public IFormFile SecurityTesting { get; set; }
        public bool IsActive { get; set; }
        public string ShortOrder { get; set; }
    }
}
