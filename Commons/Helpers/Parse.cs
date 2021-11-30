using Commons.Models;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Commons.Helpers
{
    public class Parse
    {
        protected List<News> newsList;

        protected int id;
        protected string title;
        protected string summary;
        protected string image;
        protected string time;
        protected string url;
        protected Guid guid;
        protected string source;

        public Parse()
        {
            newsList = new List<News>();
            id = 100000;
            title = string.Empty;
            summary = string.Empty;
            image = string.Empty;
            time = string.Empty;
            url = string.Empty;
            source = string.Empty;
        }

        public List<News> Haberler(string data)
        {
            var titles = Regex.Matches(data, @"<span class=""hblnTitle""(.*?)>(.*?)</span>");
            var summaries = Regex.Matches(data, @"<div class=""hblnContent"">(.*?)<p>(.*?)</p></div>");
            var images = Regex.Matches(data, @"<div class=""hblnImage""><img src=""(.*?)"" class=""lazyload"" data-src=""(.*?)""(.*?)></div>");
            var times = Regex.Matches(data, @"<div class=""hblnTime"">(.*?)</div>");
            var urls = Regex.Matches(data, @"<div class=""hblnBox"">(.*?)<a href=""(.*?)""(.*?)>(.*?)</div>(.*?)</div>");

            for (int i = 0; i < titles.Count; i++)
            {
                newsList.Add(new News
                {
                    Id = id,
                    Title = titles[i].Groups[2].Value.ToString(),
                    Summary = summaries[i].Groups[2].ToString(),
                    Image = images[i].Groups[2].ToString() == "static/images/default_blank_170.png" ||
                    images[i].Groups[2].Length == 0 ?
                        "https://www.haberler.com/son-dakika/" + "mstatic/images/Default_157.jpg" :
                        images[i].Groups[2].ToString(),
                    Time = times[i].Groups[1].ToString(),
                    Url = "https://haberler.com" + urls[i].Groups[2].ToString(),
                    UniqId = Guid.NewGuid(),
                    Source = new Source
                    {
                        Title = "Haberler",
                        Url = "https://haberler.com",
                        Logo = "https://www.haberler.com/static/img/tasarim/haberler-logo.svg"
                    }
                });
                id++;
            }

            return newsList;
        }

        public List<News> Hurriyet(string data)
        {
            var titles = Regex.Matches(data, @"<span class=""hblnTitle""(.*?)>(.*?)</span>");
            var summaries = Regex.Matches(data, @"<div class=""hblnContent"">(.*?)<p>(.*?)</p></div>");
            var images = Regex.Matches(data, @"<div class=""hblnImage""><img src=""(.*?)"" class=""lazyload"" data-src=""(.*?)""(.*?)></div>");
            var times = Regex.Matches(data, @"<div class=""hblnTime"">(.*?)</div>");
            var urls = Regex.Matches(data, @"<div class=""hblnBox"">(.*?)<a href=""(.*?)""(.*?)>(.*?)</div>(.*?)</div>");
            return newsList;
        }
    }
}
