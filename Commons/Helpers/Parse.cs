using Commons.Models;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;

namespace Commons.Helpers
{
    public class Parse
    {
        protected List<News> _newsList;
        protected HtmlDocument _htmlDocument;
        protected HtmlDocument _subDocument;
        protected HtmlNodeCollection _htmlNodeCollection;

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
            _htmlDocument = new HtmlDocument();
            _subDocument = new HtmlDocument();
            _newsList = new List<News>();
            id = 1000;
            title = string.Empty;
            summary = string.Empty;
            image = string.Empty;
            time = string.Empty;
            url = string.Empty;
            source = string.Empty;
        }

        public List<News> HaberlerDotCom(string data)
        {
            _htmlDocument.LoadHtml(data);
            _htmlNodeCollection = _htmlDocument.DocumentNode.SelectNodes("//div[@class='hblnBox']");

            if (_htmlNodeCollection != null)
            {
                foreach (var htmlNode in _htmlNodeCollection)
                {
                    if (htmlNode.InnerHtml.Contains("hblnTime") &&
                        htmlNode.InnerHtml.Contains("hblnImage") &&
                        htmlNode.InnerHtml.Contains("hblnContent"))
                    {
                        foreach (var classes in htmlNode.GetClasses())
                        {
                            _subDocument.LoadHtml(htmlNode.InnerHtml);

                            title = _subDocument.DocumentNode.SelectSingleNode("//div[@class='hblnContent']/a").InnerText;

                            summary = _subDocument.DocumentNode.SelectSingleNode("//div[@class='hblnContent']/p").InnerText;

                            IEnumerable<HtmlAttribute> attributes = _subDocument.DocumentNode.SelectSingleNode("//div[@class='hblnImage']/a/img").GetAttributes();
                            foreach (var attribute in attributes)
                            {
                                if (attribute.Name == "src")
                                    image = _subDocument.DocumentNode.SelectSingleNode("//div[@class='hblnImage']/a/img").Attributes["src"].Value;
                                else if (attribute.Name == "data-src")
                                    image = _subDocument.DocumentNode.SelectSingleNode("//div[@class='hblnImage']/a/img").Attributes["data-src"].Value;
                            }

                            if (image == "static/images/default_blank_170.png")
                                image = "https://www.haberler.com/son-dakika/" + "static/images/default_blank_170.png";

                            time = _subDocument.DocumentNode.SelectSingleNode("//div[@class='hblnTime']").InnerText;

                            url = _subDocument.DocumentNode.SelectSingleNode("//div[@class='hblnContent']/a").Attributes["href"].Value;

                            guid = Guid.NewGuid();

                            source = "https://www.haberler.com/";

                            _newsList.Add(new News
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
            return _newsList;
        }

    }
}
