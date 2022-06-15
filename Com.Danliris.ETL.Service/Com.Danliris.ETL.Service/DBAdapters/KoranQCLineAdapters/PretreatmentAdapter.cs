using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using System.Threading.Tasks;
using Com.Danliris.ETL.Service.ExcelModels.KoranQCLine;
using Microsoft.Extensions.DependencyInjection;
using Com.Danliris.ETL.Service.Tools;

namespace Com.Danliris.ETL.Service.DBAdapters.KoranQCLineAdapters
{
    public class PretreatmentAdapter : IPretreatmentAdapter
    {
        private readonly ISqlDataContext<Pretreatment> context;
        public PretreatmentAdapter(IServiceProvider serviceProvider)
        {
            context = serviceProvider.GetService<ISqlDataContext<Pretreatment>>();
        }

        // public async Task<IEnumerable<Pretreatment>> BulkInsertUpdate(Pretreatment pretreatment) {
        //     _connection.Open();
        //     var sql = (@"
        //         insert into [dbo].[DashboardPretreatment]
        //         ([ID],[No],[Date],[Operator],[GroupIM],[ShiftIM],[Machine],[MachineGroup],[OrderNo],
        //         [Material],[PatternColor],[Buyer],[TrainNo],[Total],[GradeA],[GradeB],[GradeC],
        //         [GradeBS],[Description1],[Description2],[Description3])
        //         values (@ID,@No,@Date,@Operator,@GroupIM,@ShiftIM,@Machine,@MachineGroup,@OrderNo,
        //         @Material,@PatternColor,@Buyer,@TrainNo,@Total,@GradeA,@GradeB,@GradeC,@GradeBS,
        //         @Description1,@Description2,@Description3)
        //     ");
        //     var result = await _connection.QueryAsync<Pretreatment>(sql, pretreatment);
        //     _connection.Close();
        //     return result.ToList();
        // }

        // public async Task<bool> BatchDelete(DateTime periode) {
        //     try
        //     {
        //         _connection.Open();
        //         var sql = (@"
        //             DELETE from [dbo].[DashboardPretreatment]
        //             WHERE month([Date]) = @month AND year([Date]) = @year
        //         ");
        //         var result = await _connection.QueryAsync<bool>(sql, new { month = periode.Month, year = periode.Year });
        //         _connection.Close();
        //         return true;
        //     }
        //     catch (Exception ex)
        //     {
        //         throw ex;
        //     }
        // }

        public async Task DeleteByMonthAndYear(DateTime periode)
        {
            var query = $"DELETE FROM [dbo].[DashboardPretreatment] WHERE MONTH(Date)=@month AND YEAR(Date)=@year";
            await context.QueryAsync(query, new { month = periode.Month, year = periode.Year });
        }

        public async Task InsertBulk(IEnumerable<Pretreatment> models)
        {
            var sql = (@"
                insert into [dbo].[DashboardPretreatment]
                ([ID],[No],[Date],[Operator],[GroupIM],[ShiftIM],[Machine],[MachineGroup],[OrderNo],
                [Material],[PatternColor],[Buyer],[TrainNo],[Total],[GradeA],[GradeB],[GradeC],
                [GradeBS],[Description1],[Description2],[Description3])
                values (@ID,@No,@Date,@Operator,@GroupIM,@ShiftIM,@Machine,@MachineGroup,@OrderNo,
                @Material,@PatternColor,@Buyer,@TrainNo,@Total,@GradeA,@GradeB,@GradeC,@GradeBS,
                @Description1,@Description2,@Description3)
            ");
            await context.ExecuteAsync(sql, models);
        }
    }
}