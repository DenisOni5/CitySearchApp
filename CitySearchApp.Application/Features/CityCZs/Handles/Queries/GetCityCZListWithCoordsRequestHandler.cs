using AutoMapper;
using CitySearchApp.Application.Contracts.Persistance;
using CitySearchApp.Application.DTOs.SearchDTOs;
using CitySearchApp.Application.DTOs;
using CitySearchApp.Application.Features.CityCZs.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitySearchApp.Application.Features.CityCZs.Handles.Queries
{
    public class GetCityCZListWithCoordsRequestHandler : IRequestHandler<GetCityCZListWithCoordsRequest, List<CityCZDto>>
    {
        private readonly ICityCZDapperRepository _repository;
        private readonly IMapper _mapper;

        public GetCityCZListWithCoordsRequestHandler(ICityCZDapperRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<CityCZDto>> Handle(GetCityCZListWithCoordsRequest request, CancellationToken cancellationToken)
        {
            return await _repository.LoadCitiesWithParam(request.parameters, request.storedprocedure);
        }
    }
}
