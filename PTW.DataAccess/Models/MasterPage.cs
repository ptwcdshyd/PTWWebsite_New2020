using System;
using System.Collections.Generic;
using System.Text;

namespace PTW.DataAccess.Models
{
    public class MasterPage
    {
        public string HeaderContent { get; set; }
        public string FooterContent { get; set; }

        public string HtmlContent { get; set; }

        public int ModuleId { get; set; }
        public int LanguageId { get; set; }

        public string Content { get; set; }
    }
}
