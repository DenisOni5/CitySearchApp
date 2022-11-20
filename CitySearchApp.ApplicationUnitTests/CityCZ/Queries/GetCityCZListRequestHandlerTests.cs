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
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CitySearchApp.ApplicationUnitTests.CityCZ.Queries
{
    public class GetCityCZListRequestHandlerTests
    {
        private readonly IMapper _mapper;
        public GetCityCZListRequestHandlerTests()
        {
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task GetCityCZListTest()
        {
            CityLongSearchDto searchDto = new();

            var _mockRepo = MockCityCZRepository.GetCityCZRepository(searchDto);

            var hadler = new GetCityCZListRequestHandler(_mockRepo.Object, _mapper);

            var result = await hadler.Handle(new GetCityCZListRequest { parameters = searchDto }, CancellationToken.None);

            result.ShouldBeOfType<List<CityCZDto>>();

            result.Count.ShouldBe(3);
        }

        [Fact]
        public async Task GetCityCZCountTest()
        {
            CityShortSearchDto searchDto = new CityLongSearchDto();

            var _mockRepo = MockCityCZRepository.GetCityCZRepository((CityLongSearchDto)searchDto);

            var hadler = new GetCityCZCountRequestHandler(_mockRepo.Object);

            var result = await hadler.Handle(new GetCityCZCountRequest {  shortSearchDto = searchDto  }, CancellationToken.None);

            result.ShouldBe(3);
        }

        [Fact]
        public async Task GetCityCZKrajeTest()
        {
            CityLongSearchDto searchDto = new();

            var _mockRepo = MockCityCZRepository.GetCityCZRepository(searchDto);

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
