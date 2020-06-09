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

        public List<Languages> LanguageList { get; set; }
        public List<Module> ModuleList { get; set; }

        public List<Images> Images { get; set; }
    }

    public class Languages {
        public int LanguageId { get; set; }
        public string Language { get; set; }
        public string LanguageCode { get; set; }

    }

    public class Module
    {
        public int ModuleId { get; set; }
        public string ModuleName { get; set; }
    }

    public class Images
    {
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
        public string Type { get; set; }
    }

}
