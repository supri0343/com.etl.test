using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Com.Danliris.ETL.Service.ExcelModels.DashboardKoranLaborat;
using Com.Danliris.ETL.Service.Tools;
using Dapper;
using Microsoft.Extensions.DependencyInjection;

namespace Com.Danliris.ETL.Service.DBAdapters.KoranLaboratAdapters
{
    public class RnDTestResultAdapter : IRnDTestResultAdapter
    {
        private readonly ISqlDataContext<DashboardRnDTestResult> context;
        public RnDTestResultAdapter(IServiceProvider serviceProvider)
        {
            context = serviceProvider.GetService<ISqlDataContext<DashboardRnDTestResult>>();
        }

        public async Task DeleteByMonthAndYear(DateTime periode)
        {
            var query = $"DELETE FROM [dbo].[DashboardRnDTestResults] WHERE MONTH(Date)=@month AND YEAR(Date)=@year";
            await context.QueryAsync(query, new { month = periode.Month, year = periode.Year });
        }

        public async Task InsertBulk(IEnumerable<DashboardRnDTestResult> models)
        {
            var query = $"INSERT INTO [dbo].[DashboardRnDTestResults] ([ID] ,[No] ,[Date] ,[SOFSPP] ,[Construction] ,[Tegewa] ,[FabricPHDrop] ,[AbsorbtionDrop] ,[Capillary] ,[Whiteness] ,[Shading31] ,[Shading14] ,[Shading42] ,[CapillarityLeft] ,[CapillarityCenter] ,[CapillarityRight])" 
            + $"VALUES (@Id ,@No ,@Date ,@SOFSPP ,@Construction ,@Tegewa ,@FabricPHDrop ,@AbsorbtionDrop ,@Capillary ,@Whiteness ,@Shading31 ,@Shading14 ,@Shading42 ,@CapillarityLeft ,@CapillarityCenter ,@CapillarityRight)";
            await context.ExecuteAsync(query, models);
        }
    }
}