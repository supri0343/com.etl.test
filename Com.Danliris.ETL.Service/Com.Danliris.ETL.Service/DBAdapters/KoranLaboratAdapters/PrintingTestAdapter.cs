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
    public class PrintingTestAdapter : IPrintingTestAdapter
    {
        private readonly ISqlDataContext<DashboardPrintingTest> context;
        public PrintingTestAdapter(IServiceProvider serviceProvider)
        {
            context = serviceProvider.GetService<ISqlDataContext<DashboardPrintingTest>>();
        }
        
        public async Task DeleteByMonthAndYear(DateTime periode)
        {
            var query = $"DELETE FROM [dbo].[DashboardPrintingTest] WHERE MONTH(Date)=@month AND YEAR(Date)=@year";
            await context.QueryAsync(query, new { month = periode.Month, year = periode.Year });
        }

        public async Task InsertBulk(IEnumerable<DashboardPrintingTest> models)
        {
            var query = $"INSERT INTO [dbo].[DashboardPrintingTest] ( [ID] ,[No] ,[Date] ,[SPP] ,[Colour] ,[Material] ,[Buyer] ,[BatchQty] ,[ResultPhysicalTest] ,[ShrinkageTestMethod] ,[ShrinkageWarp] ,[ShrinkageWeft] ,[TearStrengthWarp] ,[TearStrengthWeft] ,[TensileStrengthWarp] ,[TensileStrengthWeft] ,[Gramasi] ,[PillingMethod] ,[MartindaleAbrasion] ,[DPRating] ,[Decision] ,[ReasonFail] ,[FormaldehydeContent] ,[PHValue] ,[WashingStaining] ,[WashingColourChange] ,[Staining] ,[PersAlkaliColourChange] ,[PersAcidStaining] ,[PersAcidColourChange] ,[RubbingDry] ,[RubbingWet] ,[HotPress] ,[Decision2] ,[ReasonFail2] ,[Status])" 
            + $"VALUES (@Id ,@No ,@Date ,@SPP ,@Colour ,@Material ,@Buyer ,@BatchQty ,@ResultPhysicalTest ,@ShrinkageTestMethod ,@ShrinkageWarp ,@ShrinkageWeft ,@TearStrengthWarp ,@TearStrengthWeft ,@TensileStrengthWarp ,@TensileStrengthWeft ,@Gramasi ,@PillingMethod ,@MartindaleAbrasion ,@DPRating ,@Decision ,@ReasonFail ,@FormaldehydeContent ,@PHValue ,@WashingStaining ,@WashingColourChange ,@Staining ,@PersAlkaliColourChange ,@PersAcidStaining ,@PersAcidColourChange ,@RubbingDry ,@RubbingWet ,@HotPress ,@Decision2 ,@ReasonFail2 ,@Status)";
            await context.ExecuteAsync(query, models);
        }
    }
}