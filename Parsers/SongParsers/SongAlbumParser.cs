using System.Linq;
using HtmlAgilityPack;

namespace MusAppScrapper.Parsers.SongParsers
{
    public class SongAlbumParser : BaseNodeHtmlParser<string>
    {
        protected override string ParseNode(HtmlNode node)
        {
            var song = node.ChildNodes
                    .ElementAt(1)
                    .ChildNodes
                    .ElementAt(1)
                    .ChildNodes
                    .Elements().First().InnerText;
            return song;
        }
    }
}