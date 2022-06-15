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
    public class MaterialPreparingAdapter : IMaterialPreparingAdapter
    {
        //private readonly IDbConnection _connection;
        private readonly ISqlDataContext<IncomingMaterialPreparingModel> context;
        public MaterialPreparingAdapter(IServiceProvider service)
        {
            context = service.GetService<ISqlDataContext<IncomingMaterialPreparingModel>>();
        }

        public async Task DeleteByMonthAndYear(DateTime periode)
        {
            var query = $"DELETE FROM [dbo].[DashboardIncomingMaterialPreparing] WHERE MONTH(Date)=@month AND YEAR(Date)=@year";
            await context.QueryAsync(query, new { month = periode.Month, year = periode.Year });
        }

        public async Task InsertBulk(IEnumerable<IncomingMaterialPreparingModel> models)
        {
            var query = $"INSERT INTO [dbo].[DashboardIncomingMaterialPreparing] ([ID],[No],[Activity],[DPRFP],[Date],[Shift],[Group],[SPNo],[Construction],[Color],[TrainNo],[PiecesNo],[Grade],[Meter],[SealDate],[SewingDate],[RollDate],[Time],[PretreatmentOutDate],[PretreatmentOutTotal]) VALUES(@Id,@No,@Activity,@DPRFP,@Date,@Shift,@Group,@SPNo,@Construction,@Color,@TrainNo,@PiecesNo,@Grade,@Meter,@SealDate,@SewingDate,@RollDate,@Time,@PretreatmentOutDate,@PretreatmentOutTotal)";
            var result = await context.ExecuteAsync(query, models);
        }
    }
}
