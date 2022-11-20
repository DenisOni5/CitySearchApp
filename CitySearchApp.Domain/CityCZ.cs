using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CitySearchApp.Domain.Common;

namespace CitySearchApp.Domain
{
    public class CityCZ : BaseDomainEntity
    {
        [MaxLength(50)]
        public string Obec { get; set; }
        [MaxLength(10)]
        public string ObecCode { get; set; }
        [MaxLength(50)]
        public string Okres { get; set; }
        [MaxLength(10)]
        public string OkresCode { get; set; }
        [MaxLength(50)]
        public string Kraj { get; set; }
        [MaxLength(10)]
        public string KrajCode { get; set; }

    }
}
