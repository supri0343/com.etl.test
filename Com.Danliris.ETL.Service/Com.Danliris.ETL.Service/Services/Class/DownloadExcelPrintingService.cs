using Com.Danliris.ETL.Service.DBAdapters.PrintingAdapters;
using Com.Danliris.ETL.Service.Services.Interfaces;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace Com.Danliris.ETL.Service.Services.Class
{
    public class DownloadExcelPrintingService : IDownloadExcelPrintingService
    {
        static string connectionStringPrinting = Environment.GetEnvironmentVariable("ConnectionStrings:SQLConnectionString", EnvironmentVariableTarget.Process);

        public DownloadExcelPrintingService(IServiceProvider provider)
        {

        }

        public Stream Download(DateTime periode)
        {
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add(new DataColumn() { ColumnName = "DOItemId", DataType = typeof(int) });
                table.Columns.Add(new DataColumn() { ColumnName = "PONo", DataType = typeof(string) });
                table.Columns.Add(new DataColumn() { ColumnName = "RONo", DataType = typeof(string) });
                table.Columns.Add(new DataColumn() { ColumnName = "Product Code", DataType = typeof(string) });
                table.Columns.Add(new DataColumn() { ColumnName = "Product Name", DataType = typeof(string) });
                table.Columns.Add(new DataColumn() { ColumnName = "Design Color", DataType = typeof(string) });
                table.Columns.Add(new DataColumn() { ColumnName = "Before Quantity", DataType = typeof(decimal) });
                table.Columns.Add(new DataColumn() { ColumnName = "Quantity", DataType = typeof(decimal) });

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                var excelPack = new ExcelPackage();
                
                var ws = excelPack.Workbook.Worksheets.Add("WriteTest");
                ws.Cells["A1"].Value = "Tanggal Stock Opname";
                ws.Cells["A5"].LoadFromDataTable(table, true);
                ws.Cells[ws.Dimension.Address].AutoFitColumns();

                Stream stream = new MemoryStream();
                excelPack.SaveAs(stream);

                return stream;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
