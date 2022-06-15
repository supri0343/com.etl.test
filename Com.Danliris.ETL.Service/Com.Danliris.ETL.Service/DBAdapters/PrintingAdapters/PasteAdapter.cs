using Com.Danliris.ETL.Service.Models.DashboardPrintingModels;
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

namespace Com.Danliris.ETL.Service.DBAdapters.PrintingAdapters
{
    public class PasteAdapter : IPasteAdapter
    {
        private readonly ISqlDataContext<DashBoardPasteModel> context;
        public PasteAdapter(IServiceProvider service)
        {
            context = service.GetService<ISqlDataContext<DashBoardPasteModel>>();
        }

        public async Task DeleteByMonthAndYear(DateTime periode)
        {
            var query = $"DELETE FROM [dbo].[DashboardPaste] WHERE MONTH(Date)=@month AND YEAR(Date)=@year";
            await context.QueryAsync(query, new { month = periode.Month, year = periode.Year });
        }

        public async Task InsertBulk(IEnumerable<DashBoardPasteModel> models)
        {            
            var query = $"INSERT INTO [dbo].[DashboardPaste] ([ID] ,[No],[Date],[Production] ,[Shift] ,[Group] ,[SPNo] ,[NewProductionDrum] ,[ReworkDrum] ,[ThicknerProductionDrum],[PVAGlueProductionDrum]) VALUES(@Id,@No, @Date, @Production , @Shift, @Group, @SPNo, @NewProductionDrum,@ReworkDrum, @ThicknerProductionDrum, @PVAGlueProductionDrum)";
            var result = await context.ExecuteAsync(query, models);
        }
    }
}
