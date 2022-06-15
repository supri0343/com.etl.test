using Com.Danliris.ETL.Service.Models.DashboardGudangSparepartModels;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Com.Danliris.ETL.Service.Tools;

namespace Com.Danliris.ETL.Service.DBAdapters.GudangSparepartAdapters
{
    public class ReleaseItemSparepartAdapter : IReleaseItemSparepartAdapter
    {
        //private readonly IDbConnection _connection;
        private readonly ISqlDataContext<SparepartItemRelease> context;
        public ReleaseItemSparepartAdapter(IServiceProvider service)
        {
            context = service.GetService<ISqlDataContext<SparepartItemRelease>>();

        }

        //public async Task<List<SparepartItemRelease>> Get()
        //{
        //    _connection.Open();
        //    var query = $"SELECT * FROM [dbo].[DashboardSparepartItemRelease]";
        //    var result = await _connection.QueryAsync<SparepartItemRelease>(query);
        //    _connection.Close();
        //    return result.ToList();
        //}

        public async Task InsertBulk(IEnumerable<SparepartItemRelease> model)
        {
           // _connection.Open();
            var query = $"INSERT INTO [dbo].[DashboardSparepartItemRelease] ([ID],[No],[Date],[BonNo],[ItemCode],[ItemName],[Total],[Unit],[MC],[TakenBy],[Area]) VALUES ( @Id , @No , @Date , @BonNo , @ItemCode , @ItemName , @Total , @Unit , @MC , @TakenBy , @Area)";
            var result = await context.ExecuteAsync(query, model);
            //_connection.Close();
        }

        public async Task DeleteByMonthAndYear(DateTime periode)
        {
            //_connection.Open();
            var  query = $"DELETE FROM [dbo].[DashboardSparepartItemRelease] WHERE MONTH(Date)=@month AND YEAR(Date)=@year";
            await context.QueryAsync(query, new { month = periode.Month, year = periode.Year });
            //_connection.Close();
            //return result.ToList();
        }

        //public async Task Update(SparepartItemRelease model)
        //{
        //    _connection.Open();
        //    var query = $"UPDATE [dbo].[DashboardSparepartItemRelease] SET ([ID],[No],[Date],[BonNo],[ItemCode],[ItemName],[Total],[Unit],[MC],[TakenBy],[Area]) VALUES ( @Id , @No , @Date , @BonNo , @ItemCode , @ItemName , @Total , @Unit , @MC , @TakenBy , @Area  ) WHERE [ID] = @Id";
        //    var result = await _connection.QueryAsync<SparepartItemRelease>(query, model);
        //    _connection.Close();
        //}

        //public async Task Delete(SparepartItemRelease model)
        //{
        //    _connection.Open();
        //    var query = $"DELETE FROM [dbo].[DashboardSparepartItemRelease] WHERE [ID] = @Id";
        //    var result = await _connection.ExecuteAsync(query, model);
        //    _connection.Close();

        //}


    }
}
