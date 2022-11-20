using CitySearchApp.Application.Contracts.Persistance;
using CitySearchApp.Application.Features.CityCZs.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitySearchApp.Application.Features.CityCZs.Handles.Queries
{
    public class GetCityCZCountRequestHandler : IRequestHandler<GetCityCZCountRequest, int>
    {
        private readonly ICityCZRepository _repository;

        public GetCityCZCountRequestHandler(ICityCZRepository repository)
        {
            _repository = repository;
        }
        public async Task<int> Handle(GetCityCZCountRequest request, CancellationToken cancellationToken)
        {
            return _repository.GetCityCount(request.shortSearchDto);
        }
    }
}
