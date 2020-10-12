using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace PTW.DataAccess.Models
{
    public class Careers
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
        public IFormFile CareerHeader { get; set; }
        public IFormFile Deborah { get; set; }
        public IFormFile Frame { get; set; }
    }
}
