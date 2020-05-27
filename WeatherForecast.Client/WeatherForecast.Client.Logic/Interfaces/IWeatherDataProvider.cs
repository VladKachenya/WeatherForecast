using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherForecast.Client.Logic.ViewModel;

namespace WeatherForecast.Client.Logic.Interfaces
{
    public interface IWeatherDataProvider
    {
        Task<IEnumerable<CityViewModel>> GetCities();
        Task<WeatherReportViewModel> GetWeatherReportForTomorrow(CityViewModel cityViewModel);

    }
}