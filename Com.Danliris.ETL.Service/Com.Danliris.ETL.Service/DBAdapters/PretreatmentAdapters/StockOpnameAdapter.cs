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
    public class StockOpnameAdapter : IStockOpnameAdapter
    {
        //private readonly IDbConnection _connection;
        private readonly ISqlDataContext<StockOpnameModel> context;
        public StockOpnameAdapter(IServiceProvider service)
        {
            context = service.GetService<ISqlDataContext<StockOpnameModel>>();
        }

        public async Task DeleteByMonthAndYear(DateTime periode)
        {
            var query = $"DELETE FROM [dbo].[DashboardStockOpname] WHERE MONTH(Date)=@month AND YEAR(Date)=@year";
            await context.QueryAsync(query, new { month = periode.Month, year = periode.Year });
        }

        public async Task InsertBulk(IEnumerable<StockOpnameModel> models)
        {
            var query = $"INSERT INTO [dbo].[DashboardStockOpname] ([ID],[No],[Date],[SPNo],[Material],[Train],[Length],[Description]) VALUES (@ID,@No,@Date,@SPNo,@Material,@Train,@Length,@Description)";
            var result = await context.ExecuteAsync(query, models);
        }
    }
}
