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
    public class ReceiveAdapter : IReceiveAdapter
    {
        private readonly ISqlDataContext<DashboardReceiveF> _context;

        public ReceiveAdapter(IServiceProvider serviceProvider)
        {
            _context = serviceProvider.GetService<ISqlDataContext<DashboardReceiveF>>();
        }

        public async Task DeleteByMonthAndYear(DateTime periode)
        {
            var query = $"DELETE FROM [dbo].[DashboardReceiveF] WHERE MONTH(Date)=@month AND YEAR(Date)=@year";
            await _context.QueryAsync(query, new { month = periode.Month, year = periode.Year });
        }

        public async Task InsertBulk(IEnumerable<DashboardReceiveF> models)
        {
            var query = $"INSERT INTO [dbo].[DashboardReceiveF] ([ID] ,[No] ,[Date] ,[SPNo] ,[Construction] ,[TrainNo], [Quantity] ,[Grade] ,[Description] ,[Source]) " +
                $"VALUES (@Id, @No, @Date, @SPNo, @Construction, @TrainNo, @Quantity, @Grade, @Description, @Source)";
            await _context.ExecuteAsync(query, models);
        }
    }
}
