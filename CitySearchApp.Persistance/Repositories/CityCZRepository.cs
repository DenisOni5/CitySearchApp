using AutoMapper;
using CitySearchApp.Application.Contracts.Persistance;
using CitySearchApp.Application.DTOs.SearchDTOs;
using CitySearchApp.Domain;
using Microsoft.EntityFrameworkCore;
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

        public async Task<int> GetCityCount(CityShortSearchDto search)
        {
            return await LoadData()
                .Where(x => (x.Kraj == search.Kraj || search.Kraj == null) && (x.Obec.StartsWith(search.Obec) || search.Obec == null)).CountAsync();
        }

        public async Task<List<string>> GetKraje()
        {
            var kraje = _mapper.Map<List<string>>(await LoadData().GroupBy(x => x.Kraj).Select(x => x.Key).ToListAsync());
            return kraje;
        }

        public async Task<List<CityCZ>> LoadCitiesWithParam(CityLongSearchDto search)
        {

            return await LoadData().OrderBy(x => x.Obec)
                .Where(x => (x.Kraj == search.Kraj || search.Kraj == null) && (x.Obec.StartsWith(search.Obec) || search.Obec == null))
                .Skip(search.start.Value).Take(search.finish.Value).ToListAsync();
        }
    }
}
