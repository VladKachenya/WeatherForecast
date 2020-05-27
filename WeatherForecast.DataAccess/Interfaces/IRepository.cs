using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherForecast.DataAccess.Entities;

namespace WeatherForecast.DataAccess.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<T> AddAsync(T entity);
        Task<T> AddOrUpdateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}