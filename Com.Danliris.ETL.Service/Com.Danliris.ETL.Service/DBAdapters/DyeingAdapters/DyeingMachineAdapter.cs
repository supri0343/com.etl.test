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
    public class DyeingMachineAdapter : IDyeingMachineAdapter
    {
        
        private readonly ISqlDataContext<DashboardDyeingMachine> context;
        public DyeingMachineAdapter(IServiceProvider serviceProvider)
        {
            context = serviceProvider.GetService<ISqlDataContext<DashboardDyeingMachine>>();
        }

        public async Task DeleteByMonthAndYear(DateTime periode)
        {
            var query = $"DELETE FROM [dbo].[DashboardDyeingMachine] WHERE MONTH(Date)=@month AND YEAR(Date)=@year";
            await context.QueryAsync(query, new { month = periode.Month, year = periode.Year });
        }

        public async Task InsertBulk(IEnumerable<DashboardDyeingMachine> dashboardDyeingMachines)
        {
            var query = $"INSERT INTO [dbo].[DashboardDyeingMachine] ([ID] ,[No] ,[Machine] ,[Date] ,[Shift] ,[Group] ,[OrderNo] ,[Material] ,[Color] ,[TrainNo] ,[ProcessType] ,[ProcessDescription] ,[Speed] ,[QtyInMeter] ,[QtyOutMeter]) VALUES (@Id, @No, @Machine, @Date, @Shift, @Group, @OrderNo, @Material, @Color, @TrainNo, @ProcessType, @ProcessDescription, @Speed, @QtyInMeter, @QtyOutMeter)";
            await context.ExecuteAsync(query, dashboardDyeingMachines);
        }
    }
}