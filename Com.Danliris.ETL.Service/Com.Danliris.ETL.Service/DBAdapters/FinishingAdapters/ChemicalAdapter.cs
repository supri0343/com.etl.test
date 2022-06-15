using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Com.Danliris.ETL.Service.ExcelModels;
using Com.Danliris.ETL.Service.Tools;
using Dapper;
using Microsoft.Extensions.DependencyInjection;

namespace Com.Danliris.ETL.Service.DBAdapters.FinishingAdapters
{
    public class ChemicalAdapter : IChemicalAdapter
    {
        private readonly ISqlDataContext<DashboardChemicalF> _context;

        public ChemicalAdapter(IServiceProvider serviceProvider)
        {
            _context = serviceProvider.GetService<ISqlDataContext<DashboardChemicalF>>();
        }

        // public async Task<int> Insert(DashboardChemicalF model)
        // {
        //     var query = $"INSERT INTO [dbo].[DashboardChemicalF] ([ID] ,[No] ,[Date] ,[Machine] ,[OrderType] ,[OrderNo] ,[Construction], [Color] ,[ProcessType] ,[Length] ,[SolutionVolTotal] ,[ChemicalTypeUsed] ,[ChemicalWeightUsed]) " +
        //         $"VALUES (@Id, @No, @Date, @Machine, @OrderType, @OrderNo, @Construction, @Color, @ProcessType, @Length, @SolutionVolTotal, @ChemicalTypeUsed, @ChemicalWeightUsed)";
        //     return await _connection.ExecuteAsync(query, model);
        // }

        // public async Task<int> Update(DashboardChemicalF model)
        // {
        //     var query = $"UPDATE [dbo].[DashboardChemicalF] SET [ID]=@Id ,[No]=@No ,[Date]=@Date ,[Machine]=@Machine ,[OrderType]=@OrderType ,[OrderNo]=@OrderNo ,[Construction]=@Construction ,[Color]=@Color ,[ProcessType]=@ProcessType ,[Length]=@Length ,[SolutionVolTotal]=@SolutionVolTotal ,[ChemicalTypeUsed]=@ChemicalTypeUsed ,[ChemicalWeightUsed]=@ChemicalWeightUsed  WHERE [ID]=@Id";
        //     return await _connection.ExecuteAsync(query, model);
        // }

        // public async Task<int> Delete(DashboardChemicalF model)
        // {
        //     var query = $"DELETE FROM [dbo].[DashboardChemicalF] WHERE [ID]=@Id";
        //     return await _connection.ExecuteAsync(query, model);
        // }

        // public async Task<IEnumerable<DashboardChemicalF>> GetByMonthAndYear(int month, int year)
        // {
        //     var result = await _connection.QueryAsync<DashboardChemicalF>($"SELECT * FROM [dbo].[DashboardChemicalF] WHERE MONTH(Date)={month} AND YEAR(Date)={year}");
        //     return result.ToList();
        // }

        public async Task DeleteByMonthAndYear(DateTime periode)
        {
            var query = $"DELETE FROM [dbo].[DashboardChemicalF] WHERE MONTH(Date)=@month AND YEAR(Date)=@year";
            await _context.QueryAsync(query, new { month = periode.Month, year = periode.Year });
        }

        public async Task InsertBulk(IEnumerable<DashboardChemicalF> models)
        {
            var query = $"INSERT INTO [dbo].[DashboardChemicalF] ([ID] ,[No] ,[Date] ,[Machine] ,[OrderType] ,[OrderNo] ,[Construction], [Color] ,[ProcessType] ,[Length] ,[SolutionVolTotal] ,[ChemicalTypeUsed] ,[ChemicalWeightUsed]) " +
                $"VALUES (@Id, @No, @Date, @Machine, @OrderType, @OrderNo, @Construction, @Color, @ProcessType, @Length, @SolutionVolTotal, @ChemicalTypeUsed, @ChemicalWeightUsed)";
            await _context.ExecuteAsync(query, models);
        }
    }
}
