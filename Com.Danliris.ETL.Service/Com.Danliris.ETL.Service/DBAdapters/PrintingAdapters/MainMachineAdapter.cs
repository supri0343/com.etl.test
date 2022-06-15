using Com.Danliris.ETL.Service.Models.DashboardMaintenanceModels;
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
    public class MainMachineAdapter : IMainMachineAdapter
    {
        //private readonly IDbConnection _connection;
        private readonly ISqlDataContext<DashBoardMainMachineModel> context;
        public MainMachineAdapter(IServiceProvider service)
        {
            context = service.GetService<ISqlDataContext<DashBoardMainMachineModel>>();
        }

        public async Task DeleteByMonthAndYear(DateTime periode)
        {
            var query = $"DELETE FROM [dbo].[DashboardMainMachine] WHERE MONTH(Date)=@month AND YEAR(Date)=@year";
            await context.QueryAsync(query, new { month = periode.Month, year = periode.Year });
        }

        public async Task InsertBulk(IEnumerable<DashBoardMainMachineModel> models)
        {
            var query = $"INSERT INTO [dbo].[DashboardMainMachine]([ID],[NO],[Date],[Machine],[Shift],[Group],[OrderNo],[Material],[ColorCW],[TrainNo],[ProcessType],[PatternCode],[DesignCode],[Screen],[Speed],[QtyInMeter],[QtyOutBQMeter],[QtyOutBSMeter],[WidthInch]) VALUES(@Id,@No,@Date,@Machine,@Shift,@Group,@OrderNo,@Material,@ColorCW,@TrainNo,@ProcessType,@PatternCode,@DesignCode,@Screen,@Speed,@QtyInMeter,@QtyOutBQMeter,@QtyOutBSMeter,@WidthInch)";
            var result = await context.ExecuteAsync(query, models);
        }
    }
}
