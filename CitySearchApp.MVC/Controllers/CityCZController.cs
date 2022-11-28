using CitySearchApp.MVC.Models.CitySearch;
using CitySearchApp.MVC.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CitySearchApp.MVC.Controllers
{
    public class CityCZController : Controller
    {
        private readonly ICityCZRepository _repository;

        public CityCZController(ICityCZRepository repository)
        {
            _repository = repository;
        }
        [Route("/Cities/{pageNum?}/")]
        [Route("/Cities/{pageNum?}/{Kraj?}")]
        [Route("/Cities/{pageNum?}/{Obec?}/ObecList")]
        [Route("/Cities/{pageNum?}/{Kraj?}/{Obec?}/ObecList")]
        public async Task<ViewResult> Cities(int pageNum = 1, string? Obec = null, string? Kraj = null)
        {
            CityShortSearchVM parameters = new()
            {
                Kraj = Kraj,
                Obec = Obec,
            };
            int perpage = 10;

            return View(await _repository.GetCityDisplayModel(parameters, pageNum, perpage, nameof(Cities)));
        }
        [Route("/CitiesCoords")]
        [Route("/CitiesCoords/latitude={latitude}/longitude={longitude}")]
        public async Task<ViewResult> CitiesCoords(double latitude, double longitude)
        {
            CityCoordsSearchVM parametersfloat = new()
            {
                Latitude = latitude,
                Longitude = longitude,
                Distance = 20,
                Count = 10
            };
            return View(nameof(Cities), await _repository.GetCityDisplayModel(parametersfloat, nameof(CitiesCoords)));
        }
    }
}
