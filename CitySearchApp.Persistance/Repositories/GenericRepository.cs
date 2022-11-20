using CitySearchApp.Application.Contracts.Persistance;
using CitySearchApp.Application.DTOs.SearchDTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitySearchApp.Persistance.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly CitySearchAppDbContext _dbContext;

        public GenericRepository(CitySearchAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IQueryable<T> LoadData()
        {
            return _dbContext.Set<T>().AsQueryable();
        }

        public async Task<T> SaveDataAsync(T entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task SaveDataSetAsync(List<T> entities)
        {
            await _dbContext.AddRangeAsync(entities);
            await _dbContext.SaveChangesAsync();
        }
    }
}
