using AutoMapper;
using CitySearchApp.Application.Contracts.Persistance;
using CitySearchApp.Application.DTOs;
using CitySearchApp.Application.DTOs.SearchDTOs;
using CitySearchApp.Application.Features.CityCZs.Handles.Queries;
using CitySearchApp.Application.Features.CityCZs.Requests.Queries;
using CitySearchApp.Application.Profiles;
using CitySearchApp.ApplicationUnitTests.Mocks;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CitySearchApp.ApplicationUnitTests.CityCZ.Queries
{
    public class GetCityCZListRequestHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ICityCZRepository> _mockRepo;
        private readonly CityLongSearchDto _searchDto;
        public GetCityCZListRequestHandlerTests()
        {
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });



            _mapper = mapperConfig.CreateMapper();

            _searchDto = new CityLongSearchDto();

            _mockRepo = MockCityCZRepository.GetCityCZRepository(_searchDto);
        }

        [Fact]
        public async Task GetCityCZListTest()
        {
            var hadler = new GetCityCZListRequestHandler(_mockRepo.Object, _mapper);

            var result = await hadler.Handle(new GetCityCZListRequest { parameters = _searchDto }, CancellationToken.None);


            result.ShouldBeOfType<List<CityCZDto>>();

           // result.Count.ShouldBe(3);
        }

        [Fact]
        public async Task GetCityCZCountTest()
        {

            var hadler = new GetCityCZCountRequestHandler(_mockRepo.Object);

            var result = await hadler.Handle(new GetCityCZCountRequest { shortSearchDto = _searchDto }, CancellationToken.None);

            result.ShouldBe(3);
        }

        [Fact]
        public async Task GetCityCZKrajeTest()
        {

            var hadler = new GetCityCZKrajeRequestHandler(_mockRepo.Object);

            var result = await hadler.Handle(new GetCityCZKrajeRequest(), CancellationToken.None);

            result.Count.ShouldBe(2);

            result.ShouldBeOfType<List<string>>();

            result.ForEach(c => IsKrajTrue(c).ShouldBe(true));
        }

        public bool IsKrajTrue (string kraj)
        {
            return kraj switch
            {
                "Plzeňský kraj" => true,
                "Hlavní město Praha" => true,
                _ => false
            };
        }
    }
}
