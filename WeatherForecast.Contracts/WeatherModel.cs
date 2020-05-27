using System;
using System.Runtime.Serialization;

namespace WeatherForecast.Contracts
{
    [DataContract]
    public class WeatherModel
    {
        [DataMember]
        public DateTime Day { get; set; }
        [DataMember]
        public int TemperatureFrom { get; set; }
        [DataMember]
        public int TemperatureTo { get; set; }
    }
}