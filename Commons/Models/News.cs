using System;

namespace Commons.Models
{
    public class News
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Image { get; set; }
        public string Time { get; set; }
        public string Url { get; set; }
        public Guid UniqId { get; set; }
        public Source Source { get; set; }
    }
}