using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace PTW.DataAccess.Models
{
    public class TalentSolution
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
        public IFormFile TalentSolutionHearder { get; set; }
        public IFormFile VettedAndVerified { get; set; }
        public IFormFile FasterDevlivery { get; set; }
        public IFormFile FlexibleSolutions { get; set; }
       
        public IFormFile Unity { get; set; }
        public IFormFile Unreal { get; set; }
        public IFormFile GameMakerStudio { get; set; }
        public IFormFile HTML { get; set; }

        public IFormFile GameDevelopment { get; set; }
        public IFormFile CreativeandDesign { get; set; }
        public IFormFile Infrastructure { get; set; }
        public IFormFile RiskAndSecurity { get; set; }
        public IFormFile DataManagement { get; set; }
        public IFormFile ProjectManagement { get; set; }
        public IFormFile Frame { get; set; }
        public bool IsActive { get; set; }
        public string ShortOrder { get; set; }
    }
}
