using Com.Danliris.ETL.Service.DBAdapters.KoranLaboratAdapters;
using Com.Danliris.ETL.Service.ExcelModels.DashboardKoranLaborat;
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
    public class UploadExcelKoranLaboratServiceTest
    {
        GenerateTemplateSheet generateTemplateSheet = new GenerateTemplateSheet();
        private ExcelWorksheets GenerateDashboardDyeingTestSheet()
        {
            var guid = Guid.NewGuid().ToString("N").Substring(0, 5);
            Random rnd = new Random();

            var startRow = 3;
            var excel = generateTemplateSheet.GenerateTempalate("TEST_DYEING", startRow, 35);
            var sheet = excel[0];

            sheet.Cells[startRow, 1].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 2].Value = DateTime.Now.ToString();
            sheet.Cells[startRow, 3].Value = guid;
            sheet.Cells[startRow, 4].Value = guid;
            sheet.Cells[startRow, 5].Value = guid;
            sheet.Cells[startRow, 6].Value = guid;
            sheet.Cells[startRow, 7].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 8].Value = guid;
            sheet.Cells[startRow, 9].Value = guid;
            sheet.Cells[startRow, 10].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 11].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 12].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 13].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 14].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 15].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 16].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 17].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 18].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 19].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 20].Value = guid;
            sheet.Cells[startRow, 21].Value = guid;
            sheet.Cells[startRow, 22].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 23].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 24].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 25].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 26].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 27].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 28].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 29].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 30].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 31].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 32].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 33].Value = guid;
            sheet.Cells[startRow, 34].Value = guid;
            sheet.Cells[startRow, 35].Value = guid;

            return excel;
        }

        private ExcelWorksheets GenerateDashboardLabDipSheet()
        {
            var guid = Guid.NewGuid().ToString("N").Substring(0, 5);
            Random rnd = new Random();

            var startRow = 3;
            var excel = generateTemplateSheet.GenerateTempalate("LAB_DIP", startRow, 19);
            var sheet = excel[0];

            sheet.Cells[startRow, 1].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 2].Value = DateTime.Now.ToString();
            sheet.Cells[startRow, 3].Value = guid;
            sheet.Cells[startRow, 4].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 5].Value = guid;
            sheet.Cells[startRow, 6].Value = guid;
            sheet.Cells[startRow, 7].Value = guid;
            sheet.Cells[startRow, 8].Value = guid;
            sheet.Cells[startRow, 9].Value = guid;
            sheet.Cells[startRow, 10].Value = guid;
            sheet.Cells[startRow, 11].Value = guid;
            sheet.Cells[startRow, 12].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 13].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 14].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 15].Value = DateTime.Now.ToString();
            sheet.Cells[startRow, 16].Value = DateTime.Now.ToString();
            sheet.Cells[startRow, 17].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 18].Value = guid;
            sheet.Cells[startRow, 19].Value = guid;

            return excel;
        }
        private ExcelWorksheets GenerateDashboardPrintingLabSheet()
        {
            var guid = Guid.NewGuid().ToString("N").Substring(0, 5);
            Random rnd = new Random();

            var startRow = 3;
            var excel = generateTemplateSheet.GenerateTempalate("LAB_PRINTING", startRow, 30);
            var sheet = excel[0];

            sheet.Cells[startRow, 1].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 2].Value = DateTime.Now.ToString();
            sheet.Cells[startRow, 3].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 4].Value = guid;
            sheet.Cells[startRow, 5].Value = guid;
            sheet.Cells[startRow, 6].Value = guid;
            sheet.Cells[startRow, 7].Value = guid;
            sheet.Cells[startRow, 8].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 9].Value = guid;
            sheet.Cells[startRow, 10].Value = DateTime.Now.ToString();
            sheet.Cells[startRow, 11].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 12].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 13].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 14].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 15].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 16].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 17].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 18].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 19].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 20].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 21].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 22].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 23].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 24].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 25].Value = guid;
            sheet.Cells[startRow, 26].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 27].Value = DateTime.Now.ToString();
            sheet.Cells[startRow, 28].Value = guid;
            sheet.Cells[startRow, 29].Value = guid;
            sheet.Cells[startRow, 30].Value = guid;

            return excel;
        }
        private ExcelWorksheets GenerateDashboardPrintingTestSheet()
        {
            var guid = Guid.NewGuid().ToString("N").Substring(0, 5);
            Random rnd = new Random();

            var startRow = 3;
            var excel = generateTemplateSheet.GenerateTempalate("TEST_PRINTING", startRow, 35);
            var sheet = excel[0];

            sheet.Cells[startRow, 1].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 2].Value = DateTime.Now.ToString();
            sheet.Cells[startRow, 3].Value = guid;
            sheet.Cells[startRow, 4].Value = guid;
            sheet.Cells[startRow, 5].Value = guid;
            sheet.Cells[startRow, 6].Value = guid;
            sheet.Cells[startRow, 7].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 8].Value = guid;
            sheet.Cells[startRow, 9].Value = guid;
            sheet.Cells[startRow, 10].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 11].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 12].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 13].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 14].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 15].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 16].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 17].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 18].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 19].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 20].Value = guid;
            sheet.Cells[startRow, 21].Value = guid;
            sheet.Cells[startRow, 22].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 23].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 24].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 25].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 26].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 27].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 28].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 29].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 30].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 31].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 32].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 33].Value = guid;
            sheet.Cells[startRow, 34].Value = guid;
            sheet.Cells[startRow, 35].Value = guid;

            return excel;
        }

        private ExcelWorksheets GenerateRnDTestResultSheet()
        {
            var guid = Guid.NewGuid().ToString("N").Substring(0, 5);
            Random rnd = new Random();

            var startRow = 3;
            var excel = generateTemplateSheet.GenerateTempalate("HASIL_TEST_RND", startRow, 15);
            var sheet = excel[0];

            sheet.Cells[startRow, 1].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 2].Value = DateTime.Now.ToString();
            sheet.Cells[startRow, 3].Value = guid;
            sheet.Cells[startRow, 4].Value = guid;
            sheet.Cells[startRow, 5].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 6].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 7].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 8].Value = guid;
            sheet.Cells[startRow, 9].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 10].Value = guid;
            sheet.Cells[startRow, 11].Value = guid;
            sheet.Cells[startRow, 12].Value = guid;
            sheet.Cells[startRow, 13].Value = guid;
            sheet.Cells[startRow, 14].Value = guid;
            sheet.Cells[startRow, 15].Value = guid;

            return excel;
        }


        #region Success
        [Fact]
        public async Task UploadDashboardDyeingTestSheetSuccess()
        {
            var sqlDataContext = new Mock<ISqlDataContext<DashboardDyeingTest>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<DashboardDyeingTest>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<DashboardDyeingTest>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<DashboardDyeingTest>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(IDyeingTestAdapter))).Returns(new DyeingTestAdapter(serviceProvider.Object));

            UploadExcelKoranLaboratService uploadExcelKoranLaboratService = new UploadExcelKoranLaboratService(serviceProvider.Object);

            var excel = GenerateDashboardDyeingTestSheet();
            await uploadExcelKoranLaboratService.Upload(excel, DateTime.Now);
            Assert.True(true);
        }
        [Fact]
        public async Task UploadDashboardLabDipSheetSuccess()
        {
            var sqlDataContext = new Mock<ISqlDataContext<DashboardLabDip>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<DashboardLabDip>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<DashboardLabDip>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<DashboardLabDip>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(ILabDipAdapter))).Returns(new LabDipAdapter(serviceProvider.Object));

            UploadExcelKoranLaboratService uploadExcelKoranLaboratService = new UploadExcelKoranLaboratService(serviceProvider.Object);

            var excel = GenerateDashboardLabDipSheet();
            await uploadExcelKoranLaboratService.Upload(excel, DateTime.Now);
            Assert.True(true);
        }
        [Fact]
        public async Task UploadDashboardPrintingLabSheetSuccess()
        {
            var sqlDataContext = new Mock<ISqlDataContext<DashboardPrintingLab>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<DashboardPrintingLab>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<DashboardPrintingLab>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<DashboardPrintingLab>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(IPrintingLabAdapter))).Returns(new PrintingLabAdapter(serviceProvider.Object));

            UploadExcelKoranLaboratService uploadExcelKoranLaboratService = new UploadExcelKoranLaboratService(serviceProvider.Object);

            var excel = GenerateDashboardPrintingLabSheet();
            await uploadExcelKoranLaboratService.Upload(excel, DateTime.Now);
            Assert.True(true);
        }
        [Fact]
        public async Task UploadDashboardPrintingTestSheetSuccess()
        {
            var sqlDataContext = new Mock<ISqlDataContext<DashboardPrintingTest>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<DashboardPrintingTest>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<DashboardPrintingTest>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<DashboardPrintingTest>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(IPrintingTestAdapter))).Returns(new PrintingTestAdapter(serviceProvider.Object));

            UploadExcelKoranLaboratService uploadExcelKoranLaboratService = new UploadExcelKoranLaboratService(serviceProvider.Object);

            var excel = GenerateDashboardPrintingTestSheet();
            await uploadExcelKoranLaboratService.Upload(excel, DateTime.Now);
            Assert.True(true);
        }

        [Fact]
        public async Task UploadDashboardRnDTestResultSheetSuccess()
        {
            var sqlDataContext = new Mock<ISqlDataContext<DashboardRnDTestResult>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<DashboardRnDTestResult>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<DashboardRnDTestResult>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<DashboardRnDTestResult>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(IRnDTestResultAdapter))).Returns(new RnDTestResultAdapter(serviceProvider.Object));

            UploadExcelKoranLaboratService uploadExcelKoranLaboratService = new UploadExcelKoranLaboratService(serviceProvider.Object);

            var excel = GenerateRnDTestResultSheet();
            await uploadExcelKoranLaboratService.Upload(excel, DateTime.Now);
            Assert.True(true);
        }
        #endregion


        #region Failed
        [Fact]
        public async Task UploadDashboardDyeingTestValidatePeriodeFailed()
        {
            var sqlDataContext = new Mock<ISqlDataContext<DashboardDyeingTest>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<DashboardDyeingTest>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<DashboardDyeingTest>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<DashboardDyeingTest>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(IDyeingTestAdapter))).Returns(new DyeingTestAdapter(serviceProvider.Object));

            UploadExcelKoranLaboratService uploadExcelKoranLaboratService = new UploadExcelKoranLaboratService(serviceProvider.Object);

            var excel = GenerateDashboardDyeingTestSheet();
            Exception message = await Assert.ThrowsAnyAsync<Exception>(() => uploadExcelKoranLaboratService.Upload(excel, DateTime.Now.AddMonths(1)));
            Assert.Equal("Gagal memproses Sheet Test_Dyeing pada baris ke-3 - Periode yang dipilih tidak sesuai dengan periode data excel", message.Message);
        }
        [Fact]
        public async Task UploadDashboardLabDipValidatePeriodeFailed()
        {
            var sqlDataContext = new Mock<ISqlDataContext<DashboardLabDip>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<DashboardLabDip>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<DashboardLabDip>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<DashboardLabDip>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(ILabDipAdapter))).Returns(new LabDipAdapter(serviceProvider.Object));

            UploadExcelKoranLaboratService uploadExcelKoranLaboratService = new UploadExcelKoranLaboratService(serviceProvider.Object);

            var excel = GenerateDashboardLabDipSheet();
            Exception message = await Assert.ThrowsAnyAsync<Exception>(() => uploadExcelKoranLaboratService.Upload(excel, DateTime.Now.AddMonths(1)));
            Assert.Equal("Gagal memproses Sheet Lab_Dip pada baris ke-3 - Periode yang dipilih tidak sesuai dengan periode data excel", message.Message);
        }
        [Fact]
        public async Task UploadDashboardPrintingLabValidatePeriodeFailed()
        {
            var sqlDataContext = new Mock<ISqlDataContext<DashboardPrintingLab>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<DashboardPrintingLab>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<DashboardPrintingLab>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<DashboardPrintingLab>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(IPrintingLabAdapter))).Returns(new PrintingLabAdapter(serviceProvider.Object));

            UploadExcelKoranLaboratService uploadExcelKoranLaboratService = new UploadExcelKoranLaboratService(serviceProvider.Object);

            var excel = GenerateDashboardPrintingLabSheet();
            Exception message = await Assert.ThrowsAnyAsync<Exception>(() => uploadExcelKoranLaboratService.Upload(excel, DateTime.Now.AddMonths(1)));
            Assert.Equal("Gagal memproses Sheet Lab_Printing pada baris ke-3 - Periode yang dipilih tidak sesuai dengan periode data excel", message.Message);
        }
        [Fact]
        public async Task UploadDashboardPrintingTestValidatePeriodeFailed()
        {
            var sqlDataContext = new Mock<ISqlDataContext<DashboardPrintingTest>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<DashboardPrintingTest>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<DashboardPrintingTest>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<DashboardPrintingTest>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(IPrintingTestAdapter))).Returns(new PrintingTestAdapter(serviceProvider.Object));

            UploadExcelKoranLaboratService uploadExcelKoranLaboratService = new UploadExcelKoranLaboratService(serviceProvider.Object);

            var excel = GenerateDashboardPrintingTestSheet();
            Exception message = await Assert.ThrowsAnyAsync<Exception>(() => uploadExcelKoranLaboratService.Upload(excel, DateTime.Now.AddMonths(1)));
            Assert.Equal("Gagal memproses Sheet Test_Printing pada baris ke-3 - Periode yang dipilih tidak sesuai dengan periode data excel", message.Message);
        }

        [Fact]
        public async Task UploadDashboardRnDTestResultValidatePeriodeFailed()
        {
            var sqlDataContext = new Mock<ISqlDataContext<DashboardRnDTestResult>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<DashboardRnDTestResult>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<DashboardRnDTestResult>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<DashboardRnDTestResult>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(IRnDTestResultAdapter))).Returns(new RnDTestResultAdapter(serviceProvider.Object));

            UploadExcelKoranLaboratService uploadExcelKoranLaboratService = new UploadExcelKoranLaboratService(serviceProvider.Object);

            var excel = GenerateRnDTestResultSheet();
            Exception message = await Assert.ThrowsAnyAsync<Exception>(() => uploadExcelKoranLaboratService.Upload(excel, DateTime.Now.AddMonths(1)));
            Assert.Equal("Gagal memproses Sheet Hasil_test_RnD pada baris ke-3 - Periode yang dipilih tidak sesuai dengan periode data excel", message.Message);
        }

        [Fact]
        public async Task UploadExcelValidateSheetNameFailed()
        {
            var sqlDataContext = new Mock<ISqlDataContext<DashboardRnDTestResult>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<DashboardRnDTestResult>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<DashboardRnDTestResult>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<DashboardRnDTestResult>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(IRnDTestResultAdapter))).Returns(new RnDTestResultAdapter(serviceProvider.Object));

            UploadExcelKoranLaboratService uploadExcelKoranLaboratService = new UploadExcelKoranLaboratService(serviceProvider.Object);

            var excel = generateTemplateSheet.GenerateTempalate("Test", 3, 11);
            Exception message = await Assert.ThrowsAnyAsync<Exception>(() => uploadExcelKoranLaboratService.Upload(excel, DateTime.Now.AddMonths(1)));
            Assert.Equal("Test tidak valid", message.Message);
        }
        #endregion
    }
}
