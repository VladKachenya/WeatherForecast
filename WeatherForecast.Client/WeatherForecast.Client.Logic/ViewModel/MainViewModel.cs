using System;
using System.Collections.ObjectModel;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Windows.Threading;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using WeatherForecast.Client.Logic.Interfaces;

namespace WeatherForecast.Client.Logic.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private readonly IWeatherDataProvider _weatherDataProvider;
        private readonly Dispatcher _dispatcher;
        private CityViewModel _selectedCity;
        private WeatherReportViewModel _weatherReport;
        private bool _isLoadingEnabled = true;
        private string _errorMessage;

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>

        public MainViewModel(IWeatherDataProvider weatherDataProvider)
        {
            _weatherDataProvider = weatherDataProvider;
            Cities = new ObservableCollection<CityViewModel>();
            _dispatcher = Dispatcher.CurrentDispatcher;
            SelectCityCommand = new RelayCommand<CityViewModel>(OnSelectCity);
            LoadCitiesCommand = new RelayCommand(OnLoadCities, () => _isLoadingEnabled);

            OnLoadCities();
        }

        public async void OnLoadCities()
        {
            try
            {
                _isLoadingEnabled = false;

                var cityViewModels = await _weatherDataProvider.GetCities();
                Cities.Clear();
                foreach (var cityViewModel in cityViewModels)
                {
                    _dispatcher.Invoke(() => Cities.Add(cityViewModel));
                }
                ErrorMessage = String.Empty;
            }
            catch (Exception e)
            {
                ErrorMessage = "Load cities data error";
            }
            finally
            {
                _isLoadingEnabled = true;
            }
        }

        public string Title => "Weather forecast";
        public ObservableCollection<CityViewModel> Cities { get; }

        public CityViewModel SelectedCity
        {
            get => _selectedCity;
            set => Set(ref _selectedCity, value);
        }

        public WeatherReportViewModel WeatherReport
        {
            get => _weatherReport;
            set => Set(ref _weatherReport, value);
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set => Set(ref _errorMessage, value);
        }

        public RelayCommand<CityViewModel> SelectCityCommand { get; }
        public RelayCommand LoadCitiesCommand{ get; }

        public async void OnSelectCity(CityViewModel cityViewModel)
        {
            try
            {
                if (cityViewModel != null)
                {
                    SelectedCity = cityViewModel;
                    var weatherReport = await _weatherDataProvider.GetWeatherReportForTomorrow(cityViewModel);
                    _dispatcher.Invoke(() => WeatherReport = weatherReport);
                }
                ErrorMessage = String.Empty;
            }
            catch (Exception e)
            {
                ErrorMessage = "Load weather data error";
            }
        }

    }
}