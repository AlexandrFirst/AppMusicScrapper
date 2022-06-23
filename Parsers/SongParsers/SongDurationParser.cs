using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace MusAppScrapper.Parsers.SongParsers
{
    public class SongDurationParser : BaseNodeHtmlParser<string>
    {
        protected override string ParseNode(HtmlNode node)
        {
            var songTime = node.InnerText;
            return songTime.Trim();
        }
    }
}