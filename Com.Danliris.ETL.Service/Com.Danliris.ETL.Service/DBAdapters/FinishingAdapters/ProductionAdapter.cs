using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Com.Danliris.ETL.Service.DBAdapters.FinishingAdapters;
using Com.Danliris.ETL.Service.ExcelModels;
using Com.Danliris.ETL.Service.Tools;
using Dapper;
using Microsoft.Extensions.DependencyInjection;

namespace Com.Danliris.ETL.Service.DBAdapters.FinishingAdapters
{
    public class ProductionAdapter : IProductionAdapter
    {

        private readonly ISqlDataContext<DashboardProductionF> _context;

        public ProductionAdapter(IServiceProvider serviceProvider)
        {
            _context = serviceProvider.GetService<ISqlDataContext<DashboardProductionF>>();
        } 

        public async Task DeleteByMonthAndYear(DateTime periode)
        {
            var query = $"DELETE FROM [dbo].[DashboardProductionF] WHERE MONTH(Date)=@month AND YEAR(Date)=@year";
            await _context.QueryAsync(query, new { month = periode.Month, year = periode.Year });
        }

        public async Task InsertBulk(IEnumerable<DashboardProductionF> models)
        {
            var query = $"INSERT INTO [dbo].[DashboardProductionF] ([ID] ,[No] ,[Machine] ,[Date] ,[Shift] ,[OrderNo] ,[Material], [TrainNo] ,[ProcessType] ,[ProcessDescription] ,[LengthIn] ,[LengthOut] ,[OperationStart], [OperationStop], [Duration]) " +
                $"VALUES (@Id, @No, @Machine, @Date, @Shift, @OrderNo, @Material, @TrainNo, @ProcessType, @ProcessDescription, @LengthIn, @LengthOut, @OperationStart, @OperationStop, @Duration)";
            await _context.ExecuteAsync(query, models); 
        }
    }
}
