using Com.Danliris.ETL.Service.Models.DashboardMaintenanceModels;
using Com.Danliris.ETL.Service.Test.DataUtil;
using Com.Danliris.ETL.Service.DBAdapters.MaintenanceAdapters;
using Com.Danliris.ETL.Service.Tools;
using Moq;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Com.Danliris.ETL.Service.Services.Class;

namespace Com.Danliris.ETL.Service.Test.Services
{
    public class UploadExcelMaintenanceServiceTest
    {
        GenerateTemplateSheet generateTemplateSheet = new GenerateTemplateSheet();
        private ExcelWorksheets GenerateHandoverSheet()
        {
            var guid = Guid.NewGuid().ToString("N").Substring(0, 5);
            Random rnd = new Random();

            var startRow = 4;
            var excel = generateTemplateSheet.GenerateTempalate("serah_terima", startRow, 10);
            var sheet = excel[0];

            sheet.Cells[startRow, 1].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 2].Value = DateTime.Now.ToString();
            sheet.Cells[startRow, 3].Value = "Machine-" + guid;
            sheet.Cells[startRow, 4].Value = "Repair-" + guid;
            sheet.Cells[startRow, 5].Value = "Result" + guid;
            sheet.Cells[startRow, 6].Value = "Team-" + guid;
            sheet.Cells[startRow, 7].Value = "Implementor-" + guid;
            sheet.Cells[startRow, 8].Value = "Kasubsie Verification" + guid;
            sheet.Cells[startRow, 9].Value = "Kasie Verification" + guid;
            sheet.Cells[startRow, 10].Value = "Productino" + guid;

            return excel;
        }
        private ExcelWorksheets GenerateLKMSheet()
        {
            var guid = Guid.NewGuid().ToString("N").Substring(0, 5);
            Random rnd = new Random();

            var startRow = 4;
            var excel = generateTemplateSheet.GenerateTempalate("lkm", startRow, 14);
            var sheet = excel[0];

            sheet.Cells[startRow, 1].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 2].Value = DateTime.Now.ToString();
            sheet.Cells[startRow, 3].Value = "Section-" + guid;
            sheet.Cells[startRow, 4].Value = "Machine-" + guid;
            sheet.Cells[startRow, 5].Value = "Problem" + guid;
            sheet.Cells[startRow, 6].Value = "Action-" + guid;
            sheet.Cells[startRow, 7].Value = "Usage Spare Part-" + guid;
            sheet.Cells[startRow, 8].Value = "Description" + guid;
            sheet.Cells[startRow, 9].Value = "Operator1" + guid;
            sheet.Cells[startRow, 10].Value = "Operator2" + guid;
            sheet.Cells[startRow, 11].Value = "Operator3" + guid;
            sheet.Cells[startRow, 12].Value = "Known By" + guid;
            sheet.Cells[startRow, 13].Value = "Team" + guid;
            sheet.Cells[startRow, 14].Value = DateTime.Now.ToString();

            return excel;
        }

        #region Success
        [Fact]
        public async Task UploadHandoverSheetSuccess()
        {
            var sqlDataContext = new Mock<ISqlDataContext<DashboardHandoverModel>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<DashboardHandoverModel>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<DashboardHandoverModel>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<DashboardHandoverModel>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(IDashboardHandoverAdapter))).Returns(new HandoverAdapter(serviceProvider.Object));

            UploadExcelMaintenanceService uploadExcelMaintenanceService = new UploadExcelMaintenanceService(serviceProvider.Object);

            var excel = GenerateHandoverSheet();
            await uploadExcelMaintenanceService.Upload(excel, DateTime.Now);
            Assert.True(true);
        }
        [Fact]
        public async Task UploadLKMSheetSuccess()
        {
            var sqlDataContext = new Mock<ISqlDataContext<DashboardLkmModel>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<DashboardLkmModel>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<DashboardLkmModel>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<DashboardLkmModel>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(IDashboardLkmAdapter))).Returns(new LkmAdapter(serviceProvider.Object));

            UploadExcelMaintenanceService uploadExcelMaintenanceService = new UploadExcelMaintenanceService(serviceProvider.Object);

            var excel = GenerateLKMSheet();
            await uploadExcelMaintenanceService.Upload(excel, DateTime.Now);
            Assert.True(true);
        }
        #endregion

        #region Success
        [Fact]
        public async Task UploadHandoverValidatePeriodeFailed()
        {
            var sqlDataContext = new Mock<ISqlDataContext<DashboardHandoverModel>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<DashboardHandoverModel>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<DashboardHandoverModel>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<DashboardHandoverModel>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(IDashboardHandoverAdapter))).Returns(new HandoverAdapter(serviceProvider.Object));

            UploadExcelMaintenanceService uploadExcelMaintenanceService = new UploadExcelMaintenanceService(serviceProvider.Object);

            var excel = GenerateHandoverSheet();
            Exception message = await Assert.ThrowsAnyAsync<Exception>(() => uploadExcelMaintenanceService.Upload(excel, DateTime.Now.AddMonths(1)));
            Assert.Equal("Gagal memproses Sheet Serah_Terima pada baris ke-4 - Periode yang dipilih tidak sesuai dengan periode data excel", message.Message);
        }
        [Fact]
        public async Task UploadLKMValidatePeriodeFailed()
        {
            var sqlDataContext = new Mock<ISqlDataContext<DashboardLkmModel>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<DashboardLkmModel>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<DashboardLkmModel>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<DashboardLkmModel>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(IDashboardLkmAdapter))).Returns(new LkmAdapter(serviceProvider.Object));

            UploadExcelMaintenanceService uploadExcelMaintenanceService = new UploadExcelMaintenanceService(serviceProvider.Object);

            var excel = GenerateLKMSheet();
            Exception message = await Assert.ThrowsAnyAsync<Exception>(() => uploadExcelMaintenanceService.Upload(excel, DateTime.Now.AddMonths(1)));
            Assert.Equal("Gagal memproses Sheet LKM pada baris ke-4 - Periode yang dipilih tidak sesuai dengan periode data excel", message.Message);
        }
        [Fact]
        public async Task UploadValidateSheetNameFailed()
        {
            var sqlDataContext = new Mock<ISqlDataContext<DashboardLkmModel>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<DashboardLkmModel>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<DashboardLkmModel>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<DashboardLkmModel>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(IDashboardLkmAdapter))).Returns(new LkmAdapter(serviceProvider.Object));

            UploadExcelMaintenanceService uploadExcelMaintenanceService = new UploadExcelMaintenanceService(serviceProvider.Object);

            var excel = generateTemplateSheet.GenerateTempalate("Test", 3, 10);
            Exception message = await Assert.ThrowsAnyAsync<Exception>(() => uploadExcelMaintenanceService.Upload(excel, DateTime.Now.AddMonths(1)));
            Assert.Equal("Test tidak valid", message.Message);
        }
        #endregion
    }
}
