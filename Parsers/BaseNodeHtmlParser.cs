using System.Collections.Generic;
using HtmlAgilityPack;

namespace MusAppScrapper.Parsers
{
    public abstract class BaseNodeHtmlParser<T>
    {
        protected abstract T ParseNode(HtmlNode node);

        public List<T> parseNodes(HtmlNodeCollection htmlCollection)
        {
            List<T> result = new List<T>();
            foreach (var item in htmlCollection)
            {
                result.Add(ParseNode(item));
            }
            return result;
        }
    }
}