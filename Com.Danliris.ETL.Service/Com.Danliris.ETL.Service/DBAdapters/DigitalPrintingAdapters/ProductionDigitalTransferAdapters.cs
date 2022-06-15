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
    public class ProductionDigitalTransferAdapters : IProductionDigitalTransferAdapter
    {
        //private readonly IDbConnection _connection;
        private readonly ISqlDataContext<ProductionDigitalTransfer> context;
        public ProductionDigitalTransferAdapters(IServiceProvider service)
        {
            context = service.GetService<ISqlDataContext<ProductionDigitalTransfer>>();
        }
        public async Task InsertBulk(IEnumerable<ProductionDigitalTransfer> models) {            
            var query = @"INSERT INTO [dbo].[DashboardProductionDigitalTransfer] ([ID], [No], [Date], [Shift], [Group], [SPNo], [ProcessType], [Material], [ColorCW], [PatternCode], [QtyInMeter], [Start], [Finish], [LeadTime] , [A], [B],[C], [BS], DescriptionBS)values (@ID, @No, @Date, @Shift, @Group, @SPNo, @ProcessType, @Material, @ColorCW, @PatternCode, @QtyInMeter, @Start, @Finish, @LeadTime, @A, @B, @C, @BS, @DescriptionBS)";
            await context.ExecuteAsync(query, models);
            
        }

        public async Task DeleteByMonthAndYear(DateTime periode) {
            try
            {
                //_connection.Open();
                var query = ("DELETE from [dbo].[DashboardProductionDigitalTransfer]WHERE month([Date]) = @month AND year([Date]) = @year");
                await context.QueryAsync(query, new { month = periode.Month, year = periode.Year });
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