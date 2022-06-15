using Com.Danliris.ETL.Service.Models.DashboardPretreatmentModels;
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

namespace Com.Danliris.ETL.Service.DBAdapters.PretreatmentAdapters
{
    public class DeliveryBetweenAdapter : IDeliveryBetweenAdapter
    {
        //private readonly IDbConnection _connection;
        private readonly ISqlDataContext<DeliveryBetweenAreaModel> context;
        public DeliveryBetweenAdapter(IServiceProvider service)
        {
            context = service.GetService<ISqlDataContext<DeliveryBetweenAreaModel>>();
        }

        public async Task DeleteByMonthAndYear(DateTime periode)
        {
            var query = $"DELETE FROM [dbo].[DashboardDeliveryBetweenArea] WHERE MONTH(Date)=@month AND YEAR(Date)=@year";
            await context.QueryAsync(query, new { month = periode.Month, year = periode.Year });
        }

        public async Task InsertBulk(IEnumerable<DeliveryBetweenAreaModel> models)
        {
            var query = $"INSERT INTO [dbo].[DashboardDeliveryBetweenArea] ([ID],[No],[Date],[SP],[Construction],[QTY],[Train],[Grade],[Activity],[ProductionSubcon],[Destination],[Reprocess],[Desc],[Area],[Description],[Problem]) VALUES (@Id,@No,@Date,@SP,@Construction,@QTY,@Train,@Grade,@Activity,@ProductionSubcon,@Destination,@Reprocess,@Desc,@Area,@Description,@Problem)";
            var result = await context.ExecuteAsync(query, models);
        }
    }
}
