using Com.Danliris.ETL.Service.DBAdapters.DigitalPrintingAdapters;
using Com.Danliris.ETL.Service.DBAdapters.DyeingAdapters;
using Com.Danliris.ETL.Service.DBAdapters.FinishingAdapters;
using Com.Danliris.ETL.Service.DBAdapters.GudangChemicalAdapters;
using Com.Danliris.ETL.Service.DBAdapters.GudangSparepartAdapters;
using Com.Danliris.ETL.Service.DBAdapters.KoranLaboratAdapters;
using Com.Danliris.ETL.Service.DBAdapters.KoranQCLineAdapters;
using Com.Danliris.ETL.Service.DBAdapters.MaintenanceAdapters;
using Com.Danliris.ETL.Service.DBAdapters.PretreatmentAdapters;
using Com.Danliris.ETL.Service.DBAdapters.PrintingAdapters;
using Com.Danliris.ETL.Service.DBAdapters.YarnDyeingAdapters;
using Com.Danliris.ETL.Service.ExcelModels;
using Com.Danliris.ETL.Service.ExcelModels.DashboardDyeingModels;
using Com.Danliris.ETL.Service.ExcelModels.DashboardKoranLaborat;
using Com.Danliris.ETL.Service.ExcelModels.DashboardYarnDyeing;
using Com.Danliris.ETL.Service.ExcelModels.KoranQCLine;
using Com.Danliris.ETL.Service.Models.DashboardGudangChemicalModels;
using Com.Danliris.ETL.Service.Models.DashboardGudangSparepartModels;
using Com.Danliris.ETL.Service.Models.DashboardMaintenanceModels;
using Com.Danliris.ETL.Service.Models.DashboardPretreatmentModels;
using Com.Danliris.ETL.Service.Models.DashboardPrintingModels;
using Com.Danliris.ETL.Service.Services;
using Com.Danliris.ETL.Service.Services.Class;
using Com.Danliris.ETL.Service.Services.Interfaces;
using Com.Danliris.ETL.Service.Tools;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Http;
using System;
using System.Collections.Generic;
using System.Text;
using HandoverAdapter = Com.Danliris.ETL.Service.DBAdapters.MaintenanceAdapters.HandoverAdapter;

[assembly: FunctionsStartup(typeof(Com.Danliris.ETL.Service.Startup))]
namespace Com.Danliris.ETL.Service
{
    public class Startup : FunctionsStartup
    {
        private readonly string connectionString = Environment.GetEnvironmentVariable("ConnectionStrings:SQLConnectionString", EnvironmentVariableTarget.Process);

        public override void Configure(IFunctionsHostBuilder builder)
        {
            //builder.Services.AddHttpClient();

            //Dyeing
            builder.Services
                .AddSingleton<ISqlDataContext<DashboardDyeingMachine>>((s) =>
                {
                    return new SqlDataContext<DashboardDyeingMachine>(connectionString);
                })
                .AddSingleton<ISqlDataContext<DashboardHandOverArea>>((s) =>
                {
                    return new SqlDataContext<DashboardHandOverArea>(connectionString);
                })
                .AddSingleton<ISqlDataContext<DashboardCKDyeing>>((s) =>
                {
                    return new SqlDataContext<DashboardCKDyeing>(connectionString);
                })
                .AddSingleton<ISqlDataContext<DashboardLPGMachineUsage>>((s) =>
                {
                    return new SqlDataContext<DashboardLPGMachineUsage>(connectionString);
                })
                .AddSingleton<ISqlDataContext<DashboardWIPMaterial>>((s) =>
                {
                    return new SqlDataContext<DashboardWIPMaterial>(connectionString);
                });
            //Koran Laborat
            builder.Services
                .AddSingleton<ISqlDataContext<DashboardDyeingTest>>((s) =>
                {
                    return new SqlDataContext<DashboardDyeingTest>(connectionString);
                })
                .AddSingleton<ISqlDataContext<DashboardLabDip>>((s) =>
                {
                    return new SqlDataContext<DashboardLabDip>(connectionString);
                })
                .AddSingleton<ISqlDataContext<DashboardPrintingLab>>((s) =>
                {
                    return new SqlDataContext<DashboardPrintingLab>(connectionString);
                })                
                .AddSingleton<ISqlDataContext<DashboardLabDip>>((s) =>
                {
                    return new SqlDataContext<DashboardLabDip>(connectionString);
                })
                .AddSingleton<ISqlDataContext<DashboardPrintingTest>>((s) =>
                {
                    return new SqlDataContext<DashboardPrintingTest>(connectionString);
                })
                .AddSingleton<ISqlDataContext<DashboardRnDTestResult>>((s) =>
                {
                    return new SqlDataContext<DashboardRnDTestResult>(connectionString);
                });
            //Yarn Dyeing
            builder.Services
                .AddSingleton<ISqlDataContext<DashboardDyeYarnRequest>>((s) =>
                {
                    return new SqlDataContext<DashboardDyeYarnRequest>(connectionString);
                })
                .AddSingleton<ISqlDataContext<DashboardFabricYDOrderRequest>>((s) =>
                {
                    return new SqlDataContext<DashboardFabricYDOrderRequest>(connectionString);
                })
                .AddSingleton<ISqlDataContext<DashboardMiniloomRequest>>((s) =>
                {
                    return new SqlDataContext<DashboardMiniloomRequest>(connectionString);
                })
                .AddSingleton<ISqlDataContext<DashboardYarnOrderRequest>>((s) =>
                {
                    return new SqlDataContext<DashboardYarnOrderRequest>(connectionString);
                });
            //Chemical
            builder.Services
                .AddSingleton<ISqlDataContext<ChemicalStockModel>>((s) =>
                 {
                     return new SqlDataContext<ChemicalStockModel>(connectionString);
                 })
                .AddSingleton<ISqlDataContext<ChemicalReceiptItemModel>>((s) =>
                {
                    return new SqlDataContext<ChemicalReceiptItemModel>(connectionString);
                })
                .AddSingleton<ISqlDataContext<ChemicalReleaseItemModel>>((s) =>
                {
                    return new SqlDataContext<ChemicalReleaseItemModel>(connectionString);
                });
            //Maintenance
            builder.Services
                  .AddSingleton<ISqlDataContext<DashboardLkmModel>>((s) =>
                  {
                      return new SqlDataContext<DashboardLkmModel>(connectionString);
                  })
                  .AddSingleton<ISqlDataContext<DashboardHandoverModel>>((s) =>
                  {
                      return new SqlDataContext<DashboardHandoverModel>(connectionString);
                  });
            //Printing
            builder.Services
                .AddSingleton<ISqlDataContext<DashBoardMainMachineModel>>((s) =>
                {
                    return new SqlDataContext<DashBoardMainMachineModel>(connectionString);
                })
                .AddSingleton<ISqlDataContext<DashBoardSupportMachineModel>>((s) =>
                {
                    return new SqlDataContext<DashBoardSupportMachineModel>(connectionString);
                })
                .AddSingleton<ISqlDataContext<DashBoardPasteModel>>((s) =>
                {
                    return new SqlDataContext<DashBoardPasteModel>(connectionString);
                })
                .AddSingleton<ISqlDataContext<DashBoardEngravingModel>>((s) =>
                {
                    return new SqlDataContext<DashBoardEngravingModel>(connectionString);
                })
                .AddSingleton<ISqlDataContext<DashBoardWIPHandoverAreaModel>>((s) =>
                {
                    return new SqlDataContext<DashBoardWIPHandoverAreaModel>(connectionString);
                });
            //Pretreatment
            builder.Services
                .AddSingleton<ISqlDataContext<PretreatmentMachineProductionModel>>((s) =>
                {
                    return new SqlDataContext<PretreatmentMachineProductionModel>(connectionString);
                })
                .AddSingleton<ISqlDataContext<IncomingMaterialPreparingModel>>((s) =>
                {
                    return new SqlDataContext<IncomingMaterialPreparingModel>(connectionString);
                })
                .AddSingleton<ISqlDataContext<MaterialTroubleModel>>((s) =>
                {
                    return new SqlDataContext<MaterialTroubleModel>(connectionString);
                })
                .AddSingleton<ISqlDataContext<ReprocessModel>>((s) =>
                {
                    return new SqlDataContext<ReprocessModel>(connectionString);
                })
                .AddSingleton<ISqlDataContext<StockMaterialModel>>((s) =>
                {
                    return new SqlDataContext<StockMaterialModel>(connectionString);
                })
                .AddSingleton<ISqlDataContext<StockOpnameModel>>((s) =>
                {
                    return new SqlDataContext<StockOpnameModel>(connectionString);
                })
                .AddSingleton<ISqlDataContext<DeliveryBetweenAreaModel>>((s) =>
                {
                    return new SqlDataContext<DeliveryBetweenAreaModel>(connectionString);
                });
            //Finishing
            builder.Services
                .AddSingleton<ISqlDataContext<DashboardChemicalF>>((s) =>
                {
                    return new SqlDataContext<DashboardChemicalF>(connectionString);
                })
                .AddSingleton<ISqlDataContext<DashboardProductionF>>((s) =>
                {
                    return new SqlDataContext<DashboardProductionF>(connectionString);
                })
                .AddSingleton<ISqlDataContext<DashboardReceiveF>>((s) =>
                {
                    return new SqlDataContext<DashboardReceiveF>(connectionString);
                })
                .AddSingleton<ISqlDataContext<DashboardReprocessF>>((s) =>
                {
                    return new SqlDataContext<DashboardReprocessF>(connectionString);
                })
                .AddSingleton<ISqlDataContext<DashboardStockOpnameF>>((s) =>
                {
                    return new SqlDataContext<DashboardStockOpnameF>(connectionString);
                })
                .AddSingleton<ISqlDataContext<DashboardTransferF>>((s) =>
                {
                    return new SqlDataContext<DashboardTransferF>(connectionString);
                });

            //Sparepart
            builder.Services
                .AddSingleton<ISqlDataContext<StockSparepartModel>>((s) =>
                {
                    return new SqlDataContext<StockSparepartModel>(connectionString);
                })
                .AddSingleton<ISqlDataContext<SparepartItemRelease>>((s) =>
                {
                    return new SqlDataContext<SparepartItemRelease>(connectionString);
                })
                .AddSingleton<ISqlDataContext<SparepartItemReceipt>>((s) =>
                {
                    return new SqlDataContext<SparepartItemReceipt>(connectionString);
                });

            //DigitalPrinting
            builder.Services
                .AddSingleton<ISqlDataContext<ProductionDigitalTransfer>>((s) =>
                {
                    return new SqlDataContext<ProductionDigitalTransfer>(connectionString);
                })
                .AddSingleton<ISqlDataContext<DailyDelivery>>((s) =>
                {
                    return new SqlDataContext<DailyDelivery>(connectionString);
                })
                .AddSingleton<ISqlDataContext<OrderIn>>((s) =>
                {
                    return new SqlDataContext<OrderIn>(connectionString);
                })
                .AddSingleton<ISqlDataContext<SOOrderDigitalTransfer>>((s) =>
                {
                    return new SqlDataContext<SOOrderDigitalTransfer>(connectionString);
                })
                .AddSingleton<ISqlDataContext<WipArea>>((s) =>
                {
                    return new SqlDataContext<WipArea>(connectionString);
                });
            // QC Line
            builder.Services
                .AddSingleton<ISqlDataContext<DigitalPrint>>((s) =>
                {
                    return new SqlDataContext<DigitalPrint>(connectionString);
                })
                .AddSingleton<ISqlDataContext<Dyeing>>((s) =>
                {
                    return new SqlDataContext<Dyeing>(connectionString);
                })
                .AddSingleton<ISqlDataContext<Pretreatment>>((s) =>
                {
                    return new SqlDataContext<Pretreatment>(connectionString);
                })
                .AddSingleton<ISqlDataContext<Printing>>((s) =>
                {
                    return new SqlDataContext<Printing>(connectionString);
                });


            //Dyeing
            builder.Services.AddTransient<IDyeingMachineAdapter, DyeingMachineAdapter>()
                .AddTransient<ICkDyeingAdapter, CkDyeingAdapter>()
                .AddTransient<IHandoverAdapter, DBAdapters.DyeingAdapters.HandoverAdapter>()
                .AddTransient<ILpgMachineUsageAdapter, LpgMachineUsageAdapter>()
                .AddTransient<IWipMaterialAdapter, WipMaterialAdapter>()
                .AddTransient<IUploadExcelDyeingService, UploadExcelDyeingService>();
            //Yarn Dyeing
            builder.Services.AddTransient<IDyeYarnRequestAdapter, DyeYarnRequestAdapter>()
                .AddTransient<IFabricYDOrderRequestAdapter, FabricYDOrderRequestAdapter>()
                .AddTransient<IMiniloomRequestAdapter, MiniloomRequestAdapter>()
                .AddTransient<IYarnOrderRequestAdapter, YarnOrderRequestAdapter>()
                .AddTransient<IUploadExcelYarnDyeingService, UploadExcelYarnDyeingService>();
            //KoranLaborat
            builder.Services.AddTransient<IDyeingTestAdapter, DyeingTestAdapter>()
                .AddTransient<ILabDipAdapter, LabDipAdapter>()
                .AddTransient<IPrintingLabAdapter, PrintingLabAdapter>()
                .AddTransient<IPrintingTestAdapter, PrintingTestAdapter>()
                .AddTransient<IRnDTestResultAdapter, RnDTestResultAdapter>()
                .AddTransient<IUploadExcelKoranLaboratService, UploadExcelKoranLaboratService>();
            //Chemical
            builder.Services
                .AddTransient<IChemicalStockAdapter, ChemicalStockAdapter>()
                .AddTransient<IReceiptItemAdapter, ReceiptItemAdapter>()
                .AddTransient<IReleaseItemAdapter, ReleaseItemAdapter>()
                .AddTransient<IUploadExcelGudangChemicalService, UploadExcelGudangChemicalService>();
            //Maintenance
            builder.Services
                .AddTransient<IDashboardHandoverAdapter, HandoverAdapter>()
                .AddTransient<IDashboardLkmAdapter, LkmAdapter>()
                .AddTransient<IUploadExcelMaintenanceService, UploadExcelMaintenanceService>();
            //Printing
            builder.Services
                .AddTransient<IMainMachineAdapter, MainMachineAdapter>()
                .AddTransient<ISupportMachineAdapter, SupportMachineAdapter>()
                .AddTransient<IPasteAdapter, PasteAdapter>()
                .AddTransient<IEngravingAdapter, EngravingAdapter>()
                .AddTransient<IWipHandoverAdapter, WipHandoverAdapter>()
                .AddTransient<IUploadExcelPrintingService, UploadExcelPrintingService>()
                .AddTransient<IDownloadExcelPrintingService, DownloadExcelPrintingService>();
            //Pretreatment
            builder.Services
                .AddTransient<IMachineProductionAdapter, MachineProductionAdapter>()
                .AddTransient<IMaterialPreparingAdapter , MaterialPreparingAdapter>()
                .AddTransient<IMaterialTroubleAdapter, MaterialTroubleAdapter>()
                .AddTransient<DBAdapters.PretreatmentAdapters.IReprocessAdapter, DBAdapters.PretreatmentAdapters.ReprocessAdapter>()
                .AddTransient<IStockMaterialAdapter, StockMaterialAdapter>()
                .AddTransient<DBAdapters.PretreatmentAdapters.IStockOpnameAdapter, DBAdapters.PretreatmentAdapters.StockOpnameAdapter>()
                .AddTransient<IDeliveryBetweenAdapter , DeliveryBetweenAdapter>()
                .AddTransient<IUploadExcelPretreatmentService, UploadExcelPretreatmentService>();

            //Finishing            
            builder.Services
                .AddTransient<IChemicalAdapter, ChemicalAdapter>()
                .AddTransient<IProductionAdapter, ProductionAdapter>()
                .AddTransient<IReceiveAdapter, ReceiveAdapter>()
                .AddTransient<DBAdapters.FinishingAdapters.IReprocessAdapter, DBAdapters.FinishingAdapters.ReprocessAdapter>()
                .AddTransient<DBAdapters.FinishingAdapters.IStockOpnameAdapter, DBAdapters.FinishingAdapters.StockOpnameAdapter>()
                .AddTransient<ITransferAdapter, TransferAdapter>()
                .AddTransient<IUploadExcelFinishingService, UploadExcelFinishingService>();

            //Sparepart
            builder.Services
                .AddTransient<IStockSparepartAdapter, StockSparepartAdapter>()
                .AddTransient<IReceiptItemSparepartAdapter, ReceiptItemSparepartAdapter>()
                .AddTransient<IReleaseItemSparepartAdapter, ReleaseItemSparepartAdapter>()
                .AddTransient<IUploadExcelSparepartService, UploadExcelSparePartService>();

            //DigitalPrinting
            builder.Services
                .AddTransient<IDailyDeliveryAdapter, DailyDeliveryAdapters>()
                .AddTransient<ISOOrderDigitalTransferAdapter, SOOrderDigitalTransferAdapters>()
                .AddTransient<IWipAreaAdapter, WipAreaAdapters>()
                .AddTransient<IProductionDigitalTransferAdapter , ProductionDigitalTransferAdapters>()
                .AddTransient<IOrderInAdapter , OrderInAdapters>()
                .AddTransient<IUploadExcelDigitalPrintingService, UploadExcelDigitalPrintService>();

            //QC Line
            builder.Services
                .AddTransient<IDigitalPrintAdapter, DigitalPrintAdapter>()
                .AddTransient<IDyeingAdapter, DyeingAdapter>()
                .AddTransient<IPretreatmentAdapter, PretreatmentAdapter>()
                .AddTransient<IPrintingAdapter, PrintingAdapter>()
                .AddTransient<IUploadExcelAreaQCService, UploadExcelAreaQCService>();

        }
    }
}
