using System.Threading.Tasks;

namespace Gruppe3.Service;

// Endret fra IPollenAPIService til IPollenApiService (bruk "Api" istedenfor "API")
public interface IPollenApiService
{
    Task ImportPollenDataAsync();
}