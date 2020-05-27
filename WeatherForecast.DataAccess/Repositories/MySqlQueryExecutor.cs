using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using WeatherForecast.DataAccess.Interfaces;

namespace WeatherForecast.DataAccess.Repositories
{
    internal class MySqlQueryExecutor : IQueryExecutor
    {
        private readonly string _connectionString;

        static MySqlQueryExecutor()
        {
            using (var db = new MySqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                db.Open();

                var creationQuery = String.Format(@"
                CREATE DATABASE IF NOT EXISTS weather_storage;
                USE weather_storage;

                CREATE TABLE IF NOT EXISTS cities
                (
                    id int PRIMARY KEY AUTO_INCREMENT,
                    name varchar(50) NOT NULL UNIQUE
                    );

                CREATE TABLE IF NOT EXISTS weather
                (
                    id int PRIMARY KEY AUTO_INCREMENT,
                    city_id INT NOT NULL,
                    day date,
                max_temperature smallint,
                    min_temperature smallint,
                FOREIGN KEY (city_id) REFERENCES cities (id)
                    );");
                var creationCommand = new MySqlCommand(creationQuery, db);
                creationCommand.ExecuteNonQuery();
                db.Close();
            }
        }
        public MySqlQueryExecutor()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString + ";Database=weather_storage";
        }

        public async Task<TResult> ExecuteAsync<TResult>(Func<IDbConnection, Task<TResult>> queryFunc)
        {
            using (IDbConnection db = new MySqlConnection(_connectionString))
            {
                return await queryFunc(db);
            }
        }

        public async Task ExecuteAsync<T>(Func<IDbConnection, Task> queryFunc)
        {
            using (IDbConnection db = new MySqlConnection(_connectionString))
            {
                await queryFunc(db);
            }
        }
    }
}