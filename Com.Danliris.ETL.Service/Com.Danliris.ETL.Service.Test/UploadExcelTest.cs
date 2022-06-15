using Com.Danliris.ETL.Service.Services;
using Microsoft.Extensions.Logging;
using Moq;
using OfficeOpenXml;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.ETL.Service.Test
{
    public class UploadExcelTest
    {

        [Fact]
        public async Task UploadFileInvalidSheet()
        {
            //var guid = Guid.NewGuid().ToString("N").Substring(0, 5);
            //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            //ExcelPackage excelPackage = new ExcelPackage();
            //ExcelWorksheets sheets = excelPackage.Workbook.Worksheets;
            //sheets.Add("Sheet-" + guid);

            //var log = new Mock<ILogger>();
            //UploadExcelDyeingService uploadExcelDyeingService = new UploadExcelDyeingService();
            //Exception message = await Assert.ThrowsAnyAsync<Exception>(() => uploadExcelDyeingService.Upload(sheets, log.Object));
            //Assert.Equal($"Sheet-{guid} tidak valid", message.Message);
        }
    }
}
