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
    public class TransferAdapter : ITransferAdapter
    {

        private readonly ISqlDataContext<DashboardTransferF> _context;

        public TransferAdapter(IServiceProvider serviceProvider)
        {
            _context = serviceProvider.GetService<ISqlDataContext<DashboardTransferF>>();
        } 

        public async Task DeleteByMonthAndYear(DateTime periode)
        {
            var query = $"DELETE FROM [dbo].[DashboardTransferF] WHERE MONTH(Date)=@month AND YEAR(Date)=@year";
            await _context.QueryAsync(query, new { month = periode.Month, year = periode.Year });
        }

        public async Task InsertBulk(IEnumerable<DashboardTransferF> models)
        {
            var query = $"INSERT INTO [dbo].[DashboardTransferF] ([ID] ,[No] ,[Date] ,[SPNo] ,[Construction] ,[TrainNo], [Quantity] ,[Grade] ,[Description] ,[Destination]) " +
                $"VALUES (@Id, @No, @Date, @SPNo, @Construction, @TrainNo, @Quantity, @Grade, @Description, @Destination)";
            await _context.ExecuteAsync(query, models);
        }
    }
}
