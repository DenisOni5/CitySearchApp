using CitySearchApp.Application.DTOs.SearchDTOs;
using CitySearchApp.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitySearchApp.Application.Features.CityCZs.Requests.Queries
{
    public class GetCityCZListWithCoordsRequest : IRequest<List<CityCZDto>>
    {
        public string storedprocedure { get; set; }
        public CityCoordsSearchDto parameters { get; set; }
    }
}
