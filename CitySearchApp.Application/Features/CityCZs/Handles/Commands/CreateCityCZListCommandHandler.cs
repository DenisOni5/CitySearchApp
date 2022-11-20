using AutoMapper;
using CitySearchApp.Application.Contracts.Persistance;
using CitySearchApp.Application.DTOs;
using CitySearchApp.Application.Features.CityCZs.Requests.Commands;
using CitySearchApp.Application.Responses;
using CitySearchApp.Domain;
using CitySearchApp.Persistance.Seed;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitySearchApp.Application.Features.CityCZs.Handles.Commands
{
    public class CreateCityCZListCommandHandler : IRequestHandler<CreateCityCZListCommand, BaseCommandResponse>
    {
        private readonly ICitySeed _seed;
        private readonly IMapper _mapper;

        public CreateCityCZListCommandHandler(ICitySeed seed, IMapper mapper)
        {
            _seed = seed;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateCityCZListCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            try
            {
                _seed.Seed();
                response.Success = true;
                response.Message = "Creation Successful";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors.Add(ex.Message);
            }

            return response;
        }
    }
}
