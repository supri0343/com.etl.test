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
    public class ReceiptItemAdapter : IReceiptItemAdapter
    {
        //private readonly IDbConnection _connection;
        private readonly ISqlDataContext<ChemicalReceiptItemModel> context;
        public ReceiptItemAdapter(IServiceProvider service)
        {
            context = service.GetService<ISqlDataContext<ChemicalReceiptItemModel>>();
        }

        //public async Task<List<ChemicalReceiptItemModel>> Get()
        //{
        //    _connection.Open();
        //    var query = $"SELECT * FROM [dbo].[DashboardChemicalReceiptItem]";
        //    var result = await _connection.QueryAsync<ChemicalReceiptItemModel>(query);
        //    _connection.Close();
        //    return result.ToList();
        //}

        public async Task Insert(ChemicalReceiptItemModel model)
        {
            //_connection.Open();
            var query = $"INSERT INTO [dbo].[DashboardChemicalReceiptItem] ([ID],[No],[Date],[UnitArea],[BonNo],[SupplierName],[PONo],[Code],[ItemName],[Total],[Unit],[Description]) VALUES (@ID,@No,@Date,@UnitArea,@BonNo,@SupplierName,@PONo,@Code,@ItemName,@Total,@Unit,@Description)";
            var result = await context.ExecuteAsync(query, model);
            //_connection.Close();
        }

        public async Task Update(ChemicalReceiptItemModel model)
        {
            //_connection.Open();
            var query = $"UPDATE [dbo].[DashboardChemicalReceiptItem] SET ([ID],[No],[Date],[UnitArea],[BonNo],[SupplierName],[PONo],[Code],[ItemName],[Total],[Unit],[Description]) VALUES (@ID,@No,@Date,@UnitArea,@BonNo,@SupplierName,@PONo,@Code,@ItemName,@Total,@Unit,@Description) WHERE [ID] = @Id";
            var result = await context.ExecuteAsync(query, model);
            //_connection.Close();
        }

        public async Task Delete(ChemicalReceiptItemModel model)
        {
            //_connection.Open();
            var query = $"DELETE FROM [dbo].[DashboardChemicalReceiptItem] WHERE [ID] = @Id";
            var result = await context.ExecuteAsync(query, model);
            //_connection.Close();

        }

        public async Task<IEnumerable<ChemicalReceiptItemModel>> GetByMonthAndYear(int month, int year)
        {
            //_connection.Open();
            var query = $"SELECT * FROM [dbo].[DashboardChemicalReceiptItem] WHERE MONTH(Date)={month} AND YEAR(Date)={year}";
            var result = await context.QueryAsync(query);
            return result.ToList();
            //_connection.Close();
        }

        public async Task DeleteByMonthAndYear(DateTime periode)
        {
            var query = $"DELETE FROM [dbo].[DashboardChemicalReceiptItem] WHERE MONTH(Date)=@month AND YEAR(Date)=@year";
            await context.QueryAsync(query, new { month = periode.Month, year = periode.Year });
        }

        public async Task InsertBulk(IEnumerable<ChemicalReceiptItemModel> models)
        {
            var query = $"INSERT INTO [dbo].[DashboardChemicalReceiptItem] ([ID],[No],[Date],[UnitArea],[BonNo],[SupplierName],[PONo],[Code],[ItemName],[Total],[Unit],[Description]) VALUES (@ID,@No,@Date,@UnitArea,@BonNo,@SupplierName,@PONo,@Code,@ItemName,@Total,@Unit,@Description)";
            var result = await context.ExecuteAsync(query, models);
        }
    }
}
