using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitySearchApp.Application.Contracts.Persistance
{
    public interface IDapperGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> LoadDataAsync<T>(string sql, string connectionId = "Default");
        Task<IEnumerable<T>> LoadDataWithParamAsync<T>(string storedprocedure, object param, string connectionId = "Default");

        Task<int> SaveDataAsync<T>(string sql, T data, string connectionId = "Default");
        Task<int> SaveDataSetAsync<T>(List<T> data, string storedprocedure, string UDTable, string connectionId = "Default");

        Task<int> GetCountAsync<T>(string storedprocedure, object paremeters, string connectionId = "Default");
    }
}
