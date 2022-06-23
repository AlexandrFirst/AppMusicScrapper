using HtmlAgilityPack;

namespace MusAppScrapper.Parsers.AlbumParsers
{
    public class AlbumNameParser: BaseNodeHtmlParser<string>
    {
        protected override string ParseNode(HtmlNode node)
        {
            return node.InnerText.Trim();
        }
    }
}