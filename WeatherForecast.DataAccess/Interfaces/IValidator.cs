using System;

namespace WeatherForecast.DataAccess.Interfaces
{
    public interface IValidator
    {
        bool IsValidId(int id);
        bool IsNotNull(object obj);

        bool IsValidName(string name);
    }
}