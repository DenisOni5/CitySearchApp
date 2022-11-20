using CitySearchApp.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitySearchApp.Application.Contracts.Persistance
{
    public interface ICityCZDapperRepository
    {
        Task<List<string>> LoadKrajsEnum();
        Task<List<CityCZDto>> LoadCitiesWithParam(object parameters, string storedprocedure);
        Task<int> PopulateDatabaseFromCSV<T>(string sourcefile, string storedprocedure, string udttable, string connectionId = "CitySearchAppConnectionString");
        void ExecuteSqlFile(string connectionId = "CitySearchAppConnectionString");
    }
}
