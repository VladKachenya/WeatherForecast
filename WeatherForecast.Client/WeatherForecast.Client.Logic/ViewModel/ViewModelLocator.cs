/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:WeatherForecast.Client.Shell"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using System;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using WeatherForecast.Client.Logic.Interfaces;
using WeatherForecast.Client.Logic.Services;
using WeatherForecast.Contracts;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace WeatherForecast.Client.Logic.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<Func<CityModel, CityViewModel>>(() => (cm) => new CityViewModel(cm.Id) { Name = cm.Name });
            SimpleIoc.Default.Register<Func<CityViewModel, CityModel>>(() => (cvm) => new CityModel() { Name = cvm.Name, Id = cvm.GetCityId()});

            SimpleIoc.Default.Register<Func<WeatherModel, WeatherReportViewModel>>(() => (wm) => new WeatherReportViewModel(wm.Day, wm.TemperatureTo, wm.TemperatureFrom));

            SimpleIoc.Default.Register<IWeatherDataProvider, WeatherDataProvider>();
            SimpleIoc.Default.Register(
                () =>  new ChannelFactory<IWeatherForecastContract>(new BasicHttpBinding(), new EndpointAddress("http://localhost:9001/WeatherForecastService")).CreateChannel());

        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}