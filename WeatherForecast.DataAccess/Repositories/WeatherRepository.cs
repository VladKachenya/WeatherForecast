using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using WeatherForecast.DataAccess.Entities;
using WeatherForecast.DataAccess.Interfaces;

namespace WeatherForecast.DataAccess.Repositories
{
    internal class WeatherRepository : BaseRepository, IWeatherRepository
    {
        public async Task<Weather> GetByIdAsync(int id)
        {
            var res = await _queryExecutor.ExecuteAsync(async (db) => await db.QueryFirstAsync($"SELECT * FROM {TableNames.weather} WHERE id = '{id}'"));
            return new Weather() { Id = (int)res.id, CityId  = (int)res.city_id, Day = (DateTime)res.day, MaxTemperature = (int)res.max_temperature, MinTemperature = (int)res.min_temperature };
        }

        public async Task<IReadOnlyList<Weather>> ListAllAsync()
        {
            return await _queryExecutor.ExecuteAsync(async (db) => (await db.QueryAsync<Weather>("SELECT * FROM weather")).ToList());
        }

        public async Task<Weather> AddAsync(Weather entity)
        {
            return await _queryExecutor.ExecuteAsync(async (db) =>
            {
                var sqlQuery = $"INSERT INTO {TableNames.weather}(city_id, day, max_temperature, min_temperature) " +
                               $"VALUES({entity.CityId}, '{entity.Day:yyyy-MM-dd}', {entity.MaxTemperature}, {entity.MinTemperature}); " +
                               $"SELECT LAST_INSERT_ID()";
                entity.Id = (await db.QueryAsync<int>(sqlQuery)).FirstOrDefault();
                return entity;
            });
        }

        public async Task<Weather> AddOrUpdateAsync(Weather entity)
        {
            return await _queryExecutor.ExecuteAsync(async (db) =>
            {
                var sqlQuery = $"SELECT id FROM {TableNames.weather} " +
                               $"WHERE city_id = {entity.CityId} AND day = '{entity.Day:yyyy-MM-dd}';";
                var weatherId = await db.QueryFirstOrDefaultAsync<int>(sqlQuery);
                if (weatherId > 0)
                {
                    entity.Id = weatherId;
                    await UpdateAsync(entity);
                }
                else
                {
                    entity = await AddAsync(entity);
                }
                return entity;
            });
        }

        public async Task UpdateAsync(Weather entity)
        {
            await _queryExecutor.ExecuteAsync<Task>(async (db) =>
            {
                var sqlQuery = $"UPDATE {TableNames.weather} " +
                               $"SET day = '{entity.Day:yyyy-MM-dd}', " +
                               $"max_temperature = {entity.MaxTemperature}, " +
                               $"min_temperature = {entity.MinTemperature} " +
                               $"WHERE id = {entity.Id}";
                await db.ExecuteAsync(sqlQuery);
            });
        }

        public async Task DeleteAsync(Weather entity)
        {
            await _queryExecutor.ExecuteAsync<Task>(async (db) =>
            {
                await db.ExecuteAsync($"DELETE FROM {TableNames.weather} WHERE Id = {entity.Id}");
            });
        }

        public async Task<Weather> GetCityWeather(City city, DateTime day)
        {
            var res = await _queryExecutor.ExecuteAsync(async (db) => await db.QueryFirstAsync($"SELECT * FROM {TableNames.weather} WHERE city_id = {city.Id} AND day = '{day:yyyy-MM-dd}'"));
            return new Weather() { Id = (int)res.id, CityId = (int)res.city_id, Day = (DateTime)res.day, MaxTemperature = (int)res.max_temperature, MinTemperature = (int)res.min_temperature };
        }
    }
}