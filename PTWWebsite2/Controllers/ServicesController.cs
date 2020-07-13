using Microsoft.AspNetCore.Mvc;
using PTW.DataAccess.Models;
using PTW.DataAccess.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace PTWWebsite2.Controllers
{
    public class ServicesController:Controller
    {
        private readonly IMasterService _masterService;
        public ServicesController(IMasterService masterService)
        {
            _masterService = masterService;
        }
        
        
        [Route("Service")]
        [Route("{culture}/Service")]
        
        public IActionResult GetServicecontent(string culture)
        {
            MasterPage masterPage = new MasterPage();
            DataTable dtContent = _masterService.GetModuleContent("Services", (culture == null ? "en-US" : culture));
            masterPage.HtmlContent = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Services")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
            ViewData["Header"] = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Header")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
            ViewData["Footer"] = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Footer")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
            return View(masterPage);
        }

        [Route("Service_Cx")]
        [Route("{culture}/Service_Cx")]

        public IActionResult GetServicCxecontent(string culture)
        {
            MasterPage masterPage = new MasterPage();
            DataTable dtContent = _masterService.GetModuleContent("Services_CX", (culture == null ? "en-US" : culture));
            masterPage.HtmlContent = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Services_CX")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
            ViewData["Header"] = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Header")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
            ViewData["Footer"] = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Footer")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();

            MasterPage masterPage1 = _masterService.GetNewsAndLabDetails("CX", 1);
            masterPage1.HtmlContent = masterPage.HtmlContent;
            string heading = "<div class=\"col-lg-12 col-md-12 col-sm-12 col-sx-12\"><p id=\"ContentPlaceHolder1_P1\">News &amp; LAB Feed</p></div>";

            switch (culture)
            {
                case null:
                    string content = "";

                    content = "<div class=\"col-lg-12 col-md-12 col-sm-12 col-sx-12\"><p id=\"ContentPlaceHolder1_P1\">News &amp; LAB Feed</p></div>";
                    string englishContent = Content(masterPage1.NewsAndLabs, content, "more");

                    masterPage1.HtmlContent = masterPage1.HtmlContent.Replace(heading, englishContent);

                    break;
                case "fr-CA":
                    string content1 = "";

                    content1 = "<div class=\"col-lg-12 col-md-12 col-sm-12 col-sx-12\"><p id=\"ContentPlaceHolder1_P1\">Fil LABO &amp; Infos</p></div>";
                    string frenchContent = Content(masterPage1.NewsAndLabs, content1, "Plus");

                    masterPage1.HtmlContent = masterPage1.HtmlContent.Replace(heading, frenchContent);

                    break;
                case "ko":
                    string content2 = "";

                    content2 = "<div class=\"col-lg-12 col-md-12 col-sm-12 col-sx-12\"><p id=\"ContentPlaceHolder1_P1\">소식 및 LAB 피드</p></div>";
                    string koreanContent = Content(masterPage1.NewsAndLabs, content2, "더 보기");

                    masterPage1.HtmlContent = masterPage1.HtmlContent.Replace(heading, koreanContent);

                    break;
                case "ja":
                    string content3 = "";

                    content3 = "<div class=\"col-lg-12 col-md-12 col-sm-12 col-sx-12\"><p id=\"ContentPlaceHolder1_P1\">ニュースとLABフィード</p></div>";
                    string japaneseContent = Content(masterPage1.NewsAndLabs, content3, "詳細");

                    masterPage1.HtmlContent = masterPage1.HtmlContent.Replace(heading, japaneseContent);

                    break;
                case "zh-CHS":
                    string content4 = "";

                    content4 = "<div class=\"col-lg-12 col-md-12 col-sm-12 col-sx-12\"><p id=\"ContentPlaceHolder1_P1\">新闻 &amp; 实验反馈</p></div>";
                    string simplifiedChineseContent = Content(masterPage1.NewsAndLabs, content4, "更多");

                    masterPage1.HtmlContent = masterPage1.HtmlContent.Replace(heading, simplifiedChineseContent);

                    break;
                case "zh-CHT":
                    string content5 = "";

                    content5 = "<div class=\"col-lg-12 col-md-12 col-sm-12 col-sx-12\"><p id=\"ContentPlaceHolder1_P1\">News &amp; LAB Feed</p></div>";
                    string traditionalChinese = Content(masterPage1.NewsAndLabs, content5, "more");

                    masterPage1.HtmlContent = masterPage1.HtmlContent.Replace(heading, traditionalChinese);

                    break;
            }

            return View(masterPage1);
        }

        public string Content(List<NewsAndLabs> newsandlabs, string content, string language)
        {
            foreach (var item in newsandlabs)
            {
                if (item.Type == "News")
                {
                    content = content + "<div class=\"col-lg-12 col-md-4 col-sm-12 col-sx-12\"><div class=\"g-underline\"></div><h6>" + item.Title + " </h6> <a href=\"/News/" + item.ShortDescription.Replace(" ", "-").Replace("&", "@") + "\" titlevalue=\"4\" class=\"btn btn-default btn-sm btn-r text-lowercase  more pull-down float-left\">" + language + "</a></div>";
                }
                if (item.Type == "Labs")
                {
                    content = content + "<div class=\"col-lg-12 col-md-4 col-sm-12 col-sx-12\"><div class=\"g-underline\"></div><h6>" + item.Title + " </h6> <a href=\"/Lab/" + item.ShortDescription.Replace(" ", "-").Replace("&", "@") + "\" titlevalue=\"4\" class=\"btn btn-default btn-sm btn-r text-lowercase  more pull-down float-left\">" + language + "</a></div>";
                }
            }
            return content;
        }
        [Route("Service_QA")]
        [Route("{culture}/Service_QA")]
        //[Route("{culture}/")]
        public IActionResult GetServiceQacontent(string culture)
        {
            MasterPage masterPage = new MasterPage();
            DataTable dtContent = _masterService.GetModuleContent("Services_QA", (culture == null ? "en-US" : culture));
            masterPage.HtmlContent = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Services_QA")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
            ViewData["Header"] = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Header")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
            ViewData["Footer"] = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Footer")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
            MasterPage masterPage1 = _masterService.GetNewsAndLabDetails("QA", 1);
            masterPage1.HtmlContent = masterPage.HtmlContent;
            string heading = "<div class=\"col-lg-12 col-md-12 col-sm-12 col-sx-12\"><p id=\"ContentPlaceHolder1_P1\">News &amp; LAB Feed</p></div>";

            switch (culture)
            {
                case null:
                    string content = "";

                    content = "<div class=\"col-lg-12 col-md-12 col-sm-12 col-sx-12\"><p id=\"ContentPlaceHolder1_P1\">News &amp; LAB Feed</p></div>";
                    string englishContent = Content(masterPage1.NewsAndLabs, content, "more");

                    masterPage1.HtmlContent = masterPage1.HtmlContent.Replace(heading, englishContent);

                    break;
                case "fr-CA":
                    string content1 = "";

                    content1 = "<div class=\"col-lg-12 col-md-12 col-sm-12 col-sx-12\"><p id=\"ContentPlaceHolder1_P1\">Fil LABO &amp; Infos</p></div>";
                    string frenchContent = Content(masterPage1.NewsAndLabs, content1, "Plus");

                    masterPage1.HtmlContent = masterPage1.HtmlContent.Replace(heading, frenchContent);

                    break;
                case "ko":
                    string content2 = "";

                    content2 = "<div class=\"col-lg-12 col-md-12 col-sm-12 col-sx-12\"><p id=\"ContentPlaceHolder1_P1\">소식 및 LAB 피드</p></div>";
                    string koreanContent = Content(masterPage1.NewsAndLabs, content2, "더 보기");

                    masterPage1.HtmlContent = masterPage1.HtmlContent.Replace(heading, koreanContent);

                    break;
                case "ja":
                    string content3 = "";

                    content3 = "<div class=\"col-lg-12 col-md-12 col-sm-12 col-sx-12\"><p id=\"ContentPlaceHolder1_P1\">ニュースとLABフィード</p></div>";
                    string japaneseContent = Content(masterPage1.NewsAndLabs, content3, "詳細");

                    masterPage1.HtmlContent = masterPage1.HtmlContent.Replace(heading, japaneseContent);

                    break;
                case "zh-CHS":
                    string content4 = "";

                    content4 = "<div class=\"col-lg-12 col-md-12 col-sm-12 col-sx-12\"><p id=\"ContentPlaceHolder1_P1\">新闻 &amp; 实验反馈</p></div>";
                    string simplifiedChineseContent = Content(masterPage1.NewsAndLabs, content4, "更多");

                    masterPage1.HtmlContent = masterPage1.HtmlContent.Replace(heading, simplifiedChineseContent);

                    break;
                case "zh-CHT":
                    string content5 = "";

                    content5 = "<div class=\"col-lg-12 col-md-12 col-sm-12 col-sx-12\"><p id=\"ContentPlaceHolder1_P1\">News &amp; LAB Feed</p></div>";
                    string traditionalChinese = Content(masterPage1.NewsAndLabs, content5, "more");

                    masterPage1.HtmlContent = masterPage1.HtmlContent.Replace(heading, traditionalChinese);

                    break;
            }
            return View(masterPage1);
        }

        [Route("Localization")]
        [Route("{culture}/Localization")]
        //[Route("{culture}/")]
        public IActionResult GetServiceLocalizationcontent(string culture)
        {
            MasterPage masterPage = new MasterPage();
            DataTable dtContent = _masterService.GetModuleContent("Localization", (culture == null ? "en-US" : culture));
            masterPage.HtmlContent = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Localization")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
            ViewData["Header"] = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Header")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
            ViewData["Footer"] = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Footer")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
            MasterPage masterPage1 = _masterService.GetNewsAndLabDetails("LOCALIZATION", 1);
            masterPage1.HtmlContent = masterPage.HtmlContent;
            string heading = "<div class=\"col-lg-12 col-md-12 col-sm-12 col-sx-12\"><p id=\"ContentPlaceHolder1_P1\">News &amp; LAB Feed</p></div>";
            switch (culture)
            {
                case null:
                    string content = "";

                    content = "<div class=\"col-lg-12 col-md-12 col-sm-12 col-sx-12\"><p id=\"ContentPlaceHolder1_P1\">News &amp; LAB Feed</p></div>";
                    string englishContent = Content(masterPage1.NewsAndLabs, content, "more");

                    masterPage1.HtmlContent = masterPage1.HtmlContent.Replace(heading, englishContent);

                    break;
                case "fr-CA":
                    string content1 = "";

                    content1 = "<div class=\"col-lg-12 col-md-12 col-sm-12 col-sx-12\"><p id=\"ContentPlaceHolder1_P1\">Fil LABO &amp; Infos</p></div>";
                    string frenchContent = Content(masterPage1.NewsAndLabs, content1, "Plus");

                    masterPage1.HtmlContent = masterPage1.HtmlContent.Replace(heading, frenchContent);

                    break;
                case "ko":
                    string content2 = "";

                    content2 = "<div class=\"col-lg-12 col-md-12 col-sm-12 col-sx-12\"><p id=\"ContentPlaceHolder1_P1\">소식 및 LAB 피드</p></div>";
                    string koreanContent = Content(masterPage1.NewsAndLabs, content2, "더 보기");

                    masterPage1.HtmlContent = masterPage1.HtmlContent.Replace(heading, koreanContent);

                    break;
                case "ja":
                    string content3 = "";

                    content3 = "<div class=\"col-lg-12 col-md-12 col-sm-12 col-sx-12\"><p id=\"ContentPlaceHolder1_P1\">ニュースとLABフィード</p></div>";
                    string japaneseContent = Content(masterPage1.NewsAndLabs, content3, "詳細");

                    masterPage1.HtmlContent = masterPage1.HtmlContent.Replace(heading, japaneseContent);

                    break;
                case "zh-CHS":
                    string content4 = "";

                    content4 = "<div class=\"col-lg-12 col-md-12 col-sm-12 col-sx-12\"><p id=\"ContentPlaceHolder1_P1\">新闻 &amp; 实验反馈</p></div>";
                    string simplifiedChineseContent = Content(masterPage1.NewsAndLabs, content4, "更多");

                    masterPage1.HtmlContent = masterPage1.HtmlContent.Replace(heading, simplifiedChineseContent);

                    break;
                case "zh-CHT":
                    string content5 = "";

                    content5 = "<div class=\"col-lg-12 col-md-12 col-sm-12 col-sx-12\"><p id=\"ContentPlaceHolder1_P1\">News &amp; LAB Feed</p></div>";
                    string traditionalChinese = Content(masterPage1.NewsAndLabs, content5, "more");

                    masterPage1.HtmlContent = masterPage1.HtmlContent.Replace(heading, traditionalChinese);

                    break;
            }
            return View(masterPage1);
        }

        [Route("AudioProduction")]

        [Route("{culture}/AudioProduction")]
        //[Route("{culture}/")]
        public IActionResult GetServiceAudioProductioncontent(string culture)
        {
            MasterPage masterPage = new MasterPage();
            DataTable dtContent = _masterService.GetModuleContent("Audio Production", (culture == null ? "en-US" : culture));
            masterPage.HtmlContent = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Audio Production")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
            ViewData["Header"] = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Header")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
            ViewData["Footer"] = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Footer")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
            MasterPage masterPage1 = _masterService.GetNewsAndLabDetails("AUDIO", 1);
            masterPage1.HtmlContent = masterPage.HtmlContent;
            string heading = "<div class=\"col-lg-12 col-md-12 col-sm-12 col-sx-12\"><p id=\"ContentPlaceHolder1_P1\">News &amp; LAB Feed</p></div>";
            switch (culture)
            {
                case null:
                    string content = "";

                    content = "<div class=\"col-lg-12 col-md-12 col-sm-12 col-sx-12\"><p id=\"ContentPlaceHolder1_P1\">News &amp; LAB Feed</p></div>";
                    string englishContent = Content(masterPage1.NewsAndLabs, content, "more");

                    masterPage1.HtmlContent = masterPage1.HtmlContent.Replace(heading, englishContent);

                    break;
                case "fr-CA":
                    string content1 = "";

                    content1 = "<div class=\"col-lg-12 col-md-12 col-sm-12 col-sx-12\"><p id=\"ContentPlaceHolder1_P1\">Fil LABO &amp; Infos</p></div>";
                    string frenchContent = Content(masterPage1.NewsAndLabs, content1, "Plus");

                    masterPage1.HtmlContent = masterPage1.HtmlContent.Replace(heading, frenchContent);

                    break;
                case "ko":
                    string content2 = "";

                    content2 = "<div class=\"col-lg-12 col-md-12 col-sm-12 col-sx-12\"><p id=\"ContentPlaceHolder1_P1\">소식 및 LAB 피드</p></div>";
                    string koreanContent = Content(masterPage1.NewsAndLabs, content2, "더 보기");

                    masterPage1.HtmlContent = masterPage1.HtmlContent.Replace(heading, koreanContent);

                    break;
                case "ja":
                    string content3 = "";

                    content3 = "<div class=\"col-lg-12 col-md-12 col-sm-12 col-sx-12\"><p id=\"ContentPlaceHolder1_P1\">ニュースとLABフィード</p></div>";
                    string japaneseContent = Content(masterPage1.NewsAndLabs, content3, "詳細");

                    masterPage1.HtmlContent = masterPage1.HtmlContent.Replace(heading, japaneseContent);

                    break;
                case "zh-CHS":
                    string content4 = "";

                    content4 = "<div class=\"col-lg-12 col-md-12 col-sm-12 col-sx-12\"><p id=\"ContentPlaceHolder1_P1\">新闻 &amp; 实验反馈</p></div>";
                    string simplifiedChineseContent = Content(masterPage1.NewsAndLabs, content4, "更多");

                    masterPage1.HtmlContent = masterPage1.HtmlContent.Replace(heading, simplifiedChineseContent);

                    break;
                case "zh-CHT":
                    string content5 = "";

                    content5 = "<div class=\"col-lg-12 col-md-12 col-sm-12 col-sx-12\"><p id=\"ContentPlaceHolder1_P1\">News &amp; LAB Feed</p></div>";
                    string traditionalChinese = Content(masterPage1.NewsAndLabs, content5, "more");

                    masterPage1.HtmlContent = masterPage1.HtmlContent.Replace(heading, traditionalChinese);

                    break;
            }
            return View(masterPage1);
        }

        [Route("ProductDevelopment")]

        [Route("{culture}/ProductDevelopment")]
        //[Route("{culture}/")]
        public IActionResult GetServiceEngineeringcontent(string culture)
        {
            MasterPage masterPage = new MasterPage();
            DataTable dtContent = _masterService.GetModuleContent("ProductDevelopment", (culture == null ? "en-US" : culture));
            masterPage.HtmlContent = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("ProductDevelopment")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
            ViewData["Header"] = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Header")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
            ViewData["Footer"] = dtContent.Rows.Cast<DataRow>().Where(x => Convert.ToString(x["ModuleName"]).Equals("Footer")).Select(y => Convert.ToString(y["Content"])).FirstOrDefault();
            MasterPage masterPage1 = _masterService.GetNewsAndLabDetails("ENGINEERING", 1);
            masterPage1.HtmlContent = masterPage.HtmlContent;
            string heading = "<div class=\"col-lg-12 col-md-12 col-sm-12 col-sx-12\"><p id=\"ContentPlaceHolder1_P1\">News &amp; LAB Feed</p></div>";
            switch (culture)
            {
                case null:
                    string content = "";

                    content = "<div class=\"col-lg-12 col-md-12 col-sm-12 col-sx-12\"><p id=\"ContentPlaceHolder1_P1\">News &amp; LAB Feed</p></div>";
                    string englishContent = Content(masterPage1.NewsAndLabs, content, "more");

                    masterPage1.HtmlContent = masterPage1.HtmlContent.Replace(heading, englishContent);

                    break;
                case "fr-CA":
                    string content1 = "";

                    content1 = "<div class=\"col-lg-12 col-md-12 col-sm-12 col-sx-12\"><p id=\"ContentPlaceHolder1_P1\">Fil LABO &amp; Infos</p></div>";
                    string frenchContent = Content(masterPage1.NewsAndLabs, content1, "Plus");

                    masterPage1.HtmlContent = masterPage1.HtmlContent.Replace(heading, frenchContent);

                    break;
                case "ko":
                    string content2 = "";

                    content2 = "<div class=\"col-lg-12 col-md-12 col-sm-12 col-sx-12\"><p id=\"ContentPlaceHolder1_P1\">소식 및 LAB 피드</p></div>";
                    string koreanContent = Content(masterPage1.NewsAndLabs, content2, "더 보기");

                    masterPage1.HtmlContent = masterPage1.HtmlContent.Replace(heading, koreanContent);

                    break;
                case "ja":
                    string content3 = "";

                    content3 = "<div class=\"col-lg-12 col-md-12 col-sm-12 col-sx-12\"><p id=\"ContentPlaceHolder1_P1\">ニュースとLABフィード</p></div>";
                    string japaneseContent = Content(masterPage1.NewsAndLabs, content3, "詳細");

                    masterPage1.HtmlContent = masterPage1.HtmlContent.Replace(heading, japaneseContent);

                    break;
                case "zh-CHS":
                    string content4 = "";

                    content4 = "<div class=\"col-lg-12 col-md-12 col-sm-12 col-sx-12\"><p id=\"ContentPlaceHolder1_P1\">新闻 &amp; 实验反馈</p></div>";
                    string simplifiedChineseContent = Content(masterPage1.NewsAndLabs, content4, "更多");

                    masterPage1.HtmlContent = masterPage1.HtmlContent.Replace(heading, simplifiedChineseContent);

                    break;
                case "zh-CHT":
                    string content5 = "";

                    content5 = "<div class=\"col-lg-12 col-md-12 col-sm-12 col-sx-12\"><p id=\"ContentPlaceHolder1_P1\">News &amp; LAB Feed</p></div>";
                    string traditionalChinese = Content(masterPage1.NewsAndLabs, content5, "more");

                    masterPage1.HtmlContent = masterPage1.HtmlContent.Replace(heading, traditionalChinese);

                    break;
            }
            return View(masterPage1);
        }
    }
}
