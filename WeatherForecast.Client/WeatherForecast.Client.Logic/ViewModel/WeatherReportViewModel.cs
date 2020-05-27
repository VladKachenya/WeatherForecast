using System;
using GalaSoft.MvvmLight;

namespace WeatherForecast.Client.Logic.ViewModel
{
    public class WeatherReportViewModel : ViewModelBase
    {
        private string _date;
        private string _temperatureFrom;
        private string _temperatureTo;

        public WeatherReportViewModel()
        {
            
        }

        public WeatherReportViewModel(DateTime date, int maxT, int minT)
        {
            Date = date.ToString("yyyy-MM-dd");
            TemperatureFrom = ConvertTemperatureToString(minT);
            TemperatureTo = ConvertTemperatureToString(maxT);
        }

        private string ConvertTemperatureToString(int t)
        {
            if (t < 0)
            {
                return $"{t}";
            }

            if(t > 0)
            {
                return $"+{t}";
            }

            return "0";
        }

        public string Date
        {
            get => _date;
            set => Set(ref _date, value);
        }

        public string TemperatureFrom
        {
            get => _temperatureFrom;
            set => Set(ref _temperatureFrom, value);
        }

        public string TemperatureTo
        {
            get => _temperatureTo;
            set => Set(ref _temperatureTo, value);
        }
    }
}