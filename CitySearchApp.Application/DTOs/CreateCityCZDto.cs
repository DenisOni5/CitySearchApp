using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitySearchApp.Application.DTOs
{
    public class CreateCityCZDto
    {
        public string Obec { get; set; }
        public string ObecCode { get; set; }
        public string Okres { get; set; }
        public string OkresCode { get; set; }
        public string Kraj { get; set; }
        public string KrajCode { get; set; }
        public string PSC { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
