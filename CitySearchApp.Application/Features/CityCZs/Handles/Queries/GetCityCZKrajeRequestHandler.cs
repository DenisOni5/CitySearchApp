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
    public class GetCityCZKrajeRequestHandler : IRequestHandler<GetCityCZKrajeRequest, List<string>>
    {
        private readonly ICityCZRepository _repository;

        public GetCityCZKrajeRequestHandler(ICityCZRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<string>> Handle(GetCityCZKrajeRequest request, CancellationToken cancellationToken)
        {
            return _repository.GetKraje();            
        }
    }
}
