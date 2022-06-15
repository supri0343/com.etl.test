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
    public class PrintingLabAdapter : IPrintingLabAdapter
    {
        private readonly ISqlDataContext<DashboardPrintingLab> context;
        public PrintingLabAdapter(IServiceProvider serviceProvider)
        {
            context = serviceProvider.GetService<ISqlDataContext<DashboardPrintingLab>>();
        }

        public async Task DeleteByMonthAndYear(DateTime periode)
        {
            var query = $"DELETE FROM [dbo].[DashboardPrintingLab] WHERE MONTH(Date)=@month AND YEAR(Date)=@year";
            await context.QueryAsync(query, new { month = periode.Month, year = periode.Year });
        }

        public async Task InsertBulk(IEnumerable<DashboardPrintingLab> models)
        {
            var query = $"INSERT INTO [dbo].[DashboardPrintingLab] ( [ID] ,[No] ,[Date], [SONo] ,[CodeRCFC] ,[NewReSO] ,[DesignCode] ,[Material] ,[Buyer] ,[Sales] ,[SentDate] ,[CW] ,[SPP1] ,[SPP2] ,[SPP3] ,[SPP4] ,[SPP5] ,[SPP6] ,[SPP7] ,[SPP8] ,[SPP9] ,[SPP10] ,[SPP11] ,[SPP12] ,[SPPTotal] ,[Status] ,[DownOrder] ,[AccDate] ,[FeedbackSales] ,[SalesDescription] ,[LabDescriptionLatePotential])" 
            + $"VALUES (@Id ,@No ,@Date ,@SONo ,@CodeRCFC ,@NewReSO ,@DesignCode ,@Material ,@Buyer ,@Sales ,@SentDate ,@CW ,@SPP1 ,@SPP2 ,@SPP3 ,@SPP4 ,@SPP5 ,@SPP6 ,@SPP7 ,@SPP8 ,@SPP9 ,@SPP10 ,@SPP11 ,@SPP12 ,@SPPTotal ,@Status ,@DownOrder ,@AccDate ,@FeedbackSales ,@SalesDescription ,@LabDescriptionLatePotential)";
            await context.ExecuteAsync(query, models);
        }
    }
}