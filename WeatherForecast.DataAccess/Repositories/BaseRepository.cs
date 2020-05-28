using System.Configuration;
using Dapper;
using WeatherForecast.DataAccess.Entities;
using WeatherForecast.DataAccess.Interfaces;
using WeatherForecast.DataAccess.Validators;

namespace WeatherForecast.DataAccess.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly IQueryExecutor _queryExecutor;
        protected readonly IValidator _validator;

        protected BaseRepository(string connectionString = null)
        {
            _queryExecutor = new MySqlQueryExecutor();
            _validator = new Validator();
        }
    }
}