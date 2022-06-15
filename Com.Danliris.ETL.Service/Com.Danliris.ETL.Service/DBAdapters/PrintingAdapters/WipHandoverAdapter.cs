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
    public class WipHandoverAdapter : IWipHandoverAdapter
    {
        //private readonly IDbConnection _connection;
        private readonly ISqlDataContext<DashBoardWIPHandoverAreaModel> context;
        public WipHandoverAdapter(IServiceProvider service)
        {
            context = service.GetService<ISqlDataContext<DashBoardWIPHandoverAreaModel>>();
        }

        public async Task DeleteByMonthAndYear(DateTime periode)
        {
            var query = $"DELETE FROM [dbo].[DashboardWIPHandoverArea] WHERE MONTH(Date)=@month AND YEAR(Date)=@year";
            await context.QueryAsync(query, new { month = periode.Month, year = periode.Year });
        }

        public async Task InsertBulk(IEnumerable<DashBoardWIPHandoverAreaModel> models)
        {
            var query = $"INSERT INTO [dbo].[DashboardWIPHandoverArea] ([ID] ,[No]  ,[Date] ,[SPNo] ,[Construction] ,[TrainNo],[Activity],[ProductionSubcon] ,[Destination] , [Reprocess] ,[Description] ,[Area] ,[POK] ,[DescriptionSubcon],[ProcessTypeItem] ,[A],[B] ,[C] ,[BS] ,[Total] ,[YarnNo] ,[FabricWidth]) VALUES(@Id, @No,@Date,@SPNo,@Construction, @TrainNo,@Activity,@ProductionSubcon, @Destination,@Reprocess,@Description,@Area,@POK,@DescriptionSubcon,@ProcessTypeItem,@A ,@B, @C, @BS, @Total, @YarnNo, @FabricWidth)";
            var result = await context.ExecuteAsync(query, models);
        }
    }
}
