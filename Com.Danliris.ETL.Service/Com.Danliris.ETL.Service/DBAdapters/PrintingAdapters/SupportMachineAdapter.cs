using Com.Danliris.ETL.Service.Models.DashboardPrintingModels;
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

namespace Com.Danliris.ETL.Service.DBAdapters.PrintingAdapters
{
    public class SupportMachineAdapter : ISupportMachineAdapter
    {
        private readonly ISqlDataContext<DashBoardSupportMachineModel> context;
        public SupportMachineAdapter(IServiceProvider service)
        {
            context = service.GetService<ISqlDataContext<DashBoardSupportMachineModel>>();
        }

        public async Task DeleteByMonthAndYear(DateTime periode)
        {
            var query = $"DELETE FROM [dbo].[DashboardSupportMachine] WHERE MONTH(Date)=@month AND YEAR(Date)=@year";
            await context.QueryAsync(query, new { month = periode.Month, year = periode.Year });
        }

        public async Task InsertBulk(IEnumerable<DashBoardSupportMachineModel> models)
        {
            var query = $"INSERT INTO [dbo].[DashboardSupportMachine] ([ID] ,[No],[Date],[Machine] ,[Shift] ,[Group] ,[OrderNo] ,[Material] ,[PatternCode] ,[TrainNo],[QtyIn],[BQ],[BS],[Speed]) VALUES(@Id,@No, @Date, @Machine, @Shift, @Group, @OrderNo, @Material,@PatternCode, @TrainNo, @QtyIn,@BQ,  @BS,  @Speed)";
            var result = await context.ExecuteAsync(query, models);
        }
    }
}
