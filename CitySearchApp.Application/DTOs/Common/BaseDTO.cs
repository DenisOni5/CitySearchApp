using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitySearchApp.Application.DTOs.Common
{
    public abstract class BaseDTO
    {
        public int Id { get; set; }
        public string PSC { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int distance { get; set; } = 0;
    }
}
