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
    public class ReprocessAdapter : IReprocessAdapter
    {
        //private readonly IDbConnection _connection;
        private readonly ISqlDataContext<ReprocessModel> context;
        public ReprocessAdapter(IServiceProvider service)
        {
            context = service.GetService<ISqlDataContext<ReprocessModel>>();
        }

        public async Task DeleteByMonthAndYear(DateTime periode)
        {
            var query = $"DELETE FROM [dbo].[DashboardReprocess] WHERE MONTH(Date)=@month AND YEAR(Date)=@year";
            await context.QueryAsync(query, new { month = periode.Month, year = periode.Year });
        }

        public async Task InsertBulk(IEnumerable<ReprocessModel> models)
        {
            var query = $"INSERT INTO [dbo].[DashboardReprocess] ([ID],[No],[Date],[Shift],[Group],[OrderNo],[Material],[TrainNo],[QtyIn],[Machine],[ProcessType],[Reprocess],[Problem]) VALUES(@Id,@No,@Date,@Shift,@Group,@OrderNo,@Material,@TrainNo,@QtyIn,@Machine,@ProcessType,@Reprocess,@Problem)";
            var result = await context.ExecuteAsync(query, models);
        }
    }
}
