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

            var query = @"
                        select
                            s.active,
                            s.cookie,
                            s.telegram_api_key,
                            s.telegram_chat_id,
                            s.engagement,
                            string_agg(c.name, ',') as categories
                        from settings s
                        full join category c on true
                        group by 1, 2, 3, 4, 5;
                        ";
            
            return connection.Query<Settings>(query).First();
        }

        public void SetActive(bool active)
        {
            using var connection = new NpgsqlConnection(CONNECTION_STRING);
            connection.Open();

            connection.Execute($"update settings set active = {active.ToString()};");
        }
    }
}