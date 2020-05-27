using System.Configuration;
using Dapper;
using WeatherForecast.DataAccess.Entities;
using WeatherForecast.DataAccess.Interfaces;

namespace WeatherForecast.DataAccess.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly IQueryExecutor _queryExecutor;


        protected BaseRepository(string connectionString = null)
        {
            _queryExecutor = new MySqlQueryExecutor();
        }
    }
}