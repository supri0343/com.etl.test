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
    public class ReceiptItemSparepartAdapter : IReceiptItemSparepartAdapter
    {
        //private readonly IDbConnection _connection;
        private readonly ISqlDataContext<SparepartItemReceipt> context;
        public ReceiptItemSparepartAdapter(IServiceProvider service)
        {
            context = service.GetService<ISqlDataContext<SparepartItemReceipt>>();
        }

        //public async Task<List<SparepartItemReceipt>> Get()
        //{
        //    //_connection.Open();
        //    var query = $"SELECT * FROM [dbo].[DashboardSparepartItemReceipt]";
        //    var result = await _connection.QueryAsync<SparepartItemReceipt>(query);
        //    //_connection.Close();
        //    return result.ToList();
        //}

        public async Task InsertBulk(IEnumerable<SparepartItemReceipt> models)
        {
            
            var query = $"INSERT INTO [dbo].[DashboardSparepartItemReceipt] ([ID],[No],[Date],[Area],[BonNo],[Supplier],[PONo],[ItemCode],[ItemName],[Total],[Unit],[Description]) VALUES (@ID,@No,@Date,@Area,@BonNo,@Supplier,@PONo,@ItemCode,@ItemName,@Total,@Unit,@Description)";
            var result = await context.ExecuteAsync(query, models);

        }
        public async Task  DeleteByMonthAndYear(DateTime periode)
        {
            //_connection.Open();
            var query = $"DELETE FROM [dbo].[DashboardSparepartItemReceipt] WHERE MONTH(Date)=@month AND YEAR(Date)=@year";
            await context.QueryAsync(query, new { month = periode.Month, year = periode.Year });
            //_connection.Close();
            //return result.ToList();
        }

        //public async Task Update(SparepartItemReceipt model)
        //{
        //    _connection.Open();
        //    var query = $"UPDATE [dbo].[DashboardSparepartItemReceipt] SET ([ID],[No],[Date],[Area],[BonNo],[Supplier],[PONo],[ItemCode],[ItemName],[Total],[Unit],[Description]) VALUES (@ID,@No,@Date,@Area,@BonNo,@Supplier,@PONo,@ItemCode,@ItemName,@Total,@Unit,@Description) WHERE [ID] = @Id";
        //    var result = await _connection.QueryAsync<SparepartItemReceipt>(query, model);
        //    _connection.Close();
        //}

        //public async Task Delete(SparepartItemReceipt model)
        //{
        //    _connection.Open();
        //    var query = $"DELETE FROM [dbo].[DashboardSparepartItemReceipt] WHERE [ID] = @Id";
        //    var result = await _connection.ExecuteAsync(query, model);
        //    _connection.Close();

        //}

    }
}
