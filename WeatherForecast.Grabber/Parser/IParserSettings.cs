using System.Collections.Generic;

namespace WeatherForecast.Grabber.Parser
{
    interface IParserSettings
    {
        string BaseUrl { get; set; }

        List<string> Prefixes { get;}
    }
}
