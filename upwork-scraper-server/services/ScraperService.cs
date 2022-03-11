using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using upwork_scraper_server.models;

namespace upwork_scraper_server.services
{
    public class ScraperService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ScraperService(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;
        
        public void Scrape()
        {
            // main scraping method
            var client = _httpClientFactory.CreateClient();
        }

        private async Task<HttpResponseMessage> SendRequestAsync(HttpClient client, string cookie)
        {
            // send upwork request
            var requestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://www.upwork.com/ab/find-work/api/feeds/embeddings-recommendations"),
                Headers = {{ "cookie", cookie }}
            };

            return await client.SendAsync(requestMessage);
        }
            
        private async Task<List<Job>> DeserializeResponse(HttpResponseMessage response)
        {
            return null;
        }

        private bool EvaluateJob(Object job)
        {
            // check if job qualifies for the notification
            return false;
        }
    }
}