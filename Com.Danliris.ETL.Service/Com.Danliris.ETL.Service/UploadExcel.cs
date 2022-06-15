using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using System.Collections.Generic;
using Com.Danliris.ETL.Service.ExcelModels;
using Com.Danliris.ETL.Service.Models;
using Com.Danliris.ETL.Service.Services;
using Com.Danliris.ETL.Service.Services.Class;
using Com.Danliris.ETL.Service.DBAdapters;
using System.Linq;
using Com.Danliris.ETL.Service.Services.Interfaces;
using System.Net.Http;

namespace Com.Danliris.ETL.Service
{
    public class UploadExcel
    {
        //public readonly HttpClient httpClient;
        private readonly IUploadExcelDyeingService _uploadExcelDyeingService;
        private readonly IUploadExcelKoranLaboratService _uploadExcelKoranLaboratService;
        private readonly IUploadExcelYarnDyeingService _uploadExcelYarnDyeingService;
        private readonly IUploadExcelGudangChemicalService _uploadExcelGudangChemicalService;
        private readonly IUploadExcelMaintenanceService _uploadExcelMaintenanceService;
        private readonly IUploadExcelPretreatmentService _uploadExcelPretreatmentService;
        private readonly IUploadExcelPrintingService _uploadExcelPrintingService;
        private readonly IUploadExcelFinishingService _uploadExcelFinishingService;
        private readonly IUploadExcelAreaQCService _uploadExcelAreaQCService;
        private readonly IUploadExcelSparepartService _uploadExcelSparepartService;
        private readonly IUploadExcelDigitalPrintingService _uploadExcelDigitalPrintingService;
        public UploadExcel(
            IUploadExcelDyeingService uploadExcelDyeingService,
            IUploadExcelKoranLaboratService uploadExcelKoranLaboratService,
            IUploadExcelYarnDyeingService uploadExcelYarnDyeingService,
            IUploadExcelGudangChemicalService uploadExcelGudangChemicalService,
            IUploadExcelMaintenanceService uploadExcelMaintenanceService,
            IUploadExcelPretreatmentService uploadExcelPretreatmentService,
            IUploadExcelPrintingService uploadExcelPrintingService,
            IUploadExcelFinishingService uploadExcelFinishingService,
            IUploadExcelAreaQCService uploadExcelAreaQCService,
            IUploadExcelSparepartService uploadExcelSparepartService,
            IUploadExcelDigitalPrintingService uploadExcelDigitalPrintingService
            )
        {
            _uploadExcelDyeingService = uploadExcelDyeingService;
            _uploadExcelKoranLaboratService = uploadExcelKoranLaboratService;
            _uploadExcelYarnDyeingService = uploadExcelYarnDyeingService;
            _uploadExcelGudangChemicalService = uploadExcelGudangChemicalService; 
            _uploadExcelMaintenanceService = uploadExcelMaintenanceService;
            _uploadExcelPretreatmentService = uploadExcelPretreatmentService;
            _uploadExcelPrintingService = uploadExcelPrintingService;
            _uploadExcelFinishingService = uploadExcelFinishingService;
            _uploadExcelAreaQCService = uploadExcelAreaQCService;
            _uploadExcelSparepartService = uploadExcelSparepartService;
            _uploadExcelDigitalPrintingService = uploadExcelDigitalPrintingService;
            //httpClient = clientFactory.CreateClient();
        }

        [FunctionName("UploadExcel")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            req.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");

            var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings:SQLConnectionString", EnvironmentVariableTarget.Process);
            FilePeriodeAdapters filePeriodeAdapters = new FilePeriodeAdapters(connectionString);   

            const string EXTENSION = ".xlsx";
            try
            {
                var formdata = await req.ReadFormAsync();
                IFormFile file = req.Form.Files["file"];
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                if (System.IO.Path.GetExtension(file.FileName) == EXTENSION) {
                    
                    using (var excelPack = new ExcelPackage())
                    {
                        using (var stream = file.OpenReadStream())
                        {
                            excelPack.Load(stream);
                        }

                        var area = req.Form["area"];
                        var areaName = req.Form["areaName"];
                        var periode = req.Form["periode"];
                        var monthName = req.Form["monthName"];

                        var periodeFormated = DateTime.ParseExact(periode, "M/yyyy", null);

                        if (areaName == "Yarn Dyeing") {
                            monthName = "-";
                        }

                        var filePeriode = 
                            new FilePeriode(
                                monthName, 
                                areaName, 
                                periodeFormated.Year, 
                                DateTime.UtcNow, 
                                DateTime.UtcNow, 
                                Convert.ToInt32(area)
                            );
                        
                        var sheet = excelPack.Workbook.Worksheets;
                        
                        try {
                            fileValidator(sheet, Convert.ToInt32(area));
                        } catch (Exception ex) {
                            return new BadRequestObjectResult(new ResponseFailed(ex.Message, null));
                        }

                        switch(Convert.ToInt32(area)) {
                            case 1:
                                #region Printing
                                // var uploadExcelPrintingService = new UploadExcelPrintingService();
                                try {
                                    await _uploadExcelPrintingService.Upload(sheet, periodeFormated);
                                    try {
                                        await filePeriodeAdapters.Insert(filePeriode);
                                    }
                                    catch (Exception ex) {
                                        return new BadRequestObjectResult(new ResponseFailed(ex.Message, null));
                                    }
                                    return new OkObjectResult(new ResponseSuccess("success"));
                                } catch (Exception ex) {
                                    // Disini buat messagenya
                                    return new BadRequestObjectResult(new ResponseFailed(ex.Message, ex.Data));
                                }
                                #endregion
                            case 2: 
                                #region Dyeing
                                //var uploadExcelDyeingService = new UploadExcelDyeingService();
                                try {
                                    await _uploadExcelDyeingService.Upload(sheet, periodeFormated);
                                    try {
                                        await filePeriodeAdapters.Insert(filePeriode);
                                    }
                                    catch (Exception ex) {
                                        return new BadRequestObjectResult(new ResponseFailed(ex.Message, null));
                                    }
                                    return new OkObjectResult(new ResponseSuccess("success"));
                                } catch (Exception ex) {
                                    // Disini buat messagenya
                                    return new BadRequestObjectResult(new ResponseFailed(ex.Message, ex.Data));
                                }
                            #endregion
                            case 3:
                                #region Yarn Dyeing
                                // var uploadExcelYarnDyeingService = new UploadExcelYarnDyeingService();
                                try {
                                    await _uploadExcelYarnDyeingService.Upload(sheet, periodeFormated);
                                    try {
                                        await filePeriodeAdapters.Insert(filePeriode);
                                    }
                                    catch (Exception ex)
                                    {
                                        return new BadRequestObjectResult(new ResponseFailed(ex.Message, null));
                                    }
                                    return new OkObjectResult(new ResponseSuccess("success"));
                                }
                                catch (Exception ex)
                                {
                                    // Disini buat messagenya
                                    return new BadRequestObjectResult(new ResponseFailed(ex.Message, ex.Data));
                                }
                            #endregion
                            case 4:
                                #region Digital/Transfer Printing
                                //var uploadExcelDigitalPrintService = new UploadExcelDigitalPrintService(log);
                                try {
                                    await _uploadExcelDigitalPrintingService.Upload(sheet, periodeFormated);
                                    try {
                                        await filePeriodeAdapters.Insert(filePeriode);
                                    }
                                    catch (Exception ex)
                                    {
                                        return new BadRequestObjectResult(new ResponseFailed(ex.Message, null));
                                    }
                                    return new OkObjectResult(new ResponseSuccess("success"));
                                }catch (Exception ex) {
                                    // Disini buat messagenya
                                    return new BadRequestObjectResult(new ResponseFailed(ex.Message, ex.Data));
                                }
                                #endregion
                            case 5: 
                                #region Pretreatment
                                // var uploadExcelPreTreatment = new uploadExcelPretreatmentService();
                                try {
                                    await _uploadExcelPretreatmentService.Upload(sheet, periodeFormated);
                                    try {
                                        await filePeriodeAdapters.Insert(filePeriode);
                                    }
                                    catch (Exception ex) {
                                        return new BadRequestObjectResult(new ResponseFailed(ex.Message, null));
                                    }
                                    return new OkObjectResult(new ResponseSuccess("success"));
                                } catch (Exception ex) {
                                    // Disini buat messagenya
                                    return new BadRequestObjectResult(new ResponseFailed(ex.Message, ex.Data));
                                }
                            #endregion
                            case 6:
                                #region Finishing
                                try {
                                    await _uploadExcelFinishingService.Upload(sheet, periodeFormated);
                                    try {
                                        await filePeriodeAdapters.Insert(filePeriode);
                                    }
                                    catch (Exception ex) {
                                        return new BadRequestObjectResult(new ResponseFailed(ex.Message, null));
                                    }
                                    return new OkObjectResult(new ResponseSuccess("success"));
                                } catch (Exception ex) {
                                    // Disini buat messagenya
                                    return new BadRequestObjectResult(new ResponseFailed(ex.Message, ex.Data));
                                }
                                #endregion
                            case 7: 
                                #region Laborat
                                // var uploadExcelKoranLaboratService = new UploadExcelKoranLaboratService();
                                try {
                                    await _uploadExcelKoranLaboratService.Upload(sheet, periodeFormated);
                                    try {
                                        await filePeriodeAdapters.Insert(filePeriode);
                                    }
                                    catch (Exception ex) {
                                        return new BadRequestObjectResult(new ResponseFailed(ex.Message, null));
                                    }
                                    return new OkObjectResult(new ResponseSuccess("success"));
                                } catch (Exception ex) {
                                    return new BadRequestObjectResult(new ResponseFailed(ex.Message, ex.Data));
                                }
                                #endregion
                            case 8:
                                #region Gudang Chemical
                               // var uploadExcelGudangChemical = new UploadExcelGudangChemical();
                                try {
                                    await _uploadExcelGudangChemicalService.Upload(sheet, periodeFormated);
                                    //await uploadExcelGudangChemical.UploadFile(sheet, log);
                                    try {
                                        await filePeriodeAdapters.Insert(filePeriode);
                                    }
                                    catch (Exception ex)
                                    {
                                        return new BadRequestObjectResult(new ResponseFailed(ex.Message, null));
                                    }
                                    return new OkObjectResult(new ResponseSuccess("success"));
                                }catch (Exception ex) {
                                    return new BadRequestObjectResult(new ResponseFailed(ex.Message, ex.Data));
                                }
                                #endregion
                            case 9:
                                #region Gudang Sparepart
                                //var uploadExcelSparePartService = new UploadExcelSparePartService();
                                try {
                                    await _uploadExcelSparepartService.Upload(sheet, periodeFormated);
                                    try {
                                        await filePeriodeAdapters.Insert(filePeriode);
                                    }
                                    catch (Exception ex) {
                                        return new BadRequestObjectResult(new ResponseFailed(ex.Message, null));
                                    }
                                    return new OkObjectResult(new ResponseSuccess("success"));
                                } catch (Exception ex) {
                                    return new BadRequestObjectResult(new ResponseFailed(ex.Message, ex.Data));
                                }
                            #endregion
                            case 10:
                                #region QC
                                // var uploadExcelAreaQCService = new UploadExcelAreaQCService();
                                try {
                                    await _uploadExcelAreaQCService.Upload(sheet, periodeFormated);
                                    try {
                                        await filePeriodeAdapters.Insert(filePeriode);
                                    }
                                    catch (Exception ex)
                                    {
                                        return new BadRequestObjectResult(new ResponseFailed(ex.Message, null));
                                    }
                                    return new OkObjectResult(new ResponseSuccess("success"));
                                }
                                catch (Exception ex)
                                {
                                    return new BadRequestObjectResult(new ResponseFailed(ex.Message, ex.Data));
                                }
                            #endregion
                            case 11:
                                #region Maintenance
                                // var uploadExcelMaintenanceService = new UploadExcelMaintenanceService();
                                try {
                                    await _uploadExcelMaintenanceService.Upload(sheet, periodeFormated);
                                    try {
                                        await filePeriodeAdapters.Insert(filePeriode);
                                    }
                                    catch (Exception ex)
                                    {
                                        return new BadRequestObjectResult(new ResponseFailed(ex.Message, null));
                                    }
                                    return new OkObjectResult(new ResponseSuccess("success"));
                                }
                                catch (Exception ex)
                                {
                                    return new BadRequestObjectResult(new ResponseFailed(ex.Message, ex.Data));
                                }
                            #endregion
                            default:
                                return new BadRequestObjectResult(new ResponseFailed("Area file tidak ditemukan", null));
                        }

                        return new BadRequestObjectResult(new ResponseFailed("Gagal menyimpan data", null));

                    }
                }
                return new BadRequestObjectResult(new ResponseFailed("Ekstensi file harus bertipe .xlsx", null));
            }
            catch (Exception ex)    
            {
                return new BadRequestObjectResult(new ResponseFailed("Gagal menyimpan data", ex));
            }
        }
    
        public static void fileValidator(ExcelWorksheets worksheets, int areaId) {
            // List informasi sheet 
            switch (areaId) {
                case 1:
                    #region Printing
                    var sheetPrinting = new List<string>();
                    sheetPrinting.Add("MESIN_UTAMA");
                    sheetPrinting.Add("MESIN_PENDUKUNG");
                    sheetPrinting.Add("PASTA");
                    sheetPrinting.Add("ENGRAVING");
                    sheetPrinting.Add("WIP_(SERAH_TERIMA_AREA)");

                    if (worksheets.Count() != sheetPrinting.Count) {
                        throw new Exception("File tidak sesuai untuk Area Printing");
                    }

                    foreach (var sheet in worksheets) {
                        if (!sheetPrinting.Contains(sheet.Name.Trim().ToUpper())) {
                            throw new Exception("File tidak sesuai untuk Area Printing");
                        }
                    }
                    #endregion
                    break;
                case 2:
                    #region Dyeing
                    var sheetDyeing = new List<string>();
                    sheetDyeing.Add("MESIN_DYEING");
                    sheetDyeing.Add("SERAH_TERIMA_AREA");
                    sheetDyeing.Add("WIP_MATERIAL");
                    sheetDyeing.Add("CK_DYEING");
                    sheetDyeing.Add("PENGGUNAAN_LPG_MESIN");

                    if (worksheets.Count() != sheetDyeing.Count) {
                        throw new Exception("File tidak sesuai untuk Area Dyeing");
                    }

                    foreach (var sheet in worksheets) {
                        if (!sheetDyeing.Contains(sheet.Name.Trim().ToUpper())) {
                            throw new Exception("File tidak sesuai untuk Area Dyeing");
                        }
                    }
                    #endregion
                    break;
                case 3:
                    #region Yarn Dyeing
                    var sheetYarnDyeing = new List<string>();
                    sheetYarnDyeing.Add("PERMINTAAN_BENANG_CELUP");
                    sheetYarnDyeing.Add("PERMINTAAN_MINILOOM");
                    sheetYarnDyeing.Add("PERMINTAAN_ORDER_BENANG");
                    sheetYarnDyeing.Add("PERMINTAAN_ORDER_FABRIC_YD");

                    if (worksheets.Count() != sheetYarnDyeing.Count)
                    {
                        throw new Exception("File tidak sesuai untuk Area Yarn Dyeing");
                    }

                    foreach (var sheet in worksheets)
                    {
                        if (!sheetYarnDyeing.Contains(sheet.Name.Trim().ToUpper()))
                        {
                            throw new Exception("File tidak sesuai untuk Area Yarn Dyeing");
                        }
                    }
                    #endregion
                    break;
                case 4:
                    #region Digital Print
                    var sheetDigitalPrint = new List<string>();
                    sheetDigitalPrint.Add("PRODUKSI_DIGITAL_TRANSFER");
                    sheetDigitalPrint.Add("ORDER_MASUK");
                    sheetDigitalPrint.Add("ORDER_SO_DIGITAL_TRANSFER");
                    sheetDigitalPrint.Add("WIP_AREA");
                    sheetDigitalPrint.Add("PENGIRIMAN_HARIAN");

                    if (worksheets.Count() != sheetDigitalPrint.Count)
                    {
                        throw new Exception("File tidak sesuai untuk Area Digita/Transfer Printing");
                    }

                    foreach (var sheet in worksheets)
                    {
                        if (!sheetDigitalPrint.Contains(sheet.Name.Trim().ToUpper()))
                        {
                            throw new Exception("File tidak sesuai untuk Area Digita/Transfer Printing");
                        }
                    }
                    #endregion
                    break;
                case 5:
                    #region Pretreatment
                    var sheetPretreatment= new List<string>();
                    sheetPretreatment.Add("PRODUKSI_MESIN_PRETREATMENT");
                    sheetPretreatment.Add("MATERIAL_MASUK_PREPARING");
                    sheetPretreatment.Add("REPROSES");
                    sheetPretreatment.Add("PENGIRIMAN_ANTAR_AREA");
                    sheetPretreatment.Add("STOCK_OPNAME");
                    sheetPretreatment.Add("MATERIAL_TROUBLE");
                    sheetPretreatment.Add("STOCK_MATERIAL");

                    if (worksheets.Count() != sheetPretreatment.Count) {
                        throw new Exception("File tidak sesuai untuk Area Pretreatment");
                    }

                    foreach (var sheet in worksheets) {
                        if (!sheetPretreatment.Contains(sheet.Name.Trim().ToUpper())) {
                            throw new Exception("File tidak sesuai untuk Area Pretreatment");
                        }
                    }
                    #endregion
                    break;
                case 6:
                    #region Finishing
                    var sheetFinishing = new List<string>();
                    sheetFinishing.Add("PRODUKSI_F.");
                    sheetFinishing.Add("CHEMICAL_F.");
                    sheetFinishing.Add("REPROSES_F.");
                    sheetFinishing.Add("PENYERAHAN_F.");
                    sheetFinishing.Add("TERIMA_F.");
                    sheetFinishing.Add("STOCK_OPNAME_F.");

                    if (worksheets.Count() != sheetFinishing.Count)
                    {
                        throw new Exception("File tidak sesuai untuk Area Finishing");
                    }

                    foreach (var sheet in worksheets)
                    {
                        if (!sheetFinishing.Contains(sheet.Name.Trim().ToUpper()))
                        {
                            throw new Exception("File tidak sesuai untuk Area Finishing");
                        }
                    }
                    #endregion
                    break;
                case 7:
                    #region Laborat
                    var sheetLaborat = new List<string>();
                    sheetLaborat.Add("TEST_DYEING");
                    sheetLaborat.Add("TEST_PRINTING");
                    sheetLaborat.Add("LAB_DIP");
                    sheetLaborat.Add("LAB_PRINTING");
                    sheetLaborat.Add("HASIL_TEST_RND");

                    if (worksheets.Count() != sheetLaborat.Count) {
                        throw new Exception("File tidak sesuai untuk Area Laborat");
                    }

                    foreach (var sheet in worksheets) {
                        if (!sheetLaborat.Contains(sheet.Name.Trim().ToUpper())) {
                            throw new Exception("File tidak sesuai untuk Area Laborat");
                        }
                    }
                    #endregion
                    break;
                case 8:
                    #region Gudang Chemical
                    var sheetChemical = new List<string>();
                    sheetChemical.Add("STOCK_CHEMICAL");
                    sheetChemical.Add("PENGELUARAN_BARANG");
                    sheetChemical.Add("PENERIMAAN_BARANG");

                    if (worksheets.Count() != sheetChemical.Count) {
                        throw new Exception("File tidak sesuai untuk Area Gudang Chemical");
                    }

                    foreach (var sheet in worksheets) {
                        if (!sheetChemical.Contains(sheet.Name.Trim().ToUpper())) {
                            throw new Exception("File tidak sesuai untuk Area Gudang Chemical");
                        }
                    }
                    #endregion
                    break;
                case 9:
                    #region Sparepart
                    var sheetSparepart = new List<string>();
                    sheetSparepart.Add("STOCK_SPARE_PART");
                    sheetSparepart.Add("PENGELUARAN_BARANG");
                    sheetSparepart.Add("PENERIMAAN_BARANG");

                    if (worksheets.Count() != sheetSparepart.Count) {
                        throw new Exception("File tidak sesuai untuk Area Gudang Sparepart");
                    }

                    foreach (var sheet in worksheets) {
                        if (!sheetSparepart.Contains(sheet.Name.Trim().ToUpper())) {
                            throw new Exception("File tidak sesuai untuk Area Gudang Sparepart");
                        }
                    }
                    #endregion
                    break;
                case 10:
                    #region QC
                    var sheetQC = new List<string>();
                    sheetQC.Add("PRETREATMENT");
                    sheetQC.Add("PRINTING");
                    sheetQC.Add("DYEING");
                    sheetQC.Add("DIGITAL PRINT");

                    if (worksheets.Count() != sheetQC.Count)
                    {
                        throw new Exception("File tidak sesuai untuk Area QC");
                    }

                    foreach (var sheet in worksheets)
                    {
                        if (!sheetQC.Contains(sheet.Name.Trim().ToUpper()))
                        {
                            throw new Exception("File tidak sesuai untuk Area QC");
                        }
                    }
                    #endregion
                    break;
                case 11:
                    #region Maintenance
                    var sheetMaintenance = new List<string>();
                    sheetMaintenance.Add("SERAH_TERIMA");
                    sheetMaintenance.Add("LKM");

                    if (worksheets.Count() != sheetMaintenance.Count)
                    {
                        throw new Exception("File tidak sesuai untuk Area Maintenance");
                    }

                    foreach (var sheet in worksheets)
                    {
                        if (!sheetMaintenance.Contains(sheet.Name.Trim().ToUpper()))
                        {
                            throw new Exception("File tidak sesuai untuk Area Maintenance");
                        }
                    }
                    #endregion
                    break;
            }
        }

    }
}