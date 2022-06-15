using Com.Danliris.ETL.Service.Models.DashboardPrintingModels;
using Com.Danliris.ETL.Service.Tools;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Com.Danliris.ETL.Service.DBAdapters.PrintingAdapters
{
    public class EngravingAdapter : IEngravingAdapter
    {
        private readonly ISqlDataContext<DashBoardEngravingModel> context;
        public EngravingAdapter(IServiceProvider service)
        {
            context = service.GetService<ISqlDataContext<DashBoardEngravingModel>>();
        }

        public async Task DeleteByMonthAndYear(DateTime periode)
        {            
            var query = $"DELETE FROM [dbo].[DashboardEngraving] WHERE MONTH(Date)=@month AND YEAR(Date)=@year";
            await context.QueryAsync(query, new { month = periode.Month, year = periode.Year });
        }

        public async Task InsertBulk(IEnumerable<DashBoardEngravingModel> models)
        {
            var query = $"INSERT INTO [dbo].[DashboardEngraving]([ID] ,[No] ,[Date],[Shift],[Group],[Machine],[SPNo],[TotalProductionScreen],[TotalRepairScreen]) VALUES(@Id,@No,@Date,@Shift,@Group,@Machine,@SPNo,@TotalProductionScreen,@TotalRepairScreen)";
            var result = await context.ExecuteAsync(query, models);
        }
    }
}
