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
    public class DyeingTestAdapter : IDyeingTestAdapter
    {
        private readonly ISqlDataContext<DashboardDyeingTest> context;
        public DyeingTestAdapter(IServiceProvider serviceProvider)
        {
            context = serviceProvider.GetService<ISqlDataContext<DashboardDyeingTest>>();
        }

        public async Task DeleteByMonthAndYear(DateTime periode)
        {
            var query = $"DELETE FROM [dbo].[DashboardDyeingTest] WHERE MONTH(Date)=@month AND YEAR(Date)=@year";
            await context.QueryAsync(query, new { month = periode.Month, year = periode.Year });
        }

        public async Task InsertBulk(IEnumerable<DashboardDyeingTest> models)
        {
            var query = $"INSERT INTO [dbo].[DashboardDyeingTest] ( [ID] ,[No] ,[Date] ,[SPP] ,[Colour] ,[Material] ,[Buyer] ,[BatchQty] ,[ResultPhysicalTest] ,[ShrinkageTestMethod] ,[ShrinkageWarp] ,[ShrinkageWeft] ,[TearStrengthWarp] ,[TearStrengthWeft] ,[TensileStrengthWarp] ,[TensileStrengthWeft] ,[Gramasi] ,[PillingMethod] ,[MartindaleAbrasion] ,[DPRating] ,[Decision] ,[ReasonFail] ,[FormaldehydeContent] ,[PHValue] ,[WashingStaining] ,[WashingColourChange] ,[Staining] ,[PersAlkaliColourChange] ,[PersAcidStaining] ,[PersAcidColourChange] ,[RubbingDry] ,[RubbingWet] ,[HotPress] ,[Decision2] ,[ReasonFail2] ,[Status])" 
            + $"VALUES (@Id ,@No ,@Date ,@SPP ,@Colour ,@Material ,@Buyer ,@BatchQty ,@ResultPhysicalTest ,@ShrinkageTestMethod ,@ShrinkageWarp ,@ShrinkageWeft ,@TearStrengthWarp ,@TearStrengthWeft ,@TensileStrengthWarp ,@TensileStrengthWeft ,@Gramasi ,@PillingMethod ,@MartindaleAbrasion ,@DPRating ,@Decision ,@ReasonFail ,@FormaldehydeContent ,@PHValue ,@WashingStaining ,@WashingColourChange ,@Staining ,@PersAlkaliColourChange ,@PersAcidStaining ,@PersAcidColourChange ,@RubbingDry ,@RubbingWet ,@HotPress ,@Decision2 ,@ReasonFail2 ,@Status)";
            await context.ExecuteAsync(query, models);
        }
    }
}