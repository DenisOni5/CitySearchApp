using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitySearchApp.Application.DTOs.SearchDTOs
{
    public class CityShortSearchDto
    {
        private string? obec;

        public string? Kraj { get; set; }
        public string? Obec { get; set; }
        public int PerPage { get; set; } = 10;
        public int PageNum { get; set; } = 1;
    }
}
