using System.Linq;
using Dapper;
using Npgsql;
using upwork_scraper_server.models;

namespace upwork_scraper_server.services
{
    public class SettingsService
    {
        private const string CONNECTION_STRING = "Host=localhost;Username=postgres;Password=postgres;Database=scraper;";
        public SettingsService()
        {
            
        }
        public bool IsActive()
        {
            using var connection = new NpgsqlConnection(CONNECTION_STRING);
            connection.Open();

            return connection.Query<bool>("select active from settings;").First();
        }

        public Settings GetSettings()
        {
            using var connection = new NpgsqlConnection(CONNECTION_STRING);
            connection.Open();

            return connection.Query<Settings>("select * from settings").First();
        }

        public void SetActive(string active)
        {
            using var connection = new NpgsqlConnection(CONNECTION_STRING);
            connection.Open();

            connection.Execute($"update settings set active = {active};");
        }
    }
}