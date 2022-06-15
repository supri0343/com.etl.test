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
    public class LabDipAdapter : ILabDipAdapter
    {
        private readonly ISqlDataContext<DashboardLabDip> context;
        public LabDipAdapter(IServiceProvider serviceProvider)
        {
            context = serviceProvider.GetService<ISqlDataContext<DashboardLabDip>>();
        }

        public async Task DeleteByMonthAndYear(DateTime periode)
        {
            var query = $"DELETE FROM [dbo].[DashboardLabDip] WHERE MONTH(Date)=@month AND YEAR(Date)=@year";
            await context.QueryAsync(query, new { month = periode.Month, year = periode.Year });
        }

        public async Task InsertBulk(IEnumerable<DashboardLabDip> models)
        {
            var query = $"INSERT INTO [dbo].[DashboardLabDip] ( [ID] ,[No] ,[Date] ,[Team] ,[LabdipNo] ,[Buyer] ,[ColorLevel] ,[Color] ,[ColorDescription] ,[YarnFabric] ,[FabricType] ,[ProcessType] ,[ProcessingTime] ,[MatchingTotalDisperse] ,[MatchingTotalReactive2] ,[DeliveryPlan] ,[DeliveryRealization] ,[LeadTime] ,[Description] ,[BuyerAcc])" 
            + $"VALUES (@Id ,@No ,@Date ,@Team ,@LabdipNo ,@Buyer ,@ColorLevel ,@Color ,@ColorDescription ,@YarnFabric ,@FabricType ,@ProcessType ,@ProcessingTime ,@MatchingTotalDisperse ,@MatchingTotalReactive2 ,@DeliveryPlan ,@DeliveryRealization ,@LeadTime ,@Description ,@BuyerAcc)";
            await context.ExecuteAsync(query, models);
        }
    }
}