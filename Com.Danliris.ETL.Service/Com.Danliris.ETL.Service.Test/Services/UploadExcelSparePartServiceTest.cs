using Com.Danliris.ETL.Service.DBAdapters.GudangSparepartAdapters;
using Com.Danliris.ETL.Service.Models.DashboardGudangSparepartModels;
using Com.Danliris.ETL.Service.Services.Class;
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
    public class UploadExcelSparePartServiceTest
    {
        GenerateTemplateSheet generateTemplateSheet = new GenerateTemplateSheet();
        private ExcelWorksheets GenerateSparePartItemReceiptSheet()
        {
            var guid = Guid.NewGuid().ToString("N").Substring(0, 5);
            Random rnd = new Random();

            var startRow = 3;
            var excel = generateTemplateSheet.GenerateTempalate("penerimaan_barang", startRow, 11);
            var sheet = excel[0];

            sheet.Cells[startRow, 1].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 2].Value = DateTime.Now.ToString();
            sheet.Cells[startRow, 3].Value = "Unit Area-" + guid;
            sheet.Cells[startRow, 4].Value = "No Bon-" + guid;
            sheet.Cells[startRow, 5].Value = "Nama Supplier" + guid;
            sheet.Cells[startRow, 6].Value = "No PO-" + guid;
            sheet.Cells[startRow, 7].Value = "Kode Item-" + guid;
            sheet.Cells[startRow, 8].Value = "Nama Item" + guid;
            sheet.Cells[startRow, 9].Value = rnd.Next(50, 100);
            sheet.Cells[startRow, 10].Value = "Unit" + guid;
            sheet.Cells[startRow, 11].Value = "Deskripsi" + guid;

            return excel;
        }
        private ExcelWorksheets GenerateSparePartItemReleaseSheet()
        {
            var guid = Guid.NewGuid().ToString("N").Substring(0, 5);
            Random rnd = new Random();

            var startRow = 3;
            var excel = generateTemplateSheet.GenerateTempalate("pengeluaran_barang", startRow, 10);
            var sheet = excel[0];

            sheet.Cells[startRow, 1].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 2].Value = DateTime.Now.ToString();
            sheet.Cells[startRow, 3].Value = "Unit Area-" + guid;
            sheet.Cells[startRow, 4].Value = "No Bon-" + guid;
            sheet.Cells[startRow, 5].Value = "Kode Item" + guid;
            sheet.Cells[startRow, 6].Value = rnd.Next(50, 100); 
            sheet.Cells[startRow, 7].Value = "Nama Item-" + guid;
            sheet.Cells[startRow, 8].Value = "Unit" + guid;
            sheet.Cells[startRow, 9].Value = "MC" + guid;
            sheet.Cells[startRow, 10].Value = "Diambil Oleh" + guid;

            return excel;
        }
        private ExcelWorksheets GenerateStockSparePartSheet()
        {
            var guid = Guid.NewGuid().ToString("N").Substring(0, 5);
            Random rnd = new Random();

            var startRow = 3;
            var excel = generateTemplateSheet.GenerateTempalate("stock_spare_part", startRow, 11);
            var sheet = excel[0];

            sheet.Cells[startRow, 1].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 2].Value = DateTime.Now.ToString();
            sheet.Cells[startRow, 3].Value = "Kode Item-" + guid;
            sheet.Cells[startRow, 4].Value = "No Rak-" + guid;
            sheet.Cells[startRow, 5].Value = "Master Kode" + guid;
            sheet.Cells[startRow, 6].Value = "Nama Item-" + guid;
            sheet.Cells[startRow, 7].Value = "Unit" + guid;
            sheet.Cells[startRow, 8].Value = rnd.Next(50, 100);
            sheet.Cells[startRow, 9].Value = rnd.Next(50, 100);
            sheet.Cells[startRow, 10].Value = rnd.Next(50, 100);
            sheet.Cells[startRow, 11].Value = rnd.Next(50, 100);

            return excel;
        }

        #region Success
        [Fact]
        public async Task UploadSparepartItemReceiptSheetSuccess()
        {
            var sqlDataContext = new Mock<ISqlDataContext<SparepartItemReceipt>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<SparepartItemReceipt>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<SparepartItemReceipt>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<SparepartItemReceipt>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(IReceiptItemSparepartAdapter))).Returns(new ReceiptItemSparepartAdapter(serviceProvider.Object));

            UploadExcelSparePartService uploadExcelSparePartService = new UploadExcelSparePartService(serviceProvider.Object);

            var excel = GenerateSparePartItemReceiptSheet();
            await uploadExcelSparePartService.Upload(excel, DateTime.Now);
            Assert.True(true);
        }

        [Fact]
        public async Task UploadSparepartItemReleaseSheetSuccess()
        {
            var sqlDataContext = new Mock<ISqlDataContext<SparepartItemRelease>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<SparepartItemRelease>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<SparepartItemRelease>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<SparepartItemRelease>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(IReleaseItemSparepartAdapter))).Returns(new ReleaseItemSparepartAdapter(serviceProvider.Object));

            UploadExcelSparePartService uploadExcelSparePartService = new UploadExcelSparePartService(serviceProvider.Object);

            var excel = GenerateSparePartItemReleaseSheet();
            await uploadExcelSparePartService.Upload(excel, DateTime.Now);
            Assert.True(true);
        }

        [Fact]
        public async Task UploadStockSparepartSheetSuccess()
        {
            var sqlDataContext = new Mock<ISqlDataContext<StockSparepartModel>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<StockSparepartModel>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<StockSparepartModel>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<StockSparepartModel>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(IStockSparepartAdapter))).Returns(new StockSparepartAdapter(serviceProvider.Object));

            UploadExcelSparePartService uploadExcelSparePartService = new UploadExcelSparePartService(serviceProvider.Object);

            var excel = GenerateStockSparePartSheet();
            await uploadExcelSparePartService.Upload(excel, DateTime.Now);
            Assert.True(true);
        }
        #endregion

        #region Failed
        [Fact]
        public async Task UploadSparepartItemReceiptValidatePeriodeFailed()
        {
            var sqlDataContext = new Mock<ISqlDataContext<SparepartItemReceipt>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<SparepartItemReceipt>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<SparepartItemReceipt>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<SparepartItemReceipt>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(IReceiptItemSparepartAdapter))).Returns(new ReceiptItemSparepartAdapter(serviceProvider.Object));

            UploadExcelSparePartService uploadExcelSparePartService = new UploadExcelSparePartService(serviceProvider.Object);

            var excel = GenerateSparePartItemReceiptSheet();
            Exception message = await Assert.ThrowsAnyAsync<Exception>(() => uploadExcelSparePartService.Upload(excel, DateTime.Now.AddMonths(1)));
            Assert.Equal("Gagal memproses Sheet PENERIMAAN_BARANG pada baris ke-3 - Periode yang dipilih tidak sesuai dengan periode data excel", message.Message);
        }

        [Fact]
        public async Task UploadSparepartItemReleaseValidatePeriodeFailed()
        {
            var sqlDataContext = new Mock<ISqlDataContext<SparepartItemRelease>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<SparepartItemRelease>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<SparepartItemRelease>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<SparepartItemRelease>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(IReleaseItemSparepartAdapter))).Returns(new ReleaseItemSparepartAdapter(serviceProvider.Object));

            UploadExcelSparePartService uploadExcelSparePartService = new UploadExcelSparePartService(serviceProvider.Object);

            var excel = GenerateSparePartItemReleaseSheet();
            Exception message = await Assert.ThrowsAnyAsync<Exception>(() => uploadExcelSparePartService.Upload(excel, DateTime.Now.AddMonths(1)));
            Assert.Equal("Gagal memproses Sheet PENGELUARAN_BARANG pada baris ke-3 - Periode yang dipilih tidak sesuai dengan periode data excel", message.Message);
        }

        [Fact]
        public async Task UploadStockSparepartValidatePeriodeFailed()
        {
            var sqlDataContext = new Mock<ISqlDataContext<StockSparepartModel>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<StockSparepartModel>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<StockSparepartModel>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<StockSparepartModel>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(IStockSparepartAdapter))).Returns(new StockSparepartAdapter(serviceProvider.Object));

            UploadExcelSparePartService uploadExcelSparePartService = new UploadExcelSparePartService(serviceProvider.Object);

            var excel = GenerateStockSparePartSheet();
            Exception message = await Assert.ThrowsAnyAsync<Exception>(() => uploadExcelSparePartService.Upload(excel, DateTime.Now.AddMonths(1)));
            Assert.Equal("Gagal memproses Sheet STOCK_SPARE_PART pada baris ke-3 - Periode yang dipilih tidak sesuai dengan periode data excel", message.Message);
        }

        [Fact]
        public async Task UploadValidateSheetNameFailed()
        {
            var sqlDataContext = new Mock<ISqlDataContext<StockSparepartModel>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<StockSparepartModel>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<StockSparepartModel>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<StockSparepartModel>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(IStockSparepartAdapter))).Returns(new StockSparepartAdapter(serviceProvider.Object));

            UploadExcelSparePartService uploadExcelSparePartService = new UploadExcelSparePartService(serviceProvider.Object);

            var excel = generateTemplateSheet.GenerateTempalate("Test", 3, 11);
            Exception message = await Assert.ThrowsAnyAsync<Exception>(() => uploadExcelSparePartService.Upload(excel, DateTime.Now.AddMonths(1)));
            Assert.Equal("Test tidak valid", message.Message);
        }
        #endregion
    }
}
