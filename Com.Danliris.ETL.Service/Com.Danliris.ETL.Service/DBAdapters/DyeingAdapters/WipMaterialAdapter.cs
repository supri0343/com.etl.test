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
    public class WipMaterialAdapter : IWipMaterialAdapter
    {
        private readonly ISqlDataContext<DashboardWIPMaterial> context;
        public WipMaterialAdapter(IServiceProvider serviceProvider)
        {
            context = serviceProvider.GetService<ISqlDataContext<DashboardWIPMaterial>>();
        }

        public async Task DeleteByMonthAndYear(DateTime periode)
        {
            var query = $"DELETE FROM [dbo].[DashboardWIPMaterial] WHERE MONTH(Date)=@month AND YEAR(Date)=@year";
            await context.QueryAsync(query, new { month = periode.Month, year = periode.Year });
        }

        public async Task InsertBulk(IEnumerable<DashboardWIPMaterial> models)
        {
            var query = $"INSERT INTO [dbo].[DashboardWIPMaterial] ([ID] ,[No] ,[Date] ,[Machine] ,[SPNo] ,[Material] ,[TrainNo] ,[Color] ,[Length] ,[DescriptionProcess]) VALUES (@Id, @No, @Date, @Machine, @SpNo, @Material, @TrainNo, @Color, @Length, @DescriptionProcess)";
            await context.ExecuteAsync(query, models);
        }
    }
}