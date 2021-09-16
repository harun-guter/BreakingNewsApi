using Commons.Models;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Commons.Helpers
{
    public class Parse
    {
        protected List<News> newsList;
        protected HtmlDocument htmlDocument;
        protected HtmlDocument subDocument;
        protected HtmlNodeCollection htmlNodeCollection;

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
            htmlDocument = new HtmlDocument();
            subDocument = new HtmlDocument();
            newsList = new List<News>();
            id = 1000;
            title = string.Empty;
            summary = string.Empty;
            image = string.Empty;
            time = string.Empty;
            url = string.Empty;
            source = string.Empty;
        }

        [Obsolete]
        public List<News> HaberlerDotCom(string data)
        {
            htmlDocument.LoadHtml(data);
            htmlNodeCollection = htmlDocument.DocumentNode.SelectNodes("//div[@class='hblnBox']");

            if (htmlNodeCollection != null)
            {
                foreach (var htmlNode in htmlNodeCollection)
                {
                    if (htmlNode.InnerHtml.Contains("hblnTime") &&
                        htmlNode.InnerHtml.Contains("hblnImage") &&
                        htmlNode.InnerHtml.Contains("hblnContent"))
                    {
                        foreach (var classes in htmlNode.GetClasses())
                        {
                            subDocument.LoadHtml(htmlNode.InnerHtml);
                            title = subDocument.DocumentNode.SelectSingleNode("//div[@class='hblnContent']/a").InnerText;
                            summary = subDocument.DocumentNode.SelectSingleNode("//div[@class='hblnContent']/p").InnerText;

                            IEnumerable<HtmlAttribute> attributes = subDocument.DocumentNode.SelectSingleNode("//div[@class='hblnImage']/a/img").GetAttributes();

                            foreach (var attribute in attributes)
                            {
                                if (attribute.Name == "src")
                                    image = subDocument.DocumentNode.SelectSingleNode("//div[@class='hblnImage']/a/img").Attributes["src"].Value;
                                else if (attribute.Name == "data-src")
                                    image = subDocument.DocumentNode.SelectSingleNode("//div[@class='hblnImage']/a/img").Attributes["data-src"].Value;
                            }

                            if (image == "static/images/defaultblank170.png")
                                image = "https://www.haberler.com/son-dakika/" + "static/images/defaultblank170.png";

                            time = subDocument.DocumentNode.SelectSingleNode("//div[@class='hblnTime']").InnerText;
                            url = subDocument.DocumentNode.SelectSingleNode("//div[@class='hblnContent']/a").Attributes["href"].Value;
                            guid = Guid.NewGuid();
                            source = "https://www.haberler.com/";

                            newsList.Add(new News
                            {
                                Id = id,
                                Title = title,
                                Summary = summary,
                                Image = image,
                                Time = time,
                                Url = url,
                                UniqId = guid,
                                Source = new Source
                                {
                                    Title = "Haberler",
                                    Url = "https://haberler.com",
                                    Logo = "https://www.haberler.com/static/img/tasarim/haberler-logo.svg"
                                }
                            });

                            id++;
                        }
                    }
                }
            }
            return newsList;
        }

        public List<News> HaberlerDotComV2(string data)
        {
            var titles = Regex.Matches(data, @"class=""hblnTitle"" style=(.*?)>(.*?)</a>");
            var summaries = Regex.Matches(data, @"<div class=""hblnContent""> (.*?)<p>(.*?)</p></div>");
            var images = Regex.Matches(data, @"<div class=""hblnImage""> (.*?)<img(.*?)src=""(.*?)""(.*?)></a> </div>");
            var times = Regex.Matches(data, @"<div class=""hblnTime"">(.*?)<\/div>");
            var urls = Regex.Matches(data, @"<div class=""hblnContent""> <a href=""(.*?)"" (.*?)<\/div>");

            for (int i = 0; i < titles.Count; i++)
            {
                newsList.Add(new News
                {
                    Id = id,
                    Title = titles[i].Groups[2].ToString(),
                    Summary = summaries[i].Groups[2].ToString(),
                    Image = images[i].Groups[3].ToString() == "static/images/defaultblank170.png" ?
                        "https://www.haberler.com/son-dakika/" + "static/images/defaultblank170.png" :
                        images[i].Groups[3].ToString(),
                    Time = times[i].Groups[1].ToString(),
                    Url = urls[i].Groups[1].ToString(),
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
    }
}
