using System.Runtime.Serialization;

namespace WeatherForecast.Contracts
{
    [DataContract]
    public class CityModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}