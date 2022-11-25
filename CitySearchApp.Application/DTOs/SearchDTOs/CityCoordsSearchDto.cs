using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitySearchApp.Application.DTOs.SearchDTOs
{
    public class CityCoordsSearchDto
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Distance { get; set; } = 20;
        public int Count { get; set; } = 10;
    }
}
