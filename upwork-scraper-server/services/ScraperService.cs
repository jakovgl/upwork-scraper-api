using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using upwork_scraper_server.dtos;
using upwork_scraper_server.models;

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
        
        public async void Scrape()
        {
            // main scraping method
            var client = _httpClientFactory.CreateClient();
            var settings = _settingsService.GetSettings();

            if (!settings.Active)
                return;
            
            Console.WriteLine("Its active");
            var response = await SendRequestAsync(client, settings.Cookie);
            var jobs = await DeserializeResponse(response);
            
            jobs.ForEach(job =>
            {
                if (EvaluateJob(job, settings))
                {
                    // TODO: Send Telegram message
                    // TODO: Save to DB
                }
            });
        }

        private async Task<HttpResponseMessage> SendRequestAsync(HttpClient client, string cookie)
        {
            // send upwork request
            var requestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://www.upwork.com/ab/find-work/api/feeds/embeddings-recommendations"),
                Headers =
                {
                    { "cookie", cookie }, 
                    { "x-requested-with", "XMLHttpRequest" }
                }
            };

            return await client.SendAsync(requestMessage);
        }
            
        private async Task<List<ResponseDtos.Job>> DeserializeResponse(HttpResponseMessage response)
        {
            List<ResponseDtos.Job> deserializedObjects = null;
            try
            {
                deserializedObjects = JsonConvert.DeserializeObject<ResponseDtos.Response>(await response.Content.ReadAsStringAsync())?.Results;
            }
            catch (Exception e)
            {
                _settingsService.SetActive(false);
                // TODO: Send some kind of Telegram message                
            }

            if (deserializedObjects == null)
            {
                _settingsService.SetActive(false);
                // TODO: Send some kind of Telegram message
            }

            return deserializedObjects;
        }

        // check if job qualifies for the notification
        private bool EvaluateJob(ResponseDtos.Job job, Settings settings)
        {
            if (job.ShortEngagement.Equals(settings.Engagement))
            {
                return true;
            }
            return false;
        }
    }
}