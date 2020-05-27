using System;
using System.Threading.Tasks;
using WeatherForecast.DataAccess.Entities;

namespace WeatherForecast.DataAccess.Interfaces
{
    public interface IWeatherRepository : IRepository<Weather>
    {
        Task<Weather> GetCityWeather(City city, DateTime day);
    }
}