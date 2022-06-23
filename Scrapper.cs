using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MusAppScrapper.Models;

namespace MusAppScrapper
{
    public class Scrapper
    {
        private readonly string scrapUrl;

        BaseScrapper<MusicItem> songScraper = new SongListScrapper();
        BaseScrapper<MusicAlbum> albumScraper = new AlbumScrapper();

        public Scrapper(string scrapUrl)
        {
            this.scrapUrl = scrapUrl;
        }

        public void Run()
        {
            Task<List<MusicItem>> songScraperTask = Task.Factory.StartNew<List<MusicItem>>(() => songScraper.ParseLinks(scrapUrl));
            Task<List<MusicAlbum>> albumScraperTask = Task.Factory.StartNew<List<MusicAlbum>>(() => albumScraper.ParseLinks(scrapUrl));
            Task.WaitAll(songScraperTask, albumScraperTask);

            DisplayContent(albumScraperTask.Result);
            DisplayContent(songScraperTask.Result);
        }

        private void DisplayContent(List<MusicItem> songs)
        {

            System.Console.WriteLine("Songs ---------------------------------");
            System.Console.WriteLine("{0,-50}{1,-30}{2,-20}{3,-5}", "song name", "artist", "album name", "duration");

            foreach (var song in songs)
            {
                System.Console.WriteLine("{0,-50}{1,-20}{2,-50}{3,-5}", song.SongName, song.Artist, song.AlbumName, song.Duration);
            }
        }

        private void DisplayContent(List<MusicAlbum> albums)
        {
            System.Console.WriteLine("Album ---------------------------------");
            System.Console.WriteLine("{0,-50}{1,-70}{2,-50}", "Album name", "albom avatar", "album description");

            foreach (var album in albums)
            {
                System.Console.WriteLine("{0,-20}{1,-70}{2,-50}", album.Name, GetAbsoluteUrlString(scrapUrl, album.Avatar), album.Description);
            }
        }

        string GetAbsoluteUrlString(string baseUrl, string url)
        {
            var uri = new Uri(url, UriKind.RelativeOrAbsolute);
            if (!uri.IsAbsoluteUri)
                uri = new Uri(new Uri(baseUrl), uri);
            return uri.ToString();
        }
    }
}