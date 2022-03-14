using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using FluentScheduler;
using Newtonsoft.Json;
using upwork_scraper_server.dtos;

namespace upwork_scraper_server.services
{
    public class ScraperService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly SettingsService _settingsService;

        public ScraperService(IHttpClientFactory httpClientFactory, SettingsService settingsService)
        {
            _httpClientFactory = httpClientFactory;
            _settingsService = settingsService;
        }
        
        public void Scrape()
        {
            // main scraping method
            var client = _httpClientFactory.CreateClient();
            var settings = _settingsService.GetSettings();

            Console.WriteLine("this is working.");
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
            
        private async Task<List<ResponseDtos.Job>> DeserializeResponse(HttpResponseMessage response)
        {
            var deserializedObjects = JsonConvert.DeserializeObject<ResponseDtos.Response>(await response.Content.ReadAsStringAsync())?.Results;

            if (deserializedObjects == null)
            {
                // TODO: Set the active flag to false
                // TODO: Throw some kind of an error
            }

            return deserializedObjects;
        }

        private bool EvaluateJob(Object job)
        {
            // check if job qualifies for the notification
            return false;
        }
    }
}