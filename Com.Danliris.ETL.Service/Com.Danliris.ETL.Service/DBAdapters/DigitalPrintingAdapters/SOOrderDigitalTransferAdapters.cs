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

namespace Com.Danliris.ETL.Service.DBAdapters.DigitalPrintingAdapters
{
    public class SOOrderDigitalTransferAdapters : ISOOrderDigitalTransferAdapter
    {
        //private readonly IDbConnection _connection;
        private readonly ISqlDataContext<SOOrderDigitalTransfer> context;
        public SOOrderDigitalTransferAdapters(IServiceProvider service)
        {
            context = service.GetService<ISqlDataContext<SOOrderDigitalTransfer>>();
        }

        public async Task InsertBulk(IEnumerable<SOOrderDigitalTransfer> models) {
            //_connection.Open();
            var query = $"INSERT INTO [dbo].[DashboardSOOrderDigitalTransfer]([ID],[No],[Date],[SONo],[PatternCode],[ProcessType],[SerianTotal],[DesignType],[DesignSize],[Buyer],[Material],[DesignFormat],[RUN],[DeliveryRequestDate],[AccDate],[LeadTime],[Status],[Description]) VALUES(@ID,@No,@Date,@SONo,@PatternCode,@ProcessType,@SerianTotal,@DesignType,@DesignSize,@Buyer,@Material,@DesignFormat,@RUN,@DeliveryRequestDate,@AccDate,@LeadTime,@Status, @Description)";
            var result = await context.ExecuteAsync(query, models);
            //var result = await _connection.QueryAsync<SOOrderDigitalTransfer>(sql, sOOrderDigitalTransfer);
            //_connection.Close();
            //return result.ToList();
        }

        public async Task  DeleteByMonthAndYear(DateTime periode) {
            try
            {
                //_connection.Open();
                var query = ($"DELETE from [dbo].[DashboardSOOrderDigitalTransfer]WHERE month([Date]) = @month AND year([Date]) = @year");
                await context.QueryAsync(query, new { month = periode.Month, year = periode.Year });
                //var result = await _connection.QueryAsync<bool>(sql, new { month = periode.Month, year = periode.Year });
                //_connection.Close();
                //return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}