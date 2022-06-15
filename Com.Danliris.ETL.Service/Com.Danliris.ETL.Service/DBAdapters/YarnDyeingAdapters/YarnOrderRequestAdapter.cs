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
    public class YarnOrderRequestAdapter : IYarnOrderRequestAdapter
    {
        private readonly ISqlDataContext<DashboardYarnOrderRequest> context;
        public YarnOrderRequestAdapter(IServiceProvider serviceProvider)
        {
            context = serviceProvider.GetService<ISqlDataContext<DashboardYarnOrderRequest>>();
        }    
        
        public async Task Update(DashboardYarnOrderRequest yarnOrderRequest)
        {            
            var query = $"UPDATE [dbo].[DashboardYarnOrderRequest] SET [No] = @No, [Color] = @Color ,[Buyer] = @Buyer ,[NoLabDip] = @NoLabDip ,[Material] = @Material ,[Qty] = @Qty ,[Sales] = @Sales ,[OrderDate] = @OrderDate ,[DeliveryTarget] = @DeliveryTarget ,[DyeYarnRealization] = @DyeYarnRealization ,[Rewind] = @Rewind ,[FinishedWarehouse] = @FinishedWarehouse ,[Description] = @Description ,[Review] = @Review WHERE [SPNo]=@SPNo";
            await context.ExecuteAsync(query, yarnOrderRequest);
            
        }

        public async Task<IEnumerable<DashboardYarnOrderRequest>> GetBySPNo(List<string> listSPNos)
        {            
            var query = $"SELECT * FROM [dbo].[DashboardYarnOrderRequest] WHERE [SPNO] IN @SPNos";
            var result = await context.QueryAsync(query, new{SPNos = listSPNos});
            
            return result.ToList();
        }

        public async Task DeleteByMonthAndYear(DateTime periode)
        {
            var query = $"DELETE FROM [dbo].[DashboardYarnOrderRequest] WHERE YEAR(OrderDate)=@year";
            await context.QueryAsync(query, new { month = periode.Month, year = periode.Year });
        }

        public async Task InsertBulk(IEnumerable<DashboardYarnOrderRequest> models)
        {
            var query = $"INSERT INTO [dbo].[DashboardYarnOrderRequest]  ([No], [SPNo] ,[Color] ,[Buyer] ,[NoLabDip] ,[Material] ,[Qty] ,[Sales] ,[OrderDate] ,[DeliveryTarget] ,[DyeYarnRealization] ,[Rewind] ,[FinishedWarehouse] ,[Description] ,[Review]) VALUES (@No, @SPNo, @Color, @Buyer, @NoLabDip, @Material, @Qty, @Sales, @OrderDate, @DeliveryTarget, @DyeYarnRealization, @Rewind, @FinishedWarehouse, @Description, @Review)";
            await context.ExecuteAsync(query, models);
        }
    }
}