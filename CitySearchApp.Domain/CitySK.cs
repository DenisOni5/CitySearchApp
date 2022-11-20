using CitySearchApp.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitySearchApp.Domain
{
    public class CitySK : BaseDomainEntity
    {
        [MaxLength(50)]
        public string? OBEC { get; set; }
        [MaxLength(50)]
        public string? OKRES { get; set; }
    }
}
