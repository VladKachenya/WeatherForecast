using AngleSharp.Html.Dom;

namespace WeatherForecast.Grabber.Parser
{
    interface IParser<T> where T : class
    {
        T Parse(IHtmlDocument document);
    }
}
