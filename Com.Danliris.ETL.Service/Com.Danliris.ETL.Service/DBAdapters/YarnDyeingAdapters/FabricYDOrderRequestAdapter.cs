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
    public class FabricYDOrderRequestAdapter : IFabricYDOrderRequestAdapter 
    {
        private readonly ISqlDataContext<DashboardFabricYDOrderRequest> context;
        public FabricYDOrderRequestAdapter(IServiceProvider serviceProvider)
        {
            context = serviceProvider.GetService<ISqlDataContext<DashboardFabricYDOrderRequest>>();
        }  
        
        public async Task Update(DashboardFabricYDOrderRequest yDOrderRequest)
        {
            var query = $"UPDATE [dbo].[DashboardFabricYDOrderRequest] SET [No] = @No, [DesignName] = @DesignName, [Buyer] = @Buyer, [SampleReferenceNumber] = @SampleReferenceNumber, [Color] = @Color, [Construction] = @Construction, [Material] = @Material, [QtyInMeter] = @QtyInMeter, [Sales] = @Sales, [OrderDate] = @OrderDate, [DateDel] = @DateDel, [DeliveryRealization] = @DeliveryRealization, [BookingYarn] = @BookingYarn, [QtyinKg] = @QtyinKg, [QtySendWeavingTotal] = @QtySendWeavingTotal, [QtyDeliverytoWeavinginKG] = @QtyDeliverytoWeavinginKG, [Pretreatment] = @Pretreatment, [Washing] = @Washing, [Finishing] = @Finishing, [QCin] = @QCin, [SendWarehouse] = @SendWarehouse, [QtyDeliverytoFinishedWarehouse] = @QtyDeliverytoFinishedWarehouse, [Description] = @Description, [OrderStatus] = @OrderStatus, [Review] = @Review WHERE [SPNo]=@SPNo";
            await context.ExecuteAsync(query, yDOrderRequest);
        }

        public async Task<IEnumerable<DashboardFabricYDOrderRequest>> GetBySPNo(List<string> listSPNos)
        {
            var query = $"SELECT * FROM [dbo].[DashboardFabricYDOrderRequest] WHERE [SPNO] IN @SPNos";
            var result = await context.QueryAsync(query, new{SPNos = listSPNos});
            return result.ToList();
        }

        public async Task DeleteByMonthAndYear(DateTime periode)
        {
            var query = $"DELETE FROM [dbo].[DashboardFabricYDOrderRequest] WHERE YEAR(OrderDate)=@year";
            await context.QueryAsync(query, new { month = periode.Month, year = periode.Year });
        }

        public async Task InsertBulk(IEnumerable<DashboardFabricYDOrderRequest> models)
        {
            var query = $"INSERT INTO [dbo].[DashboardFabricYDOrderRequest]  ([No] ,[SPNo] ,[DesignName] ,[Buyer] ,[SampleReferenceNumber] ,[Color] ,[Construction] ,[Material] ,[QtyInMeter] ,[Sales] ,[OrderDate] ,[DateDel] ,[DeliveryRealization] ,[BookingYarn] ,[QtyinKg] ,[QtySendWeavingTotal] ,[QtyDeliverytoWeavinginKG] ,[Pretreatment] ,[Washing] ,[Finishing] ,[QCin] ,[SendWarehouse] ,[QtyDeliverytoFinishedWarehouse] ,[Description] ,[OrderStatus] ,[Review]) VALUES (@No, @SPNo, @DesignName, @Buyer, @SampleReferenceNumber, @Color, @Construction, @Material, @QtyInMeter, @Sales, @OrderDate, @DateDel, @DeliveryRealization, @BookingYarn, @QtyInKg, @QtySendWeavingTotal, @QtyDeliverytoWeavinginKG, @Pretreatment, @Washing, @Finishing, @QCin, @SendWarehouse, @QtyDeliverytoFinishedWarehouse, @Description, @OrderStatus, @Review)";
            await context.ExecuteAsync(query, models);
        }
    }
}