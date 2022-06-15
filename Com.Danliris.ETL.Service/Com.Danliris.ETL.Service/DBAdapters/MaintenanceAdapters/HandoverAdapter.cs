using Com.Danliris.ETL.Service.Models.DashboardMaintenanceModels;
using Com.Danliris.ETL.Service.Tools;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Com.Danliris.ETL.Service.DBAdapters.MaintenanceAdapters
{
    public class HandoverAdapter : IDashboardHandoverAdapter
    {
        //private readonly IDbConnection _connection;
        private readonly ISqlDataContext<DashboardHandoverModel> context;

        public HandoverAdapter(IServiceProvider service)
        {
            context = service.GetService<ISqlDataContext<DashboardHandoverModel>>();
        }

        public async Task DeleteByMonthAndYear(DateTime periode)
        {
            var query = $"DELETE FROM [dbo].[DashboardHandOver] WHERE MONTH(Date)=@month AND YEAR(Date)=@year";
            await context.QueryAsync(query, new { month = periode.Month, year = periode.Year });
        }

        public async Task InsertBulk(IEnumerable<DashboardHandoverModel> models)
        {
            var query = $"INSERT INTO [dbo].[DashboardHandOver] ([ID],[No],[Date],[Machine],[Repair],[Result],[Team],[Implementer],[KasubsieVerification],[KasieVerification],[Production]) VALUES (@Id,@No,@Date,@Machine,@Repair,@Result,@Team,@Implementer,@KasubsieVerification,@KasieVerification,@Production)";
            var result = await context.ExecuteAsync(query, models);
        }
    }
}
