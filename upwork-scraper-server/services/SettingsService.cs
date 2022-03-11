using System;
using System.Linq;
using Dapper;
using Npgsql;
using upwork_scraper_server.models;

namespace upwork_scraper_server.services
{
    public class SettingsService
    {
        public SettingsService()
        {
            
        }
        public bool IsActive()
        {
            using var connection = new NpgsqlConnection("Host=localhost;Username=postgres;Password=postgres;Database=scraper;");
            
            connection.Open();

            var settings = connection.Query<Settings>("select * from settings;");
            
            Console.WriteLine(settings.First().Active);

            return false;
        }
    }
}