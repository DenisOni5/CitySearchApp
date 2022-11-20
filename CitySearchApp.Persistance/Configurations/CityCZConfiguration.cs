using CitySearchApp.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitySearchApp.Persistance.Configurations
{
    public class CityCZConfiguration : IEntityTypeConfiguration<CityCZ>
    {
        public void Configure(EntityTypeBuilder<CityCZ> builder)
        {

        }
    }
}
