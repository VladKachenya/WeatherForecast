using System.Collections.Generic;

namespace WeatherForecast.Grabber.Parser
{
    public class ParserSettings : IParserSettings
    {
        public ParserSettings()
        {
            Prefixes = new List<string>();
        }

        public string BaseUrl { get; set; }
        public List<string> Prefixes { get;}
    }
}