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
    public class ReprocessAdapter : IReprocessAdapter
    {
        private readonly ISqlDataContext<DashboardReprocessF> _context;

        public ReprocessAdapter(IServiceProvider serviceProvider)
        {
            _context = serviceProvider.GetService<ISqlDataContext<DashboardReprocessF>>();
        }

        public async Task DeleteByMonthAndYear(DateTime periode)
        {
            var query = $"DELETE FROM [dbo].[DashboardReprocessF] WHERE MONTH(QCOutDate)=@month AND YEAR(QCOutDate)=@year";
            await _context.QueryAsync(query, new { month = periode.Month, year = periode.Year });
        }

        public async Task InsertBulk(IEnumerable<DashboardReprocessF> models)
        {
            var query = $"INSERT INTO [dbo].[DashboardReprocessF] ([ID] ,[No] ,[SPPNo] ,[QCOutDate] ,[TrainNumber] ,[Material], [Code] ,[Reprocess] ,[QuantityOut] ,[Information] ,[AreaDestination]) " +
                $"VALUES (@Id, @No, @SPPNo, @QCOutDate, @TrainNumber, @Material, @Code, @Reprocess, @QuantityOut, @Information, @AreaDestination)";
            await _context.ExecuteAsync(query, models);
        }
    }
}
