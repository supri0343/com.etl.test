using OfficeOpenXml;
using Microsoft.Extensions.Logging;
using Com.Danliris.ETL.Service.ExcelModels.KoranQCLine;
using System.Collections.Generic;
using System;
using Com.Danliris.ETL.Service.Tools;
using Com.Danliris.ETL.Service.DBAdapters.KoranQCLineAdapters;
using System.Linq;
using System.Threading.Tasks;
using Com.Danliris.ETL.Service.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Com.Danliris.ETL.Service.Services
{
    public class UploadExcelAreaQCService : IUploadExcelAreaQCService
    {
        static string connectionString = Environment.GetEnvironmentVariable("ConnectionStrings:SQLConnectionString", EnvironmentVariableTarget.Process);
        IDigitalPrintAdapter digitalPrintAdapter;
        IDyeingAdapter dyeingAdapter;
        IPretreatmentAdapter pretreatmentAdapter;
        IPrintingAdapter printingAdapter;
        ConverterChecker converter = new ConverterChecker();
        GeneralHelper generalHelper = new GeneralHelper();
        DateTime Periode = new DateTime();
        
        public UploadExcelAreaQCService(IServiceProvider provider)
        {
            digitalPrintAdapter = provider.GetService<IDigitalPrintAdapter>();            
            dyeingAdapter = provider.GetService<IDyeingAdapter>();
            pretreatmentAdapter = provider.GetService<IPretreatmentAdapter>();
            printingAdapter = provider.GetService<IPrintingAdapter>();
        }
        public async Task Upload(ExcelWorksheets sheets, DateTime periode) {
            try{
                this.Periode = periode;
                foreach (var item in sheets)
                {
                    switch(item.Name.Trim())
                    {
                        case "Pretreatment":
                            await UploadSheetPretreatment(sheets[item.Name]);
                            break;
                        case "Printing":
                            await UploadSheetPrinting(sheets[item.Name]);
                            break;
                        case "Dyeing":
                            await UploadSheetDyeing(sheets[item.Name]);
                            break;
                        case "Digital Print":
                            await UploadSheetDigitalPrint(sheets[item.Name]);
                            break;
                        default:
                            throw new Exception(item.Name + " tidak valid");                    
                    }
                }
            }catch(Exception ex){
                throw ex;
            }
        }

        public async Task UploadSheetPretreatment(ExcelWorksheet sheet) {
            var startRow = 3;
            var startCol = 1;
            var totalRows = sheet.Dimension.Rows;
            var totalColumns = sheet.Dimension.Columns;

            var listData = new List<Pretreatment>();
            int rowIndex = 0;
            try {
                for(rowIndex = startRow; rowIndex <= totalRows; rowIndex ++) {
                    if (sheet.Cells[rowIndex,startCol].Value != null)
                    {
                        generalHelper.ValidatePeriode(sheet.Cells[rowIndex,2], Periode);
                        listData.Add(new Pretreatment(
                            generalHelper.GenerateId(sheet.Cells[rowIndex,2],sheet.Cells[rowIndex,1]),
                            Convert.ToInt32(sheet.Cells[rowIndex,startCol].Value.ToString()), // No
                            generalHelper.ConvertDateTime(sheet.Cells[rowIndex,startCol + 1]), // Date
                            converter.GenerateValueString(sheet.Cells[rowIndex,startCol+2]), // Nama_Operator
                            converter.GenerateValueChar(sheet.Cells[rowIndex,startCol+3]), // Group_IM
                            converter.GenerateValueString(sheet.Cells[rowIndex,startCol+4]), // Shift_IM
                            converter.GenerateValueString(sheet.Cells[rowIndex,startCol+5]), // Mesin
                            converter.GenerateValueString(sheet.Cells[rowIndex,startCol+6]), // Group_Mesin
                            converter.GenerateValueString(sheet.Cells[rowIndex,startCol+7]), // No._Order
                            converter.GenerateValueString(sheet.Cells[rowIndex,startCol+8]), // Material
                            converter.GenerateValueString(sheet.Cells[rowIndex,startCol+9]), // Warna/Motif
                            converter.GenerateValueString(sheet.Cells[rowIndex,startCol+10]), // Buyer
                            converter.GeneratePureString(sheet.Cells[rowIndex,startCol+11]), // No._Kereta
                            converter.GeneratePureDouble(sheet.Cells[rowIndex,startCol+12]), // Total
                            converter.GeneratePureDouble(sheet.Cells[rowIndex,startCol+13]), // Grade_A
                            converter.GeneratePureDouble(sheet.Cells[rowIndex,startCol+14]), // Grade_B
                            converter.GeneratePureDouble(sheet.Cells[rowIndex,startCol+15]), // Grade_C
                            converter.GeneratePureDouble(sheet.Cells[rowIndex,startCol+16]), // Grade_BS
                            converter.GeneratePureString(sheet.Cells[rowIndex,startCol+17]), // Keterangan_1
                            converter.GeneratePureString(sheet.Cells[rowIndex,startCol+18]), // Keterangan_2
                            converter.GeneratePureString(sheet.Cells[rowIndex,startCol+19]) // Keterangan_3
                        ));
                    }
                }
            }
            catch(Exception ex){
                throw new Exception($"Gagal memproses Sheet Pretreatment pada baris ke-{rowIndex} - {ex.Message}");
            }

            try{                   
                if (listData.Count() > 0) {                    
                    await pretreatmentAdapter.DeleteByMonthAndYear(Periode);
                    await pretreatmentAdapter.InsertBulk(listData);
                }
            } catch (Exception ex) {
                throw new Exception("Gagal menyimpan Sheet Pretreatment - " + ex.Message);
            }
        }

        public async Task UploadSheetDigitalPrint(ExcelWorksheet sheet) {
            var startRow = 3;
            var startCol = 1;
            var totalRows = sheet.Dimension.Rows;
            var totalColumns = sheet.Dimension.Columns;

            var listData = new List<DigitalPrint>();
            int rowIndex = 0;
            try {
                for(rowIndex = startRow; rowIndex <= totalRows; rowIndex ++) {
                    if (sheet.Cells[rowIndex,startCol].Value != null)
                    {
                        generalHelper.ValidatePeriode(sheet.Cells[rowIndex,2], Periode);
                        listData.Add(new DigitalPrint(
                            generalHelper.GenerateId(sheet.Cells[rowIndex,startCol+1], sheet.Cells[rowIndex,startCol]),
                            Convert.ToInt32(sheet.Cells[rowIndex,startCol].Value.ToString()), // No
                            generalHelper.ConvertDateTime(sheet.Cells[rowIndex,startCol+1]),  //Date
                            converter.GenerateValueString(sheet.Cells[rowIndex,startCol+2]), // Nama_Operator
                            converter.GenerateValueChar(sheet.Cells[rowIndex,startCol+3]), // Group_IM
                            converter.GenerateValueString(sheet.Cells[rowIndex,startCol+4]), // Shift_IM
                            converter.GenerateValueString(sheet.Cells[rowIndex,startCol+5]), // No._Order
                            converter.GenerateValueString(sheet.Cells[rowIndex,startCol+6]), // Material
                            converter.GenerateValueString(sheet.Cells[rowIndex,startCol+7]), // Warna/Motif
                            converter.GenerateValueString(sheet.Cells[rowIndex,startCol+8]), // Buyer
                            converter.GeneratePureString(sheet.Cells[rowIndex,startCol+9]), // No._Kereta
                            converter.GeneratePureDouble(sheet.Cells[rowIndex,startCol+10]), // Total
                            converter.GeneratePureDouble(sheet.Cells[rowIndex,startCol+11]), // Grade_A
                            converter.GeneratePureDouble(sheet.Cells[rowIndex,startCol+12]), // Grade_B
                            converter.GeneratePureDouble(sheet.Cells[rowIndex,startCol+13]), // Grade_C
                            converter.GeneratePureDouble(sheet.Cells[rowIndex,startCol+14]), // Grade_BS
                            converter.GeneratePureString(sheet.Cells[rowIndex,startCol+15]), // Keterangan_1
                            converter.GeneratePureString(sheet.Cells[rowIndex,startCol+16]), // Keterangan_2
                            converter.GeneratePureString(sheet.Cells[rowIndex,startCol+17]) // Keterangan_3
                        ));
                    }
                }
            }
            catch(Exception ex){
                throw new Exception($"Gagal memproses Sheet Digital Print pada baris ke-{rowIndex} - {ex.Message}");
            }

            try{                   
                if (listData.Count() > 0) {
                    await digitalPrintAdapter.DeleteByMonthAndYear(Periode);
                    await digitalPrintAdapter.InsertBulk(listData);
                }
            } catch (Exception ex) {
                throw new Exception("Gagal menyimpan Sheet Digital Print - " + ex.Message);
            }
        }

        public async Task UploadSheetDyeing(ExcelWorksheet sheet) {
            var startRow = 3;
            var startCol = 1;
            var totalRows = sheet.Dimension.Rows;
            var totalColumns = sheet.Dimension.Columns;

            var listData = new List<Dyeing>();
            int rowIndex = 0;
            try {
                for(rowIndex = startRow; rowIndex <= totalRows; rowIndex ++) {
                    if (sheet.Cells[rowIndex,startCol].Value != null)
                    {
                        generalHelper.ValidatePeriode(sheet.Cells[rowIndex,2], Periode);
                        listData.Add(new Dyeing(
                            generalHelper.GenerateId(sheet.Cells[rowIndex,startCol+1], sheet.Cells[rowIndex,startCol]),
                            Convert.ToInt32(sheet.Cells[rowIndex,startCol].Value.ToString()), // No
                            generalHelper.ConvertDateTime(sheet.Cells[rowIndex,startCol+1]),  //Date
                            converter.GenerateValueString(sheet.Cells[rowIndex,startCol+2]), // Nama_Operator
                            converter.GenerateValueChar(sheet.Cells[rowIndex,startCol+3]), // Group_IM
                            converter.GenerateValueString(sheet.Cells[rowIndex,startCol+4]), // Shift_IM
                            converter.GenerateValueString(sheet.Cells[rowIndex,startCol+5]), // Mesin
                            converter.GenerateValueString(sheet.Cells[rowIndex,startCol+6]), // Group_Mesin
                            converter.GenerateValueString(sheet.Cells[rowIndex,startCol+7]), // No._Order
                            converter.GenerateValueString(sheet.Cells[rowIndex,startCol+8]), // Material
                            converter.GenerateValueString(sheet.Cells[rowIndex,startCol+9]), // Warna/Motif
                            converter.GenerateValueString(sheet.Cells[rowIndex,startCol+10]), // Buyer
                            converter.GeneratePureString(sheet.Cells[rowIndex,startCol+11]), // No._Kereta
                            converter.GeneratePureDouble(sheet.Cells[rowIndex,startCol+12]), // Total
                            converter.GeneratePureDouble(sheet.Cells[rowIndex,startCol+13]), // Grade_A
                            converter.GeneratePureDouble(sheet.Cells[rowIndex,startCol+14]), // Grade_B
                            converter.GeneratePureDouble(sheet.Cells[rowIndex,startCol+15]), // Grade_C
                            converter.GeneratePureDouble(sheet.Cells[rowIndex,startCol+16]), // Grade_BS
                            converter.GeneratePureString(sheet.Cells[rowIndex,startCol+17]), // Keterangan_1
                            converter.GeneratePureString(sheet.Cells[rowIndex,startCol+18]), // Keterangan_2
                            converter.GeneratePureString(sheet.Cells[rowIndex,startCol+19]) // Keterangan_3
                        ));
                    }
                }
            }
            catch(Exception ex){
                throw new Exception($"Gagal memproses Sheet Dyeing pada baris ke-{rowIndex} - {ex.Message}");
            }

            try{                   
                if (listData.Count() > 0) {
                    await dyeingAdapter.DeleteByMonthAndYear(Periode);
                    await dyeingAdapter.InsertBulk(listData);
                }
            } catch (Exception ex) {
                throw new Exception("Gagal menyimpan Sheet Dyeing - " + ex.Message);
            }
        }

        public async Task UploadSheetPrinting(ExcelWorksheet sheet) {            
            var startRow = 3;
            var startCol = 1;
            var totalRows = sheet.Dimension.Rows;
            var totalColumns = sheet.Dimension.Columns;

            var listData = new List<Printing>();
            int rowIndex = 0;
            try {
                for(rowIndex = startRow; rowIndex <= totalRows; rowIndex ++) {
                    if (sheet.Cells[rowIndex,startCol].Value != null)
                    {
                        generalHelper.ValidatePeriode(sheet.Cells[rowIndex,2], Periode);
                        listData.Add(new Printing(
                            generalHelper.GenerateId(sheet.Cells[rowIndex,startCol+1], sheet.Cells[rowIndex,startCol]),
                            Convert.ToInt32(sheet.Cells[rowIndex,startCol].Value.ToString()), // No
                            generalHelper.ConvertDateTime(sheet.Cells[rowIndex,startCol+1]),  //Date
                            converter.GenerateValueString(sheet.Cells[rowIndex,startCol+2]), // Nama_Operator
                            converter.GenerateValueChar(sheet.Cells[rowIndex,startCol+3]), // Group_IM
                            converter.GenerateValueString(sheet.Cells[rowIndex,startCol+4]), // Shift_IM
                            converter.GenerateValueString(sheet.Cells[rowIndex,startCol+5]), // Mesin
                            converter.GenerateValueString(sheet.Cells[rowIndex,startCol+6]), // Group_Mesin
                            converter.GenerateValueString(sheet.Cells[rowIndex,startCol+7]), // No._Order
                            converter.GenerateValueString(sheet.Cells[rowIndex,startCol+8]), // Material
                            converter.GenerateValueString(sheet.Cells[rowIndex,startCol+9]), // Warna/Motif
                            converter.GenerateValueString(sheet.Cells[rowIndex,startCol+10]), // Buyer
                            converter.GeneratePureString(sheet.Cells[rowIndex,startCol+11]), // No._Kereta
                            converter.GeneratePureDouble(sheet.Cells[rowIndex,startCol+12]), // Total
                            converter.GeneratePureDouble(sheet.Cells[rowIndex,startCol+13]), // Grade_A
                            converter.GeneratePureDouble(sheet.Cells[rowIndex,startCol+14]), // Grade_B
                            converter.GeneratePureDouble(sheet.Cells[rowIndex,startCol+15]), // Grade_C
                            converter.GeneratePureDouble(sheet.Cells[rowIndex,startCol+16]), // Grade_BS
                            converter.GeneratePureString(sheet.Cells[rowIndex,startCol+17]), // Keterangan_1
                            converter.GeneratePureString(sheet.Cells[rowIndex,startCol+18]), // Keterangan_2
                            converter.GeneratePureString(sheet.Cells[rowIndex,startCol+19]) // Keterangan_3
                        ));
                    }
                }   
            }
            catch(Exception ex){
                throw new Exception($"Gagal memproses Sheet Printing pada baris ke-{rowIndex} - {ex.Message}");
            }

            try{                
                if (listData.Count() > 0) {
                    await printingAdapter.DeleteByMonthAndYear(Periode);
                    await printingAdapter.InsertBulk(listData);
                }
            } catch (Exception ex) {
                throw new Exception("Gagal menyimpan Sheet Printing - " + ex.Message);
            }
        }
    }
}