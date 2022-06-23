using System.Linq;
using HtmlAgilityPack;

namespace MusAppScrapper.Parsers.AlbumParsers
{
    public class AlbumDescriptionParser : BaseNodeHtmlParser<string>
    {
        protected override string ParseNode(HtmlNode node)
        {
            var description = node
                .ChildNodes.Where(n => n.Name == "div").First()
                .ChildNodes.Where(n => n.Name == "div").First()
                .InnerText;
            return description.Trim();
        }
    }
}