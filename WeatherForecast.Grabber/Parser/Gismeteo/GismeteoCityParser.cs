using System;
using System.Collections.Generic;
using System.Linq;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;

namespace WeatherForecast.Grabber.Parser.Gismeteo
{
    public class GismeteoCityParser : IParser<string[]>
    {
        public string[] Parse(IHtmlDocument document)
        {
            var cityAnchors = new List<IElement>();
            cityAnchors.AddRange(document.QuerySelectorAll("a").Where(item => item.ClassName != null && item.ClassName.Contains("cities_link")));
            cityAnchors.AddRange(document.QuerySelectorAll("noscript").FirstOrDefault(item => item.Id != null && item.Id.Contains("noscript"))
                                     ?.QuerySelectorAll("a") ?? throw new InvalidOperationException());

            return cityAnchors.Select(item =>
                {
                    if (item is IHtmlAnchorElement anchorElement)
                    {
                        return anchorElement.PathName;
                    }

                    return string.Empty;
                })
                .GroupBy(x => x)
                .Select(x => x.First())
                .ToArray(); ;
        }
    }
}