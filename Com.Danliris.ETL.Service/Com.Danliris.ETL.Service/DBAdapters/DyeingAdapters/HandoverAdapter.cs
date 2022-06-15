using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Com.Danliris.ETL.Service.ExcelModels.DashboardDyeingModels;
using System.Threading.Tasks;
using Com.Danliris.ETL.Service.Tools;
using Microsoft.Extensions.DependencyInjection;

namespace Com.Danliris.ETL.Service.DBAdapters.DyeingAdapters
{
    public class HandoverAdapter : IHandoverAdapter
    {
        private readonly ISqlDataContext<DashboardHandOverArea> context;
        public HandoverAdapter(IServiceProvider serviceProvider)
        {
            context = serviceProvider.GetService<ISqlDataContext<DashboardHandOverArea>>();
        }

        public async Task DeleteByMonthAndYear(DateTime periode)
        {            
            var query = $"DELETE FROM [dbo].[DashboardHandOverArea] WHERE MONTH(Date)=@month AND YEAR(Date)=@year";
            await context.QueryAsync(query, new { month = periode.Month, year = periode.Year });
        }

        public async Task InsertBulk(IEnumerable<DashboardHandOverArea> models)
        {
            var query = $"INSERT INTO [dbo].[DashboardHandOverArea] ([ID] ,[No] ,[Date] ,[OrderNo] ,[Construction] ,[Qty] ,[TrainNo] ,[Grade] ,[Activity] ,[ProductionSubcon] ,[Destination] ,[Reprocess] ,[Description] ,[Area] ,[Color] ,[DescriptionSubcon] ,[A] ,[B] ,[C] ,[BS] ,[Total] ,[FabricWidth]) VALUES (@Id, @No, @Date, @OrderNo, @Construction, @Qty, @TrainNo, @Grade, @Activity, @ProductionSubcon, @Destination, @Reprocess, @Description, @Area, @Color, @DescriptionSubcon, @A, @B, @C, @BS, @Total, @FabricWidth)";
            await context.ExecuteAsync(query, models);
        }
    }
}