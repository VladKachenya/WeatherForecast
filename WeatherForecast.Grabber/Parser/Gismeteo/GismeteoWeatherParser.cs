using System;
using System.Linq;
using AngleSharp.Html.Dom;

namespace WeatherForecast.Grabber.Parser.Gismeteo
{
    public class GismeteoWeatherParser : IParser<CityWeather>
    {
        public CityWeather Parse(IHtmlDocument document)
        {
            try
            {
                var res = new CityWeather();
                res.CityName = document.QuerySelectorAll("div")
                    .FirstOrDefault(item =>
                        item.ClassName != null && item.ClassName.Contains("subnav_search_city js_citytitle"))
                    ?.TextContent;
                var temperature = document.QuerySelectorAll("div")
                    .FirstOrDefault(item => item.ClassName != null && item.ClassName.Contains("tabs _center"))
                    ?.QuerySelectorAll("div")
                    .FirstOrDefault(item => item.ClassName != null && item.ClassName.Contains("tab  tooltip"))
                    ?.QuerySelectorAll("span")
                    .Where(item => item.ClassName != null && item.ClassName.Contains("unit_temperature_c"))
                    .Select(item => int.Parse(item.TextContent));

                if (temperature.Count() != 2)
                {
                    throw new Exception();
                }

                res.MaxTemperature = temperature.Max();
                res.MinTemperature = temperature.Min();
                res.IsValid = true;
                return res;
            }
            catch
            {
                return new CityWeather();
            }
        }
    }
}