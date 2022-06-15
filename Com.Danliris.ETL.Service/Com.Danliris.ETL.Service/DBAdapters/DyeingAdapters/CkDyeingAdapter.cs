using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Com.Danliris.ETL.Service.ExcelModels.DashboardDyeingModels;
using System.Threading.Tasks;
using Com.Danliris.ETL.Service.Tools;
using Microsoft.Extensions.DependencyInjection;

namespace Com.Danliris.ETL.Service.DBAdapters.DyeingAdapters
{
    public class CkDyeingAdapter : ICkDyeingAdapter
    {
        private readonly ISqlDataContext<DashboardCKDyeing> context;
        public CkDyeingAdapter(IServiceProvider serviceProvider)
        {
            context = serviceProvider.GetService<ISqlDataContext<DashboardCKDyeing>>();
        }

        // public async Task Insert(DashboardCKDyeing ckDyeing)
        // {
        //     //_connection.Open();
        //     var query = $"INSERT INTO [dbo].[DashboardCKDyeing] ([ID] ,[No] ,[Date] ,[Machine] ,[OrderNo] ,[Construction] ,[Color] ,[Process] ,[Length] ,[SolutionVolume] ,[ChemicalDyesDetail] ,[LAB] ,[Prod]) VALUES (@Id, @No, @Date, @Machine, @OrderNo, @Construction, @Color, @Process, @Length, @SolutionVolume, @ChemicalDyesDetail, @LAB, @Prod)";
        //     var result = await context.ExecuteAsync(query, ckDyeing);
        //     //var result = await _connection.ExecuteAsync(query, ckDyeing);
        //     //_connection.Close();
        // }
        
        // public async Task Update(DashboardCKDyeing ckDyeing)
        // {
        //     //_connection.Open();
        //     var query = $"UPDATE [dbo].[DashboardCKDyeing] SET [ID]=@Id ,[No]=@No ,[Date]=@Date ,[Machine]=@Machine ,[OrderNo]=@OrderNo ,[Construction]=@construction ,[Color]=@Color ,[Process]=@Process ,[Length]=@Length ,[SolutionVolume]=@SolutionVolume ,[ChemicalDyesDetail]=@ChemicalDyesDetail ,[LAB]=@LAB ,[Prod]=@Prod WHERE [ID]=@Id";
        //     var result = await context.ExecuteAsync(query, ckDyeing);
        //     //var result = await _connection.ExecuteAsync(query, ckDyeing);
        //     //_connection.Close();
        // }

        // public async Task Delete(DashboardCKDyeing ckDyeing)
        // {
        //     //_connection.Open();
        //     var query = $"DELETE FROM [dbo].[DashboardCKDyeing] WHERE [ID]=@Id";
        //     var result = await context.ExecuteAsync(query, ckDyeing);
        //     //var result = await _connection.ExecuteAsync(query, ckDyeing);
        //     //_connection.Close();
        // }

        // public async Task<IEnumerable<DashboardCKDyeing>> GetByMonthAndYear(int month, int year)
        // {
        //     //_connection.Open();
        //     var query = $"SELECT * FROM [dbo].[DashboardCKDyeing] WHERE MONTH(Date)={month} AND YEAR(Date)={year}";
        //     var result =  await context.QueryAsync(query);
        //     return result.ToList();
        //     //_connection.Close();
        //     //return result.ToList();
        // }

        public async Task DeleteByMonthAndYear(DateTime periode)
        {
            var query = $"DELETE FROM [dbo].[DashboardCKDyeing] WHERE MONTH(Date)=@month AND YEAR(Date)=@year";
            await context.QueryAsync(query, new { month = periode.Month, year = periode.Year });
        }

        public async Task InsertBulk(IEnumerable<DashboardCKDyeing> models)
        {
            var query = $"INSERT INTO [dbo].[DashboardCKDyeing] ([ID] ,[No] ,[Date] ,[Machine] ,[OrderNo] ,[Construction] ,[Color] ,[Process] ,[Length] ,[SolutionVolume] ,[ChemicalDyesDetail] ,[LAB] ,[Prod]) VALUES (@Id, @No, @Date, @Machine, @OrderNo, @Construction, @Color, @Process, @Length, @SolutionVolume, @ChemicalDyesDetail, @LAB, @Prod)";
            await context.ExecuteAsync(query, models);
        }
    }
}