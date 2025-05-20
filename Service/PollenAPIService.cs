using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Gruppe3.Data;
using System.Text.Json;
using Gruppe3.Models;
using System.Linq;

namespace Gruppe3.Service
{
    public class PollenAPIService : IPollenAPIService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<PollenAPIService> _logger;
        private readonly HttpClient _httpClient;

        public PollenAPIService(AppDbContext context, ILogger<PollenAPIService> logger, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task ImportPollenDataAsync()
        {
            string apiKey = "AIzaSyCcJ3vf6FXeMkfgdGuJytfRuh6PQ_tDJ7U";
            string url = $"https://pollen.googleapis.com/v1/forecast:lookup?location.latitude=59.26754&location.longitude=10.40762&days=5&key={apiKey}";

            try
            {
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();

                var data = JsonSerializer.Deserialize<PollenApiResponse>(json);

                if (data?.dailyInfo != null)
                {
                    foreach (var day in data.dailyInfo)
                    {
                        foreach (var idx in day.indexes)
                        {
                            // Sjekk om fargen finnes fra før
                            var color = _context.ColorInfos.FirstOrDefault(c =>
                                c.Red == idx.color.red &&
                                c.Green == idx.color.green &&
                                c.Blue == idx.color.blue);

                            if (color == null)
                            {
                                color = new ColorInfo
                                {
                                    Red = idx.color.red,
                                    Green = idx.color.green,
                                    Blue = idx.color.blue
                                };
                                _context.ColorInfos.Add(color);
                                await _context.SaveChangesAsync();
                            }

                            var indexInfo = new IndexInfo
                            {
                                Code = idx.code,
                                DisplayName = idx.displayName,
                                Value = idx.value,
                                Category = idx.category,
                                IndexDescription = idx.indexDescription,
                                ColorInfoId = color.Id
                            };
                            _context.IndexInfos.Add(indexInfo);
                        }
                    }
                    await _context.SaveChangesAsync();
                }

                _logger.LogInformation("Pollen data hentet og lagret.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Feil ved henting av pollen data fra Google API.");
            }
        }
    }
}