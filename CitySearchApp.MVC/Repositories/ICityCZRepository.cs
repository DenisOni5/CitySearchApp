using CitySearchApp.MVC.Models;
using CitySearchApp.MVC.Models.CitySearch;

namespace CitySearchApp.MVC.Repositories
{
    public interface ICityCZRepository
    {
        Task<CityDisplayModelVM> GetCityDisplayModel(CityCoordsSearchVM parameters, string controllerName);
        Task<CityDisplayModelVM> GetCityDisplayModel(CityShortSearchVM parameters, int Page, int Perpage, string controllerName);
    }
}