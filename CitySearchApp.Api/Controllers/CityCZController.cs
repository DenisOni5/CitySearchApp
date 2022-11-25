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


        [HttpGet]
        public async Task<ActionResult<IQueryable<CityCZDto>>> Cities(CityShortSearchDto searchDto)
        {
            var cities = await _mediator.Send(new GetCityCZListRequest {  parameters = searchDto });
            return Ok(cities);
        }

        [HttpGet("Coords/")]
        public async Task<ActionResult<IQueryable<CityCZDto>>> CitiesCoords(CityCoordsSearchDto searchDto)
        {

            var citiescoords = await _mediator.Send(new GetCityCZListWithCoordsRequest
            {
                parameters = searchDto,
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
        public async Task<ActionResult<int>> CitiesCount(CityShortSearchDto searchDto)
        {
           
            var count = await _mediator.Send(new GetCityCZCountRequest { shortSearchDto = searchDto });
            return Ok(count);
        }
    }
}
