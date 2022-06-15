using Com.Danliris.ETL.Service.DBAdapters.MaintenanceAdapters;
using Com.Danliris.ETL.Service.Models.DashboardMaintenanceModels;
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
    public class UploadExcelMaintenanceService : IUploadExcelMaintenanceService
    {
        static string connectionString = Environment.GetEnvironmentVariable("ConnectionStrings:SQLConnectionString", EnvironmentVariableTarget.Process);
        public IDashboardHandoverAdapter handoverAdapter;
        public IDashboardLkmAdapter lkmAdapter;
        ConverterChecker converter = new ConverterChecker();
        GeneralHelper general = new GeneralHelper();

        public UploadExcelMaintenanceService(IServiceProvider provider)
        {
            handoverAdapter = provider.GetService<IDashboardHandoverAdapter>();
            lkmAdapter = provider.GetService<IDashboardLkmAdapter>();
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
                        case "serah_terima":
                            await UploadHandover(sheet, periode, data);
                            break;

                        case "lkm":
                            await UploadLkm(sheet, periode, data);
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

        #region Serah_Terima
        public async Task UploadHandover(ExcelWorksheets sheet, DateTime periode, int data)
        {
            var sheet1 = sheet[data];
            var startRow = 4;
            var startCol = 1;

            var totalRows = sheet1.Dimension.Rows;
            var totalCols = sheet1.Dimension.Columns;

            var listData = new List<DashboardHandoverModel>();
            int rowIndex = 0;
            try
            {
                for (rowIndex = startRow; rowIndex <= totalRows; rowIndex++)
                {

                    if (sheet1.Cells[rowIndex, startCol].Value != null)
                    {
                        general.ValidatePeriode(sheet1.Cells[rowIndex,2], periode);
                        listData.Add(new DashboardHandoverModel(
                           general.GenerateId(sheet1.Cells[rowIndex, startCol + 1], sheet1.Cells[rowIndex, 1]), //ID
                           Convert.ToInt32(sheet1.Cells[rowIndex, startCol].Value), //No
                           general.ConvertDateTime(sheet1.Cells[rowIndex, startCol + 1]), //Date
                           converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 2]), //Machine
                           converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 3]), //Repair
                           converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 4]), //Result
                           converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 5]), //Team
                           converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 6]), //Implementer
                           converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 7]), //KasubsieVerification
                           converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 8]), //KasieVerification
                           converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 9]) //Production                           
                        ));
                    }
                }
            }
            catch(Exception ex){
                throw new Exception($"Gagal memproses Sheet Serah_Terima pada baris ke-{rowIndex} - {ex.Message}");
            }

            try{
                if (listData.Count() > 0)
                {
                    await handoverAdapter.DeleteByMonthAndYear(periode);
                    await handoverAdapter.InsertBulk(listData);                    
                }

            }
            catch (Exception ex)
            {

                throw new Exception("Gagal menyimpan Sheet Serah_Terima - " + ex.Message);
            }
        }
        
        #endregion

        #region LKM
        public async Task UploadLkm(ExcelWorksheets sheet, DateTime periode, int data)
        {
            var sheet1 = sheet[data];
            var startRow = 4;
            var startCol = 1;

            var totalRows = sheet1.Dimension.Rows;
            var totalCols = sheet1.Dimension.Columns;

            var listData = new List<DashboardLkmModel>();
            int rowIndex = 0;
            try
            {
                for (rowIndex = startRow; rowIndex <= totalRows; rowIndex++)
                {

                    if (sheet1.Cells[rowIndex, startCol].Value != null)
                    {
                        general.ValidatePeriode(sheet1.Cells[rowIndex,2], periode);
                        listData.Add(new DashboardLkmModel(
                           general.GenerateId(sheet1.Cells[rowIndex, startCol + 1], sheet1.Cells[rowIndex, 1]), //ID
                           Convert.ToInt32(sheet1.Cells[rowIndex, startCol].Value), //No
                           general.ConvertDateTime(sheet1.Cells[rowIndex, startCol + 1]), //Date
                           converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 2]), //Section
                           converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 3]), //Machine
                           converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 4]), //Problem
                           converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 5]), //Action
                           converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 6]), //UsageSparepart
                           converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 7]), //Description
                           converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 8]), //Operator1
                           converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 9]), //Operator2
                           converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 10]), //Operator3    
                           converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 11]), //KnownBy
                           converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 12]), //Team
                           general.ConvertNullDateTime(sheet1.Cells[rowIndex, startCol + 13]) //RemaintenanceDate                          
                        ));
                    }
                }
            }
            catch(Exception ex){
                throw new Exception($"Gagal memproses Sheet LKM pada baris ke-{rowIndex} - {ex.Message}");
            }

            try{
                if (listData.Count() > 0)
                {
                    await lkmAdapter.DeleteByMonthAndYear(periode);
                    await lkmAdapter.InsertBulk(listData);                    
                }

            }
            catch (Exception ex)
            {

                throw new Exception("Gagal menyimpan Sheet LKM - " + ex.Message);
            }
        }
        #endregion
    }
}
