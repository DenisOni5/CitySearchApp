using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitySearchApp.Application.DTOs.SearchDTOs
{
    public class CityLongSearchDto : CityShortSearchDto
    {
        public int? start { get; set; }
        public int? finish { get; set; }
    }
}
