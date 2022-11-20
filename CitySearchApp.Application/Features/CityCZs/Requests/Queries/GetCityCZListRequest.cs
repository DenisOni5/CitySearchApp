using CitySearchApp.Application.DTOs;
using CitySearchApp.Application.DTOs.SearchDTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitySearchApp.Application.Features.CityCZs.Requests.Queries
{
    public class GetCityCZListRequest : IRequest<List<CityCZDto>>
    {
        public CityLongSearchDto parameters { get; set; }
    }
}
