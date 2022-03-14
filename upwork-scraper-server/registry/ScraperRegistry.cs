using System;
using FluentScheduler;

namespace upwork_scraper_server.registry
{
    public class ScraperRegistry : Registry
    {
        public ScraperRegistry()
        {
            var scrape = new Action(() =>
            {
                Console.WriteLine("I'm wooorking...");
            });

            Schedule(scrape).ToRunEvery(5).Seconds();
        }
    }
}