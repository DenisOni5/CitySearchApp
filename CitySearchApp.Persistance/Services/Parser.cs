using CsvHelper.Configuration;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitySearchApp.Domain.Common;

namespace CitySearchApp.Persistance.Services
{
    public static class Parser
    {
        public static List<T> ReadInCSV<T>(string absolutePath)
        {
            List<T> output = new List<T>();
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            { 
                HeaderValidated = null,
                MissingFieldFound = null
            };
            using (var reader = new StreamReader(absolutePath))
            using (var csv = new CsvReader(reader, config))
            {
                output = csv.GetRecords<T>().ToList();
            }

            //for (int i = 0; i < output.Count; i++)
            //{
            //    output[i].Id = i+1;
            //}
            return output;
        }

    }
}
