using Com.Danliris.ETL.Service.DBAdapters.GudangChemicalAdapters;
using Com.Danliris.ETL.Service.ExcelModels.DashboardDyeingModels;
using Com.Danliris.ETL.Service.Models.DashboardGudangChemicalModels;
using Com.Danliris.ETL.Service.Services;
using Com.Danliris.ETL.Service.Tools;
using Microsoft.Extensions.Logging;
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
    public class UploadExcelGudangChemicalTest
    {
        private ExcelWorksheets GenerateTempalateSheet(string name, int endRow, int endCol)
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

        private ExcelWorksheets GenerateChemicalStockSheet()
        {
            var guid = Guid.NewGuid().ToString("N").Substring(0, 5);
            Random rnd = new Random();

            var startRow = 3;
            var excel = GenerateTempalateSheet("STOCK_CHEMICAL", startRow, 14);
            var sheet = excel[0];

            sheet.Cells[startRow, 1].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 2].Value = DateTime.Now.ToString("dd/MM/yyyy");
            sheet.Cells[startRow, 3].Value = "ItemCode-" + guid;
            sheet.Cells[startRow, 4].Value = "RackNo";
            sheet.Cells[startRow, 5].Value = "MasterKd-" + guid;
            sheet.Cells[startRow, 6].Value = "ItemName-" + guid;
            sheet.Cells[startRow, 7].Value = "Unit-" + guid;
            sheet.Cells[startRow, 8].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 9].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 10].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 11].Value = rnd.Next(1, 10);

            return excel;
        }

        private ExcelWorksheets GeneratePengeluaranBarangSheet()
        {
            var guid = Guid.NewGuid().ToString("N").Substring(0, 5);
            Random rnd = new Random();

            var startRow = 3;
            var excel = GenerateTempalateSheet("PENGELUARAN_BARANG", startRow, 14);
            var sheet = excel[0];

            sheet.Cells[startRow, 1].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 2].Value = DateTime.Now.ToString("dd/MM/yyyy");
            sheet.Cells[startRow, 3].Value = "No.Bon-" + guid;
            sheet.Cells[startRow, 4].Value = "Kode_Barang";
            sheet.Cells[startRow, 5].Value = "Nama_Barang-" + guid;
            sheet.Cells[startRow, 6].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 7].Value = "Sat-" + guid;
            sheet.Cells[startRow, 8].Value = "M/C-" + guid;
            sheet.Cells[startRow, 9].Value = "Yang_Mengambil-" + guid;
            sheet.Cells[startRow, 10].Value = "Bagian-" + guid;
            

            return excel;
        }

        private ExcelWorksheets GeneratePenerimaanBarangSheet()
        {
            var guid = Guid.NewGuid().ToString("N").Substring(0, 5);
            Random rnd = new Random();

            var startRow = 4;
            var excel = GenerateTempalateSheet("PENERIMAAN_BARANG", startRow, 14);
            var sheet = excel[0];

            sheet.Cells[startRow, 1].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 2].Value = DateTime.Now.ToString("dd/MM/yyyy");
            sheet.Cells[startRow, 3].Value = "Unit/Area-" + guid;
            sheet.Cells[startRow, 4].Value = "No.Bon";
            sheet.Cells[startRow, 5].Value = "Nama_Supplier-" + guid;
            sheet.Cells[startRow, 6].Value = "No_PO-" + guid;
            sheet.Cells[startRow, 7].Value = "Code-" + guid;
            sheet.Cells[startRow, 8].Value = "Nama_Barang-" + guid;
            sheet.Cells[startRow, 9].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 10].Value = "Sat-" + guid;
            sheet.Cells[startRow, 11].Value = "Keterangan-" + guid;
            


            return excel;
        }


        [Fact]
        public async Task UploadSheetChemicalStockSuccess()
        {
            string rootPath = Directory.GetCurrentDirectory();
            string testFolderPath = @"Files\";
            string testFile = "DASHBOARD GUDANG CHEMICAL.xlsx";
            string fullPath = Path.Combine(rootPath, testFolderPath, testFile);

            var sqlDataContext = new Mock<ISqlDataContext<ChemicalStockModel>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<ChemicalStockModel>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<ChemicalStockModel>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<ChemicalStockModel>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(IChemicalStockAdapter))).Returns(new ChemicalStockAdapter(serviceProvider.Object));



            UploadExcelGudangChemicalService uploadExcelDyeingService = new UploadExcelGudangChemicalService(serviceProvider.Object);
            var log = new Mock<ILogger>();

            var excel = GenerateChemicalStockSheet();

            await uploadExcelDyeingService.Upload(excel, DateTime.Now);
            

            Assert.True(true);
        }

        [Fact]
        public async Task UploadSheetPengeluaranBarangSuccess()
        {
            string rootPath = Directory.GetCurrentDirectory();
            string testFolderPath = @"Files\";
            string testFile = "DASHBOARD GUDANG CHEMICAL.xlsx";
            string fullPath = Path.Combine(rootPath, testFolderPath, testFile);

            var sqlDataContext = new Mock<ISqlDataContext<ChemicalReleaseItemModel>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<ChemicalReleaseItemModel>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<ChemicalReleaseItemModel>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<ChemicalReleaseItemModel>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(IReleaseItemAdapter))).Returns(new ReleaseItemAdapter(serviceProvider.Object));



            UploadExcelGudangChemicalService uploadExcelDyeingService = new UploadExcelGudangChemicalService(serviceProvider.Object);
            var log = new Mock<ILogger>();

            var excel = GeneratePengeluaranBarangSheet();

            await uploadExcelDyeingService.Upload(excel, DateTime.Now);
            

            Assert.True(true);
        }

        [Fact]
        public async Task UploadSheetPenerimaanBarangSuccess()
        {
            string rootPath = Directory.GetCurrentDirectory();
            string testFolderPath = @"Files\";
            string testFile = "DASHBOARD GUDANG CHEMICAL.xlsx";
            string fullPath = Path.Combine(rootPath, testFolderPath, testFile);

            var sqlDataContext = new Mock<ISqlDataContext<ChemicalReceiptItemModel>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<ChemicalReceiptItemModel>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<ChemicalReceiptItemModel>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<ChemicalReceiptItemModel>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(IReceiptItemAdapter))).Returns(new ReceiptItemAdapter(serviceProvider.Object));



            UploadExcelGudangChemicalService uploadExcelDyeingService = new UploadExcelGudangChemicalService(serviceProvider.Object);
            var log = new Mock<ILogger>();

            var excel = GeneratePenerimaanBarangSheet();

            await uploadExcelDyeingService.Upload(excel, DateTime.Now);
            

            Assert.True(true);
        }

        //[Fact]
        //public async Task UploadExcelGudangChemicalSuccess()
        //{
        //    string rootPath = Directory.GetCurrentDirectory();
        //    string testFolderPath = @"Files\";
        //    string testFile = "DASHBOARD GUDANG CHEMICAL.xlsx";
        //    string fullPath = Path.Combine(rootPath, testFolderPath, testFile);

        //    var sqlDataContext = new Mock<ISqlDataContext<ChemicalStockModel>>();
        //    var serviceProvider = new Mock<IServiceProvider>();

        //    sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<ChemicalStockModel>())).ReturnsAsync(1);
        //    sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<ChemicalStockModel>());
        //    serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<ChemicalStockModel>))).Returns(sqlDataContext.Object);
        //    serviceProvider.Setup(x => x.GetService(typeof(IChemicalStockAdapter))).Returns(new ChemicalStockAdapter(serviceProvider.Object));

        //    var sqlDataContextRelease = new Mock<ISqlDataContext<ChemicalReleaseItemModel>>();
        //    sqlDataContextRelease.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<ChemicalReleaseItemModel>())).ReturnsAsync(1);
        //    sqlDataContextRelease.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<ChemicalReleaseItemModel>());

        //    serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<ChemicalReleaseItemModel>))).Returns(sqlDataContextRelease.Object);
        //    serviceProvider.Setup(x => x.GetService(typeof(IReleaseItemAdapter))).Returns(new ReleaseItemAdapter(serviceProvider.Object));

        //    var sqlDataContextReceipt = new Mock<ISqlDataContext<ChemicalReceiptItemModel>>();
        //    sqlDataContextReceipt.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<ChemicalReceiptItemModel>())).ReturnsAsync(1);
        //    sqlDataContextReceipt.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<ChemicalReceiptItemModel>());

        //    serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<ChemicalReceiptItemModel>))).Returns(sqlDataContextReceipt.Object);
        //    serviceProvider.Setup(x => x.GetService(typeof(IReceiptItemAdapter))).Returns(new ReceiptItemAdapter(serviceProvider.Object));



        //    UploadExcelGudangChemicalService uploadExcelDyeingService = new UploadExcelGudangChemicalService(serviceProvider.Object);
        //    var log = new Mock<ILogger>();

        //    var excel = GenerateDyeingMachineSheet();
        //    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        //    using (var sheet = new ExcelPackage())
        //    {
        //        using (FileStream fs = File.OpenRead(fullPath))
        //        {
        //            sheet.Load(fs);
        //        }
        //        var excelSheet = sheet.Workbook.Worksheets;
        //        await uploadExcelDyeingService.Upload(excelSheet, DateTime.Now);
        //    }

        //    Assert.True(true);
        //}
    }
}
