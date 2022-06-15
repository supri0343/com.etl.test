using Com.Danliris.ETL.Service.DBAdapters.DyeingAdapters;
using Com.Danliris.ETL.Service.ExcelModels.DashboardDyeingModels;
using Com.Danliris.ETL.Service.Services;
using Com.Danliris.ETL.Service.Services.Class;
using Com.Danliris.ETL.Service.Test.DataUtil;
using Com.Danliris.ETL.Service.Tools;
using Microsoft.Extensions.Logging;
using Moq;
using OfficeOpenXml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.ETL.Service.Test.Services
{
    public class UploadExcelDyeingServiceTest
    {
        GenerateTemplateSheet generateTemplateSheet = new GenerateTemplateSheet();
        private ExcelWorksheets GenerateDyeingMachineSheet()
        {
            var guid = Guid.NewGuid().ToString("N").Substring(0, 5);
            Random rnd = new Random();

            var startRow = 3;
            var excel = generateTemplateSheet.GenerateTempalate("MESIN_DYEING", startRow, 14);
            var sheet = excel[0];

            sheet.Cells[startRow, 1].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 2].Value = "Machine-" + guid;
            sheet.Cells[startRow, 3].Value = DateTime.Now.ToString();
            sheet.Cells[startRow, 4].Value = "Shift-" + guid;
            sheet.Cells[startRow, 5].Value = "A";
            sheet.Cells[startRow, 6].Value = "No Oder-" + guid;
            sheet.Cells[startRow, 7].Value = "Material-" + guid;
            sheet.Cells[startRow, 8].Value = "Warna-" + guid;
            sheet.Cells[startRow, 9].Value = "No Kereta" + guid;
            sheet.Cells[startRow, 10].Value = "Jenis Kereta" + guid;
            sheet.Cells[startRow, 11].Value = "Keterangan Proses" + guid;
            sheet.Cells[startRow, 12].Value = rnd.Next(50, 100);
            sheet.Cells[startRow, 13].Value = rnd.Next(100, 2000);
            sheet.Cells[startRow, 14].Value = rnd.Next(100, 2000);

            return excel;
        }

        private ExcelWorksheets GenerateHandoverAreaSheet()
        {
            var guid = Guid.NewGuid().ToString("N").Substring(0, 5);
            Random rnd = new Random();

            var startRow = 3;
            var excel = generateTemplateSheet.GenerateTempalate("SERAH_TERIMA_AREA", startRow, 21);
            var sheet = excel[0];

            sheet.Cells[startRow, 1].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 2].Value = DateTime.Now.ToString();
            sheet.Cells[startRow, 3].Value = "No Order-" + guid;
            sheet.Cells[startRow, 4].Value = "Konstruksi-" + guid;
            sheet.Cells[startRow, 5].Value = rnd.NextDouble();
            sheet.Cells[startRow, 6].Value = "Kereta-" + guid;
            sheet.Cells[startRow, 7].Value = "Grade-" + guid.Substring(0,2);
            sheet.Cells[startRow, 8].Value = "Activity-" + guid;
            sheet.Cells[startRow, 9].Value = "Produksi-" + guid;
            sheet.Cells[startRow, 10].Value = "Asal-" + guid;
            sheet.Cells[startRow, 11].Value = "Reproses-" + guid;
            sheet.Cells[startRow, 12].Value = "Keterangan" + guid;
            sheet.Cells[startRow, 13].Value = "Area-" + guid;
            sheet.Cells[startRow, 14].Value = "Warna-" + guid;
            sheet.Cells[startRow, 15].Value = "Keterangan" + guid;
            sheet.Cells[startRow, 16].Value = rnd.NextDouble();
            sheet.Cells[startRow, 17].Value = rnd.NextDouble();
            sheet.Cells[startRow, 18].Value = rnd.NextDouble();
            sheet.Cells[startRow, 19].Value = rnd.NextDouble();
            sheet.Cells[startRow, 20].Value = rnd.NextDouble();
            sheet.Cells[startRow, 21].Value = rnd.NextDouble();

            return excel;
        }

        private ExcelWorksheets GenerateWipMaterialSheet()
        {
            var guid = Guid.NewGuid().ToString("N").Substring(0, 5);
            Random rnd = new Random();

            var startRow = 3;
            var excel = generateTemplateSheet.GenerateTempalate("WIP_MATERIAL", startRow, 9);
            var sheet = excel[0];

            sheet.Cells[startRow, 1].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 2].Value = DateTime.Now.ToString();
            sheet.Cells[startRow, 3].Value = "Mesin-" + guid;
            sheet.Cells[startRow, 4].Value = "SPF-" + guid;
            sheet.Cells[startRow, 5].Value = "Material-"+guid;
            sheet.Cells[startRow, 6].Value = "Kereta-" + guid;
            sheet.Cells[startRow, 7].Value = "Warna-" + guid;
            sheet.Cells[startRow, 8].Value = rnd.NextDouble();
            sheet.Cells[startRow, 9].Value = "Keterangan Proses-" + guid;

            return excel;
        }

        private ExcelWorksheets GenerateCkDyeingSheet()
        {
            var guid = Guid.NewGuid().ToString("N").Substring(0, 5);
            Random rnd = new Random();

            var startRow = 3;
            var excel = generateTemplateSheet.GenerateTempalate("CK_DYEING", startRow, 12);
            var sheet = excel[0];

            sheet.Cells[startRow, 1].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 2].Value = DateTime.Now.ToString();
            sheet.Cells[startRow, 3].Value = "Mesin-" + guid;
            sheet.Cells[startRow, 4].Value = "Order-" + guid;
            sheet.Cells[startRow, 5].Value = "Konstruksi-" + guid;
            sheet.Cells[startRow, 6].Value = "Color-" + guid;
            sheet.Cells[startRow, 7].Value = "Proses-" + guid;
            sheet.Cells[startRow, 8].Value = rnd.NextDouble();
            sheet.Cells[startRow, 9].Value = rnd.NextDouble();
            sheet.Cells[startRow, 10].Value = "Detail Dyes-" + guid;
            sheet.Cells[startRow, 11].Value = rnd.NextDouble();
            sheet.Cells[startRow, 12].Value = rnd.NextDouble();

            return excel;
        }

        private ExcelWorksheets GenerateLpgMachineUsageSheet()
        {
            var guid = Guid.NewGuid().ToString("N").Substring(0, 5);
            Random rnd = new Random();

            var startRow = 5;
            var excel = generateTemplateSheet.GenerateTempalate("PENGGUNAAN_LPG_MESIN", startRow, 10);
            var sheet = excel[0];

            sheet.Cells[startRow, 1].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 2].Value = rnd.Next(1, 20);
            sheet.Cells[startRow, 3].Value = DateTime.Now.ToString();
            sheet.Cells[startRow, 4].Value = "A";
            sheet.Cells[startRow, 5].Value = "Shift-" + guid;
            sheet.Cells[startRow, 6].Value = rnd.NextDouble();
            sheet.Cells[startRow, 7].Value = rnd.NextDouble();
            sheet.Cells[startRow, 8].Value = rnd.NextDouble();
            sheet.Cells[startRow, 9].Value = rnd.NextDouble();
            sheet.Cells[startRow, 10].Value = rnd.NextDouble();

            return excel;
        }

        #region Success
        [Fact]
        public async Task UploadMachineDyeingSheetSuccess()
        {
            var sqlDataContext = new Mock<ISqlDataContext<DashboardDyeingMachine>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<DashboardDyeingMachine>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<DashboardDyeingMachine>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<DashboardDyeingMachine>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(IDyeingMachineAdapter))).Returns(new DyeingMachineAdapter(serviceProvider.Object));

            UploadExcelDyeingService uploadExcelDyeingService = new UploadExcelDyeingService(serviceProvider.Object);

            var excel = GenerateDyeingMachineSheet();
            await uploadExcelDyeingService.Upload(excel, DateTime.Now);
            Assert.True(true);
        }

        [Fact]
        public async Task UploadHandoverSuccess()
        {
            var sqlDataContext = new Mock<ISqlDataContext<DashboardHandOverArea>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<DashboardHandOverArea>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<DashboardHandOverArea>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<DashboardHandOverArea>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(IHandoverAdapter))).Returns(new HandoverAdapter(serviceProvider.Object));
            
            UploadExcelDyeingService uploadExcelDyeingService = new UploadExcelDyeingService(serviceProvider.Object);

            var excel = GenerateHandoverAreaSheet();
            await uploadExcelDyeingService.Upload(excel, DateTime.Now);
            Assert.True(true);
        }

        [Fact]
        public async Task UploadWipMaterialSuccess()
        {
            var sqlDataContext = new Mock<ISqlDataContext<DashboardWIPMaterial>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<DashboardWIPMaterial>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<DashboardWIPMaterial>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<DashboardWIPMaterial>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(IWipMaterialAdapter))).Returns(new WipMaterialAdapter(serviceProvider.Object));

            UploadExcelDyeingService uploadExcelDyeingService = new UploadExcelDyeingService(serviceProvider.Object);

            var excel = GenerateWipMaterialSheet();
            await uploadExcelDyeingService.Upload(excel, DateTime.Now);
            Assert.True(true);
        }

        [Fact]
        public async Task UploadCkDyeingSuccess()
        {
            var sqlDataContext = new Mock<ISqlDataContext<DashboardCKDyeing>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<DashboardCKDyeing>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<DashboardCKDyeing>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<DashboardCKDyeing>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(ICkDyeingAdapter))).Returns(new CkDyeingAdapter(serviceProvider.Object));

            UploadExcelDyeingService uploadExcelDyeingService = new UploadExcelDyeingService(serviceProvider.Object);

            var excel = GenerateCkDyeingSheet();
            await uploadExcelDyeingService.Upload(excel, DateTime.Now);
            Assert.True(true);
        }

        [Fact]
        public async Task UploadLpgMachineUsageSuccess()
        {
            var sqlDataContext = new Mock<ISqlDataContext<DashboardLPGMachineUsage>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<DashboardLPGMachineUsage>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<DashboardLPGMachineUsage>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<DashboardLPGMachineUsage>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(ILpgMachineUsageAdapter))).Returns(new LpgMachineUsageAdapter(serviceProvider.Object));

            UploadExcelDyeingService uploadExcelDyeingService = new UploadExcelDyeingService(serviceProvider.Object);

            var excel = GenerateLpgMachineUsageSheet();
            await uploadExcelDyeingService.Upload(excel, DateTime.Now);
            Assert.True(true);
        }
        #endregion

        #region Failed
        [Fact]
        public async Task UploadMachineDyeingValidatePeriodeFailed()
        {
            var sqlDataContext = new Mock<ISqlDataContext<DashboardDyeingMachine>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<DashboardDyeingMachine>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<DashboardDyeingMachine>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<DashboardDyeingMachine>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(IDyeingMachineAdapter))).Returns(new DyeingMachineAdapter(serviceProvider.Object));

            UploadExcelDyeingService uploadExcelDyeingService = new UploadExcelDyeingService(serviceProvider.Object);

            var excel = GenerateDyeingMachineSheet();
            Exception message = await Assert.ThrowsAnyAsync<Exception>(() => uploadExcelDyeingService.Upload(excel, DateTime.Now.AddMonths(1)));
            Assert.Equal("Gagal memproses Sheet DYEING_MACHINE pada baris ke-3 - Periode yang dipilih tidak sesuai dengan periode data excel", message.Message);
        }

        [Fact]
        public async Task UploadHandoverValidatePeriodeFailed()
        {
            var sqlDataContext = new Mock<ISqlDataContext<DashboardHandOverArea>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<DashboardHandOverArea>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<DashboardHandOverArea>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<DashboardHandOverArea>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(IHandoverAdapter))).Returns(new HandoverAdapter(serviceProvider.Object));

            UploadExcelDyeingService uploadExcelDyeingService = new UploadExcelDyeingService(serviceProvider.Object);

            var excel = GenerateHandoverAreaSheet();
            Exception message = await Assert.ThrowsAnyAsync<Exception>(() => uploadExcelDyeingService.Upload(excel, DateTime.Now.AddMonths(1)));
            Assert.Equal("Gagal memproses Sheet SERAH_TERIMA_AREA pada baris ke-3 - Periode yang dipilih tidak sesuai dengan periode data excel", message.Message);
        }

        [Fact]
        public async Task UploadWipMaterialValidatePeriodeFailed()
        {
            var sqlDataContext = new Mock<ISqlDataContext<DashboardWIPMaterial>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<DashboardWIPMaterial>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<DashboardWIPMaterial>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<DashboardWIPMaterial>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(IWipMaterialAdapter))).Returns(new WipMaterialAdapter(serviceProvider.Object));

            UploadExcelDyeingService uploadExcelDyeingService = new UploadExcelDyeingService(serviceProvider.Object);

            var excel = GenerateWipMaterialSheet();
            Exception message = await Assert.ThrowsAnyAsync<Exception>(() => uploadExcelDyeingService.Upload(excel, DateTime.Now.AddMonths(1)));
            Assert.Equal("Gagal memproses Sheet WIP_MATERIAL pada baris ke-3 - Periode yang dipilih tidak sesuai dengan periode data excel", message.Message);
        }

        [Fact]
        public async Task UploadCkDyeingValidatePeriodeFailed()
        {
            var sqlDataContext = new Mock<ISqlDataContext<DashboardCKDyeing>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<DashboardCKDyeing>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<DashboardCKDyeing>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<DashboardCKDyeing>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(ICkDyeingAdapter))).Returns(new CkDyeingAdapter(serviceProvider.Object));

            UploadExcelDyeingService uploadExcelDyeingService = new UploadExcelDyeingService(serviceProvider.Object);

            var excel = GenerateCkDyeingSheet();
            Exception message = await Assert.ThrowsAnyAsync<Exception>(() => uploadExcelDyeingService.Upload(excel, DateTime.Now.AddMonths(1)));
            Assert.Equal("Gagal memproses Sheet CK_DYEING pada baris ke-3 - Periode yang dipilih tidak sesuai dengan periode data excel", message.Message);
        }

        [Fact]
        public async Task UploadLpgMachineUsageVallidatePeriodeFailed()
        {
            var sqlDataContext = new Mock<ISqlDataContext<DashboardLPGMachineUsage>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<DashboardLPGMachineUsage>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<DashboardLPGMachineUsage>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<DashboardLPGMachineUsage>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(ILpgMachineUsageAdapter))).Returns(new LpgMachineUsageAdapter(serviceProvider.Object));

            UploadExcelDyeingService uploadExcelDyeingService = new UploadExcelDyeingService(serviceProvider.Object);

            var excel = GenerateLpgMachineUsageSheet();
            Exception message = await Assert.ThrowsAnyAsync<Exception>(() => uploadExcelDyeingService.Upload(excel, DateTime.Now.AddMonths(1)));
            Assert.Equal("Gagal memproses Sheet PENGGUNAAN_LPG_MESIN pada baris ke-5 - Periode yang dipilih tidak sesuai dengan periode data excel", message.Message);
        }

        [Fact]
        public async Task UploadVallidateSheetNameFailed()
        {
            var sqlDataContext = new Mock<ISqlDataContext<DashboardLPGMachineUsage>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<DashboardLPGMachineUsage>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<DashboardLPGMachineUsage>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<DashboardLPGMachineUsage>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(ILpgMachineUsageAdapter))).Returns(new LpgMachineUsageAdapter(serviceProvider.Object));

            UploadExcelDyeingService uploadExcelDyeingService = new UploadExcelDyeingService(serviceProvider.Object);

            var excel = generateTemplateSheet.GenerateTempalate("Test", 3, 14);
            Exception message = await Assert.ThrowsAnyAsync<Exception>(() => uploadExcelDyeingService.Upload(excel, DateTime.Now.AddMonths(1)));
            Assert.Equal("Test tidak valid", message.Message);
        }
        #endregion
    }
}
