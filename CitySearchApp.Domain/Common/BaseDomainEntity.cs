using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitySearchApp.Domain.Common
{
    public abstract class BaseDomainEntity
    {
        public int Id { get; set; }

        [MaxLength(10)]
        public string? PSC { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        [NotMapped]
        public int distance { get; set; }
    }
}
