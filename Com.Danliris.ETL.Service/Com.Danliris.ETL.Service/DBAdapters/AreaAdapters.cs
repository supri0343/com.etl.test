using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Com.Danliris.ETL.Service.Models;

namespace Com.Danliris.ETL.Service.DBAdapters
{
    public class AreaAdapters
    {
        private readonly IDbConnection _connection;
        public AreaAdapters(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
        }

        public async Task<List<Area>> Get()
        {
            _connection.Open();
            var sql = "SELECT * FROM [dbo].[tabelArea]";
            var result = await _connection.QueryAsync<Area>(sql);
            return result.ToList();
        }
    }
}