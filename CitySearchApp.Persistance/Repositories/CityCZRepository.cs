using AutoMapper;
using CitySearchApp.Application.Contracts.Persistance;
using CitySearchApp.Application.DTOs.SearchDTOs;
using CitySearchApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitySearchApp.Persistance.Repositories
{
    public class CityCZRepository : GenericRepository<CityCZ>, ICityCZRepository
    {
        private readonly CitySearchAppDbContext _dbContext;
        private readonly IMapper _mapper;

        public CityCZRepository(CitySearchAppDbContext dbContext, IMapper mapper) : base(dbContext)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public int GetCityCount(CityShortSearchDto search)
        {
            IQueryable<CityCZ> cities = LoadData()
                .Where(x => (x.Kraj == search.Kraj || search.Kraj == null) && (x.Obec.StartsWith(search.Obec) || search.Obec == null))
                .AsQueryable();
            return cities.Count();
        }

        public List<string> GetKraje()
        {
            IQueryable<string> krajeq = LoadData().GroupBy(x => x.Kraj).Select(x => x.Key).AsQueryable();
            var kraje = _mapper.Map<List<string>>(krajeq.ToList());
            return kraje;
        }

        public List<CityCZ> LoadCitiesWithParam(CityLongSearchDto search)
        {
            IQueryable<CityCZ> cities = LoadData().OrderBy(x => x.Obec)
                .Where(x => (x.Kraj == search.Kraj || search.Kraj == null) && (x.Obec.StartsWith(search.Obec) || search.Obec == null))
                .Skip(search.start.Value).Take(search.finish.Value).AsQueryable();

            return cities.ToList();
        }
    }
}
