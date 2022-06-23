using System.Collections.Generic;
using System.Linq;
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
            var songsNameList = songNameParser.parseNodes(songNamesHtml);

            BaseNodeHtmlParser<string> songArtistParser = new SongArtistParser();
            var songsArtistList = songArtistParser.parseNodes(songArtistsHtml);

            BaseNodeHtmlParser<string> songAlbumParser = new SongAlbumParser();
            var songsAlbumList = songAlbumParser.parseNodes(songAlbumsHtml);

            BaseNodeHtmlParser<string> songDurationParser = new SongDurationParser();
            var songsDurationList = songDurationParser.parseNodes(songDurationsHtml);


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