using AutoMapper;
using CitySearchApp.Application.DTOs;
using CitySearchApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitySearchApp.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CityCZ,CityCZDto>().ReverseMap();
            CreateMap<CitySK,CitySKDto>().ReverseMap();
        }
    }
}
