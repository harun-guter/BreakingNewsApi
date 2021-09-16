using Commons.Helpers;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        public NewsViewModel Get()
        {
            var request = new Request();
            var parse = new Parse();
            var result = new NewsViewModel();

            var data = request.Download("https://www.haberler.com/son-dakika/");
            //var news = parse.HaberlerDotCom(data);
            var news = parse.HaberlerDotComV2(data);

            result.TotalNews = news.Count;
            result.News = news;

            return result;
        }
    }
}
