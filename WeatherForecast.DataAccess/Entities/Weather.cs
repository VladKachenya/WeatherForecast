using System;

namespace WeatherForecast.DataAccess.Entities
{
    public class Weather : BaseEntity
    {
        public int CityId { get; set; }

        public DateTime Day { get; set; }

        public int MaxTemperature { get; set; }

        public int MinTemperature { get; set; }

    }
}
