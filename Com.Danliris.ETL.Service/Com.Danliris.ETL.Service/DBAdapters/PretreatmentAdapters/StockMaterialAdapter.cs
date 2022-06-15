using Com.Danliris.ETL.Service.Models.DashboardPretreatmentModels;
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

namespace Com.Danliris.ETL.Service.DBAdapters.PretreatmentAdapters
{
    public class StockMaterialAdapter : IStockMaterialAdapter
    {
        //private readonly IDbConnection _connection;
        private readonly ISqlDataContext<StockMaterialModel> context;
        public StockMaterialAdapter(IServiceProvider service)
        {
            context = service.GetService<ISqlDataContext<StockMaterialModel>>();
        }

        public async Task DeleteByMonthAndYear(DateTime periode)
        {
            var query = $"DELETE FROM [dbo].[DashboardStockMaterial] WHERE MONTH(DateInOut)=@month AND YEAR(DateInOut)=@year";
            await context.QueryAsync(query, new { month = periode.Month, year = periode.Year });
        }

        public async Task InsertBulk(IEnumerable<StockMaterialModel> models)
        {
            var query = $"INSERT INTO [dbo].[DashboardStockMaterial] ([ID],[No],[DateInOut],[Activity],[OrderNo],[Material],[Train],[Quantity]) VALUES ( @Id,@No,@DateInOut,@Activity,@OrderNo,@Material,@Train,@Quantity)";
            var result = await context.ExecuteAsync(query, models);
        }
    }
}
