using Com.Danliris.ETL.Service.Models.DashboardGudangChemicalModels;
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

namespace Com.Danliris.ETL.Service.DBAdapters.GudangChemicalAdapters
{
    public class ChemicalStockAdapter : IChemicalStockAdapter
    {
        private readonly ISqlDataContext<ChemicalStockModel> context;

        //private readonly IDbConnection _connection;
        public ChemicalStockAdapter(IServiceProvider serviceProvider)
        {
            context = serviceProvider.GetService<ISqlDataContext<ChemicalStockModel>>();
        }
        public async Task DeleteByMonthAndYear(DateTime periode)
        {
            var query = $"DELETE FROM [dbo].[DashboardChemicalStock] WHERE MONTH(Date)=@month AND YEAR(Date)=@year";
            await context.QueryAsync(query, new { month = periode.Month, year = periode.Year });
        }

        public async Task InsertBulk(IEnumerable<ChemicalStockModel> models)
        {
            var query = $"INSERT INTO [dbo].[DashboardChemicalStock] ([ID],[No],[Date],[ItemCode],[RackNo],[MasterKd],[ItemName],[Unit],[EarlyS],[In],[Out],[FinalS]) VALUES (@Id ,@No ,@Date ,@ItemCode ,@RackNo ,@MasterKd ,@ItemName ,@Unit ,@EarlyS ,@In ,@Out ,@FinalS )";
            var result = await context.ExecuteAsync(query, models);
        }
    }
}
