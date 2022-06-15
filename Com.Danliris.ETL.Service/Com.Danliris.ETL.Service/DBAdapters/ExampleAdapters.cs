using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;
using Com.Danliris.ETL.Service.Models;
using Com.Danliris.ETL.Service.ExcelModels;

namespace Com.Danliris.ETL.Service.DBAdapters
{
    public class ExampleAdapters
    {
        private readonly IDbConnection _connection;
        public ExampleAdapters(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
        }

        public List<Person> Get(int size, int page, string keyword)
        {
            return _connection.Query<Person>($"SELECT * FROM Persons ORDER BY Name OFFSET({(page - 1) * size}) ROWS FETCH NEXT({size}) ROWS ONLY").ToList();
        }

        public int Insert(Person person)
        {
            var query = $"INSERT INTO [dbo].[Persons]([Name], [Gender]) VALUES (@Name, @Gender)";
            var result = _connection.Execute(query, person);
            return result;
        }
    }
}