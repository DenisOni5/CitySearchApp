using CitySearchApp.Application.DTOs.SearchDTOs;
using CitySearchApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitySearchApp.Application.Contracts.Persistance
{
    public interface ICityCZRepository : IGenericRepository<CityCZ>
    {
        public Task<List<string>> GetKraje();
        public Task<List<CityCZ>> LoadCitiesWithParam(CityLongSearchDto search);
        public Task<int> GetCityCount(CityShortSearchDto search);
    }
}
