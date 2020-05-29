using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherForecast.Client.Logic.Interfaces;
using WeatherForecast.Client.Logic.ViewModel;
using WeatherForecast.Contracts;
using WeatherForecast.Logger;

namespace WeatherForecast.Client.Logic.Services
{
    public class WeatherDataProvider : IWeatherDataProvider
    {
        private readonly Func<CityModel, CityViewModel> _cityViewModelMapper;
        private readonly Func<WeatherModel, WeatherReportViewModel> _weatherReportViewModelMapper;
        private readonly Func<CityViewModel, CityModel> _cityModelMapper;
        private readonly IWeatherForecastContract _weatherForecastContract;
        private readonly ILog _logger;

        public WeatherDataProvider(
            Func<CityModel, CityViewModel> cityViewModelMapper,
            Func<WeatherModel, WeatherReportViewModel> weatherReportViewModelMapper,
            Func<CityViewModel, CityModel> cityModelMapper,
            IWeatherForecastContract weatherForecastContract,
            ILog logger)
        {
            _cityViewModelMapper = cityViewModelMapper;
            _weatherReportViewModelMapper = weatherReportViewModelMapper;
            _cityModelMapper = cityModelMapper;
            _weatherForecastContract = weatherForecastContract;
            _logger = logger;
        }

        public async Task<IEnumerable<CityViewModel>> GetCities()
        {
            try
            {
                return (await _weatherForecastContract.ListCities()).Select(_cityViewModelMapper);
            }
            catch (Exception e)
            {
                //Place for logger
                _logger.Error(e, "Server Error");
                throw;
            }
        }

        public async Task<WeatherReportViewModel> GetWeatherReportForTomorrow(CityViewModel cityViewModel)
        {
            try
            {
                return  _weatherReportViewModelMapper(await _weatherForecastContract.ReportCityWeather(_cityModelMapper(cityViewModel), DateTime.Now.AddDays(1)));
            }
            catch (Exception e)
            {
                //Place for logger
                _logger.Error(e, "Server Error");
                throw;
            }
        }
    }
}