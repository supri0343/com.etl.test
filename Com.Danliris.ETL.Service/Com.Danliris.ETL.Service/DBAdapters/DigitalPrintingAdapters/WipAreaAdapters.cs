using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using System;
using System.Threading.Tasks;
using Com.Danliris.ETL.Service.ExcelModels;
using Microsoft.Extensions.DependencyInjection;
using Com.Danliris.ETL.Service.Tools;

namespace Com.Danliris.ETL.Service.DBAdapters.DigitalPrintingAdapters
{
    public class WipAreaAdapters : IWipAreaAdapter
    {
        //private readonly IDbConnection _connection;

        private readonly ISqlDataContext<WipArea> context;
        public WipAreaAdapters(IServiceProvider service)
        {
            context = service.GetService<ISqlDataContext<WipArea>>();
        }
        public async Task InsertBulk(IEnumerable<WipArea> models) {
            //_connection.Open();
            var query = $"insert into [dbo].[DashboardWipArea]([ID],[No],[Date],[SPNo],[Material],[TrainNo],[QtyInMeter],[BQ],[BS],[Activity],[Destination])VALUES (@ID,@No,@Date,@SPNo,@Material,@TrainNo,@QtyInMeter,@BQ,@BS,@Activity,@Destination)";
            var result = await context.ExecuteAsync(query, models);
            //    var result = await _connection.QueryAsync<WipArea>(sql, wipArea);
            //    _connection.Close();
            //    return result.ToList();
        }

        public async Task DeleteByMonthAndYear(DateTime periode) {
            try
            {
                var query = $"DELETE from [dbo].[DashboardWipArea] WHERE month([Date]) = @month AND year([Date]) = @year";
                await context.QueryAsync(query, new { month = periode.Month, year = periode.Year });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}