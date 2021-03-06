﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using WeatherForecast.DataAccess.Entities;
using WeatherForecast.DataAccess.Interfaces;

namespace WeatherForecast.DataAccess.Repositories
{
    internal class CityRepository : BaseRepository, IRepository<City>
    {
        public async Task<City> GetByIdAsync(int id)
        {
            if (!_validator.IsValidId(id)) throw new ArgumentException("Id must be more then 0");

            return await _queryExecutor.ExecuteAsync(async (db) => await db.QueryFirstAsync<City>($"SELECT * FROM {TableNames.cities} WHERE id = '{id}'"));
        }

        public async Task<IReadOnlyList<City>> ListAllAsync()
        {
            return await _queryExecutor.ExecuteAsync( async (db) => (await db.QueryAsync<City>($"SELECT * FROM {TableNames.cities}")).ToList());
        }

        public async Task<City> AddAsync(City entity)
        {
            if (!_validator.IsNotNull(entity)) throw new NullReferenceException();
            if (!_validator.IsValidName(entity.Name)) throw new ValidationException($"{nameof(entity.Name)} can not be empty");

            return await _queryExecutor.ExecuteAsync(async (db) =>
            {
                var sqlQuery = $"INSERT INTO {TableNames.cities}(name) " +
                               $"VALUES('{entity.Name}'); " +
                               $"SELECT LAST_INSERT_ID()";
                entity.Id = (await db.QueryAsync<int>(sqlQuery)).FirstOrDefault();
                return entity;
            });
        }

        public async Task<City> AddOrUpdateAsync(City entity)
        {
            if (!_validator.IsNotNull(entity)) throw new NullReferenceException();
            if (!_validator.IsValidName(entity.Name)) throw new ValidationException($"{nameof(entity.Name)} can not be empty");

            return await _queryExecutor.ExecuteAsync(async (db) =>
            {
                var sqlQuery = $"SELECT id FROM {TableNames.cities} " +
                               $"WHERE name = '{entity.Name}'";
                var cityId = await db.QueryFirstOrDefaultAsync<int>(sqlQuery);
                if (cityId > 0)
                {
                    entity.Id = cityId;
                }
                else
                {
                    entity = await AddAsync(entity);
                }
                return entity;
            });
        }

        public async Task UpdateAsync(City entity)
        {
            if (!_validator.IsNotNull(entity)) throw new NullReferenceException();
            if (!_validator.IsValidId(entity.Id)) throw new ArgumentException($"{nameof(entity.Id)} must be more then 0");
            if (!_validator.IsValidName(entity.Name)) throw new ValidationException($"{nameof(entity.Name)} can not be empty");

            await _queryExecutor.ExecuteAsync<Task>(async (db) =>
            {
                var sqlQuery = $"UPDATE {TableNames.cities} " +
                               $"SET name = '{entity.Name}' " +
                               $"WHERE id = {entity.Id};";
                await db.ExecuteAsync(sqlQuery);
            });
        }

        public async Task DeleteAsync(City entity)
        {
            if (!_validator.IsNotNull(entity)) throw new NullReferenceException();
            if (!_validator.IsValidId(entity.Id)) throw new ArgumentException($"{nameof(entity.Id)} must be more then 0");
            if (!_validator.IsValidName(entity.Name)) throw new ValidationException($"{nameof(entity.Name)} can not be empty");

            await _queryExecutor.ExecuteAsync<Task>(async (db) =>
            {
                var sqlQuery = $"DELETE FROM {TableNames.weather} " +
                               $"WHERE city_id = {entity.Id}; " +
                               $"DELETE FROM {TableNames.cities} " +
                               $"WHERE id = {entity.Id};";
                await db.ExecuteAsync(sqlQuery);
            });
        }
    }
}