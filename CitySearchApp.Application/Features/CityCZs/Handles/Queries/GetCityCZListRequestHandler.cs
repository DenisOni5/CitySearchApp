using AutoMapper;
using CitySearchApp.Application.Contracts.Persistance;
using CitySearchApp.Application.DTOs;
using CitySearchApp.Application.DTOs.SearchDTOs;
using CitySearchApp.Application.Features.CityCZs.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitySearchApp.Application.Features.CityCZs.Handles.Queries
{
    public class GetCityCZListRequestHandler : IRequestHandler<GetCityCZListRequest, List<CityCZDto>>
    {
        private readonly ICityCZRepository _repository;
        private readonly IMapper _mapper;

        public GetCityCZListRequestHandler(ICityCZRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<CityCZDto>> Handle(GetCityCZListRequest request, CancellationToken cancellationToken)
        {
            int page = request.parameters.PageNum;
            int perpage = request.parameters.PerPage;

            int start = page;
            int finish = perpage;

            if (start > 1)
            {
                start = (start * perpage) - perpage;
            }
            else
            {
                start = start - 1;
            };


            var parametersnew = _mapper.Map<CityLongSearchDto>(request.parameters);

            parametersnew.start = start;
            parametersnew.finish = finish;

            var citiesCz = _repository.LoadCitiesWithParam(parametersnew);
            return _mapper.Map<List<CityCZDto>>(citiesCz);
        }
    }
}
