using Com.Danliris.ETL.Service.DBAdapters.FinishingAdapters;
using Com.Danliris.ETL.Service.ExcelModels;
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
    public class UploadExcelFinishingServiceTest
    {
        GenerateTemplateSheet generateTemplateSheet = new GenerateTemplateSheet();
        private ExcelWorksheets GenerateDashboardChemicalFSheet()
        {
            var guid = Guid.NewGuid().ToString("N").Substring(0, 5);
            Random rnd = new Random();

            var startRow = 3;
            var excel = generateTemplateSheet.GenerateTempalate("Chemical_F.", startRow, 12);
            var sheet = excel[0];

            sheet.Cells[startRow, 1].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 2].Value = DateTime.Now.ToString();
            sheet.Cells[startRow, 3].Value = "Mesin-" + guid;
            sheet.Cells[startRow, 4].Value = "Tipe Order-" + guid;
            sheet.Cells[startRow, 5].Value = "No Order-" + guid;
            sheet.Cells[startRow, 6].Value = "Konstruksi-" + guid;
            sheet.Cells[startRow, 7].Value = "Color-" + guid;
            sheet.Cells[startRow, 8].Value = "Tipe Proses-" + guid;
            sheet.Cells[startRow, 9].Value = rnd.Next(50, 100);
            sheet.Cells[startRow, 10].Value = rnd.Next(50, 100);
            sheet.Cells[startRow, 11].Value = "Tipe Chemical-" + guid;
            sheet.Cells[startRow, 12].Value = rnd.Next(50, 100);

            return excel;
        }
        private ExcelWorksheets GenerateDashboardProductionFSheet()
        {
            var guid = Guid.NewGuid().ToString("N").Substring(0, 5);
            Random rnd = new Random();

            var startRow = 3;
            var excel = generateTemplateSheet.GenerateTempalate("Produksi_F.", startRow, 14);
            var sheet = excel[0];

            sheet.Cells[startRow, 1].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 2].Value = "Mesin-" + guid;
            sheet.Cells[startRow, 3].Value = DateTime.Now.ToString();
            sheet.Cells[startRow, 4].Value = "Shift-" + guid;
            sheet.Cells[startRow, 5].Value = "No Order-" + guid;
            sheet.Cells[startRow, 6].Value = "Material-" + guid;
            sheet.Cells[startRow, 7].Value = "No Kereta-" + guid;
            sheet.Cells[startRow, 8].Value = "Tipe Proses-" + guid;
            sheet.Cells[startRow, 9].Value = "Deskripsi Proses-" + guid;
            sheet.Cells[startRow, 10].Value = rnd.Next(50, 100);
            sheet.Cells[startRow, 11].Value = rnd.Next(50, 100);
            sheet.Cells[startRow, 12].Value = "Mulai-" + guid;
            sheet.Cells[startRow, 13].Value = "Berhenti-" + guid;
            sheet.Cells[startRow, 14].Value = "Durasi-" + guid;

            return excel;
        }
        private ExcelWorksheets GenerateDashboardReceiveFSheet()
        {
            var guid = Guid.NewGuid().ToString("N").Substring(0, 5);
            Random rnd = new Random();

            var startRow = 3;
            var excel = generateTemplateSheet.GenerateTempalate("Terima_F.", startRow, 9);
            var sheet = excel[0];

            sheet.Cells[startRow, 1].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 2].Value = DateTime.Now.ToString();
            sheet.Cells[startRow, 3].Value = "SPNo-" + guid;
            sheet.Cells[startRow, 4].Value = "Konstruksi-" + guid;
            sheet.Cells[startRow, 5].Value = "No Kereta-" + guid;
            sheet.Cells[startRow, 6].Value = rnd.Next(50, 100);
            sheet.Cells[startRow, 7].Value = "Grade-" + guid;
            sheet.Cells[startRow, 8].Value = "Deskripsi-" + guid;
            sheet.Cells[startRow, 9].Value = "Asal-" + guid;

            return excel;
        }
        private ExcelWorksheets GenerateDashboardReprocessFSheet()
        {
            var guid = Guid.NewGuid().ToString("N").Substring(0, 5);
            Random rnd = new Random();

            var startRow = 3;
            var excel = generateTemplateSheet.GenerateTempalate("Reproses_F.", startRow, 10);
            var sheet = excel[0];

            sheet.Cells[startRow, 1].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 2].Value = "SPNo-" + guid; ;
            sheet.Cells[startRow, 3].Value = DateTime.Now.ToString();
            sheet.Cells[startRow, 4].Value = "No Kereta-" + guid;
            sheet.Cells[startRow, 5].Value = "Material-" + guid;
            sheet.Cells[startRow, 6].Value = "Kode-" + guid;
            sheet.Cells[startRow, 7].Value = "Reproses-" + guid;
            sheet.Cells[startRow, 8].Value = rnd.Next(50, 100);
            sheet.Cells[startRow, 9].Value = "Informasi-" + guid;
            sheet.Cells[startRow, 10].Value = "Area Tujuan-" + guid;

            return excel;
        }
        private ExcelWorksheets GenerateDashboardStockOpnameFSheet()
        {
            var guid = Guid.NewGuid().ToString("N").Substring(0, 5);
            Random rnd = new Random();

            var startRow = 3;
            var excel = generateTemplateSheet.GenerateTempalate("Stock_Opname_F.", startRow, 7);
            var sheet = excel[0];

            sheet.Cells[startRow, 1].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 2].Value = DateTime.Now.ToString();
            sheet.Cells[startRow, 3].Value = "SPNo-" + guid;
            sheet.Cells[startRow, 4].Value = "Material-" + guid;
            sheet.Cells[startRow, 5].Value = "No Kereta-" + guid;
            sheet.Cells[startRow, 6].Value = rnd.Next(50, 100);
            sheet.Cells[startRow, 7].Value = "Deskripsi-" + guid;

            return excel;
        }
        private ExcelWorksheets GenerateDashboardTransferFSheet()
        {
            var guid = Guid.NewGuid().ToString("N").Substring(0, 5);
            Random rnd = new Random();

            var startRow = 3;
            var excel = generateTemplateSheet.GenerateTempalate("Penyerahan_F.", startRow, 9);
            var sheet = excel[0];

            sheet.Cells[startRow, 1].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 2].Value = DateTime.Now.ToString();
            sheet.Cells[startRow, 3].Value = "SPNo-" + guid;
            sheet.Cells[startRow, 4].Value = "Konstruksi-" + guid;
            sheet.Cells[startRow, 5].Value = "No Kereta-" + guid;
            sheet.Cells[startRow, 6].Value = rnd.Next(50, 100);
            sheet.Cells[startRow, 7].Value = "Grade-" + guid;
            sheet.Cells[startRow, 8].Value = "Deskripsi-" + guid;
            sheet.Cells[startRow, 9].Value = "Tujuan-" + guid;

            return excel;
        }

        #region Success
        [Fact]
        public async Task UploadDashboarChemicalFSheetSuccess()
        {
            var sqlDataContext = new Mock<ISqlDataContext<DashboardChemicalF>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<DashboardChemicalF>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<DashboardChemicalF>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<DashboardChemicalF>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(IChemicalAdapter))).Returns(new ChemicalAdapter(serviceProvider.Object));

            UploadExcelFinishingService uploadExcelFinishingService = new UploadExcelFinishingService(serviceProvider.Object);

            var excel = GenerateDashboardChemicalFSheet();
            await uploadExcelFinishingService.Upload(excel, DateTime.Now);
            Assert.True(true);
        }
        [Fact]
        public async Task UploadDashboarReceiveFSheetSuccess()
        {
            var sqlDataContext = new Mock<ISqlDataContext<DashboardReceiveF>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<DashboardReceiveF>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<DashboardReceiveF>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<DashboardReceiveF>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(IReceiveAdapter))).Returns(new ReceiveAdapter(serviceProvider.Object));

            UploadExcelFinishingService uploadExcelFinishingService = new UploadExcelFinishingService(serviceProvider.Object);

            var excel = GenerateDashboardReceiveFSheet();
            await uploadExcelFinishingService.Upload(excel, DateTime.Now);
            Assert.True(true);
        }
        [Fact]
        public async Task UploadDashboarProductioneFSheetSuccess()
        {
            var sqlDataContext = new Mock<ISqlDataContext<DashboardProductionF>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<DashboardProductionF>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<DashboardProductionF>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<DashboardProductionF>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(IProductionAdapter))).Returns(new ProductionAdapter(serviceProvider.Object));

            UploadExcelFinishingService uploadExcelFinishingService = new UploadExcelFinishingService(serviceProvider.Object);

            var excel = GenerateDashboardProductionFSheet();
            await uploadExcelFinishingService.Upload(excel, DateTime.Now);
            Assert.True(true);
        }
        [Fact]
        public async Task UploadDashboarReprocessFSheetSuccess()
        {
            var sqlDataContext = new Mock<ISqlDataContext<DashboardReprocessF>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<DashboardReprocessF>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<DashboardReprocessF>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<DashboardReprocessF>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(IReprocessAdapter))).Returns(new ReprocessAdapter(serviceProvider.Object));

            UploadExcelFinishingService uploadExcelFinishingService = new UploadExcelFinishingService(serviceProvider.Object);

            var excel = GenerateDashboardReprocessFSheet();
            await uploadExcelFinishingService.Upload(excel, DateTime.Now);
            Assert.True(true);
        }
        [Fact]
        public async Task UploadDashboarStockOpnameFSheetSuccess()
        {
            var sqlDataContext = new Mock<ISqlDataContext<DashboardStockOpnameF>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<DashboardStockOpnameF>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<DashboardStockOpnameF>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<DashboardStockOpnameF>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(IStockOpnameAdapter))).Returns(new StockOpnameAdapter(serviceProvider.Object));

            UploadExcelFinishingService uploadExcelFinishingService = new UploadExcelFinishingService(serviceProvider.Object);

            var excel = GenerateDashboardStockOpnameFSheet();
            await uploadExcelFinishingService.Upload(excel, DateTime.Now);
            Assert.True(true);
        }
        [Fact]
        public async Task UploadDashboarTransferFSheetSuccess()
        {
            var sqlDataContext = new Mock<ISqlDataContext<DashboardTransferF>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<DashboardTransferF>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<DashboardTransferF>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<DashboardTransferF>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(ITransferAdapter))).Returns(new TransferAdapter(serviceProvider.Object));

            UploadExcelFinishingService uploadExcelFinishingService = new UploadExcelFinishingService(serviceProvider.Object);

            var excel = GenerateDashboardTransferFSheet();
            await uploadExcelFinishingService.Upload(excel, DateTime.Now);
            Assert.True(true);
        }
        #endregion
        #region Failed
        [Fact]
        public async Task UploadDashboarChemicalFValidatePeriodeFailed()
        {
            var sqlDataContext = new Mock<ISqlDataContext<DashboardChemicalF>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<DashboardChemicalF>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<DashboardChemicalF>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<DashboardChemicalF>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(IChemicalAdapter))).Returns(new ChemicalAdapter(serviceProvider.Object));

            UploadExcelFinishingService uploadExcelFinishingService = new UploadExcelFinishingService(serviceProvider.Object);

            var excel = GenerateDashboardChemicalFSheet();
            Exception message = await Assert.ThrowsAnyAsync<Exception>(() => uploadExcelFinishingService.Upload(excel, DateTime.Now.AddMonths(1)));
            Assert.Equal("Gagal memproses Sheet Chemical_F pada baris ke-3 - Periode yang dipilih tidak sesuai dengan periode data excel", message.Message);
        }
        [Fact]
        public async Task UploadDashboarReceiveFValidatePeriodeFailed()
        {
            var sqlDataContext = new Mock<ISqlDataContext<DashboardReceiveF>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<DashboardReceiveF>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<DashboardReceiveF>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<DashboardReceiveF>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(IReceiveAdapter))).Returns(new ReceiveAdapter(serviceProvider.Object));

            UploadExcelFinishingService uploadExcelFinishingService = new UploadExcelFinishingService(serviceProvider.Object);

            var excel = GenerateDashboardReceiveFSheet();
            Exception message = await Assert.ThrowsAnyAsync<Exception>(() => uploadExcelFinishingService.Upload(excel, DateTime.Now.AddMonths(1)));
            Assert.Equal("Gagal memproses Sheet Terima_F pada baris ke-3 - Periode yang dipilih tidak sesuai dengan periode data excel", message.Message);
        }
        [Fact]
        public async Task UploadDashboarProductioneFValidatePeriodeFailed()
        {
            var sqlDataContext = new Mock<ISqlDataContext<DashboardProductionF>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<DashboardProductionF>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<DashboardProductionF>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<DashboardProductionF>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(IProductionAdapter))).Returns(new ProductionAdapter(serviceProvider.Object));

            UploadExcelFinishingService uploadExcelFinishingService = new UploadExcelFinishingService(serviceProvider.Object);

            var excel = GenerateDashboardProductionFSheet();
            Exception message = await Assert.ThrowsAnyAsync<Exception>(() => uploadExcelFinishingService.Upload(excel, DateTime.Now.AddMonths(1)));
            Assert.Equal("Gagal memproses Sheet Produksi_F pada baris ke-3 - Periode yang dipilih tidak sesuai dengan periode data excel", message.Message);
        }
        [Fact]
        public async Task UploadDashboarReprocessFValidatePeriodeFailed()
        {
            var sqlDataContext = new Mock<ISqlDataContext<DashboardReprocessF>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<DashboardReprocessF>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<DashboardReprocessF>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<DashboardReprocessF>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(IReprocessAdapter))).Returns(new ReprocessAdapter(serviceProvider.Object));

            UploadExcelFinishingService uploadExcelFinishingService = new UploadExcelFinishingService(serviceProvider.Object);

            var excel = GenerateDashboardReprocessFSheet();
            Exception message = await Assert.ThrowsAnyAsync<Exception>(() => uploadExcelFinishingService.Upload(excel, DateTime.Now.AddMonths(1)));
            Assert.Equal("Gagal memproses Sheet Reproses_F pada baris ke-3 - Periode yang dipilih tidak sesuai dengan periode data excel", message.Message);
        }
        [Fact]
        public async Task UploadDashboarStockOpnameFValidatePeriodeFailed()
        {
            var sqlDataContext = new Mock<ISqlDataContext<DashboardStockOpnameF>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<DashboardStockOpnameF>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<DashboardStockOpnameF>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<DashboardStockOpnameF>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(IStockOpnameAdapter))).Returns(new StockOpnameAdapter(serviceProvider.Object));

            UploadExcelFinishingService uploadExcelFinishingService = new UploadExcelFinishingService(serviceProvider.Object);

            var excel = GenerateDashboardStockOpnameFSheet();
            Exception message = await Assert.ThrowsAnyAsync<Exception>(() => uploadExcelFinishingService.Upload(excel, DateTime.Now.AddMonths(1)));
            Assert.Equal("Gagal memproses Sheet Stock_Opname_F pada baris ke-3 - Periode yang dipilih tidak sesuai dengan periode data excel", message.Message);
        }
        [Fact]
        public async Task UploadDashboarTransferFValidatePeriodeFailed()
        {
            var sqlDataContext = new Mock<ISqlDataContext<DashboardTransferF>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<DashboardTransferF>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<DashboardTransferF>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<DashboardTransferF>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(ITransferAdapter))).Returns(new TransferAdapter(serviceProvider.Object));

            UploadExcelFinishingService uploadExcelFinishingService = new UploadExcelFinishingService(serviceProvider.Object);

            var excel = GenerateDashboardTransferFSheet();
            Exception message = await Assert.ThrowsAnyAsync<Exception>(() => uploadExcelFinishingService.Upload(excel, DateTime.Now.AddMonths(1)));
            Assert.Equal("Gagal memproses Sheet Penyerahan_F pada baris ke-3 - Periode yang dipilih tidak sesuai dengan periode data excel", message.Message);
        }
        #endregion
    }
}
