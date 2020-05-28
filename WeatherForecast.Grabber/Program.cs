using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using WeatherForecast.Grabber.Parser;
using WeatherForecast.Grabber.Parser.Gismeteo;

namespace WeatherForecast.Grabber
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = "https://www.gismeteo.ru";

            var cityParserSettings = new ParserSettings { BaseUrl = url};
            cityParserSettings.Prefixes.Add(string.Empty);

            var cityParser = new ParserWorker<string[]>(new GismeteoCityParser(), cityParserSettings);
            ParserWorker<CityWeather> weatherParser;

            List<string> cityPrefixes = new List<string>();

            cityParser.OnNewData += (sender, data) => cityPrefixes.AddRange(data);
            cityParser.OnCompleted += sender =>
            {
                var weatherParserSettings = new ParserSettings { BaseUrl = url };
                weatherParserSettings.Prefixes.AddRange(cityPrefixes.Select(pref => '/' + pref.Trim('/') + "/tomorrow/"));

                weatherParser = new ParserWorker<CityWeather>(new GismeteoWeatherParser(), weatherParserSettings);

                weatherParser.Start();
                weatherParser.OnNewData += (sender1, cityWeather) => (new WeatherDataWriter()).Write(cityWeather);
                weatherParser.OnCompleted += s =>
                {
                    Console.WriteLine("Data added");
                    Thread.Sleep(2000);
                    weatherParser.Start();
                };
            };
            cityParser.Start();

            Console.ReadLine();
        }
    }
}
