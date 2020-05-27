using System;
using System.ServiceProcess;
using System.ServiceModel;
using System.ServiceModel.Description;
using WeatherForecast.Contracts;


namespace WeatherForecast.Service
{
    public partial class WeatherForecastService : ServiceBase
    {
        private ServiceHost _serviceHost = null;
        public WeatherForecastService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            _serviceHost?.Close();
            //_serviceHost = new ServiceHost(typeof(WeatherForecastProvider));

            string address_HTTP = "http://localhost:9001/WeatherForecastService";

            _serviceHost = new ServiceHost(typeof(WeatherForecastProvider), new Uri(address_HTTP));

            ServiceMetadataBehavior behavior = new ServiceMetadataBehavior();
            _serviceHost.Description.Behaviors.Add(behavior);

            BasicHttpBinding bindingHttp = new BasicHttpBinding();
            _serviceHost.AddServiceEndpoint(typeof(IWeatherForecastContract), bindingHttp, address_HTTP);
            _serviceHost.AddServiceEndpoint(typeof(IMetadataExchange), MetadataExchangeBindings.CreateMexHttpBinding(), "mex");

            _serviceHost.Open();
        }

        protected override void OnStop()
        {
            if (_serviceHost != null)
            {
                _serviceHost.Close();
            }
        }
    }
}
