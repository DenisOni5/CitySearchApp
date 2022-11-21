using CitySearchApp.MVC.Models;
using CitySearchApp.MVC.Models.CitySearch;

namespace CitySearchApp.MVC.Contracts
{
    public interface ICityCZService
    {
        Task<int> GetCityCount(CityShortSearchVM search);
        Task<List<string>> GetKraje();
        Task<List<CityCZVM>> LoadCitiesWithParam(CityLongSearchVM search);
        Task<List<CityCZVM>> LoadCitiesWithParam(CityCoordsSearchVM search);
    }
}
