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
    public class StockSparepartAdapter : IStockSparepartAdapter
    {
        //private readonly IDbConnection _connection;
        private readonly ISqlDataContext<StockSparepartModel> context;
        public StockSparepartAdapter(IServiceProvider service)
        {
            context = service.GetService<ISqlDataContext<StockSparepartModel>>();
        }

        //public async Task<List<StockSparepartModel>> Get()
        //{
        //    _connection.Open();
        //    var query = $"SELECT * FROM [dbo].[DashboardStockSparepart]";
        //    var result = await _connection.QueryAsync<StockSparepartModel>(query);
        //    _connection.Close();
        //    return result.ToList();
        //}

        public async Task DeleteByMonthAndYear(DateTime periode)
        {
            //_connection.Open();
            var query = $"DELETE FROM [dbo].[DashboardStockSparepart] WHERE MONTH(Date)=@month AND YEAR(Date)=@year";
            await context.QueryAsync(query, new { month = periode.Month, year = periode.Year });
            //_connection.Close();
            //return result.ToList();
        }

        public async Task InsertBulk(IEnumerable<StockSparepartModel> models)
        {
            //_connection.Open();
            var query = $"INSERT INTO [dbo].[DashboardStockSparepart] ([ID],[No],[Date],[ItemCode],[RackNo],[MasterCode],[ItemName],[Unit],[EarlyS],[In],[Out],[FinalS]) VALUES (@ID,@No,@Date,@ItemCode,@RackNo,@MasterCode,@ItemName,@Unit,@EarlyS,@In,@Out,@FinalS)";
            var result = await context.ExecuteAsync(query, models);

            //_connection.Close();
        }

        //public async Task Update(StockSparepartModel model)
        //{
        //    _connection.Open();
        //    var query = $"UPDATE [dbo].[DashboardStockSparepart] SET ([ID],[No],[Date],[ItemCode],[RackNo],[MasterCode],[ItemName],[Unit],[EarlyS],[In],[Out],[FinalS]) VALUES (@ID,@No,@Date,@ItemCode,@RackNo,@MasterCode,@ItemName,@Unit,@EarlyS,@In,@Out,@FinalS) WHERE [ID] = @Id";
        //    var result = await _connection.QueryAsync<StockSparepartModel>(query, model);
        //    _connection.Close();
        //}

        //public async Task Delete(StockSparepartModel model)
        //{
        //    _connection.Open();
        //    var query = $"DELETE FROM [dbo].[DashboardStockSparepart] WHERE [ID] = @Id";
        //    var result = await _connection.ExecuteAsync(query, model);
        //    _connection.Close();

        //}

        
    }
}
