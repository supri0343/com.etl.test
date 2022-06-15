using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System;
using Dapper;
using System.Threading.Tasks;
using Com.Danliris.ETL.Service.ExcelModels;
using Microsoft.Extensions.DependencyInjection;
using Com.Danliris.ETL.Service.Tools;

namespace Com.Danliris.ETL.Service.DBAdapters.DigitalPrintingAdapters
{
    public class DailyDeliveryAdapters : IDailyDeliveryAdapter
    {
        //private readonly IDbConnection _connection;
        private readonly ISqlDataContext<DailyDelivery> context;
        public DailyDeliveryAdapters(IServiceProvider service)
        {
            context = service.GetService<ISqlDataContext<DailyDelivery>>();
        }

        public async Task InsertBulk(IEnumerable<DailyDelivery> models) {
            
            var query = ($"INSERT INTO [dbo].[DashboardDailyDelivery]([ID],[No],[Date],[SPNo],[PatternCode],[Material],[ProcessType],[A],[B],[C],[BS],[Aval],[Total],[DescriptionBS],[QtyBS])values (@ID,@No,@Date,@SPNo,@PatternCode,@Material,@ProcessType,@A,@B,@C,@BS,@Aval,@Total,@DescriptionBS,@QtyBS)");
            await context.ExecuteAsync(query, models);
        }

        public async Task DeleteByMonthAndYear(DateTime periode) {
            try
            {

                var query = $"DELETE from [dbo].[DashboardDailyDelivery] WHERE month([Date]) = @month AND year([Date]) = @year";
                await context.QueryAsync(query, new { month = periode.Month, year = periode.Year });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}