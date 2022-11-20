using CitySearchApp.Application.DTOs;
using CitySearchApp.Application.DTOs.SearchDTOs;
using CitySearchApp.Application.Features.CityCZs.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> Cities(int pageNum = 1, string? Obec = null, string? Kraj = null)
        {
            CityLongSearchDto parameters = new()
            {
                Kraj = Kraj,
                Obec = Obec,
                PageNum = pageNum
            };

            var cities = await _mediator.Send(new GetCityCZListRequest {  parameters = parameters });
            return Ok(cities);
        }
    }
}
