using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using WeatherForecast.DataAccess.Entities;
using WeatherForecast.DataAccess.Factories;
using WeatherForecast.Grabber.Parser.Gismeteo;

namespace WeatherForecast.Grabber
{
    public class WeatherDataWriter
    {
        private readonly RepositoryFactory _repositoryFactory;
        public WeatherDataWriter()
        {
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
                    Console.WriteLine($"Данные добавлены: {weather}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Не удалось записать данные: {weather}");
                }
            }
        }
    }
}