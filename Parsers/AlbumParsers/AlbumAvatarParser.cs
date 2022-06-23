using System.Linq;
using HtmlAgilityPack;

namespace MusAppScrapper.Parsers.AlbumParsers
{
    public class AlbumAvatarParser : BaseNodeHtmlParser<string>
    {
        protected override string ParseNode(HtmlNode node)
        {
            var avatarPath = node.Attributes.Where(a => a.Name == "src").First().Value;
            return avatarPath;
        }
    }
}