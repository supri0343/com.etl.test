using Castle.Core.Logging;
using Com.Danliris.ETL.Service.DBAdapters.PrintingAdapters;
using Com.Danliris.ETL.Service.Models.DashboardPrintingModels;
using Com.Danliris.ETL.Service.Services;
using Com.Danliris.ETL.Service.Services.Class;
using Com.Danliris.ETL.Service.Tools;
using Moq;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.ETL.Service.Test.Services
{
    public class UploadExcelPrintingServiceTest
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

        private ExcelWorksheets GenerateMesinUtamaSheet()
        {
            var guid = Guid.NewGuid().ToString("N").Substring(0, 5);
            Random rnd = new Random();

            var startRow = 3;
            var excel = GenerateTemplateSheet("MESIN_UTAMA", startRow, 18);
            var sheet = excel[0];

            sheet.Cells[startRow, 1].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 2].Value = DateTime.Now.ToString("dd/MM/yyyy");
            sheet.Cells[startRow, 3].Value = "Mesin-" + guid;
            sheet.Cells[startRow, 4].Value = "Shift";
            sheet.Cells[startRow, 5].Value = "Group-" + guid;
            sheet.Cells[startRow, 6].Value = "NoOrder-" + guid;
            sheet.Cells[startRow, 7].Value = "Material-" + guid;
            sheet.Cells[startRow, 8].Value = "WarnaC/W-" + guid;
            sheet.Cells[startRow, 9].Value = "No_Kereta-" + guid;
            sheet.Cells[startRow, 10].Value = "Jenis_Proses-" + guid;
            sheet.Cells[startRow, 11].Value = "Kode_Motif-" + guid;
            sheet.Cells[startRow, 12].Value = "Kode_Design-" + guid;
            sheet.Cells[startRow, 13].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 14].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 15].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 16].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 17].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 18].Value = rnd.Next(1, 10);

            return excel;
        }

        private ExcelWorksheets GenerateMesinPendukungSheet()
        {
            var guid = Guid.NewGuid().ToString("N").Substring(0, 5);
            Random rnd = new Random();

            var startRow = 3;
            var excel = GenerateTemplateSheet("MESIN_PENDUKUNG", startRow, 14);
            var sheet = excel[0];

            sheet.Cells[startRow, 1].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 2].Value = DateTime.Now.ToString("dd/MM/yyyy");
            sheet.Cells[startRow, 3].Value = "Mesin-" + guid;
            sheet.Cells[startRow, 4].Value = "Shift";
            sheet.Cells[startRow, 5].Value = "Group-" + guid;
            sheet.Cells[startRow, 6].Value = "NoOrder-" + guid;
            sheet.Cells[startRow, 7].Value = "Material-" + guid;
            sheet.Cells[startRow, 8].Value = "Kode_motif-" + guid;
            sheet.Cells[startRow, 9].Value = "No_Kereta-" + guid;
            sheet.Cells[startRow, 10].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 11].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 12].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 13].Value = rnd.Next(1, 10);

            return excel;
        }

        private ExcelWorksheets GeneratePastaSheet()
        {
            var guid = Guid.NewGuid().ToString("N").Substring(0, 5);
            Random rnd = new Random();

            var startRow = 3;
            var excel = GenerateTemplateSheet("Pasta", startRow, 10);
            var sheet = excel[0];

            sheet.Cells[startRow, 1].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 2].Value = DateTime.Now.ToString("dd/MM/yyyy");
            sheet.Cells[startRow, 3].Value = "Produksi-" + guid;
            sheet.Cells[startRow, 4].Value = "Shift-";
            sheet.Cells[startRow, 5].Value = "Group-" + guid;
            sheet.Cells[startRow, 6].Value = "NoSPP-" + guid;
            sheet.Cells[startRow, 7].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 8].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 9].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 10].Value = rnd.Next(1, 10);      

            return excel;
        }

        private ExcelWorksheets GenerateEngravingSheet()
        {
            var guid = Guid.NewGuid().ToString("N").Substring(0, 5);
            Random rnd = new Random();

            var startRow = 3;
            var excel = GenerateTemplateSheet("ENGRAVING", startRow, 8);
            var sheet = excel[0];

            sheet.Cells[startRow, 1].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 2].Value = DateTime.Now.ToString("dd/MM/yyyy");
            sheet.Cells[startRow, 3].Value = "Shift-";
            sheet.Cells[startRow, 4].Value = "Group-" + guid;
            sheet.Cells[startRow, 5].Value = "Mesin-" + guid;
            sheet.Cells[startRow, 6].Value = "NoSPP-" + guid;
            sheet.Cells[startRow, 7].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 8].Value = rnd.Next(1, 10);                        

            return excel;
        }

        private ExcelWorksheets GenerateWIPSerahTerimaSheet()
        {
            var guid = Guid.NewGuid().ToString("N").Substring(0, 5);
            Random rnd = new Random();

            var startRow = 3;
            var excel = GenerateTemplateSheet("WIP_(Serah_Terima_Area)", startRow, 21);
            var sheet = excel[0];

            sheet.Cells[startRow, 1].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 2].Value = DateTime.Now.ToString("dd/MM/yyyy");
            sheet.Cells[startRow, 3].Value = "SP-";
            sheet.Cells[startRow, 4].Value = "Konstruksi-" + guid;
            sheet.Cells[startRow, 5].Value = "Kereta-" + guid;
            sheet.Cells[startRow, 6].Value = "Activity-" + guid;
            sheet.Cells[startRow, 7].Value = "Produksi/Subcon-" + guid;
            sheet.Cells[startRow, 8].Value = "Asal/Tujuan-" + guid;
            sheet.Cells[startRow, 9].Value = "Reproses-" + guid;
            sheet.Cells[startRow, 10].Value = "Keterangan-" + guid;
            sheet.Cells[startRow, 11].Value = "Area-" + guid;
            sheet.Cells[startRow, 12].Value = DateTime.Now.ToString("dd/MM/yyyy");
            sheet.Cells[startRow, 13].Value = "Keterangan/Subcon-" + guid;
            sheet.Cells[startRow, 14].Value = "JenisProses-" + guid;
            sheet.Cells[startRow, 15].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 16].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 17].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 18].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 19].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 20].Value = "No_Benang-" + guid;
            sheet.Cells[startRow, 21].Value = rnd.Next(1, 10);
            
            
            return excel;
        }

        [Fact]
        public async Task UploadSheetMesinUtamaSuccess()
        {

            var sqlDataContext = new Mock<ISqlDataContext<DashBoardMainMachineModel>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<DashBoardMainMachineModel>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<DashBoardMainMachineModel>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<DashBoardMainMachineModel>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(IMainMachineAdapter))).Returns(new MainMachineAdapter(serviceProvider.Object));



            UploadExcelPrintingService uploadExcelDyeingService = new UploadExcelPrintingService(serviceProvider.Object);
            var log = new Mock<ILogger>();

            var excel = GenerateMesinUtamaSheet();

            await uploadExcelDyeingService.Upload(excel, DateTime.Now);


            Assert.True(true);
        }

        [Fact]
        public async Task UploadSheetMesinPendukungSuccess()
        {

            var sqlDataContext = new Mock<ISqlDataContext<DashBoardSupportMachineModel>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<DashBoardSupportMachineModel>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<DashBoardSupportMachineModel>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<DashBoardSupportMachineModel>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(ISupportMachineAdapter))).Returns(new SupportMachineAdapter(serviceProvider.Object));



            UploadExcelPrintingService uploadExcelDyeingService = new UploadExcelPrintingService(serviceProvider.Object);
            var log = new Mock<ILogger>();

            var excel = GenerateMesinPendukungSheet();

            await uploadExcelDyeingService.Upload(excel, DateTime.Now);


            Assert.True(true);
        }

        [Fact]
        public async Task UploadSheetPasteSuccess()
        {

            var sqlDataContext = new Mock<ISqlDataContext<DashBoardPasteModel>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<DashBoardPasteModel>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<DashBoardPasteModel>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<DashBoardPasteModel>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(IPasteAdapter))).Returns(new PasteAdapter(serviceProvider.Object));



            UploadExcelPrintingService uploadExcelDyeingService = new UploadExcelPrintingService(serviceProvider.Object);
            var log = new Mock<ILogger>();

            var excel = GeneratePastaSheet();

            await uploadExcelDyeingService.Upload(excel, DateTime.Now);


            Assert.True(true);
        }

        [Fact]
        public async Task UploadEngravingSuccess()
        {

            var sqlDataContext = new Mock<ISqlDataContext<DashBoardEngravingModel>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<DashBoardEngravingModel>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<DashBoardEngravingModel>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<DashBoardEngravingModel>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(IEngravingAdapter))).Returns(new EngravingAdapter(serviceProvider.Object));



            UploadExcelPrintingService uploadExcelDyeingService = new UploadExcelPrintingService(serviceProvider.Object);
            var log = new Mock<ILogger>();

            var excel = GenerateEngravingSheet();

            await uploadExcelDyeingService.Upload(excel, DateTime.Now);


            Assert.True(true);
        }

        [Fact]
        public async Task UploadSheetWIPSerahTerimaSuccess()
        {

            var sqlDataContext = new Mock<ISqlDataContext<DashBoardWIPHandoverAreaModel>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<DashBoardWIPHandoverAreaModel>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<DashBoardWIPHandoverAreaModel>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<DashBoardWIPHandoverAreaModel>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(IWipHandoverAdapter))).Returns(new WipHandoverAdapter(serviceProvider.Object));



            UploadExcelPrintingService uploadExcelDyeingService = new UploadExcelPrintingService(serviceProvider.Object);
            var log = new Mock<ILogger>();

            var excel = GenerateWIPSerahTerimaSheet();

            await uploadExcelDyeingService.Upload(excel, DateTime.Now);


            Assert.True(true);
        }

    }
}
