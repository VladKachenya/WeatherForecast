using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherForecast.Contracts;
using WeatherForecast.DataAccess.Entities;
using WeatherForecast.DataAccess.Factories;

namespace WeatherForecast.Service
{
    public class WeatherForecastProvider : IWeatherForecastContract
    {
        private readonly RepositoryFactory _repositoryFactory;
        public WeatherForecastProvider()
        {
            _repositoryFactory = new RepositoryFactory();
        }
        public async Task<IEnumerable<CityModel>> ListCities()
        {
            var cityModels = (await _repositoryFactory.GetRepository<City>().ListAllAsync())
                .Select(c => new CityModel(){Id = c.Id, Name = c.Name});
            return cityModels;
        }

        public async Task<WeatherModel> ReportCityWeather(CityModel city, DateTime dateTime)
        {
            var weatherEntity = await _repositoryFactory.GetWeatherRepository()
                .GetCityWeather(new City() {Id = city.Id, Name = city.Name}, dateTime);
            return new WeatherModel()
            {
                Day = weatherEntity.Day,
                TemperatureTo = weatherEntity.MaxTemperature,
                TemperatureFrom = weatherEntity.MinTemperature
            };
        }
    }
}