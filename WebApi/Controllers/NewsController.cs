using Commons.Helpers;
using Commons.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        [HttpGet]
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

        [HttpGet("{id}")]
        public News Get(int id)
        {
            var request = new Request();
            var parse = new Parse();
            var result = new News();

            var data = request.Download("https://www.haberler.com/son-dakika/");
            var news = parse.HaberlerDotComV2(data);

            result = news.Where(i => i.Id == id).SingleOrDefault();
            return result;
        }
    }
}
