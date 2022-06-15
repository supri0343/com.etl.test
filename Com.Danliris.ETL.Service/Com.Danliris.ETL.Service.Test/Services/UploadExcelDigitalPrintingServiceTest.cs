using Castle.Core.Logging;
using Com.Danliris.ETL.Service.DBAdapters.DigitalPrintingAdapters;
using Com.Danliris.ETL.Service.ExcelModels;
using Com.Danliris.ETL.Service.Services;
using Com.Danliris.ETL.Service.Services.Class;
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
    public class UploadExcelDigitalPrintingServiceTest
    {
        private ExcelWorksheets GenerateTemplateSheet(string name, int endRow, int endCol)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage excelPackage = new ExcelPackage();
            ExcelWorksheets excel = excelPackage.Workbook.Worksheets;
            excel.Add(name);
            var sheet = excel[0];
            for (int row = 1; row < endRow; row++)
            {
                for (int col = 1; col <= 14; col++)
                {
                    sheet.Cells[row, col].Value = name;
                }
            }

            return excel;
        }

        private ExcelWorksheets GenerateProduksiDigitalTransferSheet()
        {
            var guid = Guid.NewGuid().ToString("N").Substring(0, 5);
            Random rnd = new Random();

            var startRow = 3;
            var excel = GenerateTemplateSheet("PRODUKSI_DIGITAL_TRANSFER", startRow, 18);
            var sheet = excel[0];

            sheet.Cells[startRow, 1].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 2].Value = DateTime.Now.ToString("dd/MM/yyyy");            
            sheet.Cells[startRow, 3].Value = "A";
            sheet.Cells[startRow, 4].Value = "Group-" + guid;
            sheet.Cells[startRow, 5].Value = "NoSPP-" + guid;
            sheet.Cells[startRow, 6].Value = "JenisProses-" + guid;
            sheet.Cells[startRow, 7].Value = "Material-" + guid ;
            sheet.Cells[startRow, 8].Value = "Warna_C/W-" + guid;            
            sheet.Cells[startRow, 9].Value = "Kode_Motif-" + guid;            
            sheet.Cells[startRow, 10].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 11].Value = DateTime.Now.ToString();
            sheet.Cells[startRow, 12].Value = DateTime.Now.ToString();
            sheet.Cells[startRow, 13].Value = DateTime.Now.ToString();
            sheet.Cells[startRow, 14].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 15].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 16].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 17].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 18].Value = "KeteranganBS-" + guid;

            return excel;
        }

        private ExcelWorksheets GenerateOrderMasukSheet()
        {
            var guid = Guid.NewGuid().ToString("N").Substring(0, 5);
            Random rnd = new Random();

            var startRow = 3;
            var excel = GenerateTemplateSheet("ORDER_MASUK", startRow, 21);
            var sheet = excel[0];

            sheet.Cells[startRow, 1].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 2].Value = DateTime.Now.ToString("dd/MM/yyyy");
            sheet.Cells[startRow, 3].Value = "SPP" + guid;
            sheet.Cells[startRow, 4].Value = "DESIGN-" + guid;
            sheet.Cells[startRow, 5].Value = "JenisProses-" + guid;
            sheet.Cells[startRow, 6].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 7].Value = "Material-" + guid;
            sheet.Cells[startRow, 8].Value = "No_Benang-" + guid;
            sheet.Cells[startRow, 9].Value = "Lebar-" + guid;
            sheet.Cells[startRow, 10].Value = "Motif-" + guid;
            sheet.Cells[startRow, 11].Value = "Acuan-" + guid;
            sheet.Cells[startRow, 12].Value = "Seri-" + guid;
            sheet.Cells[startRow, 13].Value = "Buyer-" + guid;
            sheet.Cells[startRow, 14].Value = "Asal_Material-" + guid;
            sheet.Cells[startRow, 15].Value = DateTime.Now.ToString("dd/MM/yyyy");
            sheet.Cells[startRow, 16].Value = DateTime.Now.ToString("dd/MM/yyyy");
            sheet.Cells[startRow, 17].Value = DateTime.Now.ToString("dd/MM/yyyy");
            sheet.Cells[startRow, 18].Value = DateTime.Now.ToString("dd/MM/yyyy");
            sheet.Cells[startRow, 19].Value = DateTime.Now.ToString("dd/MM/yyyy");
            sheet.Cells[startRow, 20].Value = "KeteranganBS-" + guid;
            sheet.Cells[startRow, 21].Value = rnd.Next(1, 10);

            return excel;
        }

        private ExcelWorksheets GenerateOrderSODigitalSheet()
        {
            var guid = Guid.NewGuid().ToString("N").Substring(0, 5);
            Random rnd = new Random();

            var startRow = 3;
            var excel = GenerateTemplateSheet("ORDER_SO_DIGITAL_TRANSFER", startRow, 17);
            var sheet = excel[0];

            sheet.Cells[startRow, 1].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 2].Value = DateTime.Now.ToString("dd/MM/yyyy");
            sheet.Cells[startRow, 3].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 4].Value = "Motif-" + guid;
            sheet.Cells[startRow, 5].Value = "Jenis_Proses-" + guid;
            sheet.Cells[startRow, 6].Value = "Jumlah_Serian-" + guid;
            sheet.Cells[startRow, 7].Value = "Jenis_Design-" + guid;
            sheet.Cells[startRow, 8].Value = "Ukuran_Design-" + guid;
            sheet.Cells[startRow, 9].Value = "Buyer-" + guid;
            sheet.Cells[startRow, 10].Value = "Material-" + guid;
            sheet.Cells[startRow, 11].Value = "Bentuk_Design-" + guid;
            sheet.Cells[startRow, 12].Value = "RUN-" + guid;                        
            sheet.Cells[startRow, 13].Value = DateTime.Now.ToString("dd/MM/yyyy");
            sheet.Cells[startRow, 14].Value = DateTime.Now.ToString("dd/MM/yyyy");            
            sheet.Cells[startRow, 15].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 16].Value = "Status-" + guid;
            sheet.Cells[startRow, 17].Value = "Keterangan-" + guid;
            return excel;
        }

        private ExcelWorksheets GenerateWIPAreaSheet()
        {
            var guid = Guid.NewGuid().ToString("N").Substring(0, 5);
            Random rnd = new Random();

            var startRow = 3;
            var excel = GenerateTemplateSheet("WIP_AREA", startRow, 11);
            var sheet = excel[0];

            sheet.Cells[startRow, 1].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 2].Value = DateTime.Now.ToString("dd/MM/yyyy");
            sheet.Cells[startRow, 3].Value = "No_SP-";
            sheet.Cells[startRow, 4].Value = "Material-" + guid;
            sheet.Cells[startRow, 5].Value = "Kereta-" + guid;            
            sheet.Cells[startRow, 6].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 7].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 8].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 9].Value = "Kegiatan-" + guid;
            sheet.Cells[startRow, 10].Value = "Tujuan/Dari-" + guid;
            
            return excel;
        }

        private ExcelWorksheets GeneratePengirimanHarianSheet()
        {
            var guid = Guid.NewGuid().ToString("N").Substring(0, 5);
            Random rnd = new Random();

            var startRow = 3;
            var excel = GenerateTemplateSheet("Pengiriman_Harian", startRow, 14);
            var sheet = excel[0];

            sheet.Cells[startRow, 1].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 2].Value = DateTime.Now.ToString("dd/MM/yyyy");
            sheet.Cells[startRow, 3].Value = "No_SP-";
            sheet.Cells[startRow, 4].Value = "Motif-" + guid;
            sheet.Cells[startRow, 5].Value = "Material-" + guid;
            sheet.Cells[startRow, 6].Value = "Jenis_Proses-" + guid;
            sheet.Cells[startRow, 7].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 8].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 9].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 10].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 11].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 12].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 13].Value = "KeteranganBS-" + guid;
            sheet.Cells[startRow, 14].Value = rnd.Next(1, 10);

            return excel;
        }

        [Fact]
        public async Task UploadSheetProduksiDigitalTransferSuccess()
        {

            var sqlDataContext = new Mock<ISqlDataContext<ProductionDigitalTransfer>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<ProductionDigitalTransfer>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<ProductionDigitalTransfer>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<ProductionDigitalTransfer>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(IProductionDigitalTransferAdapter))).Returns(new ProductionDigitalTransferAdapters(serviceProvider.Object));



            UploadExcelDigitalPrintService uploadExcelDyeingService = new UploadExcelDigitalPrintService(serviceProvider.Object);
            var log = new Mock<ILogger>();

            var excel = GenerateProduksiDigitalTransferSheet();

            await uploadExcelDyeingService.Upload(excel, DateTime.Now);


            Assert.True(true);
        }

        [Fact]
        public async Task UploadSheetOrderMasukSuccess()
        {

            var sqlDataContext = new Mock<ISqlDataContext<OrderIn>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<OrderIn>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<OrderIn>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<OrderIn>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(IOrderInAdapter))).Returns(new OrderInAdapters(serviceProvider.Object));



            UploadExcelDigitalPrintService uploadExcelDyeingService = new UploadExcelDigitalPrintService(serviceProvider.Object);
            var log = new Mock<ILogger>();

            var excel = GenerateOrderMasukSheet();

            await uploadExcelDyeingService.Upload(excel, DateTime.Now);


            Assert.True(true);
        }
        [Fact]
        public async Task UploadSheetOrderSODigitalSuccess()
        {

            var sqlDataContext = new Mock<ISqlDataContext<SOOrderDigitalTransfer>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<SOOrderDigitalTransfer>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<SOOrderDigitalTransfer>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<SOOrderDigitalTransfer>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(ISOOrderDigitalTransferAdapter))).Returns(new SOOrderDigitalTransferAdapters(serviceProvider.Object));



            UploadExcelDigitalPrintService uploadExcelDyeingService = new UploadExcelDigitalPrintService(serviceProvider.Object);
            var log = new Mock<ILogger>();

            var excel = GenerateOrderSODigitalSheet();

            await uploadExcelDyeingService.Upload(excel, DateTime.Now);


            Assert.True(true);
        }

        [Fact]
        public async Task UploadSheetWIPAreaSuccess()
        {

            var sqlDataContext = new Mock<ISqlDataContext<WipArea>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<WipArea>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<WipArea>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<WipArea>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(IWipAreaAdapter))).Returns(new WipAreaAdapters(serviceProvider.Object));



            UploadExcelDigitalPrintService uploadExcelDyeingService = new UploadExcelDigitalPrintService(serviceProvider.Object);
            var log = new Mock<ILogger>();

            var excel = GenerateWIPAreaSheet();

            await uploadExcelDyeingService.Upload(excel, DateTime.Now);


            Assert.True(true);
        }

        [Fact]
        public async Task UploadSheetPengirimanHarianSuccess()
        {

            var sqlDataContext = new Mock<ISqlDataContext<DailyDelivery>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<DailyDelivery>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<DailyDelivery>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<DailyDelivery>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(IDailyDeliveryAdapter))).Returns(new DailyDeliveryAdapters(serviceProvider.Object));



            UploadExcelDigitalPrintService uploadExcelDyeingService = new UploadExcelDigitalPrintService(serviceProvider.Object);
            var log = new Mock<ILogger>();

            var excel = GeneratePengirimanHarianSheet();

            await uploadExcelDyeingService.Upload(excel, DateTime.Now);


            Assert.True(true);
        }


    }
}
