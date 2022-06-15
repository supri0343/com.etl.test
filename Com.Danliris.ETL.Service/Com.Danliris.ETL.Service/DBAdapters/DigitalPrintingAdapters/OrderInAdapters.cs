using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using System.Threading.Tasks;
using Com.Danliris.ETL.Service.ExcelModels;
using Microsoft.Extensions.DependencyInjection;
using Com.Danliris.ETL.Service.Tools;
using Com.Danliris.ETL.Service.DBAdapters.DigitalPrintingAdapters;

namespace Com.Danliris.ETL.Service.DBAdapters.DigitalPrintingAdapters
{
    public class OrderInAdapters : IOrderInAdapter
    {
        //private readonly IDbConnection _connection;
        private readonly ISqlDataContext<OrderIn> context;
        public OrderInAdapters(IServiceProvider service)
        {
            context = service.GetService<ISqlDataContext<OrderIn>>();
        }

        public async Task InsertBulk(IEnumerable<OrderIn> models) {
            //_connection.Open();
            var query = $"INSERT INTO [dbo].[DashboardOrderIn]([ID],[No],[Date],[SPNo],[Design],[ProcessType],[QtyInMeter],[Material],[YarnNo],[Width],[PatternCode],[Reference],[Series],[Buyer],[MaterialSource],[OrderCreateDate],[OrderEntryProductionDate],[DeliveryRequestDate],[ProductionDate],[DeliveryRealizationDate],[Status],[LeadTime]) VALUES(@ID,@No,@Date,@SPNo,@Design,@ProcessType,@QtyInMeter,@Material,@YarnNo,@Width,@PatternCode,@Reference,@Series,@Buyer,@MaterialSource,@OrderCreateDate,@OrderEntryProductionDate,@DeliveryRequestDate,@ProductionDate,@DeliveryRealizationDate,@Status,@LeadTime)";
            var result = await context.ExecuteAsync(query, models);
            //_connection.Close();
            //return result.ToList();
        }

        public async Task DeleteByMonthAndYear(DateTime periode) {
            try
            {
                //_connection.Open();
                var query = $"DELETE from [dbo].[DashboardOrderIn] WHERE month([Date]) = @month AND year([Date]) = @year";
                await context.QueryAsync(query, new { month = periode.Month, year = periode.Year });
                // _connection.Close();
                //return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}