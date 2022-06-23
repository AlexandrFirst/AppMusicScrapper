using System.Linq;
using HtmlAgilityPack;

namespace MusAppScrapper.Parsers.SongParsers
{
    public class SongArtistParser : BaseNodeHtmlParser<string>
    {
        protected override string ParseNode(HtmlNode node)
        {
            var artist = node.ChildNodes.ElementAt(1)
                    .ChildNodes
                    .Elements()
                    .First().InnerText;
            return artist;
        }
    }
}