using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Com.Danliris.ETL.Service.Models;
using Com.Danliris.ETL.Service.ViewModels;
using Newtonsoft.Json;

namespace Com.Danliris.ETL.Service.DBAdapters
{
    public class FilePeriodeAdapters
    {
        private readonly IDbConnection _connection;
        public FilePeriodeAdapters(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
        }
        
        public async Task<FilePeriodePaginationViewModel> Get(int page, int size, string order, string keyword)
        {
            _connection.Open();
            var orderObject = JsonConvert.DeserializeObject<FilePerioderOrderViewModel>(order);
            var pageSelected = (page - 1) * size;

            var sql = (@" SELECT * FROM [dbo].[tableFilePeriode] 
                        INNER JOIN [dbo].[tabelArea] ON [tabelArea].[id] = [tableFilePeriode].[AreaId]");

            if (!keyword.Equals("")) {
                sql += " WHERE [tabelArea].[name] LIKE '%" + keyword +"%' ";
            }

            if (orderObject.month != null) {
                sql += "  ORDER BY [tableFilePeriode].[month] " + orderObject.month;
            }

            if (orderObject.year != null) {
                sql += "  ORDER BY [tableFilePeriode].[year] " + orderObject.year;
            }

            if (orderObject.name != null) {
                sql += "  ORDER BY [tabelArea].[name] " + orderObject.name;
            }

            if (orderObject.updatedAt != null) {
                sql += "  ORDER BY [tableFilePeriode].[updatedAt] " + orderObject.updatedAt;
            }

            if (orderObject.month == null && orderObject.year == null && orderObject.name == null && orderObject.updatedAt == null) {
                sql += "  ORDER BY [tableFilePeriode].[updatedAt] desc ";
            }

            sql += " OFFSET @Page ROWS FETCH NEXT @Size ROWS ONLY";

            var sqlTotal = ("SELECT COUNT(*) FROM [dbo].[tableFilePeriode] INNER JOIN [dbo].[tabelArea] ON [tabelArea].[id] = [tableFilePeriode].[AreaId]");
            var total = await _connection.QueryAsync<int>(sqlTotal);
            var result = await _connection.QueryAsync<FilePeriodeListViewModel>(sql, new { Page = pageSelected, Size = size });
            return new FilePeriodePaginationViewModel(result.ToList(), new { Count = result.Count(), Page = page, Size = size, Total = total.FirstOrDefault()});
        }

        public async Task<bool> Insert(FilePeriode filePeriode) {
            try {
                _connection.Open();

                var sql = (@"
                    begin transaction
                    if exists (select * from [dbo].[tableFilePeriode] where [month]=@month AND [year]=@year AND [AreaId]=@AreaId)
                    begin
                        update [dbo].[tableFilePeriode] set [month]=@month, [year]=@year,
                        [updatedAt]=@updatedAt, [AreaId]=@AreaId
                        where [month]=@month AND [year]=@year AND [AreaId]=@AreaId
                    end
                    else
                    begin
                        insert into [dbo].[tableFilePeriode]
                        ([month],[year],[createdAt],[updatedAt],[AreaId])
                        values (@month, @year, @createdAt, @updatedAt, @AreaId)
                    end
                    commit transaction
                ");

                var result = await _connection.QueryAsync<FilePeriode>(sql, filePeriode);
                _connection.Close();
                return true;
            } catch (SqlException ex) {
                throw ex;
            } 
        }

    }
}