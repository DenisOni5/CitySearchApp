using CitySearchApp.Application.Contracts.Persistance;
using CitySearchApp.Application.DTOs;
using CitySearchApp.Domain;
using CitySearchApp.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CitySearchApp.Persistance.Repositories.CityCZDapperRepository;

namespace CitySearchApp.Persistance.Seed
{
    public class CitySeed : ICitySeed
    {
        private readonly ICityCZDapperRepository _repository;
        private readonly ILogger<CitySeed> _logger;

        public CitySeed(ICityCZDapperRepository repository, ILogger<CitySeed> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public async void Seed()
        {
            try
            {

                if (_repository.LoadKrajsEnum().Result.Any() == false)
                {
                    _repository.ExecuteSqlFile();
                    await _repository.PopulateDatabaseFromCSV<CreateCityCZDto>("cities_souradnice.csv", "dbo." + nameof(dbo.InsertCitiesSet), nameof(dbo.CityUDT));
                    
                    //_cityProcessor.CollateCityDiacriticsAsync();

                }
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogError("Populating cities error: " + ex.Message);
                // Check ex.CancellationToken.IsCancellationRequested here.
                // If false, it's pretty safe to assume it was a timeout.
            }
            
        }
    }
}
