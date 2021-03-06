using FluentScheduler;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using upwork_scraper_server.services;

namespace upwork_scraper_server
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient();

            services.AddScoped<SettingsService>();
            services.AddScoped<ScraperService>();
            services.AddSingleton<TelegramService>();

            services.AddControllers();
            services.AddHttpContextAccessor();

            var serviceProvider = services.BuildServiceProvider();
            var scrapingService = serviceProvider.GetService<ScraperService>();
            
            JobManager.Initialize();
            JobManager.AddJob(() => scrapingService.Scrape(), s => s.ToRunNow().AndEvery(2).Seconds());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            
            app.UseRouting();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context => { await context.Response.WriteAsync("upwork-scraper-server"); });
            });
        }
    }
}