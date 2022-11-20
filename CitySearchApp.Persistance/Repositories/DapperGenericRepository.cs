using CitySearchApp.Application.Contracts.Persistance;
using CitySearchApp.Persistance.Configurations;
using CitySearchApp.Persistance.Services;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitySearchApp.Persistance.Repositories
{
    public class DapperGenericRepository<T> : IDapperGenericRepository<T> where T : class
    {
        private readonly IConfiguration _conf;
        private const string connectionStringName = "CitySearchAppConnectionString";

        public DapperGenericRepository(IConfiguration conf)
        {
            _conf = conf;
        }

        public async Task<IEnumerable<T>> LoadDataAsync<T>(string sql, string connectionId = ConnectionString.Name)
        {
            using IDbConnection conn = new SqlConnection(_conf.GetConnectionString(connectionId));
            return await conn.QueryAsync<T>(sql);

        }
        public async Task<IEnumerable<T>> LoadDataWithParamAsync<T>(string storedprocedure, object param, string connectionId = ConnectionString.Name)
        {
            using IDbConnection conn = new SqlConnection(_conf.GetConnectionString(connectionId));
            return await conn.QueryAsync<T>(storedprocedure, param, commandType: CommandType.StoredProcedure);

        }

        public async Task<int> SaveDataAsync<T>(string sql, T data, string connectionId = ConnectionString.Name)
        {
            using IDbConnection conn = new SqlConnection(_conf.GetConnectionString(connectionId));
            return await conn.ExecuteAsync(sql, data);

        }
        public async Task<int> SaveDataSetAsync<T>(List<T> data, string storedprocedure, string UDTable, string connectionId = ConnectionString.Name)
        {
            var datatable = DataTableClass.ToDataTable<T>(data);
            var c = new
            {
                elements = datatable.AsTableValuedParameter(UDTable)
            };
            using IDbConnection conn = new SqlConnection(_conf.GetConnectionString(connectionId));
            var result = 0;
            try
            {
                result = await conn.ExecuteAsync(storedprocedure, c, commandType: CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {
                Console.Write(ex.Message.ToString());
                throw;
            }
            return result;
        }

        public async Task<int> GetCountAsync<T>(string storedprocedure, object parameters, string connectionId = ConnectionString.Name)
        {
            using IDbConnection conn = new SqlConnection(_conf.GetConnectionString(connectionId));
            return await conn.QueryFirstAsync<int>(storedprocedure, parameters, commandType: CommandType.StoredProcedure);
        }

    }
}
