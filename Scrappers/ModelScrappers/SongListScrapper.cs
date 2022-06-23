using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using MusAppScrapper.Models;
using MusAppScrapper.Parsers;
using MusAppScrapper.Parsers.SongParsers;

namespace MusAppScrapper
{
    public class SongListScrapper : BaseScrapper<MusicItem>
    {
        protected override List<MusicItem> ScrapInfo(HtmlDocument doc)
        {
            List<MusicItem> songs = new List<MusicItem>();

            HtmlNodeCollection songNamesHtml = doc.DocumentNode.SelectNodes("//div[@class='songs-list-row__song-name']");
            HtmlNodeCollection songArtistsHtml = doc.DocumentNode.SelectNodes("//div[@class='songs-list__col songs-list__col--artist typography-body']");
            HtmlNodeCollection songAlbumsHtml = doc.DocumentNode.SelectNodes("//div[@class='songs-list__col songs-list__col--album typography-body']");
            HtmlNodeCollection songDurationsHtml = doc.DocumentNode.SelectNodes("//time[@class='songs-list-row__length']");


            BaseNodeHtmlParser<string> songNameParser = new SongNameParser();
            BaseNodeHtmlParser<string> songArtistParser = new SongArtistParser();
            BaseNodeHtmlParser<string> songAlbumParser = new SongAlbumParser();
            BaseNodeHtmlParser<string> songDurationParser = new SongDurationParser();

            var songsNameListTask = Task.Factory.StartNew<List<string>>(() => songNameParser.parseNodes(songNamesHtml));
            var songsArtistListTask = Task.Factory.StartNew<List<string>>(() => songArtistParser.parseNodes(songArtistsHtml));
            var songsAlbumListTask = Task.Factory.StartNew<List<string>>(() => songAlbumParser.parseNodes(songAlbumsHtml));
            var songsDurationListTask = Task.Factory.StartNew<List<string>>(() => songDurationParser.parseNodes(songDurationsHtml));

            Task.WaitAll(songsNameListTask, songsArtistListTask, songsAlbumListTask, songsDurationListTask);

            var songsNameList = songsNameListTask.Result;
            var songsArtistList = songsArtistListTask.Result;
            var songsAlbumList = songsAlbumListTask.Result;
            var songsDurationList = songsDurationListTask.Result;

            for (int i = 0; i < songsNameList.Count; i++)
            {
                songs.Add(new MusicItem()
                {
                    SongName = songsNameList[i],
                    Artist = i < songsArtistList.Count ? songsArtistList[i] : "artist",
                    AlbumName = i < songsAlbumList.Count ? songsAlbumList[i] : "album",
                    Duration = i < songsDurationList.Count ? songsDurationList[i] : "00:00"
                });
            }

            return songs;
        }
    }
}