using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using HtmlAgilityPack;

namespace MusAppScrapper
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("-----Apple music scrapper-----");
            Scrapper scrapper = new Scrapper("https://music.apple.com/ua/playlist/a-list-pop/pl.5ee8333dbe944d9f9151e97d92d1ead9");
            scrapper.Run();
        }
    }
}
