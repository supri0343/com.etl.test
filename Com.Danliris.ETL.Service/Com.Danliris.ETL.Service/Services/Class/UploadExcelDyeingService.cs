using Microsoft.Extensions.DependencyInjection;
using OfficeOpenXml;
using Microsoft.Extensions.Logging;
using Com.Danliris.ETL.Service.ExcelModels.DashboardDyeingModels;
using System.Collections.Generic;
using System;
using Com.Danliris.ETL.Service.DBAdapters.DyeingAdapters;
using System.Linq;
using Com.Danliris.ETL.Service.Tools;
using System.Threading.Tasks;
using Com.Danliris.ETL.Service.Services.Interfaces;

namespace Com.Danliris.ETL.Service.Services.Class
{
    public class UploadExcelDyeingService : IUploadExcelDyeingService
    {    
        static string connectionString = Environment.GetEnvironmentVariable("ConnectionStrings:SQLConnectionString", EnvironmentVariableTarget.Process);
        //DyeingMachineAdapter dyeingMachineAdapter = new DyeingMachineAdapter(connectionString);   
        public IDyeingMachineAdapter dyeingMachineAdapter;
        public IHandoverAdapter handoverAdapter;
        public IWipMaterialAdapter wipMaterialAdapter;
        public ICkDyeingAdapter ckDyeingAdapter;
        public ILpgMachineUsageAdapter lpgMachineUsageAdapter;
        ConverterChecker converterChecker = new ConverterChecker();
        GeneralHelper generalHelper = new GeneralHelper();
        
        public UploadExcelDyeingService(IServiceProvider provider)
        {
            dyeingMachineAdapter = provider.GetService<IDyeingMachineAdapter>();
            handoverAdapter = provider.GetService<IHandoverAdapter>();
            wipMaterialAdapter = provider.GetService<IWipMaterialAdapter>();
            ckDyeingAdapter = provider.GetService<ICkDyeingAdapter>();
            lpgMachineUsageAdapter = provider.GetService<ILpgMachineUsageAdapter>();
        }

        public async Task Upload(ExcelWorksheets excel, DateTime periode) {
            
            try{
                var data=0;
                foreach (var item in excel)
                {
                    switch(item.Name.ToUpper().Trim())
                    {
                        case "MESIN_DYEING":
                            await UploadDyeingMachine(excel, periode, data);
                            break;
                        case "SERAH_TERIMA_AREA":
                            await UploadHandover(excel, periode, data);
                            break;
                        case "WIP_MATERIAL":
                            await UploadWipMaterial(excel, periode, data);
                            break;
                        case "CK_DYEING":
                            await UploadCkDyeing(excel, periode, data);
                            break;
                        case "PENGGUNAAN_LPG_MESIN":
                            await UploadLpgMachineUsage(excel, periode, data);
                            break;
                        default:
                            throw new Exception(item.Name.Trim() + " tidak valid");                        
                    }
                    data++;
                }
            }catch(Exception ex){
                
                throw ex;
            }
        }

#region DYEING MACHINE
        private async Task UploadDyeingMachine(ExcelWorksheets excel, DateTime periode, int data){
            var sheet = excel[data];
            var totalRow = sheet.Dimension.Rows;
            var listData = new List<DashboardDyeingMachine>();
            int rowIndex = 0;
            try{                
                for(rowIndex = 3; rowIndex <= totalRow; rowIndex ++) {
                    if (sheet.Cells[rowIndex,1].Value != null) {
                        generalHelper.ValidatePeriode(sheet.Cells[rowIndex,3], periode);
                        listData.Add(new DashboardDyeingMachine(
                            generalHelper.GenerateId(sheet.Cells[rowIndex,3],sheet.Cells[rowIndex,1]),
                            Convert.ToInt32(sheet.Cells[rowIndex,1].Value),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,2]),
                            generalHelper.ConvertDateTime(sheet.Cells[rowIndex,3]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,4]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,5]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,6]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,7]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,8]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,9]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,10]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,11]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,12]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,13]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,14])
                        ));
                    }
                }
            }
            catch(Exception ex){
                throw new Exception($"Gagal memproses Sheet DYEING_MACHINE pada baris ke-{rowIndex} - {ex.Message}");
            }

            try{
                if(listData.Count()>0){
                    await dyeingMachineAdapter.DeleteByMonthAndYear(periode);
                    await dyeingMachineAdapter.InsertBulk(listData);

                    // var month = Convert.ToInt32(listData[0].Date.ToString("MM"));
                    // var year = Convert.ToInt32(listData[0].Date.ToString("yyyy"));

                    // var existingData = await dyeingMachineAdapter.GetByMonthAndYear(month,year);
                    // if(existingData.Count()>0)
                    //     await Delete(existingData);
                    
                    // await Save(listData);
                }
            }catch(Exception ex){
                throw new Exception($"Gagal menyimpan Sheet DYEING_MACHINE - " + ex.Message);
            }
        }

        // private async Task Save(IEnumerable<DashboardDyeingMachine> dyeingMachines){
        //     try{                
        //         var listData = new List<DashboardDyeingMachine>();
        //         foreach (var item in dyeingMachines)
        //         {
        //             await dyeingMachineAdapter.Insert(item);
        //         }
        //     }catch(Exception ex){
        //         throw new Exception("Gagal menyimpan data Mesin Dyeing - " + ex.Message);
        //     }
        // }

        // private async Task Delete(IEnumerable<DashboardDyeingMachine> dyeingMachines){
        //     try{                
        //         var listData = new List<DashboardDyeingMachine>();
        //         foreach (var item in dyeingMachines)
        //         {
        //             await dyeingMachineAdapter.Delete(item);
        //         }

        //     }catch(Exception ex){
        //         throw new Exception("Gagal menyimpan data Mesin Dyeing - " + ex.Message);
        //     }
        // }

#endregion

#region HANDOVER
        private async Task UploadHandover(ExcelWorksheets excel, DateTime periode, int data)    {
            var sheet = excel[data];
            var totalRow = sheet.Dimension.Rows;
            var listData = new List<DashboardHandOverArea>();
            int rowIndex = 0;
            try{
                for(rowIndex = 3; rowIndex <= totalRow; rowIndex ++) {
                    if (sheet.Cells[rowIndex,1].Value != null) {
                        generalHelper.ValidatePeriode(sheet.Cells[rowIndex,2], periode);
                        listData.Add(new DashboardHandOverArea(
                            generalHelper.GenerateId(sheet.Cells[rowIndex,2],sheet.Cells[rowIndex,1]),
                            converterChecker.GenerateValueInt(sheet.Cells[rowIndex,1]),
                            generalHelper.ConvertDateTime(sheet.Cells[rowIndex,2]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,3]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,4]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,5]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,6]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,7]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,8]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,9]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,10]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,11]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,12]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,13]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,14]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,15]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,16]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,17]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,18]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,19]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,20]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,21])
                        ));
                    }
                }
            }
            catch(Exception ex){
                throw new Exception($"Gagal memproses Sheet SERAH_TERIMA_AREA pada baris ke-{rowIndex} - {ex.Message}");
            }

            try{
                if(listData.Count()>0){
                    await handoverAdapter.DeleteByMonthAndYear(periode);
                    await handoverAdapter.InsertBulk(listData);
                    // var month = Convert.ToInt32(listData[0].Date?.ToString("MM"));
                    // var year = Convert.ToInt32(listData[0].Date?.ToString("yyyy"));

                    // var existingData = await handoverAdapter.GetByMonthAndYear(month,year);

                    // if(existingData.Count() > 0){
                    //     await Delete(existingData);
                    // }
                    
                    // await Save(listData);
                }
            }catch(Exception ex){
                throw new Exception($"Gagal menyimpan Sheet SERAH_TERIMA_AREA - " + ex.Message);
            }
        }
        // private async Task Save(IEnumerable<DashboardHandOverArea> handovers){
        //     try{                
        //         var listData = new List<DashboardHandOverArea>();
        //         foreach (var item in handovers)
        //         {
        //             await handoverAdapter.Insert(item);
        //         }

        //     }catch(Exception ex){
        //         throw new Exception("Gagal menyimpan data Serah Terima Area - " + ex.Message);
        //     }
        // }

        // private async Task Delete(IEnumerable<DashboardHandOverArea> handovers){
        //     try{                
        //         var listData = new List<DashboardHandOverArea>();
        //         foreach (var item in handovers)
        //         {
        //             await handoverAdapter.Delete(item);
        //         }

        //     }catch(Exception ex){
        //         throw new Exception("Gagal menyimpan data Serah Terima Area - " + ex.Message);
        //     }
        // }

#endregion

#region WIP MATERIAL
        private async Task UploadWipMaterial(ExcelWorksheets excel, DateTime periode, int data){
            var sheet = excel[data];
            var totalRow = sheet.Dimension.Rows;
            var listData = new List<DashboardWIPMaterial>();
            int rowIndex = 0;
            try{
                for(rowIndex = 3; rowIndex <= totalRow; rowIndex ++) {
                    if (sheet.Cells[rowIndex,1].Value != null) {
                        generalHelper.ValidatePeriode(sheet.Cells[rowIndex,2], periode);
                        listData.Add(new DashboardWIPMaterial(
                            generalHelper.GenerateId(sheet.Cells[rowIndex,2], sheet.Cells[rowIndex,1]),
                            Convert.ToInt32(sheet.Cells[rowIndex,1].Value),
                            generalHelper.ConvertDateTime(sheet.Cells[rowIndex,2]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,3]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,4]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,5]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,6]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,7]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,8]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,9])
                        ));
                    }
                }
            }
            catch(Exception ex){
                throw new Exception($"Gagal memproses Sheet WIP_MATERIAL pada baris ke-{rowIndex} - {ex.Message}");
            }

            try{
                if(listData.Count()>0){
                    await wipMaterialAdapter.DeleteByMonthAndYear(periode);
                    await wipMaterialAdapter.InsertBulk(listData);
                    // var month = Convert.ToInt32(listData[0].Date.ToString("MM"));
                    // var year = Convert.ToInt32(listData[0].Date.ToString("yyyy"));

                    // var existingData = await wipMaterialAdapter.GetByMonthAndYear(month,year);

                    // if(existingData.Count() > 0){
                    //     await Delete(existingData);
                    // }
                    
                    // await Save(listData);

                }
            }catch(Exception ex){
                throw new Exception($"Gagal menyimpan Sheet WIP_MATERIAL - " + ex.Message);
            }
        }

        // private async Task Save(IEnumerable<DashboardWIPMaterial> wipMaterials){
        //     try{                
        //         var listData = new List<DashboardWIPMaterial>();
        //         foreach (var item in wipMaterials)
        //         {
        //             await wipMaterialAdapter.Insert(item);
        //         }

        //     }catch(Exception ex){
        //         throw new Exception("Gagal menyimpan data WIP Material - " + ex.Message);
        //     }
        // }

        // private async Task Delete(IEnumerable<DashboardWIPMaterial> wipMaterials){
        //     try{                
        //         var listData = new List<DashboardWIPMaterial>();
        //         foreach (var item in wipMaterials)
        //         {
        //             await wipMaterialAdapter.Delete(item);
        //         }

        //     }catch(Exception ex){
        //         throw new Exception("Gagal menyimpan data WIP Material - " + ex.Message);
        //     }
        // }

#endregion

#region  CK DYEING
        private async Task UploadCkDyeing(ExcelWorksheets excel, DateTime periode, int data){
            var sheet = excel[data];
            var totalRow = sheet.Dimension.Rows;
            var listData = new List<DashboardCKDyeing>();
            int rowIndex = 0;
            try{
                for(rowIndex = 3; rowIndex <= totalRow; rowIndex ++) {
                    if (sheet.Cells[rowIndex,1].Value != null) {
                        generalHelper.ValidatePeriode(sheet.Cells[rowIndex,2], periode);
                        listData.Add(new DashboardCKDyeing(
                            generalHelper.GenerateId(sheet.Cells[rowIndex,2],sheet.Cells[rowIndex,1]),
                            Convert.ToInt32(sheet.Cells[rowIndex,1].Text),
                            generalHelper.ConvertDateTime(sheet.Cells[rowIndex,2]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,3]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,4]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,5]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,6]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,7]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,8]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,9]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,10]),
                            converterChecker.GenerateValueDouble(sheet.Cells[rowIndex,11]),
                            converterChecker.GenerateValueDouble(sheet.Cells[rowIndex,12])
                        ));
                        
                    }
                }
            }
            catch(Exception ex){
                throw new Exception($"Gagal memproses Sheet CK_DYEING pada baris ke-{rowIndex} - {ex.Message}");
            }

            try{
                if(listData.Count()>0){
                    await ckDyeingAdapter.DeleteByMonthAndYear(periode);
                    await ckDyeingAdapter.InsertBulk(listData);
                    // var month = Convert.ToInt32(listData[0].Date.ToString("MM"));
                    // var year = Convert.ToInt32(listData[0].Date.ToString("yyyy"));

                    // var existingData = await ckDyeingAdapter.GetByMonthAndYear(month,year);

                    // if(existingData.Count() > 0){
                    //     await Delete(existingData);
                    // }
                    // await Save(listData);
                }
            }catch(Exception ex){
                throw new Exception($"Gagal menyimpan Sheet CK_DYEING - " + ex.Message);
            }
        }

        // private async Task Save(IEnumerable<DashboardCKDyeing> ckDyeings){
        //     try{                
        //         var listData = new List<DashboardCKDyeing>();
        //         foreach (var item in ckDyeings)
        //         {
        //             await ckDyeingAdapter.Insert(item);
        //         }

        //     }catch(Exception ex){
        //         throw new Exception("Gagal menyimpan data CK Dyeing - " + ex.Message);
        //     }
        // }

        // private async Task Delete(IEnumerable<DashboardCKDyeing> ckDyeings){
        //     try{                
        //         var listData = new List<DashboardCKDyeing>();
        //         foreach (var item in ckDyeings)
        //         {
        //             await ckDyeingAdapter.Delete(item);
        //         }

        //     }catch(Exception ex){
        //         throw new Exception("Gagal menyimpan data CK Dyeing - " + ex.Message);
        //     }
        // }

#endregion

#region LPG MACHINE USAGE
        private async Task UploadLpgMachineUsage(ExcelWorksheets excel, DateTime periode, int data){
            var sheet = excel[data];
            var totalRow = sheet.Dimension.Rows;
            var listData = new List<DashboardLPGMachineUsage>();
            int rowIndex = 0;
            try{                
                for(rowIndex = 5; rowIndex <= totalRow; rowIndex ++) {
                    if (sheet.Cells[rowIndex,1].Value != null) {
                        generalHelper.ValidatePeriode(sheet.Cells[rowIndex,3], periode);
                        listData.Add(new DashboardLPGMachineUsage(
                            generalHelper.GenerateId(sheet.Cells[rowIndex, 3], sheet.Cells[rowIndex,1]),
                            Convert.ToInt32(sheet.Cells[rowIndex,1].Text),
                            Convert.ToInt32(sheet.Cells[rowIndex,2].Text),
                            generalHelper.ConvertDateTime(sheet.Cells[rowIndex,3]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,4]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex,5]),
                            converterChecker.GenerateValueDouble(sheet.Cells[rowIndex,6]),
                            converterChecker.GenerateValueDouble(sheet.Cells[rowIndex,7]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,8]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,9]),
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex,10])
                        ));
                    }
                }
            }
            catch(Exception ex){
                throw new Exception($"Gagal memproses Sheet PENGGUNAAN_LPG_MESIN pada baris ke-{rowIndex} - {ex.Message}");
            }

            try{
                if(listData.Count()>0){
                    await lpgMachineUsageAdapter.DeleteByMonthAndYear(periode);
                    await lpgMachineUsageAdapter.InsertBulk(listData);
                    // var month = Convert.ToInt32(listData[0].Date.ToString("MM"));
                    // var year = Convert.ToInt32(listData[0].Date.ToString("yyyy"));

                    // var existingData = await lpgMachineUsageAdapter.GetByMonthAndYear(month,year);

                    // if(existingData.Count() > 0){
                    //     await Delete(existingData);
                    // }                    
                    // await Save(listData);
                }
            }catch(Exception ex){
                throw new Exception($"Gagal menyimpan Sheet PENGGUNAAN_LPG_MESIN - " + ex.Message);
            }
        }

        // private async Task Save(IEnumerable<DashboardLPGMachineUsage> lpgMachineUsages){
        //     try{                
        //         var listData = new List<DashboardLPGMachineUsage>();
        //         foreach (var item in lpgMachineUsages)
        //         {
        //             await lpgMachineUsageAdapter.Insert(item);
        //         }

        //     }catch(Exception ex){
        //         throw new Exception("Gagal menyimpan data Penggunaan LPG Mesin - " + ex.Message);
        //     }
        // }

        // private async Task Delete(IEnumerable<DashboardLPGMachineUsage> lpgMachineUsages){
        //     try{                
        //         var listData = new List<DashboardLPGMachineUsage>();
        //         foreach (var item in lpgMachineUsages)
        //         {
        //             await lpgMachineUsageAdapter.Delete(item);
        //         }

        //     }catch(Exception ex){
        //         throw new Exception("Gagal menyimpan data Penggunaan LPG Mesin - " + ex.Message);
        //     }
        // }
#endregion
    }
}