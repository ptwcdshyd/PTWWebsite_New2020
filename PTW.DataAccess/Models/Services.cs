using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace PTW.DataAccess.Models
{
   public class Services
    {
        public IFormFile Customerexperience { get; set; }
        public IFormFile Audioproduction { get; set; }
        public IFormFile Engineering { get; set; }
        public IFormFile Qualityassurance { get; set; }
        public IFormFile Localization { get; set; }
    }
}
