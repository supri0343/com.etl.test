using Com.Danliris.ETL.Service.Models.DashboardPretreatmentModels;
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

namespace Com.Danliris.ETL.Service.DBAdapters.PretreatmentAdapters
{
    public class MachineProductionAdapter : IMachineProductionAdapter
    {
        private readonly ISqlDataContext<PretreatmentMachineProductionModel> context;
        public MachineProductionAdapter(IServiceProvider service)
        {
            context =  service.GetService<ISqlDataContext<PretreatmentMachineProductionModel>>();
        }

        public async Task DeleteByMonthAndYear(DateTime periode)
        {
            var query = $"DELETE FROM [dbo].[DashboardPretreatmentMachineProduction] WHERE MONTH(Date)=@month AND YEAR(Date)=@year";
            await context.QueryAsync(query, new { month = periode.Month, year = periode.Year });
        }

        public async Task InsertBulk(IEnumerable<PretreatmentMachineProductionModel> models)
        {
            var query = $"INSERT INTO [dbo].[DashboardPretreatmentMachineProduction] ([ID],[No],[Date],[Shift],[Group] ,[Machine] ,[OrderNo],[Material],[ColorCW] ,[LengthIn],[LengthOutBQ] ,[LengthOutBS] ,[WidthFabric],[TrainNo],[ProcessType] ,[Speed],[Description]) VALUES(@Id,@No,@Date,@Shift,@Group,@Machine,@OrderNo,@Material,@ColorCW,@LengthIn,@LengthOutBQ,@LengthOutBS,@WidthFabric,@TrainNo,@ProcessType,@Speed,@Description)";
            var result = await context.ExecuteAsync(query, models);
        }
    }
}
