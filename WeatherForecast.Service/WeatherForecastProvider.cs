using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherForecast.Contracts;
using WeatherForecast.DataAccess.Entities;
using WeatherForecast.DataAccess.Factories;
using WeatherForecast.Logger;

namespace WeatherForecast.Service
{
    public class WeatherForecastProvider : IWeatherForecastContract
    {
        private readonly RepositoryFactory _repositoryFactory;
        private readonly ILog _logger;
        public WeatherForecastProvider()
        {
            _logger = (new LoggerFactory()).GetLogger();
            _repositoryFactory = new RepositoryFactory();
        }
        public async Task<IEnumerable<CityModel>> ListCities()
        {
            try
            {
                var cityModels = (await _repositoryFactory.GetRepository<City>().ListAllAsync())
                    .Select(c => new CityModel() { Id = c.Id, Name = c.Name });
                return cityModels;
            }
            catch (Exception e)
            {
                _logger.Error(e,"Failed to list cities data");
                throw;
            }
        }

        public async Task<WeatherModel> ReportCityWeather(CityModel city, DateTime dateTime)
        {
            try
            {
                var weatherEntity = await _repositoryFactory.GetWeatherRepository()
                    .GetCityWeather(new City() { Id = city.Id, Name = city.Name }, dateTime);
                return new WeatherModel()
                {
                    Day = weatherEntity.Day,
                    TemperatureTo = weatherEntity.MaxTemperature,
                    TemperatureFrom = weatherEntity.MinTemperature
                };
            }
            catch (Exception e)
            {
                _logger.Error(e, "Failed reporting weather data");
                throw;
            }
        }
    }
}