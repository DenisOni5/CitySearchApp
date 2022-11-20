using AutoMapper;
using CitySearchApp.Application.Contracts.Persistance;
using CitySearchApp.Application.DTOs;
using CitySearchApp.Application.DTOs.SearchDTOs;
using CitySearchApp.Domain;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CitySearchApp.Persistance.Repositories
{
    public class CityCZDapperRepository : DapperGenericRepository<CityCZ>, ICityCZDapperRepository
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _conf;

        public CityCZDapperRepository(IConfiguration conf, IMapper mapper) : base(conf)
        {
            _conf = conf;
            _mapper = mapper;
        }
        public async Task<int> PopulateDatabaseFromCSV<T>(string sourcefile, string storedprocedure, string udttable, string connectionId = ConnectionString.Name)
        {
            var path = $@"Data\{sourcefile}";

            var _data = Parser.ReadInCSV<T>(path);

            return await SaveDataSetAsync(_data, storedprocedure, udttable);
        }
        public async Task<List<string>> LoadKrajsEnum()
        {
            string sql = "SELECT DISTINCT Kraj FROM dbo.CitiesCZ;";

            var kraje = _mapper.Map<List<string>>(await LoadDataAsync<CityCZ>(sql));

            return kraje;
        }
        public async Task<List<CityCZDto>> LoadCitiesWithParam(object parameters, string storedprocedure)
        {
            var cities = _mapper.Map<List<CityCZDto>>(await LoadDataWithParamAsync<CityCZ>(storedprocedure, parameters));

            return cities;
        }
        public async Task<int> GetCityCount(object parameters)
        {
            return await GetCountAsync<CityCZDto>(nameof(dbo.GetCityCount), parameters);
        }

        public async void CollateCityDiacriticsAsync(string connectionId = ConnectionString.Name)
        {
            using IDbConnection conn = new SqlConnection(_conf.GetConnectionString(connectionId));

            await conn.ExecuteAsync(nameof(dbo.AlterTableCity), commandType: CommandType.StoredProcedure);
        }

        public enum dbo
        {
            AlterTableCity,
            GetCities,
            GetCityCount,
            InsertCitiesSet,
            CityUDT,
            GetNearestCity
        }

        public void ExecuteSqlFile(string connectionId = ConnectionString.Name)
        {

            using IDbConnection conn = new SqlConnection(_conf.GetConnectionString(connectionId));

            conn.Execute($"DROP PROCEDURE IF EXISTS dbo.{nameof(dbo.AlterTableCity)};");
            conn.Execute($"DROP PROCEDURE IF EXISTS dbo.{nameof(dbo.GetCities)};");
            conn.Execute($"DROP PROCEDURE IF EXISTS dbo.{nameof(dbo.GetCityCount)};");
            conn.Execute($"DROP PROCEDURE IF EXISTS dbo.{nameof(dbo.InsertCitiesSet)};");
            conn.Execute($"DROP PROCEDURE IF EXISTS dbo.{nameof(dbo.GetNearestCity)};");
            conn.Execute($"DROP TYPE IF EXISTS dbo.{nameof(dbo.CityUDT)};");


            conn.Execute($"CREATE TYPE dbo.{nameof(dbo.CityUDT)} AS TABLE (" +
                                    "[Obec]      NVARCHAR (50) NULL," +
                                    "[ObecCode]  NVARCHAR (10) NULL," +
                                    "[Okres]     NVARCHAR (50) NULL," +
                                    "[OkresCode] NVARCHAR (10) NULL," +
                                    "[Kraj]      NVARCHAR (50) NULL," +
                                    "[KrajCode]  NVARCHAR (10) NULL," +
                                    "[PSC]       NVARCHAR (10) NULL," +
                                    "[Latitude]  FLOAT (53)    NULL," +
                                    "[Longitude] FLOAT (53)    NULL);");

            conn.Execute($"CREATE  PROCEDURE dbo.{nameof(dbo.AlterTableCity)} " +
                                    "AS " +
                                    "ALTER TABLE CitiesCZ ALTER COLUMN Obec nvarchar(50) " +
                                    "COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI NULL; ALTER TABLE City ALTER COLUMN Okres nvarchar(50) " +
                                    "COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI NULL; ALTER TABLE City ALTER COLUMN Kraj nvarchar(50) " +
                                    "COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI NULL;");


            conn.Execute($"CREATE PROCEDURE dbo.{nameof(dbo.GetCities)} " +
                                    "@start int = 1, " +
                                    "@finish int, " +
                                    "@Obec nvarchar(50) = NULL, " +
                                    "@Kraj nvarchar(50) = NULL " +
                                    "AS " +
                                    "SELECT * " +
                                    "FROM(SELECT    ROW_NUMBER() OVER(ORDER BY Obec) AS RowNum, * " +
                                              "FROM      dbo.CitiesCZ " +
                                              "WHERE " +
                                               "(@Obec IS NULL OR(Obec COLLATE Latin1_General_CI_AI like @Obec + '%')) " +

                                              "AND(@Kraj IS NULL OR(Kraj = @Kraj)) " +
                                            ") AS RowConstrainedResult " +
                                    "WHERE   RowNum >= @start " +
                                        "AND RowNum < @finish " +
                                    "ORDER BY RowNum;");


            conn.Execute($"CREATE PROCEDURE dbo.{nameof(dbo.GetCityCount)} " +
                                    "@Obec nvarchar(50) = NULL, " +
                                    "@Kraj nvarchar(50) = NULL " +
                                    "AS " +
                                    "SELECT COUNT(*) FROM dbo.CitiesCZ " +
                                              "WHERE " +
                                               "(@Obec IS NULL OR(Obec COLLATE Latin1_General_CI_AI like @Obec + '%')) " +
                                              "AND(@Kraj IS NULL OR(Kraj = @Kraj))");


            conn.Execute($"CREATE PROCEDURE dbo.{nameof(dbo.InsertCitiesSet)} " +
                                    "@elements CityUDT readonly " +
                                    "AS " +
                                    "BEGIN " +

                                        "INSERT INTO dbo.CitiesCZ(Obec, ObecCode, Okres, OkresCode, Kraj, KrajCode, PSC, Latitude, Longitude) " +

                                        "SELECT[Obec], [ObecCode], [Okres], [OkresCode], [Kraj], [KrajCode], [PSC], [Latitude], [Longitude] " +
                                                "FROM @elements; " +
                                    "END");

            conn.Execute($"CREATE PROCEDURE dbo.{nameof(dbo.GetNearestCity)} " +
                        "@Longitude Varchar(50)," +
                        "@Latitude Varchar(50)," +
                        "@Distance Varchar(50)," +
                        "@Count int " +
                        "AS " +
                        "\n" +
                        "declare @point geography;" +
                        "set @point = geography::Point(@Latitude, @Longitude, 4326);" +
                        "SELECT TOP (@Count) *, @point.STDistance(geography::Point(Latitude, Longitude,4326)) / 1000 as distance from CitiesCZ where CAST(@point.STDistance(geography::Point(Latitude, Longitude,4326)) / 1000 as int) <= @distance order by distance ASC;");

        }
    }
}
