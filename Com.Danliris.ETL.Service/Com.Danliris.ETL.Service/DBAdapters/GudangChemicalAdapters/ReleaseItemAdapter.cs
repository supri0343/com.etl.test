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
    public class ReleaseItemAdapter : IReleaseItemAdapter
    {
        //private readonly IDbConnection _connection;
        private readonly ISqlDataContext<ChemicalReleaseItemModel> context;
        public ReleaseItemAdapter(IServiceProvider service)
        {
            context = service.GetService<ISqlDataContext<ChemicalReleaseItemModel>>();
           // _connection = new SqlConnection(connectionString);
        }

        //public async Task<List<ChemicalReleaseItemModel>> Get()
        //{
        //    //_connection.Open();
        //    var query = $"SELECT * FROM [dbo].[DashboardChemicalReleaseItem]";
        //    var result = await _connection.QueryAsync<ChemicalReleaseItemModel>(query);
        //    //_connection.Close();
        //    return result.ToList();
        //}

        public async Task Insert(ChemicalReleaseItemModel model)
        {
            //_connection.Open();
            var query = $"INSERT INTO [dbo].[DashboardChemicalReleaseItem] ([ID],[No],[Date],[BonNo],[ItemCode],[ItemName],[Total],[Unit],[MC],[TakenBy],[Area]) VALUES ( @Id , @No , @Date , @BonNo , @ItemCode , @ItemName , @Total , @Unit , @MC , @TakenBy , @Area)";
            var result = await context.ExecuteAsync(query, model);
            // _connection.Close();
        }

        public async Task Update(ChemicalReleaseItemModel model)
        {
            //_connection.Open();
            var query = $"UPDATE [dbo].[DashboardChemicalReleaseItem] SET ([ID],[No],[Date],[BonNo],[ItemCode],[ItemName],[Total],[Unit],[MC],[TakenBy],[Area]) VALUES ( @Id , @No , @Date , @BonNo , @ItemCode , @ItemName , @Total , @Unit , @MC , @TakenBy , @Area  ) WHERE [ID] = @Id";
            var result = await context.ExecuteAsync(query, model);
            // _connection.Close();
        }

        public async Task Delete(ChemicalReleaseItemModel model)
        {
           // _connection.Open();
            var query = $"DELETE FROM [dbo].[DashboardChemicalReleaseItem] WHERE [ID] = @Id";
            var result = await context.ExecuteAsync(query, model);
            //_connection.Close();

        }

        public async Task<IEnumerable<ChemicalReleaseItemModel>> GetByMonthAndYear(int month, int year)
        {
            //_connection.Open();
            var query = $"SELECT * FROM [dbo].[DashboardChemicalReleaseItem] WHERE MONTH(Date)={month} AND YEAR(Date)={year}";
            var result = await context.QueryAsync(query);
            return result.ToList();
            //_connection.Close();
        }

        public async Task DeleteByMonthAndYear(DateTime periode)
        {
            var query = $"DELETE FROM [dbo].[DashboardChemicalReleaseItem] WHERE MONTH(Date)=@month AND YEAR(Date)=@year";
            await context.QueryAsync(query, new { month = periode.Month, year = periode.Year });
        }

        public async Task InsertBulk(IEnumerable<ChemicalReleaseItemModel> models)
        {
            var query = $"INSERT INTO [dbo].[DashboardChemicalReleaseItem] ([ID],[No],[Date],[BonNo],[ItemCode],[ItemName],[Total],[Unit],[MC],[TakenBy],[Area]) VALUES ( @Id , @No , @Date , @BonNo , @ItemCode , @ItemName , @Total , @Unit , @MC , @TakenBy , @Area)";
            var result = await context.ExecuteAsync(query, models);
        }
    }
}
