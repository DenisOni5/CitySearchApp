using CitySearchApp.Application.DTOs.SearchDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitySearchApp.Application.Contracts.Persistance
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> LoadData();
        Task<T> SaveDataAsync(T entity);
        Task SaveDataSetAsync(List<T> entities);
    }
}
