using AutoMapper;
using CitySearchApp.Application.DTOs.SearchDTOs;
using CitySearchApp.MVC.Models;
using CitySearchApp.MVC.Models.CitySearch;
using CitySearchApp.MVC.Services.Base;

namespace CitySearchApp.MVC
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CityCZDto, CityCZVM>().ReverseMap();
            CreateMap<CityShortSearchDto, CityShortSearchVM>().ReverseMap();
            CreateMap<CityLongSearchDto, CityLongSearchVM>().ReverseMap();
            CreateMap<CityShortSearchVM, CityLongSearchVM>().ReverseMap();
        }
    }
}
