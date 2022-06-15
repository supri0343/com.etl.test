using OfficeOpenXml;
using Microsoft.Extensions.Logging;
using Com.Danliris.ETL.Service.ExcelModels.DashboardYarnDyeing;
using System.Collections.Generic;
using System;
using Com.Danliris.ETL.Service.DBAdapters.YarnDyeingAdapters;
using System.Linq;
using Com.Danliris.ETL.Service.Tools;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Com.Danliris.ETL.Service.Services.Interfaces;

namespace Com.Danliris.ETL.Service.Services.Class
{
    public class UploadExcelYarnDyeingService : IUploadExcelYarnDyeingService
    {
        static string connectionString = Environment.GetEnvironmentVariable("ConnectionStrings:SQLConnectionString", EnvironmentVariableTarget.Process);  
        IDyeYarnRequestAdapter yarnRequestAdapter;    
        IMiniloomRequestAdapter miniloomRequestAdapter;
        IYarnOrderRequestAdapter yarnOrderRequestAdapter;
        IFabricYDOrderRequestAdapter fabricYDOrderRequestAdapter;
        ConverterChecker converterChecker = new ConverterChecker();
        GeneralHelper generalHelper = new GeneralHelper();

        public UploadExcelYarnDyeingService(IServiceProvider serviceProvider)
        {
            yarnRequestAdapter = serviceProvider.GetService<IDyeYarnRequestAdapter>();
            miniloomRequestAdapter = serviceProvider.GetService<IMiniloomRequestAdapter>();
            yarnOrderRequestAdapter = serviceProvider.GetService<IYarnOrderRequestAdapter>();
            fabricYDOrderRequestAdapter = serviceProvider.GetService<IFabricYDOrderRequestAdapter>();
        }
        public async Task Upload(ExcelWorksheets excel, DateTime periode) {
            
            try{
                var data=0;
                foreach (var item in excel)
                {
                    switch(item.Name.ToUpper().Trim())
                    {
                        case "PERMINTAAN_BENANG_CELUP":
                            await UploadDyeYarnRequest(excel, periode, data);
                            break;
                        case "PERMINTAAN_MINILOOM":
                            await UploadMiniloomRequest(excel, periode, data);
                            break;
                        case "PERMINTAAN_ORDER_BENANG":
                            await UploadYarnOrderRequest(excel, periode, data);
                            break;
                        case "PERMINTAAN_ORDER_FABRIC_YD":
                            await UploadFabricYDOrderRequest(excel, periode, data);
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

#region DYE YARN REQUEST
        private async Task UploadDyeYarnRequest(ExcelWorksheets excel, DateTime periode, int data){
            var sheet = excel[data];
            var totalRow = sheet.Dimension.Rows;
            var listData = new List<DashboardDyeYarnRequest>();
            int rowIndex = 0;
            try{
                for(rowIndex = 6; rowIndex <= totalRow; rowIndex ++) {
                    if (sheet.Cells[rowIndex,2].Value != null) {
                        listData.Add(new DashboardDyeYarnRequest(
                            generalHelper.GenerateId(sheet.Cells[rowIndex,9],sheet.Cells[rowIndex,2]),
                            Convert.ToInt32(sheet.Cells[rowIndex,2].Value),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,3]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,4]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,5]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,6]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,7]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,8]),
                            converterChecker.GenerateValueDate(sheet.Cells[rowIndex,9]),
                            converterChecker.GenerateValueDate(sheet.Cells[rowIndex,10]),
                            converterChecker.GenerateValueDate(sheet.Cells[rowIndex,11]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,12]),
                            converterChecker.GenerateValueDate(sheet.Cells[rowIndex,13]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,14]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,15]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,16])
                        ));
                    }
                }
            }
            catch(Exception ex){
                throw new Exception($"Gagal memproses Sheet Permintaan_Benang_Celup pada baris ke-{rowIndex} - {ex.Message}");
            }

            try{
                if(listData.Count()>0){
                    await yarnRequestAdapter.DeleteByMonthAndYear(periode);
                    
                    var listOfSPNO = listData.Select(x => x.SPNo).ToList();
                    var existingDataBySPNo = await yarnRequestAdapter.GetBySPNo(listOfSPNO);
                    if(existingDataBySPNo.Count()>0){
                        foreach (var item in existingDataBySPNo)
                        {
                            var removeItem = listData.Single(x => x.SPNo == item.SPNo);
                            listData.Remove(removeItem);
                            await yarnRequestAdapter.Update(item);   
                        }                        
                    }

                    await yarnRequestAdapter.InsertBulk(listData);
                    // var year = Convert.ToInt32(listData[0].OrderDate?.ToString("yyyy"));
                    // var listOfSPNO = listData.Select(x => x.SPNo).ToList();
                    
                    // var existingDataByYear = await yarnRequestAdapter.GetByYear(year);
                    // if(existingDataByYear.Count()>0)
                    //     await Delete(existingDataByYear);

                    // var existingDataBySPNo = await yarnRequestAdapter.GetBySPNo(listOfSPNO);
                    // if(existingDataBySPNo.Count()>0){
                    //     foreach (var item in existingDataBySPNo)
                    //     {
                    //         var removeItem = listData.Single(x => x.SPNo == item.SPNo);
                    //         listData.Remove(removeItem);
                    //         await yarnRequestAdapter.Update(item);   
                    //     }                        
                    // }
                    
                    // await Save(listData);
                }
            }catch(Exception ex){
                throw new Exception("Gagal menyimpan Sheet Permintaan_Benang_Celup - " + ex.Message);
            }
        }
#endregion

#region MINILOOM REQUEST
        private async Task UploadMiniloomRequest(ExcelWorksheets excel, DateTime periode, int data){
            var sheet = excel[data];
            var totalRow = sheet.Dimension.Rows;
            var listData = new List<DashboardMiniloomRequest>();
            int rowIndex = 0;
            try{
                for(rowIndex = 5; rowIndex <= totalRow; rowIndex ++) {
                    if (sheet.Cells[rowIndex,2].Value != null) {
                        listData.Add(new DashboardMiniloomRequest(
                            generalHelper.GenerateId(sheet.Cells[rowIndex,10],sheet.Cells[rowIndex,2]),
                            Convert.ToInt32(sheet.Cells[rowIndex,2].Value),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,3]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,4]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,5]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,6]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,7]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,8]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,9]),
                            converterChecker.GenerateValueDate(sheet.Cells[rowIndex,10]),
                            converterChecker.GenerateValueDate(sheet.Cells[rowIndex,11]),
                            converterChecker.GenerateValueDate(sheet.Cells[rowIndex,12]),
                            converterChecker.GenerateValueDate(sheet.Cells[rowIndex,13]),
                            converterChecker.GenerateValueDate(sheet.Cells[rowIndex,14]),
                            converterChecker.GenerateValueDate(sheet.Cells[rowIndex,15]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,16]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,17]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,18]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,19]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,20])
                        ));
                    }
                }
            }
            catch(Exception ex){
                throw new Exception($"Gagal memproses Sheet Permintaan_Miniloom pada baris ke-{rowIndex} - {ex.Message}");
            }

            try{
                if(listData.Count()>0){
                    await miniloomRequestAdapter.DeleteByMonthAndYear(periode);
                    
                    var listOfSPNO = listData.Select(x => x.SPNo).ToList();
                    var existingDataBySPNo = await miniloomRequestAdapter.GetBySPNo(listOfSPNO);
                    if(existingDataBySPNo.Count()>0){
                        foreach (var item in existingDataBySPNo)
                        {
                            var removeItem = listData.Single(x => x.SPNo == item.SPNo);
                            listData.Remove(removeItem);
                            await miniloomRequestAdapter.Update(item);   
                        }                        
                    }

                    await miniloomRequestAdapter.InsertBulk(listData);
                    // var year = Convert.ToInt32(listData[0].OrderDate?.ToString("yyyy"));
                    // var listOfSPNO = listData.Select(x => x.SPNo).ToList();
                    
                    // var existingDataByYear = await miniloomRequestAdapter.GetByYear(year);
                    // if(existingDataByYear.Count()>0)
                    //     await Delete(existingDataByYear);

                    // var existingDataBySPNo = await miniloomRequestAdapter.GetBySPNo(listOfSPNO);
                    // if(existingDataBySPNo.Count()>0){
                    //     foreach (var item in existingDataBySPNo)
                    //     {
                    //         var removeItem = listData.Single(x => x.SPNo == item.SPNo);
                    //         listData.Remove(removeItem);
                    //         await miniloomRequestAdapter.Update(item);   
                    //     }                        
                    // }
                    
                    // await Save(listData);
                }
            }catch(Exception ex){
                throw new Exception("Gagal menyimpan Sheet Permintaan_Miniloom - " + ex.Message);
            }
        }
#endregion

#region YARN ORDER REQUEST
        private async Task UploadYarnOrderRequest(ExcelWorksheets excel, DateTime periode, int data){
            var sheet = excel[data];
            var totalRow = sheet.Dimension.Rows;
            var listData = new List<DashboardYarnOrderRequest>();
            int rowIndex = 0;
            try{
                for(rowIndex = 5; rowIndex <= totalRow; rowIndex ++) {
                    if (sheet.Cells[rowIndex,2].Value != null) {
                        listData.Add(new DashboardYarnOrderRequest(
                            generalHelper.GenerateId(sheet.Cells[rowIndex,10],sheet.Cells[rowIndex,2]),
                            Convert.ToInt32(sheet.Cells[rowIndex,2].Value),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,3]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,4]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,5]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,6]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,7]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,8]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,9]),
                            converterChecker.GenerateValueDate(sheet.Cells[rowIndex,10]),
                            converterChecker.GenerateValueDate(sheet.Cells[rowIndex,11]),
                            converterChecker.GenerateValueDate(sheet.Cells[rowIndex,12]),
                            converterChecker.GenerateValueDate(sheet.Cells[rowIndex,13]),
                            converterChecker.GenerateValueDate(sheet.Cells[rowIndex,14]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,15]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,16])
                        ));
                    }
                }
            }
            catch(Exception ex){
                throw new Exception($"Gagal memproses Sheet Permintaan_Order_Benang pada baris ke-{rowIndex} - {ex.Message}");
            }

            try{
                if(listData.Count()>0){
                    await yarnOrderRequestAdapter.DeleteByMonthAndYear(periode);
                    
                    var listOfSPNO = listData.Select(x => x.SPNo).ToList();
                    var existingDataBySPNo = await yarnOrderRequestAdapter.GetBySPNo(listOfSPNO);
                    if(existingDataBySPNo.Count()>0){
                        foreach (var item in existingDataBySPNo)
                        {
                            var removeItem = listData.Single(x => x.SPNo == item.SPNo);
                            listData.Remove(removeItem);
                            await yarnOrderRequestAdapter.Update(item);   
                        }                        
                    }

                    await yarnOrderRequestAdapter.InsertBulk(listData);
                    // var year = Convert.ToInt32(listData[0].OrderDate?.ToString("yyyy"));
                    // var listOfSPNO = listData.Select(x => x.SPNo).ToList();
                    
                    // var existingDataByYear = await yarnOrderRequestAdapter.GetByYear(year);
                    // if(existingDataByYear.Count()>0)
                    //     await Delete(existingDataByYear);

                    // var existingDataBySPNo = await yarnOrderRequestAdapter.GetBySPNo(listOfSPNO);
                    // if(existingDataBySPNo.Count()>0){
                    //     foreach (var item in existingDataBySPNo)
                    //     {
                    //         var removeItem = listData.Single(x => x.SPNo == item.SPNo);
                    //         listData.Remove(removeItem);
                    //         await yarnOrderRequestAdapter.Update(item);   
                    //     }                        
                    // }
                    
                    // await Save(listData);
                }
            }catch(Exception ex){
                throw new Exception("Gagal menyimpan Sheet Permintaan_Order_Benang - " + ex.Message);
            }
        }
#endregion

#region FABRIC YD ORDER REQUEST
        private async Task UploadFabricYDOrderRequest(ExcelWorksheets excel, DateTime periode, int data){
            var sheet = excel[data];
            var totalRow = sheet.Dimension.Rows;
            var listData = new List<DashboardFabricYDOrderRequest>();
            int rowIndex = 0;
            try{
                for(rowIndex = 6; rowIndex <= totalRow; rowIndex ++) {
                    if (sheet.Cells[rowIndex,2].Value != null) {
                        listData.Add(new DashboardFabricYDOrderRequest(
                            generalHelper.GenerateId(sheet.Cells[rowIndex,12],sheet.Cells[rowIndex,2]),
                            Convert.ToInt32(sheet.Cells[rowIndex,2].Value),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,3]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,4]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,5]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,6]),
                            converterChecker.GenerateValueInt(sheet.Cells[rowIndex,7]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,8]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,9]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,10]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,11]),
                            converterChecker.GenerateValueDate(sheet.Cells[rowIndex,12]),
                            converterChecker.GenerateValueDate(sheet.Cells[rowIndex,13]),
                            converterChecker.GenerateValueDate(sheet.Cells[rowIndex,14]),
                            converterChecker.GenerateValueDate(sheet.Cells[rowIndex,15]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,16]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,17]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,18]),
                            converterChecker.GenerateValueDate(sheet.Cells[rowIndex,19]),
                            converterChecker.GenerateValueDate(sheet.Cells[rowIndex,20]),
                            converterChecker.GenerateValueDate(sheet.Cells[rowIndex,21]),
                            converterChecker.GenerateValueDate(sheet.Cells[rowIndex,22]),
                            converterChecker.GenerateValueDate(sheet.Cells[rowIndex,23]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,24]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,25]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,26]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,27])
                        ));
                    }
                }
            }
            catch(Exception ex){
                throw new Exception($"Gagal memproses Sheet Permintaan_Order_Fabric_YD pada baris ke-{rowIndex} - {ex.Message}");
            }

            try{
                if(listData.Count()>0){
                    await fabricYDOrderRequestAdapter.DeleteByMonthAndYear(periode);
                    
                    var listOfSPNO = listData.Select(x => x.SPNo).ToList();
                    var existingDataBySPNo = await fabricYDOrderRequestAdapter.GetBySPNo(listOfSPNO);
                    if(existingDataBySPNo.Count()>0){
                        foreach (var item in existingDataBySPNo)
                        {
                            var removeItem = listData.Single(x => x.SPNo == item.SPNo);
                            listData.Remove(removeItem);
                            await fabricYDOrderRequestAdapter.Update(item);   
                        }                        
                    }

                    await fabricYDOrderRequestAdapter.InsertBulk(listData);
                    // var year = Convert.ToInt32(listData[0].OrderDate?.ToString("yyyy"));
                    // var listOfSPNO = listData.Select(x => x.SPNo).ToList();
                    
                    // var existingDataByYear = await fabricYDOrderRequestAdapter.GetByYear(year);
                    // if(existingDataByYear.Count()>0)
                    //     await Delete(existingDataByYear);

                    // var existingDataBySPNo = await fabricYDOrderRequestAdapter.GetBySPNo(listOfSPNO);
                    // if(existingDataBySPNo.Count()>0){
                    //     foreach (var item in existingDataBySPNo)
                    //     {
                    //         var removeItem = listData.Single(x => x.SPNo == item.SPNo);
                    //         listData.Remove(removeItem);
                    //         await fabricYDOrderRequestAdapter.Update(item);   
                    //     }                        
                    // }
                    
                    // await Save(listData);
                }
            }catch(Exception ex){
                throw new Exception("Gagal menyimpan Sheet Permintaan_Order_Fabric_YD - " + ex.Message);
            }
        }
#endregion
    }
}