using CitySearchApp.Application.Contracts.Persistance;
using CitySearchApp.Application.DTOs.SearchDTOs;
using CitySearchApp.Domain;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace CitySearchApp.ApplicationUnitTests.Mocks
{
    public static class MockCityCZRepository
    {
        public static Mock<ICityCZRepository> GetCityCZRepository()
        {
            var cities = new List<CitySearchApp.Domain.CityCZ>
            {
                new CitySearchApp.Domain.CityCZ
                {
                     Obec = "Bělá nad Radbuzou",
                     ObecCode = "553441",
                     Okres = "Domažlice",
                     OkresCode = "CZ0321",
                     Kraj = "Plzeňský kraj",
                     KrajCode = "CZ032",
                     PSC = "34526",
                     Latitude = 49.591261,
                     Longitude = 12.717718
                },
                new CitySearchApp.Domain.CityCZ
                {
                     Obec = "Chotěšov",
                     ObecCode = "557838",
                     Okres = "Plzeň-jih",
                     OkresCode = "CZ0324",
                     Kraj = "Plzeňský kraj",
                     KrajCode = "CZ032",
                     PSC = "33214",
                     Latitude = 49.654190,
                     Longitude = 13.202822
                },
                new CitySearchApp.Domain.CityCZ
                {
                     Obec = "Praha",
                     ObecCode = "554782",
                     Okres = "Praha",
                     OkresCode = "CZ0100",
                     Kraj = "Hlavní město Praha",
                     KrajCode = "CZ010",
                     PSC = "11000",
                     Latitude = 50.075638,
                     Longitude = 14.437900
                }
            };

            var mockRepo = new Mock<ICityCZRepository>();

            mockRepo.Setup(r => r.LoadCitiesWithParam(It.IsAny<CityLongSearchDto>())).Returns(cities);

            mockRepo.Setup(r => r.GetCityCount(It.IsAny<CityShortSearchDto>())).Returns(cities.Count);

            mockRepo.Setup(r => r.GetKraje()).Returns(cities.Select(c=>c.Kraj).Distinct().ToList());

            return mockRepo;
        }
    }
}
