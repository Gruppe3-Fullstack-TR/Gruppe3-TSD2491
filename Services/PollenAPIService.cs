using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Gruppe3.Data;
using System.Text.Json;
using Gruppe3.Models;
using System.Linq;

namespace Gruppe3.Service
{
    public class PollenApiService : IPollenAPIService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<PollenApiService> _logger;
        private readonly HttpClient _httpClient;

        public PollenApiService(AppDbContext context, ILogger<PollenApiService> logger, IHttpClientFactory httpClientFactory)
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

                if (data?.DailyInfo != null)
                {
                    foreach (var day in data.DailyInfo)
                    {
                        if (day.PollenTypeInfo != null)
                        {
                            foreach (var typeInfo in day.PollenTypeInfo)
                            {
                                var idx = typeInfo.IndexInfo;
                                if (idx == null) continue;
                                // Sjekk om fargen finnes fra før
                                var color = _context.ColorInfos.FirstOrDefault(c =>
                                    c.Red == idx.Color.Red &&
                                    c.Green == idx.Color.Green &&
                                    c.Blue == idx.Color.Blue);

                                if (color == null)
                                {
                                    color = new ColorInfo
                                    {
                                        Red = idx.Color != null ? idx.Color.Red ?? 0f : 0f,
                                        Green = idx.Color != null ? idx.Color.Green ?? 0f : 0f,
                                        Blue = idx.Color != null ? idx.Color.Blue ?? 0f : 0f
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