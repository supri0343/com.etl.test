using Com.Danliris.ETL.Service.DBAdapters.PretreatmentAdapters;
using Com.Danliris.ETL.Service.Models.DashboardPretreatmentModels;
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

namespace Com.Danliris.ETL.Service.Services
{
    public class UploadExcelPretreatmentService : IUploadExcelPretreatmentService
    {
        static string connectionString = Environment.GetEnvironmentVariable("ConnectionStrings:SQLConnectionString", EnvironmentVariableTarget.Process);
        public IMachineProductionAdapter machineProductionAdapter;
        public IMaterialPreparingAdapter materialPreparingAdapter;
        public IDeliveryBetweenAdapter deliveryBetweenAdapter;
        public IMaterialTroubleAdapter materialTroubleAdapter;
        public IStockMaterialAdapter stockMaterialAdapter;
        public IStockOpnameAdapter stockOpnameAdapter;
        public IReprocessAdapter reprocessAdapter;
        GeneralHelper general = new GeneralHelper();
        ConverterChecker converter = new ConverterChecker();

        public UploadExcelPretreatmentService(IServiceProvider provider)
        {
            machineProductionAdapter = provider.GetService<IMachineProductionAdapter>();
            materialPreparingAdapter = provider.GetService<IMaterialPreparingAdapter>();
            deliveryBetweenAdapter = provider.GetService<IDeliveryBetweenAdapter>();
            materialTroubleAdapter = provider.GetService<IMaterialTroubleAdapter>();
            stockMaterialAdapter = provider.GetService<IStockMaterialAdapter>();
            stockOpnameAdapter = provider.GetService<IStockOpnameAdapter>();
            reprocessAdapter = provider.GetService<IReprocessAdapter>();
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
                        case "produksi_mesin_pretreatment":
                            await UploadPretreatmentMachineProduction(sheet, periode, data);
                            break;
                        case "material_masuk_preparing":
                            await UploadMaterialPreparing(sheet, periode, data);                            
                            break;
                        case "reproses":
                            await UploadReproses(sheet, periode, data);
                            break;
                        case "pengiriman_antar_area":
                            await UploadDeliveryBetweenArea(sheet, periode, data);
                            break;
                        case "stock_opname":
                            await UploadStockOpname(sheet, periode, data);
                            break;
                        case "material_trouble":
                            await UploadMaterialTrouble(sheet, periode, data);
                            break;
                        case "stock_material":
                            await UploadStockMaterial(sheet, periode, data);
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


        #region Machine Production
        public async Task UploadPretreatmentMachineProduction(ExcelWorksheets sheet, DateTime periode, int data)
        {
            var sheet1 = sheet[data];
            var startRow = 3;
            var startCol = 1;

            var totalRows = sheet1.Dimension.Rows;
            var totalCols = sheet1.Dimension.Columns;

            var listData = new List<PretreatmentMachineProductionModel>();
            int rowIndex = 0;    
            try
            {
                for (rowIndex = startRow; rowIndex <= totalRows; rowIndex++){
                    if (sheet1.Cells[rowIndex, startCol].Value != null)
                    {
                        general.ValidatePeriode(sheet1.Cells[rowIndex,2], periode);
                        listData.Add(new PretreatmentMachineProductionModel(
                           general.GenerateId(sheet1.Cells[rowIndex, startCol + 1], sheet1.Cells[rowIndex, 1]), //ID
                           Convert.ToInt32(sheet1.Cells[rowIndex, startCol].Value), //No
                           general.ConvertDateTime(sheet1.Cells[rowIndex, startCol + 1]), //Date
                           converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 2]), //Shift
                           converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 3]), //Group
                           converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 4]), //Machine
                           converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 5]), //OrderNo
                           converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 6]), //Material
                           converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 7]), //ColorCW
                           converter.GenerateValueDouble(sheet1.Cells[rowIndex, startCol + 8]), //LengthIn
                           converter.GenerateValueDouble(sheet1.Cells[rowIndex, startCol + 9]), //LengthOutBQ
                           converter.GenerateValueDouble(sheet1.Cells[rowIndex, startCol + 10]), //LengthOutBS
                           converter.GenerateValueDouble(sheet1.Cells[rowIndex, startCol + 11]), //WidthFabric
                           converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 12]), //TrainNo
                           converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 13]), //ProcessType
                           converter.GenerateValueDouble(sheet1.Cells[rowIndex, startCol + 14]), //Speed
                           converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 15]) //Description
                        ));
                    }
                }
                
            }
            catch(Exception ex){
                throw new Exception($"Gagal memproses Sheet Produksi_Mesin_Treatment pada baris ke-{rowIndex} - {ex.Message}");
            }

            try{
                if (listData.Count() > 0)
            {                    
                    await machineProductionAdapter.DeleteByMonthAndYear(periode);
                    await machineProductionAdapter.InsertBulk(listData);
                }

            }
            catch (Exception ex)
            {

                throw new Exception("Gagal menyimpan sheet Produksi_Mesin_Treatment - " + ex.Message);
            }
        }

        
        
        #endregion

        #region Material Preparing
        public async Task UploadMaterialPreparing(ExcelWorksheets sheet, DateTime periode, int data)    
        {
            var sheet1 = sheet[data];
            var startRow = 3;
            var startCol = 1;

            var totalRows = sheet1.Dimension.Rows;
            var totalCols = sheet1.Dimension.Columns;

            var listData = new List<IncomingMaterialPreparingModel>();
            int rowIndex = 0;
            try
            {
                for (rowIndex = startRow; rowIndex <= totalRows; rowIndex++)
                {
                    if (sheet1.Cells[rowIndex, startCol].Value != null)
                    {
                        general.ValidatePeriode(sheet1.Cells[rowIndex,4], periode);
                        listData.Add(new IncomingMaterialPreparingModel(
                           general.GenerateId(sheet1.Cells[rowIndex, startCol + 3], sheet1.Cells[rowIndex, 1]), //ID
                           Convert.ToInt32(sheet1.Cells[rowIndex, startCol].Value), // No
                           converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 1]), // Activity
                           converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 2]), //DPRFP
                           general.ConvertDateTime(sheet1.Cells[rowIndex, startCol + 3]), //Tanggal
                           converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 4]), //Shift
                           converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 5]), //Group
                           converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 6]), //NoSp
                           converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 7]), //Konstruksi
                           converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 8]), //Warna
                           converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 9]), //TrainNo
                           converter.GenerateValueDouble(sheet1.Cells[rowIndex, startCol + 10]), //NoPieces
                           converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 11]), //Grade
                           converter.GenerateValueDouble(sheet1.Cells[rowIndex, startCol + 12]), //Meter
                           general.ConvertNullDateTime(sheet1.Cells[rowIndex, startCol + 13]), //SealDate
                           general.ConvertNullDateTime(sheet1.Cells[rowIndex, startCol + 14]), //SewingDate
                           general.ConvertNullDateTime(sheet1.Cells[rowIndex, startCol + 15]), //RollDate
                           converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 16]), //Time
                           general.ConvertNullDateTime(sheet1.Cells[rowIndex, startCol + 17]), //PretreatMentOutDate
                           general.ConvertNullDateTime(sheet1.Cells[rowIndex, startCol + 18])// PretreatmentoutTotal
                        ));
                    }
                }
                
            }
            catch(Exception ex){
                throw new Exception($"Gagal memproses Sheet Material_Masuk_Preparing pada baris ke-{rowIndex} - {ex.Message}");
            }

            try{
                if (listData.Count() > 0)
                {                    
                    await materialPreparingAdapter.DeleteByMonthAndYear(periode);
                    await materialPreparingAdapter.InsertBulk(listData);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Gagal menyimpan Sheet Material_Masuk_Preparing - " + ex.Message);
            }
        }

        
        
        #endregion

        #region Reproses
        public async Task UploadReproses(ExcelWorksheets sheet, DateTime periode, int data)
        {
            var sheet1 = sheet[data];
            var startRow = 3;
            var startCol = 1;

            var totalRows = sheet1.Dimension.Rows;
            var totalCols = sheet1.Dimension.Columns;

            var listData = new List<ReprocessModel>();
            int rowIndex = 0;
            try
            {
                for (rowIndex = startRow; rowIndex <= totalRows; rowIndex++)
                {
                    if (sheet1.Cells[rowIndex, startCol].Value != null)
                    {
                        general.ValidatePeriode(sheet1.Cells[rowIndex,2], periode);
                        listData.Add(new ReprocessModel(
                           general.GenerateId(sheet1.Cells[rowIndex, startCol + 1], sheet1.Cells[rowIndex, 1]), //ID
                           Convert.ToInt32(sheet1.Cells[rowIndex, startCol].Value), // No
                           general.ConvertDateTime(sheet1.Cells[rowIndex, startCol + 1]), //Date
                           converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 2]), // Shift
                           converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 3]), //Group
                           converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 4]), //OrderNo
                           converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 5]), //Material
                           converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 6]), //TrainNo
                           converter.GenerateValueDouble(sheet1.Cells[rowIndex, startCol + 7]), //QtyIn
                           converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 8]), //Machine
                           converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 9]), //ProcessType
                           converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 10]), //Reprocess
                           converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 11]) //Problem                          
                        ));
                    }
                }
                
            }
            catch(Exception ex){
                throw new Exception($"Gagal memproses Sheet Reproses pada baris ke-{rowIndex} - {ex.Message}");
            }

            try{
                if (listData.Count() > 0)
                {                    
                    await reprocessAdapter.DeleteByMonthAndYear(periode);
                    await reprocessAdapter.InsertBulk(listData);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Gagal menyimpan Sheet Reproses - " + ex.Message);
            }
        }

        
        
        #endregion

        #region Pengiriman Antar Area
        public async Task UploadDeliveryBetweenArea(ExcelWorksheets sheet, DateTime periode, int data)
        {
            var sheet1 = sheet[data];
            var startRow = 3;
            var startCol = 1;

            var totalRows = sheet1.Dimension.Rows;
            var totalCols = sheet1.Dimension.Columns;

            var listData = new List<DeliveryBetweenAreaModel>();
            int rowIndex = 0;
            try
            {
                for (rowIndex = startRow; rowIndex <= totalRows; rowIndex++)
                {
                    if (sheet1.Cells[rowIndex, startCol].Value != null)
                    {
                        general.ValidatePeriode(sheet1.Cells[rowIndex,2], periode);
                        listData.Add(new DeliveryBetweenAreaModel(
                           general.GenerateId(sheet1.Cells[rowIndex, startCol + 1], sheet1.Cells[rowIndex, 1]), //ID
                           Convert.ToInt32(sheet1.Cells[rowIndex, startCol].Value),               // No
                           general.ConvertDateTime(sheet1.Cells[rowIndex, startCol + 1]),         //Date
                           converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 2]),   // SP
                           converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 3]),   //Konstruksi
                           converter.GenerateValueDouble(sheet1.Cells[rowIndex, startCol + 4]),   //Activity
                           converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 5]),   //Qty
                           converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 6]),   //Kereta
                           converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 7]),   //Grade
                           converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 8]),   //Subcon
                           converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 9]),   //Destination
                           converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 10]),  //Reprocess
                           converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 11]),  //Ket
                           converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 12]),  //Area
                           converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 13]),  //Keterangan
                           converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 14])   //Problem
                        ));
                    }
                }
                
            }
            catch(Exception ex){
                throw new Exception($"Gagal memproses Sheet Pengiriman_Antar_Area pada baris ke-{rowIndex} - {ex.Message}");
            }

            try{
                if (listData.Count() > 0)
                {                    
                    await deliveryBetweenAdapter.DeleteByMonthAndYear(periode);
                    await deliveryBetweenAdapter.InsertBulk(listData);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Gagal menyimpan Sheet Pengiriman_Antar_Area - " + ex.Message);
            }
        }

        
        
        #endregion

        #region Stock_Opname
        public async Task UploadStockOpname(ExcelWorksheets sheet, DateTime periode, int data)
        {
            var sheet1 = sheet[data];
            var startRow = 3;
            var startCol = 1;

            var totalRows = sheet1.Dimension.Rows;
            var totalCols = sheet1.Dimension.Columns;

            var listData = new List<StockOpnameModel>();
            int rowIndex = 0;
            try
            {
                for (rowIndex = startRow; rowIndex <= totalRows; rowIndex++)
                {
                    if (sheet1.Cells[rowIndex, startCol].Value != null)
                    {
                        general.ValidatePeriode(sheet1.Cells[rowIndex,2], periode);
                        listData.Add(new StockOpnameModel(
                           general.GenerateId(sheet1.Cells[rowIndex, startCol + 1], sheet1.Cells[rowIndex, 1]), //ID
                           Convert.ToInt32(sheet1.Cells[rowIndex, startCol].Value),               // No
                           general.ConvertDateTime(sheet1.Cells[rowIndex, startCol + 1]),         //Date
                           converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 2]),   //SPNo
                           converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 3]),   //Material    
                           converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 4]),   //Train
                           converter.GeneratePureDouble(sheet1.Cells[rowIndex, startCol + 5]),   //Length
                           converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 6])   //Description                          
                        ));
                    }
                }
                
            }
            catch(Exception ex){
                throw new Exception($"Gagal memproses Sheet Stock_Opname pada baris ke-{rowIndex} - {ex.Message}");
            }

            try{
                if (listData.Count() > 0)
                {                    
                    await stockOpnameAdapter.DeleteByMonthAndYear(periode);
                    await stockOpnameAdapter.InsertBulk(listData);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Gagal menyimpan Sheet Stock_Opname - " + ex.Message);
            }
        }

        
        
        #endregion

        #region Material Trouble
        public async Task UploadMaterialTrouble(ExcelWorksheets sheet, DateTime periode, int data)
        {
            var sheet1 = sheet[data];
            var startRow = 3;
            var startCol = 1;

            var totalRows = sheet1.Dimension.Rows;
            var totalCols = sheet1.Dimension.Columns;

            var listData = new List<MaterialTroubleModel>();
            int rowIndex = 0;
            try
            {
                for (rowIndex = startRow; rowIndex <= totalRows; rowIndex++)
                {
                    if (sheet1.Cells[rowIndex, startCol].Value != null)
                    {
                        general.ValidatePeriode(sheet1.Cells[rowIndex,2], periode);
                        listData.Add(new MaterialTroubleModel(
                           general.GenerateId(sheet1.Cells[rowIndex, startCol + 1], sheet1.Cells[rowIndex, 1]), //ID
                           Convert.ToInt32(sheet1.Cells[rowIndex, startCol].Value),               // No
                           general.ConvertDateTime(sheet1.Cells[rowIndex, startCol + 1]),         //Date
                           converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 2]),   // OrderNo
                           converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 3]),   //Material
                           converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 4]),   //Buyer
                           converter.GeneratePureDouble(sheet1.Cells[rowIndex, startCol + 5]),   //BQ
                           converter.GeneratePureDouble(sheet1.Cells[rowIndex, startCol + 6]),   //BS                          
                           converter.GeneratePureDouble(sheet1.Cells[rowIndex, startCol + 7]),   // Total
                           converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 8])    //Problem
                        ));
                    }
                }
                
            }
            catch(Exception ex){
                throw new Exception($"Gagal memproses Sheet Material_Trouble pada baris ke-{rowIndex} - {ex.Message}");
            }

            try{
                if (listData.Count() > 0)
                {                    
                    await materialTroubleAdapter.DeleteByMonthAndYear(periode);
                    await materialTroubleAdapter.InsertBulk(listData);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Gagal menyimpan Sheet Material_Trouble - " + ex.Message);
            }
        }

        
        

        #endregion

        #region Stock Material
        public async Task UploadStockMaterial(ExcelWorksheets sheet, DateTime periode, int data)
        {
            var sheet1 = sheet[data];
            var startRow = 3;
            var startCol = 1;

            var totalRows = sheet1.Dimension.Rows;
            var totalCols = sheet1.Dimension.Columns;

            var listData = new List<StockMaterialModel>();
            int rowIndex = 0;
            try
            {
                for (rowIndex = startRow; rowIndex <= totalRows; rowIndex++)
                {
                    if (sheet1.Cells[rowIndex, startCol].Value != null)
                    {
                        general.ValidatePeriode(sheet1.Cells[rowIndex,2], periode);
                        listData.Add(new StockMaterialModel(
                           general.GenerateId(sheet1.Cells[rowIndex, startCol + 1], sheet1.Cells[rowIndex, 1]), //ID
                           Convert.ToInt32(sheet1.Cells[rowIndex, startCol].Value),               // No
                           general.ConvertDateTime(sheet1.Cells[rowIndex, startCol + 1]),         // DateInOut
                           converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 2]),   // Activity
                           converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 3]),   // OrderNo
                           converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 4]),   // Material
                           converter.GenerateValueString(sheet1.Cells[rowIndex, startCol + 5]),   // Train
                           converter.GenerateValueDouble(sheet1.Cells[rowIndex, startCol + 6])   // Quantity                                                     
                        ));
                    }
                }
                
            }
            catch(Exception ex){
                throw new Exception($"Gagal memproses Sheet Stock_Material pada baris ke-{rowIndex} - {ex.Message}");
            }

            try{
                if (listData.Count() > 0)
                {                    
                    await stockMaterialAdapter.DeleteByMonthAndYear(periode);
                    await stockMaterialAdapter.InsertBulk(listData);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Gagal menyimpan Sheet Stock_Material - " + ex.Message);
            }
        }

        
        
        #endregion
    }
}
