using System;
using System.Data;
using System.Threading.Tasks;

namespace WeatherForecast.DataAccess.Interfaces
{
    public interface IQueryExecutor
    {
        Task<TResult> ExecuteAsync<TResult>(Func<IDbConnection, Task<TResult>> queryFunc);
        Task ExecuteAsync<T>(Func<IDbConnection, Task> queryFunc);
    }
}