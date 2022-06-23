using System.Collections.Generic;
using System.Threading.Tasks;
using HtmlAgilityPack;
using MusAppScrapper.Models;
using MusAppScrapper.Parsers;
using MusAppScrapper.Parsers.AlbumParsers;

namespace MusAppScrapper
{
    public class AlbumScrapper : BaseScrapper<MusicAlbum>
    {
        protected override List<MusicAlbum> ScrapInfo(HtmlDocument doc)
        {
            List<MusicAlbum> albums = new List<MusicAlbum>();

            HtmlNodeCollection albumNamesHtml = doc.DocumentNode.SelectNodes("//h1[@id='page-container__first-linked-element']");
            HtmlNodeCollection albumAvatarsHtml = doc.DocumentNode.SelectNodes("//img[@class='media-artwork-v2__image']");
            HtmlNodeCollection albumDescriptionsHtml = doc.DocumentNode.SelectNodes("//div[@class='product-page-header__metadata--notes typography-body-tall']");

            BaseNodeHtmlParser<string> albumNameParser = new AlbumNameParser();
            BaseNodeHtmlParser<string> albumAvatarParser = new AlbumAvatarParser();
            BaseNodeHtmlParser<string> albumDescriptionParser = new AlbumDescriptionParser();


            var albumNameListTask = Task.Factory.StartNew<List<string>>(() => albumNameParser.parseNodes(albumNamesHtml));

            var albumAvatarListTask = Task.Factory.StartNew<List<string>>(() => albumAvatarParser.parseNodes(albumAvatarsHtml));

            var albumDescriptionListTaks = Task.Factory.StartNew<List<string>>(() => albumDescriptionParser.parseNodes(albumDescriptionsHtml));

            Task.WaitAll(albumNameListTask, albumAvatarListTask, albumDescriptionListTaks);
            
            var albumNameList = albumNameListTask.Result;
            var albumAvatarList = albumAvatarListTask.Result;
            var albumDescriptionList = albumDescriptionListTaks.Result;

            for (int i = 0; i < albumNameList.Count; i++)
            {
                albums.Add(new MusicAlbum()
                {
                    Name = albumNameList[i],
                    Avatar = i < albumAvatarList.Count ? albumAvatarList[i] : "avatar_path",
                    Description = i < albumDescriptionList.Count ? albumDescriptionList[i] : "album description"
                });
            }

            return albums;
        }
    }
}