using System.Linq;
using Dapper;
using Npgsql;
using upwork_scraper_server.dtos;

namespace upwork_scraper_server.services
{
    public class JobService
    {
        private const string CONNECTION_STRING = "Host=localhost;Username=postgres;Password=postgres;Database=scraper;";

        public JobService()
        {
            
        }

        public void SaveJob(ResponseDtos.Job job)
        {
            var sql =
                $@"insert into job (cipher, title, description, amount, currency, engagement, proposals_tier, attributes) values
                        (@Cipher, @Title, Description, @Amount, @Currency, @Engagement, @ProposalsTier, @Attributes);";
            
            using var connection = new NpgsqlConnection(CONNECTION_STRING);
            connection.Open();

            connection.Execute(sql, new
            {
                Cipher = job.CipherText, 
                job.Title, 
                job.Description, 
                job.Amount, 
                Currency = job.Amount.CurrencyCode, 
                Engagement = job.ShortEngagement,
                job.ProposalsTier,
                Attributes = string.Join(", ", job.Attrs.Select(x => x.PrettyName))
            });
        }

        public bool JobExists(string cipher)
        {
            var sql = @"select exists(select 1 from job where cipher = 'cipher') as exists";

            using var connection = new NpgsqlConnection(CONNECTION_STRING);
            connection.Open();

            return connection.Query<bool>(sql).First();
        }
    }
}
