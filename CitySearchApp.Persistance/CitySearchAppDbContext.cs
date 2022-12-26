using CitySearchApp.Domain;
using CitySearchApp.Persistance.Configurations;
using CitySearchApp.Persistance.Seed;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitySearchApp.Persistance
{
    public class CitySearchAppDbContext : DbContext
    {
        public CitySearchAppDbContext(DbContextOptions<CitySearchAppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CityCZConfiguration());
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CitySearchAppDbContext).Assembly);
        }

        public DbSet<CityCZ> CitiesCZ { get; set; }
    }
}
