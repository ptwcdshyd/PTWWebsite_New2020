using System;
using System.Collections.Generic;
using System.Text;

namespace PTW.DataAccess.Models
{
     public class MasterContent
    {
        public int ModuleId { get; set; }
        public int LanguageId { get; set; }

        public string Content { get; set; }
    }
}
