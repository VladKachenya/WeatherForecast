using GalaSoft.MvvmLight;

namespace WeatherForecast.Client.Logic.ViewModel
{
    public class CityViewModel: ViewModelBase
    {
        private readonly int _cityId;

        public CityViewModel(int cityId)
        {
            _cityId = cityId;
        }

        private string _name;

        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }

        public int GetCityId() => _cityId;
    }
}