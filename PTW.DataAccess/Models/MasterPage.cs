﻿using System;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace PTW.DataAccess.Models
{
    public class MasterPage
    {
        // public string HeaderContent { get; set; }
        //  public string FooterContent { get; set; }
        public string FileString { get; set; }
        public string HtmlContent { get; set; }

        //public string ModuleName { get; set; }

        public int ModuleId { get; set; }
        public int LanguageId { get; set; }

        public int ResultCode { get; set; }

        public string LanguageCode { get; set; }

        public string Content { get; set; }
        //public string HeaderContent { get; set; }
        //public string FooterContent { get; set; }

        public List<Languages> LanguageList { get; set; }
        public List<Module> ModuleList { get; set; }
        public List<Sections> SectionsList { get; set; }
        public List<IFormFile> Photos { get; set; }

        public List<Images> Images { get; set; }
        public List<NewsAndLabs> NewsAndLabs { get; set; }
        public List<IFormFile> Files { get; set; }
        public string Culture { get; set; }

        public string Metatage { get; set; }
        public string MetaDescription { get; set; }
        public string MetaTitle { get; set; }

        public string MetaUrl { get; set; }

        public List<HomeLabs> HomeLabs { get; set; }
    }

   

    public class Languages {
        public int LanguageId { get; set; }
        public string Language { get; set; }
        public string LanguageCode { get; set; }
    }

   public class Images
    {
        public int ModuleId { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
    }

    public class Module
    {
        public int ModuleId { get; set; }
        public string ModuleName { get; set; }
    }

    public class Sections
    {
        public int SectionId { get; set; }
        public string SectionName { get; set; }
    }

    public class NewsAndLabs
    {
        public string Type { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
    }

    public class HomeLabs
    {
        public int ShortOrder { get; set; }
        public string Title { get; set; }
        public string ImagePath { get; set; }
        public string NavigateUrl { get; set; }
    }

}
