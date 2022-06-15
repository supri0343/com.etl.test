using Com.Danliris.ETL.Service.DBAdapters.YarnDyeingAdapters;
using Com.Danliris.ETL.Service.ExcelModels.DashboardYarnDyeing;
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
    public class UploadExcelYarnDyeingServiceTest
    {
        GenerateTemplateSheet generateTemplate = new GenerateTemplateSheet();

        private ExcelWorksheets GeneratePermintaanBenangCelupSheet()
        {
            var guid = Guid.NewGuid().ToString("N").Substring(0, 5);
            Random rnd = new Random();

            var startRow = 6;
            var excel = generateTemplate.GenerateTempalate("PERMINTAAN_BENANG_CELUP", startRow, 15);
            var sheet = excel[0];

            sheet.Cells[startRow, 1].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 2].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 3].Value = "NOSP-" + guid;
            sheet.Cells[startRow, 4].Value = "Warna-" + guid;
            sheet.Cells[startRow, 5].Value = "NoReferensi--"+guid;
            sheet.Cells[startRow, 6].Value = "Material-" + guid;
            sheet.Cells[startRow, 7].Value = "Sales-" + guid;
            sheet.Cells[startRow, 8].Value = "Buyer-" + guid;
            sheet.Cells[startRow, 9].Value = DateTime.Now.ToString("dd/MM/yyyy");
            sheet.Cells[startRow, 10].Value = DateTime.Now.ToString("dd/MM/yyyy");
            sheet.Cells[startRow, 11].Value = DateTime.Now.ToString("dd/MM/yyyy");
            sheet.Cells[startRow, 12].Value = "KodeWarna-"+guid;
            sheet.Cells[startRow, 13].Value = DateTime.Now.ToString("dd/MM/yyyy");
            sheet.Cells[startRow, 14].Value = "Keterangan-" + guid;
            sheet.Cells[startRow, 15].Value = "EvaluasiTurun" + guid;
            sheet.Cells[startRow, 16].Value = "Review" + guid;

            return excel;
        }

        private ExcelWorksheets GeneratePermintaanMiniLoomSheet()
        {
            var guid = Guid.NewGuid().ToString("N").Substring(0, 5);
            Random rnd = new Random();

            var startRow = 5;
            var excel = generateTemplate.GenerateTempalate("Permintaan_MiniLoom", startRow, 20);
            var sheet = excel[0];

            sheet.Cells[startRow, 1].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 2].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 3].Value = "NOSP-" + guid;
            sheet.Cells[startRow, 4].Value = "NamaDesain-" + guid;
            sheet.Cells[startRow, 5].Value = "Buyer-" + guid;
            sheet.Cells[startRow, 6].Value = "Konstruksi-" + guid;
            sheet.Cells[startRow, 7].Value = "Material-" + guid;
            sheet.Cells[startRow, 8].Value = rnd.Next(50, 100);
            sheet.Cells[startRow, 9].Value = "Sales-" + guid;
            sheet.Cells[startRow, 10].Value = DateTime.Now.ToString("dd/MM/yyyy");
            sheet.Cells[startRow, 11].Value = DateTime.Now.ToString("dd/MM/yyyy");
            sheet.Cells[startRow, 12].Value = DateTime.Now.ToString("dd/MM/yyyy");
            sheet.Cells[startRow, 13].Value = DateTime.Now.ToString("dd/MM/yyyy");
            sheet.Cells[startRow, 14].Value = DateTime.Now.ToString("dd/MM/yyyy");
            sheet.Cells[startRow, 15].Value = DateTime.Now.ToString("dd/MM/yyyy");
            sheet.Cells[startRow, 16].Value = "Nomor_REF_MINI_LOOM-" + guid;
            sheet.Cells[startRow, 17].Value = rnd.Next(50, 100);
            sheet.Cells[startRow, 18].Value = "Keterangan-" + guid;
            sheet.Cells[startRow, 19].Value = "EvaluasiTurun-" + guid;
            sheet.Cells[startRow, 20].Value = "Review-" + guid;

            return excel;
        }

        private ExcelWorksheets GeneratePermintaanOrderBarangSheet()
        {
            var guid = Guid.NewGuid().ToString("N").Substring(0, 5);
            Random rnd = new Random();

            var startRow = 5;
            var excel = generateTemplate.GenerateTempalate("PERMINTAAN_ORDER_BENANG", startRow, 16);
            var sheet = excel[0];

            sheet.Cells[startRow, 1].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 2].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 3].Value = "NOSP-" + guid;
            sheet.Cells[startRow, 4].Value = "Warna--" + guid;
            sheet.Cells[startRow, 5].Value = "Buyer-" + guid;
            sheet.Cells[startRow, 6].Value = "NO_LAB_DIP-" + guid;
            sheet.Cells[startRow, 7].Value = "MATERIAL-" + guid;
            sheet.Cells[startRow, 8].Value = rnd.Next(50, 100);
            sheet.Cells[startRow, 9].Value = "Sales-" + guid;
            sheet.Cells[startRow, 10].Value = DateTime.Now.ToString("dd/MM/yyyy");
            sheet.Cells[startRow, 11].Value = DateTime.Now.ToString("dd/MM/yyyy");
            sheet.Cells[startRow, 12].Value = DateTime.Now.ToString("dd/MM/yyyy");
            sheet.Cells[startRow, 13].Value = DateTime.Now.ToString("dd/MM/yyyy");
            sheet.Cells[startRow, 14].Value = DateTime.Now.ToString("dd/MM/yyyy");
            sheet.Cells[startRow, 15].Value = "Keterangan" + guid;
            sheet.Cells[startRow, 16].Value = "Review" + guid;

            return excel;
        }

        private ExcelWorksheets GeneratePermintaanOrderFabricYDSheet()
        {
            var guid = Guid.NewGuid().ToString("N").Substring(0, 5);
            Random rnd = new Random();

            var startRow = 5;
            var excel = generateTemplate.GenerateTempalate("Permintaan_Order_Fabric_YD", startRow, 27);
            var sheet = excel[0];

            sheet.Cells[startRow, 1].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 2].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 3].Value = "NamaDesain-" + guid;
            sheet.Cells[startRow, 4].Value = "Buyer-" + guid;
            sheet.Cells[startRow, 5].Value = "NO_REFERENSI_SAMPLE-" + guid;
            sheet.Cells[startRow, 6].Value = "NO_LAB_DIP-" + guid;
            sheet.Cells[startRow, 7].Value = rnd.Next(50, 100);
            sheet.Cells[startRow, 8].Value = "Konstruksi-" + guid;
            sheet.Cells[startRow, 9].Value = "Material-" + guid;
            sheet.Cells[startRow, 10].Value = rnd.Next(50, 100);
            sheet.Cells[startRow, 11].Value = "Sales-" + guid;
            sheet.Cells[startRow, 12].Value = DateTime.Now.ToString("dd/MM/yyyy");
            sheet.Cells[startRow, 13].Value = DateTime.Now.ToString("dd/MM/yyyy");
            sheet.Cells[startRow, 14].Value = DateTime.Now.ToString("dd/MM/yyyy");
            sheet.Cells[startRow, 15].Value = DateTime.Now.ToString("dd/MM/yyyy");
            sheet.Cells[startRow, 16].Value = rnd.Next(50, 100);
            sheet.Cells[startRow, 17].Value = rnd.Next(50, 100);
            sheet.Cells[startRow, 18].Value = rnd.Next(50, 100);
            sheet.Cells[startRow, 19].Value = DateTime.Now.ToString("dd/MM/yyyy");
            sheet.Cells[startRow, 20].Value = DateTime.Now.ToString("dd/MM/yyyy");
            sheet.Cells[startRow, 21].Value = DateTime.Now.ToString("dd/MM/yyyy");
            sheet.Cells[startRow, 22].Value = DateTime.Now.ToString("dd/MM/yyyy");
            sheet.Cells[startRow, 23].Value = DateTime.Now.ToString("dd/MM/yyyy");
            sheet.Cells[startRow, 24].Value = rnd.Next(50, 100);
            sheet.Cells[startRow, 25].Value = "Keterangan-" + guid;
            sheet.Cells[startRow, 26].Value = "Status_Order-" + guid;
            sheet.Cells[startRow, 27].Value = "Review-" + guid;

            return excel;
        }

        [Fact]
        public async Task UploadPermintaanBenangCelupSheetSuccess()
        {
            var sqlDataContext = new Mock<ISqlDataContext<DashboardDyeYarnRequest>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<DashboardDyeYarnRequest>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<DashboardDyeYarnRequest>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<DashboardDyeYarnRequest>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(IDyeYarnRequestAdapter))).Returns(new DyeYarnRequestAdapter(serviceProvider.Object));

            UploadExcelYarnDyeingService uploadExcelAreaQCService = new UploadExcelYarnDyeingService(serviceProvider.Object);

            var excel = GeneratePermintaanBenangCelupSheet();
            await uploadExcelAreaQCService.Upload(excel, DateTime.Now);
            Assert.True(true);
        }

        [Fact]
        public async Task UploadPermintaanMiniLoomSheetSuccess()
        {
            var sqlDataContext = new Mock<ISqlDataContext<DashboardMiniloomRequest>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<DashboardMiniloomRequest>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<DashboardMiniloomRequest>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<DashboardMiniloomRequest>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(IMiniloomRequestAdapter))).Returns(new MiniloomRequestAdapter(serviceProvider.Object));

            UploadExcelYarnDyeingService uploadExcelAreaQCService = new UploadExcelYarnDyeingService(serviceProvider.Object);

            var excel = GeneratePermintaanMiniLoomSheet();
            await uploadExcelAreaQCService.Upload(excel, DateTime.Now);
            Assert.True(true);
        }

        [Fact]
        public async Task UploadPermintaanOrderBarangSheetSuccess()
        {
            var sqlDataContext = new Mock<ISqlDataContext<DashboardYarnOrderRequest>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<DashboardYarnOrderRequest>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<DashboardYarnOrderRequest>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<DashboardYarnOrderRequest>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(IYarnOrderRequestAdapter))).Returns(new YarnOrderRequestAdapter(serviceProvider.Object));

            UploadExcelYarnDyeingService uploadExcelAreaQCService = new UploadExcelYarnDyeingService(serviceProvider.Object);

            var excel = GeneratePermintaanOrderBarangSheet();
            await uploadExcelAreaQCService.Upload(excel, DateTime.Now);
            Assert.True(true);
        }

        [Fact]
        public async Task UploadPermintaanFabricYDSheetSuccess()
        {
            var sqlDataContext = new Mock<ISqlDataContext<DashboardFabricYDOrderRequest>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<DashboardFabricYDOrderRequest>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<DashboardFabricYDOrderRequest>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<DashboardFabricYDOrderRequest>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(IFabricYDOrderRequestAdapter))).Returns(new FabricYDOrderRequestAdapter(serviceProvider.Object));

            UploadExcelYarnDyeingService uploadExcelAreaQCService = new UploadExcelYarnDyeingService(serviceProvider.Object);

            var excel = GeneratePermintaanOrderFabricYDSheet();
            await uploadExcelAreaQCService.Upload(excel, DateTime.Now);
            Assert.True(true);
        }
    }
}
