using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using WeatherForecast.DataAccess.Entities;
using WeatherForecast.DataAccess.Factories;
using WeatherForecast.Grabber.Parser.Gismeteo;
using WeatherForecast.Logger;

namespace WeatherForecast.Grabber
{
    public class WeatherDataWriter
    {
        private readonly RepositoryFactory _repositoryFactory;
        private readonly ILog _logger;
        public WeatherDataWriter()
        {
            _logger = (new LoggerFactory()).GetLogger();
            _repositoryFactory = new RepositoryFactory();
        }
        public async void Write(CityWeather weather)
        {
            if (weather.IsValid)
            {
                try
                {
                    var cityRepository = _repositoryFactory.GetRepository<City>();
                    var cityId = (await cityRepository.AddOrUpdateAsync(new City() { Name = weather.CityName })).Id;

                    var weatherRepository = _repositoryFactory.GetRepository<Weather>();

                    await weatherRepository.AddOrUpdateAsync(new Weather()
                    {
                        CityId = cityId,
                        MaxTemperature = weather.MaxTemperature,
                        MinTemperature = weather.MinTemperature,
                        Day = DateTime.Today.AddDays(1)
                    });
                    _logger.Info($"Data: {weather} added");
                }
                catch (Exception e)
                {
                   _logger.Error(e,$"Error with data writing: {weather}");
                }
            }
        }
    }
}