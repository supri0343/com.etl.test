using OfficeOpenXml;
using Microsoft.Extensions.Logging;
using Com.Danliris.ETL.Service.ExcelModels.DashboardKoranLaborat;
using System.Collections.Generic;
using System;
using Com.Danliris.ETL.Service.DBAdapters.KoranLaboratAdapters;
using System.Linq;
using Com.Danliris.ETL.Service.Tools;
using System.Threading.Tasks;
using Com.Danliris.ETL.Service.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Com.Danliris.ETL.Service.Services.Class
{
    public class UploadExcelKoranLaboratService : IUploadExcelKoranLaboratService
    {
        static string connectionString = Environment.GetEnvironmentVariable("ConnectionStrings:SQLConnectionString", EnvironmentVariableTarget.Process);  
        IDyeingTestAdapter dyeingTestAdapter;        
        ILabDipAdapter labDipAdapter;    
        IPrintingLabAdapter printingLabAdapter;
        IPrintingTestAdapter printingTestAdapter; 
        IRnDTestResultAdapter rnDTestResultAdapter;
        ConverterChecker converterChecker = new ConverterChecker();
        GeneralHelper generalHelper = new GeneralHelper();
        public UploadExcelKoranLaboratService(IServiceProvider serviceProvider)
        {
            dyeingTestAdapter = serviceProvider.GetService<IDyeingTestAdapter>();
            labDipAdapter = serviceProvider.GetService<ILabDipAdapter>();
            printingLabAdapter = serviceProvider.GetService<IPrintingLabAdapter>();
            printingTestAdapter = serviceProvider.GetService<IPrintingTestAdapter>();
            rnDTestResultAdapter = serviceProvider.GetService<IRnDTestResultAdapter>();
        }
        public async Task Upload(ExcelWorksheets excel, DateTime periode) {
            
            try{
                var data=0;
                foreach (var item in excel)
                {
                    switch(item.Name.ToUpper().Trim())
                    {
                        case "TEST_DYEING":
                            await UploadDyeingTest(excel, periode, data);
                            break;
                        case "TEST_PRINTING":
                            await UploadPrintingTest(excel, periode, data);
                            break;
                        case "LAB_DIP":
                            await UploadLabDip(excel, periode, data);
                            break;
                        case "LAB_PRINTING":
                            await UploadLabPrinting(excel, periode, data);
                            break;
                        case "HASIL_TEST_RND":
                            await UploadRnDTestResult(excel, periode, data);
                            break;
                        default:
                            throw new Exception(item.Name + " tidak valid");                        
                    }
                    data++;
                }
            }catch(Exception ex){
                throw ex;
            }
        }

#region TEST DYEING
        private async Task UploadDyeingTest(ExcelWorksheets excel, DateTime periode ,int data){
            var sheet = excel[data];
            var totalRow = sheet.Dimension.Rows;
            var listData = new List<DashboardDyeingTest>();
            int rowIndex = 0;
            try{
                for(rowIndex = 3; rowIndex <= totalRow; rowIndex ++) {
                    if (sheet.Cells[rowIndex,2].Value != null) {
                        generalHelper.ValidatePeriode(sheet.Cells[rowIndex,2], periode);
                        listData.Add(new DashboardDyeingTest(
                            generalHelper.GenerateId(sheet.Cells[rowIndex,2],sheet.Cells[rowIndex,1]),
                            Convert.ToInt32(sheet.Cells[rowIndex,1].Value),
                            generalHelper.ConvertDateTime(sheet.Cells[rowIndex,2]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,3]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,4]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,5]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,6]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,7]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,8]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,9]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,10]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,11]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,12]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,13]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,14]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,15]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,16]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,17]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,18]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,19]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,20]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,21]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,22]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,23]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,24]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,25]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,26]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,27]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,28]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,29]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,30]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,31]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,32]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,33]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,34]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,35])
                        ));
                    }
                }
            }
            catch(Exception ex){
                throw new Exception($"Gagal memproses Sheet Test_Dyeing pada baris ke-{rowIndex} - {ex.Message}");
            }

            try{
                if(listData.Count()>0){
                    await dyeingTestAdapter.DeleteByMonthAndYear(periode);
                    await dyeingTestAdapter.InsertBulk(listData);                    
                }
            }catch(Exception ex){
                throw new Exception("Gagal menyimpan sheet Test_Dyeing - " + ex.Message);
            }
        }

#endregion

#region TEST PRINTING
        private async Task UploadPrintingTest(ExcelWorksheets excel, DateTime periode ,int data){
            var sheet = excel[data];
            var totalRow = sheet.Dimension.Rows;
            var listData = new List<DashboardPrintingTest>();
            int rowIndex = 0;
            try{
                for(rowIndex = 3; rowIndex <= totalRow; rowIndex ++) {
                    if (sheet.Cells[rowIndex,2].Value != null) {
                        generalHelper.ValidatePeriode(sheet.Cells[rowIndex,2], periode);
                        listData.Add(new DashboardPrintingTest(
                            generalHelper.GenerateId(sheet.Cells[rowIndex,2],sheet.Cells[rowIndex,1]),
                            Convert.ToInt32(sheet.Cells[rowIndex,1].Value),
                            generalHelper.ConvertDateTime(sheet.Cells[rowIndex,2]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,3]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,4]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,5]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,6]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,7]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,8]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,9]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,10]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,11]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,12]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,13]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,14]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,15]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,16]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,17]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,18]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,19]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,20]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,21]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,22]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,23]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,24]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,25]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,26]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,27]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,28]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,29]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,30]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,31]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,32]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,33]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,34]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,35])
                        ));
                    }
                }
            }
            catch(Exception ex){
                throw new Exception($"Gagal memproses Sheet Test_Printing pada baris ke-{rowIndex} - {ex.Message}");
            }

            try{
                if(listData.Count()>0){
                    await printingTestAdapter.DeleteByMonthAndYear(periode);
                    await printingTestAdapter.InsertBulk(listData);                    
                }
            }catch(Exception ex){
                throw new Exception("Gagal menyimpan sheet Test_Printing - " + ex.Message);
            }
        }

#endregion
    
#region LAB DIP
        private async Task UploadLabDip(ExcelWorksheets excel, DateTime periode ,int data){
            var sheet = excel[data];
            var totalRow = sheet.Dimension.Rows;
            var listData = new List<DashboardLabDip>();
            int rowIndex = 0;
            try{
                for(rowIndex = 3; rowIndex <= totalRow; rowIndex ++) {
                    if (sheet.Cells[rowIndex,2].Value != null) {
                        generalHelper.ValidatePeriode(sheet.Cells[rowIndex,2], periode);
                        listData.Add(new DashboardLabDip(
                            generalHelper.GenerateId(sheet.Cells[rowIndex,2],sheet.Cells[rowIndex,1]),
                            Convert.ToInt32(sheet.Cells[rowIndex,1].Value),
                            generalHelper.ConvertDateTime(sheet.Cells[rowIndex,2]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,3]),
                            converterChecker.GenerateValueInt(sheet.Cells[rowIndex,4]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,5]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,6]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,7]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,8]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,9]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,10]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,11]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,12]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,13]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,14]),
                            converterChecker.GenerateValueDate(sheet.Cells[rowIndex,15]),
                            converterChecker.GenerateValueDate(sheet.Cells[rowIndex,16]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,17]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,18]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,19])
                        ));
                    }
                }
            }
            catch(Exception ex){
                throw new Exception($"Gagal memproses Sheet Lab_Dip pada baris ke-{rowIndex} - {ex.Message}");
            }

            try{
                if(listData.Count()>0){
                    await labDipAdapter.DeleteByMonthAndYear(periode);
                    await labDipAdapter.InsertBulk(listData);                    
                }
            }catch(Exception ex){
                throw new Exception("Gagal menyimpan sheet Lab_Dip - " + ex.Message);
            }
        }

#endregion

#region LAB PRINTING
        private async Task UploadLabPrinting(ExcelWorksheets excel, DateTime periode ,int data){
            var sheet = excel[data];
            var totalRow = sheet.Dimension.Rows;
            var listData = new List<DashboardPrintingLab>();
            int rowIndex = 0;
            try{
                for(rowIndex = 3; rowIndex <= totalRow; rowIndex ++) {
                    if (sheet.Cells[rowIndex,2].Value != null) {
                        generalHelper.ValidatePeriode(sheet.Cells[rowIndex,2], periode);
                        listData.Add(new DashboardPrintingLab(
                            generalHelper.GenerateId(sheet.Cells[rowIndex,2],sheet.Cells[rowIndex,1]),
                            Convert.ToInt32(sheet.Cells[rowIndex,1].Value),
                            generalHelper.ConvertDateTime(sheet.Cells[rowIndex,2]),
                            Convert.ToInt32(sheet.Cells[rowIndex,3].Value),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,4]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,5]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,6]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,7]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,8]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,9]),
                            generalHelper.ConvertNullDateTime(sheet.Cells[rowIndex,10]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,11]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,12]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,13]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,14]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,15]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,16]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,17]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,18]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,19]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,20]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,21]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,32]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,23]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,24]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,25]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,26]),
                            generalHelper.ConvertNullDateTime(sheet.Cells[rowIndex,27]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,28]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,29]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,30])
                        ));
                    }
                }
            }
            catch(Exception ex){
                throw new Exception($"Gagal memproses Sheet Lab_Printing pada baris ke-{rowIndex} - {ex.Message}");
            }

            try{
                if(listData.Count()>0){
                    await printingLabAdapter.DeleteByMonthAndYear(periode);
                    await printingLabAdapter.InsertBulk(listData);                    
                }
            }catch(Exception ex){
                throw new Exception("Gagal menyimpan sheet Lab_Printing - " + ex.Message);
            }
        }

#endregion

#region HASIL TEST RND 
        private async Task UploadRnDTestResult(ExcelWorksheets excel, DateTime periode ,int data){
            var sheet = excel[data];
            var totalRow = sheet.Dimension.Rows;
            var listData = new List<DashboardRnDTestResult>();
            int rowIndex = 0;
            try{
                for(rowIndex = 3; rowIndex <= totalRow; rowIndex ++) {
                    if (sheet.Cells[rowIndex,2].Value != null) {
                        generalHelper.ValidatePeriode(sheet.Cells[rowIndex,2], periode);
                        listData.Add(new DashboardRnDTestResult(
                            generalHelper.GenerateId(sheet.Cells[rowIndex,2],sheet.Cells[rowIndex,1]),
                            Convert.ToInt32(sheet.Cells[rowIndex,1].Value),
                            generalHelper.ConvertDateTime(sheet.Cells[rowIndex,2]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,3]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,4]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,5]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,6]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,7]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,8]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,9]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,10]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,11]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,12]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,13]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,14]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,15])
                        ));
                    }
                }
            }
            catch(Exception ex){
                throw new Exception($"Gagal memproses Sheet Hasil_test_RnD pada baris ke-{rowIndex} - {ex.Message}");
            }

            try{
                if(listData.Count()>0){
                    await rnDTestResultAdapter.DeleteByMonthAndYear(periode);
                    await rnDTestResultAdapter.InsertBulk(listData);                    
                }
            }catch(Exception ex){
                throw new Exception("Gagal menyimpan sheet Hasil_test_RnD - " + ex.Message);
            }
        }

#endregion
    }
}