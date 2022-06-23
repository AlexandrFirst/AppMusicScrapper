using HtmlAgilityPack;

namespace MusAppScrapper.Parsers.SongParsers
{
    public class SongNameParser : BaseNodeHtmlParser<string>
    {
        protected override string ParseNode(HtmlNode node)
        {
            var songName = node.InnerText;
            return songName;
        }
    }
}