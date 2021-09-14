using Commons.Models;
using System.Collections.Generic;

namespace WebApi.Models
{
    public class NewsViewModel
    {
        public int TotalNews { get; set; }
        public List<News> News { get; set; }
    }
}
