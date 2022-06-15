using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Com.Danliris.ETL.Service.ExcelModels.DashboardYarnDyeing;
using Com.Danliris.ETL.Service.Tools;
using Dapper;
using Microsoft.Extensions.DependencyInjection;

namespace Com.Danliris.ETL.Service.DBAdapters.YarnDyeingAdapters
{
    public class DyeYarnRequestAdapter : IDyeYarnRequestAdapter
    {        
        private readonly ISqlDataContext<DashboardDyeYarnRequest> context;
        public DyeYarnRequestAdapter(IServiceProvider serviceProvider)
        {
            context = serviceProvider.GetService<ISqlDataContext<DashboardDyeYarnRequest>>();
        }
        
        public async Task Update(DashboardDyeYarnRequest dyeYarnRequest)
        {
            var query = $"UPDATE [dbo].[DashboardDyeYarnRequest] SET [No] = @No, [Color] = @Color, [ReferenceNo] = @ReferenceNo, [Material] = @Material, [Sales] = @Sales, [Buyer] = @Buyer, [OrderDate] = @OrderDate, [DeliveryTarget] = @DeliveryTarget, [LabRealization] = @LabRealization, [YarnColorCode] = @YarnColorCode, [DeliveryRealDate] = @DeliveryRealDate, [Description] = @Description, [EvaluationDownOrder] = @EvaluationDownOrder, [Review] = @Review WHERE [SPNo]=@SPNo";
            var result = await context.ExecuteAsync(query, dyeYarnRequest);
        } 

        public async Task <IEnumerable<DashboardDyeYarnRequest>> GetBySPNo(List<string> listSPNos)
        {
            var query = $"SELECT * FROM [dbo].[DashboardDyeYarnRequest] WHERE [SPNo] IN @SPNos";
            var result = await context.QueryAsync(query, new{SPNos = listSPNos});
            return result.ToList();
        }

        public async Task DeleteByMonthAndYear(DateTime periode)
        {
            var query = $"DELETE FROM [dbo].[DashboardDyeYarnRequest] WHERE YEAR(OrderDate)=@year";
            await context.QueryAsync(query, new { month = periode.Month, year = periode.Year });
        }

        public async Task InsertBulk(IEnumerable<DashboardDyeYarnRequest> models)
        {
            var query = $"INSERT INTO [dbo].[DashboardDyeYarnRequest] ( [No] ,[SPNo] ,[Color] ,[ReferenceNo] ,[Material] ,[Sales] ,[Buyer] ,[OrderDate] ,[DeliveryTarget] ,[LabRealization] ,[YarnColorCode] ,[DeliveryRealDate] ,[Description] ,[EvaluationDownOrder] ,[Review]) VALUES (@No, @SPNo, @Color, @ReferenceNo, @Material, @Sales, @Buyer, @OrderDate, @DeliveryTarget, @LabRealization, @YarnColorCode, @DeliveryRealDate, @Description, @EvaluationDownOrder, @Review)";
            var result = await context.ExecuteAsync(query, models);
        }
    }
}