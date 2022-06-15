using Castle.Core.Logging;
using Com.Danliris.ETL.Service.DBAdapters.PretreatmentAdapters;
using Com.Danliris.ETL.Service.Models.DashboardPretreatmentModels;
using Com.Danliris.ETL.Service.Services;
using Com.Danliris.ETL.Service.Test.DataUtil;
using Com.Danliris.ETL.Service.Tools;
using Moq;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.ETL.Service.Test.Services
{
    public class UploadExcelPretreatmentServiceTest
    {
        GenerateTemplateSheet generateTemplate = new GenerateTemplateSheet();

        private ExcelWorksheets GeneratePretreatmentMachineProductionSheet()
        {
            var guid = Guid.NewGuid().ToString("N").Substring(0, 5);
            Random rnd = new Random();

            var startRow = 3;
            var excel = generateTemplate.GenerateTempalate("Produksi_Mesin_Pretreatment", startRow, 17);
            var sheet = excel[0];

            sheet.Cells[startRow, 1].Value = rnd.Next(1, 10);            
            sheet.Cells[startRow, 2].Value = DateTime.Now.ToString("dd/MM/yyyy");
            sheet.Cells[startRow, 3].Value = "Shift";
            sheet.Cells[startRow, 4].Value = "Group-" + guid;
            sheet.Cells[startRow, 5].Value = "Machine-" + guid;
            sheet.Cells[startRow, 6].Value = "OrderNo-" + guid;
            sheet.Cells[startRow, 7].Value = "Material-" + guid;
            sheet.Cells[startRow, 8].Value = "Color_C/W-" + guid;
            sheet.Cells[startRow, 9].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 10].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 11].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 12].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 13].Value = "TrainNo-" + guid;
            sheet.Cells[startRow, 14].Value = "ProcessType-" + guid;
            sheet.Cells[startRow, 15].Value = rnd.Next(1, 10); 
            sheet.Cells[startRow, 16].Value = "Description-" + guid;           


            return excel;
        }

        private ExcelWorksheets GenerateMaterialPreparingSheet()
        {
            var guid = Guid.NewGuid().ToString("N").Substring(0, 5);
            Random rnd = new Random();

            var startRow = 3;
            var excel = generateTemplate.GenerateTempalate("Material_Masuk_Preparing", startRow, 19);
            var sheet = excel[0];

            sheet.Cells[startRow, 1].Value = rnd.Next(1, 10);            
            sheet.Cells[startRow, 2].Value = "Aktivitas-" + guid;
            sheet.Cells[startRow, 3].Value = "DPFRP-" + guid;
            sheet.Cells[startRow, 4].Value = DateTime.Now.ToString("dd/MM/yyyy");
            sheet.Cells[startRow, 5].Value = "Shift-" + guid;
            sheet.Cells[startRow, 6].Value = "Group-" + guid;
            sheet.Cells[startRow, 7].Value = "NoSP-" + guid;
            sheet.Cells[startRow, 8].Value = "Konstruksi-" + guid;
            sheet.Cells[startRow, 9].Value = "Warna-" + guid;
            sheet.Cells[startRow, 10].Value = "TrainNo-" + guid;
            sheet.Cells[startRow, 11].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 12].Value = "Grade-" + guid;
            sheet.Cells[startRow, 13].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 14].Value = DateTime.Now.ToString("dd/MM/yyyy");
            sheet.Cells[startRow, 15].Value = DateTime.Now.ToString("dd/MM/yyyy");
            sheet.Cells[startRow, 16].Value = DateTime.Now.ToString("dd/MM/yyyy");
            sheet.Cells[startRow, 17].Value = "Time-" + guid;
            sheet.Cells[startRow, 18].Value = DateTime.Now.ToString("dd/MM/yyyy");
            sheet.Cells[startRow, 19].Value = DateTime.Now.ToString("dd/MM/yyyy");


            return excel;
        }

        private ExcelWorksheets GenerateReprocessSheet()
        {
            var guid = Guid.NewGuid().ToString("N").Substring(0, 5);
            Random rnd = new Random();

            var startRow = 3;
            var excel = generateTemplate.GenerateTempalate("Reproses", startRow, 13);
            var sheet = excel[0];

            sheet.Cells[startRow, 1].Value = rnd.Next(1, 10);            
            sheet.Cells[startRow, 2].Value = DateTime.Now.ToString("dd/MM/yyyy");
            sheet.Cells[startRow, 3].Value = "Shift-" + guid;
            sheet.Cells[startRow, 4].Value = "Group-" + guid;
            sheet.Cells[startRow, 5].Value = "OrderNo-" + guid;
            sheet.Cells[startRow, 6].Value = "Material";
            sheet.Cells[startRow, 7].Value = "No_Kereta-" + guid;
            sheet.Cells[startRow, 8].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 9].Value = "Machine-" + guid;
            sheet.Cells[startRow, 10].Value = "ProcessType-" + guid;
            sheet.Cells[startRow, 11].Value = "Reprocess-" + guid;
            sheet.Cells[startRow, 12].Value = "Problem-" + guid;


            return excel;
        }

        private ExcelWorksheets GenerateDeliveryBetweenAreaSheet()
        {
            var guid = Guid.NewGuid().ToString("N").Substring(0, 5);
            Random rnd = new Random();

            var startRow = 3;
            var excel = generateTemplate.GenerateTempalate("Pengiriman_Antar_Area", startRow, 16);
            var sheet = excel[0];

            sheet.Cells[startRow, 1].Value = rnd.Next(1, 10);            
            sheet.Cells[startRow, 2].Value = DateTime.Now.ToString("dd/MM/yyyy");
            sheet.Cells[startRow, 3].Value = "NoSP-" + guid;
            sheet.Cells[startRow, 4].Value = "Konstruksi-" + guid;
            sheet.Cells[startRow, 5].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 6].Value = "Kereta-" + guid;
            sheet.Cells[startRow, 7].Value = "Grade-" + guid;
            sheet.Cells[startRow, 8].Value = "Activity-";
            sheet.Cells[startRow, 9].Value = "SubCon-" + guid;
            sheet.Cells[startRow, 10].Value = "Destination-" + guid;
            sheet.Cells[startRow, 11].Value = "Reprocess-" + guid;
            sheet.Cells[startRow, 12].Value = "Ket-" + guid;
            sheet.Cells[startRow, 13].Value = "Area-" + guid;
            sheet.Cells[startRow, 14].Value = "Keterangan-" + guid;
            sheet.Cells[startRow, 15].Value = "Problem-" + guid;


            return excel;
        }

        private ExcelWorksheets GenerateStockOpnameSheet()
        {
            var guid = Guid.NewGuid().ToString("N").Substring(0, 5);
            Random rnd = new Random();

            var startRow = 3;
            var excel = generateTemplate.GenerateTempalate("Stock_Opname", startRow, 8);
            var sheet = excel[0];

            sheet.Cells[startRow, 1].Value = rnd.Next(1, 10);            
            sheet.Cells[startRow, 2].Value = DateTime.Now.ToString("dd/MM/yyyy");
            sheet.Cells[startRow, 3].Value = "NoSP-" + guid;
            sheet.Cells[startRow, 4].Value = "Material-" + guid;
            sheet.Cells[startRow, 5].Value = "Train-" + guid;
            sheet.Cells[startRow, 6].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 7].Value = "Description-" + guid;

            return excel;
        }

        private ExcelWorksheets GenerateMaterialTroubleSheet()
        {
            var guid = Guid.NewGuid().ToString("N").Substring(0, 5);
            Random rnd = new Random();

            var startRow = 3;
            var excel = generateTemplate.GenerateTempalate("Material_Trouble", startRow, 10);
            var sheet = excel[0];

            sheet.Cells[startRow, 1].Value = rnd.Next(1, 10);            
            sheet.Cells[startRow, 2].Value = DateTime.Now.ToString("dd/MM/yyyy");
            sheet.Cells[startRow, 3].Value = "OrderNo-" + guid;
            sheet.Cells[startRow, 4].Value = "Material-" + guid;
            sheet.Cells[startRow, 5].Value = "Train-" + guid;
            sheet.Cells[startRow, 6].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 7].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 8].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 9].Value = "Problem-" + guid;

            return excel;
        }

        private ExcelWorksheets GenerateStockMaterialSheet()
        {
            var guid = Guid.NewGuid().ToString("N").Substring(0, 5);
            Random rnd = new Random();

            var startRow = 3;
            var excel = generateTemplate.GenerateTempalate("Stock_Material", startRow, 8);
            var sheet = excel[0];

            sheet.Cells[startRow, 1].Value = rnd.Next(1, 10);            
            sheet.Cells[startRow, 2].Value = DateTime.Now.ToString("dd/MM/yyyy");
            sheet.Cells[startRow, 3].Value = "Activity-" + guid;
            sheet.Cells[startRow, 4].Value = "OrderNo-" + guid;
            sheet.Cells[startRow, 5].Value = "Material-" + guid;
            sheet.Cells[startRow, 6].Value = "Train-" + guid;
            sheet.Cells[startRow, 7].Value = rnd.Next(1, 10);            

            return excel;
        }

        [Fact]
        public async Task UploadSheetPretreatmentMachineProductionSuccess()
        {

            var sqlDataContext = new Mock<ISqlDataContext<PretreatmentMachineProductionModel>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<PretreatmentMachineProductionModel>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<PretreatmentMachineProductionModel>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<PretreatmentMachineProductionModel>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(IMachineProductionAdapter))).Returns(new MachineProductionAdapter(serviceProvider.Object));



            UploadExcelPretreatmentService uploadExcelDyeingService = new UploadExcelPretreatmentService(serviceProvider.Object);
            var log = new Mock<ILogger>();

            var excel = GeneratePretreatmentMachineProductionSheet();

            await uploadExcelDyeingService.Upload(excel, DateTime.Now);


            Assert.True(true);
        }

        [Fact]
        public async Task UploadSheetMaterialPreparingSuccess()
        {

            var sqlDataContext = new Mock<ISqlDataContext<IncomingMaterialPreparingModel>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<IncomingMaterialPreparingModel>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<IncomingMaterialPreparingModel>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<IncomingMaterialPreparingModel>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(IMaterialPreparingAdapter))).Returns(new MaterialPreparingAdapter(serviceProvider.Object));



            UploadExcelPretreatmentService uploadExcelDyeingService = new UploadExcelPretreatmentService(serviceProvider.Object);
            var log = new Mock<ILogger>();

            var excel = GenerateMaterialPreparingSheet();

            await uploadExcelDyeingService.Upload(excel, DateTime.Now);


            Assert.True(true);
        }

        [Fact]
        public async Task UploadSheetReprocessSuccess()
        {

            var sqlDataContext = new Mock<ISqlDataContext<ReprocessModel>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<ReprocessModel>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<ReprocessModel>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<ReprocessModel>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(IReprocessAdapter))).Returns(new ReprocessAdapter(serviceProvider.Object));



            UploadExcelPretreatmentService uploadExcelDyeingService = new UploadExcelPretreatmentService(serviceProvider.Object);
            var log = new Mock<ILogger>();

            var excel = GenerateReprocessSheet();

            await uploadExcelDyeingService.Upload(excel, DateTime.Now);


            Assert.True(true);
        }

        [Fact]
        public async Task UploadSheetDeliveryBetweenAreaSuccess()
        {

            var sqlDataContext = new Mock<ISqlDataContext<DeliveryBetweenAreaModel>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<DeliveryBetweenAreaModel>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<DeliveryBetweenAreaModel>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<DeliveryBetweenAreaModel>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(IDeliveryBetweenAdapter))).Returns(new DeliveryBetweenAdapter(serviceProvider.Object));



            UploadExcelPretreatmentService uploadExcelDyeingService = new UploadExcelPretreatmentService(serviceProvider.Object);
            var log = new Mock<ILogger>();

            var excel = GenerateDeliveryBetweenAreaSheet();

            await uploadExcelDyeingService.Upload(excel, DateTime.Now);


            Assert.True(true);
        }

        [Fact]
        public async Task UploadSheetStockOpnameSuccess()
        {

            var sqlDataContext = new Mock<ISqlDataContext<StockOpnameModel>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<StockOpnameModel>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<StockOpnameModel>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<StockOpnameModel>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(IStockOpnameAdapter))).Returns(new StockOpnameAdapter(serviceProvider.Object));



            UploadExcelPretreatmentService uploadExcelDyeingService = new UploadExcelPretreatmentService(serviceProvider.Object);
            var log = new Mock<ILogger>();

            var excel = GenerateStockOpnameSheet();

            await uploadExcelDyeingService.Upload(excel, DateTime.Now);


            Assert.True(true);
        }
        [Fact]
        public async Task UploadSheetMaterialTroubleSuccess()
        {

            var sqlDataContext = new Mock<ISqlDataContext<MaterialTroubleModel>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<MaterialTroubleModel>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<MaterialTroubleModel>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<MaterialTroubleModel>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(IMaterialTroubleAdapter))).Returns(new MaterialTroubleAdapter(serviceProvider.Object));



            UploadExcelPretreatmentService uploadExcelDyeingService = new UploadExcelPretreatmentService(serviceProvider.Object);
            var log = new Mock<ILogger>();

            var excel = GenerateMaterialTroubleSheet();

            await uploadExcelDyeingService.Upload(excel, DateTime.Now);


            Assert.True(true);
        }

        [Fact]
        public async Task UploadSheetStockMaterialSuccess()
        {

            var sqlDataContext = new Mock<ISqlDataContext<StockMaterialModel>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<StockMaterialModel>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<StockMaterialModel>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<StockMaterialModel>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(IStockMaterialAdapter))).Returns(new StockMaterialAdapter(serviceProvider.Object));



            UploadExcelPretreatmentService uploadExcelDyeingService = new UploadExcelPretreatmentService(serviceProvider.Object);
            var log = new Mock<ILogger>();

            var excel = GenerateStockMaterialSheet();

            await uploadExcelDyeingService.Upload(excel, DateTime.Now);


            Assert.True(true);
        }
    }
}
