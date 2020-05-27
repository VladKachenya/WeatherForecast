using System;
using System.Collections.Generic;
using WeatherForecast.DataAccess.Entities;
using WeatherForecast.DataAccess.Interfaces;
using WeatherForecast.DataAccess.Repositories;


namespace WeatherForecast.DataAccess.Factories
{
    public class RepositoryFactory
    {
        private readonly Dictionary<Type, Func<BaseRepository>> _repositoryDictionary;
        public RepositoryFactory()
        {
            _repositoryDictionary = new Dictionary<Type, Func<BaseRepository>>();
            _repositoryDictionary.Add(typeof(City), () => new CityRepository());
            _repositoryDictionary.Add(typeof(Weather), () => new WeatherRepository());
        }
        public IRepository<T> GetRepository<T>() where T : BaseEntity
        {
            var type = typeof(T);
            return _repositoryDictionary[type].Invoke() as IRepository<T>;
        }
        public IWeatherRepository GetWeatherRepository()
        {
            return new WeatherRepository();
        }
    }
}