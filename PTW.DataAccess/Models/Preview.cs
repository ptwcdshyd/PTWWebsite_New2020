using System;
using System.Collections.Generic;
using System.Text;

namespace PTW.DataAccess.Models
{
    public class Preview
    {
        public int ModuleId { get; set; }
        public string HtmlContent { get; set; }
        public string MetaTitle { get; set; }
        public string MetaUrl { get; set; }
        public string MetaDescription { get; set; }
    }
}
