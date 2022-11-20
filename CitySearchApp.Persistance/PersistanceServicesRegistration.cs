using CitySearchApp.Application.Contracts.Persistance;
using CitySearchApp.Persistance.Configurations;
using CitySearchApp.Persistance.Repositories;
using CitySearchApp.Persistance.Seed;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitySearchApp.Persistance
{
    public static class PersistanceServicesRegistration
    {
        public static IServiceCollection ConfigurePersistanceServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionStringObj = configuration.GetConnectionString(ConnectionString.Name);

            services.AddDbContext<CitySearchAppDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString(ConnectionString.Name)
                    )
            );
            services.AddTransient<IDbConnection>((sp) => new SqlConnection(ConnectionString.Name));

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddScoped<ICityCZRepository, CityCZRepository>();
            services.AddSingleton<ICityCZDapperRepository, CityCZDapperRepository>();
            services.AddSingleton<ICitySeed, CitySeed>();


            return services;
        }
    }
}
