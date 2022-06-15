using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Com.Danliris.ETL.Service.ExcelModels;
using Com.Danliris.ETL.Service.Tools;
using Dapper;
using Microsoft.Extensions.DependencyInjection;

namespace Com.Danliris.ETL.Service.DBAdapters.FinishingAdapters
{
    public class StockOpnameAdapter : IStockOpnameAdapter
    {

        private readonly ISqlDataContext<DashboardStockOpnameF> _context;

        public StockOpnameAdapter(IServiceProvider serviceProvider)
        {
            _context = serviceProvider.GetService<ISqlDataContext<DashboardStockOpnameF>>();
        }

        public async Task DeleteByMonthAndYear(DateTime periode)
        {
            var query = $"DELETE FROM [dbo].[DashboardStockOpnameF] WHERE MONTH(Date)=@month AND YEAR(Date)=@year";
            await _context.QueryAsync(query, new { month = periode.Month, year = periode.Year });
        }

        public async Task InsertBulk(IEnumerable<DashboardStockOpnameF> models)
        {
            var query = $"INSERT INTO [dbo].[DashboardStockOpnameF] ([ID] ,[No] ,[Date] ,[SPNo] ,[Material] ,[TrainNo], [Length] ,[Description]) " +
                $"VALUES (@Id, @No, @Date, @SPNo, @Material, @TrainNo, @Length, @Description)";
            await _context.ExecuteAsync(query, models);
        }
    }
}
