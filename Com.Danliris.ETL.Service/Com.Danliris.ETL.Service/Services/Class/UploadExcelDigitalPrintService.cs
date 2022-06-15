using OfficeOpenXml;
using Microsoft.Extensions.Logging;
using Com.Danliris.ETL.Service.ExcelModels;
using System.Collections.Generic;
using System;
using Com.Danliris.ETL.Service.Tools;
using Com.Danliris.ETL.Service.DBAdapters;
using System.Linq;
using System.Threading.Tasks;
using Com.Danliris.ETL.Service.Services.Interfaces;
using Com.Danliris.ETL.Service.DBAdapters.DigitalPrintingAdapters;
using Microsoft.Extensions.DependencyInjection;

namespace Com.Danliris.ETL.Service.Services.Class
{
    public class UploadExcelDigitalPrintService : IUploadExcelDigitalPrintingService
    {
        
        static string connectionString = Environment.GetEnvironmentVariable("ConnectionStrings:SQLConnectionString", EnvironmentVariableTarget.Process);
        IProductionDigitalTransferAdapter productionDigitalTransferAdapters;
        IOrderInAdapter orderInAdapters;
        ISOOrderDigitalTransferAdapter sOOrderDigitalTransferAdapters;
        IWipAreaAdapter wipAreaAdapters;
        IDailyDeliveryAdapter dailyDeliveryAdapters;
        ConverterChecker converter = new ConverterChecker();
        GeneralHelper generalHelper = new GeneralHelper();
        DateTime Periode = new DateTime();
        public UploadExcelDigitalPrintService(IServiceProvider service)
        {
            productionDigitalTransferAdapters = service.GetService<IProductionDigitalTransferAdapter>();
            orderInAdapters = service.GetService<IOrderInAdapter>();
            sOOrderDigitalTransferAdapters = service.GetService<ISOOrderDigitalTransferAdapter>();
            wipAreaAdapters = service.GetService<IWipAreaAdapter>();
            dailyDeliveryAdapters = service.GetService<IDailyDeliveryAdapter>();
        }


        public async Task Upload(ExcelWorksheets sheets, DateTime periode)
        {
            try
            {
                this.Periode = periode;
                var data = 0;
                foreach (var item in sheets)
                {                    
                    switch (item.Name.Trim().ToLower())
                    {
                        case "produksi_digital_transfer":
                            await UploadSheetProductionDigitalTransfer(sheets[item.Name], periode);
                            break;
                        case "order_masuk":
                            await UploadSheetOrderIn(sheets[item.Name], periode);
                            break;
                        case "order_so_digital_transfer":
                            await UploadSheetSOOrderDigitalTransfer(sheets[item.Name], periode);
                            break;
                        case "wip_area":
                            await UploadSheetWipArea(sheets[item.Name], periode);
                            break;
                        case "pengiriman_harian":
                            await UploadSheetDailyDelivery(sheets[item.Name], periode);
                            break;
                        default:
                            throw new Exception("'" + item.Name + "' tidak valid");
                    }
                    data++;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task UploadSheetProductionDigitalTransfer(ExcelWorksheet sheet, DateTime periode)
        {
            var startRow = 3;
            var startCol = 1;
            var totalRows = sheet.Dimension.Rows;
            var totalColumns = sheet.Dimension.Columns;
            int rowIndex = 0;
            var listData = new List<ProductionDigitalTransfer>();
            try
            {

                for (rowIndex = startRow; rowIndex <= totalRows; rowIndex++)
                {
                    if (sheet.Cells[rowIndex, startCol].Value != null)
                    {
                        generalHelper.ValidatePeriode(sheet.Cells[rowIndex,2], Periode);
                        var cell = sheet.Cells;
                        listData.Add(new ProductionDigitalTransfer(
                            generalHelper.GenerateId(sheet.Cells[rowIndex,2],sheet.Cells[rowIndex,1]),
                            Convert.ToInt32(sheet.Cells[rowIndex, startCol].Value.ToString()),
                            generalHelper.ConvertDateTime(sheet.Cells[rowIndex,startCol + 1]),
                            Convert.ToChar(sheet.Cells[rowIndex, startCol + 2].Value.ToString()),
                            converter.GenerateValueString(sheet.Cells[rowIndex, startCol + 3]),
                            converter.GenerateValueString(sheet.Cells[rowIndex, startCol + 4]),
                            converter.GenerateValueString(sheet.Cells[rowIndex, startCol + 5]),
                            converter.GenerateValueString(sheet.Cells[rowIndex, startCol + 6]),
                            converter.GenerateValueString(sheet.Cells[rowIndex, startCol + 7]),
                            converter.GenerateValueString(sheet.Cells[rowIndex, startCol + 8]),
                            converter.GeneratePureDouble(sheet.Cells[rowIndex, startCol + 9]),
                            converter.GeneratePureTime(sheet.Cells[rowIndex, startCol + 10]),
                            converter.GeneratePureTime(sheet.Cells[rowIndex, startCol + 11]),
                            Math.Round(Convert.ToDecimal(sheet.Cells[rowIndex, startCol + 12].Text), 2),
                            converter.GenerateValueInt(sheet.Cells[rowIndex, startCol + 13]),
                            converter.GenerateValueInt(sheet.Cells[rowIndex, startCol + 14]),
                            converter.GenerateValueInt(sheet.Cells[rowIndex, startCol + 15]),
                            converter.GenerateValueInt(sheet.Cells[rowIndex, startCol + 16]),
                            converter.GenerateValueString(sheet.Cells[rowIndex, startCol + 17])
                        ));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Gagal memproses Sheet Produksi_Digital_Transfer pada baris ke-{rowIndex} " + ex.Message);
            }
            try
            {
                if (listData.Count() > 0)
                {
                    await productionDigitalTransferAdapters.DeleteByMonthAndYear(periode);
                    await productionDigitalTransferAdapters.InsertBulk(listData);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Gagal menyimpan Sheet PRODUKSI_DIGITAL_TRANSFER - " + ex.Message);
            }

        }

        public async Task UploadSheetOrderIn(ExcelWorksheet sheet, DateTime periode)
        {
            var startRow = 3;
            var startCol = 1;
            var totalRows = sheet.Dimension.Rows;
            var totalColumns = sheet.Dimension.Columns;
            int rowIndex = 0;
            var listData = new List<OrderIn>();
            try
            {
                for (rowIndex = startRow; rowIndex <= totalRows; rowIndex++)
                {
                    if (sheet.Cells[rowIndex, startCol].Value != null)
                    {
                        generalHelper.ValidatePeriode(sheet.Cells[rowIndex,2], Periode);
                        listData.Add(new OrderIn(
                            generalHelper.GenerateId(sheet.Cells[rowIndex,2],sheet.Cells[rowIndex,1]),
                            converter.GenerateValueInt(sheet.Cells[rowIndex, startCol]),
                            generalHelper.ConvertDateTime(sheet.Cells[rowIndex,startCol + 1]),
                            converter.GenerateValueString(sheet.Cells[rowIndex, startCol + 2]),
                            converter.GenerateValueString(sheet.Cells[rowIndex, startCol + 3]),
                            converter.GenerateValueString(sheet.Cells[rowIndex, startCol + 4]),
                            converter.GeneratePureDouble(sheet.Cells[rowIndex, startCol + 5]),
                            converter.GenerateValueString(sheet.Cells[rowIndex, startCol + 6]),
                            converter.GenerateValueString(sheet.Cells[rowIndex, startCol + 7]),
                            converter.GeneratePureString(sheet.Cells[rowIndex, startCol + 8]),
                            converter.GenerateValueString(sheet.Cells[rowIndex, startCol + 9]),
                            converter.GenerateValueString(sheet.Cells[rowIndex, startCol + 10]),
                            converter.GenerateValueString(sheet.Cells[rowIndex, startCol + 11]),
                            converter.GenerateValueString(sheet.Cells[rowIndex, startCol + 12]),
                            converter.GenerateValueString(sheet.Cells[rowIndex, startCol + 13]),
                            generalHelper.ConvertDateTime(sheet.Cells[rowIndex,startCol + 14]),
                            generalHelper.ConvertDateTime(sheet.Cells[rowIndex,startCol + 15]),
                            generalHelper.ConvertDateTime(sheet.Cells[rowIndex,startCol + 16]),
                            generalHelper.ConvertNullDateTime(sheet.Cells[rowIndex,startCol + 17]),
                            generalHelper.ConvertNullDateTime(sheet.Cells[rowIndex,startCol + 18]),
                            converter.GenerateValueString(sheet.Cells[rowIndex, startCol + 19]),
                            converter.GeneratePureDouble(sheet.Cells[rowIndex, startCol + 20])
                        ));
                    }
                }


            }
            catch (Exception ex)
            {
                throw new Exception($"Gagal memproses Sheet Order_Masuk pada baris ke-{rowIndex} " + ex.Message);
            }

            try
            {
                if (listData.Count() > 0)
                {
                    await orderInAdapters.DeleteByMonthAndYear(periode);
                    await orderInAdapters.InsertBulk(listData);
                }
            }
            catch (Exception Ex)
            {
                throw new Exception($"Gagal menyimpan Sheet Order_Masuk - " + Ex.Message);
            }
        }

        public async Task UploadSheetSOOrderDigitalTransfer(ExcelWorksheet sheet, DateTime periode)
        {
            var startRow = 3;
            var startCol = 1;
            var totalRows = sheet.Dimension.Rows;
            var totalColumns = sheet.Dimension.Columns;
            int rowIndex = 0;
            var listData = new List<SOOrderDigitalTransfer>();
            try
            {
                for (rowIndex = startRow; rowIndex <= totalRows; rowIndex++)
                {
                    if (sheet.Cells[rowIndex, startCol].Value != null)
                    {
                        generalHelper.ValidatePeriode(sheet.Cells[rowIndex,2], Periode);
                        listData.Add(new SOOrderDigitalTransfer(
                            generalHelper.GenerateId(sheet.Cells[rowIndex,2],sheet.Cells[rowIndex,1]),
                            converter.GenerateValueInt(sheet.Cells[rowIndex, startCol]),
                            generalHelper.ConvertDateTime(sheet.Cells[rowIndex,startCol + 1]),
                            converter.GenerateValueInt(sheet.Cells[rowIndex, startCol + 2]),
                            converter.GenerateValueString(sheet.Cells[rowIndex, startCol + 3]),
                            converter.GenerateValueString(sheet.Cells[rowIndex, startCol + 4]),
                            converter.GenerateValueString(sheet.Cells[rowIndex, startCol + 5]),
                            converter.GenerateValueString(sheet.Cells[rowIndex, startCol + 6]),
                            converter.GenerateValueString(sheet.Cells[rowIndex, startCol + 7]),
                            converter.GenerateValueString(sheet.Cells[rowIndex, startCol + 8]),
                            converter.GenerateValueString(sheet.Cells[rowIndex, startCol + 9]),
                            converter.GenerateValueString(sheet.Cells[rowIndex, startCol + 10]),
                            converter.GenerateValueString(sheet.Cells[rowIndex, startCol + 11]),
                            generalHelper.ConvertDateTime(sheet.Cells[rowIndex,startCol + 12]),
                            generalHelper.ConvertNullDateTime(sheet.Cells[rowIndex,startCol + 13]),
                            converter.GeneratePureDouble(sheet.Cells[rowIndex, startCol + 14]),
                            converter.GenerateValueString(sheet.Cells[rowIndex, startCol + 15]),
                            converter.GenerateValueString(sheet.Cells[rowIndex, startCol + 16])
                        ));
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception($"Gagal memproses Sheet Order_SO_Digital_Transfer pada baris ke-{rowIndex} " + ex.Message);
            }

            try
            {

                if (listData.Count() > 0)
                {
                    await sOOrderDigitalTransferAdapters.DeleteByMonthAndYear(periode);
                    await sOOrderDigitalTransferAdapters.InsertBulk(listData);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Gagal menyimpan Sheet ORDER_SO_DIGITAL_Transfer - " + ex.Message);
            }
        }

        public async Task UploadSheetWipArea(ExcelWorksheet sheet, DateTime periode)
        {
            var startRow = 3;
            var startCol = 1;
            var totalRows = sheet.Dimension.Rows;
            var totalColumns = sheet.Dimension.Columns;
            int rowIndex = 0;
            var listData = new List<WipArea>();
            try
            {
                for (rowIndex = startRow; rowIndex <= totalRows; rowIndex++)
                {
                    if (sheet.Cells[rowIndex, startCol].Value != null)
                    {
                        generalHelper.ValidatePeriode(sheet.Cells[rowIndex,2], Periode);
                        listData.Add(new WipArea(
                            generalHelper.GenerateId(sheet.Cells[rowIndex,2],sheet.Cells[rowIndex,1]),
                            converter.GenerateValueInt(sheet.Cells[rowIndex, startCol]),
                            generalHelper.ConvertDateTime(sheet.Cells[rowIndex,startCol + 1]),
                            converter.GenerateValueString(sheet.Cells[rowIndex, startCol + 2]),
                            converter.GenerateValueString(sheet.Cells[rowIndex, startCol + 3]),
                            converter.GeneratePureString(sheet.Cells[rowIndex, startCol + 4]),
                            converter.GeneratePureDouble(sheet.Cells[rowIndex, startCol + 5]),
                            converter.GeneratePureDouble(sheet.Cells[rowIndex, startCol + 6]),
                            converter.GeneratePureDouble(sheet.Cells[rowIndex, startCol + 7]),
                            converter.GenerateValueString(sheet.Cells[rowIndex, startCol + 8]),
                            converter.GenerateValueString(sheet.Cells[rowIndex, startCol + 9])
                        ));
                    }
                }


            }
            catch (Exception ex)
            {
                throw new Exception($"Gagal memproses Sheet WIP_AREA pada baris ke-{rowIndex} " + ex.Message);
            }
            try
            {
                if (listData.Count() > 0)
                {
                    await wipAreaAdapters.DeleteByMonthAndYear(periode);
                    await wipAreaAdapters.InsertBulk(listData);
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Gagal menyimpan Sheet WIP_AREA - " + ex.Message);
            }
        }

        public async Task UploadSheetDailyDelivery(ExcelWorksheet sheet, DateTime periode)
        {
            var startRow = 3;
            var startCol = 1;
            var totalRows = sheet.Dimension.Rows;
            var totalColumns = sheet.Dimension.Columns;
            int rowIndex = 0;
            var listData = new List<DailyDelivery>();
            try
            {
                for (rowIndex = startRow; rowIndex <= totalRows; rowIndex++)
                {
                    if (sheet.Cells[rowIndex, startCol].Value != null)
                    {
                        generalHelper.ValidatePeriode(sheet.Cells[rowIndex,2], Periode);
                        listData.Add(new DailyDelivery(
                            generalHelper.GenerateId(sheet.Cells[rowIndex,2],sheet.Cells[rowIndex,1]),
                            converter.GenerateValueInt(sheet.Cells[rowIndex, startCol]),
                            generalHelper.ConvertDateTime(sheet.Cells[rowIndex,startCol + 1]),
                            converter.GenerateValueString(sheet.Cells[rowIndex, startCol + 2]),
                            converter.GenerateValueString(sheet.Cells[rowIndex, startCol + 3]),
                            converter.GenerateValueString(sheet.Cells[rowIndex, startCol + 4]),
                            converter.GenerateValueString(sheet.Cells[rowIndex, startCol + 5]),
                            converter.GeneratePureDouble(sheet.Cells[rowIndex, startCol + 6]),
                            converter.GeneratePureDouble(sheet.Cells[rowIndex, startCol + 7]),
                            converter.GeneratePureDouble(sheet.Cells[rowIndex, startCol + 8]),
                            converter.GeneratePureDouble(sheet.Cells[rowIndex, startCol + 9]),
                            converter.GeneratePureDouble(sheet.Cells[rowIndex, startCol + 10]),
                            converter.GeneratePureDouble(sheet.Cells[rowIndex, startCol + 11]),
                            converter.GenerateValueString(sheet.Cells[rowIndex, startCol + 12]),
                            converter.GeneratePureDouble(sheet.Cells[rowIndex, startCol + 13])
                        ));
                    }
                }



            }
            catch (Exception ex)
            {
                throw new Exception($"Gagal memproses Sheet Pengiriman_Harian pada baris ke-{rowIndex} " + ex.Message);
            }

            try
            {
                if (listData.Count() > 0)
                {
                    await dailyDeliveryAdapters.DeleteByMonthAndYear(periode);
                    await dailyDeliveryAdapters.InsertBulk(listData);
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Gagal menyimpan Sheet Pengiriman_Harian " + ex.Message);
            }
        }
    }
}