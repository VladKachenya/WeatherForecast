using System;
using System.ComponentModel.DataAnnotations;
using WeatherForecast.DataAccess.Interfaces;

namespace WeatherForecast.DataAccess.Validators
{
    internal class Validator : IValidator
    {
        public bool IsValidId(int id)
        {
            return id > 0;
        }

        public bool IsNotNull(object obj)
        {
            return obj != null;
        }

        public bool IsValidName(string name)
        {
            return !string.IsNullOrWhiteSpace(name);
        }
    }
}