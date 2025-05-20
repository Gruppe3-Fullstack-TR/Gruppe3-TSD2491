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

                // Logg JSON for feilsøking
                _logger.LogInformation("API response: " + json);

                var data = JsonSerializer.Deserialize<PollenApiResponse>(json);

                if (data?.DailyInfo != null)
                {
                    foreach (var day in data.DailyInfo)
                    {
                        if (day?.PollenTypeInfo == null)
                        {
                            _logger.LogWarning("Ingen pollenTypeInfo for dag: {@day}", day);
                            continue;
                        }

                        foreach (var type in day.PollenTypeInfo)
                        {
                            var idx = type.IndexInfo;
                            if (idx == null || idx.Color == null)
                            {
                                _logger.LogWarning("Null indexInfo eller color for pollenType: {@type}", type);
                                continue;
                            }

                            // Fargene i API-et er float mellom 0 og 1, konverter til int 0-255
                            int r = idx.Color.Red.HasValue ? (int)(idx.Color.Red.Value * 255) : 0;
                            int g = idx.Color.Green.HasValue ? (int)(idx.Color.Green.Value * 255) : 0;
                            int b = idx.Color.Blue.HasValue ? (int)(idx.Color.Blue.Value * 255) : 0;

                            var color = _context.ColorInfos.FirstOrDefault(c =>
                                c.Red == r &&
                                c.Green == g &&
                                c.Blue == b);

                            if (color == null)
                            {
                                color = new ColorInfo
                                {
                                    Red = r,
                                    Green = g,
                                    Blue = b
                                };
                                _context.ColorInfos.Add(color);
                                await _context.SaveChangesAsync();
                            }

                            var indexInfo = new IndexInfo
                            {
                                Code = idx.Code,
                                DisplayName = idx.DisplayName,
                                Value = idx.Value,
                                Category = idx.Category,
                                IndexDescription = idx.IndexDescription,
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