using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using HtmlAgilityPack;

namespace MusAppScrapper
{
    public abstract class BaseScrapper<T>
    {
        public List<T> ParseLinks(string urlToCrawl)
        {
            WebClient webClient = new WebClient();

            byte[] data = webClient.DownloadData(urlToCrawl);
            string download = Encoding.ASCII.GetString(data);


            var doc = new HtmlDocument();
            doc.LoadHtml(download);

            List<T> result = ScrapInfo(doc);

           
            return result;
        }

        protected abstract List<T> ScrapInfo(HtmlDocument doc);
    }
}