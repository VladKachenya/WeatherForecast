using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherForecast.Service
{
    [RunInstaller(true)]
    public partial class WeatherForecastServiceInstaller : System.Configuration.Install.Installer
    {
        public WeatherForecastServiceInstaller()
        {
            InitializeComponent();
        }
    }
}
