using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityDataUpdater.Models
{
    public class CityModel
    {
        public CityModel(string city, DateTime date, int temperatureC, string summary)
        {
            City = city;
            Date = date;
            TemperatureC = temperatureC;
            Summary = summary;
        }

        public DateTime Date { get; }
        public int TemperatureC { get; }
        public string Summary { get; }
        public string City { get; }

        public static CityModel GetSpecificModel(string cityName)
        {
            var rndTemperatureC = GetRandomTemperatureC();
            var rndSummary = GetRandomSummary();
            return new CityModel(cityName, DateTime.Now, rndTemperatureC, rndSummary);
        }

        public static IEnumerable<CityModel> GetAllCityModel()
        {
            return _cities
                .ToList()
                .Select(GetSpecificModel);
        }
        public static CityModel GetRandomCityModel()
        {
            var rndCity = GetRandomCity();
            var rndTemperatureC = GetRandomTemperatureC();
            var rndSummary = GetRandomSummary();
            return new CityModel(rndCity, DateTime.Now, rndTemperatureC, rndSummary);
        }

        private static readonly string[] _cities = {
            "Taipei",
            "Taichung",
            "Hsinchu",
            "Tainan",
            "Kaohsiung",
            "Changhua",
            "Miaoli",
            "Hualien"

        };
        private static readonly string[] _summaries = new[]
        {
            "Hot",
            "Warm",
            "Cold",
            "Cool",
            "Sunny",
            "Rainy",
            "Clear",
            "Cloudy",
            "Dry",
            "Humid",
            "Foggy",
            "Misty",
            "Gusty",
            "Windy",
            "Thunder",
            "Lightning",
        };

        private static string GetRandomCity()
        {
            var rnd = new Random((int)DateTime.Now.Ticks);
            var rndIdx = rnd.Next(0, _cities.Length - 1);
            return _cities[rndIdx];
        }
        private static int GetRandomTemperatureC()
        {
            var rnd = new Random((int)DateTime.Now.Ticks);
            return rnd.Next(0, 40);
        }
        private static string GetRandomSummary()
        {

            var rnd = new Random((int)DateTime.Now.Ticks);
            var rndIdx = rnd.Next(0, _summaries.Length - 1);
            return _summaries[rndIdx];
        }
    }
}
