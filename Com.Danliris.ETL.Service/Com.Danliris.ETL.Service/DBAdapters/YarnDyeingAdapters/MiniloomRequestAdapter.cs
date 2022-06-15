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
    public class MiniloomRequestAdapter : IMiniloomRequestAdapter
    {
        private readonly ISqlDataContext<DashboardMiniloomRequest> context;
        public MiniloomRequestAdapter(IServiceProvider serviceProvider)
        {
            context = serviceProvider.GetService<ISqlDataContext<DashboardMiniloomRequest>>();
        }        
        
        public async Task Update(DashboardMiniloomRequest miniloomRequest)
        {
            var query = $"UPDATE [dbo].[DashboardMiniloomRequest] SET [No] = @No, [NameDesign] = @NameDesign, [Buyer] = @Buyer, [Construction] = @Construction, [Material] = @Material, [TotalColor] = @TotalColor, [Sales] = @Sales, [OrderDate] = @OrderDate, [DeliveryTarget] = @DeliveryTarget, [LabRealization] = @LabRealization, [DeliveryWeaving] = @DeliveryWeaving, [FinishMiniloomLaboratoryDate] = @FinishMiniloomLaboratoryDate, [DeliverySales] = @DeliverySales, [RefMiniloomNumber] = @RefMiniloomNumber, [QtyReaLinKG] = @QtyReaLinKG, [Description] = @Description, [EvaluationDownOrder] = @EvaluationDownOrder, [Review] = @Review WHERE [SPNo]=@SPNo";
            await context.ExecuteAsync(query, miniloomRequest);            
        }

        public async Task<IEnumerable<DashboardMiniloomRequest>> GetBySPNo(List<string> listSPNos)
        {
            var query = $"SELECT * FROM [dbo].[DashboardMiniloomRequest] WHERE [SPNO] IN @SPNos";
            var result = await context.QueryAsync(query, new{SPNos = listSPNos});            
            return result.ToList();
        }

        public async Task DeleteByMonthAndYear(DateTime periode)
        {
            var query = $"DELETE FROM [dbo].[DashboardMiniloomRequest] WHERE YEAR(OrderDate)=@year";
            await context.QueryAsync(query, new { month = periode.Month, year = periode.Year });
        }

        public async Task InsertBulk(IEnumerable<DashboardMiniloomRequest> models)
        {
            var query = $"INSERT INTO [dbo].[DashboardMiniloomRequest]  ([No] ,[SPNo] ,[NameDesign] ,[Buyer] ,[Construction] ,[Material] ,[TotalColor] ,[Sales] ,[OrderDate] ,[DeliveryTarget] ,[LabRealization] ,[DeliveryWeaving] ,[FinishMiniloomLaboratoryDate] ,[DeliverySales] ,[RefMiniloomNumber] ,[QtyReaLinKG] ,[Description] ,[EvaluationDownOrder] ,[Review]) VALUES (@No, @SPNo, @NameDesign, @Buyer, @Construction, @Material, @TotalColor, @Sales, @OrderDate, @DeliveryTarget, @LabRealization, @DeliveryWeaving, @FinishMiniloomLaboratoryDate, @DeliverySales, @RefMiniloomNumber, @QtyRealinKg, @Description, @EvaluationDownOrder, @Review)";
            await context.ExecuteAsync(query, models); 
        }
    }
}