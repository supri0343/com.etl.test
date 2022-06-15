using Com.Danliris.ETL.Service.DBAdapters.KoranQCLineAdapters;
using Com.Danliris.ETL.Service.ExcelModels.KoranQCLine;
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
    public class UploadExcelAreaQCServiceTest
    {
        GenerateTemplateSheet generateTemplateSheet = new GenerateTemplateSheet();

        private ExcelWorksheets GenerateDigitalPrintSheet()
        {
            var guid = Guid.NewGuid().ToString("N").Substring(0, 5);
            Random rnd = new Random();

            var startRow = 3;
            var excel = generateTemplateSheet.GenerateTempalate("Digital Print", startRow, 18);
            var sheet = excel[0];

            sheet.Cells[startRow, 1].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 2].Value = DateTime.Now.ToString();
            sheet.Cells[startRow, 3].Value = "Operator-" + guid;
            sheet.Cells[startRow, 4].Value = "A";
            sheet.Cells[startRow, 5].Value = "Shift-" + guid;
            sheet.Cells[startRow, 6].Value = "No Oder-" + guid;
            sheet.Cells[startRow, 7].Value = "Material-" + guid;
            sheet.Cells[startRow, 8].Value = "Warna-" + guid;
            sheet.Cells[startRow, 9].Value = "Buyer" + guid;
            sheet.Cells[startRow, 10].Value = "Jenis Kereta" + guid;
            sheet.Cells[startRow, 11].Value = rnd.Next(50, 100); 
            sheet.Cells[startRow, 12].Value = rnd.Next(50, 100);
            sheet.Cells[startRow, 13].Value = rnd.Next(100, 2000);
            sheet.Cells[startRow, 14].Value = rnd.Next(100, 2000);
            sheet.Cells[startRow, 15].Value = rnd.Next(100, 2000);
            sheet.Cells[startRow, 16].Value = "Description1" + guid;
            sheet.Cells[startRow, 17].Value = "Description2" + guid;
            sheet.Cells[startRow, 18].Value = "Description3" + guid;

            return excel;
        }
        private ExcelWorksheets GenerateDyeingPretreatmentPrintingSheet(string name)
        {
            var guid = Guid.NewGuid().ToString("N").Substring(0, 5);
            Random rnd = new Random();

            var startRow = 3;
            var excel = generateTemplateSheet.GenerateTempalate(name, startRow, 20);
            var sheet = excel[0];

            sheet.Cells[startRow, 1].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 2].Value = DateTime.Now.ToString();
            sheet.Cells[startRow, 3].Value = "Operator-" + guid;
            sheet.Cells[startRow, 4].Value = "A";
            sheet.Cells[startRow, 5].Value = "Shift-" + guid;
            sheet.Cells[startRow, 6].Value = "Mesin" + guid;
            sheet.Cells[startRow, 7].Value = "Grup Mesin-" + guid;
            sheet.Cells[startRow, 8].Value = "No Order-" + guid;
            sheet.Cells[startRow, 9].Value = "Material-" + guid;
            sheet.Cells[startRow, 10].Value = "Warna" + guid;
            sheet.Cells[startRow, 11].Value = "Buyer" + guid;
            sheet.Cells[startRow, 12].Value = "No Kerete" + guid;
            sheet.Cells[startRow, 13].Value = rnd.Next(50, 100);
            sheet.Cells[startRow, 14].Value = rnd.Next(100, 2000);
            sheet.Cells[startRow, 15].Value = rnd.Next(100, 2000);
            sheet.Cells[startRow, 16].Value = rnd.Next(100, 2000);
            sheet.Cells[startRow, 17].Value = rnd.Next(100, 2000);
            sheet.Cells[startRow, 18].Value = "Description1" + guid;
            sheet.Cells[startRow, 19].Value = "Description2" + guid;
            sheet.Cells[startRow, 20].Value = "Description3" + guid;

            return excel;
        }
        #region Success
        [Fact]
        public async Task UploadDigitalPrintSheetSuccess()
        {
            var sqlDataContext = new Mock<ISqlDataContext<DigitalPrint>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<DigitalPrint>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<DigitalPrint>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<DigitalPrint>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(IDigitalPrintAdapter))).Returns(new DigitalPrintAdapter(serviceProvider.Object));

            UploadExcelAreaQCService uploadExcelAreaQCService = new UploadExcelAreaQCService(serviceProvider.Object);

            var excel = GenerateDigitalPrintSheet();
            await uploadExcelAreaQCService.Upload(excel, DateTime.Now);
            Assert.True(true);
        }
        [Fact]
        public async Task UploadDyeingSheetSuccess()
        {
            var sqlDataContext = new Mock<ISqlDataContext<Dyeing>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<Dyeing>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<Dyeing>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<Dyeing>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(IDyeingAdapter))).Returns(new DyeingAdapter(serviceProvider.Object));

            UploadExcelAreaQCService uploadExcelAreaQCService = new UploadExcelAreaQCService(serviceProvider.Object);

            var excel = GenerateDyeingPretreatmentPrintingSheet("Dyeing");
            await uploadExcelAreaQCService.Upload(excel, DateTime.Now);
            Assert.True(true);
        }
        [Fact]
        public async Task UploadPretreatmentSheetSuccess()
        {
            var sqlDataContext = new Mock<ISqlDataContext<Pretreatment>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<Pretreatment>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<Pretreatment>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<Pretreatment>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(IPretreatmentAdapter))).Returns(new PretreatmentAdapter(serviceProvider.Object));

            UploadExcelAreaQCService uploadExcelAreaQCService = new UploadExcelAreaQCService(serviceProvider.Object);

            var excel = GenerateDyeingPretreatmentPrintingSheet("Pretreatment");
            await uploadExcelAreaQCService.Upload(excel, DateTime.Now);
            Assert.True(true);
        }
        [Fact]
        public async Task UploadPrintingSheetSuccess()
        {
            var sqlDataContext = new Mock<ISqlDataContext<Printing>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<Printing>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<Printing>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<Printing>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(IPrintingAdapter))).Returns(new PrintingAdapter(serviceProvider.Object));

            UploadExcelAreaQCService uploadExcelAreaQCService = new UploadExcelAreaQCService(serviceProvider.Object);

            var excel = GenerateDyeingPretreatmentPrintingSheet("Printing");
            await uploadExcelAreaQCService.Upload(excel, DateTime.Now);
            Assert.True(true);
        }
        #endregion
        #region Failed
        [Fact]
        public async Task UploadDigitalPrintValidatePeriodeFailed()
        {
            var sqlDataContext = new Mock<ISqlDataContext<DigitalPrint>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<DigitalPrint>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<DigitalPrint>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<DigitalPrint>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(IDigitalPrintAdapter))).Returns(new DigitalPrintAdapter(serviceProvider.Object));

            UploadExcelAreaQCService uploadExcelAreaQCService = new UploadExcelAreaQCService(serviceProvider.Object);

            var excel = GenerateDigitalPrintSheet();
            Exception message = await Assert.ThrowsAnyAsync<Exception>(() => uploadExcelAreaQCService.Upload(excel, DateTime.Now.AddMonths(1)));
            Assert.Equal("Gagal memproses Sheet Digital Print pada baris ke-3 - Periode yang dipilih tidak sesuai dengan periode data excel", message.Message);
        }
        [Fact]
        public async Task UploadDyeingValidatePeriodeFailed()
        {
            var sqlDataContext = new Mock<ISqlDataContext<Dyeing>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<Dyeing>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<Dyeing>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<Dyeing>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(IDyeingAdapter))).Returns(new DyeingAdapter(serviceProvider.Object));

            UploadExcelAreaQCService uploadExcelAreaQCService = new UploadExcelAreaQCService(serviceProvider.Object);

            var excel = GenerateDyeingPretreatmentPrintingSheet("Dyeing");
            Exception message = await Assert.ThrowsAnyAsync<Exception>(() => uploadExcelAreaQCService.Upload(excel, DateTime.Now.AddMonths(1)));
            Assert.Equal("Gagal memproses Sheet Dyeing pada baris ke-3 - Periode yang dipilih tidak sesuai dengan periode data excel", message.Message);
        }
        [Fact]
        public async Task UploadPretreatmentValidatePeriodeFailed()
        {
            var sqlDataContext = new Mock<ISqlDataContext<Pretreatment>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<Pretreatment>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<Pretreatment>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<Pretreatment>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(IPretreatmentAdapter))).Returns(new PretreatmentAdapter(serviceProvider.Object));

            UploadExcelAreaQCService uploadExcelAreaQCService = new UploadExcelAreaQCService(serviceProvider.Object);

            var excel = GenerateDyeingPretreatmentPrintingSheet("Pretreatment");
            Exception message = await Assert.ThrowsAnyAsync<Exception>(() => uploadExcelAreaQCService.Upload(excel, DateTime.Now.AddMonths(1)));
            Assert.Equal("Gagal memproses Sheet Pretreatment pada baris ke-3 - Periode yang dipilih tidak sesuai dengan periode data excel", message.Message);
        }
        [Fact]
        public async Task UploadPrintingValidatePeriodeFailed()
        {
            var sqlDataContext = new Mock<ISqlDataContext<Printing>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<Printing>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<Printing>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<Printing>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(IPrintingAdapter))).Returns(new PrintingAdapter(serviceProvider.Object));

            UploadExcelAreaQCService uploadExcelAreaQCService = new UploadExcelAreaQCService(serviceProvider.Object);

            var excel = GenerateDyeingPretreatmentPrintingSheet("Printing");
            Exception message = await Assert.ThrowsAnyAsync<Exception>(() => uploadExcelAreaQCService.Upload(excel, DateTime.Now.AddMonths(1)));
            Assert.Equal("Gagal memproses Sheet Printing pada baris ke-3 - Periode yang dipilih tidak sesuai dengan periode data excel", message.Message);
        }
        [Fact]
        public async Task UploadValidateSheetNameFailed()
        {
            var sqlDataContext = new Mock<ISqlDataContext<Printing>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<Printing>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<Printing>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<Printing>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(IPrintingAdapter))).Returns(new PrintingAdapter(serviceProvider.Object));

            UploadExcelAreaQCService uploadExcelAreaQCService = new UploadExcelAreaQCService(serviceProvider.Object);

            var excel = generateTemplateSheet.GenerateTempalate("Test", 3, 14);
            Exception message = await Assert.ThrowsAnyAsync<Exception>(() => uploadExcelAreaQCService.Upload(excel, DateTime.Now.AddMonths(1)));
            Assert.Equal("Test tidak valid", message.Message);
        }
        #endregion

    }
}
