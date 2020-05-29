using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace WeatherForecast.Logger
{
    public class LoggerFactory
    {
        public ILog GetLogger()
        {
            return new Logger();
        }
    }
}
