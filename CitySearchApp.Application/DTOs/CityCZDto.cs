using CitySearchApp.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitySearchApp.Application.DTOs
{
    public class CityCZDto : BaseDTO
    {
        public string Obec { get; set; }
        public string ObecCode { get; set; }
        public string Okres { get; set; }
        public string OkresCode { get; set; }
        public string Kraj { get; set; }
        public string KrajCode { get; set; }
    }
}
