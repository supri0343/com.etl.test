using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Com.Danliris.ETL.Service.DBAdapters;
using Com.Danliris.ETL.Service.DBAdapters.FinishingAdapters;
using Com.Danliris.ETL.Service.ExcelModels;
using Com.Danliris.ETL.Service.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using Microsoft.Extensions.DependencyInjection;
using Com.Danliris.ETL.Service.Services.Interfaces;

namespace Com.Danliris.ETL.Service.Services
{
    public class UploadExcelFinishingService : IUploadExcelFinishingService
    {

        static string connectionString = Environment.GetEnvironmentVariable("ConnectionStrings:SQLConnectionString", EnvironmentVariableTarget.Process);

        IProductionAdapter productionAdapter;
        IChemicalAdapter chemicalAdapter;
        IReprocessAdapter reprocessAdapter;
        ITransferAdapter transferAdapter;
        IReceiveAdapter receiveAdapter;
        IStockOpnameAdapter stockOpnameAdapter;
        ConverterChecker converterChecker = new ConverterChecker();
        GeneralHelper generalHelper = new GeneralHelper();

        public UploadExcelFinishingService(IServiceProvider provider)
        {
            productionAdapter = provider.GetService<IProductionAdapter>();
            chemicalAdapter = provider.GetService<IChemicalAdapter>();
            reprocessAdapter = provider.GetService<IReprocessAdapter>();
            transferAdapter = provider.GetService<ITransferAdapter>();
            receiveAdapter = provider.GetService<IReceiveAdapter>();
            stockOpnameAdapter = provider.GetService<IStockOpnameAdapter>();
        }
        public async Task Upload(ExcelWorksheets sheets, DateTime periode)
        {
            try
            {
                foreach (var sheet in sheets)
                {
                    switch (sheet.Name)
                    {
                        case "Produksi_F.":
                            await UploadProduksiF(sheet, periode);
                            break;
                        case "Chemical_F.":
                            await UploadChemicalF(sheet, periode);
                            break;
                        case "Reproses_F.":
                            await UploadReprocessF(sheet, periode);
                            break;
                        case "Penyerahan_F.":
                            await UploadTransferF(sheet, periode);
                            break;
                        case "Terima_F.":
                            await UploadReceiveF(sheet, periode);
                            break;
                        case "Stock_Opname_F.":
                            await UploadStockOpname(sheet, periode);
                            break;
                        default:
                            throw (new Exception($"Tidak ada sheet yang sesuai untuk area ini"));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

#region Produksi F
        public async Task UploadProduksiF(ExcelWorksheet excel, DateTime periode)
        {
            var sheet = excel;
            var totalRow = sheet.Dimension.Rows;
            var listData = new List<DashboardProductionF>();
            int rowIndex =0;

            try
            {
                for (rowIndex = 3; rowIndex <= totalRow; rowIndex++)
                {
                    if (sheet.Cells[rowIndex,1].Value != null) {
                        generalHelper.ValidatePeriode(sheet.Cells[rowIndex,3], periode);
                        listData.Add(new DashboardProductionF(                        
                            Convert.ToInt32(sheet.Cells[rowIndex, 1].Value.ToString()),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,2]),
                            generalHelper.ConvertDateTime(sheet.Cells[rowIndex,3]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,4]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,5]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,6]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,7]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,8]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,9]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,10]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,11]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,12]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,13]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,14])
                        ));    
                    }
                }
            }            
            catch(Exception ex){
                throw new Exception($"Gagal memproses Sheet Produksi_F pada baris ke-{rowIndex} - {ex.Message}");
            }

            try{
                if(listData.Count() > 0){
                    await productionAdapter.DeleteByMonthAndYear(periode);
                    await productionAdapter.InsertBulk(listData);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Gagal menyimpan Sheet Produksi_F - " + ex.Message);
            }
        }        
#endregion

#region Chemical F
        public async Task UploadChemicalF(ExcelWorksheet excel, DateTime periode)
        {
            var sheet = excel;
            var totalRow = sheet.Dimension.Rows;
            var listData = new List<DashboardChemicalF>();
            int rowIndex = 0;
            try
            {
                for (rowIndex = 3; rowIndex <= totalRow; rowIndex++)
                {
                    if (sheet.Cells[rowIndex,1].Value != null) {                     
                        generalHelper.ValidatePeriode(sheet.Cells[rowIndex,2], periode);
                        listData.Add(new DashboardChemicalF(                        
                            Convert.ToInt32(sheet.Cells[rowIndex, 1].Value.ToString()),
                            generalHelper.ConvertDateTime(sheet.Cells[rowIndex,2]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,3]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,4]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,5]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,6]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,7]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,8]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,9]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,10]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,11]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,12])
                        ));   
                    }
                }
            }
            catch(Exception ex){
                throw new Exception($"Gagal memproses Sheet Chemical_F pada baris ke-{rowIndex} - {ex.Message}");
            }

            try{
                if(listData.Count() > 0){
                    await chemicalAdapter.DeleteByMonthAndYear(periode);
                    await chemicalAdapter.InsertBulk(listData);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Gagal menyimpan Sheet Chemical_F - " + ex.Message);
            }
        }   
#endregion

#region Reprocess F
        public async Task UploadReprocessF(ExcelWorksheet excel, DateTime periode)
        {
            var sheet = excel;
            var totalRow = sheet.Dimension.Rows;

            var listData = new List<DashboardReprocessF>();
            int rowIndex = 0;
            try
            {
                for (rowIndex = 3; rowIndex <= totalRow; rowIndex++)
                {   
                    if (sheet.Cells[rowIndex,1].Value != null) { 
                        generalHelper.ValidatePeriode(sheet.Cells[rowIndex,3], periode);
                        listData.Add(new DashboardReprocessF(                        
                            Convert.ToInt32(sheet.Cells[rowIndex, 1].Value.ToString()),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,2]),
                            generalHelper.ConvertDateTime(sheet.Cells[rowIndex,3]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,4]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,5]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,6]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,7]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,8]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,9]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,10])
                        ));
                    }
                }
            }
            catch(Exception ex){
                throw new Exception($"Gagal memproses Sheet Reproses_F pada baris ke-{rowIndex} - {ex.Message}");
            }

            try{
                if(listData.Count() > 0){
                    await reprocessAdapter.DeleteByMonthAndYear(periode);
                    await reprocessAdapter.InsertBulk(listData);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Gagal menyimpan Sheet Reproses_F - " + ex.Message);
            }
        }
#endregion

#region Transfer F
        public async Task UploadTransferF(ExcelWorksheet excel, DateTime periode)
        {
            var sheet = excel;
            var totalRow = sheet.Dimension.Rows;
            var listData = new List<DashboardTransferF>();
            int rowIndex = 0;

            try
            {
                for (rowIndex = 3; rowIndex <= totalRow; rowIndex++)
                {
                    if (sheet.Cells[rowIndex,1].Value != null) { 
                        generalHelper.ValidatePeriode(sheet.Cells[rowIndex,2], periode);
                        listData.Add(new DashboardTransferF(                        
                            Convert.ToInt32(sheet.Cells[rowIndex, 1].Value.ToString()),
                            generalHelper.ConvertDateTime(sheet.Cells[rowIndex,2]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,3]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,4]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,5]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,6]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,7]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,8]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,9])
                        ));
                    }
                }

            }
            catch(Exception ex){
                throw new Exception($"Gagal memproses Sheet Penyerahan_F pada baris ke-{rowIndex} - {ex.Message}");
            }

            try{
                if(listData.Count() > 0){
                    await transferAdapter.DeleteByMonthAndYear(periode);
                    await transferAdapter.InsertBulk(listData);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Gagal menyimpan Sheet Penyerahan_F - " + ex.Message);
            }
        }
#endregion

#region Receive F
        public async Task UploadReceiveF(ExcelWorksheet excel, DateTime periode)
        {
            var sheet = excel;
            var totalRow = sheet.Dimension.Rows;
            var listData = new List<DashboardReceiveF>();
            int rowIndex = 0;
            try
            {
                for (rowIndex = 3; rowIndex <= totalRow; rowIndex++)
                {                    
                    if (sheet.Cells[rowIndex,1].Value != null) { 
                        generalHelper.ValidatePeriode(sheet.Cells[rowIndex,2], periode);
                        listData.Add(new DashboardReceiveF(                        
                            Convert.ToInt32(sheet.Cells[rowIndex, 1].Value.ToString()),
                            generalHelper.ConvertDateTime(sheet.Cells[rowIndex,2]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,3]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,4]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,5]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,6]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,7]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,8]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,9])
                        ));
                    }
                }
            }
            catch(Exception ex){
                throw new Exception($"Gagal memproses Sheet Terima_F pada baris ke-{rowIndex} - {ex.Message}");
            }

            try{
                if(listData.Count() > 0){
                    await receiveAdapter.DeleteByMonthAndYear(periode);
                    await receiveAdapter.InsertBulk(listData);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Gagal menyimpan Sheet Terima_F - " + ex.Message);
            }
        }
#endregion

#region Stock Opname
        public async Task UploadStockOpname(ExcelWorksheet excel, DateTime periode)
        {
            var sheet = excel;
            var totalRow = sheet.Dimension.Rows;
            var listData = new List<DashboardStockOpnameF>();
            int rowIndex = 0;
            try
            {
                for (rowIndex = 3; rowIndex <= totalRow; rowIndex++)
                {                    
                    if (sheet.Cells[rowIndex,1].Value != null) { 
                        generalHelper.ValidatePeriode(sheet.Cells[rowIndex,2], periode);
                        listData.Add(new DashboardStockOpnameF(                        
                            Convert.ToInt32(sheet.Cells[rowIndex, 1].Value.ToString()),
                            generalHelper.ConvertDateTime(sheet.Cells[rowIndex,2]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,3]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,4]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,5]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,6]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,7])
                        ));
                    }
                }
            }
            catch(Exception ex){
                throw new Exception($"Gagal memproses Sheet Stock_Opname_F pada baris ke-{rowIndex} - {ex.Message}");
            }

            try{
                if(listData.Count() > 0){
                    await stockOpnameAdapter.DeleteByMonthAndYear(periode);
                    await stockOpnameAdapter.InsertBulk(listData);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Gagal menyimpan Sheet Stock_Opname_F - " + ex.Message);
            }
        }
#endregion
    }
}