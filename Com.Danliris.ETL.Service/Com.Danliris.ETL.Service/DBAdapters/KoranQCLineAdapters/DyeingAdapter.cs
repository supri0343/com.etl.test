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
    public class DyeingAdapter : IDyeingAdapter
    {
        
        private readonly ISqlDataContext<Dyeing> context;
        public DyeingAdapter(IServiceProvider serviceProvider)
        {
            context = serviceProvider.GetService<ISqlDataContext<Dyeing>>();
        }

        // public async Task<IEnumerable<Dyeing>> BulkInsertUpdate(Dyeing dyeing) {
        //     _connection.Open();
            // var sql = (@"
            //     insert into [dbo].[DashboardDyeing]
            //     ([ID],[No],[Date],[Operator],[GroupIM],[ShiftIM],[Machine],[MachineGroup],[OrderNo],
            //     [Material],[PatternColor],[Buyer],[TrainNo],[Total],[GradeA],[GradeB],[GradeC],
            //     [GradeBS],[Description1],[Description2],[Description3])
            //     values (@ID,@No,@Date,@Operator,@GroupIM,@ShiftIM,@Machine,@MachineGroup,@OrderNo,
            //     @Material,@PatternColor,@Buyer,@TrainNo,@Total,@GradeA,@GradeB,@GradeC,@GradeBS,
            //     @Description1,@Description2,@Description3)
            // ");
            // var result = await _connection.QueryAsync<Dyeing>(sql, dyeing);
        //     _connection.Close();
        //     return result.ToList();
        // }

        // public async Task<bool> BatchDelete(DateTime periode) {
        //     try
        //     {
        //         _connection.Open();
        //         var sql = (@"
        //             DELETE from [dbo].[DashboardDyeing]
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
            var query = $"DELETE FROM [dbo].[DashboardDyeing] WHERE MONTH(Date)=@month AND YEAR(Date)=@year";
            await context.QueryAsync(query, new { month = periode.Month, year = periode.Year });
        }

        public async Task InsertBulk(IEnumerable<Dyeing> models)
        {     
            var sql = (@"
                insert into [dbo].[DashboardDyeing]
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