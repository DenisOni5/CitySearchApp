using CitySearchApp.MVC.Services.Base;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CitySearchApp.MVC.Models
{
    public class CityDisplayModelVM
    {
        public SelectList KrajeEnum { get; set; }

        public string Kraj { get; set; }

        public string? Obec { get; set; }
        public List<CityCZVM> Cities { get; set; }
        public Pagin? Paging { get; set; }
        public string TimeGenerated { get; set; }
        public string ControllerName { get; set; }
    }
}
