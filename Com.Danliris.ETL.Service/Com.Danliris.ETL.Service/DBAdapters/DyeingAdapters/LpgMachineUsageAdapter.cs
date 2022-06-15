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
    public class LpgMachineUsageAdapter : ILpgMachineUsageAdapter
    {

        private readonly ISqlDataContext<DashboardLPGMachineUsage> context;
        public LpgMachineUsageAdapter(IServiceProvider serviceProvider)
        {
            context = serviceProvider.GetService<ISqlDataContext<DashboardLPGMachineUsage>>();
        }
        
        public async Task DeleteByMonthAndYear(DateTime periode)
        {            
            var query = $"DELETE FROM [dbo].[DashboardLPGMachineUsage] WHERE MONTH(Date)=@month AND YEAR(Date)=@year";
            await context.QueryAsync(query, new { month = periode.Month, year = periode.Year });
        }

        public async Task InsertBulk(IEnumerable<DashboardLPGMachineUsage> models)
        {
            var query = $"INSERT INTO [dbo].[DashboardLPGMachineUsage] ([ID] ,[No] ,[Week] ,[Date] ,[Group] ,[Shift] ,[TimeRequiredMin] ,[TimeRequiredHour] ,[CostPerKg] ,[KgPerHour] ,[TotalCost]) VALUES (@Id, @No, @Week, @Date, @Group, @Shift, @TimeRequiredMin, @TimeRequiredHour, @CostPerKg, @KgPerHour, @TotalCost)";
            await context.ExecuteAsync(query, models);
        }
    }
}