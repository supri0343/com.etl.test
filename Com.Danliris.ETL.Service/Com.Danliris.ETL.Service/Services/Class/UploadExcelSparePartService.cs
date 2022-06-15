using Com.Danliris.ETL.Service.DBAdapters.GudangSparepartAdapters;
using Com.Danliris.ETL.Service.Models.DashboardGudangSparepartModels;
using Com.Danliris.ETL.Service.Services.Interfaces;
using Com.Danliris.ETL.Service.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Com.Danliris.ETL.Service.Services.Class
{
    public class UploadExcelSparePartService : IUploadExcelSparepartService
    {
        static string connectionString = Environment.GetEnvironmentVariable("ConnectionStrings:SQLConnectionString", EnvironmentVariableTarget.Process);
        IReleaseItemSparepartAdapter releaseItemSparepartAdapter;
        IReceiptItemSparepartAdapter receiptItemSparepartAdapter;
        IStockSparepartAdapter stockSparepartAdapter;
        ConverterChecker converter = new ConverterChecker();
        GeneralHelper general = new GeneralHelper();

        public UploadExcelSparePartService(IServiceProvider service)
        {
            releaseItemSparepartAdapter = service.GetService<IReleaseItemSparepartAdapter>();
            receiptItemSparepartAdapter = service.GetService<IReceiptItemSparepartAdapter>();
            stockSparepartAdapter = service.GetService<IStockSparepartAdapter>();
        }
        public async Task Upload(ExcelWorksheets excel, DateTime periode)
        {
            try
            {

                var data = 0;
                foreach (var fileName in excel)
                {
                    switch (fileName.Name.ToLower().Trim())
                    {
                        case "stock_spare_part":
                            await UploadStockSparePart(excel, periode, data);

                            break;

                        case "pengeluaran_barang":
                            await UploadSparepartReleaseItem(excel, periode, data);
                            break;

                        case "penerimaan_barang":
                            await UploadSparepartReceiptItem(excel, periode, data);
                            break;

                        default:
                            throw new Exception(fileName.Name + " tidak valid");
                    }
                    data++;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #region Stock_Spare_Part
        public async Task UploadStockSparePart(ExcelWorksheets excel, DateTime periode, int data)
        {
            var sheet = excel[data];
            var startRow = 3;
            var startCol = 1;

            var totalRows = sheet.Dimension.Rows;
            var totalCols = sheet.Dimension.Columns;
            int rowIndex = 0;
            var listData = new List<StockSparepartModel>();
            try
            {
                for (rowIndex = startRow; rowIndex <= totalRows; rowIndex++)
                {
                    if (sheet.Cells[rowIndex, startCol].Value != null)
                    {
                        general.ValidatePeriode(sheet.Cells[rowIndex, 2], periode);
                        listData.Add(new StockSparepartModel(
                           general.GenerateId(sheet.Cells[rowIndex, startCol + 1], sheet.Cells[rowIndex, 1]),
                           Convert.ToInt32(sheet.Cells[rowIndex, startCol].Value),
                           general.ConvertDateTime(sheet.Cells[rowIndex, startCol + 1]),
                           converter.GenerateValueString(sheet.Cells[rowIndex, startCol + 2]),
                           converter.GenerateValueString(sheet.Cells[rowIndex, startCol + 3]),
                           converter.GenerateValueString(sheet.Cells[rowIndex, startCol + 4]),
                           converter.GenerateValueString(sheet.Cells[rowIndex, startCol + 5]),
                           converter.GenerateValueString(sheet.Cells[rowIndex, startCol + 6]),
                           converter.GenerateValueDouble(sheet.Cells[rowIndex, startCol + 7]),
                           converter.GenerateValueDouble(sheet.Cells[rowIndex, startCol + 8]),
                           converter.GenerateValueDouble(sheet.Cells[rowIndex, startCol + 9]),
                           converter.GenerateValueDouble(sheet.Cells[rowIndex, startCol + 10])
                        ));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Gagal memproses Sheet STOCK_SPARE_PART pada baris ke-{rowIndex} - {ex.Message}");
            }

            try
            {
                if (listData.Count() > 0)
                {
                    await stockSparepartAdapter.DeleteByMonthAndYear(periode);
                    await stockSparepartAdapter.InsertBulk(listData);
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Gagal menyimpan sheet STOCK_SPARE_PART - " + ex.Message);
            }
        }
        #endregion

        #region Pengeluaran Barang
        public async Task UploadSparepartReleaseItem(ExcelWorksheets excel, DateTime periode, int data)
        {
            var sheet = excel[data];
            var startRow = 3;
            var startCol = 1;

            var totalRows = sheet.Dimension.Rows;
            var totalCols = sheet.Dimension.Columns;

            var listData = new List<SparepartItemRelease>();
            int rowIndex = 0;
            try
            {
                for (rowIndex = startRow; rowIndex <= totalRows; rowIndex++)
                {

                    if (sheet.Cells[rowIndex, startCol].Value != null)
                    {
                        general.ValidatePeriode(sheet.Cells[rowIndex, 2], periode);
                        listData.Add(new SparepartItemRelease(
                           general.GenerateId(sheet.Cells[rowIndex, startCol + 1], sheet.Cells[rowIndex, 1]), //ID
                           Convert.ToInt32(sheet.Cells[rowIndex, startCol].Value), //No
                           general.ConvertDateTime(sheet.Cells[rowIndex, startCol + 1]), //Date
                           converter.GenerateValueString(sheet.Cells[rowIndex, startCol + 2]), //BonNo
                           converter.GenerateValueString(sheet.Cells[rowIndex, startCol + 3]), //ItemCode
                           converter.GenerateValueString(sheet.Cells[rowIndex, startCol + 4]), //ItemName
                           converter.GenerateValueDouble(sheet.Cells[rowIndex, startCol + 5]), //Total
                           converter.GenerateValueString(sheet.Cells[rowIndex, startCol + 6]), //Unit
                           converter.GenerateValueString(sheet.Cells[rowIndex, startCol + 7]), //MC
                           converter.GenerateValueString(sheet.Cells[rowIndex, startCol + 8]), //TakenBy
                           converter.GenerateValueString(sheet.Cells[rowIndex, startCol + 9]) //Area                           
                        ));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Gagal memproses Sheet PENGELUARAN_BARANG pada baris ke-{rowIndex} - { ex.Message }");
            }

            try
            {
                if (listData.Count() > 0)
                {
                    await releaseItemSparepartAdapter.DeleteByMonthAndYear(periode);
                    await releaseItemSparepartAdapter.InsertBulk(listData);
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Gagal menyimpan sheet PENGELUARANG_BARANG " + ex.Message);
            }
        }
        #endregion

        #region Penerimaan Barang
        public async Task UploadSparepartReceiptItem(ExcelWorksheets sheet, DateTime periode, int data)
        {
            var sheet1 = sheet[data];
            var startRow = 3;
            var startCol = 1;

            var totalRows = sheet1.Dimension.Rows;
            var totalCols = sheet1.Dimension.Columns;

            var listData = new List<SparepartItemReceipt>();
            int rowIndex = 0;
            try
            {

                for (rowIndex = startRow; rowIndex <= totalRows; rowIndex++)
                {

                    if (sheet1.Cells[rowIndex, startCol].Value != null)
                    {
                        general.ValidatePeriode(sheet1.Cells[rowIndex, 2], periode);
                        listData.Add(new SparepartItemReceipt(
                           general.GenerateId(sheet1.Cells[rowIndex, startCol + 1], sheet1.Cells[rowIndex, 1]), //ID
                           Convert.ToInt32(sheet1.Cells[rowIndex, startCol].Value), //No
                           general.ConvertDateTime(sheet1.Cells[rowIndex, startCol + 1]), //Date
                           converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 2]), //UnitArea
                           converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 3]), //BonNo
                           converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 4]), //SupplierName
                           converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 5]), //PONo
                           converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 6]), //Code
                           converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 7]), //ItemName
                           converter.GenerateValueDouble(sheet1.Cells[rowIndex, startCol + 8]), //Total
                           converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 9]), //Unit                           
                           converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 10]) // Description
                        ));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Gagal memproses Sheet PENERIMAAN_BARANG pada baris ke-{rowIndex} - {ex.Message}");
            }

            try
            {
                if (listData.Count() > 0)
                {
                    await receiptItemSparepartAdapter.DeleteByMonthAndYear(periode);
                    await receiptItemSparepartAdapter.InsertBulk(listData);
                }
            }
            catch (Exception Ex)
            {

                throw new Exception($"Gagal menyimpan sheet PENERIMAAN_BARANG " + Ex.Message);
            }
        }
        #endregion
    }
}
