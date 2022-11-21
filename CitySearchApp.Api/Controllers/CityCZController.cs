using CitySearchApp.Application.DTOs;
using CitySearchApp.Application.DTOs.SearchDTOs;
using CitySearchApp.Application.Features.CityCZs.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static CitySearchApp.Persistance.Repositories.CityCZDapperRepository;

namespace CitySearchApp.Api.Controllers
{
    [Route("api/[controller]")]
    public class CityCZController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CityCZController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("{pageNum}/")]
        [HttpGet("{pageNum}/{Kraj}")]
        [HttpGet("{pageNum}/{Obec}/ObecList")]
        [HttpGet("{pageNum}/{Kraj}/{Obec}/ObecList")]
        public async Task<ActionResult<IQueryable<CityCZDto>>> Cities(int pageNum = 1, string? Obec = null, string? Kraj = null)
        {
            CityLongSearchDto parameters = new()
            {
                Kraj = Kraj,
                Obec = Obec,
                PageNum = pageNum,
                PerPage = 10
            };

            var cities = await _mediator.Send(new GetCityCZListRequest {  parameters = parameters });
            return Ok(cities);
        }

        [HttpGet("latitude={latitude}/longitude={longitude}")]
        public async Task<ActionResult<IQueryable<CityCZDto>>> CitiesCoords(double latitude, double longitude)
        {
            CityCoordsSearchDto parametersfloat = new()
            {
                Latitude = latitude,
                Longitude = longitude,
                Distance = 20,
                Count = 10
            };

            var citiescoords = await _mediator.Send(new GetCityCZListWithCoordsRequest
            {
                parameters = parametersfloat,
                storedprocedure = "dbo." + nameof(dbo.GetNearestCity)
            });
            return Ok(citiescoords);
        }


        [HttpGet("Kraje/")]
        public async Task<ActionResult<IQueryable<string>>> Kraje()
        {
            var kraje = await _mediator.Send(new GetCityCZKrajeRequest());
            return Ok(kraje);
        }

        [HttpGet("Count/")]
        [HttpGet("Count/{Kraj}")]
        [HttpGet("Count/{Obec}/ObecList")]
        public async Task<ActionResult<int>> CitiesCount(string? Obec = null, string? Kraj = null)
        {
            CityShortSearchDto parameters = new()
            {
                Kraj = Kraj,
                Obec = Obec
            };
           
            var count = await _mediator.Send(new GetCityCZCountRequest { shortSearchDto = parameters });
            return Ok(count);
        }
    }
}
