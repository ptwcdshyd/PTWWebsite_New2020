using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace PTW.DataAccess.Models
{
    public class AudioProduction
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
        public IFormFile AudioProductionHearder { get; set; }
        public IFormFile Casting { get; set; }
        public IFormFile ProductManagement { get; set; }
        public IFormFile PerformanceDirection { get; set; }
        public IFormFile Audio { get; set; }
        public IFormFile OnLocalRecording { get; set; }
        public IFormFile PostProductionMastering { get; set; }
        public IFormFile AudioLocalization { get; set; }
        public IFormFile AudioCharacter { get; set; }
        public IFormFile CastingEdgeGlobalstudios { get; set; }
        public IFormFile DedicatedProductManagement { get; set; }
        public IFormFile Frame { get; set; }
    }
}
