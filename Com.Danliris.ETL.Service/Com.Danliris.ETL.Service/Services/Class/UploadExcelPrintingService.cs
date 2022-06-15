using Com.Danliris.ETL.Service.DBAdapters;
using Com.Danliris.ETL.Service.DBAdapters.PrintingAdapters;
using Com.Danliris.ETL.Service.ExcelModels;
using Com.Danliris.ETL.Service.Models;
using Com.Danliris.ETL.Service.Models.DashboardPrintingModels;
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
    public class UploadExcelPrintingService : IUploadExcelPrintingService
    {
        static string connectionStringPrinting = Environment.GetEnvironmentVariable("ConnectionStrings:SQLConnectionString", EnvironmentVariableTarget.Process);

        public IMainMachineAdapter mainMachineAdapter;
        public ISupportMachineAdapter supportMachineAdapter;
        public IPasteAdapter pasteAdapter;
        public IEngravingAdapter engravingAdapter;
        public IWipHandoverAdapter wipHandoverAdapter;

        ConverterChecker converter = new ConverterChecker();
        GeneralHelper generalHelper = new GeneralHelper();

        public UploadExcelPrintingService(IServiceProvider provider)
        {
            mainMachineAdapter = provider.GetService<IMainMachineAdapter>();
            supportMachineAdapter = provider.GetService<ISupportMachineAdapter>();
            pasteAdapter = provider.GetService<IPasteAdapter>();
            engravingAdapter = provider.GetService<IEngravingAdapter>();
            wipHandoverAdapter = provider.GetService<IWipHandoverAdapter>();
        }

        public async Task Upload(ExcelWorksheets sheet, DateTime periode)
        {
            try
            {
                var data = 0;
                foreach (var fileName in sheet)
                {
                    switch (fileName.Name.ToLower())
                    {
                        case "mesin_utama":
                            await InsertExcelMainMachine(sheet, periode, data);
                            break;
                        case "mesin_pendukung":
                            await InsertExcelSupportMachine(sheet, periode, data);
                            break;
                        case "pasta":
                            await InsertExcelPaste(sheet, periode, data);
                            break;
                        case "engraving":
                            await InsertExcelEngraving(sheet, periode, data);
                            break;
                        case "wip_(serah_terima_area)":
                            await InsertExcelWIPHandover(sheet, periode, data);
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
        public async Task InsertExcelMainMachine(ExcelWorksheets sheet, DateTime periode, int data)
        {
            var sheet1 = sheet[data];

            var startRow = 3;
            var startCol = 1;

            var totalRows = sheet1.Dimension.Rows;
            var totalColumn = sheet1.Dimension.Columns;
            var listData = new List<DashBoardMainMachineModel>();
            int rowIndex = 0;
            try
            {
                for (rowIndex = startRow; rowIndex <= totalRows; rowIndex++)
                {
                    if (sheet1.Cells[rowIndex, startCol].Value != null)
                    {
                        generalHelper.ValidatePeriode(sheet1.Cells[rowIndex,2], periode);
                        listData.Add(new DashBoardMainMachineModel(
                            generalHelper.GenerateId(sheet1.Cells[rowIndex,2],sheet1.Cells[rowIndex,1]),
                            Convert.ToInt32(sheet1.Cells[rowIndex, startCol].Value),
                            generalHelper.ConvertDateTime(sheet1.Cells[rowIndex, startCol + 1]),
                            converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 2]),
                            converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 3]),
                            converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 4]),
                            converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 5]),
                            converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 6]),
                            converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 7]),
                            converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 8]),
                            converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 9]),
                            converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 10]),
                            converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 11]),
                            converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 12]),
                            converter.GenerateValueDouble(sheet1.Cells[rowIndex, startCol + 13]),
                            converter.GenerateValueDouble(sheet1.Cells[rowIndex, startCol + 14]),
                            converter.GenerateValueDouble(sheet1.Cells[rowIndex, startCol + 15]),
                            converter.GenerateValueDouble(sheet1.Cells[rowIndex, startCol + 16]),
                            converter.GenerateValueDouble(sheet1.Cells[rowIndex, startCol + 17])
                        ));
                    }
                }
            }
            catch(Exception ex){
                throw new Exception($"Gagal memproses Sheet Mesin_Utama pada baris ke-{rowIndex} - {ex.Message}");
            }

            try{
                if (listData.Count() > 0)
                {
                    await mainMachineAdapter.DeleteByMonthAndYear(periode);
                    await mainMachineAdapter.InsertBulk(listData);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Gagal menyimpan Sheet Mesin_Utama" + ex.Message);
            }
        }

        public async Task InsertExcelSupportMachine(ExcelWorksheets sheet, DateTime periode, int data)
        {
            var sheet1 = sheet[data];

            var startRow = 3;
            var startCol = 1;

            var totalRows = sheet1.Dimension.Rows;
            var totalColumn = sheet1.Dimension.Columns;
            var listData = new List<DashBoardSupportMachineModel>();
            int rowIndex = 0;            

            try
            {
                for (rowIndex = startRow; rowIndex <= totalRows; rowIndex++)
                {
                    if (sheet1.Cells[rowIndex, startCol].Value != null)
                    {
                        generalHelper.ValidatePeriode(sheet1.Cells[rowIndex,2], periode);
                        listData.Add(new DashBoardSupportMachineModel(
                            generalHelper.GenerateId(sheet1.Cells[rowIndex,2],sheet1.Cells[rowIndex,1]),
                            Convert.ToInt32(sheet1.Cells[rowIndex, startCol].Value),
                            generalHelper.ConvertDateTime(sheet1.Cells[rowIndex, startCol + 1]),
                            converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 2]),
                            converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 3]),
                            converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 4]),
                            converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 5]),
                            converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 6]),
                            converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 7]),
                            converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 8]),
                            converter.GenerateValueDouble(sheet1.Cells[rowIndex, startCol + 9]),
                            converter.GenerateValueDouble(sheet1.Cells[rowIndex, startCol + 10]),
                            converter.GenerateValueDouble(sheet1.Cells[rowIndex, startCol + 11]),
                            converter.GenerateValueDouble(sheet1.Cells[rowIndex, startCol + 12])

                        ));
                    }
                }
            }
            catch(Exception ex){
                throw new Exception($"Gagal memproses Sheet Mesin_Pendukung pada baris ke-{rowIndex} - {ex.Message}");
            }

            try{
                if (listData.Count() > 0)
                {
                    await supportMachineAdapter.DeleteByMonthAndYear(periode);
                    await supportMachineAdapter.InsertBulk(listData);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Gagal menyimpan Sheet Mesin_Pendukung" + ex.Message);
            }
        }

        public async Task InsertExcelPaste(ExcelWorksheets sheet, DateTime periode, int data)
        {
            var sheet1 = sheet[data];

            var startRow = 3;
            var startCol = 1;

            var totalRows = sheet1.Dimension.Rows;
            var totalColumn = sheet1.Dimension.Columns;
            var listData = new List<DashBoardPasteModel>();
            int rowIndex = 0;            
            try
            {
                for (rowIndex = startRow; rowIndex <= totalRows; rowIndex++)
                {
                    if (sheet1.Cells[rowIndex, startCol].Value != null)
                    {
                        generalHelper.ValidatePeriode(sheet1.Cells[rowIndex,2], periode);
                        listData.Add(new DashBoardPasteModel(
                            generalHelper.GenerateId(sheet1.Cells[rowIndex,2],sheet1.Cells[rowIndex,1]),
                            Convert.ToInt32(sheet1.Cells[rowIndex, startCol].Value),
                            generalHelper.ConvertDateTime(sheet1.Cells[rowIndex, startCol + 1]),
                            converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 2]),
                            converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 3]),
                            converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 4]),
                            converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 5]),
                            converter.GenerateValueDouble(sheet1.Cells[rowIndex, startCol + 6]),
                            converter.GenerateValueDouble(sheet1.Cells[rowIndex, startCol + 7]),
                            converter.GenerateValueDouble(sheet1.Cells[rowIndex, startCol + 8]),
                            converter.GenerateValueDouble(sheet1.Cells[rowIndex, startCol + 9])
                        ));
                    }
                }
            }
            catch(Exception ex){
                throw new Exception($"Gagal memproses Sheet Pasta pada baris ke-{rowIndex} - {ex.Message}");
            }

            try{
                if (listData.Count() > 0)
                {
                    await pasteAdapter.DeleteByMonthAndYear(periode);
                    await pasteAdapter.InsertBulk(listData);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Gagal menyimpan Sheet Pasta" + ex.Message);
            }

        }

        public async Task InsertExcelEngraving(ExcelWorksheets sheet, DateTime periode, int data)
        {
            var sheet1 = sheet[data];

            var startRow = 3;
            var startCol = 1;

            var totalRows = sheet1.Dimension.Rows;
            var totalColumn = sheet1.Dimension.Columns;
            var listData = new List<DashBoardEngravingModel>();
            int rowIndex = 0;            
            try
            {
                for (rowIndex = startRow; rowIndex <= totalRows; rowIndex++)
                {
                    if (sheet1.Cells[rowIndex, startCol].Value != null)
                    {
                        generalHelper.ValidatePeriode(sheet1.Cells[rowIndex,2], periode);                        
                        listData.Add(new DashBoardEngravingModel(
                            generalHelper.GenerateId(sheet1.Cells[rowIndex,2],sheet1.Cells[rowIndex,1]),
                            Convert.ToInt32(sheet1.Cells[rowIndex, startCol].Value),
                            generalHelper.ConvertDateTime(sheet1.Cells[rowIndex, startCol + 1]),
                            converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 2]),
                            converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 3]),
                            converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 4]),
                            converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 5]),
                            converter.GenerateValueDouble(sheet1.Cells[rowIndex, startCol + 6]),
                            converter.GenerateValueDouble(sheet1.Cells[rowIndex, startCol + 7])
                        ));
                    }
                }
            }
            catch(Exception ex){
                throw new Exception($"Gagal memproses Sheet Engraving pada baris ke-{rowIndex} - {ex.Message}");
            }

            try{
                if (listData.Count() > 0)
                {
                    await engravingAdapter.DeleteByMonthAndYear(periode);
                    await engravingAdapter.InsertBulk(listData);
                }


            }
            catch (Exception ex)
            {
                throw new Exception("Gagal menyimpan Sheet Engraving" + ex.Message);
            }
        }

        public async Task InsertExcelWIPHandover(ExcelWorksheets sheet, DateTime periode, int data)
        {
            var sheet1 = sheet[data];

            var startRow = 3;
            var startCol = 1;

            var totalRows = sheet1.Dimension.Rows;
            var totalColumn = sheet1.Dimension.Columns;
            var listData = new List<DashBoardWIPHandoverAreaModel>();
            int rowIndex = 0;                        
            try
            {
                for (rowIndex = startRow; rowIndex <= totalRows; rowIndex++)
                {
                    if (sheet1.Cells[rowIndex, startCol].Value != null)
                    {
                        generalHelper.ValidatePeriode(sheet1.Cells[rowIndex,2], periode);                        
                        listData.Add(new DashBoardWIPHandoverAreaModel(
                            generalHelper.GenerateId(sheet1.Cells[rowIndex,2],sheet1.Cells[rowIndex,1]),
                            Convert.ToInt32(sheet1.Cells[rowIndex, startCol].Value),
                            generalHelper.ConvertDateTime(sheet1.Cells[rowIndex, startCol + 1]),
                            converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 2]),
                            converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 3]),
                            converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 4]),
                            converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 5]),
                            converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 6]),
                            converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 7]),
                            converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 8]),
                            converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 9]),
                            converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 10]),
                            generalHelper.ConvertNullDateTime(sheet1.Cells[rowIndex, startCol + 11]),
                            converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 12]),
                            converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 13]),
                            converter.GenerateValueDouble(sheet1.Cells[rowIndex, startCol + 14]),
                            converter.GenerateValueDouble(sheet1.Cells[rowIndex, startCol + 15]),
                            converter.GenerateValueDouble(sheet1.Cells[rowIndex, startCol + 16]),
                            converter.GenerateValueDouble(sheet1.Cells[rowIndex, startCol + 17]),
                            converter.GenerateValueDouble(sheet1.Cells[rowIndex, startCol + 18]),
                            converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 19]),
                            converter.GenerateValueDouble(sheet1.Cells[rowIndex, startCol + 20])
                        ));
                    }
                }
            }
            catch(Exception ex){
                throw new Exception($"Gagal memproses Sheet WIP_(Serah_Terima_Area) pada baris ke-{rowIndex} - {ex.Message}");
            }

            try{
                if (listData.Count() > 0)
                {
                    await wipHandoverAdapter.DeleteByMonthAndYear(periode);
                    await wipHandoverAdapter.InsertBulk(listData);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Gagal menyimpan Sheet WIP_(Serah_Terima_Area)" + ex.Message);
            }
        }        
    }
}
