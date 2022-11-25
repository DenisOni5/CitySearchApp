using AutoMapper;
using CitySearchApp.MVC.Contracts;
using CitySearchApp.MVC.Models;
using CitySearchApp.MVC.Models.CitySearch;
using CitySearchApp.MVC.Services.Base;

namespace CitySearchApp.MVC.Services
{
    public class CityCZService : BaseHttpService, ICityCZService
    {
        private readonly IClient _httpclient;
        private readonly IMapper _mapper;

        public CityCZService(IClient httpclient, IMapper mapper) : base(httpclient)
        {
            _httpclient = httpclient;
            _mapper = mapper;
        }
        public async Task<int> GetCityCount(CityShortSearchVM search)
        {
            return await _httpclient.CountAsync(search.Kraj, search.Obec, search.PerPage, search.PageNum);
        }

        public async Task<List<string>> GetKraje()
        {
            var kraje = await _httpclient.KrajeAsync();
            return _mapper.Map<List<string>>(kraje);
        }

        public async Task<List<CityCZVM>> LoadCitiesWithParam(CityLongSearchVM search)
        {
            var cities = await _httpclient.CityCZAsync(search.Kraj, search.Obec, search.PerPage, search.PageNum);
            return _mapper.Map<List<CityCZVM>>(cities);
        }
        public async Task<List<CityCZVM>> LoadCitiesWithParam(CityCoordsSearchVM search)
        {
            var cities = await _httpclient.CoordsAsync(search.Latitude, search.Longitude, search.Distance, search.Count);
            return _mapper.Map<List<CityCZVM>>(cities);
        }

    }
}
