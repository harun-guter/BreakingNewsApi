using System;
using System.Net;
using System.Text;

namespace Commons.Helpers
{
    public class Request
    {
        private string _data;

        public string Download(string url)
        {
            using var webClient = new WebClient();
            Uri uri = new Uri(url);
            webClient.Encoding = Encoding.UTF8;
            return webClient.DownloadString(url);
        }
    }
}