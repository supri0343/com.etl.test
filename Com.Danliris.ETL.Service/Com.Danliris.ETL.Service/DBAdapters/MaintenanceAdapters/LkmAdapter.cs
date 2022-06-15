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
    public class LkmAdapter : IDashboardLkmAdapter
    {
        //private readonly IDbConnection _connection;
        private readonly ISqlDataContext<DashboardLkmModel> context;
        public LkmAdapter(IServiceProvider service)
        {
            context = service.GetService<ISqlDataContext<DashboardLkmModel>>();
        }

        public async Task DeleteByMonthAndYear(DateTime periode)
        {
            var query = $"DELETE FROM [dbo].[DashboardLKM] WHERE MONTH(Date)=@month AND YEAR(Date)=@year";
            await context.QueryAsync(query, new { month = periode.Month, year = periode.Year });
        }

        public async Task InsertBulk(IEnumerable<DashboardLkmModel> models)
        {
            var query = $"INSERT INTO [dbo].[DashboardLKM] ([ID],[No],[Date],[Section],[Machine],[Problem],[Action],[UsageSparepart],[Description],[Operator1],[Operator2],[Operator3],[KnownBy],[Team],[RemaintenanceDate]) VALUES (@Id,@No,@Date,@Section,@Machine,@Problem,@Action,@UsageSparepart,@Description,@Operator1,@Operator2,@Operator3,@KnownBy,@Team,@RemaintenanceDate)";
            var result = await context.ExecuteAsync(query, models);
        }
    }
}
