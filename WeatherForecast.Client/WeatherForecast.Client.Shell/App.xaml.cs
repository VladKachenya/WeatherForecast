using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace WeatherForecast.Client.Shell
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private void App_OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message + Environment.NewLine + Environment.NewLine + e.Exception.StackTrace,
                "Ошибка приложения", MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
        }
    }
}
