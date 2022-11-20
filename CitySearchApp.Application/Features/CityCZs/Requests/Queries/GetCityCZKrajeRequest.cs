using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitySearchApp.Application.Features.CityCZs.Requests.Queries
{
    public class GetCityCZKrajeRequest : IRequest<List<string>>
    {
    }
}
