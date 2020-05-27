namespace WeatherForecast.Grabber.Parser.Gismeteo
{
    public class CityWeather
    {
        public string CityName { get; set; }
        public int MinTemperature { get; set; }
        public int MaxTemperature { get; set; }
        public bool IsValid { get; set; }

        public override string ToString()
        {
            return $"{CityName} MinT: {MinTemperature} MaxT: {MaxTemperature} {nameof(IsValid)}:{IsValid}";
        }
    }
}