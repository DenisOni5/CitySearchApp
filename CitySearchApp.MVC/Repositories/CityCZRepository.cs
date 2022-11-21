using CitySearchApp.MVC.Models.CitySearch;
using CitySearchApp.MVC.Models;
using CitySearchApp.MVC.Services.Base;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using System.Reflection;
using CitySearchApp.MVC.Contracts;
using AutoMapper;
using static CitySearchApp.Persistance.Repositories.CityCZDapperRepository;
using CitySearchApp.Application.DTOs.SearchDTOs;
using Azure.Core;

namespace CitySearchApp.MVC.Repositories
{
    public class CityCZRepository : ICityCZRepository
    {
        private readonly ICityCZService _service;
        private readonly IMapper _mapper;

        public CityCZRepository(ICityCZService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        public async Task<CityDisplayModelVM> GetCityDisplayModel(CityCoordsSearchVM parameters, string controllerName)
        {

            string Kraj = "";
            var timer = new Stopwatch();
            timer.Start();

            var cities = await _service.LoadCitiesWithParam(parameters);

            var KrajeTextEnum = (await _service.GetKraje()).OrderBy(a => a);
            var KrajeEnum = new List<SelectListItem>();

            foreach (var item in KrajeTextEnum)
            {
                KrajeEnum.Add(new SelectListItem() { Text = item, Value = System.Net.WebUtility.UrlEncode(item) });
            }
            var KrajeEnumList = new SelectList(KrajeEnum, "Value", "Text", Kraj);

            timer.Stop();
            TimeSpan timeTaken = timer.Elapsed;
            string timeGenerated = timeTaken.ToString(@"m\:ss\.fff");

            CityDisplayModelVM cityDisplayModel = new()
            {
                Cities = cities,
                Paging = null,
                KrajeEnum = KrajeEnumList,
                TimeGenerated = timeGenerated,
                ControllerName = controllerName,
            };

            return cityDisplayModel;
        }

        public async Task<CityDisplayModelVM> GetCityDisplayModel(CityShortSearchVM parameters, int Page, int Perpage, string controllerName)
        {
            string extraQstringToAdd = "";
            string? Kraj = parameters.Kraj;
            string? Obec = parameters.Obec;

            if (Kraj is not null)
            {
                if (Kraj != "")
                {
                    string KrajEncoded = new string(System.Net.WebUtility.UrlEncode(Kraj));
                    extraQstringToAdd += $"/{KrajEncoded}";
                    Kraj = System.Net.WebUtility.UrlDecode(Kraj);
                    parameters.Kraj = Kraj;
                }
            }
            if (Obec is not null)
            {
                if (Obec != "")
                {
                    string ObecEncoded = new string(System.Net.WebUtility.UrlEncode(Obec));
                    extraQstringToAdd += $"/{ObecEncoded}/ObecList";
                    Obec = System.Net.WebUtility.UrlDecode(Obec);
                    parameters.Obec = Obec;
                }
            }
            
            parameters.PageNum = Page;
            parameters.PerPage = Perpage;

            int start = Page;
            int finish = Perpage;

            if (start > 1)
            {
                start = (start * Perpage) - Perpage;
            }
            else
            {
                start = start - 1;
            };


            var parametersnew = _mapper.Map<CityLongSearchVM>(parameters);

            parametersnew.start = start;
            parametersnew.finish = finish;

            var timer = new Stopwatch();
            timer.Start();

            int maxcount = await _service.GetCityCount(parameters);

            var cities = await _service.LoadCitiesWithParam(parametersnew);
            var paging = Paging.GetPagingDone(Page, maxcount, Perpage, "/Cities/", extraQstringToAdd);

            var KrajeTextEnum = (await _service.GetKraje()).OrderBy(a => a);
            var KrajeEnum = new List<SelectListItem>();

            foreach (var item in KrajeTextEnum)
            {
                KrajeEnum.Add(new SelectListItem() { Text = item, Value = System.Net.WebUtility.UrlEncode(item) });
            }
            var KrajeEnumList = new SelectList(KrajeEnum, "Value", "Text", Kraj);

            timer.Stop();
            TimeSpan timeTaken = timer.Elapsed;
            string timeGenerated = timeTaken.ToString(@"m\:ss\.fff");

            CityDisplayModelVM cityDisplayModel = new()
            {
                Cities = cities,
                Paging = paging,
                KrajeEnum = KrajeEnumList,
                TimeGenerated = timeGenerated,
                ControllerName = controllerName,
                Obec = parameters.Obec

            };
            return cityDisplayModel;
        }

    }
}
