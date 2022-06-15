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
    public class MaterialTroubleAdapter : IMaterialTroubleAdapter
    {
        //private readonly IDbConnection _connection;
        private readonly ISqlDataContext<MaterialTroubleModel> context;
        public MaterialTroubleAdapter(IServiceProvider service)
        {
            context = service.GetService<ISqlDataContext<MaterialTroubleModel>>();
        }

        public async Task DeleteByMonthAndYear(DateTime periode)
        {
            var query = $"DELETE FROM [dbo].[DashboardMaterialTrouble] WHERE MONTH(Date)=@month AND YEAR(Date)=@year";
            await context.QueryAsync(query, new { month = periode.Month, year = periode.Year });
        }

        public async Task InsertBulk(IEnumerable<MaterialTroubleModel> models)
        {
            var query = $"INSERT INTO [dbo].[DashboardMaterialTrouble] ([ID],[No],[Date],[OrderNo],[Material],[Buyer],[BQ],[BS],[Total],[Problem]) VALUES(@Id,@No,@Date,@OrderNo,@Material,@Buyer,@BQ,@BS,@Total,@Problem)";
            var result = await context.ExecuteAsync(query, models);
        }
    }
}
