using System;
using NLog;

namespace WeatherForecast.Logger
{
    public interface ILog
    {
        void Error(Exception e, string message);
        void Info(string message);
    }

    internal class Logger : ILog
    {
        public void Error(Exception e, string message)
        {
            var logger = LogManager.GetLogger("logger");
            logger.Error(e, message);
        }

        public void Info(string message)
        {
            var logger = LogManager.GetLogger("logger");
            logger.Info(message);
        }
    }
}