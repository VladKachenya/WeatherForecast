using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace WeatherForecast.Contracts
{

    [ServiceContract]
    public interface IWeatherForecastContract
    {
        [OperationContract]
        Task<IEnumerable<CityModel>> ListCities();
        [OperationContract]
        Task<WeatherModel> ReportCityWeather(CityModel city, DateTime dateTime);
    }

}